using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Reflection.Internal
{
	// Token: 0x02000187 RID: 391
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class ObjectPool<T> where T : class
	{
		// Token: 0x06000C02 RID: 3074 RVA: 0x00024501 File Offset: 0x00022701
		internal ObjectPool(Func<T> factory)
			: this(factory, Environment.ProcessorCount * 2)
		{
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x00024511 File Offset: 0x00022711
		internal ObjectPool(Func<T> factory, int size)
		{
			this._factory = factory;
			this._items = new ObjectPool<T>.Element[size];
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x0002452C File Offset: 0x0002272C
		private T CreateInstance()
		{
			return this._factory.Invoke();
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x00024548 File Offset: 0x00022748
		internal T Allocate()
		{
			ObjectPool<T>.Element[] items = this._items;
			for (int i = 0; i < items.Length; i++)
			{
				T value = items[i].Value;
				if (value != null && value == Interlocked.CompareExchange<T>(ref items[i].Value, default(T), value))
				{
					return value;
				}
			}
			return this.CreateInstance();
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x000245B0 File Offset: 0x000227B0
		internal void Free(T obj)
		{
			ObjectPool<T>.Element[] items = this._items;
			for (int i = 0; i < items.Length; i++)
			{
				if (items[i].Value == null)
				{
					items[i].Value = obj;
					return;
				}
			}
		}

		// Token: 0x0400096C RID: 2412
		private readonly ObjectPool<T>.Element[] _items;

		// Token: 0x0400096D RID: 2413
		private readonly Func<T> _factory;

		// Token: 0x0200020E RID: 526
		private struct Element
		{
			// Token: 0x04000BF0 RID: 3056
			internal T Value;
		}
	}
}
