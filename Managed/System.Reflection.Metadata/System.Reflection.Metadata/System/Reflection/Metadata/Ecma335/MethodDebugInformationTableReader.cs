using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000165 RID: 357
	internal readonly struct MethodDebugInformationTableReader
	{
		// Token: 0x06000B36 RID: 2870 RVA: 0x0002209C File Offset: 0x0002029C
		internal MethodDebugInformationTableReader(int numberOfRows, int documentRefSize, int blobHeapRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._isDocumentRefSmall = documentRefSize == 2;
			this._isBlobHeapRefSizeSmall = blobHeapRefSize == 2;
			this._sequencePointsOffset = documentRefSize;
			this.RowSize = this._sequencePointsOffset + blobHeapRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x000220F0 File Offset: 0x000202F0
		internal DocumentHandle GetDocument(MethodDebugInformationHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return DocumentHandle.FromRowId(this.Block.PeekReference(num, this._isDocumentRefSmall));
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x00022128 File Offset: 0x00020328
		internal BlobHandle GetSequencePoints(MethodDebugInformationHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return BlobHandle.FromOffset(this.Block.PeekHeapReference(num + this._sequencePointsOffset, this._isBlobHeapRefSizeSmall));
		}

		// Token: 0x04000908 RID: 2312
		internal readonly int NumberOfRows;

		// Token: 0x04000909 RID: 2313
		private readonly bool _isDocumentRefSmall;

		// Token: 0x0400090A RID: 2314
		private readonly bool _isBlobHeapRefSizeSmall;

		// Token: 0x0400090B RID: 2315
		private const int DocumentOffset = 0;

		// Token: 0x0400090C RID: 2316
		private readonly int _sequencePointsOffset;

		// Token: 0x0400090D RID: 2317
		internal readonly int RowSize;

		// Token: 0x0400090E RID: 2318
		internal readonly MemoryBlock Block;
	}
}
