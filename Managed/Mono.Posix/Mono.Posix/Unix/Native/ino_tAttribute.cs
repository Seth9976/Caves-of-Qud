using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000079 RID: 121
	[AttributeUsage(AttributeTargets.Field)]
	internal class ino_tAttribute : MapAttribute
	{
		// Token: 0x06000670 RID: 1648 RVA: 0x0000E2D4 File Offset: 0x0000C4D4
		public ino_tAttribute()
			: base("ino_t")
		{
		}
	}
}
