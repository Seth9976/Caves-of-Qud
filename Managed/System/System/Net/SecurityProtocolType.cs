using System;

namespace System.Net
{
	/// <summary>Specifies the security protocols that are supported by the Schannel security package.</summary>
	// Token: 0x02000599 RID: 1433
	[Flags]
	public enum SecurityProtocolType
	{
		// Token: 0x040019C9 RID: 6601
		SystemDefault = 0,
		/// <summary>Specifies the Secure Socket Layer (SSL) 3.0 security protocol.</summary>
		// Token: 0x040019CA RID: 6602
		Ssl3 = 48,
		/// <summary>Specifies the Transport Layer Security (TLS) 1.0 security protocol.</summary>
		// Token: 0x040019CB RID: 6603
		Tls = 192,
		/// <summary>Specifies the Transport Layer Security (TLS) 1.1 security protocol.</summary>
		// Token: 0x040019CC RID: 6604
		Tls11 = 768,
		/// <summary>Specifies the Transport Layer Security (TLS) 1.2 security protocol.</summary>
		// Token: 0x040019CD RID: 6605
		Tls12 = 3072,
		// Token: 0x040019CE RID: 6606
		Tls13 = 12288
	}
}
