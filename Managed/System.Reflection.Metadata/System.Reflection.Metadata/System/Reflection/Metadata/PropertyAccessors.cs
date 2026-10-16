using System;
using System.Collections.Immutable;

namespace System.Reflection.Metadata
{
	// Token: 0x02000097 RID: 151
	public readonly struct PropertyAccessors
	{
		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x0000CF64 File Offset: 0x0000B164
		public MethodDefinitionHandle Getter
		{
			get
			{
				return MethodDefinitionHandle.FromRowId(this._getterRowId);
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x0000CF71 File Offset: 0x0000B171
		public MethodDefinitionHandle Setter
		{
			get
			{
				return MethodDefinitionHandle.FromRowId(this._setterRowId);
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x0000CF7E File Offset: 0x0000B17E
		public ImmutableArray<MethodDefinitionHandle> Others
		{
			get
			{
				return this._others;
			}
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x0000CF86 File Offset: 0x0000B186
		internal PropertyAccessors(int getterRowId, int setterRowId, ImmutableArray<MethodDefinitionHandle> others)
		{
			this._getterRowId = getterRowId;
			this._setterRowId = setterRowId;
			this._others = others;
		}

		// Token: 0x04000384 RID: 900
		private readonly int _getterRowId;

		// Token: 0x04000385 RID: 901
		private readonly int _setterRowId;

		// Token: 0x04000386 RID: 902
		private readonly ImmutableArray<MethodDefinitionHandle> _others;
	}
}
