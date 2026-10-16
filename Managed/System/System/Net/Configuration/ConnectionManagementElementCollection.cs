using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents a container for connection management configuration elements. This class cannot be inherited.</summary>
	// Token: 0x02000764 RID: 1892
	[ConfigurationCollection(typeof(ConnectionManagementElement), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public sealed class ConnectionManagementElementCollection : ConfigurationElementCollection
	{
		/// <summary>Gets or sets the element at the specified position in the collection.</summary>
		/// <returns>The <see cref="T:System.Net.Configuration.ConnectionManagementElement" /> at the specified location.</returns>
		/// <param name="index">The zero-based index of the element.</param>
		// Token: 0x17000D7B RID: 3451
		[MonoTODO]
		public ConnectionManagementElement this[int index]
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
		/// <returns>The <see cref="T:System.Net.Configuration.ConnectionManagementElement" /> with the specified key or null if there is no element with the specified key.</returns>
		/// <param name="name">The key for an element in the collection. </param>
		// Token: 0x17000D7C RID: 3452
		public ConnectionManagementElement this[string name]
		{
			get
			{
				return (ConnectionManagementElement)base[name];
			}
			set
			{
				base[name] = value;
			}
		}

		/// <summary>Adds an element to the collection.</summary>
		/// <param name="element">The <see cref="T:System.Net.Configuration.ConnectionManagementElement" /> to add to the collection.</param>
		// Token: 0x06003BA6 RID: 15270 RVA: 0x000316BF File Offset: 0x0002F8BF
		public void Add(ConnectionManagementElement element)
		{
			this.BaseAdd(element);
		}

		/// <summary>Removes all elements from the collection.</summary>
		// Token: 0x06003BA7 RID: 15271 RVA: 0x000316C8 File Offset: 0x0002F8C8
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x06003BA8 RID: 15272 RVA: 0x000CC658 File Offset: 0x000CA858
		protected override ConfigurationElement CreateNewElement()
		{
			return new ConnectionManagementElement();
		}

		// Token: 0x06003BA9 RID: 15273 RVA: 0x000CC65F File Offset: 0x000CA85F
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (!(element is ConnectionManagementElement))
			{
				throw new ArgumentException("element");
			}
			return ((ConnectionManagementElement)element).Address;
		}

		/// <summary>Returns the index of the specified configuration element.</summary>
		/// <returns>The zero-based index of <paramref name="element" />.</returns>
		/// <param name="element">A <see cref="T:System.Net.Configuration.ConnectionManagementElement" />.</param>
		// Token: 0x06003BAA RID: 15274 RVA: 0x000CC46D File Offset: 0x000CA66D
		public int IndexOf(ConnectionManagementElement element)
		{
			return base.BaseIndexOf(element);
		}

		/// <summary>Removes the specified configuration element from the collection.</summary>
		/// <param name="element">The <see cref="T:System.Net.Configuration.ConnectionManagementElement" /> to remove.</param>
		// Token: 0x06003BAB RID: 15275 RVA: 0x000CC476 File Offset: 0x000CA676
		public void Remove(ConnectionManagementElement element)
		{
			base.BaseRemove(element);
		}

		/// <summary>Removes the element with the specified key.</summary>
		/// <param name="name">The key of the element to remove.</param>
		// Token: 0x06003BAC RID: 15276 RVA: 0x000CC476 File Offset: 0x000CA676
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		/// <summary>Removes the element at the specified index.</summary>
		/// <param name="index">The zero-based index of the element to remove.</param>
		// Token: 0x06003BAD RID: 15277 RVA: 0x000CC47F File Offset: 0x000CA67F
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}
	}
}
