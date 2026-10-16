using System;

namespace System.Net.Sockets
{
	/// <summary>Specifies the protocols that the <see cref="T:System.Net.Sockets.Socket" /> class supports.</summary>
	// Token: 0x020007B0 RID: 1968
	public enum ProtocolType
	{
		/// <summary>Internet Protocol.</summary>
		// Token: 0x04002525 RID: 9509
		IP,
		/// <summary>IPv6 Hop by Hop Options header.</summary>
		// Token: 0x04002526 RID: 9510
		IPv6HopByHopOptions = 0,
		/// <summary>Internet Control Message Protocol.</summary>
		// Token: 0x04002527 RID: 9511
		Icmp,
		/// <summary>Internet Group Management Protocol.</summary>
		// Token: 0x04002528 RID: 9512
		Igmp,
		/// <summary>Gateway To Gateway Protocol.</summary>
		// Token: 0x04002529 RID: 9513
		Ggp,
		/// <summary>Internet Protocol version 4.</summary>
		// Token: 0x0400252A RID: 9514
		IPv4,
		/// <summary>Transmission Control Protocol.</summary>
		// Token: 0x0400252B RID: 9515
		Tcp = 6,
		/// <summary>PARC Universal Packet Protocol.</summary>
		// Token: 0x0400252C RID: 9516
		Pup = 12,
		/// <summary>User Datagram Protocol.</summary>
		// Token: 0x0400252D RID: 9517
		Udp = 17,
		/// <summary>Internet Datagram Protocol.</summary>
		// Token: 0x0400252E RID: 9518
		Idp = 22,
		/// <summary>Internet Protocol version 6 (IPv6). </summary>
		// Token: 0x0400252F RID: 9519
		IPv6 = 41,
		/// <summary>IPv6 Routing header.</summary>
		// Token: 0x04002530 RID: 9520
		IPv6RoutingHeader = 43,
		/// <summary>IPv6 Fragment header.</summary>
		// Token: 0x04002531 RID: 9521
		IPv6FragmentHeader,
		/// <summary>IPv6 Encapsulating Security Payload header.</summary>
		// Token: 0x04002532 RID: 9522
		IPSecEncapsulatingSecurityPayload = 50,
		/// <summary>IPv6 Authentication header. For details, see RFC 2292 section 2.2.1, available at http://www.ietf.org.</summary>
		// Token: 0x04002533 RID: 9523
		IPSecAuthenticationHeader,
		/// <summary>Internet Control Message Protocol for IPv6.</summary>
		// Token: 0x04002534 RID: 9524
		IcmpV6 = 58,
		/// <summary>IPv6 No next header.</summary>
		// Token: 0x04002535 RID: 9525
		IPv6NoNextHeader,
		/// <summary>IPv6 Destination Options header.</summary>
		// Token: 0x04002536 RID: 9526
		IPv6DestinationOptions,
		/// <summary>Net Disk Protocol (unofficial).</summary>
		// Token: 0x04002537 RID: 9527
		ND = 77,
		/// <summary>Raw IP packet protocol.</summary>
		// Token: 0x04002538 RID: 9528
		Raw = 255,
		/// <summary>Unspecified protocol.</summary>
		// Token: 0x04002539 RID: 9529
		Unspecified = 0,
		/// <summary>Internet Packet Exchange Protocol.</summary>
		// Token: 0x0400253A RID: 9530
		Ipx = 1000,
		/// <summary>Sequenced Packet Exchange protocol.</summary>
		// Token: 0x0400253B RID: 9531
		Spx = 1256,
		/// <summary>Sequenced Packet Exchange version 2 protocol.</summary>
		// Token: 0x0400253C RID: 9532
		SpxII,
		/// <summary>Unknown protocol.</summary>
		// Token: 0x0400253D RID: 9533
		Unknown = -1
	}
}
