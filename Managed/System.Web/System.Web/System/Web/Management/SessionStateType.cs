using System;

namespace System.Web.Management
{
	/// <summary>Describes the session-state type used when installing a session-state database provider.</summary>
	// Token: 0x02000528 RID: 1320
	public enum SessionStateType
	{
		/// <summary>Session state data is stored in tempdb, and stored procedures are placed in the "ASPState" database. Session state data will not survive a restart of SQL Server.</summary>
		// Token: 0x04001F5E RID: 8030
		Temporary,
		/// <summary>Session-state data and stored procedures are placed in the "ASPState" database. Session-state data will survive a restart of the database server.</summary>
		// Token: 0x04001F5F RID: 8031
		Persisted,
		/// <summary>Session-state data and stored procedures are placed in a custom data store.</summary>
		// Token: 0x04001F60 RID: 8032
		Custom
	}
}
