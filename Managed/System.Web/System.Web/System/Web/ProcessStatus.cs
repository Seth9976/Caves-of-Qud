using System;

namespace System.Web
{
	/// <summary>Provides enumerated values that indicate the current status of a process.</summary>
	// Token: 0x020000CD RID: 205
	public enum ProcessStatus
	{
		/// <summary>Indicates that the process is running.</summary>
		// Token: 0x04001086 RID: 4230
		Alive = 1,
		/// <summary>Indicates that the process has begun to shut down.</summary>
		// Token: 0x04001087 RID: 4231
		ShuttingDown,
		/// <summary>Indicates that the process has shut down normally after receiving a shutdown message from the Internet Information Services (IIS) process.</summary>
		// Token: 0x04001088 RID: 4232
		ShutDown,
		/// <summary>Indicates that the process was forced to terminate by the Internet Information Services (IIS) process.</summary>
		// Token: 0x04001089 RID: 4233
		Terminated
	}
}
