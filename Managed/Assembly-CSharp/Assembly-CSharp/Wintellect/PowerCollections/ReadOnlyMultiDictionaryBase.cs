using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Wintellect.PowerCollections
{
	// Token: 0x02000304 RID: 772
	[DebuggerDisplay("{DebuggerDisplayString()}")]
	[Serializable]
	public abstract class ReadOnlyMultiDictionaryBase<TKey, TValue> : ReadOnlyCollectionBase<KeyValuePair<TKey, ICollection<TValue>>>, IDictionary<TKey, ICollection<TValue>>, ICollection<KeyValuePair<TKey, ICollection<TValue>>>, IEnumerable<KeyValuePair<TKey, ICollection<TValue>>>, IEnumerable
	{
		// Token: 0x06001C75 RID: 7285 RVA: 0x0008BFF3 File Offset: 0x0008A1F3
		private void MethodModifiesCollection()
		{
			throw new NotSupportedException(string.Format(Strings.CannotModifyCollection, Util.SimpleClassName(base.GetType())));
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06001C76 RID: 7286
		public abstract override int Count { get; }

		// Token: 0x06001C77 RID: 7287
		protected abstract IEnumerator<TKey> EnumerateKeys();

		// Token: 0x06001C78 RID: 7288
		protected abstract bool TryEnumerateValuesForKey(TKey key, out IEnumerator<TValue> values);

		// Token: 0x06001C79 RID: 7289 RVA: 0x0008C00F File Offset: 0x0008A20F
		void IDictionary<TKey, ICollection<TValue>>.Add(TKey key, ICollection<TValue> values)
		{
			this.MethodModifiesCollection();
		}

		// Token: 0x06001C7A RID: 7290 RVA: 0x0008C017 File Offset: 0x0008A217
		bool IDictionary<TKey, ICollection<TValue>>.Remove(TKey key)
		{
			this.MethodModifiesCollection();
			return false;
		}

		// Token: 0x06001C7B RID: 7291 RVA: 0x0008C020 File Offset: 0x0008A220
		bool IDictionary<TKey, ICollection<TValue>>.TryGetValue(TKey key, out ICollection<TValue> values)
		{
			if (this.ContainsKey(key))
			{
				values = this[key];
				return true;
			}
			values = null;
			return false;
		}

		// Token: 0x06001C7C RID: 7292 RVA: 0x0008C03C File Offset: 0x0008A23C
		public virtual bool ContainsKey(TKey key)
		{
			IEnumerator<TValue> enumerator;
			return this.TryEnumerateValuesForKey(key, out enumerator);
		}

		// Token: 0x06001C7D RID: 7293
		public abstract bool Contains(TKey key, TValue value);

		// Token: 0x06001C7E RID: 7294 RVA: 0x0008C054 File Offset: 0x0008A254
		public override bool Contains(KeyValuePair<TKey, ICollection<TValue>> pair)
		{
			foreach (TValue tvalue in pair.Value)
			{
				if (!this.Contains(pair.Key, tvalue))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001C7F RID: 7295 RVA: 0x0008C0B4 File Offset: 0x0008A2B4
		protected virtual bool EqualValues(TValue value1, TValue value2)
		{
			if (this.valueEqualityComparer == null)
			{
				this.valueEqualityComparer = EqualityComparer<TValue>.Default;
			}
			return this.valueEqualityComparer.Equals(value1, value2);
		}

		// Token: 0x06001C80 RID: 7296 RVA: 0x0008C0DC File Offset: 0x0008A2DC
		protected virtual int CountValues(TKey key)
		{
			int num = 0;
			IEnumerator<TValue> enumerator;
			if (this.TryEnumerateValuesForKey(key, out enumerator))
			{
				using (enumerator)
				{
					while (enumerator.MoveNext())
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x06001C81 RID: 7297 RVA: 0x0008C124 File Offset: 0x0008A324
		protected virtual int CountAllValues()
		{
			int num = 0;
			using (IEnumerator<TKey> enumerator = this.EnumerateKeys())
			{
				while (enumerator.MoveNext())
				{
					TKey tkey = enumerator.Current;
					num += this.CountValues(tkey);
				}
			}
			return num;
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06001C82 RID: 7298 RVA: 0x0008C174 File Offset: 0x0008A374
		public virtual ICollection<TKey> Keys
		{
			get
			{
				return new ReadOnlyMultiDictionaryBase<TKey, TValue>.KeysCollection(this);
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06001C83 RID: 7299 RVA: 0x0008C17C File Offset: 0x0008A37C
		public virtual ICollection<TValue> Values
		{
			get
			{
				return new ReadOnlyMultiDictionaryBase<TKey, TValue>.ValuesCollection(this);
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06001C84 RID: 7300 RVA: 0x0008C184 File Offset: 0x0008A384
		ICollection<ICollection<TValue>> IDictionary<TKey, ICollection<TValue>>.Values
		{
			get
			{
				return new ReadOnlyMultiDictionaryBase<TKey, TValue>.EnumerableValuesCollection(this);
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06001C85 RID: 7301 RVA: 0x0008C18C File Offset: 0x0008A38C
		public virtual ICollection<KeyValuePair<TKey, TValue>> KeyValuePairs
		{
			get
			{
				return new ReadOnlyMultiDictionaryBase<TKey, TValue>.KeyValuePairsCollection(this);
			}
		}

		// Token: 0x170003CE RID: 974
		public virtual ICollection<TValue> this[TKey key]
		{
			get
			{
				return new ReadOnlyMultiDictionaryBase<TKey, TValue>.ValuesForKeyCollection(this, key);
			}
		}

		// Token: 0x170003CF RID: 975
		ICollection<TValue> IDictionary<TKey, ICollection<TValue>>.this[TKey key]
		{
			get
			{
				if (this.ContainsKey(key))
				{
					return new ReadOnlyMultiDictionaryBase<TKey, TValue>.ValuesForKeyCollection(this, key);
				}
				throw new KeyNotFoundException(Strings.KeyNotFound);
			}
			set
			{
				this.MethodModifiesCollection();
			}
		}

		// Token: 0x06001C89 RID: 7305 RVA: 0x0008C1C4 File Offset: 0x0008A3C4
		public override string ToString()
		{
			bool flag = true;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{");
			foreach (KeyValuePair<TKey, ICollection<TValue>> keyValuePair in this)
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
				stringBuilder.Append('(');
				bool flag2 = true;
				foreach (TValue tvalue in keyValuePair.Value)
				{
					if (!flag2)
					{
						stringBuilder.Append(",");
					}
					if (tvalue == null)
					{
						stringBuilder.Append("null");
					}
					else
					{
						stringBuilder.Append(tvalue.ToString());
					}
					flag2 = false;
				}
				stringBuilder.Append(')');
				flag = false;
			}
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x06001C8A RID: 7306 RVA: 0x0008C318 File Offset: 0x0008A518
		internal new string DebuggerDisplayString()
		{
			bool flag = true;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{");
			foreach (KeyValuePair<TKey, ICollection<TValue>> keyValuePair in this)
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
				stringBuilder.Append('(');
				bool flag2 = true;
				foreach (TValue tvalue in keyValuePair.Value)
				{
					if (!flag2)
					{
						stringBuilder.Append(",");
					}
					if (tvalue == null)
					{
						stringBuilder.Append("null");
					}
					else
					{
						stringBuilder.Append(tvalue.ToString());
					}
					flag2 = false;
				}
				stringBuilder.Append(')');
				flag = false;
			}
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x06001C8B RID: 7307 RVA: 0x0008C4A0 File Offset: 0x0008A6A0
		public override IEnumerator<KeyValuePair<TKey, ICollection<TValue>>> GetEnumerator()
		{
			using (IEnumerator<TKey> enumKeys = this.EnumerateKeys())
			{
				while (enumKeys.MoveNext())
				{
					TKey tkey = enumKeys.Current;
					yield return new KeyValuePair<TKey, ICollection<TValue>>(tkey, new ReadOnlyMultiDictionaryBase<TKey, TValue>.ValuesForKeyCollection(this, tkey));
				}
			}
			IEnumerator<TKey> enumKeys = null;
			yield break;
			yield break;
		}

		// Token: 0x04000F25 RID: 3877
		private volatile IEqualityComparer<TValue> valueEqualityComparer;

		// Token: 0x020016FF RID: 5887
		[Serializable]
		private sealed class ValuesForKeyCollection : ReadOnlyCollectionBase<TValue>
		{
			// Token: 0x0600DB55 RID: 56149 RVA: 0x00505F7D File Offset: 0x0050417D
			public ValuesForKeyCollection(ReadOnlyMultiDictionaryBase<TKey, TValue> myDictionary, TKey key)
			{
				this.myDictionary = myDictionary;
				this.key = key;
			}

			// Token: 0x17000DDC RID: 3548
			// (get) Token: 0x0600DB56 RID: 56150 RVA: 0x00505F93 File Offset: 0x00504193
			public override int Count
			{
				get
				{
					return this.myDictionary.CountValues(this.key);
				}
			}

			// Token: 0x0600DB57 RID: 56151 RVA: 0x00505FA6 File Offset: 0x005041A6
			private IEnumerator<TValue> NoValues()
			{
				yield break;
			}

			// Token: 0x0600DB58 RID: 56152 RVA: 0x00505FB0 File Offset: 0x005041B0
			public override IEnumerator<TValue> GetEnumerator()
			{
				IEnumerator<TValue> enumerator;
				if (this.myDictionary.TryEnumerateValuesForKey(this.key, out enumerator))
				{
					return enumerator;
				}
				return this.NoValues();
			}

			// Token: 0x0600DB59 RID: 56153 RVA: 0x00505FDA File Offset: 0x005041DA
			public override bool Contains(TValue item)
			{
				return this.myDictionary.Contains(this.key, item);
			}

			// Token: 0x04005C76 RID: 23670
			private ReadOnlyMultiDictionaryBase<TKey, TValue> myDictionary;

			// Token: 0x04005C77 RID: 23671
			private TKey key;
		}

		// Token: 0x02001700 RID: 5888
		[Serializable]
		private sealed class KeysCollection : ReadOnlyCollectionBase<TKey>
		{
			// Token: 0x0600DB5A RID: 56154 RVA: 0x00505FEE File Offset: 0x005041EE
			public KeysCollection(ReadOnlyMultiDictionaryBase<TKey, TValue> myDictionary)
			{
				this.myDictionary = myDictionary;
			}

			// Token: 0x17000DDD RID: 3549
			// (get) Token: 0x0600DB5B RID: 56155 RVA: 0x00505FFD File Offset: 0x005041FD
			public override int Count
			{
				get
				{
					return this.myDictionary.Count;
				}
			}

			// Token: 0x0600DB5C RID: 56156 RVA: 0x0050600A File Offset: 0x0050420A
			public override IEnumerator<TKey> GetEnumerator()
			{
				return this.myDictionary.EnumerateKeys();
			}

			// Token: 0x0600DB5D RID: 56157 RVA: 0x00506017 File Offset: 0x00504217
			public override bool Contains(TKey key)
			{
				return this.myDictionary.ContainsKey(key);
			}

			// Token: 0x04005C78 RID: 23672
			private ReadOnlyMultiDictionaryBase<TKey, TValue> myDictionary;
		}

		// Token: 0x02001701 RID: 5889
		[Serializable]
		private sealed class ValuesCollection : ReadOnlyCollectionBase<TValue>
		{
			// Token: 0x0600DB5E RID: 56158 RVA: 0x00506025 File Offset: 0x00504225
			public ValuesCollection(ReadOnlyMultiDictionaryBase<TKey, TValue> myDictionary)
			{
				this.myDictionary = myDictionary;
			}

			// Token: 0x17000DDE RID: 3550
			// (get) Token: 0x0600DB5F RID: 56159 RVA: 0x00506034 File Offset: 0x00504234
			public override int Count
			{
				get
				{
					return this.myDictionary.CountAllValues();
				}
			}

			// Token: 0x0600DB60 RID: 56160 RVA: 0x00506041 File Offset: 0x00504241
			public override IEnumerator<TValue> GetEnumerator()
			{
				using (IEnumerator<TKey> enumKeys = this.myDictionary.EnumerateKeys())
				{
					while (enumKeys.MoveNext())
					{
						TKey tkey = enumKeys.Current;
						IEnumerator<TValue> enumValues;
						if (this.myDictionary.TryEnumerateValuesForKey(tkey, out enumValues))
						{
							using (enumValues)
							{
								while (enumValues.MoveNext())
								{
									TValue tvalue = enumValues.Current;
									yield return tvalue;
								}
							}
							IEnumerator<TValue> enumerator = null;
						}
						enumValues = null;
					}
				}
				IEnumerator<TKey> enumKeys = null;
				yield break;
				yield break;
			}

			// Token: 0x0600DB61 RID: 56161 RVA: 0x00506050 File Offset: 0x00504250
			public override bool Contains(TValue value)
			{
				foreach (TValue tvalue in this)
				{
					if (this.myDictionary.EqualValues(tvalue, value))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x04005C79 RID: 23673
			private ReadOnlyMultiDictionaryBase<TKey, TValue> myDictionary;
		}

		// Token: 0x02001702 RID: 5890
		[Serializable]
		private sealed class EnumerableValuesCollection : ReadOnlyCollectionBase<ICollection<TValue>>
		{
			// Token: 0x0600DB62 RID: 56162 RVA: 0x005060A8 File Offset: 0x005042A8
			public EnumerableValuesCollection(ReadOnlyMultiDictionaryBase<TKey, TValue> myDictionary)
			{
				this.myDictionary = myDictionary;
			}

			// Token: 0x17000DDF RID: 3551
			// (get) Token: 0x0600DB63 RID: 56163 RVA: 0x005060B7 File Offset: 0x005042B7
			public override int Count
			{
				get
				{
					return this.myDictionary.Count;
				}
			}

			// Token: 0x0600DB64 RID: 56164 RVA: 0x005060C4 File Offset: 0x005042C4
			public override IEnumerator<ICollection<TValue>> GetEnumerator()
			{
				using (IEnumerator<TKey> enumKeys = this.myDictionary.EnumerateKeys())
				{
					while (enumKeys.MoveNext())
					{
						TKey tkey = enumKeys.Current;
						yield return new ReadOnlyMultiDictionaryBase<TKey, TValue>.ValuesForKeyCollection(this.myDictionary, tkey);
					}
				}
				IEnumerator<TKey> enumKeys = null;
				yield break;
				yield break;
			}

			// Token: 0x0600DB65 RID: 56165 RVA: 0x005060D4 File Offset: 0x005042D4
			public override bool Contains(ICollection<TValue> values)
			{
				if (values == null)
				{
					return false;
				}
				TValue[] array = Algorithms.ToArray<TValue>(values);
				foreach (ICollection<TValue> collection in this)
				{
					if (collection.Count == array.Length)
					{
						if (Algorithms.EqualCollections<TValue>(collection, values, new BinaryPredicate<TValue>(this.myDictionary.EqualValues)))
						{
							return true;
						}
						bool[] array2 = new bool[array.Length];
						foreach (TValue tvalue in collection)
						{
							for (int i = 0; i < array.Length; i++)
							{
								if (!array2[i] && this.myDictionary.EqualValues(tvalue, array[i]))
								{
									array2[i] = true;
								}
							}
						}
						if (Array.IndexOf<bool>(array2, false) < 0)
						{
							return true;
						}
					}
				}
				return false;
			}

			// Token: 0x04005C7A RID: 23674
			private ReadOnlyMultiDictionaryBase<TKey, TValue> myDictionary;
		}

		// Token: 0x02001703 RID: 5891
		[Serializable]
		private sealed class KeyValuePairsCollection : ReadOnlyCollectionBase<KeyValuePair<TKey, TValue>>
		{
			// Token: 0x0600DB66 RID: 56166 RVA: 0x005061E0 File Offset: 0x005043E0
			public KeyValuePairsCollection(ReadOnlyMultiDictionaryBase<TKey, TValue> myDictionary)
			{
				this.myDictionary = myDictionary;
			}

			// Token: 0x17000DE0 RID: 3552
			// (get) Token: 0x0600DB67 RID: 56167 RVA: 0x005061EF File Offset: 0x005043EF
			public override int Count
			{
				get
				{
					return this.myDictionary.CountAllValues();
				}
			}

			// Token: 0x0600DB68 RID: 56168 RVA: 0x005061FC File Offset: 0x005043FC
			public override IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
			{
				using (IEnumerator<TKey> enumKeys = this.myDictionary.EnumerateKeys())
				{
					while (enumKeys.MoveNext())
					{
						TKey key = enumKeys.Current;
						IEnumerator<TValue> enumValues;
						if (this.myDictionary.TryEnumerateValuesForKey(key, out enumValues))
						{
							using (enumValues)
							{
								while (enumValues.MoveNext())
								{
									TValue tvalue = enumValues.Current;
									yield return new KeyValuePair<TKey, TValue>(key, tvalue);
								}
							}
							IEnumerator<TValue> enumerator = null;
						}
						key = default(TKey);
						enumValues = null;
					}
				}
				IEnumerator<TKey> enumKeys = null;
				yield break;
				yield break;
			}

			// Token: 0x0600DB69 RID: 56169 RVA: 0x0050620B File Offset: 0x0050440B
			public override bool Contains(KeyValuePair<TKey, TValue> pair)
			{
				return this.myDictionary[pair.Key].Contains(pair.Value);
			}

			// Token: 0x04005C7B RID: 23675
			private ReadOnlyMultiDictionaryBase<TKey, TValue> myDictionary;
		}
	}
}
