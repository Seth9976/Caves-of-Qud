using System;

namespace System.Net.Sockets
{
	/// <summary>Contains <see cref="T:System.Net.IPAddress" /> values used to join and drop multicast groups.</summary>
	// Token: 0x020007AD RID: 1965
	public class MulticastOption
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.MulticastOption" /> class with the specified IP multicast group address and local IP address associated with a network interface.</summary>
		/// <param name="group">The group <see cref="T:System.Net.IPAddress" />. </param>
		/// <param name="mcint">The local <see cref="T:System.Net.IPAddress" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="group" /> is null.-or- <paramref name="mcint" /> is null. </exception>
		// Token: 0x06003EB3 RID: 16051 RVA: 0x000D67F4 File Offset: 0x000D49F4
		public MulticastOption(IPAddress group, IPAddress mcint)
		{
			if (group == null)
			{
				throw new ArgumentNullException("group");
			}
			if (mcint == null)
			{
				throw new ArgumentNullException("mcint");
			}
			this.Group = group;
			this.LocalAddress = mcint;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.MulticastOption" /> class with the specified IP multicast group address and interface index.</summary>
		/// <param name="group">The <see cref="T:System.Net.IPAddress" /> of the multicast group.</param>
		/// <param name="interfaceIndex">The index of the interface that is used to send and receive multicast packets.</param>
		// Token: 0x06003EB4 RID: 16052 RVA: 0x000D6826 File Offset: 0x000D4A26
		public MulticastOption(IPAddress group, int interfaceIndex)
		{
			if (group == null)
			{
				throw new ArgumentNullException("group");
			}
			if (interfaceIndex < 0 || interfaceIndex > 16777215)
			{
				throw new ArgumentOutOfRangeException("interfaceIndex");
			}
			this.Group = group;
			this.ifIndex = interfaceIndex;
		}

		/// <summary>Initializes a new version of the <see cref="T:System.Net.Sockets.MulticastOption" /> class for the specified IP multicast group.</summary>
		/// <param name="group">The <see cref="T:System.Net.IPAddress" /> of the multicast group. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="group" /> is null. </exception>
		// Token: 0x06003EB5 RID: 16053 RVA: 0x000D6861 File Offset: 0x000D4A61
		public MulticastOption(IPAddress group)
		{
			if (group == null)
			{
				throw new ArgumentNullException("group");
			}
			this.Group = group;
			this.LocalAddress = IPAddress.Any;
		}

		/// <summary>Gets or sets the IP address of a multicast group.</summary>
		/// <returns>An <see cref="T:System.Net.IPAddress" /> that contains the Internet address of a multicast group.</returns>
		// Token: 0x17000E33 RID: 3635
		// (get) Token: 0x06003EB6 RID: 16054 RVA: 0x000D6889 File Offset: 0x000D4A89
		// (set) Token: 0x06003EB7 RID: 16055 RVA: 0x000D6891 File Offset: 0x000D4A91
		public IPAddress Group
		{
			get
			{
				return this.group;
			}
			set
			{
				this.group = value;
			}
		}

		/// <summary>Gets or sets the local address associated with a multicast group.</summary>
		/// <returns>An <see cref="T:System.Net.IPAddress" /> that contains the local address associated with a multicast group.</returns>
		// Token: 0x17000E34 RID: 3636
		// (get) Token: 0x06003EB8 RID: 16056 RVA: 0x000D689A File Offset: 0x000D4A9A
		// (set) Token: 0x06003EB9 RID: 16057 RVA: 0x000D68A2 File Offset: 0x000D4AA2
		public IPAddress LocalAddress
		{
			get
			{
				return this.localAddress;
			}
			set
			{
				this.ifIndex = 0;
				this.localAddress = value;
			}
		}

		/// <summary>Gets or sets the index of the interface that is used to send and receive multicast packets. </summary>
		/// <returns>An integer that represents the index of a <see cref="T:System.Net.NetworkInformation.NetworkInterface" /> array element.</returns>
		// Token: 0x17000E35 RID: 3637
		// (get) Token: 0x06003EBA RID: 16058 RVA: 0x000D68B2 File Offset: 0x000D4AB2
		// (set) Token: 0x06003EBB RID: 16059 RVA: 0x000D68BA File Offset: 0x000D4ABA
		public int InterfaceIndex
		{
			get
			{
				return this.ifIndex;
			}
			set
			{
				if (value < 0 || value > 16777215)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.localAddress = null;
				this.ifIndex = value;
			}
		}

		// Token: 0x040024FF RID: 9471
		private IPAddress group;

		// Token: 0x04002500 RID: 9472
		private IPAddress localAddress;

		// Token: 0x04002501 RID: 9473
		private int ifIndex;
	}
}
