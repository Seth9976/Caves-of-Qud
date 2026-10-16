using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Represents the IP address of the network gateway. This class cannot be instantiated.</summary>
	// Token: 0x020006E0 RID: 1760
	public abstract class GatewayIPAddressInformation
	{
		/// <summary>Get the IP address of the gateway.</summary>
		/// <returns>An <see cref="T:System.Net.IPAddress" /> object that contains the IP address of the gateway.</returns>
		// Token: 0x17000BD0 RID: 3024
		// (get) Token: 0x06003866 RID: 14438
		public abstract IPAddress Address { get; }
	}
}
