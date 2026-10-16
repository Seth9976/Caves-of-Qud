using System;

namespace UnityEngine
{
	// Token: 0x02000236 RID: 566
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
	public sealed class MinAttribute : PropertyAttribute
	{
		// Token: 0x06001E65 RID: 7781 RVA: 0x0003A889 File Offset: 0x00038A89
		public MinAttribute(float min)
		{
			this.min = min;
		}

		// Token: 0x04000906 RID: 2310
		public readonly float min;
	}
}
