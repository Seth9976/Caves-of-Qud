using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents a container for Web request module configuration elements. This class cannot be inherited.</summary>
	// Token: 0x02000782 RID: 1922
	[ConfigurationCollection(typeof(WebRequestModuleElement), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public sealed class WebRequestModuleElementCollection : ConfigurationElementCollection
	{
		/// <summary>Gets or sets the element at the specified position in the collection.</summary>
		/// <returns>The <see cref="T:System.Net.Configuration.WebRequestModuleElement" /> at the specified location.</returns>
		/// <param name="index">The zero-based index of the element.</param>
		// Token: 0x17000DDA RID: 3546
		[MonoTODO]
		public WebRequestModuleElement this[int index]
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the element with the specified key.</summary>
		/// <returns>The <see cref="T:System.Net.Configuration.WebRequestModuleElement" /> with the specified key or null if there is no element with the specified key.</returns>
		/// <param name="name">The key for an element in the collection.</param>
		// Token: 0x17000DDB RID: 3547
		[MonoTODO]
		public WebRequestModuleElement this[string name]
		{
			get
			{
				return (WebRequestModuleElement)base[name];
			}
			set
			{
				base[name] = value;
			}
		}

		/// <summary>Adds an element to the collection.</summary>
		/// <param name="element">The <see cref="T:System.Net.Configuration.WebRequestModuleElement" /> to add to the collection.</param>
		// Token: 0x06003C88 RID: 15496 RVA: 0x000316BF File Offset: 0x0002F8BF
		public void Add(WebRequestModuleElement element)
		{
			this.BaseAdd(element);
		}

		/// <summary>Removes all elements from the collection.</summary>
		// Token: 0x06003C89 RID: 15497 RVA: 0x000316C8 File Offset: 0x0002F8C8
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x06003C8A RID: 15498 RVA: 0x000CE436 File Offset: 0x000CC636
		protected override ConfigurationElement CreateNewElement()
		{
			return new WebRequestModuleElement();
		}

		// Token: 0x06003C8B RID: 15499 RVA: 0x000CE43D File Offset: 0x000CC63D
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (!(element is WebRequestModuleElement))
			{
				throw new ArgumentException("element");
			}
			return ((WebRequestModuleElement)element).Prefix;
		}

		/// <summary>Returns the index of the specified configuration element.</summary>
		/// <returns>The zero-based index of <paramref name="element" />.</returns>
		/// <param name="element">A <see cref="T:System.Net.Configuration.WebRequestModuleElement" />.</param>
		// Token: 0x06003C8C RID: 15500 RVA: 0x000CC46D File Offset: 0x000CA66D
		public int IndexOf(WebRequestModuleElement element)
		{
			return base.BaseIndexOf(element);
		}

		/// <summary>Removes the specified configuration element from the collection.</summary>
		/// <param name="element">The <see cref="T:System.Net.Configuration.WebRequestModuleElement" /> to remove.</param>
		// Token: 0x06003C8D RID: 15501 RVA: 0x000CE45D File Offset: 0x000CC65D
		public void Remove(WebRequestModuleElement element)
		{
			base.BaseRemove(element.Prefix);
		}

		/// <summary>Removes the element with the specified key.</summary>
		/// <param name="name">The key of the element to remove.</param>
		// Token: 0x06003C8E RID: 15502 RVA: 0x000CC476 File Offset: 0x000CA676
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		/// <summary>Removes the element at the specified index.</summary>
		/// <param name="index">The zero-based index of the element to remove.</param>
		// Token: 0x06003C8F RID: 15503 RVA: 0x000CC47F File Offset: 0x000CA67F
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}
	}
}
