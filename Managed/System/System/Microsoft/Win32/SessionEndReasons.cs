using System;

namespace Microsoft.Win32
{
	/// <summary>Defines identifiers that represent how the current logon session is ending.</summary>
	// Token: 0x02000125 RID: 293
	public enum SessionEndReasons
	{
		/// <summary>The user is logging off and ending the current user session. The operating system continues to run.</summary>
		// Token: 0x040004F5 RID: 1269
		Logoff = 1,
		/// <summary>The operating system is shutting down.</summary>
		// Token: 0x040004F6 RID: 1270
		SystemShutdown
	}
}
