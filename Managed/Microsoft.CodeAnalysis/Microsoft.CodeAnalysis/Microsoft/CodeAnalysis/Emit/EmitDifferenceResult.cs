using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Emit
{
	// Token: 0x02000485 RID: 1157
	[NullableContext(2)]
	[Nullable(0)]
	public sealed class EmitDifferenceResult : EmitResult
	{
		// Token: 0x17000D27 RID: 3367
		// (get) Token: 0x06002D9D RID: 11677 RVA: 0x00081676 File Offset: 0x0007F876
		public EmitBaseline Baseline { get; }

		// Token: 0x06002D9E RID: 11678 RVA: 0x0008167E File Offset: 0x0007F87E
		internal EmitDifferenceResult(bool success, [Nullable(new byte[] { 0, 1 })] ImmutableArray<Diagnostic> diagnostics, EmitBaseline baseline)
			: base(success, diagnostics)
		{
			this.Baseline = baseline;
		}
	}
}
