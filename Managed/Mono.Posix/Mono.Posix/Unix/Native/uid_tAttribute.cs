using System;

namespace Mono.Unix.Native
{
	// Token: 0x0200007E RID: 126
	[AttributeUsage(AttributeTargets.Field)]
	internal class uid_tAttribute : MapAttribute
	{
		// Token: 0x06000675 RID: 1653 RVA: 0x0000E315 File Offset: 0x0000C515
		public uid_tAttribute()
			: base("uid_t")
		{
		}
	}
}
