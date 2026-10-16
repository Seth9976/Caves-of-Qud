using System;
using System.Configuration;
using System.Configuration.Provider;
using System.Threading;
using System.Web.Configuration;

namespace System.Web.Security
{
	/// <summary>Manages user membership in roles for authorization checking in an ASP.NET application. This class cannot be inherited.</summary>
	// Token: 0x020004C7 RID: 1223
	public static class Roles
	{
		/// <summary>Adds the specified users to the specified role.</summary>
		/// <param name="usernames">A string array of user names to add to the specified role. </param>
		/// <param name="roleName">The role to add the specified user names to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="roleName" /> is null.-or-One of the elements in <paramref name="usernames" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="roleName" /> is an empty string or contains a comma (,).-or-One of the elements in <paramref name="usernames" /> is an empty string or contains a comma (,).-or-<paramref name="usernames" /> contains a duplicate element.</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">Role management is not enabled.</exception>
		// Token: 0x06003743 RID: 14147 RVA: 0x0008E6DD File Offset: 0x0008C8DD
		public static void AddUsersToRole(string[] usernames, string roleName)
		{
			Roles.Provider.AddUsersToRoles(usernames, new string[] { roleName });
		}

		/// <summary>Adds the specified users to the specified roles.</summary>
		/// <param name="usernames">A string array of user names to add to the specified roles. </param>
		/// <param name="roleNames">A string array of role names to add the specified user names to. </param>
		/// <exception cref="T:System.ArgumentNullException">One of the roles in <paramref name="roleNames" /> is null.-or-One of the users in <paramref name="usernames" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">One of the roles in <paramref name="roleNames" /> is an empty string or contains a comma (,).-or-One of the users in <paramref name="usernames" /> is an empty string or contains a comma (,).-or-<paramref name="roleNames" /> contains a duplicate element.-or-<paramref name="usernames" /> contains a duplicate element.</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">Role management is not enabled.</exception>
		// Token: 0x06003744 RID: 14148 RVA: 0x0008E6F4 File Offset: 0x0008C8F4
		public static void AddUsersToRoles(string[] usernames, string[] roleNames)
		{
			Roles.Provider.AddUsersToRoles(usernames, roleNames);
		}

		/// <summary>Adds the specified user to the specified role.</summary>
		/// <param name="username">The user name to add to the specified role.</param>
		/// <param name="roleName">The role to add the specified user name to. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="roleName" /> is null.-or-<paramref name="username" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="roleName" /> is an empty string or contains a comma (,).-or-<paramref name="username" /> is an empty string or contains a comma (,).</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">Role management is not enabled. -or-User is already assigned to the specified role.</exception>
		// Token: 0x06003745 RID: 14149 RVA: 0x0008E702 File Offset: 0x0008C902
		public static void AddUserToRole(string username, string roleName)
		{
			Roles.Provider.AddUsersToRoles(new string[] { username }, new string[] { roleName });
		}

		/// <summary>Adds the specified user to the specified roles.</summary>
		/// <param name="username">The user name to add to the specified roles. </param>
		/// <param name="roleNames">A string array of roles to add the specified user name to. </param>
		/// <exception cref="T:System.ArgumentNullException">One of the roles in <paramref name="roleNames" /> is null.-or-<paramref name="username" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">One of the roles in <paramref name="roleNames" /> is an empty string or contains a comma (,).-or-<paramref name="username" /> is an empty string or contains a comma (,).-or-<paramref name="roleNames" /> contains a duplicate element.</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">Role management is not enabled.</exception>
		// Token: 0x06003746 RID: 14150 RVA: 0x0008E722 File Offset: 0x0008C922
		public static void AddUserToRoles(string username, string[] roleNames)
		{
			Roles.Provider.AddUsersToRoles(new string[] { username }, roleNames);
		}

