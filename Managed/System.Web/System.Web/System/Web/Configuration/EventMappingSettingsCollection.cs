using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Provides a collection of <see cref="T:System.Web.Configuration.EventMappingSettings" /> objects. This class cannot be inherited.</summary>
	// Token: 0x02000596 RID: 1430
	[ConfigurationCollection(typeof(EventMappingSettings), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public sealed class EventMappingSettingsCollection : ConfigurationElementCollection
	{
		/// <summary>Adds an <see cref="T:System.Web.Configuration.EventMappingSettings" /> object to the collection.</summary>
		/// <param name="eventMappingSettings">An <see cref="T:System.Web.Configuration.EventMappingSettings" /> object to add to the collection.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.EventMappingSettings" /> object to add already exists in the collection, or the collection is read-only.</exception>
		// Token: 0x06003C91 RID: 15505 RVA: 0x0009D8D3 File Offset: 0x0009BAD3
		public void Add(EventMappingSettings eventMappingSettings)
		{
			this.BaseAdd(eventMappingSettings);
		}

		/// <summary>Removes all <see cref="T:System.Web.Configuration.EventMappingSettings" /> objects from the collection.</summary>
		// Token: 0x06003C92 RID: 15506 RVA: 0x0009CBA3 File Offset: 0x0009ADA3
		public void Clear()
		{
			base.BaseClear();
		}

		/// <summary>Indicates whether the collection contains an <see cref="T:System.Web.Configuration.EventMappingSettings" /> object with the specified name.</summary>
		/// <returns>true if the collection contains an <see cref="T:System.Web.Configuration.EventMappingSettings" /> object with the specified name; otherwise, false.</returns>
		/// <param name="name">The name of an <see cref="T:System.Web.Configuration.EventMappingSettings" /> object in the collection.</param>
		// Token: 0x06003C93 RID: 15507 RVA: 0x0009F12D File Offset: 0x0009D32D
		public bool Contains(string name)
		{
			return base.BaseGet(name) != null;
		}

		// Token: 0x06003C94 RID: 15508 RVA: 0x0009F139 File Offset: 0x0009D339
		protected override ConfigurationElement CreateNewElement()
		{
			return new EventMappingSettings();
		}

		// Token: 0x06003C95 RID: 15509 RVA: 0x0009F140 File Offset: 0x0009D340
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((EventMappingSettings)element).Name;
		}

		/// <summary>Returns the index of the specified <see cref="T:System.Web.Configuration.EventMappingSettings" /> object.</summary>
		/// <returns>The index of the specified <see cref="T:System.Web.Configuration.EventMappingSettings" /> object, or -1 if the object is not found in the collection.</returns>
		/// <param name="name">The name of an <see cref="T:System.Web.Configuration.EventMappingSettings" /> object in the collection.</param>
		// Token: 0x06003C96 RID: 15510 RVA: 0x0009F150 File Offset: 0x0009D350
		public int IndexOf(string name)
		{
			EventMappingSettings eventMappingSettings = (EventMappingSettings)base.BaseGet(name);
			if (eventMappingSettings == null)
			{
				return -1;
			}
			return base.BaseIndexOf(eventMappingSettings);
		}

		/// <summary>Adds the specified <see cref="T:System.Web.Configuration.EventMappingSettings" /> object to the specified index point in the collection.</summary>
		/// <param name="index">A valid index of an <see cref="T:System.Web.Configuration.EventMappingSettings" /> object in the collection.</param>
		/// <param name="eventMappingSettings">The <see cref="T:System.Web.Configuration.EventMappingSettings" /> object to insert into the collection.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.EventMappingSettings" /> object to add already exists in the collection, the index is invalid, or the collection is read-only.</exception>
		// Token: 0x06003C97 RID: 15511 RVA: 0x0009F176 File Offset: 0x0009D376
		[global::System.MonoTODO("why did they use 'Insert' and not 'Add' as other collections do?")]
		public void Insert(int index, EventMappingSettings eventMappingSettings)
		{
			this.BaseAdd(index, eventMappingSettings);
		}

		/// <summary>Removes an <see cref="T:System.Web.Configuration.EventMappingSettings" /> object from the collection.</summary>
		/// <param name="name">The name of the <see cref="T:System.Web.Configuration.EventMappingSettings" /> to remove from the collection.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">There is no <see cref="T:System.Web.Configuration.EventMappingSettings" /> object with the specified key in the collection, the element has already been removed, or the collection is read-only.</exception>
		// Token: 0x06003C98 RID: 15512 RVA: 0x0009CBBF File Offset: 0x0009ADBF
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		/// <summary>Removes an <see cref="T:System.Web.Configuration.EventMappingSettings" /> object from the collection.</summary>
		/// <param name="index">The index of an <see cref="T:System.Web.Configuration.EventMappingSettings" /> object in the collection.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">There is no <see cref="T:System.Web.Configuration.EventMappingSettings" /> object with the specified index in the collection, the element has already been removed, or the collection is read-only.</exception>
		// Token: 0x06003C99 RID: 15513 RVA: 0x0009CBC8 File Offset: 0x0009ADC8
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.EventMappingSettings" /> object at the specified index location.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.EventMappingSettings" /> object at the specified index, or null if there is no object at that index.</returns>
		/// <param name="index">A valid index of an <see cref="T:System.Web.Configuration.EventMappingSettings" /> object in the collection.</param>
		// Token: 0x170012BC RID: 4796
		public EventMappingSettings this[int index]
		{
			get
			{
				return (EventMappingSettings)base.BaseGet(index);
			}
			set
			{
				if (base.BaseGet(index) != null)
				{
					base.BaseRemoveAt(index);
				}
				this.BaseAdd(index, value);
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.EventMappingSettings" /> object based on the specified key in the collection.</summary>
		/// <returns>An <see cref="T:System.Web.Configuration.EventMappingSettings" /> object.</returns>
		/// <param name="key">The name of the <see cref="T:System.Web.Configuration.EventMappingSettings" /> object contained in the collection.</param>
		// Token: 0x170012BD RID: 4797
		public EventMappingSettings this[string key]
		{
			get
			{
				return (EventMappingSettings)base.BaseGet(key);
			}
		}

		// Token: 0x170012BE RID: 4798
		// (get) Token: 0x06003C9D RID: 15517 RVA: 0x0009F19C File Offset: 0x0009D39C
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return EventMappingSettingsCollection.properties;
			}
		}

		// Token: 0x040020D7 RID: 8407
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
