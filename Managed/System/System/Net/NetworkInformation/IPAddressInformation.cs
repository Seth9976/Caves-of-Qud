using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides information about a network interface address.</summary>
	// Token: 0x020006E3 RID: 1763
	public abstract class IPAddressInformation
	{
		/// <summary>Gets the Internet Protocol (IP) address.</summary>
		/// <returns>An <see cref="T:System.Net.IPAddress" /> instance that contains the IP address of an interface.</returns>
		// Token: 0x17000BD7 RID: 3031
		// (get) Token: 0x06003880 RID: 14464
		public abstract IPAddress Address { get; }

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the Internet Protocol (IP) address is valid to appear in a Domain Name System (DNS) server database.</summary>
		/// <returns>true if the address can appear in a DNS database; otherwise, false.</returns>
		// Token: 0x17000BD8 RID: 3032
		// (get) Token: 0x06003881 RID: 14465
		public abstract bool IsDnsEligible { get; }

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the Internet Protocol (IP) address is transient (a cluster address).</summary>
		/// <returns>true if the address is transient; otherwise, false.</returns>
		// Token: 0x17000BD9 RID: 3033
		// (get) Token: 0x06003882 RID: 14466
		public abstract bool IsTransient { get; }
	}
}
