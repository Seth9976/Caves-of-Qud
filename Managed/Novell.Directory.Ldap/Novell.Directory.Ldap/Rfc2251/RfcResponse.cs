using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x0200007A RID: 122
	public interface RfcResponse
	{
		// Token: 0x060003DD RID: 989
		Asn1Enumerated getResultCode();

		// Token: 0x060003DE RID: 990
		RfcLdapDN getMatchedDN();

		// Token: 0x060003DF RID: 991
		RfcLdapString getErrorMessage();

		// Token: 0x060003E0 RID: 992
		RfcReferral getReferral();
	}
}
