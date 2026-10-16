using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Represents a collection of <see cref="T:System.Web.Configuration.UrlMapping" /> objects. This class cannot be inherited.</summary>
	// Token: 0x020005E4 RID: 1508
	[ConfigurationCollection(typeof(UrlMapping), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public sealed class UrlMappingCollection : ConfigurationElementCollection
	{
		/// <summary>Adds the specified <see cref="T:System.Web.Configuration.UrlMapping" /> to the collection.</summary>
		/// <param name="urlMapping">The <see cref="T:System.Web.Configuration.UrlMapping" /> object to add to the collection.</param>
		// Token: 0x06004168 RID: 16744 RVA: 0x0009D8D3 File Offset: 0x0009BAD3
		public void Add(UrlMapping urlMapping)
		{
			this.BaseAdd(urlMapping);
		}

		/// <summary>Removes all the <see cref="T:System.Web.Configuration.UrlMapping" /> objects from the collection.</summary>
		// Token: 0x06004169 RID: 16745 RVA: 0x0009CBA3 File Offset: 0x0009ADA3
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x0600416A RID: 16746 RVA: 0x000A91FE File Offset: 0x000A73FE
		protected override ConfigurationElement CreateNewElement()
		{
			return new UrlMapping();
		}

		// Token: 0x0600416B RID: 16747 RVA: 0x000A9205 File Offset: 0x000A7405
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((UrlMapping)element).Url;
		}

		/// <summary>Gets the collection key of the specified element.</summary>
		/// <returns>A string representing the value of the key. </returns>
		/// <param name="index">The collection index of the element to get.</param>
		// Token: 0x0600416C RID: 16748 RVA: 0x0009E02A File Offset: 0x0009C22A
		public string GetKey(int index)
		{
			return (string)base.BaseGetKey(index);
		}

		/// <summary>Removes the <see cref="T:System.Web.Configuration.UrlMapping" /> object with the specified name from the collection.</summary>
		/// <param name="name">The name of the <see cref="T:System.Web.Configuration.UrlMapping" /> object to remove from the collection.</param>
		// Token: 0x0600416D RID: 16749 RVA: 0x0009CBBF File Offset: 0x0009ADBF
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		/// <summary>Removes the specified <see cref="T:System.Web.Configuration.UrlMapping" /> object from the collection.</summary>
		/// <param name="urlMapping">The <see cref="T:System.Web.Configuration.UrlMapping" /> object to remove from the collection.</param>
		// Token: 0x0600416E RID: 16750 RVA: 0x000A9212 File Offset: 0x000A7412
		public void Remove(UrlMapping urlMapping)
		{
			base.BaseRemove(urlMapping.Url);
		}

		/// <summary>Removes the <see cref="T:System.Web.Configuration.UrlMapping" /> object with the specified index from the collection.</summary>
		/// <param name="index">The collection index of the <see cref="T:System.Web.Configuration.UrlMapping" /> object to remove.</param>
		// Token: 0x0600416F RID: 16751 RVA: 0x0009CBC8 File Offset: 0x0009ADC8
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		/// <summary>Gets an array of the keys for all of the configuration elements contained in the <see cref="T:System.Web.Configuration.UrlMappingCollection" />.</summary>
		/// <returns>An array of the keys for all of the <see cref="T:System.Web.Configuration.UrlMapping" /> objects contained in the <see cref="T:System.Web.Configuration.UrlMappingCollection" />.</returns>
		// Token: 0x170014E6 RID: 5350
		// (get) Token: 0x06004170 RID: 16752 RVA: 0x000A9220 File Offset: 0x000A7420
		public string[] AllKeys
		{
			get
			{
				string[] array = new string[base.Count];
				for (int i = 0; i < base.Count; i++)
				{
					array[i] = this[i].Url;
				}
				return array;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.UrlMapping" /> object at the specified index.</summary>
		/// <returns>The object at the specified index.</returns>
		/// <param name="index">The index of the object to get.</param>
		// Token: 0x170014E7 RID: 5351
		public UrlMapping this[int index]
		{
			get
			{
				return (UrlMapping)base.BaseGet(index);
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

		/// <summary>Gets the <see cref="T:System.Web.Configuration.UrlMapping" /> object with the specified name.</summary>
		/// <returns>The collection object that has the specified name.</returns>
		/// <param name="name">The name of the collection object.</param>
		// Token: 0x170014E8 RID: 5352
		public UrlMapping this[string name]
		{
			get
			{
				return (UrlMapping)base.BaseGet(name);
			}
		}

		// Token: 0x170014E9 RID: 5353
		// (get) Token: 0x06004174 RID: 16756 RVA: 0x000A9276 File Offset: 0x000A7476
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return UrlMappingCollection.properties;
			}
		}

		// Token: 0x04002338 RID: 9016
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
