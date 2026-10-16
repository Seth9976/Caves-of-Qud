using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Defines the mapping of specific security levels to named policy files. This class cannot be inherited.</summary>
	// Token: 0x020005E0 RID: 1504
	public sealed class TrustLevel : ConfigurationElement
	{
		// Token: 0x06004135 RID: 16693 RVA: 0x000A8E40 File Offset: 0x000A7040
		static TrustLevel()
		{
			TrustLevel.properties.Add(TrustLevel.nameProp);
			TrustLevel.properties.Add(TrustLevel.policyFileProp);
		}

		// Token: 0x06004136 RID: 16694 RVA: 0x0009CC6D File Offset: 0x0009AE6D
		internal TrustLevel()
		{
		}

		/// <summary>Creates an instance of the <see cref="T:System.Web.Configuration.TrustLevel" /> class that is initialized based on the provided values, which define the mapping of specific security levels to named policy files.</summary>
		/// <param name="name">A named security level that is mapped to a policy file.</param>
		/// <param name="policyFile">The configuration file that contains security policy settings for the named security level.</param>
		// Token: 0x06004137 RID: 16695 RVA: 0x000A8EC7 File Offset: 0x000A70C7
		public TrustLevel(string name, string policyFile)
		{
			this.Name = name;
			this.PolicyFile = policyFile;
		}

		/// <summary>Gets or sets a named security level that is mapped to a policy file.</summary>
		/// <returns>The <see cref="P:System.Web.Configuration.TrustLevel.Name" /> that is mapped to a policy file.</returns>
		// Token: 0x170014D3 RID: 5331
		// (get) Token: 0x06004138 RID: 16696 RVA: 0x000A8EDD File Offset: 0x000A70DD
		// (set) Token: 0x06004139 RID: 16697 RVA: 0x000A8EEF File Offset: 0x000A70EF
		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("name", DefaultValue = "Full", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public string Name
		{
			get
			{
				return (string)base[TrustLevel.nameProp];
			}
			set
			{
				base[TrustLevel.nameProp] = value;
			}
		}

		/// <summary>Gets or sets the configuration file reference that contains the security policy settings for the named security level.</summary>
		/// <returns>The configuration file reference that contains the security policy settings for the associated security level.</returns>
		// Token: 0x170014D4 RID: 5332
		// (get) Token: 0x0600413A RID: 16698 RVA: 0x000A8EFD File Offset: 0x000A70FD
		// (set) Token: 0x0600413B RID: 16699 RVA: 0x000A8F0F File Offset: 0x000A710F
		[ConfigurationProperty("policyFile", DefaultValue = "internal", Options = ConfigurationPropertyOptions.IsRequired)]
		public string PolicyFile
		{
			get
			{
				return (string)base[TrustLevel.policyFileProp];
			}
			set
			{
				base[TrustLevel.policyFileProp] = value;
			}
		}

		// Token: 0x170014D5 RID: 5333
		// (get) Token: 0x0600413C RID: 16700 RVA: 0x000A8F1D File Offset: 0x000A711D
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return TrustLevel.properties;
			}
		}

		// Token: 0x0400232D RID: 9005
		private static ConfigurationProperty nameProp = new ConfigurationProperty("name", typeof(string), "Full", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x0400232E RID: 9006
		private static ConfigurationProperty policyFileProp = new ConfigurationProperty("policyFile", typeof(string), "internal", ConfigurationPropertyOptions.IsRequired);

		// Token: 0x0400232F RID: 9007
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
