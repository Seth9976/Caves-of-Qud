using System;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Controls code access permissions for event logging.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000263 RID: 611
	[Serializable]
	public sealed class EventLogPermission : ResourcePermissionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLogPermission" /> class.</summary>
		// Token: 0x0600130E RID: 4878 RVA: 0x00050DE5 File Offset: 0x0004EFE5
		public EventLogPermission()
		{
			this.SetUp();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLogPermission" /> class with the specified permission entries.</summary>
		/// <param name="permissionAccessEntries">An array of  objects that represent permission entries. The <see cref="P:System.Diagnostics.EventLogPermission.PermissionEntries" /> property is set to this value. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="permissionAccessEntries" /> is null.</exception>
		// Token: 0x0600130F RID: 4879 RVA: 0x00050DF3 File Offset: 0x0004EFF3
		public EventLogPermission(EventLogPermissionEntry[] permissionAccessEntries)
		{
			if (permissionAccessEntries == null)
			{
				throw new ArgumentNullException("permissionAccessEntries");
			}
			this.SetUp();
			this.innerCollection = new EventLogPermissionEntryCollection(this);
			this.innerCollection.AddRange(permissionAccessEntries);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLogPermission" /> class with the specified permission state.</summary>
		/// <param name="state">One of the enumeration values that specifies the permission state (full access or no access to resources). </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.PermissionState" />. </exception>
		// Token: 0x06001310 RID: 4880 RVA: 0x00050E27 File Offset: 0x0004F027
		public EventLogPermission(PermissionState state)
			: base(state)
		{
			this.SetUp();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLogPermission" /> class with the specified access levels and the name of the computer to use.</summary>
		/// <param name="permissionAccess">One of the enumeration values that specifies an access level. </param>
		/// <param name="machineName">The name of the computer on which to read or write events. </param>
		// Token: 0x06001311 RID: 4881 RVA: 0x00050E36 File Offset: 0x0004F036
		public EventLogPermission(EventLogPermissionAccess permissionAccess, string machineName)
		{
			this.SetUp();
			this.innerCollection = new EventLogPermissionEntryCollection(this);
			this.innerCollection.Add(new EventLogPermissionEntry(permissionAccess, machineName));
		}

		/// <summary>Gets the collection of permission entries for this permissions request.</summary>
		/// <returns>A collection that contains the permission entries for this permissions request.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06001312 RID: 4882 RVA: 0x00050E63 File Offset: 0x0004F063
		public EventLogPermissionEntryCollection PermissionEntries
		{
			get
			{
				if (this.innerCollection == null)
				{
					this.innerCollection = new EventLogPermissionEntryCollection(this);
				}
				return this.innerCollection;
			}
		}

		// Token: 0x06001313 RID: 4883 RVA: 0x00050E7F File Offset: 0x0004F07F
		private void SetUp()
		{
			base.TagNames = new string[] { "Machine" };
			base.PermissionAccessType = typeof(EventLogPermissionAccess);
		}

		// Token: 0x06001314 RID: 4884 RVA: 0x00050EA5 File Offset: 0x0004F0A5
		internal ResourcePermissionBaseEntry[] GetEntries()
		{
			return base.GetPermissionEntries();
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x00050EAD File Offset: 0x0004F0AD
		internal void ClearEntries()
		{
			base.Clear();
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x00050EB8 File Offset: 0x0004F0B8
		internal void Add(object obj)
		{
			EventLogPermissionEntry eventLogPermissionEntry = obj as EventLogPermissionEntry;
			base.AddPermissionAccess(eventLogPermissionEntry.CreateResourcePermissionBaseEntry());
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x00050ED8 File Offset: 0x0004F0D8
		internal void Remove(object obj)
		{
			EventLogPermissionEntry eventLogPermissionEntry = obj as EventLogPermissionEntry;
			base.RemovePermissionAccess(eventLogPermissionEntry.CreateResourcePermissionBaseEntry());
		}

		// Token: 0x04000AF1 RID: 2801
		private EventLogPermissionEntryCollection innerCollection;
	}
}
