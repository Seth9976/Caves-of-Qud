using System;
using Unity.Services.Core.Internal;

namespace Unity.Services.Core.Networking.Internal
{
	// Token: 0x0200001E RID: 30
	internal interface IHttpClient : IServiceComponent
	{
		// Token: 0x06000050 RID: 80
		string GetBaseUrlFor(string serviceId);

		// Token: 0x06000051 RID: 81
		HttpOptions GetDefaultOptionsFor(string serviceId);

		// Token: 0x06000052 RID: 82
		HttpRequest CreateRequestForService(string serviceId, string resourcePath);

		// Token: 0x06000053 RID: 83
		IAsyncOperation<ReadOnlyHttpResponse> Send(HttpRequest request);
	}
}
