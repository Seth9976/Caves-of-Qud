using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x0200021B RID: 539
	[Serializable]
	public class GetPlayerSegmentsResult : PlayFabResultCommon
	{
		// Token: 0x04000C4C RID: 3148
		public List<GetSegmentResult> Segments;
	}
}
