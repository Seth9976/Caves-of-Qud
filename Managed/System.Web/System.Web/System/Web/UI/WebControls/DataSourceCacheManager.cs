using System;
using System.Text;
using System.Web.Caching;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000380 RID: 896
	internal class DataSourceCacheManager
	{
		// Token: 0x06002289 RID: 8841 RVA: 0x000571AC File Offset: 0x000553AC
		internal DataSourceCacheManager(int cacheDuration, string cacheKeyDependency, DataSourceCacheExpiry cacheExpirationPolicy, Control owner, HttpContext context)
		{
			this.cacheDuration = cacheDuration;
			this.cacheKeyDependency = cacheKeyDependency;
			this.cacheExpirationPolicy = cacheExpirationPolicy;
			this.controlID = owner.UniqueID;
			this.owner = owner;
			this.context = context;
			if (DataSourceCacheManager.DataCache[this.controlID] == null)
			{
				DataSourceCacheManager.DataCache[this.controlID] = new object();
			}
		}

		// Token: 0x0600228A RID: 8842 RVA: 0x00057218 File Offset: 0x00055418
		internal void Expire()
		{
			DataSourceCacheManager.DataCache[this.controlID] = new object();
		}

		// Token: 0x0600228B RID: 8843 RVA: 0x0005722F File Offset: 0x0005542F
		internal object GetCachedObject(string methodName, ParameterCollection parameters)
		{
			return DataSourceCacheManager.DataCache[this.GetKeyFromParameters(methodName, parameters)];
		}

		// Token: 0x0600228C RID: 8844 RVA: 0x00057244 File Offset: 0x00055444
		internal void SetCachedObject(string methodName, ParameterCollection parameters, object o)
		{
			if (o == null)
			{
				return;
			}
			string keyFromParameters = this.GetKeyFromParameters(methodName, parameters);
			if (DataSourceCacheManager.DataCache[keyFromParameters] != null)
			{
				DataSourceCacheManager.DataCache.Remove(keyFromParameters);
			}
			DateTime dateTime = Cache.NoAbsoluteExpiration;
			TimeSpan noSlidingExpiration = Cache.NoSlidingExpiration;
			if (this.cacheDuration > 0)
			{
				if (this.cacheExpirationPolicy == DataSourceCacheExpiry.Absolute)
				{
					dateTime = DateTime.Now.AddSeconds((double)this.cacheDuration);
				}
				else
				{
					noSlidingExpiration = new TimeSpan(0, 0, this.cacheDuration);
				}
			}
			string[] array;
			if (this.cacheKeyDependency.Length > 0)
			{
				array = new string[] { this.cacheKeyDependency };
			}
			else
			{
				array = new string[0];
			}
			DataSourceCacheManager.DataCache.Add(keyFromParameters, o, new CacheDependency(new string[0], array), dateTime, noSlidingExpiration, CacheItemPriority.Normal, null);
		}

		// Token: 0x17000B00 RID: 2816
		// (get) Token: 0x0600228D RID: 8845 RVA: 0x000572FF File Offset: 0x000554FF
		private static Cache DataCache
		{
			get
			{
				if (HttpContext.Current != null)
				{
					return HttpContext.Current.InternalCache;
				}
				throw new InvalidOperationException("HttpContext.Current is null.");
			}
		}

		// Token: 0x0600228E RID: 8846 RVA: 0x00057320 File Offset: 0x00055520
		private string GetKeyFromParameters(string methodName, ParameterCollection parameters)
		{
			StringBuilder stringBuilder = new StringBuilder(methodName);
			if (this.owner != null)
			{
				stringBuilder.Append(this.owner.ID);
			}
			for (int i = 0; i < parameters.Count; i++)
			{
				stringBuilder.Append(parameters[i].Name);
				stringBuilder.Append(parameters[i].GetValue(this.context, this.owner));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400192E RID: 6446
		private readonly int cacheDuration;

		// Token: 0x0400192F RID: 6447
		private readonly string cacheKeyDependency;

		// Token: 0x04001930 RID: 6448
		private readonly string controlID;

		// Token: 0x04001931 RID: 6449
		private readonly DataSourceCacheExpiry cacheExpirationPolicy;

		// Token: 0x04001932 RID: 6450
		private readonly Control owner;

		// Token: 0x04001933 RID: 6451
		private readonly HttpContext context;
	}
}
