using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000076 RID: 118
	[AttributeUsage(AttributeTargets.Field)]
	internal class gid_tAttribute : MapAttribute
	{
		// Token: 0x0600066D RID: 1645 RVA: 0x0000E2AD File Offset: 0x0000C4AD
		public gid_tAttribute()
			: base("gid_t")
		{
		}
	}
}
