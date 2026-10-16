using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.CodeAnalysis;

namespace Roslyn.Utilities
{
	// Token: 0x02000045 RID: 69
	[NullableContext(1)]
	[Nullable(0)]
	internal static class Hash
	{
		// Token: 0x06000167 RID: 359 RVA: 0x00005B67 File Offset: 0x00003D67
		internal static int Combine(int newKey, int currentKey)
		{
			return currentKey * -1521134295 + newKey;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00005B72 File Offset: 0x00003D72
		internal static int Combine(bool newKeyPart, int currentKey)
		{
			return Hash.Combine(currentKey, newKeyPart ? 1 : 0);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00005B84 File Offset: 0x00003D84
		internal static int Combine<[Nullable(2)] T>(T newKeyPart, int currentKey) where T : class
		{
			int num = currentKey * -1521134295;
			if (newKeyPart != null)
			{
				return num + newKeyPart.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00005BB0 File Offset: 0x00003DB0
		[NullableContext(2)]
		internal static int CombineValues<T>([Nullable(new byte[] { 2, 1 })] IEnumerable<T> values, int maxItemsToHash = 2147483647)
		{
			if (values == null)
			{
				return 0;
			}
			int num = 0;
			int num2 = 0;
			foreach (T t in values)
			{
				if (num2++ >= maxItemsToHash)
				{
					break;
				}
				if (t != null)
				{
					num = Hash.Combine(t.GetHashCode(), num);
				}
			}
			return num;
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00005C24 File Offset: 0x00003E24
		[NullableContext(2)]
		internal static int CombineValues<T>([Nullable(new byte[] { 2, 1 })] T[] values, int maxItemsToHash = 2147483647)
		{
			if (values == null)
			{
				return 0;
			}
			int num = Math.Min(maxItemsToHash, values.Length);
			int num2 = 0;
			for (int i = 0; i < num; i++)
			{
				T t = values[i];
				if (t != null)
				{
					num2 = Hash.Combine(t.GetHashCode(), num2);
				}
			}
			return num2;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00005C74 File Offset: 0x00003E74
		[NullableContext(2)]
		internal static int CombineValues<T>([Nullable(new byte[] { 0, 1 })] ImmutableArray<T> values, int maxItemsToHash = 2147483647)
		{
			if (values.IsDefaultOrEmpty)
			{
				return 0;
			}
			int num = 0;
			int num2 = 0;
			foreach (T t in values)
			{
				if (num2++ >= maxItemsToHash)
				{
					break;
				}
				if (t != null)
				{
					num = Hash.Combine(t.GetHashCode(), num);
				}
			}
			return num;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00005CD0 File Offset: 0x00003ED0
		internal static int CombineValues([Nullable(2)] IEnumerable<string> values, StringComparer stringComparer, int maxItemsToHash = 2147483647)
		{
			if (values == null)
			{
				return 0;
			}
			int num = 0;
			int num2 = 0;
			foreach (string text in values)
			{
				if (num2++ >= maxItemsToHash)
				{
					break;
				}
				if (text != null)
				{
					num = Hash.Combine(stringComparer.GetHashCode(text), num);
				}
			}
			return num;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00005D38 File Offset: 0x00003F38
		internal static int GetFNVHashCode(byte[] data)
		{
			int num = -2128831035;
			for (int i = 0; i < data.Length; i++)
			{
				num = (num ^ (int)data[i]) * 16777619;
			}
			return num;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00005D68 File Offset: 0x00003F68
		[NullableContext(0)]
		internal unsafe static int GetFNVHashCode(ReadOnlySpan<byte> data, out bool isAscii)
		{
			int num = -2128831035;
			byte b = 0;
			for (int i = 0; i < data.Length; i++)
			{
				byte b2 = *data[i];
				b |= b2;
				num = (num ^ (int)b2) * 16777619;
			}
			isAscii = (b & 128) == 0;
			return num;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00005DB8 File Offset: 0x00003FB8
		[NullableContext(0)]
		internal static int GetFNVHashCode(ImmutableArray<byte> data)
		{
			int num = -2128831035;
			for (int i = 0; i < data.Length; i++)
			{
				num = (num ^ (int)data[i]) * 16777619;
			}
			return num;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00005DF0 File Offset: 0x00003FF0
		[NullableContext(0)]
		internal unsafe static int GetFNVHashCode(ReadOnlySpan<char> data)
		{
			int num = -2128831035;
			for (int i = 0; i < data.Length; i++)
			{
				num = (num ^ (int)(*data[i])) * 16777619;
			}
			return num;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00005E28 File Offset: 0x00004028
		internal static int GetFNVHashCode(string text, int start, int length)
		{
			return Hash.GetFNVHashCode(MemoryExtensions.AsSpan(text, start, length));
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00005E37 File Offset: 0x00004037
		internal static int GetCaseInsensitiveFNVHashCode(string text)
		{
			return Hash.GetCaseInsensitiveFNVHashCode(MemoryExtensions.AsSpan(text, 0, text.Length));
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00005E4C File Offset: 0x0000404C
		[NullableContext(0)]
		internal unsafe static int GetCaseInsensitiveFNVHashCode(ReadOnlySpan<char> data)
		{
			int num = -2128831035;
			for (int i = 0; i < data.Length; i++)
			{
				num = (num ^ (int)CaseInsensitiveComparison.ToLower((char)(*data[i]))) * 16777619;
			}
			return num;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00005E89 File Offset: 0x00004089
		internal static int GetFNVHashCode(string text, int start)
		{
			return Hash.GetFNVHashCode(text, start, text.Length - start);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00005E9A File Offset: 0x0000409A
		internal static int GetFNVHashCode(string text)
		{
			return Hash.CombineFNVHash(-2128831035, text);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00005EA8 File Offset: 0x000040A8
		internal static int GetFNVHashCode(StringBuilder text)
		{
			int num = -2128831035;
			int length = text.Length;
			for (int i = 0; i < length; i++)
			{
				num = (num ^ (int)text.get_Chars(i)) * 16777619;
			}
			return num;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00005EE0 File Offset: 0x000040E0
		internal static int GetFNVHashCode(char[] text, int start, int length)
		{
			int num = -2128831035;
			int num2 = start + length;
			for (int i = start; i < num2; i++)
			{
				num = (num ^ (int)text[i]) * 16777619;
			}
			return num;
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00005F10 File Offset: 0x00004110
		internal static int GetFNVHashCode(char ch)
		{
			return Hash.CombineFNVHash(-2128831035, ch);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00005F20 File Offset: 0x00004120
		internal static int CombineFNVHash(int hashCode, string text)
		{
			for (int i = 0; i < text.Length; i++)
			{
				char c = text.get_Chars(i);
				hashCode = (hashCode ^ (int)c) * 16777619;
			}
			return hashCode;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00005F54 File Offset: 0x00004154
		internal static int CombineFNVHash(int hashCode, char ch)
		{
			return (hashCode ^ (int)ch) * 16777619;
		}

		// Token: 0x0400006F RID: 111
		internal const int FnvOffsetBias = -2128831035;

		// Token: 0x04000070 RID: 112
		internal const int FnvPrime = 16777619;
	}
}
