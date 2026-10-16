using System;

namespace System
{
	/// <summary>Defines host name types for the <see cref="M:System.Uri.CheckHostName(System.String)" /> method.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000160 RID: 352
	public enum UriHostNameType
	{
		/// <summary>The type of the host name is not supplied.</summary>
		// Token: 0x0400066E RID: 1646
		Unknown,
		/// <summary>The host is set, but the type cannot be determined.</summary>
		// Token: 0x0400066F RID: 1647
		Basic,
		/// <summary>The host name is a domain name system (DNS) style host name.</summary>
		// Token: 0x04000670 RID: 1648
		Dns,
		/// <summary>The host name is an Internet Protocol (IP) version 4 host address.</summary>
		// Token: 0x04000671 RID: 1649
		IPv4,
		/// <summary>The host name is an Internet Protocol (IP) version 6 host address.</summary>
		// Token: 0x04000672 RID: 1650
		IPv6
	}
}
