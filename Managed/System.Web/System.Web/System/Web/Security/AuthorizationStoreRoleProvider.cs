using System;
using Unity;

namespace System.Web.Security
{
	/// <summary>Manages storage of role-membership information for an ASP.NET application in an authorization-manager policy store, either in an XML file, in an Active Directory, or on an Active Directory Application Mode server.</summary>
	// Token: 0x020006E5 RID: 1765
	public class AuthorizationStoreRoleProvider : RoleProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Security.AuthorizationStoreRoleProvider" /> class. </summary>
		// Token: 0x06004AA3 RID: 19107 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public AuthorizationStoreRoleProvider()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets or sets the name of the authorization store application for which to store and retrieve role information.</summary>
		/// <returns>The name of the authorization store application for which to store and retrieve role information. The default is the <see cref="P:System.Web.HttpRequest.ApplicationPath" /> property value for the current <see cref="P:System.Web.HttpContext.Request" />.</returns>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">An attempt was made to set the <see cref="P:System.Web.Security.AuthorizationStoreRoleProvider.ApplicationName" /> to a string that is longer than 256 characters.</exception>
		// Token: 0x17001729 RID: 5929
		// (get) Token: 0x06004AA4 RID: 19108 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004AA5 RID: 19109 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public override string ApplicationName
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

		/// <summary>Gets the number of minutes between refreshes of the cache of the policy-store data.</summary>
		/// <returns>The number of minutes between refreshes of cached policy-store data. The default is 60.</returns>
		// Token: 0x1700172A RID: 5930
		// (get) Token: 0x06004AA6 RID: 19110 RVA: 0x000C7F70 File Offset: 0x000C6170
		public int CacheRefreshInterval
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets or sets the scope name for the authorization store.</summary>
		/// <returns>The scope name for the authorization store.</returns>
		// Token: 0x1700172B RID: 5931
		// (get) Token: 0x06004AA7 RID: 19111 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004AA8 RID: 19112 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public string ScopeName
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

