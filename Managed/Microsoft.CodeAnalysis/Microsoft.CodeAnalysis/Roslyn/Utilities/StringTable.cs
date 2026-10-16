using System;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Roslyn.Utilities
{
	// Token: 0x02000065 RID: 101
	[NullableContext(1)]
	[Nullable(0)]
	internal class StringTable
	{
		// Token: 0x0600023B RID: 571 RVA: 0x0000787A File Offset: 0x00005A7A
		internal StringTable()
			: this(null)
		{
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00007883 File Offset: 0x00005A83
		private StringTable([Nullable(new byte[] { 2, 1 })] ObjectPool<StringTable> pool)
		{
			this._pool = pool;
		}

		// Token: 0x0600023D RID: 573 RVA: 0x000078AD File Offset: 0x00005AAD
		private static ObjectPool<StringTable> CreatePool()
		{
			return new ObjectPool<StringTable>((ObjectPool<StringTable> pool) => new StringTable(pool), Environment.ProcessorCount * 2);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x000078DA File Offset: 0x00005ADA
		public static StringTable GetInstance()
		{
			return StringTable.s_staticPool.Allocate();
		}

		// Token: 0x0600023F RID: 575 RVA: 0x000078E6 File Offset: 0x00005AE6
		public void Free()
		{
			ObjectPool<StringTable> pool = this._pool;
			if (pool == null)
			{
				return;
			}
			pool.Free(this);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x000078FC File Offset: 0x00005AFC
		internal string Add(char[] chars, int start, int len)
		{
			Span<char> span = MemoryExtensions.AsSpan<char>(chars, start, len);
			int fnvhashCode = Hash.GetFNVHashCode(chars, start, len);
			StringTable.Entry[] localTable = this._localTable;
			int num = StringTable.LocalIdxFromHash(fnvhashCode);
			if (localTable[num].Text != null && localTable[num].HashCode == fnvhashCode)
			{
				string text = localTable[num].Text;
				if (StringTable.TextEquals(text, span))
				{
					return text;
				}
			}
			string text2 = StringTable.FindSharedEntry(chars, start, len, fnvhashCode);
			if (text2 != null)
			{
				localTable[num].HashCode = fnvhashCode;
				localTable[num].Text = text2;
				return text2;
			}
			return this.AddItem(chars, start, len, fnvhashCode);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x000079A0 File Offset: 0x00005BA0
		internal string Add(string chars, int start, int len)
		{
			int fnvhashCode = Hash.GetFNVHashCode(chars, start, len);
			StringTable.Entry[] localTable = this._localTable;
			int num = StringTable.LocalIdxFromHash(fnvhashCode);
			if (localTable[num].Text != null && localTable[num].HashCode == fnvhashCode)
			{
				string text = localTable[num].Text;
				if (StringTable.TextEquals(text, chars, start, len))
				{
					return text;
				}
			}
			string text2 = StringTable.FindSharedEntry(chars, start, len, fnvhashCode);
			if (text2 != null)
			{
				localTable[num].HashCode = fnvhashCode;
				localTable[num].Text = text2;
				return text2;
			}
			return this.AddItem(chars, start, len, fnvhashCode);
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00007A34 File Offset: 0x00005C34
		internal string Add(char chars)
		{
			int fnvhashCode = Hash.GetFNVHashCode(chars);
			StringTable.Entry[] localTable = this._localTable;
			int num = StringTable.LocalIdxFromHash(fnvhashCode);
			string text = localTable[num].Text;
			if (text != null)
			{
				string text2 = localTable[num].Text;
				if (text.Length == 1 && text.get_Chars(0) == chars)
				{
					return text2;
				}
			}
			string text3 = StringTable.FindSharedEntry(chars, fnvhashCode);
			if (text3 != null)
			{
				localTable[num].HashCode = fnvhashCode;
				localTable[num].Text = text3;
				return text3;
			}
			return this.AddItem(chars, fnvhashCode);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00007AC0 File Offset: 0x00005CC0
		internal string Add(StringBuilder chars)
		{
			int fnvhashCode = Hash.GetFNVHashCode(chars);
			StringTable.Entry[] localTable = this._localTable;
			int num = StringTable.LocalIdxFromHash(fnvhashCode);
			if (localTable[num].Text != null && localTable[num].HashCode == fnvhashCode)
			{
				string text = localTable[num].Text;
				if (StringTable.TextEquals(text, chars))
				{
					return text;
				}
			}
			string text2 = StringTable.FindSharedEntry(chars, fnvhashCode);
			if (text2 != null)
			{
				localTable[num].HashCode = fnvhashCode;
				localTable[num].Text = text2;
				return text2;
			}
			return this.AddItem(chars, fnvhashCode);
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00007B4C File Offset: 0x00005D4C
		internal string Add(string chars)
		{
			int fnvhashCode = Hash.GetFNVHashCode(chars);
			StringTable.Entry[] localTable = this._localTable;
			int num = StringTable.LocalIdxFromHash(fnvhashCode);
			if (localTable[num].Text != null && localTable[num].HashCode == fnvhashCode)
			{
				string text = localTable[num].Text;
				if (text == chars)
				{
					return text;
				}
			}
			string text2 = StringTable.FindSharedEntry(chars, fnvhashCode);
			if (text2 != null)
			{
				localTable[num].HashCode = fnvhashCode;
				localTable[num].Text = text2;
				return text2;
			}
			this.AddCore(chars, fnvhashCode);
			return chars;
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00007BD8 File Offset: 0x00005DD8
		[return: Nullable(2)]
		private static string FindSharedEntry(char[] chars, int start, int len, int hashCode)
		{
			StringTable.Entry[] array = StringTable.s_sharedTable;
			int num = StringTable.SharedIdxFromHash(hashCode);
			string text = null;
			for (int i = 1; i < 17; i++)
			{
				text = array[num].Text;
				int hashCode2 = array[num].HashCode;
				if (text == null || (hashCode2 == hashCode && StringTable.TextEquals(text, MemoryExtensions.AsSpan<char>(chars, start, len))))
				{
					break;
				}
				text = null;
				num = (num + i) & 65535;
			}
			return text;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00007C48 File Offset: 0x00005E48
		[return: Nullable(2)]
		private static string FindSharedEntry(string chars, int start, int len, int hashCode)
		{
			StringTable.Entry[] array = StringTable.s_sharedTable;
			int num = StringTable.SharedIdxFromHash(hashCode);
			string text = null;
			for (int i = 1; i < 17; i++)
			{
				text = array[num].Text;
				int hashCode2 = array[num].HashCode;
				if (text == null || (hashCode2 == hashCode && StringTable.TextEquals(text, chars, start, len)))
				{
					break;
				}
				text = null;
				num = (num + i) & 65535;
			}
			return text;
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00007CAC File Offset: 0x00005EAC
		[NullableContext(0)]
		[return: Nullable(2)]
		private static string FindSharedEntryASCII(int hashCode, ReadOnlySpan<byte> asciiChars)
		{
			StringTable.Entry[] array = StringTable.s_sharedTable;
			int num = StringTable.SharedIdxFromHash(hashCode);
			string text = null;
			for (int i = 1; i < 17; i++)
			{
				text = array[num].Text;
				int hashCode2 = array[num].HashCode;
				if (text == null || (hashCode2 == hashCode && StringTable.TextEqualsASCII(text, asciiChars)))
				{
					break;
				}
				text = null;
				num = (num + i) & 65535;
			}
			return text;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00007D10 File Offset: 0x00005F10
		[NullableContext(2)]
		private static string FindSharedEntry(char chars, int hashCode)
		{
			StringTable.Entry[] array = StringTable.s_sharedTable;
			int num = StringTable.SharedIdxFromHash(hashCode);
			string text = null;
			for (int i = 1; i < 17; i++)
			{
				text = array[num].Text;
				if (text == null || (text.Length == 1 && text.get_Chars(0) == chars))
				{
					break;
				}
				text = null;
				num = (num + i) & 65535;
			}
			return text;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00007D6C File Offset: 0x00005F6C
		[return: Nullable(2)]
		private static string FindSharedEntry(StringBuilder chars, int hashCode)
		{
			StringTable.Entry[] array = StringTable.s_sharedTable;
			int num = StringTable.SharedIdxFromHash(hashCode);
			string text = null;
			for (int i = 1; i < 17; i++)
			{
				text = array[num].Text;
				int hashCode2 = array[num].HashCode;
				if (text == null || (hashCode2 == hashCode && StringTable.TextEquals(text, chars)))
				{
					break;
				}
				text = null;
				num = (num + i) & 65535;
			}
			return text;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00007DD0 File Offset: 0x00005FD0
		[return: Nullable(2)]
		private static string FindSharedEntry(string chars, int hashCode)
		{
			StringTable.Entry[] array = StringTable.s_sharedTable;
			int num = StringTable.SharedIdxFromHash(hashCode);
			string text = null;
			for (int i = 1; i < 17; i++)
			{
				text = array[num].Text;
				int hashCode2 = array[num].HashCode;
				if (text == null || (hashCode2 == hashCode && text == chars))
				{
					break;
				}
				text = null;
				num = (num + i) & 65535;
			}
			return text;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00007E34 File Offset: 0x00006034
		private string AddItem(char[] chars, int start, int len, int hashCode)
		{
			string text = new string(chars, start, len);
			this.AddCore(text, hashCode);
			return text;
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00007E54 File Offset: 0x00006054
		private string AddItem(string chars, int start, int len, int hashCode)
		{
			string text = chars.Substring(start, len);
			this.AddCore(text, hashCode);
			return text;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00007E74 File Offset: 0x00006074
		private string AddItem(char chars, int hashCode)
		{
			string text = new string(chars, 1);
			this.AddCore(text, hashCode);
			return text;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00007E94 File Offset: 0x00006094
		private string AddItem(StringBuilder chars, int hashCode)
		{
			string text = chars.ToString();
			this.AddCore(text, hashCode);
			return text;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00007EB4 File Offset: 0x000060B4
		private void AddCore(string chars, int hashCode)
		{
			this.AddSharedEntry(hashCode, chars);
			StringTable.Entry[] localTable = this._localTable;
			int num = StringTable.LocalIdxFromHash(hashCode);
			localTable[num].HashCode = hashCode;
			localTable[num].Text = chars;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00007EF0 File Offset: 0x000060F0
		private void AddSharedEntry(int hashCode, string text)
		{
			StringTable.Entry[] array = StringTable.s_sharedTable;
			int num = StringTable.SharedIdxFromHash(hashCode);
			int num2 = num;
			for (int i = 1; i < 17; i++)
			{
				if (array[num2].Text == null)
				{
					num = num2;
					IL_0057:
					array[num].HashCode = hashCode;
					Volatile.Write<string>(ref array[num].Text, text);
					return;
				}
				num2 = (num2 + i) & 65535;
			}
			int num3 = this.LocalNextRandom() & 15;
			num = (num + (num3 * num3 + num3) / 2) & 65535;
			goto IL_0057;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00007F74 File Offset: 0x00006174
		internal static string AddShared(StringBuilder chars)
		{
			int fnvhashCode = Hash.GetFNVHashCode(chars);
			string text = StringTable.FindSharedEntry(chars, fnvhashCode);
			if (text != null)
			{
				return text;
			}
			return StringTable.AddSharedSlow(fnvhashCode, chars);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00007F9C File Offset: 0x0000619C
		private static string AddSharedSlow(int hashCode, StringBuilder builder)
		{
			string text = builder.ToString();
			StringTable.AddSharedSlow(hashCode, text);
			return text;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00007FB8 File Offset: 0x000061B8
		[NullableContext(0)]
		[return: Nullable(1)]
		internal static string AddSharedUTF8(ReadOnlySpan<byte> bytes)
		{
			bool flag;
			int fnvhashCode = Hash.GetFNVHashCode(bytes, out flag);
			if (flag)
			{
				string text = StringTable.FindSharedEntryASCII(fnvhashCode, bytes);
				if (text != null)
				{
					return text;
				}
			}
			return StringTable.AddSharedSlow(fnvhashCode, bytes, flag);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00007FE8 File Offset: 0x000061E8
		[NullableContext(0)]
		[return: Nullable(1)]
		private unsafe static string AddSharedSlow(int hashCode, ReadOnlySpan<byte> utf8Bytes, bool isAscii)
		{
			string @string;
			fixed (byte* pinnableReference = utf8Bytes.GetPinnableReference())
			{
				byte* ptr = pinnableReference;
				@string = Encoding.UTF8.GetString(ptr, utf8Bytes.Length);
			}
			if (isAscii)
			{
				StringTable.AddSharedSlow(hashCode, @string);
			}
			return @string;
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00008024 File Offset: 0x00006224
		private static void AddSharedSlow(int hashCode, string text)
		{
			StringTable.Entry[] array = StringTable.s_sharedTable;
			int num = StringTable.SharedIdxFromHash(hashCode);
			int num2 = num;
			for (int i = 1; i < 17; i++)
			{
				if (array[num2].Text == null)
				{
					num = num2;
					IL_0056:
					array[num].HashCode = hashCode;
					Volatile.Write<string>(ref array[num].Text, text);
					return;
				}
				num2 = (num2 + i) & 65535;
			}
			int num3 = StringTable.SharedNextRandom() & 15;
			num = (num + (num3 * num3 + num3) / 2) & 65535;
			goto IL_0056;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x000080A6 File Offset: 0x000062A6
		private static int LocalIdxFromHash(int hash)
		{
			return hash & 2047;
		}

		// Token: 0x06000257 RID: 599 RVA: 0x000080AF File Offset: 0x000062AF
		private static int SharedIdxFromHash(int hash)
		{
			return (hash ^ (hash >> 11)) & 65535;
		}

		// Token: 0x06000258 RID: 600 RVA: 0x000080C0 File Offset: 0x000062C0
		private int LocalNextRandom()
		{
			int localRandom = this._localRandom;
			this._localRandom = localRandom + 1;
			return localRandom;
		}

		// Token: 0x06000259 RID: 601 RVA: 0x000080DE File Offset: 0x000062DE
		private static int SharedNextRandom()
		{
			return Interlocked.Increment(ref StringTable.s_sharedRandom);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x000080EC File Offset: 0x000062EC
		internal static bool TextEquals(string array, string text, int start, int length)
		{
			if (array.Length != length)
			{
				return false;
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (array.get_Chars(i) != text.get_Chars(start + i))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000812C File Offset: 0x0000632C
		internal static bool TextEquals(string array, StringBuilder text)
		{
			if (array.Length != text.Length)
			{
				return false;
			}
			for (int i = array.Length - 1; i >= 0; i--)
			{
				if (array.get_Chars(i) != text.get_Chars(i))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00008170 File Offset: 0x00006370
		[NullableContext(0)]
		internal unsafe static bool TextEqualsASCII([Nullable(1)] string text, ReadOnlySpan<byte> ascii)
		{
			if (ascii.Length != text.Length)
			{
				return false;
			}
			for (int i = 0; i < ascii.Length; i++)
			{
				if ((char)(*ascii[i]) != text.get_Chars(i))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600025D RID: 605 RVA: 0x000081B5 File Offset: 0x000063B5
		[NullableContext(0)]
		internal static bool TextEquals([Nullable(1)] string array, ReadOnlySpan<char> text)
		{
			return MemoryExtensions.Equals(text, MemoryExtensions.AsSpan(array), 4);
		}

		// Token: 0x04000090 RID: 144
		private const int LocalSizeBits = 11;

		// Token: 0x04000091 RID: 145
		private const int LocalSize = 2048;

		// Token: 0x04000092 RID: 146
		private const int LocalSizeMask = 2047;

		// Token: 0x04000093 RID: 147
		private const int SharedSizeBits = 16;

		// Token: 0x04000094 RID: 148
		private const int SharedSize = 65536;

		// Token: 0x04000095 RID: 149
		private const int SharedSizeMask = 65535;

		// Token: 0x04000096 RID: 150
		private const int SharedBucketBits = 4;

		// Token: 0x04000097 RID: 151
		private const int SharedBucketSize = 16;

		// Token: 0x04000098 RID: 152
		private const int SharedBucketSizeMask = 15;

		// Token: 0x04000099 RID: 153
		private readonly StringTable.Entry[] _localTable = new StringTable.Entry[2048];

		// Token: 0x0400009A RID: 154
		private static readonly StringTable.Entry[] s_sharedTable = new StringTable.Entry[65536];

		// Token: 0x0400009B RID: 155
		private int _localRandom = Environment.TickCount;

		// Token: 0x0400009C RID: 156
		private static int s_sharedRandom = Environment.TickCount;

		// Token: 0x0400009D RID: 157
		[Nullable(new byte[] { 2, 1 })]
		private readonly ObjectPool<StringTable> _pool;

		// Token: 0x0400009E RID: 158
		private static readonly ObjectPool<StringTable> s_staticPool = StringTable.CreatePool();

		// Token: 0x0200050D RID: 1293
		[NullableContext(0)]
		private struct Entry
		{
			// Token: 0x0400141B RID: 5147
			public int HashCode;

			// Token: 0x0400141C RID: 5148
			[Nullable(1)]
			public string Text;
		}
	}
}
