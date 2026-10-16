using System;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Defines the smallest unit of a code access security permission that is set for a <see cref="T:System.Diagnostics.PerformanceCounter" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200027C RID: 636
	[Serializable]
	public class PerformanceCounterPermissionEntry
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.PerformanceCounterPermissionEntry" /> class.</summary>
		/// <param name="permissionAccess">A bitwise combination of the <see cref="T:System.Diagnostics.PerformanceCounterPermissionAccess" /> values. The <see cref="P:System.Diagnostics.PerformanceCounterPermissionEntry.PermissionAccess" /> property is set to this value. </param>
		/// <param name="machineName">The server on which the category of the performance counter resides. </param>
		/// <param name="categoryName">The name of the performance counter category (performance object) with which this performance counter is associated. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="categoryName" /> is null.-or-<paramref name="machineName" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="permissionAccess" /> is not a valid <see cref="T:System.Diagnostics.PerformanceCounterPermissionAccess" /> value.-or-<paramref name="machineName" /> is not a valid computer name.</exception>
		// Token: 0x06001431 RID: 5169 RVA: 0x00052E64 File Offset: 0x00051064
		public PerformanceCounterPermissionEntry(PerformanceCounterPermissionAccess permissionAccess, string machineName, string categoryName)
		{
			if (machineName == null)
			{
				throw new ArgumentNullException("machineName");
			}
			if ((permissionAccess | PerformanceCounterPermissionAccess.Administer) != PerformanceCounterPermissionAccess.Administer)
			{
				throw new ArgumentException("permissionAccess");
			}
			ResourcePermissionBase.ValidateMachineName(machineName);
			if (categoryName == null)
			{
				throw new ArgumentNullException("categoryName");
			}
			this.permissionAccess = permissionAccess;
			this.machineName = machineName;
			this.categoryName = categoryName;
		}

		/// <summary>Gets the name of the performance counter category (performance object).</summary>
		/// <returns>The name of the performance counter category (performance object).</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06001432 RID: 5170 RVA: 0x00052EBF File Offset: 0x000510BF
		public string CategoryName
		{
			get
			{
				return this.categoryName;
			}
		}

		/// <summary>Gets the name of the server on which the category of the performance counter resides.</summary>
		/// <returns>The name of the server on which the category resides.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06001433 RID: 5171 RVA: 0x00052EC7 File Offset: 0x000510C7
		public string MachineName
		{
			get
			{
				return this.machineName;
			}
		}

		/// <summary>Gets the permission access level of the entry.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Diagnostics.PerformanceCounterPermissionAccess" /> values.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06001434 RID: 5172 RVA: 0x00052ECF File Offset: 0x000510CF
		public PerformanceCounterPermissionAccess PermissionAccess
		{
			get
			{
				return this.permissionAccess;
			}
		}

		// Token: 0x06001435 RID: 5173 RVA: 0x00052ED7 File Offset: 0x000510D7
		internal ResourcePermissionBaseEntry CreateResourcePermissionBaseEntry()
		{
			return new ResourcePermissionBaseEntry((int)this.permissionAccess, new string[] { this.machineName, this.categoryName });
		}

		// Token: 0x04000B50 RID: 2896
		private const PerformanceCounterPermissionAccess All = PerformanceCounterPermissionAccess.Administer;

		// Token: 0x04000B51 RID: 2897
		private PerformanceCounterPermissionAccess permissionAccess;

		// Token: 0x04000B52 RID: 2898
		private string machineName;

		// Token: 0x04000B53 RID: 2899
		private string categoryName;
	}
}
