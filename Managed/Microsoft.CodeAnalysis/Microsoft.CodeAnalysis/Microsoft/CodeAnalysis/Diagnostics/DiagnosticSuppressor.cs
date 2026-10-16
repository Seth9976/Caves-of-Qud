using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x02000349 RID: 841
	public abstract class DiagnosticSuppressor : DiagnosticAnalyzer
	{
		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x0600243E RID: 9278 RVA: 0x00071218 File Offset: 0x0006F418
		[Nullable(new byte[] { 0, 1 })]
		public sealed override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return ImmutableArray<DiagnosticDescriptor>.Empty;
			}
		}

		// Token: 0x0600243F RID: 9279 RVA: 0x0007121F File Offset: 0x0006F41F
		[NullableContext(1)]
		public sealed override void Initialize(AnalysisContext context)
		{
			context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
		}

		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x06002440 RID: 9280
		[Nullable(new byte[] { 0, 1 })]
		public abstract ImmutableArray<SuppressionDescriptor> SupportedSuppressions
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x06002441 RID: 9281
		public abstract void ReportSuppressions(SuppressionAnalysisContext context);
	}
}
