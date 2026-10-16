using System;

namespace System.Web.UI
{
	// Token: 0x0200023B RID: 571
	[Flags]
	[Serializable]
	public enum UrlTypes
	{
		// Token: 0x040015ED RID: 5613
		Absolute = 1,
		// Token: 0x040015EE RID: 5614
		AppRelative = 2,
		// Token: 0x040015EF RID: 5615
		DocRelative = 4,
		// Token: 0x040015F0 RID: 5616
		RootRelative = 8
	}
}
