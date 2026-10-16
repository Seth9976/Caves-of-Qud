using System;
using System.Configuration;

namespace System.Web.Profile
{
	/// <summary>Defines the contract that ASP.NET implements to provide profile services using custom profile providers.</summary>
	// Token: 0x0200050A RID: 1290
	public abstract class ProfileProvider : SettingsProvider
	{
		/// <summary>When overridden in a derived class, deletes all user-profile data for profiles in which the last activity date occurred before the specified date.</summary>
		/// <returns>The number of profiles deleted from the data source.</returns>
		/// <param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption" /> values, specifying whether anonymous, authenticated, or both types of profiles are deleted.</param>
		/// <param name="userInactiveSinceDate">A <see cref="T:System.DateTime" /> that identifies which user profiles are considered inactive. If the <see cref="P:System.Web.Profile.ProfileInfo.LastActivityDate" />  value of a user profile occurs on or before this date and time, the profile is considered inactive.</param>
		// Token: 0x06003951 RID: 14673
		public abstract int DeleteInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate);

		/// <summary>When overridden in a derived class, deletes profile properties and information for profiles that match the supplied list of user names.</summary>
		/// <returns>The number of profiles deleted from the data source.</returns>
		/// <param name="usernames">A string array of user names for profiles to be deleted.</param>
		// Token: 0x06003952 RID: 14674
		public abstract int DeleteProfiles(string[] usernames);

		/// <summary>When overridden in a derived class, deletes profile properties and information for the supplied list of profiles.</summary>
		/// <returns>The number of profiles deleted from the data source.</returns>
		/// <param name="profiles">A <see cref="T:System.Web.Profile.ProfileInfoCollection" />  of information about profiles that are to be deleted.</param>
		// Token: 0x06003953 RID: 14675
		public abstract int DeleteProfiles(ProfileInfoCollection profiles);

		/// <summary>When overridden in a derived class, retrieves profile information for profiles in which the last activity date occurred on or before the specified date and the user name matches the specified user name.</summary>
		/// <returns>A <see cref="T:System.Web.Profile.ProfileInfoCollection" /> containing user profile information for inactive profiles where the user name matches the supplied <paramref name="usernameToMatch" /> parameter.</returns>
		/// <param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption" /> values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param>
		/// <param name="usernameToMatch">The user name to search for.</param>
		/// <param name="userInactiveSinceDate">A <see cref="T:System.DateTime" /> that identifies which user profiles are considered inactive. If the <see cref="P:System.Web.Profile.ProfileInfo.LastActivityDate" /> value of a user profile occurs on or before this date and time, the profile is considered inactive.</param>
		/// <param name="pageIndex">The index of the page of results to return.</param>
		/// <param name="pageSize">The size of the page of results to return.</param>
		/// <param name="totalRecords">When this method returns, contains the total number of profiles.</param>
		// Token: 0x06003954 RID: 14676
		public abstract ProfileInfoCollection FindInactiveProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords);

		/// <summary>When overridden in a derived class, retrieves profile information for profiles in which the user name matches the specified user names.</summary>
		/// <returns>A <see cref="T:System.Web.Profile.ProfileInfoCollection" /> containing user-profile information for profiles where the user name matches the supplied <paramref name="usernameToMatch" /> parameter.</returns>
		/// <param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption" /> values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param>
		/// <param name="usernameToMatch">The user name to search for.</param>
		/// <param name="pageIndex">The index of the page of results to return.</param>
		/// <param name="pageSize">The size of the page of results to return.</param>
		/// <param name="totalRecords">When this method returns, contains the total number of profiles.</param>
		// Token: 0x06003955 RID: 14677
		public abstract ProfileInfoCollection FindProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, int pageIndex, int pageSize, out int totalRecords);

		/// <summary>When overridden in a derived class, retrieves user-profile data from the data source for profiles in which the last activity date occurred on or before the specified date.</summary>
		/// <returns>A <see cref="T:System.Web.Profile.ProfileInfoCollection" /> containing user-profile information about the inactive profiles.</returns>
		/// <param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption" /> values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param>
		/// <param name="userInactiveSinceDate">A <see cref="T:System.DateTime" /> that identifies which user profiles are considered inactive. If the <see cref="P:System.Web.Profile.ProfileInfo.LastActivityDate" />  of a user profile occurs on or before this date and time, the profile is considered inactive.</param>
		/// <param name="pageIndex">The index of the page of results to return.</param>
		/// <param name="pageSize">The size of the page of results to return.</param>
		/// <param name="totalRecords">When this method returns, contains the total number of profiles.</param>
		// Token: 0x06003956 RID: 14678
		public abstract ProfileInfoCollection GetAllInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords);

		/// <summary>When overridden in a derived class, retrieves user profile data for all profiles in the data source.</summary>
		/// <returns>A <see cref="T:System.Web.Profile.ProfileInfoCollection" /> containing user-profile information for all profiles in the data source.</returns>
		/// <param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption" /> values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param>
		/// <param name="pageIndex">The index of the page of results to return.</param>
		/// <param name="pageSize">The size of the page of results to return.</param>
		/// <param name="totalRecords">When this method returns, contains the total number of profiles.</param>
		// Token: 0x06003957 RID: 14679
		public abstract ProfileInfoCollection GetAllProfiles(ProfileAuthenticationOption authenticationOption, int pageIndex, int pageSize, out int totalRecords);

		/// <summary>When overridden in a derived class, returns the number of profiles in which the last activity date occurred on or before the specified date.</summary>
		/// <returns>The number of profiles in which the last activity date occurred on or before the specified date.</returns>
		/// <param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption" /> values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param>
		/// <param name="userInactiveSinceDate">A <see cref="T:System.DateTime" /> that identifies which user profiles are considered inactive. If the <see cref="P:System.Web.Profile.ProfileInfo.LastActivityDate" />  of a user profile occurs on or before this date and time, the profile is considered inactive.</param>
		// Token: 0x06003958 RID: 14680
		public abstract int GetNumberOfInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate);
	}
}
