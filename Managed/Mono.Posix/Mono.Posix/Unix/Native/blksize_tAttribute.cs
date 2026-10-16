using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000074 RID: 116
	[AttributeUsage(AttributeTargets.Field)]
	internal class blksize_tAttribute : MapAttribute
	{
		// Token: 0x0600066B RID: 1643 RVA: 0x0000E293 File Offset: 0x0000C493
		public blksize_tAttribute()
			: base("blksize_t")
		{
		}
	}
}
