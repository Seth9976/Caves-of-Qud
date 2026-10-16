using System;
using System.Collections;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Represents a collection of <see cref="T:System.Web.Configuration.ExpressionBuilder" /> objects. This class cannot be inherited.</summary>
	// Token: 0x02000598 RID: 1432
	[ConfigurationCollection(typeof(ExpressionBuilder), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public sealed class ExpressionBuilderCollection : ConfigurationElementCollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.ExpressionBuilderCollection" /> class.</summary>
		// Token: 0x06003CA9 RID: 15529 RVA: 0x0009DA90 File Offset: 0x0009BC90
		public ExpressionBuilderCollection()
			: base(CaseInsensitiveComparer.DefaultInvariant)
		{
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.ExpressionBuilder" /> at the specified index in the <see cref="T:System.Web.Configuration.ExpressionBuilderCollection" /> collection.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.ExpressionBuilder" /> object at the specified index or null if there is no object at that index.</returns>
		/// <param name="index">An integer value specifying an <see cref="T:System.Web.Configuration.ExpressionBuilder" /> object within the <see cref="T:System.Web.Configuration.ExpressionBuilderCollection" /> collection.</param>
		// Token: 0x170012C3 RID: 4803
		public ExpressionBuilder this[int index]
		{
			get
			{
				return (ExpressionBuilder)base.BaseGet(index);
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

		/// <summary>Gets the <see cref="T:System.Web.Configuration.ExpressionBuilder" /> object with the specified name.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.ExpressionBuilder" /> or null if there is no object with that name in the collection.</returns>
		/// <param name="name">The name of an <see cref="T:System.Web.Configuration.ExpressionBuilder" /> object in the <see cref="T:System.Web.Configuration.ExpressionBuilderCollection" /> collection.</param>
		// Token: 0x170012C4 RID: 4804
		public ExpressionBuilder this[string name]
		{
			get
			{
				return (ExpressionBuilder)base.BaseGet(name);
			}
		}

		// Token: 0x170012C5 RID: 4805
		// (get) Token: 0x06003CAD RID: 15533 RVA: 0x0009F2D2 File Offset: 0x0009D4D2
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ExpressionBuilderCollection.props;
			}
		}

		/// <summary>Adds an <see cref="T:System.Web.Configuration.ExpressionBuilder" /> object to the <see cref="T:System.Web.Configuration.ExpressionBuilderCollection" />.</summary>
		/// <param name="buildProvider">A string value specifying the <see cref="T:System.Web.Configuration.ExpressionBuilder" /> reference.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.ExpressionBuilder" /> object to add already exists in the collection, or the collection is read-only.</exception>
		// Token: 0x06003CAE RID: 15534 RVA: 0x0009D8D3 File Offset: 0x0009BAD3
		public void Add(ExpressionBuilder buildProvider)
		{
			this.BaseAdd(buildProvider);
		}

		/// <summary>Clears all the <see cref="T:System.Web.Configuration.ExpressionBuilder" /> objects from the <see cref="T:System.Web.Configuration.ExpressionBuilderCollection" /> collection.</summary>
		// Token: 0x06003CAF RID: 15535 RVA: 0x0009CBA3 File Offset: 0x0009ADA3
		public void Clear()
		{
			base.BaseClear();
		}

		/// <summary>Removes an <see cref="T:System.Web.Configuration.ExpressionBuilder" /> object from the <see cref="T:System.Web.Configuration.ExpressionBuilderCollection" /> collection.</summary>
		/// <param name="name">A string value specifying the <see cref="T:System.Web.Configuration.ExpressionBuilder" /> reference.</param>
		// Token: 0x06003CB0 RID: 15536 RVA: 0x0009CBBF File Offset: 0x0009ADBF
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		/// <summary>Removes an <see cref="T:System.Web.Configuration.ExpressionBuilder" /> object from the <see cref="T:System.Web.Configuration.ExpressionBuilderCollection" /> collection.</summary>
		/// <param name="index">An integer value specifying a specific <see cref="T:System.Web.Configuration.ExpressionBuilder" /> object within the <see cref="T:System.Web.Configuration.ExpressionBuilderCollection" /> collection.</param>
		// Token: 0x06003CB1 RID: 15537 RVA: 0x0009CBC8 File Offset: 0x0009ADC8
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		// Token: 0x06003CB2 RID: 15538 RVA: 0x0009F2D9 File Offset: 0x0009D4D9
		protected override ConfigurationElement CreateNewElement()
		{
			return new ExpressionBuilder();
		}

		// Token: 0x06003CB3 RID: 15539 RVA: 0x0009F2E0 File Offset: 0x0009D4E0
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((ExpressionBuilder)element).ExpressionPrefix;
		}

		// Token: 0x040020DB RID: 8411
		private static ConfigurationPropertyCollection props = new ConfigurationPropertyCollection();
	}
}
