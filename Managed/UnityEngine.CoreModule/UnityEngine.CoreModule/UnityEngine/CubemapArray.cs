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
	// Token: 0x020001F7 RID: 503
	[ExcludeFromPreset]
	[NativeHeader("Runtime/Graphics/CubemapArrayTexture.h")]
	public sealed class CubemapArray : Texture
	{
		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06001981 RID: 6529 RVA: 0x0002C8DC File Offset: 0x0002AADC
		public int cubemapCount
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CubemapArray>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return CubemapArray.get_cubemapCount_Injected(intPtr);
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06001982 RID: 6530 RVA: 0x0002C900 File Offset: 0x0002AB00
		public TextureFormat format
		{
			[NativeName("GetTextureFormat")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CubemapArray>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return CubemapArray.get_format_Injected(intPtr);
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06001983 RID: 6531 RVA: 0x0002C924 File Offset: 0x0002AB24
		public override bool isReadable
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CubemapArray>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return CubemapArray.get_isReadable_Injected(intPtr);
			}
		}

		// Token: 0x06001984 RID: 6532
		[FreeFunction("CubemapArrayScripting::Create")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Internal_CreateImpl([Writable] CubemapArray mono, int ext, int count, int mipCount, GraphicsFormat format, TextureColorSpace colorSpace, TextureCreationFlags flags);

		// Token: 0x06001985 RID: 6533 RVA: 0x0002C948 File Offset: 0x0002AB48
		private static void Internal_Create([Writable] CubemapArray mono, int ext, int count, int mipCount, GraphicsFormat format, TextureColorSpace colorSpace, TextureCreationFlags flags)
		{
			bool flag = !CubemapArray.Internal_CreateImpl(mono, ext, count, mipCount, format, colorSpace, flags);
			if (flag)
			{
				throw new UnityException("Failed to create cubemap array texture because of invalid parameters.");
			}
		}

		// Token: 0x06001986 RID: 6534 RVA: 0x0002C978 File Offset: 0x0002AB78
		[FreeFunction(Name = "CubemapArrayScripting::Apply", HasExplicitThis = true)]
		private void ApplyImpl(bool updateMipmaps, bool makeNoLongerReadable)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CubemapArray>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CubemapArray.ApplyImpl_Injected(intPtr, updateMipmaps, makeNoLongerReadable);
		}

		// Token: 0x06001987 RID: 6535 RVA: 0x0002C99C File Offset: 0x0002AB9C
		[FreeFunction(Name = "CubemapArrayScripting::GetPixels", HasExplicitThis = true, ThrowsException = true)]
		[return: Unmarshalled]
		public Color[] GetPixels(CubemapFace face, int arrayElement, int miplevel)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CubemapArray>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return CubemapArray.GetPixels_Injected(intPtr, face, arrayElement, miplevel);
		}

		// Token: 0x06001988 RID: 6536 RVA: 0x0002C9C4 File Offset: 0x0002ABC4
		public Color[] GetPixels(CubemapFace face, int arrayElement)
		{
			return this.GetPixels(face, arrayElement, 0);
		}

		// Token: 0x06001989 RID: 6537 RVA: 0x0002C9E0 File Offset: 0x0002ABE0
		[FreeFunction(Name = "CubemapArrayScripting::GetPixels32", HasExplicitThis = true, ThrowsException = true)]
		[return: Unmarshalled]
		public Color32[] GetPixels32(CubemapFace face, int arrayElement, int miplevel)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CubemapArray>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return CubemapArray.GetPixels32_Injected(intPtr, face, arrayElement, miplevel);
		}

		// Token: 0x0600198A RID: 6538 RVA: 0x0002CA08 File Offset: 0x0002AC08
		public Color32[] GetPixels32(CubemapFace face, int arrayElement)
		{
			return this.GetPixels32(face, arrayElement, 0);
		}

		// Token: 0x0600198B RID: 6539 RVA: 0x0002CA24 File Offset: 0x0002AC24
		[FreeFunction(Name = "CubemapArrayScripting::SetPixels", HasExplicitThis = true, ThrowsException = true)]
		public unsafe void SetPixels(Color[] colors, CubemapFace face, int arrayElement, int miplevel)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CubemapArray>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<Color> span = new Span<Color>(colors);
			fixed (Color* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				CubemapArray.SetPixels_Injected(intPtr, ref managedSpanWrapper, face, arrayElement, miplevel);
			}
		}

		// Token: 0x0600198C RID: 6540 RVA: 0x0002CA70 File Offset: 0x0002AC70
		public void SetPixels(Color[] colors, CubemapFace face, int arrayElement)
		{
			this.SetPixels(colors, face, arrayElement, 0);
		}

		// Token: 0x0600198D RID: 6541 RVA: 0x0002CA80 File Offset: 0x0002AC80
		[FreeFunction(Name = "CubemapArrayScripting::SetPixels32", HasExplicitThis = true, ThrowsException = true)]
		public unsafe void SetPixels32(Color32[] colors, CubemapFace face, int arrayElement, int miplevel)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CubemapArray>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<Color32> span = new Span<Color32>(colors);
			fixed (Color32* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				CubemapArray.SetPixels32_Injected(intPtr, ref managedSpanWrapper, face, arrayElement, miplevel);
			}
		}

		// Token: 0x0600198E RID: 6542 RVA: 0x0002CACC File Offset: 0x0002ACCC
		public void SetPixels32(Color32[] colors, CubemapFace face, int arrayElement)
		{
			this.SetPixels32(colors, face, arrayElement, 0);
		}

		// Token: 0x0600198F RID: 6543 RVA: 0x0002CADC File Offset: 0x0002ACDC
		[FreeFunction(Name = "CubemapArrayScripting::SetPixelDataArray", HasExplicitThis = true, ThrowsException = true)]
		private bool SetPixelDataImplArray(Array data, int mipLevel, int face, int element, int elementSize, int dataArraySize, int sourceDataStartIndex = 0)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CubemapArray>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return CubemapArray.SetPixelDataImplArray_Injected(intPtr, data, mipLevel, face, element, elementSize, dataArraySize, sourceDataStartIndex);
		}

		// Token: 0x06001990 RID: 6544 RVA: 0x0002CB0C File Offset: 0x0002AD0C
		[FreeFunction(Name = "CubemapArrayScripting::SetPixelData", HasExplicitThis = true, ThrowsException = true)]
		private bool SetPixelDataImpl(IntPtr data, int mipLevel, int face, int element, int elementSize, int dataArraySize, int sourceDataStartIndex = 0)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CubemapArray>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return CubemapArray.SetPixelDataImpl_Injected(intPtr, data, mipLevel, face, element, elementSize, dataArraySize, sourceDataStartIndex);
		}

		// Token: 0x06001991 RID: 6545 RVA: 0x0002CB3C File Offset: 0x0002AD3C
		[FreeFunction(Name = "CubemapArrayScripting::CopyPixels", HasExplicitThis = true, ThrowsException = true)]
		private void CopyPixels_Full(Texture src)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CubemapArray>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CubemapArray.CopyPixels_Full_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Texture>(src));
		}

		// Token: 0x06001992 RID: 6546 RVA: 0x0002CB64 File Offset: 0x0002AD64
		[FreeFunction(Name = "CubemapArrayScripting::CopyPixels", HasExplicitThis = true, ThrowsException = true)]
		private void CopyPixels_Slice(Texture src, int srcElement, int srcMip, int dstElement, int dstMip)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CubemapArray>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CubemapArray.CopyPixels_Slice_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Texture>(src), srcElement, srcMip, dstElement, dstMip);
		}

		// Token: 0x06001993 RID: 6547 RVA: 0x0002CB94 File Offset: 0x0002AD94
		[FreeFunction(Name = "CubemapArrayScripting::CopyPixels", HasExplicitThis = true, ThrowsException = true)]
		private void CopyPixels_Region(Texture src, int srcElement, int srcMip, int srcX, int srcY, int srcWidth, int srcHeight, int dstElement, int dstMip, int dstX, int dstY)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CubemapArray>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CubemapArray.CopyPixels_Region_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Texture>(src), srcElement, srcMip, srcX, srcY, srcWidth, srcHeight, dstElement, dstMip, dstX, dstY);
		}

		// Token: 0x06001994 RID: 6548 RVA: 0x0002CBD0 File Offset: 0x0002ADD0
		private IntPtr GetImageData()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CubemapArray>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return CubemapArray.GetImageData_Injected(intPtr);
		}

		// Token: 0x06001995 RID: 6549 RVA: 0x0002CBF2 File Offset: 0x0002ADF2
		[ExcludeFromDocs]
		public CubemapArray(int width, int cubemapCount, DefaultFormat format, TextureCreationFlags flags)
			: this(width, cubemapCount, SystemInfo.GetGraphicsFormat(format), flags)
		{
		}

		// Token: 0x06001996 RID: 6550 RVA: 0x0002CC06 File Offset: 0x0002AE06
		[ExcludeFromDocs]
		public CubemapArray(int width, int cubemapCount, DefaultFormat format, TextureCreationFlags flags, [DefaultValue("Texture.GenerateAllMips")] int mipCount)
			: this(width, cubemapCount, SystemInfo.GetGraphicsFormat(format), flags, mipCount)
		{
		}

		// Token: 0x06001997 RID: 6551 RVA: 0x0002CC1C File Offset: 0x0002AE1C
		[RequiredByNativeCode]
		public CubemapArray(int width, int cubemapCount, GraphicsFormat format, TextureCreationFlags flags)
			: this(width, cubemapCount, format, flags, Texture.GenerateAllMips)
		{
		}

		// Token: 0x06001998 RID: 6552 RVA: 0x0002CC30 File Offset: 0x0002AE30
		[ExcludeFromDocs]
		public CubemapArray(int width, int cubemapCount, GraphicsFormat format, TextureCreationFlags flags, [DefaultValue("Texture.GenerateAllMips")] int mipCount)
		{
			bool flag = !base.ValidateFormat(format, GraphicsFormatUsage.Sample);
			if (!flag)
			{
				CubemapArray.ValidateIsNotCrunched(flags);
				CubemapArray.Internal_Create(this, width, cubemapCount, mipCount, format, base.GetTextureColorSpace(format), flags);
			}
		}

		// Token: 0x06001999 RID: 6553 RVA: 0x0002CC74 File Offset: 0x0002AE74
		public CubemapArray(int width, int cubemapCount, TextureFormat textureFormat, int mipCount, bool linear, [DefaultValue("false")] bool createUninitialized)
		{
			bool flag = !base.ValidateFormat(textureFormat);
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
				CubemapArray.ValidateIsNotCrunched(textureCreationFlags);
				CubemapArray.Internal_Create(this, width, cubemapCount, mipCount, graphicsFormat, base.GetTextureColorSpace(linear), textureCreationFlags);
			}
		}

		// Token: 0x0600199A RID: 6554 RVA: 0x0002CCEA File Offset: 0x0002AEEA
		public CubemapArray(int width, int cubemapCount, TextureFormat textureFormat, int mipCount, bool linear)
			: this(width, cubemapCount, textureFormat, mipCount, linear, false)
		{
		}

		// Token: 0x0600199B RID: 6555 RVA: 0x0002CCFC File Offset: 0x0002AEFC
		public CubemapArray(int width, int cubemapCount, TextureFormat textureFormat, bool mipChain, [DefaultValue("false")] bool linear, [DefaultValue("false")] bool createUninitialized)
			: this(width, cubemapCount, textureFormat, mipChain ? Texture.GenerateAllMips : 1, linear, createUninitialized)
		{
		}

		// Token: 0x0600199C RID: 6556 RVA: 0x0002CD19 File Offset: 0x0002AF19
		[ExcludeFromDocs]
		public CubemapArray(int width, int cubemapCount, TextureFormat textureFormat, bool mipChain, [DefaultValue("false")] bool linear)
			: this(width, cubemapCount, textureFormat, mipChain ? Texture.GenerateAllMips : 1, linear)
		{
		}

		// Token: 0x0600199D RID: 6557 RVA: 0x0002CD34 File Offset: 0x0002AF34
		public CubemapArray(int width, int cubemapCount, TextureFormat textureFormat, bool mipChain)
			: this(width, cubemapCount, textureFormat, mipChain ? Texture.GenerateAllMips : 1, false)
		{
		}

		// Token: 0x0600199E RID: 6558 RVA: 0x0002CD50 File Offset: 0x0002AF50
		public void Apply([DefaultValue("true")] bool updateMipmaps, [DefaultValue("false")] bool makeNoLongerReadable)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			this.ApplyImpl(updateMipmaps, makeNoLongerReadable);
		}

		// Token: 0x0600199F RID: 6559 RVA: 0x0002CD7C File Offset: 0x0002AF7C
		[ExcludeFromDocs]
		public void Apply(bool updateMipmaps)
		{
			this.Apply(updateMipmaps, false);
		}

		// Token: 0x060019A0 RID: 6560 RVA: 0x0002CD88 File Offset: 0x0002AF88
		[ExcludeFromDocs]
		public void Apply()
		{
			this.Apply(true, false);
		}

		// Token: 0x060019A1 RID: 6561 RVA: 0x0002CD94 File Offset: 0x0002AF94
		public void SetPixelData<T>(T[] data, int mipLevel, CubemapFace face, int element, [DefaultValue("0")] int sourceDataStartIndex = 0)
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
			this.SetPixelDataImplArray(data, mipLevel, (int)face, element, Marshal.SizeOf<T>(data[0]), data.Length, sourceDataStartIndex);
		}

		// Token: 0x060019A2 RID: 6562 RVA: 0x0002CE04 File Offset: 0x0002B004
		public void SetPixelData<T>(NativeArray<T> data, int mipLevel, CubemapFace face, int element, [DefaultValue("0")] int sourceDataStartIndex = 0) where T : struct
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
			this.SetPixelDataImpl((IntPtr)data.GetUnsafeReadOnlyPtr<T>(), mipLevel, (int)face, element, UnsafeUtility.SizeOf<T>(), data.Length, sourceDataStartIndex);
		}

		// Token: 0x060019A3 RID: 6563 RVA: 0x0002CE84 File Offset: 0x0002B084
		public unsafe NativeArray<T> GetPixelData<T>(int mipLevel, CubemapFace face, int element) where T : struct
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
				throw new ArgumentException("The passed in face " + face.ToString() + " is invalid.  The valid range is 0 through 5");
			}
			bool flag4 = element < 0 || element >= this.cubemapCount;
			if (flag4)
			{
				throw new ArgumentException("The passed in element " + element.ToString() + " is invalid. The valid range is 0 through " + (this.cubemapCount - 1).ToString());
			}
			int num = (int)(element * 6 + face);
			ulong pixelDataOffset = base.GetPixelDataOffset(base.mipmapCount, num);
			ulong pixelDataOffset2 = base.GetPixelDataOffset(mipLevel, num);
			ulong pixelDataSize = base.GetPixelDataSize(mipLevel, num);
			int num2 = UnsafeUtility.SizeOf<T>();
			ulong num3 = pixelDataSize / (ulong)((long)num2);
			bool flag5 = num3 > 2147483647UL;
			if (flag5)
			{
				throw base.CreateNativeArrayLengthOverflowException();
			}
			IntPtr intPtr = new IntPtr((long)this.GetImageData() + (long)(pixelDataOffset * (ulong)((long)num) + pixelDataOffset2));
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>((void*)intPtr, (int)num3, Allocator.None);
		}

		// Token: 0x060019A4 RID: 6564 RVA: 0x0002CFEC File Offset: 0x0002B1EC
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

		// Token: 0x060019A5 RID: 6565 RVA: 0x0002D02C File Offset: 0x0002B22C
		public void CopyPixels(Texture src, int srcElement, int srcMip, int dstElement, int dstMip)
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
			this.CopyPixels_Slice(src, srcElement, srcMip, dstElement, dstMip);
		}

		// Token: 0x060019A6 RID: 6566 RVA: 0x0002D074 File Offset: 0x0002B274
		public void CopyPixels(Texture src, int srcElement, int srcMip, int srcX, int srcY, int srcWidth, int srcHeight, int dstElement, int dstMip, int dstX, int dstY)
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
			this.CopyPixels_Region(src, srcElement, srcMip, srcX, srcY, srcWidth, srcHeight, dstElement, dstMip, dstX, dstY);
		}

		// Token: 0x060019A7 RID: 6567 RVA: 0x0002D0C8 File Offset: 0x0002B2C8
		private static void ValidateIsNotCrunched(TextureCreationFlags flags)
		{
			bool flag = (flags &= TextureCreationFlags.Crunch) > TextureCreationFlags.None;
			if (flag)
			{
				throw new ArgumentException("Crunched TextureCubeArray is not supported.");
			}
		}

		// Token: 0x060019A8 RID: 6568
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_cubemapCount_Injected(IntPtr _unity_self);

		// Token: 0x060019A9 RID: 6569
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern TextureFormat get_format_Injected(IntPtr _unity_self);

		// Token: 0x060019AA RID: 6570
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isReadable_Injected(IntPtr _unity_self);

		// Token: 0x060019AB RID: 6571
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ApplyImpl_Injected(IntPtr _unity_self, bool updateMipmaps, bool makeNoLongerReadable);

		// Token: 0x060019AC RID: 6572
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Color[] GetPixels_Injected(IntPtr _unity_self, CubemapFace face, int arrayElement, int miplevel);

		// Token: 0x060019AD RID: 6573
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Color32[] GetPixels32_Injected(IntPtr _unity_self, CubemapFace face, int arrayElement, int miplevel);

		// Token: 0x060019AE RID: 6574
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPixels_Injected(IntPtr _unity_self, ref ManagedSpanWrapper colors, CubemapFace face, int arrayElement, int miplevel);

		// Token: 0x060019AF RID: 6575
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPixels32_Injected(IntPtr _unity_self, ref ManagedSpanWrapper colors, CubemapFace face, int arrayElement, int miplevel);

		// Token: 0x060019B0 RID: 6576
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetPixelDataImplArray_Injected(IntPtr _unity_self, Array data, int mipLevel, int face, int element, int elementSize, int dataArraySize, int sourceDataStartIndex);

		// Token: 0x060019B1 RID: 6577
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetPixelDataImpl_Injected(IntPtr _unity_self, IntPtr data, int mipLevel, int face, int element, int elementSize, int dataArraySize, int sourceDataStartIndex);

		// Token: 0x060019B2 RID: 6578
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyPixels_Full_Injected(IntPtr _unity_self, IntPtr src);

		// Token: 0x060019B3 RID: 6579
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyPixels_Slice_Injected(IntPtr _unity_self, IntPtr src, int srcElement, int srcMip, int dstElement, int dstMip);

		// Token: 0x060019B4 RID: 6580
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyPixels_Region_Injected(IntPtr _unity_self, IntPtr src, int srcElement, int srcMip, int srcX, int srcY, int srcWidth, int srcHeight, int dstElement, int dstMip, int dstX, int dstY);

		// Token: 0x060019B5 RID: 6581
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetImageData_Injected(IntPtr _unity_self);
	}
}
