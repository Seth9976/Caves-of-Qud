using System;
using System.Collections.Generic;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000067 RID: 103
	internal class DialogRequest
	{
		// Token: 0x04000107 RID: 263
		public string QueryText;

		// Token: 0x04000108 RID: 264
		public string OkayButtonText;

		// Token: 0x04000109 RID: 265
		public string CancelButtonText;

		// Token: 0x0400010A RID: 266
		public List<string> Options;

		// Token: 0x0400010B RID: 267
		public Action<bool, int> Callback;
	}
}
