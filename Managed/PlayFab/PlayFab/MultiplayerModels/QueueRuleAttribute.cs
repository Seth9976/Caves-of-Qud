using System;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000E5 RID: 229
	[Serializable]
	public class QueueRuleAttribute : PlayFabBaseModel
	{
		// Token: 0x04000564 RID: 1380
		public string Path;

		// Token: 0x04000565 RID: 1381
		public AttributeSource Source;
	}
}
