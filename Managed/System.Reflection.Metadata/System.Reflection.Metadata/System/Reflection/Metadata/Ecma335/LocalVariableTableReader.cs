using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000167 RID: 359
	internal readonly struct LocalVariableTableReader
	{
		// Token: 0x06000B42 RID: 2882 RVA: 0x000223DC File Offset: 0x000205DC
		internal LocalVariableTableReader(int numberOfRows, int stringHeapRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._isStringHeapRefSizeSmall = stringHeapRefSize == 2;
			this._attributesOffset = 0;
			this._indexOffset = this._attributesOffset + 2;
			this._nameOffset = this._indexOffset + 2;
			this.RowSize = this._nameOffset + stringHeapRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x00022444 File Offset: 0x00020644
		internal LocalVariableAttributes GetAttributes(LocalVariableHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return (LocalVariableAttributes)this.Block.PeekUInt16(num + this._attributesOffset);
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x00022478 File Offset: 0x00020678
		internal ushort GetIndex(LocalVariableHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return this.Block.PeekUInt16(num + this._indexOffset);
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x000224AC File Offset: 0x000206AC
		internal StringHandle GetName(LocalVariableHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return StringHandle.FromOffset(this.Block.PeekHeapReference(num + this._nameOffset, this._isStringHeapRefSizeSmall));
		}

		// Token: 0x0400091C RID: 2332
		internal readonly int NumberOfRows;

		// Token: 0x0400091D RID: 2333
		private readonly bool _isStringHeapRefSizeSmall;

		// Token: 0x0400091E RID: 2334
		private readonly int _attributesOffset;

		// Token: 0x0400091F RID: 2335
		private readonly int _indexOffset;

		// Token: 0x04000920 RID: 2336
		private readonly int _nameOffset;

		// Token: 0x04000921 RID: 2337
		internal readonly int RowSize;

		// Token: 0x04000922 RID: 2338
		internal readonly MemoryBlock Block;
	}
}
