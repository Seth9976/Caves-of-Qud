using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x0200005A RID: 90
	public class RfcAttributeTypeAndValues : Asn1Sequence
	{
		// Token: 0x06000335 RID: 821 RVA: 0x00010360 File Offset: 0x0000E560
		public RfcAttributeTypeAndValues(RfcAttributeDescription type, Asn1SetOf vals)
			: base(2)
		{
			base.add(type);
			base.add(vals);
		}
	}
}
