using System;
using System.Collections;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Represents a collection of <see cref="T:System.Web.Configuration.CodeSubDirectory" /> objects. This class cannot be inherited.</summary>
	// Token: 0x0200058C RID: 1420
	[ConfigurationCollection(typeof(CodeSubDirectory), CollectionType = ConfigurationElementCollectionType.BasicMap)]
	public sealed class CodeSubDirectoriesCollection : ConfigurationElementCollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.CodeSubDirectoriesCollection" /> class.</summary>
		// Token: 0x06003BF6 RID: 15350 RVA: 0x0009DA90 File Offset: 0x0009BC90
		public CodeSubDirectoriesCollection()
			: base(CaseInsensitiveComparer.DefaultInvariant)
		{
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.CodeSubDirectory" /> at the specified index in the <see cref="T:System.Web.Configuration.CodeSubDirectoriesCollection" /> collection.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.CodeSubDirectory" /> object.</returns>
		/// <param name="index">An integer value specifying a specific <see cref="T:System.Web.Configuration.CodeSubDirectory" /> object within the <see cref="T:System.Web.Configuration.CodeSubDirectoriesCollection" /> collection.</param>
		// Token: 0x1700127A RID: 4730
		public CodeSubDirectory this[int index]
		{
			get
			{
				return (CodeSubDirectory)base.BaseGet(index);
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

		/// <summary>Gets the type of the configuration collection.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationElementCollectionType" /> object of the collection.</returns>
		// Token: 0x1700127B RID: 4731
		// (get) Token: 0x06003BF9 RID: 15353 RVA: 0x00008A4D File Offset: 0x00006C4D
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x1700127C RID: 4732
		// (get) Token: 0x06003BFA RID: 15354 RVA: 0x0009E10A File Offset: 0x0009C30A
		protected override string ElementName
		{
			get
			{
				return "add";
			}
		}

		// Token: 0x1700127D RID: 4733
		// (get) Token: 0x06003BFB RID: 15355 RVA: 0x0009E111 File Offset: 0x0009C311
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return CodeSubDirectoriesCollection.props;
			}
		}

		/// <summary>Adds a <see cref="T:System.Web.Configuration.CodeSubDirectory" /> object to the <see cref="T:System.Web.Configuration.CodeSubDirectoriesCollection" />.</summary>
		/// <param name="codeSubDirectory">A string value specifying the <see cref="T:System.Web.Configuration.CodeSubDirectory" /> reference.</param>
		// Token: 0x06003BFC RID: 15356 RVA: 0x0009D8D3 File Offset: 0x0009BAD3
		public void Add(CodeSubDirectory codeSubDirectory)
		{
			this.BaseAdd(codeSubDirectory);
		}

		/// <summary>Removes all items from the collection</summary>
		// Token: 0x06003BFD RID: 15357 RVA: 0x0009CBA3 File Offset: 0x0009ADA3
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x06003BFE RID: 15358 RVA: 0x0009E118 File Offset: 0x0009C318
		protected override ConfigurationElement CreateNewElement()
		{
			return new CodeSubDirectory(null);
		}

		// Token: 0x06003BFF RID: 15359 RVA: 0x0009E120 File Offset: 0x0009C320
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((CodeSubDirectory)element).DirectoryName;
		}

		/// <summary>Removes the specified item from the collection.</summary>
		/// <param name="directoryName">The name of the directory to remove.</param>
		// Token: 0x06003C00 RID: 15360 RVA: 0x0009CBBF File Offset: 0x0009ADBF
		public void Remove(string directoryName)
		{
			base.BaseRemove(directoryName);
		}

		/// <summary>Removes the item at the specified index in the collection.</summary>
		/// <param name="index">The index position of the item to be removed.</param>
		// Token: 0x06003C01 RID: 15361 RVA: 0x0009CBC8 File Offset: 0x0009ADC8
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		// Token: 0x040020A9 RID: 8361
		private static ConfigurationPropertyCollection props = new ConfigurationPropertyCollection();
	}
}
