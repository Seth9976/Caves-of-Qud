using System;
using System.Reflection.Internal;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x0200014B RID: 331
	internal readonly struct AssemblyTableReader
	{
		// Token: 0x06000AE6 RID: 2790 RVA: 0x000206C0 File Offset: 0x0001E8C0
		internal AssemblyTableReader(int numberOfRows, int stringHeapRefSize, int blobHeapRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = ((numberOfRows > 1) ? 1 : numberOfRows);
			this._IsStringHeapRefSizeSmall = stringHeapRefSize == 2;
			this._IsBlobHeapRefSizeSmall = blobHeapRefSize == 2;
			this._HashAlgIdOffset = 0;
			this._MajorVersionOffset = this._HashAlgIdOffset + 4;
			this._MinorVersionOffset = this._MajorVersionOffset + 2;
			this._BuildNumberOffset = this._MinorVersionOffset + 2;
			this._RevisionNumberOffset = this._BuildNumberOffset + 2;
			this._FlagsOffset = this._RevisionNumberOffset + 2;
			this._PublicKeyOffset = this._FlagsOffset + 4;
			this._NameOffset = this._PublicKeyOffset + blobHeapRefSize;
			this._CultureOffset = this._NameOffset + stringHeapRefSize;
			this.RowSize = this._CultureOffset + stringHeapRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x0002078B File Offset: 0x0001E98B
		internal AssemblyHashAlgorithm GetHashAlgorithm()
		{
			return (AssemblyHashAlgorithm)this.Block.PeekUInt32(this._HashAlgIdOffset);
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x000207A0 File Offset: 0x0001E9A0
		[NullableContext(1)]
		internal Version GetVersion()
		{
			return new Version((int)this.Block.PeekUInt16(this._MajorVersionOffset), (int)this.Block.PeekUInt16(this._MinorVersionOffset), (int)this.Block.PeekUInt16(this._BuildNumberOffset), (int)this.Block.PeekUInt16(this._RevisionNumberOffset));
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x000207F6 File Offset: 0x0001E9F6
		internal AssemblyFlags GetFlags()
		{
			return (AssemblyFlags)this.Block.PeekUInt32(this._FlagsOffset);
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x00020809 File Offset: 0x0001EA09
		internal BlobHandle GetPublicKey()
		{
			return BlobHandle.FromOffset(this.Block.PeekHeapReference(this._PublicKeyOffset, this._IsBlobHeapRefSizeSmall));
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x00020827 File Offset: 0x0001EA27
		internal StringHandle GetName()
		{
			return StringHandle.FromOffset(this.Block.PeekHeapReference(this._NameOffset, this._IsStringHeapRefSizeSmall));
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x00020845 File Offset: 0x0001EA45
		internal StringHandle GetCulture()
		{
			return StringHandle.FromOffset(this.Block.PeekHeapReference(this._CultureOffset, this._IsStringHeapRefSizeSmall));
		}

		// Token: 0x04000831 RID: 2097
		internal readonly int NumberOfRows;

		// Token: 0x04000832 RID: 2098
		private readonly bool _IsStringHeapRefSizeSmall;

		// Token: 0x04000833 RID: 2099
		private readonly bool _IsBlobHeapRefSizeSmall;

		// Token: 0x04000834 RID: 2100
		private readonly int _HashAlgIdOffset;

		// Token: 0x04000835 RID: 2101
		private readonly int _MajorVersionOffset;

		// Token: 0x04000836 RID: 2102
		private readonly int _MinorVersionOffset;

		// Token: 0x04000837 RID: 2103
		private readonly int _BuildNumberOffset;

		// Token: 0x04000838 RID: 2104
		private readonly int _RevisionNumberOffset;

		// Token: 0x04000839 RID: 2105
		private readonly int _FlagsOffset;

		// Token: 0x0400083A RID: 2106
		private readonly int _PublicKeyOffset;

		// Token: 0x0400083B RID: 2107
		private readonly int _NameOffset;

		// Token: 0x0400083C RID: 2108
		private readonly int _CultureOffset;

		// Token: 0x0400083D RID: 2109
		internal readonly int RowSize;

		// Token: 0x0400083E RID: 2110
		internal readonly MemoryBlock Block;
	}
}
