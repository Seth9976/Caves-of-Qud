using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents a container for authentication module configuration elements. This class cannot be inherited.</summary>
	// Token: 0x0200075F RID: 1887
	[ConfigurationCollection(typeof(AuthenticationModuleElement), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public sealed class AuthenticationModuleElementCollection : ConfigurationElementCollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.AuthenticationModuleElementCollection" /> class. </summary>
		// Token: 0x06003B72 RID: 15218 RVA: 0x000316B7 File Offset: 0x0002F8B7
		[MonoTODO]
		public AuthenticationModuleElementCollection()
		{
		}

		/// <summary>Gets or sets the element at the specified position in the collection.</summary>
		/// <returns>The <see cref="T:System.Net.Configuration.AuthenticationModuleElement" /> at the specified location.</returns>
		/// <param name="index">The zero-based index of the element.</param>
		// Token: 0x17000D6F RID: 3439
		[MonoTODO]
		public AuthenticationModuleElement this[int index]
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
		/// <returns>The <see cref="T:System.Net.Configuration.AuthenticationModuleElement" /> with the specified key or null if there is no element with the specified key.</returns>
		/// <param name="name">The key for an element in the collection. </param>
		// Token: 0x17000D70 RID: 3440
		[MonoTODO]
		public AuthenticationModuleElement this[string name]
		{
			get
			{
				return (AuthenticationModuleElement)base[name];
			}
			set
			{
				base[name] = value;
			}
		}

		/// <summary>Adds an element to the collection.</summary>
		/// <param name="element">The <see cref="T:System.Net.Configuration.AuthenticationModuleElement" /> to add to the collection.</param>
		// Token: 0x06003B77 RID: 15223 RVA: 0x000316BF File Offset: 0x0002F8BF
		public void Add(AuthenticationModuleElement element)
		{
			this.BaseAdd(element);
		}

		/// <summary>Removes all elements from the collection.</summary>
		// Token: 0x06003B78 RID: 15224 RVA: 0x000316C8 File Offset: 0x0002F8C8
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x06003B79 RID: 15225 RVA: 0x000CC446 File Offset: 0x000CA646
		protected override ConfigurationElement CreateNewElement()
		{
			return new AuthenticationModuleElement();
		}

		// Token: 0x06003B7A RID: 15226 RVA: 0x000CC44D File Offset: 0x000CA64D
		[MonoTODO("argument exception?")]
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (!(element is AuthenticationModuleElement))
			{
				throw new ArgumentException("element");
			}
			return ((AuthenticationModuleElement)element).Type;
		}

		/// <summary>Returns the index of the specified configuration element.</summary>
		/// <returns>The zero-based index of <paramref name="element" />.</returns>
		/// <param name="element">A <see cref="T:System.Net.Configuration.AuthenticationModuleElement" />.</param>
		// Token: 0x06003B7B RID: 15227 RVA: 0x000CC46D File Offset: 0x000CA66D
		public int IndexOf(AuthenticationModuleElement element)
		{
			return base.BaseIndexOf(element);
		}

		/// <summary>Removes the specified configuration element from the collection.</summary>
		/// <param name="element">The <see cref="T:System.Net.Configuration.AuthenticationModuleElement" /> to remove.</param>
		// Token: 0x06003B7C RID: 15228 RVA: 0x000CC476 File Offset: 0x000CA676
		public void Remove(AuthenticationModuleElement element)
		{
			base.BaseRemove(element);
		}

		/// <summary>Removes the element with the specified key.</summary>
		/// <param name="name">The key of the element to remove.</param>
		// Token: 0x06003B7D RID: 15229 RVA: 0x000CC476 File Offset: 0x000CA676
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		/// <summary>Removes the element at the specified index.</summary>
		/// <param name="index">The zero-based index of the element to remove.</param>
		// Token: 0x06003B7E RID: 15230 RVA: 0x000CC47F File Offset: 0x000CA67F
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}
	}
}
