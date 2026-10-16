using System;
using System.Configuration;
using System.Net.Security;
using System.Net.Sockets;

namespace System.Net.Configuration
{
	// Token: 0x0200075D RID: 1885
	internal sealed class SettingsSectionInternal
	{
		// Token: 0x17000D64 RID: 3428
		// (get) Token: 0x06003B5A RID: 15194 RVA: 0x000CC2D8 File Offset: 0x000CA4D8
		internal static SettingsSectionInternal Section
		{
			get
			{
				return SettingsSectionInternal.instance;
			}
		}

		// Token: 0x17000D65 RID: 3429
		// (get) Token: 0x06003B5B RID: 15195 RVA: 0x000CC2DF File Offset: 0x000CA4DF
		// (set) Token: 0x06003B5C RID: 15196 RVA: 0x000CC2E7 File Offset: 0x000CA4E7
		internal bool UseNagleAlgorithm { get; set; }

		// Token: 0x17000D66 RID: 3430
		// (get) Token: 0x06003B5D RID: 15197 RVA: 0x000CC2F0 File Offset: 0x000CA4F0
		// (set) Token: 0x06003B5E RID: 15198 RVA: 0x000CC2F8 File Offset: 0x000CA4F8
		internal bool Expect100Continue { get; set; }

		// Token: 0x17000D67 RID: 3431
		// (get) Token: 0x06003B5F RID: 15199 RVA: 0x000CC301 File Offset: 0x000CA501
		// (set) Token: 0x06003B60 RID: 15200 RVA: 0x000CC309 File Offset: 0x000CA509
		internal bool CheckCertificateName { get; private set; }

		// Token: 0x17000D68 RID: 3432
		// (get) Token: 0x06003B61 RID: 15201 RVA: 0x000CC312 File Offset: 0x000CA512
		// (set) Token: 0x06003B62 RID: 15202 RVA: 0x000CC31A File Offset: 0x000CA51A
		internal int DnsRefreshTimeout { get; set; }

		// Token: 0x17000D69 RID: 3433
		// (get) Token: 0x06003B63 RID: 15203 RVA: 0x000CC323 File Offset: 0x000CA523
		// (set) Token: 0x06003B64 RID: 15204 RVA: 0x000CC32B File Offset: 0x000CA52B
		internal bool EnableDnsRoundRobin { get; set; }

		// Token: 0x17000D6A RID: 3434
		// (get) Token: 0x06003B65 RID: 15205 RVA: 0x000CC334 File Offset: 0x000CA534
		// (set) Token: 0x06003B66 RID: 15206 RVA: 0x000CC33C File Offset: 0x000CA53C
		internal bool CheckCertificateRevocationList { get; set; }

		// Token: 0x17000D6B RID: 3435
		// (get) Token: 0x06003B67 RID: 15207 RVA: 0x000CC345 File Offset: 0x000CA545
		// (set) Token: 0x06003B68 RID: 15208 RVA: 0x000CC34D File Offset: 0x000CA54D
		internal EncryptionPolicy EncryptionPolicy { get; private set; }

		// Token: 0x17000D6C RID: 3436
		// (get) Token: 0x06003B69 RID: 15209 RVA: 0x000CC358 File Offset: 0x000CA558
		internal bool Ipv6Enabled
		{
			get
			{
				try
				{
					SettingsSection settingsSection = (SettingsSection)ConfigurationManager.GetSection("system.net/settings");
					if (settingsSection != null)
					{
						return settingsSection.Ipv6.Enabled;
					}
				}
				catch
				{
				}
				return true;
			}
		}

		// Token: 0x04002390 RID: 9104
		private static readonly SettingsSectionInternal instance = new SettingsSectionInternal();

		// Token: 0x04002391 RID: 9105
		internal UnicodeEncodingConformance WebUtilityUnicodeEncodingConformance;

		// Token: 0x04002392 RID: 9106
		internal UnicodeDecodingConformance WebUtilityUnicodeDecodingConformance;

		// Token: 0x04002393 RID: 9107
		internal readonly bool HttpListenerUnescapeRequestUrl = true;

		// Token: 0x04002394 RID: 9108
		internal readonly IPProtectionLevel IPProtectionLevel = IPProtectionLevel.Unspecified;
	}
}
