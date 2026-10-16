using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000166 RID: 358
	internal readonly struct LocalScopeTableReader
	{
		// Token: 0x06000B39 RID: 2873 RVA: 0x00022164 File Offset: 0x00020364
		internal LocalScopeTableReader(int numberOfRows, bool declaredSorted, int methodRefSize, int importScopeRefSize, int localVariableRefSize, int localConstantRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._isMethodRefSmall = methodRefSize == 2;
			this._isImportScopeRefSmall = importScopeRefSize == 2;
			this._isLocalVariableRefSmall = localVariableRefSize == 2;
			this._isLocalConstantRefSmall = localConstantRefSize == 2;
			this._importScopeOffset = methodRefSize;
			this._variableListOffset = this._importScopeOffset + importScopeRefSize;
			this._constantListOffset = this._variableListOffset + localVariableRefSize;
			this._startOffsetOffset = this._constantListOffset + localConstantRefSize;
			this._lengthOffset = this._startOffsetOffset + 4;
			this.RowSize = this._lengthOffset + 4;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
			if (numberOfRows > 0 && !declaredSorted)
			{
				Throw.TableNotSorted(TableIndex.LocalScope);
			}
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x00022218 File Offset: 0x00020418
		internal MethodDefinitionHandle GetMethod(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return MethodDefinitionHandle.FromRowId(this.Block.PeekReference(num, this._isMethodRefSmall));
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x00022248 File Offset: 0x00020448
		internal ImportScopeHandle GetImportScope(LocalScopeHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return ImportScopeHandle.FromRowId(this.Block.PeekReference(num + this._importScopeOffset, this._isImportScopeRefSmall));
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x00022284 File Offset: 0x00020484
		internal int GetVariableStart(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return this.Block.PeekReference(num + this._variableListOffset, this._isLocalVariableRefSmall);
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x000222B8 File Offset: 0x000204B8
		internal int GetConstantStart(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return this.Block.PeekReference(num + this._constantListOffset, this._isLocalConstantRefSmall);
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x000222EC File Offset: 0x000204EC
		internal int GetStartOffset(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return this.Block.PeekInt32(num + this._startOffsetOffset);
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x00022318 File Offset: 0x00020518
		internal int GetLength(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return this.Block.PeekInt32(num + this._lengthOffset);
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x00022344 File Offset: 0x00020544
		internal int GetEndOffset(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			long num2 = (long)((ulong)(this.Block.PeekUInt32(num + this._startOffsetOffset) + this.Block.PeekUInt32(num + this._lengthOffset)));
			if ((long)((int)num2) != num2)
			{
				Throw.ValueOverflow();
			}
			return (int)num2;
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x00022394 File Offset: 0x00020594
		internal void GetLocalScopeRange(int methodDefRid, out int firstScopeRowId, out int lastScopeRowId)
		{
			int num;
			int num2;
			this.Block.BinarySearchReferenceRange(this.NumberOfRows, this.RowSize, 0, (uint)methodDefRid, this._isMethodRefSmall, out num, out num2);
			if (num == -1)
			{
				firstScopeRowId = 1;
				lastScopeRowId = 0;
				return;
			}
			firstScopeRowId = num + 1;
			lastScopeRowId = num2 + 1;
		}

		// Token: 0x0400090F RID: 2319
		internal readonly int NumberOfRows;

		// Token: 0x04000910 RID: 2320
		private readonly bool _isMethodRefSmall;

		// Token: 0x04000911 RID: 2321
		private readonly bool _isImportScopeRefSmall;

		// Token: 0x04000912 RID: 2322
		private readonly bool _isLocalConstantRefSmall;

		// Token: 0x04000913 RID: 2323
		private readonly bool _isLocalVariableRefSmall;

		// Token: 0x04000914 RID: 2324
		private const int MethodOffset = 0;

		// Token: 0x04000915 RID: 2325
		private readonly int _importScopeOffset;

		// Token: 0x04000916 RID: 2326
		private readonly int _variableListOffset;

		// Token: 0x04000917 RID: 2327
		private readonly int _constantListOffset;

		// Token: 0x04000918 RID: 2328
		private readonly int _startOffsetOffset;

		// Token: 0x04000919 RID: 2329
		private readonly int _lengthOffset;

		// Token: 0x0400091A RID: 2330
		internal readonly int RowSize;

		// Token: 0x0400091B RID: 2331
		internal readonly MemoryBlock Block;
	}
}
