using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x0200013C RID: 316
	internal readonly struct StandAloneSigTableReader
	{
		// Token: 0x06000AAF RID: 2735 RVA: 0x0001F8F5 File Offset: 0x0001DAF5
		internal StandAloneSigTableReader(int numberOfRows, int blobHeapRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsBlobHeapRefSizeSmall = blobHeapRefSize == 2;
			this._SignatureOffset = 0;
			this.RowSize = this._SignatureOffset + blobHeapRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x0001F934 File Offset: 0x0001DB34
		internal BlobHandle GetSignature(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return BlobHandle.FromOffset(this.Block.PeekHeapReference(num + this._SignatureOffset, this._IsBlobHeapRefSizeSmall));
		}

		// Token: 0x040007D1 RID: 2001
		internal readonly int NumberOfRows;

		// Token: 0x040007D2 RID: 2002
		private readonly bool _IsBlobHeapRefSizeSmall;

		// Token: 0x040007D3 RID: 2003
		private readonly int _SignatureOffset;

		// Token: 0x040007D4 RID: 2004
		internal readonly int RowSize;

		// Token: 0x040007D5 RID: 2005
		internal readonly MemoryBlock Block;
	}
}
