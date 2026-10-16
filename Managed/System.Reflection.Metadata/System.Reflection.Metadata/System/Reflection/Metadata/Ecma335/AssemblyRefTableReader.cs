using System;
using System.Reflection.Internal;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x0200014E RID: 334
	internal readonly struct AssemblyRefTableReader
	{
		// Token: 0x06000AEF RID: 2799 RVA: 0x000208F4 File Offset: 0x0001EAF4
		internal AssemblyRefTableReader(int numberOfRows, int stringHeapRefSize, int blobHeapRefSize, MemoryBlock containingBlock, int containingBlockOffset, MetadataKind metadataKind)
		{
			this.NumberOfNonVirtualRows = numberOfRows;
			this.NumberOfVirtualRows = ((metadataKind == MetadataKind.Ecma335) ? 0 : 6);
			this._IsStringHeapRefSizeSmall = stringHeapRefSize == 2;
			this._IsBlobHeapRefSizeSmall = blobHeapRefSize == 2;
			this._MajorVersionOffset = 0;
			this._MinorVersionOffset = this._MajorVersionOffset + 2;
			this._BuildNumberOffset = this._MinorVersionOffset + 2;
			this._RevisionNumberOffset = this._BuildNumberOffset + 2;
			this._FlagsOffset = this._RevisionNumberOffset + 2;
			this._PublicKeyOrTokenOffset = this._FlagsOffset + 4;
			this._NameOffset = this._PublicKeyOrTokenOffset + blobHeapRefSize;
			this._CultureOffset = this._NameOffset + stringHeapRefSize;
			this._HashValueOffset = this._CultureOffset + stringHeapRefSize;
			this.RowSize = this._HashValueOffset + blobHeapRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x000209C8 File Offset: 0x0001EBC8
		[NullableContext(1)]
		internal Version GetVersion(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return new Version((int)this.Block.PeekUInt16(num + this._MajorVersionOffset), (int)this.Block.PeekUInt16(num + this._MinorVersionOffset), (int)this.Block.PeekUInt16(num + this._BuildNumberOffset), (int)this.Block.PeekUInt16(num + this._RevisionNumberOffset));
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x00020A34 File Offset: 0x0001EC34
		internal AssemblyFlags GetFlags(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return (AssemblyFlags)this.Block.PeekUInt32(num + this._FlagsOffset);
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x00020A60 File Offset: 0x0001EC60
		internal BlobHandle GetPublicKeyOrToken(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return BlobHandle.FromOffset(this.Block.PeekHeapReference(num + this._PublicKeyOrTokenOffset, this._IsBlobHeapRefSizeSmall));
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x00020A98 File Offset: 0x0001EC98
		internal StringHandle GetName(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return StringHandle.FromOffset(this.Block.PeekHeapReference(num + this._NameOffset, this._IsStringHeapRefSizeSmall));
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x00020AD0 File Offset: 0x0001ECD0
		internal StringHandle GetCulture(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return StringHandle.FromOffset(this.Block.PeekHeapReference(num + this._CultureOffset, this._IsStringHeapRefSizeSmall));
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x00020B08 File Offset: 0x0001ED08
		internal BlobHandle GetHashValue(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return BlobHandle.FromOffset(this.Block.PeekHeapReference(num + this._HashValueOffset, this._IsBlobHeapRefSizeSmall));
		}

		// Token: 0x04000849 RID: 2121
		internal readonly int NumberOfNonVirtualRows;

		// Token: 0x0400084A RID: 2122
		internal readonly int NumberOfVirtualRows;

		// Token: 0x0400084B RID: 2123
		private readonly bool _IsStringHeapRefSizeSmall;

		// Token: 0x0400084C RID: 2124
		private readonly bool _IsBlobHeapRefSizeSmall;

		// Token: 0x0400084D RID: 2125
		private readonly int _MajorVersionOffset;

		// Token: 0x0400084E RID: 2126
		private readonly int _MinorVersionOffset;

		// Token: 0x0400084F RID: 2127
		private readonly int _BuildNumberOffset;

		// Token: 0x04000850 RID: 2128
		private readonly int _RevisionNumberOffset;

		// Token: 0x04000851 RID: 2129
		private readonly int _FlagsOffset;

		// Token: 0x04000852 RID: 2130
		private readonly int _PublicKeyOrTokenOffset;

		// Token: 0x04000853 RID: 2131
		private readonly int _NameOffset;

		// Token: 0x04000854 RID: 2132
		private readonly int _CultureOffset;

		// Token: 0x04000855 RID: 2133
		private readonly int _HashValueOffset;

		// Token: 0x04000856 RID: 2134
		internal readonly int RowSize;

		// Token: 0x04000857 RID: 2135
		internal readonly MemoryBlock Block;
	}
}
