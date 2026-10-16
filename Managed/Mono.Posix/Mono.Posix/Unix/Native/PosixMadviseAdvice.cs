using System;

namespace Mono.Unix.Native
{
	// Token: 0x0200003A RID: 58
	[Map]
	[CLSCompliant(false)]
	public enum PosixMadviseAdvice
	{
		// Token: 0x040001ED RID: 493
		POSIX_MADV_NORMAL,
		// Token: 0x040001EE RID: 494
		POSIX_MADV_RANDOM,
		// Token: 0x040001EF RID: 495
		POSIX_MADV_SEQUENTIAL,
		// Token: 0x040001F0 RID: 496
		POSIX_MADV_WILLNEED,
		// Token: 0x040001F1 RID: 497
		POSIX_MADV_DONTNEED
	}
}
