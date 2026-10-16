using System;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200003A RID: 58
	public class LdapUnbindRequest : LdapMessage
	{
		// Token: 0x06000243 RID: 579 RVA: 0x0000AD42 File Offset: 0x00008F42
		public LdapUnbindRequest(LdapControl[] cont)
			: base(2, new RfcUnbindRequest(), cont)
		{
		}
	}
}
