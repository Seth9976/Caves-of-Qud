using System;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000139 RID: 313
	internal static class MemoryHelpers
	{
		// Token: 0x06001173 RID: 4467 RVA: 0x00053B94 File Offset: 0x00051D94
		public unsafe static bool Compare(void* ptr1, void* ptr2, MemoryHelpers.BitRegion region)
		{
			if (region.sizeInBits == 1U)
			{
				return MemoryHelpers.ReadSingleBit(ptr1, region.bitOffset) == MemoryHelpers.ReadSingleBit(ptr2, region.bitOffset);
			}
			return MemoryHelpers.MemCmpBitRegion(ptr1, ptr2, region.bitOffset, region.sizeInBits, null);
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x00053BCF File Offset: 0x00051DCF
		public static uint ComputeFollowingByteOffset(uint byteOffset, uint sizeInBits)
		{
			return (uint)((ulong)(byteOffset + sizeInBits / 8U) + (ulong)((sizeInBits % 8U > 0U) ? 1L : 0L));
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x00053BE4 File Offset: 0x00051DE4
		public unsafe static void WriteSingleBit(void* ptr, uint bitOffset, bool value)
		{
			uint num = bitOffset >> 3;
			bitOffset &= 7U;
			if (value)
			{
				byte* ptr2 = (byte*)ptr + num;
				*ptr2 |= (byte)(1 << (int)bitOffset);
				return;
			}
			byte* ptr3 = (byte*)ptr + num;
			*ptr3 &= (byte)(~(byte)(1 << (int)bitOffset));
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x00053C20 File Offset: 0x00051E20
		public unsafe static bool ReadSingleBit(void* ptr, uint bitOffset)
		{
			uint num = bitOffset >> 3;
			bitOffset &= 7U;
			return ((int)((byte*)ptr)[num] & (1 << (int)bitOffset)) != 0;
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x00053C48 File Offset: 0x00051E48
		public unsafe static void MemCpyBitRegion(void* destination, void* source, uint bitOffset, uint bitCount)
		{
			byte* ptr = (byte*)destination;
			byte* ptr2 = (byte*)source;
			if (bitOffset >= 8U)
			{
				uint num = bitOffset / 8U;
				ptr += num;
				ptr2 += num;
				bitOffset %= 8U;
			}
			if (bitOffset > 0U)
			{
				int num2 = 255 << (int)bitOffset;
				if (bitCount + bitOffset < 8U)
				{
					num2 &= 255 >> (int)(8U - (bitCount + bitOffset));
				}
				*ptr = (byte)((((int)(*ptr) & ~num2) | ((int)(*ptr2) & num2)) & 255);
				if (bitCount + bitOffset <= 8U)
				{
					return;
				}
				ptr++;
				ptr2++;
				bitCount -= 8U - bitOffset;
			}
			uint num3 = bitCount / 8U;
			if (num3 >= 1U)
			{
				UnsafeUtility.MemCpy((void*)ptr, (void*)ptr2, (long)((ulong)num3));
			}
			uint num4 = bitCount % 8U;
			if (num4 > 0U)
			{
				ptr += num3;
				ptr2 += num3;
				int num5 = 255 >> (int)(8U - num4);
				*ptr = (byte)((((int)(*ptr) & ~num5) | ((int)(*ptr2) & num5)) & 255);
			}
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x00053D10 File Offset: 0x00051F10
		public unsafe static bool MemCmpBitRegion(void* ptr1, void* ptr2, uint bitOffset, uint bitCount, void* mask = null)
		{
			byte* ptr3 = (byte*)ptr1;
			byte* ptr4 = (byte*)ptr2;
			byte* ptr5 = (byte*)mask;
			if (bitOffset >= 8U)
			{
				uint num = bitOffset / 8U;
				ptr3 += num;
				ptr4 += num;
				if (ptr5 != null)
				{
					ptr5 += num;
				}
				bitOffset %= 8U;
			}
			if (bitOffset > 0U)
			{
				int num2 = 255 << (int)bitOffset;
				if (bitCount + bitOffset < 8U)
				{
					num2 &= 255 >> (int)(8U - (bitCount + bitOffset));
				}
				if (ptr5 != null)
				{
					num2 &= (int)(*ptr5);
					ptr5++;
				}
				int num3 = (int)(*ptr3) & num2;
				int num4 = (int)(*ptr4) & num2;
				if (num3 != num4)
				{
					return false;
				}
				if (bitCount + bitOffset <= 8U)
				{
					return true;
				}
				ptr3++;
				ptr4++;
				bitCount -= 8U - bitOffset;
			}
			uint num5 = bitCount / 8U;
			if (num5 >= 1U)
			{
				if (ptr5 != null)
				{
					int num6 = 0;
					while ((long)num6 < (long)((ulong)num5))
					{
						byte b = ptr3[num6];
						byte b2 = ptr4[num6];
						byte b3 = ptr5[num6];
						if ((b & b3) != (b2 & b3))
						{
							return false;
						}
						num6++;
					}
				}
				else if (UnsafeUtility.MemCmp((void*)ptr3, (void*)ptr4, (long)((ulong)num5)) != 0)
				{
					return false;
				}
			}
			uint num7 = bitCount % 8U;
			if (num7 > 0U)
			{
				ptr3 += num5;
				ptr4 += num5;
				int num8 = 255 >> (int)(8U - num7);
				if (ptr5 != null)
				{
					ptr5 += num5;
					num8 &= (int)(*ptr5);
				}
				int num9 = (int)(*ptr3) & num8;
				int num10 = (int)(*ptr4) & num8;
				if (num9 != num10)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x00053E40 File Offset: 0x00052040
		public unsafe static void MemSet(void* destination, int numBytes, byte value)
		{
			int num = 0;
			while (numBytes >= 8)
			{
				*(long*)((byte*)destination + num) = (long)(((ulong)value << 56) | ((ulong)value << 48) | ((ulong)value << 40) | ((ulong)value << 32) | ((ulong)value << 24) | ((ulong)value << 16) | ((ulong)value << 8) | (ulong)value);
				numBytes -= 8;
				num += 8;
			}
			while (numBytes >= 4)
			{
				*(int*)((byte*)destination + num) = ((int)value << 24) | ((int)value << 16) | ((int)value << 8) | (int)value;
				numBytes -= 4;
				num += 4;
			}
			while (numBytes > 0)
			{
				((byte*)destination)[num] = value;
				numBytes--;
				num++;
			}
		}

		// Token: 0x0600117A RID: 4474 RVA: 0x00053EC8 File Offset: 0x000520C8
		public unsafe static void MemCpyMasked(void* destination, void* source, int numBytes, void* mask)
		{
			int num = 0;
			while (numBytes >= 8)
			{
				*(long*)((byte*)destination + num) &= ~(*(long*)((byte*)mask + num));
				*(long*)((byte*)destination + num) |= *(long*)((byte*)source + num) & *(long*)((byte*)mask + num);
				numBytes -= 8;
				num += 8;
			}
			while (numBytes >= 4)
			{
				*(uint*)((byte*)destination + num) &= ~(*(uint*)((byte*)mask + num));
				*(uint*)((byte*)destination + num) |= *(uint*)((byte*)source + num) & *(uint*)((byte*)mask + num);
				numBytes -= 4;
				num += 4;
			}
			while (numBytes > 0)
			{
				byte* ptr = (byte*)destination + num;
				*ptr &= ~((byte*)mask)[num];
				byte* ptr2 = (byte*)destination + num;
				*ptr2 |= ((byte*)source)[num] & ((byte*)mask)[num];
				numBytes--;
				num++;
			}
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x00053F64 File Offset: 0x00052164
		public unsafe static uint ReadMultipleBitsAsUInt(void* ptr, uint bitOffset, uint bitCount)
		{
			if (ptr == null)
			{
				throw new ArgumentNullException("ptr");
			}
			if (bitCount > 32U)
			{
				throw new ArgumentException("Trying to read more than 32 bits as int", "bitCount");
			}
			if (bitOffset > 32U)
			{
				int num = (int)(bitOffset % 32U);
				int num2 = (int)((bitOffset - (uint)num) / 32U);
				ptr = (void*)((byte*)ptr + num2 * 4);
				bitOffset = (uint)num;
			}
			if (bitOffset + bitCount <= 8U)
			{
				uint num3 = (uint)((byte)(*(byte*)ptr >> (int)bitOffset));
				uint num4 = 255U >> (int)(8U - bitCount);
				return num3 & num4;
			}
			if (bitOffset + bitCount <= 16U)
			{
				uint num5 = (uint)((ushort)(*(ushort*)ptr >> (int)bitOffset));
				uint num6 = 65535U >> (int)(16U - bitCount);
				return num5 & num6;
			}
			if (bitOffset + bitCount <= 32U)
			{
				uint num7 = *(uint*)ptr >> (int)bitOffset;
				uint num8 = uint.MaxValue >> (int)(32U - bitCount);
				return num7 & num8;
			}
			throw new NotImplementedException("Reading int straddling int boundary");
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x00054018 File Offset: 0x00052218
		public unsafe static void WriteUIntAsMultipleBits(void* ptr, uint bitOffset, uint bitCount, uint value)
		{
			if (ptr == null)
			{
				throw new ArgumentNullException("ptr");
			}
			if (bitCount > 32U)
			{
				throw new ArgumentException("Trying to write more than 32 bits as int", "bitCount");
			}
			if (bitOffset > 32U)
			{
				int num = (int)(bitOffset % 32U);
				int num2 = (int)((bitOffset - (uint)num) / 32U);
				ptr = (void*)((byte*)ptr + num2 * 4);
				bitOffset = (uint)num;
			}
			if (bitOffset + bitCount <= 8U)
			{
				byte b = (byte)value;
				b = (byte)(b << (int)bitOffset);
				uint num3 = ~(255U >> (int)(8U - bitCount) << (int)bitOffset);
				*(byte*)ptr = (byte)(((uint)(*(byte*)ptr) & num3) | (uint)b);
				return;
			}
			if (bitOffset + bitCount <= 16U)
			{
				ushort num4 = (ushort)value;
				num4 = (ushort)(num4 << (int)bitOffset);
				uint num5 = ~(65535U >> (int)(16U - bitCount) << (int)bitOffset);
				*(short*)ptr = (short)((ushort)(((uint)(*(ushort*)ptr) & num5) | (uint)num4));
				return;
			}
			if (bitOffset + bitCount <= 32U)
			{
				uint num6 = value << (int)bitOffset;
				uint num7 = ~(uint.MaxValue >> (int)(32U - bitCount) << (int)bitOffset);
				*(int*)ptr = (int)((*(uint*)ptr & num7) | num6);
				return;
			}
			throw new NotImplementedException("Writing int straddling int boundary");
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x00054102 File Offset: 0x00052302
		public unsafe static int ReadTwosComplementMultipleBitsAsInt(void* ptr, uint bitOffset, uint bitCount)
		{
			return (int)MemoryHelpers.ReadMultipleBitsAsUInt(ptr, bitOffset, bitCount);
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x0005410C File Offset: 0x0005230C
		public unsafe static void WriteIntAsTwosComplementMultipleBits(void* ptr, uint bitOffset, uint bitCount, int value)
		{
			MemoryHelpers.WriteUIntAsMultipleBits(ptr, bitOffset, bitCount, (uint)value);
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x00054118 File Offset: 0x00052318
		public unsafe static int ReadExcessKMultipleBitsAsInt(void* ptr, uint bitOffset, uint bitCount)
		{
			long num = (long)((ulong)MemoryHelpers.ReadMultipleBitsAsUInt(ptr, bitOffset, bitCount));
			long num2 = (1L << (int)bitCount) / 2L;
			return (int)(num - num2);
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x0005413C File Offset: 0x0005233C
		public unsafe static void WriteIntAsExcessKMultipleBits(void* ptr, uint bitOffset, uint bitCount, int value)
		{
			long num = (1L << (int)bitCount) / 2L + (long)value;
			MemoryHelpers.WriteUIntAsMultipleBits(ptr, bitOffset, bitCount, (uint)num);
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x00054164 File Offset: 0x00052364
		public unsafe static float ReadMultipleBitsAsNormalizedUInt(void* ptr, uint bitOffset, uint bitCount)
		{
			uint num = MemoryHelpers.ReadMultipleBitsAsUInt(ptr, bitOffset, bitCount);
			uint num2 = (uint)((1L << (int)bitCount) - 1L);
			return NumberHelpers.UIntToNormalizedFloat(num, 0U, num2);
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x0005418C File Offset: 0x0005238C
		public unsafe static void WriteNormalizedUIntAsMultipleBits(void* ptr, uint bitOffset, uint bitCount, float value)
		{
			uint num = (uint)((1L << (int)bitCount) - 1L);
			uint num2 = NumberHelpers.NormalizedFloatToUInt(value, 0U, num);
			MemoryHelpers.WriteUIntAsMultipleBits(ptr, bitOffset, bitCount, num2);
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x000541B8 File Offset: 0x000523B8
		public unsafe static void SetBitsInBuffer(void* buffer, int byteOffset, int bitOffset, int sizeInBits, bool value)
		{
			if (buffer == null)
			{
				throw new ArgumentException("A buffer must be provided to apply the bitmask on", "buffer");
			}
			if (sizeInBits < 0)
			{
				throw new ArgumentException("Negative sizeInBits", "sizeInBits");
			}
			if (bitOffset < 0)
			{
				throw new ArgumentException("Negative bitOffset", "bitOffset");
			}
			if (byteOffset < 0)
			{
				throw new ArgumentException("Negative byteOffset", "byteOffset");
			}
			if (bitOffset >= 8)
			{
				int num = bitOffset / 8;
				byteOffset += num;
				bitOffset %= 8;
			}
			byte* ptr = (byte*)buffer + byteOffset;
			int i = sizeInBits;
			if (bitOffset != 0)
			{
				int num2 = 255 << bitOffset;
				if (i + bitOffset < 8)
				{
					num2 &= 255 >> 8 - (i + bitOffset);
				}
				if (value)
				{
					byte* ptr2 = ptr;
					*ptr2 |= (byte)num2;
				}
				else
				{
					byte* ptr3 = ptr;
					*ptr3 &= (byte)(~(byte)num2);
				}
				ptr++;
				i -= 8 - bitOffset;
			}
			while (i >= 8)
			{
				*ptr = (value ? byte.MaxValue : 0);
				ptr++;
				i -= 8;
			}
			if (i > 0)
			{
				byte b = (byte)(255 >> 8 - i);
				if (value)
				{
					byte* ptr4 = ptr;
					*ptr4 |= b;
					return;
				}
				byte* ptr5 = ptr;
				*ptr5 &= ~b;
			}
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x000542BC File Offset: 0x000524BC
		public static void Swap<TValue>(ref TValue a, ref TValue b)
		{
			TValue tvalue = a;
			a = b;
			b = tvalue;
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x000542E4 File Offset: 0x000524E4
		public static uint AlignNatural(uint offset, uint sizeInBytes)
		{
			uint num = Math.Min(8U, sizeInBytes);
			return offset.AlignToMultipleOf(num);
		}

		// Token: 0x02000249 RID: 585
		public struct BitRegion
		{
			// Token: 0x17000609 RID: 1545
			// (get) Token: 0x0600164E RID: 5710 RVA: 0x00066FC3 File Offset: 0x000651C3
			public bool isEmpty
			{
				get
				{
					return this.sizeInBits == 0U;
				}
			}

			// Token: 0x0600164F RID: 5711 RVA: 0x00066FCE File Offset: 0x000651CE
			public BitRegion(uint bitOffset, uint sizeInBits)
			{
				this.bitOffset = bitOffset;
				this.sizeInBits = sizeInBits;
			}

			// Token: 0x06001650 RID: 5712 RVA: 0x00066FDE File Offset: 0x000651DE
			public BitRegion(uint byteOffset, uint bitOffset, uint sizeInBits)
			{
				this.bitOffset = byteOffset * 8U + bitOffset;
				this.sizeInBits = sizeInBits;
			}

			// Token: 0x06001651 RID: 5713 RVA: 0x00066FF4 File Offset: 0x000651F4
			public MemoryHelpers.BitRegion Overlap(MemoryHelpers.BitRegion other)
			{
				uint num = this.bitOffset + this.sizeInBits;
				uint num2 = other.bitOffset + other.sizeInBits;
				if (num <= other.bitOffset || num2 <= this.bitOffset)
				{
					return default(MemoryHelpers.BitRegion);
				}
				uint num3 = Math.Min(num, num2);
				uint num4 = Math.Max(this.bitOffset, other.bitOffset);
				return new MemoryHelpers.BitRegion(num4, num3 - num4);
			}

			// Token: 0x04000C8D RID: 3213
			public uint bitOffset;

			// Token: 0x04000C8E RID: 3214
			public uint sizeInBits;
		}
	}
}
