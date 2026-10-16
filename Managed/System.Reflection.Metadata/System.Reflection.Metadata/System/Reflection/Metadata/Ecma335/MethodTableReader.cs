using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000131 RID: 305
	internal readonly struct MethodTableReader
	{
		// Token: 0x06000A78 RID: 2680 RVA: 0x0001EA3C File Offset: 0x0001CC3C
		internal MethodTableReader(int numberOfRows, int paramRefSize, int stringHeapRefSize, int blobHeapRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsParamRefSizeSmall = paramRefSize == 2;
			this._IsStringHeapRefSizeSmall = stringHeapRefSize == 2;
			this._IsBlobHeapRefSizeSmall = blobHeapRefSize == 2;
			this._RvaOffset = 0;
			this._ImplFlagsOffset = this._RvaOffset + 4;
			this._FlagsOffset = this._ImplFlagsOffset + 2;
			this._NameOffset = this._FlagsOffset + 2;
			this._SignatureOffset = this._NameOffset + stringHeapRefSize;
			this._ParamListOffset = this._SignatureOffset + blobHeapRefSize;
			this.RowSize = this._ParamListOffset + paramRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x0001EAE4 File Offset: 0x0001CCE4
		internal int GetParamStart(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return this.Block.PeekReference(num + this._ParamListOffset, this._IsParamRefSizeSmall);
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x0001EB18 File Offset: 0x0001CD18
		internal BlobHandle GetSignature(MethodDefinitionHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return BlobHandle.FromOffset(this.Block.PeekHeapReference(num + this._SignatureOffset, this._IsBlobHeapRefSizeSmall));
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x0001EB54 File Offset: 0x0001CD54
		internal int GetRva(MethodDefinitionHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return this.Block.PeekInt32(num + this._RvaOffset);
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x0001EB88 File Offset: 0x0001CD88
		internal StringHandle GetName(MethodDefinitionHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return StringHandle.FromOffset(this.Block.PeekHeapReference(num + this._NameOffset, this._IsStringHeapRefSizeSmall));
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x0001EBC4 File Offset: 0x0001CDC4
		internal MethodAttributes GetFlags(MethodDefinitionHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return this.Block.PeekUInt16(num + this._FlagsOffset);
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x0001EBF8 File Offset: 0x0001CDF8
		internal MethodImplAttributes GetImplFlags(MethodDefinitionHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return this.Block.PeekUInt16(num + this._ImplFlagsOffset);
		}

		// Token: 0x0400077B RID: 1915
		internal readonly int NumberOfRows;

		// Token: 0x0400077C RID: 1916
		private readonly bool _IsParamRefSizeSmall;

		// Token: 0x0400077D RID: 1917
		private readonly bool _IsStringHeapRefSizeSmall;

		// Token: 0x0400077E RID: 1918
		private readonly bool _IsBlobHeapRefSizeSmall;

		// Token: 0x0400077F RID: 1919
		private readonly int _RvaOffset;

		// Token: 0x04000780 RID: 1920
		private readonly int _ImplFlagsOffset;

		// Token: 0x04000781 RID: 1921
		private readonly int _FlagsOffset;

		// Token: 0x04000782 RID: 1922
		private readonly int _NameOffset;

		// Token: 0x04000783 RID: 1923
		private readonly int _SignatureOffset;

		// Token: 0x04000784 RID: 1924
		private readonly int _ParamListOffset;

		// Token: 0x04000785 RID: 1925
		internal readonly int RowSize;

		// Token: 0x04000786 RID: 1926
		internal readonly MemoryBlock Block;
	}
}
