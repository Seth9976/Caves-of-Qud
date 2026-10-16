using System;
using System.Security.Permissions;

namespace Microsoft.Win32
{
	/// <summary>Provides data for the <see cref="E:Microsoft.Win32.SystemEvents.UserPreferenceChanged" /> event.</summary>
	// Token: 0x02000131 RID: 305
	[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
	[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
	public class UserPreferenceChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.Win32.UserPreferenceChangedEventArgs" /> class using the specified user preference category identifier.</summary>
		/// <param name="category">One of the <see cref="T:Microsoft.Win32.UserPreferenceCategory" /> values that indicates the user preference category that has changed. </param>
		// Token: 0x0600070F RID: 1807 RVA: 0x00013842 File Offset: 0x00011A42
		public UserPreferenceChangedEventArgs(UserPreferenceCategory category)
		{
			this.mycategory = category;
		}

		/// <summary>Gets the category of user preferences that has changed.</summary>
		/// <returns>One of the <see cref="T:Microsoft.Win32.UserPreferenceCategory" /> values that indicates the category of user preferences that has changed.</returns>
		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000710 RID: 1808 RVA: 0x00013851 File Offset: 0x00011A51
		public UserPreferenceCategory Category
		{
			get
			{
				return this.mycategory;
			}
		}

		// Token: 0x04000517 RID: 1303
		private UserPreferenceCategory mycategory;
	}
}
