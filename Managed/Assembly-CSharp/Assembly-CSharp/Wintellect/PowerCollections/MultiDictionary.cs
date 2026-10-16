using System;
using System.Collections.Generic;

namespace Wintellect.PowerCollections
{
	// Token: 0x020002FA RID: 762
	[Serializable]
	public class MultiDictionary<TKey, TValue> : MultiDictionaryBase<TKey, TValue>, ICloneable
	{
		// Token: 0x06001B2C RID: 6956 RVA: 0x00088764 File Offset: 0x00086964
		public MultiDictionary(bool allowDuplicateValues)
			: this(allowDuplicateValues, EqualityComparer<TKey>.Default, EqualityComparer<TValue>.Default)
		{
		}

		// Token: 0x06001B2D RID: 6957 RVA: 0x00088777 File Offset: 0x00086977
		public MultiDictionary(bool allowDuplicateValues, IEqualityComparer<TKey> keyEqualityComparer)
			: this(allowDuplicateValues, keyEqualityComparer, EqualityComparer<TValue>.Default)
		{
		}

		// Token: 0x06001B2E RID: 6958 RVA: 0x00088788 File Offset: 0x00086988
		public MultiDictionary(bool allowDuplicateValues, IEqualityComparer<TKey> keyEqualityComparer, IEqualityComparer<TValue> valueEqualityComparer)
		{
			if (keyEqualityComparer == null)
			{
				throw new ArgumentNullException("keyEqualityComparer");
			}
			if (valueEqualityComparer == null)
			{
				throw new ArgumentNullException("valueEqualityComparer");
			}
			this.allowDuplicateValues = allowDuplicateValues;
			this.keyEqualityComparer = keyEqualityComparer;
			this.valueEqualityComparer = valueEqualityComparer;
			this.equalityComparer = new MultiDictionary<TKey, TValue>.KeyAndValuesEqualityComparer(keyEqualityComparer);
			this.hash = new Hash<MultiDictionary<TKey, TValue>.KeyAndValues>(this.equalityComparer);
		}

		// Token: 0x06001B2F RID: 6959 RVA: 0x000887EC File Offset: 0x000869EC
		private MultiDictionary(bool allowDuplicateValues, IEqualityComparer<TKey> keyEqualityComparer, IEqualityComparer<TValue> valueEqualityComparer, IEqualityComparer<MultiDictionary<TKey, TValue>.KeyAndValues> equalityComparer, Hash<MultiDictionary<TKey, TValue>.KeyAndValues> hash)
		{
			if (keyEqualityComparer == null)
			{
				throw new ArgumentNullException("keyEqualityComparer");
			}
			if (valueEqualityComparer == null)
			{
				throw new ArgumentNullException("valueEqualityComparer");
			}
			this.allowDuplicateValues = allowDuplicateValues;
			this.keyEqualityComparer = keyEqualityComparer;
			this.valueEqualityComparer = valueEqualityComparer;
			this.equalityComparer = equalityComparer;
			this.hash = hash;
		}

		// Token: 0x06001B30 RID: 6960 RVA: 0x00088840 File Offset: 0x00086A40
		public sealed override void Add(TKey key, TValue value)
		{
			MultiDictionary<TKey, TValue>.KeyAndValues keyAndValues = new MultiDictionary<TKey, TValue>.KeyAndValues(key);
			MultiDictionary<TKey, TValue>.KeyAndValues keyAndValues2;
			if (this.hash.Find(keyAndValues, false, out keyAndValues2))
			{
				int count = keyAndValues2.Count;
				if (!this.allowDuplicateValues)
				{
					int hashCode = Util.GetHashCode<TValue>(value, this.valueEqualityComparer);
					for (int i = 0; i < count; i++)
					{
						if (Util.GetHashCode<TValue>(keyAndValues2.Values[i], this.valueEqualityComparer) == hashCode && this.valueEqualityComparer.Equals(keyAndValues2.Values[i], value))
						{
							keyAndValues2.Values[i] = value;
							return;
						}
					}
				}
				if (count == keyAndValues2.Values.Length)
				{
					TValue[] array = new TValue[count * 2];
					Array.Copy(keyAndValues2.Values, array, count);
					keyAndValues2.Values = array;
				}
				keyAndValues2.Values[count] = value;
				keyAndValues2.Count = count + 1;
				this.hash.Find(keyAndValues2, true, out keyAndValues);
				return;
			}
			keyAndValues.Count = 1;
			keyAndValues.Values = new TValue[] { value };
			this.hash.Insert(keyAndValues, true, out keyAndValues2);
		}

