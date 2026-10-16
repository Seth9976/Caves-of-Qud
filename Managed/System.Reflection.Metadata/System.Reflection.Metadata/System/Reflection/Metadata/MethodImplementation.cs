using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x020000AC RID: 172
	public readonly struct MethodImplementation
	{
		// Token: 0x060006B3 RID: 1715 RVA: 0x0001120E File Offset: 0x0000F40E
		[NullableContext(1)]
		internal MethodImplementation(MetadataReader reader, MethodImplementationHandle handle)
		{
			this._reader = reader;
			this._rowId = handle.RowId;
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x00011224 File Offset: 0x0000F424
		private MethodImplementationHandle Handle
		{
			get
			{
				return MethodImplementationHandle.FromRowId(this._rowId);
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x060006B5 RID: 1717 RVA: 0x00011231 File Offset: 0x0000F431
		public TypeDefinitionHandle Type
		{
			get
			{
				return this._reader.MethodImplTable.GetClass(this.Handle);
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x060006B6 RID: 1718 RVA: 0x00011249 File Offset: 0x0000F449
		public EntityHandle MethodBody
		{
			get
			{
				return this._reader.MethodImplTable.GetMethodBody(this.Handle);
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x060006B7 RID: 1719 RVA: 0x00011261 File Offset: 0x0000F461
		public EntityHandle MethodDeclaration
		{
			get
			{
				return this._reader.MethodImplTable.GetMethodDeclaration(this.Handle);
			}
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x00011279 File Offset: 0x0000F479
		public CustomAttributeHandleCollection GetCustomAttributes()
		{
			return new CustomAttributeHandleCollection(this._reader, this.Handle);
		}

		// Token: 0x0400042F RID: 1071
		private readonly MetadataReader _reader;

		// Token: 0x04000430 RID: 1072
		private readonly int _rowId;
	}
}
