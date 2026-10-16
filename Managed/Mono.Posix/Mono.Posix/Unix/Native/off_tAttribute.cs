using System;

namespace Mono.Unix.Native
{
	// Token: 0x0200007B RID: 123
	[AttributeUsage(AttributeTargets.Field)]
	internal class off_tAttribute : MapAttribute
	{
		// Token: 0x06000672 RID: 1650 RVA: 0x0000E2EE File Offset: 0x0000C4EE
		public off_tAttribute()
			: base("off_t")
		{
		}
	}
}
