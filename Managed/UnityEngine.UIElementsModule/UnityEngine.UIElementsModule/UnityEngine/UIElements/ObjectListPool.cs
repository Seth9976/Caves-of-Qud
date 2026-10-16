using System;
using System.Collections.Generic;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x020005B0 RID: 1456
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal class ObjectListPool<T>
	{
		// Token: 0x06002B5D RID: 11101 RVA: 0x0009CB28 File Offset: 0x0009AD28
		public static List<T> Get()
		{
			return ObjectListPool<T>.pool.Get();
		}

		// Token: 0x06002B5E RID: 11102 RVA: 0x0009CB44 File Offset: 0x0009AD44
		public static void Release(List<T> elements)
		{
			elements.Clear();
			ObjectListPool<T>.pool.Release(elements);
		}

		// Token: 0x040013AB RID: 5035
		private static ObjectPool<List<T>> pool = new ObjectPool<List<T>>(() => new List<T>(), 20);
	}
}
