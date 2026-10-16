using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides information about the Transmission Control Protocol (TCP) connections on the local computer.</summary>
	// Token: 0x02000708 RID: 1800
	public abstract class TcpConnectionInformation
	{
		/// <summary>Gets the local endpoint of a Transmission Control Protocol (TCP) connection.</summary>
		/// <returns>An <see cref="T:System.Net.IPEndPoint" /> instance that contains the IP address and port on the local computer.</returns>
		// Token: 0x17000C8B RID: 3211
		// (get) Token: 0x060039A0 RID: 14752
		public abstract IPEndPoint LocalEndPoint { get; }

		/// <summary>Gets the remote endpoint of a Transmission Control Protocol (TCP) connection.</summary>
		/// <returns>An <see cref="T:System.Net.IPEndPoint" /> instance that contains the IP address and port on the remote computer.</returns>
		// Token: 0x17000C8C RID: 3212
		// (get) Token: 0x060039A1 RID: 14753
		public abstract IPEndPoint RemoteEndPoint { get; }

		/// <summary>Gets the state of this Transmission Control Protocol (TCP) connection.</summary>
		/// <returns>One of the <see cref="T:System.Net.NetworkInformation.TcpState" /> enumeration values.</returns>
		// Token: 0x17000C8D RID: 3213
		// (get) Token: 0x060039A2 RID: 14754
		public abstract TcpState State { get; }
	}
}
