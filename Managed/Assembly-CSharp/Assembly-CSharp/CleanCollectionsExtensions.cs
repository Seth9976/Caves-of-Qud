using System;
using System.Collections.Generic;

// Token: 0x0200017A RID: 378
public static class CleanCollectionsExtensions
{
	// Token: 0x06000E92 RID: 3730 RVA: 0x0005D214 File Offset: 0x0005B414
	public static bool CleanContains<T>(this List<T> list, T item)
	{
		if (item == null)
		{
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i] == null)
				{
					return true;
				}
			}
		}
		else
		{
			for (int j = 0; j < list.Count; j++)
			{
				if (list[j] != null)
				{
					T t = list[j];
					if (t.Equals(item))
					{
						return true;
					}
				}
			}
		}
		return false;
	}
}
