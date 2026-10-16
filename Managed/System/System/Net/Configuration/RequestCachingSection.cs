using System;
using System.Configuration;
using System.Net.Cache;
using System.Xml;

namespace System.Net.Configuration
{
	/// <summary>Represents the configuration section for cache behavior. This class cannot be inherited.</summary>
	// Token: 0x02000779 RID: 1913
	public sealed class RequestCachingSection : ConfigurationSection
	{
		// Token: 0x06003C1A RID: 15386 RVA: 0x000CDA50 File Offset: 0x000CBC50
		static RequestCachingSection()
		{
			RequestCachingSection.properties.Add(RequestCachingSection.defaultFtpCachePolicyProp);
			RequestCachingSection.properties.Add(RequestCachingSection.defaultHttpCachePolicyProp);
			RequestCachingSection.properties.Add(RequestCachingSection.defaultPolicyLevelProp);
			RequestCachingSection.properties.Add(RequestCachingSection.disableAllCachingProp);
			RequestCachingSection.properties.Add(RequestCachingSection.isPrivateCacheProp);
			RequestCachingSection.properties.Add(RequestCachingSection.unspecifiedMaximumAgeProp);
		}

		/// <summary>Gets the default FTP caching behavior for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.FtpCachePolicyElement" /> that defines the default cache policy.</returns>
		// Token: 0x17000DA6 RID: 3494
		// (get) Token: 0x06003C1C RID: 15388 RVA: 0x000CDB77 File Offset: 0x000CBD77
		[ConfigurationProperty("defaultFtpCachePolicy")]
		public FtpCachePolicyElement DefaultFtpCachePolicy
		{
			get
			{
				return (FtpCachePolicyElement)base[RequestCachingSection.defaultFtpCachePolicyProp];
			}
		}

		/// <summary>Gets the default caching behavior for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.HttpCachePolicyElement" /> that defines the default cache policy.</returns>
		// Token: 0x17000DA7 RID: 3495
		// (get) Token: 0x06003C1D RID: 15389 RVA: 0x000CDB89 File Offset: 0x000CBD89
		[ConfigurationProperty("defaultHttpCachePolicy")]
		public HttpCachePolicyElement DefaultHttpCachePolicy
		{
			get
			{
				return (HttpCachePolicyElement)base[RequestCachingSection.defaultHttpCachePolicyProp];
			}
		}

		/// <summary>Gets or sets the default cache policy level.</summary>
		/// <returns>A <see cref="T:System.Net.Cache.RequestCacheLevel" /> enumeration value.</returns>
		// Token: 0x17000DA8 RID: 3496
		// (get) Token: 0x06003C1E RID: 15390 RVA: 0x000CDB9B File Offset: 0x000CBD9B
		// (set) Token: 0x06003C1F RID: 15391 RVA: 0x000CDBAD File Offset: 0x000CBDAD
		[ConfigurationProperty("defaultPolicyLevel", DefaultValue = "BypassCache")]
		public RequestCacheLevel DefaultPolicyLevel
		{
			get
			{
				return (RequestCacheLevel)base[RequestCachingSection.defaultPolicyLevelProp];
			}
			set
			{
				base[RequestCachingSection.defaultPolicyLevelProp] = value;
			}
		}

		/// <summary>Gets or sets a Boolean value that enables caching on the local computer.</summary>
		/// <returns>true if caching is disabled on the local computer; otherwise, false.</returns>
		// Token: 0x17000DA9 RID: 3497
		// (get) Token: 0x06003C20 RID: 15392 RVA: 0x000CDBC0 File Offset: 0x000CBDC0
		// (set) Token: 0x06003C21 RID: 15393 RVA: 0x000CDBD2 File Offset: 0x000CBDD2
		[ConfigurationProperty("disableAllCaching", DefaultValue = "False")]
		public bool DisableAllCaching
		{
			get
			{
				return (bool)base[RequestCachingSection.disableAllCachingProp];
			}
			set
			{
				base[RequestCachingSection.disableAllCachingProp] = value;
			}
		}

		/// <summary>Gets or sets a Boolean value that indicates whether the local computer cache is private.</summary>
		/// <returns>true if the cache provides user isolation; otherwise, false.</returns>
		// Token: 0x17000DAA RID: 3498
		// (get) Token: 0x06003C22 RID: 15394 RVA: 0x000CDBE5 File Offset: 0x000CBDE5
		// (set) Token: 0x06003C23 RID: 15395 RVA: 0x000CDBF7 File Offset: 0x000CBDF7
		[ConfigurationProperty("isPrivateCache", DefaultValue = "True")]
		public bool IsPrivateCache
		{
			get
			{
				return (bool)base[RequestCachingSection.isPrivateCacheProp];
			}
			set
			{
				base[RequestCachingSection.isPrivateCacheProp] = value;
			}
		}

		/// <summary>Gets or sets a value used as the maximum age for cached resources that do not have expiration information.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> that provides a default maximum age for cached resources.</returns>
		// Token: 0x17000DAB RID: 3499
		// (get) Token: 0x06003C24 RID: 15396 RVA: 0x000CDC0A File Offset: 0x000CBE0A
		// (set) Token: 0x06003C25 RID: 15397 RVA: 0x000CDC1C File Offset: 0x000CBE1C
		[ConfigurationProperty("unspecifiedMaximumAge", DefaultValue = "1.00:00:00")]
		public TimeSpan UnspecifiedMaximumAge
		{
			get
			{
				return (TimeSpan)base[RequestCachingSection.unspecifiedMaximumAgeProp];
			}
			set
			{
				base[RequestCachingSection.unspecifiedMaximumAgeProp] = value;
			}
		}

		// Token: 0x17000DAC RID: 3500
		// (get) Token: 0x06003C26 RID: 15398 RVA: 0x000CDC2F File Offset: 0x000CBE2F
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return RequestCachingSection.properties;
			}
		}

		// Token: 0x06003C27 RID: 15399 RVA: 0x00066D5E File Offset: 0x00064F5E
		[MonoTODO]
		protected override void PostDeserialize()
		{
			base.PostDeserialize();
		}

		// Token: 0x06003C28 RID: 15400 RVA: 0x000CDC36 File Offset: 0x000CBE36
		[MonoTODO]
		protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
		{
			base.DeserializeElement(reader, serializeCollectionKey);
		}

		// Token: 0x040023D3 RID: 9171
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x040023D4 RID: 9172
		private static ConfigurationProperty defaultFtpCachePolicyProp = new ConfigurationProperty("defaultFtpCachePolicy", typeof(FtpCachePolicyElement));

		// Token: 0x040023D5 RID: 9173
		private static ConfigurationProperty defaultHttpCachePolicyProp = new ConfigurationProperty("defaultHttpCachePolicy", typeof(HttpCachePolicyElement));

		// Token: 0x040023D6 RID: 9174
		private static ConfigurationProperty defaultPolicyLevelProp = new ConfigurationProperty("defaultPolicyLevel", typeof(RequestCacheLevel), RequestCacheLevel.BypassCache);

		// Token: 0x040023D7 RID: 9175
		private static ConfigurationProperty disableAllCachingProp = new ConfigurationProperty("disableAllCaching", typeof(bool), false);

		// Token: 0x040023D8 RID: 9176
		private static ConfigurationProperty isPrivateCacheProp = new ConfigurationProperty("isPrivateCache", typeof(bool), true);

		// Token: 0x040023D9 RID: 9177
		private static ConfigurationProperty unspecifiedMaximumAgeProp = new ConfigurationProperty("unspecifiedMaximumAge", typeof(TimeSpan), new TimeSpan(1, 0, 0, 0));
	}
}
