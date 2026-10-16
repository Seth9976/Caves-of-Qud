using System;
using System.Security.Permissions;

namespace Microsoft.Win32
{
	/// <summary>Provides data for the <see cref="E:Microsoft.Win32.SystemEvents.SessionEnded" /> event.</summary>
	// Token: 0x02000126 RID: 294
	[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
	[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
	public class SessionEndedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.Win32.SessionEndedEventArgs" /> class.</summary>
		/// <param name="reason">One of the <see cref="T:Microsoft.Win32.SessionEndReasons" /> values indicating how the session ended. </param>
		// Token: 0x060006D3 RID: 1747 RVA: 0x0001368F File Offset: 0x0001188F
		public SessionEndedEventArgs(SessionEndReasons reason)
		{
			this.myreason = reason;
		}

		/// <summary>Gets an identifier that indicates how the session ended.</summary>
		/// <returns>One of the <see cref="T:Microsoft.Win32.SessionEndReasons" /> values that indicates how the session ended.</returns>
		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060006D4 RID: 1748 RVA: 0x0001369E File Offset: 0x0001189E
		public SessionEndReasons Reason
		{
			get
			{
				return this.myreason;
			}
		}

		// Token: 0x040004F7 RID: 1271
		private SessionEndReasons myreason;
	}
}
