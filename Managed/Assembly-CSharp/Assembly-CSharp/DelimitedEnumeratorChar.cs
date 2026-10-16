using System;

// Token: 0x02000034 RID: 52
public ref struct DelimitedEnumeratorChar
{
	// Token: 0x1700003C RID: 60
	// (get) Token: 0x060001E9 RID: 489 RVA: 0x0000A197 File Offset: 0x00008397
	public ReadOnlySpan<char> Current
	{
		get
		{
			return this.Value.Slice(this.Start, this.End - this.Start);
		}
	}

	// Token: 0x060001EA RID: 490 RVA: 0x0000A1B7 File Offset: 0x000083B7
	public DelimitedEnumeratorChar(ReadOnlySpan<char> Value, char Separator)
	{
		this.Value = Value;
		this.Separator = Separator;
		this.End = -1;
		this.Start = 0;
		this.Length = Value.Length;
	}

	// Token: 0x060001EB RID: 491 RVA: 0x0000A1E2 File Offset: 0x000083E2
	public DelimitedEnumeratorChar GetEnumerator()
	{
		return this;
	}

	// Token: 0x060001EC RID: 492 RVA: 0x0000A1EC File Offset: 0x000083EC
	public unsafe bool MoveNext()
	{
		this.Start = this.End + 1;
		if (this.Start >= this.Length)
		{
			return false;
		}
		this.End = -1;
		ReadOnlySpan<char> value = this.Value;
		char separator = this.Separator;
		for (int i = this.Start; i < this.Length; i++)
		{
			if (*value[i] == (ushort)separator)
			{
				this.End = i;
				break;
			}
		}
		if (this.End == -1)
		{
			this.End = this.Length;
		}
		return true;
	}

	// Token: 0x040000FB RID: 251
	private ReadOnlySpan<char> Value;

	// Token: 0x040000FC RID: 252
	private char Separator;

	// Token: 0x040000FD RID: 253
	private int Start;

	// Token: 0x040000FE RID: 254
	private int End;

	// Token: 0x040000FF RID: 255
	private int Length;
}
