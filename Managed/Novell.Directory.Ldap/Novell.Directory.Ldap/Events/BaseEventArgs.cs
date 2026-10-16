using System;

namespace Novell.Directory.Ldap.Events
{
	// Token: 0x020000A3 RID: 163
	public class BaseEventArgs : EventArgs
	{
		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x00013E59 File Offset: 0x00012059
		public LdapMessage ContianedEventInformation
		{
			get
			{
				return this.ldap_message;
			}
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00013E61 File Offset: 0x00012061
		public BaseEventArgs(LdapMessage message)
		{
			this.ldap_message = message;
		}

		// Token: 0x04000302 RID: 770
		protected LdapMessage ldap_message;
	}
}
