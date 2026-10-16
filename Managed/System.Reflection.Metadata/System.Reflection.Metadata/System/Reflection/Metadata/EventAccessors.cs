using System;
using System.Collections.Immutable;

namespace System.Reflection.Metadata
{
	// Token: 0x02000098 RID: 152
	public readonly struct EventAccessors
	{
		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x0000CF9D File Offset: 0x0000B19D
		public MethodDefinitionHandle Adder
		{
			get
			{
				return MethodDefinitionHandle.FromRowId(this._adderRowId);
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x0000CFAA File Offset: 0x0000B1AA
		public MethodDefinitionHandle Remover
		{
			get
			{
				return MethodDefinitionHandle.FromRowId(this._removerRowId);
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060005A7 RID: 1447 RVA: 0x0000CFB7 File Offset: 0x0000B1B7
		public MethodDefinitionHandle Raiser
		{
			get
			{
				return MethodDefinitionHandle.FromRowId(this._raiserRowId);
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x0000CFC4 File Offset: 0x0000B1C4
		public ImmutableArray<MethodDefinitionHandle> Others
		{
			get
			{
				return this._others;
			}
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0000CFCC File Offset: 0x0000B1CC
		internal EventAccessors(int adderRowId, int removerRowId, int raiserRowId, ImmutableArray<MethodDefinitionHandle> others)
		{
			this._adderRowId = adderRowId;
			this._removerRowId = removerRowId;
			this._raiserRowId = raiserRowId;
			this._others = others;
		}

		// Token: 0x04000387 RID: 903
		private readonly int _adderRowId;

		// Token: 0x04000388 RID: 904
		private readonly int _removerRowId;

		// Token: 0x04000389 RID: 905
		private readonly int _raiserRowId;

		// Token: 0x0400038A RID: 906
		private readonly ImmutableArray<MethodDefinitionHandle> _others;
	}
}
