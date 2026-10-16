using System;

namespace System.Web.Profile
{
	/// <summary>Identifies the profile provider for a user-profile property.</summary>
	// Token: 0x0200050B RID: 1291
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class ProfileProviderAttribute : Attribute
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Web.Profile.ProfileProviderAttribute" /> class with the specified profile provider name.</summary>
		/// <param name="providerName">The name of the profile provider for the property.</param>
		// Token: 0x06003959 RID: 14681 RVA: 0x000984BA File Offset: 0x000966BA
		public ProfileProviderAttribute(string providerName)
		{
			this.providerName = providerName;
		}

		/// <summary>Gets the name of the profile provider for the user-profile property.</summary>
		/// <returns>The name of the profile provider for the user-profile property.</returns>
		// Token: 0x170011DC RID: 4572
		// (get) Token: 0x0600395A RID: 14682 RVA: 0x000984C9 File Offset: 0x000966C9
		public string ProviderName
		{
			get
			{
				return this.providerName;
			}
		}

		// Token: 0x04001F2D RID: 7981
		private string providerName;
	}
}
