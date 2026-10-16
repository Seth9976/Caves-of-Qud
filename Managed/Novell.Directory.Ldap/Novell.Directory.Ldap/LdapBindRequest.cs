using System;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000012 RID: 18
	public class LdapBindRequest : LdapMessage
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x000048D5 File Offset: 0x00002AD5
		public virtual string AuthenticationDN
		{
			get
			{
				return this.Asn1Object.RequestDN;
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000048E2 File Offset: 0x00002AE2
		[CLSCompliant(false)]
		public LdapBindRequest(int version, string dn, sbyte[] passwd, LdapControl[] cont)
			: base(0, new RfcBindRequest(new Asn1Integer(version), new RfcLdapDN(dn), new RfcAuthenticationChoice(new Asn1Tagged(new Asn1Identifier(2, false, 0), new Asn1OctetString(passwd), false))), cont)
		{
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00004917 File Offset: 0x00002B17
		[CLSCompliant(false)]
		public LdapBindRequest(int version, string dn, string mechanism, sbyte[] credentials, LdapControl[] cont)
			: base(0, new RfcBindRequest(version, dn, mechanism, credentials), cont)
		{
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x0000492C File Offset: 0x00002B2C
		public override string ToString()
		{
			return this.Asn1Object.ToString();
		}
	}
}
