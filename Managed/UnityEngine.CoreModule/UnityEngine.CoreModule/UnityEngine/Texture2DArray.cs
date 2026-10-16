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
	// Token: 0x020001F6 RID: 502
	[ExcludeFromPreset]
	[NativeHeader("Runtime/Graphics/Texture2DArray.h")]
	public sealed class Texture2DArray : Texture
	{
		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x0600193B RID: 6459
		public static extern int allSlices
		{
			[NativeName("GetAllTextureLayersIdentifier")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x0600193C RID: 6460 RVA: 0x0002BE08 File Offset: 0x0002A008
		public int depth
		{
			[NativeName("GetTextureLayerCount")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2DArray>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Texture2DArray.get_depth_Injected(intPtr);
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x0600193D RID: 6461 RVA: 0x0002BE2C File Offset: 0x0002A02C
		public TextureFormat format
		{
			[NativeName("GetTextureFormat")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2DArray>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Texture2DArray.get_format_Injected(intPtr);
			}
		}

		// Token: 0x0600193E RID: 6462 RVA: 0x0002BE50 File Offset: 0x0002A050
		private bool IgnoreMipmapLimit()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2DArray>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Texture2DArray.IgnoreMipmapLimit_Injected(intPtr);
		}

		// Token: 0x0600193F RID: 6463 RVA: 0x0002BE74 File Offset: 0x0002A074
		private void SetIgnoreMipmapLimitAndReload(bool value)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2DArray>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Texture2DArray.SetIgnoreMipmapLimitAndReload_Injected(intPtr, value);
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06001940 RID: 6464 RVA: 0x0002BE98 File Offset: 0x0002A098
		public string mipmapLimitGroup
		{
			[NativeName("GetMipmapLimitGroupName")]
			get
			{
				string stringAndDispose;
				try
				{
					IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2DArray>(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					ManagedSpanWrapper managedSpanWrapper;
					Texture2DArray.get_mipmapLimitGroup_Injected(intPtr, out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06001941 RID: 6465 RVA: 0x0002BED8 File Offset: 0x0002A0D8
		public int activeMipmapLimit
		{
			[NativeName("GetMipmapLimit")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2DArray>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Texture2DArray.get_activeMipmapLimit_Injected(intPtr);
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06001942 RID: 6466 RVA: 0x0002BEFC File Offset: 0x0002A0FC
		public override bool isReadable
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2DArray>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Texture2DArray.get_isReadable_Injected(intPtr);
			}
		}

		// Token: 0x06001943 RID: 6467 RVA: 0x0002BF20 File Offset: 0x0002A120
		[FreeFunction("Texture2DArrayScripting::Create")]
		private unsafe static bool Internal_CreateImpl([Writable] Texture2DArray mono, int w, int h, int d, int mipCount, GraphicsFormat format, TextureColorSpace colorSpace, TextureCreationFlags flags, bool ignoreMipmapLimit, string mipmapLimitGroupName)
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
				flag = Texture2DArray.Internal_CreateImpl_Injected(mono, w, h, d, mipCount, format, colorSpace, flags, ignoreMipmapLimit, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x06001944 RID: 6468 RVA: 0x0002BF88 File Offset: 0x0002A188
		private static void Internal_Create([Writable] Texture2DArray mono, int w, int h, int d, int mipCount, GraphicsFormat format, TextureColorSpace colorSpace, TextureCreationFlags flags, bool ignoreMipmapLimit, string mipmapLimitGroupName)
		{
			bool flag = !Texture2DArray.Internal_CreateImpl(mono, w, h, d, mipCount, format, colorSpace, flags, ignoreMipmapLimit, mipmapLimitGroupName);
			if (flag)
			{
				throw new UnityException("Failed to create 2D array texture because of invalid parameters.");
			}
		}

		// Token: 0x06001945 RID: 6469 RVA: 0x0002BFC0 File Offset: 0x0002A1C0
		[FreeFunction(Name = "Texture2DArrayScripting::Apply", HasExplicitThis = true)]
		private void ApplyImpl(bool updateMipmaps, bool makeNoLongerReadable)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2DArray>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Texture2DArray.ApplyImpl_Injected(intPtr, updateMipmaps, makeNoLongerReadable);
		}

		// Token: 0x06001946 RID: 6470 RVA: 0x0002BFE4 File Offset: 0x0002A1E4
		[FreeFunction(Name = "Texture2DArrayScripting::GetPixels", HasExplicitThis = true, ThrowsException = true)]
		[return: Unmarshalled]
		public Color[] GetPixels(int arrayElement, int miplevel)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2DArray>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Texture2DArray.GetPixels_Injected(intPtr, arrayElement, miplevel);
		}

		// Token: 0x06001947 RID: 6471 RVA: 0x0002C008 File Offset: 0x0002A208
		public Color[] GetPixels(int arrayElement)
		{
			return this.GetPixels(arrayElement, 0);
		}

		// Token: 0x06001948 RID: 6472 RVA: 0x0002C024 File Offset: 0x0002A224
		[FreeFunction(Name = "Texture2DArrayScripting::SetPixelDataArray", HasExplicitThis = true, ThrowsException = true)]
		private bool SetPixelDataImplArray(Array data, int mipLevel, int element, int elementSize, int dataArraySize, int sourceDataStartIndex = 0)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2DArray>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Texture2DArray.SetPixelDataImplArray_Injected(intPtr, data, mipLevel, element, elementSize, dataArraySize, sourceDataStartIndex);
		}

		// Token: 0x06001949 RID: 6473 RVA: 0x0002C050 File Offset: 0x0002A250
		[FreeFunction(Name = "Texture2DArrayScripting::SetPixelData", HasExplicitThis = true, ThrowsException = true)]
		private bool SetPixelDataImpl(IntPtr data, int mipLevel, int element, int elementSize, int dataArraySize, int sourceDataStartIndex = 0)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2DArray>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Texture2DArray.SetPixelDataImpl_Injected(intPtr, data, mipLevel, element, elementSize, dataArraySize, sourceDataStartIndex);
		}

		// Token: 0x0600194A RID: 6474 RVA: 0x0002C07C File Offset: 0x0002A27C
		[FreeFunction(Name = "Texture2DArrayScripting::GetPixels32", HasExplicitThis = true, ThrowsException = true)]
		[return: Unmarshalled]
		public Color32[] GetPixels32(int arrayElement, int miplevel)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2DArray>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Texture2DArray.GetPixels32_Injected(intPtr, arrayElement, miplevel);
		}

		// Token: 0x0600194B RID: 6475 RVA: 0x0002C0A0 File Offset: 0x0002A2A0
		public Color32[] GetPixels32(int arrayElement)
		{
			return this.GetPixels32(arrayElement, 0);
		}

		// Token: 0x0600194C RID: 6476 RVA: 0x0002C0BC File Offset: 0x0002A2BC
		[FreeFunction(Name = "Texture2DArrayScripting::SetPixels", HasExplicitThis = true, ThrowsException = true)]
		public unsafe void SetPixels(Color[] colors, int arrayElement, int miplevel)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2DArray>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<Color> span = new Span<Color>(colors);
			fixed (Color* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				Texture2DArray.SetPixels_Injected(intPtr, ref managedSpanWrapper, arrayElement, miplevel);
			}
		}

		// Token: 0x0600194D RID: 6477 RVA: 0x0002C106 File Offset: 0x0002A306
		public void SetPixels(Color[] colors, int arrayElement)
		{
			this.SetPixels(colors, arrayElement, 0);
		}

		// Token: 0x0600194E RID: 6478 RVA: 0x0002C114 File Offset: 0x0002A314
		[FreeFunction(Name = "Texture2DArrayScripting::SetPixels32", HasExplicitThis = true, ThrowsException = true)]
		public unsafe void SetPixels32(Color32[] colors, int arrayElement, int miplevel)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2DArray>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<Color32> span = new Span<Color32>(colors);
			fixed (Color32* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				Texture2DArray.SetPixels32_Injected(intPtr, ref managedSpanWrapper, arrayElement, miplevel);
			}
		}

		// Token: 0x0600194F RID: 6479 RVA: 0x0002C15E File Offset: 0x0002A35E
		public void SetPixels32(Color32[] colors, int arrayElement)
		{
			this.SetPixels32(colors, arrayElement, 0);
		}

		// Token: 0x06001950 RID: 6480 RVA: 0x0002C16C File Offset: 0x0002A36C
		[FreeFunction(Name = "Texture2DArrayScripting::CopyPixels", HasExplicitThis = true, ThrowsException = true)]
		private void CopyPixels_Full(Texture src)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2DArray>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Texture2DArray.CopyPixels_Full_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Texture>(src));
		}

		// Token: 0x06001951 RID: 6481 RVA: 0x0002C194 File Offset: 0x0002A394
		[FreeFunction(Name = "Texture2DArrayScripting::CopyPixels", HasExplicitThis = true, ThrowsException = true)]
		private void CopyPixels_Slice(Texture src, int srcElement, int srcMip, int dstElement, int dstMip)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2DArray>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Texture2DArray.CopyPixels_Slice_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Texture>(src), srcElement, srcMip, dstElement, dstMip);
		}

		// Token: 0x06001952 RID: 6482 RVA: 0x0002C1C4 File Offset: 0x0002A3C4
		[FreeFunction(Name = "Texture2DArrayScripting::CopyPixels", HasExplicitThis = true, ThrowsException = true)]
		private void CopyPixels_Region(Texture src, int srcElement, int srcMip, int srcX, int srcY, int srcWidth, int srcHeight, int dstElement, int dstMip, int dstX, int dstY)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2DArray>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Texture2DArray.CopyPixels_Region_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Texture>(src), srcElement, srcMip, srcX, srcY, srcWidth, srcHeight, dstElement, dstMip, dstX, dstY);
		}

		// Token: 0x06001953 RID: 6483 RVA: 0x0002C200 File Offset: 0x0002A400
		private IntPtr GetImageData()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2DArray>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Texture2DArray.GetImageData_Injected(intPtr);
		}

		// Token: 0x06001954 RID: 6484 RVA: 0x0002C224 File Offset: 0x0002A424
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

		// Token: 0x06001955 RID: 6485 RVA: 0x0002C290 File Offset: 0x0002A490
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

		// Token: 0x06001956 RID: 6486 RVA: 0x0002C2F3 File Offset: 0x0002A4F3
		[ExcludeFromDocs]
		public Texture2DArray(int width, int height, int depth, DefaultFormat format, TextureCreationFlags flags)
			: this(width, height, depth, SystemInfo.GetGraphicsFormat(format), flags)
		{
		}

		// Token: 0x06001957 RID: 6487 RVA: 0x0002C309 File Offset: 0x0002A509
		[ExcludeFromDocs]
		public Texture2DArray(int width, int height, int depth, DefaultFormat format, TextureCreationFlags flags, int mipCount)
			: this(width, height, depth, SystemInfo.GetGraphicsFormat(format), flags, mipCount)
		{
		}

		// Token: 0x06001958 RID: 6488 RVA: 0x0002C321 File Offset: 0x0002A521
		[ExcludeFromDocs]
		public Texture2DArray(int width, int height, int depth, DefaultFormat format, TextureCreationFlags flags, int mipCount, MipmapLimitDescriptor mipmapLimitDescriptor)
			: this(width, height, depth, SystemInfo.GetGraphicsFormat(format), flags, mipCount, mipmapLimitDescriptor)
		{
		}

		// Token: 0x06001959 RID: 6489 RVA: 0x0002C33C File Offset: 0x0002A53C
		[RequiredByNativeCode]
		public Texture2DArray(int width, int height, int depth, GraphicsFormat format, TextureCreationFlags flags)
			: this(width, height, depth, format, flags, Texture.GenerateAllMips, default(MipmapLimitDescriptor))
		{
		}

		// Token: 0x0600195A RID: 6490 RVA: 0x0002C368 File Offset: 0x0002A568
		[ExcludeFromDocs]
		public Texture2DArray(int width, int height, int depth, GraphicsFormat format, TextureCreationFlags flags, int mipCount)
			: this(width, height, depth, format, flags, mipCount, default(MipmapLimitDescriptor))
		{
		}

		// Token: 0x0600195B RID: 6491 RVA: 0x0002C390 File Offset: 0x0002A590
		[ExcludeFromDocs]
		public Texture2DArray(int width, int height, int depth, GraphicsFormat format, TextureCreationFlags flags, int mipCount, MipmapLimitDescriptor mipmapLimitDescriptor)
		{
			bool flag = !this.ValidateFormat(format, width, height);
			if (!flag)
			{
				Texture2DArray.ValidateIsNotCrunched(flags);
				Texture2DArray.Internal_Create(this, width, height, depth, mipCount, format, base.GetTextureColorSpace(format), flags, !mipmapLimitDescriptor.useMipmapLimit, mipmapLimitDescriptor.groupName);
			}
		}

		// Token: 0x0600195C RID: 6492 RVA: 0x0002C3EC File Offset: 0x0002A5EC
		public Texture2DArray(int width, int height, int depth, TextureFormat textureFormat, int mipCount, bool linear, bool createUninitialized, MipmapLimitDescriptor mipmapLimitDescriptor)
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
				Texture2DArray.ValidateIsNotCrunched(textureCreationFlags);
				Texture2DArray.Internal_Create(this, width, height, depth, mipCount, graphicsFormat, base.GetTextureColorSpace(linear), textureCreationFlags, !mipmapLimitDescriptor.useMipmapLimit, mipmapLimitDescriptor.groupName);
			}
		}

		// Token: 0x0600195D RID: 6493 RVA: 0x0002C47C File Offset: 0x0002A67C
		public Texture2DArray(int width, int height, int depth, TextureFormat textureFormat, int mipCount, bool linear, bool createUninitialized)
			: this(width, height, depth, textureFormat, mipCount, linear, createUninitialized, default(MipmapLimitDescriptor))
		{
		}

		// Token: 0x0600195E RID: 6494 RVA: 0x0002C4A8 File Offset: 0x0002A6A8
		public Texture2DArray(int width, int height, int depth, TextureFormat textureFormat, int mipCount, bool linear)
			: this(width, height, depth, textureFormat, mipCount, linear, false, default(MipmapLimitDescriptor))
		{
		}

		// Token: 0x0600195F RID: 6495 RVA: 0x0002C4D0 File Offset: 0x0002A6D0
		public Texture2DArray(int width, int height, int depth, TextureFormat textureFormat, bool mipChain, [DefaultValue("false")] bool linear, [DefaultValue("false")] bool createUninitialized)
			: this(width, height, depth, textureFormat, mipChain ? Texture.GenerateAllMips : 1, linear, createUninitialized, default(MipmapLimitDescriptor))
		{
		}

		// Token: 0x06001960 RID: 6496 RVA: 0x0002C503 File Offset: 0x0002A703
		public Texture2DArray(int width, int height, int depth, TextureFormat textureFormat, bool mipChain, [DefaultValue("false")] bool linear)
			: this(width, height, depth, textureFormat, mipChain ? Texture.GenerateAllMips : 1, linear)
		{
		}

		// Token: 0x06001961 RID: 6497 RVA: 0x0002C520 File Offset: 0x0002A720
		[ExcludeFromDocs]
		public Texture2DArray(int width, int height, int depth, TextureFormat textureFormat, bool mipChain)
			: this(width, height, depth, textureFormat, mipChain ? Texture.GenerateAllMips : 1, false)
		{
		}

		// Token: 0x06001962 RID: 6498 RVA: 0x0002C53C File Offset: 0x0002A73C
		public void Apply([DefaultValue("true")] bool updateMipmaps, [DefaultValue("false")] bool makeNoLongerReadable)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			this.ApplyImpl(updateMipmaps, makeNoLongerReadable);
		}

		// Token: 0x06001963 RID: 6499 RVA: 0x0002C568 File Offset: 0x0002A768
		[ExcludeFromDocs]
		public void Apply(bool updateMipmaps)
		{
			this.Apply(updateMipmaps, false);
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x0002C574 File Offset: 0x0002A774
		[ExcludeFromDocs]
		public void Apply()
		{
			this.Apply(true, false);
		}

		// Token: 0x06001965 RID: 6501 RVA: 0x0002C580 File Offset: 0x0002A780
		public void SetPixelData<T>(T[] data, int mipLevel, int element, [DefaultValue("0")] int sourceDataStartIndex = 0)
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
			this.SetPixelDataImplArray(data, mipLevel, element, Marshal.SizeOf<T>(data[0]), data.Length, sourceDataStartIndex);
		}

		// Token: 0x06001966 RID: 6502 RVA: 0x0002C5EC File Offset: 0x0002A7EC
		public void SetPixelData<T>(NativeArray<T> data, int mipLevel, int element, [DefaultValue("0")] int sourceDataStartIndex = 0) where T : struct
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
			this.SetPixelDataImpl((IntPtr)data.GetUnsafeReadOnlyPtr<T>(), mipLevel, element, UnsafeUtility.SizeOf<T>(), data.Length, sourceDataStartIndex);
		}

		// Token: 0x06001967 RID: 6503 RVA: 0x0002C66C File Offset: 0x0002A86C
		public unsafe NativeArray<T> GetPixelData<T>(int mipLevel, int element) where T : struct
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
			bool flag3 = element < 0 || element >= this.depth;
			if (flag3)
			{
				throw new ArgumentException("The passed in element " + element.ToString() + " is invalid. The valid range is 0 through " + (this.depth - 1).ToString());
			}
			ulong pixelDataOffset = base.GetPixelDataOffset(base.mipmapCount, element);
			ulong pixelDataOffset2 = base.GetPixelDataOffset(mipLevel, element);
			ulong pixelDataSize = base.GetPixelDataSize(mipLevel, element);
			int num = UnsafeUtility.SizeOf<T>();
			ulong num2 = pixelDataSize / (ulong)((long)num);
			bool flag4 = num2 > 2147483647UL;
			if (flag4)
			{
				throw base.CreateNativeArrayLengthOverflowException();
			}
			IntPtr intPtr = new IntPtr((long)this.GetImageData() + (long)(pixelDataOffset * (ulong)((long)element) + pixelDataOffset2));
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>((void*)intPtr, (int)num2, Allocator.None);
		}

		// Token: 0x06001968 RID: 6504 RVA: 0x0002C794 File Offset: 0x0002A994
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

		// Token: 0x06001969 RID: 6505 RVA: 0x0002C7D4 File Offset: 0x0002A9D4
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

		// Token: 0x0600196A RID: 6506 RVA: 0x0002C81C File Offset: 0x0002AA1C
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

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x0600196B RID: 6507 RVA: 0x0002C870 File Offset: 0x0002AA70
		// (set) Token: 0x0600196C RID: 6508 RVA: 0x0002C888 File Offset: 0x0002AA88
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

		// Token: 0x0600196D RID: 6509 RVA: 0x0002C8B4 File Offset: 0x0002AAB4
		private static void ValidateIsNotCrunched(TextureCreationFlags flags)
		{
			bool flag = (flags &= TextureCreationFlags.Crunch) > TextureCreationFlags.None;
			if (flag)
			{
				throw new ArgumentException("Crunched Texture2DArray is not supported.");
			}
		}

		// Token: 0x0600196E RID: 6510
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_depth_Injected(IntPtr _unity_self);

		// Token: 0x0600196F RID: 6511
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern TextureFormat get_format_Injected(IntPtr _unity_self);

		// Token: 0x06001970 RID: 6512
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IgnoreMipmapLimit_Injected(IntPtr _unity_self);

		// Token: 0x06001971 RID: 6513
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetIgnoreMipmapLimitAndReload_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06001972 RID: 6514
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_mipmapLimitGroup_Injected(IntPtr _unity_self, out ManagedSpanWrapper ret);

		// Token: 0x06001973 RID: 6515
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_activeMipmapLimit_Injected(IntPtr _unity_self);

		// Token: 0x06001974 RID: 6516
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isReadable_Injected(IntPtr _unity_self);

		// Token: 0x06001975 RID: 6517
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Internal_CreateImpl_Injected([Writable] Texture2DArray mono, int w, int h, int d, int mipCount, GraphicsFormat format, TextureColorSpace colorSpace, TextureCreationFlags flags, bool ignoreMipmapLimit, ref ManagedSpanWrapper mipmapLimitGroupName);

		// Token: 0x06001976 RID: 6518
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ApplyImpl_Injected(IntPtr _unity_self, bool updateMipmaps, bool makeNoLongerReadable);

		// Token: 0x06001977 RID: 6519
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Color[] GetPixels_Injected(IntPtr _unity_self, int arrayElement, int miplevel);

		// Token: 0x06001978 RID: 6520
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetPixelDataImplArray_Injected(IntPtr _unity_self, Array data, int mipLevel, int element, int elementSize, int dataArraySize, int sourceDataStartIndex);

		// Token: 0x06001979 RID: 6521
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetPixelDataImpl_Injected(IntPtr _unity_self, IntPtr data, int mipLevel, int element, int elementSize, int dataArraySize, int sourceDataStartIndex);

		// Token: 0x0600197A RID: 6522
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Color32[] GetPixels32_Injected(IntPtr _unity_self, int arrayElement, int miplevel);

		// Token: 0x0600197B RID: 6523
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPixels_Injected(IntPtr _unity_self, ref ManagedSpanWrapper colors, int arrayElement, int miplevel);

		// Token: 0x0600197C RID: 6524
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPixels32_Injected(IntPtr _unity_self, ref ManagedSpanWrapper colors, int arrayElement, int miplevel);

		// Token: 0x0600197D RID: 6525
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyPixels_Full_Injected(IntPtr _unity_self, IntPtr src);

		// Token: 0x0600197E RID: 6526
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyPixels_Slice_Injected(IntPtr _unity_self, IntPtr src, int srcElement, int srcMip, int dstElement, int dstMip);

		// Token: 0x0600197F RID: 6527
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyPixels_Region_Injected(IntPtr _unity_self, IntPtr src, int srcElement, int srcMip, int srcX, int srcY, int srcWidth, int srcHeight, int dstElement, int dstMip, int dstX, int dstY);

		// Token: 0x06001980 RID: 6528
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetImageData_Injected(IntPtr _unity_self);
	}
}
