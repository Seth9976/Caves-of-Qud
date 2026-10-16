using System;

namespace Mono.Unix.Native
{
	// Token: 0x0200007D RID: 125
	[AttributeUsage(AttributeTargets.Field)]
	internal class suseconds_tAttribute : MapAttribute
	{
		// Token: 0x06000674 RID: 1652 RVA: 0x0000E308 File Offset: 0x0000C508
		public suseconds_tAttribute()
			: base("suseconds_t")
		{
		}
	}
}
