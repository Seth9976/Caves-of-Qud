using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000037 RID: 55
	[Map]
	[CLSCompliant(false)]
	public enum SeekFlags : short
	{
		// Token: 0x040001D7 RID: 471
		SEEK_SET,
		// Token: 0x040001D8 RID: 472
		SEEK_CUR,
		// Token: 0x040001D9 RID: 473
		SEEK_END,
		// Token: 0x040001DA RID: 474
		L_SET = 0,
		// Token: 0x040001DB RID: 475
		L_INCR,
		// Token: 0x040001DC RID: 476
		L_XTND
	}
}
