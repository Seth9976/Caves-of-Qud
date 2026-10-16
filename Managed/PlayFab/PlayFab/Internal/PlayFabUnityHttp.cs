using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Ionic.Zlib;
using PlayFab.SharedModels;
using UnityEngine;
using UnityEngine.Networking;

namespace PlayFab.Internal
{
	// Token: 0x020001AA RID: 426
	public class PlayFabUnityHttp : ITransportPlugin, IPlayFabPlugin
	{
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x000144D6 File Offset: 0x000126D6
		public bool IsInitialized
		{
			get
			{
				return this._isInitialized;
			}
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x000144DE File Offset: 0x000126DE
		public void Initialize()
		{
			this._isInitialized = true;
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x000144E7 File Offset: 0x000126E7
		public void Update()
		{
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x000144E9 File Offset: 0x000126E9
		public void OnDestroy()
		{
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x000144EB File Offset: 0x000126EB
		public void SimpleGetCall(string fullUrl, Action<byte[]> successCallback, Action<string> errorCallback)
		{
			SingletonMonoBehaviour<PlayFabHttp>.instance.StartCoroutine(PlayFabUnityHttp.SimpleCallCoroutine("get", fullUrl, null, successCallback, errorCallback));
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x00014506 File Offset: 0x00012706
		public void SimplePutCall(string fullUrl, byte[] payload, Action<byte[]> successCallback, Action<string> errorCallback)
		{
			SingletonMonoBehaviour<PlayFabHttp>.instance.StartCoroutine(PlayFabUnityHttp.SimpleCallCoroutine("put", fullUrl, payload, successCallback, errorCallback));
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x00014522 File Offset: 0x00012722
		public void SimplePostCall(string fullUrl, byte[] payload, Action<byte[]> successCallback, Action<string> errorCallback)
		{
			SingletonMonoBehaviour<PlayFabHttp>.instance.StartCoroutine(PlayFabUnityHttp.SimpleCallCoroutine("post", fullUrl, payload, successCallback, errorCallback));
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0001453E File Offset: 0x0001273E
		private static IEnumerator SimpleCallCoroutine(string method, string fullUrl, byte[] payload, Action<byte[]> successCallback, Action<string> errorCallback)
		{
			if (payload == null)
			{
				using (UnityWebRequest www = UnityWebRequest.Get(fullUrl))
				{
					yield return www.SendWebRequest();
					if (!string.IsNullOrEmpty(www.error))
					{
						errorCallback(www.error);
					}
					else
					{
						successCallback(www.downloadHandler.data);
					}
				}
				UnityWebRequest www = null;
			}
			else
			{
				UnityWebRequest www;
				if (method == "put")
				{
					www = UnityWebRequest.Put(fullUrl, payload);
				}
				else
				{
					www = new UnityWebRequest(fullUrl, "POST");
					www.uploadHandler = new UploadHandlerRaw(payload);
					www.downloadHandler = new DownloadHandlerBuffer();
					www.SetRequestHeader("Content-Type", "application/json");
				}
				yield return www.SendWebRequest();
				if (www.isNetworkError || www.isHttpError)
				{
					errorCallback(www.error);
				}
				else
				{
					successCallback(www.downloadHandler.data);
				}
				www = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0001456C File Offset: 0x0001276C
		public void MakeApiCall(object reqContainerObj)
		{
			CallRequestContainer callRequestContainer = (CallRequestContainer)reqContainerObj;
			callRequestContainer.RequestHeaders["Content-Type"] = "application/json";
			if (PlayFabSettings.CompressApiData)
			{
				callRequestContainer.RequestHeaders["Content-Encoding"] = "GZIP";
				callRequestContainer.RequestHeaders["X-Accept-Encoding"] = "GZIP";
				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (GZipStream gzipStream = new GZipStream(memoryStream, CompressionMode.Compress, Ionic.Zlib.CompressionLevel.BestCompression))
					{
						gzipStream.Write(callRequestContainer.Payload, 0, callRequestContainer.Payload.Length);
					}
					callRequestContainer.Payload = memoryStream.ToArray();
				}
			}
			SingletonMonoBehaviour<PlayFabHttp>.instance.StartCoroutine(this.Post(callRequestContainer));
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x00014640 File Offset: 0x00012840
		private IEnumerator Post(CallRequestContainer reqContainer)
		{
			UnityWebRequest www = new UnityWebRequest(reqContainer.FullUrl)
			{
				uploadHandler = new UploadHandlerRaw(reqContainer.Payload),
				downloadHandler = new DownloadHandlerBuffer(),
				method = "POST"
			};
			foreach (KeyValuePair<string, string> keyValuePair in reqContainer.RequestHeaders)
			{
				if (!string.IsNullOrEmpty(keyValuePair.Key) && !string.IsNullOrEmpty(keyValuePair.Value))
				{
					www.SetRequestHeader(keyValuePair.Key, keyValuePair.Value);
				}
				else
				{
					Debug.LogWarning("Null header: " + keyValuePair.Key + " = " + keyValuePair.Value);
				}
			}
			yield return www.SendWebRequest();
			if (!string.IsNullOrEmpty(www.error))
			{
				this.OnError(www.error, reqContainer);
			}
			else
			{
				try
				{
					byte[] data = www.downloadHandler.data;
					object obj = data != null && data[0] == 31 && data[1] == 139;
					string text = "Unexpected error: cannot decompress GZIP stream.";
					object obj2 = obj;
					if (obj2 == null && data != null)
					{
						text = Encoding.UTF8.GetString(data, 0, data.Length);
					}
					if (obj2 != null)
					{
						using (GZipStream gzipStream = new GZipStream(new MemoryStream(data), CompressionMode.Decompress, false))
						{
							byte[] array = new byte[4096];
							using (MemoryStream memoryStream = new MemoryStream())
							{
								int num;
								while ((num = gzipStream.Read(array, 0, array.Length)) > 0)
								{
									memoryStream.Write(array, 0, num);
								}
								memoryStream.Seek(0L, SeekOrigin.Begin);
								string text2 = new StreamReader(memoryStream).ReadToEnd();
								this.OnResponse(text2, reqContainer);
							}
							goto IL_0228;
						}
					}
					this.OnResponse(text, reqContainer);
					IL_0228:;
				}
				catch (Exception ex)
				{
					string text3 = "Unhandled error in PlayFabUnityHttp: ";
					Exception ex2 = ex;
					this.OnError(text3 + ((ex2 != null) ? ex2.ToString() : null), reqContainer);
				}
			}
			www.Dispose();
			yield break;
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x00014656 File Offset: 0x00012856
		public int GetPendingMessages()
		{
			return this._pendingWwwMessages;
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x00014660 File Offset: 0x00012860
		public void OnResponse(string response, CallRequestContainer reqContainer)
		{
			try
			{
				ISerializerPlugin plugin = PluginManager.GetPlugin<ISerializerPlugin>(PluginContract.PlayFab_Serializer, "");
				HttpResponseObject httpResponseObject = plugin.DeserializeObject<HttpResponseObject>(response);
				if (httpResponseObject.code == 200)
				{
					reqContainer.JsonResponse = plugin.SerializeObject(httpResponseObject.data);
					reqContainer.DeserializeResultJson();
					reqContainer.ApiResult.Request = reqContainer.ApiRequest;
					reqContainer.ApiResult.CustomData = reqContainer.CustomData;
					SingletonMonoBehaviour<PlayFabHttp>.instance.OnPlayFabApiResult(reqContainer);
					PlayFabDeviceUtil.OnPlayFabLogin(reqContainer.ApiResult, reqContainer.settings, reqContainer.instanceApi);
					try
					{
						PlayFabHttp.SendEvent(reqContainer.ApiEndpoint, reqContainer.ApiRequest, reqContainer.ApiResult, ApiProcessingEventType.Post);
					}
					catch (Exception ex)
					{
						Debug.LogException(ex);
					}
					try
					{
						reqContainer.InvokeSuccessCallback();
						goto IL_00FD;
					}
					catch (Exception ex2)
					{
						Debug.LogException(ex2);
						goto IL_00FD;
					}
				}
				if (reqContainer.ErrorCallback != null)
				{
					reqContainer.Error = PlayFabHttp.GeneratePlayFabError(reqContainer.ApiEndpoint, response, reqContainer.CustomData);
					PlayFabHttp.SendErrorEvent(reqContainer.ApiRequest, reqContainer.Error);
					reqContainer.ErrorCallback(reqContainer.Error);
				}
				IL_00FD:;
			}
			catch (Exception ex3)
			{
				Debug.LogException(ex3);
			}
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x0001479C File Offset: 0x0001299C
		public void OnError(string error, CallRequestContainer reqContainer)
		{
			reqContainer.JsonResponse = error;
			if (reqContainer.ErrorCallback != null)
			{
				reqContainer.Error = PlayFabHttp.GeneratePlayFabError(reqContainer.ApiEndpoint, reqContainer.JsonResponse, reqContainer.CustomData);
				PlayFabHttp.SendErrorEvent(reqContainer.ApiRequest, reqContainer.Error);
				reqContainer.ErrorCallback(reqContainer.Error);
			}
		}

		// Token: 0x0400093E RID: 2366
		private bool _isInitialized;

		// Token: 0x0400093F RID: 2367
		private readonly int _pendingWwwMessages;
	}
}
