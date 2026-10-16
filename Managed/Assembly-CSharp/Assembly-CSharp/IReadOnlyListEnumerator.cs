using System;
using System.Collections.Generic;

// Token: 0x02000033 RID: 51
public ref struct IReadOnlyListEnumerator<T>
{
	// Token: 0x1700003B RID: 59
	// (get) Token: 0x060001E5 RID: 485 RVA: 0x0000A126 File Offset: 0x00008326
	public T Current
	{
		get
		{
			return this.Value;
		}
	}

	// Token: 0x060001E6 RID: 486 RVA: 0x0000A12E File Offset: 0x0000832E
	public IReadOnlyListEnumerator(IReadOnlyList<T> List, int Start, int Length)
	{
		this.List = List;
		this.Index = Start;
		this.Length = Start + Length;
		this.Value = default(T);
	}

	// Token: 0x060001E7 RID: 487 RVA: 0x0000A153 File Offset: 0x00008353
	public IReadOnlyListEnumerator<T> GetEnumerator()
	{
		return this;
	}

	// Token: 0x060001E8 RID: 488 RVA: 0x0000A15C File Offset: 0x0000835C
	public bool MoveNext()
	{
		int index = this.Index;
		if (index >= this.Length)
		{
			return false;
		}
		this.Value = this.List[index];
		this.Index = index + 1;
		return true;
	}

	// Token: 0x040000F7 RID: 247
	private IReadOnlyList<T> List;

	// Token: 0x040000F8 RID: 248
	private T Value;

	// Token: 0x040000F9 RID: 249
	private int Index;

	// Token: 0x040000FA RID: 250
	private int Length;
}
