using System;
using System.Diagnostics;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x0200040F RID: 1039
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	internal static class CompModSwitches
	{
		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x06002184 RID: 8580 RVA: 0x00072897 File Offset: 0x00070A97
		public static BooleanSwitch CommonDesignerServices
		{
			get
			{
				if (CompModSwitches.commonDesignerServices == null)
				{
					CompModSwitches.commonDesignerServices = new BooleanSwitch("CommonDesignerServices", "Assert if any common designer service is not found.");
				}
				return CompModSwitches.commonDesignerServices;
			}
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x06002185 RID: 8581 RVA: 0x000728BF File Offset: 0x00070ABF
		public static TraceSwitch EventLog
		{
			get
			{
				if (CompModSwitches.eventLog == null)
				{
					CompModSwitches.eventLog = new TraceSwitch("EventLog", "Enable tracing for the EventLog component.");
				}
				return CompModSwitches.eventLog;
			}
		}

		// Token: 0x04001036 RID: 4150
		private static volatile BooleanSwitch commonDesignerServices;

		// Token: 0x04001037 RID: 4151
		private static volatile TraceSwitch eventLog;
	}
}
