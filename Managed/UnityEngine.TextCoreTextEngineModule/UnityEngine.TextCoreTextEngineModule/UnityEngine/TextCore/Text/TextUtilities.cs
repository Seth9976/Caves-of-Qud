using System;
using System.Collections.Generic;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000071 RID: 113
	internal static class TextUtilities
	{
		// Token: 0x06000378 RID: 888 RVA: 0x0002779C File Offset: 0x0002599C
		internal static void ResizeArray<T>(ref T[] array)
		{
			int num = TextUtilities.NextPowerOfTwo(array.Length);
			Array.Resize<T>(ref array, num);
		}

		// Token: 0x06000379 RID: 889 RVA: 0x000277BC File Offset: 0x000259BC
		internal static void ResizeArray<T>(ref T[] array, int size)
		{
			size = TextUtilities.NextPowerOfTwo(size);
			Array.Resize<T>(ref array, size);
		}

		// Token: 0x0600037A RID: 890 RVA: 0x000277D0 File Offset: 0x000259D0
		internal static int NextPowerOfTwo(int v)
		{
			v |= v >> 16;
			v |= v >> 8;
			v |= v >> 4;
			v |= v >> 2;
			v |= v >> 1;
			return v + 1;
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0002780C File Offset: 0x00025A0C
		internal static char ToLowerFast(char c)
		{
			bool flag = (int)c > "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@abcdefghijklmnopqrstuvwxyz[-]^_`abcdefghijklmnopqrstuvwxyz{|}~-".Length - 1;
			char c2;
			if (flag)
			{
				c2 = c;
			}
			else
			{
				c2 = "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@abcdefghijklmnopqrstuvwxyz[-]^_`abcdefghijklmnopqrstuvwxyz{|}~-"[(int)c];
			}
			return c2;
		}

		// Token: 0x0600037C RID: 892 RVA: 0x00027840 File Offset: 0x00025A40
		internal static char ToUpperFast(char c)
		{
			bool flag = (int)c > "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[-]^_`ABCDEFGHIJKLMNOPQRSTUVWXYZ{|}~-".Length - 1;
			char c2;
			if (flag)
			{
				c2 = c;
			}
			else
			{
				c2 = "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[-]^_`ABCDEFGHIJKLMNOPQRSTUVWXYZ{|}~-"[(int)c];
			}
			return c2;
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00027874 File Offset: 0x00025A74
		internal static uint ToUpperASCIIFast(uint c)
		{
			bool flag = (ulong)c > (ulong)((long)("-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[-]^_`ABCDEFGHIJKLMNOPQRSTUVWXYZ{|}~-".Length - 1));
			uint num;
			if (flag)
			{
				num = c;
			}
			else
			{
				num = (uint)"-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[-]^_`ABCDEFGHIJKLMNOPQRSTUVWXYZ{|}~-"[(int)c];
			}
			return num;
		}

		// Token: 0x0600037E RID: 894 RVA: 0x000278AC File Offset: 0x00025AAC
		internal static uint ToLowerASCIIFast(uint c)
		{
			bool flag = (ulong)c > (ulong)((long)("-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@abcdefghijklmnopqrstuvwxyz[-]^_`abcdefghijklmnopqrstuvwxyz{|}~-".Length - 1));
			uint num;
			if (flag)
			{
				num = c;
			}
			else
			{
				num = (uint)"-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@abcdefghijklmnopqrstuvwxyz[-]^_`abcdefghijklmnopqrstuvwxyz{|}~-"[(int)c];
			}
			return num;
		}

		// Token: 0x0600037F RID: 895 RVA: 0x000278E4 File Offset: 0x00025AE4
		public static int GetHashCodeCaseSensitive(string s)
		{
			int num = 0;
			for (int i = 0; i < s.Length; i++)
			{
				num = ((num << 5) + num) ^ (int)s[i];
			}
			return num;
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0002791C File Offset: 0x00025B1C
		public static int GetHashCodeCaseInSensitive(string s)
		{
			int num = 0;
			for (int i = 0; i < s.Length; i++)
			{
				num = ((num << 5) + num) ^ (int)TextUtilities.ToUpperFast(s[i]);
			}
			return num;
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0002795C File Offset: 0x00025B5C
		public static int GetHashCode(string s)
		{
			bool flag = string.IsNullOrEmpty(s);
			int num;
			if (flag)
			{
				num = 0;
			}
			else
			{
				int num2 = 0;
				for (int i = 0; i < s.Length; i++)
				{
					num2 = ((num2 << 5) + num2) ^ (int)TextUtilities.ToUpperFast(s[i]);
				}
				num = num2;
			}
			return num;
		}

		// Token: 0x06000382 RID: 898 RVA: 0x000279AC File Offset: 0x00025BAC
		public static int GetSimpleHashCode(string s)
		{
			int num = 0;
			for (int i = 0; i < s.Length; i++)
			{
				num = ((num << 5) + num) ^ (int)s[i];
			}
			return num;
		}

		// Token: 0x06000383 RID: 899 RVA: 0x000279E4 File Offset: 0x00025BE4
		public static uint GetSimpleHashCodeLowercase(string s)
		{
			uint num = 0U;
			for (int i = 0; i < s.Length; i++)
			{
				num = ((num << 5) + num) ^ (uint)TextUtilities.ToLowerFast(s[i]);
			}
			return num;
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00027A24 File Offset: 0x00025C24
		internal static uint ConvertToUTF32(uint highSurrogate, uint lowSurrogate)
		{
			return (highSurrogate - 55296U) * 1024U + (lowSurrogate - 56320U + 65536U);
		}

		// Token: 0x06000385 RID: 901 RVA: 0x00027A54 File Offset: 0x00025C54
		internal static uint ReadUTF16(uint[] text, int index)
		{
			uint num = 0U;
			num += TextUtilities.HexToInt((char)text[index]) << 12;
			num += TextUtilities.HexToInt((char)text[index + 1]) << 8;
			num += TextUtilities.HexToInt((char)text[index + 2]) << 4;
			return num + TextUtilities.HexToInt((char)text[index + 3]);
		}

		// Token: 0x06000386 RID: 902 RVA: 0x00027AA8 File Offset: 0x00025CA8
		internal static uint ReadUTF32(uint[] text, int index)
		{
			uint num = 0U;
			num += TextUtilities.HexToInt((char)text[index]) << 30;
			num += TextUtilities.HexToInt((char)text[index + 1]) << 24;
			num += TextUtilities.HexToInt((char)text[index + 2]) << 20;
			num += TextUtilities.HexToInt((char)text[index + 3]) << 16;
			num += TextUtilities.HexToInt((char)text[index + 4]) << 12;
			num += TextUtilities.HexToInt((char)text[index + 5]) << 8;
			num += TextUtilities.HexToInt((char)text[index + 6]) << 4;
			return num + TextUtilities.HexToInt((char)text[index + 7]);
		}

		// Token: 0x06000387 RID: 903 RVA: 0x00027B40 File Offset: 0x00025D40
		private static uint HexToInt(char hex)
		{
			switch (hex)
			{
			case '0':
				return 0U;
			case '1':
				return 1U;
			case '2':
				return 2U;
			case '3':
				return 3U;
			case '4':
				return 4U;
			case '5':
				return 5U;
			case '6':
				return 6U;
			case '7':
				return 7U;
			case '8':
				return 8U;
			case '9':
				return 9U;
			case ':':
			case ';':
			case '<':
			case '=':
			case '>':
			case '?':
			case '@':
				break;
			case 'A':
				return 10U;
			case 'B':
				return 11U;
			case 'C':
				return 12U;
			case 'D':
				return 13U;
			case 'E':
				return 14U;
			case 'F':
				return 15U;
			default:
				switch (hex)
				{
				case 'a':
					return 10U;
				case 'b':
					return 11U;
				case 'c':
					return 12U;
				case 'd':
					return 13U;
				case 'e':
					return 14U;
				case 'f':
					return 15U;
				}
				break;
			}
			return 15U;
		}

		// Token: 0x06000388 RID: 904 RVA: 0x00027C48 File Offset: 0x00025E48
		public static uint StringHexToInt(string s)
		{
			uint num = 0U;
			int length = s.Length;
			for (int i = 0; i < length; i++)
			{
				num += TextUtilities.HexToInt(s[i]) * (uint)Mathf.Pow(16f, (float)(length - 1 - i));
			}
			return num;
		}

		// Token: 0x06000389 RID: 905 RVA: 0x00027C98 File Offset: 0x00025E98
		internal static string UintToString(this List<uint> unicodes)
		{
			char[] array = new char[unicodes.Count];
			for (int i = 0; i < unicodes.Count; i++)
			{
				array[i] = (char)unicodes[i];
			}
			return new string(array);
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00027CE0 File Offset: 0x00025EE0
		internal static int GetTextFontWeightIndex(TextFontWeight fontWeight)
		{
			if (fontWeight <= TextFontWeight.Regular)
			{
				if (fontWeight <= TextFontWeight.ExtraLight)
				{
					if (fontWeight == TextFontWeight.Thin)
					{
						return 1;
					}
					if (fontWeight == TextFontWeight.ExtraLight)
					{
						return 2;
					}
				}
				else
				{
					if (fontWeight == TextFontWeight.Light)
					{
						return 3;
					}
					if (fontWeight == TextFontWeight.Regular)
					{
						return 4;
					}
				}
			}
			else if (fontWeight <= TextFontWeight.SemiBold)
			{
				if (fontWeight == TextFontWeight.Medium)
				{
					return 5;
				}
				if (fontWeight == TextFontWeight.SemiBold)
				{
					return 6;
				}
			}
			else
			{
				if (fontWeight == TextFontWeight.Bold)
				{
					return 7;
				}
				if (fontWeight == TextFontWeight.Heavy)
				{
					return 8;
				}
				if (fontWeight == TextFontWeight.Black)
				{
					return 9;
				}
			}
			return 4;
		}

		// Token: 0x040004C1 RID: 1217
		private const string k_LookupStringL = "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@abcdefghijklmnopqrstuvwxyz[-]^_`abcdefghijklmnopqrstuvwxyz{|}~-";

		// Token: 0x040004C2 RID: 1218
		private const string k_LookupStringU = "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[-]^_`ABCDEFGHIJKLMNOPQRSTUVWXYZ{|}~-";
	}
}
