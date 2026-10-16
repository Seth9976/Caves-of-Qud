using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000566 RID: 1382
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public class UxmlTypeReferenceAttribute : PropertyAttribute
	{
		// Token: 0x060029DE RID: 10718 RVA: 0x000970DE File Offset: 0x000952DE
		public UxmlTypeReferenceAttribute()
			: this(null)
		{
		}

		// Token: 0x060029DF RID: 10719 RVA: 0x000970E9 File Offset: 0x000952E9
		public UxmlTypeReferenceAttribute(Type baseType)
		{
			this.baseType = baseType;
		}

		// Token: 0x040012C6 RID: 4806
		public Type baseType;
	}
}
