using System;
using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;
using System.Security.Principal;

namespace System.Web.UI.WebControls
{
	/// <summary>Contains a sequential list of role groups that the <see cref="T:System.Web.UI.WebControls.LoginView" /> control uses to determine which control template to display to users based on their role. This class cannot be inherited.</summary>
	// Token: 0x020003FE RID: 1022
	[Editor("System.Web.UI.Design.WebControls.RoleGroupCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class RoleGroupCollection : CollectionBase
	{
		/// <summary>Gets the role group at the specified index.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.RoleGroup" /> at the specified index.</returns>
		/// <param name="index">The index of the role group to return. </param>
		// Token: 0x17000E87 RID: 3719
		public RoleGroup this[int index]
		{
			get
			{
				return (RoleGroup)base.List[index];
			}
		}

		/// <summary>Adds a role group to the end of the collection.</summary>
		/// <param name="group">The <see cref="T:System.Web.UI.WebControls.RoleGroup" /> to add to the collection. </param>
		// Token: 0x06002D52 RID: 11602 RVA: 0x0007627C File Offset: 0x0007447C
		public void Add(RoleGroup group)
		{
			base.List.Add(group);
		}

		/// <summary>Indicates whether the collection contains the specified role group.</summary>
		/// <returns>true if the specified role group is a member of the collection; otherwise false.</returns>
		/// <param name="group">The <see cref="T:System.Web.UI.WebControls.RoleGroup" /> to look for in the collection. </param>
		// Token: 0x06002D53 RID: 11603 RVA: 0x0007628B File Offset: 0x0007448B
		public bool Contains(RoleGroup group)
		{
			return base.List.Contains(group);
		}

		/// <summary>Copies all the items from the <see cref="T:System.Web.UI.WebControls.RoleGroupCollection" /> collection to a compatible one-dimensional array of <see cref="T:System.Web.UI.WebControls.RoleGroup" /> objects, starting at the specified index in the target array.</summary>
		/// <param name="array">A zero-based array of <see cref="T:System.Web.UI.WebControls.RoleGroup" /> objects that receives the items copied from the collection.</param>
		/// <param name="index">The position in the target array at which the array starts receiving the copied items.</param>
		// Token: 0x06002D54 RID: 11604 RVA: 0x0007629C File Offset: 0x0007449C
		public void CopyTo(RoleGroup[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentException(global::Locale.GetText("Negative index."), "index");
			}
			if (base.Count <= array.Length - index)
			{
				throw new ArgumentException(global::Locale.GetText("Destination isn't large enough to copy collection."), "array");
			}
			for (int i = 0; i < base.Count; i++)
			{
				array[i + index] = this[i];
			}
		}

		/// <summary>Returns the first role group that contains the specified user account.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.RoleGroup" /> representing the first role group in the collection that contains the specified user account. If the user is not part of a role group in the collection, it returns null.</returns>
		/// <param name="user">An <see cref="T:System.Security.Principal.IPrincipal" /> that represents the user account to find the role group collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="user" /> is null.</exception>
		// Token: 0x06002D55 RID: 11605 RVA: 0x00076310 File Offset: 0x00074510
		public RoleGroup GetMatchingRoleGroup(IPrincipal user)
		{
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			if (base.Count > 0)
			{
				foreach (object obj in this)
				{
					RoleGroup roleGroup = (RoleGroup)obj;
					if (roleGroup.ContainsUser(user))
					{
						return roleGroup;
					}
				}
			}
			return null;
		}

		/// <summary>Searches the collection and returns the zero-based index of the first occurrence of the specified <see cref="T:System.Web.UI.WebControls.RoleGroup" />.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="group" /> within the entire <see cref="T:System.Web.UI.WebControls.RoleGroupCollection" />, if found; otherwise, -1.</returns>
		/// <param name="group">The <see cref="T:System.Web.UI.WebControls.RoleGroup" /> to locate in the collection. </param>
		// Token: 0x06002D56 RID: 11606 RVA: 0x00076384 File Offset: 0x00074584
		public int IndexOf(RoleGroup group)
		{
			return base.List.IndexOf(group);
		}

		/// <summary>Adds a <see cref="T:System.Web.UI.WebControls.RoleGroup" /> to the collection at the specified index.</summary>
		/// <param name="index">The zero-based index at which to insert the role group. </param>
		/// <param name="group">The role group to insert. </param>
		// Token: 0x06002D57 RID: 11607 RVA: 0x00076392 File Offset: 0x00074592
		public void Insert(int index, RoleGroup group)
		{
			base.List.Insert(index, group);
		}

		// Token: 0x06002D58 RID: 11608 RVA: 0x000763A1 File Offset: 0x000745A1
		protected override void OnValidate(object value)
		{
			base.OnValidate(value);
		}

		/// <summary>Deletes the first occurrence of the specified role group from the collection.</summary>
		/// <param name="group">The <see cref="T:System.Web.UI.WebControls.RoleGroup" /> to remove from the collection. </param>
		// Token: 0x06002D59 RID: 11609 RVA: 0x000763AA File Offset: 0x000745AA
		public void Remove(RoleGroup group)
		{
			if (group != null && this.Contains(group))
			{
				base.List.Remove(group);
			}
		}
	}
}
