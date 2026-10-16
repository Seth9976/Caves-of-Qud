using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020004FD RID: 1277
	[Flags]
	public enum BatchDrawCommandFlags
	{
		// Token: 0x04000F83 RID: 3971
		None = 0,
		// Token: 0x04000F84 RID: 3972
		FlipWinding = 1,
		// Token: 0x04000F85 RID: 3973
		HasMotion = 2,
		// Token: 0x04000F86 RID: 3974
		IsLightMapped = 4,
		// Token: 0x04000F87 RID: 3975
		HasSortingPosition = 8,
		// Token: 0x04000F88 RID: 3976
		LODCrossFadeKeyword = 16,
		// Token: 0x04000F89 RID: 3977
		LODCrossFadeValuePacked = 32,
		// Token: 0x04000F8A RID: 3978
		LODCrossFade = 48,
		// Token: 0x04000F8B RID: 3979
		UseLegacyLightmapsKeyword = 64
	}
}
