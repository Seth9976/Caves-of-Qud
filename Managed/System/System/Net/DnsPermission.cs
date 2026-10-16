using System;
using System.Security;
using System.Security.Permissions;

namespace System.Net
{
	/// <summary>Controls rights to access Domain Name System (DNS) servers on the network.</summary>
	// Token: 0x02000681 RID: 1665
	[Serializable]
	public sealed class DnsPermission : CodeAccessPermission, IUnrestrictedPermission
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Net.DnsPermission" /> class that either allows unrestricted DNS access or disallows DNS access.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="state" /> is not a valid <see cref="T:System.Security.Permissions.PermissionState" /> value. </exception>
		// Token: 0x0600345E RID: 13406 RVA: 0x000B6553 File Offset: 0x000B4753
		public DnsPermission(PermissionState state)
		{
			this.m_noRestriction = state == PermissionState.Unrestricted;
		}

		/// <summary>Creates an identical copy of the current permission instance.</summary>
		/// <returns>A new instance of the <see cref="T:System.Net.DnsPermission" /> class that is an identical copy of the current instance.</returns>
		// Token: 0x0600345F RID: 13407 RVA: 0x000B6565 File Offset: 0x000B4765
		public override IPermission Copy()
		{
			return new DnsPermission(this.m_noRestriction ? PermissionState.Unrestricted : PermissionState.None);
		}

		/// <summary>Creates a permission instance that is the intersection of the current permission instance and the specified permission instance.</summary>
		/// <returns>A <see cref="T:System.Net.DnsPermission" /> instance that represents the intersection of the current <see cref="T:System.Net.DnsPermission" /> instance with the specified <see cref="T:System.Net.DnsPermission" /> instance, or null if the intersection is empty. If both the current instance and <paramref name="target" /> are unrestricted, this method returns a new <see cref="T:System.Net.DnsPermission" /> instance that is unrestricted; otherwise, it returns null.</returns>
		/// <param name="target">The <see cref="T:System.Net.DnsPermission" /> instance to intersect with the current instance. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is neither a <see cref="T:System.Net.DnsPermission" /> nor null. </exception>
		// Token: 0x06003460 RID: 13408 RVA: 0x000B6578 File Offset: 0x000B4778
		public override IPermission Intersect(IPermission target)
		{
			DnsPermission dnsPermission = this.Cast(target);
			if (dnsPermission == null)
			{
				return null;
			}
			if (this.IsUnrestricted() && dnsPermission.IsUnrestricted())
			{
				return new DnsPermission(PermissionState.Unrestricted);
			}
			return null;
		}

		/// <summary>Determines whether the current permission instance is a subset of the specified permission instance.</summary>
		/// <returns>false if the current instance is unrestricted and <paramref name="target" /> is either null or unrestricted; otherwise, true.</returns>
		/// <param name="target">The second <see cref="T:System.Net.DnsPermission" /> instance to be tested for the subset relationship. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is neither a <see cref="T:System.Net.DnsPermission" /> nor null. </exception>
		// Token: 0x06003461 RID: 13409 RVA: 0x000B65AC File Offset: 0x000B47AC
		public override bool IsSubsetOf(IPermission target)
		{
			DnsPermission dnsPermission = this.Cast(target);
			if (dnsPermission == null)
			{
				return this.IsEmpty();
			}
			return dnsPermission.IsUnrestricted() || this.m_noRestriction == dnsPermission.m_noRestriction;
		}

		/// <summary>Checks the overall permission state of the object.</summary>
		/// <returns>true if the <see cref="T:System.Net.DnsPermission" /> instance was created with <see cref="F:System.Security.Permissions.PermissionState.Unrestricted" />; otherwise, false.</returns>
		// Token: 0x06003462 RID: 13410 RVA: 0x000B65E3 File Offset: 0x000B47E3
		public bool IsUnrestricted()
		{
			return this.m_noRestriction;
		}

		/// <summary>Creates an XML encoding of a <see cref="T:System.Net.DnsPermission" /> instance and its current state.</summary>
		/// <returns>A <see cref="T:System.Security.SecurityElement" /> instance that contains an XML-encoded representation of the security object, including state information.</returns>
		// Token: 0x06003463 RID: 13411 RVA: 0x000B65EC File Offset: 0x000B47EC
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = PermissionHelper.Element(typeof(DnsPermission), 1);
			if (this.m_noRestriction)
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			return securityElement;
		}

		/// <summary>Reconstructs a <see cref="T:System.Net.DnsPermission" /> instance from an XML encoding.</summary>
		/// <param name="securityElement">The XML encoding to use to reconstruct the <see cref="T:System.Net.DnsPermission" /> instance. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="securityElement" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="securityElement" /> is not a <see cref="T:System.Net.DnsPermission" /> element. </exception>
		// Token: 0x06003464 RID: 13412 RVA: 0x000B6623 File Offset: 0x000B4823
		public override void FromXml(SecurityElement securityElement)
		{
			PermissionHelper.CheckSecurityElement(securityElement, "securityElement", 1, 1);
			if (securityElement.Tag != "IPermission")
			{
				throw new ArgumentException("securityElement");
			}
			this.m_noRestriction = PermissionHelper.IsUnrestricted(securityElement);
		}

		/// <summary>Creates a permission instance that is the union of the current permission instance and the specified permission instance.</summary>
		/// <returns>A <see cref="T:System.Net.DnsPermission" /> instance that represents the union of the current <see cref="T:System.Net.DnsPermission" /> instance with the specified <see cref="T:System.Net.DnsPermission" /> instance. If <paramref name="target" /> is null, this method returns a copy of the current instance. If the current instance or <paramref name="target" /> is unrestricted, this method returns a <see cref="T:System.Net.DnsPermission" /> instance that is unrestricted; otherwise, it returns a <see cref="T:System.Net.DnsPermission" /> instance that is restricted.</returns>
		/// <param name="target">The <see cref="T:System.Net.DnsPermission" /> instance to combine with the current instance. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is neither a <see cref="T:System.Net.DnsPermission" /> nor null. </exception>
		// Token: 0x06003465 RID: 13413 RVA: 0x000B665C File Offset: 0x000B485C
		public override IPermission Union(IPermission target)
		{
			DnsPermission dnsPermission = this.Cast(target);
			if (dnsPermission == null)
			{
				return this.Copy();
			}
			if (this.IsUnrestricted() || dnsPermission.IsUnrestricted())
			{
				return new DnsPermission(PermissionState.Unrestricted);
			}
			return new DnsPermission(PermissionState.None);
		}

		// Token: 0x06003466 RID: 13414 RVA: 0x000B6698 File Offset: 0x000B4898
		private bool IsEmpty()
		{
			return !this.m_noRestriction;
		}

		// Token: 0x06003467 RID: 13415 RVA: 0x000B66A3 File Offset: 0x000B48A3
		private DnsPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			DnsPermission dnsPermission = target as DnsPermission;
			if (dnsPermission == null)
			{
				PermissionHelper.ThrowInvalidPermission(target, typeof(DnsPermission));
			}
			return dnsPermission;
		}

		// Token: 0x04001EAF RID: 7855
		private const int version = 1;

		// Token: 0x04001EB0 RID: 7856
		private bool m_noRestriction;
	}
}
