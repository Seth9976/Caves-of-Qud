using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	/// <summary>Provides information about the scope of local variables and constants. This information is stored in debug metadata.</summary>
	// Token: 0x020000CA RID: 202
	public readonly struct LocalScope
	{
		// Token: 0x06000788 RID: 1928 RVA: 0x00012262 File Offset: 0x00010462
		[NullableContext(1)]
		internal LocalScope(MetadataReader reader, LocalScopeHandle handle)
		{
			this._reader = reader;
			this._rowId = handle.RowId;
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000789 RID: 1929 RVA: 0x00012278 File Offset: 0x00010478
		private LocalScopeHandle Handle
		{
			get
			{
				return LocalScopeHandle.FromRowId(this._rowId);
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x0600078A RID: 1930 RVA: 0x00012285 File Offset: 0x00010485
		public MethodDefinitionHandle Method
		{
			get
			{
				return this._reader.LocalScopeTable.GetMethod(this._rowId);
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x0600078B RID: 1931 RVA: 0x0001229D File Offset: 0x0001049D
		public ImportScopeHandle ImportScope
		{
			get
			{
				return this._reader.LocalScopeTable.GetImportScope(this.Handle);
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x0600078C RID: 1932 RVA: 0x000122B5 File Offset: 0x000104B5
		public int StartOffset
		{
			get
			{
				return this._reader.LocalScopeTable.GetStartOffset(this._rowId);
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x0600078D RID: 1933 RVA: 0x000122CD File Offset: 0x000104CD
		public int Length
		{
			get
			{
				return this._reader.LocalScopeTable.GetLength(this._rowId);
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x0600078E RID: 1934 RVA: 0x000122E5 File Offset: 0x000104E5
		public int EndOffset
		{
			get
			{
				return this._reader.LocalScopeTable.GetEndOffset(this._rowId);
			}
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x000122FD File Offset: 0x000104FD
		public LocalVariableHandleCollection GetLocalVariables()
		{
			return new LocalVariableHandleCollection(this._reader, this.Handle);
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x00012310 File Offset: 0x00010510
		public LocalConstantHandleCollection GetLocalConstants()
		{
			return new LocalConstantHandleCollection(this._reader, this.Handle);
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x00012323 File Offset: 0x00010523
		public LocalScopeHandleCollection.ChildrenEnumerator GetChildren()
		{
			return new LocalScopeHandleCollection.ChildrenEnumerator(this._reader, this._rowId);
		}

		// Token: 0x04000481 RID: 1153
		private readonly MetadataReader _reader;

		// Token: 0x04000482 RID: 1154
		private readonly int _rowId;
	}
}
