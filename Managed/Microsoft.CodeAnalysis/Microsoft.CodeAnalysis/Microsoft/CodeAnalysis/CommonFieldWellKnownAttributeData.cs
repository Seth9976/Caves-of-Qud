using System;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200021C RID: 540
	internal class CommonFieldWellKnownAttributeData : WellKnownAttributeData, IMarshalAsAttributeTarget
	{
		// Token: 0x06001532 RID: 5426 RVA: 0x00043E24 File Offset: 0x00042024
		public CommonFieldWellKnownAttributeData()
		{
			this._offset = -1;
		}

		// Token: 0x06001533 RID: 5427 RVA: 0x00043E3E File Offset: 0x0004203E
		public void SetFieldOffset(int offset)
		{
			this._offset = offset;
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06001534 RID: 5428 RVA: 0x00043E48 File Offset: 0x00042048
		public int? Offset
		{
			get
			{
				if (this._offset == -1)
				{
					return default(int?);
				}
				return new int?(this._offset);
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06001535 RID: 5429 RVA: 0x00043E73 File Offset: 0x00042073
		// (set) Token: 0x06001536 RID: 5430 RVA: 0x00043E7B File Offset: 0x0004207B
		public ConstantValue ConstValue
		{
			get
			{
				return this._constValue;
			}
			set
			{
				this._constValue = value;
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06001537 RID: 5431 RVA: 0x00043E84 File Offset: 0x00042084
		// (set) Token: 0x06001538 RID: 5432 RVA: 0x00043E8C File Offset: 0x0004208C
		public bool HasSpecialNameAttribute
		{
			get
			{
				return this._hasSpecialNameAttribute;
			}
			set
			{
				this._hasSpecialNameAttribute = value;
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06001539 RID: 5433 RVA: 0x00043E95 File Offset: 0x00042095
		// (set) Token: 0x0600153A RID: 5434 RVA: 0x00043E9D File Offset: 0x0004209D
		public bool HasNonSerializedAttribute
		{
			get
			{
				return this._hasNonSerializedAttribute;
			}
			set
			{
				this._hasNonSerializedAttribute = value;
			}
		}

		// Token: 0x0600153B RID: 5435 RVA: 0x00043EA6 File Offset: 0x000420A6
		MarshalPseudoCustomAttributeData IMarshalAsAttributeTarget.GetOrCreateData()
		{
			if (this._lazyMarshalAsData == null)
			{
				this._lazyMarshalAsData = new MarshalPseudoCustomAttributeData();
			}
			return this._lazyMarshalAsData;
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x0600153C RID: 5436 RVA: 0x00043EC1 File Offset: 0x000420C1
		public MarshalPseudoCustomAttributeData MarshallingInformation
		{
			get
			{
				return this._lazyMarshalAsData;
			}
		}

		// Token: 0x040008B6 RID: 2230
		private int _offset;

		// Token: 0x040008B7 RID: 2231
		private const int Uninitialized = -1;

		// Token: 0x040008B8 RID: 2232
		private ConstantValue _constValue = ConstantValue.Unset;

		// Token: 0x040008B9 RID: 2233
		private bool _hasSpecialNameAttribute;

		// Token: 0x040008BA RID: 2234
		private bool _hasNonSerializedAttribute;

		// Token: 0x040008BB RID: 2235
		private MarshalPseudoCustomAttributeData _lazyMarshalAsData;
	}
}
