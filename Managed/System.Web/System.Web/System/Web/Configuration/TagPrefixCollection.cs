using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Contains a collection of <see cref="T:System.Web.Configuration.TagPrefixInfo" /> objects.</summary>
	// Token: 0x020005DB RID: 1499
	[ConfigurationCollection(typeof(TagPrefixInfo), CollectionType = ConfigurationElementCollectionType.BasicMap)]
	public sealed class TagPrefixCollection : ConfigurationElementCollection
	{
		/// <summary>Adds a <see cref="T:System.Web.Configuration.TagPrefixInfo" /> object to the collection.</summary>
		/// <param name="tagPrefixInformation">The <see cref="T:System.Web.Configuration.TagPrefixInfo" /> object to add to the collection.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.TagPrefixInfo" /> object to add already exists in the collection.- or -The collection is read-only. </exception>
		// Token: 0x060040F3 RID: 16627 RVA: 0x0009D8D3 File Offset: 0x0009BAD3
		public void Add(TagPrefixInfo tagPrefixInformation)
		{
			this.BaseAdd(tagPrefixInformation);
		}

		/// <summary>Clears all object from the collection.</summary>
		// Token: 0x060040F4 RID: 16628 RVA: 0x0009CBA3 File Offset: 0x0009ADA3
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x060040F5 RID: 16629 RVA: 0x000A86F5 File Offset: 0x000A68F5
		protected override ConfigurationElement CreateNewElement()
		{
			return new TagPrefixInfo();
		}

		// Token: 0x060040F6 RID: 16630 RVA: 0x000A86FC File Offset: 0x000A68FC
		protected override object GetElementKey(ConfigurationElement element)
		{
			TagPrefixInfo tagPrefixInfo = (TagPrefixInfo)element;
			return string.Concat(new string[] { tagPrefixInfo.TagPrefix, "-", tagPrefixInfo.TagName, "-", tagPrefixInfo.Source, "-", tagPrefixInfo.Namespace, "-", tagPrefixInfo.Assembly });
		}

		/// <summary>Removes the specified object from the collection.</summary>
		/// <param name="tagPrefixInformation">A <see cref="T:System.Web.Configuration.TagPrefixInfo" /> object in the collection.</param>
		// Token: 0x060040F7 RID: 16631 RVA: 0x000A8769 File Offset: 0x000A6969
		public void Remove(TagPrefixInfo tagPrefixInformation)
		{
			base.BaseRemove(this.GetElementKey(tagPrefixInformation));
		}

		/// <summary>Gets the type of the configuration collection.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationElementCollectionType" /> object of the collection.</returns>
		// Token: 0x170014BA RID: 5306
		// (get) Token: 0x060040F8 RID: 16632 RVA: 0x00008A4D File Offset: 0x00006C4D
		[global::System.MonoTODO("why override this?")]
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x170014BB RID: 5307
		// (get) Token: 0x060040F9 RID: 16633 RVA: 0x0009E10A File Offset: 0x0009C30A
		[global::System.MonoTODO("why override this?")]
		protected override string ElementName
		{
			get
			{
				return "add";
			}
		}

		// Token: 0x170014BC RID: 5308
		// (get) Token: 0x060040FA RID: 16634 RVA: 0x000A8778 File Offset: 0x000A6978
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return TagPrefixCollection.properties;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.TagPrefixInfo" /> object at the specified index location.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.TagPrefixInfo" /> object at the specified index, or null if there is no object at that index.</returns>
		/// <param name="index">The index of a <see cref="T:System.Web.Configuration.TagPrefixInfo" /> object in the collection.</param>
		// Token: 0x170014BD RID: 5309
		public TagPrefixInfo this[int index]
		{
			get
			{
				return (TagPrefixInfo)base.BaseGet(index);
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

		// Token: 0x170014BE RID: 5310
		// (get) Token: 0x060040FD RID: 16637 RVA: 0x00008A4D File Offset: 0x00006C4D
		protected override bool ThrowOnDuplicate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04002319 RID: 8985
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
