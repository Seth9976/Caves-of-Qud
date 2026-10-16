using System;
using System.Collections.Generic;

namespace System.Collections.Specialized
{
	// Token: 0x020004C1 RID: 1217
	internal class GenericAdapter : IDictionary<string, string>, ICollection<KeyValuePair<string, string>>, IEnumerable<KeyValuePair<string, string>>, IEnumerable
	{
		// Token: 0x0600275E RID: 10078 RVA: 0x00088939 File Offset: 0x00086B39
		internal GenericAdapter(StringDictionary stringDictionary)
		{
			this.m_stringDictionary = stringDictionary;
		}

		// Token: 0x0600275F RID: 10079 RVA: 0x00088948 File Offset: 0x00086B48
		public void Add(string key, string value)
		{
			this[key] = value;
		}

		// Token: 0x06002760 RID: 10080 RVA: 0x00088952 File Offset: 0x00086B52
		public bool ContainsKey(string key)
		{
			return this.m_stringDictionary.ContainsKey(key);
		}

		// Token: 0x06002761 RID: 10081 RVA: 0x00088960 File Offset: 0x00086B60
		public void Clear()
		{
			this.m_stringDictionary.Clear();
		}

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x06002762 RID: 10082 RVA: 0x0008896D File Offset: 0x00086B6D
		public int Count
		{
			get
			{
				return this.m_stringDictionary.Count;
			}
		}

		// Token: 0x17000806 RID: 2054
		public string this[string key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (!this.m_stringDictionary.ContainsKey(key))
				{
					throw new KeyNotFoundException();
				}
				return this.m_stringDictionary[key];
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				this.m_stringDictionary[key] = value;
			}
		}

		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x06002765 RID: 10085 RVA: 0x000889C7 File Offset: 0x00086BC7
		public ICollection<string> Keys
		{
			get
			{
				if (this._keys == null)
				{
					this._keys = new GenericAdapter.ICollectionToGenericCollectionAdapter(this.m_stringDictionary, GenericAdapter.KeyOrValue.Key);
				}
				return this._keys;
			}
		}

		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x06002766 RID: 10086 RVA: 0x000889E9 File Offset: 0x00086BE9
		public ICollection<string> Values
		{
			get
			{
				if (this._values == null)
				{
					this._values = new GenericAdapter.ICollectionToGenericCollectionAdapter(this.m_stringDictionary, GenericAdapter.KeyOrValue.Value);
				}
				return this._values;
			}
		}

		// Token: 0x06002767 RID: 10087 RVA: 0x00088A0B File Offset: 0x00086C0B
		public bool Remove(string key)
		{
			if (!this.m_stringDictionary.ContainsKey(key))
			{
				return false;
			}
			this.m_stringDictionary.Remove(key);
			return true;
		}

		// Token: 0x06002768 RID: 10088 RVA: 0x00088A2A File Offset: 0x00086C2A
		public bool TryGetValue(string key, out string value)
		{
			if (!this.m_stringDictionary.ContainsKey(key))
			{
				value = null;
				return false;
			}
			value = this.m_stringDictionary[key];
			return true;
		}

		// Token: 0x06002769 RID: 10089 RVA: 0x00088A4E File Offset: 0x00086C4E
		void ICollection<KeyValuePair<string, string>>.Add(KeyValuePair<string, string> item)
		{
			this.m_stringDictionary.Add(item.Key, item.Value);
		}

		// Token: 0x0600276A RID: 10090 RVA: 0x00088A6C File Offset: 0x00086C6C
		bool ICollection<KeyValuePair<string, string>>.Contains(KeyValuePair<string, string> item)
		{
			string text;
			return this.TryGetValue(item.Key, out text) && text.Equals(item.Value);
		}

