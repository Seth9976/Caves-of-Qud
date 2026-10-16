using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Symbols.Metadata.PE;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000115 RID: 277
	internal class ClsComplianceChecker : CSharpSymbolVisitor
	{
		// Token: 0x060016DA RID: 5850 RVA: 0x0007B28C File Offset: 0x0007948C
		private ClsComplianceChecker(CSharpCompilation compilation, SyntaxTree filterTree, TextSpan? filterSpanWithinTree, BindingDiagnosticBag diagnostics, CancellationToken cancellationToken)
		{
			this._compilation = compilation;
			this._filterTree = filterTree;
			this._filterSpanWithinTree = filterSpanWithinTree;
			this._diagnostics = diagnostics;
			this._cancellationToken = cancellationToken;
			this._declaredOrInheritedCompliance = new ConcurrentDictionary<Symbol, ClsComplianceChecker.Compliance>(SymbolEqualityComparer.ConsiderEverything);
			if (this.ConcurrentAnalysis)
			{
				this._compilerTasks = new ConcurrentStack<Task>();
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x060016DB RID: 5851 RVA: 0x0007B2E7 File Offset: 0x000794E7
		private bool ConcurrentAnalysis
		{
			get
			{
				return this._filterTree == null && this._compilation.Options.ConcurrentBuild;
			}
		}

		// Token: 0x060016DC RID: 5852 RVA: 0x0007B304 File Offset: 0x00079504
		public static void CheckCompliance(CSharpCompilation compilation, BindingDiagnosticBag diagnostics, CancellationToken cancellationToken, SyntaxTree filterTree = null, TextSpan? filterSpanWithinTree = null)
		{
			BindingDiagnosticBag bindingDiagnosticBag = new BindingDiagnosticBag(diagnostics.DiagnosticBag, diagnostics.AccumulatesDependencies ? new ConcurrentSet<AssemblySymbol>() : null);
			ClsComplianceChecker clsComplianceChecker = new ClsComplianceChecker(compilation, filterTree, filterSpanWithinTree, bindingDiagnosticBag, cancellationToken);
			clsComplianceChecker.Visit(compilation.Assembly);
			clsComplianceChecker.WaitForWorkers();
			diagnostics.AddDependencies(bindingDiagnosticBag, false);
		}

		// Token: 0x060016DD RID: 5853 RVA: 0x0007B354 File Offset: 0x00079554
		public override void VisitAssembly(AssemblySymbol symbol)
		{
			this._cancellationToken.ThrowIfCancellationRequested();
			ClsComplianceChecker.Compliance declaredOrInheritedCompliance = this.GetDeclaredOrInheritedCompliance(symbol);
			if (declaredOrInheritedCompliance == ClsComplianceChecker.Compliance.DeclaredFalse)
			{
				return;
			}
			bool flag = ClsComplianceChecker.IsTrue(declaredOrInheritedCompliance);
			for (int i = 0; i < symbol.Modules.Length; i++)
			{
				ModuleSymbol moduleSymbol = symbol.Modules[i];
				Location location;
				bool? declaredCompliance = this.GetDeclaredCompliance(moduleSymbol, out location);
				Location location2 = ((i == 0) ? location : moduleSymbol.Locations[0]);
				if (declaredCompliance != null)
				{
					if (location2 != null)
					{
						if (!ClsComplianceChecker.IsDeclared(declaredOrInheritedCompliance))
						{
							this.AddDiagnostic(ErrorCode.WRN_CLS_NotOnModules, location2);
						}
						else if (flag != declaredCompliance.GetValueOrDefault())
						{
							this.AddDiagnostic(ErrorCode.WRN_CLS_NotOnModules2, location2);
						}
					}
				}
				else if (flag && i > 0)
				{
					bool flag2 = false;
					PEModuleSymbol pemoduleSymbol = (PEModuleSymbol)moduleSymbol;
					ImmutableArray<CSharpAttributeData>.Enumerator enumerator = pemoduleSymbol.GetAssemblyAttributes().GetEnumerator();
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.IsTargetAttribute(pemoduleSymbol, AttributeDescription.CLSCompliantAttribute))
						{
							flag2 = true;
							break;
						}
					}
					if (!flag2)
					{
						this.AddDiagnostic(ErrorCode.WRN_CLS_ModuleMissingCLS, location2);
					}
				}
			}
			if (flag)
			{
				this.CheckForAttributeWithArrayArgument(symbol);
			}
			ModuleSymbol moduleSymbol2 = symbol.Modules[0];
			if (ClsComplianceChecker.IsTrue(this.GetDeclaredOrInheritedCompliance(moduleSymbol2)))
			{
				this.CheckForAttributeWithArrayArgument(moduleSymbol2);
			}
			this.Visit(symbol.GlobalNamespace);
		}

		// Token: 0x060016DE RID: 5854 RVA: 0x0007B4C0 File Offset: 0x000796C0
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

		// Token: 0x060016DF RID: 5855 RVA: 0x0007B4F4 File Offset: 0x000796F4
		public override void VisitNamespace(NamespaceSymbol symbol)
		{
			this._cancellationToken.ThrowIfCancellationRequested();
			if (this.DoNotVisit(symbol))
			{
				return;
			}
			if (ClsComplianceChecker.IsTrue(this.GetDeclaredOrInheritedCompliance(symbol)))
			{
				this.CheckName(symbol);
				this.CheckMemberDistinctness(symbol);
			}
			if (this.ConcurrentAnalysis)
			{
				this.VisitNamespaceMembersAsTasks(symbol);
				return;
			}
			this.VisitNamespaceMembers(symbol);
		}

		// Token: 0x060016E0 RID: 5856 RVA: 0x0007B54C File Offset: 0x0007974C
		private void VisitNamespaceMembersAsTasks(NamespaceSymbol symbol)
		{
			ImmutableArray<Symbol>.Enumerator enumerator = symbol.GetMembersUnordered().GetEnumerator();
			while (enumerator.MoveNext())
			{
				Symbol m = enumerator.Current;
				this._compilerTasks.Push(Task.Run(UICultureUtilities.WithCurrentUICulture(delegate
				{
					try
					{
						this.Visit(m);
					}
					catch (Exception ex) when (FatalError.ReportAndPropagateUnlessCanceled(ex))
					{
						throw ExceptionUtilities.Unreachable;
					}
				}), this._cancellationToken));
			}
		}

		// Token: 0x060016E1 RID: 5857 RVA: 0x0007B5B4 File Offset: 0x000797B4
		private void VisitNamespaceMembers(NamespaceSymbol symbol)
		{
			foreach (Symbol symbol2 in symbol.GetMembersUnordered())
			{
				this.Visit(symbol2);
			}
		}

		// Token: 0x060016E2 RID: 5858 RVA: 0x0007B5EC File Offset: 0x000797EC
		public override void VisitNamedType(NamedTypeSymbol symbol)
		{
			this._cancellationToken.ThrowIfCancellationRequested();
			if (this.DoNotVisit(symbol))
			{
				return;
			}
			ClsComplianceChecker.Compliance declaredOrInheritedCompliance = this.GetDeclaredOrInheritedCompliance(symbol);
			if (this.VisitTypeOrMember(symbol, declaredOrInheritedCompliance) && ClsComplianceChecker.IsTrue(declaredOrInheritedCompliance))
			{
				this.CheckBaseTypeCompliance(symbol);
				this.CheckTypeParameterCompliance(symbol.TypeParameters, symbol);
				if (symbol.TypeKind == TypeKind.Delegate)
				{
					this.CheckParameterCompliance(symbol.DelegateInvokeMethod.Parameters, symbol);
				}
				else if (this._compilation.IsAttributeType(symbol) && !this.HasAcceptableAttributeConstructor(symbol))
				{
					this.AddDiagnostic(ErrorCode.WRN_CLS_BadAttributeType, symbol.Locations[0], new object[] { symbol });
				}
			}
			foreach (Symbol symbol2 in symbol.GetMembersUnordered())
			{
				this.Visit(symbol2);
			}
		}

		// Token: 0x060016E3 RID: 5859 RVA: 0x0007B6C4 File Offset: 0x000798C4
		private bool HasAcceptableAttributeConstructor(NamedTypeSymbol attributeType)
		{
			foreach (MethodSymbol methodSymbol in attributeType.InstanceConstructors)
			{
				if (ClsComplianceChecker.IsTrue(this.GetDeclaredOrInheritedCompliance(methodSymbol)) && ClsComplianceChecker.IsAccessibleIfContainerIsAccessible(methodSymbol))
				{
					bool flag = false;
					foreach (TypeWithAnnotations typeWithAnnotations in methodSymbol.ParameterTypesWithAnnotations)
					{
						if (typeWithAnnotations.TypeKind == TypeKind.Array || typeWithAnnotations.Type.GetAttributeParameterTypedConstantKind(this._compilation) == TypedConstantKind.Error)
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060016E4 RID: 5860 RVA: 0x0007B758 File Offset: 0x00079958
		public override void VisitMethod(MethodSymbol symbol)
		{
			this._cancellationToken.ThrowIfCancellationRequested();
			if (this.DoNotVisit(symbol))
			{
				return;
			}
			ClsComplianceChecker.Compliance declaredOrInheritedCompliance = this.GetDeclaredOrInheritedCompliance(symbol);
			if (symbol.IsAccessor())
			{
				this.CheckForAttributeOnAccessor(symbol);
				this.CheckForMeaninglessOnParameter(symbol.Parameters);
				this.CheckForMeaninglessOnReturn(symbol);
				if (ClsComplianceChecker.IsTrue(declaredOrInheritedCompliance))
				{
					this.CheckForAttributeWithArrayArgument(symbol);
				}
				return;
			}
			if (!this.VisitTypeOrMember(symbol, declaredOrInheritedCompliance))
			{
				return;
			}
			if (ClsComplianceChecker.IsTrue(declaredOrInheritedCompliance))
			{
				this.CheckParameterCompliance(symbol.Parameters, symbol.ContainingType);
				this.CheckTypeParameterCompliance(symbol.TypeParameters, symbol.ContainingType);
				if (symbol.IsVararg)
				{
					this.AddDiagnostic(ErrorCode.WRN_CLS_NoVarArgs, symbol.Locations[0]);
				}
			}
		}

		// Token: 0x060016E5 RID: 5861 RVA: 0x0007B810 File Offset: 0x00079A10
		private void CheckForAttributeOnAccessor(MethodSymbol symbol)
		{
			foreach (CSharpAttributeData csharpAttributeData in symbol.GetAttributes())
			{
				Location location;
				if (csharpAttributeData.IsTargetAttribute(symbol, AttributeDescription.CLSCompliantAttribute) && this.TryGetAttributeWarningLocation(csharpAttributeData, out location))
				{
					AttributeUsageInfo attributeUsageInfo = csharpAttributeData.AttributeClass.GetAttributeUsageInfo();
					this.AddDiagnostic(ErrorCode.ERR_AttributeNotOnAccessor, location, new object[]
					{
						csharpAttributeData.AttributeClass.Name,
						attributeUsageInfo.GetValidTargetsErrorArgument()
					});
					return;
				}
			}
		}

		// Token: 0x060016E6 RID: 5862 RVA: 0x0007B890 File Offset: 0x00079A90
		public override void VisitProperty(PropertySymbol symbol)
		{
			this._cancellationToken.ThrowIfCancellationRequested();
			if (this.DoNotVisit(symbol))
			{
				return;
			}
			ClsComplianceChecker.Compliance declaredOrInheritedCompliance = this.GetDeclaredOrInheritedCompliance(symbol);
			if (!this.VisitTypeOrMember(symbol, declaredOrInheritedCompliance))
			{
				return;
			}
			if (ClsComplianceChecker.IsTrue(declaredOrInheritedCompliance))
			{
				this.CheckParameterCompliance(symbol.Parameters, symbol.ContainingType);
			}
		}

		// Token: 0x060016E7 RID: 5863 RVA: 0x0007B8E4 File Offset: 0x00079AE4
		public override void VisitEvent(EventSymbol symbol)
		{
			this._cancellationToken.ThrowIfCancellationRequested();
			if (this.DoNotVisit(symbol))
			{
				return;
			}
			ClsComplianceChecker.Compliance declaredOrInheritedCompliance = this.GetDeclaredOrInheritedCompliance(symbol);
			this.VisitTypeOrMember(symbol, declaredOrInheritedCompliance);
		}

		// Token: 0x060016E8 RID: 5864 RVA: 0x0007B91C File Offset: 0x00079B1C
		public override void VisitField(FieldSymbol symbol)
		{
			this._cancellationToken.ThrowIfCancellationRequested();
			if (this.DoNotVisit(symbol))
			{
				return;
			}
			ClsComplianceChecker.Compliance declaredOrInheritedCompliance = this.GetDeclaredOrInheritedCompliance(symbol);
			if (!this.VisitTypeOrMember(symbol, declaredOrInheritedCompliance))
			{
				return;
			}
			if (ClsComplianceChecker.IsTrue(declaredOrInheritedCompliance) && symbol.IsVolatile)
			{
				this.AddDiagnostic(ErrorCode.WRN_CLS_VolatileField, symbol.Locations[0], new object[] { symbol });
			}
		}

		// Token: 0x060016E9 RID: 5865 RVA: 0x0007B988 File Offset: 0x00079B88
		private bool VisitTypeOrMember(Symbol symbol, ClsComplianceChecker.Compliance compliance)
		{
			SymbolKind kind = symbol.Kind;
			if (!this.CheckForDeclarationWithoutAssemblyDeclaration(symbol, compliance))
			{
				return false;
			}
			bool flag = ClsComplianceChecker.IsTrue(compliance);
			bool flag2 = ClsComplianceChecker.IsAccessibleOutsideAssembly(symbol);
			if (flag2)
			{
				if (flag)
				{
					this.CheckName(symbol);
					this.CheckForCompliantWithinNonCompliant(symbol);
					this.CheckReturnTypeCompliance(symbol);
					if (symbol.Kind == SymbolKind.NamedType)
					{
						this.CheckMemberDistinctness((NamedTypeSymbol)symbol);
					}
				}
				else if (this.GetDeclaredOrInheritedCompliance(symbol.ContainingAssembly) == ClsComplianceChecker.Compliance.DeclaredTrue && ClsComplianceChecker.IsTrue(this.GetInheritedCompliance(symbol)))
				{
					this.CheckForNonCompliantAbstractMember(symbol);
				}
			}
			else if (ClsComplianceChecker.IsDeclared(compliance))
			{
				this.AddDiagnostic(ErrorCode.WRN_CLS_MeaninglessOnPrivateType, symbol.Locations[0], new object[] { symbol });
				return false;
			}
			if (flag)
			{
				this.CheckForAttributeWithArrayArgument(symbol);
			}
			if (kind == SymbolKind.NamedType)
			{
				NamedTypeSymbol namedTypeSymbol = (NamedTypeSymbol)symbol;
				if (namedTypeSymbol.TypeKind == TypeKind.Delegate)
				{
					MethodSymbol delegateInvokeMethod = namedTypeSymbol.DelegateInvokeMethod;
					this.CheckForMeaninglessOnParameter(delegateInvokeMethod.Parameters);
					this.CheckForMeaninglessOnReturn(delegateInvokeMethod);
				}
			}
			else if (kind == SymbolKind.Method)
			{
				MethodSymbol methodSymbol = (MethodSymbol)symbol;
				this.CheckForMeaninglessOnParameter(methodSymbol.Parameters);
				this.CheckForMeaninglessOnReturn(methodSymbol);
			}
			else if (kind == SymbolKind.Property)
			{
				PropertySymbol propertySymbol = (PropertySymbol)symbol;
				this.CheckForMeaninglessOnParameter(propertySymbol.Parameters);
			}
			return flag2;
		}

		// Token: 0x060016EA RID: 5866 RVA: 0x0007BABC File Offset: 0x00079CBC
		private void CheckForNonCompliantAbstractMember(Symbol symbol)
		{
			NamedTypeSymbol containingType = symbol.ContainingType;
			if (containingType != null && containingType.IsInterface)
			{
				this.AddDiagnostic(ErrorCode.WRN_CLS_BadInterfaceMember, symbol.Locations[0], new object[] { symbol });
				return;
			}
			if (symbol.IsAbstract && symbol.Kind != SymbolKind.NamedType)
			{
				this.AddDiagnostic(ErrorCode.WRN_CLS_NoAbstractMembers, symbol.Locations[0], new object[] { symbol });
			}
		}

		// Token: 0x060016EB RID: 5867 RVA: 0x0007BB38 File Offset: 0x00079D38
		private void CheckBaseTypeCompliance(NamedTypeSymbol symbol)
		{
			if (symbol.IsInterface)
			{
				foreach (NamedTypeSymbol namedTypeSymbol in symbol.InterfacesNoUseSiteDiagnostics(null))
				{
					if (!this.IsCompliantType(namedTypeSymbol, symbol))
					{
						this.AddDiagnostic(ErrorCode.WRN_CLS_BadInterface, symbol.Locations[0], new object[] { symbol, namedTypeSymbol });
					}
				}
				return;
			}
			NamedTypeSymbol namedTypeSymbol2 = symbol.EnumUnderlyingType ?? symbol.BaseTypeNoUseSiteDiagnostics;
			if (namedTypeSymbol2 != null && !this.IsCompliantType(namedTypeSymbol2, symbol))
			{
				this.AddDiagnostic(ErrorCode.WRN_CLS_BadBase, symbol.Locations[0], new object[] { symbol, namedTypeSymbol2 });
			}
		}

		// Token: 0x060016EC RID: 5868 RVA: 0x0007BBF0 File Offset: 0x00079DF0
		private void CheckForCompliantWithinNonCompliant(Symbol symbol)
		{
			NamedTypeSymbol containingType = symbol.ContainingType;
			if (containingType != null && !ClsComplianceChecker.IsTrue(this.GetDeclaredOrInheritedCompliance(containingType)))
			{
				this.AddDiagnostic(ErrorCode.WRN_CLS_IllegalTrueInFalse, symbol.Locations[0], new object[] { symbol, containingType });
			}
		}

		// Token: 0x060016ED RID: 5869 RVA: 0x0007BC40 File Offset: 0x00079E40
		private void CheckTypeParameterCompliance(ImmutableArray<TypeParameterSymbol> typeParameters, NamedTypeSymbol context)
		{
			foreach (TypeParameterSymbol typeParameterSymbol in typeParameters)
			{
				foreach (TypeWithAnnotations typeWithAnnotations in typeParameterSymbol.ConstraintTypesNoUseSiteDiagnostics)
				{
					if (!this.IsCompliantType(typeWithAnnotations.Type, context))
					{
						this.AddDiagnostic(ErrorCode.WRN_CLS_BadTypeVar, typeParameterSymbol.Locations[0], new object[] { typeWithAnnotations.Type });
					}
				}
			}
		}

		// Token: 0x060016EE RID: 5870 RVA: 0x0007BCC8 File Offset: 0x00079EC8
		private void CheckParameterCompliance(ImmutableArray<ParameterSymbol> parameters, NamedTypeSymbol context)
		{
			foreach (ParameterSymbol parameterSymbol in parameters)
			{
				if (!this.IsCompliantType(parameterSymbol.Type, context))
				{
					this.AddDiagnostic(ErrorCode.WRN_CLS_BadArgType, parameterSymbol.Locations[0], new object[] { parameterSymbol.Type });
				}
			}
		}

		// Token: 0x060016EF RID: 5871 RVA: 0x0007BD28 File Offset: 0x00079F28
		private void CheckForAttributeWithArrayArgument(Symbol symbol)
		{
			this.CheckForAttributeWithArrayArgumentInternal(symbol.GetAttributes());
			if (symbol.Kind == SymbolKind.Method)
			{
				this.CheckForAttributeWithArrayArgumentInternal(((MethodSymbol)symbol).GetReturnTypeAttributes());
			}
		}

		// Token: 0x060016F0 RID: 5872 RVA: 0x0007BD54 File Offset: 0x00079F54
		private void CheckForAttributeWithArrayArgumentInternal(ImmutableArray<CSharpAttributeData> attributes)
		{
			foreach (CSharpAttributeData csharpAttributeData in attributes)
			{
				foreach (TypedConstant typedConstant in csharpAttributeData.ConstructorArguments)
				{
					Location location;
					if (typedConstant.TypeInternal.TypeKind == TypeKind.Array && this.TryGetAttributeWarningLocation(csharpAttributeData, out location))
					{
						this.AddDiagnostic(ErrorCode.WRN_CLS_ArrayArgumentToAttribute, location);
						return;
					}
				}
				foreach (KeyValuePair<string, TypedConstant> keyValuePair in csharpAttributeData.NamedArguments)
				{
					Location location2;
					if (keyValuePair.Value.TypeInternal.TypeKind == TypeKind.Array && this.TryGetAttributeWarningLocation(csharpAttributeData, out location2))
					{
						this.AddDiagnostic(ErrorCode.WRN_CLS_ArrayArgumentToAttribute, location2);
						return;
					}
				}
				if (csharpAttributeData.AttributeConstructor != null)
				{
					foreach (TypeWithAnnotations typeWithAnnotations in csharpAttributeData.AttributeConstructor.ParameterTypesWithAnnotations)
					{
						Location location3;
						if (typeWithAnnotations.TypeKind == TypeKind.Array && this.TryGetAttributeWarningLocation(csharpAttributeData, out location3))
						{
							this.AddDiagnostic(ErrorCode.WRN_CLS_ArrayArgumentToAttribute, location3);
							return;
						}
					}
				}
			}
		}

		// Token: 0x060016F1 RID: 5873 RVA: 0x0007BEAC File Offset: 0x0007A0AC
		private bool TryGetAttributeWarningLocation(CSharpAttributeData attribute, out Location location)
		{
			SyntaxReference applicationSyntaxReference = attribute.ApplicationSyntaxReference;
			if (applicationSyntaxReference == null && this._filterTree == null)
			{
				location = NoLocation.Singleton;
				return true;
			}
			if (this._filterTree == null || (applicationSyntaxReference != null && applicationSyntaxReference.SyntaxTree == this._filterTree))
			{
				location = new SourceLocation(applicationSyntaxReference);
				return true;
			}
			location = null;
			return false;
		}

		// Token: 0x060016F2 RID: 5874 RVA: 0x0007BEFC File Offset: 0x0007A0FC
		private void CheckForMeaninglessOnParameter(ImmutableArray<ParameterSymbol> parameters)
		{
			if (parameters.IsEmpty)
			{
				return;
			}
			int num = 0;
			Symbol containingSymbol = parameters[0].ContainingSymbol;
			if (containingSymbol.Kind == SymbolKind.Method)
			{
				Symbol associatedSymbol = ((MethodSymbol)containingSymbol).AssociatedSymbol;
				if (associatedSymbol != null && associatedSymbol.Kind == SymbolKind.Property)
				{
					num = ((PropertySymbol)associatedSymbol).ParameterCount;
				}
			}
			for (int i = num; i < parameters.Length; i++)
			{
				Location location;
				if (this.TryGetClsComplianceAttributeLocation(parameters[i].GetAttributes(), parameters[i], out location))
				{
					this.AddDiagnostic(ErrorCode.WRN_CLS_MeaninglessOnParam, location);
				}
			}
		}

		// Token: 0x060016F3 RID: 5875 RVA: 0x0007BF90 File Offset: 0x0007A190
		private void CheckForMeaninglessOnReturn(MethodSymbol method)
		{
			Location location;
			if (this.TryGetClsComplianceAttributeLocation(method.GetReturnTypeAttributes(), method, out location))
			{
				this.AddDiagnostic(ErrorCode.WRN_CLS_MeaninglessOnReturn, location);
			}
		}

		// Token: 0x060016F4 RID: 5876 RVA: 0x0007BFBC File Offset: 0x0007A1BC
		private void CheckReturnTypeCompliance(Symbol symbol)
		{
			SymbolKind kind = symbol.Kind;
			ErrorCode errorCode;
			TypeSymbol typeSymbol;
			switch (kind)
			{
			case SymbolKind.Event:
				errorCode = ErrorCode.WRN_CLS_BadFieldPropType;
				typeSymbol = ((EventSymbol)symbol).Type;
				goto IL_00B7;
			case SymbolKind.Field:
				errorCode = ErrorCode.WRN_CLS_BadFieldPropType;
				typeSymbol = ((FieldSymbol)symbol).Type;
				goto IL_00B7;
			case SymbolKind.Label:
			case SymbolKind.Local:
			case SymbolKind.NetModule:
				goto IL_00A6;
			case SymbolKind.Method:
				break;
			case SymbolKind.NamedType:
				symbol = ((NamedTypeSymbol)symbol).DelegateInvokeMethod;
				if (symbol == null)
				{
					return;
				}
				break;
			default:
				if (kind != SymbolKind.Property)
				{
					goto IL_00A6;
				}
				errorCode = ErrorCode.WRN_CLS_BadFieldPropType;
				typeSymbol = ((PropertySymbol)symbol).Type;
				goto IL_00B7;
			}
			errorCode = ErrorCode.WRN_CLS_BadReturnType;
			MethodSymbol methodSymbol = (MethodSymbol)symbol;
			typeSymbol = methodSymbol.ReturnType;
			if (methodSymbol.MethodKind == MethodKind.DelegateInvoke)
			{
				symbol = methodSymbol.ContainingType;
				goto IL_00B7;
			}
			goto IL_00B7;
			IL_00A6:
			throw ExceptionUtilities.UnexpectedValue(symbol.Kind);
			IL_00B7:
			if (!this.IsCompliantType(typeSymbol, symbol.ContainingType))
			{
				this.AddDiagnostic(errorCode, symbol.Locations[0], new object[] { symbol });
			}
		}

		// Token: 0x060016F5 RID: 5877 RVA: 0x0007C0B0 File Offset: 0x0007A2B0
		private bool TryGetClsComplianceAttributeLocation(ImmutableArray<CSharpAttributeData> attributes, Symbol targetSymbol, out Location attributeLocation)
		{
			foreach (CSharpAttributeData csharpAttributeData in attributes)
			{
				if (csharpAttributeData.IsTargetAttribute(targetSymbol, AttributeDescription.CLSCompliantAttribute) && this.TryGetAttributeWarningLocation(csharpAttributeData, out attributeLocation))
				{
					return true;
				}
			}
			attributeLocation = null;
			return false;
		}

		// Token: 0x060016F6 RID: 5878 RVA: 0x0007C0F8 File Offset: 0x0007A2F8
		private bool CheckForDeclarationWithoutAssemblyDeclaration(Symbol symbol, ClsComplianceChecker.Compliance compliance)
		{
			if (ClsComplianceChecker.IsDeclared(compliance) && !ClsComplianceChecker.IsDeclared(this.GetDeclaredOrInheritedCompliance(symbol.ContainingAssembly)))
			{
				ErrorCode errorCode = (ClsComplianceChecker.IsTrue(compliance) ? ErrorCode.WRN_CLS_AssemblyNotCLS : ErrorCode.WRN_CLS_AssemblyNotCLS2);
				this.AddDiagnostic(errorCode, symbol.Locations[0], new object[] { symbol });
				return false;
			}
			return true;
		}

		// Token: 0x060016F7 RID: 5879 RVA: 0x0007C158 File Offset: 0x0007A358
		private void CheckMemberDistinctness(NamespaceOrTypeSymbol symbol)
		{
			MultiDictionary<string, Symbol> multiDictionary = new MultiDictionary<string, Symbol>(CaseInsensitiveComparison.Comparer);
			if (symbol.Kind != SymbolKind.Namespace)
			{
				NamedTypeSymbol namedTypeSymbol = (NamedTypeSymbol)symbol;
				foreach (NamedTypeSymbol namedTypeSymbol2 in namedTypeSymbol.InterfacesAndTheirBaseInterfacesNoUseSiteDiagnostics.Keys)
				{
					if (ClsComplianceChecker.IsAccessibleOutsideAssembly(namedTypeSymbol2))
					{
						foreach (Symbol symbol2 in namedTypeSymbol2.GetMembersUnordered())
						{
							if (ClsComplianceChecker.IsAccessibleIfContainerIsAccessible(symbol2) && (!symbol2.IsOverride || (symbol2.Kind != SymbolKind.Method && symbol2.Kind != SymbolKind.Property)))
							{
								multiDictionary.Add(symbol2.Name, symbol2);
							}
						}
					}
				}
				for (NamedTypeSymbol namedTypeSymbol3 = namedTypeSymbol.BaseTypeNoUseSiteDiagnostics; namedTypeSymbol3 != null; namedTypeSymbol3 = namedTypeSymbol3.BaseTypeNoUseSiteDiagnostics)
				{
					foreach (Symbol symbol3 in namedTypeSymbol3.GetMembersUnordered())
					{
						if (ClsComplianceChecker.IsAccessibleOutsideAssembly(symbol3) && ClsComplianceChecker.IsTrue(this.GetDeclaredOrInheritedCompliance(symbol3)) && (!symbol3.IsOverride || (symbol3.Kind != SymbolKind.Method && symbol3.Kind != SymbolKind.Property)))
						{
							multiDictionary.Add(symbol3.Name, symbol3);
						}
					}
				}
			}
			foreach (Symbol symbol4 in symbol.GetMembers())
			{
				if (!this.DoNotVisit(symbol4) && ClsComplianceChecker.IsAccessibleIfContainerIsAccessible(symbol4) && ClsComplianceChecker.IsTrue(this.GetDeclaredOrInheritedCompliance(symbol4)) && !symbol4.IsOverride)
				{
					string name = symbol4.Name;
					MultiDictionary<string, Symbol>.ValueSet valueSet = multiDictionary[name];
					if (valueSet.Count > 0)
					{
						this.CheckSymbolDistinctness(symbol4, name, valueSet);
					}
					multiDictionary.Add(name, symbol4);
				}
			}
		}

		// Token: 0x060016F8 RID: 5880 RVA: 0x0007C334 File Offset: 0x0007A534
		private void CheckSymbolDistinctness(Symbol symbol, string symbolName, MultiDictionary<string, Symbol>.ValueSet sameNameSymbols)
		{
			bool flag = symbol.Kind == SymbolKind.Method || symbol.Kind == SymbolKind.Property;
			foreach (Symbol symbol2 in sameNameSymbols)
			{
				if (symbol2.Name != symbolName && (!flag || symbol2.Kind != symbol.Kind))
				{
					this.AddDiagnostic(ErrorCode.WRN_CLS_BadIdentifierCase, symbol.Locations[0], new object[] { symbol });
					return;
				}
			}
			if (!flag)
			{
				return;
			}
			foreach (Symbol symbol3 in sameNameSymbols)
			{
				ErrorCode errorCode;
				if (symbol.Kind == symbol3.Kind && !symbol.IsAccessor() && !symbol3.IsAccessor() && ClsComplianceChecker.TryGetCollisionErrorCode(symbol, symbol3, out errorCode))
				{
					this.AddDiagnostic(errorCode, symbol.Locations[0], new object[] { symbol });
					break;
				}
				if (symbol3.Name != symbolName)
				{
					this.AddDiagnostic(ErrorCode.WRN_CLS_BadIdentifierCase, symbol.Locations[0], new object[] { symbol });
					break;
				}
			}
		}

		// Token: 0x060016F9 RID: 5881 RVA: 0x0007C4A4 File Offset: 0x0007A6A4
		private void CheckName(Symbol symbol)
		{
			if (!symbol.CanBeReferencedByName || symbol.IsOverride)
			{
				return;
			}
			string name = symbol.Name;
			if (name.Length > 0 && name.get_Chars(0) == '_')
			{
				this.AddDiagnostic(ErrorCode.WRN_CLS_BadIdentifier, symbol.Locations[0], new object[] { name });
			}
		}

		// Token: 0x060016FA RID: 5882 RVA: 0x0007C501 File Offset: 0x0007A701
		private bool DoNotVisit(Symbol symbol)
		{
			return symbol.Kind != SymbolKind.Namespace && (symbol.DeclaringCompilation != this._compilation || symbol.IsImplicitlyDeclared || this.IsSyntacticallyFilteredOut(symbol));
		}

		// Token: 0x060016FB RID: 5883 RVA: 0x0007C530 File Offset: 0x0007A730
		private bool IsSyntacticallyFilteredOut(Symbol symbol)
		{
			return this._filterTree != null && !symbol.IsDefinedInSourceTree(this._filterTree, this._filterSpanWithinTree, default(CancellationToken));
		}

		// Token: 0x060016FC RID: 5884 RVA: 0x0007C568 File Offset: 0x0007A768
		private bool IsCompliantType(TypeSymbol type, NamedTypeSymbol context)
		{
			switch (type.TypeKind)
			{
			case TypeKind.Array:
				return this.IsCompliantType(((ArrayTypeSymbol)type).ElementType, context);
			case TypeKind.Class:
			case TypeKind.Delegate:
			case TypeKind.Enum:
			case TypeKind.Interface:
			case TypeKind.Struct:
			case TypeKind.Submission:
				return this.IsCompliantType((NamedTypeSymbol)type, context);
			case TypeKind.Dynamic:
				return true;
			case TypeKind.Error:
			case TypeKind.TypeParameter:
				return true;
			case TypeKind.Pointer:
			case TypeKind.FunctionPointer:
				return false;
			}
			throw ExceptionUtilities.UnexpectedValue(type.TypeKind);
		}

		// Token: 0x060016FD RID: 5885 RVA: 0x0007C5F4 File Offset: 0x0007A7F4
		private bool IsCompliantType(NamedTypeSymbol type, NamedTypeSymbol context)
		{
			SpecialType specialType = type.SpecialType;
			if (specialType <= SpecialType.System_UInt64)
			{
				if (specialType != SpecialType.System_SByte)
				{
					switch (specialType)
					{
					case SpecialType.System_UInt16:
					case SpecialType.System_UInt32:
					case SpecialType.System_UInt64:
						break;
					case SpecialType.System_Int32:
					case SpecialType.System_Int64:
						goto IL_003E;
					default:
						goto IL_003E;
					}
				}
				return false;
			}
			if (specialType == SpecialType.System_UIntPtr || specialType == SpecialType.System_TypedReference)
			{
				return false;
			}
			IL_003E:
			if (type.TypeKind == TypeKind.Error)
			{
				return true;
			}
			if (!ClsComplianceChecker.IsTrue(this.GetDeclaredOrInheritedCompliance(type.OriginalDefinition)))
			{
				return false;
			}
			foreach (TypeWithAnnotations typeWithAnnotations in type.TypeArgumentsWithAnnotationsNoUseSiteDiagnostics)
			{
				if (!this.IsCompliantType(typeWithAnnotations.Type, context))
				{
					return false;
				}
			}
			return !ClsComplianceChecker.IsInaccessibleBecauseOfConstruction(type, context);
		}

		// Token: 0x060016FE RID: 5886 RVA: 0x0007C6A0 File Offset: 0x0007A8A0
		private static bool IsInaccessibleBecauseOfConstruction(NamedTypeSymbol type, NamedTypeSymbol context)
		{
			bool flag = type.DeclaredAccessibility.HasProtected();
			bool flag2 = false;
			Dictionary<NamedTypeSymbol, NamedTypeSymbol> dictionary = null;
			for (NamedTypeSymbol namedTypeSymbol = type.ContainingType; namedTypeSymbol != null; namedTypeSymbol = namedTypeSymbol.ContainingType)
			{
				if (dictionary == null)
				{
					dictionary = new Dictionary<NamedTypeSymbol, NamedTypeSymbol>();
				}
				flag = flag || namedTypeSymbol.DeclaredAccessibility.HasProtected();
				flag2 = flag2 || namedTypeSymbol.Arity > 0;
				dictionary.Add(namedTypeSymbol.OriginalDefinition, namedTypeSymbol);
			}
			if (!flag || !flag2 || dictionary == null)
			{
				return false;
			}
			while (context != null)
			{
				for (NamedTypeSymbol namedTypeSymbol2 = context; namedTypeSymbol2 != null; namedTypeSymbol2 = namedTypeSymbol2.BaseTypeNoUseSiteDiagnostics)
				{
					NamedTypeSymbol namedTypeSymbol3;
					if (dictionary.TryGetValue(namedTypeSymbol2.OriginalDefinition, ref namedTypeSymbol3))
					{
						return !TypeSymbol.Equals(namedTypeSymbol3, namedTypeSymbol2, TypeCompareKind.ConsiderEverything);
					}
				}
				context = context.ContainingType;
			}
			return false;
		}

		// Token: 0x060016FF RID: 5887 RVA: 0x0007C750 File Offset: 0x0007A950
		private ClsComplianceChecker.Compliance GetDeclaredOrInheritedCompliance(Symbol symbol)
		{
			if (symbol.Kind == SymbolKind.Namespace)
			{
				return this.GetDeclaredOrInheritedCompliance(symbol.ContainingAssembly);
			}
			if (symbol.Kind == SymbolKind.Method)
			{
				Symbol associatedSymbol = ((MethodSymbol)symbol).AssociatedSymbol;
				if (associatedSymbol != null)
				{
					return this.GetDeclaredOrInheritedCompliance(associatedSymbol);
				}
			}
			ClsComplianceChecker.Compliance compliance;
			if (this._declaredOrInheritedCompliance.TryGetValue(symbol, ref compliance))
			{
				return compliance;
			}
			Location location;
			bool? declaredCompliance = this.GetDeclaredCompliance(symbol, out location);
			if (declaredCompliance != null)
			{
				compliance = (declaredCompliance.GetValueOrDefault() ? ClsComplianceChecker.Compliance.DeclaredTrue : ClsComplianceChecker.Compliance.DeclaredFalse);
			}
			else if (symbol.Kind == SymbolKind.Assembly)
			{
				compliance = ClsComplianceChecker.Compliance.ImpliedFalse;
			}
			else
			{
				compliance = (ClsComplianceChecker.IsTrue(this.GetInheritedCompliance(symbol)) ? ClsComplianceChecker.Compliance.InheritedTrue : ClsComplianceChecker.Compliance.InheritedFalse);
			}
			if (symbol.Kind != SymbolKind.Assembly && symbol.Kind != SymbolKind.NamedType)
			{
				return compliance;
			}
			return this._declaredOrInheritedCompliance.GetOrAdd(symbol, compliance);
		}

		// Token: 0x06001700 RID: 5888 RVA: 0x0007C80C File Offset: 0x0007AA0C
		private ClsComplianceChecker.Compliance GetInheritedCompliance(Symbol symbol)
		{
			Symbol symbol2 = symbol.ContainingType ?? symbol.ContainingAssembly;
			return this.GetDeclaredOrInheritedCompliance(symbol2);
		}

		// Token: 0x06001701 RID: 5889 RVA: 0x0007C834 File Offset: 0x0007AA34
		private bool? GetDeclaredCompliance(Symbol symbol, out Location attributeLocation)
		{
			attributeLocation = null;
			foreach (CSharpAttributeData csharpAttributeData in symbol.GetAttributes())
			{
				if (csharpAttributeData.IsTargetAttribute(symbol, AttributeDescription.CLSCompliantAttribute))
				{
					NamedTypeSymbol attributeClass = csharpAttributeData.AttributeClass;
					if ((attributeClass == null || !this._diagnostics.ReportUseSite(attributeClass, symbol.Locations.IsEmpty ? NoLocation.Singleton : symbol.Locations[0])) && !csharpAttributeData.HasErrors)
					{
						if (!this.TryGetAttributeWarningLocation(csharpAttributeData, out attributeLocation))
						{
							attributeLocation = null;
						}
						return new bool?((bool)csharpAttributeData.CommonConstructorArguments[0].ValueInternal);
					}
				}
			}
			return default(bool?);
		}

		// Token: 0x06001702 RID: 5890 RVA: 0x0007C8FD File Offset: 0x0007AAFD
		private static bool IsAccessibleOutsideAssembly(Symbol symbol)
		{
			while (symbol != null && !ClsComplianceChecker.IsImplicitClass(symbol))
			{
				if (!ClsComplianceChecker.IsAccessibleIfContainerIsAccessible(symbol))
				{
					return false;
				}
				symbol = symbol.ContainingType;
			}
			return true;
		}

		// Token: 0x06001703 RID: 5891 RVA: 0x0007C920 File Offset: 0x0007AB20
		private static bool IsAccessibleIfContainerIsAccessible(Symbol symbol)
		{
			switch (symbol.DeclaredAccessibility)
			{
			case Accessibility.NotApplicable:
				return false;
			case Accessibility.Private:
			case Accessibility.ProtectedAndInternal:
			case Accessibility.Internal:
				return false;
			case Accessibility.Protected:
			case Accessibility.ProtectedOrInternal:
			case Accessibility.Public:
				return true;
			default:
				throw ExceptionUtilities.UnexpectedValue(symbol.DeclaredAccessibility);
			}
		}

		// Token: 0x06001704 RID: 5892 RVA: 0x0007C970 File Offset: 0x0007AB70
		private void AddDiagnostic(ErrorCode code, Location location)
		{
			CSDiagnostic csdiagnostic = new CSDiagnostic(new CSDiagnosticInfo(code), location, false);
			this._diagnostics.Add(csdiagnostic);
		}

		// Token: 0x06001705 RID: 5893 RVA: 0x0007C998 File Offset: 0x0007AB98
		private void AddDiagnostic(ErrorCode code, Location location, params object[] args)
		{
			CSDiagnostic csdiagnostic = new CSDiagnostic(new CSDiagnosticInfo(code, args), location, false);
			this._diagnostics.Add(csdiagnostic);
		}

		// Token: 0x06001706 RID: 5894 RVA: 0x0007C9C0 File Offset: 0x0007ABC0
		private static bool IsImplicitClass(Symbol symbol)
		{
			return symbol.Kind == SymbolKind.NamedType && ((NamedTypeSymbol)symbol).IsImplicitClass;
		}

		// Token: 0x06001707 RID: 5895 RVA: 0x0007C9D9 File Offset: 0x0007ABD9
		private static bool IsTrue(ClsComplianceChecker.Compliance compliance)
		{
			switch (compliance)
			{
			case ClsComplianceChecker.Compliance.DeclaredTrue:
			case ClsComplianceChecker.Compliance.InheritedTrue:
				return true;
			case ClsComplianceChecker.Compliance.DeclaredFalse:
			case ClsComplianceChecker.Compliance.InheritedFalse:
			case ClsComplianceChecker.Compliance.ImpliedFalse:
				return false;
			default:
				throw ExceptionUtilities.UnexpectedValue(compliance);
			}
		}

		// Token: 0x06001708 RID: 5896 RVA: 0x0007CA06 File Offset: 0x0007AC06
		private static bool IsDeclared(ClsComplianceChecker.Compliance compliance)
		{
			if (compliance <= ClsComplianceChecker.Compliance.DeclaredFalse)
			{
				return true;
			}
			if (compliance - ClsComplianceChecker.Compliance.InheritedTrue > 2)
			{
				throw ExceptionUtilities.UnexpectedValue(compliance);
			}
			return false;
		}

		// Token: 0x06001709 RID: 5897 RVA: 0x0007CA24 File Offset: 0x0007AC24
		private static bool TryGetCollisionErrorCode(Symbol x, Symbol y, out ErrorCode code)
		{
			code = ErrorCode.Void;
			SymbolKind kind = x.Kind;
			ImmutableArray<TypeWithAnnotations> immutableArray;
			ImmutableArray<RefKind> immutableArray2;
			ImmutableArray<TypeWithAnnotations> immutableArray3;
			ImmutableArray<RefKind> immutableArray4;
			if (kind != SymbolKind.Method)
			{
				if (kind != SymbolKind.Property)
				{
					throw ExceptionUtilities.UnexpectedValue(x.Kind);
				}
				PropertySymbol propertySymbol = (PropertySymbol)x;
				immutableArray = propertySymbol.ParameterTypesWithAnnotations;
				immutableArray2 = propertySymbol.ParameterRefKinds;
				PropertySymbol propertySymbol2 = (PropertySymbol)y;
				immutableArray3 = propertySymbol2.ParameterTypesWithAnnotations;
				immutableArray4 = propertySymbol2.ParameterRefKinds;
			}
			else
			{
				MethodSymbol methodSymbol = (MethodSymbol)x;
				immutableArray = methodSymbol.ParameterTypesWithAnnotations;
				immutableArray2 = methodSymbol.ParameterRefKinds;
				MethodSymbol methodSymbol2 = (MethodSymbol)y;
				immutableArray3 = methodSymbol2.ParameterTypesWithAnnotations;
				immutableArray4 = methodSymbol2.ParameterRefKinds;
			}
			int length = immutableArray.Length;
			if (immutableArray3.Length != length)
			{
				return false;
			}
			bool flag = immutableArray2.IsDefault != immutableArray4.IsDefault;
			bool flag2 = false;
			bool flag3 = false;
			for (int i = 0; i < length; i++)
			{
				TypeSymbol type = immutableArray[i].Type;
				TypeSymbol type2 = immutableArray3[i].Type;
				TypeKind typeKind = type.TypeKind;
				if (type2.TypeKind != typeKind)
				{
					return false;
				}
				if (typeKind == TypeKind.Array)
				{
					ArrayTypeSymbol arrayTypeSymbol = (ArrayTypeSymbol)type;
					ArrayTypeSymbol arrayTypeSymbol2 = (ArrayTypeSymbol)type2;
					flag2 = flag2 || arrayTypeSymbol.Rank != arrayTypeSymbol2.Rank;
					bool flag4 = !TypeSymbol.Equals(arrayTypeSymbol.ElementType, arrayTypeSymbol2.ElementType, TypeCompareKind.ConsiderEverything);
					if (ClsComplianceChecker.IsArrayOfArrays(arrayTypeSymbol) || ClsComplianceChecker.IsArrayOfArrays(arrayTypeSymbol2))
					{
						flag3 = flag3 || flag4;
					}
					else if (flag4)
					{
						return false;
					}
				}
				else if (!TypeSymbol.Equals(type, type2, TypeCompareKind.ConsiderEverything))
				{
					return false;
				}
				if (!immutableArray2.IsDefault)
				{
					flag = flag || immutableArray2[i] != immutableArray4[i];
				}
			}
			code = (flag3 ? ErrorCode.WRN_CLS_OverloadUnnamed : (flag2 ? ErrorCode.WRN_CLS_OverloadRefOut : (flag ? ErrorCode.WRN_CLS_OverloadRefOut : ErrorCode.Void)));
			return code != ErrorCode.Void;
		}

		// Token: 0x0600170A RID: 5898 RVA: 0x0007CC04 File Offset: 0x0007AE04
		private static bool IsArrayOfArrays(ArrayTypeSymbol arrayType)
		{
			return arrayType.ElementType.Kind == SymbolKind.ArrayType;
		}

		// Token: 0x04000657 RID: 1623
		private readonly CSharpCompilation _compilation;

		// Token: 0x04000658 RID: 1624
		private readonly SyntaxTree _filterTree;

		// Token: 0x04000659 RID: 1625
		private readonly TextSpan? _filterSpanWithinTree;

		// Token: 0x0400065A RID: 1626
		private readonly BindingDiagnosticBag _diagnostics;

		// Token: 0x0400065B RID: 1627
		private readonly CancellationToken _cancellationToken;

		// Token: 0x0400065C RID: 1628
		private readonly ConcurrentDictionary<Symbol, ClsComplianceChecker.Compliance> _declaredOrInheritedCompliance;

		// Token: 0x0400065D RID: 1629
		private readonly ConcurrentStack<Task> _compilerTasks;

		// Token: 0x020006C2 RID: 1730
		private enum Compliance
		{
			// Token: 0x0400217B RID: 8571
			DeclaredTrue,
			// Token: 0x0400217C RID: 8572
			DeclaredFalse,
			// Token: 0x0400217D RID: 8573
			InheritedTrue,
			// Token: 0x0400217E RID: 8574
			InheritedFalse,
			// Token: 0x0400217F RID: 8575
			ImpliedFalse
		}
	}
}
