using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Genkit;

namespace XRL.Collections
{
	// Token: 0x020005D0 RID: 1488
	[Serializable]
	public class Rack<T> : Container<T>, IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IReadOnlyList<T>, IReadOnlyCollection<T>, IList, ICollection
	{
		// Token: 0x06003F87 RID: 16263 RVA: 0x00138953 File Offset: 0x00136B53
		public Rack()
		{
		}

		// Token: 0x06003F88 RID: 16264 RVA: 0x0013895B File Offset: 0x00136B5B
		public Rack(int Capacity)
		{
			base.EnsureCapacity(Capacity);
		}

		// Token: 0x06003F89 RID: 16265 RVA: 0x0013896C File Offset: 0x00136B6C
		public Rack(IReadOnlyList<T> List)
		{
			if (List != null)
			{
				int count = List.Count;
				base.EnsureCapacity(count);
				for (int i = 0; i < count; i++)
				{
					this.Add(List[i]);
				}
			}
		}

		// Token: 0x1700067E RID: 1662
		public virtual T this[int Index]
		{
			get
			{
				if (Index >= this.Length)
				{
					throw new ArgumentOutOfRangeException();
				}
				return this.Items[Index];
			}
			set
			{
				if (Index >= this.Length)
				{
					throw new ArgumentOutOfRangeException();
				}
				this.Items[Index] = value;
				this.Variant++;
			}
		}

		/// <summary>Retrieve a value by reference.</summary>
		// Token: 0x06003F8C RID: 16268 RVA: 0x001389F2 File Offset: 0x00136BF2
		public ref T GetReference(int Index)
		{
			if (Index >= this.Length)
			{
				throw new ArgumentOutOfRangeException();
			}
			return ref this.Items[Index];
		}

		// Token: 0x06003F8D RID: 16269 RVA: 0x00138A10 File Offset: 0x00136C10
		public virtual void Add(T Item)
		{
			if (this.Length == this.Size)
			{
				base.Resize(this.Length * 2);
			}
			T[] items = this.Items;
			int length = this.Length;
			this.Length = length + 1;
			items[length] = Item;
			this.Variant++;
		}

		// Token: 0x06003F8E RID: 16270 RVA: 0x00138A64 File Offset: 0x00136C64
		public void AddRange(IReadOnlyList<T> Items)
		{
			int count = Items.Count;
			base.EnsureCapacity(this.Length + count);
			for (int i = 0; i < count; i++)
			{
				this.Add(Items[i]);
			}
		}

		// Token: 0x06003F8F RID: 16271 RVA: 0x00138AA0 File Offset: 0x00136CA0
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

		// Token: 0x06003F90 RID: 16272 RVA: 0x00138B10 File Offset: 0x00136D10
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

		// Token: 0x06003F91 RID: 16273 RVA: 0x00138B6C File Offset: 0x00136D6C
		public void AddRange(ReadOnlySpan<T> Items)
		{
			base.EnsureCapacity(this.Length + Items.Length);
			Items.CopyTo(this.Items.AsSpan<T>(this.Length, Items.Length));
			this.Length += Items.Length;
			this.Variant++;
		}

		// Token: 0x06003F92 RID: 16274 RVA: 0x00138BCE File Offset: 0x00136DCE
		public Span<T> FillSpan(int Length)
		{
			base.EnsureCapacity(this.Length + Length);
			Span<T> span = new Span<T>(this.Items, this.Length, Length);
			this.Length += Length;
			this.Variant++;
			return span;
		}

		// Token: 0x06003F93 RID: 16275 RVA: 0x00138C0C File Offset: 0x00136E0C
		public virtual void Clear()
		{
			if (this.Length > 0)
			{
				Array.Clear(this.Items, 0, this.Length);
				this.Length = 0;
			}
			this.Variant++;
		}

		// Token: 0x06003F94 RID: 16276 RVA: 0x00138C3E File Offset: 0x00136E3E
		public void Reverse()
		{
			this.Reverse(0, this.Length);
		}

		// Token: 0x06003F95 RID: 16277 RVA: 0x00138C4D File Offset: 0x00136E4D
		public virtual void Reverse(int Index, int Length)
		{
			Array.Reverse<T>(this.Items, Index, Length);
			this.Variant++;
		}

		// Token: 0x06003F96 RID: 16278 RVA: 0x00138C6C File Offset: 0x00136E6C
		public virtual bool Remove(T Item)
		{
			int num = Array.IndexOf<T>(this.Items, Item, 0, this.Length);
			if (num >= 0)
			{
				this.RemoveAt(num);
				return true;
			}
			return false;
		}

		// Token: 0x06003F97 RID: 16279 RVA: 0x00138C9C File Offset: 0x00136E9C
		public virtual void Insert(int Index, T Item)
		{
			if (Index > this.Length)
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
			this.Variant++;
		}

		// Token: 0x06003F98 RID: 16280 RVA: 0x00138D24 File Offset: 0x00136F24
		public virtual void RemoveAt(int Index)
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
			this.Variant++;
		}

		// Token: 0x06003F99 RID: 16281 RVA: 0x00138D9C File Offset: 0x00136F9C
		public T TakeAt(int Index)
		{
			T t = this.Items[Index];
			this.RemoveAt(Index);
			return t;
		}

