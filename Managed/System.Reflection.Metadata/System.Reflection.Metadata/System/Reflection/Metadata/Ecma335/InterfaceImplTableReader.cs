using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000134 RID: 308
	internal readonly struct InterfaceImplTableReader
	{
		// Token: 0x06000A85 RID: 2693 RVA: 0x0001EDAC File Offset: 0x0001CFAC
		internal InterfaceImplTableReader(int numberOfRows, bool declaredSorted, int typeDefTableRowRefSize, int typeDefOrRefRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsTypeDefTableRowRefSizeSmall = typeDefTableRowRefSize == 2;
			this._IsTypeDefOrRefRefSizeSmall = typeDefOrRefRefSize == 2;
			this._ClassOffset = 0;
			this._InterfaceOffset = this._ClassOffset + typeDefTableRowRefSize;
			this.RowSize = this._InterfaceOffset + typeDefOrRefRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
			if (!declaredSorted && !this.CheckSorted())
			{
				Throw.TableNotSorted(TableIndex.InterfaceImpl);
			}
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x0001EE22 File Offset: 0x0001D022
		private bool CheckSorted()
		{
			return this.Block.IsOrderedByReferenceAscending(this.RowSize, this._ClassOffset, this._IsTypeDefTableRowRefSizeSmall);
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x0001EE44 File Offset: 0x0001D044
		internal void GetInterfaceImplRange(TypeDefinitionHandle typeDef, out int firstImplRowId, out int lastImplRowId)
		{
			int rowId = typeDef.RowId;
			int num;
			int num2;
			this.Block.BinarySearchReferenceRange(this.NumberOfRows, this.RowSize, this._ClassOffset, (uint)rowId, this._IsTypeDefTableRowRefSizeSmall, out num, out num2);
			if (num == -1)
			{
				firstImplRowId = 1;
				lastImplRowId = 0;
				return;
			}
			firstImplRowId = num + 1;
			lastImplRowId = num2 + 1;
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x0001EE98 File Offset: 0x0001D098
		internal EntityHandle GetInterface(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return TypeDefOrRefTag.ConvertToHandle(this.Block.PeekTaggedReference(num + this._InterfaceOffset, this._IsTypeDefOrRefRefSizeSmall));
		}

		// Token: 0x04000793 RID: 1939
		internal readonly int NumberOfRows;

		// Token: 0x04000794 RID: 1940
		private readonly bool _IsTypeDefTableRowRefSizeSmall;

		// Token: 0x04000795 RID: 1941
		private readonly bool _IsTypeDefOrRefRefSizeSmall;

		// Token: 0x04000796 RID: 1942
		private readonly int _ClassOffset;

		// Token: 0x04000797 RID: 1943
		private readonly int _InterfaceOffset;

		// Token: 0x04000798 RID: 1944
		internal readonly int RowSize;

		// Token: 0x04000799 RID: 1945
		internal readonly MemoryBlock Block;
	}
}
