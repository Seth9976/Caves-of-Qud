using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000058 RID: 88
	public class RfcAttributeDescriptionList : Asn1SequenceOf
	{
		// Token: 0x06000332 RID: 818 RVA: 0x00010311 File Offset: 0x0000E511
		public RfcAttributeDescriptionList(int size)
			: base(size)
		{
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0001031C File Offset: 0x0000E51C
		public RfcAttributeDescriptionList(string[] attrs)
			: base((attrs == null) ? 0 : attrs.Length)
		{
			if (attrs != null)
			{
				for (int i = 0; i < attrs.Length; i++)
				{
					base.add(new RfcAttributeDescription(attrs[i]));
				}
			}
		}
	}
}
