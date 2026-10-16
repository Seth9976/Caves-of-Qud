using System;
using PlayFab.SharedModels;

namespace PlayFab.Internal
{
	// Token: 0x020001A9 RID: 425
	public class ApiProcessingEventArgs
	{
		// Token: 0x06000526 RID: 1318 RVA: 0x000144BC File Offset: 0x000126BC
		public TRequest GetRequest<TRequest>() where TRequest : PlayFabRequestCommon
		{
			return this.Request as TRequest;
		}

		// Token: 0x0400093A RID: 2362
		public string ApiEndpoint;

		// Token: 0x0400093B RID: 2363
		public ApiProcessingEventType EventType;

		// Token: 0x0400093C RID: 2364
		public PlayFabRequestCommon Request;

		// Token: 0x0400093D RID: 2365
		public PlayFabResultCommon Result;
	}
}
