using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000038 RID: 56
	[Map]
	[Flags]
	[CLSCompliant(false)]
	public enum DirectoryNotifyFlags
	{
		// Token: 0x040001DE RID: 478
		DN_ACCESS = 1,
		// Token: 0x040001DF RID: 479
		DN_MODIFY = 2,
		// Token: 0x040001E0 RID: 480
		DN_CREATE = 4,
		// Token: 0x040001E1 RID: 481
		DN_DELETE = 8,
		// Token: 0x040001E2 RID: 482
		DN_RENAME = 16,
		// Token: 0x040001E3 RID: 483
		DN_ATTRIB = 32,
		// Token: 0x040001E4 RID: 484
		DN_MULTISHOT = -2147483648
	}
}
