using System;

namespace Microsoft.Win32
{
	/// <summary>Defines identifiers used to represent the type of a session switch event.</summary>
	// Token: 0x0200012C RID: 300
	public enum SessionSwitchReason
	{
		/// <summary>A session has been connected from the console.</summary>
		// Token: 0x040004FC RID: 1276
		ConsoleConnect = 1,
		/// <summary>A session has been disconnected from the console.</summary>
		// Token: 0x040004FD RID: 1277
		ConsoleDisconnect,
		/// <summary>A session has been connected from a remote connection.</summary>
		// Token: 0x040004FE RID: 1278
		RemoteConnect,
		/// <summary>A session has been disconnected from a remote connection.</summary>
		// Token: 0x040004FF RID: 1279
		RemoteDisconnect,
		/// <summary>A user has logged on to a session.</summary>
		// Token: 0x04000500 RID: 1280
		SessionLogon,
		/// <summary>A user has logged off from a session.</summary>
		// Token: 0x04000501 RID: 1281
		SessionLogoff,
		/// <summary>A session has been locked.</summary>
		// Token: 0x04000502 RID: 1282
		SessionLock,
		/// <summary>A session has been unlocked.</summary>
		// Token: 0x04000503 RID: 1283
		SessionUnlock,
		/// <summary>A session has changed its status to or from remote controlled mode.</summary>
		// Token: 0x04000504 RID: 1284
		SessionRemoteControl
	}
}
