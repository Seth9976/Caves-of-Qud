using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000075 RID: 117
	public class RfcModifyRequest : Asn1Sequence, RfcRequest
	{
		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x0001230A File Offset: 0x0001050A
		public virtual Asn1SequenceOf Modifications
		{
			get
			{
				return (Asn1SequenceOf)base.get_Renamed(1);
			}
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x00012318 File Offset: 0x00010518
		public RfcModifyRequest(RfcLdapDN object_Renamed, Asn1SequenceOf modification)
			: base(2)
		{
			base.add(object_Renamed);
			base.add(modification);
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0001232F File Offset: 0x0001052F
		internal RfcModifyRequest(Asn1Object[] origRequest, string base_Renamed)
			: base(origRequest, origRequest.Length)
		{
			if (base_Renamed != null)
			{
				base.set_Renamed(0, new RfcLdapDN(base_Renamed));
			}
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0001234B File Offset: 0x0001054B
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 6);
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00012355 File Offset: 0x00010555
		public RfcRequest dupRequest(string base_Renamed, string filter, bool request)
		{
			return new RfcModifyRequest(base.toArray(), base_Renamed);
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00012363 File Offset: 0x00010563
		public string getRequestDN()
		{
			return ((RfcLdapDN)base.get_Renamed(0)).stringValue();
		}
	}
}
