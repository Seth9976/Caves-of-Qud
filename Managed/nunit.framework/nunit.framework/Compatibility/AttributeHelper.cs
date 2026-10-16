using System;
using System.Reflection;

namespace NUnit.Compatibility
{
	// Token: 0x02000004 RID: 4
	public static class AttributeHelper
	{
		// Token: 0x06000003 RID: 3 RVA: 0x00002079 File Offset: 0x00000279
		public static Attribute[] GetCustomAttributes(object actual, Type attributeType, bool inherit)
		{
			ICustomAttributeProvider customAttributeProvider = actual as ICustomAttributeProvider;
			if (customAttributeProvider == null)
			{
				throw new ArgumentException(string.Format("Actual value {0} does not implement ICustomAttributeProvider.", actual), "actual");
			}
			return (Attribute[])customAttributeProvider.GetCustomAttributes(attributeType, inherit);
		}
	}
}
