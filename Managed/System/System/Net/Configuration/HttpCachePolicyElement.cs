using System;
using System.Configuration;
using System.Net.Cache;
using System.Xml;

namespace System.Net.Configuration
{
	/// <summary>Represents the default HTTP cache policy for network resources. This class cannot be inherited.</summary>
	// Token: 0x0200076C RID: 1900
	public sealed class HttpCachePolicyElement : ConfigurationElement
	{
		// Token: 0x06003BD5 RID: 15317 RVA: 0x000CD06C File Offset: 0x000CB26C
		static HttpCachePolicyElement()
		{
			HttpCachePolicyElement.properties.Add(HttpCachePolicyElement.maximumAgeProp);
			HttpCachePolicyElement.properties.Add(HttpCachePolicyElement.maximumStaleProp);
			HttpCachePolicyElement.properties.Add(HttpCachePolicyElement.minimumFreshProp);
			HttpCachePolicyElement.properties.Add(HttpCachePolicyElement.policyLevelProp);
		}

		/// <summary>Gets or sets the maximum age permitted for a resource returned from the cache.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> value that specifies the maximum age for cached resources specified in the configuration file.</returns>
		// Token: 0x17000D88 RID: 3464
		// (get) Token: 0x06003BD7 RID: 15319 RVA: 0x000CD148 File Offset: 0x000CB348
		// (set) Token: 0x06003BD8 RID: 15320 RVA: 0x000CD15A File Offset: 0x000CB35A
		[ConfigurationProperty("maximumAge", DefaultValue = "10675199.02:48:05.4775807")]
		public TimeSpan MaximumAge
		{
			get
			{
				return (TimeSpan)base[HttpCachePolicyElement.maximumAgeProp];
			}
			set
			{
				base[HttpCachePolicyElement.maximumAgeProp] = value;
			}
		}

		/// <summary>Gets or sets the maximum staleness value permitted for a resource returned from the cache.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> value that is set to the maximum staleness value specified in the configuration file.</returns>
		// Token: 0x17000D89 RID: 3465
		// (get) Token: 0x06003BD9 RID: 15321 RVA: 0x000CD16D File Offset: 0x000CB36D
		// (set) Token: 0x06003BDA RID: 15322 RVA: 0x000CD17F File Offset: 0x000CB37F
		[ConfigurationProperty("maximumStale", DefaultValue = "-10675199.02:48:05.4775808")]
		public TimeSpan MaximumStale
		{
			get
			{
				return (TimeSpan)base[HttpCachePolicyElement.maximumStaleProp];
			}
			set
			{
				base[HttpCachePolicyElement.maximumStaleProp] = value;
			}
		}

		/// <summary>Gets or sets the minimum freshness permitted for a resource returned from the cache.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> value that specifies the minimum freshness specified in the configuration file.</returns>
		// Token: 0x17000D8A RID: 3466
		// (get) Token: 0x06003BDB RID: 15323 RVA: 0x000CD192 File Offset: 0x000CB392
		// (set) Token: 0x06003BDC RID: 15324 RVA: 0x000CD1A4 File Offset: 0x000CB3A4
		[ConfigurationProperty("minimumFresh", DefaultValue = "-10675199.02:48:05.4775808")]
		public TimeSpan MinimumFresh
		{
			get
			{
				return (TimeSpan)base[HttpCachePolicyElement.minimumFreshProp];
			}
			set
			{
				base[HttpCachePolicyElement.minimumFreshProp] = value;
			}
		}

		/// <summary>Gets or sets HTTP caching behavior for the local machine.</summary>
		/// <returns>A <see cref="T:System.Net.Cache.HttpRequestCacheLevel" /> value that specifies the cache behavior.</returns>
		// Token: 0x17000D8B RID: 3467
		// (get) Token: 0x06003BDD RID: 15325 RVA: 0x000CD1B7 File Offset: 0x000CB3B7
		// (set) Token: 0x06003BDE RID: 15326 RVA: 0x000CD1C9 File Offset: 0x000CB3C9
		[ConfigurationProperty("policyLevel", DefaultValue = "Default", Options = ConfigurationPropertyOptions.IsRequired)]
		public HttpRequestCacheLevel PolicyLevel
		{
			get
			{
				return (HttpRequestCacheLevel)base[HttpCachePolicyElement.policyLevelProp];
			}
			set
			{
				base[HttpCachePolicyElement.policyLevelProp] = value;
			}
		}

		// Token: 0x17000D8C RID: 3468
		// (get) Token: 0x06003BDF RID: 15327 RVA: 0x000CD1DC File Offset: 0x000CB3DC
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return HttpCachePolicyElement.properties;
			}
		}

		// Token: 0x06003BE0 RID: 15328 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06003BE1 RID: 15329 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		protected override void Reset(ConfigurationElement parentElement)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040023B1 RID: 9137
		private static ConfigurationProperty maximumAgeProp = new ConfigurationProperty("maximumAge", typeof(TimeSpan), TimeSpan.MaxValue);

		// Token: 0x040023B2 RID: 9138
		private static ConfigurationProperty maximumStaleProp = new ConfigurationProperty("maximumStale", typeof(TimeSpan), TimeSpan.MinValue);

		// Token: 0x040023B3 RID: 9139
		private static ConfigurationProperty minimumFreshProp = new ConfigurationProperty("minimumFresh", typeof(TimeSpan), TimeSpan.MinValue);

		// Token: 0x040023B4 RID: 9140
		private static ConfigurationProperty policyLevelProp = new ConfigurationProperty("policyLevel", typeof(HttpRequestCacheLevel), HttpRequestCacheLevel.Default, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x040023B5 RID: 9141
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
