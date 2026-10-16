using System;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000034 RID: 52
	public class LdapSearchQueue : LdapMessageQueue
	{
		// Token: 0x06000222 RID: 546 RVA: 0x0000A2E4 File Offset: 0x000084E4
		internal LdapSearchQueue(MessageAgent agent)
			: base("LdapSearchQueue", agent)
		{
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000A2F4 File Offset: 0x000084F4
		public virtual void merge(LdapMessageQueue queue2)
		{
			LdapSearchQueue ldapSearchQueue = (LdapSearchQueue)queue2;
			this.agent.merge(ldapSearchQueue.MessageAgent);
		}
	}
}
