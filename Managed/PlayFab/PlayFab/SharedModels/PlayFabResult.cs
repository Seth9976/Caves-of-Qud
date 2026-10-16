using System;

namespace PlayFab.SharedModels
{
	// Token: 0x02000033 RID: 51
	public class PlayFabResult<TResult> where TResult : PlayFabResultCommon
	{
		// Token: 0x040002D2 RID: 722
		public TResult Result;

		// Token: 0x040002D3 RID: 723
		public object CustomData;
	}
}
