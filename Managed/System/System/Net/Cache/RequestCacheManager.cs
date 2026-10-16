using System;

namespace System.Net.Cache
{
	// Token: 0x02000787 RID: 1927
	internal sealed class RequestCacheManager
	{
		// Token: 0x06003CBF RID: 15551 RVA: 0x0000219B File Offset: 0x0000039B
		private RequestCacheManager()
		{
		}

		// Token: 0x06003CC0 RID: 15552 RVA: 0x000CEA3C File Offset: 0x000CCC3C
		internal static RequestCacheBinding GetBinding(string internedScheme)
		{
			if (internedScheme == null)
			{
				throw new ArgumentNullException("uriScheme");
			}
			if (RequestCacheManager.s_CacheConfigSettings == null)
			{
				RequestCacheManager.LoadConfigSettings();
			}
			if (RequestCacheManager.s_CacheConfigSettings.DisableAllCaching)
			{
				return RequestCacheManager.s_BypassCacheBinding;
			}
			if (internedScheme.Length == 0)
			{
				return RequestCacheManager.s_DefaultGlobalBinding;
			}
			if (internedScheme == Uri.UriSchemeHttp || internedScheme == Uri.UriSchemeHttps)
			{
				return RequestCacheManager.s_DefaultHttpBinding;
			}
			if (internedScheme == Uri.UriSchemeFtp)
			{
				return RequestCacheManager.s_DefaultFtpBinding;
			}
			return RequestCacheManager.s_BypassCacheBinding;
		}

		// Token: 0x17000DEC RID: 3564
		// (get) Token: 0x06003CC1 RID: 15553 RVA: 0x000CEAB6 File Offset: 0x000CCCB6
		internal static bool IsCachingEnabled
		{
			get
			{
				if (RequestCacheManager.s_CacheConfigSettings == null)
				{
					RequestCacheManager.LoadConfigSettings();
				}
				return !RequestCacheManager.s_CacheConfigSettings.DisableAllCaching;
			}
		}

		// Token: 0x06003CC2 RID: 15554 RVA: 0x000CEAD8 File Offset: 0x000CCCD8
		internal static void SetBinding(string uriScheme, RequestCacheBinding binding)
		{
			if (uriScheme == null)
			{
				throw new ArgumentNullException("uriScheme");
			}
			if (RequestCacheManager.s_CacheConfigSettings == null)
			{
				RequestCacheManager.LoadConfigSettings();
			}
			if (RequestCacheManager.s_CacheConfigSettings.DisableAllCaching)
			{
				return;
			}
			if (uriScheme.Length == 0)
			{
				RequestCacheManager.s_DefaultGlobalBinding = binding;
				return;
			}
			if (uriScheme == Uri.UriSchemeHttp || uriScheme == Uri.UriSchemeHttps)
			{
				RequestCacheManager.s_DefaultHttpBinding = binding;
				return;
			}
			if (uriScheme == Uri.UriSchemeFtp)
			{
				RequestCacheManager.s_DefaultFtpBinding = binding;
			}
		}

		// Token: 0x06003CC3 RID: 15555 RVA: 0x000CEB5C File Offset: 0x000CCD5C
		private static void LoadConfigSettings()
		{
			RequestCacheBinding requestCacheBinding = RequestCacheManager.s_BypassCacheBinding;
			lock (requestCacheBinding)
			{
				if (RequestCacheManager.s_CacheConfigSettings == null)
				{
					RequestCacheManager.s_CacheConfigSettings = new RequestCachingSectionInternal();
				}
			}
		}

		// Token: 0x04002403 RID: 9219
		private static volatile RequestCachingSectionInternal s_CacheConfigSettings;

		// Token: 0x04002404 RID: 9220
		private static readonly RequestCacheBinding s_BypassCacheBinding = new RequestCacheBinding(null, null, new RequestCachePolicy(RequestCacheLevel.BypassCache));

		// Token: 0x04002405 RID: 9221
		private static volatile RequestCacheBinding s_DefaultGlobalBinding;

		// Token: 0x04002406 RID: 9222
		private static volatile RequestCacheBinding s_DefaultHttpBinding;

		// Token: 0x04002407 RID: 9223
		private static volatile RequestCacheBinding s_DefaultFtpBinding;
	}
}
