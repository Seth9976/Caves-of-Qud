using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace XRL.Collections
{
	/// <summary>A list using the <see cref="P:System.Buffers.ArrayPool`1.Shared" /> pool for its internal arrays.</summary>
	// Token: 0x020005CF RID: 1487
	[Serializable]
	public class PooledList<T> : PooledContainer<T>, IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IReadOnlyList<T>, IReadOnlyCollection<T>
	{
		// Token: 0x06003F73 RID: 16243 RVA: 0x00138559 File Offset: 0x00136759
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static PooledList<T> Get()
		{
			return ScopeDisposedList<T>.GetFromPool();
		}

		// Token: 0x1700067D RID: 1661
		public T this[int Index]
		{
			get
			{
				if (Index < this.Length)
				{
					return this.Items[Index];
				}
				throw new IndexOutOfRangeException();
			}
			set
			{
				if (Index >= this.Length)
				{
					throw new IndexOutOfRangeException();
				}
				this.Version++;
				this.Items[Index] = value;
			}
		}

		// Token: 0x06003F76 RID: 16246 RVA: 0x001385A9 File Offset: 0x001367A9
		public PooledList()
		{
		}

		// Token: 0x06003F77 RID: 16247 RVA: 0x001385B1 File Offset: 0x001367B1
		public PooledList(int Capacity)
		{
			base.EnsureCapacity(Capacity);
		}

		// Token: 0x06003F78 RID: 16248 RVA: 0x001385C0 File Offset: 0x001367C0
		public PooledList(IReadOnlyList<T> List)
		{
			this.AddRange(List);
		}

		/// <summary>Retrieve a value by reference.</summary>
		// Token: 0x06003F79 RID: 16249 RVA: 0x001385CF File Offset: 0x001367CF
		public ref T GetReference(int Index)
		{
			if (Index >= this.Length)
			{
				throw new ArgumentOutOfRangeException();
			}
			return ref this.Items[Index];
		}

		// Token: 0x06003F7A RID: 16250 RVA: 0x001385EC File Offset: 0x001367EC
		public void Add(T Item)
		{
			if (this.Length == this.Size)
			{
				base.Resize(this.Length * 2);
			}
			T[] items = this.Items;
			int length = this.Length;
			this.Length = length + 1;
			items[length] = Item;
			this.Version++;
		}

		// Token: 0x06003F7B RID: 16251 RVA: 0x00138640 File Offset: 0x00136840
		public void AddRange(IReadOnlyList<T> Items)
		{
			int count = Items.Count;
			base.EnsureCapacity(this.Length + count);
			for (int i = 0; i < count; i++)
			{
				this.Add(Items[i]);
			}
		}

		// Token: 0x06003F7C RID: 16252 RVA: 0x0013867C File Offset: 0x0013687C
		public void AddRange(IReadOnlyCollection<T> Items)
		{
			IReadOnlyList<T> readOnlyList = Items as IReadOnlyList<T>;
			if (readOnlyList != null)
			{
				this.AddRange(readOnlyList);
				return;
			}
			base.EnsureCapacity(this.Length + Items.Count);
			foreach (T t in Items)
			{
				this.Add(t);
			}
		}

		// Token: 0x06003F7D RID: 16253 RVA: 0x001386EC File Offset: 0x001368EC
		public void AddRange(IEnumerable<T> Items)
		{
			IReadOnlyCollection<T> readOnlyCollection = Items as IReadOnlyCollection<T>;
			if (readOnlyCollection != null)
			{
				this.AddRange(readOnlyCollection);
				return;
			}
			foreach (T t in Items)
			{
				this.Add(t);
			}
		}

		// Token: 0x06003F7E RID: 16254 RVA: 0x00138748 File Offset: 0x00136948
		public void AddRange(ReadOnlySpan<T> Items)
		{
			base.EnsureCapacity(this.Length + Items.Length);
			Items.CopyTo(this.Items.AsSpan<T>(this.Length, Items.Length));
			this.Length += Items.Length;
			this.Version++;
		}

		// Token: 0x06003F7F RID: 16255 RVA: 0x001387AA File Offset: 0x001369AA
		public void Clear()
		{
			if (this.Length > 0)
			{
				Array.Clear(this.Items, 0, this.Length);
				this.Length = 0;
			}
			this.Version++;
		}

		// Token: 0x06003F80 RID: 16256 RVA: 0x001387DC File Offset: 0x001369DC
		public bool Remove(T Item)
		{
			int num = Array.IndexOf<T>(this.Items, Item, 0, this.Length);
			if (num >= 0)
			{
				this.RemoveAt(num);
				return true;
			}
			return false;
		}

		// Token: 0x06003F81 RID: 16257 RVA: 0x0013880C File Offset: 0x00136A0C
		public void Insert(int Index, T Item)
		{
			if (Index > this.Size)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (this.Length == this.Size)
			{
				base.Resize(this.Size * 2);
			}
			if (Index < this.Length)
			{
				Array.Copy(this.Items, Index, this.Items, Index + 1, this.Length - Index);
			}
			this.Items[Index] = Item;
			this.Length++;
			this.Version++;
		}

		// Token: 0x06003F82 RID: 16258 RVA: 0x00138894 File Offset: 0x00136A94
		public void RemoveAt(int Index)
		{
			if (Index >= this.Length)
			{
				throw new ArgumentOutOfRangeException();
			}
			this.Length--;
			if (Index < this.Length)
			{
				Array.Copy(this.Items, Index + 1, this.Items, Index, this.Length - Index);
			}
			this.Items[this.Length] = default(T);
			this.Version++;
		}

		// Token: 0x06003F83 RID: 16259 RVA: 0x0013890C File Offset: 0x00136B0C
		public T TakeAt(int Index)
		{
			T t = this.Items[Index];
			this.RemoveAt(Index);
			return t;
		}

		// Token: 0x06003F84 RID: 16260 RVA: 0x00138921 File Offset: 0x00136B21
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Span<T> AsSpan()
		{
			return new Span<T>(this.Items);
		}

		// Token: 0x06003F85 RID: 16261 RVA: 0x0013892E File Offset: 0x00136B2E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Span<T> AsSpan(int Start)
		{
			return new Span<T>(this.Items, Start, this.Length - Start);
		}

		// Token: 0x06003F86 RID: 16262 RVA: 0x00138944 File Offset: 0x00136B44
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Span<T> AsSpan(int Start, int Length)
		{
			return new Span<T>(this.Items, Start, Length);
		}
	}
}
