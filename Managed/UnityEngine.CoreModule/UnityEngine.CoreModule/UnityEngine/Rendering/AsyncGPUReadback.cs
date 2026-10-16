using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering
{
	// Token: 0x0200047A RID: 1146
	[StaticAccessor("AsyncGPUReadbackManager::GetInstance()", StaticAccessorType.Dot)]
	public static class AsyncGPUReadback
	{
		// Token: 0x06002D31 RID: 11569 RVA: 0x00055654 File Offset: 0x00053854
		internal static void ValidateFormat(Texture src, GraphicsFormat dstformat)
		{
			GraphicsFormat format = GraphicsFormatUtility.GetFormat(src);
			bool flag = !SystemInfo.IsFormatSupported(format, GraphicsFormatUsage.ReadPixels);
			if (flag)
			{
				Debug.LogError(string.Format("'{0}' doesn't support ReadPixels usage on this platform. Async GPU readback failed.", format));
			}
		}

		// Token: 0x06002D32 RID: 11570
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void WaitAllRequests();

		// Token: 0x06002D33 RID: 11571 RVA: 0x00055694 File Offset: 0x00053894
		public static AsyncGPUReadbackRequest Request(ComputeBuffer src, Action<AsyncGPUReadbackRequest> callback = null)
		{
			AsyncGPUReadbackRequest asyncGPUReadbackRequest = AsyncGPUReadback.Request_Internal_ComputeBuffer_1(src, null);
			asyncGPUReadbackRequest.SetScriptingCallback(callback);
			return asyncGPUReadbackRequest;
		}

		// Token: 0x06002D34 RID: 11572 RVA: 0x000556BC File Offset: 0x000538BC
		public static AsyncGPUReadbackRequest Request(ComputeBuffer src, int size, int offset, Action<AsyncGPUReadbackRequest> callback = null)
		{
			AsyncGPUReadbackRequest asyncGPUReadbackRequest = AsyncGPUReadback.Request_Internal_ComputeBuffer_2(src, size, offset, null);
			asyncGPUReadbackRequest.SetScriptingCallback(callback);
			return asyncGPUReadbackRequest;
		}

		// Token: 0x06002D35 RID: 11573 RVA: 0x000556E4 File Offset: 0x000538E4
		public static AsyncGPUReadbackRequest Request(GraphicsBuffer src, Action<AsyncGPUReadbackRequest> callback = null)
		{
			AsyncGPUReadbackRequest asyncGPUReadbackRequest = AsyncGPUReadback.Request_Internal_GraphicsBuffer_1(src, null);
			asyncGPUReadbackRequest.SetScriptingCallback(callback);
			return asyncGPUReadbackRequest;
		}

		// Token: 0x06002D36 RID: 11574 RVA: 0x0005570C File Offset: 0x0005390C
		public static AsyncGPUReadbackRequest Request(GraphicsBuffer src, int size, int offset, Action<AsyncGPUReadbackRequest> callback = null)
		{
			AsyncGPUReadbackRequest asyncGPUReadbackRequest = AsyncGPUReadback.Request_Internal_GraphicsBuffer_2(src, size, offset, null);
			asyncGPUReadbackRequest.SetScriptingCallback(callback);
			return asyncGPUReadbackRequest;
		}

		// Token: 0x06002D37 RID: 11575 RVA: 0x00055734 File Offset: 0x00053934
		public static AsyncGPUReadbackRequest Request(Texture src, int mipIndex = 0, Action<AsyncGPUReadbackRequest> callback = null)
		{
			AsyncGPUReadbackRequest asyncGPUReadbackRequest = AsyncGPUReadback.Request_Internal_Texture_1(src, mipIndex, null);
			asyncGPUReadbackRequest.SetScriptingCallback(callback);
			return asyncGPUReadbackRequest;
		}

		// Token: 0x06002D38 RID: 11576 RVA: 0x0005575C File Offset: 0x0005395C
		public static AsyncGPUReadbackRequest Request(Texture src, int mipIndex, TextureFormat dstFormat, Action<AsyncGPUReadbackRequest> callback = null)
		{
			return AsyncGPUReadback.Request(src, mipIndex, GraphicsFormatUtility.GetGraphicsFormat(dstFormat, QualitySettings.activeColorSpace == ColorSpace.Linear), callback);
		}

		// Token: 0x06002D39 RID: 11577 RVA: 0x00055784 File Offset: 0x00053984
		public static AsyncGPUReadbackRequest Request(Texture src, int mipIndex, GraphicsFormat dstFormat, Action<AsyncGPUReadbackRequest> callback = null)
		{
			AsyncGPUReadback.ValidateFormat(src, dstFormat);
			AsyncGPUReadbackRequest asyncGPUReadbackRequest = AsyncGPUReadback.Request_Internal_Texture_2(src, mipIndex, dstFormat, null);
			asyncGPUReadbackRequest.SetScriptingCallback(callback);
			return asyncGPUReadbackRequest;
		}

		// Token: 0x06002D3A RID: 11578 RVA: 0x000557B4 File Offset: 0x000539B4
		public static AsyncGPUReadbackRequest Request(Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, Action<AsyncGPUReadbackRequest> callback = null)
		{
			AsyncGPUReadbackRequest asyncGPUReadbackRequest = AsyncGPUReadback.Request_Internal_Texture_3(src, mipIndex, x, width, y, height, z, depth, null);
			asyncGPUReadbackRequest.SetScriptingCallback(callback);
			return asyncGPUReadbackRequest;
		}

		// Token: 0x06002D3B RID: 11579 RVA: 0x000557E8 File Offset: 0x000539E8
		public static AsyncGPUReadbackRequest Request(Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, TextureFormat dstFormat, Action<AsyncGPUReadbackRequest> callback = null)
		{
			return AsyncGPUReadback.Request(src, mipIndex, x, width, y, height, z, depth, GraphicsFormatUtility.GetGraphicsFormat(dstFormat, QualitySettings.activeColorSpace == ColorSpace.Linear), callback);
		}

		// Token: 0x06002D3C RID: 11580 RVA: 0x0005581C File Offset: 0x00053A1C
		public static AsyncGPUReadbackRequest Request(Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, GraphicsFormat dstFormat, Action<AsyncGPUReadbackRequest> callback = null)
		{
			AsyncGPUReadback.ValidateFormat(src, dstFormat);
			AsyncGPUReadbackRequest asyncGPUReadbackRequest = AsyncGPUReadback.Request_Internal_Texture_4(src, mipIndex, x, width, y, height, z, depth, dstFormat, null);
			asyncGPUReadbackRequest.SetScriptingCallback(callback);
			return asyncGPUReadbackRequest;
		}

		// Token: 0x06002D3D RID: 11581 RVA: 0x0005585C File Offset: 0x00053A5C
		public unsafe static AsyncGPUReadbackRequest RequestIntoNativeArray<T>(ref NativeArray<T> output, ComputeBuffer src, Action<AsyncGPUReadbackRequest> callback = null) where T : struct
		{
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			AsyncGPUReadbackRequest asyncGPUReadbackRequest = AsyncGPUReadback.Request_Internal_ComputeBuffer_1(src, &asyncRequestNativeArrayData);
			asyncGPUReadbackRequest.SetScriptingCallback(callback);
			return asyncGPUReadbackRequest;
		}

		// Token: 0x06002D3E RID: 11582 RVA: 0x00055890 File Offset: 0x00053A90
		public unsafe static AsyncGPUReadbackRequest RequestIntoNativeArray<T>(ref NativeArray<T> output, ComputeBuffer src, int size, int offset, Action<AsyncGPUReadbackRequest> callback = null) where T : struct
		{
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			AsyncGPUReadbackRequest asyncGPUReadbackRequest = AsyncGPUReadback.Request_Internal_ComputeBuffer_2(src, size, offset, &asyncRequestNativeArrayData);
			asyncGPUReadbackRequest.SetScriptingCallback(callback);
			return asyncGPUReadbackRequest;
		}

		// Token: 0x06002D3F RID: 11583 RVA: 0x000558C8 File Offset: 0x00053AC8
		public unsafe static AsyncGPUReadbackRequest RequestIntoNativeArray<T>(ref NativeArray<T> output, GraphicsBuffer src, Action<AsyncGPUReadbackRequest> callback = null) where T : struct
		{
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			AsyncGPUReadbackRequest asyncGPUReadbackRequest = AsyncGPUReadback.Request_Internal_GraphicsBuffer_1(src, &asyncRequestNativeArrayData);
			asyncGPUReadbackRequest.SetScriptingCallback(callback);
			return asyncGPUReadbackRequest;
		}

		// Token: 0x06002D40 RID: 11584 RVA: 0x000558FC File Offset: 0x00053AFC
		public unsafe static AsyncGPUReadbackRequest RequestIntoNativeArray<T>(ref NativeArray<T> output, GraphicsBuffer src, int size, int offset, Action<AsyncGPUReadbackRequest> callback = null) where T : struct
		{
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			AsyncGPUReadbackRequest asyncGPUReadbackRequest = AsyncGPUReadback.Request_Internal_GraphicsBuffer_2(src, size, offset, &asyncRequestNativeArrayData);
			asyncGPUReadbackRequest.SetScriptingCallback(callback);
			return asyncGPUReadbackRequest;
		}

		// Token: 0x06002D41 RID: 11585 RVA: 0x00055934 File Offset: 0x00053B34
		public unsafe static AsyncGPUReadbackRequest RequestIntoNativeArray<T>(ref NativeArray<T> output, Texture src, int mipIndex = 0, Action<AsyncGPUReadbackRequest> callback = null) where T : struct
		{
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			AsyncGPUReadbackRequest asyncGPUReadbackRequest = AsyncGPUReadback.Request_Internal_Texture_1(src, mipIndex, &asyncRequestNativeArrayData);
			asyncGPUReadbackRequest.SetScriptingCallback(callback);
			return asyncGPUReadbackRequest;
		}

		// Token: 0x06002D42 RID: 11586 RVA: 0x00055968 File Offset: 0x00053B68
		public static AsyncGPUReadbackRequest RequestIntoNativeArray<T>(ref NativeArray<T> output, Texture src, int mipIndex, TextureFormat dstFormat, Action<AsyncGPUReadbackRequest> callback = null) where T : struct
		{
			return AsyncGPUReadback.RequestIntoNativeArray<T>(ref output, src, mipIndex, GraphicsFormatUtility.GetGraphicsFormat(dstFormat, QualitySettings.activeColorSpace == ColorSpace.Linear), callback);
		}

		// Token: 0x06002D43 RID: 11587 RVA: 0x00055994 File Offset: 0x00053B94
		public unsafe static AsyncGPUReadbackRequest RequestIntoNativeArray<T>(ref NativeArray<T> output, Texture src, int mipIndex, GraphicsFormat dstFormat, Action<AsyncGPUReadbackRequest> callback = null) where T : struct
		{
			AsyncGPUReadback.ValidateFormat(src, dstFormat);
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			AsyncGPUReadbackRequest asyncGPUReadbackRequest = AsyncGPUReadback.Request_Internal_Texture_2(src, mipIndex, dstFormat, &asyncRequestNativeArrayData);
			asyncGPUReadbackRequest.SetScriptingCallback(callback);
			return asyncGPUReadbackRequest;
		}

		// Token: 0x06002D44 RID: 11588 RVA: 0x000559D4 File Offset: 0x00053BD4
		public static AsyncGPUReadbackRequest RequestIntoNativeArray<T>(ref NativeArray<T> output, Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, TextureFormat dstFormat, Action<AsyncGPUReadbackRequest> callback = null) where T : struct
		{
			return AsyncGPUReadback.RequestIntoNativeArray<T>(ref output, src, mipIndex, x, width, y, height, z, depth, GraphicsFormatUtility.GetGraphicsFormat(dstFormat, QualitySettings.activeColorSpace == ColorSpace.Linear), callback);
		}

		// Token: 0x06002D45 RID: 11589 RVA: 0x00055A0C File Offset: 0x00053C0C
		public unsafe static AsyncGPUReadbackRequest RequestIntoNativeArray<T>(ref NativeArray<T> output, Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, GraphicsFormat dstFormat, Action<AsyncGPUReadbackRequest> callback = null) where T : struct
		{
			AsyncGPUReadback.ValidateFormat(src, dstFormat);
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			AsyncGPUReadbackRequest asyncGPUReadbackRequest = AsyncGPUReadback.Request_Internal_Texture_4(src, mipIndex, x, width, y, height, z, depth, dstFormat, &asyncRequestNativeArrayData);
			asyncGPUReadbackRequest.SetScriptingCallback(callback);
			return asyncGPUReadbackRequest;
		}

		// Token: 0x06002D46 RID: 11590 RVA: 0x00055A58 File Offset: 0x00053C58
		public unsafe static AsyncGPUReadbackRequest RequestIntoNativeSlice<T>(ref NativeSlice<T> output, ComputeBuffer src, Action<AsyncGPUReadbackRequest> callback = null) where T : struct
		{
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			AsyncGPUReadbackRequest asyncGPUReadbackRequest = AsyncGPUReadback.Request_Internal_ComputeBuffer_1(src, &asyncRequestNativeArrayData);
			asyncGPUReadbackRequest.SetScriptingCallback(callback);
			return asyncGPUReadbackRequest;
		}

		// Token: 0x06002D47 RID: 11591 RVA: 0x00055A8C File Offset: 0x00053C8C
		public unsafe static AsyncGPUReadbackRequest RequestIntoNativeSlice<T>(ref NativeSlice<T> output, ComputeBuffer src, int size, int offset, Action<AsyncGPUReadbackRequest> callback = null) where T : struct
		{
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			AsyncGPUReadbackRequest asyncGPUReadbackRequest = AsyncGPUReadback.Request_Internal_ComputeBuffer_2(src, size, offset, &asyncRequestNativeArrayData);
			asyncGPUReadbackRequest.SetScriptingCallback(callback);
			return asyncGPUReadbackRequest;
		}

		// Token: 0x06002D48 RID: 11592 RVA: 0x00055AC4 File Offset: 0x00053CC4
		public unsafe static AsyncGPUReadbackRequest RequestIntoNativeSlice<T>(ref NativeSlice<T> output, GraphicsBuffer src, Action<AsyncGPUReadbackRequest> callback = null) where T : struct
		{
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			AsyncGPUReadbackRequest asyncGPUReadbackRequest = AsyncGPUReadback.Request_Internal_GraphicsBuffer_1(src, &asyncRequestNativeArrayData);
			asyncGPUReadbackRequest.SetScriptingCallback(callback);
			return asyncGPUReadbackRequest;
		}

		// Token: 0x06002D49 RID: 11593 RVA: 0x00055AF8 File Offset: 0x00053CF8
		public unsafe static AsyncGPUReadbackRequest RequestIntoNativeSlice<T>(ref NativeSlice<T> output, GraphicsBuffer src, int size, int offset, Action<AsyncGPUReadbackRequest> callback = null) where T : struct
		{
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			AsyncGPUReadbackRequest asyncGPUReadbackRequest = AsyncGPUReadback.Request_Internal_GraphicsBuffer_2(src, size, offset, &asyncRequestNativeArrayData);
			asyncGPUReadbackRequest.SetScriptingCallback(callback);
			return asyncGPUReadbackRequest;
		}

		// Token: 0x06002D4A RID: 11594 RVA: 0x00055B30 File Offset: 0x00053D30
		public unsafe static AsyncGPUReadbackRequest RequestIntoNativeSlice<T>(ref NativeSlice<T> output, Texture src, int mipIndex = 0, Action<AsyncGPUReadbackRequest> callback = null) where T : struct
		{
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			AsyncGPUReadbackRequest asyncGPUReadbackRequest = AsyncGPUReadback.Request_Internal_Texture_1(src, mipIndex, &asyncRequestNativeArrayData);
			asyncGPUReadbackRequest.SetScriptingCallback(callback);
			return asyncGPUReadbackRequest;
		}

		// Token: 0x06002D4B RID: 11595 RVA: 0x00055B64 File Offset: 0x00053D64
		public static AsyncGPUReadbackRequest RequestIntoNativeSlice<T>(ref NativeSlice<T> output, Texture src, int mipIndex, TextureFormat dstFormat, Action<AsyncGPUReadbackRequest> callback = null) where T : struct
		{
			return AsyncGPUReadback.RequestIntoNativeSlice<T>(ref output, src, mipIndex, GraphicsFormatUtility.GetGraphicsFormat(dstFormat, QualitySettings.activeColorSpace == ColorSpace.Linear), callback);
		}

		// Token: 0x06002D4C RID: 11596 RVA: 0x00055B90 File Offset: 0x00053D90
		public unsafe static AsyncGPUReadbackRequest RequestIntoNativeSlice<T>(ref NativeSlice<T> output, Texture src, int mipIndex, GraphicsFormat dstFormat, Action<AsyncGPUReadbackRequest> callback = null) where T : struct
		{
			AsyncGPUReadback.ValidateFormat(src, dstFormat);
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			AsyncGPUReadbackRequest asyncGPUReadbackRequest = AsyncGPUReadback.Request_Internal_Texture_2(src, mipIndex, dstFormat, &asyncRequestNativeArrayData);
			asyncGPUReadbackRequest.SetScriptingCallback(callback);
			return asyncGPUReadbackRequest;
		}

		// Token: 0x06002D4D RID: 11597 RVA: 0x00055BD0 File Offset: 0x00053DD0
		public static AsyncGPUReadbackRequest RequestIntoNativeSlice<T>(ref NativeSlice<T> output, Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, TextureFormat dstFormat, Action<AsyncGPUReadbackRequest> callback = null) where T : struct
		{
			return AsyncGPUReadback.RequestIntoNativeSlice<T>(ref output, src, mipIndex, x, width, y, height, z, depth, GraphicsFormatUtility.GetGraphicsFormat(dstFormat, QualitySettings.activeColorSpace == ColorSpace.Linear), callback);
		}

		// Token: 0x06002D4E RID: 11598 RVA: 0x00055C08 File Offset: 0x00053E08
		public unsafe static AsyncGPUReadbackRequest RequestIntoNativeSlice<T>(ref NativeSlice<T> output, Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, GraphicsFormat dstFormat, Action<AsyncGPUReadbackRequest> callback = null) where T : struct
		{
			AsyncGPUReadback.ValidateFormat(src, dstFormat);
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			AsyncGPUReadbackRequest asyncGPUReadbackRequest = AsyncGPUReadback.Request_Internal_Texture_4(src, mipIndex, x, width, y, height, z, depth, dstFormat, &asyncRequestNativeArrayData);
			asyncGPUReadbackRequest.SetScriptingCallback(callback);
			return asyncGPUReadbackRequest;
		}

		// Token: 0x06002D4F RID: 11599 RVA: 0x00055C54 File Offset: 0x00053E54
		[NativeMethod("Request")]
		private unsafe static AsyncGPUReadbackRequest Request_Internal_ComputeBuffer_1([NotNull] ComputeBuffer buffer, AsyncRequestNativeArrayData* data)
		{
			if (buffer == null)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			IntPtr intPtr = ComputeBuffer.BindingsMarshaller.ConvertToNative(buffer);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			AsyncGPUReadbackRequest asyncGPUReadbackRequest;
			AsyncGPUReadback.Request_Internal_ComputeBuffer_1_Injected(intPtr, data, out asyncGPUReadbackRequest);
			return asyncGPUReadbackRequest;
		}

		// Token: 0x06002D50 RID: 11600 RVA: 0x00055C90 File Offset: 0x00053E90
		[NativeMethod("Request")]
		private unsafe static AsyncGPUReadbackRequest Request_Internal_ComputeBuffer_2([NotNull] ComputeBuffer src, int size, int offset, AsyncRequestNativeArrayData* data)
		{
			if (src == null)
			{
				ThrowHelper.ThrowArgumentNullException(src, "src");
			}
			IntPtr intPtr = ComputeBuffer.BindingsMarshaller.ConvertToNative(src);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(src, "src");
			}
			AsyncGPUReadbackRequest asyncGPUReadbackRequest;
			AsyncGPUReadback.Request_Internal_ComputeBuffer_2_Injected(intPtr, size, offset, data, out asyncGPUReadbackRequest);
			return asyncGPUReadbackRequest;
		}

		// Token: 0x06002D51 RID: 11601 RVA: 0x00055CCC File Offset: 0x00053ECC
		[NativeMethod("Request")]
		private unsafe static AsyncGPUReadbackRequest Request_Internal_GraphicsBuffer_1([NotNull] GraphicsBuffer buffer, AsyncRequestNativeArrayData* data)
		{
			if (buffer == null)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			IntPtr intPtr = GraphicsBuffer.BindingsMarshaller.ConvertToNative(buffer);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			AsyncGPUReadbackRequest asyncGPUReadbackRequest;
			AsyncGPUReadback.Request_Internal_GraphicsBuffer_1_Injected(intPtr, data, out asyncGPUReadbackRequest);
			return asyncGPUReadbackRequest;
		}

		// Token: 0x06002D52 RID: 11602 RVA: 0x00055D08 File Offset: 0x00053F08
		[NativeMethod("Request")]
		private unsafe static AsyncGPUReadbackRequest Request_Internal_GraphicsBuffer_2([NotNull] GraphicsBuffer src, int size, int offset, AsyncRequestNativeArrayData* data)
		{
			if (src == null)
			{
				ThrowHelper.ThrowArgumentNullException(src, "src");
			}
			IntPtr intPtr = GraphicsBuffer.BindingsMarshaller.ConvertToNative(src);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(src, "src");
			}
			AsyncGPUReadbackRequest asyncGPUReadbackRequest;
			AsyncGPUReadback.Request_Internal_GraphicsBuffer_2_Injected(intPtr, size, offset, data, out asyncGPUReadbackRequest);
			return asyncGPUReadbackRequest;
		}

		// Token: 0x06002D53 RID: 11603 RVA: 0x00055D44 File Offset: 0x00053F44
		[NativeMethod("Request")]
		private unsafe static AsyncGPUReadbackRequest Request_Internal_Texture_1([NotNull] Texture src, int mipIndex, AsyncRequestNativeArrayData* data)
		{
			if (src == null)
			{
				ThrowHelper.ThrowArgumentNullException(src, "src");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture>(src);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(src, "src");
			}
			AsyncGPUReadbackRequest asyncGPUReadbackRequest;
			AsyncGPUReadback.Request_Internal_Texture_1_Injected(intPtr, mipIndex, data, out asyncGPUReadbackRequest);
			return asyncGPUReadbackRequest;
		}

		// Token: 0x06002D54 RID: 11604 RVA: 0x00055D80 File Offset: 0x00053F80
		[NativeMethod("Request")]
		private unsafe static AsyncGPUReadbackRequest Request_Internal_Texture_2([NotNull] Texture src, int mipIndex, GraphicsFormat dstFormat, AsyncRequestNativeArrayData* data)
		{
			if (src == null)
			{
				ThrowHelper.ThrowArgumentNullException(src, "src");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture>(src);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(src, "src");
			}
			AsyncGPUReadbackRequest asyncGPUReadbackRequest;
			AsyncGPUReadback.Request_Internal_Texture_2_Injected(intPtr, mipIndex, dstFormat, data, out asyncGPUReadbackRequest);
			return asyncGPUReadbackRequest;
		}

		// Token: 0x06002D55 RID: 11605 RVA: 0x00055DBC File Offset: 0x00053FBC
		[NativeMethod("Request")]
		private unsafe static AsyncGPUReadbackRequest Request_Internal_Texture_3([NotNull] Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, AsyncRequestNativeArrayData* data)
		{
			if (src == null)
			{
				ThrowHelper.ThrowArgumentNullException(src, "src");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture>(src);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(src, "src");
			}
			AsyncGPUReadbackRequest asyncGPUReadbackRequest;
			AsyncGPUReadback.Request_Internal_Texture_3_Injected(intPtr, mipIndex, x, width, y, height, z, depth, data, out asyncGPUReadbackRequest);
			return asyncGPUReadbackRequest;
		}

		// Token: 0x06002D56 RID: 11606 RVA: 0x00055E04 File Offset: 0x00054004
		[NativeMethod("Request")]
		private unsafe static AsyncGPUReadbackRequest Request_Internal_Texture_4([NotNull] Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, GraphicsFormat dstFormat, AsyncRequestNativeArrayData* data)
		{
			if (src == null)
			{
				ThrowHelper.ThrowArgumentNullException(src, "src");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture>(src);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(src, "src");
			}
			AsyncGPUReadbackRequest asyncGPUReadbackRequest;
			AsyncGPUReadback.Request_Internal_Texture_4_Injected(intPtr, mipIndex, x, width, y, height, z, depth, dstFormat, data, out asyncGPUReadbackRequest);
			return asyncGPUReadbackRequest;
		}

		// Token: 0x06002D57 RID: 11607 RVA: 0x00055E4C File Offset: 0x0005404C
		public static Awaitable<AsyncGPUReadbackRequest> RequestAsync(ComputeBuffer src)
		{
			Awaitable<AsyncGPUReadbackRequest> managed = Awaitable<AsyncGPUReadbackRequest>.GetManaged();
			AsyncGPUReadback.Request(src, new Action<AsyncGPUReadbackRequest>(managed.SetResultAndRaiseContinuation));
			return managed;
		}

		// Token: 0x06002D58 RID: 11608 RVA: 0x00055E78 File Offset: 0x00054078
		public static Awaitable<AsyncGPUReadbackRequest> RequestAsync(ComputeBuffer src, int size, int offset)
		{
			Awaitable<AsyncGPUReadbackRequest> managed = Awaitable<AsyncGPUReadbackRequest>.GetManaged();
			AsyncGPUReadback.Request(src, size, offset, new Action<AsyncGPUReadbackRequest>(managed.SetResultAndRaiseContinuation));
			return managed;
		}

		// Token: 0x06002D59 RID: 11609 RVA: 0x00055EA8 File Offset: 0x000540A8
		public static Awaitable<AsyncGPUReadbackRequest> RequestAsync(GraphicsBuffer src)
		{
			Awaitable<AsyncGPUReadbackRequest> managed = Awaitable<AsyncGPUReadbackRequest>.GetManaged();
			AsyncGPUReadback.Request(src, new Action<AsyncGPUReadbackRequest>(managed.SetResultAndRaiseContinuation));
			return managed;
		}

		// Token: 0x06002D5A RID: 11610 RVA: 0x00055ED4 File Offset: 0x000540D4
		public static Awaitable<AsyncGPUReadbackRequest> RequestAsync(GraphicsBuffer src, int size, int offset)
		{
			Awaitable<AsyncGPUReadbackRequest> managed = Awaitable<AsyncGPUReadbackRequest>.GetManaged();
			AsyncGPUReadback.Request(src, size, offset, new Action<AsyncGPUReadbackRequest>(managed.SetResultAndRaiseContinuation));
			return managed;
		}

		// Token: 0x06002D5B RID: 11611 RVA: 0x00055F04 File Offset: 0x00054104
		public static Awaitable<AsyncGPUReadbackRequest> RequestAsync(Texture src, int mipIndex = 0)
		{
			Awaitable<AsyncGPUReadbackRequest> managed = Awaitable<AsyncGPUReadbackRequest>.GetManaged();
			AsyncGPUReadback.Request(src, mipIndex, new Action<AsyncGPUReadbackRequest>(managed.SetResultAndRaiseContinuation));
			return managed;
		}

		// Token: 0x06002D5C RID: 11612 RVA: 0x00055F34 File Offset: 0x00054134
		public static Awaitable<AsyncGPUReadbackRequest> RequestAsync(Texture src, int mipIndex, TextureFormat dstFormat)
		{
			Awaitable<AsyncGPUReadbackRequest> managed = Awaitable<AsyncGPUReadbackRequest>.GetManaged();
			AsyncGPUReadback.Request(src, mipIndex, dstFormat, new Action<AsyncGPUReadbackRequest>(managed.SetResultAndRaiseContinuation));
			return managed;
		}

		// Token: 0x06002D5D RID: 11613 RVA: 0x00055F64 File Offset: 0x00054164
		public static Awaitable<AsyncGPUReadbackRequest> RequestAsync(Texture src, int mipIndex, GraphicsFormat dstFormat)
		{
			Awaitable<AsyncGPUReadbackRequest> managed = Awaitable<AsyncGPUReadbackRequest>.GetManaged();
			AsyncGPUReadback.Request(src, mipIndex, dstFormat, new Action<AsyncGPUReadbackRequest>(managed.SetResultAndRaiseContinuation));
			return managed;
		}

		// Token: 0x06002D5E RID: 11614 RVA: 0x00055F94 File Offset: 0x00054194
		public static Awaitable<AsyncGPUReadbackRequest> RequestAsync(Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth)
		{
			Awaitable<AsyncGPUReadbackRequest> managed = Awaitable<AsyncGPUReadbackRequest>.GetManaged();
			AsyncGPUReadback.Request(src, mipIndex, x, width, y, height, z, depth, new Action<AsyncGPUReadbackRequest>(managed.SetResultAndRaiseContinuation));
			return managed;
		}

		// Token: 0x06002D5F RID: 11615 RVA: 0x00055FCC File Offset: 0x000541CC
		public static Awaitable<AsyncGPUReadbackRequest> RequestAsync(Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, TextureFormat dstFormat)
		{
			Awaitable<AsyncGPUReadbackRequest> managed = Awaitable<AsyncGPUReadbackRequest>.GetManaged();
			AsyncGPUReadback.Request(src, mipIndex, x, width, y, height, z, depth, dstFormat, new Action<AsyncGPUReadbackRequest>(managed.SetResultAndRaiseContinuation));
			return managed;
		}

		// Token: 0x06002D60 RID: 11616 RVA: 0x00056008 File Offset: 0x00054208
		public static Awaitable<AsyncGPUReadbackRequest> RequestAsync(Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, GraphicsFormat dstFormat)
		{
			Awaitable<AsyncGPUReadbackRequest> managed = Awaitable<AsyncGPUReadbackRequest>.GetManaged();
			AsyncGPUReadback.Request(src, mipIndex, x, width, y, height, z, depth, dstFormat, new Action<AsyncGPUReadbackRequest>(managed.SetResultAndRaiseContinuation));
			return managed;
		}

		// Token: 0x06002D61 RID: 11617 RVA: 0x00056044 File Offset: 0x00054244
		public static Awaitable<AsyncGPUReadbackRequest> RequestIntoNativeArrayAsync<T>(ref NativeArray<T> output, ComputeBuffer src) where T : struct
		{
			Awaitable<AsyncGPUReadbackRequest> managed = Awaitable<AsyncGPUReadbackRequest>.GetManaged();
			AsyncGPUReadback.RequestIntoNativeArray<T>(ref output, src, new Action<AsyncGPUReadbackRequest>(managed.SetResultAndRaiseContinuation));
			return managed;
		}

		// Token: 0x06002D62 RID: 11618 RVA: 0x00056074 File Offset: 0x00054274
		public static Awaitable<AsyncGPUReadbackRequest> RequestIntoNativeSliceAsync<T>(ref NativeSlice<T> output, ComputeBuffer src) where T : struct
		{
			Awaitable<AsyncGPUReadbackRequest> managed = Awaitable<AsyncGPUReadbackRequest>.GetManaged();
			AsyncGPUReadback.RequestIntoNativeSlice<T>(ref output, src, new Action<AsyncGPUReadbackRequest>(managed.SetResultAndRaiseContinuation));
			return managed;
		}

		// Token: 0x06002D63 RID: 11619 RVA: 0x000560A4 File Offset: 0x000542A4
		public static Awaitable<AsyncGPUReadbackRequest> RequestIntoNativeArrayAsync<T>(ref NativeArray<T> output, ComputeBuffer src, int size, int offset) where T : struct
		{
			Awaitable<AsyncGPUReadbackRequest> managed = Awaitable<AsyncGPUReadbackRequest>.GetManaged();
			AsyncGPUReadback.RequestIntoNativeArray<T>(ref output, src, size, offset, new Action<AsyncGPUReadbackRequest>(managed.SetResultAndRaiseContinuation));
			return managed;
		}

		// Token: 0x06002D64 RID: 11620 RVA: 0x000560D4 File Offset: 0x000542D4
		public static Awaitable<AsyncGPUReadbackRequest> RequestIntoNativeSliceAsync<T>(ref NativeSlice<T> output, ComputeBuffer src, int size, int offset) where T : struct
		{
			Awaitable<AsyncGPUReadbackRequest> managed = Awaitable<AsyncGPUReadbackRequest>.GetManaged();
			AsyncGPUReadback.RequestIntoNativeSlice<T>(ref output, src, size, offset, new Action<AsyncGPUReadbackRequest>(managed.SetResultAndRaiseContinuation));
			return managed;
		}

		// Token: 0x06002D65 RID: 11621 RVA: 0x00056104 File Offset: 0x00054304
		public static Awaitable<AsyncGPUReadbackRequest> RequestIntoNativeArrayAsync<T>(ref NativeArray<T> output, GraphicsBuffer src) where T : struct
		{
			Awaitable<AsyncGPUReadbackRequest> managed = Awaitable<AsyncGPUReadbackRequest>.GetManaged();
			AsyncGPUReadback.RequestIntoNativeArray<T>(ref output, src, new Action<AsyncGPUReadbackRequest>(managed.SetResultAndRaiseContinuation));
			return managed;
		}

		// Token: 0x06002D66 RID: 11622 RVA: 0x00056134 File Offset: 0x00054334
		public static Awaitable<AsyncGPUReadbackRequest> RequestIntoNativeSliceAsync<T>(ref NativeSlice<T> output, GraphicsBuffer src) where T : struct
		{
			Awaitable<AsyncGPUReadbackRequest> managed = Awaitable<AsyncGPUReadbackRequest>.GetManaged();
			AsyncGPUReadback.RequestIntoNativeSlice<T>(ref output, src, new Action<AsyncGPUReadbackRequest>(managed.SetResultAndRaiseContinuation));
			return managed;
		}

		// Token: 0x06002D67 RID: 11623 RVA: 0x00056164 File Offset: 0x00054364
		public static Awaitable<AsyncGPUReadbackRequest> RequestIntoNativeArrayAsync<T>(ref NativeArray<T> output, GraphicsBuffer src, int size, int offset) where T : struct
		{
			Awaitable<AsyncGPUReadbackRequest> managed = Awaitable<AsyncGPUReadbackRequest>.GetManaged();
			AsyncGPUReadback.RequestIntoNativeArray<T>(ref output, src, size, offset, new Action<AsyncGPUReadbackRequest>(managed.SetResultAndRaiseContinuation));
			return managed;
		}

		// Token: 0x06002D68 RID: 11624 RVA: 0x00056194 File Offset: 0x00054394
		public static Awaitable<AsyncGPUReadbackRequest> RequestIntoNativeSliceAsync<T>(ref NativeSlice<T> output, GraphicsBuffer src, int size, int offset) where T : struct
		{
			Awaitable<AsyncGPUReadbackRequest> managed = Awaitable<AsyncGPUReadbackRequest>.GetManaged();
			AsyncGPUReadback.RequestIntoNativeSlice<T>(ref output, src, size, offset, new Action<AsyncGPUReadbackRequest>(managed.SetResultAndRaiseContinuation));
			return managed;
		}

		// Token: 0x06002D69 RID: 11625 RVA: 0x000561C4 File Offset: 0x000543C4
		public static Awaitable<AsyncGPUReadbackRequest> RequestIntoNativeArrayAsync<T>(ref NativeArray<T> output, Texture src, int mipIndex = 0) where T : struct
		{
			Awaitable<AsyncGPUReadbackRequest> managed = Awaitable<AsyncGPUReadbackRequest>.GetManaged();
			AsyncGPUReadback.RequestIntoNativeArray<T>(ref output, src, mipIndex, new Action<AsyncGPUReadbackRequest>(managed.SetResultAndRaiseContinuation));
			return managed;
		}

		// Token: 0x06002D6A RID: 11626 RVA: 0x000561F4 File Offset: 0x000543F4
		public static Awaitable<AsyncGPUReadbackRequest> RequestIntoNativeSliceAsync<T>(ref NativeSlice<T> output, Texture src, int mipIndex = 0) where T : struct
		{
			Awaitable<AsyncGPUReadbackRequest> managed = Awaitable<AsyncGPUReadbackRequest>.GetManaged();
			AsyncGPUReadback.RequestIntoNativeSlice<T>(ref output, src, mipIndex, new Action<AsyncGPUReadbackRequest>(managed.SetResultAndRaiseContinuation));
			return managed;
		}

		// Token: 0x06002D6B RID: 11627 RVA: 0x00056224 File Offset: 0x00054424
		public static Awaitable<AsyncGPUReadbackRequest> RequestIntoNativeArrayAsync<T>(ref NativeArray<T> output, Texture src, int mipIndex, TextureFormat dstFormat) where T : struct
		{
			Awaitable<AsyncGPUReadbackRequest> managed = Awaitable<AsyncGPUReadbackRequest>.GetManaged();
			AsyncGPUReadback.RequestIntoNativeArray<T>(ref output, src, mipIndex, dstFormat, new Action<AsyncGPUReadbackRequest>(managed.SetResultAndRaiseContinuation));
			return managed;
		}

		// Token: 0x06002D6C RID: 11628 RVA: 0x00056254 File Offset: 0x00054454
		public static Awaitable<AsyncGPUReadbackRequest> RequestIntoNativeSliceAsync<T>(ref NativeSlice<T> output, Texture src, int mipIndex, TextureFormat dstFormat) where T : struct
		{
			Awaitable<AsyncGPUReadbackRequest> managed = Awaitable<AsyncGPUReadbackRequest>.GetManaged();
			AsyncGPUReadback.RequestIntoNativeSlice<T>(ref output, src, mipIndex, dstFormat, new Action<AsyncGPUReadbackRequest>(managed.SetResultAndRaiseContinuation));
			return managed;
		}

		// Token: 0x06002D6D RID: 11629 RVA: 0x00056284 File Offset: 0x00054484
		public static Awaitable<AsyncGPUReadbackRequest> RequestIntoNativeArrayAsync<T>(ref NativeArray<T> output, Texture src, int mipIndex, GraphicsFormat dstFormat) where T : struct
		{
			Awaitable<AsyncGPUReadbackRequest> managed = Awaitable<AsyncGPUReadbackRequest>.GetManaged();
			AsyncGPUReadback.RequestIntoNativeArray<T>(ref output, src, mipIndex, dstFormat, new Action<AsyncGPUReadbackRequest>(managed.SetResultAndRaiseContinuation));
			return managed;
		}

		// Token: 0x06002D6E RID: 11630 RVA: 0x000562B4 File Offset: 0x000544B4
		public static Awaitable<AsyncGPUReadbackRequest> RequestIntoNativeSliceAsync<T>(ref NativeSlice<T> output, Texture src, int mipIndex, GraphicsFormat dstFormat) where T : struct
		{
			Awaitable<AsyncGPUReadbackRequest> managed = Awaitable<AsyncGPUReadbackRequest>.GetManaged();
			AsyncGPUReadback.RequestIntoNativeSlice<T>(ref output, src, mipIndex, dstFormat, new Action<AsyncGPUReadbackRequest>(managed.SetResultAndRaiseContinuation));
			return managed;
		}

		// Token: 0x06002D6F RID: 11631 RVA: 0x000562E4 File Offset: 0x000544E4
		public static Awaitable<AsyncGPUReadbackRequest> RequestIntoNativeArrayAsync<T>(ref NativeArray<T> output, Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, TextureFormat dstFormat) where T : struct
		{
			Awaitable<AsyncGPUReadbackRequest> managed = Awaitable<AsyncGPUReadbackRequest>.GetManaged();
			AsyncGPUReadback.RequestIntoNativeArray<T>(ref output, src, mipIndex, x, width, y, height, z, depth, dstFormat, new Action<AsyncGPUReadbackRequest>(managed.SetResultAndRaiseContinuation));
			return managed;
		}

		// Token: 0x06002D70 RID: 11632 RVA: 0x00056320 File Offset: 0x00054520
		public static Awaitable<AsyncGPUReadbackRequest> RequestIntoNativeSliceAsync<T>(ref NativeSlice<T> output, Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, TextureFormat dstFormat) where T : struct
		{
			Awaitable<AsyncGPUReadbackRequest> managed = Awaitable<AsyncGPUReadbackRequest>.GetManaged();
			AsyncGPUReadback.RequestIntoNativeSlice<T>(ref output, src, mipIndex, x, width, y, height, z, depth, dstFormat, new Action<AsyncGPUReadbackRequest>(managed.SetResultAndRaiseContinuation));
			return managed;
		}

		// Token: 0x06002D71 RID: 11633 RVA: 0x0005635C File Offset: 0x0005455C
		public static Awaitable<AsyncGPUReadbackRequest> RequestIntoNativeArrayAsync<T>(ref NativeArray<T> output, Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, GraphicsFormat dstFormat) where T : struct
		{
			Awaitable<AsyncGPUReadbackRequest> managed = Awaitable<AsyncGPUReadbackRequest>.GetManaged();
			AsyncGPUReadback.RequestIntoNativeArray<T>(ref output, src, mipIndex, x, width, y, height, z, depth, dstFormat, new Action<AsyncGPUReadbackRequest>(managed.SetResultAndRaiseContinuation));
			return managed;
		}

		// Token: 0x06002D72 RID: 11634 RVA: 0x00056398 File Offset: 0x00054598
		public static Awaitable<AsyncGPUReadbackRequest> RequestIntoNativeSliceAsync<T>(ref NativeSlice<T> output, Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, GraphicsFormat dstFormat) where T : struct
		{
			Awaitable<AsyncGPUReadbackRequest> managed = Awaitable<AsyncGPUReadbackRequest>.GetManaged();
			AsyncGPUReadback.RequestIntoNativeSlice<T>(ref output, src, mipIndex, x, width, y, height, z, depth, dstFormat, new Action<AsyncGPUReadbackRequest>(managed.SetResultAndRaiseContinuation));
			return managed;
		}

		// Token: 0x06002D73 RID: 11635
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Request_Internal_ComputeBuffer_1_Injected(IntPtr buffer, AsyncRequestNativeArrayData* data, out AsyncGPUReadbackRequest ret);

		// Token: 0x06002D74 RID: 11636
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Request_Internal_ComputeBuffer_2_Injected(IntPtr src, int size, int offset, AsyncRequestNativeArrayData* data, out AsyncGPUReadbackRequest ret);

		// Token: 0x06002D75 RID: 11637
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Request_Internal_GraphicsBuffer_1_Injected(IntPtr buffer, AsyncRequestNativeArrayData* data, out AsyncGPUReadbackRequest ret);

		// Token: 0x06002D76 RID: 11638
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Request_Internal_GraphicsBuffer_2_Injected(IntPtr src, int size, int offset, AsyncRequestNativeArrayData* data, out AsyncGPUReadbackRequest ret);

		// Token: 0x06002D77 RID: 11639
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Request_Internal_Texture_1_Injected(IntPtr src, int mipIndex, AsyncRequestNativeArrayData* data, out AsyncGPUReadbackRequest ret);

		// Token: 0x06002D78 RID: 11640
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Request_Internal_Texture_2_Injected(IntPtr src, int mipIndex, GraphicsFormat dstFormat, AsyncRequestNativeArrayData* data, out AsyncGPUReadbackRequest ret);

		// Token: 0x06002D79 RID: 11641
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Request_Internal_Texture_3_Injected(IntPtr src, int mipIndex, int x, int width, int y, int height, int z, int depth, AsyncRequestNativeArrayData* data, out AsyncGPUReadbackRequest ret);

		// Token: 0x06002D7A RID: 11642
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Request_Internal_Texture_4_Injected(IntPtr src, int mipIndex, int x, int width, int y, int height, int z, int depth, GraphicsFormat dstFormat, AsyncRequestNativeArrayData* data, out AsyncGPUReadbackRequest ret);
	}
}
