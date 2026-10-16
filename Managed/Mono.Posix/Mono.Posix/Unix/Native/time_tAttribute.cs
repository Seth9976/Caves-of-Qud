using System;

namespace Mono.Unix.Native
{
	// Token: 0x0200007F RID: 127
	[AttributeUsage(AttributeTargets.Field)]
	internal class time_tAttribute : MapAttribute
	{
		// Token: 0x06000676 RID: 1654 RVA: 0x0000E322 File Offset: 0x0000C522
		public time_tAttribute()
			: base("time_t")
		{
		}
	}
}
