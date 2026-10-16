using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x0200012D RID: 301
	internal struct TypeDefTableReader
	{
		// Token: 0x06000A64 RID: 2660 RVA: 0x0001E494 File Offset: 0x0001C694
		internal TypeDefTableReader(int numberOfRows, int fieldRefSize, int methodRefSize, int typeDefOrRefRefSize, int stringHeapRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsFieldRefSizeSmall = fieldRefSize == 2;
			this._IsMethodRefSizeSmall = methodRefSize == 2;
			this._IsTypeDefOrRefRefSizeSmall = typeDefOrRefRefSize == 2;
			this._IsStringHeapRefSizeSmall = stringHeapRefSize == 2;
			this._FlagsOffset = 0;
			this._NameOffset = this._FlagsOffset + 4;
			this._NamespaceOffset = this._NameOffset + stringHeapRefSize;
			this._ExtendsOffset = this._NamespaceOffset + stringHeapRefSize;
			this._FieldListOffset = this._ExtendsOffset + typeDefOrRefRefSize;
			this._MethodListOffset = this._FieldListOffset + fieldRefSize;
			this.RowSize = this._MethodListOffset + methodRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x0001E548 File Offset: 0x0001C748
		internal TypeAttributes GetFlags(TypeDefinitionHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return this.Block.PeekUInt32(num + this._FlagsOffset);
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x0001E57C File Offset: 0x0001C77C
		internal NamespaceDefinitionHandle GetNamespaceDefinition(TypeDefinitionHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return NamespaceDefinitionHandle.FromFullNameOffset(this.Block.PeekHeapReference(num + this._NamespaceOffset, this._IsStringHeapRefSizeSmall));
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x0001E5B8 File Offset: 0x0001C7B8
		internal StringHandle GetNamespace(TypeDefinitionHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return StringHandle.FromOffset(this.Block.PeekHeapReference(num + this._NamespaceOffset, this._IsStringHeapRefSizeSmall));
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x0001E5F4 File Offset: 0x0001C7F4
		internal StringHandle GetName(TypeDefinitionHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return StringHandle.FromOffset(this.Block.PeekHeapReference(num + this._NameOffset, this._IsStringHeapRefSizeSmall));
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x0001E630 File Offset: 0x0001C830
		internal EntityHandle GetExtends(TypeDefinitionHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return TypeDefOrRefTag.ConvertToHandle(this.Block.PeekTaggedReference(num + this._ExtendsOffset, this._IsTypeDefOrRefRefSizeSmall));
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x0001E66C File Offset: 0x0001C86C
		internal int GetFieldStart(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return this.Block.PeekReference(num + this._FieldListOffset, this._IsFieldRefSizeSmall);
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x0001E6A0 File Offset: 0x0001C8A0
		internal int GetMethodStart(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return this.Block.PeekReference(num + this._MethodListOffset, this._IsMethodRefSizeSmall);
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x0001E6D4 File Offset: 0x0001C8D4
		internal TypeDefinitionHandle FindTypeContainingMethod(int methodDefOrPtrRowId, int numberOfMethods)
		{
			int numberOfRows = this.NumberOfRows;
			int num = this.Block.BinarySearchForSlot(numberOfRows, this.RowSize, this._MethodListOffset, (uint)methodDefOrPtrRowId, this._IsMethodRefSizeSmall);
			int i = num + 1;
			if (i == 0)
			{
				return default(TypeDefinitionHandle);
			}
			if (i <= numberOfRows)
			{
				int num2 = this.GetMethodStart(i);
				if (num2 == methodDefOrPtrRowId)
				{
					while (i < numberOfRows)
					{
						int num3 = i + 1;
						num2 = this.GetMethodStart(num3);
						if (num2 != methodDefOrPtrRowId)
						{
							break;
						}
						i = num3;
					}
				}
				return TypeDefinitionHandle.FromRowId(i);
			}
			if (methodDefOrPtrRowId <= numberOfMethods)
			{
				return TypeDefinitionHandle.FromRowId(numberOfRows);
			}
			return default(TypeDefinitionHandle);
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x0001E764 File Offset: 0x0001C964
		internal TypeDefinitionHandle FindTypeContainingField(int fieldDefOrPtrRowId, int numberOfFields)
		{
			int numberOfRows = this.NumberOfRows;
			int num = this.Block.BinarySearchForSlot(numberOfRows, this.RowSize, this._FieldListOffset, (uint)fieldDefOrPtrRowId, this._IsFieldRefSizeSmall);
			int i = num + 1;
			if (i == 0)
			{
				return default(TypeDefinitionHandle);
			}
			if (i <= numberOfRows)
			{
				int num2 = this.GetFieldStart(i);
				if (num2 == fieldDefOrPtrRowId)
				{
					while (i < numberOfRows)
					{
						int num3 = i + 1;
						num2 = this.GetFieldStart(num3);
						if (num2 != fieldDefOrPtrRowId)
						{
							break;
						}
						i = num3;
					}
				}
				return TypeDefinitionHandle.FromRowId(i);
			}
			if (fieldDefOrPtrRowId <= numberOfFields)
			{
				return TypeDefinitionHandle.FromRowId(numberOfRows);
			}
			return default(TypeDefinitionHandle);
		}

		// Token: 0x0400075C RID: 1884
		internal readonly int NumberOfRows;

		// Token: 0x0400075D RID: 1885
		private readonly bool _IsFieldRefSizeSmall;

		// Token: 0x0400075E RID: 1886
		private readonly bool _IsMethodRefSizeSmall;

		// Token: 0x0400075F RID: 1887
		private readonly bool _IsTypeDefOrRefRefSizeSmall;

		// Token: 0x04000760 RID: 1888
		private readonly bool _IsStringHeapRefSizeSmall;

		// Token: 0x04000761 RID: 1889
		private readonly int _FlagsOffset;

		// Token: 0x04000762 RID: 1890
		private readonly int _NameOffset;

		// Token: 0x04000763 RID: 1891
		private readonly int _NamespaceOffset;

		// Token: 0x04000764 RID: 1892
		private readonly int _ExtendsOffset;

		// Token: 0x04000765 RID: 1893
		private readonly int _FieldListOffset;

		// Token: 0x04000766 RID: 1894
		private readonly int _MethodListOffset;

		// Token: 0x04000767 RID: 1895
		internal readonly int RowSize;

		// Token: 0x04000768 RID: 1896
		internal MemoryBlock Block;
	}
}
