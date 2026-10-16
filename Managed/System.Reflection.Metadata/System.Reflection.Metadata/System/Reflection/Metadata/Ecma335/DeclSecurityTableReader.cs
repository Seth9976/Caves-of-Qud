using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000139 RID: 313
	internal readonly struct DeclSecurityTableReader
	{
		// Token: 0x06000A9E RID: 2718 RVA: 0x0001F4E8 File Offset: 0x0001D6E8
		internal DeclSecurityTableReader(int numberOfRows, bool declaredSorted, int hasDeclSecurityRefSize, int blobHeapRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsHasDeclSecurityRefSizeSmall = hasDeclSecurityRefSize == 2;
			this._IsBlobHeapRefSizeSmall = blobHeapRefSize == 2;
			this._ActionOffset = 0;
			this._ParentOffset = this._ActionOffset + 2;
			this._PermissionSetOffset = this._ParentOffset + hasDeclSecurityRefSize;
			this.RowSize = this._PermissionSetOffset + blobHeapRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
			if (!declaredSorted && !this.CheckSorted())
			{
				Throw.TableNotSorted(TableIndex.DeclSecurity);
			}
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x0001F56C File Offset: 0x0001D76C
		internal DeclarativeSecurityAction GetAction(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return (DeclarativeSecurityAction)this.Block.PeekUInt16(num + this._ActionOffset);
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x0001F598 File Offset: 0x0001D798
		internal EntityHandle GetParent(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return HasDeclSecurityTag.ConvertToHandle(this.Block.PeekTaggedReference(num + this._ParentOffset, this._IsHasDeclSecurityRefSizeSmall));
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x0001F5D0 File Offset: 0x0001D7D0
		internal BlobHandle GetPermissionSet(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return BlobHandle.FromOffset(this.Block.PeekHeapReference(num + this._PermissionSetOffset, this._IsBlobHeapRefSizeSmall));
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x0001F608 File Offset: 0x0001D808
		internal void GetAttributeRange(EntityHandle parentToken, out int firstImplRowId, out int lastImplRowId)
		{
			int num;
			int num2;
			this.Block.BinarySearchReferenceRange(this.NumberOfRows, this.RowSize, this._ParentOffset, HasDeclSecurityTag.ConvertToTag(parentToken), this._IsHasDeclSecurityRefSizeSmall, out num, out num2);
			if (num == -1)
			{
				firstImplRowId = 1;
				lastImplRowId = 0;
				return;
			}
			firstImplRowId = num + 1;
			lastImplRowId = num2 + 1;
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x0001F657 File Offset: 0x0001D857
		private bool CheckSorted()
		{
			return this.Block.IsOrderedByReferenceAscending(this.RowSize, this._ParentOffset, this._IsHasDeclSecurityRefSizeSmall);
		}

		// Token: 0x040007BC RID: 1980
		internal readonly int NumberOfRows;

		// Token: 0x040007BD RID: 1981
		private readonly bool _IsHasDeclSecurityRefSizeSmall;

		// Token: 0x040007BE RID: 1982
		private readonly bool _IsBlobHeapRefSizeSmall;

		// Token: 0x040007BF RID: 1983
		private readonly int _ActionOffset;

		// Token: 0x040007C0 RID: 1984
		private readonly int _ParentOffset;

		// Token: 0x040007C1 RID: 1985
		private readonly int _PermissionSetOffset;

		// Token: 0x040007C2 RID: 1986
		internal readonly int RowSize;

		// Token: 0x040007C3 RID: 1987
		internal readonly MemoryBlock Block;
	}
}
