using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000133 RID: 307
	internal readonly struct ParamTableReader
	{
		// Token: 0x06000A81 RID: 2689 RVA: 0x0001ECA0 File Offset: 0x0001CEA0
		internal ParamTableReader(int numberOfRows, int stringHeapRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsStringHeapRefSizeSmall = stringHeapRefSize == 2;
			this._FlagsOffset = 0;
			this._SequenceOffset = this._FlagsOffset + 2;
			this._NameOffset = this._SequenceOffset + 2;
			this.RowSize = this._NameOffset + stringHeapRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x0001ED08 File Offset: 0x0001CF08
		internal ParameterAttributes GetFlags(ParameterHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return this.Block.PeekUInt16(num + this._FlagsOffset);
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x0001ED3C File Offset: 0x0001CF3C
		internal ushort GetSequence(ParameterHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return this.Block.PeekUInt16(num + this._SequenceOffset);
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x0001ED70 File Offset: 0x0001CF70
		internal StringHandle GetName(ParameterHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return StringHandle.FromOffset(this.Block.PeekHeapReference(num + this._NameOffset, this._IsStringHeapRefSizeSmall));
		}

		// Token: 0x0400078C RID: 1932
		internal readonly int NumberOfRows;

		// Token: 0x0400078D RID: 1933
		private readonly bool _IsStringHeapRefSizeSmall;

		// Token: 0x0400078E RID: 1934
		private readonly int _FlagsOffset;

		// Token: 0x0400078F RID: 1935
		private readonly int _SequenceOffset;

		// Token: 0x04000790 RID: 1936
		private readonly int _NameOffset;

		// Token: 0x04000791 RID: 1937
		internal readonly int RowSize;

		// Token: 0x04000792 RID: 1938
		internal readonly MemoryBlock Block;
	}
}
