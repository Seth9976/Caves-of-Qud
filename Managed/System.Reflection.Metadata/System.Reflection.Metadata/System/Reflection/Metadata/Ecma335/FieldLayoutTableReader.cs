using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x0200013B RID: 315
	internal readonly struct FieldLayoutTableReader
	{
		// Token: 0x06000AAA RID: 2730 RVA: 0x0001F7CC File Offset: 0x0001D9CC
		internal FieldLayoutTableReader(int numberOfRows, bool declaredSorted, int fieldTableRowRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsFieldTableRowRefSizeSmall = fieldTableRowRefSize == 2;
			this._OffsetOffset = 0;
			this._FieldOffset = this._OffsetOffset + 4;
			this.RowSize = this._FieldOffset + fieldTableRowRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
			if (!declaredSorted && !this.CheckSorted())
			{
				Throw.TableNotSorted(TableIndex.FieldLayout);
			}
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x0001F838 File Offset: 0x0001DA38
		internal int FindFieldLayoutRowId(FieldDefinitionHandle handle)
		{
			int num = this.Block.BinarySearchReference(this.NumberOfRows, this.RowSize, this._FieldOffset, (uint)handle.RowId, this._IsFieldTableRowRefSizeSmall);
			return num + 1;
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x0001F874 File Offset: 0x0001DA74
		internal uint GetOffset(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return this.Block.PeekUInt32(num + this._OffsetOffset);
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x0001F8A0 File Offset: 0x0001DAA0
		internal FieldDefinitionHandle GetField(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return FieldDefinitionHandle.FromRowId(this.Block.PeekReference(num + this._FieldOffset, this._IsFieldTableRowRefSizeSmall));
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x0001F8D6 File Offset: 0x0001DAD6
		private bool CheckSorted()
		{
			return this.Block.IsOrderedByReferenceAscending(this.RowSize, this._FieldOffset, this._IsFieldTableRowRefSizeSmall);
		}

		// Token: 0x040007CB RID: 1995
		internal readonly int NumberOfRows;

		// Token: 0x040007CC RID: 1996
		private readonly bool _IsFieldTableRowRefSizeSmall;

		// Token: 0x040007CD RID: 1997
		private readonly int _OffsetOffset;

		// Token: 0x040007CE RID: 1998
		private readonly int _FieldOffset;

		// Token: 0x040007CF RID: 1999
		internal readonly int RowSize;

		// Token: 0x040007D0 RID: 2000
		internal readonly MemoryBlock Block;
	}
}
