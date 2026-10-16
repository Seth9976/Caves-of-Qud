using System;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x020006EC RID: 1772
	internal struct MatchResult
	{
		// Token: 0x17000C7B RID: 3195
		// (get) Token: 0x06003320 RID: 13088 RVA: 0x000C7378 File Offset: 0x000C5578
		public bool success
		{
			get
			{
				return this.errorCode == MatchResultErrorCode.None;
			}
		}

		// Token: 0x04001A1C RID: 6684
		public MatchResultErrorCode errorCode;

		// Token: 0x04001A1D RID: 6685
		public string errorValue;
	}
}
