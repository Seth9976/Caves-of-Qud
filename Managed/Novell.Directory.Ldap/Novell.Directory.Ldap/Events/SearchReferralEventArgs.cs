using System;

namespace Novell.Directory.Ldap.Events
{
	// Token: 0x020000AB RID: 171
	public class SearchReferralEventArgs : LdapEventArgs
	{
		// Token: 0x06000457 RID: 1111 RVA: 0x0001440E File Offset: 0x0001260E
		public SearchReferralEventArgs(LdapMessage sourceMessage, EventClassifiers aClassification, LdapEventType aType)
			: base(sourceMessage, EventClassifiers.CLASSIFICATION_LDAP_PSEARCH, LdapEventType.LDAP_PSEARCH_ANY)
		{
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0001441A File Offset: 0x0001261A
		public string[] getUrls()
		{
			return ((LdapSearchResultReference)this.ldap_message).Referrals;
		}
	}
}
