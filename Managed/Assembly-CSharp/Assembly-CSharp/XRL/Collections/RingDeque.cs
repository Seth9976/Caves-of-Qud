using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using XRL.World;

namespace XRL.Collections
{
	/// <summary>A double-ended queue using an expanding ring buffer.</summary>
	// Token: 0x020005D1 RID: 1489
	[Serializable]
	public class RingDeque<T> : ICollection<T>, IEnumerable<T>, IEnumerable, IComposite
	{
		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x06003FB0 RID: 16304 RVA: 0x00139002 File Offset: 0x00137202
		protected virtual EqualityComparer<T> Comparer
		{
			get
			{
				return EqualityComparer<T>.Default;
			}
		}

		// Token: 0x06003FB1 RID: 16305 RVA: 0x00139009 File Offset: 0x00137209
		public RingDeque()
		{
			this.Buffer = Array.Empty<T>();
		}

		// Token: 0x06003FB2 RID: 16306 RVA: 0x0013901C File Offset: 0x0013721C
		public RingDeque(int Capacity)
		{
			this.Buffer = new T[Capacity];
			this.Size = Capacity;
			this.Head = 0;
			this.Tail = 0;
			this.Amount = 0;
		}

		// Token: 0x17000684 RID: 1668
		public T this[int Index]
		{
			get
			{
				return this.Buffer[(this.Head + Index) % this.Size];
			}
			set
			{
				this.Buffer[(this.Head + Index) % this.Size] = value;
			}
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x06003FB5 RID: 16309 RVA: 0x00139085 File Offset: 0x00137285
		public T First
		{
			get
			{
				return this.Buffer[this.Head];
			}
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06003FB6 RID: 16310 RVA: 0x00139098 File Offset: 0x00137298
		public T Last
		{
			get
			{
				return this.Buffer[(this.Tail + this.Size - 1) % this.Size];
			}
		}

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x06003FB7 RID: 16311 RVA: 0x001390BB File Offset: 0x001372BB
		public int Count
		{
			get
			{
				return this.Amount;
			}
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06003FB8 RID: 16312 RVA: 0x001390C3 File Offset: 0x001372C3
		public int Capacity
		{
			get
			{
				return this.Size;
			}
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06003FB9 RID: 16313 RVA: 0x001390CB File Offset: 0x001372CB
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06003FBA RID: 16314 RVA: 0x001390CE File Offset: 0x001372CE
		public bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003FBB RID: 16315 RVA: 0x001390D1 File Offset: 0x001372D1
		void ICollection<T>.Add(T Item)
		{
			this.Enqueue(Item);
		}

		// Token: 0x06003FBC RID: 16316 RVA: 0x001390DC File Offset: 0x001372DC
		public virtual void Clear()
		{
			if (this.Head < this.Tail)
			{
				Array.Clear(this.Buffer, this.Head, this.Amount);
			}
			else
			{
				Array.Clear(this.Buffer, this.Head, this.Size - this.Head);
				Array.Clear(this.Buffer, 0, this.Tail);
			}
			this.Head = 0;
			this.Tail = 0;
			this.Amount = 0;
			this.Version++;
		}

		/// <summary>Add element to tail of queue.</summary>
		// Token: 0x06003FBD RID: 16317 RVA: 0x00139164 File Offset: 0x00137364
		public virtual void Enqueue(T Item)
		{
			if (this.Amount == this.Size)
			{
				this.SetCapacity((this.Size == 0) ? 4 : (this.Size * 2));
			}
			this.Buffer[this.Tail] = Item;
			this.Tail = (this.Tail + 1) % this.Size;
			this.Amount++;
			this.Version++;
		}

		/// <summary>Add element to head of queue.</summary>
		// Token: 0x06003FBE RID: 16318 RVA: 0x001391DC File Offset: 0x001373DC
		public virtual void Push(T Item)
		{
			if (this.Amount == this.Size)
			{
				this.SetCapacity((this.Size == 0) ? 4 : (this.Size * 2));
			}
			this.Head = (this.Head + this.Size - 1) % this.Size;
			this.Buffer[this.Head] = Item;
			this.Amount++;
			this.Version++;
		}

		/// <summary>Remove element from head of queue.</summary>
		// Token: 0x06003FBF RID: 16319 RVA: 0x0013925C File Offset: 0x0013745C
		public T Dequeue()
		{
			T t;
			this.TryDequeue(out t);
			return t;
		}

		/// <summary>Try to remove element from head of queue, if one exists.</summary>
		// Token: 0x06003FC0 RID: 16320 RVA: 0x00139274 File Offset: 0x00137474
		public virtual bool TryDequeue(out T Value)
		{
			if (this.Amount == 0)
			{
				Value = default(T);
				return false;
			}
			Value = this.Buffer[this.Head];
			this.Buffer[this.Head] = default(T);
			this.Head = (this.Head + 1) % this.Size;
			this.Amount--;
			this.Version++;
			return true;
		}

		/// <summary>Remove element from tail of queue.</summary>
		// Token: 0x06003FC1 RID: 16321 RVA: 0x001392F8 File Offset: 0x001374F8
		public T Eject()
		{
			T t;
			this.TryEject(out t);
			return t;
		}

		/// <summary>Try to remove element from tail of queue, if one exists.</summary>
		// Token: 0x06003FC2 RID: 16322 RVA: 0x00139310 File Offset: 0x00137510
		public virtual bool TryEject(out T Value)
		{
			if (this.Amount == 0)
			{
				Value = default(T);
				return false;
			}
			this.Tail = (this.Tail + this.Size - 1) % this.Size;
			Value = this.Buffer[this.Tail];
			this.Buffer[this.Tail] = default(T);
			this.Amount--;
			this.Version++;
			return true;
		}

		/// <summary>Get index of item closest to head.</summary>
		// Token: 0x06003FC3 RID: 16323 RVA: 0x00139398 File Offset: 0x00137598
		public int IndexOf(T Item)
		{
			EqualityComparer<T> comparer = this.Comparer;
			for (int i = 0; i < this.Amount; i++)
			{
				T t = this.Buffer[(this.Head + i) % this.Size];
				if (t == null)
				{
					if (Item == null)
					{
						return i;
					}
				}
				else if (comparer.Equals(t, Item))
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>Get index of item closest to tail.</summary>
		// Token: 0x06003FC4 RID: 16324 RVA: 0x001393F8 File Offset: 0x001375F8
		public int LastIndexOf(T Item)
		{
			EqualityComparer<T> comparer = this.Comparer;
			for (int i = this.Amount - 1; i >= 0; i--)
			{
				T t = this.Buffer[(this.Head + i) % this.Size];
				if (t == null)
				{
					if (Item == null)
					{
						return i;
					}
				}
				else if (comparer.Equals(t, Item))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06003FC5 RID: 16325 RVA: 0x00139459 File Offset: 0x00137659
		public bool Contains(T Item)
		{
			return this.IndexOf(Item) >= 0;
		}

		// Token: 0x06003FC6 RID: 16326 RVA: 0x00139468 File Offset: 0x00137668
		public bool Remove(T Item)
		{
			int num = this.IndexOf(Item);
			if (num >= 0)
			{
				this.RemoveAt(num);
				return true;
			}
			return false;
		}

		/// <remarks>Removing from start or end of queue is done in constant time, linear otherwise.</remarks>
		// Token: 0x06003FC7 RID: 16327 RVA: 0x0013948C File Offset: 0x0013768C
		public T RemoveAt(int Index)
		{
			Index %= this.Amount;
			T t;
			if (Index == 0)
			{
				t = this.Buffer[this.Head];
				this.Buffer[this.Head] = default(T);
				this.Head = (this.Head + 1) % this.Size;
			}
			else if (Index == this.Amount - 1)
			{
				this.Tail = (this.Tail + this.Size - 1) % this.Size;
				t = this.Buffer[this.Tail];
				this.Buffer[this.Tail] = default(T);
			}
			else
			{
				int num = (this.Head + Index) % this.Size;
				t = this.Buffer[num];
				if (num < this.Tail)
				{
					this.Tail = (this.Tail + this.Size - 1) % this.Size;
					Array.Copy(this.Buffer, num + 1, this.Buffer, num, this.Tail - num);
					this.Buffer[this.Tail] = default(T);
				}
				else
				{
					Array.Copy(this.Buffer, this.Head, this.Buffer, this.Head + 1, num - this.Head);
					this.Buffer[this.Head] = default(T);
					this.Head = (this.Head + 1) % this.Size;
				}
			}
			this.Amount--;
			this.Version++;
			return t;
		}

		// Token: 0x06003FC8 RID: 16328 RVA: 0x00139634 File Offset: 0x00137834
		public void CopyTo(T[] Destination, int Index)
		{
			int num = Destination.Length - Index;
			int num2 = ((num < this.Amount) ? num : this.Amount);
			if (num2 <= 0)
			{
				return;
			}
			if (this.Size - this.Head < num2)
			{
				Array.Copy(this.Buffer, this.Head, Destination, Index, this.Size - this.Head);
				Array.Copy(this.Buffer, 0, Destination, Index + this.Size - this.Head, num2);
				return;
			}
			Array.Copy(this.Buffer, 0, Destination, Index + this.Size - this.Head, num2);
		}

		// Token: 0x06003FC9 RID: 16329 RVA: 0x001396CC File Offset: 0x001378CC
		public T[] ToArray()
		{
			T[] array = new T[this.Amount];
			if (this.Amount == 0)
			{
				return array;
			}
			if (this.Head < this.Tail)
			{
				Array.Copy(this.Buffer, this.Head, array, 0, this.Amount);
			}
			else
			{
				Array.Copy(this.Buffer, this.Head, array, 0, this.Size - this.Head);
				Array.Copy(this.Buffer, 0, array, this.Size - this.Head, this.Tail);
			}
			return array;
		}

		// Token: 0x06003FCA RID: 16330 RVA: 0x0013975C File Offset: 0x0013795C
		protected void SetCapacity(int Capacity)
		{
			T[] array = new T[Capacity];
			if (this.Amount > 0)
			{
				if (this.Head < this.Tail)
				{
					Array.Copy(this.Buffer, this.Head, array, 0, this.Amount);
				}
				else
				{
					Array.Copy(this.Buffer, this.Head, array, 0, this.Size - this.Head);
					Array.Copy(this.Buffer, 0, array, this.Size - this.Head, this.Tail);
				}
			}
			this.Buffer = array;
			this.Head = 0;
			this.Tail = ((this.Amount == Capacity) ? 0 : this.Amount);
			this.Size = Capacity;
			this.Version++;
		}

		// Token: 0x06003FCB RID: 16331 RVA: 0x0013981D File Offset: 0x00137A1D
		public void EnsureCapacity(int Capacity)
		{
			if (Capacity > this.Size)
			{
				this.SetCapacity(Capacity);
			}
		}

		// Token: 0x06003FCC RID: 16332 RVA: 0x00139830 File Offset: 0x00137A30
		public virtual void Write(SerializationWriter Writer)
		{
			Writer.WriteOptimized(this.Amount);
			for (int i = 0; i < this.Amount; i++)
			{
				Writer.WriteObject(this.Buffer[(this.Head + i) % this.Size]);
			}
		}

		// Token: 0x06003FCD RID: 16333 RVA: 0x00139880 File Offset: 0x00137A80
		public virtual void Read(SerializationReader Reader)
		{
			this.Amount += Reader.ReadOptimizedInt32();
			this.EnsureCapacity(this.Amount);
			for (int i = 0; i < this.Amount; i++)
			{
				T t = (T)((object)Reader.ReadObject());
				this.Buffer[this.Tail] = t;
				this.Tail = (this.Tail + 1) % this.Size;
			}
		}

		// Token: 0x06003FCE RID: 16334 RVA: 0x001398F0 File Offset: 0x00137AF0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public T[] GetArray()
		{
			return this.Buffer;
		}

		// Token: 0x06003FCF RID: 16335 RVA: 0x001398F8 File Offset: 0x00137AF8
		public RingDeque<T>.Enumerator GetEnumerator()
		{
			return new RingDeque<T>.Enumerator(this);
		}

		// Token: 0x06003FD0 RID: 16336 RVA: 0x00139900 File Offset: 0x00137B00
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return new RingDeque<T>.Enumerator(this);
		}

		// Token: 0x06003FD1 RID: 16337 RVA: 0x0013990D File Offset: 0x00137B0D
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new RingDeque<T>.Enumerator(this);
		}

		// Token: 0x04001F48 RID: 8008
		protected T[] Buffer;

		// Token: 0x04001F49 RID: 8009
		protected int Head;

		// Token: 0x04001F4A RID: 8010
		protected int Tail;

		// Token: 0x04001F4B RID: 8011
		protected int Size;

		// Token: 0x04001F4C RID: 8012
		protected int Amount;

		// Token: 0x04001F4D RID: 8013
		protected int Version;

		// Token: 0x020019E3 RID: 6627
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x17000EAE RID: 3758
			// (get) Token: 0x0600E590 RID: 58768 RVA: 0x0052F2F7 File Offset: 0x0052D4F7
			public T Current
			{
				get
				{
					return this.Item;
				}
			}

			// Token: 0x17000EAF RID: 3759
			// (get) Token: 0x0600E591 RID: 58769 RVA: 0x0052F2FF File Offset: 0x0052D4FF
			object IEnumerator.Current
			{
				get
				{
					return this.Item;
				}
			}

			// Token: 0x0600E592 RID: 58770 RVA: 0x0052F30C File Offset: 0x0052D50C
			public Enumerator(RingDeque<T> Queue)
			{
				this.Queue = Queue;
				this.Version = this.Queue.Version;
				this.Index = -1;
				this.Item = default(T);
			}

			// Token: 0x0600E593 RID: 58771 RVA: 0x0052F33C File Offset: 0x0052D53C
			public bool MoveNext()
			{
				if (this.Version != this.Queue.Version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				int num = this.Index + 1;
				this.Index = num;
				if (num >= this.Queue.Amount)
				{
					this.Item = default(T);
					return false;
				}
				this.Item = this.Queue.Buffer[(this.Queue.Head + this.Index) % this.Queue.Size];
				return true;
			}

			// Token: 0x0600E594 RID: 58772 RVA: 0x0052F3C8 File Offset: 0x0052D5C8
			void IEnumerator.Reset()
			{
				this.Index = -1;
				this.Item = default(T);
			}

			// Token: 0x0600E595 RID: 58773 RVA: 0x0052F3DD File Offset: 0x0052D5DD
			public void Dispose()
			{
			}

			// Token: 0x0400678C RID: 26508
			private RingDeque<T> Queue;

			// Token: 0x0400678D RID: 26509
			private int Index;

			// Token: 0x0400678E RID: 26510
			private int Version;

			// Token: 0x0400678F RID: 26511
			private T Item;
		}
	}
}
