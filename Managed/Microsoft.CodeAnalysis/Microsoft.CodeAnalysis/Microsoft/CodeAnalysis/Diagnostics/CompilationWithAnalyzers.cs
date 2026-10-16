using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.Diagnostics.Telemetry;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x02000315 RID: 789
	[NullableContext(1)]
	[Nullable(0)]
	public class CompilationWithAnalyzers
	{
		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x06002274 RID: 8820 RVA: 0x0006D98A File Offset: 0x0006BB8A
		public Compilation Compilation
		{
			get
			{
				return this._compilation;
			}
		}

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x06002275 RID: 8821 RVA: 0x0006D992 File Offset: 0x0006BB92
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<DiagnosticAnalyzer> Analyzers
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._analyzers;
			}
		}

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x06002276 RID: 8822 RVA: 0x0006D99A File Offset: 0x0006BB9A
		public CompilationWithAnalyzersOptions AnalysisOptions
		{
			get
			{
				return this._analysisOptions;
			}
		}

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x06002277 RID: 8823 RVA: 0x0006D9A2 File Offset: 0x0006BBA2
		public CancellationToken CancellationToken
		{
			get
			{
				return this._cancellationToken;
			}
		}

		// Token: 0x06002278 RID: 8824 RVA: 0x0006D9AC File Offset: 0x0006BBAC
		public CompilationWithAnalyzers(Compilation compilation, [Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticAnalyzer> analyzers, [Nullable(2)] AnalyzerOptions options, CancellationToken cancellationToken)
			: this(compilation, analyzers, new CompilationWithAnalyzersOptions(options, null, true, true, false, null), cancellationToken)
		{
		}

		// Token: 0x06002279 RID: 8825 RVA: 0x0006D9CE File Offset: 0x0006BBCE
		public CompilationWithAnalyzers(Compilation compilation, [Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticAnalyzer> analyzers, CompilationWithAnalyzersOptions analysisOptions)
			: this(compilation, analyzers, analysisOptions, CancellationToken.None)
		{
		}

		// Token: 0x0600227A RID: 8826 RVA: 0x0006D9E0 File Offset: 0x0006BBE0
		private CompilationWithAnalyzers(Compilation compilation, [Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticAnalyzer> analyzers, CompilationWithAnalyzersOptions analysisOptions, CancellationToken cancellationToken)
		{
			CompilationWithAnalyzers <>4__this = this;
			CompilationWithAnalyzers.VerifyArguments(compilation, analyzers, analysisOptions);
			compilation = compilation.WithOptions(compilation.Options.WithReportSuppressedDiagnostics(analysisOptions.ReportSuppressedDiagnostics)).WithSemanticModelProvider(new CachingSemanticModelProvider()).WithEventQueue(new AsyncQueue<CompilationEvent>());
			this._compilation = compilation;
			this._analyzers = analyzers;
			this._analysisOptions = analysisOptions;
			this._cancellationToken = cancellationToken;
			this._compilationData = new AnalyzerDriver.CompilationData(this._compilation);
			this._analysisState = new AnalysisState(analyzers, this._compilationData.SemanticModelProvider, this._compilation.Options);
			bool logAnalyzerExecutionTime = analysisOptions.LogAnalyzerExecutionTime;
			ImmutableArray<DiagnosticAnalyzer> analyzers2 = analyzers;
			AnalyzerOptions options = this._analysisOptions.Options;
			this._analysisResultBuilder = new AnalysisResultBuilder(logAnalyzerExecutionTime, analyzers2, (options != null) ? options.AdditionalFiles : ImmutableArray<AdditionalText>.Empty);
			this._analyzerManager = new AnalyzerManager(analyzers);
			this._driverPool = new ObjectPool<AnalyzerDriver>(() => <>4__this._compilation.CreateAnalyzerDriver(analyzers, <>4__this._analyzerManager, SeverityFilter.None));
			this._executingConcurrentTreeTasksOpt = (analysisOptions.ConcurrentAnalysis ? new Dictionary<SourceOrAdditionalFile, Tuple<Task, CancellationTokenSource>>() : null);
			this._concurrentTreeTaskTokensOpt = (analysisOptions.ConcurrentAnalysis ? new Dictionary<Task, int>() : null);
			this._executingCompilationOrNonConcurrentTreeTask = null;
		}

		// Token: 0x0600227B RID: 8827 RVA: 0x0006DB68 File Offset: 0x0006BD68
		private static void VerifyArguments(Compilation compilation, [Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticAnalyzer> analyzers, CompilationWithAnalyzersOptions analysisOptions)
		{
			if (compilation == null)
			{
				throw new ArgumentNullException("compilation");
			}
			if (analysisOptions == null)
			{
				throw new ArgumentNullException("analysisOptions");
			}
			CompilationWithAnalyzers.VerifyAnalyzersArgumentForStaticApis(analyzers, false);
		}

		// Token: 0x0600227C RID: 8828 RVA: 0x0006DB90 File Offset: 0x0006BD90
		private static void VerifyAnalyzersArgumentForStaticApis([Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticAnalyzer> analyzers, bool allowDefaultOrEmpty = false)
		{
			if (analyzers.IsDefaultOrEmpty)
			{
				if (allowDefaultOrEmpty)
				{
					return;
				}
				throw new ArgumentException(CodeAnalysisResources.ArgumentCannotBeEmpty, "analyzers");
			}
			else
			{
				if (analyzers.Any<DiagnosticAnalyzer>((DiagnosticAnalyzer a) => a == null))
				{
					throw new ArgumentException(CodeAnalysisResources.ArgumentElementCannotBeNull, "analyzers");
				}
				if (analyzers.Distinct<DiagnosticAnalyzer>(null).Length != analyzers.Length)
				{
					throw new ArgumentException(CodeAnalysisResources.DuplicateAnalyzerInstances, "analyzers");
				}
				return;
			}
		}

		// Token: 0x0600227D RID: 8829 RVA: 0x0006DC1C File Offset: 0x0006BE1C
		private void VerifyAnalyzerArgument(DiagnosticAnalyzer analyzer)
		{
			CompilationWithAnalyzers.VerifyAnalyzerArgumentForStaticApis(analyzer);
			if (!this._analyzers.Contains(analyzer))
			{
				throw new ArgumentException(CodeAnalysisResources.UnsupportedAnalyzerInstance, "analyzer");
			}
		}

		// Token: 0x0600227E RID: 8830 RVA: 0x0006DC50 File Offset: 0x0006BE50
		private static void VerifyAnalyzerArgumentForStaticApis(DiagnosticAnalyzer analyzer)
		{
			if (analyzer == null)
			{
				throw new ArgumentException(CodeAnalysisResources.ArgumentCannotBeEmpty, "analyzer");
			}
		}

		// Token: 0x0600227F RID: 8831 RVA: 0x0006DC68 File Offset: 0x0006BE68
		private void VerifyExistingAnalyzersArgument([Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticAnalyzer> analyzers)
		{
			CompilationWithAnalyzers.VerifyAnalyzersArgumentForStaticApis(analyzers, false);
			if (analyzers.Any<DiagnosticAnalyzer>((DiagnosticAnalyzer a) => !this._analyzers.Contains(a)))
			{
				throw new ArgumentException(CodeAnalysisResources.UnsupportedAnalyzerInstance, "_analyzers");
			}
			if (analyzers.Distinct<DiagnosticAnalyzer>(null).Length != analyzers.Length)
			{
				throw new ArgumentException(CodeAnalysisResources.DuplicateAnalyzerInstances, "analyzers");
			}
		}

		// Token: 0x06002280 RID: 8832 RVA: 0x0006DCC8 File Offset: 0x0006BEC8
		private void VerifyModel(SemanticModel model)
		{
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}
			if (!this._compilation.ContainsSyntaxTree(model.SyntaxTree))
			{
				throw new ArgumentException(CodeAnalysisResources.InvalidTree, "model");
			}
		}

		// Token: 0x06002281 RID: 8833 RVA: 0x0006DCFB File Offset: 0x0006BEFB
		private void VerifyTree(SyntaxTree tree)
		{
			if (tree == null)
			{
				throw new ArgumentNullException("tree");
			}
			if (!this._compilation.ContainsSyntaxTree(tree))
			{
				throw new ArgumentException(CodeAnalysisResources.InvalidTree, "tree");
			}
		}

		// Token: 0x06002282 RID: 8834 RVA: 0x0006DD2C File Offset: 0x0006BF2C
		private void VerifyAdditionalFile(AdditionalText file)
		{
			if (file == null)
			{
				throw new ArgumentNullException("file");
			}
			if (this._analysisOptions.Options == null || !this._analysisOptions.Options.AdditionalFiles.Contains(file))
			{
				throw new ArgumentException(CodeAnalysisResources.InvalidAdditionalFile, "file");
			}
		}

		// Token: 0x06002283 RID: 8835 RVA: 0x0006DD7F File Offset: 0x0006BF7F
		[return: Nullable(new byte[] { 1, 0, 1 })]
		public Task<ImmutableArray<Diagnostic>> GetAnalyzerDiagnosticsAsync()
		{
			return this.GetAnalyzerDiagnosticsAsync(this._cancellationToken);
		}

		// Token: 0x06002284 RID: 8836 RVA: 0x0006DD90 File Offset: 0x0006BF90
		[return: Nullable(new byte[] { 1, 0, 1 })]
		public Task<ImmutableArray<Diagnostic>> GetAnalyzerDiagnosticsAsync(CancellationToken cancellationToken)
		{
			CompilationWithAnalyzers.<GetAnalyzerDiagnosticsAsync>d__37 <GetAnalyzerDiagnosticsAsync>d__;
			<GetAnalyzerDiagnosticsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<ImmutableArray<Diagnostic>>.Create();
			<GetAnalyzerDiagnosticsAsync>d__.<>4__this = this;
			<GetAnalyzerDiagnosticsAsync>d__.cancellationToken = cancellationToken;
			<GetAnalyzerDiagnosticsAsync>d__.<>1__state = -1;
			<GetAnalyzerDiagnosticsAsync>d__.<>t__builder.Start<CompilationWithAnalyzers.<GetAnalyzerDiagnosticsAsync>d__37>(ref <GetAnalyzerDiagnosticsAsync>d__);
			return <GetAnalyzerDiagnosticsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06002285 RID: 8837 RVA: 0x0006DDDC File Offset: 0x0006BFDC
		[return: Nullable(new byte[] { 1, 0, 1 })]
		public Task<ImmutableArray<Diagnostic>> GetAnalyzerDiagnosticsAsync([Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticAnalyzer> analyzers, CancellationToken cancellationToken)
		{
			CompilationWithAnalyzers.<GetAnalyzerDiagnosticsAsync>d__38 <GetAnalyzerDiagnosticsAsync>d__;
			<GetAnalyzerDiagnosticsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<ImmutableArray<Diagnostic>>.Create();
			<GetAnalyzerDiagnosticsAsync>d__.<>4__this = this;
			<GetAnalyzerDiagnosticsAsync>d__.analyzers = analyzers;
			<GetAnalyzerDiagnosticsAsync>d__.cancellationToken = cancellationToken;
			<GetAnalyzerDiagnosticsAsync>d__.<>1__state = -1;
			<GetAnalyzerDiagnosticsAsync>d__.<>t__builder.Start<CompilationWithAnalyzers.<GetAnalyzerDiagnosticsAsync>d__38>(ref <GetAnalyzerDiagnosticsAsync>d__);
			return <GetAnalyzerDiagnosticsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06002286 RID: 8838 RVA: 0x0006DE30 File Offset: 0x0006C030
		public Task<AnalysisResult> GetAnalysisResultAsync(CancellationToken cancellationToken)
		{
			CompilationWithAnalyzers.<GetAnalysisResultAsync>d__39 <GetAnalysisResultAsync>d__;
			<GetAnalysisResultAsync>d__.<>t__builder = AsyncTaskMethodBuilder<AnalysisResult>.Create();
			<GetAnalysisResultAsync>d__.<>4__this = this;
			<GetAnalysisResultAsync>d__.cancellationToken = cancellationToken;
			<GetAnalysisResultAsync>d__.<>1__state = -1;
			<GetAnalysisResultAsync>d__.<>t__builder.Start<CompilationWithAnalyzers.<GetAnalysisResultAsync>d__39>(ref <GetAnalysisResultAsync>d__);
			return <GetAnalysisResultAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06002287 RID: 8839 RVA: 0x0006DE7C File Offset: 0x0006C07C
		public Task<AnalysisResult> GetAnalysisResultAsync([Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticAnalyzer> analyzers, CancellationToken cancellationToken)
		{
			CompilationWithAnalyzers.<GetAnalysisResultAsync>d__40 <GetAnalysisResultAsync>d__;
			<GetAnalysisResultAsync>d__.<>t__builder = AsyncTaskMethodBuilder<AnalysisResult>.Create();
			<GetAnalysisResultAsync>d__.<>4__this = this;
			<GetAnalysisResultAsync>d__.analyzers = analyzers;
			<GetAnalysisResultAsync>d__.cancellationToken = cancellationToken;
			<GetAnalysisResultAsync>d__.<>1__state = -1;
			<GetAnalysisResultAsync>d__.<>t__builder.Start<CompilationWithAnalyzers.<GetAnalysisResultAsync>d__40>(ref <GetAnalysisResultAsync>d__);
			return <GetAnalysisResultAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06002288 RID: 8840 RVA: 0x0006DECF File Offset: 0x0006C0CF
		[return: Nullable(new byte[] { 1, 0, 1 })]
		public Task<ImmutableArray<Diagnostic>> GetAllDiagnosticsAsync()
		{
			return this.GetAllDiagnosticsAsync(this._cancellationToken);
		}

		// Token: 0x06002289 RID: 8841 RVA: 0x0006DEE0 File Offset: 0x0006C0E0
		[return: Nullable(new byte[] { 1, 0, 1 })]
		public Task<ImmutableArray<Diagnostic>> GetAllDiagnosticsAsync(CancellationToken cancellationToken)
		{
			CompilationWithAnalyzers.<GetAllDiagnosticsAsync>d__42 <GetAllDiagnosticsAsync>d__;
			<GetAllDiagnosticsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<ImmutableArray<Diagnostic>>.Create();
			<GetAllDiagnosticsAsync>d__.<>4__this = this;
			<GetAllDiagnosticsAsync>d__.cancellationToken = cancellationToken;
			<GetAllDiagnosticsAsync>d__.<>1__state = -1;
			<GetAllDiagnosticsAsync>d__.<>t__builder.Start<CompilationWithAnalyzers.<GetAllDiagnosticsAsync>d__42>(ref <GetAllDiagnosticsAsync>d__);
			return <GetAllDiagnosticsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600228A RID: 8842 RVA: 0x0006DF2C File Offset: 0x0006C12C
		[Obsolete("This API was found to have performance issues and hence has been deprecated. Instead, invoke the API 'GetAnalysisResultAsync' and access the property 'CompilationDiagnostics' on the returned 'AnalysisResult' to fetch the compilation diagnostics.")]
		[return: Nullable(new byte[] { 1, 0, 1 })]
		public Task<ImmutableArray<Diagnostic>> GetAnalyzerCompilationDiagnosticsAsync(CancellationToken cancellationToken)
		{
			CompilationWithAnalyzers.<GetAnalyzerCompilationDiagnosticsAsync>d__43 <GetAnalyzerCompilationDiagnosticsAsync>d__;
			<GetAnalyzerCompilationDiagnosticsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<ImmutableArray<Diagnostic>>.Create();
			<GetAnalyzerCompilationDiagnosticsAsync>d__.<>4__this = this;
			<GetAnalyzerCompilationDiagnosticsAsync>d__.cancellationToken = cancellationToken;
			<GetAnalyzerCompilationDiagnosticsAsync>d__.<>1__state = -1;
			<GetAnalyzerCompilationDiagnosticsAsync>d__.<>t__builder.Start<CompilationWithAnalyzers.<GetAnalyzerCompilationDiagnosticsAsync>d__43>(ref <GetAnalyzerCompilationDiagnosticsAsync>d__);
			return <GetAnalyzerCompilationDiagnosticsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600228B RID: 8843 RVA: 0x0006DF78 File Offset: 0x0006C178
		[Obsolete("This API was found to have performance issues and hence has been deprecated. Instead, invoke the API 'GetAnalysisResultAsync' and access the property 'CompilationDiagnostics' on the returned 'AnalysisResult' to fetch the compilation diagnostics.")]
		[return: Nullable(new byte[] { 1, 0, 1 })]
		public Task<ImmutableArray<Diagnostic>> GetAnalyzerCompilationDiagnosticsAsync([Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticAnalyzer> analyzers, CancellationToken cancellationToken)
		{
			CompilationWithAnalyzers.<GetAnalyzerCompilationDiagnosticsAsync>d__44 <GetAnalyzerCompilationDiagnosticsAsync>d__;
			<GetAnalyzerCompilationDiagnosticsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<ImmutableArray<Diagnostic>>.Create();
			<GetAnalyzerCompilationDiagnosticsAsync>d__.<>4__this = this;
			<GetAnalyzerCompilationDiagnosticsAsync>d__.analyzers = analyzers;
			<GetAnalyzerCompilationDiagnosticsAsync>d__.cancellationToken = cancellationToken;
			<GetAnalyzerCompilationDiagnosticsAsync>d__.<>1__state = -1;
			<GetAnalyzerCompilationDiagnosticsAsync>d__.<>t__builder.Start<CompilationWithAnalyzers.<GetAnalyzerCompilationDiagnosticsAsync>d__44>(ref <GetAnalyzerCompilationDiagnosticsAsync>d__);
			return <GetAnalyzerCompilationDiagnosticsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600228C RID: 8844 RVA: 0x0006DFCC File Offset: 0x0006C1CC
		[return: Nullable(new byte[] { 1, 0, 1 })]
		private Task<ImmutableArray<Diagnostic>> GetAnalyzerCompilationDiagnosticsCoreAsync([Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticAnalyzer> analyzers, CancellationToken cancellationToken)
		{
			CompilationWithAnalyzers.<GetAnalyzerCompilationDiagnosticsCoreAsync>d__45 <GetAnalyzerCompilationDiagnosticsCoreAsync>d__;
			<GetAnalyzerCompilationDiagnosticsCoreAsync>d__.<>t__builder = AsyncTaskMethodBuilder<ImmutableArray<Diagnostic>>.Create();
			<GetAnalyzerCompilationDiagnosticsCoreAsync>d__.<>4__this = this;
			<GetAnalyzerCompilationDiagnosticsCoreAsync>d__.analyzers = analyzers;
			<GetAnalyzerCompilationDiagnosticsCoreAsync>d__.cancellationToken = cancellationToken;
			<GetAnalyzerCompilationDiagnosticsCoreAsync>d__.<>1__state = -1;
			<GetAnalyzerCompilationDiagnosticsCoreAsync>d__.<>t__builder.Start<CompilationWithAnalyzers.<GetAnalyzerCompilationDiagnosticsCoreAsync>d__45>(ref <GetAnalyzerCompilationDiagnosticsCoreAsync>d__);
			return <GetAnalyzerCompilationDiagnosticsCoreAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600228D RID: 8845 RVA: 0x0006E020 File Offset: 0x0006C220
		private Task<AnalysisResult> GetAnalysisResultWithoutStateTrackingAsync([Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticAnalyzer> analyzers, CancellationToken cancellationToken)
		{
			CompilationWithAnalyzers.<GetAnalysisResultWithoutStateTrackingAsync>d__46 <GetAnalysisResultWithoutStateTrackingAsync>d__;
			<GetAnalysisResultWithoutStateTrackingAsync>d__.<>t__builder = AsyncTaskMethodBuilder<AnalysisResult>.Create();
			<GetAnalysisResultWithoutStateTrackingAsync>d__.<>4__this = this;
			<GetAnalysisResultWithoutStateTrackingAsync>d__.analyzers = analyzers;
			<GetAnalysisResultWithoutStateTrackingAsync>d__.cancellationToken = cancellationToken;
			<GetAnalysisResultWithoutStateTrackingAsync>d__.<>1__state = -1;
			<GetAnalysisResultWithoutStateTrackingAsync>d__.<>t__builder.Start<CompilationWithAnalyzers.<GetAnalysisResultWithoutStateTrackingAsync>d__46>(ref <GetAnalysisResultWithoutStateTrackingAsync>d__);
			return <GetAnalysisResultWithoutStateTrackingAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600228E RID: 8846 RVA: 0x0006E074 File Offset: 0x0006C274
		[return: Nullable(new byte[] { 1, 0, 1 })]
		private Task<ImmutableArray<Diagnostic>> GetAnalyzerDiagnosticsWithoutStateTrackingAsync([Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticAnalyzer> analyzers, CancellationToken cancellationToken)
		{
			CompilationWithAnalyzers.<GetAnalyzerDiagnosticsWithoutStateTrackingAsync>d__47 <GetAnalyzerDiagnosticsWithoutStateTrackingAsync>d__;
			<GetAnalyzerDiagnosticsWithoutStateTrackingAsync>d__.<>t__builder = AsyncTaskMethodBuilder<ImmutableArray<Diagnostic>>.Create();
			<GetAnalyzerDiagnosticsWithoutStateTrackingAsync>d__.<>4__this = this;
			<GetAnalyzerDiagnosticsWithoutStateTrackingAsync>d__.analyzers = analyzers;
			<GetAnalyzerDiagnosticsWithoutStateTrackingAsync>d__.cancellationToken = cancellationToken;
			<GetAnalyzerDiagnosticsWithoutStateTrackingAsync>d__.<>1__state = -1;
			<GetAnalyzerDiagnosticsWithoutStateTrackingAsync>d__.<>t__builder.Start<CompilationWithAnalyzers.<GetAnalyzerDiagnosticsWithoutStateTrackingAsync>d__47>(ref <GetAnalyzerDiagnosticsWithoutStateTrackingAsync>d__);
			return <GetAnalyzerDiagnosticsWithoutStateTrackingAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600228F RID: 8847 RVA: 0x0006E0C7 File Offset: 0x0006C2C7
		private static AnalyzerDriver CreateDriverForComputingDiagnosticsWithoutStateTracking(Compilation compilation, [Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticAnalyzer> analyzers)
		{
			return compilation.CreateAnalyzerDriver(analyzers, new AnalyzerManager(analyzers), SeverityFilter.None);
		}

		// Token: 0x06002290 RID: 8848 RVA: 0x0006E0D8 File Offset: 0x0006C2D8
		private Task ComputeAnalyzerDiagnosticsWithoutStateTrackingAsync(CancellationToken cancellationToken)
		{
			CompilationWithAnalyzers.<ComputeAnalyzerDiagnosticsWithoutStateTrackingAsync>d__49 <ComputeAnalyzerDiagnosticsWithoutStateTrackingAsync>d__;
			<ComputeAnalyzerDiagnosticsWithoutStateTrackingAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ComputeAnalyzerDiagnosticsWithoutStateTrackingAsync>d__.<>4__this = this;
			<ComputeAnalyzerDiagnosticsWithoutStateTrackingAsync>d__.cancellationToken = cancellationToken;
			<ComputeAnalyzerDiagnosticsWithoutStateTrackingAsync>d__.<>1__state = -1;
			<ComputeAnalyzerDiagnosticsWithoutStateTrackingAsync>d__.<>t__builder.Start<CompilationWithAnalyzers.<ComputeAnalyzerDiagnosticsWithoutStateTrackingAsync>d__49>(ref <ComputeAnalyzerDiagnosticsWithoutStateTrackingAsync>d__);
			return <ComputeAnalyzerDiagnosticsWithoutStateTrackingAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06002291 RID: 8849 RVA: 0x0006E124 File Offset: 0x0006C324
		[return: Nullable(new byte[] { 1, 0, 1 })]
		private Task<ImmutableArray<Diagnostic>> GetAllDiagnosticsWithoutStateTrackingAsync([Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticAnalyzer> analyzers, CancellationToken cancellationToken)
		{
			CompilationWithAnalyzers.<GetAllDiagnosticsWithoutStateTrackingAsync>d__50 <GetAllDiagnosticsWithoutStateTrackingAsync>d__;
			<GetAllDiagnosticsWithoutStateTrackingAsync>d__.<>t__builder = AsyncTaskMethodBuilder<ImmutableArray<Diagnostic>>.Create();
			<GetAllDiagnosticsWithoutStateTrackingAsync>d__.<>4__this = this;
			<GetAllDiagnosticsWithoutStateTrackingAsync>d__.analyzers = analyzers;
			<GetAllDiagnosticsWithoutStateTrackingAsync>d__.cancellationToken = cancellationToken;
			<GetAllDiagnosticsWithoutStateTrackingAsync>d__.<>1__state = -1;
			<GetAllDiagnosticsWithoutStateTrackingAsync>d__.<>t__builder.Start<CompilationWithAnalyzers.<GetAllDiagnosticsWithoutStateTrackingAsync>d__50>(ref <GetAllDiagnosticsWithoutStateTrackingAsync>d__);
			return <GetAllDiagnosticsWithoutStateTrackingAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06002292 RID: 8850 RVA: 0x0006E178 File Offset: 0x0006C378
		[return: Nullable(new byte[] { 1, 0, 1 })]
		public Task<ImmutableArray<Diagnostic>> GetAnalyzerSyntaxDiagnosticsAsync(SyntaxTree tree, CancellationToken cancellationToken)
		{
			CompilationWithAnalyzers.<GetAnalyzerSyntaxDiagnosticsAsync>d__51 <GetAnalyzerSyntaxDiagnosticsAsync>d__;
			<GetAnalyzerSyntaxDiagnosticsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<ImmutableArray<Diagnostic>>.Create();
			<GetAnalyzerSyntaxDiagnosticsAsync>d__.<>4__this = this;
			<GetAnalyzerSyntaxDiagnosticsAsync>d__.tree = tree;
			<GetAnalyzerSyntaxDiagnosticsAsync>d__.cancellationToken = cancellationToken;
			<GetAnalyzerSyntaxDiagnosticsAsync>d__.<>1__state = -1;
			<GetAnalyzerSyntaxDiagnosticsAsync>d__.<>t__builder.Start<CompilationWithAnalyzers.<GetAnalyzerSyntaxDiagnosticsAsync>d__51>(ref <GetAnalyzerSyntaxDiagnosticsAsync>d__);
			return <GetAnalyzerSyntaxDiagnosticsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06002293 RID: 8851 RVA: 0x0006E1CC File Offset: 0x0006C3CC
		[return: Nullable(new byte[] { 1, 0, 1 })]
		public Task<ImmutableArray<Diagnostic>> GetAnalyzerSyntaxDiagnosticsAsync(SyntaxTree tree, [Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticAnalyzer> analyzers, CancellationToken cancellationToken)
		{
			CompilationWithAnalyzers.<GetAnalyzerSyntaxDiagnosticsAsync>d__52 <GetAnalyzerSyntaxDiagnosticsAsync>d__;
			<GetAnalyzerSyntaxDiagnosticsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<ImmutableArray<Diagnostic>>.Create();
			<GetAnalyzerSyntaxDiagnosticsAsync>d__.<>4__this = this;
			<GetAnalyzerSyntaxDiagnosticsAsync>d__.tree = tree;
			<GetAnalyzerSyntaxDiagnosticsAsync>d__.analyzers = analyzers;
			<GetAnalyzerSyntaxDiagnosticsAsync>d__.cancellationToken = cancellationToken;
			<GetAnalyzerSyntaxDiagnosticsAsync>d__.<>1__state = -1;
			<GetAnalyzerSyntaxDiagnosticsAsync>d__.<>t__builder.Start<CompilationWithAnalyzers.<GetAnalyzerSyntaxDiagnosticsAsync>d__52>(ref <GetAnalyzerSyntaxDiagnosticsAsync>d__);
			return <GetAnalyzerSyntaxDiagnosticsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06002294 RID: 8852 RVA: 0x0006E227 File Offset: 0x0006C427
		public Task<AnalysisResult> GetAnalysisResultAsync(SyntaxTree tree, CancellationToken cancellationToken)
		{
			this.VerifyTree(tree);
			return this.GetAnalysisResultCoreAsync(new SourceOrAdditionalFile(tree), this.Analyzers, cancellationToken);
		}

		// Token: 0x06002295 RID: 8853 RVA: 0x0006E243 File Offset: 0x0006C443
		public Task<AnalysisResult> GetAnalysisResultAsync(SyntaxTree tree, [Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticAnalyzer> analyzers, CancellationToken cancellationToken)
		{
			this.VerifyTree(tree);
			this.VerifyExistingAnalyzersArgument(analyzers);
			return this.GetAnalysisResultCoreAsync(new SourceOrAdditionalFile(tree), analyzers, cancellationToken);
		}

		// Token: 0x06002296 RID: 8854 RVA: 0x0006E264 File Offset: 0x0006C464
		public Task<AnalysisResult> GetAnalysisResultAsync(AdditionalText file, CancellationToken cancellationToken)
		{
			CompilationWithAnalyzers.<GetAnalysisResultAsync>d__55 <GetAnalysisResultAsync>d__;
			<GetAnalysisResultAsync>d__.<>t__builder = AsyncTaskMethodBuilder<AnalysisResult>.Create();
			<GetAnalysisResultAsync>d__.<>4__this = this;
			<GetAnalysisResultAsync>d__.file = file;
			<GetAnalysisResultAsync>d__.cancellationToken = cancellationToken;
			<GetAnalysisResultAsync>d__.<>1__state = -1;
			<GetAnalysisResultAsync>d__.<>t__builder.Start<CompilationWithAnalyzers.<GetAnalysisResultAsync>d__55>(ref <GetAnalysisResultAsync>d__);
			return <GetAnalysisResultAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06002297 RID: 8855 RVA: 0x0006E2B8 File Offset: 0x0006C4B8
		public Task<AnalysisResult> GetAnalysisResultAsync(AdditionalText file, [Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticAnalyzer> analyzers, CancellationToken cancellationToken)
		{
			CompilationWithAnalyzers.<GetAnalysisResultAsync>d__56 <GetAnalysisResultAsync>d__;
			<GetAnalysisResultAsync>d__.<>t__builder = AsyncTaskMethodBuilder<AnalysisResult>.Create();
			<GetAnalysisResultAsync>d__.<>4__this = this;
			<GetAnalysisResultAsync>d__.file = file;
			<GetAnalysisResultAsync>d__.analyzers = analyzers;
			<GetAnalysisResultAsync>d__.cancellationToken = cancellationToken;
			<GetAnalysisResultAsync>d__.<>1__state = -1;
			<GetAnalysisResultAsync>d__.<>t__builder.Start<CompilationWithAnalyzers.<GetAnalysisResultAsync>d__56>(ref <GetAnalysisResultAsync>d__);
			return <GetAnalysisResultAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06002298 RID: 8856 RVA: 0x0006E314 File Offset: 0x0006C514
		private Task<AnalysisResult> GetAnalysisResultCoreAsync(SourceOrAdditionalFile file, [Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticAnalyzer> analyzers, CancellationToken cancellationToken)
		{
			CompilationWithAnalyzers.<GetAnalysisResultCoreAsync>d__57 <GetAnalysisResultCoreAsync>d__;
			<GetAnalysisResultCoreAsync>d__.<>t__builder = AsyncTaskMethodBuilder<AnalysisResult>.Create();
			<GetAnalysisResultCoreAsync>d__.<>4__this = this;
			<GetAnalysisResultCoreAsync>d__.file = file;
			<GetAnalysisResultCoreAsync>d__.analyzers = analyzers;
			<GetAnalysisResultCoreAsync>d__.cancellationToken = cancellationToken;
			<GetAnalysisResultCoreAsync>d__.<>1__state = -1;
			<GetAnalysisResultCoreAsync>d__.<>t__builder.Start<CompilationWithAnalyzers.<GetAnalysisResultCoreAsync>d__57>(ref <GetAnalysisResultCoreAsync>d__);
			return <GetAnalysisResultCoreAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06002299 RID: 8857 RVA: 0x0006E370 File Offset: 0x0006C570
		[return: Nullable(new byte[] { 1, 0, 1 })]
		private Task<ImmutableArray<Diagnostic>> GetAnalyzerSyntaxDiagnosticsCoreAsync(SyntaxTree tree, [Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticAnalyzer> analyzers, CancellationToken cancellationToken)
		{
			CompilationWithAnalyzers.<GetAnalyzerSyntaxDiagnosticsCoreAsync>d__58 <GetAnalyzerSyntaxDiagnosticsCoreAsync>d__;
			<GetAnalyzerSyntaxDiagnosticsCoreAsync>d__.<>t__builder = AsyncTaskMethodBuilder<ImmutableArray<Diagnostic>>.Create();
			<GetAnalyzerSyntaxDiagnosticsCoreAsync>d__.<>4__this = this;
			<GetAnalyzerSyntaxDiagnosticsCoreAsync>d__.tree = tree;
			<GetAnalyzerSyntaxDiagnosticsCoreAsync>d__.analyzers = analyzers;
			<GetAnalyzerSyntaxDiagnosticsCoreAsync>d__.cancellationToken = cancellationToken;
			<GetAnalyzerSyntaxDiagnosticsCoreAsync>d__.<>1__state = -1;
			<GetAnalyzerSyntaxDiagnosticsCoreAsync>d__.<>t__builder.Start<CompilationWithAnalyzers.<GetAnalyzerSyntaxDiagnosticsCoreAsync>d__58>(ref <GetAnalyzerSyntaxDiagnosticsCoreAsync>d__);
			return <GetAnalyzerSyntaxDiagnosticsCoreAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600229A RID: 8858 RVA: 0x0006E3CC File Offset: 0x0006C5CC
		private Task ComputeAnalyzerSyntaxDiagnosticsAsync(AnalysisScope analysisScope, CancellationToken cancellationToken)
		{
			CompilationWithAnalyzers.<ComputeAnalyzerSyntaxDiagnosticsAsync>d__59 <ComputeAnalyzerSyntaxDiagnosticsAsync>d__;
			<ComputeAnalyzerSyntaxDiagnosticsAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ComputeAnalyzerSyntaxDiagnosticsAsync>d__.<>4__this = this;
			<ComputeAnalyzerSyntaxDiagnosticsAsync>d__.analysisScope = analysisScope;
			<ComputeAnalyzerSyntaxDiagnosticsAsync>d__.cancellationToken = cancellationToken;
			<ComputeAnalyzerSyntaxDiagnosticsAsync>d__.<>1__state = -1;
			<ComputeAnalyzerSyntaxDiagnosticsAsync>d__.<>t__builder.Start<CompilationWithAnalyzers.<ComputeAnalyzerSyntaxDiagnosticsAsync>d__59>(ref <ComputeAnalyzerSyntaxDiagnosticsAsync>d__);
			return <ComputeAnalyzerSyntaxDiagnosticsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600229B RID: 8859 RVA: 0x0006E420 File Offset: 0x0006C620
		[return: Nullable(new byte[] { 1, 0, 1 })]
		public Task<ImmutableArray<Diagnostic>> GetAnalyzerSemanticDiagnosticsAsync(SemanticModel model, TextSpan? filterSpan, CancellationToken cancellationToken)
		{
			CompilationWithAnalyzers.<GetAnalyzerSemanticDiagnosticsAsync>d__60 <GetAnalyzerSemanticDiagnosticsAsync>d__;
			<GetAnalyzerSemanticDiagnosticsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<ImmutableArray<Diagnostic>>.Create();
			<GetAnalyzerSemanticDiagnosticsAsync>d__.<>4__this = this;
			<GetAnalyzerSemanticDiagnosticsAsync>d__.model = model;
			<GetAnalyzerSemanticDiagnosticsAsync>d__.filterSpan = filterSpan;
			<GetAnalyzerSemanticDiagnosticsAsync>d__.cancellationToken = cancellationToken;
			<GetAnalyzerSemanticDiagnosticsAsync>d__.<>1__state = -1;
			<GetAnalyzerSemanticDiagnosticsAsync>d__.<>t__builder.Start<CompilationWithAnalyzers.<GetAnalyzerSemanticDiagnosticsAsync>d__60>(ref <GetAnalyzerSemanticDiagnosticsAsync>d__);
			return <GetAnalyzerSemanticDiagnosticsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600229C RID: 8860 RVA: 0x0006E47C File Offset: 0x0006C67C
		[return: Nullable(new byte[] { 1, 0, 1 })]
		public Task<ImmutableArray<Diagnostic>> GetAnalyzerSemanticDiagnosticsAsync(SemanticModel model, TextSpan? filterSpan, [Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticAnalyzer> analyzers, CancellationToken cancellationToken)
		{
			CompilationWithAnalyzers.<GetAnalyzerSemanticDiagnosticsAsync>d__61 <GetAnalyzerSemanticDiagnosticsAsync>d__;
			<GetAnalyzerSemanticDiagnosticsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<ImmutableArray<Diagnostic>>.Create();
			<GetAnalyzerSemanticDiagnosticsAsync>d__.<>4__this = this;
			<GetAnalyzerSemanticDiagnosticsAsync>d__.model = model;
			<GetAnalyzerSemanticDiagnosticsAsync>d__.filterSpan = filterSpan;
			<GetAnalyzerSemanticDiagnosticsAsync>d__.analyzers = analyzers;
			<GetAnalyzerSemanticDiagnosticsAsync>d__.cancellationToken = cancellationToken;
			<GetAnalyzerSemanticDiagnosticsAsync>d__.<>1__state = -1;
			<GetAnalyzerSemanticDiagnosticsAsync>d__.<>t__builder.Start<CompilationWithAnalyzers.<GetAnalyzerSemanticDiagnosticsAsync>d__61>(ref <GetAnalyzerSemanticDiagnosticsAsync>d__);
			return <GetAnalyzerSemanticDiagnosticsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600229D RID: 8861 RVA: 0x0006E4E0 File Offset: 0x0006C6E0
		public Task<AnalysisResult> GetAnalysisResultAsync(SemanticModel model, TextSpan? filterSpan, CancellationToken cancellationToken)
		{
			this.VerifyModel(model);
			return this.GetAnalysisResultCoreAsync(model, filterSpan, this.Analyzers, cancellationToken);
		}

		// Token: 0x0600229E RID: 8862 RVA: 0x0006E4F8 File Offset: 0x0006C6F8
		public Task<AnalysisResult> GetAnalysisResultAsync(SemanticModel model, TextSpan? filterSpan, [Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticAnalyzer> analyzers, CancellationToken cancellationToken)
		{
			this.VerifyModel(model);
			this.VerifyExistingAnalyzersArgument(analyzers);
			return this.GetAnalysisResultCoreAsync(model, filterSpan, analyzers, cancellationToken);
		}

		// Token: 0x0600229F RID: 8863 RVA: 0x0006E514 File Offset: 0x0006C714
		private Task<AnalysisResult> GetAnalysisResultCoreAsync(SemanticModel model, TextSpan? filterSpan, [Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticAnalyzer> analyzers, CancellationToken cancellationToken)
		{
			CompilationWithAnalyzers.<GetAnalysisResultCoreAsync>d__64 <GetAnalysisResultCoreAsync>d__;
			<GetAnalysisResultCoreAsync>d__.<>t__builder = AsyncTaskMethodBuilder<AnalysisResult>.Create();
			<GetAnalysisResultCoreAsync>d__.<>4__this = this;
			<GetAnalysisResultCoreAsync>d__.model = model;
			<GetAnalysisResultCoreAsync>d__.filterSpan = filterSpan;
			<GetAnalysisResultCoreAsync>d__.analyzers = analyzers;
			<GetAnalysisResultCoreAsync>d__.cancellationToken = cancellationToken;
			<GetAnalysisResultCoreAsync>d__.<>1__state = -1;
			<GetAnalysisResultCoreAsync>d__.<>t__builder.Start<CompilationWithAnalyzers.<GetAnalysisResultCoreAsync>d__64>(ref <GetAnalysisResultCoreAsync>d__);
			return <GetAnalysisResultCoreAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060022A0 RID: 8864 RVA: 0x0006E578 File Offset: 0x0006C778
		[return: Nullable(new byte[] { 1, 0, 1 })]
		private Task<ImmutableArray<Diagnostic>> GetAnalyzerSemanticDiagnosticsCoreAsync(SemanticModel model, TextSpan? filterSpan, [Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticAnalyzer> analyzers, CancellationToken cancellationToken)
		{
			CompilationWithAnalyzers.<GetAnalyzerSemanticDiagnosticsCoreAsync>d__65 <GetAnalyzerSemanticDiagnosticsCoreAsync>d__;
			<GetAnalyzerSemanticDiagnosticsCoreAsync>d__.<>t__builder = AsyncTaskMethodBuilder<ImmutableArray<Diagnostic>>.Create();
			<GetAnalyzerSemanticDiagnosticsCoreAsync>d__.<>4__this = this;
			<GetAnalyzerSemanticDiagnosticsCoreAsync>d__.model = model;
			<GetAnalyzerSemanticDiagnosticsCoreAsync>d__.filterSpan = filterSpan;
			<GetAnalyzerSemanticDiagnosticsCoreAsync>d__.analyzers = analyzers;
			<GetAnalyzerSemanticDiagnosticsCoreAsync>d__.cancellationToken = cancellationToken;
			<GetAnalyzerSemanticDiagnosticsCoreAsync>d__.<>1__state = -1;
			<GetAnalyzerSemanticDiagnosticsCoreAsync>d__.<>t__builder.Start<CompilationWithAnalyzers.<GetAnalyzerSemanticDiagnosticsCoreAsync>d__65>(ref <GetAnalyzerSemanticDiagnosticsCoreAsync>d__);
			return <GetAnalyzerSemanticDiagnosticsCoreAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060022A1 RID: 8865 RVA: 0x0006E5DC File Offset: 0x0006C7DC
		private Task ComputeAnalyzerSemanticDiagnosticsAsync(SemanticModel model, AnalysisScope analysisScope, CancellationToken cancellationToken, bool forceCompletePartialTrees = true)
		{
			CompilationWithAnalyzers.<ComputeAnalyzerSemanticDiagnosticsAsync>d__66 <ComputeAnalyzerSemanticDiagnosticsAsync>d__;
			<ComputeAnalyzerSemanticDiagnosticsAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ComputeAnalyzerSemanticDiagnosticsAsync>d__.<>4__this = this;
			<ComputeAnalyzerSemanticDiagnosticsAsync>d__.model = model;
			<ComputeAnalyzerSemanticDiagnosticsAsync>d__.analysisScope = analysisScope;
			<ComputeAnalyzerSemanticDiagnosticsAsync>d__.cancellationToken = cancellationToken;
			<ComputeAnalyzerSemanticDiagnosticsAsync>d__.forceCompletePartialTrees = forceCompletePartialTrees;
			<ComputeAnalyzerSemanticDiagnosticsAsync>d__.<>1__state = -1;
			<ComputeAnalyzerSemanticDiagnosticsAsync>d__.<>t__builder.Start<CompilationWithAnalyzers.<ComputeAnalyzerSemanticDiagnosticsAsync>d__66>(ref <ComputeAnalyzerSemanticDiagnosticsAsync>d__);
			return <ComputeAnalyzerSemanticDiagnosticsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060022A2 RID: 8866 RVA: 0x0006E640 File Offset: 0x0006C840
		[return: TupleElementNames(new string[] { "events", "hasSymbolStartActions" })]
		[return: Nullable(new byte[] { 1, 0, 0, 1 })]
		private Task<ValueTuple<ImmutableArray<CompilationEvent>, bool>> ComputeAnalyzerDiagnosticsAsync(AnalysisScope analysisScope, [Nullable(new byte[] { 2, 0, 1 })] Func<ImmutableArray<CompilationEvent>> getPendingEventsOpt, int newTaskToken, CancellationToken cancellationToken)
		{
			CompilationWithAnalyzers.<ComputeAnalyzerDiagnosticsAsync>d__67 <ComputeAnalyzerDiagnosticsAsync>d__;
			<ComputeAnalyzerDiagnosticsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<ValueTuple<ImmutableArray<CompilationEvent>, bool>>.Create();
			<ComputeAnalyzerDiagnosticsAsync>d__.<>4__this = this;
			<ComputeAnalyzerDiagnosticsAsync>d__.analysisScope = analysisScope;
			<ComputeAnalyzerDiagnosticsAsync>d__.getPendingEventsOpt = getPendingEventsOpt;
			<ComputeAnalyzerDiagnosticsAsync>d__.newTaskToken = newTaskToken;
			<ComputeAnalyzerDiagnosticsAsync>d__.cancellationToken = cancellationToken;
			<ComputeAnalyzerDiagnosticsAsync>d__.<>1__state = -1;
			<ComputeAnalyzerDiagnosticsAsync>d__.<>t__builder.Start<CompilationWithAnalyzers.<ComputeAnalyzerDiagnosticsAsync>d__67>(ref <ComputeAnalyzerDiagnosticsAsync>d__);
			return <ComputeAnalyzerDiagnosticsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060022A3 RID: 8867 RVA: 0x0006E6A4 File Offset: 0x0006C8A4
		private void GenerateCompilationEvents(AnalysisScope analysisScope, CancellationToken cancellationToken)
		{
			if (analysisScope.FilterFileOpt == null)
			{
				this._compilation.GetDiagnostics(cancellationToken);
				return;
			}
			if (!analysisScope.IsSyntacticSingleFileAnalysis)
			{
				this._compilation.GetSemanticModel(analysisScope.FilterFileOpt.Value.SourceTree, false).GetDiagnostics(default(TextSpan?), cancellationToken);
			}
		}

		// Token: 0x060022A4 RID: 8868 RVA: 0x0006E70C File Offset: 0x0006C90C
		private Task PopulateEventsCacheAsync(CancellationToken cancellationToken)
		{
			CompilationWithAnalyzers.<PopulateEventsCacheAsync>d__69 <PopulateEventsCacheAsync>d__;
			<PopulateEventsCacheAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<PopulateEventsCacheAsync>d__.<>4__this = this;
			<PopulateEventsCacheAsync>d__.cancellationToken = cancellationToken;
			<PopulateEventsCacheAsync>d__.<>1__state = -1;
			<PopulateEventsCacheAsync>d__.<>t__builder.Start<CompilationWithAnalyzers.<PopulateEventsCacheAsync>d__69>(ref <PopulateEventsCacheAsync>d__);
			return <PopulateEventsCacheAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060022A5 RID: 8869 RVA: 0x0006E758 File Offset: 0x0006C958
		private Task<AnalyzerDriver> GetAnalyzerDriverAsync(CancellationToken cancellationToken)
		{
			CompilationWithAnalyzers.<GetAnalyzerDriverAsync>d__70 <GetAnalyzerDriverAsync>d__;
			<GetAnalyzerDriverAsync>d__.<>t__builder = AsyncTaskMethodBuilder<AnalyzerDriver>.Create();
			<GetAnalyzerDriverAsync>d__.<>4__this = this;
			<GetAnalyzerDriverAsync>d__.cancellationToken = cancellationToken;
			<GetAnalyzerDriverAsync>d__.<>1__state = -1;
			<GetAnalyzerDriverAsync>d__.<>t__builder.Start<CompilationWithAnalyzers.<GetAnalyzerDriverAsync>d__70>(ref <GetAnalyzerDriverAsync>d__);
			return <GetAnalyzerDriverAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060022A6 RID: 8870 RVA: 0x0006E7A3 File Offset: 0x0006C9A3
		[NullableContext(2)]
		private void FreeDriver(AnalyzerDriver driver)
		{
			if (driver != null && driver.IsInitialized && !driver.WhenInitializedTask.IsCanceled)
			{
				this._driverPool.Free(driver);
			}
		}

		// Token: 0x060022A7 RID: 8871 RVA: 0x0006E7CC File Offset: 0x0006C9CC
		private Task ComputeAnalyzerDiagnosticsCoreAsync(AnalyzerDriver driver, AsyncQueue<CompilationEvent> eventQueue, AnalysisScope analysisScope, CancellationToken cancellationToken)
		{
			CompilationWithAnalyzers.<ComputeAnalyzerDiagnosticsCoreAsync>d__72 <ComputeAnalyzerDiagnosticsCoreAsync>d__;
			<ComputeAnalyzerDiagnosticsCoreAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ComputeAnalyzerDiagnosticsCoreAsync>d__.<>4__this = this;
			<ComputeAnalyzerDiagnosticsCoreAsync>d__.driver = driver;
			<ComputeAnalyzerDiagnosticsCoreAsync>d__.eventQueue = eventQueue;
			<ComputeAnalyzerDiagnosticsCoreAsync>d__.analysisScope = analysisScope;
			<ComputeAnalyzerDiagnosticsCoreAsync>d__.cancellationToken = cancellationToken;
			<ComputeAnalyzerDiagnosticsCoreAsync>d__.<>1__state = -1;
			<ComputeAnalyzerDiagnosticsCoreAsync>d__.<>t__builder.Start<CompilationWithAnalyzers.<ComputeAnalyzerDiagnosticsCoreAsync>d__72>(ref <ComputeAnalyzerDiagnosticsCoreAsync>d__);
			return <ComputeAnalyzerDiagnosticsCoreAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060022A8 RID: 8872 RVA: 0x0006E830 File Offset: 0x0006CA30
		private Task<Task> SetActiveAnalysisTaskAsync(Func<Tuple<Task, CancellationTokenSource>> getNewAnalysisTask, SourceOrAdditionalFile? fileOpt, int newTaskToken, CancellationToken cancellationToken)
		{
			if (fileOpt != null)
			{
				return this.SetActiveTreeAnalysisTaskAsync(getNewAnalysisTask, fileOpt.Value, newTaskToken, cancellationToken);
			}
			return this.SetActiveCompilationAnalysisTaskAsync(getNewAnalysisTask, cancellationToken);
		}

		// Token: 0x060022A9 RID: 8873 RVA: 0x0006E858 File Offset: 0x0006CA58
		private Task<Task> SetActiveCompilationAnalysisTaskAsync(Func<Tuple<Task, CancellationTokenSource>> getNewCompilationTask, CancellationToken cancellationToken)
		{
			CompilationWithAnalyzers.<SetActiveCompilationAnalysisTaskAsync>d__74 <SetActiveCompilationAnalysisTaskAsync>d__;
			<SetActiveCompilationAnalysisTaskAsync>d__.<>t__builder = AsyncTaskMethodBuilder<Task>.Create();
			<SetActiveCompilationAnalysisTaskAsync>d__.<>4__this = this;
			<SetActiveCompilationAnalysisTaskAsync>d__.getNewCompilationTask = getNewCompilationTask;
			<SetActiveCompilationAnalysisTaskAsync>d__.cancellationToken = cancellationToken;
			<SetActiveCompilationAnalysisTaskAsync>d__.<>1__state = -1;
			<SetActiveCompilationAnalysisTaskAsync>d__.<>t__builder.Start<CompilationWithAnalyzers.<SetActiveCompilationAnalysisTaskAsync>d__74>(ref <SetActiveCompilationAnalysisTaskAsync>d__);
			return <SetActiveCompilationAnalysisTaskAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060022AA RID: 8874 RVA: 0x0006E8AC File Offset: 0x0006CAAC
		private Task WaitForActiveAnalysisTasksAsync(bool waitForTreeTasks, bool waitForCompilationOrNonConcurrentTask, CancellationToken cancellationToken)
		{
			CompilationWithAnalyzers.<WaitForActiveAnalysisTasksAsync>d__75 <WaitForActiveAnalysisTasksAsync>d__;
			<WaitForActiveAnalysisTasksAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WaitForActiveAnalysisTasksAsync>d__.<>4__this = this;
			<WaitForActiveAnalysisTasksAsync>d__.waitForTreeTasks = waitForTreeTasks;
			<WaitForActiveAnalysisTasksAsync>d__.waitForCompilationOrNonConcurrentTask = waitForCompilationOrNonConcurrentTask;
			<WaitForActiveAnalysisTasksAsync>d__.cancellationToken = cancellationToken;
			<WaitForActiveAnalysisTasksAsync>d__.<>1__state = -1;
			<WaitForActiveAnalysisTasksAsync>d__.<>t__builder.Start<CompilationWithAnalyzers.<WaitForActiveAnalysisTasksAsync>d__75>(ref <WaitForActiveAnalysisTasksAsync>d__);
			return <WaitForActiveAnalysisTasksAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060022AB RID: 8875 RVA: 0x0006E908 File Offset: 0x0006CB08
		private Task<Task> SetActiveTreeAnalysisTaskAsync(Func<Tuple<Task, CancellationTokenSource>> getNewTreeAnalysisTask, SourceOrAdditionalFile tree, int newTaskToken, CancellationToken cancellationToken)
		{
			CompilationWithAnalyzers.<SetActiveTreeAnalysisTaskAsync>d__76 <SetActiveTreeAnalysisTaskAsync>d__;
			<SetActiveTreeAnalysisTaskAsync>d__.<>t__builder = AsyncTaskMethodBuilder<Task>.Create();
			<SetActiveTreeAnalysisTaskAsync>d__.<>4__this = this;
			<SetActiveTreeAnalysisTaskAsync>d__.getNewTreeAnalysisTask = getNewTreeAnalysisTask;
			<SetActiveTreeAnalysisTaskAsync>d__.tree = tree;
			<SetActiveTreeAnalysisTaskAsync>d__.newTaskToken = newTaskToken;
			<SetActiveTreeAnalysisTaskAsync>d__.cancellationToken = cancellationToken;
			<SetActiveTreeAnalysisTaskAsync>d__.<>1__state = -1;
			<SetActiveTreeAnalysisTaskAsync>d__.<>t__builder.Start<CompilationWithAnalyzers.<SetActiveTreeAnalysisTaskAsync>d__76>(ref <SetActiveTreeAnalysisTaskAsync>d__);
			return <SetActiveTreeAnalysisTaskAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060022AC RID: 8876 RVA: 0x0006E96C File Offset: 0x0006CB6C
		private static Task WaitForExecutingTaskAsync(Task executingTask, bool alwaysYield)
		{
			CompilationWithAnalyzers.<WaitForExecutingTaskAsync>d__77 <WaitForExecutingTaskAsync>d__;
			<WaitForExecutingTaskAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WaitForExecutingTaskAsync>d__.executingTask = executingTask;
			<WaitForExecutingTaskAsync>d__.alwaysYield = alwaysYield;
			<WaitForExecutingTaskAsync>d__.<>1__state = -1;
			<WaitForExecutingTaskAsync>d__.<>t__builder.Start<CompilationWithAnalyzers.<WaitForExecutingTaskAsync>d__77>(ref <WaitForExecutingTaskAsync>d__);
			return <WaitForExecutingTaskAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060022AD RID: 8877 RVA: 0x0006E9B7 File Offset: 0x0006CBB7
		private void SuspendAnalysis_NoLock(Task computeTask, CancellationTokenSource cts)
		{
			if (!computeTask.IsCompleted)
			{
				cts.Cancel();
			}
		}

		// Token: 0x060022AE RID: 8878 RVA: 0x0006E9C8 File Offset: 0x0006CBC8
		[NullableContext(2)]
		private void ClearExecutingTask(Task computeTask, SourceOrAdditionalFile? fileOpt)
		{
			if (computeTask != null)
			{
				object executingTasksLock = this._executingTasksLock;
				lock (executingTasksLock)
				{
					if (fileOpt != null && this._analysisOptions.ConcurrentAnalysis)
					{
						Tuple<Task, CancellationTokenSource> tuple;
						if (this._executingConcurrentTreeTasksOpt.TryGetValue(fileOpt.Value, ref tuple) && tuple.Item1 == computeTask)
						{
							this._executingConcurrentTreeTasksOpt.Remove(fileOpt.Value);
						}
						if (this._concurrentTreeTaskTokensOpt.ContainsKey(computeTask))
						{
							this._concurrentTreeTaskTokensOpt.Remove(computeTask);
						}
					}
					else
					{
						Tuple<Task, CancellationTokenSource> executingCompilationOrNonConcurrentTreeTask = this._executingCompilationOrNonConcurrentTreeTask;
						if (((executingCompilationOrNonConcurrentTreeTask != null) ? executingCompilationOrNonConcurrentTreeTask.Item1 : null) == computeTask)
						{
							this._executingCompilationOrNonConcurrentTreeTask = null;
						}
					}
				}
			}
		}

		// Token: 0x060022AF RID: 8879 RVA: 0x0006EA8C File Offset: 0x0006CC8C
		private AsyncQueue<CompilationEvent> CreateEventsQueue([Nullable(new byte[] { 0, 1 })] ImmutableArray<CompilationEvent> compilationEvents)
		{
			if (compilationEvents.IsEmpty)
			{
				return CompilationWithAnalyzers.s_EmptyEventQueue;
			}
			AsyncQueue<CompilationEvent> asyncQueue = this._eventQueuePool.Allocate();
			foreach (CompilationEvent compilationEvent in compilationEvents)
			{
				asyncQueue.TryEnqueue(compilationEvent);
			}
			return asyncQueue;
		}

		// Token: 0x060022B0 RID: 8880 RVA: 0x0006EAD8 File Offset: 0x0006CCD8
		private static void FreeEventQueue(AsyncQueue<CompilationEvent> eventQueue, ObjectPool<AsyncQueue<CompilationEvent>> eventQueuePool)
		{
			if (eventQueue == null || eventQueue == CompilationWithAnalyzers.s_EmptyEventQueue)
			{
				return;
			}
			if (eventQueue.Count > 0)
			{
				CompilationEvent compilationEvent;
				while (eventQueue.TryDequeue(out compilationEvent))
				{
				}
			}
			if (!eventQueue.IsCompleted)
			{
				eventQueuePool.Free(eventQueue);
			}
		}

		// Token: 0x060022B1 RID: 8881 RVA: 0x0006EB13 File Offset: 0x0006CD13
		public static IEnumerable<Diagnostic> GetEffectiveDiagnostics(IEnumerable<Diagnostic> diagnostics, Compilation compilation)
		{
			return CompilationWithAnalyzers.GetEffectiveDiagnostics(diagnostics.AsImmutableOrNull<Diagnostic>(), compilation);
		}

		// Token: 0x060022B2 RID: 8882 RVA: 0x0006EB21 File Offset: 0x0006CD21
		public static IEnumerable<Diagnostic> GetEffectiveDiagnostics([Nullable(new byte[] { 0, 1 })] ImmutableArray<Diagnostic> diagnostics, Compilation compilation)
		{
			if (diagnostics.IsDefault)
			{
				throw new ArgumentNullException("diagnostics");
			}
			if (compilation == null)
			{
				throw new ArgumentNullException("compilation");
			}
			return CompilationWithAnalyzers.GetEffectiveDiagnosticsImpl(diagnostics, compilation);
		}

		// Token: 0x060022B3 RID: 8883 RVA: 0x0006EB4C File Offset: 0x0006CD4C
		private static IEnumerable<Diagnostic> GetEffectiveDiagnosticsImpl([Nullable(new byte[] { 0, 1 })] ImmutableArray<Diagnostic> diagnostics, Compilation compilation)
		{
			if (diagnostics.IsEmpty)
			{
				yield break;
			}
			if (compilation.SemanticModelProvider == null)
			{
				compilation = compilation.WithSemanticModelProvider(new CachingSemanticModelProvider());
			}
			SuppressMessageAttributeState suppressMessageState = new SuppressMessageAttributeState(compilation);
			foreach (Diagnostic diagnostic in diagnostics)
			{
				if (diagnostic != null)
				{
					Diagnostic diagnostic2 = compilation.Options.FilterDiagnostic(diagnostic, CancellationToken.None);
					if (diagnostic2 != null)
					{
						yield return suppressMessageState.ApplySourceSuppressions(diagnostic2);
					}
				}
			}
			ImmutableArray<Diagnostic>.Enumerator enumerator = default(ImmutableArray<Diagnostic>.Enumerator);
			yield break;
		}

		// Token: 0x060022B4 RID: 8884 RVA: 0x0006EB64 File Offset: 0x0006CD64
		public static bool IsDiagnosticAnalyzerSuppressed(DiagnosticAnalyzer analyzer, CompilationOptions options, [Nullable(new byte[] { 2, 1, 1, 1 })] Action<Exception, DiagnosticAnalyzer, Diagnostic> onAnalyzerException = null)
		{
			CompilationWithAnalyzers.VerifyAnalyzerArgumentForStaticApis(analyzer);
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			AnalyzerManager analyzerManager = new AnalyzerManager(analyzer);
			AnalyzerExecutor analyzerExecutor = AnalyzerExecutor.CreateForSupportedDiagnostics(onAnalyzerException, analyzerManager, default(CancellationToken));
			return AnalyzerDriver.IsDiagnosticAnalyzerSuppressed(analyzer, options, analyzerManager, analyzerExecutor, SeverityFilter.None);
		}

		// Token: 0x060022B5 RID: 8885 RVA: 0x0006EBAD File Offset: 0x0006CDAD
		[Obsolete("This API is no longer required to be invoked. Analyzer state is automatically cleaned up when CompilationWithAnalyzers instance is released.")]
		public static void ClearAnalyzerState([Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticAnalyzer> analyzers)
		{
		}

		// Token: 0x060022B6 RID: 8886 RVA: 0x0006EBB0 File Offset: 0x0006CDB0
		public Task<AnalyzerTelemetryInfo> GetAnalyzerTelemetryInfoAsync(DiagnosticAnalyzer analyzer, CancellationToken cancellationToken)
		{
			CompilationWithAnalyzers.<GetAnalyzerTelemetryInfoAsync>d__87 <GetAnalyzerTelemetryInfoAsync>d__;
			<GetAnalyzerTelemetryInfoAsync>d__.<>t__builder = AsyncTaskMethodBuilder<AnalyzerTelemetryInfo>.Create();
			<GetAnalyzerTelemetryInfoAsync>d__.<>4__this = this;
			<GetAnalyzerTelemetryInfoAsync>d__.analyzer = analyzer;
			<GetAnalyzerTelemetryInfoAsync>d__.cancellationToken = cancellationToken;
			<GetAnalyzerTelemetryInfoAsync>d__.<>1__state = -1;
			<GetAnalyzerTelemetryInfoAsync>d__.<>t__builder.Start<CompilationWithAnalyzers.<GetAnalyzerTelemetryInfoAsync>d__87>(ref <GetAnalyzerTelemetryInfoAsync>d__);
			return <GetAnalyzerTelemetryInfoAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060022B7 RID: 8887 RVA: 0x0006EC04 File Offset: 0x0006CE04
		private Task<AnalyzerActionCounts> GetAnalyzerActionCountsAsync(DiagnosticAnalyzer analyzer, CancellationToken cancellationToken)
		{
			CompilationWithAnalyzers.<GetAnalyzerActionCountsAsync>d__88 <GetAnalyzerActionCountsAsync>d__;
			<GetAnalyzerActionCountsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<AnalyzerActionCounts>.Create();
			<GetAnalyzerActionCountsAsync>d__.<>4__this = this;
			<GetAnalyzerActionCountsAsync>d__.analyzer = analyzer;
			<GetAnalyzerActionCountsAsync>d__.cancellationToken = cancellationToken;
			<GetAnalyzerActionCountsAsync>d__.<>1__state = -1;
			<GetAnalyzerActionCountsAsync>d__.<>t__builder.Start<CompilationWithAnalyzers.<GetAnalyzerActionCountsAsync>d__88>(ref <GetAnalyzerActionCountsAsync>d__);
			return <GetAnalyzerActionCountsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060022B8 RID: 8888 RVA: 0x0006EC58 File Offset: 0x0006CE58
		private TimeSpan GetAnalyzerExecutionTime(DiagnosticAnalyzer analyzer)
		{
			if (!this._analysisOptions.LogAnalyzerExecutionTime)
			{
				return default(TimeSpan);
			}
			return this._analysisResultBuilder.GetAnalyzerExecutionTime(analyzer);
		}

		// Token: 0x060022BB RID: 8891 RVA: 0x0006ECB4 File Offset: 0x0006CEB4
		[CompilerGenerated]
		[return: Nullable(new byte[] { 0, 1 })]
		internal static ImmutableArray<CompilationEvent> <PopulateEventsCacheAsync>g__dequeueGeneratedCompilationEvents|69_0(AsyncQueue<CompilationEvent> eventQueue, [Nullable(new byte[] { 0, 1 })] ImmutableArray<AdditionalText> additionalFiles)
		{
			ImmutableArray<CompilationEvent>.Builder builder = ImmutableArray.CreateBuilder<CompilationEvent>();
			CompilationEvent compilationEvent;
			while (eventQueue.TryDequeue(out compilationEvent))
			{
				CompilationStartedEvent compilationStartedEvent = compilationEvent as CompilationStartedEvent;
				if (compilationStartedEvent != null && !additionalFiles.IsEmpty)
				{
					compilationEvent = compilationStartedEvent.WithAdditionalFiles(additionalFiles);
				}
				builder.Add(compilationEvent);
			}
			return builder.ToImmutable();
		}

		// Token: 0x04000EC9 RID: 3785
		private readonly Compilation _compilation;

		// Token: 0x04000ECA RID: 3786
		private readonly AnalyzerDriver.CompilationData _compilationData;

		// Token: 0x04000ECB RID: 3787
		[Nullable(new byte[] { 0, 1 })]
		private readonly ImmutableArray<DiagnosticAnalyzer> _analyzers;

		// Token: 0x04000ECC RID: 3788
		private readonly CompilationWithAnalyzersOptions _analysisOptions;

		// Token: 0x04000ECD RID: 3789
		private readonly CancellationToken _cancellationToken;

		// Token: 0x04000ECE RID: 3790
		private readonly AnalyzerManager _analyzerManager;

		// Token: 0x04000ECF RID: 3791
		private readonly ObjectPool<AnalyzerDriver> _driverPool;

		// Token: 0x04000ED0 RID: 3792
		private readonly AnalysisState _analysisState;

		// Token: 0x04000ED1 RID: 3793
		private readonly AnalysisResultBuilder _analysisResultBuilder;

		// Token: 0x04000ED2 RID: 3794
		private readonly ConcurrentSet<Diagnostic> _exceptionDiagnostics = new ConcurrentSet<Diagnostic>();

		// Token: 0x04000ED3 RID: 3795
		private readonly object _executingTasksLock = new object();

		// Token: 0x04000ED4 RID: 3796
		[Nullable(new byte[] { 2, 1, 1, 1 })]
		private readonly Dictionary<SourceOrAdditionalFile, Tuple<Task, CancellationTokenSource>> _executingConcurrentTreeTasksOpt;

		// Token: 0x04000ED5 RID: 3797
		[Nullable(new byte[] { 2, 1, 1 })]
		private Tuple<Task, CancellationTokenSource> _executingCompilationOrNonConcurrentTreeTask;

		// Token: 0x04000ED6 RID: 3798
		private int _currentToken;

		// Token: 0x04000ED7 RID: 3799
		[Nullable(new byte[] { 2, 1 })]
		private readonly Dictionary<Task, int> _concurrentTreeTaskTokensOpt;

		// Token: 0x04000ED8 RID: 3800
		private readonly ObjectPool<AsyncQueue<CompilationEvent>> _eventQueuePool = new ObjectPool<AsyncQueue<CompilationEvent>>(() => new AsyncQueue<CompilationEvent>());

		// Token: 0x04000ED9 RID: 3801
		private static readonly AsyncQueue<CompilationEvent> s_EmptyEventQueue = new AsyncQueue<CompilationEvent>();
	}
}
