using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Contains a collection of namespace objects. This class cannot be inherited.</summary>
	// Token: 0x020005B8 RID: 1464
	[ConfigurationCollection(typeof(NamespaceInfo), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public sealed class NamespaceCollection : ConfigurationElementCollection
	{
		// Token: 0x06003EB8 RID: 16056 RVA: 0x000A431A File Offset: 0x000A251A
		static NamespaceCollection()
		{
			NamespaceCollection.properties.Add(NamespaceCollection.autoImportVBNamespaceProp);
		}

		/// <summary>Adds a <see cref="T:System.Web.Configuration.NamespaceInfo" /> object to the collection.</summary>
		/// <param name="namespaceInformation">A <see cref="T:System.Web.Configuration.NamespaceInfo" /> object to add to the collection.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.NamespaceInfo" /> object to add already exists in the collection or the collection is read-only.</exception>
		// Token: 0x06003EBA RID: 16058 RVA: 0x0009D8D3 File Offset: 0x0009BAD3
		public void Add(NamespaceInfo namespaceInformation)
		{
			this.BaseAdd(namespaceInformation);
		}

		/// <summary>Removes all <see cref="T:System.Web.Configuration.NamespaceInfo" /> objects from the collection.</summary>
		// Token: 0x06003EBB RID: 16059 RVA: 0x0009CBA3 File Offset: 0x0009ADA3
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x06003EBC RID: 16060 RVA: 0x000A4354 File Offset: 0x000A2554
		protected override ConfigurationElement CreateNewElement()
		{
			return new NamespaceInfo(null);
		}

		// Token: 0x06003EBD RID: 16061 RVA: 0x000A435C File Offset: 0x000A255C
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((NamespaceInfo)element).Namespace;
		}

		/// <summary>Removes the <see cref="T:System.Web.Configuration.NamespaceInfo" /> object with the specified key from the collection.</summary>
		/// <param name="s">The namespace of a <see cref="T:System.Web.Configuration.NamespaceInfo" /> object to remove from the collection.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">There is no <see cref="T:System.Web.Configuration.NamespaceInfo" /> object with the specified key in the collection.- or -The element has already been removed.- or -The collection is read-only.</exception>
		// Token: 0x06003EBE RID: 16062 RVA: 0x0009CBBF File Offset: 0x0009ADBF
		public void Remove(string s)
		{
			base.BaseRemove(s);
		}

		/// <summary>Removes a <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object from the specified index in the collection.</summary>
		/// <param name="index">The index of a <see cref="T:System.Web.Configuration.NamespaceInfo" /> object to remove from the collection.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">There is no <see cref="T:System.Web.Configuration.NamespaceInfo" /> object at the specified index in the collection.- or -The element has already been removed.- or -The collection is read-only.</exception>
		// Token: 0x06003EBF RID: 16063 RVA: 0x0009CBC8 File Offset: 0x0009ADC8
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		/// <summary>Gets or sets a value that determines whether the Visual Basic namespace is imported without having to specify it.</summary>
		/// <returns>true if the Visual Basic namespace is imported automatically; otherwise, false. The default is true.</returns>
		// Token: 0x170013C6 RID: 5062
		// (get) Token: 0x06003EC0 RID: 16064 RVA: 0x000A4369 File Offset: 0x000A2569
		// (set) Token: 0x06003EC1 RID: 16065 RVA: 0x000A437B File Offset: 0x000A257B
		[ConfigurationProperty("autoImportVBNamespace", DefaultValue = true)]
		public bool AutoImportVBNamespace
		{
			get
			{
				return (bool)base[NamespaceCollection.autoImportVBNamespaceProp];
			}
			set
			{
				base[NamespaceCollection.autoImportVBNamespaceProp] = value;
			}
		}

		// Token: 0x170013C7 RID: 5063
		// (get) Token: 0x06003EC2 RID: 16066 RVA: 0x000A438E File Offset: 0x000A258E
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return NamespaceCollection.properties;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.NamespaceInfo" /> object at the specified index in the collection.</summary>
		/// <returns>
		///   <see cref="T:System.Web.Configuration.NamespaceInfo" /> object at the specified index, or null if there is no object at that index.</returns>
		/// <param name="index">The index of a <see cref="T:System.Web.Configuration.NamespaceInfo" /> object in the collection.</param>
		// Token: 0x170013C8 RID: 5064
		public NamespaceInfo this[int index]
		{
			get
			{
				return (NamespaceInfo)base.BaseGet(index);
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

		// Token: 0x04002255 RID: 8789
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002256 RID: 8790
		private static ConfigurationProperty autoImportVBNamespaceProp = new ConfigurationProperty("autoImportVBNamespace", typeof(bool), true);
	}
}
