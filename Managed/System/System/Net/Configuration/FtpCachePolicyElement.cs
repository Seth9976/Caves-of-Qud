using System;
using System.Configuration;
using System.Net.Cache;
using System.Xml;

namespace System.Net.Configuration
{
	/// <summary>Represents the default FTP cache policy for network resources. This class cannot be inherited.</summary>
	// Token: 0x0200076B RID: 1899
	public sealed class FtpCachePolicyElement : ConfigurationElement
	{
		// Token: 0x06003BCE RID: 15310 RVA: 0x000CD005 File Offset: 0x000CB205
		static FtpCachePolicyElement()
		{
			FtpCachePolicyElement.properties.Add(FtpCachePolicyElement.policyLevelProp);
		}

		/// <summary>Gets or sets FTP caching behavior for the local machine.</summary>
		/// <returns>A <see cref="T:System.Net.Cache.RequestCacheLevel" /> value that specifies the cache behavior.</returns>
		// Token: 0x17000D86 RID: 3462
		// (get) Token: 0x06003BD0 RID: 15312 RVA: 0x000CD03F File Offset: 0x000CB23F
		// (set) Token: 0x06003BD1 RID: 15313 RVA: 0x000CD051 File Offset: 0x000CB251
		[ConfigurationProperty("policyLevel", DefaultValue = "Default")]
		public RequestCacheLevel PolicyLevel
		{
			get
			{
				return (RequestCacheLevel)base[FtpCachePolicyElement.policyLevelProp];
			}
			set
			{
				base[FtpCachePolicyElement.policyLevelProp] = value;
			}
		}

		// Token: 0x17000D87 RID: 3463
		// (get) Token: 0x06003BD2 RID: 15314 RVA: 0x000CD064 File Offset: 0x000CB264
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return FtpCachePolicyElement.properties;
			}
		}

		// Token: 0x06003BD3 RID: 15315 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06003BD4 RID: 15316 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		protected override void Reset(ConfigurationElement parentElement)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040023AF RID: 9135
		private static ConfigurationProperty policyLevelProp = new ConfigurationProperty("policyLevel", typeof(RequestCacheLevel), RequestCacheLevel.Default);

		// Token: 0x040023B0 RID: 9136
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
