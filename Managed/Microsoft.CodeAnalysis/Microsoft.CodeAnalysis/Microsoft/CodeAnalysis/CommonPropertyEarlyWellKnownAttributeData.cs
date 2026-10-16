using System;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000222 RID: 546
	internal class CommonPropertyEarlyWellKnownAttributeData : EarlyWellKnownAttributeData
	{
		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x0600156F RID: 5487 RVA: 0x000441ED File Offset: 0x000423ED
		// (set) Token: 0x06001570 RID: 5488 RVA: 0x00044204 File Offset: 0x00042404
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

		// Token: 0x040008D5 RID: 2261
		private ObsoleteAttributeData _obsoleteAttributeData = ObsoleteAttributeData.Uninitialized;
	}
}
