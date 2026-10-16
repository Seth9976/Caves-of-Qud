using System;
using System.Security;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Allows declaritive permission checks for event logging. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000265 RID: 613
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Event, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public class EventLogPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLogPermissionAttribute" /> class.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values. </param>
		// Token: 0x06001318 RID: 4888 RVA: 0x00050EF8 File Offset: 0x0004F0F8
		public EventLogPermissionAttribute(SecurityAction action)
			: base(action)
		{
			this.machineName = ".";
			this.permissionAccess = EventLogPermissionAccess.Write;
		}

		/// <summary>Gets or sets the name of the computer on which events might be read.</summary>
		/// <returns>The name of the computer on which events might be read. The default is ".".</returns>
		/// <exception cref="T:System.ArgumentException">The computer name is invalid. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06001319 RID: 4889 RVA: 0x00050F14 File Offset: 0x0004F114
		// (set) Token: 0x0600131A RID: 4890 RVA: 0x00050F1C File Offset: 0x0004F11C
		public string MachineName
		{
			get
			{
				return this.machineName;
			}
			set
			{
				ResourcePermissionBase.ValidateMachineName(value);
				this.machineName = value;
			}
		}

		/// <summary>Gets or sets the access levels used in the permissions request.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Diagnostics.EventLogPermissionAccess" /> values. The default is <see cref="F:System.Diagnostics.EventLogPermissionAccess.Write" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000377 RID: 887
		// (get) Token: 0x0600131B RID: 4891 RVA: 0x00050F2B File Offset: 0x0004F12B
		// (set) Token: 0x0600131C RID: 4892 RVA: 0x00050F33 File Offset: 0x0004F133
		public EventLogPermissionAccess PermissionAccess
		{
			get
			{
				return this.permissionAccess;
			}
			set
			{
				this.permissionAccess = value;
			}
		}

		/// <summary>Creates the permission based on the <see cref="P:System.Diagnostics.EventLogPermissionAttribute.MachineName" /> property and the requested access levels that are set through the <see cref="P:System.Diagnostics.EventLogPermissionAttribute.PermissionAccess" /> property on the attribute.</summary>
		/// <returns>An <see cref="T:System.Security.IPermission" /> that represents the created permission.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600131D RID: 4893 RVA: 0x00050F3C File Offset: 0x0004F13C
		public override IPermission CreatePermission()
		{
			if (base.Unrestricted)
			{
				return new EventLogPermission(PermissionState.Unrestricted);
			}
			return new EventLogPermission(this.permissionAccess, this.machineName);
		}

		// Token: 0x04000AF9 RID: 2809
		private string machineName;

		// Token: 0x04000AFA RID: 2810
		private EventLogPermissionAccess permissionAccess;
	}
}
