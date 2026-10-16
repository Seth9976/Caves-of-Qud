using System;
using System.Collections.Generic;
using PlayFab.Internal;
using PlayFab.LocalizationModels;

namespace PlayFab
{
	// Token: 0x02000014 RID: 20
	public static class PlayFabLocalizationAPI
	{
		// Token: 0x06000209 RID: 521 RVA: 0x0000C5C3 File Offset: 0x0000A7C3
		public static bool IsEntityLoggedIn()
		{
			return PlayFabSettings.staticPlayer.IsEntityLoggedIn();
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000C5CF File Offset: 0x0000A7CF
		public static void ForgetAllCredentials()
		{
			PlayFabSettings.staticPlayer.ForgetAllCredentials();
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000C5DC File Offset: 0x0000A7DC
		public static void GetLanguageList(GetLanguageListRequest request, Action<GetLanguageListResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabAuthenticationContext playFabAuthenticationContext = ((request == null) ? null : request.AuthenticationContext) ?? PlayFabSettings.staticPlayer;
			PlayFabApiSettings staticSettings = PlayFabSettings.staticSettings;
			if (!playFabAuthenticationContext.IsEntityLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<GetLanguageListResponse>("/Locale/GetLanguageList", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, playFabAuthenticationContext, staticSettings, null);
		}
	}
}
