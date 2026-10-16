using System;
using System.Security.Permissions;

namespace Microsoft.Win32
{
	/// <summary>Provides data for the <see cref="E:Microsoft.Win32.SystemEvents.SessionSwitch" /> event.</summary>
	// Token: 0x0200012A RID: 298
	[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
	[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
	public class SessionSwitchEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.Win32.SessionSwitchEventArgs" /> class using the specified session change event type identifer.</summary>
		/// <param name="reason">A <see cref="T:Microsoft.Win32.SessionSwitchReason" /> that indicates the type of session change event. </param>
		// Token: 0x060006E1 RID: 1761 RVA: 0x000136CE File Offset: 0x000118CE
		public SessionSwitchEventArgs(SessionSwitchReason reason)
		{
			this.reason = reason;
		}

		/// <summary>Gets an identifier that indicates the type of session change event.</summary>
		/// <returns>A <see cref="T:Microsoft.Win32.SessionSwitchReason" /> indicating the type of the session change event.</returns>
		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060006E2 RID: 1762 RVA: 0x000136DD File Offset: 0x000118DD
		public SessionSwitchReason Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x040004FA RID: 1274
		private SessionSwitchReason reason;
	}
}
