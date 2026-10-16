using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000564 RID: 1380
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public class UxmlAttributeAttribute : Attribute
	{
		// Token: 0x060029D8 RID: 10712 RVA: 0x0009708B File Offset: 0x0009528B
		public UxmlAttributeAttribute()
			: this(null, null)
		{
		}

		// Token: 0x060029D9 RID: 10713 RVA: 0x00097097 File Offset: 0x00095297
		public UxmlAttributeAttribute(string name)
			: this(name, null)
		{
		}

		// Token: 0x060029DA RID: 10714 RVA: 0x000970A3 File Offset: 0x000952A3
		public UxmlAttributeAttribute(string name, params string[] obsoleteNames)
		{
			this.name = name;
			this.obsoleteNames = obsoleteNames;
		}

		// Token: 0x040012C3 RID: 4803
		public string name;

		// Token: 0x040012C4 RID: 4804
		public string[] obsoleteNames;
	}
}
