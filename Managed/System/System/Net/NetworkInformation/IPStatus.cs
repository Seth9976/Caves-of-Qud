using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Reports the status of sending an Internet Control Message Protocol (ICMP) echo message to a computer.</summary>
	// Token: 0x020006EC RID: 1772
	public enum IPStatus
	{
		/// <summary>The ICMP echo request succeeded; an ICMP echo reply was received. When you get this status code, the other <see cref="T:System.Net.NetworkInformation.PingReply" /> properties contain valid data.</summary>
		// Token: 0x04002194 RID: 8596
		Success,
		/// <summary>The ICMP echo request failed because the network that contains the destination computer is not reachable.</summary>
		// Token: 0x04002195 RID: 8597
		DestinationNetworkUnreachable = 11002,
		/// <summary>The ICMP echo request failed because the destination computer is not reachable.</summary>
		// Token: 0x04002196 RID: 8598
		DestinationHostUnreachable,
		/// <summary>The ICMP echo request failed because the destination computer that is specified in an ICMP echo message is not reachable, because it does not support the packet's protocol. This value applies only to IPv4. This value is described in IETF RFC 1812 as Communication Administratively Prohibited.</summary>
		// Token: 0x04002197 RID: 8599
		DestinationProtocolUnreachable,
		/// <summary>The ICMP echo request failed because the port on the destination computer is not available.</summary>
		// Token: 0x04002198 RID: 8600
		DestinationPortUnreachable,
		/// <summary>The ICMPv6 echo request failed because contact with the destination computer is administratively prohibited. This value applies only to IPv6.</summary>
		// Token: 0x04002199 RID: 8601
		DestinationProhibited = 11004,
		/// <summary>The ICMP echo request failed because of insufficient network resources.</summary>
		// Token: 0x0400219A RID: 8602
		NoResources = 11006,
		/// <summary>The ICMP echo request failed because it contains an invalid option.</summary>
		// Token: 0x0400219B RID: 8603
		BadOption,
		/// <summary>The ICMP echo request failed because of a hardware error.</summary>
		// Token: 0x0400219C RID: 8604
		HardwareError,
		/// <summary>The ICMP echo request failed because the packet containing the request is larger than the maximum transmission unit (MTU) of a node (router or gateway) located between the source and destination. The MTU defines the maximum size of a transmittable packet.</summary>
		// Token: 0x0400219D RID: 8605
		PacketTooBig,
		/// <summary>The ICMP echo Reply was not received within the allotted time. The default time allowed for replies is 5 seconds. You can change this value using the <see cref="Overload:System.Net.NetworkInformation.Ping.Send" />  or <see cref="Overload:System.Net.NetworkInformation.Ping.SendAsync" /> methods that take a <paramref name="timeout" /> parameter.</summary>
		// Token: 0x0400219E RID: 8606
		TimedOut,
		/// <summary>The ICMP echo request failed because there is no valid route between the source and destination computers.</summary>
		// Token: 0x0400219F RID: 8607
		BadRoute = 11012,
		/// <summary>The ICMP echo request failed because its Time to Live (TTL) value reached zero, causing the forwarding node (router or gateway) to discard the packet.</summary>
		// Token: 0x040021A0 RID: 8608
		TtlExpired,
		/// <summary>The ICMP echo request failed because the packet was divided into fragments for transmission and all of the fragments were not received within the time allotted for reassembly. RFC 2460 (available at www.ietf.org) specifies 60 seconds as the time limit within which all packet fragments must be received.</summary>
		// Token: 0x040021A1 RID: 8609
		TtlReassemblyTimeExceeded,
		/// <summary>The ICMP echo request failed because a node (router or gateway) encountered problems while processing the packet header. This is the status if, for example, the header contains invalid field data or an unrecognized option.</summary>
		// Token: 0x040021A2 RID: 8610
		ParameterProblem,
		/// <summary>The ICMP echo request failed because the packet was discarded. This occurs when the source computer's output queue has insufficient storage space, or when packets arrive at the destination too quickly to be processed.</summary>
		// Token: 0x040021A3 RID: 8611
		SourceQuench,
		/// <summary>The ICMP echo request failed because the destination IP address cannot receive ICMP echo requests or should never appear in the destination address field of any IP datagram. For example, calling <see cref="Overload:System.Net.NetworkInformation.Ping.Send" /> and specifying IP address "000.0.0.0" returns this status.</summary>
		// Token: 0x040021A4 RID: 8612
		BadDestination = 11018,
		/// <summary>The ICMP echo request failed because the destination computer that is specified in an ICMP echo message is not reachable; the exact cause of problem is unknown.</summary>
		// Token: 0x040021A5 RID: 8613
		DestinationUnreachable = 11040,
		/// <summary>The ICMP echo request failed because its Time to Live (TTL) value reached zero, causing the forwarding node (router or gateway) to discard the packet.</summary>
		// Token: 0x040021A6 RID: 8614
		TimeExceeded,
		/// <summary>The ICMP echo request failed because the header is invalid.</summary>
		// Token: 0x040021A7 RID: 8615
		BadHeader,
		/// <summary>The ICMP echo request failed because the Next Header field does not contain a recognized value. The Next Header field indicates the extension header type (if present) or the protocol above the IP layer, for example, TCP or UDP.</summary>
		// Token: 0x040021A8 RID: 8616
		UnrecognizedNextHeader,
		/// <summary>The ICMP echo request failed because of an ICMP protocol error.</summary>
		// Token: 0x040021A9 RID: 8617
		IcmpError,
		/// <summary>The ICMP echo request failed because the source address and destination address that are specified in an ICMP echo message are not in the same scope. This is typically caused by a router forwarding a packet using an interface that is outside the scope of the source address. Address scopes (link-local, site-local, and global scope) determine where on the network an address is valid.</summary>
		// Token: 0x040021AA RID: 8618
		DestinationScopeMismatch,
		/// <summary>The ICMP echo request failed for an unknown reason.</summary>
		// Token: 0x040021AB RID: 8619
		Unknown = -1
	}
}
