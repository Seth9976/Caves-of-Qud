using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Roslyn.Utilities
{
	// Token: 0x0200006C RID: 108
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class WeakList<T> : IEnumerable<T>, IEnumerable where T : class
	{
		// Token: 0x06000288 RID: 648 RVA: 0x000091C2 File Offset: 0x000073C2
		public WeakList()
		{
			this._items = Array.Empty<WeakReference<T>>();
		}

		// Token: 0x06000289 RID: 649 RVA: 0x000091D8 File Offset: 0x000073D8
		private void Resize()
		{
			int num = this._items.Length;
			int num2 = -1;
			for (int i = 0; i < this._items.Length; i++)
			{
				T t;
				if (!this._items[i].TryGetTarget(ref t))
				{
					if (num2 == -1)
					{
						num2 = i;
					}
					num--;
				}
			}
			if (num < this._items.Length / 4)
			{
				this.Shrink(num2, num);
				return;
			}
			if (num >= 3 * this._items.Length / 4)
			{
				WeakReference<T>[] array = new WeakReference<T>[WeakList<T>.GetExpandedSize(this._items.Length)];
				if (num2 >= 0)
				{
					this.Compact(num2, array);
				}
				else
				{
					Array.Copy(this._items, 0, array, 0, this._items.Length);
				}
				this._items = array;
				return;
			}
			this.Compact(num2, this._items);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00009294 File Offset: 0x00007494
		private void Shrink(int firstDead, int alive)
		{
			int expandedSize = WeakList<T>.GetExpandedSize(alive);
			WeakReference<T>[] array = ((expandedSize == this._items.Length) ? this._items : new WeakReference<T>[expandedSize]);
			this.Compact(firstDead, array);
			this._items = array;
		}

		// Token: 0x0600028B RID: 651 RVA: 0x000092D1 File Offset: 0x000074D1
		private static int GetExpandedSize(int baseSize)
		{
			return Math.Max(baseSize * 2 + 1, 4);
		}

		// Token: 0x0600028C RID: 652 RVA: 0x000092E0 File Offset: 0x000074E0
		private void Compact(int firstDead, WeakReference<T>[] result)
		{
			if (this._items != result)
			{
				Array.Copy(this._items, 0, result, 0, firstDead);
			}
			int size = this._size;
			int i = firstDead;
			for (int j = firstDead + 1; j < size; j++)
			{
				WeakReference<T> weakReference = this._items[j];
				T t;
				if (weakReference.TryGetTarget(ref t))
				{
					result[i++] = weakReference;
				}
			}
			this._size = i;
			if (this._items == result)
			{
				while (i < size)
				{
					this._items[i++] = null;
				}
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600028D RID: 653 RVA: 0x0000935A File Offset: 0x0000755A
		public int WeakCount
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00009362 File Offset: 0x00007562
		public WeakReference<T> GetWeakReference(int index)
		{
			if (index < 0 || index >= this._size)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return this._items[index];
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00009384 File Offset: 0x00007584
		public void Add(T item)
		{
			if (this._size == this._items.Length)
			{
				this.Resize();
			}
			WeakReference<T>[] items = this._items;
			int size = this._size;
			this._size = size + 1;
			items[size] = new WeakReference<T>(item);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x000093C5 File Offset: 0x000075C5
		public IEnumerator<T> GetEnumerator()
		{
			int count = this._size;
			int alive = this._size;
			int firstDead = -1;
			int num;
			for (int i = 0; i < count; i = num + 1)
			{
				T t;
				if (this._items[i].TryGetTarget(ref t))
				{
					yield return t;
				}
				else
				{
					if (firstDead < 0)
					{
						firstDead = i;
					}
					num = alive;
					alive = num - 1;
				}
				num = i;
			}
			if (alive == 0)
			{
				this._items = Array.Empty<WeakReference<T>>();
				this._size = 0;
			}
			else if (alive < this._items.Length / 4)
			{
				this.Shrink(firstDead, alive);
			}
			yield break;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x000093D4 File Offset: 0x000075D4
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000292 RID: 658 RVA: 0x000093DC File Offset: 0x000075DC
		internal WeakReference<T>[] TestOnly_UnderlyingArray
		{
			get
			{
				return this._items;
			}
		}

		// Token: 0x040000B1 RID: 177
		private WeakReference<T>[] _items;

		// Token: 0x040000B2 RID: 178
		private int _size;

		// Token: 0x040000B3 RID: 179
		private const int MinimalNonEmptySize = 4;
	}
}
