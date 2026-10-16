using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using PlayFab.SharedModels;
using UnityEngine;

namespace PlayFab.Internal
{
	// Token: 0x020001AB RID: 427
	public class PlayFabWebRequest : ITransportPlugin, IPlayFabPlugin
	{
		// Token: 0x06000536 RID: 1334 RVA: 0x000147FF File Offset: 0x000129FF
		public static void SkipCertificateValidation()
		{
			ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(PlayFabWebRequest.AcceptAllCertifications);
			PlayFabWebRequest.certValidationSet = true;
		}

		// Token: 0x17000033 RID: 51
		// (set) Token: 0x06000537 RID: 1335 RVA: 0x00014818 File Offset: 0x00012A18
		public static RemoteCertificateValidationCallback CustomCertValidationHook
		{
			set
			{
				ServicePointManager.ServerCertificateValidationCallback = value;
				PlayFabWebRequest.certValidationSet = true;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000538 RID: 1336 RVA: 0x00014826 File Offset: 0x00012A26
		public bool IsInitialized
		{
			get
			{
				return this._isInitialized;
			}
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x0001482E File Offset: 0x00012A2E
		public void Initialize()
		{
			this.SetupCertificates();
			PlayFabWebRequest._isApplicationPlaying = true;
			PlayFabWebRequest._unityVersion = Application.unityVersion;
			this._isInitialized = true;
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00014850 File Offset: 0x00012A50
		public void OnDestroy()
		{
			PlayFabWebRequest._isApplicationPlaying = false;
			Queue<Action> resultQueueTransferThread = PlayFabWebRequest.ResultQueueTransferThread;
			lock (resultQueueTransferThread)
			{
				PlayFabWebRequest.ResultQueueTransferThread.Clear();
			}
			List<CallRequestContainer> activeRequests = PlayFabWebRequest.ActiveRequests;
			lock (activeRequests)
			{
				PlayFabWebRequest.ActiveRequests.Clear();
			}
			object threadLock = PlayFabWebRequest._ThreadLock;
			lock (threadLock)
			{
				PlayFabWebRequest._requestQueueThread = null;
			}
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x000148FC File Offset: 0x00012AFC
		private void SetupCertificates()
		{
			ServicePointManager.DefaultConnectionLimit = 10;
			ServicePointManager.Expect100Continue = false;
			if (!PlayFabWebRequest.certValidationSet)
			{
				Debug.LogWarning("PlayFab API calls will likely fail because you have not set up a HttpWebRequest certificate validation mechanism");
				Debug.LogWarning("Please set a validation callback into PlayFab.Internal.PlayFabWebRequest.CustomCertValidationHook, or set PlayFab.Internal.PlayFabWebRequest.SkipCertificateValidation()");
			}
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00014926 File Offset: 0x00012B26
		private static bool AcceptAllCertifications(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			return true;
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00014929 File Offset: 0x00012B29
		public void SimpleGetCall(string fullUrl, Action<byte[]> successCallback, Action<string> errorCallback)
		{
			new Thread(delegate
			{
				this.SimpleHttpsWorker("GET", fullUrl, null, successCallback, errorCallback);
			}).Start();
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00014961 File Offset: 0x00012B61
		public void SimplePutCall(string fullUrl, byte[] payload, Action<byte[]> successCallback, Action<string> errorCallback)
		{
			new Thread(delegate
			{
				this.SimpleHttpsWorker("PUT", fullUrl, payload, successCallback, errorCallback);
			}).Start();
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x000149A1 File Offset: 0x00012BA1
		public void SimplePostCall(string fullUrl, byte[] payload, Action<byte[]> successCallback, Action<string> errorCallback)
		{
			new Thread(delegate
			{
				this.SimpleHttpsWorker("POST", fullUrl, payload, successCallback, errorCallback);
			}).Start();
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x000149E4 File Offset: 0x00012BE4
		private void SimpleHttpsWorker(string httpMethod, string fullUrl, byte[] payload, Action<byte[]> successCallback, Action<string> errorCallback)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(fullUrl);
			httpWebRequest.UserAgent = "UnityEngine-Unity; Version: " + PlayFabWebRequest._unityVersion;
			httpWebRequest.Method = httpMethod;
			httpWebRequest.KeepAlive = PlayFabSettings.RequestKeepAlive;
			httpWebRequest.Timeout = PlayFabSettings.RequestTimeout;
			httpWebRequest.AllowWriteStreamBuffering = false;
			httpWebRequest.ReadWriteTimeout = PlayFabSettings.RequestTimeout;
			if (payload != null)
			{
				httpWebRequest.ContentLength = (long)payload.Length;
				using (Stream requestStream = httpWebRequest.GetRequestStream())
				{
					requestStream.Write(payload, 0, payload.Length);
				}
			}
			try
			{
				WebResponse response = httpWebRequest.GetResponse();
				byte[] array = null;
				using (Stream responseStream = response.GetResponseStream())
				{
					if (responseStream != null)
					{
						array = new byte[response.ContentLength];
						responseStream.Read(array, 0, array.Length);
					}
				}
				successCallback(array);
			}
			catch (WebException ex)
			{
				try
				{
					using (Stream responseStream2 = ex.Response.GetResponseStream())
					{
						if (responseStream2 != null)
						{
							using (StreamReader streamReader = new StreamReader(responseStream2))
							{
								errorCallback(streamReader.ReadToEnd());
							}
						}
					}
				}
				catch (Exception ex2)
				{
					Debug.LogException(ex2);
				}
			}
			catch (Exception ex3)
			{
				Debug.LogException(ex3);
			}
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00014B64 File Offset: 0x00012D64
		public void MakeApiCall(object reqContainerObj)
		{
			CallRequestContainer callRequestContainer = (CallRequestContainer)reqContainerObj;
			callRequestContainer.HttpState = HttpRequestState.Idle;
			List<CallRequestContainer> activeRequests = PlayFabWebRequest.ActiveRequests;
			lock (activeRequests)
			{
				PlayFabWebRequest.ActiveRequests.Insert(0, callRequestContainer);
			}
			PlayFabWebRequest.ActivateThreadWorker();
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00014BBC File Offset: 0x00012DBC
		private static void ActivateThreadWorker()
		{
			object threadLock = PlayFabWebRequest._ThreadLock;
			lock (threadLock)
			{
				if (PlayFabWebRequest._requestQueueThread == null)
				{
					PlayFabWebRequest._requestQueueThread = new Thread(new ThreadStart(PlayFabWebRequest.WorkerThreadMainLoop));
					PlayFabWebRequest._requestQueueThread.Start();
				}
			}
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00014C20 File Offset: 0x00012E20
		private static void WorkerThreadMainLoop()
		{
			try
			{
				object obj = PlayFabWebRequest._ThreadLock;
				lock (obj)
				{
					PlayFabWebRequest._threadKillTime = DateTime.UtcNow + PlayFabWebRequest.ThreadKillTimeout;
				}
				List<CallRequestContainer> list = new List<CallRequestContainer>();
				bool flag2;
				do
				{
					List<CallRequestContainer> activeRequests = PlayFabWebRequest.ActiveRequests;
					lock (activeRequests)
					{
						list.AddRange(PlayFabWebRequest.ActiveRequests);
						PlayFabWebRequest.ActiveRequests.Clear();
						PlayFabWebRequest._activeCallCount = list.Count;
					}
					int count = list.Count;
					for (int i = count - 1; i >= 0; i--)
					{
						switch (list[i].HttpState)
						{
						case HttpRequestState.Sent:
							if (list[i].HttpRequest.HaveResponse)
							{
								PlayFabWebRequest.ProcessHttpResponse(list[i]);
							}
							break;
						case HttpRequestState.Received:
							PlayFabWebRequest.ProcessJsonResponse(list[i]);
							list.RemoveAt(i);
							break;
						case HttpRequestState.Idle:
							PlayFabWebRequest.Post(list[i]);
							break;
						case HttpRequestState.Error:
							list.RemoveAt(i);
							break;
						}
					}
					obj = PlayFabWebRequest._ThreadLock;
					lock (obj)
					{
						DateTime utcNow = DateTime.UtcNow;
						if (count > 0 && PlayFabWebRequest._isApplicationPlaying)
						{
							PlayFabWebRequest._threadKillTime = utcNow + PlayFabWebRequest.ThreadKillTimeout;
						}
						flag2 = utcNow <= PlayFabWebRequest._threadKillTime;
						if (!flag2)
						{
							PlayFabWebRequest._requestQueueThread = null;
						}
					}
					Thread.Sleep(1);
				}
				while (flag2);
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
				PlayFabWebRequest._requestQueueThread = null;
			}
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x00014E10 File Offset: 0x00013010
		private static void Post(CallRequestContainer reqContainer)
		{
			try
			{
				reqContainer.HttpRequest = (HttpWebRequest)WebRequest.Create(reqContainer.FullUrl);
				reqContainer.HttpRequest.UserAgent = "UnityEngine-Unity; Version: " + PlayFabWebRequest._unityVersion;
				reqContainer.HttpRequest.SendChunked = false;
				reqContainer.HttpRequest.Proxy = null;
				foreach (KeyValuePair<string, string> keyValuePair in reqContainer.RequestHeaders)
				{
					reqContainer.HttpRequest.Headers.Add(keyValuePair.Key, keyValuePair.Value);
				}
				reqContainer.HttpRequest.ContentType = "application/json";
				reqContainer.HttpRequest.Method = "POST";
				reqContainer.HttpRequest.KeepAlive = PlayFabSettings.RequestKeepAlive;
				reqContainer.HttpRequest.Timeout = PlayFabSettings.RequestTimeout;
				reqContainer.HttpRequest.AllowWriteStreamBuffering = false;
				reqContainer.HttpRequest.Proxy = null;
				reqContainer.HttpRequest.ContentLength = (long)reqContainer.Payload.Length;
				reqContainer.HttpRequest.ReadWriteTimeout = PlayFabSettings.RequestTimeout;
				using (Stream requestStream = reqContainer.HttpRequest.GetRequestStream())
				{
					requestStream.Write(reqContainer.Payload, 0, reqContainer.Payload.Length);
				}
				reqContainer.HttpState = HttpRequestState.Sent;
			}
			catch (WebException ex)
			{
				reqContainer.JsonResponse = PlayFabWebRequest.ResponseToString(ex.Response) ?? (ex.Status.ToString() + ": WebException making http request to: " + reqContainer.FullUrl);
				Debug.LogException(new WebException(reqContainer.JsonResponse, ex));
				PlayFabWebRequest.QueueRequestError(reqContainer);
			}
			catch (Exception ex2)
			{
				reqContainer.JsonResponse = "Unhandled exception in Post : " + reqContainer.FullUrl;
				Debug.LogException(new Exception(reqContainer.JsonResponse, ex2));
				PlayFabWebRequest.QueueRequestError(reqContainer);
			}
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00015050 File Offset: 0x00013250
		private static void ProcessHttpResponse(CallRequestContainer reqContainer)
		{
			try
			{
				HttpWebResponse httpWebResponse = (HttpWebResponse)reqContainer.HttpRequest.GetResponse();
				if (httpWebResponse.StatusCode == HttpStatusCode.OK)
				{
					reqContainer.JsonResponse = PlayFabWebRequest.ResponseToString(httpWebResponse);
				}
				if (httpWebResponse.StatusCode != HttpStatusCode.OK || string.IsNullOrEmpty(reqContainer.JsonResponse))
				{
					reqContainer.JsonResponse = reqContainer.JsonResponse ?? "No response from server";
					PlayFabWebRequest.QueueRequestError(reqContainer);
				}
				else
				{
					reqContainer.HttpState = HttpRequestState.Received;
				}
			}
			catch (Exception ex)
			{
				string text = "Unhandled exception in ProcessHttpResponse : " + reqContainer.FullUrl;
				reqContainer.JsonResponse = reqContainer.JsonResponse ?? text;
				Debug.LogException(new Exception(text, ex));
				PlayFabWebRequest.QueueRequestError(reqContainer);
			}
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00015110 File Offset: 0x00013310
		private static void QueueRequestError(CallRequestContainer reqContainer)
		{
			reqContainer.Error = PlayFabHttp.GeneratePlayFabError(reqContainer.ApiEndpoint, reqContainer.JsonResponse, reqContainer.CustomData);
			reqContainer.HttpState = HttpRequestState.Error;
			Queue<Action> resultQueueTransferThread = PlayFabWebRequest.ResultQueueTransferThread;
			lock (resultQueueTransferThread)
			{
				PlayFabWebRequest.ResultQueueTransferThread.Enqueue(delegate
				{
					PlayFabHttp.SendErrorEvent(reqContainer.ApiRequest, reqContainer.Error);
					if (reqContainer.ErrorCallback != null)
					{
						reqContainer.ErrorCallback(reqContainer.Error);
					}
				});
			}
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x000151AC File Offset: 0x000133AC
		private static void ProcessJsonResponse(CallRequestContainer reqContainer)
		{
			try
			{
				ISerializerPlugin plugin = PluginManager.GetPlugin<ISerializerPlugin>(PluginContract.PlayFab_Serializer, "");
				HttpResponseObject httpResponseObject = plugin.DeserializeObject<HttpResponseObject>(reqContainer.JsonResponse);
				if (httpResponseObject == null || httpResponseObject.code != 200)
				{
					PlayFabWebRequest.QueueRequestError(reqContainer);
				}
				else
				{
					reqContainer.JsonResponse = plugin.SerializeObject(httpResponseObject.data);
					reqContainer.DeserializeResultJson();
					reqContainer.ApiResult.Request = reqContainer.ApiRequest;
					reqContainer.ApiResult.CustomData = reqContainer.CustomData;
					SingletonMonoBehaviour<PlayFabHttp>.instance.OnPlayFabApiResult(reqContainer);
					Queue<Action> queue = PlayFabWebRequest.ResultQueueTransferThread;
					lock (queue)
					{
						PlayFabWebRequest.ResultQueueTransferThread.Enqueue(delegate
						{
							PlayFabDeviceUtil.OnPlayFabLogin(reqContainer.ApiResult, reqContainer.settings, reqContainer.instanceApi);
						});
					}
					queue = PlayFabWebRequest.ResultQueueTransferThread;
					lock (queue)
					{
						PlayFabWebRequest.ResultQueueTransferThread.Enqueue(delegate
						{
							try
							{
								PlayFabHttp.SendEvent(reqContainer.ApiEndpoint, reqContainer.ApiRequest, reqContainer.ApiResult, ApiProcessingEventType.Post);
								reqContainer.InvokeSuccessCallback();
							}
							catch (Exception ex2)
							{
								Debug.LogException(ex2);
							}
						});
					}
				}
			}
			catch (Exception ex)
			{
				string text = "Unhandled exception in ProcessJsonResponse : " + reqContainer.FullUrl;
				reqContainer.JsonResponse = reqContainer.JsonResponse ?? text;
				Debug.LogException(new Exception(text, ex));
				PlayFabWebRequest.QueueRequestError(reqContainer);
			}
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00015378 File Offset: 0x00013578
		public void Update()
		{
			Queue<Action> resultQueueTransferThread = PlayFabWebRequest.ResultQueueTransferThread;
			lock (resultQueueTransferThread)
			{
				while (PlayFabWebRequest.ResultQueueTransferThread.Count > 0)
				{
					Action action = PlayFabWebRequest.ResultQueueTransferThread.Dequeue();
					PlayFabWebRequest.ResultQueueMainThread.Enqueue(action);
				}
				goto IL_0050;
			}
			IL_0041:
			PlayFabWebRequest.ResultQueueMainThread.Dequeue()();
			IL_0050:
			if (PlayFabWebRequest.ResultQueueMainThread.Count <= 0)
			{
				return;
			}
			goto IL_0041;
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x000153F4 File Offset: 0x000135F4
		private static string ResponseToString(WebResponse webResponse)
		{
			if (webResponse == null)
			{
				return null;
			}
			string text;
			try
			{
				using (Stream responseStream = webResponse.GetResponseStream())
				{
					if (responseStream == null)
					{
						text = null;
					}
					else
					{
						using (StreamReader streamReader = new StreamReader(responseStream))
						{
							text = streamReader.ReadToEnd();
						}
					}
				}
			}
			catch (WebException ex)
			{
				try
				{
					using (Stream responseStream2 = ex.Response.GetResponseStream())
					{
						if (responseStream2 == null)
						{
							text = null;
						}
						else
						{
							using (StreamReader streamReader2 = new StreamReader(responseStream2))
							{
								text = streamReader2.ReadToEnd();
							}
						}
					}
				}
				catch (Exception ex2)
				{
					Debug.LogException(ex2);
					text = null;
				}
			}
			catch (Exception ex3)
			{
				Debug.LogException(ex3);
				text = null;
			}
			return text;
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x000154E8 File Offset: 0x000136E8
		public int GetPendingMessages()
		{
			int num = 0;
			List<CallRequestContainer> activeRequests = PlayFabWebRequest.ActiveRequests;
			lock (activeRequests)
			{
				num += PlayFabWebRequest.ActiveRequests.Count + PlayFabWebRequest._activeCallCount;
			}
			Queue<Action> resultQueueTransferThread = PlayFabWebRequest.ResultQueueTransferThread;
			lock (resultQueueTransferThread)
			{
				num += PlayFabWebRequest.ResultQueueTransferThread.Count;
			}
			return num;
		}

		// Token: 0x04000940 RID: 2368
		private static readonly Queue<Action> ResultQueueTransferThread = new Queue<Action>();

		// Token: 0x04000941 RID: 2369
		private static readonly Queue<Action> ResultQueueMainThread = new Queue<Action>();

		// Token: 0x04000942 RID: 2370
		private static readonly List<CallRequestContainer> ActiveRequests = new List<CallRequestContainer>();

		// Token: 0x04000943 RID: 2371
		private static bool certValidationSet = false;

		// Token: 0x04000944 RID: 2372
		private static Thread _requestQueueThread;

		// Token: 0x04000945 RID: 2373
		private static readonly object _ThreadLock = new object();

		// Token: 0x04000946 RID: 2374
		private static readonly TimeSpan ThreadKillTimeout = TimeSpan.FromSeconds(60.0);

		// Token: 0x04000947 RID: 2375
		private static DateTime _threadKillTime = DateTime.UtcNow + PlayFabWebRequest.ThreadKillTimeout;

		// Token: 0x04000948 RID: 2376
		private static bool _isApplicationPlaying;

		// Token: 0x04000949 RID: 2377
		private static int _activeCallCount;

		// Token: 0x0400094A RID: 2378
		private static string _unityVersion;

		// Token: 0x0400094B RID: 2379
		private bool _isInitialized;
	}
}
