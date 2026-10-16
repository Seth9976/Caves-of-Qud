using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000291 RID: 657
	[Serializable]
	public class LogStatement : PlayFabBaseModel
	{
		// Token: 0x04000DB6 RID: 3510
		public object Data;

		// Token: 0x04000DB7 RID: 3511
		public string Level;

		// Token: 0x04000DB8 RID: 3512
		public string Message;
	}
}
