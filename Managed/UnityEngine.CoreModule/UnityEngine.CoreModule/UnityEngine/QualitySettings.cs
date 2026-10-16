using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200017C RID: 380
	[NativeHeader("Runtime/Misc/PlayerSettings.h")]
	[StaticAccessor("GetQualitySettings()", StaticAccessorType.Dot)]
	[NativeHeader("Runtime/Graphics/QualitySettings.h")]
	public sealed class QualitySettings : Object
	{
		// Token: 0x14000019 RID: 25
		// (add) Token: 0x06000DDA RID: 3546 RVA: 0x00019ABC File Offset: 0x00017CBC
		// (remove) Token: 0x06000DDB RID: 3547 RVA: 0x00019AF0 File Offset: 0x00017CF0
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<int, int> activeQualityLevelChanged;

		// Token: 0x06000DDC RID: 3548 RVA: 0x00019B23 File Offset: 0x00017D23
		[RequiredByNativeCode]
		internal static void OnActiveQualityLevelChanged(int previousQualityLevel, int currentQualityLevel)
		{
			Action<int, int> action = QualitySettings.activeQualityLevelChanged;
			if (action != null)
			{
				action(previousQualityLevel, currentQualityLevel);
			}
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x00019B39 File Offset: 0x00017D39
		public static void IncreaseLevel([DefaultValue("false")] bool applyExpensiveChanges)
		{
			QualitySettings.SetQualityLevel(QualitySettings.GetQualityLevel() + 1, applyExpensiveChanges);
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x00019B4A File Offset: 0x00017D4A
		public static void DecreaseLevel([DefaultValue("false")] bool applyExpensiveChanges)
		{
			QualitySettings.SetQualityLevel(QualitySettings.GetQualityLevel() - 1, applyExpensiveChanges);
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x00019B5B File Offset: 0x00017D5B
		public static void SetQualityLevel(int index)
		{
			QualitySettings.SetQualityLevel(index, true);
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x00019B66 File Offset: 0x00017D66
		public static void IncreaseLevel()
		{
			QualitySettings.IncreaseLevel(false);
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x00019B70 File Offset: 0x00017D70
		public static void DecreaseLevel()
		{
			QualitySettings.DecreaseLevel(false);
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000DE2 RID: 3554 RVA: 0x00019B7C File Offset: 0x00017D7C
		// (set) Token: 0x06000DE3 RID: 3555 RVA: 0x00019B5B File Offset: 0x00017D5B
		[Obsolete("Use GetQualityLevel and SetQualityLevel", false)]
		public static QualityLevel currentLevel
		{
			get
			{
				return (QualityLevel)QualitySettings.GetQualityLevel();
			}
			set
			{
				QualitySettings.SetQualityLevel((int)value, true);
			}
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x00019B94 File Offset: 0x00017D94
		public static void ForEach(Action callback)
		{
			bool flag = callback == null;
			if (!flag)
			{
				int qualityLevel = QualitySettings.GetQualityLevel();
				try
				{
					for (int i = 0; i < QualitySettings.count; i++)
					{
						QualitySettings.SetQualityLevel(i, false);
						callback();
					}
				}
				finally
				{
					QualitySettings.SetQualityLevel(qualityLevel, false);
				}
			}
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x00019BF8 File Offset: 0x00017DF8
		public static void ForEach(Action<int, string> callback)
		{
			bool flag = callback == null;
			if (!flag)
			{
				int qualityLevel = QualitySettings.GetQualityLevel();
				try
				{
					for (int i = 0; i < QualitySettings.count; i++)
					{
						QualitySettings.SetQualityLevel(i, false);
						callback(i, QualitySettings.names[i]);
					}
				}
				finally
				{
					QualitySettings.SetQualityLevel(qualityLevel, false);
				}
			}
		}

		// Token: 0x06000DE6 RID: 3558 RVA: 0x00008C6F File Offset: 0x00006E6F
		private QualitySettings()
		{
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000DE7 RID: 3559
		// (set) Token: 0x06000DE8 RID: 3560
		public static extern int pixelLightCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000DE9 RID: 3561
		// (set) Token: 0x06000DEA RID: 3562
		[NativeProperty("ShadowQuality")]
		public static extern ShadowQuality shadows
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000DEB RID: 3563
		// (set) Token: 0x06000DEC RID: 3564
		public static extern ShadowProjection shadowProjection
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000DED RID: 3565
		// (set) Token: 0x06000DEE RID: 3566
		public static extern int shadowCascades
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000DEF RID: 3567
		// (set) Token: 0x06000DF0 RID: 3568
		public static extern float shadowDistance
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000DF1 RID: 3569
		// (set) Token: 0x06000DF2 RID: 3570
		[NativeProperty("ShadowResolution")]
		public static extern ShadowResolution shadowResolution
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000DF3 RID: 3571
		// (set) Token: 0x06000DF4 RID: 3572
		[NativeProperty("ShadowmaskMode")]
		public static extern ShadowmaskMode shadowmaskMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000DF5 RID: 3573
		// (set) Token: 0x06000DF6 RID: 3574
		public static extern float shadowNearPlaneOffset
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000DF7 RID: 3575
		// (set) Token: 0x06000DF8 RID: 3576
		public static extern float shadowCascade2Split
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000DF9 RID: 3577 RVA: 0x00019C64 File Offset: 0x00017E64
		// (set) Token: 0x06000DFA RID: 3578 RVA: 0x00019C7C File Offset: 0x00017E7C
		public static Vector3 shadowCascade4Split
		{
			get
			{
				Vector3 vector;
				QualitySettings.get_shadowCascade4Split_Injected(out vector);
				return vector;
			}
			set
			{
				QualitySettings.set_shadowCascade4Split_Injected(ref value);
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000DFB RID: 3579
		// (set) Token: 0x06000DFC RID: 3580
		[NativeProperty("LODBias")]
		public static extern float lodBias
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000DFD RID: 3581
		// (set) Token: 0x06000DFE RID: 3582
		[NativeProperty("AnisotropicTextures")]
		public static extern AnisotropicFiltering anisotropicFiltering
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000DFF RID: 3583
		// (set) Token: 0x06000E00 RID: 3584
		[NativeProperty("GlobalTextureMipmapLimit")]
		[Obsolete("masterTextureLimit has been deprecated. Use globalTextureMipmapLimit instead (UnityUpgradable) -> globalTextureMipmapLimit", false)]
		public static extern int masterTextureLimit
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000E01 RID: 3585
		// (set) Token: 0x06000E02 RID: 3586
		public static extern int globalTextureMipmapLimit
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000E03 RID: 3587
		// (set) Token: 0x06000E04 RID: 3588
		public static extern int maximumLODLevel
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000E05 RID: 3589
		// (set) Token: 0x06000E06 RID: 3590
		public static extern bool enableLODCrossFade
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000E07 RID: 3591
		// (set) Token: 0x06000E08 RID: 3592
		public static extern int particleRaycastBudget
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000E09 RID: 3593
		// (set) Token: 0x06000E0A RID: 3594
		public static extern bool softParticles
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000E0B RID: 3595
		// (set) Token: 0x06000E0C RID: 3596
		public static extern bool softVegetation
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000E0D RID: 3597
		// (set) Token: 0x06000E0E RID: 3598
		public static extern int vSyncCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000E0F RID: 3599
		// (set) Token: 0x06000E10 RID: 3600
		public static extern int realtimeGICPUUsage
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000E11 RID: 3601
		// (set) Token: 0x06000E12 RID: 3602
		public static extern int antiAliasing
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000E13 RID: 3603
		// (set) Token: 0x06000E14 RID: 3604
		public static extern int asyncUploadTimeSlice
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000E15 RID: 3605
		// (set) Token: 0x06000E16 RID: 3606
		public static extern int asyncUploadBufferSize
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000E17 RID: 3607
		// (set) Token: 0x06000E18 RID: 3608
		public static extern bool asyncUploadPersistentBuffer
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06000E19 RID: 3609
		[NativeName("SetLODSettings")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetLODSettings(float lodBias, int maximumLODLevel, bool setDirty = true);

		// Token: 0x06000E1A RID: 3610 RVA: 0x00019C90 File Offset: 0x00017E90
		[NativeName("SetTextureMipmapLimitSettings")]
		[NativeThrows]
		public unsafe static void SetTextureMipmapLimitSettings(string groupName, TextureMipmapLimitSettings textureMipmapLimitSettings)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(groupName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = groupName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				QualitySettings.SetTextureMipmapLimitSettings_Injected(ref managedSpanWrapper, ref textureMipmapLimitSettings);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x00019CE8 File Offset: 0x00017EE8
		[NativeName("GetTextureMipmapLimitSettings")]
		[NativeThrows]
		public unsafe static TextureMipmapLimitSettings GetTextureMipmapLimitSettings(string groupName)
		{
			TextureMipmapLimitSettings textureMipmapLimitSettings2;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(groupName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = groupName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				TextureMipmapLimitSettings textureMipmapLimitSettings;
				QualitySettings.GetTextureMipmapLimitSettings_Injected(ref managedSpanWrapper, out textureMipmapLimitSettings);
			}
			finally
			{
				char* ptr = null;
				TextureMipmapLimitSettings textureMipmapLimitSettings;
				textureMipmapLimitSettings2 = textureMipmapLimitSettings;
			}
			return textureMipmapLimitSettings2;
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000E1C RID: 3612
		// (set) Token: 0x06000E1D RID: 3613
		public static extern bool realtimeReflectionProbes
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000E1E RID: 3614
		// (set) Token: 0x06000E1F RID: 3615
		public static extern bool billboardsFaceCameraPosition
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000E20 RID: 3616
		// (set) Token: 0x06000E21 RID: 3617
		public static extern bool useLegacyDetailDistribution
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000E22 RID: 3618
		// (set) Token: 0x06000E23 RID: 3619
		public static extern float resolutionScalingFixedDPIFactor
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000E24 RID: 3620
		// (set) Token: 0x06000E25 RID: 3621
		public static extern TerrainQualityOverrides terrainQualityOverrides
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000E26 RID: 3622
		// (set) Token: 0x06000E27 RID: 3623
		public static extern float terrainPixelError
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000E28 RID: 3624
		// (set) Token: 0x06000E29 RID: 3625
		public static extern float terrainDetailDensityScale
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000E2A RID: 3626
		// (set) Token: 0x06000E2B RID: 3627
		public static extern float terrainBasemapDistance
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000E2C RID: 3628
		// (set) Token: 0x06000E2D RID: 3629
		public static extern float terrainDetailDistance
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000E2E RID: 3630
		// (set) Token: 0x06000E2F RID: 3631
		public static extern float terrainTreeDistance
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000E30 RID: 3632
		// (set) Token: 0x06000E31 RID: 3633
		public static extern float terrainBillboardStart
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000E32 RID: 3634
		// (set) Token: 0x06000E33 RID: 3635
		public static extern float terrainFadeLength
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000E34 RID: 3636
		// (set) Token: 0x06000E35 RID: 3637
		public static extern float terrainMaxTrees
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000E36 RID: 3638 RVA: 0x00019D44 File Offset: 0x00017F44
		// (set) Token: 0x06000E37 RID: 3639 RVA: 0x00019D5C File Offset: 0x00017F5C
		[NativeName("RenderPipeline")]
		private static ScriptableObject INTERNAL_renderPipeline
		{
			get
			{
				return Unmarshal.UnmarshalUnityObject<ScriptableObject>(QualitySettings.get_INTERNAL_renderPipeline_Injected());
			}
			set
			{
				QualitySettings.set_INTERNAL_renderPipeline_Injected(Object.MarshalledUnityObject.Marshal<ScriptableObject>(value));
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000E38 RID: 3640 RVA: 0x00019D74 File Offset: 0x00017F74
		// (set) Token: 0x06000E39 RID: 3641 RVA: 0x00019D90 File Offset: 0x00017F90
		public static RenderPipelineAsset renderPipeline
		{
			get
			{
				return QualitySettings.INTERNAL_renderPipeline as RenderPipelineAsset;
			}
			set
			{
				GraphicsSettings.ValidateSetRenderPipelineAsset(value);
				QualitySettings.INTERNAL_renderPipeline = value;
			}
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x00019DA4 File Offset: 0x00017FA4
		[NativeName("GetRenderPipelineAssetAt")]
		internal static ScriptableObject InternalGetRenderPipelineAssetAt(int index)
		{
			return Unmarshal.UnmarshalUnityObject<ScriptableObject>(QualitySettings.InternalGetRenderPipelineAssetAt_Injected(index));
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x00019DBC File Offset: 0x00017FBC
		public static RenderPipelineAsset GetRenderPipelineAssetAt(int index)
		{
			bool flag = index < 0 || index >= QualitySettings.names.Length;
			if (flag)
			{
				throw new IndexOutOfRangeException(string.Format("{0} is out of range [0..{1}[", "index", QualitySettings.names.Length));
			}
			return QualitySettings.InternalGetRenderPipelineAssetAt(index) as RenderPipelineAsset;
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000E3C RID: 3644
		// (set) Token: 0x06000E3D RID: 3645
		[Obsolete("blendWeights is obsolete. Use skinWeights instead (UnityUpgradable) -> skinWeights", true)]
		public static extern BlendWeights blendWeights
		{
			[NativeName("GetSkinWeights")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeThrows]
			[NativeName("SetSkinWeights")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000E3E RID: 3646
		// (set) Token: 0x06000E3F RID: 3647
		public static extern SkinWeights skinWeights
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeThrows]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000E40 RID: 3648
		public static extern int count
		{
			[NativeName("GetQualitySettingsCount")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06000E41 RID: 3649
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetStrippedMaximumLODLevel();

		// Token: 0x06000E42 RID: 3650
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetStrippedMaximumLODLevel(int maximumLODLevel);

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000E43 RID: 3651
		// (set) Token: 0x06000E44 RID: 3652
		public static extern bool streamingMipmapsActive
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000E45 RID: 3653
		// (set) Token: 0x06000E46 RID: 3654
		public static extern float streamingMipmapsMemoryBudget
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000E47 RID: 3655
		// (set) Token: 0x06000E48 RID: 3656
		public static extern int streamingMipmapsRenderersPerFrame
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000E49 RID: 3657
		// (set) Token: 0x06000E4A RID: 3658
		public static extern int streamingMipmapsMaxLevelReduction
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000E4B RID: 3659
		// (set) Token: 0x06000E4C RID: 3660
		public static extern bool streamingMipmapsAddAllCameras
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000E4D RID: 3661
		// (set) Token: 0x06000E4E RID: 3662
		public static extern int streamingMipmapsMaxFileIORequests
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000E4F RID: 3663
		// (set) Token: 0x06000E50 RID: 3664
		[StaticAccessor("QualitySettingsScripting", StaticAccessorType.DoubleColon)]
		public static extern int maxQueuedFrames
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06000E51 RID: 3665
		[NativeName("GetCurrentIndex")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetQualityLevel();

		// Token: 0x06000E52 RID: 3666 RVA: 0x00019E14 File Offset: 0x00018014
		[FreeFunction]
		public static Object GetQualitySettings()
		{
			return Unmarshal.UnmarshalUnityObject<Object>(QualitySettings.GetQualitySettings_Injected());
		}

		// Token: 0x06000E53 RID: 3667
		[NativeName("SetCurrentIndex")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetQualityLevel(int index, [DefaultValue("true")] bool applyExpensiveChanges);

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000E54 RID: 3668
		[NativeProperty("QualitySettingsNames")]
		public static extern string[] names
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000E55 RID: 3669
		public static extern ColorSpace desiredColorSpace
		{
			[NativeName("GetColorSpace")]
			[StaticAccessor("GetPlayerSettings()", StaticAccessorType.Dot)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000E56 RID: 3670
		public static extern ColorSpace activeColorSpace
		{
			[NativeName("GetColorSpace")]
			[StaticAccessor("GetPlayerSettings()", StaticAccessorType.Dot)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06000E57 RID: 3671
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_shadowCascade4Split_Injected(out Vector3 ret);

		// Token: 0x06000E58 RID: 3672
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_shadowCascade4Split_Injected([In] ref Vector3 value);

		// Token: 0x06000E59 RID: 3673
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetTextureMipmapLimitSettings_Injected(ref ManagedSpanWrapper groupName, [In] ref TextureMipmapLimitSettings textureMipmapLimitSettings);

		// Token: 0x06000E5A RID: 3674
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetTextureMipmapLimitSettings_Injected(ref ManagedSpanWrapper groupName, out TextureMipmapLimitSettings ret);

		// Token: 0x06000E5B RID: 3675
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_INTERNAL_renderPipeline_Injected();

		// Token: 0x06000E5C RID: 3676
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_INTERNAL_renderPipeline_Injected(IntPtr value);

		// Token: 0x06000E5D RID: 3677
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr InternalGetRenderPipelineAssetAt_Injected(int index);

		// Token: 0x06000E5E RID: 3678
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetQualitySettings_Injected();
	}
}
