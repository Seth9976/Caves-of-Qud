using System;
using System.Collections.Generic;

// Token: 0x0200017B RID: 379
[Serializable]
public class CleanQueue<T>
{
	// Token: 0x06000E93 RID: 3731 RVA: 0x0005D28B File Offset: 0x0005B48B
	public void Clear()
	{
		this.Items.Clear();
	}

	// Token: 0x17000161 RID: 353
	// (get) Token: 0x06000E94 RID: 3732 RVA: 0x0005D298 File Offset: 0x0005B498
	public int Count
	{
		get
		{
			return this.Items.Count;
		}
	}

	// Token: 0x06000E95 RID: 3733 RVA: 0x0005D2A8 File Offset: 0x0005B4A8
	public bool Contains(T Item)
	{
		if (Item == null)
		{
			for (int i = 0; i < this.Items.Count; i++)
			{
				if (this.Items[i] == null)
				{
					return true;
				}
			}
		}
		else
		{
			for (int j = 0; j < this.Items.Count; j++)
			{
				if (this.Items[j] != null)
				{
					T t = this.Items[j];
					if (t.Equals(Item))
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x06000E96 RID: 3734 RVA: 0x0005D338 File Offset: 0x0005B538
	public T Peek()
	{
		if (this.Items.Count == 0)
		{
			return default(T);
		}
		return this.Items[0];
	}

	// Token: 0x06000E97 RID: 3735 RVA: 0x0005D368 File Offset: 0x0005B568
	public void Enqueue(T Item)
	{
		this.Items.Add(Item);
	}

	// Token: 0x06000E98 RID: 3736 RVA: 0x0005D376 File Offset: 0x0005B576
	public T Dequeue()
	{
		T t = this.Items[0];
		this.Items.RemoveAt(0);
		return t;
	}

	// Token: 0x04000A16 RID: 2582
	public List<T> Items = new List<T>(1000);
}
