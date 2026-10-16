using System;

namespace System.Web.Profile
{
	/// <summary>Describes the authentication type of user profiles to be searched.</summary>
	// Token: 0x020004FC RID: 1276
	public enum ProfileAuthenticationOption
	{
		/// <summary>Search only anonymous profiles.</summary>
		// Token: 0x04001F08 RID: 7944
		Anonymous,
		/// <summary>Search only authenticated profiles.</summary>
		// Token: 0x04001F09 RID: 7945
		Authenticated,
		/// <summary>Search all profiles.</summary>
		// Token: 0x04001F0A RID: 7946
		All
	}
}
