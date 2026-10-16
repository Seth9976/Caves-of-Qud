using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x0200016A RID: 362
	internal readonly struct ImportScopeTableReader
	{
		// Token: 0x06000B4C RID: 2892 RVA: 0x00022684 File Offset: 0x00020884
		internal ImportScopeTableReader(int numberOfRows, int importScopeRefSize, int blobHeapRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._isImportScopeRefSizeSmall = importScopeRefSize == 2;
			this._isBlobHeapRefSizeSmall = blobHeapRefSize == 2;
			this._importsOffset = importScopeRefSize;
			this.RowSize = this._importsOffset + blobHeapRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x000226D8 File Offset: 0x000208D8
		internal ImportScopeHandle GetParent(ImportScopeHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return ImportScopeHandle.FromRowId(this.Block.PeekReference(num, this._isImportScopeRefSizeSmall));
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x00022710 File Offset: 0x00020910
		internal BlobHandle GetImports(ImportScopeHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return BlobHandle.FromOffset(this.Block.PeekHeapReference(num + this._importsOffset, this._isBlobHeapRefSizeSmall));
		}

		// Token: 0x04000930 RID: 2352
		internal readonly int NumberOfRows;

		// Token: 0x04000931 RID: 2353
		private readonly bool _isImportScopeRefSizeSmall;

		// Token: 0x04000932 RID: 2354
		private readonly bool _isBlobHeapRefSizeSmall;

		// Token: 0x04000933 RID: 2355
		private const int ParentOffset = 0;

		// Token: 0x04000934 RID: 2356
		private readonly int _importsOffset;

		// Token: 0x04000935 RID: 2357
		internal readonly int RowSize;

		// Token: 0x04000936 RID: 2358
		internal readonly MemoryBlock Block;
	}
}
