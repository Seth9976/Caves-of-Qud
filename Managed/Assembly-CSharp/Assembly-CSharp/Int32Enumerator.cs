using System;
using System.Globalization;

// Token: 0x02000032 RID: 50
public ref struct Int32Enumerator
{
	// Token: 0x1700003A RID: 58
	// (get) Token: 0x060001E1 RID: 481 RVA: 0x0000A07A File Offset: 0x0000827A
	public int Current
	{
		get
		{
			return this.Value;
		}
	}

	// Token: 0x060001E2 RID: 482 RVA: 0x0000A082 File Offset: 0x00008282
	public Int32Enumerator(ReadOnlySpan<char> Text)
	{
		this.Text = Text;
		this.Value = 0;
		this.Index = 0;
		this.Length = Text.Length;
	}

	// Token: 0x060001E3 RID: 483 RVA: 0x0000A0A6 File Offset: 0x000082A6
	public Int32Enumerator GetEnumerator()
	{
		return this;
	}

	// Token: 0x060001E4 RID: 484 RVA: 0x0000A0B0 File Offset: 0x000082B0
	public unsafe bool MoveNext()
	{
		int i = this.Index;
		int length = this.Length;
		if (i >= length)
		{
			return false;
		}
		ReadOnlySpan<char> text = this.Text;
		int num = -1;
		while (i < length)
		{
			if (char.IsDigit((char)(*text[i])))
			{
				if (num == -1)
				{
					num = i;
				}
			}
			else if (num != -1)
			{
				break;
			}
			i++;
		}
		if (num == -1)
		{
			return false;
		}
		this.Value = int.Parse(text.Slice(num, i - num), NumberStyles.Integer, null);
		this.Index = i;
		return true;
	}

	// Token: 0x040000F3 RID: 243
	private ReadOnlySpan<char> Text;

	// Token: 0x040000F4 RID: 244
	private int Value;

	// Token: 0x040000F5 RID: 245
	private int Index;

	// Token: 0x040000F6 RID: 246
	private int Length;
}
