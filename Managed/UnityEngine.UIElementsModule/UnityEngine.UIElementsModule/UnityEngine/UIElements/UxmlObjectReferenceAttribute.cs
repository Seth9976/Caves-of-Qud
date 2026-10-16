using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000569 RID: 1385
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false)]
	public class UxmlObjectReferenceAttribute : Attribute
	{
		// Token: 0x060029E2 RID: 10722 RVA: 0x000970FA File Offset: 0x000952FA
		public UxmlObjectReferenceAttribute()
			: this(null, null)
		{
		}

		// Token: 0x060029E3 RID: 10723 RVA: 0x00097108 File Offset: 0x00095308
		public UxmlObjectReferenceAttribute(string uxmlName)
			: this(uxmlName, null)
		{
			bool flag = uxmlName == "null";
			if (flag)
			{
				throw new ArgumentException("UxmlObjectReferenceAttribute name cannot be \"null\".");
			}
		}

		// Token: 0x060029E4 RID: 10724 RVA: 0x00097139 File Offset: 0x00095339
		public UxmlObjectReferenceAttribute(string uxmlName, params Type[] acceptedTypes)
		{
			this.name = uxmlName;
			this.types = acceptedTypes;
		}

		// Token: 0x040012C7 RID: 4807
		public string name;

		// Token: 0x040012C8 RID: 4808
		public Type[] types;
	}
}
