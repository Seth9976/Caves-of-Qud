using System;
using System.Collections.Generic;
using System.IO;
using Unity.Services.Core.Internal;
using Unity.Services.Core.Networking.Internal;
using UnityEngine.Networking;

namespace Unity.Services.Core.Networking
{
	// Token: 0x02000004 RID: 4
	internal class UnityWebRequestClient : IHttpClient, IServiceComponent
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BB File Offset: 0x000002BB
		public string GetBaseUrlFor(string serviceId)
		{
			return this.m_ServiceIdToConfig[serviceId].BaseUrl;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020CE File Offset: 0x000002CE
		public HttpOptions GetDefaultOptionsFor(string serviceId)
		{
			return this.m_ServiceIdToConfig[serviceId].DefaultOptions;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020E4 File Offset: 0x000002E4
		public HttpRequest CreateRequestForService(string serviceId, string resourcePath)
		{
			HttpServiceConfig httpServiceConfig = this.m_ServiceIdToConfig[serviceId];
			string text = UnityWebRequestClient.CombinePaths(httpServiceConfig.BaseUrl, resourcePath);
			return new HttpRequest().SetUrl(text).SetOptions(httpServiceConfig.DefaultOptions);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002121 File Offset: 0x00000321
		internal static string CombinePaths(string path1, string path2)
		{
			return Path.Combine(path1, path2).Replace('\\', '/');
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002134 File Offset: 0x00000334
		public IAsyncOperation<ReadOnlyHttpResponse> Send(HttpRequest request)
		{
			UnityWebRequestClient.<>c__DisplayClass5_0 CS$<>8__locals1 = new UnityWebRequestClient.<>c__DisplayClass5_0();
			CS$<>8__locals1.request = request;
			CS$<>8__locals1.operation = new AsyncOperation<ReadOnlyHttpResponse>();
			CS$<>8__locals1.operation.SetInProgress();
			try
			{
				UnityWebRequestClient.ConvertToWebRequest(CS$<>8__locals1.request).SendWebRequest().completed += CS$<>8__locals1.<Send>g__OnWebRequestCompleted|0;
			}
			catch (Exception ex)
			{
				CS$<>8__locals1.operation.Fail(ex);
			}
			return CS$<>8__locals1.operation;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021AC File Offset: 0x000003AC
		private static UnityWebRequest ConvertToWebRequest(HttpRequest request)
		{
			UnityWebRequest unityWebRequest = new UnityWebRequest(request.Url, request.Method)
			{
				downloadHandler = new DownloadHandlerBuffer(),
				redirectLimit = request.Options.RedirectLimit,
				timeout = request.Options.RequestTimeoutInSeconds
			};
			if (request.Body != null && request.Body.Length != 0)
			{
				unityWebRequest.uploadHandler = new UploadHandlerRaw(request.Body);
			}
			if (request.Headers != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in request.Headers)
				{
					unityWebRequest.SetRequestHeader(keyValuePair.Key, keyValuePair.Value);
				}
			}
			return unityWebRequest;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002278 File Offset: 0x00000478
		private static HttpResponse ConvertToResponse(UnityWebRequest webRequest)
		{
			HttpResponse httpResponse = new HttpResponse().SetHeaders(webRequest.GetResponseHeaders());
			DownloadHandler downloadHandler = webRequest.downloadHandler;
			return httpResponse.SetData((downloadHandler != null) ? downloadHandler.data : null).SetStatusCode(webRequest.responseCode).SetErrorMessage(webRequest.error)
				.SetIsHttpError(webRequest.result == UnityWebRequest.Result.ProtocolError)
				.SetIsNetworkError(webRequest.result == UnityWebRequest.Result.ConnectionError);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000022DE File Offset: 0x000004DE
		internal void SetServiceConfig(HttpServiceConfig config)
		{
			this.m_ServiceIdToConfig[config.ServiceId] = config;
		}

		// Token: 0x04000004 RID: 4
		private readonly Dictionary<string, HttpServiceConfig> m_ServiceIdToConfig = new Dictionary<string, HttpServiceConfig>();
	}
}
