using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Represents a collection of <see cref="T:System.Web.Configuration.HttpHandlerAction" /> elements. This class cannot be inherited.</summary>
	// Token: 0x020005A8 RID: 1448
	[ConfigurationCollection(typeof(HttpHandlerAction), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMapAlternate)]
	public sealed class HttpHandlerActionCollection : ConfigurationElementCollection
	{
		/// <summary>Adds an <see cref="T:System.Web.Configuration.HttpHandlerAction" /> object to the collection.</summary>
		/// <param name="httpHandlerAction">The <see cref="T:System.Web.Configuration.HttpHandlerAction" /> object to add to the collection. </param>
		/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.HttpHandlerAction" /> object to add already exists in the collection or the collection is read-only.</exception>
		// Token: 0x06003DF4 RID: 15860 RVA: 0x000A254F File Offset: 0x000A074F
		public void Add(HttpHandlerAction httpHandlerAction)
		{
			HttpApplication.ClearHandlerCache();
			this.BaseAdd(httpHandlerAction);
		}

		/// <summary>Removes all <see cref="T:System.Web.Configuration.HttpHandlerAction" /> objects from the collection.</summary>
		// Token: 0x06003DF5 RID: 15861 RVA: 0x000A255D File Offset: 0x000A075D
		public void Clear()
		{
			HttpApplication.ClearHandlerCache();
			base.BaseClear();
		}

		// Token: 0x06003DF6 RID: 15862 RVA: 0x000A256A File Offset: 0x000A076A
		protected override ConfigurationElement CreateNewElement()
		{
			return new HttpHandlerAction();
		}

		// Token: 0x06003DF7 RID: 15863 RVA: 0x000A2571 File Offset: 0x000A0771
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((HttpHandlerAction)element).Path + "-" + ((HttpHandlerAction)element).Verb;
		}

		/// <summary>Gets the collection index of the specified <see cref="T:System.Web.Configuration.HttpHandlerAction" /> object.</summary>
		/// <returns>The collection index value.</returns>
		/// <param name="action">The <see cref="T:System.Web.Configuration.HttpHandlerAction" /> object for which to get the collection index. </param>
		// Token: 0x06003DF8 RID: 15864 RVA: 0x0009D41E File Offset: 0x0009B61E
		public int IndexOf(HttpHandlerAction action)
		{
			return base.BaseIndexOf(action);
		}

		/// <summary>Removes an <see cref="T:System.Web.Configuration.HttpHandlerAction" /> object with the specified <see cref="P:System.Web.Configuration.HttpHandlerAction.Verb" /> and <see cref="P:System.Web.Configuration.HttpHandlerAction.Path" /> properties from the collection.</summary>
		/// <param name="verb">The <see cref="P:System.Web.Configuration.HttpHandlerAction.Verb" /> property value that belongs to the handler to remove.</param>
		/// <param name="path">The <see cref="P:System.Web.Configuration.HttpHandlerAction.Path" /> property value that belongs to the handler to remove.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">There is no <see cref="T:System.Web.Configuration.HttpHandlerAction" /> object with the specified key in the collection, the element has already been removed, or the collection is read-only.</exception>
		// Token: 0x06003DF9 RID: 15865 RVA: 0x000A2593 File Offset: 0x000A0793
		public void Remove(string verb, string path)
		{
			HttpApplication.ClearHandlerCache();
			base.BaseRemove(path + "-" + verb);
		}

		/// <summary>Removes an <see cref="T:System.Web.Configuration.HttpHandlerAction" /> object from the collection.</summary>
		/// <param name="action">The <see cref="T:System.Web.Configuration.HttpHandlerAction" /> object to remove. </param>
		/// <exception cref="T:System.Configuration.ConfigurationException">The passed <see cref="T:System.Web.Configuration.HttpHandlerAction" /> object does not exist in the collection, the element has already been removed, or the collection is read-only.</exception>
		// Token: 0x06003DFA RID: 15866 RVA: 0x000A25AC File Offset: 0x000A07AC
		public void Remove(HttpHandlerAction action)
		{
			HttpApplication.ClearHandlerCache();
			base.BaseRemove(action.Path + "-" + action.Verb);
		}

		/// <summary>Removes an <see cref="T:System.Web.Configuration.HttpHandlerAction" /> object at the specified index location from the collection.</summary>
		/// <param name="index">The collection index of the object to remove.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">There is no <see cref="T:System.Web.Configuration.HttpHandlerAction" /> object at the specified index in the collection, the element has already been removed, or the collection is read-only.</exception>
		// Token: 0x06003DFB RID: 15867 RVA: 0x000A25CF File Offset: 0x000A07CF
		public void RemoveAt(int index)
		{
			HttpApplication.ClearHandlerCache();
			base.BaseRemoveAt(index);
		}

		/// <summary>Gets the type of <see cref="T:System.Web.Configuration.HttpHandlerActionCollection" />.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationElementCollectionType" /> of this collection.</returns>
		// Token: 0x1700137F RID: 4991
		// (get) Token: 0x06003DFC RID: 15868 RVA: 0x000A25DD File Offset: 0x000A07DD
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.AddRemoveClearMapAlternate;
			}
		}

		// Token: 0x17001380 RID: 4992
		// (get) Token: 0x06003DFD RID: 15869 RVA: 0x000A25E0 File Offset: 0x000A07E0
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return HttpHandlerActionCollection.properties;
			}
		}

		/// <summary>Gets or sets an item in this collection.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.HttpHandlerAction" /> object selected.</returns>
		/// <param name="index">The item index.</param>
		// Token: 0x17001381 RID: 4993
		public HttpHandlerAction this[int index]
		{
			get
			{
				return (HttpHandlerAction)base.BaseGet(index);
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

		// Token: 0x17001382 RID: 4994
		// (get) Token: 0x06003E00 RID: 15872 RVA: 0x00008A4D File Offset: 0x00006C4D
		protected override bool ThrowOnDuplicate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04002209 RID: 8713
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