		// Token: 0x06001B31 RID: 6961 RVA: 0x0008895C File Offset: 0x00086B5C
		public sealed override bool Remove(TKey key, TValue value)
		{
			MultiDictionary<TKey, TValue>.KeyAndValues keyAndValues = new MultiDictionary<TKey, TValue>.KeyAndValues(key);
			MultiDictionary<TKey, TValue>.KeyAndValues keyAndValues2;
			if (!this.hash.Find(keyAndValues, false, out keyAndValues2))
			{
				return false;
			}
			int count = keyAndValues2.Count;
			int hashCode = Util.GetHashCode<TValue>(value, this.valueEqualityComparer);
			int num = -1;
			for (int i = 0; i < count; i++)
			{
				if (Util.GetHashCode<TValue>(keyAndValues2.Values[i], this.valueEqualityComparer) == hashCode && this.valueEqualityComparer.Equals(keyAndValues2.Values[i], value))
				{
					num = i;
				}
			}
			if (count == 1)
			{
				this.hash.Delete(keyAndValues2, out keyAndValues);
				return true;
			}
			if (num >= 0)
			{
				if (num < count - 1)
				{
					Array.Copy(keyAndValues2.Values, num + 1, keyAndValues2.Values, num, count - num - 1);
				}
				keyAndValues2.Count = count - 1;
				this.hash.Find(keyAndValues2, true, out keyAndValues);
				return true;
			}
			return false;
		}

		// Token: 0x06001B32 RID: 6962 RVA: 0x00088A44 File Offset: 0x00086C44
		public sealed override bool Remove(TKey key)
		{
			MultiDictionary<TKey, TValue>.KeyAndValues keyAndValues;
			return this.hash.Delete(new MultiDictionary<TKey, TValue>.KeyAndValues(key), out keyAndValues);
		}

