using System;

namespace Ionic.Zip
{
	// Token: 0x0200002F RID: 47
	[Flags]
	public enum ZipEntryTimestamp
	{
		// Token: 0x040000ED RID: 237
		None = 0,
		// Token: 0x040000EE RID: 238
		DOS = 1,
		// Token: 0x040000EF RID: 239
		Windows = 2,
		// Token: 0x040000F0 RID: 240
		Unix = 4,
		// Token: 0x040000F1 RID: 241
		InfoZip1 = 8
	}
}
