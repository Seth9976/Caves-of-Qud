using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Security.Principal;

namespace System.Web.UI.WebControls
{
	/// <summary>Associates a content template in a <see cref="T:System.Web.UI.WebControls.LoginView" /> control with one or more roles defined for the Web site. This class cannot be inherited.</summary>
	// Token: 0x020003FD RID: 1021
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class RoleGroup
	{
		/// <summary>Gets or sets the content template associated with this role group.</summary>
		/// <returns>The <see cref="T:System.Web.UI.ITemplate" /> associated with this role group. The default value is null.</returns>
		// Token: 0x17000E85 RID: 3717
		// (get) Token: 0x06002D4A RID: 11594 RVA: 0x000761AC File Offset: 0x000743AC
		// (set) Token: 0x06002D4B RID: 11595 RVA: 0x000761B4 File Offset: 0x000743B4
		[Browsable(false)]
		[TemplateContainer(typeof(LoginView))]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ITemplate ContentTemplate
		{
			get
			{
				return this.contentTemplate;
			}
			set
			{
				this.contentTemplate = value;
			}
		}

		/// <summary>Gets or sets the roles associated with this role group.</summary>
		/// <returns>A comma-separated list of roles associated with this role group. The default is null.</returns>
		// Token: 0x17000E86 RID: 3718
		// (get) Token: 0x06002D4C RID: 11596 RVA: 0x000761BD File Offset: 0x000743BD
		// (set) Token: 0x06002D4D RID: 11597 RVA: 0x000761D9 File Offset: 0x000743D9
		[TypeConverter(typeof(StringArrayConverter))]
		public string[] Roles
		{
			get
			{
				if (this.roles == null)
				{
					this.roles = new string[0];
				}
				return this.roles;
			}
			set
			{
				this.roles = value;
			}
		}

		/// <summary>Indicates whether the specified user is a member of any of the roles in the role group.</summary>
		/// <returns>true if the user is a member of one of the roles associated with this role group; otherwise, false.</returns>
		/// <param name="user">The user name to look for in the role group. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="User" /> is null.</exception>
		// Token: 0x06002D4E RID: 11598 RVA: 0x000761E4 File Offset: 0x000743E4
		public bool ContainsUser(IPrincipal user)
		{
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			if (this.roles != null)
			{
				foreach (string text in this.roles)
				{
					if (user.IsInRole(text))
					{
						return true;
					}
				}
			}
			return false;
		}

		/// <summary>Returns a comma-separated list of the roles associated with this role group.</summary>
		/// <returns>A comma-separated list of the roles associated with this role group.</returns>
		// Token: 0x06002D4F RID: 11599 RVA: 0x0007622C File Offset: 0x0007442C
		public override string ToString()
		{
			if (this.roles == null || this.roles.Length == 0)
			{
				return string.Empty;
			}
			if (this.roles.Length == 1)
			{
				return this.roles[0];
			}
			return string.Join(",", this.roles);
		}

		// Token: 0x04001B76 RID: 7030
		private ITemplate contentTemplate;

		// Token: 0x04001B77 RID: 7031
		private string[] roles;
	}
}
