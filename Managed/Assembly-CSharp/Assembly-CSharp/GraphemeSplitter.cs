using System;

// Token: 0x02000073 RID: 115
public static class GraphemeSplitter
{
	// Token: 0x06000433 RID: 1075 RVA: 0x0001C4B8 File Offset: 0x0001A6B8
	public static ReadOnlySpan<char> GetNextPrintableSpan(ReadOnlySpan<char> In, ref int Index)
	{
		if (Index >= In.Length)
		{
			return default(ReadOnlySpan<char>);
		}
		int num = Index;
		Index = num + 1;
		return In.Slice(num, 1);
	}
}
