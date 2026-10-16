using System;
using System.Collections.Generic;

namespace UnityEngine.Pool
{
	// Token: 0x0200042A RID: 1066
	internal static class PoolManager
	{
		// Token: 0x060029B0 RID: 10672 RVA: 0x000506A8 File Offset: 0x0004E8A8
		public static void Reset()
		{
			for (int i = PoolManager.s_WeakPoolReferences.Count - 1; i >= 0; i--)
			{
				IPool pool;
				bool flag = PoolManager.s_WeakPoolReferences[i].TryGetTarget(out pool);
				if (flag)
				{
					pool.Clear();
				}
				else
				{
					PoolManager.s_WeakPoolReferences.RemoveAt(i);
				}
			}
		}

		// Token: 0x060029B1 RID: 10673 RVA: 0x00050705 File Offset: 0x0004E905
		public static void Register(IPool pool)
		{
			PoolManager.s_WeakPoolReferences.Add(new WeakReference<IPool>(pool));
		}

		// Token: 0x04000BDB RID: 3035
		private static readonly List<WeakReference<IPool>> s_WeakPoolReferences = new List<WeakReference<IPool>>();
	}
}
