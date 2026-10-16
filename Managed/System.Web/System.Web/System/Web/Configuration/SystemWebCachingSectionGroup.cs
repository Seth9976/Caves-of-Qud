using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Configures the caching group within a configuration file. This class cannot be inherited. </summary>
	// Token: 0x02000574 RID: 1396
	public sealed class SystemWebCachingSectionGroup : ConfigurationSectionGroup
	{
		/// <summary>Gets the cache section contained within the configuration.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.CacheSection" /> object.</returns>
		// Token: 0x17001233 RID: 4659
		// (get) Token: 0x06003B27 RID: 15143 RVA: 0x0009C77F File Offset: 0x0009A97F
		[ConfigurationProperty("cache")]
		public CacheSection Cache
		{
			get
			{
				return (CacheSection)base.Sections["cache"];
			}
		}

		/// <summary>Gets the outputCache section contained within the configuration.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.OutputCacheSection" /> object.</returns>
		// Token: 0x17001234 RID: 4660
		// (get) Token: 0x06003B28 RID: 15144 RVA: 0x0009C796 File Offset: 0x0009A996
		[ConfigurationProperty("outputCache")]
		public OutputCacheSection OutputCache
		{
			get
			{
				return (OutputCacheSection)base.Sections["outputCache"];
			}
		}

		/// <summary>Gets the outputCacheSettings section contained within the configuration.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.OutputCacheSettingsSection" /> object.</returns>
		// Token: 0x17001235 RID: 4661
		// (get) Token: 0x06003B29 RID: 15145 RVA: 0x0009C7AD File Offset: 0x0009A9AD
		[ConfigurationProperty("outputCacheSettings")]
		public OutputCacheSettingsSection OutputCacheSettings
		{
			get
			{
				return (OutputCacheSettingsSection)base.Sections["outputCacheSettings"];
			}
		}

		/// <summary>Gets the sqlCacheDependency section contained within the configuration.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.SqlCacheDependencySection" /> object.</returns>
		// Token: 0x17001236 RID: 4662
		// (get) Token: 0x06003B2A RID: 15146 RVA: 0x0009C7C4 File Offset: 0x0009A9C4
		[ConfigurationProperty("sqlCacheDependency")]
		public SqlCacheDependencySection SqlCacheDependency
		{
			get
			{
				return (SqlCacheDependencySection)base.Sections["sqlCacheDependency"];
			}
		}
	}
}
