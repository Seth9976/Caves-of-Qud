using System;
using System.Security;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Allows declaritive performance counter permission checks. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200027B RID: 635
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Event, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public class PerformanceCounterPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.PerformanceCounterPermissionAttribute" /> class.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values. </param>
		// Token: 0x06001429 RID: 5161 RVA: 0x00052DCC File Offset: 0x00050FCC
		public PerformanceCounterPermissionAttribute(SecurityAction action)
			: base(action)
		{
			this.categoryName = "*";
			this.machineName = ".";
			this.permissionAccess = PerformanceCounterPermissionAccess.Write;
		}

		/// <summary>Gets or sets the name of the performance counter category.</summary>
		/// <returns>The name of the performance counter category (performance object).</returns>
		/// <exception cref="T:System.ArgumentNullException">The value is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x0600142A RID: 5162 RVA: 0x00052DF2 File Offset: 0x00050FF2
		// (set) Token: 0x0600142B RID: 5163 RVA: 0x00052DFA File Offset: 0x00050FFA
		public string CategoryName
		{
			get
			{
				return this.categoryName;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("CategoryName");
				}
				this.categoryName = value;
			}
		}

		/// <summary>Gets or sets the computer name for the performance counter.</summary>
		/// <returns>The server on which the category of the performance counter resides.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Diagnostics.PerformanceCounterPermissionAttribute.MachineName" /> format is invalid. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x0600142C RID: 5164 RVA: 0x00052E11 File Offset: 0x00051011
		// (set) Token: 0x0600142D RID: 5165 RVA: 0x00052E19 File Offset: 0x00051019
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
		/// <returns>A bitwise combination of the <see cref="T:System.Diagnostics.PerformanceCounterPermissionAccess" /> values. The default is <see cref="F:System.Diagnostics.EventLogPermissionAccess.Write" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x0600142E RID: 5166 RVA: 0x00052E28 File Offset: 0x00051028
		// (set) Token: 0x0600142F RID: 5167 RVA: 0x00052E30 File Offset: 0x00051030
		public PerformanceCounterPermissionAccess PermissionAccess
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

		/// <summary>Creates the permission based on the requested access levels that are set through the <see cref="P:System.Diagnostics.PerformanceCounterPermissionAttribute.PermissionAccess" /> property on the attribute.</summary>
		/// <returns>An <see cref="T:System.Security.IPermission" /> that represents the created permission.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001430 RID: 5168 RVA: 0x00052E39 File Offset: 0x00051039
		public override IPermission CreatePermission()
		{
			if (base.Unrestricted)
			{
				return new PerformanceCounterPermission(PermissionState.Unrestricted);
			}
			return new PerformanceCounterPermission(this.permissionAccess, this.machineName, this.categoryName);
		}

		// Token: 0x04000B4D RID: 2893
		private string categoryName;

		// Token: 0x04000B4E RID: 2894
		private string machineName;

		// Token: 0x04000B4F RID: 2895
		private PerformanceCounterPermissionAccess permissionAccess;
	}
}
