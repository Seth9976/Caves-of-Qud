using System;

// Token: 0x02000035 RID: 53
public ref struct DelimitedEnumeratorString
{
	// Token: 0x1700003D RID: 61
	// (get) Token: 0x060001ED RID: 493 RVA: 0x0000A26D File Offset: 0x0000846D
	public ReadOnlySpan<char> Current
	{
		get
		{
			return this.Value.Slice(this.Start, this.End - this.Start);
		}
	}

	// Token: 0x060001EE RID: 494 RVA: 0x0000A28D File Offset: 0x0000848D
	public DelimitedEnumeratorString(ReadOnlySpan<char> Value, string Separator)
	{
		this.Value = Value;
		this.Separator = Separator;
		this.Start = 0;
		this.Length = Value.Length;
		this.SeparatorLength = Separator.Length;
		this.End = -this.SeparatorLength;
	}

	// Token: 0x060001EF RID: 495 RVA: 0x0000A2CA File Offset: 0x000084CA
	public DelimitedEnumeratorString GetEnumerator()
	{
		return this;
	}

	// Token: 0x060001F0 RID: 496 RVA: 0x0000A2D4 File Offset: 0x000084D4
	public unsafe bool MoveNext()
	{
		this.Start = this.End + this.SeparatorLength;
		if (this.Start >= this.Length)
		{
			return false;
		}
		this.End = -1;
		ReadOnlySpan<char> value = this.Value;
		char c = this.Separator[0];
		for (int i = this.Start; i < this.Length; i++)
		{
			if (*value[i] == (ushort)c)
			{
				int num = i + this.SeparatorLength - 1;
				if (i == num)
				{
					this.End = i;
					break;
				}
				if (num < this.Length)
				{
					bool flag = true;
					for (int j = i + 1; j <= num; j++)
					{
						if (*value[j] != (ushort)this.Separator[j - i])
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						this.End = i;
						break;
					}
				}
			}
		}
		if (this.End == -1)
		{
			this.End = this.Length;
		}
		return true;
	}

	// Token: 0x04000100 RID: 256
	private ReadOnlySpan<char> Value;

	// Token: 0x04000101 RID: 257
	private string Separator;

	// Token: 0x04000102 RID: 258
	private int Start;

	// Token: 0x04000103 RID: 259
	private int End;

	// Token: 0x04000104 RID: 260
	private int Length;

	// Token: 0x04000105 RID: 261
	private int SeparatorLength;
}
