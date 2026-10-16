using System;
using System.Collections.Immutable;
using System.Reflection.Internal;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x02000015 RID: 21
	[NullableContext(1)]
	[Nullable(0)]
	internal static class BlobUtilities
	{
		// Token: 0x0600009C RID: 156 RVA: 0x0000299C File Offset: 0x00000B9C
		[NullableContext(0)]
		[return: Nullable(1)]
		public unsafe static byte[] ReadBytes(byte* buffer, int byteCount)
		{
			if (byteCount == 0)
			{
				return EmptyArray<byte>.Instance;
			}
			byte[] array = new byte[byteCount];
			Marshal.Copy((IntPtr)((void*)buffer), array, 0, byteCount);
			return array;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000029C8 File Offset: 0x00000BC8
		[NullableContext(0)]
		public unsafe static ImmutableArray<byte> ReadImmutableBytes(byte* buffer, int byteCount)
		{
			byte[] array = BlobUtilities.ReadBytes(buffer, byteCount);
			return ImmutableByteArrayInterop.DangerousCreateFromUnderlyingArray(ref array);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000029E4 File Offset: 0x00000BE4
		public unsafe static void WriteBytes(this byte[] buffer, int start, byte value, int byteCount)
		{
			fixed (byte* ptr = &buffer[0])
			{
				byte* ptr2 = ptr;
				byte* ptr3 = ptr2 + start;
				for (int i = 0; i < byteCount; i++)
				{
					ptr3[i] = value;
				}
			}
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00002A14 File Offset: 0x00000C14
		public unsafe static void WriteDouble(this byte[] buffer, int start, double value)
		{
			buffer.WriteUInt64(start, (ulong)(*(long*)(&value)));
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00002A21 File Offset: 0x00000C21
		public unsafe static void WriteSingle(this byte[] buffer, int start, float value)
		{
			buffer.WriteUInt32(start, *(uint*)(&value));
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00002A2E File Offset: 0x00000C2E
		public static void WriteByte(this byte[] buffer, int start, byte value)
		{
			buffer[start] = value;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00002A34 File Offset: 0x00000C34
		public unsafe static void WriteUInt16(this byte[] buffer, int start, ushort value)
		{
			fixed (byte* ptr = &buffer[start])
			{
				byte* ptr2 = ptr;
				*ptr2 = (byte)value;
				ptr2[1] = (byte)(value >> 8);
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00002A5C File Offset: 0x00000C5C
		public unsafe static void WriteUInt16BE(this byte[] buffer, int start, ushort value)
		{
			fixed (byte* ptr = &buffer[start])
			{
				byte* ptr2 = ptr;
				*ptr2 = (byte)(value >> 8);
				ptr2[1] = (byte)value;
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00002A84 File Offset: 0x00000C84
		public unsafe static void WriteUInt32BE(this byte[] buffer, int start, uint value)
		{
			fixed (byte* ptr = &buffer[start])
			{
				byte* ptr2 = ptr;
				*ptr2 = (byte)(value >> 24);
				ptr2[1] = (byte)(value >> 16);
				ptr2[2] = (byte)(value >> 8);
				ptr2[3] = (byte)value;
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00002AC0 File Offset: 0x00000CC0
		public unsafe static void WriteUInt32(this byte[] buffer, int start, uint value)
		{
			fixed (byte* ptr = &buffer[start])
			{
				byte* ptr2 = ptr;
				*ptr2 = (byte)value;
				ptr2[1] = (byte)(value >> 8);
				ptr2[2] = (byte)(value >> 16);
				ptr2[3] = (byte)(value >> 24);
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00002AF9 File Offset: 0x00000CF9
		public static void WriteUInt64(this byte[] buffer, int start, ulong value)
		{
			buffer.WriteUInt32(start, (uint)value);
			buffer.WriteUInt32(start + 4, (uint)(value >> 32));
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00002B14 File Offset: 0x00000D14
		public static void WriteDecimal(this byte[] buffer, int start, decimal value)
		{
			bool flag;
			byte b;
			uint num;
			uint num2;
			uint num3;
			value.GetBits(out flag, out b, out num, out num2, out num3);
			buffer.WriteByte(start, b | (flag ? 128 : 0));
			buffer.WriteUInt32(start + 1, num);
			buffer.WriteUInt32(start + 5, num2);
			buffer.WriteUInt32(start + 9, num3);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00002B68 File Offset: 0x00000D68
		public unsafe static void WriteGuid(this byte[] buffer, int start, Guid value)
		{
			fixed (byte* ptr = &buffer[start])
			{
				byte* ptr2 = ptr;
				byte* ptr3 = (byte*)(&value);
				uint num = *(uint*)ptr3;
				*ptr2 = (byte)num;
				ptr2[1] = (byte)(num >> 8);
				ptr2[2] = (byte)(num >> 16);
				ptr2[3] = (byte)(num >> 24);
				ushort num2 = *(ushort*)(ptr3 + 4);
				ptr2[4] = (byte)num2;
				ptr2[5] = (byte)(num2 >> 8);
				ushort num3 = *(ushort*)(ptr3 + 6);
				ptr2[6] = (byte)num3;
				ptr2[7] = (byte)(num3 >> 8);
				ptr2[8] = ptr3[8];
				ptr2[9] = ptr3[9];
				ptr2[10] = ptr3[10];
				ptr2[11] = ptr3[11];
				ptr2[12] = ptr3[12];
				ptr2[13] = ptr3[13];
				ptr2[14] = ptr3[14];
				ptr2[15] = ptr3[15];
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00002C24 File Offset: 0x00000E24
		[NullableContext(0)]
		public unsafe static void WriteUTF8([Nullable(1)] this byte[] buffer, int start, char* charPtr, int charCount, int byteCount, bool allowUnpairedSurrogates)
		{
			char* ptr = charPtr + charCount;
			fixed (byte* ptr2 = &buffer[0])
			{
				byte* ptr3 = ptr2;
				byte* ptr4 = ptr3 + start;
				if (byteCount == charCount)
				{
					while (charPtr < ptr)
					{
						*(ptr4++) = (byte)(*(charPtr++));
					}
				}
				else
				{
					while (charPtr < ptr)
					{
						char c = *(charPtr++);
						if (c < '\u0080')
						{
							*(ptr4++) = (byte)c;
						}
						else if (c < 'ࠀ')
						{
							*ptr4 = (byte)(((c >> 6) & '\u001f') | 'À');
							ptr4[1] = (byte)((c & '?') | '\u0080');
							ptr4 += 2;
						}
						else
						{
							if (BlobUtilities.IsSurrogateChar((int)c))
							{
								if (BlobUtilities.IsHighSurrogateChar((int)c) && charPtr < ptr && BlobUtilities.IsLowSurrogateChar((int)(*charPtr)))
								{
									int num = (int)c;
									int num2 = (int)(*(charPtr++));
									int num3 = (num - 55296 << 10) + num2 - 56320 + 65536;
									*ptr4 = (byte)(((num3 >> 18) & 7) | 240);
									ptr4[1] = (byte)(((num3 >> 12) & 63) | 128);
									ptr4[2] = (byte)(((num3 >> 6) & 63) | 128);
									ptr4[3] = (byte)((num3 & 63) | 128);
									ptr4 += 4;
									continue;
								}
								if (!allowUnpairedSurrogates)
								{
									c = '\ufffd';
								}
							}
							*ptr4 = (byte)(((c >> 12) & '\u000f') | 'à');
							ptr4[1] = (byte)(((c >> 6) & '?') | '\u0080');
							ptr4[2] = (byte)((c & '?') | '\u0080');
							ptr4 += 3;
						}
					}
				}
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00002DA0 File Offset: 0x00000FA0
		internal unsafe static int GetUTF8ByteCount(string str)
		{
			char* ptr = str;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return BlobUtilities.GetUTF8ByteCount(ptr, str.Length);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00002DCC File Offset: 0x00000FCC
		[NullableContext(0)]
		internal unsafe static int GetUTF8ByteCount(char* str, int charCount)
		{
			char* ptr;
			return BlobUtilities.GetUTF8ByteCount(str, charCount, int.MaxValue, out ptr);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00002DE8 File Offset: 0x00000FE8
		[NullableContext(0)]
		internal unsafe static int GetUTF8ByteCount(char* str, int charCount, int byteLimit, out char* remainder)
		{
			char* ptr = str + charCount;
			char* ptr2 = str;
			int num = 0;
			while (ptr2 < ptr)
			{
				char c = *(ptr2++);
				int num2;
				if (c < '\u0080')
				{
					num2 = 1;
				}
				else if (c < 'ࠀ')
				{
					num2 = 2;
				}
				else if (BlobUtilities.IsHighSurrogateChar((int)c) && ptr2 < ptr && BlobUtilities.IsLowSurrogateChar((int)(*ptr2)))
				{
					num2 = 4;
					ptr2++;
				}
				else
				{
					num2 = 3;
				}
				if (num + num2 > byteLimit)
				{
					ptr2 -= ((num2 < 4) ? 1 : 2);
					break;
				}
				num += num2;
			}
			remainder = ptr2;
			return num;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00002E66 File Offset: 0x00001066
		internal static bool IsSurrogateChar(int c)
		{
			return c - 55296 <= 2047;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00002E79 File Offset: 0x00001079
		internal static bool IsHighSurrogateChar(int c)
		{
			return c - 55296 <= 1023;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00002E8C File Offset: 0x0000108C
		internal static bool IsLowSurrogateChar(int c)
		{
			return c - 56320 <= 1023;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00002E9F File Offset: 0x0000109F
		[MethodImpl(256)]
		internal static void ValidateRange(int bufferLength, int start, int byteCount, string byteCountParameterName)
		{
			if (start < 0 || start > bufferLength)
			{
				Throw.ArgumentOutOfRange("start");
			}
			if (byteCount < 0 || byteCount > bufferLength - start)
			{
				Throw.ArgumentOutOfRange(byteCountParameterName);
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00002EC3 File Offset: 0x000010C3
		internal static int GetUserStringByteLength(int characterCount)
		{
			return characterCount * 2 + 1;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00002ECC File Offset: 0x000010CC
		internal static byte GetUserStringTrailingByte(string str)
		{
			for (int i = 0; i < str.Length; i++)
			{
				char c = str.get_Chars(i);
				if (c >= '\u007f')
				{
					return 1;
				}
				switch (c)
				{
				case '\u0001':
				case '\u0002':
				case '\u0003':
				case '\u0004':
				case '\u0005':
				case '\u0006':
				case '\a':
				case '\b':
				case '\u000e':
				case '\u000f':
				case '\u0010':
				case '\u0011':
				case '\u0012':
				case '\u0013':
				case '\u0014':
				case '\u0015':
				case '\u0016':
				case '\u0017':
				case '\u0018':
				case '\u0019':
				case '\u001a':
				case '\u001b':
				case '\u001c':
				case '\u001d':
				case '\u001e':
				case '\u001f':
				case '\'':
				case '-':
					return 1;
				}
			}
			return 0;
		}

		// Token: 0x0400000D RID: 13
		public const int SizeOfSerializedDecimal = 13;

		// Token: 0x0400000E RID: 14
		public const int SizeOfGuid = 16;
	}
}
