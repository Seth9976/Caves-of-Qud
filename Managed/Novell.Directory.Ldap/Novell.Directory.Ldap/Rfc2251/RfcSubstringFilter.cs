using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000080 RID: 128
	public class RfcSubstringFilter : Asn1Sequence
	{
		// Token: 0x060003F1 RID: 1009 RVA: 0x00012512 File Offset: 0x00010712
		public RfcSubstringFilter(RfcAttributeDescription type, Asn1SequenceOf substrings)
			: base(2)
		{
			base.add(type);
			base.add(substrings);
		}
	}
}