		// Token: 0x06003F9A RID: 16282 RVA: 0x00138DB4 File Offset: 0x00136FB4
		public void ShuffleInPlace(Random Random = null)
		{
			if (Random == null)
			{
				Random = Calc.R;
			}
			for (int i = this.Length - 1; i >= 1; i--)
			{
				int num = Random.Next(i + 1);
				T t = this.Items[i];
				this.Items[i] = this.Items[num];
				this.Items[num] = t;
			}
		}

		// Token: 0x06003F9B RID: 16283 RVA: 0x00138E1A File Offset: 0x0013701A
		public void Sort()
		{
			this.Sort(0, this.Length, null);
		}

		// Token: 0x06003F9C RID: 16284 RVA: 0x00138E2A File Offset: 0x0013702A
		public void Sort(IComparer<T> Comparer)
		{
			this.Sort(0, this.Length, Comparer);
		}

		// Token: 0x06003F9D RID: 16285 RVA: 0x00138E3A File Offset: 0x0013703A
		public void Sort(int Index, int Count, IComparer<T> Comparer)
		{
			Array.Sort<T>(this.Items, Index, Count, Comparer);
			this.Variant++;
		}

		// Token: 0x06003F9E RID: 16286 RVA: 0x00138E58 File Offset: 0x00137058
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public T[] GetArray()
		{
			return this.Items;
		}

		// Token: 0x06003F9F RID: 16287 RVA: 0x00138E60 File Offset: 0x00137060
		public T[] GetArray(int Capacity)
		{
			base.EnsureCapacity(Capacity);
			return this.Items;
		}

		/// <summary>Reuse an existing array by wrapping it in a rack.</summary>
		// Token: 0x06003FA0 RID: 16288 RVA: 0x00138E6F File Offset: 0x0013706F
		public static Rack<T> Wrap(T[] Array, int Length)
		{
			return new Rack<T>
			{
				Items = Array,
				Size = Array.Length,
				Length = Length
			};
		}

		// Token: 0x06003FA1 RID: 16289 RVA: 0x00138E8D File Offset: 0x0013708D
		public static implicit operator Span<T>(Rack<T> Rack)
		{
			return Rack.AsSpan();
		}

		// Token: 0x06003FA2 RID: 16290 RVA: 0x00138E95 File Offset: 0x00137095
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public new Span<T> AsSpan()
		{
			return new Span<T>(this.Items, 0, this.Length);
		}

		// Token: 0x06003FA3 RID: 16291 RVA: 0x00138EA9 File Offset: 0x001370A9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public new Span<T> AsSpan(int Start)
		{
			if (Start > this.Length)
			{
				throw new ArgumentOutOfRangeException("Start");
			}
			return new Span<T>(this.Items, Start, this.Length - Start);
		}

		// Token: 0x06003FA4 RID: 16292 RVA: 0x00138ED3 File Offset: 0x001370D3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public new Span<T> AsSpan(int Start, int Length)
		{
			if (Start + Length > this.Length)
			{
				throw new ArgumentOutOfRangeException("Length");
			}
			return new Span<T>(this.Items, Start, Length);
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06003FA5 RID: 16293 RVA: 0x00138EF8 File Offset: 0x001370F8
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06003FA6 RID: 16294 RVA: 0x00138EFB File Offset: 0x001370FB
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x06003FA7 RID: 16295 RVA: 0x00138EFE File Offset: 0x001370FE
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000682 RID: 1666
		object IList.this[int Index]
		{
			get
			{
				if (Index < this.Length)
				{
					return this.Items[Index];
				}
				throw new ArgumentOutOfRangeException();
			}
			set
			{
				if (Index < this.Length)
				{
					this.Variant++;
					this.Items[Index] = (T)((object)value);
				}
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x06003FAA RID: 16298 RVA: 0x00138F53 File Offset: 0x00137153
		int IList.Add(object Value)
		{
			this.Add((T)((object)Value));
			return this.Length - 1;
		}

		// Token: 0x06003FAB RID: 16299 RVA: 0x00138F6C File Offset: 0x0013716C
		bool IList.Contains(object Value)
		{
			if (Value is T)
			{
				T t = (T)((object)Value);
				return base.Contains(t);
			}
			return false;
		}

		// Token: 0x06003FAC RID: 16300 RVA: 0x00138F94 File Offset: 0x00137194
		int IList.IndexOf(object Value)
		{
			if (Value is T)
			{
				T t = (T)((object)Value);
				return base.IndexOf(t);
			}
			return -1;
		}

		// Token: 0x06003FAD RID: 16301 RVA: 0x00138FB9 File Offset: 0x001371B9
		void IList.Insert(int Index, object Value)
		{
			this.Insert(Index, (T)((object)Value));
		}

		// Token: 0x06003FAE RID: 16302 RVA: 0x00138FC8 File Offset: 0x001371C8
		void IList.Remove(object Value)
		{
			if (Value is T)
			{
				T t = (T)((object)Value);
				this.Remove(t);
			}
		}

		// Token: 0x06003FAF RID: 16303 RVA: 0x00138FEC File Offset: 0x001371EC
		void ICollection.CopyTo(Array Array, int Index)
		{
			Array.Copy(this.Items, 0, Array, Index, this.Length);
		}
	}
}
