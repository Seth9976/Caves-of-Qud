using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000135 RID: 309
	internal struct MemberRefTableReader
	{
		// Token: 0x06000A89 RID: 2697 RVA: 0x0001EED0 File Offset: 0x0001D0D0
		internal MemberRefTableReader(int numberOfRows, int memberRefParentRefSize, int stringHeapRefSize, int blobHeapRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsMemberRefParentRefSizeSmall = memberRefParentRefSize == 2;
			this._IsStringHeapRefSizeSmall = stringHeapRefSize == 2;
			this._IsBlobHeapRefSizeSmall = blobHeapRefSize == 2;
			this._ClassOffset = 0;
			this._NameOffset = this._ClassOffset + memberRefParentRefSize;
			this._SignatureOffset = this._NameOffset + stringHeapRefSize;
			this.RowSize = this._SignatureOffset + blobHeapRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x0001EF4C File Offset: 0x0001D14C
		internal BlobHandle GetSignature(MemberReferenceHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return BlobHandle.FromOffset(this.Block.PeekHeapReference(num + this._SignatureOffset, this._IsBlobHeapRefSizeSmall));
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x0001EF88 File Offset: 0x0001D188
		internal StringHandle GetName(MemberReferenceHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return StringHandle.FromOffset(this.Block.PeekHeapReference(num + this._NameOffset, this._IsStringHeapRefSizeSmall));
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x0001EFC4 File Offset: 0x0001D1C4
		internal EntityHandle GetClass(MemberReferenceHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return MemberRefParentTag.ConvertToHandle(this.Block.PeekTaggedReference(num + this._ClassOffset, this._IsMemberRefParentRefSizeSmall));
		}

		// Token: 0x0400079A RID: 1946
		internal int NumberOfRows;

		// Token: 0x0400079B RID: 1947
		private readonly bool _IsMemberRefParentRefSizeSmall;

		// Token: 0x0400079C RID: 1948
		private readonly bool _IsStringHeapRefSizeSmall;

		// Token: 0x0400079D RID: 1949
		private readonly bool _IsBlobHeapRefSizeSmall;

		// Token: 0x0400079E RID: 1950
		private readonly int _ClassOffset;

		// Token: 0x0400079F RID: 1951
		private readonly int _NameOffset;

		// Token: 0x040007A0 RID: 1952
		private readonly int _SignatureOffset;

		// Token: 0x040007A1 RID: 1953
		internal readonly int RowSize;

		// Token: 0x040007A2 RID: 1954
		internal MemoryBlock Block;
	}
}
