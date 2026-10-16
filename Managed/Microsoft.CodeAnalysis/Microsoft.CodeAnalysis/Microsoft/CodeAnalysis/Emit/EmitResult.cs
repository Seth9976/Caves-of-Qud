using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Emit
{
	// Token: 0x02000474 RID: 1140
	[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
	public class EmitResult
	{
		// Token: 0x17000CF0 RID: 3312
		// (get) Token: 0x06002C7E RID: 11390 RVA: 0x0007DA46 File Offset: 0x0007BC46
		public bool Success { get; }

		// Token: 0x17000CF1 RID: 3313
		// (get) Token: 0x06002C7F RID: 11391 RVA: 0x0007DA4E File Offset: 0x0007BC4E
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<Diagnostic> Diagnostics
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x06002C80 RID: 11392 RVA: 0x0007DA56 File Offset: 0x0007BC56
		internal EmitResult(bool success, [Nullable(new byte[] { 0, 1 })] ImmutableArray<Diagnostic> diagnostics)
		{
			this.Success = success;
			this.Diagnostics = diagnostics;
		}

		// Token: 0x06002C81 RID: 11393 RVA: 0x0007DA6C File Offset: 0x0007BC6C
		[NullableContext(1)]
		protected virtual string GetDebuggerDisplay()
		{
			string text = "Success = " + (this.Success ? "true" : "false");
			if (new ImmutableArray<Diagnostic>?(this.Diagnostics) != default(ImmutableArray<Diagnostic>?))
			{
				text = text + ", Diagnostics.Count = " + this.Diagnostics.Length.ToString();
			}
			else
			{
				text += ", Diagnostics = null";
			}
			return text;
		}
	}
}
