using System;

namespace System.Web.Profile
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Web.Profile.ProfileModule.MigrateAnonymous" /> event of the <see cref="T:System.Web.Profile.ProfileModule" /> class.</summary>
	/// <param name="sender">The <see cref="T:System.Web.Profile.ProfileModule" /> that raised the <see cref="E:System.Web.Profile.ProfileModule.MigrateAnonymous" /> event.</param>
	/// <param name="e">A <see cref="T:System.Web.Profile.ProfileMigrateEventArgs" /> that contains the event data.</param>
	// Token: 0x02000507 RID: 1287
	// (Invoke) Token: 0x0600393E RID: 14654
	public delegate void ProfileMigrateEventHandler(object sender, ProfileMigrateEventArgs e);
}
