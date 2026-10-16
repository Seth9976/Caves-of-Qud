using System;

namespace System.Net
{
	/// <summary>Provides a container class for Internet host address information.</summary>
	// Token: 0x020005DB RID: 1499
	public class IPHostEntry
	{
		/// <summary>Gets or sets the DNS name of the host.</summary>
		/// <returns>A string that contains the primary host name for the server.</returns>
		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x06003033 RID: 12339 RVA: 0x000A6412 File Offset: 0x000A4612
		// (set) Token: 0x06003034 RID: 12340 RVA: 0x000A641A File Offset: 0x000A461A
		public string HostName
		{
			get
			{
				return this.hostName;
			}
			set
			{
				this.hostName = value;
			}
		}

		/// <summary>Gets or sets a list of aliases that are associated with a host.</summary>
		/// <returns>An array of strings that contain DNS names that resolve to the IP addresses in the <see cref="P:System.Net.IPHostEntry.AddressList" /> property.</returns>
		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x06003035 RID: 12341 RVA: 0x000A6423 File Offset: 0x000A4623
		// (set) Token: 0x06003036 RID: 12342 RVA: 0x000A642B File Offset: 0x000A462B
		public string[] Aliases
		{
			get
			{
				return this.aliases;
			}
			set
			{
				this.aliases = value;
			}
		}

		/// <summary>Gets or sets a list of IP addresses that are associated with a host.</summary>
		/// <returns>An array of type <see cref="T:System.Net.IPAddress" /> that contains IP addresses that resolve to the host names that are contained in the <see cref="P:System.Net.IPHostEntry.Aliases" /> property.</returns>
		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x06003037 RID: 12343 RVA: 0x000A6434 File Offset: 0x000A4634
		// (set) Token: 0x06003038 RID: 12344 RVA: 0x000A643C File Offset: 0x000A463C
		public IPAddress[] AddressList
		{
			get
			{
				return this.addressList;
			}
			set
			{
				this.addressList = value;
			}
		}

		// Token: 0x04001B1B RID: 6939
		private string hostName;

		// Token: 0x04001B1C RID: 6940
		private string[] aliases;

		// Token: 0x04001B1D RID: 6941
		private IPAddress[] addressList;

		// Token: 0x04001B1E RID: 6942
		internal bool isTrustedHost = true;
	}
}
