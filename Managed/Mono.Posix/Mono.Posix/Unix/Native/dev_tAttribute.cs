using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000075 RID: 117
	[AttributeUsage(AttributeTargets.Field)]
	internal class dev_tAttribute : MapAttribute
	{
		// Token: 0x0600066C RID: 1644 RVA: 0x0000E2A0 File Offset: 0x0000C4A0
		public dev_tAttribute()
			: base("dev_t")
		{
		}
	}
}
