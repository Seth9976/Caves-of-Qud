using System;
using System.Collections.Generic;

// Token: 0x0200017C RID: 380
[Serializable]
public class CleanStack<T>
{
	// Token: 0x06000E9A RID: 3738 RVA: 0x0005D3A8 File Offset: 0x0005B5A8
	public void Clear()
	{
		this.Items.Clear();
	}

	// Token: 0x17000162 RID: 354
	// (get) Token: 0x06000E9B RID: 3739 RVA: 0x0005D3B5 File Offset: 0x0005B5B5
	public int Count
	{
		get
		{
			return this.Items.Count;
		}
	}

	// Token: 0x06000E9C RID: 3740 RVA: 0x0005D3C4 File Offset: 0x0005B5C4
	public bool Contains(T Item)
	{
		if (Item == null)
		{
			int i = 0;
			int count = this.Items.Count;
			while (i < count)
			{
				if (this.Items[i] == null)
				{
					return true;
				}
				i++;
			}
		}
		else
		{
			int j = 0;
			int count2 = this.Items.Count;
			while (j < count2)
			{
				T t = this.Items[j];
				if (t != null && t.Equals(Item))
				{
					return true;
				}
				j++;
			}
		}
		return false;
	}

	// Token: 0x06000E9D RID: 3741 RVA: 0x0005D450 File Offset: 0x0005B650
	public int IndexOf(T Item)
	{
		if (Item == null)
		{
			int i = 0;
			int count = this.Items.Count;
			while (i < count)
			{
				if (this.Items[i] == null)
				{
					return i;
				}
				i++;
			}
		}
		else
		{
			int j = 0;
			int count2 = this.Items.Count;
			while (j < count2)
			{
				T t = this.Items[j];
				if (t != null && t.Equals(Item))
				{
					return j;
				}
				j++;
			}
		}
		return -1;
	}

	// Token: 0x06000E9E RID: 3742 RVA: 0x0005D4DC File Offset: 0x0005B6DC
	public T Peek()
	{
		if (this.Items.Count == 0)
		{
			return default(T);
		}
		return this.Items[this.Items.Count - 1];
	}

	// Token: 0x06000E9F RID: 3743 RVA: 0x0005D518 File Offset: 0x0005B718
	public void Push(T Item)
	{
		this.Items.Add(Item);
	}

	// Token: 0x06000EA0 RID: 3744 RVA: 0x0005D526 File Offset: 0x0005B726
	public T Pop()
	{
		T t = this.Items[this.Items.Count - 1];
		this.Items.RemoveAt(this.Items.Count - 1);
		return t;
	}

	// Token: 0x06000EA1 RID: 3745 RVA: 0x0005D558 File Offset: 0x0005B758
	public void Insert(int Index, T Item)
	{
		this.Items.Insert(Index, Item);
	}

	// Token: 0x06000EA2 RID: 3746 RVA: 0x0005D568 File Offset: 0x0005B768
	public bool InsertUnder(T Under, T Item)
	{
		int num = this.IndexOf(Under);
		if (num == -1)
		{
			this.Push(Item);
			return false;
		}
		this.Items.Insert(num, Item);
		return true;
	}

	// Token: 0x04000A17 RID: 2583
	public List<T> Items = new List<T>(64);
}
