using System;

// Token: 0x02000072 RID: 114
public static class UITextSkinZAllocPlatformTestsTempExtensions
{
	// Token: 0x06000431 RID: 1073 RVA: 0x0001C45E File Offset: 0x0001A65E
	public static bool Contains(this ReadOnlySpan<char> Haystack, ReadOnlySpan<char> Needle)
	{
		return Haystack.IndexOf(Needle, StringComparison.Ordinal) >= 0;
	}

	// Token: 0x06000432 RID: 1074 RVA: 0x0001C470 File Offset: 0x0001A670
	public unsafe static int IndexOfAnyStartFrom(this ReadOnlySpan<char> builder, int Start, ReadOnlySpan<char> C)
	{
		for (int i = Start; i < builder.Length; i++)
		{
			for (int j = 0; j < C.Length; j++)
			{
				if (*builder[i] == *C[j])
				{
					return i;
				}
			}
		}
		return -1;
	}
}
