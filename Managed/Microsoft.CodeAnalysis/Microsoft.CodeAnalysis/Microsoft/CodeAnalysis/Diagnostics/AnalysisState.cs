using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.Diagnostics.Telemetry;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x020002FE RID: 766
	[NullableContext(1)]
	[Nullable(0)]
	internal class AnalysisState
	{
		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x060020E2 RID: 8418 RVA: 0x00065BA0 File Offset: 0x00063DA0
		private ImmutableDictionary<DiagnosticAnalyzer, AnalyzerActionCounts> AnalyzerActionCountsMap
		{
			get
			{
				return this._lazyAnalyzerActionCountsMap;
			}
		}

		// Token: 0x060020E3 RID: 8419 RVA: 0x00065BA8 File Offset: 0x00063DA8
		public AnalysisState([Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticAnalyzer> analyzers, CachingSemanticModelProvider semanticModelProvider, CompilationOptions compilationOptions)
		{
			this._gate = new SemaphoreSlim(1);
			this._analyzerStateMap = AnalysisState.CreateAnalyzerStateMap(analyzers, out this._analyzerStates);
			this._semanticModelProvider = semanticModelProvider;
			this._compilationOptions = compilationOptions;
			this._pendingSourceEvents = new Dictionary<SyntaxTree, HashSet<CompilationEvent>>();
			this._pendingNonSourceEvents = new HashSet<CompilationEvent>();
			this._partialSymbolsWithGeneratedSourceEvents = new HashSet<ISymbol>();
			this._compilationEventsPool = new ObjectPool<HashSet<CompilationEvent>>(() => new HashSet<CompilationEvent>());
			this._pooledEventsWithAnyActionsSet = new HashSet<CompilationEvent>();
		}

		// Token: 0x060020E4 RID: 8420 RVA: 0x00065C40 File Offset: 0x00063E40
		private static ImmutableDictionary<DiagnosticAnalyzer, int> CreateAnalyzerStateMap([Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticAnalyzer> analyzers, [Nullable(new byte[] { 0, 1 })] out ImmutableArray<AnalysisState.PerAnalyzerState> analyzerStates)
		{
			ObjectPool<AnalysisState.AnalyzerStateData> objectPool = new ObjectPool<AnalysisState.AnalyzerStateData>(() => new AnalysisState.AnalyzerStateData());
			ObjectPool<AnalysisState.DeclarationAnalyzerStateData> objectPool2 = new ObjectPool<AnalysisState.DeclarationAnalyzerStateData>(() => new AnalysisState.DeclarationAnalyzerStateData());
			ObjectPool<Dictionary<int, AnalysisState.DeclarationAnalyzerStateData>> objectPool3 = new ObjectPool<Dictionary<int, AnalysisState.DeclarationAnalyzerStateData>>(() => new Dictionary<int, AnalysisState.DeclarationAnalyzerStateData>());
			ImmutableArray<AnalysisState.PerAnalyzerState>.Builder builder = ImmutableArray.CreateBuilder<AnalysisState.PerAnalyzerState>();
			ImmutableDictionary<DiagnosticAnalyzer, int>.Builder builder2 = ImmutableDictionary.CreateBuilder<DiagnosticAnalyzer, int>();
			int num = 0;
			foreach (DiagnosticAnalyzer diagnosticAnalyzer in analyzers)
			{
				builder.Add(new AnalysisState.PerAnalyzerState(objectPool, objectPool2, objectPool3));
				builder2[diagnosticAnalyzer] = num;
				num++;
			}
			analyzerStates = builder.ToImmutable();
			return builder2.ToImmutable();
		}

		// Token: 0x060020E5 RID: 8421 RVA: 0x00065D1C File Offset: 0x00063F1C
		private AnalysisState.PerAnalyzerState GetAnalyzerState(DiagnosticAnalyzer analyzer)
		{
			int num = this._analyzerStateMap[analyzer];
			return this._analyzerStates[num];
		}

		// Token: 0x060020E6 RID: 8422 RVA: 0x00065D48 File Offset: 0x00063F48
		public Task OnCompilationEventsGeneratedAsync([Nullable(new byte[] { 1, 1, 1, 0, 1, 0, 1 })] Func<AsyncQueue<CompilationEvent>, ImmutableArray<AdditionalText>, ImmutableArray<CompilationEvent>> getCompilationEvents, AsyncQueue<CompilationEvent> eventQueue, [Nullable(new byte[] { 0, 1 })] ImmutableArray<AdditionalText> additionalFiles, AnalyzerDriver driver, CancellationToken cancellationToken)
		{
			AnalysisState.<OnCompilationEventsGeneratedAsync>d__17 <OnCompilationEventsGeneratedAsync>d__;
			<OnCompilationEventsGeneratedAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<OnCompilationEventsGeneratedAsync>d__.<>4__this = this;
			<OnCompilationEventsGeneratedAsync>d__.getCompilationEvents = getCompilationEvents;
			<OnCompilationEventsGeneratedAsync>d__.eventQueue = eventQueue;
			<OnCompilationEventsGeneratedAsync>d__.additionalFiles = additionalFiles;
			<OnCompilationEventsGeneratedAsync>d__.driver = driver;
			<OnCompilationEventsGeneratedAsync>d__.cancellationToken = cancellationToken;
			<OnCompilationEventsGeneratedAsync>d__.<>1__state = -1;
			<OnCompilationEventsGeneratedAsync>d__.<>t__builder.Start<AnalysisState.<OnCompilationEventsGeneratedAsync>d__17>(ref <OnCompilationEventsGeneratedAsync>d__);
			return <OnCompilationEventsGeneratedAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060020E7 RID: 8423 RVA: 0x00065DB8 File Offset: 0x00063FB8
		private void OnCompilationEventsGenerated_NoLock([Nullable(new byte[] { 0, 1 })] ImmutableArray<CompilationEvent> compilationEvents)
		{
			this.AddToEventsMap_NoLock(compilationEvents);
			ArrayBuilder<ISymbol> arrayBuilder = null;
			foreach (KeyValuePair<DiagnosticAnalyzer, int> keyValuePair in this._analyzerStateMap)
			{
				DiagnosticAnalyzer key = keyValuePair.Key;
				AnalysisState.PerAnalyzerState perAnalyzerState = this._analyzerStates[keyValuePair.Value];
				AnalyzerActionCounts analyzerActionCounts = this.AnalyzerActionCountsMap[key];
				foreach (CompilationEvent compilationEvent in compilationEvents)
				{
					if (AnalysisState.HasActionsForEvent(compilationEvent, analyzerActionCounts))
					{
						this._pooledEventsWithAnyActionsSet.Add(compilationEvent);
						SymbolDeclaredCompilationEvent symbolDeclaredCompilationEvent = compilationEvent as SymbolDeclaredCompilationEvent;
						if (symbolDeclaredCompilationEvent != null && symbolDeclaredCompilationEvent.DeclaringSyntaxReferences.Length > 1)
						{
							if (this._partialSymbolsWithGeneratedSourceEvents.Contains(symbolDeclaredCompilationEvent.Symbol))
							{
								continue;
							}
							if (arrayBuilder == null)
							{
								arrayBuilder = ArrayBuilder<ISymbol>.GetInstance();
							}
							arrayBuilder.Add(symbolDeclaredCompilationEvent.Symbol);
						}
						perAnalyzerState.OnCompilationEventGenerated(compilationEvent, analyzerActionCounts);
					}
				}
			}
			foreach (CompilationEvent compilationEvent2 in compilationEvents)
			{
				if (!this._pooledEventsWithAnyActionsSet.Remove(compilationEvent2))
				{
					this.UpdateEventsMap_NoLock(compilationEvent2, false);
				}
			}
			if (arrayBuilder != null)
			{
				this._partialSymbolsWithGeneratedSourceEvents.AddAll<ISymbol>(arrayBuilder);
				arrayBuilder.Free();
			}
		}

		// Token: 0x060020E8 RID: 8424 RVA: 0x00065F18 File Offset: 0x00064118
		private void AddToEventsMap_NoLock([Nullable(new byte[] { 0, 1 })] ImmutableArray<CompilationEvent> compilationEvents)
		{
			foreach (CompilationEvent compilationEvent in compilationEvents)
			{
				this.UpdateEventsMap_NoLock(compilationEvent, true);
			}
		}

		// Token: 0x060020E9 RID: 8425 RVA: 0x00065F48 File Offset: 0x00064148
		private void UpdateEventsMap_NoLock(CompilationEvent compilationEvent, bool add)
		{
			SymbolDeclaredCompilationEvent symbolDeclaredCompilationEvent = compilationEvent as SymbolDeclaredCompilationEvent;
			if (symbolDeclaredCompilationEvent != null)
			{
				foreach (Location location in symbolDeclaredCompilationEvent.Symbol.Locations)
				{
					if (location.SourceTree != null)
					{
						if (add)
						{
							this.AddPendingSourceEvent_NoLock(location.SourceTree, compilationEvent);
						}
						else
						{
							this.RemovePendingSourceEvent_NoLock(location.SourceTree, compilationEvent);
						}
					}
				}
				return;
			}
			CompilationUnitCompletedEvent compilationUnitCompletedEvent = compilationEvent as CompilationUnitCompletedEvent;
			if (compilationUnitCompletedEvent == null)
			{
				if (compilationEvent is CompilationStartedEvent)
				{
					this.<UpdateEventsMap_NoLock>g__compilationStartedOrCompletedEventCommon|20_0(compilationEvent, add);
					return;
				}
				if (!(compilationEvent is CompilationCompletedEvent))
				{
					throw new InvalidOperationException("Unexpected compilation event of type " + compilationEvent.GetType().Name);
				}
				this.<UpdateEventsMap_NoLock>g__compilationStartedOrCompletedEventCommon|20_0(compilationEvent, add);
				if (!add)
				{
					this._semanticModelProvider.ClearCache(compilationEvent.Compilation);
					return;
				}
				return;
			}
			else
			{
				SyntaxTree compilationUnit = compilationUnitCompletedEvent.CompilationUnit;
				if (add)
				{
					this.AddPendingSourceEvent_NoLock(compilationUnit, compilationEvent);
					return;
				}
				this.RemovePendingSourceEvent_NoLock(compilationUnit, compilationEvent);
				return;
			}
		}

		// Token: 0x060020EA RID: 8426 RVA: 0x00066030 File Offset: 0x00064230
		private void AddPendingSourceEvent_NoLock(SyntaxTree tree, CompilationEvent compilationEvent)
		{
			HashSet<CompilationEvent> hashSet;
			if (!this._pendingSourceEvents.TryGetValue(tree, ref hashSet))
			{
				hashSet = new HashSet<CompilationEvent>();
				this._pendingSourceEvents[tree] = hashSet;
				this._semanticModelProvider.ClearCache(tree, compilationEvent.Compilation);
			}
			hashSet.Add(compilationEvent);
		}

		// Token: 0x060020EB RID: 8427 RVA: 0x0006607C File Offset: 0x0006427C
		private void RemovePendingSourceEvent_NoLock(SyntaxTree tree, CompilationEvent compilationEvent)
		{
			HashSet<CompilationEvent> hashSet;
			if (this._pendingSourceEvents.TryGetValue(tree, ref hashSet) && hashSet.Remove(compilationEvent) && hashSet.Count == 0)
			{
				this._pendingSourceEvents.Remove(tree);
				this._semanticModelProvider.ClearCache(tree, compilationEvent.Compilation);
			}
		}

		// Token: 0x060020EC RID: 8428 RVA: 0x000660CC File Offset: 0x000642CC
		private Task EnsureAnalyzerActionCountsInitializedAsync(AnalyzerDriver driver, CancellationToken cancellationToken)
		{
			AnalysisState.<EnsureAnalyzerActionCountsInitializedAsync>d__23 <EnsureAnalyzerActionCountsInitializedAsync>d__;
			<EnsureAnalyzerActionCountsInitializedAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<EnsureAnalyzerActionCountsInitializedAsync>d__.<>4__this = this;
			<EnsureAnalyzerActionCountsInitializedAsync>d__.driver = driver;
			<EnsureAnalyzerActionCountsInitializedAsync>d__.cancellationToken = cancellationToken;
			<EnsureAnalyzerActionCountsInitializedAsync>d__.<>1__state = -1;
			<EnsureAnalyzerActionCountsInitializedAsync>d__.<>t__builder.Start<AnalysisState.<EnsureAnalyzerActionCountsInitializedAsync>d__23>(ref <EnsureAnalyzerActionCountsInitializedAsync>d__);
			return <EnsureAnalyzerActionCountsInitializedAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060020ED RID: 8429 RVA: 0x00066120 File Offset: 0x00064320
		internal Task<AnalyzerActionCounts> GetOrComputeAnalyzerActionCountsAsync(DiagnosticAnalyzer analyzer, AnalyzerDriver driver, CancellationToken cancellationToken)
		{
			AnalysisState.<GetOrComputeAnalyzerActionCountsAsync>d__24 <GetOrComputeAnalyzerActionCountsAsync>d__;
			<GetOrComputeAnalyzerActionCountsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<AnalyzerActionCounts>.Create();
			<GetOrComputeAnalyzerActionCountsAsync>d__.<>4__this = this;
			<GetOrComputeAnalyzerActionCountsAsync>d__.analyzer = analyzer;
			<GetOrComputeAnalyzerActionCountsAsync>d__.driver = driver;
			<GetOrComputeAnalyzerActionCountsAsync>d__.cancellationToken = cancellationToken;
			<GetOrComputeAnalyzerActionCountsAsync>d__.<>1__state = -1;
			<GetOrComputeAnalyzerActionCountsAsync>d__.<>t__builder.Start<AnalysisState.<GetOrComputeAnalyzerActionCountsAsync>d__24>(ref <GetOrComputeAnalyzerActionCountsAsync>d__);
			return <GetOrComputeAnalyzerActionCountsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060020EE RID: 8430 RVA: 0x0006617B File Offset: 0x0006437B
		internal AnalyzerActionCounts GetAnalyzerActionCounts(DiagnosticAnalyzer analyzer)
		{
			return this.AnalyzerActionCountsMap[analyzer];
		}

		// Token: 0x060020EF RID: 8431 RVA: 0x0006618C File Offset: 0x0006438C
		private static bool HasActionsForEvent(CompilationEvent compilationEvent, AnalyzerActionCounts actionCounts)
		{
			bool flag;
			if (!(compilationEvent is CompilationStartedEvent))
			{
				if (!(compilationEvent is CompilationCompletedEvent))
				{
					if (!(compilationEvent is SymbolDeclaredCompilationEvent))
					{
						flag = actionCounts.SemanticModelActionsCount > 0;
					}
					else
					{
						flag = actionCounts.SymbolActionsCount > 0 || actionCounts.HasAnyExecutableCodeActions;
					}
				}
				else
				{
					flag = actionCounts.CompilationEndActionsCount > 0;
				}
			}
			else
			{
				flag = actionCounts.CompilationActionsCount > 0 || actionCounts.SyntaxTreeActionsCount > 0 || actionCounts.AdditionalFileActionsCount > 0;
			}
			return flag;
		}

		// Token: 0x060020F0 RID: 8432 RVA: 0x00066200 File Offset: 0x00064400
		private Task OnSymbolDeclaredEventProcessedAsync(SymbolDeclaredCompilationEvent symbolDeclaredEvent, [Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticAnalyzer> analyzers, Func<ISymbol, DiagnosticAnalyzer, Task> onSymbolAndMembersProcessedAsync)
		{
			AnalysisState.<OnSymbolDeclaredEventProcessedAsync>d__27 <OnSymbolDeclaredEventProcessedAsync>d__;
			<OnSymbolDeclaredEventProcessedAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<OnSymbolDeclaredEventProcessedAsync>d__.<>4__this = this;
			<OnSymbolDeclaredEventProcessedAsync>d__.symbolDeclaredEvent = symbolDeclaredEvent;
			<OnSymbolDeclaredEventProcessedAsync>d__.analyzers = analyzers;
			<OnSymbolDeclaredEventProcessedAsync>d__.onSymbolAndMembersProcessedAsync = onSymbolAndMembersProcessedAsync;
			<OnSymbolDeclaredEventProcessedAsync>d__.<>1__state = -1;
			<OnSymbolDeclaredEventProcessedAsync>d__.<>t__builder.Start<AnalysisState.<OnSymbolDeclaredEventProcessedAsync>d__27>(ref <OnSymbolDeclaredEventProcessedAsync>d__);
			return <OnSymbolDeclaredEventProcessedAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060020F1 RID: 8433 RVA: 0x0006625C File Offset: 0x0006445C
		public Task OnCompilationEventProcessedAsync(CompilationEvent compilationEvent, [Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticAnalyzer> analyzers, Func<ISymbol, DiagnosticAnalyzer, Task> onSymbolAndMembersProcessedAsync)
		{
			AnalysisState.<OnCompilationEventProcessedAsync>d__28 <OnCompilationEventProcessedAsync>d__;
			<OnCompilationEventProcessedAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<OnCompilationEventProcessedAsync>d__.<>4__this = this;
			<OnCompilationEventProcessedAsync>d__.compilationEvent = compilationEvent;
			<OnCompilationEventProcessedAsync>d__.analyzers = analyzers;
			<OnCompilationEventProcessedAsync>d__.onSymbolAndMembersProcessedAsync = onSymbolAndMembersProcessedAsync;
			<OnCompilationEventProcessedAsync>d__.<>1__state = -1;
			<OnCompilationEventProcessedAsync>d__.<>t__builder.Start<AnalysisState.<OnCompilationEventProcessedAsync>d__28>(ref <OnCompilationEventProcessedAsync>d__);
			return <OnCompilationEventProcessedAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060020F2 RID: 8434 RVA: 0x000662B8 File Offset: 0x000644B8
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<CompilationEvent> GetPendingEvents([Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticAnalyzer> analyzers, SyntaxTree tree, CancellationToken cancellationToken)
		{
			ImmutableArray<CompilationEvent> pendingEvents_NoLock;
			using (this._gate.DisposableWait(cancellationToken))
			{
				pendingEvents_NoLock = this.GetPendingEvents_NoLock(analyzers, tree);
			}
			return pendingEvents_NoLock;
		}

		// Token: 0x060020F3 RID: 8435 RVA: 0x000662FC File Offset: 0x000644FC
		private HashSet<CompilationEvent> GetPendingEvents_NoLock([Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticAnalyzer> analyzers)
		{
			HashSet<CompilationEvent> hashSet = this._compilationEventsPool.Allocate();
			foreach (DiagnosticAnalyzer diagnosticAnalyzer in analyzers)
			{
				this.GetAnalyzerState(diagnosticAnalyzer).AddPendingEvents(hashSet);
			}
			return hashSet;
		}

		// Token: 0x060020F4 RID: 8436 RVA: 0x00066340 File Offset: 0x00064540
		[return: Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<CompilationEvent> GetPendingEvents_NoLock([Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticAnalyzer> analyzers, SyntaxTree tree)
		{
			HashSet<CompilationEvent> hashSet;
			if (this._pendingSourceEvents.TryGetValue(tree, ref hashSet) && hashSet != null && hashSet.Count > 0)
			{
				HashSet<CompilationEvent> hashSet2 = null;
				try
				{
					hashSet2 = this.GetPendingEvents_NoLock(analyzers);
					if (hashSet2.Count > 0)
					{
						hashSet2.IntersectWith(hashSet);
						return hashSet2.ToImmutableArray<CompilationEvent>();
					}
				}
				finally
				{
					this.Free(hashSet2);
				}
			}
			return ImmutableArray<CompilationEvent>.Empty;
		}

		// Token: 0x060020F5 RID: 8437 RVA: 0x000663B0 File Offset: 0x000645B0
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<CompilationEvent> GetPendingEvents([Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticAnalyzer> analyzers, bool includeSourceEvents, bool includeNonSourceEvents, CancellationToken cancellationToken)
		{
			ImmutableArray<CompilationEvent> pendingEvents_NoLock;
			using (this._gate.DisposableWait(cancellationToken))
			{
				pendingEvents_NoLock = this.GetPendingEvents_NoLock(analyzers, includeSourceEvents, includeNonSourceEvents);
			}
			return pendingEvents_NoLock;
		}

		// Token: 0x060020F6 RID: 8438 RVA: 0x000663F8 File Offset: 0x000645F8
		[return: Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<CompilationEvent> GetPendingEvents_NoLock([Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticAnalyzer> analyzers, bool includeSourceEvents, bool includeNonSourceEvents)
		{
			HashSet<CompilationEvent> hashSet = null;
			HashSet<CompilationEvent> hashSet2 = null;
			ImmutableArray<CompilationEvent> immutableArray;
			try
			{
				hashSet = this.GetPendingEvents_NoLock(analyzers);
				if (hashSet.Count == 0)
				{
					immutableArray = ImmutableArray<CompilationEvent>.Empty;
				}
				else
				{
					hashSet2 = this._compilationEventsPool.Allocate();
					if (includeSourceEvents)
					{
						foreach (HashSet<CompilationEvent> hashSet3 in this._pendingSourceEvents.Values)
						{
							foreach (CompilationEvent compilationEvent in hashSet3)
							{
								hashSet2.Add(compilationEvent);
							}
						}
					}
					if (includeNonSourceEvents)
					{
						foreach (CompilationEvent compilationEvent2 in this._pendingNonSourceEvents)
						{
							hashSet2.Add(compilationEvent2);
						}
					}
					hashSet2.IntersectWith(hashSet);
					immutableArray = hashSet2.ToImmutableArray<CompilationEvent>();
				}
			}
			finally
			{
				this.Free(hashSet);
				this.Free(hashSet2);
			}
			return immutableArray;
		}

		// Token: 0x060020F7 RID: 8439 RVA: 0x0006652C File Offset: 0x0006472C
		private void Free([Nullable(new byte[] { 2, 1 })] HashSet<CompilationEvent> events)
		{
			if (events != null)
			{
				events.Clear();
				this._compilationEventsPool.Free(events);
			}
		}

		// Token: 0x060020F8 RID: 8440 RVA: 0x00066544 File Offset: 0x00064744
		public bool HasPendingSyntaxAnalysis(AnalysisScope analysisScope)
		{
			if (analysisScope.IsSingleFileAnalysis && !analysisScope.IsSyntacticSingleFileAnalysis)
			{
				return false;
			}
			foreach (DiagnosticAnalyzer diagnosticAnalyzer in analysisScope.Analyzers)
			{
				if (this.GetAnalyzerState(diagnosticAnalyzer).HasPendingSyntaxAnalysis(analysisScope.FilterFileOpt))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060020F9 RID: 8441 RVA: 0x0006659C File Offset: 0x0006479C
		public bool HasPendingSymbolAnalysis(AnalysisScope analysisScope, CancellationToken cancellationToken)
		{
			foreach (SymbolDeclaredCompilationEvent symbolDeclaredCompilationEvent in this.GetPendingSymbolDeclaredEvents(analysisScope.FilterFileOpt.Value.SourceTree, cancellationToken))
			{
				if (analysisScope.ShouldAnalyze(symbolDeclaredCompilationEvent.Symbol))
				{
					foreach (DiagnosticAnalyzer diagnosticAnalyzer in analysisScope.Analyzers)
					{
						if (this.GetAnalyzerState(diagnosticAnalyzer).HasPendingSymbolAnalysis(symbolDeclaredCompilationEvent.Symbol))
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x060020FA RID: 8442 RVA: 0x00066630 File Offset: 0x00064830
		[return: Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<SymbolDeclaredCompilationEvent> GetPendingSymbolDeclaredEvents(SyntaxTree tree, CancellationToken cancellationToken)
		{
			ImmutableArray<SymbolDeclaredCompilationEvent> immutableArray;
			using (this._gate.DisposableWait(cancellationToken))
			{
				HashSet<CompilationEvent> hashSet;
				if (!this._pendingSourceEvents.TryGetValue(tree, ref hashSet))
				{
					immutableArray = ImmutableArray<SymbolDeclaredCompilationEvent>.Empty;
				}
				else
				{
					immutableArray = Enumerable.OfType<SymbolDeclaredCompilationEvent>(hashSet).ToImmutableArray<SymbolDeclaredCompilationEvent>();
				}
			}
			return immutableArray;
		}

		// Token: 0x060020FB RID: 8443 RVA: 0x00066690 File Offset: 0x00064890
		public bool TryStartProcessingEvent(CompilationEvent compilationEvent, DiagnosticAnalyzer analyzer, [Nullable(2)] [NotNullWhen(true)] out AnalysisState.AnalyzerStateData state)
		{
			return this.GetAnalyzerState(analyzer).TryStartProcessingEvent(compilationEvent, out state);
		}

		// Token: 0x060020FC RID: 8444 RVA: 0x000666A0 File Offset: 0x000648A0
		public void MarkEventComplete(CompilationEvent compilationEvent, DiagnosticAnalyzer analyzer)
		{
			this.GetAnalyzerState(analyzer).MarkEventComplete(compilationEvent);
		}

		// Token: 0x060020FD RID: 8445 RVA: 0x000666B0 File Offset: 0x000648B0
		public void MarkEventComplete(CompilationEvent compilationEvent, IEnumerable<DiagnosticAnalyzer> analyzers)
		{
			foreach (DiagnosticAnalyzer diagnosticAnalyzer in analyzers)
			{
				this.GetAnalyzerState(diagnosticAnalyzer).MarkEventComplete(compilationEvent);
			}
		}

		// Token: 0x060020FE RID: 8446 RVA: 0x00066700 File Offset: 0x00064900
		public void MarkEventCompleteForUnprocessedAnalyzers(CompilationEvent completedEvent, AnalysisScope analysisScope, HashSet<DiagnosticAnalyzer> processedAnalyzers)
		{
			AnalysisState.MarkAnalysisCompleteForUnprocessedAnalyzers<CompilationEvent>(analysisScope, processedAnalyzers, new Action<CompilationEvent, DiagnosticAnalyzer>(this.MarkEventComplete), completedEvent);
		}

		// Token: 0x060020FF RID: 8447 RVA: 0x00066716 File Offset: 0x00064916
		public bool IsEventComplete(CompilationEvent compilationEvent, DiagnosticAnalyzer analyzer)
		{
			return this.GetAnalyzerState(analyzer).IsEventAnalyzed(compilationEvent);
		}

		// Token: 0x06002100 RID: 8448 RVA: 0x00066725 File Offset: 0x00064925
		public bool TryStartAnalyzingSymbol(ISymbol symbol, DiagnosticAnalyzer analyzer, [Nullable(2)] [NotNullWhen(true)] out AnalysisState.AnalyzerStateData state)
		{
			return this.GetAnalyzerState(analyzer).TryStartAnalyzingSymbol(symbol, out state);
		}

		// Token: 0x06002101 RID: 8449 RVA: 0x00066735 File Offset: 0x00064935
		public bool TryStartSymbolEndAnalysis(ISymbol symbol, DiagnosticAnalyzer analyzer, [Nullable(2)] [NotNullWhen(true)] out AnalysisState.AnalyzerStateData state)
		{
			return this.GetAnalyzerState(analyzer).TryStartSymbolEndAnalysis(symbol, out state);
		}

		// Token: 0x06002102 RID: 8450 RVA: 0x00066745 File Offset: 0x00064945
		public void MarkSymbolComplete(ISymbol symbol, DiagnosticAnalyzer analyzer)
		{
			this.GetAnalyzerState(analyzer).MarkSymbolComplete(symbol);
		}

		// Token: 0x06002103 RID: 8451 RVA: 0x00066754 File Offset: 0x00064954
		public void MarkSymbolCompleteForUnprocessedAnalyzers(ISymbol symbol, AnalysisScope analysisScope, HashSet<DiagnosticAnalyzer> processedAnalyzers)
		{
			AnalysisState.MarkAnalysisCompleteForUnprocessedAnalyzers<ISymbol>(analysisScope, processedAnalyzers, new Action<ISymbol, DiagnosticAnalyzer>(this.MarkSymbolComplete), symbol);
		}

		// Token: 0x06002104 RID: 8452 RVA: 0x0006676A File Offset: 0x0006496A
		public bool IsSymbolComplete(ISymbol symbol, DiagnosticAnalyzer analyzer)
		{
			return this.GetAnalyzerState(analyzer).IsSymbolComplete(symbol);
		}

		// Token: 0x06002105 RID: 8453 RVA: 0x0006677C File Offset: 0x0006497C
		public void MarkSymbolEndAnalysisComplete(ISymbol symbol, IEnumerable<DiagnosticAnalyzer> analyzers)
		{
			foreach (DiagnosticAnalyzer diagnosticAnalyzer in analyzers)
			{
				this.MarkSymbolEndAnalysisComplete(symbol, diagnosticAnalyzer);
			}
		}

		// Token: 0x06002106 RID: 8454 RVA: 0x000667C8 File Offset: 0x000649C8
		public void MarkSymbolEndAnalysisComplete(ISymbol symbol, DiagnosticAnalyzer analyzer)
		{
			this.GetAnalyzerState(analyzer).MarkSymbolEndAnalysisComplete(symbol);
		}

		// Token: 0x06002107 RID: 8455 RVA: 0x000667D7 File Offset: 0x000649D7
		public bool IsSymbolEndAnalysisComplete(ISymbol symbol, DiagnosticAnalyzer analyzer)
		{
			return this.GetAnalyzerState(analyzer).IsSymbolEndAnalysisComplete(symbol);
		}

		// Token: 0x06002108 RID: 8456 RVA: 0x000667E6 File Offset: 0x000649E6
		public bool TryStartAnalyzingDeclaration(ISymbol symbol, int declarationIndex, DiagnosticAnalyzer analyzer, [Nullable(2)] [NotNullWhen(true)] out AnalysisState.DeclarationAnalyzerStateData state)
		{
			return this.GetAnalyzerState(analyzer).TryStartAnalyzingDeclaration(symbol, declarationIndex, out state);
		}

		// Token: 0x06002109 RID: 8457 RVA: 0x000667F8 File Offset: 0x000649F8
		public bool IsDeclarationComplete(ISymbol symbol, int declarationIndex)
		{
			return AnalysisState.IsDeclarationComplete(symbol, declarationIndex, this._analyzerStates);
		}

		// Token: 0x0600210A RID: 8458 RVA: 0x0006680C File Offset: 0x00064A0C
		public bool IsDeclarationComplete(ISymbol symbol, int declarationIndex, DiagnosticAnalyzer analyzer)
		{
			AnalysisState.PerAnalyzerState analyzerState = this.GetAnalyzerState(analyzer);
			return AnalysisState.IsDeclarationComplete(symbol, declarationIndex, SpecializedCollections.SingletonEnumerable<AnalysisState.PerAnalyzerState>(analyzerState));
		}

		// Token: 0x0600210B RID: 8459 RVA: 0x00066830 File Offset: 0x00064A30
		private static bool IsDeclarationComplete(ISymbol symbol, int declarationIndex, IEnumerable<AnalysisState.PerAnalyzerState> analyzerStates)
		{
			using (IEnumerator<AnalysisState.PerAnalyzerState> enumerator = analyzerStates.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.IsDeclarationComplete(symbol, declarationIndex))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0600210C RID: 8460 RVA: 0x00066880 File Offset: 0x00064A80
		public void MarkDeclarationComplete(ISymbol symbol, int declarationIndex, DiagnosticAnalyzer analyzer)
		{
			this.GetAnalyzerState(analyzer).MarkDeclarationComplete(symbol, declarationIndex);
		}

		// Token: 0x0600210D RID: 8461 RVA: 0x00066890 File Offset: 0x00064A90
		public void MarkDeclarationComplete(ISymbol symbol, int declarationIndex, IEnumerable<DiagnosticAnalyzer> analyzers)
		{
			foreach (DiagnosticAnalyzer diagnosticAnalyzer in analyzers)
			{
				this.GetAnalyzerState(diagnosticAnalyzer).MarkDeclarationComplete(symbol, declarationIndex);
			}
		}

		// Token: 0x0600210E RID: 8462 RVA: 0x000668E0 File Offset: 0x00064AE0
		public void MarkDeclarationsComplete(ISymbol symbol, IEnumerable<DiagnosticAnalyzer> analyzers)
		{
			foreach (DiagnosticAnalyzer diagnosticAnalyzer in analyzers)
			{
				this.GetAnalyzerState(diagnosticAnalyzer).MarkDeclarationsComplete(symbol);
			}
		}

		// Token: 0x0600210F RID: 8463 RVA: 0x00066930 File Offset: 0x00064B30
		public bool TryStartSyntaxAnalysis(SourceOrAdditionalFile file, DiagnosticAnalyzer analyzer, [Nullable(2)] [NotNullWhen(true)] out AnalysisState.AnalyzerStateData state)
		{
			return this.GetAnalyzerState(analyzer).TryStartSyntaxAnalysis(file, out state);
		}

		// Token: 0x06002110 RID: 8464 RVA: 0x00066940 File Offset: 0x00064B40
		public void MarkSyntaxAnalysisComplete(SourceOrAdditionalFile file, DiagnosticAnalyzer analyzer)
		{
			this.GetAnalyzerState(analyzer).MarkSyntaxAnalysisComplete(file);
		}

		// Token: 0x06002111 RID: 8465 RVA: 0x00066950 File Offset: 0x00064B50
		public void MarkSyntaxAnalysisComplete(SourceOrAdditionalFile file, IEnumerable<DiagnosticAnalyzer> analyzers)
		{
			foreach (DiagnosticAnalyzer diagnosticAnalyzer in analyzers)
			{
				this.GetAnalyzerState(diagnosticAnalyzer).MarkSyntaxAnalysisComplete(file);
			}
		}

		// Token: 0x06002112 RID: 8466 RVA: 0x000669A0 File Offset: 0x00064BA0
		public void MarkSyntaxAnalysisCompleteForUnprocessedAnalyzers(SourceOrAdditionalFile file, AnalysisScope analysisScope, HashSet<DiagnosticAnalyzer> processedAnalyzers)
		{
			AnalysisState.MarkAnalysisCompleteForUnprocessedAnalyzers<SourceOrAdditionalFile>(analysisScope, processedAnalyzers, new Action<SourceOrAdditionalFile, DiagnosticAnalyzer>(this.MarkSyntaxAnalysisComplete), file);
		}

		// Token: 0x06002113 RID: 8467 RVA: 0x000669B8 File Offset: 0x00064BB8
		private static void MarkAnalysisCompleteForUnprocessedAnalyzers<[Nullable(2)] T>(AnalysisScope analysisScope, HashSet<DiagnosticAnalyzer> processedAnalyzers, Action<T, DiagnosticAnalyzer> markComplete, T arg)
		{
			if (analysisScope.Analyzers.Length == processedAnalyzers.Count)
			{
				return;
			}
			foreach (DiagnosticAnalyzer diagnosticAnalyzer in analysisScope.Analyzers)
			{
				if (!processedAnalyzers.Contains(diagnosticAnalyzer))
				{
					markComplete.Invoke(arg, diagnosticAnalyzer);
				}
			}
		}

		// Token: 0x06002114 RID: 8468 RVA: 0x00066A0F File Offset: 0x00064C0F
		[CompilerGenerated]
		private void <UpdateEventsMap_NoLock>g__compilationStartedOrCompletedEventCommon|20_0(CompilationEvent compilationEvent, bool add)
		{
			if (add)
			{
				this._pendingNonSourceEvents.Add(compilationEvent);
				return;
			}
			this._pendingNonSourceEvents.Remove(compilationEvent);
		}

		// Token: 0x04000E4D RID: 3661
		private readonly SemaphoreSlim _gate;

		// Token: 0x04000E4E RID: 3662
		private readonly ImmutableDictionary<DiagnosticAnalyzer, int> _analyzerStateMap;

		// Token: 0x04000E4F RID: 3663
		[Nullable(new byte[] { 0, 1 })]
		private readonly ImmutableArray<AnalysisState.PerAnalyzerState> _analyzerStates;

		// Token: 0x04000E50 RID: 3664
		private readonly Dictionary<SyntaxTree, HashSet<CompilationEvent>> _pendingSourceEvents;

		// Token: 0x04000E51 RID: 3665
		private readonly HashSet<CompilationEvent> _pendingNonSourceEvents;

		// Token: 0x04000E52 RID: 3666
		[Nullable(new byte[] { 2, 1, 1 })]
		private ImmutableDictionary<DiagnosticAnalyzer, AnalyzerActionCounts> _lazyAnalyzerActionCountsMap;

		// Token: 0x04000E53 RID: 3667
		private readonly HashSet<ISymbol> _partialSymbolsWithGeneratedSourceEvents;

		// Token: 0x04000E54 RID: 3668
		private readonly CachingSemanticModelProvider _semanticModelProvider;

		// Token: 0x04000E55 RID: 3669
		private readonly CompilationOptions _compilationOptions;

		// Token: 0x04000E56 RID: 3670
		private readonly ObjectPool<HashSet<CompilationEvent>> _compilationEventsPool;

		// Token: 0x04000E57 RID: 3671
		private readonly HashSet<CompilationEvent> _pooledEventsWithAnyActionsSet;

		// Token: 0x02000683 RID: 1667
		[NullableContext(0)]
		internal class AnalyzerStateData
		{
			// Token: 0x17000FDD RID: 4061
			// (get) Token: 0x06003952 RID: 14674 RVA: 0x0009F29E File Offset: 0x0009D49E
			// (set) Token: 0x06003953 RID: 14675 RVA: 0x0009F2A6 File Offset: 0x0009D4A6
			public AnalysisState.StateKind StateKind { get; private set; }

			// Token: 0x17000FDE RID: 4062
			// (get) Token: 0x06003954 RID: 14676 RVA: 0x0009F2AF File Offset: 0x0009D4AF
			public HashSet<AnalyzerAction> ProcessedActions { get; }

			// Token: 0x06003955 RID: 14677 RVA: 0x0009F2B7 File Offset: 0x0009D4B7
			public AnalyzerStateData()
			{
				this.StateKind = AnalysisState.StateKind.InProcess;
				this.ProcessedActions = new HashSet<AnalyzerAction>();
			}

			// Token: 0x06003956 RID: 14678 RVA: 0x0009F2D1 File Offset: 0x0009D4D1
			private static AnalysisState.AnalyzerStateData CreateFullyProcessedInstance()
			{
				AnalysisState.AnalyzerStateData analyzerStateData = new AnalysisState.AnalyzerStateData();
				analyzerStateData.SetStateKind(AnalysisState.StateKind.FullyProcessed);
				return analyzerStateData;
			}

			// Token: 0x06003957 RID: 14679 RVA: 0x0009F2DF File Offset: 0x0009D4DF
			public virtual void SetStateKind(AnalysisState.StateKind stateKind)
			{
				this.StateKind = stateKind;
			}

			// Token: 0x06003958 RID: 14680 RVA: 0x0009F2E8 File Offset: 0x0009D4E8
			public void ResetToReadyState()
			{
				this.SetStateKind(AnalysisState.StateKind.ReadyToProcess);
			}

			// Token: 0x06003959 RID: 14681 RVA: 0x0009F2F1 File Offset: 0x0009D4F1
			public virtual void Free()
			{
				this.StateKind = AnalysisState.StateKind.ReadyToProcess;
				this.ProcessedActions.Clear();
			}

			// Token: 0x040018F5 RID: 6389
			public static readonly AnalysisState.AnalyzerStateData FullyProcessedInstance = AnalysisState.AnalyzerStateData.CreateFullyProcessedInstance();
		}

		// Token: 0x02000684 RID: 1668
		[Nullable(0)]
		private class PerAnalyzerState
		{
			// Token: 0x0600395B RID: 14683 RVA: 0x0009F314 File Offset: 0x0009D514
			public PerAnalyzerState(ObjectPool<AnalysisState.AnalyzerStateData> analyzerStateDataPool, ObjectPool<AnalysisState.DeclarationAnalyzerStateData> declarationAnalyzerStateDataPool, ObjectPool<Dictionary<int, AnalysisState.DeclarationAnalyzerStateData>> currentlyAnalyzingDeclarationsMapPool)
			{
				this._gate = new object();
				this._pendingEvents = new Dictionary<CompilationEvent, AnalysisState.AnalyzerStateData>();
				this._pendingSymbols = new Dictionary<ISymbol, AnalysisState.AnalyzerStateData>();
				this._pendingDeclarations = new Dictionary<ISymbol, Dictionary<int, AnalysisState.DeclarationAnalyzerStateData>>();
				this._analyzerStateDataPool = analyzerStateDataPool;
				this._declarationAnalyzerStateDataPool = declarationAnalyzerStateDataPool;
				this._currentlyAnalyzingDeclarationsMapPool = currentlyAnalyzingDeclarationsMapPool;
			}

			// Token: 0x0600395C RID: 14684 RVA: 0x0009F368 File Offset: 0x0009D568
			public void AddPendingEvents(HashSet<CompilationEvent> uniqueEvents)
			{
				object gate = this._gate;
				lock (gate)
				{
					foreach (CompilationEvent compilationEvent in this._pendingEvents.Keys)
					{
						uniqueEvents.Add(compilationEvent);
					}
				}
			}

			// Token: 0x0600395D RID: 14685 RVA: 0x0009F3E8 File Offset: 0x0009D5E8
			public bool HasPendingSyntaxAnalysis(SourceOrAdditionalFile? file)
			{
				object gate = this._gate;
				bool flag2;
				lock (gate)
				{
					AnalysisState.AnalyzerStateData analyzerStateData;
					if (this._pendingSyntaxAnalysisFilesCount == 0)
					{
						flag2 = false;
					}
					else if (file == null)
					{
						flag2 = true;
					}
					else if (!this._lazyFilesWithAnalysisData.TryGetValue(file.Value, ref analyzerStateData))
					{
						flag2 = true;
					}
					else
					{
						flag2 = analyzerStateData.StateKind != AnalysisState.StateKind.FullyProcessed;
					}
				}
				return flag2;
			}

			// Token: 0x0600395E RID: 14686 RVA: 0x0009F464 File Offset: 0x0009D664
			public bool HasPendingSymbolAnalysis(ISymbol symbol)
			{
				object gate = this._gate;
				bool flag3;
				lock (gate)
				{
					bool flag2;
					if (!this._pendingSymbols.ContainsKey(symbol))
					{
						Dictionary<ISymbol, AnalysisState.AnalyzerStateData> lazyPendingSymbolEndAnalyses = this._lazyPendingSymbolEndAnalyses;
						flag2 = lazyPendingSymbolEndAnalyses != null && lazyPendingSymbolEndAnalyses.ContainsKey(symbol);
					}
					else
					{
						flag2 = true;
					}
					flag3 = flag2;
				}
				return flag3;
			}

			// Token: 0x0600395F RID: 14687 RVA: 0x0009F4C4 File Offset: 0x0009D6C4
			private bool TryStartProcessingEntity<TAnalysisEntity, [Nullable(0)] TAnalyzerStateData>(TAnalysisEntity analysisEntity, [Nullable(new byte[] { 1, 1, 2 })] Dictionary<TAnalysisEntity, TAnalyzerStateData> pendingEntities, ObjectPool<TAnalyzerStateData> pool, [Nullable(2)] [NotNullWhen(true)] out TAnalyzerStateData newState) where TAnalyzerStateData : AnalysisState.AnalyzerStateData, new()
			{
				object gate = this._gate;
				bool flag2;
				lock (gate)
				{
					flag2 = AnalysisState.PerAnalyzerState.TryStartProcessingEntity_NoLock<TAnalysisEntity, TAnalyzerStateData>(analysisEntity, pendingEntities, pool, out newState);
				}
				return flag2;
			}

			// Token: 0x06003960 RID: 14688 RVA: 0x0009F50C File Offset: 0x0009D70C
			private static bool TryStartProcessingEntity_NoLock<TAnalysisEntity, [Nullable(0)] TAnalyzerStateData>(TAnalysisEntity analysisEntity, [Nullable(new byte[] { 1, 1, 2 })] Dictionary<TAnalysisEntity, TAnalyzerStateData> pendingEntities, ObjectPool<TAnalyzerStateData> pool, [Nullable(2)] [NotNullWhen(true)] out TAnalyzerStateData state) where TAnalyzerStateData : AnalysisState.AnalyzerStateData
			{
				if (pendingEntities.TryGetValue(analysisEntity, ref state) && (state == null || state.StateKind == AnalysisState.StateKind.ReadyToProcess))
				{
					if (state == null)
					{
						state = pool.Allocate();
					}
					state.SetStateKind(AnalysisState.StateKind.InProcess);
					pendingEntities[analysisEntity] = state;
					return true;
				}
				state = default(TAnalyzerStateData);
				return false;
			}

			// Token: 0x06003961 RID: 14689 RVA: 0x0009F57C File Offset: 0x0009D77C
			private void MarkEntityProcessed<TAnalysisEntity, [Nullable(0)] TAnalyzerStateData>(TAnalysisEntity analysisEntity, [Nullable(new byte[] { 1, 1, 2 })] Dictionary<TAnalysisEntity, TAnalyzerStateData> pendingEntities, ObjectPool<TAnalyzerStateData> pool) where TAnalyzerStateData : AnalysisState.AnalyzerStateData
			{
				object gate = this._gate;
				lock (gate)
				{
					AnalysisState.PerAnalyzerState.MarkEntityProcessed_NoLock<TAnalysisEntity, TAnalyzerStateData>(analysisEntity, pendingEntities, pool);
				}
			}

			// Token: 0x06003962 RID: 14690 RVA: 0x0009F5C0 File Offset: 0x0009D7C0
			private static bool MarkEntityProcessed_NoLock<TAnalysisEntity, [Nullable(0)] TAnalyzerStateData>(TAnalysisEntity analysisEntity, [Nullable(new byte[] { 1, 1, 2 })] Dictionary<TAnalysisEntity, TAnalyzerStateData> pendingEntities, ObjectPool<TAnalyzerStateData> pool) where TAnalyzerStateData : AnalysisState.AnalyzerStateData
			{
				TAnalyzerStateData tanalyzerStateData;
				if (pendingEntities.TryGetValue(analysisEntity, ref tanalyzerStateData))
				{
					pendingEntities.Remove(analysisEntity);
					AnalysisState.PerAnalyzerState.FreeState_NoLock<TAnalyzerStateData>(tanalyzerStateData, pool);
					return true;
				}
				return false;
			}

			// Token: 0x06003963 RID: 14691 RVA: 0x0009F5EC File Offset: 0x0009D7EC
			[NullableContext(2)]
			private bool TryStartSyntaxAnalysis_NoLock(SourceOrAdditionalFile file, [NotNullWhen(true)] out AnalysisState.AnalyzerStateData state)
			{
				if (this._pendingSyntaxAnalysisFilesCount == 0)
				{
					state = null;
					return false;
				}
				if (this._lazyFilesWithAnalysisData.TryGetValue(file, ref state))
				{
					if (state.StateKind != AnalysisState.StateKind.ReadyToProcess)
					{
						state = null;
						return false;
					}
				}
				else
				{
					state = this._analyzerStateDataPool.Allocate();
				}
				state.SetStateKind(AnalysisState.StateKind.InProcess);
				this._lazyFilesWithAnalysisData[file] = state;
				return true;
			}

			// Token: 0x06003964 RID: 14692 RVA: 0x0009F648 File Offset: 0x0009D848
			private void MarkSyntaxAnalysisComplete_NoLock(SourceOrAdditionalFile file)
			{
				if (this._pendingSyntaxAnalysisFilesCount == 0)
				{
					return;
				}
				bool flag = false;
				AnalysisState.AnalyzerStateData analyzerStateData;
				if (this._lazyFilesWithAnalysisData.TryGetValue(file, ref analyzerStateData))
				{
					if (analyzerStateData.StateKind != AnalysisState.StateKind.FullyProcessed)
					{
						AnalysisState.PerAnalyzerState.FreeState_NoLock<AnalysisState.AnalyzerStateData>(analyzerStateData, this._analyzerStateDataPool);
					}
					else
					{
						flag = true;
					}
				}
				if (!flag)
				{
					this._pendingSyntaxAnalysisFilesCount--;
				}
				this._lazyFilesWithAnalysisData[file] = AnalysisState.AnalyzerStateData.FullyProcessedInstance;
			}

			// Token: 0x06003965 RID: 14693 RVA: 0x0009F6AB File Offset: 0x0009D8AB
			private Dictionary<int, AnalysisState.DeclarationAnalyzerStateData> EnsureDeclarationDataMap_NoLock(ISymbol symbol, [Nullable(new byte[] { 2, 1 })] Dictionary<int, AnalysisState.DeclarationAnalyzerStateData> declarationDataMap)
			{
				if (declarationDataMap == null)
				{
					declarationDataMap = this._currentlyAnalyzingDeclarationsMapPool.Allocate();
					this._pendingDeclarations[symbol] = declarationDataMap;
				}
				return declarationDataMap;
			}

			// Token: 0x06003966 RID: 14694 RVA: 0x0009F6CC File Offset: 0x0009D8CC
			private bool TryStartAnalyzingDeclaration_NoLock(ISymbol symbol, int declarationIndex, [Nullable(2)] [NotNullWhen(true)] out AnalysisState.DeclarationAnalyzerStateData state)
			{
				Dictionary<int, AnalysisState.DeclarationAnalyzerStateData> dictionary;
				if (!this._pendingDeclarations.TryGetValue(symbol, ref dictionary))
				{
					state = null;
					return false;
				}
				dictionary = this.EnsureDeclarationDataMap_NoLock(symbol, dictionary);
				if (dictionary.TryGetValue(declarationIndex, ref state))
				{
					if (state.StateKind != AnalysisState.StateKind.ReadyToProcess)
					{
						state = null;
						return false;
					}
				}
				else
				{
					state = this._declarationAnalyzerStateDataPool.Allocate();
				}
				state.SetStateKind(AnalysisState.StateKind.InProcess);
				dictionary[declarationIndex] = state;
				return true;
			}

			// Token: 0x06003967 RID: 14695 RVA: 0x0009F730 File Offset: 0x0009D930
			private void MarkDeclarationProcessed_NoLock(ISymbol symbol, int declarationIndex)
			{
				Dictionary<int, AnalysisState.DeclarationAnalyzerStateData> dictionary;
				if (!this._pendingDeclarations.TryGetValue(symbol, ref dictionary))
				{
					return;
				}
				dictionary = this.EnsureDeclarationDataMap_NoLock(symbol, dictionary);
				AnalysisState.DeclarationAnalyzerStateData declarationAnalyzerStateData;
				if (dictionary.TryGetValue(declarationIndex, ref declarationAnalyzerStateData))
				{
					this.FreeDeclarationAnalyzerState_NoLock(declarationAnalyzerStateData);
				}
				dictionary[declarationIndex] = AnalysisState.DeclarationAnalyzerStateData.FullyProcessedInstance;
			}

			// Token: 0x06003968 RID: 14696 RVA: 0x0009F778 File Offset: 0x0009D978
			private void MarkDeclarationsProcessed_NoLock(ISymbol symbol)
			{
				Dictionary<int, AnalysisState.DeclarationAnalyzerStateData> dictionary;
				if (this._pendingDeclarations.TryGetValue(symbol, ref dictionary))
				{
					this.FreeDeclarationDataMap_NoLock(dictionary);
					this._pendingDeclarations.Remove(symbol);
				}
			}

			// Token: 0x06003969 RID: 14697 RVA: 0x0009F7A9 File Offset: 0x0009D9A9
			private void FreeDeclarationDataMap_NoLock([Nullable(new byte[] { 2, 1 })] Dictionary<int, AnalysisState.DeclarationAnalyzerStateData> declarationDataMap)
			{
				if (declarationDataMap != null)
				{
					declarationDataMap.Clear();
					this._currentlyAnalyzingDeclarationsMapPool.Free(declarationDataMap);
				}
			}

			// Token: 0x0600396A RID: 14698 RVA: 0x0009F7C0 File Offset: 0x0009D9C0
			private void FreeDeclarationAnalyzerState_NoLock(AnalysisState.DeclarationAnalyzerStateData state)
			{
				if (state == AnalysisState.DeclarationAnalyzerStateData.FullyProcessedInstance)
				{
					return;
				}
				AnalysisState.PerAnalyzerState.FreeState_NoLock<AnalysisState.DeclarationAnalyzerStateData>(state, this._declarationAnalyzerStateDataPool);
			}

			// Token: 0x0600396B RID: 14699 RVA: 0x0009F7D7 File Offset: 0x0009D9D7
			private static void FreeState_NoLock<[Nullable(0)] TAnalyzerStateData>([Nullable(2)] TAnalyzerStateData state, ObjectPool<TAnalyzerStateData> pool) where TAnalyzerStateData : AnalysisState.AnalyzerStateData
			{
				if (state != null && state != AnalysisState.AnalyzerStateData.FullyProcessedInstance)
				{
					state.Free();
					pool.Free(state);
				}
			}

			// Token: 0x0600396C RID: 14700 RVA: 0x0009F800 File Offset: 0x0009DA00
			private bool IsEntityFullyProcessed<TAnalysisEntity, [Nullable(0)] TAnalyzerStateData>(TAnalysisEntity analysisEntity, [Nullable(new byte[] { 1, 1, 2 })] Dictionary<TAnalysisEntity, TAnalyzerStateData> pendingEntities) where TAnalyzerStateData : AnalysisState.AnalyzerStateData
			{
				object gate = this._gate;
				bool flag2;
				lock (gate)
				{
					flag2 = AnalysisState.PerAnalyzerState.IsEntityFullyProcessed_NoLock<TAnalysisEntity, TAnalyzerStateData>(analysisEntity, pendingEntities);
				}
				return flag2;
			}

			// Token: 0x0600396D RID: 14701 RVA: 0x0009F844 File Offset: 0x0009DA44
			private static bool IsEntityFullyProcessed_NoLock<TAnalysisEntity, [Nullable(0)] TAnalyzerStateData>(TAnalysisEntity analysisEntity, [Nullable(new byte[] { 1, 1, 2 })] Dictionary<TAnalysisEntity, TAnalyzerStateData> pendingEntities) where TAnalyzerStateData : AnalysisState.AnalyzerStateData
			{
				TAnalyzerStateData tanalyzerStateData;
				if (pendingEntities.TryGetValue(analysisEntity, ref tanalyzerStateData))
				{
					TAnalyzerStateData tanalyzerStateData2 = tanalyzerStateData;
					return tanalyzerStateData2 != null && tanalyzerStateData2.StateKind == AnalysisState.StateKind.FullyProcessed;
				}
				return true;
			}

			// Token: 0x0600396E RID: 14702 RVA: 0x0009F874 File Offset: 0x0009DA74
			private bool IsDeclarationComplete_NoLock(ISymbol symbol, int declarationIndex)
			{
				Dictionary<int, AnalysisState.DeclarationAnalyzerStateData> dictionary;
				AnalysisState.DeclarationAnalyzerStateData declarationAnalyzerStateData;
				return !this._pendingDeclarations.TryGetValue(symbol, ref dictionary) || (dictionary != null && dictionary.TryGetValue(declarationIndex, ref declarationAnalyzerStateData) && declarationAnalyzerStateData.StateKind == AnalysisState.StateKind.FullyProcessed);
			}

			// Token: 0x0600396F RID: 14703 RVA: 0x0009F8AC File Offset: 0x0009DAAC
			private bool AreDeclarationsProcessed_NoLock(ISymbol symbol, int declarationsCount)
			{
				Dictionary<int, AnalysisState.DeclarationAnalyzerStateData> dictionary;
				if (!this._pendingDeclarations.TryGetValue(symbol, ref dictionary))
				{
					return true;
				}
				if (dictionary != null && dictionary.Count == declarationsCount)
				{
					return Enumerable.All<AnalysisState.DeclarationAnalyzerStateData>(dictionary.Values, (AnalysisState.DeclarationAnalyzerStateData state) => state.StateKind == AnalysisState.StateKind.FullyProcessed);
				}
				return false;
			}

			// Token: 0x06003970 RID: 14704 RVA: 0x0009F903 File Offset: 0x0009DB03
			public bool TryStartProcessingEvent(CompilationEvent compilationEvent, [Nullable(2)] [NotNullWhen(true)] out AnalysisState.AnalyzerStateData state)
			{
				return this.TryStartProcessingEntity<CompilationEvent, AnalysisState.AnalyzerStateData>(compilationEvent, this._pendingEvents, this._analyzerStateDataPool, out state);
			}

			// Token: 0x06003971 RID: 14705 RVA: 0x0009F919 File Offset: 0x0009DB19
			public void MarkEventComplete(CompilationEvent compilationEvent)
			{
				this.MarkEntityProcessed<CompilationEvent, AnalysisState.AnalyzerStateData>(compilationEvent, this._pendingEvents, this._analyzerStateDataPool);
			}

			// Token: 0x06003972 RID: 14706 RVA: 0x0009F92E File Offset: 0x0009DB2E
			public bool TryStartAnalyzingSymbol(ISymbol symbol, [Nullable(2)] [NotNullWhen(true)] out AnalysisState.AnalyzerStateData state)
			{
				return this.TryStartProcessingEntity<ISymbol, AnalysisState.AnalyzerStateData>(symbol, this._pendingSymbols, this._analyzerStateDataPool, out state);
			}

			// Token: 0x06003973 RID: 14707 RVA: 0x0009F944 File Offset: 0x0009DB44
			public bool TryStartSymbolEndAnalysis(ISymbol symbol, [Nullable(2)] [NotNullWhen(true)] out AnalysisState.AnalyzerStateData state)
			{
				return this.TryStartProcessingEntity<ISymbol, AnalysisState.AnalyzerStateData>(symbol, this._lazyPendingSymbolEndAnalyses, this._analyzerStateDataPool, out state);
			}

			// Token: 0x06003974 RID: 14708 RVA: 0x0009F95A File Offset: 0x0009DB5A
			public void MarkSymbolComplete(ISymbol symbol)
			{
				this.MarkEntityProcessed<ISymbol, AnalysisState.AnalyzerStateData>(symbol, this._pendingSymbols, this._analyzerStateDataPool);
			}

			// Token: 0x06003975 RID: 14709 RVA: 0x0009F96F File Offset: 0x0009DB6F
			public void MarkSymbolEndAnalysisComplete(ISymbol symbol)
			{
				if (this._lazyPendingSymbolEndAnalyses != null)
				{
					this.MarkEntityProcessed<ISymbol, AnalysisState.AnalyzerStateData>(symbol, this._lazyPendingSymbolEndAnalyses, this._analyzerStateDataPool);
				}
			}

			// Token: 0x06003976 RID: 14710 RVA: 0x0009F98C File Offset: 0x0009DB8C
			public bool TryStartAnalyzingDeclaration(ISymbol symbol, int declarationIndex, [Nullable(2)] [NotNullWhen(true)] out AnalysisState.DeclarationAnalyzerStateData state)
			{
				object gate = this._gate;
				bool flag2;
				lock (gate)
				{
					flag2 = this.TryStartAnalyzingDeclaration_NoLock(symbol, declarationIndex, out state);
				}
				return flag2;
			}

			// Token: 0x06003977 RID: 14711 RVA: 0x0009F9D4 File Offset: 0x0009DBD4
			public bool IsDeclarationComplete(ISymbol symbol, int declarationIndex)
			{
				object gate = this._gate;
				bool flag2;
				lock (gate)
				{
					flag2 = this.IsDeclarationComplete_NoLock(symbol, declarationIndex);
				}
				return flag2;
			}

			// Token: 0x06003978 RID: 14712 RVA: 0x0009FA18 File Offset: 0x0009DC18
			public void MarkDeclarationComplete(ISymbol symbol, int declarationIndex)
			{
				object gate = this._gate;
				lock (gate)
				{
					this.MarkDeclarationProcessed_NoLock(symbol, declarationIndex);
				}
			}

			// Token: 0x06003979 RID: 14713 RVA: 0x0009FA5C File Offset: 0x0009DC5C
			public void MarkDeclarationsComplete(ISymbol symbol)
			{
				object gate = this._gate;
				lock (gate)
				{
					this.MarkDeclarationsProcessed_NoLock(symbol);
				}
			}

			// Token: 0x0600397A RID: 14714 RVA: 0x0009FAA0 File Offset: 0x0009DCA0
			[NullableContext(2)]
			public bool TryStartSyntaxAnalysis(SourceOrAdditionalFile tree, [NotNullWhen(true)] out AnalysisState.AnalyzerStateData state)
			{
				object gate = this._gate;
				bool flag2;
				lock (gate)
				{
					flag2 = this.TryStartSyntaxAnalysis_NoLock(tree, out state);
				}
				return flag2;
			}

			// Token: 0x0600397B RID: 14715 RVA: 0x0009FAE4 File Offset: 0x0009DCE4
			public void MarkSyntaxAnalysisComplete(SourceOrAdditionalFile file)
			{
				object gate = this._gate;
				lock (gate)
				{
					this.MarkSyntaxAnalysisComplete_NoLock(file);
				}
			}

			// Token: 0x0600397C RID: 14716 RVA: 0x0009FB28 File Offset: 0x0009DD28
			public void OnCompilationEventGenerated(CompilationEvent compilationEvent, AnalyzerActionCounts actionCounts)
			{
				object gate = this._gate;
				lock (gate)
				{
					SymbolDeclaredCompilationEvent symbolDeclaredCompilationEvent = compilationEvent as SymbolDeclaredCompilationEvent;
					if (symbolDeclaredCompilationEvent != null)
					{
						bool flag2 = false;
						ISymbol symbol = symbolDeclaredCompilationEvent.Symbol;
						bool flag3 = AnalysisScope.ShouldSkipSymbolAnalysis(symbolDeclaredCompilationEvent);
						if (!flag3 && actionCounts.SymbolActionsCount > 0)
						{
							flag2 = true;
							this._pendingSymbols[symbol] = null;
						}
						bool flag4 = AnalysisScope.ShouldSkipDeclarationAnalysis(symbol);
						if (!flag4 && actionCounts.HasAnyExecutableCodeActions)
						{
							flag2 = true;
							this._pendingDeclarations[symbol] = null;
						}
						if (actionCounts.SymbolStartActionsCount > 0 && (!flag3 || !flag4))
						{
							flag2 = true;
							if (this._lazyPendingSymbolEndAnalyses == null)
							{
								this._lazyPendingSymbolEndAnalyses = new Dictionary<ISymbol, AnalysisState.AnalyzerStateData>();
							}
							this._lazyPendingSymbolEndAnalyses[symbol] = null;
						}
						if (!flag2)
						{
							return;
						}
					}
					else
					{
						CompilationStartedEvent compilationStartedEvent = compilationEvent as CompilationStartedEvent;
						if (compilationStartedEvent != null)
						{
							int num = ((actionCounts.SyntaxTreeActionsCount > 0) ? Enumerable.Count<SyntaxTree>(compilationEvent.Compilation.SyntaxTrees) : 0);
							num += ((actionCounts.AdditionalFileActionsCount > 0) ? compilationStartedEvent.AdditionalFiles.Length : 0);
							if (num > 0)
							{
								this._lazyFilesWithAnalysisData = new Dictionary<SourceOrAdditionalFile, AnalysisState.AnalyzerStateData>();
								this._pendingSyntaxAnalysisFilesCount = num;
							}
							if (actionCounts.CompilationActionsCount == 0)
							{
								return;
							}
						}
					}
					this._pendingEvents[compilationEvent] = null;
				}
			}

			// Token: 0x0600397D RID: 14717 RVA: 0x0009FC88 File Offset: 0x0009DE88
			public bool IsEventAnalyzed(CompilationEvent compilationEvent)
			{
				return this.IsEntityFullyProcessed<CompilationEvent, AnalysisState.AnalyzerStateData>(compilationEvent, this._pendingEvents);
			}

			// Token: 0x0600397E RID: 14718 RVA: 0x0009FC97 File Offset: 0x0009DE97
			public bool IsSymbolComplete(ISymbol symbol)
			{
				return this.IsEntityFullyProcessed<ISymbol, AnalysisState.AnalyzerStateData>(symbol, this._pendingSymbols);
			}

			// Token: 0x0600397F RID: 14719 RVA: 0x0009FCA6 File Offset: 0x0009DEA6
			public bool IsSymbolEndAnalysisComplete(ISymbol symbol)
			{
				return this.IsEntityFullyProcessed<ISymbol, AnalysisState.AnalyzerStateData>(symbol, this._lazyPendingSymbolEndAnalyses);
			}

			// Token: 0x06003980 RID: 14720 RVA: 0x0009FCB8 File Offset: 0x0009DEB8
			public bool OnSymbolDeclaredEventProcessed(SymbolDeclaredCompilationEvent symbolDeclaredEvent)
			{
				object gate = this._gate;
				bool flag2;
				lock (gate)
				{
					flag2 = this.OnSymbolDeclaredEventProcessed_NoLock(symbolDeclaredEvent);
				}
				return flag2;
			}

			// Token: 0x06003981 RID: 14721 RVA: 0x0009FCFC File Offset: 0x0009DEFC
			private bool OnSymbolDeclaredEventProcessed_NoLock(SymbolDeclaredCompilationEvent symbolDeclaredEvent)
			{
				if (!AnalysisState.PerAnalyzerState.IsEntityFullyProcessed_NoLock<ISymbol, AnalysisState.AnalyzerStateData>(symbolDeclaredEvent.Symbol, this._pendingSymbols))
				{
					return false;
				}
				if (!this.AreDeclarationsProcessed_NoLock(symbolDeclaredEvent.Symbol, symbolDeclaredEvent.DeclaringSyntaxReferences.Length))
				{
					return false;
				}
				if (this._lazyPendingSymbolEndAnalyses != null && !AnalysisState.PerAnalyzerState.IsEntityFullyProcessed_NoLock<ISymbol, AnalysisState.AnalyzerStateData>(symbolDeclaredEvent.Symbol, this._lazyPendingSymbolEndAnalyses))
				{
					return false;
				}
				this.MarkDeclarationsProcessed_NoLock(symbolDeclaredEvent.Symbol);
				return AnalysisState.PerAnalyzerState.MarkEntityProcessed_NoLock<CompilationEvent, AnalysisState.AnalyzerStateData>(symbolDeclaredEvent, this._pendingEvents, this._analyzerStateDataPool);
			}

			// Token: 0x040018F6 RID: 6390
			private readonly object _gate;

			// Token: 0x040018F7 RID: 6391
			[Nullable(new byte[] { 1, 1, 2 })]
			private readonly Dictionary<CompilationEvent, AnalysisState.AnalyzerStateData> _pendingEvents;

			// Token: 0x040018F8 RID: 6392
			[Nullable(new byte[] { 1, 1, 2 })]
			private readonly Dictionary<ISymbol, AnalysisState.AnalyzerStateData> _pendingSymbols;

			// Token: 0x040018F9 RID: 6393
			[Nullable(new byte[] { 1, 1, 2, 1 })]
			private readonly Dictionary<ISymbol, Dictionary<int, AnalysisState.DeclarationAnalyzerStateData>> _pendingDeclarations;

			// Token: 0x040018FA RID: 6394
			[Nullable(new byte[] { 2, 1 })]
			private Dictionary<SourceOrAdditionalFile, AnalysisState.AnalyzerStateData> _lazyFilesWithAnalysisData;

			// Token: 0x040018FB RID: 6395
			private int _pendingSyntaxAnalysisFilesCount;

			// Token: 0x040018FC RID: 6396
			[Nullable(new byte[] { 2, 1, 2 })]
			private Dictionary<ISymbol, AnalysisState.AnalyzerStateData> _lazyPendingSymbolEndAnalyses;

			// Token: 0x040018FD RID: 6397
			private readonly ObjectPool<AnalysisState.AnalyzerStateData> _analyzerStateDataPool;

			// Token: 0x040018FE RID: 6398
			private readonly ObjectPool<AnalysisState.DeclarationAnalyzerStateData> _declarationAnalyzerStateDataPool;

			// Token: 0x040018FF RID: 6399
			private readonly ObjectPool<Dictionary<int, AnalysisState.DeclarationAnalyzerStateData>> _currentlyAnalyzingDeclarationsMapPool;
		}

		// Token: 0x02000685 RID: 1669
		[NullableContext(0)]
		internal enum StateKind
		{
			// Token: 0x04001901 RID: 6401
			ReadyToProcess,
			// Token: 0x04001902 RID: 6402
			InProcess,
			// Token: 0x04001903 RID: 6403
			FullyProcessed
		}

		// Token: 0x02000686 RID: 1670
		[NullableContext(0)]
		internal sealed class DeclarationAnalyzerStateData : AnalysisState.SyntaxNodeAnalyzerStateData
		{
			// Token: 0x17000FDF RID: 4063
			// (get) Token: 0x06003982 RID: 14722 RVA: 0x0009FD77 File Offset: 0x0009DF77
			public AnalysisState.CodeBlockAnalyzerStateData CodeBlockAnalysisState { get; }

			// Token: 0x17000FE0 RID: 4064
			// (get) Token: 0x06003983 RID: 14723 RVA: 0x0009FD7F File Offset: 0x0009DF7F
			public AnalysisState.OperationBlockAnalyzerStateData OperationBlockAnalysisState { get; }

			// Token: 0x06003984 RID: 14724 RVA: 0x0009FD87 File Offset: 0x0009DF87
			public DeclarationAnalyzerStateData()
			{
				this.CodeBlockAnalysisState = new AnalysisState.CodeBlockAnalyzerStateData();
				this.OperationBlockAnalysisState = new AnalysisState.OperationBlockAnalyzerStateData();
			}

			// Token: 0x06003985 RID: 14725 RVA: 0x0009FDA5 File Offset: 0x0009DFA5
			private static AnalysisState.DeclarationAnalyzerStateData CreateFullyProcessedInstance()
			{
				AnalysisState.DeclarationAnalyzerStateData declarationAnalyzerStateData = new AnalysisState.DeclarationAnalyzerStateData();
				declarationAnalyzerStateData.SetStateKind(AnalysisState.StateKind.FullyProcessed);
				return declarationAnalyzerStateData;
			}

			// Token: 0x06003986 RID: 14726 RVA: 0x0009FDB3 File Offset: 0x0009DFB3
			public override void SetStateKind(AnalysisState.StateKind stateKind)
			{
				this.CodeBlockAnalysisState.SetStateKind(stateKind);
				this.OperationBlockAnalysisState.SetStateKind(stateKind);
				base.SetStateKind(stateKind);
			}

			// Token: 0x06003987 RID: 14727 RVA: 0x0009FDD4 File Offset: 0x0009DFD4
			public override void Free()
			{
				base.Free();
				this.CodeBlockAnalysisState.Free();
				this.OperationBlockAnalysisState.Free();
			}

			// Token: 0x04001906 RID: 6406
			public new static readonly AnalysisState.DeclarationAnalyzerStateData FullyProcessedInstance = AnalysisState.DeclarationAnalyzerStateData.CreateFullyProcessedInstance();
		}

		// Token: 0x02000687 RID: 1671
		[NullableContext(0)]
		internal class SyntaxNodeAnalyzerStateData : AnalysisState.AnalyzerStateData
		{
			// Token: 0x17000FE1 RID: 4065
			// (get) Token: 0x06003989 RID: 14729 RVA: 0x0009FDFE File Offset: 0x0009DFFE
			public HashSet<SyntaxNode> ProcessedNodes { get; }

			// Token: 0x17000FE2 RID: 4066
			// (get) Token: 0x0600398A RID: 14730 RVA: 0x0009FE06 File Offset: 0x0009E006
			// (set) Token: 0x0600398B RID: 14731 RVA: 0x0009FE0E File Offset: 0x0009E00E
			public SyntaxNode CurrentNode { get; set; }

			// Token: 0x0600398C RID: 14732 RVA: 0x0009FE17 File Offset: 0x0009E017
			public SyntaxNodeAnalyzerStateData()
			{
				this.CurrentNode = null;
				this.ProcessedNodes = new HashSet<SyntaxNode>();
			}

			// Token: 0x0600398D RID: 14733 RVA: 0x0009FE31 File Offset: 0x0009E031
			public void ClearNodeAnalysisState()
			{
				this.CurrentNode = null;
				base.ProcessedActions.Clear();
			}

			// Token: 0x0600398E RID: 14734 RVA: 0x0009FE45 File Offset: 0x0009E045
			public override void Free()
			{
				base.Free();
				this.CurrentNode = null;
				this.ProcessedNodes.Clear();
			}
		}

		// Token: 0x02000688 RID: 1672
		[NullableContext(0)]
		internal class OperationAnalyzerStateData : AnalysisState.AnalyzerStateData
		{
			// Token: 0x17000FE3 RID: 4067
			// (get) Token: 0x0600398F RID: 14735 RVA: 0x0009FE5F File Offset: 0x0009E05F
			public HashSet<IOperation> ProcessedOperations { get; }

			// Token: 0x17000FE4 RID: 4068
			// (get) Token: 0x06003990 RID: 14736 RVA: 0x0009FE67 File Offset: 0x0009E067
			// (set) Token: 0x06003991 RID: 14737 RVA: 0x0009FE6F File Offset: 0x0009E06F
			public IOperation CurrentOperation { get; set; }

			// Token: 0x06003992 RID: 14738 RVA: 0x0009FE78 File Offset: 0x0009E078
			public OperationAnalyzerStateData()
			{
				this.CurrentOperation = null;
				this.ProcessedOperations = new HashSet<IOperation>();
			}

			// Token: 0x06003993 RID: 14739 RVA: 0x0009FE92 File Offset: 0x0009E092
			public void ClearNodeAnalysisState()
			{
				this.CurrentOperation = null;
				base.ProcessedActions.Clear();
			}

			// Token: 0x06003994 RID: 14740 RVA: 0x0009FEA6 File Offset: 0x0009E0A6
			public override void Free()
			{
				base.Free();
				this.CurrentOperation = null;
				this.ProcessedOperations.Clear();
			}
		}

		// Token: 0x02000689 RID: 1673
		[NullableContext(0)]
		internal abstract class BlockAnalyzerStateData<TBlockAction, TNodeStateData> : AnalysisState.AnalyzerStateData where TBlockAction : AnalyzerAction where TNodeStateData : AnalysisState.AnalyzerStateData, new()
		{
			// Token: 0x17000FE5 RID: 4069
			// (get) Token: 0x06003995 RID: 14741 RVA: 0x0009FEC0 File Offset: 0x0009E0C0
			public TNodeStateData ExecutableNodesAnalysisState { get; }

			// Token: 0x17000FE6 RID: 4070
			// (get) Token: 0x06003996 RID: 14742 RVA: 0x0009FEC8 File Offset: 0x0009E0C8
			// (set) Token: 0x06003997 RID: 14743 RVA: 0x0009FED0 File Offset: 0x0009E0D0
			public ImmutableHashSet<TBlockAction> CurrentBlockEndActions { get; set; }

			// Token: 0x17000FE7 RID: 4071
			// (get) Token: 0x06003998 RID: 14744 RVA: 0x0009FED9 File Offset: 0x0009E0D9
			// (set) Token: 0x06003999 RID: 14745 RVA: 0x0009FEE1 File Offset: 0x0009E0E1
			public ImmutableHashSet<AnalyzerAction> CurrentBlockNodeActions { get; set; }

			// Token: 0x0600399A RID: 14746 RVA: 0x0009FEEA File Offset: 0x0009E0EA
			public BlockAnalyzerStateData()
			{
				this.ExecutableNodesAnalysisState = new TNodeStateData();
				this.CurrentBlockEndActions = null;
				this.CurrentBlockNodeActions = null;
			}

			// Token: 0x0600399B RID: 14747 RVA: 0x0009FF0B File Offset: 0x0009E10B
			public override void SetStateKind(AnalysisState.StateKind stateKind)
			{
				this.ExecutableNodesAnalysisState.SetStateKind(stateKind);
				base.SetStateKind(stateKind);
			}

			// Token: 0x0600399C RID: 14748 RVA: 0x0009FF25 File Offset: 0x0009E125
			public override void Free()
			{
				base.Free();
				this.ExecutableNodesAnalysisState.Free();
				this.CurrentBlockEndActions = null;
				this.CurrentBlockNodeActions = null;
			}
		}

		// Token: 0x0200068A RID: 1674
		[NullableContext(0)]
		internal sealed class CodeBlockAnalyzerStateData : AnalysisState.BlockAnalyzerStateData<CodeBlockAnalyzerAction, AnalysisState.SyntaxNodeAnalyzerStateData>
		{
		}

		// Token: 0x0200068B RID: 1675
		[NullableContext(0)]
		internal sealed class OperationBlockAnalyzerStateData : AnalysisState.BlockAnalyzerStateData<OperationBlockAnalyzerAction, AnalysisState.OperationAnalyzerStateData>
		{
		}
	}
}
