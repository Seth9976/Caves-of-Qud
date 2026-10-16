using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000172 RID: 370
	[NativeHeader("Runtime/Graphics/LightmapSettings.h")]
	[StaticAccessor("GetLightmapSettings()")]
	public sealed class LightmapSettings : Object
	{
		// Token: 0x06000D41 RID: 3393 RVA: 0x00008C6F File Offset: 0x00006E6F
		private LightmapSettings()
		{
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000D42 RID: 3394
		// (set) Token: 0x06000D43 RID: 3395
		public static extern LightmapData[] lightmaps
		{
			[FreeFunction]
			[MethodImpl(MethodImplOptions.InternalCall)]
			[return: Unmarshalled]
			get;
			[FreeFunction(ThrowsException = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			[param: Unmarshalled]
			set;
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000D44 RID: 3396
		// (set) Token: 0x06000D45 RID: 3397
		public static extern LightmapsMode lightmapsMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction(ThrowsException = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000D46 RID: 3398 RVA: 0x00018D50 File Offset: 0x00016F50
		// (set) Token: 0x06000D47 RID: 3399 RVA: 0x00018D68 File Offset: 0x00016F68
		public static LightProbes lightProbes
		{
			get
			{
				return Unmarshal.UnmarshalUnityObject<LightProbes>(LightmapSettings.get_lightProbes_Injected());
			}
			[NativeName("SetLightProbes")]
			[FreeFunction]
			set
			{
				LightmapSettings.set_lightProbes_Injected(Object.MarshalledUnityObject.Marshal<LightProbes>(value));
			}
		}

		// Token: 0x06000D48 RID: 3400
		[NativeName("ResetAndAwakeFromLoad")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Reset();

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000D49 RID: 3401 RVA: 0x00018D80 File Offset: 0x00016F80
		// (set) Token: 0x06000D4A RID: 3402 RVA: 0x00002925 File Offset: 0x00000B25
		[Obsolete("Use lightmapsMode instead.", false)]
		public static LightmapsModeLegacy lightmapsModeLegacy
		{
			get
			{
				return LightmapsModeLegacy.Single;
			}
			set
			{
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000D4B RID: 3403 RVA: 0x00018D94 File Offset: 0x00016F94
		// (set) Token: 0x06000D4C RID: 3404 RVA: 0x00002925 File Offset: 0x00000B25
		[Obsolete("Use QualitySettings.desiredColorSpace instead.", false)]
		public static ColorSpace bakedColorSpace
		{
			get
			{
				return QualitySettings.desiredColorSpace;
			}
			set
			{
			}
		}

		// Token: 0x06000D4D RID: 3405
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_lightProbes_Injected();

		// Token: 0x06000D4E RID: 3406
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_lightProbes_Injected(IntPtr value);
	}
}
