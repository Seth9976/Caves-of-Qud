using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000164 RID: 356
	internal readonly struct DocumentTableReader
	{
		// Token: 0x06000B31 RID: 2865 RVA: 0x00021F40 File Offset: 0x00020140
		internal DocumentTableReader(int numberOfRows, int guidHeapRefSize, int blobHeapRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._isGuidHeapRefSizeSmall = guidHeapRefSize == 2;
			this._isBlobHeapRefSizeSmall = blobHeapRefSize == 2;
			this._hashAlgorithmOffset = blobHeapRefSize;
			this._hashOffset = this._hashAlgorithmOffset + guidHeapRefSize;
			this._languageOffset = this._hashOffset + blobHeapRefSize;
			this.RowSize = this._languageOffset + guidHeapRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x00021FB0 File Offset: 0x000201B0
		internal DocumentNameBlobHandle GetName(DocumentHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return DocumentNameBlobHandle.FromOffset(this.Block.PeekHeapReference(num, this._isBlobHeapRefSizeSmall));
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x00021FE8 File Offset: 0x000201E8
		internal GuidHandle GetHashAlgorithm(DocumentHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return GuidHandle.FromIndex(this.Block.PeekHeapReference(num + this._hashAlgorithmOffset, this._isGuidHeapRefSizeSmall));
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x00022024 File Offset: 0x00020224
		internal BlobHandle GetHash(DocumentHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return BlobHandle.FromOffset(this.Block.PeekHeapReference(num + this._hashOffset, this._isBlobHeapRefSizeSmall));
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x00022060 File Offset: 0x00020260
		internal GuidHandle GetLanguage(DocumentHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return GuidHandle.FromIndex(this.Block.PeekHeapReference(num + this._languageOffset, this._isGuidHeapRefSizeSmall));
		}

		// Token: 0x040008FF RID: 2303
		internal readonly int NumberOfRows;

		// Token: 0x04000900 RID: 2304
		private readonly bool _isGuidHeapRefSizeSmall;

		// Token: 0x04000901 RID: 2305
		private readonly bool _isBlobHeapRefSizeSmall;

		// Token: 0x04000902 RID: 2306
		private const int NameOffset = 0;

		// Token: 0x04000903 RID: 2307
		private readonly int _hashAlgorithmOffset;

		// Token: 0x04000904 RID: 2308
		private readonly int _hashOffset;

		// Token: 0x04000905 RID: 2309
		private readonly int _languageOffset;

		// Token: 0x04000906 RID: 2310
		internal readonly int RowSize;

		// Token: 0x04000907 RID: 2311
		internal readonly MemoryBlock Block;
	}
}
