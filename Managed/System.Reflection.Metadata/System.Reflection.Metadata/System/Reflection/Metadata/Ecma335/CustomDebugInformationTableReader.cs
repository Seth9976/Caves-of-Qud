using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x0200016B RID: 363
	internal readonly struct CustomDebugInformationTableReader
	{
		// Token: 0x06000B4F RID: 2895 RVA: 0x0002274C File Offset: 0x0002094C
		internal CustomDebugInformationTableReader(int numberOfRows, bool declaredSorted, int hasCustomDebugInformationRefSize, int guidHeapRefSize, int blobHeapRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._isHasCustomDebugInformationRefSizeSmall = hasCustomDebugInformationRefSize == 2;
			this._isGuidHeapRefSizeSmall = guidHeapRefSize == 2;
			this._isBlobHeapRefSizeSmall = blobHeapRefSize == 2;
			this._kindOffset = hasCustomDebugInformationRefSize;
			this._valueOffset = this._kindOffset + guidHeapRefSize;
			this.RowSize = this._valueOffset + blobHeapRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
			if (numberOfRows > 0 && !declaredSorted)
			{
				Throw.TableNotSorted(TableIndex.CustomDebugInformation);
			}
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x000227CC File Offset: 0x000209CC
		internal EntityHandle GetParent(CustomDebugInformationHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return HasCustomDebugInformationTag.ConvertToHandle(this.Block.PeekTaggedReference(num, this._isHasCustomDebugInformationRefSizeSmall));
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x00022804 File Offset: 0x00020A04
		internal GuidHandle GetKind(CustomDebugInformationHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return GuidHandle.FromIndex(this.Block.PeekHeapReference(num + this._kindOffset, this._isGuidHeapRefSizeSmall));
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x00022840 File Offset: 0x00020A40
		internal BlobHandle GetValue(CustomDebugInformationHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return BlobHandle.FromOffset(this.Block.PeekHeapReference(num + this._valueOffset, this._isBlobHeapRefSizeSmall));
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x0002287C File Offset: 0x00020A7C
		internal void GetRange(EntityHandle parentHandle, out int firstImplRowId, out int lastImplRowId)
		{
			int num;
			int num2;
			this.Block.BinarySearchReferenceRange(this.NumberOfRows, this.RowSize, 0, HasCustomDebugInformationTag.ConvertToTag(parentHandle), this._isHasCustomDebugInformationRefSizeSmall, out num, out num2);
			if (num == -1)
			{
				firstImplRowId = 1;
				lastImplRowId = 0;
				return;
			}
			firstImplRowId = num + 1;
			lastImplRowId = num2 + 1;
		}

		// Token: 0x04000937 RID: 2359
		internal readonly int NumberOfRows;

		// Token: 0x04000938 RID: 2360
		private readonly bool _isHasCustomDebugInformationRefSizeSmall;

		// Token: 0x04000939 RID: 2361
		private readonly bool _isGuidHeapRefSizeSmall;

		// Token: 0x0400093A RID: 2362
		private readonly bool _isBlobHeapRefSizeSmall;

		// Token: 0x0400093B RID: 2363
		private const int ParentOffset = 0;

		// Token: 0x0400093C RID: 2364
		private readonly int _kindOffset;

		// Token: 0x0400093D RID: 2365
		private readonly int _valueOffset;

		// Token: 0x0400093E RID: 2366
		internal readonly int RowSize;

		// Token: 0x0400093F RID: 2367
		internal readonly MemoryBlock Block;
	}
}
