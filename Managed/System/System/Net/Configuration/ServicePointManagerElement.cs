using System;
using System.Configuration;
using System.Net.Security;
using Unity;

namespace System.Net.Configuration
{
	/// <summary>Represents the default settings used to create connections to a remote computer. This class cannot be inherited.</summary>
	// Token: 0x0200077A RID: 1914
	public sealed class ServicePointManagerElement : ConfigurationElement
	{
		// Token: 0x06003C29 RID: 15401 RVA: 0x000CDC40 File Offset: 0x000CBE40
		static ServicePointManagerElement()
		{
			ServicePointManagerElement.properties.Add(ServicePointManagerElement.checkCertificateNameProp);
			ServicePointManagerElement.properties.Add(ServicePointManagerElement.checkCertificateRevocationListProp);
			ServicePointManagerElement.properties.Add(ServicePointManagerElement.dnsRefreshTimeoutProp);
			ServicePointManagerElement.properties.Add(ServicePointManagerElement.enableDnsRoundRobinProp);
			ServicePointManagerElement.properties.Add(ServicePointManagerElement.expect100ContinueProp);
			ServicePointManagerElement.properties.Add(ServicePointManagerElement.useNagleAlgorithmProp);
		}

		/// <summary>Gets or sets a Boolean value that controls checking host name information in an X509 certificate.</summary>
		/// <returns>true to specify host name checking; otherwise, false. </returns>
		// Token: 0x17000DAD RID: 3501
		// (get) Token: 0x06003C2B RID: 15403 RVA: 0x000CDD6F File Offset: 0x000CBF6F
		// (set) Token: 0x06003C2C RID: 15404 RVA: 0x000CDD81 File Offset: 0x000CBF81
		[ConfigurationProperty("checkCertificateName", DefaultValue = "True")]
		public bool CheckCertificateName
		{
			get
			{
				return (bool)base[ServicePointManagerElement.checkCertificateNameProp];
			}
			set
			{
				base[ServicePointManagerElement.checkCertificateNameProp] = value;
			}
		}

		/// <summary>Gets or sets a Boolean value that indicates whether the certificate is checked against the certificate authority revocation list.</summary>
		/// <returns>true if the certificate revocation list is checked; otherwise, false.The default value is false.</returns>
		// Token: 0x17000DAE RID: 3502
		// (get) Token: 0x06003C2D RID: 15405 RVA: 0x000CDD94 File Offset: 0x000CBF94
		// (set) Token: 0x06003C2E RID: 15406 RVA: 0x000CDDA6 File Offset: 0x000CBFA6
		[ConfigurationProperty("checkCertificateRevocationList", DefaultValue = "False")]
		public bool CheckCertificateRevocationList
		{
			get
			{
				return (bool)base[ServicePointManagerElement.checkCertificateRevocationListProp];
			}
			set
			{
				base[ServicePointManagerElement.checkCertificateRevocationListProp] = value;
			}
		}

		/// <summary>Gets or sets the amount of time after which address information is refreshed.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> that specifies when addresses are resolved using DNS.</returns>
		// Token: 0x17000DAF RID: 3503
		// (get) Token: 0x06003C2F RID: 15407 RVA: 0x000CDDB9 File Offset: 0x000CBFB9
		// (set) Token: 0x06003C30 RID: 15408 RVA: 0x000CDDCB File Offset: 0x000CBFCB
		[ConfigurationProperty("dnsRefreshTimeout", DefaultValue = "120000")]
		public int DnsRefreshTimeout
		{
			get
			{
				return (int)base[ServicePointManagerElement.dnsRefreshTimeoutProp];
			}
			set
			{
				base[ServicePointManagerElement.dnsRefreshTimeoutProp] = value;
			}
		}

		/// <summary>Gets or sets a Boolean value that controls using different IP addresses on connections to the same server.</summary>
		/// <returns>true to enable DNS round-robin behavior; otherwise, false.</returns>
		// Token: 0x17000DB0 RID: 3504
		// (get) Token: 0x06003C31 RID: 15409 RVA: 0x000CDDDE File Offset: 0x000CBFDE
		// (set) Token: 0x06003C32 RID: 15410 RVA: 0x000CDDF0 File Offset: 0x000CBFF0
		[ConfigurationProperty("enableDnsRoundRobin", DefaultValue = "False")]
		public bool EnableDnsRoundRobin
		{
			get
			{
				return (bool)base[ServicePointManagerElement.enableDnsRoundRobinProp];
			}
			set
			{
				base[ServicePointManagerElement.enableDnsRoundRobinProp] = value;
			}
		}

