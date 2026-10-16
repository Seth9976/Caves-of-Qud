using System;
using System.Collections;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Represents a collection of <see cref="T:System.Web.Configuration.Compiler" /> objects. This class cannot be inherited.</summary>
	// Token: 0x02000590 RID: 1424
	[ConfigurationCollection(typeof(Compiler), AddItemName = "compiler", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	public sealed class CompilerCollection : ConfigurationElementCollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.CompilerCollection" /> class.</summary>
		// Token: 0x06003C47 RID: 15431 RVA: 0x0009DA90 File Offset: 0x0009BC90
		public CompilerCollection()
			: base(CaseInsensitiveComparer.DefaultInvariant)
		{
		}

		// Token: 0x06003C48 RID: 15432 RVA: 0x0009EA13 File Offset: 0x0009CC13
		protected override ConfigurationElement CreateNewElement()
		{
			return new Compiler();
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.Compiler" /> collection element at the specified index.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.Compiler" /> object.</returns>
		/// <param name="index">An integer value specifying a <see cref="T:System.Web.Configuration.Compiler" /> within the <see cref="T:System.Web.Configuration.CompilerCollection" />.</param>
		// Token: 0x06003C49 RID: 15433 RVA: 0x0009EA1A File Offset: 0x0009CC1A
		public Compiler Get(int index)
		{
			return (Compiler)base.BaseGet(index);
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.Compiler" /> collection element for the specified language.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.Compiler" /> object.</returns>
		/// <param name="language">The language for the <see cref="T:System.Web.Configuration.Compiler" /> object within the collection.</param>
		// Token: 0x06003C4A RID: 15434 RVA: 0x0009EA28 File Offset: 0x0009CC28
		public Compiler Get(string language)
		{
			return this[language];
		}

		// Token: 0x06003C4B RID: 15435 RVA: 0x0009EA31 File Offset: 0x0009CC31
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((Compiler)element).Language;
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.CompilerCollection" /> key name at the specified index.</summary>
		/// <returns>The key name at the specified index of the <see cref="T:System.Web.Configuration.CompilerCollection" />.</returns>
		/// <param name="index">An integer value specifying a <see cref="T:System.Web.Configuration.Compiler" /> within the <see cref="T:System.Web.Configuration.CompilerCollection" />.</param>
		// Token: 0x06003C4C RID: 15436 RVA: 0x0009E02A File Offset: 0x0009C22A
		public string GetKey(int index)
		{
			return (string)base.BaseGetKey(index);
		}

		/// <summary>Gets all the keys of the <see cref="T:System.Web.Configuration.CompilerCollection" />.</summary>
		/// <returns>The string array containing the collection keys.</returns>
		// Token: 0x170012A0 RID: 4768
		// (get) Token: 0x06003C4D RID: 15437 RVA: 0x0009EA40 File Offset: 0x0009CC40
		public string[] AllKeys
		{
			get
			{
				string[] array = new string[base.Count];
				for (int i = 0; i < base.Count; i++)
				{
					array[i] = this[i].Language;
				}
				return array;
			}
		}

		/// <summary>Gets the type of the configuration collection.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationElementCollectionType" /> object of the collection.</returns>
		// Token: 0x170012A1 RID: 4769
		// (get) Token: 0x06003C4E RID: 15438 RVA: 0x00008A4D File Offset: 0x00006C4D
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x170012A2 RID: 4770
		// (get) Token: 0x06003C4F RID: 15439 RVA: 0x0009EA7A File Offset: 0x0009CC7A
		protected override string ElementName
		{
			get
			{
				return "compiler";
			}
		}

		// Token: 0x170012A3 RID: 4771
		// (get) Token: 0x06003C50 RID: 15440 RVA: 0x0009EA81 File Offset: 0x0009CC81
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return CompilerCollection.properties;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.Compiler" /> at the specified index of the collection.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.Compiler" /> object.</returns>
		/// <param name="index">An integer value specifying a <see cref="T:System.Web.Configuration.Compiler" /> within the <see cref="T:System.Web.Configuration.CompilerCollection" />.</param>
		// Token: 0x170012A4 RID: 4772
		public Compiler this[int index]
		{
			get
			{
				return (Compiler)base.BaseGet(index);
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.Compiler" /> collection element for the specified language.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.Compiler" /> object.</returns>
		/// <param name="language">The language for the <see cref="T:System.Web.Configuration.Compiler" /> object within the collection.</param>
		// Token: 0x170012A5 RID: 4773
		public Compiler this[string language]
		{
			get
			{
				foreach (object obj in this)
				{
					Compiler compiler = (Compiler)obj;
					if (compiler.Language.IndexOf(language, StringComparison.InvariantCultureIgnoreCase) != -1)
					{
						return compiler;
					}
				}
				return null;
			}
		}

		// Token: 0x040020C6 RID: 8390
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
