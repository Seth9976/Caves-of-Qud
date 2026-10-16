using System;
using System.Collections.Generic;

// Token: 0x02000030 RID: 48
public ref struct RingEnumerator<T>
{
	// Token: 0x17000038 RID: 56
	// (get) Token: 0x060001D9 RID: 473 RVA: 0x00009E96 File Offset: 0x00008096
	public T Current
	{
		get
		{
			return this.Element;
		}
	}

	// Token: 0x060001DA RID: 474 RVA: 0x00009EA0 File Offset: 0x000080A0
	public RingEnumerator(IReadOnlyList<T> List, int Start, bool IncludeSelf = false, bool Invert = false)
	{
		int count = List.Count;
		if (Invert)
		{
			Start -= count / 2;
			if (Start < 0)
			{
				Start += count;
			}
		}
		this.Start = Start;
		this.List = List;
		this.Delta = (IncludeSelf ? 0 : (-1));
		this.Length = count;
		this.Element = default(T);
	}

	// Token: 0x060001DB RID: 475 RVA: 0x00009EF6 File Offset: 0x000080F6
	public RingEnumerator<T> GetEnumerator()
	{
		return this;
	}

	// Token: 0x060001DC RID: 476 RVA: 0x00009F00 File Offset: 0x00008100
	public bool MoveNext()
	{
		int delta = this.Delta;
		int num = this.Start + delta;
		int length = this.Length;
		if (delta < 0)
		{
			if (delta + delta < -length)
			{
				return false;
			}
			this.Element = this.List[(num < 0) ? (num + length) : num];
			this.Delta = -delta;
		}
		else
		{
			if (delta + delta >= length)
			{
				return false;
			}
			this.Element = this.List[(num >= length) ? (num - length) : num];
			this.Delta = -(delta + 1);
		}
		return true;
	}

	// Token: 0x040000E9 RID: 233
	private int Start;

	// Token: 0x040000EA RID: 234
	private int Delta;

	// Token: 0x040000EB RID: 235
	private int Length;

	// Token: 0x040000EC RID: 236
	private IReadOnlyList<T> List;

	// Token: 0x040000ED RID: 237
	private T Element;
}
