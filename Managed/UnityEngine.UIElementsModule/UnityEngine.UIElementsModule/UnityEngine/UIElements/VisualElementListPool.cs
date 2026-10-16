using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020005AE RID: 1454
	internal static class VisualElementListPool
	{
		// Token: 0x06002B56 RID: 11094 RVA: 0x0009CA7C File Offset: 0x0009AC7C
		public static List<VisualElement> Copy(List<VisualElement> elements)
		{
			List<VisualElement> list = VisualElementListPool.pool.Get();
			list.AddRange(elements);
			return list;
		}

		// Token: 0x06002B57 RID: 11095 RVA: 0x0009CAA4 File Offset: 0x0009ACA4
		public static List<VisualElement> Get(int initialCapacity = 0)
		{
			List<VisualElement> list = VisualElementListPool.pool.Get();
			bool flag = initialCapacity > 0 && list.Capacity < initialCapacity;
			if (flag)
			{
				list.Capacity = initialCapacity;
			}
			return list;
		}

		// Token: 0x06002B58 RID: 11096 RVA: 0x0009CAE0 File Offset: 0x0009ACE0
		public static void Release(List<VisualElement> elements)
		{
			elements.Clear();
			VisualElementListPool.pool.Release(elements);
		}

		// Token: 0x040013A9 RID: 5033
		private static ObjectPool<List<VisualElement>> pool = new ObjectPool<List<VisualElement>>(() => new List<VisualElement>(), 20);
	}
}
