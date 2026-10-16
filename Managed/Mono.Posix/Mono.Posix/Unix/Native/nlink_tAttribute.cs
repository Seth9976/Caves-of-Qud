using System;

namespace Mono.Unix.Native
{
	// Token: 0x0200007A RID: 122
	[AttributeUsage(AttributeTargets.Field)]
	internal class nlink_tAttribute : MapAttribute
	{
		// Token: 0x06000671 RID: 1649 RVA: 0x0000E2E1 File Offset: 0x0000C4E1
		public nlink_tAttribute()
			: base("nlink_t")
		{
		}
	}
}
