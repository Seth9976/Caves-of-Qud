using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering.VirtualTexturing
{
	// Token: 0x0200000B RID: 11
	[StaticAccessor("VirtualTexturing::Streaming", StaticAccessorType.DoubleColon)]
	[NativeHeader("Modules/VirtualTexturing/ScriptBindings/VirtualTexturing.bindings.h")]
	public static class Streaming
	{
		// Token: 0x0600002E RID: 46 RVA: 0x00002418 File Offset: 0x00000618
		[NativeThrows]
		public static void RequestRegion([NotNull] Material mat, int stackNameId, Rect r, int mipMap, int numMips)
		{
			if (mat == null)
			{
				ThrowHelper.ThrowArgumentNullException(mat, "mat");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(mat);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(mat, "mat");
			}
			Streaming.RequestRegion_Injected(intPtr, stackNameId, ref r, mipMap, numMips);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002454 File Offset: 0x00000654
		[NativeThrows]
		public static void GetTextureStackSize([NotNull] Material mat, int stackNameId, out int width, out int height)
		{
			if (mat == null)
			{
				ThrowHelper.ThrowArgumentNullException(mat, "mat");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(mat);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(mat, "mat");
			}
			Streaming.GetTextureStackSize_Injected(intPtr, stackNameId, out width, out height);
		}

		// Token: 0x06000030 RID: 48
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetCPUCacheSize(int sizeInMegabytes);

		// Token: 0x06000031 RID: 49
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetCPUCacheSize();

		// Token: 0x06000032 RID: 50 RVA: 0x00002490 File Offset: 0x00000690
		[NativeThrows]
		public unsafe static void SetGPUCacheSettings(GPUCacheSetting[] cacheSettings)
		{
			Span<GPUCacheSetting> span = new Span<GPUCacheSetting>(cacheSettings);
			fixed (GPUCacheSetting* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				Streaming.SetGPUCacheSettings_Injected(ref managedSpanWrapper);
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000024C8 File Offset: 0x000006C8
		[NativeThrows]
		public static GPUCacheSetting[] GetGPUCacheSettings()
		{
			GPUCacheSetting[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				Streaming.GetGPUCacheSettings_Injected(out blittableArrayWrapper);
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

		// Token: 0x06000034 RID: 52
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void EnableMipPreloading(int texturesPerFrame, int mipCount);

		// Token: 0x06000035 RID: 53
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RequestRegion_Injected(IntPtr mat, int stackNameId, [In] ref Rect r, int mipMap, int numMips);

		// Token: 0x06000036 RID: 54
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetTextureStackSize_Injected(IntPtr mat, int stackNameId, out int width, out int height);

		// Token: 0x06000037 RID: 55
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGPUCacheSettings_Injected(ref ManagedSpanWrapper cacheSettings);

		// Token: 0x06000038 RID: 56
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetGPUCacheSettings_Injected(out BlittableArrayWrapper ret);
	}
}
