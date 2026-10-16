using System;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200003B RID: 59
	public interface LdapUnsolicitedNotificationListener
	{
		// Token: 0x06000244 RID: 580
		void messageReceived(LdapExtendedResponse msg);
	}
}