		/// <summary>Adds the specified user names to each of the specified roles.</summary>
		/// <param name="usernames">A string array of user names to be added to the specified roles. </param>
		/// <param name="roleNames">A string array of role names to add the specified user names to. </param>
		/// <exception cref="T:System.ArgumentNullException">One of the specified user names is null.-or-One of the specified role names is null.-or-<paramref name="usernames" /> is null.-or-<paramref name="roleNames" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">One of the specified user names is an empty string or contains a comma (,).-or-One of the specified role names is an empty string or contains a comma (,).-or-<paramref name="usernames" /> contains a duplicate element.-or-<paramref name="roleNames" /> contains a duplicate element.</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">The configured applicationName was not found.-or-The configured scopeName was not found.-or-The authorization-manager runtime is not installed on the server.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The connectionStringName attribute references a connection string to a file that does not exist.</exception>
		/// <exception cref="T:System.Web.HttpException">The <see cref="T:System.Web.Security.AuthorizationStoreRoleProvider" /> instance is configured with a file-based policy store, and read access to the file is not allowed at the current trust level.</exception>
		// Token: 0x06004AA9 RID: 19113 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public override void AddUsersToRoles(string[] usernames, string[] roleNames)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Adds a new role to the role authorization-manager policy store.</summary>
		/// <param name="roleName">The name of the role to create. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="roleName" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="roleName" /> is an empty string.-or-<paramref name="roleName" /> contains a comma.</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">The configured applicationName was not found.-or-The configured scopeName was not found.-or-The authorization-manager runtime is not installed on the server.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The connectionStringName attribute references a connection string to a file that does not exist.</exception>
		/// <exception cref="T:System.Web.HttpException">The <see cref="T:System.Web.Security.AuthorizationStoreRoleProvider" /> instance is configured with a file-based policy store, and read access to the file is not allowed at the current trust level.</exception>
		// Token: 0x06004AAA RID: 19114 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public override void CreateRole(string roleName)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Removes a role from the authorization-manager policy store.</summary>
		/// <returns>true if the role was deleted; otherwise, false.</returns>
		/// <param name="roleName">The name of the role to delete.</param>
		/// <param name="throwOnPopulatedRole">If true, throws an exception if <paramref name="roleName" /> has one or more members.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="roleName" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="roleName" /> is an empty string.-or-<paramref name="roleName" /> contains a comma.</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">
		///   <paramref name="roleName" /> has one or more members and <paramref name="throwOnPopulatedRole" /> is true.-or-The configured applicationName was not found.-or-The configured scopeName was not found.-or-The authorization-manager runtime is not installed on the server.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The connectionStringName attribute references a connection string to a file that does not exist.</exception>
		/// <exception cref="T:System.Web.HttpException">The <see cref="T:System.Web.Security.AuthorizationStoreRoleProvider" /> instance is configured with a file-based policy store, and read access to the file is not allowed at the current trust level.</exception>
		// Token: 0x06004AAB RID: 19115 RVA: 0x000C7F8C File Offset: 0x000C618C
		public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>This method is not supported by the authorization store role provider.</summary>
		/// <returns>A string array containing the names of all the users whose user name matches <paramref name="usernameToMatch" /> and who are members of the specified role.</returns>
		/// <param name="roleName">The role to search in.</param>
		/// <param name="usernameToMatch">The user name to search for.</param>
		/// <exception cref="T:System.NotImplementedException">An unsupported method was called.</exception>
		// Token: 0x06004AAC RID: 19116 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public override string[] FindUsersInRole(string roleName, string usernameToMatch)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets a list of all the roles for the application.</summary>
		/// <returns>A string array containing the names of all the roles stored in the authorization-manager policy store for a particular application.</returns>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">The configured applicationName was not found.-or-The configured scopeName was not found.-or-The authorization-manager runtime is not installed on the server.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The connectionStringName attribute references a connection string to a file that does not exist.</exception>
		/// <exception cref="T:System.Web.HttpException">The <see cref="T:System.Web.Security.AuthorizationStoreRoleProvider" /> instance is configured with a file-based policy store, and read access to the file is not allowed at the current trust level.</exception>
		// Token: 0x06004AAD RID: 19117 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public override string[] GetAllRoles()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets a list of the roles that a user is in.</summary>
		/// <returns>A string array containing the names of all the roles that the specified user is in.</returns>
		/// <param name="username">The user to return a list of roles for. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="username" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="username" /> contains a comma.</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">The configured applicationName was not found.-or-The configured scopeName was not found.-or-The authorization-manager runtime is not installed on the server.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The connectionStringName attribute references a connection string to a file that does not exist.</exception>
		/// <exception cref="T:System.Web.HttpException">The <see cref="T:System.Web.Security.AuthorizationStoreRoleProvider" /> instance is configured with a file-based policy store, and read access to the file is not allowed at the current trust level.</exception>
		// Token: 0x06004AAE RID: 19118 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public override string[] GetRolesForUser(string username)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets a list of users in the specified role.</summary>
		/// <returns>A string array containing the names of all the users who are members of the specified role.</returns>
		/// <param name="roleName">The name of the role to get the list of users for. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="roleName" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="roleName" /> is an empty string.-or-<paramref name="roleName" /> contains a comma.</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">The configured applicationName was not found.-or-The configured scopeName was not found.-or-The authorization-manager runtime is not installed on the server.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The connectionStringName attribute references a connection string to a file that does not exist.</exception>
		/// <exception cref="T:System.Web.HttpException">The <see cref="T:System.Web.Security.AuthorizationStoreRoleProvider" /> instance is configured with a file-based policy store, and read access to the file is not allowed at the current trust level.</exception>
		// Token: 0x06004AAF RID: 19119 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public override string[] GetUsersInRole(string roleName)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets a value indicating whether the specified user is in the specified role.</summary>
		/// <returns>true if the specified user name is in the specified role; otherwise, false.</returns>
		/// <param name="username">The user name to search for. </param>
		/// <param name="roleName">The role to search in. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="username" /> is null.-or<paramref name="roleName" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="roleName" /> is an empty string.-or-<paramref name="roleName" /> contains a comma. -or-<paramref name="username" /> contains a comma.</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">The configured applicationName was not found.-or-The configured scopeName was not found.-or-The authorization-manager runtime is not installed on the server.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The connectionStringName attribute references a connection string to a file that does not exist.</exception>
		/// <exception cref="T:System.Web.HttpException">The <see cref="T:System.Web.Security.AuthorizationStoreRoleProvider" /> instance is configured with a file-based policy store, and read access to the file is not allowed at the current trust level.</exception>
		// Token: 0x06004AB0 RID: 19120 RVA: 0x000C7FA8 File Offset: 0x000C61A8
		public override bool IsUserInRole(string username, string roleName)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Removes the specified user names from the specified roles.</summary>
		/// <param name="userNames">A string array of user names to be removed from the specified roles. </param>
		/// <param name="roleNames">A string array of role names to remove the specified user names from. </param>
		/// <exception cref="T:System.ArgumentNullException">One of the specified user names is null.-or-One of the specified role names is null.-or-<paramref name="userNames" /> is null.-or-<paramref name="roleNames" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">One of the specified user names is an empty string or contains a comma.-or-One of the specified role names is an empty string or contains a comma.-or-<paramref name="userNames" /> contains a duplicate element.-or-<paramref name="roleNames" /> contains a duplicate element.</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">The configured applicationName was not found.-or-The configured scopeName was not found.-or-The authorization-manager runtime is not installed on the server.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The connectionStringName attribute references a connection string to a file that does not exist.</exception>
		/// <exception cref="T:System.Web.HttpException">The <see cref="T:System.Web.Security.AuthorizationStoreRoleProvider" /> instance is configured with a file-based policy store, and read access to the file is not allowed at the current trust level.</exception>
		// Token: 0x06004AB1 RID: 19121 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public override void RemoveUsersFromRoles(string[] userNames, string[] roleNames)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a value indicating whether the specified role name already exists in the authorization-manager policy store.</summary>
		/// <returns>true if the role name already exists in the authorization-manager policy store; otherwise, false.</returns>
		/// <param name="roleName">The name of the role to search for in the authorization-manager policy store. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="roleName" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="roleName" /> is an empty string.-or-<paramref name="roleName" /> contains a comma.</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">The configured applicationName was not found.-or-The configured scopeName was not found.-or-The authorization-manager runtime is not installed on the server.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The connectionStringName attribute references a connection string to a file that does not exist.</exception>
		/// <exception cref="T:System.Web.HttpException">The <see cref="T:System.Web.Security.AuthorizationStoreRoleProvider" /> instance is configured with a file-based policy store, and read access to the file is not allowed at the current trust level.</exception>
		// Token: 0x06004AB2 RID: 19122 RVA: 0x000C7FC4 File Offset: 0x000C61C4
		public override bool RoleExists(string roleName)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}
	}
}
