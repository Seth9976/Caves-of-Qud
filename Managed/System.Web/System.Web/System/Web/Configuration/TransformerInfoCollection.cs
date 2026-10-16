using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Contains a collection of <see cref="T:System.Web.Configuration.TransformerInfo" /> objects. This class cannot be inherited.</summary>
	// Token: 0x020005DF RID: 1503
	[ConfigurationCollection(typeof(TransformerInfo), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public sealed class TransformerInfoCollection : ConfigurationElementCollection
	{
		/// <summary>Adds a <see cref="T:System.Web.Configuration.TransformerInfo" /> object to the collection.</summary>
		/// <param name="transformerInfo">A <see cref="T:System.Web.Configuration.TransformerInfo" /> object to add to the collection.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.TransformerInfo" /> object to add already exists in the collection.- or -The collection is read-only. </exception>
		// Token: 0x0600412B RID: 16683 RVA: 0x0009D8D3 File Offset: 0x0009BAD3
		public void Add(TransformerInfo transformerInfo)
		{
			this.BaseAdd(transformerInfo);
		}

		/// <summary>Removes all <see cref="T:System.Web.Configuration.TransformerInfo" /> objects from the collection.</summary>
		// Token: 0x0600412C RID: 16684 RVA: 0x0009CBA3 File Offset: 0x0009ADA3
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x0600412D RID: 16685 RVA: 0x000A8E0A File Offset: 0x000A700A
		protected override ConfigurationElement CreateNewElement()
		{
			return new TransformerInfo("", "");
		}

		// Token: 0x0600412E RID: 16686 RVA: 0x000A8E1B File Offset: 0x000A701B
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((TransformerInfo)element).Name;
		}

		/// <summary>Removes the <see cref="T:System.Web.Configuration.TransformerInfo" /> object with the passed <see cref="P:System.Web.Configuration.TransformerInfo.Name" /> property value from the collection.</summary>
		/// <param name="s">The name of a <see cref="T:System.Web.Configuration.TransformerInfo" /> object to remove from the collection.</param>
		// Token: 0x0600412F RID: 16687 RVA: 0x0009CBBF File Offset: 0x0009ADBF
		public void Remove(string s)
		{
			base.BaseRemove(s);
		}

		/// <summary>Removes the <see cref="T:System.Web.Configuration.TransformerInfo" /> object from the collection at the passed index.</summary>
		/// <param name="index">The index of a <see cref="T:System.Web.Configuration.TransformerInfo" /> object to remove from the collection.</param>
		// Token: 0x06004130 RID: 16688 RVA: 0x0009CBC8 File Offset: 0x0009ADC8
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.TagMapInfo" /> object at the specified index location.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.TransformerInfo" /> object at the specified index, or null if there is no object at that index.</returns>
		/// <param name="index">The index of a <see cref="T:System.Web.Configuration.TransformerInfo" /> object in the collection.</param>
		// Token: 0x170014D1 RID: 5329
		public TransformerInfo this[int index]
		{
			get
			{
				return (TransformerInfo)base.BaseGet(index);
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

		// Token: 0x170014D2 RID: 5330
		// (get) Token: 0x06004133 RID: 16691 RVA: 0x000A8E36 File Offset: 0x000A7036
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return TransformerInfoCollection.properties;
			}
		}

		// Token: 0x0400232C RID: 9004
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
