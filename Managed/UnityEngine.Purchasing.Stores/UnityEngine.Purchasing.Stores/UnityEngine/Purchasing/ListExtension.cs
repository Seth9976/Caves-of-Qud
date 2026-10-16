using System;
using System.Collections.Generic;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000010 RID: 16
	internal static class ListExtension
	{
		// Token: 0x06000021 RID: 33 RVA: 0x00002439 File Offset: 0x00000639
		internal static AndroidJavaObject ToJava<T>(this List<T> values)
		{
			return ListExtension.ToJavaArray<T>(values);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002444 File Offset: 0x00000644
		private static AndroidJavaObject ToJavaArray<T>(List<T> values)
		{
			AndroidJavaObject androidJavaObject = new AndroidJavaObject("java.util.ArrayList", Array.Empty<object>());
			foreach (T t in values)
			{
				androidJavaObject.Call<bool>("add", new object[] { t });
			}
			return androidJavaObject;
		}
	}
}
