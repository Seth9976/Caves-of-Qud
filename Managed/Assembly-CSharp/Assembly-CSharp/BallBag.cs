using System;
using System.Collections;
using System.Collections.Generic;
using XRL.Rules;

// Token: 0x0200017E RID: 382
public class BallBag<T> : IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable
{
	// Token: 0x17000163 RID: 355
	// (get) Token: 0x06000EAE RID: 3758 RVA: 0x0005D809 File Offset: 0x0005BA09
	public int Count
	{
		get
		{
			return this.Items.Count;
		}
	}

	// Token: 0x17000164 RID: 356
	public T this[int Index]
	{
		get
		{
			return this.Items[Index];
		}
	}

	// Token: 0x17000165 RID: 357
	public int this[T Item]
	{
		get
		{
			int num = this.Items.IndexOf(Item);
			if (num == -1)
			{
				throw new KeyNotFoundException();
			}
			if (num > 0)
			{
				return this.TotalWeights[num] - this.TotalWeights[num - 1];
			}
			return this.TotalWeights[0];
		}
		set
		{
			this.Add(Item, value);
		}
	}

	// Token: 0x06000EB2 RID: 3762 RVA: 0x0005D87E File Offset: 0x0005BA7E
	public BallBag(int Capacity = 0)
	{
		this.Random = Stat.Rnd;
		this.Items = new List<T>(Capacity);
		this.TotalWeights = new List<int>(Capacity);
	}

	// Token: 0x06000EB3 RID: 3763 RVA: 0x0005D8A9 File Offset: 0x0005BAA9
	public BallBag(Random Random, int Capacity = 0)
	{
		this.Random = Random;
		this.Items = new List<T>(Capacity);
		this.TotalWeights = new List<int>(Capacity);
	}

	// Token: 0x06000EB4 RID: 3764 RVA: 0x0005D8D0 File Offset: 0x0005BAD0
	public void Add(T Item, int Weight)
	{
		if (Weight <= 0)
		{
			return;
		}
		this.Items.Add(Item);
		this.TotalWeight += Weight;
		this.TotalWeights.Add(this.TotalWeight);
	}

	// Token: 0x06000EB5 RID: 3765 RVA: 0x0005D904 File Offset: 0x0005BB04
	public void AddRange(IEnumerable<T> Items, Func<T, int> Weight)
	{
		foreach (T t in Items)
		{
			this.Add(t, Weight(t));
		}
	}

	// Token: 0x06000EB6 RID: 3766 RVA: 0x0005D954 File Offset: 0x0005BB54
	public bool Remove(T Item)
	{
		int num = this.Items.IndexOf(Item);
		if (num < 0)
		{
			return false;
		}
		this.RemoveAt(num);
		return true;
	}

	// Token: 0x06000EB7 RID: 3767 RVA: 0x0005D980 File Offset: 0x0005BB80
	public T RemoveAt(int Index)
	{
		int weight = this.GetWeight(Index);
		T t = this.Items[Index];
		this.Items.RemoveAt(Index);
		this.TotalWeights.RemoveAt(Index);
		for (int i = Index; i < this.Items.Count; i++)
		{
			List<int> totalWeights = this.TotalWeights;
			int num = i;
			totalWeights[num] -= weight;
		}
		this.TotalWeight -= weight;
		return t;
	}

	// Token: 0x06000EB8 RID: 3768 RVA: 0x0005D9FB File Offset: 0x0005BBFB
	public int GetWeight(int Index)
	{
		if (Index <= 0)
		{
			return this.TotalWeights[Index];
		}
		return this.TotalWeights[Index] - this.TotalWeights[Index - 1];
	}

	// Token: 0x06000EB9 RID: 3769 RVA: 0x0005DA2C File Offset: 0x0005BC2C
	private int Roll()
	{
		int num = this.Random.Next(0, this.TotalWeight + 1);
		int num2 = 0;
		int i = this.Items.Count - 1;
		while (i > num2)
		{
			int num3 = (num2 + i) / 2;
			if (this.TotalWeights[num3] < num)
			{
				num2 = num3 + 1;
			}
			else
			{
				if (num3 <= 0 || this.TotalWeights[num3 - 1] <= num)
				{
					return num3;
				}
				i = num3 - 1;
			}
		}
		return (num2 + i) / 2;
	}

	// Token: 0x06000EBA RID: 3770 RVA: 0x0005DAA1 File Offset: 0x0005BCA1
	public void Clear()
	{
		this.Items.Clear();
		this.TotalWeights.Clear();
		this.TotalWeight = 0;
	}

	// Token: 0x06000EBB RID: 3771 RVA: 0x0005DAC0 File Offset: 0x0005BCC0
	public T PeekOne()
	{
		if (this.Items.Count != 0)
		{
			return this.Items[this.Roll()];
		}
		return default(T);
	}

	// Token: 0x06000EBC RID: 3772 RVA: 0x0005DAF8 File Offset: 0x0005BCF8
	public List<T> Peek(int Amount)
	{
		List<T> list = new List<T>(Amount);
		for (int i = 0; i < Amount; i++)
		{
			list.Add(this.PeekOne());
		}
		return list;
	}

	// Token: 0x06000EBD RID: 3773 RVA: 0x0005DB28 File Offset: 0x0005BD28
	public T PluckOne()
	{
		int num = this.Roll();
		T t = this.Items[num];
		int i = num;
		while (i < this.Items.Count)
		{
			List<int> totalWeights = this.TotalWeights;
			int num2 = i;
			int num3 = totalWeights[num2];
			totalWeights[num2] = num3 - 1;
			int num4 = num3;
			if (i > 0)
			{
				num4 -= this.TotalWeights[i - 1];
			}
			if (num4 == 0)
			{
				this.Items.RemoveAt(i);
				this.TotalWeights.RemoveAt(i);
			}
			else
			{
				i++;
			}
		}
		this.TotalWeight--;
		return t;
	}

	// Token: 0x06000EBE RID: 3774 RVA: 0x0005DBC4 File Offset: 0x0005BDC4
	public List<T> Pluck(int Amount)
	{
		List<T> list = new List<T>(Amount);
		for (int i = 0; i < Amount; i++)
		{
			list.Add(this.PluckOne());
		}
		return list;
	}

	// Token: 0x06000EBF RID: 3775 RVA: 0x0005DBF1 File Offset: 0x0005BDF1
	public T PickOne()
	{
		return this.RemoveAt(this.Roll());
	}

	// Token: 0x06000EC0 RID: 3776 RVA: 0x0005DBFF File Offset: 0x0005BDFF
	public List<T>.Enumerator GetEnumerator()
	{
		return this.Items.GetEnumerator();
	}

	// Token: 0x06000EC1 RID: 3777 RVA: 0x0005DC0C File Offset: 0x0005BE0C
	IEnumerator<T> IEnumerable<T>.GetEnumerator()
	{
		return this.Items.GetEnumerator();
	}

	// Token: 0x06000EC2 RID: 3778 RVA: 0x0005DC1E File Offset: 0x0005BE1E
	IEnumerator IEnumerable.GetEnumerator()
	{
		return this.Items.GetEnumerator();
	}

	// Token: 0x04000A18 RID: 2584
	public List<T> Items;

	// Token: 0x04000A19 RID: 2585
	public List<int> TotalWeights;

	// Token: 0x04000A1A RID: 2586
	public int TotalWeight;

	// Token: 0x04000A1B RID: 2587
	public Random Random;
}
