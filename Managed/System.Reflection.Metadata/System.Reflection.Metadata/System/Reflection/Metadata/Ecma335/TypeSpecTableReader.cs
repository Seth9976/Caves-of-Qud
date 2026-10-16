using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000146 RID: 326
	internal readonly struct TypeSpecTableReader
	{
		// Token: 0x06000AD5 RID: 2773 RVA: 0x0002028C File Offset: 0x0001E48C
		internal TypeSpecTableReader(int numberOfRows, int blobHeapRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsBlobHeapRefSizeSmall = blobHeapRefSize == 2;
			this._SignatureOffset = 0;
			this.RowSize = this._SignatureOffset + blobHeapRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x000202CC File Offset: 0x0001E4CC
		internal BlobHandle GetSignature(TypeSpecificationHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return BlobHandle.FromOffset(this.Block.PeekHeapReference(num + this._SignatureOffset, this._IsBlobHeapRefSizeSmall));
		}

		// Token: 0x04000813 RID: 2067
		internal readonly int NumberOfRows;

		// Token: 0x04000814 RID: 2068
		private readonly bool _IsBlobHeapRefSizeSmall;

		// Token: 0x04000815 RID: 2069
		private readonly int _SignatureOffset;

		// Token: 0x04000816 RID: 2070
		internal readonly int RowSize;

		// Token: 0x04000817 RID: 2071
		internal readonly MemoryBlock Block;
	}
}
