using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x0200005B RID: 91
	public class RfcAttributeValue : Asn1OctetString
	{
		// Token: 0x06000336 RID: 822 RVA: 0x00010377 File Offset: 0x0000E577
		public RfcAttributeValue(string value_Renamed)
			: base(value_Renamed)
		{
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00010380 File Offset: 0x0000E580
		[CLSCompliant(false)]
		public RfcAttributeValue(sbyte[] value_Renamed)
			: base(value_Renamed)
		{
		}
	}
}
