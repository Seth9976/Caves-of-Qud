using System;
using System.Collections;
using System.Security.Permissions;
using Unity;

namespace System.Diagnostics
{
	/// <summary>Contains a strongly typed collection of <see cref="T:System.Diagnostics.EventLogPermissionEntry" /> objects.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000267 RID: 615
	[Serializable]
	public class EventLogPermissionEntryCollection : CollectionBase
	{
		// Token: 0x06001322 RID: 4898 RVA: 0x00050FA8 File Offset: 0x0004F1A8
		internal EventLogPermissionEntryCollection(EventLogPermission owner)
		{
			this.owner = owner;
			ResourcePermissionBaseEntry[] entries = owner.GetEntries();
			if (entries.Length != 0)
			{
				foreach (ResourcePermissionBaseEntry resourcePermissionBaseEntry in entries)
				{
					EventLogPermissionEntry eventLogPermissionEntry = new EventLogPermissionEntry((EventLogPermissionAccess)resourcePermissionBaseEntry.PermissionAccess, resourcePermissionBaseEntry.PermissionAccessPath[0]);
					base.InnerList.Add(eventLogPermissionEntry);
				}
			}
		}

		/// <summary>Gets or sets the object at a specified index.</summary>
		/// <returns>The <see cref="T:System.Diagnostics.EventLogPermissionEntry" /> that exists at the specified index.</returns>
		/// <param name="index">The zero-based index into the collection. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700037A RID: 890
		public EventLogPermissionEntry this[int index]
		{
			get
			{
				return (EventLogPermissionEntry)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds a specified <see cref="T:System.Diagnostics.EventLogPermissionEntry" /> to this collection.</summary>
		/// <returns>The zero-based index of the added <see cref="T:System.Diagnostics.EventLogPermissionEntry" />.</returns>
		/// <param name="value">The <see cref="T:System.Diagnostics.EventLogPermissionEntry" /> to add. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001325 RID: 4901 RVA: 0x00051026 File Offset: 0x0004F226
		public int Add(EventLogPermissionEntry value)
		{
			return base.List.Add(value);
		}

		/// <summary>Appends a set of specified permission entries to this collection.</summary>
		/// <param name="value">An array of type <see cref="T:System.Diagnostics.EventLogPermissionEntry" /> objects that contains the permission entries to add. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001326 RID: 4902 RVA: 0x00051034 File Offset: 0x0004F234
		public void AddRange(EventLogPermissionEntry[] value)
		{
			foreach (EventLogPermissionEntry eventLogPermissionEntry in value)
			{
				base.List.Add(eventLogPermissionEntry);
			}
		}

		/// <summary>Appends a set of specified permission entries to this collection.</summary>
		/// <param name="value">A <see cref="T:System.Diagnostics.EventLogPermissionEntryCollection" /> that contains the permission entries to add. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001327 RID: 4903 RVA: 0x00051064 File Offset: 0x0004F264
		public void AddRange(EventLogPermissionEntryCollection value)
		{
			foreach (object obj in value)
			{
				EventLogPermissionEntry eventLogPermissionEntry = (EventLogPermissionEntry)obj;
				base.List.Add(eventLogPermissionEntry);
			}
		}

		/// <summary>Determines whether this collection contains a specified <see cref="T:System.Diagnostics.EventLogPermissionEntry" />.</summary>
		/// <returns>true if the specified <see cref="T:System.Diagnostics.EventLogPermissionEntry" /> belongs to this collection; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.Diagnostics.EventLogPermissionEntry" /> to find. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001328 RID: 4904 RVA: 0x000510C0 File Offset: 0x0004F2C0
		public bool Contains(EventLogPermissionEntry value)
		{
			return base.List.Contains(value);
		}

		/// <summary>Copies the permission entries from this collection to an array, starting at a particular index of the array.</summary>
		/// <param name="array">An array of type <see cref="T:System.Diagnostics.EventLogPermissionEntry" /> that receives this collection's permission entries. </param>
		/// <param name="index">The zero-based index at which to begin copying the permission entries. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001329 RID: 4905 RVA: 0x000510CE File Offset: 0x0004F2CE
		public void CopyTo(EventLogPermissionEntry[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Determines the index of a specified permission entry in this collection.</summary>
		/// <returns>The zero-based index of the specified permission entry, or -1 if the permission entry was not found in the collection.</returns>
		/// <param name="value">The permission entry to search for. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600132A RID: 4906 RVA: 0x000510DD File Offset: 0x0004F2DD
		public int IndexOf(EventLogPermissionEntry value)
		{
			return base.List.IndexOf(value);
		}

		/// <summary>Inserts a permission entry into this collection at a specified index.</summary>
		/// <param name="index">The zero-based index of the collection at which to insert the permission entry. </param>
		/// <param name="value">The permission entry to insert into this collection. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600132B RID: 4907 RVA: 0x000510EB File Offset: 0x0004F2EB
		public void Insert(int index, EventLogPermissionEntry value)
		{
			base.List.Insert(index, value);
		}

		/// <summary>Performs additional custom processes after clearing the contents of the collection.</summary>
		// Token: 0x0600132C RID: 4908 RVA: 0x000510FA File Offset: 0x0004F2FA
		protected override void OnClear()
		{
			this.owner.ClearEntries();
		}

		/// <summary>Performs additional custom processes before a new permission entry is inserted into the collection.</summary>
		/// <param name="index">The zero-based index at which to insert <paramref name="value" />. </param>
		/// <param name="value">The new value of the permission entry at <paramref name="index" />. </param>
		// Token: 0x0600132D RID: 4909 RVA: 0x00051107 File Offset: 0x0004F307
		protected override void OnInsert(int index, object value)
		{
			this.owner.Add(value);
		}

		/// <summary>Performs additional custom processes when removing a new permission entry from the collection.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> can be found. </param>
		/// <param name="value">The permission entry to remove from <paramref name="index" />. </param>
		// Token: 0x0600132E RID: 4910 RVA: 0x00051115 File Offset: 0x0004F315
		protected override void OnRemove(int index, object value)
		{
			this.owner.Remove(value);
		}

		/// <summary>Performs additional custom processes before setting a value in the collection.</summary>
		/// <param name="index">The zero-based index at which <paramref name="oldValue" /> can be found. </param>
		/// <param name="oldValue">The value to replace with <paramref name="newValue" />. </param>
		/// <param name="newValue">The new value of the permission entry at <paramref name="index" />. </param>
		// Token: 0x0600132F RID: 4911 RVA: 0x00051123 File Offset: 0x0004F323
		protected override void OnSet(int index, object oldValue, object newValue)
		{
			this.owner.Remove(oldValue);
			this.owner.Add(newValue);
		}

		/// <summary>Removes a specified permission entry from this collection.</summary>
		/// <param name="value">The permission entry to remove. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001330 RID: 4912 RVA: 0x0005113D File Offset: 0x0004F33D
		public void Remove(EventLogPermissionEntry value)
		{
			base.List.Remove(value);
		}

		// Token: 0x06001331 RID: 4913 RVA: 0x00013BAE File Offset: 0x00011DAE
		internal EventLogPermissionEntryCollection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000AFD RID: 2813
		private EventLogPermission owner;
	}
}
