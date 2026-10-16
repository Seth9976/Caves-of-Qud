using System;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x02000099 RID: 153
	public class RefreshLdapServerRequest : LdapExtendedOperation
	{
		// Token: 0x06000425 RID: 1061 RVA: 0x0001397C File Offset: 0x00011B7C
		public RefreshLdapServerRequest()
			: base("2.16.840.1.113719.1.27.100.9", null)
		{
		}
	}
}
