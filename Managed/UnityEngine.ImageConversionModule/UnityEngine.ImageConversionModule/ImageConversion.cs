using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine
{
	// Token: 0x02000002 RID: 2
	[NativeHeader("Modules/ImageConversion/ScriptBindings/ImageConversion.bindings.h")]
	public static class ImageConversion
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002067 File Offset: 0x00000267
		public static bool EnableLegacyPngGammaRuntimeLoadBehavior
		{
			get
			{
				return ImageConversion.GetEnableLegacyPngGammaRuntimeLoadBehavior();
			}
			set
			{
				ImageConversion.SetEnableLegacyPngGammaRuntimeLoadBehavior(value);
			}
		}

		// Token: 0x06000003 RID: 3
		[NativeMethod(Name = "ImageConversionBindings::GetEnableLegacyPngGammaRuntimeLoadBehavior", IsFreeFunction = true, ThrowsException = false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetEnableLegacyPngGammaRuntimeLoadBehavior();

		// Token: 0x06000004 RID: 4
		[NativeMethod(Name = "ImageConversionBindings::SetEnableLegacyPngGammaRuntimeLoadBehavior", IsFreeFunction = true, ThrowsException = false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetEnableLegacyPngGammaRuntimeLoadBehavior(bool enable);

		// Token: 0x06000005 RID: 5 RVA: 0x00002074 File Offset: 0x00000274
		[NativeMethod(Name = "ImageConversionBindings::EncodeToTGA", IsFreeFunction = true, ThrowsException = true)]
		public static byte[] EncodeToTGA(this Texture2D tex)
		{
			byte[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				ImageConversion.EncodeToTGA_Injected(Object.MarshalledUnityObject.Marshal<Texture2D>(tex), out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				byte[] array;
				blittableArrayWrapper.Unmarshal<byte>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020B0 File Offset: 0x000002B0
		[NativeMethod(Name = "ImageConversionBindings::EncodeToPNG", IsFreeFunction = true, ThrowsException = true)]
		public static byte[] EncodeToPNG(this Texture2D tex)
		{
			byte[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				ImageConversion.EncodeToPNG_Injected(Object.MarshalledUnityObject.Marshal<Texture2D>(tex), out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				byte[] array;
				blittableArrayWrapper.Unmarshal<byte>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020EC File Offset: 0x000002EC
		[NativeMethod(Name = "ImageConversionBindings::EncodeToJPG", IsFreeFunction = true, ThrowsException = true)]
		public static byte[] EncodeToJPG(this Texture2D tex, int quality)
		{
			byte[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				ImageConversion.EncodeToJPG_Injected(Object.MarshalledUnityObject.Marshal<Texture2D>(tex), quality, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				byte[] array;
				blittableArrayWrapper.Unmarshal<byte>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002128 File Offset: 0x00000328
		public static byte[] EncodeToJPG(this Texture2D tex)
		{
			return tex.EncodeToJPG(75);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002144 File Offset: 0x00000344
		[NativeMethod(Name = "ImageConversionBindings::EncodeToEXR", IsFreeFunction = true, ThrowsException = true)]
		public static byte[] EncodeToEXR(this Texture2D tex, Texture2D.EXRFlags flags)
		{
			byte[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				ImageConversion.EncodeToEXR_Injected(Object.MarshalledUnityObject.Marshal<Texture2D>(tex), flags, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				byte[] array;
				blittableArrayWrapper.Unmarshal<byte>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002180 File Offset: 0x00000380
		public static byte[] EncodeToEXR(this Texture2D tex)
		{
			return tex.EncodeToEXR(Texture2D.EXRFlags.None);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000219C File Offset: 0x0000039C
		[NativeMethod(Name = "ImageConversionBindings::EncodeToR2D", IsFreeFunction = true, ThrowsException = true)]
		internal static byte[] EncodeToR2DInternal(this Texture2D tex)
		{
			byte[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				ImageConversion.EncodeToR2DInternal_Injected(Object.MarshalledUnityObject.Marshal<Texture2D>(tex), out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				byte[] array;
				blittableArrayWrapper.Unmarshal<byte>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021D8 File Offset: 0x000003D8
		[NativeMethod(Name = "ImageConversionBindings::LoadImage", IsFreeFunction = true)]
		public unsafe static bool LoadImage([NotNull] this Texture2D tex, ReadOnlySpan<byte> data, bool markNonReadable)
		{
			if (tex == null)
			{
				ThrowHelper.ThrowArgumentNullException(tex, "tex");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(tex);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(tex, "tex");
			}
			ReadOnlySpan<byte> readOnlySpan = data;
			bool flag;
			fixed (byte* pinnableReference = readOnlySpan.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, readOnlySpan.Length);
				flag = ImageConversion.LoadImage_Injected(intPtr, ref managedSpanWrapper, markNonReadable);
			}
			return flag;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000222F File Offset: 0x0000042F
		public static bool LoadImage(this Texture2D tex, ReadOnlySpan<byte> data)
		{
			return tex.LoadImage(data, false);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002239 File Offset: 0x00000439
		public static bool LoadImage(this Texture2D tex, byte[] data, bool markNonReadable)
		{
			return tex.LoadImage(new ReadOnlySpan<byte>(data), markNonReadable);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002248 File Offset: 0x00000448
		public static bool LoadImage(this Texture2D tex, byte[] data)
		{
			return tex.LoadImage(new ReadOnlySpan<byte>(data), false);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002258 File Offset: 0x00000458
		[FreeFunction("ImageConversionBindings::EncodeArrayToTGA", true)]
		public static byte[] EncodeArrayToTGA(Array array, GraphicsFormat format, uint width, uint height, uint rowBytes = 0U)
		{
			byte[] array3;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				ImageConversion.EncodeArrayToTGA_Injected(array, format, width, height, rowBytes, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				byte[] array2;
				blittableArrayWrapper.Unmarshal<byte>(ref array2);
				array3 = array2;
			}
			return array3;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002294 File Offset: 0x00000494
		[FreeFunction("ImageConversionBindings::EncodeArrayToPNG", true)]
		public static byte[] EncodeArrayToPNG(Array array, GraphicsFormat format, uint width, uint height, uint rowBytes = 0U)
		{
			byte[] array3;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				ImageConversion.EncodeArrayToPNG_Injected(array, format, width, height, rowBytes, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				byte[] array2;
				blittableArrayWrapper.Unmarshal<byte>(ref array2);
				array3 = array2;
			}
			return array3;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022D0 File Offset: 0x000004D0
		[FreeFunction("ImageConversionBindings::EncodeArrayToJPG", true)]
		public static byte[] EncodeArrayToJPG(Array array, GraphicsFormat format, uint width, uint height, uint rowBytes = 0U, int quality = 75)
		{
			byte[] array3;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				ImageConversion.EncodeArrayToJPG_Injected(array, format, width, height, rowBytes, quality, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				byte[] array2;
				blittableArrayWrapper.Unmarshal<byte>(ref array2);
				array3 = array2;
			}
			return array3;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000230C File Offset: 0x0000050C
		[FreeFunction("ImageConversionBindings::EncodeArrayToEXR", true)]
		public static byte[] EncodeArrayToEXR(Array array, GraphicsFormat format, uint width, uint height, uint rowBytes = 0U, Texture2D.EXRFlags flags = Texture2D.EXRFlags.None)
		{
			byte[] array3;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				ImageConversion.EncodeArrayToEXR_Injected(array, format, width, height, rowBytes, flags, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				byte[] array2;
				blittableArrayWrapper.Unmarshal<byte>(ref array2);
				array3 = array2;
			}
			return array3;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002348 File Offset: 0x00000548
		[FreeFunction("ImageConversionBindings::EncodeArrayToR2D", true)]
		internal static byte[] EncodeArrayToR2DInternal(Array array, GraphicsFormat format, uint width, uint height, uint rowBytes = 0U)
		{
			byte[] array3;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				ImageConversion.EncodeArrayToR2DInternal_Injected(array, format, width, height, rowBytes, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				byte[] array2;
				blittableArrayWrapper.Unmarshal<byte>(ref array2);
				array3 = array2;
			}
			return array3;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002384 File Offset: 0x00000584
		public unsafe static NativeArray<byte> EncodeNativeArrayToTGA<T>(NativeArray<T> input, GraphicsFormat format, uint width, uint height, uint rowBytes = 0U) where T : struct
		{
			int num = input.Length * UnsafeUtility.SizeOf<T>();
			void* ptr = ImageConversion.UnsafeEncodeNativeArrayToTGA(NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<T>(input), ref num, format, width, height, rowBytes);
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<byte>(ptr, num, Allocator.Persistent);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000023C4 File Offset: 0x000005C4
		public unsafe static NativeArray<byte> EncodeNativeArrayToPNG<T>(NativeArray<T> input, GraphicsFormat format, uint width, uint height, uint rowBytes = 0U) where T : struct
		{
			int num = input.Length * UnsafeUtility.SizeOf<T>();
			void* ptr = ImageConversion.UnsafeEncodeNativeArrayToPNG(NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<T>(input), ref num, format, width, height, rowBytes);
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<byte>(ptr, num, Allocator.Persistent);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002404 File Offset: 0x00000604
		public unsafe static NativeArray<byte> EncodeNativeArrayToJPG<T>(NativeArray<T> input, GraphicsFormat format, uint width, uint height, uint rowBytes = 0U, int quality = 75) where T : struct
		{
			int num = input.Length * UnsafeUtility.SizeOf<T>();
			void* ptr = ImageConversion.UnsafeEncodeNativeArrayToJPG(NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<T>(input), ref num, format, width, height, rowBytes, quality);
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<byte>(ptr, num, Allocator.Persistent);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002444 File Offset: 0x00000644
		public unsafe static NativeArray<byte> EncodeNativeArrayToEXR<T>(NativeArray<T> input, GraphicsFormat format, uint width, uint height, uint rowBytes = 0U, Texture2D.EXRFlags flags = Texture2D.EXRFlags.None) where T : struct
		{
			int num = input.Length * UnsafeUtility.SizeOf<T>();
			void* ptr = ImageConversion.UnsafeEncodeNativeArrayToEXR(NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<T>(input), ref num, format, width, height, rowBytes, flags);
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<byte>(ptr, num, Allocator.Persistent);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002484 File Offset: 0x00000684
		internal unsafe static NativeArray<byte> EncodeNativeArrayToR2DInternal<T>(NativeArray<T> input, GraphicsFormat format, uint width, uint height, uint rowBytes = 0U) where T : struct
		{
			int num = input.Length * UnsafeUtility.SizeOf<T>();
			void* ptr = ImageConversion.UnsafeEncodeNativeArrayToR2D(NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<T>(input), ref num, format, width, height, rowBytes);
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<byte>(ptr, num, Allocator.Persistent);
		}

		// Token: 0x0600001A RID: 26
		[FreeFunction("ImageConversionBindings::UnsafeEncodeNativeArrayToTGA", true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void* UnsafeEncodeNativeArrayToTGA(void* array, ref int sizeInBytes, GraphicsFormat format, uint width, uint height, uint rowBytes = 0U);

		// Token: 0x0600001B RID: 27
		[FreeFunction("ImageConversionBindings::UnsafeEncodeNativeArrayToPNG", true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void* UnsafeEncodeNativeArrayToPNG(void* array, ref int sizeInBytes, GraphicsFormat format, uint width, uint height, uint rowBytes = 0U);

		// Token: 0x0600001C RID: 28
		[FreeFunction("ImageConversionBindings::UnsafeEncodeNativeArrayToJPG", true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void* UnsafeEncodeNativeArrayToJPG(void* array, ref int sizeInBytes, GraphicsFormat format, uint width, uint height, uint rowBytes = 0U, int quality = 75);

		// Token: 0x0600001D RID: 29
		[FreeFunction("ImageConversionBindings::UnsafeEncodeNativeArrayToEXR", true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void* UnsafeEncodeNativeArrayToEXR(void* array, ref int sizeInBytes, GraphicsFormat format, uint width, uint height, uint rowBytes = 0U, Texture2D.EXRFlags flags = Texture2D.EXRFlags.None);

		// Token: 0x0600001E RID: 30
		[FreeFunction("ImageConversionBindings::UnsafeEncodeNativeArrayToR2D", true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void* UnsafeEncodeNativeArrayToR2D(void* array, ref int sizeInBytes, GraphicsFormat format, uint width, uint height, uint rowBytes = 0U);

		// Token: 0x0600001F RID: 31
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EncodeToTGA_Injected(IntPtr tex, out BlittableArrayWrapper ret);

		// Token: 0x06000020 RID: 32
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EncodeToPNG_Injected(IntPtr tex, out BlittableArrayWrapper ret);

		// Token: 0x06000021 RID: 33
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EncodeToJPG_Injected(IntPtr tex, int quality, out BlittableArrayWrapper ret);

		// Token: 0x06000022 RID: 34
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EncodeToEXR_Injected(IntPtr tex, Texture2D.EXRFlags flags, out BlittableArrayWrapper ret);

		// Token: 0x06000023 RID: 35
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EncodeToR2DInternal_Injected(IntPtr tex, out BlittableArrayWrapper ret);

		// Token: 0x06000024 RID: 36
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool LoadImage_Injected(IntPtr tex, ref ManagedSpanWrapper data, bool markNonReadable);

		// Token: 0x06000025 RID: 37
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EncodeArrayToTGA_Injected(Array array, GraphicsFormat format, uint width, uint height, uint rowBytes, out BlittableArrayWrapper ret);

		// Token: 0x06000026 RID: 38
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EncodeArrayToPNG_Injected(Array array, GraphicsFormat format, uint width, uint height, uint rowBytes, out BlittableArrayWrapper ret);

		// Token: 0x06000027 RID: 39
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EncodeArrayToJPG_Injected(Array array, GraphicsFormat format, uint width, uint height, uint rowBytes, int quality, out BlittableArrayWrapper ret);

		// Token: 0x06000028 RID: 40
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EncodeArrayToEXR_Injected(Array array, GraphicsFormat format, uint width, uint height, uint rowBytes, Texture2D.EXRFlags flags, out BlittableArrayWrapper ret);

		// Token: 0x06000029 RID: 41
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EncodeArrayToR2DInternal_Injected(Array array, GraphicsFormat format, uint width, uint height, uint rowBytes, out BlittableArrayWrapper ret);
	}
}
