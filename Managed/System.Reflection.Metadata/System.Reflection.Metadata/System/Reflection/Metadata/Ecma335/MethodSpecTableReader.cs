using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000156 RID: 342
	internal readonly struct MethodSpecTableReader
	{
		// Token: 0x06000B17 RID: 2839 RVA: 0x00021414 File Offset: 0x0001F614
		internal MethodSpecTableReader(int numberOfRows, int methodDefOrRefRefSize, int blobHeapRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsMethodDefOrRefRefSizeSmall = methodDefOrRefRefSize == 2;
			this._IsBlobHeapRefSizeSmall = blobHeapRefSize == 2;
			this._MethodOffset = 0;
			this._InstantiationOffset = this._MethodOffset + methodDefOrRefRefSize;
			this.RowSize = this._InstantiationOffset + blobHeapRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x00021478 File Offset: 0x0001F678
		internal EntityHandle GetMethod(MethodSpecificationHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return MethodDefOrRefTag.ConvertToHandle(this.Block.PeekTaggedReference(num + this._MethodOffset, this._IsMethodDefOrRefRefSizeSmall));
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x000214B4 File Offset: 0x0001F6B4
		internal BlobHandle GetInstantiation(MethodSpecificationHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return BlobHandle.FromOffset(this.Block.PeekHeapReference(num + this._InstantiationOffset, this._IsBlobHeapRefSizeSmall));
		}

		// Token: 0x04000890 RID: 2192
		internal readonly int NumberOfRows;

		// Token: 0x04000891 RID: 2193
		private readonly bool _IsMethodDefOrRefRefSizeSmall;

		// Token: 0x04000892 RID: 2194
		private readonly bool _IsBlobHeapRefSizeSmall;

		// Token: 0x04000893 RID: 2195
		private readonly int _MethodOffset;

		// Token: 0x04000894 RID: 2196
		private readonly int _InstantiationOffset;

		// Token: 0x04000895 RID: 2197
		internal readonly int RowSize;

		// Token: 0x04000896 RID: 2198
		internal readonly MemoryBlock Block;
	}
}
