using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Wintellect.PowerCollections
{
	// Token: 0x02000302 RID: 770
	[DebuggerDisplay("{DebuggerDisplayString()}")]
	[Serializable]
	public abstract class ReadOnlyDictionaryBase<TKey, TValue> : ReadOnlyCollectionBase<KeyValuePair<TKey, TValue>>, IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IDictionary, ICollection
	{
		// Token: 0x06001C34 RID: 7220 RVA: 0x0008B9E0 File Offset: 0x00089BE0
		private void MethodModifiesCollection()
		{
			throw new NotSupportedException(string.Format(Strings.CannotModifyCollection, Util.SimpleClassName(base.GetType())));
		}

		// Token: 0x06001C35 RID: 7221 RVA: 0x0008B9FC File Offset: 0x00089BFC
		void IDictionary<TKey, TValue>.Add(TKey key, TValue value)
		{
			this.MethodModifiesCollection();
		}

		// Token: 0x06001C36 RID: 7222 RVA: 0x0008BA04 File Offset: 0x00089C04
		public virtual bool Remove(TKey key)
		{
			this.MethodModifiesCollection();
			return false;
		}

		// Token: 0x06001C37 RID: 7223 RVA: 0x0008BA10 File Offset: 0x00089C10
		public virtual bool ContainsKey(TKey key)
		{
			TValue tvalue;
			return this.TryGetValue(key, out tvalue);
		}

		// Token: 0x06001C38 RID: 7224
		public abstract bool TryGetValue(TKey key, out TValue value);

		// Token: 0x170003BC RID: 956
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
				this.MethodModifiesCollection();
			}
		}

		// Token: 0x06001C3B RID: 7227 RVA: 0x0008BA54 File Offset: 0x00089C54
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

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06001C3C RID: 7228 RVA: 0x0008BB50 File Offset: 0x00089D50
		public virtual ICollection<TKey> Keys
		{
			get
			{
				return new ReadOnlyDictionaryBase<TKey, TValue>.KeysCollection(this);
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06001C3D RID: 7229 RVA: 0x0008BB58 File Offset: 0x00089D58
		public virtual ICollection<TValue> Values
		{
			get
			{
				return new ReadOnlyDictionaryBase<TKey, TValue>.ValuesCollection(this);
			}
		}

		// Token: 0x06001C3E RID: 7230 RVA: 0x0008BB60 File Offset: 0x00089D60
		public override bool Contains(KeyValuePair<TKey, TValue> item)
		{
			return this.ContainsKey(item.Key) && object.Equals(this[item.Key], item.Value);
		}

		// Token: 0x06001C3F RID: 7231 RVA: 0x0008BB96 File Offset: 0x00089D96
		void IDictionary.Add(object key, object value)
		{
			this.MethodModifiesCollection();
		}

		// Token: 0x06001C40 RID: 7232 RVA: 0x0008BB9E File Offset: 0x00089D9E
		void IDictionary.Clear()
		{
			this.MethodModifiesCollection();
		}

		// Token: 0x06001C41 RID: 7233 RVA: 0x0008BBA6 File Offset: 0x00089DA6
		bool IDictionary.Contains(object key)
		{
			return (key is TKey || key == null) && this.ContainsKey((TKey)((object)key));
		}

		// Token: 0x06001C42 RID: 7234 RVA: 0x0008BBC1 File Offset: 0x00089DC1
		void IDictionary.Remove(object key)
		{
			this.MethodModifiesCollection();
		}

		// Token: 0x06001C43 RID: 7235 RVA: 0x0008BBC9 File Offset: 0x00089DC9
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new ReadOnlyDictionaryBase<TKey, TValue>.DictionaryEnumeratorWrapper(this.GetEnumerator());
		}

		// Token: 0x06001C44 RID: 7236 RVA: 0x0008BBD6 File Offset: 0x00089DD6
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IDictionary)this).GetEnumerator();
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06001C45 RID: 7237 RVA: 0x0008BBDE File Offset: 0x00089DDE
		bool IDictionary.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06001C46 RID: 7238 RVA: 0x0008BBE1 File Offset: 0x00089DE1
		bool IDictionary.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06001C47 RID: 7239 RVA: 0x0008BBE4 File Offset: 0x00089DE4
		ICollection IDictionary.Keys
		{
			get
			{
				return new ReadOnlyDictionaryBase<TKey, TValue>.KeysCollection(this);
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06001C48 RID: 7240 RVA: 0x0008BBEC File Offset: 0x00089DEC
		ICollection IDictionary.Values
		{
			get
			{
				return new ReadOnlyDictionaryBase<TKey, TValue>.ValuesCollection(this);
			}
		}

		// Token: 0x170003C3 RID: 963
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
				this.MethodModifiesCollection();
			}
		}

		// Token: 0x06001C4B RID: 7243 RVA: 0x0008BC30 File Offset: 0x00089E30
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

		// Token: 0x020016FB RID: 5883
		[Serializable]
		private sealed class KeysCollection : ReadOnlyCollectionBase<TKey>
		{
			// Token: 0x0600DB41 RID: 56129 RVA: 0x00505D8F File Offset: 0x00503F8F
			public KeysCollection(ReadOnlyDictionaryBase<TKey, TValue> myDictionary)
			{
				this.myDictionary = myDictionary;
			}

			// Token: 0x17000DD4 RID: 3540
			// (get) Token: 0x0600DB42 RID: 56130 RVA: 0x00505D9E File Offset: 0x00503F9E
			public override int Count
			{
				get
				{
					return this.myDictionary.Count;
				}
			}

			// Token: 0x0600DB43 RID: 56131 RVA: 0x00505DAB File Offset: 0x00503FAB
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

			// Token: 0x0600DB44 RID: 56132 RVA: 0x00505DBA File Offset: 0x00503FBA
			public override bool Contains(TKey key)
			{
				return this.myDictionary.ContainsKey(key);
			}

			// Token: 0x04005C6E RID: 23662
			private ReadOnlyDictionaryBase<TKey, TValue> myDictionary;
		}

		// Token: 0x020016FC RID: 5884
		[Serializable]
		private sealed class ValuesCollection : ReadOnlyCollectionBase<TValue>
		{
			// Token: 0x0600DB45 RID: 56133 RVA: 0x00505DC8 File Offset: 0x00503FC8
			public ValuesCollection(ReadOnlyDictionaryBase<TKey, TValue> myDictionary)
			{
				this.myDictionary = myDictionary;
			}

			// Token: 0x17000DD5 RID: 3541
			// (get) Token: 0x0600DB46 RID: 56134 RVA: 0x00505DD7 File Offset: 0x00503FD7
			public override int Count
			{
				get
				{
					return this.myDictionary.Count;
				}
			}

			// Token: 0x0600DB47 RID: 56135 RVA: 0x00505DE4 File Offset: 0x00503FE4
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

			// Token: 0x04005C6F RID: 23663
			private ReadOnlyDictionaryBase<TKey, TValue> myDictionary;
		}

		// Token: 0x020016FD RID: 5885
		[Serializable]
		private class DictionaryEnumeratorWrapper : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x0600DB48 RID: 56136 RVA: 0x00505DF3 File Offset: 0x00503FF3
			public DictionaryEnumeratorWrapper(IEnumerator<KeyValuePair<TKey, TValue>> enumerator)
			{
				this.enumerator = enumerator;
			}

			// Token: 0x17000DD6 RID: 3542
			// (get) Token: 0x0600DB49 RID: 56137 RVA: 0x00505E04 File Offset: 0x00504004
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

			// Token: 0x17000DD7 RID: 3543
			// (get) Token: 0x0600DB4A RID: 56138 RVA: 0x00505E5C File Offset: 0x0050405C
			public object Key
			{
				get
				{
					KeyValuePair<TKey, TValue> keyValuePair = this.enumerator.Current;
					return keyValuePair.Key;
				}
			}

			// Token: 0x17000DD8 RID: 3544
			// (get) Token: 0x0600DB4B RID: 56139 RVA: 0x00505E84 File Offset: 0x00504084
			public object Value
			{
				get
				{
					KeyValuePair<TKey, TValue> keyValuePair = this.enumerator.Current;
					return keyValuePair.Value;
				}
			}

			// Token: 0x0600DB4C RID: 56140 RVA: 0x00505EA9 File Offset: 0x005040A9
			public void Reset()
			{
				throw new NotSupportedException(Strings.ResetNotSupported);
			}

			// Token: 0x0600DB4D RID: 56141 RVA: 0x00505EB5 File Offset: 0x005040B5
			public bool MoveNext()
			{
				return this.enumerator.MoveNext();
			}

			// Token: 0x17000DD9 RID: 3545
			// (get) Token: 0x0600DB4E RID: 56142 RVA: 0x00505EC2 File Offset: 0x005040C2
			public object Current
			{
				get
				{
					return this.Entry;
				}
			}

			// Token: 0x04005C70 RID: 23664
			private IEnumerator<KeyValuePair<TKey, TValue>> enumerator;
		}
	}
}
