using System;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000E2 RID: 226
	[Serializable]
	public class Port : PlayFabBaseModel
	{
		// Token: 0x0400055C RID: 1372
		public string Name;

		// Token: 0x0400055D RID: 1373
		public int Num;

		// Token: 0x0400055E RID: 1374
		public ProtocolType Protocol;
	}
}
