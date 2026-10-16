using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.CodeGen;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001F0 RID: 496
	internal sealed class ClosureConversion : MethodToClassRewriter
	{
		// Token: 0x060021BC RID: 8636 RVA: 0x000B06B0 File Offset: 0x000AE8B0
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		private ClosureConversion(ClosureConversion.Analysis analysis, NamedTypeSymbol thisType, ParameterSymbol thisParameterOpt, MethodSymbol method, int methodOrdinal, MethodSymbol substitutedSourceMethod, ArrayBuilder<LambdaDebugInfo> lambdaDebugInfoBuilder, VariableSlotAllocator slotAllocatorOpt, TypeCompilationState compilationState, BindingDiagnosticBag diagnostics, HashSet<LocalSymbol> assignLocals)
			: base(slotAllocatorOpt, compilationState, diagnostics)
		{
			this._topLevelMethod = method;
			this._substitutedSourceMethod = substitutedSourceMethod;
			this._topLevelMethodOrdinal = methodOrdinal;
			this._lambdaDebugInfoBuilder = lambdaDebugInfoBuilder;
			this._currentMethod = method;
			this._analysis = analysis;
			this._assignLocals = assignLocals;
			this._currentTypeParameters = method.TypeParameters;
			this._currentLambdaBodyTypeMap = TypeMap.Empty;
			this._currentFrameThis = thisParameterOpt;
			this._innermostFramePointer = thisParameterOpt;
			this._framePointers[thisType] = thisParameterOpt;
			this._seenBaseCall = method.MethodKind != MethodKind.Constructor;
			this._synthesizedFieldNameIdDispenser = 1;
			ImmutableHashSet<Symbol>.Builder allCapturedVars = ImmutableHashSet.CreateBuilder<Symbol>();
			ClosureConversion.Analysis.VisitNestedFunctions(analysis.ScopeTree, delegate(ClosureConversion.Analysis.Scope scope, ClosureConversion.Analysis.NestedFunction function)
			{
				allCapturedVars.UnionWith(function.CapturedVariables);
			});
			this._allCapturedVariables = allCapturedVars.ToImmutable();
		}

		// Token: 0x060021BD RID: 8637 RVA: 0x000B07A9 File Offset: 0x000AE9A9
		protected override bool NeedsProxy(Symbol localOrParameter)
		{
			return this._allCapturedVariables.Contains(localOrParameter);
		}

		// Token: 0x060021BE RID: 8638 RVA: 0x000B07B8 File Offset: 0x000AE9B8
		public static BoundStatement Rewrite(BoundStatement loweredBody, NamedTypeSymbol thisType, ParameterSymbol thisParameter, MethodSymbol method, int methodOrdinal, MethodSymbol substitutedSourceMethod, ArrayBuilder<LambdaDebugInfo> lambdaDebugInfoBuilder, ArrayBuilder<ClosureDebugInfo> closureDebugInfoBuilder, VariableSlotAllocator slotAllocatorOpt, TypeCompilationState compilationState, BindingDiagnosticBag diagnostics, HashSet<LocalSymbol> assignLocals)
		{
			ClosureConversion.Analysis analysis = ClosureConversion.Analysis.Analyze(loweredBody, method, methodOrdinal, substitutedSourceMethod, slotAllocatorOpt, compilationState, closureDebugInfoBuilder, diagnostics.DiagnosticBag);
			ClosureConversion closureConversion = new ClosureConversion(analysis, thisType, thisParameter, method, methodOrdinal, substitutedSourceMethod, lambdaDebugInfoBuilder, slotAllocatorOpt, compilationState, diagnostics, assignLocals);
			closureConversion.SynthesizeClosureEnvironments(closureDebugInfoBuilder);
			closureConversion.SynthesizeClosureMethods();
			BoundStatement boundStatement = closureConversion.AddStatementsIfNeeded((BoundStatement)closureConversion.Visit(loweredBody));
			if (closureConversion._synthesizedMethods != null)
			{
				if (compilationState.SynthesizedMethods == null)
				{
					compilationState.SynthesizedMethods = closureConversion._synthesizedMethods;
				}
				else
				{
					compilationState.SynthesizedMethods.AddRange(closureConversion._synthesizedMethods);
					closureConversion._synthesizedMethods.Free();
				}
			}
			analysis.Free();
			return boundStatement;
		}

		// Token: 0x060021BF RID: 8639 RVA: 0x000B085C File Offset: 0x000AEA5C
		private BoundStatement AddStatementsIfNeeded(BoundStatement body)
		{
			if (this._addedLocals != null)
			{
				this._addedStatements.Add(body);
				body = new BoundBlock(body.Syntax, this._addedLocals.ToImmutableAndFree(), this._addedStatements.ToImmutableAndFree(), false)
				{
					WasCompilerGenerated = true
				};
				this._addedLocals = null;
				this._addedStatements = null;
			}
			return body;
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x060021C0 RID: 8640 RVA: 0x000B08B7 File Offset: 0x000AEAB7
		protected override TypeMap TypeMap
		{
			get
			{
				return this._currentLambdaBodyTypeMap;
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x060021C1 RID: 8641 RVA: 0x000B08BF File Offset: 0x000AEABF
		protected override MethodSymbol CurrentMethod
		{
			get
			{
				return this._currentMethod;
			}
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x060021C2 RID: 8642 RVA: 0x000B08C7 File Offset: 0x000AEAC7
		protected override NamedTypeSymbol ContainingType
		{
			get
			{
				return this._topLevelMethod.ContainingType;
			}
		}

		// Token: 0x060021C3 RID: 8643 RVA: 0x000B08D4 File Offset: 0x000AEAD4
		private void SynthesizeClosureEnvironments(ArrayBuilder<ClosureDebugInfo> closureDebugInfo)
		{
			ClosureConversion.Analysis.VisitScopeTree(this._analysis.ScopeTree, delegate(ClosureConversion.Analysis.Scope scope)
			{
				ClosureConversion.Analysis.ClosureEnvironment declaredEnvironment = scope.DeclaredEnvironment;
				if (declaredEnvironment != null)
				{
					SynthesizedClosureEnvironment synthesizedClosureEnvironment = base.<SynthesizeClosureEnvironments>g__MakeFrame|1(scope, declaredEnvironment);
					declaredEnvironment.SynthesizedEnvironment = synthesizedClosureEnvironment;
					this.CompilationState.ModuleBuilderOpt.AddSynthesizedDefinition(this.ContainingType, synthesizedClosureEnvironment.GetCciAdapter());
					if (synthesizedClosureEnvironment.Constructor != null)
					{
						this.AddSynthesizedMethod(synthesizedClosureEnvironment.Constructor, FlowAnalysisPass.AppendImplicitReturn(MethodCompiler.BindMethodBody(synthesizedClosureEnvironment.Constructor, this.CompilationState, this.Diagnostics), synthesizedClosureEnvironment.Constructor));
					}
					this._frames.Add(scope.BoundNode, declaredEnvironment);
				}
			});
		}

		// Token: 0x060021C4 RID: 8644 RVA: 0x000B0911 File Offset: 0x000AEB11
		private void SynthesizeClosureMethods()
		{
			ClosureConversion.Analysis.VisitNestedFunctions(this._analysis.ScopeTree, delegate(ClosureConversion.Analysis.Scope scope, ClosureConversion.Analysis.NestedFunction nestedFunction)
			{
				MethodSymbol originalMethodSymbol = nestedFunction.OriginalMethodSymbol;
				SyntaxNode syntax = originalMethodSymbol.DeclaringSyntaxReferences[0].GetSyntax(default(CancellationToken));
				ClosureKind closureKind;
				NamedTypeSymbol namedTypeSymbol;
				int num;
				if (nestedFunction.ContainingEnvironmentOpt != null)
				{
					SynthesizedClosureEnvironment synthesizedEnvironment = nestedFunction.ContainingEnvironmentOpt.SynthesizedEnvironment;
					closureKind = ClosureKind.General;
					namedTypeSymbol = synthesizedEnvironment;
					num = synthesizedEnvironment.ClosureOrdinal;
				}
				else if (nestedFunction.CapturesThis)
				{
					namedTypeSymbol = this._topLevelMethod.ContainingType;
					closureKind = ClosureKind.ThisOnly;
					num = -2;
				}
				else if ((nestedFunction.CapturedEnvironments.Count == 0 && originalMethodSymbol.MethodKind == MethodKind.AnonymousFunction && this._analysis.MethodsConvertedToDelegates.Contains(originalMethodSymbol)) || VarianceSafety.GetEnclosingVariantInterface(this._topLevelMethod) != null)
				{
					namedTypeSymbol = this.GetStaticFrame(this.Diagnostics, syntax);
					closureKind = ClosureKind.Singleton;
					num = -1;
				}
				else
				{
					namedTypeSymbol = this._topLevelMethod.ContainingType;
					closureKind = ClosureKind.Static;
					num = -1;
				}
				DebugId topLevelMethodId = this._analysis.GetTopLevelMethodId();
				DebugId lambdaId = this.GetLambdaId(syntax, closureKind, num);
				SynthesizedClosureMethod synthesizedClosureMethod = new SynthesizedClosureMethod(namedTypeSymbol, ClosureConversion.<SynthesizeClosureMethods>g__getStructEnvironments|36_1(nestedFunction), closureKind, this._topLevelMethod, topLevelMethodId, originalMethodSymbol, nestedFunction.BlockSyntax, lambdaId);
				nestedFunction.SynthesizedLoweredMethod = synthesizedClosureMethod;
			});
		}

		// Token: 0x060021C5 RID: 8645 RVA: 0x000B0930 File Offset: 0x000AEB30
		private SynthesizedClosureEnvironment GetStaticFrame(BindingDiagnosticBag diagnostics, SyntaxNode syntax)
		{
			if (this._lazyStaticLambdaFrame == null)
			{
				bool flag = !this._topLevelMethod.IsGenericMethod;
				if (flag)
				{
					this._lazyStaticLambdaFrame = this.CompilationState.StaticLambdaFrame;
				}
				if (this._lazyStaticLambdaFrame == null)
				{
					DebugId topLevelMethodId;
					if (flag)
					{
						topLevelMethodId = new DebugId(-1, this.CompilationState.ModuleBuilderOpt.CurrentGenerationOrdinal);
					}
					else
					{
						topLevelMethodId = this._analysis.GetTopLevelMethodId();
					}
					DebugId debugId = default(DebugId);
					MethodSymbol methodSymbol = (flag ? null : (this._substitutedSourceMethod ?? this._topLevelMethod));
					this._lazyStaticLambdaFrame = new SynthesizedClosureEnvironment(this._topLevelMethod, methodSymbol, false, null, topLevelMethodId, debugId);
					if (flag)
					{
						this.CompilationState.StaticLambdaFrame = this._lazyStaticLambdaFrame;
					}
					SynthesizedClosureEnvironment lazyStaticLambdaFrame = this._lazyStaticLambdaFrame;
					this.CompilationState.ModuleBuilderOpt.AddSynthesizedDefinition(this.ContainingType, lazyStaticLambdaFrame.GetCciAdapter());
					this.AddSynthesizedMethod(lazyStaticLambdaFrame.Constructor, FlowAnalysisPass.AppendImplicitReturn(MethodCompiler.BindMethodBody(lazyStaticLambdaFrame.Constructor, this.CompilationState, diagnostics), lazyStaticLambdaFrame.Constructor));
					SyntheticBoundNodeFactory syntheticBoundNodeFactory = new SyntheticBoundNodeFactory(lazyStaticLambdaFrame.StaticConstructor, syntax, this.CompilationState, diagnostics);
					BoundBlock boundBlock = syntheticBoundNodeFactory.Block(new BoundStatement[]
					{
						syntheticBoundNodeFactory.Assignment(syntheticBoundNodeFactory.Field(null, lazyStaticLambdaFrame.SingletonCache), syntheticBoundNodeFactory.New(lazyStaticLambdaFrame.Constructor, Array.Empty<BoundExpression>()), false),
						new BoundReturnStatement(syntax, RefKind.None, null, false)
					});
					this.AddSynthesizedMethod(lazyStaticLambdaFrame.StaticConstructor, boundBlock);
				}
			}
			return this._lazyStaticLambdaFrame;
		}

		// Token: 0x060021C6 RID: 8646 RVA: 0x000B0AAC File Offset: 0x000AECAC
		private BoundExpression FrameOfType(SyntaxNode syntax, NamedTypeSymbol frameType)
		{
			return this.FramePointer(syntax, frameType.OriginalDefinition);
		}

		// Token: 0x060021C7 RID: 8647 RVA: 0x000B0ABC File Offset: 0x000AECBC
		protected override BoundExpression FramePointer(SyntaxNode syntax, NamedTypeSymbol frameClass)
		{
			if (this._currentFrameThis != null && TypeSymbol.Equals(this._currentFrameThis.Type, frameClass, TypeCompareKind.ConsiderEverything))
			{
				return new BoundThisReference(syntax, frameClass);
			}
			SynthesizedClosureMethod synthesizedClosureMethod = this._currentMethod as SynthesizedClosureMethod;
			if (synthesizedClosureMethod != null)
			{
				for (int i = synthesizedClosureMethod.ParameterCount - synthesizedClosureMethod.ExtraSynthesizedParameterCount; i < synthesizedClosureMethod.ParameterCount; i++)
				{
					ParameterSymbol parameterSymbol = synthesizedClosureMethod.Parameters[i];
					if (TypeSymbol.Equals(parameterSymbol.Type.OriginalDefinition, frameClass, TypeCompareKind.ConsiderEverything))
					{
						return new BoundParameter(syntax, parameterSymbol);
					}
				}
			}
			Symbol symbol = this._framePointers[frameClass];
			CapturedSymbolReplacement capturedSymbolReplacement;
			if (this.proxies.TryGetValue(symbol, ref capturedSymbolReplacement))
			{
				return capturedSymbolReplacement.Replacement(syntax, (NamedTypeSymbol frameType) => this.FramePointer(syntax, frameType));
			}
			LocalSymbol localSymbol = (LocalSymbol)symbol;
			return new BoundLocal(syntax, localSymbol, null, localSymbol.Type, false);
		}

		// Token: 0x060021C8 RID: 8648 RVA: 0x000B0BC8 File Offset: 0x000AEDC8
		private static void InsertAndFreePrologue<T>(ArrayBuilder<BoundStatement> result, ArrayBuilder<T> prologue) where T : BoundNode
		{
			foreach (T t in prologue)
			{
				BoundStatement boundStatement = t as BoundStatement;
				if (boundStatement != null)
				{
					result.Add(boundStatement);
				}
				else
				{
					result.Add(new BoundExpressionStatement(t.Syntax, (BoundExpression)((object)t), false));
				}
			}
			prologue.Free();
		}

		// Token: 0x060021C9 RID: 8649 RVA: 0x000B0C30 File Offset: 0x000AEE30
		private BoundNode IntroduceFrame(BoundNode node, ClosureConversion.Analysis.ClosureEnvironment env, Func<ArrayBuilder<BoundExpression>, ArrayBuilder<LocalSymbol>, BoundNode> F)
		{
			SynthesizedClosureEnvironment synthesizedEnvironment = env.SynthesizedEnvironment;
			ImmutableArray<TypeWithAnnotations> immutableArray = ImmutableArray.Create<TypeWithAnnotations>(this._currentTypeParameters.SelectAsArray<TypeParameterSymbol, TypeWithAnnotations>((TypeParameterSymbol t) => TypeWithAnnotations.Create(t, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>))), 0, synthesizedEnvironment.Arity);
			NamedTypeSymbol namedTypeSymbol = synthesizedEnvironment.ConstructIfGeneric(immutableArray);
			LocalSymbol localSymbol = new SynthesizedLocal(this._topLevelMethod, TypeWithAnnotations.Create(namedTypeSymbol, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), SynthesizedLocalKind.LambdaDisplayClass, synthesizedEnvironment.ScopeSyntaxOpt, false, RefKind.None);
			SyntaxNode syntax = node.Syntax;
			ArrayBuilder<BoundExpression> instance = ArrayBuilder<BoundExpression>.GetInstance();
			if (synthesizedEnvironment.Constructor != null)
			{
				MethodSymbol methodSymbol = synthesizedEnvironment.Constructor.AsMember(namedTypeSymbol);
				instance.Add(new BoundAssignmentOperator(syntax, new BoundLocal(syntax, localSymbol, null, namedTypeSymbol, false), new BoundObjectCreationExpression(syntax, methodSymbol, Array.Empty<BoundExpression>()), namedTypeSymbol, false, false));
			}
			CapturedSymbolReplacement capturedSymbolReplacement = null;
			if (this._innermostFramePointer != null)
			{
				this.proxies.TryGetValue(this._innermostFramePointer, ref capturedSymbolReplacement);
				if (env.CapturesParent)
				{
					LambdaCapturedVariable lambdaCapturedVariable = LambdaCapturedVariable.Create(synthesizedEnvironment, this._innermostFramePointer, ref this._synthesizedFieldNameIdDispenser);
					FieldSymbol fieldSymbol = lambdaCapturedVariable.AsMember(namedTypeSymbol);
					BoundExpression boundExpression = new BoundFieldAccess(syntax, new BoundLocal(syntax, localSymbol, null, namedTypeSymbol, false), fieldSymbol, null, false);
					BoundExpression boundExpression2 = this.FrameOfType(syntax, fieldSymbol.Type as NamedTypeSymbol);
					BoundExpression boundExpression3 = new BoundAssignmentOperator(syntax, boundExpression, boundExpression2, boundExpression.Type, false, false);
					instance.Add(boundExpression3);
					if (this.CompilationState.Emitting)
					{
						synthesizedEnvironment.AddHoistedField(lambdaCapturedVariable);
						this.CompilationState.ModuleBuilderOpt.AddSynthesizedDefinition(synthesizedEnvironment, lambdaCapturedVariable.GetCciAdapter());
					}
					this.proxies[this._innermostFramePointer] = new CapturedToFrameSymbolReplacement(lambdaCapturedVariable, false);
				}
			}
			foreach (Symbol symbol in env.CapturedVariables)
			{
				this.InitVariableProxy(syntax, symbol, localSymbol, instance);
			}
			Symbol innermostFramePointer = this._innermostFramePointer;
			if (!localSymbol.Type.IsValueType)
			{
				this._innermostFramePointer = localSymbol;
			}
			ArrayBuilder<LocalSymbol> instance2 = ArrayBuilder<LocalSymbol>.GetInstance();
			instance2.Add(localSymbol);
			this._framePointers.Add(synthesizedEnvironment, localSymbol);
			BoundNode boundNode = F.Invoke(instance, instance2);
			this._innermostFramePointer = innermostFramePointer;
			if (this._innermostFramePointer != null)
			{
				if (capturedSymbolReplacement != null)
				{
					this.proxies[this._innermostFramePointer] = capturedSymbolReplacement;
					return boundNode;
				}
				this.proxies.Remove(this._innermostFramePointer);
			}
			return boundNode;
		}

		// Token: 0x060021CA RID: 8650 RVA: 0x000B0EA0 File Offset: 0x000AF0A0
		private void InitVariableProxy(SyntaxNode syntax, Symbol symbol, LocalSymbol framePointer, ArrayBuilder<BoundExpression> prologue)
		{
			CapturedSymbolReplacement capturedSymbolReplacement;
			if (this.proxies.TryGetValue(symbol, ref capturedSymbolReplacement))
			{
				SymbolKind kind = symbol.Kind;
				BoundExpression boundExpression;
				if (kind != SymbolKind.Local)
				{
					if (kind != SymbolKind.Parameter)
					{
						throw ExceptionUtilities.UnexpectedValue(symbol.Kind);
					}
					ParameterSymbol parameterSymbol = (ParameterSymbol)symbol;
					ParameterSymbol parameterSymbol2;
					if (!this._parameterMap.TryGetValue(parameterSymbol, ref parameterSymbol2))
					{
						parameterSymbol2 = parameterSymbol;
					}
					boundExpression = new BoundParameter(syntax, parameterSymbol2);
				}
				else
				{
					LocalSymbol localSymbol = (LocalSymbol)symbol;
					if (this._assignLocals == null || !this._assignLocals.Contains(localSymbol))
					{
						return;
					}
					LocalSymbol localSymbol2;
					if (!this.localMap.TryGetValue(localSymbol, ref localSymbol2))
					{
						localSymbol2 = localSymbol;
					}
					boundExpression = new BoundLocal(syntax, localSymbol2, null, localSymbol2.Type, false);
				}
				BoundExpression boundExpression2 = capturedSymbolReplacement.Replacement(syntax, (NamedTypeSymbol frameType1) => new BoundLocal(syntax, framePointer, null, framePointer.Type, false));
				BoundAssignmentOperator boundAssignmentOperator = new BoundAssignmentOperator(syntax, boundExpression2, boundExpression, boundExpression.Type, false, false);
				if (this._currentMethod.MethodKind == MethodKind.Constructor && symbol == this._currentMethod.ThisParameter && !this._seenBaseCall)
				{
					this._thisProxyInitDeferred = boundAssignmentOperator;
					return;
				}
				prologue.Add(boundAssignmentOperator);
			}
		}

		// Token: 0x060021CB RID: 8651 RVA: 0x000B0FE0 File Offset: 0x000AF1E0
		protected override BoundNode VisitUnhoistedParameter(BoundParameter node)
		{
			ParameterSymbol parameterSymbol;
			if (this._parameterMap.TryGetValue(node.ParameterSymbol, ref parameterSymbol))
			{
				return new BoundParameter(node.Syntax, parameterSymbol, node.HasErrors);
			}
			return base.VisitUnhoistedParameter(node);
		}

		// Token: 0x060021CC RID: 8652 RVA: 0x000B101C File Offset: 0x000AF21C
		public override BoundNode VisitThisReference(BoundThisReference node)
		{
			if (!(this._currentMethod == this._topLevelMethod) && !(this._topLevelMethod.ThisParameter == null))
			{
				return this.FramePointer(node.Syntax, (NamedTypeSymbol)node.Type);
			}
			return node;
		}

		// Token: 0x060021CD RID: 8653 RVA: 0x000B1068 File Offset: 0x000AF268
		public override BoundNode VisitBaseReference(BoundBaseReference node)
		{
			if (this._currentMethod.IsStatic || !TypeSymbol.Equals(this._currentMethod.ContainingType, this._topLevelMethod.ContainingType, TypeCompareKind.ConsiderEverything))
			{
				return this.FramePointer(node.Syntax, this._topLevelMethod.ContainingType);
			}
			return node;
		}

		// Token: 0x060021CE RID: 8654 RVA: 0x000B10BC File Offset: 0x000AF2BC
		private void RemapLocalFunction(SyntaxNode syntax, MethodSymbol localFunc, out BoundExpression receiver, out MethodSymbol method, ref ImmutableArray<BoundExpression> arguments, ref ImmutableArray<RefKind> argRefKinds)
		{
			SynthesizedClosureMethod synthesizedLoweredMethod = ClosureConversion.Analysis.GetNestedFunctionInTree(this._analysis.ScopeTree, localFunc.OriginalDefinition).SynthesizedLoweredMethod;
			int extraSynthesizedParameterCount = synthesizedLoweredMethod.ExtraSynthesizedParameterCount;
			if (extraSynthesizedParameterCount != 0)
			{
				ArrayBuilder<BoundExpression> instance = ArrayBuilder<BoundExpression>.GetInstance(synthesizedLoweredMethod.ParameterCount);
				instance.AddRange(arguments);
				for (int i = synthesizedLoweredMethod.ParameterCount - extraSynthesizedParameterCount; i < synthesizedLoweredMethod.ParameterCount; i++)
				{
					NamedTypeSymbol namedTypeSymbol = (NamedTypeSymbol)synthesizedLoweredMethod.Parameters[i].Type.OriginalDefinition;
					if (namedTypeSymbol.Arity > 0)
					{
						ImmutableArray<TypeParameterSymbol> constructedFromTypeParameters = ((SynthesizedClosureEnvironment)namedTypeSymbol).ConstructedFromTypeParameters;
						ImmutableArray<TypeParameterSymbol> immutableArray = this.TypeMap.SubstituteTypeParameters(constructedFromTypeParameters);
						namedTypeSymbol = namedTypeSymbol.Construct(immutableArray);
					}
					BoundExpression boundExpression = this.FrameOfType(syntax, namedTypeSymbol);
					instance.Add(boundExpression);
				}
				ArrayBuilder<RefKind> instance2 = ArrayBuilder<RefKind>.GetInstance(instance.Count);
				if (!argRefKinds.IsDefault)
				{
					instance2.AddRange(argRefKinds);
				}
				else
				{
					instance2.AddMany(RefKind.None, arguments.Length);
				}
				instance2.AddMany(RefKind.Ref, extraSynthesizedParameterCount);
				arguments = instance.ToImmutableAndFree();
				argRefKinds = instance2.ToImmutableAndFree();
			}
			method = synthesizedLoweredMethod;
			NamedTypeSymbol namedTypeSymbol2;
			this.RemapLambdaOrLocalFunction(syntax, localFunc, this.SubstituteTypeArguments(localFunc.TypeArgumentsWithAnnotations), synthesizedLoweredMethod.ClosureKind, ref method, out receiver, out namedTypeSymbol2);
		}

		// Token: 0x060021CF RID: 8655 RVA: 0x000B1210 File Offset: 0x000AF410
		private ImmutableArray<TypeWithAnnotations> SubstituteTypeArguments(ImmutableArray<TypeWithAnnotations> typeArguments)
		{
			if (typeArguments.IsEmpty)
			{
				return typeArguments;
			}
			ArrayBuilder<TypeWithAnnotations> instance = ArrayBuilder<TypeWithAnnotations>.GetInstance(typeArguments.Length);
			foreach (TypeWithAnnotations typeWithAnnotations in typeArguments)
			{
				TypeWithAnnotations typeWithAnnotations2;
				do
				{
					typeWithAnnotations2 = typeWithAnnotations;
					typeWithAnnotations = this.TypeMap.SubstituteType(typeWithAnnotations2);
				}
				while (!TypeSymbol.Equals(typeWithAnnotations2.Type, typeWithAnnotations.Type, TypeCompareKind.ConsiderEverything));
				instance.Add(typeWithAnnotations);
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x060021D0 RID: 8656 RVA: 0x000B1284 File Offset: 0x000AF484
		private void RemapLambdaOrLocalFunction(SyntaxNode syntax, MethodSymbol originalMethod, ImmutableArray<TypeWithAnnotations> typeArgumentsOpt, ClosureKind closureKind, ref MethodSymbol synthesizedMethod, out BoundExpression receiver, out NamedTypeSymbol constructedFrame)
		{
			NamedTypeSymbol containingType = synthesizedMethod.ContainingType;
			SynthesizedClosureEnvironment synthesizedClosureEnvironment = containingType as SynthesizedClosureEnvironment;
			int num = ((synthesizedClosureEnvironment != null) ? synthesizedClosureEnvironment.Arity : 0) + synthesizedMethod.Arity;
			ImmutableArray<TypeWithAnnotations> immutableArray = ImmutableArray.Create<TypeWithAnnotations>(this._currentTypeParameters.SelectAsArray<TypeParameterSymbol, TypeWithAnnotations>((TypeParameterSymbol t) => TypeWithAnnotations.Create(t, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>))), 0, num - originalMethod.Arity);
			if (!typeArgumentsOpt.IsDefault)
			{
				immutableArray = immutableArray.Concat<TypeWithAnnotations>(typeArgumentsOpt);
			}
			if (synthesizedClosureEnvironment != null && synthesizedClosureEnvironment.Arity != 0)
			{
				ImmutableArray<TypeWithAnnotations> immutableArray2 = ImmutableArray.Create<TypeWithAnnotations>(immutableArray, 0, synthesizedClosureEnvironment.Arity);
				immutableArray = ImmutableArray.Create<TypeWithAnnotations>(immutableArray, synthesizedClosureEnvironment.Arity, immutableArray.Length - synthesizedClosureEnvironment.Arity);
				constructedFrame = synthesizedClosureEnvironment.Construct(immutableArray2);
			}
			else
			{
				constructedFrame = containingType;
			}
			synthesizedMethod = synthesizedMethod.AsMember(constructedFrame);
			if (synthesizedMethod.IsGenericMethod)
			{
				synthesizedMethod = synthesizedMethod.Construct(immutableArray);
			}
			if (closureKind == ClosureKind.Singleton)
			{
				FieldSymbol fieldSymbol = synthesizedClosureEnvironment.SingletonCache.AsMember(constructedFrame);
				receiver = new BoundFieldAccess(syntax, null, fieldSymbol, null, false);
				return;
			}
			if (closureKind == ClosureKind.Static)
			{
				receiver = new BoundTypeExpression(syntax, null, synthesizedMethod.ContainingType, false);
				return;
			}
			receiver = this.FrameOfType(syntax, constructedFrame);
		}

		// Token: 0x060021D1 RID: 8657 RVA: 0x000B13B4 File Offset: 0x000AF5B4
		public override BoundNode VisitCall(BoundCall node)
		{
			if (node.Method.MethodKind == MethodKind.LocalFunction)
			{
				ImmutableArray<BoundExpression> immutableArray = base.VisitList<BoundExpression>(node.Arguments);
				ImmutableArray<RefKind> argumentRefKindsOpt = node.ArgumentRefKindsOpt;
				TypeSymbol typeSymbol = this.VisitType(node.Type);
				BoundExpression boundExpression;
				MethodSymbol methodSymbol;
				this.RemapLocalFunction(node.Syntax, node.Method, out boundExpression, out methodSymbol, ref immutableArray, ref argumentRefKindsOpt);
				return node.Update(boundExpression, methodSymbol, immutableArray, node.ArgumentNamesOpt, argumentRefKindsOpt, node.IsDelegateCall, node.Expanded, node.InvokedAsExtensionMethod, node.ArgsToParamsOpt, node.DefaultArguments, node.ResultKind, typeSymbol);
			}
			BoundNode boundNode = base.VisitCall(node);
			if (boundNode.Kind != BoundKind.Call)
			{
				return boundNode;
			}
			BoundCall boundCall = (BoundCall)boundNode;
			if (!this._seenBaseCall && this._currentMethod == this._topLevelMethod && node.IsConstructorInitializer())
			{
				this._seenBaseCall = true;
				if (this._thisProxyInitDeferred != null)
				{
					return new BoundSequence(node.Syntax, ImmutableArray<LocalSymbol>.Empty, ImmutableArray.Create<BoundExpression>(boundCall), this._thisProxyInitDeferred, boundCall.Type, false);
				}
			}
			return boundCall;
		}

		// Token: 0x060021D2 RID: 8658 RVA: 0x000B14BC File Offset: 0x000AF6BC
		private BoundSequence RewriteSequence(BoundSequence node, ArrayBuilder<BoundExpression> prologue, ArrayBuilder<LocalSymbol> newLocals)
		{
			base.RewriteLocals(node.Locals, newLocals);
			foreach (BoundExpression boundExpression in node.SideEffects)
			{
				BoundExpression boundExpression2 = (BoundExpression)this.Visit(boundExpression);
				if (boundExpression2 != null)
				{
					prologue.Add(boundExpression2);
				}
			}
			BoundExpression boundExpression3 = (BoundExpression)this.Visit(node.Value);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(newLocals.ToImmutableAndFree(), prologue.ToImmutableAndFree(), boundExpression3, typeSymbol);
		}

		// Token: 0x060021D3 RID: 8659 RVA: 0x000B1548 File Offset: 0x000AF748
		public override BoundNode VisitBlock(BoundBlock node)
		{
			ClosureConversion.Analysis.ClosureEnvironment closureEnvironment;
			if (this._frames.TryGetValue(node, ref closureEnvironment))
			{
				return this.IntroduceFrame(node, closureEnvironment, (ArrayBuilder<BoundExpression> prologue, ArrayBuilder<LocalSymbol> newLocals) => this.RewriteBlock(node, prologue, newLocals));
			}
			return this.RewriteBlock(node, ArrayBuilder<BoundExpression>.GetInstance(), ArrayBuilder<LocalSymbol>.GetInstance());
		}

		// Token: 0x060021D4 RID: 8660 RVA: 0x000B15B0 File Offset: 0x000AF7B0
		private BoundBlock RewriteBlock(BoundBlock node, ArrayBuilder<BoundExpression> prologue, ArrayBuilder<LocalSymbol> newLocals)
		{
			base.RewriteLocals(node.Locals, newLocals);
			ArrayBuilder<BoundStatement> instance = ArrayBuilder<BoundStatement>.GetInstance();
			if (prologue.Count > 0)
			{
				instance.Add(BoundSequencePoint.CreateHidden(null, false));
			}
			ClosureConversion.InsertAndFreePrologue<BoundExpression>(instance, prologue);
			foreach (BoundStatement boundStatement in node.Statements)
			{
				BoundStatement boundStatement2 = (BoundStatement)this.Visit(boundStatement);
				if (boundStatement2 != null)
				{
					instance.Add(boundStatement2);
				}
			}
			return node.Update(newLocals.ToImmutableAndFree(), node.LocalFunctions, instance.ToImmutableAndFree());
		}

		// Token: 0x060021D5 RID: 8661 RVA: 0x000B1644 File Offset: 0x000AF844
		public override BoundNode VisitScope(BoundScope node)
		{
			ArrayBuilder<LocalSymbol> instance = ArrayBuilder<LocalSymbol>.GetInstance();
			base.RewriteLocals(node.Locals, instance);
			ImmutableArray<BoundStatement> immutableArray = base.VisitList<BoundStatement>(node.Statements);
			if (instance.Count == 0)
			{
				instance.Free();
				return new BoundStatementList(node.Syntax, immutableArray, false);
			}
			return node.Update(instance.ToImmutableAndFree(), immutableArray);
		}

		// Token: 0x060021D6 RID: 8662 RVA: 0x000B169C File Offset: 0x000AF89C
		public override BoundNode VisitCatchBlock(BoundCatchBlock node)
		{
			ClosureConversion.Analysis.ClosureEnvironment closureEnvironment;
			if (this._frames.TryGetValue(node, ref closureEnvironment))
			{
				return this.IntroduceFrame(node, closureEnvironment, (ArrayBuilder<BoundExpression> prologue, ArrayBuilder<LocalSymbol> newLocals) => this.RewriteCatch(node, prologue, newLocals));
			}
			return this.RewriteCatch(node, ArrayBuilder<BoundExpression>.GetInstance(), ArrayBuilder<LocalSymbol>.GetInstance());
		}

		// Token: 0x060021D7 RID: 8663 RVA: 0x000B1704 File Offset: 0x000AF904
		private BoundNode RewriteCatch(BoundCatchBlock node, ArrayBuilder<BoundExpression> prologue, ArrayBuilder<LocalSymbol> newLocals)
		{
			base.RewriteLocals(node.Locals, newLocals);
			ImmutableArray<LocalSymbol> immutableArray = newLocals.ToImmutableAndFree();
			BoundExpression boundExpression = null;
			BoundStatementList boundStatementList = (BoundStatementList)this.Visit(node.ExceptionFilterPrologueOpt);
			BoundExpression boundExpression2 = (BoundExpression)this.Visit(node.ExceptionFilterOpt);
			if (node.ExceptionSourceOpt != null)
			{
				boundExpression = (BoundExpression)this.Visit(node.ExceptionSourceOpt);
				if (prologue.Count > 0)
				{
					boundExpression = new BoundSequence(boundExpression.Syntax, ImmutableArray.Create<LocalSymbol>(), prologue.ToImmutable(), boundExpression, boundExpression.Type, false);
				}
			}
			else if (prologue.Count > 0)
			{
				ArrayBuilder<BoundStatement> instance = ArrayBuilder<BoundStatement>.GetInstance(prologue.Count);
				foreach (BoundExpression boundExpression3 in prologue)
				{
					instance.Add(new BoundExpressionStatement(boundExpression3.Syntax, boundExpression3, false)
					{
						WasCompilerGenerated = true
					});
				}
				if (boundStatementList != null)
				{
					instance.AddRange(boundStatementList.Statements);
				}
				boundStatementList = new BoundStatementList(boundExpression2.Syntax, instance.ToImmutableAndFree(), false);
			}
			prologue.Free();
			TypeSymbol typeSymbol = this.VisitType(node.ExceptionTypeOpt);
			BoundBlock boundBlock = (BoundBlock)this.Visit(node.Body);
			return node.Update(immutableArray, boundExpression, typeSymbol, boundStatementList, boundExpression2, boundBlock, node.IsSynthesizedAsyncCatchAll);
		}

		// Token: 0x060021D8 RID: 8664 RVA: 0x000B1840 File Offset: 0x000AFA40
		public override BoundNode VisitSequence(BoundSequence node)
		{
			ClosureConversion.Analysis.ClosureEnvironment closureEnvironment;
			if (this._frames.TryGetValue(node, ref closureEnvironment))
			{
				return this.IntroduceFrame(node, closureEnvironment, (ArrayBuilder<BoundExpression> prologue, ArrayBuilder<LocalSymbol> newLocals) => this.RewriteSequence(node, prologue, newLocals));
			}
			return this.RewriteSequence(node, ArrayBuilder<BoundExpression>.GetInstance(), ArrayBuilder<LocalSymbol>.GetInstance());
		}

		// Token: 0x060021D9 RID: 8665 RVA: 0x000B18A8 File Offset: 0x000AFAA8
		public override BoundNode VisitStatementList(BoundStatementList node)
		{
			ClosureConversion.Analysis.ClosureEnvironment closureEnvironment;
			if (this._frames.TryGetValue(node, ref closureEnvironment))
			{
				return this.IntroduceFrame(node, closureEnvironment, delegate(ArrayBuilder<BoundExpression> prologue, ArrayBuilder<LocalSymbol> newLocals)
				{
					ArrayBuilder<BoundStatement> instance = ArrayBuilder<BoundStatement>.GetInstance();
					ClosureConversion.InsertAndFreePrologue<BoundExpression>(instance, prologue);
					foreach (BoundStatement boundStatement in node.Statements)
					{
						instance.Add((BoundStatement)this.Visit(boundStatement));
					}
					return new BoundBlock(node.Syntax, newLocals.ToImmutableAndFree(), instance.ToImmutableAndFree(), node.HasErrors);
				});
			}
			return base.VisitStatementList(node);
		}

		// Token: 0x060021DA RID: 8666 RVA: 0x000B1904 File Offset: 0x000AFB04
		public override BoundNode VisitDelegateCreationExpression(BoundDelegateCreationExpression node)
		{
			if (node.Argument.Kind == BoundKind.Lambda)
			{
				return this.RewriteLambdaConversion((BoundLambda)node.Argument);
			}
			MethodSymbol methodOpt = node.MethodOpt;
			if (methodOpt != null && methodOpt.MethodKind == MethodKind.LocalFunction)
			{
				ImmutableArray<BoundExpression> immutableArray = default(ImmutableArray<BoundExpression>);
				ImmutableArray<RefKind> immutableArray2 = default(ImmutableArray<RefKind>);
				BoundExpression boundExpression;
				MethodSymbol methodSymbol;
				this.RemapLocalFunction(node.Syntax, node.MethodOpt, out boundExpression, out methodSymbol, ref immutableArray, ref immutableArray2);
				return new BoundDelegateCreationExpression(node.Syntax, boundExpression, methodSymbol, node.IsExtensionMethod, this.VisitType(node.Type), false);
			}
			return base.VisitDelegateCreationExpression(node);
		}

		// Token: 0x060021DB RID: 8667 RVA: 0x000B19A0 File Offset: 0x000AFBA0
		public override BoundNode VisitFunctionPointerLoad(BoundFunctionPointerLoad node)
		{
			if (node.TargetMethod.MethodKind == MethodKind.LocalFunction)
			{
				ImmutableArray<BoundExpression> immutableArray = default(ImmutableArray<BoundExpression>);
				ImmutableArray<RefKind> immutableArray2 = default(ImmutableArray<RefKind>);
				BoundExpression boundExpression;
				MethodSymbol methodSymbol;
				this.RemapLocalFunction(node.Syntax, node.TargetMethod, out boundExpression, out methodSymbol, ref immutableArray, ref immutableArray2);
				return node.Update(methodSymbol, node.Type);
			}
			return base.VisitFunctionPointerLoad(node);
		}

		// Token: 0x060021DC RID: 8668 RVA: 0x000B19FC File Offset: 0x000AFBFC
		public override BoundNode VisitConversion(BoundConversion conversion)
		{
			if (conversion.ConversionKind == ConversionKind.AnonymousFunction)
			{
				BoundExpression boundExpression = (BoundExpression)this.RewriteLambdaConversion((BoundLambda)conversion.Operand);
				if (this._inExpressionLambda && conversion.ExplicitCastInCode)
				{
					SyntaxNode syntax = conversion.Syntax;
					BoundExpression boundExpression2 = boundExpression;
					Conversion conversion2 = conversion.Conversion;
					bool flag = false;
					bool flag2 = false;
					bool flag3 = true;
					ConversionGroup conversionGroupOpt = conversion.ConversionGroupOpt;
					boundExpression = new BoundConversion(syntax, boundExpression2, conversion2, flag, flag2, flag3, conversion.ConstantValueOpt, conversionGroupOpt, conversion.Type, false);
				}
				return boundExpression;
			}
			return base.VisitConversion(conversion);
		}

		// Token: 0x060021DD RID: 8669 RVA: 0x000B1A70 File Offset: 0x000AFC70
		public override BoundNode VisitLocalFunctionStatement(BoundLocalFunctionStatement node)
		{
			ClosureKind closureKind;
			NamedTypeSymbol namedTypeSymbol;
			SynthesizedClosureEnvironment synthesizedClosureEnvironment;
			BoundNode boundNode;
			DebugId debugId;
			DebugId debugId2;
			this.RewriteLambdaOrLocalFunction(node, out closureKind, out namedTypeSymbol, out synthesizedClosureEnvironment, out boundNode, out debugId, out debugId2);
			return new BoundNoOpStatement(node.Syntax, NoOpStatementFlavor.Default);
		}

		// Token: 0x060021DE RID: 8670 RVA: 0x000B1AA0 File Offset: 0x000AFCA0
		private DebugId GetLambdaId(SyntaxNode syntax, ClosureKind closureKind, int closureOrdinal)
		{
			AnonymousFunctionExpressionSyntax anonymousFunctionExpressionSyntax = syntax as AnonymousFunctionExpressionSyntax;
			SyntaxNode syntaxNode;
			bool flag;
			if (anonymousFunctionExpressionSyntax != null)
			{
				syntaxNode = anonymousFunctionExpressionSyntax.Body;
				flag = true;
			}
			else
			{
				LocalFunctionStatementSyntax localFunctionStatementSyntax = syntax as LocalFunctionStatementSyntax;
				if (localFunctionStatementSyntax != null)
				{
					BlockSyntax blockSyntax;
					if ((blockSyntax = localFunctionStatementSyntax.Body) == null)
					{
						ArrowExpressionClauseSyntax expressionBody = localFunctionStatementSyntax.ExpressionBody;
						blockSyntax = ((expressionBody != null) ? expressionBody.Expression : null);
					}
					syntaxNode = blockSyntax;
					if (syntaxNode == null)
					{
						syntaxNode = localFunctionStatementSyntax;
						flag = false;
					}
					else
					{
						flag = true;
					}
				}
				else if (LambdaUtilities.IsQueryPairLambda(syntax))
				{
					syntaxNode = syntax;
					flag = false;
				}
				else
				{
					syntaxNode = syntax;
					flag = true;
				}
			}
			DebugId debugId;
			DebugId debugId2;
			if (this.slotAllocatorOpt != null && this.slotAllocatorOpt.TryGetPreviousLambda(syntaxNode, flag, out debugId))
			{
				debugId2 = debugId;
			}
			else
			{
				debugId2 = new DebugId(this._lambdaDebugInfoBuilder.Count, this.CompilationState.ModuleBuilderOpt.CurrentGenerationOrdinal);
			}
			int num = this._topLevelMethod.CalculateLocalSyntaxOffset(LambdaUtilities.GetDeclaratorPosition(syntaxNode), syntaxNode.SyntaxTree);
			this._lambdaDebugInfoBuilder.Add(new LambdaDebugInfo(num, debugId2, closureOrdinal));
			return debugId2;
		}

		// Token: 0x060021DF RID: 8671 RVA: 0x000B1B7C File Offset: 0x000AFD7C
		private SynthesizedClosureMethod RewriteLambdaOrLocalFunction(IBoundLambdaOrFunction node, out ClosureKind closureKind, out NamedTypeSymbol translatedLambdaContainer, out SynthesizedClosureEnvironment containerAsFrame, out BoundNode lambdaScope, out DebugId topLevelMethodId, out DebugId lambdaId)
		{
			ClosureConversion.Analysis.NestedFunction function = ClosureConversion.Analysis.GetNestedFunctionInTree(this._analysis.ScopeTree, node.Symbol);
			SynthesizedClosureMethod synthesizedLoweredMethod = function.SynthesizedLoweredMethod;
			closureKind = synthesizedLoweredMethod.ClosureKind;
			translatedLambdaContainer = synthesizedLoweredMethod.ContainingType;
			containerAsFrame = translatedLambdaContainer as SynthesizedClosureEnvironment;
			topLevelMethodId = this._analysis.GetTopLevelMethodId();
			lambdaId = synthesizedLoweredMethod.LambdaId;
			if (function.ContainingEnvironmentOpt != null)
			{
				BoundNode tmpScope = null;
				ClosureConversion.Analysis.VisitScopeTree(this._analysis.ScopeTree, delegate(ClosureConversion.Analysis.Scope scope)
				{
					if (scope.DeclaredEnvironment == function.ContainingEnvironmentOpt)
					{
						tmpScope = scope.BoundNode;
					}
				});
				lambdaScope = tmpScope;
			}
			else
			{
				lambdaScope = null;
			}
			this.CompilationState.ModuleBuilderOpt.AddSynthesizedDefinition(translatedLambdaContainer, synthesizedLoweredMethod.GetCciAdapter());
			foreach (ParameterSymbol parameterSymbol in node.Symbol.Parameters)
			{
				this._parameterMap.Add(parameterSymbol, synthesizedLoweredMethod.Parameters[parameterSymbol.Ordinal]);
			}
			MethodSymbol currentMethod = this._currentMethod;
			ParameterSymbol currentFrameThis = this._currentFrameThis;
			ImmutableArray<TypeParameterSymbol> currentTypeParameters = this._currentTypeParameters;
			Symbol innermostFramePointer = this._innermostFramePointer;
			TypeMap currentLambdaBodyTypeMap = this._currentLambdaBodyTypeMap;
			ArrayBuilder<BoundStatement> addedStatements = this._addedStatements;
			ArrayBuilder<LocalSymbol> addedLocals = this._addedLocals;
			this._addedStatements = null;
			this._addedLocals = null;
			this._currentMethod = synthesizedLoweredMethod;
			if (closureKind == ClosureKind.Static || closureKind == ClosureKind.Singleton)
			{
				this._innermostFramePointer = (this._currentFrameThis = null);
			}
			else
			{
				this._currentFrameThis = synthesizedLoweredMethod.ThisParameter;
				this._framePointers.TryGetValue(translatedLambdaContainer, ref this._innermostFramePointer);
			}
			SynthesizedClosureEnvironment synthesizedClosureEnvironment = containerAsFrame;
			this._currentTypeParameters = ((synthesizedClosureEnvironment != null) ? synthesizedClosureEnvironment.TypeParameters.Concat<TypeParameterSymbol>(synthesizedLoweredMethod.TypeParameters) : synthesizedLoweredMethod.TypeParameters);
			this._currentLambdaBodyTypeMap = synthesizedLoweredMethod.TypeMap;
			BoundBlock body = node.Body;
			if (body != null)
			{
				BoundStatement boundStatement = this.AddStatementsIfNeeded((BoundStatement)this.VisitBlock(body));
				this.AddSynthesizedMethod(synthesizedLoweredMethod, boundStatement);
			}
			this._currentMethod = currentMethod;
			this._currentFrameThis = currentFrameThis;
			this._currentTypeParameters = currentTypeParameters;
			this._innermostFramePointer = innermostFramePointer;
			this._currentLambdaBodyTypeMap = currentLambdaBodyTypeMap;
			this._addedLocals = addedLocals;
			this._addedStatements = addedStatements;
			return synthesizedLoweredMethod;
		}

		// Token: 0x060021E0 RID: 8672 RVA: 0x000B1DAE File Offset: 0x000AFFAE
		private void AddSynthesizedMethod(MethodSymbol method, BoundStatement body)
		{
			if (this._synthesizedMethods == null)
			{
				this._synthesizedMethods = ArrayBuilder<TypeCompilationState.MethodWithBody>.GetInstance();
			}
			this._synthesizedMethods.Add(new TypeCompilationState.MethodWithBody(method, body, this.CompilationState.CurrentImportChain));
		}

		// Token: 0x060021E1 RID: 8673 RVA: 0x000B1DE0 File Offset: 0x000AFFE0
		private BoundNode RewriteLambdaConversion(BoundLambda node)
		{
			bool inExpressionLambda = this._inExpressionLambda;
			this._inExpressionLambda = this._inExpressionLambda || node.Type.IsExpressionTree();
			if (this._inExpressionLambda)
			{
				TypeSymbol typeSymbol = this.VisitType(node.Type);
				BoundBlock boundBlock = (BoundBlock)this.Visit(node.Body);
				node = node.Update(node.UnboundLambda, node.Symbol, boundBlock, node.Diagnostics, node.Binder, typeSymbol);
				BoundNode boundNode = (inExpressionLambda ? node : ExpressionLambdaRewriter.RewriteLambda(node, this.CompilationState, this.TypeMap, base.RecursionDepth, this.Diagnostics));
				this._inExpressionLambda = inExpressionLambda;
				return boundNode;
			}
			ClosureKind closureKind;
			NamedTypeSymbol namedTypeSymbol;
			SynthesizedClosureEnvironment synthesizedClosureEnvironment;
			BoundNode boundNode2;
			DebugId debugId;
			DebugId debugId2;
			MethodSymbol methodSymbol = this.RewriteLambdaOrLocalFunction(node, out closureKind, out namedTypeSymbol, out synthesizedClosureEnvironment, out boundNode2, out debugId, out debugId2);
			BoundExpression boundExpression;
			NamedTypeSymbol namedTypeSymbol2;
			this.RemapLambdaOrLocalFunction(node.Syntax, node.Symbol, default(ImmutableArray<TypeWithAnnotations>), closureKind, ref methodSymbol, out boundExpression, out namedTypeSymbol2);
			TypeSymbol typeSymbol2 = this.VisitType(node.Type);
			BoundExpression boundExpression2 = new BoundDelegateCreationExpression(node.Syntax, boundExpression, methodSymbol, false, typeSymbol2, false);
			bool flag = closureKind == ClosureKind.Singleton && this._currentMethod.MethodKind != MethodKind.StaticConstructor && !methodSymbol.IsGenericMethod;
			bool flag2 = boundNode2 != null && boundNode2 != ClosureConversion.Analysis.GetScopeParent(this._analysis.ScopeTree, node.Body).BoundNode && ClosureConversion.InLoopOrLambda(node.Syntax, boundNode2.Syntax);
			if (flag || flag2)
			{
				SyntheticBoundNodeFactory syntheticBoundNodeFactory = new SyntheticBoundNodeFactory(this._currentMethod, node.Syntax, this.CompilationState, this.Diagnostics);
				try
				{
					BoundExpression boundExpression3;
					if (flag || (flag2 && synthesizedClosureEnvironment != null))
					{
						TypeSymbol type = synthesizedClosureEnvironment.TypeMap.SubstituteType(node.Type).Type;
						string text = GeneratedNames.MakeLambdaCacheFieldName((closureKind == ClosureKind.General) ? (-1) : debugId.Ordinal, debugId.Generation, debugId2.Ordinal, debugId2.Generation);
						SynthesizedLambdaCacheFieldSymbol synthesizedLambdaCacheFieldSymbol = new SynthesizedLambdaCacheFieldSymbol(namedTypeSymbol, type, text, this._topLevelMethod, false, closureKind == ClosureKind.Singleton);
						this.CompilationState.ModuleBuilderOpt.AddSynthesizedDefinition(namedTypeSymbol, synthesizedLambdaCacheFieldSymbol.GetCciAdapter());
						boundExpression3 = syntheticBoundNodeFactory.Field(boundExpression, synthesizedLambdaCacheFieldSymbol.AsMember(namedTypeSymbol2));
					}
					else
					{
						LocalSymbol localSymbol = syntheticBoundNodeFactory.SynthesizedLocal(typeSymbol2, null, false, RefKind.None, SynthesizedLocalKind.CachedAnonymousMethodDelegate);
						if (this._addedLocals == null)
						{
							this._addedLocals = ArrayBuilder<LocalSymbol>.GetInstance();
						}
						this._addedLocals.Add(localSymbol);
						if (this._addedStatements == null)
						{
							this._addedStatements = ArrayBuilder<BoundStatement>.GetInstance();
						}
						boundExpression3 = syntheticBoundNodeFactory.Local(localSymbol);
						this._addedStatements.Add(syntheticBoundNodeFactory.Assignment(boundExpression3, syntheticBoundNodeFactory.Null(typeSymbol2), false));
					}
					boundExpression2 = syntheticBoundNodeFactory.Coalesce(boundExpression3, syntheticBoundNodeFactory.AssignmentExpression(boundExpression3, boundExpression2, false));
				}
				catch (SyntheticBoundNodeFactory.MissingPredefinedMember missingPredefinedMember)
				{
					this.Diagnostics.Add(missingPredefinedMember.Diagnostic);
					return new BoundBadExpression(syntheticBoundNodeFactory.Syntax, LookupResultKind.Empty, ImmutableArray<Symbol>.Empty, ImmutableArray.Create<BoundExpression>(node), node.Type);
				}
				return boundExpression2;
			}
			return boundExpression2;
		}

		// Token: 0x060021E2 RID: 8674 RVA: 0x000B20E4 File Offset: 0x000B02E4
		private static bool InLoopOrLambda(SyntaxNode lambdaSyntax, SyntaxNode scopeSyntax)
		{
			SyntaxNode syntaxNode = lambdaSyntax.Parent;
			while (syntaxNode != null && syntaxNode != scopeSyntax)
			{
				SyntaxKind syntaxKind = syntaxNode.Kind();
				if (syntaxKind - SyntaxKind.SimpleLambdaExpression <= 1 || syntaxKind - SyntaxKind.WhileStatement <= 3 || syntaxKind == SyntaxKind.ForEachVariableStatement)
				{
					return true;
				}
				syntaxNode = syntaxNode.Parent;
			}
			return false;
		}

		// Token: 0x060021E3 RID: 8675 RVA: 0x000B212E File Offset: 0x000B032E
		public override BoundNode VisitLambda(BoundLambda node)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x060021E5 RID: 8677 RVA: 0x000B2244 File Offset: 0x000B0444
		[CompilerGenerated]
		internal static ImmutableArray<SynthesizedClosureEnvironment> <SynthesizeClosureMethods>g__getStructEnvironments|36_1(ClosureConversion.Analysis.NestedFunction function)
		{
			ArrayBuilder<SynthesizedClosureEnvironment> instance = ArrayBuilder<SynthesizedClosureEnvironment>.GetInstance();
			foreach (ClosureConversion.Analysis.ClosureEnvironment closureEnvironment in function.CapturedEnvironments)
			{
				if (closureEnvironment.IsStruct)
				{
					instance.Add(closureEnvironment.SynthesizedEnvironment);
				}
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x04001066 RID: 4198
		private readonly ClosureConversion.Analysis _analysis;

		// Token: 0x04001067 RID: 4199
		private readonly MethodSymbol _topLevelMethod;

		// Token: 0x04001068 RID: 4200
		private readonly MethodSymbol _substitutedSourceMethod;

		// Token: 0x04001069 RID: 4201
		private readonly int _topLevelMethodOrdinal;

		// Token: 0x0400106A RID: 4202
		private SynthesizedClosureEnvironment _lazyStaticLambdaFrame;

		// Token: 0x0400106B RID: 4203
		private readonly Dictionary<ParameterSymbol, ParameterSymbol> _parameterMap = new Dictionary<ParameterSymbol, ParameterSymbol>();

		// Token: 0x0400106C RID: 4204
		private readonly Dictionary<BoundNode, ClosureConversion.Analysis.ClosureEnvironment> _frames = new Dictionary<BoundNode, ClosureConversion.Analysis.ClosureEnvironment>();

		// Token: 0x0400106D RID: 4205
		private readonly Dictionary<NamedTypeSymbol, Symbol> _framePointers = new Dictionary<NamedTypeSymbol, Symbol>();

		// Token: 0x0400106E RID: 4206
		private readonly HashSet<LocalSymbol> _assignLocals;

		// Token: 0x0400106F RID: 4207
		private MethodSymbol _currentMethod;

		// Token: 0x04001070 RID: 4208
		private ParameterSymbol _currentFrameThis;

		// Token: 0x04001071 RID: 4209
		private readonly ArrayBuilder<LambdaDebugInfo> _lambdaDebugInfoBuilder;

		// Token: 0x04001072 RID: 4210
		private int _synthesizedFieldNameIdDispenser;

		// Token: 0x04001073 RID: 4211
		private Symbol _innermostFramePointer;

		// Token: 0x04001074 RID: 4212
		private TypeMap _currentLambdaBodyTypeMap;

		// Token: 0x04001075 RID: 4213
		private ImmutableArray<TypeParameterSymbol> _currentTypeParameters;

		// Token: 0x04001076 RID: 4214
		private BoundExpression _thisProxyInitDeferred;

		// Token: 0x04001077 RID: 4215
		private bool _seenBaseCall;

		// Token: 0x04001078 RID: 4216
		private bool _inExpressionLambda;

		// Token: 0x04001079 RID: 4217
		private ArrayBuilder<LocalSymbol> _addedLocals;

		// Token: 0x0400107A RID: 4218
		private ArrayBuilder<BoundStatement> _addedStatements;

		// Token: 0x0400107B RID: 4219
		private ArrayBuilder<TypeCompilationState.MethodWithBody> _synthesizedMethods;

		// Token: 0x0400107C RID: 4220
		private readonly ImmutableHashSet<Symbol> _allCapturedVariables;

		// Token: 0x02000719 RID: 1817
		internal sealed class Analysis : BoundTreeWalkerWithStackGuardWithoutRecursionOnTheLeftOfBinaryOperator
		{
			// Token: 0x060085AA RID: 34218 RVA: 0x001E410D File Offset: 0x001E230D
			public bool CanTakeRefParameters(MethodSymbol function)
			{
				return !function.IsAsync && !function.IsIterator && !this.MethodsConvertedToDelegates.Contains(function);
			}

			// Token: 0x060085AB RID: 34219 RVA: 0x001E4130 File Offset: 0x001E2330
			private Analysis(ClosureConversion.Analysis.Scope scopeTree, PooledHashSet<MethodSymbol> methodsConvertedToDelegates, MethodSymbol topLevelMethod, int topLevelMethodOrdinal, VariableSlotAllocator slotAllocatorOpt, TypeCompilationState compilationState)
			{
				this.ScopeTree = scopeTree;
				this.MethodsConvertedToDelegates = methodsConvertedToDelegates;
				this._topLevelMethod = topLevelMethod;
				this._topLevelMethodOrdinal = topLevelMethodOrdinal;
				this._slotAllocatorOpt = slotAllocatorOpt;
				this._compilationState = compilationState;
			}

			// Token: 0x060085AC RID: 34220 RVA: 0x001E4168 File Offset: 0x001E2368
			public static ClosureConversion.Analysis Analyze(BoundNode node, MethodSymbol method, int topLevelMethodOrdinal, MethodSymbol substitutedSourceMethod, VariableSlotAllocator slotAllocatorOpt, TypeCompilationState compilationState, ArrayBuilder<ClosureDebugInfo> closureDebugInfo, DiagnosticBag diagnostics)
			{
				PooledHashSet<MethodSymbol> instance = PooledHashSet<MethodSymbol>.GetInstance();
				ClosureConversion.Analysis analysis = new ClosureConversion.Analysis(ClosureConversion.Analysis.ScopeTreeBuilder.Build(node, method, instance, diagnostics), instance, method, topLevelMethodOrdinal, slotAllocatorOpt, compilationState);
				analysis.MakeAndAssignEnvironments();
				analysis.ComputeLambdaScopesAndFrameCaptures();
				if (compilationState.Compilation.Options.OptimizationLevel == OptimizationLevel.Release)
				{
					analysis.MergeEnvironments();
				}
				analysis.InlineThisOnlyEnvironments();
				return analysis;
			}

			// Token: 0x060085AD RID: 34221 RVA: 0x001E41C0 File Offset: 0x001E23C0
			private static BoundNode FindNodeToAnalyze(BoundNode node)
			{
				BoundKind kind;
				for (;;)
				{
					kind = node.Kind;
					if (kind <= BoundKind.SequencePoint)
					{
						if (kind == BoundKind.FieldEqualsValue)
						{
							return node;
						}
						if (kind == BoundKind.GlobalStatementInitializer)
						{
							goto IL_004B;
						}
						if (kind != BoundKind.SequencePoint)
						{
							break;
						}
						node = ((BoundSequencePoint)node).StatementOpt;
					}
					else
					{
						if (kind != BoundKind.SequencePointWithSpan)
						{
							goto Block_4;
						}
						node = ((BoundSequencePointWithSpan)node).StatementOpt;
					}
				}
				goto IL_0057;
				Block_4:
				if (kind != BoundKind.Block && kind != BoundKind.StatementList)
				{
					goto IL_0057;
				}
				return node;
				IL_004B:
				return ((BoundGlobalStatementInitializer)node).Statement;
				IL_0057:
				throw ExceptionUtilities.UnexpectedValue(node.Kind);
			}

			// Token: 0x060085AE RID: 34222 RVA: 0x001E4234 File Offset: 0x001E2434
			private void ComputeLambdaScopesAndFrameCaptures()
			{
				ClosureConversion.Analysis.VisitNestedFunctions(this.ScopeTree, delegate(ClosureConversion.Analysis.Scope scope, ClosureConversion.Analysis.NestedFunction function)
				{
					if (function.CapturedEnvironments.Count > 0)
					{
						PooledHashSet<ClosureConversion.Analysis.ClosureEnvironment> instance = PooledHashSet<ClosureConversion.Analysis.ClosureEnvironment>.GetInstance();
						instance.AddAll<ClosureConversion.Analysis.ClosureEnvironment>(function.CapturedEnvironments);
						ClosureConversion.Analysis.Scope scope2;
						for (scope2 = scope; scope2 != null; scope2 = scope2.Parent)
						{
							ClosureConversion.Analysis.ClosureEnvironment declaredEnvironment = scope2.DeclaredEnvironment;
							if (declaredEnvironment != null && instance.Remove(declaredEnvironment) && !declaredEnvironment.IsStruct)
							{
								function.ContainingEnvironmentOpt = declaredEnvironment;
								break;
							}
						}
						ClosureConversion.Analysis.ClosureEnvironment closureEnvironment = ((scope2 != null) ? scope2.DeclaredEnvironment : null);
						scope2 = ((scope2 != null) ? scope2.Parent : null);
						while (scope2 != null && instance.Count != 0)
						{
							ClosureConversion.Analysis.ClosureEnvironment declaredEnvironment2 = scope2.DeclaredEnvironment;
							if (declaredEnvironment2 != null)
							{
								if (!declaredEnvironment2.IsStruct)
								{
									closureEnvironment.CapturesParent = true;
									closureEnvironment = declaredEnvironment2;
								}
								instance.Remove(declaredEnvironment2);
							}
							scope2 = scope2.Parent;
						}
						if (instance.Count > 0)
						{
							throw ExceptionUtilities.Unreachable;
						}
						instance.Free();
					}
				});
			}

			// Token: 0x060085AF RID: 34223 RVA: 0x001E4260 File Offset: 0x001E2460
			private void InlineThisOnlyEnvironments()
			{
				ClosureConversion.Analysis.<>c__DisplayClass11_0 CS$<>8__locals1 = new ClosureConversion.Analysis.<>c__DisplayClass11_0();
				CS$<>8__locals1.<>4__this = this;
				ParameterSymbol parameterSymbol;
				if (!this._topLevelMethod.TryGetThisParameter(out parameterSymbol) || parameterSymbol == null)
				{
					return;
				}
				CS$<>8__locals1.env = this.ScopeTree.DeclaredEnvironment;
				if (CS$<>8__locals1.env == null)
				{
					return;
				}
				if (CS$<>8__locals1.env.CapturedVariables.Count > 1 || !CS$<>8__locals1.env.CapturedVariables.Contains(parameterSymbol))
				{
					return;
				}
				if (CS$<>8__locals1.env.IsStruct)
				{
					if (!ClosureConversion.Analysis.CheckNestedFunctions(this.ScopeTree, (ClosureConversion.Analysis.Scope scope, ClosureConversion.Analysis.NestedFunction closure) => closure.CapturedEnvironments.Contains(CS$<>8__locals1.env) && closure.ContainingEnvironmentOpt != null))
					{
						CS$<>8__locals1.<InlineThisOnlyEnvironments>g__RemoveEnv|1();
						return;
					}
				}
				else if (VarianceSafety.GetEnclosingVariantInterface(this._topLevelMethod) == null)
				{
					CS$<>8__locals1.<InlineThisOnlyEnvironments>g__RemoveEnv|1();
					ClosureConversion.Analysis.VisitNestedFunctions(this.ScopeTree, delegate(ClosureConversion.Analysis.Scope scope, ClosureConversion.Analysis.NestedFunction closure)
					{
						if (closure.ContainingEnvironmentOpt == CS$<>8__locals1.env)
						{
							closure.ContainingEnvironmentOpt = null;
						}
					});
				}
			}

			// Token: 0x060085B0 RID: 34224 RVA: 0x001E432B File Offset: 0x001E252B
			private void MakeAndAssignEnvironments()
			{
				ClosureConversion.Analysis.VisitScopeTree(this.ScopeTree, delegate(ClosureConversion.Analysis.Scope scope)
				{
					SetWithInsertionOrder<Symbol> declaredVariables = scope.DeclaredVariables;
					if (declaredVariables.Count == 0)
					{
						return;
					}
					bool isStruct = VarianceSafety.GetEnclosingVariantInterface(this._topLevelMethod) == null;
					SetWithInsertionOrder<ClosureConversion.Analysis.NestedFunction> closures = new SetWithInsertionOrder<ClosureConversion.Analysis.NestedFunction>();
					bool addedItem;
					do
					{
						addedItem = false;
						ClosureConversion.Analysis.VisitNestedFunctions(scope, delegate(ClosureConversion.Analysis.Scope closureScope, ClosureConversion.Analysis.NestedFunction closure)
						{
							if (!closures.Contains(closure))
							{
								if (!closure.CapturedVariables.Overlaps(scope.DeclaredVariables))
								{
									if (!closure.CapturedVariables.Overlaps(Enumerable.Select<ClosureConversion.Analysis.NestedFunction, MethodSymbol>(closures, (ClosureConversion.Analysis.NestedFunction c) => c.OriginalMethodSymbol)))
									{
										return;
									}
								}
								closures.Add(closure);
								addedItem = true;
								isStruct &= this.CanTakeRefParameters(closure.OriginalMethodSymbol);
							}
						});
					}
					while (addedItem);
					ClosureConversion.Analysis.ClosureEnvironment closureEnvironment = new ClosureConversion.Analysis.ClosureEnvironment(declaredVariables, isStruct);
					scope.DeclaredEnvironment = closureEnvironment;
					ParameterSymbol parameterSymbol;
					this._topLevelMethod.TryGetThisParameter(out parameterSymbol);
					foreach (ClosureConversion.Analysis.NestedFunction nestedFunction in closures)
					{
						nestedFunction.CapturedEnvironments.Add(closureEnvironment);
						if (parameterSymbol != null && closureEnvironment.CapturedVariables.Contains(parameterSymbol))
						{
							nestedFunction.CapturesThis = true;
						}
					}
				});
			}

			// Token: 0x060085B1 RID: 34225 RVA: 0x001E4344 File Offset: 0x001E2544
			private PooledDictionary<ClosureConversion.Analysis.Scope, PooledHashSet<ClosureConversion.Analysis.NestedFunction>> CalculateFunctionsCapturingScopeVariables()
			{
				PooledDictionary<ClosureConversion.Analysis.Scope, PooledHashSet<ClosureConversion.Analysis.NestedFunction>> closuresCapturingScopeVariables = PooledDictionary<ClosureConversion.Analysis.Scope, PooledHashSet<ClosureConversion.Analysis.NestedFunction>>.GetInstance();
				PooledDictionary<ClosureConversion.Analysis.ClosureEnvironment, ClosureConversion.Analysis.Scope> environmentsToScopes = PooledDictionary<ClosureConversion.Analysis.ClosureEnvironment, ClosureConversion.Analysis.Scope>.GetInstance();
				ClosureConversion.Analysis.VisitScopeTree(this.ScopeTree, delegate(ClosureConversion.Analysis.Scope scope)
				{
					if (scope.DeclaredEnvironment != null)
					{
						closuresCapturingScopeVariables[scope] = PooledHashSet<ClosureConversion.Analysis.NestedFunction>.GetInstance();
						environmentsToScopes[scope.DeclaredEnvironment] = scope;
					}
					foreach (ClosureConversion.Analysis.NestedFunction nestedFunction in scope.NestedFunctions)
					{
						foreach (ClosureConversion.Analysis.ClosureEnvironment closureEnvironment in nestedFunction.CapturedEnvironments)
						{
							closuresCapturingScopeVariables[environmentsToScopes[closureEnvironment]].Add(nestedFunction);
						}
					}
				});
				environmentsToScopes.Free();
				foreach (KeyValuePair<ClosureConversion.Analysis.Scope, PooledHashSet<ClosureConversion.Analysis.NestedFunction>> keyValuePair in closuresCapturingScopeVariables)
				{
					ClosureConversion.Analysis.Scope scope4;
					PooledHashSet<ClosureConversion.Analysis.NestedFunction> pooledHashSet;
					keyValuePair.Deconstruct<ClosureConversion.Analysis.Scope, PooledHashSet<ClosureConversion.Analysis.NestedFunction>>(out scope4, out pooledHashSet);
					ClosureConversion.Analysis.Scope scope2 = scope4;
					PooledHashSet<ClosureConversion.Analysis.NestedFunction> pooledHashSet2 = pooledHashSet;
					if (scope2.DeclaredEnvironment != null)
					{
						ClosureConversion.Analysis.Scope scope3 = scope2;
						while (scope3.DeclaredEnvironment == null || scope3.DeclaredEnvironment.CapturesParent)
						{
							scope3 = scope3.Parent;
							if (scope3 == null)
							{
								throw ExceptionUtilities.Unreachable;
							}
							if (scope3.DeclaredEnvironment != null && !scope3.DeclaredEnvironment.IsStruct)
							{
								closuresCapturingScopeVariables[scope3].AddAll<ClosureConversion.Analysis.NestedFunction>(pooledHashSet2);
							}
						}
					}
				}
				return closuresCapturingScopeVariables;
			}

			// Token: 0x060085B2 RID: 34226 RVA: 0x001E4448 File Offset: 0x001E2648
			private void MergeEnvironments()
			{
				PooledDictionary<ClosureConversion.Analysis.Scope, PooledHashSet<ClosureConversion.Analysis.NestedFunction>> pooledDictionary = this.CalculateFunctionsCapturingScopeVariables();
				foreach (KeyValuePair<ClosureConversion.Analysis.Scope, PooledHashSet<ClosureConversion.Analysis.NestedFunction>> keyValuePair in pooledDictionary)
				{
					ClosureConversion.Analysis.Scope scope;
					PooledHashSet<ClosureConversion.Analysis.NestedFunction> pooledHashSet;
					keyValuePair.Deconstruct<ClosureConversion.Analysis.Scope, PooledHashSet<ClosureConversion.Analysis.NestedFunction>>(out scope, out pooledHashSet);
					ClosureConversion.Analysis.Scope scope2 = scope;
					PooledHashSet<ClosureConversion.Analysis.NestedFunction> pooledHashSet2 = pooledHashSet;
					if (pooledHashSet2.Count != 0)
					{
						ClosureConversion.Analysis.ClosureEnvironment declaredEnvironment = scope2.DeclaredEnvironment;
						if (!declaredEnvironment.IsStruct)
						{
							ClosureConversion.Analysis.Scope scope3 = scope2;
							ClosureConversion.Analysis.Scope scope4 = scope2;
							while (scope4.Parent != null && scope4.CanMergeWithParent)
							{
								ClosureConversion.Analysis.Scope parent = scope4.Parent;
								ClosureConversion.Analysis.ClosureEnvironment declaredEnvironment2 = parent.DeclaredEnvironment;
								if (declaredEnvironment2 == null || declaredEnvironment2.IsStruct)
								{
									scope4 = parent;
								}
								else
								{
									if (!pooledDictionary[parent].SetEquals(pooledHashSet2))
									{
										break;
									}
									scope3 = parent;
									scope4 = parent;
								}
							}
							if (scope3 != scope2)
							{
								ClosureConversion.Analysis.ClosureEnvironment declaredEnvironment3 = scope3.DeclaredEnvironment;
								foreach (Symbol symbol in declaredEnvironment.CapturedVariables)
								{
									declaredEnvironment3.CapturedVariables.Add(symbol);
								}
								scope2.DeclaredEnvironment = null;
								foreach (ClosureConversion.Analysis.NestedFunction nestedFunction in pooledHashSet2)
								{
									nestedFunction.CapturedEnvironments.Remove(declaredEnvironment);
									if (!nestedFunction.CapturedEnvironments.Contains(declaredEnvironment3))
									{
										nestedFunction.CapturedEnvironments.Add(declaredEnvironment3);
									}
									if (nestedFunction.ContainingEnvironmentOpt == declaredEnvironment)
									{
										nestedFunction.ContainingEnvironmentOpt = declaredEnvironment3;
									}
								}
							}
						}
					}
				}
				foreach (PooledHashSet<ClosureConversion.Analysis.NestedFunction> pooledHashSet3 in pooledDictionary.Values)
				{
					pooledHashSet3.Free();
				}
				pooledDictionary.Free();
			}

			// Token: 0x060085B3 RID: 34227 RVA: 0x001E4674 File Offset: 0x001E2874
			internal DebugId GetTopLevelMethodId()
			{
				VariableSlotAllocator slotAllocatorOpt = this._slotAllocatorOpt;
				DebugId? debugId = ((slotAllocatorOpt != null) ? slotAllocatorOpt.MethodId : default(DebugId?));
				if (debugId == null)
				{
					return new DebugId(this._topLevelMethodOrdinal, this._compilationState.ModuleBuilderOpt.CurrentGenerationOrdinal);
				}
				return debugId.GetValueOrDefault();
			}

			// Token: 0x060085B4 RID: 34228 RVA: 0x001E46C8 File Offset: 0x001E28C8
			internal DebugId GetClosureId(SyntaxNode syntax, ArrayBuilder<ClosureDebugInfo> closureDebugInfo)
			{
				DebugId debugId;
				DebugId debugId2;
				if (this._slotAllocatorOpt != null && this._slotAllocatorOpt.TryGetPreviousClosure(syntax, out debugId))
				{
					debugId2 = debugId;
				}
				else
				{
					debugId2 = new DebugId(closureDebugInfo.Count, this._compilationState.ModuleBuilderOpt.CurrentGenerationOrdinal);
				}
				int num = this._topLevelMethod.CalculateLocalSyntaxOffset(LambdaUtilities.GetDeclaratorPosition(syntax), syntax.SyntaxTree);
				closureDebugInfo.Add(new ClosureDebugInfo(num, debugId2));
				return debugId2;
			}

			// Token: 0x060085B5 RID: 34229 RVA: 0x001E4734 File Offset: 0x001E2934
			public static ClosureConversion.Analysis.Scope GetVariableDeclarationScope(ClosureConversion.Analysis.Scope startingScope, Symbol variable)
			{
				ParameterSymbol parameterSymbol = variable as ParameterSymbol;
				if (parameterSymbol != null && parameterSymbol.IsThis)
				{
					return null;
				}
				ClosureConversion.Analysis.Scope scope = startingScope;
				while (scope != null)
				{
					SymbolKind kind = variable.Kind;
					if (kind == SymbolKind.Local)
					{
						goto IL_002D;
					}
					if (kind != SymbolKind.Method)
					{
						if (kind == SymbolKind.Parameter)
						{
							goto IL_002D;
						}
						throw ExceptionUtilities.UnexpectedValue(variable.Kind);
					}
					else
					{
						ArrayBuilder<ClosureConversion.Analysis.NestedFunction>.Enumerator enumerator = scope.NestedFunctions.GetEnumerator();
						while (enumerator.MoveNext())
						{
							if (enumerator.Current.OriginalMethodSymbol == variable)
							{
								return scope;
							}
						}
					}
					IL_007D:
					scope = scope.Parent;
					continue;
					IL_002D:
					if (scope.DeclaredVariables.Contains(variable))
					{
						return scope;
					}
					goto IL_007D;
				}
				return null;
			}

			// Token: 0x060085B6 RID: 34230 RVA: 0x001E47C9 File Offset: 0x001E29C9
			public static ClosureConversion.Analysis.Scope GetScopeParent(ClosureConversion.Analysis.Scope treeRoot, BoundNode scopeNode)
			{
				return ClosureConversion.Analysis.GetScopeWithMatchingBoundNode(treeRoot, scopeNode).Parent;
			}

			// Token: 0x060085B7 RID: 34231 RVA: 0x001E47D8 File Offset: 0x001E29D8
			public static ClosureConversion.Analysis.Scope GetScopeWithMatchingBoundNode(ClosureConversion.Analysis.Scope treeRoot, BoundNode node)
			{
				ClosureConversion.Analysis.<>c__DisplayClass19_0 CS$<>8__locals1;
				CS$<>8__locals1.node = node;
				ClosureConversion.Analysis.Scope scope = ClosureConversion.Analysis.<GetScopeWithMatchingBoundNode>g__Helper|19_0(treeRoot, ref CS$<>8__locals1);
				if (scope == null)
				{
					throw ExceptionUtilities.Unreachable;
				}
				return scope;
			}

			// Token: 0x060085B8 RID: 34232 RVA: 0x001E4800 File Offset: 0x001E2A00
			public static ValueTuple<ClosureConversion.Analysis.NestedFunction, ClosureConversion.Analysis.Scope> GetVisibleNestedFunction(ClosureConversion.Analysis.Scope startingScope, MethodSymbol functionSymbol)
			{
				for (ClosureConversion.Analysis.Scope scope = startingScope; scope != null; scope = scope.Parent)
				{
					foreach (ClosureConversion.Analysis.NestedFunction nestedFunction in scope.NestedFunctions)
					{
						if (nestedFunction.OriginalMethodSymbol == functionSymbol)
						{
							return new ValueTuple<ClosureConversion.Analysis.NestedFunction, ClosureConversion.Analysis.Scope>(nestedFunction, scope);
						}
					}
				}
				throw ExceptionUtilities.Unreachable;
			}

			// Token: 0x060085B9 RID: 34233 RVA: 0x001E4858 File Offset: 0x001E2A58
			public static ClosureConversion.Analysis.NestedFunction GetNestedFunctionInTree(ClosureConversion.Analysis.Scope treeRoot, MethodSymbol functionSymbol)
			{
				ClosureConversion.Analysis.<>c__DisplayClass21_0 CS$<>8__locals1;
				CS$<>8__locals1.functionSymbol = functionSymbol;
				ClosureConversion.Analysis.NestedFunction nestedFunction = ClosureConversion.Analysis.<GetNestedFunctionInTree>g__helper|21_0(treeRoot, ref CS$<>8__locals1);
				if (nestedFunction == null)
				{
					throw ExceptionUtilities.Unreachable;
				}
				return nestedFunction;
			}

			// Token: 0x060085BA RID: 34234 RVA: 0x001E487F File Offset: 0x001E2A7F
			public void Free()
			{
				this.MethodsConvertedToDelegates.Free();
				this.ScopeTree.Free();
			}

			// Token: 0x060085BB RID: 34235 RVA: 0x001E4898 File Offset: 0x001E2A98
			public static void VisitNestedFunctions(ClosureConversion.Analysis.Scope scope, Action<ClosureConversion.Analysis.Scope, ClosureConversion.Analysis.NestedFunction> action)
			{
				foreach (ClosureConversion.Analysis.NestedFunction nestedFunction in scope.NestedFunctions)
				{
					action.Invoke(scope, nestedFunction);
				}
				foreach (ClosureConversion.Analysis.Scope scope2 in scope.NestedScopes)
				{
					ClosureConversion.Analysis.VisitNestedFunctions(scope2, action);
				}
			}

			// Token: 0x060085BC RID: 34236 RVA: 0x001E48F0 File Offset: 0x001E2AF0
			public static bool CheckNestedFunctions(ClosureConversion.Analysis.Scope scope, Func<ClosureConversion.Analysis.Scope, ClosureConversion.Analysis.NestedFunction, bool> func)
			{
				foreach (ClosureConversion.Analysis.NestedFunction nestedFunction in scope.NestedFunctions)
				{
					if (func.Invoke(scope, nestedFunction))
					{
						return true;
					}
				}
				ArrayBuilder<ClosureConversion.Analysis.Scope>.Enumerator enumerator2 = scope.NestedScopes.GetEnumerator();
				while (enumerator2.MoveNext())
				{
					if (ClosureConversion.Analysis.CheckNestedFunctions(enumerator2.Current, func))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x060085BD RID: 34237 RVA: 0x001E4954 File Offset: 0x001E2B54
			public static void VisitScopeTree(ClosureConversion.Analysis.Scope treeRoot, Action<ClosureConversion.Analysis.Scope> action)
			{
				action.Invoke(treeRoot);
				foreach (ClosureConversion.Analysis.Scope scope in treeRoot.NestedScopes)
				{
					ClosureConversion.Analysis.VisitScopeTree(scope, action);
				}
			}

			// Token: 0x060085BF RID: 34239 RVA: 0x001E4A9C File Offset: 0x001E2C9C
			[CompilerGenerated]
			internal static ClosureConversion.Analysis.Scope <GetScopeWithMatchingBoundNode>g__Helper|19_0(ClosureConversion.Analysis.Scope currentScope, ref ClosureConversion.Analysis.<>c__DisplayClass19_0 A_1)
			{
				if (currentScope.BoundNode == A_1.node)
				{
					return currentScope;
				}
				foreach (ClosureConversion.Analysis.Scope scope in currentScope.NestedScopes)
				{
					ClosureConversion.Analysis.Scope scope2 = ClosureConversion.Analysis.<GetScopeWithMatchingBoundNode>g__Helper|19_0(scope, ref A_1);
					if (scope2 != null)
					{
						return scope2;
					}
				}
				return null;
			}

			// Token: 0x060085C0 RID: 34240 RVA: 0x001E4AE4 File Offset: 0x001E2CE4
			[CompilerGenerated]
			internal static ClosureConversion.Analysis.NestedFunction <GetNestedFunctionInTree>g__helper|21_0(ClosureConversion.Analysis.Scope scope, ref ClosureConversion.Analysis.<>c__DisplayClass21_0 A_1)
			{
				foreach (ClosureConversion.Analysis.NestedFunction nestedFunction in scope.NestedFunctions)
				{
					if (nestedFunction.OriginalMethodSymbol == A_1.functionSymbol)
					{
						return nestedFunction;
					}
				}
				foreach (ClosureConversion.Analysis.Scope scope2 in scope.NestedScopes)
				{
					ClosureConversion.Analysis.NestedFunction nestedFunction2 = ClosureConversion.Analysis.<GetNestedFunctionInTree>g__helper|21_0(scope2, ref A_1);
					if (nestedFunction2 != null)
					{
						return nestedFunction2;
					}
				}
				return null;
			}

			// Token: 0x04002289 RID: 8841
			public readonly PooledHashSet<MethodSymbol> MethodsConvertedToDelegates;

			// Token: 0x0400228A RID: 8842
			public readonly ClosureConversion.Analysis.Scope ScopeTree;

			// Token: 0x0400228B RID: 8843
			private readonly MethodSymbol _topLevelMethod;

			// Token: 0x0400228C RID: 8844
			private readonly int _topLevelMethodOrdinal;

			// Token: 0x0400228D RID: 8845
			private readonly VariableSlotAllocator _slotAllocatorOpt;

			// Token: 0x0400228E RID: 8846
			private readonly TypeCompilationState _compilationState;

			// Token: 0x020009D3 RID: 2515
			[DebuggerDisplay("{ToString(), nq}")]
			public sealed class Scope
			{
				// Token: 0x0600933C RID: 37692 RVA: 0x002020A4 File Offset: 0x002002A4
				public Scope(ClosureConversion.Analysis.Scope parent, BoundNode boundNode, ClosureConversion.Analysis.NestedFunction containingFunction)
				{
					this.Parent = parent;
					this.BoundNode = boundNode;
					this.ContainingFunctionOpt = containingFunction;
				}

				// Token: 0x1700293B RID: 10555
				// (get) Token: 0x0600933D RID: 37693 RVA: 0x002020F4 File Offset: 0x002002F4
				// (set) Token: 0x0600933E RID: 37694 RVA: 0x002020FC File Offset: 0x002002FC
				public bool CanMergeWithParent { get; internal set; } = true;

				// Token: 0x0600933F RID: 37695 RVA: 0x00202108 File Offset: 0x00200308
				public void Free()
				{
					foreach (ClosureConversion.Analysis.Scope scope in this.NestedScopes)
					{
						scope.Free();
					}
					this.NestedScopes.Free();
					foreach (ClosureConversion.Analysis.NestedFunction nestedFunction in this.NestedFunctions)
					{
						nestedFunction.Free();
					}
					this.NestedFunctions.Free();
				}

				// Token: 0x06009340 RID: 37696 RVA: 0x00202171 File Offset: 0x00200371
				public override string ToString()
				{
					return this.BoundNode.Syntax.GetText(null, SourceHashAlgorithm.Sha1).ToString();
				}

				// Token: 0x040029D0 RID: 10704
				public readonly ClosureConversion.Analysis.Scope Parent;

				// Token: 0x040029D1 RID: 10705
				public readonly ArrayBuilder<ClosureConversion.Analysis.Scope> NestedScopes = ArrayBuilder<ClosureConversion.Analysis.Scope>.GetInstance();

				// Token: 0x040029D2 RID: 10706
				public readonly ArrayBuilder<ClosureConversion.Analysis.NestedFunction> NestedFunctions = ArrayBuilder<ClosureConversion.Analysis.NestedFunction>.GetInstance();

				// Token: 0x040029D3 RID: 10707
				public readonly SetWithInsertionOrder<Symbol> DeclaredVariables = new SetWithInsertionOrder<Symbol>();

				// Token: 0x040029D4 RID: 10708
				public readonly BoundNode BoundNode;

				// Token: 0x040029D5 RID: 10709
				public readonly ClosureConversion.Analysis.NestedFunction ContainingFunctionOpt;

				// Token: 0x040029D6 RID: 10710
				[global::System.Runtime.CompilerServices.Nullable(2)]
				public ClosureConversion.Analysis.ClosureEnvironment DeclaredEnvironment;
			}

			// Token: 0x020009D4 RID: 2516
			public sealed class NestedFunction
			{
				// Token: 0x1700293C RID: 10556
				// (get) Token: 0x06009341 RID: 37697 RVA: 0x0020218A File Offset: 0x0020038A
				// (set) Token: 0x06009342 RID: 37698 RVA: 0x00202192 File Offset: 0x00200392
				public bool CapturesThis
				{
					get
					{
						return this._capturesThis;
					}
					set
					{
						this._capturesThis = value;
					}
				}

				// Token: 0x06009343 RID: 37699 RVA: 0x0020219B File Offset: 0x0020039B
				public NestedFunction(MethodSymbol symbol, SyntaxReference blockSyntax)
				{
					this.OriginalMethodSymbol = symbol;
					this.BlockSyntax = blockSyntax;
				}

				// Token: 0x06009344 RID: 37700 RVA: 0x002021C7 File Offset: 0x002003C7
				public void Free()
				{
					this.CapturedVariables.Free();
					this.CapturedEnvironments.Free();
				}

				// Token: 0x040029D8 RID: 10712
				public readonly MethodSymbol OriginalMethodSymbol;

				// Token: 0x040029D9 RID: 10713
				public readonly SyntaxReference BlockSyntax;

				// Token: 0x040029DA RID: 10714
				public readonly PooledHashSet<Symbol> CapturedVariables = PooledHashSet<Symbol>.GetInstance();

				// Token: 0x040029DB RID: 10715
				public readonly ArrayBuilder<ClosureConversion.Analysis.ClosureEnvironment> CapturedEnvironments = ArrayBuilder<ClosureConversion.Analysis.ClosureEnvironment>.GetInstance();

				// Token: 0x040029DC RID: 10716
				public ClosureConversion.Analysis.ClosureEnvironment ContainingEnvironmentOpt;

				// Token: 0x040029DD RID: 10717
				private bool _capturesThis;

				// Token: 0x040029DE RID: 10718
				public SynthesizedClosureMethod SynthesizedLoweredMethod;
			}

			// Token: 0x020009D5 RID: 2517
			public sealed class ClosureEnvironment
			{
				// Token: 0x06009345 RID: 37701 RVA: 0x002021E0 File Offset: 0x002003E0
				public ClosureEnvironment(IEnumerable<Symbol> capturedVariables, bool isStruct)
				{
					this.CapturedVariables = new SetWithInsertionOrder<Symbol>();
					foreach (Symbol symbol in capturedVariables)
					{
						this.CapturedVariables.Add(symbol);
					}
					this.IsStruct = isStruct;
				}

				// Token: 0x040029DF RID: 10719
				public readonly SetWithInsertionOrder<Symbol> CapturedVariables;

				// Token: 0x040029E0 RID: 10720
				public bool CapturesParent;

				// Token: 0x040029E1 RID: 10721
				public readonly bool IsStruct;

				// Token: 0x040029E2 RID: 10722
				internal SynthesizedClosureEnvironment SynthesizedEnvironment;
			}

			// Token: 0x020009D6 RID: 2518
			private class ScopeTreeBuilder : BoundTreeWalkerWithStackGuardWithoutRecursionOnTheLeftOfBinaryOperator
			{
				// Token: 0x06009346 RID: 37702 RVA: 0x00202248 File Offset: 0x00200448
				private ScopeTreeBuilder(ClosureConversion.Analysis.Scope rootScope, MethodSymbol topLevelMethod, HashSet<MethodSymbol> methodsConvertedToDelegates, DiagnosticBag diagnostics)
				{
					this._currentScope = rootScope;
					this._labelsInScope.Push(ArrayBuilder<LabelSymbol>.GetInstance());
					this._topLevelMethod = topLevelMethod;
					this._methodsConvertedToDelegates = methodsConvertedToDelegates;
					this._diagnostics = diagnostics;
				}

				// Token: 0x06009347 RID: 37703 RVA: 0x002022A9 File Offset: 0x002004A9
				public static ClosureConversion.Analysis.Scope Build(BoundNode node, MethodSymbol topLevelMethod, HashSet<MethodSymbol> methodsConvertedToDelegates, DiagnosticBag diagnostics)
				{
					ClosureConversion.Analysis.Scope scope = new ClosureConversion.Analysis.Scope(null, node, null);
					new ClosureConversion.Analysis.ScopeTreeBuilder(scope, topLevelMethod, methodsConvertedToDelegates, diagnostics).Build();
					return scope;
				}

				// Token: 0x06009348 RID: 37704 RVA: 0x002022C4 File Offset: 0x002004C4
				private void Build()
				{
					this.DeclareLocals<ParameterSymbol>(this._currentScope, this._topLevelMethod.Parameters, false);
					ParameterSymbol parameterSymbol;
					if (this._topLevelMethod.TryGetThisParameter(out parameterSymbol) && parameterSymbol != null)
					{
						this.DeclareLocals<Symbol>(this._currentScope, ImmutableArray.Create<Symbol>(parameterSymbol), false);
					}
					this.Visit(this._currentScope.BoundNode);
					foreach (ArrayBuilder<ClosureConversion.Analysis.Scope> arrayBuilder in this._scopesAfterLabel.Values)
					{
						arrayBuilder.Free();
					}
					this._scopesAfterLabel.Free();
					this._labelsInScope.Pop<ArrayBuilder<LabelSymbol>>().Free();
					this._labelsInScope.Free();
				}

				// Token: 0x06009349 RID: 37705 RVA: 0x00202390 File Offset: 0x00200590
				public override BoundNode VisitMethodGroup(BoundMethodGroup node)
				{
					throw ExceptionUtilities.Unreachable;
				}

				// Token: 0x0600934A RID: 37706 RVA: 0x00202398 File Offset: 0x00200598
				public override BoundNode VisitBlock(BoundBlock node)
				{
					ClosureConversion.Analysis.Scope currentScope = this._currentScope;
					this.PushOrReuseScope<LocalSymbol>(node, node.Locals);
					BoundNode boundNode = base.VisitBlock(node);
					this.PopScope(currentScope);
					return boundNode;
				}

				// Token: 0x0600934B RID: 37707 RVA: 0x002023C8 File Offset: 0x002005C8
				public override BoundNode VisitCatchBlock(BoundCatchBlock node)
				{
					ClosureConversion.Analysis.Scope currentScope = this._currentScope;
					this.PushOrReuseScope<LocalSymbol>(node, node.Locals);
					BoundNode boundNode = base.VisitCatchBlock(node);
					this.PopScope(currentScope);
					return boundNode;
				}

				// Token: 0x0600934C RID: 37708 RVA: 0x002023F8 File Offset: 0x002005F8
				public override BoundNode VisitSequence(BoundSequence node)
				{
					ClosureConversion.Analysis.Scope currentScope = this._currentScope;
					this.PushOrReuseScope<LocalSymbol>(node, node.Locals);
					BoundNode boundNode = base.VisitSequence(node);
					this.PopScope(currentScope);
					return boundNode;
				}

				// Token: 0x0600934D RID: 37709 RVA: 0x00202428 File Offset: 0x00200628
				public override BoundNode VisitLambda(BoundLambda node)
				{
					bool inExpressionTree = this._inExpressionTree;
					this._inExpressionTree |= node.Type.IsExpressionTree();
					this._methodsConvertedToDelegates.Add(node.Symbol.OriginalDefinition);
					BoundNode boundNode = this.VisitNestedFunction(node.Symbol, node.Body);
					this._inExpressionTree = inExpressionTree;
					return boundNode;
				}

				// Token: 0x0600934E RID: 37710 RVA: 0x00202484 File Offset: 0x00200684
				public override BoundNode VisitLocalFunctionStatement(BoundLocalFunctionStatement node)
				{
					return this.VisitNestedFunction(node.Symbol.OriginalDefinition, node.Body);
				}

				// Token: 0x0600934F RID: 37711 RVA: 0x0020249D File Offset: 0x0020069D
				public override BoundNode VisitCall(BoundCall node)
				{
					if (node.Method.MethodKind == MethodKind.LocalFunction)
					{
						this.AddIfCaptured(node.Method.OriginalDefinition, node.Syntax);
					}
					return base.VisitCall(node);
				}

				// Token: 0x06009350 RID: 37712 RVA: 0x002024CC File Offset: 0x002006CC
				public override BoundNode VisitDelegateCreationExpression(BoundDelegateCreationExpression node)
				{
					MethodSymbol methodOpt = node.MethodOpt;
					if (methodOpt != null && methodOpt.MethodKind == MethodKind.LocalFunction)
					{
						MethodSymbol originalDefinition = node.MethodOpt.OriginalDefinition;
						this.AddIfCaptured(originalDefinition, node.Syntax);
						this._methodsConvertedToDelegates.Add(originalDefinition);
					}
					return base.VisitDelegateCreationExpression(node);
				}

				// Token: 0x06009351 RID: 37713 RVA: 0x0020251E File Offset: 0x0020071E
				public override BoundNode VisitParameter(BoundParameter node)
				{
					this.AddIfCaptured(node.ParameterSymbol, node.Syntax);
					return base.VisitParameter(node);
				}

				// Token: 0x06009352 RID: 37714 RVA: 0x00202539 File Offset: 0x00200739
				public override BoundNode VisitLocal(BoundLocal node)
				{
					this.AddIfCaptured(node.LocalSymbol, node.Syntax);
					return base.VisitLocal(node);
				}

				// Token: 0x06009353 RID: 37715 RVA: 0x00202554 File Offset: 0x00200754
				public override BoundNode VisitBaseReference(BoundBaseReference node)
				{
					this.AddIfCaptured(this._topLevelMethod.ThisParameter, node.Syntax);
					return base.VisitBaseReference(node);
				}

				// Token: 0x06009354 RID: 37716 RVA: 0x00202574 File Offset: 0x00200774
				public override BoundNode VisitThisReference(BoundThisReference node)
				{
					ParameterSymbol thisParameter = this._topLevelMethod.ThisParameter;
					if (thisParameter != null)
					{
						this.AddIfCaptured(thisParameter, node.Syntax);
					}
					return base.VisitThisReference(node);
				}

				// Token: 0x06009355 RID: 37717 RVA: 0x002025AA File Offset: 0x002007AA
				public override BoundNode VisitLabelStatement(BoundLabelStatement node)
				{
					this._labelsInScope.Peek<ArrayBuilder<LabelSymbol>>().Add(node.Label);
					this._scopesAfterLabel.Add(node.Label, ArrayBuilder<ClosureConversion.Analysis.Scope>.GetInstance());
					return base.VisitLabelStatement(node);
				}

				// Token: 0x06009356 RID: 37718 RVA: 0x002025DF File Offset: 0x002007DF
				public override BoundNode VisitGotoStatement(BoundGotoStatement node)
				{
					this.CheckCanMergeWithParent(node.Label);
					return base.VisitGotoStatement(node);
				}

				// Token: 0x06009357 RID: 37719 RVA: 0x002025F4 File Offset: 0x002007F4
				public override BoundNode VisitConditionalGoto(BoundConditionalGoto node)
				{
					this.CheckCanMergeWithParent(node.Label);
					return base.VisitConditionalGoto(node);
				}

				// Token: 0x06009358 RID: 37720 RVA: 0x0020260C File Offset: 0x0020080C
				private void CheckCanMergeWithParent(LabelSymbol jumpTarget)
				{
					ArrayBuilder<ClosureConversion.Analysis.Scope> arrayBuilder;
					if (this._scopesAfterLabel.TryGetValue(jumpTarget, ref arrayBuilder))
					{
						foreach (ClosureConversion.Analysis.Scope scope in arrayBuilder)
						{
							scope.CanMergeWithParent = false;
						}
						arrayBuilder.Clear();
					}
				}

				// Token: 0x06009359 RID: 37721 RVA: 0x00202650 File Offset: 0x00200850
				[global::System.Runtime.CompilerServices.NullableContext(2)]
				private BoundNode VisitNestedFunction([global::System.Runtime.CompilerServices.Nullable(1)] MethodSymbol functionSymbol, BoundBlock body)
				{
					if (body == null)
					{
						this._currentScope.NestedFunctions.Add(new ClosureConversion.Analysis.NestedFunction(functionSymbol, null));
						return null;
					}
					ClosureConversion.Analysis.NestedFunction nestedFunction = new ClosureConversion.Analysis.NestedFunction(functionSymbol, body.Syntax.GetReference());
					this._currentScope.NestedFunctions.Add(nestedFunction);
					ClosureConversion.Analysis.NestedFunction currentFunction = this._currentFunction;
					this._currentFunction = nestedFunction;
					ClosureConversion.Analysis.Scope currentScope = this._currentScope;
					this.CreateAndPushScope(body);
					this.DeclareLocals<ParameterSymbol>(this._currentScope, functionSymbol.Parameters, this._inExpressionTree);
					BoundNode boundNode = (this._inExpressionTree ? base.VisitBlock(body) : this.VisitBlock(body));
					this.PopScope(currentScope);
					this._currentFunction = currentFunction;
					return boundNode;
				}

				// Token: 0x0600935A RID: 37722 RVA: 0x002026F8 File Offset: 0x002008F8
				private void AddIfCaptured(Symbol symbol, SyntaxNode syntax)
				{
					if (this._currentFunction == null)
					{
						return;
					}
					LocalSymbol localSymbol = symbol as LocalSymbol;
					if (localSymbol != null && localSymbol.IsConst)
					{
						return;
					}
					MethodSymbol methodSymbol = symbol as MethodSymbol;
					if (methodSymbol != null && this._currentFunction.OriginalMethodSymbol == methodSymbol)
					{
						return;
					}
					if (symbol.ContainingSymbol != this._currentFunction.OriginalMethodSymbol)
					{
						this.AddDiagnosticIfRestrictedType(symbol, syntax);
						ClosureConversion.Analysis.Scope scope = this._currentScope;
						ClosureConversion.Analysis.NestedFunction nestedFunction = this._currentFunction;
						while (nestedFunction != null && symbol.ContainingSymbol != nestedFunction.OriginalMethodSymbol)
						{
							nestedFunction.CapturedVariables.Add(symbol);
							while (scope.ContainingFunctionOpt == nestedFunction)
							{
								scope = scope.Parent;
							}
							nestedFunction = scope.ContainingFunctionOpt;
						}
						if (symbol.Kind == SymbolKind.Method)
						{
							return;
						}
						ClosureConversion.Analysis.Scope scope2;
						if (this._localToScope.TryGetValue(symbol, out scope2))
						{
							scope2.DeclaredVariables.Add(symbol);
						}
					}
				}

				// Token: 0x0600935B RID: 37723 RVA: 0x002027D4 File Offset: 0x002009D4
				private void AddDiagnosticIfRestrictedType(Symbol capturedVariable, SyntaxNode syntax)
				{
					SymbolKind kind = capturedVariable.Kind;
					TypeSymbol typeSymbol;
					if (kind != SymbolKind.Local)
					{
						if (kind != SymbolKind.Parameter)
						{
							return;
						}
						typeSymbol = ((ParameterSymbol)capturedVariable).Type;
					}
					else
					{
						typeSymbol = ((LocalSymbol)capturedVariable).Type;
					}
					if (typeSymbol.IsRestrictedType(false))
					{
						this._diagnostics.Add(ErrorCode.ERR_SpecialByRefInLambda, syntax.Location, new object[] { typeSymbol });
					}
				}

				// Token: 0x0600935C RID: 37724 RVA: 0x00202836 File Offset: 0x00200A36
				private void PushOrReuseScope<TSymbol>(BoundNode node, ImmutableArray<TSymbol> locals) where TSymbol : Symbol
				{
					if (!locals.IsEmpty && this._currentScope.BoundNode != node)
					{
						this.CreateAndPushScope(node);
					}
					this.DeclareLocals<TSymbol>(this._currentScope, locals, false);
				}

				// Token: 0x0600935D RID: 37725 RVA: 0x00202864 File Offset: 0x00200A64
				private void CreateAndPushScope(BoundNode node)
				{
					ClosureConversion.Analysis.ScopeTreeBuilder.<>c__DisplayClass32_0 CS$<>8__locals1;
					CS$<>8__locals1.node = node;
					ClosureConversion.Analysis.Scope scope = ClosureConversion.Analysis.ScopeTreeBuilder.<CreateAndPushScope>g__CreateNestedScope|32_0(this._currentScope, this._currentFunction, ref CS$<>8__locals1);
					foreach (LabelSymbol labelSymbol in this._labelsInScope.Peek<ArrayBuilder<LabelSymbol>>())
					{
						this._scopesAfterLabel[labelSymbol].Add(scope);
					}
					this._labelsInScope.Push(ArrayBuilder<LabelSymbol>.GetInstance());
					this._currentScope = scope;
				}

				// Token: 0x0600935E RID: 37726 RVA: 0x002028DC File Offset: 0x00200ADC
				private void PopScope(ClosureConversion.Analysis.Scope scope)
				{
					if (scope == this._currentScope)
					{
						return;
					}
					ArrayBuilder<LabelSymbol> arrayBuilder = this._labelsInScope.Pop<ArrayBuilder<LabelSymbol>>();
					foreach (LabelSymbol labelSymbol in arrayBuilder)
					{
						this._scopesAfterLabel[labelSymbol].Free();
						this._scopesAfterLabel.Remove(labelSymbol);
					}
					arrayBuilder.Free();
					this._currentScope = this._currentScope.Parent;
				}

				// Token: 0x0600935F RID: 37727 RVA: 0x00202950 File Offset: 0x00200B50
				private void DeclareLocals<TSymbol>(ClosureConversion.Analysis.Scope scope, ImmutableArray<TSymbol> locals, bool declareAsFree = false) where TSymbol : Symbol
				{
					foreach (TSymbol tsymbol in locals)
					{
						if (!declareAsFree)
						{
							this._localToScope.Add(tsymbol, scope);
						}
					}
				}

				// Token: 0x06009360 RID: 37728 RVA: 0x00202990 File Offset: 0x00200B90
				[CompilerGenerated]
				internal static ClosureConversion.Analysis.Scope <CreateAndPushScope>g__CreateNestedScope|32_0(ClosureConversion.Analysis.Scope parentScope, ClosureConversion.Analysis.NestedFunction currentFunction, ref ClosureConversion.Analysis.ScopeTreeBuilder.<>c__DisplayClass32_0 A_2)
				{
					ClosureConversion.Analysis.Scope scope = new ClosureConversion.Analysis.Scope(parentScope, A_2.node, currentFunction);
					parentScope.NestedScopes.Add(scope);
					return scope;
				}

				// Token: 0x040029E3 RID: 10723
				private ClosureConversion.Analysis.Scope _currentScope;

				// Token: 0x040029E4 RID: 10724
				private ClosureConversion.Analysis.NestedFunction _currentFunction;

				// Token: 0x040029E5 RID: 10725
				private bool _inExpressionTree;

				// Token: 0x040029E6 RID: 10726
				private readonly SmallDictionary<Symbol, ClosureConversion.Analysis.Scope> _localToScope = new SmallDictionary<Symbol, ClosureConversion.Analysis.Scope>();

				// Token: 0x040029E7 RID: 10727
				private readonly MethodSymbol _topLevelMethod;

				// Token: 0x040029E8 RID: 10728
				private readonly HashSet<MethodSymbol> _methodsConvertedToDelegates;

				// Token: 0x040029E9 RID: 10729
				private readonly DiagnosticBag _diagnostics;

				// Token: 0x040029EA RID: 10730
				private readonly PooledDictionary<LabelSymbol, ArrayBuilder<ClosureConversion.Analysis.Scope>> _scopesAfterLabel = PooledDictionary<LabelSymbol, ArrayBuilder<ClosureConversion.Analysis.Scope>>.GetInstance();

				// Token: 0x040029EB RID: 10731
				private readonly ArrayBuilder<ArrayBuilder<LabelSymbol>> _labelsInScope = ArrayBuilder<ArrayBuilder<LabelSymbol>>.GetInstance();
			}
		}
	}
}
