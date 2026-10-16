using System;

namespace UnityEngine
{
	// Token: 0x02000235 RID: 565
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
	public sealed class RangeAttribute : PropertyAttribute
	{
		// Token: 0x06001E64 RID: 7780 RVA: 0x0003A871 File Offset: 0x00038A71
		public RangeAttribute(float min, float max)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x04000904 RID: 2308
		public readonly float min;

		// Token: 0x04000905 RID: 2309
		public readonly float max;
	}
}
