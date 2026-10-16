using System;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000014 RID: 20
	public class LdapCompareRequest : LdapMessage
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00004BFB File Offset: 0x00002DFB
		public virtual string AttributeDescription
		{
			get
			{
				return ((RfcCompareRequest)this.Asn1Object.getRequest()).AttributeValueAssertion.AttributeDescription;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00004C17 File Offset: 0x00002E17
		[CLSCompliant(false)]
		public virtual sbyte[] AssertionValue
		{
			get
			{
				return ((RfcCompareRequest)this.Asn1Object.getRequest()).AttributeValueAssertion.AssertionValue;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00004C33 File Offset: 0x00002E33
		public virtual string DN
		{
			get
			{
				return this.Asn1Object.RequestDN;
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00004C40 File Offset: 0x00002E40
		[CLSCompliant(false)]
		public LdapCompareRequest(string dn, string name, sbyte[] value_Renamed, LdapControl[] cont)
			: base(14, new RfcCompareRequest(new RfcLdapDN(dn), new RfcAttributeValueAssertion(new RfcAttributeDescription(name), new RfcAssertionValue(value_Renamed))), cont)
		{
		}
	}
}
