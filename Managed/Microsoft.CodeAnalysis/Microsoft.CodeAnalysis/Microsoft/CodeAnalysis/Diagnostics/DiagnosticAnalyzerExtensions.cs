using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x0200033B RID: 827
	[NullableContext(1)]
	[Nullable(0)]
	public static class DiagnosticAnalyzerExtensions
	{
		// Token: 0x060023A3 RID: 9123 RVA: 0x0006FF9F File Offset: 0x0006E19F
		public static CompilationWithAnalyzers WithAnalyzers(this Compilation compilation, [Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticAnalyzer> analyzers, [Nullable(2)] AnalyzerOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return new CompilationWithAnalyzers(compilation, analyzers, options, cancellationToken);
		}

		// Token: 0x060023A4 RID: 9124 RVA: 0x0006FFAA File Offset: 0x0006E1AA
		public static CompilationWithAnalyzers WithAnalyzers(this Compilation compilation, [Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticAnalyzer> analyzers, CompilationWithAnalyzersOptions analysisOptions)
		{
			return new CompilationWithAnalyzers(compilation, analyzers, analysisOptions);
		}
	}
}
