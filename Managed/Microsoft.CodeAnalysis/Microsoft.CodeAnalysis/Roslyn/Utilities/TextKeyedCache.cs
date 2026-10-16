using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Roslyn.Utilities
{
	// Token: 0x02000067 RID: 103
	[NullableContext(1)]
	[Nullable(0)]
	internal class TextKeyedCache<T> where T : class
	{
		// Token: 0x06000268 RID: 616 RVA: 0x000089F4 File Offset: 0x00006BF4
		internal TextKeyedCache()
			: this(null)
		{
		}

		// Token: 0x06000269 RID: 617 RVA: 0x000089FD File Offset: 0x00006BFD
		private TextKeyedCache([Nullable(new byte[] { 2, 1, 1 })] ObjectPool<TextKeyedCache<T>> pool)
		{
			this._pool = pool;
			this._strings = new StringTable();
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00008A32 File Offset: 0x00006C32
		private static ObjectPool<TextKeyedCache<T>> CreatePool()
		{
			return new ObjectPool<TextKeyedCache<T>>((ObjectPool<TextKeyedCache<T>> pool) => new TextKeyedCache<T>(pool), Environment.ProcessorCount * 4);
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00008A5F File Offset: 0x00006C5F
		public static TextKeyedCache<T> GetInstance()
		{
			return TextKeyedCache<T>.s_staticPool.Allocate();
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00008A6B File Offset: 0x00006C6B
		public void Free()
		{
			ObjectPool<TextKeyedCache<T>> pool = this._pool;
			if (pool == null)
			{
				return;
			}
			pool.Free(this);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00008A80 File Offset: 0x00006C80
		[return: Nullable(2)]
		internal T FindItem(char[] chars, int start, int len, int hashCode)
		{
			ref ValueTuple<string, int, T> ptr = ref this._localTable[TextKeyedCache<T>.LocalIdxFromHash(hashCode)];
			string item = ptr.Item1;
			if (item != null && ptr.Item2 == hashCode && StringTable.TextEquals(item, MemoryExtensions.AsSpan<char>(chars, start, len)))
			{
				return ptr.Item3;
			}
			TextKeyedCache<T>.SharedEntryValue sharedEntryValue = this.FindSharedEntry(chars, start, len, hashCode);
			if (sharedEntryValue != null)
			{
				ptr.Item2 = hashCode;
				ptr.Item1 = sharedEntryValue.Text;
				T item2 = sharedEntryValue.Item;
				ptr.Item3 = item2;
				return item2;
			}
			return default(T);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00008B10 File Offset: 0x00006D10
		[return: Nullable(new byte[] { 2, 0 })]
		private TextKeyedCache<T>.SharedEntryValue FindSharedEntry(char[] chars, int start, int len, int hashCode)
		{
			ValueTuple<int, TextKeyedCache<T>.SharedEntryValue>[] sharedTableInst = this._sharedTableInst;
			int num = TextKeyedCache<T>.SharedIdxFromHash(hashCode);
			TextKeyedCache<T>.SharedEntryValue sharedEntryValue = null;
			for (int i = 1; i < 17; i++)
			{
				ValueTuple<int, TextKeyedCache<T>.SharedEntryValue> valueTuple = sharedTableInst[num];
				int item = valueTuple.Item1;
				sharedEntryValue = valueTuple.Item2;
				if (sharedEntryValue == null || (item == hashCode && StringTable.TextEquals(sharedEntryValue.Text, MemoryExtensions.AsSpan<char>(chars, start, len))))
				{
					break;
				}
				sharedEntryValue = null;
				num = (num + i) & 65535;
			}
			return sharedEntryValue;
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00008B84 File Offset: 0x00006D84
		internal void AddItem(char[] chars, int start, int len, int hashCode, T item)
		{
			string text = this._strings.Add(chars, start, len);
			TextKeyedCache<T>.SharedEntryValue sharedEntryValue = new TextKeyedCache<T>.SharedEntryValue(text, item);
			this.AddSharedEntry(hashCode, sharedEntryValue);
			ValueTuple<string, int, T>[] localTable = this._localTable;
			int num = TextKeyedCache<T>.LocalIdxFromHash(hashCode);
			localTable[num].Item2 = hashCode;
			localTable[num].Item1 = text;
			localTable[num].Item3 = item;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00008BDC File Offset: 0x00006DDC
		private void AddSharedEntry(int hashCode, [Nullable(new byte[] { 1, 0 })] TextKeyedCache<T>.SharedEntryValue e)
		{
			ValueTuple<int, TextKeyedCache<T>.SharedEntryValue>[] sharedTableInst = this._sharedTableInst;
			int num = TextKeyedCache<T>.SharedIdxFromHash(hashCode);
			int num2 = num;
			for (int i = 1; i < 17; i++)
			{
				if (sharedTableInst[num2].Item2 == null)
				{
					num = num2;
					IL_0058:
					sharedTableInst[num].Item1 = hashCode;
					Volatile.Write<TextKeyedCache<T>.SharedEntryValue>(ref sharedTableInst[num].Item2, e);
					return;
				}
				num2 = (num2 + i) & 65535;
			}
			int num3 = this.NextRandom() & 15;
			num = (num + (num3 * num3 + num3) / 2) & 65535;
			goto IL_0058;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00008C60 File Offset: 0x00006E60
		private static int LocalIdxFromHash(int hash)
		{
			return hash & 2047;
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00008C69 File Offset: 0x00006E69
		private static int SharedIdxFromHash(int hash)
		{
			return (hash ^ (hash >> 11)) & 65535;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00008C78 File Offset: 0x00006E78
		private int NextRandom()
		{
			Random random = this._random;
			if (random != null)
			{
				return random.Next();
			}
			random = new Random();
			this._random = random;
			return random.Next();
		}

		// Token: 0x0400009F RID: 159
		private const int LocalSizeBits = 11;

		// Token: 0x040000A0 RID: 160
		private const int LocalSize = 2048;

		// Token: 0x040000A1 RID: 161
		private const int LocalSizeMask = 2047;

		// Token: 0x040000A2 RID: 162
		private const int SharedSizeBits = 16;

		// Token: 0x040000A3 RID: 163
		private const int SharedSize = 65536;

		// Token: 0x040000A4 RID: 164
		private const int SharedSizeMask = 65535;

		// Token: 0x040000A5 RID: 165
		private const int SharedBucketBits = 4;

		// Token: 0x040000A6 RID: 166
		private const int SharedBucketSize = 16;

		// Token: 0x040000A7 RID: 167
		private const int SharedBucketSizeMask = 15;

		// Token: 0x040000A8 RID: 168
		[TupleElementNames(new string[] { "Text", "HashCode", "Item" })]
		[Nullable(new byte[] { 1, 0, 1, 1 })]
		private readonly ValueTuple<string, int, T>[] _localTable = new ValueTuple<string, int, T>[2048];

		// Token: 0x040000A9 RID: 169
		[TupleElementNames(new string[] { "HashCode", "Entry" })]
		[Nullable(new byte[] { 1, 0, 1, 0 })]
		private static readonly ValueTuple<int, TextKeyedCache<T>.SharedEntryValue>[] s_sharedTable = new ValueTuple<int, TextKeyedCache<T>.SharedEntryValue>[65536];

		// Token: 0x040000AA RID: 170
		[TupleElementNames(new string[] { "HashCode", "Entry" })]
		[Nullable(new byte[] { 1, 0, 1, 0 })]
		private readonly ValueTuple<int, TextKeyedCache<T>.SharedEntryValue>[] _sharedTableInst = TextKeyedCache<T>.s_sharedTable;

		// Token: 0x040000AB RID: 171
		private readonly StringTable _strings;

		// Token: 0x040000AC RID: 172
		[Nullable(2)]
		private Random _random;

		// Token: 0x040000AD RID: 173
		[Nullable(new byte[] { 2, 1, 1 })]
		private readonly ObjectPool<TextKeyedCache<T>> _pool;

		// Token: 0x040000AE RID: 174
		private static readonly ObjectPool<TextKeyedCache<T>> s_staticPool = TextKeyedCache<T>.CreatePool();

		// Token: 0x02000511 RID: 1297
		[Nullable(0)]
		private class SharedEntryValue
		{
			// Token: 0x06003318 RID: 13080 RVA: 0x00090D9C File Offset: 0x0008EF9C
			public SharedEntryValue(string Text, T item)
			{
				this.Text = Text;
				this.Item = item;
			}

			// Token: 0x04001428 RID: 5160
			public readonly string Text;

			// Token: 0x04001429 RID: 5161
			public readonly T Item;
		}
	}
}
