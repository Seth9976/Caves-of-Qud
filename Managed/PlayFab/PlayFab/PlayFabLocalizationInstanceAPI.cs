using System;
using System.Collections.Generic;
using PlayFab.Internal;
using PlayFab.LocalizationModels;
using PlayFab.SharedModels;

namespace PlayFab
{
	// Token: 0x02000015 RID: 21
	public class PlayFabLocalizationInstanceAPI : IPlayFabInstanceApi
	{
		// Token: 0x0600020C RID: 524 RVA: 0x0000C62D File Offset: 0x0000A82D
		public PlayFabLocalizationInstanceAPI(PlayFabAuthenticationContext context)
		{
			if (context == null)
			{
				throw new PlayFabException(PlayFabExceptionCode.AuthContextRequired, "Context cannot be null, create a PlayFabAuthenticationContext for each player in advance, or call <PlayFabClientInstanceAPI>.GetAuthenticationContext()");
			}
			this.authenticationContext = context;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000C64B File Offset: 0x0000A84B
		public PlayFabLocalizationInstanceAPI(PlayFabApiSettings settings, PlayFabAuthenticationContext context)
		{
			if (context == null)
			{
				throw new PlayFabException(PlayFabExceptionCode.AuthContextRequired, "Context cannot be null, create a PlayFabAuthenticationContext for each player in advance, or call <PlayFabClientInstanceAPI>.GetAuthenticationContext()");
			}
			this.apiSettings = settings;
			this.authenticationContext = context;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000C670 File Offset: 0x0000A870
		public bool IsEntityLoggedIn()
		{
			return this.authenticationContext != null && this.authenticationContext.IsEntityLoggedIn();
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000C687 File Offset: 0x0000A887
		public void ForgetAllCredentials()
		{
			if (this.authenticationContext != null)
			{
				this.authenticationContext.ForgetAllCredentials();
			}
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000C69C File Offset: 0x0000A89C
		public void GetLanguageList(GetLanguageListRequest request, Action<GetLanguageListResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabAuthenticationContext playFabAuthenticationContext = ((request == null) ? null : request.AuthenticationContext) ?? this.authenticationContext;
			PlayFabApiSettings playFabApiSettings = this.apiSettings ?? PlayFabSettings.staticSettings;
			if (!playFabAuthenticationContext.IsEntityLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<GetLanguageListResponse>("/Locale/GetLanguageList", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, playFabAuthenticationContext, playFabApiSettings, this);
		}

		// Token: 0x04000022 RID: 34
		public readonly PlayFabApiSettings apiSettings;

		// Token: 0x04000023 RID: 35
		public readonly PlayFabAuthenticationContext authenticationContext;
	}
}
