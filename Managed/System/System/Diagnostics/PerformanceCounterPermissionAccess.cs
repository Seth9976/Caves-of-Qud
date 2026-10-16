using System;

namespace System.Diagnostics
{
	/// <summary>Defines access levels used by <see cref="T:System.Diagnostics.PerformanceCounter" /> permission classes.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200027A RID: 634
	[Flags]
	public enum PerformanceCounterPermissionAccess
	{
		/// <summary>The <see cref="T:System.Diagnostics.PerformanceCounter" /> has no permissions.</summary>
		// Token: 0x04000B47 RID: 2887
		None = 0,
		/// <summary>The <see cref="T:System.Diagnostics.PerformanceCounter" /> can read categories.</summary>
		// Token: 0x04000B48 RID: 2888
		[Obsolete]
		Browse = 1,
		/// <summary>The <see cref="T:System.Diagnostics.PerformanceCounter" /> can read categories.</summary>
		// Token: 0x04000B49 RID: 2889
		Read = 1,
		/// <summary>The <see cref="T:System.Diagnostics.PerformanceCounter" /> can write categories.</summary>
		// Token: 0x04000B4A RID: 2890
		Write = 2,
		/// <summary>The <see cref="T:System.Diagnostics.PerformanceCounter" /> can read and write categories.</summary>
		// Token: 0x04000B4B RID: 2891
		[Obsolete]
		Instrument = 3,
		/// <summary>The <see cref="T:System.Diagnostics.PerformanceCounter" /> can read, write, and create categories.</summary>
		// Token: 0x04000B4C RID: 2892
		Administer = 7
	}
}
