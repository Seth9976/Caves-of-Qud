using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x0200012C RID: 300
	internal readonly struct TypeRefTableReader
	{
		// Token: 0x06000A60 RID: 2656 RVA: 0x0001E370 File Offset: 0x0001C570
		internal TypeRefTableReader(int numberOfRows, int resolutionScopeRefSize, int stringHeapRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsResolutionScopeRefSizeSmall = resolutionScopeRefSize == 2;
			this._IsStringHeapRefSizeSmall = stringHeapRefSize == 2;
			this._ResolutionScopeOffset = 0;
			this._NameOffset = this._ResolutionScopeOffset + resolutionScopeRefSize;
			this._NamespaceOffset = this._NameOffset + stringHeapRefSize;
			this.RowSize = this._NamespaceOffset + stringHeapRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x0001E3E0 File Offset: 0x0001C5E0
		internal EntityHandle GetResolutionScope(TypeReferenceHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return ResolutionScopeTag.ConvertToHandle(this.Block.PeekTaggedReference(num + this._ResolutionScopeOffset, this._IsResolutionScopeRefSizeSmall));
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x0001E41C File Offset: 0x0001C61C
		internal StringHandle GetName(TypeReferenceHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return StringHandle.FromOffset(this.Block.PeekHeapReference(num + this._NameOffset, this._IsStringHeapRefSizeSmall));
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x0001E458 File Offset: 0x0001C658
		internal StringHandle GetNamespace(TypeReferenceHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return StringHandle.FromOffset(this.Block.PeekHeapReference(num + this._NamespaceOffset, this._IsStringHeapRefSizeSmall));
		}

		// Token: 0x04000754 RID: 1876
		internal readonly int NumberOfRows;

		// Token: 0x04000755 RID: 1877
		private readonly bool _IsResolutionScopeRefSizeSmall;

		// Token: 0x04000756 RID: 1878
		private readonly bool _IsStringHeapRefSizeSmall;

		// Token: 0x04000757 RID: 1879
		private readonly int _ResolutionScopeOffset;

		// Token: 0x04000758 RID: 1880
		private readonly int _NameOffset;

		// Token: 0x04000759 RID: 1881
		private readonly int _NamespaceOffset;

		// Token: 0x0400075A RID: 1882
		internal readonly int RowSize;

		// Token: 0x0400075B RID: 1883
		internal readonly MemoryBlock Block;
	}
}
