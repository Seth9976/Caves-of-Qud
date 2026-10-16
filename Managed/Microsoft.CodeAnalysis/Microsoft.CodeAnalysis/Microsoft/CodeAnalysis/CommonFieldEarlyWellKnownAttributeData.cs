using System;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200021B RID: 539
	internal class CommonFieldEarlyWellKnownAttributeData : EarlyWellKnownAttributeData
	{
		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x0600152F RID: 5423 RVA: 0x00043DF1 File Offset: 0x00041FF1
		// (set) Token: 0x06001530 RID: 5424 RVA: 0x00043E08 File Offset: 0x00042008
		public ObsoleteAttributeData ObsoleteAttributeData
		{
			get
			{
				if (!this._obsoleteAttributeData.IsUninitialized)
				{
					return this._obsoleteAttributeData;
				}
				return null;
			}
			set
			{
				this._obsoleteAttributeData = value;
			}
		}

		// Token: 0x040008B5 RID: 2229
		private ObsoleteAttributeData _obsoleteAttributeData = ObsoleteAttributeData.Uninitialized;
	}
}
