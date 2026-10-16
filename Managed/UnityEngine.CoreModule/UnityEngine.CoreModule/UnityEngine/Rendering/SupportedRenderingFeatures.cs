using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000560 RID: 1376
	public class SupportedRenderingFeatures
	{
		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x0600352F RID: 13615 RVA: 0x00068868 File Offset: 0x00066A68
		// (set) Token: 0x06003530 RID: 13616 RVA: 0x00068895 File Offset: 0x00066A95
		public static SupportedRenderingFeatures active
		{
			get
			{
				bool flag = SupportedRenderingFeatures.s_Active == null;
				if (flag)
				{
					SupportedRenderingFeatures.s_Active = new SupportedRenderingFeatures();
				}
				return SupportedRenderingFeatures.s_Active;
			}
			set
			{
				SupportedRenderingFeatures.s_Active = value;
			}
		}

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x06003531 RID: 13617 RVA: 0x0006889E File Offset: 0x00066A9E
		// (set) Token: 0x06003532 RID: 13618 RVA: 0x000688A6 File Offset: 0x00066AA6
		public SupportedRenderingFeatures.ReflectionProbeModes reflectionProbeModes { get; set; } = SupportedRenderingFeatures.ReflectionProbeModes.None;

		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x06003533 RID: 13619 RVA: 0x000688AF File Offset: 0x00066AAF
		// (set) Token: 0x06003534 RID: 13620 RVA: 0x000688B7 File Offset: 0x00066AB7
		public SupportedRenderingFeatures.LightmapMixedBakeModes defaultMixedLightingModes { get; set; } = SupportedRenderingFeatures.LightmapMixedBakeModes.None;

		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x06003535 RID: 13621 RVA: 0x000688C0 File Offset: 0x00066AC0
		// (set) Token: 0x06003536 RID: 13622 RVA: 0x000688C8 File Offset: 0x00066AC8
		public SupportedRenderingFeatures.LightmapMixedBakeModes mixedLightingModes { get; set; } = SupportedRenderingFeatures.LightmapMixedBakeModes.IndirectOnly | SupportedRenderingFeatures.LightmapMixedBakeModes.Subtractive | SupportedRenderingFeatures.LightmapMixedBakeModes.Shadowmask;

		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x06003537 RID: 13623 RVA: 0x000688D1 File Offset: 0x00066AD1
		// (set) Token: 0x06003538 RID: 13624 RVA: 0x000688D9 File Offset: 0x00066AD9
		public LightmapBakeType lightmapBakeTypes { get; set; } = LightmapBakeType.Realtime | LightmapBakeType.Baked | LightmapBakeType.Mixed;

		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x06003539 RID: 13625 RVA: 0x000688E2 File Offset: 0x00066AE2
		// (set) Token: 0x0600353A RID: 13626 RVA: 0x000688EA File Offset: 0x00066AEA
		public LightmapsMode lightmapsModes { get; set; } = LightmapsMode.CombinedDirectional;

		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x0600353B RID: 13627 RVA: 0x000688F3 File Offset: 0x00066AF3
		// (set) Token: 0x0600353C RID: 13628 RVA: 0x000688FB File Offset: 0x00066AFB
		[Obsolete("Bake with the Progressive Lightmapper. The backend that uses Enlighten to bake is obsolete.", true)]
		public bool enlightenLightmapper { get; set; } = false;

		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x0600353D RID: 13629 RVA: 0x00068904 File Offset: 0x00066B04
		// (set) Token: 0x0600353E RID: 13630 RVA: 0x0006890C File Offset: 0x00066B0C
		public bool enlighten { get; set; } = true;

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x0600353F RID: 13631 RVA: 0x00068915 File Offset: 0x00066B15
		// (set) Token: 0x06003540 RID: 13632 RVA: 0x0006891D File Offset: 0x00066B1D
		public bool skyOcclusion { get; set; } = false;

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x06003541 RID: 13633 RVA: 0x00068926 File Offset: 0x00066B26
		// (set) Token: 0x06003542 RID: 13634 RVA: 0x0006892E File Offset: 0x00066B2E
		public bool lightProbeProxyVolumes { get; set; } = true;

		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x06003543 RID: 13635 RVA: 0x00068937 File Offset: 0x00066B37
		// (set) Token: 0x06003544 RID: 13636 RVA: 0x0006893F File Offset: 0x00066B3F
		public bool motionVectors { get; set; } = true;

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x06003545 RID: 13637 RVA: 0x00068948 File Offset: 0x00066B48
		// (set) Token: 0x06003546 RID: 13638 RVA: 0x00068950 File Offset: 0x00066B50
		public bool receiveShadows { get; set; } = true;

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x06003547 RID: 13639 RVA: 0x00068959 File Offset: 0x00066B59
		// (set) Token: 0x06003548 RID: 13640 RVA: 0x00068961 File Offset: 0x00066B61
		public bool reflectionProbes { get; set; } = true;

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x06003549 RID: 13641 RVA: 0x0006896A File Offset: 0x00066B6A
		// (set) Token: 0x0600354A RID: 13642 RVA: 0x00068972 File Offset: 0x00066B72
		public bool reflectionProbesBlendDistance { get; set; } = true;

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x0600354B RID: 13643 RVA: 0x0006897B File Offset: 0x00066B7B
		// (set) Token: 0x0600354C RID: 13644 RVA: 0x00068983 File Offset: 0x00066B83
		public bool rendererPriority { get; set; } = false;

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x0600354D RID: 13645 RVA: 0x0006898C File Offset: 0x00066B8C
		// (set) Token: 0x0600354E RID: 13646 RVA: 0x00068994 File Offset: 0x00066B94
		public bool rendersUIOverlay { get; set; } = false;

		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x0600354F RID: 13647 RVA: 0x0006899D File Offset: 0x00066B9D
		// (set) Token: 0x06003550 RID: 13648 RVA: 0x000689A5 File Offset: 0x00066BA5
		public bool overridesEnvironmentLighting { get; set; } = false;

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x06003551 RID: 13649 RVA: 0x000689AE File Offset: 0x00066BAE
		// (set) Token: 0x06003552 RID: 13650 RVA: 0x000689B6 File Offset: 0x00066BB6
		public bool overridesFog { get; set; } = false;

		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x06003553 RID: 13651 RVA: 0x000689BF File Offset: 0x00066BBF
		// (set) Token: 0x06003554 RID: 13652 RVA: 0x000689C7 File Offset: 0x00066BC7
		public bool overridesRealtimeReflectionProbes { get; set; } = false;

		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x06003555 RID: 13653 RVA: 0x000689D0 File Offset: 0x00066BD0
		// (set) Token: 0x06003556 RID: 13654 RVA: 0x000689D8 File Offset: 0x00066BD8
		public bool overridesOtherLightingSettings { get; set; } = false;

		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x06003557 RID: 13655 RVA: 0x000689E1 File Offset: 0x00066BE1
		// (set) Token: 0x06003558 RID: 13656 RVA: 0x000689E9 File Offset: 0x00066BE9
		public bool editableMaterialRenderQueue { get; set; } = true;

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x06003559 RID: 13657 RVA: 0x000689F2 File Offset: 0x00066BF2
		// (set) Token: 0x0600355A RID: 13658 RVA: 0x000689FA File Offset: 0x00066BFA
		public bool overridesLODBias { get; set; } = false;

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x0600355B RID: 13659 RVA: 0x00068A03 File Offset: 0x00066C03
		// (set) Token: 0x0600355C RID: 13660 RVA: 0x00068A0B File Offset: 0x00066C0B
		public bool overridesMaximumLODLevel { get; set; } = false;

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x0600355D RID: 13661 RVA: 0x00068A14 File Offset: 0x00066C14
		// (set) Token: 0x0600355E RID: 13662 RVA: 0x00068A1C File Offset: 0x00066C1C
		public bool overridesEnableLODCrossFade { get; set; } = false;

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x0600355F RID: 13663 RVA: 0x00068A25 File Offset: 0x00066C25
		// (set) Token: 0x06003560 RID: 13664 RVA: 0x00068A2D File Offset: 0x00066C2D
		public bool rendererProbes { get; set; } = true;

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x06003561 RID: 13665 RVA: 0x00068A36 File Offset: 0x00066C36
		// (set) Token: 0x06003562 RID: 13666 RVA: 0x00068A3E File Offset: 0x00066C3E
		public bool particleSystemInstancing { get; set; } = true;

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x06003563 RID: 13667 RVA: 0x00068A48 File Offset: 0x00066C48
		// (set) Token: 0x06003564 RID: 13668 RVA: 0x00068A60 File Offset: 0x00066C60
		[Obsolete("autoAmbientProbeBaking is obsolete. To enable or disable baking of the ambient probe, use ambientProbeBaking instead. (UnityUpgradable) -> ambientProbeBaking", false)]
		public bool autoAmbientProbeBaking
		{
			get
			{
				return this.ambientProbeBaking;
			}
			set
			{
				this.ambientProbeBaking = value;
			}
		}

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x06003565 RID: 13669 RVA: 0x00068A6C File Offset: 0x00066C6C
		// (set) Token: 0x06003566 RID: 13670 RVA: 0x00068A84 File Offset: 0x00066C84
		[Obsolete("autoDefaultReflectionProbeBaking is obsolete. To enable or disable baking of the default reflection probe, use defaultReflectionProbeBaking instead. (UnityUpgradable) -> defaultReflectionProbeBaking", false)]
		public bool autoDefaultReflectionProbeBaking
		{
			get
			{
				return this.defaultReflectionProbeBaking;
			}
			set
			{
				this.defaultReflectionProbeBaking = value;
			}
		}

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x06003567 RID: 13671 RVA: 0x00068A8F File Offset: 0x00066C8F
		// (set) Token: 0x06003568 RID: 13672 RVA: 0x00068A97 File Offset: 0x00066C97
		public bool ambientProbeBaking { get; set; } = true;

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x06003569 RID: 13673 RVA: 0x00068AA0 File Offset: 0x00066CA0
		// (set) Token: 0x0600356A RID: 13674 RVA: 0x00068AA8 File Offset: 0x00066CA8
		public bool defaultReflectionProbeBaking { get; set; } = true;

		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x0600356B RID: 13675 RVA: 0x00068AB1 File Offset: 0x00066CB1
		// (set) Token: 0x0600356C RID: 13676 RVA: 0x00068AB9 File Offset: 0x00066CB9
		public bool overridesShadowmask { get; set; } = false;

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x0600356D RID: 13677 RVA: 0x00068AC2 File Offset: 0x00066CC2
		// (set) Token: 0x0600356E RID: 13678 RVA: 0x00068ACA File Offset: 0x00066CCA
		public bool overridesLightProbeSystem { get; set; } = false;

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x0600356F RID: 13679 RVA: 0x00068AD3 File Offset: 0x00066CD3
		// (set) Token: 0x06003570 RID: 13680 RVA: 0x00068ADB File Offset: 0x00066CDB
		public bool supportsHDR { get; set; } = false;

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x06003571 RID: 13681 RVA: 0x00068AE4 File Offset: 0x00066CE4
		// (set) Token: 0x06003572 RID: 13682 RVA: 0x00068AEC File Offset: 0x00066CEC
		public bool supportsClouds { get; set; } = false;

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x06003573 RID: 13683 RVA: 0x00068AF5 File Offset: 0x00066CF5
		// (set) Token: 0x06003574 RID: 13684 RVA: 0x00068AFD File Offset: 0x00066CFD
		public string overridesLightProbeSystemWarningMessage { get; set; } = "Light Probe Groups are unavailable as Probe Volumes have been enabled by the current Render Pipeline.";

		// Token: 0x06003575 RID: 13685 RVA: 0x00068B08 File Offset: 0x00066D08
		internal unsafe static MixedLightingMode FallbackMixedLightingMode()
		{
			MixedLightingMode mixedLightingMode;
			SupportedRenderingFeatures.FallbackMixedLightingModeByRef(new IntPtr((void*)(&mixedLightingMode)));
			return mixedLightingMode;
		}

		// Token: 0x06003576 RID: 13686 RVA: 0x00068B2C File Offset: 0x00066D2C
		[RequiredByNativeCode]
		internal unsafe static void FallbackMixedLightingModeByRef(IntPtr fallbackModePtr)
		{
			MixedLightingMode* ptr = (MixedLightingMode*)(void*)fallbackModePtr;
			bool flag = SupportedRenderingFeatures.active.defaultMixedLightingModes != SupportedRenderingFeatures.LightmapMixedBakeModes.None && (SupportedRenderingFeatures.active.mixedLightingModes & SupportedRenderingFeatures.active.defaultMixedLightingModes) == SupportedRenderingFeatures.active.defaultMixedLightingModes;
			if (flag)
			{
				SupportedRenderingFeatures.LightmapMixedBakeModes defaultMixedLightingModes = SupportedRenderingFeatures.active.defaultMixedLightingModes;
				SupportedRenderingFeatures.LightmapMixedBakeModes lightmapMixedBakeModes = defaultMixedLightingModes;
				if (lightmapMixedBakeModes != SupportedRenderingFeatures.LightmapMixedBakeModes.Subtractive)
				{
					if (lightmapMixedBakeModes != SupportedRenderingFeatures.LightmapMixedBakeModes.Shadowmask)
					{
						*ptr = MixedLightingMode.IndirectOnly;
					}
					else
					{
						*ptr = MixedLightingMode.Shadowmask;
					}
				}
				else
				{
					*ptr = MixedLightingMode.Subtractive;
				}
			}
			else
			{
				bool flag2 = SupportedRenderingFeatures.IsMixedLightingModeSupported(MixedLightingMode.Shadowmask);
				if (flag2)
				{
					*ptr = MixedLightingMode.Shadowmask;
				}
				else
				{
					bool flag3 = SupportedRenderingFeatures.IsMixedLightingModeSupported(MixedLightingMode.Subtractive);
					if (flag3)
					{
						*ptr = MixedLightingMode.Subtractive;
					}
					else
					{
						*ptr = MixedLightingMode.IndirectOnly;
					}
				}
			}
		}

		// Token: 0x06003577 RID: 13687 RVA: 0x00068BC8 File Offset: 0x00066DC8
		internal unsafe static bool IsMixedLightingModeSupported(MixedLightingMode mixedMode)
		{
			bool flag;
			SupportedRenderingFeatures.IsMixedLightingModeSupportedByRef(mixedMode, new IntPtr((void*)(&flag)));
			return flag;
		}

		// Token: 0x06003578 RID: 13688 RVA: 0x00068BEC File Offset: 0x00066DEC
		[RequiredByNativeCode]
		internal unsafe static void IsMixedLightingModeSupportedByRef(MixedLightingMode mixedMode, IntPtr isSupportedPtr)
		{
			bool* ptr = (bool*)(void*)isSupportedPtr;
			bool flag = !SupportedRenderingFeatures.IsLightmapBakeTypeSupported(LightmapBakeType.Mixed);
			if (flag)
			{
				*ptr = false;
			}
			else
			{
				*ptr = (mixedMode == MixedLightingMode.IndirectOnly && (SupportedRenderingFeatures.active.mixedLightingModes & SupportedRenderingFeatures.LightmapMixedBakeModes.IndirectOnly) == SupportedRenderingFeatures.LightmapMixedBakeModes.IndirectOnly) || (mixedMode == MixedLightingMode.Subtractive && (SupportedRenderingFeatures.active.mixedLightingModes & SupportedRenderingFeatures.LightmapMixedBakeModes.Subtractive) == SupportedRenderingFeatures.LightmapMixedBakeModes.Subtractive) || (mixedMode == MixedLightingMode.Shadowmask && (SupportedRenderingFeatures.active.mixedLightingModes & SupportedRenderingFeatures.LightmapMixedBakeModes.Shadowmask) == SupportedRenderingFeatures.LightmapMixedBakeModes.Shadowmask);
			}
		}

		// Token: 0x06003579 RID: 13689 RVA: 0x00068C54 File Offset: 0x00066E54
		internal unsafe static bool IsLightmapBakeTypeSupported(LightmapBakeType bakeType)
		{
			bool flag;
			SupportedRenderingFeatures.IsLightmapBakeTypeSupportedByRef(bakeType, new IntPtr((void*)(&flag)));
			return flag;
		}

		// Token: 0x0600357A RID: 13690 RVA: 0x00068C78 File Offset: 0x00066E78
		[RequiredByNativeCode]
		internal unsafe static void IsLightmapBakeTypeSupportedByRef(LightmapBakeType bakeType, IntPtr isSupportedPtr)
		{
			bool* ptr = (bool*)(void*)isSupportedPtr;
			bool flag = bakeType == LightmapBakeType.Mixed;
			if (flag)
			{
				bool flag2 = SupportedRenderingFeatures.IsLightmapBakeTypeSupported(LightmapBakeType.Baked);
				bool flag3 = !flag2 || SupportedRenderingFeatures.active.mixedLightingModes == SupportedRenderingFeatures.LightmapMixedBakeModes.None;
				if (flag3)
				{
					*ptr = false;
					return;
				}
			}
			*ptr = (SupportedRenderingFeatures.active.lightmapBakeTypes & bakeType) == bakeType;
			bool flag4 = bakeType == LightmapBakeType.Realtime && !SupportedRenderingFeatures.active.enlighten;
			if (flag4)
			{
				*ptr = false;
			}
		}

		// Token: 0x0600357B RID: 13691 RVA: 0x00068CEC File Offset: 0x00066EEC
		internal unsafe static bool IsLightmapsModeSupported(LightmapsMode mode)
		{
			bool flag;
			SupportedRenderingFeatures.IsLightmapsModeSupportedByRef(mode, new IntPtr((void*)(&flag)));
			return flag;
		}

		// Token: 0x0600357C RID: 13692 RVA: 0x00068D10 File Offset: 0x00066F10
		[RequiredByNativeCode]
		internal unsafe static void IsLightmapsModeSupportedByRef(LightmapsMode mode, IntPtr isSupportedPtr)
		{
			bool* ptr = (bool*)(void*)isSupportedPtr;
			*ptr = (SupportedRenderingFeatures.active.lightmapsModes & mode) == mode;
		}

		// Token: 0x0600357D RID: 13693 RVA: 0x00068D38 File Offset: 0x00066F38
		internal unsafe static bool IsLightmapperSupported(int lightmapper)
		{
			bool flag;
			SupportedRenderingFeatures.IsLightmapperSupportedByRef(lightmapper, new IntPtr((void*)(&flag)));
			return flag;
		}

		// Token: 0x0600357E RID: 13694 RVA: 0x00068D5C File Offset: 0x00066F5C
		[RequiredByNativeCode]
		internal unsafe static void IsLightmapperSupportedByRef(int lightmapper, IntPtr isSupportedPtr)
		{
			bool* ptr = (bool*)(void*)isSupportedPtr;
			*ptr = lightmapper != 0;
		}

		// Token: 0x0600357F RID: 13695 RVA: 0x00068D78 File Offset: 0x00066F78
		[RequiredByNativeCode]
		internal unsafe static void IsUIOverlayRenderedBySRP(IntPtr isSupportedPtr)
		{
			bool* ptr = (bool*)(void*)isSupportedPtr;
			*ptr = SupportedRenderingFeatures.active.rendersUIOverlay;
		}

		// Token: 0x06003580 RID: 13696 RVA: 0x00068D9C File Offset: 0x00066F9C
		[RequiredByNativeCode]
		internal unsafe static void IsAmbientProbeBakingSupported(IntPtr isSupportedPtr)
		{
			bool* ptr = (bool*)(void*)isSupportedPtr;
			*ptr = SupportedRenderingFeatures.active.ambientProbeBaking;
		}

		// Token: 0x06003581 RID: 13697 RVA: 0x00068DC0 File Offset: 0x00066FC0
		[RequiredByNativeCode]
		internal unsafe static void IsDefaultReflectionProbeBakingSupported(IntPtr isSupportedPtr)
		{
			bool* ptr = (bool*)(void*)isSupportedPtr;
			*ptr = SupportedRenderingFeatures.active.defaultReflectionProbeBaking;
		}

		// Token: 0x06003582 RID: 13698 RVA: 0x00068DE4 File Offset: 0x00066FE4
		[RequiredByNativeCode]
		internal unsafe static void OverridesLightProbeSystem(IntPtr overridesPtr)
		{
			bool* ptr = (bool*)(void*)overridesPtr;
			*ptr = SupportedRenderingFeatures.active.overridesLightProbeSystem;
		}

		// Token: 0x06003583 RID: 13699 RVA: 0x00068E08 File Offset: 0x00067008
		internal unsafe static int FallbackLightmapper()
		{
			int num;
			SupportedRenderingFeatures.FallbackLightmapperByRef(new IntPtr((void*)(&num)));
			return num;
		}

		// Token: 0x06003584 RID: 13700 RVA: 0x00068E2C File Offset: 0x0006702C
		[RequiredByNativeCode]
		internal unsafe static void FallbackLightmapperByRef(IntPtr lightmapperPtr)
		{
			int* ptr = (int*)(void*)lightmapperPtr;
			*ptr = 1;
		}

		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x06003585 RID: 13701 RVA: 0x00068E44 File Offset: 0x00067044
		// (set) Token: 0x06003586 RID: 13702 RVA: 0x00002925 File Offset: 0x00000B25
		[Obsolete("terrainDetailUnsupported is deprecated.")]
		public bool terrainDetailUnsupported
		{
			get
			{
				return true;
			}
			set
			{
			}
		}

		// Token: 0x040011E3 RID: 4579
		private static SupportedRenderingFeatures s_Active = new SupportedRenderingFeatures();

		// Token: 0x02000561 RID: 1377
		[Flags]
		public enum ReflectionProbeModes
		{
			// Token: 0x04001205 RID: 4613
			None = 0,
			// Token: 0x04001206 RID: 4614
			Rotation = 1
		}

		// Token: 0x02000562 RID: 1378
		[Flags]
		public enum LightmapMixedBakeModes
		{
			// Token: 0x04001208 RID: 4616
			None = 0,
			// Token: 0x04001209 RID: 4617
			IndirectOnly = 1,
			// Token: 0x0400120A RID: 4618
			Subtractive = 2,
			// Token: 0x0400120B RID: 4619
			Shadowmask = 4
		}
	}
}
