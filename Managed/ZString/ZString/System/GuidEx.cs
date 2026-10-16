using System;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x02000009 RID: 9
	internal struct GuidEx
	{
		// Token: 0x06000015 RID: 21 RVA: 0x000021FA File Offset: 0x000003FA
		private unsafe static int HexsToChars(char* guidChars, int a, int b)
		{
			*guidChars = HexConverter.ToCharLower(a >> 4);
			guidChars[1] = HexConverter.ToCharLower(a);
			guidChars[2] = HexConverter.ToCharLower(b >> 4);
			guidChars[3] = HexConverter.ToCharLower(b);
			return 4;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002230 File Offset: 0x00000430
		private unsafe static int HexsToCharsHexOutput(char* guidChars, int a, int b)
		{
			*guidChars = '0';
			guidChars[1] = 'x';
			guidChars[2] = HexConverter.ToCharLower(a >> 4);
			guidChars[3] = HexConverter.ToCharLower(a);
			guidChars[4] = ',';
			guidChars[5] = '0';
			guidChars[6] = 'x';
			guidChars[7] = HexConverter.ToCharLower(b >> 4);
			guidChars[8] = HexConverter.ToCharLower(b);
			return 9;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000229C File Offset: 0x0000049C
		public unsafe bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format = default(ReadOnlySpan<char>))
		{
			if (format.Length == 0)
			{
				format = "D".AsSpan();
			}
			if (format.Length != 1)
			{
				throw new FormatException("InvalidGuidFormatSpecification");
			}
			bool flag = true;
			bool flag2 = false;
			int num = 0;
			char c = (char)(*format[0]);
			if (c <= 'X')
			{
				if (c <= 'D')
				{
					if (c == 'B')
					{
						goto IL_009D;
					}
					if (c != 'D')
					{
						goto IL_00C2;
					}
				}
				else
				{
					if (c == 'N')
					{
						goto IL_0096;
					}
					if (c == 'P')
					{
						goto IL_00A8;
					}
					if (c != 'X')
					{
						goto IL_00C2;
					}
					goto IL_00B3;
				}
			}
			else if (c <= 'd')
			{
				if (c == 'b')
				{
					goto IL_009D;
				}
				if (c != 'd')
				{
					goto IL_00C2;
				}
			}
			else
			{
				if (c == 'n')
				{
					goto IL_0096;
				}
				if (c == 'p')
				{
					goto IL_00A8;
				}
				if (c != 'x')
				{
					goto IL_00C2;
				}
				goto IL_00B3;
			}
			int num2 = 36;
			goto IL_00CD;
			IL_0096:
			flag = false;
			num2 = 32;
			goto IL_00CD;
			IL_009D:
			num = 8192123;
			num2 = 38;
			goto IL_00CD;
			IL_00A8:
			num = 2687016;
			num2 = 38;
			goto IL_00CD;
			IL_00B3:
			num = 8192123;
			flag = false;
			flag2 = true;
			num2 = 68;
			goto IL_00CD;
			IL_00C2:
			throw new FormatException("InvalidGuidFormatSpecification");
			IL_00CD:
			if (destination.Length < num2)
			{
				charsWritten = 0;
				return false;
			}
			fixed (char* reference = MemoryMarshal.GetReference<char>(destination))
			{
				char* ptr = reference;
				if (num != 0)
				{
					*(ptr++) = (char)num;
				}
				if (flag2)
				{
					*(ptr++) = '0';
					*(ptr++) = 'x';
					ptr += GuidEx.HexsToChars(ptr, this._a >> 24, this._a >> 16);
					ptr += GuidEx.HexsToChars(ptr, this._a >> 8, this._a);
					*(ptr++) = ',';
					*(ptr++) = '0';
					*(ptr++) = 'x';
					ptr += GuidEx.HexsToChars(ptr, this._b >> 8, (int)this._b);
					*(ptr++) = ',';
					*(ptr++) = '0';
					*(ptr++) = 'x';
					ptr += GuidEx.HexsToChars(ptr, this._c >> 8, (int)this._c);
					*(ptr++) = ',';
					*(ptr++) = '{';
					ptr += GuidEx.HexsToCharsHexOutput(ptr, (int)this._d, (int)this._e);
					*(ptr++) = ',';
					ptr += GuidEx.HexsToCharsHexOutput(ptr, (int)this._f, (int)this._g);
					*(ptr++) = ',';
					ptr += GuidEx.HexsToCharsHexOutput(ptr, (int)this._h, (int)this._i);
					*(ptr++) = ',';
					ptr += GuidEx.HexsToCharsHexOutput(ptr, (int)this._j, (int)this._k);
					*(ptr++) = '}';
				}
				else
				{
					ptr += GuidEx.HexsToChars(ptr, this._a >> 24, this._a >> 16);
					ptr += GuidEx.HexsToChars(ptr, this._a >> 8, this._a);
					if (flag)
					{
						*(ptr++) = '-';
					}
					ptr += GuidEx.HexsToChars(ptr, this._b >> 8, (int)this._b);
					if (flag)
					{
						*(ptr++) = '-';
					}
					ptr += GuidEx.HexsToChars(ptr, this._c >> 8, (int)this._c);
					if (flag)
					{
						*(ptr++) = '-';
					}
					ptr += GuidEx.HexsToChars(ptr, (int)this._d, (int)this._e);
					if (flag)
					{
						*(ptr++) = '-';
					}
					ptr += GuidEx.HexsToChars(ptr, (int)this._f, (int)this._g);
					ptr += GuidEx.HexsToChars(ptr, (int)this._h, (int)this._i);
					ptr += GuidEx.HexsToChars(ptr, (int)this._j, (int)this._k);
				}
				if (num != 0)
				{
					*(ptr++) = (char)(num >> 16);
				}
			}
			charsWritten = num2;
			return true;
		}

		// Token: 0x04000004 RID: 4
		private int _a;

		// Token: 0x04000005 RID: 5
		private short _b;

		// Token: 0x04000006 RID: 6
		private short _c;

		// Token: 0x04000007 RID: 7
		private byte _d;

		// Token: 0x04000008 RID: 8
		private byte _e;

		// Token: 0x04000009 RID: 9
		private byte _f;

		// Token: 0x0400000A RID: 10
		private byte _g;

		// Token: 0x0400000B RID: 11
		private byte _h;

		// Token: 0x0400000C RID: 12
		private byte _i;

		// Token: 0x0400000D RID: 13
		private byte _j;

		// Token: 0x0400000E RID: 14
		private byte _k;
	}
}
