using System;

// Token: 0x0200015A RID: 346
public static class Coach
{
	// Token: 0x06000DB3 RID: 3507 RVA: 0x00058C84 File Offset: 0x00056E84
	public static void StartSection(string Name, bool bTrackGarbage = false)
	{
	}

	// Token: 0x06000DB4 RID: 3508 RVA: 0x00058C86 File Offset: 0x00056E86
	public static void EndSection()
	{
	}

	// Token: 0x0400098B RID: 2443
	public static CoachSection Root;

	// Token: 0x0400098C RID: 2444
	public static CoachSection CurrentSection;

	// Token: 0x0400098D RID: 2445
	public static long StartMem;
}
