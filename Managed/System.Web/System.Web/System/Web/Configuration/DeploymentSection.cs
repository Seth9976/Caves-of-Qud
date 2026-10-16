using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Defines a configuration setting that is typically used on a production server to override application-level settings that are appropriate only on development computers. </summary>
	// Token: 0x02000594 RID: 1428
	public sealed class DeploymentSection : ConfigurationSection
	{
		// Token: 0x06003C7E RID: 15486 RVA: 0x0009EEDB File Offset: 0x0009D0DB
		static DeploymentSection()
		{
			DeploymentSection.properties.Add(DeploymentSection.retailProp);
		}

		/// <summary>Gets or sets a value that specifies whether Web applications on the computer are deployed in retail mode.</summary>
		/// <returns>true if Web applications are deployed in retail mode; otherwise, false. The default is false.</returns>
		// Token: 0x170012B5 RID: 4789
		// (get) Token: 0x06003C7F RID: 15487 RVA: 0x0009EF15 File Offset: 0x0009D115
		// (set) Token: 0x06003C80 RID: 15488 RVA: 0x0009EF27 File Offset: 0x0009D127
		[ConfigurationProperty("retail", DefaultValue = "False")]
		public bool Retail
		{
			get
			{
				return (bool)base[DeploymentSection.retailProp];
			}
			set
			{
				base[DeploymentSection.retailProp] = value;
			}
		}

		// Token: 0x170012B6 RID: 4790
		// (get) Token: 0x06003C81 RID: 15489 RVA: 0x0009EF3A File Offset: 0x0009D13A
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return DeploymentSection.properties;
			}
		}

		// Token: 0x040020D0 RID: 8400
		private static ConfigurationProperty retailProp = new ConfigurationProperty("retail", typeof(bool), false);

		// Token: 0x040020D1 RID: 8401
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
