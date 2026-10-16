using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x0200005C RID: 92
	public class RfcAttributeValueAssertion : Asn1Sequence
	{
		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000338 RID: 824 RVA: 0x00010389 File Offset: 0x0000E589
		public virtual string AttributeDescription
		{
			get
			{
				return ((RfcAttributeDescription)base.get_Renamed(0)).stringValue();
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000339 RID: 825 RVA: 0x0001039C File Offset: 0x0000E59C
		[CLSCompliant(false)]
		public virtual sbyte[] AssertionValue
		{
			get
			{
				return ((RfcAssertionValue)base.get_Renamed(1)).byteValue();
			}
		}

		// Token: 0x0600033A RID: 826 RVA: 0x000103AF File Offset: 0x0000E5AF
		public RfcAttributeValueAssertion(RfcAttributeDescription ad, RfcAssertionValue av)
			: base(2)
		{
			base.add(ad);
			base.add(av);
		}
	}
}
