using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Defines configuration settings that are used to support the security infrastructure of a Web application. This class cannot be inherited.</summary>
	// Token: 0x020005D1 RID: 1489
	public sealed class SecurityPolicySection : ConfigurationSection
	{
		// Token: 0x06004051 RID: 16465 RVA: 0x000A75C5 File Offset: 0x000A57C5
		static SecurityPolicySection()
		{
			SecurityPolicySection.properties.Add(SecurityPolicySection.Prop);
		}

		/// <summary>Gets the <see cref="P:System.Web.Configuration.SecurityPolicySection.TrustLevels" /> collection.</summary>
		/// <returns>A collection of <see cref="P:System.Web.Configuration.SecurityPolicySection.TrustLevels" /> objects. </returns>
		// Token: 0x17001465 RID: 5221
		// (get) Token: 0x06004052 RID: 16466 RVA: 0x000A75FD File Offset: 0x000A57FD
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public TrustLevelCollection TrustLevels
		{
			get
			{
				return (TrustLevelCollection)base[SecurityPolicySection.Prop];
			}
		}

		// Token: 0x17001466 RID: 5222
		// (get) Token: 0x06004053 RID: 16467 RVA: 0x000A760F File Offset: 0x000A580F
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SecurityPolicySection.properties;
			}
		}

		// Token: 0x040022ED RID: 8941
		private static ConfigurationProperty Prop = new ConfigurationProperty("", typeof(TrustLevelCollection), null, null, null, ConfigurationPropertyOptions.IsDefaultCollection);

		// Token: 0x040022EE RID: 8942
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
