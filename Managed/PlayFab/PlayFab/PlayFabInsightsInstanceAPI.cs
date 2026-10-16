using System;
using System.Collections.Generic;
using PlayFab.InsightsModels;
using PlayFab.Internal;
using PlayFab.SharedModels;

namespace PlayFab
{
	// Token: 0x02000013 RID: 19
	public class PlayFabInsightsInstanceAPI : IPlayFabInstanceApi
	{
		// Token: 0x060001FE RID: 510 RVA: 0x0000C315 File Offset: 0x0000A515
		public PlayFabInsightsInstanceAPI(PlayFabAuthenticationContext context)
		{
			if (context == null)
			{
				throw new PlayFabException(PlayFabExceptionCode.AuthContextRequired, "Context cannot be null, create a PlayFabAuthenticationContext for each player in advance, or call <PlayFabClientInstanceAPI>.GetAuthenticationContext()");
			}
			this.authenticationContext = context;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000C333 File Offset: 0x0000A533
		public PlayFabInsightsInstanceAPI(PlayFabApiSettings settings, PlayFabAuthenticationContext context)
		{
			if (context == null)
			{
				throw new PlayFabException(PlayFabExceptionCode.AuthContextRequired, "Context cannot be null, create a PlayFabAuthenticationContext for each player in advance, or call <PlayFabClientInstanceAPI>.GetAuthenticationContext()");
			}
			this.apiSettings = settings;
			this.authenticationContext = context;
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000C358 File Offset: 0x0000A558
		public bool IsEntityLoggedIn()
		{
			return this.authenticationContext != null && this.authenticationContext.IsEntityLoggedIn();
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000C36F File Offset: 0x0000A56F
		public void ForgetAllCredentials()
		{
			if (this.authenticationContext != null)
			{
				this.authenticationContext.ForgetAllCredentials();
			}
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000C384 File Offset: 0x0000A584
		public void GetDetails(InsightsEmptyRequest request, Action<InsightsGetDetailsResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabAuthenticationContext playFabAuthenticationContext = ((request == null) ? null : request.AuthenticationContext) ?? this.authenticationContext;
			PlayFabApiSettings playFabApiSettings = this.apiSettings ?? PlayFabSettings.staticSettings;
			if (!playFabAuthenticationContext.IsEntityLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<InsightsGetDetailsResponse>("/Insights/GetDetails", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, playFabAuthenticationContext, playFabApiSettings, this);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000C3E4 File Offset: 0x0000A5E4
		public void GetLimits(InsightsEmptyRequest request, Action<InsightsGetLimitsResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabAuthenticationContext playFabAuthenticationContext = ((request == null) ? null : request.AuthenticationContext) ?? this.authenticationContext;
			PlayFabApiSettings playFabApiSettings = this.apiSettings ?? PlayFabSettings.staticSettings;
			if (!playFabAuthenticationContext.IsEntityLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<InsightsGetLimitsResponse>("/Insights/GetLimits", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, playFabAuthenticationContext, playFabApiSettings, this);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000C444 File Offset: 0x0000A644
		public void GetOperationStatus(InsightsGetOperationStatusRequest request, Action<InsightsGetOperationStatusResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabAuthenticationContext playFabAuthenticationContext = ((request == null) ? null : request.AuthenticationContext) ?? this.authenticationContext;
			PlayFabApiSettings playFabApiSettings = this.apiSettings ?? PlayFabSettings.staticSettings;
			if (!playFabAuthenticationContext.IsEntityLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<InsightsGetOperationStatusResponse>("/Insights/GetOperationStatus", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, playFabAuthenticationContext, playFabApiSettings, this);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000C4A4 File Offset: 0x0000A6A4
		public void GetPendingOperations(InsightsGetPendingOperationsRequest request, Action<InsightsGetPendingOperationsResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabAuthenticationContext playFabAuthenticationContext = ((request == null) ? null : request.AuthenticationContext) ?? this.authenticationContext;
			PlayFabApiSettings playFabApiSettings = this.apiSettings ?? PlayFabSettings.staticSettings;
			if (!playFabAuthenticationContext.IsEntityLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<InsightsGetPendingOperationsResponse>("/Insights/GetPendingOperations", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, playFabAuthenticationContext, playFabApiSettings, this);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000C504 File Offset: 0x0000A704
		public void SetPerformance(InsightsSetPerformanceRequest request, Action<InsightsOperationResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabAuthenticationContext playFabAuthenticationContext = ((request == null) ? null : request.AuthenticationContext) ?? this.authenticationContext;
			PlayFabApiSettings playFabApiSettings = this.apiSettings ?? PlayFabSettings.staticSettings;
			if (!playFabAuthenticationContext.IsEntityLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<InsightsOperationResponse>("/Insights/SetPerformance", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, playFabAuthenticationContext, playFabApiSettings, this);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000C564 File Offset: 0x0000A764
		public void SetStorageRetention(InsightsSetStorageRetentionRequest request, Action<InsightsOperationResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabAuthenticationContext playFabAuthenticationContext = ((request == null) ? null : request.AuthenticationContext) ?? this.authenticationContext;
			PlayFabApiSettings playFabApiSettings = this.apiSettings ?? PlayFabSettings.staticSettings;
			if (!playFabAuthenticationContext.IsEntityLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<InsightsOperationResponse>("/Insights/SetStorageRetention", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, playFabAuthenticationContext, playFabApiSettings, this);
		}

		// Token: 0x04000020 RID: 32
		public readonly PlayFabApiSettings apiSettings;

		// Token: 0x04000021 RID: 33
		public readonly PlayFabAuthenticationContext authenticationContext;
	}
}
