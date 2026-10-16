using System;

// Token: 0x02000036 RID: 54
public static class CharCache
{
	// Token: 0x060001F1 RID: 497 RVA: 0x0000A3BC File Offset: 0x000085BC
	static CharCache()
	{
		for (int i = 0; i < CharCache.Length; i++)
		{
			CharCache.Values[i] = char.ToString((char)i);
		}
	}

	// Token: 0x04000106 RID: 262
	public static readonly int Length = 256;

	// Token: 0x04000107 RID: 263
	public static string[] Values = new string[CharCache.Length];
}
