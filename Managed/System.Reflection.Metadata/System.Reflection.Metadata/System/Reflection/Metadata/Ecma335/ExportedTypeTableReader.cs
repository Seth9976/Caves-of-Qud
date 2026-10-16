using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000152 RID: 338
	internal readonly struct ExportedTypeTableReader
	{
		// Token: 0x06000AFC RID: 2812 RVA: 0x00020D28 File Offset: 0x0001EF28
		internal ExportedTypeTableReader(int numberOfRows, int implementationRefSize, int stringHeapRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsImplementationRefSizeSmall = implementationRefSize == 2;
			this._IsStringHeapRefSizeSmall = stringHeapRefSize == 2;
			this._FlagsOffset = 0;
			this._TypeDefIdOffset = this._FlagsOffset + 4;
			this._TypeNameOffset = this._TypeDefIdOffset + 4;
			this._TypeNamespaceOffset = this._TypeNameOffset + stringHeapRefSize;
			this._ImplementationOffset = this._TypeNamespaceOffset + stringHeapRefSize;
			this.RowSize = this._ImplementationOffset + implementationRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x00020DB4 File Offset: 0x0001EFB4
		internal StringHandle GetTypeName(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return StringHandle.FromOffset(this.Block.PeekHeapReference(num + this._TypeNameOffset, this._IsStringHeapRefSizeSmall));
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x00020DEC File Offset: 0x0001EFEC
		internal StringHandle GetTypeNamespaceString(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return StringHandle.FromOffset(this.Block.PeekHeapReference(num + this._TypeNamespaceOffset, this._IsStringHeapRefSizeSmall));
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x00020E24 File Offset: 0x0001F024
		internal NamespaceDefinitionHandle GetTypeNamespace(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return NamespaceDefinitionHandle.FromFullNameOffset(this.Block.PeekHeapReference(num + this._TypeNamespaceOffset, this._IsStringHeapRefSizeSmall));
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x00020E5C File Offset: 0x0001F05C
		internal EntityHandle GetImplementation(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return ImplementationTag.ConvertToHandle(this.Block.PeekTaggedReference(num + this._ImplementationOffset, this._IsImplementationRefSizeSmall));
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x00020E94 File Offset: 0x0001F094
		internal TypeAttributes GetFlags(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return this.Block.PeekUInt32(num + this._FlagsOffset);
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x00020EC0 File Offset: 0x0001F0C0
		internal int GetTypeDefId(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return this.Block.PeekInt32(num + this._TypeDefIdOffset);
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x00020EEC File Offset: 0x0001F0EC
		internal int GetNamespace(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return this.Block.PeekReference(num + this._TypeNamespaceOffset, this._IsStringHeapRefSizeSmall);
		}

		// Token: 0x0400086E RID: 2158
		internal readonly int NumberOfRows;

		// Token: 0x0400086F RID: 2159
		private readonly bool _IsImplementationRefSizeSmall;

		// Token: 0x04000870 RID: 2160
		private readonly bool _IsStringHeapRefSizeSmall;

		// Token: 0x04000871 RID: 2161
		private readonly int _FlagsOffset;

		// Token: 0x04000872 RID: 2162
		private readonly int _TypeDefIdOffset;

		// Token: 0x04000873 RID: 2163
		private readonly int _TypeNameOffset;

		// Token: 0x04000874 RID: 2164
		private readonly int _TypeNamespaceOffset;

		// Token: 0x04000875 RID: 2165
		private readonly int _ImplementationOffset;

		// Token: 0x04000876 RID: 2166
		internal readonly int RowSize;

		// Token: 0x04000877 RID: 2167
		internal readonly MemoryBlock Block;
	}
}
