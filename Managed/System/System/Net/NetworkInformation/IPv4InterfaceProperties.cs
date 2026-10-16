using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides information about network interfaces that support Internet Protocol version 4 (IPv4).</summary>
	// Token: 0x020006ED RID: 1773
	public abstract class IPv4InterfaceProperties
	{
		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether an interface uses Windows Internet Name Service (WINS).</summary>
		/// <returns>true if the interface uses WINS; otherwise, false.</returns>
		// Token: 0x17000C1A RID: 3098
		// (get) Token: 0x060038E5 RID: 14565
		public abstract bool UsesWins { get; }

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the interface is configured to use a Dynamic Host Configuration Protocol (DHCP) server to obtain an IP address.</summary>
		/// <returns>true if the interface is configured to obtain an IP address from a DHCP server; otherwise, false.</returns>
		// Token: 0x17000C1B RID: 3099
		// (get) Token: 0x060038E6 RID: 14566
		public abstract bool IsDhcpEnabled { get; }

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether this interface has an automatic private IP addressing (APIPA) address.</summary>
		/// <returns>true if the interface uses an APIPA address; otherwise, false.</returns>
		// Token: 0x17000C1C RID: 3100
		// (get) Token: 0x060038E7 RID: 14567
		public abstract bool IsAutomaticPrivateAddressingActive { get; }

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether this interface has automatic private IP addressing (APIPA) enabled.</summary>
		/// <returns>true if the interface uses APIPA; otherwise, false.</returns>
		// Token: 0x17000C1D RID: 3101
		// (get) Token: 0x060038E8 RID: 14568
		public abstract bool IsAutomaticPrivateAddressingEnabled { get; }

		/// <summary>Gets the index of the network interface associated with the Internet Protocol version 4 (IPv4) address.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains the index of the IPv4 interface.</returns>
		// Token: 0x17000C1E RID: 3102
		// (get) Token: 0x060038E9 RID: 14569
		public abstract int Index { get; }

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether this interface can forward (route) packets.</summary>
		/// <returns>true if this interface routes packets; otherwise false.</returns>
		// Token: 0x17000C1F RID: 3103
		// (get) Token: 0x060038EA RID: 14570
		public abstract bool IsForwardingEnabled { get; }

		/// <summary>Gets the maximum transmission unit (MTU) for this network interface.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the MTU.</returns>
		// Token: 0x17000C20 RID: 3104
		// (get) Token: 0x060038EB RID: 14571
		public abstract int Mtu { get; }
	}
}
