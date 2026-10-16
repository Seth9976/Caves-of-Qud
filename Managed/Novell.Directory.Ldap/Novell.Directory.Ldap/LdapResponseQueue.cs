using System;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000030 RID: 48
	public class LdapResponseQueue : LdapMessageQueue
	{
		// Token: 0x060001E6 RID: 486 RVA: 0x00009913 File Offset: 0x00007B13
		internal LdapResponseQueue(MessageAgent agent)
			: base("LdapResponseQueue", agent)
		{
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00009924 File Offset: 0x00007B24
		public virtual void merge(LdapMessageQueue queue2)
		{
			LdapResponseQueue ldapResponseQueue = (LdapResponseQueue)queue2;
			this.agent.merge(ldapResponseQueue.MessageAgent);
		}
	}
}