		// Token: 0x06001B33 RID: 6963 RVA: 0x00088A64 File Offset: 0x00086C64
		public sealed override void Clear()
		{
			this.hash.StopEnumerations();
			this.hash = new Hash<MultiDictionary<TKey, TValue>.KeyAndValues>(this.equalityComparer);
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06001B34 RID: 6964 RVA: 0x00088A82 File Offset: 0x00086C82
		public IEqualityComparer<TKey> KeyComparer
		{
			get
			{
				return this.keyEqualityComparer;
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06001B35 RID: 6965 RVA: 0x00088A8A File Offset: 0x00086C8A
		public IEqualityComparer<TValue> ValueComparer
		{
			get
			{
				return this.valueEqualityComparer;
			}
		}

		// Token: 0x06001B36 RID: 6966 RVA: 0x00088A92 File Offset: 0x00086C92
		protected sealed override bool EqualValues(TValue value1, TValue value2)
		{
			return this.valueEqualityComparer.Equals(value1, value2);
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06001B37 RID: 6967 RVA: 0x00088AA1 File Offset: 0x00086CA1
		public sealed override int Count
		{
			get
			{
				return this.hash.ElementCount;
			}
		}

		// Token: 0x06001B38 RID: 6968 RVA: 0x00088AB0 File Offset: 0x00086CB0
		public sealed override bool Contains(TKey key, TValue value)
		{
			MultiDictionary<TKey, TValue>.KeyAndValues keyAndValues = new MultiDictionary<TKey, TValue>.KeyAndValues(key);
			MultiDictionary<TKey, TValue>.KeyAndValues keyAndValues2;
			if (this.hash.Find(keyAndValues, false, out keyAndValues2))
			{
				int count = keyAndValues2.Count;
				int hashCode = Util.GetHashCode<TValue>(value, this.valueEqualityComparer);
				for (int i = 0; i < count; i++)
				{
					if (Util.GetHashCode<TValue>(keyAndValues2.Values[i], this.valueEqualityComparer) == hashCode && this.valueEqualityComparer.Equals(keyAndValues2.Values[i], value))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001B39 RID: 6969 RVA: 0x00088B34 File Offset: 0x00086D34
		public sealed override bool ContainsKey(TKey key)
		{
			MultiDictionary<TKey, TValue>.KeyAndValues keyAndValues = new MultiDictionary<TKey, TValue>.KeyAndValues(key);
			MultiDictionary<TKey, TValue>.KeyAndValues keyAndValues2;
			return this.hash.Find(keyAndValues, false, out keyAndValues2);
		}

		// Token: 0x06001B3A RID: 6970 RVA: 0x00088B58 File Offset: 0x00086D58
		protected sealed override IEnumerator<TKey> EnumerateKeys()
		{
			foreach (MultiDictionary<TKey, TValue>.KeyAndValues keyAndValues in this.hash)
			{
				yield return keyAndValues.Key;
			}
			IEnumerator<MultiDictionary<TKey, TValue>.KeyAndValues> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06001B3B RID: 6971 RVA: 0x00088B67 File Offset: 0x00086D67
		private IEnumerator<TValue> EnumerateValues(MultiDictionary<TKey, TValue>.KeyAndValues keyAndValues)
		{
			int count = keyAndValues.Count;
			int stamp = this.hash.GetEnumerationStamp();
			int num;
			for (int i = 0; i < count; i = num)
			{
				yield return keyAndValues.Values[i];
				this.hash.CheckEnumerationStamp(stamp);
				num = i + 1;
			}
			yield break;
		}

		// Token: 0x06001B3C RID: 6972 RVA: 0x00088B80 File Offset: 0x00086D80
		protected sealed override bool TryEnumerateValuesForKey(TKey key, out IEnumerator<TValue> values)
		{
			MultiDictionary<TKey, TValue>.KeyAndValues keyAndValues = new MultiDictionary<TKey, TValue>.KeyAndValues(key);
			MultiDictionary<TKey, TValue>.KeyAndValues keyAndValues2;
			if (this.hash.Find(keyAndValues, false, out keyAndValues2))
			{
				values = this.EnumerateValues(keyAndValues2);
				return true;
			}
			values = null;
			return false;
		}

		// Token: 0x06001B3D RID: 6973 RVA: 0x00088BB8 File Offset: 0x00086DB8
		protected sealed override int CountValues(TKey key)
		{
			MultiDictionary<TKey, TValue>.KeyAndValues keyAndValues = new MultiDictionary<TKey, TValue>.KeyAndValues(key);
			MultiDictionary<TKey, TValue>.KeyAndValues keyAndValues2;
			if (this.hash.Find(keyAndValues, false, out keyAndValues2))
			{
				return keyAndValues2.Count;
			}
			return 0;
		}

		// Token: 0x06001B3E RID: 6974 RVA: 0x00088BE6 File Offset: 0x00086DE6
		public MultiDictionary<TKey, TValue> Clone()
		{
			return new MultiDictionary<TKey, TValue>(this.allowDuplicateValues, this.keyEqualityComparer, this.valueEqualityComparer, this.equalityComparer, this.hash.Clone(new Converter<MultiDictionary<TKey, TValue>.KeyAndValues, MultiDictionary<TKey, TValue>.KeyAndValues>(MultiDictionary<TKey, TValue>.KeyAndValues.Copy)));
		}

		// Token: 0x06001B3F RID: 6975 RVA: 0x00088C1C File Offset: 0x00086E1C
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x06001B40 RID: 6976 RVA: 0x00088C24 File Offset: 0x00086E24
		private void NonCloneableType(Type t)
		{
			throw new InvalidOperationException(string.Format(Strings.TypeNotCloneable, t.FullName));
		}

		// Token: 0x06001B41 RID: 6977 RVA: 0x00088C3C File Offset: 0x00086E3C
		public MultiDictionary<TKey, TValue> CloneContents()
		{
			bool flag;
			if (!Util.IsCloneableType(typeof(TKey), out flag))
			{
				this.NonCloneableType(typeof(TKey));
			}
			bool flag2;
			if (!Util.IsCloneableType(typeof(TValue), out flag2))
			{
				this.NonCloneableType(typeof(TValue));
			}
			MultiDictionary<TKey, TValue> multiDictionary = new MultiDictionary<TKey, TValue>(this.allowDuplicateValues, this.keyEqualityComparer, this.valueEqualityComparer);
			foreach (MultiDictionary<TKey, TValue>.KeyAndValues keyAndValues in this.hash)
			{
				TKey tkey;
				if (flag)
				{
					tkey = keyAndValues.Key;
				}
				else if (keyAndValues.Key == null)
				{
					tkey = default(TKey);
				}
				else
				{
					tkey = (TKey)((object)((ICloneable)((object)keyAndValues.Key)).Clone());
				}
				TValue[] array = new TValue[keyAndValues.Count];
				if (flag2)
				{
					Array.Copy(keyAndValues.Values, array, keyAndValues.Count);
				}
				else
				{
					for (int i = 0; i < keyAndValues.Count; i++)
					{
						if (keyAndValues.Values[i] == null)
						{
							array[i] = default(TValue);
						}
						else
						{
							array[i] = (TValue)((object)((ICloneable)((object)keyAndValues.Values[i])).Clone());
						}
					}
				}
				multiDictionary.AddMany(tkey, array);
			}
			return multiDictionary;
		}

		// Token: 0x04000F0C RID: 3852
		private IEqualityComparer<TKey> keyEqualityComparer;

		// Token: 0x04000F0D RID: 3853
		private IEqualityComparer<TValue> valueEqualityComparer;

		// Token: 0x04000F0E RID: 3854
		private IEqualityComparer<MultiDictionary<TKey, TValue>.KeyAndValues> equalityComparer;

		// Token: 0x04000F0F RID: 3855
		private Hash<MultiDictionary<TKey, TValue>.KeyAndValues> hash;

		// Token: 0x04000F10 RID: 3856
		private bool allowDuplicateValues;

		// Token: 0x020016E1 RID: 5857
		[Serializable]
		private struct KeyAndValues
		{
			// Token: 0x0600DAA5 RID: 55973 RVA: 0x00503F4D File Offset: 0x0050214D
			public KeyAndValues(TKey key)
			{
				this.Key = key;
				this.Count = 0;
				this.Values = null;
			}

			// Token: 0x0600DAA6 RID: 55974 RVA: 0x00503F64 File Offset: 0x00502164
			public static MultiDictionary<TKey, TValue>.KeyAndValues Copy(MultiDictionary<TKey, TValue>.KeyAndValues x)
			{
				MultiDictionary<TKey, TValue>.KeyAndValues keyAndValues;
				keyAndValues.Key = x.Key;
				keyAndValues.Count = x.Count;
				if (x.Values != null)
				{
					keyAndValues.Values = (TValue[])x.Values.Clone();
				}
				else
				{
					keyAndValues.Values = null;
				}
				return keyAndValues;
			}

			// Token: 0x04005C18 RID: 23576
			public TKey Key;

			// Token: 0x04005C19 RID: 23577
			public int Count;

			// Token: 0x04005C1A RID: 23578
			public TValue[] Values;
		}

		// Token: 0x020016E2 RID: 5858
		[Serializable]
		private class KeyAndValuesEqualityComparer : IEqualityComparer<MultiDictionary<TKey, TValue>.KeyAndValues>
		{
			// Token: 0x0600DAA7 RID: 55975 RVA: 0x00503FB5 File Offset: 0x005021B5
			public KeyAndValuesEqualityComparer(IEqualityComparer<TKey> keyEqualityComparer)
			{
				this.keyEqualityComparer = keyEqualityComparer;
			}

			// Token: 0x0600DAA8 RID: 55976 RVA: 0x00503FC4 File Offset: 0x005021C4
			public bool Equals(MultiDictionary<TKey, TValue>.KeyAndValues x, MultiDictionary<TKey, TValue>.KeyAndValues y)
			{
				return this.keyEqualityComparer.Equals(x.Key, y.Key);
			}

			// Token: 0x0600DAA9 RID: 55977 RVA: 0x00503FDD File Offset: 0x005021DD
			public int GetHashCode(MultiDictionary<TKey, TValue>.KeyAndValues obj)
			{
				return Util.GetHashCode<TKey>(obj.Key, this.keyEqualityComparer);
			}

			// Token: 0x04005C1B RID: 23579
			private IEqualityComparer<TKey> keyEqualityComparer;
		}
	}
}
