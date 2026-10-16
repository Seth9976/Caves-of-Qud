using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000147 RID: 327
	internal readonly struct ImplMapTableReader
	{
		// Token: 0x06000AD7 RID: 2775 RVA: 0x00020308 File Offset: 0x0001E508
		internal ImplMapTableReader(int numberOfRows, bool declaredSorted, int moduleRefTableRowRefSize, int memberForwardedRefSize, int stringHeapRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsModuleRefTableRowRefSizeSmall = moduleRefTableRowRefSize == 2;
			this._IsMemberForwardRowRefSizeSmall = memberForwardedRefSize == 2;
			this._IsStringHeapRefSizeSmall = stringHeapRefSize == 2;
			this._FlagsOffset = 0;
			this._MemberForwardedOffset = this._FlagsOffset + 2;
			this._ImportNameOffset = this._MemberForwardedOffset + memberForwardedRefSize;
			this._ImportScopeOffset = this._ImportNameOffset + stringHeapRefSize;
			this.RowSize = this._ImportScopeOffset + moduleRefTableRowRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
			if (!declaredSorted && !this.CheckSorted())
			{
				Throw.TableNotSorted(TableIndex.ImplMap);
			}
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x000203A8 File Offset: 0x0001E5A8
		internal MethodImport GetImport(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			MethodImportAttributes methodImportAttributes = (MethodImportAttributes)this.Block.PeekUInt16(num + this._FlagsOffset);
			StringHandle stringHandle = StringHandle.FromOffset(this.Block.PeekHeapReference(num + this._ImportNameOffset, this._IsStringHeapRefSizeSmall));
			ModuleReferenceHandle moduleReferenceHandle = ModuleReferenceHandle.FromRowId(this.Block.PeekReference(num + this._ImportScopeOffset, this._IsModuleRefTableRowRefSizeSmall));
			return new MethodImport(methodImportAttributes, stringHandle, moduleReferenceHandle);
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x0002041C File Offset: 0x0001E61C
		internal EntityHandle GetMemberForwarded(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return MemberForwardedTag.ConvertToHandle(this.Block.PeekTaggedReference(num + this._MemberForwardedOffset, this._IsMemberForwardRowRefSizeSmall));
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x00020454 File Offset: 0x0001E654
		internal int FindImplForMethod(MethodDefinitionHandle methodDef)
		{
			uint num = MemberForwardedTag.ConvertMethodDefToTag(methodDef);
			return this.BinarySearchTag(num);
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x00020470 File Offset: 0x0001E670
		private int BinarySearchTag(uint searchCodedTag)
		{
			int num = this.Block.BinarySearchReference(this.NumberOfRows, this.RowSize, this._MemberForwardedOffset, searchCodedTag, this._IsMemberForwardRowRefSizeSmall);
			return num + 1;
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x000204A5 File Offset: 0x0001E6A5
		private bool CheckSorted()
		{
			return this.Block.IsOrderedByReferenceAscending(this.RowSize, this._MemberForwardedOffset, this._IsMemberForwardRowRefSizeSmall);
		}

		// Token: 0x04000818 RID: 2072
		internal readonly int NumberOfRows;

		// Token: 0x04000819 RID: 2073
		private readonly bool _IsModuleRefTableRowRefSizeSmall;

		// Token: 0x0400081A RID: 2074
		private readonly bool _IsMemberForwardRowRefSizeSmall;

		// Token: 0x0400081B RID: 2075
		private readonly bool _IsStringHeapRefSizeSmall;

		// Token: 0x0400081C RID: 2076
		private readonly int _FlagsOffset;

		// Token: 0x0400081D RID: 2077
		private readonly int _MemberForwardedOffset;

		// Token: 0x0400081E RID: 2078
		private readonly int _ImportNameOffset;

		// Token: 0x0400081F RID: 2079
		private readonly int _ImportScopeOffset;

		// Token: 0x04000820 RID: 2080
		internal readonly int RowSize;

		// Token: 0x04000821 RID: 2081
		internal readonly MemoryBlock Block;
	}
}
