using System;
using System.Collections;
using System.Reflection;

// Token: 0x02000003 RID: 3
internal static class CountPropertyHelper
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	public static bool TryGetCount(object value, out int count)
	{
		ICollection collection = value as ICollection;
		if (collection != null)
		{
			count = collection.Count;
			return true;
		}
		PropertyInfo runtimeProperty = value.GetType().GetRuntimeProperty("Count");
		if (runtimeProperty != null && runtimeProperty.CanRead && runtimeProperty.PropertyType == typeof(int))
		{
			count = (int)runtimeProperty.GetValue(value);
			return true;
		}
		count = -1;
		return false;
	}
}
