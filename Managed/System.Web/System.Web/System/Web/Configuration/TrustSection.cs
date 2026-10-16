using System;
using System.ComponentModel;
using System.Configuration;
using Unity;

namespace System.Web.Configuration
{
	/// <summary>Configures the code-access security level that is applied to an application. This class cannot be inherited.</summary>
	// Token: 0x020005E2 RID: 1506
	public sealed class TrustSection : ConfigurationSection
	{
		// Token: 0x0600414F RID: 16719 RVA: 0x000A8F94 File Offset: 0x000A7194
		static TrustSection()
		{
			TrustSection.properties.Add(TrustSection.levelProp);
			TrustSection.properties.Add(TrustSection.originUrlProp);
			TrustSection.properties.Add(TrustSection.processRequestInApplicationTrustProp);
		}

		/// <summary>Gets or sets the name of the security level under which the application will run. </summary>
		/// <returns>The name of the trust level. The default is "Full".</returns>
		// Token: 0x170014DC RID: 5340
		// (get) Token: 0x06004150 RID: 16720 RVA: 0x000A9048 File Offset: 0x000A7248
		// (set) Token: 0x06004151 RID: 16721 RVA: 0x000A905A File Offset: 0x000A725A
		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("level", DefaultValue = "Full", Options = ConfigurationPropertyOptions.IsRequired)]
		public string Level
		{
			get
			{
				return (string)base[TrustSection.levelProp];
			}
			set
			{
				base[TrustSection.levelProp] = value;
			}
		}

		/// <summary>Specifies the URL of origin for an application.</summary>
		/// <returns>A well-formed HTTP URL or an empty string (""). The default is an empty string.</returns>
		// Token: 0x170014DD RID: 5341
		// (get) Token: 0x06004152 RID: 16722 RVA: 0x000A9068 File Offset: 0x000A7268
		// (set) Token: 0x06004153 RID: 16723 RVA: 0x000A907A File Offset: 0x000A727A
		[ConfigurationProperty("originUrl", DefaultValue = "")]
		public string OriginUrl
		{
			get
			{
				return (string)base[TrustSection.originUrlProp];
			}
			set
			{
				base[TrustSection.originUrlProp] = value;
			}
		}

		/// <summary>Gets or set a value that indicates whether page requests are automatically restricted to the permissions that are configured in the trust policy file that is applied to the ASP.NET application.</summary>
		/// <returns>true if requests are automatically restricted to the permissions that are configured in the trust policy file; otherwise, false.</returns>
		// Token: 0x170014DE RID: 5342
		// (get) Token: 0x06004154 RID: 16724 RVA: 0x000A9088 File Offset: 0x000A7288
		// (set) Token: 0x06004155 RID: 16725 RVA: 0x000A909A File Offset: 0x000A729A
		[ConfigurationProperty("processRequestInApplicationTrust", DefaultValue = "True")]
		public bool ProcessRequestInApplicationTrust
		{
			get
			{
				return (bool)base[TrustSection.processRequestInApplicationTrustProp];
			}
			set
			{
				base[TrustSection.processRequestInApplicationTrustProp] = value;
			}
		}

		// Token: 0x170014DF RID: 5343
		// (get) Token: 0x06004156 RID: 16726 RVA: 0x000A90AD File Offset: 0x000A72AD
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return TrustSection.properties;
			}
		}

		/// <summary>Gets or sets the custom security-policy resolution type.</summary>
		/// <returns>The custom security-policy resolution type.</returns>
		// Token: 0x170014E0 RID: 5344
		// (get) Token: 0x06004158 RID: 16728 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004159 RID: 16729 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public string HostSecurityPolicyResolverType
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or set a value that indicates whether the legacy code access security is enabled.</summary>
		/// <returns>true if legacy code access security is enabled; otherwise, false. The default is false.</returns>
		// Token: 0x170014E1 RID: 5345
		// (get) Token: 0x0600415A RID: 16730 RVA: 0x000A90B4 File Offset: 0x000A72B4
		// (set) Token: 0x0600415B RID: 16731 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public bool LegacyCasModel
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the name of the permission set. </summary>
		/// <returns>The name of the permission set.</returns>
		// Token: 0x170014E2 RID: 5346
		// (get) Token: 0x0600415C RID: 16732 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x0600415D RID: 16733 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public string PermissionSetName
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x04002331 RID: 9009
		private static ConfigurationProperty levelProp = new ConfigurationProperty("level", typeof(string), "Full", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x04002332 RID: 9010
		private static ConfigurationProperty originUrlProp = new ConfigurationProperty("originUrl", typeof(string), "");

		// Token: 0x04002333 RID: 9011
		private static ConfigurationProperty processRequestInApplicationTrustProp = new ConfigurationProperty("processRequestInApplicationTrust", typeof(bool), true);

		// Token: 0x04002334 RID: 9012
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
