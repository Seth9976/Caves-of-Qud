using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Represents a collection of <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> objects. This class cannot be inherited.</summary>
	// Token: 0x0200059C RID: 1436
	[ConfigurationCollection(typeof(FormsAuthenticationUser), AddItemName = "user", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	public sealed class FormsAuthenticationUserCollection : ConfigurationElementCollection
	{
		/// <summary>Adds a <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> object to the collection.</summary>
		/// <param name="user">The <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> object to add to the collection. </param>
		/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> object already exists in the collection, or the collection is read-only.</exception>
		// Token: 0x06003CE3 RID: 15587 RVA: 0x0009D8D3 File Offset: 0x0009BAD3
		public void Add(FormsAuthenticationUser user)
		{
			this.BaseAdd(user);
		}

		/// <summary>Removes all <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> objects from the collection.</summary>
		/// <exception cref="T:System.Configuration.ConfigurationException">The collection is read-only.</exception>
		// Token: 0x06003CE4 RID: 15588 RVA: 0x0009CBA3 File Offset: 0x0009ADA3
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x06003CE5 RID: 15589 RVA: 0x0009F97A File Offset: 0x0009DB7A
		protected override ConfigurationElement CreateNewElement()
		{
			return new FormsAuthenticationUser("", "");
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> collection element at the specified index.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> that contains the user name and password.</returns>
		/// <param name="index">The collection user's index. </param>
		// Token: 0x06003CE6 RID: 15590 RVA: 0x0009F98B File Offset: 0x0009DB8B
		public FormsAuthenticationUser Get(int index)
		{
			return (FormsAuthenticationUser)base.BaseGet(index);
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> collection element with the specified name.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> object that contains the user name and password.</returns>
		/// <param name="name">The user's name. </param>
		// Token: 0x06003CE7 RID: 15591 RVA: 0x0009F999 File Offset: 0x0009DB99
		public FormsAuthenticationUser Get(string name)
		{
			return (FormsAuthenticationUser)base.BaseGet(name);
		}

		// Token: 0x06003CE8 RID: 15592 RVA: 0x0009F9A7 File Offset: 0x0009DBA7
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((FormsAuthenticationUser)element).Name;
		}

		/// <summary>Gets the key at the specified <see cref="T:System.Web.Configuration.FormsAuthenticationUserCollection" /> collection index.</summary>
		/// <returns>The key at the specified index of the <see cref="T:System.Web.Configuration.FormsAuthenticationUserCollection" />.</returns>
		/// <param name="index">The index in the collection.</param>
		// Token: 0x06003CE9 RID: 15593 RVA: 0x0009F9B4 File Offset: 0x0009DBB4
		public string GetKey(int index)
		{
			return this.Get(index).Name;
		}

		/// <summary>Removes a <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> object from the collection.</summary>
		/// <param name="name">The name of the <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> object to remove from the collection. </param>
		/// <exception cref="T:System.Configuration.ConfigurationException">There is no <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> object with the specified key in the collection, the element has already been removed, or the collection is read-only.</exception>
		// Token: 0x06003CEA RID: 15594 RVA: 0x0009CBBF File Offset: 0x0009ADBF
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		/// <summary>Removes a <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> object at the specified index from the collection.</summary>
		/// <param name="index">The index of the <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> object to remove from the collection. </param>
		/// <exception cref="T:System.Configuration.ConfigurationException">There is no <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> object at the specified index in the collection, the element has already been removed, or the collection is read only.</exception>
		// Token: 0x06003CEB RID: 15595 RVA: 0x0009CBC8 File Offset: 0x0009ADC8
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		/// <summary>Modifies the specified <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> object contained in the collection.</summary>
		/// <param name="user">The <see cref="T:System.Web.Configuration.FormsAuthenticationUserCollection" /> object that must be changed. </param>
		// Token: 0x06003CEC RID: 15596 RVA: 0x0009F9C4 File Offset: 0x0009DBC4
		public void Set(FormsAuthenticationUser user)
		{
			FormsAuthenticationUser formsAuthenticationUser = this.Get(user.Name);
			if (formsAuthenticationUser == null)
			{
				this.Add(user);
				return;
			}
			int num = base.BaseIndexOf(formsAuthenticationUser);
			this.RemoveAt(num);
			this.BaseAdd(num, user);
		}

		/// <summary>Gets all the collection's keys.</summary>
		/// <returns>The string array containing the collection keys.</returns>
		// Token: 0x170012DC RID: 4828
		// (get) Token: 0x06003CED RID: 15597 RVA: 0x0009FA00 File Offset: 0x0009DC00
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

		/// <summary>Gets the type of the <see cref="T:System.Web.Configuration.FormsAuthenticationUserCollection" />.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationElementCollectionType" /> of this collection.</returns>
		// Token: 0x170012DD RID: 4829
		// (get) Token: 0x06003CEE RID: 15598 RVA: 0x00008A4D File Offset: 0x00006C4D
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x170012DE RID: 4830
		// (get) Token: 0x06003CEF RID: 15599 RVA: 0x0009FA3A File Offset: 0x0009DC3A
		protected override string ElementName
		{
			get
			{
				return "user";
			}
		}

		// Token: 0x170012DF RID: 4831
		// (get) Token: 0x06003CF0 RID: 15600 RVA: 0x0009FA41 File Offset: 0x0009DC41
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return FormsAuthenticationUserCollection.properties;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> at the specified index.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> that contains the user name and password.</returns>
		/// <param name="index">The collection user's index. </param>
		// Token: 0x170012E0 RID: 4832
		public FormsAuthenticationUser this[int index]
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

		/// <summary>Gets the <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> with the specified name.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> object that contains the user name and password.</returns>
		/// <param name="name">The user's name. </param>
		// Token: 0x170012E1 RID: 4833
		public FormsAuthenticationUser this[string name]
		{
			get
			{
				return this.Get(name);
			}
		}

		// Token: 0x170012E2 RID: 4834
		// (get) Token: 0x06003CF4 RID: 15604 RVA: 0x00008A4D File Offset: 0x00006C4D
		protected override bool ThrowOnDuplicate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040020F0 RID: 8432
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
