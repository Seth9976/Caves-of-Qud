using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000064 RID: 100
	public class RfcDelRequest : RfcLdapDN, RfcRequest
	{
		// Token: 0x06000368 RID: 872 RVA: 0x00010866 File Offset: 0x0000EA66
		public RfcDelRequest(string dn)
			: base(dn)
		{
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0001086F File Offset: 0x0000EA6F
		[CLSCompliant(false)]
		public RfcDelRequest(sbyte[] dn)
			: base(dn)
		{
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00010878 File Offset: 0x0000EA78
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, false, 10);
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00010883 File Offset: 0x0000EA83
		public RfcRequest dupRequest(string base_Renamed, string filter, bool request)
		{
			if (base_Renamed == null)
			{
				return new RfcDelRequest(base.byteValue());
			}
			return new RfcDelRequest(base_Renamed);
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0001089A File Offset: 0x0000EA9A
		public string getRequestDN()
		{
			return base.stringValue();
		}
	}
}
