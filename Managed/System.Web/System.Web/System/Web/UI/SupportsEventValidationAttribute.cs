using System;
using System.Collections;

namespace System.Web.UI
{
	/// <summary>Defines the metadata attribute that Web server controls use to indicate support for event validation. This class cannot be inherited.</summary>
	// Token: 0x0200018E RID: 398
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public sealed class SupportsEventValidationAttribute : Attribute
	{
		// Token: 0x06000F83 RID: 3971 RVA: 0x00028C1C File Offset: 0x00026E1C
		internal static bool SupportsEventValidation(Type type)
		{
			object obj = SupportsEventValidationAttribute._typesSupportsEventValidation[type];
			if (obj != null)
			{
				return (bool)obj;
			}
			object[] customAttributes = type.GetCustomAttributes(typeof(SupportsEventValidationAttribute), false);
			bool flag = customAttributes != null && customAttributes.Length != 0;
			SupportsEventValidationAttribute._typesSupportsEventValidation[type] = flag;
			return flag;
		}

		// Token: 0x04001322 RID: 4898
		private static Hashtable _typesSupportsEventValidation = Hashtable.Synchronized(new Hashtable());
	}
}
