using System;

namespace Microsoft.Win32
{
	/// <summary>Defines identifiers for power mode events reported by the operating system.</summary>
	// Token: 0x02000124 RID: 292
	public enum PowerModes
	{
		/// <summary>The operating system is about to resume from a suspended state.</summary>
		// Token: 0x040004F1 RID: 1265
		Resume = 1,
		/// <summary>A power mode status notification event has been raised by the operating system. This might indicate a weak or charging battery, a transition between AC power and battery, or another change in the status of the system power supply.</summary>
		// Token: 0x040004F2 RID: 1266
		StatusChange,
		/// <summary>The operating system is about to be suspended.</summary>
		// Token: 0x040004F3 RID: 1267
		Suspend
	}
}
