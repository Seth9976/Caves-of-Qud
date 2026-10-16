using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000169 RID: 361
	internal readonly struct StateMachineMethodTableReader
	{
		// Token: 0x06000B49 RID: 2889 RVA: 0x000225B0 File Offset: 0x000207B0
		internal StateMachineMethodTableReader(int numberOfRows, bool declaredSorted, int methodRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._isMethodRefSizeSmall = methodRefSize == 2;
			this._kickoffMethodOffset = methodRefSize;
			this.RowSize = this._kickoffMethodOffset + methodRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
			if (numberOfRows > 0 && !declaredSorted)
			{
				Throw.TableNotSorted(TableIndex.StateMachineMethod);
			}
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x00022608 File Offset: 0x00020808
		internal MethodDefinitionHandle FindKickoffMethod(int moveNextMethodRowId)
		{
			int num = this.Block.BinarySearchReference(this.NumberOfRows, this.RowSize, 0, (uint)moveNextMethodRowId, this._isMethodRefSizeSmall);
			if (num < 0)
			{
				return default(MethodDefinitionHandle);
			}
			return this.GetKickoffMethod(num + 1);
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x0002264C File Offset: 0x0002084C
		private MethodDefinitionHandle GetKickoffMethod(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return MethodDefinitionHandle.FromRowId(this.Block.PeekReference(num + this._kickoffMethodOffset, this._isMethodRefSizeSmall));
		}

		// Token: 0x0400092A RID: 2346
		internal readonly int NumberOfRows;

		// Token: 0x0400092B RID: 2347
		private readonly bool _isMethodRefSizeSmall;

		// Token: 0x0400092C RID: 2348
		private const int MoveNextMethodOffset = 0;

		// Token: 0x0400092D RID: 2349
		private readonly int _kickoffMethodOffset;

		// Token: 0x0400092E RID: 2350
		internal readonly int RowSize;

		// Token: 0x0400092F RID: 2351
		internal readonly MemoryBlock Block;
	}
}
