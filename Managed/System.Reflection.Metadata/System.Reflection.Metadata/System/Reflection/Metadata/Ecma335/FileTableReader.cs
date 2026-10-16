using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000151 RID: 337
	internal readonly struct FileTableReader
	{
		// Token: 0x06000AF8 RID: 2808 RVA: 0x00020C0C File Offset: 0x0001EE0C
		internal FileTableReader(int numberOfRows, int stringHeapRefSize, int blobHeapRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsStringHeapRefSizeSmall = stringHeapRefSize == 2;
			this._IsBlobHeapRefSizeSmall = blobHeapRefSize == 2;
			this._FlagsOffset = 0;
			this._NameOffset = this._FlagsOffset + 4;
			this._HashValueOffset = this._NameOffset + stringHeapRefSize;
			this.RowSize = this._HashValueOffset + blobHeapRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x00020C7C File Offset: 0x0001EE7C
		internal BlobHandle GetHashValue(AssemblyFileHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return BlobHandle.FromOffset(this.Block.PeekHeapReference(num + this._HashValueOffset, this._IsBlobHeapRefSizeSmall));
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x00020CB8 File Offset: 0x0001EEB8
		internal uint GetFlags(AssemblyFileHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return this.Block.PeekUInt32(num + this._FlagsOffset);
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x00020CEC File Offset: 0x0001EEEC
		internal StringHandle GetName(AssemblyFileHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return StringHandle.FromOffset(this.Block.PeekHeapReference(num + this._NameOffset, this._IsStringHeapRefSizeSmall));
		}

		// Token: 0x04000866 RID: 2150
		internal readonly int NumberOfRows;

		// Token: 0x04000867 RID: 2151
		private readonly bool _IsStringHeapRefSizeSmall;

		// Token: 0x04000868 RID: 2152
		private readonly bool _IsBlobHeapRefSizeSmall;

		// Token: 0x04000869 RID: 2153
		private readonly int _FlagsOffset;

		// Token: 0x0400086A RID: 2154
		private readonly int _NameOffset;

		// Token: 0x0400086B RID: 2155
		private readonly int _HashValueOffset;

		// Token: 0x0400086C RID: 2156
		internal readonly int RowSize;

		// Token: 0x0400086D RID: 2157
		internal readonly MemoryBlock Block;
	}
}
