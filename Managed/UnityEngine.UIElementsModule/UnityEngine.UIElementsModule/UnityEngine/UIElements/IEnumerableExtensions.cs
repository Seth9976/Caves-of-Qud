using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x020002D1 RID: 721
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal static class IEnumerableExtensions
	{
		// Token: 0x060014C0 RID: 5312 RVA: 0x000534F4 File Offset: 0x000516F4
		internal static bool HasValues(this IEnumerable<string> collection)
		{
			bool flag = collection == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				using (IEnumerator<string> enumerator = collection.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						string text = enumerator.Current;
						return true;
					}
				}
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x060014C1 RID: 5313 RVA: 0x00053550 File Offset: 0x00051750
		internal static bool NoElementOfTypeMatchesPredicate<T>(this IEnumerable collection, Func<T, bool> predicate)
		{
			foreach (object obj in collection)
			{
				bool flag;
				if (obj is T)
				{
					T t = (T)((object)obj);
					flag = predicate(t);
				}
				else
				{
					flag = false;
				}
				bool flag2 = flag;
				if (flag2)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060014C2 RID: 5314 RVA: 0x000535CC File Offset: 0x000517CC
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal static int GetCount(this IEnumerable collection)
		{
			int num = 0;
			foreach (object obj in collection)
			{
				num++;
			}
			return num;
		}
	}
}
