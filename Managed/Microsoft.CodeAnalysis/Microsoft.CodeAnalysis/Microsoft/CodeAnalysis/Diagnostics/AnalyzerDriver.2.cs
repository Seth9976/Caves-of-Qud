using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.CodeAnalysis.Collections;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x02000304 RID: 772
	[NullableContext(1)]
	[Nullable(0)]
	internal class AnalyzerDriver<[Nullable(0)] TLanguageKindEnum> : AnalyzerDriver where TLanguageKindEnum : struct
	{
		// Token: 0x06002194 RID: 8596 RVA: 0x0006901B File Offset: 0x0006721B
		internal AnalyzerDriver([Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticAnalyzer> analyzers, [Nullable(new byte[] { 1, 1, 0 })] Func<SyntaxNode, TLanguageKindEnum> getKind, AnalyzerManager analyzerManager, SeverityFilter severityFilter, Func<SyntaxTrivia, bool> isComment)
			: base(analyzers, analyzerManager, severityFilter, isComment)
		{
			this._getKind = getKind;
		}

		// Token: 0x06002195 RID: 8597 RVA: 0x00069030 File Offset: 0x00067230
		[return: Nullable(new byte[] { 1, 0 })]
		private AnalyzerDriver<TLanguageKindEnum>.GroupedAnalyzerActions GetOrCreateCoreActions()
		{
			if (this._lazyCoreActions == null)
			{
				Interlocked.CompareExchange<AnalyzerDriver<TLanguageKindEnum>.GroupedAnalyzerActions>(ref this._lazyCoreActions, this.<GetOrCreateCoreActions>g__createCoreActions|3_0(), null);
			}
			return this._lazyCoreActions;
		}

		// Token: 0x06002196 RID: 8598 RVA: 0x00069054 File Offset: 0x00067254
		private static void ComputeShouldExecuteActions(in AnalyzerActions coreActions, in AnalyzerActions additionalActions, ISymbol symbol, out bool executeSyntaxNodeActions, out bool executeCodeBlockActions, out bool executeOperationActions, out bool executeOperationBlockActions)
		{
			executeSyntaxNodeActions = false;
			executeCodeBlockActions = false;
			executeOperationActions = false;
			executeOperationBlockActions = false;
			bool flag = AnalyzerExecutor.CanHaveExecutableCodeBlock(symbol);
			AnalyzerDriver<TLanguageKindEnum>.<ComputeShouldExecuteActions>g__computeShouldExecuteActions|4_0(coreActions, flag, ref executeSyntaxNodeActions, ref executeCodeBlockActions, ref executeOperationActions, ref executeOperationBlockActions);
			AnalyzerDriver<TLanguageKindEnum>.<ComputeShouldExecuteActions>g__computeShouldExecuteActions|4_0(additionalActions, flag, ref executeSyntaxNodeActions, ref executeCodeBlockActions, ref executeOperationActions, ref executeOperationBlockActions);
		}

		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x06002197 RID: 8599 RVA: 0x0006909D File Offset: 0x0006729D
		protected override AnalyzerDriver.IGroupedAnalyzerActions EmptyGroupedActions
		{
			get
			{
				return AnalyzerDriver<TLanguageKindEnum>.GroupedAnalyzerActions.Empty;
			}
		}

		// Token: 0x06002198 RID: 8600 RVA: 0x000690A4 File Offset: 0x000672A4
		protected override AnalyzerDriver.IGroupedAnalyzerActions CreateGroupedActions(DiagnosticAnalyzer analyzer, in AnalyzerActions analyzerActions)
		{
			return AnalyzerDriver<TLanguageKindEnum>.GroupedAnalyzerActions.Create(analyzer, in analyzerActions);
		}

		// Token: 0x06002199 RID: 8601 RVA: 0x000690B0 File Offset: 0x000672B0
		protected override bool TryExecuteDeclaringReferenceActions(SymbolDeclaredCompilationEvent symbolEvent, AnalysisScope analysisScope, [Nullable(2)] AnalysisState analysisState, bool isGeneratedCodeSymbol, AnalyzerDriver.IGroupedAnalyzerActions additionalPerSymbolActions, CancellationToken cancellationToken)
		{
			ISymbol symbol = symbolEvent.Symbol;
			readonly ref AnalyzerActions analyzerActions = ref base.AnalyzerActions;
			AnalyzerActions analyzerActions2 = additionalPerSymbolActions.AnalyzerActions;
			bool flag;
			bool flag2;
			bool flag3;
			bool flag4;
			AnalyzerDriver<TLanguageKindEnum>.ComputeShouldExecuteActions(in analyzerActions, in analyzerActions2, symbol, out flag, out flag2, out flag3, out flag4);
			bool flag5 = true;
			if (flag || flag3 || flag2 || flag4)
			{
				ImmutableArray<SyntaxReference> declaringSyntaxReferences = symbolEvent.DeclaringSyntaxReferences;
				AnalyzerDriver<TLanguageKindEnum>.GroupedAnalyzerActions orCreateCoreActions = this.GetOrCreateCoreActions();
				int i = 0;
				while (i < declaringSyntaxReferences.Length)
				{
					cancellationToken.ThrowIfCancellationRequested();
					SyntaxReference syntaxReference = declaringSyntaxReferences[i];
					SourceOrAdditionalFile? sourceOrAdditionalFile = analysisScope.FilterFileOpt;
					if (sourceOrAdditionalFile == null)
					{
						goto IL_009E;
					}
					sourceOrAdditionalFile = analysisScope.FilterFileOpt;
					if (((sourceOrAdditionalFile != null) ? sourceOrAdditionalFile.GetValueOrDefault().SourceTree : null) == syntaxReference.SyntaxTree)
					{
						goto IL_009E;
					}
					IL_0104:
					i++;
					continue;
					IL_009E:
					bool flag6 = isGeneratedCodeSymbol || base.IsGeneratedOrHiddenCodeLocation(syntaxReference.SyntaxTree, syntaxReference.Span);
					if (flag6 && base.DoNotAnalyzeGeneratedCode)
					{
						if (analysisState != null)
						{
							analysisState.MarkDeclarationComplete(symbol, i, analysisScope.Analyzers);
							goto IL_0104;
						}
						goto IL_0104;
					}
					else
					{
						if (!this.TryExecuteDeclaringReferenceActions(syntaxReference, i, symbolEvent, analysisScope, analysisState, orCreateCoreActions, (AnalyzerDriver<TLanguageKindEnum>.GroupedAnalyzerActions)additionalPerSymbolActions, flag, flag3, flag2, flag4, flag6, cancellationToken))
						{
							flag5 = false;
							goto IL_0104;
						}
						goto IL_0104;
					}
				}
			}
			else if (analysisState != null)
			{
				cancellationToken.ThrowIfCancellationRequested();
				analysisState.MarkDeclarationsComplete(symbol, analysisScope.Analyzers);
				ImmutableArray<SyntaxReference> declaringSyntaxReferences2 = symbolEvent.DeclaringSyntaxReferences;
				for (int j = 0; j < declaringSyntaxReferences2.Length; j++)
				{
					SyntaxReference syntaxReference2 = declaringSyntaxReferences2[j];
					this.ClearCachedAnalysisDataIfAnalyzed(syntaxReference2, symbol, j, analysisState);
				}
			}
			return flag5;
		}

		// Token: 0x0600219A RID: 8602 RVA: 0x0006922A File Offset: 0x0006742A
		private void ClearCachedAnalysisDataIfAnalyzed(SyntaxReference declaration, ISymbol symbol, int declarationIndex, AnalysisState analysisState)
		{
			if (!analysisState.IsDeclarationComplete(symbol, declarationIndex))
			{
				return;
			}
			base.CurrentCompilationData.ClearDeclarationAnalysisData(declaration);
		}

		// Token: 0x0600219B RID: 8603 RVA: 0x00069244 File Offset: 0x00067444
		private AnalyzerDriver.DeclarationAnalysisData ComputeDeclarationAnalysisData(ISymbol symbol, SyntaxReference declaration, SemanticModel semanticModel, AnalysisScope analysisScope, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			ArrayBuilder<DeclarationInfo> instance = ArrayBuilder<DeclarationInfo>.GetInstance();
			SyntaxNode syntax = declaration.GetSyntax(cancellationToken);
			SyntaxNode topmostNodeForDiagnosticAnalysis = semanticModel.GetTopmostNodeForDiagnosticAnalysis(symbol, syntax);
			AnalyzerDriver<TLanguageKindEnum>.ComputeDeclarationsInNode(semanticModel, symbol, syntax, topmostNodeForDiagnosticAnalysis, instance, cancellationToken);
			ImmutableArray<DeclarationInfo> immutableArray = instance.ToImmutableAndFree();
			bool flag = analysisScope.FilterSpanOpt != null && !analysisScope.ContainsSpan(topmostNodeForDiagnosticAnalysis.FullSpan);
			ImmutableArray<SyntaxNode> syntaxNodesToAnalyze = AnalyzerDriver<TLanguageKindEnum>.GetSyntaxNodesToAnalyze(topmostNodeForDiagnosticAnalysis, symbol, immutableArray, analysisScope, flag, semanticModel, base.AnalyzerExecutor);
			return new AnalyzerDriver.DeclarationAnalysisData(syntax, topmostNodeForDiagnosticAnalysis, immutableArray, syntaxNodesToAnalyze, flag);
		}

		// Token: 0x0600219C RID: 8604 RVA: 0x000692CC File Offset: 0x000674CC
		private static void ComputeDeclarationsInNode(SemanticModel semanticModel, ISymbol declaredSymbol, SyntaxNode declaringReferenceSyntax, SyntaxNode topmostNodeForAnalysis, ArrayBuilder<DeclarationInfo> builder, CancellationToken cancellationToken)
		{
			int? num = new int?(2);
			bool flag = topmostNodeForAnalysis != declaringReferenceSyntax || declaredSymbol.Kind == SymbolKind.Namespace;
			semanticModel.ComputeDeclarationsInNode(topmostNodeForAnalysis, declaredSymbol, flag, builder, cancellationToken, num);
		}

		// Token: 0x0600219D RID: 8605 RVA: 0x00069304 File Offset: 0x00067504
		private bool TryExecuteDeclaringReferenceActions(SyntaxReference decl, int declarationIndex, SymbolDeclaredCompilationEvent symbolEvent, AnalysisScope analysisScope, [Nullable(2)] AnalysisState analysisState, [Nullable(new byte[] { 1, 0 })] AnalyzerDriver<TLanguageKindEnum>.GroupedAnalyzerActions coreActions, [Nullable(new byte[] { 1, 0 })] AnalyzerDriver<TLanguageKindEnum>.GroupedAnalyzerActions additionalPerSymbolActions, bool shouldExecuteSyntaxNodeActions, bool shouldExecuteOperationActions, bool shouldExecuteCodeBlockActions, bool shouldExecuteOperationBlockActions, bool isInGeneratedCode, CancellationToken cancellationToken)
		{
			AnalyzerDriver<TLanguageKindEnum>.<>c__DisplayClass12_0 CS$<>8__locals1 = new AnalyzerDriver<TLanguageKindEnum>.<>c__DisplayClass12_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.decl = decl;
			CS$<>8__locals1.analysisScope = analysisScope;
			CS$<>8__locals1.cancellationToken = cancellationToken;
			CS$<>8__locals1.shouldExecuteSyntaxNodeActions = shouldExecuteSyntaxNodeActions;
			CS$<>8__locals1.coreActions = coreActions;
			CS$<>8__locals1.additionalPerSymbolActions = additionalPerSymbolActions;
			CS$<>8__locals1.declarationIndex = declarationIndex;
			CS$<>8__locals1.analysisState = analysisState;
			CS$<>8__locals1.isInGeneratedCode = isInGeneratedCode;
			CS$<>8__locals1.shouldExecuteCodeBlockActions = shouldExecuteCodeBlockActions;
			CS$<>8__locals1.shouldExecuteOperationActions = shouldExecuteOperationActions;
			CS$<>8__locals1.shouldExecuteOperationBlockActions = shouldExecuteOperationBlockActions;
			CS$<>8__locals1.symbol = symbolEvent.Symbol;
			CS$<>8__locals1.semanticModel = symbolEvent.SemanticModelWithCachedBoundNodes ?? base.SemanticModelProvider.GetSemanticModel(CS$<>8__locals1.decl.SyntaxTree, symbolEvent.Compilation, false);
			bool flag = CS$<>8__locals1.analysisScope.Analyzers.Length < base.Analyzers.Length && (CS$<>8__locals1.analysisScope.FilterSpanOpt == null || CS$<>8__locals1.analysisScope.FilterSpanOpt.Value.Length >= CS$<>8__locals1.decl.SyntaxTree.GetRoot(CS$<>8__locals1.cancellationToken).Span.Length);
			CS$<>8__locals1.declarationAnalysisData = base.CurrentCompilationData.GetOrComputeDeclarationAnalysisData(CS$<>8__locals1.decl, () => CS$<>8__locals1.<>4__this.ComputeDeclarationAnalysisData(CS$<>8__locals1.symbol, CS$<>8__locals1.decl, CS$<>8__locals1.semanticModel, CS$<>8__locals1.analysisScope, CS$<>8__locals1.cancellationToken), flag);
			if (!CS$<>8__locals1.analysisScope.ShouldAnalyze(CS$<>8__locals1.declarationAnalysisData.TopmostNodeForAnalysis))
			{
				return true;
			}
			CS$<>8__locals1.success = true;
			CS$<>8__locals1.<TryExecuteDeclaringReferenceActions>g__executeNodeActions|1();
			CS$<>8__locals1.<TryExecuteDeclaringReferenceActions>g__executeExecutableCodeActions|3();
			if (CS$<>8__locals1.success && CS$<>8__locals1.analysisState != null && !CS$<>8__locals1.declarationAnalysisData.IsPartialAnalysis)
			{
				CS$<>8__locals1.cancellationToken.ThrowIfCancellationRequested();
				foreach (DiagnosticAnalyzer diagnosticAnalyzer in CS$<>8__locals1.analysisScope.Analyzers)
				{
					CS$<>8__locals1.analysisState.MarkDeclarationComplete(CS$<>8__locals1.symbol, CS$<>8__locals1.declarationIndex, diagnosticAnalyzer);
				}
				if (flag)
				{
					this.ClearCachedAnalysisDataIfAnalyzed(CS$<>8__locals1.decl, CS$<>8__locals1.symbol, CS$<>8__locals1.declarationIndex, CS$<>8__locals1.analysisState);
				}
			}
			return CS$<>8__locals1.success;
		}

		// Token: 0x0600219E RID: 8606 RVA: 0x00069520 File Offset: 0x00067720
		[return: Nullable(new byte[] { 0, 1 })]
		private static ImmutableArray<SyntaxNode> GetSyntaxNodesToAnalyze(SyntaxNode declaredNode, ISymbol declaredSymbol, [Nullable(0)] ImmutableArray<DeclarationInfo> declarationsInNode, AnalysisScope analysisScope, bool isPartialDeclAnalysis, SemanticModel semanticModel, AnalyzerExecutor analyzerExecutor)
		{
			AnalyzerDriver<TLanguageKindEnum>.<>c__DisplayClass13_0 CS$<>8__locals1 = new AnalyzerDriver<TLanguageKindEnum>.<>c__DisplayClass13_0();
			CS$<>8__locals1.descendantDeclsToSkip = null;
			bool flag = true;
			foreach (DeclarationInfo declarationInfo in declarationsInNode)
			{
				analyzerExecutor.CancellationToken.ThrowIfCancellationRequested();
				if (declarationInfo.DeclaredNode != declaredNode)
				{
					if (AnalyzerDriver<TLanguageKindEnum>.IsEquivalentSymbol(declaredSymbol, declarationInfo.DeclaredSymbol))
					{
						if (!flag)
						{
							return ImmutableArray<SyntaxNode>.Empty;
						}
						break;
					}
					else
					{
						SyntaxNode syntaxNode = declarationInfo.DeclaredNode;
						ISymbol symbol = declarationInfo.DeclaredSymbol ?? semanticModel.GetDeclaredSymbol(declarationInfo.DeclaredNode, analyzerExecutor.CancellationToken);
						if (symbol != null)
						{
							syntaxNode = semanticModel.GetTopmostNodeForDiagnosticAnalysis(symbol, declarationInfo.DeclaredNode);
						}
						if (CS$<>8__locals1.descendantDeclsToSkip == null)
						{
							CS$<>8__locals1.descendantDeclsToSkip = new HashSet<SyntaxNode>();
						}
						CS$<>8__locals1.descendantDeclsToSkip.Add(syntaxNode);
					}
				}
				flag = false;
			}
			CS$<>8__locals1.additionalFilter = semanticModel.GetSyntaxNodesToAnalyzeFilter(declaredNode, declaredSymbol);
			ArrayBuilder<SyntaxNode> instance = ArrayBuilder<SyntaxNode>.GetInstance();
			foreach (SyntaxNode syntaxNode2 in declaredNode.DescendantNodesAndSelf(new Func<SyntaxNode, bool>(CS$<>8__locals1.<GetSyntaxNodesToAnalyze>g__shouldAddNode|0), true))
			{
				if (CS$<>8__locals1.<GetSyntaxNodesToAnalyze>g__shouldAddNode|0(syntaxNode2) && (!isPartialDeclAnalysis || analysisScope.ShouldAnalyze(syntaxNode2)))
				{
					instance.Add(syntaxNode2);
				}
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x0600219F RID: 8607 RVA: 0x0006967C File Offset: 0x0006787C
		private static bool IsEquivalentSymbol(ISymbol declaredSymbol, [Nullable(2)] ISymbol otherSymbol)
		{
			return declaredSymbol.Equals(otherSymbol) || (otherSymbol != null && declaredSymbol.Kind == SymbolKind.Namespace && otherSymbol.Kind == SymbolKind.Namespace && declaredSymbol.Name == otherSymbol.Name && declaredSymbol.ToDisplayString(null) == otherSymbol.ToDisplayString(null));
		}

		// Token: 0x060021A0 RID: 8608 RVA: 0x000696D4 File Offset: 0x000678D4
		[return: Nullable(new byte[] { 0, 1 })]
		private static ImmutableArray<IOperation> GetOperationBlocksToAnalyze([Nullable(new byte[] { 0, 1 })] ImmutableArray<SyntaxNode> executableBlocks, SemanticModel semanticModel, CancellationToken cancellationToken)
		{
			ArrayBuilder<IOperation> instance = ArrayBuilder<IOperation>.GetInstance();
			foreach (SyntaxNode syntaxNode in executableBlocks)
			{
				IOperation operation = semanticModel.GetOperation(syntaxNode, cancellationToken);
				if (operation != null)
				{
					instance.AddRange(new IOperation[] { operation });
				}
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x060021A1 RID: 8609 RVA: 0x00069724 File Offset: 0x00067924
		[return: Nullable(new byte[] { 0, 1 })]
		private static ImmutableArray<IOperation> GetOperationsToAnalyze([Nullable(new byte[] { 0, 1 })] ImmutableArray<IOperation> operationBlocks)
		{
			ArrayBuilder<IOperation> instance = ArrayBuilder<IOperation>.GetInstance();
			bool flag = true;
			foreach (IOperation operation in operationBlocks)
			{
				if (flag && operation.Parent != null)
				{
					OperationKind kind = operation.Parent.Kind;
					if (kind != OperationKind.ExpressionStatement)
					{
						if (kind - OperationKind.MethodBody <= 1)
						{
							instance.Add(operation.Parent);
						}
					}
					else
					{
						instance.Add(operation.Parent.Parent);
					}
					flag = false;
				}
				instance.AddRange(operation.DescendantsAndSelf());
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x060021A2 RID: 8610 RVA: 0x000697AD File Offset: 0x000679AD
		[CompilerGenerated]
		[return: Nullable(new byte[] { 1, 0 })]
		private AnalyzerDriver<TLanguageKindEnum>.GroupedAnalyzerActions <GetOrCreateCoreActions>g__createCoreActions|3_0()
		{
			if (base.AnalyzerActions.IsEmpty)
			{
				return AnalyzerDriver<TLanguageKindEnum>.GroupedAnalyzerActions.Empty;
			}
			return AnalyzerDriver<TLanguageKindEnum>.GroupedAnalyzerActions.Create(base.Analyzers.WhereAsArray<DiagnosticAnalyzer>(new Func<DiagnosticAnalyzer, bool>(base.UnsuppressedAnalyzers.Contains)), base.AnalyzerActions);
		}

		// Token: 0x060021A3 RID: 8611 RVA: 0x000697EC File Offset: 0x000679EC
		[CompilerGenerated]
		internal static void <ComputeShouldExecuteActions>g__computeShouldExecuteActions|4_0(AnalyzerActions analyzerActions, bool canHaveExecutableCodeBlock, ref bool executeSyntaxNodeActions, ref bool executeCodeBlockActions, ref bool executeOperationActions, ref bool executeOperationBlockActions)
		{
			if (analyzerActions.IsEmpty)
			{
				return;
			}
			executeSyntaxNodeActions |= analyzerActions.SyntaxNodeActionsCount > 0;
			executeOperationActions |= analyzerActions.OperationActionsCount > 0;
			if (canHaveExecutableCodeBlock)
			{
				executeCodeBlockActions |= analyzerActions.CodeBlockStartActionsCount > 0 || analyzerActions.CodeBlockActionsCount > 0;
				executeOperationBlockActions |= analyzerActions.OperationBlockStartActionsCount > 0 || analyzerActions.OperationBlockActionsCount > 0;
			}
		}

		// Token: 0x060021A4 RID: 8612 RVA: 0x00069860 File Offset: 0x00067A60
		[CompilerGenerated]
		internal static void <TryExecuteDeclaringReferenceActions>g__addExecutableCodeBlockAnalyzerActions|12_9([Nullable(new byte[] { 1, 0 })] AnalyzerDriver<TLanguageKindEnum>.GroupedAnalyzerActions groupedActions, AnalysisScope analysisScope, [Nullable(new byte[] { 1, 0, 0 })] ArrayBuilder<AnalyzerDriver<TLanguageKindEnum>.ExecutableCodeBlockAnalyzerActions> builder)
		{
			foreach (ValueTuple<DiagnosticAnalyzer, AnalyzerDriver<TLanguageKindEnum>.GroupedAnalyzerActionsForAnalyzer> valueTuple in groupedActions.GroupedActionsByAnalyzer)
			{
				DiagnosticAnalyzer item = valueTuple.Item1;
				AnalyzerDriver<TLanguageKindEnum>.GroupedAnalyzerActionsForAnalyzer item2 = valueTuple.Item2;
				AnalyzerDriver<TLanguageKindEnum>.ExecutableCodeBlockAnalyzerActions executableCodeBlockAnalyzerActions;
				if (analysisScope.Contains(item) && item2.TryGetExecutableCodeBlockActions(out executableCodeBlockAnalyzerActions))
				{
					builder.Add(executableCodeBlockAnalyzerActions);
				}
			}
		}

		// Token: 0x04000E8A RID: 3722
		[Nullable(new byte[] { 1, 1, 0 })]
		private readonly Func<SyntaxNode, TLanguageKindEnum> _getKind;

		// Token: 0x04000E8B RID: 3723
		[Nullable(new byte[] { 2, 0 })]
		private AnalyzerDriver<TLanguageKindEnum>.GroupedAnalyzerActions _lazyCoreActions;

		// Token: 0x020006BC RID: 1724
		[NullableContext(0)]
		[StructLayout(3)]
		private struct ExecutableCodeBlockAnalyzerActions
		{
			// Token: 0x04001A2B RID: 6699
			[Nullable(1)]
			public DiagnosticAnalyzer Analyzer;

			// Token: 0x04001A2C RID: 6700
			[Nullable(new byte[] { 0, 1, 0 })]
			public ImmutableArray<CodeBlockStartAnalyzerAction<TLanguageKindEnum>> CodeBlockStartActions;

			// Token: 0x04001A2D RID: 6701
			[Nullable(new byte[] { 0, 1 })]
			public ImmutableArray<CodeBlockAnalyzerAction> CodeBlockActions;

			// Token: 0x04001A2E RID: 6702
			[Nullable(new byte[] { 0, 1 })]
			public ImmutableArray<CodeBlockAnalyzerAction> CodeBlockEndActions;

			// Token: 0x04001A2F RID: 6703
			[Nullable(new byte[] { 0, 1 })]
			public ImmutableArray<OperationBlockStartAnalyzerAction> OperationBlockStartActions;

			// Token: 0x04001A30 RID: 6704
			[Nullable(new byte[] { 0, 1 })]
			public ImmutableArray<OperationBlockAnalyzerAction> OperationBlockActions;

			// Token: 0x04001A31 RID: 6705
			[Nullable(new byte[] { 0, 1 })]
			public ImmutableArray<OperationBlockAnalyzerAction> OperationBlockEndActions;
		}

		// Token: 0x020006BD RID: 1725
		[NullableContext(0)]
		private sealed class GroupedAnalyzerActions : AnalyzerDriver.IGroupedAnalyzerActions
		{
			// Token: 0x06003A13 RID: 14867 RVA: 0x000A3056 File Offset: 0x000A1256
			private GroupedAnalyzerActions([Nullable(new byte[] { 0, 0, 1, 1, 0 })] ImmutableArray<ValueTuple<DiagnosticAnalyzer, AnalyzerDriver<TLanguageKindEnum>.GroupedAnalyzerActionsForAnalyzer>> groupedActionsAndAnalyzers, in AnalyzerActions analyzerActions)
			{
				this.GroupedActionsByAnalyzer = groupedActionsAndAnalyzers;
				this.AnalyzerActions = analyzerActions;
			}

			// Token: 0x17000FF5 RID: 4085
			// (get) Token: 0x06003A14 RID: 14868 RVA: 0x000A3071 File Offset: 0x000A1271
			[TupleElementNames(new string[] { "analyzer", "groupedActions" })]
			[Nullable(new byte[] { 0, 0, 1, 1, 0 })]
			public ImmutableArray<ValueTuple<DiagnosticAnalyzer, AnalyzerDriver<TLanguageKindEnum>.GroupedAnalyzerActionsForAnalyzer>> GroupedActionsByAnalyzer
			{
				[return: TupleElementNames(new string[] { "analyzer", "groupedActions" })]
				[return: Nullable(new byte[] { 0, 0, 1, 1, 0 })]
				get;
			}

			// Token: 0x17000FF6 RID: 4086
			// (get) Token: 0x06003A15 RID: 14869 RVA: 0x000A3079 File Offset: 0x000A1279
			public AnalyzerActions AnalyzerActions { get; }

			// Token: 0x17000FF7 RID: 4087
			// (get) Token: 0x06003A16 RID: 14870 RVA: 0x000A3081 File Offset: 0x000A1281
			public bool IsEmpty
			{
				get
				{
					return this == AnalyzerDriver<TLanguageKindEnum>.GroupedAnalyzerActions.Empty;
				}
			}

			// Token: 0x06003A17 RID: 14871 RVA: 0x000A308C File Offset: 0x000A128C
			[NullableContext(1)]
			[return: Nullable(new byte[] { 1, 0 })]
			public static AnalyzerDriver<TLanguageKindEnum>.GroupedAnalyzerActions Create(DiagnosticAnalyzer analyzer, in AnalyzerActions analyzerActions)
			{
				if (analyzerActions.IsEmpty)
				{
					return AnalyzerDriver<TLanguageKindEnum>.GroupedAnalyzerActions.Empty;
				}
				AnalyzerDriver<TLanguageKindEnum>.GroupedAnalyzerActionsForAnalyzer groupedAnalyzerActionsForAnalyzer = new AnalyzerDriver<TLanguageKindEnum>.GroupedAnalyzerActionsForAnalyzer(analyzer, in analyzerActions, false);
				return new AnalyzerDriver<TLanguageKindEnum>.GroupedAnalyzerActions(ImmutableArray<ValueTuple<DiagnosticAnalyzer, AnalyzerDriver<TLanguageKindEnum>.GroupedAnalyzerActionsForAnalyzer>>.Empty.Add(new ValueTuple<DiagnosticAnalyzer, AnalyzerDriver<TLanguageKindEnum>.GroupedAnalyzerActionsForAnalyzer>(analyzer, groupedAnalyzerActionsForAnalyzer)), in analyzerActions);
			}

			// Token: 0x06003A18 RID: 14872 RVA: 0x000A30CA File Offset: 0x000A12CA
			[return: Nullable(new byte[] { 1, 0 })]
			public static AnalyzerDriver<TLanguageKindEnum>.GroupedAnalyzerActions Create([Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticAnalyzer> analyzers, in AnalyzerActions analyzerActions)
			{
				return new AnalyzerDriver<TLanguageKindEnum>.GroupedAnalyzerActions(analyzers.SelectAsArray((DiagnosticAnalyzer analyzer, AnalyzerActions analyzerActions) => new ValueTuple<DiagnosticAnalyzer, AnalyzerDriver<TLanguageKindEnum>.GroupedAnalyzerActionsForAnalyzer>(analyzer, new AnalyzerDriver<TLanguageKindEnum>.GroupedAnalyzerActionsForAnalyzer(analyzer, in analyzerActions, true)), analyzerActions), in analyzerActions);
			}

			// Token: 0x06003A19 RID: 14873 RVA: 0x000A3100 File Offset: 0x000A1300
			[NullableContext(1)]
			AnalyzerDriver.IGroupedAnalyzerActions AnalyzerDriver.IGroupedAnalyzerActions.Append(AnalyzerDriver.IGroupedAnalyzerActions igroupedAnalyzerActions)
			{
				AnalyzerDriver<TLanguageKindEnum>.GroupedAnalyzerActions groupedAnalyzerActions = (AnalyzerDriver<TLanguageKindEnum>.GroupedAnalyzerActions)igroupedAnalyzerActions;
				ImmutableArray<ValueTuple<DiagnosticAnalyzer, AnalyzerDriver<TLanguageKindEnum>.GroupedAnalyzerActionsForAnalyzer>> immutableArray = this.GroupedActionsByAnalyzer.AddRange(groupedAnalyzerActions.GroupedActionsByAnalyzer);
				AnalyzerActions analyzerActions = this.AnalyzerActions;
				AnalyzerActions analyzerActions2 = groupedAnalyzerActions.AnalyzerActions;
				AnalyzerActions analyzerActions3 = analyzerActions.Append(in analyzerActions2, true);
				return new AnalyzerDriver<TLanguageKindEnum>.GroupedAnalyzerActions(immutableArray, in analyzerActions3);
			}

			// Token: 0x04001A32 RID: 6706
			[Nullable(new byte[] { 1, 0 })]
			public static readonly AnalyzerDriver<TLanguageKindEnum>.GroupedAnalyzerActions Empty = new AnalyzerDriver<TLanguageKindEnum>.GroupedAnalyzerActions(ImmutableArray<ValueTuple<DiagnosticAnalyzer, AnalyzerDriver<TLanguageKindEnum>.GroupedAnalyzerActionsForAnalyzer>>.Empty, in AnalyzerActions.Empty);
		}

		// Token: 0x020006BE RID: 1726
		[Nullable(0)]
		private sealed class GroupedAnalyzerActionsForAnalyzer
		{
			// Token: 0x06003A1B RID: 14875 RVA: 0x000A315F File Offset: 0x000A135F
			public GroupedAnalyzerActionsForAnalyzer(DiagnosticAnalyzer analyzer, in AnalyzerActions analyzerActions, bool analyzerActionsNeedFiltering)
			{
				this._analyzer = analyzer;
				this.AnalyzerActions = analyzerActions;
				this._analyzerActionsNeedFiltering = analyzerActionsNeedFiltering;
			}

			// Token: 0x17000FF8 RID: 4088
			// (get) Token: 0x06003A1C RID: 14876 RVA: 0x000A3181 File Offset: 0x000A1381
			public AnalyzerActions AnalyzerActions { get; }

			// Token: 0x06003A1D RID: 14877 RVA: 0x000A318C File Offset: 0x000A138C
			[Conditional("DEBUG")]
			private static void VerifyActions<[Nullable(0)] TAnalyzerAction>([Nullable(new byte[] { 0, 1 })] in ImmutableArray<TAnalyzerAction> actions, DiagnosticAnalyzer analyzer) where TAnalyzerAction : AnalyzerAction
			{
				ImmutableArray<TAnalyzerAction> immutableArray = actions;
				foreach (TAnalyzerAction tanalyzerAction in immutableArray)
				{
				}
			}

			// Token: 0x06003A1E RID: 14878 RVA: 0x000A31BB File Offset: 0x000A13BB
			[NullableContext(0)]
			[return: Nullable(new byte[] { 0, 1 })]
			private ImmutableArray<TAnalyzerAction> GetFilteredActions<TAnalyzerAction>([Nullable(new byte[] { 0, 1 })] in ImmutableArray<TAnalyzerAction> actions) where TAnalyzerAction : AnalyzerAction
			{
				return AnalyzerDriver<TLanguageKindEnum>.GroupedAnalyzerActionsForAnalyzer.GetFilteredActions<TAnalyzerAction>(in actions, this._analyzer, this._analyzerActionsNeedFiltering);
			}

			// Token: 0x06003A1F RID: 14879 RVA: 0x000A31CF File Offset: 0x000A13CF
			[return: Nullable(new byte[] { 0, 1 })]
			private static ImmutableArray<TAnalyzerAction> GetFilteredActions<[Nullable(0)] TAnalyzerAction>([Nullable(new byte[] { 0, 1 })] in ImmutableArray<TAnalyzerAction> actions, DiagnosticAnalyzer analyzer, bool analyzerActionsNeedFiltering) where TAnalyzerAction : AnalyzerAction
			{
				if (!analyzerActionsNeedFiltering)
				{
					return actions;
				}
				return actions.WhereAsArray((TAnalyzerAction action, DiagnosticAnalyzer analyzer) => action.Analyzer == analyzer, analyzer);
			}

			// Token: 0x17000FF9 RID: 4089
			// (get) Token: 0x06003A20 RID: 14880 RVA: 0x000A3208 File Offset: 0x000A1408
			[Nullable(new byte[] { 0, 0, 0, 1, 0 })]
			public ImmutableSegmentedDictionary<TLanguageKindEnum, ImmutableArray<SyntaxNodeAnalyzerAction<TLanguageKindEnum>>> NodeActionsByAnalyzerAndKind
			{
				[return: Nullable(new byte[] { 0, 0, 0, 1, 0 })]
				get
				{
					if (new ImmutableSegmentedDictionary<TLanguageKindEnum, ImmutableArray<SyntaxNodeAnalyzerAction<TLanguageKindEnum>>>?(this._lazyNodeActionsByKind) == default(ImmutableSegmentedDictionary<TLanguageKindEnum, ImmutableArray<SyntaxNodeAnalyzerAction<TLanguageKindEnum>>>?))
					{
						ImmutableArray<SyntaxNodeAnalyzerAction<TLanguageKindEnum>> immutableArray = (this._analyzerActionsNeedFiltering ? this.AnalyzerActions.GetSyntaxNodeActions<TLanguageKindEnum>(this._analyzer) : this.AnalyzerActions.GetSyntaxNodeActions<TLanguageKindEnum>());
						ImmutableSegmentedDictionary<TLanguageKindEnum, ImmutableArray<SyntaxNodeAnalyzerAction<TLanguageKindEnum>>> immutableSegmentedDictionary = ((!immutableArray.IsEmpty) ? AnalyzerExecutor.GetNodeActionsByKind<TLanguageKindEnum>(immutableArray) : ImmutableSegmentedDictionary<TLanguageKindEnum, ImmutableArray<SyntaxNodeAnalyzerAction<TLanguageKindEnum>>>.Empty);
						RoslynImmutableInterlocked.InterlockedInitialize<TLanguageKindEnum, ImmutableArray<SyntaxNodeAnalyzerAction<TLanguageKindEnum>>>(ref this._lazyNodeActionsByKind, immutableSegmentedDictionary);
					}
					return this._lazyNodeActionsByKind;
				}
			}

			// Token: 0x17000FFA RID: 4090
			// (get) Token: 0x06003A21 RID: 14881 RVA: 0x000A328C File Offset: 0x000A148C
			[Nullable(new byte[] { 0, 0, 1 })]
			public ImmutableSegmentedDictionary<OperationKind, ImmutableArray<OperationAnalyzerAction>> OperationActionsByAnalyzerAndKind
			{
				[return: Nullable(new byte[] { 0, 0, 1 })]
				get
				{
					if (new ImmutableSegmentedDictionary<OperationKind, ImmutableArray<OperationAnalyzerAction>>?(this._lazyOperationActionsByKind) == default(ImmutableSegmentedDictionary<OperationKind, ImmutableArray<OperationAnalyzerAction>>?))
					{
						ImmutableArray<OperationAnalyzerAction> operationActions = this.AnalyzerActions.OperationActions;
						ImmutableArray<OperationAnalyzerAction> filteredActions = this.GetFilteredActions<OperationAnalyzerAction>(in operationActions);
						ImmutableSegmentedDictionary<OperationKind, ImmutableArray<OperationAnalyzerAction>> immutableSegmentedDictionary = (filteredActions.Any<OperationAnalyzerAction>() ? AnalyzerExecutor.GetOperationActionsByKind(filteredActions) : ImmutableSegmentedDictionary<OperationKind, ImmutableArray<OperationAnalyzerAction>>.Empty);
						RoslynImmutableInterlocked.InterlockedInitialize<OperationKind, ImmutableArray<OperationAnalyzerAction>>(ref this._lazyOperationActionsByKind, immutableSegmentedDictionary);
					}
					return this._lazyOperationActionsByKind;
				}
			}

			// Token: 0x17000FFB RID: 4091
			// (get) Token: 0x06003A22 RID: 14882 RVA: 0x000A32FC File Offset: 0x000A14FC
			[Nullable(new byte[] { 0, 1, 0 })]
			private ImmutableArray<CodeBlockStartAnalyzerAction<TLanguageKindEnum>> CodeBlockStartActions
			{
				[return: Nullable(new byte[] { 0, 1, 0 })]
				get
				{
					if (this._lazyCodeBlockStartActions.IsDefault)
					{
						ImmutableArray<CodeBlockStartAnalyzerAction<TLanguageKindEnum>> codeBlockStartActions = this.AnalyzerActions.GetCodeBlockStartActions<TLanguageKindEnum>();
						ImmutableArray<CodeBlockStartAnalyzerAction<TLanguageKindEnum>> filteredActions = this.GetFilteredActions<CodeBlockStartAnalyzerAction<TLanguageKindEnum>>(in codeBlockStartActions);
						ImmutableInterlocked.InterlockedInitialize<CodeBlockStartAnalyzerAction<TLanguageKindEnum>>(ref this._lazyCodeBlockStartActions, filteredActions);
					}
					return this._lazyCodeBlockStartActions;
				}
			}

			// Token: 0x17000FFC RID: 4092
			// (get) Token: 0x06003A23 RID: 14883 RVA: 0x000A3344 File Offset: 0x000A1544
			[Nullable(new byte[] { 0, 1 })]
			private ImmutableArray<CodeBlockAnalyzerAction> CodeBlockEndActions
			{
				[return: Nullable(new byte[] { 0, 1 })]
				get
				{
					return AnalyzerDriver<TLanguageKindEnum>.GroupedAnalyzerActionsForAnalyzer.GetExecutableCodeActions<CodeBlockAnalyzerAction>(ref this._lazyCodeBlockEndActions, this.AnalyzerActions.CodeBlockEndActions, this._analyzer, this._analyzerActionsNeedFiltering);
				}
			}

			// Token: 0x17000FFD RID: 4093
			// (get) Token: 0x06003A24 RID: 14884 RVA: 0x000A3378 File Offset: 0x000A1578
			[Nullable(new byte[] { 0, 1 })]
			private ImmutableArray<CodeBlockAnalyzerAction> CodeBlockActions
			{
				[return: Nullable(new byte[] { 0, 1 })]
				get
				{
					return AnalyzerDriver<TLanguageKindEnum>.GroupedAnalyzerActionsForAnalyzer.GetExecutableCodeActions<CodeBlockAnalyzerAction>(ref this._lazyCodeBlockActions, this.AnalyzerActions.CodeBlockActions, this._analyzer, this._analyzerActionsNeedFiltering);
				}
			}

			// Token: 0x17000FFE RID: 4094
			// (get) Token: 0x06003A25 RID: 14885 RVA: 0x000A33AC File Offset: 0x000A15AC
			[Nullable(new byte[] { 0, 1 })]
			private ImmutableArray<OperationBlockStartAnalyzerAction> OperationBlockStartActions
			{
				[return: Nullable(new byte[] { 0, 1 })]
				get
				{
					return AnalyzerDriver<TLanguageKindEnum>.GroupedAnalyzerActionsForAnalyzer.GetExecutableCodeActions<OperationBlockStartAnalyzerAction>(ref this._lazyOperationBlockStartActions, this.AnalyzerActions.OperationBlockStartActions, this._analyzer, this._analyzerActionsNeedFiltering);
				}
			}

			// Token: 0x17000FFF RID: 4095
			// (get) Token: 0x06003A26 RID: 14886 RVA: 0x000A33E0 File Offset: 0x000A15E0
			[Nullable(new byte[] { 0, 1 })]
			private ImmutableArray<OperationBlockAnalyzerAction> OperationBlockEndActions
			{
				[return: Nullable(new byte[] { 0, 1 })]
				get
				{
					return AnalyzerDriver<TLanguageKindEnum>.GroupedAnalyzerActionsForAnalyzer.GetExecutableCodeActions<OperationBlockAnalyzerAction>(ref this._lazyOperationBlockEndActions, this.AnalyzerActions.OperationBlockEndActions, this._analyzer, this._analyzerActionsNeedFiltering);
				}
			}

			// Token: 0x17001000 RID: 4096
			// (get) Token: 0x06003A27 RID: 14887 RVA: 0x000A3414 File Offset: 0x000A1614
			[Nullable(new byte[] { 0, 1 })]
			private ImmutableArray<OperationBlockAnalyzerAction> OperationBlockActions
			{
				[return: Nullable(new byte[] { 0, 1 })]
				get
				{
					return AnalyzerDriver<TLanguageKindEnum>.GroupedAnalyzerActionsForAnalyzer.GetExecutableCodeActions<OperationBlockAnalyzerAction>(ref this._lazyOperationBlockActions, this.AnalyzerActions.OperationBlockActions, this._analyzer, this._analyzerActionsNeedFiltering);
				}
			}

			// Token: 0x06003A28 RID: 14888 RVA: 0x000A3446 File Offset: 0x000A1646
			[return: Nullable(new byte[] { 0, 1 })]
			private static ImmutableArray<ActionType> GetExecutableCodeActions<[Nullable(0)] ActionType>([Nullable(new byte[] { 0, 1 })] ref ImmutableArray<ActionType> lazyCodeBlockActions, [Nullable(new byte[] { 0, 1 })] ImmutableArray<ActionType> codeBlockActions, DiagnosticAnalyzer analyzer, bool analyzerActionsNeedFiltering) where ActionType : AnalyzerAction
			{
				if (lazyCodeBlockActions.IsDefault)
				{
					codeBlockActions = AnalyzerDriver<TLanguageKindEnum>.GroupedAnalyzerActionsForAnalyzer.GetFilteredActions<ActionType>(in codeBlockActions, analyzer, analyzerActionsNeedFiltering);
					ImmutableInterlocked.InterlockedInitialize<ActionType>(ref lazyCodeBlockActions, codeBlockActions);
				}
				return lazyCodeBlockActions;
			}

			// Token: 0x06003A29 RID: 14889 RVA: 0x000A346C File Offset: 0x000A166C
			[NullableContext(0)]
			public bool TryGetExecutableCodeBlockActions(out AnalyzerDriver<TLanguageKindEnum>.ExecutableCodeBlockAnalyzerActions actions)
			{
				if (!this.OperationBlockStartActions.IsEmpty || !this.OperationBlockActions.IsEmpty || !this.OperationBlockEndActions.IsEmpty || !this.CodeBlockStartActions.IsEmpty || !this.CodeBlockActions.IsEmpty || !this.CodeBlockEndActions.IsEmpty)
				{
					actions = new AnalyzerDriver<TLanguageKindEnum>.ExecutableCodeBlockAnalyzerActions
					{
						Analyzer = this._analyzer,
						CodeBlockStartActions = this.CodeBlockStartActions,
						CodeBlockActions = this.CodeBlockActions,
						CodeBlockEndActions = this.CodeBlockEndActions,
						OperationBlockStartActions = this.OperationBlockStartActions,
						OperationBlockActions = this.OperationBlockActions,
						OperationBlockEndActions = this.OperationBlockEndActions
					};
					return true;
				}
				actions = default(AnalyzerDriver<TLanguageKindEnum>.ExecutableCodeBlockAnalyzerActions);
				return false;
			}

			// Token: 0x04001A35 RID: 6709
			private readonly DiagnosticAnalyzer _analyzer;

			// Token: 0x04001A36 RID: 6710
			private readonly bool _analyzerActionsNeedFiltering;

			// Token: 0x04001A37 RID: 6711
			[Nullable(new byte[] { 0, 0, 0, 1, 0 })]
			private ImmutableSegmentedDictionary<TLanguageKindEnum, ImmutableArray<SyntaxNodeAnalyzerAction<TLanguageKindEnum>>> _lazyNodeActionsByKind;

			// Token: 0x04001A38 RID: 6712
			[Nullable(new byte[] { 0, 0, 1 })]
			private ImmutableSegmentedDictionary<OperationKind, ImmutableArray<OperationAnalyzerAction>> _lazyOperationActionsByKind;

			// Token: 0x04001A39 RID: 6713
			[Nullable(new byte[] { 0, 1, 0 })]
			private ImmutableArray<CodeBlockStartAnalyzerAction<TLanguageKindEnum>> _lazyCodeBlockStartActions;

			// Token: 0x04001A3A RID: 6714
			[Nullable(new byte[] { 0, 1 })]
			private ImmutableArray<CodeBlockAnalyzerAction> _lazyCodeBlockEndActions;

			// Token: 0x04001A3B RID: 6715
			[Nullable(new byte[] { 0, 1 })]
			private ImmutableArray<CodeBlockAnalyzerAction> _lazyCodeBlockActions;

			// Token: 0x04001A3C RID: 6716
			[Nullable(new byte[] { 0, 1 })]
			private ImmutableArray<OperationBlockStartAnalyzerAction> _lazyOperationBlockStartActions;

			// Token: 0x04001A3D RID: 6717
			[Nullable(new byte[] { 0, 1 })]
			private ImmutableArray<OperationBlockAnalyzerAction> _lazyOperationBlockActions;

			// Token: 0x04001A3E RID: 6718
			[Nullable(new byte[] { 0, 1 })]
			private ImmutableArray<OperationBlockAnalyzerAction> _lazyOperationBlockEndActions;
		}
	}
}
