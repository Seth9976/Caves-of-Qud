using System;

namespace Mono.Cecil.PE
{
	// Token: 0x02000294 RID: 660
	internal sealed class TextMap
	{
		// Token: 0x060010D4 RID: 4308 RVA: 0x00036831 File Offset: 0x00034A31
		public void AddMap(TextSegment segment, int length)
		{
			this.map[(int)segment] = new Range(this.GetStart(segment), (uint)length);
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x0003684C File Offset: 0x00034A4C
		public void AddMap(TextSegment segment, int length, int align)
		{
			align--;
			this.AddMap(segment, (length + align) & ~align);
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x00036860 File Offset: 0x00034A60
		public void AddMap(TextSegment segment, Range range)
		{
			this.map[(int)segment] = range;
		}

		// Token: 0x060010D7 RID: 4311 RVA: 0x0003686F File Offset: 0x00034A6F
		public Range GetRange(TextSegment segment)
		{
			return this.map[(int)segment];
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x00036880 File Offset: 0x00034A80
		public DataDirectory GetDataDirectory(TextSegment segment)
		{
			Range range = this.map[(int)segment];
			return new DataDirectory((range.Length == 0U) ? 0U : range.Start, range.Length);
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x000368B6 File Offset: 0x00034AB6
		public uint GetRVA(TextSegment segment)
		{
			return this.map[(int)segment].Start;
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x000368CC File Offset: 0x00034ACC
		public uint GetNextRVA(TextSegment segment)
		{
			return this.map[(int)segment].Start + this.map[(int)segment].Length;
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x000368FE File Offset: 0x00034AFE
		public int GetLength(TextSegment segment)
		{
			return (int)this.map[(int)segment].Length;
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x00036914 File Offset: 0x00034B14
		private uint GetStart(TextSegment segment)
		{
			if (segment != TextSegment.ImportAddressTable)
			{
				return this.ComputeStart((int)segment);
			}
			return 8192U;
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x00036933 File Offset: 0x00034B33
		private uint ComputeStart(int index)
		{
			index--;
			return this.map[index].Start + this.map[index].Length;
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x00036960 File Offset: 0x00034B60
		public uint GetLength()
		{
			Range range = this.map[16];
			return range.Start - 8192U + range.Length;
		}

		// Token: 0x04000640 RID: 1600
		private readonly Range[] map = new Range[17];
	}
}
