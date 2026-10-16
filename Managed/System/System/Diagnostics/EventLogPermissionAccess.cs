using System;

namespace System.Diagnostics
{
	/// <summary>Defines access levels used by <see cref="T:System.Diagnostics.EventLog" /> permission classes.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000264 RID: 612
	[Flags]
	public enum EventLogPermissionAccess
	{
		/// <summary>The <see cref="T:System.Diagnostics.EventLog" /> has no permissions.</summary>
		// Token: 0x04000AF3 RID: 2803
		None = 0,
		/// <summary>The <see cref="T:System.Diagnostics.EventLog" /> can read existing logs. Note This member is now obsolete, use <see cref="F:System.Diagnostics.EventLogPermissionAccess.Administer" /> instead.</summary>
		// Token: 0x04000AF4 RID: 2804
		[Obsolete]
		Browse = 2,
		/// <summary>The <see cref="T:System.Diagnostics.EventLog" /> can read or write to existing logs, and create event sources and logs. Note This member is now obsolete, use <see cref="F:System.Diagnostics.EventLogPermissionAccess.Write" /> instead.</summary>
		// Token: 0x04000AF5 RID: 2805
		[Obsolete]
		Instrument = 6,
		/// <summary>The <see cref="T:System.Diagnostics.EventLog" /> can read existing logs, delete event sources or logs, respond to entries, clear an event log, listen to events, and access a collection of all event logs. Note This member is now obsolete, use <see cref="F:System.Diagnostics.EventLogPermissionAccess.Administer" /> instead.</summary>
		// Token: 0x04000AF6 RID: 2806
		[Obsolete]
		Audit = 10,
		/// <summary>The <see cref="T:System.Diagnostics.EventLog" /> can write to existing logs, and create event sources and logs.</summary>
		// Token: 0x04000AF7 RID: 2807
		Write = 16,
		/// <summary>The <see cref="T:System.Diagnostics.EventLog" /> can create an event source, read existing logs, delete event sources or logs, respond to entries, clear an event log, listen to events, and access a collection of all event logs.</summary>
		// Token: 0x04000AF8 RID: 2808
		Administer = 48
	}
}
