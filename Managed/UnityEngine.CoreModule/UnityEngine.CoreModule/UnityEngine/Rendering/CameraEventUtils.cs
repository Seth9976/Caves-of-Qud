using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000497 RID: 1175
	internal static class CameraEventUtils
	{
		// Token: 0x06002D82 RID: 11650 RVA: 0x000563E8 File Offset: 0x000545E8
		public static bool IsValid(CameraEvent value)
		{
			return value >= CameraEvent.BeforeDepthTexture && value <= CameraEvent.AfterHaloAndLensFlares;
		}

		// Token: 0x04000D50 RID: 3408
		private const CameraEvent k_MinimumValue = CameraEvent.BeforeDepthTexture;

		// Token: 0x04000D51 RID: 3409
		private const CameraEvent k_MaximumValue = CameraEvent.AfterHaloAndLensFlares;
	}
}
