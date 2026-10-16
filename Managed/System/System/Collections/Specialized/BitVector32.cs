using System;
using System.Text;

namespace System.Collections.Specialized
{
	/// <summary>Provides a simple structure that stores Boolean values and small integers in 32 bits of memory.</summary>
	// Token: 0x020004A6 RID: 1190
	public struct BitVector32
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.BitVector32" /> structure containing the data represented in an integer.</summary>
		/// <param name="data">An integer representing the data of the new <see cref="T:System.Collections.Specialized.BitVector32" />. </param>
		// Token: 0x06002619 RID: 9753 RVA: 0x000857FA File Offset: 0x000839FA
		public BitVector32(int data)
		{
			this._data = (uint)data;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.BitVector32" /> structure containing the data represented in an existing <see cref="T:System.Collections.Specialized.BitVector32" /> structure.</summary>
		/// <param name="value">A <see cref="T:System.Collections.Specialized.BitVector32" /> structure that contains the data to copy. </param>
		// Token: 0x0600261A RID: 9754 RVA: 0x00085803 File Offset: 0x00083A03
		public BitVector32(BitVector32 value)
		{
			this._data = value._data;
		}

		/// <summary>Gets or sets the state of the bit flag indicated by the specified mask.</summary>
		/// <returns>true if the specified bit flag is on (1); otherwise, false.</returns>
		/// <param name="bit">A mask that indicates the bit to get or set. </param>
		// Token: 0x170007A7 RID: 1959
		public bool this[int bit]
		{
			get
			{
				return ((ulong)this._data & (ulong)((long)bit)) == (ulong)bit;
			}
			set
			{
				if (value)
				{
					this._data |= (uint)bit;
					return;
				}
				this._data &= (uint)(~(uint)bit);
			}
		}

		/// <summary>Gets or sets the value stored in the specified <see cref="T:System.Collections.Specialized.BitVector32.Section" />.</summary>
		/// <returns>The value stored in the specified <see cref="T:System.Collections.Specialized.BitVector32.Section" />.</returns>
		/// <param name="section">A <see cref="T:System.Collections.Specialized.BitVector32.Section" /> that contains the value to get or set. </param>
		// Token: 0x170007A8 RID: 1960
		public int this[BitVector32.Section section]
		{
			get
			{
				return (int)((this._data & (uint)((uint)section.Mask << (int)section.Offset)) >> (int)section.Offset);
			}
			set
			{
				value <<= (int)section.Offset;
				int num = (65535 & (int)section.Mask) << (int)section.Offset;
				this._data = (this._data & (uint)(~(uint)num)) | (uint)(value & num);
			}
		}

		/// <summary>Gets the value of the <see cref="T:System.Collections.Specialized.BitVector32" /> as an integer.</summary>
		/// <returns>The value of the <see cref="T:System.Collections.Specialized.BitVector32" /> as an integer.</returns>
		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x0600261F RID: 9759 RVA: 0x000858B3 File Offset: 0x00083AB3
		public int Data
		{
			get
			{
				return (int)this._data;
			}
		}

		// Token: 0x06002620 RID: 9760 RVA: 0x000858BC File Offset: 0x00083ABC
		private static short CountBitsSet(short mask)
		{
			short num = 0;
			while ((mask & 1) != 0)
			{
				num += 1;
				mask = (short)(mask >> 1);
			}
			return num;
		}

		/// <summary>Creates the first mask in a series of masks that can be used to retrieve individual bits in a <see cref="T:System.Collections.Specialized.BitVector32" /> that is set up as bit flags.</summary>
		/// <returns>A mask that isolates the first bit flag in the <see cref="T:System.Collections.Specialized.BitVector32" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002621 RID: 9761 RVA: 0x000858DE File Offset: 0x00083ADE
		public static int CreateMask()
		{
			return BitVector32.CreateMask(0);
		}

		/// <summary>Creates an additional mask following the specified mask in a series of masks that can be used to retrieve individual bits in a <see cref="T:System.Collections.Specialized.BitVector32" /> that is set up as bit flags.</summary>
		/// <returns>A mask that isolates the bit flag following the one that <paramref name="previous" /> points to in <see cref="T:System.Collections.Specialized.BitVector32" />.</returns>
		/// <param name="previous">The mask that indicates the previous bit flag. </param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="previous" /> indicates the last bit flag in the <see cref="T:System.Collections.Specialized.BitVector32" />. </exception>
		// Token: 0x06002622 RID: 9762 RVA: 0x000858E6 File Offset: 0x00083AE6
		public static int CreateMask(int previous)
		{
			if (previous == 0)
			{
				return 1;
			}
			if (previous == -2147483648)
			{
				throw new InvalidOperationException("Bit vector is full.");
			}
			return previous << 1;
		}

		// Token: 0x06002623 RID: 9763 RVA: 0x00085904 File Offset: 0x00083B04
		private static short CreateMaskFromHighValue(short highValue)
		{
			short num = 16;
			while (((int)highValue & 32768) == 0)
			{
				num -= 1;
				highValue = (short)(highValue << 1);
			}
			ushort num2 = 0;
			while (num > 0)
			{
				num -= 1;
				num2 = (ushort)(num2 << 1);
				num2 |= 1;
			}
			return (short)num2;
		}

		/// <summary>Creates the first <see cref="T:System.Collections.Specialized.BitVector32.Section" /> in a series of sections that contain small integers.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.BitVector32.Section" /> that can hold a number from zero to <paramref name="maxValue" />.</returns>
		/// <param name="maxValue">A 16-bit signed integer that specifies the maximum value for the new <see cref="T:System.Collections.Specialized.BitVector32.Section" />. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="maxValue" /> is less than 1. </exception>
		// Token: 0x06002624 RID: 9764 RVA: 0x00085943 File Offset: 0x00083B43
		public static BitVector32.Section CreateSection(short maxValue)
		{
			return BitVector32.CreateSectionHelper(maxValue, 0, 0);
		}

		/// <summary>Creates a new <see cref="T:System.Collections.Specialized.BitVector32.Section" /> following the specified <see cref="T:System.Collections.Specialized.BitVector32.Section" /> in a series of sections that contain small integers.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.BitVector32.Section" /> that can hold a number from zero to <paramref name="maxValue" />.</returns>
		/// <param name="maxValue">A 16-bit signed integer that specifies the maximum value for the new <see cref="T:System.Collections.Specialized.BitVector32.Section" />. </param>
		/// <param name="previous">The previous <see cref="T:System.Collections.Specialized.BitVector32.Section" /> in the <see cref="T:System.Collections.Specialized.BitVector32" />. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="maxValue" /> is less than 1. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="previous" /> includes the final bit in the <see cref="T:System.Collections.Specialized.BitVector32" />.-or- <paramref name="maxValue" /> is greater than the highest value that can be represented by the number of bits after <paramref name="previous" />. </exception>
		// Token: 0x06002625 RID: 9765 RVA: 0x0008594D File Offset: 0x00083B4D
		public static BitVector32.Section CreateSection(short maxValue, BitVector32.Section previous)
		{
			return BitVector32.CreateSectionHelper(maxValue, previous.Mask, previous.Offset);
		}

		// Token: 0x06002626 RID: 9766 RVA: 0x00085964 File Offset: 0x00083B64
		private static BitVector32.Section CreateSectionHelper(short maxValue, short priorMask, short priorOffset)
		{
			if (maxValue < 1)
			{
				throw new ArgumentException(SR.Format("Argument {0} should be larger than {1}.", "maxValue", 1), "maxValue");
			}
			short num = priorOffset + BitVector32.CountBitsSet(priorMask);
			if (num >= 32)
			{
				throw new InvalidOperationException("Bit vector is full.");
			}
			return new BitVector32.Section(BitVector32.CreateMaskFromHighValue(maxValue), num);
		}

		/// <summary>Determines whether the specified object is equal to the <see cref="T:System.Collections.Specialized.BitVector32" />.</summary>
		/// <returns>true if the specified object is equal to the <see cref="T:System.Collections.Specialized.BitVector32" />; otherwise, false.</returns>
		/// <param name="o">The object to compare with the current <see cref="T:System.Collections.Specialized.BitVector32" />. </param>
		// Token: 0x06002627 RID: 9767 RVA: 0x000859BB File Offset: 0x00083BBB
		public override bool Equals(object o)
		{
			return o is BitVector32 && this._data == ((BitVector32)o)._data;
		}

		/// <summary>Serves as a hash function for the <see cref="T:System.Collections.Specialized.BitVector32" />.</summary>
		/// <returns>A hash code for the <see cref="T:System.Collections.Specialized.BitVector32" />.</returns>
		// Token: 0x06002628 RID: 9768 RVA: 0x000859DA File Offset: 0x00083BDA
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Returns a string that represents the specified <see cref="T:System.Collections.Specialized.BitVector32" />.</summary>
		/// <returns>A string that represents the specified <see cref="T:System.Collections.Specialized.BitVector32" />.</returns>
		/// <param name="value">The <see cref="T:System.Collections.Specialized.BitVector32" /> to represent. </param>
		// Token: 0x06002629 RID: 9769 RVA: 0x000859EC File Offset: 0x00083BEC
		public static string ToString(BitVector32 value)
		{
			StringBuilder stringBuilder = new StringBuilder(45);
			stringBuilder.Append("BitVector32{");
			int num = (int)value._data;
			for (int i = 0; i < 32; i++)
			{
				if (((long)num & (long)((ulong)(-2147483648))) != 0L)
				{
					stringBuilder.Append('1');
				}
				else
				{
					stringBuilder.Append('0');
				}
				num <<= 1;
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Collections.Specialized.BitVector32" />.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Collections.Specialized.BitVector32" />.</returns>
		// Token: 0x0600262A RID: 9770 RVA: 0x00085A53 File Offset: 0x00083C53
		public override string ToString()
		{
			return BitVector32.ToString(this);
		}

		// Token: 0x04001503 RID: 5379
		private uint _data;

		/// <summary>Represents a section of the vector that can contain an integer number.</summary>
		// Token: 0x020004A7 RID: 1191
		public readonly struct Section
		{
			// Token: 0x0600262B RID: 9771 RVA: 0x00085A60 File Offset: 0x00083C60
			internal Section(short mask, short offset)
			{
				this._mask = mask;
				this._offset = offset;
			}

			/// <summary>Gets a mask that isolates this section within the <see cref="T:System.Collections.Specialized.BitVector32" />.</summary>
			/// <returns>A mask that isolates this section within the <see cref="T:System.Collections.Specialized.BitVector32" />.</returns>
			// Token: 0x170007AA RID: 1962
			// (get) Token: 0x0600262C RID: 9772 RVA: 0x00085A70 File Offset: 0x00083C70
			public short Mask
			{
				get
				{
					return this._mask;
				}
			}

			/// <summary>Gets the offset of this section from the start of the <see cref="T:System.Collections.Specialized.BitVector32" />.</summary>
			/// <returns>The offset of this section from the start of the <see cref="T:System.Collections.Specialized.BitVector32" />.</returns>
			// Token: 0x170007AB RID: 1963
			// (get) Token: 0x0600262D RID: 9773 RVA: 0x00085A78 File Offset: 0x00083C78
			public short Offset
			{
				get
				{
					return this._offset;
				}
			}

			/// <summary>Determines whether the specified object is the same as the current <see cref="T:System.Collections.Specialized.BitVector32.Section" /> object.</summary>
			/// <returns>true if the specified object is the same as the current <see cref="T:System.Collections.Specialized.BitVector32.Section" /> object; otherwise, false.</returns>
			/// <param name="o">The object to compare with the current <see cref="T:System.Collections.Specialized.BitVector32.Section" />.</param>
			// Token: 0x0600262E RID: 9774 RVA: 0x00085A80 File Offset: 0x00083C80
			public override bool Equals(object o)
			{
				return o is BitVector32.Section && this.Equals((BitVector32.Section)o);
			}

			/// <summary>Determines whether the specified <see cref="T:System.Collections.Specialized.BitVector32.Section" /> object is the same as the current <see cref="T:System.Collections.Specialized.BitVector32.Section" /> object.</summary>
			/// <returns>true if the <paramref name="obj" /> parameter is the same as the current <see cref="T:System.Collections.Specialized.BitVector32.Section" /> object; otherwise false.</returns>
			/// <param name="obj">The <see cref="T:System.Collections.Specialized.BitVector32.Section" /> object to compare with the current <see cref="T:System.Collections.Specialized.BitVector32.Section" /> object.</param>
			// Token: 0x0600262F RID: 9775 RVA: 0x00085A98 File Offset: 0x00083C98
			public bool Equals(BitVector32.Section obj)
			{
				return obj._mask == this._mask && obj._offset == this._offset;
			}

			/// <summary>Determines whether two specified <see cref="T:System.Collections.Specialized.BitVector32.Section" /> objects are equal.</summary>
			/// <returns>true if the <paramref name="a" /> and <paramref name="b" /> parameters represent the same <see cref="T:System.Collections.Specialized.BitVector32.Section" /> object, otherwise, false.</returns>
			/// <param name="a">A <see cref="T:System.Collections.Specialized.BitVector32.Section" /> object.</param>
			/// <param name="b">A <see cref="T:System.Collections.Specialized.BitVector32.Section" /> object.</param>
			// Token: 0x06002630 RID: 9776 RVA: 0x00085AB8 File Offset: 0x00083CB8
			public static bool operator ==(BitVector32.Section a, BitVector32.Section b)
			{
				return a.Equals(b);
			}

			/// <summary>Determines whether two <see cref="T:System.Collections.Specialized.BitVector32.Section" /> objects have different values.</summary>
			/// <returns>true if the <paramref name="a" /> and <paramref name="b" /> parameters represent different <see cref="T:System.Collections.Specialized.BitVector32.Section" /> objects; otherwise, false.</returns>
			/// <param name="a">A <see cref="T:System.Collections.Specialized.BitVector32.Section" /> object.</param>
			/// <param name="b">A <see cref="T:System.Collections.Specialized.BitVector32.Section" /> object.</param>
			// Token: 0x06002631 RID: 9777 RVA: 0x00085AC2 File Offset: 0x00083CC2
			public static bool operator !=(BitVector32.Section a, BitVector32.Section b)
			{
				return !(a == b);
			}

			/// <summary>Serves as a hash function for the current <see cref="T:System.Collections.Specialized.BitVector32.Section" />, suitable for hashing algorithms and data structures, such as a hash table.</summary>
			/// <returns>A hash code for the current <see cref="T:System.Collections.Specialized.BitVector32.Section" />.</returns>
			// Token: 0x06002632 RID: 9778 RVA: 0x00085ACE File Offset: 0x00083CCE
			public override int GetHashCode()
			{
				return base.GetHashCode();
			}

			/// <summary>Returns a string that represents the specified <see cref="T:System.Collections.Specialized.BitVector32.Section" />.</summary>
			/// <returns>A string that represents the specified <see cref="T:System.Collections.Specialized.BitVector32.Section" />.</returns>
			/// <param name="value">The <see cref="T:System.Collections.Specialized.BitVector32.Section" /> to represent.</param>
			// Token: 0x06002633 RID: 9779 RVA: 0x00085AE0 File Offset: 0x00083CE0
			public static string ToString(BitVector32.Section value)
			{
				return string.Concat(new string[]
				{
					"Section{0x",
					Convert.ToString(value.Mask, 16),
					", 0x",
					Convert.ToString(value.Offset, 16),
					"}"
				});
			}

			/// <summary>Returns a string that represents the current <see cref="T:System.Collections.Specialized.BitVector32.Section" />.</summary>
			/// <returns>A string that represents the current <see cref="T:System.Collections.Specialized.BitVector32.Section" />.</returns>
			// Token: 0x06002634 RID: 9780 RVA: 0x00085B32 File Offset: 0x00083D32
			public override string ToString()
			{
				return BitVector32.Section.ToString(this);
			}

			// Token: 0x04001504 RID: 5380
			private readonly short _mask;

			// Token: 0x04001505 RID: 5381
			private readonly short _offset;
		}
	}
}
