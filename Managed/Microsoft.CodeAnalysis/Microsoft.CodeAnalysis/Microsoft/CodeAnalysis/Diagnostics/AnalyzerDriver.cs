using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.Collections;
using Microsoft.CodeAnalysis.Diagnostics.Telemetry;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x02000303 RID: 771
	[NullableContext(1)]
	[Nullable(0)]
	internal abstract class AnalyzerDriver : IDisposable
	{
		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x06002126 RID: 8486 RVA: 0x00066B44 File Offset: 0x00064D44
		[Nullable(new byte[] { 0, 1 })]
		protected ImmutableArray<DiagnosticAnalyzer> Analyzers
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x06002127 RID: 8487 RVA: 0x00066B4C File Offset: 0x00064D4C
		protected AnalyzerManager AnalyzerManager { get; }

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x06002128 RID: 8488 RVA: 0x00066B54 File Offset: 0x00064D54
		protected AnalyzerExecutor AnalyzerExecutor
		{
			get
			{
				return this._lazyAnalyzerExecutor;
			}
		}

		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x06002129 RID: 8489 RVA: 0x00066B5C File Offset: 0x00064D5C
		protected AnalyzerDriver.CompilationData CurrentCompilationData
		{
			get
			{
				return this._lazyCurrentCompilationData;
			}
		}

		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x0600212A RID: 8490 RVA: 0x00066B64 File Offset: 0x00064D64
		protected CachingSemanticModelProvider SemanticModelProvider
		{
			get
			{
				return this.CurrentCompilationData.SemanticModelProvider;
			}
		}

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x0600212B RID: 8491 RVA: 0x00066B71 File Offset: 0x00064D71
		protected readonly ref AnalyzerActions AnalyzerActions
		{
			get
			{
				return ref this._lazyAnalyzerActions;
			}
		}

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x0600212C RID: 8492 RVA: 0x00066B79 File Offset: 0x00064D79
		protected ImmutableHashSet<DiagnosticAnalyzer> UnsuppressedAnalyzers
		{
			get
			{
				return this._lazyUnsuppressedAnalyzers;
			}
		}

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x0600212D RID: 8493 RVA: 0x00066B81 File Offset: 0x00064D81
		[Nullable(new byte[] { 1, 0, 1, 1, 1 })]
		private ConcurrentDictionary<ValueTuple<INamespaceOrTypeSymbol, DiagnosticAnalyzer>, AnalyzerDriver.IGroupedAnalyzerActions> PerSymbolAnalyzerActionsCache
		{
			[return: Nullable(new byte[] { 1, 0, 1, 1, 1 })]
			get
			{
				return this._lazyPerSymbolAnalyzerActionsCache;
			}
		}

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x0600212E RID: 8494 RVA: 0x00066B89 File Offset: 0x00064D89
		private ImmutableHashSet<DiagnosticAnalyzer> CompilationEndAnalyzers
		{
			get
			{
				return this._lazyCompilationEndAnalyzers;
			}
		}

		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x0600212F RID: 8495 RVA: 0x00066B91 File Offset: 0x00064D91
		[Nullable(new byte[] { 0, 1, 1 })]
		private ImmutableSegmentedDictionary<DiagnosticAnalyzer, SemaphoreSlim> AnalyzerGateMap
		{
			[return: Nullable(new byte[] { 0, 1, 1 })]
			get
			{
				return this._lazyAnalyzerGateMap;
			}
		}

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x06002130 RID: 8496 RVA: 0x00066B99 File Offset: 0x00064D99
		[Nullable(new byte[] { 0, 1 })]
		private ImmutableSegmentedDictionary<DiagnosticAnalyzer, GeneratedCodeAnalysisFlags> GeneratedCodeAnalysisFlagsMap
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._lazyGeneratedCodeAnalysisFlagsMap;
			}
		}

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x06002131 RID: 8497 RVA: 0x00066BA1 File Offset: 0x00064DA1
		private ImmutableHashSet<DiagnosticAnalyzer> NonConfigurableAnalyzers
		{
			get
			{
				return this._lazyNonConfigurableAnalyzers;
			}
		}

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x06002132 RID: 8498 RVA: 0x00066BA9 File Offset: 0x00064DA9
		private ImmutableHashSet<DiagnosticAnalyzer> SymbolStartAnalyzers
		{
			get
			{
				return this._lazySymbolStartAnalyzers;
			}
		}

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x06002133 RID: 8499 RVA: 0x00066BB1 File Offset: 0x00064DB1
		private bool TreatAllCodeAsNonGeneratedCode
		{
			get
			{
				return this._lazyTreatAllCodeAsNonGeneratedCode.Value;
			}
		}

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x06002134 RID: 8500 RVA: 0x00066BBE File Offset: 0x00064DBE
		private ConcurrentDictionary<SyntaxTree, bool> GeneratedCodeFilesMap
		{
			get
			{
				return this._lazyGeneratedCodeFilesMap;
			}
		}

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x06002135 RID: 8501 RVA: 0x00066BC6 File Offset: 0x00064DC6
		private Dictionary<SyntaxTree, ImmutableHashSet<ISymbol>> GeneratedCodeSymbolsForTreeMap
		{
			get
			{
				return this._lazyGeneratedCodeSymbolsForTreeMap;
			}
		}

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x06002136 RID: 8502 RVA: 0x00066BCE File Offset: 0x00064DCE
		private ConcurrentDictionary<SyntaxTree, ImmutableHashSet<DiagnosticAnalyzer>> SuppressedAnalyzersForTreeMap
		{
			get
			{
				return this._lazySuppressedAnalyzersForTreeMap;
			}
		}

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x06002137 RID: 8503 RVA: 0x00066BD6 File Offset: 0x00064DD6
		private ConcurrentDictionary<ISymbol, bool> IsGeneratedCodeSymbolMap
		{
			get
			{
				return this._lazyIsGeneratedCodeSymbolMap;
			}
		}

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x06002138 RID: 8504 RVA: 0x00066BDE File Offset: 0x00064DDE
		public AsyncQueue<CompilationEvent> CompilationEventQueue
		{
			get
			{
				return this._lazyCompilationEventQueue;
			}
		}

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x06002139 RID: 8505 RVA: 0x00066BE6 File Offset: 0x00064DE6
		public DiagnosticQueue DiagnosticQueue
		{
			get
			{
				return this._lazyDiagnosticQueue;
			}
		}

		// Token: 0x0600213A RID: 8506 RVA: 0x00066BF0 File Offset: 0x00064DF0
		protected AnalyzerDriver([Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticAnalyzer> analyzers, AnalyzerManager analyzerManager, SeverityFilter severityFilter, Func<SyntaxTrivia, bool> isComment)
		{
			this.Analyzers = analyzers;
			this.AnalyzerManager = analyzerManager;
			this._isGeneratedCode = (SyntaxTree tree, CancellationToken ct) => GeneratedCodeUtilities.IsGeneratedCode(tree, isComment, ct);
			this._severityFilter = severityFilter;
			this._hasDiagnosticSuppressors = this.Analyzers.Any<DiagnosticAnalyzer>((DiagnosticAnalyzer a) => a is DiagnosticSuppressor);
			this._programmaticSuppressions = (this._hasDiagnosticSuppressors ? new ConcurrentSet<Suppression>() : null);
			this._diagnosticsProcessedForProgrammaticSuppressions = (this._hasDiagnosticSuppressors ? new ConcurrentSet<Diagnostic>(ReferenceEqualityComparer.Instance) : null);
			this._lazyAnalyzerGateMap = ImmutableSegmentedDictionary<DiagnosticAnalyzer, SemaphoreSlim>.Empty;
		}

		// Token: 0x0600213B RID: 8507 RVA: 0x00066CB0 File Offset: 0x00064EB0
		private void Initialize(AnalyzerExecutor analyzerExecutor, DiagnosticQueue diagnosticQueue, AnalyzerDriver.CompilationData compilationData, CancellationToken cancellationToken)
		{
			AnalyzerDriver.<>c__DisplayClass84_0 CS$<>8__locals1 = new AnalyzerDriver.<>c__DisplayClass84_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.analyzerExecutor = analyzerExecutor;
			try
			{
				this._lazyAnalyzerExecutor = CS$<>8__locals1.analyzerExecutor;
				this._lazyCurrentCompilationData = compilationData;
				this._lazyDiagnosticQueue = diagnosticQueue;
				this._lazyInitializeTask = Task.Run(delegate
				{
					AnalyzerDriver.<>c__DisplayClass84_0.<<Initialize>b__0>d <<Initialize>b__0>d;
					<<Initialize>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
					<<Initialize>b__0>d.<>4__this = CS$<>8__locals1;
					<<Initialize>b__0>d.<>1__state = -1;
					<<Initialize>b__0>d.<>t__builder.Start<AnalyzerDriver.<>c__DisplayClass84_0.<<Initialize>b__0>d>(ref <<Initialize>b__0>d);
					return <<Initialize>b__0>d.<>t__builder.Task;
				}, cancellationToken);
				cancellationToken.ThrowIfCancellationRequested();
				this._initializeSucceeded = true;
			}
			finally
			{
				if (this._lazyInitializeTask == null)
				{
					this._lazyInitializeTask = Task.FromCanceled(new CancellationToken(true));
					this._lazyPrimaryTask = Task.FromCanceled(new CancellationToken(true));
					this.DiagnosticQueue.TryComplete();
				}
			}
		}

		// Token: 0x0600213C RID: 8508 RVA: 0x00066D5C File Offset: 0x00064F5C
		internal void Initialize(Compilation compilation, CompilationWithAnalyzersOptions analysisOptions, AnalyzerDriver.CompilationData compilationData, bool categorizeDiagnostics, CancellationToken cancellationToken)
		{
			DiagnosticQueue diagnosticQueue = DiagnosticQueue.Create(categorizeDiagnostics);
			Action<Diagnostic> addNotCategorizedDiagnostic = null;
			Action<Diagnostic, DiagnosticAnalyzer, bool> action = null;
			Action<Diagnostic, DiagnosticAnalyzer> addCategorizedNonLocalDiagnostic = null;
			if (categorizeDiagnostics)
			{
				action = AnalyzerDriver.GetDiagnosticSink(new Action<Diagnostic, DiagnosticAnalyzer, bool>(diagnosticQueue.EnqueueLocal), compilation, analysisOptions.Options, this._severityFilter, cancellationToken);
				addCategorizedNonLocalDiagnostic = AnalyzerDriver.GetDiagnosticSink(new Action<Diagnostic, DiagnosticAnalyzer>(diagnosticQueue.EnqueueNonLocal), compilation, analysisOptions.Options, this._severityFilter, cancellationToken);
			}
			else
			{
				addNotCategorizedDiagnostic = AnalyzerDriver.GetDiagnosticSink(new Action<Diagnostic>(diagnosticQueue.Enqueue), compilation, analysisOptions.Options, this._severityFilter, cancellationToken);
			}
			Action<Exception, DiagnosticAnalyzer, Diagnostic> action2 = delegate(Exception ex, DiagnosticAnalyzer analyzer, Diagnostic diagnostic)
			{
				Diagnostic filteredDiagnostic = AnalyzerDriver.GetFilteredDiagnostic(diagnostic, compilation, analysisOptions.Options, this._severityFilter, cancellationToken);
				if (filteredDiagnostic != null)
				{
					if (analysisOptions.OnAnalyzerException != null)
					{
						analysisOptions.OnAnalyzerException.Invoke(ex, analyzer, filteredDiagnostic);
						return;
					}
					if (categorizeDiagnostics)
					{
						addCategorizedNonLocalDiagnostic.Invoke(filteredDiagnostic, analyzer);
						return;
					}
					addNotCategorizedDiagnostic.Invoke(filteredDiagnostic);
				}
			};
			AnalyzerExecutor analyzerExecutor = AnalyzerExecutor.Create(compilation, analysisOptions.Options ?? AnalyzerOptions.Empty, addNotCategorizedDiagnostic, action2, analysisOptions.AnalyzerExceptionFilter, new Func<DiagnosticAnalyzer, bool>(AnalyzerDriver.IsCompilerAnalyzer), this.AnalyzerManager, new Func<DiagnosticAnalyzer, bool>(this.ShouldSkipAnalysisOnGeneratedCode), new Func<Diagnostic, DiagnosticAnalyzer, Compilation, CancellationToken, bool>(this.ShouldSuppressGeneratedCodeDiagnostic), new Func<SyntaxTree, TextSpan, bool>(this.IsGeneratedOrHiddenCodeLocation), new Func<DiagnosticAnalyzer, SyntaxTree, SyntaxTreeOptionsProvider, bool>(this.IsAnalyzerSuppressedForTree), new Func<DiagnosticAnalyzer, object>(this.GetAnalyzerGate), new Func<SyntaxTree, SemanticModel>(this.GetOrCreateSemanticModel), analysisOptions.LogAnalyzerExecutionTime, action, addCategorizedNonLocalDiagnostic, delegate(Suppression s)
			{
				this._programmaticSuppressions.Add(s);
			}, cancellationToken);
			this.Initialize(analyzerExecutor, diagnosticQueue, compilationData, cancellationToken);
		}

		// Token: 0x0600213D RID: 8509 RVA: 0x00066F24 File Offset: 0x00065124
		[return: Nullable(2)]
		private SemaphoreSlim GetAnalyzerGate(DiagnosticAnalyzer analyzer)
		{
			SemaphoreSlim semaphoreSlim;
			if (this.AnalyzerGateMap.TryGetValue(analyzer, out semaphoreSlim))
			{
				return semaphoreSlim;
			}
			return null;
		}

		// Token: 0x0600213E RID: 8510 RVA: 0x00066F48 File Offset: 0x00065148
		private ImmutableHashSet<DiagnosticAnalyzer> ComputeNonConfigurableAnalyzers(ImmutableHashSet<DiagnosticAnalyzer> unsuppressedAnalyzers)
		{
			ImmutableHashSet<DiagnosticAnalyzer>.Builder builder = ImmutableHashSet.CreateBuilder<DiagnosticAnalyzer>();
			foreach (DiagnosticAnalyzer diagnosticAnalyzer in unsuppressedAnalyzers)
			{
				ImmutableArray<DiagnosticDescriptor>.Enumerator enumerator2 = this.AnalyzerManager.GetSupportedDiagnosticDescriptors(diagnosticAnalyzer, this.AnalyzerExecutor).GetEnumerator();
				while (enumerator2.MoveNext())
				{
					if (enumerator2.Current.IsNotConfigurable())
					{
						builder.Add(diagnosticAnalyzer);
						break;
					}
				}
			}
			return builder.ToImmutableHashSet<DiagnosticAnalyzer>();
		}

		// Token: 0x0600213F RID: 8511 RVA: 0x00066FDC File Offset: 0x000651DC
		private ImmutableHashSet<DiagnosticAnalyzer> ComputeSymbolStartAnalyzers(ImmutableHashSet<DiagnosticAnalyzer> unsuppressedAnalyzers)
		{
			ImmutableHashSet<DiagnosticAnalyzer>.Builder builder = ImmutableHashSet.CreateBuilder<DiagnosticAnalyzer>();
			foreach (SymbolStartAnalyzerAction symbolStartAnalyzerAction in this.AnalyzerActions.SymbolStartActions)
			{
				if (unsuppressedAnalyzers.Contains(symbolStartAnalyzerAction.Analyzer))
				{
					builder.Add(symbolStartAnalyzerAction.Analyzer);
				}
			}
			return builder.ToImmutableHashSet<DiagnosticAnalyzer>();
		}

		// Token: 0x06002140 RID: 8512 RVA: 0x00067038 File Offset: 0x00065238
		private static bool ComputeShouldSkipAnalysisOnGeneratedCode(ImmutableHashSet<DiagnosticAnalyzer> analyzers, [Nullable(new byte[] { 0, 1 })] ImmutableSegmentedDictionary<DiagnosticAnalyzer, GeneratedCodeAnalysisFlags> generatedCodeAnalysisFlagsMap, bool treatAllCodeAsNonGeneratedCode)
		{
			using (ImmutableHashSet<DiagnosticAnalyzer>.Enumerator enumerator = analyzers.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!AnalyzerDriver.ShouldSkipAnalysisOnGeneratedCode(enumerator.Current, generatedCodeAnalysisFlagsMap, treatAllCodeAsNonGeneratedCode))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06002141 RID: 8513 RVA: 0x00067090 File Offset: 0x00065290
		private static bool ComputeShouldTreatAllCodeAsNonGeneratedCode(ImmutableHashSet<DiagnosticAnalyzer> analyzers, [Nullable(new byte[] { 0, 1 })] ImmutableSegmentedDictionary<DiagnosticAnalyzer, GeneratedCodeAnalysisFlags> generatedCodeAnalysisFlagsMap)
		{
			foreach (DiagnosticAnalyzer diagnosticAnalyzer in analyzers)
			{
				GeneratedCodeAnalysisFlags generatedCodeAnalysisFlags = generatedCodeAnalysisFlagsMap[diagnosticAnalyzer];
				bool flag = (generatedCodeAnalysisFlags & GeneratedCodeAnalysisFlags.Analyze) > GeneratedCodeAnalysisFlags.None;
				bool flag2 = (generatedCodeAnalysisFlags & GeneratedCodeAnalysisFlags.ReportDiagnostics) > GeneratedCodeAnalysisFlags.None;
				if (!flag || !flag2)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002142 RID: 8514 RVA: 0x000670FC File Offset: 0x000652FC
		private bool ShouldSkipAnalysisOnGeneratedCode(DiagnosticAnalyzer analyzer)
		{
			return AnalyzerDriver.ShouldSkipAnalysisOnGeneratedCode(analyzer, this.GeneratedCodeAnalysisFlagsMap, this.TreatAllCodeAsNonGeneratedCode);
		}

		// Token: 0x06002143 RID: 8515 RVA: 0x00067110 File Offset: 0x00065310
		private static bool ShouldSkipAnalysisOnGeneratedCode(DiagnosticAnalyzer analyzer, [Nullable(new byte[] { 0, 1 })] ImmutableSegmentedDictionary<DiagnosticAnalyzer, GeneratedCodeAnalysisFlags> generatedCodeAnalysisFlagsMap, bool treatAllCodeAsNonGeneratedCode)
		{
			return !treatAllCodeAsNonGeneratedCode && (generatedCodeAnalysisFlagsMap[analyzer] & GeneratedCodeAnalysisFlags.Analyze) == GeneratedCodeAnalysisFlags.None;
		}

		// Token: 0x06002144 RID: 8516 RVA: 0x00067124 File Offset: 0x00065324
		private bool ShouldSuppressGeneratedCodeDiagnostic(Diagnostic diagnostic, DiagnosticAnalyzer analyzer, Compilation compilation, CancellationToken cancellationToken)
		{
			return !this.TreatAllCodeAsNonGeneratedCode && (this.GeneratedCodeAnalysisFlagsMap[analyzer] & GeneratedCodeAnalysisFlags.ReportDiagnostics) == GeneratedCodeAnalysisFlags.None && this.IsInGeneratedCode(diagnostic.Location, compilation, cancellationToken);
		}

		// Token: 0x06002145 RID: 8517 RVA: 0x00067164 File Offset: 0x00065364
		internal Task AttachQueueAndProcessAllEventsAsync(AsyncQueue<CompilationEvent> eventQueue, AnalysisScope analysisScope, [Nullable(2)] AnalysisState analysisState, CancellationToken cancellationToken)
		{
			AnalyzerDriver.<AttachQueueAndProcessAllEventsAsync>d__94 <AttachQueueAndProcessAllEventsAsync>d__;
			<AttachQueueAndProcessAllEventsAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<AttachQueueAndProcessAllEventsAsync>d__.<>4__this = this;
			<AttachQueueAndProcessAllEventsAsync>d__.eventQueue = eventQueue;
			<AttachQueueAndProcessAllEventsAsync>d__.analysisScope = analysisScope;
			<AttachQueueAndProcessAllEventsAsync>d__.analysisState = analysisState;
			<AttachQueueAndProcessAllEventsAsync>d__.cancellationToken = cancellationToken;
			<AttachQueueAndProcessAllEventsAsync>d__.<>1__state = -1;
			<AttachQueueAndProcessAllEventsAsync>d__.<>t__builder.Start<AnalyzerDriver.<AttachQueueAndProcessAllEventsAsync>d__94>(ref <AttachQueueAndProcessAllEventsAsync>d__);
			return <AttachQueueAndProcessAllEventsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06002146 RID: 8518 RVA: 0x000671C8 File Offset: 0x000653C8
		internal void AttachQueueAndStartProcessingEvents(AsyncQueue<CompilationEvent> eventQueue, AnalysisScope analysisScope, CancellationToken cancellationToken)
		{
			try
			{
				if (this._initializeSucceeded)
				{
					this._lazyCompilationEventQueue = eventQueue;
					this._lazyQueueRegistration = new CancellationTokenRegistration?(cancellationToken.Register(delegate
					{
						this.CompilationEventQueue.TryComplete();
						this.DiagnosticQueue.TryComplete();
					}));
					this._lazyPrimaryTask = this.ExecutePrimaryAnalysisTaskAsync(analysisScope, null, false, cancellationToken).ContinueWith<bool>((Task c) => this.DiagnosticQueue.TryComplete(), cancellationToken, 524288, TaskScheduler.Default);
				}
			}
			finally
			{
				if (this._lazyPrimaryTask == null)
				{
					this._lazyPrimaryTask = Task.FromCanceled(new CancellationToken(true));
					this.DiagnosticQueue.TryComplete();
				}
			}
		}

		// Token: 0x06002147 RID: 8519 RVA: 0x00067268 File Offset: 0x00065468
		private Task ExecutePrimaryAnalysisTaskAsync(AnalysisScope analysisScope, [Nullable(2)] AnalysisState analysisState, bool usingPrePopulatedEventQueue, CancellationToken cancellationToken)
		{
			AnalyzerDriver.<ExecutePrimaryAnalysisTaskAsync>d__96 <ExecutePrimaryAnalysisTaskAsync>d__;
			<ExecutePrimaryAnalysisTaskAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ExecutePrimaryAnalysisTaskAsync>d__.<>4__this = this;
			<ExecutePrimaryAnalysisTaskAsync>d__.analysisScope = analysisScope;
			<ExecutePrimaryAnalysisTaskAsync>d__.analysisState = analysisState;
			<ExecutePrimaryAnalysisTaskAsync>d__.usingPrePopulatedEventQueue = usingPrePopulatedEventQueue;
			<ExecutePrimaryAnalysisTaskAsync>d__.cancellationToken = cancellationToken;
			<ExecutePrimaryAnalysisTaskAsync>d__.<>1__state = -1;
			<ExecutePrimaryAnalysisTaskAsync>d__.<>t__builder.Start<AnalyzerDriver.<ExecutePrimaryAnalysisTaskAsync>d__96>(ref <ExecutePrimaryAnalysisTaskAsync>d__);
			return <ExecutePrimaryAnalysisTaskAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06002148 RID: 8520 RVA: 0x000672CC File Offset: 0x000654CC
		private static void OnDriverException(Task faultedTask, AnalyzerExecutor analyzerExecutor, [Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticAnalyzer> analyzers)
		{
			AggregateException exception = faultedTask.Exception;
			Exception ex = ((exception != null) ? exception.InnerException : null);
			if (ex == null || ex is OperationCanceledException)
			{
				return;
			}
			Diagnostic diagnostic = AnalyzerExecutor.CreateDriverExceptionDiagnostic(ex);
			DiagnosticAnalyzer diagnosticAnalyzer = analyzers[0];
			analyzerExecutor.OnAnalyzerException.Invoke(ex, diagnosticAnalyzer, diagnostic);
		}

		// Token: 0x06002149 RID: 8521 RVA: 0x00067318 File Offset: 0x00065518
		private void ExecuteSyntaxTreeActions(AnalysisScope analysisScope, [Nullable(2)] AnalysisState analysisState, CancellationToken cancellationToken)
		{
			if (analysisScope.IsSingleFileAnalysis && !analysisScope.IsSyntacticSingleFileAnalysis)
			{
				return;
			}
			foreach (SyntaxTree syntaxTree in analysisScope.SyntaxTrees)
			{
				bool flag = this.IsGeneratedCode(syntaxTree);
				SourceOrAdditionalFile sourceOrAdditionalFile = new SourceOrAdditionalFile(syntaxTree);
				if (flag && this.DoNotAnalyzeGeneratedCode)
				{
					if (analysisState != null)
					{
						analysisState.MarkSyntaxAnalysisComplete(sourceOrAdditionalFile, analysisScope.Analyzers);
					}
				}
				else
				{
					PooledHashSet<DiagnosticAnalyzer> pooledHashSet = ((analysisState != null) ? PooledHashSet<DiagnosticAnalyzer>.GetInstance() : null);
					try
					{
						foreach (ValueTuple<DiagnosticAnalyzer, ImmutableArray<SyntaxTreeAnalyzerAction>> valueTuple in this._lazySyntaxTreeActions)
						{
							DiagnosticAnalyzer item = valueTuple.Item1;
							ImmutableArray<SyntaxTreeAnalyzerAction> item2 = valueTuple.Item2;
							if (analysisScope.Contains(item))
							{
								cancellationToken.ThrowIfCancellationRequested();
								this.AnalyzerExecutor.TryExecuteSyntaxTreeActions(item2, item, sourceOrAdditionalFile, analysisScope, analysisState, flag);
								if (pooledHashSet != null)
								{
									pooledHashSet.Add(item);
								}
							}
						}
						if (analysisState != null)
						{
							analysisState.MarkSyntaxAnalysisCompleteForUnprocessedAnalyzers(sourceOrAdditionalFile, analysisScope, pooledHashSet);
						}
					}
					finally
					{
						if (pooledHashSet != null)
						{
							pooledHashSet.Free();
						}
					}
				}
			}
		}

		// Token: 0x0600214A RID: 8522 RVA: 0x00067444 File Offset: 0x00065644
		private void ExecuteAdditionalFileActions(AnalysisScope analysisScope, [Nullable(2)] AnalysisState analysisState, CancellationToken cancellationToken)
		{
			if (analysisScope.IsSingleFileAnalysis && !analysisScope.IsSyntacticSingleFileAnalysis)
			{
				return;
			}
			foreach (AdditionalText additionalText in analysisScope.AdditionalFiles)
			{
				SourceOrAdditionalFile sourceOrAdditionalFile = new SourceOrAdditionalFile(additionalText);
				PooledHashSet<DiagnosticAnalyzer> pooledHashSet = ((analysisState != null) ? PooledHashSet<DiagnosticAnalyzer>.GetInstance() : null);
				try
				{
					foreach (ValueTuple<DiagnosticAnalyzer, ImmutableArray<AdditionalFileAnalyzerAction>> valueTuple in this._lazyAdditionalFileActions)
					{
						DiagnosticAnalyzer item = valueTuple.Item1;
						ImmutableArray<AdditionalFileAnalyzerAction> item2 = valueTuple.Item2;
						if (analysisScope.Contains(item))
						{
							cancellationToken.ThrowIfCancellationRequested();
							this.AnalyzerExecutor.TryExecuteAdditionalFileActions(item2, item, sourceOrAdditionalFile, analysisScope, analysisState);
							if (pooledHashSet != null)
							{
								pooledHashSet.Add(item);
							}
						}
					}
					if (analysisState != null)
					{
						analysisState.MarkSyntaxAnalysisCompleteForUnprocessedAnalyzers(sourceOrAdditionalFile, analysisScope, pooledHashSet);
					}
				}
				finally
				{
					if (pooledHashSet != null)
					{
						pooledHashSet.Free();
					}
				}
			}
		}

		// Token: 0x0600214B RID: 8523 RVA: 0x0006753C File Offset: 0x0006573C
		public static AnalyzerDriver CreateAndAttachToCompilation(Compilation compilation, [Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticAnalyzer> analyzers, AnalyzerOptions options, AnalyzerManager analyzerManager, Action<Diagnostic> addExceptionDiagnostic, bool reportAnalyzer, SeverityFilter severityFilter, out Compilation newCompilation, CancellationToken cancellationToken)
		{
			Action<Exception, DiagnosticAnalyzer, Diagnostic> action = delegate(Exception ex, DiagnosticAnalyzer analyzer, Diagnostic diagnostic)
			{
				Action<Diagnostic> addExceptionDiagnostic2 = addExceptionDiagnostic;
				if (addExceptionDiagnostic2 == null)
				{
					return;
				}
				addExceptionDiagnostic2.Invoke(diagnostic);
			};
			Func<Exception, bool> func = null;
			return AnalyzerDriver.CreateAndAttachToCompilation(compilation, analyzers, options, analyzerManager, action, func, reportAnalyzer, severityFilter, out newCompilation, cancellationToken);
		}

		// Token: 0x0600214C RID: 8524 RVA: 0x00067578 File Offset: 0x00065778
		internal static AnalyzerDriver CreateAndAttachToCompilation(Compilation compilation, [Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticAnalyzer> analyzers, AnalyzerOptions options, AnalyzerManager analyzerManager, Action<Exception, DiagnosticAnalyzer, Diagnostic> onAnalyzerException, [Nullable(new byte[] { 2, 1 })] Func<Exception, bool> analyzerExceptionFilter, bool reportAnalyzer, SeverityFilter severityFilter, out Compilation newCompilation, CancellationToken cancellationToken)
		{
			AnalyzerDriver analyzerDriver = compilation.CreateAnalyzerDriver(analyzers, analyzerManager, severityFilter);
			newCompilation = compilation.WithSemanticModelProvider(new CachingSemanticModelProvider()).WithEventQueue(new AsyncQueue<CompilationEvent>());
			bool flag = false;
			CompilationWithAnalyzersOptions compilationWithAnalyzersOptions = new CompilationWithAnalyzersOptions(options, onAnalyzerException, true, reportAnalyzer, false, analyzerExceptionFilter);
			analyzerDriver.Initialize(newCompilation, compilationWithAnalyzersOptions, new AnalyzerDriver.CompilationData(newCompilation), flag, cancellationToken);
			AnalysisScope analysisScope = new AnalysisScope(newCompilation, options, analyzers, true, newCompilation.Options.ConcurrentBuild, flag);
			analyzerDriver.AttachQueueAndStartProcessingEvents(newCompilation.EventQueue, analysisScope, cancellationToken);
			return analyzerDriver;
		}

		// Token: 0x0600214D RID: 8525 RVA: 0x000675FC File Offset: 0x000657FC
		[return: Nullable(new byte[] { 1, 0, 1 })]
		public Task<ImmutableArray<Diagnostic>> GetDiagnosticsAsync(Compilation compilation)
		{
			AnalyzerDriver.<GetDiagnosticsAsync>d__102 <GetDiagnosticsAsync>d__;
			<GetDiagnosticsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<ImmutableArray<Diagnostic>>.Create();
			<GetDiagnosticsAsync>d__.<>4__this = this;
			<GetDiagnosticsAsync>d__.compilation = compilation;
			<GetDiagnosticsAsync>d__.<>1__state = -1;
			<GetDiagnosticsAsync>d__.<>t__builder.Start<AnalyzerDriver.<GetDiagnosticsAsync>d__102>(ref <GetDiagnosticsAsync>d__);
			return <GetDiagnosticsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600214E RID: 8526 RVA: 0x00067647 File Offset: 0x00065847
		private SemanticModel GetOrCreateSemanticModel(SyntaxTree tree)
		{
			return this.GetOrCreateSemanticModel(tree, this.AnalyzerExecutor.Compilation);
		}

		// Token: 0x0600214F RID: 8527 RVA: 0x0006765B File Offset: 0x0006585B
		private SemanticModel GetOrCreateSemanticModel(SyntaxTree tree, Compilation compilation)
		{
			return this.SemanticModelProvider.GetSemanticModel(tree, compilation, false);
		}

		// Token: 0x06002150 RID: 8528 RVA: 0x0006766C File Offset: 0x0006586C
		public void ApplyProgrammaticSuppressions(DiagnosticBag reportedDiagnostics, Compilation compilation)
		{
			if (!this._hasDiagnosticSuppressors)
			{
				return;
			}
			ImmutableArray<Diagnostic> immutableArray = this.ApplyProgrammaticSuppressionsCore(reportedDiagnostics.ToReadOnly(), compilation);
			reportedDiagnostics.Clear();
			reportedDiagnostics.AddRange<Diagnostic>(immutableArray);
		}

		// Token: 0x06002151 RID: 8529 RVA: 0x0006769D File Offset: 0x0006589D
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<Diagnostic> ApplyProgrammaticSuppressions([Nullable(new byte[] { 0, 1 })] ImmutableArray<Diagnostic> reportedDiagnostics, Compilation compilation)
		{
			if (reportedDiagnostics.IsEmpty || !this._hasDiagnosticSuppressors)
			{
				return reportedDiagnostics;
			}
			return this.ApplyProgrammaticSuppressionsCore(reportedDiagnostics, compilation);
		}

		// Token: 0x06002152 RID: 8530 RVA: 0x000676BC File Offset: 0x000658BC
		[return: Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<Diagnostic> ApplyProgrammaticSuppressionsCore([Nullable(new byte[] { 0, 1 })] ImmutableArray<Diagnostic> reportedDiagnostics, Compilation compilation)
		{
			ImmutableArray<Diagnostic> immutableArray;
			try
			{
				IEnumerable<Diagnostic> enumerable = reportedDiagnostics.Where<Diagnostic>((Diagnostic d) => !d.IsSuppressed && !d.IsNotConfigurable() && d.DefaultSeverity != DiagnosticSeverity.Error && !this._diagnosticsProcessedForProgrammaticSuppressions.Contains(d));
				if (enumerable.IsEmpty<Diagnostic>())
				{
					immutableArray = reportedDiagnostics;
				}
				else
				{
					this.<ApplyProgrammaticSuppressionsCore>g__executeSuppressionActions|107_0(enumerable, compilation.Options.ConcurrentBuild);
					if (this._programmaticSuppressions.IsEmpty)
					{
						immutableArray = reportedDiagnostics;
					}
					else
					{
						ArrayBuilder<Diagnostic> instance = ArrayBuilder<Diagnostic>.GetInstance(reportedDiagnostics.Length);
						ImmutableDictionary<Diagnostic, ProgrammaticSuppressionInfo> immutableDictionary = AnalyzerDriver.<ApplyProgrammaticSuppressionsCore>g__createProgrammaticSuppressionsByDiagnosticMap|107_1(this._programmaticSuppressions);
						foreach (Diagnostic diagnostic in reportedDiagnostics)
						{
							ProgrammaticSuppressionInfo programmaticSuppressionInfo;
							if (immutableDictionary.TryGetValue(diagnostic, out programmaticSuppressionInfo))
							{
								Diagnostic diagnostic2 = diagnostic.WithProgrammaticSuppression(programmaticSuppressionInfo);
								instance.Add(diagnostic2);
							}
							else
							{
								instance.Add(diagnostic);
							}
						}
						immutableArray = instance.ToImmutableAndFree();
					}
				}
			}
			finally
			{
				this._diagnosticsProcessedForProgrammaticSuppressions.AddRange(reportedDiagnostics);
			}
			return immutableArray;
		}

		// Token: 0x06002153 RID: 8531 RVA: 0x0006779C File Offset: 0x0006599C
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<Diagnostic> DequeueLocalDiagnosticsAndApplySuppressions(DiagnosticAnalyzer analyzer, bool syntax, Compilation compilation)
		{
			ImmutableArray<Diagnostic> immutableArray = (syntax ? this.DiagnosticQueue.DequeueLocalSyntaxDiagnostics(analyzer) : this.DiagnosticQueue.DequeueLocalSemanticDiagnostics(analyzer));
			return this.FilterDiagnosticsSuppressedInSourceOrByAnalyzers(immutableArray, compilation);
		}

		// Token: 0x06002154 RID: 8532 RVA: 0x000677D0 File Offset: 0x000659D0
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<Diagnostic> DequeueNonLocalDiagnosticsAndApplySuppressions(DiagnosticAnalyzer analyzer, Compilation compilation)
		{
			ImmutableArray<Diagnostic> immutableArray = this.DiagnosticQueue.DequeueNonLocalDiagnostics(analyzer);
			return this.FilterDiagnosticsSuppressedInSourceOrByAnalyzers(immutableArray, compilation);
		}

		// Token: 0x06002155 RID: 8533 RVA: 0x000677F2 File Offset: 0x000659F2
		[return: Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<Diagnostic> FilterDiagnosticsSuppressedInSourceOrByAnalyzers([Nullable(new byte[] { 0, 1 })] ImmutableArray<Diagnostic> diagnostics, Compilation compilation)
		{
			diagnostics = AnalyzerDriver.FilterDiagnosticsSuppressedInSource(diagnostics, compilation, this.CurrentCompilationData.SuppressMessageAttributeState);
			return this.ApplyProgrammaticSuppressions(diagnostics, compilation);
		}

		// Token: 0x06002156 RID: 8534 RVA: 0x00067810 File Offset: 0x00065A10
		[return: Nullable(new byte[] { 0, 1 })]
		private static ImmutableArray<Diagnostic> FilterDiagnosticsSuppressedInSource([Nullable(new byte[] { 0, 1 })] ImmutableArray<Diagnostic> diagnostics, Compilation compilation, SuppressMessageAttributeState suppressMessageState)
		{
			if (diagnostics.IsEmpty)
			{
				return diagnostics;
			}
			bool reportSuppressedDiagnostics = compilation.Options.ReportSuppressedDiagnostics;
			ImmutableArray<Diagnostic>.Builder builder = ImmutableArray.CreateBuilder<Diagnostic>();
			for (int i = 0; i < diagnostics.Length; i++)
			{
				Diagnostic diagnostic = suppressMessageState.ApplySourceSuppressions(diagnostics[i]);
				if (reportSuppressedDiagnostics || !diagnostic.IsSuppressed)
				{
					builder.Add(diagnostic);
				}
			}
			return builder.ToImmutable();
		}

		// Token: 0x06002157 RID: 8535 RVA: 0x00067874 File Offset: 0x00065A74
		private bool IsInGeneratedCode(Location location, Compilation compilation, CancellationToken cancellationToken)
		{
			if (this.TreatAllCodeAsNonGeneratedCode || !location.IsInSource)
			{
				return false;
			}
			if (this.IsGeneratedOrHiddenCodeLocation(location.SourceTree, location.SourceSpan))
			{
				return true;
			}
			if (this._lazyGeneratedCodeAttribute != null)
			{
				ImmutableHashSet<ISymbol> immutableHashSet = this.<IsInGeneratedCode>g__getOrComputeGeneratedCodeSymbolsInTree|112_0(location.SourceTree, compilation, cancellationToken);
				if (immutableHashSet.Count > 0)
				{
					SemanticModel semanticModel = compilation.GetSemanticModel(location.SourceTree, false);
					for (SyntaxNode syntaxNode = location.SourceTree.GetRoot(cancellationToken).FindNode(location.SourceSpan, false, true); syntaxNode != null; syntaxNode = syntaxNode.Parent)
					{
						foreach (ISymbol symbol in semanticModel.GetDeclaredSymbolsForNode(syntaxNode, cancellationToken))
						{
							if (immutableHashSet.Contains(symbol))
							{
								return true;
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06002158 RID: 8536 RVA: 0x00067930 File Offset: 0x00065B30
		private bool IsAnalyzerSuppressedForTree(DiagnosticAnalyzer analyzer, SyntaxTree tree, [Nullable(2)] SyntaxTreeOptionsProvider options)
		{
			ImmutableHashSet<DiagnosticAnalyzer> orAdd;
			if (!this.SuppressedAnalyzersForTreeMap.TryGetValue(tree, ref orAdd))
			{
				orAdd = this.SuppressedAnalyzersForTreeMap.GetOrAdd(tree, this.ComputeSuppressedAnalyzersForTree(tree, options));
			}
			return orAdd.Contains(analyzer);
		}

		// Token: 0x06002159 RID: 8537 RVA: 0x0006796C File Offset: 0x00065B6C
		private ImmutableHashSet<DiagnosticAnalyzer> ComputeSuppressedAnalyzersForTree(SyntaxTree tree, [Nullable(2)] SyntaxTreeOptionsProvider options)
		{
			if (options == null)
			{
				return ImmutableHashSet<DiagnosticAnalyzer>.Empty;
			}
			ImmutableHashSet<DiagnosticAnalyzer>.Builder builder = null;
			foreach (DiagnosticAnalyzer diagnosticAnalyzer in this.UnsuppressedAnalyzers)
			{
				if (!this.NonConfigurableAnalyzers.Contains(diagnosticAnalyzer) && ((!this.SymbolStartAnalyzers.Contains(diagnosticAnalyzer) && !this.CompilationEndAnalyzers.Contains(diagnosticAnalyzer)) || this.ShouldSkipAnalysisOnGeneratedCode(diagnosticAnalyzer)))
				{
					ImmutableArray<DiagnosticDescriptor> supportedDiagnosticDescriptors = this.AnalyzerManager.GetSupportedDiagnosticDescriptors(diagnosticAnalyzer, this.AnalyzerExecutor);
					bool flag = false;
					foreach (DiagnosticDescriptor diagnosticDescriptor in supportedDiagnosticDescriptors)
					{
						ReportDiagnostic reportDiagnostic;
						options.TryGetGlobalDiagnosticValue(diagnosticDescriptor.Id, this.AnalyzerExecutor.CancellationToken, out reportDiagnostic);
						ReportDiagnostic reportDiagnostic2;
						if (options.TryGetDiagnosticValue(tree, diagnosticDescriptor.Id, this.AnalyzerExecutor.CancellationToken, out reportDiagnostic2))
						{
							reportDiagnostic = reportDiagnostic2;
						}
						if (reportDiagnostic != ReportDiagnostic.Suppress)
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						if (builder == null)
						{
							builder = ImmutableHashSet.CreateBuilder<DiagnosticAnalyzer>();
						}
						builder.Add(diagnosticAnalyzer);
					}
				}
			}
			if (builder == null)
			{
				return ImmutableHashSet<DiagnosticAnalyzer>.Empty;
			}
			return builder.ToImmutable();
		}

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x0600215A RID: 8538 RVA: 0x00067AA0 File Offset: 0x00065CA0
		public bool IsInitialized
		{
			get
			{
				return this._lazyInitializeTask != null;
			}
		}

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x0600215B RID: 8539 RVA: 0x00067AAB File Offset: 0x00065CAB
		public Task WhenInitializedTask
		{
			get
			{
				return this._lazyInitializeTask;
			}
		}

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x0600215C RID: 8540 RVA: 0x00067AB3 File Offset: 0x00065CB3
		public Task WhenCompletedTask
		{
			get
			{
				return this._lazyPrimaryTask;
			}
		}

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x0600215D RID: 8541 RVA: 0x00067ABB File Offset: 0x00065CBB
		internal ImmutableDictionary<DiagnosticAnalyzer, TimeSpan> AnalyzerExecutionTimes
		{
			get
			{
				return this.AnalyzerExecutor.AnalyzerExecutionTimes;
			}
		}

		// Token: 0x0600215E RID: 8542 RVA: 0x00067AC8 File Offset: 0x00065CC8
		internal TimeSpan ResetAnalyzerExecutionTime(DiagnosticAnalyzer analyzer)
		{
			return this.AnalyzerExecutor.ResetAnalyzerExecutionTime(analyzer);
		}

		// Token: 0x0600215F RID: 8543 RVA: 0x00067AD8 File Offset: 0x00065CD8
		[return: Nullable(new byte[] { 0, 0, 1, 0, 0, 1 })]
		private static ImmutableArray<ValueTuple<DiagnosticAnalyzer, ImmutableArray<ImmutableArray<SymbolAnalyzerAction>>>> MakeSymbolActionsByKind(in AnalyzerActions analyzerActions)
		{
			ArrayBuilder<ValueTuple<DiagnosticAnalyzer, ImmutableArray<ImmutableArray<SymbolAnalyzerAction>>>> instance = ArrayBuilder<ValueTuple<DiagnosticAnalyzer, ImmutableArray<ImmutableArray<SymbolAnalyzerAction>>>>.GetInstance();
			IEnumerable<IGrouping<DiagnosticAnalyzer, SymbolAnalyzerAction>> enumerable = Enumerable.GroupBy<SymbolAnalyzerAction, DiagnosticAnalyzer>(analyzerActions.SymbolActions, (SymbolAnalyzerAction action) => action.Analyzer);
			ArrayBuilder<ArrayBuilder<SymbolAnalyzerAction>> instance2 = ArrayBuilder<ArrayBuilder<SymbolAnalyzerAction>>.GetInstance();
			foreach (IGrouping<DiagnosticAnalyzer, SymbolAnalyzerAction> grouping in enumerable)
			{
				instance2.Clear();
				foreach (SymbolAnalyzerAction symbolAnalyzerAction in grouping)
				{
					foreach (int i in symbolAnalyzerAction.Kinds.Distinct<SymbolKind>(null))
					{
						if (i <= 100)
						{
							while (i >= instance2.Count)
							{
								instance2.Add(ArrayBuilder<SymbolAnalyzerAction>.GetInstance());
							}
							instance2[i].Add(symbolAnalyzerAction);
						}
					}
				}
				ImmutableArray<ImmutableArray<SymbolAnalyzerAction>> immutableArray = Enumerable.Select<ArrayBuilder<SymbolAnalyzerAction>, ImmutableArray<SymbolAnalyzerAction>>(instance2, (ArrayBuilder<SymbolAnalyzerAction> a) => a.ToImmutableAndFree()).ToImmutableArray<ImmutableArray<SymbolAnalyzerAction>>();
				instance.Add(new ValueTuple<DiagnosticAnalyzer, ImmutableArray<ImmutableArray<SymbolAnalyzerAction>>>(grouping.Key, immutableArray));
			}
			instance2.Free();
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06002160 RID: 8544 RVA: 0x00067C38 File Offset: 0x00065E38
		[NullableContext(0)]
		[return: Nullable(new byte[] { 0, 0, 1, 0, 1 })]
		private static ImmutableArray<ValueTuple<DiagnosticAnalyzer, ImmutableArray<TAnalyzerAction>>> MakeActionsByAnalyzer<TAnalyzerAction>([Nullable(new byte[] { 0, 1 })] in ImmutableArray<TAnalyzerAction> analyzerActions) where TAnalyzerAction : AnalyzerAction
		{
			ArrayBuilder<ValueTuple<DiagnosticAnalyzer, ImmutableArray<TAnalyzerAction>>> instance = ArrayBuilder<ValueTuple<DiagnosticAnalyzer, ImmutableArray<TAnalyzerAction>>>.GetInstance();
			foreach (IGrouping<DiagnosticAnalyzer, TAnalyzerAction> grouping in Enumerable.GroupBy<TAnalyzerAction, DiagnosticAnalyzer>(analyzerActions, (TAnalyzerAction action) => action.Analyzer))
			{
				instance.Add(new ValueTuple<DiagnosticAnalyzer, ImmutableArray<TAnalyzerAction>>(grouping.Key, grouping.ToImmutableArray<TAnalyzerAction>()));
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06002161 RID: 8545 RVA: 0x00067CCC File Offset: 0x00065ECC
		private static ImmutableHashSet<DiagnosticAnalyzer> MakeCompilationEndAnalyzers([Nullable(new byte[] { 0, 0, 1, 0, 1 })] ImmutableArray<ValueTuple<DiagnosticAnalyzer, ImmutableArray<CompilationAnalyzerAction>>> compilationEndActionsByAnalyzer)
		{
			ImmutableHashSet<DiagnosticAnalyzer>.Builder builder = ImmutableHashSet.CreateBuilder<DiagnosticAnalyzer>();
			foreach (ValueTuple<DiagnosticAnalyzer, ImmutableArray<CompilationAnalyzerAction>> valueTuple in compilationEndActionsByAnalyzer)
			{
				DiagnosticAnalyzer item = valueTuple.Item1;
				builder.Add(item);
			}
			return builder.ToImmutable();
		}

		// Token: 0x06002162 RID: 8546 RVA: 0x00067D10 File Offset: 0x00065F10
		private Task ProcessCompilationEventsAsync(AnalysisScope analysisScope, [Nullable(2)] AnalysisState analysisState, bool prePopulatedEventQueue, CancellationToken cancellationToken)
		{
			AnalyzerDriver.<ProcessCompilationEventsAsync>d__127 <ProcessCompilationEventsAsync>d__;
			<ProcessCompilationEventsAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ProcessCompilationEventsAsync>d__.<>4__this = this;
			<ProcessCompilationEventsAsync>d__.analysisScope = analysisScope;
			<ProcessCompilationEventsAsync>d__.analysisState = analysisState;
			<ProcessCompilationEventsAsync>d__.prePopulatedEventQueue = prePopulatedEventQueue;
			<ProcessCompilationEventsAsync>d__.cancellationToken = cancellationToken;
			<ProcessCompilationEventsAsync>d__.<>1__state = -1;
			<ProcessCompilationEventsAsync>d__.<>t__builder.Start<AnalyzerDriver.<ProcessCompilationEventsAsync>d__127>(ref <ProcessCompilationEventsAsync>d__);
			return <ProcessCompilationEventsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06002163 RID: 8547 RVA: 0x00067D74 File Offset: 0x00065F74
		[return: Nullable(new byte[] { 1, 2 })]
		private Task<CompilationCompletedEvent> ProcessCompilationEventsCoreAsync(AnalysisScope analysisScope, [Nullable(2)] AnalysisState analysisState, bool prePopulatedEventQueue, CancellationToken cancellationToken)
		{
			AnalyzerDriver.<ProcessCompilationEventsCoreAsync>d__128 <ProcessCompilationEventsCoreAsync>d__;
			<ProcessCompilationEventsCoreAsync>d__.<>t__builder = AsyncTaskMethodBuilder<CompilationCompletedEvent>.Create();
			<ProcessCompilationEventsCoreAsync>d__.<>4__this = this;
			<ProcessCompilationEventsCoreAsync>d__.analysisScope = analysisScope;
			<ProcessCompilationEventsCoreAsync>d__.analysisState = analysisState;
			<ProcessCompilationEventsCoreAsync>d__.prePopulatedEventQueue = prePopulatedEventQueue;
			<ProcessCompilationEventsCoreAsync>d__.cancellationToken = cancellationToken;
			<ProcessCompilationEventsCoreAsync>d__.<>1__state = -1;
			<ProcessCompilationEventsCoreAsync>d__.<>t__builder.Start<AnalyzerDriver.<ProcessCompilationEventsCoreAsync>d__128>(ref <ProcessCompilationEventsCoreAsync>d__);
			return <ProcessCompilationEventsCoreAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06002164 RID: 8548 RVA: 0x00067DD8 File Offset: 0x00065FD8
		private Task ProcessEventAsync(CompilationEvent e, AnalysisScope analysisScope, [Nullable(2)] AnalysisState analysisState, CancellationToken cancellationToken)
		{
			AnalyzerDriver.<ProcessEventAsync>d__129 <ProcessEventAsync>d__;
			<ProcessEventAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ProcessEventAsync>d__.<>4__this = this;
			<ProcessEventAsync>d__.e = e;
			<ProcessEventAsync>d__.analysisScope = analysisScope;
			<ProcessEventAsync>d__.analysisState = analysisState;
			<ProcessEventAsync>d__.cancellationToken = cancellationToken;
			<ProcessEventAsync>d__.<>1__state = -1;
			<ProcessEventAsync>d__.<>t__builder.Start<AnalyzerDriver.<ProcessEventAsync>d__129>(ref <ProcessEventAsync>d__);
			return <ProcessEventAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06002165 RID: 8549 RVA: 0x00067E3C File Offset: 0x0006603C
		private Task OnEventProcessedCoreAsync(CompilationEvent compilationEvent, [Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticAnalyzer> processedAnalyzers, [Nullable(2)] AnalysisState analysisState, CancellationToken cancellationToken)
		{
			AnalyzerDriver.<OnEventProcessedCoreAsync>d__130 <OnEventProcessedCoreAsync>d__;
			<OnEventProcessedCoreAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<OnEventProcessedCoreAsync>d__.<>4__this = this;
			<OnEventProcessedCoreAsync>d__.compilationEvent = compilationEvent;
			<OnEventProcessedCoreAsync>d__.processedAnalyzers = processedAnalyzers;
			<OnEventProcessedCoreAsync>d__.analysisState = analysisState;
			<OnEventProcessedCoreAsync>d__.cancellationToken = cancellationToken;
			<OnEventProcessedCoreAsync>d__.<>1__state = -1;
			<OnEventProcessedCoreAsync>d__.<>t__builder.Start<AnalyzerDriver.<OnEventProcessedCoreAsync>d__130>(ref <OnEventProcessedCoreAsync>d__);
			return <OnEventProcessedCoreAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06002166 RID: 8550 RVA: 0x00067EA0 File Offset: 0x000660A0
		[return: Nullable(new byte[] { 0, 1 })]
		private ValueTask<AnalyzerDriver.EventProcessedState> TryProcessEventCoreAsync(CompilationEvent compilationEvent, AnalysisScope analysisScope, [Nullable(2)] AnalysisState analysisState, CancellationToken cancellationToken)
		{
			AnalyzerDriver.<TryProcessEventCoreAsync>d__131 <TryProcessEventCoreAsync>d__;
			<TryProcessEventCoreAsync>d__.<>t__builder = AsyncValueTaskMethodBuilder<AnalyzerDriver.EventProcessedState>.Create();
			<TryProcessEventCoreAsync>d__.<>4__this = this;
			<TryProcessEventCoreAsync>d__.compilationEvent = compilationEvent;
			<TryProcessEventCoreAsync>d__.analysisScope = analysisScope;
			<TryProcessEventCoreAsync>d__.analysisState = analysisState;
			<TryProcessEventCoreAsync>d__.cancellationToken = cancellationToken;
			<TryProcessEventCoreAsync>d__.<>1__state = -1;
			<TryProcessEventCoreAsync>d__.<>t__builder.Start<AnalyzerDriver.<TryProcessEventCoreAsync>d__131>(ref <TryProcessEventCoreAsync>d__);
			return <TryProcessEventCoreAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06002167 RID: 8551 RVA: 0x00067F04 File Offset: 0x00066104
		[return: Nullable(new byte[] { 0, 1 })]
		private ValueTask<AnalyzerDriver.EventProcessedState> TryProcessSymbolDeclaredAsync(SymbolDeclaredCompilationEvent symbolEvent, AnalysisScope analysisScope, [Nullable(2)] AnalysisState analysisState, CancellationToken cancellationToken)
		{
			AnalyzerDriver.<TryProcessSymbolDeclaredAsync>d__132 <TryProcessSymbolDeclaredAsync>d__;
			<TryProcessSymbolDeclaredAsync>d__.<>t__builder = AsyncValueTaskMethodBuilder<AnalyzerDriver.EventProcessedState>.Create();
			<TryProcessSymbolDeclaredAsync>d__.<>4__this = this;
			<TryProcessSymbolDeclaredAsync>d__.symbolEvent = symbolEvent;
			<TryProcessSymbolDeclaredAsync>d__.analysisScope = analysisScope;
			<TryProcessSymbolDeclaredAsync>d__.analysisState = analysisState;
			<TryProcessSymbolDeclaredAsync>d__.cancellationToken = cancellationToken;
			<TryProcessSymbolDeclaredAsync>d__.<>1__state = -1;
			<TryProcessSymbolDeclaredAsync>d__.<>t__builder.Start<AnalyzerDriver.<TryProcessSymbolDeclaredAsync>d__132>(ref <TryProcessSymbolDeclaredAsync>d__);
			return <TryProcessSymbolDeclaredAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06002168 RID: 8552 RVA: 0x00067F68 File Offset: 0x00066168
		private bool TryExecuteSymbolActions(SymbolDeclaredCompilationEvent symbolEvent, AnalysisScope analysisScope, [Nullable(2)] AnalysisState analysisState, bool isGeneratedCodeSymbol, CancellationToken cancellationToken)
		{
			ISymbol symbol = symbolEvent.Symbol;
			if (!analysisScope.ShouldAnalyze(symbol))
			{
				return true;
			}
			PooledHashSet<DiagnosticAnalyzer> pooledHashSet = ((analysisState != null) ? PooledHashSet<DiagnosticAnalyzer>.GetInstance() : null);
			bool flag2;
			try
			{
				bool flag = true;
				foreach (ValueTuple<DiagnosticAnalyzer, ImmutableArray<ImmutableArray<SymbolAnalyzerAction>>> valueTuple in this._lazySymbolActionsByKind)
				{
					DiagnosticAnalyzer item = valueTuple.Item1;
					ImmutableArray<ImmutableArray<SymbolAnalyzerAction>> item2 = valueTuple.Item2;
					if (analysisScope.Contains(item) && symbol.Kind < (SymbolKind)item2.Length)
					{
						if (!this.AnalyzerExecutor.TryExecuteSymbolActions(item2[(int)symbol.Kind], item, symbolEvent, AnalyzerDriver.s_getTopmostNodeForAnalysis, analysisScope, analysisState, isGeneratedCodeSymbol))
						{
							flag = false;
						}
						if (pooledHashSet != null)
						{
							pooledHashSet.Add(item);
						}
					}
				}
				if (analysisState != null)
				{
					analysisState.MarkSymbolCompleteForUnprocessedAnalyzers(symbol, analysisScope, pooledHashSet);
				}
				flag2 = flag;
			}
			finally
			{
				if (pooledHashSet != null)
				{
					pooledHashSet.Free();
				}
			}
			return flag2;
		}

		// Token: 0x06002169 RID: 8553 RVA: 0x0006803C File Offset: 0x0006623C
		private bool TryExecuteSymbolEndActions(in AnalyzerActions perSymbolActions, SymbolDeclaredCompilationEvent symbolEvent, AnalysisScope analysisScope, [Nullable(2)] AnalysisState analysisState, [Nullable(new byte[] { 0, 1 })] out ImmutableArray<DiagnosticAnalyzer> subsetProcessedAnalyzers)
		{
			ISymbol symbol = symbolEvent.Symbol;
			ImmutableArray<SymbolEndAnalyzerAction> symbolEndActions = perSymbolActions.SymbolEndActions;
			if (!analysisScope.ShouldAnalyze(symbol) || symbolEndActions.IsEmpty)
			{
				if (analysisState != null)
				{
					analysisState.MarkSymbolEndAnalysisComplete(symbol, analysisScope.Analyzers);
				}
				subsetProcessedAnalyzers = ImmutableArray<DiagnosticAnalyzer>.Empty;
				return true;
			}
			bool flag = true;
			ArrayBuilder<DiagnosticAnalyzer> instance = ArrayBuilder<DiagnosticAnalyzer>.GetInstance();
			PooledHashSet<DiagnosticAnalyzer> instance2 = PooledHashSet<DiagnosticAnalyzer>.GetInstance();
			bool flag2;
			try
			{
				foreach (IGrouping<DiagnosticAnalyzer, SymbolEndAnalyzerAction> grouping in Enumerable.GroupBy<SymbolEndAnalyzerAction, DiagnosticAnalyzer>(symbolEndActions, (SymbolEndAnalyzerAction a) => a.Analyzer))
				{
					DiagnosticAnalyzer key = grouping.Key;
					if (analysisScope.Contains(key))
					{
						instance2.Add(key);
						ImmutableArray<SymbolEndAnalyzerAction> immutableArray = grouping.ToImmutableArrayOrEmpty<SymbolEndAnalyzerAction>();
						if (!immutableArray.IsEmpty && !this.AnalyzerExecutor.TryExecuteSymbolEndActions(immutableArray, key, symbolEvent, AnalyzerDriver.s_getTopmostNodeForAnalysis, analysisState))
						{
							flag = false;
						}
						else
						{
							this.AnalyzerExecutor.MarkSymbolEndAnalysisComplete(symbol, key, analysisState);
							instance.Add(key);
						}
					}
				}
				if (instance2.Count < analysisScope.Analyzers.Length)
				{
					foreach (DiagnosticAnalyzer diagnosticAnalyzer in analysisScope.Analyzers)
					{
						if (!instance2.Contains(diagnosticAnalyzer))
						{
							this.AnalyzerExecutor.MarkSymbolEndAnalysisComplete(symbol, diagnosticAnalyzer, analysisState);
							instance.Add(diagnosticAnalyzer);
						}
					}
				}
				if (!flag)
				{
					subsetProcessedAnalyzers = instance.ToImmutable();
					flag2 = false;
				}
				else
				{
					subsetProcessedAnalyzers = ImmutableArray<DiagnosticAnalyzer>.Empty;
					flag2 = true;
				}
			}
			finally
			{
				instance2.Free();
				instance.Free();
			}
			return flag2;
		}

		// Token: 0x0600216A RID: 8554 RVA: 0x00068220 File Offset: 0x00066420
		private static SyntaxNode GetTopmostNodeForAnalysis(ISymbol symbol, SyntaxReference syntaxReference, Compilation compilation, CancellationToken cancellationToken)
		{
			return compilation.GetSemanticModel(syntaxReference.SyntaxTree, false).GetTopmostNodeForDiagnosticAnalysis(symbol, syntaxReference.GetSyntax(cancellationToken));
		}

		// Token: 0x0600216B RID: 8555
		protected abstract bool TryExecuteDeclaringReferenceActions(SymbolDeclaredCompilationEvent symbolEvent, AnalysisScope analysisScope, [Nullable(2)] AnalysisState analysisState, bool isGeneratedCodeSymbol, AnalyzerDriver.IGroupedAnalyzerActions additionalPerSymbolActions, CancellationToken cancellationToken);

		// Token: 0x0600216C RID: 8556 RVA: 0x0006823C File Offset: 0x0006643C
		private bool TryProcessCompilationUnitCompleted(CompilationUnitCompletedEvent completedEvent, AnalysisScope analysisScope, [Nullable(2)] AnalysisState analysisState, CancellationToken cancellationToken)
		{
			SemanticModel semanticModel = this.SemanticModelProvider.GetSemanticModel(completedEvent.CompilationUnit, completedEvent.Compilation, false);
			if (!analysisScope.ShouldAnalyze(semanticModel.SyntaxTree))
			{
				return true;
			}
			bool flag = this.IsGeneratedCode(semanticModel.SyntaxTree);
			if (flag && this.DoNotAnalyzeGeneratedCode)
			{
				if (analysisState != null)
				{
					analysisState.MarkEventComplete(completedEvent, analysisScope.Analyzers);
				}
				return true;
			}
			PooledHashSet<DiagnosticAnalyzer> pooledHashSet = ((analysisState != null) ? PooledHashSet<DiagnosticAnalyzer>.GetInstance() : null);
			bool flag3;
			try
			{
				bool flag2 = true;
				foreach (ValueTuple<DiagnosticAnalyzer, ImmutableArray<SemanticModelAnalyzerAction>> valueTuple in this._lazySemanticModelActions)
				{
					DiagnosticAnalyzer item = valueTuple.Item1;
					ImmutableArray<SemanticModelAnalyzerAction> item2 = valueTuple.Item2;
					if (analysisScope.Contains(item))
					{
						if (!this.AnalyzerExecutor.TryExecuteSemanticModelActions(item2, item, semanticModel, completedEvent, analysisScope, analysisState, flag))
						{
							flag2 = false;
						}
						if (pooledHashSet != null)
						{
							pooledHashSet.Add(item);
						}
					}
				}
				if (analysisState != null)
				{
					analysisState.MarkEventCompleteForUnprocessedAnalyzers(completedEvent, analysisScope, pooledHashSet);
				}
				flag3 = flag2;
			}
			finally
			{
				if (pooledHashSet != null)
				{
					pooledHashSet.Free();
				}
			}
			return flag3;
		}

		// Token: 0x0600216D RID: 8557 RVA: 0x00068338 File Offset: 0x00066538
		private bool TryProcessCompilationStarted(CompilationStartedEvent startedEvent, AnalysisScope analysisScope, [Nullable(2)] AnalysisState analysisState)
		{
			return this.TryExecuteCompilationActions(this._lazyCompilationActions, startedEvent, analysisScope, analysisState);
		}

		// Token: 0x0600216E RID: 8558 RVA: 0x00068349 File Offset: 0x00066549
		private bool TryProcessCompilationCompleted(CompilationCompletedEvent endEvent, AnalysisScope analysisScope, [Nullable(2)] AnalysisState analysisState)
		{
			return this.TryExecuteCompilationActions(this._lazyCompilationEndActions, endEvent, analysisScope, analysisState);
		}

		// Token: 0x0600216F RID: 8559 RVA: 0x0006835C File Offset: 0x0006655C
		private bool TryExecuteCompilationActions([Nullable(new byte[] { 0, 0, 1, 0, 1 })] ImmutableArray<ValueTuple<DiagnosticAnalyzer, ImmutableArray<CompilationAnalyzerAction>>> compilationActionsMap, CompilationEvent compilationEvent, AnalysisScope analysisScope, [Nullable(2)] AnalysisState analysisState)
		{
			PooledHashSet<DiagnosticAnalyzer> pooledHashSet = ((analysisState != null) ? PooledHashSet<DiagnosticAnalyzer>.GetInstance() : null);
			bool flag2;
			try
			{
				bool flag = true;
				foreach (ValueTuple<DiagnosticAnalyzer, ImmutableArray<CompilationAnalyzerAction>> valueTuple in compilationActionsMap)
				{
					DiagnosticAnalyzer item = valueTuple.Item1;
					ImmutableArray<CompilationAnalyzerAction> item2 = valueTuple.Item2;
					if (analysisScope.Contains(item))
					{
						if (!this.AnalyzerExecutor.TryExecuteCompilationActions(item2, item, compilationEvent, analysisScope, analysisState))
						{
							flag = false;
						}
						if (pooledHashSet != null)
						{
							pooledHashSet.Add(item);
						}
					}
				}
				if (analysisState != null)
				{
					analysisState.MarkEventCompleteForUnprocessedAnalyzers(compilationEvent, analysisScope, pooledHashSet);
				}
				flag2 = flag;
			}
			finally
			{
				if (pooledHashSet != null)
				{
					pooledHashSet.Free();
				}
			}
			return flag2;
		}

		// Token: 0x06002170 RID: 8560 RVA: 0x000683FC File Offset: 0x000665FC
		internal static Action<Diagnostic> GetDiagnosticSink(Action<Diagnostic> addDiagnosticCore, Compilation compilation, [Nullable(2)] AnalyzerOptions analyzerOptions, SeverityFilter severityFilter, CancellationToken cancellationToken)
		{
			return delegate(Diagnostic diagnostic)
			{
				Diagnostic filteredDiagnostic = AnalyzerDriver.GetFilteredDiagnostic(diagnostic, compilation, analyzerOptions, severityFilter, cancellationToken);
				if (filteredDiagnostic != null)
				{
					addDiagnosticCore.Invoke(filteredDiagnostic);
				}
			};
		}

		// Token: 0x06002171 RID: 8561 RVA: 0x00068432 File Offset: 0x00066632
		internal static Action<Diagnostic, DiagnosticAnalyzer, bool> GetDiagnosticSink(Action<Diagnostic, DiagnosticAnalyzer, bool> addLocalDiagnosticCore, Compilation compilation, [Nullable(2)] AnalyzerOptions analyzerOptions, SeverityFilter severityFilter, CancellationToken cancellationToken)
		{
			return delegate(Diagnostic diagnostic, DiagnosticAnalyzer analyzer, bool isSyntaxDiagnostic)
			{
				Diagnostic filteredDiagnostic = AnalyzerDriver.GetFilteredDiagnostic(diagnostic, compilation, analyzerOptions, severityFilter, cancellationToken);
				if (filteredDiagnostic != null)
				{
					addLocalDiagnosticCore.Invoke(filteredDiagnostic, analyzer, isSyntaxDiagnostic);
				}
			};
		}

		// Token: 0x06002172 RID: 8562 RVA: 0x00068468 File Offset: 0x00066668
		internal static Action<Diagnostic, DiagnosticAnalyzer> GetDiagnosticSink(Action<Diagnostic, DiagnosticAnalyzer> addDiagnosticCore, Compilation compilation, [Nullable(2)] AnalyzerOptions analyzerOptions, SeverityFilter severityFilter, CancellationToken cancellationToken)
		{
			return delegate(Diagnostic diagnostic, DiagnosticAnalyzer analyzer)
			{
				Diagnostic filteredDiagnostic = AnalyzerDriver.GetFilteredDiagnostic(diagnostic, compilation, analyzerOptions, severityFilter, cancellationToken);
				if (filteredDiagnostic != null)
				{
					addDiagnosticCore.Invoke(filteredDiagnostic, analyzer);
				}
			};
		}

		// Token: 0x06002173 RID: 8563 RVA: 0x000684A0 File Offset: 0x000666A0
		[return: Nullable(2)]
		private static Diagnostic GetFilteredDiagnostic(Diagnostic diagnostic, Compilation compilation, [Nullable(2)] AnalyzerOptions analyzerOptions, SeverityFilter severityFilter, CancellationToken cancellationToken)
		{
			AnalyzerDriver.<>c__DisplayClass144_0 CS$<>8__locals1;
			CS$<>8__locals1.analyzerOptions = analyzerOptions;
			CS$<>8__locals1.compilation = compilation;
			CS$<>8__locals1.cancellationToken = cancellationToken;
			CS$<>8__locals1.severityFilter = severityFilter;
			return AnalyzerDriver.<GetFilteredDiagnostic>g__applyFurtherFiltering|144_0(CS$<>8__locals1.compilation.Options.FilterDiagnostic(diagnostic, CS$<>8__locals1.cancellationToken), ref CS$<>8__locals1);
		}

		// Token: 0x06002174 RID: 8564 RVA: 0x000684EC File Offset: 0x000666EC
		[return: TupleElementNames(new string[] { "actions", "unsuppressedAnalyzers" })]
		[return: Nullable(new byte[] { 1, 0, 1, 1 })]
		private static Task<ValueTuple<AnalyzerActions, ImmutableHashSet<DiagnosticAnalyzer>>> GetAnalyzerActionsAsync([Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticAnalyzer> analyzers, AnalyzerManager analyzerManager, AnalyzerExecutor analyzerExecutor, SeverityFilter severityFilter)
		{
			AnalyzerDriver.<GetAnalyzerActionsAsync>d__145 <GetAnalyzerActionsAsync>d__;
			<GetAnalyzerActionsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<ValueTuple<Microsoft.CodeAnalysis.Diagnostics.AnalyzerActions, ImmutableHashSet<DiagnosticAnalyzer>>>.Create();
			<GetAnalyzerActionsAsync>d__.analyzers = analyzers;
			<GetAnalyzerActionsAsync>d__.analyzerManager = analyzerManager;
			<GetAnalyzerActionsAsync>d__.analyzerExecutor = analyzerExecutor;
			<GetAnalyzerActionsAsync>d__.severityFilter = severityFilter;
			<GetAnalyzerActionsAsync>d__.<>1__state = -1;
			<GetAnalyzerActionsAsync>d__.<>t__builder.Start<AnalyzerDriver.<GetAnalyzerActionsAsync>d__145>(ref <GetAnalyzerActionsAsync>d__);
			return <GetAnalyzerActionsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06002175 RID: 8565 RVA: 0x00068548 File Offset: 0x00066748
		public bool HasSymbolStartedActions(AnalysisScope analysisScope)
		{
			if (this.AnalyzerActions.SymbolStartActionsCount == 0)
			{
				return false;
			}
			if (analysisScope.Analyzers.Length == this.Analyzers.Length)
			{
				return true;
			}
			if (analysisScope.Analyzers.Length == 1)
			{
				DiagnosticAnalyzer diagnosticAnalyzer = analysisScope.Analyzers[0];
				ImmutableArray<SymbolStartAnalyzerAction>.Enumerator enumerator = this.AnalyzerActions.SymbolStartActions.GetEnumerator();
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Analyzer == diagnosticAnalyzer)
					{
						return true;
					}
				}
				return false;
			}
			PooledHashSet<DiagnosticAnalyzer> instance = PooledHashSet<DiagnosticAnalyzer>.GetInstance();
			bool flag;
			try
			{
				foreach (SymbolStartAnalyzerAction symbolStartAnalyzerAction in this.AnalyzerActions.SymbolStartActions)
				{
					instance.Add(symbolStartAnalyzerAction.Analyzer);
				}
				foreach (DiagnosticAnalyzer diagnosticAnalyzer2 in analysisScope.Analyzers)
				{
					if (instance.Contains(diagnosticAnalyzer2))
					{
						return true;
					}
				}
				flag = false;
			}
			finally
			{
				instance.Free();
			}
			return flag;
		}

		// Token: 0x06002176 RID: 8566 RVA: 0x00068668 File Offset: 0x00066868
		[return: Nullable(new byte[] { 0, 1 })]
		private ValueTask<AnalyzerDriver.IGroupedAnalyzerActions> GetPerSymbolAnalyzerActionsAsync(ISymbol symbol, AnalysisScope analysisScope, [Nullable(2)] AnalysisState analysisState, CancellationToken cancellationToken)
		{
			AnalyzerDriver.<GetPerSymbolAnalyzerActionsAsync>d__147 <GetPerSymbolAnalyzerActionsAsync>d__;
			<GetPerSymbolAnalyzerActionsAsync>d__.<>t__builder = AsyncValueTaskMethodBuilder<AnalyzerDriver.IGroupedAnalyzerActions>.Create();
			<GetPerSymbolAnalyzerActionsAsync>d__.<>4__this = this;
			<GetPerSymbolAnalyzerActionsAsync>d__.symbol = symbol;
			<GetPerSymbolAnalyzerActionsAsync>d__.analysisScope = analysisScope;
			<GetPerSymbolAnalyzerActionsAsync>d__.analysisState = analysisState;
			<GetPerSymbolAnalyzerActionsAsync>d__.cancellationToken = cancellationToken;
			<GetPerSymbolAnalyzerActionsAsync>d__.<>1__state = -1;
			<GetPerSymbolAnalyzerActionsAsync>d__.<>t__builder.Start<AnalyzerDriver.<GetPerSymbolAnalyzerActionsAsync>d__147>(ref <GetPerSymbolAnalyzerActionsAsync>d__);
			return <GetPerSymbolAnalyzerActionsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06002177 RID: 8567 RVA: 0x000686CC File Offset: 0x000668CC
		[return: Nullable(new byte[] { 0, 1 })]
		private ValueTask<AnalyzerDriver.IGroupedAnalyzerActions> GetPerSymbolAnalyzerActionsAsync(ISymbol symbol, DiagnosticAnalyzer analyzer, [Nullable(2)] AnalysisState analysisState, CancellationToken cancellationToken)
		{
			AnalyzerDriver.<GetPerSymbolAnalyzerActionsAsync>d__148 <GetPerSymbolAnalyzerActionsAsync>d__;
			<GetPerSymbolAnalyzerActionsAsync>d__.<>t__builder = AsyncValueTaskMethodBuilder<AnalyzerDriver.IGroupedAnalyzerActions>.Create();
			<GetPerSymbolAnalyzerActionsAsync>d__.<>4__this = this;
			<GetPerSymbolAnalyzerActionsAsync>d__.symbol = symbol;
			<GetPerSymbolAnalyzerActionsAsync>d__.analyzer = analyzer;
			<GetPerSymbolAnalyzerActionsAsync>d__.analysisState = analysisState;
			<GetPerSymbolAnalyzerActionsAsync>d__.cancellationToken = cancellationToken;
			<GetPerSymbolAnalyzerActionsAsync>d__.<>1__state = -1;
			<GetPerSymbolAnalyzerActionsAsync>d__.<>t__builder.Start<AnalyzerDriver.<GetPerSymbolAnalyzerActionsAsync>d__148>(ref <GetPerSymbolAnalyzerActionsAsync>d__);
			return <GetPerSymbolAnalyzerActionsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06002178 RID: 8568 RVA: 0x00068730 File Offset: 0x00066930
		[return: Nullable(new byte[] { 1, 0, 1, 1 })]
		private static Task<ImmutableSegmentedDictionary<DiagnosticAnalyzer, SemaphoreSlim>> CreateAnalyzerGateMapAsync(ImmutableHashSet<DiagnosticAnalyzer> analyzers, AnalyzerManager analyzerManager, AnalyzerExecutor analyzerExecutor, SeverityFilter severityFilter)
		{
			AnalyzerDriver.<CreateAnalyzerGateMapAsync>d__149 <CreateAnalyzerGateMapAsync>d__;
			<CreateAnalyzerGateMapAsync>d__.<>t__builder = AsyncTaskMethodBuilder<ImmutableSegmentedDictionary<DiagnosticAnalyzer, SemaphoreSlim>>.Create();
			<CreateAnalyzerGateMapAsync>d__.analyzers = analyzers;
			<CreateAnalyzerGateMapAsync>d__.analyzerManager = analyzerManager;
			<CreateAnalyzerGateMapAsync>d__.analyzerExecutor = analyzerExecutor;
			<CreateAnalyzerGateMapAsync>d__.<>1__state = -1;
			<CreateAnalyzerGateMapAsync>d__.<>t__builder.Start<AnalyzerDriver.<CreateAnalyzerGateMapAsync>d__149>(ref <CreateAnalyzerGateMapAsync>d__);
			return <CreateAnalyzerGateMapAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06002179 RID: 8569 RVA: 0x00068784 File Offset: 0x00066984
		[return: Nullable(new byte[] { 1, 0, 1 })]
		private static Task<ImmutableSegmentedDictionary<DiagnosticAnalyzer, GeneratedCodeAnalysisFlags>> CreateGeneratedCodeAnalysisFlagsMapAsync(ImmutableHashSet<DiagnosticAnalyzer> analyzers, AnalyzerManager analyzerManager, AnalyzerExecutor analyzerExecutor, SeverityFilter severityFilter)
		{
			AnalyzerDriver.<CreateGeneratedCodeAnalysisFlagsMapAsync>d__150 <CreateGeneratedCodeAnalysisFlagsMapAsync>d__;
			<CreateGeneratedCodeAnalysisFlagsMapAsync>d__.<>t__builder = AsyncTaskMethodBuilder<ImmutableSegmentedDictionary<DiagnosticAnalyzer, GeneratedCodeAnalysisFlags>>.Create();
			<CreateGeneratedCodeAnalysisFlagsMapAsync>d__.analyzers = analyzers;
			<CreateGeneratedCodeAnalysisFlagsMapAsync>d__.analyzerManager = analyzerManager;
			<CreateGeneratedCodeAnalysisFlagsMapAsync>d__.analyzerExecutor = analyzerExecutor;
			<CreateGeneratedCodeAnalysisFlagsMapAsync>d__.<>1__state = -1;
			<CreateGeneratedCodeAnalysisFlagsMapAsync>d__.<>t__builder.Start<AnalyzerDriver.<CreateGeneratedCodeAnalysisFlagsMapAsync>d__150>(ref <CreateGeneratedCodeAnalysisFlagsMapAsync>d__);
			return <CreateGeneratedCodeAnalysisFlagsMapAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600217A RID: 8570 RVA: 0x000687D8 File Offset: 0x000669D8
		private bool IsGeneratedCodeSymbol(ISymbol symbol)
		{
			AnalyzerDriver.<>c__DisplayClass151_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.symbol = symbol;
			if (this.TreatAllCodeAsNonGeneratedCode)
			{
				return false;
			}
			bool flag;
			if (!this.IsGeneratedCodeSymbolMap.TryGetValue(CS$<>8__locals1.symbol, ref flag))
			{
				return this.IsGeneratedCodeSymbolMap.GetOrAdd(CS$<>8__locals1.symbol, this.<IsGeneratedCodeSymbol>g__computeIsGeneratedCodeSymbol|151_0(ref CS$<>8__locals1));
			}
			return flag;
		}

		// Token: 0x0600217B RID: 8571 RVA: 0x00068830 File Offset: 0x00066A30
		protected bool IsGeneratedCode(SyntaxTree tree)
		{
			AnalyzerDriver.<>c__DisplayClass152_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.tree = tree;
			if (this.TreatAllCodeAsNonGeneratedCode)
			{
				return false;
			}
			bool flag;
			if (!this.GeneratedCodeFilesMap.TryGetValue(CS$<>8__locals1.tree, ref flag))
			{
				flag = this.<IsGeneratedCode>g__computeIsGeneratedCode|152_0(ref CS$<>8__locals1);
				this.GeneratedCodeFilesMap.TryAdd(CS$<>8__locals1.tree, flag);
			}
			return flag;
		}

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x0600217C RID: 8572 RVA: 0x00068889 File Offset: 0x00066A89
		protected bool DoNotAnalyzeGeneratedCode
		{
			get
			{
				return this._lazyDoNotAnalyzeGeneratedCode.Value;
			}
		}

		// Token: 0x0600217D RID: 8573 RVA: 0x00068896 File Offset: 0x00066A96
		protected bool IsGeneratedOrHiddenCodeLocation(SyntaxTree syntaxTree, TextSpan span)
		{
			return this.IsGeneratedCode(syntaxTree) || this.IsHiddenSourceLocation(syntaxTree, span);
		}

		// Token: 0x0600217E RID: 8574 RVA: 0x000688AC File Offset: 0x00066AAC
		protected bool IsHiddenSourceLocation(SyntaxTree syntaxTree, TextSpan span)
		{
			return this.HasHiddenRegions(syntaxTree) && syntaxTree.IsHiddenPosition(span.Start, default(CancellationToken));
		}

		// Token: 0x0600217F RID: 8575 RVA: 0x000688DC File Offset: 0x00066ADC
		private bool HasHiddenRegions(SyntaxTree tree)
		{
			if (this._lazyTreesWithHiddenRegionsMap == null)
			{
				return false;
			}
			bool flag;
			if (!this._lazyTreesWithHiddenRegionsMap.TryGetValue(tree, ref flag))
			{
				flag = tree.HasHiddenRegions();
				this._lazyTreesWithHiddenRegionsMap.TryAdd(tree, flag);
			}
			return flag;
		}

		// Token: 0x06002180 RID: 8576 RVA: 0x0006891C File Offset: 0x00066B1C
		internal Task<AnalyzerActionCounts> GetAnalyzerActionCountsAsync(DiagnosticAnalyzer analyzer, CompilationOptions compilationOptions, CancellationToken cancellationToken)
		{
			AnalyzerDriver.<GetAnalyzerActionCountsAsync>d__158 <GetAnalyzerActionCountsAsync>d__;
			<GetAnalyzerActionCountsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<AnalyzerActionCounts>.Create();
			<GetAnalyzerActionCountsAsync>d__.<>4__this = this;
			<GetAnalyzerActionCountsAsync>d__.analyzer = analyzer;
			<GetAnalyzerActionCountsAsync>d__.compilationOptions = compilationOptions;
			<GetAnalyzerActionCountsAsync>d__.cancellationToken = cancellationToken;
			<GetAnalyzerActionCountsAsync>d__.<>1__state = -1;
			<GetAnalyzerActionCountsAsync>d__.<>t__builder.Start<AnalyzerDriver.<GetAnalyzerActionCountsAsync>d__158>(ref <GetAnalyzerActionCountsAsync>d__);
			return <GetAnalyzerActionCountsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06002181 RID: 8577 RVA: 0x00068977 File Offset: 0x00066B77
		internal static bool IsDiagnosticAnalyzerSuppressed(DiagnosticAnalyzer analyzer, CompilationOptions options, AnalyzerManager analyzerManager, AnalyzerExecutor analyzerExecutor, SeverityFilter severityFilter)
		{
			return analyzerManager.IsDiagnosticAnalyzerSuppressed(analyzer, options, AnalyzerDriver.s_IsCompilerAnalyzerFunc, analyzerExecutor, severityFilter);
		}

		// Token: 0x06002182 RID: 8578 RVA: 0x00068989 File Offset: 0x00066B89
		private static bool IsCompilerAnalyzer(DiagnosticAnalyzer analyzer)
		{
			return analyzer is CompilerDiagnosticAnalyzer;
		}

		// Token: 0x06002183 RID: 8579 RVA: 0x00068994 File Offset: 0x00066B94
		public void Dispose()
		{
			AsyncQueue<CompilationEvent> lazyCompilationEventQueue = this._lazyCompilationEventQueue;
			if (lazyCompilationEventQueue != null)
			{
				lazyCompilationEventQueue.TryComplete();
			}
			DiagnosticQueue lazyDiagnosticQueue = this._lazyDiagnosticQueue;
			if (lazyDiagnosticQueue != null)
			{
				lazyDiagnosticQueue.TryComplete();
			}
			if (this._lazyQueueRegistration == null)
			{
				return;
			}
			this._lazyQueueRegistration.GetValueOrDefault().Dispose();
		}

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x06002184 RID: 8580
		protected abstract AnalyzerDriver.IGroupedAnalyzerActions EmptyGroupedActions { get; }

		// Token: 0x06002185 RID: 8581
		protected abstract AnalyzerDriver.IGroupedAnalyzerActions CreateGroupedActions(DiagnosticAnalyzer analyzer, in AnalyzerActions analyzerActions);

		// Token: 0x0600218A RID: 8586 RVA: 0x00068A5C File Offset: 0x00066C5C
		[CompilerGenerated]
		private void <ApplyProgrammaticSuppressionsCore>g__executeSuppressionActions|107_0(IEnumerable<Diagnostic> reportedDiagnostics, bool concurrent)
		{
			AnalyzerDriver.<>c__DisplayClass107_0 CS$<>8__locals1 = new AnalyzerDriver.<>c__DisplayClass107_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.reportedDiagnostics = reportedDiagnostics;
			IEnumerable<DiagnosticSuppressor> enumerable = this.Analyzers.OfType<DiagnosticSuppressor>();
			if (concurrent)
			{
				ArrayBuilder<Task> instance = ArrayBuilder<Task>.GetInstance();
				try
				{
					using (IEnumerator<DiagnosticSuppressor> enumerator = enumerable.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							DiagnosticSuppressor suppressor = enumerator.Current;
							Task task = Task.Run(delegate
							{
								CS$<>8__locals1.<>4__this.AnalyzerExecutor.ExecuteSuppressionAction(suppressor, CS$<>8__locals1.<ApplyProgrammaticSuppressionsCore>g__getSuppressableDiagnostics|3(suppressor));
							}, this.AnalyzerExecutor.CancellationToken);
							instance.Add(task);
						}
					}
					Task.WaitAll(instance.ToArray(), this.AnalyzerExecutor.CancellationToken);
					return;
				}
				finally
				{
					instance.Free();
				}
			}
			foreach (DiagnosticSuppressor diagnosticSuppressor in enumerable)
			{
				this.AnalyzerExecutor.ExecuteSuppressionAction(diagnosticSuppressor, CS$<>8__locals1.<ApplyProgrammaticSuppressionsCore>g__getSuppressableDiagnostics|3(diagnosticSuppressor));
			}
		}

		// Token: 0x0600218B RID: 8587 RVA: 0x00068B80 File Offset: 0x00066D80
		[CompilerGenerated]
		internal static ImmutableDictionary<Diagnostic, ProgrammaticSuppressionInfo> <ApplyProgrammaticSuppressionsCore>g__createProgrammaticSuppressionsByDiagnosticMap|107_1(ConcurrentSet<Suppression> programmaticSuppressions)
		{
			PooledDictionary<Diagnostic, ImmutableHashSet<ValueTuple<string, LocalizableString>>.Builder> instance = PooledDictionary<Diagnostic, ImmutableHashSet<ValueTuple<string, LocalizableString>>.Builder>.GetInstance();
			foreach (Suppression suppression in programmaticSuppressions)
			{
				ImmutableHashSet<ValueTuple<string, LocalizableString>>.Builder builder;
				if (!instance.TryGetValue(suppression.SuppressedDiagnostic, ref builder))
				{
					builder = ImmutableHashSet.CreateBuilder<ValueTuple<string, LocalizableString>>();
					instance.Add(suppression.SuppressedDiagnostic, builder);
				}
				builder.Add(new ValueTuple<string, LocalizableString>(suppression.Descriptor.Id, suppression.Descriptor.Justification));
			}
			ImmutableDictionary<Diagnostic, ProgrammaticSuppressionInfo>.Builder builder2 = ImmutableDictionary.CreateBuilder<Diagnostic, ProgrammaticSuppressionInfo>();
			foreach (KeyValuePair<Diagnostic, ImmutableHashSet<ValueTuple<string, LocalizableString>>.Builder> keyValuePair in instance)
			{
				Diagnostic diagnostic;
				ImmutableHashSet<ValueTuple<string, LocalizableString>>.Builder builder3;
				keyValuePair.Deconstruct<Diagnostic, ImmutableHashSet<ValueTuple<string, LocalizableString>>.Builder>(out diagnostic, out builder3);
				Diagnostic diagnostic2 = diagnostic;
				ImmutableHashSet<ValueTuple<string, LocalizableString>>.Builder builder4 = builder3;
				builder2.Add(diagnostic2, new ProgrammaticSuppressionInfo(builder4.ToImmutable()));
			}
			return builder2.ToImmutable();
		}

		// Token: 0x0600218C RID: 8588 RVA: 0x00068C64 File Offset: 0x00066E64
		[CompilerGenerated]
		private ImmutableHashSet<ISymbol> <IsInGeneratedCode>g__getOrComputeGeneratedCodeSymbolsInTree|112_0(SyntaxTree tree, Compilation compilation, CancellationToken cancellationToken)
		{
			Dictionary<SyntaxTree, ImmutableHashSet<ISymbol>> dictionary = this.GeneratedCodeSymbolsForTreeMap;
			ImmutableHashSet<ISymbol> immutableHashSet;
			lock (dictionary)
			{
				if (this.GeneratedCodeSymbolsForTreeMap.TryGetValue(tree, ref immutableHashSet))
				{
					return immutableHashSet;
				}
			}
			immutableHashSet = AnalyzerDriver.<IsInGeneratedCode>g__computeGeneratedCodeSymbolsInTree|112_1(tree, compilation, this._lazyGeneratedCodeAttribute, cancellationToken);
			dictionary = this.GeneratedCodeSymbolsForTreeMap;
			lock (dictionary)
			{
				ImmutableHashSet<ISymbol> immutableHashSet2;
				if (!this.GeneratedCodeSymbolsForTreeMap.TryGetValue(tree, ref immutableHashSet2))
				{
					this.GeneratedCodeSymbolsForTreeMap.Add(tree, immutableHashSet);
				}
			}
			return immutableHashSet;
		}

		// Token: 0x0600218D RID: 8589 RVA: 0x00068D0C File Offset: 0x00066F0C
		[CompilerGenerated]
		internal static ImmutableHashSet<ISymbol> <IsInGeneratedCode>g__computeGeneratedCodeSymbolsInTree|112_1(SyntaxTree tree, Compilation compilation, INamedTypeSymbol generatedCodeAttribute, CancellationToken cancellationToken)
		{
			AnalyzerDriver.GeneratedCodeTokenWalker generatedCodeTokenWalker = new AnalyzerDriver.GeneratedCodeTokenWalker(cancellationToken);
			generatedCodeTokenWalker.Visit(tree.GetRoot(cancellationToken));
			if (!generatedCodeTokenWalker.HasGeneratedCodeIdentifier)
			{
				return ImmutableHashSet<ISymbol>.Empty;
			}
			SemanticModel semanticModel = compilation.GetSemanticModel(tree, false);
			TextSpan fullSpan = tree.GetRoot(cancellationToken).FullSpan;
			ArrayBuilder<DeclarationInfo> instance = ArrayBuilder<DeclarationInfo>.GetInstance();
			semanticModel.ComputeDeclarationsInSpan(fullSpan, true, instance, cancellationToken);
			ImmutableHashSet<ISymbol>.Builder builder = null;
			foreach (DeclarationInfo declarationInfo in instance)
			{
				ISymbol declaredSymbol = declarationInfo.DeclaredSymbol;
				if (declaredSymbol != null && GeneratedCodeUtilities.IsGeneratedSymbolWithGeneratedCodeAttribute(declaredSymbol, generatedCodeAttribute))
				{
					if (builder == null)
					{
						builder = ImmutableHashSet.CreateBuilder<ISymbol>();
					}
					builder.Add(declaredSymbol);
				}
			}
			instance.Free();
			if (builder == null)
			{
				return ImmutableHashSet<ISymbol>.Empty;
			}
			return builder.ToImmutable();
		}

		// Token: 0x0600218E RID: 8590 RVA: 0x00068DB8 File Offset: 0x00066FB8
		[NullableContext(2)]
		[CompilerGenerated]
		internal static Diagnostic <GetFilteredDiagnostic>g__applyFurtherFiltering|144_0(Diagnostic diagnostic, ref AnalyzerDriver.<>c__DisplayClass144_0 A_1)
		{
			SyntaxTree syntaxTree = ((diagnostic != null) ? diagnostic.Location.SourceTree : null);
			ReportDiagnostic reportDiagnostic;
			if (syntaxTree != null && A_1.analyzerOptions.TryGetSeverityFromBulkConfiguration(syntaxTree, A_1.compilation, diagnostic.Descriptor, A_1.cancellationToken, out reportDiagnostic))
			{
				diagnostic = diagnostic.WithReportDiagnostic(reportDiagnostic);
			}
			if (diagnostic != null && A_1.severityFilter.Contains(DiagnosticDescriptor.MapSeverityToReport(diagnostic.Severity)))
			{
				return null;
			}
			return diagnostic;
		}

		// Token: 0x0600218F RID: 8591 RVA: 0x00068E24 File Offset: 0x00067024
		[CompilerGenerated]
		[return: Nullable(new byte[] { 0, 1 })]
		private ValueTask<AnalyzerDriver.IGroupedAnalyzerActions> <GetPerSymbolAnalyzerActionsAsync>g__getAllActionsAsync|148_0(AnalyzerDriver driver, ISymbol symbol, DiagnosticAnalyzer analyzer, [Nullable(2)] AnalysisState analysisState, CancellationToken cancellationToken)
		{
			AnalyzerDriver.<<GetPerSymbolAnalyzerActionsAsync>g__getAllActionsAsync|148_0>d <<GetPerSymbolAnalyzerActionsAsync>g__getAllActionsAsync|148_0>d;
			<<GetPerSymbolAnalyzerActionsAsync>g__getAllActionsAsync|148_0>d.<>t__builder = AsyncValueTaskMethodBuilder<AnalyzerDriver.IGroupedAnalyzerActions>.Create();
			<<GetPerSymbolAnalyzerActionsAsync>g__getAllActionsAsync|148_0>d.<>4__this = this;
			<<GetPerSymbolAnalyzerActionsAsync>g__getAllActionsAsync|148_0>d.driver = driver;
			<<GetPerSymbolAnalyzerActionsAsync>g__getAllActionsAsync|148_0>d.symbol = symbol;
			<<GetPerSymbolAnalyzerActionsAsync>g__getAllActionsAsync|148_0>d.analyzer = analyzer;
			<<GetPerSymbolAnalyzerActionsAsync>g__getAllActionsAsync|148_0>d.analysisState = analysisState;
			<<GetPerSymbolAnalyzerActionsAsync>g__getAllActionsAsync|148_0>d.cancellationToken = cancellationToken;
			<<GetPerSymbolAnalyzerActionsAsync>g__getAllActionsAsync|148_0>d.<>1__state = -1;
			<<GetPerSymbolAnalyzerActionsAsync>g__getAllActionsAsync|148_0>d.<>t__builder.Start<AnalyzerDriver.<<GetPerSymbolAnalyzerActionsAsync>g__getAllActionsAsync|148_0>d>(ref <<GetPerSymbolAnalyzerActionsAsync>g__getAllActionsAsync|148_0>d);
			return <<GetPerSymbolAnalyzerActionsAsync>g__getAllActionsAsync|148_0>d.<>t__builder.Task;
		}

		// Token: 0x06002190 RID: 8592 RVA: 0x00068E94 File Offset: 0x00067094
		[CompilerGenerated]
		[return: Nullable(new byte[] { 0, 1 })]
		private ValueTask<AnalyzerDriver.IGroupedAnalyzerActions> <GetPerSymbolAnalyzerActionsAsync>g__getInheritedActionsAsync|148_1(AnalyzerDriver driver, ISymbol symbol, DiagnosticAnalyzer analyzer, [Nullable(2)] AnalysisState analysisState, CancellationToken cancellationToken)
		{
			AnalyzerDriver.<<GetPerSymbolAnalyzerActionsAsync>g__getInheritedActionsAsync|148_1>d <<GetPerSymbolAnalyzerActionsAsync>g__getInheritedActionsAsync|148_1>d;
			<<GetPerSymbolAnalyzerActionsAsync>g__getInheritedActionsAsync|148_1>d.<>t__builder = AsyncValueTaskMethodBuilder<AnalyzerDriver.IGroupedAnalyzerActions>.Create();
			<<GetPerSymbolAnalyzerActionsAsync>g__getInheritedActionsAsync|148_1>d.<>4__this = this;
			<<GetPerSymbolAnalyzerActionsAsync>g__getInheritedActionsAsync|148_1>d.driver = driver;
			<<GetPerSymbolAnalyzerActionsAsync>g__getInheritedActionsAsync|148_1>d.symbol = symbol;
			<<GetPerSymbolAnalyzerActionsAsync>g__getInheritedActionsAsync|148_1>d.analyzer = analyzer;
			<<GetPerSymbolAnalyzerActionsAsync>g__getInheritedActionsAsync|148_1>d.analysisState = analysisState;
			<<GetPerSymbolAnalyzerActionsAsync>g__getInheritedActionsAsync|148_1>d.cancellationToken = cancellationToken;
			<<GetPerSymbolAnalyzerActionsAsync>g__getInheritedActionsAsync|148_1>d.<>1__state = -1;
			<<GetPerSymbolAnalyzerActionsAsync>g__getInheritedActionsAsync|148_1>d.<>t__builder.Start<AnalyzerDriver.<<GetPerSymbolAnalyzerActionsAsync>g__getInheritedActionsAsync|148_1>d>(ref <<GetPerSymbolAnalyzerActionsAsync>g__getInheritedActionsAsync|148_1>d);
			return <<GetPerSymbolAnalyzerActionsAsync>g__getInheritedActionsAsync|148_1>d.<>t__builder.Task;
		}

		// Token: 0x06002191 RID: 8593 RVA: 0x00068F04 File Offset: 0x00067104
		[CompilerGenerated]
		[return: Nullable(0)]
		internal static ValueTask<AnalyzerActions> <GetPerSymbolAnalyzerActionsAsync>g__getSymbolActionsCoreAsync|148_2(AnalyzerDriver driver, ISymbol symbol, DiagnosticAnalyzer analyzer)
		{
			AnalyzerDriver.<<GetPerSymbolAnalyzerActionsAsync>g__getSymbolActionsCoreAsync|148_2>d <<GetPerSymbolAnalyzerActionsAsync>g__getSymbolActionsCoreAsync|148_2>d;
			<<GetPerSymbolAnalyzerActionsAsync>g__getSymbolActionsCoreAsync|148_2>d.<>t__builder = AsyncValueTaskMethodBuilder<Microsoft.CodeAnalysis.Diagnostics.AnalyzerActions>.Create();
			<<GetPerSymbolAnalyzerActionsAsync>g__getSymbolActionsCoreAsync|148_2>d.driver = driver;
			<<GetPerSymbolAnalyzerActionsAsync>g__getSymbolActionsCoreAsync|148_2>d.symbol = symbol;
			<<GetPerSymbolAnalyzerActionsAsync>g__getSymbolActionsCoreAsync|148_2>d.analyzer = analyzer;
			<<GetPerSymbolAnalyzerActionsAsync>g__getSymbolActionsCoreAsync|148_2>d.<>1__state = -1;
			<<GetPerSymbolAnalyzerActionsAsync>g__getSymbolActionsCoreAsync|148_2>d.<>t__builder.Start<AnalyzerDriver.<<GetPerSymbolAnalyzerActionsAsync>g__getSymbolActionsCoreAsync|148_2>d>(ref <<GetPerSymbolAnalyzerActionsAsync>g__getSymbolActionsCoreAsync|148_2>d);
			return <<GetPerSymbolAnalyzerActionsAsync>g__getSymbolActionsCoreAsync|148_2>d.<>t__builder.Task;
		}

		// Token: 0x06002192 RID: 8594 RVA: 0x00068F58 File Offset: 0x00067158
		[CompilerGenerated]
		private bool <IsGeneratedCodeSymbol>g__computeIsGeneratedCodeSymbol|151_0(ref AnalyzerDriver.<>c__DisplayClass151_0 A_1)
		{
			if (this._lazyGeneratedCodeAttribute != null && GeneratedCodeUtilities.IsGeneratedSymbolWithGeneratedCodeAttribute(A_1.symbol, this._lazyGeneratedCodeAttribute))
			{
				return true;
			}
			foreach (SyntaxReference syntaxReference in A_1.symbol.DeclaringSyntaxReferences)
			{
				if (!this.IsGeneratedOrHiddenCodeLocation(syntaxReference.SyntaxTree, syntaxReference.Span))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002193 RID: 8595 RVA: 0x00068FC0 File Offset: 0x000671C0
		[CompilerGenerated]
		private bool <IsGeneratedCode>g__computeIsGeneratedCode|152_0(ref AnalyzerDriver.<>c__DisplayClass152_0 A_1)
		{
			bool? isGeneratedCodeFromOptions = GeneratedCodeUtilities.GetIsGeneratedCodeFromOptions(this.AnalyzerExecutor.AnalyzerOptions.AnalyzerConfigOptionsProvider.GetOptions(A_1.tree));
			if (isGeneratedCodeFromOptions == null)
			{
				return this._isGeneratedCode.Invoke(A_1.tree, this.AnalyzerExecutor.CancellationToken);
			}
			return isGeneratedCodeFromOptions.GetValueOrDefault();
		}

		// Token: 0x04000E60 RID: 3680
		private const int MaxSymbolKind = 100;

		// Token: 0x04000E61 RID: 3681
		private static readonly Func<DiagnosticAnalyzer, bool> s_IsCompilerAnalyzerFunc = new Func<DiagnosticAnalyzer, bool>(AnalyzerDriver.IsCompilerAnalyzer);

		// Token: 0x04000E62 RID: 3682
		private static readonly Func<ISymbol, SyntaxReference, Compilation, CancellationToken, SyntaxNode> s_getTopmostNodeForAnalysis = new Func<ISymbol, SyntaxReference, Compilation, CancellationToken, SyntaxNode>(AnalyzerDriver.GetTopmostNodeForAnalysis);

		// Token: 0x04000E63 RID: 3683
		private readonly Func<SyntaxTree, CancellationToken, bool> _isGeneratedCode;

		// Token: 0x04000E64 RID: 3684
		[Nullable(2)]
		private readonly ConcurrentSet<Suppression> _programmaticSuppressions;

		// Token: 0x04000E65 RID: 3685
		[Nullable(new byte[] { 2, 1 })]
		private readonly ConcurrentSet<Diagnostic> _diagnosticsProcessedForProgrammaticSuppressions;

		// Token: 0x04000E66 RID: 3686
		private readonly bool _hasDiagnosticSuppressors;

		// Token: 0x04000E67 RID: 3687
		private readonly SeverityFilter _severityFilter;

		// Token: 0x04000E6A RID: 3690
		private CancellationTokenRegistration? _lazyQueueRegistration;

		// Token: 0x04000E6B RID: 3691
		[Nullable(2)]
		private AnalyzerExecutor _lazyAnalyzerExecutor;

		// Token: 0x04000E6C RID: 3692
		[Nullable(2)]
		private AnalyzerDriver.CompilationData _lazyCurrentCompilationData;

		// Token: 0x04000E6D RID: 3693
		[Nullable(new byte[] { 2, 1 })]
		private ImmutableHashSet<DiagnosticAnalyzer> _lazyUnsuppressedAnalyzers;

		// Token: 0x04000E6E RID: 3694
		[Nullable(new byte[] { 2, 0, 1, 1, 1 })]
		private ConcurrentDictionary<ValueTuple<INamespaceOrTypeSymbol, DiagnosticAnalyzer>, AnalyzerDriver.IGroupedAnalyzerActions> _lazyPerSymbolAnalyzerActionsCache;

		// Token: 0x04000E6F RID: 3695
		[Nullable(new byte[] { 0, 0, 1, 0, 0, 1 })]
		private ImmutableArray<ValueTuple<DiagnosticAnalyzer, ImmutableArray<ImmutableArray<SymbolAnalyzerAction>>>> _lazySymbolActionsByKind;

		// Token: 0x04000E70 RID: 3696
		[Nullable(new byte[] { 0, 0, 1, 0, 1 })]
		private ImmutableArray<ValueTuple<DiagnosticAnalyzer, ImmutableArray<SemanticModelAnalyzerAction>>> _lazySemanticModelActions;

		// Token: 0x04000E71 RID: 3697
		[Nullable(new byte[] { 0, 0, 1, 0, 1 })]
		private ImmutableArray<ValueTuple<DiagnosticAnalyzer, ImmutableArray<SyntaxTreeAnalyzerAction>>> _lazySyntaxTreeActions;

		// Token: 0x04000E72 RID: 3698
		[Nullable(new byte[] { 0, 0, 1, 0, 1 })]
		private ImmutableArray<ValueTuple<DiagnosticAnalyzer, ImmutableArray<AdditionalFileAnalyzerAction>>> _lazyAdditionalFileActions;

		// Token: 0x04000E73 RID: 3699
		[Nullable(new byte[] { 0, 0, 1, 0, 1 })]
		private ImmutableArray<ValueTuple<DiagnosticAnalyzer, ImmutableArray<CompilationAnalyzerAction>>> _lazyCompilationActions;

		// Token: 0x04000E74 RID: 3700
		[Nullable(new byte[] { 0, 0, 1, 0, 1 })]
		private ImmutableArray<ValueTuple<DiagnosticAnalyzer, ImmutableArray<CompilationAnalyzerAction>>> _lazyCompilationEndActions;

		// Token: 0x04000E75 RID: 3701
		[Nullable(new byte[] { 2, 1 })]
		private ImmutableHashSet<DiagnosticAnalyzer> _lazyCompilationEndAnalyzers;

		// Token: 0x04000E76 RID: 3702
		internal const GeneratedCodeAnalysisFlags DefaultGeneratedCodeAnalysisFlags = GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics;

		// Token: 0x04000E77 RID: 3703
		[Nullable(new byte[] { 0, 1, 1 })]
		private ImmutableSegmentedDictionary<DiagnosticAnalyzer, SemaphoreSlim> _lazyAnalyzerGateMap;

		// Token: 0x04000E78 RID: 3704
		[Nullable(new byte[] { 0, 1 })]
		private ImmutableSegmentedDictionary<DiagnosticAnalyzer, GeneratedCodeAnalysisFlags> _lazyGeneratedCodeAnalysisFlagsMap;

		// Token: 0x04000E79 RID: 3705
		private AnalyzerActions _lazyAnalyzerActions;

		// Token: 0x04000E7A RID: 3706
		[Nullable(new byte[] { 2, 1 })]
		private ImmutableHashSet<DiagnosticAnalyzer> _lazyNonConfigurableAnalyzers;

		// Token: 0x04000E7B RID: 3707
		[Nullable(new byte[] { 2, 1 })]
		private ImmutableHashSet<DiagnosticAnalyzer> _lazySymbolStartAnalyzers;

		// Token: 0x04000E7C RID: 3708
		private bool? _lazyTreatAllCodeAsNonGeneratedCode;

		// Token: 0x04000E7D RID: 3709
		private bool? _lazyDoNotAnalyzeGeneratedCode;

		// Token: 0x04000E7E RID: 3710
		[Nullable(new byte[] { 2, 1 })]
		private ConcurrentDictionary<SyntaxTree, bool> _lazyGeneratedCodeFilesMap;

		// Token: 0x04000E7F RID: 3711
		[Nullable(new byte[] { 2, 1, 1, 1 })]
		private Dictionary<SyntaxTree, ImmutableHashSet<ISymbol>> _lazyGeneratedCodeSymbolsForTreeMap;

		// Token: 0x04000E80 RID: 3712
		[Nullable(new byte[] { 2, 1, 1, 1 })]
		private ConcurrentDictionary<SyntaxTree, ImmutableHashSet<DiagnosticAnalyzer>> _lazySuppressedAnalyzersForTreeMap;

		// Token: 0x04000E81 RID: 3713
		[Nullable(new byte[] { 2, 1 })]
		private ConcurrentDictionary<ISymbol, bool> _lazyIsGeneratedCodeSymbolMap;

		// Token: 0x04000E82 RID: 3714
		[Nullable(new byte[] { 2, 1 })]
		private ConcurrentDictionary<SyntaxTree, bool> _lazyTreesWithHiddenRegionsMap;

		// Token: 0x04000E83 RID: 3715
		[Nullable(2)]
		private INamedTypeSymbol _lazyGeneratedCodeAttribute;

		// Token: 0x04000E84 RID: 3716
		[Nullable(2)]
		private Task _lazyInitializeTask;

		// Token: 0x04000E85 RID: 3717
		private bool _initializeSucceeded;

		// Token: 0x04000E86 RID: 3718
		[Nullable(2)]
		private Task _lazyPrimaryTask;

		// Token: 0x04000E87 RID: 3719
		private readonly int _workerCount = Environment.ProcessorCount;

		// Token: 0x04000E88 RID: 3720
		[Nullable(new byte[] { 2, 1 })]
		private AsyncQueue<CompilationEvent> _lazyCompilationEventQueue;

		// Token: 0x04000E89 RID: 3721
		[Nullable(2)]
		private DiagnosticQueue _lazyDiagnosticQueue;

		// Token: 0x02000693 RID: 1683
		[Nullable(0)]
		internal class CompilationData
		{
			// Token: 0x060039B1 RID: 14769 RVA: 0x000A0655 File Offset: 0x0009E855
			public CompilationData(Compilation compilation)
			{
				this.SemanticModelProvider = (CachingSemanticModelProvider)compilation.SemanticModelProvider;
				this.SuppressMessageAttributeState = new SuppressMessageAttributeState(compilation);
				this._declarationAnalysisDataMap = new Dictionary<SyntaxReference, AnalyzerDriver.DeclarationAnalysisData>();
			}

			// Token: 0x17000FE9 RID: 4073
			// (get) Token: 0x060039B2 RID: 14770 RVA: 0x000A0685 File Offset: 0x0009E885
			public CachingSemanticModelProvider SemanticModelProvider { get; }

			// Token: 0x17000FEA RID: 4074
			// (get) Token: 0x060039B3 RID: 14771 RVA: 0x000A068D File Offset: 0x0009E88D
			public SuppressMessageAttributeState SuppressMessageAttributeState { get; }

			// Token: 0x060039B4 RID: 14772 RVA: 0x000A0698 File Offset: 0x0009E898
			internal AnalyzerDriver.DeclarationAnalysisData GetOrComputeDeclarationAnalysisData(SyntaxReference declaration, Func<AnalyzerDriver.DeclarationAnalysisData> computeDeclarationAnalysisData, bool cacheAnalysisData)
			{
				if (!cacheAnalysisData)
				{
					return computeDeclarationAnalysisData.Invoke();
				}
				Dictionary<SyntaxReference, AnalyzerDriver.DeclarationAnalysisData> dictionary = this._declarationAnalysisDataMap;
				lock (dictionary)
				{
					AnalyzerDriver.DeclarationAnalysisData declarationAnalysisData;
					if (this._declarationAnalysisDataMap.TryGetValue(declaration, ref declarationAnalysisData))
					{
						return declarationAnalysisData;
					}
				}
				AnalyzerDriver.DeclarationAnalysisData declarationAnalysisData2 = computeDeclarationAnalysisData.Invoke();
				dictionary = this._declarationAnalysisDataMap;
				lock (dictionary)
				{
					AnalyzerDriver.DeclarationAnalysisData declarationAnalysisData3;
					if (!this._declarationAnalysisDataMap.TryGetValue(declaration, ref declarationAnalysisData3))
					{
						this._declarationAnalysisDataMap.Add(declaration, declarationAnalysisData2);
					}
					else
					{
						declarationAnalysisData2 = declarationAnalysisData3;
					}
				}
				return declarationAnalysisData2;
			}

			// Token: 0x060039B5 RID: 14773 RVA: 0x000A0748 File Offset: 0x0009E948
			internal void ClearDeclarationAnalysisData(SyntaxReference declaration)
			{
				Dictionary<SyntaxReference, AnalyzerDriver.DeclarationAnalysisData> declarationAnalysisDataMap = this._declarationAnalysisDataMap;
				lock (declarationAnalysisDataMap)
				{
					this._declarationAnalysisDataMap.Remove(declaration);
				}
			}

			// Token: 0x0400193C RID: 6460
			private readonly Dictionary<SyntaxReference, AnalyzerDriver.DeclarationAnalysisData> _declarationAnalysisDataMap;
		}

		// Token: 0x02000694 RID: 1684
		[Nullable(0)]
		internal sealed class DeclarationAnalysisData
		{
			// Token: 0x060039B6 RID: 14774 RVA: 0x000A0790 File Offset: 0x0009E990
			public DeclarationAnalysisData(SyntaxNode declaringReferenceSyntax, SyntaxNode topmostNodeForAnalysis, [Nullable(0)] ImmutableArray<DeclarationInfo> declarationsInNodeBuilder, [Nullable(new byte[] { 0, 1 })] ImmutableArray<SyntaxNode> descendantNodesToAnalyze, bool isPartialAnalysis)
			{
				this.DeclaringReferenceSyntax = declaringReferenceSyntax;
				this.TopmostNodeForAnalysis = topmostNodeForAnalysis;
				this.DeclarationsInNode = declarationsInNodeBuilder;
				this.DescendantNodesToAnalyze = descendantNodesToAnalyze;
				this.IsPartialAnalysis = isPartialAnalysis;
			}

			// Token: 0x17000FEB RID: 4075
			// (get) Token: 0x060039B7 RID: 14775 RVA: 0x000A07BD File Offset: 0x0009E9BD
			public SyntaxNode DeclaringReferenceSyntax { get; }

			// Token: 0x17000FEC RID: 4076
			// (get) Token: 0x060039B8 RID: 14776 RVA: 0x000A07C5 File Offset: 0x0009E9C5
			public SyntaxNode TopmostNodeForAnalysis { get; }

			// Token: 0x17000FED RID: 4077
			// (get) Token: 0x060039B9 RID: 14777 RVA: 0x000A07CD File Offset: 0x0009E9CD
			[Nullable(0)]
			public ImmutableArray<DeclarationInfo> DeclarationsInNode
			{
				[NullableContext(0)]
				get;
			}

			// Token: 0x17000FEE RID: 4078
			// (get) Token: 0x060039BA RID: 14778 RVA: 0x000A07D5 File Offset: 0x0009E9D5
			[Nullable(new byte[] { 0, 1 })]
			public ImmutableArray<SyntaxNode> DescendantNodesToAnalyze
			{
				[return: Nullable(new byte[] { 0, 1 })]
				get;
			}

			// Token: 0x17000FEF RID: 4079
			// (get) Token: 0x060039BB RID: 14779 RVA: 0x000A07DD File Offset: 0x0009E9DD
			public bool IsPartialAnalysis { get; }
		}

		// Token: 0x02000695 RID: 1685
		[Nullable(0)]
		private sealed class EventProcessedState
		{
			// Token: 0x17000FF0 RID: 4080
			// (get) Token: 0x060039BC RID: 14780 RVA: 0x000A07E5 File Offset: 0x0009E9E5
			public AnalyzerDriver.EventProcessedStateKind Kind { get; }

			// Token: 0x17000FF1 RID: 4081
			// (get) Token: 0x060039BD RID: 14781 RVA: 0x000A07ED File Offset: 0x0009E9ED
			[Nullable(new byte[] { 0, 1 })]
			public ImmutableArray<DiagnosticAnalyzer> SubsetProcessedAnalyzers
			{
				[return: Nullable(new byte[] { 0, 1 })]
				get;
			}

			// Token: 0x060039BE RID: 14782 RVA: 0x000A07F5 File Offset: 0x0009E9F5
			private EventProcessedState(AnalyzerDriver.EventProcessedStateKind kind)
			{
				this.Kind = kind;
				this.SubsetProcessedAnalyzers = default(ImmutableArray<DiagnosticAnalyzer>);
			}

			// Token: 0x060039BF RID: 14783 RVA: 0x000A0810 File Offset: 0x0009EA10
			private EventProcessedState([Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticAnalyzer> subsetProcessedAnalyzers)
			{
				this.SubsetProcessedAnalyzers = subsetProcessedAnalyzers;
				this.Kind = AnalyzerDriver.EventProcessedStateKind.PartiallyProcessed;
			}

			// Token: 0x060039C0 RID: 14784 RVA: 0x000A0826 File Offset: 0x0009EA26
			public static AnalyzerDriver.EventProcessedState CreatePartiallyProcessed([Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticAnalyzer> subsetProcessedAnalyzers)
			{
				return new AnalyzerDriver.EventProcessedState(subsetProcessedAnalyzers);
			}

			// Token: 0x04001944 RID: 6468
			public static readonly AnalyzerDriver.EventProcessedState Processed = new AnalyzerDriver.EventProcessedState(AnalyzerDriver.EventProcessedStateKind.Processed);

			// Token: 0x04001945 RID: 6469
			public static readonly AnalyzerDriver.EventProcessedState NotProcessed = new AnalyzerDriver.EventProcessedState(AnalyzerDriver.EventProcessedStateKind.NotProcessed);
		}

		// Token: 0x02000696 RID: 1686
		[NullableContext(0)]
		private enum EventProcessedStateKind
		{
			// Token: 0x04001949 RID: 6473
			Processed,
			// Token: 0x0400194A RID: 6474
			NotProcessed,
			// Token: 0x0400194B RID: 6475
			PartiallyProcessed
		}

		// Token: 0x02000697 RID: 1687
		[NullableContext(0)]
		private sealed class GeneratedCodeTokenWalker : SyntaxWalker
		{
			// Token: 0x060039C2 RID: 14786 RVA: 0x000A0846 File Offset: 0x0009EA46
			public GeneratedCodeTokenWalker(CancellationToken cancellationToken)
				: base(SyntaxWalkerDepth.Token)
			{
				this._cancellationToken = cancellationToken;
			}

			// Token: 0x17000FF2 RID: 4082
			// (get) Token: 0x060039C3 RID: 14787 RVA: 0x000A0856 File Offset: 0x0009EA56
			// (set) Token: 0x060039C4 RID: 14788 RVA: 0x000A085E File Offset: 0x0009EA5E
			public bool HasGeneratedCodeIdentifier { get; private set; }

			// Token: 0x060039C5 RID: 14789 RVA: 0x000A0868 File Offset: 0x0009EA68
			[NullableContext(1)]
			public override void Visit(SyntaxNode node)
			{
				if (this.HasGeneratedCodeIdentifier)
				{
					return;
				}
				this._cancellationToken.ThrowIfCancellationRequested();
				base.Visit(node);
			}

			// Token: 0x060039C6 RID: 14790 RVA: 0x000A0893 File Offset: 0x0009EA93
			protected override void VisitToken(SyntaxToken token)
			{
				this.HasGeneratedCodeIdentifier |= string.Equals(token.ValueText, "GeneratedCode", 4) || string.Equals(token.ValueText, "GeneratedCodeAttribute", 4);
			}

			// Token: 0x0400194C RID: 6476
			private readonly CancellationToken _cancellationToken;
		}

		// Token: 0x02000698 RID: 1688
		protected interface IGroupedAnalyzerActions
		{
			// Token: 0x17000FF3 RID: 4083
			// (get) Token: 0x060039C7 RID: 14791
			bool IsEmpty { get; }

			// Token: 0x17000FF4 RID: 4084
			// (get) Token: 0x060039C8 RID: 14792
			AnalyzerActions AnalyzerActions { get; }

			// Token: 0x060039C9 RID: 14793
			AnalyzerDriver.IGroupedAnalyzerActions Append(AnalyzerDriver.IGroupedAnalyzerActions groupedAnalyzerActions);
		}
	}
}
