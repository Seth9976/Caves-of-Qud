using System;

namespace System.Web.Profile
{
	/// <summary>Identifies whether a profile property can be set or accessed for an anonymous user.</summary>
	// Token: 0x0200050D RID: 1293
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class SettingsAllowAnonymousAttribute : Attribute
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Web.Profile.SettingsAllowAnonymousAttribute" /> class and specifies whether to allow anonymous access to the associated profile property.</summary>
		/// <param name="allow">true if anonymous users can access the associated profile property; otherwise false.</param>
		// Token: 0x0600395E RID: 14686 RVA: 0x000984F0 File Offset: 0x000966F0
		public SettingsAllowAnonymousAttribute(bool allow)
		{
			this.allow = allow;
		}

		/// <summary>Gets a value indicating whether the <see cref="P:System.Web.Profile.SettingsAllowAnonymousAttribute.Allow" /> property is set to the default value.</summary>
		/// <returns>true if the <see cref="P:System.Web.Profile.SettingsAllowAnonymousAttribute.Allow" /> property is set to the default value; otherwise false.</returns>
		// Token: 0x0600395F RID: 14687 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		public override bool IsDefaultAttribute()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a value indicating whether the associated property of a custom profile implementation can be accessed if the user is an anonymous user.</summary>
		/// <returns>true if anonymous users can access the associated profile property; otherwise, false.</returns>
		// Token: 0x170011DE RID: 4574
		// (get) Token: 0x06003960 RID: 14688 RVA: 0x000984FF File Offset: 0x000966FF
		public bool Allow
		{
			get
			{
				return this.allow;
			}
		}

		// Token: 0x04001F2E RID: 7982
		private bool allow;
	}
}
