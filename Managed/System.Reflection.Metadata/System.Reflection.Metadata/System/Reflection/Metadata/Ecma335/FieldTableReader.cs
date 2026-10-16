using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x0200012F RID: 303
	internal readonly struct FieldTableReader
	{
		// Token: 0x06000A71 RID: 2673 RVA: 0x0001E888 File Offset: 0x0001CA88
		internal FieldTableReader(int numberOfRows, int stringHeapRefSize, int blobHeapRefSize, MemoryBlock containingBlock, int containingBlockOffset)
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

		// Token: 0x06000A72 RID: 2674 RVA: 0x0001E8F8 File Offset: 0x0001CAF8
		internal StringHandle GetName(FieldDefinitionHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return StringHandle.FromOffset(this.Block.PeekHeapReference(num + this._NameOffset, this._IsStringHeapRefSizeSmall));
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x0001E934 File Offset: 0x0001CB34
		internal FieldAttributes GetFlags(FieldDefinitionHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return this.Block.PeekUInt16(num + this._FlagsOffset);
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x0001E968 File Offset: 0x0001CB68
		internal BlobHandle GetSignature(FieldDefinitionHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return BlobHandle.FromOffset(this.Block.PeekHeapReference(num + this._SignatureOffset, this._IsBlobHeapRefSizeSmall));
		}

		// Token: 0x0400076E RID: 1902
		internal readonly int NumberOfRows;

		// Token: 0x0400076F RID: 1903
		private readonly bool _IsStringHeapRefSizeSmall;

		// Token: 0x04000770 RID: 1904
		private readonly bool _IsBlobHeapRefSizeSmall;

		// Token: 0x04000771 RID: 1905
		private readonly int _FlagsOffset;

		// Token: 0x04000772 RID: 1906
		private readonly int _NameOffset;

		// Token: 0x04000773 RID: 1907
		private readonly int _SignatureOffset;

		// Token: 0x04000774 RID: 1908
		internal readonly int RowSize;

		// Token: 0x04000775 RID: 1909
		internal readonly MemoryBlock Block;
	}
}
