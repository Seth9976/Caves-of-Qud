using System;
using System.Security;
using System.Security.Permissions;

namespace System.Net
{
	/// <summary>Specifies permission to request information from Domain Name Servers.</summary>
	// Token: 0x02000682 RID: 1666
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class DnsPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.DnsPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" /> value.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="action" /> parameter is not a valid <see cref="T:System.Security.Permissions.SecurityAction" />. </exception>
		// Token: 0x06003468 RID: 13416 RVA: 0x000A979A File Offset: 0x000A799A
		public DnsPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		/// <summary>Creates and returns a new instance of the <see cref="T:System.Net.DnsPermission" /> class.</summary>
		/// <returns>A <see cref="T:System.Net.DnsPermission" /> that corresponds to the security declaration.</returns>
		// Token: 0x06003469 RID: 13417 RVA: 0x000B66C3 File Offset: 0x000B48C3
		public override IPermission CreatePermission()
		{
			return new DnsPermission(base.Unrestricted ? PermissionState.Unrestricted : PermissionState.None);
		}
	}
}
