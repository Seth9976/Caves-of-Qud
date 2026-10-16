using System;

namespace System.Net.Cache
{
	// Token: 0x0200078A RID: 1930
	internal class RequestCacheBinding
	{
		// Token: 0x06003CC8 RID: 15560 RVA: 0x000CEBCF File Offset: 0x000CCDCF
		internal RequestCacheBinding(RequestCache requestCache, RequestCacheValidator cacheValidator, RequestCachePolicy policy)
		{
			this.m_RequestCache = requestCache;
			this.m_CacheValidator = cacheValidator;
			this.m_Policy = policy;
		}

		// Token: 0x17000DED RID: 3565
		// (get) Token: 0x06003CC9 RID: 15561 RVA: 0x000CEBEC File Offset: 0x000CCDEC
		internal RequestCache Cache
		{
			get
			{
				return this.m_RequestCache;
			}
		}

		// Token: 0x17000DEE RID: 3566
		// (get) Token: 0x06003CCA RID: 15562 RVA: 0x000CEBF4 File Offset: 0x000CCDF4
		internal RequestCacheValidator Validator
		{
			get
			{
				return this.m_CacheValidator;
			}
		}

		// Token: 0x17000DEF RID: 3567
		// (get) Token: 0x06003CCB RID: 15563 RVA: 0x000CEBFC File Offset: 0x000CCDFC
		internal RequestCachePolicy Policy
		{
			get
			{
				return this.m_Policy;
			}
		}

		// Token: 0x04002409 RID: 9225
		private RequestCache m_RequestCache;

		// Token: 0x0400240A RID: 9226
		private RequestCacheValidator m_CacheValidator;

		// Token: 0x0400240B RID: 9227
		private RequestCachePolicy m_Policy;
	}
}
