using System;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Defines the smallest unit of a code access security permission that is set for an <see cref="T:System.Diagnostics.EventLog" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000266 RID: 614
	[Serializable]
	public class EventLogPermissionEntry
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLogPermissionEntry" /> class.</summary>
		/// <param name="permissionAccess">A bitwise combination of the <see cref="T:System.Diagnostics.EventLogPermissionAccess" /> values. The <see cref="P:System.Diagnostics.EventLogPermissionEntry.PermissionAccess" /> property is set to this value. </param>
		/// <param name="machineName">The name of the computer on which to read or write events. The <see cref="P:System.Diagnostics.EventLogPermissionEntry.MachineName" /> property is set to this value. </param>
		/// <exception cref="T:System.ArgumentException">The computer name is invalid. </exception>
		// Token: 0x0600131E RID: 4894 RVA: 0x00050F5E File Offset: 0x0004F15E
		public EventLogPermissionEntry(EventLogPermissionAccess permissionAccess, string machineName)
		{
			ResourcePermissionBase.ValidateMachineName(machineName);
			this.permissionAccess = permissionAccess;
			this.machineName = machineName;
		}

		/// <summary>Gets the name of the computer on which to read or write events.</summary>
		/// <returns>The name of the computer on which to read or write events.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000378 RID: 888
		// (get) Token: 0x0600131F RID: 4895 RVA: 0x00050F7A File Offset: 0x0004F17A
		public string MachineName
		{
			get
			{
				return this.machineName;
			}
		}

		/// <summary>Gets the permission access levels used in the permissions request.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Diagnostics.EventLogPermissionAccess" /> values.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06001320 RID: 4896 RVA: 0x00050F82 File Offset: 0x0004F182
		public EventLogPermissionAccess PermissionAccess
		{
			get
			{
				return this.permissionAccess;
			}
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x00050F8A File Offset: 0x0004F18A
		internal ResourcePermissionBaseEntry CreateResourcePermissionBaseEntry()
		{
			return new ResourcePermissionBaseEntry((int)this.permissionAccess, new string[] { this.machineName });
		}

		// Token: 0x04000AFB RID: 2811
		private EventLogPermissionAccess permissionAccess;

		// Token: 0x04000AFC RID: 2812
		private string machineName;
	}
}
