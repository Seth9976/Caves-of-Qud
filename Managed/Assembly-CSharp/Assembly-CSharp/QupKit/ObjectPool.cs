using System;
using System.Collections.Generic;

namespace QupKit
{
	// Token: 0x020003A4 RID: 932
	public class ObjectPool<T> where T : new()
	{
		// Token: 0x060022F5 RID: 8949 RVA: 0x000B1C04 File Offset: 0x000AFE04
		public static T Checkout()
		{
			Queue<T> pool = ObjectPool<T>.Pool;
			T t2;
			lock (pool)
			{
				T t;
				if (!ObjectPool<T>.Pool.TryDequeue(out t))
				{
					t = new T();
				}
				t2 = t;
			}
			return t2;
		}

		// Token: 0x060022F6 RID: 8950 RVA: 0x000B1C54 File Offset: 0x000AFE54
		public static bool TryCheckout(out T Object)
		{
			Queue<T> pool = ObjectPool<T>.Pool;
			bool flag2;
			lock (pool)
			{
				flag2 = ObjectPool<T>.Pool.TryDequeue(out Object);
			}
			return flag2;
		}

		// Token: 0x060022F7 RID: 8951 RVA: 0x000B1C9C File Offset: 0x000AFE9C
		public static void Return(T item)
		{
			Queue<T> pool = ObjectPool<T>.Pool;
			lock (pool)
			{
				ObjectPool<T>.Pool.Enqueue(item);
			}
		}

		// Token: 0x0400119E RID: 4510
		private static Queue<T> Pool = new Queue<T>();
	}
}
