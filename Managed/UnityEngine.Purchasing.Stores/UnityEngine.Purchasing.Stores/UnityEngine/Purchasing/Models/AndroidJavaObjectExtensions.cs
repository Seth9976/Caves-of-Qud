using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.Purchasing.Models
{
	// Token: 0x020000AB RID: 171
	internal static class AndroidJavaObjectExtensions
	{
		// Token: 0x060003C1 RID: 961 RVA: 0x0000BB40 File Offset: 0x00009D40
		internal static IEnumerable<T> Enumerate<T>(this AndroidJavaObject androidJavaList)
		{
			AndroidJavaObject androidJavaList2 = androidJavaList;
			int num = ((androidJavaList2 != null) ? androidJavaList2.Call<int>("size", Array.Empty<object>()) : 0);
			return (from i in Enumerable.Range(0, num)
				select androidJavaList.Call<T>("get", new object[] { i })).ToList<T>();
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000BB94 File Offset: 0x00009D94
		internal static IEnumerable<AndroidJavaObject> Enumerate(this AndroidJavaObject androidJavaList)
		{
			return androidJavaList.Enumerate<AndroidJavaObject>();
		}
	}
}