		/// <summary>Adds a new role to the data source.</summary>
		/// <param name="roleName">The name of the role to create. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="roleName" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="roleName" /> is an empty string.-or-<paramref name="roleName" /> contains a comma.</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">Role management is not enabled.</exception>
		// Token: 0x06003747 RID: 14151 RVA: 0x0008E739 File Offset: 0x0008C939
		public static void CreateRole(string roleName)
		{
			Roles.Provider.CreateRole(roleName);
		}

		/// <summary>Deletes the cookie where role names are cached.</summary>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">Role management is not enabled.</exception>
		// Token: 0x06003748 RID: 14152 RVA: 0x0008E748 File Offset: 0x0008C948
		public static void DeleteCookie()
		{
			if (Roles.CacheRolesInCookie)
			{
				HttpContext httpContext = HttpContext.Current;
				if (httpContext == null)
				{
					throw new HttpException("Context is null.");
				}
				HttpResponse response = httpContext.Response;
				if (response == null)
				{
					throw new HttpException("Response is null.");
				}
				HttpCookieCollection cookies = response.Cookies;
				cookies.Remove(Roles.CookieName);
				cookies.Add(new HttpCookie(Roles.CookieName, "")
				{
					Expires = new DateTime(1999, 10, 12),
					Path = Roles.CookiePath
				});
			}
		}

		/// <summary>Removes a role from the data source.</summary>
		/// <returns>true if <paramref name="roleName" /> was deleted from the data source; otherwise, false.</returns>
		/// <param name="roleName">The name of the role to delete. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="roleName" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="roleName" /> is an empty string or contains a comma (,).</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">
		///   <paramref name="roleName" /> has one or more members.-or-Role management is not enabled.</exception>
		// Token: 0x06003749 RID: 14153 RVA: 0x0008E7C7 File Offset: 0x0008C9C7
		public static bool DeleteRole(string roleName)
		{
			return Roles.Provider.DeleteRole(roleName, true);
		}

		/// <summary>Removes a role from the data source.</summary>
		/// <returns>true if <paramref name="roleName" /> was deleted from the data source; otherwise; false.</returns>
		/// <param name="roleName">The name of the role to delete.</param>
		/// <param name="throwOnPopulatedRole">If true, throws an exception if <paramref name="roleName" /> has one or more members.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="roleName" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="roleName" /> is an empty string.</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">
		///   <paramref name="roleName" /> has one or more members and <paramref name="throwOnPopulatedRole" /> is true.-or-Role management is not enabled.</exception>
		// Token: 0x0600374A RID: 14154 RVA: 0x0008E7D5 File Offset: 0x0008C9D5
		public static bool DeleteRole(string roleName, bool throwOnPopulatedRole)
		{
			return Roles.Provider.DeleteRole(roleName, throwOnPopulatedRole);
		}

		/// <summary>Gets a list of all the roles for the application.</summary>
		/// <returns>A string array containing the names of all the roles stored in the data source for the application.</returns>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">Role management is not enabled.</exception>
		// Token: 0x0600374B RID: 14155 RVA: 0x0008E7E3 File Offset: 0x0008C9E3
		public static string[] GetAllRoles()
		{
			return Roles.Provider.GetAllRoles();
		}

		/// <summary>Gets a list of the roles that the currently logged-on user is in.</summary>
		/// <returns>A string array containing the names of all the roles that the currently logged-on user is in.</returns>
		/// <exception cref="T:System.ArgumentNullException">There is no current logged-on user.</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">Role management is not enabled.</exception>
		// Token: 0x0600374C RID: 14156 RVA: 0x0008E7EF File Offset: 0x0008C9EF
		public static string[] GetRolesForUser()
		{
			return Roles.Provider.GetRolesForUser(Roles.CurrentUser);
		}

		// Token: 0x17001163 RID: 4451
		// (get) Token: 0x0600374D RID: 14157 RVA: 0x0008E800 File Offset: 0x0008CA00
		private static string CurrentUser
		{
			get
			{
				if (HttpContext.Current != null && HttpContext.Current.User != null)
				{
					return HttpContext.Current.User.Identity.Name;
				}
				return Thread.CurrentPrincipal.Identity.Name;
			}
		}

