using System;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000224 RID: 548
	internal class CommonReturnTypeWellKnownAttributeData : WellKnownAttributeData, IMarshalAsAttributeTarget
	{
		// Token: 0x06001577 RID: 5495 RVA: 0x0004424A File Offset: 0x0004244A
		MarshalPseudoCustomAttributeData IMarshalAsAttributeTarget.GetOrCreateData()
		{
			if (this._lazyMarshalAsData == null)
			{
				this._lazyMarshalAsData = new MarshalPseudoCustomAttributeData();
			}
			return this._lazyMarshalAsData;
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06001578 RID: 5496 RVA: 0x00044265 File Offset: 0x00042465
		public MarshalPseudoCustomAttributeData MarshallingInformation
		{
			get
			{
				return this._lazyMarshalAsData;
			}
		}

		// Token: 0x040008D8 RID: 2264
		private MarshalPseudoCustomAttributeData _lazyMarshalAsData;
	}
}
