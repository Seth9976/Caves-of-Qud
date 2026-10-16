using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;

namespace System.Web.Services.Configuration
{
	/// <summary>Contains a strongly typed collection of <see cref="T:System.Web.Services.Configuration.TypeElement" /> objects.</summary>
	// Token: 0x02000149 RID: 329
	[ConfigurationCollection(typeof(TypeElement))]
	public sealed class TypeElementCollection : ConfigurationElementCollection
	{
		/// <summary>Adds a <see cref="T:System.Web.Services.Configuration.TypeElement" /> to the collection.</summary>
		/// <param name="element">The <see cref="T:System.Web.Services.Configuration.TypeElement" /> to add.</param>
		// Token: 0x060009FE RID: 2558 RVA: 0x00043695 File Offset: 0x00041895
		public void Add(TypeElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			this.BaseAdd(element);
		}

		/// <summary>Removes all <see cref="T:System.Web.Services.Configuration.TypeElement" /> objects from the collection.</summary>
		// Token: 0x060009FF RID: 2559 RVA: 0x000436AC File Offset: 0x000418AC
		public void Clear()
		{
			base.BaseClear();
		}

		/// <summary>Returns a <see cref="T:System.Boolean" /> that indicates whether a <see cref="T:System.Web.Services.Configuration.TypeElement" /> with the specified key exists in the collection.</summary>
		/// <returns>true if the collection contains a <see cref="T:System.Web.Services.Configuration.TypeElement" /> with the specified key; otherwise, false.</returns>
		/// <param name="key">The key of the <see cref="T:System.Web.Services.Configuration.TypeElement" /> to find in the collection.</param>
		// Token: 0x06000A00 RID: 2560 RVA: 0x000436B4 File Offset: 0x000418B4
		public bool ContainsKey(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return base.BaseGet(key) != null;
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x00043E65 File Offset: 0x00042065
		protected override ConfigurationElement CreateNewElement()
		{
			return new TypeElement();
		}

		/// <summary>Copies the elements from the collection to an array, starting at a specified index of the array.</summary>
		/// <param name="array">An array of type <see cref="T:System.Web.Services.Configuration.TypeElement" /> to which to copy the contents of the collection.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x06000A02 RID: 2562 RVA: 0x000436D5 File Offset: 0x000418D5
		public void CopyTo(TypeElement[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x00043E6C File Offset: 0x0004206C
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return ((TypeElement)element).Type;
		}

		/// <summary>Returns the zero-based index of a specified <see cref="T:System.Web.Services.Configuration.TypeElement" /> in the collection.</summary>
		/// <returns>The zero-based index of the specified <see cref="T:System.Web.Services.Configuration.TypeElement" />, or -1 if the element was not found in the collection.</returns>
		/// <param name="element">The <see cref="T:System.Web.Services.Configuration.TypeElement" /> to find in the collection.</param>
		// Token: 0x06000A04 RID: 2564 RVA: 0x00043724 File Offset: 0x00041924
		public int IndexOf(TypeElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return base.BaseIndexOf(element);
		}

		/// <summary>Removes a specified <see cref="T:System.Web.Services.Configuration.TypeElement" /> from the collection.</summary>
		/// <param name="element">The <see cref="T:System.Web.Services.Configuration.TypeElement" /> to remove from the collection.</param>
		// Token: 0x06000A05 RID: 2565 RVA: 0x0004373B File Offset: 0x0004193B
		public void Remove(TypeElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			base.BaseRemove(this.GetElementKey(element));
		}

		/// <summary>Removes the <see cref="T:System.Web.Services.Configuration.TypeElement" /> with the specified key from the collection.</summary>
		/// <param name="key">The key of the <see cref="T:System.Web.Services.Configuration.TypeElement" /> to be removed from the collection.</param>
		// Token: 0x06000A06 RID: 2566 RVA: 0x00043758 File Offset: 0x00041958
		public void RemoveAt(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			base.BaseRemove(key);
		}

		/// <summary>Removes the element at the specified index in the collection.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Web.Services.Configuration.TypeElement" /> to remove from the collection.</param>
		// Token: 0x06000A07 RID: 2567 RVA: 0x0004376F File Offset: 0x0004196F
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.Services.Configuration.TypeElement" /> that has the specified key in the collection. </summary>
		/// <returns>The <see cref="T:System.Web.Services.Configuration.TypeElement" /> with the specified key.</returns>
		/// <param name="key">The key of the <see cref="T:System.Web.Services.Configuration.TypeElement" /> to get or set in the collection.</param>
		/// <exception cref="T:System.Collections.Generic.KeyNotFoundException">The <see cref="T:System.Web.Services.Configuration.TypeElement" /> with the specified key was not found in the collection.</exception>
		// Token: 0x1700028A RID: 650
		public TypeElement this[object key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				TypeElement typeElement = (TypeElement)base.BaseGet(key);
				if (typeElement == null)
				{
					throw new KeyNotFoundException(string.Format(CultureInfo.InvariantCulture, Res.GetString("No elements matching the key {0} were found in the ConfigurationElementCollection."), key.ToString()));
				}
				return typeElement;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (this.GetElementKey(value).Equals(key))
				{
					if (base.BaseGet(key) != null)
					{
						base.BaseRemove(key);
					}
					this.Add(value);
					return;
				}
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, Res.GetString("The key does not match the indexer key. Key on element (expected value): {0}. Key provided to indexer: {1}."), this.GetElementKey(value).ToString(), key.ToString()));
			}
		}

		/// <summary>Gets or sets the element at a specified index in the collection.</summary>
		/// <returns>The <see cref="T:System.Web.Services.Configuration.TypeElement" /> that exists at the specified index.</returns>
		/// <param name="index">The zero-based index into the collection.</param>
		// Token: 0x1700028B RID: 651
		public TypeElement this[int index]
		{
			get
			{
				return (TypeElement)base.BaseGet(index);
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
	}
}