		/// <summary>Gets a list of the roles that a user is in.</summary>
		/// <returns>A string array containing the names of all the roles that the specified user is in.</returns>
		/// <param name="username">The user to return a list of roles for. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="username" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="username" /> contains a comma (,). </exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">Role management is not enabled.</exception>
		// Token: 0x0600374E RID: 14158 RVA: 0x0008E839 File Offset: 0x0008CA39
		public static string[] GetRolesForUser(string username)
		{
			return Roles.Provider.GetRolesForUser(username);
		}

		/// <summary>Gets a list of users in the specified role.</summary>
		/// <returns>A string array containing the names of all the users who are members of the specified role.</returns>
		/// <param name="roleName">The role to get the list of users for. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="roleName" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="roleName" /> is an empty string or contains a comma (,).</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">Role management is not enabled.</exception>
		// Token: 0x0600374F RID: 14159 RVA: 0x0008E846 File Offset: 0x0008CA46
		public static string[] GetUsersInRole(string roleName)
		{
			return Roles.Provider.GetUsersInRole(roleName);
		}

		/// <summary>Gets a value indicating whether the currently logged-on user is in the specified role.</summary>
		/// <returns>true if the currently logged-on user is in the specified role; otherwise, false.</returns>
		/// <param name="roleName">The name of the role to search in. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="roleName" /> is null.-or-There is no current logged-on user.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="roleName" /> is an empty string or contains a comma (,).</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">Role management is not enabled.</exception>
		// Token: 0x06003750 RID: 14160 RVA: 0x0008E853 File Offset: 0x0008CA53
		public static bool IsUserInRole(string roleName)
		{
			return Roles.IsUserInRole(Roles.CurrentUser, roleName);
		}

		/// <summary>Gets a value indicating whether the specified user is in the specified role.</summary>
		/// <returns>true if the specified user is in the specified role; otherwise, false.</returns>
		/// <param name="username">The name of the user to search for. </param>
		/// <param name="roleName">The name of the role to search in. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="roleName" /> is null.-or-<paramref name="username" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="roleName" /> is an empty string or contains a comma (,).-or-<paramref name="username" /> contains a comma (,).</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">Role management is not enabled.</exception>
		// Token: 0x06003751 RID: 14161 RVA: 0x0008E860 File Offset: 0x0008CA60
		public static bool IsUserInRole(string username, string roleName)
		{
			return !string.IsNullOrEmpty(username) && Roles.Provider.IsUserInRole(username, roleName);
		}

		/// <summary>Removes the specified user from the specified role.</summary>
		/// <param name="username">The user to remove from the specified role.</param>
		/// <param name="roleName">The role to remove the specified user from.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="roleName" /> is null.-or-<paramref name="username" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="roleName" /> is an empty string or contains a comma (,)<paramref name="username" /> is an empty string or contains a comma (,).</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">Role management is not enabled.</exception>
		// Token: 0x06003752 RID: 14162 RVA: 0x0008E878 File Offset: 0x0008CA78
		public static void RemoveUserFromRole(string username, string roleName)
		{
			Roles.Provider.RemoveUsersFromRoles(new string[] { username }, new string[] { roleName });
		}

		/// <summary>Removes the specified user from the specified roles.</summary>
		/// <param name="username">The user to remove from the specified roles. </param>
		/// <param name="roleNames">A string array of role names to remove the specified user from. </param>
		/// <exception cref="T:System.ArgumentNullException">One of the roles in <paramref name="roleNames" /> is null.-or-<paramref name="username" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">One of the roles in <paramref name="roleNames" /> is an empty string or contains a comma (,).-or-<paramref name="username" /> is an empty string or contains a comma (,).-or-<paramref name="roleNames" /> contains a duplicate element.</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">Role management is not enabled.</exception>
		// Token: 0x06003753 RID: 14163 RVA: 0x0008E898 File Offset: 0x0008CA98
		public static void RemoveUserFromRoles(string username, string[] roleNames)
		{
			Roles.Provider.RemoveUsersFromRoles(new string[] { username }, roleNames);
		}

