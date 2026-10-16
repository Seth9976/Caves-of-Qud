using System;
using PlayFab.SharedModels;

namespace PlayFab.DataModels
{
	// Token: 0x02000173 RID: 371
	[Serializable]
	public class SetObjectInfo : PlayFabBaseModel
	{
		// Token: 0x0400074B RID: 1867
		public string ObjectName;

		// Token: 0x0400074C RID: 1868
		public string OperationReason;

		// Token: 0x0400074D RID: 1869
		public OperationTypes? SetResult;
	}
}
