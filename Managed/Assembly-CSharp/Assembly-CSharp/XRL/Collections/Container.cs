using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using XRL.World;

namespace XRL.Collections
{
	// Token: 0x020005CB RID: 1483
	[Serializable]
	public abstract class Container<T> : IReadOnlyList<T>, IEnumerable<T>, IEnumerable, IReadOnlyCollection<T>, IComposite
	{
		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06003F19 RID: 16153 RVA: 0x00137354 File Offset: 0x00135554
		protected virtual int DefaultCapacity
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06003F1A RID: 16154 RVA: 0x00137357 File Offset: 0x00135557
		public bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06003F1B RID: 16155 RVA: 0x0013735A File Offset: 0x0013555A
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x06003F1C RID: 16156 RVA: 0x0013735D File Offset: 0x0013555D
		public int Capacity
		{
			get
			{
				return this.Size;
			}
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06003F1D RID: 16157 RVA: 0x00137365 File Offset: 0x00135565
		public int Count
		{
			get
			{
				return this.Length;
			}
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x06003F1E RID: 16158 RVA: 0x0013736D File Offset: 0x0013556D
		public int Version
		{
			get
			{
				return this.Variant;
			}
		}

		// Token: 0x1700066E RID: 1646
		T IReadOnlyList<T>.this[int Index]
		{
			get
			{
				if (Index >= this.Size)
				{
					throw new ArgumentOutOfRangeException();
				}
				return this.Items[Index];
			}
		}

		// Token: 0x06003F20 RID: 16160 RVA: 0x00137392 File Offset: 0x00135592
		public void EnsureCapacity(int Capacity)
		{
			if (this.Size < Capacity)
			{
				this.Resize(Capacity);
			}
		}

		// Token: 0x06003F21 RID: 16161 RVA: 0x001373A4 File Offset: 0x001355A4
		protected void Resize(int Capacity)
		{
			if (Capacity == 0)
			{
				Capacity = this.DefaultCapacity;
			}
			T[] array = new T[Capacity];
			Array.Copy(this.Items, 0, array, 0, this.Length);
			this.Items = array;
			this.Size = Capacity;
		}

		// Token: 0x06003F22 RID: 16162 RVA: 0x001373E5 File Offset: 0x001355E5
		public bool Contains(T Item)
		{
			return Array.IndexOf<T>(this.Items, Item, 0, this.Length) >= 0;
		}

		// Token: 0x06003F23 RID: 16163 RVA: 0x00137400 File Offset: 0x00135600
		public void CopyTo(T[] Buffer, int Index)
		{
			Array.Copy(this.Items, 0, Buffer, Index, this.Length);
		}

		// Token: 0x06003F24 RID: 16164 RVA: 0x00137416 File Offset: 0x00135616
		public int IndexOf(T Item)
		{
			return Array.IndexOf<T>(this.Items, Item, 0, this.Length);
		}

		// Token: 0x06003F25 RID: 16165 RVA: 0x0013742C File Offset: 0x0013562C
		public virtual void Write(SerializationWriter Writer)
		{
			Writer.WriteOptimized(this.Length);
			for (int i = 0; i < this.Length; i++)
			{
				Writer.WriteObject(this.Items[i]);
			}
		}

		// Token: 0x06003F26 RID: 16166 RVA: 0x00137470 File Offset: 0x00135670
		public virtual void Read(SerializationReader Reader)
		{
			this.Size = (this.Length = Reader.ReadOptimizedInt32());
			this.Items = new T[this.Size];
			for (int i = 0; i < this.Length; i++)
			{
				this.Items[i] = (T)((object)Reader.ReadObject());
			}
		}

		// Token: 0x06003F27 RID: 16167 RVA: 0x001374CC File Offset: 0x001356CC
		public T[] ToArray()
		{
			T[] array = new T[this.Length];
			Array.Copy(this.Items, 0, array, 0, this.Length);
			return array;
		}

		// Token: 0x06003F28 RID: 16168 RVA: 0x001374FA File Offset: 0x001356FA
		public static implicit operator ReadOnlySpan<T>(Container<T> Container)
		{
			return Container.AsSpan();
		}

		// Token: 0x06003F29 RID: 16169 RVA: 0x00137502 File Offset: 0x00135702
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ReadOnlySpan<T> AsSpan()
		{
			return new ReadOnlySpan<T>(this.Items, 0, this.Length);
		}

		// Token: 0x06003F2A RID: 16170 RVA: 0x00137516 File Offset: 0x00135716
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ReadOnlySpan<T> AsSpan(int Start)
		{
			if (Start > this.Length)
			{
				throw new ArgumentOutOfRangeException("Start");
			}
			return new ReadOnlySpan<T>(this.Items, Start, this.Length - Start);
		}

		// Token: 0x06003F2B RID: 16171 RVA: 0x00137540 File Offset: 0x00135740
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ReadOnlySpan<T> AsSpan(int Start, int Length)
		{
			if (Start + Length > this.Length)
			{
				throw new ArgumentOutOfRangeException("Length");
			}
			return new ReadOnlySpan<T>(this.Items, Start, Length);
		}

		// Token: 0x06003F2C RID: 16172 RVA: 0x00137565 File Offset: 0x00135765
		public Container<T>.Enumerator GetEnumerator()
		{
			return new Container<T>.Enumerator(this);
		}

		// Token: 0x06003F2D RID: 16173 RVA: 0x0013756D File Offset: 0x0013576D
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return new Container<T>.Enumerator(this);
		}

		// Token: 0x06003F2E RID: 16174 RVA: 0x0013757A File Offset: 0x0013577A
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new Container<T>.Enumerator(this);
		}

