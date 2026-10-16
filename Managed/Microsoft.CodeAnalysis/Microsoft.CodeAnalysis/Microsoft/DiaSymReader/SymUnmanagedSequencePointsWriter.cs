using System;

namespace Microsoft.DiaSymReader
{
	// Token: 0x02000100 RID: 256
	internal sealed class SymUnmanagedSequencePointsWriter
	{
		// Token: 0x060008B4 RID: 2228 RVA: 0x00018F0C File Offset: 0x0001710C
		public SymUnmanagedSequencePointsWriter(SymUnmanagedWriter writer, int capacity = 64)
		{
			if (capacity <= 0)
			{
				throw new ArgumentOutOfRangeException("capacity");
			}
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			this._writer = writer;
			this._currentDocumentIndex = -1;
			this._offsets = new int[capacity];
			this._startLines = new int[capacity];
			this._startColumns = new int[capacity];
			this._endLines = new int[capacity];
			this._endColumns = new int[capacity];
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x00018F88 File Offset: 0x00017188
		private void EnsureCapacity(int length)
		{
			if (length > this._offsets.Length)
			{
				int num = Math.Max(length, (this._offsets.Length + 1) * 2);
				Array.Resize<int>(ref this._offsets, num);
				Array.Resize<int>(ref this._startLines, num);
				Array.Resize<int>(ref this._startColumns, num);
				Array.Resize<int>(ref this._endLines, num);
				Array.Resize<int>(ref this._endColumns, num);
			}
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x00018FEF File Offset: 0x000171EF
		private void Clear()
		{
			this._currentDocumentIndex = -1;
			this._count = 0;
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x00019000 File Offset: 0x00017200
		public void Add(int documentIndex, int offset, int startLine, int startColumn, int endLine, int endColumn)
		{
			if (documentIndex < 0)
			{
				throw new ArgumentOutOfRangeException("documentIndex");
			}
			if (this._currentDocumentIndex != documentIndex)
			{
				if (this._currentDocumentIndex != -1)
				{
					this.Flush();
				}
				this._currentDocumentIndex = documentIndex;
			}
			int count = this._count;
			this._count = count + 1;
			int num = count;
			this.EnsureCapacity(this._count);
			this._offsets[num] = offset;
			this._startLines[num] = startLine;
			this._startColumns[num] = startColumn;
			this._endLines[num] = endLine;
			this._endColumns[num] = endColumn;
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x0001908C File Offset: 0x0001728C
		public void Flush()
		{
			if (this._count > 0)
			{
				this._writer.DefineSequencePoints(this._currentDocumentIndex, this._count, this._offsets, this._startLines, this._startColumns, this._endLines, this._endColumns);
			}
			this.Clear();
		}

		// Token: 0x04000278 RID: 632
		private readonly SymUnmanagedWriter _writer;

		// Token: 0x04000279 RID: 633
		private int _currentDocumentIndex;

		// Token: 0x0400027A RID: 634
		private int _count;

		// Token: 0x0400027B RID: 635
		private int[] _offsets;

		// Token: 0x0400027C RID: 636
		private int[] _startLines;

		// Token: 0x0400027D RID: 637
		private int[] _startColumns;

		// Token: 0x0400027E RID: 638
		private int[] _endLines;

		// Token: 0x0400027F RID: 639
		private int[] _endColumns;
	}
}
