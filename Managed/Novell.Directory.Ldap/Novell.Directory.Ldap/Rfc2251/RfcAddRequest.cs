using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000054 RID: 84
	public class RfcAddRequest : Asn1Sequence, RfcRequest
	{
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000326 RID: 806 RVA: 0x00010265 File Offset: 0x0000E465
		public virtual RfcAttributeList Attributes
		{
			get
			{
				return (RfcAttributeList)base.get_Renamed(1);
			}
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00010273 File Offset: 0x0000E473
		public RfcAddRequest(RfcLdapDN entry, RfcAttributeList attributes)
			: base(2)
		{
			base.add(entry);
			base.add(attributes);
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0001028A File Offset: 0x0000E48A
		internal RfcAddRequest(Asn1Object[] origRequest, string base_Renamed)
			: base(origRequest, origRequest.Length)
		{
			if (base_Renamed != null)
			{
				base.set_Renamed(0, new RfcLdapDN(base_Renamed));
			}
		}

		// Token: 0x06000329 RID: 809 RVA: 0x000102A6 File Offset: 0x0000E4A6
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 8);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x000102B0 File Offset: 0x0000E4B0
		public RfcRequest dupRequest(string base_Renamed, string filter, bool request)
		{
			return new RfcAddRequest(base.toArray(), base_Renamed);
		}

		// Token: 0x0600032B RID: 811 RVA: 0x000102BE File Offset: 0x0000E4BE
		public string getRequestDN()
		{
			return ((RfcLdapDN)base.get_Renamed(0)).stringValue();
		}
	}
}
