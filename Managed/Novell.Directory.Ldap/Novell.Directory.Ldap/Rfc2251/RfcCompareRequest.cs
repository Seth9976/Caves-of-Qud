using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000060 RID: 96
	public class RfcCompareRequest : Asn1Sequence, RfcRequest
	{
		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000351 RID: 849 RVA: 0x0001060C File Offset: 0x0000E80C
		public virtual RfcAttributeValueAssertion AttributeValueAssertion
		{
			get
			{
				return (RfcAttributeValueAssertion)base.get_Renamed(1);
			}
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0001061A File Offset: 0x0000E81A
		public RfcCompareRequest(RfcLdapDN entry, RfcAttributeValueAssertion ava)
			: base(2)
		{
			base.add(entry);
			base.add(ava);
			if (ava.AssertionValue == null)
			{
				throw new ArgumentException("compare: Attribute must have an assertion value");
			}
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00010644 File Offset: 0x0000E844
		internal RfcCompareRequest(Asn1Object[] origRequest, string base_Renamed)
			: base(origRequest, origRequest.Length)
		{
			if (base_Renamed != null)
			{
				base.set_Renamed(0, new RfcLdapDN(base_Renamed));
			}
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00010660 File Offset: 0x0000E860
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 14);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0001066B File Offset: 0x0000E86B
		public RfcRequest dupRequest(string base_Renamed, string filter, bool request)
		{
			return new RfcCompareRequest(base.toArray(), base_Renamed);
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00010679 File Offset: 0x0000E879
		public string getRequestDN()
		{
			return ((RfcLdapDN)base.get_Renamed(0)).stringValue();
		}
	}
}
