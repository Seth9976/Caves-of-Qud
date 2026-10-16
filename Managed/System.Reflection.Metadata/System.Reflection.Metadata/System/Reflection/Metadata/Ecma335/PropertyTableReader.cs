using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000142 RID: 322
	internal readonly struct PropertyTableReader
	{
		// Token: 0x06000AC1 RID: 2753 RVA: 0x0001FD80 File Offset: 0x0001DF80
		internal PropertyTableReader(int numberOfRows, int stringHeapRefSize, int blobHeapRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsStringHeapRefSizeSmall = stringHeapRefSize == 2;
			this._IsBlobHeapRefSizeSmall = blobHeapRefSize == 2;
			this._FlagsOffset = 0;
			this._NameOffset = this._FlagsOffset + 2;
			this._SignatureOffset = this._NameOffset + stringHeapRefSize;
			this.RowSize = this._SignatureOffset + blobHeapRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x0001FDF0 File Offset: 0x0001DFF0
		internal PropertyAttributes GetFlags(PropertyDefinitionHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return this.Block.PeekUInt16(num + this._FlagsOffset);
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x0001FE24 File Offset: 0x0001E024
		internal StringHandle GetName(PropertyDefinitionHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return StringHandle.FromOffset(this.Block.PeekHeapReference(num + this._NameOffset, this._IsStringHeapRefSizeSmall));
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x0001FE60 File Offset: 0x0001E060
		internal BlobHandle GetSignature(PropertyDefinitionHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return BlobHandle.FromOffset(this.Block.PeekHeapReference(num + this._SignatureOffset, this._IsBlobHeapRefSizeSmall));
		}

		// Token: 0x040007F6 RID: 2038
		internal readonly int NumberOfRows;

		// Token: 0x040007F7 RID: 2039
		private readonly bool _IsStringHeapRefSizeSmall;

		// Token: 0x040007F8 RID: 2040
		private readonly bool _IsBlobHeapRefSizeSmall;

		// Token: 0x040007F9 RID: 2041
		private readonly int _FlagsOffset;

		// Token: 0x040007FA RID: 2042
		private readonly int _NameOffset;

		// Token: 0x040007FB RID: 2043
		private readonly int _SignatureOffset;

		// Token: 0x040007FC RID: 2044
		internal readonly int RowSize;

		// Token: 0x040007FD RID: 2045
		internal readonly MemoryBlock Block;
	}
}
