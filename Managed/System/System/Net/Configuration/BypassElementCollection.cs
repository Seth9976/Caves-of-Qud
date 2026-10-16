using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents a container for the addresses of resources that bypass the proxy server. This class cannot be inherited.</summary>
	// Token: 0x02000762 RID: 1890
	[ConfigurationCollection(typeof(BypassElement), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public sealed class BypassElementCollection : ConfigurationElementCollection
	{
		/// <summary>Gets or sets the element at the specified position in the collection.</summary>
		/// <returns>The <see cref="T:System.Net.Configuration.BypassElement" /> at the specified location.</returns>
		/// <param name="index">The zero-based index of the element.</param>
		// Token: 0x17000D75 RID: 3445
		[MonoTODO]
		public BypassElement this[int index]
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
		/// <returns>The <see cref="T:System.Net.Configuration.BypassElement" /> with the specified key, or null if there is no element with the specified key.</returns>
		/// <param name="name">The key for an element in the collection. </param>
		// Token: 0x17000D76 RID: 3446
		public BypassElement this[string name]
		{
			get
			{
				return (BypassElement)base[name];
			}
			set
			{
				base[name] = value;
			}
		}

		// Token: 0x17000D77 RID: 3447
		// (get) Token: 0x06003B90 RID: 15248 RVA: 0x00003062 File Offset: 0x00001262
		protected override bool ThrowOnDuplicate
		{
			get
			{
				return false;
			}
		}

		/// <summary>Adds an element to the collection.</summary>
		/// <param name="element">The <see cref="T:System.Net.Configuration.BypassElement" /> to add to the collection.</param>
		// Token: 0x06003B91 RID: 15249 RVA: 0x000316BF File Offset: 0x0002F8BF
		public void Add(BypassElement element)
		{
			this.BaseAdd(element);
		}

		/// <summary>Removes all elements from the collection.</summary>
		// Token: 0x06003B92 RID: 15250 RVA: 0x000316C8 File Offset: 0x0002F8C8
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x06003B93 RID: 15251 RVA: 0x000CC551 File Offset: 0x000CA751
		protected override ConfigurationElement CreateNewElement()
		{
			return new BypassElement();
		}

		// Token: 0x06003B94 RID: 15252 RVA: 0x000CC558 File Offset: 0x000CA758
		[MonoTODO("argument exception?")]
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (!(element is BypassElement))
			{
				throw new ArgumentException("element");
			}
			return ((BypassElement)element).Address;
		}

		/// <summary>Returns the index of the specified configuration element.</summary>
		/// <returns>The zero-based index of <paramref name="element" />.</returns>
		/// <param name="element">A <see cref="T:System.Net.Configuration.BypassElement" />.</param>
		// Token: 0x06003B95 RID: 15253 RVA: 0x000CC46D File Offset: 0x000CA66D
		public int IndexOf(BypassElement element)
		{
			return base.BaseIndexOf(element);
		}

		/// <summary>Removes the specified configuration element from the collection.</summary>
		/// <param name="element">The <see cref="T:System.Net.Configuration.BypassElement" /> to remove.</param>
		// Token: 0x06003B96 RID: 15254 RVA: 0x000CC476 File Offset: 0x000CA676
		public void Remove(BypassElement element)
		{
			base.BaseRemove(element);
		}

		/// <summary>Removes the element with the specified key.</summary>
		/// <param name="name">The key of the element to remove.</param>
		// Token: 0x06003B97 RID: 15255 RVA: 0x000CC476 File Offset: 0x000CA676
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		/// <summary>Removes the element at the specified index.</summary>
		/// <param name="index">The zero-based index of the element to remove.</param>
		// Token: 0x06003B98 RID: 15256 RVA: 0x000CC47F File Offset: 0x000CA67F
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}
	}
}
