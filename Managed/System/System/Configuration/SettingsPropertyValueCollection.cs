using System;
using System.Collections;

namespace System.Configuration
{
	/// <summary>Contains a collection of settings property values that map <see cref="T:System.Configuration.SettingsProperty" /> objects to <see cref="T:System.Configuration.SettingsPropertyValue" /> objects.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001D4 RID: 468
	public class SettingsPropertyValueCollection : ICloneable, ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsPropertyValueCollection" /> class.</summary>
		// Token: 0x06000C3E RID: 3134 RVA: 0x0003256C File Offset: 0x0003076C
		public SettingsPropertyValueCollection()
		{
			this.items = new Hashtable();
		}

		/// <summary>Adds a <see cref="T:System.Configuration.SettingsPropertyValue" /> object to the collection.</summary>
		/// <param name="property">A <see cref="T:System.Configuration.SettingsPropertyValue" /> object.</param>
		/// <exception cref="T:System.NotSupportedException">An attempt was made to add an item to the collection, but the collection was marked as read-only.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000C3F RID: 3135 RVA: 0x0003257F File Offset: 0x0003077F
		public void Add(SettingsPropertyValue property)
		{
			if (this.isReadOnly)
			{
				throw new NotSupportedException();
			}
			this.items.Add(property.Name, property);
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x000325A4 File Offset: 0x000307A4
		internal void Add(SettingsPropertyValueCollection vals)
		{
			foreach (object obj in vals)
			{
				SettingsPropertyValue settingsPropertyValue = (SettingsPropertyValue)obj;
				this.Add(settingsPropertyValue);
			}
		}

		/// <summary>Removes all <see cref="T:System.Configuration.SettingsPropertyValue" /> objects from the collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000C41 RID: 3137 RVA: 0x000325F8 File Offset: 0x000307F8
		public void Clear()
		{
			if (this.isReadOnly)
			{
				throw new NotSupportedException();
			}
			this.items.Clear();
		}

		/// <summary>Creates a copy of the existing collection.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsPropertyValueCollection" /> class.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000C42 RID: 3138 RVA: 0x00032613 File Offset: 0x00030813
		public object Clone()
		{
			return new SettingsPropertyValueCollection
			{
				items = (Hashtable)this.items.Clone()
			};
		}

		/// <summary>Copies this <see cref="T:System.Configuration.SettingsPropertyValueCollection" /> collection to an array.</summary>
		/// <param name="array">The array to copy the collection to.</param>
		/// <param name="index">The index at which to begin copying.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000C43 RID: 3139 RVA: 0x00032630 File Offset: 0x00030830
		public void CopyTo(Array array, int index)
		{
			this.items.Values.CopyTo(array, index);
		}

		/// <summary>Gets the <see cref="T:System.Collections.IEnumerator" /> object as it applies to the collection.</summary>
		/// <returns>The <see cref="T:System.Collections.IEnumerator" /> object as it applies to the collection.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000C44 RID: 3140 RVA: 0x00032644 File Offset: 0x00030844
		public IEnumerator GetEnumerator()
		{
			return this.items.Values.GetEnumerator();
		}

		/// <summary>Removes a <see cref="T:System.Configuration.SettingsPropertyValue" /> object from the collection.</summary>
		/// <param name="name">The name of the <see cref="T:System.Configuration.SettingsPropertyValue" /> object.</param>
		/// <exception cref="T:System.NotSupportedException">An attempt was made to remove an item from the collection, but the collection was marked as read-only.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000C45 RID: 3141 RVA: 0x00032656 File Offset: 0x00030856
		public void Remove(string name)
		{
			if (this.isReadOnly)
			{
				throw new NotSupportedException();
			}
			this.items.Remove(name);
		}

		/// <summary>Sets the collection to be read-only.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000C46 RID: 3142 RVA: 0x00032672 File Offset: 0x00030872
		public void SetReadOnly()
		{
			this.isReadOnly = true;
		}

		/// <summary>Gets a value that specifies the number of <see cref="T:System.Configuration.SettingsPropertyValue" /> objects in the collection.</summary>
		/// <returns>The number of <see cref="T:System.Configuration.SettingsPropertyValue" /> objects in the collection.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000C47 RID: 3143 RVA: 0x0003267B File Offset: 0x0003087B
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		/// <summary>Gets a value that indicates whether access to the collection is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Configuration.SettingsPropertyValueCollection" /> collection is synchronized; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000C48 RID: 3144 RVA: 0x0000829A File Offset: 0x0000649A
		public bool IsSynchronized
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets an item from the collection.</summary>
		/// <returns>The <see cref="T:System.Configuration.SettingsPropertyValue" /> object with the specified <paramref name="name" />.</returns>
		/// <param name="name">A <see cref="T:System.Configuration.SettingsPropertyValue" /> object.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700021B RID: 539
		public SettingsPropertyValue this[string name]
		{
			get
			{
				return (SettingsPropertyValue)this.items[name];
			}
		}

		/// <summary>Gets the object to synchronize access to the collection.</summary>
		/// <returns>The object to synchronize access to the collection.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000C4A RID: 3146 RVA: 0x0000829A File Offset: 0x0000649A
		public object SyncRoot
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x040007D2 RID: 2002
		private Hashtable items;

		// Token: 0x040007D3 RID: 2003
		private bool isReadOnly;
	}
}
