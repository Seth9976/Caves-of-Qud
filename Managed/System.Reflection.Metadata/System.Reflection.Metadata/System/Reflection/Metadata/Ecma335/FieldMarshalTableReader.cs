using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000138 RID: 312
	internal readonly struct FieldMarshalTableReader
	{
		// Token: 0x06000A99 RID: 2713 RVA: 0x0001F3A4 File Offset: 0x0001D5A4
		internal FieldMarshalTableReader(int numberOfRows, bool declaredSorted, int hasFieldMarshalRefSize, int blobHeapRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsHasFieldMarshalRefSizeSmall = hasFieldMarshalRefSize == 2;
			this._IsBlobHeapRefSizeSmall = blobHeapRefSize == 2;
			this._ParentOffset = 0;
			this._NativeTypeOffset = this._ParentOffset + hasFieldMarshalRefSize;
			this.RowSize = this._NativeTypeOffset + blobHeapRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
			if (!declaredSorted && !this.CheckSorted())
			{
				Throw.TableNotSorted(TableIndex.FieldMarshal);
			}
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x0001F41C File Offset: 0x0001D61C
		internal EntityHandle GetParent(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return HasFieldMarshalTag.ConvertToHandle(this.Block.PeekTaggedReference(num + this._ParentOffset, this._IsHasFieldMarshalRefSizeSmall));
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x0001F454 File Offset: 0x0001D654
		internal BlobHandle GetNativeType(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return BlobHandle.FromOffset(this.Block.PeekHeapReference(num + this._NativeTypeOffset, this._IsBlobHeapRefSizeSmall));
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x0001F48C File Offset: 0x0001D68C
		internal int FindFieldMarshalRowId(EntityHandle handle)
		{
			int num = this.Block.BinarySearchReference(this.NumberOfRows, this.RowSize, this._ParentOffset, HasFieldMarshalTag.ConvertToTag(handle), this._IsHasFieldMarshalRefSizeSmall);
			return num + 1;
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x0001F4C6 File Offset: 0x0001D6C6
		private bool CheckSorted()
		{
			return this.Block.IsOrderedByReferenceAscending(this.RowSize, this._ParentOffset, this._IsHasFieldMarshalRefSizeSmall);
		}

		// Token: 0x040007B5 RID: 1973
		internal readonly int NumberOfRows;

		// Token: 0x040007B6 RID: 1974
		private readonly bool _IsHasFieldMarshalRefSizeSmall;

		// Token: 0x040007B7 RID: 1975
		private readonly bool _IsBlobHeapRefSizeSmall;

		// Token: 0x040007B8 RID: 1976
		private readonly int _ParentOffset;

		// Token: 0x040007B9 RID: 1977
		private readonly int _NativeTypeOffset;

		// Token: 0x040007BA RID: 1978
		internal readonly int RowSize;

		// Token: 0x040007BB RID: 1979
		internal readonly MemoryBlock Block;
	}
}
