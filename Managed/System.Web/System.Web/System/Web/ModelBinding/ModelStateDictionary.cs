using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace System.Web.ModelBinding
{
	/// <summary>Represents the state of model binding.</summary>
	// Token: 0x02000520 RID: 1312
	[Serializable]
	public class ModelStateDictionary : IDictionary<string, ModelState>, ICollection<KeyValuePair<string, ModelState>>, IEnumerable<KeyValuePair<string, ModelState>>, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ModelStateDictionary" /> class.</summary>
		// Token: 0x060039C3 RID: 14787 RVA: 0x0009A726 File Offset: 0x00098926
		public ModelStateDictionary()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ModelStateDictionary" /> class using an existing dictionary collection.</summary>
		/// <param name="dictionary">The dictionary.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="dictionary" /> parameter is null.</exception>
		// Token: 0x060039C4 RID: 14788 RVA: 0x0009A740 File Offset: 0x00098940
		public ModelStateDictionary(ModelStateDictionary dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			foreach (KeyValuePair<string, ModelState> keyValuePair in dictionary)
			{
				this._innerDictionary.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}

		/// <summary>Gets the number of key/value pairs in the collection.</summary>
		/// <returns>The number of key/value pairs in the collection.</returns>
		// Token: 0x170011EF RID: 4591
		// (get) Token: 0x060039C5 RID: 14789 RVA: 0x0009A7C0 File Offset: 0x000989C0
		public int Count
		{
			get
			{
				return this._innerDictionary.Count;
			}
		}

		/// <summary>Gets a value that indicates whether the dictionary is read-only.</summary>
		/// <returns>true if the dictionary is read-only; otherwise, false.</returns>
		// Token: 0x170011F0 RID: 4592
		// (get) Token: 0x060039C6 RID: 14790 RVA: 0x0009A7CD File Offset: 0x000989CD
		public bool IsReadOnly
		{
			get
			{
				return ((ICollection<KeyValuePair<string, ModelState>>)this._innerDictionary).IsReadOnly;
			}
		}

		/// <summary>Gets a value that indicates whether there are any errors in any of the model state objects in the dictionary.</summary>
		/// <returns>false if any errors were found; otherwise, true.</returns>
		// Token: 0x170011F1 RID: 4593
		// (get) Token: 0x060039C7 RID: 14791 RVA: 0x0009A7DA File Offset: 0x000989DA
		public bool IsValid
		{
			get
			{
				return this.Values.All<ModelState>((ModelState modelState) => modelState.Errors.Count == 0);
			}
		}

		/// <summary>Gets a collection that contains the keys of the dictionary.</summary>
		/// <returns>The keys of the dictionary.</returns>
		// Token: 0x170011F2 RID: 4594
		// (get) Token: 0x060039C8 RID: 14792 RVA: 0x0009A806 File Offset: 0x00098A06
		public ICollection<string> Keys
		{
			get
			{
				return this._innerDictionary.Keys;
			}
		}

		/// <summary>Gets or sets the value that is associated with the specified key.</summary>
		/// <returns>The item.</returns>
		/// <param name="key">The key.</param>
		// Token: 0x170011F3 RID: 4595
		public ModelState this[string key]
		{
			get
			{
				ModelState modelState;
				this._innerDictionary.TryGetValue(key, out modelState);
				return modelState;
			}
			set
			{
				this._innerDictionary[key] = value;
			}
		}

		/// <summary>Gets a collection that contains the values of the dictionary.</summary>
		/// <returns>The values of the dictionary.</returns>
		// Token: 0x170011F4 RID: 4596
		// (get) Token: 0x060039CB RID: 14795 RVA: 0x0009A840 File Offset: 0x00098A40
		public ICollection<ModelState> Values
		{
			get
			{
				return this._innerDictionary.Values;
			}
		}

		/// <summary>Adds the specified item to the dictionary.</summary>
		/// <param name="item">The item.</param>
		// Token: 0x060039CC RID: 14796 RVA: 0x0009A84D File Offset: 0x00098A4D
		public void Add(KeyValuePair<string, ModelState> item)
		{
			((ICollection<KeyValuePair<string, ModelState>>)this._innerDictionary).Add(item);
		}

		/// <summary>Adds an item that has the specified key and value to the dictionary.</summary>
		/// <param name="key">The key.</param>
		/// <param name="value">The value.</param>
		// Token: 0x060039CD RID: 14797 RVA: 0x0009A85B File Offset: 0x00098A5B
		public void Add(string key, ModelState value)
		{
			this._innerDictionary.Add(key, value);
		}

		/// <summary>Adds a model error to the errors collection using the specified key and using the specified exception for the value.</summary>
		/// <param name="key">The key.</param>
		/// <param name="exception">The exception object.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="key" /> parameter is null.</exception>
		// Token: 0x060039CE RID: 14798 RVA: 0x0009A86A File Offset: 0x00098A6A
		public void AddModelError(string key, Exception exception)
		{
			this.GetModelStateForKey(key).Errors.Add(exception);
		}

		/// <summary>Adds the specified model error to the errors collection using the specified key and using the specified error message string for the value.</summary>
		/// <param name="key">The key.</param>
		/// <param name="errorMessage">The error message.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="key" /> parameter is null.</exception>
		// Token: 0x060039CF RID: 14799 RVA: 0x0009A87E File Offset: 0x00098A7E
		public void AddModelError(string key, string errorMessage)
		{
			this.GetModelStateForKey(key).Errors.Add(errorMessage);
		}

		/// <summary>Removes all items from the dictionary.</summary>
		// Token: 0x060039D0 RID: 14800 RVA: 0x0009A892 File Offset: 0x00098A92
		public void Clear()
		{
			this._innerDictionary.Clear();
		}

		/// <summary>Determines whether the model-state dictionary contains a specific item.</summary>
		/// <returns>true if <paramref name="item" /> is found in the dictionary; otherwise, false.</returns>
		/// <param name="item">The item to locate in the model-state dictionary.</param>
		// Token: 0x060039D1 RID: 14801 RVA: 0x0009A89F File Offset: 0x00098A9F
		public bool Contains(KeyValuePair<string, ModelState> item)
		{
			return ((ICollection<KeyValuePair<string, ModelState>>)this._innerDictionary).Contains(item);
		}

		/// <summary>Determines whether the model-state dictionary contains the specified key.</summary>
		/// <returns>true if the dictionary contains the specified key; otherwise, false.</returns>
		/// <param name="key">The key.</param>
		// Token: 0x060039D2 RID: 14802 RVA: 0x0009A8AD File Offset: 0x00098AAD
		public bool ContainsKey(string key)
		{
			return this._innerDictionary.ContainsKey(key);
		}

		/// <summary>Copies the elements of the dictionary to an array, starting at a specified index.</summary>
		/// <param name="array">The one-dimensional array that is the destination of the elements copied from the dictionary. The array must have zero-based indexing.</param>
		/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying starts.</param>
		// Token: 0x060039D3 RID: 14803 RVA: 0x0009A8BB File Offset: 0x00098ABB
		public void CopyTo(KeyValuePair<string, ModelState>[] array, int arrayIndex)
		{
			((ICollection<KeyValuePair<string, ModelState>>)this._innerDictionary).CopyTo(array, arrayIndex);
		}

		/// <summary>Returns an enumerator that can be used to iterate through the dictionary.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		// Token: 0x060039D4 RID: 14804 RVA: 0x0009A8CA File Offset: 0x00098ACA
		public IEnumerator<KeyValuePair<string, ModelState>> GetEnumerator()
		{
			return this._innerDictionary.GetEnumerator();
		}

		// Token: 0x060039D5 RID: 14805 RVA: 0x0009A8DC File Offset: 0x00098ADC
		private ModelState GetModelStateForKey(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			ModelState modelState;
			if (!this.TryGetValue(key, out modelState))
			{
				modelState = new ModelState();
				this[key] = modelState;
			}
			return modelState;
		}

		/// <summary>Determines whether there are any <see cref="T:System.Web.ModelBinding.ModelError" /> objects that are associated with the specified key or that are prefixed with the specified key.</summary>
		/// <returns>true if any <see cref="T:System.Web.ModelBinding.ModelError" /> objects are associated with the specified key or prefixed with the specified key; otherwise, false. If the key is not found in the dictionary, this method returns true.</returns>
		/// <param name="key">The key.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="key" /> parameter is null.</exception>
		// Token: 0x060039D6 RID: 14806 RVA: 0x0009A911 File Offset: 0x00098B11
		public bool IsValidField(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return DictionaryHelpers.FindKeysWithPrefix<ModelState>(this, key).All<KeyValuePair<string, ModelState>>((KeyValuePair<string, ModelState> entry) => entry.Value.Errors.Count == 0);
		}

		/// <summary>Copies the values from the specified model-state dictionary object into this dictionary, overwriting existing values if the keys are the same.</summary>
		/// <param name="dictionary">The model-state dictionary to be merged into this one.</param>
		// Token: 0x060039D7 RID: 14807 RVA: 0x0009A94C File Offset: 0x00098B4C
		public void Merge(ModelStateDictionary dictionary)
		{
			if (dictionary == null)
			{
				return;
			}
			foreach (KeyValuePair<string, ModelState> keyValuePair in dictionary)
			{
				this[keyValuePair.Key] = keyValuePair.Value;
			}
		}

		/// <summary>Removes the first occurrence of the specified item from the model-state dictionary.</summary>
		/// <returns>true if the item was successfully removed from the dictionary, or false if the item was not removed or was not found in the dictionary.</returns>
		/// <param name="item">The item to remove.</param>
		// Token: 0x060039D8 RID: 14808 RVA: 0x0009A9A8 File Offset: 0x00098BA8
		public bool Remove(KeyValuePair<string, ModelState> item)
		{
			return ((ICollection<KeyValuePair<string, ModelState>>)this._innerDictionary).Remove(item);
		}

		/// <summary>Removes the item that has the specified key from the dictionary.</summary>
		/// <returns>true if the item was successfully removed from the dictionary, or false if the item was not removed or was not found in the dictionary.</returns>
		/// <param name="key">The key of the item to remove.</param>
		// Token: 0x060039D9 RID: 14809 RVA: 0x0009A9B6 File Offset: 0x00098BB6
		public bool Remove(string key)
		{
			return this._innerDictionary.Remove(key);
		}

		/// <summary>Sets the value for the specified key.</summary>
		/// <param name="key">The key.</param>
		/// <param name="value">The value.</param>
		// Token: 0x060039DA RID: 14810 RVA: 0x0009A9C4 File Offset: 0x00098BC4
		public void SetModelValue(string key, ValueProviderResult value)
		{
			this.GetModelStateForKey(key).Value = value;
		}

		/// <summary>Attempts to gets the value that is associated with the specified key.</summary>
		/// <returns>true if the dictionary contains an item that has the specified key; otherwise, false.</returns>
		/// <param name="key">The key of the value to get.</param>
		/// <param name="value">When this method returns, contains the value that is associated with the specified key, if the key was found; otherwise, contains the default value for the type of this parameter. This parameter is passed uninitialized.</param>
		// Token: 0x060039DB RID: 14811 RVA: 0x0009A9D3 File Offset: 0x00098BD3
		public bool TryGetValue(string key, out ModelState value)
		{
			return this._innerDictionary.TryGetValue(key, out value);
		}

		/// <summary>Returns an enumerator that can be used to iterate through the collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		// Token: 0x060039DC RID: 14812 RVA: 0x0009A9E2 File Offset: 0x00098BE2
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)this._innerDictionary).GetEnumerator();
		}

		// Token: 0x04001F50 RID: 8016
		private readonly Dictionary<string, ModelState> _innerDictionary = new Dictionary<string, ModelState>(StringComparer.OrdinalIgnoreCase);
	}
}
