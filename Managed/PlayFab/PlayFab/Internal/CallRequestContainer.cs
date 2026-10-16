using System;
using System.Collections.Generic;
using System.Net;
using PlayFab.SharedModels;

namespace PlayFab.Internal
{
	// Token: 0x020001A6 RID: 422
	public class CallRequestContainer
	{
		// Token: 0x0400091D RID: 2333
		public HttpRequestState HttpState = HttpRequestState.Idle;

		// Token: 0x0400091E RID: 2334
		public HttpWebRequest HttpRequest;

		// Token: 0x0400091F RID: 2335
		public string ApiEndpoint;

		// Token: 0x04000920 RID: 2336
		public string FullUrl;

		// Token: 0x04000921 RID: 2337
		public byte[] Payload;

		// Token: 0x04000922 RID: 2338
		public string JsonResponse;

		// Token: 0x04000923 RID: 2339
		public PlayFabRequestCommon ApiRequest;

		// Token: 0x04000924 RID: 2340
		public Dictionary<string, string> RequestHeaders;

		// Token: 0x04000925 RID: 2341
		public PlayFabResultCommon ApiResult;

		// Token: 0x04000926 RID: 2342
		public PlayFabError Error;

		// Token: 0x04000927 RID: 2343
		public Action DeserializeResultJson;

		// Token: 0x04000928 RID: 2344
		public Action InvokeSuccessCallback;

		// Token: 0x04000929 RID: 2345
		public Action<PlayFabError> ErrorCallback;

		// Token: 0x0400092A RID: 2346
		public object CustomData;

		// Token: 0x0400092B RID: 2347
		public PlayFabApiSettings settings;

		// Token: 0x0400092C RID: 2348
		public PlayFabAuthenticationContext context;

		// Token: 0x0400092D RID: 2349
		public IPlayFabInstanceApi instanceApi;
	}
}
