using System;
using PlayFab.SharedModels;

namespace PlayFab.CloudScriptModels
{
	// Token: 0x0200018C RID: 396
	[Serializable]
	public class LogStatement : PlayFabBaseModel
	{
		// Token: 0x040008AE RID: 2222
		public object Data;

		// Token: 0x040008AF RID: 2223
		public string Level;

		// Token: 0x040008B0 RID: 2224
		public string Message;
	}
}
