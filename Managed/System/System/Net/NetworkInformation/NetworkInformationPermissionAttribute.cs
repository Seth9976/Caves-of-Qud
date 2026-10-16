using System;
using System.Security;
using System.Security.Permissions;

namespace System.Net.NetworkInformation
{
	/// <summary>Allows security actions for <see cref="T:System.Net.NetworkInformation.NetworkInformationPermission" /> to be applied to code using declarative security.</summary>
	// Token: 0x020006F9 RID: 1785
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class NetworkInformationPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.NetworkInformationPermissionAttribute" /> class.</summary>
		/// <param name="action">A <see cref="T:System.Security.Permissions.SecurityAction" /> value that specifies the permission behavior.</param>
		// Token: 0x06003950 RID: 14672 RVA: 0x000A979A File Offset: 0x000A799A
		public NetworkInformationPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		/// <summary>Gets or sets the network information access level.</summary>
		/// <returns>A string that specifies the access level.</returns>
		// Token: 0x17000C68 RID: 3176
		// (get) Token: 0x06003951 RID: 14673 RVA: 0x000C8353 File Offset: 0x000C6553
		// (set) Token: 0x06003952 RID: 14674 RVA: 0x000C835B File Offset: 0x000C655B
		public string Access
		{
			get
			{
				return this.access;
			}
			set
			{
				this.access = value;
			}
		}

		/// <summary>Creates and returns a new <see cref="T:System.Net.NetworkInformation.NetworkInformationPermission" /> object.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.NetworkInformationPermission" /> that corresponds to this attribute.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06003953 RID: 14675 RVA: 0x000C8364 File Offset: 0x000C6564
		public override IPermission CreatePermission()
		{
			NetworkInformationPermission networkInformationPermission;
			if (base.Unrestricted)
			{
				networkInformationPermission = new NetworkInformationPermission(PermissionState.Unrestricted);
			}
			else
			{
				networkInformationPermission = new NetworkInformationPermission(PermissionState.None);
				if (this.access != null)
				{
					if (string.Compare(this.access, "Read", StringComparison.OrdinalIgnoreCase) == 0)
					{
						networkInformationPermission.AddPermission(NetworkInformationAccess.Read);
					}
					else if (string.Compare(this.access, "Ping", StringComparison.OrdinalIgnoreCase) == 0)
					{
						networkInformationPermission.AddPermission(NetworkInformationAccess.Ping);
					}
					else
					{
						if (string.Compare(this.access, "None", StringComparison.OrdinalIgnoreCase) != 0)
						{
							throw new ArgumentException(SR.GetString("The parameter value '{0}={1}' is invalid.", new object[] { "Access", this.access }));
						}
						networkInformationPermission.AddPermission(NetworkInformationAccess.None);
					}
				}
			}
			return networkInformationPermission;
		}

		// Token: 0x040021B7 RID: 8631
		private const string strAccess = "Access";

		// Token: 0x040021B8 RID: 8632
		private string access;
	}
}
