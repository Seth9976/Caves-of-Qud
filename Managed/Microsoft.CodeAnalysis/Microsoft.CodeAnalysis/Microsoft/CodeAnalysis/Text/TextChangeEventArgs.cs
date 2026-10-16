using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Text
{
	// Token: 0x020002F4 RID: 756
	[NullableContext(1)]
	[Nullable(0)]
	public class TextChangeEventArgs : EventArgs
	{
		// Token: 0x06002057 RID: 8279 RVA: 0x00063D27 File Offset: 0x00061F27
		public TextChangeEventArgs(SourceText oldText, SourceText newText, IEnumerable<TextChangeRange> changes)
		{
			if (changes == null)
			{
				throw new ArgumentNullException("changes");
			}
			this.OldText = oldText;
			this.NewText = newText;
			this.Changes = changes.ToImmutableArray<TextChangeRange>();
		}

		// Token: 0x06002058 RID: 8280 RVA: 0x00063D5C File Offset: 0x00061F5C
		public TextChangeEventArgs(SourceText oldText, SourceText newText, params TextChangeRange[] changes)
			: this(oldText, newText, changes)
		{
		}

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x06002059 RID: 8281 RVA: 0x00063D67 File Offset: 0x00061F67
		public SourceText OldText { get; }

		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x0600205A RID: 8282 RVA: 0x00063D6F File Offset: 0x00061F6F
		public SourceText NewText { get; }

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x0600205B RID: 8283 RVA: 0x00063D77 File Offset: 0x00061F77
		public IReadOnlyList<TextChangeRange> Changes { get; }
	}
}
