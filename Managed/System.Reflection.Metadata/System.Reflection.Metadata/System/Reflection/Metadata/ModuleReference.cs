using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x020000B0 RID: 176
	public readonly struct ModuleReference
	{
		// Token: 0x060006CA RID: 1738 RVA: 0x000113DD File Offset: 0x0000F5DD
		[NullableContext(1)]
		internal ModuleReference(MetadataReader reader, ModuleReferenceHandle handle)
		{
			this._reader = reader;
			this._rowId = handle.RowId;
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x060006CB RID: 1739 RVA: 0x000113F3 File Offset: 0x0000F5F3
		private ModuleReferenceHandle Handle
		{
			get
			{
				return ModuleReferenceHandle.FromRowId(this._rowId);
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x060006CC RID: 1740 RVA: 0x00011400 File Offset: 0x0000F600
		public StringHandle Name
		{
			get
			{
				return this._reader.ModuleRefTable.GetName(this.Handle);
			}
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x00011418 File Offset: 0x0000F618
		public CustomAttributeHandleCollection GetCustomAttributes()
		{
			return new CustomAttributeHandleCollection(this._reader, this.Handle);
		}

		// Token: 0x04000437 RID: 1079
		private readonly MetadataReader _reader;

		// Token: 0x04000438 RID: 1080
		private readonly int _rowId;
	}
}
