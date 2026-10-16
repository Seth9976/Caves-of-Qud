using System;

namespace System.Net.Mail
{
	/// <summary>Specifies the level of access allowed to a Simple Mail Transport Protocol (SMTP) server.</summary>
	// Token: 0x02000806 RID: 2054
	public enum SmtpAccess
	{
		/// <summary>No access to an SMTP host.</summary>
		// Token: 0x04002766 RID: 10086
		None,
		/// <summary>Connection to an SMTP host on the default port (port 25).</summary>
		// Token: 0x04002767 RID: 10087
		Connect,
		/// <summary>Connection to an SMTP host on any port.</summary>
		// Token: 0x04002768 RID: 10088
		ConnectToUnrestrictedPort
	}
}
