using System;

namespace LaundryBear.PlatformServices
{
	// Token: 0x02000025 RID: 37
	[Flags]
	public enum Platform
	{
		// Token: 0x0400003A RID: 58
		None = 0,
		// Token: 0x0400003B RID: 59
		Windows = 1,
		// Token: 0x0400003C RID: 60
		MacOS = 2,
		// Token: 0x0400003D RID: 61
		Switch = 4,
		// Token: 0x0400003E RID: 62
		GameCoreXboxSeries = 8,
		// Token: 0x0400003F RID: 63
		GameCoreXboxOne = 16,
		// Token: 0x04000040 RID: 64
		GameCoreWindows = 32,
		// Token: 0x04000041 RID: 65
		PS4 = 64,
		// Token: 0x04000042 RID: 66
		PS5 = 128,
		// Token: 0x04000043 RID: 67
		Android = 256,
		// Token: 0x04000044 RID: 68
		iOS = 512,
		// Token: 0x04000045 RID: 69
		Desktop = 3,
		// Token: 0x04000046 RID: 70
		GameCoreConsole = 24,
		// Token: 0x04000047 RID: 71
		GameCore = 56,
		// Token: 0x04000048 RID: 72
		PlayStation = 192,
		// Token: 0x04000049 RID: 73
		Console = 252,
		// Token: 0x0400004A RID: 74
		Mobile = 768
	}
}
