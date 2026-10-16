using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Specifies types of network interfaces.</summary>
	// Token: 0x0200070E RID: 1806
	public enum NetworkInterfaceType
	{
		/// <summary>The interface type is not known.</summary>
		// Token: 0x04002200 RID: 8704
		Unknown = 1,
		/// <summary>The network interface uses an Ethernet connection. Ethernet is defined in IEEE standard 802.3.</summary>
		// Token: 0x04002201 RID: 8705
		Ethernet = 6,
		/// <summary>The network interface uses a Token-Ring connection. Token-Ring is defined in IEEE standard 802.5.</summary>
		// Token: 0x04002202 RID: 8706
		TokenRing = 9,
		/// <summary>The network interface uses a Fiber Distributed Data Interface (FDDI) connection. FDDI is a set of standards for data transmission on fiber optic lines in a local area network.</summary>
		// Token: 0x04002203 RID: 8707
		Fddi = 15,
		/// <summary>The network interface uses a basic rate interface Integrated Services Digital Network (ISDN) connection. ISDN is a set of standards for data transmission over telephone lines.</summary>
		// Token: 0x04002204 RID: 8708
		BasicIsdn = 20,
		/// <summary>The network interface uses a primary rate interface Integrated Services Digital Network (ISDN) connection. ISDN is a set of standards for data transmission over telephone lines.</summary>
		// Token: 0x04002205 RID: 8709
		PrimaryIsdn,
		/// <summary>The network interface uses a Point-To-Point protocol (PPP) connection. PPP is a protocol for data transmission using a serial device.</summary>
		// Token: 0x04002206 RID: 8710
		Ppp = 23,
		/// <summary>The network interface is a loopback adapter. Such interfaces are often used for testing; no traffic is sent over the wire.</summary>
		// Token: 0x04002207 RID: 8711
		Loopback,
		/// <summary>The network interface uses an Ethernet 3 megabit/second connection. This version of Ethernet is defined in IETF RFC 895.</summary>
		// Token: 0x04002208 RID: 8712
		Ethernet3Megabit = 26,
		/// <summary>The network interface uses a Serial Line Internet Protocol (SLIP) connection. SLIP is defined in IETF RFC 1055.</summary>
		// Token: 0x04002209 RID: 8713
		Slip = 28,
		/// <summary>The network interface uses asynchronous transfer mode (ATM) for data transmission.</summary>
		// Token: 0x0400220A RID: 8714
		Atm = 37,
		/// <summary>The network interface uses a modem.</summary>
		// Token: 0x0400220B RID: 8715
		GenericModem = 48,
		/// <summary>The network interface uses a Fast Ethernet connection over twisted pair and provides a data rate of 100 megabits per second. This type of connection is also known as 100Base-T.</summary>
		// Token: 0x0400220C RID: 8716
		FastEthernetT = 62,
		/// <summary>The network interface uses a connection configured for ISDN and the X.25 protocol. X.25 allows computers on public networks to communicate using an intermediary computer.</summary>
		// Token: 0x0400220D RID: 8717
		Isdn,
		/// <summary>The network interface uses a Fast Ethernet connection over optical fiber and provides a data rate of 100 megabits per second. This type of connection is also known as 100Base-FX.</summary>
		// Token: 0x0400220E RID: 8718
		FastEthernetFx = 69,
		/// <summary>The network interface uses a wireless LAN connection (IEEE 802.11 standard).</summary>
		// Token: 0x0400220F RID: 8719
		Wireless80211 = 71,
		/// <summary>The network interface uses an Asymmetric Digital Subscriber Line (ADSL).</summary>
		// Token: 0x04002210 RID: 8720
		AsymmetricDsl = 94,
		/// <summary>The network interface uses a Rate Adaptive Digital Subscriber Line (RADSL).</summary>
		// Token: 0x04002211 RID: 8721
		RateAdaptDsl,
		/// <summary>The network interface uses a Symmetric Digital Subscriber Line (SDSL).</summary>
		// Token: 0x04002212 RID: 8722
		SymmetricDsl,
		/// <summary>The network interface uses a Very High Data Rate Digital Subscriber Line (VDSL).</summary>
		// Token: 0x04002213 RID: 8723
		VeryHighSpeedDsl,
		/// <summary>The network interface uses the Internet Protocol (IP) in combination with asynchronous transfer mode (ATM) for data transmission.</summary>
		// Token: 0x04002214 RID: 8724
		IPOverAtm = 114,
		/// <summary>The network interface uses a gigabit Ethernet connection and provides a data rate of 1,000 megabits per second (1 gigabit per second).</summary>
		// Token: 0x04002215 RID: 8725
		GigabitEthernet = 117,
		/// <summary>The network interface uses a tunnel connection.</summary>
		// Token: 0x04002216 RID: 8726
		Tunnel = 131,
		/// <summary>The network interface uses a Multirate Digital Subscriber Line.</summary>
		// Token: 0x04002217 RID: 8727
		MultiRateSymmetricDsl = 143,
		/// <summary>The network interface uses a High Performance Serial Bus.</summary>
		// Token: 0x04002218 RID: 8728
		HighPerformanceSerialBus,
		/// <summary>The network interface uses a mobile broadband interface for WiMax devices.</summary>
		// Token: 0x04002219 RID: 8729
		Wman = 237,
		/// <summary>The network interface uses a mobile broadband interface for GSM-based devices.</summary>
		// Token: 0x0400221A RID: 8730
		Wwanpp = 243,
		/// <summary>The network interface uses a mobile broadband interface for CDMA-based devices.</summary>
		// Token: 0x0400221B RID: 8731
		Wwanpp2
	}
}