		/// <summary>Removes the specified users from the specified role.</summary>
		/// <param name="usernames">A string array of user names to remove from the specified roles. </param>
		/// <param name="roleName">The name of the role to remove the specified users from. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="roleName" /> is null.-or-One of the user names in <paramref name="usernames" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="roleName" /> is an empty string or contains a comma (,).-or-One of the user names in <paramref name="usernames" /> is an empty string or contains a comma (,).-or-<paramref name="usernames" /> contains a duplicate element.</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">Role management is not enabled.</exception>
		// Token: 0x06003754 RID: 14164 RVA: 0x0008E8AF File Offset: 0x0008CAAF
		public static void RemoveUsersFromRole(string[] usernames, string roleName)
		{
			Roles.Provider.RemoveUsersFromRoles(usernames, new string[] { roleName });
		}

		/// <summary>Removes the specified user names from the specified roles.</summary>
		/// <param name="usernames">A string array of user names to remove from the specified roles. </param>
		/// <param name="roleNames">A string array of role names to remove the specified users from. </param>
		/// <exception cref="T:System.ArgumentNullException">One of the roles specified in <paramref name="roleNames" /> is null.-or-One of the users specified in <paramref name="usernames" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">One of the roles specified in <paramref name="roleNames" /> is an empty string or contains a comma (,).-or-One of the users specified in <paramref name="usernames" /> is an empty string or contains a comma (,).-or-<paramref name="roleNames" /> contains a duplicate element.-or-<paramref name="usernames" /> contains a duplicate element.</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">Role management is not enabled.</exception>
		// Token: 0x06003755 RID: 14165 RVA: 0x0008E8C6 File Offset: 0x0008CAC6
		public static void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
		{
			Roles.Provider.RemoveUsersFromRoles(usernames, roleNames);
		}

		/// <summary>Gets a value indicating whether the specified role name already exists in the role data source.</summary>
		/// <returns>true if the role name already exists in the data source; otherwise, false.</returns>
		/// <param name="roleName">The name of the role to search for in the data source. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="roleName" /> is null (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="roleName" /> is an empty string or contains a comma (,).</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">Role management is not enabled.</exception>
		// Token: 0x06003756 RID: 14166 RVA: 0x0008E8D4 File Offset: 0x0008CAD4
		public static bool RoleExists(string roleName)
		{
			return Roles.Provider.RoleExists(roleName);
		}

		/// <summary>Gets a list of users in a specified role where the user name contains the specified user name to match.</summary>
		/// <returns>A string array containing the names of all the users whose user name matches <paramref name="usernameToMatch" /> and who are members of the specified role.</returns>
		/// <param name="roleName">The role to search in.</param>
		/// <param name="usernameToMatch">The user name to search for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="roleName" /> is null (Nothing in Visual Basic).-or-<paramref name="usernameToMatch" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="roleName" /> is an empty string or contains a comma (,).-or-<paramref name="usernameToMatch" /> is an empty string.</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">Role management is not enabled.</exception>
		// Token: 0x06003757 RID: 14167 RVA: 0x0008E8E1 File Offset: 0x0008CAE1
		public static string[] FindUsersInRole(string roleName, string usernameToMatch)
		{
			return Roles.Provider.FindUsersInRole(roleName, usernameToMatch);
		}

		/// <summary>Gets or sets the name of the application to store and retrieve role information for.</summary>
		/// <returns>The name of the application to store and retrieve role information for.</returns>
		// Token: 0x17001164 RID: 4452
		// (get) Token: 0x06003758 RID: 14168 RVA: 0x0008E8EF File Offset: 0x0008CAEF
		// (set) Token: 0x06003759 RID: 14169 RVA: 0x0008E8FB File Offset: 0x0008CAFB
		public static string ApplicationName
		{
			get
			{
				return Roles.Provider.ApplicationName;
			}
			set
			{
				Roles.Provider.ApplicationName = value;
			}
		}

