using System;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Implements management and administrative functionality for Web Parts personalization. This class cannot be inherited. </summary>
	// Token: 0x020007AB RID: 1963
	public static class PersonalizationAdministration
	{
		/// <summary>Gets or sets the name of the application specified by the provider.</summary>
		/// <returns>The application name. </returns>
		/// <exception cref="T:System.ArgumentException">The provider for a personalization provider defined in configuration is not of the correct type.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration exception occurred while attempting to create and initialize an instance of one of the configured personalization providers.- or -The default personalization provider defined in configuration could not be found.</exception>
		// Token: 0x1700182F RID: 6191
		// (get) Token: 0x06004F31 RID: 20273 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004F32 RID: 20274 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public static string ApplicationName
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Returns an instance of the default personalization provider.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationProvider" /> of the default provider.</returns>
		/// <exception cref="T:System.ArgumentException">The provider for a personalization provider defined in configuration is not of the correct type.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration exception occurred while attempting to create and initialize an instance of one of the configured personalization providers.- or -The default personalization provider defined in configuration could not be found.</exception>
		// Token: 0x17001830 RID: 6192
		// (get) Token: 0x06004F33 RID: 20275 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public static PersonalizationProvider Provider
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Returns a collection of personalization providers indexed by name.</summary>
		/// <returns>A read-only <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationProviderCollection" /> that contains the personalization providers available to the application.</returns>
		/// <exception cref="T:System.ArgumentException">The provider for a personalization provider defined in configuration is not of the correct type.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration exception occurred while attempting to create and initialize an instance of one of the configured personalization providers.- or -The default personalization provider defined in configuration could not be found.</exception>
		// Token: 0x17001831 RID: 6193
		// (get) Token: 0x06004F34 RID: 20276 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public static PersonalizationProviderCollection Providers
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Returns a collection of per-user personalization state information for inactive users, based on the specified parameters.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationStateInfoCollection" /> that contains per-user personalization state information.</returns>
		/// <param name="pathToMatch">The path of the page to match.</param>
		/// <param name="usernameToMatch">The user name to match that has personalization data associated with the page.</param>
		/// <param name="userInactiveSinceDate">The last date a user's personalization information was accessed.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="pathToMatch" /> or <paramref name="usernameToMatch" /> before or after trimming is an empty string ("").- or -The provider for a personalization provider defined in configuration is not of the correct type.- or - The length of the string of any parameter is greater than 256 characters.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration exception occurred while attempting to create and initialize an instance of one of the configured personalization providers.- or -The default personalization provider defined in configuration could not be found.</exception>
		// Token: 0x06004F35 RID: 20277 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public static PersonalizationStateInfoCollection FindInactiveUserState(string pathToMatch, string usernameToMatch, DateTime userInactiveSinceDate)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns a collection of per-user personalization state information for inactive users, based on the specified parameters.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationStateInfoCollection" /> that contains per-user personalization state information.</returns>
		/// <param name="pathToMatch">The path of the page to match.</param>
		/// <param name="usernameToMatch">The user name to match that has personalization data associated with the page.</param>
		/// <param name="userInactiveSinceDate">The last date personalization information was accessed.</param>
		/// <param name="pageIndex">The zero-based index of the page of results to return. </param>
		/// <param name="pageSize">The number of records to return.</param>
		/// <param name="totalRecords">The total number of records available.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="pathToMatch" /> or <paramref name="usernameToMatch" /> before or after trimming is an empty string ("").- or -<paramref name="pageIndex" /> is less than zero- or -<paramref name="pageSize" /> is less than or equal to zero.- or -the combination of <paramref name="pageIndex" /> and <paramref name="pageSize" /> exceeds <see cref="F:System.Int32.MaxValue" />.- or -The provider for a personalization provider defined in configuration is not of the correct type.- or -The length of the string of any parameter is greater than 256 characters.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration exception occurred while attempting to create and initialize an instance of one of the configured personalization providers.- or -The default personalization provider defined in configuration could not be found.</exception>
		// Token: 0x06004F36 RID: 20278 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public static PersonalizationStateInfoCollection FindInactiveUserState(string pathToMatch, string usernameToMatch, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns a collection of shared personalization state information based on the specified path.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationStateInfoCollection" /> that contains shared personalization state information.</returns>
		/// <param name="pathToMatch">The path of the page to match.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="pathToMatch" /> is an empty string ("") either before or after trimming.- or -The provider for a personalization provider defined in configuration is not of the correct type.- or -The length of the string of any parameter is greater than 256 characters.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration exception occurred while attempting to create and initialize an instance of one of the configured personalization providers.- or -The default personalization provider defined in configuration could not be found.</exception>
		// Token: 0x06004F37 RID: 20279 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public static PersonalizationStateInfoCollection FindSharedState(string pathToMatch)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns a collection of shared personalization state information based on the specified parameters.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationStateInfoCollection" /> that contains shared personalization state information.</returns>
		/// <param name="pathToMatch">The path of the page to match.</param>
		/// <param name="pageIndex">The zero-based index of the page of results to return. </param>
		/// <param name="pageSize">The number of records to return.</param>
		/// <param name="totalRecords">The total number of records available.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="pathToMatch" /> before or after trimming is an empty string ("").- or -<paramref name="pageIndex" /> is less than zero.- or -<paramref name="pageSize" /> is less than or equal to zero.- or -The combination of <paramref name="pageIndex" /> and <paramref name="pageSize" /> exceeds <see cref="F:System.Int32.MaxValue" />.- or -The provider for a personalization provider defined in configuration is not of the correct type.- or - The length of the string of any parameter is greater than 256 characters.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration exception occurred while attempting to create and initialize an instance of one of the configured personalization providers.- or -The default personalization provider defined in configuration could not be found.</exception>
		// Token: 0x06004F38 RID: 20280 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public static PersonalizationStateInfoCollection FindSharedState(string pathToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns a collection of per-user personalization state information based on the user name and page path.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationStateInfoCollection" /> that contains per-user personalization state information.</returns>
		/// <param name="pathToMatch">The path of the page to match.</param>
		/// <param name="usernameToMatch">The user name to match that has personalization data associated with the page.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="pathToMatch" /> or <paramref name="usernameToMatch" /> before or after trimming is an empty string ("").- or -The provider for a personalization provider defined in configuration is not of the correct type.- or -The length of the string of any parameter is greater than 256 characters.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration exception occurred while attempting to create and initialize an instance of one of the configured personalization providers.- or -The default personalization provider defined in configuration could not be found.</exception>
		// Token: 0x06004F39 RID: 20281 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public static PersonalizationStateInfoCollection FindUserState(string pathToMatch, string usernameToMatch)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns a collection of per-user personalization state information based on the specified parameters.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationStateInfoCollection" /> that contains per-user personalization state information.</returns>
		/// <param name="pathToMatch">The path of the page to match.</param>
		/// <param name="usernameToMatch">The user name to match that has personalization data associated with the page.</param>
		/// <param name="pageIndex">The zero-based index of the page of results to return. </param>
		/// <param name="pageSize">The number of records to return.</param>
		/// <param name="totalRecords">The total number or records available.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="pathToMatch" /> or <paramref name="usernameToMatch" /> before or after trimming is an empty string ("").- or -<paramref name="pageIndex" /> is less than zero.- or -<paramref name="pageSize" /> is less than or equal to zero.-or-The combination of <paramref name="pageIndex" /> and <paramref name="pageSize" /> exceeds <see cref="F:System.Int32.MaxValue" />.- or - The provider for a personalization provider defined in configuration is not of the correct type.- or -The length of the string of any parameter is greater than 256 characters.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration exception occurred while attempting to create and initialize an instance of one of the configured personalization providers.- or -The default personalization provider defined in configuration could not be found.</exception>
		// Token: 0x06004F3A RID: 20282 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public static PersonalizationStateInfoCollection FindUserState(string pathToMatch, string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns a collection of all per-user personalization state information associated with inactive users, based on the specified date.</summary>
		/// <returns>A collection of <see cref="T:System.Web.UI.WebControls.WebParts.UserPersonalizationStateInfo" /> instances.</returns>
		/// <param name="userInactiveSinceDate">The last date a user's personalization information was accessed.</param>
		/// <exception cref="T:System.ArgumentException">The provider for a personalization provider defined in configuration is not of the correct type.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration exception occurred while attempting to create and initialize an instance of one of the configured personalization providers.- or -The default personalization provider defined in configuration could not be found.</exception>
		// Token: 0x06004F3B RID: 20283 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public static PersonalizationStateInfoCollection GetAllInactiveUserState(DateTime userInactiveSinceDate)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns a subset of all per-user personalization state information associated with inactive users, based on the specified parameters.</summary>
		/// <returns>A collection of <see cref="T:System.Web.UI.WebControls.WebParts.UserPersonalizationStateInfo" /> instances.</returns>
		/// <param name="userInactiveSinceDate">The last date a user's personalization information was accessed.</param>
		/// <param name="pageIndex">The zero-based index of the page of results to return. </param>
		/// <param name="pageSize">The number of records to return.</param>
		/// <param name="totalRecords">The total number of records available.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="pageIndex" /> is less than zero.- or -<paramref name="pageSize" /> is less than or equal to zero.- or -The combination of <paramref name="pageIndex" /> and <paramref name="pageSize" /> exceeds <see cref="F:System.Int32.MaxValue" />.- or -The provider for a personalization provider defined in configuration is not of the correct type.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration exception occurred while attempting to create and initialize an instance of one of the configured personalization providers.- or -The default personalization provider defined in configuration could not be found.</exception>
		// Token: 0x06004F3C RID: 20284 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public static PersonalizationStateInfoCollection GetAllInactiveUserState(DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns a collection of all personalization state information from the underlying data store for the requested personalization scope.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationStateInfoCollection" /> containing state information for the specified scope.</returns>
		/// <param name="scope">The scope of the personalization information to be retrieved.</param>
		/// <exception cref="T:System.ArgumentException">The provider for a personalization provider defined in configuration is not of the correct type.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration exception occurred while attempting to create and initialize an instance of one of the configured personalization providers.- or -The default personalization provider defined in configuration could not be found.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="scope" /> is not a valid member of the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationScope" /> enumeration.</exception>
		// Token: 0x06004F3D RID: 20285 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public static PersonalizationStateInfoCollection GetAllState(PersonalizationScope scope)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns a subset of all personalization state information from the underlying data store, based on the specified parameters.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationStateInfoCollection" /> containing state information for the specified scope and parameters.</returns>
		/// <param name="scope">The scope of the personalization information to be retrieved.</param>
		/// <param name="pageIndex">The zero-based index of the page of results to return. </param>
		/// <param name="pageSize">The number of records to return.</param>
		/// <param name="totalRecords">The total number of records available.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="pageIndex" /> is less than zero.- or -<paramref name="pageSize" /> is less than or equal to zero.- or -The provider for a personalization provider defined in configuration is not of the correct type.- or - The combination of <paramref name="pageIndex" /> and <paramref name="pageSize" /> exceeds <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="scope" /> is not a valid member of the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationScope" /> enumeration.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration exception occurred while attempting to create and initialize an instance of one of the configured personalization providers.- or -The default personalization provider defined in configuration could not be found.</exception>
		// Token: 0x06004F3E RID: 20286 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public static PersonalizationStateInfoCollection GetAllState(PersonalizationScope scope, int pageIndex, int pageSize, out int totalRecords)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns a count of the per-user personalization items in the underlying data store for inactive users, based on the parameter specified.</summary>
		/// <returns>The number of personalization items for inactive users.</returns>
		/// <param name="userInactiveSinceDate">The last date a user's personalization information was accessed.</param>
		/// <exception cref="T:System.ArgumentException">The provider for a personalization provider defined in configuration is not of the correct type.- or - The length of the string of any parameter is greater than 256 characters.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration exception occurred while attempting to create and initialize an instance of one of the configured personalization providers.- or -The default personalization provider defined in configuration could not be found.</exception>
		/// <exception cref="T:System.Web.HttpException">The default provider returned a negative number for the count.</exception>
		// Token: 0x06004F3F RID: 20287 RVA: 0x000C8CE4 File Offset: 0x000C6EE4
		public static int GetCountOfInactiveUserState(DateTime userInactiveSinceDate)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}

		/// <summary>Returns a count of the per-user personalization items in the underlying data store for inactive users, based on the specified parameters.</summary>
		/// <returns>The count of the per-user personalization items in the underlying data store for inactive users.</returns>
		/// <param name="pathToMatch">The path to the page with personalization state items to retrieve.</param>
		/// <param name="userInactiveSinceDate">The last date a user's personalization information was accessed.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="pathToMatch" />, after trimming, is an empty string ("").- or -The provider for a personalization provider defined in configuration is not of the correct type.- or - The length of the string of any parameter is greater than 256 characters.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration exception occurred while attempting to create and initialize an instance of one of the configured personalization providers.- or -The default personalization provider defined in configuration could not be found.</exception>
		/// <exception cref="T:System.Web.HttpException">The default provider returned a negative number for the count.</exception>
		// Token: 0x06004F40 RID: 20288 RVA: 0x000C8D00 File Offset: 0x000C6F00
		public static int GetCountOfInactiveUserState(string pathToMatch, DateTime userInactiveSinceDate)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}

		/// <summary>Returns a count of the personalization state items in the underlying data store that exist for the specified scope.</summary>
		/// <returns>The number of personalization state items for the specified scope.</returns>
		/// <param name="scope">The scope of the personalization state items to retrieve.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="scope" /> is not a valid member of the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationScope" /> enumeration.</exception>
		/// <exception cref="T:System.ArgumentException">The provider for a personalization provider defined in configuration is not of the correct type.- or - The length of the string of any parameter is greater than 256 characters.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration exception occurred while attempting to create and initialize an instance of one of the configured personalization providers.- or -The default personalization provider defined in configuration could not be found.</exception>
		/// <exception cref="T:System.Web.HttpException">The default provider returned a negative number for the count.</exception>
		// Token: 0x06004F41 RID: 20289 RVA: 0x000C8D1C File Offset: 0x000C6F1C
		public static int GetCountOfState(PersonalizationScope scope)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}

		/// <summary>Returns a count of the personalization state items in the underlying data store that exist for the specified parameters.</summary>
		/// <returns>The number of personalization state items for the specified scope.</returns>
		/// <param name="scope">The scope of the personalization state items to retrieve.</param>
		/// <param name="pathToMatch">The path to the page with personalization state items to retrieve.</param>
		/// <exception cref="T:System.ArgumentException">The provider for a personalization provider defined in configuration is not of the correct type.- or -<paramref name="pathToMatch" />, after trimming, is an empty string ("").- or - The length of the string of any parameter is greater than 256 characters.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration exception occurred while attempting to create and initialize an instance of one of the configured personalization providers.- or -The default personalization provider defined in configuration could not be found.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="scope" /> is not a valid member of the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationScope" /> enumeration.</exception>
		/// <exception cref="T:System.Web.HttpException">The default provider returned a negative number for the count.</exception>
		// Token: 0x06004F42 RID: 20290 RVA: 0x000C8D38 File Offset: 0x000C6F38
		public static int GetCountOfState(PersonalizationScope scope, string pathToMatch)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}

		/// <summary>Returns a count of the personalization state items in the underlying data store that exist for the specified user.</summary>
		/// <returns>The number of personalization state items for the specified user.</returns>
		/// <param name="usernameToMatch">The user name associated with the personalization state information to retrieve.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="usernameToMatch" /> is an empty string ("") after trimming.- or -The provider for a personalization provider defined in configuration is not of the correct type.- or - The length of the string of any parameter is greater than 256 characters.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration exception occurred while attempting to create and initialize an instance of one of the configured personalization providers.- or -The default personalization provider defined in configuration could not be found.</exception>
		/// <exception cref="T:System.Web.HttpException">The default provider returned a negative number for the count.</exception>
		// Token: 0x06004F43 RID: 20291 RVA: 0x000C8D54 File Offset: 0x000C6F54
		public static int GetCountOfUserState(string usernameToMatch)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}

		/// <summary>Resets all personalization data in the underlying data store by deleting all rows associated with the specified scope.</summary>
		/// <returns>The number of items that were reset.</returns>
		/// <param name="scope">The scope associated with the personalization data to be deleted.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="scope" /> is not a valid member of the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationScope" /> enumeration.</exception>
		/// <exception cref="T:System.ArgumentException">The provider for a personalization provider defined in configuration is not of the correct type.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration exception occurred while attempting to create and initialize an instance of one of the configured personalization providers.- or -The default personalization provider defined in configuration could not be found.</exception>
		/// <exception cref="T:System.Web.HttpException">The default provider returned a negative number for the count of affected rows.</exception>
		// Token: 0x06004F44 RID: 20292 RVA: 0x000C8D70 File Offset: 0x000C6F70
		public static int ResetAllState(PersonalizationScope scope)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}

		/// <summary>Resets all per-user personalization state information in the underlying data store, based on the specified parameter.</summary>
		/// <returns>The number of rows in the underlying data store that were reset.</returns>
		/// <param name="userInactiveSinceDate">The last date a user's personalization information was accessed.</param>
		/// <exception cref="T:System.ArgumentException">The provider for a personalization provider defined in configuration is not of the correct type.- or - The length of the string of any parameter is greater than 256 characters.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration exception occurred while attempting to create and initialize an instance of one of the configured personalization providers.- or -The default personalization provider defined in configuration could not be found.</exception>
		/// <exception cref="T:System.Web.HttpException">The default provider returned a negative number for the count.</exception>
		// Token: 0x06004F45 RID: 20293 RVA: 0x000C8D8C File Offset: 0x000C6F8C
		public static int ResetInactiveUserState(DateTime userInactiveSinceDate)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}

		/// <summary>Resets all per-user state information in the underlying data store for inactive users, based on the specified parameters.</summary>
		/// <returns>The number of rows in the underlying data store that were reset.</returns>
		/// <param name="path">The path to the page associated with the personalization state information to be reset.</param>
		/// <param name="userInactiveSinceDate">The last active date to be used in resetting user state personalization items.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string ("") after trimming.- or -The provider for a personalization provider defined in configuration is not of the correct type.- or - The length of the string of any parameter is greater than 256 characters.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration exception occurred while attempting to create and initialize an instance of one of the configured personalization providers.- or -The default personalization provider defined in configuration could not be found.</exception>
		/// <exception cref="T:System.Web.HttpException">The default provider returned a negative number for the count.</exception>
		// Token: 0x06004F46 RID: 20294 RVA: 0x000C8DA8 File Offset: 0x000C6FA8
		public static int ResetInactiveUserState(string path, DateTime userInactiveSinceDate)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}

		/// <summary>Resets shared state in the underlying data store for the specified path.</summary>
		/// <returns>true if personalization shared state was reset; otherwise, false.</returns>
		/// <param name="path">The path to the page associated with the personalization state information to be reset.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string ("") after trimming.- or -The provider for a personalization provider defined in configuration is not of the correct type.- or - The length of the string of any parameter is greater than 256 characters.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration exception occurred while attempting to create and initialize an instance of one of the configured personalization providers.- or -The default personalization provider defined in configuration could not be found.</exception>
		/// <exception cref="T:System.Web.HttpException">The default provider indicated that the number of deleted rows was either a negative number or was greater than one.</exception>
		// Token: 0x06004F47 RID: 20295 RVA: 0x000C8DC4 File Offset: 0x000C6FC4
		public static bool ResetSharedState(string path)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Resets shared state in the underlying data store for the specified paths.</summary>
		/// <returns>The number of rows that were reset.</returns>
		/// <param name="paths">The paths to the pages associated with the personalization state information to be reset.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="paths" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="paths" /> is a zero-length array.- or -A member of <paramref name="paths" /> is either null or an empty string ("") after trimming.- or -The provider for a personalization provider defined in configuration is not of the correct type.- or - The length of the string of any parameter is greater than 256 characters.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration exception occurred while attempting to create and initialize an instance of one of the configured personalization providers.- or -The default personalization provider defined in configuration could not be found.</exception>
		/// <exception cref="T:System.Web.HttpException">The default provider indicated that the number of deleted rows was a negative number.</exception>
		// Token: 0x06004F48 RID: 20296 RVA: 0x000C8DE0 File Offset: 0x000C6FE0
		public static int ResetSharedState(string[] paths)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}

		/// <summary>Resets personalization data in the underlying data store, based on the items contained in the collection.</summary>
		/// <returns>The number of rows that were reset.</returns>
		/// <param name="data">A collection of <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationStateInfo" /> objects indicating what data should be reset.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="data" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="data" /> is an empty collection.- or -An element of the collection is null.- or -The path value of a <see cref="T:System.Web.UI.WebControls.WebParts.UserPersonalizationStateInfo" /> instance in the collection is null or an empty string ("").- or -The <paramref name="user" /> value of a <see cref="T:System.Web.UI.WebControls.WebParts.UserPersonalizationStateInfo" /> instance in the collection is null, an empty string, or contains commas.- or -The provider for a personalization provider defined in configuration is not of the correct type.- or - The length of the string of any parameter is greater than 256 characters.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration exception occurred while attempting to create and initialize an instance of one of the configured personalization providers.- or -The default personalization provider defined in configuration could not be found.</exception>
		/// <exception cref="T:System.Web.HttpException">The default provider indicated that the number of deleted rows was a negative number.</exception>
		// Token: 0x06004F49 RID: 20297 RVA: 0x000C8DFC File Offset: 0x000C6FFC
		public static int ResetState(PersonalizationStateInfoCollection data)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}

		/// <summary>Resets all per-user state in the underlying data store for the specified path.</summary>
		/// <returns>The number of rows that were reset.</returns>
		/// <param name="path">The path to the page associated with the personalization state information to be reset.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string ("").- or -The provider for a personalization provider defined in configuration is not of the correct type.- or -The length of the string of any parameter is greater than 256 characters.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration exception occurred while attempting to create and initialize an instance of one of the configured personalization providers.- or -The default personalization provider defined in configuration could not be found.</exception>
		/// <exception cref="T:System.Web.HttpException">The default provider indicated that the number of deleted rows was a negative number.</exception>
		// Token: 0x06004F4A RID: 20298 RVA: 0x000C8E18 File Offset: 0x000C7018
		public static int ResetUserState(string path)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}

		/// <summary>Resets per-user state in the underlying data store for the specified combination of user name and path.</summary>
		/// <returns>The number of rows that were reset.</returns>
		/// <param name="path">The path to the page associated with the personalization state information to be reset.</param>
		/// <param name="username">The user name associated with the personalization data to be reset.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> or <paramref name="username" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> or <paramref name="username" /> is an empty string ("") after trimming.- or -<paramref name="username" /> contains commas.- or -The provider for a personalization provider defined in configuration is not of the correct type.- or - The length of the string of any parameter is greater than 256 characters.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration exception occurred while attempting to create and initialize an instance of one of the configured personalization providers.- or -The default personalization provider defined in configuration could not be found.</exception>
		/// <exception cref="T:System.Web.HttpException">The default provider indicated that the number of deleted rows was a negative number.</exception>
		// Token: 0x06004F4B RID: 20299 RVA: 0x000C8E34 File Offset: 0x000C7034
		public static bool ResetUserState(string path, string username)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Resets per-user state in the underlying data store for the page and users specified.</summary>
		/// <returns>The number of rows that were reset.</returns>
		/// <param name="path">The path to the page associated with the personalization state information to be reset.</param>
		/// <param name="usernames">The user names associated with the personalization data to be reset.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> or <paramref name="usernames" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string ("") after trimming.- or -A member of <paramref name="usernames" /> is null, an empty string after trimming, or contains commas.- or -<paramref name="usernames" /> is a zero-length array.- or -The provider for a personalization provider defined in configuration is not of the correct type.- or -The length of the string of any parameter is greater than 256 characters.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration exception occurred while attempting to create and initialize an instance of one of the configured personalization providers.- or -The default personalization provider defined in configuration could not be found.</exception>
		/// <exception cref="T:System.Web.HttpException">The default provider indicated that the number of deleted rows was a negative number.</exception>
		// Token: 0x06004F4C RID: 20300 RVA: 0x000C8E50 File Offset: 0x000C7050
		public static int ResetUserState(string path, string[] usernames)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}

		/// <summary>Resets all per-user state in the underlying data store for the specified paths.</summary>
		/// <returns>The number of rows that were reset.</returns>
		/// <param name="usernames">An array of user names whose per-user data should be reset.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="usernames" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="usernames" /> is a zero-length array.- or -A member of <paramref name="usernames" /> is either null, contains commas, or is an empty string ("") after trimming.- or -The provider for a personalization provider defined in configuration is not of the correct type.- or -The length of the string of any parameter is greater than 256 characters.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration exception occurred while attempting to create and initialize an instance of one of the configured personalization providers.- or -The default personalization provider defined in configuration could not be found.</exception>
		/// <exception cref="T:System.Web.HttpException">The default provider indicated that the number of deleted rows was a negative number.</exception>
		// Token: 0x06004F4D RID: 20301 RVA: 0x000C8E6C File Offset: 0x000C706C
		public static int ResetUserState(string[] usernames)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}
	}
}
