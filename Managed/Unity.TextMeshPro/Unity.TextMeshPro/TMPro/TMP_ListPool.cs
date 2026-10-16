using System;
using System.Collections.Generic;

namespace TMPro
{
	// Token: 0x0200003A RID: 58
	internal static class TMP_ListPool<T>
	{
		// Token: 0x060002B7 RID: 695 RVA: 0x0000FCA1 File Offset: 0x0000DEA1
		public static List<T> Get()
		{
			return TMP_ListPool<T>.s_ListPool.Get();
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000FCAD File Offset: 0x0000DEAD
		public static void Release(List<T> toRelease)
		{
			TMP_ListPool<T>.s_ListPool.Release(toRelease);
		}

		// Token: 0x040001BD RID: 445
		private static readonly TMP_ObjectPool<List<T>> s_ListPool = new TMP_ObjectPool<List<T>>(null, delegate(List<T> l)
		{
			l.Clear();
		});
	}
}
