using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Represents a collection of <see cref="T:System.Web.Configuration.HttpModuleAction" /> objects. This class cannot be inherited.</summary>
	// Token: 0x020005AB RID: 1451
	[ConfigurationCollection(typeof(HttpModuleAction), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public sealed class HttpModuleActionCollection : ConfigurationElementCollection
	{
		/// <summary>Adds an <see cref="T:System.Web.Configuration.HttpModuleAction" /> object to the collection.</summary>
		/// <param name="httpModule">The <see cref="T:System.Web.Configuration.HttpModuleAction" /> module to add to the collection.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">An attempt was made to add an <see cref="T:System.Web.Configuration.HttpModuleAction" /> object that already exists in the collection. </exception>
		// Token: 0x06003E12 RID: 15890 RVA: 0x0009D8D3 File Offset: 0x0009BAD3
		public void Add(HttpModuleAction httpModule)
		{
			this.BaseAdd(httpModule);
		}

		/// <summary>Removes all <see cref="T:System.Web.Configuration.HttpModuleAction" /> objects from the collection.</summary>
		// Token: 0x06003E13 RID: 15891 RVA: 0x0009CBA3 File Offset: 0x0009ADA3
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x06003E14 RID: 15892 RVA: 0x000A280C File Offset: 0x000A0A0C
		protected override ConfigurationElement CreateNewElement()
		{
			return new HttpModuleAction();
		}

		// Token: 0x06003E15 RID: 15893 RVA: 0x000A2813 File Offset: 0x000A0A13
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((HttpModuleAction)element).Name;
		}

		/// <summary>Gets the collection index of the specified <see cref="T:System.Web.Configuration.HttpModuleAction" /> module.</summary>
		/// <returns>The collection index value for the specified module.</returns>
		/// <param name="action">The <see cref="T:System.Web.Configuration.HttpModuleAction" /> module for which to get the collection index.</param>
		// Token: 0x06003E16 RID: 15894 RVA: 0x0009D41E File Offset: 0x0009B61E
		public int IndexOf(HttpModuleAction action)
		{
			return base.BaseIndexOf(action);
		}

		/// <summary>Removes an <see cref="T:System.Web.Configuration.HttpModuleAction" /> object from the collection.</summary>
		/// <param name="name">The key that identifies the <see cref="T:System.Web.Configuration.HttpModuleAction" /> object to remove from the collection.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">There is no <see cref="T:System.Web.Configuration.HttpModuleAction" /> object with the specified key in the collection, the element has already been removed, or the collection is read-only.</exception>
		// Token: 0x06003E17 RID: 15895 RVA: 0x0009CBBF File Offset: 0x0009ADBF
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		/// <summary>Removes an <see cref="T:System.Web.Configuration.HttpModuleAction" /> object from the collection.</summary>
		/// <param name="action">The <see cref="T:System.Web.Configuration.HttpModuleAction" /> module to remove.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">The passed <see cref="T:System.Web.Configuration.HttpModuleAction" /> object does not exist in the collection, the element has already been removed, or the collection is read-only.</exception>
		// Token: 0x06003E18 RID: 15896 RVA: 0x000A2820 File Offset: 0x000A0A20
		public void Remove(HttpModuleAction action)
		{
			base.BaseRemove(action.Name);
		}

		/// <summary>Removes an <see cref="T:System.Web.Configuration.HttpModuleAction" /> module at the specified index from the collection.</summary>
		/// <param name="index">The index of the <see cref="T:System.Web.Configuration.HttpModuleAction" /> module to remove.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">There is no <see cref="T:System.Web.Configuration.HttpModuleAction" /> object at the specified index in the collection, the element has already been removed, or the collection is read-only.</exception>
		// Token: 0x06003E19 RID: 15897 RVA: 0x0009CBC8 File Offset: 0x0009ADC8
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		// Token: 0x06003E1A RID: 15898 RVA: 0x000A282E File Offset: 0x000A0A2E
		protected override bool IsElementRemovable(ConfigurationElement element)
		{
			return base.IsElementRemovable(element);
		}

		// Token: 0x17001389 RID: 5001
		// (get) Token: 0x06003E1B RID: 15899 RVA: 0x000A2837 File Offset: 0x000A0A37
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return HttpModuleActionCollection.properties;
			}
		}

		/// <summary>Gets or sets an item in this collection.</summary>
		/// <returns>The item at the specified <paramref name="index" />.</returns>
		/// <param name="index">Module collection index.</param>
		// Token: 0x1700138A RID: 5002
		public HttpModuleAction this[int index]
		{
			get
			{
				return (HttpModuleAction)base.BaseGet(index);
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

		// Token: 0x04002210 RID: 8720
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
