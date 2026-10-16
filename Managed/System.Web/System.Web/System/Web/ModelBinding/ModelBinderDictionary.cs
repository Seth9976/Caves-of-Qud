using System;
using System.Collections;
using System.Collections.Generic;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Provides a container for all model binders for the application, listed by binder type.</summary>
	// Token: 0x0200071D RID: 1821
	public class ModelBinderDictionary : ICollection<KeyValuePair<Type, IModelBinder>>, IEnumerable<KeyValuePair<Type, IModelBinder>>, IEnumerable, IDictionary<Type, IModelBinder>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ModelBinderDictionary" /> class.</summary>
		// Token: 0x06004BE3 RID: 19427 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public ModelBinderDictionary()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the number of items in the dictionary.</summary>
		/// <returns>The number of items in the dictionary.</returns>
		// Token: 0x17001777 RID: 6007
		// (get) Token: 0x06004BE4 RID: 19428 RVA: 0x000C83B4 File Offset: 0x000C65B4
		public int Count
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets or sets the default model binder.</summary>
		/// <returns>The default model binder.</returns>
		// Token: 0x17001778 RID: 6008
		// (get) Token: 0x06004BE5 RID: 19429 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004BE6 RID: 19430 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public IModelBinder DefaultBinder
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets a value that indicates whether the dictionary is read-only.</summary>
		/// <returns>true if the dictionary is read-only; otherwise, false.</returns>
		// Token: 0x17001779 RID: 6009
		// (get) Token: 0x06004BE7 RID: 19431 RVA: 0x000C83D0 File Offset: 0x000C65D0
		public bool IsReadOnly
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets or sets the specified key in an object that implements the <see cref="T:System.Web.ModelBinding.IModelBinder" /> interface.</summary>
		/// <returns>The element that has the specified key.</returns>
		/// <param name="key">The item key.</param>
		// Token: 0x1700177A RID: 6010
		public IModelBinder this[Type key]
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets a collection that contains the keys in the dictionary.</summary>
		/// <returns>A collection that contains the keys in the dictionary.</returns>
		// Token: 0x1700177B RID: 6011
		// (get) Token: 0x06004BEA RID: 19434 RVA: 0x0000FA7F File Offset: 0x0000DC7F
		public ICollection<Type> Keys
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets a collection that contains the values in the model binder dictionary.</summary>
		/// <returns>A collection that contains the values in the model binder dictionary.</returns>
		// Token: 0x1700177C RID: 6012
		// (get) Token: 0x06004BEB RID: 19435 RVA: 0x0000FA7F File Offset: 0x0000DC7F
		public ICollection<IModelBinder> Values
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Adds the specified item to the dictionary.</summary>
		/// <param name="item">The object to add to the dictionary.</param>
		// Token: 0x06004BEC RID: 19436 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void Add(KeyValuePair<Type, IModelBinder> item)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Adds the specified item to the dictionary.</summary>
		/// <param name="key">The key of the item to add.</param>
		/// <param name="value">The value of the item to add.</param>
		// Token: 0x06004BED RID: 19437 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void Add(Type key, IModelBinder value)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Removes all items from the dictionary.</summary>
		// Token: 0x06004BEE RID: 19438 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void Clear()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Determines whether the model binder dictionary contains a specified value.</summary>
		/// <returns>true if <paramref name="item" /> is found; otherwise, false.</returns>
		/// <param name="item">The item to search for.</param>
		// Token: 0x06004BEF RID: 19439 RVA: 0x000C83EC File Offset: 0x000C65EC
		public bool Contains(KeyValuePair<Type, IModelBinder> item)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Determines whether the model binder dictionary contains an element that has the specified key.</summary>
		/// <returns>true if <paramref name="key" /> is found; otherwise, false.</returns>
		/// <param name="key">The key to search for.</param>
		// Token: 0x06004BF0 RID: 19440 RVA: 0x000C8408 File Offset: 0x000C6608
		public bool ContainsKey(Type key)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Copies the elements of the model binder dictionary to an array, starting at a specified index.</summary>
		/// <param name="array">The destination array. The array must be one-dimensional and have zero-based indexing.</param>
		/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying starts.</param>
		// Token: 0x06004BF1 RID: 19441 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void CopyTo(KeyValuePair<Type, IModelBinder>[] array, int arrayIndex)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Returns an enumerator that can be used to iterate through the collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		// Token: 0x06004BF2 RID: 19442 RVA: 0x0000FA7F File Offset: 0x0000DC7F
		public IEnumerator<KeyValuePair<Type, IModelBinder>> GetEnumerator()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}

		/// <summary>Removes the first occurrence of the specified element from the model binder dictionary.</summary>
		/// <returns>true if <paramref name="item" /> was successfully removed from the dictionary; false if <paramref name="item" /> was not removed or was not found in the dictionary.</returns>
		/// <param name="item">The item to remove.</param>
		// Token: 0x06004BF3 RID: 19443 RVA: 0x000C8424 File Offset: 0x000C6624
		public bool Remove(KeyValuePair<Type, IModelBinder> item)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Removes the element that has the specified key from the model binder dictionary.</summary>
		/// <returns>true if the element was successfully removed; false if <paramref name="key" /> was note removed or was not found in the dictionary.</returns>
		/// <param name="key">The key of the item to remove.</param>
		// Token: 0x06004BF4 RID: 19444 RVA: 0x000C8440 File Offset: 0x000C6640
		public bool Remove(Type key)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Returns an enumerator that can be used to iterate through a collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		// Token: 0x06004BF5 RID: 19445 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		IEnumerator IEnumerable.GetEnumerator()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets the value that is associated with the specified key.</summary>
		/// <returns>true if the dictionary contains an element that has the specified key; otherwise, false.</returns>
		/// <param name="key">The key of the value to get.</param>
		/// <param name="value">When this method returns, the value that is associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value" /> parameter. This parameter is passed uninitialized.</param>
		// Token: 0x06004BF6 RID: 19446 RVA: 0x000C845C File Offset: 0x000C665C
		public bool TryGetValue(Type key, out IModelBinder value)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}
	}
}
