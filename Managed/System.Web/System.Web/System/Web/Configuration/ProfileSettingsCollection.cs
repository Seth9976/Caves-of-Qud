using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Contains a collection of <see cref="T:System.Web.Configuration.ProfileSettings" /> objects. This class cannot be inherited.</summary>
	// Token: 0x020005C9 RID: 1481
	[ConfigurationCollection(typeof(ProfileSettings), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public sealed class ProfileSettingsCollection : ConfigurationElementCollection
	{
		/// <summary>Adds a <see cref="T:System.Web.Configuration.ProfileSettings" /> object to the collection.</summary>
		/// <param name="profilesSettings">A <see cref="T:System.Web.Configuration.ProfileSettings" /> object to add to the collection.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.ProfileSettings" /> object to add already exists in the collection, or the collection is read-only.</exception>
		// Token: 0x06003FEE RID: 16366 RVA: 0x0009D8D3 File Offset: 0x0009BAD3
		public void Add(ProfileSettings profilesSettings)
		{
			this.BaseAdd(profilesSettings);
		}

		/// <summary>Removes all <see cref="T:System.Web.Configuration.ProfileSettings" /> objects from the collection.</summary>
		// Token: 0x06003FEF RID: 16367 RVA: 0x0009CBA3 File Offset: 0x0009ADA3
		public void Clear()
		{
			base.BaseClear();
		}

		/// <summary>Indicates whether the collection contains a <see cref="T:System.Web.Configuration.ProfileSettings" /> object with the specified name.</summary>
		/// <returns>true if the collection contains a <see cref="T:System.Web.Configuration.ProfileSettings" /> object with the specified <paramref name="name" />; otherwise, false.</returns>
		/// <param name="name">The name of a <see cref="T:System.Web.Configuration.ProfileSettings" /> object in the collection.</param>
		// Token: 0x06003FF0 RID: 16368 RVA: 0x0009F12D File Offset: 0x0009D32D
		public bool Contains(string name)
		{
			return base.BaseGet(name) != null;
		}

		// Token: 0x06003FF1 RID: 16369 RVA: 0x000A69F5 File Offset: 0x000A4BF5
		protected override ConfigurationElement CreateNewElement()
		{
			return new ProfileSettings();
		}

		// Token: 0x06003FF2 RID: 16370 RVA: 0x000A69FC File Offset: 0x000A4BFC
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((ProfileSettings)element).Name;
		}

		/// <summary>Returns the index of the specified <see cref="T:System.Web.Configuration.ProfileSettings" /> object.</summary>
		/// <returns>The index of the specified <see cref="T:System.Web.Configuration.ProfileSettings" /> object, or -1 if the object is not found in the collection.</returns>
		/// <param name="name">The name of a <see cref="T:System.Web.Configuration.ProfileSettings" /> object in the collection.</param>
		// Token: 0x06003FF3 RID: 16371 RVA: 0x000A6A0C File Offset: 0x000A4C0C
		public int IndexOf(string name)
		{
			ProfileSettings profileSettings = (ProfileSettings)base.BaseGet(name);
			if (profileSettings == null)
			{
				return -1;
			}
			return base.BaseIndexOf(profileSettings);
		}

		/// <summary>Inserts the specified <see cref="T:System.Web.Configuration.ProfileSettings" /> object at the specified index in the collection.</summary>
		/// <param name="index">The index of a <see cref="T:System.Web.Configuration.ProfileSettings" /> object in the collection.</param>
		/// <param name="authorizationSettings">A <see cref="T:System.Web.Configuration.ProfileSettings" /> object to insert into the collection.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.ProfileSettings" /> object to add already exists in the collection, the index is invalid, or the collection is read-only.</exception>
		// Token: 0x06003FF4 RID: 16372 RVA: 0x0009F176 File Offset: 0x0009D376
		[global::System.MonoTODO("why did they use 'Insert' and not 'Add' as other collections do?")]
		public void Insert(int index, ProfileSettings authorizationSettings)
		{
			this.BaseAdd(index, authorizationSettings);
		}

		/// <summary>Removes a <see cref="T:System.Web.Configuration.BufferModeSettings" /> object from the collection.</summary>
		/// <param name="name">The name of a <see cref="T:System.Web.Configuration.ProfileSettings" /> object to remove from the collection.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">There is no <see cref="T:System.Web.Configuration.ProfileSettings" /> object with the specified key in the collection, the element has already been removed, or the collection is read-only.</exception>
		// Token: 0x06003FF5 RID: 16373 RVA: 0x0009CBBF File Offset: 0x0009ADBF
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		/// <summary>Removes a <see cref="T:System.Web.Configuration.ProfileSettings" /> object at the specified index location from the collection.</summary>
		/// <param name="index">The index of a <see cref="T:System.Web.Configuration.ProfileSettings" /> object in the collection.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">There is no <see cref="T:System.Web.Configuration.ProfileSettings" /> object at the specified index in the collection, the element has already been removed, or the collection is read-only.</exception>
		// Token: 0x06003FF6 RID: 16374 RVA: 0x0009CBC8 File Offset: 0x0009ADC8
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.ProfileSettings" /> object based on the specified key in the collection.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.ProfileSettings" /> object.</returns>
		/// <param name="key">The name of the <see cref="T:System.Web.Configuration.ProfileSettings" /> object contained in the collection.</param>
		// Token: 0x17001443 RID: 5187
		public ProfileSettings this[string key]
		{
			get
			{
				return (ProfileSettings)base.BaseGet(key);
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.ProfileSettings" /> object at the specified numeric index in the collection.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.ProfileSettings" /> object at the specified index, or null if there is no object at that index.</returns>
		/// <param name="index">The index of a <see cref="T:System.Web.Configuration.ProfileSettings" /> object in the collection.</param>
		// Token: 0x17001444 RID: 5188
		public ProfileSettings this[int index]
		{
			get
			{
				return (ProfileSettings)base.BaseGet(index);
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

		// Token: 0x17001445 RID: 5189
		// (get) Token: 0x06003FFA RID: 16378 RVA: 0x000A6A4E File Offset: 0x000A4C4E
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ProfileSettingsCollection.properties;
			}
		}

		// Token: 0x040022C4 RID: 8900
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
