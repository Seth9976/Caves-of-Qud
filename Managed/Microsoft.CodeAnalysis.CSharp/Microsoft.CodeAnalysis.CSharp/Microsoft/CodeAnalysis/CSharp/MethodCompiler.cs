using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CodeGen;
using Microsoft.CodeAnalysis.CSharp.CodeGen;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Debugging;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000118 RID: 280
	internal sealed class MethodCompiler : CSharpSymbolVisitor<TypeCompilationState, object>
	{
		// Token: 0x0600173A RID: 5946 RVA: 0x0007E8EC File Offset: 0x0007CAEC
		private void SetGlobalErrorIfTrue(bool arg)
		{
			if (arg)
			{
				this._globalHasErrors = true;
			}
		}

		// Token: 0x0600173B RID: 5947 RVA: 0x0007E8F8 File Offset: 0x0007CAF8
		internal MethodCompiler(CSharpCompilation compilation, PEModuleBuilder moduleBeingBuiltOpt, bool emittingPdb, bool emitTestCoverageData, bool hasDeclarationErrors, bool emitMethodBodies, BindingDiagnosticBag diagnostics, Predicate<Symbol> filterOpt, SynthesizedEntryPointSymbol.AsyncForwardEntryPoint entryPointOpt, CancellationToken cancellationToken)
		{
			this._compilation = compilation;
			this._moduleBeingBuiltOpt = moduleBeingBuiltOpt;
			this._emittingPdb = emittingPdb;
			this._cancellationToken = cancellationToken;
			this._diagnostics = diagnostics;
			this._filterOpt = filterOpt;
			this._entryPointOpt = entryPointOpt;
			this._hasDeclarationErrors = hasDeclarationErrors;
			this.SetGlobalErrorIfTrue(hasDeclarationErrors);
			if (emittingPdb || emitTestCoverageData)
			{
				this._debugDocumentProvider = (string path, string basePath) => moduleBeingBuiltOpt.DebugDocumentsBuilder.GetOrAddDebugDocument(path, basePath, new Func<string, DebugSourceDocument>(MethodCompiler.CreateDebugDocumentForFile));
			}
			this._emitTestCoverageData = emitTestCoverageData;
			this._emitMethodBodies = emitMethodBodies;
		}

		// Token: 0x0600173C RID: 5948 RVA: 0x0007E98C File Offset: 0x0007CB8C
		public static void CompileMethodBodies(CSharpCompilation compilation, PEModuleBuilder moduleBeingBuiltOpt, bool emittingPdb, bool emitTestCoverageData, bool hasDeclarationErrors, bool emitMethodBodies, BindingDiagnosticBag diagnostics, Predicate<Symbol> filterOpt, CancellationToken cancellationToken)
		{
			if (compilation.PreviousSubmission != null)
			{
				compilation.PreviousSubmission.EnsureAnonymousTypeTemplates(cancellationToken);
			}
			MethodSymbol methodSymbol = null;
			if (filterOpt == null)
			{
				methodSymbol = MethodCompiler.GetEntryPoint(compilation, moduleBeingBuiltOpt, hasDeclarationErrors, emitMethodBodies, diagnostics, cancellationToken);
			}
			MethodCompiler methodCompiler = new MethodCompiler(compilation, moduleBeingBuiltOpt, emittingPdb, emitTestCoverageData, hasDeclarationErrors, emitMethodBodies, diagnostics, filterOpt, methodSymbol as SynthesizedEntryPointSymbol.AsyncForwardEntryPoint, cancellationToken);
			if (compilation.Options.ConcurrentBuild)
			{
				methodCompiler._compilerTasks = new ConcurrentStack<Task>();
			}
			methodCompiler.CompileNamespace(compilation.SourceModule.GlobalNamespace);
			methodCompiler.WaitForWorkers();
			if (moduleBeingBuiltOpt != null)
			{
				ImmutableArray<NamedTypeSymbol> additionalTopLevelTypes = moduleBeingBuiltOpt.GetAdditionalTopLevelTypes();
				methodCompiler.CompileSynthesizedMethods(additionalTopLevelTypes, diagnostics);
				ImmutableArray<NamedTypeSymbol> embeddedTypes = moduleBeingBuiltOpt.GetEmbeddedTypes(diagnostics);
				methodCompiler.CompileSynthesizedMethods(embeddedTypes, diagnostics);
				if (emitMethodBodies)
				{
					compilation.AnonymousTypeManager.AssignTemplatesNamesAndCompile(methodCompiler, moduleBeingBuiltOpt, diagnostics);
				}
				methodCompiler.WaitForWorkers();
				PrivateImplementationDetails privateImplClass = moduleBeingBuiltOpt.PrivateImplClass;
				if (privateImplClass != null)
				{
					privateImplClass.Freeze();
					methodCompiler.CompileSynthesizedMethods(privateImplClass, diagnostics);
				}
			}
			if (moduleBeingBuiltOpt != null && (methodCompiler._globalHasErrors || moduleBeingBuiltOpt.SourceModule.HasBadAttributes) && !diagnostics.HasAnyErrors() && !hasDeclarationErrors)
			{
				string text = (methodCompiler._globalHasErrors ? "UnableToDetermineSpecificCauseOfFailure" : "ModuleHasInvalidAttributes");
				diagnostics.Add(ErrorCode.ERR_ModuleEmitFailure, NoLocation.Singleton, new object[]
				{
					((INamedEntity)moduleBeingBuiltOpt).Name,
					new LocalizableResourceString(text, CodeAnalysisResources.ResourceManager, typeof(CodeAnalysisResources))
				});
			}
			diagnostics.AddRange(compilation.AdditionalCodegenWarnings);
			if (filterOpt == null)
			{
				MethodCompiler.WarnUnusedFields(compilation, diagnostics, cancellationToken);
				if (moduleBeingBuiltOpt != null && methodSymbol != null && compilation.Options.OutputKind.IsApplication())
				{
					moduleBeingBuiltOpt.SetPEEntryPoint(methodSymbol, diagnostics.DiagnosticBag);
				}
			}
		}

		// Token: 0x0600173D RID: 5949 RVA: 0x0007EB24 File Offset: 0x0007CD24
		private static MethodSymbol GetEntryPoint(CSharpCompilation compilation, PEModuleBuilder moduleBeingBuilt, bool hasDeclarationErrors, bool emitMethodBodies, BindingDiagnosticBag diagnostics, CancellationToken cancellationToken)
		{
			CSharpCompilation.EntryPoint entryPointAndDiagnostics = compilation.GetEntryPointAndDiagnostics(cancellationToken);
			diagnostics.AddRange(entryPointAndDiagnostics.Diagnostics, true);
			MethodSymbol methodSymbol = entryPointAndDiagnostics.MethodSymbol;
			if (methodSymbol == null)
			{
				return null;
			}
			SynthesizedEntryPointSymbol synthesizedEntryPointSymbol = methodSymbol as SynthesizedEntryPointSymbol;
			if (synthesizedEntryPointSymbol == null)
			{
				TypeSymbol returnType = methodSymbol.ReturnType;
				if (returnType.IsGenericTaskType(compilation) || returnType.IsNonGenericTaskType(compilation))
				{
					synthesizedEntryPointSymbol = new SynthesizedEntryPointSymbol.AsyncForwardEntryPoint(compilation, methodSymbol.ContainingType, methodSymbol);
					methodSymbol = synthesizedEntryPointSymbol;
					if (moduleBeingBuilt != null)
					{
						moduleBeingBuilt.AddSynthesizedDefinition(methodSymbol.ContainingType, synthesizedEntryPointSymbol.GetCciAdapter());
					}
				}
			}
			if (synthesizedEntryPointSymbol != null && moduleBeingBuilt != null && !hasDeclarationErrors && !diagnostics.HasAnyErrors())
			{
				BoundStatement boundStatement = synthesizedEntryPointSymbol.CreateBody(diagnostics);
				if (boundStatement.HasErrors || diagnostics.HasAnyErrors())
				{
					return methodSymbol;
				}
				ImmutableArray<SourceSpan> empty = ImmutableArray<SourceSpan>.Empty;
				VariableSlotAllocator variableSlotAllocator = null;
				ArrayBuilder<LambdaDebugInfo> instance = ArrayBuilder<LambdaDebugInfo>.GetInstance();
				ArrayBuilder<ClosureDebugInfo> instance2 = ArrayBuilder<ClosureDebugInfo>.GetInstance();
				StateMachineTypeSymbol stateMachineTypeSymbol = null;
				BoundStatement boundStatement2 = MethodCompiler.LowerBodyOrInitializer(synthesizedEntryPointSymbol, -1, boundStatement, null, new TypeCompilationState(synthesizedEntryPointSymbol.ContainingType, compilation, moduleBeingBuilt), false, null, ref empty, diagnostics, ref variableSlotAllocator, instance, instance2, out stateMachineTypeSymbol);
				instance.Free();
				instance2.Free();
				if (emitMethodBodies)
				{
					MethodBody methodBody = MethodCompiler.GenerateMethodBody(moduleBeingBuilt, synthesizedEntryPointSymbol, -1, boundStatement2, ImmutableArray<LambdaDebugInfo>.Empty, ImmutableArray<ClosureDebugInfo>.Empty, null, null, diagnostics, null, null, false, false, ImmutableArray<SourceSpan>.Empty, null);
					moduleBeingBuilt.SetMethodBody(synthesizedEntryPointSymbol, methodBody);
				}
			}
			return methodSymbol;
		}

		// Token: 0x0600173E RID: 5950 RVA: 0x0007EC5C File Offset: 0x0007CE5C
		private void WaitForWorkers()
		{
			ConcurrentStack<Task> compilerTasks = this._compilerTasks;
			if (compilerTasks == null)
			{
				return;
			}
			Task task;
			while (compilerTasks.TryPop(ref task))
			{
				task.GetAwaiter().GetResult();
			}
		}

		// Token: 0x0600173F RID: 5951 RVA: 0x0007EC8C File Offset: 0x0007CE8C
		private static void WarnUnusedFields(CSharpCompilation compilation, BindingDiagnosticBag diagnostics, CancellationToken cancellationToken)
		{
			SourceAssemblySymbol sourceAssemblySymbol = (SourceAssemblySymbol)compilation.Assembly;
			diagnostics.AddRange<Diagnostic>(sourceAssemblySymbol.GetUnusedFieldWarnings(cancellationToken));
		}

		// Token: 0x06001740 RID: 5952 RVA: 0x0007ECB4 File Offset: 0x0007CEB4
		public override object VisitNamespace(NamespaceSymbol symbol, TypeCompilationState arg)
		{
			if (!MethodCompiler.PassesFilter(this._filterOpt, symbol))
			{
				return null;
			}
			this._cancellationToken.ThrowIfCancellationRequested();
			if (this._compilation.Options.ConcurrentBuild)
			{
				Task task = this.CompileNamespaceAsAsync(symbol);
				this._compilerTasks.Push(task);
			}
			else
			{
				this.CompileNamespace(symbol);
			}
			return null;
		}

		// Token: 0x06001741 RID: 5953 RVA: 0x0007ED12 File Offset: 0x0007CF12
		private Task CompileNamespaceAsAsync(NamespaceSymbol symbol)
		{
			return Task.Run(UICultureUtilities.WithCurrentUICulture(delegate
			{
				try
				{
					this.CompileNamespace(symbol);
				}
				catch (Exception ex) when (FatalError.ReportAndPropagateUnlessCanceled(ex))
				{
					throw ExceptionUtilities.Unreachable;
				}
			}), this._cancellationToken);
		}

		// Token: 0x06001742 RID: 5954 RVA: 0x0007ED44 File Offset: 0x0007CF44
		private void CompileNamespace(NamespaceSymbol symbol)
		{
			foreach (Symbol symbol2 in symbol.GetMembersUnordered())
			{
				symbol2.Accept<TypeCompilationState, object>(this, null);
			}
		}

		// Token: 0x06001743 RID: 5955 RVA: 0x0007ED7C File Offset: 0x0007CF7C
		public override object VisitNamedType(NamedTypeSymbol symbol, TypeCompilationState arg)
		{
			if (!MethodCompiler.PassesFilter(this._filterOpt, symbol))
			{
				return null;
			}
			this._cancellationToken.ThrowIfCancellationRequested();
			if (this._compilation.Options.ConcurrentBuild)
			{
				Task task = this.CompileNamedTypeAsync(symbol);
				this._compilerTasks.Push(task);
			}
			else
			{
				this.CompileNamedType(symbol);
			}
			return null;
		}

		// Token: 0x06001744 RID: 5956 RVA: 0x0007EDDA File Offset: 0x0007CFDA
		private Task CompileNamedTypeAsync(NamedTypeSymbol symbol)
		{
			return Task.Run(UICultureUtilities.WithCurrentUICulture(delegate
			{
				try
				{
					this.CompileNamedType(symbol);
				}
				catch (Exception ex) when (FatalError.ReportAndPropagateUnlessCanceled(ex))
				{
					throw ExceptionUtilities.Unreachable;
				}
			}), this._cancellationToken);
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x0007EE0C File Offset: 0x0007D00C
		private void CompileNamedType(NamedTypeSymbol containingType)
		{
			TypeCompilationState typeCompilationState = new TypeCompilationState(containingType, this._compilation, this._moduleBeingBuiltOpt);
			this._cancellationToken.ThrowIfCancellationRequested();
			SynthesizedInstanceConstructor synthesizedInstanceConstructor = null;
			SynthesizedInteractiveInitializerMethod synthesizedInteractiveInitializerMethod = null;
			SynthesizedEntryPointSymbol synthesizedEntryPointSymbol = null;
			int num = -1;
			if (containingType.IsScriptClass)
			{
				synthesizedInstanceConstructor = containingType.GetScriptConstructor();
				synthesizedInteractiveInitializerMethod = containingType.GetScriptInitializer();
				synthesizedEntryPointSymbol = containingType.GetScriptEntryPoint();
			}
			SynthesizedSubmissionFields synthesizedSubmissionFields = (containingType.IsSubmissionClass ? new SynthesizedSubmissionFields(this._compilation, containingType) : null);
			Binder.ProcessedFieldInitializers processedFieldInitializers = default(Binder.ProcessedFieldInitializers);
			Binder.ProcessedFieldInitializers processedFieldInitializers2 = default(Binder.ProcessedFieldInitializers);
			SourceMemberContainerTypeSymbol sourceMemberContainerTypeSymbol = containingType as SourceMemberContainerTypeSymbol;
			if (sourceMemberContainerTypeSymbol != null)
			{
				this._cancellationToken.ThrowIfCancellationRequested();
				Binder.BindFieldInitializers(this._compilation, synthesizedInteractiveInitializerMethod, sourceMemberContainerTypeSymbol.StaticInitializers, this._diagnostics, ref processedFieldInitializers);
				this._cancellationToken.ThrowIfCancellationRequested();
				Binder.BindFieldInitializers(this._compilation, synthesizedInteractiveInitializerMethod, sourceMemberContainerTypeSymbol.InstanceInitializers, this._diagnostics, ref processedFieldInitializers2);
				if (typeCompilationState.Emitting)
				{
					this.CompileSynthesizedExplicitImplementations(sourceMemberContainerTypeSymbol, typeCompilationState);
				}
			}
			bool flag = false;
			ImmutableArray<Symbol> members = containingType.GetMembers();
			for (int i = 0; i < members.Length; i++)
			{
				Symbol symbol = members[i];
				if (MethodCompiler.PassesFilter(this._filterOpt, symbol))
				{
					SymbolKind kind = symbol.Kind;
					switch (kind)
					{
					case SymbolKind.Event:
					{
						SourceEventSymbol sourceEventSymbol = symbol as SourceEventSymbol;
						if (sourceEventSymbol != null && sourceEventSymbol.HasAssociatedField && !sourceEventSymbol.IsAbstract && typeCompilationState.Emitting)
						{
							this.CompileFieldLikeEventAccessor(sourceEventSymbol, true);
							this.CompileFieldLikeEventAccessor(sourceEventSymbol, false);
						}
						break;
					}
					case SymbolKind.Field:
					{
						FieldSymbol fieldSymbol = (FieldSymbol)symbol;
						if (!(symbol is TupleErrorFieldSymbol))
						{
							if (fieldSymbol.IsConst)
							{
								ConstantValue constantValue = fieldSymbol.GetConstantValue(ConstantFieldsInProgress.Empty, false);
								this.SetGlobalErrorIfTrue(constantValue == null || constantValue.IsBad);
							}
							if (fieldSymbol.IsFixedSizeBuffer && typeCompilationState.Emitting)
							{
								fieldSymbol.FixedImplementationType(typeCompilationState.ModuleBuilderOpt);
							}
						}
						break;
					}
					case SymbolKind.Label:
					case SymbolKind.Local:
					case SymbolKind.NetModule:
						break;
					case SymbolKind.Method:
					{
						MethodSymbol methodSymbol = (MethodSymbol)symbol;
						if (methodSymbol.IsScriptConstructor)
						{
							num = i;
						}
						else if (methodSymbol != synthesizedEntryPointSymbol && !MethodCompiler.IsFieldLikeEventAccessor(methodSymbol))
						{
							if (methodSymbol.IsPartialDefinition())
							{
								methodSymbol = methodSymbol.PartialImplementationPart;
								if (methodSymbol == null)
								{
									break;
								}
							}
							Binder.ProcessedFieldInitializers processedFieldInitializers3 = ((methodSymbol.MethodKind == MethodKind.Constructor || methodSymbol.IsScriptInitializer) ? processedFieldInitializers2 : ((methodSymbol.MethodKind == MethodKind.StaticConstructor) ? processedFieldInitializers : default(Binder.ProcessedFieldInitializers)));
							this.CompileMethod(methodSymbol, i, ref processedFieldInitializers3, synthesizedSubmissionFields, typeCompilationState);
							if (methodSymbol.MethodKind == MethodKind.StaticConstructor)
							{
								flag = true;
							}
						}
						break;
					}
					case SymbolKind.NamedType:
						symbol.Accept<TypeCompilationState, object>(this, typeCompilationState);
						break;
					default:
						if (kind == SymbolKind.Property)
						{
							SourcePropertySymbolBase sourcePropertySymbolBase = symbol as SourcePropertySymbolBase;
							if (sourcePropertySymbolBase != null && sourcePropertySymbolBase.IsSealed && typeCompilationState.Emitting)
							{
								this.CompileSynthesizedSealedAccessors(sourcePropertySymbolBase, typeCompilationState);
							}
						}
						break;
					}
				}
			}
			if (AnonymousTypeManager.IsAnonymousTypeTemplate(containingType))
			{
				Binder.ProcessedFieldInitializers processedFieldInitializers4 = default(Binder.ProcessedFieldInitializers);
				foreach (MethodSymbol methodSymbol2 in AnonymousTypeManager.GetAnonymousTypeHiddenMethods(containingType))
				{
					this.CompileMethod(methodSymbol2, -1, ref processedFieldInitializers4, synthesizedSubmissionFields, typeCompilationState);
				}
			}
			if (this._moduleBeingBuiltOpt != null && !flag && !processedFieldInitializers.BoundInitializers.IsDefaultOrEmpty)
			{
				MethodSymbol methodSymbol3 = new SynthesizedStaticConstructor(sourceMemberContainerTypeSymbol);
				if (MethodCompiler.PassesFilter(this._filterOpt, methodSymbol3))
				{
					this.CompileMethod(methodSymbol3, -1, ref processedFieldInitializers, synthesizedSubmissionFields, typeCompilationState);
					if (this._moduleBeingBuiltOpt.GetMethodBody(methodSymbol3) != null)
					{
						this._moduleBeingBuiltOpt.AddSynthesizedDefinition(sourceMemberContainerTypeSymbol, methodSymbol3.GetCciAdapter());
					}
				}
			}
			bool flag2 = !flag && processedFieldInitializers.BoundInitializers.IsDefaultOrEmpty && this._compilation.LanguageVersion >= MessageID.IDS_FeatureNullableReferenceTypes.RequiredVersion();
			if (flag2)
			{
				bool flag3;
				if (containingType != null && !containingType.IsImplicitlyDeclared)
				{
					TypeKind typeKind = containingType.TypeKind;
					if (typeKind == TypeKind.Class || typeKind == TypeKind.Interface || typeKind == TypeKind.Struct)
					{
						flag3 = true;
						goto IL_03F7;
					}
				}
				flag3 = false;
				IL_03F7:
				flag2 = flag3;
			}
			if (flag2)
			{
				NullableWalker.VariableState variableState;
				NullableWalker.AnalyzeIfNeeded(this._compilation, new SynthesizedStaticConstructor(containingType), MethodCompiler.GetSynthesizedEmptyBody(containingType), this._diagnostics.DiagnosticBag, true, null, false, out variableState);
			}
			if (synthesizedInstanceConstructor != null && typeCompilationState.Emitting)
			{
				Binder.ProcessedFieldInitializers processedFieldInitializers5 = new Binder.ProcessedFieldInitializers
				{
					BoundInitializers = ImmutableArray<BoundInitializer>.Empty
				};
				this.CompileMethod(synthesizedInstanceConstructor, num, ref processedFieldInitializers5, synthesizedSubmissionFields, typeCompilationState);
				if (synthesizedSubmissionFields != null)
				{
					synthesizedSubmissionFields.AddToType(containingType, typeCompilationState.ModuleBuilderOpt);
				}
			}
			if (this._moduleBeingBuiltOpt != null)
			{
				this.CompileSynthesizedMethods(typeCompilationState);
			}
			typeCompilationState.Free();
		}

		// Token: 0x06001746 RID: 5958 RVA: 0x0007F2A0 File Offset: 0x0007D4A0
		private void CompileSynthesizedMethods(PrivateImplementationDetails privateImplClass, BindingDiagnosticBag diagnostics)
		{
			TypeCompilationState typeCompilationState = new TypeCompilationState(null, this._compilation, this._moduleBeingBuiltOpt);
			foreach (IMethodDefinition methodDefinition in privateImplClass.GetMethods(new EmitContext(this._moduleBeingBuiltOpt, null, diagnostics.DiagnosticBag, false, true)))
			{
				((MethodSymbol)methodDefinition.GetInternalSymbol()).GenerateMethodBody(typeCompilationState, diagnostics);
			}
			this.CompileSynthesizedMethods(typeCompilationState);
			typeCompilationState.Free();
		}

		// Token: 0x06001747 RID: 5959 RVA: 0x0007F32C File Offset: 0x0007D52C
		private void CompileSynthesizedMethods(ImmutableArray<NamedTypeSymbol> additionalTypes, BindingDiagnosticBag diagnostics)
		{
			foreach (NamedTypeSymbol namedTypeSymbol in additionalTypes)
			{
				TypeCompilationState typeCompilationState = new TypeCompilationState(namedTypeSymbol, this._compilation, this._moduleBeingBuiltOpt);
				foreach (MethodSymbol methodSymbol in namedTypeSymbol.GetMethodsToEmit())
				{
					methodSymbol.GenerateMethodBody(typeCompilationState, diagnostics);
				}
				if (!diagnostics.HasAnyErrors())
				{
					this.CompileSynthesizedMethods(typeCompilationState);
				}
				typeCompilationState.Free();
			}
		}

		// Token: 0x06001748 RID: 5960 RVA: 0x0007F3BC File Offset: 0x0007D5BC
		private void CompileSynthesizedMethods(TypeCompilationState compilationState)
		{
			ArrayBuilder<TypeCompilationState.MethodWithBody> synthesizedMethods = compilationState.SynthesizedMethods;
			if (synthesizedMethods == null)
			{
				return;
			}
			ImportChain currentImportChain = compilationState.CurrentImportChain;
			try
			{
				foreach (TypeCompilationState.MethodWithBody methodWithBody in synthesizedMethods)
				{
					ImportChain importChain = methodWithBody.ImportChain;
					compilationState.CurrentImportChain = importChain;
					BindingDiagnosticBag instance = BindingDiagnosticBag.GetInstance(this._diagnostics);
					MethodSymbol method = methodWithBody.Method;
					SynthesizedClosureMethod synthesizedClosureMethod = method as SynthesizedClosureMethod;
					VariableSlotAllocator variableSlotAllocator = ((synthesizedClosureMethod != null) ? this._moduleBeingBuiltOpt.TryCreateVariableSlotAllocator(synthesizedClosureMethod, synthesizedClosureMethod.TopLevelMethod, instance.DiagnosticBag) : this._moduleBeingBuiltOpt.TryCreateVariableSlotAllocator(method, method, instance.DiagnosticBag));
					MethodBody methodBody = null;
					try
					{
						IteratorStateMachine iteratorStateMachine;
						BoundStatement boundStatement = IteratorRewriter.Rewrite(methodWithBody.Body, method, -1, variableSlotAllocator, compilationState, instance, out iteratorStateMachine);
						StateMachineTypeSymbol stateMachineTypeSymbol = iteratorStateMachine;
						if (!boundStatement.HasErrors)
						{
							AsyncStateMachine asyncStateMachine;
							boundStatement = AsyncRewriter.Rewrite(boundStatement, method, -1, variableSlotAllocator, compilationState, instance, out asyncStateMachine);
							stateMachineTypeSymbol = stateMachineTypeSymbol ?? asyncStateMachine;
						}
						if (this._emitMethodBodies && !instance.HasAnyErrors() && !this._globalHasErrors)
						{
							methodBody = MethodCompiler.GenerateMethodBody(this._moduleBeingBuiltOpt, method, -1, boundStatement, ImmutableArray<LambdaDebugInfo>.Empty, ImmutableArray<ClosureDebugInfo>.Empty, stateMachineTypeSymbol, variableSlotAllocator, instance, this._debugDocumentProvider, method.GenerateDebugInfo ? importChain : null, this._emittingPdb, this._emitTestCoverageData, ImmutableArray<SourceSpan>.Empty, this._entryPointOpt);
						}
					}
					catch (BoundTreeVisitor.CancelledByStackGuardException ex)
					{
						ex.AddAnError(this._diagnostics);
					}
					this._diagnostics.AddRange(instance, false);
					instance.Free();
					if (this._emitMethodBodies)
					{
						if (methodBody == null)
						{
							break;
						}
						this._moduleBeingBuiltOpt.SetMethodBody(method, methodBody);
					}
				}
			}
			finally
			{
				compilationState.CurrentImportChain = currentImportChain;
			}
		}

		// Token: 0x06001749 RID: 5961 RVA: 0x0007F58C File Offset: 0x0007D78C
		private static bool IsFieldLikeEventAccessor(MethodSymbol method)
		{
			Symbol associatedSymbol = method.AssociatedSymbol;
			return associatedSymbol != null && associatedSymbol.Kind == SymbolKind.Event && ((EventSymbol)associatedSymbol).HasAssociatedField;
		}

		// Token: 0x0600174A RID: 5962 RVA: 0x0007F5BC File Offset: 0x0007D7BC
		private void CompileSynthesizedExplicitImplementations(SourceMemberContainerTypeSymbol sourceTypeSymbol, TypeCompilationState compilationState)
		{
			if (!this._globalHasErrors)
			{
				BindingDiagnosticBag instance = BindingDiagnosticBag.GetInstance(this._diagnostics);
				foreach (SynthesizedExplicitImplementationForwardingMethod synthesizedExplicitImplementationForwardingMethod in sourceTypeSymbol.GetSynthesizedExplicitImplementations(this._cancellationToken))
				{
					synthesizedExplicitImplementationForwardingMethod.GenerateMethodBody(compilationState, instance);
					instance.DiagnosticBag.Clear();
					this._moduleBeingBuiltOpt.AddSynthesizedDefinition(sourceTypeSymbol, synthesizedExplicitImplementationForwardingMethod.GetCciAdapter());
				}
				this._diagnostics.AddRangeAndFree(instance);
			}
		}

		// Token: 0x0600174B RID: 5963 RVA: 0x0007F638 File Offset: 0x0007D838
		private void CompileSynthesizedSealedAccessors(SourcePropertySymbolBase sourceProperty, TypeCompilationState compilationState)
		{
			SynthesizedSealedPropertyAccessor synthesizedSealedAccessorOpt = sourceProperty.SynthesizedSealedAccessorOpt;
			if (synthesizedSealedAccessorOpt != null && !this._globalHasErrors)
			{
				BindingDiagnosticBag instance = BindingDiagnosticBag.GetInstance(this._diagnostics);
				synthesizedSealedAccessorOpt.GenerateMethodBody(compilationState, instance);
				this._diagnostics.AddDependencies(instance, false);
				instance.Free();
				this._moduleBeingBuiltOpt.AddSynthesizedDefinition(sourceProperty.ContainingType, synthesizedSealedAccessorOpt.GetCciAdapter());
			}
		}

		// Token: 0x0600174C RID: 5964 RVA: 0x0007F698 File Offset: 0x0007D898
		private void CompileFieldLikeEventAccessor(SourceEventSymbol eventSymbol, bool isAddMethod)
		{
			MethodSymbol methodSymbol = (isAddMethod ? eventSymbol.AddMethod : eventSymbol.RemoveMethod);
			BindingDiagnosticBag instance = BindingDiagnosticBag.GetInstance(this._diagnostics);
			try
			{
				BoundBlock boundBlock = MethodBodySynthesizer.ConstructFieldLikeEventAccessorBody(eventSymbol, isAddMethod, this._compilation, instance);
				bool flag = instance.HasAnyErrors();
				this.SetGlobalErrorIfTrue(flag);
				if (!flag && !this._hasDeclarationErrors && this._emitMethodBodies)
				{
					MethodBody methodBody = MethodCompiler.GenerateMethodBody(this._moduleBeingBuiltOpt, methodSymbol, -1, boundBlock, ImmutableArray<LambdaDebugInfo>.Empty, ImmutableArray<ClosureDebugInfo>.Empty, null, null, instance, this._debugDocumentProvider, null, false, this._emitTestCoverageData, ImmutableArray<SourceSpan>.Empty, null);
					this._moduleBeingBuiltOpt.SetMethodBody(methodSymbol, methodBody);
				}
			}
			finally
			{
				this._diagnostics.AddRange(instance, false);
				instance.Free();
			}
		}

		// Token: 0x0600174D RID: 5965 RVA: 0x0007F758 File Offset: 0x0007D958
		public override object VisitMethod(MethodSymbol symbol, TypeCompilationState arg)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x0600174E RID: 5966 RVA: 0x0007F75F File Offset: 0x0007D95F
		public override object VisitProperty(PropertySymbol symbol, TypeCompilationState argument)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x0600174F RID: 5967 RVA: 0x0007F766 File Offset: 0x0007D966
		public override object VisitEvent(EventSymbol symbol, TypeCompilationState argument)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06001750 RID: 5968 RVA: 0x0007F76D File Offset: 0x0007D96D
		public override object VisitField(FieldSymbol symbol, TypeCompilationState argument)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06001751 RID: 5969 RVA: 0x0007F774 File Offset: 0x0007D974
		private void CompileMethod(MethodSymbol methodSymbol, int methodOrdinal, ref Binder.ProcessedFieldInitializers processedInitializers, SynthesizedSubmissionFields previousSubmissionFields, TypeCompilationState compilationState)
		{
			this._cancellationToken.ThrowIfCancellationRequested();
			SourceMemberMethodSymbol sourceMemberMethodSymbol = methodSymbol as SourceMemberMethodSymbol;
			if (!methodSymbol.IsAbstract)
			{
				NamedTypeSymbol containingType = methodSymbol.ContainingType;
				if (containingType == null || !containingType.IsDelegateType())
				{
					if (this._moduleBeingBuiltOpt == null && sourceMemberMethodSymbol != null)
					{
						ImmutableArray<Diagnostic> diagnostics = sourceMemberMethodSymbol.Diagnostics;
						if (!diagnostics.IsDefault)
						{
							this._diagnostics.AddRange<Diagnostic>(diagnostics);
							return;
						}
					}
					ImportChain currentImportChain = compilationState.CurrentImportChain;
					BindingDiagnosticBag instance = BindingDiagnosticBag.GetInstance(this._diagnostics);
					try
					{
						if (methodSymbol.SynthesizesLoweredBoundBody)
						{
							if (this._moduleBeingBuiltOpt != null)
							{
								methodSymbol.GenerateMethodBody(compilationState, instance);
								this._diagnostics.AddRange(instance, false);
							}
						}
						else if (!methodSymbol.IsDefaultValueTypeConstructor())
						{
							bool flag = false;
							bool flag2 = false;
							BoundStatementList boundStatementList = null;
							MethodBodySemanticModel.InitialState forSemanticModel = default(MethodBodySemanticModel.InitialState);
							ImportChain importChain = null;
							bool flag3 = false;
							BoundBlock boundBlock;
							if (methodSymbol.IsScriptConstructor)
							{
								boundBlock = new BoundBlock(methodSymbol.GetNonNullSyntaxNode(), ImmutableArray<LocalSymbol>.Empty, ImmutableArray<BoundStatement>.Empty, false)
								{
									WasCompilerGenerated = true
								};
							}
							else if (methodSymbol.IsScriptInitializer)
							{
								BoundTypeOrInstanceInitializers boundTypeOrInstanceInitializers = InitializerRewriter.RewriteScriptInitializer(processedInitializers.BoundInitializers, (SynthesizedInteractiveInitializerMethod)methodSymbol, out flag3);
								boundBlock = BoundBlock.SynthesizedNoLocals(boundTypeOrInstanceInitializers.Syntax, boundTypeOrInstanceInitializers.Statements);
								NullableWalker.AnalyzeIfNeeded(this._compilation, methodSymbol, boundTypeOrInstanceInitializers, instance.DiagnosticBag, false, null, true, out processedInitializers.AfterInitializersState);
								DiagnosticBag instance2 = DiagnosticBag.GetInstance();
								DefiniteAssignmentPass.Analyze(this._compilation, methodSymbol, boundTypeOrInstanceInitializers, instance2, false);
								DiagnosticsPass.IssueDiagnostics(this._compilation, boundTypeOrInstanceInitializers, BindingDiagnosticBag.Discarded, methodSymbol);
								instance2.Free();
							}
							else
							{
								bool flag4 = methodSymbol.IncludeFieldInitializersInBody();
								flag = flag4 && !processedInitializers.BoundInitializers.IsDefaultOrEmpty;
								if (flag && processedInitializers.LoweredInitializers == null)
								{
									boundStatementList = InitializerRewriter.RewriteConstructor(processedInitializers.BoundInitializers, methodSymbol);
									processedInitializers.HasErrors = processedInitializers.HasErrors || boundStatementList.HasAnyErrors;
								}
								if (flag4 && processedInitializers.AfterInitializersState == null)
								{
									NullableWalker.AnalyzeIfNeeded(this._compilation, methodSymbol, boundStatementList ?? MethodCompiler.GetSynthesizedEmptyBody(methodSymbol), instance.DiagnosticBag, false, null, true, out processedInitializers.AfterInitializersState);
								}
								boundBlock = MethodCompiler.BindMethodBody(methodSymbol, compilationState, instance, processedInitializers.AfterInitializersState, out importChain, out flag2, out forSemanticModel);
								if (instance.HasAnyErrors() && boundBlock != null)
								{
									boundBlock = (BoundBlock)boundBlock.WithHasErrors();
								}
								if (flag && processedInitializers.LoweredInitializers == null)
								{
									if (boundBlock != null && ((methodSymbol.ContainingType.IsStructType() && !methodSymbol.IsImplicitConstructor) || methodSymbol is SynthesizedRecordConstructor || this._emitTestCoverageData))
									{
										if (this._emitTestCoverageData && methodSymbol.IsImplicitConstructor)
										{
											DefiniteAssignmentPass.Analyze(this._compilation, methodSymbol, boundStatementList, instance.DiagnosticBag, false);
										}
										boundBlock = boundBlock.Update(boundBlock.Locals, boundBlock.LocalFunctions, boundBlock.Statements.Insert(0, boundStatementList));
										flag = false;
										boundStatementList = null;
									}
									else
									{
										DefiniteAssignmentPass.Analyze(this._compilation, methodSymbol, boundStatementList, instance.DiagnosticBag, false);
										DiagnosticsPass.IssueDiagnostics(this._compilation, boundStatementList, instance, methodSymbol);
									}
								}
							}
							importChain = importChain ?? processedInitializers.FirstImportChain;
							compilationState.CurrentImportChain = importChain;
							if (boundBlock != null)
							{
								DiagnosticsPass.IssueDiagnostics(this._compilation, boundBlock, instance, methodSymbol);
							}
							BoundBlock boundBlock2 = null;
							if (boundBlock != null)
							{
								boundBlock2 = FlowAnalysisPass.Rewrite(methodSymbol, boundBlock, instance.DiagnosticBag, flag3, flag2);
							}
							bool flag5 = this._hasDeclarationErrors || instance.HasAnyErrors() || processedInitializers.HasErrors;
							this.SetGlobalErrorIfTrue(flag5);
							bool flag6 = false;
							ImmutableBindingDiagnostic<AssemblySymbol> immutableBindingDiagnostic = instance.ToReadOnly();
							if (sourceMemberMethodSymbol != null)
							{
								immutableBindingDiagnostic = new ImmutableBindingDiagnostic<AssemblySymbol>(sourceMemberMethodSymbol.SetDiagnostics(immutableBindingDiagnostic.Diagnostics, out flag6), immutableBindingDiagnostic.Dependencies);
							}
							if (flag6 && !methodSymbol.IsImplicitlyDeclared && this._compilation.EventQueue != null)
							{
								SyntaxTreeSemanticModel semanticModelWithCachedBoundNodes = null;
								if (boundBlock != null)
								{
									CSharpSyntaxNode syntax = forSemanticModel.Syntax;
									if (syntax != null)
									{
										CachingSemanticModelProvider cachingSemanticModelProvider = this._compilation.SemanticModelProvider as CachingSemanticModelProvider;
										if (cachingSemanticModelProvider != null)
										{
											SyntaxNode syntax2 = boundBlock.Syntax;
											semanticModelWithCachedBoundNodes = (SyntaxTreeSemanticModel)cachingSemanticModelProvider.GetSemanticModel(syntax2.SyntaxTree, this._compilation, false);
											semanticModelWithCachedBoundNodes.GetOrAddModel(syntax, (CSharpSyntaxNode rootSyntax) => MethodBodySemanticModel.Create(semanticModelWithCachedBoundNodes, methodSymbol, forSemanticModel));
										}
									}
								}
								this._compilation.EventQueue.TryEnqueue(new SymbolDeclaredCompilationEvent(this._compilation, methodSymbol.GetPublicSymbol(), semanticModelWithCachedBoundNodes));
							}
							if (this._moduleBeingBuiltOpt == null || flag5)
							{
								this._diagnostics.AddRange(immutableBindingDiagnostic, false);
							}
							else
							{
								ImmutableArray<SourceSpan> empty = ImmutableArray<SourceSpan>.Empty;
								bool flag7 = boundBlock2 != null;
								VariableSlotAllocator variableSlotAllocator = null;
								StateMachineTypeSymbol stateMachineTypeSymbol = null;
								ArrayBuilder<LambdaDebugInfo> instance3 = ArrayBuilder<LambdaDebugInfo>.GetInstance();
								ArrayBuilder<ClosureDebugInfo> instance4 = ArrayBuilder<ClosureDebugInfo>.GetInstance();
								try
								{
									BoundStatement boundStatement;
									if (flag7)
									{
										boundStatement = MethodCompiler.LowerBodyOrInitializer(methodSymbol, methodOrdinal, boundBlock2, previousSubmissionFields, compilationState, this._emitTestCoverageData, this._debugDocumentProvider, ref empty, instance, ref variableSlotAllocator, instance3, instance4, out stateMachineTypeSymbol);
									}
									else
									{
										boundStatement = null;
									}
									flag5 = flag5 || (flag7 && boundStatement.HasErrors) || instance.HasAnyErrors();
									this.SetGlobalErrorIfTrue(flag5);
									if (!flag5 && (flag7 || flag))
									{
										ImmutableArray<BoundStatement> immutableArray;
										if (methodSymbol.IsScriptConstructor)
										{
											immutableArray = MethodBodySynthesizer.ConstructScriptConstructorBody(boundStatement, methodSymbol, previousSubmissionFields, this._compilation);
										}
										else
										{
											immutableArray = ImmutableArray<BoundStatement>.Empty;
											if (boundStatementList != null)
											{
												StateMachineTypeSymbol stateMachineTypeSymbol2;
												BoundStatement boundStatement2 = MethodCompiler.LowerBodyOrInitializer(methodSymbol, methodOrdinal, boundStatementList, previousSubmissionFields, compilationState, this._emitTestCoverageData, this._debugDocumentProvider, ref empty, instance, ref variableSlotAllocator, instance3, instance4, out stateMachineTypeSymbol2);
												processedInitializers.LoweredInitializers = boundStatement2;
												flag5 = boundStatement2.HasAnyErrors || instance.HasAnyErrors();
												this.SetGlobalErrorIfTrue(flag5);
												if (flag5)
												{
													this._diagnostics.AddRange(instance, false);
													return;
												}
												processedInitializers.LoweredInitializers = (BoundStatementList)boundStatement2;
											}
											if (flag)
											{
												if (processedInitializers.LoweredInitializers.Kind == BoundKind.StatementList)
												{
													BoundStatementList boundStatementList2 = (BoundStatementList)processedInitializers.LoweredInitializers;
													immutableArray = immutableArray.Concat<BoundStatement>(boundStatementList2.Statements);
												}
												else
												{
													immutableArray = immutableArray.Add(processedInitializers.LoweredInitializers);
												}
											}
											if (flag7)
											{
												immutableArray = immutableArray.Concat<BoundStatement>(ImmutableArray.Create<BoundStatement>(boundStatement));
											}
										}
										if (this._emitMethodBodies)
										{
											SynthesizedStaticConstructor synthesizedStaticConstructor = methodSymbol as SynthesizedStaticConstructor;
											if (synthesizedStaticConstructor == null || synthesizedStaticConstructor.ShouldEmit(processedInitializers.BoundInitializers))
											{
												BoundStatementList boundStatementList3 = BoundStatementList.Synthesized(methodSymbol.GetNonNullSyntaxNode(), immutableArray);
												MethodBody methodBody = MethodCompiler.GenerateMethodBody(this._moduleBeingBuiltOpt, methodSymbol, methodOrdinal, boundStatementList3, instance3.ToImmutable(), instance4.ToImmutable(), stateMachineTypeSymbol, variableSlotAllocator, instance, this._debugDocumentProvider, importChain, this._emittingPdb, this._emitTestCoverageData, empty, null);
												this._moduleBeingBuiltOpt.SetMethodBody(methodSymbol.PartialDefinitionPart ?? methodSymbol, methodBody);
											}
										}
									}
									this._diagnostics.AddRange(instance, false);
								}
								finally
								{
									instance3.Free();
									instance4.Free();
								}
							}
						}
					}
					finally
					{
						instance.Free();
						compilationState.CurrentImportChain = currentImportChain;
					}
					return;
				}
			}
			if (sourceMemberMethodSymbol != null)
			{
				bool flag8;
				sourceMemberMethodSymbol.SetDiagnostics(ImmutableArray<Diagnostic>.Empty, out flag8);
				if (flag8 && !methodSymbol.IsImplicitlyDeclared && this._compilation.EventQueue != null)
				{
					this._compilation.SymbolDeclaredEvent(methodSymbol);
				}
			}
		}

		// Token: 0x06001752 RID: 5970 RVA: 0x0007FF50 File Offset: 0x0007E150
		internal static BoundStatement LowerBodyOrInitializer(MethodSymbol method, int methodOrdinal, BoundStatement body, SynthesizedSubmissionFields previousSubmissionFields, TypeCompilationState compilationState, bool instrumentForDynamicAnalysis, DebugDocumentProvider debugDocumentProvider, ref ImmutableArray<SourceSpan> dynamicAnalysisSpans, BindingDiagnosticBag diagnostics, ref VariableSlotAllocator lazyVariableSlotAllocator, ArrayBuilder<LambdaDebugInfo> lambdaDebugInfoBuilder, ArrayBuilder<ClosureDebugInfo> closureDebugInfoBuilder, out StateMachineTypeSymbol stateMachineTypeOpt)
		{
			stateMachineTypeOpt = null;
			if (body.HasErrors)
			{
				return body;
			}
			BoundStatement boundStatement2;
			try
			{
				bool flag;
				bool flag2;
				bool flag3;
				BoundStatement boundStatement = LocalRewriter.Rewrite(method.DeclaringCompilation, method, methodOrdinal, method.ContainingType, body, compilationState, previousSubmissionFields, true, instrumentForDynamicAnalysis, ref dynamicAnalysisSpans, debugDocumentProvider, diagnostics, out flag, out flag2, out flag3);
				if (boundStatement.HasErrors)
				{
					boundStatement2 = boundStatement;
				}
				else
				{
					if (flag3)
					{
						boundStatement = AsyncExceptionHandlerRewriter.Rewrite(method, method.ContainingType, boundStatement, compilationState, diagnostics);
					}
					if (boundStatement.HasErrors)
					{
						boundStatement2 = boundStatement;
					}
					else
					{
						if (lazyVariableSlotAllocator == null)
						{
							lazyVariableSlotAllocator = compilationState.ModuleBuilderOpt.TryCreateVariableSlotAllocator(method, method, diagnostics.DiagnosticBag);
						}
						BoundStatement boundStatement3 = boundStatement;
						if (flag || flag2)
						{
							boundStatement3 = ClosureConversion.Rewrite(boundStatement, method.ContainingType, method.ThisParameter, method, methodOrdinal, null, lambdaDebugInfoBuilder, closureDebugInfoBuilder, lazyVariableSlotAllocator, compilationState, diagnostics, null);
						}
						if (boundStatement3.HasErrors)
						{
							boundStatement2 = boundStatement3;
						}
						else
						{
							IteratorStateMachine iteratorStateMachine;
							BoundStatement boundStatement4 = IteratorRewriter.Rewrite(boundStatement3, method, methodOrdinal, lazyVariableSlotAllocator, compilationState, diagnostics, out iteratorStateMachine);
							if (boundStatement4.HasErrors)
							{
								boundStatement2 = boundStatement4;
							}
							else
							{
								AsyncStateMachine asyncStateMachine;
								BoundStatement boundStatement5 = AsyncRewriter.Rewrite(boundStatement4, method, methodOrdinal, lazyVariableSlotAllocator, compilationState, diagnostics, out asyncStateMachine);
								stateMachineTypeOpt = iteratorStateMachine ?? asyncStateMachine;
								boundStatement2 = boundStatement5;
							}
						}
					}
				}
			}
			catch (BoundTreeVisitor.CancelledByStackGuardException ex)
			{
				ex.AddAnError(diagnostics);
				boundStatement2 = new BoundBadStatement(body.Syntax, ImmutableArray.Create<BoundNode>(body), true);
			}
			return boundStatement2;
		}

		// Token: 0x06001753 RID: 5971 RVA: 0x00080094 File Offset: 0x0007E294
		private static MethodBody GenerateMethodBody(PEModuleBuilder moduleBuilder, MethodSymbol method, int methodOrdinal, BoundStatement block, ImmutableArray<LambdaDebugInfo> lambdaDebugInfo, ImmutableArray<ClosureDebugInfo> closureDebugInfo, StateMachineTypeSymbol stateMachineTypeOpt, VariableSlotAllocator variableSlotAllocatorOpt, BindingDiagnosticBag diagnostics, DebugDocumentProvider debugDocumentProvider, ImportChain importChainOpt, bool emittingPdb, bool emitTestCoverageData, ImmutableArray<SourceSpan> dynamicAnalysisSpans, SynthesizedEntryPointSymbol.AsyncForwardEntryPoint entryPointOpt)
		{
			CSharpCompilation compilation = moduleBuilder.Compilation;
			LocalSlotManager localSlotManager = new LocalSlotManager(variableSlotAllocatorOpt);
			OptimizationLevel optimizationLevel = compilation.Options.OptimizationLevel;
			ILBuilder ilbuilder = new ILBuilder(moduleBuilder, localSlotManager, optimizationLevel, method.AreLocalsZeroed);
			BindingDiagnosticBag instance = BindingDiagnosticBag.GetInstance(true, diagnostics.AccumulatesDependencies);
			MethodBody methodBody;
			try
			{
				StateMachineMoveNextBodyDebugInfo stateMachineMoveNextBodyDebugInfo = null;
				CodeGenerator codeGenerator = new CodeGenerator(method, block, ilbuilder, moduleBuilder, instance.DiagnosticBag, optimizationLevel, emittingPdb);
				if (instance.HasAnyErrors())
				{
					methodBody = null;
				}
				else
				{
					SynthesizedStateMachineMethod synthesizedStateMachineMethod = method as SynthesizedStateMachineMethod;
					MethodSymbol methodSymbol;
					bool flag;
					if (synthesizedStateMachineMethod != null && method.Name == "MoveNext")
					{
						methodSymbol = synthesizedStateMachineMethod.StateMachineType.KickoffMethod;
						flag = methodSymbol.IsAsync;
						methodSymbol = methodSymbol.PartialDefinitionPart ?? methodSymbol;
					}
					else
					{
						methodSymbol = null;
						flag = false;
					}
					bool flag2;
					if (flag)
					{
						int num;
						ImmutableArray<int> immutableArray;
						ImmutableArray<int> immutableArray2;
						codeGenerator.Generate(out num, out immutableArray, out immutableArray2, out flag2);
						bool flag3 = entryPointOpt != null && entryPointOpt.UserMain.Equals(methodSymbol);
						stateMachineMoveNextBodyDebugInfo = new AsyncMoveNextBodyDebugInfo(methodSymbol.GetCciAdapter(), (methodSymbol.ReturnsVoid || flag3) ? num : (-1), immutableArray, immutableArray2);
					}
					else
					{
						codeGenerator.Generate(out flag2);
						if (methodSymbol != null)
						{
							stateMachineMoveNextBodyDebugInfo = new IteratorMoveNextBodyDebugInfo(methodSymbol.GetCciAdapter());
						}
					}
					ImmutableArray<StateMachineHoistedLocalScope> immutableArray3 = ((methodSymbol != null) ? ilbuilder.GetHoistedLocalScopes() : default(ImmutableArray<StateMachineHoistedLocalScope>));
					IImportScope importScope = ((importChainOpt != null) ? importChainOpt.Translate(moduleBuilder, instance.DiagnosticBag) : null);
					ImmutableArray<ILocalDefinition> immutableArray4 = ilbuilder.LocalSlotManager.LocalsInOrder();
					if (immutableArray4.Length > 65534)
					{
						instance.Add(ErrorCode.ERR_TooManyLocals, method.Locations.First<Location>());
					}
					if (instance.HasAnyErrors())
					{
						methodBody = null;
					}
					else
					{
						if (moduleBuilder.SaveTestData)
						{
							moduleBuilder.SetMethodTestData(method, ilbuilder.GetSnapshot());
						}
						ImmutableArray<EncHoistedLocalInfo> immutableArray5 = default(ImmutableArray<EncHoistedLocalInfo>);
						ImmutableArray<ITypeReference> immutableArray6 = default(ImmutableArray<ITypeReference>);
						if (optimizationLevel == OptimizationLevel.Debug && stateMachineTypeOpt != null)
						{
							MethodCompiler.GetStateMachineSlotDebugInfo(moduleBuilder, moduleBuilder.GetSynthesizedFields(stateMachineTypeOpt), variableSlotAllocatorOpt, instance, out immutableArray5, out immutableArray6);
						}
						DynamicAnalysisMethodBodyData dynamicAnalysisMethodBodyData = null;
						if (emitTestCoverageData)
						{
							dynamicAnalysisMethodBodyData = new DynamicAnalysisMethodBodyData(dynamicAnalysisSpans);
						}
						methodBody = new MethodBody(ilbuilder.RealizedIL, ilbuilder.MaxStack, (method.PartialDefinitionPart ?? method).GetCciAdapter(), ((variableSlotAllocatorOpt != null) ? variableSlotAllocatorOpt.MethodId : default(DebugId?)) ?? new DebugId(methodOrdinal, moduleBuilder.CurrentGenerationOrdinal), immutableArray4, ilbuilder.RealizedSequencePoints, debugDocumentProvider, ilbuilder.RealizedExceptionHandlers, ilbuilder.AreLocalsZeroed, flag2, ilbuilder.GetAllScopes(), ilbuilder.HasDynamicLocal, importScope, lambdaDebugInfo, closureDebugInfo, (stateMachineTypeOpt != null) ? stateMachineTypeOpt.Name : null, immutableArray3, immutableArray5, immutableArray6, stateMachineMoveNextBodyDebugInfo, dynamicAnalysisMethodBodyData);
					}
				}
			}
			finally
			{
				ilbuilder.FreeBasicBlocks();
				diagnostics.AddRange(instance, false);
				instance.Free();
			}
			return methodBody;
		}

		// Token: 0x06001754 RID: 5972 RVA: 0x00080350 File Offset: 0x0007E550
		private static void GetStateMachineSlotDebugInfo(PEModuleBuilder moduleBuilder, IEnumerable<IFieldDefinition> fieldDefs, VariableSlotAllocator variableSlotAllocatorOpt, BindingDiagnosticBag diagnostics, out ImmutableArray<EncHoistedLocalInfo> hoistedVariableSlots, out ImmutableArray<ITypeReference> awaiterSlots)
		{
			ArrayBuilder<EncHoistedLocalInfo> instance = ArrayBuilder<EncHoistedLocalInfo>.GetInstance();
			ArrayBuilder<ITypeReference> instance2 = ArrayBuilder<ITypeReference>.GetInstance();
			foreach (IFieldDefinition fieldDefinition in fieldDefs)
			{
				StateMachineFieldSymbol stateMachineFieldSymbol = (StateMachineFieldSymbol)fieldDefinition;
				int i = stateMachineFieldSymbol.SlotIndex;
				if (stateMachineFieldSymbol.SlotDebugInfo.SynthesizedKind == SynthesizedLocalKind.AwaiterField)
				{
					while (i >= instance2.Count)
					{
						instance2.Add(null);
					}
					instance2[i] = moduleBuilder.EncTranslateLocalVariableType(stateMachineFieldSymbol.Type, diagnostics.DiagnosticBag);
				}
				else if (!stateMachineFieldSymbol.SlotDebugInfo.Id.IsNone)
				{
					while (i >= instance.Count)
					{
						instance.Add(new EncHoistedLocalInfo(true));
					}
					instance[i] = new EncHoistedLocalInfo(stateMachineFieldSymbol.SlotDebugInfo, moduleBuilder.EncTranslateLocalVariableType(stateMachineFieldSymbol.Type, diagnostics.DiagnosticBag));
				}
			}
			if (variableSlotAllocatorOpt != null)
			{
				int previousAwaiterSlotCount = variableSlotAllocatorOpt.PreviousAwaiterSlotCount;
				while (instance2.Count < previousAwaiterSlotCount)
				{
					instance2.Add(null);
				}
				int previousHoistedLocalSlotCount = variableSlotAllocatorOpt.PreviousHoistedLocalSlotCount;
				while (instance.Count < previousHoistedLocalSlotCount)
				{
					instance.Add(new EncHoistedLocalInfo(true));
				}
			}
			hoistedVariableSlots = instance.ToImmutableAndFree();
			awaiterSlots = instance2.ToImmutableAndFree();
		}

		// Token: 0x06001755 RID: 5973 RVA: 0x0008049C File Offset: 0x0007E69C
		internal static BoundBlock BindMethodBody(MethodSymbol method, TypeCompilationState compilationState, BindingDiagnosticBag diagnostics)
		{
			ImportChain importChain;
			bool flag;
			MethodBodySemanticModel.InitialState initialState;
			return MethodCompiler.BindMethodBody(method, compilationState, diagnostics, null, out importChain, out flag, out initialState);
		}

		// Token: 0x06001756 RID: 5974 RVA: 0x000804B8 File Offset: 0x0007E6B8
		private static BoundBlock BindMethodBody(MethodSymbol method, TypeCompilationState compilationState, BindingDiagnosticBag diagnostics, NullableWalker.VariableState nullableInitialState, out ImportChain importChain, out bool originalBodyNested, out MethodBodySemanticModel.InitialState forSemanticModel)
		{
			originalBodyNested = false;
			importChain = null;
			forSemanticModel = default(MethodBodySemanticModel.InitialState);
			SynthesizedRecordConstructor synthesizedRecordConstructor = method as SynthesizedRecordConstructor;
			BoundBlock boundBlock;
			if (synthesizedRecordConstructor != null && method.ContainingType.IsRecordStruct)
			{
				boundBlock = BoundBlock.SynthesizedNoLocals(synthesizedRecordConstructor.GetSyntax(), Array.Empty<BoundStatement>());
			}
			else
			{
				SourceMemberMethodSymbol sourceMemberMethodSymbol = method as SourceMemberMethodSymbol;
				if (sourceMemberMethodSymbol != null)
				{
					CSharpSyntaxNode syntaxNode = sourceMemberMethodSymbol.SyntaxNode;
					if (method.MethodKind == MethodKind.StaticConstructor)
					{
						ConstructorDeclarationSyntax constructorDeclarationSyntax = syntaxNode as ConstructorDeclarationSyntax;
						if (constructorDeclarationSyntax != null && constructorDeclarationSyntax.Initializer != null)
						{
							diagnostics.Add(ErrorCode.ERR_StaticConstructorWithExplicitConstructorCall, constructorDeclarationSyntax.Initializer.ThisOrBaseKeyword.GetLocation(), new object[] { constructorDeclarationSyntax.Identifier.ValueText });
						}
					}
					ExecutableCodeBinder executableCodeBinder = sourceMemberMethodSymbol.TryGetBodyBinder(null, false);
					if (sourceMemberMethodSymbol.IsExtern || sourceMemberMethodSymbol.IsDefaultValueTypeConstructor())
					{
						return null;
					}
					if (executableCodeBinder != null)
					{
						importChain = executableCodeBinder.ImportChain;
						BoundNode boundNode = executableCodeBinder.BindMethodBody(syntaxNode, diagnostics);
						BoundNode boundNode2 = boundNode;
						NullableWalker.SnapshotManager snapshotManager = null;
						ImmutableDictionary<Symbol, Symbol> immutableDictionary = null;
						CSharpCompilation compilation = executableCodeBinder.Compilation;
						bool flag = compilation.LanguageVersion >= MessageID.IDS_FeatureNullableReferenceTypes.RequiredVersion();
						if (compilation.IsNullableAnalysisEnabledIn(method))
						{
							boundNode2 = NullableWalker.AnalyzeAndRewrite(compilation, method, boundNode, executableCodeBinder, nullableInitialState, flag ? diagnostics.DiagnosticBag : new DiagnosticBag(), true, out snapshotManager, ref immutableDictionary);
						}
						else
						{
							NullableWalker.VariableState variableState;
							NullableWalker.AnalyzeIfNeeded(compilation, method, boundNode, diagnostics.DiagnosticBag, true, nullableInitialState, false, out variableState);
						}
						forSemanticModel = new MethodBodySemanticModel.InitialState(syntaxNode, boundNode2, executableCodeBinder, snapshotManager, immutableDictionary);
						BoundKind kind = boundNode.Kind;
						if (kind != BoundKind.Block)
						{
							if (kind != BoundKind.NonConstructorMethodBody)
							{
								if (kind != BoundKind.ConstructorMethodBody)
								{
									throw ExceptionUtilities.UnexpectedValue(boundNode.Kind);
								}
								BoundConstructorMethodBody boundConstructorMethodBody = (BoundConstructorMethodBody)boundNode;
								boundBlock = boundConstructorMethodBody.BlockBody ?? boundConstructorMethodBody.ExpressionBody;
								if (boundConstructorMethodBody.Initializer != null)
								{
									MethodCompiler.ReportCtorInitializerCycles(method, boundConstructorMethodBody.Initializer.Expression, compilationState, diagnostics);
									if (boundBlock == null)
									{
										boundBlock = new BoundBlock(boundConstructorMethodBody.Syntax, boundConstructorMethodBody.Locals, ImmutableArray.Create<BoundStatement>(boundConstructorMethodBody.Initializer), false);
									}
									else
									{
										boundBlock = new BoundBlock(boundConstructorMethodBody.Syntax, boundConstructorMethodBody.Locals, ImmutableArray.Create<BoundStatement>(boundConstructorMethodBody.Initializer, boundBlock), false);
										originalBodyNested = true;
									}
									return boundBlock;
								}
							}
							else
							{
								BoundNonConstructorMethodBody boundNonConstructorMethodBody = (BoundNonConstructorMethodBody)boundNode;
								boundBlock = boundNonConstructorMethodBody.BlockBody ?? boundNonConstructorMethodBody.ExpressionBody;
							}
						}
						else
						{
							boundBlock = (BoundBlock)boundNode;
						}
					}
					else
					{
						SourcePropertySymbolBase sourcePropertySymbolBase = sourceMemberMethodSymbol.AssociatedSymbol as SourcePropertySymbolBase;
						if (sourcePropertySymbolBase != null && sourcePropertySymbolBase.IsAutoPropertyWithGetAccessor)
						{
							return MethodBodySynthesizer.ConstructAutoPropertyAccessorBody(sourceMemberMethodSymbol);
						}
						return null;
					}
				}
				else
				{
					SynthesizedInstanceConstructor synthesizedInstanceConstructor = method as SynthesizedInstanceConstructor;
					if (synthesizedInstanceConstructor != null)
					{
						CSharpSyntaxNode nonNullSyntaxNode = synthesizedInstanceConstructor.GetNonNullSyntaxNode();
						SyntheticBoundNodeFactory syntheticBoundNodeFactory = new SyntheticBoundNodeFactory(synthesizedInstanceConstructor, nonNullSyntaxNode, compilationState, diagnostics);
						ArrayBuilder<BoundStatement> instance = ArrayBuilder<BoundStatement>.GetInstance();
						synthesizedInstanceConstructor.GenerateMethodBodyStatements(syntheticBoundNodeFactory, instance, diagnostics);
						boundBlock = BoundBlock.SynthesizedNoLocals(nonNullSyntaxNode, instance.ToImmutableAndFree());
					}
					else
					{
						boundBlock = null;
					}
				}
			}
			if (method.IsConstructor() && method.IsImplicitlyDeclared && nullableInitialState != null)
			{
				NullableWalker.VariableState variableState;
				NullableWalker.AnalyzeIfNeeded(compilationState.Compilation, method, boundBlock ?? MethodCompiler.GetSynthesizedEmptyBody(method), diagnostics.DiagnosticBag, true, nullableInitialState, false, out variableState);
			}
			if (method.MethodKind == MethodKind.Destructor && boundBlock != null)
			{
				return MethodBodySynthesizer.ConstructDestructorBody(method, boundBlock);
			}
			BoundStatement boundStatement = MethodCompiler.BindImplicitConstructorInitializerIfAny(method, compilationState, diagnostics);
			ImmutableArray<BoundStatement> immutableArray;
			if (boundStatement == null)
			{
				if (boundBlock != null)
				{
					return boundBlock;
				}
				immutableArray = ImmutableArray<BoundStatement>.Empty;
			}
			else if (boundBlock == null)
			{
				immutableArray = ImmutableArray.Create<BoundStatement>(boundStatement);
			}
			else
			{
				immutableArray = ImmutableArray.Create<BoundStatement>(boundStatement, boundBlock);
				originalBodyNested = true;
			}
			return BoundBlock.SynthesizedNoLocals(method.GetNonNullSyntaxNode(), immutableArray);
		}

		// Token: 0x06001757 RID: 5975 RVA: 0x0008081B File Offset: 0x0007EA1B
		private static BoundBlock GetSynthesizedEmptyBody(Symbol symbol)
		{
			return BoundBlock.SynthesizedNoLocals(symbol.GetNonNullSyntaxNode(), Array.Empty<BoundStatement>());
		}

		// Token: 0x06001758 RID: 5976 RVA: 0x00080830 File Offset: 0x0007EA30
		private static BoundStatement BindImplicitConstructorInitializerIfAny(MethodSymbol method, TypeCompilationState compilationState, BindingDiagnosticBag diagnostics)
		{
			if (method.MethodKind == MethodKind.Constructor && !method.IsExtern)
			{
				CSharpCompilation declaringCompilation = method.DeclaringCompilation;
				BoundExpression boundExpression = MethodCompiler.BindImplicitConstructorInitializer(method, diagnostics, declaringCompilation);
				if (boundExpression != null)
				{
					MethodCompiler.ReportCtorInitializerCycles(method, boundExpression, compilationState, diagnostics);
					return new BoundExpressionStatement(boundExpression.Syntax, boundExpression, false)
					{
						WasCompilerGenerated = method.IsImplicitlyDeclared
					};
				}
			}
			return null;
		}

		// Token: 0x06001759 RID: 5977 RVA: 0x00080888 File Offset: 0x0007EA88
		private static void ReportCtorInitializerCycles(MethodSymbol method, BoundExpression initializerInvocation, TypeCompilationState compilationState, BindingDiagnosticBag diagnostics)
		{
			BoundCall boundCall = initializerInvocation as BoundCall;
			if (boundCall != null && !boundCall.HasAnyErrors && boundCall.Method != method && TypeSymbol.Equals(boundCall.Method.ContainingType, method.ContainingType, TypeCompareKind.ConsiderEverything))
			{
				compilationState.ReportCtorInitializerCycles(method, boundCall.Method, boundCall.Syntax, diagnostics);
			}
		}

		// Token: 0x0600175A RID: 5978 RVA: 0x000808E4 File Offset: 0x0007EAE4
		internal static BoundExpression BindImplicitConstructorInitializer(MethodSymbol constructor, BindingDiagnosticBag diagnostics, CSharpCompilation compilation)
		{
			NamedTypeSymbol containingType = constructor.ContainingType;
			NamedTypeSymbol baseTypeNoUseSiteDiagnostics = containingType.BaseTypeNoUseSiteDiagnostics;
			SourceMemberMethodSymbol sourceMemberMethodSymbol = constructor as SourceMemberMethodSymbol;
			if (baseTypeNoUseSiteDiagnostics != null)
			{
				if (baseTypeNoUseSiteDiagnostics.SpecialType == SpecialType.System_Object)
				{
					return MethodCompiler.GenerateBaseParameterlessConstructorInitializer(constructor, diagnostics);
				}
				if (baseTypeNoUseSiteDiagnostics.IsErrorType() || baseTypeNoUseSiteDiagnostics.IsStatic)
				{
					return null;
				}
			}
			if (containingType.IsStructType() || containingType.IsEnumType())
			{
				return null;
			}
			SynthesizedRecordCopyCtor synthesizedRecordCopyCtor = constructor as SynthesizedRecordCopyCtor;
			if (synthesizedRecordCopyCtor != null)
			{
				return MethodCompiler.GenerateBaseCopyConstructorInitializer(synthesizedRecordCopyCtor, diagnostics);
			}
			Binder binder;
			if (sourceMemberMethodSymbol == null)
			{
				CSharpSyntaxNode nonNullSyntaxNode = constructor.GetNonNullSyntaxNode();
				BinderFactory binderFactory = compilation.GetBinderFactory(nonNullSyntaxNode.SyntaxTree, false);
				RecordDeclarationSyntax recordDeclarationSyntax = nonNullSyntaxNode as RecordDeclarationSyntax;
				if (recordDeclarationSyntax != null)
				{
					binder = binderFactory.GetInRecordBodyBinder(recordDeclarationSyntax);
				}
				else
				{
					binder = binderFactory.GetBinder(nonNullSyntaxNode, MethodCompiler.GetImplicitConstructorBodyToken(nonNullSyntaxNode).Position, null, null);
				}
			}
			else
			{
				BinderFactory binderFactory2 = compilation.GetBinderFactory(sourceMemberMethodSymbol.SyntaxTree, false);
				CSharpSyntaxNode syntaxNode = sourceMemberMethodSymbol.SyntaxNode;
				ConstructorDeclarationSyntax constructorDeclarationSyntax = syntaxNode as ConstructorDeclarationSyntax;
				if (constructorDeclarationSyntax == null)
				{
					RecordDeclarationSyntax recordDeclarationSyntax2 = syntaxNode as RecordDeclarationSyntax;
					if (recordDeclarationSyntax2 == null)
					{
						throw ExceptionUtilities.Unreachable;
					}
					binder = binderFactory2.GetInRecordBodyBinder(recordDeclarationSyntax2);
				}
				else
				{
					binder = binderFactory2.GetBinder(constructorDeclarationSyntax.ParameterList, null, null);
				}
			}
			return binder.WithAdditionalFlagsAndContainingMemberOrLambda(BinderFlags.ConstructorInitializer, constructor).BindConstructorInitializer(null, constructor, diagnostics);
		}

		// Token: 0x0600175B RID: 5979 RVA: 0x00080A0D File Offset: 0x0007EC0D
		private static SyntaxToken GetImplicitConstructorBodyToken(CSharpSyntaxNode containerNode)
		{
			return ((BaseTypeDeclarationSyntax)containerNode).OpenBraceToken;
		}

		// Token: 0x0600175C RID: 5980 RVA: 0x00080A1C File Offset: 0x0007EC1C
		internal static BoundCall GenerateBaseParameterlessConstructorInitializer(MethodSymbol constructor, BindingDiagnosticBag diagnostics)
		{
			NamedTypeSymbol baseTypeNoUseSiteDiagnostics = constructor.ContainingType.BaseTypeNoUseSiteDiagnostics;
			MethodSymbol methodSymbol = null;
			LookupResultKind lookupResultKind = LookupResultKind.Viable;
			Location location = (constructor.Locations.IsEmpty ? NoLocation.Singleton : constructor.Locations[0]);
			foreach (MethodSymbol methodSymbol2 in baseTypeNoUseSiteDiagnostics.InstanceConstructors)
			{
				if (methodSymbol2.ParameterCount == 0)
				{
					methodSymbol = methodSymbol2;
					break;
				}
			}
			if (methodSymbol == null)
			{
				diagnostics.Add(ErrorCode.ERR_BadCtorArgCount, location, new object[] { baseTypeNoUseSiteDiagnostics, 0 });
				return null;
			}
			if (Binder.ReportUseSite(methodSymbol, diagnostics, location))
			{
				return null;
			}
			bool flag = false;
			CompoundUseSiteInfo<AssemblySymbol> compoundUseSiteInfo = new CompoundUseSiteInfo<AssemblySymbol>(diagnostics, constructor.ContainingAssembly);
			if (!AccessCheck.IsSymbolAccessible(methodSymbol, constructor.ContainingType, ref compoundUseSiteInfo, null))
			{
				diagnostics.Add(ErrorCode.ERR_BadAccess, location, new object[] { methodSymbol });
				lookupResultKind = LookupResultKind.Inaccessible;
				flag = true;
			}
			diagnostics.Add(location, compoundUseSiteInfo);
			CSharpSyntaxNode nonNullSyntaxNode = constructor.GetNonNullSyntaxNode();
			BoundExpression boundExpression = new BoundThisReference(nonNullSyntaxNode, constructor.ContainingType)
			{
				WasCompilerGenerated = true
			};
			return new BoundCall(nonNullSyntaxNode, boundExpression, methodSymbol, ImmutableArray<BoundExpression>.Empty, ImmutableArray<string>.Empty, ImmutableArray<RefKind>.Empty, false, false, false, ImmutableArray<int>.Empty, BitVector.Empty, lookupResultKind, methodSymbol.ReturnType, flag)
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x0600175D RID: 5981 RVA: 0x00080B5C File Offset: 0x0007ED5C
		private static BoundCall GenerateBaseCopyConstructorInitializer(SynthesizedRecordCopyCtor constructor, BindingDiagnosticBag diagnostics)
		{
			NamedTypeSymbol containingType = constructor.ContainingType;
			NamedTypeSymbol baseTypeNoUseSiteDiagnostics = containingType.BaseTypeNoUseSiteDiagnostics;
			Location location = constructor.Locations.FirstOrNone();
			CompoundUseSiteInfo<AssemblySymbol> compoundUseSiteInfo = new CompoundUseSiteInfo<AssemblySymbol>(diagnostics, containingType.ContainingAssembly);
			MethodSymbol methodSymbol = SynthesizedRecordCopyCtor.FindCopyConstructor(baseTypeNoUseSiteDiagnostics, containingType, ref compoundUseSiteInfo);
			if (methodSymbol == null)
			{
				diagnostics.Add(ErrorCode.ERR_NoCopyConstructorInBaseType, location, new object[] { baseTypeNoUseSiteDiagnostics });
				return null;
			}
			if (Binder.ReportUseSite(methodSymbol, diagnostics, location))
			{
				return null;
			}
			diagnostics.Add(location, compoundUseSiteInfo);
			CSharpSyntaxNode nonNullSyntaxNode = constructor.GetNonNullSyntaxNode();
			BoundExpression boundExpression = new BoundThisReference(nonNullSyntaxNode, constructor.ContainingType)
			{
				WasCompilerGenerated = true
			};
			BoundExpression boundExpression2 = new BoundParameter(nonNullSyntaxNode, constructor.Parameters[0]);
			return new BoundCall(nonNullSyntaxNode, boundExpression, methodSymbol, ImmutableArray.Create<BoundExpression>(boundExpression2), default(ImmutableArray<string>), default(ImmutableArray<RefKind>), false, false, false, default(ImmutableArray<int>), default(BitVector), LookupResultKind.Viable, methodSymbol.ReturnType, false)
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x0600175E RID: 5982 RVA: 0x00080C4E File Offset: 0x0007EE4E
		private static DebugSourceDocument CreateDebugDocumentForFile(string normalizedPath)
		{
			return new DebugSourceDocument(normalizedPath, DebugSourceDocument.CorSymLanguageTypeCSharp);
		}

		// Token: 0x0600175F RID: 5983 RVA: 0x00080C5B File Offset: 0x0007EE5B
		private static bool PassesFilter(Predicate<Symbol> filterOpt, Symbol symbol)
		{
			return filterOpt == null || filterOpt.Invoke(symbol);
		}

		// Token: 0x0400066C RID: 1644
		private readonly CSharpCompilation _compilation;

		// Token: 0x0400066D RID: 1645
		private readonly bool _emittingPdb;

		// Token: 0x0400066E RID: 1646
		private readonly bool _emitTestCoverageData;

		// Token: 0x0400066F RID: 1647
		private readonly CancellationToken _cancellationToken;

		// Token: 0x04000670 RID: 1648
		private readonly BindingDiagnosticBag _diagnostics;

		// Token: 0x04000671 RID: 1649
		private readonly bool _hasDeclarationErrors;

		// Token: 0x04000672 RID: 1650
		private readonly bool _emitMethodBodies;

		// Token: 0x04000673 RID: 1651
		private readonly PEModuleBuilder _moduleBeingBuiltOpt;

		// Token: 0x04000674 RID: 1652
		private readonly Predicate<Symbol> _filterOpt;

		// Token: 0x04000675 RID: 1653
		private readonly DebugDocumentProvider _debugDocumentProvider;

		// Token: 0x04000676 RID: 1654
		private readonly SynthesizedEntryPointSymbol.AsyncForwardEntryPoint _entryPointOpt;

		// Token: 0x04000677 RID: 1655
		private ConcurrentStack<Task> _compilerTasks;

		// Token: 0x04000678 RID: 1656
		private bool _globalHasErrors;
	}
}
