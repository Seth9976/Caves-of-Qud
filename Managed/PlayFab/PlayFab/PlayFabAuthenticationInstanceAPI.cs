using System;
using System.Collections.Generic;
using PlayFab.AuthenticationModels;
using PlayFab.Internal;
using PlayFab.SharedModels;

namespace PlayFab
{
	// Token: 0x02000005 RID: 5
	public class PlayFabAuthenticationInstanceAPI : IPlayFabInstanceApi
	{
		// Token: 0x06000009 RID: 9 RVA: 0x000021D9 File Offset: 0x000003D9
		public PlayFabAuthenticationInstanceAPI()
		{
			this.authenticationContext = new PlayFabAuthenticationContext();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021EC File Offset: 0x000003EC
		public PlayFabAuthenticationInstanceAPI(PlayFabApiSettings settings)
		{
			this.apiSettings = settings;
			this.authenticationContext = new PlayFabAuthenticationContext();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002206 File Offset: 0x00000406
		public PlayFabAuthenticationInstanceAPI(PlayFabAuthenticationContext context)
		{
			this.authenticationContext = context ?? new PlayFabAuthenticationContext();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000221E File Offset: 0x0000041E
		public PlayFabAuthenticationInstanceAPI(PlayFabApiSettings settings, PlayFabAuthenticationContext context)
		{
			this.apiSettings = settings;
			this.authenticationContext = context ?? new PlayFabAuthenticationContext();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000223D File Offset: 0x0000043D
		public bool IsEntityLoggedIn()
		{
			return this.authenticationContext != null && this.authenticationContext.IsEntityLoggedIn();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002254 File Offset: 0x00000454
		public void ForgetAllCredentials()
		{
			if (this.authenticationContext != null)
			{
				this.authenticationContext.ForgetAllCredentials();
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000226C File Offset: 0x0000046C
		public void GetEntityToken(GetEntityTokenRequest request, Action<GetEntityTokenResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabAuthenticationContext playFabAuthenticationContext = ((request == null) ? null : request.AuthenticationContext) ?? this.authenticationContext;
			PlayFabApiSettings playFabApiSettings = this.apiSettings ?? PlayFabSettings.staticSettings;
			AuthType authType = AuthType.None;
			if (playFabAuthenticationContext.IsClientLoggedIn())
			{
				authType = AuthType.LoginSession;
			}
			if (playFabAuthenticationContext.IsEntityLoggedIn())
			{
				authType = AuthType.EntityToken;
			}
			PlayFabHttp.MakeApiCall<GetEntityTokenResponse>("/Authentication/GetEntityToken", request, authType, resultCallback, errorCallback, customData, extraHeaders, playFabAuthenticationContext, playFabApiSettings, this);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000022CC File Offset: 0x000004CC
		public void ValidateEntityToken(ValidateEntityTokenRequest request, Action<ValidateEntityTokenResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabAuthenticationContext playFabAuthenticationContext = ((request == null) ? null : request.AuthenticationContext) ?? this.authenticationContext;
			PlayFabApiSettings playFabApiSettings = this.apiSettings ?? PlayFabSettings.staticSettings;
			if (!playFabAuthenticationContext.IsEntityLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<ValidateEntityTokenResponse>("/Authentication/ValidateEntityToken", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, playFabAuthenticationContext, playFabApiSettings, this);
		}

		// Token: 0x04000012 RID: 18
		public readonly PlayFabApiSettings apiSettings;

		// Token: 0x04000013 RID: 19
		public readonly PlayFabAuthenticationContext authenticationContext;
	}
}
