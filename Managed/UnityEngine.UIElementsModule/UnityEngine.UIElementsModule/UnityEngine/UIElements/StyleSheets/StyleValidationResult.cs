using System;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x020006F3 RID: 1779
	internal struct StyleValidationResult
	{
		// Token: 0x17000C8A RID: 3210
		// (get) Token: 0x06003370 RID: 13168 RVA: 0x000C8854 File Offset: 0x000C6A54
		public bool success
		{
			get
			{
				return this.status == StyleValidationStatus.Ok;
			}
		}

		// Token: 0x04001A3B RID: 6715
		public StyleValidationStatus status;

		// Token: 0x04001A3C RID: 6716
		public string message;

		// Token: 0x04001A3D RID: 6717
		public string errorValue;

		// Token: 0x04001A3E RID: 6718
		public string hint;
	}
}
