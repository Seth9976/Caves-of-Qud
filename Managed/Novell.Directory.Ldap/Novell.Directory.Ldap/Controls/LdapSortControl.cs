using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Controls
{
	// Token: 0x020000C9 RID: 201
	public class LdapSortControl : LdapControl
	{
		// Token: 0x060004E9 RID: 1257 RVA: 0x00016658 File Offset: 0x00014858
		public LdapSortControl(LdapSortKey key, bool critical)
			: this(new LdapSortKey[] { key }, critical)
		{
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0001666C File Offset: 0x0001486C
		public LdapSortControl(LdapSortKey[] keys, bool critical)
			: base(LdapSortControl.requestOID, critical, null)
		{
			Asn1SequenceOf asn1SequenceOf = new Asn1SequenceOf();
			for (int i = 0; i < keys.Length; i++)
			{
				Asn1Sequence asn1Sequence = new Asn1Sequence();
				asn1Sequence.add(new Asn1OctetString(keys[i].Key));
				if (keys[i].MatchRule != null)
				{
					asn1Sequence.add(new Asn1Tagged(new Asn1Identifier(2, false, LdapSortControl.ORDERING_RULE), new Asn1OctetString(keys[i].MatchRule), false));
				}
				if (keys[i].Reverse)
				{
					asn1Sequence.add(new Asn1Tagged(new Asn1Identifier(2, false, LdapSortControl.REVERSE_ORDER), new Asn1Boolean(true), false));
				}
				asn1SequenceOf.add(asn1Sequence);
			}
			this.setValue(asn1SequenceOf.getEncoding(new LBEREncoder()));
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00016728 File Offset: 0x00014928
		static LdapSortControl()
		{
			try
			{
				LdapControl.register(LdapSortControl.responseOID, Type.GetType("Novell.Directory.Ldap.Controls.LdapSortResponse"));
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x04000481 RID: 1153
		private static int ORDERING_RULE = 0;

		// Token: 0x04000482 RID: 1154
		private static int REVERSE_ORDER = 1;

		// Token: 0x04000483 RID: 1155
		private static string requestOID = "1.2.840.113556.1.4.473";

		// Token: 0x04000484 RID: 1156
		private static string responseOID = "1.2.840.113556.1.4.474";
	}
}
