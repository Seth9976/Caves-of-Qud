using System;
using System.Configuration;
using Unity;

namespace System.Net.Configuration
{
	/// <summary>Represents the network element in the SMTP configuration file. This class cannot be inherited.</summary>
	// Token: 0x0200077C RID: 1916
	public sealed class SmtpNetworkElement : ConfigurationElement
	{
		/// <summary>Determines whether or not default user credentials are used to access an SMTP server. The default value is false.</summary>
		/// <returns>true indicates that default user credentials will be used to access the SMTP server; otherwise, false.</returns>
		// Token: 0x17000DBF RID: 3519
		// (get) Token: 0x06003C47 RID: 15431 RVA: 0x000CDFEA File Offset: 0x000CC1EA
		// (set) Token: 0x06003C48 RID: 15432 RVA: 0x000CDFFC File Offset: 0x000CC1FC
		[ConfigurationProperty("defaultCredentials", DefaultValue = "False")]
		public bool DefaultCredentials
		{
			get
			{
				return (bool)base["defaultCredentials"];
			}
			set
			{
				base["defaultCredentials"] = value;
			}
		}

		/// <summary>Gets or sets the name of the SMTP server.</summary>
		/// <returns>A string that represents the name of the SMTP server to connect to.</returns>
		// Token: 0x17000DC0 RID: 3520
		// (get) Token: 0x06003C49 RID: 15433 RVA: 0x000CE00F File Offset: 0x000CC20F
		// (set) Token: 0x06003C4A RID: 15434 RVA: 0x000CE021 File Offset: 0x000CC221
		[ConfigurationProperty("host")]
		public string Host
		{
			get
			{
				return (string)base["host"];
			}
			set
			{
				base["host"] = value;
			}
		}

		/// <summary>Gets or sets the user password to use to connect to an SMTP mail server.</summary>
		/// <returns>A string that represents the password to use to connect to an SMTP mail server.</returns>
		// Token: 0x17000DC1 RID: 3521
		// (get) Token: 0x06003C4B RID: 15435 RVA: 0x000CE02F File Offset: 0x000CC22F
		// (set) Token: 0x06003C4C RID: 15436 RVA: 0x000CE041 File Offset: 0x000CC241
		[ConfigurationProperty("password")]
		public string Password
		{
			get
			{
				return (string)base["password"];
			}
			set
			{
				base["password"] = value;
			}
		}

		/// <summary>Gets or sets the port that SMTP clients use to connect to an SMTP mail server. The default value is 25.</summary>
		/// <returns>A string that represents the port to connect to an SMTP mail server.</returns>
		// Token: 0x17000DC2 RID: 3522
		// (get) Token: 0x06003C4D RID: 15437 RVA: 0x000CE04F File Offset: 0x000CC24F
		// (set) Token: 0x06003C4E RID: 15438 RVA: 0x000CE061 File Offset: 0x000CC261
		[ConfigurationProperty("port", DefaultValue = "25")]
		public int Port
		{
			get
			{
				return (int)base["port"];
			}
			set
			{
				base["port"] = value;
			}
		}

		/// <summary>Gets or sets the user name to connect to an SMTP mail server.</summary>
		/// <returns>A string that represents the user name to connect to an SMTP mail server.</returns>
		// Token: 0x17000DC3 RID: 3523
		// (get) Token: 0x06003C4F RID: 15439 RVA: 0x000CE074 File Offset: 0x000CC274
		// (set) Token: 0x06003C50 RID: 15440 RVA: 0x000CE086 File Offset: 0x000CC286
		[ConfigurationProperty("userName", DefaultValue = null)]
		public string UserName
		{
			get
			{
				return (string)base["userName"];
			}
			set
			{
				base["userName"] = value;
			}
		}

		/// <summary>Gets or sets the Service Provider Name (SPN) to use for authentication when using extended protection to connect to an SMTP mail server.</summary>
		/// <returns>A string that represents the SPN to use for authentication when using extended protection to connect to an SMTP mail server.</returns>
		// Token: 0x17000DC4 RID: 3524
		// (get) Token: 0x06003C51 RID: 15441 RVA: 0x000CE094 File Offset: 0x000CC294
		// (set) Token: 0x06003C52 RID: 15442 RVA: 0x000CE0A6 File Offset: 0x000CC2A6
		[ConfigurationProperty("targetName", DefaultValue = null)]
		public string TargetName
		{
			get
			{
				return (string)base["targetName"];
			}
			set
			{
				base["targetName"] = value;
			}
		}

		/// <summary>Gets or sets whether SSL is used to access an SMTP mail server. The default value is false.</summary>
		/// <returns>true indicates that SSL will be used to access the SMTP mail server; otherwise, false.</returns>
		// Token: 0x17000DC5 RID: 3525
		// (get) Token: 0x06003C53 RID: 15443 RVA: 0x000CE0B4 File Offset: 0x000CC2B4
		// (set) Token: 0x06003C54 RID: 15444 RVA: 0x000CE0C6 File Offset: 0x000CC2C6
		[ConfigurationProperty("enableSsl", DefaultValue = false)]
		public bool EnableSsl
		{
			get
			{
				return (bool)base["enableSsl"];
			}
			set
			{
				base["enableSsl"] = value;
			}
		}

		// Token: 0x17000DC6 RID: 3526
		// (get) Token: 0x06003C55 RID: 15445 RVA: 0x0003176B File Offset: 0x0002F96B
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return base.Properties;
			}
		}

		// Token: 0x06003C56 RID: 15446 RVA: 0x00003917 File Offset: 0x00001B17
		protected override void PostDeserialize()
		{
		}

		/// <summary>Gets or sets the client domain name used in the initial SMTP protocol request to connect to an SMTP mail server.</summary>
		/// <returns>A string that represents the client domain name used in the initial SMTP protocol request to connect to an SMTP mail server.</returns>
		// Token: 0x17000DC7 RID: 3527
		// (get) Token: 0x06003C58 RID: 15448 RVA: 0x00032868 File Offset: 0x00030A68
		// (set) Token: 0x06003C59 RID: 15449 RVA: 0x00013BAE File Offset: 0x00011DAE
		public string ClientDomain
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
	}
}
