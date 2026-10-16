using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering.VirtualTexturing
{
	// Token: 0x0200000C RID: 12
	[Obsolete("Procedural Virtual Texturing is experimental, not ready for production use and Unity does not currently support it. The feature might be changed or removed in the future.", false)]
	[StaticAccessor("VirtualTexturing::Procedural", StaticAccessorType.DoubleColon)]
	[NativeHeader("Modules/VirtualTexturing/ScriptBindings/VirtualTexturing.bindings.h")]
	public static class Procedural
	{
		// Token: 0x06000039 RID: 57 RVA: 0x000024FC File Offset: 0x000006FC
		[NativeThrows]
		public static void SetDebugFlagInteger(Guid guid, long value)
		{
			UnityEngine.Rendering.VirtualTexturing.System.SetDebugFlagInteger(guid, value);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002507 File Offset: 0x00000707
		[NativeThrows]
		public static void SetDebugFlagDouble(Guid guid, double value)
		{
			UnityEngine.Rendering.VirtualTexturing.System.SetDebugFlagDouble(guid, value);
		}

		// Token: 0x0600003B RID: 59
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetCPUCacheSize(int sizeInMegabytes);

		// Token: 0x0600003C RID: 60
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetCPUCacheSize();

		// Token: 0x0600003D RID: 61 RVA: 0x00002514 File Offset: 0x00000714
		[NativeThrows]
		public unsafe static void SetGPUCacheSettings(GPUCacheSetting[] cacheSettings)
		{
			Span<GPUCacheSetting> span = new Span<GPUCacheSetting>(cacheSettings);
			fixed (GPUCacheSetting* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				Procedural.SetGPUCacheSettings_Injected(ref managedSpanWrapper);
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000254C File Offset: 0x0000074C
		[NativeThrows]
		public static GPUCacheSetting[] GetGPUCacheSettings()
		{
			GPUCacheSetting[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				Procedural.GetGPUCacheSettings_Injected(out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				GPUCacheSetting[] array;
				blittableArrayWrapper.Unmarshal<GPUCacheSetting>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x0600003F RID: 63
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetGPUCacheStagingAreaCapacity(uint tilesPerFrame);

		// Token: 0x06000040 RID: 64
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern uint GetGPUCacheStagingAreaCapacity();

		// Token: 0x06000041 RID: 65
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGPUCacheSettings_Injected(ref ManagedSpanWrapper cacheSettings);

		// Token: 0x06000042 RID: 66
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetGPUCacheSettings_Injected(out BlittableArrayWrapper ret);

		// Token: 0x0200000D RID: 13
		[NativeHeader("Modules/VirtualTexturing/ScriptBindings/VirtualTexturing.bindings.h")]
		[StaticAccessor("VirtualTexturing::Procedural", StaticAccessorType.DoubleColon)]
		internal static class Binding
		{
			// Token: 0x06000043 RID: 67 RVA: 0x00002580 File Offset: 0x00000780
			internal static ulong Create(Procedural.CreationParameters p)
			{
				return Procedural.Binding.Create_Injected(ref p);
			}

			// Token: 0x06000044 RID: 68
			[MethodImpl(MethodImplOptions.InternalCall)]
			internal static extern void Destroy(ulong handle);

			// Token: 0x06000045 RID: 69
			[NativeThrows]
			[MethodImpl(MethodImplOptions.InternalCall)]
			internal static extern int PopRequests(ulong handle, IntPtr requestHandles, int length);

			// Token: 0x06000046 RID: 70
			[ThreadSafe]
			[NativeThrows]
			[MethodImpl(MethodImplOptions.InternalCall)]
			internal static extern void GetRequestParameters(IntPtr requestHandles, IntPtr requestParameters, int length);

			// Token: 0x06000047 RID: 71
			[ThreadSafe]
			[NativeThrows]
			[MethodImpl(MethodImplOptions.InternalCall)]
			internal static extern void UpdateRequestState(IntPtr requestHandles, IntPtr requestUpdates, int length);

			// Token: 0x06000048 RID: 72 RVA: 0x00002594 File Offset: 0x00000794
			[NativeThrows]
			[ThreadSafe]
			internal static void UpdateRequestStateWithCommandBuffer(IntPtr requestHandles, IntPtr requestUpdates, int length, CommandBuffer fenceBuffer)
			{
				Procedural.Binding.UpdateRequestStateWithCommandBuffer_Injected(requestHandles, requestUpdates, length, (fenceBuffer == null) ? ((IntPtr)0) : CommandBuffer.BindingsMarshaller.ConvertToNative(fenceBuffer));
			}

			// Token: 0x06000049 RID: 73 RVA: 0x000025B8 File Offset: 0x000007B8
			internal unsafe static void BindToMaterialPropertyBlock(ulong handle, [NotNull] MaterialPropertyBlock material, string name)
			{
				if (material == null)
				{
					ThrowHelper.ThrowArgumentNullException(material, "material");
				}
				try
				{
					IntPtr intPtr = MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(material);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowArgumentNullException(material, "material");
					}
					ManagedSpanWrapper managedSpanWrapper;
					if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
					{
						ReadOnlySpan<char> readOnlySpan = name.AsSpan();
						fixed (char* ptr = readOnlySpan.GetPinnableReference())
						{
							managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
						}
					}
					Procedural.Binding.BindToMaterialPropertyBlock_Injected(handle, intPtr, ref managedSpanWrapper);
				}
				finally
				{
					char* ptr = null;
				}
			}

			// Token: 0x0600004A RID: 74 RVA: 0x00002634 File Offset: 0x00000834
			internal unsafe static void BindToMaterial(ulong handle, [NotNull] Material material, string name)
			{
				if (material == null)
				{
					ThrowHelper.ThrowArgumentNullException(material, "material");
				}
				try
				{
					IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(material);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowArgumentNullException(material, "material");
					}
					ManagedSpanWrapper managedSpanWrapper;
					if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
					{
						ReadOnlySpan<char> readOnlySpan = name.AsSpan();
						fixed (char* ptr = readOnlySpan.GetPinnableReference())
						{
							managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
						}
					}
					Procedural.Binding.BindToMaterial_Injected(handle, intPtr, ref managedSpanWrapper);
				}
				finally
				{
					char* ptr = null;
				}
			}

			// Token: 0x0600004B RID: 75 RVA: 0x000026B0 File Offset: 0x000008B0
			internal unsafe static void BindGlobally(ulong handle, string name)
			{
				try
				{
					ManagedSpanWrapper managedSpanWrapper;
					if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
					{
						ReadOnlySpan<char> readOnlySpan = name.AsSpan();
						fixed (char* ptr = readOnlySpan.GetPinnableReference())
						{
							managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
						}
					}
					Procedural.Binding.BindGlobally_Injected(handle, ref managedSpanWrapper);
				}
				finally
				{
					char* ptr = null;
				}
			}

			// Token: 0x0600004C RID: 76 RVA: 0x00002708 File Offset: 0x00000908
			[NativeThrows]
			internal static void RequestRegion(ulong handle, Rect r, int mipMap, int numMips)
			{
				Procedural.Binding.RequestRegion_Injected(handle, ref r, mipMap, numMips);
			}

			// Token: 0x0600004D RID: 77 RVA: 0x00002720 File Offset: 0x00000920
			[NativeThrows]
			internal static void InvalidateRegion(ulong handle, Rect r, int mipMap, int numMips)
			{
				Procedural.Binding.InvalidateRegion_Injected(handle, ref r, mipMap, numMips);
			}

			// Token: 0x0600004E RID: 78 RVA: 0x00002738 File Offset: 0x00000938
			[NativeThrows]
			public static void EvictRegion(ulong handle, Rect r, int mipMap, int numMips)
			{
				Procedural.Binding.EvictRegion_Injected(handle, ref r, mipMap, numMips);
			}

			// Token: 0x0600004F RID: 79
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ulong Create_Injected([In] ref Procedural.CreationParameters p);

			// Token: 0x06000050 RID: 80
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void UpdateRequestStateWithCommandBuffer_Injected(IntPtr requestHandles, IntPtr requestUpdates, int length, IntPtr fenceBuffer);

			// Token: 0x06000051 RID: 81
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void BindToMaterialPropertyBlock_Injected(ulong handle, IntPtr material, ref ManagedSpanWrapper name);

			// Token: 0x06000052 RID: 82
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void BindToMaterial_Injected(ulong handle, IntPtr material, ref ManagedSpanWrapper name);

			// Token: 0x06000053 RID: 83
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void BindGlobally_Injected(ulong handle, ref ManagedSpanWrapper name);

			// Token: 0x06000054 RID: 84
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void RequestRegion_Injected(ulong handle, [In] ref Rect r, int mipMap, int numMips);

			// Token: 0x06000055 RID: 85
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void InvalidateRegion_Injected(ulong handle, [In] ref Rect r, int mipMap, int numMips);

			// Token: 0x06000056 RID: 86
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void EvictRegion_Injected(ulong handle, [In] ref Rect r, int mipMap, int numMips);
		}

		// Token: 0x0200000E RID: 14
		[NativeHeader("Modules/VirtualTexturing/ScriptBindings/VirtualTexturing.bindings.h")]
		public struct CreationParameters
		{
			// Token: 0x06000057 RID: 87 RVA: 0x00002750 File Offset: 0x00000950
			internal void Validate()
			{
				bool flag = this.width <= 0 || this.height <= 0 || this.tilesize <= 0;
				if (flag)
				{
					throw new ArgumentException(string.Format("Zero sized dimensions are invalid (width: {0}, height: {1}, tilesize {2}", this.width, this.height, this.tilesize));
				}
				bool flag2 = this.layers == null || this.layers.Length > 4;
				if (flag2)
				{
					throw new ArgumentException(string.Format("layers is either invalid or has too many layers (maxNumLayers: {0})", 4));
				}
				bool flag3 = this.gpuGeneration == 1 && this.filterMode != FilterMode.Bilinear;
				if (flag3)
				{
					throw new ArgumentException("Filter mode invalid for GPU PVT; only FilterMode.Bilinear is currently supported");
				}
				bool flag4 = this.gpuGeneration == 0 && this.filterMode != FilterMode.Bilinear && this.filterMode != FilterMode.Trilinear;
				if (flag4)
				{
					throw new ArgumentException("Filter mode invalid for CPU PVT; only FilterMode.Bilinear and FilterMode.Trilinear are currently supported");
				}
				GraphicsFormat[] array = new GraphicsFormat[]
				{
					GraphicsFormat.R8G8B8A8_SRGB,
					GraphicsFormat.R8G8B8A8_UNorm,
					GraphicsFormat.R32G32B32A32_SFloat,
					GraphicsFormat.R8G8_SRGB,
					GraphicsFormat.R8G8_UNorm,
					GraphicsFormat.R32_SFloat,
					GraphicsFormat.RGB_DXT1_SRGB,
					GraphicsFormat.RGB_DXT1_UNorm,
					GraphicsFormat.RGBA_DXT5_SRGB,
					GraphicsFormat.RGBA_DXT5_UNorm,
					GraphicsFormat.RGBA_BC7_SRGB,
					GraphicsFormat.RGBA_BC7_UNorm,
					GraphicsFormat.RG_BC5_SNorm,
					GraphicsFormat.RG_BC5_UNorm,
					GraphicsFormat.RGB_BC6H_SFloat,
					GraphicsFormat.RGB_BC6H_UFloat,
					GraphicsFormat.R16_SFloat,
					GraphicsFormat.R16_UNorm,
					GraphicsFormat.R16G16_SFloat,
					GraphicsFormat.R16G16_UNorm,
					GraphicsFormat.R16G16B16A16_SFloat,
					GraphicsFormat.R16G16B16A16_UNorm
				};
				GraphicsFormat[] array2 = new GraphicsFormat[]
				{
					GraphicsFormat.R8G8B8A8_SRGB,
					GraphicsFormat.R8G8B8A8_UNorm,
					GraphicsFormat.R32G32B32A32_SFloat,
					GraphicsFormat.R8G8_SRGB,
					GraphicsFormat.R8G8_UNorm,
					GraphicsFormat.R32_SFloat,
					GraphicsFormat.A2B10G10R10_UNormPack32,
					GraphicsFormat.R16_UNorm
				};
				GraphicsFormatUsage graphicsFormatUsage = ((this.gpuGeneration == 1) ? GraphicsFormatUsage.Render : GraphicsFormatUsage.Sample);
				for (int i = 0; i < this.layers.Length; i++)
				{
					bool flag5 = SystemInfo.GetCompatibleFormat(this.layers[i], graphicsFormatUsage) != this.layers[i];
					if (flag5)
					{
						throw new ArgumentException(string.Format("Requested format {0} on layer {1} is not supported on this platform", this.layers[i], i));
					}
					bool flag6 = false;
					GraphicsFormat[] array3 = ((this.gpuGeneration == 1) ? array2 : array);
					for (int j = 0; j < array3.Length; j++)
					{
						bool flag7 = this.layers[i] == array3[j];
						if (flag7)
						{
							flag6 = true;
							break;
						}
					}
					bool flag8 = !flag6;
					if (flag8)
					{
						string text = ((this.gpuGeneration == 1) ? "GPU" : "CPU");
						throw new ArgumentException(string.Format("{0} Procedural Virtual Texturing doesn't support GraphicsFormat {1} for stack layer {2}", text, this.layers[i], i));
					}
				}
				bool flag9 = this.maxActiveRequests > 4095 || this.maxActiveRequests <= 0;
				if (flag9)
				{
					throw new ArgumentException(string.Format("Invalid requests per frame (maxActiveRequests: ]0, {0}])", this.maxActiveRequests));
				}
			}

			// Token: 0x04000011 RID: 17
			public const int MaxNumLayers = 4;

			// Token: 0x04000012 RID: 18
			public const int MaxRequestsPerFrameSupported = 4095;

			// Token: 0x04000013 RID: 19
			public int width;

			// Token: 0x04000014 RID: 20
			public int height;

			// Token: 0x04000015 RID: 21
			public int maxActiveRequests;

			// Token: 0x04000016 RID: 22
			public int tilesize;

			// Token: 0x04000017 RID: 23
			public GraphicsFormat[] layers;

			// Token: 0x04000018 RID: 24
			public FilterMode filterMode;

			// Token: 0x04000019 RID: 25
			internal int borderSize;

			// Token: 0x0400001A RID: 26
			internal int gpuGeneration;

			// Token: 0x0400001B RID: 27
			internal int flags;
		}

		// Token: 0x0200000F RID: 15
		[UsedByNativeCode]
		[NativeHeader("Modules/VirtualTexturing/ScriptBindings/VirtualTexturing.bindings.h")]
		internal struct RequestHandlePayload : IEquatable<Procedural.RequestHandlePayload>
		{
			// Token: 0x06000058 RID: 88 RVA: 0x000029B8 File Offset: 0x00000BB8
			public static bool operator !=(Procedural.RequestHandlePayload lhs, Procedural.RequestHandlePayload rhs)
			{
				return !(lhs == rhs);
			}

			// Token: 0x06000059 RID: 89 RVA: 0x000029D4 File Offset: 0x00000BD4
			public override bool Equals(object obj)
			{
				return obj is Procedural.RequestHandlePayload && this == (Procedural.RequestHandlePayload)obj;
			}

			// Token: 0x0600005A RID: 90 RVA: 0x00002A04 File Offset: 0x00000C04
			public bool Equals(Procedural.RequestHandlePayload other)
			{
				return this == other;
			}

			// Token: 0x0600005B RID: 91 RVA: 0x00002A24 File Offset: 0x00000C24
			public override int GetHashCode()
			{
				int num = -2128608763;
				num = num * -1521134295 + this.id.GetHashCode();
				num = num * -1521134295 + this.lifetime.GetHashCode();
				return num * -1521134295 + this.callback.GetHashCode();
			}

			// Token: 0x0600005C RID: 92 RVA: 0x00002A7C File Offset: 0x00000C7C
			public static bool operator ==(Procedural.RequestHandlePayload lhs, Procedural.RequestHandlePayload rhs)
			{
				return lhs.id == rhs.id && lhs.lifetime == rhs.lifetime && lhs.callback == rhs.callback;
			}

			// Token: 0x0400001C RID: 28
			internal int id;

			// Token: 0x0400001D RID: 29
			internal int lifetime;

			// Token: 0x0400001E RID: 30
			[NativeDisableUnsafePtrRestriction]
			internal IntPtr callback;
		}

		// Token: 0x02000010 RID: 16
		public struct TextureStackRequestHandle<T> : IEquatable<Procedural.TextureStackRequestHandle<T>> where T : struct
		{
			// Token: 0x0600005D RID: 93 RVA: 0x00002AC0 File Offset: 0x00000CC0
			public static bool operator !=(Procedural.TextureStackRequestHandle<T> h1, Procedural.TextureStackRequestHandle<T> h2)
			{
				return !(h1 == h2);
			}

			// Token: 0x0600005E RID: 94 RVA: 0x00002ADC File Offset: 0x00000CDC
			public override bool Equals(object obj)
			{
				return obj is Procedural.TextureStackRequestHandle<T> && this == (Procedural.TextureStackRequestHandle<T>)obj;
			}

			// Token: 0x0600005F RID: 95 RVA: 0x00002B0C File Offset: 0x00000D0C
			public bool Equals(Procedural.TextureStackRequestHandle<T> other)
			{
				return this == other;
			}

			// Token: 0x06000060 RID: 96 RVA: 0x00002B2C File Offset: 0x00000D2C
			public override int GetHashCode()
			{
				return this.payload.GetHashCode();
			}

			// Token: 0x06000061 RID: 97 RVA: 0x00002B50 File Offset: 0x00000D50
			public static bool operator ==(Procedural.TextureStackRequestHandle<T> h1, Procedural.TextureStackRequestHandle<T> h2)
			{
				return h1.payload == h2.payload;
			}

			// Token: 0x06000062 RID: 98 RVA: 0x00002B73 File Offset: 0x00000D73
			public void CompleteRequest(Procedural.RequestStatus status)
			{
				Procedural.Binding.UpdateRequestState((IntPtr)UnsafeUtility.AddressOf<Procedural.TextureStackRequestHandle<T>>(ref this), (IntPtr)UnsafeUtility.AddressOf<Procedural.RequestStatus>(ref status), 1);
			}

			// Token: 0x06000063 RID: 99 RVA: 0x00002B96 File Offset: 0x00000D96
			public void CompleteRequest(Procedural.RequestStatus status, CommandBuffer fenceBuffer)
			{
				Procedural.Binding.UpdateRequestStateWithCommandBuffer((IntPtr)UnsafeUtility.AddressOf<Procedural.TextureStackRequestHandle<T>>(ref this), (IntPtr)UnsafeUtility.AddressOf<Procedural.RequestStatus>(ref status), 1, fenceBuffer);
			}

			// Token: 0x06000064 RID: 100 RVA: 0x00002BBC File Offset: 0x00000DBC
			public static void CompleteRequests(NativeSlice<Procedural.TextureStackRequestHandle<T>> requestHandles, NativeSlice<Procedural.RequestStatus> status)
			{
				bool flag = !UnityEngine.Rendering.VirtualTexturing.System.enabled;
				if (flag)
				{
					throw new InvalidOperationException("Virtual texturing is not enabled in the player settings.");
				}
				bool flag2 = requestHandles.Length != status.Length;
				if (flag2)
				{
					throw new ArgumentException(string.Format("Array sizes do not match ({0} handles, {1} requests)", requestHandles.Length, status.Length));
				}
				Procedural.Binding.UpdateRequestState((IntPtr)requestHandles.GetUnsafePtr<Procedural.TextureStackRequestHandle<T>>(), (IntPtr)status.GetUnsafePtr<Procedural.RequestStatus>(), requestHandles.Length);
			}

			// Token: 0x06000065 RID: 101 RVA: 0x00002C4C File Offset: 0x00000E4C
			public static void CompleteRequests(NativeSlice<Procedural.TextureStackRequestHandle<T>> requestHandles, NativeSlice<Procedural.RequestStatus> status, CommandBuffer fenceBuffer)
			{
				bool flag = !UnityEngine.Rendering.VirtualTexturing.System.enabled;
				if (flag)
				{
					throw new InvalidOperationException("Virtual texturing is not enabled in the player settings.");
				}
				bool flag2 = requestHandles.Length != status.Length;
				if (flag2)
				{
					throw new ArgumentException(string.Format("Array sizes do not match ({0} handles, {1} requests)", requestHandles.Length, status.Length));
				}
				Procedural.Binding.UpdateRequestStateWithCommandBuffer((IntPtr)requestHandles.GetUnsafePtr<Procedural.TextureStackRequestHandle<T>>(), (IntPtr)status.GetUnsafePtr<Procedural.RequestStatus>(), requestHandles.Length, fenceBuffer);
			}

			// Token: 0x06000066 RID: 102 RVA: 0x00002CDC File Offset: 0x00000EDC
			public T GetRequestParameters()
			{
				T t = new T();
				Procedural.Binding.GetRequestParameters((IntPtr)UnsafeUtility.AddressOf<Procedural.TextureStackRequestHandle<T>>(ref this), (IntPtr)UnsafeUtility.AddressOf<T>(ref t), 1);
				return t;
			}

			// Token: 0x06000067 RID: 103 RVA: 0x00002D18 File Offset: 0x00000F18
			public static void GetRequestParameters(NativeSlice<Procedural.TextureStackRequestHandle<T>> handles, NativeSlice<T> requests)
			{
				bool flag = !UnityEngine.Rendering.VirtualTexturing.System.enabled;
				if (flag)
				{
					throw new InvalidOperationException("Virtual texturing is not enabled in the player settings.");
				}
				bool flag2 = handles.Length != requests.Length;
				if (flag2)
				{
					throw new ArgumentException(string.Format("Array sizes do not match ({0} handles, {1} requests)", handles.Length, requests.Length));
				}
				Procedural.Binding.GetRequestParameters((IntPtr)handles.GetUnsafePtr<Procedural.TextureStackRequestHandle<T>>(), (IntPtr)requests.GetUnsafePtr<T>(), handles.Length);
			}

			// Token: 0x0400001F RID: 31
			internal Procedural.RequestHandlePayload payload;
		}

		// Token: 0x02000011 RID: 17
		[NativeHeader("Modules/VirtualTexturing/ScriptBindings/VirtualTexturing.bindings.h")]
		[UsedByNativeCode]
		public struct GPUTextureStackRequestLayerParameters
		{
			// Token: 0x06000068 RID: 104
			[MethodImpl(MethodImplOptions.InternalCall)]
			public extern int GetWidth();

			// Token: 0x06000069 RID: 105
			[MethodImpl(MethodImplOptions.InternalCall)]
			public extern int GetHeight();

			// Token: 0x04000020 RID: 32
			public int destX;

			// Token: 0x04000021 RID: 33
			public int destY;

			// Token: 0x04000022 RID: 34
			public RenderTargetIdentifier dest;
		}

		// Token: 0x02000012 RID: 18
		[NativeHeader("Modules/VirtualTexturing/ScriptBindings/VirtualTexturing.bindings.h")]
		[UsedByNativeCode]
		public struct CPUTextureStackRequestLayerParameters
		{
			// Token: 0x0600006A RID: 106 RVA: 0x00002DA8 File Offset: 0x00000FA8
			public NativeArray<T> GetData<T>() where T : struct
			{
				return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>(this.data, this.dataSize, Allocator.None);
			}

			// Token: 0x0600006B RID: 107 RVA: 0x00002DD0 File Offset: 0x00000FD0
			public NativeArray<T> GetMipData<T>() where T : struct
			{
				return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>(this.mipData, this.mipDataSize, Allocator.None);
			}

			// Token: 0x17000009 RID: 9
			// (get) Token: 0x0600006C RID: 108 RVA: 0x00002DF5 File Offset: 0x00000FF5
			public int scanlineSize
			{
				get
				{
					return this._scanlineSize;
				}
			}

			// Token: 0x1700000A RID: 10
			// (get) Token: 0x0600006D RID: 109 RVA: 0x00002DFD File Offset: 0x00000FFD
			public int mipScanlineSize
			{
				get
				{
					return this._mipScanlineSize;
				}
			}

			// Token: 0x1700000B RID: 11
			// (get) Token: 0x0600006E RID: 110 RVA: 0x00002E05 File Offset: 0x00001005
			public bool requiresCachedMip
			{
				get
				{
					return this.mipDataSize != 0;
				}
			}

			// Token: 0x04000023 RID: 35
			internal int _scanlineSize;

			// Token: 0x04000024 RID: 36
			internal int dataSize;

			// Token: 0x04000025 RID: 37
			[NativeDisableUnsafePtrRestriction]
			internal unsafe void* data;

			// Token: 0x04000026 RID: 38
			internal int _mipScanlineSize;

			// Token: 0x04000027 RID: 39
			internal int mipDataSize;

			// Token: 0x04000028 RID: 40
			[NativeDisableUnsafePtrRestriction]
			internal unsafe void* mipData;
		}

		// Token: 0x02000013 RID: 19
		[NativeHeader("Modules/VirtualTexturing/ScriptBindings/VirtualTexturing.bindings.h")]
		[UsedByNativeCode]
		public struct GPUTextureStackRequestParameters
		{
			// Token: 0x0600006F RID: 111 RVA: 0x00002E10 File Offset: 0x00001010
			public Procedural.GPUTextureStackRequestLayerParameters GetLayer(int index)
			{
				Procedural.GPUTextureStackRequestLayerParameters gputextureStackRequestLayerParameters;
				switch (index)
				{
				case 0:
					gputextureStackRequestLayerParameters = this.layer0;
					break;
				case 1:
					gputextureStackRequestLayerParameters = this.layer1;
					break;
				case 2:
					gputextureStackRequestLayerParameters = this.layer2;
					break;
				case 3:
					gputextureStackRequestLayerParameters = this.layer3;
					break;
				default:
					throw new IndexOutOfRangeException();
				}
				return gputextureStackRequestLayerParameters;
			}

			// Token: 0x04000029 RID: 41
			public int level;

			// Token: 0x0400002A RID: 42
			public int x;

			// Token: 0x0400002B RID: 43
			public int y;

			// Token: 0x0400002C RID: 44
			public int width;

			// Token: 0x0400002D RID: 45
			public int height;

			// Token: 0x0400002E RID: 46
			public int numLayers;

			// Token: 0x0400002F RID: 47
			private Procedural.GPUTextureStackRequestLayerParameters layer0;

			// Token: 0x04000030 RID: 48
			private Procedural.GPUTextureStackRequestLayerParameters layer1;

			// Token: 0x04000031 RID: 49
			private Procedural.GPUTextureStackRequestLayerParameters layer2;

			// Token: 0x04000032 RID: 50
			private Procedural.GPUTextureStackRequestLayerParameters layer3;
		}

		// Token: 0x02000014 RID: 20
		[UsedByNativeCode]
		[NativeHeader("Modules/VirtualTexturing/ScriptBindings/VirtualTexturing.bindings.h")]
		public struct CPUTextureStackRequestParameters
		{
			// Token: 0x06000070 RID: 112 RVA: 0x00002E68 File Offset: 0x00001068
			public Procedural.CPUTextureStackRequestLayerParameters GetLayer(int index)
			{
				Procedural.CPUTextureStackRequestLayerParameters cputextureStackRequestLayerParameters;
				switch (index)
				{
				case 0:
					cputextureStackRequestLayerParameters = this.layer0;
					break;
				case 1:
					cputextureStackRequestLayerParameters = this.layer1;
					break;
				case 2:
					cputextureStackRequestLayerParameters = this.layer2;
					break;
				case 3:
					cputextureStackRequestLayerParameters = this.layer3;
					break;
				default:
					throw new IndexOutOfRangeException();
				}
				return cputextureStackRequestLayerParameters;
			}

			// Token: 0x04000033 RID: 51
			public int level;

			// Token: 0x04000034 RID: 52
			public int x;

			// Token: 0x04000035 RID: 53
			public int y;

			// Token: 0x04000036 RID: 54
			public int width;

			// Token: 0x04000037 RID: 55
			public int height;

			// Token: 0x04000038 RID: 56
			public int numLayers;

			// Token: 0x04000039 RID: 57
			private Procedural.CPUTextureStackRequestLayerParameters layer0;

			// Token: 0x0400003A RID: 58
			private Procedural.CPUTextureStackRequestLayerParameters layer1;

			// Token: 0x0400003B RID: 59
			private Procedural.CPUTextureStackRequestLayerParameters layer2;

			// Token: 0x0400003C RID: 60
			private Procedural.CPUTextureStackRequestLayerParameters layer3;
		}

		// Token: 0x02000015 RID: 21
		[UsedByNativeCode]
		internal enum ProceduralTextureStackRequestStatus
		{
			// Token: 0x0400003E RID: 62
			StatusFree = 65535,
			// Token: 0x0400003F RID: 63
			StatusRequested,
			// Token: 0x04000040 RID: 64
			StatusProcessing,
			// Token: 0x04000041 RID: 65
			StatusComplete,
			// Token: 0x04000042 RID: 66
			StatusDropped
		}

		// Token: 0x02000016 RID: 22
		public enum RequestStatus
		{
			// Token: 0x04000044 RID: 68
			Dropped = 65539,
			// Token: 0x04000045 RID: 69
			Generated = 65538
		}

		// Token: 0x02000017 RID: 23
		public class TextureStackBase<T> : IDisposable where T : struct
		{
			// Token: 0x06000071 RID: 113 RVA: 0x00002EC0 File Offset: 0x000010C0
			public int PopRequests(NativeSlice<Procedural.TextureStackRequestHandle<T>> requestHandles)
			{
				bool flag = !this.IsValid();
				if (flag)
				{
					throw new InvalidOperationException("Invalid ProceduralTextureStack " + this.name);
				}
				return Procedural.Binding.PopRequests(this.handle, (IntPtr)requestHandles.GetUnsafePtr<Procedural.TextureStackRequestHandle<T>>(), requestHandles.Length);
			}

			// Token: 0x06000072 RID: 114 RVA: 0x00002F18 File Offset: 0x00001118
			public bool IsValid()
			{
				return this.handle > 0UL;
			}

			// Token: 0x06000073 RID: 115 RVA: 0x00002F34 File Offset: 0x00001134
			public TextureStackBase(string _name, Procedural.CreationParameters _creationParams, bool gpuGeneration)
			{
				bool flag = !UnityEngine.Rendering.VirtualTexturing.System.enabled;
				if (flag)
				{
					throw new InvalidOperationException("Virtual texturing is not enabled in the player settings.");
				}
				this.name = _name;
				this.creationParams = _creationParams;
				this.creationParams.borderSize = Procedural.TextureStackBase<T>.borderSize;
				this.creationParams.gpuGeneration = (gpuGeneration ? 1 : 0);
				this.creationParams.flags = 0;
				this.creationParams.Validate();
				this.handle = Procedural.Binding.Create(this.creationParams);
			}

			// Token: 0x06000074 RID: 116 RVA: 0x00002FBC File Offset: 0x000011BC
			public void Dispose()
			{
				bool flag = this.IsValid();
				if (flag)
				{
					Procedural.Binding.Destroy(this.handle);
					this.handle = 0UL;
				}
			}

			// Token: 0x06000075 RID: 117 RVA: 0x00002FEC File Offset: 0x000011EC
			public void BindToMaterialPropertyBlock(MaterialPropertyBlock mpb)
			{
				bool flag = mpb == null;
				if (flag)
				{
					throw new ArgumentNullException("mbp");
				}
				bool flag2 = !this.IsValid();
				if (flag2)
				{
					throw new InvalidOperationException("Invalid ProceduralTextureStack " + this.name);
				}
				Procedural.Binding.BindToMaterialPropertyBlock(this.handle, mpb, this.name);
			}

			// Token: 0x06000076 RID: 118 RVA: 0x00003048 File Offset: 0x00001248
			public void BindToMaterial(Material mat)
			{
				bool flag = mat == null;
				if (flag)
				{
					throw new ArgumentNullException("mat");
				}
				bool flag2 = !this.IsValid();
				if (flag2)
				{
					throw new InvalidOperationException("Invalid ProceduralTextureStack " + this.name);
				}
				Procedural.Binding.BindToMaterial(this.handle, mat, this.name);
			}

			// Token: 0x06000077 RID: 119 RVA: 0x000030A4 File Offset: 0x000012A4
			public void BindGlobally()
			{
				bool flag = !this.IsValid();
				if (flag)
				{
					throw new InvalidOperationException("Invalid ProceduralTextureStack " + this.name);
				}
				Procedural.Binding.BindGlobally(this.handle, this.name);
			}

			// Token: 0x06000078 RID: 120 RVA: 0x000030E8 File Offset: 0x000012E8
			public void RequestRegion(Rect r, int mipMap, int numMips)
			{
				bool flag = !this.IsValid();
				if (flag)
				{
					throw new InvalidOperationException("Invalid ProceduralTextureStack " + this.name);
				}
				Procedural.Binding.RequestRegion(this.handle, r, mipMap, numMips);
			}

			// Token: 0x06000079 RID: 121 RVA: 0x0000312C File Offset: 0x0000132C
			public void InvalidateRegion(Rect r, int mipMap, int numMips)
			{
				bool flag = !this.IsValid();
				if (flag)
				{
					throw new InvalidOperationException("Invalid ProceduralTextureStack " + this.name);
				}
				Procedural.Binding.InvalidateRegion(this.handle, r, mipMap, numMips);
			}

			// Token: 0x0600007A RID: 122 RVA: 0x00003170 File Offset: 0x00001370
			public void EvictRegion(Rect r, int mipMap, int numMips)
			{
				bool flag = !this.IsValid();
				if (flag)
				{
					throw new InvalidOperationException("Invalid ProceduralTextureStack " + this.name);
				}
				Procedural.Binding.EvictRegion(this.handle, r, mipMap, numMips);
			}

			// Token: 0x04000046 RID: 70
			internal ulong handle;

			// Token: 0x04000047 RID: 71
			public static readonly int borderSize = 8;

			// Token: 0x04000048 RID: 72
			private string name;

			// Token: 0x04000049 RID: 73
			private Procedural.CreationParameters creationParams;

			// Token: 0x0400004A RID: 74
			public const int AllMips = 2147483647;
		}

		// Token: 0x02000018 RID: 24
		public sealed class GPUTextureStack : Procedural.TextureStackBase<Procedural.GPUTextureStackRequestParameters>
		{
			// Token: 0x0600007C RID: 124 RVA: 0x000031B9 File Offset: 0x000013B9
			public GPUTextureStack(string _name, Procedural.CreationParameters creationParams)
				: base(_name, creationParams, true)
			{
			}
		}

		// Token: 0x02000019 RID: 25
		public sealed class CPUTextureStack : Procedural.TextureStackBase<Procedural.CPUTextureStackRequestParameters>
		{
			// Token: 0x0600007D RID: 125 RVA: 0x000031C6 File Offset: 0x000013C6
			public CPUTextureStack(string _name, Procedural.CreationParameters creationParams)
				: base(_name, creationParams, false)
			{
			}
		}
	}
}
