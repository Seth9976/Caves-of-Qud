using System;

namespace UnityEngine.Android
{
	// Token: 0x02000015 RID: 21
	public enum ProcessImportance
	{
		// Token: 0x0400002F RID: 47
		Foreground = 100,
		// Token: 0x04000030 RID: 48
		ForeGroundService = 125,
		// Token: 0x04000031 RID: 49
		Visible = 200,
		// Token: 0x04000032 RID: 50
		Perceptible = 230,
		// Token: 0x04000033 RID: 51
		TopSleeping = 325,
		// Token: 0x04000034 RID: 52
		CantSaveState = 350,
		// Token: 0x04000035 RID: 53
		Service = 300,
		// Token: 0x04000036 RID: 54
		Cached = 400,
		// Token: 0x04000037 RID: 55
		Gone = 1000
	}
}
