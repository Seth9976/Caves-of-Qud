using System;
using System.Collections.Generic;
using XRL.Rules;

namespace XRL.Names
{
	// Token: 0x02000580 RID: 1408
	public static class NameElementExtensions
	{
		// Token: 0x06003C77 RID: 15479 RVA: 0x00127C4C File Offset: 0x00125E4C
		public static string GetRandomNameElement<T>(this List<T> List, Random R = null) where T : NameElement
		{
			int count = List.Count;
			if (count == 0)
			{
				return null;
			}
			if (count != 1)
			{
				if (R == null)
				{
					R = Stat.Rand;
				}
				int num = 0;
				int i = 0;
				int count2 = List.Count;
				while (i < count2)
				{
					if (List[i].Weight > 0)
					{
						num += List[i].Weight;
					}
					i++;
				}
				if (num <= 0)
				{
					return null;
				}
				int num2 = R.Next(0, num);
				int num3 = 0;
				int j = 0;
				int count3 = List.Count;
				while (j < count3)
				{
					if (List[j].Weight > 0)
					{
						num3 += List[j].Weight;
						if (num2 < num3)
						{
							return List[j].Name;
						}
					}
					j++;
				}
				return null;
			}
			else
			{
				if (List[0].Weight <= 0)
				{
					return null;
				}
				return List[0].Name;
			}
		}

		// Token: 0x06003C78 RID: 15480 RVA: 0x00127D50 File Offset: 0x00125F50
		public static T Find<T>(this List<T> List, string Name) where T : NameElement
		{
			foreach (T t in List)
			{
				if (t.Name == Name)
				{
					return t;
				}
			}
			return default(T);
		}

		// Token: 0x06003C79 RID: 15481 RVA: 0x00127DBC File Offset: 0x00125FBC
		public static bool Has<T>(this List<T> List, string Name) where T : NameElement
		{
			using (List<T>.Enumerator enumerator = List.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Name == Name)
					{
						return true;
					}
				}
			}
			return false;
		}
	}
}
