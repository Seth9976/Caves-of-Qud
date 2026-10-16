using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000070 RID: 112
	public class RfcMatchingRuleAssertion : Asn1Sequence
	{
		// Token: 0x060003C0 RID: 960 RVA: 0x00012141 File Offset: 0x00010341
		public RfcMatchingRuleAssertion(RfcAssertionValue matchValue)
			: this(null, null, matchValue, null)
		{
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x00012150 File Offset: 0x00010350
		public RfcMatchingRuleAssertion(RfcMatchingRuleId matchingRule, RfcAttributeDescription type, RfcAssertionValue matchValue, Asn1Boolean dnAttributes)
			: base(4)
		{
			if (matchingRule != null)
			{
				base.add(new Asn1Tagged(new Asn1Identifier(2, false, 1), matchingRule, false));
			}
			if (type != null)
			{
				base.add(new Asn1Tagged(new Asn1Identifier(2, false, 2), type, false));
			}
			base.add(new Asn1Tagged(new Asn1Identifier(2, false, 3), matchValue, false));
			if (dnAttributes != null && dnAttributes.booleanValue())
			{
				base.add(new Asn1Tagged(new Asn1Identifier(2, false, 4), dnAttributes, false));
			}
		}
	}
}
