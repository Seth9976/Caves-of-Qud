using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Contains a collection of <see cref="T:System.Web.Configuration.TagMapInfo" /> objects. </summary>
	// Token: 0x020005D9 RID: 1497
	[ConfigurationCollection(typeof(TagMapInfo), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public sealed class TagMapCollection : ConfigurationElementCollection
	{
		/// <summary>Adds a <see cref="T:System.Web.Configuration.TagMapInfo" /> object to the collection.</summary>
		/// <param name="tagMapInformation">A <see cref="T:System.Web.Configuration.TagMapInfo" /> object to add to the collection.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.TagMapInfo" /> object to add already exists in the collection.- or -The collection is read-only. </exception>
		// Token: 0x060040DE RID: 16606 RVA: 0x0009D8D3 File Offset: 0x0009BAD3
		public void Add(TagMapInfo tagMapInformation)
		{
			this.BaseAdd(tagMapInformation);
		}

		/// <summary>Clears all object from the collection.</summary>
		// Token: 0x060040DF RID: 16607 RVA: 0x0009CBA3 File Offset: 0x0009ADA3
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x060040E0 RID: 16608 RVA: 0x000A8556 File Offset: 0x000A6756
		protected override ConfigurationElement CreateNewElement()
		{
			return new TagMapInfo();
		}

		// Token: 0x060040E1 RID: 16609 RVA: 0x000A855D File Offset: 0x000A675D
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((TagMapInfo)element).TagType;
		}

		/// <summary>Removes the specified object from the collection.</summary>
		/// <param name="tagMapInformation">A <see cref="T:System.Web.Configuration.TagMapInfo" /> object in the collection.</param>
		// Token: 0x060040E2 RID: 16610 RVA: 0x000A856A File Offset: 0x000A676A
		public void Remove(TagMapInfo tagMapInformation)
		{
			base.BaseRemove(tagMapInformation.TagType);
		}

		// Token: 0x170014B5 RID: 5301
		// (get) Token: 0x060040E3 RID: 16611 RVA: 0x000A8578 File Offset: 0x000A6778
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return TagMapCollection.properties;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.TagMapInfo" /> object at the specified index location.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.TagMapInfo" /> object at the specified index, or null if there is no object at that index.</returns>
		/// <param name="index">The index of a <see cref="T:System.Web.Configuration.TagMapInfo" /> object in the collection.</param>
		// Token: 0x170014B6 RID: 5302
		public TagMapInfo this[int index]
		{
			get
			{
				return (TagMapInfo)base.BaseGet(index);
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

		// Token: 0x04002315 RID: 8981
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
