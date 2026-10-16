using System;
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
	// Token: 0x020001F4 RID: 500
	[NativeHeader("Runtime/Graphics/CubemapTexture.h")]
	[ExcludeFromPreset]
	public sealed class Cubemap : Texture
	{
		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x060018A1 RID: 6305 RVA: 0x0002A914 File Offset: 0x00028B14
		public TextureFormat format
		{
			[NativeName("GetTextureFormat")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cubemap>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Cubemap.get_format_Injected(intPtr);
			}
		}

		// Token: 0x060018A2 RID: 6306
		[FreeFunction("CubemapScripting::Create")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Internal_CreateImpl([Writable] Cubemap mono, int ext, int mipCount, GraphicsFormat format, TextureColorSpace colorSpace, TextureCreationFlags flags, IntPtr nativeTex);

		// Token: 0x060018A3 RID: 6307 RVA: 0x0002A938 File Offset: 0x00028B38
		private static void Internal_Create([Writable] Cubemap mono, int ext, int mipCount, GraphicsFormat format, TextureColorSpace colorSpace, TextureCreationFlags flags, IntPtr nativeTex)
		{
			bool flag = !Cubemap.Internal_CreateImpl(mono, ext, mipCount, format, colorSpace, flags, nativeTex);
			if (flag)
			{
				throw new UnityException("Failed to create texture because of invalid parameters.");
			}
		}

		// Token: 0x060018A4 RID: 6308 RVA: 0x0002A968 File Offset: 0x00028B68
		[FreeFunction(Name = "CubemapScripting::Apply", HasExplicitThis = true)]
		private void ApplyImpl(bool updateMipmaps, bool makeNoLongerReadable)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cubemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Cubemap.ApplyImpl_Injected(intPtr, updateMipmaps, makeNoLongerReadable);
		}

		// Token: 0x060018A5 RID: 6309 RVA: 0x0002A98C File Offset: 0x00028B8C
		[FreeFunction("CubemapScripting::UpdateExternalTexture", HasExplicitThis = true)]
		public void UpdateExternalTexture(IntPtr nativeTexture)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cubemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Cubemap.UpdateExternalTexture_Injected(intPtr, nativeTexture);
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x060018A6 RID: 6310 RVA: 0x0002A9B0 File Offset: 0x00028BB0
		public override bool isReadable
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cubemap>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Cubemap.get_isReadable_Injected(intPtr);
			}
		}

		// Token: 0x060018A7 RID: 6311 RVA: 0x0002A9D4 File Offset: 0x00028BD4
		[NativeName("SetPixel")]
		private void SetPixelImpl(int image, int mip, int x, int y, Color color)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cubemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Cubemap.SetPixelImpl_Injected(intPtr, image, mip, x, y, ref color);
		}

		// Token: 0x060018A8 RID: 6312 RVA: 0x0002AA00 File Offset: 0x00028C00
		[NativeName("GetPixel")]
		private Color GetPixelImpl(int image, int mip, int x, int y)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cubemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Color color;
			Cubemap.GetPixelImpl_Injected(intPtr, image, mip, x, y, out color);
			return color;
		}

		// Token: 0x060018A9 RID: 6313 RVA: 0x0002AA2C File Offset: 0x00028C2C
		[NativeName("FixupEdges")]
		public void SmoothEdges([DefaultValue("1")] int smoothRegionWidthInPixels)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cubemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Cubemap.SmoothEdges_Injected(intPtr, smoothRegionWidthInPixels);
		}

		// Token: 0x060018AA RID: 6314 RVA: 0x0002AA4F File Offset: 0x00028C4F
		public void SmoothEdges()
		{
			this.SmoothEdges(1);
		}

		// Token: 0x060018AB RID: 6315 RVA: 0x0002AA5C File Offset: 0x00028C5C
		[FreeFunction(Name = "CubemapScripting::GetPixels", HasExplicitThis = true, ThrowsException = true)]
		[return: Unmarshalled]
		public Color[] GetPixels(CubemapFace face, int miplevel)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cubemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Cubemap.GetPixels_Injected(intPtr, face, miplevel);
		}

		// Token: 0x060018AC RID: 6316 RVA: 0x0002AA80 File Offset: 0x00028C80
		public Color[] GetPixels(CubemapFace face)
		{
			return this.GetPixels(face, 0);
		}

		// Token: 0x060018AD RID: 6317 RVA: 0x0002AA9C File Offset: 0x00028C9C
		[FreeFunction(Name = "CubemapScripting::SetPixels", HasExplicitThis = true, ThrowsException = true)]
		public unsafe void SetPixels(Color[] colors, CubemapFace face, int miplevel)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cubemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<Color> span = new Span<Color>(colors);
			fixed (Color* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				Cubemap.SetPixels_Injected(intPtr, ref managedSpanWrapper, face, miplevel);
			}
		}

		// Token: 0x060018AE RID: 6318 RVA: 0x0002AAE8 File Offset: 0x00028CE8
		[FreeFunction(Name = "CubemapScripting::SetPixelDataArray", HasExplicitThis = true, ThrowsException = true)]
		private bool SetPixelDataImplArray(Array data, int mipLevel, int face, int elementSize, int dataArraySize, int sourceDataStartIndex = 0)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cubemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Cubemap.SetPixelDataImplArray_Injected(intPtr, data, mipLevel, face, elementSize, dataArraySize, sourceDataStartIndex);
		}

		// Token: 0x060018AF RID: 6319 RVA: 0x0002AB14 File Offset: 0x00028D14
		[FreeFunction(Name = "CubemapScripting::SetPixelData", HasExplicitThis = true, ThrowsException = true)]
		private bool SetPixelDataImpl(IntPtr data, int mipLevel, int face, int elementSize, int dataArraySize, int sourceDataStartIndex = 0)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cubemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Cubemap.SetPixelDataImpl_Injected(intPtr, data, mipLevel, face, elementSize, dataArraySize, sourceDataStartIndex);
		}

		// Token: 0x060018B0 RID: 6320 RVA: 0x0002AB3F File Offset: 0x00028D3F
		public void SetPixels(Color[] colors, CubemapFace face)
		{
			this.SetPixels(colors, face, 0);
		}

		// Token: 0x060018B1 RID: 6321 RVA: 0x0002AB4C File Offset: 0x00028D4C
		[FreeFunction(Name = "CubemapScripting::CopyPixels", HasExplicitThis = true, ThrowsException = true)]
		private void CopyPixels_Full(Texture src)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cubemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Cubemap.CopyPixels_Full_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Texture>(src));
		}

		// Token: 0x060018B2 RID: 6322 RVA: 0x0002AB74 File Offset: 0x00028D74
		[FreeFunction(Name = "CubemapScripting::CopyPixels", HasExplicitThis = true, ThrowsException = true)]
		private void CopyPixels_Slice(Texture src, int srcElement, int srcMip, int dstFace, int dstMip)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cubemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Cubemap.CopyPixels_Slice_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Texture>(src), srcElement, srcMip, dstFace, dstMip);
		}

		// Token: 0x060018B3 RID: 6323 RVA: 0x0002ABA4 File Offset: 0x00028DA4
		[FreeFunction(Name = "CubemapScripting::CopyPixels", HasExplicitThis = true, ThrowsException = true)]
		private void CopyPixels_Region(Texture src, int srcElement, int srcMip, int srcX, int srcY, int srcWidth, int srcHeight, int dstFace, int dstMip, int dstX, int dstY)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cubemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Cubemap.CopyPixels_Region_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Texture>(src), srcElement, srcMip, srcX, srcY, srcWidth, srcHeight, dstFace, dstMip, dstX, dstY);
		}

		// Token: 0x060018B4 RID: 6324 RVA: 0x0002ABE0 File Offset: 0x00028DE0
		private IntPtr GetWritableImageData(int frame)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cubemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Cubemap.GetWritableImageData_Injected(intPtr, frame);
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x060018B5 RID: 6325 RVA: 0x0002AC04 File Offset: 0x00028E04
		internal bool isPreProcessed
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cubemap>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Cubemap.get_isPreProcessed_Injected(intPtr);
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x060018B6 RID: 6326 RVA: 0x0002AC28 File Offset: 0x00028E28
		public bool streamingMipmaps
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cubemap>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Cubemap.get_streamingMipmaps_Injected(intPtr);
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x060018B7 RID: 6327 RVA: 0x0002AC4C File Offset: 0x00028E4C
		public int streamingMipmapsPriority
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cubemap>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Cubemap.get_streamingMipmapsPriority_Injected(intPtr);
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x060018B8 RID: 6328 RVA: 0x0002AC70 File Offset: 0x00028E70
		// (set) Token: 0x060018B9 RID: 6329 RVA: 0x0002AC94 File Offset: 0x00028E94
		public int requestedMipmapLevel
		{
			[FreeFunction(Name = "GetTextureStreamingManager().GetRequestedMipmapLevel", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cubemap>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Cubemap.get_requestedMipmapLevel_Injected(intPtr);
			}
			[FreeFunction(Name = "GetTextureStreamingManager().SetRequestedMipmapLevel", HasExplicitThis = true)]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cubemap>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Cubemap.set_requestedMipmapLevel_Injected(intPtr, value);
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x060018BA RID: 6330 RVA: 0x0002ACB8 File Offset: 0x00028EB8
		// (set) Token: 0x060018BB RID: 6331 RVA: 0x0002ACDC File Offset: 0x00028EDC
		internal bool loadAllMips
		{
			[FreeFunction(Name = "GetTextureStreamingManager().GetLoadAllMips", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cubemap>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Cubemap.get_loadAllMips_Injected(intPtr);
			}
			[FreeFunction(Name = "GetTextureStreamingManager().SetLoadAllMips", HasExplicitThis = true)]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cubemap>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Cubemap.set_loadAllMips_Injected(intPtr, value);
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x060018BC RID: 6332 RVA: 0x0002AD00 File Offset: 0x00028F00
		public int desiredMipmapLevel
		{
			[FreeFunction(Name = "GetTextureStreamingManager().GetDesiredMipmapLevel", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cubemap>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Cubemap.get_desiredMipmapLevel_Injected(intPtr);
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x060018BD RID: 6333 RVA: 0x0002AD24 File Offset: 0x00028F24
		public int loadingMipmapLevel
		{
			[FreeFunction(Name = "GetTextureStreamingManager().GetLoadingMipmapLevel", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cubemap>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Cubemap.get_loadingMipmapLevel_Injected(intPtr);
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x060018BE RID: 6334 RVA: 0x0002AD48 File Offset: 0x00028F48
		public int loadedMipmapLevel
		{
			[FreeFunction(Name = "GetTextureStreamingManager().GetLoadedMipmapLevel", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cubemap>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Cubemap.get_loadedMipmapLevel_Injected(intPtr);
			}
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x0002AD6C File Offset: 0x00028F6C
		[FreeFunction(Name = "GetTextureStreamingManager().ClearRequestedMipmapLevel", HasExplicitThis = true)]
		public void ClearRequestedMipmapLevel()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cubemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Cubemap.ClearRequestedMipmapLevel_Injected(intPtr);
		}

		// Token: 0x060018C0 RID: 6336 RVA: 0x0002AD90 File Offset: 0x00028F90
		[FreeFunction(Name = "GetTextureStreamingManager().IsRequestedMipmapLevelLoaded", HasExplicitThis = true)]
		public bool IsRequestedMipmapLevelLoaded()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cubemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Cubemap.IsRequestedMipmapLevelLoaded_Injected(intPtr);
		}

		// Token: 0x060018C1 RID: 6337 RVA: 0x0002ADB4 File Offset: 0x00028FB4
		internal bool ValidateFormat(TextureFormat format, int width)
		{
			bool flag = base.ValidateFormat(format);
			bool flag2 = flag;
			if (flag2)
			{
				bool flag3 = TextureFormat.PVRTC_RGB2 <= format && format <= TextureFormat.PVRTC_RGBA4;
				bool flag4 = flag3 && !Mathf.IsPowerOfTwo(width);
				if (flag4)
				{
					throw new UnityException(string.Format("'{0}' demands texture to have power-of-two dimensions", format.ToString()));
				}
			}
			return flag;
		}

		// Token: 0x060018C2 RID: 6338 RVA: 0x0002AE1C File Offset: 0x0002901C
		internal bool ValidateFormat(GraphicsFormat format, int width)
		{
			bool flag = base.ValidateFormat(format, GraphicsFormatUsage.Sample);
			bool flag2 = flag;
			if (flag2)
			{
				bool flag3 = GraphicsFormatUtility.IsPVRTCFormat(format);
				bool flag4 = flag3 && !Mathf.IsPowerOfTwo(width);
				if (flag4)
				{
					throw new UnityException(string.Format("'{0}' demands texture to have power-of-two dimensions", format.ToString()));
				}
			}
			return flag;
		}

		// Token: 0x060018C3 RID: 6339 RVA: 0x0002AE78 File Offset: 0x00029078
		[ExcludeFromDocs]
		public Cubemap(int width, DefaultFormat format, TextureCreationFlags flags)
			: this(width, SystemInfo.GetGraphicsFormat(format), flags)
		{
		}

		// Token: 0x060018C4 RID: 6340 RVA: 0x0002AE8A File Offset: 0x0002908A
		[ExcludeFromDocs]
		public Cubemap(int width, DefaultFormat format, TextureCreationFlags flags, int mipCount)
			: this(width, SystemInfo.GetGraphicsFormat(format), flags, mipCount)
		{
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x0002AE9E File Offset: 0x0002909E
		[ExcludeFromDocs]
		[RequiredByNativeCode]
		public Cubemap(int width, GraphicsFormat format, TextureCreationFlags flags)
			: this(width, format, flags, Texture.GenerateAllMips)
		{
		}

		// Token: 0x060018C6 RID: 6342 RVA: 0x0002AEB0 File Offset: 0x000290B0
		[ExcludeFromDocs]
		public Cubemap(int width, GraphicsFormat format, TextureCreationFlags flags, int mipCount)
		{
			bool flag = !this.ValidateFormat(format, width);
			if (!flag)
			{
				Cubemap.ValidateIsNotCrunched(flags);
				Cubemap.Internal_Create(this, width, mipCount, format, base.GetTextureColorSpace(format), flags, IntPtr.Zero);
			}
		}

		// Token: 0x060018C7 RID: 6343 RVA: 0x0002AEF8 File Offset: 0x000290F8
		internal Cubemap(int width, TextureFormat textureFormat, int mipCount, IntPtr nativeTex, bool createUninitialized)
		{
			bool flag = !this.ValidateFormat(textureFormat, width);
			if (!flag)
			{
				GraphicsFormat graphicsFormat = GraphicsFormatUtility.GetGraphicsFormat(textureFormat, false);
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
				Cubemap.ValidateIsNotCrunched(textureCreationFlags);
				Cubemap.Internal_Create(this, width, mipCount, graphicsFormat, base.GetTextureColorSpace(true), textureCreationFlags, nativeTex);
			}
		}

		// Token: 0x060018C8 RID: 6344 RVA: 0x0002AF69 File Offset: 0x00029169
		public Cubemap(int width, TextureFormat textureFormat, bool mipChain)
			: this(width, textureFormat, mipChain ? Texture.GenerateAllMips : 1, IntPtr.Zero, false)
		{
		}

		// Token: 0x060018C9 RID: 6345 RVA: 0x0002AF86 File Offset: 0x00029186
		public Cubemap(int width, TextureFormat textureFormat, bool mipChain, [DefaultValue("false")] bool createUninitialized)
			: this(width, textureFormat, mipChain ? Texture.GenerateAllMips : 1, IntPtr.Zero, createUninitialized)
		{
		}

		// Token: 0x060018CA RID: 6346 RVA: 0x0002AFA4 File Offset: 0x000291A4
		public Cubemap(int width, TextureFormat format, int mipCount)
			: this(width, format, mipCount, IntPtr.Zero, false)
		{
		}

		// Token: 0x060018CB RID: 6347 RVA: 0x0002AFB7 File Offset: 0x000291B7
		public Cubemap(int width, TextureFormat format, int mipCount, [DefaultValue("false")] bool createUninitialized)
			: this(width, format, mipCount, IntPtr.Zero, createUninitialized)
		{
		}

		// Token: 0x060018CC RID: 6348 RVA: 0x0002AFCC File Offset: 0x000291CC
		public static Cubemap CreateExternalTexture(int width, TextureFormat format, bool mipmap, IntPtr nativeTex)
		{
			bool flag = nativeTex == IntPtr.Zero;
			if (flag)
			{
				throw new ArgumentException("nativeTex can not be null");
			}
			return new Cubemap(width, format, mipmap ? Texture.GenerateAllMips : 1, nativeTex, false);
		}

		// Token: 0x060018CD RID: 6349 RVA: 0x0002B00C File Offset: 0x0002920C
		public void SetPixelData<T>(T[] data, int mipLevel, CubemapFace face, [DefaultValue("0")] int sourceDataStartIndex = 0)
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
			this.SetPixelDataImplArray(data, mipLevel, (int)face, Marshal.SizeOf<T>(data[0]), data.Length, sourceDataStartIndex);
		}

		// Token: 0x060018CE RID: 6350 RVA: 0x0002B078 File Offset: 0x00029278
		public void SetPixelData<T>(NativeArray<T> data, int mipLevel, CubemapFace face, [DefaultValue("0")] int sourceDataStartIndex = 0) where T : struct
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
			this.SetPixelDataImpl((IntPtr)data.GetUnsafeReadOnlyPtr<T>(), mipLevel, (int)face, UnsafeUtility.SizeOf<T>(), data.Length, sourceDataStartIndex);
		}

		// Token: 0x060018CF RID: 6351 RVA: 0x0002B0F8 File Offset: 0x000292F8
		public unsafe NativeArray<T> GetPixelData<T>(int mipLevel, CubemapFace face) where T : struct
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			bool flag2 = mipLevel < 0 || mipLevel >= base.mipmapCount;
			if (flag2)
			{
				throw new ArgumentException("The passed in miplevel " + mipLevel.ToString() + " is invalid. The valid range is 0 through " + (base.mipmapCount - 1).ToString());
			}
			bool flag3 = face < CubemapFace.PositiveX || face >= (CubemapFace)6;
			if (flag3)
			{
				throw new ArgumentException("The passed in face " + face.ToString() + " is invalid. The valid range is 0 through 5.");
			}
			bool flag4 = this.GetWritableImageData(0).ToInt64() == 0L;
			if (flag4)
			{
				throw new UnityException("Texture '" + base.name + "' has no data.");
			}
			ulong pixelDataOffset = base.GetPixelDataOffset(base.mipmapCount, (int)face);
			ulong pixelDataOffset2 = base.GetPixelDataOffset(mipLevel, (int)face);
			ulong pixelDataSize = base.GetPixelDataSize(mipLevel, (int)face);
			int num = UnsafeUtility.SizeOf<T>();
			ulong num2 = pixelDataSize / (ulong)((long)num);
			bool flag5 = num2 > 2147483647UL;
			if (flag5)
			{
				throw base.CreateNativeArrayLengthOverflowException();
			}
			IntPtr intPtr = new IntPtr((long)this.GetWritableImageData(0) + (long)(pixelDataOffset * (ulong)((long)face) + pixelDataOffset2));
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>((void*)intPtr, (int)num2, Allocator.None);
		}

		// Token: 0x060018D0 RID: 6352 RVA: 0x0002B245 File Offset: 0x00029445
		[ExcludeFromDocs]
		public void SetPixel(CubemapFace face, int x, int y, Color color)
		{
			this.SetPixel(face, x, y, color, 0);
		}

		// Token: 0x060018D1 RID: 6353 RVA: 0x0002B258 File Offset: 0x00029458
		public void SetPixel(CubemapFace face, int x, int y, Color color, [DefaultValue("0")] int mip)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			this.SetPixelImpl((int)face, mip, x, y, color);
		}

		// Token: 0x060018D2 RID: 6354 RVA: 0x0002B28C File Offset: 0x0002948C
		[ExcludeFromDocs]
		public Color GetPixel(CubemapFace face, int x, int y)
		{
			return this.GetPixel(face, x, y, 0);
		}

		// Token: 0x060018D3 RID: 6355 RVA: 0x0002B2A8 File Offset: 0x000294A8
		public Color GetPixel(CubemapFace face, int x, int y, [DefaultValue("0")] int mip)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			return this.GetPixelImpl((int)face, mip, x, y);
		}

		// Token: 0x060018D4 RID: 6356 RVA: 0x0002B2DC File Offset: 0x000294DC
		public void Apply([DefaultValue("true")] bool updateMipmaps, [DefaultValue("false")] bool makeNoLongerReadable)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			this.ApplyImpl(updateMipmaps, makeNoLongerReadable);
		}

		// Token: 0x060018D5 RID: 6357 RVA: 0x0002B308 File Offset: 0x00029508
		[ExcludeFromDocs]
		public void Apply(bool updateMipmaps)
		{
			this.Apply(updateMipmaps, false);
		}

		// Token: 0x060018D6 RID: 6358 RVA: 0x0002B314 File Offset: 0x00029514
		[ExcludeFromDocs]
		public void Apply()
		{
			this.Apply(true, false);
		}

		// Token: 0x060018D7 RID: 6359 RVA: 0x0002B320 File Offset: 0x00029520
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

		// Token: 0x060018D8 RID: 6360 RVA: 0x0002B360 File Offset: 0x00029560
		public void CopyPixels(Texture src, int srcElement, int srcMip, CubemapFace dstFace, int dstMip)
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
			this.CopyPixels_Slice(src, srcElement, srcMip, (int)dstFace, dstMip);
		}

		// Token: 0x060018D9 RID: 6361 RVA: 0x0002B3A8 File Offset: 0x000295A8
		public void CopyPixels(Texture src, int srcElement, int srcMip, int srcX, int srcY, int srcWidth, int srcHeight, CubemapFace dstFace, int dstMip, int dstX, int dstY)
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
			this.CopyPixels_Region(src, srcElement, srcMip, srcX, srcY, srcWidth, srcHeight, (int)dstFace, dstMip, dstX, dstY);
		}

		// Token: 0x060018DA RID: 6362 RVA: 0x0002B3FC File Offset: 0x000295FC
		private static void ValidateIsNotCrunched(TextureCreationFlags flags)
		{
			bool flag = (flags &= TextureCreationFlags.Crunch) > TextureCreationFlags.None;
			if (flag)
			{
				throw new ArgumentException("Crunched Cubemap is not supported for textures created from script.");
			}
		}

		// Token: 0x060018DB RID: 6363
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern TextureFormat get_format_Injected(IntPtr _unity_self);

		// Token: 0x060018DC RID: 6364
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ApplyImpl_Injected(IntPtr _unity_self, bool updateMipmaps, bool makeNoLongerReadable);

		// Token: 0x060018DD RID: 6365
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UpdateExternalTexture_Injected(IntPtr _unity_self, IntPtr nativeTexture);

		// Token: 0x060018DE RID: 6366
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isReadable_Injected(IntPtr _unity_self);

		// Token: 0x060018DF RID: 6367
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPixelImpl_Injected(IntPtr _unity_self, int image, int mip, int x, int y, [In] ref Color color);

		// Token: 0x060018E0 RID: 6368
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPixelImpl_Injected(IntPtr _unity_self, int image, int mip, int x, int y, out Color ret);

		// Token: 0x060018E1 RID: 6369
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SmoothEdges_Injected(IntPtr _unity_self, [DefaultValue("1")] int smoothRegionWidthInPixels);

		// Token: 0x060018E2 RID: 6370
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Color[] GetPixels_Injected(IntPtr _unity_self, CubemapFace face, int miplevel);

		// Token: 0x060018E3 RID: 6371
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPixels_Injected(IntPtr _unity_self, ref ManagedSpanWrapper colors, CubemapFace face, int miplevel);

		// Token: 0x060018E4 RID: 6372
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetPixelDataImplArray_Injected(IntPtr _unity_self, Array data, int mipLevel, int face, int elementSize, int dataArraySize, int sourceDataStartIndex);

		// Token: 0x060018E5 RID: 6373
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetPixelDataImpl_Injected(IntPtr _unity_self, IntPtr data, int mipLevel, int face, int elementSize, int dataArraySize, int sourceDataStartIndex);

		// Token: 0x060018E6 RID: 6374
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyPixels_Full_Injected(IntPtr _unity_self, IntPtr src);

		// Token: 0x060018E7 RID: 6375
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyPixels_Slice_Injected(IntPtr _unity_self, IntPtr src, int srcElement, int srcMip, int dstFace, int dstMip);

		// Token: 0x060018E8 RID: 6376
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyPixels_Region_Injected(IntPtr _unity_self, IntPtr src, int srcElement, int srcMip, int srcX, int srcY, int srcWidth, int srcHeight, int dstFace, int dstMip, int dstX, int dstY);

		// Token: 0x060018E9 RID: 6377
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetWritableImageData_Injected(IntPtr _unity_self, int frame);

		// Token: 0x060018EA RID: 6378
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isPreProcessed_Injected(IntPtr _unity_self);

		// Token: 0x060018EB RID: 6379
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_streamingMipmaps_Injected(IntPtr _unity_self);

		// Token: 0x060018EC RID: 6380
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_streamingMipmapsPriority_Injected(IntPtr _unity_self);

		// Token: 0x060018ED RID: 6381
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_requestedMipmapLevel_Injected(IntPtr _unity_self);

		// Token: 0x060018EE RID: 6382
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_requestedMipmapLevel_Injected(IntPtr _unity_self, int value);

		// Token: 0x060018EF RID: 6383
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_loadAllMips_Injected(IntPtr _unity_self);

		// Token: 0x060018F0 RID: 6384
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_loadAllMips_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060018F1 RID: 6385
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_desiredMipmapLevel_Injected(IntPtr _unity_self);

		// Token: 0x060018F2 RID: 6386
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_loadingMipmapLevel_Injected(IntPtr _unity_self);

		// Token: 0x060018F3 RID: 6387
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_loadedMipmapLevel_Injected(IntPtr _unity_self);

		// Token: 0x060018F4 RID: 6388
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClearRequestedMipmapLevel_Injected(IntPtr _unity_self);

		// Token: 0x060018F5 RID: 6389
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsRequestedMipmapLevelLoaded_Injected(IntPtr _unity_self);
	}
}
