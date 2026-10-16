using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000073 RID: 115
	[AttributeUsage(AttributeTargets.Field)]
	internal class blkcnt_tAttribute : MapAttribute
	{
		// Token: 0x0600066A RID: 1642 RVA: 0x0000E286 File Offset: 0x0000C486
		public blkcnt_tAttribute()
			: base("blkcnt_t")
		{
		}
	}
}
