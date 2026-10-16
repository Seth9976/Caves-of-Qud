using System;
using System.Configuration;
using Unity;

namespace System.Net.Configuration
{
	/// <summary>Represents information used to configure Web proxy scripts. This class cannot be inherited.</summary>
	// Token: 0x02000780 RID: 1920
	public sealed class WebProxyScriptElement : ConfigurationElement
	{
		// Token: 0x06003C72 RID: 15474 RVA: 0x000CE2AC File Offset: 0x000CC4AC
		static WebProxyScriptElement()
		{
			WebProxyScriptElement.properties.Add(WebProxyScriptElement.downloadTimeoutProp);
		}

		// Token: 0x06003C73 RID: 15475 RVA: 0x00003917 File Offset: 0x00001B17
		protected override void PostDeserialize()
		{
		}

		/// <summary>Gets or sets the Web proxy script download timeout using the format hours:minutes:seconds.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> object that contains the timeout value. The default download timeout is one minute.</returns>
		// Token: 0x17000DD4 RID: 3540
		// (get) Token: 0x06003C74 RID: 15476 RVA: 0x000CE2F9 File Offset: 0x000CC4F9
		// (set) Token: 0x06003C75 RID: 15477 RVA: 0x000CE30B File Offset: 0x000CC50B
		[ConfigurationProperty("downloadTimeout", DefaultValue = "00:02:00")]
		public TimeSpan DownloadTimeout
		{
			get
			{
				return (TimeSpan)base[WebProxyScriptElement.downloadTimeoutProp];
			}
			set
			{
				base[WebProxyScriptElement.downloadTimeoutProp] = value;
			}
		}

		// Token: 0x17000DD5 RID: 3541
		// (get) Token: 0x06003C76 RID: 15478 RVA: 0x000CE31E File Offset: 0x000CC51E
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return WebProxyScriptElement.properties;
			}
		}

		// Token: 0x17000DD6 RID: 3542
		// (get) Token: 0x06003C78 RID: 15480 RVA: 0x000CE328 File Offset: 0x000CC528
		// (set) Token: 0x06003C79 RID: 15481 RVA: 0x00013BAE File Offset: 0x00011DAE
		public int AutoConfigUrlRetryInterval
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x040023ED RID: 9197
		private static ConfigurationProperty downloadTimeoutProp = new ConfigurationProperty("downloadTimeout", typeof(TimeSpan), new TimeSpan(0, 0, 2, 0));

		// Token: 0x040023EE RID: 9198
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
