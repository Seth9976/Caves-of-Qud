using System;

namespace JetBrains.Annotations
{
	// Token: 0x020000E5 RID: 229
	[Flags]
	public enum CollectionAccessType
	{
		// Token: 0x040002B4 RID: 692
		None = 0,
		// Token: 0x040002B5 RID: 693
		Read = 1,
		// Token: 0x040002B6 RID: 694
		ModifyExistingContent = 2,
		// Token: 0x040002B7 RID: 695
		UpdatedContent = 6
	}
}
