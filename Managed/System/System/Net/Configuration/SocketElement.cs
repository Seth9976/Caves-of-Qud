using System;
using System.Configuration;
using System.Net.Sockets;
using Unity;

namespace System.Net.Configuration
{
	/// <summary>Represents information used to configure <see cref="T:System.Net.Sockets.Socket" /> objects. This class cannot be inherited.</summary>
	// Token: 0x0200077F RID: 1919
	public sealed class SocketElement : ConfigurationElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.SocketElement" /> class. </summary>
		// Token: 0x06003C69 RID: 15465 RVA: 0x000CE1C4 File Offset: 0x000CC3C4
		public SocketElement()
		{
			SocketElement.alwaysUseCompletionPortsForAcceptProp = new ConfigurationProperty("alwaysUseCompletionPortsForAccept", typeof(bool), false);
			SocketElement.alwaysUseCompletionPortsForConnectProp = new ConfigurationProperty("alwaysUseCompletionPortsForConnect", typeof(bool), false);
			SocketElement.properties = new ConfigurationPropertyCollection();
			SocketElement.properties.Add(SocketElement.alwaysUseCompletionPortsForAcceptProp);
			SocketElement.properties.Add(SocketElement.alwaysUseCompletionPortsForConnectProp);
		}

		/// <summary>Gets or sets a Boolean value that specifies whether completion ports are used when accepting connections.</summary>
		/// <returns>true to use completion ports; otherwise, false.</returns>
		// Token: 0x17000DD0 RID: 3536
		// (get) Token: 0x06003C6A RID: 15466 RVA: 0x000CE23D File Offset: 0x000CC43D
		// (set) Token: 0x06003C6B RID: 15467 RVA: 0x000CE24F File Offset: 0x000CC44F
		[ConfigurationProperty("alwaysUseCompletionPortsForAccept", DefaultValue = "False")]
		public bool AlwaysUseCompletionPortsForAccept
		{
			get
			{
				return (bool)base[SocketElement.alwaysUseCompletionPortsForAcceptProp];
			}
			set
			{
				base[SocketElement.alwaysUseCompletionPortsForAcceptProp] = value;
			}
		}

		/// <summary>Gets or sets a Boolean value that specifies whether completion ports are used when making connections.</summary>
		/// <returns>true to use completion ports; otherwise, false.</returns>
		// Token: 0x17000DD1 RID: 3537
		// (get) Token: 0x06003C6C RID: 15468 RVA: 0x000CE262 File Offset: 0x000CC462
		// (set) Token: 0x06003C6D RID: 15469 RVA: 0x000CE274 File Offset: 0x000CC474
		[ConfigurationProperty("alwaysUseCompletionPortsForConnect", DefaultValue = "False")]
		public bool AlwaysUseCompletionPortsForConnect
		{
			get
			{
				return (bool)base[SocketElement.alwaysUseCompletionPortsForConnectProp];
			}
			set
			{
				base[SocketElement.alwaysUseCompletionPortsForConnectProp] = value;
			}
		}

		// Token: 0x17000DD2 RID: 3538
		// (get) Token: 0x06003C6E RID: 15470 RVA: 0x000CE287 File Offset: 0x000CC487
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SocketElement.properties;
			}
		}

		// Token: 0x06003C6F RID: 15471 RVA: 0x00003917 File Offset: 0x00001B17
		[MonoTODO]
		protected override void PostDeserialize()
		{
		}

		/// <summary>Gets or sets a value that specifies the default <see cref="T:System.Net.Sockets.IPProtectionLevel" /> to use for a socket.</summary>
		/// <returns>The value of the <see cref="T:System.Net.Sockets.IPProtectionLevel" /> for the current instance.</returns>
		// Token: 0x17000DD3 RID: 3539
		// (get) Token: 0x06003C70 RID: 15472 RVA: 0x000CE290 File Offset: 0x000CC490
		// (set) Token: 0x06003C71 RID: 15473 RVA: 0x00013BAE File Offset: 0x00011DAE
		public IPProtectionLevel IPProtectionLevel
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return (IPProtectionLevel)0;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x040023EA RID: 9194
		private static ConfigurationPropertyCollection properties;

		// Token: 0x040023EB RID: 9195
		private static ConfigurationProperty alwaysUseCompletionPortsForAcceptProp;

		// Token: 0x040023EC RID: 9196
		private static ConfigurationProperty alwaysUseCompletionPortsForConnectProp;
	}
}
