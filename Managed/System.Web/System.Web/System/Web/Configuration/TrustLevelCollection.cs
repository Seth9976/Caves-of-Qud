using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Contains a collection of <see cref="T:System.Web.Configuration.TrustLevel" /> objects. This class cannot be inherited.</summary>
	// Token: 0x020005E1 RID: 1505
	[ConfigurationCollection(typeof(TrustLevel), AddItemName = "trustLevel")]
	public sealed class TrustLevelCollection : ConfigurationElementCollection
	{
		/// <summary>Adds a <see cref="T:System.Web.Configuration.TrustLevel" /> object to the collection.</summary>
		/// <param name="trustLevel">The <see cref="T:System.Web.Configuration.TrustLevel" /> to add to the collection.</param>
		// Token: 0x0600413E RID: 16702 RVA: 0x0009D8D3 File Offset: 0x0009BAD3
		public void Add(TrustLevel trustLevel)
		{
			this.BaseAdd(trustLevel);
		}

		/// <summary>Removes all <see cref="T:System.Web.Configuration.TrustLevel" /> objects from the collection.</summary>
		// Token: 0x0600413F RID: 16703 RVA: 0x0009CBA3 File Offset: 0x0009ADA3
		public void Clear()
		{
			base.BaseClear();
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.TrustLevel" /> object at the specified index.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.TrustLevel" /> at the specified index.</returns>
		/// <param name="index">The index of the <see cref="T:System.Web.Configuration.TrustLevelCollection" />.</param>
		// Token: 0x06004140 RID: 16704 RVA: 0x000A8F30 File Offset: 0x000A7130
		public TrustLevel Get(int index)
		{
			return (TrustLevel)base.BaseGet(index);
		}

		/// <summary>Removes a <see cref="T:System.Web.Configuration.TrustLevel" /> object from the <see cref="T:System.Web.Configuration.TrustLevelCollection" /> object.</summary>
		/// <param name="trustLevel">The <see cref="T:System.Web.Configuration.TrustLevel" /> to remove from the <see cref="T:System.Web.Configuration.TrustLevelCollection" />.</param>
		// Token: 0x06004141 RID: 16705 RVA: 0x000A8F3E File Offset: 0x000A713E
		public void Remove(TrustLevel trustLevel)
		{
			base.BaseRemove(trustLevel.Name);
		}

		/// <summary>Removes a <see cref="T:System.Web.Configuration.TrustLevel" /> object at the specified index location from the <see cref="T:System.Web.Configuration.TrustLevelCollection" /> object.</summary>
		/// <param name="index">The index location of the <see cref="T:System.Web.Configuration.TrustLevel" /> to remove from the <see cref="T:System.Web.Configuration.TrustLevelCollection" />.</param>
		// Token: 0x06004142 RID: 16706 RVA: 0x0009CBC8 File Offset: 0x0009ADC8
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		/// <summary>Adds the specified <see cref="T:System.Web.Configuration.TrustLevel" /> object to the <see cref="T:System.Web.Configuration.TrustLevelCollection" /> object at the specified index.</summary>
		/// <param name="index">The index location of the <see cref="T:System.Web.Configuration.TrustLevel" /> to be set within the <see cref="T:System.Web.Configuration.TrustLevelCollection" />.</param>
		/// <param name="trustLevel">The <see cref="T:System.Web.Configuration.TrustLevel" /> to be set within the <see cref="T:System.Web.Configuration.TrustLevelCollection" />.</param>
		// Token: 0x06004143 RID: 16707 RVA: 0x0009CBDF File Offset: 0x0009ADDF
		public void Set(int index, TrustLevel trustLevel)
		{
			if (base.BaseGet(index) != null)
			{
				base.BaseRemoveAt(index);
			}
			this.BaseAdd(index, trustLevel);
		}

		// Token: 0x06004144 RID: 16708 RVA: 0x000A8F4C File Offset: 0x000A714C
		protected override ConfigurationElement CreateNewElement()
		{
			return new TrustLevel();
		}

		// Token: 0x06004145 RID: 16709 RVA: 0x000A8F53 File Offset: 0x000A7153
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((TrustLevel)element).Name;
		}

		// Token: 0x06004146 RID: 16710 RVA: 0x000A8F60 File Offset: 0x000A7160
		protected override bool IsElementName(string elementname)
		{
			return elementname == "trustlevel";
		}

		/// <summary>Gets the type of the <see cref="T:System.Web.Configuration.TrustLevelCollection" /> object.</summary>
		/// <returns>A value from the <see cref="T:System.Configuration.ConfigurationElementCollectionType" /> enumeration representing the type of the collection.</returns>
		// Token: 0x170014D6 RID: 5334
		// (get) Token: 0x06004147 RID: 16711 RVA: 0x00008A4D File Offset: 0x00006C4D
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x170014D7 RID: 5335
		// (get) Token: 0x06004148 RID: 16712 RVA: 0x000A8F6D File Offset: 0x000A716D
		protected override string ElementName
		{
			get
			{
				return "trustLevel";
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.TrustLevel" /> object at the specified index.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.TrustLevel" /> that exists at the specified index of the <see cref="T:System.Web.Configuration.TrustLevelCollection" />.</returns>
		/// <param name="key">The index of the <see cref="T:System.Web.Configuration.TrustLevel" />.</param>
		// Token: 0x170014D8 RID: 5336
		public TrustLevel this[string key]
		{
			get
			{
				return (TrustLevel)base.BaseGet(key);
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.TrustLevel" /> item at the specified index within the <see cref="T:System.Web.Configuration.TrustLevelCollection" /> object.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.TrustLevel" /> at the specified index.</returns>
		/// <param name="index">The numeric index of the <see cref="T:System.Web.Configuration.TrustLevel" />.</param>
		// Token: 0x170014D9 RID: 5337
		public TrustLevel this[int index]
		{
			get
			{
				return (TrustLevel)base.BaseGet(index);
			}
			set
			{
				this.Set(index, value);
			}
		}

		// Token: 0x170014DA RID: 5338
		// (get) Token: 0x0600414C RID: 16716 RVA: 0x00008A4D File Offset: 0x00006C4D
		protected override bool ThrowOnDuplicate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170014DB RID: 5339
		// (get) Token: 0x0600414D RID: 16717 RVA: 0x000A8F8C File Offset: 0x000A718C
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return TrustLevelCollection.properties;
			}
		}

		// Token: 0x04002330 RID: 9008
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
