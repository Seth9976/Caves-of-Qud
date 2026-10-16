using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.Diagnostics.Telemetry;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x020002FC RID: 764
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class AnalysisResultBuilder
	{
		// Token: 0x060020B4 RID: 8372 RVA: 0x00064CFC File Offset: 0x00062EFC
		internal AnalysisResultBuilder(bool logAnalyzerExecutionTime, [Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticAnalyzer> analyzers, [Nullable(new byte[] { 0, 1 })] ImmutableArray<AdditionalText> additionalFiles)
		{
			this._analyzerExecutionTimeOpt = (logAnalyzerExecutionTime ? AnalysisResultBuilder.CreateAnalyzerExecutionTimeMap(analyzers) : null);
			this._completedAnalyzers = new HashSet<DiagnosticAnalyzer>();
			this._analyzerActionCounts = new Dictionary<DiagnosticAnalyzer, AnalyzerActionCounts>(analyzers.Length);
			this._pathToAdditionalTextMap = AnalysisResultBuilder.CreatePathToAdditionalTextMap(additionalFiles);
		}

		// Token: 0x060020B5 RID: 8373 RVA: 0x00064D58 File Offset: 0x00062F58
		private static Dictionary<DiagnosticAnalyzer, TimeSpan> CreateAnalyzerExecutionTimeMap([Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticAnalyzer> analyzers)
		{
			Dictionary<DiagnosticAnalyzer, TimeSpan> dictionary = new Dictionary<DiagnosticAnalyzer, TimeSpan>(analyzers.Length);
			foreach (DiagnosticAnalyzer diagnosticAnalyzer in analyzers)
			{
				dictionary[diagnosticAnalyzer] = default(TimeSpan);
			}
			return dictionary;
		}

		// Token: 0x060020B6 RID: 8374 RVA: 0x00064DA0 File Offset: 0x00062FA0
		[return: Nullable(new byte[] { 1, 1, 0, 1 })]
		private static ImmutableDictionary<string, OneOrMany<AdditionalText>> CreatePathToAdditionalTextMap([Nullable(new byte[] { 0, 1 })] ImmutableArray<AdditionalText> additionalFiles)
		{
			if (additionalFiles.IsEmpty)
			{
				return AnalysisResultBuilder.s_emptyPathToAdditionalTextMap;
			}
			ImmutableDictionary<string, OneOrMany<AdditionalText>>.Builder builder = ImmutableDictionary.CreateBuilder<string, OneOrMany<AdditionalText>>(PathUtilities.Comparer);
			foreach (AdditionalText additionalText in additionalFiles)
			{
				string text = additionalText.Path ?? string.Empty;
				OneOrMany<AdditionalText> oneOrMany;
				if (builder.TryGetValue(text, out oneOrMany))
				{
					oneOrMany = oneOrMany.Add(additionalText);
				}
				else
				{
					oneOrMany = new OneOrMany<AdditionalText>(additionalText);
				}
				builder[text] = oneOrMany;
			}
			return builder.ToImmutable();
		}

		// Token: 0x060020B7 RID: 8375 RVA: 0x00064E20 File Offset: 0x00063020
		public TimeSpan GetAnalyzerExecutionTime(DiagnosticAnalyzer analyzer)
		{
			object gate = this._gate;
			TimeSpan timeSpan;
			lock (gate)
			{
				timeSpan = this._analyzerExecutionTimeOpt[analyzer];
			}
			return timeSpan;
		}

		// Token: 0x060020B8 RID: 8376 RVA: 0x00064E68 File Offset: 0x00063068
		[return: Nullable(new byte[] { 0, 1 })]
		internal ImmutableArray<DiagnosticAnalyzer> GetPendingAnalyzers([Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticAnalyzer> analyzers)
		{
			object gate = this._gate;
			ImmutableArray<DiagnosticAnalyzer> immutableArray;
			lock (gate)
			{
				ArrayBuilder<DiagnosticAnalyzer> arrayBuilder = null;
				foreach (DiagnosticAnalyzer diagnosticAnalyzer in analyzers)
				{
					if (!this._completedAnalyzers.Contains(diagnosticAnalyzer))
					{
						arrayBuilder = arrayBuilder ?? ArrayBuilder<DiagnosticAnalyzer>.GetInstance();
						arrayBuilder.Add(diagnosticAnalyzer);
					}
				}
				immutableArray = ((arrayBuilder != null) ? arrayBuilder.ToImmutableAndFree() : ImmutableArray<DiagnosticAnalyzer>.Empty);
			}
			return immutableArray;
		}

		// Token: 0x060020B9 RID: 8377 RVA: 0x00064EF8 File Offset: 0x000630F8
		internal void ApplySuppressionsAndStoreAnalysisResult(AnalysisScope analysisScope, AnalyzerDriver driver, Compilation compilation, Func<DiagnosticAnalyzer, AnalyzerActionCounts> getAnalyzerActionCounts, bool fullAnalysisResultForAnalyzersInScope)
		{
			AnalysisResultBuilder.<>c__DisplayClass15_0 CS$<>8__locals1 = new AnalysisResultBuilder.<>c__DisplayClass15_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.analysisScope = analysisScope;
			foreach (DiagnosticAnalyzer diagnosticAnalyzer in CS$<>8__locals1.analysisScope.Analyzers)
			{
				ImmutableArray<Diagnostic> immutableArray = driver.DequeueLocalDiagnosticsAndApplySuppressions(diagnosticAnalyzer, true, compilation);
				ImmutableArray<Diagnostic> immutableArray2 = driver.DequeueLocalDiagnosticsAndApplySuppressions(diagnosticAnalyzer, false, compilation);
				ImmutableArray<Diagnostic> immutableArray3 = driver.DequeueNonLocalDiagnosticsAndApplySuppressions(diagnosticAnalyzer, compilation);
				object gate = this._gate;
				lock (gate)
				{
					if (!this._completedAnalyzers.Contains(diagnosticAnalyzer))
					{
						if (immutableArray.Length > 0 || immutableArray2.Length > 0 || immutableArray3.Length > 0 || fullAnalysisResultForAnalyzersInScope)
						{
							this.UpdateLocalDiagnostics_NoLock<SyntaxTree>(diagnosticAnalyzer, immutableArray, fullAnalysisResultForAnalyzersInScope, new Func<Diagnostic, SyntaxTree>(AnalysisResultBuilder.<ApplySuppressionsAndStoreAnalysisResult>g__getSourceTree|15_0), ref this._localSyntaxDiagnosticsOpt);
							this.UpdateLocalDiagnostics_NoLock<AdditionalText>(diagnosticAnalyzer, immutableArray, fullAnalysisResultForAnalyzersInScope, new Func<Diagnostic, AdditionalText>(CS$<>8__locals1.<ApplySuppressionsAndStoreAnalysisResult>g__getAdditionalTextKey|1), ref this._localAdditionalFileDiagnosticsOpt);
							this.UpdateLocalDiagnostics_NoLock<SyntaxTree>(diagnosticAnalyzer, immutableArray2, fullAnalysisResultForAnalyzersInScope, new Func<Diagnostic, SyntaxTree>(AnalysisResultBuilder.<ApplySuppressionsAndStoreAnalysisResult>g__getSourceTree|15_0), ref this._localSemanticDiagnosticsOpt);
							this.UpdateNonLocalDiagnostics_NoLock(diagnosticAnalyzer, immutableArray3, fullAnalysisResultForAnalyzersInScope);
						}
						if (this._analyzerExecutionTimeOpt != null)
						{
							TimeSpan timeSpan = driver.ResetAnalyzerExecutionTime(diagnosticAnalyzer);
							this._analyzerExecutionTimeOpt[diagnosticAnalyzer] = (fullAnalysisResultForAnalyzersInScope ? timeSpan : (this._analyzerExecutionTimeOpt[diagnosticAnalyzer] + timeSpan));
						}
						if (!this._analyzerActionCounts.ContainsKey(diagnosticAnalyzer))
						{
							this._analyzerActionCounts.Add(diagnosticAnalyzer, getAnalyzerActionCounts.Invoke(diagnosticAnalyzer));
						}
						if (fullAnalysisResultForAnalyzersInScope)
						{
							this._completedAnalyzers.Add(diagnosticAnalyzer);
						}
					}
				}
			}
		}

		// Token: 0x060020BA RID: 8378 RVA: 0x000650AC File Offset: 0x000632AC
		private void UpdateLocalDiagnostics_NoLock<TKey>(DiagnosticAnalyzer analyzer, [Nullable(new byte[] { 0, 1 })] ImmutableArray<Diagnostic> diagnostics, bool overwrite, [Nullable(new byte[] { 1, 1, 2 })] Func<Diagnostic, TKey> getKeyFunc, [Nullable(new byte[] { 2, 1, 1, 1, 1, 1 })] ref Dictionary<TKey, Dictionary<DiagnosticAnalyzer, ImmutableArray<Diagnostic>.Builder>> lazyLocalDiagnostics) where TKey : class
		{
			if (diagnostics.IsEmpty)
			{
				return;
			}
			lazyLocalDiagnostics = lazyLocalDiagnostics ?? new Dictionary<TKey, Dictionary<DiagnosticAnalyzer, ImmutableArray<Diagnostic>.Builder>>();
			foreach (IGrouping<TKey, Diagnostic> grouping in Enumerable.GroupBy<Diagnostic, TKey>(diagnostics, getKeyFunc))
			{
				TKey key = grouping.Key;
				if (key != null)
				{
					Dictionary<DiagnosticAnalyzer, ImmutableArray<Diagnostic>.Builder> dictionary;
					if (!lazyLocalDiagnostics.TryGetValue(key, ref dictionary))
					{
						dictionary = new Dictionary<DiagnosticAnalyzer, ImmutableArray<Diagnostic>.Builder>();
						lazyLocalDiagnostics[key] = dictionary;
					}
					ImmutableArray<Diagnostic>.Builder builder;
					if (!dictionary.TryGetValue(analyzer, ref builder))
					{
						builder = ImmutableArray.CreateBuilder<Diagnostic>();
						dictionary[analyzer] = builder;
					}
					if (overwrite)
					{
						builder.Clear();
					}
					builder.AddRange(grouping);
				}
			}
		}

		// Token: 0x060020BB RID: 8379 RVA: 0x0006516C File Offset: 0x0006336C
		private void UpdateNonLocalDiagnostics_NoLock(DiagnosticAnalyzer analyzer, [Nullable(new byte[] { 0, 1 })] ImmutableArray<Diagnostic> diagnostics, bool overwrite)
		{
			if (diagnostics.IsEmpty)
			{
				return;
			}
			this._nonLocalDiagnosticsOpt = this._nonLocalDiagnosticsOpt ?? new Dictionary<DiagnosticAnalyzer, ImmutableArray<Diagnostic>.Builder>();
			ImmutableArray<Diagnostic>.Builder builder;
			if (!this._nonLocalDiagnosticsOpt.TryGetValue(analyzer, ref builder))
			{
				builder = ImmutableArray.CreateBuilder<Diagnostic>();
				this._nonLocalDiagnosticsOpt[analyzer] = builder;
			}
			if (overwrite)
			{
				builder.Clear();
			}
			builder.AddRange(diagnostics);
		}

		// Token: 0x060020BC RID: 8380 RVA: 0x000651CC File Offset: 0x000633CC
		[return: Nullable(new byte[] { 0, 1 })]
		internal ImmutableArray<Diagnostic> GetDiagnostics(AnalysisScope analysisScope, bool getLocalDiagnostics, bool getNonLocalDiagnostics)
		{
			object gate = this._gate;
			ImmutableArray<Diagnostic> diagnostics_NoLock;
			lock (gate)
			{
				diagnostics_NoLock = this.GetDiagnostics_NoLock(analysisScope, getLocalDiagnostics, getNonLocalDiagnostics);
			}
			return diagnostics_NoLock;
		}

		// Token: 0x060020BD RID: 8381 RVA: 0x00065214 File Offset: 0x00063414
		[return: Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<Diagnostic> GetDiagnostics_NoLock(AnalysisScope analysisScope, bool getLocalDiagnostics, bool getNonLocalDiagnostics)
		{
			ImmutableArray<Diagnostic>.Builder builder = ImmutableArray.CreateBuilder<Diagnostic>();
			if (getLocalDiagnostics)
			{
				if (!analysisScope.IsSingleFileAnalysis)
				{
					AnalysisResultBuilder.AddAllLocalDiagnostics_NoLock<SyntaxTree>(this._localSyntaxDiagnosticsOpt, analysisScope, builder);
					AnalysisResultBuilder.AddAllLocalDiagnostics_NoLock<SyntaxTree>(this._localSemanticDiagnosticsOpt, analysisScope, builder);
					AnalysisResultBuilder.AddAllLocalDiagnostics_NoLock<AdditionalText>(this._localAdditionalFileDiagnosticsOpt, analysisScope, builder);
				}
				else if (analysisScope.IsSyntacticSingleFileAnalysis)
				{
					AnalysisResultBuilder.AddLocalDiagnosticsForPartialAnalysis_NoLock(this._localSyntaxDiagnosticsOpt, analysisScope, builder);
					AnalysisResultBuilder.AddLocalDiagnosticsForPartialAnalysis_NoLock(this._localAdditionalFileDiagnosticsOpt, analysisScope, builder);
				}
				else
				{
					AnalysisResultBuilder.AddLocalDiagnosticsForPartialAnalysis_NoLock(this._localSemanticDiagnosticsOpt, analysisScope, builder);
				}
			}
			if (getNonLocalDiagnostics && this._nonLocalDiagnosticsOpt != null)
			{
				AnalysisResultBuilder.AddDiagnostics_NoLock(this._nonLocalDiagnosticsOpt, analysisScope.Analyzers, builder);
			}
			return builder.ToImmutableArray<Diagnostic>();
		}

		// Token: 0x060020BE RID: 8382 RVA: 0x000652B0 File Offset: 0x000634B0
		private static void AddAllLocalDiagnostics_NoLock<TKey>([Nullable(new byte[] { 2, 1, 1, 1, 1, 1 })] Dictionary<TKey, Dictionary<DiagnosticAnalyzer, ImmutableArray<Diagnostic>.Builder>> lazyLocalDiagnostics, AnalysisScope analysisScope, ImmutableArray<Diagnostic>.Builder builder) where TKey : class
		{
			if (lazyLocalDiagnostics != null)
			{
				foreach (Dictionary<DiagnosticAnalyzer, ImmutableArray<Diagnostic>.Builder> dictionary in lazyLocalDiagnostics.Values)
				{
					AnalysisResultBuilder.AddDiagnostics_NoLock(dictionary, analysisScope.Analyzers, builder);
				}
			}
		}

		// Token: 0x060020BF RID: 8383 RVA: 0x0006530C File Offset: 0x0006350C
		private static void AddLocalDiagnosticsForPartialAnalysis_NoLock([Nullable(new byte[] { 2, 1, 1, 1, 1, 1 })] Dictionary<SyntaxTree, Dictionary<DiagnosticAnalyzer, ImmutableArray<Diagnostic>.Builder>> localDiagnostics, AnalysisScope analysisScope, ImmutableArray<Diagnostic>.Builder builder)
		{
			AnalysisResultBuilder.AddLocalDiagnosticsForPartialAnalysis_NoLock<SyntaxTree>(localDiagnostics, analysisScope.FilterFileOpt.Value.SourceTree, analysisScope.Analyzers, builder);
		}

		// Token: 0x060020C0 RID: 8384 RVA: 0x0006533C File Offset: 0x0006353C
		private static void AddLocalDiagnosticsForPartialAnalysis_NoLock([Nullable(new byte[] { 2, 1, 1, 1, 1, 1 })] Dictionary<AdditionalText, Dictionary<DiagnosticAnalyzer, ImmutableArray<Diagnostic>.Builder>> localDiagnostics, AnalysisScope analysisScope, ImmutableArray<Diagnostic>.Builder builder)
		{
			AnalysisResultBuilder.AddLocalDiagnosticsForPartialAnalysis_NoLock<AdditionalText>(localDiagnostics, analysisScope.FilterFileOpt.Value.AdditionalFile, analysisScope.Analyzers, builder);
		}

		// Token: 0x060020C1 RID: 8385 RVA: 0x0006536C File Offset: 0x0006356C
		private static void AddLocalDiagnosticsForPartialAnalysis_NoLock<TKey>([Nullable(new byte[] { 2, 1, 1, 1, 1, 1 })] Dictionary<TKey, Dictionary<DiagnosticAnalyzer, ImmutableArray<Diagnostic>.Builder>> localDiagnostics, [Nullable(2)] TKey key, [Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticAnalyzer> analyzers, ImmutableArray<Diagnostic>.Builder builder) where TKey : class
		{
			Dictionary<DiagnosticAnalyzer, ImmutableArray<Diagnostic>.Builder> dictionary;
			if (key != null && localDiagnostics != null && localDiagnostics.TryGetValue(key, ref dictionary))
			{
				AnalysisResultBuilder.AddDiagnostics_NoLock(dictionary, analyzers, builder);
			}
		}

		// Token: 0x060020C2 RID: 8386 RVA: 0x00065398 File Offset: 0x00063598
		private static void AddDiagnostics_NoLock(Dictionary<DiagnosticAnalyzer, ImmutableArray<Diagnostic>.Builder> diagnostics, [Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticAnalyzer> analyzers, ImmutableArray<Diagnostic>.Builder builder)
		{
			foreach (DiagnosticAnalyzer diagnosticAnalyzer in analyzers)
			{
				ImmutableArray<Diagnostic>.Builder builder2;
				if (diagnostics.TryGetValue(diagnosticAnalyzer, ref builder2))
				{
					builder.AddRange(builder2);
				}
			}
		}

		// Token: 0x060020C3 RID: 8387 RVA: 0x000653D4 File Offset: 0x000635D4
		internal AnalysisResult ToAnalysisResult([Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticAnalyzer> analyzers, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			ImmutableHashSet<DiagnosticAnalyzer> immutableHashSet = analyzers.ToImmutableHashSet<DiagnosticAnalyzer>();
			object gate = this._gate;
			ImmutableDictionary<SyntaxTree, ImmutableDictionary<DiagnosticAnalyzer, ImmutableArray<Diagnostic>>> immutable;
			ImmutableDictionary<SyntaxTree, ImmutableDictionary<DiagnosticAnalyzer, ImmutableArray<Diagnostic>>> immutable2;
			ImmutableDictionary<AdditionalText, ImmutableDictionary<DiagnosticAnalyzer, ImmutableArray<Diagnostic>>> immutable3;
			ImmutableDictionary<DiagnosticAnalyzer, ImmutableArray<Diagnostic>> immutable4;
			lock (gate)
			{
				immutable = AnalysisResultBuilder.GetImmutable<SyntaxTree>(immutableHashSet, this._localSyntaxDiagnosticsOpt);
				immutable2 = AnalysisResultBuilder.GetImmutable<SyntaxTree>(immutableHashSet, this._localSemanticDiagnosticsOpt);
				immutable3 = AnalysisResultBuilder.GetImmutable<AdditionalText>(immutableHashSet, this._localAdditionalFileDiagnosticsOpt);
				immutable4 = AnalysisResultBuilder.GetImmutable(immutableHashSet, this._nonLocalDiagnosticsOpt);
			}
			cancellationToken.ThrowIfCancellationRequested();
			ImmutableDictionary<DiagnosticAnalyzer, AnalyzerTelemetryInfo> telemetryInfo = this.GetTelemetryInfo(analyzers);
			return new AnalysisResult(analyzers, immutable, immutable2, immutable3, immutable4, telemetryInfo);
		}

		// Token: 0x060020C4 RID: 8388 RVA: 0x0006547C File Offset: 0x0006367C
		[return: Nullable(new byte[] { 1, 1, 1, 1, 0, 1 })]
		private static ImmutableDictionary<TKey, ImmutableDictionary<DiagnosticAnalyzer, ImmutableArray<Diagnostic>>> GetImmutable<TKey>(ImmutableHashSet<DiagnosticAnalyzer> analyzers, [Nullable(new byte[] { 2, 1, 1, 1, 1, 1 })] Dictionary<TKey, Dictionary<DiagnosticAnalyzer, ImmutableArray<Diagnostic>.Builder>> localDiagnosticsOpt) where TKey : class
		{
			if (localDiagnosticsOpt == null)
			{
				return ImmutableDictionary<TKey, ImmutableDictionary<DiagnosticAnalyzer, ImmutableArray<Diagnostic>>>.Empty;
			}
			ImmutableDictionary<TKey, ImmutableDictionary<DiagnosticAnalyzer, ImmutableArray<Diagnostic>>>.Builder builder = ImmutableDictionary.CreateBuilder<TKey, ImmutableDictionary<DiagnosticAnalyzer, ImmutableArray<Diagnostic>>>();
			ImmutableDictionary<DiagnosticAnalyzer, ImmutableArray<Diagnostic>>.Builder builder2 = ImmutableDictionary.CreateBuilder<DiagnosticAnalyzer, ImmutableArray<Diagnostic>>();
			foreach (KeyValuePair<TKey, Dictionary<DiagnosticAnalyzer, ImmutableArray<Diagnostic>.Builder>> keyValuePair in localDiagnosticsOpt)
			{
				TKey key = keyValuePair.Key;
				foreach (KeyValuePair<DiagnosticAnalyzer, ImmutableArray<Diagnostic>.Builder> keyValuePair2 in keyValuePair.Value)
				{
					if (analyzers.Contains(keyValuePair2.Key))
					{
						builder2.Add(keyValuePair2.Key, keyValuePair2.Value.ToImmutable());
					}
				}
				builder.Add(key, builder2.ToImmutable());
				builder2.Clear();
			}
			return builder.ToImmutable();
		}

		// Token: 0x060020C5 RID: 8389 RVA: 0x00065564 File Offset: 0x00063764
		[return: Nullable(new byte[] { 1, 1, 0, 1 })]
		private static ImmutableDictionary<DiagnosticAnalyzer, ImmutableArray<Diagnostic>> GetImmutable(ImmutableHashSet<DiagnosticAnalyzer> analyzers, [Nullable(new byte[] { 2, 1, 1, 1 })] Dictionary<DiagnosticAnalyzer, ImmutableArray<Diagnostic>.Builder> nonLocalDiagnosticsOpt)
		{
			if (nonLocalDiagnosticsOpt == null)
			{
				return ImmutableDictionary<DiagnosticAnalyzer, ImmutableArray<Diagnostic>>.Empty;
			}
			ImmutableDictionary<DiagnosticAnalyzer, ImmutableArray<Diagnostic>>.Builder builder = ImmutableDictionary.CreateBuilder<DiagnosticAnalyzer, ImmutableArray<Diagnostic>>();
			foreach (KeyValuePair<DiagnosticAnalyzer, ImmutableArray<Diagnostic>.Builder> keyValuePair in nonLocalDiagnosticsOpt)
			{
				if (analyzers.Contains(keyValuePair.Key))
				{
					builder.Add(keyValuePair.Key, keyValuePair.Value.ToImmutable());
				}
			}
			return builder.ToImmutable();
		}

		// Token: 0x060020C6 RID: 8390 RVA: 0x000655E8 File Offset: 0x000637E8
		private ImmutableDictionary<DiagnosticAnalyzer, AnalyzerTelemetryInfo> GetTelemetryInfo([Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticAnalyzer> analyzers)
		{
			ImmutableDictionary<DiagnosticAnalyzer, AnalyzerTelemetryInfo>.Builder builder = ImmutableDictionary.CreateBuilder<DiagnosticAnalyzer, AnalyzerTelemetryInfo>();
			object gate = this._gate;
			lock (gate)
			{
				foreach (DiagnosticAnalyzer diagnosticAnalyzer in analyzers)
				{
					AnalyzerActionCounts empty;
					if (!this._analyzerActionCounts.TryGetValue(diagnosticAnalyzer, ref empty))
					{
						empty = AnalyzerActionCounts.Empty;
					}
					int num = ((diagnosticAnalyzer is DiagnosticSuppressor) ? 1 : 0);
					TimeSpan timeSpan = ((this._analyzerExecutionTimeOpt != null) ? this._analyzerExecutionTimeOpt[diagnosticAnalyzer] : default(TimeSpan));
					AnalyzerTelemetryInfo analyzerTelemetryInfo = new AnalyzerTelemetryInfo(empty, num, timeSpan);
					builder.Add(diagnosticAnalyzer, analyzerTelemetryInfo);
				}
			}
			return builder.ToImmutable();
		}

		// Token: 0x060020C8 RID: 8392 RVA: 0x000656C2 File Offset: 0x000638C2
		[CompilerGenerated]
		[return: Nullable(2)]
		internal static SyntaxTree <ApplySuppressionsAndStoreAnalysisResult>g__getSourceTree|15_0(Diagnostic diagnostic)
		{
			return diagnostic.Location.SourceTree;
		}

		// Token: 0x04000E39 RID: 3641
		[Nullable(new byte[] { 1, 1, 0, 1 })]
		private static readonly ImmutableDictionary<string, OneOrMany<AdditionalText>> s_emptyPathToAdditionalTextMap = ImmutableDictionary<string, OneOrMany<AdditionalText>>.Empty.WithComparers(PathUtilities.Comparer);

		// Token: 0x04000E3A RID: 3642
		private readonly object _gate = new object();

		// Token: 0x04000E3B RID: 3643
		[Nullable(new byte[] { 2, 1 })]
		private readonly Dictionary<DiagnosticAnalyzer, TimeSpan> _analyzerExecutionTimeOpt;

		// Token: 0x04000E3C RID: 3644
		private readonly HashSet<DiagnosticAnalyzer> _completedAnalyzers;

		// Token: 0x04000E3D RID: 3645
		private readonly Dictionary<DiagnosticAnalyzer, AnalyzerActionCounts> _analyzerActionCounts;

		// Token: 0x04000E3E RID: 3646
		[Nullable(new byte[] { 1, 1, 0, 1 })]
		private readonly ImmutableDictionary<string, OneOrMany<AdditionalText>> _pathToAdditionalTextMap;

		// Token: 0x04000E3F RID: 3647
		[Nullable(new byte[] { 2, 1, 1, 1, 1, 1 })]
		private Dictionary<SyntaxTree, Dictionary<DiagnosticAnalyzer, ImmutableArray<Diagnostic>.Builder>> _localSemanticDiagnosticsOpt;

		// Token: 0x04000E40 RID: 3648
		[Nullable(new byte[] { 2, 1, 1, 1, 1, 1 })]
		private Dictionary<SyntaxTree, Dictionary<DiagnosticAnalyzer, ImmutableArray<Diagnostic>.Builder>> _localSyntaxDiagnosticsOpt;

		// Token: 0x04000E41 RID: 3649
		[Nullable(new byte[] { 2, 1, 1, 1, 1, 1 })]
		private Dictionary<AdditionalText, Dictionary<DiagnosticAnalyzer, ImmutableArray<Diagnostic>.Builder>> _localAdditionalFileDiagnosticsOpt;

		// Token: 0x04000E42 RID: 3650
		[Nullable(new byte[] { 2, 1, 1, 1 })]
		private Dictionary<DiagnosticAnalyzer, ImmutableArray<Diagnostic>.Builder> _nonLocalDiagnosticsOpt;
	}
}
