using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace PlayFab.Json
{
	// Token: 0x02000038 RID: 56
	[GeneratedCode("simple-json", "1.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class JsonObject : IDictionary<string, object>, ICollection<KeyValuePair<string, object>>, IEnumerable<KeyValuePair<string, object>>, IEnumerable
	{
		// Token: 0x06000329 RID: 809 RVA: 0x00010576 File Offset: 0x0000E776
		public JsonObject()
		{
			this._members = new Dictionary<string, object>(16);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0001058B File Offset: 0x0000E78B
		public JsonObject(IEqualityComparer<string> comparer)
		{
			this._members = new Dictionary<string, object>(comparer);
		}

		// Token: 0x17000024 RID: 36
		public object this[int index]
		{
			get
			{
				return JsonObject.GetAtIndex(this._members, index);
			}
		}

		// Token: 0x0600032C RID: 812 RVA: 0x000105B0 File Offset: 0x0000E7B0
		internal static object GetAtIndex(IDictionary<string, object> obj, int index)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (index >= obj.Count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			int num = 0;
			foreach (KeyValuePair<string, object> keyValuePair in obj)
			{
				if (num++ == index)
				{
					return keyValuePair.Value;
				}
			}
			return null;
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0001062C File Offset: 0x0000E82C
		public void Add(string key, object value)
		{
			this._members.Add(key, value);
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0001063B File Offset: 0x0000E83B
		public bool ContainsKey(string key)
		{
			return this._members.ContainsKey(key);
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600032F RID: 815 RVA: 0x00010649 File Offset: 0x0000E849
		public ICollection<string> Keys
		{
			get
			{
				return this._members.Keys;
			}
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00010656 File Offset: 0x0000E856
		public bool Remove(string key)
		{
			return this._members.Remove(key);
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00010664 File Offset: 0x0000E864
		public bool TryGetValue(string key, out object value)
		{
			return this._members.TryGetValue(key, out value);
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000332 RID: 818 RVA: 0x00010673 File Offset: 0x0000E873
		public ICollection<object> Values
		{
			get
			{
				return this._members.Values;
			}
		}

		// Token: 0x17000027 RID: 39
		public object this[string key]
		{
			get
			{
				return this._members[key];
			}
			set
			{
				this._members[key] = value;
			}
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0001069D File Offset: 0x0000E89D
		public void Add(KeyValuePair<string, object> item)
		{
			this._members.Add(item.Key, item.Value);
		}

		// Token: 0x06000336 RID: 822 RVA: 0x000106B8 File Offset: 0x0000E8B8
		public void Clear()
		{
			this._members.Clear();
		}

		// Token: 0x06000337 RID: 823 RVA: 0x000106C8 File Offset: 0x0000E8C8
		public bool Contains(KeyValuePair<string, object> item)
		{
			object obj;
			return this._members.TryGetValue(item.Key, out obj) && obj == item.Value;
		}

		// Token: 0x06000338 RID: 824 RVA: 0x000106F8 File Offset: 0x0000E8F8
		public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			int num = this.Count;
			foreach (KeyValuePair<string, object> keyValuePair in this._members)
			{
				array[arrayIndex++] = keyValuePair;
				if (--num <= 0)
				{
					break;
				}
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000339 RID: 825 RVA: 0x00010770 File Offset: 0x0000E970
		public int Count
		{
			get
			{
				return this._members.Count;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600033A RID: 826 RVA: 0x0001077D File Offset: 0x0000E97D
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600033B RID: 827 RVA: 0x00010780 File Offset: 0x0000E980
		public bool Remove(KeyValuePair<string, object> item)
		{
			return this._members.Remove(item.Key);
		}

		// Token: 0x0600033C RID: 828 RVA: 0x00010794 File Offset: 0x0000E994
		public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
		{
			return this._members.GetEnumerator();
		}

		// Token: 0x0600033D RID: 829 RVA: 0x000107A6 File Offset: 0x0000E9A6
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._members.GetEnumerator();
		}

		// Token: 0x0600033E RID: 830 RVA: 0x000107B8 File Offset: 0x0000E9B8
		public override string ToString()
		{
			return PlayFabSimpleJson.SerializeObject(this._members, null);
		}

		// Token: 0x040002DA RID: 730
		private const int DICTIONARY_DEFAULT_SIZE = 16;

		// Token: 0x040002DB RID: 731
		private readonly Dictionary<string, object> _members;
	}
}
