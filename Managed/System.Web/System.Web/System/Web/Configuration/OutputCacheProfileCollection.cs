using System;
using System.Collections;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Represents a collection of <see cref="T:System.Web.Configuration.OutputCacheProfile" /> objects. This class cannot be inherited.</summary>
	// Token: 0x020005BB RID: 1467
	[ConfigurationCollection(typeof(OutputCacheProfile), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public sealed class OutputCacheProfileCollection : ConfigurationElementCollection, ICollection, IEnumerable
	{
		/// <summary>Adds a <see cref="T:System.Web.Configuration.OutputCacheProfile" /> object to the collection.</summary>
		/// <param name="name">The name of the <see cref="T:System.Web.Configuration.OutputCacheProfile" /> object to add to the collection.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.OutputCacheProfile" /> object already exists in the collection or the collection is read only.</exception>
		// Token: 0x06003EE7 RID: 16103 RVA: 0x0009D8D3 File Offset: 0x0009BAD3
		public void Add(OutputCacheProfile name)
		{
			this.BaseAdd(name);
		}

		/// <summary>Removes all the  <see cref="T:System.Web.Configuration.OutputCacheProfile" /> objects from the collection.</summary>
		/// <exception cref="T:System.Configuration.ConfigurationException">The collection is read only.</exception>
		// Token: 0x06003EE8 RID: 16104 RVA: 0x0009CBA3 File Offset: 0x0009ADA3
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x06003EE9 RID: 16105 RVA: 0x000A4805 File Offset: 0x000A2A05
		protected override ConfigurationElement CreateNewElement()
		{
			return new OutputCacheProfile();
		}

		// Token: 0x06003EEA RID: 16106 RVA: 0x000A480C File Offset: 0x000A2A0C
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((OutputCacheProfile)element).Name;
		}

		/// <summary>Gets the key at the specified <see cref="T:System.Web.Configuration.OutputCacheProfileCollection" /> index.</summary>
		/// <returns>The key with the specified <see cref="T:System.Web.Configuration.OutputCacheProfileCollection" /> index.</returns>
		/// <param name="index">The <see cref="T:System.Web.Configuration.OutputCacheProfileCollection" /> index of the key. </param>
		// Token: 0x06003EEB RID: 16107 RVA: 0x0009E02A File Offset: 0x0009C22A
		public string GetKey(int index)
		{
			return (string)base.BaseGetKey(index);
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.OutputCacheProfile" /> element with the specified name.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.OutputCacheProfile" /> element with the specified name.</returns>
		/// <param name="name">The name of the <see cref="T:System.Web.Configuration.OutputCacheProfileCollection" /> element.</param>
		// Token: 0x06003EEC RID: 16108 RVA: 0x000A4819 File Offset: 0x000A2A19
		public OutputCacheProfile Get(string name)
		{
			return (OutputCacheProfile)base.BaseGet(name);
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.OutputCacheProfile" /> element at the specified index.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.OutputCacheProfile" /> element at the specified index.</returns>
		/// <param name="index">The index of the <see cref="T:System.Web.Configuration.OutputCacheProfileCollection" /> element. </param>
		// Token: 0x06003EED RID: 16109 RVA: 0x000A4827 File Offset: 0x000A2A27
		public OutputCacheProfile Get(int index)
		{
			return (OutputCacheProfile)base.BaseGet(index);
		}

		/// <summary>Removes the <see cref="T:System.Web.Configuration.OutputCacheProfile" /> object with the specified name from the collection.</summary>
		/// <param name="name">The name of the <see cref="T:System.Web.Configuration.OutputCacheProfile" /> element to remove from the collection. </param>
		/// <exception cref="T:System.Configuration.ConfigurationException">There is no <see cref="T:System.Web.Configuration.OutputCacheProfile" /> object with the specified key in the collection, the element has already been removed, or the collection is read-only.</exception>
		// Token: 0x06003EEE RID: 16110 RVA: 0x0009CBBF File Offset: 0x0009ADBF
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		/// <summary>Removes the <see cref="T:System.Web.Configuration.OutputCacheProfile" /> object at the specified index from the collection.</summary>
		/// <param name="index">The index of the <see cref="T:System.Web.Configuration.OutputCacheProfile" /> element to remove from the collection.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">There is no <see cref="T:System.Web.Configuration.OutputCacheProfile" /> object with the specified key in the collection, the element has already been removed, or the collection is read-only.</exception>
		// Token: 0x06003EEF RID: 16111 RVA: 0x0009CBC8 File Offset: 0x0009ADC8
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		/// <summary>Sets the specified <see cref="T:System.Web.Configuration.OutputCacheProfile" /> object. </summary>
		/// <param name="user">The <see cref="T:System.Web.Configuration.OutputCacheProfileCollection" /> element to set.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.OutputCacheProfileCollection" /> is read-only.</exception>
		// Token: 0x06003EF0 RID: 16112 RVA: 0x000A4838 File Offset: 0x000A2A38
		public void Set(OutputCacheProfile user)
		{
			OutputCacheProfile outputCacheProfile = this.Get(user.Name);
			if (outputCacheProfile == null)
			{
				this.Add(user);
				return;
			}
			int num = base.BaseIndexOf(outputCacheProfile);
			this.RemoveAt(num);
			this.BaseAdd(num, user);
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.OutputCacheProfileCollection" /> keys.</summary>
		/// <returns>The string array containing the collection keys.</returns>
		// Token: 0x170013D7 RID: 5079
		// (get) Token: 0x06003EF1 RID: 16113 RVA: 0x000A4874 File Offset: 0x000A2A74
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

		/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.OutputCacheProfile" /> object at the specified index.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.OutputCacheProfile" /> at the specified index.</returns>
		/// <param name="index">The collection index of the <see cref="T:System.Web.Configuration.OutputCacheProfile" /> object </param>
		// Token: 0x170013D8 RID: 5080
		public OutputCacheProfile this[int index]
		{
			get
			{
				return (OutputCacheProfile)base.BaseGet(index);
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

		/// <summary>Gets the <see cref="T:System.Web.Configuration.OutputCacheProfile" /> with the specified name.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.OutputCacheProfile" /> with the specified name.</returns>
		/// <param name="name">The name of the <see cref="T:System.Web.Configuration.OutputCacheProfile" /> object.</param>
		// Token: 0x170013D9 RID: 5081
		public OutputCacheProfile this[string name]
		{
			get
			{
				return (OutputCacheProfile)base.BaseGet(name);
			}
		}

		// Token: 0x170013DA RID: 5082
		// (get) Token: 0x06003EF5 RID: 16117 RVA: 0x000A48AE File Offset: 0x000A2AAE
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return OutputCacheProfileCollection.properties;
			}
		}

		// Token: 0x04002265 RID: 8805
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
