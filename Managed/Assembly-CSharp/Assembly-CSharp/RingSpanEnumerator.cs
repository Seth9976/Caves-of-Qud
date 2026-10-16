using System;

// Token: 0x02000031 RID: 49
public ref struct RingSpanEnumerator<T>
{
	// Token: 0x17000039 RID: 57
	// (get) Token: 0x060001DD RID: 477 RVA: 0x00009F84 File Offset: 0x00008184
	public T Current
	{
		get
		{
			return this.Element;
		}
	}

	// Token: 0x060001DE RID: 478 RVA: 0x00009F8C File Offset: 0x0000818C
	public RingSpanEnumerator(ReadOnlySpan<T> Span, int Start, bool IncludeSelf = false, bool Invert = false)
	{
		int length = Span.Length;
		if (Invert)
		{
			Start -= length / 2;
			if (Start < 0)
			{
				Start += length;
			}
		}
		this.Start = Start;
		this.Span = Span;
		this.Delta = (IncludeSelf ? 0 : (-1));
		this.Length = length;
		this.Element = default(T);
	}

	// Token: 0x060001DF RID: 479 RVA: 0x00009FE3 File Offset: 0x000081E3
	public RingSpanEnumerator<T> GetEnumerator()
	{
		return this;
	}

	// Token: 0x060001E0 RID: 480 RVA: 0x00009FEC File Offset: 0x000081EC
	public unsafe bool MoveNext()
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
			this.Element = *this.Span[(num < 0) ? (num + length) : num];
			this.Delta = -delta;
		}
		else
		{
			if (delta + delta >= length)
			{
				return false;
			}
			this.Element = *this.Span[(num >= length) ? (num - length) : num];
			this.Delta = -(delta + 1);
		}
		return true;
	}

	// Token: 0x040000EE RID: 238
	private int Start;

	// Token: 0x040000EF RID: 239
	private int Delta;

	// Token: 0x040000F0 RID: 240
	private int Length;

	// Token: 0x040000F1 RID: 241
	private ReadOnlySpan<T> Span;

	// Token: 0x040000F2 RID: 242
	private T Element;
}
