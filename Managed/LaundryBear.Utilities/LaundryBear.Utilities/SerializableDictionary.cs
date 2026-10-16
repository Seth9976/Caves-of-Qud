using System;
using System.Collections.Generic;
using UnityEngine;

namespace LaundryBear
{
	// Token: 0x02000010 RID: 16
	[Serializable]
	public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
	{
		// Token: 0x06000057 RID: 87 RVA: 0x00002BD0 File Offset: 0x00000DD0
		public SerializableDictionary(IEqualityComparer<TKey> keyComparer)
			: base(keyComparer)
		{
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002BE4 File Offset: 0x00000DE4
		public void OnAfterDeserialize()
		{
			for (int i = 0; i < this.m_entries.Count; i++)
			{
				SerializableDictionary<TKey, TValue>.Entry entry = this.m_entries[i];
				base[entry.Key] = entry.Value;
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002C28 File Offset: 0x00000E28
		public void OnBeforeSerialize()
		{
			this.m_entries.Clear();
			foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
			{
				this.m_entries.Add(new SerializableDictionary<TKey, TValue>.Entry
				{
					m_key = keyValuePair.Key,
					m_value = keyValuePair.Value
				});
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002CA4 File Offset: 0x00000EA4
		public bool TryFindValue(SerializableDictionary<TKey, TValue>.Predicate predicate, out TValue result)
		{
			foreach (TValue tvalue in base.Values)
			{
				if (predicate(tvalue))
				{
					result = tvalue;
					return true;
				}
			}
			result = default(TValue);
			return false;
		}

		// Token: 0x0400001F RID: 31
		[SerializeField]
		private List<SerializableDictionary<TKey, TValue>.Entry> m_entries = new List<SerializableDictionary<TKey, TValue>.Entry>();

		// Token: 0x02000025 RID: 37
		[Serializable]
		public class Entry
		{
			// Token: 0x17000028 RID: 40
			// (get) Token: 0x060000BA RID: 186 RVA: 0x000037F9 File Offset: 0x000019F9
			public TKey Key
			{
				get
				{
					return this.m_key;
				}
			}

			// Token: 0x17000029 RID: 41
			// (get) Token: 0x060000BB RID: 187 RVA: 0x00003801 File Offset: 0x00001A01
			public TValue Value
			{
				get
				{
					return this.m_value;
				}
			}

			// Token: 0x04000061 RID: 97
			[SerializeField]
			internal TKey m_key;

			// Token: 0x04000062 RID: 98
			[SerializeField]
			internal TValue m_value;
		}

		// Token: 0x02000026 RID: 38
		// (Invoke) Token: 0x060000BE RID: 190
		public delegate bool Predicate(TValue value);
	}
}
