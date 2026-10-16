using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using XRL.World;

namespace XRL.Collections
{
	// Token: 0x020005CE RID: 1486
	[Serializable]
	public abstract class PooledContainer<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IReadOnlyList<T>, IReadOnlyCollection<T>, IComposite, IDisposable
	{
		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x06003F53 RID: 16211 RVA: 0x00138230 File Offset: 0x00136430
		protected virtual ArrayPool<T> Pool
		{
			get
			{
				return PooledContainer<T>.DefaultPool;
			}
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06003F54 RID: 16212 RVA: 0x00138237 File Offset: 0x00136437
		protected virtual int DefaultCapacity
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x06003F55 RID: 16213 RVA: 0x0013823A File Offset: 0x0013643A
		public bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x06003F56 RID: 16214 RVA: 0x0013823D File Offset: 0x0013643D
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x06003F57 RID: 16215 RVA: 0x00138240 File Offset: 0x00136440
		public int Capacity
		{
			get
			{
				return this.Size;
			}
		}

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x06003F58 RID: 16216 RVA: 0x00138248 File Offset: 0x00136448
		public int Count
		{
			get
			{
				return this.Length;
			}
		}

		// Token: 0x1700067B RID: 1659
		T IList<T>.this[int Index]
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
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700067C RID: 1660
		public T this[int Index]
		{
			get
			{
				if (Index < this.Length)
				{
					return this.Items[Index];
				}
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x06003F5C RID: 16220 RVA: 0x00138291 File Offset: 0x00136491
		public void EnsureCapacity(int Capacity)
		{
			if (this.Size < Capacity)
			{
				this.Resize(Capacity);
			}
		}

		// Token: 0x06003F5D RID: 16221 RVA: 0x001382A4 File Offset: 0x001364A4
		protected void Resize(int Capacity)
		{
			if (Capacity == 0)
			{
				Capacity = this.DefaultCapacity;
			}
			T[] array = this.Pool.Rent(Capacity);
			if (this.Size > 0)
			{
				Array.Copy(this.Items, 0, array, 0, this.Length);
				Array.Clear(this.Items, 0, this.Length);
				this.Pool.Return(this.Items, false);
			}
			this.Items = array;
			this.Size = array.Length;
		}

		// Token: 0x06003F5E RID: 16222 RVA: 0x0013831A File Offset: 0x0013651A
		void ICollection<T>.Add(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003F5F RID: 16223 RVA: 0x00138321 File Offset: 0x00136521
		bool ICollection<T>.Remove(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003F60 RID: 16224 RVA: 0x00138328 File Offset: 0x00136528
		void ICollection<T>.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003F61 RID: 16225 RVA: 0x0013832F File Offset: 0x0013652F
		void IList<T>.Insert(int Index, T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003F62 RID: 16226 RVA: 0x00138336 File Offset: 0x00136536
		void IList<T>.RemoveAt(int Index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003F63 RID: 16227 RVA: 0x0013833D File Offset: 0x0013653D
		public bool Contains(T Item)
		{
			return Array.IndexOf<T>(this.Items, Item, 0, this.Length) >= 0;
		}

		// Token: 0x06003F64 RID: 16228 RVA: 0x00138358 File Offset: 0x00136558
		public void CopyTo(T[] Buffer, int Index)
		{
			Array.Copy(this.Items, 0, Buffer, Index, this.Length);
		}

		// Token: 0x06003F65 RID: 16229 RVA: 0x0013836E File Offset: 0x0013656E
		public int IndexOf(T Item)
		{
			return Array.IndexOf<T>(this.Items, Item, 0, this.Length);
		}

		// Token: 0x06003F66 RID: 16230 RVA: 0x00138383 File Offset: 0x00136583
		public void Sort()
		{
			this.Sort(0, this.Length, null);
		}

		// Token: 0x06003F67 RID: 16231 RVA: 0x00138393 File Offset: 0x00136593
		public void Sort(IComparer<T> Comparer)
		{
			this.Sort(0, this.Length, Comparer);
		}

		// Token: 0x06003F68 RID: 16232 RVA: 0x001383A3 File Offset: 0x001365A3
		public void Sort(int Index, int Count, IComparer<T> Comparer)
		{
			Array.Sort<T>(this.Items, Index, Count, Comparer);
			this.Version++;
		}

		// Token: 0x06003F69 RID: 16233 RVA: 0x001383C4 File Offset: 0x001365C4
		public virtual void Write(SerializationWriter Writer)
		{
			Writer.WriteOptimized(this.Length);
			for (int i = 0; i < this.Length; i++)
			{
				Writer.WriteObject(this.Items[i]);
			}
		}

		// Token: 0x06003F6A RID: 16234 RVA: 0x00138408 File Offset: 0x00136608
		public virtual void Read(SerializationReader Reader)
		{
			this.Length = Reader.ReadOptimizedInt32();
			this.Items = this.Pool.Rent(this.Length);
			this.Size = this.Items.Length;
			for (int i = 0; i < this.Length; i++)
			{
				this.Items[i] = (T)((object)Reader.ReadObject());
			}
		}

		// Token: 0x06003F6B RID: 16235 RVA: 0x00138470 File Offset: 0x00136670
		public virtual void Dispose()
		{
			if (this.Size > 0)
			{
				Array.Clear(this.Items, 0, this.Length);
				this.Pool.Return<T>(ref this.Items, false);
				this.Length = (this.Size = 0);
			}
		}

		// Token: 0x06003F6C RID: 16236 RVA: 0x001384BC File Offset: 0x001366BC
		public T[] ToArray()
		{
			T[] array = new T[this.Length];
			Array.Copy(this.Items, 0, array, 0, this.Length);
			return array;
		}

		// Token: 0x06003F6D RID: 16237 RVA: 0x001384EC File Offset: 0x001366EC
		~PooledContainer()
		{
			this.Dispose();
		}

		// Token: 0x06003F6E RID: 16238 RVA: 0x00138518 File Offset: 0x00136718
		public PooledContainer<T>.Enumerator GetEnumerator()
		{
			return new PooledContainer<T>.Enumerator(this);
		}

		// Token: 0x06003F6F RID: 16239 RVA: 0x00138520 File Offset: 0x00136720
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return new PooledContainer<T>.Enumerator(this);
		}

		// Token: 0x06003F70 RID: 16240 RVA: 0x0013852D File Offset: 0x0013672D
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new PooledContainer<T>.Enumerator(this);
		}

		// Token: 0x04001F43 RID: 8003
		protected static readonly ArrayPool<T> DefaultPool = ArrayPool<T>.Shared;

		// Token: 0x04001F44 RID: 8004
		protected T[] Items = Array.Empty<T>();

		// Token: 0x04001F45 RID: 8005
		protected int Size;

		// Token: 0x04001F46 RID: 8006
		protected int Length;

		// Token: 0x04001F47 RID: 8007
		protected int Version;

		// Token: 0x020019E2 RID: 6626
		[Serializable]
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x17000EAC RID: 3756
			// (get) Token: 0x0600E58A RID: 58762 RVA: 0x0052F219 File Offset: 0x0052D419
			public T Current
			{
				get
				{
					return this.Item;
				}
			}

			// Token: 0x17000EAD RID: 3757
			// (get) Token: 0x0600E58B RID: 58763 RVA: 0x0052F221 File Offset: 0x0052D421
			object IEnumerator.Current
			{
				get
				{
					return this.Item;
				}
			}

			// Token: 0x0600E58C RID: 58764 RVA: 0x0052F22E File Offset: 0x0052D42E
			public Enumerator(PooledContainer<T> List)
			{
				this.List = List;
				this.Index = 0;
				this.Version = List.Version;
				this.Item = default(T);
			}

			// Token: 0x0600E58D RID: 58765 RVA: 0x0052F258 File Offset: 0x0052D458
			public bool MoveNext()
			{
				if (this.List.Version != this.Version)
				{
					throw new InvalidOperationException();
				}
				if (this.Index >= this.List.Length)
				{
					this.Item = default(T);
					return false;
				}
				T[] items = this.List.Items;
				int index = this.Index;
				this.Index = index + 1;
				this.Item = items[index];
				return true;
			}

			// Token: 0x0600E58E RID: 58766 RVA: 0x0052F2C7 File Offset: 0x0052D4C7
			public void Reset()
			{
				if (this.List.Version != this.Version)
				{
					throw new InvalidOperationException();
				}
				this.Index = 0;
				this.Item = default(T);
			}

			// Token: 0x0600E58F RID: 58767 RVA: 0x0052F2F5 File Offset: 0x0052D4F5
			public void Dispose()
			{
			}

			// Token: 0x04006788 RID: 26504
			private PooledContainer<T> List;

			// Token: 0x04006789 RID: 26505
			private int Index;

			// Token: 0x0400678A RID: 26506
			private int Version;

			// Token: 0x0400678B RID: 26507
			private T Item;
		}
	}
}
