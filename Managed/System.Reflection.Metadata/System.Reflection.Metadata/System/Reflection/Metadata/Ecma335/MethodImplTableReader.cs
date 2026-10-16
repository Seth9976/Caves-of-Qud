using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000144 RID: 324
	internal readonly struct MethodImplTableReader
	{
		// Token: 0x06000ACD RID: 2765 RVA: 0x00020064 File Offset: 0x0001E264
		internal MethodImplTableReader(int numberOfRows, bool declaredSorted, int typeDefTableRowRefSize, int methodDefOrRefRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsTypeDefTableRowRefSizeSmall = typeDefTableRowRefSize == 2;
			this._IsMethodDefOrRefRefSizeSmall = methodDefOrRefRefSize == 2;
			this._ClassOffset = 0;
			this._MethodBodyOffset = this._ClassOffset + typeDefTableRowRefSize;
			this._MethodDeclarationOffset = this._MethodBodyOffset + methodDefOrRefRefSize;
			this.RowSize = this._MethodDeclarationOffset + methodDefOrRefRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
			if (!declaredSorted && !this.CheckSorted())
			{
				Throw.TableNotSorted(TableIndex.MethodImpl);
			}
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x000200EC File Offset: 0x0001E2EC
		internal TypeDefinitionHandle GetClass(MethodImplementationHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return TypeDefinitionHandle.FromRowId(this.Block.PeekReference(num + this._ClassOffset, this._IsTypeDefTableRowRefSizeSmall));
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x00020128 File Offset: 0x0001E328
		internal EntityHandle GetMethodBody(MethodImplementationHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return MethodDefOrRefTag.ConvertToHandle(this.Block.PeekTaggedReference(num + this._MethodBodyOffset, this._IsMethodDefOrRefRefSizeSmall));
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x00020164 File Offset: 0x0001E364
		internal EntityHandle GetMethodDeclaration(MethodImplementationHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return MethodDefOrRefTag.ConvertToHandle(this.Block.PeekTaggedReference(num + this._MethodDeclarationOffset, this._IsMethodDefOrRefRefSizeSmall));
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x000201A0 File Offset: 0x0001E3A0
		internal void GetMethodImplRange(TypeDefinitionHandle typeDef, out int firstImplRowId, out int lastImplRowId)
		{
			int num;
			int num2;
			this.Block.BinarySearchReferenceRange(this.NumberOfRows, this.RowSize, this._ClassOffset, (uint)typeDef.RowId, this._IsTypeDefTableRowRefSizeSmall, out num, out num2);
			if (num == -1)
			{
				firstImplRowId = 1;
				lastImplRowId = 0;
				return;
			}
			firstImplRowId = num + 1;
			lastImplRowId = num2 + 1;
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x000201F0 File Offset: 0x0001E3F0
		private bool CheckSorted()
		{
			return this.Block.IsOrderedByReferenceAscending(this.RowSize, this._ClassOffset, this._IsTypeDefTableRowRefSizeSmall);
		}

		// Token: 0x04000806 RID: 2054
		internal readonly int NumberOfRows;

		// Token: 0x04000807 RID: 2055
		private readonly bool _IsTypeDefTableRowRefSizeSmall;

		// Token: 0x04000808 RID: 2056
		private readonly bool _IsMethodDefOrRefRefSizeSmall;

		// Token: 0x04000809 RID: 2057
		private readonly int _ClassOffset;

		// Token: 0x0400080A RID: 2058
		private readonly int _MethodBodyOffset;

		// Token: 0x0400080B RID: 2059
		private readonly int _MethodDeclarationOffset;

		// Token: 0x0400080C RID: 2060
		internal readonly int RowSize;

		// Token: 0x0400080D RID: 2061
		internal readonly MemoryBlock Block;
	}
}
