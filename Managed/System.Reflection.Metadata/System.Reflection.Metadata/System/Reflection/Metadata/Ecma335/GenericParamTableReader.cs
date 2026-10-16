using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000155 RID: 341
	internal readonly struct GenericParamTableReader
	{
		// Token: 0x06000B0E RID: 2830 RVA: 0x000211E8 File Offset: 0x0001F3E8
		internal GenericParamTableReader(int numberOfRows, bool declaredSorted, int typeOrMethodDefRefSize, int stringHeapRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsTypeOrMethodDefRefSizeSmall = typeOrMethodDefRefSize == 2;
			this._IsStringHeapRefSizeSmall = stringHeapRefSize == 2;
			this._NumberOffset = 0;
			this._FlagsOffset = this._NumberOffset + 2;
			this._OwnerOffset = this._FlagsOffset + 2;
			this._NameOffset = this._OwnerOffset + typeOrMethodDefRefSize;
			this.RowSize = this._NameOffset + stringHeapRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
			if (!declaredSorted && !this.CheckSorted())
			{
				Throw.TableNotSorted(TableIndex.GenericParam);
			}
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x0002127C File Offset: 0x0001F47C
		internal ushort GetNumber(GenericParameterHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return this.Block.PeekUInt16(num + this._NumberOffset);
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x000212B0 File Offset: 0x0001F4B0
		internal GenericParameterAttributes GetFlags(GenericParameterHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return this.Block.PeekUInt16(num + this._FlagsOffset);
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x000212E4 File Offset: 0x0001F4E4
		internal StringHandle GetName(GenericParameterHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return StringHandle.FromOffset(this.Block.PeekHeapReference(num + this._NameOffset, this._IsStringHeapRefSizeSmall));
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x00021320 File Offset: 0x0001F520
		internal EntityHandle GetOwner(GenericParameterHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return TypeOrMethodDefTag.ConvertToHandle(this.Block.PeekTaggedReference(num + this._OwnerOffset, this._IsTypeOrMethodDefRefSizeSmall));
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x0002135C File Offset: 0x0001F55C
		internal GenericParameterHandleCollection FindGenericParametersForType(TypeDefinitionHandle typeDef)
		{
			ushort num = 0;
			uint num2 = TypeOrMethodDefTag.ConvertTypeDefRowIdToTag(typeDef);
			int num3 = this.BinarySearchTag(num2, ref num);
			return new GenericParameterHandleCollection(num3, num);
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x00021384 File Offset: 0x0001F584
		internal GenericParameterHandleCollection FindGenericParametersForMethod(MethodDefinitionHandle methodDef)
		{
			ushort num = 0;
			uint num2 = TypeOrMethodDefTag.ConvertMethodDefToTag(methodDef);
			int num3 = this.BinarySearchTag(num2, ref num);
			return new GenericParameterHandleCollection(num3, num);
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x000213AC File Offset: 0x0001F5AC
		private int BinarySearchTag(uint searchCodedTag, ref ushort genericParamCount)
		{
			int num;
			int num2;
			this.Block.BinarySearchReferenceRange(this.NumberOfRows, this.RowSize, this._OwnerOffset, searchCodedTag, this._IsTypeOrMethodDefRefSizeSmall, out num, out num2);
			if (num == -1)
			{
				genericParamCount = 0;
				return 0;
			}
			genericParamCount = (ushort)(num2 - num + 1);
			return num + 1;
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x000213F5 File Offset: 0x0001F5F5
		private bool CheckSorted()
		{
			return this.Block.IsOrderedByReferenceAscending(this.RowSize, this._OwnerOffset, this._IsTypeOrMethodDefRefSizeSmall);
		}

		// Token: 0x04000887 RID: 2183
		internal readonly int NumberOfRows;

		// Token: 0x04000888 RID: 2184
		private readonly bool _IsTypeOrMethodDefRefSizeSmall;

		// Token: 0x04000889 RID: 2185
		private readonly bool _IsStringHeapRefSizeSmall;

		// Token: 0x0400088A RID: 2186
		private readonly int _NumberOffset;

		// Token: 0x0400088B RID: 2187
		private readonly int _FlagsOffset;

		// Token: 0x0400088C RID: 2188
		private readonly int _OwnerOffset;

		// Token: 0x0400088D RID: 2189
		private readonly int _NameOffset;

		// Token: 0x0400088E RID: 2190
		internal readonly int RowSize;

		// Token: 0x0400088F RID: 2191
		internal readonly MemoryBlock Block;
	}
}
