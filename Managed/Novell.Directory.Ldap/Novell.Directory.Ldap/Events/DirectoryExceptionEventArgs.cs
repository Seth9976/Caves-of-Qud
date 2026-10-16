using System;

namespace Novell.Directory.Ldap.Events
{
	// Token: 0x020000A5 RID: 165
	public class DirectoryExceptionEventArgs : BaseEventArgs
	{
		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000434 RID: 1076 RVA: 0x00013E91 File Offset: 0x00012091
		public LdapException LdapExceptionObject
		{
			get
			{
				return this.ldap_exception_object;
			}
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00013E99 File Offset: 0x00012099
		public DirectoryExceptionEventArgs(LdapMessage message, LdapException ldapException)
			: base(message)
		{
			this.ldap_exception_object = ldapException;
		}

		// Token: 0x04000304 RID: 772
		protected LdapException ldap_exception_object;
	}
}
