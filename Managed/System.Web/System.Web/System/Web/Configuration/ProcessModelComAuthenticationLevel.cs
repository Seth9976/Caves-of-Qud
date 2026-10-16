using System;

namespace System.Web.Configuration
{
	/// <summary>Specifies the level of authentication for DCOM security.</summary>
	// Token: 0x0200056A RID: 1386
	public enum ProcessModelComAuthenticationLevel
	{
		/// <summary>Specifies no authentication. This field is constant. </summary>
		// Token: 0x0400202D RID: 8237
		None,
		/// <summary>Specifies that DCOM authenticates the credentials of the client. This field is constant.</summary>
		// Token: 0x0400202E RID: 8238
		Call,
		/// <summary>Specifies that DCOM authenticates the credentials of the client. This field is constant.</summary>
		// Token: 0x0400202F RID: 8239
		Connect,
		/// <summary>Specifies that DCOM determines the authentication level. This field is constant. </summary>
		// Token: 0x04002030 RID: 8240
		Default,
		/// <summary>Specifies that DCOM verifies that all data received is from the expected client. This field is constant. </summary>
		// Token: 0x04002031 RID: 8241
		Pkt,
		/// <summary>Specifies that DCOM authenticates and verifies the data transferred. This field is constant. </summary>
		// Token: 0x04002032 RID: 8242
		PktIntegrity,
		/// <summary>Specifies that DCOM authenticates all previous levels and does encryption. This field is constant. </summary>
		// Token: 0x04002033 RID: 8243
		PktPrivacy
	}
}
