using System;

namespace System.Web.SessionState
{
	/// <summary>Specifies the session-state mode.</summary>
	// Token: 0x0200049E RID: 1182
	public enum SessionStateMode
	{
		/// <summary>Session state is disabled.</summary>
		// Token: 0x04001D71 RID: 7537
		Off,
		/// <summary>Session state is in process with an ASP.NET worker process.</summary>
		// Token: 0x04001D72 RID: 7538
		InProc,
		/// <summary>Session state is using the out-of-process ASP.NET State Service to store state information.</summary>
		// Token: 0x04001D73 RID: 7539
		StateServer,
		/// <summary>Session state is using an out-of-process SQL Server database to store state information.</summary>
		// Token: 0x04001D74 RID: 7540
		SQLServer,
		/// <summary>Session state is using a custom data store to store session-state information.</summary>
		// Token: 0x04001D75 RID: 7541
		Custom
	}
}
