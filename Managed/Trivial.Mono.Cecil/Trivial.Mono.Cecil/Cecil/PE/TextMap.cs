using System;

namespace Trivial.Mono.Cecil.PE
{
	// Token: 0x020000D7 RID: 215
	internal sealed class TextMap
	{
		// Token: 0x0600094F RID: 2383 RVA: 0x0001F757 File Offset: 0x0001D957
		public void AddMap(TextSegment segment, int length)
		{
			this.map[(int)segment] = new Range(this.GetStart(segment), (uint)length);
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x0001F772 File Offset: 0x0001D972
		public void AddMap(TextSegment segment, int length, int align)
		{
			align--;
			this.AddMap(segment, (length + align) & ~align);
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x0001F786 File Offset: 0x0001D986
		public void AddMap(TextSegment segment, Range range)
		{
			this.map[(int)segment] = range;
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x0001F795 File Offset: 0x0001D995
		public Range GetRange(TextSegment segment)
		{
			return this.map[(int)segment];
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x0001F7A4 File Offset: 0x0001D9A4
		public DataDirectory GetDataDirectory(TextSegment segment)
		{
			Range range = this.map[(int)segment];
			return new DataDirectory((range.Length != 0U) ? range.Start : 0U, range.Length);
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x0001F7DA File Offset: 0x0001D9DA
		public uint GetRVA(TextSegment segment)
		{
			return this.map[(int)segment].Start;
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x0001F7ED File Offset: 0x0001D9ED
		public uint GetNextRVA(TextSegment segment)
		{
			return this.map[(int)segment].Start + this.map[(int)segment].Length;
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x0001F812 File Offset: 0x0001DA12
		public int GetLength(TextSegment segment)
		{
			return (int)this.map[(int)segment].Length;
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x0001F825 File Offset: 0x0001DA25
		private uint GetStart(TextSegment segment)
		{
			if (segment != TextSegment.ImportAddressTable)
			{
				return this.ComputeStart((int)segment);
			}
			return 8192U;
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x0001F837 File Offset: 0x0001DA37
		private uint ComputeStart(int index)
		{
			index--;
			return this.map[index].Start + this.map[index].Length;
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x0001F864 File Offset: 0x0001DA64
		public uint GetLength()
		{
			Range range = this.map[16];
			return range.Start - 8192U + range.Length;
		}

		// Token: 0x04000367 RID: 871
		private readonly Range[] map = new Range[17];
	}
}
