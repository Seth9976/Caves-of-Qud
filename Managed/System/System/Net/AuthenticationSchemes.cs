using System;

namespace System.Net
{
	/// <summary>Specifies protocols for authentication.</summary>
	// Token: 0x020005C5 RID: 1477
	[Flags]
	public enum AuthenticationSchemes
	{
		/// <summary>No authentication is allowed. A client requesting an <see cref="T:System.Net.HttpListener" /> object with this flag set will always receive a 403 Forbidden status. Use this flag when a resource should never be served to a client.</summary>
		// Token: 0x04001A76 RID: 6774
		None = 0,
		/// <summary>Specifies digest authentication.</summary>
		// Token: 0x04001A77 RID: 6775
		Digest = 1,
		/// <summary>Negotiates with the client to determine the authentication scheme. If both client and server support Kerberos, it is used; otherwise, NTLM is used.</summary>
		// Token: 0x04001A78 RID: 6776
		Negotiate = 2,
		/// <summary>Specifies NTLM authentication.</summary>
		// Token: 0x04001A79 RID: 6777
		Ntlm = 4,
		/// <summary>Specifies basic authentication. </summary>
		// Token: 0x04001A7A RID: 6778
		Basic = 8,
		/// <summary>Specifies anonymous authentication.</summary>
		// Token: 0x04001A7B RID: 6779
		Anonymous = 32768,
		/// <summary>Specifies Windows authentication.</summary>
		// Token: 0x04001A7C RID: 6780
		IntegratedWindowsAuthentication = 6
	}
}
