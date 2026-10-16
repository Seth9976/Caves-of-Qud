using System;
using System.ComponentModel;

namespace UnityEngine
{
	// Token: 0x02000111 RID: 273
	public enum RuntimePlatform
	{
		// Token: 0x04000345 RID: 837
		OSXEditor,
		// Token: 0x04000346 RID: 838
		OSXPlayer,
		// Token: 0x04000347 RID: 839
		WindowsPlayer,
		// Token: 0x04000348 RID: 840
		[Obsolete("WebPlayer export is no longer supported in Unity 5.4+.", true)]
		OSXWebPlayer,
		// Token: 0x04000349 RID: 841
		[Obsolete("Dashboard widget on Mac OS X export is no longer supported in Unity 5.4+.", true)]
		OSXDashboardPlayer,
		// Token: 0x0400034A RID: 842
		[Obsolete("WebPlayer export is no longer supported in Unity 5.4+.", true)]
		WindowsWebPlayer,
		// Token: 0x0400034B RID: 843
		WindowsEditor = 7,
		// Token: 0x0400034C RID: 844
		IPhonePlayer,
		// Token: 0x0400034D RID: 845
		[Obsolete("Xbox360 export is no longer supported in Unity 5.5+.")]
		XBOX360 = 10,
		// Token: 0x0400034E RID: 846
		[Obsolete("PS3 export is no longer supported in Unity >=5.5.")]
		PS3 = 9,
		// Token: 0x0400034F RID: 847
		Android = 11,
		// Token: 0x04000350 RID: 848
		[Obsolete("NaCl export is no longer supported in Unity 5.0+.")]
		NaCl,
		// Token: 0x04000351 RID: 849
		[Obsolete("FlashPlayer export is no longer supported in Unity 5.0+.")]
		FlashPlayer = 15,
		// Token: 0x04000352 RID: 850
		LinuxPlayer = 13,
		// Token: 0x04000353 RID: 851
		LinuxEditor = 16,
		// Token: 0x04000354 RID: 852
		WebGLPlayer,
		// Token: 0x04000355 RID: 853
		[Obsolete("Use WSAPlayerX86 instead")]
		MetroPlayerX86,
		// Token: 0x04000356 RID: 854
		WSAPlayerX86 = 18,
		// Token: 0x04000357 RID: 855
		[Obsolete("Use WSAPlayerX64 instead")]
		MetroPlayerX64,
		// Token: 0x04000358 RID: 856
		WSAPlayerX64 = 19,
		// Token: 0x04000359 RID: 857
		[Obsolete("Use WSAPlayerARM instead")]
		MetroPlayerARM,
		// Token: 0x0400035A RID: 858
		WSAPlayerARM = 20,
		// Token: 0x0400035B RID: 859
		[Obsolete("Windows Phone 8 was removed in 5.3")]
		WP8Player,
		// Token: 0x0400035C RID: 860
		[Obsolete("BlackBerryPlayer export is no longer supported in Unity 5.4+.")]
		BlackBerryPlayer,
		// Token: 0x0400035D RID: 861
		[Obsolete("TizenPlayer export is no longer supported in Unity 2017.3+.")]
		TizenPlayer,
		// Token: 0x0400035E RID: 862
		[Obsolete("PSP2 is no longer supported as of Unity 2018.3")]
		PSP2,
		// Token: 0x0400035F RID: 863
		PS4,
		// Token: 0x04000360 RID: 864
		[Obsolete("PSM export is no longer supported in Unity >= 5.3")]
		PSM,
		// Token: 0x04000361 RID: 865
		XboxOne,
		// Token: 0x04000362 RID: 866
		[Obsolete("SamsungTVPlayer export is no longer supported in Unity 2017.3+.")]
		SamsungTVPlayer,
		// Token: 0x04000363 RID: 867
		[Obsolete("Wii U is no longer supported in Unity 2018.1+.")]
		WiiU = 30,
		// Token: 0x04000364 RID: 868
		tvOS,
		// Token: 0x04000365 RID: 869
		Switch,
		// Token: 0x04000366 RID: 870
		[Obsolete("Lumin is no longer supported in Unity 2022.2")]
		Lumin,
		// Token: 0x04000367 RID: 871
		[Obsolete("Stadia is no longer supported in Unity 2023.1")]
		Stadia,
		// Token: 0x04000368 RID: 872
		[Obsolete("CloudRendering is deprecated, please use LinuxHeadlessSimulation (UnityUpgradable) -> LinuxHeadlessSimulation", false)]
		CloudRendering = -1,
		// Token: 0x04000369 RID: 873
		LinuxHeadlessSimulation = 35,
		// Token: 0x0400036A RID: 874
		[Obsolete("GameCoreScarlett is deprecated, please use GameCoreXboxSeries (UnityUpgradable) -> GameCoreXboxSeries", false)]
		GameCoreScarlett = -1,
		// Token: 0x0400036B RID: 875
		GameCoreXboxSeries = 36,
		// Token: 0x0400036C RID: 876
		GameCoreXboxOne,
		// Token: 0x0400036D RID: 877
		PS5,
		// Token: 0x0400036E RID: 878
		EmbeddedLinuxArm64,
		// Token: 0x0400036F RID: 879
		[Obsolete("32-bit embedded platforms are no longer supported")]
		EmbeddedLinuxArm32,
		// Token: 0x04000370 RID: 880
		EmbeddedLinuxX64,
		// Token: 0x04000371 RID: 881
		[Obsolete("32-bit embedded platforms are no longer supported")]
		EmbeddedLinuxX86,
		// Token: 0x04000372 RID: 882
		LinuxServer,
		// Token: 0x04000373 RID: 883
		WindowsServer,
		// Token: 0x04000374 RID: 884
		OSXServer,
		// Token: 0x04000375 RID: 885
		[Obsolete("32-bit embedded platforms are no longer supported")]
		QNXArm32,
		// Token: 0x04000376 RID: 886
		QNXArm64,
		// Token: 0x04000377 RID: 887
		QNXX64,
		// Token: 0x04000378 RID: 888
		[Obsolete("32-bit embedded platforms are no longer supported")]
		QNXX86,
		// Token: 0x04000379 RID: 889
		VisionOS,
		// Token: 0x0400037A RID: 890
		Switch2,
		// Token: 0x0400037B RID: 891
		[Obsolete]
		[EditorBrowsable(EditorBrowsableState.Never)]
		KeplerArm64,
		// Token: 0x0400037C RID: 892
		[Obsolete]
		[EditorBrowsable(EditorBrowsableState.Never)]
		KeplerX64
	}
}
