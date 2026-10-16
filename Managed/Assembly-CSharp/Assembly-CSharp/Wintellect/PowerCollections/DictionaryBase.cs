using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Wintellect.PowerCollections
{
	// Token: 0x020002F7 RID: 759
	[DebuggerDisplay("{DebuggerDisplayString()}")]
	[Serializable]
	public abstract class DictionaryBase<TKey, TValue> : CollectionBase<KeyValuePair<TKey, TValue>>, IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IDictionary, ICollection
	{
		// Token: 0x06001ACC RID: 6860
		public abstract override void Clear();

		// Token: 0x06001ACD RID: 6861
		public abstract bool Remove(TKey key);

		// Token: 0x06001ACE RID: 6862
		public abstract bool TryGetValue(TKey key, out TValue value);

		// Token: 0x06001ACF RID: 6863 RVA: 0x00087708 File Offset: 0x00085908
		public virtual void Add(TKey key, TValue value)
		{
			if (this.ContainsKey(key))
			{
				throw new ArgumentException(Strings.KeyAlreadyPresent, "key");
			}
			this[key] = value;
		}

		// Token: 0x06001AD0 RID: 6864 RVA: 0x0008772C File Offset: 0x0008592C
		public virtual bool ContainsKey(TKey key)
		{
			TValue tvalue;
			return this.TryGetValue(key, out tvalue);
		}

		// Token: 0x17000391 RID: 913
		public virtual TValue this[TKey key]
		{
			get
			{
				TValue tvalue;
				if (this.TryGetValue(key, out tvalue))
				{
					return tvalue;
				}
				throw new KeyNotFoundException(Strings.KeyNotFound);
			}
			set
			{
				throw new NotImplementedException(Strings.MustOverrideIndexerSet);
			}
		}

		// Token: 0x06001AD3 RID: 6867 RVA: 0x00087774 File Offset: 0x00085974
		public override string ToString()
		{
			bool flag = true;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{");
			foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
			{
				if (!flag)
				{
					stringBuilder.Append(", ");
				}
				if (keyValuePair.Key == null)
				{
					stringBuilder.Append("null");
				}
				else
				{
					StringBuilder stringBuilder2 = stringBuilder;
					TKey key = keyValuePair.Key;
					stringBuilder2.Append(key.ToString());
				}
				stringBuilder.Append("->");
				if (keyValuePair.Value == null)
				{
					stringBuilder.Append("null");
				}
				else
				{
					StringBuilder stringBuilder3 = stringBuilder;
					TValue value = keyValuePair.Value;
					stringBuilder3.Append(value.ToString());
				}
				flag = false;
			}
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x06001AD4 RID: 6868 RVA: 0x00087870 File Offset: 0x00085A70
		public new virtual IDictionary<TKey, TValue> AsReadOnly()
		{
			return Algorithms.ReadOnly<TKey, TValue>(this);
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06001AD5 RID: 6869 RVA: 0x00087878 File Offset: 0x00085A78
		public virtual ICollection<TKey> Keys
		{
			get
			{
				return new DictionaryBase<TKey, TValue>.KeysCollection(this);
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06001AD6 RID: 6870 RVA: 0x00087880 File Offset: 0x00085A80
		public virtual ICollection<TValue> Values
		{
			get
			{
				return new DictionaryBase<TKey, TValue>.ValuesCollection(this);
			}
		}

		// Token: 0x06001AD7 RID: 6871 RVA: 0x00087888 File Offset: 0x00085A88
		public override void Add(KeyValuePair<TKey, TValue> item)
		{
			this.Add(item.Key, item.Value);
		}

		// Token: 0x06001AD8 RID: 6872 RVA: 0x0008789E File Offset: 0x00085A9E
		public override bool Contains(KeyValuePair<TKey, TValue> item)
		{
			return this.ContainsKey(item.Key) && object.Equals(this[item.Key], item.Value);
		}

		// Token: 0x06001AD9 RID: 6873 RVA: 0x000878D4 File Offset: 0x00085AD4
		public override bool Remove(KeyValuePair<TKey, TValue> item)
		{
			return ((ICollection<KeyValuePair<TKey, TValue>>)this).Contains(item) && this.Remove(item.Key);
		}

		// Token: 0x06001ADA RID: 6874 RVA: 0x000878EE File Offset: 0x00085AEE
		private void CheckGenericType<ExpectedType>(string name, object value)
		{
			if (!(value is ExpectedType))
			{
				throw new ArgumentException(string.Format(Strings.WrongType, value, typeof(ExpectedType)), name);
			}
		}

		// Token: 0x06001ADB RID: 6875 RVA: 0x00087914 File Offset: 0x00085B14
		void IDictionary.Add(object key, object value)
		{
			this.CheckGenericType<TKey>("key", key);
			this.CheckGenericType<TValue>("value", value);
			this.Add((TKey)((object)key), (TValue)((object)value));
		}

		// Token: 0x06001ADC RID: 6876 RVA: 0x00087940 File Offset: 0x00085B40
		void IDictionary.Clear()
		{
			this.Clear();
		}

		// Token: 0x06001ADD RID: 6877 RVA: 0x00087948 File Offset: 0x00085B48
		bool IDictionary.Contains(object key)
		{
			return (key is TKey || key == null) && this.ContainsKey((TKey)((object)key));
		}

		// Token: 0x06001ADE RID: 6878 RVA: 0x00087963 File Offset: 0x00085B63
		void IDictionary.Remove(object key)
		{
			if (key is TKey || key == null)
			{
				this.Remove((TKey)((object)key));
			}
		}

		// Token: 0x06001ADF RID: 6879 RVA: 0x0008797D File Offset: 0x00085B7D
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new DictionaryBase<TKey, TValue>.DictionaryEnumeratorWrapper(this.GetEnumerator());
		}

		// Token: 0x06001AE0 RID: 6880 RVA: 0x0008798A File Offset: 0x00085B8A
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IDictionary)this).GetEnumerator();
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06001AE1 RID: 6881 RVA: 0x00087992 File Offset: 0x00085B92
		bool IDictionary.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06001AE2 RID: 6882 RVA: 0x00087995 File Offset: 0x00085B95
		bool IDictionary.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06001AE3 RID: 6883 RVA: 0x00087998 File Offset: 0x00085B98
		ICollection IDictionary.Keys
		{
			get
			{
				return new DictionaryBase<TKey, TValue>.KeysCollection(this);
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06001AE4 RID: 6884 RVA: 0x000879A0 File Offset: 0x00085BA0
		ICollection IDictionary.Values
		{
			get
			{
				return new DictionaryBase<TKey, TValue>.ValuesCollection(this);
			}
		}

		// Token: 0x17000398 RID: 920
		object IDictionary.this[object key]
		{
			get
			{
				if (!(key is TKey) && key != null)
				{
					return null;
				}
				TKey tkey = (TKey)((object)key);
				TValue tvalue;
				if (this.TryGetValue(tkey, out tvalue))
				{
					return tvalue;
				}
				return null;
			}
			set
			{
				this.CheckGenericType<TKey>("key", key);
				this.CheckGenericType<TValue>("value", value);
				this[(TKey)((object)key)] = (TValue)((object)value);
			}
		}

		// Token: 0x06001AE7 RID: 6887 RVA: 0x00087A08 File Offset: 0x00085C08
		internal new string DebuggerDisplayString()
		{
			bool flag = true;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{");
			foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
			{
				if (stringBuilder.Length >= 250)
				{
					stringBuilder.Append(", ...");
					break;
				}
				if (!flag)
				{
					stringBuilder.Append(", ");
				}
				if (keyValuePair.Key == null)
				{
					stringBuilder.Append("null");
				}
				else
				{
					StringBuilder stringBuilder2 = stringBuilder;
					TKey key = keyValuePair.Key;
					stringBuilder2.Append(key.ToString());
				}
				stringBuilder.Append("->");
				if (keyValuePair.Value == null)
				{
					stringBuilder.Append("null");
				}
				else
				{
					StringBuilder stringBuilder3 = stringBuilder;
					TValue value = keyValuePair.Value;
					stringBuilder3.Append(value.ToString());
				}
				flag = false;
			}
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x020016DB RID: 5851
		[Serializable]
		private sealed class KeysCollection : ReadOnlyCollectionBase<TKey>
		{
			// Token: 0x0600DA85 RID: 55941 RVA: 0x00503BF1 File Offset: 0x00501DF1
			public KeysCollection(DictionaryBase<TKey, TValue> myDictionary)
			{
				this.myDictionary = myDictionary;
			}

			// Token: 0x17000DA8 RID: 3496
			// (get) Token: 0x0600DA86 RID: 55942 RVA: 0x00503C00 File Offset: 0x00501E00
			public override int Count
			{
				get
				{
					return this.myDictionary.Count;
				}
			}

			// Token: 0x0600DA87 RID: 55943 RVA: 0x00503C0D File Offset: 0x00501E0D
			public override IEnumerator<TKey> GetEnumerator()
			{
				foreach (KeyValuePair<TKey, TValue> keyValuePair in this.myDictionary)
				{
					yield return keyValuePair.Key;
				}
				IEnumerator<KeyValuePair<TKey, TValue>> enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x0600DA88 RID: 55944 RVA: 0x00503C1C File Offset: 0x00501E1C
			public override bool Contains(TKey key)
			{
				return this.myDictionary.ContainsKey(key);
			}

			// Token: 0x04005C08 RID: 23560
			private DictionaryBase<TKey, TValue> myDictionary;
		}

		// Token: 0x020016DC RID: 5852
		[Serializable]
		private sealed class ValuesCollection : ReadOnlyCollectionBase<TValue>
		{
			// Token: 0x0600DA89 RID: 55945 RVA: 0x00503C2A File Offset: 0x00501E2A
			public ValuesCollection(DictionaryBase<TKey, TValue> myDictionary)
			{
				this.myDictionary = myDictionary;
			}

			// Token: 0x17000DA9 RID: 3497
			// (get) Token: 0x0600DA8A RID: 55946 RVA: 0x00503C39 File Offset: 0x00501E39
			public override int Count
			{
				get
				{
					return this.myDictionary.Count;
				}
			}

			// Token: 0x0600DA8B RID: 55947 RVA: 0x00503C46 File Offset: 0x00501E46
			public override IEnumerator<TValue> GetEnumerator()
			{
				foreach (KeyValuePair<TKey, TValue> keyValuePair in this.myDictionary)
				{
					yield return keyValuePair.Value;
				}
				IEnumerator<KeyValuePair<TKey, TValue>> enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x04005C09 RID: 23561
			private DictionaryBase<TKey, TValue> myDictionary;
		}

		// Token: 0x020016DD RID: 5853
		[Serializable]
		private class DictionaryEnumeratorWrapper : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x0600DA8C RID: 55948 RVA: 0x00503C55 File Offset: 0x00501E55
			public DictionaryEnumeratorWrapper(IEnumerator<KeyValuePair<TKey, TValue>> enumerator)
			{
				this.enumerator = enumerator;
			}

			// Token: 0x17000DAA RID: 3498
			// (get) Token: 0x0600DA8D RID: 55949 RVA: 0x00503C64 File Offset: 0x00501E64
			public DictionaryEntry Entry
			{
				get
				{
					KeyValuePair<TKey, TValue> keyValuePair = this.enumerator.Current;
					DictionaryEntry dictionaryEntry = default(DictionaryEntry);
					if (keyValuePair.Key != null)
					{
						dictionaryEntry.Key = keyValuePair.Key;
					}
					dictionaryEntry.Value = keyValuePair.Value;
					return dictionaryEntry;
				}
			}

			// Token: 0x17000DAB RID: 3499
			// (get) Token: 0x0600DA8E RID: 55950 RVA: 0x00503CBC File Offset: 0x00501EBC
			public object Key
			{
				get
				{
					KeyValuePair<TKey, TValue> keyValuePair = this.enumerator.Current;
					return keyValuePair.Key;
				}
			}

			// Token: 0x17000DAC RID: 3500
			// (get) Token: 0x0600DA8F RID: 55951 RVA: 0x00503CE4 File Offset: 0x00501EE4
			public object Value
			{
				get
				{
					KeyValuePair<TKey, TValue> keyValuePair = this.enumerator.Current;
					return keyValuePair.Value;
				}
			}

			// Token: 0x0600DA90 RID: 55952 RVA: 0x00503D09 File Offset: 0x00501F09
			public void Reset()
			{
				throw new NotSupportedException(Strings.ResetNotSupported);
			}

			// Token: 0x0600DA91 RID: 55953 RVA: 0x00503D15 File Offset: 0x00501F15
			public bool MoveNext()
			{
				return this.enumerator.MoveNext();
			}

			// Token: 0x17000DAD RID: 3501
			// (get) Token: 0x0600DA92 RID: 55954 RVA: 0x00503D22 File Offset: 0x00501F22
			public object Current
			{
				get
				{
					return this.Entry;
				}
			}

			// Token: 0x04005C0A RID: 23562
			private IEnumerator<KeyValuePair<TKey, TValue>> enumerator;
		}
	}
}