		// Token: 0x04001F2E RID: 7982
		protected T[] Items = Array.Empty<T>();

		// Token: 0x04001F2F RID: 7983
		protected int Size;

		// Token: 0x04001F30 RID: 7984
		protected int Length;

		// Token: 0x04001F31 RID: 7985
		protected int Variant;

		// Token: 0x020019DD RID: 6621
		[Serializable]
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x17000EA8 RID: 3752
			// (get) Token: 0x0600E56B RID: 58731 RVA: 0x0052E767 File Offset: 0x0052C967
			public T Current
			{
				get
				{
					return this.Item;
				}
			}

			// Token: 0x17000EA9 RID: 3753
			// (get) Token: 0x0600E56C RID: 58732 RVA: 0x0052E76F File Offset: 0x0052C96F
			object IEnumerator.Current
			{
				get
				{
					return this.Item;
				}
			}

			// Token: 0x0600E56D RID: 58733 RVA: 0x0052E77C File Offset: 0x0052C97C
			public Enumerator(Container<T> List)
			{
				this.List = List;
				this.Index = 0;
				this.Variant = List.Variant;
				this.Item = default(T);
			}

			// Token: 0x0600E56E RID: 58734 RVA: 0x0052E7A4 File Offset: 0x0052C9A4
			public bool MoveNext()
			{
				if (this.List.Variant != this.Variant)
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

			// Token: 0x0600E56F RID: 58735 RVA: 0x0052E813 File Offset: 0x0052CA13
			public void Reset()
			{
				if (this.List.Variant != this.Variant)
				{
					throw new InvalidOperationException();
				}
				this.Index = 0;
				this.Item = default(T);
			}

			// Token: 0x0600E570 RID: 58736 RVA: 0x0052E841 File Offset: 0x0052CA41
			public void Dispose()
			{
			}

			// Token: 0x04006779 RID: 26489
			private Container<T> List;

			// Token: 0x0400677A RID: 26490
			private int Index;

			// Token: 0x0400677B RID: 26491
			private int Variant;

			// Token: 0x0400677C RID: 26492
			private T Item;
		}
	}
}
