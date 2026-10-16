using System;

namespace UnityEngine
{
	// Token: 0x02000234 RID: 564
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
	public class HeaderAttribute : PropertyAttribute
	{
		// Token: 0x06001E63 RID: 7779 RVA: 0x0003A85F File Offset: 0x00038A5F
		public HeaderAttribute(string header)
			: base(true)
		{
			this.header = header;
		}

		// Token: 0x04000903 RID: 2307
		public readonly string header;
	}
}
