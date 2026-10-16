using System;
using System.Configuration;
using System.Xml;

namespace System.Web.Configuration
{
	/// <summary>Contains a set of <see cref="T:System.Web.Configuration.ProfilePropertySettingsCollection" /> objects.</summary>
	// Token: 0x020005C6 RID: 1478
	[ConfigurationCollection(typeof(ProfilePropertySettings), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public class ProfilePropertySettingsCollection : ConfigurationElementCollection
	{
		/// <summary>Adds a <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object to the collection.</summary>
		/// <param name="propertySettings">A <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object to add to the collection.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object to add already exists in the collection or the collection is read only.</exception>
		// Token: 0x06003FBC RID: 16316 RVA: 0x0009D8D3 File Offset: 0x0009BAD3
		public void Add(ProfilePropertySettings propertySettings)
		{
			this.BaseAdd(propertySettings);
		}

		/// <summary>Removes all <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> objects from the collection.</summary>
		// Token: 0x06003FBD RID: 16317 RVA: 0x0009CBA3 File Offset: 0x0009ADA3
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x06003FBE RID: 16318 RVA: 0x000A64AA File Offset: 0x000A46AA
		protected override ConfigurationElement CreateNewElement()
		{
			return new ProfilePropertySettings();
		}

		/// <summary>Returns the <see cref="T:System.Web.Configuration.ProfileSection" /> object at the specified index.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.ProfileSection" /> object at the specified index, or null if there is no object at that index.</returns>
		/// <param name="index">The index of the <see cref="T:System.Web.Configuration.ProfileSection" /> to get.</param>
		// Token: 0x06003FBF RID: 16319 RVA: 0x000A64B1 File Offset: 0x000A46B1
		public ProfilePropertySettings Get(int index)
		{
			return (ProfilePropertySettings)base.BaseGet(index);
		}

		/// <summary>Returns the <see cref="T:System.Web.Configuration.ProfileSection" /> object with the specified name.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.ProfileSection" /> object with the specified name, or null if the name does not exist.</returns>
		/// <param name="name">The name of the <see cref="T:System.Web.Configuration.ProfileSection" /> to get.</param>
		// Token: 0x06003FC0 RID: 16320 RVA: 0x000A64BF File Offset: 0x000A46BF
		public ProfilePropertySettings Get(string name)
		{
			return (ProfilePropertySettings)base.BaseGet(name);
		}

		/// <summary>Gets the key for the specified configuration element.</summary>
		/// <returns>The name of the element.</returns>
		/// <param name="element">A <see cref="T:System.Configuration.ConfigurationElement" /> in the collection.</param>
		// Token: 0x06003FC1 RID: 16321 RVA: 0x000A64CD File Offset: 0x000A46CD
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((ProfilePropertySettings)element).Name;
		}

		/// <summary>Handles the reading of unrecognized configuration elements from a configuration file and causes the configuration system to throw an exception if the element cannot be handled.</summary>
		/// <returns>true if the unrecognized element was deserialized successfully; otherwise, false.</returns>
		/// <param name="elementName">The name of the unrecognized element.</param>
		/// <param name="reader">An input stream that reads XML from the configuration file.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">
		///   <paramref name="elementName" /> equals "clear"- or -<paramref name="elementName" /> equals "group".</exception>
		// Token: 0x06003FC2 RID: 16322 RVA: 0x000A64DA File Offset: 0x000A46DA
		protected override bool OnDeserializeUnrecognizedElement(string elementName, XmlReader reader)
		{
			return base.OnDeserializeUnrecognizedElement(elementName, reader);
		}

		/// <summary>Gets the name of the <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> at the specified index location.</summary>
		/// <returns>The name of the <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> at the specified index location.</returns>
		/// <param name="index">The index of a <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> in the collection.</param>
		// Token: 0x06003FC3 RID: 16323 RVA: 0x000A64E4 File Offset: 0x000A46E4
		public string GetKey(int index)
		{
			ProfilePropertySettings profilePropertySettings = this.Get(index);
			if (profilePropertySettings == null)
			{
				return null;
			}
			return profilePropertySettings.Name;
		}

		/// <summary>Returns the index of the specified <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object.</summary>
		/// <returns>The index of the specified <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object, or -1 if the object is not found in the collection.</returns>
		/// <param name="propertySettings">A <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object in the collection.</param>
		// Token: 0x06003FC4 RID: 16324 RVA: 0x0009D41E File Offset: 0x0009B61E
		public int IndexOf(ProfilePropertySettings propertySettings)
		{
			return base.BaseIndexOf(propertySettings);
		}

		/// <summary>Removes a <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object from the collection.</summary>
		/// <param name="name">The name of a <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object to remove from the collection.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">There is no <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object with the specified key in the collection.- or -The element has already been removed.- or -The collection is read-only.</exception>
		// Token: 0x06003FC5 RID: 16325 RVA: 0x0009CBBF File Offset: 0x0009ADBF
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		/// <summary>Removes a <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object at the specified index location from the collection.</summary>
		/// <param name="index">The index of a <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object in the collection.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">There is no <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object at the specified index in the collection.- or -The element has already been removed.- or -The collection is read-only. </exception>
		// Token: 0x06003FC6 RID: 16326 RVA: 0x0009CBC8 File Offset: 0x0009ADC8
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		/// <summary>Adds the specified <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object to the collection.</summary>
		/// <param name="propertySettings">A <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object to add already exists in the collection, or the collection is read-only.</exception>
		// Token: 0x06003FC7 RID: 16327 RVA: 0x000A6504 File Offset: 0x000A4704
		public void Set(ProfilePropertySettings propertySettings)
		{
			ProfilePropertySettings profilePropertySettings = this.Get(propertySettings.Name);
			if (profilePropertySettings == null)
			{
				this.Add(propertySettings);
				return;
			}
			int num = base.BaseIndexOf(profilePropertySettings);
			this.RemoveAt(num);
			this.BaseAdd(num, propertySettings);
		}

		/// <summary>Returns an array containing the names of all the <see cref="T:System.Web.Configuration.ProfileSection" /> objects contained in the collection.</summary>
		/// <returns>An array containing the names of all the <see cref="T:System.Web.Configuration.ProfileSection" /> objects contained in the collection or an empty array if the collection is empty.</returns>
		// Token: 0x17001430 RID: 5168
		// (get) Token: 0x06003FC8 RID: 16328 RVA: 0x000A6540 File Offset: 0x000A4740
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

		/// <summary>Gets a value indicating whether the &lt;clear&gt; element is valid as a <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x17001431 RID: 5169
		// (get) Token: 0x06003FC9 RID: 16329 RVA: 0x00008A4D File Offset: 0x00006C4D
		protected virtual bool AllowClear
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object at the specified index location.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object at the specified index location.</returns>
		/// <param name="index">The index of a <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object in the collection.</param>
		// Token: 0x17001432 RID: 5170
		public ProfilePropertySettings this[int index]
		{
			get
			{
				return this.Get(index);
			}
			set
			{
				if (this.Get(index) != null)
				{
					base.BaseRemoveAt(index);
				}
				this.BaseAdd(index, value);
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object with the specified name.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object with the specified name.</returns>
		/// <param name="name">The name of a <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object in the collection. </param>
		// Token: 0x17001433 RID: 5171
		public ProfilePropertySettings this[string name]
		{
			get
			{
				return (ProfilePropertySettings)base.BaseGet(name);
			}
		}

		/// <summary>Gets a value indicating whether an error should be thrown if an attempt to create a duplicate object is made.</summary>
		/// <returns>true in all cases.</returns>
		// Token: 0x17001434 RID: 5172
		// (get) Token: 0x06003FCD RID: 16333 RVA: 0x00008B4A File Offset: 0x00006D4A
		protected override bool ThrowOnDuplicate
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a collection of configuration properties.</summary>
		/// <returns>A collection of configuration properties.</returns>
		// Token: 0x17001435 RID: 5173
		// (get) Token: 0x06003FCE RID: 16334 RVA: 0x000A659D File Offset: 0x000A479D
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ProfilePropertySettingsCollection.properties;
			}
		}

		// Token: 0x040022B6 RID: 8886
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
