using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Specifies how an IP address network prefix was located.</summary>
	// Token: 0x02000701 RID: 1793
	public enum PrefixOrigin
	{
		/// <summary>The prefix was located using an unspecified source.</summary>
		// Token: 0x040021D3 RID: 8659
		Other,
		/// <summary>The prefix was manually configured.</summary>
		// Token: 0x040021D4 RID: 8660
		Manual,
		/// <summary>The prefix is a well-known prefix. Well-known prefixes are specified in standard-track Request for Comments (RFC) documents and assigned by the Internet Assigned Numbers Authority (Iana) or an address registry. Such prefixes are reserved for special purposes.</summary>
		// Token: 0x040021D5 RID: 8661
		WellKnown,
		/// <summary>The prefix was supplied by a Dynamic Host Configuration Protocol (DHCP) server.</summary>
		// Token: 0x040021D6 RID: 8662
		Dhcp,
		/// <summary>The prefix was supplied by a router advertisement.</summary>
		// Token: 0x040021D7 RID: 8663
		RouterAdvertisement
	}
}
