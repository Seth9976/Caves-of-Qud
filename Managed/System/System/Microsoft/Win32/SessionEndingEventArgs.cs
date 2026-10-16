using System;
using System.Security.Permissions;

namespace Microsoft.Win32
{
	/// <summary>Provides data for the <see cref="E:Microsoft.Win32.SystemEvents.SessionEnding" /> event.</summary>
	// Token: 0x02000128 RID: 296
	[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
	[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
	public class SessionEndingEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.Win32.SessionEndingEventArgs" /> class using the specified value indicating the type of session close event that is occurring.</summary>
		/// <param name="reason">One of the <see cref="T:Microsoft.Win32.SessionEndReasons" /> that specifies how the session ends. </param>
		// Token: 0x060006D9 RID: 1753 RVA: 0x000136A6 File Offset: 0x000118A6
		public SessionEndingEventArgs(SessionEndReasons reason)
		{
			this.myreason = reason;
		}

		/// <summary>Gets the reason the session is ending.</summary>
		/// <returns>One of the <see cref="T:Microsoft.Win32.SessionEndReasons" /> values that specifies how the session is ending.</returns>
		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x000136B5 File Offset: 0x000118B5
		public SessionEndReasons Reason
		{
			get
			{
				return this.myreason;
			}
		}

		/// <summary>Gets or sets a value indicating whether to cancel the user request to end the session.</summary>
		/// <returns>true to cancel the user request to end the session; otherwise, false.</returns>
		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060006DB RID: 1755 RVA: 0x000136BD File Offset: 0x000118BD
		// (set) Token: 0x060006DC RID: 1756 RVA: 0x000136C5 File Offset: 0x000118C5
		public bool Cancel
		{
			get
			{
				return this.mycancel;
			}
			set
			{
				this.mycancel = value;
			}
		}

		// Token: 0x040004F8 RID: 1272
		private SessionEndReasons myreason;

		// Token: 0x040004F9 RID: 1273
		private bool mycancel;
	}
}
