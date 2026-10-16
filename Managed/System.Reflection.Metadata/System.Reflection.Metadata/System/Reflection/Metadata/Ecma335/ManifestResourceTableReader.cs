using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000153 RID: 339
	internal readonly struct ManifestResourceTableReader
	{
		// Token: 0x06000B04 RID: 2820 RVA: 0x00020F20 File Offset: 0x0001F120
		internal ManifestResourceTableReader(int numberOfRows, int implementationRefSize, int stringHeapRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsImplementationRefSizeSmall = implementationRefSize == 2;
			this._IsStringHeapRefSizeSmall = stringHeapRefSize == 2;
			this._OffsetOffset = 0;
			this._FlagsOffset = this._OffsetOffset + 4;
			this._NameOffset = this._FlagsOffset + 4;
			this._ImplementationOffset = this._NameOffset + stringHeapRefSize;
			this.RowSize = this._ImplementationOffset + implementationRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x00020FA0 File Offset: 0x0001F1A0
		internal StringHandle GetName(ManifestResourceHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return StringHandle.FromOffset(this.Block.PeekHeapReference(num + this._NameOffset, this._IsStringHeapRefSizeSmall));
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x00020FDC File Offset: 0x0001F1DC
		internal EntityHandle GetImplementation(ManifestResourceHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return ImplementationTag.ConvertToHandle(this.Block.PeekTaggedReference(num + this._ImplementationOffset, this._IsImplementationRefSizeSmall));
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x00021018 File Offset: 0x0001F218
		internal uint GetOffset(ManifestResourceHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return this.Block.PeekUInt32(num + this._OffsetOffset);
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x0002104C File Offset: 0x0001F24C
		internal ManifestResourceAttributes GetFlags(ManifestResourceHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return (ManifestResourceAttributes)this.Block.PeekUInt32(num + this._FlagsOffset);
		}

		// Token: 0x04000878 RID: 2168
		internal readonly int NumberOfRows;

		// Token: 0x04000879 RID: 2169
		private readonly bool _IsImplementationRefSizeSmall;

		// Token: 0x0400087A RID: 2170
		private readonly bool _IsStringHeapRefSizeSmall;

		// Token: 0x0400087B RID: 2171
		private readonly int _OffsetOffset;

		// Token: 0x0400087C RID: 2172
		private readonly int _FlagsOffset;

		// Token: 0x0400087D RID: 2173
		private readonly int _NameOffset;

		// Token: 0x0400087E RID: 2174
		private readonly int _ImplementationOffset;

		// Token: 0x0400087F RID: 2175
		internal readonly int RowSize;

		// Token: 0x04000880 RID: 2176
		internal readonly MemoryBlock Block;
	}
}
