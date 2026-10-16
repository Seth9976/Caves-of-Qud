using System;
using System.ComponentModel;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001D5 RID: 469
	public enum SourceCodeKind
	{
		// Token: 0x040005EF RID: 1519
		Regular,
		// Token: 0x040005F0 RID: 1520
		Script,
		// Token: 0x040005F1 RID: 1521
		[Obsolete("Use Script instead", false)]
		[EditorBrowsable(1)]
		Interactive
	}
}
