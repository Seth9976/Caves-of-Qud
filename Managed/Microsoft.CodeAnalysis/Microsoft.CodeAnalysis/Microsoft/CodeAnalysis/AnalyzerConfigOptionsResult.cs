using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000125 RID: 293
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct AnalyzerConfigOptionsResult
	{
		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000A50 RID: 2640 RVA: 0x0001E0CA File Offset: 0x0001C2CA
		public ImmutableDictionary<string, ReportDiagnostic> TreeOptions { get; }

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000A51 RID: 2641 RVA: 0x0001E0D2 File Offset: 0x0001C2D2
		public ImmutableDictionary<string, string> AnalyzerOptions { get; }

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000A52 RID: 2642 RVA: 0x0001E0DA File Offset: 0x0001C2DA
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<Diagnostic> Diagnostics
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x0001E0E2 File Offset: 0x0001C2E2
		internal AnalyzerConfigOptionsResult(ImmutableDictionary<string, ReportDiagnostic> treeOptions, ImmutableDictionary<string, string> analyzerOptions, [Nullable(new byte[] { 0, 1 })] ImmutableArray<Diagnostic> diagnostics)
		{
			this.TreeOptions = treeOptions;
			this.AnalyzerOptions = analyzerOptions;
			this.Diagnostics = diagnostics;
		}
	}
}
