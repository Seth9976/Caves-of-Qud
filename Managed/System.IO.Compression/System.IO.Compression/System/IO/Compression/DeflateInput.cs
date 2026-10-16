using System;

namespace System.IO.Compression
{
	// Token: 0x02000016 RID: 22
	internal sealed class DeflateInput
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000068 RID: 104 RVA: 0x000036BB File Offset: 0x000018BB
		// (set) Token: 0x06000069 RID: 105 RVA: 0x000036C3 File Offset: 0x000018C3
		internal byte[] Buffer { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600006A RID: 106 RVA: 0x000036CC File Offset: 0x000018CC
		// (set) Token: 0x0600006B RID: 107 RVA: 0x000036D4 File Offset: 0x000018D4
		internal int Count { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600006C RID: 108 RVA: 0x000036DD File Offset: 0x000018DD
		// (set) Token: 0x0600006D RID: 109 RVA: 0x000036E5 File Offset: 0x000018E5
		internal int StartIndex { get; set; }

		// Token: 0x0600006E RID: 110 RVA: 0x000036EE File Offset: 0x000018EE
		internal void ConsumeBytes(int n)
		{
			this.StartIndex += n;
			this.Count -= n;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x0000370C File Offset: 0x0000190C
		internal DeflateInput.InputState DumpState()
		{
			return new DeflateInput.InputState(this.Count, this.StartIndex);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x0000371F File Offset: 0x0000191F
		internal void RestoreState(DeflateInput.InputState state)
		{
			this.Count = state._count;
			this.StartIndex = state._startIndex;
		}

		// Token: 0x02000017 RID: 23
		internal readonly struct InputState
		{
			// Token: 0x06000072 RID: 114 RVA: 0x00003739 File Offset: 0x00001939
			internal InputState(int count, int startIndex)
			{
				this._count = count;
				this._startIndex = startIndex;
			}

			// Token: 0x040000BE RID: 190
			internal readonly int _count;

			// Token: 0x040000BF RID: 191
			internal readonly int _startIndex;
		}
	}
}
