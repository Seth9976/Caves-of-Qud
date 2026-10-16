using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000168 RID: 360
	internal readonly struct LocalConstantTableReader
	{
		// Token: 0x06000B46 RID: 2886 RVA: 0x000224E8 File Offset: 0x000206E8
		internal LocalConstantTableReader(int numberOfRows, int stringHeapRefSize, int blobHeapRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._isStringHeapRefSizeSmall = stringHeapRefSize == 2;
			this._isBlobHeapRefSizeSmall = blobHeapRefSize == 2;
			this._signatureOffset = stringHeapRefSize;
			this.RowSize = this._signatureOffset + blobHeapRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x0002253C File Offset: 0x0002073C
		internal StringHandle GetName(LocalConstantHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return StringHandle.FromOffset(this.Block.PeekHeapReference(num, this._isStringHeapRefSizeSmall));
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x00022574 File Offset: 0x00020774
		internal BlobHandle GetSignature(LocalConstantHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return BlobHandle.FromOffset(this.Block.PeekHeapReference(num + this._signatureOffset, this._isBlobHeapRefSizeSmall));
		}

		// Token: 0x04000923 RID: 2339
		internal readonly int NumberOfRows;

		// Token: 0x04000924 RID: 2340
		private readonly bool _isStringHeapRefSizeSmall;

		// Token: 0x04000925 RID: 2341
		private readonly bool _isBlobHeapRefSizeSmall;

		// Token: 0x04000926 RID: 2342
		private const int NameOffset = 0;

		// Token: 0x04000927 RID: 2343
		private readonly int _signatureOffset;

		// Token: 0x04000928 RID: 2344
		internal readonly int RowSize;

		// Token: 0x04000929 RID: 2345
		internal readonly MemoryBlock Block;
	}
}
