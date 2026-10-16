using System;
using System.Configuration;
using System.Web.Configuration;
using Unity;

namespace System.Web.Profile
{
	/// <summary>Manages user profile data and settings.</summary>
	// Token: 0x02000505 RID: 1285
	public static class ProfileManager
	{
		/// <summary>Deletes user profile data for which the last activity date and time occurred before the specified date and time.</summary>
		/// <returns>The number of profiles deleted from the data source.</returns>
		/// <param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption" /> enumeration values, specifying whether anonymous, authenticated, or both types of profiles are deleted.</param>
		/// <param name="userInactiveSinceDate">A <see cref="T:System.DateTime" /> that identifies which user profiles are considered inactive. If the <see cref="P:System.Web.Profile.ProfileInfo.LastActivityDate" />  of a user profile occurs on or before this date and time, the profile is considered inactive.</param>
		// Token: 0x06003924 RID: 14628 RVA: 0x00097F30 File Offset: 0x00096130
		public static int DeleteInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
		{
			return ProfileManager.Provider.DeleteInactiveProfiles(authenticationOption, userInactiveSinceDate);
		}

		/// <summary>Deletes the profile for the specified user name from the data source.</summary>
		/// <returns>true if the user profile was found and deleted; otherwise, false.</returns>
		/// <param name="username">The user name for the profile to be deleted.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="username" /> is an empty string ("") or contains a comma.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="username" /> is null.</exception>
		// Token: 0x06003925 RID: 14629 RVA: 0x00097F3E File Offset: 0x0009613E
		public static bool DeleteProfile(string username)
		{
			return ProfileManager.Provider.DeleteProfiles(new string[] { username }) > 0;
		}

		/// <summary>Deletes profile properties and information for the supplied list of user names.</summary>
		/// <returns>The number of profiles deleted from the data source.</returns>
		/// <param name="usernames">A string array of user names for profiles to be deleted. </param>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="usernames" /> is zero.- or -One of the items in <paramref name="usernames" /> is an empty string ("") or contains a comma.- or -Two or more items in <paramref name="usernames" /> have the same value.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="usernames" /> is null.- or -One of the items in <paramref name="usernames" /> is null.</exception>
		// Token: 0x06003926 RID: 14630 RVA: 0x00097F57 File Offset: 0x00096157
		public static int DeleteProfiles(string[] usernames)
		{
			return ProfileManager.Provider.DeleteProfiles(usernames);
		}

		/// <summary>Deletes profile properties and information from the data source for the supplied list of profiles.</summary>
		/// <returns>The number of profiles deleted from the data source.</returns>
		/// <param name="profiles">A <see cref="T:System.Web.Profile.ProfileInfoCollection" />  that contains profile information for profiles to be deleted.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="profiles" /> has a <see cref="P:System.Web.Profile.ProfileInfoCollection.Count" /> value of zero.- or -One of the <see cref="T:System.Web.Profile.ProfileInfo" /> objects in <paramref name="profiles" /> has a <see cref="P:System.Web.Profile.ProfileInfo.UserName" /> that is an empty string ("") or contains a comma.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="profiles" /> is null.- or -One of the <see cref="T:System.Web.Profile.ProfileInfo" /> objects in <paramref name="profiles" /> has a <see cref="P:System.Web.Profile.ProfileInfo.UserName" /> that is null.</exception>
		// Token: 0x06003927 RID: 14631 RVA: 0x00097F64 File Offset: 0x00096164
		public static int DeleteProfiles(ProfileInfoCollection profiles)
		{
			return ProfileManager.Provider.DeleteProfiles(profiles);
		}

		/// <summary>Retrieves profile information for all profiles in which the last activity date occurred on or before the specified date and time and the user name for the profile matches the specified name.</summary>
		/// <returns>A <see cref="T:System.Web.Profile.ProfileInfoCollection" /> containing user profile information for inactive profiles in which the user name matches the supplied <paramref name="usernameToMatch" /> parameter.</returns>
		/// <param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption" />  enumeration values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param>
		/// <param name="usernameToMatch">The user name for which to search.</param>
		/// <param name="userInactiveSinceDate">A <see cref="T:System.DateTime" /> that identifies which user profiles are considered inactive. If the <see cref="P:System.Web.Profile.ProfileInfo.LastActivityDate" />  of a user profile occurs on or before this date and time, the profile is considered inactive.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="usernameToMatch" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="usernameToMatch" /> is an empty string ("").</exception>
		// Token: 0x06003928 RID: 14632 RVA: 0x00097F74 File Offset: 0x00096174
		public static ProfileInfoCollection FindInactiveProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, DateTime userInactiveSinceDate)
		{
			int num = 0;
			return ProfileManager.Provider.FindInactiveProfilesByUserName(authenticationOption, usernameToMatch, userInactiveSinceDate, 0, int.MaxValue, out num);
		}

