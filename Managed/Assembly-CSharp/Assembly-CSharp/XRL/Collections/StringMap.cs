using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Genkit;
using XRL.World;

namespace XRL.Collections
{
	/// <summary>String-keyed dictionary that also accepts <see cref="T:System.ReadOnlySpan`1" /> of type <see cref="T:System.Char" /> for keys.</summary>
	/// <remarks>Stable key hashing with <see cref="M:Genkit.Hash.FNV1A64(System.String,System.UInt64)" />.</remarks>
	// Token: 0x020005D3 RID: 1491
	[Serializable]
	public class StringMap<T> : IDictionary<string, T>, ICollection<KeyValuePair<string, T>>, IEnumerable<KeyValuePair<string, T>>, IEnumerable, IReadOnlyDictionary<string, T>, IReadOnlyCollection<KeyValuePair<string, T>>, IComposite, IDictionary, ICollection
	{
		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06003FDA RID: 16346 RVA: 0x001399C1 File Offset: 0x00137BC1
		public StringMap<T>.ValueEnumerator Values
		{
			get
			{
				return new StringMap<T>.ValueEnumerator(this);
			}
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06003FDB RID: 16347 RVA: 0x001399C9 File Offset: 0x00137BC9
		public int Capacity
		{
			get
			{
				return this.Size;
			}
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06003FDC RID: 16348 RVA: 0x001399D1 File Offset: 0x00137BD1
		public int Count
		{
			get
			{
				return this.Amount;
			}
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06003FDD RID: 16349 RVA: 0x001399D9 File Offset: 0x00137BD9
		public virtual bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06003FDE RID: 16350 RVA: 0x001399DC File Offset: 0x00137BDC
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06003FDF RID: 16351 RVA: 0x001399DF File Offset: 0x00137BDF
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06003FE0 RID: 16352 RVA: 0x001399E2 File Offset: 0x00137BE2
		bool IDictionary.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06003FE1 RID: 16353 RVA: 0x001399E5 File Offset: 0x00137BE5
		bool IDictionary.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06003FE2 RID: 16354 RVA: 0x001399E8 File Offset: 0x00137BE8
		bool ICollection<KeyValuePair<string, T>>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x06003FE3 RID: 16355 RVA: 0x001399EB File Offset: 0x00137BEB
		ICollection IDictionary.Keys
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x06003FE4 RID: 16356 RVA: 0x001399F2 File Offset: 0x00137BF2
		ICollection<string> IDictionary<string, T>.Keys
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06003FE5 RID: 16357 RVA: 0x001399F9 File Offset: 0x00137BF9
		IEnumerable<string> IReadOnlyDictionary<string, T>.Keys
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06003FE6 RID: 16358 RVA: 0x00139A00 File Offset: 0x00137C00
		ICollection IDictionary.Values
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06003FE7 RID: 16359 RVA: 0x00139A07 File Offset: 0x00137C07
		ICollection<T> IDictionary<string, T>.Values
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06003FE8 RID: 16360 RVA: 0x00139A0E File Offset: 0x00137C0E
		IEnumerable<T> IReadOnlyDictionary<string, T>.Values
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700069A RID: 1690
		public T this[[AllowNull] string Key]
		{
			get
			{
				int num = this.IndexOf(Key);
				if (num < 0)
				{
					return default(T);
				}
				return this.Slots[num].Value;
			}
			set
			{
				this.InsertInternal(Key, value, false, false);
			}
		}

		// Token: 0x1700069B RID: 1691
		public T this[ReadOnlySpan<char> Key]
		{
			get
			{
				int num = this.IndexOf(Key);
				if (num < 0)
				{
					return default(T);
				}
				return this.Slots[num].Value;
			}
			set
			{
				this.InsertInternal(Key, value, false, false);
			}
		}

		// Token: 0x1700069C RID: 1692
		object IDictionary.this[object Key]
		{
			get
			{
				int num = this.IndexOf((string)Key);
				return (num >= 0) ? this.Slots[num].Value : default(T);
			}
			set
			{
				this.InsertInternal((string)Key, (T)((object)value), false, false);
			}
		}

		// Token: 0x1700069D RID: 1693
		public T this[int Index]
		{
			get
			{
				return this.Slots[Index].Value;
			}
		}

		// Token: 0x06003FF0 RID: 16368 RVA: 0x00139B09 File Offset: 0x00137D09
		public StringMap()
		{
			this._Seed = 14695981039346656037UL;
		}

		// Token: 0x06003FF1 RID: 16369 RVA: 0x00139B3D File Offset: 0x00137D3D
		public StringMap(int Capacity = 0, ulong Seed = 14695981039346656037UL)
		{
			this._Seed = Seed;
			if (Capacity > 0)
			{
				this.InitCapacity(Capacity);
			}
		}

		// Token: 0x06003FF2 RID: 16370 RVA: 0x00139B74 File Offset: 0x00137D74
		public StringMap([DisallowNull] IDictionary<string, T> Dictionary, ulong Seed = 14695981039346656037UL)
			: this(Dictionary.Count, Seed)
		{
			foreach (KeyValuePair<string, T> keyValuePair in Dictionary)
			{
				this.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x06003FF3 RID: 16371 RVA: 0x00139BD8 File Offset: 0x00137DD8
		protected void InitCapacity(int Capacity)
		{
			Capacity = Hash.GetCapacity(Capacity);
			this.Buckets = new int[Capacity];
			this.Slots = new StringMap<T>.Slot[Capacity];
		}

		// Token: 0x06003FF4 RID: 16372 RVA: 0x00139BFA File Offset: 0x00137DFA
		public void Add([AllowNull] string Key, T Value)
		{
			this.InsertInternal(Key, Value, false, true);
		}

		// Token: 0x06003FF5 RID: 16373 RVA: 0x00139C07 File Offset: 0x00137E07
		public void Add(ReadOnlySpan<char> Key, T Value)
		{
			this.InsertInternal(Key, Value, false, true);
		}

		// Token: 0x06003FF6 RID: 16374 RVA: 0x00139C14 File Offset: 0x00137E14
		public bool TryAdd([AllowNull] string Key, T Value)
		{
			return this.InsertInternal(Key, Value, true, false);
		}

		// Token: 0x06003FF7 RID: 16375 RVA: 0x00139C20 File Offset: 0x00137E20
		public bool TryAdd(ReadOnlySpan<char> Key, T Value)
		{
			return this.InsertInternal(Key, Value, true, false);
		}

		// Token: 0x06003FF8 RID: 16376 RVA: 0x00139C2C File Offset: 0x00137E2C
		public virtual void Clear()
		{
			if (this.Length > 0)
			{
				Array.Clear(this.Buckets, 0, this.Size);
				Array.Clear(this.Slots, 0, this.Length);
				this.Next = -1;
				this.Length = 0;
				this.Amount = 0;
				this.Version++;
			}
		}

		// Token: 0x06003FF9 RID: 16377 RVA: 0x00139C8C File Offset: 0x00137E8C
		public void ClearValues()
		{
			for (int i = 0; i < this.Length; i++)
			{
				this.Slots[i].Value = default(T);
			}
		}

		// Token: 0x06003FFA RID: 16378 RVA: 0x00139CC1 File Offset: 0x00137EC1
		public bool ContainsKey([AllowNull] string Key)
		{
			return this.IndexOf(Key) >= 0;
		}

		// Token: 0x06003FFB RID: 16379 RVA: 0x00139CD0 File Offset: 0x00137ED0
		public bool ContainsKey(ReadOnlySpan<char> Key)
		{
			return this.IndexOf(Key) >= 0;
		}

		// Token: 0x06003FFC RID: 16380 RVA: 0x00139CE0 File Offset: 0x00137EE0
		public bool ContainsValue([AllowNull] T Value)
		{
			if (Value == null)
			{
				for (int i = 0; i < this.Length; i++)
				{
					if (this.Slots[i].Key != null && this.Slots[i].Value == null)
					{
						return true;
					}
				}
			}
			else
			{
				EqualityComparer<T> @default = EqualityComparer<T>.Default;
				for (int j = 0; j < this.Length; j++)
				{
					if (this.Slots[j].Key != null && @default.Equals(this.Slots[j].Value, Value))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06003FFD RID: 16381 RVA: 0x00139D7C File Offset: 0x00137F7C
		protected void CopyTo(KeyValuePair<string, T>[] Array, int Index)
		{
			for (int i = 0; i < this.Length; i++)
			{
				if (this.Slots[i].Key != null)
				{
					Array[Index++] = new KeyValuePair<string, T>(this.Slots[i].Key, this.Slots[i].Value);
				}
			}
		}

		// Token: 0x06003FFE RID: 16382 RVA: 0x00139DE0 File Offset: 0x00137FE0
		protected int IndexOf([AllowNull] string Key)
		{
			if (Key != null && this.Size > 0)
			{
				ulong num = Hash.FNV1A64(Key, this._Seed);
				for (int i = this.Buckets[(int)(checked((IntPtr)(num % (ulong)(unchecked((long)this.Size)))))] - 1; i >= 0; i = this.Slots[i].Next)
				{
					if (this.Slots[i].Hash == num && (!this.Slots[i].Strict || Key.Equals(this.Slots[i].Key, StringComparison.Ordinal)))
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x06003FFF RID: 16383 RVA: 0x00139E7C File Offset: 0x0013807C
		protected unsafe int IndexOf([AllowNull] StringBuilder Text, int Start, int Length)
		{
			if (Text != null && this.Size > 0)
			{
				Span<char> span;
				checked
				{
					span = new Span<char>(stackalloc byte[unchecked((UIntPtr)Length) * 2], Length);
				}
				for (int i = 0; i < Length; i++)
				{
					*span[i] = Text[i + Start];
				}
				return this.IndexOf(span);
			}
			return -1;
		}

		// Token: 0x06004000 RID: 16384 RVA: 0x00139ED0 File Offset: 0x001380D0
		protected int IndexOf(ReadOnlySpan<char> Key)
		{
			if (this.Size > 0)
			{
				ulong num = Hash.FNV1A64(Key, this._Seed);
				for (int i = this.Buckets[(int)(checked((IntPtr)(num % (ulong)(unchecked((long)this.Size)))))] - 1; i >= 0; i = this.Slots[i].Next)
				{
					if (this.Slots[i].Hash == num && (!this.Slots[i].Strict || Key.Equals(this.Slots[i].Key, StringComparison.Ordinal)))
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x06004001 RID: 16385 RVA: 0x00139F6C File Offset: 0x0013816C
		protected bool InsertInternal(string Key, T Value, bool ReturnOnDuplicate = false, bool ThrowOnDuplicate = false)
		{
			if (Key == null)
			{
				return false;
			}
			ulong num = Hash.FNV1A64(Key, this._Seed);
			ulong num2 = 0UL;
			bool flag = false;
			int i;
			if (this.Size > 0)
			{
				num2 = num % (ulong)((long)this.Size);
				for (i = this.Buckets[(int)(checked((IntPtr)num2))] - 1; i >= 0; i = this.Slots[i].Next)
				{
					if (this.Slots[i].Hash == num)
					{
						if (Key.Equals(this.Slots[i].Key, StringComparison.Ordinal))
						{
							if (ThrowOnDuplicate)
							{
								throw new ArgumentException("Element by key '" + Key + "' already exists.");
							}
							if (ReturnOnDuplicate)
							{
								return true;
							}
							this.Slots[i].Value = Value;
							this.Version++;
							return true;
						}
						else
						{
							flag = (this.Slots[i].Strict = true);
						}
					}
				}
			}
			if (this.Next >= 0)
			{
				this.Next = this.Slots[i = this.Next].Next;
			}
			else
			{
				if (this.Length == this.Size)
				{
					this.Resize(this.Length * 2);
					num2 = num % (ulong)((long)this.Size);
				}
				int length = this.Length;
				this.Length = length + 1;
				i = length;
			}
			this.Slots[i].Hash = num;
			this.Slots[i].Next = this.Buckets[(int)(checked((IntPtr)num2))] - 1;
			this.Slots[i].Key = Key;
			this.Slots[i].Value = Value;
			this.Slots[i].Strict = flag;
			this.Buckets[(int)(checked((IntPtr)num2))] = i + 1;
			this.Version++;
			this.Amount++;
			return true;
		}

		// Token: 0x06004002 RID: 16386 RVA: 0x0013A148 File Offset: 0x00138348
		protected bool InsertInternal(ReadOnlySpan<char> Key, T Value, bool ReturnOnDuplicate = false, bool ThrowOnDuplicate = false)
		{
			ulong num = Hash.FNV1A64(Key, this._Seed);
			ulong num2 = 0UL;
			bool flag = false;
			int i;
			if (this.Size > 0)
			{
				num2 = num % (ulong)((long)this.Size);
				for (i = this.Buckets[(int)(checked((IntPtr)num2))] - 1; i >= 0; i = this.Slots[i].Next)
				{
					if (this.Slots[i].Hash == num)
					{
						if (Key.Equals(this.Slots[i].Key, StringComparison.Ordinal))
						{
							if (ThrowOnDuplicate)
							{
								throw new ArgumentException("Element by key '" + this.Slots[i].Key + "' already exists.");
							}
							if (ReturnOnDuplicate)
							{
								return true;
							}
							this.Slots[i].Value = Value;
							this.Version++;
							return true;
						}
						else
						{
							flag = (this.Slots[i].Strict = true);
						}
					}
				}
			}
			if (this.Next >= 0)
			{
				this.Next = this.Slots[i = this.Next].Next;
			}
			else
			{
				if (this.Length == this.Size)
				{
					this.Resize(this.Length * 2);
					num2 = num % (ulong)((long)this.Size);
				}
				int length = this.Length;
				this.Length = length + 1;
				i = length;
			}
			this.Slots[i].Hash = num;
			this.Slots[i].Next = this.Buckets[(int)(checked((IntPtr)num2))] - 1;
			this.Slots[i].Key = new string(Key);
			this.Slots[i].Value = Value;
			this.Slots[i].Strict = flag;
			this.Buckets[(int)(checked((IntPtr)num2))] = i + 1;
			this.Version++;
			this.Amount++;
			return true;
		}

		// Token: 0x06004003 RID: 16387 RVA: 0x0013A33C File Offset: 0x0013853C
		protected void Resize(int Capacity)
		{
			Capacity = Hash.GetCapacity(Capacity);
			if (Capacity == this.Size)
			{
				return;
			}
			int[] array = new int[Capacity];
			StringMap<T>.Slot[] array2 = new StringMap<T>.Slot[Capacity];
			Array.Copy(this.Slots, 0, array2, 0, this.Size);
			for (int i = 0; i < this.Size; i++)
			{
				if (this.Slots[i].Key != null)
				{
					ulong num = array2[i].Hash % (ulong)((long)Capacity);
					array2[i].Next = array[(int)(checked((IntPtr)num))] - 1;
					array[(int)(checked((IntPtr)num))] = i + 1;
				}
			}
			this.Buckets = array;
			this.Slots = array2;
			this.Size = Capacity;
		}

		// Token: 0x06004004 RID: 16388 RVA: 0x0013A3DF File Offset: 0x001385DF
		public void EnsureCapacity(int Capacity)
		{
			if (this.Size < Capacity)
			{
				this.Resize(Capacity);
			}
		}

		// Token: 0x06004005 RID: 16389 RVA: 0x0013A3F4 File Offset: 0x001385F4
		public bool Remove([AllowNull] string Key)
		{
			if (Key == null || this.Size == 0)
			{
				return false;
			}
			ulong num = Hash.FNV1A64(Key, this._Seed);
			ulong num2 = num % (ulong)((long)this.Size);
			int num3 = 0;
			int i = this.Buckets[(int)(checked((IntPtr)num2))] - 1;
			int num4 = -1;
			while (i >= 0)
			{
				if (this.Slots[i].Hash == num)
				{
					if (this.Slots[i].Strict)
					{
						num3++;
						if (!Key.Equals(this.Slots[i].Key, StringComparison.Ordinal))
						{
							goto IL_016F;
						}
					}
					if (num4 < 0)
					{
						this.Buckets[(int)(checked((IntPtr)num2))] = this.Slots[i].Next + 1;
					}
					else
					{
						this.Slots[num4].Next = this.Slots[i].Next;
					}
					this.Slots[i].Hash = 0UL;
					this.Slots[i].Next = this.Next;
					this.Slots[i].Key = null;
					this.Slots[i].Value = default(T);
					this.Slots[i].Strict = true;
					this.Next = i;
					this.Amount--;
					this.Version++;
					if (num3 > 0 && num3 <= 2)
					{
						this.ValidateCollisions(this.Buckets[(int)(checked((IntPtr)num2))] - 1, num);
					}
					return true;
				}
				IL_016F:
				num4 = i;
				i = this.Slots[i].Next;
			}
			return false;
		}

		// Token: 0x06004006 RID: 16390 RVA: 0x0013A590 File Offset: 0x00138790
		public bool Remove(ReadOnlySpan<char> Key)
		{
			if (this.Size == 0)
			{
				return false;
			}
			ulong num = Hash.FNV1A64(Key, this._Seed);
			ulong num2 = num % (ulong)((long)this.Size);
			int num3 = 0;
			int i = this.Buckets[(int)(checked((IntPtr)num2))] - 1;
			int num4 = -1;
			while (i >= 0)
			{
				if (this.Slots[i].Hash == num)
				{
					if (this.Slots[i].Strict)
					{
						num3++;
						if (!Key.Equals(this.Slots[i].Key, StringComparison.Ordinal))
						{
							goto IL_0171;
						}
					}
					if (num4 < 0)
					{
						this.Buckets[(int)(checked((IntPtr)num2))] = this.Slots[i].Next + 1;
					}
					else
					{
						this.Slots[num4].Next = this.Slots[i].Next;
					}
					this.Slots[i].Hash = 0UL;
					this.Slots[i].Next = this.Next;
					this.Slots[i].Key = null;
					this.Slots[i].Value = default(T);
					this.Slots[i].Strict = true;
					this.Next = i;
					this.Amount--;
					this.Version++;
					if (num3 > 0 && num3 <= 2)
					{
						this.ValidateCollisions(this.Buckets[(int)(checked((IntPtr)num2))] - 1, num);
					}
					return true;
				}
				IL_0171:
				num4 = i;
				i = this.Slots[i].Next;
			}
			return false;
		}

		// Token: 0x06004007 RID: 16391 RVA: 0x0013A72C File Offset: 0x0013892C
		protected void ValidateCollisions(int Slot, ulong Hash)
		{
			int num = -1;
			for (int i = Slot; i >= 0; i = this.Slots[i].Next)
			{
				if (this.Slots[i].Hash == Hash)
				{
					if (num != -1)
					{
						return;
					}
					num = i;
				}
			}
			this.Slots[num].Strict = false;
		}

		// Token: 0x06004008 RID: 16392 RVA: 0x0013A784 File Offset: 0x00138984
		public T GetValue(string Key)
		{
			int num = this.IndexOf(Key);
			if (num < 0)
			{
				return default(T);
			}
			return this.Slots[num].Value;
		}

		// Token: 0x06004009 RID: 16393 RVA: 0x0013A7B8 File Offset: 0x001389B8
		public T GetValue(ReadOnlySpan<char> Key)
		{
			int num = this.IndexOf(Key);
			if (num < 0)
			{
				return default(T);
			}
			return this.Slots[num].Value;
		}

		// Token: 0x0600400A RID: 16394 RVA: 0x0013A7EC File Offset: 0x001389EC
		public T GetValue(ReadOnlySpan<char> Key, T Default)
		{
			int num = this.IndexOf(Key);
			if (num < 0)
			{
				return Default;
			}
			return this.Slots[num].Value;
		}

		// Token: 0x0600400B RID: 16395 RVA: 0x0013A818 File Offset: 0x00138A18
		public bool TryGetValue(string Key, out T Value)
		{
			int num = this.IndexOf(Key);
			if (num >= 0)
			{
				Value = this.Slots[num].Value;
				return true;
			}
			Value = default(T);
			return false;
		}

		// Token: 0x0600400C RID: 16396 RVA: 0x0013A854 File Offset: 0x00138A54
		public bool TryGetValue(ReadOnlySpan<char> Key, out T Value)
		{
			int num = this.IndexOf(Key);
			if (num >= 0)
			{
				Value = this.Slots[num].Value;
				return true;
			}
			Value = default(T);
			return false;
		}

		// Token: 0x0600400D RID: 16397 RVA: 0x0013A890 File Offset: 0x00138A90
		public bool TryGetValue(StringBuilder Builder, int Start, int Length, out T Value)
		{
			int num = this.IndexOf(Builder, Start, Length);
			if (num >= 0)
			{
				Value = this.Slots[num].Value;
				return true;
			}
			Value = default(T);
			return false;
		}

		// Token: 0x0600400E RID: 16398 RVA: 0x0013A8D0 File Offset: 0x00138AD0
		public bool TryGetPair(string Key, out KeyValuePair<string, T> Pair)
		{
			int num = this.IndexOf(Key);
			if (num >= 0)
			{
				Pair = new KeyValuePair<string, T>(this.Slots[num].Key, this.Slots[num].Value);
				return true;
			}
			Pair = default(KeyValuePair<string, T>);
			return false;
		}

		// Token: 0x0600400F RID: 16399 RVA: 0x0013A920 File Offset: 0x00138B20
		public bool TryGetPair(ReadOnlySpan<char> Key, out KeyValuePair<string, T> Pair)
		{
			int num = this.IndexOf(Key);
			if (num >= 0)
			{
				Pair = new KeyValuePair<string, T>(this.Slots[num].Key, this.Slots[num].Value);
				return true;
			}
			Pair = default(KeyValuePair<string, T>);
			return false;
		}

		// Token: 0x06004010 RID: 16400 RVA: 0x0013A970 File Offset: 0x00138B70
		public virtual void Write(SerializationWriter Writer)
		{
			Writer.WriteOptimized(this.Amount);
			for (int i = 0; i < this.Length; i++)
			{
				if (this.Slots[i].Key != null)
				{
					Writer.WriteOptimized(this.Slots[i].Key);
					Writer.WriteObject(this.Slots[i].Value);
				}
			}
		}

		// Token: 0x06004011 RID: 16401 RVA: 0x0013A9E0 File Offset: 0x00138BE0
		public virtual void Read(SerializationReader Reader)
		{
			int num = Reader.ReadOptimizedInt32();
			this.Resize(num);
			for (int i = 0; i < num; i++)
			{
				this.InsertInternal(Reader.ReadOptimizedString(), (T)((object)Reader.ReadObject()), false, false);
			}
		}

		// Token: 0x06004012 RID: 16402 RVA: 0x0013AA21 File Offset: 0x00138C21
		void IDictionary.Add(object Key, object Value)
		{
			this.Add((string)Key, (T)((object)Value));
		}

		// Token: 0x06004013 RID: 16403 RVA: 0x0013AA35 File Offset: 0x00138C35
		void ICollection<KeyValuePair<string, T>>.Add(KeyValuePair<string, T> Pair)
		{
			this.Add(Pair.Key, Pair.Value);
		}

		// Token: 0x06004014 RID: 16404 RVA: 0x0013AA4C File Offset: 0x00138C4C
		bool ICollection<KeyValuePair<string, T>>.Contains(KeyValuePair<string, T> Pair)
		{
			int num = this.IndexOf(Pair.Key);
			return num >= 0 && EqualityComparer<T>.Default.Equals(this.Slots[num].Value, Pair.Value);
		}

		// Token: 0x06004015 RID: 16405 RVA: 0x0013AA94 File Offset: 0x00138C94
		bool ICollection<KeyValuePair<string, T>>.Remove(KeyValuePair<string, T> Pair)
		{
			int num = this.IndexOf(Pair.Key);
			if (num >= 0 && EqualityComparer<T>.Default.Equals(this.Slots[num].Value, Pair.Value))
			{
				this.Remove(Pair.Key);
				return true;
			}
			return false;
		}

		// Token: 0x06004016 RID: 16406 RVA: 0x0013AAE8 File Offset: 0x00138CE8
		void ICollection<KeyValuePair<string, T>>.CopyTo(KeyValuePair<string, T>[] Array, int Index)
		{
			this.CopyTo(Array, Index);
		}

		// Token: 0x06004017 RID: 16407 RVA: 0x0013AAF2 File Offset: 0x00138CF2
		void ICollection.CopyTo(Array Array, int Index)
		{
			this.CopyTo((KeyValuePair<string, T>[])Array, Index);
		}

		// Token: 0x06004018 RID: 16408 RVA: 0x0013AB04 File Offset: 0x00138D04
		bool IDictionary.Contains(object Key)
		{
			string text = Key as string;
			return text != null && this.ContainsKey(text);
		}

		// Token: 0x06004019 RID: 16409 RVA: 0x0013AB24 File Offset: 0x00138D24
		void IDictionary.Remove(object Key)
		{
			string text = Key as string;
			if (text != null)
			{
				this.Remove(text);
			}
		}

		// Token: 0x0600401A RID: 16410 RVA: 0x0013AB43 File Offset: 0x00138D43
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new StringMap<T>.Enumerator(this);
		}

		// Token: 0x0600401B RID: 16411 RVA: 0x0013AB50 File Offset: 0x00138D50
		public StringMap<T>.Enumerator GetEnumerator()
		{
			return new StringMap<T>.Enumerator(this);
		}

		// Token: 0x0600401C RID: 16412 RVA: 0x0013AB58 File Offset: 0x00138D58
		IEnumerator<KeyValuePair<string, T>> IEnumerable<KeyValuePair<string, T>>.GetEnumerator()
		{
			return new StringMap<T>.Enumerator(this);
		}

		// Token: 0x0600401D RID: 16413 RVA: 0x0013AB65 File Offset: 0x00138D65
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new StringMap<T>.Enumerator(this);
		}

		// Token: 0x04001F50 RID: 8016
		protected int[] Buckets = Array.Empty<int>();

		// Token: 0x04001F51 RID: 8017
		protected StringMap<T>.Slot[] Slots = Array.Empty<StringMap<T>.Slot>();

		// Token: 0x04001F52 RID: 8018
		protected int Size;

		// Token: 0x04001F53 RID: 8019
		protected int Length;

		// Token: 0x04001F54 RID: 8020
		protected int Amount;

		// Token: 0x04001F55 RID: 8021
		protected int Next = -1;

		// Token: 0x04001F56 RID: 8022
		protected int Version;

		// Token: 0x04001F57 RID: 8023
		protected ulong _Seed;

		// Token: 0x020019E4 RID: 6628
		protected struct Slot
		{
			// Token: 0x04006790 RID: 26512
			public ulong Hash;

			// Token: 0x04006791 RID: 26513
			public int Next;

			// Token: 0x04006792 RID: 26514
			public string Key;

			// Token: 0x04006793 RID: 26515
			public T Value;

			// Token: 0x04006794 RID: 26516
			public bool Strict;
		}

		// Token: 0x020019E5 RID: 6629
		[Serializable]
		public struct Enumerator : IEnumerator<KeyValuePair<string, T>>, IEnumerator, IDisposable, IDictionaryEnumerator
		{
			// Token: 0x17000EB0 RID: 3760
			// (get) Token: 0x0600E596 RID: 58774 RVA: 0x0052F3DF File Offset: 0x0052D5DF
			public KeyValuePair<string, T> Current
			{
				get
				{
					return new KeyValuePair<string, T>(this.Map.Slots[this.Index].Key, this.Map.Slots[this.Index].Value);
				}
			}

			// Token: 0x17000EB1 RID: 3761
			// (get) Token: 0x0600E597 RID: 58775 RVA: 0x0052F41C File Offset: 0x0052D61C
			object IEnumerator.Current
			{
				get
				{
					return new KeyValuePair<string, T>(this.Map.Slots[this.Index].Key, this.Map.Slots[this.Index].Value);
				}
			}

			// Token: 0x17000EB2 RID: 3762
			// (get) Token: 0x0600E598 RID: 58776 RVA: 0x0052F46C File Offset: 0x0052D66C
			DictionaryEntry IDictionaryEnumerator.Entry
			{
				get
				{
					return new DictionaryEntry(this.Map.Slots[this.Index].Key, this.Map.Slots[this.Index].Value);
				}
			}

			// Token: 0x17000EB3 RID: 3763
			// (get) Token: 0x0600E599 RID: 58777 RVA: 0x0052F4B9 File Offset: 0x0052D6B9
			object IDictionaryEnumerator.Key
			{
				get
				{
					return this.Map.Slots[this.Index].Key;
				}
			}

			// Token: 0x17000EB4 RID: 3764
			// (get) Token: 0x0600E59A RID: 58778 RVA: 0x0052F4D6 File Offset: 0x0052D6D6
			object IDictionaryEnumerator.Value
			{
				get
				{
					return this.Map.Slots[this.Index].Value;
				}
			}

			// Token: 0x0600E59B RID: 58779 RVA: 0x0052F4F8 File Offset: 0x0052D6F8
			public Enumerator(StringMap<T> Map)
			{
				this.Map = Map;
				this.Version = Map.Version;
				this.Index = -1;
			}

			// Token: 0x0600E59C RID: 58780 RVA: 0x0052F514 File Offset: 0x0052D714
			public bool MoveNext()
			{
				if (this.Version != this.Map.Version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				do
				{
					int num = this.Index + 1;
					this.Index = num;
					if (num >= this.Map.Length)
					{
						return false;
					}
				}
				while (this.Map.Slots[this.Index].Key == null);
				return true;
			}

			// Token: 0x0600E59D RID: 58781 RVA: 0x0052F57D File Offset: 0x0052D77D
			public void Dispose()
			{
			}

			// Token: 0x0600E59E RID: 58782 RVA: 0x0052F57F File Offset: 0x0052D77F
			void IEnumerator.Reset()
			{
				if (this.Version != this.Map.Version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				this.Index = -1;
			}

			// Token: 0x04006795 RID: 26517
			private StringMap<T> Map;

			// Token: 0x04006796 RID: 26518
			private int Version;

			// Token: 0x04006797 RID: 26519
			private int Index;
		}

		// Token: 0x020019E6 RID: 6630
		[Serializable]
		public struct ValueEnumerator : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x0600E59F RID: 58783 RVA: 0x0052F5A6 File Offset: 0x0052D7A6
			public ValueEnumerator(StringMap<T> Map)
			{
				this.Map = Map;
				this.Version = Map.Version;
				this.Index = -1;
			}

			// Token: 0x0600E5A0 RID: 58784 RVA: 0x0052F5C2 File Offset: 0x0052D7C2
			public StringMap<T>.ValueEnumerator GetEnumerator()
			{
				return this;
			}

			// Token: 0x0600E5A1 RID: 58785 RVA: 0x0052F5CC File Offset: 0x0052D7CC
			public bool MoveNext()
			{
				if (this.Version != this.Map.Version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				do
				{
					int num = this.Index + 1;
					this.Index = num;
					if (num >= this.Map.Length)
					{
						return false;
					}
				}
				while (this.Map.Slots[this.Index].Key == null);
				return true;
			}

			// Token: 0x17000EB5 RID: 3765
			// (get) Token: 0x0600E5A2 RID: 58786 RVA: 0x0052F635 File Offset: 0x0052D835
			public T Current
			{
				get
				{
					return this.Map.Slots[this.Index].Value;
				}
			}

			// Token: 0x0600E5A3 RID: 58787 RVA: 0x0052F652 File Offset: 0x0052D852
			public void Dispose()
			{
			}

			// Token: 0x17000EB6 RID: 3766
			// (get) Token: 0x0600E5A4 RID: 58788 RVA: 0x0052F654 File Offset: 0x0052D854
			object IEnumerator.Current
			{
				get
				{
					return this.Map.Slots[this.Index].Value;
				}
			}

			// Token: 0x0600E5A5 RID: 58789 RVA: 0x0052F676 File Offset: 0x0052D876
			void IEnumerator.Reset()
			{
				if (this.Version != this.Map.Version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				this.Index = -1;
			}

			// Token: 0x04006798 RID: 26520
			private StringMap<T> Map;

			// Token: 0x04006799 RID: 26521
			private int Version;

			// Token: 0x0400679A RID: 26522
			private int Index;
		}
	}
}
