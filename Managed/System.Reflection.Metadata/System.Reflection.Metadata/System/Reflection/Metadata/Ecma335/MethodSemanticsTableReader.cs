using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000143 RID: 323
	internal readonly struct MethodSemanticsTableReader
	{
		// Token: 0x06000AC5 RID: 2757 RVA: 0x0001FE9C File Offset: 0x0001E09C
		internal MethodSemanticsTableReader(int numberOfRows, bool declaredSorted, int methodTableRowRefSize, int hasSemanticRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsMethodTableRowRefSizeSmall = methodTableRowRefSize == 2;
			this._IsHasSemanticRefSizeSmall = hasSemanticRefSize == 2;
			this._SemanticsFlagOffset = 0;
			this._MethodOffset = this._SemanticsFlagOffset + 2;
			this._AssociationOffset = this._MethodOffset + methodTableRowRefSize;
			this.RowSize = this._AssociationOffset + hasSemanticRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
			if (!declaredSorted && !this.CheckSorted())
			{
				Throw.TableNotSorted(TableIndex.MethodSemantics);
			}
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x0001FF20 File Offset: 0x0001E120
		internal MethodDefinitionHandle GetMethod(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return MethodDefinitionHandle.FromRowId(this.Block.PeekReference(num + this._MethodOffset, this._IsMethodTableRowRefSizeSmall));
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x0001FF58 File Offset: 0x0001E158
		internal MethodSemanticsAttributes GetSemantics(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return (MethodSemanticsAttributes)this.Block.PeekUInt16(num + this._SemanticsFlagOffset);
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x0001FF84 File Offset: 0x0001E184
		internal EntityHandle GetAssociation(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return HasSemanticsTag.ConvertToHandle(this.Block.PeekTaggedReference(num + this._AssociationOffset, this._IsHasSemanticRefSizeSmall));
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x0001FFBC File Offset: 0x0001E1BC
		internal int FindSemanticMethodsForEvent(EventDefinitionHandle eventDef, out ushort methodCount)
		{
			methodCount = 0;
			uint num = HasSemanticsTag.ConvertEventHandleToTag(eventDef);
			return this.BinarySearchTag(num, ref methodCount);
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x0001FFDC File Offset: 0x0001E1DC
		internal int FindSemanticMethodsForProperty(PropertyDefinitionHandle propertyDef, out ushort methodCount)
		{
			methodCount = 0;
			uint num = HasSemanticsTag.ConvertPropertyHandleToTag(propertyDef);
			return this.BinarySearchTag(num, ref methodCount);
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x0001FFFC File Offset: 0x0001E1FC
		private int BinarySearchTag(uint searchCodedTag, ref ushort methodCount)
		{
			int num;
			int num2;
			this.Block.BinarySearchReferenceRange(this.NumberOfRows, this.RowSize, this._AssociationOffset, searchCodedTag, this._IsHasSemanticRefSizeSmall, out num, out num2);
			if (num == -1)
			{
				methodCount = 0;
				return 0;
			}
			methodCount = (ushort)(num2 - num + 1);
			return num + 1;
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x00020045 File Offset: 0x0001E245
		private bool CheckSorted()
		{
			return this.Block.IsOrderedByReferenceAscending(this.RowSize, this._AssociationOffset, this._IsHasSemanticRefSizeSmall);
		}

		// Token: 0x040007FE RID: 2046
		internal readonly int NumberOfRows;

		// Token: 0x040007FF RID: 2047
		private readonly bool _IsMethodTableRowRefSizeSmall;

		// Token: 0x04000800 RID: 2048
		private readonly bool _IsHasSemanticRefSizeSmall;

		// Token: 0x04000801 RID: 2049
		private readonly int _SemanticsFlagOffset;

		// Token: 0x04000802 RID: 2050
		private readonly int _MethodOffset;

		// Token: 0x04000803 RID: 2051
		private readonly int _AssociationOffset;

		// Token: 0x04000804 RID: 2052
		internal readonly int RowSize;

		// Token: 0x04000805 RID: 2053
		internal readonly MemoryBlock Block;
	}
}
