using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AiUnity.Common.Extensions
{
	// Token: 0x020002DF RID: 735
	public static class ReflectionExtensions
	{
		// Token: 0x06001952 RID: 6482 RVA: 0x0008104A File Offset: 0x0007F24A
		public static T GetAttribute<T>(this PropertyInfo propertyInfo)
		{
			return propertyInfo.GetAttributes<T>().FirstOrDefault<T>();
		}

		// Token: 0x06001953 RID: 6483 RVA: 0x00081057 File Offset: 0x0007F257
		public static T GetAttribute<T>(this FieldInfo fieldInfo)
		{
			return fieldInfo.GetAttributes<T>().FirstOrDefault<T>();
		}

		// Token: 0x06001954 RID: 6484 RVA: 0x00081064 File Offset: 0x0007F264
		public static IEnumerable<T> GetAttributes<T>(this PropertyInfo propertyInfo)
		{
			return propertyInfo.GetCustomAttributes(typeof(T), true).Cast<T>();
		}

		// Token: 0x06001955 RID: 6485 RVA: 0x0008107C File Offset: 0x0007F27C
		public static IEnumerable<T> GetAttributes<T>(this FieldInfo propertyInfo)
		{
			return propertyInfo.GetCustomAttributes(typeof(T), true).Cast<T>();
		}

		// Token: 0x06001956 RID: 6486 RVA: 0x00081094 File Offset: 0x0007F294
		public static bool IsOverriding(this MethodInfo methodInfo)
		{
			if (methodInfo == null)
			{
				throw new ArgumentNullException();
			}
			return methodInfo.DeclaringType != methodInfo.GetBaseDefinition().DeclaringType;
		}
	}
}
