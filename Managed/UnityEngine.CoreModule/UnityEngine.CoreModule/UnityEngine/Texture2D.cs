using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001F2 RID: 498
	[NativeHeader("Runtime/Graphics/Texture2D.h")]
	[HelpURL("texture-type-default")]
	[NativeHeader("Runtime/Graphics/GeneratedTextures.h")]
	[UsedByNativeCode]
	[ExcludeFromPreset]
	public sealed class Texture2D : Texture
	{
		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x060017EC RID: 6124 RVA: 0x000290D0 File Offset: 0x000272D0
		public TextureFormat format
		{
			[NativeName("GetTextureFormat")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Texture2D.get_format_Injected(intPtr);
			}
		}

		// Token: 0x060017ED RID: 6125 RVA: 0x000290F4 File Offset: 0x000272F4
		private bool IgnoreMipmapLimit()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Texture2D.IgnoreMipmapLimit_Injected(intPtr);
		}

		// Token: 0x060017EE RID: 6126 RVA: 0x00029118 File Offset: 0x00027318
		private void SetIgnoreMipmapLimitAndReload(bool value)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Texture2D.SetIgnoreMipmapLimitAndReload_Injected(intPtr, value);
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x060017EF RID: 6127 RVA: 0x0002913C File Offset: 0x0002733C
		public string mipmapLimitGroup
		{
			[NativeName("GetMipmapLimitGroupName")]
			get
			{
				string stringAndDispose;
				try
				{
					IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					ManagedSpanWrapper managedSpanWrapper;
					Texture2D.get_mipmapLimitGroup_Injected(intPtr, out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x060017F0 RID: 6128 RVA: 0x0002917C File Offset: 0x0002737C
		public int activeMipmapLimit
		{
			[NativeName("GetMipmapLimit")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Texture2D.get_activeMipmapLimit_Injected(intPtr);
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x060017F1 RID: 6129 RVA: 0x000291A0 File Offset: 0x000273A0
		[StaticAccessor("builtintex", StaticAccessorType.DoubleColon)]
		public static Texture2D whiteTexture
		{
			get
			{
				return Unmarshal.UnmarshalUnityObject<Texture2D>(Texture2D.get_whiteTexture_Injected());
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x060017F2 RID: 6130 RVA: 0x000291B8 File Offset: 0x000273B8
		[StaticAccessor("builtintex", StaticAccessorType.DoubleColon)]
		public static Texture2D blackTexture
		{
			get
			{
				return Unmarshal.UnmarshalUnityObject<Texture2D>(Texture2D.get_blackTexture_Injected());
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x060017F3 RID: 6131 RVA: 0x000291D0 File Offset: 0x000273D0
		[StaticAccessor("builtintex", StaticAccessorType.DoubleColon)]
		public static Texture2D redTexture
		{
			get
			{
				return Unmarshal.UnmarshalUnityObject<Texture2D>(Texture2D.get_redTexture_Injected());
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x060017F4 RID: 6132 RVA: 0x000291E8 File Offset: 0x000273E8
		[StaticAccessor("builtintex", StaticAccessorType.DoubleColon)]
		public static Texture2D grayTexture
		{
			get
			{
				return Unmarshal.UnmarshalUnityObject<Texture2D>(Texture2D.get_grayTexture_Injected());
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x060017F5 RID: 6133 RVA: 0x00029200 File Offset: 0x00027400
		[StaticAccessor("builtintex", StaticAccessorType.DoubleColon)]
		public static Texture2D linearGrayTexture
		{
			get
			{
				return Unmarshal.UnmarshalUnityObject<Texture2D>(Texture2D.get_linearGrayTexture_Injected());
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x060017F6 RID: 6134 RVA: 0x00029218 File Offset: 0x00027418
		[StaticAccessor("builtintex", StaticAccessorType.DoubleColon)]
		public static Texture2D normalTexture
		{
			get
			{
				return Unmarshal.UnmarshalUnityObject<Texture2D>(Texture2D.get_normalTexture_Injected());
			}
		}

		// Token: 0x060017F7 RID: 6135 RVA: 0x00029230 File Offset: 0x00027430
		public void Compress(bool highQuality)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Texture2D.Compress_Injected(intPtr, highQuality);
		}

		// Token: 0x060017F8 RID: 6136
		[FreeFunction("Texture2DScripting::CreateEmpty")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Internal_CreateEmptyImpl([Writable] Texture2D mono);

		// Token: 0x060017F9 RID: 6137 RVA: 0x00029254 File Offset: 0x00027454
		[FreeFunction("Texture2DScripting::Create")]
		private unsafe static bool Internal_CreateImpl([Writable] Texture2D mono, int w, int h, int mipCount, GraphicsFormat format, TextureColorSpace colorSpace, TextureCreationFlags flags, IntPtr nativeTex, bool ignoreMipmapLimit, string mipmapLimitGroupName)
		{
			bool flag;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(mipmapLimitGroupName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = mipmapLimitGroupName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				flag = Texture2D.Internal_CreateImpl_Injected(mono, w, h, mipCount, format, colorSpace, flags, nativeTex, ignoreMipmapLimit, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x060017FA RID: 6138 RVA: 0x000292BC File Offset: 0x000274BC
		private static void Internal_Create([Writable] Texture2D mono, int w, int h, int mipCount, GraphicsFormat format, TextureColorSpace colorSpace, TextureCreationFlags flags, IntPtr nativeTex, bool ignoreMipmapLimit, string mipmapLimitGroupName)
		{
			bool flag = !Texture2D.Internal_CreateImpl(mono, w, h, mipCount, format, colorSpace, flags, nativeTex, ignoreMipmapLimit, mipmapLimitGroupName);
			if (flag)
			{
				throw new UnityException("Failed to create texture because of invalid parameters.");
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x060017FB RID: 6139 RVA: 0x000292F4 File Offset: 0x000274F4
		public override bool isReadable
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Texture2D.get_isReadable_Injected(intPtr);
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x060017FC RID: 6140 RVA: 0x00029318 File Offset: 0x00027518
		[NativeConditional("ENABLE_VIRTUALTEXTURING && UNITY_EDITOR")]
		[NativeName("VTOnly")]
		public bool vtOnly
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Texture2D.get_vtOnly_Injected(intPtr);
			}
		}

		// Token: 0x060017FD RID: 6141 RVA: 0x0002933C File Offset: 0x0002753C
		[NativeName("Apply")]
		private void ApplyImpl(bool updateMipmaps, bool makeNoLongerReadable)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Texture2D.ApplyImpl_Injected(intPtr, updateMipmaps, makeNoLongerReadable);
		}

		// Token: 0x060017FE RID: 6142 RVA: 0x00029360 File Offset: 0x00027560
		[NativeName("Reinitialize")]
		private bool ReinitializeImpl(int width, int height)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Texture2D.ReinitializeImpl_Injected(intPtr, width, height);
		}

		// Token: 0x060017FF RID: 6143 RVA: 0x00029384 File Offset: 0x00027584
		[NativeName("SetPixel")]
		private void SetPixelImpl(int image, int mip, int x, int y, Color color)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Texture2D.SetPixelImpl_Injected(intPtr, image, mip, x, y, ref color);
		}

		// Token: 0x06001800 RID: 6144 RVA: 0x000293B0 File Offset: 0x000275B0
		[NativeName("GetPixel")]
		private Color GetPixelImpl(int image, int mip, int x, int y)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Color color;
			Texture2D.GetPixelImpl_Injected(intPtr, image, mip, x, y, out color);
			return color;
		}

		// Token: 0x06001801 RID: 6145 RVA: 0x000293DC File Offset: 0x000275DC
		[NativeName("GetPixelBilinear")]
		private Color GetPixelBilinearImpl(int image, int mip, float u, float v)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Color color;
			Texture2D.GetPixelBilinearImpl_Injected(intPtr, image, mip, u, v, out color);
			return color;
		}

		// Token: 0x06001802 RID: 6146 RVA: 0x00029408 File Offset: 0x00027608
		[FreeFunction(Name = "Texture2DScripting::ReinitializeWithFormat", HasExplicitThis = true)]
		private bool ReinitializeWithFormatImpl(int width, int height, GraphicsFormat format, bool hasMipMap)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Texture2D.ReinitializeWithFormatImpl_Injected(intPtr, width, height, format, hasMipMap);
		}

		// Token: 0x06001803 RID: 6147 RVA: 0x00029430 File Offset: 0x00027630
		[FreeFunction(Name = "Texture2DScripting::ReinitializeWithTextureFormat", HasExplicitThis = true)]
		private bool ReinitializeWithTextureFormatImpl(int width, int height, TextureFormat textureFormat, bool hasMipMap)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Texture2D.ReinitializeWithTextureFormatImpl_Injected(intPtr, width, height, textureFormat, hasMipMap);
		}

		// Token: 0x06001804 RID: 6148 RVA: 0x00029458 File Offset: 0x00027658
		[FreeFunction(Name = "Texture2DScripting::ReadPixels", HasExplicitThis = true)]
		private void ReadPixelsImpl(Rect source, int destX, int destY, bool recalculateMipMaps)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Texture2D.ReadPixelsImpl_Injected(intPtr, ref source, destX, destY, recalculateMipMaps);
		}

		// Token: 0x06001805 RID: 6149 RVA: 0x00029480 File Offset: 0x00027680
		[FreeFunction(Name = "Texture2DScripting::SetPixels", HasExplicitThis = true, ThrowsException = true)]
		private unsafe void SetPixelsImpl(int x, int y, int w, int h, Color[] pixel, int miplevel, int frame)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<Color> span = new Span<Color>(pixel);
			fixed (Color* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				Texture2D.SetPixelsImpl_Injected(intPtr, x, y, w, h, ref managedSpanWrapper, miplevel, frame);
			}
		}

		// Token: 0x06001806 RID: 6150 RVA: 0x000294D4 File Offset: 0x000276D4
		[FreeFunction(Name = "Texture2DScripting::LoadRawData", HasExplicitThis = true)]
		private bool LoadRawTextureDataImpl(IntPtr data, ulong size)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Texture2D.LoadRawTextureDataImpl_Injected(intPtr, data, size);
		}

		// Token: 0x06001807 RID: 6151 RVA: 0x000294F8 File Offset: 0x000276F8
		[FreeFunction(Name = "Texture2DScripting::LoadRawData", HasExplicitThis = true)]
		private unsafe bool LoadRawTextureDataImplArray(byte[] data)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<byte> span = new Span<byte>(data);
			bool flag;
			fixed (byte* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				flag = Texture2D.LoadRawTextureDataImplArray_Injected(intPtr, ref managedSpanWrapper);
			}
			return flag;
		}

		// Token: 0x06001808 RID: 6152 RVA: 0x00029540 File Offset: 0x00027740
		[FreeFunction(Name = "Texture2DScripting::SetPixelDataArray", HasExplicitThis = true, ThrowsException = true)]
		private bool SetPixelDataImplArray(Array data, int mipLevel, int elementSize, int dataArraySize, int sourceDataStartIndex = 0)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Texture2D.SetPixelDataImplArray_Injected(intPtr, data, mipLevel, elementSize, dataArraySize, sourceDataStartIndex);
		}

		// Token: 0x06001809 RID: 6153 RVA: 0x0002956C File Offset: 0x0002776C
		[FreeFunction(Name = "Texture2DScripting::SetPixelData", HasExplicitThis = true, ThrowsException = true)]
		private bool SetPixelDataImpl(IntPtr data, int mipLevel, int elementSize, int dataArraySize, int sourceDataStartIndex = 0)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Texture2D.SetPixelDataImpl_Injected(intPtr, data, mipLevel, elementSize, dataArraySize, sourceDataStartIndex);
		}

		// Token: 0x0600180A RID: 6154 RVA: 0x00029598 File Offset: 0x00027798
		private IntPtr GetWritableImageData(int frame)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Texture2D.GetWritableImageData_Injected(intPtr, frame);
		}

		// Token: 0x0600180B RID: 6155 RVA: 0x000295BC File Offset: 0x000277BC
		private ulong GetImageDataSize()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Texture2D.GetImageDataSize_Injected(intPtr);
		}

		// Token: 0x0600180C RID: 6156 RVA: 0x000295E0 File Offset: 0x000277E0
		[FreeFunction("Texture2DScripting::GenerateAtlas")]
		private unsafe static void GenerateAtlasImpl(Vector2[] sizes, int padding, int atlasSize, [Out] Rect[] rect)
		{
			try
			{
				Span<Vector2> span = new Span<Vector2>(sizes);
				fixed (Vector2* ptr = span.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, span.Length);
					BlittableArrayWrapper blittableArrayWrapper;
					if (rect != null)
					{
						fixed (Rect[] array = rect)
						{
							if (array.Length != 0)
							{
								blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
							}
						}
					}
					Texture2D.GenerateAtlasImpl_Injected(ref managedSpanWrapper, padding, atlasSize, out blittableArrayWrapper);
				}
			}
			finally
			{
				Vector2* ptr = null;
				Rect[] array;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<Rect>(ref array);
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x0600180D RID: 6157 RVA: 0x0002965C File Offset: 0x0002785C
		internal bool isPreProcessed
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Texture2D.get_isPreProcessed_Injected(intPtr);
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x0600180E RID: 6158 RVA: 0x00029680 File Offset: 0x00027880
		public bool streamingMipmaps
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Texture2D.get_streamingMipmaps_Injected(intPtr);
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x0600180F RID: 6159 RVA: 0x000296A4 File Offset: 0x000278A4
		public int streamingMipmapsPriority
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Texture2D.get_streamingMipmapsPriority_Injected(intPtr);
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06001810 RID: 6160 RVA: 0x000296C8 File Offset: 0x000278C8
		// (set) Token: 0x06001811 RID: 6161 RVA: 0x000296EC File Offset: 0x000278EC
		public int requestedMipmapLevel
		{
			[FreeFunction(Name = "GetTextureStreamingManager().GetRequestedMipmapLevel", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Texture2D.get_requestedMipmapLevel_Injected(intPtr);
			}
			[FreeFunction(Name = "GetTextureStreamingManager().SetRequestedMipmapLevel", HasExplicitThis = true)]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Texture2D.set_requestedMipmapLevel_Injected(intPtr, value);
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06001812 RID: 6162 RVA: 0x00029710 File Offset: 0x00027910
		// (set) Token: 0x06001813 RID: 6163 RVA: 0x00029734 File Offset: 0x00027934
		public int minimumMipmapLevel
		{
			[FreeFunction(Name = "GetTextureStreamingManager().GetMinimumMipmapLevel", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Texture2D.get_minimumMipmapLevel_Injected(intPtr);
			}
			[FreeFunction(Name = "GetTextureStreamingManager().SetMinimumMipmapLevel", HasExplicitThis = true)]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Texture2D.set_minimumMipmapLevel_Injected(intPtr, value);
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06001814 RID: 6164 RVA: 0x00029758 File Offset: 0x00027958
		// (set) Token: 0x06001815 RID: 6165 RVA: 0x0002977C File Offset: 0x0002797C
		internal bool loadAllMips
		{
			[FreeFunction(Name = "GetTextureStreamingManager().GetLoadAllMips", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Texture2D.get_loadAllMips_Injected(intPtr);
			}
			[FreeFunction(Name = "GetTextureStreamingManager().SetLoadAllMips", HasExplicitThis = true)]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Texture2D.set_loadAllMips_Injected(intPtr, value);
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06001816 RID: 6166 RVA: 0x000297A0 File Offset: 0x000279A0
		public int calculatedMipmapLevel
		{
			[FreeFunction(Name = "GetTextureStreamingManager().GetCalculatedMipmapLevel", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Texture2D.get_calculatedMipmapLevel_Injected(intPtr);
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06001817 RID: 6167 RVA: 0x000297C4 File Offset: 0x000279C4
		public int desiredMipmapLevel
		{
			[FreeFunction(Name = "GetTextureStreamingManager().GetDesiredMipmapLevel", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Texture2D.get_desiredMipmapLevel_Injected(intPtr);
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06001818 RID: 6168 RVA: 0x000297E8 File Offset: 0x000279E8
		public int loadingMipmapLevel
		{
			[FreeFunction(Name = "GetTextureStreamingManager().GetLoadingMipmapLevel", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Texture2D.get_loadingMipmapLevel_Injected(intPtr);
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06001819 RID: 6169 RVA: 0x0002980C File Offset: 0x00027A0C
		public int loadedMipmapLevel
		{
			[FreeFunction(Name = "GetTextureStreamingManager().GetLoadedMipmapLevel", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Texture2D.get_loadedMipmapLevel_Injected(intPtr);
			}
		}

		// Token: 0x0600181A RID: 6170 RVA: 0x00029830 File Offset: 0x00027A30
		[FreeFunction(Name = "GetTextureStreamingManager().ClearRequestedMipmapLevel", HasExplicitThis = true)]
		public void ClearRequestedMipmapLevel()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Texture2D.ClearRequestedMipmapLevel_Injected(intPtr);
		}

		// Token: 0x0600181B RID: 6171 RVA: 0x00029854 File Offset: 0x00027A54
		[FreeFunction(Name = "GetTextureStreamingManager().IsRequestedMipmapLevelLoaded", HasExplicitThis = true)]
		public bool IsRequestedMipmapLevelLoaded()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Texture2D.IsRequestedMipmapLevelLoaded_Injected(intPtr);
		}

		// Token: 0x0600181C RID: 6172 RVA: 0x00029878 File Offset: 0x00027A78
		[FreeFunction(Name = "GetTextureStreamingManager().ClearMinimumMipmapLevel", HasExplicitThis = true)]
		public void ClearMinimumMipmapLevel()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Texture2D.ClearMinimumMipmapLevel_Injected(intPtr);
		}

		// Token: 0x0600181D RID: 6173 RVA: 0x0002989C File Offset: 0x00027A9C
		[FreeFunction("Texture2DScripting::UpdateExternalTexture", HasExplicitThis = true)]
		public void UpdateExternalTexture(IntPtr nativeTex)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Texture2D.UpdateExternalTexture_Injected(intPtr, nativeTex);
		}

		// Token: 0x0600181E RID: 6174 RVA: 0x000298C0 File Offset: 0x00027AC0
		[FreeFunction("Texture2DScripting::SetAllPixels32", HasExplicitThis = true, ThrowsException = true)]
		private unsafe void SetAllPixels32(Color32[] colors, int miplevel)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<Color32> span = new Span<Color32>(colors);
			fixed (Color32* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				Texture2D.SetAllPixels32_Injected(intPtr, ref managedSpanWrapper, miplevel);
			}
		}

		// Token: 0x0600181F RID: 6175 RVA: 0x0002990C File Offset: 0x00027B0C
		[FreeFunction("Texture2DScripting::SetBlockOfPixels32", HasExplicitThis = true, ThrowsException = true)]
		private unsafe void SetBlockOfPixels32(int x, int y, int blockWidth, int blockHeight, Color32[] colors, int miplevel)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<Color32> span = new Span<Color32>(colors);
			fixed (Color32* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				Texture2D.SetBlockOfPixels32_Injected(intPtr, x, y, blockWidth, blockHeight, ref managedSpanWrapper, miplevel);
			}
		}

		// Token: 0x06001820 RID: 6176 RVA: 0x0002995C File Offset: 0x00027B5C
		[FreeFunction("Texture2DScripting::GetRawTextureData", HasExplicitThis = true, ThrowsException = true)]
		[return: Unmarshalled]
		public byte[] GetRawTextureData()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Texture2D.GetRawTextureData_Injected(intPtr);
		}

		// Token: 0x06001821 RID: 6177 RVA: 0x00029980 File Offset: 0x00027B80
		[FreeFunction("Texture2DScripting::GetPixels", HasExplicitThis = true, ThrowsException = true)]
		[return: Unmarshalled]
		public Color[] GetPixels(int x, int y, int blockWidth, int blockHeight, [DefaultValue("0")] int miplevel)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Texture2D.GetPixels_Injected(intPtr, x, y, blockWidth, blockHeight, miplevel);
		}

		// Token: 0x06001822 RID: 6178 RVA: 0x000299AC File Offset: 0x00027BAC
		[ExcludeFromDocs]
		public Color[] GetPixels(int x, int y, int blockWidth, int blockHeight)
		{
			return this.GetPixels(x, y, blockWidth, blockHeight, 0);
		}

		// Token: 0x06001823 RID: 6179 RVA: 0x000299CC File Offset: 0x00027BCC
		[FreeFunction("Texture2DScripting::GetPixels32", HasExplicitThis = true, ThrowsException = true)]
		[return: Unmarshalled]
		public Color32[] GetPixels32([DefaultValue("0")] int miplevel)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Texture2D.GetPixels32_Injected(intPtr, miplevel);
		}

		// Token: 0x06001824 RID: 6180 RVA: 0x000299F0 File Offset: 0x00027BF0
		[ExcludeFromDocs]
		public Color32[] GetPixels32()
		{
			return this.GetPixels32(0);
		}

		// Token: 0x06001825 RID: 6181 RVA: 0x00029A0C File Offset: 0x00027C0C
		[FreeFunction("Texture2DScripting::PackTextures", HasExplicitThis = true)]
		[return: Unmarshalled]
		public Rect[] PackTextures(Texture2D[] textures, int padding, int maximumAtlasSize, bool makeNoLongerReadable)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Texture2D.PackTextures_Injected(intPtr, textures, padding, maximumAtlasSize, makeNoLongerReadable);
		}

		// Token: 0x06001826 RID: 6182 RVA: 0x00029A34 File Offset: 0x00027C34
		public Rect[] PackTextures(Texture2D[] textures, int padding, int maximumAtlasSize)
		{
			return this.PackTextures(textures, padding, maximumAtlasSize, false);
		}

		// Token: 0x06001827 RID: 6183 RVA: 0x00029A50 File Offset: 0x00027C50
		public Rect[] PackTextures(Texture2D[] textures, int padding)
		{
			return this.PackTextures(textures, padding, 2048);
		}

		// Token: 0x06001828 RID: 6184 RVA: 0x00029A70 File Offset: 0x00027C70
		[FreeFunction(Name = "Texture2DScripting::CopyPixels", HasExplicitThis = true, ThrowsException = true)]
		private void CopyPixels_Full(Texture src)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Texture2D.CopyPixels_Full_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Texture>(src));
		}

		// Token: 0x06001829 RID: 6185 RVA: 0x00029A98 File Offset: 0x00027C98
		[FreeFunction(Name = "Texture2DScripting::CopyPixels", HasExplicitThis = true, ThrowsException = true)]
		private void CopyPixels_Slice(Texture src, int srcElement, int srcMip, int dstMip)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Texture2D.CopyPixels_Slice_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Texture>(src), srcElement, srcMip, dstMip);
		}

		// Token: 0x0600182A RID: 6186 RVA: 0x00029AC4 File Offset: 0x00027CC4
		[FreeFunction(Name = "Texture2DScripting::CopyPixels", HasExplicitThis = true, ThrowsException = true)]
		private void CopyPixels_Region(Texture src, int srcElement, int srcMip, int srcX, int srcY, int srcWidth, int srcHeight, int dstMip, int dstX, int dstY)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Texture2D.CopyPixels_Region_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Texture>(src), srcElement, srcMip, srcX, srcY, srcWidth, srcHeight, dstMip, dstX, dstY);
		}

		// Token: 0x0600182B RID: 6187 RVA: 0x00029AFC File Offset: 0x00027CFC
		internal bool ValidateFormat(TextureFormat format, int width, int height)
		{
			bool flag = base.ValidateFormat(format);
			bool flag2 = flag;
			if (flag2)
			{
				bool flag3 = TextureFormat.PVRTC_RGB2 <= format && format <= TextureFormat.PVRTC_RGBA4;
				bool flag4 = flag3 && (width != height || !Mathf.IsPowerOfTwo(width));
				if (flag4)
				{
					throw new UnityException(string.Format("'{0}' demands texture to be square and have power-of-two dimensions", format.ToString()));
				}
			}
			return flag;
		}

		// Token: 0x0600182C RID: 6188 RVA: 0x00029B68 File Offset: 0x00027D68
		internal bool ValidateFormat(GraphicsFormat format, int width, int height)
		{
			bool flag = base.ValidateFormat(format, GraphicsFormatUsage.Sample);
			bool flag2 = flag;
			if (flag2)
			{
				bool flag3 = GraphicsFormatUtility.IsPVRTCFormat(format);
				bool flag4 = flag3 && (width != height || !Mathf.IsPowerOfTwo(width));
				if (flag4)
				{
					throw new UnityException(string.Format("'{0}' demands texture to be square and have power-of-two dimensions", format.ToString()));
				}
			}
			return flag;
		}

		// Token: 0x0600182D RID: 6189 RVA: 0x00029BCC File Offset: 0x00027DCC
		internal Texture2D(int width, int height, GraphicsFormat format, TextureCreationFlags flags, int mipCount, IntPtr nativeTex, MipmapLimitDescriptor mipmapLimitDescriptor)
		{
			bool flag = mipmapLimitDescriptor.useMipmapLimit;
			string groupName = mipmapLimitDescriptor.groupName;
			bool flag2 = (flags & TextureCreationFlags.IgnoreMipmapLimit) > TextureCreationFlags.None;
			bool flag3 = flag2;
			if (flag3)
			{
				flag = false;
			}
			bool flag4 = this.ValidateFormat(format, width, height);
			if (flag4)
			{
				Texture2D.Internal_Create(this, width, height, mipCount, format, base.GetTextureColorSpace(format), flags, nativeTex, !flag, groupName);
			}
		}

		// Token: 0x0600182E RID: 6190 RVA: 0x00029C2F File Offset: 0x00027E2F
		[ExcludeFromDocs]
		public Texture2D(int width, int height, DefaultFormat format, TextureCreationFlags flags)
			: this(width, height, SystemInfo.GetGraphicsFormat(format), flags)
		{
		}

		// Token: 0x0600182F RID: 6191 RVA: 0x00029C44 File Offset: 0x00027E44
		[ExcludeFromDocs]
		public Texture2D(int width, int height, DefaultFormat format, int mipCount, TextureCreationFlags flags)
			: this(width, height, SystemInfo.GetGraphicsFormat(format), flags, mipCount, IntPtr.Zero, default(MipmapLimitDescriptor))
		{
		}

		// Token: 0x06001830 RID: 6192 RVA: 0x00029C74 File Offset: 0x00027E74
		[ExcludeFromDocs]
		[Obsolete("Please provide mipmap limit information using a MipmapLimitDescriptor argument", false)]
		public Texture2D(int width, int height, DefaultFormat format, int mipCount, string mipmapLimitGroupName, TextureCreationFlags flags)
			: this(width, height, SystemInfo.GetGraphicsFormat(format), flags, mipCount, IntPtr.Zero, new MipmapLimitDescriptor(true, mipmapLimitGroupName))
		{
		}

		// Token: 0x06001831 RID: 6193 RVA: 0x00029CA2 File Offset: 0x00027EA2
		[ExcludeFromDocs]
		public Texture2D(int width, int height, DefaultFormat format, int mipCount, TextureCreationFlags flags, MipmapLimitDescriptor mipmapLimitDescriptor)
			: this(width, height, SystemInfo.GetGraphicsFormat(format), flags, mipCount, IntPtr.Zero, mipmapLimitDescriptor)
		{
		}

		// Token: 0x06001832 RID: 6194 RVA: 0x00029CC0 File Offset: 0x00027EC0
		[ExcludeFromDocs]
		public Texture2D(int width, int height, GraphicsFormat format, TextureCreationFlags flags)
			: this(width, height, format, flags, Texture.GenerateAllMips, IntPtr.Zero, default(MipmapLimitDescriptor))
		{
		}

		// Token: 0x06001833 RID: 6195 RVA: 0x00029CF0 File Offset: 0x00027EF0
		[ExcludeFromDocs]
		public Texture2D(int width, int height, GraphicsFormat format, int mipCount, TextureCreationFlags flags)
			: this(width, height, format, flags, mipCount, IntPtr.Zero, default(MipmapLimitDescriptor))
		{
		}

		// Token: 0x06001834 RID: 6196 RVA: 0x00029D1C File Offset: 0x00027F1C
		[Obsolete("Please provide mipmap limit information using a MipmapLimitDescriptor argument", false)]
		[ExcludeFromDocs]
		public Texture2D(int width, int height, GraphicsFormat format, int mipCount, string mipmapLimitGroupName, TextureCreationFlags flags)
			: this(width, height, format, flags, mipCount, IntPtr.Zero, new MipmapLimitDescriptor(true, mipmapLimitGroupName))
		{
		}

		// Token: 0x06001835 RID: 6197 RVA: 0x00029D45 File Offset: 0x00027F45
		[ExcludeFromDocs]
		public Texture2D(int width, int height, GraphicsFormat format, int mipCount, TextureCreationFlags flags, MipmapLimitDescriptor mipmapLimitDescriptor)
			: this(width, height, format, flags, mipCount, IntPtr.Zero, mipmapLimitDescriptor)
		{
		}

		// Token: 0x06001836 RID: 6198 RVA: 0x00029D60 File Offset: 0x00027F60
		internal Texture2D(int width, int height, TextureFormat textureFormat, int mipCount, bool linear, IntPtr nativeTex, bool createUninitialized, MipmapLimitDescriptor mipmapLimitDescriptor)
		{
			bool flag = !this.ValidateFormat(textureFormat, width, height);
			if (!flag)
			{
				GraphicsFormat graphicsFormat = GraphicsFormatUtility.GetGraphicsFormat(textureFormat, !linear);
				TextureCreationFlags textureCreationFlags = ((mipCount != 1) ? TextureCreationFlags.MipChain : TextureCreationFlags.None);
				bool flag2 = GraphicsFormatUtility.IsCrunchFormat(textureFormat);
				if (flag2)
				{
					textureCreationFlags |= TextureCreationFlags.Crunch;
				}
				if (createUninitialized)
				{
					textureCreationFlags |= TextureCreationFlags.DontInitializePixels | TextureCreationFlags.DontUploadUponCreate;
				}
				Texture2D.Internal_Create(this, width, height, mipCount, graphicsFormat, base.GetTextureColorSpace(linear), textureCreationFlags, nativeTex, !mipmapLimitDescriptor.useMipmapLimit, mipmapLimitDescriptor.groupName);
			}
		}

		// Token: 0x06001837 RID: 6199 RVA: 0x00029DE4 File Offset: 0x00027FE4
		public Texture2D(int width, int height, [DefaultValue("TextureFormat.RGBA32")] TextureFormat textureFormat, [DefaultValue("-1")] int mipCount, [DefaultValue("false")] bool linear)
			: this(width, height, textureFormat, mipCount, linear, IntPtr.Zero, false, default(MipmapLimitDescriptor))
		{
		}

		// Token: 0x06001838 RID: 6200 RVA: 0x00029E10 File Offset: 0x00028010
		public Texture2D(int width, int height, [DefaultValue("TextureFormat.RGBA32")] TextureFormat textureFormat, [DefaultValue("-1")] int mipCount, [DefaultValue("false")] bool linear, [DefaultValue("false")] bool createUninitialized)
			: this(width, height, textureFormat, mipCount, linear, IntPtr.Zero, createUninitialized, default(MipmapLimitDescriptor))
		{
		}

		// Token: 0x06001839 RID: 6201 RVA: 0x00029E3C File Offset: 0x0002803C
		public Texture2D(int width, int height, [DefaultValue("TextureFormat.RGBA32")] TextureFormat textureFormat, [DefaultValue("-1")] int mipCount, [DefaultValue("false")] bool linear, [DefaultValue("false")] bool createUninitialized, MipmapLimitDescriptor mipmapLimitDescriptor)
			: this(width, height, textureFormat, mipCount, linear, IntPtr.Zero, createUninitialized, mipmapLimitDescriptor)
		{
		}

		// Token: 0x0600183A RID: 6202 RVA: 0x00029E64 File Offset: 0x00028064
		[Obsolete("Please provide mipmap limit information using a MipmapLimitDescriptor argument", false)]
		public Texture2D(int width, int height, [DefaultValue("TextureFormat.RGBA32")] TextureFormat textureFormat, [DefaultValue("-1")] int mipCount, [DefaultValue("false")] bool linear, [DefaultValue("false")] bool createUninitialized, [DefaultValue("true")] bool ignoreMipmapLimit, [DefaultValue("null")] string mipmapLimitGroupName)
			: this(width, height, textureFormat, mipCount, linear, IntPtr.Zero, createUninitialized, new MipmapLimitDescriptor(!ignoreMipmapLimit, mipmapLimitGroupName))
		{
		}

		// Token: 0x0600183B RID: 6203 RVA: 0x00029E94 File Offset: 0x00028094
		public Texture2D(int width, int height, [DefaultValue("TextureFormat.RGBA32")] TextureFormat textureFormat, [DefaultValue("true")] bool mipChain, [DefaultValue("false")] bool linear)
			: this(width, height, textureFormat, mipChain ? Texture.GenerateAllMips : 1, linear, IntPtr.Zero, false, default(MipmapLimitDescriptor))
		{
		}

		// Token: 0x0600183C RID: 6204 RVA: 0x00029ECC File Offset: 0x000280CC
		public Texture2D(int width, int height, [DefaultValue("TextureFormat.RGBA32")] TextureFormat textureFormat, [DefaultValue("true")] bool mipChain, [DefaultValue("false")] bool linear, [DefaultValue("false")] bool createUninitialized)
			: this(width, height, textureFormat, mipChain ? Texture.GenerateAllMips : 1, linear, IntPtr.Zero, createUninitialized, default(MipmapLimitDescriptor))
		{
		}

		// Token: 0x0600183D RID: 6205 RVA: 0x00029F04 File Offset: 0x00028104
		public Texture2D(int width, int height, TextureFormat textureFormat, bool mipChain)
			: this(width, height, textureFormat, mipChain ? Texture.GenerateAllMips : 1, false, IntPtr.Zero, false, default(MipmapLimitDescriptor))
		{
		}

		// Token: 0x0600183E RID: 6206 RVA: 0x00029F38 File Offset: 0x00028138
		public Texture2D(int width, int height)
		{
			TextureFormat textureFormat = TextureFormat.RGBA32;
			bool flag = width == 0 && height == 0;
			if (flag)
			{
				Texture2D.Internal_CreateEmptyImpl(this);
			}
			else
			{
				bool flag2 = this.ValidateFormat(textureFormat, width, height);
				if (flag2)
				{
					Texture2D.Internal_Create(this, width, height, Texture.GenerateAllMips, GraphicsFormatUtility.GetGraphicsFormat(textureFormat, true), base.GetTextureColorSpace(false), TextureCreationFlags.MipChain, IntPtr.Zero, true, null);
				}
			}
		}

		// Token: 0x0600183F RID: 6207 RVA: 0x00029F98 File Offset: 0x00028198
		public static Texture2D CreateExternalTexture(int width, int height, TextureFormat format, bool mipChain, bool linear, IntPtr nativeTex)
		{
			bool flag = nativeTex == IntPtr.Zero;
			if (flag)
			{
				throw new ArgumentException("nativeTex can not be null");
			}
			return new Texture2D(width, height, format, mipChain ? (-1) : 1, linear, nativeTex, false, default(MipmapLimitDescriptor));
		}

		// Token: 0x06001840 RID: 6208 RVA: 0x00029FE4 File Offset: 0x000281E4
		[ExcludeFromDocs]
		public void SetPixel(int x, int y, Color color)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			this.SetPixelImpl(0, 0, x, y, color);
		}

		// Token: 0x06001841 RID: 6209 RVA: 0x0002A014 File Offset: 0x00028214
		public void SetPixel(int x, int y, Color color, [DefaultValue("0")] int mipLevel)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			this.SetPixelImpl(0, mipLevel, x, y, color);
		}

		// Token: 0x06001842 RID: 6210 RVA: 0x0002A044 File Offset: 0x00028244
		public void SetPixels(int x, int y, int blockWidth, int blockHeight, Color[] colors, [DefaultValue("0")] int miplevel)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			this.SetPixelsImpl(x, y, blockWidth, blockHeight, colors, miplevel, 0);
		}

		// Token: 0x06001843 RID: 6211 RVA: 0x0002A078 File Offset: 0x00028278
		[ExcludeFromDocs]
		public void SetPixels(int x, int y, int blockWidth, int blockHeight, Color[] colors)
		{
			this.SetPixels(x, y, blockWidth, blockHeight, colors, 0);
		}

		// Token: 0x06001844 RID: 6212 RVA: 0x0002A08C File Offset: 0x0002828C
		public void SetPixels(Color[] colors, [DefaultValue("0")] int miplevel)
		{
			int num = this.width >> miplevel;
			bool flag = num < 1;
			if (flag)
			{
				num = 1;
			}
			int num2 = this.height >> miplevel;
			bool flag2 = num2 < 1;
			if (flag2)
			{
				num2 = 1;
			}
			this.SetPixels(0, 0, num, num2, colors, miplevel);
		}

		// Token: 0x06001845 RID: 6213 RVA: 0x0002A0D3 File Offset: 0x000282D3
		[ExcludeFromDocs]
		public void SetPixels(Color[] colors)
		{
			this.SetPixels(0, 0, this.width, this.height, colors, 0);
		}

		// Token: 0x06001846 RID: 6214 RVA: 0x0002A0F0 File Offset: 0x000282F0
		[ExcludeFromDocs]
		public Color GetPixel(int x, int y)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			return this.GetPixelImpl(0, 0, x, y);
		}

		// Token: 0x06001847 RID: 6215 RVA: 0x0002A124 File Offset: 0x00028324
		public Color GetPixel(int x, int y, [DefaultValue("0")] int mipLevel)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			return this.GetPixelImpl(0, mipLevel, x, y);
		}

		// Token: 0x06001848 RID: 6216 RVA: 0x0002A158 File Offset: 0x00028358
		[ExcludeFromDocs]
		public Color GetPixelBilinear(float u, float v)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			return this.GetPixelBilinearImpl(0, 0, u, v);
		}

		// Token: 0x06001849 RID: 6217 RVA: 0x0002A18C File Offset: 0x0002838C
		public Color GetPixelBilinear(float u, float v, [DefaultValue("0")] int mipLevel)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			return this.GetPixelBilinearImpl(0, mipLevel, u, v);
		}

		// Token: 0x0600184A RID: 6218 RVA: 0x0002A1C0 File Offset: 0x000283C0
		public void LoadRawTextureData(IntPtr data, int size)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			bool flag2 = data == IntPtr.Zero || size == 0;
			if (flag2)
			{
				Debug.LogError("No texture data provided to LoadRawTextureData", this);
			}
			else
			{
				bool flag3 = !this.LoadRawTextureDataImpl(data, (ulong)((long)size));
				if (flag3)
				{
					throw new UnityException("LoadRawTextureData: not enough data provided (will result in overread).");
				}
			}
		}

		// Token: 0x0600184B RID: 6219 RVA: 0x0002A228 File Offset: 0x00028428
		public void LoadRawTextureData(byte[] data)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			bool flag2 = data == null || data.Length == 0;
			if (flag2)
			{
				Debug.LogError("No texture data provided to LoadRawTextureData", this);
			}
			else
			{
				bool flag3 = !this.LoadRawTextureDataImplArray(data);
				if (flag3)
				{
					throw new UnityException("LoadRawTextureData: not enough data provided (will result in overread).");
				}
			}
		}

		// Token: 0x0600184C RID: 6220 RVA: 0x0002A284 File Offset: 0x00028484
		public void LoadRawTextureData<T>(NativeArray<T> data) where T : struct
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			bool flag2 = !data.IsCreated || data.Length == 0;
			if (flag2)
			{
				throw new UnityException("No texture data provided to LoadRawTextureData");
			}
			bool flag3 = !this.LoadRawTextureDataImpl((IntPtr)data.GetUnsafeReadOnlyPtr<T>(), (ulong)((long)data.Length * (long)UnsafeUtility.SizeOf<T>()));
			if (flag3)
			{
				throw new UnityException("LoadRawTextureData: not enough data provided (will result in overread).");
			}
		}

		// Token: 0x0600184D RID: 6221 RVA: 0x0002A300 File Offset: 0x00028500
		public void SetPixelData<T>(T[] data, int mipLevel, [DefaultValue("0")] int sourceDataStartIndex = 0)
		{
			bool flag = sourceDataStartIndex < 0;
			if (flag)
			{
				throw new UnityException("SetPixelData: sourceDataStartIndex cannot be less than 0.");
			}
			bool flag2 = !this.isReadable;
			if (flag2)
			{
				throw base.CreateNonReadableException(this);
			}
			bool flag3 = data == null || data.Length == 0;
			if (flag3)
			{
				throw new UnityException("No texture data provided to SetPixelData.");
			}
			this.SetPixelDataImplArray(data, mipLevel, Marshal.SizeOf<T>(data[0]), data.Length, sourceDataStartIndex);
		}

		// Token: 0x0600184E RID: 6222 RVA: 0x0002A36C File Offset: 0x0002856C
		public void SetPixelData<T>(NativeArray<T> data, int mipLevel, [DefaultValue("0")] int sourceDataStartIndex = 0) where T : struct
		{
			bool flag = sourceDataStartIndex < 0;
			if (flag)
			{
				throw new UnityException("SetPixelData: sourceDataStartIndex cannot be less than 0.");
			}
			bool flag2 = !this.isReadable;
			if (flag2)
			{
				throw base.CreateNonReadableException(this);
			}
			bool flag3 = !data.IsCreated || data.Length == 0;
			if (flag3)
			{
				throw new UnityException("No texture data provided to SetPixelData.");
			}
			this.SetPixelDataImpl((IntPtr)data.GetUnsafeReadOnlyPtr<T>(), mipLevel, UnsafeUtility.SizeOf<T>(), data.Length, sourceDataStartIndex);
		}

		// Token: 0x0600184F RID: 6223 RVA: 0x0002A3E8 File Offset: 0x000285E8
		public unsafe NativeArray<T> GetPixelData<T>(int mipLevel) where T : struct
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			bool flag2 = mipLevel < 0 || mipLevel >= base.mipmapCount;
			if (flag2)
			{
				throw new ArgumentException("The passed in miplevel " + mipLevel.ToString() + " is invalid. It needs to be in the range 0 and " + (base.mipmapCount - 1).ToString());
			}
			bool flag3 = this.GetWritableImageData(0).ToInt64() == 0L;
			if (flag3)
			{
				throw new UnityException("Texture '" + base.name + "' has no data.");
			}
			ulong pixelDataOffset = base.GetPixelDataOffset(mipLevel, 0);
			ulong pixelDataSize = base.GetPixelDataSize(mipLevel, 0);
			int num = UnsafeUtility.SizeOf<T>();
			ulong num2 = pixelDataSize / (ulong)((long)num);
			bool flag4 = num2 > 2147483647UL;
			if (flag4)
			{
				throw base.CreateNativeArrayLengthOverflowException();
			}
			IntPtr intPtr = new IntPtr((long)this.GetWritableImageData(0) + (long)pixelDataOffset);
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>((void*)intPtr, (int)num2, Allocator.None);
		}

		// Token: 0x06001850 RID: 6224 RVA: 0x0002A4EC File Offset: 0x000286EC
		public unsafe NativeArray<T> GetRawTextureData<T>() where T : struct
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			int num = UnsafeUtility.SizeOf<T>();
			ulong num2 = this.GetImageDataSize() / (ulong)((long)num);
			bool flag2 = num2 > 2147483647UL;
			if (flag2)
			{
				throw base.CreateNativeArrayLengthOverflowException();
			}
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>((void*)this.GetWritableImageData(0), (int)num2, Allocator.None);
		}

		// Token: 0x06001851 RID: 6225 RVA: 0x0002A554 File Offset: 0x00028754
		public void Apply([DefaultValue("true")] bool updateMipmaps, [DefaultValue("false")] bool makeNoLongerReadable)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			this.ApplyImpl(updateMipmaps, makeNoLongerReadable);
		}

		// Token: 0x06001852 RID: 6226 RVA: 0x0002A580 File Offset: 0x00028780
		[ExcludeFromDocs]
		public void Apply(bool updateMipmaps)
		{
			this.Apply(updateMipmaps, false);
		}

		// Token: 0x06001853 RID: 6227 RVA: 0x0002A58C File Offset: 0x0002878C
		[ExcludeFromDocs]
		public void Apply()
		{
			this.Apply(true, false);
		}

		// Token: 0x06001854 RID: 6228 RVA: 0x0002A598 File Offset: 0x00028798
		public bool Reinitialize(int width, int height)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			return this.ReinitializeImpl(width, height);
		}

		// Token: 0x06001855 RID: 6229 RVA: 0x0002A5C8 File Offset: 0x000287C8
		public bool Reinitialize(int width, int height, TextureFormat format, bool hasMipMap)
		{
			return this.ReinitializeWithTextureFormatImpl(width, height, format, hasMipMap);
		}

		// Token: 0x06001856 RID: 6230 RVA: 0x0002A5E8 File Offset: 0x000287E8
		public bool Reinitialize(int width, int height, GraphicsFormat format, bool hasMipMap)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			return this.ReinitializeWithFormatImpl(width, height, format, hasMipMap);
		}

		// Token: 0x06001857 RID: 6231 RVA: 0x0002A61C File Offset: 0x0002881C
		[Obsolete("Texture2D.Resize(int, int) has been deprecated because it actually reinitializes the texture. Use Texture2D.Reinitialize(int, int) instead (UnityUpgradable) -> Reinitialize([*] System.Int32, [*] System.Int32)", false)]
		public bool Resize(int width, int height)
		{
			return this.Reinitialize(width, height);
		}

		// Token: 0x06001858 RID: 6232 RVA: 0x0002A638 File Offset: 0x00028838
		[Obsolete("Texture2D.Resize(int, int, TextureFormat, bool) has been deprecated because it actually reinitializes the texture. Use Texture2D.Reinitialize(int, int, TextureFormat, bool) instead (UnityUpgradable) -> Reinitialize([*] System.Int32, [*] System.Int32, UnityEngine.TextureFormat, [*] System.Boolean)", false)]
		public bool Resize(int width, int height, TextureFormat format, bool hasMipMap)
		{
			return this.Reinitialize(width, height, format, hasMipMap);
		}

		// Token: 0x06001859 RID: 6233 RVA: 0x0002A658 File Offset: 0x00028858
		[Obsolete("Texture2D.Resize(int, int, GraphicsFormat, bool) has been deprecated because it actually reinitializes the texture. Use Texture2D.Reinitialize(int, int, GraphicsFormat, bool) instead (UnityUpgradable) -> Reinitialize([*] System.Int32, [*] System.Int32, UnityEngine.Experimental.Rendering.GraphicsFormat, [*] System.Boolean)", false)]
		public bool Resize(int width, int height, GraphicsFormat format, bool hasMipMap)
		{
			return this.Reinitialize(width, height, format, hasMipMap);
		}

		// Token: 0x0600185A RID: 6234 RVA: 0x0002A678 File Offset: 0x00028878
		public void ReadPixels(Rect source, int destX, int destY, [DefaultValue("true")] bool recalculateMipMaps)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			this.ReadPixelsImpl(source, destX, destY, recalculateMipMaps);
		}

		// Token: 0x0600185B RID: 6235 RVA: 0x0002A6A7 File Offset: 0x000288A7
		[ExcludeFromDocs]
		public void ReadPixels(Rect source, int destX, int destY)
		{
			this.ReadPixels(source, destX, destY, true);
		}

		// Token: 0x0600185C RID: 6236 RVA: 0x0002A6B8 File Offset: 0x000288B8
		public static bool GenerateAtlas(Vector2[] sizes, int padding, int atlasSize, List<Rect> results)
		{
			bool flag = sizes == null;
			if (flag)
			{
				throw new ArgumentException("sizes array can not be null");
			}
			bool flag2 = results == null;
			if (flag2)
			{
				throw new ArgumentException("results list cannot be null");
			}
			bool flag3 = padding < 0;
			if (flag3)
			{
				throw new ArgumentException("padding can not be negative");
			}
			bool flag4 = atlasSize <= 0;
			if (flag4)
			{
				throw new ArgumentException("atlas size must be positive");
			}
			results.Clear();
			bool flag5 = sizes.Length == 0;
			bool flag6;
			if (flag5)
			{
				flag6 = true;
			}
			else
			{
				NoAllocHelpers.EnsureListElemCount<Rect>(results, sizes.Length);
				Texture2D.GenerateAtlasImpl(sizes, padding, atlasSize, NoAllocHelpers.ExtractArrayFromList<Rect>(results));
				flag6 = results.Count != 0;
			}
			return flag6;
		}

		// Token: 0x0600185D RID: 6237 RVA: 0x0002A754 File Offset: 0x00028954
		public void SetPixels32(Color32[] colors, [DefaultValue("0")] int miplevel)
		{
			this.SetAllPixels32(colors, miplevel);
		}

		// Token: 0x0600185E RID: 6238 RVA: 0x0002A760 File Offset: 0x00028960
		[ExcludeFromDocs]
		public void SetPixels32(Color32[] colors)
		{
			this.SetPixels32(colors, 0);
		}

		// Token: 0x0600185F RID: 6239 RVA: 0x0002A76C File Offset: 0x0002896C
		public void SetPixels32(int x, int y, int blockWidth, int blockHeight, Color32[] colors, [DefaultValue("0")] int miplevel)
		{
			this.SetBlockOfPixels32(x, y, blockWidth, blockHeight, colors, miplevel);
		}

		// Token: 0x06001860 RID: 6240 RVA: 0x0002A77F File Offset: 0x0002897F
		[ExcludeFromDocs]
		public void SetPixels32(int x, int y, int blockWidth, int blockHeight, Color32[] colors)
		{
			this.SetPixels32(x, y, blockWidth, blockHeight, colors, 0);
		}

		// Token: 0x06001861 RID: 6241 RVA: 0x0002A794 File Offset: 0x00028994
		public Color[] GetPixels([DefaultValue("0")] int miplevel)
		{
			int num = this.width >> miplevel;
			bool flag = num < 1;
			if (flag)
			{
				num = 1;
			}
			int num2 = this.height >> miplevel;
			bool flag2 = num2 < 1;
			if (flag2)
			{
				num2 = 1;
			}
			return this.GetPixels(0, 0, num, num2, miplevel);
		}

		// Token: 0x06001862 RID: 6242 RVA: 0x0002A7E0 File Offset: 0x000289E0
		[ExcludeFromDocs]
		public Color[] GetPixels()
		{
			return this.GetPixels(0);
		}

		// Token: 0x06001863 RID: 6243 RVA: 0x0002A7FC File Offset: 0x000289FC
		public void CopyPixels(Texture src)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			bool flag2 = !src.isReadable;
			if (flag2)
			{
				throw base.CreateNonReadableException(src);
			}
			this.CopyPixels_Full(src);
		}

		// Token: 0x06001864 RID: 6244 RVA: 0x0002A83C File Offset: 0x00028A3C
		public void CopyPixels(Texture src, int srcElement, int srcMip, int dstMip)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			bool flag2 = !src.isReadable;
			if (flag2)
			{
				throw base.CreateNonReadableException(src);
			}
			this.CopyPixels_Slice(src, srcElement, srcMip, dstMip);
		}

		// Token: 0x06001865 RID: 6245 RVA: 0x0002A880 File Offset: 0x00028A80
		public void CopyPixels(Texture src, int srcElement, int srcMip, int srcX, int srcY, int srcWidth, int srcHeight, int dstMip, int dstX, int dstY)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			bool flag2 = !src.isReadable;
			if (flag2)
			{
				throw base.CreateNonReadableException(src);
			}
			this.CopyPixels_Region(src, srcElement, srcMip, srcX, srcY, srcWidth, srcHeight, dstMip, dstX, dstY);
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06001866 RID: 6246 RVA: 0x0002A8D0 File Offset: 0x00028AD0
		// (set) Token: 0x06001867 RID: 6247 RVA: 0x0002A8E8 File Offset: 0x00028AE8
		public bool ignoreMipmapLimit
		{
			get
			{
				return this.IgnoreMipmapLimit();
			}
			set
			{
				bool flag = !this.isReadable;
				if (flag)
				{
					throw base.IgnoreMipmapLimitCannotBeToggledException(this);
				}
				this.SetIgnoreMipmapLimitAndReload(value);
			}
		}

		// Token: 0x06001868 RID: 6248
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern TextureFormat get_format_Injected(IntPtr _unity_self);

		// Token: 0x06001869 RID: 6249
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IgnoreMipmapLimit_Injected(IntPtr _unity_self);

		// Token: 0x0600186A RID: 6250
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetIgnoreMipmapLimitAndReload_Injected(IntPtr _unity_self, bool value);

		// Token: 0x0600186B RID: 6251
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_mipmapLimitGroup_Injected(IntPtr _unity_self, out ManagedSpanWrapper ret);

		// Token: 0x0600186C RID: 6252
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_activeMipmapLimit_Injected(IntPtr _unity_self);

		// Token: 0x0600186D RID: 6253
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_whiteTexture_Injected();

		// Token: 0x0600186E RID: 6254
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_blackTexture_Injected();

		// Token: 0x0600186F RID: 6255
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_redTexture_Injected();

		// Token: 0x06001870 RID: 6256
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_grayTexture_Injected();

		// Token: 0x06001871 RID: 6257
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_linearGrayTexture_Injected();

		// Token: 0x06001872 RID: 6258
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_normalTexture_Injected();

		// Token: 0x06001873 RID: 6259
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Compress_Injected(IntPtr _unity_self, bool highQuality);

		// Token: 0x06001874 RID: 6260
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Internal_CreateImpl_Injected([Writable] Texture2D mono, int w, int h, int mipCount, GraphicsFormat format, TextureColorSpace colorSpace, TextureCreationFlags flags, IntPtr nativeTex, bool ignoreMipmapLimit, ref ManagedSpanWrapper mipmapLimitGroupName);

		// Token: 0x06001875 RID: 6261
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isReadable_Injected(IntPtr _unity_self);

		// Token: 0x06001876 RID: 6262
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_vtOnly_Injected(IntPtr _unity_self);

		// Token: 0x06001877 RID: 6263
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ApplyImpl_Injected(IntPtr _unity_self, bool updateMipmaps, bool makeNoLongerReadable);

		// Token: 0x06001878 RID: 6264
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ReinitializeImpl_Injected(IntPtr _unity_self, int width, int height);

		// Token: 0x06001879 RID: 6265
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPixelImpl_Injected(IntPtr _unity_self, int image, int mip, int x, int y, [In] ref Color color);

		// Token: 0x0600187A RID: 6266
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPixelImpl_Injected(IntPtr _unity_self, int image, int mip, int x, int y, out Color ret);

		// Token: 0x0600187B RID: 6267
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPixelBilinearImpl_Injected(IntPtr _unity_self, int image, int mip, float u, float v, out Color ret);

		// Token: 0x0600187C RID: 6268
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ReinitializeWithFormatImpl_Injected(IntPtr _unity_self, int width, int height, GraphicsFormat format, bool hasMipMap);

		// Token: 0x0600187D RID: 6269
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ReinitializeWithTextureFormatImpl_Injected(IntPtr _unity_self, int width, int height, TextureFormat textureFormat, bool hasMipMap);

		// Token: 0x0600187E RID: 6270
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReadPixelsImpl_Injected(IntPtr _unity_self, [In] ref Rect source, int destX, int destY, bool recalculateMipMaps);

		// Token: 0x0600187F RID: 6271
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPixelsImpl_Injected(IntPtr _unity_self, int x, int y, int w, int h, ref ManagedSpanWrapper pixel, int miplevel, int frame);

		// Token: 0x06001880 RID: 6272
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool LoadRawTextureDataImpl_Injected(IntPtr _unity_self, IntPtr data, ulong size);

		// Token: 0x06001881 RID: 6273
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool LoadRawTextureDataImplArray_Injected(IntPtr _unity_self, ref ManagedSpanWrapper data);

		// Token: 0x06001882 RID: 6274
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetPixelDataImplArray_Injected(IntPtr _unity_self, Array data, int mipLevel, int elementSize, int dataArraySize, int sourceDataStartIndex);

		// Token: 0x06001883 RID: 6275
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetPixelDataImpl_Injected(IntPtr _unity_self, IntPtr data, int mipLevel, int elementSize, int dataArraySize, int sourceDataStartIndex);

		// Token: 0x06001884 RID: 6276
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetWritableImageData_Injected(IntPtr _unity_self, int frame);

		// Token: 0x06001885 RID: 6277
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ulong GetImageDataSize_Injected(IntPtr _unity_self);

		// Token: 0x06001886 RID: 6278
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GenerateAtlasImpl_Injected(ref ManagedSpanWrapper sizes, int padding, int atlasSize, out BlittableArrayWrapper rect);

		// Token: 0x06001887 RID: 6279
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isPreProcessed_Injected(IntPtr _unity_self);

		// Token: 0x06001888 RID: 6280
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_streamingMipmaps_Injected(IntPtr _unity_self);

		// Token: 0x06001889 RID: 6281
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_streamingMipmapsPriority_Injected(IntPtr _unity_self);

		// Token: 0x0600188A RID: 6282
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_requestedMipmapLevel_Injected(IntPtr _unity_self);

		// Token: 0x0600188B RID: 6283
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_requestedMipmapLevel_Injected(IntPtr _unity_self, int value);

		// Token: 0x0600188C RID: 6284
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_minimumMipmapLevel_Injected(IntPtr _unity_self);

		// Token: 0x0600188D RID: 6285
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_minimumMipmapLevel_Injected(IntPtr _unity_self, int value);

		// Token: 0x0600188E RID: 6286
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_loadAllMips_Injected(IntPtr _unity_self);

		// Token: 0x0600188F RID: 6287
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_loadAllMips_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06001890 RID: 6288
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_calculatedMipmapLevel_Injected(IntPtr _unity_self);

		// Token: 0x06001891 RID: 6289
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_desiredMipmapLevel_Injected(IntPtr _unity_self);

		// Token: 0x06001892 RID: 6290
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_loadingMipmapLevel_Injected(IntPtr _unity_self);

		// Token: 0x06001893 RID: 6291
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_loadedMipmapLevel_Injected(IntPtr _unity_self);

		// Token: 0x06001894 RID: 6292
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClearRequestedMipmapLevel_Injected(IntPtr _unity_self);

		// Token: 0x06001895 RID: 6293
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsRequestedMipmapLevelLoaded_Injected(IntPtr _unity_self);

		// Token: 0x06001896 RID: 6294
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClearMinimumMipmapLevel_Injected(IntPtr _unity_self);

		// Token: 0x06001897 RID: 6295
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UpdateExternalTexture_Injected(IntPtr _unity_self, IntPtr nativeTex);

		// Token: 0x06001898 RID: 6296
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetAllPixels32_Injected(IntPtr _unity_self, ref ManagedSpanWrapper colors, int miplevel);

		// Token: 0x06001899 RID: 6297
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetBlockOfPixels32_Injected(IntPtr _unity_self, int x, int y, int blockWidth, int blockHeight, ref ManagedSpanWrapper colors, int miplevel);

		// Token: 0x0600189A RID: 6298
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern byte[] GetRawTextureData_Injected(IntPtr _unity_self);

		// Token: 0x0600189B RID: 6299
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Color[] GetPixels_Injected(IntPtr _unity_self, int x, int y, int blockWidth, int blockHeight, [DefaultValue("0")] int miplevel);

		// Token: 0x0600189C RID: 6300
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Color32[] GetPixels32_Injected(IntPtr _unity_self, [DefaultValue("0")] int miplevel);

		// Token: 0x0600189D RID: 6301
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Rect[] PackTextures_Injected(IntPtr _unity_self, Texture2D[] textures, int padding, int maximumAtlasSize, bool makeNoLongerReadable);

		// Token: 0x0600189E RID: 6302
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyPixels_Full_Injected(IntPtr _unity_self, IntPtr src);

		// Token: 0x0600189F RID: 6303
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyPixels_Slice_Injected(IntPtr _unity_self, IntPtr src, int srcElement, int srcMip, int dstMip);

		// Token: 0x060018A0 RID: 6304
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyPixels_Region_Injected(IntPtr _unity_self, IntPtr src, int srcElement, int srcMip, int srcX, int srcY, int srcWidth, int srcHeight, int dstMip, int dstX, int dstY);

		// Token: 0x040006DD RID: 1757
		internal const int streamingMipmapsPriorityMin = -128;

		// Token: 0x040006DE RID: 1758
		internal const int streamingMipmapsPriorityMax = 127;

		// Token: 0x020001F3 RID: 499
		[Flags]
		public enum EXRFlags
		{
			// Token: 0x040006E0 RID: 1760
			None = 0,
			// Token: 0x040006E1 RID: 1761
			OutputAsFloat = 1,
			// Token: 0x040006E2 RID: 1762
			CompressZIP = 2,
			// Token: 0x040006E3 RID: 1763
			CompressRLE = 4,
			// Token: 0x040006E4 RID: 1764
			CompressPIZ = 8
		}
	}
}
