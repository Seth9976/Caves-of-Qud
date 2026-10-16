using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.GlobalIllumination
{
	// Token: 0x02000583 RID: 1411
	public static class Lightmapping
	{
		// Token: 0x060036A6 RID: 13990 RVA: 0x0006BF38 File Offset: 0x0006A138
		[RequiredByNativeCode]
		public static void SetDelegate(Lightmapping.RequestLightsDelegate del)
		{
			Lightmapping.s_RequestLightsDelegate = ((del != null) ? del : Lightmapping.s_DefaultDelegate);
		}

		// Token: 0x060036A7 RID: 13991 RVA: 0x0006BF4C File Offset: 0x0006A14C
		[RequiredByNativeCode]
		public static Lightmapping.RequestLightsDelegate GetDelegate()
		{
			return Lightmapping.s_RequestLightsDelegate;
		}

		// Token: 0x060036A8 RID: 13992 RVA: 0x0006BF63 File Offset: 0x0006A163
		[RequiredByNativeCode]
		public static void ResetDelegate()
		{
			Lightmapping.s_RequestLightsDelegate = Lightmapping.s_DefaultDelegate;
		}

		// Token: 0x060036A9 RID: 13993 RVA: 0x0006BF70 File Offset: 0x0006A170
		[RequiredByNativeCode]
		internal unsafe static void RequestLights(Light[] lights, IntPtr outLightsPtr, int outLightsCount)
		{
			NativeArray<LightDataGI> nativeArray = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<LightDataGI>((void*)outLightsPtr, outLightsCount, Allocator.None);
			Lightmapping.s_RequestLightsDelegate(lights, nativeArray);
		}

		// Token: 0x040012D9 RID: 4825
		[RequiredByNativeCode]
		private static readonly Lightmapping.RequestLightsDelegate s_DefaultDelegate = delegate(Light[] requests, NativeArray<LightDataGI> lightsOutput)
		{
			DirectionalLight directionalLight = default(DirectionalLight);
			PointLight pointLight = default(PointLight);
			SpotLight spotLight = default(SpotLight);
			RectangleLight rectangleLight = default(RectangleLight);
			DiscLight discLight = default(DiscLight);
			Cookie cookie = default(Cookie);
			LightDataGI lightDataGI = default(LightDataGI);
			for (int i = 0; i < requests.Length; i++)
			{
				Light light = requests[i];
				switch (light.type)
				{
				case LightType.Spot:
					LightmapperUtils.Extract(light, ref spotLight);
					LightmapperUtils.Extract(light, out cookie);
					lightDataGI.Init(ref spotLight, ref cookie);
					break;
				case LightType.Directional:
					LightmapperUtils.Extract(light, ref directionalLight);
					LightmapperUtils.Extract(light, out cookie);
					lightDataGI.Init(ref directionalLight, ref cookie);
					break;
				case LightType.Point:
					LightmapperUtils.Extract(light, ref pointLight);
					LightmapperUtils.Extract(light, out cookie);
					lightDataGI.Init(ref pointLight, ref cookie);
					break;
				case LightType.Area:
					LightmapperUtils.Extract(light, ref rectangleLight);
					LightmapperUtils.Extract(light, out cookie);
					lightDataGI.Init(ref rectangleLight, ref cookie);
					break;
				case LightType.Disc:
					LightmapperUtils.Extract(light, ref discLight);
					LightmapperUtils.Extract(light, out cookie);
					lightDataGI.Init(ref discLight, ref cookie);
					break;
				default:
					lightDataGI.InitNoBake(light.GetInstanceID());
					break;
				}
				lightsOutput[i] = lightDataGI;
			}
		};

		// Token: 0x040012DA RID: 4826
		[RequiredByNativeCode]
		private static Lightmapping.RequestLightsDelegate s_RequestLightsDelegate = Lightmapping.s_DefaultDelegate;

		// Token: 0x02000584 RID: 1412
		// (Invoke) Token: 0x060036AC RID: 13996
		public delegate void RequestLightsDelegate(Light[] requests, NativeArray<LightDataGI> lightsOutput);
	}
}
