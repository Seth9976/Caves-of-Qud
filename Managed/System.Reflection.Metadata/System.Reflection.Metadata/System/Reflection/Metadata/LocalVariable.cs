using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	/// <summary>Provides information about local variables. This information is stored in debug metadata.</summary>
	// Token: 0x020000CB RID: 203
	public readonly struct LocalVariable
	{
		// Token: 0x06000792 RID: 1938 RVA: 0x00012336 File Offset: 0x00010536
		[NullableContext(1)]
		internal LocalVariable(MetadataReader reader, LocalVariableHandle handle)
		{
			this._reader = reader;
			this._rowId = handle.RowId;
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000793 RID: 1939 RVA: 0x0001234C File Offset: 0x0001054C
		private LocalVariableHandle Handle
		{
			get
			{
				return LocalVariableHandle.FromRowId(this._rowId);
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000794 RID: 1940 RVA: 0x00012359 File Offset: 0x00010559
		public LocalVariableAttributes Attributes
		{
			get
			{
				return this._reader.LocalVariableTable.GetAttributes(this.Handle);
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000795 RID: 1941 RVA: 0x00012371 File Offset: 0x00010571
		public int Index
		{
			get
			{
				return (int)this._reader.LocalVariableTable.GetIndex(this.Handle);
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000796 RID: 1942 RVA: 0x00012389 File Offset: 0x00010589
		public StringHandle Name
		{
			get
			{
				return this._reader.LocalVariableTable.GetName(this.Handle);
			}
		}

		// Token: 0x04000483 RID: 1155
		private readonly MetadataReader _reader;

		// Token: 0x04000484 RID: 1156
		private readonly int _rowId;
	}
}
