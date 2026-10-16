using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000157 RID: 343
	internal readonly struct GenericParamConstraintTableReader
	{
		// Token: 0x06000B1A RID: 2842 RVA: 0x000214F0 File Offset: 0x0001F6F0
		internal GenericParamConstraintTableReader(int numberOfRows, bool declaredSorted, int genericParamTableRowRefSize, int typeDefOrRefRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsGenericParamTableRowRefSizeSmall = genericParamTableRowRefSize == 2;
			this._IsTypeDefOrRefRefSizeSmall = typeDefOrRefRefSize == 2;
			this._OwnerOffset = 0;
			this._ConstraintOffset = this._OwnerOffset + genericParamTableRowRefSize;
			this.RowSize = this._ConstraintOffset + typeDefOrRefRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
			if (!declaredSorted && !this.CheckSorted())
			{
				Throw.TableNotSorted(TableIndex.GenericParamConstraint);
			}
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x00021568 File Offset: 0x0001F768
		internal GenericParameterConstraintHandleCollection FindConstraintsForGenericParam(GenericParameterHandle genericParameter)
		{
			int num;
			int num2;
			this.Block.BinarySearchReferenceRange(this.NumberOfRows, this.RowSize, this._OwnerOffset, (uint)genericParameter.RowId, this._IsGenericParamTableRowRefSizeSmall, out num, out num2);
			if (num == -1)
			{
				return default(GenericParameterConstraintHandleCollection);
			}
			return new GenericParameterConstraintHandleCollection(num + 1, (ushort)(num2 - num + 1));
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x000215BF File Offset: 0x0001F7BF
		private bool CheckSorted()
		{
			return this.Block.IsOrderedByReferenceAscending(this.RowSize, this._OwnerOffset, this._IsGenericParamTableRowRefSizeSmall);
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x000215E0 File Offset: 0x0001F7E0
		internal EntityHandle GetConstraint(GenericParameterConstraintHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return TypeDefOrRefTag.ConvertToHandle(this.Block.PeekTaggedReference(num + this._ConstraintOffset, this._IsTypeDefOrRefRefSizeSmall));
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x0002161C File Offset: 0x0001F81C
		internal GenericParameterHandle GetOwner(GenericParameterConstraintHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return GenericParameterHandle.FromRowId(this.Block.PeekReference(num + this._OwnerOffset, this._IsGenericParamTableRowRefSizeSmall));
		}

		// Token: 0x04000897 RID: 2199
		internal readonly int NumberOfRows;

		// Token: 0x04000898 RID: 2200
		private readonly bool _IsGenericParamTableRowRefSizeSmall;

		// Token: 0x04000899 RID: 2201
		private readonly bool _IsTypeDefOrRefRefSizeSmall;

		// Token: 0x0400089A RID: 2202
		private readonly int _OwnerOffset;

		// Token: 0x0400089B RID: 2203
		private readonly int _ConstraintOffset;

		// Token: 0x0400089C RID: 2204
		internal readonly int RowSize;

		// Token: 0x0400089D RID: 2205
		internal readonly MemoryBlock Block;
	}
}