		/// <summary>Gets or sets a Boolean value that determines whether 100-Continue behavior is used.</summary>
		/// <returns>true to expect 100-Continue responses for POST requests; otherwise, false. The default value is true.</returns>
		// Token: 0x17000DB1 RID: 3505
		// (get) Token: 0x06003C33 RID: 15411 RVA: 0x000CDE03 File Offset: 0x000CC003
		// (set) Token: 0x06003C34 RID: 15412 RVA: 0x000CDE15 File Offset: 0x000CC015
		[ConfigurationProperty("expect100Continue", DefaultValue = "True")]
		public bool Expect100Continue
		{
			get
			{
				return (bool)base[ServicePointManagerElement.expect100ContinueProp];
			}
			set
			{
				base[ServicePointManagerElement.expect100ContinueProp] = value;
			}
		}

		/// <summary>Gets or sets a Boolean value that determines whether the Nagle algorithm is used.</summary>
		/// <returns>true to use the Nagle algorithm; otherwise, false. The default value is true.</returns>
		// Token: 0x17000DB2 RID: 3506
		// (get) Token: 0x06003C35 RID: 15413 RVA: 0x000CDE28 File Offset: 0x000CC028
		// (set) Token: 0x06003C36 RID: 15414 RVA: 0x000CDE3A File Offset: 0x000CC03A
		[ConfigurationProperty("useNagleAlgorithm", DefaultValue = "True")]
		public bool UseNagleAlgorithm
		{
			get
			{
				return (bool)base[ServicePointManagerElement.useNagleAlgorithmProp];
			}
			set
			{
				base[ServicePointManagerElement.useNagleAlgorithmProp] = value;
			}
		}

		// Token: 0x17000DB3 RID: 3507
		// (get) Token: 0x06003C37 RID: 15415 RVA: 0x000CDE4D File Offset: 0x000CC04D
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ServicePointManagerElement.properties;
			}
		}

		// Token: 0x06003C38 RID: 15416 RVA: 0x00003917 File Offset: 0x00001B17
		[MonoTODO]
		protected override void PostDeserialize()
		{
		}

		/// <summary>Gets or sets the <see cref="T:System.Net.Security.EncryptionPolicy" /> to use.</summary>
		/// <returns>The encryption policy to use for a <see cref="T:System.Net.ServicePointManager" /> instance.</returns>
		// Token: 0x17000DB4 RID: 3508
		// (get) Token: 0x06003C39 RID: 15417 RVA: 0x000CDE54 File Offset: 0x000CC054
		// (set) Token: 0x06003C3A RID: 15418 RVA: 0x00013BAE File Offset: 0x00011DAE
		public EncryptionPolicy EncryptionPolicy
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return EncryptionPolicy.RequireEncryption;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x040023DA RID: 9178
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x040023DB RID: 9179
		private static ConfigurationProperty checkCertificateNameProp = new ConfigurationProperty("checkCertificateName", typeof(bool), true);

		// Token: 0x040023DC RID: 9180
		private static ConfigurationProperty checkCertificateRevocationListProp = new ConfigurationProperty("checkCertificateRevocationList", typeof(bool), false);

		// Token: 0x040023DD RID: 9181
		private static ConfigurationProperty dnsRefreshTimeoutProp = new ConfigurationProperty("dnsRefreshTimeout", typeof(int), 120000);

		// Token: 0x040023DE RID: 9182
		private static ConfigurationProperty enableDnsRoundRobinProp = new ConfigurationProperty("enableDnsRoundRobin", typeof(bool), false);

		// Token: 0x040023DF RID: 9183
		private static ConfigurationProperty expect100ContinueProp = new ConfigurationProperty("expect100Continue", typeof(bool), true);

		// Token: 0x040023E0 RID: 9184
		private static ConfigurationProperty useNagleAlgorithmProp = new ConfigurationProperty("useNagleAlgorithm", typeof(bool), true);
	}
}
