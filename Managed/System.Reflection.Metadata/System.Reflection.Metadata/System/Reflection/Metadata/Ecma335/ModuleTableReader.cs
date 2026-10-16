using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x0200012B RID: 299
	internal readonly struct ModuleTableReader
	{
		// Token: 0x06000A5A RID: 2650 RVA: 0x0001E258 File Offset: 0x0001C458
		internal ModuleTableReader(int numberOfRows, int stringHeapRefSize, int guidHeapRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsStringHeapRefSizeSmall = stringHeapRefSize == 2;
			this._IsGUIDHeapRefSizeSmall = guidHeapRefSize == 2;
			this._GenerationOffset = 0;
			this._NameOffset = this._GenerationOffset + 2;
			this._MVIdOffset = this._NameOffset + stringHeapRefSize;
			this._EnCIdOffset = this._MVIdOffset + guidHeapRefSize;
			this._EnCBaseIdOffset = this._EnCIdOffset + guidHeapRefSize;
			this.RowSize = this._EnCBaseIdOffset + guidHeapRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x0001E2E4 File Offset: 0x0001C4E4
		internal ushort GetGeneration()
		{
			return this.Block.PeekUInt16(this._GenerationOffset);
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x0001E2F7 File Offset: 0x0001C4F7
		internal StringHandle GetName()
		{
			return StringHandle.FromOffset(this.Block.PeekHeapReference(this._NameOffset, this._IsStringHeapRefSizeSmall));
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x0001E315 File Offset: 0x0001C515
		internal GuidHandle GetMvid()
		{
			return GuidHandle.FromIndex(this.Block.PeekHeapReference(this._MVIdOffset, this._IsGUIDHeapRefSizeSmall));
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x0001E333 File Offset: 0x0001C533
		internal GuidHandle GetEncId()
		{
			return GuidHandle.FromIndex(this.Block.PeekHeapReference(this._EnCIdOffset, this._IsGUIDHeapRefSizeSmall));
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x0001E351 File Offset: 0x0001C551
		internal GuidHandle GetEncBaseId()
		{
			return GuidHandle.FromIndex(this.Block.PeekHeapReference(this._EnCBaseIdOffset, this._IsGUIDHeapRefSizeSmall));
		}

		// Token: 0x0400074A RID: 1866
		internal readonly int NumberOfRows;

		// Token: 0x0400074B RID: 1867
		private readonly bool _IsStringHeapRefSizeSmall;

		// Token: 0x0400074C RID: 1868
		private readonly bool _IsGUIDHeapRefSizeSmall;

		// Token: 0x0400074D RID: 1869
		private readonly int _GenerationOffset;

		// Token: 0x0400074E RID: 1870
		private readonly int _NameOffset;

		// Token: 0x0400074F RID: 1871
		private readonly int _MVIdOffset;

		// Token: 0x04000750 RID: 1872
		private readonly int _EnCIdOffset;

		// Token: 0x04000751 RID: 1873
		private readonly int _EnCBaseIdOffset;

		// Token: 0x04000752 RID: 1874
		internal readonly int RowSize;

		// Token: 0x04000753 RID: 1875
		internal readonly MemoryBlock Block;
	}
}
