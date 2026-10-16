using System;
using System.Reflection.Internal;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000137 RID: 311
	internal readonly struct CustomAttributeTableReader
	{
		// Token: 0x06000A93 RID: 2707 RVA: 0x0001F194 File Offset: 0x0001D394
		internal CustomAttributeTableReader(int numberOfRows, bool declaredSorted, int hasCustomAttributeRefSize, int customAttributeTypeRefSize, int blobHeapRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsHasCustomAttributeRefSizeSmall = hasCustomAttributeRefSize == 2;
			this._IsCustomAttributeTypeRefSizeSmall = customAttributeTypeRefSize == 2;
			this._IsBlobHeapRefSizeSmall = blobHeapRefSize == 2;
			this._ParentOffset = 0;
			this._TypeOffset = this._ParentOffset + hasCustomAttributeRefSize;
			this._ValueOffset = this._TypeOffset + customAttributeTypeRefSize;
			this.RowSize = this._ValueOffset + blobHeapRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
			this.PtrTable = null;
			if (!declaredSorted && !this.CheckSorted())
			{
				this.PtrTable = this.Block.BuildPtrTable(numberOfRows, this.RowSize, this._ParentOffset, this._IsHasCustomAttributeRefSizeSmall);
			}
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x0001F248 File Offset: 0x0001D448
		internal EntityHandle GetParent(CustomAttributeHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return HasCustomAttributeTag.ConvertToHandle(this.Block.PeekTaggedReference(num + this._ParentOffset, this._IsHasCustomAttributeRefSizeSmall));
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x0001F284 File Offset: 0x0001D484
		internal EntityHandle GetConstructor(CustomAttributeHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return CustomAttributeTypeTag.ConvertToHandle(this.Block.PeekTaggedReference(num + this._TypeOffset, this._IsCustomAttributeTypeRefSizeSmall));
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x0001F2C0 File Offset: 0x0001D4C0
		internal BlobHandle GetValue(CustomAttributeHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return BlobHandle.FromOffset(this.Block.PeekHeapReference(num + this._ValueOffset, this._IsBlobHeapRefSizeSmall));
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x0001F2FC File Offset: 0x0001D4FC
		internal void GetAttributeRange(EntityHandle parentHandle, out int firstImplRowId, out int lastImplRowId)
		{
			int num;
			int num2;
			if (this.PtrTable != null)
			{
				this.Block.BinarySearchReferenceRange(this.PtrTable, this.RowSize, this._ParentOffset, HasCustomAttributeTag.ConvertToTag(parentHandle), this._IsHasCustomAttributeRefSizeSmall, out num, out num2);
			}
			else
			{
				this.Block.BinarySearchReferenceRange(this.NumberOfRows, this.RowSize, this._ParentOffset, HasCustomAttributeTag.ConvertToTag(parentHandle), this._IsHasCustomAttributeRefSizeSmall, out num, out num2);
			}
			if (num == -1)
			{
				firstImplRowId = 1;
				lastImplRowId = 0;
				return;
			}
			firstImplRowId = num + 1;
			lastImplRowId = num2 + 1;
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x0001F382 File Offset: 0x0001D582
		private bool CheckSorted()
		{
			return this.Block.IsOrderedByReferenceAscending(this.RowSize, this._ParentOffset, this._IsHasCustomAttributeRefSizeSmall);
		}

		// Token: 0x040007AB RID: 1963
		internal readonly int NumberOfRows;

		// Token: 0x040007AC RID: 1964
		private readonly bool _IsHasCustomAttributeRefSizeSmall;

		// Token: 0x040007AD RID: 1965
		private readonly bool _IsCustomAttributeTypeRefSizeSmall;

		// Token: 0x040007AE RID: 1966
		private readonly bool _IsBlobHeapRefSizeSmall;

		// Token: 0x040007AF RID: 1967
		private readonly int _ParentOffset;

		// Token: 0x040007B0 RID: 1968
		private readonly int _TypeOffset;

		// Token: 0x040007B1 RID: 1969
		private readonly int _ValueOffset;

		// Token: 0x040007B2 RID: 1970
		internal readonly int RowSize;

		// Token: 0x040007B3 RID: 1971
		internal readonly MemoryBlock Block;

		// Token: 0x040007B4 RID: 1972
		[Nullable(2)]
		internal readonly int[] PtrTable;
	}
}
