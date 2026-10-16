using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001F1 RID: 497
	[NativeHeader("Runtime/Graphics/Texture.h")]
	[UsedByNativeCode]
	[NativeHeader("Runtime/Streaming/TextureStreamingManager.h")]
	public class Texture : Object
	{
		// Token: 0x06001783 RID: 6019 RVA: 0x00008C6F File Offset: 0x00006E6F
		protected Texture()
		{
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06001784 RID: 6020
		// (set) Token: 0x06001785 RID: 6021
		[Obsolete("masterTextureLimit has been deprecated. Use globalMipmapLimit instead (UnityUpgradable) -> globalMipmapLimit", false)]
		[NativeProperty("ActiveGlobalMipmapLimit")]
		public static extern int masterTextureLimit
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06001786 RID: 6022
		// (set) Token: 0x06001787 RID: 6023
		[Obsolete("globalMipmapLimit is not supported. Use QualitySettings.globalTextureMipmapLimit or Mipmap Limit Groups instead.", false)]
		[NativeProperty("ActiveGlobalMipmapLimit")]
		public static extern int globalMipmapLimit
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06001788 RID: 6024 RVA: 0x00028A14 File Offset: 0x00026C14
		public int mipmapCount
		{
			[NativeName("GetMipmapCount")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Texture.get_mipmapCount_Injected(intPtr);
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06001789 RID: 6025
		// (set) Token: 0x0600178A RID: 6026
		[NativeProperty("AnisoLimit")]
		public static extern AnisotropicFiltering anisotropicFiltering
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x0600178B RID: 6027
		[NativeName("SetGlobalAnisoLimits")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetGlobalAnisotropicFilteringLimits(int forcedMin, int globalMax);

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x0600178C RID: 6028 RVA: 0x00028A38 File Offset: 0x00026C38
		public virtual GraphicsFormat graphicsFormat
		{
			get
			{
				return GraphicsFormatUtility.GetFormat(this);
			}
		}

		// Token: 0x0600178D RID: 6029 RVA: 0x00028A50 File Offset: 0x00026C50
		[ThreadSafe]
		private int GetDataWidth()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Texture.GetDataWidth_Injected(intPtr);
		}

		// Token: 0x0600178E RID: 6030 RVA: 0x00028A74 File Offset: 0x00026C74
		[ThreadSafe]
		private int GetDataHeight()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Texture.GetDataHeight_Injected(intPtr);
		}

		// Token: 0x0600178F RID: 6031 RVA: 0x00028A98 File Offset: 0x00026C98
		[ThreadSafe]
		private TextureDimension GetDimension()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Texture.GetDimension_Injected(intPtr);
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06001790 RID: 6032 RVA: 0x00028ABC File Offset: 0x00026CBC
		// (set) Token: 0x06001791 RID: 6033 RVA: 0x00028AD4 File Offset: 0x00026CD4
		public virtual int width
		{
			get
			{
				return this.GetDataWidth();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06001792 RID: 6034 RVA: 0x00028ADC File Offset: 0x00026CDC
		// (set) Token: 0x06001793 RID: 6035 RVA: 0x00028AD4 File Offset: 0x00026CD4
		public virtual int height
		{
			get
			{
				return this.GetDataHeight();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06001794 RID: 6036 RVA: 0x00028AF4 File Offset: 0x00026CF4
		// (set) Token: 0x06001795 RID: 6037 RVA: 0x00028AD4 File Offset: 0x00026CD4
		public virtual TextureDimension dimension
		{
			get
			{
				return this.GetDimension();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06001796 RID: 6038 RVA: 0x00028B0C File Offset: 0x00026D0C
		internal bool isNativeTexture
		{
			[NativeName("IsNativeTexture")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Texture.get_isNativeTexture_Injected(intPtr);
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06001797 RID: 6039 RVA: 0x00028B30 File Offset: 0x00026D30
		public virtual bool isReadable
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Texture.get_isReadable_Injected(intPtr);
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06001798 RID: 6040 RVA: 0x00028B54 File Offset: 0x00026D54
		// (set) Token: 0x06001799 RID: 6041 RVA: 0x00028B78 File Offset: 0x00026D78
		public TextureWrapMode wrapMode
		{
			[NativeName("GetWrapModeU")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Texture.get_wrapMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Texture.set_wrapMode_Injected(intPtr, value);
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x0600179A RID: 6042 RVA: 0x00028B9C File Offset: 0x00026D9C
		// (set) Token: 0x0600179B RID: 6043 RVA: 0x00028BC0 File Offset: 0x00026DC0
		public TextureWrapMode wrapModeU
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Texture.get_wrapModeU_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Texture.set_wrapModeU_Injected(intPtr, value);
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x0600179C RID: 6044 RVA: 0x00028BE4 File Offset: 0x00026DE4
		// (set) Token: 0x0600179D RID: 6045 RVA: 0x00028C08 File Offset: 0x00026E08
		public TextureWrapMode wrapModeV
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Texture.get_wrapModeV_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Texture.set_wrapModeV_Injected(intPtr, value);
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x0600179E RID: 6046 RVA: 0x00028C2C File Offset: 0x00026E2C
		// (set) Token: 0x0600179F RID: 6047 RVA: 0x00028C50 File Offset: 0x00026E50
		public TextureWrapMode wrapModeW
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Texture.get_wrapModeW_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Texture.set_wrapModeW_Injected(intPtr, value);
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x060017A0 RID: 6048 RVA: 0x00028C74 File Offset: 0x00026E74
		// (set) Token: 0x060017A1 RID: 6049 RVA: 0x00028C98 File Offset: 0x00026E98
		public FilterMode filterMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Texture.get_filterMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Texture.set_filterMode_Injected(intPtr, value);
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x060017A2 RID: 6050 RVA: 0x00028CBC File Offset: 0x00026EBC
		// (set) Token: 0x060017A3 RID: 6051 RVA: 0x00028CE0 File Offset: 0x00026EE0
		public int anisoLevel
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Texture.get_anisoLevel_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Texture.set_anisoLevel_Injected(intPtr, value);
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x060017A4 RID: 6052 RVA: 0x00028D04 File Offset: 0x00026F04
		// (set) Token: 0x060017A5 RID: 6053 RVA: 0x00028D28 File Offset: 0x00026F28
		public float mipMapBias
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Texture.get_mipMapBias_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Texture.set_mipMapBias_Injected(intPtr, value);
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x060017A6 RID: 6054 RVA: 0x00028D4C File Offset: 0x00026F4C
		public Vector2 texelSize
		{
			[NativeName("GetTexelSize")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				Texture.get_texelSize_Injected(intPtr, out vector);
				return vector;
			}
		}

		// Token: 0x060017A7 RID: 6055 RVA: 0x00028D74 File Offset: 0x00026F74
		public IntPtr GetNativeTexturePtr()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Texture.GetNativeTexturePtr_Injected(intPtr);
		}

		// Token: 0x060017A8 RID: 6056 RVA: 0x00028D98 File Offset: 0x00026F98
		[Obsolete("Use GetNativeTexturePtr instead.", false)]
		public int GetNativeTextureID()
		{
			return (int)this.GetNativeTexturePtr();
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x060017A9 RID: 6057 RVA: 0x00028DB8 File Offset: 0x00026FB8
		public uint updateCount
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Texture.get_updateCount_Injected(intPtr);
			}
		}

		// Token: 0x060017AA RID: 6058 RVA: 0x00028DDC File Offset: 0x00026FDC
		public void IncrementUpdateCount()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Texture.IncrementUpdateCount_Injected(intPtr);
		}

		// Token: 0x060017AB RID: 6059 RVA: 0x00028E00 File Offset: 0x00027000
		[NativeMethod("GetActiveTextureColorSpace")]
		private int Internal_GetActiveTextureColorSpace()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Texture.Internal_GetActiveTextureColorSpace_Injected(intPtr);
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x060017AC RID: 6060 RVA: 0x00028E24 File Offset: 0x00027024
		internal ColorSpace activeTextureColorSpace
		{
			[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule", "Unity.UIElements" })]
			get
			{
				return (this.Internal_GetActiveTextureColorSpace() == 0) ? ColorSpace.Linear : ColorSpace.Gamma;
			}
		}

		// Token: 0x060017AD RID: 6061 RVA: 0x00028E44 File Offset: 0x00027044
		[NativeMethod("GetStoredColorSpace")]
		private TextureColorSpace Internal_GetStoredColorSpace()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Texture.Internal_GetStoredColorSpace_Injected(intPtr);
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x060017AE RID: 6062 RVA: 0x00028E68 File Offset: 0x00027068
		public bool isDataSRGB
		{
			get
			{
				return this.Internal_GetStoredColorSpace() == TextureColorSpace.sRGB;
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x060017AF RID: 6063
		public static extern ulong totalTextureMemory
		{
			[FreeFunction("GetTextureStreamingManager().GetTotalTextureMemory")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x060017B0 RID: 6064
		public static extern ulong desiredTextureMemory
		{
			[FreeFunction("GetTextureStreamingManager().GetDesiredTextureMemory")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x060017B1 RID: 6065
		public static extern ulong targetTextureMemory
		{
			[FreeFunction("GetTextureStreamingManager().GetTargetTextureMemory")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x060017B2 RID: 6066
		public static extern ulong currentTextureMemory
		{
			[FreeFunction("GetTextureStreamingManager().GetCurrentTextureMemory")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x060017B3 RID: 6067
		public static extern ulong nonStreamingTextureMemory
		{
			[FreeFunction("GetTextureStreamingManager().GetNonStreamingTextureMemory")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x060017B4 RID: 6068
		public static extern ulong streamingMipmapUploadCount
		{
			[FreeFunction("GetTextureStreamingManager().GetStreamingMipmapUploadCount")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x060017B5 RID: 6069
		public static extern ulong streamingRendererCount
		{
			[FreeFunction("GetTextureStreamingManager().GetStreamingRendererCount")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x060017B6 RID: 6070
		public static extern ulong streamingTextureCount
		{
			[FreeFunction("GetTextureStreamingManager().GetStreamingTextureCount")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x060017B7 RID: 6071
		public static extern ulong nonStreamingTextureCount
		{
			[FreeFunction("GetTextureStreamingManager().GetNonStreamingTextureCount")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x060017B8 RID: 6072
		public static extern ulong streamingTexturePendingLoadCount
		{
			[FreeFunction("GetTextureStreamingManager().GetStreamingTexturePendingLoadCount")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x060017B9 RID: 6073
		public static extern ulong streamingTextureLoadingCount
		{
			[FreeFunction("GetTextureStreamingManager().GetStreamingTextureLoadingCount")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x060017BA RID: 6074
		[FreeFunction("GetTextureStreamingManager().SetStreamingTextureMaterialDebugProperties")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetStreamingTextureMaterialDebugProperties();

		// Token: 0x060017BB RID: 6075
		[FreeFunction("GetTextureStreamingManager().SetStreamingTextureMaterialDebugPropertiesWithSlot")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetStreamingTextureMaterialDebugPropertiesWithSlot(int materialTextureSlot);

		// Token: 0x060017BC RID: 6076 RVA: 0x00028E83 File Offset: 0x00027083
		public static void SetStreamingTextureMaterialDebugProperties(int materialTextureSlot)
		{
			Texture.SetStreamingTextureMaterialDebugPropertiesWithSlot(materialTextureSlot);
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x060017BD RID: 6077
		// (set) Token: 0x060017BE RID: 6078
		public static extern bool streamingTextureForceLoadAll
		{
			[FreeFunction(Name = "GetTextureStreamingManager().GetForceLoadAll")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction(Name = "GetTextureStreamingManager().SetForceLoadAll")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x060017BF RID: 6079
		// (set) Token: 0x060017C0 RID: 6080
		public static extern bool streamingTextureDiscardUnusedMips
		{
			[FreeFunction(Name = "GetTextureStreamingManager().GetDiscardUnusedMips")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction(Name = "GetTextureStreamingManager().SetDiscardUnusedMips")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x060017C1 RID: 6081
		// (set) Token: 0x060017C2 RID: 6082
		public static extern bool allowThreadedTextureCreation
		{
			[FreeFunction(Name = "Texture2DScripting::IsCreateTextureThreadedEnabled")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction(Name = "Texture2DScripting::EnableCreateTextureThreaded")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x060017C3 RID: 6083 RVA: 0x00028E90 File Offset: 0x00027090
		internal ulong GetPixelDataSize(int mipLevel, int element = 0)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Texture.GetPixelDataSize_Injected(intPtr, mipLevel, element);
		}

		// Token: 0x060017C4 RID: 6084 RVA: 0x00028EB4 File Offset: 0x000270B4
		internal ulong GetPixelDataOffset(int mipLevel, int element = 0)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Texture.GetPixelDataOffset_Injected(intPtr, mipLevel, element);
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x060017C5 RID: 6085 RVA: 0x00028ED8 File Offset: 0x000270D8
		public GraphicsTexture graphicsTexture
		{
			[FreeFunction(Name = "Texture2DScripting::GetCurrentGraphicsTexture", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				IntPtr intPtr2 = Texture.get_graphicsTexture_Injected(intPtr);
				return (intPtr2 == 0) ? null : GraphicsTexture.BindingsMarshaller.ConvertToManaged(intPtr2);
			}
		}

		// Token: 0x060017C6 RID: 6086 RVA: 0x00028F08 File Offset: 0x00027108
		internal TextureColorSpace GetTextureColorSpace(bool linear)
		{
			return linear ? TextureColorSpace.Linear : TextureColorSpace.sRGB;
		}

		// Token: 0x060017C7 RID: 6087 RVA: 0x00028F24 File Offset: 0x00027124
		internal TextureColorSpace GetTextureColorSpace(GraphicsFormat format)
		{
			return this.GetTextureColorSpace(!GraphicsFormatUtility.IsSRGBFormat(format));
		}

		// Token: 0x060017C8 RID: 6088 RVA: 0x00028F48 File Offset: 0x00027148
		internal bool ValidateFormat(RenderTextureFormat format)
		{
			bool flag = SystemInfo.SupportsRenderTextureFormat(format);
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				Debug.LogError(string.Format("RenderTexture creation failed. '{0}' is not supported on this platform. Use 'SystemInfo.SupportsRenderTextureFormat' C# API to check format support.", format.ToString()), this);
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x060017C9 RID: 6089 RVA: 0x00028F8C File Offset: 0x0002718C
		internal bool ValidateFormat(TextureFormat format)
		{
			bool flag = SystemInfo.SupportsTextureFormat(format);
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				bool flag3 = GraphicsFormatUtility.IsCompressedFormat(format) && GraphicsFormatUtility.CanDecompressFormat(GraphicsFormatUtility.GetGraphicsFormat(format, false));
				if (flag3)
				{
					Debug.LogWarning(string.Format("'{0}' is not supported on this platform. Decompressing texture. Use 'SystemInfo.SupportsTextureFormat' C# API to check format support.", format.ToString()), this);
					flag2 = true;
				}
				else
				{
					Debug.LogError(string.Format("Texture creation failed. '{0}' is not supported on this platform. Use 'SystemInfo.SupportsTextureFormat' C# API to check format support.", format.ToString()), this);
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x060017CA RID: 6090 RVA: 0x0002900C File Offset: 0x0002720C
		internal bool ValidateFormat(GraphicsFormat format, GraphicsFormatUsage usage)
		{
			bool flag = SystemInfo.IsFormatSupported(format, usage);
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				Debug.LogError(string.Format("Texture creation failed. '{0}' is not supported for {1} usage on this platform. Use 'SystemInfo.IsFormatSupported' C# API to check format support.", format.ToString(), usage.ToString()), this);
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x060017CB RID: 6091 RVA: 0x0002905C File Offset: 0x0002725C
		internal UnityException CreateNonReadableException(Texture t)
		{
			return new UnityException(string.Format("Texture '{0}' is not readable, the texture memory can not be accessed from scripts. You can make the texture readable in the Texture Import Settings.", t.name));
		}

		// Token: 0x060017CC RID: 6092 RVA: 0x00029084 File Offset: 0x00027284
		internal UnityException IgnoreMipmapLimitCannotBeToggledException(Texture t)
		{
			return new UnityException(string.Format("Failed to toggle ignoreMipmapLimit, Texture '{0}' is not readable. You can make the texture readable in the Texture Import Settings.", t.name));
		}

		// Token: 0x060017CD RID: 6093 RVA: 0x000290AC File Offset: 0x000272AC
		internal UnityException CreateNativeArrayLengthOverflowException()
		{
			return new UnityException("Failed to create NativeArray, length exceeds the allowed maximum of Int32.MaxValue. Use a larger type as template argument to reduce the array length.");
		}

		// Token: 0x060017CF RID: 6095
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_mipmapCount_Injected(IntPtr _unity_self);

		// Token: 0x060017D0 RID: 6096
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetDataWidth_Injected(IntPtr _unity_self);

		// Token: 0x060017D1 RID: 6097
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetDataHeight_Injected(IntPtr _unity_self);

		// Token: 0x060017D2 RID: 6098
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern TextureDimension GetDimension_Injected(IntPtr _unity_self);

		// Token: 0x060017D3 RID: 6099
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isNativeTexture_Injected(IntPtr _unity_self);

		// Token: 0x060017D4 RID: 6100
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isReadable_Injected(IntPtr _unity_self);

		// Token: 0x060017D5 RID: 6101
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern TextureWrapMode get_wrapMode_Injected(IntPtr _unity_self);

		// Token: 0x060017D6 RID: 6102
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_wrapMode_Injected(IntPtr _unity_self, TextureWrapMode value);

		// Token: 0x060017D7 RID: 6103
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern TextureWrapMode get_wrapModeU_Injected(IntPtr _unity_self);

		// Token: 0x060017D8 RID: 6104
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_wrapModeU_Injected(IntPtr _unity_self, TextureWrapMode value);

		// Token: 0x060017D9 RID: 6105
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern TextureWrapMode get_wrapModeV_Injected(IntPtr _unity_self);

		// Token: 0x060017DA RID: 6106
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_wrapModeV_Injected(IntPtr _unity_self, TextureWrapMode value);

		// Token: 0x060017DB RID: 6107
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern TextureWrapMode get_wrapModeW_Injected(IntPtr _unity_self);

		// Token: 0x060017DC RID: 6108
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_wrapModeW_Injected(IntPtr _unity_self, TextureWrapMode value);

		// Token: 0x060017DD RID: 6109
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern FilterMode get_filterMode_Injected(IntPtr _unity_self);

		// Token: 0x060017DE RID: 6110
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_filterMode_Injected(IntPtr _unity_self, FilterMode value);

		// Token: 0x060017DF RID: 6111
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_anisoLevel_Injected(IntPtr _unity_self);

		// Token: 0x060017E0 RID: 6112
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_anisoLevel_Injected(IntPtr _unity_self, int value);

		// Token: 0x060017E1 RID: 6113
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_mipMapBias_Injected(IntPtr _unity_self);

		// Token: 0x060017E2 RID: 6114
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_mipMapBias_Injected(IntPtr _unity_self, float value);

		// Token: 0x060017E3 RID: 6115
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_texelSize_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x060017E4 RID: 6116
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetNativeTexturePtr_Injected(IntPtr _unity_self);

		// Token: 0x060017E5 RID: 6117
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint get_updateCount_Injected(IntPtr _unity_self);

		// Token: 0x060017E6 RID: 6118
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void IncrementUpdateCount_Injected(IntPtr _unity_self);

		// Token: 0x060017E7 RID: 6119
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_GetActiveTextureColorSpace_Injected(IntPtr _unity_self);

		// Token: 0x060017E8 RID: 6120
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern TextureColorSpace Internal_GetStoredColorSpace_Injected(IntPtr _unity_self);

		// Token: 0x060017E9 RID: 6121
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ulong GetPixelDataSize_Injected(IntPtr _unity_self, int mipLevel, int element);

		// Token: 0x060017EA RID: 6122
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ulong GetPixelDataOffset_Injected(IntPtr _unity_self, int mipLevel, int element);

		// Token: 0x060017EB RID: 6123
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_graphicsTexture_Injected(IntPtr _unity_self);

		// Token: 0x040006DC RID: 1756
		public static readonly int GenerateAllMips = -1;
	}
}
