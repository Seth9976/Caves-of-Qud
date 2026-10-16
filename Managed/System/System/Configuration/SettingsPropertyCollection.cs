using System;
using System.Collections;

namespace System.Configuration
{
	/// <summary>Contains a collection of <see cref="T:System.Configuration.SettingsProperty" /> objects.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001D0 RID: 464
	public class SettingsPropertyCollection : ICloneable, ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsPropertyCollection" /> class.</summary>
		// Token: 0x06000C15 RID: 3093 RVA: 0x00031FAC File Offset: 0x000301AC
		public SettingsPropertyCollection()
		{
			this.items = new Hashtable();
		}

		/// <summary>Adds a <see cref="T:System.Configuration.SettingsProperty" /> object to the collection.</summary>
		/// <param name="property">A <see cref="T:System.Configuration.SettingsProperty" /> object.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000C16 RID: 3094 RVA: 0x00031FBF File Offset: 0x000301BF
		public void Add(SettingsProperty property)
		{
			if (this.isReadOnly)
			{
				throw new NotSupportedException();
			}
			this.OnAdd(property);
			this.items.Add(property.Name, property);
			this.OnAddComplete(property);
		}

		/// <summary>Removes all <see cref="T:System.Configuration.SettingsProperty" /> objects from the collection.</summary>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000C17 RID: 3095 RVA: 0x00031FEF File Offset: 0x000301EF
		public void Clear()
		{
			if (this.isReadOnly)
			{
				throw new NotSupportedException();
			}
			this.OnClear();
			this.items.Clear();
			this.OnClearComplete();
		}

		/// <summary>Creates a copy of the existing collection.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsPropertyCollection" /> class.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000C18 RID: 3096 RVA: 0x00032016 File Offset: 0x00030216
		public object Clone()
		{
			return new SettingsPropertyCollection
			{
				items = (Hashtable)this.items.Clone()
			};
		}

		/// <summary>Copies this <see cref="T:System.Configuration.SettingsPropertyCollection" /> object to an array.</summary>
		/// <param name="array">The array to copy the object to.</param>
		/// <param name="index">The index at which to begin copying.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000C19 RID: 3097 RVA: 0x00032033 File Offset: 0x00030233
		public void CopyTo(Array array, int index)
		{
			this.items.Values.CopyTo(array, index);
		}

		/// <summary>Gets the <see cref="T:System.Collections.IEnumerator" /> object as it applies to the collection.</summary>
		/// <returns>The <see cref="T:System.Collections.IEnumerator" /> object as it applies to the collection.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000C1A RID: 3098 RVA: 0x00032047 File Offset: 0x00030247
		public IEnumerator GetEnumerator()
		{
			return this.items.Values.GetEnumerator();
		}

		/// <summary>Removes a <see cref="T:System.Configuration.SettingsProperty" /> object from the collection.</summary>
		/// <param name="name">The name of the <see cref="T:System.Configuration.SettingsProperty" /> object.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000C1B RID: 3099 RVA: 0x0003205C File Offset: 0x0003025C
		public void Remove(string name)
		{
			if (this.isReadOnly)
			{
				throw new NotSupportedException();
			}
			SettingsProperty settingsProperty = (SettingsProperty)this.items[name];
			this.OnRemove(settingsProperty);
			this.items.Remove(name);
			this.OnRemoveComplete(settingsProperty);
		}

		/// <summary>Sets the collection to be read-only.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000C1C RID: 3100 RVA: 0x000320A3 File Offset: 0x000302A3
		public void SetReadOnly()
		{
			this.isReadOnly = true;
		}

		/// <summary>Performs additional, custom processing when adding to the contents of the <see cref="T:System.Configuration.SettingsPropertyCollection" /> instance.</summary>
		/// <param name="property">A <see cref="T:System.Configuration.SettingsProperty" /> object.</param>
		// Token: 0x06000C1D RID: 3101 RVA: 0x00003917 File Offset: 0x00001B17
		protected virtual void OnAdd(SettingsProperty property)
		{
		}

		/// <summary>Performs additional, custom processing after adding to the contents of the <see cref="T:System.Configuration.SettingsPropertyCollection" /> instance.</summary>
		/// <param name="property">A <see cref="T:System.Configuration.SettingsProperty" /> object.</param>
		// Token: 0x06000C1E RID: 3102 RVA: 0x00003917 File Offset: 0x00001B17
		protected virtual void OnAddComplete(SettingsProperty property)
		{
		}

		/// <summary>Performs additional, custom processing when clearing the contents of the <see cref="T:System.Configuration.SettingsPropertyCollection" /> instance.</summary>
		// Token: 0x06000C1F RID: 3103 RVA: 0x00003917 File Offset: 0x00001B17
		protected virtual void OnClear()
		{
		}

		/// <summary>Performs additional, custom processing after clearing the contents of the <see cref="T:System.Configuration.SettingsPropertyCollection" /> instance.</summary>
		// Token: 0x06000C20 RID: 3104 RVA: 0x00003917 File Offset: 0x00001B17
		protected virtual void OnClearComplete()
		{
		}

		/// <summary>Performs additional, custom processing when removing the contents of the <see cref="T:System.Configuration.SettingsPropertyCollection" /> instance.</summary>
		/// <param name="property">A <see cref="T:System.Configuration.SettingsProperty" /> object.</param>
		// Token: 0x06000C21 RID: 3105 RVA: 0x00003917 File Offset: 0x00001B17
		protected virtual void OnRemove(SettingsProperty property)
		{
		}

		/// <summary>Performs additional, custom processing after removing the contents of the <see cref="T:System.Configuration.SettingsPropertyCollection" /> instance.</summary>
		/// <param name="property">A <see cref="T:System.Configuration.SettingsProperty" /> object.</param>
		// Token: 0x06000C22 RID: 3106 RVA: 0x00003917 File Offset: 0x00001B17
		protected virtual void OnRemoveComplete(SettingsProperty property)
		{
		}

		/// <summary>Gets a value that specifies the number of <see cref="T:System.Configuration.SettingsProperty" /> objects in the collection.</summary>
		/// <returns>The number of <see cref="T:System.Configuration.SettingsProperty" /> objects in the collection.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000C23 RID: 3107 RVA: 0x000320AC File Offset: 0x000302AC
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		/// <summary>Gets a value that indicates whether access to the collection is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Configuration.SettingsPropertyCollection" /> is synchronized; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000C24 RID: 3108 RVA: 0x00003062 File Offset: 0x00001262
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the collection item with the specified name.</summary>
		/// <returns>The <see cref="T:System.Configuration.SettingsProperty" /> object with the specified <paramref name="name" />.</returns>
		/// <param name="name">The name of the <see cref="T:System.Configuration.SettingsProperty" /> object.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000210 RID: 528
		public SettingsProperty this[string name]
		{
			get
			{
				return (SettingsProperty)this.items[name];
			}
		}

		/// <summary>Gets the object to synchronize access to the collection.</summary>
		/// <returns>The object to synchronize access to the collection.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000C26 RID: 3110 RVA: 0x000075E1 File Offset: 0x000057E1
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x040007C8 RID: 1992
		private Hashtable items;

		// Token: 0x040007C9 RID: 1993
		private bool isReadOnly;
	}
}
