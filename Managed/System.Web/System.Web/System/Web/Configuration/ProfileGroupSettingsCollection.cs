using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Contains a set of <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> objects.</summary>
	// Token: 0x020005C3 RID: 1475
	[ConfigurationCollection(typeof(ProfileGroupSettings), AddItemName = "group", CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public sealed class ProfileGroupSettingsCollection : ConfigurationElementCollection
	{
		/// <summary>Adds a <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object to the collection.</summary>
		/// <param name="group">A <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object to add to the collection.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object to add already exists in the collection, or the collection is read-only. </exception>
		// Token: 0x06003F8F RID: 16271 RVA: 0x0009D8D3 File Offset: 0x0009BAD3
		public void Add(ProfileGroupSettings group)
		{
			this.BaseAdd(group);
		}

		/// <summary>Gets a string array of all the key values in the collection.</summary>
		/// <returns>A string array of all the key values in the collection.</returns>
		// Token: 0x17001423 RID: 5155
		// (get) Token: 0x06003F90 RID: 16272 RVA: 0x000A604C File Offset: 0x000A424C
		public string[] AllKeys
		{
			get
			{
				string[] array = new string[base.Count];
				for (int i = 0; i < base.Count; i++)
				{
					array[i] = this[i].Name;
				}
				return array;
			}
		}

		// Token: 0x06003F91 RID: 16273 RVA: 0x000A6086 File Offset: 0x000A4286
		protected internal override bool IsModified()
		{
			return base.IsModified();
		}

		// Token: 0x06003F92 RID: 16274 RVA: 0x000A608E File Offset: 0x000A428E
		protected internal override void ResetModified()
		{
			base.ResetModified();
		}

		/// <summary>Removes all <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> objects from the collection.</summary>
		// Token: 0x06003F93 RID: 16275 RVA: 0x0009CBA3 File Offset: 0x0009ADA3
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x06003F94 RID: 16276 RVA: 0x000A6096 File Offset: 0x000A4296
		protected override ConfigurationElement CreateNewElement()
		{
			return new ProfileGroupSettings();
		}

		/// <summary>Returns the <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object at the specified index.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object at the specified index, or null if there is no object at that index.</returns>
		/// <param name="index">The index of the <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object to get.</param>
		// Token: 0x06003F95 RID: 16277 RVA: 0x000A609D File Offset: 0x000A429D
		public ProfileGroupSettings Get(int index)
		{
			return (ProfileGroupSettings)base.BaseGet(index);
		}

		/// <summary>Returns the <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object with the specified name.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object with the specified name, or null if the name does not exist.</returns>
		/// <param name="name">The name of the <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object to get.</param>
		// Token: 0x06003F96 RID: 16278 RVA: 0x000A60AB File Offset: 0x000A42AB
		public ProfileGroupSettings Get(string name)
		{
			return (ProfileGroupSettings)base.BaseGet(name);
		}

		// Token: 0x06003F97 RID: 16279 RVA: 0x000A60B9 File Offset: 0x000A42B9
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((ProfileGroupSettings)element).Name;
		}

		/// <summary>Returns the name of the <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object at the specified index.</summary>
		/// <returns>The name of the <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object at the specified index, or null if there is no object at that index.</returns>
		/// <param name="index">The index of a <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object.</param>
		// Token: 0x06003F98 RID: 16280 RVA: 0x0009E02A File Offset: 0x0009C22A
		public string GetKey(int index)
		{
			return (string)base.BaseGetKey(index);
		}

		/// <summary>Returns the index of the specified <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object.</summary>
		/// <returns>The index of the specified <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object, or -1 if the specified <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object is not contained in the collection.</returns>
		/// <param name="group">A <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object in the collection.</param>
		// Token: 0x06003F99 RID: 16281 RVA: 0x0009D41E File Offset: 0x0009B61E
		public int IndexOf(ProfileGroupSettings group)
		{
			return base.BaseIndexOf(group);
		}

		/// <summary>Removes a <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object from the collection.</summary>
		/// <param name="name">The name of the <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object to remove from the collection.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">There is no <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object at the specified index in the collection, the element has already been removed, or the collection is read-only.</exception>
		// Token: 0x06003F9A RID: 16282 RVA: 0x0009CBBF File Offset: 0x0009ADBF
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		/// <summary>Removes a <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object from the collection.</summary>
		/// <param name="index">The index of the <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object to remove from the collection.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">There is no <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object at the specified index in the collection, the element has already been removed, or the collection is read-only. </exception>
		// Token: 0x06003F9B RID: 16283 RVA: 0x0009CBC8 File Offset: 0x0009ADC8
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		/// <summary>Adds the specified <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object to the collection.</summary>
		/// <param name="group">A <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object to add already exists in the collection, or the collection is read-only. </exception>
		// Token: 0x06003F9C RID: 16284 RVA: 0x000A60C8 File Offset: 0x000A42C8
		public void Set(ProfileGroupSettings group)
		{
			ProfileGroupSettings profileGroupSettings = this.Get(group.Name);
			if (profileGroupSettings == null)
			{
				this.Add(group);
				return;
			}
			int num = base.BaseIndexOf(profileGroupSettings);
			this.RemoveAt(num);
			this.BaseAdd(num, group);
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object at the specified index location.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object at the specified index, or null if there is no object at that index.</returns>
		/// <param name="index">The index of a <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object in the collection.</param>
		// Token: 0x17001424 RID: 5156
		public ProfileGroupSettings this[int index]
		{
			get
			{
				return this.Get(index);
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

		/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object with the specified name.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object with the specified name, or null if there is no object with that name.</returns>
		/// <param name="name">The name of a <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object in the collection.</param>
		// Token: 0x17001425 RID: 5157
		public ProfileGroupSettings this[string name]
		{
			get
			{
				return (ProfileGroupSettings)base.BaseGet(name);
			}
		}

		// Token: 0x17001426 RID: 5158
		// (get) Token: 0x06003FA0 RID: 16288 RVA: 0x000A610D File Offset: 0x000A430D
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ProfileGroupSettingsCollection.properties;
			}
		}

		// Token: 0x06003FA1 RID: 16289 RVA: 0x000A6114 File Offset: 0x000A4314
		internal void ResetInternal(ConfigurationElement parentElement)
		{
			this.Reset(parentElement);
		}

		// Token: 0x06003FA2 RID: 16290 RVA: 0x0009CB99 File Offset: 0x0009AD99
		internal void AddNewSettings(ProfileGroupSettings newSettings)
		{
			base.BaseAdd(newSettings, false);
		}

		// Token: 0x040022AC RID: 8876
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
