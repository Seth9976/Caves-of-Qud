using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000112 RID: 274
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
	internal struct BitVector : IEquatable<BitVector>
	{
		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000988 RID: 2440 RVA: 0x0001B3E9 File Offset: 0x000195E9
		private static ulong[] s_emptyArray
		{
			get
			{
				return Array.Empty<ulong>();
			}
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x0001B3F0 File Offset: 0x000195F0
		private BitVector(ulong bits0, ulong[] bits, int capacity)
		{
			BitVector.WordsForCapacity(capacity);
			this._bits0 = bits0;
			this._bits = bits;
			this._capacity = capacity;
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x0001B40E File Offset: 0x0001960E
		public bool Equals(BitVector other)
		{
			return this._capacity == other._capacity && this._bits0 == other._bits0 && MemoryExtensions.SequenceEqual<ulong>(MemoryExtensions.AsSpan<ulong>(this._bits), MemoryExtensions.AsSpan<ulong>(other._bits));
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x0001B450 File Offset: 0x00019650
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			if (obj is BitVector)
			{
				BitVector bitVector = (BitVector)obj;
				return this.Equals(bitVector);
			}
			return false;
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x0001B475 File Offset: 0x00019675
		public static bool operator ==(BitVector left, BitVector right)
		{
			return left.Equals(right);
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x0001B47F File Offset: 0x0001967F
		public static bool operator !=(BitVector left, BitVector right)
		{
			return !left.Equals(right);
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x0001B48C File Offset: 0x0001968C
		public override int GetHashCode()
		{
			int num = this._bits0.GetHashCode();
			if (this._bits != null)
			{
				for (int i = 0; i < this._bits.Length; i++)
				{
					num = Hash.Combine(this._bits[i].GetHashCode(), num);
				}
			}
			return Hash.Combine(this._capacity, num);
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x0001B4E4 File Offset: 0x000196E4
		private static int WordsForCapacity(int capacity)
		{
			if (capacity <= 0)
			{
				return 0;
			}
			return capacity - 1 >> 6;
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000990 RID: 2448 RVA: 0x0001B4F1 File Offset: 0x000196F1
		public int Capacity
		{
			get
			{
				return this._capacity;
			}
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x0001B4F9 File Offset: 0x000196F9
		[Conditional("DEBUG_BITARRAY")]
		private void Check()
		{
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x0001B4FC File Offset: 0x000196FC
		public void EnsureCapacity(int newCapacity)
		{
			if (newCapacity > this._capacity)
			{
				int num = BitVector.WordsForCapacity(newCapacity);
				if (num > this._bits.Length)
				{
					Array.Resize<ulong>(ref this._bits, num);
				}
				this._capacity = newCapacity;
			}
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x0001B537 File Offset: 0x00019737
		public IEnumerable<ulong> Words()
		{
			if (this._capacity > 0)
			{
				yield return this._bits0;
			}
			int i = 0;
			ulong[] bits = this._bits;
			int j = ((bits != null) ? bits.Length : 0);
			while (i < j)
			{
				yield return this._bits[i];
				int num = i;
				i = num + 1;
			}
			yield break;
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x0001B54C File Offset: 0x0001974C
		public IEnumerable<int> TrueBits()
		{
			int num2;
			if (this._bits0 != 0UL)
			{
				for (int bit = 0; bit < 64; bit = num2 + 1)
				{
					ulong num = 1UL << bit;
					if ((this._bits0 & num) != 0UL)
					{
						if (bit >= this._capacity)
						{
							yield break;
						}
						yield return bit;
					}
					num2 = bit;
				}
			}
			for (int bit = 0; bit < this._bits.Length; bit = num2 + 1)
			{
				ulong w = this._bits[bit];
				if (w != 0UL)
				{
					for (int b = 0; b < 64; b = num2 + 1)
					{
						ulong num3 = 1UL << b;
						if ((w & num3) != 0UL)
						{
							int num4 = (bit + 1 << 6) | b;
							if (num4 >= this._capacity)
							{
								yield break;
							}
							yield return num4;
						}
						num2 = b;
					}
				}
				num2 = bit;
			}
			yield break;
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x0001B564 File Offset: 0x00019764
		public static BitVector Create(int capacity)
		{
			int num = BitVector.WordsForCapacity(capacity);
			ulong[] array = ((num == 0) ? BitVector.s_emptyArray : new ulong[num]);
			return new BitVector(0UL, array, capacity);
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x0001B594 File Offset: 0x00019794
		public static BitVector AllSet(int capacity)
		{
			if (capacity == 0)
			{
				return BitVector.Empty;
			}
			int num = BitVector.WordsForCapacity(capacity);
			ulong[] array = ((num == 0) ? BitVector.s_emptyArray : new ulong[num]);
			int num2 = num - 1;
			ulong num3 = ulong.MaxValue;
			for (int i = 0; i < num2; i++)
			{
				array[i] = ulong.MaxValue;
			}
			int num4 = capacity & 63;
			if (num4 > 0)
			{
				ulong num5 = ~(ulong.MaxValue << num4);
				if (num2 < 0)
				{
					num3 = num5;
				}
				else
				{
					array[num2] = num5;
				}
			}
			else if (num > 0)
			{
				array[num2] = ulong.MaxValue;
			}
			return new BitVector(num3, array, capacity);
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x0001B618 File Offset: 0x00019818
		public BitVector Clone()
		{
			ulong[] array;
			if (this._bits == null || this._bits.Length == 0)
			{
				array = BitVector.s_emptyArray;
			}
			else
			{
				array = (ulong[])this._bits.Clone();
			}
			return new BitVector(this._bits0, array, this._capacity);
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x0001B664 File Offset: 0x00019864
		public void Invert()
		{
			this._bits0 = ~this._bits0;
			if (this._bits != null)
			{
				for (int i = 0; i < this._bits.Length; i++)
				{
					this._bits[i] = ~this._bits[i];
				}
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000999 RID: 2457 RVA: 0x0001B6AA File Offset: 0x000198AA
		public bool IsNull
		{
			get
			{
				return this._bits == null;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x0600099A RID: 2458 RVA: 0x0001B6B5 File Offset: 0x000198B5
		public static BitVector Null
		{
			get
			{
				return BitVector.s_nullValue;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x0600099B RID: 2459 RVA: 0x0001B6BC File Offset: 0x000198BC
		public static BitVector Empty
		{
			get
			{
				return BitVector.s_emptyValue;
			}
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x0001B6C4 File Offset: 0x000198C4
		public bool IntersectWith(in BitVector other)
		{
			bool flag = false;
			int num = other._bits.Length;
			ulong[] bits = this._bits;
			int num2 = bits.Length;
			if (num > num2)
			{
				num = num2;
			}
			ulong bits2 = this._bits0;
			ulong num3 = bits2 & other._bits0;
			if (num3 != bits2)
			{
				this._bits0 = num3;
				flag = true;
			}
			for (int i = 0; i < num; i++)
			{
				ulong num4 = bits[i];
				ulong num5 = num4 & other._bits[i];
				if (num5 != num4)
				{
					bits[i] = num5;
					flag = true;
				}
			}
			for (int j = num; j < num2; j++)
			{
				if (bits[j] != 0UL)
				{
					bits[j] = 0UL;
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x0001B764 File Offset: 0x00019964
		public bool UnionWith(in BitVector other)
		{
			bool flag = false;
			if (other._capacity > this._capacity)
			{
				this.EnsureCapacity(other._capacity);
			}
			ulong num = this._bits0;
			this._bits0 |= other._bits0;
			if (num != this._bits0)
			{
				flag = true;
			}
			for (int i = 0; i < other._bits.Length; i++)
			{
				num = this._bits[i];
				this._bits[i] |= other._bits[i];
				if (this._bits[i] != num)
				{
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x170001FB RID: 507
		public bool this[int index]
		{
			get
			{
				if (index < 0)
				{
					throw new IndexOutOfRangeException();
				}
				if (index >= this._capacity)
				{
					return false;
				}
				int num = (index >> 6) - 1;
				return BitVector.IsTrue((num < 0) ? this._bits0 : this._bits[num], index);
			}
			set
			{
				if (index < 0)
				{
					throw new IndexOutOfRangeException();
				}
				if (index >= this._capacity)
				{
					this.EnsureCapacity(index + 1);
				}
				int num = (index >> 6) - 1;
				int num2 = index & 63;
				ulong num3 = 1UL << num2;
				if (num < 0)
				{
					if (value)
					{
						this._bits0 |= num3;
						return;
					}
					this._bits0 &= ~num3;
					return;
				}
				else
				{
					if (value)
					{
						this._bits[num] |= num3;
						return;
					}
					this._bits[num] &= ~num3;
					return;
				}
			}
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x0001B8C1 File Offset: 0x00019AC1
		public void Clear()
		{
			this._bits0 = 0UL;
			if (this._bits != null)
			{
				Array.Clear(this._bits, 0, this._bits.Length);
			}
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x0001B8E8 File Offset: 0x00019AE8
		public static bool IsTrue(ulong word, int index)
		{
			int num = index & 63;
			ulong num2 = 1UL << num;
			return (word & num2) > 0UL;
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x0001B909 File Offset: 0x00019B09
		public static int WordsRequired(int capacity)
		{
			if (capacity <= 0)
			{
				return 0;
			}
			return BitVector.WordsForCapacity(capacity) + 1;
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x0001B91C File Offset: 0x00019B1C
		internal string GetDebuggerDisplay()
		{
			char[] array = new char[this._capacity];
			for (int i = 0; i < this._capacity; i++)
			{
				array[this._capacity - i - 1] = (this[i] ? '1' : '0');
			}
			return new string(array);
		}

		// Token: 0x040002A6 RID: 678
		private const ulong ZeroWord = 0UL;

		// Token: 0x040002A7 RID: 679
		private const int Log2BitsPerWord = 6;

		// Token: 0x040002A8 RID: 680
		public const int BitsPerWord = 64;

		// Token: 0x040002A9 RID: 681
		private static readonly BitVector s_nullValue = default(BitVector);

		// Token: 0x040002AA RID: 682
		private static readonly BitVector s_emptyValue = new BitVector(0UL, BitVector.s_emptyArray, 0);

		// Token: 0x040002AB RID: 683
		private ulong _bits0;

		// Token: 0x040002AC RID: 684
		private ulong[] _bits;

		// Token: 0x040002AD RID: 685
		private int _capacity;
	}
}
