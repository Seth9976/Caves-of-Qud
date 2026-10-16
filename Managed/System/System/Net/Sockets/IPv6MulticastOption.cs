using System;

namespace System.Net.Sockets
{
	/// <summary>Contains option values for joining an IPv6 multicast group.</summary>
	// Token: 0x020007AE RID: 1966
	public class IPv6MulticastOption
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.IPv6MulticastOption" /> class with the specified IP multicast group and the local interface address.</summary>
		/// <param name="group">The group <see cref="T:System.Net.IPAddress" />. </param>
		/// <param name="ifindex">The local interface address. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="ifindex" /> is less than 0.-or- <paramref name="ifindex" /> is greater than 0x00000000FFFFFFFF. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="group" /> is null. </exception>
		// Token: 0x06003EBC RID: 16060 RVA: 0x000D68E1 File Offset: 0x000D4AE1
		public IPv6MulticastOption(IPAddress group, long ifindex)
		{
			if (group == null)
			{
				throw new ArgumentNullException("group");
			}
			if (ifindex < 0L || ifindex > (long)((ulong)(-1)))
			{
				throw new ArgumentOutOfRangeException("ifindex");
			}
			this.Group = group;
			this.InterfaceIndex = ifindex;
		}

		/// <summary>Initializes a new version of the <see cref="T:System.Net.Sockets.IPv6MulticastOption" /> class for the specified IP multicast group.</summary>
		/// <param name="group">The <see cref="T:System.Net.IPAddress" /> of the multicast group. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="group" /> is null. </exception>
		// Token: 0x06003EBD RID: 16061 RVA: 0x000D691A File Offset: 0x000D4B1A
		public IPv6MulticastOption(IPAddress group)
		{
			if (group == null)
			{
				throw new ArgumentNullException("group");
			}
			this.Group = group;
			this.InterfaceIndex = 0L;
		}

		/// <summary>Gets or sets the IP address of a multicast group.</summary>
		/// <returns>An <see cref="T:System.Net.IPAddress" /> that contains the Internet address of a multicast group.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="group" /> is null. </exception>
		// Token: 0x17000E36 RID: 3638
		// (get) Token: 0x06003EBE RID: 16062 RVA: 0x000D693F File Offset: 0x000D4B3F
		// (set) Token: 0x06003EBF RID: 16063 RVA: 0x000D6947 File Offset: 0x000D4B47
		public IPAddress Group
		{
			get
			{
				return this.m_Group;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_Group = value;
			}
		}

		/// <summary>Gets or sets the interface index that is associated with a multicast group.</summary>
		/// <returns>A <see cref="T:System.UInt64" /> value that specifies the address of the interface.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value that is specified for a set operation is less than 0 or greater than 0x00000000FFFFFFFF. </exception>
		// Token: 0x17000E37 RID: 3639
		// (get) Token: 0x06003EC0 RID: 16064 RVA: 0x000D695E File Offset: 0x000D4B5E
		// (set) Token: 0x06003EC1 RID: 16065 RVA: 0x000D6966 File Offset: 0x000D4B66
		public long InterfaceIndex
		{
			get
			{
				return this.m_Interface;
			}
			set
			{
				if (value < 0L || value > (long)((ulong)(-1)))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.m_Interface = value;
			}
		}

		// Token: 0x04002502 RID: 9474
		private IPAddress m_Group;

		// Token: 0x04002503 RID: 9475
		private long m_Interface;
	}
}
