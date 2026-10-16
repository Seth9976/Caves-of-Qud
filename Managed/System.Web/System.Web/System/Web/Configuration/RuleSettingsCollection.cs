using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>A collection of <see cref="T:System.Web.Configuration.RuleSettings" /> objects. This class cannot be inherited.</summary>
	// Token: 0x020005D0 RID: 1488
	[ConfigurationCollection(typeof(RuleSettings), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public sealed class RuleSettingsCollection : ConfigurationElementCollection
	{
		/// <summary>Adds a <see cref="T:System.Web.Configuration.RuleSettings" /> object to the collection.</summary>
		/// <param name="ruleSettings">A <see cref="T:System.Web.Configuration.RuleSettings" /> object to add to the collection.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.RuleSettings" /> object to add already exists in the collection or the collection is read-only.</exception>
		// Token: 0x06004043 RID: 16451 RVA: 0x0009D8D3 File Offset: 0x0009BAD3
		public void Add(RuleSettings ruleSettings)
		{
			this.BaseAdd(ruleSettings);
		}

		/// <summary>Removes all <see cref="T:System.Web.Configuration.RuleSettings" /> objects from the collection.</summary>
		// Token: 0x06004044 RID: 16452 RVA: 0x0009CBA3 File Offset: 0x0009ADA3
		public void Clear()
		{
			base.BaseClear();
		}

		/// <summary>Returns true if the collection contains a <see cref="T:System.Web.Configuration.RuleSettings" /> object with the specified name.</summary>
		/// <returns>true if the collection contains a <see cref="T:System.Web.Configuration.RuleSettings" /> object with the specified name; otherwise, false.</returns>
		/// <param name="name">The name of a <see cref="T:System.Web.Configuration.RuleSettings" /> object in the collection.</param>
		// Token: 0x06004045 RID: 16453 RVA: 0x0009F12D File Offset: 0x0009D32D
		public bool Contains(string name)
		{
			return base.BaseGet(name) != null;
		}

		// Token: 0x06004046 RID: 16454 RVA: 0x000A7568 File Offset: 0x000A5768
		protected override ConfigurationElement CreateNewElement()
		{
			return new RuleSettings();
		}

		// Token: 0x06004047 RID: 16455 RVA: 0x000A756F File Offset: 0x000A576F
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((RuleSettings)element).Name;
		}

		/// <summary>Finds the index of a <see cref="T:System.Web.Configuration.RuleSettings" /> object in the collection with the specified name.</summary>
		/// <returns>The index of a <see cref="T:System.Web.Configuration.RuleSettings" /> object in the collection with the specified name.</returns>
		/// <param name="name">The name of a <see cref="T:System.Web.Configuration.RuleSettings" /> object in the collection.</param>
		// Token: 0x06004048 RID: 16456 RVA: 0x000A757C File Offset: 0x000A577C
		public int IndexOf(string name)
		{
			RuleSettings ruleSettings = (RuleSettings)base.BaseGet(name);
			if (ruleSettings == null)
			{
				return -1;
			}
			return base.BaseIndexOf(ruleSettings);
		}

		/// <summary>Adds the specified <see cref="T:System.Web.Configuration.RuleSettings" /> object to the specified index point in the collection.</summary>
		/// <param name="index">A valid index of a <see cref="T:System.Web.Configuration.RuleSettings" /> object in the collection.</param>
		/// <param name="eventSettings">The <see cref="T:System.Web.Configuration.RuleSettings" /> object to insert into the collection.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.RuleSettings" /> object to add already exists in the collection, the index is invalid, or the collection is read only.</exception>
		// Token: 0x06004049 RID: 16457 RVA: 0x0009F176 File Offset: 0x0009D376
		[global::System.MonoTODO("why did they use 'Insert' and not 'Add' as other collections do?")]
		public void Insert(int index, RuleSettings eventSettings)
		{
			this.BaseAdd(index, eventSettings);
		}

		/// <summary>Removes a <see cref="T:System.Web.Configuration.RuleSettings" /> object from the collection.</summary>
		/// <param name="name">The name of a <see cref="T:System.Web.Configuration.RuleSettings" /> object in the collection.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">There is no <see cref="T:System.Web.Configuration.RuleSettings" /> object with the specified key in the collection, the element has already been removed, or the collection is read-only.</exception>
		// Token: 0x0600404A RID: 16458 RVA: 0x0009CBBF File Offset: 0x0009ADBF
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		/// <summary>Removes a <see cref="T:System.Web.Configuration.RuleSettings" /> object at the specified index location from the collection.</summary>
		/// <param name="index">A valid index of a <see cref="T:System.Web.Configuration.RuleSettings" /> object in the collection.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">There is no <see cref="T:System.Web.Configuration.RuleSettings" /> object at the specified index in the collection, the element has already been removed, or the collection is read-only.</exception>
		// Token: 0x0600404B RID: 16459 RVA: 0x0009CBC8 File Offset: 0x0009ADC8
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.RuleSettings" /> object based on the specified key in the collection.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.RuleSettings" /> object.</returns>
		/// <param name="key">The name of the <see cref="T:System.Web.Configuration.RuleSettings" /> object contained in the collection.</param>
		// Token: 0x17001462 RID: 5218
		public RuleSettings this[string key]
		{
			get
			{
				return (RuleSettings)base.BaseGet(key);
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.RuleSettings" /> object at the specified numeric index.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.RuleSettings" /> object at the specified index.</returns>
		/// <param name="index">The index of a <see cref="T:System.Web.Configuration.RuleSettings" /> object in the collection.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">Index is out of range.</exception>
		// Token: 0x17001463 RID: 5219
		public RuleSettings this[int index]
		{
			get
			{
				return (RuleSettings)base.BaseGet(index);
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

		// Token: 0x17001464 RID: 5220
		// (get) Token: 0x0600404F RID: 16463 RVA: 0x000A75BE File Offset: 0x000A57BE
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return RuleSettingsCollection.properties;
			}
		}

		// Token: 0x040022EC RID: 8940
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
