using System;
using System.Collections.Generic;

// Token: 0x02000179 RID: 377
[Serializable]
public class CleanDictionary<TKey, TValue>
{
	// Token: 0x06000E88 RID: 3720 RVA: 0x0005CEEC File Offset: 0x0005B0EC
	public void Clear()
	{
		if (this.Keys.Count > 0)
		{
			this.Keys.Clear();
		}
		if (this.Values.Count > 0)
		{
			this.Values.Clear();
		}
	}

	// Token: 0x1700015F RID: 351
	// (get) Token: 0x06000E89 RID: 3721 RVA: 0x0005CF20 File Offset: 0x0005B120
	public int Count
	{
		get
		{
			return this.Keys.Count;
		}
	}

	// Token: 0x06000E8A RID: 3722 RVA: 0x0005CF30 File Offset: 0x0005B130
	public void Add(TKey K, TValue V)
	{
		if (this.LocateValue(K) == -1)
		{
			this.Keys.Add(K);
			this.Values.Add(V);
			return;
		}
		throw new ArgumentException("An element with the same key " + K.ToString() + " already exists in the dictionary.");
	}

	// Token: 0x06000E8B RID: 3723 RVA: 0x0005CF84 File Offset: 0x0005B184
	public bool ContainsKey(TKey Item)
	{
		if (Item == null)
		{
			for (int i = 0; i < this.Keys.Count; i++)
			{
				if (this.Keys[i] == null)
				{
					return true;
				}
			}
		}
		else
		{
			for (int j = 0; j < this.Keys.Count; j++)
			{
				if (this.Keys[j] != null)
				{
					TKey tkey = this.Keys[j];
					if (tkey.Equals(Item))
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x06000E8C RID: 3724 RVA: 0x0005D014 File Offset: 0x0005B214
	public bool ContainsValue(TValue Item)
	{
		if (Item == null)
		{
			for (int i = 0; i < this.Keys.Count; i++)
			{
				if (this.Keys[i] == null)
				{
					return true;
				}
			}
		}
		else
		{
			for (int j = 0; j < this.Keys.Count; j++)
			{
				if (this.Keys[j] != null)
				{
					TKey tkey = this.Keys[j];
					if (tkey.Equals(Item))
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x06000E8D RID: 3725 RVA: 0x0005D0A4 File Offset: 0x0005B2A4
	public bool TryGetValue(TKey Key, out TValue Value)
	{
		int num = this.LocateValue(Key);
		if (num == -1)
		{
			Value = default(TValue);
			return false;
		}
		Value = this.Values[num];
		return true;
	}

	// Token: 0x06000E8E RID: 3726 RVA: 0x0005D0DC File Offset: 0x0005B2DC
	public int LocateValue(TKey Key)
	{
		if (Key == null)
		{
			for (int i = 0; i < this.Keys.Count; i++)
			{
				if (this.Keys[i] == null)
				{
					return i;
				}
			}
		}
		else
		{
			for (int j = 0; j < this.Keys.Count; j++)
			{
				if (this.Keys[j] != null)
				{
					TKey tkey = this.Keys[j];
					if (tkey.Equals(Key))
					{
						return j;
					}
				}
			}
		}
		return -1;
	}

	// Token: 0x17000160 RID: 352
	public TValue this[TKey Key]
	{
		get
		{
			int num = this.LocateValue(Key);
			if (num == -1)
			{
				throw new KeyNotFoundException("Couldn't find " + Key.ToString());
			}
			return this.Values[num];
		}
		set
		{
			int num = this.LocateValue(Key);
			if (num == -1)
			{
				throw new KeyNotFoundException("Couldn't find " + Key.ToString());
			}
			this.Values[num] = value;
		}
	}

	// Token: 0x04000A14 RID: 2580
	public List<TKey> Keys = new List<TKey>();

	// Token: 0x04000A15 RID: 2581
	public List<TValue> Values = new List<TValue>();
}
