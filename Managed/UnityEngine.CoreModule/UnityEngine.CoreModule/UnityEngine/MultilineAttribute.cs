using System;

namespace UnityEngine
{
	// Token: 0x02000237 RID: 567
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
	public sealed class MultilineAttribute : PropertyAttribute
	{
		// Token: 0x06001E66 RID: 7782 RVA: 0x0003A89A File Offset: 0x00038A9A
		public MultilineAttribute()
		{
			this.lines = 3;
		}

		// Token: 0x06001E67 RID: 7783 RVA: 0x0003A8AB File Offset: 0x00038AAB
		public MultilineAttribute(int lines)
		{
			this.lines = lines;
		}

		// Token: 0x04000907 RID: 2311
		public readonly int lines;
	}
}
