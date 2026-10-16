using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.CodeAnalysis.PooledObjects
{
	// Token: 0x0200029B RID: 667
	[NullableContext(1)]
	[Nullable(0)]
	internal class ObjectPool<T> where T : class
	{
		// Token: 0x06001C48 RID: 7240 RVA: 0x000537D4 File Offset: 0x000519D4
		internal ObjectPool([Nullable(new byte[] { 1, 0 })] ObjectPool<T>.Factory factory)
			: this(factory, Environment.ProcessorCount * 2)
		{
		}

		// Token: 0x06001C49 RID: 7241 RVA: 0x000537E4 File Offset: 0x000519E4
		internal ObjectPool([Nullable(new byte[] { 1, 0 })] ObjectPool<T>.Factory factory, int size)
		{
			this._factory = factory;
			this._items = new ObjectPool<T>.Element[size - 1];
		}

		// Token: 0x06001C4A RID: 7242 RVA: 0x00053804 File Offset: 0x00051A04
		internal ObjectPool(Func<ObjectPool<T>, T> factory, int size)
		{
			ObjectPool<T> <>4__this = this;
			this._factory = () => factory.Invoke(<>4__this);
			this._items = new ObjectPool<T>.Element[size - 1];
		}

		// Token: 0x06001C4B RID: 7243 RVA: 0x0005384B File Offset: 0x00051A4B
		private T CreateInstance()
		{
			return this._factory();
		}

		// Token: 0x06001C4C RID: 7244 RVA: 0x00053858 File Offset: 0x00051A58
		internal T Allocate()
		{
			T t = this._firstItem;
			if (t == null || t != Interlocked.CompareExchange<T>(ref this._firstItem, default(T), t))
			{
				t = this.AllocateSlow();
			}
			return t;
		}

		// Token: 0x06001C4D RID: 7245 RVA: 0x000538A0 File Offset: 0x00051AA0
		private T AllocateSlow()
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

		// Token: 0x06001C4E RID: 7246 RVA: 0x00053907 File Offset: 0x00051B07
		internal void Free(T obj)
		{
			if (this._firstItem == null)
			{
				this._firstItem = obj;
				return;
			}
			this.FreeSlow(obj);
		}

		// Token: 0x06001C4F RID: 7247 RVA: 0x00053928 File Offset: 0x00051B28
		private void FreeSlow(T obj)
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

		// Token: 0x06001C50 RID: 7248 RVA: 0x0005396B File Offset: 0x00051B6B
		[Conditional("DEBUG")]
		internal void ForgetTrackedObject(T old, [Nullable(2)] T replacement = default(T))
		{
		}

		// Token: 0x06001C51 RID: 7249 RVA: 0x00053970 File Offset: 0x00051B70
		[Conditional("DEBUG")]
		private void Validate(object obj)
		{
			ObjectPool<T>.Element[] items = this._items;
			for (int i = 0; i < items.Length; i++)
			{
				if (items[i].Value == null)
				{
					return;
				}
			}
		}

		// Token: 0x04000D49 RID: 3401
		[Nullable(2)]
		private T _firstItem;

		// Token: 0x04000D4A RID: 3402
		[Nullable(new byte[] { 1, 0, 0 })]
		private readonly ObjectPool<T>.Element[] _items;

		// Token: 0x04000D4B RID: 3403
		[Nullable(new byte[] { 1, 0 })]
		private readonly ObjectPool<T>.Factory _factory;

		// Token: 0x0200063B RID: 1595
		[NullableContext(0)]
		[DebuggerDisplay("{Value,nq}")]
		private struct Element
		{
			// Token: 0x04001848 RID: 6216
			[Nullable(2)]
			internal T Value;
		}

		// Token: 0x0200063C RID: 1596
		// (Invoke) Token: 0x0600386A RID: 14442
		[NullableContext(0)]
		internal delegate T Factory();
	}
}
