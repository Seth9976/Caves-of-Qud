using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Specifies how an IP address host suffix was located.</summary>
	// Token: 0x02000703 RID: 1795
	public enum SuffixOrigin
	{
		/// <summary>The suffix was located using an unspecified source.</summary>
		// Token: 0x040021E2 RID: 8674
		Other,
		/// <summary>The suffix was manually configured.</summary>
		// Token: 0x040021E3 RID: 8675
		Manual,
		/// <summary>The suffix is a well-known suffix. Well-known suffixes are specified in standard-track Request for Comments (RFC) documents and assigned by the Internet Assigned Numbers Authority (Iana) or an address registry. Such suffixes are reserved for special purposes.</summary>
		// Token: 0x040021E4 RID: 8676
		WellKnown,
		/// <summary>The suffix was supplied by a Dynamic Host Configuration Protocol (DHCP) server.</summary>
		// Token: 0x040021E5 RID: 8677
		OriginDhcp,
		/// <summary>The suffix is a link-local suffix.</summary>
		// Token: 0x040021E6 RID: 8678
		LinkLayerAddress,
		/// <summary>The suffix was randomly assigned.</summary>
		// Token: 0x040021E7 RID: 8679
		Random
	}
}
