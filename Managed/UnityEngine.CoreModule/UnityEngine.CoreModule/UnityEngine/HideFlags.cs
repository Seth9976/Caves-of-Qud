using System;

namespace UnityEngine
{
	// Token: 0x020002B9 RID: 697
	[Flags]
	public enum HideFlags
	{
		// Token: 0x040009E8 RID: 2536
		None = 0,
		// Token: 0x040009E9 RID: 2537
		HideInHierarchy = 1,
		// Token: 0x040009EA RID: 2538
		HideInInspector = 2,
		// Token: 0x040009EB RID: 2539
		DontSaveInEditor = 4,
		// Token: 0x040009EC RID: 2540
		NotEditable = 8,
		// Token: 0x040009ED RID: 2541
		DontSaveInBuild = 16,
		// Token: 0x040009EE RID: 2542
		DontUnloadUnusedAsset = 32,
		// Token: 0x040009EF RID: 2543
		DontSave = 52,
		// Token: 0x040009F0 RID: 2544
		HideAndDontSave = 61
	}
}
