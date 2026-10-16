using System;
using System.Security;
using System.Security.Permissions;

namespace System.Web
{
	/// <summary>Controls access permissions in ASP.NET hosted environments. This class cannot be inherited.</summary>
	// Token: 0x020001E4 RID: 484
	[Serializable]
	public sealed class AspNetHostingPermission : CodeAccessPermission, IUnrestrictedPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.AspNetHostingPermission" /> class with the specified permission level.</summary>
		/// <param name="level">An <see cref="T:System.Web.AspNetHostingPermissionLevel" /> enumeration value. </param>
		// Token: 0x06000C93 RID: 3219 RVA: 0x000333E8 File Offset: 0x000315E8
		public AspNetHostingPermission(AspNetHostingPermissionLevel level)
		{
			this.Level = level;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.AspNetHostingPermission" /> class with the specified <see cref="T:System.Security.Permissions.PermissionState" /> enumeration value.</summary>
		/// <param name="state">A <see cref="T:System.Security.Permissions.PermissionState" /> enumeration value. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="state" /> is not set to one of the <see cref="T:System.Security.Permissions.PermissionState" /> enumeration values.</exception>
		// Token: 0x06000C94 RID: 3220 RVA: 0x000333F7 File Offset: 0x000315F7
		public AspNetHostingPermission(PermissionState state)
		{
			if (PermissionHelper.CheckPermissionState(state, true) == PermissionState.Unrestricted)
			{
				this._level = AspNetHostingPermissionLevel.Unrestricted;
				return;
			}
			this._level = AspNetHostingPermissionLevel.None;
		}

		/// <summary>Gets or sets the current hosting permission level for an ASP.NET application.</summary>
		/// <returns>One of the <see cref="T:System.Web.AspNetHostingPermissionLevel" /> enumeration values.</returns>
		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000C95 RID: 3221 RVA: 0x0003341D File Offset: 0x0003161D
		// (set) Token: 0x06000C96 RID: 3222 RVA: 0x00033425 File Offset: 0x00031625
		public AspNetHostingPermissionLevel Level
		{
			get
			{
				return this._level;
			}
			set
			{
				if (value < AspNetHostingPermissionLevel.None || value > AspNetHostingPermissionLevel.Unrestricted)
				{
					throw new ArgumentException(string.Format(global::Locale.GetText("Invalid enum {0}."), value), "Level");
				}
				this._level = value;
			}
		}

		/// <summary>Returns a value indicating whether unrestricted access to the resource that is protected by the current permission is allowed.</summary>
		/// <returns>true if unrestricted use of the resource protected by the permission is allowed; otherwise, false.</returns>
		// Token: 0x06000C97 RID: 3223 RVA: 0x0003345B File Offset: 0x0003165B
		public bool IsUnrestricted()
		{
			return this._level == AspNetHostingPermissionLevel.Unrestricted;
		}

		/// <summary>When implemented by a derived class, creates and returns an identical copy of the current permission object.</summary>
		/// <returns>A copy of the current permission object.</returns>
		// Token: 0x06000C98 RID: 3224 RVA: 0x0003346A File Offset: 0x0003166A
		public override IPermission Copy()
		{
			return new AspNetHostingPermission(this._level);
		}

		/// <summary>Reconstructs a permission object with a specified state from an XML encoding.</summary>
		/// <param name="securityElement">The <see cref="T:System.Security.SecurityElement" /> containing the XML encoding to use to reconstruct the permission object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="securityElement" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.SecurityElement.Tag" /> property of <paramref name="securityElement" /> is not equal to "IPermission". - or- The class <see cref="M:System.Security.SecurityElement.Attribute(System.String)" /> of <paramref name="securityElement" /> is null or an empty string (""). </exception>
		// Token: 0x06000C99 RID: 3225 RVA: 0x00033478 File Offset: 0x00031678
		public override void FromXml(SecurityElement securityElement)
		{
			PermissionHelper.CheckSecurityElement(securityElement, "securityElement", 1, 1);
			if (securityElement.Tag != "IPermission")
			{
				throw new ArgumentException(string.Format(global::Locale.GetText("Invalid tag '{0}' for permission."), securityElement.Tag), "securityElement");
			}
			if (securityElement.Attribute("version") == null)
			{
				throw new ArgumentException(global::Locale.GetText("Missing version attribute."), "securityElement");
			}
			if (PermissionHelper.IsUnrestricted(securityElement))
			{
				this._level = AspNetHostingPermissionLevel.Unrestricted;
				return;
			}
			string text = securityElement.Attribute("Level");
			if (text != null)
			{
				this._level = (AspNetHostingPermissionLevel)Enum.Parse(typeof(AspNetHostingPermissionLevel), text);
				return;
			}
			this._level = AspNetHostingPermissionLevel.None;
		}

		/// <summary>Creates an XML encoding of the permission object and its current state.</summary>
		/// <returns>A <see cref="T:System.Security.SecurityElement" /> containing the XML encoding of the permission object, including any state information.</returns>
		// Token: 0x06000C9A RID: 3226 RVA: 0x00033530 File Offset: 0x00031730
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = PermissionHelper.Element(typeof(AspNetHostingPermission), 1);
			if (this.IsUnrestricted())
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			securityElement.AddAttribute("Level", this._level.ToString());
			return securityElement;
		}

