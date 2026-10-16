using System;

namespace UnityEngine
{
	// Token: 0x02000233 RID: 563
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
	public class SpaceAttribute : PropertyAttribute
	{
		// Token: 0x06001E61 RID: 7777 RVA: 0x0003A837 File Offset: 0x00038A37
		public SpaceAttribute()
			: base(true)
		{
			this.height = 8f;
		}

		// Token: 0x06001E62 RID: 7778 RVA: 0x0003A84D File Offset: 0x00038A4D
		public SpaceAttribute(float height)
			: base(true)
		{
			this.height = height;
		}

		// Token: 0x04000902 RID: 2306
		public readonly float height;
	}
}