		// Token: 0x0600276B RID: 10091 RVA: 0x00088A9C File Offset: 0x00086C9C
		void ICollection<KeyValuePair<string, string>>.CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array", SR.GetString("Array cannot be null."));
			}
			if (arrayIndex < 0)
			{
				throw new ArgumentOutOfRangeException("arrayIndex", SR.GetString("Non-negative number required."));
			}
			if (array.Length - arrayIndex < this.Count)
			{
				throw new ArgumentException(SR.GetString("Destination array is not long enough to copy all the items in the collection. Check array index and length."));
			}
			int num = arrayIndex;
			foreach (object obj in this.m_stringDictionary)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				array[num++] = new KeyValuePair<string, string>((string)dictionaryEntry.Key, (string)dictionaryEntry.Value);
			}
		}

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x0600276C RID: 10092 RVA: 0x00003062 File Offset: 0x00001262
		bool ICollection<KeyValuePair<string, string>>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600276D RID: 10093 RVA: 0x00088B68 File Offset: 0x00086D68
		bool ICollection<KeyValuePair<string, string>>.Remove(KeyValuePair<string, string> item)
		{
			if (!((ICollection<KeyValuePair<string, string>>)this).Contains(item))
			{
				return false;
			}
			this.m_stringDictionary.Remove(item.Key);
			return true;
		}

		// Token: 0x0600276E RID: 10094 RVA: 0x00088B88 File Offset: 0x00086D88
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600276F RID: 10095 RVA: 0x00088B90 File Offset: 0x00086D90
		public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
		{
			foreach (object obj in this.m_stringDictionary)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				yield return new KeyValuePair<string, string>((string)dictionaryEntry.Key, (string)dictionaryEntry.Value);
			}
			IEnumerator enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0400155F RID: 5471
		private StringDictionary m_stringDictionary;

		// Token: 0x04001560 RID: 5472
		private GenericAdapter.ICollectionToGenericCollectionAdapter _values;

		// Token: 0x04001561 RID: 5473
		private GenericAdapter.ICollectionToGenericCollectionAdapter _keys;

		// Token: 0x020004C2 RID: 1218
		internal enum KeyOrValue
		{
			// Token: 0x04001563 RID: 5475
			Key,
			// Token: 0x04001564 RID: 5476
			Value
		}

		// Token: 0x020004C3 RID: 1219
		private class ICollectionToGenericCollectionAdapter : ICollection<string>, IEnumerable<string>, IEnumerable
		{
			// Token: 0x06002770 RID: 10096 RVA: 0x00088B9F File Offset: 0x00086D9F
			public ICollectionToGenericCollectionAdapter(StringDictionary source, GenericAdapter.KeyOrValue keyOrValue)
			{
				if (source == null)
				{
					throw new ArgumentNullException("source");
				}
				this._internal = source;
				this._keyOrValue = keyOrValue;
			}

			// Token: 0x06002771 RID: 10097 RVA: 0x00088BC3 File Offset: 0x00086DC3
			public void Add(string item)
			{
				this.ThrowNotSupportedException();
			}

			// Token: 0x06002772 RID: 10098 RVA: 0x00088BC3 File Offset: 0x00086DC3
			public void Clear()
			{
				this.ThrowNotSupportedException();
			}

			// Token: 0x06002773 RID: 10099 RVA: 0x00088BCB File Offset: 0x00086DCB
			public void ThrowNotSupportedException()
			{
				if (this._keyOrValue == GenericAdapter.KeyOrValue.Key)
				{
					throw new NotSupportedException(SR.GetString("Mutating a key collection derived from a dictionary is not allowed."));
				}
				throw new NotSupportedException(SR.GetString("Mutating a value collection derived from a dictionary is not allowed."));
			}

			// Token: 0x06002774 RID: 10100 RVA: 0x00088BF4 File Offset: 0x00086DF4
			public bool Contains(string item)
			{
				if (this._keyOrValue == GenericAdapter.KeyOrValue.Key)
				{
					return this._internal.ContainsKey(item);
				}
				return this._internal.ContainsValue(item);
			}

			// Token: 0x06002775 RID: 10101 RVA: 0x00088C17 File Offset: 0x00086E17
			public void CopyTo(string[] array, int arrayIndex)
			{
				this.GetUnderlyingCollection().CopyTo(array, arrayIndex);
			}

			// Token: 0x1700080A RID: 2058
			// (get) Token: 0x06002776 RID: 10102 RVA: 0x00088C26 File Offset: 0x00086E26
			public int Count
			{
				get
				{
					return this._internal.Count;
				}
			}

			// Token: 0x1700080B RID: 2059
			// (get) Token: 0x06002777 RID: 10103 RVA: 0x0000390E File Offset: 0x00001B0E
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06002778 RID: 10104 RVA: 0x00088C33 File Offset: 0x00086E33
			public bool Remove(string item)
			{
				this.ThrowNotSupportedException();
				return false;
			}

			// Token: 0x06002779 RID: 10105 RVA: 0x00088C3C File Offset: 0x00086E3C
			private ICollection GetUnderlyingCollection()
			{
				if (this._keyOrValue == GenericAdapter.KeyOrValue.Key)
				{
					return this._internal.Keys;
				}
				return this._internal.Values;
			}

			// Token: 0x0600277A RID: 10106 RVA: 0x00088C5D File Offset: 0x00086E5D
			public IEnumerator<string> GetEnumerator()
			{
				ICollection underlyingCollection = this.GetUnderlyingCollection();
				foreach (object obj in underlyingCollection)
				{
					string text = (string)obj;
					yield return text;
				}
				IEnumerator enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x0600277B RID: 10107 RVA: 0x00088C6C File Offset: 0x00086E6C
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetUnderlyingCollection().GetEnumerator();
			}

			// Token: 0x04001565 RID: 5477
			private StringDictionary _internal;

			// Token: 0x04001566 RID: 5478
			private GenericAdapter.KeyOrValue _keyOrValue;
		}
	}
}
