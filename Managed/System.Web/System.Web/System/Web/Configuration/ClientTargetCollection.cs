using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Represents a collection of <see cref="T:System.Web.Configuration.ClientTarget" /> objects. This class cannot be inherited.</summary>
	// Token: 0x0200058A RID: 1418
	[ConfigurationCollection(typeof(ClientTarget), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public sealed class ClientTargetCollection : ConfigurationElementCollection
	{
		/// <summary>Adds the specified <see cref="T:System.Web.Configuration.ClientTarget" /> object to the collection.</summary>
		/// <param name="clientTarget">The <see cref="T:System.Web.Configuration.ClientTarget" /> to add to the collection.</param>
		// Token: 0x06003BE3 RID: 15331 RVA: 0x0009D8D3 File Offset: 0x0009BAD3
		public void Add(ClientTarget clientTarget)
		{
			this.BaseAdd(clientTarget);
		}

		/// <summary>Removes all the <see cref="T:System.Web.Configuration.ClientTarget" /> objects from the collection.</summary>
		// Token: 0x06003BE4 RID: 15332 RVA: 0x0009CBA3 File Offset: 0x0009ADA3
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x06003BE5 RID: 15333 RVA: 0x0009E014 File Offset: 0x0009C214
		protected override ConfigurationElement CreateNewElement()
		{
			return new ClientTarget(null, null);
		}

		// Token: 0x06003BE6 RID: 15334 RVA: 0x0009E01D File Offset: 0x0009C21D
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((ClientTarget)element).Alias;
		}

		/// <summary>Gets the collection key for the specified element.</summary>
		/// <returns>A string containing the value of the key.</returns>
		/// <param name="index">The collection index of the element to get.</param>
		// Token: 0x06003BE7 RID: 15335 RVA: 0x0009E02A File Offset: 0x0009C22A
		public string GetKey(int index)
		{
			return (string)base.BaseGetKey(index);
		}

		/// <summary>Removes the <see cref="T:System.Web.Configuration.ClientTarget" /> object with the specified alias from the collection.</summary>
		/// <param name="name">The alias of the <see cref="T:System.Web.Configuration.ClientTarget" /> to remove.</param>
		// Token: 0x06003BE8 RID: 15336 RVA: 0x0009CBBF File Offset: 0x0009ADBF
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		/// <summary>Removes the specified <see cref="T:System.Web.Configuration.ClientTarget" /> object from the collection.</summary>
		/// <param name="clientTarget">The <see cref="T:System.Web.Configuration.ClientTarget" /> to remove.</param>
		// Token: 0x06003BE9 RID: 15337 RVA: 0x0009E038 File Offset: 0x0009C238
		public void Remove(ClientTarget clientTarget)
		{
			base.BaseRemove(clientTarget.Alias);
		}

		/// <summary>Removes the <see cref="T:System.Web.Configuration.ClientTarget" /> object with the specified collection index.</summary>
		/// <param name="index">The collection index of the <see cref="T:System.Web.Configuration.ClientTarget" /> to remove.</param>
		// Token: 0x06003BEA RID: 15338 RVA: 0x0009CBC8 File Offset: 0x0009ADC8
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		/// <summary>Returns an array of the keys for all the configuration elements contained in the <see cref="T:System.Web.Configuration.ClientTargetCollection" /> collection.</summary>
		/// <returns>The array of the keys for all of the <see cref="T:System.Web.Configuration.ClientTarget" /> objects contained in the <see cref="T:System.Web.Configuration.ClientTargetCollection" />.</returns>
		// Token: 0x17001274 RID: 4724
		// (get) Token: 0x06003BEB RID: 15339 RVA: 0x0009E048 File Offset: 0x0009C248
		public string[] AllKeys
		{
			get
			{
				string[] array = new string[base.Count];
				for (int i = 0; i < base.Count; i++)
				{
					array[i] = this[i].Alias;
				}
				return array;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.ClientTarget" /> object at the specified index.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.ClientTarget" /> object at the specified index.</returns>
		/// <param name="index">The collection index of the object.</param>
		// Token: 0x17001275 RID: 4725
		public ClientTarget this[int index]
		{
			get
			{
				return (ClientTarget)base.BaseGet(index);
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

		/// <summary>Gets the <see cref="T:System.Web.Configuration.ClientTarget" /> object with the specified name.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.ClientTarget" /> object with the specified name.</returns>
		/// <param name="name">The user agent's name.</param>
		// Token: 0x17001276 RID: 4726
		public ClientTarget this[string name]
		{
			get
			{
				return (ClientTarget)base.BaseGet(name);
			}
		}

		// Token: 0x17001277 RID: 4727
		// (get) Token: 0x06003BEF RID: 15343 RVA: 0x0009E09E File Offset: 0x0009C29E
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ClientTargetCollection.properties;
			}
		}

		// Token: 0x040020A6 RID: 8358
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
