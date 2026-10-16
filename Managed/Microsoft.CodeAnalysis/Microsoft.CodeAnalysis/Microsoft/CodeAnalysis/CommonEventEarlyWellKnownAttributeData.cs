using System;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000219 RID: 537
	internal class CommonEventEarlyWellKnownAttributeData : EarlyWellKnownAttributeData
	{
		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06001525 RID: 5413 RVA: 0x00043D83 File Offset: 0x00041F83
		// (set) Token: 0x06001526 RID: 5414 RVA: 0x00043D9A File Offset: 0x00041F9A
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

		// Token: 0x040008B1 RID: 2225
		private ObsoleteAttributeData _obsoleteAttributeData = ObsoleteAttributeData.Uninitialized;
	}
}
