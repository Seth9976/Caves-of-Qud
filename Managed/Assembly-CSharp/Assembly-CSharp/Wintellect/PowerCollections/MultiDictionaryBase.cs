using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Wintellect.PowerCollections
{
	// Token: 0x020002FB RID: 763
	[DebuggerDisplay("{DebuggerDisplayString()}")]
	[Serializable]
	public abstract class MultiDictionaryBase<TKey, TValue> : CollectionBase<KeyValuePair<TKey, ICollection<TValue>>>, IDictionary<TKey, ICollection<TValue>>, ICollection<KeyValuePair<TKey, ICollection<TValue>>>, IEnumerable<KeyValuePair<TKey, ICollection<TValue>>>, IEnumerable
	{
		// Token: 0x06001B43 RID: 6979
		public abstract override void Clear();

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06001B44 RID: 6980
		public abstract override int Count { get; }

		// Token: 0x06001B45 RID: 6981
		protected abstract IEnumerator<TKey> EnumerateKeys();

		// Token: 0x06001B46 RID: 6982
		protected abstract bool TryEnumerateValuesForKey(TKey key, out IEnumerator<TValue> values);

		// Token: 0x06001B47 RID: 6983 RVA: 0x00088DD0 File Offset: 0x00086FD0
		public override void Add(KeyValuePair<TKey, ICollection<TValue>> item)
		{
			this.AddMany(item.Key, item.Value);
		}

		// Token: 0x06001B48 RID: 6984 RVA: 0x00088DE6 File Offset: 0x00086FE6
		void IDictionary<TKey, ICollection<TValue>>.Add(TKey key, ICollection<TValue> values)
		{
			if (this.ContainsKey(key))
			{
				throw new ArgumentException(Strings.KeyAlreadyPresent, "key");
			}
			this.AddMany(key, values);
		}

		// Token: 0x06001B49 RID: 6985 RVA: 0x00088E0C File Offset: 0x0008700C
		public virtual void AddMany(TKey key, IEnumerable<TValue> values)
		{
			foreach (TValue tvalue in values)
			{
				this.Add(key, tvalue);
			}
		}

		// Token: 0x06001B4A RID: 6986
		public abstract void Add(TKey key, TValue value);

		// Token: 0x06001B4B RID: 6987
		public abstract bool Remove(TKey key);

		// Token: 0x06001B4C RID: 6988
		public abstract bool Remove(TKey key, TValue value);

		// Token: 0x06001B4D RID: 6989 RVA: 0x00088E58 File Offset: 0x00087058
		public override bool Remove(KeyValuePair<TKey, ICollection<TValue>> pair)
		{
			return this.RemoveMany(pair.Key, pair.Value) > 0;
		}

		// Token: 0x06001B4E RID: 6990 RVA: 0x00088E74 File Offset: 0x00087074
		public virtual int RemoveMany(TKey key, IEnumerable<TValue> values)
		{
			int num = 0;
			foreach (TValue tvalue in values)
			{
				if (this.Remove(key, tvalue))
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06001B4F RID: 6991 RVA: 0x00088EC8 File Offset: 0x000870C8
		public int RemoveMany(IEnumerable<TKey> keyCollection)
		{
			int num = 0;
			foreach (TKey tkey in keyCollection)
			{
				if (this.Remove(tkey))
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06001B50 RID: 6992 RVA: 0x00088F1C File Offset: 0x0008711C
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

		// Token: 0x06001B51 RID: 6993 RVA: 0x00088F38 File Offset: 0x00087138
		public virtual bool ContainsKey(TKey key)
		{
			IEnumerator<TValue> enumerator;
			return this.TryEnumerateValuesForKey(key, out enumerator);
		}

		// Token: 0x06001B52 RID: 6994
		public abstract bool Contains(TKey key, TValue value);

		// Token: 0x06001B53 RID: 6995 RVA: 0x00088F50 File Offset: 0x00087150
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

		// Token: 0x06001B54 RID: 6996 RVA: 0x00088FB0 File Offset: 0x000871B0
		protected virtual bool EqualValues(TValue value1, TValue value2)
		{
			if (this.valueEqualityComparer == null)
			{
				this.valueEqualityComparer = EqualityComparer<TValue>.Default;
			}
			return this.valueEqualityComparer.Equals(value1, value2);
		}

		// Token: 0x06001B55 RID: 6997 RVA: 0x00088FD8 File Offset: 0x000871D8
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

		// Token: 0x06001B56 RID: 6998 RVA: 0x00089020 File Offset: 0x00087220
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

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06001B57 RID: 6999 RVA: 0x00089070 File Offset: 0x00087270
		public virtual ICollection<TKey> Keys
		{
			get
			{
				return new MultiDictionaryBase<TKey, TValue>.KeysCollection(this);
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06001B58 RID: 7000 RVA: 0x00089078 File Offset: 0x00087278
		public virtual ICollection<TValue> Values
		{
			get
			{
				return new MultiDictionaryBase<TKey, TValue>.ValuesCollection(this);
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06001B59 RID: 7001 RVA: 0x00089080 File Offset: 0x00087280
		ICollection<ICollection<TValue>> IDictionary<TKey, ICollection<TValue>>.Values
		{
			get
			{
				return new MultiDictionaryBase<TKey, TValue>.EnumerableValuesCollection(this);
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06001B5A RID: 7002 RVA: 0x00089088 File Offset: 0x00087288
		public virtual ICollection<KeyValuePair<TKey, TValue>> KeyValuePairs
		{
			get
			{
				return new MultiDictionaryBase<TKey, TValue>.KeyValuePairsCollection(this);
			}
		}

		// Token: 0x170003A9 RID: 937
		public virtual ICollection<TValue> this[TKey key]
		{
			get
			{
				return new MultiDictionaryBase<TKey, TValue>.ValuesForKeyCollection(this, key);
			}
			set
			{
				this.ReplaceMany(key, value);
			}
		}

		// Token: 0x170003AA RID: 938
		ICollection<TValue> IDictionary<TKey, ICollection<TValue>>.this[TKey key]
		{
			get
			{
				if (this.ContainsKey(key))
				{
					return new MultiDictionaryBase<TKey, TValue>.ValuesForKeyCollection(this, key);
				}
				throw new KeyNotFoundException(Strings.KeyNotFound);
			}
			set
			{
				this.ReplaceMany(key, value);
			}
		}

		// Token: 0x06001B5F RID: 7007 RVA: 0x000890CC File Offset: 0x000872CC
		public virtual bool Replace(TKey key, TValue value)
		{
			bool flag = this.Remove(key);
			this.Add(key, value);
			return flag;
		}

		// Token: 0x06001B60 RID: 7008 RVA: 0x000890DD File Offset: 0x000872DD
		public bool ReplaceMany(TKey key, IEnumerable<TValue> values)
		{
			bool flag = this.Remove(key);
			this.AddMany(key, values);
			return flag;
		}

		// Token: 0x06001B61 RID: 7009 RVA: 0x000890F0 File Offset: 0x000872F0
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

		// Token: 0x06001B62 RID: 7010 RVA: 0x00089244 File Offset: 0x00087444
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

		// Token: 0x06001B63 RID: 7011 RVA: 0x000893CC File Offset: 0x000875CC
		public override IEnumerator<KeyValuePair<TKey, ICollection<TValue>>> GetEnumerator()
		{
			using (IEnumerator<TKey> enumKeys = this.EnumerateKeys())
			{
				while (enumKeys.MoveNext())
				{
					TKey tkey = enumKeys.Current;
					yield return new KeyValuePair<TKey, ICollection<TValue>>(tkey, new MultiDictionaryBase<TKey, TValue>.ValuesForKeyCollection(this, tkey));
				}
			}
			IEnumerator<TKey> enumKeys = null;
			yield break;
			yield break;
		}

		// Token: 0x04000F11 RID: 3857
		private volatile IEqualityComparer<TValue> valueEqualityComparer;

		// Token: 0x020016E5 RID: 5861
		[Serializable]
		private sealed class ValuesForKeyCollection : CollectionBase<TValue>
		{
			// Token: 0x0600DAB7 RID: 55991 RVA: 0x005041FE File Offset: 0x005023FE
			public ValuesForKeyCollection(MultiDictionaryBase<TKey, TValue> myDictionary, TKey key)
			{
				this.myDictionary = myDictionary;
				this.key = key;
			}

			// Token: 0x0600DAB8 RID: 55992 RVA: 0x00504214 File Offset: 0x00502414
			public override void Clear()
			{
				this.myDictionary.Remove(this.key);
			}

			// Token: 0x0600DAB9 RID: 55993 RVA: 0x00504228 File Offset: 0x00502428
			public override void Add(TValue item)
			{
				this.myDictionary.Add(this.key, item);
			}

			// Token: 0x0600DABA RID: 55994 RVA: 0x0050423C File Offset: 0x0050243C
			public override bool Remove(TValue item)
			{
				return this.myDictionary.Remove(this.key, item);
			}

			// Token: 0x17000DB9 RID: 3513
			// (get) Token: 0x0600DABB RID: 55995 RVA: 0x00504250 File Offset: 0x00502450
			public override int Count
			{
				get
				{
					return this.myDictionary.CountValues(this.key);
				}
			}

			// Token: 0x0600DABC RID: 55996 RVA: 0x00504263 File Offset: 0x00502463
			private IEnumerator<TValue> NoValues()
			{
				yield break;
			}

			// Token: 0x0600DABD RID: 55997 RVA: 0x0050426C File Offset: 0x0050246C
			public override IEnumerator<TValue> GetEnumerator()
			{
				IEnumerator<TValue> enumerator;
				if (this.myDictionary.TryEnumerateValuesForKey(this.key, out enumerator))
				{
					return enumerator;
				}
				return this.NoValues();
			}

			// Token: 0x0600DABE RID: 55998 RVA: 0x00504296 File Offset: 0x00502496
			public override bool Contains(TValue item)
			{
				return this.myDictionary.Contains(this.key, item);
			}

			// Token: 0x04005C27 RID: 23591
			private MultiDictionaryBase<TKey, TValue> myDictionary;

			// Token: 0x04005C28 RID: 23592
			private TKey key;
		}

		// Token: 0x020016E6 RID: 5862
		[Serializable]
		private sealed class KeysCollection : ReadOnlyCollectionBase<TKey>
		{
			// Token: 0x0600DABF RID: 55999 RVA: 0x005042AA File Offset: 0x005024AA
			public KeysCollection(MultiDictionaryBase<TKey, TValue> myDictionary)
			{
				this.myDictionary = myDictionary;
			}

			// Token: 0x17000DBA RID: 3514
			// (get) Token: 0x0600DAC0 RID: 56000 RVA: 0x005042B9 File Offset: 0x005024B9
			public override int Count
			{
				get
				{
					return this.myDictionary.Count;
				}
			}

			// Token: 0x0600DAC1 RID: 56001 RVA: 0x005042C6 File Offset: 0x005024C6
			public override IEnumerator<TKey> GetEnumerator()
			{
				return this.myDictionary.EnumerateKeys();
			}

			// Token: 0x0600DAC2 RID: 56002 RVA: 0x005042D3 File Offset: 0x005024D3
			public override bool Contains(TKey key)
			{
				return this.myDictionary.ContainsKey(key);
			}

			// Token: 0x04005C29 RID: 23593
			private MultiDictionaryBase<TKey, TValue> myDictionary;
		}

		// Token: 0x020016E7 RID: 5863
		[Serializable]
		private sealed class ValuesCollection : ReadOnlyCollectionBase<TValue>
		{
			// Token: 0x0600DAC3 RID: 56003 RVA: 0x005042E1 File Offset: 0x005024E1
			public ValuesCollection(MultiDictionaryBase<TKey, TValue> myDictionary)
			{
				this.myDictionary = myDictionary;
			}

			// Token: 0x17000DBB RID: 3515
			// (get) Token: 0x0600DAC4 RID: 56004 RVA: 0x005042F0 File Offset: 0x005024F0
			public override int Count
			{
				get
				{
					return this.myDictionary.CountAllValues();
				}
			}

			// Token: 0x0600DAC5 RID: 56005 RVA: 0x005042FD File Offset: 0x005024FD
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

			// Token: 0x0600DAC6 RID: 56006 RVA: 0x0050430C File Offset: 0x0050250C
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

			// Token: 0x04005C2A RID: 23594
			private MultiDictionaryBase<TKey, TValue> myDictionary;
		}

		// Token: 0x020016E8 RID: 5864
		[Serializable]
		private sealed class EnumerableValuesCollection : ReadOnlyCollectionBase<ICollection<TValue>>
		{
			// Token: 0x0600DAC7 RID: 56007 RVA: 0x00504364 File Offset: 0x00502564
			public EnumerableValuesCollection(MultiDictionaryBase<TKey, TValue> myDictionary)
			{
				this.myDictionary = myDictionary;
			}

			// Token: 0x17000DBC RID: 3516
			// (get) Token: 0x0600DAC8 RID: 56008 RVA: 0x00504373 File Offset: 0x00502573
			public override int Count
			{
				get
				{
					return this.myDictionary.Count;
				}
			}

			// Token: 0x0600DAC9 RID: 56009 RVA: 0x00504380 File Offset: 0x00502580
			public override IEnumerator<ICollection<TValue>> GetEnumerator()
			{
				using (IEnumerator<TKey> enumKeys = this.myDictionary.EnumerateKeys())
				{
					while (enumKeys.MoveNext())
					{
						TKey tkey = enumKeys.Current;
						yield return new MultiDictionaryBase<TKey, TValue>.ValuesForKeyCollection(this.myDictionary, tkey);
					}
				}
				IEnumerator<TKey> enumKeys = null;
				yield break;
				yield break;
			}

			// Token: 0x0600DACA RID: 56010 RVA: 0x00504390 File Offset: 0x00502590
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

			// Token: 0x04005C2B RID: 23595
			private MultiDictionaryBase<TKey, TValue> myDictionary;
		}

		// Token: 0x020016E9 RID: 5865
		[Serializable]
		private sealed class KeyValuePairsCollection : ReadOnlyCollectionBase<KeyValuePair<TKey, TValue>>
		{
			// Token: 0x0600DACB RID: 56011 RVA: 0x0050449C File Offset: 0x0050269C
			public KeyValuePairsCollection(MultiDictionaryBase<TKey, TValue> myDictionary)
			{
				this.myDictionary = myDictionary;
			}

			// Token: 0x17000DBD RID: 3517
			// (get) Token: 0x0600DACC RID: 56012 RVA: 0x005044AB File Offset: 0x005026AB
			public override int Count
			{
				get
				{
					return this.myDictionary.CountAllValues();
				}
			}

			// Token: 0x0600DACD RID: 56013 RVA: 0x005044B8 File Offset: 0x005026B8
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

			// Token: 0x0600DACE RID: 56014 RVA: 0x005044C7 File Offset: 0x005026C7
			public override bool Contains(KeyValuePair<TKey, TValue> pair)
			{
				return this.myDictionary[pair.Key].Contains(pair.Value);
			}

			// Token: 0x04005C2C RID: 23596
			private MultiDictionaryBase<TKey, TValue> myDictionary;
		}
	}
}
