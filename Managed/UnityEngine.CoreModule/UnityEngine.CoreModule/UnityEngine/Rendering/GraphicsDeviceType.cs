using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x0200049F RID: 1183
	[UsedByNativeCode]
	public enum GraphicsDeviceType
	{
		// Token: 0x04000DA3 RID: 3491
		[Obsolete("OpenGL2 is no longer supported in Unity 5.5+")]
		OpenGL2,
		// Token: 0x04000DA4 RID: 3492
		[Obsolete("Direct3D 9 is no longer supported in Unity 2017.2+")]
		Direct3D9,
		// Token: 0x04000DA5 RID: 3493
		Direct3D11,
		// Token: 0x04000DA6 RID: 3494
		[Obsolete("PS3 is no longer supported in Unity 5.5+")]
		PlayStation3,
		// Token: 0x04000DA7 RID: 3495
		Null,
		// Token: 0x04000DA8 RID: 3496
		[Obsolete("Xbox360 is no longer supported in Unity 5.5+")]
		Xbox360 = 6,
		// Token: 0x04000DA9 RID: 3497
		[Obsolete("OpenGL ES 2.0 is no longer supported in Unity 2023.1")]
		OpenGLES2 = 8,
		// Token: 0x04000DAA RID: 3498
		OpenGLES3 = 11,
		// Token: 0x04000DAB RID: 3499
		[Obsolete("PVita is no longer supported as of Unity 2018")]
		PlayStationVita,
		// Token: 0x04000DAC RID: 3500
		PlayStation4,
		// Token: 0x04000DAD RID: 3501
		XboxOne,
		// Token: 0x04000DAE RID: 3502
		[Obsolete("PlayStationMobile is no longer supported in Unity 5.3+")]
		PlayStationMobile,
		// Token: 0x04000DAF RID: 3503
		Metal,
		// Token: 0x04000DB0 RID: 3504
		OpenGLCore,
		// Token: 0x04000DB1 RID: 3505
		Direct3D12,
		// Token: 0x04000DB2 RID: 3506
		[Obsolete("Nintendo 3DS support is unavailable since 2018.1")]
		N3DS,
		// Token: 0x04000DB3 RID: 3507
		Vulkan = 21,
		// Token: 0x04000DB4 RID: 3508
		Switch,
		// Token: 0x04000DB5 RID: 3509
		XboxOneD3D12,
		// Token: 0x04000DB6 RID: 3510
		GameCoreXboxOne,
		// Token: 0x04000DB7 RID: 3511
		[Obsolete("GameCoreScarlett is deprecated, please use GameCoreXboxSeries (UnityUpgradable) -> GameCoreXboxSeries", false)]
		GameCoreScarlett = -1,
		// Token: 0x04000DB8 RID: 3512
		GameCoreXboxSeries = 25,
		// Token: 0x04000DB9 RID: 3513
		PlayStation5,
		// Token: 0x04000DBA RID: 3514
		PlayStation5NGGC,
		// Token: 0x04000DBB RID: 3515
		WebGPU,
		// Token: 0x04000DBC RID: 3516
		Switch2
	}
}
