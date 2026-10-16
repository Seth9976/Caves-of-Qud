using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;

namespace UnityEngine
{
	// Token: 0x02000189 RID: 393
	[StaticAccessor("GetRenderSettings()", StaticAccessorType.Dot)]
	[NativeHeader("Runtime/Graphics/QualitySettingsTypes.h")]
	[NativeHeader("Runtime/Camera/RenderSettings.h")]
	public sealed class RenderSettings : Object
	{
		// Token: 0x17000300 RID: 768
		// (get) Token: 0x060010D7 RID: 4311 RVA: 0x0001D6D8 File Offset: 0x0001B8D8
		// (set) Token: 0x060010D8 RID: 4312 RVA: 0x0001D6EF File Offset: 0x0001B8EF
		[Obsolete("Use RenderSettings.ambientIntensity instead (UnityUpgradable) -> ambientIntensity", false)]
		public static float ambientSkyboxAmount
		{
			get
			{
				return RenderSettings.ambientIntensity;
			}
			set
			{
				RenderSettings.ambientIntensity = value;
			}
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x00008C6F File Offset: 0x00006E6F
		private RenderSettings()
		{
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x060010DA RID: 4314
		// (set) Token: 0x060010DB RID: 4315
		[NativeProperty("UseFog")]
		public static extern bool fog
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x060010DC RID: 4316
		// (set) Token: 0x060010DD RID: 4317
		[NativeProperty("LinearFogStart")]
		public static extern float fogStartDistance
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x060010DE RID: 4318
		// (set) Token: 0x060010DF RID: 4319
		[NativeProperty("LinearFogEnd")]
		public static extern float fogEndDistance
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x060010E0 RID: 4320
		// (set) Token: 0x060010E1 RID: 4321
		public static extern FogMode fogMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x060010E2 RID: 4322 RVA: 0x0001D6FC File Offset: 0x0001B8FC
		// (set) Token: 0x060010E3 RID: 4323 RVA: 0x0001D714 File Offset: 0x0001B914
		public static Color fogColor
		{
			get
			{
				Color color;
				RenderSettings.get_fogColor_Injected(out color);
				return color;
			}
			set
			{
				RenderSettings.set_fogColor_Injected(ref value);
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x060010E4 RID: 4324
		// (set) Token: 0x060010E5 RID: 4325
		public static extern float fogDensity
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x060010E6 RID: 4326
		// (set) Token: 0x060010E7 RID: 4327
		public static extern AmbientMode ambientMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x060010E8 RID: 4328 RVA: 0x0001D728 File Offset: 0x0001B928
		// (set) Token: 0x060010E9 RID: 4329 RVA: 0x0001D740 File Offset: 0x0001B940
		public static Color ambientSkyColor
		{
			get
			{
				Color color;
				RenderSettings.get_ambientSkyColor_Injected(out color);
				return color;
			}
			set
			{
				RenderSettings.set_ambientSkyColor_Injected(ref value);
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x060010EA RID: 4330 RVA: 0x0001D754 File Offset: 0x0001B954
		// (set) Token: 0x060010EB RID: 4331 RVA: 0x0001D76C File Offset: 0x0001B96C
		public static Color ambientEquatorColor
		{
			get
			{
				Color color;
				RenderSettings.get_ambientEquatorColor_Injected(out color);
				return color;
			}
			set
			{
				RenderSettings.set_ambientEquatorColor_Injected(ref value);
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x060010EC RID: 4332 RVA: 0x0001D780 File Offset: 0x0001B980
		// (set) Token: 0x060010ED RID: 4333 RVA: 0x0001D798 File Offset: 0x0001B998
		public static Color ambientGroundColor
		{
			get
			{
				Color color;
				RenderSettings.get_ambientGroundColor_Injected(out color);
				return color;
			}
			set
			{
				RenderSettings.set_ambientGroundColor_Injected(ref value);
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x060010EE RID: 4334
		// (set) Token: 0x060010EF RID: 4335
		public static extern float ambientIntensity
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x060010F0 RID: 4336 RVA: 0x0001D7AC File Offset: 0x0001B9AC
		// (set) Token: 0x060010F1 RID: 4337 RVA: 0x0001D7C4 File Offset: 0x0001B9C4
		[NativeProperty("AmbientSkyColor")]
		public static Color ambientLight
		{
			get
			{
				Color color;
				RenderSettings.get_ambientLight_Injected(out color);
				return color;
			}
			set
			{
				RenderSettings.set_ambientLight_Injected(ref value);
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x060010F2 RID: 4338 RVA: 0x0001D7D8 File Offset: 0x0001B9D8
		// (set) Token: 0x060010F3 RID: 4339 RVA: 0x0001D7F0 File Offset: 0x0001B9F0
		public static Color subtractiveShadowColor
		{
			get
			{
				Color color;
				RenderSettings.get_subtractiveShadowColor_Injected(out color);
				return color;
			}
			set
			{
				RenderSettings.set_subtractiveShadowColor_Injected(ref value);
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x060010F4 RID: 4340 RVA: 0x0001D804 File Offset: 0x0001BA04
		// (set) Token: 0x060010F5 RID: 4341 RVA: 0x0001D81C File Offset: 0x0001BA1C
		[NativeProperty("SkyboxMaterial")]
		public static Material skybox
		{
			get
			{
				return Unmarshal.UnmarshalUnityObject<Material>(RenderSettings.get_skybox_Injected());
			}
			set
			{
				RenderSettings.set_skybox_Injected(Object.MarshalledUnityObject.Marshal<Material>(value));
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x060010F6 RID: 4342 RVA: 0x0001D834 File Offset: 0x0001BA34
		// (set) Token: 0x060010F7 RID: 4343 RVA: 0x0001D84C File Offset: 0x0001BA4C
		public static Light sun
		{
			get
			{
				return Unmarshal.UnmarshalUnityObject<Light>(RenderSettings.get_sun_Injected());
			}
			set
			{
				RenderSettings.set_sun_Injected(Object.MarshalledUnityObject.Marshal<Light>(value));
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x060010F8 RID: 4344 RVA: 0x0001D864 File Offset: 0x0001BA64
		// (set) Token: 0x060010F9 RID: 4345 RVA: 0x0001D87C File Offset: 0x0001BA7C
		public static SphericalHarmonicsL2 ambientProbe
		{
			[NativeMethod("GetFinalAmbientProbe")]
			get
			{
				SphericalHarmonicsL2 sphericalHarmonicsL;
				RenderSettings.get_ambientProbe_Injected(out sphericalHarmonicsL);
				return sphericalHarmonicsL;
			}
			set
			{
				RenderSettings.set_ambientProbe_Injected(ref value);
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x060010FA RID: 4346 RVA: 0x0001D890 File Offset: 0x0001BA90
		// (set) Token: 0x060010FB RID: 4347 RVA: 0x0001D8C5 File Offset: 0x0001BAC5
		[Obsolete("RenderSettings.customReflection has been deprecated in favor of RenderSettings.customReflectionTexture.", false)]
		public static Cubemap customReflection
		{
			get
			{
				Cubemap cubemap = RenderSettings.customReflectionTexture as Cubemap;
				bool flag = cubemap == null;
				if (flag)
				{
					throw new ArgumentException("RenderSettings.customReflection is currently not referencing a cubemap.");
				}
				return cubemap;
			}
			[NativeThrows]
			set
			{
				RenderSettings.customReflectionTexture = value;
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x060010FC RID: 4348 RVA: 0x0001D8D0 File Offset: 0x0001BAD0
		// (set) Token: 0x060010FD RID: 4349 RVA: 0x0001D8E8 File Offset: 0x0001BAE8
		[NativeProperty("CustomReflection")]
		public static Texture customReflectionTexture
		{
			get
			{
				return Unmarshal.UnmarshalUnityObject<Texture>(RenderSettings.get_customReflectionTexture_Injected());
			}
			[NativeThrows]
			set
			{
				RenderSettings.set_customReflectionTexture_Injected(Object.MarshalledUnityObject.Marshal<Texture>(value));
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x060010FE RID: 4350
		// (set) Token: 0x060010FF RID: 4351
		public static extern float reflectionIntensity
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06001100 RID: 4352
		// (set) Token: 0x06001101 RID: 4353
		public static extern int reflectionBounces
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06001102 RID: 4354 RVA: 0x0001D900 File Offset: 0x0001BB00
		[NativeProperty("GeneratedSkyboxReflection")]
		internal static Cubemap defaultReflection
		{
			get
			{
				return Unmarshal.UnmarshalUnityObject<Cubemap>(RenderSettings.get_defaultReflection_Injected());
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06001103 RID: 4355
		// (set) Token: 0x06001104 RID: 4356
		public static extern DefaultReflectionMode defaultReflectionMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06001105 RID: 4357
		// (set) Token: 0x06001106 RID: 4358
		public static extern int defaultReflectionResolution
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06001107 RID: 4359
		// (set) Token: 0x06001108 RID: 4360
		public static extern float haloStrength
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06001109 RID: 4361
		// (set) Token: 0x0600110A RID: 4362
		public static extern float flareStrength
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x0600110B RID: 4363
		// (set) Token: 0x0600110C RID: 4364
		public static extern float flareFadeSpeed
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x0001D918 File Offset: 0x0001BB18
		[FreeFunction("GetRenderSettings")]
		internal static Object GetRenderSettings()
		{
			return Unmarshal.UnmarshalUnityObject<Object>(RenderSettings.GetRenderSettings_Injected());
		}

		// Token: 0x0600110E RID: 4366
		[StaticAccessor("RenderSettingsScripting", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Reset();

		// Token: 0x0600110F RID: 4367
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_fogColor_Injected(out Color ret);

		// Token: 0x06001110 RID: 4368
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_fogColor_Injected([In] ref Color value);

		// Token: 0x06001111 RID: 4369
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_ambientSkyColor_Injected(out Color ret);

		// Token: 0x06001112 RID: 4370
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_ambientSkyColor_Injected([In] ref Color value);

		// Token: 0x06001113 RID: 4371
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_ambientEquatorColor_Injected(out Color ret);

		// Token: 0x06001114 RID: 4372
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_ambientEquatorColor_Injected([In] ref Color value);

		// Token: 0x06001115 RID: 4373
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_ambientGroundColor_Injected(out Color ret);

		// Token: 0x06001116 RID: 4374
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_ambientGroundColor_Injected([In] ref Color value);

		// Token: 0x06001117 RID: 4375
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_ambientLight_Injected(out Color ret);

		// Token: 0x06001118 RID: 4376
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_ambientLight_Injected([In] ref Color value);

		// Token: 0x06001119 RID: 4377
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_subtractiveShadowColor_Injected(out Color ret);

		// Token: 0x0600111A RID: 4378
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_subtractiveShadowColor_Injected([In] ref Color value);

		// Token: 0x0600111B RID: 4379
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_skybox_Injected();

		// Token: 0x0600111C RID: 4380
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_skybox_Injected(IntPtr value);

		// Token: 0x0600111D RID: 4381
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_sun_Injected();

		// Token: 0x0600111E RID: 4382
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_sun_Injected(IntPtr value);

		// Token: 0x0600111F RID: 4383
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_ambientProbe_Injected(out SphericalHarmonicsL2 ret);

		// Token: 0x06001120 RID: 4384
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_ambientProbe_Injected([In] ref SphericalHarmonicsL2 value);

		// Token: 0x06001121 RID: 4385
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_customReflectionTexture_Injected();

		// Token: 0x06001122 RID: 4386
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_customReflectionTexture_Injected(IntPtr value);

		// Token: 0x06001123 RID: 4387
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_defaultReflection_Injected();

		// Token: 0x06001124 RID: 4388
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetRenderSettings_Injected();
	}
}
