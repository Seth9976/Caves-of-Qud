using System;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000036 RID: 54
	public class LdapSearchResult : LdapMessage
	{
		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000230 RID: 560 RVA: 0x0000A508 File Offset: 0x00008708
		public virtual LdapEntry Entry
		{
			get
			{
				if (this.entry == null)
				{
					LdapAttributeSet ldapAttributeSet = new LdapAttributeSet();
					foreach (Asn1Sequence asn1Sequence in ((RfcSearchResultEntry)this.message.Response).Attributes.toArray())
					{
						LdapAttribute ldapAttribute = new LdapAttribute(((Asn1OctetString)asn1Sequence.get_Renamed(0)).stringValue());
						object[] array2 = ((Asn1Set)asn1Sequence.get_Renamed(1)).toArray();
						object[] array3 = array2;
						for (int j = 0; j < array3.Length; j++)
						{
							ldapAttribute.addValue(((Asn1OctetString)array3[j]).byteValue());
						}
						ldapAttributeSet.Add(ldapAttribute);
					}
					this.entry = new LdapEntry(((RfcSearchResultEntry)this.message.Response).ObjectName.stringValue(), ldapAttributeSet);
				}
				return this.entry;
			}
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000A5E0 File Offset: 0x000087E0
		internal LdapSearchResult(RfcLdapMessage message)
			: base(message)
		{
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000A5E9 File Offset: 0x000087E9
		public LdapSearchResult(LdapEntry entry, LdapControl[] cont)
		{
			if (entry == null)
			{
				throw new ArgumentException("Argument \"entry\" cannot be null");
			}
			this.entry = entry;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000A608 File Offset: 0x00008808
		public override string ToString()
		{
			string text;
			if (this.entry == null)
			{
				text = base.ToString();
			}
			else
			{
				text = this.entry.ToString();
			}
			return text;
		}

		// Token: 0x0400015B RID: 347
		private LdapEntry entry;
	}
}
