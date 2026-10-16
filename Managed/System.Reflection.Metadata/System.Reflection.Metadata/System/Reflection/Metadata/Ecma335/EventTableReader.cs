using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x0200013F RID: 319
	internal struct EventTableReader
	{
		// Token: 0x06000AB7 RID: 2743 RVA: 0x0001FAE8 File Offset: 0x0001DCE8
		internal EventTableReader(int numberOfRows, int typeDefOrRefRefSize, int stringHeapRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsTypeDefOrRefRefSizeSmall = typeDefOrRefRefSize == 2;
			this._IsStringHeapRefSizeSmall = stringHeapRefSize == 2;
			this._FlagsOffset = 0;
			this._NameOffset = this._FlagsOffset + 2;
			this._EventTypeOffset = this._NameOffset + stringHeapRefSize;
			this.RowSize = this._EventTypeOffset + typeDefOrRefRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x0001FB58 File Offset: 0x0001DD58
		internal EventAttributes GetFlags(EventDefinitionHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return this.Block.PeekUInt16(num + this._FlagsOffset);
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x0001FB8C File Offset: 0x0001DD8C
		internal StringHandle GetName(EventDefinitionHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return StringHandle.FromOffset(this.Block.PeekHeapReference(num + this._NameOffset, this._IsStringHeapRefSizeSmall));
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x0001FBC8 File Offset: 0x0001DDC8
		internal EntityHandle GetEventType(EventDefinitionHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return TypeDefOrRefTag.ConvertToHandle(this.Block.PeekTaggedReference(num + this._EventTypeOffset, this._IsTypeDefOrRefRefSizeSmall));
		}

		// Token: 0x040007E2 RID: 2018
		internal int NumberOfRows;

		// Token: 0x040007E3 RID: 2019
		private readonly bool _IsTypeDefOrRefRefSizeSmall;

		// Token: 0x040007E4 RID: 2020
		private readonly bool _IsStringHeapRefSizeSmall;

		// Token: 0x040007E5 RID: 2021
		private readonly int _FlagsOffset;

		// Token: 0x040007E6 RID: 2022
		private readonly int _NameOffset;

		// Token: 0x040007E7 RID: 2023
		private readonly int _EventTypeOffset;

		// Token: 0x040007E8 RID: 2024
		internal readonly int RowSize;

		// Token: 0x040007E9 RID: 2025
		internal MemoryBlock Block;
	}
}
