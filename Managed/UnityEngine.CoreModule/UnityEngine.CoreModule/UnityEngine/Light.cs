using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;

namespace UnityEngine
{
	// Token: 0x0200019A RID: 410
	[NativeHeader("Runtime/Camera/Light.h")]
	[RequireComponent(typeof(Transform))]
	[RequireComponent(typeof(Transform))]
	[NativeHeader("Runtime/Export/Graphics/Light.bindings.h")]
	public sealed class Light : Behaviour
	{
		// Token: 0x17000353 RID: 851
		// (get) Token: 0x060013E2 RID: 5090 RVA: 0x00022420 File Offset: 0x00020620
		// (set) Token: 0x060013E3 RID: 5091 RVA: 0x00022444 File Offset: 0x00020644
		[NativeProperty("LightType")]
		public LightType type
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Light.get_type_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_type_Injected(intPtr, value);
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x060013E4 RID: 5092 RVA: 0x00022467 File Offset: 0x00020667
		// (set) Token: 0x060013E5 RID: 5093 RVA: 0x0002246F File Offset: 0x0002066F
		[Obsolete("This property has been deprecated. Use Light.type instead.")]
		public LightShape shape { get; set; }

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x060013E6 RID: 5094 RVA: 0x00022478 File Offset: 0x00020678
		// (set) Token: 0x060013E7 RID: 5095 RVA: 0x0002249C File Offset: 0x0002069C
		public float spotAngle
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Light.get_spotAngle_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_spotAngle_Injected(intPtr, value);
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x060013E8 RID: 5096 RVA: 0x000224C0 File Offset: 0x000206C0
		// (set) Token: 0x060013E9 RID: 5097 RVA: 0x000224E4 File Offset: 0x000206E4
		public float innerSpotAngle
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Light.get_innerSpotAngle_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_innerSpotAngle_Injected(intPtr, value);
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x060013EA RID: 5098 RVA: 0x00022508 File Offset: 0x00020708
		// (set) Token: 0x060013EB RID: 5099 RVA: 0x00022530 File Offset: 0x00020730
		public Color color
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Color color;
				Light.get_color_Injected(intPtr, out color);
				return color;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_color_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x060013EC RID: 5100 RVA: 0x00022554 File Offset: 0x00020754
		// (set) Token: 0x060013ED RID: 5101 RVA: 0x00022578 File Offset: 0x00020778
		public float colorTemperature
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Light.get_colorTemperature_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_colorTemperature_Injected(intPtr, value);
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x060013EE RID: 5102 RVA: 0x0002259C File Offset: 0x0002079C
		// (set) Token: 0x060013EF RID: 5103 RVA: 0x000225C0 File Offset: 0x000207C0
		public bool useColorTemperature
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Light.get_useColorTemperature_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_useColorTemperature_Injected(intPtr, value);
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x060013F0 RID: 5104 RVA: 0x000225E4 File Offset: 0x000207E4
		// (set) Token: 0x060013F1 RID: 5105 RVA: 0x00022608 File Offset: 0x00020808
		public float intensity
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Light.get_intensity_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_intensity_Injected(intPtr, value);
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x060013F2 RID: 5106 RVA: 0x0002262C File Offset: 0x0002082C
		// (set) Token: 0x060013F3 RID: 5107 RVA: 0x00022650 File Offset: 0x00020850
		public float bounceIntensity
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Light.get_bounceIntensity_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_bounceIntensity_Injected(intPtr, value);
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x060013F4 RID: 5108 RVA: 0x00022674 File Offset: 0x00020874
		// (set) Token: 0x060013F5 RID: 5109 RVA: 0x00022698 File Offset: 0x00020898
		public LightUnit lightUnit
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Light.get_lightUnit_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_lightUnit_Injected(intPtr, value);
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x060013F6 RID: 5110 RVA: 0x000226BC File Offset: 0x000208BC
		// (set) Token: 0x060013F7 RID: 5111 RVA: 0x000226E0 File Offset: 0x000208E0
		public float luxAtDistance
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Light.get_luxAtDistance_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_luxAtDistance_Injected(intPtr, value);
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x060013F8 RID: 5112 RVA: 0x00022704 File Offset: 0x00020904
		// (set) Token: 0x060013F9 RID: 5113 RVA: 0x00022728 File Offset: 0x00020928
		public bool enableSpotReflector
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Light.get_enableSpotReflector_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_enableSpotReflector_Injected(intPtr, value);
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x060013FA RID: 5114 RVA: 0x0002274C File Offset: 0x0002094C
		// (set) Token: 0x060013FB RID: 5115 RVA: 0x00022770 File Offset: 0x00020970
		public bool useBoundingSphereOverride
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Light.get_useBoundingSphereOverride_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_useBoundingSphereOverride_Injected(intPtr, value);
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x060013FC RID: 5116 RVA: 0x00022794 File Offset: 0x00020994
		// (set) Token: 0x060013FD RID: 5117 RVA: 0x000227BC File Offset: 0x000209BC
		public Vector4 boundingSphereOverride
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector4 vector;
				Light.get_boundingSphereOverride_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_boundingSphereOverride_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x060013FE RID: 5118 RVA: 0x000227E0 File Offset: 0x000209E0
		// (set) Token: 0x060013FF RID: 5119 RVA: 0x00022804 File Offset: 0x00020A04
		public bool useViewFrustumForShadowCasterCull
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Light.get_useViewFrustumForShadowCasterCull_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_useViewFrustumForShadowCasterCull_Injected(intPtr, value);
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06001400 RID: 5120 RVA: 0x00022828 File Offset: 0x00020A28
		// (set) Token: 0x06001401 RID: 5121 RVA: 0x0002284C File Offset: 0x00020A4C
		public bool forceVisible
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Light.get_forceVisible_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_forceVisible_Injected(intPtr, value);
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06001402 RID: 5122 RVA: 0x00022870 File Offset: 0x00020A70
		// (set) Token: 0x06001403 RID: 5123 RVA: 0x00022894 File Offset: 0x00020A94
		public int shadowCustomResolution
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Light.get_shadowCustomResolution_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_shadowCustomResolution_Injected(intPtr, value);
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06001404 RID: 5124 RVA: 0x000228B8 File Offset: 0x00020AB8
		// (set) Token: 0x06001405 RID: 5125 RVA: 0x000228DC File Offset: 0x00020ADC
		public float shadowBias
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Light.get_shadowBias_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_shadowBias_Injected(intPtr, value);
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06001406 RID: 5126 RVA: 0x00022900 File Offset: 0x00020B00
		// (set) Token: 0x06001407 RID: 5127 RVA: 0x00022924 File Offset: 0x00020B24
		public float shadowNormalBias
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Light.get_shadowNormalBias_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_shadowNormalBias_Injected(intPtr, value);
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06001408 RID: 5128 RVA: 0x00022948 File Offset: 0x00020B48
		// (set) Token: 0x06001409 RID: 5129 RVA: 0x0002296C File Offset: 0x00020B6C
		public float shadowNearPlane
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Light.get_shadowNearPlane_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_shadowNearPlane_Injected(intPtr, value);
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x0600140A RID: 5130 RVA: 0x00022990 File Offset: 0x00020B90
		// (set) Token: 0x0600140B RID: 5131 RVA: 0x000229B4 File Offset: 0x00020BB4
		public bool useShadowMatrixOverride
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Light.get_useShadowMatrixOverride_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_useShadowMatrixOverride_Injected(intPtr, value);
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x0600140C RID: 5132 RVA: 0x000229D8 File Offset: 0x00020BD8
		// (set) Token: 0x0600140D RID: 5133 RVA: 0x00022A00 File Offset: 0x00020C00
		public Matrix4x4 shadowMatrixOverride
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Matrix4x4 matrix4x;
				Light.get_shadowMatrixOverride_Injected(intPtr, out matrix4x);
				return matrix4x;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_shadowMatrixOverride_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x0600140E RID: 5134 RVA: 0x00022A24 File Offset: 0x00020C24
		// (set) Token: 0x0600140F RID: 5135 RVA: 0x00022A48 File Offset: 0x00020C48
		public float range
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Light.get_range_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_range_Injected(intPtr, value);
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06001410 RID: 5136 RVA: 0x00022A6C File Offset: 0x00020C6C
		public float dilatedRange
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Light.get_dilatedRange_Injected(intPtr);
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06001411 RID: 5137 RVA: 0x00022A90 File Offset: 0x00020C90
		// (set) Token: 0x06001412 RID: 5138 RVA: 0x00022AB8 File Offset: 0x00020CB8
		public Flare flare
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Flare>(Light.get_flare_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_flare_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Flare>(value));
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06001413 RID: 5139 RVA: 0x00022AE0 File Offset: 0x00020CE0
		// (set) Token: 0x06001414 RID: 5140 RVA: 0x00022B08 File Offset: 0x00020D08
		public LightBakingOutput bakingOutput
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LightBakingOutput lightBakingOutput;
				Light.get_bakingOutput_Injected(intPtr, out lightBakingOutput);
				return lightBakingOutput;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_bakingOutput_Injected(intPtr, ref value);
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06001415 RID: 5141 RVA: 0x00022B2C File Offset: 0x00020D2C
		// (set) Token: 0x06001416 RID: 5142 RVA: 0x00022B50 File Offset: 0x00020D50
		public int cullingMask
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Light.get_cullingMask_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_cullingMask_Injected(intPtr, value);
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06001417 RID: 5143 RVA: 0x00022B74 File Offset: 0x00020D74
		// (set) Token: 0x06001418 RID: 5144 RVA: 0x00022B98 File Offset: 0x00020D98
		public int renderingLayerMask
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Light.get_renderingLayerMask_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_renderingLayerMask_Injected(intPtr, value);
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06001419 RID: 5145 RVA: 0x00022BBC File Offset: 0x00020DBC
		// (set) Token: 0x0600141A RID: 5146 RVA: 0x00022BE0 File Offset: 0x00020DE0
		public LightShadowCasterMode lightShadowCasterMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Light.get_lightShadowCasterMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_lightShadowCasterMode_Injected(intPtr, value);
			}
		}

		// Token: 0x0600141B RID: 5147 RVA: 0x00022C04 File Offset: 0x00020E04
		public void Reset()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Light.Reset_Injected(intPtr);
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x0600141C RID: 5148 RVA: 0x00022C28 File Offset: 0x00020E28
		// (set) Token: 0x0600141D RID: 5149 RVA: 0x00022C4C File Offset: 0x00020E4C
		public LightShadows shadows
		{
			[NativeMethod("GetShadowType")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Light.get_shadows_Injected(intPtr);
			}
			[FreeFunction("Light_Bindings::SetShadowType", HasExplicitThis = true, ThrowsException = true)]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_shadows_Injected(intPtr, value);
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x0600141E RID: 5150 RVA: 0x00022C70 File Offset: 0x00020E70
		// (set) Token: 0x0600141F RID: 5151 RVA: 0x00022C94 File Offset: 0x00020E94
		public float shadowStrength
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Light.get_shadowStrength_Injected(intPtr);
			}
			[FreeFunction("Light_Bindings::SetShadowStrength", HasExplicitThis = true)]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_shadowStrength_Injected(intPtr, value);
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06001420 RID: 5152 RVA: 0x00022CB8 File Offset: 0x00020EB8
		// (set) Token: 0x06001421 RID: 5153 RVA: 0x00022CDC File Offset: 0x00020EDC
		public LightShadowResolution shadowResolution
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Light.get_shadowResolution_Injected(intPtr);
			}
			[FreeFunction("Light_Bindings::SetShadowResolution", HasExplicitThis = true, ThrowsException = true)]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_shadowResolution_Injected(intPtr, value);
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06001422 RID: 5154 RVA: 0x00022D00 File Offset: 0x00020F00
		// (set) Token: 0x06001423 RID: 5155 RVA: 0x00002925 File Offset: 0x00000B25
		[Obsolete("Shadow softness is removed in Unity 5.0+", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float shadowSoftness
		{
			get
			{
				return 4f;
			}
			set
			{
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06001424 RID: 5156 RVA: 0x00022D18 File Offset: 0x00020F18
		// (set) Token: 0x06001425 RID: 5157 RVA: 0x00002925 File Offset: 0x00000B25
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Shadow softness is removed in Unity 5.0+", true)]
		public float shadowSoftnessFade
		{
			get
			{
				return 1f;
			}
			set
			{
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06001426 RID: 5158 RVA: 0x00022D30 File Offset: 0x00020F30
		// (set) Token: 0x06001427 RID: 5159 RVA: 0x00022D74 File Offset: 0x00020F74
		public unsafe float[] layerShadowCullDistances
		{
			[FreeFunction("Light_Bindings::GetLayerShadowCullDistances", HasExplicitThis = true, ThrowsException = false)]
			get
			{
				float[] array2;
				try
				{
					IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					BlittableArrayWrapper blittableArrayWrapper;
					Light.get_layerShadowCullDistances_Injected(intPtr, out blittableArrayWrapper);
				}
				finally
				{
					BlittableArrayWrapper blittableArrayWrapper;
					float[] array;
					blittableArrayWrapper.Unmarshal<float>(ref array);
					array2 = array;
				}
				return array2;
			}
			[FreeFunction("Light_Bindings::SetLayerShadowCullDistances", HasExplicitThis = true, ThrowsException = true)]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Span<float> span = new Span<float>(value);
				fixed (float* pinnableReference = span.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
					Light.set_layerShadowCullDistances_Injected(intPtr, ref managedSpanWrapper);
				}
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06001428 RID: 5160 RVA: 0x00022DBC File Offset: 0x00020FBC
		// (set) Token: 0x06001429 RID: 5161 RVA: 0x00022DE0 File Offset: 0x00020FE0
		public float cookieSize
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Light.get_cookieSize_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_cookieSize_Injected(intPtr, value);
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x0600142A RID: 5162 RVA: 0x00022E04 File Offset: 0x00021004
		// (set) Token: 0x0600142B RID: 5163 RVA: 0x00022E2C File Offset: 0x0002102C
		public Texture cookie
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Texture>(Light.get_cookie_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_cookie_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Texture>(value));
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x0600142C RID: 5164 RVA: 0x00022E54 File Offset: 0x00021054
		// (set) Token: 0x0600142D RID: 5165 RVA: 0x00022E78 File Offset: 0x00021078
		public LightRenderMode renderMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Light.get_renderMode_Injected(intPtr);
			}
			[FreeFunction("Light_Bindings::SetRenderMode", HasExplicitThis = true, ThrowsException = true)]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_renderMode_Injected(intPtr, value);
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x0600142E RID: 5166 RVA: 0x00022E9C File Offset: 0x0002109C
		// (set) Token: 0x0600142F RID: 5167 RVA: 0x00022EB4 File Offset: 0x000210B4
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("warning bakedIndex has been removed please use bakingOutput.isBaked instead.", true)]
		public int bakedIndex
		{
			get
			{
				return this.m_BakedIndex;
			}
			set
			{
				this.m_BakedIndex = value;
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06001430 RID: 5168 RVA: 0x00022EC0 File Offset: 0x000210C0
		// (set) Token: 0x06001431 RID: 5169 RVA: 0x00022EE8 File Offset: 0x000210E8
		public Vector2 areaSize
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				Light.get_areaSize_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Light.set_areaSize_Injected(intPtr, ref value);
			}
		}

		// Token: 0x06001432 RID: 5170 RVA: 0x00022F0C File Offset: 0x0002110C
		public void AddCommandBuffer(LightEvent evt, CommandBuffer buffer)
		{
			this.AddCommandBuffer(evt, buffer, ShadowMapPass.All);
		}

		// Token: 0x06001433 RID: 5171 RVA: 0x00022F20 File Offset: 0x00021120
		public void AddCommandBuffer(LightEvent evt, CommandBuffer buffer, ShadowMapPass shadowPassMask)
		{
			bool flag = RenderPipelineManager.currentPipeline != null;
			if (flag)
			{
				Debug.LogWarning("Your project uses a scriptable render pipeline. You can use Light.AddCommandBuffer only with the built-in renderer.");
			}
			this.AddCommandBufferInternal(evt, buffer, shadowPassMask);
		}

		// Token: 0x06001434 RID: 5172 RVA: 0x00022F54 File Offset: 0x00021154
		[FreeFunction("Light_Bindings::AddCommandBuffer", HasExplicitThis = true)]
		internal void AddCommandBufferInternal(LightEvent evt, CommandBuffer buffer, ShadowMapPass shadowPassMask)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Light.AddCommandBufferInternal_Injected(intPtr, evt, (buffer == null) ? ((IntPtr)0) : CommandBuffer.BindingsMarshaller.ConvertToNative(buffer), shadowPassMask);
		}

		// Token: 0x06001435 RID: 5173 RVA: 0x00022F87 File Offset: 0x00021187
		public void AddCommandBufferAsync(LightEvent evt, CommandBuffer buffer, ComputeQueueType queueType)
		{
			this.AddCommandBufferAsync(evt, buffer, ShadowMapPass.All, queueType);
		}

		// Token: 0x06001436 RID: 5174 RVA: 0x00022F9C File Offset: 0x0002119C
		public void AddCommandBufferAsync(LightEvent evt, CommandBuffer buffer, ShadowMapPass shadowPassMask, ComputeQueueType queueType)
		{
			bool flag = RenderPipelineManager.currentPipeline != null;
			if (flag)
			{
				Debug.LogWarning("Your project uses a scriptable render pipeline. You can use Light.AddCommandBufferAsync only with the built-in renderer.");
			}
			this.AddCommandBufferAsyncInternal(evt, buffer, shadowPassMask, queueType);
		}

		// Token: 0x06001437 RID: 5175 RVA: 0x00022FD0 File Offset: 0x000211D0
		[FreeFunction("Light_Bindings::AddCommandBufferAsync", HasExplicitThis = true)]
		internal void AddCommandBufferAsyncInternal(LightEvent evt, CommandBuffer buffer, ShadowMapPass shadowPassMask, ComputeQueueType queueType)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Light.AddCommandBufferAsyncInternal_Injected(intPtr, evt, (buffer == null) ? ((IntPtr)0) : CommandBuffer.BindingsMarshaller.ConvertToNative(buffer), shadowPassMask, queueType);
		}

		// Token: 0x06001438 RID: 5176 RVA: 0x00023008 File Offset: 0x00021208
		public void RemoveCommandBuffer(LightEvent evt, CommandBuffer buffer)
		{
			bool flag = RenderPipelineManager.currentPipeline != null;
			if (flag)
			{
				Debug.LogWarning("Your project uses a scriptable render pipeline. You can use Light.RemoveCommandBuffer only with the built-in renderer.");
			}
			this.RemoveCommandBufferInternal(evt, buffer);
		}

		// Token: 0x06001439 RID: 5177 RVA: 0x00023038 File Offset: 0x00021238
		[NativeMethod("RemoveCommandBuffer")]
		internal void RemoveCommandBufferInternal(LightEvent evt, CommandBuffer buffer)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Light.RemoveCommandBufferInternal_Injected(intPtr, evt, (buffer == null) ? ((IntPtr)0) : CommandBuffer.BindingsMarshaller.ConvertToNative(buffer));
		}

		// Token: 0x0600143A RID: 5178 RVA: 0x0002306C File Offset: 0x0002126C
		public void RemoveCommandBuffers(LightEvent evt)
		{
			bool flag = RenderPipelineManager.currentPipeline != null;
			if (flag)
			{
				Debug.LogWarning("Your project uses a scriptable render pipeline. You can use Light.RemoveCommandBuffer only with the built-in renderer.");
			}
			this.RemoveCommandBuffersInternal(evt);
		}

		// Token: 0x0600143B RID: 5179 RVA: 0x0002309C File Offset: 0x0002129C
		[NativeMethod("RemoveCommandBuffers")]
		internal void RemoveCommandBuffersInternal(LightEvent evt)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Light.RemoveCommandBuffersInternal_Injected(intPtr, evt);
		}

		// Token: 0x0600143C RID: 5180 RVA: 0x000230C0 File Offset: 0x000212C0
		public void RemoveAllCommandBuffers()
		{
			bool flag = RenderPipelineManager.currentPipeline != null;
			if (flag)
			{
				Debug.LogWarning("Your project uses a scriptable render pipeline. You can use Light.RemoveAllCommandBuffers only with the built-in renderer.");
			}
			this.RemoveAllCommandBuffersInternal();
		}

		// Token: 0x0600143D RID: 5181 RVA: 0x000230F0 File Offset: 0x000212F0
		[NativeMethod("RemoveAllCommandBuffers")]
		internal void RemoveAllCommandBuffersInternal()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Light.RemoveAllCommandBuffersInternal_Injected(intPtr);
		}

		// Token: 0x0600143E RID: 5182 RVA: 0x00023114 File Offset: 0x00021314
		public CommandBuffer[] GetCommandBuffers(LightEvent evt)
		{
			bool flag = RenderPipelineManager.currentPipeline != null;
			if (flag)
			{
				Debug.LogWarning("Your project uses a scriptable render pipeline. You can use Light.GetCommandBuffers only with the built-in renderer.");
			}
			return this.GetCommandBuffersInternal(evt);
		}

		// Token: 0x0600143F RID: 5183 RVA: 0x00023148 File Offset: 0x00021348
		[FreeFunction("Light_Bindings::GetCommandBuffers", HasExplicitThis = true)]
		internal CommandBuffer[] GetCommandBuffersInternal(LightEvent evt)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Light.GetCommandBuffersInternal_Injected(intPtr, evt);
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06001440 RID: 5184 RVA: 0x0002316C File Offset: 0x0002136C
		public int commandBufferCount
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Light>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Light.get_commandBufferCount_Injected(intPtr);
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06001441 RID: 5185 RVA: 0x00023190 File Offset: 0x00021390
		// (set) Token: 0x06001442 RID: 5186 RVA: 0x000231A7 File Offset: 0x000213A7
		[Obsolete("Use QualitySettings.pixelLightCount instead.")]
		public static int pixelLightCount
		{
			get
			{
				return QualitySettings.pixelLightCount;
			}
			set
			{
				QualitySettings.pixelLightCount = value;
			}
		}

		// Token: 0x06001443 RID: 5187
		[Obsolete("Light.GetLights has been deprecated, use FindObjectsOfType in combination with light.cullingmask/light.type", false)]
		[FreeFunction("Light_Bindings::GetLights")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Light[] GetLights(LightType type, int layer);

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06001444 RID: 5188 RVA: 0x000231B4 File Offset: 0x000213B4
		// (set) Token: 0x06001445 RID: 5189 RVA: 0x00002925 File Offset: 0x00000B25
		[Obsolete("light.shadowConstantBias was removed, use light.shadowBias", true)]
		public float shadowConstantBias
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06001446 RID: 5190 RVA: 0x000231CC File Offset: 0x000213CC
		// (set) Token: 0x06001447 RID: 5191 RVA: 0x00002925 File Offset: 0x00000B25
		[Obsolete("light.shadowObjectSizeBias was removed, use light.shadowBias", true)]
		public float shadowObjectSizeBias
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06001448 RID: 5192 RVA: 0x000231E4 File Offset: 0x000213E4
		// (set) Token: 0x06001449 RID: 5193 RVA: 0x00002925 File Offset: 0x00000B25
		[Obsolete("light.attenuate was removed; all lights always attenuate now", true)]
		public bool attenuate
		{
			get
			{
				return true;
			}
			set
			{
			}
		}

		// Token: 0x0600144B RID: 5195
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern LightType get_type_Injected(IntPtr _unity_self);

		// Token: 0x0600144C RID: 5196
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_type_Injected(IntPtr _unity_self, LightType value);

		// Token: 0x0600144D RID: 5197
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_spotAngle_Injected(IntPtr _unity_self);

		// Token: 0x0600144E RID: 5198
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_spotAngle_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600144F RID: 5199
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_innerSpotAngle_Injected(IntPtr _unity_self);

		// Token: 0x06001450 RID: 5200
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_innerSpotAngle_Injected(IntPtr _unity_self, float value);

		// Token: 0x06001451 RID: 5201
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_color_Injected(IntPtr _unity_self, out Color ret);

		// Token: 0x06001452 RID: 5202
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_color_Injected(IntPtr _unity_self, [In] ref Color value);

		// Token: 0x06001453 RID: 5203
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_colorTemperature_Injected(IntPtr _unity_self);

		// Token: 0x06001454 RID: 5204
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_colorTemperature_Injected(IntPtr _unity_self, float value);

		// Token: 0x06001455 RID: 5205
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_useColorTemperature_Injected(IntPtr _unity_self);

		// Token: 0x06001456 RID: 5206
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_useColorTemperature_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06001457 RID: 5207
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_intensity_Injected(IntPtr _unity_self);

		// Token: 0x06001458 RID: 5208
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_intensity_Injected(IntPtr _unity_self, float value);

		// Token: 0x06001459 RID: 5209
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_bounceIntensity_Injected(IntPtr _unity_self);

		// Token: 0x0600145A RID: 5210
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_bounceIntensity_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600145B RID: 5211
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern LightUnit get_lightUnit_Injected(IntPtr _unity_self);

		// Token: 0x0600145C RID: 5212
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_lightUnit_Injected(IntPtr _unity_self, LightUnit value);

		// Token: 0x0600145D RID: 5213
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_luxAtDistance_Injected(IntPtr _unity_self);

		// Token: 0x0600145E RID: 5214
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_luxAtDistance_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600145F RID: 5215
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_enableSpotReflector_Injected(IntPtr _unity_self);

		// Token: 0x06001460 RID: 5216
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_enableSpotReflector_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06001461 RID: 5217
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_useBoundingSphereOverride_Injected(IntPtr _unity_self);

		// Token: 0x06001462 RID: 5218
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_useBoundingSphereOverride_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06001463 RID: 5219
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_boundingSphereOverride_Injected(IntPtr _unity_self, out Vector4 ret);

		// Token: 0x06001464 RID: 5220
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_boundingSphereOverride_Injected(IntPtr _unity_self, [In] ref Vector4 value);

		// Token: 0x06001465 RID: 5221
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_useViewFrustumForShadowCasterCull_Injected(IntPtr _unity_self);

		// Token: 0x06001466 RID: 5222
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_useViewFrustumForShadowCasterCull_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06001467 RID: 5223
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_forceVisible_Injected(IntPtr _unity_self);

		// Token: 0x06001468 RID: 5224
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_forceVisible_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06001469 RID: 5225
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_shadowCustomResolution_Injected(IntPtr _unity_self);

		// Token: 0x0600146A RID: 5226
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_shadowCustomResolution_Injected(IntPtr _unity_self, int value);

		// Token: 0x0600146B RID: 5227
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_shadowBias_Injected(IntPtr _unity_self);

		// Token: 0x0600146C RID: 5228
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_shadowBias_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600146D RID: 5229
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_shadowNormalBias_Injected(IntPtr _unity_self);

		// Token: 0x0600146E RID: 5230
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_shadowNormalBias_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600146F RID: 5231
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_shadowNearPlane_Injected(IntPtr _unity_self);

		// Token: 0x06001470 RID: 5232
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_shadowNearPlane_Injected(IntPtr _unity_self, float value);

		// Token: 0x06001471 RID: 5233
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_useShadowMatrixOverride_Injected(IntPtr _unity_self);

		// Token: 0x06001472 RID: 5234
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_useShadowMatrixOverride_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06001473 RID: 5235
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_shadowMatrixOverride_Injected(IntPtr _unity_self, out Matrix4x4 ret);

		// Token: 0x06001474 RID: 5236
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_shadowMatrixOverride_Injected(IntPtr _unity_self, [In] ref Matrix4x4 value);

		// Token: 0x06001475 RID: 5237
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_range_Injected(IntPtr _unity_self);

		// Token: 0x06001476 RID: 5238
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_range_Injected(IntPtr _unity_self, float value);

		// Token: 0x06001477 RID: 5239
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_dilatedRange_Injected(IntPtr _unity_self);

		// Token: 0x06001478 RID: 5240
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_flare_Injected(IntPtr _unity_self);

		// Token: 0x06001479 RID: 5241
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_flare_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x0600147A RID: 5242
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_bakingOutput_Injected(IntPtr _unity_self, out LightBakingOutput ret);

		// Token: 0x0600147B RID: 5243
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_bakingOutput_Injected(IntPtr _unity_self, [In] ref LightBakingOutput value);

		// Token: 0x0600147C RID: 5244
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_cullingMask_Injected(IntPtr _unity_self);

		// Token: 0x0600147D RID: 5245
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_cullingMask_Injected(IntPtr _unity_self, int value);

		// Token: 0x0600147E RID: 5246
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_renderingLayerMask_Injected(IntPtr _unity_self);

		// Token: 0x0600147F RID: 5247
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_renderingLayerMask_Injected(IntPtr _unity_self, int value);

		// Token: 0x06001480 RID: 5248
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern LightShadowCasterMode get_lightShadowCasterMode_Injected(IntPtr _unity_self);

		// Token: 0x06001481 RID: 5249
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_lightShadowCasterMode_Injected(IntPtr _unity_self, LightShadowCasterMode value);

		// Token: 0x06001482 RID: 5250
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Reset_Injected(IntPtr _unity_self);

		// Token: 0x06001483 RID: 5251
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern LightShadows get_shadows_Injected(IntPtr _unity_self);

		// Token: 0x06001484 RID: 5252
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_shadows_Injected(IntPtr _unity_self, LightShadows value);

		// Token: 0x06001485 RID: 5253
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_shadowStrength_Injected(IntPtr _unity_self);

		// Token: 0x06001486 RID: 5254
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_shadowStrength_Injected(IntPtr _unity_self, float value);

		// Token: 0x06001487 RID: 5255
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern LightShadowResolution get_shadowResolution_Injected(IntPtr _unity_self);

		// Token: 0x06001488 RID: 5256
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_shadowResolution_Injected(IntPtr _unity_self, LightShadowResolution value);

		// Token: 0x06001489 RID: 5257
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_layerShadowCullDistances_Injected(IntPtr _unity_self, out BlittableArrayWrapper ret);

		// Token: 0x0600148A RID: 5258
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_layerShadowCullDistances_Injected(IntPtr _unity_self, ref ManagedSpanWrapper value);

		// Token: 0x0600148B RID: 5259
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_cookieSize_Injected(IntPtr _unity_self);

		// Token: 0x0600148C RID: 5260
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_cookieSize_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600148D RID: 5261
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_cookie_Injected(IntPtr _unity_self);

		// Token: 0x0600148E RID: 5262
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_cookie_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x0600148F RID: 5263
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern LightRenderMode get_renderMode_Injected(IntPtr _unity_self);

		// Token: 0x06001490 RID: 5264
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_renderMode_Injected(IntPtr _unity_self, LightRenderMode value);

		// Token: 0x06001491 RID: 5265
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_areaSize_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x06001492 RID: 5266
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_areaSize_Injected(IntPtr _unity_self, [In] ref Vector2 value);

		// Token: 0x06001493 RID: 5267
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddCommandBufferInternal_Injected(IntPtr _unity_self, LightEvent evt, IntPtr buffer, ShadowMapPass shadowPassMask);

		// Token: 0x06001494 RID: 5268
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddCommandBufferAsyncInternal_Injected(IntPtr _unity_self, LightEvent evt, IntPtr buffer, ShadowMapPass shadowPassMask, ComputeQueueType queueType);

		// Token: 0x06001495 RID: 5269
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RemoveCommandBufferInternal_Injected(IntPtr _unity_self, LightEvent evt, IntPtr buffer);

		// Token: 0x06001496 RID: 5270
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RemoveCommandBuffersInternal_Injected(IntPtr _unity_self, LightEvent evt);

		// Token: 0x06001497 RID: 5271
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RemoveAllCommandBuffersInternal_Injected(IntPtr _unity_self);

		// Token: 0x06001498 RID: 5272
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern CommandBuffer[] GetCommandBuffersInternal_Injected(IntPtr _unity_self, LightEvent evt);

		// Token: 0x06001499 RID: 5273
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_commandBufferCount_Injected(IntPtr _unity_self);

		// Token: 0x04000528 RID: 1320
		private int m_BakedIndex;
	}
}
