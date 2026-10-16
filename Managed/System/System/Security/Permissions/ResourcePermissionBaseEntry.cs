using System;

namespace System.Security.Permissions
{
	/// <summary>Defines the smallest unit of a code access security permission set.</summary>
	// Token: 0x0200029A RID: 666
	[Serializable]
	public class ResourcePermissionBaseEntry
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.ResourcePermissionBaseEntry" /> class.</summary>
		// Token: 0x060014FE RID: 5374 RVA: 0x0005528F File Offset: 0x0005348F
		public ResourcePermissionBaseEntry()
		{
			this.permissionAccessPath = new string[0];
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.ResourcePermissionBaseEntry" /> class with the specified permission access and permission access path.</summary>
		/// <param name="permissionAccess">The integer representation of the permission access level enumeration value. The <see cref="P:System.Security.Permissions.ResourcePermissionBaseEntry.PermissionAccess" /> property is set to this value. </param>
		/// <param name="permissionAccessPath">The array of strings that identify the resource you are protecting. The <see cref="P:System.Security.Permissions.ResourcePermissionBaseEntry.PermissionAccessPath" /> property is set to this value. </param>
		/// <exception cref="T:System.ArgumentNullException">The specified <paramref name="permissionAccessPath" /> is null. </exception>
		// Token: 0x060014FF RID: 5375 RVA: 0x000552A3 File Offset: 0x000534A3
		public ResourcePermissionBaseEntry(int permissionAccess, string[] permissionAccessPath)
		{
			if (permissionAccessPath == null)
			{
				throw new ArgumentNullException("permissionAccessPath");
			}
			this.permissionAccess = permissionAccess;
			this.permissionAccessPath = permissionAccessPath;
		}

		/// <summary>Gets an integer representation of the access level enumeration value.</summary>
		/// <returns>The access level enumeration value.</returns>
		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06001500 RID: 5376 RVA: 0x000552C7 File Offset: 0x000534C7
		public int PermissionAccess
		{
			get
			{
				return this.permissionAccess;
			}
		}

		/// <summary>Gets an array of strings that identify the resource you are protecting.</summary>
		/// <returns>An array of strings that identify the resource you are protecting.</returns>
		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06001501 RID: 5377 RVA: 0x000552CF File Offset: 0x000534CF
		public string[] PermissionAccessPath
		{
			get
			{
				return this.permissionAccessPath;
			}
		}

		// Token: 0x04000BE3 RID: 3043
		private int permissionAccess;

		// Token: 0x04000BE4 RID: 3044
		private string[] permissionAccessPath;
	}
}
