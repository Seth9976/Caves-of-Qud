using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using PlayFab.AuthenticationModels;
using PlayFab.ClientModels;
using PlayFab.Public;
using PlayFab.SharedModels;
using UnityEngine;

namespace PlayFab.Internal
{
	// Token: 0x020001A7 RID: 423
	public class PlayFabHttp : SingletonMonoBehaviour<PlayFabHttp>
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000508 RID: 1288 RVA: 0x00013B44 File Offset: 0x00011D44
		// (remove) Token: 0x06000509 RID: 1289 RVA: 0x00013B78 File Offset: 0x00011D78
		public static event PlayFabHttp.ApiProcessingEvent<ApiProcessingEventArgs> ApiProcessingEventHandler;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600050A RID: 1290 RVA: 0x00013BAC File Offset: 0x00011DAC
		// (remove) Token: 0x0600050B RID: 1291 RVA: 0x00013BE0 File Offset: 0x00011DE0
		public static event PlayFabHttp.ApiProcessErrorEvent ApiProcessingErrorEventHandler;

		// Token: 0x0600050C RID: 1292 RVA: 0x00013C14 File Offset: 0x00011E14
		public static int GetPendingMessages()
		{
			ITransportPlugin plugin = PluginManager.GetPlugin<ITransportPlugin>(PluginContract.PlayFab_Transport, "");
			if (!plugin.IsInitialized)
			{
				return 0;
			}
			return plugin.GetPendingMessages();
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x00013C40 File Offset: 0x00011E40
		public static void InitializeHttp()
		{
			if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
			{
				throw new PlayFabException(PlayFabExceptionCode.TitleNotSet, "You must set PlayFabSettings.TitleId before making API Calls.");
			}
			ITransportPlugin plugin = PluginManager.GetPlugin<ITransportPlugin>(PluginContract.PlayFab_Transport, "");
			if (plugin.IsInitialized)
			{
				return;
			}
			plugin.Initialize();
			SingletonMonoBehaviour<PlayFabHttp>.CreateInstance();
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x00013C85 File Offset: 0x00011E85
		public static void InitializeLogger(IPlayFabLogger setLogger = null)
		{
			if (PlayFabHttp._logger != null)
			{
				throw new InvalidOperationException("Once initialized, the logger cannot be reset.");
			}
			if (setLogger == null)
			{
				setLogger = new PlayFabLogger();
			}
			PlayFabHttp._logger = setLogger;
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x00013CA9 File Offset: 0x00011EA9
		public static void InitializeScreenTimeTracker(string entityId, string entityType, string playFabUserId)
		{
			PlayFabHttp.screenTimeTracker.ClientSessionStart(entityId, entityType, playFabUserId);
			SingletonMonoBehaviour<PlayFabHttp>.instance.StartCoroutine(PlayFabHttp.SendScreenTimeEvents(5f));
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00013CCD File Offset: 0x00011ECD
		private static IEnumerator SendScreenTimeEvents(float secondsBetweenBatches)
		{
			WaitForSeconds delay = new WaitForSeconds(secondsBetweenBatches);
			while (!PlayFabSettings.DisableFocusTimeCollection)
			{
				PlayFabHttp.screenTimeTracker.Send();
				yield return delay;
			}
			yield break;
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00013CDC File Offset: 0x00011EDC
		public static void SimpleGetCall(string fullUrl, Action<byte[]> successCallback, Action<string> errorCallback)
		{
			PlayFabHttp.InitializeHttp();
			PluginManager.GetPlugin<ITransportPlugin>(PluginContract.PlayFab_Transport, "").SimpleGetCall(fullUrl, successCallback, errorCallback);
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00013CF6 File Offset: 0x00011EF6
		public static void SimplePutCall(string fullUrl, byte[] payload, Action<byte[]> successCallback, Action<string> errorCallback)
		{
			PlayFabHttp.InitializeHttp();
			PluginManager.GetPlugin<ITransportPlugin>(PluginContract.PlayFab_Transport, "").SimplePutCall(fullUrl, payload, successCallback, errorCallback);
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00013D11 File Offset: 0x00011F11
		public static void SimplePostCall(string fullUrl, byte[] payload, Action<byte[]> successCallback, Action<string> errorCallback)
		{
			PlayFabHttp.InitializeHttp();
			PluginManager.GetPlugin<ITransportPlugin>(PluginContract.PlayFab_Transport, "").SimplePostCall(fullUrl, payload, successCallback, errorCallback);
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x00013D2C File Offset: 0x00011F2C
		protected internal static void MakeApiCall<TResult>(string apiEndpoint, PlayFabRequestCommon request, AuthType authType, Action<TResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null, PlayFabAuthenticationContext authenticationContext = null, PlayFabApiSettings apiSettings = null, IPlayFabInstanceApi instanceApi = null) where TResult : PlayFabResultCommon
		{
			apiSettings = apiSettings ?? PlayFabSettings.staticSettings;
			string fullUrl = apiSettings.GetFullUrl(apiEndpoint, apiSettings.RequestGetParams);
			PlayFabHttp._MakeApiCall<TResult>(apiEndpoint, fullUrl, request, authType, resultCallback, errorCallback, customData, extraHeaders, false, authenticationContext, apiSettings, instanceApi);
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x00013D70 File Offset: 0x00011F70
		protected internal static void MakeApiCallWithFullUri<TResult>(string fullUri, PlayFabRequestCommon request, AuthType authType, Action<TResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null, PlayFabAuthenticationContext authenticationContext = null, PlayFabApiSettings apiSettings = null, IPlayFabInstanceApi instanceApi = null) where TResult : PlayFabResultCommon
		{
			apiSettings = apiSettings ?? PlayFabSettings.staticSettings;
			PlayFabHttp._MakeApiCall<TResult>(null, fullUri, request, authType, resultCallback, errorCallback, customData, extraHeaders, false, authenticationContext, apiSettings, instanceApi);
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x00013DA4 File Offset: 0x00011FA4
		private static void _MakeApiCall<TResult>(string apiEndpoint, string fullUrl, PlayFabRequestCommon request, AuthType authType, Action<TResult> resultCallback, Action<PlayFabError> errorCallback, object customData, Dictionary<string, string> extraHeaders, bool allowQueueing, PlayFabAuthenticationContext authenticationContext, PlayFabApiSettings apiSettings, IPlayFabInstanceApi instanceApi) where TResult : PlayFabResultCommon
		{
			PlayFabHttp.InitializeHttp();
			PlayFabHttp.SendEvent(apiEndpoint, request, null, ApiProcessingEventType.Pre);
			ISerializerPlugin serializer = PluginManager.GetPlugin<ISerializerPlugin>(PluginContract.PlayFab_Serializer, "");
			CallRequestContainer reqContainer = new CallRequestContainer
			{
				ApiEndpoint = apiEndpoint,
				FullUrl = fullUrl,
				settings = apiSettings,
				context = authenticationContext,
				CustomData = customData,
				Payload = Encoding.UTF8.GetBytes(serializer.SerializeObject(request)),
				ApiRequest = request,
				ErrorCallback = errorCallback,
				RequestHeaders = (extraHeaders ?? new Dictionary<string, string>()),
				instanceApi = instanceApi
			};
			foreach (KeyValuePair<string, string> keyValuePair in PlayFabHttp.GlobalHeaderInjection)
			{
				if (!reqContainer.RequestHeaders.ContainsKey(keyValuePair.Key))
				{
					reqContainer.RequestHeaders[keyValuePair.Key] = keyValuePair.Value;
				}
			}
			ITransportPlugin plugin = PluginManager.GetPlugin<ITransportPlugin>(PluginContract.PlayFab_Transport, "");
			reqContainer.RequestHeaders["X-ReportErrorAsSuccess"] = "true";
			reqContainer.RequestHeaders["X-PlayFabSDK"] = "UnitySDK-2.90.200713";
			if (authType != AuthType.LoginSession)
			{
				if (authType == AuthType.EntityToken)
				{
					if (authenticationContext != null)
					{
						reqContainer.RequestHeaders["X-EntityToken"] = authenticationContext.EntityToken;
					}
				}
			}
			else if (authenticationContext != null)
			{
				reqContainer.RequestHeaders["X-Authorization"] = authenticationContext.ClientSessionTicket;
			}
			reqContainer.DeserializeResultJson = delegate
			{
				reqContainer.ApiResult = serializer.DeserializeObject<TResult>(reqContainer.JsonResponse);
			};
			reqContainer.InvokeSuccessCallback = delegate
			{
				if (resultCallback != null)
				{
					resultCallback((TResult)((object)reqContainer.ApiResult));
				}
			};
			if (allowQueueing && PlayFabHttp._apiCallQueue != null)
			{
				for (int i = PlayFabHttp._apiCallQueue.Count - 1; i >= 0; i--)
				{
					if (PlayFabHttp._apiCallQueue[i].ApiEndpoint == apiEndpoint)
					{
						PlayFabHttp._apiCallQueue.RemoveAt(i);
					}
				}
				PlayFabHttp._apiCallQueue.Add(reqContainer);
				return;
			}
			plugin.MakeApiCall(reqContainer);
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x00013FEC File Offset: 0x000121EC
		internal void OnPlayFabApiResult(CallRequestContainer reqContainer)
		{
			PlayFabResultCommon apiResult = reqContainer.ApiResult;
			GetEntityTokenResponse getEntityTokenResponse = apiResult as GetEntityTokenResponse;
			if (getEntityTokenResponse != null)
			{
				PlayFabSettings.staticPlayer.EntityToken = getEntityTokenResponse.EntityToken;
			}
			LoginResult loginResult = apiResult as LoginResult;
			RegisterPlayFabUserResult registerPlayFabUserResult = apiResult as RegisterPlayFabUserResult;
			if (loginResult != null)
			{
				loginResult.AuthenticationContext = new PlayFabAuthenticationContext(loginResult.SessionTicket, loginResult.EntityToken.EntityToken, loginResult.PlayFabId, loginResult.EntityToken.Entity.Id, loginResult.EntityToken.Entity.Type);
				if (reqContainer.context != null)
				{
					reqContainer.context.CopyFrom(loginResult.AuthenticationContext);
					return;
				}
			}
			else if (registerPlayFabUserResult != null)
			{
				registerPlayFabUserResult.AuthenticationContext = new PlayFabAuthenticationContext(registerPlayFabUserResult.SessionTicket, registerPlayFabUserResult.EntityToken.EntityToken, registerPlayFabUserResult.PlayFabId, registerPlayFabUserResult.EntityToken.Entity.Id, registerPlayFabUserResult.EntityToken.Entity.Type);
				if (reqContainer.context != null)
				{
					reqContainer.context.CopyFrom(registerPlayFabUserResult.AuthenticationContext);
				}
			}
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x000140E3 File Offset: 0x000122E3
		private void OnEnable()
		{
			if (PlayFabHttp._logger != null)
			{
				PlayFabHttp._logger.OnEnable();
			}
			if (PlayFabHttp.screenTimeTracker != null && !PlayFabSettings.DisableFocusTimeCollection)
			{
				PlayFabHttp.screenTimeTracker.OnEnable();
			}
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0001410E File Offset: 0x0001230E
		private void OnDisable()
		{
			if (PlayFabHttp._logger != null)
			{
				PlayFabHttp._logger.OnDisable();
			}
			if (PlayFabHttp.screenTimeTracker != null && !PlayFabSettings.DisableFocusTimeCollection)
			{
				PlayFabHttp.screenTimeTracker.OnDisable();
			}
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0001413C File Offset: 0x0001233C
		private void OnDestroy()
		{
			ITransportPlugin plugin = PluginManager.GetPlugin<ITransportPlugin>(PluginContract.PlayFab_Transport, "");
			if (plugin.IsInitialized)
			{
				plugin.OnDestroy();
			}
			if (PlayFabHttp._logger != null)
			{
				PlayFabHttp._logger.OnDestroy();
			}
			if (PlayFabHttp.screenTimeTracker != null && !PlayFabSettings.DisableFocusTimeCollection)
			{
				PlayFabHttp.screenTimeTracker.OnDestroy();
			}
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0001418C File Offset: 0x0001238C
		public void OnApplicationFocus(bool isFocused)
		{
			if (PlayFabHttp.screenTimeTracker != null && !PlayFabSettings.DisableFocusTimeCollection)
			{
				PlayFabHttp.screenTimeTracker.OnApplicationFocus(isFocused);
			}
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x000141A7 File Offset: 0x000123A7
		public void OnApplicationQuit()
		{
			if (PlayFabHttp.screenTimeTracker != null && !PlayFabSettings.DisableFocusTimeCollection)
			{
				PlayFabHttp.screenTimeTracker.OnApplicationQuit();
			}
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x000141C4 File Offset: 0x000123C4
		private void Update()
		{
			ITransportPlugin plugin = PluginManager.GetPlugin<ITransportPlugin>(PluginContract.PlayFab_Transport, "");
			if (plugin.IsInitialized)
			{
				if (PlayFabHttp._apiCallQueue != null)
				{
					foreach (CallRequestContainer callRequestContainer in PlayFabHttp._apiCallQueue)
					{
						plugin.MakeApiCall(callRequestContainer);
					}
					PlayFabHttp._apiCallQueue = null;
				}
				plugin.Update();
			}
			while (this._injectedCoroutines.Count > 0)
			{
				base.StartCoroutine(this._injectedCoroutines.Dequeue());
			}
			while (this._injectedAction.Count > 0)
			{
				Action action = this._injectedAction.Dequeue();
				if (action != null)
				{
					action();
				}
			}
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00014284 File Offset: 0x00012484
		protected internal static PlayFabError GeneratePlayFabError(string apiEndpoint, string json, object customData)
		{
			Dictionary<string, object> dictionary = null;
			Dictionary<string, List<string>> dictionary2 = null;
			ISerializerPlugin plugin = PluginManager.GetPlugin<ISerializerPlugin>(PluginContract.PlayFab_Serializer, "");
			try
			{
				dictionary = plugin.DeserializeObject<Dictionary<string, object>>(json);
			}
			catch (Exception)
			{
			}
			try
			{
				object obj;
				if (dictionary != null && dictionary.TryGetValue("errorDetails", out obj))
				{
					dictionary2 = plugin.DeserializeObject<Dictionary<string, List<string>>>(obj.ToString());
				}
			}
			catch (Exception)
			{
			}
			return new PlayFabError
			{
				ApiEndpoint = apiEndpoint,
				HttpCode = ((dictionary != null && dictionary.ContainsKey("code")) ? Convert.ToInt32(dictionary["code"]) : 400),
				HttpStatus = ((dictionary != null && dictionary.ContainsKey("status")) ? ((string)dictionary["status"]) : "BadRequest"),
				Error = (PlayFabErrorCode)((dictionary != null && dictionary.ContainsKey("errorCode")) ? Convert.ToInt32(dictionary["errorCode"]) : 1123),
				ErrorMessage = ((dictionary != null && dictionary.ContainsKey("errorMessage")) ? ((string)dictionary["errorMessage"]) : json),
				ErrorDetails = dictionary2,
				CustomData = customData
			};
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x000143BC File Offset: 0x000125BC
		protected internal static void SendErrorEvent(PlayFabRequestCommon request, PlayFabError error)
		{
			if (PlayFabHttp.ApiProcessingErrorEventHandler == null)
			{
				return;
			}
			try
			{
				PlayFabHttp.ApiProcessingErrorEventHandler(request, error);
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
			}
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x000143F8 File Offset: 0x000125F8
		protected internal static void SendEvent(string apiEndpoint, PlayFabRequestCommon request, PlayFabResultCommon result, ApiProcessingEventType eventType)
		{
			if (PlayFabHttp.ApiProcessingEventHandler == null)
			{
				return;
			}
			try
			{
				PlayFabHttp.ApiProcessingEventHandler(new ApiProcessingEventArgs
				{
					ApiEndpoint = apiEndpoint,
					EventType = eventType,
					Request = request,
					Result = result
				});
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
			}
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00014454 File Offset: 0x00012654
		public static void ClearAllEvents()
		{
			PlayFabHttp.ApiProcessingEventHandler = null;
			PlayFabHttp.ApiProcessingErrorEventHandler = null;
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00014462 File Offset: 0x00012662
		public void InjectInUnityThread(IEnumerator x)
		{
			this._injectedCoroutines.Enqueue(x);
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x00014470 File Offset: 0x00012670
		public void InjectInUnityThread(Action action)
		{
			this._injectedAction.Enqueue(action);
		}

		// Token: 0x0400092E RID: 2350
		private static List<CallRequestContainer> _apiCallQueue = new List<CallRequestContainer>();

		// Token: 0x04000931 RID: 2353
		public static readonly Dictionary<string, string> GlobalHeaderInjection = new Dictionary<string, string>();

		// Token: 0x04000932 RID: 2354
		private static IPlayFabLogger _logger;

		// Token: 0x04000933 RID: 2355
		private static IScreenTimeTracker screenTimeTracker = new ScreenTimeTracker();

		// Token: 0x04000934 RID: 2356
		private const float delayBetweenBatches = 5f;

		// Token: 0x04000935 RID: 2357
		private readonly Queue<IEnumerator> _injectedCoroutines = new Queue<IEnumerator>();

		// Token: 0x04000936 RID: 2358
		private readonly Queue<Action> _injectedAction = new Queue<Action>();

		// Token: 0x02000352 RID: 850
		// (Invoke) Token: 0x06000B97 RID: 2967
		public delegate void ApiProcessingEvent<in TEventArgs>(TEventArgs e);

		// Token: 0x02000353 RID: 851
		// (Invoke) Token: 0x06000B9B RID: 2971
		public delegate void ApiProcessErrorEvent(PlayFabRequestCommon request, PlayFabError error);
	}
}
