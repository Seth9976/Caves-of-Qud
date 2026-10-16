using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000035 RID: 53
	[Map]
	[CLSCompliant(false)]
	public enum LockType : short
	{
		// Token: 0x040001CD RID: 461
		F_RDLCK,
		// Token: 0x040001CE RID: 462
		F_WRLCK,
		// Token: 0x040001CF RID: 463
		F_UNLCK
	}
}
