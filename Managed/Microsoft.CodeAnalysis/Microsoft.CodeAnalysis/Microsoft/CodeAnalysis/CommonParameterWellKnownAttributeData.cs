using System;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000221 RID: 545
	internal class CommonParameterWellKnownAttributeData : WellKnownAttributeData, IMarshalAsAttributeTarget
	{
		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06001564 RID: 5476 RVA: 0x0004417E File Offset: 0x0004237E
		// (set) Token: 0x06001565 RID: 5477 RVA: 0x00044186 File Offset: 0x00042386
		public bool HasOutAttribute
		{
			get
			{
				return this._hasOutAttribute;
			}
			set
			{
				this._hasOutAttribute = value;
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06001566 RID: 5478 RVA: 0x0004418F File Offset: 0x0004238F
		// (set) Token: 0x06001567 RID: 5479 RVA: 0x00044197 File Offset: 0x00042397
		public bool HasInAttribute
		{
			get
			{
				return this._hasInAttribute;
			}
			set
			{
				this._hasInAttribute = value;
			}
		}

		// Token: 0x06001568 RID: 5480 RVA: 0x000441A0 File Offset: 0x000423A0
		MarshalPseudoCustomAttributeData IMarshalAsAttributeTarget.GetOrCreateData()
		{
			if (this._lazyMarshalAsData == null)
			{
				this._lazyMarshalAsData = new MarshalPseudoCustomAttributeData();
			}
			return this._lazyMarshalAsData;
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06001569 RID: 5481 RVA: 0x000441BB File Offset: 0x000423BB
		public MarshalPseudoCustomAttributeData MarshallingInformation
		{
			get
			{
				return this._lazyMarshalAsData;
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x0600156A RID: 5482 RVA: 0x000441C3 File Offset: 0x000423C3
		// (set) Token: 0x0600156B RID: 5483 RVA: 0x000441CB File Offset: 0x000423CB
		public bool HasIDispatchConstantAttribute
		{
			get
			{
				return this._hasIDispatchConstantAttribute;
			}
			set
			{
				this._hasIDispatchConstantAttribute = value;
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x0600156C RID: 5484 RVA: 0x000441D4 File Offset: 0x000423D4
		// (set) Token: 0x0600156D RID: 5485 RVA: 0x000441DC File Offset: 0x000423DC
		public bool HasIUnknownConstantAttribute
		{
			get
			{
				return this._hasIUnknownConstantAttribute;
			}
			set
			{
				this._hasIUnknownConstantAttribute = value;
			}
		}

		// Token: 0x040008D0 RID: 2256
		private bool _hasOutAttribute;

		// Token: 0x040008D1 RID: 2257
		private bool _hasInAttribute;

		// Token: 0x040008D2 RID: 2258
		private MarshalPseudoCustomAttributeData _lazyMarshalAsData;

		// Token: 0x040008D3 RID: 2259
		private bool _hasIDispatchConstantAttribute;

		// Token: 0x040008D4 RID: 2260
		private bool _hasIUnknownConstantAttribute;
	}
}