		/// <summary>Retrieves profile information in pages of data for profiles in which the last activity date occurred on or before the specified date and time and the user name for the profile matches the specified name.</summary>
		/// <returns>A <see cref="T:System.Web.Profile.ProfileInfoCollection" /> containing user profile information for inactive profiles where the user name matches the supplied <paramref name="usernameToMatch" /> parameter.</returns>
		/// <param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption" />  values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param>
		/// <param name="usernameToMatch">The user name for which to search.</param>
		/// <param name="userInactiveSinceDate">A <see cref="T:System.DateTime" /> that identifies which user profiles are considered inactive. If the <see cref="P:System.Web.Profile.ProfileInfo.LastActivityDate" />  of a user profile occurs on or before this date and time, the profile is considered inactive.</param>
		/// <param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex" /> is zero-based.</param>
		/// <param name="pageSize">The size of the page of results to return.</param>
		/// <param name="totalRecords">When this method returns, contains an integer that identifies the total number of profiles. This parameter is passed uninitialized.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="usernameToMatch" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="usernameToMatch" /> is an empty string ("").- or -<paramref name="pageIndex" /> is less than zero.- or -<paramref name="pageSize" /> is less than 1.- or -<paramref name="pageIndex" /> multiplied by <paramref name="pageSize" /> is larger than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06003929 RID: 14633 RVA: 0x00097F98 File Offset: 0x00096198
		public static ProfileInfoCollection FindInactiveProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
		{
			return ProfileManager.Provider.FindInactiveProfilesByUserName(authenticationOption, usernameToMatch, userInactiveSinceDate, pageIndex, pageSize, out totalRecords);
		}

