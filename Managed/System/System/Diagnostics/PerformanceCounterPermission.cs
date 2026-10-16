using System;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Allows control of code access permissions for <see cref="T:System.Diagnostics.PerformanceCounter" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000279 RID: 633
	[Serializable]
	public sealed class PerformanceCounterPermission : ResourcePermissionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.PerformanceCounterPermission" /> class.</summary>
		// Token: 0x0600141F RID: 5151 RVA: 0x00052CC1 File Offset: 0x00050EC1
		public PerformanceCounterPermission()
		{
			this.SetUp();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.PerformanceCounterPermission" /> class with the specified permission access level entries.</summary>
		/// <param name="permissionAccessEntries">An array of <see cref="T:System.Diagnostics.PerformanceCounterPermissionEntry" /> objects. The <see cref="P:System.Diagnostics.PerformanceCounterPermission.PermissionEntries" /> property is set to this value. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="permissionAccessEntries" /> is null.</exception>
		// Token: 0x06001420 RID: 5152 RVA: 0x00052CCF File Offset: 0x00050ECF
		public PerformanceCounterPermission(PerformanceCounterPermissionEntry[] permissionAccessEntries)
		{
			if (permissionAccessEntries == null)
			{
				throw new ArgumentNullException("permissionAccessEntries");
			}
			this.SetUp();
			this.innerCollection = new PerformanceCounterPermissionEntryCollection(this);
			this.innerCollection.AddRange(permissionAccessEntries);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.PerformanceCounterPermission" /> class with the specified permission state.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.PermissionState" />. </exception>
		// Token: 0x06001421 RID: 5153 RVA: 0x00052D03 File Offset: 0x00050F03
		public PerformanceCounterPermission(PermissionState state)
			: base(state)
		{
			this.SetUp();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.PerformanceCounterPermission" /> class with the specified access levels, the name of the computer to use, and the category associated with the performance counter.</summary>
		/// <param name="permissionAccess">One of the <see cref="T:System.Diagnostics.PerformanceCounterPermissionAccess" /> values. </param>
		/// <param name="machineName">The server on which the performance counter and its associate category reside. </param>
		/// <param name="categoryName">The name of the performance counter category (performance object) with which the performance counter is associated. </param>
		// Token: 0x06001422 RID: 5154 RVA: 0x00052D12 File Offset: 0x00050F12
		public PerformanceCounterPermission(PerformanceCounterPermissionAccess permissionAccess, string machineName, string categoryName)
		{
			this.SetUp();
			this.innerCollection = new PerformanceCounterPermissionEntryCollection(this);
			this.innerCollection.Add(new PerformanceCounterPermissionEntry(permissionAccess, machineName, categoryName));
		}

		/// <summary>Gets the collection of permission entries for this permissions request.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.PerformanceCounterPermissionEntryCollection" /> that contains the permission entries for this permissions request.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06001423 RID: 5155 RVA: 0x00052D40 File Offset: 0x00050F40
		public PerformanceCounterPermissionEntryCollection PermissionEntries
		{
			get
			{
				if (this.innerCollection == null)
				{
					this.innerCollection = new PerformanceCounterPermissionEntryCollection(this);
				}
				return this.innerCollection;
			}
		}

		// Token: 0x06001424 RID: 5156 RVA: 0x00052D5C File Offset: 0x00050F5C
		private void SetUp()
		{
			base.TagNames = new string[] { "Machine", "Category" };
			base.PermissionAccessType = typeof(PerformanceCounterPermissionAccess);
		}

		// Token: 0x06001425 RID: 5157 RVA: 0x00050EA5 File Offset: 0x0004F0A5
		internal ResourcePermissionBaseEntry[] GetEntries()
		{
			return base.GetPermissionEntries();
		}

		// Token: 0x06001426 RID: 5158 RVA: 0x00050EAD File Offset: 0x0004F0AD
		internal void ClearEntries()
		{
			base.Clear();
		}

		// Token: 0x06001427 RID: 5159 RVA: 0x00052D8C File Offset: 0x00050F8C
		internal void Add(object obj)
		{
			PerformanceCounterPermissionEntry performanceCounterPermissionEntry = obj as PerformanceCounterPermissionEntry;
			base.AddPermissionAccess(performanceCounterPermissionEntry.CreateResourcePermissionBaseEntry());
		}

		// Token: 0x06001428 RID: 5160 RVA: 0x00052DAC File Offset: 0x00050FAC
		internal void Remove(object obj)
		{
			PerformanceCounterPermissionEntry performanceCounterPermissionEntry = obj as PerformanceCounterPermissionEntry;
			base.RemovePermissionAccess(performanceCounterPermissionEntry.CreateResourcePermissionBaseEntry());
		}

		// Token: 0x04000B45 RID: 2885
		private PerformanceCounterPermissionEntryCollection innerCollection;
	}
}
