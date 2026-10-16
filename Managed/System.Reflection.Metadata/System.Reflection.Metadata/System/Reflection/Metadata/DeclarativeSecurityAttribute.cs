using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x02000078 RID: 120
	public readonly struct DeclarativeSecurityAttribute
	{
		// Token: 0x060004FC RID: 1276 RVA: 0x0000C1E8 File Offset: 0x0000A3E8
		[NullableContext(1)]
		internal DeclarativeSecurityAttribute(MetadataReader reader, int rowId)
		{
			this._reader = reader;
			this._rowId = rowId;
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x0000C1F8 File Offset: 0x0000A3F8
		public DeclarativeSecurityAction Action
		{
			get
			{
				return this._reader.DeclSecurityTable.GetAction(this._rowId);
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x0000C210 File Offset: 0x0000A410
		public EntityHandle Parent
		{
			get
			{
				return this._reader.DeclSecurityTable.GetParent(this._rowId);
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x0000C228 File Offset: 0x0000A428
		public BlobHandle PermissionSet
		{
			get
			{
				return this._reader.DeclSecurityTable.GetPermissionSet(this._rowId);
			}
		}

		// Token: 0x0400032C RID: 812
		private readonly MetadataReader _reader;

		// Token: 0x0400032D RID: 813
		private readonly int _rowId;
	}
}
