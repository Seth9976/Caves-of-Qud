using System;

namespace Mono.Unix.Native
{
	// Token: 0x0200007C RID: 124
	[AttributeUsage(AttributeTargets.Field)]
	internal class pid_tAttribute : MapAttribute
	{
		// Token: 0x06000673 RID: 1651 RVA: 0x0000E2FB File Offset: 0x0000C4FB
		public pid_tAttribute()
			: base("pid_t")
		{
		}
	}
}