		/// <summary>When implemented by a derived class, creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <returns>An <see cref="T:System.Security.IPermission" /> that represents the intersection of the current permission and the specified permission; otherwise, null if the intersection is empty.</returns>
		/// <param name="target">A permission to combine with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not an <see cref="T:System.Web.AspNetHostingPermission" />. </exception>
		// Token: 0x06000C9B RID: 3227 RVA: 0x00033584 File Offset: 0x00031784
		public override IPermission Intersect(IPermission target)
		{
			AspNetHostingPermission aspNetHostingPermission = this.Cast(target);
			if (aspNetHostingPermission == null)
			{
				return null;
			}
			return new AspNetHostingPermission((this._level <= aspNetHostingPermission.Level) ? this._level : aspNetHostingPermission.Level);
		}

		/// <summary>Returns a value indicating whether the current permission is a subset of the specified permission.</summary>
		/// <returns>true if the current <see cref="T:System.Security.IPermission" /> is a subset of the specified <see cref="T:System.Security.IPermission" />; otherwise, false.</returns>
		/// <param name="target">The <see cref="T:System.Security.IPermission" /> to combine with the current permission. It must be of the same type as the current <see cref="T:System.Security.IPermission" />. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not an <see cref="T:System.Web.AspNetHostingPermission" />. </exception>
		// Token: 0x06000C9C RID: 3228 RVA: 0x000335C0 File Offset: 0x000317C0
		public override bool IsSubsetOf(IPermission target)
		{
			AspNetHostingPermission aspNetHostingPermission = this.Cast(target);
			if (aspNetHostingPermission == null)
			{
				return this.IsEmpty();
			}
			return this._level <= aspNetHostingPermission._level;
		}

		/// <summary>Creates a permission that is the union of the current permission and the specified permission.</summary>
		/// <returns>An <see cref="T:System.Security.IPermission" /> that represents the union of the current permission and the specified permission.</returns>
		/// <param name="target">A permission to combine with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not an <see cref="T:System.Web.AspNetHostingPermission" />. </exception>
		// Token: 0x06000C9D RID: 3229 RVA: 0x000335F0 File Offset: 0x000317F0
		public override IPermission Union(IPermission target)
		{
			AspNetHostingPermission aspNetHostingPermission = this.Cast(target);
			if (aspNetHostingPermission == null)
			{
				return this.Copy();
			}
			return new AspNetHostingPermission((this._level > aspNetHostingPermission.Level) ? this._level : aspNetHostingPermission.Level);
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x00033630 File Offset: 0x00031830
		private bool IsEmpty()
		{
			return this._level == AspNetHostingPermissionLevel.None;
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x0003363C File Offset: 0x0003183C
		private AspNetHostingPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			AspNetHostingPermission aspNetHostingPermission = target as AspNetHostingPermission;
			if (aspNetHostingPermission == null)
			{
				PermissionHelper.ThrowInvalidPermission(target, typeof(AspNetHostingPermission));
			}
			return aspNetHostingPermission;
		}

		// Token: 0x040007E2 RID: 2018
		private const int version = 1;

		// Token: 0x040007E3 RID: 2019
		private AspNetHostingPermissionLevel _level;
	}
}
