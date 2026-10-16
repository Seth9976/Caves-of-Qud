using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000157 RID: 343
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
	[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
	internal sealed class NullableWalker : LocalDataFlowPass<NullableWalker.LocalState, NullableWalker.LocalFunctionState>
	{
		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06001BB6 RID: 7094 RVA: 0x00091540 File Offset: 0x0008F740
		[TupleElementNames(new string[] { "State", "ResultType", "EndReachable" })]
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 1, 0, 0 })]
		private PooledDictionary<BoundExpression, ImmutableArray<ValueTuple<NullableWalker.LocalState, TypeWithState, bool>>> ConditionalInfoForConversion
		{
			[return: TupleElementNames(new string[] { "State", "ResultType", "EndReachable" })]
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 1, 0, 0 })]
			get
			{
				PooledDictionary<BoundExpression, ImmutableArray<ValueTuple<NullableWalker.LocalState, TypeWithState, bool>>> pooledDictionary;
				if ((pooledDictionary = this._conditionalInfoForConversionOpt) == null)
				{
					pooledDictionary = (this._conditionalInfoForConversionOpt = PooledDictionary<BoundExpression, ImmutableArray<ValueTuple<NullableWalker.LocalState, TypeWithState, bool>>>.GetInstance());
				}
				return pooledDictionary;
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06001BB7 RID: 7095 RVA: 0x00091565 File Offset: 0x0008F765
		private TypeWithState ResultType
		{
			get
			{
				return this._visitResult.RValueType;
			}
		}

		// Token: 0x06001BB8 RID: 7096 RVA: 0x00091574 File Offset: 0x0008F774
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private void SetResultType(BoundExpression expression, TypeWithState type, bool updateAnalyzedNullability = true)
		{
			this.SetResult(expression, type, type.ToTypeWithAnnotations(this.compilation, false), updateAnalyzedNullability, default(bool?));
		}

		// Token: 0x06001BB9 RID: 7097 RVA: 0x000915A4 File Offset: 0x0008F7A4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private void UseRvalueOnly(BoundExpression expression)
		{
			this.SetResult(expression, this.ResultType, this.ResultType.ToTypeWithAnnotations(this.compilation, false), true, new bool?(false));
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06001BBA RID: 7098 RVA: 0x000915DA File Offset: 0x0008F7DA
		private TypeWithAnnotations LvalueResultType
		{
			get
			{
				return this._visitResult.LValueType;
			}
		}

		// Token: 0x06001BBB RID: 7099 RVA: 0x000915E8 File Offset: 0x0008F7E8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private void SetLvalueResultType(BoundExpression expression, TypeWithAnnotations type)
		{
			this.SetResult(expression, type.ToTypeWithState(), type, true, default(bool?));
		}

		// Token: 0x06001BBC RID: 7100 RVA: 0x00091610 File Offset: 0x0008F810
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private void UseLvalueOnly(BoundExpression expression)
		{
			this.SetResult(expression, this.LvalueResultType.ToTypeWithState(), this.LvalueResultType, true, new bool?(true));
		}

		// Token: 0x06001BBD RID: 7101 RVA: 0x00091640 File Offset: 0x0008F840
		private void SetInvalidResult()
		{
			this.SetResult(null, NullableWalker._invalidType, NullableWalker._invalidType.ToTypeWithAnnotations(this.compilation, false), false, default(bool?));
		}

		// Token: 0x06001BBE RID: 7102 RVA: 0x00091674 File Offset: 0x0008F874
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private void SetResult(BoundExpression expression, TypeWithState resultType, TypeWithAnnotations lvalueType, bool updateAnalyzedNullability = true, bool? isLvalue = null)
		{
			this._visitResult = new NullableWalker.VisitResult(resultType, lvalueType);
			if (updateAnalyzedNullability)
			{
				this.SetAnalyzedNullability(expression, this._visitResult, isLvalue);
			}
		}

		// Token: 0x06001BBF RID: 7103 RVA: 0x00091696 File Offset: 0x0008F896
		private bool ShouldMakeNotNullRvalue(BoundExpression node)
		{
			return node.IsSuppressed || node.HasAnyErrors || !this.IsReachable();
		}

		// Token: 0x06001BC0 RID: 7104 RVA: 0x000916B4 File Offset: 0x0008F8B4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private void SetAnalyzedNullability(BoundExpression expr, NullableWalker.VisitResult result, bool? isLvalue = null)
		{
			if (expr == null || this._disableNullabilityAnalysis)
			{
				return;
			}
			if (this._analyzedNullabilityMapOpt != null)
			{
				ImmutableDictionary<BoundExpression, ValueTuple<NullabilityInfo, TypeSymbol>>.Builder analyzedNullabilityMapOpt = this._analyzedNullabilityMapOpt;
				NullabilityInfo nullabilityInfo = new NullabilityInfo(result.LValueType.ToPublicAnnotation(), result.RValueType.State.ToPublicFlowState());
				TypeSymbol type = expr.Type;
				analyzedNullabilityMapOpt[expr] = new ValueTuple<NullabilityInfo, TypeSymbol>(nullabilityInfo, (type != null && type.Equals(result.RValueType.Type, TypeCompareKind.AllIgnoreOptions)) ? result.RValueType.Type : expr.Type);
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06001BC1 RID: 7105 RVA: 0x0009173A File Offset: 0x0008F93A
		private bool IsAnalyzingAttribute
		{
			get
			{
				return this.methodMainNode.Kind == BoundKind.Attribute;
			}
		}

		// Token: 0x06001BC2 RID: 7106 RVA: 0x00091750 File Offset: 0x0008F950
		protected override void Free()
		{
			PooledDictionary<MethodSymbol, NullableWalker.Variables> nestedFunctionVariables = this._nestedFunctionVariables;
			if (nestedFunctionVariables != null)
			{
				nestedFunctionVariables.Free();
			}
			PooledDictionary<BoundAwaitableValuePlaceholder, ValueTuple<BoundExpression, NullableWalker.VisitResult>> awaitablePlaceholdersOpt = this._awaitablePlaceholdersOpt;
			if (awaitablePlaceholdersOpt != null)
			{
				awaitablePlaceholdersOpt.Free();
			}
			PooledDictionary<BoundExpression, TypeWithState> methodGroupReceiverMapOpt = this._methodGroupReceiverMapOpt;
			if (methodGroupReceiverMapOpt != null)
			{
				methodGroupReceiverMapOpt.Free();
			}
			PooledDictionary<object, NullableWalker.PlaceholderLocal> placeholderLocalsOpt = this._placeholderLocalsOpt;
			if (placeholderLocalsOpt != null)
			{
				placeholderLocalsOpt.Free();
			}
			this._variables.Free();
			PooledDictionary<BoundExpression, ImmutableArray<ValueTuple<NullableWalker.LocalState, TypeWithState, bool>>> conditionalInfoForConversionOpt = this._conditionalInfoForConversionOpt;
			if (conditionalInfoForConversionOpt != null)
			{
				conditionalInfoForConversionOpt.Free();
			}
			base.Free();
		}

		// Token: 0x06001BC3 RID: 7107 RVA: 0x000917C4 File Offset: 0x0008F9C4
		private NullableWalker(CSharpCompilation compilation, [global::System.Runtime.CompilerServices.Nullable(2)] Symbol symbol, bool useConstructorExitWarnings, bool useDelegateInvokeParameterTypes, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol delegateInvokeMethodOpt, BoundNode node, Binder binder, Conversions conversions, [global::System.Runtime.CompilerServices.Nullable(2)] NullableWalker.Variables variables, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 0, 1 })] ArrayBuilder<ValueTuple<BoundReturnStatement, TypeWithAnnotations>> returnTypesOpt, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1, 0, 2 })] ImmutableDictionary<BoundExpression, ValueTuple<NullabilityInfo, TypeSymbol>>.Builder analyzedNullabilityMapOpt, [global::System.Runtime.CompilerServices.Nullable(2)] NullableWalker.SnapshotManager.Builder snapshotBuilderOpt, bool isSpeculative = false)
			: base(compilation, symbol, node, EmptyStructTypeCache.CreatePrecise(), true)
		{
			this._variables = variables ?? NullableWalker.Variables.Create(symbol);
			this._binder = binder;
			this._conversions = (Conversions)conversions.WithNullability(true);
			this._useConstructorExitWarnings = useConstructorExitWarnings;
			this._useDelegateInvokeParameterTypes = useDelegateInvokeParameterTypes;
			this._delegateInvokeMethod = delegateInvokeMethodOpt;
			this._analyzedNullabilityMapOpt = analyzedNullabilityMapOpt;
			this._returnTypesOpt = returnTypesOpt;
			this._snapshotBuilderOpt = snapshotBuilderOpt;
			this._isSpeculative = isSpeculative;
			this._hasInitialState = variables != null;
		}

		// Token: 0x06001BC4 RID: 7108 RVA: 0x00091860 File Offset: 0x0008FA60
		public string GetDebuggerDisplay()
		{
			if (this.IsConditionalState)
			{
				return string.Concat(new string[]
				{
					"{",
					base.GetType().Name,
					" WhenTrue:",
					this.Dump(this.StateWhenTrue),
					" WhenFalse:",
					this.Dump(this.StateWhenFalse),
					"}"
				});
			}
			return string.Concat(new string[]
			{
				"{",
				base.GetType().Name,
				" ",
				this.Dump(this.State),
				"}"
			});
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06001BC5 RID: 7109 RVA: 0x0009190D File Offset: 0x0008FB0D
		public sealed override bool AwaitUsingAndForeachAddsPendingBranch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001BC6 RID: 7110 RVA: 0x00091910 File Offset: 0x0008FB10
		protected override void EnsureSufficientExecutionStack(int recursionDepth)
		{
			if (recursionDepth > 20)
			{
				CSharpCompilation.NullableData nullableAnalysisData = this.compilation.NullableAnalysisData;
				if (nullableAnalysisData != null)
				{
					int maxRecursionDepth = nullableAnalysisData.MaxRecursionDepth;
					if (maxRecursionDepth > 0 && recursionDepth > maxRecursionDepth)
					{
						throw new InsufficientExecutionStackException();
					}
				}
			}
			base.EnsureSufficientExecutionStack(recursionDepth);
		}

		// Token: 0x06001BC7 RID: 7111 RVA: 0x0009194D File Offset: 0x0008FB4D
		protected override bool ConvertInsufficientExecutionStackExceptionToCancelledByStackGuardException()
		{
			return true;
		}

		// Token: 0x06001BC8 RID: 7112 RVA: 0x00091950 File Offset: 0x0008FB50
		protected override bool TryGetVariable([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] LocalDataFlowPass<NullableWalker.LocalState, NullableWalker.LocalFunctionState>.VariableIdentifier identifier, out int slot)
		{
			return this._variables.TryGetValue(identifier, out slot);
		}

		// Token: 0x06001BC9 RID: 7113 RVA: 0x0009195F File Offset: 0x0008FB5F
		protected override int AddVariable([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] LocalDataFlowPass<NullableWalker.LocalState, NullableWalker.LocalFunctionState>.VariableIdentifier identifier)
		{
			return this._variables.Add(identifier);
		}

		// Token: 0x06001BCA RID: 7114 RVA: 0x00091970 File Offset: 0x0008FB70
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1, 1 })]
		protected override ImmutableArray<AbstractFlowPass<NullableWalker.LocalState, NullableWalker.LocalFunctionState>.PendingBranch> Scan(ref bool badRegion)
		{
			if (this._returnTypesOpt != null)
			{
				this._returnTypesOpt.Clear();
			}
			base.Diagnostics.Clear();
			this.regionPlace = RegionPlace.Before;
			if (!this._isSpeculative)
			{
				ParameterSymbol methodThisParameter = base.MethodThisParameter;
				this.EnterParameters();
				if (methodThisParameter != null)
				{
					this.EnterParameter(methodThisParameter, methodThisParameter.TypeWithAnnotations);
				}
				this.<Scan>g__makeNotNullMembersMaybeNull|52_8();
				NullableWalker.SnapshotManager.Builder snapshotBuilderOpt = this._snapshotBuilderOpt;
				if (snapshotBuilderOpt != null)
				{
					snapshotBuilderOpt.TakeIncrementalSnapshot(this.methodMainNode, this.State);
				}
			}
			ImmutableArray<AbstractFlowPass<NullableWalker.LocalState, NullableWalker.LocalFunctionState>.PendingBranch> immutableArray = base.Scan(ref badRegion);
			MethodSymbol methodSymbol = this._symbol as MethodSymbol;
			if (methodSymbol == null || !methodSymbol.IsConstructor() || this._useConstructorExitWarnings)
			{
				this.EnforceDoesNotReturn(null);
				this.<Scan>g__enforceMemberNotNull|52_0(null, this.State);
				this.<Scan>g__enforceNotNull|52_13(null, this.State);
				foreach (AbstractFlowPass<NullableWalker.LocalState, NullableWalker.LocalFunctionState>.PendingBranch pendingBranch in immutableArray)
				{
					this.<Scan>g__enforceMemberNotNull|52_0(pendingBranch.Branch.Syntax, pendingBranch.State);
					BoundReturnStatement boundReturnStatement = pendingBranch.Branch as BoundReturnStatement;
					if (boundReturnStatement != null)
					{
						this.<Scan>g__enforceNotNull|52_13(boundReturnStatement.Syntax, pendingBranch.State);
						this.<Scan>g__enforceNotNullWhenForPendingReturn|52_11(pendingBranch, boundReturnStatement);
						this.<Scan>g__enforceMemberNotNullWhenForPendingReturn|52_3(pendingBranch, boundReturnStatement);
					}
				}
			}
			return immutableArray;
		}

		// Token: 0x06001BCB RID: 7115 RVA: 0x00091AA4 File Offset: 0x0008FCA4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private void EnforceNotNullIfNotNull(SyntaxNode syntaxOpt, NullableWalker.LocalState state, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<ParameterSymbol> parameters, [global::System.Runtime.CompilerServices.Nullable(1)] ImmutableHashSet<string> inputParamNames, NullableFlowState outputState, ParameterSymbol outputParam)
		{
			if (inputParamNames.IsEmpty || outputState.IsNotNull())
			{
				return;
			}
			foreach (ParameterSymbol parameterSymbol in parameters)
			{
				if (inputParamNames.Contains(parameterSymbol.Name))
				{
					int orCreateSlot = this.GetOrCreateSlot(parameterSymbol, 0, false, true);
					if (orCreateSlot > 0 && state[orCreateSlot].IsNotNull())
					{
						Location location = ((syntaxOpt != null) ? syntaxOpt.GetLocation() : null) ?? this.methodMainNode.Syntax.GetLastToken(false, false, false, false).GetLocation();
						if (outputParam != null)
						{
							base.Diagnostics.Add(ErrorCode.WRN_ParameterNotNullIfNotNull, location, new object[] { outputParam.Name, parameterSymbol.Name });
							return;
						}
						MethodSymbol methodSymbol = this.CurrentSymbol as MethodSymbol;
						if (methodSymbol != null && !methodSymbol.IsAsync)
						{
							base.Diagnostics.Add(ErrorCode.WRN_ReturnNotNullIfNotNull, location, new object[] { parameterSymbol.Name });
							return;
						}
						break;
					}
				}
			}
		}

		// Token: 0x06001BCC RID: 7116 RVA: 0x00091BB8 File Offset: 0x0008FDB8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private void EnforceDoesNotReturn(SyntaxNode syntaxOpt)
		{
			Symbol currentSymbol = this.CurrentSymbol;
			MethodSymbol methodSymbol = currentSymbol as MethodSymbol;
			if (methodSymbol != null && currentSymbol.ContainingSymbol is TypeSymbol && (methodSymbol.FlowAnalysisAnnotations & FlowAnalysisAnnotations.DoesNotReturn) == FlowAnalysisAnnotations.DoesNotReturn && this.IsReachable())
			{
				this.ReportDiagnostic(ErrorCode.WRN_ShouldNotReturn, ((syntaxOpt != null) ? syntaxOpt.GetLocation() : null) ?? this.methodMainNode.Syntax.GetLastToken(false, false, false, false).GetLocation(), Array.Empty<object>());
			}
		}

		// Token: 0x06001BCD RID: 7117 RVA: 0x00091C3C File Offset: 0x0008FE3C
		internal static void AnalyzeIfNeeded(CSharpCompilation compilation, MethodSymbol method, BoundNode node, DiagnosticBag diagnostics, bool useConstructorExitWarnings, [global::System.Runtime.CompilerServices.Nullable(2)] NullableWalker.VariableState initialNullableState, bool getFinalNullableState, [global::System.Runtime.CompilerServices.Nullable(2)] out NullableWalker.VariableState finalNullableState)
		{
			if (!NullableWalker.HasRequiredLanguageVersion(compilation) || !compilation.IsNullableAnalysisEnabledIn(method))
			{
				if (compilation.IsNullableAnalysisEnabledAlways)
				{
					NullableWalker.VariableState variableState;
					NullableWalker.Analyze(compilation, method, node, new DiagnosticBag(), false, null, false, out variableState, false);
				}
				finalNullableState = null;
				return;
			}
			NullableWalker.Analyze(compilation, method, node, diagnostics, useConstructorExitWarnings, initialNullableState, getFinalNullableState, out finalNullableState, true);
		}

		// Token: 0x06001BCE RID: 7118 RVA: 0x00091C8C File Offset: 0x0008FE8C
		private static void Analyze(CSharpCompilation compilation, MethodSymbol method, BoundNode node, DiagnosticBag diagnostics, bool useConstructorExitWarnings, [global::System.Runtime.CompilerServices.Nullable(2)] NullableWalker.VariableState initialNullableState, bool getFinalNullableState, [global::System.Runtime.CompilerServices.Nullable(2)] out NullableWalker.VariableState finalNullableState, bool requiresAnalysis = true)
		{
			if (method.IsImplicitlyDeclared && !method.IsImplicitConstructor && !method.IsScriptInitializer)
			{
				finalNullableState = null;
				return;
			}
			SynthesizedSimpleProgramEntryPointSymbol synthesizedSimpleProgramEntryPointSymbol = method as SynthesizedSimpleProgramEntryPointSymbol;
			Binder binder = ((synthesizedSimpleProgramEntryPointSymbol != null) ? synthesizedSimpleProgramEntryPointSymbol.GetBodyBinder(false) : compilation.GetBinderFactory(node.SyntaxTree, false).GetBinder(node.Syntax, null, null));
			Conversions conversions = binder.Conversions;
			NullableWalker.Analyze(compilation, method, node, binder, conversions, diagnostics, useConstructorExitWarnings, false, null, initialNullableState, null, null, null, getFinalNullableState, out finalNullableState, requiresAnalysis);
		}

		// Token: 0x06001BCF RID: 7119 RVA: 0x00091D08 File Offset: 0x0008FF08
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal static NullableWalker.VariableState GetAfterInitializersState([global::System.Runtime.CompilerServices.Nullable(1)] CSharpCompilation compilation, Symbol symbol)
		{
			MethodSymbol methodSymbol = symbol as MethodSymbol;
			if (methodSymbol != null && methodSymbol.IncludeFieldInitializersInBody())
			{
				SourceMemberContainerTypeSymbol sourceMemberContainerTypeSymbol = methodSymbol.ContainingType as SourceMemberContainerTypeSymbol;
				if (sourceMemberContainerTypeSymbol != null)
				{
					DiagnosticBag instance = DiagnosticBag.GetInstance();
					Binder.ProcessedFieldInitializers processedFieldInitializers = default(Binder.ProcessedFieldInitializers);
					Binder.BindFieldInitializers(compilation, null, methodSymbol.IsStatic ? sourceMemberContainerTypeSymbol.StaticInitializers : sourceMemberContainerTypeSymbol.InstanceInitializers, BindingDiagnosticBag.Discarded, ref processedFieldInitializers);
					NullableWalker.VariableState variableState;
					NullableWalker.AnalyzeIfNeeded(compilation, methodSymbol, InitializerRewriter.RewriteConstructor(processedFieldInitializers.BoundInitializers, methodSymbol), instance, false, null, true, out variableState);
					instance.Free();
					return variableState;
				}
			}
			return null;
		}

		// Token: 0x06001BD0 RID: 7120 RVA: 0x00091D8C File Offset: 0x0008FF8C
		internal static void AnalyzeWithoutRewrite(CSharpCompilation compilation, [global::System.Runtime.CompilerServices.Nullable(2)] Symbol symbol, BoundNode node, Binder binder, DiagnosticBag diagnostics, bool createSnapshots)
		{
			NullableWalker.AnalyzeWithSemanticInfo(compilation, symbol, node, binder, NullableWalker.GetAfterInitializersState(compilation, symbol), diagnostics, createSnapshots, false);
		}

		// Token: 0x06001BD1 RID: 7121 RVA: 0x00091DA4 File Offset: 0x0008FFA4
		internal static BoundNode AnalyzeAndRewrite(CSharpCompilation compilation, [global::System.Runtime.CompilerServices.Nullable(2)] Symbol symbol, BoundNode node, Binder binder, [global::System.Runtime.CompilerServices.Nullable(2)] NullableWalker.VariableState initialState, DiagnosticBag diagnostics, bool createSnapshots, [global::System.Runtime.CompilerServices.Nullable(2)] out NullableWalker.SnapshotManager snapshotManager, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1, 1 })] ref ImmutableDictionary<Symbol, Symbol> remappedSymbols)
		{
			ValueTuple<NullableWalker.SnapshotManager, ImmutableDictionary<BoundExpression, ValueTuple<NullabilityInfo, TypeSymbol>>> valueTuple = NullableWalker.AnalyzeWithSemanticInfo(compilation, symbol, node, binder, initialState, diagnostics, createSnapshots, true);
			snapshotManager = valueTuple.Item1;
			return NullableWalker.Rewrite(valueTuple.Item2, snapshotManager, node, ref remappedSymbols);
		}

		// Token: 0x06001BD2 RID: 7122 RVA: 0x00091DDC File Offset: 0x0008FFDC
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2, 1, 1, 0, 2 })]
		private static ValueTuple<NullableWalker.SnapshotManager, ImmutableDictionary<BoundExpression, ValueTuple<NullabilityInfo, TypeSymbol>>> AnalyzeWithSemanticInfo(CSharpCompilation compilation, [global::System.Runtime.CompilerServices.Nullable(2)] Symbol symbol, BoundNode node, Binder binder, [global::System.Runtime.CompilerServices.Nullable(2)] NullableWalker.VariableState initialState, DiagnosticBag diagnostics, bool createSnapshots, bool requiresAnalysis)
		{
			ImmutableDictionary<BoundExpression, ValueTuple<NullabilityInfo, TypeSymbol>>.Builder builder = ImmutableDictionary.CreateBuilder<BoundExpression, ValueTuple<NullabilityInfo, TypeSymbol>>(EqualityComparer<BoundExpression>.Default, NullableWalker.NullabilityInfoTypeComparer.Instance);
			NullableWalker.SnapshotManager.Builder builder2 = ((createSnapshots && symbol != null) ? new NullableWalker.SnapshotManager.Builder() : null);
			NullableWalker.VariableState variableState;
			NullableWalker.Analyze(compilation, symbol, node, binder, binder.Conversions, diagnostics, true, false, null, initialState, builder, builder2, null, false, out variableState, requiresAnalysis);
			ImmutableDictionary<BoundExpression, ValueTuple<NullabilityInfo, TypeSymbol>> immutableDictionary = builder.ToImmutable();
			return new ValueTuple<NullableWalker.SnapshotManager, ImmutableDictionary<BoundExpression, ValueTuple<NullabilityInfo, TypeSymbol>>>((builder2 != null) ? builder2.ToManagerAndFree() : null, immutableDictionary);
		}

		// Token: 0x06001BD3 RID: 7123 RVA: 0x00091E48 File Offset: 0x00090048
		internal static BoundNode AnalyzeAndRewriteSpeculation(int position, BoundNode node, Binder binder, NullableWalker.SnapshotManager originalSnapshots, out NullableWalker.SnapshotManager newSnapshots, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1, 1 })] ref ImmutableDictionary<Symbol, Symbol> remappedSymbols)
		{
			ImmutableDictionary<BoundExpression, ValueTuple<NullabilityInfo, TypeSymbol>>.Builder builder = ImmutableDictionary.CreateBuilder<BoundExpression, ValueTuple<NullabilityInfo, TypeSymbol>>(EqualityComparer<BoundExpression>.Default, NullableWalker.NullabilityInfoTypeComparer.Instance);
			NullableWalker.SnapshotManager.Builder builder2 = new NullableWalker.SnapshotManager.Builder();
			ValueTuple<NullableWalker.VariablesSnapshot, NullableWalker.LocalStateSnapshot> snapshot = originalSnapshots.GetSnapshot(position);
			NullableWalker.VariablesSnapshot item = snapshot.Item1;
			NullableWalker.LocalStateSnapshot item2 = snapshot.Item2;
			Symbol symbol = item.Symbol;
			NullableWalker nullableWalker = new NullableWalker(binder.Compilation, symbol, false, false, null, node, binder, binder.Conversions, NullableWalker.Variables.Create(item), null, builder, builder2, true);
			try
			{
				NullableWalker.Analyze(nullableWalker, symbol, null, NullableWalker.LocalState.Create(item2), builder2, true);
			}
			finally
			{
				nullableWalker.Free();
			}
			ImmutableDictionary<BoundExpression, ValueTuple<NullabilityInfo, TypeSymbol>> immutableDictionary = builder.ToImmutable();
			newSnapshots = builder2.ToManagerAndFree();
			return NullableWalker.Rewrite(immutableDictionary, newSnapshots, node, ref remappedSymbols);
		}

		// Token: 0x06001BD4 RID: 7124 RVA: 0x00091EF8 File Offset: 0x000900F8
		private static BoundNode Rewrite([global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 1, 0, 2 })] ImmutableDictionary<BoundExpression, ValueTuple<NullabilityInfo, TypeSymbol>> updatedNullabilities, [global::System.Runtime.CompilerServices.Nullable(2)] NullableWalker.SnapshotManager snapshotManager, BoundNode node, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1, 1 })] ref ImmutableDictionary<Symbol, Symbol> remappedSymbols)
		{
			ImmutableDictionary<Symbol, Symbol>.Builder builder = ImmutableDictionary.CreateBuilder<Symbol, Symbol>(SymbolEqualityComparer.ConsiderEverything, SymbolEqualityComparer.ConsiderEverything);
			if (remappedSymbols != null)
			{
				builder.AddRange(remappedSymbols);
			}
			BoundNode boundNode = new NullabilityRewriter(updatedNullabilities, snapshotManager, builder).Visit(node);
			remappedSymbols = builder.ToImmutable();
			return boundNode;
		}

		// Token: 0x06001BD5 RID: 7125 RVA: 0x00091F37 File Offset: 0x00090137
		private static bool HasRequiredLanguageVersion(CSharpCompilation compilation)
		{
			return compilation.LanguageVersion >= MessageID.IDS_FeatureNullableReferenceTypes.RequiredVersion();
		}

		// Token: 0x06001BD6 RID: 7126 RVA: 0x00091F4E File Offset: 0x0009014E
		internal static bool NeedsAnalysis(CSharpCompilation compilation, SyntaxNode syntaxNode)
		{
			return NullableWalker.HasRequiredLanguageVersion(compilation) && (compilation.IsNullableAnalysisEnabledIn(syntaxNode) || compilation.IsNullableAnalysisEnabledAlways);
		}

		// Token: 0x06001BD7 RID: 7127 RVA: 0x00091F6C File Offset: 0x0009016C
		internal static void AnalyzeIfNeeded(Binder binder, BoundNode node, SyntaxNode syntax, DiagnosticBag diagnostics)
		{
			bool flag = true;
			CSharpCompilation compilation = binder.Compilation;
			if (!NullableWalker.HasRequiredLanguageVersion(compilation) || !compilation.IsNullableAnalysisEnabledIn(syntax))
			{
				if (!compilation.IsNullableAnalysisEnabledAlways)
				{
					return;
				}
				diagnostics = new DiagnosticBag();
				flag = false;
			}
			NullableWalker.VariableState variableState;
			NullableWalker.Analyze(compilation, null, node, binder, binder.Conversions, diagnostics, false, false, null, null, null, null, null, false, out variableState, flag);
		}

		// Token: 0x06001BD8 RID: 7128 RVA: 0x00091FC0 File Offset: 0x000901C0
		internal static void Analyze(CSharpCompilation compilation, BoundLambda lambda, Conversions conversions, DiagnosticBag diagnostics, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol delegateInvokeMethodOpt, NullableWalker.VariableState initialState, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 0, 1 })] ArrayBuilder<ValueTuple<BoundReturnStatement, TypeWithAnnotations>> returnTypesOpt)
		{
			LambdaSymbol symbol = lambda.Symbol;
			NullableWalker.Variables variables = NullableWalker.Variables.Create(initialState.Variables).CreateNestedMethodScope(symbol);
			NullableWalker nullableWalker = new NullableWalker(compilation, symbol, false, NullableWalker.UseDelegateInvokeParameterTypes(lambda, delegateInvokeMethodOpt), delegateInvokeMethodOpt, lambda.Body, lambda.Binder, conversions, variables, returnTypesOpt, null, null, false);
			try
			{
				NullableWalker.LocalState localState = NullableWalker.LocalState.Create(initialState.VariableNullableStates).CreateNestedMethodState(variables);
				NullableWalker.Analyze(nullableWalker, symbol, diagnostics, localState, null, true);
			}
			finally
			{
				nullableWalker.Free();
			}
		}

		// Token: 0x06001BD9 RID: 7129 RVA: 0x00092050 File Offset: 0x00090250
		private static void Analyze(CSharpCompilation compilation, [global::System.Runtime.CompilerServices.Nullable(2)] Symbol symbol, BoundNode node, Binder binder, Conversions conversions, DiagnosticBag diagnostics, bool useConstructorExitWarnings, bool useDelegateInvokeParameterTypes, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol delegateInvokeMethodOpt, [global::System.Runtime.CompilerServices.Nullable(2)] NullableWalker.VariableState initialState, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1, 0, 2 })] ImmutableDictionary<BoundExpression, ValueTuple<NullabilityInfo, TypeSymbol>>.Builder analyzedNullabilityMapOpt, [global::System.Runtime.CompilerServices.Nullable(2)] NullableWalker.SnapshotManager.Builder snapshotBuilderOpt, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 0, 1 })] ArrayBuilder<ValueTuple<BoundReturnStatement, TypeWithAnnotations>> returnTypesOpt, bool getFinalNullableState, [global::System.Runtime.CompilerServices.Nullable(2)] out NullableWalker.VariableState finalNullableState, bool requiresAnalysis = true)
		{
			NullableWalker nullableWalker = new NullableWalker(compilation, symbol, useConstructorExitWarnings, useDelegateInvokeParameterTypes, delegateInvokeMethodOpt, node, binder, conversions, (initialState == null) ? null : NullableWalker.Variables.Create(initialState.Variables), returnTypesOpt, analyzedNullabilityMapOpt, snapshotBuilderOpt, false);
			finalNullableState = null;
			try
			{
				NullableWalker.Analyze(nullableWalker, symbol, diagnostics, (initialState == null) ? default(Optional<NullableWalker.LocalState>) : NullableWalker.LocalState.Create(initialState.VariableNullableStates), snapshotBuilderOpt, requiresAnalysis);
				if (getFinalNullableState)
				{
					finalNullableState = NullableWalker.GetVariableState(nullableWalker._variables, nullableWalker.State);
				}
			}
			finally
			{
				nullableWalker.Free();
			}
		}

		// Token: 0x06001BDA RID: 7130 RVA: 0x000920EC File Offset: 0x000902EC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private static void Analyze([global::System.Runtime.CompilerServices.Nullable(1)] NullableWalker walker, Symbol symbol, DiagnosticBag diagnostics, [global::System.Runtime.CompilerServices.Nullable(0)] Optional<NullableWalker.LocalState> initialState, NullableWalker.SnapshotManager.Builder snapshotBuilderOpt, bool requiresAnalysis = true)
		{
			int num = ((snapshotBuilderOpt != null) ? snapshotBuilderOpt.EnterNewWalker(symbol) : (-1));
			try
			{
				bool flag = false;
				walker.Analyze(ref flag, initialState);
				if (diagnostics != null)
				{
					diagnostics.AddRange(walker.Diagnostics);
				}
			}
			catch (BoundTreeVisitor.CancelledByStackGuardException ex) when (diagnostics != null)
			{
				ex.AddAnError(diagnostics);
			}
			finally
			{
				if (snapshotBuilderOpt != null)
				{
					snapshotBuilderOpt.ExitWalker(walker.SaveSharedState(), num);
				}
			}
			walker.RecordNullableAnalysisData(symbol, requiresAnalysis);
		}

		// Token: 0x06001BDB RID: 7131 RVA: 0x00092180 File Offset: 0x00090380
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private void RecordNullableAnalysisData(Symbol symbol, bool requiredAnalysis)
		{
			CSharpCompilation.NullableData nullableAnalysisData = this.compilation.NullableAnalysisData;
			ConcurrentDictionary<object, NullableWalker.Data> concurrentDictionary = ((nullableAnalysisData != null) ? nullableAnalysisData.Data : null);
			if (concurrentDictionary != null)
			{
				object obj = symbol ?? this.methodMainNode.Syntax;
				NullableWalker.Data data;
				if (!concurrentDictionary.TryGetValue(obj, ref data))
				{
					concurrentDictionary.TryAdd(obj, new NullableWalker.Data(this._variables.GetTotalVariableCount(), requiredAnalysis));
				}
			}
		}

		// Token: 0x06001BDC RID: 7132 RVA: 0x000921DD File Offset: 0x000903DD
		private NullableWalker.SharedWalkerState SaveSharedState()
		{
			return new NullableWalker.SharedWalkerState(this._variables.CreateSnapshot());
		}

		// Token: 0x06001BDD RID: 7133 RVA: 0x000921EF File Offset: 0x000903EF
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private void TakeIncrementalSnapshot(BoundNode node)
		{
			NullableWalker.SnapshotManager.Builder snapshotBuilderOpt = this._snapshotBuilderOpt;
			if (snapshotBuilderOpt == null)
			{
				return;
			}
			snapshotBuilderOpt.TakeIncrementalSnapshot(node, this.State);
		}

		// Token: 0x06001BDE RID: 7134 RVA: 0x00092208 File Offset: 0x00090408
		private void SetUpdatedSymbol(BoundNode node, Symbol originalSymbol, Symbol updatedSymbol)
		{
			if (this._snapshotBuilderOpt == null)
			{
				return;
			}
			bool flag = false;
			BoundLambda boundLambda = node as BoundLambda;
			if (boundLambda != null)
			{
				LambdaSymbol lambdaSymbol = originalSymbol as LambdaSymbol;
				if (lambdaSymbol != null)
				{
					NamedTypeSymbol namedTypeSymbol = updatedSymbol as NamedTypeSymbol;
					if (namedTypeSymbol != null)
					{
						if (!NullableWalker.AreLambdaAndNewDelegateSimilar(lambdaSymbol, namedTypeSymbol))
						{
							return;
						}
						flag = updatedSymbol.Equals(boundLambda.Type.GetDelegateType(), TypeCompareKind.ConsiderEverything);
					}
				}
			}
			if (flag || Symbol.Equals(originalSymbol, updatedSymbol, TypeCompareKind.ConsiderEverything))
			{
				this._snapshotBuilderOpt.RemoveSymbolIfPresent(node, originalSymbol);
				return;
			}
			this._snapshotBuilderOpt.SetUpdatedSymbol(node, originalSymbol, updatedSymbol);
		}

		// Token: 0x06001BDF RID: 7135 RVA: 0x00092284 File Offset: 0x00090484
		protected override void Normalize(ref NullableWalker.LocalState state)
		{
			if (!state.Reachable)
			{
				return;
			}
			state.Normalize(this, this._variables);
		}

		// Token: 0x06001BE0 RID: 7136 RVA: 0x0009229C File Offset: 0x0009049C
		private NullableFlowState GetDefaultState(ref NullableWalker.LocalState state, int slot)
		{
			if (!state.Reachable)
			{
				return NullableFlowState.NotNull;
			}
			Symbol symbol = this._variables[slot].Symbol;
			SymbolKind kind = symbol.Kind;
			switch (kind)
			{
			case SymbolKind.ErrorType:
				return NullableFlowState.NotNull;
			case SymbolKind.Event:
			case SymbolKind.Field:
				break;
			case SymbolKind.Label:
				goto IL_00B6;
			case SymbolKind.Local:
			{
				LocalSymbol localSymbol = (LocalSymbol)symbol;
				TypeWithAnnotations typeWithAnnotations;
				if (!this._variables.TryGetType(localSymbol, out typeWithAnnotations))
				{
					typeWithAnnotations = localSymbol.TypeWithAnnotations;
				}
				return typeWithAnnotations.ToTypeWithState().State;
			}
			default:
				if (kind == SymbolKind.Parameter)
				{
					ParameterSymbol parameterSymbol = (ParameterSymbol)symbol;
					TypeWithAnnotations typeWithAnnotations2;
					if (!this._variables.TryGetType(parameterSymbol, out typeWithAnnotations2))
					{
						typeWithAnnotations2 = parameterSymbol.TypeWithAnnotations;
					}
					return NullableWalker.GetParameterState(typeWithAnnotations2, parameterSymbol.FlowAnalysisAnnotations).State;
				}
				if (kind != SymbolKind.Property)
				{
					goto IL_00B6;
				}
				break;
			}
			return this.GetDefaultState(symbol);
			IL_00B6:
			throw ExceptionUtilities.UnexpectedValue(symbol.Kind);
		}

		// Token: 0x06001BE1 RID: 7137 RVA: 0x00092370 File Offset: 0x00090570
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		protected override bool TryGetReceiverAndMember([global::System.Runtime.CompilerServices.Nullable(1)] BoundExpression expr, out BoundExpression receiver, [NotNullWhen(true)] out Symbol member)
		{
			receiver = null;
			member = null;
			switch (expr.Kind)
			{
			case BoundKind.FieldAccess:
			{
				BoundFieldAccess boundFieldAccess = (BoundFieldAccess)expr;
				FieldSymbol fieldSymbol = boundFieldAccess.FieldSymbol;
				member = fieldSymbol;
				if (fieldSymbol.IsFixedSizeBuffer)
				{
					return false;
				}
				if (fieldSymbol.IsStatic)
				{
					return true;
				}
				receiver = boundFieldAccess.ReceiverOpt;
				break;
			}
			case BoundKind.PropertyAccess:
			{
				BoundPropertyAccess boundPropertyAccess = (BoundPropertyAccess)expr;
				PropertySymbol propertySymbol = boundPropertyAccess.PropertySymbol;
				member = propertySymbol;
				if (propertySymbol.IsStatic)
				{
					return true;
				}
				receiver = boundPropertyAccess.ReceiverOpt;
				break;
			}
			case BoundKind.EventAccess:
			{
				BoundEventAccess boundEventAccess = (BoundEventAccess)expr;
				EventSymbol eventSymbol = boundEventAccess.EventSymbol;
				member = eventSymbol;
				if (eventSymbol.IsStatic)
				{
					return true;
				}
				receiver = boundEventAccess.ReceiverOpt;
				break;
			}
			}
			return member != null && receiver != null && receiver.Kind != BoundKind.TypeExpression && receiver.Type != null;
		}

		// Token: 0x06001BE2 RID: 7138 RVA: 0x0009244A File Offset: 0x0009064A
		protected override int MakeSlot(BoundExpression node)
		{
			return this.<MakeSlot>g__makeSlot|76_0(node);
		}

		// Token: 0x06001BE3 RID: 7139 RVA: 0x00092453 File Offset: 0x00090653
		protected override int GetOrCreateSlot(Symbol symbol, int containingSlot = 0, bool forceSlotEvenIfEmpty = false, bool createIfMissing = true)
		{
			if (containingSlot > 0 && !this.IsSlotMember(containingSlot, symbol))
			{
				return -1;
			}
			return base.GetOrCreateSlot(symbol, containingSlot, forceSlotEvenIfEmpty, createIfMissing);
		}

		// Token: 0x06001BE4 RID: 7140 RVA: 0x00092470 File Offset: 0x00090670
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private void VisitAndUnsplitAll<T>([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<T> nodes) where T : BoundNode
		{
			if (nodes.IsDefault)
			{
				return;
			}
			foreach (T t in nodes)
			{
				this.Visit(t);
				base.Unsplit();
			}
		}

		// Token: 0x06001BE5 RID: 7141 RVA: 0x000924B8 File Offset: 0x000906B8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private void VisitWithoutDiagnostics(BoundNode node)
		{
			bool disableDiagnostics = this._disableDiagnostics;
			this._disableDiagnostics = true;
			this.Visit(node);
			this._disableDiagnostics = disableDiagnostics;
		}

		// Token: 0x06001BE6 RID: 7142 RVA: 0x000924E2 File Offset: 0x000906E2
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		protected override void VisitRvalue(BoundExpression node, bool isKnownToBeAnLvalue = false)
		{
			this.Visit(node);
			this.VisitRvalueEpilogue(node);
		}

		// Token: 0x06001BE7 RID: 7143 RVA: 0x000924F3 File Offset: 0x000906F3
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[MethodImpl(8)]
		private void VisitRvalueEpilogue(BoundExpression node)
		{
			base.Unsplit();
			this.UseRvalueOnly(node);
		}

		// Token: 0x06001BE8 RID: 7144 RVA: 0x00092502 File Offset: 0x00090702
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private TypeWithState VisitRvalueWithState(BoundExpression node)
		{
			this.VisitRvalue(node, false);
			return this.ResultType;
		}

		// Token: 0x06001BE9 RID: 7145 RVA: 0x00092512 File Offset: 0x00090712
		private TypeWithAnnotations VisitLvalueWithAnnotations(BoundExpression node)
		{
			this.VisitLValue(node);
			base.Unsplit();
			return this.LvalueResultType;
		}

		// Token: 0x06001BEA RID: 7146 RVA: 0x00092528 File Offset: 0x00090728
		private static object GetTypeAsDiagnosticArgument([global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol typeOpt)
		{
			return typeOpt ?? "<null>";
		}

		// Token: 0x06001BEB RID: 7147 RVA: 0x00092534 File Offset: 0x00090734
		private static object GetParameterAsDiagnosticArgument([global::System.Runtime.CompilerServices.Nullable(2)] ParameterSymbol parameterOpt)
		{
			if (parameterOpt != null)
			{
				return new FormattedSymbol(parameterOpt, SymbolDisplayFormat.ShortFormat);
			}
			return "";
		}

		// Token: 0x06001BEC RID: 7148 RVA: 0x0009254C File Offset: 0x0009074C
		private static object GetContainingSymbolAsDiagnosticArgument([global::System.Runtime.CompilerServices.Nullable(2)] ParameterSymbol parameterOpt)
		{
			Symbol symbol = ((parameterOpt != null) ? parameterOpt.ContainingSymbol : null);
			if (symbol != null)
			{
				return new FormattedSymbol(symbol, SymbolDisplayFormat.MinimallyQualifiedFormat);
			}
			return "";
		}

		// Token: 0x06001BED RID: 7149 RVA: 0x0009257C File Offset: 0x0009077C
		private static bool ShouldReportNullableAssignment(TypeWithAnnotations type, NullableFlowState state)
		{
			if (!type.HasType || type.Type.IsValueType)
			{
				return false;
			}
			NullableAnnotation nullableAnnotation = type.NullableAnnotation;
			if (nullableAnnotation - NullableAnnotation.Oblivious <= 1)
			{
				return false;
			}
			if (state != NullableFlowState.NotNull)
			{
				if (state == NullableFlowState.MaybeNull)
				{
					if (type.Type.IsTypeParameterDisallowingAnnotationInCSharp8())
					{
						TypeParameterSymbol typeParameterSymbol = type.Type as TypeParameterSymbol;
						if (typeParameterSymbol == null || !(typeParameterSymbol.IsNotNullable ?? false))
						{
							return false;
						}
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06001BEE RID: 7150 RVA: 0x000925F8 File Offset: 0x000907F8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private void ReportNullableAssignmentIfNecessary(BoundExpression value, TypeWithAnnotations targetType, TypeWithState valueType, bool useLegacyWarnings, NullableWalker.AssignmentKind assignmentKind = NullableWalker.AssignmentKind.Assignment, ParameterSymbol parameterOpt = null, Location location = null)
		{
			if (targetType.HasType && !targetType.Type.Equals(valueType.Type, TypeCompareKind.AllIgnoreOptions))
			{
				return;
			}
			if (value == null || (value.WasCompilerGenerated && assignmentKind == NullableWalker.AssignmentKind.Argument) || !NullableWalker.ShouldReportNullableAssignment(targetType, valueType.State))
			{
				return;
			}
			if (location == null)
			{
				location = value.Syntax.GetLocation();
			}
			if (NullableWalker.SkipReferenceConversions(value).IsSuppressed)
			{
				return;
			}
			ConstantValue constantValue = value.ConstantValue;
			if (constantValue != null && constantValue.IsNull && !useLegacyWarnings)
			{
				this.ReportDiagnostic((assignmentKind == NullableWalker.AssignmentKind.Return) ? ErrorCode.WRN_NullReferenceReturn : ErrorCode.WRN_NullAsNonNullable, location, Array.Empty<object>());
				return;
			}
			if (assignmentKind == NullableWalker.AssignmentKind.Argument)
			{
				this.ReportDiagnostic(ErrorCode.WRN_NullReferenceArgument, location, new object[]
				{
					NullableWalker.GetParameterAsDiagnosticArgument(parameterOpt),
					NullableWalker.GetContainingSymbolAsDiagnosticArgument(parameterOpt)
				});
				this.LearnFromNonNullTest(value, ref this.State);
				return;
			}
			if (!useLegacyWarnings)
			{
				this.ReportDiagnostic((assignmentKind == NullableWalker.AssignmentKind.Return) ? ErrorCode.WRN_NullReferenceReturn : ErrorCode.WRN_NullReferenceAssignment, location, Array.Empty<object>());
				return;
			}
			if (NullableWalker.<ReportNullableAssignmentIfNecessary>g__isMaybeDefaultValue|89_0(valueType) && !NullableWalker.<ReportNullableAssignmentIfNecessary>g__allowUnconstrainedTypeParameterAnnotations|89_1(this.compilation))
			{
				return;
			}
			this.ReportNonSafetyDiagnostic(location);
		}

		// Token: 0x06001BEF RID: 7151 RVA: 0x00092718 File Offset: 0x00090918
		internal static bool AreParameterAnnotationsCompatible(RefKind refKind, TypeWithAnnotations overriddenType, FlowAnalysisAnnotations overriddenAnnotations, TypeWithAnnotations overridingType, FlowAnalysisAnnotations overridingAnnotations, bool forRef = false)
		{
			NullableWalker.<>c__DisplayClass90_0 CS$<>8__locals1;
			CS$<>8__locals1.overridingType = overridingType;
			CS$<>8__locals1.overridingAnnotations = overridingAnnotations;
			CS$<>8__locals1.overriddenAnnotations = overriddenAnnotations;
			CS$<>8__locals1.overriddenType = overriddenType;
			if (refKind == RefKind.Ref)
			{
				return NullableWalker.AreParameterAnnotationsCompatible(RefKind.None, CS$<>8__locals1.overriddenType, CS$<>8__locals1.overriddenAnnotations, CS$<>8__locals1.overridingType, CS$<>8__locals1.overridingAnnotations, true) && NullableWalker.AreParameterAnnotationsCompatible(RefKind.Out, CS$<>8__locals1.overriddenType, CS$<>8__locals1.overriddenAnnotations, CS$<>8__locals1.overridingType, CS$<>8__locals1.overridingAnnotations, false);
			}
			if (refKind == RefKind.None || refKind == RefKind.In)
			{
				if (NullableWalker.<AreParameterAnnotationsCompatible>g__isBadAssignment|90_1(NullableWalker.GetParameterState(CS$<>8__locals1.overriddenType, CS$<>8__locals1.overriddenAnnotations), CS$<>8__locals1.overridingType, CS$<>8__locals1.overridingAnnotations))
				{
					return false;
				}
				bool flag = (CS$<>8__locals1.overridingAnnotations & FlowAnalysisAnnotations.NotNull) == FlowAnalysisAnnotations.NotNull;
				if ((CS$<>8__locals1.overriddenAnnotations & FlowAnalysisAnnotations.NotNull) == FlowAnalysisAnnotations.NotNull && !flag && !forRef)
				{
					return false;
				}
				bool flag2 = (CS$<>8__locals1.overridingAnnotations & FlowAnalysisAnnotations.MaybeNull) == FlowAnalysisAnnotations.MaybeNull;
				if ((CS$<>8__locals1.overriddenAnnotations & FlowAnalysisAnnotations.MaybeNull) == FlowAnalysisAnnotations.MaybeNull && !flag2 && !forRef)
				{
					return false;
				}
			}
			return refKind != RefKind.Out || (NullableWalker.<AreParameterAnnotationsCompatible>g__canAssignOutputValueWhen|90_0(true, ref CS$<>8__locals1) && NullableWalker.<AreParameterAnnotationsCompatible>g__canAssignOutputValueWhen|90_0(false, ref CS$<>8__locals1));
		}

		// Token: 0x06001BF0 RID: 7152 RVA: 0x00092824 File Offset: 0x00090A24
		private static bool IsDefaultValue(BoundExpression expr)
		{
			BoundKind kind = expr.Kind;
			if (kind - BoundKind.DefaultLiteral <= 1)
			{
				return true;
			}
			if (kind == BoundKind.Conversion)
			{
				BoundConversion boundConversion = (BoundConversion)expr;
				ConversionKind kind2 = boundConversion.Conversion.Kind;
				return (kind2 == ConversionKind.DefaultLiteral || kind2 == ConversionKind.NullLiteral) && NullableWalker.IsDefaultValue(boundConversion.Operand);
			}
			return false;
		}

		// Token: 0x06001BF1 RID: 7153 RVA: 0x00092875 File Offset: 0x00090A75
		private void ReportNullabilityMismatchInAssignment(SyntaxNode syntaxNode, object sourceType, object destinationType)
		{
			this.ReportDiagnostic(ErrorCode.WRN_NullabilityMismatchInAssignment, syntaxNode, new object[] { sourceType, destinationType });
		}

		// Token: 0x06001BF2 RID: 7154 RVA: 0x00092891 File Offset: 0x00090A91
		private void ReportNullabilityMismatchInAssignment(Location location, object sourceType, object destinationType)
		{
			this.ReportDiagnostic(ErrorCode.WRN_NullabilityMismatchInAssignment, location, new object[] { sourceType, destinationType });
		}

		// Token: 0x06001BF3 RID: 7155 RVA: 0x000928B0 File Offset: 0x00090AB0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private void TrackNullableStateForAssignment(BoundExpression valueOpt, TypeWithAnnotations targetType, int targetSlot, TypeWithState valueType, int valueSlot = -1)
		{
			if (this.State.Reachable)
			{
				if (!targetType.HasType)
				{
					return;
				}
				if (targetSlot <= 0 || targetSlot == valueSlot)
				{
					return;
				}
				if (!this.State.HasValue(targetSlot))
				{
					this.Normalize(ref this.State);
				}
				NullableFlowState state = valueType.State;
				this.SetStateAndTrackForFinally(ref this.State, targetSlot, state);
				this.InheritDefaultState(targetType.Type, targetSlot);
				if (NullableWalker.<TrackNullableStateForAssignment>g__areEquivalentTypes|94_0(targetType, valueType))
				{
					if (targetType.Type.IsReferenceType || targetType.TypeKind == TypeKind.TypeParameter || targetType.IsNullableType())
					{
						if (valueSlot > 0)
						{
							this.InheritNullableStateOfTrackableType(targetSlot, valueSlot, targetSlot);
							return;
						}
					}
					else if (EmptyStructTypeCache.IsTrackableStructType(targetType.Type))
					{
						this.InheritNullableStateOfTrackableStruct(targetType.Type, targetSlot, valueSlot, valueOpt != null && NullableWalker.IsDefaultValue(valueOpt), targetSlot);
					}
				}
			}
		}

		// Token: 0x06001BF4 RID: 7156 RVA: 0x00092986 File Offset: 0x00090B86
		private void ReportNonSafetyDiagnostic(Location location)
		{
			this.ReportDiagnostic(ErrorCode.WRN_ConvertingNullableToNonNullable, location, Array.Empty<object>());
		}

		// Token: 0x06001BF5 RID: 7157 RVA: 0x00092999 File Offset: 0x00090B99
		private void ReportDiagnostic(ErrorCode errorCode, SyntaxNode syntaxNode, params object[] arguments)
		{
			this.ReportDiagnostic(errorCode, syntaxNode.GetLocation(), arguments);
		}

		// Token: 0x06001BF6 RID: 7158 RVA: 0x000929A9 File Offset: 0x00090BA9
		private void ReportDiagnostic(ErrorCode errorCode, Location location, params object[] arguments)
		{
			if (this.IsReachable() && !this._disableDiagnostics)
			{
				base.Diagnostics.Add(errorCode, location, arguments);
			}
		}

		// Token: 0x06001BF7 RID: 7159 RVA: 0x000929CC File Offset: 0x00090BCC
		private void InheritNullableStateOfTrackableStruct(TypeSymbol targetType, int targetSlot, int valueSlot, bool isDefaultValue, int skipSlot = -1)
		{
			if (skipSlot < 0)
			{
				skipSlot = targetSlot;
			}
			if (!isDefaultValue && valueSlot > 0)
			{
				this.InheritNullableStateOfTrackableType(targetSlot, valueSlot, skipSlot);
				return;
			}
			foreach (FieldSymbol fieldSymbol in this._emptyStructTypeCache.GetStructInstanceFields(targetType))
			{
				this.InheritNullableStateOfMember(targetSlot, valueSlot, fieldSymbol, isDefaultValue, skipSlot);
			}
		}

		// Token: 0x06001BF8 RID: 7160 RVA: 0x00092A40 File Offset: 0x00090C40
		private bool IsSlotMember(int slot, Symbol possibleMember)
		{
			TypeSymbol containingType = possibleMember.ContainingType;
			TypeSymbol typeSymbol = this.NominalSlotType(slot);
			CompoundUseSiteInfo<AssemblySymbol> discarded = CompoundUseSiteInfo<AssemblySymbol>.Discarded;
			ConversionsBase conversionsBase = this._conversions.WithNullability(false);
			return conversionsBase.HasIdentityOrImplicitReferenceConversion(typeSymbol, containingType, ref discarded) || conversionsBase.HasBoxingConversion(typeSymbol, containingType, ref discarded);
		}

		// Token: 0x06001BF9 RID: 7161 RVA: 0x00092A88 File Offset: 0x00090C88
		private void InheritNullableStateOfMember(int targetContainerSlot, int valueContainerSlot, Symbol member, bool isDefaultValue, int skipSlot)
		{
			if (!this.IsSlotMember(targetContainerSlot, member))
			{
				return;
			}
			TypeWithAnnotations typeOrReturnType = member.GetTypeOrReturnType();
			if (typeOrReturnType.Type.IsReferenceType || typeOrReturnType.TypeKind == TypeKind.TypeParameter || typeOrReturnType.IsNullableType())
			{
				int orCreateSlot = this.GetOrCreateSlot(member, targetContainerSlot, false, true);
				if (orCreateSlot > 0)
				{
					NullableFlowState nullableFlowState = (isDefaultValue ? NullableFlowState.MaybeNull : typeOrReturnType.ToTypeWithState().State);
					int num = -1;
					if (valueContainerSlot > 0)
					{
						num = base.VariableSlot(member, valueContainerSlot);
						if (num == skipSlot)
						{
							return;
						}
						nullableFlowState = (this.State.HasValue(num) ? this.State[num] : NullableFlowState.NotNull);
					}
					this.SetStateAndTrackForFinally(ref this.State, orCreateSlot, nullableFlowState);
					if (num > 0)
					{
						this.InheritNullableStateOfTrackableType(orCreateSlot, num, skipSlot);
						return;
					}
				}
			}
			else if (EmptyStructTypeCache.IsTrackableStructType(typeOrReturnType.Type))
			{
				int orCreateSlot2 = this.GetOrCreateSlot(member, targetContainerSlot, false, true);
				if (orCreateSlot2 > 0)
				{
					int num2 = ((valueContainerSlot > 0) ? this.GetOrCreateSlot(member, valueContainerSlot, false, true) : (-1));
					if (num2 == skipSlot)
					{
						return;
					}
					this.InheritNullableStateOfTrackableStruct(typeOrReturnType.Type, orCreateSlot2, num2, isDefaultValue, skipSlot);
				}
			}
		}

		// Token: 0x06001BFA RID: 7162 RVA: 0x00092B90 File Offset: 0x00090D90
		private TypeSymbol NominalSlotType(int slot)
		{
			return this._variables[slot].Symbol.GetTypeOrReturnType().Type;
		}

		// Token: 0x06001BFB RID: 7163 RVA: 0x00092BBC File Offset: 0x00090DBC
		private void SetStateAndTrackForFinally(ref NullableWalker.LocalState state, int slot, NullableFlowState newState)
		{
			state[slot] = newState;
			if (newState != NullableFlowState.NotNull && this.NonMonotonicState.HasValue)
			{
				NullableWalker.LocalState value = this.NonMonotonicState.Value;
				if (value.HasVariable(slot))
				{
					value[slot] = newState.Join(value[slot]);
					this.NonMonotonicState = value;
				}
			}
		}

		// Token: 0x06001BFC RID: 7164 RVA: 0x00092C1C File Offset: 0x00090E1C
		protected override void JoinTryBlockState(ref NullableWalker.LocalState self, ref NullableWalker.LocalState other)
		{
			NullableWalker.LocalState stateForVariables = other.GetStateForVariables(self.Id);
			this.Join(ref self, ref stateForVariables);
		}

		// Token: 0x06001BFD RID: 7165 RVA: 0x00092C40 File Offset: 0x00090E40
		private void InheritDefaultState(TypeSymbol targetType, int targetSlot)
		{
			ArrayBuilder<ValueTuple<LocalDataFlowPass<NullableWalker.LocalState, NullableWalker.LocalFunctionState>.VariableIdentifier, int>> instance = ArrayBuilder<ValueTuple<LocalDataFlowPass<NullableWalker.LocalState, NullableWalker.LocalFunctionState>.VariableIdentifier, int>>.GetInstance();
			this._variables.GetMembers(instance, targetSlot);
			foreach (ValueTuple<LocalDataFlowPass<NullableWalker.LocalState, NullableWalker.LocalFunctionState>.VariableIdentifier, int> valueTuple in instance)
			{
				LocalDataFlowPass<NullableWalker.LocalState, NullableWalker.LocalFunctionState>.VariableIdentifier item = valueTuple.Item1;
				int item2 = valueTuple.Item2;
				Symbol symbol = NullableWalker.AsMemberOfType(targetType, item.Symbol);
				this.SetStateAndTrackForFinally(ref this.State, item2, this.GetDefaultState(symbol));
				this.InheritDefaultState(symbol.GetTypeOrReturnType().Type, item2);
			}
			instance.Free();
		}

		// Token: 0x06001BFE RID: 7166 RVA: 0x00092CC8 File Offset: 0x00090EC8
		private NullableFlowState GetDefaultState(Symbol symbol)
		{
			return NullableWalker.ApplyUnconditionalAnnotations(symbol.GetTypeOrReturnType().ToTypeWithState(), this.GetRValueAnnotations(symbol)).State;
		}

		// Token: 0x06001BFF RID: 7167 RVA: 0x00092CF4 File Offset: 0x00090EF4
		private void InheritNullableStateOfTrackableType(int targetSlot, int valueSlot, int skipSlot)
		{
			ArrayBuilder<ValueTuple<LocalDataFlowPass<NullableWalker.LocalState, NullableWalker.LocalFunctionState>.VariableIdentifier, int>> instance = ArrayBuilder<ValueTuple<LocalDataFlowPass<NullableWalker.LocalState, NullableWalker.LocalFunctionState>.VariableIdentifier, int>>.GetInstance();
			this._variables.GetMembers(instance, valueSlot);
			foreach (ValueTuple<LocalDataFlowPass<NullableWalker.LocalState, NullableWalker.LocalFunctionState>.VariableIdentifier, int> valueTuple in instance)
			{
				LocalDataFlowPass<NullableWalker.LocalState, NullableWalker.LocalFunctionState>.VariableIdentifier item = valueTuple.Item1;
				Symbol symbol = item.Symbol;
				this.InheritNullableStateOfMember(targetSlot, valueSlot, symbol, false, skipSlot);
			}
			instance.Free();
		}

		// Token: 0x06001C00 RID: 7168 RVA: 0x00092D50 File Offset: 0x00090F50
		protected override NullableWalker.LocalState TopState()
		{
			NullableWalker.LocalState localState = NullableWalker.LocalState.ReachableState(this._variables);
			localState.PopulateAll(this);
			return localState;
		}

		// Token: 0x06001C01 RID: 7169 RVA: 0x00092D72 File Offset: 0x00090F72
		protected override NullableWalker.LocalState UnreachableState()
		{
			return NullableWalker.LocalState.UnreachableState(this._variables);
		}

		// Token: 0x06001C02 RID: 7170 RVA: 0x00092D7F File Offset: 0x00090F7F
		protected override NullableWalker.LocalState ReachableBottomState()
		{
			return NullableWalker.LocalState.ReachableState(this._variables);
		}

		// Token: 0x06001C03 RID: 7171 RVA: 0x00092D8C File Offset: 0x00090F8C
		private void EnterParameters()
		{
			MethodSymbol methodSymbol = this.CurrentSymbol as MethodSymbol;
			if (methodSymbol == null)
			{
				return;
			}
			if (methodSymbol is SynthesizedRecordConstructor)
			{
				if (this._hasInitialState)
				{
					return;
				}
			}
			else if (methodSymbol.IsConstructor() && !this._hasInitialState)
			{
				return;
			}
			methodSymbol = methodSymbol.PartialDefinitionPart ?? methodSymbol;
			ImmutableArray<ParameterSymbol> parameters = methodSymbol.Parameters;
			ImmutableArray<ParameterSymbol> parameters2 = (this._useDelegateInvokeParameterTypes ? this._delegateInvokeMethod : methodSymbol).Parameters;
			NullableWalker.LocalState localState = this.State.Clone();
			for (int i = 0; i < parameters.Length; i++)
			{
				ParameterSymbol parameterSymbol = parameters[i];
				TypeWithAnnotations typeWithAnnotations = ((i >= parameters2.Length) ? parameterSymbol.TypeWithAnnotations : parameters2[i].TypeWithAnnotations);
				this.EnterParameter(parameterSymbol, typeWithAnnotations);
			}
			this.Join(ref this.State, ref localState);
		}

		// Token: 0x06001C04 RID: 7172 RVA: 0x00092E60 File Offset: 0x00091060
		private void EnterParameter(ParameterSymbol parameter, TypeWithAnnotations parameterType)
		{
			this._variables.SetType(parameter, parameterType);
			int orCreateSlot = this.GetOrCreateSlot(parameter, 0, false, true);
			if (orCreateSlot > 0)
			{
				NullableFlowState state = NullableWalker.GetParameterState(parameterType, parameter.FlowAnalysisAnnotations).State;
				this.State[orCreateSlot] = state;
				if (EmptyStructTypeCache.IsTrackableStructType(parameterType.Type))
				{
					TypeSymbol type = parameterType.Type;
					int num = orCreateSlot;
					int num2 = -1;
					ConstantValue explicitDefaultConstantValue = parameter.ExplicitDefaultConstantValue;
					this.InheritNullableStateOfTrackableStruct(type, num, num2, explicitDefaultConstantValue != null && explicitDefaultConstantValue.IsNull, -1);
				}
			}
		}

		// Token: 0x06001C05 RID: 7173 RVA: 0x00092ED8 File Offset: 0x000910D8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitParameterEqualsValue(BoundParameterEqualsValue equalsValue)
		{
			ParameterSymbol parameter = equalsValue.Parameter;
			FlowAnalysisAnnotations parameterAnnotations = this.GetParameterAnnotations(parameter);
			TypeWithAnnotations typeWithAnnotations = NullableWalker.ApplyLValueAnnotations(parameter.TypeWithAnnotations, parameterAnnotations);
			TypeWithState typeWithState = this.VisitOptionalImplicitConversion(equalsValue.Value, typeWithAnnotations, false, false, NullableWalker.AssignmentKind.Assignment);
			this.CheckDisallowedNullAssignment(typeWithState, parameterAnnotations, equalsValue.Value.Syntax.Location, null);
			return null;
		}

		// Token: 0x06001C06 RID: 7174 RVA: 0x00092F2D File Offset: 0x0009112D
		private static TypeWithState GetParameterState(TypeWithAnnotations parameterType, FlowAnalysisAnnotations parameterAnnotations)
		{
			if ((parameterAnnotations & FlowAnalysisAnnotations.AllowNull) != FlowAnalysisAnnotations.None)
			{
				return TypeWithState.Create(parameterType.Type, NullableFlowState.MaybeDefault);
			}
			if ((parameterAnnotations & FlowAnalysisAnnotations.DisallowNull) != FlowAnalysisAnnotations.None)
			{
				return TypeWithState.Create(parameterType.Type, NullableFlowState.NotNull);
			}
			return parameterType.ToTypeWithState();
		}

		// Token: 0x06001C07 RID: 7175 RVA: 0x00092F5C File Offset: 0x0009115C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public sealed override BoundNode VisitReturnStatement(BoundReturnStatement node)
		{
			BoundExpression expressionOpt = node.ExpressionOpt;
			if (expressionOpt == null)
			{
				this.EnforceDoesNotReturn(node.Syntax);
				base.PendingBranches.Add(new AbstractFlowPass<NullableWalker.LocalState, NullableWalker.LocalFunctionState>.PendingBranch(node, this.State, null, false, default(NullableWalker.LocalState), default(NullableWalker.LocalState)));
				base.SetUnreachable();
				return null;
			}
			TypeWithAnnotations typeWithAnnotations;
			FlowAnalysisAnnotations flowAnalysisAnnotations;
			if (this._returnTypesOpt == null && this.TryGetReturnType(out typeWithAnnotations, out flowAnalysisAnnotations))
			{
				if (node.RefKind == RefKind.None && typeWithAnnotations.Type.SpecialType == SpecialType.System_Boolean)
				{
					this.Visit(expressionOpt);
				}
				else
				{
					TypeWithState typeWithState;
					if (node.RefKind == RefKind.None)
					{
						typeWithState = this.VisitOptionalImplicitConversion(expressionOpt, typeWithAnnotations, false, false, NullableWalker.AssignmentKind.Return);
					}
					else
					{
						typeWithState = this.VisitRefExpression(expressionOpt, typeWithAnnotations);
					}
					this.CheckDisallowedNullAssignment(typeWithState, NullableWalker.ToInwardAnnotations(flowAnalysisAnnotations), node.Syntax.Location, expressionOpt);
				}
			}
			else
			{
				TypeWithState typeWithState2 = this.VisitRvalueWithState(expressionOpt);
				if (this._returnTypesOpt != null)
				{
					this._returnTypesOpt.Add(new ValueTuple<BoundReturnStatement, TypeWithAnnotations>(node, typeWithState2.ToTypeWithAnnotations(this.compilation, false)));
				}
			}
			this.EnforceDoesNotReturn(node.Syntax);
			if (this.IsConditionalState)
			{
				NullableWalker.LocalState localState = this.StateWhenTrue.Clone();
				this.Join(ref localState, ref this.StateWhenFalse);
				base.PendingBranches.Add(new AbstractFlowPass<NullableWalker.LocalState, NullableWalker.LocalFunctionState>.PendingBranch(node, localState, null, this.IsConditionalState, this.StateWhenTrue, this.StateWhenFalse));
			}
			else
			{
				base.PendingBranches.Add(new AbstractFlowPass<NullableWalker.LocalState, NullableWalker.LocalFunctionState>.PendingBranch(node, this.State, null, false, default(NullableWalker.LocalState), default(NullableWalker.LocalState)));
			}
			base.Unsplit();
			MethodSymbol methodSymbol = this.CurrentSymbol as MethodSymbol;
			if (methodSymbol != null)
			{
				this.EnforceNotNullIfNotNull(node.Syntax, this.State, methodSymbol.Parameters, methodSymbol.ReturnNotNullIfParameterNotNull, this.ResultType.State, null);
			}
			base.SetUnreachable();
			return null;
		}

		// Token: 0x06001C08 RID: 7176 RVA: 0x00093124 File Offset: 0x00091324
		private TypeWithState VisitRefExpression(BoundExpression expr, TypeWithAnnotations destinationType)
		{
			this.Visit(expr);
			TypeWithState resultType = this.ResultType;
			if (!expr.IsSuppressed && NullableWalker.RemoveConversion(expr, false).Item1.Kind != BoundKind.ThrowExpression)
			{
				TypeWithAnnotations lvalueResultType = this.LvalueResultType;
				if (NullableWalker.IsNullabilityMismatch(lvalueResultType, destinationType))
				{
					this.ReportNullabilityMismatchInAssignment(expr.Syntax, lvalueResultType, destinationType);
				}
				else
				{
					this.ReportNullableAssignmentIfNecessary(expr, destinationType, resultType, false, NullableWalker.AssignmentKind.Assignment, null, null);
				}
			}
			return resultType;
		}

		// Token: 0x06001C09 RID: 7177 RVA: 0x00093198 File Offset: 0x00091398
		private bool TryGetReturnType(out TypeWithAnnotations type, out FlowAnalysisAnnotations annotations)
		{
			MethodSymbol methodSymbol = this.CurrentSymbol as MethodSymbol;
			if (methodSymbol == null)
			{
				type = default(TypeWithAnnotations);
				annotations = FlowAnalysisAnnotations.None;
				return false;
			}
			MethodSymbol methodSymbol2 = this._delegateInvokeMethod ?? methodSymbol;
			TypeWithAnnotations returnTypeWithAnnotations = methodSymbol2.ReturnTypeWithAnnotations;
			if (returnTypeWithAnnotations.IsVoidType())
			{
				type = default(TypeWithAnnotations);
				annotations = FlowAnalysisAnnotations.None;
				return false;
			}
			if (!methodSymbol.IsAsync)
			{
				annotations = methodSymbol2.ReturnTypeFlowAnalysisAnnotations;
				type = NullableWalker.ApplyUnconditionalAnnotations(returnTypeWithAnnotations, annotations);
				return true;
			}
			if (returnTypeWithAnnotations.Type.IsGenericTaskType(this.compilation))
			{
				type = ((NamedTypeSymbol)returnTypeWithAnnotations.Type).TypeArgumentsWithAnnotationsNoUseSiteDiagnostics.Single<TypeWithAnnotations>();
				annotations = FlowAnalysisAnnotations.None;
				return true;
			}
			type = default(TypeWithAnnotations);
			annotations = FlowAnalysisAnnotations.None;
			return false;
		}

		// Token: 0x06001C0A RID: 7178 RVA: 0x00093248 File Offset: 0x00091448
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitLocal(BoundLocal node)
		{
			LocalSymbol localSymbol = node.LocalSymbol;
			int orCreateSlot = this.GetOrCreateSlot(localSymbol, 0, false, true);
			TypeWithAnnotations typeWithAnnotations = this.GetDeclaredLocalResult(localSymbol);
			if (!node.Type.Equals(typeWithAnnotations.Type, TypeCompareKind.IgnoreDynamic | TypeCompareKind.IgnoreTupleNames | TypeCompareKind.IgnoreNullableModifiersForReferenceTypes))
			{
				typeWithAnnotations = TypeWithAnnotations.Create(node.Type, typeWithAnnotations.NullableAnnotation, default(ImmutableArray<CustomModifier>));
			}
			this.SetResult(node, this.GetAdjustedResult(typeWithAnnotations.ToTypeWithState(), orCreateSlot), typeWithAnnotations, true, default(bool?));
			this.SplitIfBooleanConstant(node);
			return null;
		}

		// Token: 0x06001C0B RID: 7179 RVA: 0x000932C9 File Offset: 0x000914C9
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitBlock(BoundBlock node)
		{
			this.DeclareLocals(node.Locals);
			this.VisitStatementsWithLocalFunctions(node);
			return null;
		}

		// Token: 0x06001C0C RID: 7180 RVA: 0x000932E0 File Offset: 0x000914E0
		private void VisitStatementsWithLocalFunctions(BoundBlock block)
		{
			if (!this.TrackingRegions && !block.LocalFunctions.IsDefaultOrEmpty)
			{
				foreach (BoundStatement boundStatement in block.Statements)
				{
					if (boundStatement.Kind != BoundKind.LocalFunctionStatement)
					{
						this.VisitStatement(boundStatement);
					}
				}
				foreach (BoundStatement boundStatement2 in block.Statements)
				{
					BoundLocalFunctionStatement boundLocalFunctionStatement = boundStatement2 as BoundLocalFunctionStatement;
					if (boundLocalFunctionStatement != null)
					{
						this.VisitLocalFunctionStatement(boundLocalFunctionStatement);
					}
				}
				return;
			}
			foreach (BoundStatement boundStatement3 in block.Statements)
			{
				this.VisitStatement(boundStatement3);
			}
		}

		// Token: 0x06001C0D RID: 7181 RVA: 0x0009339C File Offset: 0x0009159C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitLocalFunctionStatement(BoundLocalFunctionStatement node)
		{
			LocalFunctionSymbol localFunc = node.Symbol;
			NullableWalker.LocalFunctionState orCreateLocalFuncUsages = base.GetOrCreateLocalFuncUsages(localFunc);
			NullableWalker.LocalState state = this.TopState();
			NullableWalker.LocalState startingState = orCreateLocalFuncUsages.StartingState;
			startingState.ForEach<NullableWalker.Variables>(delegate(int slot, NullableWalker.Variables variables)
			{
				if (Symbol.IsCaptured(variables[variables.RootSlot(slot)].Symbol, localFunc))
				{
					state[slot] = startingState[slot];
				}
			}, this._variables);
			orCreateLocalFuncUsages.Visited = true;
			this.AnalyzeLocalFunctionOrLambda(node, localFunc, state, null, false);
			this.SetInvalidResult();
			return null;
		}

		// Token: 0x06001C0E RID: 7182 RVA: 0x00093420 File Offset: 0x00091620
		private NullableWalker.Variables GetOrCreateNestedFunctionVariables(NullableWalker.Variables container, MethodSymbol lambdaOrLocalFunction)
		{
			if (this._nestedFunctionVariables == null)
			{
				this._nestedFunctionVariables = PooledDictionary<MethodSymbol, NullableWalker.Variables>.GetInstance();
			}
			NullableWalker.Variables variables;
			if (!this._nestedFunctionVariables.TryGetValue(lambdaOrLocalFunction, ref variables))
			{
				variables = container.CreateNestedMethodScope(lambdaOrLocalFunction);
				this._nestedFunctionVariables.Add(lambdaOrLocalFunction, variables);
			}
			return variables;
		}

		// Token: 0x06001C0F RID: 7183 RVA: 0x00093468 File Offset: 0x00091668
		private void AnalyzeLocalFunctionOrLambda(IBoundLambdaOrFunction lambdaOrFunction, MethodSymbol lambdaOrFunctionSymbol, NullableWalker.LocalState state, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol delegateInvokeMethod, bool useDelegateInvokeParameterTypes)
		{
			Symbol currentSymbol = this.CurrentSymbol;
			this.CurrentSymbol = lambdaOrFunctionSymbol;
			MethodSymbol delegateInvokeMethod2 = this._delegateInvokeMethod;
			this._delegateInvokeMethod = delegateInvokeMethod;
			bool useDelegateInvokeParameterTypes2 = this._useDelegateInvokeParameterTypes;
			this._useDelegateInvokeParameterTypes = useDelegateInvokeParameterTypes;
			ArrayBuilder<ValueTuple<BoundReturnStatement, TypeWithAnnotations>> returnTypesOpt = this._returnTypesOpt;
			this._returnTypesOpt = null;
			NullableWalker.LocalState state2 = this.State;
			this._variables = this.GetOrCreateNestedFunctionVariables(this._variables, lambdaOrFunctionSymbol);
			this.State = state.CreateNestedMethodState(this._variables);
			NullableWalker.SnapshotManager.Builder snapshotBuilderOpt = this._snapshotBuilderOpt;
			int num = ((snapshotBuilderOpt != null) ? snapshotBuilderOpt.EnterNewWalker(lambdaOrFunctionSymbol) : (-1));
			try
			{
				AbstractFlowPass<NullableWalker.LocalState, NullableWalker.LocalFunctionState>.SavedPending savedPending = base.SavePending();
				this.EnterParameters();
				AbstractFlowPass<NullableWalker.LocalState, NullableWalker.LocalFunctionState>.SavedPending savedPending2 = base.SavePending();
				if (lambdaOrFunctionSymbol.IsIterator)
				{
					base.PendingBranches.Add(new AbstractFlowPass<NullableWalker.LocalState, NullableWalker.LocalFunctionState>.PendingBranch(null, this.State, null, false, default(NullableWalker.LocalState), default(NullableWalker.LocalState)));
				}
				base.VisitAlways(lambdaOrFunction.Body);
				base.RestorePending(savedPending2);
				this.RemoveReturns();
				base.RestorePending(savedPending);
			}
			finally
			{
				NullableWalker.SnapshotManager.Builder snapshotBuilderOpt2 = this._snapshotBuilderOpt;
				if (snapshotBuilderOpt2 != null)
				{
					snapshotBuilderOpt2.ExitWalker(this.SaveSharedState(), num);
				}
			}
			this._variables = this._variables.Container;
			this.State = state2;
			this._returnTypesOpt = returnTypesOpt;
			this._useDelegateInvokeParameterTypes = useDelegateInvokeParameterTypes2;
			this._delegateInvokeMethod = delegateInvokeMethod2;
			this.CurrentSymbol = currentSymbol;
		}

		// Token: 0x06001C10 RID: 7184 RVA: 0x000935C4 File Offset: 0x000917C4
		protected override void VisitLocalFunctionUse(LocalFunctionSymbol symbol, NullableWalker.LocalFunctionState localFunctionState, SyntaxNode syntax, bool isCall)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06001C11 RID: 7185 RVA: 0x000935CC File Offset: 0x000917CC
		private void VisitLocalFunctionUse(LocalFunctionSymbol symbol)
		{
			NullableWalker.LocalFunctionState orCreateLocalFuncUsages = base.GetOrCreateLocalFuncUsages(symbol);
			NullableWalker.LocalState stateForVariables = this.State.GetStateForVariables(orCreateLocalFuncUsages.StartingState.Id);
			if (this.Join(ref orCreateLocalFuncUsages.StartingState, ref stateForVariables) && orCreateLocalFuncUsages.Visited)
			{
				this.stateChangedAfterUse = true;
			}
		}

		// Token: 0x06001C12 RID: 7186 RVA: 0x00093617 File Offset: 0x00091817
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDoStatement(BoundDoStatement node)
		{
			this.DeclareLocals(node.Locals);
			return base.VisitDoStatement(node);
		}

		// Token: 0x06001C13 RID: 7187 RVA: 0x0009362C File Offset: 0x0009182C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitWhileStatement(BoundWhileStatement node)
		{
			this.DeclareLocals(node.Locals);
			return base.VisitWhileStatement(node);
		}

		// Token: 0x06001C14 RID: 7188 RVA: 0x00093644 File Offset: 0x00091844
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitWithExpression(BoundWithExpression withExpr)
		{
			BoundExpression receiver = withExpr.Receiver;
			this.VisitRvalue(receiver, false);
			this.CheckPossibleNullReceiver(receiver, false);
			MethodSymbol cloneMethod = withExpr.CloneMethod;
			TypeWithAnnotations typeWithAnnotations = ((cloneMethod != null) ? cloneMethod.ReturnTypeWithAnnotations : this.ResultType.ToTypeWithAnnotations(this.compilation, false));
			TypeWithState typeWithState = NullableWalker.ApplyUnconditionalAnnotations(typeWithAnnotations.ToTypeWithState(), this.GetRValueAnnotations(withExpr.CloneMethod));
			int orCreatePlaceholderSlot = this.GetOrCreatePlaceholderSlot(withExpr);
			this.TrackNullableStateForAssignment(receiver, typeWithAnnotations, orCreatePlaceholderSlot, typeWithState, this.MakeSlot(receiver));
			this.SetResult(withExpr, typeWithState, typeWithAnnotations, true, default(bool?));
			this.VisitObjectCreationInitializer(null, orCreatePlaceholderSlot, withExpr.InitializerExpression, FlowAnalysisAnnotations.None);
			return null;
		}

		// Token: 0x06001C15 RID: 7189 RVA: 0x000936E7 File Offset: 0x000918E7
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitForStatement(BoundForStatement node)
		{
			this.DeclareLocals(node.OuterLocals);
			this.DeclareLocals(node.InnerLocals);
			return base.VisitForStatement(node);
		}

		// Token: 0x06001C16 RID: 7190 RVA: 0x00093708 File Offset: 0x00091908
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitForEachStatement(BoundForEachStatement node)
		{
			this.DeclareLocals(node.IterationVariables);
			return base.VisitForEachStatement(node);
		}

		// Token: 0x06001C17 RID: 7191 RVA: 0x0009371D File Offset: 0x0009191D
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitUsingStatement(BoundUsingStatement node)
		{
			this.DeclareLocals(node.Locals);
			this.Visit(node.AwaitOpt);
			return base.VisitUsingStatement(node);
		}

		// Token: 0x06001C18 RID: 7192 RVA: 0x0009373F File Offset: 0x0009193F
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitUsingLocalDeclarations(BoundUsingLocalDeclarations node)
		{
			this.Visit(node.AwaitOpt);
			return base.VisitUsingLocalDeclarations(node);
		}

		// Token: 0x06001C19 RID: 7193 RVA: 0x00093755 File Offset: 0x00091955
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitFixedStatement(BoundFixedStatement node)
		{
			this.DeclareLocals(node.Locals);
			return base.VisitFixedStatement(node);
		}

		// Token: 0x06001C1A RID: 7194 RVA: 0x0009376A File Offset: 0x0009196A
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitConstructorMethodBody(BoundConstructorMethodBody node)
		{
			this.DeclareLocals(node.Locals);
			return base.VisitConstructorMethodBody(node);
		}

		// Token: 0x06001C1B RID: 7195 RVA: 0x00093780 File Offset: 0x00091980
		private void DeclareLocal(LocalSymbol local)
		{
			if (local.DeclarationKind != LocalDeclarationKind.None)
			{
				int orCreateSlot = this.GetOrCreateSlot(local, 0, false, true);
				if (orCreateSlot > 0)
				{
					this.State[orCreateSlot] = this.GetDefaultState(ref this.State, orCreateSlot);
					this.InheritDefaultState(this.GetDeclaredLocalResult(local).Type, orCreateSlot);
				}
			}
		}

		// Token: 0x06001C1C RID: 7196 RVA: 0x000937D4 File Offset: 0x000919D4
		private void DeclareLocals([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalSymbol> locals)
		{
			foreach (LocalSymbol localSymbol in locals)
			{
				this.DeclareLocal(localSymbol);
			}
		}

		// Token: 0x06001C1D RID: 7197 RVA: 0x00093804 File Offset: 0x00091A04
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitLocalDeclaration(BoundLocalDeclaration node)
		{
			LocalSymbol localSymbol = node.LocalSymbol;
			int orCreateSlot = this.GetOrCreateSlot(localSymbol, 0, false, true);
			bool disableDiagnostics = this._disableDiagnostics;
			this._disableDiagnostics = true;
			NullableWalker.LocalState state = this.State;
			this.VisitAndUnsplitAll<BoundExpression>(node.ArgumentsOpt);
			this._disableDiagnostics = disableDiagnostics;
			base.SetState(state);
			if (node.DeclaredTypeOpt != null)
			{
				this.VisitTypeExpression(node.DeclaredTypeOpt);
			}
			BoundExpression initializerOpt = node.InitializerOpt;
			if (initializerOpt == null)
			{
				return null;
			}
			TypeWithAnnotations typeWithAnnotations = localSymbol.TypeWithAnnotations;
			bool inferredType = node.InferredType;
			TypeWithState typeWithState;
			if (localSymbol.IsRef)
			{
				typeWithState = this.VisitRefExpression(initializerOpt, typeWithAnnotations);
			}
			else
			{
				typeWithState = this.VisitOptionalImplicitConversion(initializerOpt, inferredType ? default(TypeWithAnnotations) : typeWithAnnotations, true, true, NullableWalker.AssignmentKind.Assignment);
			}
			if (inferredType)
			{
				if (typeWithState.HasNullType)
				{
					typeWithState = typeWithAnnotations.ToTypeWithState();
				}
				typeWithAnnotations = typeWithState.ToAnnotatedTypeWithAnnotations(this.compilation);
				this._variables.SetType(localSymbol, typeWithAnnotations);
				if (node.DeclaredTypeOpt != null)
				{
					this.SetAnalyzedNullability(node.DeclaredTypeOpt, new NullableWalker.VisitResult(typeWithAnnotations.ToTypeWithState(), typeWithAnnotations), new bool?(true));
				}
			}
			this.TrackNullableStateForAssignment(initializerOpt, typeWithAnnotations, orCreateSlot, typeWithState, this.MakeSlot(initializerOpt));
			return null;
		}

		// Token: 0x06001C1E RID: 7198 RVA: 0x0009392C File Offset: 0x00091B2C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		protected override BoundExpression VisitExpressionWithoutStackGuard(BoundExpression node)
		{
			this.SetInvalidResult();
			base.VisitExpressionWithoutStackGuard(node);
			TypeWithState resultType = this.ResultType;
			if (this.ShouldMakeNotNullRvalue(node))
			{
				TypeWithState typeWithState = resultType.WithNotNullState();
				this.SetResult(node, typeWithState, this.LvalueResultType, true, default(bool?));
			}
			return null;
		}

		// Token: 0x06001C1F RID: 7199 RVA: 0x00093978 File Offset: 0x00091B78
		private static bool AreLambdaAndNewDelegateSimilar(LambdaSymbol l, NamedTypeSymbol n)
		{
			MethodSymbol delegateInvokeMethod = n.DelegateInvokeMethod;
			return delegateInvokeMethod.Parameters.SequenceEqual<ParameterSymbol, ParameterSymbol>(l.Parameters, (ParameterSymbol p1, ParameterSymbol p2) => p1.Type.Equals(p2.Type, TypeCompareKind.IgnoreTupleNames | TypeCompareKind.IgnoreNullableModifiersForReferenceTypes | TypeCompareKind.ObliviousNullableModifierMatchesAny)) && delegateInvokeMethod.ReturnType.Equals(l.ReturnType, TypeCompareKind.IgnoreTupleNames | TypeCompareKind.IgnoreNullableModifiersForReferenceTypes | TypeCompareKind.ObliviousNullableModifierMatchesAny);
		}

		// Token: 0x06001C20 RID: 7200 RVA: 0x000939D3 File Offset: 0x00091BD3
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override BoundNode Visit(BoundNode node)
		{
			return this.Visit(node, true);
		}

		// Token: 0x06001C21 RID: 7201 RVA: 0x000939DD File Offset: 0x00091BDD
		private BoundNode VisitLValue(BoundNode node)
		{
			return this.Visit(node, false);
		}

		// Token: 0x06001C22 RID: 7202 RVA: 0x000939E8 File Offset: 0x00091BE8
		private BoundNode Visit([global::System.Runtime.CompilerServices.Nullable(2)] BoundNode node, bool expressionIsRead)
		{
			bool expressionIsRead2 = this._expressionIsRead;
			this._expressionIsRead = expressionIsRead;
			this.TakeIncrementalSnapshot(node);
			BoundNode boundNode = base.Visit(node);
			this._expressionIsRead = expressionIsRead2;
			return boundNode;
		}

		// Token: 0x06001C23 RID: 7203 RVA: 0x00093A18 File Offset: 0x00091C18
		protected override void VisitStatement(BoundStatement statement)
		{
			this.SetInvalidResult();
			base.VisitStatement(statement);
			this.SetInvalidResult();
		}

		// Token: 0x06001C24 RID: 7204 RVA: 0x00093A30 File Offset: 0x00091C30
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitObjectCreationExpression(BoundObjectCreationExpression node)
		{
			ImmutableArray<BoundExpression> arguments = node.Arguments;
			ImmutableArray<NullableWalker.VisitArgumentResult> item = this.VisitArguments(node, arguments, node.ArgumentRefKindsOpt, node.Constructor, node.ArgsToParamsOpt, node.DefaultArguments, node.Expanded, false).Item2;
			this.VisitObjectOrDynamicObjectCreation(node, arguments, item, node.InitializerExpressionOpt);
			return null;
		}

		// Token: 0x06001C25 RID: 7205 RVA: 0x00093A81 File Offset: 0x00091C81
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitUnconvertedObjectCreationExpression(BoundUnconvertedObjectCreationExpression node)
		{
			this.SetResultType(node, TypeWithState.Create(null, NullableFlowState.NotNull), true);
			return null;
		}

		// Token: 0x06001C26 RID: 7206 RVA: 0x00093A94 File Offset: 0x00091C94
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private void VisitObjectOrDynamicObjectCreation([global::System.Runtime.CompilerServices.Nullable(1)] BoundExpression node, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments, ImmutableArray<NullableWalker.VisitArgumentResult> argumentResults, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression initializerOpt)
		{
			ImmutableArray<TypeWithState> immutableArray = argumentResults.SelectAsArray<NullableWalker.VisitArgumentResult, TypeWithState>((NullableWalker.VisitArgumentResult ar) => ar.RValueType);
			int num = -1;
			TypeSymbol type = node.Type;
			NullableFlowState nullableFlowState = NullableFlowState.NotNull;
			if (type != null)
			{
				num = this.GetOrCreatePlaceholderSlot(node);
				if (num > 0)
				{
					BoundObjectCreationExpression boundObjectCreationExpression = node as BoundObjectCreationExpression;
					MethodSymbol methodSymbol = ((boundObjectCreationExpression != null) ? boundObjectCreationExpression.Constructor : null);
					bool flag = methodSymbol != null && methodSymbol.IsDefaultValueTypeConstructor();
					if (EmptyStructTypeCache.IsTrackableStructType(type))
					{
						NamedTypeSymbol namedTypeSymbol = ((methodSymbol != null) ? methodSymbol.ContainingType : null);
						if (namedTypeSymbol != null && namedTypeSymbol.IsTupleType && !flag)
						{
							this.TrackNullableStateOfTupleElements(num, namedTypeSymbol, arguments, immutableArray, boundObjectCreationExpression.ArgsToParamsOpt, true);
						}
						else
						{
							this.InheritNullableStateOfTrackableStruct(type, num, -1, flag, -1);
						}
					}
					else if (type.IsNullableType())
					{
						TypeWithAnnotations typeWithAnnotations;
						if (flag)
						{
							nullableFlowState = NullableFlowState.MaybeNull;
						}
						else if (methodSymbol != null && methodSymbol.ParameterCount == 1 && NullableWalker.AreNullableAndUnderlyingTypes(type, methodSymbol.ParameterTypesWithAnnotations[0].Type, out typeWithAnnotations))
						{
							BoundExpression boundExpression = arguments[0];
							int num2 = this.MakeSlot(boundExpression);
							if (num2 > 0)
							{
								this.TrackNullableStateOfNullableValue(num, type, boundExpression, typeWithAnnotations.ToTypeWithState(), num2);
							}
						}
					}
					this.State[num] = nullableFlowState;
				}
			}
			if (initializerOpt != null)
			{
				this.VisitObjectCreationInitializer(null, num, initializerOpt, FlowAnalysisAnnotations.None);
			}
			this.SetResultType(node, TypeWithState.Create(type, nullableFlowState), true);
		}

		// Token: 0x06001C27 RID: 7207 RVA: 0x00093BF4 File Offset: 0x00091DF4
		private void VisitObjectCreationInitializer([global::System.Runtime.CompilerServices.Nullable(2)] Symbol containingSymbol, int containingSlot, BoundExpression node, FlowAnalysisAnnotations leftAnnotations)
		{
			NullableWalker.<>c__DisplayClass146_0 CS$<>8__locals1;
			CS$<>8__locals1.containingSlot = containingSlot;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.containingSymbol = containingSymbol;
			this.TakeIncrementalSnapshot(node);
			BoundObjectInitializerExpression boundObjectInitializerExpression = node as BoundObjectInitializerExpression;
			if (boundObjectInitializerExpression != null)
			{
				this.<VisitObjectCreationInitializer>g__checkImplicitReceiver|146_0(boundObjectInitializerExpression, ref CS$<>8__locals1);
				foreach (BoundExpression boundExpression in boundObjectInitializerExpression.Initializers)
				{
					if (boundExpression.Kind == BoundKind.AssignmentOperator)
					{
						this.VisitObjectElementInitializer(CS$<>8__locals1.containingSlot, (BoundAssignmentOperator)boundExpression);
					}
					else
					{
						this.VisitRvalue(boundExpression, false);
					}
				}
				this.SetNotNullResult(boundObjectInitializerExpression.Placeholder);
				return;
			}
			BoundCollectionInitializerExpression boundCollectionInitializerExpression = node as BoundCollectionInitializerExpression;
			if (boundCollectionInitializerExpression == null)
			{
				if (CS$<>8__locals1.containingSymbol != null)
				{
					TypeWithAnnotations typeWithAnnotations = NullableWalker.ApplyLValueAnnotations(CS$<>8__locals1.containingSymbol.GetTypeOrReturnType(), leftAnnotations);
					TypeWithState typeWithState = this.VisitOptionalImplicitConversion(node, typeWithAnnotations, false, true, NullableWalker.AssignmentKind.Assignment);
					this.TrackNullableStateForAssignment(node, typeWithAnnotations, CS$<>8__locals1.containingSlot, typeWithState, this.MakeSlot(node));
				}
				return;
			}
			this.<VisitObjectCreationInitializer>g__checkImplicitReceiver|146_0(boundCollectionInitializerExpression, ref CS$<>8__locals1);
			foreach (BoundExpression boundExpression2 in boundCollectionInitializerExpression.Initializers)
			{
				if (boundExpression2.Kind == BoundKind.CollectionElementInitializer)
				{
					this.VisitCollectionElementInitializer((BoundCollectionElementInitializer)boundExpression2);
				}
				else
				{
					this.VisitRvalue(boundExpression2, false);
				}
			}
			this.SetNotNullResult(boundCollectionInitializerExpression.Placeholder);
		}

		// Token: 0x06001C28 RID: 7208 RVA: 0x00093D40 File Offset: 0x00091F40
		private void VisitObjectElementInitializer(int containingSlot, BoundAssignmentOperator node)
		{
			this.TakeIncrementalSnapshot(node);
			BoundExpression left = node.Left;
			if (left.Kind == BoundKind.ObjectInitializerMember)
			{
				BoundObjectInitializerMember boundObjectInitializerMember = (BoundObjectInitializerMember)left;
				this.TakeIncrementalSnapshot(left);
				Symbol memberSymbol = boundObjectInitializerMember.MemberSymbol;
				if (!boundObjectInitializerMember.Arguments.IsDefaultOrEmpty)
				{
					this.VisitArguments(boundObjectInitializerMember, boundObjectInitializerMember.Arguments, boundObjectInitializerMember.ArgumentRefKindsOpt, (PropertySymbol)memberSymbol, boundObjectInitializerMember.ArgsToParamsOpt, boundObjectInitializerMember.DefaultArguments, boundObjectInitializerMember.Expanded);
				}
				if (memberSymbol != null)
				{
					int num = ((containingSlot < 0 || !this.IsSlotMember(containingSlot, memberSymbol)) ? (-1) : this.GetOrCreateSlot(memberSymbol, containingSlot, false, true));
					this.VisitObjectCreationInitializer(memberSymbol, num, node.Right, this.GetLValueAnnotations(node.Left));
				}
				NullableWalker.VisitResult visitResult = new NullableWalker.VisitResult(boundObjectInitializerMember.Type, NullableAnnotation.NotAnnotated, NullableFlowState.NotNull);
				this.SetAnalyzedNullability(boundObjectInitializerMember, visitResult, default(bool?));
				this.SetAnalyzedNullability(node, visitResult, default(bool?));
				return;
			}
			this.Visit(node);
		}

		// Token: 0x06001C29 RID: 7209 RVA: 0x00093E34 File Offset: 0x00092034
		private new void VisitCollectionElementInitializer(BoundCollectionElementInitializer node)
		{
			MethodSymbol item = this.VisitArguments(node, node.Arguments, default(ImmutableArray<RefKind>), node.AddMethod, node.ArgsToParamsOpt, node.DefaultArguments, node.Expanded, node.InvokedAsExtensionMethod).Item1;
			if (node.ImplicitReceiverOpt != null)
			{
				this.SetAnalyzedNullability(node.ImplicitReceiverOpt, new NullableWalker.VisitResult(node.ImplicitReceiverOpt.Type, NullableAnnotation.NotAnnotated, NullableFlowState.NotNull), default(bool?));
			}
			this.SetUnknownResultNullability(node);
			this.SetUpdatedSymbol(node, node.AddMethod, item);
		}

		// Token: 0x06001C2A RID: 7210 RVA: 0x00093EBF File Offset: 0x000920BF
		private void SetNotNullResult(BoundExpression node)
		{
			this.SetResultType(node, TypeWithState.Create(node.Type, NullableFlowState.NotNull), true);
		}

		// Token: 0x06001C2B RID: 7211 RVA: 0x00093ED8 File Offset: 0x000920D8
		protected override bool IsEmptyStructType(TypeSymbol type)
		{
			if (type.TypeKind != TypeKind.Struct)
			{
				return false;
			}
			if (!this._emptyStructTypeCache.IsEmptyStructType(type))
			{
				return false;
			}
			if (type.SpecialType != SpecialType.None)
			{
				return true;
			}
			ImmutableArray<Symbol> membersUnordered = ((NamedTypeSymbol)type).GetMembersUnordered();
			if (membersUnordered.Any<Symbol>((Symbol m) => m.Kind == SymbolKind.Field))
			{
				return true;
			}
			return !membersUnordered.Any<Symbol>((Symbol m) => m.Kind == SymbolKind.Property);
		}

		// Token: 0x06001C2C RID: 7212 RVA: 0x00093F6C File Offset: 0x0009216C
		private int GetOrCreatePlaceholderSlot(BoundExpression node)
		{
			if (this.IsEmptyStructType(node.Type))
			{
				return -1;
			}
			return this.GetOrCreatePlaceholderSlot(node, TypeWithAnnotations.Create(node.Type, NullableAnnotation.NotAnnotated, default(ImmutableArray<CustomModifier>)));
		}

		// Token: 0x06001C2D RID: 7213 RVA: 0x00093FA8 File Offset: 0x000921A8
		private int GetOrCreatePlaceholderSlot(object identifier, TypeWithAnnotations type)
		{
			if (this._placeholderLocalsOpt == null)
			{
				this._placeholderLocalsOpt = PooledDictionary<object, NullableWalker.PlaceholderLocal>.GetInstance();
			}
			NullableWalker.PlaceholderLocal placeholderLocal;
			if (!this._placeholderLocalsOpt.TryGetValue(identifier, ref placeholderLocal))
			{
				placeholderLocal = new NullableWalker.PlaceholderLocal(this.CurrentSymbol, identifier, type);
				this._placeholderLocalsOpt.Add(identifier, placeholderLocal);
			}
			return this.GetOrCreateSlot(placeholderLocal, 0, true, true);
		}

		// Token: 0x06001C2E RID: 7214 RVA: 0x00094000 File Offset: 0x00092200
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitAnonymousObjectCreationExpression(BoundAnonymousObjectCreationExpression node)
		{
			NamedTypeSymbol namedTypeSymbol = (NamedTypeSymbol)node.Type;
			ImmutableArray<BoundExpression> arguments = node.Arguments;
			ImmutableArray<TypeWithState> immutableArray = arguments.SelectAsArray((BoundExpression arg, NullableWalker self) => self.VisitRvalueWithState(arg), this);
			ImmutableArray<TypeWithAnnotations> immutableArray2 = immutableArray.SelectAsArray<TypeWithState, TypeWithAnnotations>((TypeWithState arg) => arg.ToTypeWithAnnotations(this.compilation, false));
			if (immutableArray2.All<TypeWithAnnotations>((TypeWithAnnotations argType) => argType.HasType))
			{
				namedTypeSymbol = AnonymousTypeManager.ConstructAnonymousTypeSymbol(namedTypeSymbol, immutableArray2);
				int orCreatePlaceholderSlot = this.GetOrCreatePlaceholderSlot(node);
				int num = 0;
				for (int i = 0; i < arguments.Length; i++)
				{
					BoundExpression boundExpression = arguments[i];
					TypeWithState typeWithState = immutableArray[i];
					PropertySymbol anonymousTypeProperty = AnonymousTypeManager.GetAnonymousTypeProperty(namedTypeSymbol, i);
					if (anonymousTypeProperty.Type.SpecialType != SpecialType.System_Void)
					{
						int orCreateSlot = this.GetOrCreateSlot(anonymousTypeProperty, orCreatePlaceholderSlot, false, true);
						this.TrackNullableStateForAssignment(boundExpression, anonymousTypeProperty.TypeWithAnnotations, orCreateSlot, typeWithState, this.MakeSlot(boundExpression));
						BoundAnonymousPropertyDeclaration boundAnonymousPropertyDeclaration = NullableWalker.<VisitAnonymousObjectCreationExpression>g__getDeclaration|153_3(node, anonymousTypeProperty, ref num);
						if (boundAnonymousPropertyDeclaration != null)
						{
							this.TakeIncrementalSnapshot(boundAnonymousPropertyDeclaration);
							this.SetAnalyzedNullability(boundAnonymousPropertyDeclaration, new NullableWalker.VisitResult(typeWithState, anonymousTypeProperty.TypeWithAnnotations), default(bool?));
						}
					}
				}
			}
			this.SetResultType(node, TypeWithState.Create(namedTypeSymbol, NullableFlowState.NotNull), true);
			return null;
		}

		// Token: 0x06001C2F RID: 7215 RVA: 0x00094154 File Offset: 0x00092354
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitArrayCreation(BoundArrayCreation node)
		{
			foreach (BoundExpression boundExpression in node.Bounds)
			{
				this.VisitRvalue(boundExpression, false);
			}
			BoundArrayInitialization initializerOpt = node.InitializerOpt;
			if (initializerOpt == null)
			{
				this.SetResultType(node, TypeWithState.Create(node.Type, NullableFlowState.NotNull), true);
				return null;
			}
			this.TakeIncrementalSnapshot(initializerOpt);
			ArrayBuilder<BoundExpression> instance = ArrayBuilder<BoundExpression>.GetInstance(initializerOpt.Initializers.Length);
			NullableWalker.GetArrayElements(initializerOpt, instance);
			int count = instance.Count;
			bool flag = node.Syntax.Kind() == SyntaxKind.ImplicitArrayCreationExpression;
			ArrayTypeSymbol arrayTypeSymbol = (ArrayTypeSymbol)node.Type;
			TypeWithAnnotations elementTypeWithAnnotations = arrayTypeSymbol.ElementTypeWithAnnotations;
			if (!flag)
			{
				foreach (BoundExpression boundExpression2 in instance)
				{
					this.VisitOptionalImplicitConversion(boundExpression2, elementTypeWithAnnotations, false, false, NullableWalker.AssignmentKind.Assignment);
				}
			}
			else
			{
				ArrayBuilder<BoundExpression> instance2 = ArrayBuilder<BoundExpression>.GetInstance(count);
				ArrayBuilder<Conversion> instance3 = ArrayBuilder<Conversion>.GetInstance(count);
				ArrayBuilder<TypeWithState> instance4 = ArrayBuilder<TypeWithState>.GetInstance(count);
				ArrayBuilder<BoundExpression> instance5 = ArrayBuilder<BoundExpression>.GetInstance(count);
				foreach (BoundExpression boundExpression3 in instance)
				{
					ValueTuple<BoundExpression, Conversion> valueTuple = NullableWalker.RemoveConversion(boundExpression3, false);
					BoundExpression item = valueTuple.Item1;
					Conversion item2 = valueTuple.Item2;
					instance2.Add(item);
					instance3.Add(item2);
					this.SnapshotWalkerThroughConversionGroup(boundExpression3, item);
					TypeWithState typeWithState = this.VisitRvalueWithState(item);
					instance4.Add(typeWithState);
					instance5.Add(NullableWalker.CreatePlaceholderIfNecessary(item, typeWithState.ToTypeWithAnnotations(this.compilation, false)));
				}
				ImmutableArray<BoundExpression> immutableArray = instance5.ToImmutableAndFree();
				TypeSymbol typeSymbol = null;
				if (!node.HasErrors)
				{
					CompoundUseSiteInfo<AssemblySymbol> discarded = CompoundUseSiteInfo<AssemblySymbol>.Discarded;
					typeSymbol = BestTypeInferrer.InferBestType(immutableArray, this._conversions, ref discarded);
				}
				TypeWithAnnotations typeWithAnnotations = ((typeSymbol == null) ? elementTypeWithAnnotations.SetUnknownNullabilityForReferenceTypes() : TypeWithAnnotations.Create(typeSymbol, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)));
				if (typeSymbol != null)
				{
					for (int i = 0; i < count; i++)
					{
						BoundExpression boundExpression4 = instance2[i];
						BoundConversion conversionIfApplicable = NullableWalker.GetConversionIfApplicable(instance[i], boundExpression4);
						instance4[i] = this.VisitConversion(conversionIfApplicable, boundExpression4, instance3[i], typeWithAnnotations, instance4[i], true, false, false, NullableWalker.AssignmentKind.Assignment, null, false, true, false, default(Optional<NullableWalker.LocalState>), false, null);
					}
					NullableFlowState nullableState = BestTypeInferrer.GetNullableState(instance4);
					typeWithAnnotations = TypeWithState.Create(typeWithAnnotations.Type, nullableState).ToTypeWithAnnotations(this.compilation, false);
					for (int j = 0; j < count; j++)
					{
						this.VisitConversion(null, instance2[j], Conversion.Identity, typeWithAnnotations, instance4[j], true, false, false, NullableWalker.AssignmentKind.Assignment, null, true, false, false, default(Optional<NullableWalker.LocalState>), false, null);
					}
				}
				else
				{
					for (int k = 0; k < count; k++)
					{
						this.TrackAnalyzedNullabilityThroughConversionGroup(typeWithAnnotations.ToTypeWithState(), instance[k] as BoundConversion, instance2[k]);
					}
				}
				instance2.Free();
				instance3.Free();
				instance4.Free();
				arrayTypeSymbol = arrayTypeSymbol.WithElementType(typeWithAnnotations);
			}
			instance.Free();
			this.SetResultType(node, TypeWithState.Create(arrayTypeSymbol, NullableFlowState.NotNull), true);
			return null;
		}

		// Token: 0x06001C30 RID: 7216 RVA: 0x00094460 File Offset: 0x00092660
		internal static TypeWithAnnotations BestTypeForLambdaReturns([global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 0, 1 })] ArrayBuilder<ValueTuple<BoundExpression, TypeWithAnnotations>> returns, Binder binder, BoundNode node, Conversions conversions)
		{
			NullableWalker nullableWalker = new NullableWalker(binder.Compilation, null, false, false, null, node, binder, conversions, null, null, null, null, false);
			int count = returns.Count;
			ArrayBuilder<TypeWithAnnotations> instance = ArrayBuilder<TypeWithAnnotations>.GetInstance(count);
			ArrayBuilder<BoundExpression> instance2 = ArrayBuilder<BoundExpression>.GetInstance(count);
			for (int i = 0; i < count; i++)
			{
				ValueTuple<BoundExpression, TypeWithAnnotations> valueTuple = returns[i];
				BoundExpression item = valueTuple.Item1;
				TypeWithAnnotations item2 = valueTuple.Item2;
				instance.Add(item2);
				instance2.Add(NullableWalker.CreatePlaceholderIfNecessary(item, item2));
			}
			CompoundUseSiteInfo<AssemblySymbol> discarded = CompoundUseSiteInfo<AssemblySymbol>.Discarded;
			ImmutableArray<BoundExpression> immutableArray = instance2.ToImmutableAndFree();
			TypeSymbol typeSymbol = BestTypeInferrer.InferBestType(immutableArray, nullableWalker._conversions, ref discarded);
			TypeWithAnnotations typeWithAnnotations2;
			if (typeSymbol != null)
			{
				TypeWithAnnotations typeWithAnnotations = TypeWithAnnotations.Create(typeSymbol, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
				ConversionsBase conversionsBase = nullableWalker._conversions.WithNullability(false);
				for (int j = 0; j < count; j++)
				{
					BoundExpression boundExpression = immutableArray[j];
					Conversion conversion = conversionsBase.ClassifyConversionFromExpression(boundExpression, typeSymbol, ref discarded, false);
					instance[j] = nullableWalker.VisitConversion(null, boundExpression, conversion, typeWithAnnotations, instance[j].ToTypeWithState(), false, false, false, NullableWalker.AssignmentKind.Return, null, false, false, false, default(Optional<NullableWalker.LocalState>), false, null).ToTypeWithAnnotations(binder.Compilation, false);
				}
				typeWithAnnotations2 = TypeWithAnnotations.Create(typeSymbol, BestTypeInferrer.GetNullableAnnotation(instance), default(ImmutableArray<CustomModifier>));
			}
			else
			{
				typeWithAnnotations2 = default(TypeWithAnnotations);
			}
			instance.Free();
			nullableWalker.Free();
			return typeWithAnnotations2;
		}

		// Token: 0x06001C31 RID: 7217 RVA: 0x000945C8 File Offset: 0x000927C8
		private static void GetArrayElements(BoundArrayInitialization node, ArrayBuilder<BoundExpression> builder)
		{
			foreach (BoundExpression boundExpression in node.Initializers)
			{
				if (boundExpression.Kind == BoundKind.ArrayInitialization)
				{
					NullableWalker.GetArrayElements((BoundArrayInitialization)boundExpression, builder);
				}
				else
				{
					builder.Add(boundExpression);
				}
			}
		}

		// Token: 0x06001C32 RID: 7218 RVA: 0x0009461C File Offset: 0x0009281C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitArrayAccess(BoundArrayAccess node)
		{
			this.Visit(node.Expression);
			this.CheckPossibleNullReceiver(node.Expression, false);
			ArrayTypeSymbol arrayTypeSymbol = this.ResultType.Type as ArrayTypeSymbol;
			foreach (BoundExpression boundExpression in node.Indices)
			{
				this.VisitRvalue(boundExpression, false);
			}
			TypeWithAnnotations typeWithAnnotations;
			if (node.Indices.Length == 1 && TypeSymbol.Equals(node.Indices[0].Type, this.compilation.GetWellKnownType(WellKnownType.System_Range), TypeCompareKind.ConsiderEverything))
			{
				typeWithAnnotations = TypeWithAnnotations.Create(arrayTypeSymbol, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
			}
			else
			{
				typeWithAnnotations = ((arrayTypeSymbol != null) ? arrayTypeSymbol.ElementTypeWithAnnotations : default(TypeWithAnnotations));
			}
			this.SetLvalueResultType(node, typeWithAnnotations);
			return null;
		}

		// Token: 0x06001C33 RID: 7219 RVA: 0x000946F4 File Offset: 0x000928F4
		private TypeWithState InferResultNullability(BinaryOperatorKind operatorKind, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol methodOpt, TypeSymbol resultType, TypeWithState leftType, TypeWithState rightType)
		{
			NullableFlowState nullableFlowState = NullableFlowState.NotNull;
			if (operatorKind.IsUserDefined())
			{
				if (methodOpt != null && methodOpt.ParameterCount == 2)
				{
					if (operatorKind.IsLifted() && !operatorKind.IsComparison())
					{
						return this.GetLiftedReturnType(methodOpt.ReturnTypeWithAnnotations, leftType.State.Join(rightType.State));
					}
					TypeWithState typeWithState = this.GetReturnTypeWithState(methodOpt);
					if ((leftType.IsNotNull && methodOpt.ReturnNotNullIfParameterNotNull.Contains(methodOpt.Parameters[0].Name)) || (rightType.IsNotNull && methodOpt.ReturnNotNullIfParameterNotNull.Contains(methodOpt.Parameters[1].Name)))
					{
						typeWithState = typeWithState.WithNotNullState();
					}
					return typeWithState;
				}
			}
			else if (!operatorKind.IsDynamic() && !resultType.IsValueType)
			{
				BinaryOperatorKind binaryOperatorKind = operatorKind.Operator() | operatorKind.OperandTypes();
				if (binaryOperatorKind != BinaryOperatorKind.DelegateCombination)
				{
					if (binaryOperatorKind != BinaryOperatorKind.DelegateRemoval)
					{
						nullableFlowState = NullableFlowState.NotNull;
					}
					else
					{
						nullableFlowState = NullableFlowState.MaybeNull;
					}
				}
				else
				{
					nullableFlowState = leftType.State.Meet(rightType.State);
				}
			}
			if (operatorKind.IsLifted() && !operatorKind.IsComparison())
			{
				nullableFlowState = leftType.State.Join(rightType.State);
			}
			return TypeWithState.Create(resultType, nullableFlowState);
		}

		// Token: 0x06001C34 RID: 7220 RVA: 0x00094830 File Offset: 0x00092A30
		protected override void VisitBinaryOperatorChildren(ArrayBuilder<BoundBinaryOperator> stack)
		{
			NullableWalker.<>c__DisplayClass159_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.binary = stack.Pop<BoundBinaryOperator>();
			ValueTuple<BoundExpression, Conversion> valueTuple = NullableWalker.RemoveConversion(CS$<>8__locals1.binary.Left, false);
			CS$<>8__locals1.leftOperand = valueTuple.Item1;
			CS$<>8__locals1.leftConversion = valueTuple.Item2;
			this.Visit(CS$<>8__locals1.leftOperand);
			for (;;)
			{
				if (!this.<VisitBinaryOperatorChildren>g__learnFromBooleanConstantTest|159_0(ref CS$<>8__locals1))
				{
					base.Unsplit();
					this.UseRvalueOnly(CS$<>8__locals1.leftOperand);
					this.AfterLeftChildHasBeenVisited(CS$<>8__locals1.leftOperand, CS$<>8__locals1.leftConversion, CS$<>8__locals1.binary);
				}
				if (stack.Count == 0)
				{
					break;
				}
				CS$<>8__locals1.leftOperand = CS$<>8__locals1.binary;
				CS$<>8__locals1.leftConversion = Conversion.Identity;
				CS$<>8__locals1.binary = stack.Pop<BoundBinaryOperator>();
			}
		}

		// Token: 0x06001C35 RID: 7221 RVA: 0x000948F0 File Offset: 0x00092AF0
		private void AfterLeftChildHasBeenVisited(BoundExpression leftOperand, Conversion leftConversion, BoundBinaryOperator binary)
		{
			TypeWithState resultType = this.ResultType;
			ValueTuple<BoundExpression, Conversion> valueTuple = NullableWalker.RemoveConversion(binary.Right, false);
			BoundExpression item = valueTuple.Item1;
			Conversion item2 = valueTuple.Item2;
			TypeWithState typeWithState = this.VisitRvalueWithState(item);
			MethodSymbol methodSymbol = binary.MethodOpt;
			if (binary.OperatorKind.IsUserDefined() && methodSymbol != null && methodSymbol.ParameterCount == 2)
			{
				TypeSymbol containingType = methodSymbol.ContainingType;
				NullableWalker.<>c__DisplayClass160_0 CS$<>8__locals1;
				CS$<>8__locals1.isLifted = binary.OperatorKind.IsLifted();
				TypeWithState nullableUnderlyingTypeIfNecessary = NullableWalker.GetNullableUnderlyingTypeIfNecessary(CS$<>8__locals1.isLifted, resultType);
				TypeWithState nullableUnderlyingTypeIfNecessary2 = NullableWalker.GetNullableUnderlyingTypeIfNecessary(CS$<>8__locals1.isLifted, typeWithState);
				TypeSymbol typeSymbol = this.<AfterLeftChildHasBeenVisited>g__getTypeIfContainingType|160_2(containingType, nullableUnderlyingTypeIfNecessary.Type) ?? this.<AfterLeftChildHasBeenVisited>g__getTypeIfContainingType|160_2(containingType, nullableUnderlyingTypeIfNecessary2.Type);
				if (typeSymbol != null)
				{
					methodSymbol = (MethodSymbol)NullableWalker.AsMemberOfType(typeSymbol, methodSymbol);
				}
				ImmutableArray<ParameterSymbol> parameters = methodSymbol.Parameters;
				this.<AfterLeftChildHasBeenVisited>g__visitOperandConversion|160_3(binary.Left, leftOperand, leftConversion, parameters[0], nullableUnderlyingTypeIfNecessary, ref CS$<>8__locals1);
				this.<AfterLeftChildHasBeenVisited>g__visitOperandConversion|160_3(binary.Right, item, item2, parameters[1], nullableUnderlyingTypeIfNecessary2, ref CS$<>8__locals1);
				this.SetUpdatedSymbol(binary, binary.MethodOpt, methodSymbol);
			}
			else
			{
				this.<AfterLeftChildHasBeenVisited>g__visitOperandConversion|160_0(binary.Left, leftOperand, leftConversion, resultType);
				this.<AfterLeftChildHasBeenVisited>g__visitOperandConversion|160_0(binary.Right, item, item2, typeWithState);
			}
			TypeWithState typeWithState2 = this.InferResultNullability(binary.OperatorKind, methodSymbol, binary.Type, resultType, typeWithState);
			this.SetResult(binary, typeWithState2, typeWithState2.ToTypeWithAnnotations(this.compilation, false), true, default(bool?));
			BinaryOperatorKind binaryOperatorKind = binary.OperatorKind.Operator();
			if (binaryOperatorKind == BinaryOperatorKind.Equal || binaryOperatorKind == BinaryOperatorKind.NotEqual)
			{
				BoundExpression boundExpression = null;
				ConstantValue constantValue = binary.Right.ConstantValue;
				if (constantValue != null && constantValue.IsNull)
				{
					boundExpression = binary.Left;
				}
				else
				{
					ConstantValue constantValue2 = binary.Left.ConstantValue;
					if (constantValue2 != null && constantValue2.IsNull)
					{
						boundExpression = binary.Right;
					}
				}
				if (boundExpression != null)
				{
					bool flag = binaryOperatorKind != BinaryOperatorKind.Equal;
					this.<AfterLeftChildHasBeenVisited>g__splitAndLearnFromNonNullTest|160_1(boundExpression, flag);
					this.LearnFromNullTest(boundExpression, ref flag ? ref this.StateWhenFalse : ref this.StateWhenTrue);
					return;
				}
			}
			BoundExpression boundExpression2 = null;
			if (resultType.IsNotNull && typeWithState.MayBeNull)
			{
				boundExpression2 = binary.Right;
			}
			else if (typeWithState.IsNotNull && resultType.MayBeNull)
			{
				boundExpression2 = binary.Left;
			}
			if (boundExpression2 != null)
			{
				if (binaryOperatorKind <= BinaryOperatorKind.GreaterThan)
				{
					if (binaryOperatorKind != BinaryOperatorKind.Equal)
					{
						if (binaryOperatorKind == BinaryOperatorKind.NotEqual)
						{
							boundExpression2 = NullableWalker.SkipReferenceConversions(boundExpression2);
							this.<AfterLeftChildHasBeenVisited>g__splitAndLearnFromNonNullTest|160_1(boundExpression2, false);
							return;
						}
						if (binaryOperatorKind != BinaryOperatorKind.GreaterThan)
						{
							return;
						}
					}
				}
				else if (binaryOperatorKind != BinaryOperatorKind.LessThan && binaryOperatorKind != BinaryOperatorKind.GreaterThanOrEqual && binaryOperatorKind != BinaryOperatorKind.LessThanOrEqual)
				{
					return;
				}
				boundExpression2 = NullableWalker.SkipReferenceConversions(boundExpression2);
				this.<AfterLeftChildHasBeenVisited>g__splitAndLearnFromNonNullTest|160_1(boundExpression2, true);
				return;
			}
		}

		// Token: 0x06001C36 RID: 7222 RVA: 0x00094BA8 File Offset: 0x00092DA8
		private void GetSlotsToMarkAsNotNullable(BoundExpression operand, ArrayBuilder<int> slotBuilder)
		{
			int lastConditionalAccessSlot = this._lastConditionalAccessSlot;
			try
			{
				int num;
				for (;;)
				{
					BoundKind kind = operand.Kind;
					if (kind != BoundKind.AsOperator)
					{
						if (kind != BoundKind.Conversion)
						{
							if (kind != BoundKind.ConditionalAccess)
							{
								break;
							}
							BoundConditionalAccess boundConditionalAccess = (BoundConditionalAccess)operand;
							this.GetSlotsToMarkAsNotNullable(boundConditionalAccess.Receiver, slotBuilder);
							num = this.MakeSlot(boundConditionalAccess.Receiver);
							if (num > 0)
							{
								TypeSymbol type = boundConditionalAccess.Receiver.Type;
								if (type.IsNullableType())
								{
									Symbol symbol;
									num = this.GetNullableOfTValueSlot(type, num, out symbol, false);
								}
							}
							if (num <= 0)
							{
								goto IL_00C0;
							}
							this._lastConditionalAccessSlot = num;
							operand = boundConditionalAccess.AccessExpression;
						}
						else
						{
							operand = ((BoundConversion)operand).Operand;
						}
					}
					else
					{
						operand = ((BoundAsOperator)operand).Operand;
					}
				}
				num = this.MakeSlot(operand);
				if (num > 0 && NullableWalker.PossiblyNullableType(operand.Type))
				{
					slotBuilder.Add(num);
				}
				IL_00C0:;
			}
			finally
			{
				this._lastConditionalAccessSlot = lastConditionalAccessSlot;
			}
		}

		// Token: 0x06001C37 RID: 7223 RVA: 0x00094C90 File Offset: 0x00092E90
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private static bool PossiblyNullableType([NotNullWhen(true)] TypeSymbol operandType)
		{
			return operandType != null && operandType.CanContainNull();
		}

		// Token: 0x06001C38 RID: 7224 RVA: 0x00094CA0 File Offset: 0x00092EA0
		private static void MarkSlotsAsNotNull(ArrayBuilder<int> slots, ref NullableWalker.LocalState stateToUpdate)
		{
			foreach (int num in slots)
			{
				stateToUpdate[num] = NullableFlowState.NotNull;
			}
		}

		// Token: 0x06001C39 RID: 7225 RVA: 0x00094CD0 File Offset: 0x00092ED0
		private void LearnFromNonNullTest(BoundExpression expression, ref NullableWalker.LocalState state)
		{
			if (expression.Kind == BoundKind.AwaitableValuePlaceholder)
			{
				ValueTuple<BoundExpression, NullableWalker.VisitResult> valueTuple;
				if (this._awaitablePlaceholdersOpt == null || !this._awaitablePlaceholdersOpt.TryGetValue((BoundAwaitableValuePlaceholder)expression, ref valueTuple))
				{
					return;
				}
				expression = valueTuple.Item1;
			}
			ArrayBuilder<int> instance = ArrayBuilder<int>.GetInstance();
			this.GetSlotsToMarkAsNotNullable(expression, instance);
			NullableWalker.MarkSlotsAsNotNull(instance, ref state);
			instance.Free();
		}

		// Token: 0x06001C3A RID: 7226 RVA: 0x00094D29 File Offset: 0x00092F29
		private void LearnFromNonNullTest(int slot, ref NullableWalker.LocalState state)
		{
			state[slot] = NullableFlowState.NotNull;
		}

		// Token: 0x06001C3B RID: 7227 RVA: 0x00094D34 File Offset: 0x00092F34
		private void LearnFromNullTest(BoundExpression expression, ref NullableWalker.LocalState state)
		{
			if (expression.ConstantValue != null)
			{
				return;
			}
			BoundExpression item = NullableWalker.RemoveConversion(expression, true).Item1;
			int num = this.MakeSlot(item);
			this.LearnFromNullTest(num, item.Type, ref state, false);
		}

		// Token: 0x06001C3C RID: 7228 RVA: 0x00094D74 File Offset: 0x00092F74
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private void LearnFromNullTest(int slot, TypeSymbol expressionType, ref NullableWalker.LocalState state, bool markDependentSlotsNotNull)
		{
			if (slot > 0 && NullableWalker.PossiblyNullableType(expressionType))
			{
				if (state[slot] == NullableFlowState.NotNull)
				{
					state[slot] = NullableFlowState.MaybeNull;
				}
				if (markDependentSlotsNotNull)
				{
					this.MarkDependentSlotsNotNull(slot, expressionType, ref state, 2);
				}
			}
		}

		// Token: 0x06001C3D RID: 7229 RVA: 0x00094DA4 File Offset: 0x00092FA4
		private void MarkDependentSlotsNotNull(int slot, TypeSymbol expressionType, ref NullableWalker.LocalState state, int depth = 2)
		{
			if (depth <= 0)
			{
				return;
			}
			foreach (Symbol symbol in NullableWalker.<MarkDependentSlotsNotNull>g__getMembers|168_0(expressionType))
			{
				CompoundUseSiteInfo<AssemblySymbol> discarded = CompoundUseSiteInfo<AssemblySymbol>.Discarded;
				Symbol symbol2 = this._symbol;
				NamedTypeSymbol namedTypeSymbol = ((symbol2 != null) ? symbol2.ContainingType : null);
				PropertySymbol propertySymbol = symbol as PropertySymbol;
				if (((propertySymbol != null && !propertySymbol.IsIndexedProperty) || symbol.Kind == SymbolKind.Field) && symbol.RequiresInstanceReceiver() && (namedTypeSymbol == null || AccessCheck.IsSymbolAccessible(symbol, namedTypeSymbol, ref discarded, null)))
				{
					int orCreateSlot = this.GetOrCreateSlot(symbol, slot, true, false);
					if (orCreateSlot > 0)
					{
						state[orCreateSlot] = NullableFlowState.NotNull;
						this.MarkDependentSlotsNotNull(orCreateSlot, symbol.GetTypeOrReturnType().Type, ref state, depth - 1);
					}
				}
			}
		}

		// Token: 0x06001C3E RID: 7230 RVA: 0x00094E7C File Offset: 0x0009307C
		private static BoundExpression SkipReferenceConversions(BoundExpression possiblyConversion)
		{
			while (possiblyConversion.Kind == BoundKind.Conversion)
			{
				BoundConversion boundConversion = (BoundConversion)possiblyConversion;
				ConversionKind conversionKind = boundConversion.ConversionKind;
				if (conversionKind != ConversionKind.ImplicitReference && conversionKind != ConversionKind.ExplicitReference)
				{
					return possiblyConversion;
				}
				possiblyConversion = boundConversion.Operand;
			}
			return possiblyConversion;
		}

		// Token: 0x06001C3F RID: 7231 RVA: 0x00094EBC File Offset: 0x000930BC
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitNullCoalescingAssignmentOperator(BoundNullCoalescingAssignmentOperator node)
		{
			BoundExpression leftOperand = node.LeftOperand;
			BoundExpression rightOperand = node.RightOperand;
			int num = this.MakeSlot(leftOperand);
			TypeWithAnnotations typeWithAnnotations = this.VisitLvalueWithAnnotations(leftOperand);
			NullableWalker.LocalState localState = this.State.Clone();
			this.LearnFromNonNullTest(leftOperand, ref localState);
			this.LearnFromNullTest(leftOperand, ref this.State);
			if (node.IsNullableValueTypeAssignment)
			{
				typeWithAnnotations = TypeWithAnnotations.Create(node.Type, NullableAnnotation.NotAnnotated, default(ImmutableArray<CustomModifier>));
			}
			TypeWithState typeWithState = this.VisitOptionalImplicitConversion(rightOperand, typeWithAnnotations, NullableWalker.UseLegacyWarnings(leftOperand, typeWithAnnotations), false, NullableWalker.AssignmentKind.Assignment);
			this.TrackNullableStateForAssignment(rightOperand, typeWithAnnotations, num, typeWithState, this.MakeSlot(rightOperand));
			this.Join(ref this.State, ref localState);
			TypeWithState typeWithState2 = TypeWithState.Create(typeWithAnnotations.Type, typeWithState.State);
			this.SetResultType(node, typeWithState2, true);
			return null;
		}

		// Token: 0x06001C40 RID: 7232 RVA: 0x00094F80 File Offset: 0x00093180
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitNullCoalescingOperator(BoundNullCoalescingOperator node)
		{
			NullableWalker.<>c__DisplayClass171_0 CS$<>8__locals1;
			CS$<>8__locals1.node = node;
			CS$<>8__locals1.<>4__this = this;
			BoundExpression leftOperand = CS$<>8__locals1.node.LeftOperand;
			BoundExpression rightOperand = CS$<>8__locals1.node.RightOperand;
			TypeWithState typeWithState = this.VisitRvalueWithState(leftOperand);
			TypeWithState typeWithState2;
			if (AbstractFlowPass<NullableWalker.LocalState, NullableWalker.LocalFunctionState>.IsConstantNull(leftOperand))
			{
				typeWithState2 = this.VisitRvalueWithState(rightOperand);
				this.SetResultType(CS$<>8__locals1.node, TypeWithState.Create(CS$<>8__locals1.node.Type, typeWithState2.State), true);
				return null;
			}
			NullableWalker.LocalState localState = this.State.Clone();
			this.LearnFromNonNullTest(leftOperand, ref localState);
			this.LearnFromNullTest(leftOperand, ref this.State);
			if (leftOperand.ConstantValue != null)
			{
				base.SetUnreachable();
			}
			typeWithState2 = this.VisitRvalueWithState(rightOperand);
			this.Join(ref this.State, ref localState);
			ConstantValue constantValue = rightOperand.ConstantValue;
			if (constantValue != null && constantValue.IsBoolean)
			{
				base.Split();
				if (rightOperand.ConstantValue.BooleanValue)
				{
					this.StateWhenFalse = localState;
				}
				else
				{
					this.StateWhenTrue = localState;
				}
			}
			TypeSymbol type = typeWithState.Type;
			TypeSymbol type2 = typeWithState2.Type;
			ValueTuple<TypeSymbol, NullableFlowState> valueTuple;
			switch (CS$<>8__locals1.node.OperatorResultKind)
			{
			case BoundNullCoalescingOperatorResultKind.NoCommonType:
				valueTuple = new ValueTuple<TypeSymbol, NullableFlowState>(CS$<>8__locals1.node.Type, NullableFlowState.NotNull);
				break;
			case BoundNullCoalescingOperatorResultKind.LeftType:
				valueTuple = this.<VisitNullCoalescingOperator>g__getLeftResultType|171_0(type, type2, ref CS$<>8__locals1);
				break;
			case BoundNullCoalescingOperatorResultKind.LeftUnwrappedType:
				valueTuple = this.<VisitNullCoalescingOperator>g__getLeftResultType|171_0(type.StrippedType(), type2, ref CS$<>8__locals1);
				break;
			case BoundNullCoalescingOperatorResultKind.RightType:
				valueTuple = this.<VisitNullCoalescingOperator>g__getResultStateWithRightType|171_1(type, type2, ref CS$<>8__locals1);
				break;
			case BoundNullCoalescingOperatorResultKind.LeftUnwrappedRightType:
				valueTuple = this.<VisitNullCoalescingOperator>g__getResultStateWithRightType|171_1(type.StrippedType(), type2, ref CS$<>8__locals1);
				break;
			case BoundNullCoalescingOperatorResultKind.RightDynamicType:
				valueTuple = new ValueTuple<TypeSymbol, NullableFlowState>(type2, NullableFlowState.NotNull);
				break;
			default:
				throw ExceptionUtilities.UnexpectedValue(CS$<>8__locals1.node.OperatorResultKind);
			}
			ValueTuple<TypeSymbol, NullableFlowState> valueTuple2 = valueTuple;
			TypeSymbol item = valueTuple2.Item1;
			NullableFlowState item2 = valueTuple2.Item2;
			this.SetResultType(CS$<>8__locals1.node, TypeWithState.Create(item, typeWithState2.State.Join(item2)), true);
			return null;
		}

		// Token: 0x06001C41 RID: 7233 RVA: 0x00095170 File Offset: 0x00093370
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitConditionalAccess(BoundConditionalAccess node)
		{
			BoundExpression receiver = node.Receiver;
			this.VisitRvalueWithState(receiver);
			this._currentConditionalReceiverVisitResult = this._visitResult;
			int lastConditionalAccessSlot = this._lastConditionalAccessSlot;
			NullableWalker.LocalState localState = this.State.Clone();
			if (AbstractFlowPass<NullableWalker.LocalState, NullableWalker.LocalFunctionState>.IsConstantNull(node.Receiver))
			{
				base.SetUnreachable();
				this._lastConditionalAccessSlot = -1;
			}
			else
			{
				this.LearnFromNullTest(receiver, ref localState);
				this.LearnFromNonNullTest(receiver, ref this.State);
				int num = this.MakeSlot(receiver);
				if (num > 0)
				{
					TypeSymbol type = receiver.Type;
					if (type != null && type.IsNullableType())
					{
						Symbol symbol;
						num = this.GetNullableOfTValueSlot(receiver.Type, num, out symbol, false);
					}
				}
				this._lastConditionalAccessSlot = num;
			}
			TypeWithAnnotations typeWithAnnotations = this.VisitLvalueWithAnnotations(node.AccessExpression);
			TypeSymbol type2 = typeWithAnnotations.Type;
			this.Join(ref this.State, ref localState);
			TypeSymbol type3 = node.Type;
			TypeSymbol typeSymbol = ((type3.IsVoidType() || type3.IsErrorType()) ? type3 : ((type3.IsNullableType() && !type2.IsNullableType()) ? this.MakeNullableOf(typeWithAnnotations) : type2));
			this.SetResultType(node, TypeWithState.Create(typeSymbol, NullableFlowState.MaybeDefault), true);
			this._currentConditionalReceiverVisitResult = default(NullableWalker.VisitResult);
			this._lastConditionalAccessSlot = lastConditionalAccessSlot;
			return null;
		}

		// Token: 0x06001C42 RID: 7234 RVA: 0x000952A0 File Offset: 0x000934A0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		protected override BoundNode VisitConditionalOperatorCore(BoundExpression node, bool isRef, BoundExpression condition, BoundExpression originalConsequence, BoundExpression originalAlternative)
		{
			base.VisitCondition(condition);
			NullableWalker.LocalState localState = this.StateWhenTrue;
			NullableWalker.LocalState stateWhenFalse = this.StateWhenFalse;
			TypeWithState typeWithState;
			TypeWithState typeWithState2;
			if (isRef)
			{
				ValueTuple<TypeWithAnnotations, TypeWithState> valueTuple = this.<VisitConditionalOperatorCore>g__visitConditionalRefOperand|173_1(localState, originalConsequence);
				TypeWithAnnotations item = valueTuple.Item1;
				typeWithState = valueTuple.Item2;
				localState = this.State;
				ValueTuple<TypeWithAnnotations, TypeWithState> valueTuple2 = this.<VisitConditionalOperatorCore>g__visitConditionalRefOperand|173_1(stateWhenFalse, originalAlternative);
				TypeWithAnnotations item2 = valueTuple2.Item1;
				typeWithState2 = valueTuple2.Item2;
				this.Join(ref this.State, ref localState);
				TypeSymbol type = node.Type;
				TypeSymbol typeSymbol = ((type != null) ? type.SetUnknownNullabilityForReferenceTypes() : null);
				if (NullableWalker.IsNullabilityMismatch(item, item2))
				{
					this.ReportNullabilityMismatchInAssignment(node.Syntax, item, item2);
				}
				else if (!node.HasErrors)
				{
					typeSymbol = typeWithState.Type.MergeEquivalentTypes(typeWithState2.Type, VarianceKind.None);
				}
				NullableAnnotation nullableAnnotation = item.NullableAnnotation.EnsureCompatible(item2.NullableAnnotation);
				NullableFlowState nullableFlowState = typeWithState.State.Join(typeWithState2.State);
				this.SetResult(node, TypeWithState.Create(typeSymbol, nullableFlowState), TypeWithAnnotations.Create(typeSymbol, nullableAnnotation, default(ImmutableArray<CustomModifier>)), true, default(bool?));
				return null;
			}
			BoundExpression boundExpression;
			Conversion conversion;
			BoundExpression boundExpression2;
			Conversion conversion2;
			bool flag;
			bool flag2;
			if (!stateWhenFalse.Reachable)
			{
				ValueTuple<BoundExpression, Conversion, TypeWithState> valueTuple3 = this.<VisitConditionalOperatorCore>g__visitConditionalOperand|173_0(stateWhenFalse, originalAlternative);
				boundExpression = valueTuple3.Item1;
				conversion = valueTuple3.Item2;
				typeWithState2 = valueTuple3.Item3;
				ValueTuple<BoundExpression, Conversion, TypeWithState> valueTuple4 = this.<VisitConditionalOperatorCore>g__visitConditionalOperand|173_0(localState, originalConsequence);
				boundExpression2 = valueTuple4.Item1;
				conversion2 = valueTuple4.Item2;
				typeWithState = valueTuple4.Item3;
				flag = false;
				flag2 = this.IsReachable();
			}
			else if (!localState.Reachable)
			{
				ValueTuple<BoundExpression, Conversion, TypeWithState> valueTuple5 = this.<VisitConditionalOperatorCore>g__visitConditionalOperand|173_0(localState, originalConsequence);
				boundExpression2 = valueTuple5.Item1;
				conversion2 = valueTuple5.Item2;
				typeWithState = valueTuple5.Item3;
				ValueTuple<BoundExpression, Conversion, TypeWithState> valueTuple6 = this.<VisitConditionalOperatorCore>g__visitConditionalOperand|173_0(stateWhenFalse, originalAlternative);
				boundExpression = valueTuple6.Item1;
				conversion = valueTuple6.Item2;
				typeWithState2 = valueTuple6.Item3;
				flag2 = false;
				flag = this.IsReachable();
			}
			else
			{
				ValueTuple<BoundExpression, Conversion, TypeWithState> valueTuple7 = this.<VisitConditionalOperatorCore>g__visitConditionalOperand|173_0(localState, originalConsequence);
				boundExpression2 = valueTuple7.Item1;
				conversion2 = valueTuple7.Item2;
				typeWithState = valueTuple7.Item3;
				base.Unsplit();
				localState = this.State;
				flag2 = localState.Reachable;
				ValueTuple<BoundExpression, Conversion, TypeWithState> valueTuple8 = this.<VisitConditionalOperatorCore>g__visitConditionalOperand|173_0(stateWhenFalse, originalAlternative);
				boundExpression = valueTuple8.Item1;
				conversion = valueTuple8.Item2;
				typeWithState2 = valueTuple8.Item3;
				base.Unsplit();
				flag = this.State.Reachable;
				this.Join(ref this.State, ref localState);
			}
			BoundConditionalOperator boundConditionalOperator = node as BoundConditionalOperator;
			bool flag3 = boundConditionalOperator != null && boundConditionalOperator.WasTargetTyped;
			TypeSymbol typeSymbol2;
			if (node.HasErrors || flag3)
			{
				typeSymbol2 = null;
			}
			else
			{
				BoundExpression boundExpression3 = NullableWalker.CreatePlaceholderIfNecessary(boundExpression2, typeWithState.ToTypeWithAnnotations(this.compilation, false));
				BoundExpression boundExpression4 = NullableWalker.CreatePlaceholderIfNecessary(boundExpression, typeWithState2.ToTypeWithAnnotations(this.compilation, false));
				CompoundUseSiteInfo<AssemblySymbol> discarded = CompoundUseSiteInfo<AssemblySymbol>.Discarded;
				bool flag4;
				typeSymbol2 = BestTypeInferrer.InferBestTypeForConditionalOperator(boundExpression3, boundExpression4, this._conversions, out flag4, ref discarded);
			}
			if (typeSymbol2 == null)
			{
				TypeSymbol type2 = node.Type;
				typeSymbol2 = ((type2 != null) ? type2.SetUnknownNullabilityForReferenceTypes() : null);
			}
			NullableFlowState nullableFlowState2;
			if (!flag3)
			{
				if (typeSymbol2 == null)
				{
					nullableFlowState2 = NullableFlowState.NotNull;
				}
				else
				{
					TypeWithAnnotations typeWithAnnotations = TypeWithAnnotations.Create(typeSymbol2, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
					ref TypeWithState ptr = this.ConvertConditionalOperandOrSwitchExpressionArmResult(originalConsequence, boundExpression2, conversion2, typeWithAnnotations, typeWithState, localState, flag2);
					TypeWithState typeWithState3 = this.ConvertConditionalOperandOrSwitchExpressionArmResult(originalAlternative, boundExpression, conversion, typeWithAnnotations, typeWithState2, stateWhenFalse, flag);
					nullableFlowState2 = ptr.State.Join(typeWithState3.State);
				}
			}
			else
			{
				nullableFlowState2 = typeWithState.State.Join(typeWithState2.State);
				this.ConditionalInfoForConversion.Add(node, ImmutableArray.Create<ValueTuple<NullableWalker.LocalState, TypeWithState, bool>>(new ValueTuple<NullableWalker.LocalState, TypeWithState, bool>(localState, typeWithState, flag2), new ValueTuple<NullableWalker.LocalState, TypeWithState, bool>(stateWhenFalse, typeWithState2, flag)));
			}
			this.SetResultType(node, TypeWithState.Create(typeSymbol2, nullableFlowState2), true);
			return null;
		}

		// Token: 0x06001C43 RID: 7235 RVA: 0x0009560C File Offset: 0x0009380C
		private TypeWithState ConvertConditionalOperandOrSwitchExpressionArmResult(BoundExpression node, BoundExpression operand, Conversion conversion, TypeWithAnnotations targetType, TypeWithState operandType, NullableWalker.LocalState state, bool isReachable)
		{
			NullableWalker.LocalState state2 = this.State;
			this.State = state;
			bool disableDiagnostics = this._disableDiagnostics;
			if (!isReachable)
			{
				this._disableDiagnostics = true;
			}
			TypeWithState typeWithState = this.VisitConversion(NullableWalker.GetConversionIfApplicable(node, operand), operand, conversion, targetType, operandType, true, false, false, NullableWalker.AssignmentKind.Assignment, null, false, true, false, default(Optional<NullableWalker.LocalState>), false, null);
			if (!isReachable)
			{
				typeWithState = default(TypeWithState);
				this._disableDiagnostics = disableDiagnostics;
			}
			this.State = state2;
			return typeWithState;
		}

		// Token: 0x06001C44 RID: 7236 RVA: 0x0009567C File Offset: 0x0009387C
		private bool IsReachable()
		{
			if (!this.IsConditionalState)
			{
				return this.State.Reachable;
			}
			return this.StateWhenTrue.Reachable || this.StateWhenFalse.Reachable;
		}

		// Token: 0x06001C45 RID: 7237 RVA: 0x000956AC File Offset: 0x000938AC
		private static BoundExpression CreatePlaceholderIfNecessary(BoundExpression expr, TypeWithAnnotations type)
		{
			if (type.HasType)
			{
				return new BoundExpressionWithNullability(expr.Syntax, expr, type.NullableAnnotation, type.Type);
			}
			return expr;
		}

		// Token: 0x06001C46 RID: 7238 RVA: 0x000956D4 File Offset: 0x000938D4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitConditionalReceiver(BoundConditionalReceiver node)
		{
			TypeSymbol typeSymbol = this._currentConditionalReceiverVisitResult.RValueType.Type;
			if (typeSymbol != null && typeSymbol.IsNullableType())
			{
				typeSymbol = typeSymbol.GetNullableUnderlyingType();
			}
			this.SetResultType(node, TypeWithState.Create(typeSymbol, NullableFlowState.NotNull), true);
			return null;
		}

		// Token: 0x06001C47 RID: 7239 RVA: 0x00095714 File Offset: 0x00093914
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitCall(BoundCall node)
		{
			TypeWithState typeWithState = this.VisitCallReceiver(node);
			this.ReinferMethodAndVisitArguments(node, typeWithState);
			return null;
		}

		// Token: 0x06001C48 RID: 7240 RVA: 0x00095734 File Offset: 0x00093934
		private void ReinferMethodAndVisitArguments(BoundCall node, TypeWithState receiverType)
		{
			MethodSymbol methodSymbol = node.Method;
			ImmutableArray<RefKind> argumentRefKindsOpt = node.ArgumentRefKindsOpt;
			if (!receiverType.HasNullType)
			{
				methodSymbol = (MethodSymbol)NullableWalker.AsMemberOfType(receiverType.Type, methodSymbol);
			}
			ValueTuple<MethodSymbol, ImmutableArray<NullableWalker.VisitArgumentResult>, bool> valueTuple = this.VisitArguments(node, node.Arguments, argumentRefKindsOpt, methodSymbol.Parameters, node.ArgsToParamsOpt, node.DefaultArguments, node.Expanded, node.InvokedAsExtensionMethod, methodSymbol);
			methodSymbol = valueTuple.Item1;
			ImmutableArray<NullableWalker.VisitArgumentResult> item = valueTuple.Item2;
			bool item2 = valueTuple.Item3;
			this.ApplyMemberPostConditions(node.ReceiverOpt, methodSymbol);
			this.LearnFromEqualsMethod(methodSymbol, node, receiverType, item);
			TypeWithState typeWithState = this.GetReturnTypeWithState(methodSymbol);
			if (item2)
			{
				typeWithState = typeWithState.WithNotNullState();
			}
			this.SetResult(node, typeWithState, methodSymbol.ReturnTypeWithAnnotations, true, default(bool?));
			this.SetUpdatedSymbol(node, node.Method, methodSymbol);
		}

		// Token: 0x06001C49 RID: 7241 RVA: 0x000957FC File Offset: 0x000939FC
		private void LearnFromEqualsMethod(MethodSymbol method, BoundCall node, TypeWithState receiverType, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<NullableWalker.VisitArgumentResult> results)
		{
			int parameterCount = method.ParameterCount;
			ImmutableArray<BoundExpression> arguments = node.Arguments;
			if (node.HasErrors || (parameterCount != 1 && parameterCount != 2) || parameterCount != arguments.Length || method.MethodKind != MethodKind.Ordinary || method.ReturnType.SpecialType != SpecialType.System_Boolean || (method.Name != SpecialMembers.GetDescriptor(SpecialMember.System_Object__Equals).Name && method.Name != SpecialMembers.GetDescriptor(SpecialMember.System_Object__ReferenceEquals).Name && !NullableWalker.<LearnFromEqualsMethod>g__anyOverriddenMethodHasExplicitImplementation|180_0(method)))
			{
				return;
			}
			if (method.Equals(this.compilation.GetSpecialTypeMember(SpecialMember.System_Object__EqualsObjectObject)) || method.Equals(this.compilation.GetSpecialTypeMember(SpecialMember.System_Object__ReferenceEquals)) || NullableWalker.<LearnFromEqualsMethod>g__isWellKnownEqualityMethodOrImplementation|180_1(this.compilation, method, receiverType.Type, WellKnownMember.System_Collections_Generic_IEqualityComparer_T__Equals))
			{
				this.<LearnFromEqualsMethod>g__learnFromEqualsMethodArguments|180_2(arguments[0], results[0].RValueType, arguments[1], results[1].RValueType);
				return;
			}
			bool flag = method.GetLeastOverriddenMethod(null).Equals(this.compilation.GetSpecialTypeMember(SpecialMember.System_Object__Equals));
			BoundExpression receiverOpt = node.ReceiverOpt;
			if (receiverOpt != null && (flag || NullableWalker.<LearnFromEqualsMethod>g__isWellKnownEqualityMethodOrImplementation|180_1(this.compilation, method, receiverType.Type, WellKnownMember.System_IEquatable_T__Equals)))
			{
				this.<LearnFromEqualsMethod>g__learnFromEqualsMethodArguments|180_2(receiverOpt, receiverType, arguments[0], results[0].RValueType);
				return;
			}
		}

		// Token: 0x06001C4A RID: 7242 RVA: 0x00095960 File Offset: 0x00093B60
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private bool IsCompareExchangeMethod(MethodSymbol method)
		{
			return method != null && (method.Equals(this.compilation.GetWellKnownTypeMember(WellKnownMember.System_Threading_Interlocked__CompareExchange), SymbolEqualityComparer.ConsiderEverything.CompareKind) || method.OriginalDefinition.Equals(this.compilation.GetWellKnownTypeMember(WellKnownMember.System_Threading_Interlocked__CompareExchange_T), SymbolEqualityComparer.ConsiderEverything.CompareKind));
		}

		// Token: 0x06001C4B RID: 7243 RVA: 0x000959BC File Offset: 0x00093BBC
		private NullableFlowState LearnFromCompareExchangeMethod(in NullableWalker.CompareExchangeInfo compareExchangeInfo)
		{
			if (compareExchangeInfo.Arguments.Length != 3)
			{
				return NullableFlowState.NotNull;
			}
			ImmutableArray<int> argsToParamsOpt = compareExchangeInfo.ArgsToParamsOpt;
			int num4;
			int num5;
			int num6;
			if (!argsToParamsOpt.IsDefault)
			{
				int num = argsToParamsOpt.IndexOf(2);
				int num2 = argsToParamsOpt.IndexOf(1);
				int num3 = argsToParamsOpt.IndexOf(0);
				num4 = num;
				num5 = num2;
				num6 = num3;
			}
			else
			{
				num4 = 2;
				num5 = 1;
				num6 = 0;
			}
			BoundExpression boundExpression = compareExchangeInfo.Arguments[num4];
			NullableFlowState nullableFlowState = compareExchangeInfo.Results[num5].RValueType.State;
			ConstantValue constantValue = boundExpression.ConstantValue;
			if (constantValue == null || !constantValue.IsNull)
			{
				NullableFlowState state = compareExchangeInfo.Results[num6].RValueType.State;
				nullableFlowState = nullableFlowState.Join(state);
			}
			return nullableFlowState;
		}

		// Token: 0x06001C4C RID: 7244 RVA: 0x00095A8C File Offset: 0x00093C8C
		private TypeWithState VisitCallReceiver(BoundCall node)
		{
			BoundExpression receiverOpt = node.ReceiverOpt;
			TypeWithState typeWithState = default(TypeWithState);
			if (receiverOpt != null)
			{
				typeWithState = this.VisitRvalueWithState(receiverOpt);
				bool flag = false;
				TypeSymbol type = typeWithState.Type;
				MethodSymbol method = node.Method;
				if (method.RequiresInstanceReceiver && type != null && type.IsNullableType() && method.ContainingType.IsReferenceType)
				{
					flag = true;
				}
				else if (method.OriginalDefinition == this.compilation.GetSpecialTypeMember(SpecialMember.System_Nullable_T_get_Value))
				{
					flag = true;
				}
				this.CheckPossibleNullReceiver(receiverOpt, flag);
			}
			return typeWithState;
		}

		// Token: 0x06001C4D RID: 7245 RVA: 0x00095B11 File Offset: 0x00093D11
		private TypeWithState GetReturnTypeWithState(MethodSymbol method)
		{
			return TypeWithState.Create(method.ReturnTypeWithAnnotations, this.GetRValueAnnotations(method));
		}

		// Token: 0x06001C4E RID: 7246 RVA: 0x00095B25 File Offset: 0x00093D25
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private FlowAnalysisAnnotations GetRValueAnnotations(Symbol symbol)
		{
			if (this.IsAnalyzingAttribute)
			{
				return FlowAnalysisAnnotations.None;
			}
			return symbol.GetFlowAnalysisAnnotations() & (FlowAnalysisAnnotations.MaybeNullWhenTrue | FlowAnalysisAnnotations.MaybeNullWhenFalse | FlowAnalysisAnnotations.NotNullWhenTrue | FlowAnalysisAnnotations.NotNullWhenFalse);
		}

		// Token: 0x06001C4F RID: 7247 RVA: 0x00095B3A File Offset: 0x00093D3A
		private FlowAnalysisAnnotations GetParameterAnnotations(ParameterSymbol parameter)
		{
			if (!this.IsAnalyzingAttribute)
			{
				return parameter.FlowAnalysisAnnotations;
			}
			return FlowAnalysisAnnotations.None;
		}

		// Token: 0x06001C50 RID: 7248 RVA: 0x00095B4C File Offset: 0x00093D4C
		private static TypeWithAnnotations ApplyLValueAnnotations(TypeWithAnnotations declaredType, FlowAnalysisAnnotations flowAnalysisAnnotations)
		{
			if ((flowAnalysisAnnotations & FlowAnalysisAnnotations.DisallowNull) == FlowAnalysisAnnotations.DisallowNull)
			{
				return declaredType.AsNotAnnotated();
			}
			if ((flowAnalysisAnnotations & FlowAnalysisAnnotations.AllowNull) == FlowAnalysisAnnotations.AllowNull)
			{
				return declaredType.AsAnnotated();
			}
			return declaredType;
		}

		// Token: 0x06001C51 RID: 7249 RVA: 0x00095B6B File Offset: 0x00093D6B
		private static TypeWithState ApplyUnconditionalAnnotations(TypeWithState typeWithState, FlowAnalysisAnnotations annotations)
		{
			if ((annotations & FlowAnalysisAnnotations.NotNull) == FlowAnalysisAnnotations.NotNull)
			{
				return TypeWithState.Create(typeWithState.Type, NullableFlowState.NotNull);
			}
			if ((annotations & FlowAnalysisAnnotations.MaybeNull) == FlowAnalysisAnnotations.MaybeNull)
			{
				return TypeWithState.Create(typeWithState.Type, NullableFlowState.MaybeDefault);
			}
			return typeWithState;
		}

		// Token: 0x06001C52 RID: 7250 RVA: 0x00095B98 File Offset: 0x00093D98
		private static TypeWithAnnotations ApplyUnconditionalAnnotations(TypeWithAnnotations declaredType, FlowAnalysisAnnotations annotations)
		{
			if ((annotations & FlowAnalysisAnnotations.MaybeNull) == FlowAnalysisAnnotations.MaybeNull)
			{
				return declaredType.AsAnnotated();
			}
			if ((annotations & FlowAnalysisAnnotations.NotNull) == FlowAnalysisAnnotations.NotNull)
			{
				return declaredType.AsNotAnnotated();
			}
			return declaredType;
		}

		// Token: 0x06001C53 RID: 7251 RVA: 0x00095BBC File Offset: 0x00093DBC
		private static bool HasImplicitTypeArguments(BoundNode node)
		{
			BoundCollectionElementInitializer boundCollectionElementInitializer = node as BoundCollectionElementInitializer;
			if (boundCollectionElementInitializer != null)
			{
				MethodSymbol methodSymbol = boundCollectionElementInitializer.AddMethod;
				if (methodSymbol != null && !methodSymbol.TypeArgumentsWithAnnotations.IsEmpty)
				{
					return true;
				}
			}
			BoundForEachStatement boundForEachStatement = node as BoundForEachStatement;
			if (boundForEachStatement != null)
			{
				ForEachEnumeratorInfo enumeratorInfoOpt = boundForEachStatement.EnumeratorInfoOpt;
				if (enumeratorInfoOpt != null)
				{
					MethodArgumentInfo getEnumeratorInfo = enumeratorInfoOpt.GetEnumeratorInfo;
					if (getEnumeratorInfo != null)
					{
						MethodSymbol methodSymbol = getEnumeratorInfo.Method;
						if (methodSymbol != null && !methodSymbol.TypeArgumentsWithAnnotations.IsEmpty)
						{
							return true;
						}
					}
				}
			}
			SyntaxNode syntax = node.Syntax;
			return syntax.Kind() == SyntaxKind.InvocationExpression && NullableWalker.HasImplicitTypeArguments(((InvocationExpressionSyntax)syntax).Expression);
		}

		// Token: 0x06001C54 RID: 7252 RVA: 0x00095C58 File Offset: 0x00093E58
		private static bool HasImplicitTypeArguments(SyntaxNode syntax)
		{
			string text;
			NameSyntax nameSyntax = Binder.GetNameSyntax(syntax, out text);
			if (nameSyntax == null)
			{
				return false;
			}
			nameSyntax = nameSyntax.GetUnqualifiedName();
			return nameSyntax.Kind() != SyntaxKind.GenericName;
		}

		// Token: 0x06001C55 RID: 7253 RVA: 0x00095C8A File Offset: 0x00093E8A
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		protected override void VisitArguments([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments, ImmutableArray<RefKind> refKindsOpt, [global::System.Runtime.CompilerServices.Nullable(1)] MethodSymbol method)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06001C56 RID: 7254 RVA: 0x00095C94 File Offset: 0x00093E94
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[return: TupleElementNames(new string[] { "method", "results", "returnNotNull" })]
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2, 0 })]
		private ValueTuple<MethodSymbol, ImmutableArray<NullableWalker.VisitArgumentResult>, bool> VisitArguments([global::System.Runtime.CompilerServices.Nullable(1)] BoundExpression node, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments, ImmutableArray<RefKind> refKindsOpt, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol method, ImmutableArray<int> argsToParamsOpt, BitVector defaultArguments, bool expanded, bool invokedAsExtensionMethod)
		{
			return this.VisitArguments(node, arguments, refKindsOpt, (method == null) ? default(ImmutableArray<ParameterSymbol>) : method.Parameters, argsToParamsOpt, defaultArguments, expanded, invokedAsExtensionMethod, method);
		}

		// Token: 0x06001C57 RID: 7255 RVA: 0x00095CCC File Offset: 0x00093ECC
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private ImmutableArray<NullableWalker.VisitArgumentResult> VisitArguments([global::System.Runtime.CompilerServices.Nullable(1)] BoundExpression node, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments, ImmutableArray<RefKind> refKindsOpt, [global::System.Runtime.CompilerServices.Nullable(2)] PropertySymbol property, ImmutableArray<int> argsToParamsOpt, BitVector defaultArguments, bool expanded)
		{
			return this.VisitArguments(node, arguments, refKindsOpt, (property == null) ? default(ImmutableArray<ParameterSymbol>) : property.Parameters, argsToParamsOpt, defaultArguments, expanded, false, null).Item2;
		}

		// Token: 0x06001C58 RID: 7256 RVA: 0x00095D08 File Offset: 0x00093F08
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[return: TupleElementNames(new string[] { "method", "results", "returnNotNull" })]
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2, 0 })]
		private ValueTuple<MethodSymbol, ImmutableArray<NullableWalker.VisitArgumentResult>, bool> VisitArguments([global::System.Runtime.CompilerServices.Nullable(1)] BoundNode node, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments, ImmutableArray<RefKind> refKindsOpt, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<ParameterSymbol> parametersOpt, ImmutableArray<int> argsToParamsOpt, BitVector defaultArguments, bool expanded, bool invokedAsExtensionMethod, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol method = null)
		{
			bool flag = false;
			ValueTuple<ImmutableArray<BoundExpression>, ImmutableArray<Conversion>> valueTuple = this.RemoveArgumentConversions(arguments, refKindsOpt);
			ImmutableArray<BoundExpression> item = valueTuple.Item1;
			ImmutableArray<Conversion> item2 = valueTuple.Item2;
			ImmutableArray<NullableWalker.VisitArgumentResult> immutableArray = this.VisitArgumentsEvaluate(node.Syntax, item, refKindsOpt, parametersOpt, argsToParamsOpt, defaultArguments, expanded);
			if (method != null && method.IsGenericMethod)
			{
				if (NullableWalker.HasImplicitTypeArguments(node))
				{
					method = this.InferMethodTypeArguments(method, this.GetArgumentsForMethodTypeInference(immutableArray, item), refKindsOpt, argsToParamsOpt, expanded);
					parametersOpt = method.Parameters;
				}
				if (ConstraintsHelper.RequiresChecking(method))
				{
					SyntaxNode syntax = node.Syntax;
					InvocationExpressionSyntax invocationExpressionSyntax = syntax as InvocationExpressionSyntax;
					SyntaxNode syntaxNode;
					if (invocationExpressionSyntax != null)
					{
						ExpressionSyntax expression = invocationExpressionSyntax.Expression;
						syntaxNode = expression;
					}
					else
					{
						ForEachStatementSyntax forEachStatementSyntax = syntax as ForEachStatementSyntax;
						if (forEachStatementSyntax != null)
						{
							ExpressionSyntax expression2 = forEachStatementSyntax.Expression;
							syntaxNode = expression2;
						}
						else
						{
							syntaxNode = syntax;
						}
					}
					this.CheckMethodConstraints(syntaxNode, method);
				}
			}
			bool flag2;
			if (!this.IsAnalyzingAttribute && !parametersOpt.IsDefault)
			{
				flag2 = parametersOpt.Any<ParameterSymbol>((ParameterSymbol p) => !p.NotNullIfParameterNotNull.IsEmpty);
			}
			else
			{
				flag2 = false;
			}
			ArrayBuilder<ParameterSymbol> arrayBuilder = (flag2 ? ArrayBuilder<ParameterSymbol>.GetInstance() : null);
			if (!node.HasErrors && !parametersOpt.IsDefault)
			{
				ImmutableHashSet<string> immutableHashSet = (this.IsAnalyzingAttribute ? null : ((method != null) ? method.ReturnNotNullIfParameterNotNull : null));
				for (int i = 0; i < immutableArray.Length; i++)
				{
					ValueTuple<ParameterSymbol, TypeWithAnnotations, FlowAnalysisAnnotations, bool> correspondingParameter = this.GetCorrespondingParameter(i, parametersOpt, argsToParamsOpt, expanded);
					ParameterSymbol item3 = correspondingParameter.Item1;
					TypeWithAnnotations item4 = correspondingParameter.Item2;
					FlowAnalysisAnnotations item5 = correspondingParameter.Item3;
					bool item6 = correspondingParameter.Item4;
					if (item3 != null)
					{
						BoundExpression boundExpression = item[i];
						BoundExpression boundExpression2 = ((i < arguments.Length) ? arguments[i] : item[i]);
						bool disableDiagnostics = this._disableDiagnostics;
						this._disableDiagnostics |= defaultArguments[i];
						this.VisitArgumentConversionAndInboundAssignmentsAndPreConditions(NullableWalker.GetConversionIfApplicable(boundExpression2, boundExpression), boundExpression, (item2.IsDefault || i >= item2.Length) ? Conversion.Identity : item2[i], AbstractFlowPass<NullableWalker.LocalState, NullableWalker.LocalFunctionState>.GetRefKind(refKindsOpt, i), item3, item4, item5, immutableArray[i], invokedAsExtensionMethod && i == 0);
						this._disableDiagnostics = disableDiagnostics;
						if (immutableArray[i].RValueType.IsNotNull || item6)
						{
							if (arrayBuilder != null)
							{
								arrayBuilder.Add(item3);
							}
							if (immutableHashSet != null && immutableHashSet.Contains(item3.Name))
							{
								flag = true;
							}
						}
					}
				}
			}
			else
			{
				for (int j = 0; j < immutableArray.Length; j++)
				{
					BoundLambda boundLambda = item[j] as BoundLambda;
					if (boundLambda != null)
					{
						this.VisitLambda(boundLambda, null, immutableArray[j].StateForLambda);
					}
				}
			}
			BoundCall boundCall = node as BoundCall;
			if (boundCall != null)
			{
				MethodSymbol method2 = boundCall.Method;
				if (method2 != null)
				{
					LocalFunctionSymbol localFunctionSymbol = method2.OriginalDefinition as LocalFunctionSymbol;
					if (localFunctionSymbol != null)
					{
						this.VisitLocalFunctionUse(localFunctionSymbol);
					}
				}
			}
			if (!node.HasErrors && !parametersOpt.IsDefault)
			{
				NullableWalker.CompareExchangeInfo compareExchangeInfo = (this.IsCompareExchangeMethod(method) ? new NullableWalker.CompareExchangeInfo(arguments, immutableArray, argsToParamsOpt) : default(NullableWalker.CompareExchangeInfo));
				for (int k = 0; k < arguments.Length; k++)
				{
					ValueTuple<ParameterSymbol, TypeWithAnnotations, FlowAnalysisAnnotations, bool> correspondingParameter2 = this.GetCorrespondingParameter(k, parametersOpt, argsToParamsOpt, expanded);
					ParameterSymbol item7 = correspondingParameter2.Item1;
					TypeWithAnnotations item8 = correspondingParameter2.Item2;
					FlowAnalysisAnnotations item9 = correspondingParameter2.Item3;
					if (item7 != null)
					{
						this.VisitArgumentOutboundAssignmentsAndPostConditions(arguments[k], AbstractFlowPass<NullableWalker.LocalState, NullableWalker.LocalFunctionState>.GetRefKind(refKindsOpt, k), item7, item8, item9, immutableArray[k], arrayBuilder, (!compareExchangeInfo.IsDefault && item7.Ordinal == 0) ? compareExchangeInfo : default(NullableWalker.CompareExchangeInfo));
					}
				}
			}
			else
			{
				for (int l = 0; l < arguments.Length; l++)
				{
					BoundExpression boundExpression3 = arguments[l];
					NullableWalker.VisitArgumentResult visitArgumentResult = immutableArray[l];
					BoundExpression boundExpression4 = item[l];
					this.TrackAnalyzedNullabilityThroughConversionGroup(TypeWithState.Create(boundExpression3.Type, visitArgumentResult.RValueType.State), boundExpression3 as BoundConversion, boundExpression4);
				}
			}
			if (!this.IsAnalyzingAttribute && method != null && (method.FlowAnalysisAnnotations & FlowAnalysisAnnotations.DoesNotReturn) == FlowAnalysisAnnotations.DoesNotReturn)
			{
				base.SetUnreachable();
			}
			if (arrayBuilder != null)
			{
				arrayBuilder.Free();
			}
			return new ValueTuple<MethodSymbol, ImmutableArray<NullableWalker.VisitArgumentResult>, bool>(method, immutableArray, flag);
		}

		// Token: 0x06001C59 RID: 7257 RVA: 0x00096160 File Offset: 0x00094360
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private void ApplyMemberPostConditions(BoundExpression receiverOpt, MethodSymbol method)
		{
			if (method == null)
			{
				return;
			}
			int num = (method.IsStatic ? 0 : ((receiverOpt == null) ? (-1) : this.MakeSlot(receiverOpt)));
			if (num < 0)
			{
				return;
			}
			this.ApplyMemberPostConditions(num, method);
		}

		// Token: 0x06001C5A RID: 7258 RVA: 0x00096198 File Offset: 0x00094398
		private void ApplyMemberPostConditions(int receiverSlot, MethodSymbol method)
		{
			do
			{
				NamedTypeSymbol containingType = method.ContainingType;
				ImmutableArray<string> notNullMembers = method.NotNullMembers;
				ImmutableArray<string> notNullWhenTrueMembers = method.NotNullWhenTrueMembers;
				ImmutableArray<string> notNullWhenFalseMembers = method.NotNullWhenFalseMembers;
				if (this.IsConditionalState)
				{
					this.<ApplyMemberPostConditions>g__applyMemberPostConditions|198_0(receiverSlot, containingType, notNullMembers, ref this.StateWhenTrue);
					this.<ApplyMemberPostConditions>g__applyMemberPostConditions|198_0(receiverSlot, containingType, notNullMembers, ref this.StateWhenFalse);
				}
				else
				{
					this.<ApplyMemberPostConditions>g__applyMemberPostConditions|198_0(receiverSlot, containingType, notNullMembers, ref this.State);
				}
				if (!notNullWhenTrueMembers.IsEmpty || !notNullWhenFalseMembers.IsEmpty)
				{
					base.Split();
					this.<ApplyMemberPostConditions>g__applyMemberPostConditions|198_0(receiverSlot, containingType, notNullWhenTrueMembers, ref this.StateWhenTrue);
					this.<ApplyMemberPostConditions>g__applyMemberPostConditions|198_0(receiverSlot, containingType, notNullWhenFalseMembers, ref this.StateWhenFalse);
				}
				method = method.OverriddenMethod;
			}
			while (method != null);
		}

		// Token: 0x06001C5B RID: 7259 RVA: 0x00096244 File Offset: 0x00094444
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private ImmutableArray<NullableWalker.VisitArgumentResult> VisitArgumentsEvaluate([global::System.Runtime.CompilerServices.Nullable(1)] SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments, ImmutableArray<RefKind> refKindsOpt, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<ParameterSymbol> parametersOpt, ImmutableArray<int> argsToParamsOpt, BitVector defaultArguments, bool expanded)
		{
			int length = arguments.Length;
			if (length == 0 && parametersOpt.IsDefaultOrEmpty)
			{
				return ImmutableArray<NullableWalker.VisitArgumentResult>.Empty;
			}
			PooledHashSet<ParameterSymbol> instance = PooledHashSet<ParameterSymbol>.GetInstance();
			ArrayBuilder<NullableWalker.VisitArgumentResult> instance2 = ArrayBuilder<NullableWalker.VisitArgumentResult>.GetInstance(length);
			bool disableDiagnostics = this._disableDiagnostics;
			for (int i = 0; i < length; i++)
			{
				ValueTuple<ParameterSymbol, TypeWithAnnotations, FlowAnalysisAnnotations, bool> correspondingParameter = this.GetCorrespondingParameter(i, parametersOpt, argsToParamsOpt, expanded);
				ParameterSymbol item = correspondingParameter.Item1;
				FlowAnalysisAnnotations item2 = correspondingParameter.Item3;
				this._disableDiagnostics = defaultArguments[i] || disableDiagnostics;
				instance2.Add(this.VisitArgumentEvaluate(arguments[i], AbstractFlowPass<NullableWalker.LocalState, NullableWalker.LocalFunctionState>.GetRefKind(refKindsOpt, i), item2));
				instance.Add(item);
			}
			this._disableDiagnostics = disableDiagnostics;
			this.SetInvalidResult();
			instance.Free();
			return instance2.ToImmutableAndFree();
		}

		// Token: 0x06001C5C RID: 7260 RVA: 0x00096300 File Offset: 0x00094500
		private NullableWalker.VisitArgumentResult VisitArgumentEvaluate(BoundExpression argument, RefKind refKind, FlowAnalysisAnnotations annotations)
		{
			Optional<NullableWalker.LocalState> optional = ((argument.Kind == BoundKind.Lambda) ? this.State.Clone() : default(Optional<NullableWalker.LocalState>));
			switch (refKind)
			{
			case RefKind.None:
			case RefKind.In:
			{
				FlowAnalysisAnnotations flowAnalysisAnnotations = annotations & FlowAnalysisAnnotations.DoesNotReturn;
				if (flowAnalysisAnnotations != FlowAnalysisAnnotations.DoesNotReturnIfFalse)
				{
					if (flowAnalysisAnnotations == FlowAnalysisAnnotations.DoesNotReturnIfTrue)
					{
						this.Visit(argument);
						if (this.IsConditionalState)
						{
							base.SetState(this.StateWhenFalse);
						}
					}
					else
					{
						this.VisitRvalue(argument, false);
					}
				}
				else
				{
					this.Visit(argument);
					if (this.IsConditionalState)
					{
						base.SetState(this.StateWhenTrue);
					}
				}
				break;
			}
			case RefKind.Ref:
				this.Visit(argument);
				base.Unsplit();
				break;
			case RefKind.Out:
				this.Visit(argument);
				this.UseLvalueOnly(argument);
				break;
			}
			return new NullableWalker.VisitArgumentResult(this._visitResult, optional);
		}

		// Token: 0x06001C5D RID: 7261 RVA: 0x000963D4 File Offset: 0x000945D4
		private void VisitArgumentConversionAndInboundAssignmentsAndPreConditions([global::System.Runtime.CompilerServices.Nullable(2)] BoundConversion conversionOpt, BoundExpression argumentNoConversion, Conversion conversion, RefKind refKind, ParameterSymbol parameter, TypeWithAnnotations parameterType, FlowAnalysisAnnotations parameterAnnotations, NullableWalker.VisitArgumentResult result, bool extensionMethodThisArgument)
		{
			TypeWithState rvalueType = result.RValueType;
			switch (refKind)
			{
			case RefKind.None:
			case RefKind.In:
			{
				if (conversion.Kind == ConversionKind.ImplicitUserDefined)
				{
					TypeSymbol type = rvalueType.Type;
					conversion = NullableWalker.GenerateConversion(this._conversions, argumentNoConversion, type, parameterType.Type, false, false);
					if (!conversion.Exists && !argumentNoConversion.IsSuppressed)
					{
						this.ReportNullabilityMismatchInArgument(argumentNoConversion.Syntax, type, parameter, parameterType.Type, false);
					}
				}
				TypeWithState typeWithState = this.VisitConversion(conversionOpt, argumentNoConversion, conversion, NullableWalker.ApplyLValueAnnotations(parameterType, parameterAnnotations), rvalueType, true, false, false, NullableWalker.AssignmentKind.Argument, parameter, true, true, extensionMethodThisArgument, result.StateForLambda, false, null);
				if (this.CheckDisallowedNullAssignment(typeWithState, parameterAnnotations, argumentNoConversion.Syntax.Location, null))
				{
					this.LearnFromNonNullTest(argumentNoConversion, ref this.State);
				}
				this.SetResultType(argumentNoConversion, typeWithState, false);
				return;
			}
			case RefKind.Ref:
				if (!argumentNoConversion.IsSuppressed)
				{
					TypeWithAnnotations lvalueType = result.LValueType;
					if (NullableWalker.IsNullabilityMismatch(lvalueType.Type, parameterType.Type))
					{
						this.ReportNullabilityMismatchInRefArgument(argumentNoConversion, lvalueType.Type, parameter, parameterType.Type);
						return;
					}
					this.ReportNullableAssignmentIfNecessary(argumentNoConversion, NullableWalker.ApplyLValueAnnotations(parameterType, parameterAnnotations), rvalueType, false, NullableWalker.AssignmentKind.Assignment, null, null);
					this.CheckDisallowedNullAssignment(rvalueType, parameterAnnotations, argumentNoConversion.Syntax.Location, null);
					return;
				}
				break;
			case RefKind.Out:
				break;
			default:
				throw ExceptionUtilities.UnexpectedValue(refKind);
			}
		}

		// Token: 0x06001C5E RID: 7262 RVA: 0x00096522 File Offset: 0x00094722
		private bool CheckDisallowedNullAssignment(TypeWithState state, FlowAnalysisAnnotations annotations, Location location, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression boundValueOpt = null)
		{
			if (boundValueOpt != null && boundValueOpt.WasCompilerGenerated)
			{
				return false;
			}
			if (NullableWalker.IsDisallowedNullAssignment(state, annotations))
			{
				this.ReportDiagnostic(ErrorCode.WRN_DisallowNullAttributeForbidsMaybeNullAssignment, location, Array.Empty<object>());
				return true;
			}
			return false;
		}

		// Token: 0x06001C5F RID: 7263 RVA: 0x00096550 File Offset: 0x00094750
		private static bool IsDisallowedNullAssignment(TypeWithState valueState, FlowAnalysisAnnotations targetAnnotations)
		{
			return (targetAnnotations & FlowAnalysisAnnotations.DisallowNull) != FlowAnalysisAnnotations.None && NullableWalker.<IsDisallowedNullAssignment>g__hasNoNonNullableCounterpart|203_0(valueState.Type) && valueState.MayBeNull;
		}

		// Token: 0x06001C60 RID: 7264 RVA: 0x00096570 File Offset: 0x00094770
		private void VisitArgumentOutboundAssignmentsAndPostConditions(BoundExpression argument, RefKind refKind, ParameterSymbol parameter, TypeWithAnnotations parameterType, FlowAnalysisAnnotations parameterAnnotations, NullableWalker.VisitArgumentResult result, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ArrayBuilder<ParameterSymbol> notNullParametersOpt, NullableWalker.CompareExchangeInfo compareExchangeInfoOpt)
		{
			switch (refKind)
			{
			case RefKind.None:
			case RefKind.In:
				this.<VisitArgumentOutboundAssignmentsAndPostConditions>g__learnFromPostConditions|204_6(argument, parameterType, parameterAnnotations);
				return;
			case RefKind.Ref:
			{
				parameterAnnotations = this.<VisitArgumentOutboundAssignmentsAndPostConditions>g__notNullBasedOnParameters|204_0(parameterAnnotations, notNullParametersOpt, parameter);
				TypeWithState typeWithState = TypeWithState.Create(parameterType, parameterAnnotations);
				if (!compareExchangeInfoOpt.IsDefault)
				{
					NullableFlowState nullableFlowState = this.LearnFromCompareExchangeMethod(in compareExchangeInfoOpt);
					typeWithState = TypeWithState.Create(parameterType.Type, nullableFlowState);
				}
				BoundParameter boundParameter = new BoundParameter(argument.Syntax, parameter);
				TypeWithAnnotations lvalueType = result.LValueType;
				this.<VisitArgumentOutboundAssignmentsAndPostConditions>g__trackNullableStateForAssignment|204_1(boundParameter, lvalueType, this.MakeSlot(argument), typeWithState, argument.IsSuppressed, parameterAnnotations);
				if (!argument.IsSuppressed)
				{
					FlowAnalysisAnnotations lvalueAnnotations = this.GetLValueAnnotations(argument);
					this.ReportNullableAssignmentIfNecessary(boundParameter, NullableWalker.ApplyLValueAnnotations(lvalueType, lvalueAnnotations), NullableWalker.<VisitArgumentOutboundAssignmentsAndPostConditions>g__applyPostConditionsUnconditionally|204_3(typeWithState, parameterAnnotations), NullableWalker.UseLegacyWarnings(argument, result.LValueType), NullableWalker.AssignmentKind.Assignment, null, null);
					return;
				}
				break;
			}
			case RefKind.Out:
			{
				parameterAnnotations = this.<VisitArgumentOutboundAssignmentsAndPostConditions>g__notNullBasedOnParameters|204_0(parameterAnnotations, notNullParametersOpt, parameter);
				TypeWithState typeWithState2 = TypeWithState.Create(parameterType, parameterAnnotations);
				TypeWithState typeWithState3 = NullableWalker.<VisitArgumentOutboundAssignmentsAndPostConditions>g__applyPostConditionsUnconditionally|204_3(typeWithState2, parameterAnnotations);
				TypeWithAnnotations lvalueType2 = result.LValueType;
				FlowAnalysisAnnotations lvalueAnnotations2 = this.GetLValueAnnotations(argument);
				TypeWithAnnotations typeWithAnnotations = NullableWalker.ApplyLValueAnnotations(lvalueType2, lvalueAnnotations2);
				BoundLocal boundLocal = argument as BoundLocal;
				if (boundLocal != null && boundLocal.DeclarationKind == BoundLocalDeclarationKind.WithInferredType)
				{
					TypeWithAnnotations typeWithAnnotations2 = typeWithState3.ToAnnotatedTypeWithAnnotations(this.compilation);
					this._variables.SetType(boundLocal.LocalSymbol, typeWithAnnotations2);
					typeWithAnnotations = typeWithAnnotations2;
				}
				else
				{
					BoundDiscardExpression boundDiscardExpression = argument as BoundDiscardExpression;
					if (boundDiscardExpression != null)
					{
						this.SetAnalyzedNullability(boundDiscardExpression, new NullableWalker.VisitResult(typeWithState2, typeWithState2.ToTypeWithAnnotations(this.compilation, false)), new bool?(true));
					}
				}
				BoundParameter boundParameter2 = new BoundParameter(argument.Syntax, parameter);
				this.CheckDisallowedNullAssignment(typeWithState2, lvalueAnnotations2, argument.Syntax.Location, null);
				this.AdjustSetValue(argument, ref typeWithState2);
				this.<VisitArgumentOutboundAssignmentsAndPostConditions>g__trackNullableStateForAssignment|204_1(boundParameter2, typeWithAnnotations, this.MakeSlot(argument), typeWithState2, argument.IsSuppressed, parameterAnnotations);
				if (!argument.IsSuppressed)
				{
					this.ReportNullableAssignmentIfNecessary(boundParameter2, typeWithAnnotations, typeWithState3, NullableWalker.UseLegacyWarnings(argument, result.LValueType), NullableWalker.AssignmentKind.Assignment, null, null);
					CompoundUseSiteInfo<AssemblySymbol> discarded = CompoundUseSiteInfo<AssemblySymbol>.Discarded;
					if (!this._conversions.HasIdentityOrImplicitReferenceConversion(parameterType.Type, typeWithAnnotations.Type, ref discarded))
					{
						this.ReportNullabilityMismatchInArgument(argument.Syntax, typeWithAnnotations.Type, parameter, parameterType.Type, true);
						return;
					}
				}
				break;
			}
			default:
				throw ExceptionUtilities.UnexpectedValue(refKind);
			}
		}

		// Token: 0x06001C61 RID: 7265 RVA: 0x000967A8 File Offset: 0x000949A8
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[return: TupleElementNames(new string[] { "arguments", "conversions" })]
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 0, 1, 0 })]
		private ValueTuple<ImmutableArray<BoundExpression>, ImmutableArray<Conversion>> RemoveArgumentConversions([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments, ImmutableArray<RefKind> refKindsOpt)
		{
			int length = arguments.Length;
			ImmutableArray<Conversion> immutableArray = default(ImmutableArray<Conversion>);
			if (length > 0)
			{
				ArrayBuilder<BoundExpression> instance = ArrayBuilder<BoundExpression>.GetInstance(length);
				ArrayBuilder<Conversion> instance2 = ArrayBuilder<Conversion>.GetInstance(length);
				bool flag = false;
				for (int i = 0; i < length; i++)
				{
					bool refKind = AbstractFlowPass<NullableWalker.LocalState, NullableWalker.LocalFunctionState>.GetRefKind(refKindsOpt, i) != RefKind.None;
					BoundExpression boundExpression = arguments[i];
					Conversion conversion = Conversion.Identity;
					if (!refKind)
					{
						BoundExpression boundExpression2 = boundExpression;
						ValueTuple<BoundExpression, Conversion> valueTuple = NullableWalker.RemoveConversion(boundExpression, false);
						boundExpression = valueTuple.Item1;
						conversion = valueTuple.Item2;
						if (boundExpression != boundExpression2)
						{
							this.SnapshotWalkerThroughConversionGroup(boundExpression2, boundExpression);
							flag = true;
						}
					}
					instance.Add(boundExpression);
					instance2.Add(conversion);
				}
				if (flag)
				{
					arguments = instance.ToImmutable();
					immutableArray = instance2.ToImmutable();
				}
				instance.Free();
				instance2.Free();
			}
			return new ValueTuple<ImmutableArray<BoundExpression>, ImmutableArray<Conversion>>(arguments, immutableArray);
		}

		// Token: 0x06001C62 RID: 7266 RVA: 0x0009686D File Offset: 0x00094A6D
		private static NullableWalker.VariableState GetVariableState(NullableWalker.Variables variables, NullableWalker.LocalState localState)
		{
			return new NullableWalker.VariableState(variables.CreateSnapshot(), localState.CreateSnapshot());
		}

		// Token: 0x06001C63 RID: 7267 RVA: 0x00096884 File Offset: 0x00094A84
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[return: TupleElementNames(new string[] { "Parameter", "Type", "Annotations", "isExpandedParamsArgument" })]
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
		private ValueTuple<ParameterSymbol, TypeWithAnnotations, FlowAnalysisAnnotations, bool> GetCorrespondingParameter(int argumentOrdinal, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<ParameterSymbol> parametersOpt, ImmutableArray<int> argsToParamsOpt, bool expanded)
		{
			if (parametersOpt.IsDefault)
			{
				return default(ValueTuple<ParameterSymbol, TypeWithAnnotations, FlowAnalysisAnnotations, bool>);
			}
			ParameterSymbol correspondingParameter = Binder.GetCorrespondingParameter(argumentOrdinal, parametersOpt, argsToParamsOpt, expanded);
			if (correspondingParameter == null)
			{
				return default(ValueTuple<ParameterSymbol, TypeWithAnnotations, FlowAnalysisAnnotations, bool>);
			}
			TypeWithAnnotations typeWithAnnotations = correspondingParameter.TypeWithAnnotations;
			if (expanded && correspondingParameter.Ordinal == parametersOpt.Length - 1 && typeWithAnnotations.IsSZArray())
			{
				typeWithAnnotations = ((ArrayTypeSymbol)typeWithAnnotations.Type).ElementTypeWithAnnotations;
				return new ValueTuple<ParameterSymbol, TypeWithAnnotations, FlowAnalysisAnnotations, bool>(correspondingParameter, typeWithAnnotations, FlowAnalysisAnnotations.None, true);
			}
			return new ValueTuple<ParameterSymbol, TypeWithAnnotations, FlowAnalysisAnnotations, bool>(correspondingParameter, typeWithAnnotations, this.GetParameterAnnotations(correspondingParameter), false);
		}

		// Token: 0x06001C64 RID: 7268 RVA: 0x0009690C File Offset: 0x00094B0C
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		private MethodSymbol InferMethodTypeArguments([global::System.Runtime.CompilerServices.Nullable(1)] MethodSymbol method, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments, ImmutableArray<RefKind> argumentRefKindsOpt, ImmutableArray<int> argsToParamsOpt, bool expanded)
		{
			MethodSymbol constructedFrom = method.ConstructedFrom;
			ArrayBuilder<RefKind> instance = ArrayBuilder<RefKind>.GetInstance();
			if (new ImmutableArray<RefKind>?(argumentRefKindsOpt) != default(ImmutableArray<RefKind>?))
			{
				instance.AddRange(argumentRefKindsOpt);
			}
			ImmutableArray<TypeWithAnnotations> immutableArray;
			ImmutableArray<RefKind> immutableArray2;
			OverloadResolution.GetEffectiveParameterTypes(constructedFrom, arguments.Length, argsToParamsOpt, instance, false, true, this._binder, expanded, out immutableArray, out immutableArray2);
			instance.Free();
			CompoundUseSiteInfo<AssemblySymbol> discarded = CompoundUseSiteInfo<AssemblySymbol>.Discarded;
			MethodTypeInferenceResult methodTypeInferenceResult = MethodTypeInferrer.Infer(this._binder, this._conversions, constructedFrom.TypeParameters, constructedFrom.ContainingType, immutableArray, immutableArray2, arguments, ref discarded, new NullableWalker.MethodInferenceExtensions(this));
			if (!methodTypeInferenceResult.Success)
			{
				return method;
			}
			return constructedFrom.Construct(methodTypeInferenceResult.InferredTypeArguments);
		}

		// Token: 0x06001C65 RID: 7269 RVA: 0x000969B4 File Offset: 0x00094BB4
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<BoundExpression> GetArgumentsForMethodTypeInference(ImmutableArray<NullableWalker.VisitArgumentResult> argumentResults, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments)
		{
			int length = argumentResults.Length;
			ArrayBuilder<BoundExpression> instance = ArrayBuilder<BoundExpression>.GetInstance(length);
			for (int i = 0; i < length; i++)
			{
				NullableWalker.VisitArgumentResult visitArgumentResult = argumentResults[i];
				Optional<NullableWalker.LocalState> stateForLambda = visitArgumentResult.StateForLambda;
				TypeWithAnnotations typeWithAnnotations = visitArgumentResult.RValueType.ToTypeWithAnnotations(this.compilation, false);
				instance.Add(this.<GetArgumentsForMethodTypeInference>g__getArgumentForMethodTypeInference|210_0(arguments[i], typeWithAnnotations, stateForLambda));
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06001C66 RID: 7270 RVA: 0x00096A24 File Offset: 0x00094C24
		private void CheckMethodConstraints(SyntaxNode syntax, MethodSymbol method)
		{
			if (this._disableDiagnostics)
			{
				return;
			}
			ArrayBuilder<TypeParameterDiagnosticInfo> instance = ArrayBuilder<TypeParameterDiagnosticInfo>.GetInstance();
			ArrayBuilder<TypeParameterDiagnosticInfo> instance2 = ArrayBuilder<TypeParameterDiagnosticInfo>.GetInstance();
			ArrayBuilder<TypeParameterDiagnosticInfo> arrayBuilder = null;
			ConstraintsHelper.CheckConstraintsArgs checkConstraintsArgs = new ConstraintsHelper.CheckConstraintsArgs(this.compilation, this._conversions, true, NoLocation.Singleton, null, CompoundUseSiteInfo<AssemblySymbol>.Discarded);
			ConstraintsHelper.CheckMethodConstraints(method, in checkConstraintsArgs, instance, instance2, ref arrayBuilder, default(BitVector));
			foreach (TypeParameterDiagnosticInfo typeParameterDiagnosticInfo in instance2)
			{
				if (typeParameterDiagnosticInfo.UseSiteInfo.DiagnosticInfo != null)
				{
					base.Diagnostics.Add(typeParameterDiagnosticInfo.UseSiteInfo.DiagnosticInfo, syntax.Location);
				}
			}
			if (arrayBuilder != null)
			{
				arrayBuilder.Free();
			}
			instance2.Free();
			instance.Free();
		}

		// Token: 0x06001C67 RID: 7271 RVA: 0x00096ADC File Offset: 0x00094CDC
		[return: TupleElementNames(new string[] { "expression", "conversion" })]
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private static ValueTuple<BoundExpression, Conversion> RemoveConversion(BoundExpression expr, bool includeExplicitConversions)
		{
			ConversionGroup conversionGroup = null;
			while (expr.Kind == BoundKind.Conversion)
			{
				BoundConversion boundConversion = (BoundConversion)expr;
				if (conversionGroup != boundConversion.ConversionGroupOpt && conversionGroup != null)
				{
					break;
				}
				conversionGroup = boundConversion.ConversionGroupOpt;
				if (!includeExplicitConversions && conversionGroup != null && conversionGroup.IsExplicitConversion)
				{
					return new ValueTuple<BoundExpression, Conversion>(expr, Conversion.Identity);
				}
				expr = boundConversion.Operand;
				if (conversionGroup == null)
				{
					return new ValueTuple<BoundExpression, Conversion>(expr, boundConversion.Conversion);
				}
			}
			return new ValueTuple<BoundExpression, Conversion>(expr, (conversionGroup != null) ? conversionGroup.Conversion : Conversion.Identity);
		}

		// Token: 0x06001C68 RID: 7272 RVA: 0x00096B58 File Offset: 0x00094D58
		private Conversion GenerateConversionForConditionalOperator(BoundExpression sourceExpression, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol sourceType, TypeSymbol destinationType, bool reportMismatch)
		{
			Conversion conversion = NullableWalker.GenerateConversion(this._conversions, sourceExpression, sourceType, destinationType, false, false);
			if (!conversion.Exists && reportMismatch && !sourceExpression.IsSuppressed)
			{
				this.ReportNullabilityMismatchInAssignment(sourceExpression.Syntax, NullableWalker.GetTypeAsDiagnosticArgument(sourceType), destinationType);
			}
			return conversion;
		}

		// Token: 0x06001C69 RID: 7273 RVA: 0x00096BA4 File Offset: 0x00094DA4
		private static Conversion GenerateConversion(Conversions conversions, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression sourceExpression, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol sourceType, TypeSymbol destinationType, bool fromExplicitCast, bool extensionMethodThisArgument)
		{
			CompoundUseSiteInfo<AssemblySymbol> discarded = CompoundUseSiteInfo<AssemblySymbol>.Discarded;
			bool flag = sourceType == null || NullableWalker.UseExpressionForConversion(sourceExpression);
			if (extensionMethodThisArgument)
			{
				return conversions.ClassifyImplicitExtensionMethodThisArgConversion(flag ? sourceExpression : null, sourceType, destinationType, ref discarded);
			}
			if (!flag)
			{
				if (!fromExplicitCast)
				{
					return conversions.ClassifyImplicitConversionFromType(sourceType, destinationType, ref discarded);
				}
				return conversions.ClassifyConversionFromType(sourceType, destinationType, ref discarded, true);
			}
			else
			{
				if (!fromExplicitCast)
				{
					return conversions.ClassifyImplicitConversionFromExpression(sourceExpression, destinationType, ref discarded);
				}
				return conversions.ClassifyConversionFromExpression(sourceExpression, destinationType, ref discarded, true);
			}
		}

		// Token: 0x06001C6A RID: 7274 RVA: 0x00096C12 File Offset: 0x00094E12
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private static bool UseExpressionForConversion([NotNullWhen(true)] BoundExpression value)
		{
			return value != null && (value.Type == null || value.Type.IsDynamic() || value.ConstantValue != null || value.Kind == BoundKind.InterpolatedString);
		}

		// Token: 0x06001C6B RID: 7275 RVA: 0x00096C50 File Offset: 0x00094E50
		private TypeWithState GetAdjustedResult(TypeWithState type, int slot)
		{
			if (this.State.HasValue(slot))
			{
				NullableFlowState nullableFlowState = this.State[slot];
				return TypeWithState.Create(type.Type, nullableFlowState);
			}
			return type;
		}

		// Token: 0x06001C6C RID: 7276 RVA: 0x00096C88 File Offset: 0x00094E88
		private static Symbol AsMemberOfType([global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol type, Symbol symbol)
		{
			NullableWalker.<>c__DisplayClass217_0 CS$<>8__locals1;
			CS$<>8__locals1.symbol = symbol;
			NamedTypeSymbol namedTypeSymbol = type as NamedTypeSymbol;
			if (namedTypeSymbol == null || namedTypeSymbol.IsErrorType() || CS$<>8__locals1.symbol is ErrorMethodSymbol)
			{
				return CS$<>8__locals1.symbol;
			}
			if (CS$<>8__locals1.symbol.Kind == SymbolKind.Method && ((MethodSymbol)CS$<>8__locals1.symbol).MethodKind == MethodKind.LocalFunction)
			{
				return CS$<>8__locals1.symbol;
			}
			if (CS$<>8__locals1.symbol is TupleElementFieldSymbol || CS$<>8__locals1.symbol is TupleErrorFieldSymbol)
			{
				return CS$<>8__locals1.symbol.SymbolAsMember(namedTypeSymbol);
			}
			CS$<>8__locals1.symbolContainer = CS$<>8__locals1.symbol.ContainingType;
			if (CS$<>8__locals1.symbolContainer.IsAnonymousType)
			{
				int? num = ((CS$<>8__locals1.symbol.Kind == SymbolKind.Property) ? CS$<>8__locals1.symbol.MemberIndexOpt : default(int?));
				if (num == null)
				{
					return CS$<>8__locals1.symbol;
				}
				return AnonymousTypeManager.GetAnonymousTypeProperty(namedTypeSymbol, num.GetValueOrDefault());
			}
			else
			{
				if (!CS$<>8__locals1.symbolContainer.IsGenericType)
				{
					return CS$<>8__locals1.symbol;
				}
				if (!namedTypeSymbol.IsGenericType)
				{
					return CS$<>8__locals1.symbol;
				}
				if (!CS$<>8__locals1.symbolContainer.IsInterface)
				{
					Symbol symbol2;
					while (!NullableWalker.<AsMemberOfType>g__tryAsMemberOfSingleType|217_0(namedTypeSymbol, out symbol2, ref CS$<>8__locals1))
					{
						namedTypeSymbol = namedTypeSymbol.BaseTypeNoUseSiteDiagnostics;
						if (namedTypeSymbol == null)
						{
							goto IL_016D;
						}
					}
					return symbol2;
				}
				Symbol symbol3;
				if (NullableWalker.<AsMemberOfType>g__tryAsMemberOfSingleType|217_0(namedTypeSymbol, out symbol3, ref CS$<>8__locals1))
				{
					return symbol3;
				}
				ImmutableArray<NamedTypeSymbol>.Enumerator enumerator = namedTypeSymbol.AllInterfacesNoUseSiteDiagnostics.GetEnumerator();
				while (enumerator.MoveNext())
				{
					if (NullableWalker.<AsMemberOfType>g__tryAsMemberOfSingleType|217_0(enumerator.Current, out symbol3, ref CS$<>8__locals1))
					{
						return symbol3;
					}
				}
				IL_016D:
				return CS$<>8__locals1.symbol;
			}
		}

		// Token: 0x06001C6D RID: 7277 RVA: 0x00096E08 File Offset: 0x00095008
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitConversion(BoundConversion node)
		{
			ConversionGroup conversionGroupOpt = node.ConversionGroupOpt;
			TypeWithAnnotations typeWithAnnotations = ((conversionGroupOpt != null) ? conversionGroupOpt.ExplicitType : default(TypeWithAnnotations));
			bool hasType = typeWithAnnotations.HasType;
			TypeWithAnnotations typeWithAnnotations2 = (hasType ? typeWithAnnotations : TypeWithAnnotations.Create(node.Type, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)));
			ValueTuple<BoundExpression, Conversion> valueTuple = NullableWalker.RemoveConversion(node, true);
			BoundExpression item = valueTuple.Item1;
			Conversion item2 = valueTuple.Item2;
			this.SnapshotWalkerThroughConversionGroup(node, item);
			TypeWithState typeWithState = this.VisitRvalueWithState(item);
			this.SetResultType(node, this.VisitConversion(node, item, item2, typeWithAnnotations2, typeWithState, true, hasType, hasType, NullableWalker.AssignmentKind.Assignment, null, hasType, true, false, default(Optional<NullableWalker.LocalState>), true, null), true);
			return null;
		}

		// Token: 0x06001C6E RID: 7278 RVA: 0x00096EA8 File Offset: 0x000950A8
		private TypeWithState VisitOptionalImplicitConversion(BoundExpression expr, TypeWithAnnotations targetTypeOpt, bool useLegacyWarnings, bool trackMembers, NullableWalker.AssignmentKind assignmentKind)
		{
			if (!targetTypeOpt.HasType)
			{
				return this.VisitRvalueWithState(expr);
			}
			ValueTuple<BoundExpression, Conversion> valueTuple = NullableWalker.RemoveConversion(expr, false);
			BoundExpression item = valueTuple.Item1;
			Conversion item2 = valueTuple.Item2;
			this.SnapshotWalkerThroughConversionGroup(expr, item);
			TypeWithState typeWithState = this.VisitRvalueWithState(item);
			bool flag = !item2.IsExplicit;
			return this.VisitConversion(NullableWalker.GetConversionIfApplicable(expr, item), item, item2, targetTypeOpt, typeWithState, true, false, useLegacyWarnings, assignmentKind, null, true, flag, false, default(Optional<NullableWalker.LocalState>), trackMembers, null);
		}

		// Token: 0x06001C6F RID: 7279 RVA: 0x00096F20 File Offset: 0x00095120
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private static bool AreNullableAndUnderlyingTypes([NotNullWhen(true)] TypeSymbol nullableTypeOpt, [NotNullWhen(true)] TypeSymbol underlyingTypeOpt, out TypeWithAnnotations underlyingTypeWithAnnotations)
		{
			if (nullableTypeOpt != null && nullableTypeOpt.IsNullableType() && underlyingTypeOpt != null && !underlyingTypeOpt.IsNullableType())
			{
				TypeWithAnnotations nullableUnderlyingTypeWithAnnotations = nullableTypeOpt.GetNullableUnderlyingTypeWithAnnotations();
				if (nullableUnderlyingTypeWithAnnotations.Type.Equals(underlyingTypeOpt, TypeCompareKind.AllIgnoreOptions))
				{
					underlyingTypeWithAnnotations = nullableUnderlyingTypeWithAnnotations;
					return true;
				}
			}
			underlyingTypeWithAnnotations = default(TypeWithAnnotations);
			return false;
		}

		// Token: 0x06001C70 RID: 7280 RVA: 0x00096F6C File Offset: 0x0009516C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitTupleLiteral(BoundTupleLiteral node)
		{
			this.VisitTupleExpression(node);
			return null;
		}

		// Token: 0x06001C71 RID: 7281 RVA: 0x00096F78 File Offset: 0x00095178
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitConvertedTupleLiteral(BoundConvertedTupleLiteral node)
		{
			NullableWalker.LocalState localState = this.State.Clone();
			this.VisitWithoutDiagnostics(node.SourceTuple);
			base.SetState(localState);
			this.VisitTupleExpression(node);
			return null;
		}

		// Token: 0x06001C72 RID: 7282 RVA: 0x00096FAC File Offset: 0x000951AC
		private void VisitTupleExpression(BoundTupleExpression node)
		{
			ImmutableArray<BoundExpression> arguments = node.Arguments;
			ImmutableArray<TypeWithState> immutableArray = arguments.SelectAsArray((BoundExpression a, NullableWalker w) => w.VisitRvalueWithState(a), this);
			ImmutableArray<TypeWithAnnotations> immutableArray2 = immutableArray.SelectAsArray<TypeWithState, TypeWithAnnotations>((TypeWithState a) => a.ToTypeWithAnnotations(this.compilation, false));
			NamedTypeSymbol namedTypeSymbol = (NamedTypeSymbol)node.Type;
			if (namedTypeSymbol == null)
			{
				this.SetResultType(node, TypeWithState.Create(null, NullableFlowState.NotNull), true);
				return;
			}
			int orCreatePlaceholderSlot = this.GetOrCreatePlaceholderSlot(node);
			if (orCreatePlaceholderSlot > 0)
			{
				this.State[orCreatePlaceholderSlot] = NullableFlowState.NotNull;
				this.TrackNullableStateOfTupleElements(orCreatePlaceholderSlot, namedTypeSymbol, arguments, immutableArray, default(ImmutableArray<int>), false);
			}
			namedTypeSymbol = namedTypeSymbol.WithElementTypes(immutableArray2);
			if (!this._disableDiagnostics)
			{
				ImmutableArray<Location> immutableArray3 = namedTypeSymbol.TupleElements.SelectAsArray((FieldSymbol element, Location location) => element.Locations.FirstOrDefault<Location>() ?? location, node.Syntax.Location);
				NamedTypeSymbol namedTypeSymbol2 = namedTypeSymbol;
				ConstraintsHelper.CheckConstraintsArgs checkConstraintsArgs = new ConstraintsHelper.CheckConstraintsArgs(this.compilation, this._conversions, true, node.Syntax.Location, null);
				namedTypeSymbol2.CheckConstraints(in checkConstraintsArgs, node.Syntax, immutableArray3, new BindingDiagnosticBag(base.Diagnostics));
			}
			this.SetResultType(node, TypeWithState.Create(namedTypeSymbol, NullableFlowState.NotNull), true);
		}

		// Token: 0x06001C73 RID: 7283 RVA: 0x000970DC File Offset: 0x000952DC
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private void TrackNullableStateOfTupleElements(int slot, [global::System.Runtime.CompilerServices.Nullable(1)] NamedTypeSymbol tupleType, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> values, ImmutableArray<TypeWithState> types, ImmutableArray<int> argsToParamsOpt, bool useRestField)
		{
			NullableWalker.<>c__DisplayClass224_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.slot = slot;
			CS$<>8__locals1.argsToParamsOpt = argsToParamsOpt;
			if (CS$<>8__locals1.slot > 0)
			{
				ImmutableArray<FieldSymbol> tupleElements = tupleType.TupleElements;
				int num = values.Length;
				if (useRestField)
				{
					num = Math.Min(num, 7);
				}
				for (int i = 0; i < num; i++)
				{
					int num2 = this.<TrackNullableStateOfTupleElements>g__GetArgumentOrdinalFromParameterOrdinal|224_1(i, ref CS$<>8__locals1);
					this.<TrackNullableStateOfTupleElements>g__trackState|224_0(values[num2], tupleElements[i], types[num2], ref CS$<>8__locals1);
				}
				if (useRestField && values.Length == 8)
				{
					FieldSymbol fieldSymbol = tupleType.GetMembers("Rest").FirstOrDefault<Symbol>() as FieldSymbol;
					if (fieldSymbol != null)
					{
						int num3 = this.<TrackNullableStateOfTupleElements>g__GetArgumentOrdinalFromParameterOrdinal|224_1(7, ref CS$<>8__locals1);
						this.<TrackNullableStateOfTupleElements>g__trackState|224_0(values[num3], fieldSymbol, types[num3], ref CS$<>8__locals1);
					}
				}
			}
		}

		// Token: 0x06001C74 RID: 7284 RVA: 0x000971B8 File Offset: 0x000953B8
		private void TrackNullableStateOfNullableValue(int containingSlot, TypeSymbol containingType, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression value, TypeWithState valueType, int valueSlot)
		{
			Symbol symbol;
			int nullableOfTValueSlot = this.GetNullableOfTValueSlot(containingType, containingSlot, out symbol, false);
			if (nullableOfTValueSlot > 0)
			{
				this.TrackNullableStateForAssignment(value, symbol.GetTypeOrReturnType(), nullableOfTValueSlot, valueType, valueSlot);
			}
		}

		// Token: 0x06001C75 RID: 7285 RVA: 0x000971E8 File Offset: 0x000953E8
		private void TrackNullableStateOfTupleConversion([global::System.Runtime.CompilerServices.Nullable(2)] BoundConversion conversionOpt, BoundExpression convertedNode, Conversion conversion, TypeSymbol targetType, TypeSymbol operandType, int slot, int valueSlot, NullableWalker.AssignmentKind assignmentKind, [global::System.Runtime.CompilerServices.Nullable(2)] ParameterSymbol parameterOpt, bool reportWarnings)
		{
			NullableWalker.<>c__DisplayClass226_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.slot = slot;
			CS$<>8__locals1.valueSlot = valueSlot;
			CS$<>8__locals1.conversionOpt = conversionOpt;
			CS$<>8__locals1.convertedNode = convertedNode;
			CS$<>8__locals1.assignmentKind = assignmentKind;
			CS$<>8__locals1.parameterOpt = parameterOpt;
			CS$<>8__locals1.reportWarnings = reportWarnings;
			NamedTypeSymbol namedTypeSymbol = operandType as NamedTypeSymbol;
			if (namedTypeSymbol == null || !namedTypeSymbol.IsTupleType)
			{
				return;
			}
			ImmutableArray<Conversion> underlyingConversions = conversion.UnderlyingConversions;
			ImmutableArray<FieldSymbol> tupleElements = ((NamedTypeSymbol)targetType).TupleElements;
			ImmutableArray<FieldSymbol> tupleElements2 = namedTypeSymbol.TupleElements;
			int length = tupleElements2.Length;
			for (int i = 0; i < length; i++)
			{
				this.<TrackNullableStateOfTupleConversion>g__trackConvertedValue|226_0(tupleElements[i], underlyingConversions[i], tupleElements2[i], ref CS$<>8__locals1);
			}
		}

		// Token: 0x06001C76 RID: 7286 RVA: 0x000972A8 File Offset: 0x000954A8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitTupleBinaryOperator(BoundTupleBinaryOperator node)
		{
			base.VisitTupleBinaryOperator(node);
			this.SetNotNullResult(node);
			return null;
		}

		// Token: 0x06001C77 RID: 7287 RVA: 0x000972BA File Offset: 0x000954BA
		private void ReportNullabilityMismatchWithTargetDelegate(Location location, TypeSymbol targetType, MethodSymbol targetInvokeMethod, MethodSymbol sourceInvokeMethod, bool invokedAsExtensionMethod)
		{
			SourceMemberContainerTypeSymbol.CheckValidNullableMethodOverride<ValueTuple<TypeSymbol, Location>>(this.compilation, targetInvokeMethod, sourceInvokeMethod, new BindingDiagnosticBag(base.Diagnostics), new ReportMismatchInReturnType<ValueTuple<TypeSymbol, Location>>(this.<ReportNullabilityMismatchWithTargetDelegate>g__reportBadDelegateReturn|228_0), new ReportMismatchInParameterType<ValueTuple<TypeSymbol, Location>>(this.<ReportNullabilityMismatchWithTargetDelegate>g__reportBadDelegateParameter|228_1), new ValueTuple<TypeSymbol, Location>(targetType, location), invokedAsExtensionMethod);
		}

		// Token: 0x06001C78 RID: 7288 RVA: 0x000972F8 File Offset: 0x000954F8
		private void ReportNullabilityMismatchWithTargetDelegate(Location location, NamedTypeSymbol delegateType, UnboundLambda unboundLambda)
		{
			if (!unboundLambda.HasExplicitlyTypedParameterList)
			{
				return;
			}
			MethodSymbol methodSymbol = ((delegateType != null) ? delegateType.DelegateInvokeMethod : null);
			if (methodSymbol == null)
			{
				return;
			}
			int num = Math.Min(methodSymbol.ParameterCount, unboundLambda.ParameterCount);
			for (int i = 0; i < num; i++)
			{
				ParameterSymbol parameterSymbol = methodSymbol.Parameters[i];
				if (this.IsNullabilityMismatch(parameterSymbol.TypeWithAnnotations, unboundLambda.ParameterTypeWithAnnotations(i), true))
				{
					this.ReportDiagnostic(ErrorCode.WRN_NullabilityMismatchInParameterTypeOfTargetDelegate, location, new object[]
					{
						unboundLambda.ParameterName(i),
						unboundLambda.MessageID.Localize(),
						delegateType
					});
				}
			}
		}

		// Token: 0x06001C79 RID: 7289 RVA: 0x00097398 File Offset: 0x00095598
		private bool IsNullabilityMismatch(TypeWithAnnotations source, TypeWithAnnotations destination, bool requireIdentity)
		{
			if (!this.HasTopLevelNullabilityConversion(source, destination, requireIdentity))
			{
				return true;
			}
			if (requireIdentity)
			{
				return NullableWalker.IsNullabilityMismatch(source, destination);
			}
			TypeSymbol type = source.Type;
			TypeSymbol type2 = destination.Type;
			CompoundUseSiteInfo<AssemblySymbol> discarded = CompoundUseSiteInfo<AssemblySymbol>.Discarded;
			return !this._conversions.ClassifyImplicitConversionFromType(type, type2, ref discarded).Exists;
		}

		// Token: 0x06001C7A RID: 7290 RVA: 0x000973ED File Offset: 0x000955ED
		private bool HasTopLevelNullabilityConversion(TypeWithAnnotations source, TypeWithAnnotations destination, bool requireIdentity)
		{
			if (!requireIdentity)
			{
				return this._conversions.HasTopLevelNullabilityImplicitConversion(source, destination);
			}
			return this._conversions.HasTopLevelNullabilityIdentityConversion(source, destination);
		}

		// Token: 0x06001C7B RID: 7291 RVA: 0x0009740D File Offset: 0x0009560D
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private static BoundConversion GetConversionIfApplicable(BoundExpression conversionOpt, [global::System.Runtime.CompilerServices.Nullable(1)] BoundExpression convertedNode)
		{
			if (conversionOpt != convertedNode)
			{
				return (BoundConversion)conversionOpt;
			}
			return null;
		}

		// Token: 0x06001C7C RID: 7292 RVA: 0x0009741C File Offset: 0x0009561C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private TypeWithState VisitConversion(BoundConversion conversionOpt, [global::System.Runtime.CompilerServices.Nullable(1)] BoundExpression conversionOperand, Conversion conversion, TypeWithAnnotations targetTypeWithNullability, TypeWithState operandType, bool checkConversion, bool fromExplicitCast, bool useLegacyWarnings, NullableWalker.AssignmentKind assignmentKind, ParameterSymbol parameterOpt = null, bool reportTopLevelWarnings = true, bool reportRemainingWarnings = true, bool extensionMethodThisArgument = false, [global::System.Runtime.CompilerServices.Nullable(0)] Optional<NullableWalker.LocalState> stateForLambda = default(Optional<NullableWalker.LocalState>), bool trackMembers = false, Location diagnosticLocationOpt = null)
		{
			NullableWalker.<>c__DisplayClass233_0 CS$<>8__locals1;
			CS$<>8__locals1.conversionOperand = conversionOperand;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.targetTypeWithNullability = targetTypeWithNullability;
			CS$<>8__locals1.operandType = operandType;
			NullableFlowState nullableFlowState = NullableFlowState.NotNull;
			bool flag = true;
			bool flag2 = false;
			if (diagnosticLocationOpt == null)
			{
				diagnosticLocationOpt = (conversionOpt ?? CS$<>8__locals1.conversionOperand).Syntax.GetLocation();
			}
			if (CS$<>8__locals1.conversionOperand.IsSuppressed)
			{
				reportTopLevelWarnings = false;
				reportRemainingWarnings = false;
				flag2 = true;
			}
			TypeSymbol type = CS$<>8__locals1.targetTypeWithNullability.Type;
			switch (conversion.Kind)
			{
			case ConversionKind.NoConversion:
				nullableFlowState = NullableWalker.<VisitConversion>g__getConversionResultState|233_4(CS$<>8__locals1.operandType);
				goto IL_05C5;
			case ConversionKind.Identity:
				break;
			case ConversionKind.ImplicitNumeric:
			case ConversionKind.ImplicitEnumeration:
			case ConversionKind.ImplicitPointerToVoid:
			case ConversionKind.ImplicitNullToPointer:
			case ConversionKind.ImplicitPointer:
			case ConversionKind.ImplicitConstant:
			case ConversionKind.ExplicitNumeric:
			case ConversionKind.ExplicitEnumeration:
			case ConversionKind.ExplicitPointerToPointer:
			case ConversionKind.ExplicitIntegerToPointer:
			case ConversionKind.ExplicitPointerToInteger:
			case ConversionKind.IntPtr:
			case ConversionKind.Deconstruction:
			case ConversionKind.StackAllocToPointerType:
			case ConversionKind.StackAllocToSpanType:
			case ConversionKind.PinnedObjectToPointer:
				goto IL_05C5;
			case ConversionKind.ImplicitThrow:
				nullableFlowState = NullableFlowState.NotNull;
				goto IL_05C5;
			case ConversionKind.ImplicitTupleLiteral:
			case ConversionKind.ImplicitTuple:
			case ConversionKind.ExplicitTupleLiteral:
			case ConversionKind.ExplicitTuple:
				goto IL_052C;
			case ConversionKind.ImplicitNullable:
				goto IL_042E;
			case ConversionKind.NullLiteral:
			case ConversionKind.DefaultLiteral:
				checkConversion = false;
				break;
			case ConversionKind.ImplicitReference:
			case ConversionKind.ExplicitReference:
				goto IL_03D1;
			case ConversionKind.Boxing:
				nullableFlowState = NullableWalker.<VisitConversion>g__getBoxingConversionResultState|233_2(CS$<>8__locals1.targetTypeWithNullability, CS$<>8__locals1.operandType);
				goto IL_05C5;
			case ConversionKind.ImplicitDynamic:
			case ConversionKind.ExplicitDynamic:
				nullableFlowState = NullableWalker.<VisitConversion>g__getConversionResultState|233_4(CS$<>8__locals1.operandType);
				goto IL_05C5;
			case ConversionKind.ImplicitUserDefined:
			case ConversionKind.ExplicitUserDefined:
				return this.VisitUserDefinedConversion(conversionOpt, CS$<>8__locals1.conversionOperand, conversion, CS$<>8__locals1.targetTypeWithNullability, CS$<>8__locals1.operandType, useLegacyWarnings, assignmentKind, parameterOpt, reportTopLevelWarnings, reportRemainingWarnings, diagnosticLocationOpt);
			case ConversionKind.AnonymousFunction:
			{
				BoundLambda boundLambda = CS$<>8__locals1.conversionOperand as BoundLambda;
				if (boundLambda != null)
				{
					NamedTypeSymbol delegateType = type.GetDelegateType();
					this.VisitLambda(boundLambda, delegateType, stateForLambda);
					if (reportRemainingWarnings)
					{
						this.ReportNullabilityMismatchWithTargetDelegate(diagnosticLocationOpt, delegateType, boundLambda.UnboundLambda);
					}
					this.TrackAnalyzedNullabilityThroughConversionGroup(CS$<>8__locals1.targetTypeWithNullability.ToTypeWithState(), conversionOpt, CS$<>8__locals1.conversionOperand);
					return TypeWithState.Create(type, NullableFlowState.NotNull);
				}
				goto IL_05C5;
			}
			case ConversionKind.MethodGroup:
			{
				BoundMethodGroup boundMethodGroup = CS$<>8__locals1.conversionOperand as BoundMethodGroup;
				ValueTuple<MethodSymbol, ImmutableArray<ParameterSymbol>> valueTuple = NullableWalker.<VisitConversion>g__getDelegateOrFunctionPointerInfo|233_7(type);
				MethodSymbol item = valueTuple.Item1;
				ImmutableArray<ParameterSymbol> item2 = valueTuple.Item2;
				MethodSymbol methodSymbol = conversion.Method;
				if (boundMethodGroup != null)
				{
					LocalFunctionSymbol localFunctionSymbol = ((methodSymbol != null) ? methodSymbol.OriginalDefinition : null) as LocalFunctionSymbol;
					if (localFunctionSymbol != null)
					{
						this.VisitLocalFunctionUse(localFunctionSymbol);
					}
					methodSymbol = this.CheckMethodGroupReceiverNullability(boundMethodGroup, item2, methodSymbol, conversion.IsExtensionMethod);
				}
				if (reportRemainingWarnings && item != null)
				{
					this.ReportNullabilityMismatchWithTargetDelegate(diagnosticLocationOpt, type, item, methodSymbol, conversion.IsExtensionMethod);
				}
				nullableFlowState = NullableFlowState.NotNull;
				goto IL_05C5;
			}
			case ConversionKind.ExplicitNullable:
			{
				TypeSymbol type2 = CS$<>8__locals1.operandType.Type;
				if (type2 == null || !type2.IsNullableType() || type.IsNullableType())
				{
					goto IL_042E;
				}
				if (reportTopLevelWarnings && CS$<>8__locals1.operandType.MayBeNull)
				{
					this.ReportDiagnostic(ErrorCode.WRN_NullableValueTypeMayBeNull, diagnosticLocationOpt, Array.Empty<object>());
				}
				if (CS$<>8__locals1.conversionOperand != null)
				{
					this.LearnFromNonNullTest(CS$<>8__locals1.conversionOperand, ref this.State);
					goto IL_042E;
				}
				goto IL_042E;
			}
			case ConversionKind.Unboxing:
				if (type.IsNonNullableValueType())
				{
					if (!CS$<>8__locals1.operandType.IsNotNull && reportRemainingWarnings)
					{
						this.ReportDiagnostic(ErrorCode.WRN_UnboxPossibleNull, diagnosticLocationOpt, Array.Empty<object>());
					}
					this.LearnFromNonNullTest(CS$<>8__locals1.conversionOperand, ref this.State);
					goto IL_05C5;
				}
				nullableFlowState = NullableWalker.<VisitConversion>g__getUnboxingConversionResultState|233_3(CS$<>8__locals1.operandType);
				goto IL_05C5;
			case ConversionKind.InterpolatedString:
				nullableFlowState = NullableFlowState.NotNull;
				goto IL_05C5;
			case ConversionKind.SwitchExpression:
			case ConversionKind.ConditionalExpression:
			case ConversionKind.ObjectCreation:
				nullableFlowState = this.<VisitConversion>g__visitNestedTargetTypedConstructs|233_6(ref CS$<>8__locals1);
				this.TrackAnalyzedNullabilityThroughConversionGroup(CS$<>8__locals1.targetTypeWithNullability.ToTypeWithState(), conversionOpt, CS$<>8__locals1.conversionOperand);
				goto IL_05C5;
			default:
				goto IL_05C5;
			}
			if (useLegacyWarnings)
			{
				BoundConversion boundConversion = CS$<>8__locals1.conversionOperand as BoundConversion;
				if (boundConversion != null && !boundConversion.ConversionKind.IsUserDefinedConversion())
				{
					ConversionGroup conversionGroupOpt = boundConversion.ConversionGroupOpt;
					TypeWithAnnotations? typeWithAnnotations = ((conversionGroupOpt != null) ? new TypeWithAnnotations?(conversionGroupOpt.ExplicitType) : default(TypeWithAnnotations?));
					if (typeWithAnnotations != null && typeWithAnnotations.GetValueOrDefault().Equals(CS$<>8__locals1.targetTypeWithNullability, TypeCompareKind.ConsiderEverything))
					{
						this.TrackAnalyzedNullabilityThroughConversionGroup(NullableWalker.<VisitConversion>g__calculateResultType|233_0(CS$<>8__locals1.targetTypeWithNullability, fromExplicitCast, CS$<>8__locals1.operandType.State, flag2, type), conversionOpt, CS$<>8__locals1.conversionOperand);
						return CS$<>8__locals1.operandType;
					}
				}
			}
			TypeSymbol type3 = CS$<>8__locals1.operandType.Type;
			if ((type3 != null && type3.IsTupleType) || CS$<>8__locals1.conversionOperand.Kind == BoundKind.TupleLiteral)
			{
				goto IL_052C;
			}
			IL_03D1:
			if (checkConversion)
			{
				conversion = NullableWalker.GenerateConversion(this._conversions, CS$<>8__locals1.conversionOperand, CS$<>8__locals1.operandType.Type, type, fromExplicitCast, extensionMethodThisArgument);
				flag = conversion.Exists;
			}
			nullableFlowState = (conversion.IsReference ? NullableWalker.<VisitConversion>g__getReferenceConversionResultState|233_1(CS$<>8__locals1.targetTypeWithNullability, CS$<>8__locals1.operandType) : CS$<>8__locals1.operandType.State);
			goto IL_05C5;
			IL_042E:
			TypeWithAnnotations typeWithAnnotations2;
			if (trackMembers && NullableWalker.AreNullableAndUnderlyingTypes(type, CS$<>8__locals1.operandType.Type, out typeWithAnnotations2))
			{
				int num = this.MakeSlot(CS$<>8__locals1.conversionOperand);
				if (num > 0)
				{
					int orCreatePlaceholderSlot = this.GetOrCreatePlaceholderSlot(conversionOpt);
					this.TrackNullableStateOfNullableValue(orCreatePlaceholderSlot, type, CS$<>8__locals1.conversionOperand, typeWithAnnotations2.ToTypeWithState(), num);
				}
			}
			if (checkConversion)
			{
				conversion = NullableWalker.GenerateConversion(this._conversions, CS$<>8__locals1.conversionOperand, CS$<>8__locals1.operandType.Type, type, fromExplicitCast, extensionMethodThisArgument);
				flag = conversion.Exists;
			}
			nullableFlowState = CS$<>8__locals1.operandType.State;
			goto IL_05C5;
			IL_052C:
			if (trackMembers)
			{
				ConversionKind kind = conversion.Kind;
				if (kind == ConversionKind.ImplicitTuple || kind == ConversionKind.ExplicitTuple)
				{
					int num2 = this.MakeSlot(CS$<>8__locals1.conversionOperand);
					if (num2 > 0)
					{
						int orCreatePlaceholderSlot2 = this.GetOrCreatePlaceholderSlot(conversionOpt);
						if (orCreatePlaceholderSlot2 > 0)
						{
							this.TrackNullableStateOfTupleConversion(conversionOpt, CS$<>8__locals1.conversionOperand, conversion, type, CS$<>8__locals1.operandType.Type, orCreatePlaceholderSlot2, num2, assignmentKind, parameterOpt, reportRemainingWarnings);
						}
					}
				}
			}
			if (checkConversion && !type.IsErrorType())
			{
				conversion = NullableWalker.GenerateConversion(this._conversions, CS$<>8__locals1.conversionOperand, CS$<>8__locals1.operandType.Type, type, fromExplicitCast, extensionMethodThisArgument);
				flag = conversion.Exists;
			}
			nullableFlowState = NullableFlowState.NotNull;
			IL_05C5:
			TypeWithState typeWithState = NullableWalker.<VisitConversion>g__calculateResultType|233_0(CS$<>8__locals1.targetTypeWithNullability, fromExplicitCast, nullableFlowState, flag2, type);
			TypeSymbol type4 = CS$<>8__locals1.operandType.Type;
			if ((type4 == null || !type4.IsErrorType()) && !type.IsErrorType())
			{
				if (reportTopLevelWarnings)
				{
					this.ReportNullableAssignmentIfNecessary(CS$<>8__locals1.conversionOperand, CS$<>8__locals1.targetTypeWithNullability, typeWithState, useLegacyWarnings, assignmentKind, parameterOpt, diagnosticLocationOpt);
				}
				if (reportRemainingWarnings && !flag)
				{
					if (assignmentKind == NullableWalker.AssignmentKind.Argument)
					{
						this.ReportNullabilityMismatchInArgument(diagnosticLocationOpt, CS$<>8__locals1.operandType.Type, parameterOpt, type, false);
					}
					else
					{
						this.ReportNullabilityMismatchInAssignment(diagnosticLocationOpt, NullableWalker.GetTypeAsDiagnosticArgument(CS$<>8__locals1.operandType.Type), type);
					}
				}
			}
			this.TrackAnalyzedNullabilityThroughConversionGroup(typeWithState, conversionOpt, CS$<>8__locals1.conversionOperand);
			return typeWithState;
		}

		// Token: 0x06001C7D RID: 7293 RVA: 0x00097A98 File Offset: 0x00095C98
		private TypeWithState VisitUserDefinedConversion([global::System.Runtime.CompilerServices.Nullable(2)] BoundConversion conversionOpt, BoundExpression conversionOperand, Conversion conversion, TypeWithAnnotations targetTypeWithNullability, TypeWithState operandType, bool useLegacyWarnings, NullableWalker.AssignmentKind assignmentKind, [global::System.Runtime.CompilerServices.Nullable(2)] ParameterSymbol parameterOpt, bool reportTopLevelWarnings, bool reportRemainingWarnings, Location diagnosticLocation)
		{
			TypeSymbol type = targetTypeWithNullability.Type;
			if (!conversion.IsValid)
			{
				TypeWithState typeWithState = TypeWithState.Create(type, NullableFlowState.NotNull);
				this.TrackAnalyzedNullabilityThroughConversionGroup(typeWithState, conversionOpt, conversionOperand);
				return typeWithState;
			}
			operandType = this.VisitConversion(conversionOpt, conversionOperand, conversion.UserDefinedFromConversion, TypeWithAnnotations.Create(conversion.BestUserDefinedConversionAnalysis.FromType, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), operandType, true, false, useLegacyWarnings, assignmentKind, parameterOpt, reportTopLevelWarnings, reportRemainingWarnings, false, default(Optional<NullableWalker.LocalState>), false, diagnosticLocation);
			MethodSymbol method = conversion.Method;
			ParameterSymbol parameterSymbol = method.Parameters[0];
			FlowAnalysisAnnotations parameterAnnotations = this.GetParameterAnnotations(parameterSymbol);
			TypeWithAnnotations typeWithAnnotations = NullableWalker.ApplyLValueAnnotations(parameterSymbol.TypeWithAnnotations, parameterAnnotations);
			TypeWithState typeWithState2 = default(TypeWithState);
			bool flag = false;
			if (operandType.Type.IsNullableType() && !typeWithAnnotations.IsNullableType())
			{
				TypeWithAnnotations nullableUnderlyingTypeWithAnnotations = operandType.Type.GetNullableUnderlyingTypeWithAnnotations();
				typeWithState2 = nullableUnderlyingTypeWithAnnotations.ToTypeWithState();
				flag = typeWithAnnotations.Equals(nullableUnderlyingTypeWithAnnotations, TypeCompareKind.AllIgnoreOptions);
			}
			NullableFlowState state = operandType.State;
			Location location = conversionOperand.Syntax.GetLocation();
			this.ClassifyAndVisitConversion(conversionOperand, typeWithAnnotations, flag ? typeWithState2 : operandType, useLegacyWarnings, NullableWalker.AssignmentKind.Argument, parameterSymbol, reportRemainingWarnings, false, location);
			if (!flag && this.CheckDisallowedNullAssignment(operandType, parameterAnnotations, conversionOperand.Syntax.Location, null))
			{
				this.LearnFromNonNullTest(conversionOperand, ref this.State);
			}
			TypeWithAnnotations returnTypeWithAnnotations = method.ReturnTypeWithAnnotations;
			operandType = this.GetLiftedReturnTypeIfNecessary(flag, returnTypeWithAnnotations, state);
			if (!flag || state.IsNotNull())
			{
				if (state.IsNotNull() && method.ReturnNotNullIfParameterNotNull.Contains(parameterSymbol.Name))
				{
					operandType = operandType.WithNotNullState();
				}
				else
				{
					operandType = NullableWalker.ApplyUnconditionalAnnotations(operandType, this.GetRValueAnnotations(method));
				}
			}
			operandType = this.ClassifyAndVisitConversion(conversionOperand, TypeWithAnnotations.Create(conversion.BestUserDefinedConversionAnalysis.ToType, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), operandType, useLegacyWarnings, assignmentKind, parameterOpt, reportRemainingWarnings, false, location);
			operandType = this.ClassifyAndVisitConversion(conversionOpt ?? conversionOperand, targetTypeWithNullability, operandType, useLegacyWarnings, assignmentKind, parameterOpt, reportRemainingWarnings, conversionOpt != null && conversionOpt.ExplicitCastInCode, diagnosticLocation);
			this.TrackAnalyzedNullabilityThroughConversionGroup(operandType, conversionOpt, conversionOperand);
			return operandType;
		}

		// Token: 0x06001C7E RID: 7294 RVA: 0x00097CAC File Offset: 0x00095EAC
		private void SnapshotWalkerThroughConversionGroup(BoundExpression conversionExpression, BoundExpression convertedNode)
		{
			if (this._snapshotBuilderOpt == null)
			{
				return;
			}
			BoundConversion boundConversion = conversionExpression as BoundConversion;
			if (boundConversion != null)
			{
				ConversionGroup conversionGroupOpt = boundConversion.ConversionGroupOpt;
			}
			while (boundConversion != null && boundConversion != convertedNode && boundConversion.Syntax.SpanStart != convertedNode.Syntax.SpanStart)
			{
				this.TakeIncrementalSnapshot(boundConversion);
				boundConversion = boundConversion.Operand as BoundConversion;
			}
		}

		// Token: 0x06001C7F RID: 7295 RVA: 0x00097D08 File Offset: 0x00095F08
		private void TrackAnalyzedNullabilityThroughConversionGroup(TypeWithState resultType, [global::System.Runtime.CompilerServices.Nullable(2)] BoundConversion conversionOpt, BoundExpression convertedNode)
		{
			NullableWalker.VisitResult visitResult = new NullableWalker.VisitResult(resultType, resultType.ToTypeWithAnnotations(this.compilation, false));
			if (conversionOpt != null)
			{
				ConversionGroup conversionGroupOpt = conversionOpt.ConversionGroupOpt;
			}
			while (conversionOpt != null && conversionOpt != convertedNode)
			{
				visitResult = NullableWalker.<TrackAnalyzedNullabilityThroughConversionGroup>g__withType|236_0(visitResult, conversionOpt.Type);
				this.SetAnalyzedNullability(conversionOpt, visitResult, default(bool?));
				conversionOpt = conversionOpt.Operand as BoundConversion;
			}
		}

		// Token: 0x06001C80 RID: 7296 RVA: 0x00097D6C File Offset: 0x00095F6C
		private TypeWithState GetLiftedReturnType(TypeWithAnnotations returnType, NullableFlowState operandState)
		{
			TypeSymbol typeSymbol = (returnType.Type.IsNonNullableValueType() ? this.MakeNullableOf(returnType) : returnType.Type);
			NullableFlowState nullableFlowState = returnType.ToTypeWithState().State.Join(operandState);
			return TypeWithState.Create(typeSymbol, nullableFlowState);
		}

		// Token: 0x06001C81 RID: 7297 RVA: 0x00097DB0 File Offset: 0x00095FB0
		private static TypeWithState GetNullableUnderlyingTypeIfNecessary(bool isLifted, TypeWithState typeWithState)
		{
			if (isLifted)
			{
				TypeSymbol type = typeWithState.Type;
				if (type != null && type.IsNullableType())
				{
					return type.GetNullableUnderlyingTypeWithAnnotations().ToTypeWithState();
				}
			}
			return typeWithState;
		}

		// Token: 0x06001C82 RID: 7298 RVA: 0x00097DE2 File Offset: 0x00095FE2
		private TypeWithState GetLiftedReturnTypeIfNecessary(bool isLifted, TypeWithAnnotations returnType, NullableFlowState operandState)
		{
			if (!isLifted)
			{
				return returnType.ToTypeWithState();
			}
			return this.GetLiftedReturnType(returnType, operandState);
		}

		// Token: 0x06001C83 RID: 7299 RVA: 0x00097DF7 File Offset: 0x00095FF7
		private TypeSymbol MakeNullableOf(TypeWithAnnotations underlying)
		{
			return this.compilation.GetSpecialType(SpecialType.System_Nullable_T).Construct(ImmutableArray.Create<TypeWithAnnotations>(underlying));
		}

		// Token: 0x06001C84 RID: 7300 RVA: 0x00097E14 File Offset: 0x00096014
		private TypeWithState ClassifyAndVisitConversion(BoundExpression node, TypeWithAnnotations targetType, TypeWithState operandType, bool useLegacyWarnings, NullableWalker.AssignmentKind assignmentKind, [global::System.Runtime.CompilerServices.Nullable(2)] ParameterSymbol parameterOpt, bool reportWarnings, bool fromExplicitCast, Location diagnosticLocation)
		{
			CompoundUseSiteInfo<AssemblySymbol> discarded = CompoundUseSiteInfo<AssemblySymbol>.Discarded;
			Conversion conversion = this._conversions.ClassifyStandardConversion(null, operandType.Type, targetType.Type, ref discarded);
			if (reportWarnings && !conversion.Exists)
			{
				if (assignmentKind == NullableWalker.AssignmentKind.Argument)
				{
					this.ReportNullabilityMismatchInArgument(diagnosticLocation, operandType.Type, parameterOpt, targetType.Type, false);
				}
				else
				{
					this.ReportNullabilityMismatchInAssignment(diagnosticLocation, operandType.Type, targetType.Type);
				}
			}
			return this.VisitConversion(null, node, conversion, targetType, operandType, false, fromExplicitCast, useLegacyWarnings, assignmentKind, parameterOpt, reportWarnings, !fromExplicitCast && reportWarnings, false, default(Optional<NullableWalker.LocalState>), false, diagnosticLocation);
		}

		// Token: 0x06001C85 RID: 7301 RVA: 0x00097EB4 File Offset: 0x000960B4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDelegateCreationExpression(BoundDelegateCreationExpression node)
		{
			MethodSymbol methodOpt = node.MethodOpt;
			LocalFunctionSymbol localFunctionSymbol = ((methodOpt != null) ? methodOpt.OriginalDefinition : null) as LocalFunctionSymbol;
			if (localFunctionSymbol != null)
			{
				this.VisitLocalFunctionUse(localFunctionSymbol);
			}
			NamedTypeSymbol namedTypeSymbol = (NamedTypeSymbol)node.Type;
			BoundExpression argument = node.Argument;
			BoundMethodGroup boundMethodGroup = argument as BoundMethodGroup;
			if (boundMethodGroup == null)
			{
				BoundLambda boundLambda = argument as BoundLambda;
				if (boundLambda == null)
				{
					if (argument != null)
					{
						TypeSymbol type = argument.Type;
						if (type != null && type.TypeKind == TypeKind.Delegate)
						{
							TypeWithAnnotations typeWithAnnotations = TypeWithAnnotations.Create(type, NullableAnnotation.NotAnnotated, default(ImmutableArray<CustomModifier>));
							TypeWithState typeWithState = this.VisitRvalueWithState(argument);
							this.ReportNullableAssignmentIfNecessary(argument, typeWithAnnotations, typeWithState, false, NullableWalker.AssignmentKind.Assignment, null, null);
							if (!argument.IsSuppressed)
							{
								this.ReportNullabilityMismatchWithTargetDelegate(argument.Syntax.Location, namedTypeSymbol, namedTypeSymbol.DelegateInvokeMethod, type.DelegateInvokeMethod(), false);
							}
							this.LearnFromNonNullTest(argument, ref this.State);
							goto IL_01A0;
						}
					}
					this.VisitRvalue(node.Argument, false);
				}
				else
				{
					this.VisitLambda(boundLambda, namedTypeSymbol, default(Optional<NullableWalker.LocalState>));
					this.SetNotNullResult(boundLambda);
					if (!boundLambda.IsSuppressed)
					{
						this.ReportNullabilityMismatchWithTargetDelegate(boundLambda.Symbol.DiagnosticLocation, namedTypeSymbol, boundLambda.UnboundLambda);
					}
				}
			}
			else
			{
				this.VisitMethodGroup(boundMethodGroup);
				MethodSymbol methodSymbol = node.MethodOpt;
				if (methodSymbol != null)
				{
					methodSymbol = this.CheckMethodGroupReceiverNullability(boundMethodGroup, namedTypeSymbol.DelegateInvokeMethod.Parameters, methodSymbol, node.IsExtensionMethod);
					if (!boundMethodGroup.IsSuppressed)
					{
						this.ReportNullabilityMismatchWithTargetDelegate(boundMethodGroup.Syntax.Location, namedTypeSymbol, namedTypeSymbol.DelegateInvokeMethod, methodSymbol, node.IsExtensionMethod);
					}
				}
				this.SetAnalyzedNullability(boundMethodGroup, default(NullableWalker.VisitResult), default(bool?));
			}
			IL_01A0:
			this.SetNotNullResult(node);
			return null;
		}

		// Token: 0x06001C86 RID: 7302 RVA: 0x0009806C File Offset: 0x0009626C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitMethodGroup(BoundMethodGroup node)
		{
			BoundExpression receiverOpt = node.ReceiverOpt;
			if (receiverOpt != null)
			{
				this.VisitRvalue(receiverOpt, false);
				this.SetMethodGroupReceiverNullability(receiverOpt, this.ResultType);
			}
			this.SetNotNullResult(node);
			return null;
		}

		// Token: 0x06001C87 RID: 7303 RVA: 0x000980A0 File Offset: 0x000962A0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private bool TryGetMethodGroupReceiverNullability([NotNullWhen(true)] BoundExpression receiverOpt, out TypeWithState type)
		{
			if (receiverOpt != null && this._methodGroupReceiverMapOpt != null && this._methodGroupReceiverMapOpt.TryGetValue(receiverOpt, ref type))
			{
				return true;
			}
			type = default(TypeWithState);
			return false;
		}

		// Token: 0x06001C88 RID: 7304 RVA: 0x000980C6 File Offset: 0x000962C6
		private void SetMethodGroupReceiverNullability(BoundExpression receiver, TypeWithState type)
		{
			if (this._methodGroupReceiverMapOpt == null)
			{
				this._methodGroupReceiverMapOpt = PooledDictionary<BoundExpression, TypeWithState>.GetInstance();
			}
			this._methodGroupReceiverMapOpt[receiver] = type;
		}

		// Token: 0x06001C89 RID: 7305 RVA: 0x000980E8 File Offset: 0x000962E8
		private MethodSymbol CheckMethodGroupReceiverNullability(BoundMethodGroup group, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<ParameterSymbol> parameters, MethodSymbol method, bool invokedAsExtensionMethod)
		{
			BoundExpression receiverOpt = group.ReceiverOpt;
			TypeWithState typeWithState;
			if (this.TryGetMethodGroupReceiverNullability(receiverOpt, out typeWithState))
			{
				SyntaxNode syntax = group.Syntax;
				if (!invokedAsExtensionMethod)
				{
					method = (MethodSymbol)NullableWalker.AsMemberOfType(typeWithState.Type, method);
				}
				if (method.IsGenericMethod && NullableWalker.HasImplicitTypeArguments(group.Syntax))
				{
					ArrayBuilder<BoundExpression> instance = ArrayBuilder<BoundExpression>.GetInstance();
					if (invokedAsExtensionMethod)
					{
						instance.Add(NullableWalker.CreatePlaceholderIfNecessary(receiverOpt, typeWithState.ToTypeWithAnnotations(this.compilation, false)));
					}
					foreach (ParameterSymbol parameterSymbol in parameters)
					{
						TypeWithAnnotations typeWithAnnotations = parameterSymbol.TypeWithAnnotations;
						instance.Add(new BoundExpressionWithNullability(syntax, new BoundParameter(syntax, parameterSymbol), typeWithAnnotations.NullableAnnotation, typeWithAnnotations.Type));
					}
					method = this.InferMethodTypeArguments(method, instance.ToImmutableAndFree(), default(ImmutableArray<RefKind>), default(ImmutableArray<int>), false);
				}
				if (invokedAsExtensionMethod)
				{
					this.CheckExtensionMethodThisNullability(receiverOpt, Conversion.Identity, method.Parameters[0], typeWithState);
				}
				else
				{
					this.CheckPossibleNullReceiver(receiverOpt, typeWithState, false);
				}
				if (ConstraintsHelper.RequiresChecking(method))
				{
					this.CheckMethodConstraints(syntax, method);
				}
			}
			return method;
		}

		// Token: 0x06001C8A RID: 7306 RVA: 0x00098211 File Offset: 0x00096411
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitLambda(BoundLambda node)
		{
			this.SetNotNullResult(node);
			return null;
		}

		// Token: 0x06001C8B RID: 7307 RVA: 0x0009821C File Offset: 0x0009641C
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private void VisitLambda([global::System.Runtime.CompilerServices.Nullable(1)] BoundLambda node, [global::System.Runtime.CompilerServices.Nullable(2)] NamedTypeSymbol delegateTypeOpt, Optional<NullableWalker.LocalState> initialState = default(Optional<NullableWalker.LocalState>))
		{
			MethodSymbol methodSymbol = ((delegateTypeOpt != null) ? delegateTypeOpt.DelegateInvokeMethod : null);
			bool flag = NullableWalker.UseDelegateInvokeParameterTypes(node, methodSymbol);
			if (flag && this._snapshotBuilderOpt != null)
			{
				this.SetUpdatedSymbol(node, node.Symbol, delegateTypeOpt);
			}
			this.AnalyzeLocalFunctionOrLambda(node, node.Symbol, initialState.HasValue ? initialState.Value : this.State.Clone(), methodSymbol, flag);
		}

		// Token: 0x06001C8C RID: 7308 RVA: 0x00098283 File Offset: 0x00096483
		private static bool UseDelegateInvokeParameterTypes(BoundLambda lambda, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol delegateInvokeMethod)
		{
			return delegateInvokeMethod != null && !lambda.UnboundLambda.HasExplicitlyTypedParameterList;
		}

		// Token: 0x06001C8D RID: 7309 RVA: 0x00098298 File Offset: 0x00096498
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitUnboundLambda(UnboundLambda node)
		{
			BoundLambda boundLambda = node.BindForErrorRecovery();
			this.VisitLambda(boundLambda, null, default(Optional<NullableWalker.LocalState>));
			this.SetNotNullResult(node);
			return null;
		}

		// Token: 0x06001C8E RID: 7310 RVA: 0x000982C5 File Offset: 0x000964C5
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitThisReference(BoundThisReference node)
		{
			this.VisitThisOrBaseReference(node);
			return null;
		}

		// Token: 0x06001C8F RID: 7311 RVA: 0x000982D0 File Offset: 0x000964D0
		private void VisitThisOrBaseReference(BoundExpression node)
		{
			TypeWithState typeWithState = TypeWithState.Create(node.Type, NullableFlowState.NotNull);
			TypeWithAnnotations typeWithAnnotations = TypeWithAnnotations.Create(node.Type, NullableAnnotation.NotAnnotated, default(ImmutableArray<CustomModifier>));
			this.SetResult(node, typeWithState, typeWithAnnotations, true, default(bool?));
		}

		// Token: 0x06001C90 RID: 7312 RVA: 0x00098314 File Offset: 0x00096514
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitParameter(BoundParameter node)
		{
			ParameterSymbol parameterSymbol = node.ParameterSymbol;
			int orCreateSlot = this.GetOrCreateSlot(parameterSymbol, 0, false, true);
			TypeWithAnnotations declaredParameterResult = this.GetDeclaredParameterResult(parameterSymbol);
			TypeWithState parameterState = NullableWalker.GetParameterState(declaredParameterResult, parameterSymbol.FlowAnalysisAnnotations);
			this.SetResult(node, this.GetAdjustedResult(parameterState, orCreateSlot), declaredParameterResult, true, default(bool?));
			return null;
		}

		// Token: 0x06001C91 RID: 7313 RVA: 0x00098364 File Offset: 0x00096564
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitAssignmentOperator(BoundAssignmentOperator node)
		{
			BoundExpression boundExpression = node.Left;
			BoundFieldAccess boundFieldAccess = boundExpression as BoundFieldAccess;
			if (boundFieldAccess != null)
			{
				FieldSymbol fieldSymbol = boundFieldAccess.ExpressionSymbol as FieldSymbol;
				if (fieldSymbol != null)
				{
					Symbol associatedSymbol = fieldSymbol.AssociatedSymbol;
					PropertySymbol propertySymbol = associatedSymbol as PropertySymbol;
					if (propertySymbol == null)
					{
						EventSymbol eventSymbol = associatedSymbol as EventSymbol;
						if (eventSymbol != null)
						{
							BoundFieldAccess boundFieldAccess2 = boundFieldAccess;
							boundExpression = new BoundEventAccess(boundFieldAccess2.Syntax, boundFieldAccess2.ReceiverOpt, eventSymbol, true, LookupResultKind.Viable, eventSymbol.Type, boundFieldAccess2.HasErrors);
						}
					}
					else
					{
						boundExpression = new BoundPropertyAccess(boundFieldAccess.Syntax, boundFieldAccess.ReceiverOpt, propertySymbol, LookupResultKind.Viable, propertySymbol.Type, boundFieldAccess.HasErrors);
					}
				}
			}
			BoundExpression right = node.Right;
			this.VisitLValue(boundExpression);
			base.Unsplit();
			FlowAnalysisAnnotations lvalueAnnotations = this.GetLValueAnnotations(boundExpression);
			TypeWithAnnotations typeWithAnnotations = NullableWalker.ApplyLValueAnnotations(this.LvalueResultType, lvalueAnnotations);
			if (boundExpression.Kind == BoundKind.EventAccess && ((BoundEventAccess)boundExpression).EventSymbol.IsWindowsRuntimeEvent)
			{
				this.VisitRvalue(right, false);
				this.SetNotNullResult(node);
			}
			else
			{
				TypeWithState typeWithState;
				if (!node.IsRef)
				{
					bool flag = boundExpression is BoundDiscardExpression;
					typeWithState = this.VisitOptionalImplicitConversion(right, flag ? default(TypeWithAnnotations) : typeWithAnnotations, NullableWalker.UseLegacyWarnings(boundExpression, typeWithAnnotations), true, NullableWalker.AssignmentKind.Assignment);
				}
				else
				{
					typeWithState = this.VisitRefExpression(right, typeWithAnnotations);
				}
				this.CheckDisallowedNullAssignment(typeWithState, lvalueAnnotations, right.Syntax.Location, null);
				this.AdjustSetValue(boundExpression, ref typeWithState);
				this.TrackNullableStateForAssignment(right, typeWithAnnotations, this.MakeSlot(boundExpression), typeWithState, this.MakeSlot(right));
				if (boundExpression is BoundDiscardExpression)
				{
					TypeWithAnnotations typeWithAnnotations2 = typeWithState.ToTypeWithAnnotations(this.compilation, false);
					this.SetResult(boundExpression, typeWithState, typeWithAnnotations2, true, new bool?(true));
					this.SetResult(node, typeWithState, typeWithAnnotations2, true, default(bool?));
				}
				else
				{
					this.SetResult(node, TypeWithState.Create(typeWithAnnotations.Type, typeWithState.State), typeWithAnnotations, true, default(bool?));
				}
			}
			return null;
		}

		// Token: 0x06001C92 RID: 7314 RVA: 0x00098550 File Offset: 0x00096750
		private bool IsPropertyOutputMoreStrictThanInput(PropertySymbol property)
		{
			TypeWithAnnotations typeWithAnnotations = property.TypeWithAnnotations;
			FlowAnalysisAnnotations flowAnalysisAnnotations = (this.IsAnalyzingAttribute ? FlowAnalysisAnnotations.None : property.GetFlowAnalysisAnnotations());
			TypeWithAnnotations typeWithAnnotations2 = NullableWalker.ApplyLValueAnnotations(typeWithAnnotations, flowAnalysisAnnotations);
			return !typeWithAnnotations2.NullableAnnotation.IsOblivious() && typeWithAnnotations2.CanBeAssignedNull && NullableWalker.ApplyUnconditionalAnnotations(typeWithAnnotations.ToTypeWithState(), flowAnalysisAnnotations).IsNotNull;
		}

		// Token: 0x06001C93 RID: 7315 RVA: 0x000985AC File Offset: 0x000967AC
		private void AdjustSetValue(BoundExpression left, ref TypeWithState rightState)
		{
			BoundPropertyAccess boundPropertyAccess = left as BoundPropertyAccess;
			PropertySymbol propertySymbol;
			if (boundPropertyAccess == null)
			{
				BoundIndexerAccess boundIndexerAccess = left as BoundIndexerAccess;
				if (boundIndexerAccess == null)
				{
					propertySymbol = null;
				}
				else
				{
					propertySymbol = boundIndexerAccess.Indexer;
				}
			}
			else
			{
				propertySymbol = boundPropertyAccess.PropertySymbol;
			}
			PropertySymbol propertySymbol2 = propertySymbol;
			if (propertySymbol2 != null && this.IsPropertyOutputMoreStrictThanInput(propertySymbol2))
			{
				rightState = rightState.WithNotNullState();
			}
		}

		// Token: 0x06001C94 RID: 7316 RVA: 0x00098600 File Offset: 0x00096800
		private FlowAnalysisAnnotations GetLValueAnnotations(BoundExpression expr)
		{
			if (this.IsAnalyzingAttribute)
			{
				return FlowAnalysisAnnotations.None;
			}
			BoundPropertyAccess boundPropertyAccess = expr as BoundPropertyAccess;
			FlowAnalysisAnnotations flowAnalysisAnnotations;
			if (boundPropertyAccess == null)
			{
				BoundIndexerAccess boundIndexerAccess = expr as BoundIndexerAccess;
				if (boundIndexerAccess == null)
				{
					BoundFieldAccess boundFieldAccess = expr as BoundFieldAccess;
					if (boundFieldAccess == null)
					{
						BoundObjectInitializerMember boundObjectInitializerMember = expr as BoundObjectInitializerMember;
						if (boundObjectInitializerMember != null)
						{
							Symbol memberSymbol = boundObjectInitializerMember.MemberSymbol;
							PropertySymbol propertySymbol = memberSymbol as PropertySymbol;
							if (propertySymbol != null)
							{
								flowAnalysisAnnotations = propertySymbol.GetFlowAnalysisAnnotations();
								goto IL_00CB;
							}
							FieldSymbol fieldSymbol = memberSymbol as FieldSymbol;
							if (fieldSymbol != null)
							{
								flowAnalysisAnnotations = NullableWalker.<GetLValueAnnotations>g__getFieldAnnotations|257_0(fieldSymbol);
								goto IL_00CB;
							}
						}
						else
						{
							BoundParameter boundParameter = expr as BoundParameter;
							if (boundParameter != null)
							{
								ParameterSymbol parameterSymbol = boundParameter.ParameterSymbol;
								if (parameterSymbol != null)
								{
									flowAnalysisAnnotations = NullableWalker.ToInwardAnnotations(this.GetParameterAnnotations(parameterSymbol) & ~(FlowAnalysisAnnotations.NotNullWhenTrue | FlowAnalysisAnnotations.NotNullWhenFalse));
									goto IL_00CB;
								}
							}
						}
						flowAnalysisAnnotations = FlowAnalysisAnnotations.None;
					}
					else
					{
						flowAnalysisAnnotations = NullableWalker.<GetLValueAnnotations>g__getFieldAnnotations|257_0(boundFieldAccess.FieldSymbol);
					}
				}
				else
				{
					flowAnalysisAnnotations = boundIndexerAccess.Indexer.GetFlowAnalysisAnnotations();
				}
			}
			else
			{
				flowAnalysisAnnotations = boundPropertyAccess.PropertySymbol.GetFlowAnalysisAnnotations();
			}
			IL_00CB:
			return flowAnalysisAnnotations & (FlowAnalysisAnnotations.AllowNull | FlowAnalysisAnnotations.DisallowNull);
		}

		// Token: 0x06001C95 RID: 7317 RVA: 0x000986DC File Offset: 0x000968DC
		private static FlowAnalysisAnnotations ToInwardAnnotations(FlowAnalysisAnnotations outwardAnnotations)
		{
			FlowAnalysisAnnotations flowAnalysisAnnotations = FlowAnalysisAnnotations.None;
			if ((outwardAnnotations & FlowAnalysisAnnotations.MaybeNull) != FlowAnalysisAnnotations.None)
			{
				flowAnalysisAnnotations |= FlowAnalysisAnnotations.AllowNull;
			}
			if ((outwardAnnotations & FlowAnalysisAnnotations.NotNull) == FlowAnalysisAnnotations.NotNull)
			{
				flowAnalysisAnnotations |= FlowAnalysisAnnotations.DisallowNull;
			}
			return flowAnalysisAnnotations;
		}

		// Token: 0x06001C96 RID: 7318 RVA: 0x00098704 File Offset: 0x00096904
		private static bool UseLegacyWarnings(BoundExpression expr, TypeWithAnnotations exprType)
		{
			BoundKind kind = expr.Kind;
			if (kind != BoundKind.Local)
			{
				return kind == BoundKind.Parameter && ((BoundParameter)expr).ParameterSymbol.RefKind == RefKind.None;
			}
			return expr.GetRefKind() == RefKind.None;
		}

		// Token: 0x06001C97 RID: 7319 RVA: 0x00098744 File Offset: 0x00096944
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDeconstructionAssignmentOperator(BoundDeconstructionAssignmentOperator node)
		{
			return this.VisitDeconstructionAssignmentOperator(node, default(TypeWithState?));
		}

		// Token: 0x06001C98 RID: 7320 RVA: 0x00098764 File Offset: 0x00096964
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		private BoundNode VisitDeconstructionAssignmentOperator(BoundDeconstructionAssignmentOperator node, TypeWithState? rightResultOpt)
		{
			bool disableNullabilityAnalysis = this._disableNullabilityAnalysis;
			this._disableNullabilityAnalysis = true;
			BoundTupleExpression left = node.Left;
			BoundConversion right = node.Right;
			ArrayBuilder<NullableWalker.DeconstructionVariable> deconstructionAssignmentVariables = this.GetDeconstructionAssignmentVariables(left);
			if (node.HasErrors)
			{
				this.VisitRvalue(right.Operand, false);
			}
			else
			{
				this.VisitDeconstructionArguments(deconstructionAssignmentVariables, right.Conversion, right.Operand, rightResultOpt);
			}
			deconstructionAssignmentVariables.FreeAll<NullableWalker.DeconstructionVariable>((NullableWalker.DeconstructionVariable v) => v.NestedVariables);
			this.SetNotNullResult(node);
			this._disableNullabilityAnalysis = disableNullabilityAnalysis;
			return null;
		}

		// Token: 0x06001C99 RID: 7321 RVA: 0x000987F4 File Offset: 0x000969F4
		private void VisitDeconstructionArguments(ArrayBuilder<NullableWalker.DeconstructionVariable> variables, Conversion conversion, BoundExpression right, TypeWithState? rightResultOpt = null)
		{
			if (!conversion.DeconstructionInfo.IsDefault)
			{
				this.VisitDeconstructMethodArguments(variables, conversion, right, rightResultOpt);
				return;
			}
			this.VisitTupleDeconstructionArguments(variables, conversion.UnderlyingConversions, right);
		}

		// Token: 0x06001C9A RID: 7322 RVA: 0x00098830 File Offset: 0x00096A30
		private void VisitDeconstructMethodArguments(ArrayBuilder<NullableWalker.DeconstructionVariable> variables, Conversion conversion, BoundExpression right, TypeWithState? rightResultOpt)
		{
			this.VisitRvalue(right, false);
			if (rightResultOpt != null)
			{
				this.SetResultType(right, rightResultOpt.Value, true);
			}
			TypeWithState resultType = this.ResultType;
			BoundCall boundCall = conversion.DeconstructionInfo.Invocation as BoundCall;
			MethodSymbol methodSymbol = ((boundCall != null) ? boundCall.Method : null);
			if (methodSymbol != null)
			{
				int count = variables.Count;
				if (!boundCall.InvokedAsExtensionMethod)
				{
					this.CheckPossibleNullReceiver(right, false);
					if (methodSymbol.OriginalDefinition != methodSymbol)
					{
						methodSymbol = methodSymbol.OriginalDefinition.AsMember((NamedTypeSymbol)resultType.Type);
					}
				}
				else if (methodSymbol.IsGenericMethod)
				{
					ArrayBuilder<BoundExpression> instance = ArrayBuilder<BoundExpression>.GetInstance(count + 1);
					instance.Add(NullableWalker.CreatePlaceholderIfNecessary(right, resultType.ToTypeWithAnnotations(this.compilation, false)));
					for (int i = 0; i < count; i++)
					{
						instance.Add(new BoundExpressionWithNullability(variables[i].Expression.Syntax, variables[i].Expression, NullableAnnotation.Oblivious, conversion.DeconstructionInfo.OutputPlaceholders[i].Type));
					}
					methodSymbol = this.InferMethodTypeArguments(methodSymbol, instance.ToImmutableAndFree(), boundCall.ArgumentRefKindsOpt, boundCall.ArgsToParamsOpt, boundCall.Expanded);
					if (ConstraintsHelper.RequiresChecking(methodSymbol))
					{
						this.CheckMethodConstraints(boundCall.Syntax, methodSymbol);
					}
				}
				ImmutableArray<ParameterSymbol> parameters = methodSymbol.Parameters;
				int num = (boundCall.InvokedAsExtensionMethod ? 1 : 0);
				if (boundCall.InvokedAsExtensionMethod)
				{
					Conversion item = NullableWalker.RemoveConversion(boundCall.Arguments[0], false).Item2;
					this.CheckExtensionMethodThisNullability(right, item, methodSymbol.Parameters[0], resultType);
				}
				for (int j = 0; j < count; j++)
				{
					NullableWalker.DeconstructionVariable deconstructionVariable = variables[j];
					ParameterSymbol parameterSymbol = parameters[j + num];
					Conversion conversion2 = conversion.UnderlyingConversions[j];
					ArrayBuilder<NullableWalker.DeconstructionVariable> nestedVariables = deconstructionVariable.NestedVariables;
					if (nestedVariables != null)
					{
						BoundExpression boundExpression = NullableWalker.CreatePlaceholderIfNecessary(boundCall.Arguments[j + num], parameterSymbol.TypeWithAnnotations);
						this.VisitDeconstructionArguments(nestedVariables, conversion2, boundExpression, default(TypeWithState?));
					}
					else
					{
						this.VisitArgumentConversionAndInboundAssignmentsAndPreConditions(null, deconstructionVariable.Expression, conversion2, parameterSymbol.RefKind, parameterSymbol, parameterSymbol.TypeWithAnnotations, this.GetParameterAnnotations(parameterSymbol), new NullableWalker.VisitArgumentResult(new NullableWalker.VisitResult(deconstructionVariable.Type.ToTypeWithState(), deconstructionVariable.Type), default(Optional<NullableWalker.LocalState>)), false);
					}
				}
				for (int k = 0; k < count; k++)
				{
					NullableWalker.DeconstructionVariable deconstructionVariable2 = variables[k];
					ParameterSymbol parameterSymbol2 = parameters[k + num];
					if (deconstructionVariable2.NestedVariables == null)
					{
						this.VisitArgumentOutboundAssignmentsAndPostConditions(deconstructionVariable2.Expression, parameterSymbol2.RefKind, parameterSymbol2, parameterSymbol2.TypeWithAnnotations, this.GetRValueAnnotations(parameterSymbol2), new NullableWalker.VisitArgumentResult(new NullableWalker.VisitResult(deconstructionVariable2.Type.ToTypeWithState(), deconstructionVariable2.Type), default(Optional<NullableWalker.LocalState>)), null, default(NullableWalker.CompareExchangeInfo));
					}
				}
			}
		}

		// Token: 0x06001C9B RID: 7323 RVA: 0x00098B48 File Offset: 0x00096D48
		private void VisitTupleDeconstructionArguments(ArrayBuilder<NullableWalker.DeconstructionVariable> variables, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<Conversion> conversions, BoundExpression right)
		{
			int count = variables.Count;
			ImmutableArray<BoundExpression> deconstructionRightParts = this.GetDeconstructionRightParts(right);
			for (int i = 0; i < count; i++)
			{
				NullableWalker.DeconstructionVariable deconstructionVariable = variables[i];
				Conversion conversion = conversions[i];
				BoundExpression boundExpression = deconstructionRightParts[i];
				ArrayBuilder<NullableWalker.DeconstructionVariable> nestedVariables = deconstructionVariable.NestedVariables;
				if (nestedVariables != null)
				{
					this.VisitDeconstructionArguments(nestedVariables, conversion, boundExpression, default(TypeWithState?));
				}
				else
				{
					TypeWithAnnotations typeWithAnnotations = deconstructionVariable.Type;
					FlowAnalysisAnnotations lvalueAnnotations = this.GetLValueAnnotations(deconstructionVariable.Expression);
					typeWithAnnotations = NullableWalker.ApplyLValueAnnotations(typeWithAnnotations, lvalueAnnotations);
					TypeWithState typeWithState2;
					TypeWithState typeWithState;
					int num;
					if (conversion.IsIdentity)
					{
						BoundLocal boundLocal = deconstructionVariable.Expression as BoundLocal;
						if (boundLocal != null && boundLocal.DeclarationKind == BoundLocalDeclarationKind.WithInferredType)
						{
							typeWithState = (typeWithState2 = this.VisitRvalueWithState(boundExpression));
							this._variables.SetType(boundLocal.LocalSymbol, typeWithState.ToAnnotatedTypeWithAnnotations(this.compilation));
						}
						else
						{
							typeWithState = default(TypeWithState);
							typeWithState2 = this.VisitOptionalImplicitConversion(boundExpression, typeWithAnnotations, true, true, NullableWalker.AssignmentKind.Assignment);
						}
						num = this.MakeSlot(boundExpression);
					}
					else
					{
						typeWithState = this.VisitRvalueWithState(boundExpression);
						typeWithState2 = this.VisitConversion(null, boundExpression, conversion, typeWithAnnotations, typeWithState, true, false, true, NullableWalker.AssignmentKind.Assignment, null, true, true, false, default(Optional<NullableWalker.LocalState>), false, null);
						num = -1;
					}
					this.CheckDisallowedNullAssignment(typeWithState2, lvalueAnnotations, right.Syntax.Location, null);
					int num2 = this.MakeSlot(deconstructionVariable.Expression);
					this.AdjustSetValue(deconstructionVariable.Expression, ref typeWithState2);
					this.TrackNullableStateForAssignment(boundExpression, typeWithAnnotations, num2, typeWithState2, num);
					TypeWithAnnotations typeWithAnnotations2;
					if (num2 > 0 && conversion.Kind == ConversionKind.ImplicitNullable && NullableWalker.AreNullableAndUnderlyingTypes(typeWithAnnotations.Type, typeWithState.Type, out typeWithAnnotations2))
					{
						num = this.MakeSlot(boundExpression);
						if (num > 0)
						{
							TypeWithState typeWithState3 = TypeWithState.Create(typeWithAnnotations2.Type, NullableFlowState.NotNull);
							this.TrackNullableStateOfNullableValue(num2, typeWithAnnotations.Type, boundExpression, typeWithState3, num);
						}
					}
				}
			}
		}

		// Token: 0x06001C9C RID: 7324 RVA: 0x00098D20 File Offset: 0x00096F20
		private ArrayBuilder<NullableWalker.DeconstructionVariable> GetDeconstructionAssignmentVariables(BoundTupleExpression tuple)
		{
			ImmutableArray<BoundExpression> arguments = tuple.Arguments;
			ArrayBuilder<NullableWalker.DeconstructionVariable> instance = ArrayBuilder<NullableWalker.DeconstructionVariable>.GetInstance(arguments.Length);
			foreach (BoundExpression boundExpression in arguments)
			{
				instance.Add(this.<GetDeconstructionAssignmentVariables>g__getDeconstructionAssignmentVariable|266_0(boundExpression));
			}
			return instance;
		}

		// Token: 0x06001C9D RID: 7325 RVA: 0x00098D6C File Offset: 0x00096F6C
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<BoundExpression> GetDeconstructionRightParts(BoundExpression expr)
		{
			BoundKind kind = expr.Kind;
			if (kind != BoundKind.Conversion)
			{
				if (kind - BoundKind.TupleLiteral <= 1)
				{
					return ((BoundTupleExpression)expr).Arguments;
				}
			}
			else
			{
				BoundConversion boundConversion = (BoundConversion)expr;
				ConversionKind conversionKind = boundConversion.ConversionKind;
				if (conversionKind == ConversionKind.Identity || conversionKind == ConversionKind.ImplicitTupleLiteral)
				{
					return this.GetDeconstructionRightParts(boundConversion.Operand);
				}
			}
			NamedTypeSymbol namedTypeSymbol = expr.Type as NamedTypeSymbol;
			if (namedTypeSymbol != null && namedTypeSymbol.IsTupleType)
			{
				return namedTypeSymbol.TupleElements.SelectAsArray((FieldSymbol f, BoundExpression e) => new BoundFieldAccess(e.Syntax, e, f, null, false), expr);
			}
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06001C9E RID: 7326 RVA: 0x00098E08 File Offset: 0x00097008
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitIncrementOperator(BoundIncrementOperator node)
		{
			TypeWithState typeWithState = this.VisitRvalueWithState(node.Operand);
			TypeWithAnnotations lvalueResultType = this.LvalueResultType;
			bool flag = false;
			if (this.State.Reachable)
			{
				MethodSymbol methodSymbol;
				if (node.OperatorKind.IsUserDefined())
				{
					MethodSymbol methodOpt = node.MethodOpt;
					if (methodOpt != null && methodOpt.ParameterCount == 1)
					{
						methodSymbol = node.MethodOpt;
						goto IL_0053;
					}
				}
				methodSymbol = null;
				IL_0053:
				MethodSymbol methodSymbol2 = methodSymbol;
				NullableWalker.AssignmentKind assignmentKind = NullableWalker.AssignmentKind.Assignment;
				ParameterSymbol parameterSymbol = null;
				TypeWithAnnotations typeWithAnnotations;
				if (node.OperandConversion.IsUserDefined)
				{
					MethodSymbol method = node.OperandConversion.Method;
					if (method != null && method.ParameterCount == 1)
					{
						typeWithAnnotations = node.OperandConversion.Method.ReturnTypeWithAnnotations;
						goto IL_00DC;
					}
				}
				if (methodSymbol2 != null)
				{
					typeWithAnnotations = methodSymbol2.Parameters[0].TypeWithAnnotations;
					assignmentKind = NullableWalker.AssignmentKind.Argument;
					parameterSymbol = methodSymbol2.Parameters[0];
				}
				else
				{
					typeWithAnnotations = default(TypeWithAnnotations);
				}
				IL_00DC:
				TypeWithState typeWithState2;
				if (typeWithAnnotations.HasType)
				{
					typeWithState2 = this.VisitConversion(null, node.Operand, node.OperandConversion, typeWithAnnotations, typeWithState, true, false, false, assignmentKind, parameterSymbol, true, true, false, default(Optional<NullableWalker.LocalState>), false, null);
				}
				else
				{
					typeWithState2 = typeWithState;
				}
				TypeWithState typeWithState3;
				if (methodSymbol2 == null)
				{
					typeWithState3 = typeWithState2;
				}
				else
				{
					typeWithState3 = methodSymbol2.ReturnTypeWithAnnotations.ToTypeWithState();
				}
				TypeWithAnnotations typeWithAnnotations2 = typeWithState.ToTypeWithAnnotations(this.compilation, false);
				typeWithState3 = this.VisitConversion(null, node, node.ResultConversion, typeWithAnnotations2, typeWithState3, true, false, false, NullableWalker.AssignmentKind.Assignment, null, true, true, false, default(Optional<NullableWalker.LocalState>), false, null);
				if (!node.HasErrors)
				{
					UnaryOperatorKind unaryOperatorKind = node.OperatorKind.Operator();
					TypeWithState typeWithState4 = ((unaryOperatorKind == UnaryOperatorKind.PrefixIncrement || unaryOperatorKind == UnaryOperatorKind.PrefixDecrement) ? typeWithState3 : typeWithState);
					this.SetResultType(node, typeWithState4, true);
					flag = true;
					this.TrackNullableStateForAssignment(node, lvalueResultType, this.MakeSlot(node.Operand), typeWithState3, -1);
				}
			}
			if (!flag)
			{
				this.SetNotNullResult(node);
			}
			return null;
		}

		// Token: 0x06001C9F RID: 7327 RVA: 0x00098FDC File Offset: 0x000971DC
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitCompoundAssignmentOperator(BoundCompoundAssignmentOperator node)
		{
			BoundExpression left = node.Left;
			BoundExpression right = node.Right;
			this.Visit(left);
			TypeWithAnnotations typeWithAnnotations = this.LvalueResultType;
			TypeWithState resultType = this.ResultType;
			TypeWithState typeWithState = this.GetAdjustedResult(resultType, this.MakeSlot(node.Left));
			if (node.Operator.LeftType != null)
			{
				typeWithState = this.VisitConversion(null, node.Left, node.LeftConversion, TypeWithAnnotations.Create(node.Operator.LeftType, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), typeWithState, true, false, false, NullableWalker.AssignmentKind.Assignment, null, false, true, false, default(Optional<NullableWalker.LocalState>), false, null);
			}
			else
			{
				typeWithState = default(TypeWithState);
			}
			TypeWithState typeWithState2 = this.VisitRvalueWithState(right);
			TypeWithState typeWithState3;
			if (node.Operator.ReturnType != null)
			{
				if (node.Operator.Kind.IsUserDefined() && node.Operator.Method != null && node.Operator.Method.ParameterCount == 2)
				{
					MethodSymbol method = node.Operator.Method;
					this.VisitArguments(node, ImmutableArray.Create<BoundExpression>(node.Left, right), method.ParameterRefKinds, method.Parameters, default(ImmutableArray<int>), default(BitVector), true, false, method);
				}
				typeWithState3 = this.InferResultNullability(node.Operator.Kind, node.Operator.Method, node.Operator.ReturnType, typeWithState, typeWithState2);
				FlowAnalysisAnnotations lvalueAnnotations = this.GetLValueAnnotations(node.Left);
				typeWithAnnotations = NullableWalker.ApplyLValueAnnotations(typeWithAnnotations, lvalueAnnotations);
				typeWithState3 = this.VisitConversion(null, node, node.FinalConversion, typeWithAnnotations, typeWithState3, true, false, false, NullableWalker.AssignmentKind.Assignment, null, true, true, false, default(Optional<NullableWalker.LocalState>), false, null);
				this.CheckDisallowedNullAssignment(typeWithState3, lvalueAnnotations, node.Syntax.Location, null);
			}
			else
			{
				typeWithState3 = TypeWithState.Create(node.Type, NullableFlowState.NotNull);
			}
			this.AdjustSetValue(left, ref typeWithState3);
			this.TrackNullableStateForAssignment(node, typeWithAnnotations, this.MakeSlot(node.Left), typeWithState3, -1);
			this.SetResultType(node, typeWithState3, true);
			return null;
		}

		// Token: 0x06001CA0 RID: 7328 RVA: 0x000991D0 File Offset: 0x000973D0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitFixedLocalCollectionInitializer(BoundFixedLocalCollectionInitializer node)
		{
			BoundExpression boundExpression = node.Expression;
			if (boundExpression.Kind == BoundKind.AddressOfOperator)
			{
				boundExpression = ((BoundAddressOfOperator)boundExpression).Operand;
			}
			this.VisitRvalue(boundExpression, false);
			if (node.Expression.Kind == BoundKind.AddressOfOperator)
			{
				this.SetResultType(node.Expression, TypeWithState.Create(node.Expression.Type, this.ResultType.State), true);
			}
			this.SetNotNullResult(node);
			return null;
		}

		// Token: 0x06001CA1 RID: 7329 RVA: 0x00099241 File Offset: 0x00097441
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitAddressOfOperator(BoundAddressOfOperator node)
		{
			this.Visit(node.Operand);
			this.SetNotNullResult(node);
			return null;
		}

		// Token: 0x06001CA2 RID: 7330 RVA: 0x00099258 File Offset: 0x00097458
		private void ReportArgumentWarnings(BoundExpression argument, TypeWithState argumentType, ParameterSymbol parameter)
		{
			TypeWithAnnotations typeWithAnnotations = parameter.TypeWithAnnotations;
			this.ReportNullableAssignmentIfNecessary(argument, typeWithAnnotations, argumentType, false, NullableWalker.AssignmentKind.Argument, parameter, null);
			TypeSymbol type = argumentType.Type;
			if (type != null && NullableWalker.IsNullabilityMismatch(typeWithAnnotations.Type, type))
			{
				this.ReportNullabilityMismatchInArgument(argument.Syntax, type, parameter, typeWithAnnotations.Type, false);
			}
		}

		// Token: 0x06001CA3 RID: 7331 RVA: 0x000992A8 File Offset: 0x000974A8
		private void ReportNullabilityMismatchInRefArgument(BoundExpression argument, TypeSymbol argumentType, ParameterSymbol parameter, TypeSymbol parameterType)
		{
			this.ReportDiagnostic(ErrorCode.WRN_NullabilityMismatchInArgument, argument.Syntax, new object[]
			{
				argumentType,
				parameterType,
				NullableWalker.GetParameterAsDiagnosticArgument(parameter),
				NullableWalker.GetContainingSymbolAsDiagnosticArgument(parameter)
			});
		}

		// Token: 0x06001CA4 RID: 7332 RVA: 0x000992DC File Offset: 0x000974DC
		private void ReportNullabilityMismatchInArgument(SyntaxNode argument, TypeSymbol argumentType, ParameterSymbol parameter, TypeSymbol parameterType, bool forOutput)
		{
			this.ReportNullabilityMismatchInArgument(argument.GetLocation(), argumentType, parameter, parameterType, forOutput);
		}

		// Token: 0x06001CA5 RID: 7333 RVA: 0x000992F0 File Offset: 0x000974F0
		private void ReportNullabilityMismatchInArgument(Location argumentLocation, TypeSymbol argumentType, [global::System.Runtime.CompilerServices.Nullable(2)] ParameterSymbol parameterOpt, TypeSymbol parameterType, bool forOutput)
		{
			this.ReportDiagnostic(forOutput ? ErrorCode.WRN_NullabilityMismatchInArgumentForOutput : ErrorCode.WRN_NullabilityMismatchInArgument, argumentLocation, new object[]
			{
				argumentType,
				(parameterOpt != null && parameterOpt.Type.IsNonNullableValueType() && parameterType.IsNullableType()) ? parameterOpt.Type : parameterType,
				NullableWalker.GetParameterAsDiagnosticArgument(parameterOpt),
				NullableWalker.GetContainingSymbolAsDiagnosticArgument(parameterOpt)
			});
		}

		// Token: 0x06001CA6 RID: 7334 RVA: 0x00099358 File Offset: 0x00097558
		private TypeWithAnnotations GetDeclaredLocalResult(LocalSymbol local)
		{
			TypeWithAnnotations typeWithAnnotations;
			if (!this._variables.TryGetType(local, out typeWithAnnotations))
			{
				return local.TypeWithAnnotations;
			}
			return typeWithAnnotations;
		}

		// Token: 0x06001CA7 RID: 7335 RVA: 0x00099380 File Offset: 0x00097580
		private TypeWithAnnotations GetDeclaredParameterResult(ParameterSymbol parameter)
		{
			TypeWithAnnotations typeWithAnnotations;
			if (!this._variables.TryGetType(parameter, out typeWithAnnotations))
			{
				return parameter.TypeWithAnnotations;
			}
			return typeWithAnnotations;
		}

		// Token: 0x06001CA8 RID: 7336 RVA: 0x000993A5 File Offset: 0x000975A5
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitBaseReference(BoundBaseReference node)
		{
			this.VisitThisOrBaseReference(node);
			return null;
		}

		// Token: 0x06001CA9 RID: 7337 RVA: 0x000993B0 File Offset: 0x000975B0
		private void SplitIfBooleanConstant(BoundExpression node)
		{
			ConstantValue constantValue = node.ConstantValue;
			if (constantValue != null && constantValue.IsBoolean)
			{
				bool booleanValue = constantValue.BooleanValue;
				base.Split();
				if (booleanValue)
				{
					this.StateWhenFalse = this.UnreachableState();
					return;
				}
				this.StateWhenTrue = this.UnreachableState();
			}
		}

		// Token: 0x06001CAA RID: 7338 RVA: 0x000993F8 File Offset: 0x000975F8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitFieldAccess(BoundFieldAccess node)
		{
			Symbol symbol = this.VisitMemberAccess(node, node.ReceiverOpt, node.FieldSymbol);
			this.SplitIfBooleanConstant(node);
			this.SetUpdatedSymbol(node, node.FieldSymbol, symbol);
			return null;
		}

		// Token: 0x06001CAB RID: 7339 RVA: 0x00099430 File Offset: 0x00097630
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitPropertyAccess(BoundPropertyAccess node)
		{
			PropertySymbol propertySymbol = node.PropertySymbol;
			Symbol symbol = this.VisitMemberAccess(node, node.ReceiverOpt, propertySymbol);
			if (!this.IsAnalyzingAttribute)
			{
				if (this._expressionIsRead)
				{
					this.ApplyMemberPostConditions(node.ReceiverOpt, propertySymbol.GetMethod);
				}
				else
				{
					this.ApplyMemberPostConditions(node.ReceiverOpt, propertySymbol.SetMethod);
				}
			}
			this.SetUpdatedSymbol(node, propertySymbol, symbol);
			return null;
		}

		// Token: 0x06001CAC RID: 7340 RVA: 0x00099494 File Offset: 0x00097694
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitIndexerAccess(BoundIndexerAccess node)
		{
			BoundExpression receiverOpt = node.ReceiverOpt;
			TypeSymbol type = this.VisitRvalueWithState(receiverOpt).Type;
			this.CheckPossibleNullReceiver(receiverOpt, false);
			PropertySymbol propertySymbol = node.Indexer;
			if (type != null)
			{
				propertySymbol = (PropertySymbol)NullableWalker.AsMemberOfType(type, propertySymbol);
			}
			this.VisitArguments(node, node.Arguments, node.ArgumentRefKindsOpt, propertySymbol, node.ArgsToParamsOpt, node.DefaultArguments, node.Expanded);
			TypeWithState typeWithState = NullableWalker.ApplyUnconditionalAnnotations(propertySymbol.TypeWithAnnotations.ToTypeWithState(), this.GetRValueAnnotations(propertySymbol));
			this.SetResult(node, typeWithState, propertySymbol.TypeWithAnnotations, true, default(bool?));
			this.SetUpdatedSymbol(node, node.Indexer, propertySymbol);
			return null;
		}

		// Token: 0x06001CAD RID: 7341 RVA: 0x00099540 File Offset: 0x00097740
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitIndexOrRangePatternIndexerAccess(BoundIndexOrRangePatternIndexerAccess node)
		{
			BoundExpression receiver = node.Receiver;
			TypeSymbol type = this.VisitRvalueWithState(receiver).Type;
			this.CheckPossibleNullReceiver(receiver, false);
			this.VisitRvalue(node.Argument, false);
			Symbol symbol = node.PatternSymbol;
			if (type != null)
			{
				symbol = NullableWalker.AsMemberOfType(type, symbol);
			}
			this.SetLvalueResultType(node, symbol.GetTypeOrReturnType());
			this.SetUpdatedSymbol(node, node.PatternSymbol, symbol);
			return null;
		}

		// Token: 0x06001CAE RID: 7342 RVA: 0x000995A8 File Offset: 0x000977A8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitEventAccess(BoundEventAccess node)
		{
			Symbol symbol = this.VisitMemberAccess(node, node.ReceiverOpt, node.EventSymbol);
			this.SetUpdatedSymbol(node, node.EventSymbol, symbol);
			return null;
		}

		// Token: 0x06001CAF RID: 7343 RVA: 0x000995D8 File Offset: 0x000977D8
		private Symbol VisitMemberAccess(BoundExpression node, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression receiverOpt, Symbol member)
		{
			TypeWithState typeWithState = ((receiverOpt != null) ? this.VisitRvalueWithState(receiverOpt) : default(TypeWithState));
			SpecialMember? specialMember = default(SpecialMember?);
			SpecialMember? specialMember2;
			SpecialMember specialMember3;
			if (member.RequiresInstanceReceiver())
			{
				member = NullableWalker.AsMemberOfType(typeWithState.Type, member);
				specialMember = this.GetNullableOfTMember(member);
				specialMember2 = specialMember;
				specialMember3 = SpecialMember.System_Nullable_T_get_Value;
				bool flag = !((specialMember2.GetValueOrDefault() == specialMember3) & (specialMember2 != null));
				this.CheckPossibleNullReceiver(receiverOpt, !flag);
			}
			TypeWithAnnotations typeOrReturnType = member.GetTypeOrReturnType();
			FlowAnalysisAnnotations rvalueAnnotations = this.GetRValueAnnotations(member);
			TypeWithState typeWithState2 = NullableWalker.ApplyUnconditionalAnnotations(typeOrReturnType.ToTypeWithState(), rvalueAnnotations);
			if (NullableWalker.PossiblyNullableType(typeWithState2.Type))
			{
				int num = base.MakeMemberSlot(receiverOpt, member);
				if (this.State.HasValue(num))
				{
					NullableFlowState nullableFlowState = this.State[num];
					typeWithState2 = TypeWithState.Create(typeWithState2.Type, nullableFlowState);
				}
			}
			specialMember2 = specialMember;
			specialMember3 = SpecialMember.System_Nullable_T_get_HasValue;
			if (((specialMember2.GetValueOrDefault() == specialMember3) & (specialMember2 != null)) && receiverOpt != null)
			{
				int num2 = this.MakeSlot(receiverOpt);
				if (num2 > 0)
				{
					base.Split();
					this.StateWhenTrue[num2] = NullableFlowState.NotNull;
				}
			}
			this.SetResult(node, typeWithState2, typeOrReturnType, true, default(bool?));
			return member;
		}

		// Token: 0x06001CB0 RID: 7344 RVA: 0x0009970C File Offset: 0x0009790C
		private SpecialMember? GetNullableOfTMember(Symbol member)
		{
			if (member.Kind == SymbolKind.Property)
			{
				MethodSymbol getMethod = ((PropertySymbol)member.OriginalDefinition).GetMethod;
				if (getMethod != null && getMethod.ContainingType.SpecialType == SpecialType.System_Nullable_T)
				{
					if (getMethod == this.compilation.GetSpecialTypeMember(SpecialMember.System_Nullable_T_get_Value))
					{
						return new SpecialMember?(SpecialMember.System_Nullable_T_get_Value);
					}
					if (getMethod == this.compilation.GetSpecialTypeMember(SpecialMember.System_Nullable_T_get_HasValue))
					{
						return new SpecialMember?(SpecialMember.System_Nullable_T_get_HasValue);
					}
				}
			}
			return default(SpecialMember?);
		}

		// Token: 0x06001CB1 RID: 7345 RVA: 0x0009978C File Offset: 0x0009798C
		private int GetNullableOfTValueSlot(TypeSymbol containingType, int containingSlot, [global::System.Runtime.CompilerServices.Nullable(2)] out Symbol valueProperty, bool forceSlotEvenIfEmpty = false)
		{
			MethodSymbol methodSymbol = (MethodSymbol)this.compilation.GetSpecialTypeMember(SpecialMember.System_Nullable_T_get_Value);
			Symbol symbol;
			if (methodSymbol == null)
			{
				symbol = null;
			}
			else
			{
				MethodSymbol methodSymbol2 = methodSymbol.AsMember((NamedTypeSymbol)containingType);
				symbol = ((methodSymbol2 != null) ? methodSymbol2.AssociatedSymbol : null);
			}
			valueProperty = symbol;
			if (valueProperty != null)
			{
				return this.GetOrCreateSlot(valueProperty, containingSlot, forceSlotEvenIfEmpty, true);
			}
			return -1;
		}

		// Token: 0x06001CB2 RID: 7346 RVA: 0x000997E0 File Offset: 0x000979E0
		protected override void VisitForEachExpression(BoundForEachStatement node)
		{
			if (node.Expression.Kind != BoundKind.Conversion)
			{
				this.VisitRvalue(node.Expression, false);
				return;
			}
			ValueTuple<BoundExpression, Conversion> valueTuple = NullableWalker.RemoveConversion(node.Expression, false);
			BoundExpression item = valueTuple.Item1;
			Conversion item2 = valueTuple.Item2;
			this.SnapshotWalkerThroughConversionGroup(node.Expression, item);
			TypeWithState typeWithState = this.VisitRvalueWithState(item);
			TypeSymbol type = typeWithState.Type;
			this.SetAnalyzedNullability(item, this._visitResult, default(bool?));
			MethodSymbol methodSymbol = null;
			ForEachEnumeratorInfo enumeratorInfoOpt = node.EnumeratorInfoOpt;
			MethodArgumentInfo methodArgumentInfo = ((enumeratorInfoOpt != null) ? enumeratorInfoOpt.GetEnumeratorInfo : null);
			MethodSymbol methodSymbol2;
			TypeWithAnnotations typeWithAnnotations;
			if (methodArgumentInfo != null)
			{
				methodSymbol2 = methodArgumentInfo.Method;
				if (methodSymbol2 != null && methodSymbol2.IsExtensionMethod)
				{
					ImmutableArray<ParameterSymbol> parameters = methodSymbol2.Parameters;
					ValueTuple<MethodSymbol, ImmutableArray<NullableWalker.VisitArgumentResult>, bool> valueTuple2 = this.VisitArguments(node, methodArgumentInfo.Arguments, default(ImmutableArray<RefKind>), parameters, methodArgumentInfo.ArgsToParamsOpt, methodArgumentInfo.DefaultArguments, false, true, methodArgumentInfo.Method);
					MethodSymbol item3 = valueTuple2.Item1;
					ImmutableArray<NullableWalker.VisitArgumentResult> item4 = valueTuple2.Item2;
					typeWithAnnotations = item4[0].LValueType;
					methodSymbol = item3;
					goto IL_01CA;
				}
			}
			if (item2.IsIdentity || (item2.Kind == ConversionKind.ExplicitReference && type.SpecialType == SpecialType.System_String))
			{
				typeWithAnnotations = typeWithState.ToTypeWithAnnotations(this.compilation, false);
			}
			else
			{
				if (!item2.IsImplicit)
				{
					return;
				}
				bool flag = node.AwaitOpt != null;
				if (node.Expression.Type.SpecialType == SpecialType.System_Collections_IEnumerable)
				{
					typeWithAnnotations = TypeWithAnnotations.Create(node.Expression.Type, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
				}
				else
				{
					if (!ForEachLoopBinder.IsIEnumerableT(node.Expression.Type.OriginalDefinition, flag, this.compilation))
					{
						return;
					}
					CompoundUseSiteInfo<AssemblySymbol> discarded = CompoundUseSiteInfo<AssemblySymbol>.Discarded;
					bool flag2;
					typeWithAnnotations = TypeWithAnnotations.Create(ForEachLoopBinder.GetIEnumerableOfT(type, flag, this.compilation, ref discarded, out flag2), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
				}
			}
			IL_01CA:
			TypeWithState typeWithState2 = this.VisitConversion(NullableWalker.GetConversionIfApplicable(node.Expression, item), item, item2, typeWithAnnotations, typeWithState, true, false, false, NullableWalker.AssignmentKind.Assignment, null, true, true, false, default(Optional<NullableWalker.LocalState>), false, null);
			ForEachEnumeratorInfo enumeratorInfoOpt2 = node.EnumeratorInfoOpt;
			methodSymbol2 = ((enumeratorInfoOpt2 != null) ? enumeratorInfoOpt2.GetEnumeratorInfo.Method : null);
			bool flag3 = (methodSymbol2 == null || !methodSymbol2.IsExtensionMethod) && this.CheckPossibleNullReceiver(item, false);
			this.SetAnalyzedNullability(node.Expression, new NullableWalker.VisitResult(typeWithState2, typeWithState2.ToTypeWithAnnotations(this.compilation, false)), default(bool?));
			TypeWithState typeWithState3;
			if (node.EnumeratorInfoOpt == null)
			{
				typeWithState3 = default(TypeWithState);
			}
			else
			{
				ArrayTypeSymbol arrayTypeSymbol = type as ArrayTypeSymbol;
				if (arrayTypeSymbol != null)
				{
					typeWithState3 = arrayTypeSymbol.ElementTypeWithAnnotations.ToTypeWithState();
				}
				else if (type.SpecialType == SpecialType.System_String)
				{
					typeWithState3 = TypeWithAnnotations.Create(node.EnumeratorInfoOpt.ElementType, NullableAnnotation.NotAnnotated, default(ImmutableArray<CustomModifier>)).ToTypeWithState();
				}
				else
				{
					if (methodSymbol == null)
					{
						methodSymbol = (MethodSymbol)NullableWalker.AsMemberOfType(typeWithState2.Type, node.EnumeratorInfoOpt.GetEnumeratorInfo.Method);
					}
					TypeWithState returnTypeWithState = this.GetReturnTypeWithState(methodSymbol);
					if (returnTypeWithState.State != NullableFlowState.NotNull && !flag3)
					{
						BoundConversion boundConversion = node.Expression as BoundConversion;
						if (boundConversion != null)
						{
							BoundExpression operand = boundConversion.Operand;
							if (operand != null && operand.IsSuppressed)
							{
								goto IL_033F;
							}
						}
						this.ReportDiagnostic(ErrorCode.WRN_NullReferenceReceiver, item.Syntax.GetLocation(), Array.Empty<object>());
					}
					IL_033F:
					MethodSymbol methodSymbol3 = (MethodSymbol)NullableWalker.AsMemberOfType(returnTypeWithState.Type, node.EnumeratorInfoOpt.CurrentPropertyGetter);
					typeWithState3 = NullableWalker.ApplyUnconditionalAnnotations(methodSymbol3.ReturnTypeWithAnnotations.ToTypeWithState(), methodSymbol3.ReturnTypeFlowAnalysisAnnotations);
					BoundAwaitableInfo awaitOpt = node.AwaitOpt;
					if (awaitOpt != null)
					{
						BoundAwaitableValuePlaceholder awaitableInstancePlaceholder = awaitOpt.AwaitableInstancePlaceholder;
						if (awaitableInstancePlaceholder != null)
						{
							MethodSymbol methodSymbol4 = (MethodSymbol)NullableWalker.AsMemberOfType(methodSymbol.ReturnType, node.EnumeratorInfoOpt.MoveNextInfo.Method);
							this.EnsureAwaitablePlaceholdersInitialized();
							NullableWalker.VisitResult visitResult = new NullableWalker.VisitResult(this.GetReturnTypeWithState(methodSymbol4), methodSymbol4.ReturnTypeWithAnnotations);
							this._awaitablePlaceholdersOpt.Add(awaitableInstancePlaceholder, new ValueTuple<BoundExpression, NullableWalker.VisitResult>(awaitableInstancePlaceholder, visitResult));
							this.Visit(awaitOpt);
							this._awaitablePlaceholdersOpt.Remove(awaitableInstancePlaceholder);
						}
					}
					ForEachEnumeratorInfo enumeratorInfoOpt3 = node.EnumeratorInfoOpt;
					if (enumeratorInfoOpt3 != null && enumeratorInfoOpt3.NeedsDisposal)
					{
						BoundAwaitableInfo disposeAwaitableInfo = enumeratorInfoOpt3.DisposeAwaitableInfo;
						if (disposeAwaitableInfo != null)
						{
							BoundAwaitableValuePlaceholder awaitableInstancePlaceholder2 = disposeAwaitableInfo.AwaitableInstancePlaceholder;
							bool flag4 = false;
							MethodArgumentInfo patternDisposeInfo = node.EnumeratorInfoOpt.PatternDisposeInfo;
							if (patternDisposeInfo != null)
							{
								MethodSymbol method = patternDisposeInfo.Method;
								MethodSymbol methodSymbol5 = (MethodSymbol)NullableWalker.AsMemberOfType(methodSymbol.ReturnType, method);
								this.EnsureAwaitablePlaceholdersInitialized();
								NullableWalker.VisitResult visitResult2 = new NullableWalker.VisitResult(this.GetReturnTypeWithState(methodSymbol5), methodSymbol5.ReturnTypeWithAnnotations);
								this._awaitablePlaceholdersOpt.Add(awaitableInstancePlaceholder2, new ValueTuple<BoundExpression, NullableWalker.VisitResult>(awaitableInstancePlaceholder2, visitResult2));
								flag4 = true;
							}
							this.Visit(disposeAwaitableInfo);
							if (flag4)
							{
								this._awaitablePlaceholdersOpt.Remove(awaitableInstancePlaceholder2);
							}
						}
					}
				}
			}
			this.SetResultType(null, typeWithState3, true);
		}

		// Token: 0x06001CB3 RID: 7347 RVA: 0x00099CAC File Offset: 0x00097EAC
		public override void VisitForEachIterationVariables(BoundForEachStatement node)
		{
			TypeWithState typeWithState = ((node.EnumeratorInfoOpt == null) ? default(TypeWithState) : this.ResultType);
			TypeWithAnnotations typeWithAnnotations = typeWithState.ToTypeWithAnnotations(this.compilation, false);
			SyntaxNode syntax = node.Syntax;
			ForEachStatementSyntax forEachStatementSyntax = syntax as ForEachStatementSyntax;
			Location location;
			if (forEachStatementSyntax == null)
			{
				ForEachVariableStatementSyntax forEachVariableStatementSyntax = syntax as ForEachVariableStatementSyntax;
				if (forEachVariableStatementSyntax == null)
				{
					throw ExceptionUtilities.UnexpectedValue(node.Syntax);
				}
				location = forEachVariableStatementSyntax.Variable.GetLocation();
			}
			else
			{
				location = forEachStatementSyntax.Identifier.GetLocation();
			}
			Location location2 = location;
			if (node.DeconstructionOpt != null)
			{
				BoundDeconstructionAssignmentOperator deconstructionAssignment = node.DeconstructionOpt.DeconstructionAssignment;
				this.VisitDeconstructionAssignmentOperator(deconstructionAssignment, typeWithState.HasNullType ? default(TypeWithState?) : new TypeWithState?(typeWithState));
				this.Visit(node.IterationVariableType);
				return;
			}
			this.Visit(node.IterationVariableType);
			foreach (LocalSymbol localSymbol in node.IterationVariables)
			{
				NullableFlowState nullableFlowState = NullableFlowState.NotNull;
				if (!typeWithState.HasNullType)
				{
					TypeWithAnnotations typeWithAnnotations2 = localSymbol.TypeWithAnnotations;
					TypeWithState typeWithState2 = typeWithState;
					TypeWithState typeWithState3 = typeWithState;
					if (localSymbol.IsRef)
					{
						if (NullableWalker.IsNullabilityMismatch(typeWithAnnotations, typeWithAnnotations2))
						{
							ForEachStatementSyntax forEachStatementSyntax2 = (ForEachStatementSyntax)node.Syntax;
							this.ReportNullabilityMismatchInAssignment(forEachStatementSyntax2.Type, typeWithAnnotations, typeWithAnnotations2);
						}
					}
					else
					{
						SourceLocalSymbol sourceLocalSymbol = localSymbol as SourceLocalSymbol;
						if (sourceLocalSymbol != null && sourceLocalSymbol.IsVar)
						{
							typeWithAnnotations2 = typeWithState.ToAnnotatedTypeWithAnnotations(this.compilation);
							this._variables.SetType(localSymbol, typeWithAnnotations2);
							typeWithState3 = typeWithAnnotations2.ToTypeWithState();
						}
						else
						{
							CompoundUseSiteInfo<AssemblySymbol> discarded = CompoundUseSiteInfo<AssemblySymbol>.Discarded;
							Conversion conversion = ((node.ElementConversion.Kind == ConversionKind.UnsetConversionKind) ? this._conversions.ClassifyImplicitConversionFromType(typeWithAnnotations.Type, typeWithAnnotations2.Type, ref discarded) : node.ElementConversion);
							BoundConversion boundConversion = null;
							BoundExpression iterationVariableType = node.IterationVariableType;
							Conversion conversion2 = conversion;
							TypeWithAnnotations typeWithAnnotations3 = typeWithAnnotations2;
							TypeWithState typeWithState4 = typeWithState;
							bool flag = true;
							bool flag2 = !conversion.IsImplicit;
							bool flag3 = true;
							NullableWalker.AssignmentKind assignmentKind = NullableWalker.AssignmentKind.ForEachIterationVariable;
							ParameterSymbol parameterSymbol = null;
							bool flag4 = true;
							bool flag5 = true;
							bool flag6 = false;
							location = location2;
							typeWithState2 = this.VisitConversion(boundConversion, iterationVariableType, conversion2, typeWithAnnotations3, typeWithState4, flag, flag2, flag3, assignmentKind, parameterSymbol, flag4, flag5, flag6, default(Optional<NullableWalker.LocalState>), false, location);
						}
					}
					this.SetAnalyzedNullability(node.IterationVariableType, new NullableWalker.VisitResult(typeWithState3, typeWithAnnotations2), new bool?(true));
					nullableFlowState = typeWithState2.State;
				}
				int orCreateSlot = this.GetOrCreateSlot(localSymbol, 0, false, true);
				if (orCreateSlot > 0)
				{
					this.State[orCreateSlot] = nullableFlowState;
				}
			}
		}

		// Token: 0x06001CB4 RID: 7348 RVA: 0x00099F0E File Offset: 0x0009810E
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitFromEndIndexExpression(BoundFromEndIndexExpression node)
		{
			BoundNode boundNode = base.VisitFromEndIndexExpression(node);
			this.SetNotNullResult(node);
			return boundNode;
		}

		// Token: 0x06001CB5 RID: 7349 RVA: 0x00099F1E File Offset: 0x0009811E
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitObjectInitializerMember(BoundObjectInitializerMember node)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06001CB6 RID: 7350 RVA: 0x00099F25 File Offset: 0x00098125
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDynamicObjectInitializerMember(BoundDynamicObjectInitializerMember node)
		{
			this.SetNotNullResult(node);
			return null;
		}

		// Token: 0x06001CB7 RID: 7351 RVA: 0x00099F30 File Offset: 0x00098130
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitBadExpression(BoundBadExpression node)
		{
			foreach (BoundExpression boundExpression in node.ChildBoundNodes)
			{
				BoundLambda boundLambda = boundExpression as BoundLambda;
				if (boundLambda != null)
				{
					this.TakeIncrementalSnapshot(boundLambda);
					this.VisitLambda(boundLambda, null, default(Optional<NullableWalker.LocalState>));
					this.VisitRvalueEpilogue(boundLambda);
				}
				else
				{
					this.VisitRvalue(boundExpression, false);
				}
			}
			TypeWithAnnotations typeWithAnnotations = TypeWithAnnotations.Create(node.Type, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
			this.SetLvalueResultType(node, typeWithAnnotations);
			return null;
		}

		// Token: 0x06001CB8 RID: 7352 RVA: 0x00099FB8 File Offset: 0x000981B8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitTypeExpression(BoundTypeExpression node)
		{
			BoundNode boundNode = base.VisitTypeExpression(node);
			if (node.BoundContainingTypeOpt != null)
			{
				this.VisitTypeExpression(node.BoundContainingTypeOpt);
			}
			this.SetNotNullResult(node);
			return boundNode;
		}

		// Token: 0x06001CB9 RID: 7353 RVA: 0x00099FDD File Offset: 0x000981DD
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitTypeOrValueExpression(BoundTypeOrValueExpression node)
		{
			BoundNode boundNode = base.VisitTypeOrValueExpression(node);
			this.SetNotNullResult(node);
			return boundNode;
		}

		// Token: 0x06001CBA RID: 7354 RVA: 0x00099FF0 File Offset: 0x000981F0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitUnaryOperator(BoundUnaryOperator node)
		{
			NullableWalker.<>c__DisplayClass296_0 CS$<>8__locals1;
			CS$<>8__locals1.node = node;
			UnaryOperatorKind operatorKind = CS$<>8__locals1.node.OperatorKind;
			TypeWithState typeWithState;
			if (operatorKind != UnaryOperatorKind.BoolLogicalNegation)
			{
				if (operatorKind != UnaryOperatorKind.DynamicLogicalNegation)
				{
					if (operatorKind != UnaryOperatorKind.DynamicTrue)
					{
						if (CS$<>8__locals1.node.OperatorKind.IsUserDefined())
						{
							MethodSymbol methodSymbol = CS$<>8__locals1.node.MethodOpt;
							if (methodSymbol != null && methodSymbol.ParameterCount == 1)
							{
								ValueTuple<BoundExpression, Conversion> valueTuple = NullableWalker.RemoveConversion(CS$<>8__locals1.node.Operand, false);
								BoundExpression item = valueTuple.Item1;
								Conversion item2 = valueTuple.Item2;
								this.VisitRvalue(item, false);
								TypeWithState resultType = this.ResultType;
								bool flag = CS$<>8__locals1.node.OperatorKind.IsLifted();
								TypeWithState nullableUnderlyingTypeIfNecessary = NullableWalker.GetNullableUnderlyingTypeIfNecessary(flag, resultType);
								methodSymbol = (MethodSymbol)NullableWalker.AsMemberOfType(nullableUnderlyingTypeIfNecessary.Type.StrippedType(), methodSymbol);
								ParameterSymbol parameterSymbol = methodSymbol.Parameters[0];
								this.VisitConversion(CS$<>8__locals1.node.Operand as BoundConversion, item, item2, parameterSymbol.TypeWithAnnotations, nullableUnderlyingTypeIfNecessary, true, false, false, NullableWalker.AssignmentKind.Argument, parameterSymbol, true, true, false, default(Optional<NullableWalker.LocalState>), false, null);
								typeWithState = this.GetLiftedReturnTypeIfNecessary(flag, methodSymbol.ReturnTypeWithAnnotations, resultType.State);
								this.SetUpdatedSymbol(CS$<>8__locals1.node, CS$<>8__locals1.node.MethodOpt, methodSymbol);
								goto IL_0208;
							}
						}
						this.VisitRvalue(CS$<>8__locals1.node.Operand, false);
						typeWithState = NullableWalker.<VisitUnaryOperator>g__adjustForLifting|296_0(this.ResultType, ref CS$<>8__locals1);
					}
					else
					{
						this.Visit(CS$<>8__locals1.node.Operand);
						typeWithState = NullableWalker.<VisitUnaryOperator>g__adjustForLifting|296_0(this.ResultType, ref CS$<>8__locals1);
					}
				}
				else
				{
					this.Visit(CS$<>8__locals1.node.Operand);
					if (this.IsConditionalState)
					{
						base.SetConditionalState(this.StateWhenFalse, this.StateWhenTrue);
					}
					typeWithState = NullableWalker.<VisitUnaryOperator>g__adjustForLifting|296_0(this.ResultType, ref CS$<>8__locals1);
				}
			}
			else
			{
				this.Visit(CS$<>8__locals1.node.Operand);
				if (this.IsConditionalState)
				{
					base.SetConditionalState(this.StateWhenFalse, this.StateWhenTrue);
				}
				typeWithState = NullableWalker.<VisitUnaryOperator>g__adjustForLifting|296_0(this.ResultType, ref CS$<>8__locals1);
			}
			IL_0208:
			this.SetResultType(CS$<>8__locals1.node, typeWithState, true);
			return null;
		}

		// Token: 0x06001CBB RID: 7355 RVA: 0x0009A214 File Offset: 0x00098414
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitPointerIndirectionOperator(BoundPointerIndirectionOperator node)
		{
			BoundNode boundNode = base.VisitPointerIndirectionOperator(node);
			TypeWithAnnotations typeWithAnnotations = TypeWithAnnotations.Create(node.Type, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
			this.SetLvalueResultType(node, typeWithAnnotations);
			return boundNode;
		}

		// Token: 0x06001CBC RID: 7356 RVA: 0x0009A248 File Offset: 0x00098448
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitPointerElementAccess(BoundPointerElementAccess node)
		{
			BoundNode boundNode = base.VisitPointerElementAccess(node);
			TypeWithAnnotations typeWithAnnotations = TypeWithAnnotations.Create(node.Type, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
			this.SetLvalueResultType(node, typeWithAnnotations);
			return boundNode;
		}

		// Token: 0x06001CBD RID: 7357 RVA: 0x0009A27A File Offset: 0x0009847A
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitRefTypeOperator(BoundRefTypeOperator node)
		{
			this.VisitRvalue(node.Operand, false);
			this.SetNotNullResult(node);
			return null;
		}

		// Token: 0x06001CBE RID: 7358 RVA: 0x0009A291 File Offset: 0x00098491
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitMakeRefOperator(BoundMakeRefOperator node)
		{
			BoundNode boundNode = base.VisitMakeRefOperator(node);
			this.SetNotNullResult(node);
			return boundNode;
		}

		// Token: 0x06001CBF RID: 7359 RVA: 0x0009A2A4 File Offset: 0x000984A4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitRefValueOperator(BoundRefValueOperator node)
		{
			BoundNode boundNode = base.VisitRefValueOperator(node);
			TypeWithAnnotations typeWithAnnotations = TypeWithAnnotations.Create(node.Type, node.NullableAnnotation, default(ImmutableArray<CustomModifier>));
			this.SetLvalueResultType(node, typeWithAnnotations);
			return boundNode;
		}

		// Token: 0x06001CC0 RID: 7360 RVA: 0x0009A2DC File Offset: 0x000984DC
		private TypeWithState InferResultNullability(BoundUserDefinedConditionalLogicalOperator node)
		{
			if (node.OperatorKind.IsLifted())
			{
				return TypeWithState.Create(node.Type, NullableFlowState.NotNull);
			}
			if (node.LogicalOperator != null && node.LogicalOperator.ParameterCount == 2)
			{
				return this.GetReturnTypeWithState(node.LogicalOperator);
			}
			return default(TypeWithState);
		}

		// Token: 0x06001CC1 RID: 7361 RVA: 0x0009A330 File Offset: 0x00098530
		protected override void AfterLeftChildOfBinaryLogicalOperatorHasBeenVisited(BoundExpression node, BoundExpression right, bool isAnd, bool isBool, ref NullableWalker.LocalState leftTrue, ref NullableWalker.LocalState leftFalse)
		{
			TypeWithState resultType = this.ResultType;
			MethodSymbol methodSymbol = null;
			MethodSymbol methodSymbol2 = null;
			BoundExpression boundExpression = null;
			BoundKind kind = node.Kind;
			if (kind != BoundKind.BinaryOperator)
			{
				if (kind != BoundKind.UserDefinedConditionalLogicalOperator)
				{
					throw ExceptionUtilities.UnexpectedValue(node.Kind);
				}
				BoundUserDefinedConditionalLogicalOperator boundUserDefinedConditionalLogicalOperator = (BoundUserDefinedConditionalLogicalOperator)node;
				if (boundUserDefinedConditionalLogicalOperator.LogicalOperator != null && boundUserDefinedConditionalLogicalOperator.LogicalOperator.ParameterCount == 2)
				{
					methodSymbol = boundUserDefinedConditionalLogicalOperator.LogicalOperator;
					boundExpression = boundUserDefinedConditionalLogicalOperator.Left;
					methodSymbol2 = (isAnd ? boundUserDefinedConditionalLogicalOperator.FalseOperator : boundUserDefinedConditionalLogicalOperator.TrueOperator);
					if (methodSymbol2 != null && methodSymbol2.ParameterCount != 1)
					{
						methodSymbol2 = null;
					}
				}
			}
			if (methodSymbol2 != null)
			{
				this.ReportArgumentWarnings(boundExpression, resultType, methodSymbol2.Parameters[0]);
			}
			if (methodSymbol != null)
			{
				this.ReportArgumentWarnings(boundExpression, resultType, methodSymbol.Parameters[0]);
			}
			this.Visit(right);
			TypeWithState resultType2 = this.ResultType;
			this.SetResultType(node, this.InferResultNullabilityOfBinaryLogicalOperator(node, resultType, resultType2), true);
			if (methodSymbol != null)
			{
				this.ReportArgumentWarnings(right, resultType2, methodSymbol.Parameters[1]);
			}
			base.AfterRightChildOfBinaryLogicalOperatorHasBeenVisited(node, right, isAnd, isBool, ref leftTrue, ref leftFalse);
		}

		// Token: 0x06001CC2 RID: 7362 RVA: 0x0009A44C File Offset: 0x0009864C
		private TypeWithState InferResultNullabilityOfBinaryLogicalOperator(BoundExpression node, TypeWithState leftType, TypeWithState rightType)
		{
			BoundBinaryOperator boundBinaryOperator = node as BoundBinaryOperator;
			TypeWithState typeWithState;
			if (boundBinaryOperator == null)
			{
				BoundUserDefinedConditionalLogicalOperator boundUserDefinedConditionalLogicalOperator = node as BoundUserDefinedConditionalLogicalOperator;
				if (boundUserDefinedConditionalLogicalOperator == null)
				{
					throw ExceptionUtilities.UnexpectedValue(node);
				}
				typeWithState = this.InferResultNullability(boundUserDefinedConditionalLogicalOperator);
			}
			else
			{
				typeWithState = this.InferResultNullability(boundBinaryOperator.OperatorKind, boundBinaryOperator.MethodOpt, boundBinaryOperator.Type, leftType, rightType);
			}
			return typeWithState;
		}

		// Token: 0x06001CC3 RID: 7363 RVA: 0x0009A4A0 File Offset: 0x000986A0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitAwaitExpression(BoundAwaitExpression node)
		{
			BoundNode boundNode = base.VisitAwaitExpression(node);
			BoundAwaitableInfo awaitableInfo = node.AwaitableInfo;
			BoundAwaitableValuePlaceholder awaitableInstancePlaceholder = awaitableInfo.AwaitableInstancePlaceholder;
			this.EnsureAwaitablePlaceholdersInitialized();
			this._awaitablePlaceholdersOpt.Add(awaitableInstancePlaceholder, new ValueTuple<BoundExpression, NullableWalker.VisitResult>(node.Expression, this._visitResult));
			this.Visit(awaitableInfo);
			this._awaitablePlaceholdersOpt.Remove(awaitableInstancePlaceholder);
			if (node.Type.IsValueType || node.HasErrors || awaitableInfo.GetResult == null)
			{
				this.SetNotNullResult(node);
				return boundNode;
			}
			MethodSymbol getResult = awaitableInfo.GetResult;
			NamedTypeSymbol namedTypeSymbol = this._visitResult.RValueType.Type as NamedTypeSymbol;
			MethodSymbol methodSymbol = ((namedTypeSymbol != null) ? getResult.OriginalDefinition.AsMember(namedTypeSymbol) : getResult);
			this.SetResultType(node, methodSymbol.ReturnTypeWithAnnotations.ToTypeWithState(), true);
			return boundNode;
		}

		// Token: 0x06001CC4 RID: 7364 RVA: 0x0009A569 File Offset: 0x00098769
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitTypeOfOperator(BoundTypeOfOperator node)
		{
			BoundNode boundNode = base.VisitTypeOfOperator(node);
			this.SetResultType(node, TypeWithState.Create(node.Type, NullableFlowState.NotNull), true);
			return boundNode;
		}

		// Token: 0x06001CC5 RID: 7365 RVA: 0x0009A586 File Offset: 0x00098786
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitMethodInfo(BoundMethodInfo node)
		{
			BoundNode boundNode = base.VisitMethodInfo(node);
			this.SetNotNullResult(node);
			return boundNode;
		}

		// Token: 0x06001CC6 RID: 7366 RVA: 0x0009A596 File Offset: 0x00098796
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitFieldInfo(BoundFieldInfo node)
		{
			BoundNode boundNode = base.VisitFieldInfo(node);
			this.SetNotNullResult(node);
			return boundNode;
		}

		// Token: 0x06001CC7 RID: 7367 RVA: 0x0009A5A6 File Offset: 0x000987A6
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDefaultLiteral(BoundDefaultLiteral node)
		{
			BoundNode boundNode = base.VisitDefaultLiteral(node);
			this.SetResultType(node, TypeWithState.Create(node.Type, NullableFlowState.MaybeDefault), true);
			return boundNode;
		}

		// Token: 0x06001CC8 RID: 7368 RVA: 0x0009A5C4 File Offset: 0x000987C4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDefaultExpression(BoundDefaultExpression node)
		{
			BoundNode boundNode = base.VisitDefaultExpression(node);
			TypeSymbol type = node.Type;
			if (EmptyStructTypeCache.IsTrackableStructType(type))
			{
				int orCreatePlaceholderSlot = this.GetOrCreatePlaceholderSlot(node);
				if (orCreatePlaceholderSlot > 0)
				{
					this.State[orCreatePlaceholderSlot] = NullableFlowState.NotNull;
					this.InheritNullableStateOfTrackableStruct(type, orCreatePlaceholderSlot, -1, true, -1);
				}
			}
			this.SetResultType(node, TypeWithState.ForType(type), true);
			return boundNode;
		}

		// Token: 0x06001CC9 RID: 7369 RVA: 0x0009A61C File Offset: 0x0009881C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitIsOperator(BoundIsOperator node)
		{
			BoundExpression operand = node.Operand;
			BoundTypeExpression targetType = node.TargetType;
			BoundNode boundNode = base.VisitIsOperator(node);
			base.Split();
			this.LearnFromNonNullTest(operand, ref this.StateWhenTrue);
			TypeSymbol type = targetType.Type;
			if (type != null && type.SpecialType == SpecialType.System_Object)
			{
				this.LearnFromNullTest(operand, ref this.StateWhenFalse);
			}
			this.VisitTypeExpression(targetType);
			this.SetNotNullResult(node);
			return boundNode;
		}

		// Token: 0x06001CCA RID: 7370 RVA: 0x0009A684 File Offset: 0x00098884
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitAsOperator(BoundAsOperator node)
		{
			TypeWithState typeWithState = this.VisitRvalueWithState(node.Operand);
			NullableFlowState nullableFlowState = NullableFlowState.NotNull;
			TypeSymbol type = node.Type;
			if (type.CanContainNull())
			{
				ConversionKind kind = node.Conversion.Kind;
				if (kind == ConversionKind.Identity || kind == ConversionKind.ImplicitNullable || kind - ConversionKind.ImplicitReference <= 1)
				{
					nullableFlowState = typeWithState.State;
				}
				else
				{
					nullableFlowState = NullableFlowState.MaybeDefault;
				}
			}
			this.VisitTypeExpression(node.TargetType);
			this.SetResultType(node, TypeWithState.Create(type, nullableFlowState), true);
			return null;
		}

		// Token: 0x06001CCB RID: 7371 RVA: 0x0009A6F7 File Offset: 0x000988F7
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitSizeOfOperator(BoundSizeOfOperator node)
		{
			BoundNode boundNode = base.VisitSizeOfOperator(node);
			this.VisitTypeExpression(node.SourceType);
			this.SetNotNullResult(node);
			return boundNode;
		}

		// Token: 0x06001CCC RID: 7372 RVA: 0x0009A714 File Offset: 0x00098914
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitArgList(BoundArgList node)
		{
			BoundNode boundNode = base.VisitArgList(node);
			this.SetNotNullResult(node);
			return boundNode;
		}

		// Token: 0x06001CCD RID: 7373 RVA: 0x0009A724 File Offset: 0x00098924
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitArgListOperator(BoundArgListOperator node)
		{
			this.VisitArgumentsEvaluate(node.Syntax, node.Arguments, node.ArgumentRefKindsOpt, default(ImmutableArray<ParameterSymbol>), default(ImmutableArray<int>), default(BitVector), false);
			this.SetNotNullResult(node);
			return null;
		}

		// Token: 0x06001CCE RID: 7374 RVA: 0x0009A770 File Offset: 0x00098970
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitLiteral(BoundLiteral node)
		{
			BoundNode boundNode = base.VisitLiteral(node);
			TypeSymbol type = node.Type;
			TypeSymbol type2 = node.Type;
			NullableFlowState nullableFlowState;
			if (type2 == null || type2.CanContainNull())
			{
				ConstantValue constantValue = node.ConstantValue;
				if (constantValue != null && constantValue.IsNull)
				{
					nullableFlowState = NullableFlowState.MaybeDefault;
					goto IL_003B;
				}
			}
			nullableFlowState = NullableFlowState.NotNull;
			IL_003B:
			this.SetResultType(node, TypeWithState.Create(type, nullableFlowState), true);
			this.SplitIfBooleanConstant(node);
			return boundNode;
		}

		// Token: 0x06001CCF RID: 7375 RVA: 0x0009A7CA File Offset: 0x000989CA
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitPreviousSubmissionReference(BoundPreviousSubmissionReference node)
		{
			BoundNode boundNode = base.VisitPreviousSubmissionReference(node);
			this.SetNotNullResult(node);
			return boundNode;
		}

		// Token: 0x06001CD0 RID: 7376 RVA: 0x0009A7DA File Offset: 0x000989DA
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitHostObjectMemberReference(BoundHostObjectMemberReference node)
		{
			BoundNode boundNode = base.VisitHostObjectMemberReference(node);
			this.SetNotNullResult(node);
			return boundNode;
		}

		// Token: 0x06001CD1 RID: 7377 RVA: 0x0009A7EA File Offset: 0x000989EA
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitPseudoVariable(BoundPseudoVariable node)
		{
			BoundNode boundNode = base.VisitPseudoVariable(node);
			this.SetNotNullResult(node);
			return boundNode;
		}

		// Token: 0x06001CD2 RID: 7378 RVA: 0x0009A7FA File Offset: 0x000989FA
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitRangeExpression(BoundRangeExpression node)
		{
			BoundNode boundNode = base.VisitRangeExpression(node);
			this.SetNotNullResult(node);
			return boundNode;
		}

		// Token: 0x06001CD3 RID: 7379 RVA: 0x0009A80A File Offset: 0x00098A0A
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitRangeVariable(BoundRangeVariable node)
		{
			this.VisitWithoutDiagnostics(node.Value);
			this.SetNotNullResult(node);
			return null;
		}

		// Token: 0x06001CD4 RID: 7380 RVA: 0x0009A820 File Offset: 0x00098A20
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitLabel(BoundLabel node)
		{
			BoundNode boundNode = base.VisitLabel(node);
			this.SetUnknownResultNullability(node);
			return boundNode;
		}

		// Token: 0x06001CD5 RID: 7381 RVA: 0x0009A830 File Offset: 0x00098A30
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDynamicMemberAccess(BoundDynamicMemberAccess node)
		{
			BoundExpression receiver = node.Receiver;
			this.VisitRvalue(receiver, false);
			this.CheckPossibleNullReceiver(receiver, false);
			TypeWithAnnotations typeWithAnnotations = TypeWithAnnotations.Create(node.Type, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
			this.SetLvalueResultType(node, typeWithAnnotations);
			return null;
		}

		// Token: 0x06001CD6 RID: 7382 RVA: 0x0009A874 File Offset: 0x00098A74
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDynamicInvocation(BoundDynamicInvocation node)
		{
			BoundExpression expression = node.Expression;
			this.VisitRvalue(expression, false);
			BoundMethodGroup boundMethodGroup = expression as BoundMethodGroup;
			BoundExpression boundExpression = ((boundMethodGroup != null) ? boundMethodGroup.ReceiverOpt : null);
			TypeWithState typeWithState;
			if (this.TryGetMethodGroupReceiverNullability(boundExpression, out typeWithState))
			{
				this.CheckPossibleNullReceiver(boundExpression, typeWithState, false);
			}
			this.VisitArgumentsEvaluate(node.Syntax, node.Arguments, node.ArgumentRefKindsOpt, default(ImmutableArray<ParameterSymbol>), default(ImmutableArray<int>), default(BitVector), false);
			TypeWithAnnotations typeWithAnnotations = TypeWithAnnotations.Create(node.Type, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
			this.SetLvalueResultType(node, typeWithAnnotations);
			return null;
		}

		// Token: 0x06001CD7 RID: 7383 RVA: 0x0009A910 File Offset: 0x00098B10
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitEventAssignmentOperator(BoundEventAssignmentOperator node)
		{
			BoundExpression receiverOpt = node.ReceiverOpt;
			this.VisitRvalue(receiverOpt, false);
			EventSymbol eventSymbol = node.Event;
			if (!eventSymbol.IsStatic)
			{
				eventSymbol = (EventSymbol)NullableWalker.AsMemberOfType(this.ResultType.Type, eventSymbol);
				this.CheckPossibleNullReceiver(receiverOpt, false);
				this.SetUpdatedSymbol(node, node.Event, eventSymbol);
			}
			this.VisitRvalue(node.Argument, false);
			ConstantValue constantValue = node.Argument.ConstantValue;
			if (constantValue == null || !constantValue.IsNull)
			{
				int num = base.MakeMemberSlot(receiverOpt, eventSymbol);
				if (num > 0)
				{
					this.State[num] = (node.IsAddition ? this.State[num].Meet(this.ResultType.State) : NullableFlowState.MaybeNull);
				}
			}
			this.SetNotNullResult(node);
			return null;
		}

		// Token: 0x06001CD8 RID: 7384 RVA: 0x0009A9DC File Offset: 0x00098BDC
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDynamicObjectCreationExpression(BoundDynamicObjectCreationExpression node)
		{
			ImmutableArray<BoundExpression> arguments = node.Arguments;
			ImmutableArray<NullableWalker.VisitArgumentResult> immutableArray = this.VisitArgumentsEvaluate(node.Syntax, arguments, node.ArgumentRefKindsOpt, default(ImmutableArray<ParameterSymbol>), default(ImmutableArray<int>), default(BitVector), false);
			this.VisitObjectOrDynamicObjectCreation(node, arguments, immutableArray, node.InitializerExpressionOpt);
			return null;
		}

		// Token: 0x06001CD9 RID: 7385 RVA: 0x0009AA31 File Offset: 0x00098C31
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitObjectInitializerExpression(BoundObjectInitializerExpression node)
		{
			this.SetNotNullResult(node);
			return null;
		}

		// Token: 0x06001CDA RID: 7386 RVA: 0x0009AA3B File Offset: 0x00098C3B
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitCollectionInitializerExpression(BoundCollectionInitializerExpression node)
		{
			this.SetNotNullResult(node);
			return null;
		}

		// Token: 0x06001CDB RID: 7387 RVA: 0x0009AA45 File Offset: 0x00098C45
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDynamicCollectionElementInitializer(BoundDynamicCollectionElementInitializer node)
		{
			this.SetNotNullResult(node);
			return null;
		}

		// Token: 0x06001CDC RID: 7388 RVA: 0x0009AA4F File Offset: 0x00098C4F
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitImplicitReceiver(BoundImplicitReceiver node)
		{
			BoundNode boundNode = base.VisitImplicitReceiver(node);
			this.SetNotNullResult(node);
			return boundNode;
		}

		// Token: 0x06001CDD RID: 7389 RVA: 0x0009AA5F File Offset: 0x00098C5F
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitAnonymousPropertyDeclaration(BoundAnonymousPropertyDeclaration node)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06001CDE RID: 7390 RVA: 0x0009AA66 File Offset: 0x00098C66
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitNoPiaObjectCreationExpression(BoundNoPiaObjectCreationExpression node)
		{
			BoundNode boundNode = base.VisitNoPiaObjectCreationExpression(node);
			this.SetResultType(node, TypeWithState.Create(node.Type, NullableFlowState.NotNull), true);
			return boundNode;
		}

		// Token: 0x06001CDF RID: 7391 RVA: 0x0009AA83 File Offset: 0x00098C83
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitNewT(BoundNewT node)
		{
			this.VisitObjectOrDynamicObjectCreation(node, ImmutableArray<BoundExpression>.Empty, ImmutableArray<NullableWalker.VisitArgumentResult>.Empty, node.InitializerExpressionOpt);
			return null;
		}

		// Token: 0x06001CE0 RID: 7392 RVA: 0x0009AA9D File Offset: 0x00098C9D
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitArrayInitialization(BoundArrayInitialization node)
		{
			BoundNode boundNode = base.VisitArrayInitialization(node);
			this.SetNotNullResult(node);
			return boundNode;
		}

		// Token: 0x06001CE1 RID: 7393 RVA: 0x0009AAAD File Offset: 0x00098CAD
		private void SetUnknownResultNullability(BoundExpression expression)
		{
			this.SetResultType(expression, TypeWithState.Create(expression.Type, NullableFlowState.NotNull), true);
		}

		// Token: 0x06001CE2 RID: 7394 RVA: 0x0009AAC3 File Offset: 0x00098CC3
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitStackAllocArrayCreation(BoundStackAllocArrayCreation node)
		{
			BoundNode boundNode = base.VisitStackAllocArrayCreation(node);
			this.SetNotNullResult(node);
			return boundNode;
		}

		// Token: 0x06001CE3 RID: 7395 RVA: 0x0009AAD4 File Offset: 0x00098CD4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDynamicIndexerAccess(BoundDynamicIndexerAccess node)
		{
			BoundExpression receiver = node.Receiver;
			this.VisitRvalue(receiver, false);
			this.CheckPossibleNullReceiver(receiver, false);
			this.VisitArgumentsEvaluate(node.Syntax, node.Arguments, node.ArgumentRefKindsOpt, default(ImmutableArray<ParameterSymbol>), default(ImmutableArray<int>), default(BitVector), false);
			TypeWithAnnotations typeWithAnnotations = TypeWithAnnotations.Create(node.Type, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
			this.SetLvalueResultType(node, typeWithAnnotations);
			return null;
		}

		// Token: 0x06001CE4 RID: 7396 RVA: 0x0009AB4F File Offset: 0x00098D4F
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private bool CheckPossibleNullReceiver(BoundExpression receiverOpt, bool checkNullableValueType = false)
		{
			return this.CheckPossibleNullReceiver(receiverOpt, this.ResultType, checkNullableValueType);
		}

		// Token: 0x06001CE5 RID: 7397 RVA: 0x0009AB60 File Offset: 0x00098D60
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private bool CheckPossibleNullReceiver(BoundExpression receiverOpt, TypeWithState resultType, bool checkNullableValueType)
		{
			bool flag = false;
			if (receiverOpt != null && this.State.Reachable)
			{
				TypeSymbol type = resultType.Type;
				if (type == null)
				{
					return false;
				}
				if (!this.ReportPossibleNullReceiverIfNeeded(type, resultType.State, checkNullableValueType, receiverOpt.Syntax, out flag))
				{
					return flag;
				}
				this.LearnFromNonNullTest(receiverOpt, ref this.State);
			}
			return flag;
		}

		// Token: 0x06001CE6 RID: 7398 RVA: 0x0009ABB4 File Offset: 0x00098DB4
		private bool ReportPossibleNullReceiverIfNeeded(TypeSymbol type, NullableFlowState state, bool checkNullableValueType, SyntaxNode syntax, out bool reportedDiagnostic)
		{
			reportedDiagnostic = false;
			if (state.MayBeNull())
			{
				bool isValueType = type.IsValueType;
				if (isValueType && (!checkNullableValueType || !type.IsNullableTypeOrTypeParameter() || type.GetNullableUnderlyingType().IsErrorType()))
				{
					return false;
				}
				this.ReportDiagnostic(isValueType ? ErrorCode.WRN_NullableValueTypeMayBeNull : ErrorCode.WRN_NullReferenceReceiver, syntax, Array.Empty<object>());
				reportedDiagnostic = true;
			}
			return true;
		}

		// Token: 0x06001CE7 RID: 7399 RVA: 0x0009AC14 File Offset: 0x00098E14
		private void CheckExtensionMethodThisNullability(BoundExpression expr, Conversion conversion, ParameterSymbol parameter, TypeWithState result)
		{
			this.VisitArgumentConversionAndInboundAssignmentsAndPreConditions(null, expr, conversion, parameter.RefKind, parameter, parameter.TypeWithAnnotations, this.GetParameterAnnotations(parameter), new NullableWalker.VisitArgumentResult(new NullableWalker.VisitResult(result, result.ToTypeWithAnnotations(this.compilation, false)), default(Optional<NullableWalker.LocalState>)), true);
		}

		// Token: 0x06001CE8 RID: 7400 RVA: 0x0009AC62 File Offset: 0x00098E62
		private static bool IsNullabilityMismatch(TypeWithAnnotations type1, TypeWithAnnotations type2)
		{
			return type1.Equals(type2, TypeCompareKind.AllIgnoreOptions) && !type1.Equals(type2, TypeCompareKind.IgnoreCustomModifiersAndArraySizesAndLowerBounds | TypeCompareKind.IgnoreDynamic | TypeCompareKind.IgnoreTupleNames | TypeCompareKind.ObliviousNullableModifierMatchesAny | TypeCompareKind.IgnoreNativeIntegers);
		}

		// Token: 0x06001CE9 RID: 7401 RVA: 0x0009AC7F File Offset: 0x00098E7F
		private static bool IsNullabilityMismatch(TypeSymbol type1, TypeSymbol type2)
		{
			return type1.Equals(type2, TypeCompareKind.AllIgnoreOptions) && !type1.Equals(type2, TypeCompareKind.IgnoreCustomModifiersAndArraySizesAndLowerBounds | TypeCompareKind.IgnoreDynamic | TypeCompareKind.IgnoreTupleNames | TypeCompareKind.ObliviousNullableModifierMatchesAny | TypeCompareKind.IgnoreNativeIntegers);
		}

		// Token: 0x06001CEA RID: 7402 RVA: 0x0009AC9A File Offset: 0x00098E9A
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitQueryClause(BoundQueryClause node)
		{
			BoundNode boundNode = base.VisitQueryClause(node);
			this.SetNotNullResult(node);
			return boundNode;
		}

		// Token: 0x06001CEB RID: 7403 RVA: 0x0009ACAA File Offset: 0x00098EAA
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitNameOfOperator(BoundNameOfOperator node)
		{
			BoundNode boundNode = base.VisitNameOfOperator(node);
			this.SetResultType(node, TypeWithState.Create(node.Type, NullableFlowState.NotNull), true);
			return boundNode;
		}

		// Token: 0x06001CEC RID: 7404 RVA: 0x0009ACC7 File Offset: 0x00098EC7
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitNamespaceExpression(BoundNamespaceExpression node)
		{
			BoundNode boundNode = base.VisitNamespaceExpression(node);
			this.SetUnknownResultNullability(node);
			return boundNode;
		}

		// Token: 0x06001CED RID: 7405 RVA: 0x0009ACD7 File Offset: 0x00098ED7
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitInterpolatedString(BoundInterpolatedString node)
		{
			BoundNode boundNode = base.VisitInterpolatedString(node);
			this.SetResultType(node, TypeWithState.Create(node.Type, NullableFlowState.NotNull), true);
			return boundNode;
		}

		// Token: 0x06001CEE RID: 7406 RVA: 0x0009ACF4 File Offset: 0x00098EF4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitUnconvertedInterpolatedString(BoundUnconvertedInterpolatedString node)
		{
			BoundNode boundNode = base.VisitUnconvertedInterpolatedString(node);
			this.SetResultType(node, TypeWithState.Create(node.Type, NullableFlowState.NotNull), true);
			return boundNode;
		}

		// Token: 0x06001CEF RID: 7407 RVA: 0x0009AD11 File Offset: 0x00098F11
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitStringInsert(BoundStringInsert node)
		{
			BoundNode boundNode = base.VisitStringInsert(node);
			this.SetUnknownResultNullability(node);
			return boundNode;
		}

		// Token: 0x06001CF0 RID: 7408 RVA: 0x0009AD21 File Offset: 0x00098F21
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitConvertedStackAllocExpression(BoundConvertedStackAllocExpression node)
		{
			BoundNode boundNode = base.VisitConvertedStackAllocExpression(node);
			this.SetNotNullResult(node);
			return boundNode;
		}

		// Token: 0x06001CF1 RID: 7409 RVA: 0x0009AD34 File Offset: 0x00098F34
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDiscardExpression(BoundDiscardExpression node)
		{
			TypeWithAnnotations typeWithAnnotations = TypeWithAnnotations.Create(node.Type, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
			TypeWithState typeWithState = TypeWithState.ForType(node.Type);
			this.SetResult(node, typeWithState, typeWithAnnotations, true, default(bool?));
			return null;
		}

		// Token: 0x06001CF2 RID: 7410 RVA: 0x0009AD78 File Offset: 0x00098F78
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitThrowExpression(BoundThrowExpression node)
		{
			this.VisitThrow(node.Expression);
			this.SetResultType(node, default(TypeWithState), true);
			return null;
		}

		// Token: 0x06001CF3 RID: 7411 RVA: 0x0009ADA3 File Offset: 0x00098FA3
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitThrowStatement(BoundThrowStatement node)
		{
			this.VisitThrow(node.ExpressionOpt);
			return null;
		}

		// Token: 0x06001CF4 RID: 7412 RVA: 0x0009ADB4 File Offset: 0x00098FB4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private void VisitThrow(BoundExpression expr)
		{
			if (expr != null && this.VisitRvalueWithState(expr).MayBeNull)
			{
				this.ReportDiagnostic(ErrorCode.WRN_ThrowPossibleNull, expr.Syntax, Array.Empty<object>());
			}
			base.SetUnreachable();
		}

		// Token: 0x06001CF5 RID: 7413 RVA: 0x0009ADF4 File Offset: 0x00098FF4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitYieldReturnStatement(BoundYieldReturnStatement node)
		{
			BoundExpression expression = node.Expression;
			if (expression == null)
			{
				return null;
			}
			MethodSymbol methodSymbol = (MethodSymbol)this.CurrentSymbol;
			TypeWithAnnotations iteratorElementTypeFromReturnType = InMethodBinder.GetIteratorElementTypeFromReturnType(this.compilation, RefKind.None, methodSymbol.ReturnType, null, null);
			this.VisitOptionalImplicitConversion(expression, iteratorElementTypeFromReturnType, false, false, NullableWalker.AssignmentKind.Return);
			return null;
		}

		// Token: 0x06001CF6 RID: 7414 RVA: 0x0009AE3C File Offset: 0x0009903C
		protected override void VisitCatchBlock(BoundCatchBlock node, ref NullableWalker.LocalState finallyState)
		{
			this.TakeIncrementalSnapshot(node);
			if (node.Locals.Length > 0)
			{
				LocalSymbol localSymbol = node.Locals[0];
				if (localSymbol.DeclarationKind == LocalDeclarationKind.CatchVariable)
				{
					int orCreateSlot = this.GetOrCreateSlot(localSymbol, 0, false, true);
					if (orCreateSlot > 0)
					{
						this.State[orCreateSlot] = NullableFlowState.NotNull;
					}
				}
			}
			if (node.ExceptionSourceOpt != null)
			{
				this.VisitWithoutDiagnostics(node.ExceptionSourceOpt);
			}
			base.VisitCatchBlock(node, ref finallyState);
		}

		// Token: 0x06001CF7 RID: 7415 RVA: 0x0009AEB2 File Offset: 0x000990B2
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitLockStatement(BoundLockStatement node)
		{
			this.VisitRvalue(node.Argument, false);
			this.CheckPossibleNullReceiver(node.Argument, false);
			this.VisitStatement(node.Body);
			return null;
		}

		// Token: 0x06001CF8 RID: 7416 RVA: 0x0009AEDC File Offset: 0x000990DC
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitAttribute(BoundAttribute node)
		{
			this.VisitArguments(node, node.ConstructorArguments, ImmutableArray<RefKind>.Empty, node.Constructor, node.ConstructorArgumentsToParamsOpt, default(BitVector), node.ConstructorExpanded, false);
			foreach (BoundAssignmentOperator boundAssignmentOperator in node.NamedArguments)
			{
				this.Visit(boundAssignmentOperator);
			}
			this.SetNotNullResult(node);
			return null;
		}

		// Token: 0x06001CF9 RID: 7417 RVA: 0x0009AF4C File Offset: 0x0009914C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitExpressionWithNullability(BoundExpressionWithNullability node)
		{
			TypeWithAnnotations typeWithAnnotations = TypeWithAnnotations.Create(node.Type, node.NullableAnnotation, default(ImmutableArray<CustomModifier>));
			this.SetResult(node.Expression, typeWithAnnotations.ToTypeWithState(), typeWithAnnotations, true, default(bool?));
			return null;
		}

		// Token: 0x06001CFA RID: 7418 RVA: 0x0009AF93 File Offset: 0x00099193
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDeconstructValuePlaceholder(BoundDeconstructValuePlaceholder node)
		{
			this.SetNotNullResult(node);
			return null;
		}

		// Token: 0x06001CFB RID: 7419 RVA: 0x0009AF9D File Offset: 0x0009919D
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitObjectOrCollectionValuePlaceholder(BoundObjectOrCollectionValuePlaceholder node)
		{
			this.SetNotNullResult(node);
			return null;
		}

		// Token: 0x06001CFC RID: 7420 RVA: 0x0009AFA7 File Offset: 0x000991A7
		[MemberNotNull("_awaitablePlaceholdersOpt")]
		private void EnsureAwaitablePlaceholdersInitialized()
		{
			if (this._awaitablePlaceholdersOpt == null)
			{
				this._awaitablePlaceholdersOpt = PooledDictionary<BoundAwaitableValuePlaceholder, ValueTuple<BoundExpression, NullableWalker.VisitResult>>.GetInstance();
			}
		}

		// Token: 0x06001CFD RID: 7421 RVA: 0x0009AFBC File Offset: 0x000991BC
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitAwaitableValuePlaceholder(BoundAwaitableValuePlaceholder node)
		{
			ValueTuple<BoundExpression, NullableWalker.VisitResult> valueTuple;
			if (this._awaitablePlaceholdersOpt != null && this._awaitablePlaceholdersOpt.TryGetValue(node, ref valueTuple))
			{
				NullableWalker.VisitResult item = valueTuple.Item2;
				this.SetResult(node, item.RValueType, item.LValueType, true, default(bool?));
			}
			else
			{
				this.SetNotNullResult(node);
			}
			return null;
		}

		// Token: 0x06001CFE RID: 7422 RVA: 0x0009B00F File Offset: 0x0009920F
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitAwaitableInfo(BoundAwaitableInfo node)
		{
			this.Visit(node.AwaitableInstancePlaceholder);
			this.Visit(node.GetAwaiter);
			return null;
		}

		// Token: 0x06001CFF RID: 7423 RVA: 0x0009B02C File Offset: 0x0009922C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitFunctionPointerInvocation(BoundFunctionPointerInvocation node)
		{
			this.Visit(node.InvokedExpression);
			this.VisitArguments(node, node.Arguments, node.ArgumentRefKindsOpt, node.FunctionPointer.Signature, default(ImmutableArray<int>), default(BitVector), false, false);
			TypeWithAnnotations returnTypeWithAnnotations = node.FunctionPointer.Signature.ReturnTypeWithAnnotations;
			this.SetResult(node, returnTypeWithAnnotations.ToTypeWithState(), returnTypeWithAnnotations, true, default(bool?));
			return null;
		}

		// Token: 0x06001D00 RID: 7424 RVA: 0x0009B0A4 File Offset: 0x000992A4
		protected override string Dump(NullableWalker.LocalState state)
		{
			return state.Dump(this._variables);
		}

		// Token: 0x06001D01 RID: 7425 RVA: 0x0009B0B3 File Offset: 0x000992B3
		protected override bool Meet(ref NullableWalker.LocalState self, ref NullableWalker.LocalState other)
		{
			if (!self.Reachable)
			{
				return false;
			}
			if (!other.Reachable)
			{
				self = other.Clone();
				return true;
			}
			this.Normalize(ref self);
			this.Normalize(ref other);
			return self.Meet(in other);
		}

		// Token: 0x06001D02 RID: 7426 RVA: 0x0009B0EA File Offset: 0x000992EA
		protected override bool Join(ref NullableWalker.LocalState self, ref NullableWalker.LocalState other)
		{
			if (!other.Reachable)
			{
				return false;
			}
			if (!self.Reachable)
			{
				self = other.Clone();
				return true;
			}
			this.Normalize(ref self);
			this.Normalize(ref other);
			return self.Join(in other);
		}

		// Token: 0x06001D03 RID: 7427 RVA: 0x0009B124 File Offset: 0x00099324
		protected override NullableWalker.LocalFunctionState CreateLocalFunctionState(LocalFunctionSymbol symbol)
		{
			MethodSymbol methodSymbol = symbol.ContainingSymbol as MethodSymbol;
			return new NullableWalker.LocalFunctionState(NullableWalker.LocalState.UnreachableState(((methodSymbol != null) ? this._variables.GetVariablesForMethodScope(methodSymbol) : null) ?? this._variables.GetRootScope()));
		}

		// Token: 0x06001D04 RID: 7428 RVA: 0x0009B168 File Offset: 0x00099368
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private void LearnFromAnyNullPatterns(BoundExpression expression, BoundPattern pattern)
		{
			int num = this.MakeSlot(expression);
			this.LearnFromAnyNullPatterns(num, expression.Type, pattern);
		}

		// Token: 0x06001D05 RID: 7429 RVA: 0x0009B18C File Offset: 0x0009938C
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private void VisitPatternForRewriting(BoundPattern pattern)
		{
			NullableWalker.LocalState state = this.State;
			this.VisitWithoutDiagnostics(pattern);
			base.SetState(state);
		}

		// Token: 0x06001D06 RID: 7430 RVA: 0x0009B1AE File Offset: 0x000993AE
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public override BoundNode VisitSubpattern(BoundSubpattern node)
		{
			this.Visit(node.Pattern);
			return null;
		}

		// Token: 0x06001D07 RID: 7431 RVA: 0x0009B1BE File Offset: 0x000993BE
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public override BoundNode VisitRecursivePattern(BoundRecursivePattern node)
		{
			this.Visit(node.DeclaredType);
			this.VisitAndUnsplitAll<BoundSubpattern>(node.Deconstruction);
			this.VisitAndUnsplitAll<BoundSubpattern>(node.Properties);
			this.Visit(node.VariableAccess);
			return null;
		}

		// Token: 0x06001D08 RID: 7432 RVA: 0x0009B1F3 File Offset: 0x000993F3
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public override BoundNode VisitConstantPattern(BoundConstantPattern node)
		{
			this.Visit(node.Value);
			return null;
		}

		// Token: 0x06001D09 RID: 7433 RVA: 0x0009B203 File Offset: 0x00099403
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public override BoundNode VisitDeclarationPattern(BoundDeclarationPattern node)
		{
			this.Visit(node.VariableAccess);
			this.Visit(node.DeclaredType);
			return null;
		}

		// Token: 0x06001D0A RID: 7434 RVA: 0x0009B220 File Offset: 0x00099420
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public override BoundNode VisitDiscardPattern(BoundDiscardPattern node)
		{
			return null;
		}

		// Token: 0x06001D0B RID: 7435 RVA: 0x0009B223 File Offset: 0x00099423
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public override BoundNode VisitTypePattern(BoundTypePattern node)
		{
			this.Visit(node.DeclaredType);
			return null;
		}

		// Token: 0x06001D0C RID: 7436 RVA: 0x0009B233 File Offset: 0x00099433
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public override BoundNode VisitRelationalPattern(BoundRelationalPattern node)
		{
			this.Visit(node.Value);
			return null;
		}

		// Token: 0x06001D0D RID: 7437 RVA: 0x0009B243 File Offset: 0x00099443
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public override BoundNode VisitNegatedPattern(BoundNegatedPattern node)
		{
			this.Visit(node.Negated);
			return null;
		}

		// Token: 0x06001D0E RID: 7438 RVA: 0x0009B253 File Offset: 0x00099453
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public override BoundNode VisitBinaryPattern(BoundBinaryPattern node)
		{
			this.Visit(node.Left);
			this.Visit(node.Right);
			return null;
		}

		// Token: 0x06001D0F RID: 7439 RVA: 0x0009B270 File Offset: 0x00099470
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public override BoundNode VisitITuplePattern(BoundITuplePattern node)
		{
			this.VisitAndUnsplitAll<BoundSubpattern>(node.Subpatterns);
			return null;
		}

		// Token: 0x06001D10 RID: 7440 RVA: 0x0009B280 File Offset: 0x00099480
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private void LearnFromAnyNullPatterns(int inputSlot, TypeSymbol inputType, BoundPattern pattern)
		{
			if (inputSlot <= 0)
			{
				return;
			}
			this.VisitPatternForRewriting(pattern);
			BoundConstantPattern boundConstantPattern = pattern as BoundConstantPattern;
			if (boundConstantPattern == null)
			{
				if (!(pattern is BoundDeclarationPattern) && !(pattern is BoundDiscardPattern) && !(pattern is BoundITuplePattern) && !(pattern is BoundRelationalPattern))
				{
					BoundTypePattern boundTypePattern = pattern as BoundTypePattern;
					if (boundTypePattern == null)
					{
						BoundRecursivePattern boundRecursivePattern = pattern as BoundRecursivePattern;
						if (boundRecursivePattern == null)
						{
							BoundNegatedPattern boundNegatedPattern = pattern as BoundNegatedPattern;
							if (boundNegatedPattern != null)
							{
								this.LearnFromAnyNullPatterns(inputSlot, inputType, boundNegatedPattern.Negated);
								return;
							}
							BoundBinaryPattern boundBinaryPattern = pattern as BoundBinaryPattern;
							if (boundBinaryPattern == null)
							{
								throw ExceptionUtilities.UnexpectedValue(pattern);
							}
							this.LearnFromAnyNullPatterns(inputSlot, inputType, boundBinaryPattern.Left);
							this.LearnFromAnyNullPatterns(inputSlot, inputType, boundBinaryPattern.Right);
							return;
						}
						else
						{
							if (boundRecursivePattern.IsExplicitNotNullTest)
							{
								this.LearnFromNullTest(inputSlot, inputType, ref this.State, false);
							}
							if (boundRecursivePattern.DeconstructMethod == null && !boundRecursivePattern.Deconstruction.IsDefault)
							{
								ImmutableArray<FieldSymbol> tupleElements = inputType.TupleElements;
								int i = 0;
								int num = Math.Min(boundRecursivePattern.Deconstruction.Length, tupleElements.IsDefault ? 0 : tupleElements.Length);
								while (i < num)
								{
									BoundSubpattern boundSubpattern = boundRecursivePattern.Deconstruction[i];
									FieldSymbol fieldSymbol = tupleElements[i];
									this.LearnFromAnyNullPatterns(this.GetOrCreateSlot(fieldSymbol, inputSlot, false, true), fieldSymbol.Type, boundSubpattern.Pattern);
									i++;
								}
							}
							if (!boundRecursivePattern.Properties.IsDefault)
							{
								int j = 0;
								int length = boundRecursivePattern.Properties.Length;
								while (j < length)
								{
									BoundSubpattern boundSubpattern2 = boundRecursivePattern.Properties[j];
									Symbol symbol = boundSubpattern2.Symbol;
									if (symbol != null && symbol.ContainingType.Equals(inputType, TypeCompareKind.AllIgnoreOptions))
									{
										this.LearnFromAnyNullPatterns(this.GetOrCreateSlot(symbol, inputSlot, false, true), symbol.GetTypeOrReturnType().Type, boundSubpattern2.Pattern);
									}
									j++;
								}
								return;
							}
						}
					}
					else if (boundTypePattern.IsExplicitNotNullTest)
					{
						this.LearnFromNullTest(inputSlot, inputType, ref this.State, false);
						return;
					}
				}
			}
			else if (boundConstantPattern.Value.ConstantValue == ConstantValue.Null)
			{
				this.LearnFromNullTest(inputSlot, inputType, ref this.State, false);
				return;
			}
		}

		// Token: 0x06001D11 RID: 7441 RVA: 0x0009B4C0 File Offset: 0x000996C0
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		protected override NullableWalker.LocalState VisitSwitchStatementDispatch(BoundSwitchStatement node)
		{
			int num = (node.Expression.IsSuppressed ? this.GetOrCreatePlaceholderSlot(node.Expression) : this.MakeSlot(node.Expression));
			if (num > 0)
			{
				TypeSymbol type = node.Expression.Type;
				foreach (BoundSwitchSection boundSwitchSection in node.SwitchSections)
				{
					foreach (BoundSwitchLabel boundSwitchLabel in boundSwitchSection.SwitchLabels)
					{
						this.LearnFromAnyNullPatterns(num, type, boundSwitchLabel.Pattern);
					}
				}
			}
			this.Visit(node.Expression);
			TypeWithState resultType = this.ResultType;
			this.DeclareLocals(node.InnerLocals);
			foreach (BoundSwitchSection boundSwitchSection2 in node.SwitchSections)
			{
				this.DeclareLocals(boundSwitchSection2.Locals);
			}
			PooledDictionary<LabelSymbol, ValueTuple<NullableWalker.LocalState, bool>> pooledDictionary = this.LearnFromDecisionDag(node.Syntax, node.DecisionDag, node.Expression, resultType);
			foreach (BoundSwitchSection boundSwitchSection3 in node.SwitchSections)
			{
				foreach (BoundSwitchLabel boundSwitchLabel2 in boundSwitchSection3.SwitchLabels)
				{
					ValueTuple<NullableWalker.LocalState, bool> valueTuple2;
					ValueTuple<NullableWalker.LocalState, bool> valueTuple = (pooledDictionary.TryGetValue(boundSwitchLabel2.Label, ref valueTuple2) ? valueTuple2 : new ValueTuple<NullableWalker.LocalState, bool>(this.UnreachableState(), false));
					base.SetState(valueTuple.Item1);
					base.PendingBranches.Add(new AbstractFlowPass<NullableWalker.LocalState, NullableWalker.LocalFunctionState>.PendingBranch(boundSwitchLabel2, this.State, boundSwitchLabel2.Label, false, default(NullableWalker.LocalState), default(NullableWalker.LocalState)));
				}
			}
			ValueTuple<NullableWalker.LocalState, bool> valueTuple3;
			NullableWalker.LocalState localState = (pooledDictionary.TryGetValue(node.BreakLabel, ref valueTuple3) ? valueTuple3.Item1 : this.UnreachableState());
			pooledDictionary.Free();
			return localState;
		}

		// Token: 0x06001D12 RID: 7442 RVA: 0x0009B6A0 File Offset: 0x000998A0
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		protected override void VisitSwitchSection(BoundSwitchSection node, bool isLastSection)
		{
			this.TakeIncrementalSnapshot(node);
			base.SetState(this.UnreachableState());
			foreach (BoundSwitchLabel boundSwitchLabel in node.SwitchLabels)
			{
				this.TakeIncrementalSnapshot(boundSwitchLabel);
				this.VisitPatternForRewriting(boundSwitchLabel.Pattern);
				base.VisitLabel(boundSwitchLabel.Label, node);
			}
			this.VisitStatementList(node);
		}

		// Token: 0x06001D13 RID: 7443 RVA: 0x0009B70C File Offset: 0x0009990C
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[return: TupleElementNames(new string[] { "state", "believedReachable" })]
		private PooledDictionary<LabelSymbol, ValueTuple<NullableWalker.LocalState, bool>> LearnFromDecisionDag(SyntaxNode node, BoundDecisionDag decisionDag, BoundExpression expression, TypeWithState expressionType)
		{
			NullableWalker.<>c__DisplayClass397_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.node = node;
			BoundDagTemp boundDagTemp = BoundDagTemp.ForOriginalInput(expression);
			int num = this.MakeSlot(expression);
			if (num <= 0)
			{
				num = this.<LearnFromDecisionDag>g__makeDagTempSlot|397_6(expressionType.ToTypeWithAnnotations(this.compilation, false), boundDagTemp, ref CS$<>8__locals1);
			}
			BoundTupleExpression boundTupleExpression = expression as BoundTupleExpression;
			ImmutableArray<int> immutableArray;
			if (boundTupleExpression == null)
			{
				immutableArray = default(ImmutableArray<int>);
			}
			else
			{
				immutableArray = boundTupleExpression.Arguments.SelectAsArray((BoundExpression a, NullableWalker w) => w.MakeSlot(a), this);
			}
			ImmutableArray<int> immutableArray2 = immutableArray;
			CS$<>8__locals1.originalInputMap = PooledDictionary<int, BoundExpression>.GetInstance();
			CS$<>8__locals1.originalInputMap.Add(num, expression);
			CS$<>8__locals1.tempMap = PooledDictionary<BoundDagTemp, ValueTuple<int, TypeSymbol>>.GetInstance();
			CS$<>8__locals1.tempMap.Add(boundDagTemp, new ValueTuple<int, TypeSymbol>(num, expressionType.Type));
			CS$<>8__locals1.nodeStateMap = PooledDictionary<BoundDecisionDagNode, ValueTuple<NullableWalker.PossiblyConditionalState, bool>>.GetInstance();
			CS$<>8__locals1.nodeStateMap.Add(decisionDag.RootNode, new ValueTuple<NullableWalker.PossiblyConditionalState, bool>(NullableWalker.PossiblyConditionalState.Create(this), true));
			PooledDictionary<LabelSymbol, ValueTuple<NullableWalker.LocalState, bool>> instance = PooledDictionary<LabelSymbol, ValueTuple<NullableWalker.LocalState, bool>>.GetInstance();
			foreach (BoundDecisionDagNode boundDecisionDagNode in decisionDag.TopologicallySortedNodes)
			{
				ValueTuple<NullableWalker.PossiblyConditionalState, bool> valueTuple;
				CS$<>8__locals1.nodeStateMap.TryGetValue(boundDecisionDagNode, ref valueTuple);
				ValueTuple<NullableWalker.PossiblyConditionalState, bool> valueTuple2 = valueTuple;
				NullableWalker.PossiblyConditionalState item = valueTuple2.Item1;
				bool item2 = valueTuple2.Item2;
				if (item.IsConditionalState)
				{
					base.SetConditionalState(item.StateWhenTrue, item.StateWhenFalse);
				}
				else
				{
					base.SetState(item.State);
				}
				BoundEvaluationDecisionDagNode boundEvaluationDecisionDagNode = boundDecisionDagNode as BoundEvaluationDecisionDagNode;
				if (boundEvaluationDecisionDagNode == null)
				{
					BoundTestDecisionDagNode boundTestDecisionDagNode = boundDecisionDagNode as BoundTestDecisionDagNode;
					if (boundTestDecisionDagNode == null)
					{
						BoundLeafDecisionDagNode boundLeafDecisionDagNode = boundDecisionDagNode as BoundLeafDecisionDagNode;
						if (boundLeafDecisionDagNode == null)
						{
							BoundWhenDecisionDagNode boundWhenDecisionDagNode = boundDecisionDagNode as BoundWhenDecisionDagNode;
							if (boundWhenDecisionDagNode == null)
							{
								throw ExceptionUtilities.UnexpectedValue(boundDecisionDagNode.Kind);
							}
							base.Unsplit();
							foreach (BoundPatternBinding boundPatternBinding in boundWhenDecisionDagNode.Bindings)
							{
								BoundExpression variableAccess = boundPatternBinding.VariableAccess;
								BoundDagTemp tempContainingValue = boundPatternBinding.TempContainingValue;
								ValueTuple<int, TypeSymbol> valueTuple3;
								if (CS$<>8__locals1.tempMap.TryGetValue(tempContainingValue, ref valueTuple3))
								{
									ValueTuple<int, TypeSymbol> valueTuple4 = valueTuple3;
									int item3 = valueTuple4.Item1;
									TypeSymbol item4 = valueTuple4.Item2;
									NullableFlowState nullableFlowState = this.State[item3];
									BoundLocal boundLocal = variableAccess as BoundLocal;
									if (boundLocal != null)
									{
										SourceLocalSymbol sourceLocalSymbol = boundLocal.LocalSymbol as SourceLocalSymbol;
										if (sourceLocalSymbol != null)
										{
											TypeWithAnnotations typeWithAnnotations = TypeWithState.Create(item4, nullableFlowState).ToTypeWithAnnotations(this.compilation, boundLocal.DeclarationKind == BoundLocalDeclarationKind.WithInferredType);
											TypeWithAnnotations typeWithAnnotations2;
											if (this._variables.TryGetType(sourceLocalSymbol, out typeWithAnnotations2))
											{
												typeWithAnnotations = TypeWithAnnotations.Create(typeWithAnnotations.Type, typeWithAnnotations2.NullableAnnotation.Join(typeWithAnnotations.NullableAnnotation), default(ImmutableArray<CustomModifier>));
											}
											this._variables.SetType(sourceLocalSymbol, typeWithAnnotations);
											int orCreateSlot = this.GetOrCreateSlot(sourceLocalSymbol, 0, true, true);
											if (orCreateSlot > 0)
											{
												this.TrackNullableStateForAssignment(null, typeWithAnnotations, orCreateSlot, TypeWithState.Create(item4, nullableFlowState), item3);
											}
										}
									}
								}
							}
							if (boundWhenDecisionDagNode.WhenExpression != null && boundWhenDecisionDagNode.WhenExpression.ConstantValue != ConstantValue.True)
							{
								base.VisitCondition(boundWhenDecisionDagNode.WhenExpression);
								this.<LearnFromDecisionDag>g__gotoNode|397_5(boundWhenDecisionDagNode.WhenTrue, this.StateWhenTrue, item2, ref CS$<>8__locals1);
								this.<LearnFromDecisionDag>g__gotoNode|397_5(boundWhenDecisionDagNode.WhenFalse, this.StateWhenFalse, item2, ref CS$<>8__locals1);
							}
							else
							{
								this.<LearnFromDecisionDag>g__gotoNode|397_5(boundWhenDecisionDagNode.WhenTrue, this.State, item2, ref CS$<>8__locals1);
							}
						}
						else
						{
							base.Unsplit();
							instance.Add(boundLeafDecisionDagNode.Label, new ValueTuple<NullableWalker.LocalState, bool>(this.State, item2));
						}
					}
					else
					{
						BoundDagTest test = boundTestDecisionDagNode.Test;
						ValueTuple<int, TypeSymbol> valueTuple5;
						CS$<>8__locals1.tempMap.TryGetValue(test.Input, ref valueTuple5);
						ValueTuple<int, TypeSymbol> valueTuple6 = valueTuple5;
						int item5 = valueTuple6.Item1;
						TypeSymbol item6 = valueTuple6.Item2;
						base.Split();
						if (!(test is BoundDagTypeTest))
						{
							BoundDagNonNullTest boundDagNonNullTest = test as BoundDagNonNullTest;
							if (boundDagNonNullTest == null)
							{
								if (!(test is BoundDagExplicitNullTest))
								{
									BoundDagValueTest boundDagValueTest = test as BoundDagValueTest;
									if (boundDagValueTest == null)
									{
										if (!(test is BoundDagRelationalTest))
										{
											throw ExceptionUtilities.UnexpectedValue(test.Kind);
										}
										if (item5 > 0)
										{
											this.<LearnFromDecisionDag>g__learnFromNonNullTest|397_1(item5, ref this.StateWhenTrue, ref CS$<>8__locals1);
										}
										this.<LearnFromDecisionDag>g__gotoNode|397_5(boundTestDecisionDagNode.WhenTrue, this.StateWhenTrue, item2, ref CS$<>8__locals1);
										this.<LearnFromDecisionDag>g__gotoNode|397_5(boundTestDecisionDagNode.WhenFalse, this.StateWhenFalse, item2, ref CS$<>8__locals1);
									}
									else
									{
										if (item5 > 0)
										{
											this.<LearnFromDecisionDag>g__learnFromNonNullTest|397_1(item5, ref this.StateWhenTrue, ref CS$<>8__locals1);
										}
										bool flag = boundDagValueTest.Value == ConstantValue.False;
										this.<LearnFromDecisionDag>g__gotoNode|397_5(boundTestDecisionDagNode.WhenTrue, flag ? this.StateWhenFalse : this.StateWhenTrue, item2, ref CS$<>8__locals1);
										this.<LearnFromDecisionDag>g__gotoNode|397_5(boundTestDecisionDagNode.WhenFalse, flag ? this.StateWhenTrue : this.StateWhenFalse, item2, ref CS$<>8__locals1);
									}
								}
								else
								{
									if (item5 > 0)
									{
										this.LearnFromNullTest(item5, item6, ref this.StateWhenTrue, true);
										this.<LearnFromDecisionDag>g__learnFromNonNullTest|397_1(item5, ref this.StateWhenFalse, ref CS$<>8__locals1);
									}
									this.<LearnFromDecisionDag>g__gotoNode|397_5(boundTestDecisionDagNode.WhenTrue, this.StateWhenTrue, item2, ref CS$<>8__locals1);
									this.<LearnFromDecisionDag>g__gotoNode|397_5(boundTestDecisionDagNode.WhenFalse, this.StateWhenFalse, item2, ref CS$<>8__locals1);
								}
							}
							else
							{
								bool flag2 = this.StateWhenTrue[item5].MayBeNull();
								if (item5 > 0)
								{
									this.MarkDependentSlotsNotNull(item5, item6, ref this.StateWhenFalse, 2);
									if (boundDagNonNullTest.IsExplicitTest)
									{
										this.LearnFromNullTest(item5, item6, ref this.StateWhenFalse, false);
									}
									this.<LearnFromDecisionDag>g__learnFromNonNullTest|397_1(item5, ref this.StateWhenTrue, ref CS$<>8__locals1);
								}
								this.<LearnFromDecisionDag>g__gotoNode|397_5(boundTestDecisionDagNode.WhenTrue, this.StateWhenTrue, item2, ref CS$<>8__locals1);
								this.<LearnFromDecisionDag>g__gotoNode|397_5(boundTestDecisionDagNode.WhenFalse, this.StateWhenFalse, item2 && flag2, ref CS$<>8__locals1);
							}
						}
						else
						{
							if (item5 > 0)
							{
								this.<LearnFromDecisionDag>g__learnFromNonNullTest|397_1(item5, ref this.StateWhenTrue, ref CS$<>8__locals1);
							}
							this.<LearnFromDecisionDag>g__gotoNode|397_5(boundTestDecisionDagNode.WhenTrue, this.StateWhenTrue, item2, ref CS$<>8__locals1);
							this.<LearnFromDecisionDag>g__gotoNode|397_5(boundTestDecisionDagNode.WhenFalse, this.StateWhenFalse, item2, ref CS$<>8__locals1);
						}
					}
				}
				else
				{
					BoundDagEvaluation evaluation = boundEvaluationDecisionDagNode.Evaluation;
					ValueTuple<int, TypeSymbol> valueTuple7;
					if (!CS$<>8__locals1.tempMap.TryGetValue(evaluation.Input, ref valueTuple7))
					{
						throw ExceptionUtilities.Unreachable;
					}
					ValueTuple<int, TypeSymbol> valueTuple8 = valueTuple7;
					int item7 = valueTuple8.Item1;
					TypeSymbol item8 = valueTuple8.Item2;
					BoundDagDeconstructEvaluation boundDagDeconstructEvaluation = evaluation as BoundDagDeconstructEvaluation;
					if (boundDagDeconstructEvaluation == null)
					{
						BoundDagTypeEvaluation boundDagTypeEvaluation = evaluation as BoundDagTypeEvaluation;
						if (boundDagTypeEvaluation == null)
						{
							BoundDagFieldEvaluation boundDagFieldEvaluation = evaluation as BoundDagFieldEvaluation;
							if (boundDagFieldEvaluation == null)
							{
								BoundDagPropertyEvaluation boundDagPropertyEvaluation = evaluation as BoundDagPropertyEvaluation;
								if (boundDagPropertyEvaluation == null)
								{
									BoundDagIndexEvaluation boundDagIndexEvaluation = evaluation as BoundDagIndexEvaluation;
									if (boundDagIndexEvaluation == null)
									{
										throw ExceptionUtilities.UnexpectedValue(boundEvaluationDecisionDagNode.Evaluation.Kind);
									}
									TypeWithAnnotations typeWithAnnotations3 = TypeWithAnnotations.Create(boundDagIndexEvaluation.Property.Type, NullableAnnotation.Annotated, default(ImmutableArray<CustomModifier>));
									BoundDagTemp boundDagTemp2 = new BoundDagTemp(boundDagIndexEvaluation.Syntax, typeWithAnnotations3.Type, boundDagIndexEvaluation);
									int num2 = this.<LearnFromDecisionDag>g__makeDagTempSlot|397_6(typeWithAnnotations3, boundDagTemp2, ref CS$<>8__locals1);
									this.<LearnFromDecisionDag>g__addToTempMap|397_2(boundDagTemp2, num2, typeWithAnnotations3.Type, ref CS$<>8__locals1);
								}
								else
								{
									PropertySymbol propertySymbol = (PropertySymbol)NullableWalker.AsMemberOfType(item8, boundDagPropertyEvaluation.Property);
									TypeWithAnnotations typeWithAnnotations4 = propertySymbol.TypeWithAnnotations;
									BoundDagTemp boundDagTemp3 = new BoundDagTemp(boundDagPropertyEvaluation.Syntax, typeWithAnnotations4.Type, boundDagPropertyEvaluation);
									int num3 = this.GetOrCreateSlot(propertySymbol, item7, true, true);
									if (num3 <= 0)
									{
										num3 = this.<LearnFromDecisionDag>g__makeDagTempSlot|397_6(typeWithAnnotations4, boundDagTemp3, ref CS$<>8__locals1);
									}
									this.<LearnFromDecisionDag>g__addToTempMap|397_2(boundDagTemp3, num3, typeWithAnnotations4.Type, ref CS$<>8__locals1);
									if (propertySymbol.GetMethod != null)
									{
										this.ApplyMemberPostConditions(item7, propertySymbol.GetMethod);
									}
								}
							}
							else
							{
								FieldSymbol fieldSymbol = (FieldSymbol)NullableWalker.AsMemberOfType(item8, boundDagFieldEvaluation.Field);
								TypeWithAnnotations typeWithAnnotations5 = fieldSymbol.TypeWithAnnotations;
								BoundDagTemp boundDagTemp4 = new BoundDagTemp(boundDagFieldEvaluation.Syntax, typeWithAnnotations5.Type, boundDagFieldEvaluation);
								int num4 = -1;
								FieldSymbol fieldSymbol2 = ((boundDagFieldEvaluation.Input.IsOriginalInput && !immutableArray2.IsDefault) ? fieldSymbol : null);
								if (fieldSymbol2 != null)
								{
									num4 = immutableArray2[fieldSymbol2.TupleElementIndex];
								}
								if (num4 <= 0)
								{
									num4 = this.GetOrCreateSlot(fieldSymbol, item7, true, true);
									if (fieldSymbol2 != null && num4 > 0 && !CS$<>8__locals1.originalInputMap.ContainsKey(num4))
									{
										CS$<>8__locals1.originalInputMap.Add(num4, ((BoundTupleExpression)expression).Arguments[fieldSymbol2.TupleElementIndex]);
									}
								}
								if (num4 <= 0)
								{
									num4 = this.<LearnFromDecisionDag>g__makeDagTempSlot|397_6(typeWithAnnotations5, boundDagTemp4, ref CS$<>8__locals1);
								}
								this.<LearnFromDecisionDag>g__addToTempMap|397_2(boundDagTemp4, num4, typeWithAnnotations5.Type, ref CS$<>8__locals1);
							}
						}
						else
						{
							BoundDagTemp boundDagTemp5 = new BoundDagTemp(boundDagTypeEvaluation.Syntax, boundDagTypeEvaluation.Type, boundDagTypeEvaluation);
							CompoundUseSiteInfo<AssemblySymbol> discarded = CompoundUseSiteInfo<AssemblySymbol>.Discarded;
							ConversionKind kind = this._conversions.WithNullability(false).ClassifyConversionFromType(item8, boundDagTypeEvaluation.Type, ref discarded, false).Kind;
							int num5;
							if (kind != ConversionKind.Identity && kind != ConversionKind.ImplicitReference)
							{
								if (kind == ConversionKind.ExplicitNullable)
								{
									TypeWithAnnotations typeWithAnnotations6;
									if (NullableWalker.AreNullableAndUnderlyingTypes(item8, boundDagTypeEvaluation.Type, out typeWithAnnotations6))
									{
										Symbol symbol;
										num5 = this.GetNullableOfTValueSlot(item8, item7, out symbol, true);
										if (num5 >= 0)
										{
											goto IL_035E;
										}
									}
								}
								num5 = this.<LearnFromDecisionDag>g__makeDagTempSlot|397_6(TypeWithAnnotations.Create(boundDagTypeEvaluation.Type, NullableAnnotation.NotAnnotated, default(ImmutableArray<CustomModifier>)), boundDagTemp5, ref CS$<>8__locals1);
							}
							else
							{
								num5 = item7;
							}
							IL_035E:
							base.Unsplit();
							this.State[num5] = NullableFlowState.NotNull;
							this.<LearnFromDecisionDag>g__addToTempMap|397_2(boundDagTemp5, num5, boundDagTypeEvaluation.Type, ref CS$<>8__locals1);
						}
					}
					else
					{
						MethodSymbol deconstructMethod = boundDagDeconstructEvaluation.DeconstructMethod;
						int num6 = (deconstructMethod.RequiresInstanceReceiver ? 0 : 1);
						for (int i = 0; i < deconstructMethod.ParameterCount - num6; i++)
						{
							TypeWithAnnotations typeWithAnnotations7 = deconstructMethod.Parameters[i + num6].TypeWithAnnotations;
							BoundDagTemp boundDagTemp6 = new BoundDagTemp(boundDagDeconstructEvaluation.Syntax, typeWithAnnotations7.Type, boundDagDeconstructEvaluation, i, false);
							int num7 = this.<LearnFromDecisionDag>g__makeDagTempSlot|397_6(typeWithAnnotations7, boundDagTemp6, ref CS$<>8__locals1);
							this.<LearnFromDecisionDag>g__addToTempMap|397_2(boundDagTemp6, num7, typeWithAnnotations7.Type, ref CS$<>8__locals1);
						}
					}
					this.<LearnFromDecisionDag>g__gotoNodeWithCurrentState|397_4(boundEvaluationDecisionDagNode.Next, item2, ref CS$<>8__locals1);
				}
			}
			base.SetUnreachable();
			CS$<>8__locals1.originalInputMap.Free();
			CS$<>8__locals1.tempMap.Free();
			CS$<>8__locals1.nodeStateMap.Free();
			return instance;
		}

		// Token: 0x06001D14 RID: 7444 RVA: 0x0009C11C File Offset: 0x0009A31C
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public override BoundNode VisitConvertedSwitchExpression(BoundConvertedSwitchExpression node)
		{
			bool flag = !node.WasTargetTyped;
			this.VisitSwitchExpressionCore(node, flag);
			return null;
		}

		// Token: 0x06001D15 RID: 7445 RVA: 0x0009C13C File Offset: 0x0009A33C
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public override BoundNode VisitUnconvertedSwitchExpression(BoundUnconvertedSwitchExpression node)
		{
			this.VisitSwitchExpressionCore(node, true);
			return null;
		}

		// Token: 0x06001D16 RID: 7446 RVA: 0x0009C148 File Offset: 0x0009A348
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private void VisitSwitchExpressionCore(BoundSwitchExpression node, bool inferType)
		{
			NullableWalker.<>c__DisplayClass400_0 CS$<>8__locals1 = new NullableWalker.<>c__DisplayClass400_0();
			CS$<>8__locals1.node = node;
			CS$<>8__locals1.<>4__this = this;
			int num = (CS$<>8__locals1.node.Expression.IsSuppressed ? this.GetOrCreatePlaceholderSlot(CS$<>8__locals1.node.Expression) : this.MakeSlot(CS$<>8__locals1.node.Expression));
			if (num > 0)
			{
				TypeSymbol type = CS$<>8__locals1.node.Expression.Type;
				foreach (BoundSwitchExpressionArm boundSwitchExpressionArm in CS$<>8__locals1.node.SwitchArms)
				{
					this.LearnFromAnyNullPatterns(num, type, boundSwitchExpressionArm.Pattern);
				}
			}
			this.Visit(CS$<>8__locals1.node.Expression);
			TypeWithState resultType = this.ResultType;
			CS$<>8__locals1.labelStateMap = this.LearnFromDecisionDag(CS$<>8__locals1.node.Syntax, CS$<>8__locals1.node.DecisionDag, CS$<>8__locals1.node.Expression, resultType);
			NullableWalker.LocalState localState = this.UnreachableState();
			ValueTuple<NullableWalker.LocalState, bool> valueTuple;
			if (!CS$<>8__locals1.node.ReportedNotExhaustive && CS$<>8__locals1.node.DefaultLabel != null && CS$<>8__locals1.labelStateMap.TryGetValue(CS$<>8__locals1.node.DefaultLabel, ref valueTuple) && valueTuple.Item2)
			{
				base.SetState(valueTuple.Item1);
				ImmutableArray<BoundDecisionDagNode> topologicallySortedNodes = CS$<>8__locals1.node.DecisionDag.TopologicallySortedNodes;
				BoundDecisionDagNode boundDecisionDagNode = Enumerable.First<BoundDecisionDagNode>(topologicallySortedNodes.Where<BoundDecisionDagNode>(delegate(BoundDecisionDagNode n)
				{
					BoundLeafDecisionDagNode boundLeafDecisionDagNode = n as BoundLeafDecisionDagNode;
					return boundLeafDecisionDagNode != null && boundLeafDecisionDagNode.Label == CS$<>8__locals1.node.DefaultLabel;
				}));
				bool flag;
				bool flag2;
				string text = PatternExplainer.SamplePatternForPathToDagNode(BoundDagTemp.ForOriginalInput(CS$<>8__locals1.node.Expression), topologicallySortedNodes, boundDecisionDagNode, true, out flag, out flag2);
				ErrorCode errorCode = (flag ? ErrorCode.WRN_SwitchExpressionNotExhaustiveForNullWithWhen : ErrorCode.WRN_SwitchExpressionNotExhaustiveForNull);
				this.ReportDiagnostic(errorCode, ((SwitchExpressionSyntax)CS$<>8__locals1.node.Syntax).SwitchKeyword.GetLocation(), new object[] { text });
			}
			int length = CS$<>8__locals1.node.SwitchArms.Length;
			ArrayBuilder<Conversion> instance = ArrayBuilder<Conversion>.GetInstance(length);
			ArrayBuilder<TypeWithState> instance2 = ArrayBuilder<TypeWithState>.GetInstance(length);
			ArrayBuilder<BoundExpression> instance3 = ArrayBuilder<BoundExpression>.GetInstance(length);
			ArrayBuilder<BoundExpression> instance4 = ArrayBuilder<BoundExpression>.GetInstance(length);
			foreach (BoundSwitchExpressionArm boundSwitchExpressionArm2 in CS$<>8__locals1.node.SwitchArms)
			{
				base.SetState(CS$<>8__locals1.<VisitSwitchExpressionCore>g__getStateForArm|0(boundSwitchExpressionArm2));
				this.TakeIncrementalSnapshot(boundSwitchExpressionArm2);
				this.VisitPatternForRewriting(boundSwitchExpressionArm2.Pattern);
				ValueTuple<BoundExpression, Conversion> valueTuple2 = NullableWalker.RemoveConversion(boundSwitchExpressionArm2.Value, false);
				BoundExpression item = valueTuple2.Item1;
				Conversion item2 = valueTuple2.Item2;
				this.SnapshotWalkerThroughConversionGroup(boundSwitchExpressionArm2.Value, item);
				instance3.Add(item);
				instance.Add(item2);
				TypeWithState typeWithState = this.VisitRvalueWithState(item);
				instance2.Add(typeWithState);
				this.Join(ref localState, ref this.State);
				instance4.Add(NullableWalker.CreatePlaceholderIfNecessary(item, typeWithState.ToTypeWithAnnotations(this.compilation, false)));
			}
			ImmutableArray<BoundExpression> immutableArray = instance4.ToImmutableAndFree();
			CompoundUseSiteInfo<AssemblySymbol> discarded = CompoundUseSiteInfo<AssemblySymbol>.Discarded;
			TypeSymbol typeSymbol;
			if ((typeSymbol = (inferType ? BestTypeInferrer.InferBestType(immutableArray, this._conversions, ref discarded) : null)) == null)
			{
				TypeSymbol type2 = CS$<>8__locals1.node.Type;
				typeSymbol = ((type2 != null) ? type2.SetUnknownNullabilityForReferenceTypes() : null);
			}
			TypeSymbol typeSymbol2 = typeSymbol;
			TypeWithAnnotations typeWithAnnotations = TypeWithAnnotations.Create(typeSymbol2, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
			NullableFlowState nullableFlowState;
			if (inferType)
			{
				if (typeSymbol2 == null)
				{
					nullableFlowState = NullableFlowState.NotNull;
				}
				else
				{
					for (int i = 0; i < length; i++)
					{
						BoundExpression boundExpression = instance3[i];
						BoundSwitchExpressionArm boundSwitchExpressionArm3 = CS$<>8__locals1.node.SwitchArms[i];
						NullableWalker.LocalState localState2 = CS$<>8__locals1.<VisitSwitchExpressionCore>g__getStateForArm|0(boundSwitchExpressionArm3);
						instance2[i] = this.ConvertConditionalOperandOrSwitchExpressionArmResult(boundSwitchExpressionArm3.Value, boundExpression, instance[i], typeWithAnnotations, instance2[i], localState2, localState2.Reachable);
					}
					nullableFlowState = BestTypeInferrer.GetNullableState(instance2);
				}
			}
			else
			{
				ArrayBuilder<ValueTuple<NullableWalker.LocalState, TypeWithState, bool>> instance5 = ArrayBuilder<ValueTuple<NullableWalker.LocalState, TypeWithState, bool>>.GetInstance(length);
				for (int j = 0; j < length; j++)
				{
					BoundExpression boundExpression2 = instance3[j];
					NullableWalker.LocalState localState3 = CS$<>8__locals1.<VisitSwitchExpressionCore>g__getStateForArm|0(CS$<>8__locals1.node.SwitchArms[j]);
					instance5.Add(new ValueTuple<NullableWalker.LocalState, TypeWithState, bool>(localState3, instance2[j], localState3.Reachable));
				}
				this.ConditionalInfoForConversion.Add(CS$<>8__locals1.node, instance5.ToImmutableAndFree());
				nullableFlowState = BestTypeInferrer.GetNullableState(instance2);
			}
			TypeWithState typeWithState2 = TypeWithState.Create(typeSymbol2, nullableFlowState);
			instance.Free();
			instance2.Free();
			instance3.Free();
			CS$<>8__locals1.labelStateMap.Free();
			base.SetState(localState);
			this.SetResult(CS$<>8__locals1.node, typeWithState2, typeWithAnnotations, true, default(bool?));
		}

		// Token: 0x06001D17 RID: 7447 RVA: 0x0009C5E8 File Offset: 0x0009A7E8
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public override BoundNode VisitIsPatternExpression(BoundIsPatternExpression node)
		{
			this.LearnFromAnyNullPatterns(node.Expression, node.Pattern);
			this.VisitPatternForRewriting(node.Pattern);
			this.Visit(node.Expression);
			TypeWithState resultType = this.ResultType;
			PooledDictionary<LabelSymbol, ValueTuple<NullableWalker.LocalState, bool>> pooledDictionary = this.LearnFromDecisionDag(node.Syntax, node.DecisionDag, node.Expression, resultType);
			ValueTuple<NullableWalker.LocalState, bool> valueTuple;
			NullableWalker.LocalState localState = (pooledDictionary.TryGetValue(node.IsNegated ? node.WhenFalseLabel : node.WhenTrueLabel, ref valueTuple) ? valueTuple.Item1 : this.UnreachableState());
			ValueTuple<NullableWalker.LocalState, bool> valueTuple2;
			NullableWalker.LocalState localState2 = (pooledDictionary.TryGetValue(node.IsNegated ? node.WhenTrueLabel : node.WhenFalseLabel, ref valueTuple2) ? valueTuple2.Item1 : this.UnreachableState());
			pooledDictionary.Free();
			base.SetConditionalState(localState, localState2);
			this.SetNotNullResult(node);
			return null;
		}

		// Token: 0x06001D19 RID: 7449 RVA: 0x0009C6C8 File Offset: 0x0009A8C8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[CompilerGenerated]
		private void <Scan>g__enforceMemberNotNull|52_0(SyntaxNode syntaxOpt, NullableWalker.LocalState state)
		{
			if (!state.Reachable)
			{
				return;
			}
			MethodSymbol methodSymbol = this._symbol as MethodSymbol;
			if (methodSymbol != null)
			{
				if (methodSymbol.IsConstructor())
				{
					int num = 0;
					if (methodSymbol.RequiresInstanceReceiver)
					{
						ParameterSymbol parameterSymbol;
						methodSymbol.TryGetThisParameter(out parameterSymbol);
						num = this.GetOrCreateSlot(parameterSymbol, 0, false, true);
					}
					Location location = (methodSymbol.DeclaringSyntaxReferences.IsEmpty ? null : methodSymbol.Locations.FirstOrDefault<Location>());
					foreach (Symbol symbol in methodSymbol.ContainingType.GetMembersUnordered())
					{
						this.<Scan>g__checkMemberStateOnConstructorExit|52_1(methodSymbol, symbol, state, num, location);
					}
					return;
				}
				do
				{
					foreach (string text in methodSymbol.NotNullMembers)
					{
						this.<Scan>g__enforceMemberNotNullOnMember|52_2(syntaxOpt, state, methodSymbol, text);
					}
					methodSymbol = methodSymbol.OverriddenMethod;
				}
				while (methodSymbol != null);
			}
		}

		// Token: 0x06001D1A RID: 7450 RVA: 0x0009C7B0 File Offset: 0x0009A9B0
		[CompilerGenerated]
		private void <Scan>g__checkMemberStateOnConstructorExit|52_1(MethodSymbol constructor, Symbol member, NullableWalker.LocalState state, int thisSlot, [global::System.Runtime.CompilerServices.Nullable(2)] Location exitLocation)
		{
			bool flag = !constructor.RequiresInstanceReceiver();
			if (member.IsStatic != flag)
			{
				return;
			}
			if (LocalDataFlowPass<NullableWalker.LocalState, NullableWalker.LocalFunctionState>.HasInitializer(member))
			{
				return;
			}
			FieldSymbol fieldSymbol = member as FieldSymbol;
			TypeWithAnnotations typeWithAnnotations;
			FieldSymbol fieldSymbol2;
			Symbol symbol;
			if (fieldSymbol == null)
			{
				EventSymbol eventSymbol = member as EventSymbol;
				if (eventSymbol == null)
				{
					return;
				}
				typeWithAnnotations = eventSymbol.TypeWithAnnotations;
				fieldSymbol2 = eventSymbol.AssociatedField;
				symbol = eventSymbol;
				if (fieldSymbol2 == null)
				{
					return;
				}
			}
			else
			{
				typeWithAnnotations = fieldSymbol.TypeWithAnnotations;
				fieldSymbol2 = fieldSymbol;
				symbol = (fieldSymbol.AssociatedSymbol as PropertySymbol) ?? fieldSymbol;
			}
			if (fieldSymbol2.IsConst)
			{
				return;
			}
			if (typeWithAnnotations.Type.IsValueType || typeWithAnnotations.Type.IsErrorType())
			{
				return;
			}
			FlowAnalysisAnnotations flowAnalysisAnnotations = symbol.GetFlowAnalysisAnnotations();
			if ((flowAnalysisAnnotations & FlowAnalysisAnnotations.AllowNull) != FlowAnalysisAnnotations.None)
			{
				return;
			}
			typeWithAnnotations = NullableWalker.ApplyUnconditionalAnnotations(typeWithAnnotations, flowAnalysisAnnotations);
			if (!typeWithAnnotations.NullableAnnotation.IsNotAnnotated())
			{
				return;
			}
			int orCreateSlot = this.GetOrCreateSlot(symbol, thisSlot, false, true);
			if (orCreateSlot < 0)
			{
				return;
			}
			NullableFlowState nullableFlowState = state[orCreateSlot];
			NullableFlowState nullableFlowState2 = ((typeWithAnnotations.Type.IsPossiblyNullableReferenceTypeTypeParameter() && (flowAnalysisAnnotations & FlowAnalysisAnnotations.NotNull) == FlowAnalysisAnnotations.None) ? NullableFlowState.MaybeDefault : NullableFlowState.MaybeNull);
			if (nullableFlowState >= nullableFlowState2)
			{
				base.Diagnostics.Add(ErrorCode.WRN_UninitializedNonNullableField, exitLocation ?? symbol.Locations.FirstOrNone(), new object[]
				{
					symbol.Kind.Localize(),
					symbol.Name
				});
			}
		}

		// Token: 0x06001D1B RID: 7451 RVA: 0x0009C8F8 File Offset: 0x0009AAF8
		[CompilerGenerated]
		private void <Scan>g__enforceMemberNotNullOnMember|52_2([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode syntaxOpt, NullableWalker.LocalState state, MethodSymbol method, string memberName)
		{
			foreach (Symbol symbol in method.ContainingType.GetMembers(memberName))
			{
				if (this.<Scan>g__memberHasBadState|52_7(symbol, state))
				{
					base.Diagnostics.Add(ErrorCode.WRN_MemberNotNull, ((syntaxOpt != null) ? syntaxOpt.GetLocation() : null) ?? this.methodMainNode.Syntax.GetLastToken(false, false, false, false).GetLocation(), new object[] { symbol.Name });
				}
			}
		}

		// Token: 0x06001D1C RID: 7452 RVA: 0x0009C984 File Offset: 0x0009AB84
		[CompilerGenerated]
		private void <Scan>g__enforceMemberNotNullWhenForPendingReturn|52_3(AbstractFlowPass<NullableWalker.LocalState, NullableWalker.LocalFunctionState>.PendingBranch pendingReturn, BoundReturnStatement returnStatement)
		{
			if (pendingReturn.IsConditionalState)
			{
				BoundExpression boundExpression = returnStatement.ExpressionOpt;
				if (boundExpression != null)
				{
					ConstantValue constantValue = boundExpression.ConstantValue;
					if (constantValue != null && constantValue.IsBoolean)
					{
						bool booleanValue = constantValue.BooleanValue;
						this.<Scan>g__enforceMemberNotNullWhen|52_5(returnStatement.Syntax, booleanValue, pendingReturn.State);
						return;
					}
				}
				if (!pendingReturn.StateWhenTrue.Reachable || !pendingReturn.StateWhenFalse.Reachable)
				{
					return;
				}
				MethodSymbol methodSymbol = this._symbol as MethodSymbol;
				if (methodSymbol != null)
				{
					foreach (string text in methodSymbol.NotNullWhenTrueMembers)
					{
						this.<Scan>g__enforceMemberNotNullWhenIfAffected|52_4(returnStatement.Syntax, true, methodSymbol.ContainingType.GetMembers(text), pendingReturn.StateWhenTrue, pendingReturn.StateWhenFalse);
					}
					foreach (string text2 in methodSymbol.NotNullWhenFalseMembers)
					{
						this.<Scan>g__enforceMemberNotNullWhenIfAffected|52_4(returnStatement.Syntax, false, methodSymbol.ContainingType.GetMembers(text2), pendingReturn.StateWhenFalse, pendingReturn.StateWhenTrue);
					}
					return;
				}
			}
			else
			{
				BoundExpression boundExpression = returnStatement.ExpressionOpt;
				if (boundExpression != null)
				{
					ConstantValue constantValue = boundExpression.ConstantValue;
					if (constantValue != null && constantValue.IsBoolean)
					{
						bool booleanValue2 = constantValue.BooleanValue;
						this.<Scan>g__enforceMemberNotNullWhen|52_5(returnStatement.Syntax, booleanValue2, pendingReturn.State);
					}
				}
			}
		}

		// Token: 0x06001D1D RID: 7453 RVA: 0x0009CAD0 File Offset: 0x0009ACD0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[CompilerGenerated]
		private void <Scan>g__enforceMemberNotNullWhenIfAffected|52_4(SyntaxNode syntaxOpt, bool sense, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<Symbol> members, NullableWalker.LocalState state, NullableWalker.LocalState otherState)
		{
			foreach (Symbol symbol in members)
			{
				if (this.<Scan>g__memberHasBadState|52_7(symbol, state) != this.<Scan>g__memberHasBadState|52_7(symbol, otherState))
				{
					this.<Scan>g__reportMemberIfBadConditionalState|52_6(syntaxOpt, sense, symbol, state);
				}
			}
		}

		// Token: 0x06001D1E RID: 7454 RVA: 0x0009CB18 File Offset: 0x0009AD18
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[CompilerGenerated]
		private void <Scan>g__enforceMemberNotNullWhen|52_5(SyntaxNode syntaxOpt, bool sense, NullableWalker.LocalState state)
		{
			MethodSymbol methodSymbol = this._symbol as MethodSymbol;
			if (methodSymbol != null)
			{
				foreach (string text in (sense ? methodSymbol.NotNullWhenTrueMembers : methodSymbol.NotNullWhenFalseMembers))
				{
					foreach (Symbol symbol in methodSymbol.ContainingType.GetMembers(text))
					{
						this.<Scan>g__reportMemberIfBadConditionalState|52_6(syntaxOpt, sense, symbol, state);
					}
				}
			}
		}

		// Token: 0x06001D1F RID: 7455 RVA: 0x0009CB98 File Offset: 0x0009AD98
		[CompilerGenerated]
		private void <Scan>g__reportMemberIfBadConditionalState|52_6([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode syntaxOpt, bool sense, Symbol member, NullableWalker.LocalState state)
		{
			if (this.<Scan>g__memberHasBadState|52_7(member, state))
			{
				base.Diagnostics.Add(ErrorCode.WRN_MemberNotNullWhen, ((syntaxOpt != null) ? syntaxOpt.GetLocation() : null) ?? this.methodMainNode.Syntax.GetLastToken(false, false, false, false).GetLocation(), new object[]
				{
					member.Name,
					sense ? "true" : "false"
				});
			}
		}

		// Token: 0x06001D20 RID: 7456 RVA: 0x0009CC10 File Offset: 0x0009AE10
		[CompilerGenerated]
		private bool <Scan>g__memberHasBadState|52_7(Symbol member, NullableWalker.LocalState state)
		{
			SymbolKind kind = member.Kind;
			switch (kind)
			{
			case SymbolKind.Event:
			case SymbolKind.Label:
			case SymbolKind.Local:
			case SymbolKind.Method:
				return false;
			case SymbolKind.Field:
				break;
			default:
				if (kind != SymbolKind.Property)
				{
					return false;
				}
				break;
			}
			int num = this.<Scan>g__getSlotForFieldOrPropertyOrEvent|52_16(member);
			return num > 0 && !state[num].IsNotNull();
		}

		// Token: 0x06001D21 RID: 7457 RVA: 0x0009CC68 File Offset: 0x0009AE68
		[CompilerGenerated]
		private void <Scan>g__makeNotNullMembersMaybeNull|52_8()
		{
			Symbol symbol = this._symbol;
			NullableWalker.<>c__DisplayClass52_0 CS$<>8__locals1;
			CS$<>8__locals1.method = symbol as MethodSymbol;
			if (CS$<>8__locals1.method != null)
			{
				if (CS$<>8__locals1.method.IsConstructor())
				{
					if (this.<Scan>g__needsDefaultInitialStateForMembers|52_17(ref CS$<>8__locals1))
					{
						foreach (Symbol symbol2 in CS$<>8__locals1.method.ContainingType.GetMembersUnordered())
						{
							if (symbol2.IsStatic == CS$<>8__locals1.method.IsStatic)
							{
								Symbol symbol3 = symbol2;
								if (!(symbol2 is PropertySymbol))
								{
									FieldSymbol fieldSymbol = symbol2 as FieldSymbol;
									if (fieldSymbol != null)
									{
										if (fieldSymbol.IsConst)
										{
											continue;
										}
										PropertySymbol propertySymbol = fieldSymbol.AssociatedSymbol as PropertySymbol;
										if (propertySymbol != null)
										{
											if (this.IsPropertyOutputMoreStrictThanInput(propertySymbol))
											{
												continue;
											}
											symbol3 = propertySymbol;
										}
									}
									int num = this.<Scan>g__getSlotForFieldOrPropertyOrEvent|52_16(symbol3);
									if (num > 0)
									{
										TypeWithAnnotations typeOrReturnType = symbol3.GetTypeOrReturnType();
										if (!typeOrReturnType.NullableAnnotation.IsOblivious())
										{
											this.State[num] = (typeOrReturnType.Type.IsPossiblyNullableReferenceTypeTypeParameter() ? NullableFlowState.MaybeDefault : NullableFlowState.MaybeNull);
										}
									}
								}
							}
						}
						return;
					}
				}
				else
				{
					do
					{
						this.<Scan>g__makeMembersMaybeNull|52_9(CS$<>8__locals1.method, CS$<>8__locals1.method.NotNullMembers);
						this.<Scan>g__makeMembersMaybeNull|52_9(CS$<>8__locals1.method, CS$<>8__locals1.method.NotNullWhenTrueMembers);
						this.<Scan>g__makeMembersMaybeNull|52_9(CS$<>8__locals1.method, CS$<>8__locals1.method.NotNullWhenFalseMembers);
						CS$<>8__locals1.method = CS$<>8__locals1.method.OverriddenMethod;
					}
					while (CS$<>8__locals1.method != null);
				}
			}
		}

		// Token: 0x06001D22 RID: 7458 RVA: 0x0009CDEC File Offset: 0x0009AFEC
		[CompilerGenerated]
		private bool <Scan>g__needsDefaultInitialStateForMembers|52_17(ref NullableWalker.<>c__DisplayClass52_0 A_1)
		{
			if (this._hasInitialState)
			{
				return false;
			}
			if (!A_1.method.HasThisConstructorInitializer() && (!A_1.method.ContainingType.IsValueType || A_1.method.IsStatic))
			{
				return true;
			}
			BoundConstructorMethodBody boundConstructorMethodBody = this.methodMainNode as BoundConstructorMethodBody;
			if (boundConstructorMethodBody != null)
			{
				BoundExpressionStatement initializer = boundConstructorMethodBody.Initializer;
				MethodSymbol methodSymbol = ((initializer != null) ? initializer.Expression.ExpressionSymbol : null) as MethodSymbol;
				if (methodSymbol != null)
				{
					return methodSymbol.IsDefaultValueTypeConstructor();
				}
			}
			return false;
		}

		// Token: 0x06001D23 RID: 7459 RVA: 0x0009CE68 File Offset: 0x0009B068
		[CompilerGenerated]
		private void <Scan>g__makeMembersMaybeNull|52_9(MethodSymbol method, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<string> members)
		{
			foreach (string text in members)
			{
				this.<Scan>g__makeMemberMaybeNull|52_10(method, text);
			}
		}

		// Token: 0x06001D24 RID: 7460 RVA: 0x0009CE98 File Offset: 0x0009B098
		[CompilerGenerated]
		private void <Scan>g__makeMemberMaybeNull|52_10(MethodSymbol method, string memberName)
		{
			foreach (Symbol symbol in method.ContainingType.GetMembers(memberName))
			{
				int num = this.<Scan>g__getSlotForFieldOrPropertyOrEvent|52_16(symbol);
				if (num > 0)
				{
					this.State[num] = NullableFlowState.MaybeNull;
				}
			}
		}

		// Token: 0x06001D25 RID: 7461 RVA: 0x0009CEE8 File Offset: 0x0009B0E8
		[CompilerGenerated]
		private void <Scan>g__enforceNotNullWhenForPendingReturn|52_11(AbstractFlowPass<NullableWalker.LocalState, NullableWalker.LocalFunctionState>.PendingBranch pendingReturn, BoundReturnStatement returnStatement)
		{
			ImmutableArray<ParameterSymbol> methodParameters = base.MethodParameters;
			if (!methodParameters.IsEmpty)
			{
				if (pendingReturn.IsConditionalState)
				{
					BoundExpression boundExpression = returnStatement.ExpressionOpt;
					if (boundExpression != null)
					{
						ConstantValue constantValue = boundExpression.ConstantValue;
						if (constantValue != null && constantValue.IsBoolean)
						{
							bool booleanValue = constantValue.BooleanValue;
							this.<Scan>g__enforceParameterNotNullWhen|52_14(returnStatement.Syntax, methodParameters, booleanValue, pendingReturn.State);
							return;
						}
					}
					if (!pendingReturn.StateWhenTrue.Reachable || !pendingReturn.StateWhenFalse.Reachable)
					{
						return;
					}
					foreach (ParameterSymbol parameterSymbol in methodParameters)
					{
						int orCreateSlot = this.GetOrCreateSlot(parameterSymbol, 0, false, true);
						if (orCreateSlot > 0 && pendingReturn.StateWhenTrue[orCreateSlot] != pendingReturn.StateWhenFalse[orCreateSlot])
						{
							this.<Scan>g__reportParameterIfBadConditionalState|52_12(returnStatement.Syntax, parameterSymbol, true, pendingReturn.StateWhenTrue);
							this.<Scan>g__reportParameterIfBadConditionalState|52_12(returnStatement.Syntax, parameterSymbol, false, pendingReturn.StateWhenFalse);
						}
					}
					return;
				}
				else
				{
					BoundExpression boundExpression = returnStatement.ExpressionOpt;
					if (boundExpression != null)
					{
						ConstantValue constantValue = boundExpression.ConstantValue;
						if (constantValue != null && constantValue.IsBoolean)
						{
							bool booleanValue2 = constantValue.BooleanValue;
							this.<Scan>g__enforceParameterNotNullWhen|52_14(returnStatement.Syntax, methodParameters, booleanValue2, pendingReturn.State);
							return;
						}
					}
				}
			}
		}

		// Token: 0x06001D26 RID: 7462 RVA: 0x0009D018 File Offset: 0x0009B218
		[CompilerGenerated]
		private void <Scan>g__reportParameterIfBadConditionalState|52_12(SyntaxNode syntax, ParameterSymbol parameter, bool sense, NullableWalker.LocalState stateWhen)
		{
			if (this.<Scan>g__parameterHasBadConditionalState|52_15(parameter, sense, stateWhen))
			{
				base.Diagnostics.Add(ErrorCode.WRN_ParameterConditionallyDisallowsNull, syntax.Location, new object[]
				{
					parameter.Name,
					sense ? "true" : "false"
				});
			}
		}

		// Token: 0x06001D27 RID: 7463 RVA: 0x0009D06C File Offset: 0x0009B26C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[CompilerGenerated]
		private void <Scan>g__enforceNotNull|52_13(SyntaxNode syntaxOpt, NullableWalker.LocalState state)
		{
			if (!state.Reachable)
			{
				return;
			}
			foreach (ParameterSymbol parameterSymbol in base.MethodParameters)
			{
				int orCreateSlot = this.GetOrCreateSlot(parameterSymbol, 0, false, true);
				if (orCreateSlot > 0)
				{
					bool flag = (parameterSymbol.FlowAnalysisAnnotations & FlowAnalysisAnnotations.NotNull) == FlowAnalysisAnnotations.NotNull;
					NullableFlowState nullableFlowState = state[orCreateSlot];
					if (flag && nullableFlowState.MayBeNull())
					{
						base.Diagnostics.Add(ErrorCode.WRN_ParameterDisallowsNull, ((syntaxOpt != null) ? syntaxOpt.GetLocation() : null) ?? this.methodMainNode.Syntax.GetLastToken(false, false, false, false).GetLocation(), new object[] { parameterSymbol.Name });
					}
					else
					{
						this.EnforceNotNullIfNotNull(syntaxOpt, state, base.MethodParameters, parameterSymbol.NotNullIfParameterNotNull, nullableFlowState, parameterSymbol);
					}
				}
			}
		}

		// Token: 0x06001D28 RID: 7464 RVA: 0x0009D148 File Offset: 0x0009B348
		[CompilerGenerated]
		private void <Scan>g__enforceParameterNotNullWhen|52_14(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<ParameterSymbol> parameters, bool sense, NullableWalker.LocalState stateWhen)
		{
			if (!stateWhen.Reachable)
			{
				return;
			}
			foreach (ParameterSymbol parameterSymbol in parameters)
			{
				this.<Scan>g__reportParameterIfBadConditionalState|52_12(syntax, parameterSymbol, sense, stateWhen);
			}
		}

		// Token: 0x06001D29 RID: 7465 RVA: 0x0009D188 File Offset: 0x0009B388
		[CompilerGenerated]
		private bool <Scan>g__parameterHasBadConditionalState|52_15(ParameterSymbol parameter, bool sense, NullableWalker.LocalState stateWhen)
		{
			RefKind refKind = parameter.RefKind;
			if (refKind != RefKind.Out && refKind != RefKind.Ref)
			{
				return false;
			}
			int orCreateSlot = this.GetOrCreateSlot(parameter, 0, false, true);
			if (orCreateSlot <= 0)
			{
				return false;
			}
			NullableFlowState nullableFlowState = stateWhen[orCreateSlot];
			FlowAnalysisAnnotations flowAnalysisAnnotations = parameter.FlowAnalysisAnnotations;
			if (sense)
			{
				bool flag = (flowAnalysisAnnotations & FlowAnalysisAnnotations.NotNull) == FlowAnalysisAnnotations.NotNullWhenTrue;
				bool flag2 = (flowAnalysisAnnotations & FlowAnalysisAnnotations.MaybeNull) == FlowAnalysisAnnotations.MaybeNullWhenFalse;
				return (flag && nullableFlowState.MayBeNull()) || (flag2 && NullableWalker.ShouldReportNullableAssignment(parameter.TypeWithAnnotations, nullableFlowState));
			}
			bool flag3 = (flowAnalysisAnnotations & FlowAnalysisAnnotations.NotNull) == FlowAnalysisAnnotations.NotNullWhenFalse;
			bool flag4 = (flowAnalysisAnnotations & FlowAnalysisAnnotations.MaybeNull) == FlowAnalysisAnnotations.MaybeNullWhenTrue;
			return (flag3 && nullableFlowState.MayBeNull()) || (flag4 && NullableWalker.ShouldReportNullableAssignment(parameter.TypeWithAnnotations, nullableFlowState));
		}

		// Token: 0x06001D2A RID: 7466 RVA: 0x0009D22C File Offset: 0x0009B42C
		[CompilerGenerated]
		private int <Scan>g__getSlotForFieldOrPropertyOrEvent|52_16(Symbol member)
		{
			if (member.Kind != SymbolKind.Field && member.Kind != SymbolKind.Property && member.Kind != SymbolKind.Event)
			{
				return -1;
			}
			int num = 0;
			if (!member.IsStatic)
			{
				if (base.MethodThisParameter == null)
				{
					return -1;
				}
				num = this.GetOrCreateSlot(base.MethodThisParameter, 0, false, true);
				if (num < 0)
				{
					return -1;
				}
			}
			return this.GetOrCreateSlot(member, num, false, true);
		}

		// Token: 0x06001D2B RID: 7467 RVA: 0x0009D28C File Offset: 0x0009B48C
		[CompilerGenerated]
		private int <MakeSlot>g__makeSlot|76_0(BoundExpression node)
		{
			BoundKind kind = node.Kind;
			if (kind <= BoundKind.BaseReference)
			{
				if (kind <= BoundKind.Conversion)
				{
					if (kind - BoundKind.DefaultLiteral > 1)
					{
						if (kind != BoundKind.Conversion)
						{
							goto IL_016B;
						}
						int num = this.<MakeSlot>g__getPlaceholderSlot|76_1(node);
						if (num > 0)
						{
							return num;
						}
						BoundConversion boundConversion = (BoundConversion)node;
						ConversionKind kind2 = boundConversion.Conversion.Kind;
						if (kind2 <= ConversionKind.ImplicitTupleLiteral)
						{
							if (kind2 != ConversionKind.Identity && kind2 != ConversionKind.ImplicitTupleLiteral)
							{
								return -1;
							}
						}
						else if (kind2 - ConversionKind.ImplicitReference > 1)
						{
							if (kind2 != ConversionKind.ExplicitNullable)
							{
								if (kind2 != ConversionKind.DefaultLiteral)
								{
									return -1;
								}
							}
							else
							{
								BoundExpression operand = boundConversion.Operand;
								TypeSymbol type = operand.Type;
								TypeSymbol type2 = boundConversion.Type;
								TypeWithAnnotations typeWithAnnotations;
								if (!NullableWalker.AreNullableAndUnderlyingTypes(type, type2, out typeWithAnnotations))
								{
									return -1;
								}
								int num2 = this.MakeSlot(operand);
								if (num2 >= 0)
								{
									Symbol symbol;
									return this.GetNullableOfTValueSlot(type, num2, out symbol, false);
								}
								return -1;
							}
						}
						return this.MakeSlot(boundConversion.Operand);
					}
				}
				else
				{
					if (kind != BoundKind.ThisReference && kind != BoundKind.BaseReference)
					{
						goto IL_016B;
					}
					MethodSymbol methodSymbol = NullableWalker.<MakeSlot>g__getTopLevelMethod|76_2(this._symbol as MethodSymbol);
					ParameterSymbol parameterSymbol = ((methodSymbol != null) ? methodSymbol.ThisParameter : null);
					if (parameterSymbol == null)
					{
						return -1;
					}
					return this.GetOrCreateSlot(parameterSymbol, 0, false, true);
				}
			}
			else if (kind <= BoundKind.ConditionalReceiver)
			{
				if (kind == BoundKind.ConditionalAccess)
				{
					return this.<MakeSlot>g__getPlaceholderSlot|76_1(node);
				}
				if (kind != BoundKind.ConditionalReceiver)
				{
					goto IL_016B;
				}
				return this._lastConditionalAccessSlot;
			}
			else if (kind - BoundKind.ObjectCreationExpression > 3 && kind != BoundKind.AnonymousObjectCreationExpression && kind != BoundKind.NewT)
			{
				goto IL_016B;
			}
			return this.<MakeSlot>g__getPlaceholderSlot|76_1(node);
			IL_016B:
			int num3 = this.<MakeSlot>g__getPlaceholderSlot|76_1(node);
			if (num3 <= 0)
			{
				return base.MakeSlot(node);
			}
			return num3;
		}

		// Token: 0x06001D2C RID: 7468 RVA: 0x0009D420 File Offset: 0x0009B620
		[CompilerGenerated]
		private int <MakeSlot>g__getPlaceholderSlot|76_1(BoundExpression expr)
		{
			NullableWalker.PlaceholderLocal placeholderLocal;
			if (this._placeholderLocalsOpt != null && this._placeholderLocalsOpt.TryGetValue(expr, ref placeholderLocal))
			{
				return this.GetOrCreateSlot(placeholderLocal, 0, false, true);
			}
			return -1;
		}

		// Token: 0x06001D2D RID: 7469 RVA: 0x0009D454 File Offset: 0x0009B654
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[CompilerGenerated]
		internal static MethodSymbol <MakeSlot>g__getTopLevelMethod|76_2(MethodSymbol method)
		{
			while (method != null)
			{
				Symbol containingSymbol = method.ContainingSymbol;
				if (containingSymbol.Kind == SymbolKind.NamedType)
				{
					return method;
				}
				method = containingSymbol as MethodSymbol;
			}
			return null;
		}

		// Token: 0x06001D2E RID: 7470 RVA: 0x0009D482 File Offset: 0x0009B682
		[CompilerGenerated]
		internal static bool <ReportNullableAssignmentIfNecessary>g__isMaybeDefaultValue|89_0(TypeWithState valueType)
		{
			TypeSymbol type = valueType.Type;
			return type != null && type.TypeKind == TypeKind.TypeParameter && valueType.State == NullableFlowState.MaybeDefault;
		}

		// Token: 0x06001D2F RID: 7471 RVA: 0x0009D4A7 File Offset: 0x0009B6A7
		[CompilerGenerated]
		internal static bool <ReportNullableAssignmentIfNecessary>g__allowUnconstrainedTypeParameterAnnotations|89_1(CSharpCompilation compilation)
		{
			return MessageID.IDS_FeatureDefaultTypeParameterConstraint.RequiredVersion() <= compilation.LanguageVersion;
		}

		// Token: 0x06001D30 RID: 7472 RVA: 0x0009D4C0 File Offset: 0x0009B6C0
		[CompilerGenerated]
		internal static bool <AreParameterAnnotationsCompatible>g__canAssignOutputValueWhen|90_0(bool sense, ref NullableWalker.<>c__DisplayClass90_0 A_1)
		{
			TypeWithState typeWithState = NullableWalker.ApplyUnconditionalAnnotations(A_1.overridingType.ToTypeWithState(), NullableWalker.<AreParameterAnnotationsCompatible>g__makeUnconditionalAnnotation|90_2(A_1.overridingAnnotations, sense));
			FlowAnalysisAnnotations flowAnalysisAnnotations = NullableWalker.ToInwardAnnotations(NullableWalker.<AreParameterAnnotationsCompatible>g__makeUnconditionalAnnotation|90_2(A_1.overriddenAnnotations, sense));
			return !NullableWalker.<AreParameterAnnotationsCompatible>g__isBadAssignment|90_1(typeWithState, A_1.overriddenType, flowAnalysisAnnotations);
		}

		// Token: 0x06001D31 RID: 7473 RVA: 0x0009D50C File Offset: 0x0009B70C
		[CompilerGenerated]
		internal static bool <AreParameterAnnotationsCompatible>g__isBadAssignment|90_1(TypeWithState valueState, TypeWithAnnotations destinationType, FlowAnalysisAnnotations destinationAnnotations)
		{
			return NullableWalker.ShouldReportNullableAssignment(NullableWalker.ApplyLValueAnnotations(destinationType, destinationAnnotations), valueState.State) || NullableWalker.IsDisallowedNullAssignment(valueState, destinationAnnotations);
		}

		// Token: 0x06001D32 RID: 7474 RVA: 0x0009D530 File Offset: 0x0009B730
		[CompilerGenerated]
		internal static FlowAnalysisAnnotations <AreParameterAnnotationsCompatible>g__makeUnconditionalAnnotation|90_2(FlowAnalysisAnnotations annotations, bool sense)
		{
			if (sense)
			{
				return NullableWalker.<AreParameterAnnotationsCompatible>g__makeUnconditionalAnnotationCore|90_3(NullableWalker.<AreParameterAnnotationsCompatible>g__makeUnconditionalAnnotationCore|90_3(annotations, FlowAnalysisAnnotations.NotNullWhenTrue, FlowAnalysisAnnotations.NotNull), FlowAnalysisAnnotations.MaybeNullWhenTrue, FlowAnalysisAnnotations.MaybeNull);
			}
			return NullableWalker.<AreParameterAnnotationsCompatible>g__makeUnconditionalAnnotationCore|90_3(NullableWalker.<AreParameterAnnotationsCompatible>g__makeUnconditionalAnnotationCore|90_3(annotations, FlowAnalysisAnnotations.NotNullWhenFalse, FlowAnalysisAnnotations.NotNull), FlowAnalysisAnnotations.MaybeNullWhenFalse, FlowAnalysisAnnotations.MaybeNull);
		}

		// Token: 0x06001D33 RID: 7475 RVA: 0x0009D55A File Offset: 0x0009B75A
		[CompilerGenerated]
		internal static FlowAnalysisAnnotations <AreParameterAnnotationsCompatible>g__makeUnconditionalAnnotationCore|90_3(FlowAnalysisAnnotations annotations, FlowAnalysisAnnotations conditionalAnnotation, FlowAnalysisAnnotations replacementAnnotation)
		{
			if ((annotations & conditionalAnnotation) != FlowAnalysisAnnotations.None)
			{
				return annotations | replacementAnnotation;
			}
			return annotations & ~replacementAnnotation;
		}

		// Token: 0x06001D34 RID: 7476 RVA: 0x0009D569 File Offset: 0x0009B769
		[CompilerGenerated]
		internal static bool <TrackNullableStateForAssignment>g__areEquivalentTypes|94_0(TypeWithAnnotations target, TypeWithState assignedValue)
		{
			return target.Type.Equals(assignedValue.Type, TypeCompareKind.AllIgnoreOptions);
		}

		// Token: 0x06001D35 RID: 7477 RVA: 0x0009D580 File Offset: 0x0009B780
		[CompilerGenerated]
		private void <VisitObjectCreationInitializer>g__checkImplicitReceiver|146_0(BoundObjectInitializerExpressionBase node, ref NullableWalker.<>c__DisplayClass146_0 A_2)
		{
			if (A_2.containingSlot >= 0 && !node.Initializers.IsEmpty && !node.Type.IsValueType && this.State[A_2.containingSlot].MayBeNull())
			{
				if (A_2.containingSymbol == null)
				{
					this.ReportDiagnostic(ErrorCode.WRN_NullReferenceReceiver, node.Syntax, Array.Empty<object>());
					return;
				}
				this.ReportDiagnostic(ErrorCode.WRN_NullReferenceInitializer, node.Syntax, new object[] { A_2.containingSymbol });
			}
		}

		// Token: 0x06001D37 RID: 7479 RVA: 0x0009D61C File Offset: 0x0009B81C
		[CompilerGenerated]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		internal static BoundAnonymousPropertyDeclaration <VisitAnonymousObjectCreationExpression>g__getDeclaration|153_3(BoundAnonymousObjectCreationExpression node, PropertySymbol currentProperty, ref int currentDeclarationIndex)
		{
			if (currentDeclarationIndex >= node.Declarations.Length)
			{
				return null;
			}
			BoundAnonymousPropertyDeclaration boundAnonymousPropertyDeclaration = node.Declarations[currentDeclarationIndex];
			int? memberIndexOpt = boundAnonymousPropertyDeclaration.Property.MemberIndexOpt;
			int? memberIndexOpt2 = currentProperty.MemberIndexOpt;
			if ((memberIndexOpt.GetValueOrDefault() == memberIndexOpt2.GetValueOrDefault()) & (memberIndexOpt != null == (memberIndexOpt2 != null)))
			{
				currentDeclarationIndex++;
				return boundAnonymousPropertyDeclaration;
			}
			return null;
		}

		// Token: 0x06001D38 RID: 7480 RVA: 0x0009D690 File Offset: 0x0009B890
		[CompilerGenerated]
		private bool <VisitBinaryOperatorChildren>g__learnFromBooleanConstantTest|159_0(ref NullableWalker.<>c__DisplayClass159_0 A_1)
		{
			if (!this.IsConditionalState)
			{
				return false;
			}
			if (!A_1.leftConversion.IsIdentity)
			{
				return false;
			}
			BinaryOperatorKind binaryOperatorKind = A_1.binary.OperatorKind.Operator();
			if (binaryOperatorKind != BinaryOperatorKind.Equal && binaryOperatorKind != BinaryOperatorKind.NotEqual)
			{
				return false;
			}
			ConstantValue constantValue = A_1.binary.Right.ConstantValue;
			bool flag;
			if (constantValue != null && constantValue.IsBoolean)
			{
				this.UseRvalueOnly(A_1.leftOperand);
				NullableWalker.LocalState localState = this.StateWhenTrue.Clone();
				NullableWalker.LocalState localState2 = this.StateWhenFalse.Clone();
				base.Unsplit();
				this.Visit(A_1.binary.Right);
				this.UseRvalueOnly(A_1.binary.Right);
				base.SetConditionalState(localState, localState2);
				flag = binaryOperatorKind == BinaryOperatorKind.Equal == A_1.binary.Right.ConstantValue.BooleanValue;
			}
			else
			{
				ConstantValue constantValue2 = A_1.binary.Left.ConstantValue;
				if (constantValue2 == null || !constantValue2.IsBoolean)
				{
					return false;
				}
				base.Unsplit();
				this.UseRvalueOnly(A_1.leftOperand);
				this.Visit(A_1.binary.Right);
				this.UseRvalueOnly(A_1.binary.Right);
				flag = binaryOperatorKind == BinaryOperatorKind.Equal == A_1.binary.Left.ConstantValue.BooleanValue;
			}
			if (!flag && this.IsConditionalState)
			{
				base.SetConditionalState(this.StateWhenFalse, this.StateWhenTrue);
			}
			this.SetResult(A_1.binary, TypeWithState.ForType(A_1.binary.Type), TypeWithAnnotations.Create(A_1.binary.Type, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), true, default(bool?));
			return true;
		}

		// Token: 0x06001D39 RID: 7481 RVA: 0x0009D848 File Offset: 0x0009BA48
		[CompilerGenerated]
		private void <AfterLeftChildHasBeenVisited>g__visitOperandConversion|160_3(BoundExpression expr, BoundExpression operand, Conversion conversion, ParameterSymbol parameter, TypeWithState operandType, ref NullableWalker.<>c__DisplayClass160_0 A_6)
		{
			TypeWithAnnotations typeWithAnnotations = parameter.TypeWithAnnotations;
			if (A_6.isLifted && typeWithAnnotations.Type.IsNonNullableValueType())
			{
				typeWithAnnotations = TypeWithAnnotations.Create(this.MakeNullableOf(typeWithAnnotations), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
			}
			this.VisitConversion(expr as BoundConversion, operand, conversion, typeWithAnnotations, operandType, true, false, false, NullableWalker.AssignmentKind.Argument, parameter, true, true, false, default(Optional<NullableWalker.LocalState>), false, null);
		}

		// Token: 0x06001D3A RID: 7482 RVA: 0x0009D8B4 File Offset: 0x0009BAB4
		[CompilerGenerated]
		private void <AfterLeftChildHasBeenVisited>g__visitOperandConversion|160_0(BoundExpression expr, BoundExpression operand, Conversion conversion, TypeWithState operandType)
		{
			if (expr.Type != null)
			{
				this.VisitConversion(expr as BoundConversion, operand, conversion, TypeWithAnnotations.Create(expr.Type, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), operandType, true, false, false, NullableWalker.AssignmentKind.Argument, null, true, true, false, default(Optional<NullableWalker.LocalState>), false, null);
			}
		}

		// Token: 0x06001D3B RID: 7483 RVA: 0x0009D904 File Offset: 0x0009BB04
		[CompilerGenerated]
		private void <AfterLeftChildHasBeenVisited>g__splitAndLearnFromNonNullTest|160_1(BoundExpression operandComparedToNonNull, bool whenTrue)
		{
			ArrayBuilder<int> instance = ArrayBuilder<int>.GetInstance();
			this.GetSlotsToMarkAsNotNullable(operandComparedToNonNull, instance);
			if (instance.Count != 0)
			{
				base.Split();
				ref NullableWalker.LocalState ptr = (ref whenTrue ? ref this.StateWhenTrue : ref this.StateWhenFalse);
				NullableWalker.MarkSlotsAsNotNull(instance, ref ptr);
			}
			instance.Free();
		}

		// Token: 0x06001D3C RID: 7484 RVA: 0x0009D94C File Offset: 0x0009BB4C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[CompilerGenerated]
		private TypeSymbol <AfterLeftChildHasBeenVisited>g__getTypeIfContainingType|160_2([global::System.Runtime.CompilerServices.Nullable(1)] TypeSymbol baseType, TypeSymbol derivedType)
		{
			if (derivedType == null)
			{
				return null;
			}
			derivedType = derivedType.StrippedType();
			CompoundUseSiteInfo<AssemblySymbol> discarded = CompoundUseSiteInfo<AssemblySymbol>.Discarded;
			Conversion conversion = this._conversions.ClassifyBuiltInConversion(derivedType, baseType, ref discarded);
			if (conversion.Exists && !conversion.IsExplicit)
			{
				return derivedType;
			}
			return null;
		}

		// Token: 0x06001D3D RID: 7485 RVA: 0x0009D991 File Offset: 0x0009BB91
		[CompilerGenerated]
		internal static IEnumerable<Symbol> <MarkDependentSlotsNotNull>g__getMembers|168_0(TypeSymbol type)
		{
			foreach (Symbol symbol in type.GetMembers())
			{
				yield return symbol;
			}
			ImmutableArray<Symbol>.Enumerator enumerator = default(ImmutableArray<Symbol>.Enumerator);
			NamedTypeSymbol baseType;
			for (baseType = NullableWalker.<MarkDependentSlotsNotNull>g__effectiveBase|168_1(type); baseType != null; baseType = baseType.BaseTypeNoUseSiteDiagnostics)
			{
				foreach (Symbol symbol2 in baseType.GetMembers())
				{
					yield return symbol2;
				}
				enumerator = default(ImmutableArray<Symbol>.Enumerator);
			}
			baseType = null;
			foreach (NamedTypeSymbol namedTypeSymbol in NullableWalker.<MarkDependentSlotsNotNull>g__inheritedInterfaces|168_2(type))
			{
				foreach (Symbol symbol3 in namedTypeSymbol.GetMembers())
				{
					yield return symbol3;
				}
				enumerator = default(ImmutableArray<Symbol>.Enumerator);
			}
			ImmutableArray<NamedTypeSymbol>.Enumerator enumerator2 = default(ImmutableArray<NamedTypeSymbol>.Enumerator);
			yield break;
		}

		// Token: 0x06001D3E RID: 7486 RVA: 0x0009D9A4 File Offset: 0x0009BBA4
		[CompilerGenerated]
		internal static NamedTypeSymbol <MarkDependentSlotsNotNull>g__effectiveBase|168_1(TypeSymbol type)
		{
			TypeParameterSymbol typeParameterSymbol = type as TypeParameterSymbol;
			NamedTypeSymbol namedTypeSymbol;
			if (typeParameterSymbol != null)
			{
				namedTypeSymbol = typeParameterSymbol.EffectiveBaseClassNoUseSiteDiagnostics;
			}
			else
			{
				namedTypeSymbol = type.BaseTypeNoUseSiteDiagnostics;
			}
			return namedTypeSymbol;
		}

		// Token: 0x06001D3F RID: 7487 RVA: 0x0009D9CC File Offset: 0x0009BBCC
		[CompilerGenerated]
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal static ImmutableArray<NamedTypeSymbol> <MarkDependentSlotsNotNull>g__inheritedInterfaces|168_2(TypeSymbol type)
		{
			TypeParameterSymbol typeParameterSymbol = type as TypeParameterSymbol;
			ImmutableArray<NamedTypeSymbol> immutableArray;
			if (typeParameterSymbol == null)
			{
				if (type != null)
				{
					if (type.TypeKind == TypeKind.Interface)
					{
						return type.AllInterfacesNoUseSiteDiagnostics;
					}
				}
				immutableArray = ImmutableArray<NamedTypeSymbol>.Empty;
			}
			else
			{
				immutableArray = typeParameterSymbol.AllEffectiveInterfacesNoUseSiteDiagnostics;
			}
			return immutableArray;
		}

		// Token: 0x06001D40 RID: 7488 RVA: 0x0009DA0C File Offset: 0x0009BC0C
		[CompilerGenerated]
		[return: TupleElementNames(new string[] { "ResultType", "LeftState" })]
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private ValueTuple<TypeSymbol, NullableFlowState> <VisitNullCoalescingOperator>g__getLeftResultType|171_0(TypeSymbol leftType, TypeSymbol rightType, ref NullableWalker.<>c__DisplayClass171_0 A_3)
		{
			BoundConversion boundConversion = A_3.node.RightOperand as BoundConversion;
			if ((boundConversion == null || boundConversion.ExplicitCastInCode) && this.GenerateConversionForConditionalOperator(A_3.node.LeftOperand, leftType, rightType, false).Exists)
			{
				return new ValueTuple<TypeSymbol, NullableFlowState>(rightType, NullableFlowState.NotNull);
			}
			Conversion conversion = this.GenerateConversionForConditionalOperator(A_3.node.RightOperand, rightType, leftType, true);
			return new ValueTuple<TypeSymbol, NullableFlowState>(leftType, NullableFlowState.NotNull);
		}

		// Token: 0x06001D41 RID: 7489 RVA: 0x0009DA7C File Offset: 0x0009BC7C
		[CompilerGenerated]
		[return: TupleElementNames(new string[] { "ResultType", "LeftState" })]
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private ValueTuple<TypeSymbol, NullableFlowState> <VisitNullCoalescingOperator>g__getResultStateWithRightType|171_1(TypeSymbol leftType, TypeSymbol rightType, ref NullableWalker.<>c__DisplayClass171_0 A_3)
		{
			Conversion conversion = this.GenerateConversionForConditionalOperator(A_3.node.LeftOperand, leftType, rightType, true);
			if (conversion.IsUserDefined)
			{
				TypeWithState typeWithState = this.VisitConversion(null, A_3.node.LeftOperand, conversion, TypeWithAnnotations.Create(rightType, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), TypeWithState.Create(leftType, NullableFlowState.NotNull), false, false, false, NullableWalker.AssignmentKind.Assignment, null, false, false, false, default(Optional<NullableWalker.LocalState>), false, null);
				return new ValueTuple<TypeSymbol, NullableFlowState>(typeWithState.Type, typeWithState.State);
			}
			return new ValueTuple<TypeSymbol, NullableFlowState>(rightType, NullableFlowState.NotNull);
		}

		// Token: 0x06001D42 RID: 7490 RVA: 0x0009DB00 File Offset: 0x0009BD00
		[CompilerGenerated]
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private ValueTuple<BoundExpression, Conversion, TypeWithState> <VisitConditionalOperatorCore>g__visitConditionalOperand|173_0(NullableWalker.LocalState state, BoundExpression operand)
		{
			base.SetState(state);
			ValueTuple<BoundExpression, Conversion> valueTuple = NullableWalker.RemoveConversion(operand, false);
			BoundExpression item = valueTuple.Item1;
			Conversion item2 = valueTuple.Item2;
			this.SnapshotWalkerThroughConversionGroup(operand, item);
			this.Visit(item);
			return new ValueTuple<BoundExpression, Conversion, TypeWithState>(item, item2, this.ResultType);
		}

		// Token: 0x06001D43 RID: 7491 RVA: 0x0009DB45 File Offset: 0x0009BD45
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[CompilerGenerated]
		[return: TupleElementNames(new string[] { "LValueType", "RValueType" })]
		private ValueTuple<TypeWithAnnotations, TypeWithState> <VisitConditionalOperatorCore>g__visitConditionalRefOperand|173_1(NullableWalker.LocalState state, [global::System.Runtime.CompilerServices.Nullable(1)] BoundExpression operand)
		{
			base.SetState(state);
			return new ValueTuple<TypeWithAnnotations, TypeWithState>(this.VisitLvalueWithAnnotations(operand), this.ResultType);
		}

		// Token: 0x06001D44 RID: 7492 RVA: 0x0009DB60 File Offset: 0x0009BD60
		[CompilerGenerated]
		internal static bool <LearnFromEqualsMethod>g__anyOverriddenMethodHasExplicitImplementation|180_0(MethodSymbol method)
		{
			for (MethodSymbol methodSymbol = method; methodSymbol != null; methodSymbol = methodSymbol.OverriddenMethod)
			{
				if (methodSymbol.IsExplicitInterfaceImplementation)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001D45 RID: 7493 RVA: 0x0009DB88 File Offset: 0x0009BD88
		[CompilerGenerated]
		internal static bool <LearnFromEqualsMethod>g__isWellKnownEqualityMethodOrImplementation|180_1(CSharpCompilation compilation, MethodSymbol method, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol receiverType, WellKnownMember wellKnownMember)
		{
			MethodSymbol methodSymbol = (MethodSymbol)compilation.GetWellKnownTypeMember(wellKnownMember);
			if (methodSymbol == null || receiverType == null)
			{
				return false;
			}
			NamedTypeSymbol containingType = methodSymbol.ContainingType;
			TypeWithAnnotations typeWithAnnotations = method.Parameters[0].TypeWithAnnotations;
			NamedTypeSymbol namedTypeSymbol = containingType.Construct(ImmutableArray.Create<TypeWithAnnotations>(typeWithAnnotations));
			MethodSymbol methodSymbol2 = methodSymbol.AsMember(namedTypeSymbol);
			if (methodSymbol2.Equals(method))
			{
				return true;
			}
			TypeSymbol typeSymbol = receiverType;
			while (typeSymbol != null && method != null)
			{
				Symbol symbol = typeSymbol.FindImplementationForInterfaceMember(methodSymbol2);
				if (symbol == null)
				{
					return false;
				}
				if (symbol.ContainingType.IsInterface)
				{
					return false;
				}
				for (MethodSymbol methodSymbol3 = method; methodSymbol3 != null; methodSymbol3 = methodSymbol3.OverriddenMethod)
				{
					if (methodSymbol3.Equals(symbol))
					{
						return true;
					}
				}
				while (!typeSymbol.Equals(symbol.ContainingType) && method != null)
				{
					if (typeSymbol.Equals(method.ContainingType))
					{
						method = method.OverriddenMethod;
					}
					typeSymbol = typeSymbol.BaseTypeNoUseSiteDiagnostics;
				}
				if (method != null && typeSymbol.Equals(method.ContainingType))
				{
					method = method.OverriddenMethod;
				}
				typeSymbol = typeSymbol.BaseTypeNoUseSiteDiagnostics;
			}
			return false;
		}

		// Token: 0x06001D46 RID: 7494 RVA: 0x0009DC90 File Offset: 0x0009BE90
		[CompilerGenerated]
		private void <LearnFromEqualsMethod>g__learnFromEqualsMethodArguments|180_2(BoundExpression left, TypeWithState leftType, BoundExpression right, TypeWithState rightType)
		{
			ConstantValue constantValue = left.ConstantValue;
			if (constantValue != null && constantValue.IsNull)
			{
				base.Split();
				this.LearnFromNullTest(right, ref this.StateWhenTrue);
				this.LearnFromNonNullTest(right, ref this.StateWhenFalse);
				return;
			}
			ConstantValue constantValue2 = right.ConstantValue;
			if (constantValue2 != null && constantValue2.IsNull)
			{
				base.Split();
				this.LearnFromNullTest(left, ref this.StateWhenTrue);
				this.LearnFromNonNullTest(left, ref this.StateWhenFalse);
				return;
			}
			if (leftType.MayBeNull && rightType.IsNotNull)
			{
				base.Split();
				this.LearnFromNonNullTest(left, ref this.StateWhenTrue);
				return;
			}
			if (rightType.MayBeNull && leftType.IsNotNull)
			{
				base.Split();
				this.LearnFromNonNullTest(right, ref this.StateWhenTrue);
			}
		}

		// Token: 0x06001D47 RID: 7495 RVA: 0x0009DD54 File Offset: 0x0009BF54
		[CompilerGenerated]
		private void <ApplyMemberPostConditions>g__applyMemberPostConditions|198_0(int receiverSlot, TypeSymbol type, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<string> members, ref NullableWalker.LocalState state)
		{
			if (members.IsEmpty)
			{
				return;
			}
			foreach (string text in members)
			{
				this.<ApplyMemberPostConditions>g__markMembersAsNotNull|198_1(receiverSlot, type, text, ref state);
			}
		}

		// Token: 0x06001D48 RID: 7496 RVA: 0x0009DD94 File Offset: 0x0009BF94
		[CompilerGenerated]
		private void <ApplyMemberPostConditions>g__markMembersAsNotNull|198_1(int receiverSlot, TypeSymbol type, string memberName, ref NullableWalker.LocalState state)
		{
			foreach (Symbol symbol in type.GetMembers(memberName))
			{
				if (symbol.IsStatic)
				{
					receiverSlot = 0;
				}
				SymbolKind kind = symbol.Kind;
				switch (kind)
				{
				case SymbolKind.Event:
				case SymbolKind.Label:
				case SymbolKind.Local:
				case SymbolKind.Method:
					continue;
				case SymbolKind.Field:
					break;
				default:
					if (kind != SymbolKind.Property)
					{
						continue;
					}
					break;
				}
				int orCreateSlot = this.GetOrCreateSlot(symbol, receiverSlot, false, true);
				if (orCreateSlot > 0)
				{
					state[orCreateSlot] = NullableFlowState.NotNull;
				}
			}
		}

		// Token: 0x06001D49 RID: 7497 RVA: 0x0009DE12 File Offset: 0x0009C012
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[CompilerGenerated]
		internal static bool <IsDisallowedNullAssignment>g__hasNoNonNullableCounterpart|203_0(TypeSymbol type)
		{
			return type != null && ((type.Kind == SymbolKind.TypeParameter && !type.IsReferenceType) || type.IsNullableTypeOrTypeParameter());
		}

		// Token: 0x06001D4A RID: 7498 RVA: 0x0009DE34 File Offset: 0x0009C034
		[CompilerGenerated]
		private FlowAnalysisAnnotations <VisitArgumentOutboundAssignmentsAndPostConditions>g__notNullBasedOnParameters|204_0(FlowAnalysisAnnotations parameterAnnotations, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ArrayBuilder<ParameterSymbol> notNullParametersOpt, ParameterSymbol parameter)
		{
			if (!this.IsAnalyzingAttribute && notNullParametersOpt != null)
			{
				ImmutableHashSet<string> notNullIfParameterNotNull = parameter.NotNullIfParameterNotNull;
				if (!notNullIfParameterNotNull.IsEmpty)
				{
					foreach (ParameterSymbol parameterSymbol in notNullParametersOpt)
					{
						if (notNullIfParameterNotNull.Contains(parameterSymbol.Name))
						{
							return FlowAnalysisAnnotations.NotNull;
						}
					}
				}
			}
			return parameterAnnotations;
		}

		// Token: 0x06001D4B RID: 7499 RVA: 0x0009DE88 File Offset: 0x0009C088
		[CompilerGenerated]
		private void <VisitArgumentOutboundAssignmentsAndPostConditions>g__trackNullableStateForAssignment|204_1(BoundExpression parameterValue, TypeWithAnnotations lValueType, int targetSlot, TypeWithState parameterWithState, bool isSuppressed, FlowAnalysisAnnotations parameterAnnotations)
		{
			if (!this.IsConditionalState && !NullableWalker.<VisitArgumentOutboundAssignmentsAndPostConditions>g__hasConditionalPostCondition|204_2(parameterAnnotations))
			{
				this.TrackNullableStateForAssignment(parameterValue, lValueType, targetSlot, parameterWithState.WithSuppression(isSuppressed), -1);
				return;
			}
			base.Split();
			NullableWalker.LocalState localState = this.StateWhenFalse.Clone();
			base.SetState(this.StateWhenTrue);
			this.TrackNullableStateForAssignment(parameterValue, lValueType, targetSlot, NullableWalker.<VisitArgumentOutboundAssignmentsAndPostConditions>g__applyPostConditionsWhenTrue|204_4(parameterWithState, parameterAnnotations).WithSuppression(isSuppressed), -1);
			NullableWalker.LocalState localState2 = this.State.Clone();
			base.SetState(localState);
			this.TrackNullableStateForAssignment(parameterValue, lValueType, targetSlot, NullableWalker.<VisitArgumentOutboundAssignmentsAndPostConditions>g__applyPostConditionsWhenFalse|204_5(parameterWithState, parameterAnnotations).WithSuppression(isSuppressed), -1);
			base.SetConditionalState(localState2, this.State);
		}

		// Token: 0x06001D4C RID: 7500 RVA: 0x0009DF32 File Offset: 0x0009C132
		[CompilerGenerated]
		internal static bool <VisitArgumentOutboundAssignmentsAndPostConditions>g__hasConditionalPostCondition|204_2(FlowAnalysisAnnotations annotations)
		{
			return (((annotations & FlowAnalysisAnnotations.MaybeNullWhenTrue) > FlowAnalysisAnnotations.None) ^ ((annotations & FlowAnalysisAnnotations.MaybeNullWhenFalse) > FlowAnalysisAnnotations.None)) || (((annotations & FlowAnalysisAnnotations.NotNullWhenTrue) > FlowAnalysisAnnotations.None) ^ ((annotations & FlowAnalysisAnnotations.NotNullWhenFalse) > FlowAnalysisAnnotations.None));
		}

		// Token: 0x06001D4D RID: 7501 RVA: 0x0009DF54 File Offset: 0x0009C154
		[CompilerGenerated]
		internal static TypeWithState <VisitArgumentOutboundAssignmentsAndPostConditions>g__applyPostConditionsUnconditionally|204_3(TypeWithState typeWithState, FlowAnalysisAnnotations annotations)
		{
			if ((annotations & FlowAnalysisAnnotations.MaybeNull) != FlowAnalysisAnnotations.None)
			{
				return TypeWithState.Create(typeWithState.Type, NullableFlowState.MaybeDefault);
			}
			if ((annotations & FlowAnalysisAnnotations.NotNull) == FlowAnalysisAnnotations.NotNull)
			{
				return TypeWithState.Create(typeWithState.Type, NullableFlowState.NotNull);
			}
			return typeWithState;
		}

		// Token: 0x06001D4E RID: 7502 RVA: 0x0009DF80 File Offset: 0x0009C180
		[CompilerGenerated]
		internal static TypeWithState <VisitArgumentOutboundAssignmentsAndPostConditions>g__applyPostConditionsWhenTrue|204_4(TypeWithState typeWithState, FlowAnalysisAnnotations annotations)
		{
			bool flag = (annotations & FlowAnalysisAnnotations.NotNullWhenTrue) > FlowAnalysisAnnotations.None;
			bool flag2 = (annotations & FlowAnalysisAnnotations.MaybeNullWhenTrue) > FlowAnalysisAnnotations.None;
			bool flag3 = (annotations & FlowAnalysisAnnotations.MaybeNullWhenFalse) > FlowAnalysisAnnotations.None;
			if (flag2 && (!flag3 || !flag))
			{
				return TypeWithState.Create(typeWithState.Type, NullableFlowState.MaybeDefault);
			}
			if (flag)
			{
				return TypeWithState.Create(typeWithState.Type, NullableFlowState.NotNull);
			}
			return typeWithState;
		}

		// Token: 0x06001D4F RID: 7503 RVA: 0x0009DFC8 File Offset: 0x0009C1C8
		[CompilerGenerated]
		internal static TypeWithState <VisitArgumentOutboundAssignmentsAndPostConditions>g__applyPostConditionsWhenFalse|204_5(TypeWithState typeWithState, FlowAnalysisAnnotations annotations)
		{
			bool flag = (annotations & FlowAnalysisAnnotations.NotNullWhenFalse) > FlowAnalysisAnnotations.None;
			bool flag2 = (annotations & FlowAnalysisAnnotations.MaybeNullWhenTrue) > FlowAnalysisAnnotations.None;
			if ((annotations & FlowAnalysisAnnotations.MaybeNullWhenFalse) > FlowAnalysisAnnotations.None && (!flag2 || !flag))
			{
				return TypeWithState.Create(typeWithState.Type, NullableFlowState.MaybeDefault);
			}
			if (flag)
			{
				return TypeWithState.Create(typeWithState.Type, NullableFlowState.NotNull);
			}
			return typeWithState;
		}

		// Token: 0x06001D50 RID: 7504 RVA: 0x0009E010 File Offset: 0x0009C210
		[CompilerGenerated]
		private void <VisitArgumentOutboundAssignmentsAndPostConditions>g__learnFromPostConditions|204_6(BoundExpression argument, TypeWithAnnotations parameterType, FlowAnalysisAnnotations parameterAnnotations)
		{
			bool flag = (parameterAnnotations & FlowAnalysisAnnotations.NotNullWhenTrue) > FlowAnalysisAnnotations.None;
			bool flag2 = (parameterAnnotations & FlowAnalysisAnnotations.NotNullWhenFalse) > FlowAnalysisAnnotations.None;
			bool flag3 = (parameterAnnotations & FlowAnalysisAnnotations.MaybeNullWhenTrue) > FlowAnalysisAnnotations.None;
			bool flag4 = (parameterAnnotations & FlowAnalysisAnnotations.MaybeNullWhenFalse) > FlowAnalysisAnnotations.None;
			if (flag3 && flag4 && !this.IsConditionalState && (!flag || !flag2))
			{
				this.LearnFromNullTest(argument, ref this.State);
				return;
			}
			if (flag && flag2 && !this.IsConditionalState && (!flag3 && !flag4))
			{
				this.LearnFromNonNullTest(argument, ref this.State);
				return;
			}
			if (flag || flag2 || flag3 || flag4)
			{
				base.Split();
				if (flag)
				{
					this.LearnFromNonNullTest(argument, ref this.StateWhenTrue);
				}
				if (flag2)
				{
					this.LearnFromNonNullTest(argument, ref this.StateWhenFalse);
				}
				if (flag3)
				{
					this.LearnFromNullTest(argument, ref this.StateWhenTrue);
				}
				if (flag4)
				{
					this.LearnFromNullTest(argument, ref this.StateWhenFalse);
				}
			}
		}

		// Token: 0x06001D51 RID: 7505 RVA: 0x0009E0CC File Offset: 0x0009C2CC
		[CompilerGenerated]
		private BoundExpression <GetArgumentsForMethodTypeInference>g__getArgumentForMethodTypeInference|210_0(BoundExpression argument, TypeWithAnnotations argumentType, [global::System.Runtime.CompilerServices.Nullable(0)] Optional<NullableWalker.LocalState> lambdaState)
		{
			if (argument.Kind == BoundKind.Lambda)
			{
				return NullableWalker.<GetArgumentsForMethodTypeInference>g__getUnboundLambda|210_1((BoundLambda)argument, NullableWalker.GetVariableState(this._variables, lambdaState.Value));
			}
			if (!argumentType.HasType)
			{
				return argument;
			}
			BoundLocal boundLocal = argument as BoundLocal;
			if (boundLocal != null)
			{
				if (boundLocal.DeclarationKind != BoundLocalDeclarationKind.WithInferredType)
				{
					goto IL_0075;
				}
			}
			else
			{
				BoundConditionalOperator boundConditionalOperator = argument as BoundConditionalOperator;
				if (boundConditionalOperator != null)
				{
					if (!boundConditionalOperator.WasTargetTyped)
					{
						goto IL_0075;
					}
				}
				else
				{
					BoundConvertedSwitchExpression boundConvertedSwitchExpression = argument as BoundConvertedSwitchExpression;
					if (boundConvertedSwitchExpression == null || !boundConvertedSwitchExpression.WasTargetTyped)
					{
						goto IL_0075;
					}
				}
			}
			bool flag = true;
			goto IL_0077;
			IL_0075:
			flag = false;
			IL_0077:
			if (flag)
			{
				return new BoundExpressionWithNullability(argument.Syntax, argument, NullableAnnotation.Oblivious, null);
			}
			return new BoundExpressionWithNullability(argument.Syntax, argument, argumentType.NullableAnnotation, argumentType.Type);
		}

		// Token: 0x06001D52 RID: 7506 RVA: 0x0009E17B File Offset: 0x0009C37B
		[CompilerGenerated]
		internal static UnboundLambda <GetArgumentsForMethodTypeInference>g__getUnboundLambda|210_1(BoundLambda expr, NullableWalker.VariableState variableState)
		{
			return expr.UnboundLambda.WithNullableState(variableState);
		}

		// Token: 0x06001D53 RID: 7507 RVA: 0x0009E18C File Offset: 0x0009C38C
		[CompilerGenerated]
		internal static bool <AsMemberOfType>g__tryAsMemberOfSingleType|217_0(NamedTypeSymbol singleType, [global::System.Runtime.CompilerServices.Nullable(2)] [NotNullWhen(true)] out Symbol result, ref NullableWalker.<>c__DisplayClass217_0 A_2)
		{
			if (!singleType.Equals(A_2.symbolContainer, TypeCompareKind.AllIgnoreOptions))
			{
				result = null;
				return false;
			}
			Symbol originalDefinition = A_2.symbol.OriginalDefinition;
			result = originalDefinition.SymbolAsMember(singleType);
			MethodSymbol methodSymbol = result as MethodSymbol;
			if (methodSymbol != null && methodSymbol.IsGenericMethod)
			{
				result = methodSymbol.Construct(((MethodSymbol)A_2.symbol).TypeArgumentsWithAnnotations);
			}
			return true;
		}

		// Token: 0x06001D55 RID: 7509 RVA: 0x0009E200 File Offset: 0x0009C400
		[CompilerGenerated]
		private void <TrackNullableStateOfTupleElements>g__trackState|224_0(BoundExpression value, FieldSymbol field, TypeWithState valueType, ref NullableWalker.<>c__DisplayClass224_0 A_4)
		{
			int orCreateSlot = this.GetOrCreateSlot(field, A_4.slot, false, true);
			this.TrackNullableStateForAssignment(value, field.TypeWithAnnotations, orCreateSlot, valueType, this.MakeSlot(value));
		}

		// Token: 0x06001D56 RID: 7510 RVA: 0x0009E234 File Offset: 0x0009C434
		[CompilerGenerated]
		private int <TrackNullableStateOfTupleElements>g__GetArgumentOrdinalFromParameterOrdinal|224_1(int parameterOrdinal, ref NullableWalker.<>c__DisplayClass224_0 A_2)
		{
			if (!A_2.argsToParamsOpt.IsDefault)
			{
				return A_2.argsToParamsOpt.IndexOf(parameterOrdinal);
			}
			return parameterOrdinal;
		}

		// Token: 0x06001D57 RID: 7511 RVA: 0x0009E254 File Offset: 0x0009C454
		[CompilerGenerated]
		private void <TrackNullableStateOfTupleConversion>g__trackConvertedValue|226_0(FieldSymbol targetField, Conversion conversion, FieldSymbol valueField, ref NullableWalker.<>c__DisplayClass226_0 A_4)
		{
			ConversionKind kind = conversion.Kind;
			switch (kind)
			{
			case ConversionKind.Identity:
			case ConversionKind.NullLiteral:
			case ConversionKind.ImplicitReference:
			case ConversionKind.Boxing:
			case ConversionKind.ExplicitReference:
			case ConversionKind.Unboxing:
				break;
			case ConversionKind.ImplicitNumeric:
			case ConversionKind.ImplicitEnumeration:
			case ConversionKind.ImplicitThrow:
			case ConversionKind.ImplicitPointerToVoid:
			case ConversionKind.ImplicitNullToPointer:
			case ConversionKind.ImplicitPointer:
			case ConversionKind.ImplicitDynamic:
			case ConversionKind.ExplicitDynamic:
			case ConversionKind.ImplicitConstant:
			case ConversionKind.AnonymousFunction:
			case ConversionKind.MethodGroup:
			case ConversionKind.ExplicitNumeric:
			case ConversionKind.ExplicitEnumeration:
				return;
			case ConversionKind.ImplicitTupleLiteral:
			case ConversionKind.ImplicitTuple:
			case ConversionKind.ExplicitTupleLiteral:
			case ConversionKind.ExplicitTuple:
			{
				int orCreateSlot = this.GetOrCreateSlot(targetField, A_4.slot, false, true);
				if (orCreateSlot <= 0)
				{
					return;
				}
				this.State[orCreateSlot] = NullableFlowState.NotNull;
				int orCreateSlot2 = this.GetOrCreateSlot(valueField, A_4.valueSlot, false, true);
				if (orCreateSlot2 > 0)
				{
					this.TrackNullableStateOfTupleConversion(A_4.conversionOpt, A_4.convertedNode, conversion, targetField.Type, valueField.Type, orCreateSlot, orCreateSlot2, A_4.assignmentKind, A_4.parameterOpt, A_4.reportWarnings);
					return;
				}
				return;
			}
			case ConversionKind.ImplicitNullable:
			case ConversionKind.ExplicitNullable:
			{
				TypeWithAnnotations typeWithAnnotations;
				if (!NullableWalker.AreNullableAndUnderlyingTypes(targetField.Type, valueField.Type, out typeWithAnnotations))
				{
					return;
				}
				int orCreateSlot3 = this.GetOrCreateSlot(targetField, A_4.slot, false, true);
				if (orCreateSlot3 <= 0)
				{
					return;
				}
				this.State[orCreateSlot3] = NullableFlowState.NotNull;
				int orCreateSlot4 = this.GetOrCreateSlot(valueField, A_4.valueSlot, false, true);
				if (orCreateSlot4 > 0)
				{
					int num = orCreateSlot3;
					TypeSymbol type = targetField.Type;
					BoundExpression boundExpression = null;
					typeWithAnnotations = valueField.TypeWithAnnotations;
					this.TrackNullableStateOfNullableValue(num, type, boundExpression, typeWithAnnotations.ToTypeWithState(), orCreateSlot4);
					return;
				}
				return;
			}
			case ConversionKind.ImplicitUserDefined:
			case ConversionKind.ExplicitUserDefined:
			{
				BoundConversion conversionOpt = A_4.conversionOpt;
				BoundExpression convertedNode = A_4.convertedNode;
				Conversion conversion2 = conversion;
				TypeWithAnnotations typeWithAnnotations2 = targetField.TypeWithAnnotations;
				TypeWithAnnotations typeWithAnnotations = valueField.TypeWithAnnotations;
				TypeWithState typeWithState = this.VisitUserDefinedConversion(conversionOpt, convertedNode, conversion2, typeWithAnnotations2, typeWithAnnotations.ToTypeWithState(), false, A_4.assignmentKind, A_4.parameterOpt, A_4.reportWarnings, A_4.reportWarnings, (A_4.conversionOpt ?? A_4.convertedNode).Syntax.GetLocation());
				int orCreateSlot5 = this.GetOrCreateSlot(targetField, A_4.slot, false, true);
				if (orCreateSlot5 > 0)
				{
					this.State[orCreateSlot5] = typeWithState.State;
					return;
				}
				return;
			}
			default:
				if (kind != ConversionKind.DefaultLiteral)
				{
					return;
				}
				break;
			}
			this.InheritNullableStateOfMember(A_4.slot, A_4.valueSlot, valueField, false, A_4.slot);
		}

		// Token: 0x06001D58 RID: 7512 RVA: 0x0009E482 File Offset: 0x0009C682
		[CompilerGenerated]
		private void <ReportNullabilityMismatchWithTargetDelegate>g__reportBadDelegateReturn|228_0(BindingDiagnosticBag bag, MethodSymbol targetInvokeMethod, MethodSymbol sourceInvokeMethod, bool topLevel, [TupleElementNames(new string[] { "targetType", "location" })] [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1, 1 })] ValueTuple<TypeSymbol, Location> arg)
		{
			this.ReportDiagnostic(ErrorCode.WRN_NullabilityMismatchInReturnTypeOfTargetDelegate, arg.Item2, new object[]
			{
				new FormattedSymbol(sourceInvokeMethod, SymbolDisplayFormat.MinimallyQualifiedFormat),
				arg.Item1
			});
		}

		// Token: 0x06001D59 RID: 7513 RVA: 0x0009E4B4 File Offset: 0x0009C6B4
		[CompilerGenerated]
		private void <ReportNullabilityMismatchWithTargetDelegate>g__reportBadDelegateParameter|228_1(BindingDiagnosticBag bag, MethodSymbol sourceInvokeMethod, MethodSymbol targetInvokeMethod, ParameterSymbol parameter, bool topLevel, [TupleElementNames(new string[] { "targetType", "location" })] [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1, 1 })] ValueTuple<TypeSymbol, Location> arg)
		{
			this.ReportDiagnostic(ErrorCode.WRN_NullabilityMismatchInParameterTypeOfTargetDelegate, arg.Item2, new object[]
			{
				NullableWalker.GetParameterAsDiagnosticArgument(parameter),
				NullableWalker.GetContainingSymbolAsDiagnosticArgument(parameter),
				arg.Item1
			});
		}

		// Token: 0x06001D5A RID: 7514 RVA: 0x0009E4EC File Offset: 0x0009C6EC
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[CompilerGenerated]
		[return: TupleElementNames(new string[] { "invokeSignature", null })]
		internal static ValueTuple<MethodSymbol, ImmutableArray<ParameterSymbol>> <VisitConversion>g__getDelegateOrFunctionPointerInfo|233_7(TypeSymbol targetType)
		{
			NamedTypeSymbol namedTypeSymbol = targetType as NamedTypeSymbol;
			if (namedTypeSymbol != null)
			{
				if (targetType.TypeKind == TypeKind.Delegate)
				{
					MethodSymbol delegateInvokeMethod = namedTypeSymbol.DelegateInvokeMethod;
					if (delegateInvokeMethod != null)
					{
						ImmutableArray<ParameterSymbol> parameters = delegateInvokeMethod.Parameters;
						return new ValueTuple<MethodSymbol, ImmutableArray<ParameterSymbol>>(delegateInvokeMethod, parameters);
					}
				}
				if (targetType.SpecialType != SpecialType.System_Delegate)
				{
					if (!(targetType is ErrorTypeSymbol))
					{
						goto IL_009E;
					}
					return new ValueTuple<MethodSymbol, ImmutableArray<ParameterSymbol>>(null, ImmutableArray<ParameterSymbol>.Empty);
				}
			}
			else
			{
				FunctionPointerTypeSymbol functionPointerTypeSymbol = targetType as FunctionPointerTypeSymbol;
				if (functionPointerTypeSymbol != null)
				{
					FunctionPointerMethodSymbol signature = functionPointerTypeSymbol.Signature;
					if (signature != null)
					{
						ImmutableArray<ParameterSymbol> parameters2 = signature.Parameters;
						return new ValueTuple<MethodSymbol, ImmutableArray<ParameterSymbol>>(signature, parameters2);
					}
				}
				else if (targetType == null)
				{
					goto IL_009E;
				}
				if (targetType.SpecialType != SpecialType.System_Delegate)
				{
					goto IL_009E;
				}
			}
			return new ValueTuple<MethodSymbol, ImmutableArray<ParameterSymbol>>(null, ImmutableArray<ParameterSymbol>.Empty);
			IL_009E:
			throw ExceptionUtilities.UnexpectedValue(targetType);
		}

		// Token: 0x06001D5B RID: 7515 RVA: 0x0009E5A0 File Offset: 0x0009C7A0
		[CompilerGenerated]
		internal static TypeWithState <VisitConversion>g__calculateResultType|233_0(TypeWithAnnotations targetTypeWithNullability, bool fromExplicitCast, NullableFlowState resultState, bool isSuppressed, TypeSymbol targetType)
		{
			if (isSuppressed)
			{
				resultState = NullableFlowState.NotNull;
			}
			else if (fromExplicitCast && targetTypeWithNullability.NullableAnnotation.IsAnnotated() && !targetType.IsNullableType())
			{
				resultState = ((targetType != null && targetType.IsTypeParameterDisallowingAnnotationInCSharp8()) ? NullableFlowState.MaybeDefault : NullableFlowState.MaybeNull);
			}
			return TypeWithState.Create(targetType, resultState);
		}

		// Token: 0x06001D5C RID: 7516 RVA: 0x0009E5E0 File Offset: 0x0009C7E0
		[CompilerGenerated]
		internal static NullableFlowState <VisitConversion>g__getReferenceConversionResultState|233_1(TypeWithAnnotations targetType, TypeWithState operandType)
		{
			NullableFlowState state = operandType.State;
			if (state != NullableFlowState.MaybeNull)
			{
				if (state == NullableFlowState.MaybeDefault)
				{
					TypeSymbol type = targetType.Type;
					if (type != null && !type.IsTypeParameterDisallowingAnnotationInCSharp8())
					{
						return NullableFlowState.MaybeNull;
					}
				}
			}
			else
			{
				TypeSymbol type2 = targetType.Type;
				if (type2 != null && type2.IsTypeParameterDisallowingAnnotationInCSharp8())
				{
					TypeSymbol type3 = operandType.Type;
					if (type3 == null || !type3.IsTypeParameterDisallowingAnnotationInCSharp8())
					{
						return NullableFlowState.MaybeDefault;
					}
					if (targetType.NullableAnnotation.IsNotAnnotated())
					{
						TypeParameterSymbol typeParameterSymbol = type3 as TypeParameterSymbol;
						NullableAnnotation nullableAnnotation;
						if (typeParameterSymbol != null && NullableWalker.<VisitConversion>g__dependsOnTypeParameter|233_5(typeParameterSymbol, (TypeParameterSymbol)targetType.Type, NullableAnnotation.NotAnnotated, out nullableAnnotation))
						{
							if (nullableAnnotation != NullableAnnotation.Annotated)
							{
								return NullableFlowState.MaybeNull;
							}
							return NullableFlowState.MaybeDefault;
						}
					}
				}
			}
			return state;
		}

		// Token: 0x06001D5D RID: 7517 RVA: 0x0009E678 File Offset: 0x0009C878
		[CompilerGenerated]
		internal static NullableFlowState <VisitConversion>g__getBoxingConversionResultState|233_2(TypeWithAnnotations targetType, TypeWithState operandType)
		{
			NullableFlowState state = operandType.State;
			if (state == NullableFlowState.MaybeNull)
			{
				TypeSymbol type = operandType.Type;
				if (type == null || !type.IsTypeParameterDisallowingAnnotationInCSharp8())
				{
					return NullableFlowState.MaybeDefault;
				}
				if (targetType.NullableAnnotation.IsNotAnnotated())
				{
					TypeParameterSymbol typeParameterSymbol = type as TypeParameterSymbol;
					if (typeParameterSymbol != null)
					{
						TypeParameterSymbol typeParameterSymbol2 = targetType.Type as TypeParameterSymbol;
						NullableAnnotation nullableAnnotation;
						if (typeParameterSymbol2 != null && NullableWalker.<VisitConversion>g__dependsOnTypeParameter|233_5(typeParameterSymbol, typeParameterSymbol2, NullableAnnotation.NotAnnotated, out nullableAnnotation))
						{
							if (nullableAnnotation != NullableAnnotation.Annotated)
							{
								return NullableFlowState.MaybeNull;
							}
							return NullableFlowState.MaybeDefault;
						}
					}
				}
			}
			return state;
		}

		// Token: 0x06001D5E RID: 7518 RVA: 0x0009E6E4 File Offset: 0x0009C8E4
		[CompilerGenerated]
		internal static NullableFlowState <VisitConversion>g__getUnboxingConversionResultState|233_3(TypeWithState operandType)
		{
			NullableFlowState state = operandType.State;
			if (state == NullableFlowState.MaybeNull)
			{
				return NullableFlowState.MaybeDefault;
			}
			return state;
		}

		// Token: 0x06001D5F RID: 7519 RVA: 0x0009E700 File Offset: 0x0009C900
		[CompilerGenerated]
		internal static NullableFlowState <VisitConversion>g__getConversionResultState|233_4(TypeWithState operandType)
		{
			NullableFlowState state = operandType.State;
			if (state == NullableFlowState.MaybeNull)
			{
				return NullableFlowState.MaybeDefault;
			}
			return state;
		}

		// Token: 0x06001D60 RID: 7520 RVA: 0x0009E71C File Offset: 0x0009C91C
		[CompilerGenerated]
		internal static bool <VisitConversion>g__dependsOnTypeParameter|233_5(TypeParameterSymbol typeParameter1, TypeParameterSymbol typeParameter2, NullableAnnotation typeParameter1Annotation, out NullableAnnotation annotation)
		{
			if (typeParameter1.Equals(typeParameter2, TypeCompareKind.AllIgnoreOptions))
			{
				annotation = typeParameter1Annotation;
				return true;
			}
			bool flag = false;
			NullableAnnotation nullableAnnotation = NullableAnnotation.Annotated;
			foreach (TypeWithAnnotations typeWithAnnotations in typeParameter1.ConstraintTypesNoUseSiteDiagnostics)
			{
				TypeParameterSymbol typeParameterSymbol = typeWithAnnotations.Type as TypeParameterSymbol;
				NullableAnnotation nullableAnnotation2;
				if (typeParameterSymbol != null && NullableWalker.<VisitConversion>g__dependsOnTypeParameter|233_5(typeParameterSymbol, typeParameter2, typeWithAnnotations.NullableAnnotation, out nullableAnnotation2))
				{
					flag = true;
					nullableAnnotation = nullableAnnotation.Meet(nullableAnnotation2);
				}
			}
			if (flag)
			{
				annotation = nullableAnnotation.Join(typeParameter1Annotation);
				return true;
			}
			annotation = NullableAnnotation.NotAnnotated;
			return false;
		}

		// Token: 0x06001D61 RID: 7521 RVA: 0x0009E7A4 File Offset: 0x0009C9A4
		[CompilerGenerated]
		private NullableFlowState <VisitConversion>g__visitNestedTargetTypedConstructs|233_6(ref NullableWalker.<>c__DisplayClass233_0 A_1)
		{
			BoundConditionalOperator boundConditionalOperator = A_1.conversionOperand as BoundConditionalOperator;
			if (boundConditionalOperator != null)
			{
				if (boundConditionalOperator.WasTargetTyped)
				{
					ImmutableArray<ValueTuple<NullableWalker.LocalState, TypeWithState, bool>> immutableArray = this.ConditionalInfoForConversion[boundConditionalOperator];
					BoundExpression consequence = boundConditionalOperator.Consequence;
					ValueTuple<BoundExpression, Conversion> valueTuple = NullableWalker.RemoveConversion(consequence, false);
					BoundExpression item = valueTuple.Item1;
					Conversion item2 = valueTuple.Item2;
					ref TypeWithState ptr = this.ConvertConditionalOperandOrSwitchExpressionArmResult(consequence, item, item2, A_1.targetTypeWithNullability, immutableArray[0].Item2, immutableArray[0].Item1, immutableArray[0].Item3);
					BoundExpression alternative = boundConditionalOperator.Alternative;
					ValueTuple<BoundExpression, Conversion> valueTuple2 = NullableWalker.RemoveConversion(alternative, false);
					BoundExpression item3 = valueTuple2.Item1;
					Conversion item4 = valueTuple2.Item2;
					TypeWithState typeWithState = this.ConvertConditionalOperandOrSwitchExpressionArmResult(alternative, item3, item4, A_1.targetTypeWithNullability, immutableArray[1].Item2, immutableArray[1].Item1, immutableArray[1].Item3);
					this.ConditionalInfoForConversion.Remove(boundConditionalOperator);
					return ptr.State.Join(typeWithState.State);
				}
			}
			else
			{
				BoundConvertedSwitchExpression boundConvertedSwitchExpression = A_1.conversionOperand as BoundConvertedSwitchExpression;
				if (boundConvertedSwitchExpression == null)
				{
					BoundObjectCreationExpression boundObjectCreationExpression = A_1.conversionOperand as BoundObjectCreationExpression;
					if (boundObjectCreationExpression != null)
					{
						if (!boundObjectCreationExpression.WasTargetTyped)
						{
							goto IL_023B;
						}
					}
					else if (!(A_1.conversionOperand is BoundUnconvertedObjectCreationExpression))
					{
						if (!(A_1.conversionOperand is BoundUnconvertedConditionalOperator) && !(A_1.conversionOperand is BoundUnconvertedSwitchExpression))
						{
							goto IL_023B;
						}
						return A_1.operandType.State;
					}
					return NullableFlowState.NotNull;
				}
				if (boundConvertedSwitchExpression.WasTargetTyped)
				{
					ImmutableArray<ValueTuple<NullableWalker.LocalState, TypeWithState, bool>> immutableArray2 = this.ConditionalInfoForConversion[boundConvertedSwitchExpression];
					ArrayBuilder<TypeWithState> instance = ArrayBuilder<TypeWithState>.GetInstance(immutableArray2.Length);
					for (int i = 0; i < immutableArray2.Length; i++)
					{
						ValueTuple<NullableWalker.LocalState, TypeWithState, bool> valueTuple3 = immutableArray2[i];
						NullableWalker.LocalState item5 = valueTuple3.Item1;
						TypeWithState item6 = valueTuple3.Item2;
						bool item7 = valueTuple3.Item3;
						BoundExpression value = boundConvertedSwitchExpression.SwitchArms[i].Value;
						ValueTuple<BoundExpression, Conversion> valueTuple4 = NullableWalker.RemoveConversion(value, false);
						BoundExpression item8 = valueTuple4.Item1;
						Conversion item9 = valueTuple4.Item2;
						instance.Add(this.ConvertConditionalOperandOrSwitchExpressionArmResult(value, item8, item9, A_1.targetTypeWithNullability, item6, item5, item7));
					}
					NullableFlowState nullableState = BestTypeInferrer.GetNullableState(instance);
					instance.Free();
					this.ConditionalInfoForConversion.Remove(boundConvertedSwitchExpression);
					return nullableState;
				}
			}
			IL_023B:
			throw ExceptionUtilities.UnexpectedValue(A_1.conversionOperand.Kind);
		}

		// Token: 0x06001D62 RID: 7522 RVA: 0x0009EA04 File Offset: 0x0009CC04
		[CompilerGenerated]
		internal static NullableWalker.VisitResult <TrackAnalyzedNullabilityThroughConversionGroup>g__withType|236_0(NullableWalker.VisitResult visitResult, TypeSymbol newType)
		{
			return new NullableWalker.VisitResult(TypeWithState.Create(newType, visitResult.RValueType.State), TypeWithAnnotations.Create(newType, visitResult.LValueType.NullableAnnotation, default(ImmutableArray<CustomModifier>)));
		}

		// Token: 0x06001D63 RID: 7523 RVA: 0x0009EA44 File Offset: 0x0009CC44
		[CompilerGenerated]
		internal static FlowAnalysisAnnotations <GetLValueAnnotations>g__getFieldAnnotations|257_0(FieldSymbol field)
		{
			PropertySymbol propertySymbol = field.AssociatedSymbol as PropertySymbol;
			if (propertySymbol == null)
			{
				return field.FlowAnalysisAnnotations;
			}
			return propertySymbol.GetFlowAnalysisAnnotations();
		}

		// Token: 0x06001D64 RID: 7524 RVA: 0x0009EA70 File Offset: 0x0009CC70
		[CompilerGenerated]
		private NullableWalker.DeconstructionVariable <GetDeconstructionAssignmentVariables>g__getDeconstructionAssignmentVariable|266_0(BoundExpression expr)
		{
			BoundKind kind = expr.Kind;
			if (kind - BoundKind.TupleLiteral <= 1)
			{
				return new NullableWalker.DeconstructionVariable(expr, this.GetDeconstructionAssignmentVariables((BoundTupleExpression)expr));
			}
			this.VisitLValue(expr);
			return new NullableWalker.DeconstructionVariable(expr, this.LvalueResultType);
		}

		// Token: 0x06001D65 RID: 7525 RVA: 0x0009EAB5 File Offset: 0x0009CCB5
		[CompilerGenerated]
		internal static TypeWithState <VisitUnaryOperator>g__adjustForLifting|296_0(TypeWithState argumentResult, ref NullableWalker.<>c__DisplayClass296_0 A_1)
		{
			return TypeWithState.Create(A_1.node.Type, A_1.node.OperatorKind.IsLifted() ? argumentResult.State : NullableFlowState.NotNull);
		}

		// Token: 0x06001D66 RID: 7526 RVA: 0x0009EAE4 File Offset: 0x0009CCE4
		[CompilerGenerated]
		private void <LearnFromDecisionDag>g__learnFromNonNullTest|397_1(int inputSlot, ref NullableWalker.LocalState state, ref NullableWalker.<>c__DisplayClass397_0 A_3)
		{
			this.LearnFromNonNullTest(inputSlot, ref state);
			BoundExpression boundExpression;
			if (A_3.originalInputMap.TryGetValue(inputSlot, ref boundExpression))
			{
				this.LearnFromNonNullTest(boundExpression, ref state);
			}
		}

		// Token: 0x06001D67 RID: 7527 RVA: 0x0009EB14 File Offset: 0x0009CD14
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[CompilerGenerated]
		private void <LearnFromDecisionDag>g__addToTempMap|397_2(BoundDagTemp output, int slot, TypeSymbol type, ref NullableWalker.<>c__DisplayClass397_0 A_4)
		{
			ValueTuple<int, TypeSymbol> valueTuple;
			if (!A_4.tempMap.TryGetValue(output, ref valueTuple))
			{
				A_4.tempMap.Add(output, new ValueTuple<int, TypeSymbol>(slot, type));
			}
		}

		// Token: 0x06001D68 RID: 7528 RVA: 0x0009EB48 File Offset: 0x0009CD48
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[CompilerGenerated]
		private bool <LearnFromDecisionDag>g__isDerivedType|397_3(TypeSymbol derivedType, TypeSymbol baseType, ref NullableWalker.<>c__DisplayClass397_0 A_3)
		{
			if (derivedType.IsErrorType() || baseType.IsErrorType())
			{
				return true;
			}
			CompoundUseSiteInfo<AssemblySymbol> discarded = CompoundUseSiteInfo<AssemblySymbol>.Discarded;
			ConversionKind kind = this._conversions.WithNullability(false).ClassifyConversionFromType(derivedType, baseType, ref discarded, false).Kind;
			return kind == ConversionKind.Identity || kind == ConversionKind.ImplicitReference || kind == ConversionKind.Boxing;
		}

		// Token: 0x06001D69 RID: 7529 RVA: 0x0009EBAC File Offset: 0x0009CDAC
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[CompilerGenerated]
		private void <LearnFromDecisionDag>g__gotoNodeWithCurrentState|397_4(BoundDecisionDagNode node, bool believedReachable, ref NullableWalker.<>c__DisplayClass397_0 A_3)
		{
			ValueTuple<NullableWalker.PossiblyConditionalState, bool> valueTuple;
			if (A_3.nodeStateMap.TryGetValue(node, ref valueTuple))
			{
				bool isConditionalState = this.IsConditionalState;
				bool isConditionalState2 = valueTuple.Item1.IsConditionalState;
				if (isConditionalState)
				{
					if (!isConditionalState2)
					{
						this.Join(ref this.StateWhenTrue, ref valueTuple.Item1.State);
						this.Join(ref this.StateWhenFalse, ref valueTuple.Item1.State);
					}
					else
					{
						this.Join(ref this.StateWhenTrue, ref valueTuple.Item1.StateWhenTrue);
						this.Join(ref this.StateWhenFalse, ref valueTuple.Item1.StateWhenFalse);
					}
				}
				else if (!isConditionalState2)
				{
					this.Join(ref this.State, ref valueTuple.Item1.State);
				}
				else
				{
					base.Split();
					this.Join(ref this.StateWhenTrue, ref valueTuple.Item1.StateWhenTrue);
					this.Join(ref this.StateWhenFalse, ref valueTuple.Item1.StateWhenFalse);
				}
				believedReachable |= valueTuple.Item2;
			}
			A_3.nodeStateMap[node] = new ValueTuple<NullableWalker.PossiblyConditionalState, bool>(NullableWalker.PossiblyConditionalState.Create(this), believedReachable);
		}

		// Token: 0x06001D6A RID: 7530 RVA: 0x0009ECD0 File Offset: 0x0009CED0
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[CompilerGenerated]
		private void <LearnFromDecisionDag>g__gotoNode|397_5(BoundDecisionDagNode node, NullableWalker.LocalState state, bool believedReachable, ref NullableWalker.<>c__DisplayClass397_0 A_4)
		{
			ValueTuple<NullableWalker.PossiblyConditionalState, bool> valueTuple;
			NullableWalker.PossiblyConditionalState item;
			if (A_4.nodeStateMap.TryGetValue(node, ref valueTuple))
			{
				item = valueTuple.Item1;
				if (item.IsConditionalState)
				{
					this.Join(ref item.StateWhenTrue, ref state);
					this.Join(ref item.StateWhenFalse, ref state);
				}
				else
				{
					this.Join(ref item.State, ref state);
				}
				believedReachable |= valueTuple.Item2;
			}
			else
			{
				item = new NullableWalker.PossiblyConditionalState(state);
			}
			A_4.nodeStateMap[node] = new ValueTuple<NullableWalker.PossiblyConditionalState, bool>(item, believedReachable);
		}

		// Token: 0x06001D6B RID: 7531 RVA: 0x0009ED58 File Offset: 0x0009CF58
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[CompilerGenerated]
		private int <LearnFromDecisionDag>g__makeDagTempSlot|397_6(TypeWithAnnotations type, BoundDagTemp temp, ref NullableWalker.<>c__DisplayClass397_0 A_3)
		{
			object obj = new ValueTuple<SyntaxNode, BoundDagTemp>(A_3.node, temp);
			return this.GetOrCreatePlaceholderSlot(obj, type);
		}

		// Token: 0x04000E43 RID: 3651
		private NullableWalker.Variables _variables;

		// Token: 0x04000E44 RID: 3652
		private readonly Binder _binder;

		// Token: 0x04000E45 RID: 3653
		private readonly Conversions _conversions;

		// Token: 0x04000E46 RID: 3654
		private readonly bool _useConstructorExitWarnings;

		// Token: 0x04000E47 RID: 3655
		private bool _useDelegateInvokeParameterTypes;

		// Token: 0x04000E48 RID: 3656
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private MethodSymbol _delegateInvokeMethod;

		// Token: 0x04000E49 RID: 3657
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 0, 1 })]
		private ArrayBuilder<ValueTuple<BoundReturnStatement, TypeWithAnnotations>> _returnTypesOpt;

		// Token: 0x04000E4A RID: 3658
		private static readonly TypeWithState _invalidType = TypeWithState.Create(ErrorTypeSymbol.UnknownResultType, NullableFlowState.NotNull);

		// Token: 0x04000E4B RID: 3659
		[TupleElementNames(new string[] { "Info", "Type" })]
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1, 0, 2 })]
		private readonly ImmutableDictionary<BoundExpression, ValueTuple<NullabilityInfo, TypeSymbol>>.Builder _analyzedNullabilityMapOpt;

		// Token: 0x04000E4C RID: 3660
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private readonly NullableWalker.SnapshotManager.Builder _snapshotBuilderOpt;

		// Token: 0x04000E4D RID: 3661
		private bool _disableNullabilityAnalysis;

		// Token: 0x04000E4E RID: 3662
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })]
		private PooledDictionary<BoundExpression, TypeWithState> _methodGroupReceiverMapOpt;

		// Token: 0x04000E4F RID: 3663
		[TupleElementNames(new string[] { "AwaitableExpression", "Result" })]
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1, 0, 1 })]
		private PooledDictionary<BoundAwaitableValuePlaceholder, ValueTuple<BoundExpression, NullableWalker.VisitResult>> _awaitablePlaceholdersOpt;

		// Token: 0x04000E50 RID: 3664
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1, 1 })]
		private PooledDictionary<MethodSymbol, NullableWalker.Variables> _nestedFunctionVariables;

		// Token: 0x04000E51 RID: 3665
		[TupleElementNames(new string[] { "State", "ResultType", "EndReachable" })]
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1, 0, 0 })]
		private PooledDictionary<BoundExpression, ImmutableArray<ValueTuple<NullableWalker.LocalState, TypeWithState, bool>>> _conditionalInfoForConversionOpt;

		// Token: 0x04000E52 RID: 3666
		private readonly bool _isSpeculative;

		// Token: 0x04000E53 RID: 3667
		private readonly bool _hasInitialState;

		// Token: 0x04000E54 RID: 3668
		private NullableWalker.VisitResult _visitResult;

		// Token: 0x04000E55 RID: 3669
		private NullableWalker.VisitResult _currentConditionalReceiverVisitResult;

		// Token: 0x04000E56 RID: 3670
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1, 1 })]
		private PooledDictionary<object, NullableWalker.PlaceholderLocal> _placeholderLocalsOpt;

		// Token: 0x04000E57 RID: 3671
		private bool _disableDiagnostics;

		// Token: 0x04000E58 RID: 3672
		private bool _expressionIsRead = true;

		// Token: 0x04000E59 RID: 3673
		private int _lastConditionalAccessSlot = -1;

		// Token: 0x020006EF RID: 1775
		[global::System.Runtime.CompilerServices.Nullable(0)]
		internal sealed class VariableState
		{
			// Token: 0x060084E4 RID: 34020 RVA: 0x001E1213 File Offset: 0x001DF413
			internal VariableState(NullableWalker.VariablesSnapshot variables, NullableWalker.LocalStateSnapshot variableNullableStates)
			{
				this.Variables = variables;
				this.VariableNullableStates = variableNullableStates;
			}

			// Token: 0x040021F7 RID: 8695
			internal readonly NullableWalker.VariablesSnapshot Variables;

			// Token: 0x040021F8 RID: 8696
			internal readonly NullableWalker.LocalStateSnapshot VariableNullableStates;
		}

		// Token: 0x020006F0 RID: 1776
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal readonly struct Data
		{
			// Token: 0x060084E5 RID: 34021 RVA: 0x001E1229 File Offset: 0x001DF429
			internal Data(int trackedEntries, bool requiredAnalysis)
			{
				this.TrackedEntries = trackedEntries;
				this.RequiredAnalysis = requiredAnalysis;
			}

			// Token: 0x040021F9 RID: 8697
			internal readonly int TrackedEntries;

			// Token: 0x040021FA RID: 8698
			internal readonly bool RequiredAnalysis;
		}

		// Token: 0x020006F1 RID: 1777
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
		private readonly struct VisitResult
		{
			// Token: 0x060084E6 RID: 34022 RVA: 0x001E1239 File Offset: 0x001DF439
			public VisitResult(TypeWithState rValueType, TypeWithAnnotations lValueType)
			{
				this.RValueType = rValueType;
				this.LValueType = lValueType;
			}

			// Token: 0x060084E7 RID: 34023 RVA: 0x001E124C File Offset: 0x001DF44C
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			public VisitResult(TypeSymbol type, NullableAnnotation annotation, NullableFlowState state)
			{
				this.RValueType = TypeWithState.Create(type, state);
				this.LValueType = TypeWithAnnotations.Create(type, annotation, default(ImmutableArray<CustomModifier>));
			}

			// Token: 0x060084E8 RID: 34024 RVA: 0x001E127C File Offset: 0x001DF47C
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			internal string GetDebuggerDisplay()
			{
				return string.Concat(new string[]
				{
					"{LValue: ",
					this.LValueType.GetDebuggerDisplay(),
					", RValue: ",
					this.RValueType.GetDebuggerDisplay(),
					"}"
				});
			}

			// Token: 0x040021FB RID: 8699
			public readonly TypeWithState RValueType;

			// Token: 0x040021FC RID: 8700
			public readonly TypeWithAnnotations LValueType;
		}

		// Token: 0x020006F2 RID: 1778
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[DebuggerDisplay("{VisitResult.GetDebuggerDisplay(), nq}")]
		private readonly struct VisitArgumentResult
		{
			// Token: 0x170026AC RID: 9900
			// (get) Token: 0x060084E9 RID: 34025 RVA: 0x001E12C8 File Offset: 0x001DF4C8
			public TypeWithState RValueType
			{
				get
				{
					return this.VisitResult.RValueType;
				}
			}

			// Token: 0x170026AD RID: 9901
			// (get) Token: 0x060084EA RID: 34026 RVA: 0x001E12D5 File Offset: 0x001DF4D5
			public TypeWithAnnotations LValueType
			{
				get
				{
					return this.VisitResult.LValueType;
				}
			}

			// Token: 0x060084EB RID: 34027 RVA: 0x001E12E2 File Offset: 0x001DF4E2
			public VisitArgumentResult(NullableWalker.VisitResult visitResult, Optional<NullableWalker.LocalState> stateForLambda)
			{
				this.VisitResult = visitResult;
				this.StateForLambda = stateForLambda;
			}

			// Token: 0x040021FD RID: 8701
			public readonly NullableWalker.VisitResult VisitResult;

			// Token: 0x040021FE RID: 8702
			public readonly Optional<NullableWalker.LocalState> StateForLambda;
		}

		// Token: 0x020006F3 RID: 1779
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private enum AssignmentKind
		{
			// Token: 0x04002200 RID: 8704
			Assignment,
			// Token: 0x04002201 RID: 8705
			Return,
			// Token: 0x04002202 RID: 8706
			Argument,
			// Token: 0x04002203 RID: 8707
			ForEachIterationVariable
		}

		// Token: 0x020006F4 RID: 1780
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private readonly struct CompareExchangeInfo
		{
			// Token: 0x060084EC RID: 34028 RVA: 0x001E12F2 File Offset: 0x001DF4F2
			public CompareExchangeInfo([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments, ImmutableArray<NullableWalker.VisitArgumentResult> results, ImmutableArray<int> argsToParamsOpt)
			{
				this.Arguments = arguments;
				this.Results = results;
				this.ArgsToParamsOpt = argsToParamsOpt;
			}

			// Token: 0x170026AE RID: 9902
			// (get) Token: 0x060084ED RID: 34029 RVA: 0x001E130C File Offset: 0x001DF50C
			public bool IsDefault
			{
				get
				{
					return this.Arguments.IsDefault || this.Results.IsDefault;
				}
			}

			// Token: 0x04002204 RID: 8708
			[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			public readonly ImmutableArray<BoundExpression> Arguments;

			// Token: 0x04002205 RID: 8709
			public readonly ImmutableArray<NullableWalker.VisitArgumentResult> Results;

			// Token: 0x04002206 RID: 8710
			public readonly ImmutableArray<int> ArgsToParamsOpt;
		}

		// Token: 0x020006F5 RID: 1781
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private sealed class MethodInferenceExtensions : MethodTypeInferrer.Extensions
		{
			// Token: 0x060084EE RID: 34030 RVA: 0x001E1339 File Offset: 0x001DF539
			internal MethodInferenceExtensions(NullableWalker walker)
			{
				this._walker = walker;
			}

			// Token: 0x060084EF RID: 34031 RVA: 0x001E1348 File Offset: 0x001DF548
			internal override TypeWithAnnotations GetTypeWithAnnotations(BoundExpression expr)
			{
				return TypeWithAnnotations.Create(expr.Type, NullableWalker.MethodInferenceExtensions.GetNullableAnnotation(expr), default(ImmutableArray<CustomModifier>));
			}

			// Token: 0x060084F0 RID: 34032 RVA: 0x001E1370 File Offset: 0x001DF570
			private static NullableAnnotation GetNullableAnnotation(BoundExpression expr)
			{
				BoundKind kind = expr.Kind;
				if (kind <= BoundKind.MethodGroup)
				{
					if (kind - BoundKind.DefaultLiteral > 1 && kind != BoundKind.Literal)
					{
						if (kind != BoundKind.MethodGroup)
						{
							return NullableAnnotation.Oblivious;
						}
					}
					else
					{
						ConstantValue constantValue = expr.ConstantValue;
						if (constantValue != null && !constantValue.IsNull)
						{
							return NullableAnnotation.Annotated;
						}
						return NullableAnnotation.NotAnnotated;
					}
				}
				else if (kind != BoundKind.UnconvertedObjectCreationExpression && kind != BoundKind.UnboundLambda)
				{
					if (kind != BoundKind.ExpressionWithNullability)
					{
						return NullableAnnotation.Oblivious;
					}
					return ((BoundExpressionWithNullability)expr).NullableAnnotation;
				}
				return NullableAnnotation.NotAnnotated;
			}

			// Token: 0x060084F1 RID: 34033 RVA: 0x001E13E4 File Offset: 0x001DF5E4
			internal override TypeWithAnnotations GetMethodGroupResultType(BoundMethodGroup group, MethodSymbol method)
			{
				TypeWithState typeWithState;
				if (this._walker.TryGetMethodGroupReceiverNullability(group.ReceiverOpt, out typeWithState) && !method.IsStatic)
				{
					method = (MethodSymbol)NullableWalker.AsMemberOfType(typeWithState.Type, method);
				}
				return method.ReturnTypeWithAnnotations;
			}

			// Token: 0x04002207 RID: 8711
			private readonly NullableWalker _walker;
		}

		// Token: 0x020006F6 RID: 1782
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private readonly struct DeconstructionVariable
		{
			// Token: 0x060084F2 RID: 34034 RVA: 0x001E1427 File Offset: 0x001DF627
			internal DeconstructionVariable(BoundExpression expression, TypeWithAnnotations type)
			{
				this.Expression = expression;
				this.Type = type;
				this.NestedVariables = null;
			}

			// Token: 0x060084F3 RID: 34035 RVA: 0x001E143E File Offset: 0x001DF63E
			internal DeconstructionVariable(BoundExpression expression, ArrayBuilder<NullableWalker.DeconstructionVariable> nestedVariables)
			{
				this.Expression = expression;
				this.Type = default(TypeWithAnnotations);
				this.NestedVariables = nestedVariables;
			}

			// Token: 0x04002208 RID: 8712
			internal readonly BoundExpression Expression;

			// Token: 0x04002209 RID: 8713
			internal readonly TypeWithAnnotations Type;

			// Token: 0x0400220A RID: 8714
			[global::System.Runtime.CompilerServices.Nullable(2)]
			internal readonly ArrayBuilder<NullableWalker.DeconstructionVariable> NestedVariables;
		}

		// Token: 0x020006F7 RID: 1783
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[global::System.Runtime.CompilerServices.Nullable(0)]
		internal sealed class LocalStateSnapshot
		{
			// Token: 0x060084F4 RID: 34036 RVA: 0x001E145A File Offset: 0x001DF65A
			internal LocalStateSnapshot(int id, NullableWalker.LocalStateSnapshot container, BitVector state)
			{
				this.Id = id;
				this.Container = container;
				this.State = state;
			}

			// Token: 0x0400220B RID: 8715
			internal readonly int Id;

			// Token: 0x0400220C RID: 8716
			internal readonly NullableWalker.LocalStateSnapshot Container;

			// Token: 0x0400220D RID: 8717
			internal readonly BitVector State;
		}

		// Token: 0x020006F8 RID: 1784
		[global::System.Runtime.CompilerServices.Nullable(0)]
		[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
		internal struct LocalState : LocalDataFlowPass<NullableWalker.LocalState, NullableWalker.LocalFunctionState>.ILocalDataFlowState, AbstractFlowPass<NullableWalker.LocalState, NullableWalker.LocalFunctionState>.ILocalState
		{
			// Token: 0x060084F5 RID: 34037 RVA: 0x001E1477 File Offset: 0x001DF677
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			private LocalState(int id, NullableWalker.LocalState.Boxed container, BitVector state)
			{
				this.Id = id;
				this._container = container;
				this._state = state;
			}

			// Token: 0x060084F6 RID: 34038 RVA: 0x001E1490 File Offset: 0x001DF690
			internal static NullableWalker.LocalState Create(NullableWalker.LocalStateSnapshot snapshot)
			{
				NullableWalker.LocalState.Boxed boxed = ((snapshot.Container == null) ? null : new NullableWalker.LocalState.Boxed(NullableWalker.LocalState.Create(snapshot.Container)));
				return new NullableWalker.LocalState(snapshot.Id, boxed, snapshot.State.Clone());
			}

			// Token: 0x060084F7 RID: 34039 RVA: 0x001E14D3 File Offset: 0x001DF6D3
			internal NullableWalker.LocalStateSnapshot CreateSnapshot()
			{
				int id = this.Id;
				NullableWalker.LocalState.Boxed container = this._container;
				return new NullableWalker.LocalStateSnapshot(id, (container != null) ? container.Value.CreateSnapshot() : null, this._state.Clone());
			}

			// Token: 0x170026AF RID: 9903
			// (get) Token: 0x060084F8 RID: 34040 RVA: 0x001E1502 File Offset: 0x001DF702
			public bool Reachable
			{
				get
				{
					return this._state[0];
				}
			}

			// Token: 0x170026B0 RID: 9904
			// (get) Token: 0x060084F9 RID: 34041 RVA: 0x001E1510 File Offset: 0x001DF710
			public bool NormalizeToBottom
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060084FA RID: 34042 RVA: 0x001E1513 File Offset: 0x001DF713
			public static NullableWalker.LocalState ReachableState(NullableWalker.Variables variables)
			{
				return NullableWalker.LocalState.CreateReachableOrUnreachableState(variables, true);
			}

			// Token: 0x060084FB RID: 34043 RVA: 0x001E151C File Offset: 0x001DF71C
			public static NullableWalker.LocalState UnreachableState(NullableWalker.Variables variables)
			{
				return NullableWalker.LocalState.CreateReachableOrUnreachableState(variables, false);
			}

			// Token: 0x060084FC RID: 34044 RVA: 0x001E1528 File Offset: 0x001DF728
			private static NullableWalker.LocalState CreateReachableOrUnreachableState(NullableWalker.Variables variables, bool reachable)
			{
				NullableWalker.LocalState.Boxed boxed = ((variables.Container == null) ? null : new NullableWalker.LocalState.Boxed(NullableWalker.LocalState.CreateReachableOrUnreachableState(variables.Container, reachable)));
				int num = (reachable ? variables.NextAvailableIndex : 1);
				return new NullableWalker.LocalState(variables.Id, boxed, NullableWalker.LocalState.CreateBitVector(num, reachable));
			}

			// Token: 0x060084FD RID: 34045 RVA: 0x001E1572 File Offset: 0x001DF772
			public NullableWalker.LocalState CreateNestedMethodState(NullableWalker.Variables variables)
			{
				return new NullableWalker.LocalState(variables.Id, new NullableWalker.LocalState.Boxed(this), NullableWalker.LocalState.CreateBitVector(variables.NextAvailableIndex, true));
			}

			// Token: 0x060084FE RID: 34046 RVA: 0x001E1598 File Offset: 0x001DF798
			private static BitVector CreateBitVector(int capacity, bool reachable)
			{
				if (capacity < 1)
				{
					capacity = 1;
				}
				BitVector bitVector = BitVector.Create(capacity * 2);
				bitVector[0] = reachable;
				return bitVector;
			}

			// Token: 0x170026B1 RID: 9905
			// (get) Token: 0x060084FF RID: 34047 RVA: 0x001E15BF File Offset: 0x001DF7BF
			private int Capacity
			{
				get
				{
					return this._state.Capacity / 2;
				}
			}

			// Token: 0x06008500 RID: 34048 RVA: 0x001E15CE File Offset: 0x001DF7CE
			private void EnsureCapacity(int capacity)
			{
				this._state.EnsureCapacity(capacity * 2);
			}

			// Token: 0x06008501 RID: 34049 RVA: 0x001E15E0 File Offset: 0x001DF7E0
			public bool HasVariable(int slot)
			{
				if (slot <= 0)
				{
					return false;
				}
				ValueTuple<int, int> valueTuple = NullableWalker.Variables.DeconstructSlot(slot);
				int item = valueTuple.Item1;
				int item2 = valueTuple.Item2;
				return this.HasVariable(item, item2);
			}

			// Token: 0x06008502 RID: 34050 RVA: 0x001E160E File Offset: 0x001DF80E
			private bool HasVariable(int id, int index)
			{
				if (this.Id > id)
				{
					return this._container.Value.HasValue(id, index);
				}
				return this.Id == id;
			}

			// Token: 0x06008503 RID: 34051 RVA: 0x001E1638 File Offset: 0x001DF838
			public bool HasValue(int slot)
			{
				if (slot <= 0)
				{
					return false;
				}
				ValueTuple<int, int> valueTuple = NullableWalker.Variables.DeconstructSlot(slot);
				int item = valueTuple.Item1;
				int item2 = valueTuple.Item2;
				return this.HasValue(item, item2);
			}

			// Token: 0x06008504 RID: 34052 RVA: 0x001E1666 File Offset: 0x001DF866
			private bool HasValue(int id, int index)
			{
				if (this.Id != id)
				{
					return this._container.Value.HasValue(id, index);
				}
				return index < this.Capacity;
			}

			// Token: 0x06008505 RID: 34053 RVA: 0x001E1690 File Offset: 0x001DF890
			public void Normalize(NullableWalker walker, NullableWalker.Variables variables)
			{
				if (this.Id != variables.Id)
				{
					this.Normalize(walker, variables.Container);
					return;
				}
				NullableWalker.LocalState.Boxed container = this._container;
				if (container != null)
				{
					container.Value.Normalize(walker, variables.Container);
				}
				int capacity = this.Capacity;
				this.EnsureCapacity(variables.NextAvailableIndex);
				this.Populate(walker, capacity);
			}

			// Token: 0x06008506 RID: 34054 RVA: 0x001E16F1 File Offset: 0x001DF8F1
			public void PopulateAll(NullableWalker walker)
			{
				NullableWalker.LocalState.Boxed container = this._container;
				if (container != null)
				{
					container.Value.PopulateAll(walker);
				}
				this.Populate(walker, 1);
			}

			// Token: 0x06008507 RID: 34055 RVA: 0x001E1714 File Offset: 0x001DF914
			private void Populate(NullableWalker walker, int start)
			{
				int capacity = this.Capacity;
				for (int i = start; i < capacity; i++)
				{
					int num = NullableWalker.Variables.ConstructSlot(this.Id, i);
					this.SetValue(this.Id, i, walker.GetDefaultState(ref this, num));
				}
			}

			// Token: 0x170026B2 RID: 9906
			public NullableFlowState this[int slot]
			{
				get
				{
					ValueTuple<int, int> valueTuple = NullableWalker.Variables.DeconstructSlot(slot);
					int item = valueTuple.Item1;
					int item2 = valueTuple.Item2;
					return this.GetValue(item, item2);
				}
				set
				{
					ValueTuple<int, int> valueTuple = NullableWalker.Variables.DeconstructSlot(slot);
					int item = valueTuple.Item1;
					int item2 = valueTuple.Item2;
					this.SetValue(item, item2, value);
				}
			}

			// Token: 0x0600850A RID: 34058 RVA: 0x001E17AC File Offset: 0x001DF9AC
			private NullableFlowState GetValue(int id, int index)
			{
				if (this.Id != id)
				{
					return this._container.Value.GetValue(id, index);
				}
				if (index < this.Capacity && this.Reachable)
				{
					index *= 2;
					bool flag = this._state[index + 1];
					bool flag2 = this._state[index];
					NullableFlowState nullableFlowState;
					if (!flag)
					{
						if (flag2)
						{
							nullableFlowState = NullableFlowState.MaybeNull;
						}
						else
						{
							nullableFlowState = NullableFlowState.NotNull;
						}
					}
					else
					{
						if (!flag2)
						{
							throw ExceptionUtilities.UnexpectedValue(index);
						}
						nullableFlowState = NullableFlowState.MaybeDefault;
					}
					return nullableFlowState;
				}
				return NullableFlowState.NotNull;
			}

			// Token: 0x0600850B RID: 34059 RVA: 0x001E182C File Offset: 0x001DFA2C
			private void SetValue(int id, int index, NullableFlowState value)
			{
				if (this.Id != id)
				{
					this._container.Value.SetValue(id, index, value);
					return;
				}
				if (!this.Reachable)
				{
					return;
				}
				index *= 2;
				this._state[index] = value > NullableFlowState.NotNull;
				this._state[index + 1] = value == NullableFlowState.MaybeDefault;
			}

			// Token: 0x0600850C RID: 34060 RVA: 0x001E1888 File Offset: 0x001DFA88
			internal void ForEach<[global::System.Runtime.CompilerServices.Nullable(2)] TArg>(Action<int, TArg> action, TArg arg)
			{
				NullableWalker.LocalState.Boxed container = this._container;
				if (container != null)
				{
					container.Value.ForEach<TArg>(action, arg);
				}
				for (int i = 1; i < this.Capacity; i++)
				{
					action.Invoke(NullableWalker.Variables.ConstructSlot(this.Id, i), arg);
				}
			}

			// Token: 0x0600850D RID: 34061 RVA: 0x001E18D4 File Offset: 0x001DFAD4
			internal NullableWalker.LocalState GetStateForVariables(int id)
			{
				NullableWalker.LocalState localState = this;
				while (localState.Id != id)
				{
					localState = localState._container.Value;
				}
				return localState;
			}

			// Token: 0x0600850E RID: 34062 RVA: 0x001E1900 File Offset: 0x001DFB00
			public NullableWalker.LocalState Clone()
			{
				NullableWalker.LocalState.Boxed boxed = ((this._container == null) ? null : new NullableWalker.LocalState.Boxed(this._container.Value.Clone()));
				return new NullableWalker.LocalState(this.Id, boxed, this._state.Clone());
			}

			// Token: 0x0600850F RID: 34063 RVA: 0x001E1948 File Offset: 0x001DFB48
			public bool Join(in NullableWalker.LocalState other)
			{
				bool flag = false;
				if (this._container != null && this._container.Value.Join(in other._container.Value))
				{
					flag = true;
				}
				if (this._state.UnionWith(in other._state))
				{
					flag = true;
				}
				return flag;
			}

			// Token: 0x06008510 RID: 34064 RVA: 0x001E1994 File Offset: 0x001DFB94
			public bool Meet(in NullableWalker.LocalState other)
			{
				bool flag = false;
				if (this._container != null && this._container.Value.Meet(in other._container.Value))
				{
					flag = true;
				}
				if (this._state.IntersectWith(in other._state))
				{
					flag = true;
				}
				return flag;
			}

			// Token: 0x06008511 RID: 34065 RVA: 0x001E19E0 File Offset: 0x001DFBE0
			internal string GetDebuggerDisplay()
			{
				PooledStringBuilder instance = PooledStringBuilder.GetInstance();
				StringBuilder builder = instance.Builder;
				builder.Append(" ");
				for (int i = Math.Min(this.Capacity, 8) - 1; i >= 0; i--)
				{
					builder.Append(this._state[i * 2] ? '?' : '!');
				}
				return instance.ToStringAndFree();
			}

			// Token: 0x06008512 RID: 34066 RVA: 0x001E1A44 File Offset: 0x001DFC44
			internal string Dump(NullableWalker.Variables variables)
			{
				if (!this.Reachable)
				{
					return "unreachable";
				}
				if (this.Id != variables.Id)
				{
					return "invalid";
				}
				PooledStringBuilder instance = PooledStringBuilder.GetInstance();
				this.Dump(instance, variables);
				return instance.ToStringAndFree();
			}

			// Token: 0x06008513 RID: 34067 RVA: 0x001E1A8C File Offset: 0x001DFC8C
			private void Dump(StringBuilder builder, NullableWalker.Variables variables)
			{
				NullableWalker.LocalState.<>c__DisplayClass38_0 CS$<>8__locals1;
				CS$<>8__locals1.variables = variables;
				NullableWalker.LocalState.Boxed container = this._container;
				if (container != null)
				{
					container.Value.Dump(builder, CS$<>8__locals1.variables.Container);
				}
				for (int i = 1; i < this.Capacity; i++)
				{
					string text = NullableWalker.LocalState.<Dump>g__getName|38_0(NullableWalker.Variables.ConstructSlot(this.Id, i), ref CS$<>8__locals1);
					if (text != null)
					{
						builder.Append(text);
						NullableFlowState value = this.GetValue(this.Id, i);
						string text2;
						if (value != NullableFlowState.MaybeNull)
						{
							if (value != NullableFlowState.MaybeDefault)
							{
								text2 = "!";
							}
							else
							{
								text2 = "??";
							}
						}
						else
						{
							text2 = "?";
						}
						string text3 = text2;
						builder.Append(text3);
					}
				}
			}

			// Token: 0x06008514 RID: 34068 RVA: 0x001E1B34 File Offset: 0x001DFD34
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			[CompilerGenerated]
			internal static string <Dump>g__getName|38_0(int slot, ref NullableWalker.LocalState.<>c__DisplayClass38_0 A_1)
			{
				LocalDataFlowPass<NullableWalker.LocalState, NullableWalker.LocalFunctionState>.VariableIdentifier variableIdentifier = A_1.variables[slot];
				string name = variableIdentifier.Symbol.Name;
				int containingSlot = variableIdentifier.ContainingSlot;
				if (containingSlot <= 0)
				{
					return name;
				}
				return NullableWalker.LocalState.<Dump>g__getName|38_0(containingSlot, ref A_1) + "." + name;
			}

			// Token: 0x0400220E RID: 8718
			internal readonly int Id;

			// Token: 0x0400220F RID: 8719
			[global::System.Runtime.CompilerServices.Nullable(2)]
			private readonly NullableWalker.LocalState.Boxed _container;

			// Token: 0x04002210 RID: 8720
			private BitVector _state;

			// Token: 0x020009CD RID: 2509
			[global::System.Runtime.CompilerServices.NullableContext(0)]
			private sealed class Boxed
			{
				// Token: 0x0600932B RID: 37675 RVA: 0x00201E78 File Offset: 0x00200078
				internal Boxed(NullableWalker.LocalState value)
				{
					this.Value = value;
				}

				// Token: 0x040029C5 RID: 10693
				internal NullableWalker.LocalState Value;
			}
		}

		// Token: 0x020006F9 RID: 1785
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal sealed class LocalFunctionState : AbstractFlowPass<NullableWalker.LocalState, NullableWalker.LocalFunctionState>.AbstractLocalFunctionState
		{
			// Token: 0x06008515 RID: 34069 RVA: 0x001E1B77 File Offset: 0x001DFD77
			public LocalFunctionState(NullableWalker.LocalState unreachableState)
				: base(unreachableState.Clone(), unreachableState.Clone())
			{
				this.StartingState = unreachableState;
			}

			// Token: 0x04002211 RID: 8721
			public NullableWalker.LocalState StartingState;
		}

		// Token: 0x020006FA RID: 1786
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private sealed class NullabilityInfoTypeComparer : IEqualityComparer<ValueTuple<NullabilityInfo, TypeSymbol>>
		{
			// Token: 0x06008516 RID: 34070 RVA: 0x001E1B94 File Offset: 0x001DFD94
			public bool Equals([TupleElementNames(new string[] { "info", "type" })] [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })] ValueTuple<NullabilityInfo, TypeSymbol> x, [TupleElementNames(new string[] { "info", "type" })] [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })] ValueTuple<NullabilityInfo, TypeSymbol> y)
			{
				return x.Item1.Equals(y.Item1) && SymbolEqualityComparer.ConsiderEverything.Equals(x.Item2, y.Item2);
			}

			// Token: 0x06008517 RID: 34071 RVA: 0x001E1BC2 File Offset: 0x001DFDC2
			public int GetHashCode([TupleElementNames(new string[] { "info", "type" })] [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })] ValueTuple<NullabilityInfo, TypeSymbol> obj)
			{
				return obj.GetHashCode();
			}

			// Token: 0x04002212 RID: 8722
			[global::System.Runtime.CompilerServices.Nullable(1)]
			public static readonly NullableWalker.NullabilityInfoTypeComparer Instance = new NullableWalker.NullabilityInfoTypeComparer();
		}

		// Token: 0x020006FB RID: 1787
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private sealed class ExpressionAndSymbolEqualityComparer : IEqualityComparer<ValueTuple<BoundNode, Symbol>>
		{
			// Token: 0x0600851A RID: 34074 RVA: 0x001E1BE5 File Offset: 0x001DFDE5
			private ExpressionAndSymbolEqualityComparer()
			{
			}

			// Token: 0x0600851B RID: 34075 RVA: 0x001E1BED File Offset: 0x001DFDED
			public bool Equals([TupleElementNames(new string[] { "expr", "symbol" })] [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2, 1 })] ValueTuple<BoundNode, Symbol> x, [TupleElementNames(new string[] { "expr", "symbol" })] [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2, 1 })] ValueTuple<BoundNode, Symbol> y)
			{
				return x.Item1 == y.Item1 && x.Item2 == y.Item2;
			}

			// Token: 0x0600851C RID: 34076 RVA: 0x001E1C0D File Offset: 0x001DFE0D
			public int GetHashCode([TupleElementNames(new string[] { "expr", "symbol" })] [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2, 1 })] ValueTuple<BoundNode, Symbol> obj)
			{
				return Hash.Combine<BoundNode>(obj.Item1, obj.Item2.GetHashCode());
			}

			// Token: 0x04002213 RID: 8723
			[global::System.Runtime.CompilerServices.Nullable(1)]
			internal static readonly NullableWalker.ExpressionAndSymbolEqualityComparer Instance = new NullableWalker.ExpressionAndSymbolEqualityComparer();
		}

		// Token: 0x020006FC RID: 1788
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private sealed class PlaceholderLocal : LocalSymbol
		{
			// Token: 0x0600851E RID: 34078 RVA: 0x001E1C31 File Offset: 0x001DFE31
			public PlaceholderLocal(Symbol containingSymbol, object identifier, TypeWithAnnotations type)
			{
				this._containingSymbol = containingSymbol;
				this._type = type;
				this._identifier = identifier;
			}

			// Token: 0x0600851F RID: 34079 RVA: 0x001E1C50 File Offset: 0x001DFE50
			public override bool Equals(Symbol obj, TypeCompareKind compareKind)
			{
				if (this == obj)
				{
					return true;
				}
				NullableWalker.PlaceholderLocal placeholderLocal = obj as NullableWalker.PlaceholderLocal;
				return placeholderLocal != null && this._identifier.Equals(placeholderLocal._identifier);
			}

			// Token: 0x06008520 RID: 34080 RVA: 0x001E1C80 File Offset: 0x001DFE80
			public override int GetHashCode()
			{
				return this._identifier.GetHashCode();
			}

			// Token: 0x170026B3 RID: 9907
			// (get) Token: 0x06008521 RID: 34081 RVA: 0x001E1C8D File Offset: 0x001DFE8D
			internal override SyntaxNode ScopeDesignatorOpt
			{
				get
				{
					return null;
				}
			}

			// Token: 0x170026B4 RID: 9908
			// (get) Token: 0x06008522 RID: 34082 RVA: 0x001E1C90 File Offset: 0x001DFE90
			public override Symbol ContainingSymbol
			{
				get
				{
					return this._containingSymbol;
				}
			}

			// Token: 0x170026B5 RID: 9909
			// (get) Token: 0x06008523 RID: 34083 RVA: 0x001E1C98 File Offset: 0x001DFE98
			public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
			{
				get
				{
					return ImmutableArray<SyntaxReference>.Empty;
				}
			}

			// Token: 0x170026B6 RID: 9910
			// (get) Token: 0x06008524 RID: 34084 RVA: 0x001E1C9F File Offset: 0x001DFE9F
			public override ImmutableArray<Location> Locations
			{
				get
				{
					return ImmutableArray<Location>.Empty;
				}
			}

			// Token: 0x170026B7 RID: 9911
			// (get) Token: 0x06008525 RID: 34085 RVA: 0x001E1CA6 File Offset: 0x001DFEA6
			public override TypeWithAnnotations TypeWithAnnotations
			{
				get
				{
					return this._type;
				}
			}

			// Token: 0x170026B8 RID: 9912
			// (get) Token: 0x06008526 RID: 34086 RVA: 0x001E1CAE File Offset: 0x001DFEAE
			internal override LocalDeclarationKind DeclarationKind
			{
				get
				{
					return LocalDeclarationKind.None;
				}
			}

			// Token: 0x170026B9 RID: 9913
			// (get) Token: 0x06008527 RID: 34087 RVA: 0x001E1CB4 File Offset: 0x001DFEB4
			internal override SyntaxToken IdentifierToken
			{
				get
				{
					throw ExceptionUtilities.Unreachable;
				}
			}

			// Token: 0x170026BA RID: 9914
			// (get) Token: 0x06008528 RID: 34088 RVA: 0x001E1CC6 File Offset: 0x001DFEC6
			internal override bool IsCompilerGenerated
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170026BB RID: 9915
			// (get) Token: 0x06008529 RID: 34089 RVA: 0x001E1CC9 File Offset: 0x001DFEC9
			internal override bool IsImportedFromMetadata
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170026BC RID: 9916
			// (get) Token: 0x0600852A RID: 34090 RVA: 0x001E1CCC File Offset: 0x001DFECC
			internal override bool IsPinned
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170026BD RID: 9917
			// (get) Token: 0x0600852B RID: 34091 RVA: 0x001E1CCF File Offset: 0x001DFECF
			public override RefKind RefKind
			{
				get
				{
					return RefKind.None;
				}
			}

			// Token: 0x170026BE RID: 9918
			// (get) Token: 0x0600852C RID: 34092 RVA: 0x001E1CD2 File Offset: 0x001DFED2
			internal override SynthesizedLocalKind SynthesizedKind
			{
				get
				{
					throw ExceptionUtilities.Unreachable;
				}
			}

			// Token: 0x0600852D RID: 34093 RVA: 0x001E1CD9 File Offset: 0x001DFED9
			internal override ConstantValue GetConstantValue(SyntaxNode node, LocalSymbol inProgress, BindingDiagnosticBag diagnostics = null)
			{
				return null;
			}

			// Token: 0x0600852E RID: 34094 RVA: 0x001E1CDC File Offset: 0x001DFEDC
			internal override ImmutableBindingDiagnostic<AssemblySymbol> GetConstantValueDiagnostics(BoundExpression boundInitValue)
			{
				return ImmutableBindingDiagnostic<AssemblySymbol>.Empty;
			}

			// Token: 0x0600852F RID: 34095 RVA: 0x001E1CE3 File Offset: 0x001DFEE3
			internal override SyntaxNode GetDeclaratorSyntax()
			{
				throw ExceptionUtilities.Unreachable;
			}

			// Token: 0x06008530 RID: 34096 RVA: 0x001E1CEA File Offset: 0x001DFEEA
			internal override LocalSymbol WithSynthesizedLocalKindAndSyntax(SynthesizedLocalKind kind, SyntaxNode syntax)
			{
				throw ExceptionUtilities.Unreachable;
			}

			// Token: 0x170026BF RID: 9919
			// (get) Token: 0x06008531 RID: 34097 RVA: 0x001E1CF1 File Offset: 0x001DFEF1
			internal override uint ValEscapeScope
			{
				get
				{
					throw ExceptionUtilities.Unreachable;
				}
			}

			// Token: 0x170026C0 RID: 9920
			// (get) Token: 0x06008532 RID: 34098 RVA: 0x001E1CF8 File Offset: 0x001DFEF8
			internal override uint RefEscapeScope
			{
				get
				{
					throw ExceptionUtilities.Unreachable;
				}
			}

			// Token: 0x04002214 RID: 8724
			private readonly Symbol _containingSymbol;

			// Token: 0x04002215 RID: 8725
			private readonly TypeWithAnnotations _type;

			// Token: 0x04002216 RID: 8726
			private readonly object _identifier;
		}

		// Token: 0x020006FD RID: 1789
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal sealed class SnapshotManager
		{
			// Token: 0x06008533 RID: 34099 RVA: 0x001E1CFF File Offset: 0x001DFEFF
			private SnapshotManager(ImmutableArray<NullableWalker.SharedWalkerState> walkerSharedStates, [TupleElementNames(new string[] { "position", "snapshot" })] ImmutableArray<ValueTuple<int, NullableWalker.Snapshot>> incrementalSnapshots, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 0, 2, 1, 1 })] ImmutableDictionary<ValueTuple<BoundNode, Symbol>, Symbol> updatedSymbolsMap)
			{
				this._walkerSharedStates = walkerSharedStates;
				this._incrementalSnapshots = incrementalSnapshots;
				this._updatedSymbolsMap = updatedSymbolsMap;
			}

			// Token: 0x06008534 RID: 34100 RVA: 0x001E1D1C File Offset: 0x001DFF1C
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1, 1 })]
			internal ValueTuple<NullableWalker.VariablesSnapshot, NullableWalker.LocalStateSnapshot> GetSnapshot(int position)
			{
				NullableWalker.Snapshot snapshotForPosition = this.GetSnapshotForPosition(position);
				return new ValueTuple<NullableWalker.VariablesSnapshot, NullableWalker.LocalStateSnapshot>(this._walkerSharedStates[snapshotForPosition.SharedStateIndex].Variables, snapshotForPosition.VariableState);
			}

			// Token: 0x06008535 RID: 34101 RVA: 0x001E1D58 File Offset: 0x001DFF58
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			internal TypeWithAnnotations? GetUpdatedTypeForLocalSymbol(SourceLocalSymbol symbol)
			{
				NullableWalker.Snapshot snapshotForPosition = this.GetSnapshotForPosition(symbol.IdentifierToken.SpanStart);
				TypeWithAnnotations typeWithAnnotations;
				if (this._walkerSharedStates[snapshotForPosition.SharedStateIndex].Variables.TryGetType(symbol, out typeWithAnnotations))
				{
					return new TypeWithAnnotations?(typeWithAnnotations);
				}
				return default(TypeWithAnnotations?);
			}

			// Token: 0x06008536 RID: 34102 RVA: 0x001E1DB0 File Offset: 0x001DFFB0
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			[return: global::System.Runtime.CompilerServices.Nullable(2)]
			internal NamedTypeSymbol GetUpdatedDelegateTypeForLambda(LambdaSymbol lambda)
			{
				Symbol symbol;
				if (this._updatedSymbolsMap.TryGetValue(new ValueTuple<BoundNode, Symbol>(null, lambda), out symbol))
				{
					return (NamedTypeSymbol)symbol;
				}
				return null;
			}

			// Token: 0x06008537 RID: 34103 RVA: 0x001E1DDB File Offset: 0x001DFFDB
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			internal bool TryGetUpdatedSymbol(BoundNode node, Symbol symbol, [global::System.Runtime.CompilerServices.Nullable(2)] [NotNullWhen(true)] out Symbol updatedSymbol)
			{
				return this._updatedSymbolsMap.TryGetValue(new ValueTuple<BoundNode, Symbol>(node, symbol), out updatedSymbol);
			}

			// Token: 0x06008538 RID: 34104 RVA: 0x001E1DF0 File Offset: 0x001DFFF0
			private NullableWalker.Snapshot GetSnapshotForPosition(int position)
			{
				int num = this._incrementalSnapshots.BinarySearch(position, NullableWalker.SnapshotManager.BinarySearchComparer);
				if (num < 0)
				{
					num = ~num - 1;
					if (num < 0)
					{
						num = 0;
					}
				}
				return this._incrementalSnapshots[num].Item2;
			}

			// Token: 0x04002217 RID: 8727
			private readonly ImmutableArray<NullableWalker.SharedWalkerState> _walkerSharedStates;

			// Token: 0x04002218 RID: 8728
			[TupleElementNames(new string[] { "position", "snapshot" })]
			private readonly ImmutableArray<ValueTuple<int, NullableWalker.Snapshot>> _incrementalSnapshots;

			// Token: 0x04002219 RID: 8729
			[global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 0, 2, 1, 1 })]
			private readonly ImmutableDictionary<ValueTuple<BoundNode, Symbol>, Symbol> _updatedSymbolsMap;

			// Token: 0x0400221A RID: 8730
			[TupleElementNames(new string[] { "position", "snapshot" })]
			[global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 0 })]
			private static readonly Func<ValueTuple<int, NullableWalker.Snapshot>, int, int> BinarySearchComparer = ([TupleElementNames(new string[] { "position", "snapshot" })] ValueTuple<int, NullableWalker.Snapshot> current, int target) => current.Item1.CompareTo(target);

			// Token: 0x020009CF RID: 2511
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			[global::System.Runtime.CompilerServices.Nullable(0)]
			internal sealed class Builder
			{
				// Token: 0x0600932C RID: 37676 RVA: 0x00201E88 File Offset: 0x00200088
				internal NullableWalker.SnapshotManager ToManagerAndFree()
				{
					this._symbolToSlot.Free();
					ImmutableArray<ValueTuple<int, NullableWalker.Snapshot>> immutableArray = this._incrementalSnapshots.SelectAsArray<KeyValuePair<int, NullableWalker.Snapshot>, ValueTuple<int, NullableWalker.Snapshot>>((KeyValuePair<int, NullableWalker.Snapshot> kvp) => new ValueTuple<int, NullableWalker.Snapshot>(kvp.Key, kvp.Value));
					ImmutableDictionary<ValueTuple<BoundNode, Symbol>, Symbol> immutableDictionary = this._updatedSymbolMap.ToImmutable();
					return new NullableWalker.SnapshotManager(this._walkerStates.ToImmutableAndFree(), immutableArray, immutableDictionary);
				}

				// Token: 0x0600932D RID: 37677 RVA: 0x00201EEC File Offset: 0x002000EC
				internal int EnterNewWalker(Symbol symbol)
				{
					int currentWalkerSlot = this._currentWalkerSlot;
					int num;
					if (this._symbolToSlot.TryGetValue(symbol, ref num))
					{
						this._currentWalkerSlot = num;
						return currentWalkerSlot;
					}
					this._currentWalkerSlot = this._symbolToSlot.Count;
					this._symbolToSlot.Add(symbol, this._currentWalkerSlot);
					return currentWalkerSlot;
				}

				// Token: 0x0600932E RID: 37678 RVA: 0x00201F3A File Offset: 0x0020013A
				internal void ExitWalker(NullableWalker.SharedWalkerState stableState, int previousSlot)
				{
					this._walkerStates.SetItem(this._currentWalkerSlot, stableState);
					this._currentWalkerSlot = previousSlot;
				}

				// Token: 0x0600932F RID: 37679 RVA: 0x00201F55 File Offset: 0x00200155
				[global::System.Runtime.CompilerServices.NullableContext(2)]
				internal void TakeIncrementalSnapshot(BoundNode node, NullableWalker.LocalState currentState)
				{
					if (node == null || node.WasCompilerGenerated)
					{
						return;
					}
					this._incrementalSnapshots[node.Syntax.SpanStart] = new NullableWalker.Snapshot(currentState.CreateSnapshot(), this._currentWalkerSlot);
				}

				// Token: 0x06009330 RID: 37680 RVA: 0x00201F8B File Offset: 0x0020018B
				internal void SetUpdatedSymbol(BoundNode node, Symbol originalSymbol, Symbol updatedSymbol)
				{
					this._updatedSymbolMap[NullableWalker.SnapshotManager.Builder.GetKey(node, originalSymbol)] = updatedSymbol;
				}

				// Token: 0x06009331 RID: 37681 RVA: 0x00201FA0 File Offset: 0x002001A0
				internal void RemoveSymbolIfPresent(BoundNode node, Symbol symbol)
				{
					this._updatedSymbolMap.Remove(NullableWalker.SnapshotManager.Builder.GetKey(node, symbol));
				}

				// Token: 0x06009332 RID: 37682 RVA: 0x00201FB5 File Offset: 0x002001B5
				[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2, 1 })]
				private static ValueTuple<BoundNode, Symbol> GetKey(BoundNode node, Symbol symbol)
				{
					if (node is BoundLambda && symbol is LambdaSymbol)
					{
						return new ValueTuple<BoundNode, Symbol>(null, symbol);
					}
					return new ValueTuple<BoundNode, Symbol>(node, symbol);
				}

				// Token: 0x040029C7 RID: 10695
				[global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 0, 2, 1, 1 })]
				private readonly ImmutableDictionary<ValueTuple<BoundNode, Symbol>, Symbol>.Builder _updatedSymbolMap = ImmutableDictionary.CreateBuilder<ValueTuple<BoundNode, Symbol>, Symbol>(NullableWalker.ExpressionAndSymbolEqualityComparer.Instance, SymbolEqualityComparer.ConsiderEverything);

				// Token: 0x040029C8 RID: 10696
				private readonly ArrayBuilder<NullableWalker.SharedWalkerState> _walkerStates = ArrayBuilder<NullableWalker.SharedWalkerState>.GetInstance();

				// Token: 0x040029C9 RID: 10697
				private readonly SortedDictionary<int, NullableWalker.Snapshot> _incrementalSnapshots = new SortedDictionary<int, NullableWalker.Snapshot>();

				// Token: 0x040029CA RID: 10698
				private readonly PooledDictionary<Symbol, int> _symbolToSlot = PooledDictionary<Symbol, int>.GetInstance();

				// Token: 0x040029CB RID: 10699
				private int _currentWalkerSlot = -1;
			}
		}

		// Token: 0x020006FE RID: 1790
		[global::System.Runtime.CompilerServices.Nullable(0)]
		internal struct SharedWalkerState
		{
			// Token: 0x0600853A RID: 34106 RVA: 0x001E1E49 File Offset: 0x001E0049
			internal SharedWalkerState(NullableWalker.VariablesSnapshot variables)
			{
				this.Variables = variables;
			}

			// Token: 0x0400221B RID: 8731
			internal readonly NullableWalker.VariablesSnapshot Variables;
		}

		// Token: 0x020006FF RID: 1791
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private readonly struct Snapshot
		{
			// Token: 0x0600853B RID: 34107 RVA: 0x001E1E52 File Offset: 0x001E0052
			internal Snapshot(NullableWalker.LocalStateSnapshot variableState, int sharedStateIndex)
			{
				this.VariableState = variableState;
				this.SharedStateIndex = sharedStateIndex;
			}

			// Token: 0x0400221C RID: 8732
			internal readonly NullableWalker.LocalStateSnapshot VariableState;

			// Token: 0x0400221D RID: 8733
			internal readonly int SharedStateIndex;
		}

		// Token: 0x02000700 RID: 1792
		[global::System.Runtime.CompilerServices.Nullable(0)]
		[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
		internal sealed class VariablesSnapshot
		{
			// Token: 0x0600853C RID: 34108 RVA: 0x001E1E62 File Offset: 0x001E0062
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			internal VariablesSnapshot(int id, NullableWalker.VariablesSnapshot container, Symbol symbol, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 0, 0, 1 })] ImmutableArray<KeyValuePair<LocalDataFlowPass<NullableWalker.LocalState, NullableWalker.LocalFunctionState>.VariableIdentifier, int>> variableSlot, [global::System.Runtime.CompilerServices.Nullable(1)] ImmutableDictionary<Symbol, TypeWithAnnotations> variableTypes)
			{
				this.Id = id;
				this.Container = container;
				this.Symbol = symbol;
				this.VariableSlot = variableSlot;
				this.VariableTypes = variableTypes;
			}

			// Token: 0x0600853D RID: 34109 RVA: 0x001E1E8F File Offset: 0x001E008F
			internal bool TryGetType(Symbol symbol, out TypeWithAnnotations type)
			{
				return this.VariableTypes.TryGetValue(symbol, out type);
			}

			// Token: 0x0600853E RID: 34110 RVA: 0x001E1EA0 File Offset: 0x001E00A0
			private string GetDebuggerDisplay()
			{
				object obj = this.Symbol ?? "<null>";
				return string.Format("Id={0}, Symbol={1}, Count={2}", this.Id, obj, this.VariableSlot.Length);
			}

			// Token: 0x0400221E RID: 8734
			internal readonly int Id;

			// Token: 0x0400221F RID: 8735
			[global::System.Runtime.CompilerServices.Nullable(2)]
			internal readonly NullableWalker.VariablesSnapshot Container;

			// Token: 0x04002220 RID: 8736
			[global::System.Runtime.CompilerServices.Nullable(2)]
			internal readonly Symbol Symbol;

			// Token: 0x04002221 RID: 8737
			[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 0, 0, 1 })]
			internal readonly ImmutableArray<KeyValuePair<LocalDataFlowPass<NullableWalker.LocalState, NullableWalker.LocalFunctionState>.VariableIdentifier, int>> VariableSlot;

			// Token: 0x04002222 RID: 8738
			internal readonly ImmutableDictionary<Symbol, TypeWithAnnotations> VariableTypes;
		}

		// Token: 0x02000701 RID: 1793
		[global::System.Runtime.CompilerServices.Nullable(0)]
		[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
		internal sealed class Variables
		{
			// Token: 0x0600853F RID: 34111 RVA: 0x001E1EE6 File Offset: 0x001E00E6
			internal static NullableWalker.Variables Create([global::System.Runtime.CompilerServices.Nullable(2)] Symbol symbol)
			{
				return new NullableWalker.Variables(0, null, symbol);
			}

			// Token: 0x06008540 RID: 34112 RVA: 0x001E1EF0 File Offset: 0x001E00F0
			internal static NullableWalker.Variables Create(NullableWalker.VariablesSnapshot snapshot)
			{
				NullableWalker.Variables variables = ((snapshot.Container == null) ? null : NullableWalker.Variables.Create(snapshot.Container));
				NullableWalker.Variables variables2 = new NullableWalker.Variables(snapshot.Id, variables, snapshot.Symbol);
				variables2.Populate(snapshot);
				return variables2;
			}

			// Token: 0x06008541 RID: 34113 RVA: 0x001E1F2D File Offset: 0x001E012D
			private int GetNextId()
			{
				return this.Id + 1;
			}

			// Token: 0x06008542 RID: 34114 RVA: 0x001E1F38 File Offset: 0x001E0138
			private void Populate(NullableWalker.VariablesSnapshot snapshot)
			{
				this._variableBySlot.AddMany(default(LocalDataFlowPass<NullableWalker.LocalState, NullableWalker.LocalFunctionState>.VariableIdentifier), snapshot.VariableSlot.Length);
				foreach (KeyValuePair<LocalDataFlowPass<NullableWalker.LocalState, NullableWalker.LocalFunctionState>.VariableIdentifier, int> keyValuePair in snapshot.VariableSlot)
				{
					LocalDataFlowPass<NullableWalker.LocalState, NullableWalker.LocalFunctionState>.VariableIdentifier key = keyValuePair.Key;
					int value = keyValuePair.Value;
					this._variableSlot.Add(key, value);
					this._variableBySlot[value] = key;
				}
				foreach (KeyValuePair<Symbol, TypeWithAnnotations> keyValuePair2 in snapshot.VariableTypes)
				{
					this._variableTypes.Add(keyValuePair2.Key, keyValuePair2.Value);
				}
			}

			// Token: 0x06008543 RID: 34115 RVA: 0x001E2014 File Offset: 0x001E0214
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			private Variables(int id, NullableWalker.Variables container, Symbol symbol)
			{
				this.Id = id;
				this.Container = container;
				this.Symbol = symbol;
			}

			// Token: 0x06008544 RID: 34116 RVA: 0x001E2067 File Offset: 0x001E0267
			internal void Free()
			{
				NullableWalker.Variables container = this.Container;
				if (container != null)
				{
					container.Free();
				}
				this._variableBySlot.Free();
				this._variableTypes.Free();
				this._variableSlot.Free();
			}

			// Token: 0x06008545 RID: 34117 RVA: 0x001E209B File Offset: 0x001E029B
			internal NullableWalker.VariablesSnapshot CreateSnapshot()
			{
				int id = this.Id;
				NullableWalker.Variables container = this.Container;
				return new NullableWalker.VariablesSnapshot(id, (container != null) ? container.CreateSnapshot() : null, this.Symbol, ImmutableArray.CreateRange<KeyValuePair<LocalDataFlowPass<NullableWalker.LocalState, NullableWalker.LocalFunctionState>.VariableIdentifier, int>>(this._variableSlot), ImmutableDictionary.CreateRange<Symbol, TypeWithAnnotations>(this._variableTypes));
			}

			// Token: 0x06008546 RID: 34118 RVA: 0x001E20D6 File Offset: 0x001E02D6
			internal NullableWalker.Variables CreateNestedMethodScope(MethodSymbol method)
			{
				return new NullableWalker.Variables(this.GetNextId(), this, method);
			}

			// Token: 0x06008547 RID: 34119 RVA: 0x001E20E8 File Offset: 0x001E02E8
			internal int RootSlot(int slot)
			{
				for (;;)
				{
					int containingSlot = this[slot].ContainingSlot;
					if (containingSlot == 0)
					{
						break;
					}
					slot = containingSlot;
				}
				return slot;
			}

			// Token: 0x06008548 RID: 34120 RVA: 0x001E210B File Offset: 0x001E030B
			internal bool TryGetValue([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] LocalDataFlowPass<NullableWalker.LocalState, NullableWalker.LocalFunctionState>.VariableIdentifier identifier, out int slot)
			{
				return this.GetVariablesForVariable(identifier).TryGetValueInternal(identifier, out slot);
			}

			// Token: 0x06008549 RID: 34121 RVA: 0x001E211C File Offset: 0x001E031C
			private bool TryGetValueInternal([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] LocalDataFlowPass<NullableWalker.LocalState, NullableWalker.LocalFunctionState>.VariableIdentifier identifier, out int slot)
			{
				int num;
				if (this._variableSlot.TryGetValue(identifier, ref num))
				{
					slot = NullableWalker.Variables.ConstructSlot(this.Id, num);
					return true;
				}
				slot = -1;
				return false;
			}

			// Token: 0x0600854A RID: 34122 RVA: 0x001E214D File Offset: 0x001E034D
			internal int Add([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] LocalDataFlowPass<NullableWalker.LocalState, NullableWalker.LocalFunctionState>.VariableIdentifier identifier)
			{
				return this.GetVariablesForVariable(identifier).AddInternal(identifier);
			}

			// Token: 0x0600854B RID: 34123 RVA: 0x001E215C File Offset: 0x001E035C
			private int AddInternal([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] LocalDataFlowPass<NullableWalker.LocalState, NullableWalker.LocalFunctionState>.VariableIdentifier identifier)
			{
				if (this.<AddInternal>g__getSlotDepth|22_0(identifier.ContainingSlot) >= 5)
				{
					return -1;
				}
				int nextAvailableIndex = this.NextAvailableIndex;
				if (nextAvailableIndex > 65535)
				{
					return -1;
				}
				this._variableSlot.Add(identifier, nextAvailableIndex);
				this._variableBySlot.Add(identifier);
				return NullableWalker.Variables.ConstructSlot(this.Id, nextAvailableIndex);
			}

			// Token: 0x0600854C RID: 34124 RVA: 0x001E21B0 File Offset: 0x001E03B0
			internal bool TryGetType(Symbol symbol, out TypeWithAnnotations type)
			{
				return this.GetVariablesContainingSymbol(symbol)._variableTypes.TryGetValue(symbol, ref type);
			}

			// Token: 0x0600854D RID: 34125 RVA: 0x001E21C5 File Offset: 0x001E03C5
			internal void SetType(Symbol symbol, TypeWithAnnotations type)
			{
				this.GetVariablesContainingSymbol(symbol)._variableTypes[symbol] = type;
			}

			// Token: 0x170026C1 RID: 9921
			[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			internal LocalDataFlowPass<NullableWalker.LocalState, NullableWalker.LocalFunctionState>.VariableIdentifier this[int slot]
			{
				[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
				get
				{
					ValueTuple<int, int> valueTuple = NullableWalker.Variables.DeconstructSlot(slot);
					int item = valueTuple.Item1;
					int item2 = valueTuple.Item2;
					return this.GetVariablesForId(item)._variableBySlot[item2];
				}
			}

			// Token: 0x170026C2 RID: 9922
			// (get) Token: 0x0600854F RID: 34127 RVA: 0x001E220E File Offset: 0x001E040E
			internal int NextAvailableIndex
			{
				get
				{
					return this._variableBySlot.Count;
				}
			}

			// Token: 0x06008550 RID: 34128 RVA: 0x001E221B File Offset: 0x001E041B
			internal int GetTotalVariableCount()
			{
				NullableWalker.Variables container = this.Container;
				return ((container != null) ? container.GetTotalVariableCount() : 0) + this._variableSlot.Count;
			}

			// Token: 0x06008551 RID: 34129 RVA: 0x001E223C File Offset: 0x001E043C
			internal void GetMembers([global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 0, 0, 1 })] ArrayBuilder<ValueTuple<LocalDataFlowPass<NullableWalker.LocalState, NullableWalker.LocalFunctionState>.VariableIdentifier, int>> builder, int containingSlot)
			{
				ValueTuple<int, int> valueTuple = NullableWalker.Variables.DeconstructSlot(containingSlot);
				int item = valueTuple.Item1;
				int i = valueTuple.Item2;
				ArrayBuilder<LocalDataFlowPass<NullableWalker.LocalState, NullableWalker.LocalFunctionState>.VariableIdentifier> variableBySlot = this.GetVariablesForId(item)._variableBySlot;
				for (i++; i < variableBySlot.Count; i++)
				{
					LocalDataFlowPass<NullableWalker.LocalState, NullableWalker.LocalFunctionState>.VariableIdentifier variableIdentifier = variableBySlot[i];
					if (variableIdentifier.ContainingSlot == containingSlot)
					{
						builder.Add(new ValueTuple<LocalDataFlowPass<NullableWalker.LocalState, NullableWalker.LocalFunctionState>.VariableIdentifier, int>(variableIdentifier, NullableWalker.Variables.ConstructSlot(item, i)));
					}
				}
			}

			// Token: 0x06008552 RID: 34130 RVA: 0x001E22A0 File Offset: 0x001E04A0
			private NullableWalker.Variables GetVariablesForVariable([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] LocalDataFlowPass<NullableWalker.LocalState, NullableWalker.LocalFunctionState>.VariableIdentifier identifier)
			{
				int containingSlot = identifier.ContainingSlot;
				if (containingSlot > 0)
				{
					return this.GetVariablesForId(NullableWalker.Variables.DeconstructSlot(containingSlot).Item1);
				}
				return this.GetVariablesContainingSymbol(identifier.Symbol);
			}

			// Token: 0x06008553 RID: 34131 RVA: 0x001E22D8 File Offset: 0x001E04D8
			private NullableWalker.Variables GetVariablesContainingSymbol(Symbol symbol)
			{
				if (symbol is LocalSymbol || symbol is ParameterSymbol)
				{
					MethodSymbol methodSymbol = symbol.ContainingSymbol as MethodSymbol;
					if (methodSymbol != null)
					{
						NullableWalker.Variables variablesForMethodScope = this.GetVariablesForMethodScope(methodSymbol);
						if (variablesForMethodScope != null)
						{
							return variablesForMethodScope;
						}
					}
				}
				return this.GetRootScope();
			}

			// Token: 0x06008554 RID: 34132 RVA: 0x001E2318 File Offset: 0x001E0518
			internal NullableWalker.Variables GetRootScope()
			{
				NullableWalker.Variables variables = this;
				for (;;)
				{
					NullableWalker.Variables container = variables.Container;
					if (container == null)
					{
						break;
					}
					variables = container;
				}
				return variables;
			}

			// Token: 0x06008555 RID: 34133 RVA: 0x001E2338 File Offset: 0x001E0538
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			private NullableWalker.Variables GetVariablesForId(int id)
			{
				NullableWalker.Variables variables = this;
				while (variables.Id != id)
				{
					variables = variables.Container;
					if (variables == null)
					{
						return null;
					}
				}
				return variables;
			}

			// Token: 0x06008556 RID: 34134 RVA: 0x001E2360 File Offset: 0x001E0560
			[return: global::System.Runtime.CompilerServices.Nullable(2)]
			internal NullableWalker.Variables GetVariablesForMethodScope(MethodSymbol method)
			{
				method = method.PartialImplementationPart ?? method;
				NullableWalker.Variables variables = this;
				while (method != variables.Symbol)
				{
					variables = variables.Container;
					if (variables == null)
					{
						return null;
					}
				}
				return variables;
			}

			// Token: 0x06008557 RID: 34135 RVA: 0x001E2392 File Offset: 0x001E0592
			internal static int ConstructSlot(int id, int index)
			{
				if (index >= 0)
				{
					return (id << 16) | index;
				}
				return index;
			}

			// Token: 0x06008558 RID: 34136 RVA: 0x001E23A0 File Offset: 0x001E05A0
			[global::System.Runtime.CompilerServices.NullableContext(0)]
			[return: TupleElementNames(new string[] { "Id", "Index" })]
			internal static ValueTuple<int, int> DeconstructSlot(int slot)
			{
				if (slot >= 0)
				{
					return new ValueTuple<int, int>((slot >> 16) & 32767, slot & 65535);
				}
				return new ValueTuple<int, int>(0, slot);
			}

			// Token: 0x06008559 RID: 34137 RVA: 0x001E23C4 File Offset: 0x001E05C4
			private string GetDebuggerDisplay()
			{
				object obj = this.Symbol ?? "<null>";
				return string.Format("Id={0}, Symbol={1}, Count={2}", this.Id, obj, this._variableSlot.Count);
			}

			// Token: 0x0600855A RID: 34138 RVA: 0x001E2408 File Offset: 0x001E0608
			[CompilerGenerated]
			private int <AddInternal>g__getSlotDepth|22_0(int slot)
			{
				int num = 0;
				while (slot > 0)
				{
					num++;
					int item = NullableWalker.Variables.DeconstructSlot(slot).Item2;
					slot = this._variableBySlot[item].ContainingSlot;
				}
				return num;
			}

			// Token: 0x04002223 RID: 8739
			private const int MaxSlotDepth = 5;

			// Token: 0x04002224 RID: 8740
			private const int IdOffset = 16;

			// Token: 0x04002225 RID: 8741
			private const int IdMask = 32767;

			// Token: 0x04002226 RID: 8742
			private const int IndexMask = 65535;

			// Token: 0x04002227 RID: 8743
			internal readonly int Id;

			// Token: 0x04002228 RID: 8744
			[global::System.Runtime.CompilerServices.Nullable(2)]
			internal readonly NullableWalker.Variables Container;

			// Token: 0x04002229 RID: 8745
			[global::System.Runtime.CompilerServices.Nullable(2)]
			internal readonly Symbol Symbol;

			// Token: 0x0400222A RID: 8746
			[global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 0, 1 })]
			private readonly PooledDictionary<LocalDataFlowPass<NullableWalker.LocalState, NullableWalker.LocalFunctionState>.VariableIdentifier, int> _variableSlot = PooledDictionary<LocalDataFlowPass<NullableWalker.LocalState, NullableWalker.LocalFunctionState>.VariableIdentifier, int>.GetInstance();

			// Token: 0x0400222B RID: 8747
			private readonly PooledDictionary<Symbol, TypeWithAnnotations> _variableTypes = SpecializedSymbolCollections.GetPooledSymbolDictionaryInstance<Symbol, TypeWithAnnotations>();

			// Token: 0x0400222C RID: 8748
			[global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 0, 1 })]
			private readonly ArrayBuilder<LocalDataFlowPass<NullableWalker.LocalState, NullableWalker.LocalFunctionState>.VariableIdentifier> _variableBySlot = ArrayBuilder<LocalDataFlowPass<NullableWalker.LocalState, NullableWalker.LocalFunctionState>.VariableIdentifier>.GetInstance(1, default(LocalDataFlowPass<NullableWalker.LocalState, NullableWalker.LocalFunctionState>.VariableIdentifier));
		}

		// Token: 0x02000702 RID: 1794
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private struct PossiblyConditionalState
		{
			// Token: 0x0600855B RID: 34139 RVA: 0x001E2441 File Offset: 0x001E0641
			public PossiblyConditionalState(NullableWalker.LocalState stateWhenTrue, NullableWalker.LocalState stateWhenFalse)
			{
				this.StateWhenTrue = stateWhenTrue.Clone();
				this.StateWhenFalse = stateWhenFalse.Clone();
				this.IsConditionalState = true;
				this.State = default(NullableWalker.LocalState);
			}

			// Token: 0x0600855C RID: 34140 RVA: 0x001E2470 File Offset: 0x001E0670
			public PossiblyConditionalState(NullableWalker.LocalState state)
			{
				this.StateWhenTrue = (this.StateWhenFalse = default(NullableWalker.LocalState));
				this.IsConditionalState = false;
				this.State = state.Clone();
			}

			// Token: 0x0600855D RID: 34141 RVA: 0x001E24A9 File Offset: 0x001E06A9
			public static NullableWalker.PossiblyConditionalState Create(NullableWalker nullableWalker)
			{
				if (!nullableWalker.IsConditionalState)
				{
					return new NullableWalker.PossiblyConditionalState(nullableWalker.State);
				}
				return new NullableWalker.PossiblyConditionalState(nullableWalker.StateWhenTrue, nullableWalker.StateWhenFalse);
			}

			// Token: 0x0600855E RID: 34142 RVA: 0x001E24D0 File Offset: 0x001E06D0
			public NullableWalker.PossiblyConditionalState Clone()
			{
				if (!this.IsConditionalState)
				{
					return new NullableWalker.PossiblyConditionalState(this.State);
				}
				return new NullableWalker.PossiblyConditionalState(this.StateWhenTrue, this.StateWhenFalse);
			}

			// Token: 0x0400222D RID: 8749
			public NullableWalker.LocalState State;

			// Token: 0x0400222E RID: 8750
			public NullableWalker.LocalState StateWhenTrue;

			// Token: 0x0400222F RID: 8751
			public NullableWalker.LocalState StateWhenFalse;

			// Token: 0x04002230 RID: 8752
			public bool IsConditionalState;
		}
	}
}
