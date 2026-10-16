using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	/// <summary>Provides information about the lexical scope within which a group of imports are available. This information is stored in debug metadata.</summary>
	// Token: 0x020000C9 RID: 201
	public readonly struct ImportScope
	{
		// Token: 0x06000783 RID: 1923 RVA: 0x000121F2 File Offset: 0x000103F2
		[NullableContext(1)]
		internal ImportScope(MetadataReader reader, ImportScopeHandle handle)
		{
			this._reader = reader;
			this._rowId = handle.RowId;
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000784 RID: 1924 RVA: 0x00012208 File Offset: 0x00010408
		private ImportScopeHandle Handle
		{
			get
			{
				return ImportScopeHandle.FromRowId(this._rowId);
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000785 RID: 1925 RVA: 0x00012215 File Offset: 0x00010415
		public ImportScopeHandle Parent
		{
			get
			{
				return this._reader.ImportScopeTable.GetParent(this.Handle);
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000786 RID: 1926 RVA: 0x0001222D File Offset: 0x0001042D
		public BlobHandle ImportsBlob
		{
			get
			{
				return this._reader.ImportScopeTable.GetImports(this.Handle);
			}
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x00012245 File Offset: 0x00010445
		public ImportDefinitionCollection GetImports()
		{
			return new ImportDefinitionCollection(this._reader.BlobHeap.GetMemoryBlock(this.ImportsBlob));
		}

		// Token: 0x0400047F RID: 1151
		private readonly MetadataReader _reader;

		// Token: 0x04000480 RID: 1152
		private readonly int _rowId;
	}
}