		/// <summary>Gets a value indicating whether the current user's roles are cached in a cookie.</summary>
		/// <returns>true if the current user's roles are cached in a cookie; otherwise, false. The default is true.</returns>
		// Token: 0x17001165 RID: 4453
		// (get) Token: 0x0600375A RID: 14170 RVA: 0x0008E908 File Offset: 0x0008CB08
		public static bool CacheRolesInCookie
		{
			get
			{
				return Roles.config.CacheRolesInCookie;
			}
		}

		/// <summary>Gets the name of the cookie where role names are cached.</summary>
		/// <returns>The name of the cookie where role names are cached. The default is .ASPXROLES.</returns>
		// Token: 0x17001166 RID: 4454
		// (get) Token: 0x0600375B RID: 14171 RVA: 0x0008E914 File Offset: 0x0008CB14
		public static string CookieName
		{
			get
			{
				return Roles.config.CookieName;
			}
		}

		/// <summary>Gets the path for the cached role names cookie.</summary>
		/// <returns>The path of the cookie where role names are cached. The default is /.</returns>
		// Token: 0x17001167 RID: 4455
		// (get) Token: 0x0600375C RID: 14172 RVA: 0x0008E920 File Offset: 0x0008CB20
		public static string CookiePath
		{
			get
			{
				return Roles.config.CookiePath;
			}
		}

		/// <summary>Gets a value that indicates how role names cached in a cookie are protected.</summary>
		/// <returns>One of the <see cref="T:System.Web.Security.CookieProtection" /> enumeration values indicating how role names that are cached in a cookie are protected. The default is All.</returns>
		// Token: 0x17001168 RID: 4456
		// (get) Token: 0x0600375D RID: 14173 RVA: 0x0008E92C File Offset: 0x0008CB2C
		public static CookieProtection CookieProtectionValue
		{
			get
			{
				return Roles.config.CookieProtection;
			}
		}

		/// <summary>Gets a value indicating whether the role names cookie requires SSL in order to be returned to the server.</summary>
		/// <returns>true if SSL is required to return the role names cookie to the server; otherwise, false. The default is false.</returns>
		// Token: 0x17001169 RID: 4457
		// (get) Token: 0x0600375E RID: 14174 RVA: 0x0008E938 File Offset: 0x0008CB38
		public static bool CookieRequireSSL
		{
			get
			{
				return Roles.config.CookieRequireSSL;
			}
		}

		/// <summary>Indicates whether the role names cookie expiration date and time will be reset periodically.</summary>
		/// <returns>true if the role names cookie expiration date and time will be reset periodically; otherwise, false. The default is true.</returns>
		// Token: 0x1700116A RID: 4458
		// (get) Token: 0x0600375F RID: 14175 RVA: 0x0008E944 File Offset: 0x0008CB44
		public static bool CookieSlidingExpiration
		{
			get
			{
				return Roles.config.CookieSlidingExpiration;
			}
		}

		/// <summary>Gets the number of minutes before the roles cookie expires.</summary>
		/// <returns>An integer specifying the number of minutes before the roles cookie expires. The default is 30 minutes.</returns>
		// Token: 0x1700116B RID: 4459
		// (get) Token: 0x06003760 RID: 14176 RVA: 0x0008E950 File Offset: 0x0008CB50
		public static int CookieTimeout
		{
			get
			{
				return (int)Roles.config.CookieTimeout.TotalMinutes;
			}
		}

		/// <summary>Gets a value indicating whether the role-names cookie is session-based or persistent.</summary>
		/// <returns>true if the role-names cookie is a persistent cookie; otherwise false. The default is false.</returns>
		// Token: 0x1700116C RID: 4460
		// (get) Token: 0x06003761 RID: 14177 RVA: 0x0008E970 File Offset: 0x0008CB70
		public static bool CreatePersistentCookie
		{
			get
			{
				return Roles.config.CreatePersistentCookie;
			}
		}