		/// <summary>Retrieves all profile information for profiles in which the user name matches the specified name.</summary>
		/// <returns>A <see cref="T:System.Web.Profile.ProfileInfoCollection" /> containing user profile information for profiles where the user name matches the supplied <paramref name="usernameToMatch" /> parameter.</returns>
		/// <param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption" />  enumeration values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param>
		/// <param name="usernameToMatch">The user name for which to search.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="usernameToMatch" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="usernameToMatch" /> is an empty string ("").</exception>
		// Token: 0x0600392A RID: 14634 RVA: 0x00097FAC File Offset: 0x000961AC
		public static ProfileInfoCollection FindProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch)
		{
			int num = 0;
			return ProfileManager.Provider.FindProfilesByUserName(authenticationOption, usernameToMatch, 0, int.MaxValue, out num);
		}

		/// <summary>Retrieves profile information in pages of data for profiles in which the user name matches the specified name.</summary>
		/// <returns>A <see cref="T:System.Web.Profile.ProfileInfoCollection" /> containing user profile information for profiles where the user name matches the supplied <paramref name="usernameToMatch" /> parameter.</returns>
		/// <param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption" />  enumeration values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param>
		/// <param name="usernameToMatch">The user name for which to search.</param>
		/// <param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex" /> is zero-based.</param>
		/// <param name="pageSize">The size of the page of results to return.</param>
		/// <param name="totalRecords">When this method returns, contains an integer that identifies the total number of profiles. This parameter is passed uninitialized.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="usernameToMatch" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="usernameToMatch" /> is an empty string ("").- or -<paramref name="pageIndex" /> is less than zero.- or -<paramref name="pageSize" /> is less than 1.- or -<paramref name="pageIndex" /> multiplied by <paramref name="pageSize" /> is larger than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x0600392B RID: 14635 RVA: 0x00097FCF File Offset: 0x000961CF
		public static ProfileInfoCollection FindProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			return ProfileManager.Provider.FindProfilesByUserName(authenticationOption, usernameToMatch, pageIndex, pageSize, out totalRecords);
		}

		/// <summary>Retrieves all user profile data for profiles in which the last activity date occurred on or before the specified date and time.</summary>
		/// <returns>A <see cref="T:System.Web.Profile.ProfileInfoCollection" /> containing user profile information about the inactive profiles.</returns>
		/// <param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption" /> enumeration values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param>
		/// <param name="userInactiveSinceDate">A <see cref="T:System.DateTime" /> that identifies which user profiles are considered inactive. If the <see cref="P:System.Web.Profile.ProfileInfo.LastActivityDate" />  of a user profile occurs on or before this date and time, the profile is considered inactive.</param>
		// Token: 0x0600392C RID: 14636 RVA: 0x00097FE4 File Offset: 0x000961E4
		public static ProfileInfoCollection GetAllInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
		{
			int num = 0;
			return ProfileManager.Provider.GetAllInactiveProfiles(authenticationOption, userInactiveSinceDate, 0, int.MaxValue, out num);
		}

		/// <summary>Retrieves a page of <see cref="T:System.Web.Profile.ProfileInfo" /> objects for user profiles in which the last activity date occurred on or before the specified date and time.</summary>
		/// <returns>A <see cref="T:System.Web.Profile.ProfileInfoCollection" /> containing user profile information about the inactive profiles.</returns>
		/// <param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption" /> enumeration values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param>
		/// <param name="userInactiveSinceDate">A <see cref="T:System.DateTime" /> that identifies which user profiles are considered inactive. If the <see cref="P:System.Web.Profile.ProfileInfo.LastActivityDate" />  of a user profile occurs on or before this date and time, the profile is considered inactive.</param>
		/// <param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex" /> is zero-based.</param>
		/// <param name="pageSize">The size of the page of results to return.</param>
		/// <param name="totalRecords">When this method returns, contains an integer that identifies the total number of profiles. This parameter is passed uninitialized.</param>
		// Token: 0x0600392D RID: 14637 RVA: 0x00098007 File Offset: 0x00096207
		public static ProfileInfoCollection GetAllInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
		{
			return ProfileManager.Provider.GetAllInactiveProfiles(authenticationOption, userInactiveSinceDate, pageIndex, pageSize, out totalRecords);
		}

		/// <summary>Retrieves user profile data for profiles in the data source.</summary>
		/// <returns>A <see cref="T:System.Web.Profile.ProfileInfoCollection" /> containing user profile information for all of the profiles in the data source.</returns>
		/// <param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption" /> enumeration values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param>
		// Token: 0x0600392E RID: 14638 RVA: 0x0009801C File Offset: 0x0009621C
		public static ProfileInfoCollection GetAllProfiles(ProfileAuthenticationOption authenticationOption)
		{
			int num = 0;
			return ProfileManager.Provider.GetAllProfiles(authenticationOption, 0, int.MaxValue, out num);
		}

		/// <summary>Retrieves pages of user profile data.</summary>
		/// <returns>A <see cref="T:System.Web.Profile.ProfileInfoCollection" /> containing user profile information for all of the profiles in the data source.</returns>
		/// <param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption" /> enumeration values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param>
		/// <param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex" /> is zero-based.</param>
		/// <param name="pageSize">The size of the page of results to return.</param>
		/// <param name="totalRecords">When this method returns, contains an integer that identifies the total number of profiles. This parameter is passed uninitialized.</param>
		// Token: 0x0600392F RID: 14639 RVA: 0x0009803E File Offset: 0x0009623E
		public static ProfileInfoCollection GetAllProfiles(ProfileAuthenticationOption authenticationOption, int pageIndex, int pageSize, out int totalRecords)
		{
			return ProfileManager.Provider.GetAllProfiles(authenticationOption, pageIndex, pageSize, out totalRecords);
		}

		/// <summary>Gets the number of profiles in which the last activity date occurred on or before the specified date.</summary>
		/// <returns>The number of profiles in the data source for which the last activity date occurred before the specified date and time.</returns>
		/// <param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption" /> enumeration values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param>
		/// <param name="userInactiveSinceDate">A <see cref="T:System.DateTime" /> object that identifies which user profiles are considered inactive. If the <see cref="P:System.Web.Profile.ProfileInfo.LastActivityDate" />  of a user profile occurs on or before this date and time, the profile is considered inactive.</param>
		// Token: 0x06003930 RID: 14640 RVA: 0x0009804E File Offset: 0x0009624E
		public static int GetNumberOfInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
		{
			return ProfileManager.Provider.GetNumberOfInactiveProfiles(authenticationOption, userInactiveSinceDate);
		}

		/// <summary>Gets the number of profiles in the data source.</summary>
		/// <returns>The number of profiles in the data source.</returns>
		/// <param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption" /> enumeration values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param>
		// Token: 0x06003931 RID: 14641 RVA: 0x0009805C File Offset: 0x0009625C
		public static int GetNumberOfProfiles(ProfileAuthenticationOption authenticationOption)
		{
			int num = 0;
			ProfileManager.Provider.GetAllProfiles(authenticationOption, 0, 1, out num);
			return num;
		}

		/// <summary>Gets or sets the name of the application for which to store and retrieve profile information.</summary>
		/// <returns>The name of the application for which to store and retrieve profile information.</returns>
		// Token: 0x170011D5 RID: 4565
		// (get) Token: 0x06003932 RID: 14642 RVA: 0x0009807C File Offset: 0x0009627C
		// (set) Token: 0x06003933 RID: 14643 RVA: 0x00098088 File Offset: 0x00096288
		public static string ApplicationName
		{
			get
			{
				return ProfileManager.Provider.ApplicationName;
			}
			set
			{
				ProfileManager.Provider.ApplicationName = value;
			}
		}

		/// <summary>Gets a value indicating whether the user profile will be automatically saved at the end of the execution of an ASP.NET page.</summary>
		/// <returns>true if the user profile will be automatically saved at the end of the execution of an ASP.NET page; otherwise, false. The default is true.</returns>
		/// <exception cref="T:System.Web.HttpException">An attempt was made to get the <see cref="P:System.Web.Profile.ProfileManager.AutomaticSaveEnabled" /> property value without at least <see cref="F:System.Web.AspNetHostingPermissionLevel.Medium" /> permission.</exception>
		// Token: 0x170011D6 RID: 4566
		// (get) Token: 0x06003934 RID: 14644 RVA: 0x00098095 File Offset: 0x00096295
		public static bool AutomaticSaveEnabled
		{
			get
			{
				return ProfileManager.config.AutomaticSaveEnabled;
			}
		}

		/// <summary>Gets a value indicating whether the user profile is enabled for the application.</summary>
		/// <returns>true if the user profile is enabled; otherwise, false. The default is false.</returns>
		// Token: 0x170011D7 RID: 4567
		// (get) Token: 0x06003935 RID: 14645 RVA: 0x000980A1 File Offset: 0x000962A1
		public static bool Enabled
		{
			get
			{
				return ProfileManager.config.Enabled;
			}
		}

		/// <summary>Gets a reference to the default profile provider for the application.</summary>
		/// <returns>The default profile provider for the application.</returns>
		/// <exception cref="T:System.Web.HttpException">An attempt was made to get the <see cref="P:System.Web.Profile.ProfileManager.Provider" /> property value without at least <see cref="F:System.Web.AspNetHostingPermissionLevel.Medium" /> permission.</exception>
		// Token: 0x170011D8 RID: 4568
		// (get) Token: 0x06003936 RID: 14646 RVA: 0x000980AD File Offset: 0x000962AD
		[global::System.MonoTODO("check AspNetHostingPermissionLevel")]
		public static ProfileProvider Provider
		{
			get
			{
				ProfileProvider profileProvider = ProfileManager.Providers[ProfileManager.config.DefaultProvider];
				if (profileProvider == null)
				{
					throw new ConfigurationErrorsException("Provider '" + ProfileManager.config.DefaultProvider + "' was not found");
				}
				return profileProvider;
			}
		}

		/// <summary>Gets a collection of the profile providers for the ASP.NET application.</summary>
		/// <returns>A <see cref="T:System.Web.Profile.ProfileProviderCollection" /> of the profile providers configured for the ASP.NET application.</returns>
		/// <exception cref="T:System.Web.HttpException">An attempt was made to get the <see cref="P:System.Web.Profile.ProfileManager.Providers" /> property value without at least <see cref="F:System.Web.AspNetHostingPermissionLevel.Medium" /> permission.</exception>
		// Token: 0x170011D9 RID: 4569
		// (get) Token: 0x06003937 RID: 14647 RVA: 0x000980E8 File Offset: 0x000962E8
		public static ProfileProviderCollection Providers
		{
			get
			{
				ProfileManager.CheckEnabled();
				if (ProfileManager.providersCollection == null)
				{
					ProfileProviderCollection profileProviderCollection = new ProfileProviderCollection();
					ProvidersHelper.InstantiateProviders(ProfileManager.config.Providers, profileProviderCollection, typeof(ProfileProvider));
					ProfileManager.providersCollection = profileProviderCollection;
				}
				return ProfileManager.providersCollection;
			}
		}

		// Token: 0x06003938 RID: 14648 RVA: 0x0009812C File Offset: 0x0009632C
		private static void CheckEnabled()
		{
			if (!ProfileManager.Enabled)
			{
				throw new Exception("This feature is not enabled.  To enable it, add <profile enabled=\"true\"> to your configuration file.");
			}
		}

		/// <summary>Adds a profile property programmatically.</summary>
		/// <param name="property">The property settings to be added.</param>
		// Token: 0x06003939 RID: 14649 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public static void AddDynamicProfileProperty(ProfilePropertySettings property)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001F22 RID: 7970
		private static ProfileSection config = (ProfileSection)WebConfigurationManager.GetSection("system.web/profile");

		// Token: 0x04001F23 RID: 7971
		private static ProfileProviderCollection providersCollection;
	}
}
