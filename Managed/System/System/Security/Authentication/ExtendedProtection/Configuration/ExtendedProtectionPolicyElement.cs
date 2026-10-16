using System;
using System.Configuration;

namespace System.Security.Authentication.ExtendedProtection.Configuration
{
	/// <summary>The <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ExtendedProtectionPolicyElement" /> class represents a configuration element for an <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" />.</summary>
	// Token: 0x020002AE RID: 686
	[MonoTODO]
	public sealed class ExtendedProtectionPolicyElement : ConfigurationElement
	{
		// Token: 0x06001549 RID: 5449 RVA: 0x00055B64 File Offset: 0x00053D64
		static ExtendedProtectionPolicyElement()
		{
			Type typeFromHandle = typeof(ExtendedProtectionPolicyElement);
			ExtendedProtectionPolicyElement.custom_service_names = ConfigUtil.BuildProperty(typeFromHandle, "CustomServiceNames");
			ExtendedProtectionPolicyElement.policy_enforcement = ConfigUtil.BuildProperty(typeFromHandle, "PolicyEnforcement");
			ExtendedProtectionPolicyElement.protection_scenario = ConfigUtil.BuildProperty(typeFromHandle, "ProtectionScenario");
			foreach (ConfigurationProperty configurationProperty in new ConfigurationProperty[]
			{
				ExtendedProtectionPolicyElement.custom_service_names,
				ExtendedProtectionPolicyElement.policy_enforcement,
				ExtendedProtectionPolicyElement.protection_scenario
			})
			{
				ExtendedProtectionPolicyElement.properties.Add(configurationProperty);
			}
		}

		/// <summary>Gets or sets the custom Service Provider Name (SPN) list used to match against a client's SPN for this configuration policy element. </summary>
		/// <returns>Returns a <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection" /> that includes the custom SPN list used to match against a client's SPN.</returns>
		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x0600154A RID: 5450 RVA: 0x00055BF0 File Offset: 0x00053DF0
		[ConfigurationProperty("customServiceNames")]
		public ServiceNameElementCollection CustomServiceNames
		{
			get
			{
				return (ServiceNameElementCollection)base[ExtendedProtectionPolicyElement.custom_service_names];
			}
		}

		/// <summary>Gets or sets the policy enforcement value for this configuration policy element.</summary>
		/// <returns>Returns a <see cref="T:System.Security.Authentication.ExtendedProtection.PolicyEnforcement" /> value that indicates when the extended protection policy should be enforced.</returns>
		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x0600154B RID: 5451 RVA: 0x00055C02 File Offset: 0x00053E02
		// (set) Token: 0x0600154C RID: 5452 RVA: 0x00055C14 File Offset: 0x00053E14
		[ConfigurationProperty("policyEnforcement")]
		public PolicyEnforcement PolicyEnforcement
		{
			get
			{
				return (PolicyEnforcement)base[ExtendedProtectionPolicyElement.policy_enforcement];
			}
			set
			{
				base[ExtendedProtectionPolicyElement.policy_enforcement] = value;
			}
		}

		/// <summary>Gets or sets the kind of protection enforced by the extended protection policy for this configuration policy element.</summary>
		/// <returns>A <see cref="T:System.Security.Authentication.ExtendedProtection.ProtectionScenario" /> value that indicates the kind of protection enforced by the policy.</returns>
		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x0600154D RID: 5453 RVA: 0x00055C27 File Offset: 0x00053E27
		// (set) Token: 0x0600154E RID: 5454 RVA: 0x00055C39 File Offset: 0x00053E39
		[ConfigurationProperty("protectionScenario", DefaultValue = ProtectionScenario.TransportSelected)]
		public ProtectionScenario ProtectionScenario
		{
			get
			{
				return (ProtectionScenario)base[ExtendedProtectionPolicyElement.protection_scenario];
			}
			set
			{
				base[ExtendedProtectionPolicyElement.protection_scenario] = value;
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x0600154F RID: 5455 RVA: 0x00055C4C File Offset: 0x00053E4C
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ExtendedProtectionPolicyElement.properties;
			}
		}

		/// <summary>The <see cref="M:System.Security.Authentication.ExtendedProtection.Configuration.ExtendedProtectionPolicyElement.BuildPolicy" /> method builds a new <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> instance based on the properties set on the <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ExtendedProtectionPolicyElement" /> class. </summary>
		/// <returns>A new <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> instance that represents the extended protection policy created.</returns>
		// Token: 0x06001550 RID: 5456 RVA: 0x0000829A File Offset: 0x0000649A
		public ExtendedProtectionPolicy BuildPolicy()
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000C22 RID: 3106
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04000C23 RID: 3107
		private static ConfigurationProperty custom_service_names;

		// Token: 0x04000C24 RID: 3108
		private static ConfigurationProperty policy_enforcement;

		// Token: 0x04000C25 RID: 3109
		private static ConfigurationProperty protection_scenario;
	}
}