		/// <summary>Gets the value of the domain of the role-names cookie.</summary>
		/// <returns>The <see cref="P:System.Web.HttpCookie.Domain" /> of the role names cookie.</returns>
		// Token: 0x1700116D RID: 4461
		// (get) Token: 0x06003762 RID: 14178 RVA: 0x0008E97C File Offset: 0x0008CB7C
		public static string Domain
		{
			get
			{
				return Roles.config.Domain;
			}
		}

		/// <summary>Gets or sets a value indicating whether role management is enabled for the current Web application.</summary>
		/// <returns>true if role management is enabled; otherwise, false. The default is false.</returns>
		// Token: 0x1700116E RID: 4462
		// (get) Token: 0x06003763 RID: 14179 RVA: 0x0008E988 File Offset: 0x0008CB88
		// (set) Token: 0x06003764 RID: 14180 RVA: 0x0008E994 File Offset: 0x0008CB94
		public static bool Enabled
		{
			get
			{
				return Roles.config.Enabled;
			}
			set
			{
				Roles.config.Enabled = value;
			}
		}

		/// <summary>Gets the maximum number of role names to be cached for a user.</summary>
		/// <returns>The maximum number of role names to be cached for a user. The default is 25.</returns>
		// Token: 0x1700116F RID: 4463
		// (get) Token: 0x06003765 RID: 14181 RVA: 0x0008E9A1 File Offset: 0x0008CBA1
		public static int MaxCachedResults
		{
			get
			{
				return Roles.config.MaxCachedResults;
			}
		}

		/// <summary>Gets the default role provider for the application.</summary>
		/// <returns>The default role provider for the application, which is exposed as a class that inherits the <see cref="T:System.Web.Security.RoleProvider" /> abstract class.</returns>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">Role management is not enabled.</exception>
		// Token: 0x17001170 RID: 4464
		// (get) Token: 0x06003766 RID: 14182 RVA: 0x0008E9AD File Offset: 0x0008CBAD
		public static RoleProvider Provider
		{
			get
			{
				RoleProvider roleProvider = Roles.Providers[Roles.config.DefaultProvider];
				if (roleProvider == null)
				{
					throw new ConfigurationErrorsException("Default Role Provider could not be found: Cannot instantiate provider: '" + Roles.config.DefaultProvider + "'.");
				}
				return roleProvider;
			}
		}

		/// <summary>Gets a collection of the role providers for the ASP.NET application.</summary>
		/// <returns>A <see cref="T:System.Web.Security.RoleProviderCollection" /> that contains the role providers configured for the ASP.NET application.</returns>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">Role management is not enabled.</exception>
		// Token: 0x17001171 RID: 4465
		// (get) Token: 0x06003767 RID: 14183 RVA: 0x0008E9E8 File Offset: 0x0008CBE8
		public static RoleProviderCollection Providers
		{
			get
			{
				Roles.CheckEnabled();
				if (Roles.providersCollection == null)
				{
					RoleProviderCollection roleProviderCollection = new RoleProviderCollection();
					ProvidersHelper.InstantiateProviders(Roles.config.Providers, roleProviderCollection, typeof(RoleProvider));
					Roles.providersCollection = roleProviderCollection;
				}
				return Roles.providersCollection;
			}
		}

		// Token: 0x06003768 RID: 14184 RVA: 0x0008EA2C File Offset: 0x0008CC2C
		private static void CheckEnabled()
		{
			if (!Roles.Enabled)
			{
				throw new ProviderException("This feature is not enabled.  To enable it, add <roleManager enabled=\"true\"> to your configuration file.");
			}
		}

		// Token: 0x04001DF1 RID: 7665
		private static RoleManagerSection config = (RoleManagerSection)WebConfigurationManager.GetSection("system.web/roleManager");

		// Token: 0x04001DF2 RID: 7666
		private static RoleProviderCollection providersCollection;
	}
}
