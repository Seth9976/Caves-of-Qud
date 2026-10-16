using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.Collections;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x02000305 RID: 773
	[NullableContext(1)]
	[Nullable(0)]
	internal class AnalyzerExecutor
	{
		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x060021A5 RID: 8613 RVA: 0x000698B8 File Offset: 0x00067AB8
		private Func<IOperation, ControlFlowGraph> GetControlFlowGraph
		{
			get
			{
				Func<IOperation, ControlFlowGraph> func;
				if ((func = this._lazyGetControlFlowGraph) == null)
				{
					func = (this._lazyGetControlFlowGraph = new Func<IOperation, ControlFlowGraph>(this.GetControlFlowGraphImpl));
				}
				return func;
			}
		}

		// Token: 0x060021A6 RID: 8614 RVA: 0x000698E4 File Offset: 0x00067AE4
		private bool IsAnalyzerSuppressedForTree(DiagnosticAnalyzer analyzer, SyntaxTree tree)
		{
			return this._isAnalyzerSuppressedForTree.Invoke(analyzer, tree, this.Compilation.Options.SyntaxTreeOptionsProvider);
		}

		// Token: 0x060021A7 RID: 8615 RVA: 0x00069904 File Offset: 0x00067B04
		public static AnalyzerExecutor Create(Compilation compilation, AnalyzerOptions analyzerOptions, [Nullable(new byte[] { 2, 1 })] Action<Diagnostic> addNonCategorizedDiagnostic, Action<Exception, DiagnosticAnalyzer, Diagnostic> onAnalyzerException, [Nullable(new byte[] { 2, 1 })] Func<Exception, bool> analyzerExceptionFilter, Func<DiagnosticAnalyzer, bool> isCompilerAnalyzer, AnalyzerManager analyzerManager, Func<DiagnosticAnalyzer, bool> shouldSkipAnalysisOnGeneratedCode, Func<Diagnostic, DiagnosticAnalyzer, Compilation, CancellationToken, bool> shouldSuppressGeneratedCodeDiagnostic, Func<SyntaxTree, TextSpan, bool> isGeneratedCodeLocation, [Nullable(new byte[] { 1, 1, 1, 2 })] Func<DiagnosticAnalyzer, SyntaxTree, SyntaxTreeOptionsProvider, bool> isAnalyzerSuppressedForTree, [Nullable(new byte[] { 1, 1, 2 })] Func<DiagnosticAnalyzer, object> getAnalyzerGate, Func<SyntaxTree, SemanticModel> getSemanticModel, bool logExecutionTime = false, [Nullable(new byte[] { 2, 1, 1 })] Action<Diagnostic, DiagnosticAnalyzer, bool> addCategorizedLocalDiagnostic = null, [Nullable(new byte[] { 2, 1, 1 })] Action<Diagnostic, DiagnosticAnalyzer> addCategorizedNonLocalDiagnostic = null, [Nullable(2)] Action<Suppression> addSuppression = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			ConcurrentDictionary<DiagnosticAnalyzer, StrongBox<long>> concurrentDictionary = (logExecutionTime ? new ConcurrentDictionary<DiagnosticAnalyzer, StrongBox<long>>() : null);
			return new AnalyzerExecutor(compilation, analyzerOptions, addNonCategorizedDiagnostic, onAnalyzerException, analyzerExceptionFilter, isCompilerAnalyzer, analyzerManager, shouldSkipAnalysisOnGeneratedCode, shouldSuppressGeneratedCodeDiagnostic, isGeneratedCodeLocation, isAnalyzerSuppressedForTree, getAnalyzerGate, getSemanticModel, concurrentDictionary, addCategorizedLocalDiagnostic, addCategorizedNonLocalDiagnostic, addSuppression, cancellationToken);
		}

		// Token: 0x060021A8 RID: 8616 RVA: 0x00069944 File Offset: 0x00067B44
		public static AnalyzerExecutor CreateForSupportedDiagnostics([Nullable(new byte[] { 2, 1, 1, 1 })] Action<Exception, DiagnosticAnalyzer, Diagnostic> onAnalyzerException, AnalyzerManager analyzerManager, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (onAnalyzerException == null)
			{
				onAnalyzerException = delegate(Exception ex, DiagnosticAnalyzer analyzer, Diagnostic diagnostic)
				{
				};
			}
			return new AnalyzerExecutor(null, null, null, onAnalyzerException, null, null, analyzerManager, (DiagnosticAnalyzer _) => false, (Diagnostic diagnostic, DiagnosticAnalyzer analyzer, Compilation compilation, CancellationToken ct) => false, (SyntaxTree _1, TextSpan _2) => false, null, null, null, null, null, null, null, cancellationToken);
		}

		// Token: 0x060021A9 RID: 8617 RVA: 0x000699E8 File Offset: 0x00067BE8
		private AnalyzerExecutor([Nullable(2)] Compilation compilation, [Nullable(2)] AnalyzerOptions analyzerOptions, [Nullable(new byte[] { 2, 1 })] Action<Diagnostic> addNonCategorizedDiagnosticOpt, Action<Exception, DiagnosticAnalyzer, Diagnostic> onAnalyzerException, [Nullable(new byte[] { 2, 1 })] Func<Exception, bool> analyzerExceptionFilter, [Nullable(new byte[] { 2, 1 })] Func<DiagnosticAnalyzer, bool> isCompilerAnalyzer, AnalyzerManager analyzerManager, Func<DiagnosticAnalyzer, bool> shouldSkipAnalysisOnGeneratedCode, Func<Diagnostic, DiagnosticAnalyzer, Compilation, CancellationToken, bool> shouldSuppressGeneratedCodeDiagnostic, Func<SyntaxTree, TextSpan, bool> isGeneratedCodeLocation, [Nullable(new byte[] { 2, 1, 1, 2 })] Func<DiagnosticAnalyzer, SyntaxTree, SyntaxTreeOptionsProvider, bool> isAnalyzerSuppressedForTree, [Nullable(new byte[] { 2, 1, 2 })] Func<DiagnosticAnalyzer, object> getAnalyzerGate, [Nullable(new byte[] { 2, 1, 1 })] Func<SyntaxTree, SemanticModel> getSemanticModel, [Nullable(new byte[] { 2, 1, 1 })] ConcurrentDictionary<DiagnosticAnalyzer, StrongBox<long>> analyzerExecutionTimeMap, [Nullable(new byte[] { 2, 1, 1 })] Action<Diagnostic, DiagnosticAnalyzer, bool> addCategorizedLocalDiagnostic, [Nullable(new byte[] { 2, 1, 1 })] Action<Diagnostic, DiagnosticAnalyzer> addCategorizedNonLocalDiagnostic, [Nullable(2)] Action<Suppression> addSuppression, CancellationToken cancellationToken)
		{
			this._compilation = compilation;
			this._analyzerOptions = analyzerOptions;
			this._addNonCategorizedDiagnostic = addNonCategorizedDiagnosticOpt;
			this._onAnalyzerException = onAnalyzerException;
			this._analyzerExceptionFilter = analyzerExceptionFilter;
			this._isCompilerAnalyzer = isCompilerAnalyzer;
			this._analyzerManager = analyzerManager;
			this._shouldSkipAnalysisOnGeneratedCode = shouldSkipAnalysisOnGeneratedCode;
			this._shouldSuppressGeneratedCodeDiagnostic = shouldSuppressGeneratedCodeDiagnostic;
			this._isGeneratedCodeLocation = isGeneratedCodeLocation;
			this._isAnalyzerSuppressedForTree = isAnalyzerSuppressedForTree;
			this._getAnalyzerGate = getAnalyzerGate;
			this._getSemanticModel = getSemanticModel;
			this._analyzerExecutionTimeMap = analyzerExecutionTimeMap;
			this._addCategorizedLocalDiagnostic = addCategorizedLocalDiagnostic;
			this._addCategorizedNonLocalDiagnostic = addCategorizedNonLocalDiagnostic;
			this._addSuppression = addSuppression;
			this._cancellationToken = cancellationToken;
			this._compilationAnalysisValueProviderFactory = new CompilationAnalysisValueProviderFactory();
		}

		// Token: 0x060021AA RID: 8618 RVA: 0x00069A94 File Offset: 0x00067C94
		public AnalyzerExecutor WithCancellationToken(CancellationToken cancellationToken)
		{
			if (cancellationToken == this._cancellationToken)
			{
				return this;
			}
			return new AnalyzerExecutor(this._compilation, this._analyzerOptions, this._addNonCategorizedDiagnostic, this._onAnalyzerException, this._analyzerExceptionFilter, this._isCompilerAnalyzer, this._analyzerManager, this._shouldSkipAnalysisOnGeneratedCode, this._shouldSuppressGeneratedCodeDiagnostic, this._isGeneratedCodeLocation, this._isAnalyzerSuppressedForTree, this._getAnalyzerGate, this._getSemanticModel, this._analyzerExecutionTimeMap, this._addCategorizedLocalDiagnostic, this._addCategorizedNonLocalDiagnostic, this._addSuppression, cancellationToken);
		}

		// Token: 0x060021AB RID: 8619 RVA: 0x00069B20 File Offset: 0x00067D20
		[NullableContext(2)]
		internal bool TryGetCompilationAndAnalyzerOptions([NotNullWhen(true)] out Compilation compilation, [NotNullWhen(true)] out AnalyzerOptions analyzerOptions)
		{
			Compilation compilation2 = this._compilation;
			AnalyzerOptions analyzerOptions2 = this._analyzerOptions;
			compilation = compilation2;
			analyzerOptions = analyzerOptions2;
			return compilation != null && analyzerOptions != null;
		}

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x060021AC RID: 8620 RVA: 0x00069B4C File Offset: 0x00067D4C
		internal Compilation Compilation
		{
			get
			{
				return this._compilation;
			}
		}

		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x060021AD RID: 8621 RVA: 0x00069B54 File Offset: 0x00067D54
		internal AnalyzerOptions AnalyzerOptions
		{
			get
			{
				return this._analyzerOptions;
			}
		}

		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x060021AE RID: 8622 RVA: 0x00069B5C File Offset: 0x00067D5C
		internal CancellationToken CancellationToken
		{
			get
			{
				return this._cancellationToken;
			}
		}

		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x060021AF RID: 8623 RVA: 0x00069B64 File Offset: 0x00067D64
		internal Action<Exception, DiagnosticAnalyzer, Diagnostic> OnAnalyzerException
		{
			get
			{
				return this._onAnalyzerException;
			}
		}

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x060021B0 RID: 8624 RVA: 0x00069B6C File Offset: 0x00067D6C
		internal ImmutableDictionary<DiagnosticAnalyzer, TimeSpan> AnalyzerExecutionTimes
		{
			get
			{
				return this._analyzerExecutionTimeMap.ToImmutableDictionary<KeyValuePair<DiagnosticAnalyzer, StrongBox<long>>, DiagnosticAnalyzer, TimeSpan>((KeyValuePair<DiagnosticAnalyzer, StrongBox<long>> pair) => pair.Key, (KeyValuePair<DiagnosticAnalyzer, StrongBox<long>> pair) => TimeSpan.FromTicks(pair.Value.Value));
			}
		}

		// Token: 0x060021B1 RID: 8625 RVA: 0x00069BC4 File Offset: 0x00067DC4
		public void ExecuteInitializeMethod(DiagnosticAnalyzer analyzer, HostSessionStartAnalysisScope sessionScope)
		{
			AnalyzerAnalysisContext analyzerAnalysisContext = new AnalyzerAnalysisContext(analyzer, sessionScope);
			this.ExecuteAndCatchIfThrows<ValueTuple<DiagnosticAnalyzer, AnalyzerAnalysisContext>>(analyzer, delegate([TupleElementNames(new string[] { "analyzer", "context" })] ValueTuple<DiagnosticAnalyzer, AnalyzerAnalysisContext> data)
			{
				data.Item1.Initialize(data.Item2);
			}, new ValueTuple<DiagnosticAnalyzer, AnalyzerAnalysisContext>(analyzer, analyzerAnalysisContext), default(AnalysisContextInfo?));
		}

		// Token: 0x060021B2 RID: 8626 RVA: 0x00069C10 File Offset: 0x00067E10
		public void ExecuteCompilationStartActions([Nullable(new byte[] { 0, 1 })] ImmutableArray<CompilationStartAnalyzerAction> actions, HostCompilationStartAnalysisScope compilationScope)
		{
			foreach (CompilationStartAnalyzerAction compilationStartAnalyzerAction in actions)
			{
				this._cancellationToken.ThrowIfCancellationRequested();
				AnalyzerCompilationStartAnalysisContext analyzerCompilationStartAnalysisContext = new AnalyzerCompilationStartAnalysisContext(compilationStartAnalyzerAction.Analyzer, compilationScope, this.Compilation, this.AnalyzerOptions, this._compilationAnalysisValueProviderFactory, this._cancellationToken);
				this.ExecuteAndCatchIfThrows<ValueTuple<Action<CompilationStartAnalysisContext>, AnalyzerCompilationStartAnalysisContext>>(compilationStartAnalyzerAction.Analyzer, delegate([TupleElementNames(new string[] { "action", "context" })] ValueTuple<Action<CompilationStartAnalysisContext>, AnalyzerCompilationStartAnalysisContext> data)
				{
					data.Item1.Invoke(data.Item2);
				}, new ValueTuple<Action<CompilationStartAnalysisContext>, AnalyzerCompilationStartAnalysisContext>(compilationStartAnalyzerAction.Action, analyzerCompilationStartAnalysisContext), new AnalysisContextInfo?(new AnalysisContextInfo(this.Compilation)));
			}
		}

		// Token: 0x060021B3 RID: 8627 RVA: 0x00069CB8 File Offset: 0x00067EB8
		public void ExecuteSymbolStartActions(ISymbol symbol, DiagnosticAnalyzer analyzer, [Nullable(new byte[] { 0, 1 })] ImmutableArray<SymbolStartAnalyzerAction> actions, HostSymbolStartAnalysisScope symbolScope)
		{
			if (this.IsAnalyzerSuppressedForSymbol(analyzer, symbol))
			{
				return;
			}
			foreach (SymbolStartAnalyzerAction symbolStartAnalyzerAction in actions)
			{
				this._cancellationToken.ThrowIfCancellationRequested();
				AnalyzerSymbolStartAnalysisContext analyzerSymbolStartAnalysisContext = new AnalyzerSymbolStartAnalysisContext(symbolStartAnalyzerAction.Analyzer, symbolScope, symbol, this.Compilation, this.AnalyzerOptions, this._cancellationToken);
				this.ExecuteAndCatchIfThrows<ValueTuple<Action<SymbolStartAnalysisContext>, AnalyzerSymbolStartAnalysisContext>>(symbolStartAnalyzerAction.Analyzer, delegate([TupleElementNames(new string[] { "action", "context" })] ValueTuple<Action<SymbolStartAnalysisContext>, AnalyzerSymbolStartAnalysisContext> data)
				{
					data.Item1.Invoke(data.Item2);
				}, new ValueTuple<Action<SymbolStartAnalysisContext>, AnalyzerSymbolStartAnalysisContext>(symbolStartAnalyzerAction.Action, analyzerSymbolStartAnalysisContext), new AnalysisContextInfo?(new AnalysisContextInfo(this.Compilation, symbol)));
			}
		}

		// Token: 0x060021B4 RID: 8628 RVA: 0x00069D68 File Offset: 0x00067F68
		public void ExecuteSuppressionAction(DiagnosticSuppressor suppressor, [Nullable(new byte[] { 0, 1 })] ImmutableArray<Diagnostic> reportedDiagnostics)
		{
			if (reportedDiagnostics.IsEmpty)
			{
				return;
			}
			this._cancellationToken.ThrowIfCancellationRequested();
			Func<SuppressionDescriptor, bool> func = new Func<SuppressionDescriptor, bool>(this._analyzerManager.GetSupportedSuppressionDescriptors(suppressor, this).Contains);
			Action<SuppressionAnalysisContext> action = new Action<SuppressionAnalysisContext>(suppressor.ReportSuppressions);
			SuppressionAnalysisContext suppressionAnalysisContext = new SuppressionAnalysisContext(this.Compilation, this.AnalyzerOptions, reportedDiagnostics, this._addSuppression, func, this._getSemanticModel, this._cancellationToken);
			this.ExecuteAndCatchIfThrows<ValueTuple<Action<SuppressionAnalysisContext>, SuppressionAnalysisContext>>(suppressor, delegate([TupleElementNames(new string[] { "action", "context" })] ValueTuple<Action<SuppressionAnalysisContext>, SuppressionAnalysisContext> data)
			{
				data.Item1.Invoke(data.Item2);
			}, new ValueTuple<Action<SuppressionAnalysisContext>, SuppressionAnalysisContext>(action, suppressionAnalysisContext), new AnalysisContextInfo?(new AnalysisContextInfo(this.Compilation)));
		}

		// Token: 0x060021B5 RID: 8629 RVA: 0x00069E20 File Offset: 0x00068020
		public bool TryExecuteCompilationActions([Nullable(new byte[] { 0, 1 })] ImmutableArray<CompilationAnalyzerAction> compilationActions, DiagnosticAnalyzer analyzer, CompilationEvent compilationEvent, AnalysisScope analysisScope, [Nullable(2)] AnalysisState analysisState)
		{
			AnalysisState.AnalyzerStateData analyzerStateData = null;
			bool flag;
			try
			{
				if (AnalyzerExecutor.TryStartProcessingEvent(compilationEvent, analyzer, analysisScope, analysisState, out analyzerStateData))
				{
					this.ExecuteCompilationActionsCore(compilationActions, analyzer, analyzerStateData);
					if (analysisState != null)
					{
						analysisState.MarkEventComplete(compilationEvent, analyzer);
					}
					flag = true;
				}
				else
				{
					flag = AnalyzerExecutor.IsEventComplete(compilationEvent, analyzer, analysisState);
				}
			}
			finally
			{
				if (analyzerStateData != null)
				{
					analyzerStateData.ResetToReadyState();
				}
			}
			return flag;
		}

		// Token: 0x060021B6 RID: 8630 RVA: 0x00069E80 File Offset: 0x00068080
		private void ExecuteCompilationActionsCore([Nullable(new byte[] { 0, 1 })] ImmutableArray<CompilationAnalyzerAction> compilationActions, DiagnosticAnalyzer analyzer, [Nullable(2)] AnalysisState.AnalyzerStateData analyzerState)
		{
			Action<Diagnostic> addCompilationDiagnostic = this.GetAddCompilationDiagnostic(analyzer);
			Func<Diagnostic, bool> func;
			using (PooledDelegates.GetPooledFunction<Diagnostic, ValueTuple<AnalyzerExecutor, DiagnosticAnalyzer>, bool>((Diagnostic d, [TupleElementNames(new string[] { "self", "analyzer" })] ValueTuple<AnalyzerExecutor, DiagnosticAnalyzer> arg) => arg.Item1.IsSupportedDiagnostic(arg.Item2, d), new ValueTuple<AnalyzerExecutor, DiagnosticAnalyzer>(this, analyzer), out func))
			{
				foreach (CompilationAnalyzerAction compilationAnalyzerAction in compilationActions)
				{
					this._cancellationToken.ThrowIfCancellationRequested();
					if (AnalyzerExecutor.ShouldExecuteAction(analyzerState, compilationAnalyzerAction))
					{
						CompilationAnalysisContext compilationAnalysisContext = new CompilationAnalysisContext(this.Compilation, this.AnalyzerOptions, addCompilationDiagnostic, func, this._compilationAnalysisValueProviderFactory, this._cancellationToken);
						this.ExecuteAndCatchIfThrows<ValueTuple<Action<CompilationAnalysisContext>, CompilationAnalysisContext>>(compilationAnalyzerAction.Analyzer, delegate([TupleElementNames(new string[] { "action", "context" })] ValueTuple<Action<CompilationAnalysisContext>, CompilationAnalysisContext> data)
						{
							data.Item1.Invoke(data.Item2);
						}, new ValueTuple<Action<CompilationAnalysisContext>, CompilationAnalysisContext>(compilationAnalyzerAction.Action, compilationAnalysisContext), new AnalysisContextInfo?(new AnalysisContextInfo(this.Compilation)));
						if (analyzerState != null)
						{
							analyzerState.ProcessedActions.Add(compilationAnalyzerAction);
						}
					}
				}
			}
		}

		// Token: 0x060021B7 RID: 8631 RVA: 0x00069F9C File Offset: 0x0006819C
		public bool TryExecuteSymbolActions([Nullable(new byte[] { 0, 1 })] ImmutableArray<SymbolAnalyzerAction> symbolActions, DiagnosticAnalyzer analyzer, SymbolDeclaredCompilationEvent symbolDeclaredEvent, Func<ISymbol, SyntaxReference, Compilation, CancellationToken, SyntaxNode> getTopMostNodeForAnalysis, AnalysisScope analysisScope, [Nullable(2)] AnalysisState analysisState, bool isGeneratedCodeSymbol)
		{
			AnalysisState.AnalyzerStateData analyzerStateData = null;
			bool flag;
			try
			{
				ISymbol symbol = symbolDeclaredEvent.Symbol;
				if (AnalyzerExecutor.TryStartAnalyzingSymbol(symbol, analyzer, analysisScope, analysisState, out analyzerStateData))
				{
					this.ExecuteSymbolActionsCore(symbolActions, analyzer, symbolDeclaredEvent, getTopMostNodeForAnalysis, analyzerStateData, isGeneratedCodeSymbol);
					if (analysisState != null)
					{
						analysisState.MarkSymbolComplete(symbol, analyzer);
					}
					flag = true;
				}
				else
				{
					flag = AnalyzerExecutor.IsSymbolComplete(symbol, analyzer, analysisState);
				}
			}
			finally
			{
				if (analyzerStateData != null)
				{
					analyzerStateData.ResetToReadyState();
				}
			}
			return flag;
		}

		// Token: 0x060021B8 RID: 8632 RVA: 0x0006A008 File Offset: 0x00068208
		private void ExecuteSymbolActionsCore([Nullable(new byte[] { 0, 1 })] ImmutableArray<SymbolAnalyzerAction> symbolActions, DiagnosticAnalyzer analyzer, SymbolDeclaredCompilationEvent symbolDeclaredEvent, Func<ISymbol, SyntaxReference, Compilation, CancellationToken, SyntaxNode> getTopMostNodeForAnalysis, [Nullable(2)] AnalysisState.AnalyzerStateData analyzerState, bool isGeneratedCodeSymbol)
		{
			if ((isGeneratedCodeSymbol && this._shouldSkipAnalysisOnGeneratedCode.Invoke(analyzer)) || this.IsAnalyzerSuppressedForSymbol(analyzer, symbolDeclaredEvent.Symbol))
			{
				return;
			}
			ISymbol symbol = symbolDeclaredEvent.Symbol;
			Action<Diagnostic> addDiagnostic = this.GetAddDiagnostic(symbol, symbolDeclaredEvent.DeclaringSyntaxReferences, analyzer, getTopMostNodeForAnalysis);
			Func<Diagnostic, bool> func;
			using (PooledDelegates.GetPooledFunction<Diagnostic, ValueTuple<AnalyzerExecutor, DiagnosticAnalyzer>, bool>((Diagnostic d, [TupleElementNames(new string[] { "self", "analyzer" })] ValueTuple<AnalyzerExecutor, DiagnosticAnalyzer> arg) => arg.Item1.IsSupportedDiagnostic(arg.Item2, d), new ValueTuple<AnalyzerExecutor, DiagnosticAnalyzer>(this, analyzer), out func))
			{
				foreach (SymbolAnalyzerAction symbolAnalyzerAction in symbolActions)
				{
					Action<SymbolAnalysisContext> action = symbolAnalyzerAction.Action;
					if (symbolAnalyzerAction.Kinds.Contains(symbol.Kind) && AnalyzerExecutor.ShouldExecuteAction(analyzerState, symbolAnalyzerAction))
					{
						this._cancellationToken.ThrowIfCancellationRequested();
						SymbolAnalysisContext symbolAnalysisContext = new SymbolAnalysisContext(symbol, this.Compilation, this.AnalyzerOptions, addDiagnostic, func, this._cancellationToken);
						this.ExecuteAndCatchIfThrows<ValueTuple<Action<SymbolAnalysisContext>, SymbolAnalysisContext>>(symbolAnalyzerAction.Analyzer, delegate([TupleElementNames(new string[] { "action", "context" })] ValueTuple<Action<SymbolAnalysisContext>, SymbolAnalysisContext> data)
						{
							data.Item1.Invoke(data.Item2);
						}, new ValueTuple<Action<SymbolAnalysisContext>, SymbolAnalysisContext>(action, symbolAnalysisContext), new AnalysisContextInfo?(new AnalysisContextInfo(this.Compilation, symbol)));
						if (analyzerState != null)
						{
							analyzerState.ProcessedActions.Add(symbolAnalyzerAction);
						}
					}
				}
			}
		}

		// Token: 0x060021B9 RID: 8633 RVA: 0x0006A178 File Offset: 0x00068378
		public bool TryExecuteSymbolEndActionsForContainer(INamespaceOrTypeSymbol containingSymbol, ISymbol processedMemberSymbol, DiagnosticAnalyzer analyzer, Func<ISymbol, SyntaxReference, Compilation, CancellationToken, SyntaxNode> getTopMostNodeForAnalysis, [Nullable(2)] AnalysisState analysisState, [Nullable(2)] [NotNullWhen(true)] out SymbolDeclaredCompilationEvent containingSymbolDeclaredEvent)
		{
			containingSymbolDeclaredEvent = null;
			ValueTuple<ImmutableArray<SymbolEndAnalyzerAction>, SymbolDeclaredCompilationEvent> valueTuple;
			if (!this._analyzerManager.TryProcessCompletedMemberAndGetPendingSymbolEndActionsForContainer(containingSymbol, processedMemberSymbol, analyzer, out valueTuple))
			{
				return false;
			}
			ImmutableArray<SymbolEndAnalyzerAction> item = valueTuple.Item1;
			containingSymbolDeclaredEvent = valueTuple.Item2;
			return this.TryExecuteSymbolEndActionsCore(item, analyzer, containingSymbolDeclaredEvent, getTopMostNodeForAnalysis, analysisState);
		}

		// Token: 0x060021BA RID: 8634 RVA: 0x0006A1BC File Offset: 0x000683BC
		public bool TryExecuteSymbolEndActions([Nullable(new byte[] { 0, 1 })] ImmutableArray<SymbolEndAnalyzerAction> symbolEndActions, DiagnosticAnalyzer analyzer, SymbolDeclaredCompilationEvent symbolDeclaredEvent, Func<ISymbol, SyntaxReference, Compilation, CancellationToken, SyntaxNode> getTopMostNodeForAnalysis, [Nullable(2)] AnalysisState analysisState)
		{
			return this._analyzerManager.TryStartExecuteSymbolEndActions(symbolEndActions, analyzer, symbolDeclaredEvent) && this.TryExecuteSymbolEndActionsCore(symbolEndActions, analyzer, symbolDeclaredEvent, getTopMostNodeForAnalysis, analysisState);
		}

		// Token: 0x060021BB RID: 8635 RVA: 0x0006A1E0 File Offset: 0x000683E0
		private bool TryExecuteSymbolEndActionsCore([Nullable(new byte[] { 0, 1 })] ImmutableArray<SymbolEndAnalyzerAction> symbolEndActions, DiagnosticAnalyzer analyzer, SymbolDeclaredCompilationEvent symbolDeclaredEvent, Func<ISymbol, SyntaxReference, Compilation, CancellationToken, SyntaxNode> getTopMostNodeForAnalysis, [Nullable(2)] AnalysisState analysisState)
		{
			ISymbol symbol = symbolDeclaredEvent.Symbol;
			AnalysisState.AnalyzerStateData analyzerStateData = null;
			bool flag;
			try
			{
				if (AnalyzerExecutor.TryStartSymbolEndAnalysis(symbol, analyzer, analysisState, out analyzerStateData))
				{
					this.ExecuteSymbolEndActionsCore(symbolEndActions, analyzer, symbolDeclaredEvent, getTopMostNodeForAnalysis, analyzerStateData);
					this.MarkSymbolEndAnalysisComplete(symbol, analyzer, analysisState);
					flag = true;
				}
				else if (!AnalyzerExecutor.IsSymbolEndAnalysisComplete(symbol, analyzer, analysisState))
				{
					this._analyzerManager.MarkSymbolEndAnalysisPending(symbol, analyzer, symbolEndActions, symbolDeclaredEvent);
					flag = false;
				}
				else
				{
					flag = true;
				}
			}
			finally
			{
				if (analyzerStateData != null)
				{
					analyzerStateData.ResetToReadyState();
				}
			}
			return flag;
		}

		// Token: 0x060021BC RID: 8636 RVA: 0x0006A25C File Offset: 0x0006845C
		public void MarkSymbolEndAnalysisComplete(ISymbol symbol, DiagnosticAnalyzer analyzer, [Nullable(2)] AnalysisState analysisState)
		{
			if (analysisState != null)
			{
				analysisState.MarkSymbolEndAnalysisComplete(symbol, analyzer);
			}
			this._analyzerManager.MarkSymbolEndAnalysisComplete(symbol, analyzer);
		}

		// Token: 0x060021BD RID: 8637 RVA: 0x0006A278 File Offset: 0x00068478
		private void ExecuteSymbolEndActionsCore([Nullable(new byte[] { 0, 1 })] ImmutableArray<SymbolEndAnalyzerAction> symbolEndActions, DiagnosticAnalyzer analyzer, SymbolDeclaredCompilationEvent symbolDeclaredEvent, Func<ISymbol, SyntaxReference, Compilation, CancellationToken, SyntaxNode> getTopMostNodeForAnalysis, [Nullable(2)] AnalysisState.AnalyzerStateData analyzerState)
		{
			ISymbol symbol = symbolDeclaredEvent.Symbol;
			Action<Diagnostic> addDiagnostic = this.GetAddDiagnostic(symbol, symbolDeclaredEvent.DeclaringSyntaxReferences, analyzer, getTopMostNodeForAnalysis);
			Func<Diagnostic, bool> func;
			using (PooledDelegates.GetPooledFunction<Diagnostic, ValueTuple<AnalyzerExecutor, DiagnosticAnalyzer>, bool>((Diagnostic d, [TupleElementNames(new string[] { "self", "analyzer" })] ValueTuple<AnalyzerExecutor, DiagnosticAnalyzer> arg) => arg.Item1.IsSupportedDiagnostic(arg.Item2, d), new ValueTuple<AnalyzerExecutor, DiagnosticAnalyzer>(this, analyzer), out func))
			{
				foreach (SymbolEndAnalyzerAction symbolEndAnalyzerAction in symbolEndActions)
				{
					Action<SymbolAnalysisContext> action = symbolEndAnalyzerAction.Action;
					if (AnalyzerExecutor.ShouldExecuteAction(analyzerState, symbolEndAnalyzerAction))
					{
						this._cancellationToken.ThrowIfCancellationRequested();
						SymbolAnalysisContext symbolAnalysisContext = new SymbolAnalysisContext(symbol, this.Compilation, this.AnalyzerOptions, addDiagnostic, func, this._cancellationToken);
						this.ExecuteAndCatchIfThrows<ValueTuple<Action<SymbolAnalysisContext>, SymbolAnalysisContext>>(symbolEndAnalyzerAction.Analyzer, delegate([TupleElementNames(new string[] { "action", "context" })] ValueTuple<Action<SymbolAnalysisContext>, SymbolAnalysisContext> data)
						{
							data.Item1.Invoke(data.Item2);
						}, new ValueTuple<Action<SymbolAnalysisContext>, SymbolAnalysisContext>(action, symbolAnalysisContext), new AnalysisContextInfo?(new AnalysisContextInfo(this.Compilation, symbol)));
						if (analyzerState != null)
						{
							analyzerState.ProcessedActions.Add(symbolEndAnalyzerAction);
						}
					}
				}
			}
		}

		// Token: 0x060021BE RID: 8638 RVA: 0x0006A3AC File Offset: 0x000685AC
		public bool TryExecuteSemanticModelActions([Nullable(new byte[] { 0, 1 })] ImmutableArray<SemanticModelAnalyzerAction> semanticModelActions, DiagnosticAnalyzer analyzer, SemanticModel semanticModel, CompilationEvent compilationUnitCompletedEvent, AnalysisScope analysisScope, [Nullable(2)] AnalysisState analysisState, bool isGeneratedCode)
		{
			AnalysisState.AnalyzerStateData analyzerStateData = null;
			bool flag;
			try
			{
				if (AnalyzerExecutor.TryStartProcessingEvent(compilationUnitCompletedEvent, analyzer, analysisScope, analysisState, out analyzerStateData))
				{
					this.ExecuteSemanticModelActionsCore(semanticModelActions, analyzer, semanticModel, analyzerStateData, isGeneratedCode);
					if (analysisState != null)
					{
						analysisState.MarkEventComplete(compilationUnitCompletedEvent, analyzer);
					}
					flag = true;
				}
				else
				{
					flag = AnalyzerExecutor.IsEventComplete(compilationUnitCompletedEvent, analyzer, analysisState);
				}
			}
			finally
			{
				if (analyzerStateData != null)
				{
					analyzerStateData.ResetToReadyState();
				}
			}
			return flag;
		}

		// Token: 0x060021BF RID: 8639 RVA: 0x0006A414 File Offset: 0x00068614
		private void ExecuteSemanticModelActionsCore([Nullable(new byte[] { 0, 1 })] ImmutableArray<SemanticModelAnalyzerAction> semanticModelActions, DiagnosticAnalyzer analyzer, SemanticModel semanticModel, [Nullable(2)] AnalysisState.AnalyzerStateData analyzerState, bool isGeneratedCode)
		{
			if ((isGeneratedCode && this._shouldSkipAnalysisOnGeneratedCode.Invoke(analyzer)) || this.IsAnalyzerSuppressedForTree(analyzer, semanticModel.SyntaxTree))
			{
				return;
			}
			AnalyzerExecutor.AnalyzerDiagnosticReporter addSemanticDiagnostic = this.GetAddSemanticDiagnostic(semanticModel.SyntaxTree, analyzer);
			Func<Diagnostic, bool> func;
			using (PooledDelegates.GetPooledFunction<Diagnostic, ValueTuple<AnalyzerExecutor, DiagnosticAnalyzer>, bool>((Diagnostic d, [TupleElementNames(new string[] { "self", "analyzer" })] ValueTuple<AnalyzerExecutor, DiagnosticAnalyzer> arg) => arg.Item1.IsSupportedDiagnostic(arg.Item2, d), new ValueTuple<AnalyzerExecutor, DiagnosticAnalyzer>(this, analyzer), out func))
			{
				foreach (SemanticModelAnalyzerAction semanticModelAnalyzerAction in semanticModelActions)
				{
					if (AnalyzerExecutor.ShouldExecuteAction(analyzerState, semanticModelAnalyzerAction))
					{
						this._cancellationToken.ThrowIfCancellationRequested();
						SemanticModelAnalysisContext semanticModelAnalysisContext = new SemanticModelAnalysisContext(semanticModel, this.AnalyzerOptions, addSemanticDiagnostic.AddDiagnosticAction, func, this._cancellationToken);
						this.ExecuteAndCatchIfThrows<ValueTuple<Action<SemanticModelAnalysisContext>, SemanticModelAnalysisContext>>(semanticModelAnalyzerAction.Analyzer, delegate([TupleElementNames(new string[] { "action", "context" })] ValueTuple<Action<SemanticModelAnalysisContext>, SemanticModelAnalysisContext> data)
						{
							data.Item1.Invoke(data.Item2);
						}, new ValueTuple<Action<SemanticModelAnalysisContext>, SemanticModelAnalysisContext>(semanticModelAnalyzerAction.Action, semanticModelAnalysisContext), new AnalysisContextInfo?(new AnalysisContextInfo(semanticModel)));
						if (analyzerState != null)
						{
							analyzerState.ProcessedActions.Add(semanticModelAnalyzerAction);
						}
					}
				}
				addSemanticDiagnostic.Free();
			}
		}

		// Token: 0x060021C0 RID: 8640 RVA: 0x0006A558 File Offset: 0x00068758
		public bool TryExecuteSyntaxTreeActions([Nullable(new byte[] { 0, 1 })] ImmutableArray<SyntaxTreeAnalyzerAction> syntaxTreeActions, DiagnosticAnalyzer analyzer, SourceOrAdditionalFile file, AnalysisScope analysisScope, [Nullable(2)] AnalysisState analysisState, bool isGeneratedCode)
		{
			AnalysisState.AnalyzerStateData analyzerStateData = null;
			bool flag;
			try
			{
				if (AnalyzerExecutor.TryStartSyntaxAnalysis(file, analyzer, analysisScope, analysisState, out analyzerStateData))
				{
					this.ExecuteSyntaxTreeActionsCore(syntaxTreeActions, analyzer, file, analyzerStateData, isGeneratedCode);
					if (analysisState != null)
					{
						analysisState.MarkSyntaxAnalysisComplete(file, analyzer);
					}
					flag = true;
				}
				else
				{
					flag = analysisState == null || !analysisState.HasPendingSyntaxAnalysis(analysisScope);
				}
			}
			finally
			{
				if (analyzerStateData != null)
				{
					analyzerStateData.ResetToReadyState();
				}
			}
			return flag;
		}

		// Token: 0x060021C1 RID: 8641 RVA: 0x0006A5C4 File Offset: 0x000687C4
		private void ExecuteSyntaxTreeActionsCore([Nullable(new byte[] { 0, 1 })] ImmutableArray<SyntaxTreeAnalyzerAction> syntaxTreeActions, DiagnosticAnalyzer analyzer, SourceOrAdditionalFile file, [Nullable(2)] AnalysisState.AnalyzerStateData analyzerState, bool isGeneratedCode)
		{
			SyntaxTree sourceTree = file.SourceTree;
			if ((isGeneratedCode && this._shouldSkipAnalysisOnGeneratedCode.Invoke(analyzer)) || this.IsAnalyzerSuppressedForTree(analyzer, sourceTree))
			{
				return;
			}
			AnalyzerExecutor.AnalyzerDiagnosticReporter addSyntaxDiagnostic = this.GetAddSyntaxDiagnostic(file, analyzer);
			Func<Diagnostic, bool> func;
			using (PooledDelegates.GetPooledFunction<Diagnostic, ValueTuple<AnalyzerExecutor, DiagnosticAnalyzer>, bool>((Diagnostic d, [TupleElementNames(new string[] { "self", "analyzer" })] ValueTuple<AnalyzerExecutor, DiagnosticAnalyzer> arg) => arg.Item1.IsSupportedDiagnostic(arg.Item2, d), new ValueTuple<AnalyzerExecutor, DiagnosticAnalyzer>(this, analyzer), out func))
			{
				foreach (SyntaxTreeAnalyzerAction syntaxTreeAnalyzerAction in syntaxTreeActions)
				{
					if (AnalyzerExecutor.ShouldExecuteAction(analyzerState, syntaxTreeAnalyzerAction))
					{
						this._cancellationToken.ThrowIfCancellationRequested();
						SyntaxTreeAnalysisContext syntaxTreeAnalysisContext = new SyntaxTreeAnalysisContext(sourceTree, this.AnalyzerOptions, addSyntaxDiagnostic.AddDiagnosticAction, func, this.Compilation, this._cancellationToken);
						this.ExecuteAndCatchIfThrows<ValueTuple<Action<SyntaxTreeAnalysisContext>, SyntaxTreeAnalysisContext>>(syntaxTreeAnalyzerAction.Analyzer, delegate([TupleElementNames(new string[] { "action", "context" })] ValueTuple<Action<SyntaxTreeAnalysisContext>, SyntaxTreeAnalysisContext> data)
						{
							data.Item1.Invoke(data.Item2);
						}, new ValueTuple<Action<SyntaxTreeAnalysisContext>, SyntaxTreeAnalysisContext>(syntaxTreeAnalyzerAction.Action, syntaxTreeAnalysisContext), new AnalysisContextInfo?(new AnalysisContextInfo(this.Compilation, file)));
						if (analyzerState != null)
						{
							analyzerState.ProcessedActions.Add(syntaxTreeAnalyzerAction);
						}
					}
				}
				addSyntaxDiagnostic.Free();
			}
		}

		// Token: 0x060021C2 RID: 8642 RVA: 0x0006A714 File Offset: 0x00068914
		public bool TryExecuteAdditionalFileActions([Nullable(new byte[] { 0, 1 })] ImmutableArray<AdditionalFileAnalyzerAction> additionalFileActions, DiagnosticAnalyzer analyzer, SourceOrAdditionalFile file, AnalysisScope analysisScope, [Nullable(2)] AnalysisState analysisState)
		{
			AnalysisState.AnalyzerStateData analyzerStateData = null;
			bool flag;
			try
			{
				if (AnalyzerExecutor.TryStartSyntaxAnalysis(file, analyzer, analysisScope, analysisState, out analyzerStateData))
				{
					this.ExecuteAdditionalFileActionsCore(additionalFileActions, analyzer, file, analyzerStateData);
					if (analysisState != null)
					{
						analysisState.MarkSyntaxAnalysisComplete(file, analyzer);
					}
					flag = true;
				}
				else
				{
					flag = analysisState == null || !analysisState.HasPendingSyntaxAnalysis(analysisScope);
				}
			}
			finally
			{
				if (analyzerStateData != null)
				{
					analyzerStateData.ResetToReadyState();
				}
			}
			return flag;
		}

		// Token: 0x060021C3 RID: 8643 RVA: 0x0006A780 File Offset: 0x00068980
		private void ExecuteAdditionalFileActionsCore([Nullable(new byte[] { 0, 1 })] ImmutableArray<AdditionalFileAnalyzerAction> additionalFileActions, DiagnosticAnalyzer analyzer, SourceOrAdditionalFile file, [Nullable(2)] AnalysisState.AnalyzerStateData analyzerState)
		{
			AdditionalText additionalFile = file.AdditionalFile;
			AnalyzerExecutor.AnalyzerDiagnosticReporter addSyntaxDiagnostic = this.GetAddSyntaxDiagnostic(file, analyzer);
			Func<Diagnostic, bool> func;
			using (PooledDelegates.GetPooledFunction<Diagnostic, ValueTuple<AnalyzerExecutor, DiagnosticAnalyzer>, bool>((Diagnostic d, [TupleElementNames(new string[] { "self", "analyzer" })] ValueTuple<AnalyzerExecutor, DiagnosticAnalyzer> arg) => arg.Item1.IsSupportedDiagnostic(arg.Item2, d), new ValueTuple<AnalyzerExecutor, DiagnosticAnalyzer>(this, analyzer), out func))
			{
				foreach (AdditionalFileAnalyzerAction additionalFileAnalyzerAction in additionalFileActions)
				{
					if (AnalyzerExecutor.ShouldExecuteAction(analyzerState, additionalFileAnalyzerAction))
					{
						this._cancellationToken.ThrowIfCancellationRequested();
						AdditionalFileAnalysisContext additionalFileAnalysisContext = new AdditionalFileAnalysisContext(additionalFile, this.AnalyzerOptions, addSyntaxDiagnostic.AddDiagnosticAction, func, this.Compilation, this._cancellationToken);
						this.ExecuteAndCatchIfThrows<ValueTuple<Action<AdditionalFileAnalysisContext>, AdditionalFileAnalysisContext>>(additionalFileAnalyzerAction.Analyzer, delegate([TupleElementNames(new string[] { "action", "context" })] ValueTuple<Action<AdditionalFileAnalysisContext>, AdditionalFileAnalysisContext> data)
						{
							data.Item1.Invoke(data.Item2);
						}, new ValueTuple<Action<AdditionalFileAnalysisContext>, AdditionalFileAnalysisContext>(additionalFileAnalyzerAction.Action, additionalFileAnalysisContext), new AnalysisContextInfo?(new AnalysisContextInfo(this.Compilation, file)));
						if (analyzerState != null)
						{
							analyzerState.ProcessedActions.Add(additionalFileAnalyzerAction);
						}
					}
				}
				addSyntaxDiagnostic.Free();
			}
		}

		// Token: 0x060021C4 RID: 8644 RVA: 0x0006A8B4 File Offset: 0x00068AB4
		private void ExecuteSyntaxNodeAction<[Nullable(0)] TLanguageKindEnum>([Nullable(new byte[] { 1, 0 })] SyntaxNodeAnalyzerAction<TLanguageKindEnum> syntaxNodeAction, SyntaxNode node, ISymbol containingSymbol, SemanticModel semanticModel, Action<Diagnostic> addDiagnostic, Func<Diagnostic, bool> isSupportedDiagnostic, [Nullable(2)] AnalysisState.SyntaxNodeAnalyzerStateData analyzerState) where TLanguageKindEnum : struct
		{
			if (AnalyzerExecutor.ShouldExecuteAction(analyzerState, syntaxNodeAction))
			{
				SyntaxNodeAnalysisContext syntaxNodeAnalysisContext = new SyntaxNodeAnalysisContext(node, containingSymbol, semanticModel, this.AnalyzerOptions, addDiagnostic, isSupportedDiagnostic, this._cancellationToken);
				this.ExecuteAndCatchIfThrows<ValueTuple<Action<SyntaxNodeAnalysisContext>, SyntaxNodeAnalysisContext>>(syntaxNodeAction.Analyzer, delegate([TupleElementNames(new string[] { "action", "context" })] ValueTuple<Action<SyntaxNodeAnalysisContext>, SyntaxNodeAnalysisContext> data)
				{
					data.Item1.Invoke(data.Item2);
				}, new ValueTuple<Action<SyntaxNodeAnalysisContext>, SyntaxNodeAnalysisContext>(syntaxNodeAction.Action, syntaxNodeAnalysisContext), new AnalysisContextInfo?(new AnalysisContextInfo(this.Compilation, node)));
				if (analyzerState != null)
				{
					analyzerState.ProcessedActions.Add(syntaxNodeAction);
				}
			}
		}

		// Token: 0x060021C5 RID: 8645 RVA: 0x0006A940 File Offset: 0x00068B40
		private void ExecuteOperationAction(OperationAnalyzerAction operationAction, IOperation operation, ISymbol containingSymbol, SemanticModel semanticModel, Action<Diagnostic> addDiagnostic, Func<Diagnostic, bool> isSupportedDiagnostic, [Nullable(2)] AnalysisState.OperationAnalyzerStateData analyzerState)
		{
			if (AnalyzerExecutor.ShouldExecuteAction(analyzerState, operationAction))
			{
				OperationAnalysisContext operationAnalysisContext = new OperationAnalysisContext(operation, containingSymbol, semanticModel.Compilation, this.AnalyzerOptions, addDiagnostic, isSupportedDiagnostic, this.GetControlFlowGraph, this._cancellationToken);
				this.ExecuteAndCatchIfThrows<ValueTuple<Action<OperationAnalysisContext>, OperationAnalysisContext>>(operationAction.Analyzer, delegate([TupleElementNames(new string[] { "action", "context" })] ValueTuple<Action<OperationAnalysisContext>, OperationAnalysisContext> data)
				{
					data.Item1.Invoke(data.Item2);
				}, new ValueTuple<Action<OperationAnalysisContext>, OperationAnalysisContext>(operationAction.Action, operationAnalysisContext), new AnalysisContextInfo?(new AnalysisContextInfo(this.Compilation, operation)));
				if (analyzerState != null)
				{
					analyzerState.ProcessedActions.Add(operationAction);
				}
			}
		}

		// Token: 0x060021C6 RID: 8646 RVA: 0x0006A9DC File Offset: 0x00068BDC
		public bool TryExecuteCodeBlockActions<[Nullable(0)] TLanguageKindEnum>([Nullable(new byte[] { 1, 1, 0 })] IEnumerable<CodeBlockStartAnalyzerAction<TLanguageKindEnum>> codeBlockStartActions, IEnumerable<CodeBlockAnalyzerAction> codeBlockActions, IEnumerable<CodeBlockAnalyzerAction> codeBlockEndActions, DiagnosticAnalyzer analyzer, SyntaxNode declaredNode, ISymbol declaredSymbol, [Nullable(new byte[] { 0, 1 })] ImmutableArray<SyntaxNode> executableCodeBlocks, SemanticModel semanticModel, [Nullable(new byte[] { 1, 1, 0 })] Func<SyntaxNode, TLanguageKindEnum> getKind, int declarationIndex, AnalysisScope analysisScope, [Nullable(2)] AnalysisState analysisState, bool isGeneratedCode) where TLanguageKindEnum : struct
		{
			AnalysisState.DeclarationAnalyzerStateData declarationAnalyzerStateData = null;
			bool flag;
			try
			{
				if (AnalyzerExecutor.TryStartAnalyzingDeclaration(declaredSymbol, declarationIndex, analyzer, analysisScope, analysisState, out declarationAnalyzerStateData))
				{
					Func<SyntaxNode, IEnumerable<SyntaxNode>> <>9__1;
					this.ExecuteBlockActionsCore<CodeBlockStartAnalyzerAction<TLanguageKindEnum>, CodeBlockAnalyzerAction, SyntaxNodeAnalyzerAction<TLanguageKindEnum>, AnalysisState.SyntaxNodeAnalyzerStateData, SyntaxNode, TLanguageKindEnum>(codeBlockStartActions, codeBlockActions, codeBlockEndActions, analyzer, declaredNode, declaredSymbol, executableCodeBlocks, delegate([Nullable(new byte[] { 0, 1 })] ImmutableArray<SyntaxNode> codeBlocks)
					{
						IEnumerable<SyntaxNode> enumerable = codeBlocks;
						Func<SyntaxNode, IEnumerable<SyntaxNode>> func;
						if ((func = <>9__1) == null)
						{
							func = (<>9__1 = delegate(SyntaxNode cb)
							{
								Func<SyntaxNode, bool> syntaxNodesToAnalyzeFilter = semanticModel.GetSyntaxNodesToAnalyzeFilter(cb, declaredSymbol);
								if (syntaxNodesToAnalyzeFilter != null)
								{
									return Enumerable.Where<SyntaxNode>(cb.DescendantNodesAndSelf(syntaxNodesToAnalyzeFilter, false), syntaxNodesToAnalyzeFilter);
								}
								return cb.DescendantNodesAndSelf(null, false);
							});
						}
						return Enumerable.SelectMany<SyntaxNode, SyntaxNode>(enumerable, func);
					}, semanticModel, getKind, (declarationAnalyzerStateData != null) ? declarationAnalyzerStateData.CodeBlockAnalysisState : null, isGeneratedCode);
					flag = true;
				}
				else
				{
					flag = AnalyzerExecutor.IsDeclarationComplete(declaredSymbol, declarationIndex, analyzer, analysisState);
				}
			}
			finally
			{
				if (declarationAnalyzerStateData != null)
				{
					declarationAnalyzerStateData.ResetToReadyState();
				}
			}
			return flag;
		}

		// Token: 0x060021C7 RID: 8647 RVA: 0x0006AA84 File Offset: 0x00068C84
		public bool TryExecuteOperationBlockActions(IEnumerable<OperationBlockStartAnalyzerAction> operationBlockStartActions, IEnumerable<OperationBlockAnalyzerAction> operationBlockActions, IEnumerable<OperationBlockAnalyzerAction> operationBlockEndActions, DiagnosticAnalyzer analyzer, SyntaxNode declaredNode, ISymbol declaredSymbol, [Nullable(new byte[] { 0, 1 })] ImmutableArray<IOperation> operationBlocks, [Nullable(new byte[] { 0, 1 })] ImmutableArray<IOperation> operations, SemanticModel semanticModel, int declarationIndex, AnalysisScope analysisScope, [Nullable(2)] AnalysisState analysisState, bool isGeneratedCode)
		{
			AnalysisState.DeclarationAnalyzerStateData declarationAnalyzerStateData = null;
			bool flag;
			try
			{
				if (AnalyzerExecutor.TryStartAnalyzingDeclaration(declaredSymbol, declarationIndex, analyzer, analysisScope, analysisState, out declarationAnalyzerStateData))
				{
					this.ExecuteBlockActionsCore<OperationBlockStartAnalyzerAction, OperationBlockAnalyzerAction, OperationAnalyzerAction, AnalysisState.OperationAnalyzerStateData, IOperation, int>(operationBlockStartActions, operationBlockActions, operationBlockEndActions, analyzer, declaredNode, declaredSymbol, operationBlocks, ([Nullable(new byte[] { 0, 1 })] ImmutableArray<IOperation> blocks) => operations, semanticModel, null, (declarationAnalyzerStateData != null) ? declarationAnalyzerStateData.OperationBlockAnalysisState : null, isGeneratedCode);
					flag = true;
				}
				else
				{
					flag = AnalyzerExecutor.IsDeclarationComplete(declaredSymbol, declarationIndex, analyzer, analysisState);
				}
			}
			finally
			{
				if (declarationAnalyzerStateData != null)
				{
					declarationAnalyzerStateData.ResetToReadyState();
				}
			}
			return flag;
		}

		// Token: 0x060021C8 RID: 8648 RVA: 0x0006AB14 File Offset: 0x00068D14
		private void ExecuteBlockActionsCore<[Nullable(0)] TBlockStartAction, [Nullable(0)] TBlockAction, [Nullable(0)] TNodeAction, [Nullable(0)] TNodeStateData, [Nullable(2)] TNode, [Nullable(0)] TLanguageKindEnum>(IEnumerable<TBlockStartAction> startActions, IEnumerable<TBlockAction> actions, IEnumerable<TBlockAction> endActions, DiagnosticAnalyzer analyzer, SyntaxNode declaredNode, ISymbol declaredSymbol, [Nullable(new byte[] { 0, 1 })] ImmutableArray<TNode> executableBlocks, [Nullable(new byte[] { 1, 0, 1, 1, 1 })] Func<ImmutableArray<TNode>, IEnumerable<TNode>> getNodesToAnalyze, SemanticModel semanticModel, [Nullable(new byte[] { 2, 1, 0 })] Func<SyntaxNode, TLanguageKindEnum> getKind, [Nullable(new byte[] { 2, 1, 1 })] AnalysisState.BlockAnalyzerStateData<TBlockAction, TNodeStateData> analyzerState, bool isGeneratedCode) where TBlockStartAction : AnalyzerAction where TBlockAction : AnalyzerAction where TNodeAction : AnalyzerAction where TNodeStateData : AnalysisState.AnalyzerStateData, new() where TLanguageKindEnum : struct
		{
			if ((isGeneratedCode && this._shouldSkipAnalysisOnGeneratedCode.Invoke(analyzer)) || this.IsAnalyzerSuppressedForTree(analyzer, declaredNode.SyntaxTree))
			{
				return;
			}
			PooledHashSet<TBlockAction> instance = PooledHashSet<TBlockAction>.GetInstance();
			PooledHashSet<TBlockAction> instance2 = PooledHashSet<TBlockAction>.GetInstance();
			ArrayBuilder<TNodeAction> instance3 = ArrayBuilder<TNodeAction>.GetInstance();
			ArrayBuilder<SyntaxNodeAnalyzerAction<TLanguageKindEnum>> arrayBuilder = instance3 as ArrayBuilder<SyntaxNodeAnalyzerAction<TLanguageKindEnum>>;
			ArrayBuilder<OperationAnalyzerAction> arrayBuilder2 = instance3 as ArrayBuilder<OperationAnalyzerAction>;
			ImmutableArray<IOperation> immutableArray = ((executableBlocks[0] is IOperation) ? ((ImmutableArray<IOperation>)executableBlocks) : ImmutableArray<IOperation>.Empty);
			instance2.AddAll<TBlockAction>(actions);
			if (((analyzerState != null) ? analyzerState.CurrentBlockEndActions : null) != null)
			{
				instance.AddAll<TBlockAction>(Enumerable.Cast<TBlockAction>(analyzerState.CurrentBlockEndActions));
				instance3.AddRange(Enumerable.Cast<TNodeAction>(analyzerState.CurrentBlockNodeActions));
			}
			else
			{
				instance.AddAll<TBlockAction>(endActions);
			}
			AnalyzerExecutor.AnalyzerDiagnosticReporter addSemanticDiagnostic = this.GetAddSemanticDiagnostic(semanticModel.SyntaxTree, new TextSpan?(declaredNode.FullSpan), analyzer);
			try
			{
				foreach (TBlockStartAction tblockStartAction in startActions)
				{
					if (AnalyzerExecutor.ShouldExecuteAction(analyzerState, tblockStartAction))
					{
						CodeBlockStartAnalyzerAction<TLanguageKindEnum> codeBlockStartAnalyzerAction = tblockStartAction as CodeBlockStartAnalyzerAction<TLanguageKindEnum>;
						if (codeBlockStartAnalyzerAction != null)
						{
							PooledHashSet<CodeBlockAnalyzerAction> pooledHashSet = instance as PooledHashSet<CodeBlockAnalyzerAction>;
							HostCodeBlockStartAnalysisScope<TLanguageKindEnum> hostCodeBlockStartAnalysisScope = new HostCodeBlockStartAnalysisScope<TLanguageKindEnum>();
							AnalyzerCodeBlockStartAnalysisContext<TLanguageKindEnum> analyzerCodeBlockStartAnalysisContext = new AnalyzerCodeBlockStartAnalysisContext<TLanguageKindEnum>(tblockStartAction.Analyzer, hostCodeBlockStartAnalysisScope, declaredNode, declaredSymbol, semanticModel, this.AnalyzerOptions, this._cancellationToken);
							this.ExecuteAndCatchIfThrows<ValueTuple<Action<CodeBlockStartAnalysisContext<TLanguageKindEnum>>, AnalyzerCodeBlockStartAnalysisContext<TLanguageKindEnum>, HostCodeBlockStartAnalysisScope<TLanguageKindEnum>, PooledHashSet<CodeBlockAnalyzerAction>, ArrayBuilder<SyntaxNodeAnalyzerAction<TLanguageKindEnum>>>>(tblockStartAction.Analyzer, delegate([TupleElementNames(new string[] { "action", "context", "scope", "blockEndActions", "syntaxNodeActions" })] ValueTuple<Action<CodeBlockStartAnalysisContext<TLanguageKindEnum>>, AnalyzerCodeBlockStartAnalysisContext<TLanguageKindEnum>, HostCodeBlockStartAnalysisScope<TLanguageKindEnum>, PooledHashSet<CodeBlockAnalyzerAction>, ArrayBuilder<SyntaxNodeAnalyzerAction<TLanguageKindEnum>>> data)
							{
								data.Item1.Invoke(data.Item2);
								PooledHashSet<CodeBlockAnalyzerAction> item = data.Item4;
								if (item != null)
								{
									item.AddAll<CodeBlockAnalyzerAction>(data.Item3.CodeBlockEndActions);
								}
								ArrayBuilder<SyntaxNodeAnalyzerAction<TLanguageKindEnum>> item2 = data.Item5;
								if (item2 == null)
								{
									return;
								}
								item2.AddRange(data.Item3.SyntaxNodeActions);
							}, new ValueTuple<Action<CodeBlockStartAnalysisContext<TLanguageKindEnum>>, AnalyzerCodeBlockStartAnalysisContext<TLanguageKindEnum>, HostCodeBlockStartAnalysisScope<TLanguageKindEnum>, PooledHashSet<CodeBlockAnalyzerAction>, ArrayBuilder<SyntaxNodeAnalyzerAction<TLanguageKindEnum>>>(codeBlockStartAnalyzerAction.Action, analyzerCodeBlockStartAnalysisContext, hostCodeBlockStartAnalysisScope, pooledHashSet, arrayBuilder), new AnalysisContextInfo?(new AnalysisContextInfo(this.Compilation, declaredSymbol, declaredNode)));
						}
						else
						{
							OperationBlockStartAnalyzerAction operationBlockStartAnalyzerAction = tblockStartAction as OperationBlockStartAnalyzerAction;
							if (operationBlockStartAnalyzerAction != null)
							{
								PooledHashSet<OperationBlockAnalyzerAction> pooledHashSet2 = instance as PooledHashSet<OperationBlockAnalyzerAction>;
								HostOperationBlockStartAnalysisScope hostOperationBlockStartAnalysisScope = new HostOperationBlockStartAnalysisScope();
								AnalyzerOperationBlockStartAnalysisContext analyzerOperationBlockStartAnalysisContext = new AnalyzerOperationBlockStartAnalysisContext(tblockStartAction.Analyzer, hostOperationBlockStartAnalysisScope, immutableArray, declaredSymbol, semanticModel.Compilation, this.AnalyzerOptions, this.GetControlFlowGraph, this._cancellationToken);
								this.ExecuteAndCatchIfThrows<ValueTuple<Action<OperationBlockStartAnalysisContext>, AnalyzerOperationBlockStartAnalysisContext, HostOperationBlockStartAnalysisScope, PooledHashSet<OperationBlockAnalyzerAction>, ArrayBuilder<OperationAnalyzerAction>>>(tblockStartAction.Analyzer, delegate([TupleElementNames(new string[] { "action", "context", "scope", "blockEndActions", "operationActions" })] ValueTuple<Action<OperationBlockStartAnalysisContext>, AnalyzerOperationBlockStartAnalysisContext, HostOperationBlockStartAnalysisScope, PooledHashSet<OperationBlockAnalyzerAction>, ArrayBuilder<OperationAnalyzerAction>> data)
								{
									data.Item1.Invoke(data.Item2);
									PooledHashSet<OperationBlockAnalyzerAction> item3 = data.Item4;
									if (item3 != null)
									{
										item3.AddAll<OperationBlockAnalyzerAction>(data.Item3.OperationBlockEndActions);
									}
									ArrayBuilder<OperationAnalyzerAction> item4 = data.Item5;
									if (item4 == null)
									{
										return;
									}
									item4.AddRange(data.Item3.OperationActions);
								}, new ValueTuple<Action<OperationBlockStartAnalysisContext>, AnalyzerOperationBlockStartAnalysisContext, HostOperationBlockStartAnalysisScope, PooledHashSet<OperationBlockAnalyzerAction>, ArrayBuilder<OperationAnalyzerAction>>(operationBlockStartAnalyzerAction.Action, analyzerOperationBlockStartAnalysisContext, hostOperationBlockStartAnalysisScope, pooledHashSet2, arrayBuilder2), new AnalysisContextInfo?(new AnalysisContextInfo(this.Compilation, declaredSymbol)));
							}
						}
						if (analyzerState != null)
						{
							analyzerState.ProcessedActions.Add(tblockStartAction);
						}
					}
				}
			}
			finally
			{
				if (analyzerState != null)
				{
					analyzerState.CurrentBlockEndActions = instance.ToImmutableHashSet<TBlockAction>();
					analyzerState.CurrentBlockNodeActions = instance3.ToImmutableHashSet<AnalyzerAction>();
				}
			}
			Func<Diagnostic, bool> func;
			using (PooledDelegates.GetPooledFunction<Diagnostic, ValueTuple<AnalyzerExecutor, DiagnosticAnalyzer>, bool>((Diagnostic d, [TupleElementNames(new string[] { "self", "analyzer" })] ValueTuple<AnalyzerExecutor, DiagnosticAnalyzer> arg) => arg.Item1.IsSupportedDiagnostic(arg.Item2, d), new ValueTuple<AnalyzerExecutor, DiagnosticAnalyzer>(this, analyzer), out func))
			{
				if (instance3.Any())
				{
					if (arrayBuilder != null)
					{
						ImmutableSegmentedDictionary<TLanguageKindEnum, ImmutableArray<SyntaxNodeAnalyzerAction<TLanguageKindEnum>>> nodeActionsByKind = AnalyzerExecutor.GetNodeActionsByKind<TLanguageKindEnum>(arrayBuilder);
						IEnumerable<SyntaxNode> enumerable = (IEnumerable<SyntaxNode>)getNodesToAnalyze.Invoke(executableBlocks);
						this.ExecuteSyntaxNodeActions<TLanguageKindEnum>(enumerable, nodeActionsByKind, analyzer, declaredSymbol, semanticModel, getKind, addSemanticDiagnostic.AddDiagnosticAction, func, ((analyzerState != null) ? analyzerState.ExecutableNodesAnalysisState : default(TNodeStateData)) as AnalysisState.SyntaxNodeAnalyzerStateData);
					}
					else if (arrayBuilder2 != null)
					{
						ImmutableSegmentedDictionary<OperationKind, ImmutableArray<OperationAnalyzerAction>> operationActionsByKind = AnalyzerExecutor.GetOperationActionsByKind(arrayBuilder2);
						IEnumerable<IOperation> enumerable2 = (IEnumerable<IOperation>)getNodesToAnalyze.Invoke(executableBlocks);
						this.ExecuteOperationActions(enumerable2, operationActionsByKind, analyzer, declaredSymbol, semanticModel, addSemanticDiagnostic.AddDiagnosticAction, func, ((analyzerState != null) ? analyzerState.ExecutableNodesAnalysisState : default(TNodeStateData)) as AnalysisState.OperationAnalyzerStateData);
					}
				}
				instance3.Free();
				this.ExecuteBlockActions<TBlockAction, TNodeStateData>(instance2, declaredNode, declaredSymbol, analyzer, semanticModel, immutableArray, addSemanticDiagnostic.AddDiagnosticAction, func, analyzerState);
				this.ExecuteBlockActions<TBlockAction, TNodeStateData>(instance, declaredNode, declaredSymbol, analyzer, semanticModel, immutableArray, addSemanticDiagnostic.AddDiagnosticAction, func, analyzerState);
				addSemanticDiagnostic.Free();
			}
		}

		// Token: 0x060021C9 RID: 8649 RVA: 0x0006AF50 File Offset: 0x00069150
		private void ExecuteBlockActions<[Nullable(0)] TBlockAction, [Nullable(0)] TNodeStateData>(PooledHashSet<TBlockAction> blockActions, SyntaxNode declaredNode, ISymbol declaredSymbol, DiagnosticAnalyzer analyzer, SemanticModel semanticModel, [Nullable(new byte[] { 0, 1 })] ImmutableArray<IOperation> operationBlocks, Action<Diagnostic> addDiagnostic, Func<Diagnostic, bool> isSupportedDiagnostic, [Nullable(new byte[] { 2, 1, 1 })] AnalysisState.BlockAnalyzerStateData<TBlockAction, TNodeStateData> analyzerState) where TBlockAction : AnalyzerAction where TNodeStateData : AnalysisState.AnalyzerStateData, new()
		{
			foreach (TBlockAction tblockAction in blockActions)
			{
				if (AnalyzerExecutor.ShouldExecuteAction(analyzerState, tblockAction))
				{
					CodeBlockAnalyzerAction codeBlockAnalyzerAction = tblockAction as CodeBlockAnalyzerAction;
					if (codeBlockAnalyzerAction != null)
					{
						CodeBlockAnalysisContext codeBlockAnalysisContext = new CodeBlockAnalysisContext(declaredNode, declaredSymbol, semanticModel, this.AnalyzerOptions, addDiagnostic, isSupportedDiagnostic, this._cancellationToken);
						this.ExecuteAndCatchIfThrows<ValueTuple<Action<CodeBlockAnalysisContext>, CodeBlockAnalysisContext>>(codeBlockAnalyzerAction.Analyzer, delegate([TupleElementNames(new string[] { "action", "context" })] ValueTuple<Action<CodeBlockAnalysisContext>, CodeBlockAnalysisContext> data)
						{
							data.Item1.Invoke(data.Item2);
						}, new ValueTuple<Action<CodeBlockAnalysisContext>, CodeBlockAnalysisContext>(codeBlockAnalyzerAction.Action, codeBlockAnalysisContext), new AnalysisContextInfo?(new AnalysisContextInfo(this.Compilation, declaredSymbol, declaredNode)));
					}
					else
					{
						OperationBlockAnalyzerAction operationBlockAnalyzerAction = tblockAction as OperationBlockAnalyzerAction;
						if (operationBlockAnalyzerAction != null)
						{
							OperationBlockAnalysisContext operationBlockAnalysisContext = new OperationBlockAnalysisContext(operationBlocks, declaredSymbol, semanticModel.Compilation, this.AnalyzerOptions, addDiagnostic, isSupportedDiagnostic, this.GetControlFlowGraph, this._cancellationToken);
							this.ExecuteAndCatchIfThrows<ValueTuple<Action<OperationBlockAnalysisContext>, OperationBlockAnalysisContext>>(operationBlockAnalyzerAction.Analyzer, delegate([TupleElementNames(new string[] { "action", "context" })] ValueTuple<Action<OperationBlockAnalysisContext>, OperationBlockAnalysisContext> data)
							{
								data.Item1.Invoke(data.Item2);
							}, new ValueTuple<Action<OperationBlockAnalysisContext>, OperationBlockAnalysisContext>(operationBlockAnalyzerAction.Action, operationBlockAnalysisContext), new AnalysisContextInfo?(new AnalysisContextInfo(this.Compilation, declaredSymbol)));
						}
					}
					if (analyzerState != null)
					{
						analyzerState.ProcessedActions.Add(tblockAction);
					}
				}
			}
			blockActions.Free();
		}

		// Token: 0x060021CA RID: 8650 RVA: 0x0006B0D4 File Offset: 0x000692D4
		[NullableContext(0)]
		[return: Nullable(new byte[] { 0, 0, 0, 1, 0 })]
		internal static ImmutableSegmentedDictionary<TLanguageKindEnum, ImmutableArray<SyntaxNodeAnalyzerAction<TLanguageKindEnum>>> GetNodeActionsByKind<TLanguageKindEnum>([Nullable(new byte[] { 1, 1, 0 })] IEnumerable<SyntaxNodeAnalyzerAction<TLanguageKindEnum>> nodeActions) where TLanguageKindEnum : struct
		{
			PooledDictionary<TLanguageKindEnum, ArrayBuilder<SyntaxNodeAnalyzerAction<TLanguageKindEnum>>> instance = PooledDictionary<TLanguageKindEnum, ArrayBuilder<SyntaxNodeAnalyzerAction<TLanguageKindEnum>>>.GetInstance();
			foreach (SyntaxNodeAnalyzerAction<TLanguageKindEnum> syntaxNodeAnalyzerAction in nodeActions)
			{
				foreach (TLanguageKindEnum tlanguageKindEnum in syntaxNodeAnalyzerAction.Kinds)
				{
					ArrayBuilder<SyntaxNodeAnalyzerAction<TLanguageKindEnum>> instance2;
					if (!instance.TryGetValue(tlanguageKindEnum, ref instance2))
					{
						instance.Add(tlanguageKindEnum, instance2 = ArrayBuilder<SyntaxNodeAnalyzerAction<TLanguageKindEnum>>.GetInstance());
					}
					instance2.Add(syntaxNodeAnalyzerAction);
				}
			}
			ImmutableSegmentedDictionary<TLanguageKindEnum, ImmutableArray<SyntaxNodeAnalyzerAction<TLanguageKindEnum>>> immutableSegmentedDictionary = ImmutableSegmentedDictionary.CreateRange<TLanguageKindEnum, ImmutableArray<SyntaxNodeAnalyzerAction<TLanguageKindEnum>>>(Enumerable.Select<KeyValuePair<TLanguageKindEnum, ArrayBuilder<SyntaxNodeAnalyzerAction<TLanguageKindEnum>>>, KeyValuePair<TLanguageKindEnum, ImmutableArray<SyntaxNodeAnalyzerAction<TLanguageKindEnum>>>>(instance, (KeyValuePair<TLanguageKindEnum, ArrayBuilder<SyntaxNodeAnalyzerAction<TLanguageKindEnum>>> kvp) => KeyValuePairUtil.Create<TLanguageKindEnum, ImmutableArray<SyntaxNodeAnalyzerAction<TLanguageKindEnum>>>(kvp.Key, kvp.Value.ToImmutableAndFree())));
			instance.Free();
			return immutableSegmentedDictionary;
		}

		// Token: 0x060021CB RID: 8651 RVA: 0x0006B194 File Offset: 0x00069394
		public bool TryExecuteSyntaxNodeActions<[Nullable(0)] TLanguageKindEnum>(IEnumerable<SyntaxNode> nodesToAnalyze, [Nullable(new byte[] { 0, 0, 0, 1, 0 })] ImmutableSegmentedDictionary<TLanguageKindEnum, ImmutableArray<SyntaxNodeAnalyzerAction<TLanguageKindEnum>>> nodeActionsByKind, DiagnosticAnalyzer analyzer, SemanticModel model, [Nullable(new byte[] { 1, 1, 0 })] Func<SyntaxNode, TLanguageKindEnum> getKind, TextSpan filterSpan, int declarationIndex, ISymbol declaredSymbol, AnalysisScope analysisScope, [Nullable(2)] AnalysisState analysisState, bool isGeneratedCode) where TLanguageKindEnum : struct
		{
			AnalysisState.DeclarationAnalyzerStateData declarationAnalyzerStateData = null;
			bool flag;
			try
			{
				if (AnalyzerExecutor.TryStartAnalyzingDeclaration(declaredSymbol, declarationIndex, analyzer, analysisScope, analysisState, out declarationAnalyzerStateData))
				{
					this.ExecuteSyntaxNodeActionsCore<TLanguageKindEnum>(nodesToAnalyze, nodeActionsByKind, analyzer, declaredSymbol, model, getKind, filterSpan, declarationAnalyzerStateData, isGeneratedCode);
					flag = true;
				}
				else
				{
					flag = AnalyzerExecutor.IsDeclarationComplete(declaredSymbol, declarationIndex, analyzer, analysisState);
				}
			}
			finally
			{
				if (declarationAnalyzerStateData != null)
				{
					declarationAnalyzerStateData.ResetToReadyState();
				}
			}
			return flag;
		}

		// Token: 0x060021CC RID: 8652 RVA: 0x0006B1F8 File Offset: 0x000693F8
		private void ExecuteSyntaxNodeActionsCore<[Nullable(0)] TLanguageKindEnum>(IEnumerable<SyntaxNode> nodesToAnalyze, [Nullable(new byte[] { 0, 0, 0, 1, 0 })] ImmutableSegmentedDictionary<TLanguageKindEnum, ImmutableArray<SyntaxNodeAnalyzerAction<TLanguageKindEnum>>> nodeActionsByKind, DiagnosticAnalyzer analyzer, ISymbol containingSymbol, SemanticModel model, [Nullable(new byte[] { 1, 1, 0 })] Func<SyntaxNode, TLanguageKindEnum> getKind, TextSpan filterSpan, [Nullable(2)] AnalysisState.SyntaxNodeAnalyzerStateData analyzerState, bool isGeneratedCode) where TLanguageKindEnum : struct
		{
			if ((isGeneratedCode && this._shouldSkipAnalysisOnGeneratedCode.Invoke(analyzer)) || this.IsAnalyzerSuppressedForTree(analyzer, model.SyntaxTree))
			{
				return;
			}
			AnalyzerExecutor.AnalyzerDiagnosticReporter addSemanticDiagnostic = this.GetAddSemanticDiagnostic(model.SyntaxTree, new TextSpan?(filterSpan), analyzer);
			Func<Diagnostic, bool> func;
			using (PooledDelegates.GetPooledFunction<Diagnostic, ValueTuple<AnalyzerExecutor, DiagnosticAnalyzer>, bool>((Diagnostic d, [TupleElementNames(new string[] { "self", "analyzer" })] ValueTuple<AnalyzerExecutor, DiagnosticAnalyzer> arg) => arg.Item1.IsSupportedDiagnostic(arg.Item2, d), new ValueTuple<AnalyzerExecutor, DiagnosticAnalyzer>(this, analyzer), out func))
			{
				this.ExecuteSyntaxNodeActions<TLanguageKindEnum>(nodesToAnalyze, nodeActionsByKind, analyzer, containingSymbol, model, getKind, addSemanticDiagnostic.AddDiagnosticAction, func, analyzerState);
				addSemanticDiagnostic.Free();
			}
		}

		// Token: 0x060021CD RID: 8653 RVA: 0x0006B2AC File Offset: 0x000694AC
		private void ExecuteSyntaxNodeActions<[Nullable(0)] TLanguageKindEnum>(IEnumerable<SyntaxNode> nodesToAnalyze, [Nullable(new byte[] { 0, 0, 0, 1, 0 })] ImmutableSegmentedDictionary<TLanguageKindEnum, ImmutableArray<SyntaxNodeAnalyzerAction<TLanguageKindEnum>>> nodeActionsByKind, DiagnosticAnalyzer analyzer, ISymbol containingSymbol, SemanticModel model, [Nullable(new byte[] { 1, 1, 0 })] Func<SyntaxNode, TLanguageKindEnum> getKind, Action<Diagnostic> addDiagnostic, Func<Diagnostic, bool> isSupportedDiagnostic, [Nullable(2)] AnalysisState.SyntaxNodeAnalyzerStateData analyzerState) where TLanguageKindEnum : struct
		{
			SyntaxNode syntaxNode = ((analyzerState != null) ? analyzerState.CurrentNode : null);
			if (syntaxNode != null)
			{
				this.ExecuteSyntaxNodeActions<TLanguageKindEnum>(syntaxNode, nodeActionsByKind, containingSymbol, model, getKind, addDiagnostic, isSupportedDiagnostic, analyzerState);
			}
			foreach (SyntaxNode syntaxNode2 in nodesToAnalyze)
			{
				if (this.ShouldExecuteNode(analyzerState, syntaxNode2, analyzer))
				{
					AnalyzerExecutor.SetCurrentNode(analyzerState, syntaxNode2);
					this.ExecuteSyntaxNodeActions<TLanguageKindEnum>(syntaxNode2, nodeActionsByKind, containingSymbol, model, getKind, addDiagnostic, isSupportedDiagnostic, analyzerState);
				}
			}
		}

		// Token: 0x060021CE RID: 8654 RVA: 0x0006B33C File Offset: 0x0006953C
		private void ExecuteSyntaxNodeActions<[Nullable(0)] TLanguageKindEnum>(SyntaxNode node, [Nullable(new byte[] { 0, 0, 0, 1, 0 })] ImmutableSegmentedDictionary<TLanguageKindEnum, ImmutableArray<SyntaxNodeAnalyzerAction<TLanguageKindEnum>>> nodeActionsByKind, ISymbol containingSymbol, SemanticModel model, [Nullable(new byte[] { 1, 1, 0 })] Func<SyntaxNode, TLanguageKindEnum> getKind, Action<Diagnostic> addDiagnostic, Func<Diagnostic, bool> isSupportedDiagnostic, [Nullable(2)] AnalysisState.SyntaxNodeAnalyzerStateData analyzerState) where TLanguageKindEnum : struct
		{
			ImmutableArray<SyntaxNodeAnalyzerAction<TLanguageKindEnum>> immutableArray;
			if (nodeActionsByKind.TryGetValue(getKind.Invoke(node), out immutableArray))
			{
				foreach (SyntaxNodeAnalyzerAction<TLanguageKindEnum> syntaxNodeAnalyzerAction in immutableArray)
				{
					this.ExecuteSyntaxNodeAction<TLanguageKindEnum>(syntaxNodeAnalyzerAction, node, containingSymbol, model, addDiagnostic, isSupportedDiagnostic, analyzerState);
				}
			}
			if (analyzerState != null)
			{
				analyzerState.ClearNodeAnalysisState();
			}
		}

		// Token: 0x060021CF RID: 8655 RVA: 0x0006B394 File Offset: 0x00069594
		[return: Nullable(new byte[] { 0, 0, 1 })]
		internal static ImmutableSegmentedDictionary<OperationKind, ImmutableArray<OperationAnalyzerAction>> GetOperationActionsByKind(IEnumerable<OperationAnalyzerAction> operationActions)
		{
			PooledDictionary<OperationKind, ArrayBuilder<OperationAnalyzerAction>> instance = PooledDictionary<OperationKind, ArrayBuilder<OperationAnalyzerAction>>.GetInstance();
			foreach (OperationAnalyzerAction operationAnalyzerAction in operationActions)
			{
				foreach (OperationKind operationKind in operationAnalyzerAction.Kinds)
				{
					ArrayBuilder<OperationAnalyzerAction> instance2;
					if (!instance.TryGetValue(operationKind, ref instance2))
					{
						instance.Add(operationKind, instance2 = ArrayBuilder<OperationAnalyzerAction>.GetInstance());
					}
					instance2.Add(operationAnalyzerAction);
				}
			}
			ImmutableSegmentedDictionary<OperationKind, ImmutableArray<OperationAnalyzerAction>> immutableSegmentedDictionary = ImmutableSegmentedDictionary.CreateRange<OperationKind, ImmutableArray<OperationAnalyzerAction>>(Enumerable.Select<KeyValuePair<OperationKind, ArrayBuilder<OperationAnalyzerAction>>, KeyValuePair<OperationKind, ImmutableArray<OperationAnalyzerAction>>>(instance, (KeyValuePair<OperationKind, ArrayBuilder<OperationAnalyzerAction>> kvp) => KeyValuePairUtil.Create<OperationKind, ImmutableArray<OperationAnalyzerAction>>(kvp.Key, kvp.Value.ToImmutableAndFree())));
			instance.Free();
			return immutableSegmentedDictionary;
		}

		// Token: 0x060021D0 RID: 8656 RVA: 0x0006B454 File Offset: 0x00069654
		public bool TryExecuteOperationActions(IEnumerable<IOperation> operationsToAnalyze, [Nullable(new byte[] { 0, 0, 1 })] ImmutableSegmentedDictionary<OperationKind, ImmutableArray<OperationAnalyzerAction>> operationActionsByKind, DiagnosticAnalyzer analyzer, SemanticModel model, TextSpan filterSpan, int declarationIndex, ISymbol declaredSymbol, AnalysisScope analysisScope, [Nullable(2)] AnalysisState analysisState, bool isGeneratedCode)
		{
			AnalysisState.DeclarationAnalyzerStateData declarationAnalyzerStateData = null;
			bool flag;
			try
			{
				if (AnalyzerExecutor.TryStartAnalyzingDeclaration(declaredSymbol, declarationIndex, analyzer, analysisScope, analysisState, out declarationAnalyzerStateData))
				{
					this.ExecuteOperationActionsCore(operationsToAnalyze, operationActionsByKind, analyzer, declaredSymbol, model, filterSpan, (declarationAnalyzerStateData != null) ? declarationAnalyzerStateData.OperationBlockAnalysisState.ExecutableNodesAnalysisState : null, isGeneratedCode);
					flag = true;
				}
				else
				{
					flag = AnalyzerExecutor.IsDeclarationComplete(declaredSymbol, declarationIndex, analyzer, analysisState);
				}
			}
			finally
			{
				if (declarationAnalyzerStateData != null)
				{
					declarationAnalyzerStateData.ResetToReadyState();
				}
			}
			return flag;
		}

		// Token: 0x060021D1 RID: 8657 RVA: 0x0006B4C8 File Offset: 0x000696C8
		private void ExecuteOperationActionsCore(IEnumerable<IOperation> operationsToAnalyze, [Nullable(new byte[] { 0, 0, 1 })] ImmutableSegmentedDictionary<OperationKind, ImmutableArray<OperationAnalyzerAction>> operationActionsByKind, DiagnosticAnalyzer analyzer, ISymbol containingSymbol, SemanticModel model, TextSpan filterSpan, [Nullable(2)] AnalysisState.OperationAnalyzerStateData analyzerState, bool isGeneratedCode)
		{
			if ((isGeneratedCode && this._shouldSkipAnalysisOnGeneratedCode.Invoke(analyzer)) || this.IsAnalyzerSuppressedForTree(analyzer, model.SyntaxTree))
			{
				return;
			}
			AnalyzerExecutor.AnalyzerDiagnosticReporter addSemanticDiagnostic = this.GetAddSemanticDiagnostic(model.SyntaxTree, new TextSpan?(filterSpan), analyzer);
			Func<Diagnostic, bool> func;
			using (PooledDelegates.GetPooledFunction<Diagnostic, ValueTuple<AnalyzerExecutor, DiagnosticAnalyzer>, bool>((Diagnostic d, [TupleElementNames(new string[] { "self", "analyzer" })] ValueTuple<AnalyzerExecutor, DiagnosticAnalyzer> arg) => arg.Item1.IsSupportedDiagnostic(arg.Item2, d), new ValueTuple<AnalyzerExecutor, DiagnosticAnalyzer>(this, analyzer), out func))
			{
				this.ExecuteOperationActions(operationsToAnalyze, operationActionsByKind, analyzer, containingSymbol, model, addSemanticDiagnostic.AddDiagnosticAction, func, analyzerState);
				addSemanticDiagnostic.Free();
			}
		}

		// Token: 0x060021D2 RID: 8658 RVA: 0x0006B578 File Offset: 0x00069778
		private void ExecuteOperationActions(IEnumerable<IOperation> operationsToAnalyze, [Nullable(new byte[] { 0, 0, 1 })] ImmutableSegmentedDictionary<OperationKind, ImmutableArray<OperationAnalyzerAction>> operationActionsByKind, DiagnosticAnalyzer analyzer, ISymbol containingSymbol, SemanticModel model, Action<Diagnostic> addDiagnostic, Func<Diagnostic, bool> isSupportedDiagnostic, [Nullable(2)] AnalysisState.OperationAnalyzerStateData analyzerState)
		{
			IOperation operation = ((analyzerState != null) ? analyzerState.CurrentOperation : null);
			if (operation != null)
			{
				this.ExecuteOperationActions(operation, operationActionsByKind, containingSymbol, model, addDiagnostic, isSupportedDiagnostic, analyzerState);
			}
			foreach (IOperation operation2 in operationsToAnalyze)
			{
				if (this.ShouldExecuteOperation(analyzerState, operation2, analyzer))
				{
					AnalyzerExecutor.SetCurrentOperation(analyzerState, operation2);
					this.ExecuteOperationActions(operation2, operationActionsByKind, containingSymbol, model, addDiagnostic, isSupportedDiagnostic, analyzerState);
				}
			}
		}

		// Token: 0x060021D3 RID: 8659 RVA: 0x0006B604 File Offset: 0x00069804
		private void ExecuteOperationActions(IOperation operation, [Nullable(new byte[] { 0, 0, 1 })] ImmutableSegmentedDictionary<OperationKind, ImmutableArray<OperationAnalyzerAction>> operationActionsByKind, ISymbol containingSymbol, SemanticModel model, Action<Diagnostic> addDiagnostic, Func<Diagnostic, bool> isSupportedDiagnostic, [Nullable(2)] AnalysisState.OperationAnalyzerStateData analyzerState)
		{
			ImmutableArray<OperationAnalyzerAction> immutableArray;
			if (operationActionsByKind.TryGetValue(operation.Kind, out immutableArray))
			{
				foreach (OperationAnalyzerAction operationAnalyzerAction in immutableArray)
				{
					this.ExecuteOperationAction(operationAnalyzerAction, operation, containingSymbol, model, addDiagnostic, isSupportedDiagnostic, analyzerState);
				}
			}
			if (analyzerState != null)
			{
				analyzerState.ClearNodeAnalysisState();
			}
		}

		// Token: 0x060021D4 RID: 8660 RVA: 0x0006B65C File Offset: 0x0006985C
		internal static bool CanHaveExecutableCodeBlock(ISymbol symbol)
		{
			switch (symbol.Kind)
			{
			case SymbolKind.Event:
			case SymbolKind.Method:
			case SymbolKind.NamedType:
			case SymbolKind.Namespace:
			case SymbolKind.Property:
				return true;
			case SymbolKind.Field:
				return true;
			}
			return false;
		}

		// Token: 0x060021D5 RID: 8661 RVA: 0x0006B6AC File Offset: 0x000698AC
		internal void ExecuteAndCatchIfThrows<[Nullable(2)] TArg>(DiagnosticAnalyzer analyzer, Action<TArg> analyze, TArg argument, AnalysisContextInfo? info = null)
		{
			SharedStopwatch sharedStopwatch = default(SharedStopwatch);
			if (this._analyzerExecutionTimeMap != null)
			{
				SharedStopwatch.StartNew();
				sharedStopwatch = SharedStopwatch.StartNew();
			}
			Func<DiagnosticAnalyzer, object> getAnalyzerGate = this._getAnalyzerGate;
			object obj = ((getAnalyzerGate != null) ? getAnalyzerGate.Invoke(analyzer) : null);
			if (obj != null)
			{
				object obj2 = obj;
				lock (obj2)
				{
					this.ExecuteAndCatchIfThrows_NoLock<TArg>(analyzer, analyze, argument, info);
					goto IL_0061;
				}
			}
			this.ExecuteAndCatchIfThrows_NoLock<TArg>(analyzer, analyze, argument, info);
			IL_0061:
			if (this._analyzerExecutionTimeMap != null)
			{
				long ticks = sharedStopwatch.Elapsed.Ticks;
				Interlocked.Add(ref this._analyzerExecutionTimeMap.GetOrAdd(analyzer, (DiagnosticAnalyzer _) => new StrongBox<long>(0L)).Value, ticks);
			}
		}

		// Token: 0x060021D6 RID: 8662 RVA: 0x0006B77C File Offset: 0x0006997C
		private void ExecuteAndCatchIfThrows_NoLock<[Nullable(2)] TArg>(DiagnosticAnalyzer analyzer, Action<TArg> analyze, TArg argument, AnalysisContextInfo? info)
		{
			try
			{
				this._cancellationToken.ThrowIfCancellationRequested();
				analyze.Invoke(argument);
			}
			catch (Exception ex) when (this.ExceptionFilter(ex))
			{
				Diagnostic diagnostic = AnalyzerExecutor.CreateAnalyzerExceptionDiagnostic(analyzer, ex, info);
				try
				{
					this._onAnalyzerException.Invoke(ex, analyzer, diagnostic);
				}
				catch (Exception)
				{
				}
			}
		}

		// Token: 0x060021D7 RID: 8663 RVA: 0x0006B7F8 File Offset: 0x000699F8
		internal bool ExceptionFilter(Exception ex)
		{
			OperationCanceledException ex2 = ex as OperationCanceledException;
			CancellationToken? cancellationToken = ((ex2 != null) ? new CancellationToken?(ex2.CancellationToken) : default(CancellationToken?));
			CancellationToken cancellationToken2 = this._cancellationToken;
			return (cancellationToken == null || (cancellationToken != null && !(cancellationToken.GetValueOrDefault() == cancellationToken2))) && (this._analyzerExceptionFilter == null || this._analyzerExceptionFilter.Invoke(ex));
		}

		// Token: 0x060021D8 RID: 8664 RVA: 0x0006B86C File Offset: 0x00069A6C
		internal static Diagnostic CreateAnalyzerExceptionDiagnostic(DiagnosticAnalyzer analyzer, Exception e, AnalysisContextInfo? info = null)
		{
			string text = analyzer.ToString();
			string compilerAnalyzerFailure = CodeAnalysisResources.CompilerAnalyzerFailure;
			string compilerAnalyzerThrows = CodeAnalysisResources.CompilerAnalyzerThrows;
			string text2 = string.Join(Environment.NewLine, new string[]
			{
				AnalyzerExecutor.CreateDiagnosticDescription(info, e),
				AnalyzerExecutor.CreateDisablingMessage(analyzer)
			}).Trim();
			string[] array = new string[]
			{
				text,
				e.GetType().ToString(),
				e.Message,
				text2
			};
			string text3 = string.Format(CodeAnalysisResources.CompilerAnalyzerThrowsDescription, text, AnalyzerExecutor.CreateDiagnosticDescription(info, e));
			DiagnosticDescriptor analyzerExceptionDiagnosticDescriptor = AnalyzerExecutor.GetAnalyzerExceptionDiagnosticDescriptor("AD0001", compilerAnalyzerFailure, text3, compilerAnalyzerThrows);
			Location none = Location.None;
			object[] array2 = array;
			return Diagnostic.Create(analyzerExceptionDiagnosticDescriptor, none, array2);
		}

		// Token: 0x060021D9 RID: 8665 RVA: 0x0006B910 File Offset: 0x00069B10
		private static string CreateDiagnosticDescription(AnalysisContextInfo? info, Exception e)
		{
			if (info == null)
			{
				return e.CreateDiagnosticDescription();
			}
			return string.Join(Environment.NewLine, new string[]
			{
				string.Format(CodeAnalysisResources.ExceptionContext, (info != null) ? info.GetValueOrDefault().GetContext() : null),
				e.CreateDiagnosticDescription()
			});
		}

		// Token: 0x060021DA RID: 8666 RVA: 0x0006B970 File Offset: 0x00069B70
		private static string CreateDisablingMessage(DiagnosticAnalyzer analyzer)
		{
			ImmutableSortedSet<string> immutableSortedSet = ImmutableSortedSet<string>.Empty.WithComparer(StringComparer.OrdinalIgnoreCase);
			try
			{
				foreach (DiagnosticDescriptor diagnosticDescriptor in analyzer.SupportedDiagnostics)
				{
					immutableSortedSet = immutableSortedSet.Add(diagnosticDescriptor.Id);
				}
			}
			catch (Exception ex) when (FatalError.ReportAndCatch(ex))
			{
			}
			if (immutableSortedSet.IsEmpty)
			{
				return "";
			}
			return string.Format(CodeAnalysisResources.DisableAnalyzerDiagnosticsMessage, string.Join(", ", immutableSortedSet));
		}

		// Token: 0x060021DB RID: 8667 RVA: 0x0006BA0C File Offset: 0x00069C0C
		internal static Diagnostic CreateDriverExceptionDiagnostic(Exception e)
		{
			string analyzerDriverFailure = CodeAnalysisResources.AnalyzerDriverFailure;
			string analyzerDriverThrows = CodeAnalysisResources.AnalyzerDriverThrows;
			string[] array = new string[]
			{
				e.GetType().ToString(),
				e.Message
			};
			string text = string.Format(CodeAnalysisResources.AnalyzerDriverThrowsDescription, e.CreateDiagnosticDescription());
			DiagnosticDescriptor analyzerExceptionDiagnosticDescriptor = AnalyzerExecutor.GetAnalyzerExceptionDiagnosticDescriptor("AD0002", analyzerDriverFailure, text, analyzerDriverThrows);
			Location none = Location.None;
			object[] array2 = array;
			return Diagnostic.Create(analyzerExceptionDiagnosticDescriptor, none, array2);
		}

		// Token: 0x060021DC RID: 8668 RVA: 0x0006BA70 File Offset: 0x00069C70
		[NullableContext(2)]
		[return: Nullable(1)]
		internal static DiagnosticDescriptor GetAnalyzerExceptionDiagnosticDescriptor(string id = null, string title = null, string description = null, string messageFormat = null)
		{
			if (id == null)
			{
				id = "AD0001";
			}
			if (title == null)
			{
				title = CodeAnalysisResources.CompilerAnalyzerFailure;
			}
			if (messageFormat == null)
			{
				messageFormat = CodeAnalysisResources.CompilerAnalyzerThrows;
			}
			if (description == null)
			{
				description = CodeAnalysisResources.CompilerAnalyzerFailure;
			}
			return new DiagnosticDescriptor(id, title, messageFormat, "Compiler", DiagnosticSeverity.Warning, true, description, null, new string[] { "AnalyzerException" });
		}

		// Token: 0x060021DD RID: 8669 RVA: 0x0006BAC4 File Offset: 0x00069CC4
		internal static bool IsAnalyzerExceptionDiagnostic(Diagnostic diagnostic)
		{
			if (diagnostic.Id == "AD0001" || diagnostic.Id == "AD0002")
			{
				using (IEnumerator<string> enumerator = diagnostic.Descriptor.CustomTags.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current == "AnalyzerException")
						{
							return true;
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x060021DE RID: 8670 RVA: 0x0006BB48 File Offset: 0x00069D48
		internal static bool AreEquivalentAnalyzerExceptionDiagnostics(Diagnostic exceptionDiagnostic, Diagnostic other)
		{
			return AnalyzerExecutor.IsAnalyzerExceptionDiagnostic(other) && (exceptionDiagnostic.Id == other.Id && exceptionDiagnostic.Severity == other.Severity) && exceptionDiagnostic.GetMessage(null) == other.GetMessage(null);
		}

		// Token: 0x060021DF RID: 8671 RVA: 0x0006BB95 File Offset: 0x00069D95
		private bool IsSupportedDiagnostic(DiagnosticAnalyzer analyzer, Diagnostic diagnostic)
		{
			return diagnostic is DiagnosticWithInfo || this._analyzerManager.IsSupportedDiagnostic(analyzer, diagnostic, this._isCompilerAnalyzer, this);
		}

		// Token: 0x060021E0 RID: 8672 RVA: 0x0006BBB8 File Offset: 0x00069DB8
		private Action<Diagnostic> GetAddDiagnostic(ISymbol contextSymbol, [Nullable(new byte[] { 0, 1 })] ImmutableArray<SyntaxReference> cachedDeclaringReferences, DiagnosticAnalyzer analyzer, Func<ISymbol, SyntaxReference, Compilation, CancellationToken, SyntaxNode> getTopMostNodeForAnalysis)
		{
			return AnalyzerExecutor.GetAddDiagnostic(contextSymbol, cachedDeclaringReferences, this.Compilation, analyzer, this._addNonCategorizedDiagnostic, this._addCategorizedLocalDiagnostic, this._addCategorizedNonLocalDiagnostic, getTopMostNodeForAnalysis, this._shouldSuppressGeneratedCodeDiagnostic, this._cancellationToken);
		}

		// Token: 0x060021E1 RID: 8673 RVA: 0x0006BBF4 File Offset: 0x00069DF4
		private static Action<Diagnostic> GetAddDiagnostic(ISymbol contextSymbol, [Nullable(new byte[] { 0, 1 })] ImmutableArray<SyntaxReference> cachedDeclaringReferences, Compilation compilation, DiagnosticAnalyzer analyzer, [Nullable(new byte[] { 2, 1 })] Action<Diagnostic> addNonCategorizedDiagnostic, [Nullable(new byte[] { 2, 1, 1 })] Action<Diagnostic, DiagnosticAnalyzer, bool> addCategorizedLocalDiagnostic, [Nullable(new byte[] { 2, 1, 1 })] Action<Diagnostic, DiagnosticAnalyzer> addCategorizedNonLocalDiagnostic, Func<ISymbol, SyntaxReference, Compilation, CancellationToken, SyntaxNode> getTopMostNodeForAnalysis, Func<Diagnostic, DiagnosticAnalyzer, Compilation, CancellationToken, bool> shouldSuppressGeneratedCodeDiagnostic, CancellationToken cancellationToken)
		{
			return delegate(Diagnostic diagnostic)
			{
				if (shouldSuppressGeneratedCodeDiagnostic.Invoke(diagnostic, analyzer, compilation, cancellationToken))
				{
					return;
				}
				if (addCategorizedLocalDiagnostic == null)
				{
					addNonCategorizedDiagnostic.Invoke(diagnostic);
					return;
				}
				if (diagnostic.Location.IsInSource)
				{
					foreach (SyntaxReference syntaxReference in cachedDeclaringReferences)
					{
						if (syntaxReference.SyntaxTree == diagnostic.Location.SourceTree)
						{
							SyntaxNode syntaxNode = getTopMostNodeForAnalysis.Invoke(contextSymbol, syntaxReference, compilation, cancellationToken);
							if (diagnostic.Location.SourceSpan.IntersectsWith(syntaxNode.FullSpan))
							{
								addCategorizedLocalDiagnostic.Invoke(diagnostic, analyzer, false);
								return;
							}
						}
					}
				}
				addCategorizedNonLocalDiagnostic.Invoke(diagnostic, analyzer);
			};
		}

		// Token: 0x060021E2 RID: 8674 RVA: 0x0006BC5D File Offset: 0x00069E5D
		private Action<Diagnostic> GetAddCompilationDiagnostic(DiagnosticAnalyzer analyzer)
		{
			return delegate(Diagnostic diagnostic)
			{
				if (this._shouldSuppressGeneratedCodeDiagnostic.Invoke(diagnostic, analyzer, this.Compilation, this._cancellationToken))
				{
					return;
				}
				if (this._addCategorizedNonLocalDiagnostic == null)
				{
					this._addNonCategorizedDiagnostic.Invoke(diagnostic);
					return;
				}
				this._addCategorizedNonLocalDiagnostic.Invoke(diagnostic, analyzer);
			};
		}

		// Token: 0x060021E3 RID: 8675 RVA: 0x0006BC80 File Offset: 0x00069E80
		private AnalyzerExecutor.AnalyzerDiagnosticReporter GetAddSemanticDiagnostic(SyntaxTree tree, DiagnosticAnalyzer analyzer)
		{
			return AnalyzerExecutor.AnalyzerDiagnosticReporter.GetInstance(new SourceOrAdditionalFile(tree), default(TextSpan?), this.Compilation, analyzer, false, this._addNonCategorizedDiagnostic, this._addCategorizedLocalDiagnostic, this._addCategorizedNonLocalDiagnostic, this._shouldSuppressGeneratedCodeDiagnostic, this._cancellationToken);
		}

		// Token: 0x060021E4 RID: 8676 RVA: 0x0006BCC8 File Offset: 0x00069EC8
		private AnalyzerExecutor.AnalyzerDiagnosticReporter GetAddSemanticDiagnostic(SyntaxTree tree, TextSpan? span, DiagnosticAnalyzer analyzer)
		{
			return AnalyzerExecutor.AnalyzerDiagnosticReporter.GetInstance(new SourceOrAdditionalFile(tree), span, this.Compilation, analyzer, false, this._addNonCategorizedDiagnostic, this._addCategorizedLocalDiagnostic, this._addCategorizedNonLocalDiagnostic, this._shouldSuppressGeneratedCodeDiagnostic, this._cancellationToken);
		}

		// Token: 0x060021E5 RID: 8677 RVA: 0x0006BD08 File Offset: 0x00069F08
		private AnalyzerExecutor.AnalyzerDiagnosticReporter GetAddSyntaxDiagnostic(SourceOrAdditionalFile file, DiagnosticAnalyzer analyzer)
		{
			return AnalyzerExecutor.AnalyzerDiagnosticReporter.GetInstance(file, default(TextSpan?), this.Compilation, analyzer, true, this._addNonCategorizedDiagnostic, this._addCategorizedLocalDiagnostic, this._addCategorizedNonLocalDiagnostic, this._shouldSuppressGeneratedCodeDiagnostic, this._cancellationToken);
		}

		// Token: 0x060021E6 RID: 8678 RVA: 0x0006BD4A File Offset: 0x00069F4A
		private static bool ShouldExecuteAction([Nullable(2)] AnalysisState.AnalyzerStateData analyzerState, AnalyzerAction action)
		{
			return analyzerState == null || !analyzerState.ProcessedActions.Contains(action);
		}

		// Token: 0x060021E7 RID: 8679 RVA: 0x0006BD60 File Offset: 0x00069F60
		private bool ShouldExecuteNode([Nullable(2)] AnalysisState.SyntaxNodeAnalyzerStateData analyzerState, SyntaxNode node, DiagnosticAnalyzer analyzer)
		{
			return (analyzerState == null || !analyzerState.ProcessedNodes.Contains(node)) && (!this._shouldSkipAnalysisOnGeneratedCode.Invoke(analyzer) || !this._isGeneratedCodeLocation.Invoke(node.SyntaxTree, node.Span));
		}

		// Token: 0x060021E8 RID: 8680 RVA: 0x0006BDA0 File Offset: 0x00069FA0
		private bool ShouldExecuteOperation([Nullable(2)] AnalysisState.OperationAnalyzerStateData analyzerState, IOperation operation, DiagnosticAnalyzer analyzer)
		{
			return (analyzerState == null || !analyzerState.ProcessedOperations.Contains(operation)) && (operation.Syntax == null || !this._shouldSkipAnalysisOnGeneratedCode.Invoke(analyzer) || !this._isGeneratedCodeLocation.Invoke(operation.Syntax.SyntaxTree, operation.Syntax.Span));
		}

		// Token: 0x060021E9 RID: 8681 RVA: 0x0006BDFC File Offset: 0x00069FFC
		private static void SetCurrentNode([Nullable(2)] AnalysisState.SyntaxNodeAnalyzerStateData analyzerState, SyntaxNode node)
		{
			if (analyzerState != null)
			{
				analyzerState.CurrentNode = node;
			}
		}

		// Token: 0x060021EA RID: 8682 RVA: 0x0006BE08 File Offset: 0x0006A008
		private static void SetCurrentOperation([Nullable(2)] AnalysisState.OperationAnalyzerStateData analyzerState, IOperation operation)
		{
			if (analyzerState != null)
			{
				analyzerState.CurrentOperation = operation;
			}
		}

		// Token: 0x060021EB RID: 8683 RVA: 0x0006BE14 File Offset: 0x0006A014
		private static bool TryStartProcessingEvent(CompilationEvent nonSymbolCompilationEvent, DiagnosticAnalyzer analyzer, AnalysisScope analysisScope, [Nullable(2)] AnalysisState analysisState, [Nullable(2)] out AnalysisState.AnalyzerStateData analyzerState)
		{
			analyzerState = null;
			return analysisState == null || analysisState.TryStartProcessingEvent(nonSymbolCompilationEvent, analyzer, out analyzerState);
		}

		// Token: 0x060021EC RID: 8684 RVA: 0x0006BE29 File Offset: 0x0006A029
		private static bool TryStartSyntaxAnalysis(SourceOrAdditionalFile file, DiagnosticAnalyzer analyzer, AnalysisScope analysisScope, [Nullable(2)] AnalysisState analysisState, [Nullable(2)] out AnalysisState.AnalyzerStateData analyzerState)
		{
			analyzerState = null;
			return analysisState == null || analysisState.TryStartSyntaxAnalysis(file, analyzer, out analyzerState);
		}

		// Token: 0x060021ED RID: 8685 RVA: 0x0006BE3E File Offset: 0x0006A03E
		private static bool TryStartAnalyzingSymbol(ISymbol symbol, DiagnosticAnalyzer analyzer, AnalysisScope analysisScope, [Nullable(2)] AnalysisState analysisState, [Nullable(2)] out AnalysisState.AnalyzerStateData analyzerState)
		{
			analyzerState = null;
			return analysisState == null || analysisState.TryStartAnalyzingSymbol(symbol, analyzer, out analyzerState);
		}

		// Token: 0x060021EE RID: 8686 RVA: 0x0006BE53 File Offset: 0x0006A053
		private static bool TryStartSymbolEndAnalysis(ISymbol symbol, DiagnosticAnalyzer analyzer, [Nullable(2)] AnalysisState analysisState, [Nullable(2)] out AnalysisState.AnalyzerStateData analyzerState)
		{
			analyzerState = null;
			return analysisState == null || analysisState.TryStartSymbolEndAnalysis(symbol, analyzer, out analyzerState);
		}

		// Token: 0x060021EF RID: 8687 RVA: 0x0006BE66 File Offset: 0x0006A066
		private static bool TryStartAnalyzingDeclaration(ISymbol symbol, int declarationIndex, DiagnosticAnalyzer analyzer, AnalysisScope analysisScope, [Nullable(2)] AnalysisState analysisState, [Nullable(2)] out AnalysisState.DeclarationAnalyzerStateData analyzerState)
		{
			analyzerState = null;
			return analysisState == null || analysisState.TryStartAnalyzingDeclaration(symbol, declarationIndex, analyzer, out analyzerState);
		}

		// Token: 0x060021F0 RID: 8688 RVA: 0x0006BE7E File Offset: 0x0006A07E
		private static bool IsEventComplete(CompilationEvent compilationEvent, DiagnosticAnalyzer analyzer, [Nullable(2)] AnalysisState analysisState)
		{
			return analysisState == null || analysisState.IsEventComplete(compilationEvent, analyzer);
		}

		// Token: 0x060021F1 RID: 8689 RVA: 0x0006BE8D File Offset: 0x0006A08D
		private static bool IsSymbolComplete(ISymbol symbol, DiagnosticAnalyzer analyzer, [Nullable(2)] AnalysisState analysisState)
		{
			return analysisState == null || analysisState.IsSymbolComplete(symbol, analyzer);
		}

		// Token: 0x060021F2 RID: 8690 RVA: 0x0006BE9C File Offset: 0x0006A09C
		private static bool IsSymbolEndAnalysisComplete(ISymbol symbol, DiagnosticAnalyzer analyzer, [Nullable(2)] AnalysisState analysisState)
		{
			return analysisState == null || analysisState.IsSymbolEndAnalysisComplete(symbol, analyzer);
		}

		// Token: 0x060021F3 RID: 8691 RVA: 0x0006BEAB File Offset: 0x0006A0AB
		private static bool IsDeclarationComplete(ISymbol symbol, int declarationIndex, DiagnosticAnalyzer analyzer, [Nullable(2)] AnalysisState analysisState)
		{
			return analysisState == null || analysisState.IsDeclarationComplete(symbol, declarationIndex, analyzer);
		}

		// Token: 0x060021F4 RID: 8692 RVA: 0x0006BEBC File Offset: 0x0006A0BC
		internal TimeSpan ResetAnalyzerExecutionTime(DiagnosticAnalyzer analyzer)
		{
			StrongBox<long> strongBox;
			if (!this._analyzerExecutionTimeMap.TryRemove(analyzer, ref strongBox))
			{
				return TimeSpan.Zero;
			}
			return TimeSpan.FromTicks(strongBox.Value);
		}

		// Token: 0x060021F5 RID: 8693 RVA: 0x0006BEEC File Offset: 0x0006A0EC
		private ControlFlowGraph GetControlFlowGraphImpl(IOperation operation)
		{
			if (this._lazyControlFlowGraphMap == null)
			{
				Interlocked.CompareExchange<ConcurrentDictionary<IOperation, ControlFlowGraph>>(ref this._lazyControlFlowGraphMap, new ConcurrentDictionary<IOperation, ControlFlowGraph>(), null);
			}
			return this._lazyControlFlowGraphMap.GetOrAdd(operation, delegate(IOperation op)
			{
				ControlFlowGraph controlFlowGraph = null;
				ControlFlowRegion controlFlowRegion = null;
				ControlFlowGraphBuilder.CaptureIdDispenser captureIdDispenser = null;
				ControlFlowGraphBuilder.Context context = default(ControlFlowGraphBuilder.Context);
				return ControlFlowGraphBuilder.Create(op, controlFlowGraph, controlFlowRegion, captureIdDispenser, in context);
			});
		}

		// Token: 0x060021F6 RID: 8694 RVA: 0x0006BF40 File Offset: 0x0006A140
		private bool IsAnalyzerSuppressedForSymbol(DiagnosticAnalyzer analyzer, ISymbol symbol)
		{
			foreach (Location location in symbol.Locations)
			{
				if (location.SourceTree != null && !this.IsAnalyzerSuppressedForTree(analyzer, location.SourceTree))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060021F7 RID: 8695 RVA: 0x0006BF8C File Offset: 0x0006A18C
		public void OnOperationBlockActionsExecuted([Nullable(new byte[] { 0, 1 })] ImmutableArray<IOperation> operationBlocks)
		{
			ConcurrentDictionary<IOperation, ControlFlowGraph> lazyControlFlowGraphMap = this._lazyControlFlowGraphMap;
			if (lazyControlFlowGraphMap != null && lazyControlFlowGraphMap.Count > 0)
			{
				foreach (IOperation operation in operationBlocks)
				{
					IOperation rootOperation = operation.GetRootOperation();
					ControlFlowGraph controlFlowGraph;
					this._lazyControlFlowGraphMap.TryRemove(rootOperation, ref controlFlowGraph);
				}
			}
		}

		// Token: 0x04000E8C RID: 3724
		private const string DiagnosticCategory = "Compiler";

		// Token: 0x04000E8D RID: 3725
		internal const string AnalyzerExceptionDiagnosticId = "AD0001";

		// Token: 0x04000E8E RID: 3726
		internal const string AnalyzerDriverExceptionDiagnosticId = "AD0002";

		// Token: 0x04000E8F RID: 3727
		[Nullable(2)]
		private readonly Compilation _compilation;

		// Token: 0x04000E90 RID: 3728
		[Nullable(2)]
		private readonly AnalyzerOptions _analyzerOptions;

		// Token: 0x04000E91 RID: 3729
		[Nullable(new byte[] { 2, 1 })]
		private readonly Action<Diagnostic> _addNonCategorizedDiagnostic;

		// Token: 0x04000E92 RID: 3730
		[Nullable(new byte[] { 2, 1, 1 })]
		private readonly Action<Diagnostic, DiagnosticAnalyzer, bool> _addCategorizedLocalDiagnostic;

		// Token: 0x04000E93 RID: 3731
		[Nullable(new byte[] { 2, 1, 1 })]
		private readonly Action<Diagnostic, DiagnosticAnalyzer> _addCategorizedNonLocalDiagnostic;

		// Token: 0x04000E94 RID: 3732
		[Nullable(2)]
		private readonly Action<Suppression> _addSuppression;

		// Token: 0x04000E95 RID: 3733
		private readonly Action<Exception, DiagnosticAnalyzer, Diagnostic> _onAnalyzerException;

		// Token: 0x04000E96 RID: 3734
		[Nullable(new byte[] { 2, 1 })]
		private readonly Func<Exception, bool> _analyzerExceptionFilter;

		// Token: 0x04000E97 RID: 3735
		private readonly AnalyzerManager _analyzerManager;

		// Token: 0x04000E98 RID: 3736
		[Nullable(new byte[] { 2, 1 })]
		private readonly Func<DiagnosticAnalyzer, bool> _isCompilerAnalyzer;

		// Token: 0x04000E99 RID: 3737
		[Nullable(new byte[] { 2, 1, 2 })]
		private readonly Func<DiagnosticAnalyzer, object> _getAnalyzerGate;

		// Token: 0x04000E9A RID: 3738
		[Nullable(new byte[] { 2, 1, 1 })]
		private readonly Func<SyntaxTree, SemanticModel> _getSemanticModel;

		// Token: 0x04000E9B RID: 3739
		private readonly Func<DiagnosticAnalyzer, bool> _shouldSkipAnalysisOnGeneratedCode;

		// Token: 0x04000E9C RID: 3740
		private readonly Func<Diagnostic, DiagnosticAnalyzer, Compilation, CancellationToken, bool> _shouldSuppressGeneratedCodeDiagnostic;

		// Token: 0x04000E9D RID: 3741
		private readonly Func<SyntaxTree, TextSpan, bool> _isGeneratedCodeLocation;

		// Token: 0x04000E9E RID: 3742
		[Nullable(new byte[] { 2, 1, 1, 2 })]
		private readonly Func<DiagnosticAnalyzer, SyntaxTree, SyntaxTreeOptionsProvider, bool> _isAnalyzerSuppressedForTree;

		// Token: 0x04000E9F RID: 3743
		[Nullable(new byte[] { 2, 1, 1 })]
		private readonly ConcurrentDictionary<DiagnosticAnalyzer, StrongBox<long>> _analyzerExecutionTimeMap;

		// Token: 0x04000EA0 RID: 3744
		private readonly CompilationAnalysisValueProviderFactory _compilationAnalysisValueProviderFactory;

		// Token: 0x04000EA1 RID: 3745
		private readonly CancellationToken _cancellationToken;

		// Token: 0x04000EA2 RID: 3746
		[Nullable(new byte[] { 2, 1, 1 })]
		private Func<IOperation, ControlFlowGraph> _lazyGetControlFlowGraph;

		// Token: 0x04000EA3 RID: 3747
		[Nullable(new byte[] { 2, 1, 1 })]
		private ConcurrentDictionary<IOperation, ControlFlowGraph> _lazyControlFlowGraphMap;

		// Token: 0x020006C1 RID: 1729
		[Nullable(0)]
		private sealed class AnalyzerDiagnosticReporter
		{
			// Token: 0x06003A36 RID: 14902 RVA: 0x000A3BBC File Offset: 0x000A1DBC
			public static AnalyzerExecutor.AnalyzerDiagnosticReporter GetInstance(SourceOrAdditionalFile contextFile, TextSpan? span, Compilation compilation, DiagnosticAnalyzer analyzer, bool isSyntaxDiagnostic, [Nullable(new byte[] { 2, 1 })] Action<Diagnostic> addNonCategorizedDiagnostic, [Nullable(new byte[] { 2, 1, 1 })] Action<Diagnostic, DiagnosticAnalyzer, bool> addCategorizedLocalDiagnostic, [Nullable(new byte[] { 2, 1, 1 })] Action<Diagnostic, DiagnosticAnalyzer> addCategorizedNonLocalDiagnostic, Func<Diagnostic, DiagnosticAnalyzer, Compilation, CancellationToken, bool> shouldSuppressGeneratedCodeDiagnostic, CancellationToken cancellationToken)
			{
				AnalyzerExecutor.AnalyzerDiagnosticReporter analyzerDiagnosticReporter = AnalyzerExecutor.AnalyzerDiagnosticReporter.s_objectPool.Allocate();
				analyzerDiagnosticReporter._contextFile = new SourceOrAdditionalFile?(contextFile);
				analyzerDiagnosticReporter._span = span;
				analyzerDiagnosticReporter._compilation = compilation;
				analyzerDiagnosticReporter._analyzer = analyzer;
				analyzerDiagnosticReporter._isSyntaxDiagnostic = isSyntaxDiagnostic;
				analyzerDiagnosticReporter._addNonCategorizedDiagnostic = addNonCategorizedDiagnostic;
				analyzerDiagnosticReporter._addCategorizedLocalDiagnostic = addCategorizedLocalDiagnostic;
				analyzerDiagnosticReporter._addCategorizedNonLocalDiagnostic = addCategorizedNonLocalDiagnostic;
				analyzerDiagnosticReporter._shouldSuppressGeneratedCodeDiagnostic = shouldSuppressGeneratedCodeDiagnostic;
				analyzerDiagnosticReporter._cancellationToken = cancellationToken;
				return analyzerDiagnosticReporter;
			}

			// Token: 0x06003A37 RID: 14903 RVA: 0x000A3C24 File Offset: 0x000A1E24
			public void Free()
			{
				this._contextFile = default(SourceOrAdditionalFile?);
				this._span = default(TextSpan?);
				this._compilation = null;
				this._analyzer = null;
				this._isSyntaxDiagnostic = false;
				this._addNonCategorizedDiagnostic = null;
				this._addCategorizedLocalDiagnostic = null;
				this._addCategorizedNonLocalDiagnostic = null;
				this._shouldSuppressGeneratedCodeDiagnostic = null;
				this._cancellationToken = default(CancellationToken);
				AnalyzerExecutor.AnalyzerDiagnosticReporter.s_objectPool.Free(this);
			}

			// Token: 0x06003A38 RID: 14904 RVA: 0x000A3C91 File Offset: 0x000A1E91
			private AnalyzerDiagnosticReporter()
			{
				this.AddDiagnosticAction = new Action<Diagnostic>(this.AddDiagnostic);
			}

			// Token: 0x06003A39 RID: 14905 RVA: 0x000A3CAC File Offset: 0x000A1EAC
			private void AddDiagnostic(Diagnostic diagnostic)
			{
				if (this._shouldSuppressGeneratedCodeDiagnostic.Invoke(diagnostic, this._analyzer, this._compilation, this._cancellationToken))
				{
					return;
				}
				if (this._addCategorizedLocalDiagnostic == null)
				{
					this._addNonCategorizedDiagnostic.Invoke(diagnostic);
					return;
				}
				if (this.<AddDiagnostic>g__isLocalDiagnostic|15_0(diagnostic) && (this._span == null || this._span.Value.IntersectsWith(diagnostic.Location.SourceSpan)))
				{
					this._addCategorizedLocalDiagnostic.Invoke(diagnostic, this._analyzer, this._isSyntaxDiagnostic);
					return;
				}
				this._addCategorizedNonLocalDiagnostic.Invoke(diagnostic, this._analyzer);
			}

			// Token: 0x06003A3B RID: 14907 RVA: 0x000A3D70 File Offset: 0x000A1F70
			[CompilerGenerated]
			private bool <AddDiagnostic>g__isLocalDiagnostic|15_0(Diagnostic diagnostic)
			{
				if (diagnostic.Location.IsInSource)
				{
					return ((this._contextFile != null) ? this._contextFile.GetValueOrDefault().SourceTree : null) != null && this._contextFile.Value.SourceTree == diagnostic.Location.SourceTree;
				}
				if (((this._contextFile != null) ? this._contextFile.GetValueOrDefault().AdditionalFile : null) != null)
				{
					ExternalFileLocation externalFileLocation = diagnostic.Location as ExternalFileLocation;
					if (externalFileLocation != null)
					{
						return PathUtilities.Comparer.Equals(this._contextFile.Value.AdditionalFile.Path, externalFileLocation.FilePath);
					}
				}
				return false;
			}

			// Token: 0x04001A53 RID: 6739
			public readonly Action<Diagnostic> AddDiagnosticAction;

			// Token: 0x04001A54 RID: 6740
			private static readonly ObjectPool<AnalyzerExecutor.AnalyzerDiagnosticReporter> s_objectPool = new ObjectPool<AnalyzerExecutor.AnalyzerDiagnosticReporter>(() => new AnalyzerExecutor.AnalyzerDiagnosticReporter(), 10);

			// Token: 0x04001A55 RID: 6741
			private SourceOrAdditionalFile? _contextFile;

			// Token: 0x04001A56 RID: 6742
			private TextSpan? _span;

			// Token: 0x04001A57 RID: 6743
			private Compilation _compilation;

			// Token: 0x04001A58 RID: 6744
			private DiagnosticAnalyzer _analyzer;

			// Token: 0x04001A59 RID: 6745
			private bool _isSyntaxDiagnostic;

			// Token: 0x04001A5A RID: 6746
			[Nullable(new byte[] { 2, 1 })]
			private Action<Diagnostic> _addNonCategorizedDiagnostic;

			// Token: 0x04001A5B RID: 6747
			[Nullable(new byte[] { 2, 1, 1 })]
			private Action<Diagnostic, DiagnosticAnalyzer, bool> _addCategorizedLocalDiagnostic;

			// Token: 0x04001A5C RID: 6748
			[Nullable(new byte[] { 2, 1, 1 })]
			private Action<Diagnostic, DiagnosticAnalyzer> _addCategorizedNonLocalDiagnostic;

			// Token: 0x04001A5D RID: 6749
			private Func<Diagnostic, DiagnosticAnalyzer, Compilation, CancellationToken, bool> _shouldSuppressGeneratedCodeDiagnostic;

			// Token: 0x04001A5E RID: 6750
			private CancellationToken _cancellationToken;
		}
	}
}
