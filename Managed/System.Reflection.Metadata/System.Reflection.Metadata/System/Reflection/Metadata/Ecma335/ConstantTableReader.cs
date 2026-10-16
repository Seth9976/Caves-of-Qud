using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000136 RID: 310
	internal readonly struct ConstantTableReader
	{
		// Token: 0x06000A8D RID: 2701 RVA: 0x0001F000 File Offset: 0x0001D200
		internal ConstantTableReader(int numberOfRows, bool declaredSorted, int hasConstantRefSize, int blobHeapRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsHasConstantRefSizeSmall = hasConstantRefSize == 2;
			this._IsBlobHeapRefSizeSmall = blobHeapRefSize == 2;
			this._TypeOffset = 0;
			this._ParentOffset = this._TypeOffset + 1 + 1;
			this._ValueOffset = this._ParentOffset + hasConstantRefSize;
			this.RowSize = this._ValueOffset + blobHeapRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
			if (!declaredSorted && !this.CheckSorted())
			{
				Throw.TableNotSorted(TableIndex.Constant);
			}
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x0001F088 File Offset: 0x0001D288
		internal ConstantTypeCode GetType(ConstantHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return (ConstantTypeCode)this.Block.PeekByte(num + this._TypeOffset);
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x0001F0BC File Offset: 0x0001D2BC
		internal BlobHandle GetValue(ConstantHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return BlobHandle.FromOffset(this.Block.PeekHeapReference(num + this._ValueOffset, this._IsBlobHeapRefSizeSmall));
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x0001F0F8 File Offset: 0x0001D2F8
		internal EntityHandle GetParent(ConstantHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return HasConstantTag.ConvertToHandle(this.Block.PeekTaggedReference(num + this._ParentOffset, this._IsHasConstantRefSizeSmall));
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x0001F134 File Offset: 0x0001D334
		internal ConstantHandle FindConstant(EntityHandle parentHandle)
		{
			int num = this.Block.BinarySearchReference(this.NumberOfRows, this.RowSize, this._ParentOffset, HasConstantTag.ConvertToTag(parentHandle), this._IsHasConstantRefSizeSmall);
			return ConstantHandle.FromRowId(num + 1);
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x0001F173 File Offset: 0x0001D373
		private bool CheckSorted()
		{
			return this.Block.IsOrderedByReferenceAscending(this.RowSize, this._ParentOffset, this._IsHasConstantRefSizeSmall);
		}

		// Token: 0x040007A3 RID: 1955
		internal readonly int NumberOfRows;

		// Token: 0x040007A4 RID: 1956
		private readonly bool _IsHasConstantRefSizeSmall;

		// Token: 0x040007A5 RID: 1957
		private readonly bool _IsBlobHeapRefSizeSmall;

		// Token: 0x040007A6 RID: 1958
		private readonly int _TypeOffset;

		// Token: 0x040007A7 RID: 1959
		private readonly int _ParentOffset;

		// Token: 0x040007A8 RID: 1960
		private readonly int _ValueOffset;

		// Token: 0x040007A9 RID: 1961
		internal readonly int RowSize;

		// Token: 0x040007AA RID: 1962
		internal readonly MemoryBlock Block;
	}
}
