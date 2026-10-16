using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000039 RID: 57
	[Map]
	[CLSCompliant(false)]
	public enum PosixFadviseAdvice
	{
		// Token: 0x040001E6 RID: 486
		POSIX_FADV_NORMAL,
		// Token: 0x040001E7 RID: 487
		POSIX_FADV_RANDOM,
		// Token: 0x040001E8 RID: 488
		POSIX_FADV_SEQUENTIAL,
		// Token: 0x040001E9 RID: 489
		POSIX_FADV_WILLNEED,
		// Token: 0x040001EA RID: 490
		POSIX_FADV_DONTNEED,
		// Token: 0x040001EB RID: 491
		POSIX_FADV_NOREUSE
	}
}
