using System;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x02000342 RID: 834
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class HostSessionStartAnalysisScope : HostAnalysisScope
	{
		// Token: 0x060023DB RID: 9179 RVA: 0x0007045C File Offset: 0x0006E65C
		public bool IsConcurrentAnalyzer(DiagnosticAnalyzer analyzer)
		{
			return this._concurrentAnalyzers.Contains(analyzer);
		}

		// Token: 0x060023DC RID: 9180 RVA: 0x0007046C File Offset: 0x0006E66C
		public GeneratedCodeAnalysisFlags GetGeneratedCodeAnalysisFlags(DiagnosticAnalyzer analyzer)
		{
			GeneratedCodeAnalysisFlags generatedCodeAnalysisFlags;
			if (!this._generatedCodeConfigurationMap.TryGetValue(analyzer, ref generatedCodeAnalysisFlags))
			{
				return GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics;
			}
			return generatedCodeAnalysisFlags;
		}

		// Token: 0x060023DD RID: 9181 RVA: 0x0007048C File Offset: 0x0006E68C
		public void RegisterCompilationStartAction(DiagnosticAnalyzer analyzer, Action<CompilationStartAnalysisContext> action)
		{
			CompilationStartAnalyzerAction compilationStartAnalyzerAction = new CompilationStartAnalyzerAction(action, analyzer);
			base.GetOrCreateAnalyzerActions(analyzer).Value.AddCompilationStartAction(compilationStartAnalyzerAction);
		}

		// Token: 0x060023DE RID: 9182 RVA: 0x000704B3 File Offset: 0x0006E6B3
		public void EnableConcurrentExecution(DiagnosticAnalyzer analyzer)
		{
			this._concurrentAnalyzers = this._concurrentAnalyzers.Add(analyzer);
			base.GetOrCreateAnalyzerActions(analyzer).Value.EnableConcurrentExecution();
		}

		// Token: 0x060023DF RID: 9183 RVA: 0x000704D8 File Offset: 0x0006E6D8
		public void ConfigureGeneratedCodeAnalysis(DiagnosticAnalyzer analyzer, GeneratedCodeAnalysisFlags mode)
		{
			this._generatedCodeConfigurationMap.AddOrUpdate(analyzer, mode, (DiagnosticAnalyzer a, GeneratedCodeAnalysisFlags c) => mode);
		}

		// Token: 0x04000F5F RID: 3935
		private ImmutableHashSet<DiagnosticAnalyzer> _concurrentAnalyzers = ImmutableHashSet<DiagnosticAnalyzer>.Empty;

		// Token: 0x04000F60 RID: 3936
		private readonly ConcurrentDictionary<DiagnosticAnalyzer, GeneratedCodeAnalysisFlags> _generatedCodeConfigurationMap = new ConcurrentDictionary<DiagnosticAnalyzer, GeneratedCodeAnalysisFlags>();
	}
}
