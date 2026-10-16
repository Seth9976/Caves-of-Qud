using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Profiling;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Internal;
using UnityEngine.Profiling;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x020004EC RID: 1260
	[NativeHeader("Runtime/Export/Graphics/RenderingCommandBuffer.bindings.h")]
	[NativeHeader("Runtime/Shaders/RayTracing/RayTracingShader.h")]
	[UsedByNativeCode]
	[NativeHeader("Runtime/Shaders/ComputeShader.h")]
	[NativeType("Runtime/Graphics/CommandBuffer/RenderingCommandBuffer.h")]
	public class CommandBuffer : IDisposable
	{
		// Token: 0x06002EFF RID: 12031 RVA: 0x0005949F File Offset: 0x0005769F
		public void ConvertTexture(RenderTargetIdentifier src, RenderTargetIdentifier dst)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.ConvertTexture_Internal(src, 0, dst, 0);
		}

		// Token: 0x06002F00 RID: 12032 RVA: 0x000594B6 File Offset: 0x000576B6
		public void ConvertTexture(RenderTargetIdentifier src, int srcElement, RenderTargetIdentifier dst, int dstElement)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.ConvertTexture_Internal(src, srcElement, dst, dstElement);
		}

		// Token: 0x06002F01 RID: 12033 RVA: 0x000594D0 File Offset: 0x000576D0
		[NativeMethod("AddWaitAllAsyncReadbackRequests")]
		public void WaitAllAsyncReadbackRequests()
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.WaitAllAsyncReadbackRequests_Injected(intPtr);
		}

		// Token: 0x06002F02 RID: 12034 RVA: 0x000594F2 File Offset: 0x000576F2
		public void RequestAsyncReadback(ComputeBuffer src, Action<AsyncGPUReadbackRequest> callback)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Internal_RequestAsyncReadback_1(src, callback, null);
		}

		// Token: 0x06002F03 RID: 12035 RVA: 0x0005950B File Offset: 0x0005770B
		public void RequestAsyncReadback(GraphicsBuffer src, Action<AsyncGPUReadbackRequest> callback)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Internal_RequestAsyncReadback_8(src, callback, null);
		}

		// Token: 0x06002F04 RID: 12036 RVA: 0x00059524 File Offset: 0x00057724
		public void RequestAsyncReadback(ComputeBuffer src, int size, int offset, Action<AsyncGPUReadbackRequest> callback)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Internal_RequestAsyncReadback_2(src, size, offset, callback, null);
		}

		// Token: 0x06002F05 RID: 12037 RVA: 0x00059540 File Offset: 0x00057740
		public void RequestAsyncReadback(GraphicsBuffer src, int size, int offset, Action<AsyncGPUReadbackRequest> callback)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Internal_RequestAsyncReadback_9(src, size, offset, callback, null);
		}

		// Token: 0x06002F06 RID: 12038 RVA: 0x0005955C File Offset: 0x0005775C
		public void RequestAsyncReadback(Texture src, Action<AsyncGPUReadbackRequest> callback)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Internal_RequestAsyncReadback_3(src, callback, null);
		}

		// Token: 0x06002F07 RID: 12039 RVA: 0x00059575 File Offset: 0x00057775
		public void RequestAsyncReadback(Texture src, int mipIndex, Action<AsyncGPUReadbackRequest> callback)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Internal_RequestAsyncReadback_4(src, mipIndex, callback, null);
		}

		// Token: 0x06002F08 RID: 12040 RVA: 0x0005958F File Offset: 0x0005778F
		public void RequestAsyncReadback(Texture src, int mipIndex, TextureFormat dstFormat, Action<AsyncGPUReadbackRequest> callback)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Internal_RequestAsyncReadback_5(src, mipIndex, GraphicsFormatUtility.GetGraphicsFormat(dstFormat, QualitySettings.activeColorSpace == ColorSpace.Linear), callback, null);
		}

		// Token: 0x06002F09 RID: 12041 RVA: 0x000595B8 File Offset: 0x000577B8
		public void RequestAsyncReadback(Texture src, int mipIndex, GraphicsFormat dstFormat, Action<AsyncGPUReadbackRequest> callback)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Internal_RequestAsyncReadback_5(src, mipIndex, dstFormat, callback, null);
		}

		// Token: 0x06002F0A RID: 12042 RVA: 0x000595D4 File Offset: 0x000577D4
		public void RequestAsyncReadback(Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, Action<AsyncGPUReadbackRequest> callback)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Internal_RequestAsyncReadback_6(src, mipIndex, x, width, y, height, z, depth, callback, null);
		}

		// Token: 0x06002F0B RID: 12043 RVA: 0x00059608 File Offset: 0x00057808
		public void RequestAsyncReadback(Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, TextureFormat dstFormat, Action<AsyncGPUReadbackRequest> callback)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Internal_RequestAsyncReadback_7(src, mipIndex, x, width, y, height, z, depth, GraphicsFormatUtility.GetGraphicsFormat(dstFormat, QualitySettings.activeColorSpace == ColorSpace.Linear), callback, null);
		}

		// Token: 0x06002F0C RID: 12044 RVA: 0x00059648 File Offset: 0x00057848
		public void RequestAsyncReadback(Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, GraphicsFormat dstFormat, Action<AsyncGPUReadbackRequest> callback)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Internal_RequestAsyncReadback_7(src, mipIndex, x, width, y, height, z, depth, dstFormat, callback, null);
		}

		// Token: 0x06002F0D RID: 12045 RVA: 0x0005967C File Offset: 0x0005787C
		public unsafe void RequestAsyncReadbackIntoNativeArray<T>(ref NativeArray<T> output, ComputeBuffer src, Action<AsyncGPUReadbackRequest> callback) where T : struct
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			this.Internal_RequestAsyncReadback_1(src, callback, &asyncRequestNativeArrayData);
		}

		// Token: 0x06002F0E RID: 12046 RVA: 0x000596B0 File Offset: 0x000578B0
		public unsafe void RequestAsyncReadbackIntoNativeArray<T>(ref NativeArray<T> output, ComputeBuffer src, int size, int offset, Action<AsyncGPUReadbackRequest> callback) where T : struct
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			this.Internal_RequestAsyncReadback_2(src, size, offset, callback, &asyncRequestNativeArrayData);
		}

		// Token: 0x06002F0F RID: 12047 RVA: 0x000596E8 File Offset: 0x000578E8
		public unsafe void RequestAsyncReadbackIntoNativeArray<T>(ref NativeArray<T> output, GraphicsBuffer src, Action<AsyncGPUReadbackRequest> callback) where T : struct
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			this.Internal_RequestAsyncReadback_8(src, callback, &asyncRequestNativeArrayData);
		}

		// Token: 0x06002F10 RID: 12048 RVA: 0x0005971C File Offset: 0x0005791C
		public unsafe void RequestAsyncReadbackIntoNativeArray<T>(ref NativeArray<T> output, GraphicsBuffer src, int size, int offset, Action<AsyncGPUReadbackRequest> callback) where T : struct
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			this.Internal_RequestAsyncReadback_9(src, size, offset, callback, &asyncRequestNativeArrayData);
		}

		// Token: 0x06002F11 RID: 12049 RVA: 0x00059754 File Offset: 0x00057954
		public unsafe void RequestAsyncReadbackIntoNativeArray<T>(ref NativeArray<T> output, Texture src, Action<AsyncGPUReadbackRequest> callback) where T : struct
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			this.Internal_RequestAsyncReadback_3(src, callback, &asyncRequestNativeArrayData);
		}

		// Token: 0x06002F12 RID: 12050 RVA: 0x00059788 File Offset: 0x00057988
		public unsafe void RequestAsyncReadbackIntoNativeArray<T>(ref NativeArray<T> output, Texture src, int mipIndex, Action<AsyncGPUReadbackRequest> callback) where T : struct
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			this.Internal_RequestAsyncReadback_4(src, mipIndex, callback, &asyncRequestNativeArrayData);
		}

		// Token: 0x06002F13 RID: 12051 RVA: 0x000597BC File Offset: 0x000579BC
		public unsafe void RequestAsyncReadbackIntoNativeArray<T>(ref NativeArray<T> output, Texture src, int mipIndex, TextureFormat dstFormat, Action<AsyncGPUReadbackRequest> callback) where T : struct
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			this.Internal_RequestAsyncReadback_5(src, mipIndex, GraphicsFormatUtility.GetGraphicsFormat(dstFormat, QualitySettings.activeColorSpace == ColorSpace.Linear), callback, &asyncRequestNativeArrayData);
		}

		// Token: 0x06002F14 RID: 12052 RVA: 0x00059800 File Offset: 0x00057A00
		public unsafe void RequestAsyncReadbackIntoNativeArray<T>(ref NativeArray<T> output, Texture src, int mipIndex, GraphicsFormat dstFormat, Action<AsyncGPUReadbackRequest> callback) where T : struct
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			this.Internal_RequestAsyncReadback_5(src, mipIndex, dstFormat, callback, &asyncRequestNativeArrayData);
		}

		// Token: 0x06002F15 RID: 12053 RVA: 0x00059838 File Offset: 0x00057A38
		public unsafe void RequestAsyncReadbackIntoNativeArray<T>(ref NativeArray<T> output, Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, Action<AsyncGPUReadbackRequest> callback) where T : struct
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			this.Internal_RequestAsyncReadback_6(src, mipIndex, x, width, y, height, z, depth, callback, &asyncRequestNativeArrayData);
		}

		// Token: 0x06002F16 RID: 12054 RVA: 0x00059878 File Offset: 0x00057A78
		public unsafe void RequestAsyncReadbackIntoNativeArray<T>(ref NativeArray<T> output, Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, TextureFormat dstFormat, Action<AsyncGPUReadbackRequest> callback) where T : struct
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			this.Internal_RequestAsyncReadback_7(src, mipIndex, x, width, y, height, z, depth, GraphicsFormatUtility.GetGraphicsFormat(dstFormat, QualitySettings.activeColorSpace == ColorSpace.Linear), callback, &asyncRequestNativeArrayData);
		}

		// Token: 0x06002F17 RID: 12055 RVA: 0x000598C8 File Offset: 0x00057AC8
		public unsafe void RequestAsyncReadbackIntoNativeArray<T>(ref NativeArray<T> output, Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, GraphicsFormat dstFormat, Action<AsyncGPUReadbackRequest> callback) where T : struct
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			this.Internal_RequestAsyncReadback_7(src, mipIndex, x, width, y, height, z, depth, dstFormat, callback, &asyncRequestNativeArrayData);
		}

		// Token: 0x06002F18 RID: 12056 RVA: 0x0005990C File Offset: 0x00057B0C
		public unsafe void RequestAsyncReadbackIntoNativeSlice<T>(ref NativeSlice<T> output, ComputeBuffer src, Action<AsyncGPUReadbackRequest> callback) where T : struct
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			this.Internal_RequestAsyncReadback_1(src, callback, &asyncRequestNativeArrayData);
		}

		// Token: 0x06002F19 RID: 12057 RVA: 0x00059940 File Offset: 0x00057B40
		public unsafe void RequestAsyncReadbackIntoNativeSlice<T>(ref NativeSlice<T> output, ComputeBuffer src, int size, int offset, Action<AsyncGPUReadbackRequest> callback) where T : struct
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			this.Internal_RequestAsyncReadback_2(src, size, offset, callback, &asyncRequestNativeArrayData);
		}

		// Token: 0x06002F1A RID: 12058 RVA: 0x00059978 File Offset: 0x00057B78
		public unsafe void RequestAsyncReadbackIntoNativeSlice<T>(ref NativeSlice<T> output, GraphicsBuffer src, Action<AsyncGPUReadbackRequest> callback) where T : struct
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			this.Internal_RequestAsyncReadback_8(src, callback, &asyncRequestNativeArrayData);
		}

		// Token: 0x06002F1B RID: 12059 RVA: 0x000599AC File Offset: 0x00057BAC
		public unsafe void RequestAsyncReadbackIntoNativeSlice<T>(ref NativeSlice<T> output, GraphicsBuffer src, int size, int offset, Action<AsyncGPUReadbackRequest> callback) where T : struct
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			this.Internal_RequestAsyncReadback_9(src, size, offset, callback, &asyncRequestNativeArrayData);
		}

		// Token: 0x06002F1C RID: 12060 RVA: 0x000599E4 File Offset: 0x00057BE4
		public unsafe void RequestAsyncReadbackIntoNativeSlice<T>(ref NativeSlice<T> output, Texture src, Action<AsyncGPUReadbackRequest> callback) where T : struct
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			this.Internal_RequestAsyncReadback_3(src, callback, &asyncRequestNativeArrayData);
		}

		// Token: 0x06002F1D RID: 12061 RVA: 0x00059A18 File Offset: 0x00057C18
		public unsafe void RequestAsyncReadbackIntoNativeSlice<T>(ref NativeSlice<T> output, Texture src, int mipIndex, Action<AsyncGPUReadbackRequest> callback) where T : struct
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			this.Internal_RequestAsyncReadback_4(src, mipIndex, callback, &asyncRequestNativeArrayData);
		}

		// Token: 0x06002F1E RID: 12062 RVA: 0x00059A4C File Offset: 0x00057C4C
		public unsafe void RequestAsyncReadbackIntoNativeSlice<T>(ref NativeSlice<T> output, Texture src, int mipIndex, TextureFormat dstFormat, Action<AsyncGPUReadbackRequest> callback) where T : struct
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			this.Internal_RequestAsyncReadback_5(src, mipIndex, GraphicsFormatUtility.GetGraphicsFormat(dstFormat, QualitySettings.activeColorSpace == ColorSpace.Linear), callback, &asyncRequestNativeArrayData);
		}

		// Token: 0x06002F1F RID: 12063 RVA: 0x00059A90 File Offset: 0x00057C90
		public unsafe void RequestAsyncReadbackIntoNativeSlice<T>(ref NativeSlice<T> output, Texture src, int mipIndex, GraphicsFormat dstFormat, Action<AsyncGPUReadbackRequest> callback) where T : struct
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			this.Internal_RequestAsyncReadback_5(src, mipIndex, dstFormat, callback, &asyncRequestNativeArrayData);
		}

		// Token: 0x06002F20 RID: 12064 RVA: 0x00059AC8 File Offset: 0x00057CC8
		public unsafe void RequestAsyncReadbackIntoNativeSlice<T>(ref NativeSlice<T> output, Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, Action<AsyncGPUReadbackRequest> callback) where T : struct
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			this.Internal_RequestAsyncReadback_6(src, mipIndex, x, width, y, height, z, depth, callback, &asyncRequestNativeArrayData);
		}

		// Token: 0x06002F21 RID: 12065 RVA: 0x00059B08 File Offset: 0x00057D08
		public unsafe void RequestAsyncReadbackIntoNativeSlice<T>(ref NativeSlice<T> output, Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, TextureFormat dstFormat, Action<AsyncGPUReadbackRequest> callback) where T : struct
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			this.Internal_RequestAsyncReadback_7(src, mipIndex, x, width, y, height, z, depth, GraphicsFormatUtility.GetGraphicsFormat(dstFormat, QualitySettings.activeColorSpace == ColorSpace.Linear), callback, &asyncRequestNativeArrayData);
		}

		// Token: 0x06002F22 RID: 12066 RVA: 0x00059B58 File Offset: 0x00057D58
		public unsafe void RequestAsyncReadbackIntoNativeSlice<T>(ref NativeSlice<T> output, Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, GraphicsFormat dstFormat, Action<AsyncGPUReadbackRequest> callback) where T : struct
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			this.Internal_RequestAsyncReadback_7(src, mipIndex, x, width, y, height, z, depth, dstFormat, callback, &asyncRequestNativeArrayData);
		}

		// Token: 0x06002F23 RID: 12067 RVA: 0x00059B9C File Offset: 0x00057D9C
		[NativeMethod("AddRequestAsyncReadback")]
		private unsafe void Internal_RequestAsyncReadback_1([NotNull] ComputeBuffer src, [NotNull] Action<AsyncGPUReadbackRequest> callback, AsyncRequestNativeArrayData* nativeArrayData = null)
		{
			if (src == null)
			{
				ThrowHelper.ThrowArgumentNullException(src, "src");
			}
			if (callback == null)
			{
				ThrowHelper.ThrowArgumentNullException(callback, "callback");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = ComputeBuffer.BindingsMarshaller.ConvertToNative(src);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(src, "src");
			}
			CommandBuffer.Internal_RequestAsyncReadback_1_Injected(intPtr, intPtr2, callback, nativeArrayData);
		}

		// Token: 0x06002F24 RID: 12068 RVA: 0x00059BF4 File Offset: 0x00057DF4
		[NativeMethod("AddRequestAsyncReadback")]
		private unsafe void Internal_RequestAsyncReadback_2([NotNull] ComputeBuffer src, int size, int offset, [NotNull] Action<AsyncGPUReadbackRequest> callback, AsyncRequestNativeArrayData* nativeArrayData = null)
		{
			if (src == null)
			{
				ThrowHelper.ThrowArgumentNullException(src, "src");
			}
			if (callback == null)
			{
				ThrowHelper.ThrowArgumentNullException(callback, "callback");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = ComputeBuffer.BindingsMarshaller.ConvertToNative(src);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(src, "src");
			}
			CommandBuffer.Internal_RequestAsyncReadback_2_Injected(intPtr, intPtr2, size, offset, callback, nativeArrayData);
		}

		// Token: 0x06002F25 RID: 12069 RVA: 0x00059C54 File Offset: 0x00057E54
		[NativeMethod("AddRequestAsyncReadback")]
		private unsafe void Internal_RequestAsyncReadback_3([NotNull] Texture src, [NotNull] Action<AsyncGPUReadbackRequest> callback, AsyncRequestNativeArrayData* nativeArrayData = null)
		{
			if (src == null)
			{
				ThrowHelper.ThrowArgumentNullException(src, "src");
			}
			if (callback == null)
			{
				ThrowHelper.ThrowArgumentNullException(callback, "callback");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Texture>(src);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(src, "src");
			}
			CommandBuffer.Internal_RequestAsyncReadback_3_Injected(intPtr, intPtr2, callback, nativeArrayData);
		}

		// Token: 0x06002F26 RID: 12070 RVA: 0x00059CAC File Offset: 0x00057EAC
		[NativeMethod("AddRequestAsyncReadback")]
		private unsafe void Internal_RequestAsyncReadback_4([NotNull] Texture src, int mipIndex, [NotNull] Action<AsyncGPUReadbackRequest> callback, AsyncRequestNativeArrayData* nativeArrayData = null)
		{
			if (src == null)
			{
				ThrowHelper.ThrowArgumentNullException(src, "src");
			}
			if (callback == null)
			{
				ThrowHelper.ThrowArgumentNullException(callback, "callback");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Texture>(src);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(src, "src");
			}
			CommandBuffer.Internal_RequestAsyncReadback_4_Injected(intPtr, intPtr2, mipIndex, callback, nativeArrayData);
		}

		// Token: 0x06002F27 RID: 12071 RVA: 0x00059D08 File Offset: 0x00057F08
		[NativeMethod("AddRequestAsyncReadback")]
		private unsafe void Internal_RequestAsyncReadback_5([NotNull] Texture src, int mipIndex, GraphicsFormat dstFormat, [NotNull] Action<AsyncGPUReadbackRequest> callback, AsyncRequestNativeArrayData* nativeArrayData = null)
		{
			if (src == null)
			{
				ThrowHelper.ThrowArgumentNullException(src, "src");
			}
			if (callback == null)
			{
				ThrowHelper.ThrowArgumentNullException(callback, "callback");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Texture>(src);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(src, "src");
			}
			CommandBuffer.Internal_RequestAsyncReadback_5_Injected(intPtr, intPtr2, mipIndex, dstFormat, callback, nativeArrayData);
		}

		// Token: 0x06002F28 RID: 12072 RVA: 0x00059D68 File Offset: 0x00057F68
		[NativeMethod("AddRequestAsyncReadback")]
		private unsafe void Internal_RequestAsyncReadback_6([NotNull] Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, [NotNull] Action<AsyncGPUReadbackRequest> callback, AsyncRequestNativeArrayData* nativeArrayData = null)
		{
			if (src == null)
			{
				ThrowHelper.ThrowArgumentNullException(src, "src");
			}
			if (callback == null)
			{
				ThrowHelper.ThrowArgumentNullException(callback, "callback");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Texture>(src);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(src, "src");
			}
			CommandBuffer.Internal_RequestAsyncReadback_6_Injected(intPtr, intPtr2, mipIndex, x, width, y, height, z, depth, callback, nativeArrayData);
		}

		// Token: 0x06002F29 RID: 12073 RVA: 0x00059DD0 File Offset: 0x00057FD0
		[NativeMethod("AddRequestAsyncReadback")]
		private unsafe void Internal_RequestAsyncReadback_7([NotNull] Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, GraphicsFormat dstFormat, [NotNull] Action<AsyncGPUReadbackRequest> callback, AsyncRequestNativeArrayData* nativeArrayData = null)
		{
			if (src == null)
			{
				ThrowHelper.ThrowArgumentNullException(src, "src");
			}
			if (callback == null)
			{
				ThrowHelper.ThrowArgumentNullException(callback, "callback");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Texture>(src);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(src, "src");
			}
			CommandBuffer.Internal_RequestAsyncReadback_7_Injected(intPtr, intPtr2, mipIndex, x, width, y, height, z, depth, dstFormat, callback, nativeArrayData);
		}

		// Token: 0x06002F2A RID: 12074 RVA: 0x00059E3C File Offset: 0x0005803C
		[NativeMethod("AddRequestAsyncReadback")]
		private unsafe void Internal_RequestAsyncReadback_8([NotNull] GraphicsBuffer src, [NotNull] Action<AsyncGPUReadbackRequest> callback, AsyncRequestNativeArrayData* nativeArrayData = null)
		{
			if (src == null)
			{
				ThrowHelper.ThrowArgumentNullException(src, "src");
			}
			if (callback == null)
			{
				ThrowHelper.ThrowArgumentNullException(callback, "callback");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = GraphicsBuffer.BindingsMarshaller.ConvertToNative(src);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(src, "src");
			}
			CommandBuffer.Internal_RequestAsyncReadback_8_Injected(intPtr, intPtr2, callback, nativeArrayData);
		}

		// Token: 0x06002F2B RID: 12075 RVA: 0x00059E94 File Offset: 0x00058094
		[NativeMethod("AddRequestAsyncReadback")]
		private unsafe void Internal_RequestAsyncReadback_9([NotNull] GraphicsBuffer src, int size, int offset, [NotNull] Action<AsyncGPUReadbackRequest> callback, AsyncRequestNativeArrayData* nativeArrayData = null)
		{
			if (src == null)
			{
				ThrowHelper.ThrowArgumentNullException(src, "src");
			}
			if (callback == null)
			{
				ThrowHelper.ThrowArgumentNullException(callback, "callback");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = GraphicsBuffer.BindingsMarshaller.ConvertToNative(src);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(src, "src");
			}
			CommandBuffer.Internal_RequestAsyncReadback_9_Injected(intPtr, intPtr2, size, offset, callback, nativeArrayData);
		}

		// Token: 0x06002F2C RID: 12076 RVA: 0x00059EF4 File Offset: 0x000580F4
		[NativeMethod("AddSetInvertCulling")]
		public void SetInvertCulling(bool invertCulling)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetInvertCulling_Injected(intPtr, invertCulling);
		}

		// Token: 0x06002F2D RID: 12077 RVA: 0x00059F18 File Offset: 0x00058118
		private void ConvertTexture_Internal(RenderTargetIdentifier src, int srcElement, RenderTargetIdentifier dst, int dstElement)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.ConvertTexture_Internal_Injected(intPtr, ref src, srcElement, ref dst, dstElement);
		}

		// Token: 0x06002F2E RID: 12078 RVA: 0x00059F44 File Offset: 0x00058144
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetSinglePassStereo", HasExplicitThis = true)]
		private void Internal_SetSinglePassStereo(SinglePassStereoMode mode)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.Internal_SetSinglePassStereo_Injected(intPtr, mode);
		}

		// Token: 0x06002F2F RID: 12079
		[FreeFunction("RenderingCommandBuffer_Bindings::InitBuffer")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr InitBuffer();

		// Token: 0x06002F30 RID: 12080 RVA: 0x00059F68 File Offset: 0x00058168
		[FreeFunction("RenderingCommandBuffer_Bindings::CreateGPUFence_Internal", HasExplicitThis = true)]
		private IntPtr CreateGPUFence_Internal(GraphicsFenceType fenceType, SynchronisationStageFlags stage)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return CommandBuffer.CreateGPUFence_Internal_Injected(intPtr, fenceType, stage);
		}

		// Token: 0x06002F31 RID: 12081 RVA: 0x00059F8C File Offset: 0x0005818C
		[FreeFunction("RenderingCommandBuffer_Bindings::WaitOnGPUFence_Internal", HasExplicitThis = true)]
		private void WaitOnGPUFence_Internal(IntPtr fencePtr, SynchronisationStageFlags stage)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.WaitOnGPUFence_Internal_Injected(intPtr, fencePtr, stage);
		}

		// Token: 0x06002F32 RID: 12082 RVA: 0x00059FB0 File Offset: 0x000581B0
		[FreeFunction("RenderingCommandBuffer_Bindings::ReleaseBuffer", HasExplicitThis = true, IsThreadSafe = true)]
		private void ReleaseBuffer()
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.ReleaseBuffer_Injected(intPtr);
		}

		// Token: 0x06002F33 RID: 12083 RVA: 0x00059FD4 File Offset: 0x000581D4
		[FreeFunction("RenderingCommandBuffer_Bindings::SetComputeFloatParam", HasExplicitThis = true)]
		public void SetComputeFloatParam([NotNull] ComputeShader computeShader, int nameID, float val)
		{
			if (computeShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(computeShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			CommandBuffer.SetComputeFloatParam_Injected(intPtr, intPtr2, nameID, val);
		}

		// Token: 0x06002F34 RID: 12084 RVA: 0x0005A01C File Offset: 0x0005821C
		[FreeFunction("RenderingCommandBuffer_Bindings::SetComputeIntParam", HasExplicitThis = true)]
		public void SetComputeIntParam([NotNull] ComputeShader computeShader, int nameID, int val)
		{
			if (computeShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(computeShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			CommandBuffer.SetComputeIntParam_Injected(intPtr, intPtr2, nameID, val);
		}

		// Token: 0x06002F35 RID: 12085 RVA: 0x0005A064 File Offset: 0x00058264
		[FreeFunction("RenderingCommandBuffer_Bindings::SetComputeVectorParam", HasExplicitThis = true)]
		public void SetComputeVectorParam([NotNull] ComputeShader computeShader, int nameID, Vector4 val)
		{
			if (computeShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(computeShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			CommandBuffer.SetComputeVectorParam_Injected(intPtr, intPtr2, nameID, ref val);
		}

		// Token: 0x06002F36 RID: 12086 RVA: 0x0005A0B0 File Offset: 0x000582B0
		[FreeFunction("RenderingCommandBuffer_Bindings::SetComputeVectorArrayParam", HasExplicitThis = true)]
		public unsafe void SetComputeVectorArrayParam([NotNull] ComputeShader computeShader, int nameID, Vector4[] values)
		{
			if (computeShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(computeShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			Span<Vector4> span = new Span<Vector4>(values);
			fixed (Vector4* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				CommandBuffer.SetComputeVectorArrayParam_Injected(intPtr, intPtr2, nameID, ref managedSpanWrapper);
			}
		}

		// Token: 0x06002F37 RID: 12087 RVA: 0x0005A120 File Offset: 0x00058320
		[FreeFunction("RenderingCommandBuffer_Bindings::SetComputeMatrixParam", HasExplicitThis = true)]
		public void SetComputeMatrixParam([NotNull] ComputeShader computeShader, int nameID, Matrix4x4 val)
		{
			if (computeShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(computeShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			CommandBuffer.SetComputeMatrixParam_Injected(intPtr, intPtr2, nameID, ref val);
		}

		// Token: 0x06002F38 RID: 12088 RVA: 0x0005A16C File Offset: 0x0005836C
		[FreeFunction("RenderingCommandBuffer_Bindings::SetComputeMatrixArrayParam", HasExplicitThis = true)]
		public unsafe void SetComputeMatrixArrayParam([NotNull] ComputeShader computeShader, int nameID, Matrix4x4[] values)
		{
			if (computeShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(computeShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			Span<Matrix4x4> span = new Span<Matrix4x4>(values);
			fixed (Matrix4x4* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				CommandBuffer.SetComputeMatrixArrayParam_Injected(intPtr, intPtr2, nameID, ref managedSpanWrapper);
			}
		}

		// Token: 0x06002F39 RID: 12089 RVA: 0x0005A1DC File Offset: 0x000583DC
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetComputeFloats", HasExplicitThis = true)]
		private unsafe void Internal_SetComputeFloats([NotNull] ComputeShader computeShader, int nameID, float[] values)
		{
			if (computeShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(computeShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			Span<float> span = new Span<float>(values);
			fixed (float* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				CommandBuffer.Internal_SetComputeFloats_Injected(intPtr, intPtr2, nameID, ref managedSpanWrapper);
			}
		}

		// Token: 0x06002F3A RID: 12090 RVA: 0x0005A24C File Offset: 0x0005844C
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetComputeInts", HasExplicitThis = true)]
		private unsafe void Internal_SetComputeInts([NotNull] ComputeShader computeShader, int nameID, int[] values)
		{
			if (computeShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(computeShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			Span<int> span = new Span<int>(values);
			fixed (int* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				CommandBuffer.Internal_SetComputeInts_Injected(intPtr, intPtr2, nameID, ref managedSpanWrapper);
			}
		}

		// Token: 0x06002F3B RID: 12091 RVA: 0x0005A2BC File Offset: 0x000584BC
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetComputeTextureParam", HasExplicitThis = true)]
		private void Internal_SetComputeTextureParam([NotNull] ComputeShader computeShader, int kernelIndex, int nameID, ref RenderTargetIdentifier rt, int mipLevel, RenderTextureSubElement element)
		{
			if (computeShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(computeShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			CommandBuffer.Internal_SetComputeTextureParam_Injected(intPtr, intPtr2, kernelIndex, nameID, ref rt, mipLevel, element);
		}

		// Token: 0x06002F3C RID: 12092 RVA: 0x0005A30C File Offset: 0x0005850C
		[FreeFunction("RenderingCommandBuffer_Bindings::SetComputeBufferParam", HasExplicitThis = true)]
		private void Internal_SetComputeBufferParam([NotNull] ComputeShader computeShader, int kernelIndex, int nameID, ComputeBuffer buffer)
		{
			if (computeShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(computeShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			CommandBuffer.Internal_SetComputeBufferParam_Injected(intPtr, intPtr2, kernelIndex, nameID, (buffer == null) ? ((IntPtr)0) : ComputeBuffer.BindingsMarshaller.ConvertToNative(buffer));
		}

		// Token: 0x06002F3D RID: 12093 RVA: 0x0005A368 File Offset: 0x00058568
		[FreeFunction("RenderingCommandBuffer_Bindings::SetComputeBufferParam", HasExplicitThis = true)]
		private void Internal_SetComputeGraphicsBufferHandleParam([NotNull] ComputeShader computeShader, int kernelIndex, int nameID, GraphicsBufferHandle bufferHandle)
		{
			if (computeShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(computeShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			CommandBuffer.Internal_SetComputeGraphicsBufferHandleParam_Injected(intPtr, intPtr2, kernelIndex, nameID, ref bufferHandle);
		}

		// Token: 0x06002F3E RID: 12094 RVA: 0x0005A3B4 File Offset: 0x000585B4
		[FreeFunction("RenderingCommandBuffer_Bindings::SetComputeBufferParam", HasExplicitThis = true)]
		private void Internal_SetComputeGraphicsBufferParam([NotNull] ComputeShader computeShader, int kernelIndex, int nameID, GraphicsBuffer buffer)
		{
			if (computeShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(computeShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			CommandBuffer.Internal_SetComputeGraphicsBufferParam_Injected(intPtr, intPtr2, kernelIndex, nameID, (buffer == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(buffer));
		}

		// Token: 0x06002F3F RID: 12095 RVA: 0x0005A410 File Offset: 0x00058610
		[FreeFunction("RenderingCommandBuffer_Bindings::SetComputeConstantBufferParam", HasExplicitThis = true)]
		private void Internal_SetComputeConstantComputeBufferParam([NotNull] ComputeShader computeShader, int nameID, ComputeBuffer buffer, int offset, int size)
		{
			if (computeShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(computeShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			CommandBuffer.Internal_SetComputeConstantComputeBufferParam_Injected(intPtr, intPtr2, nameID, (buffer == null) ? ((IntPtr)0) : ComputeBuffer.BindingsMarshaller.ConvertToNative(buffer), offset, size);
		}

		// Token: 0x06002F40 RID: 12096 RVA: 0x0005A46C File Offset: 0x0005866C
		[FreeFunction("RenderingCommandBuffer_Bindings::SetComputeConstantBufferParam", HasExplicitThis = true)]
		private void Internal_SetComputeConstantGraphicsBufferParam([NotNull] ComputeShader computeShader, int nameID, GraphicsBuffer buffer, int offset, int size)
		{
			if (computeShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(computeShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			CommandBuffer.Internal_SetComputeConstantGraphicsBufferParam_Injected(intPtr, intPtr2, nameID, (buffer == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(buffer), offset, size);
		}

		// Token: 0x06002F41 RID: 12097 RVA: 0x0005A4C8 File Offset: 0x000586C8
		[FreeFunction("RenderingCommandBuffer_Bindings::SetComputeParamsFromMaterial", HasExplicitThis = true)]
		private void Internal_SetComputeParamsFromMaterial([NotNull] ComputeShader computeShader, int kernelIndex, Material material)
		{
			if (computeShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(computeShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			CommandBuffer.Internal_SetComputeParamsFromMaterial_Injected(intPtr, intPtr2, kernelIndex, Object.MarshalledUnityObject.Marshal<Material>(material));
		}

		// Token: 0x06002F42 RID: 12098 RVA: 0x0005A518 File Offset: 0x00058718
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_DispatchCompute", HasExplicitThis = true, ThrowsException = true)]
		private void Internal_DispatchCompute([NotNull] ComputeShader computeShader, int kernelIndex, int threadGroupsX, int threadGroupsY, int threadGroupsZ)
		{
			if (computeShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(computeShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			CommandBuffer.Internal_DispatchCompute_Injected(intPtr, intPtr2, kernelIndex, threadGroupsX, threadGroupsY, threadGroupsZ);
		}

		// Token: 0x06002F43 RID: 12099 RVA: 0x0005A564 File Offset: 0x00058764
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_DispatchComputeIndirect", HasExplicitThis = true, ThrowsException = true)]
		private void Internal_DispatchComputeIndirect([NotNull] ComputeShader computeShader, int kernelIndex, ComputeBuffer indirectBuffer, uint argsOffset)
		{
			if (computeShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(computeShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			CommandBuffer.Internal_DispatchComputeIndirect_Injected(intPtr, intPtr2, kernelIndex, (indirectBuffer == null) ? ((IntPtr)0) : ComputeBuffer.BindingsMarshaller.ConvertToNative(indirectBuffer), argsOffset);
		}

		// Token: 0x06002F44 RID: 12100 RVA: 0x0005A5BC File Offset: 0x000587BC
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_DispatchComputeIndirect", HasExplicitThis = true, ThrowsException = true)]
		private void Internal_DispatchComputeIndirectGraphicsBuffer([NotNull] ComputeShader computeShader, int kernelIndex, GraphicsBuffer indirectBuffer, uint argsOffset)
		{
			if (computeShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(computeShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			CommandBuffer.Internal_DispatchComputeIndirectGraphicsBuffer_Injected(intPtr, intPtr2, kernelIndex, (indirectBuffer == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(indirectBuffer), argsOffset);
		}

		// Token: 0x06002F45 RID: 12101 RVA: 0x0005A614 File Offset: 0x00058814
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetRayTracingBufferParam", HasExplicitThis = true)]
		private void Internal_SetRayTracingComputeBufferParam([NotNull] RayTracingShader rayTracingShader, int nameID, ComputeBuffer buffer)
		{
			if (rayTracingShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<RayTracingShader>(rayTracingShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			CommandBuffer.Internal_SetRayTracingComputeBufferParam_Injected(intPtr, intPtr2, nameID, (buffer == null) ? ((IntPtr)0) : ComputeBuffer.BindingsMarshaller.ConvertToNative(buffer));
		}

		// Token: 0x06002F46 RID: 12102 RVA: 0x0005A66C File Offset: 0x0005886C
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetRayTracingBufferParam", HasExplicitThis = true)]
		private void Internal_SetRayTracingGraphicsBufferParam([NotNull] RayTracingShader rayTracingShader, int nameID, GraphicsBuffer buffer)
		{
			if (rayTracingShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<RayTracingShader>(rayTracingShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			CommandBuffer.Internal_SetRayTracingGraphicsBufferParam_Injected(intPtr, intPtr2, nameID, (buffer == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(buffer));
		}

		// Token: 0x06002F47 RID: 12103 RVA: 0x0005A6C4 File Offset: 0x000588C4
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetRayTracingBufferParam", HasExplicitThis = true)]
		private void Internal_SetRayTracingGraphicsBufferHandleParam([NotNull] RayTracingShader rayTracingShader, int nameID, GraphicsBufferHandle bufferHandle)
		{
			if (rayTracingShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<RayTracingShader>(rayTracingShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			CommandBuffer.Internal_SetRayTracingGraphicsBufferHandleParam_Injected(intPtr, intPtr2, nameID, ref bufferHandle);
		}

		// Token: 0x06002F48 RID: 12104 RVA: 0x0005A710 File Offset: 0x00058910
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetRayTracingConstantBufferParam", HasExplicitThis = true)]
		private void Internal_SetRayTracingConstantComputeBufferParam([NotNull] RayTracingShader rayTracingShader, int nameID, ComputeBuffer buffer, int offset, int size)
		{
			if (rayTracingShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<RayTracingShader>(rayTracingShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			CommandBuffer.Internal_SetRayTracingConstantComputeBufferParam_Injected(intPtr, intPtr2, nameID, (buffer == null) ? ((IntPtr)0) : ComputeBuffer.BindingsMarshaller.ConvertToNative(buffer), offset, size);
		}

		// Token: 0x06002F49 RID: 12105 RVA: 0x0005A76C File Offset: 0x0005896C
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetRayTracingConstantBufferParam", HasExplicitThis = true)]
		private void Internal_SetRayTracingConstantGraphicsBufferParam([NotNull] RayTracingShader rayTracingShader, int nameID, GraphicsBuffer buffer, int offset, int size)
		{
			if (rayTracingShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<RayTracingShader>(rayTracingShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			CommandBuffer.Internal_SetRayTracingConstantGraphicsBufferParam_Injected(intPtr, intPtr2, nameID, (buffer == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(buffer), offset, size);
		}

		// Token: 0x06002F4A RID: 12106 RVA: 0x0005A7C8 File Offset: 0x000589C8
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetRayTracingTextureParam", HasExplicitThis = true)]
		private void Internal_SetRayTracingTextureParam([NotNull] RayTracingShader rayTracingShader, int nameID, ref RenderTargetIdentifier rt)
		{
			if (rayTracingShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<RayTracingShader>(rayTracingShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			CommandBuffer.Internal_SetRayTracingTextureParam_Injected(intPtr, intPtr2, nameID, ref rt);
		}

		// Token: 0x06002F4B RID: 12107 RVA: 0x0005A810 File Offset: 0x00058A10
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetRayTracingFloatParam", HasExplicitThis = true)]
		private void Internal_SetRayTracingFloatParam([NotNull] RayTracingShader rayTracingShader, int nameID, float val)
		{
			if (rayTracingShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<RayTracingShader>(rayTracingShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			CommandBuffer.Internal_SetRayTracingFloatParam_Injected(intPtr, intPtr2, nameID, val);
		}

		// Token: 0x06002F4C RID: 12108 RVA: 0x0005A858 File Offset: 0x00058A58
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetRayTracingIntParam", HasExplicitThis = true)]
		private void Internal_SetRayTracingIntParam([NotNull] RayTracingShader rayTracingShader, int nameID, int val)
		{
			if (rayTracingShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<RayTracingShader>(rayTracingShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			CommandBuffer.Internal_SetRayTracingIntParam_Injected(intPtr, intPtr2, nameID, val);
		}

		// Token: 0x06002F4D RID: 12109 RVA: 0x0005A8A0 File Offset: 0x00058AA0
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetRayTracingVectorParam", HasExplicitThis = true)]
		private void Internal_SetRayTracingVectorParam([NotNull] RayTracingShader rayTracingShader, int nameID, Vector4 val)
		{
			if (rayTracingShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<RayTracingShader>(rayTracingShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			CommandBuffer.Internal_SetRayTracingVectorParam_Injected(intPtr, intPtr2, nameID, ref val);
		}

		// Token: 0x06002F4E RID: 12110 RVA: 0x0005A8EC File Offset: 0x00058AEC
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetRayTracingVectorArrayParam", HasExplicitThis = true)]
		private unsafe void Internal_SetRayTracingVectorArrayParam([NotNull] RayTracingShader rayTracingShader, int nameID, Vector4[] values)
		{
			if (rayTracingShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<RayTracingShader>(rayTracingShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			Span<Vector4> span = new Span<Vector4>(values);
			fixed (Vector4* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				CommandBuffer.Internal_SetRayTracingVectorArrayParam_Injected(intPtr, intPtr2, nameID, ref managedSpanWrapper);
			}
		}

		// Token: 0x06002F4F RID: 12111 RVA: 0x0005A95C File Offset: 0x00058B5C
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetRayTracingMatrixParam", HasExplicitThis = true)]
		private void Internal_SetRayTracingMatrixParam([NotNull] RayTracingShader rayTracingShader, int nameID, Matrix4x4 val)
		{
			if (rayTracingShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<RayTracingShader>(rayTracingShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			CommandBuffer.Internal_SetRayTracingMatrixParam_Injected(intPtr, intPtr2, nameID, ref val);
		}

		// Token: 0x06002F50 RID: 12112 RVA: 0x0005A9A8 File Offset: 0x00058BA8
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetRayTracingMatrixArrayParam", HasExplicitThis = true)]
		private unsafe void Internal_SetRayTracingMatrixArrayParam([NotNull] RayTracingShader rayTracingShader, int nameID, Matrix4x4[] values)
		{
			if (rayTracingShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<RayTracingShader>(rayTracingShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			Span<Matrix4x4> span = new Span<Matrix4x4>(values);
			fixed (Matrix4x4* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				CommandBuffer.Internal_SetRayTracingMatrixArrayParam_Injected(intPtr, intPtr2, nameID, ref managedSpanWrapper);
			}
		}

		// Token: 0x06002F51 RID: 12113 RVA: 0x0005AA18 File Offset: 0x00058C18
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetRayTracingFloats", HasExplicitThis = true)]
		private unsafe void Internal_SetRayTracingFloats([NotNull] RayTracingShader rayTracingShader, int nameID, float[] values)
		{
			if (rayTracingShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<RayTracingShader>(rayTracingShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			Span<float> span = new Span<float>(values);
			fixed (float* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				CommandBuffer.Internal_SetRayTracingFloats_Injected(intPtr, intPtr2, nameID, ref managedSpanWrapper);
			}
		}

		// Token: 0x06002F52 RID: 12114 RVA: 0x0005AA88 File Offset: 0x00058C88
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetRayTracingInts", HasExplicitThis = true)]
		private unsafe void Internal_SetRayTracingInts([NotNull] RayTracingShader rayTracingShader, int nameID, int[] values)
		{
			if (rayTracingShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<RayTracingShader>(rayTracingShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			Span<int> span = new Span<int>(values);
			fixed (int* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				CommandBuffer.Internal_SetRayTracingInts_Injected(intPtr, intPtr2, nameID, ref managedSpanWrapper);
			}
		}

		// Token: 0x06002F53 RID: 12115 RVA: 0x0005AAF8 File Offset: 0x00058CF8
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_BuildRayTracingAccelerationStructure", HasExplicitThis = true)]
		private void Internal_BuildRayTracingAccelerationStructure([NotNull] RayTracingAccelerationStructure accelerationStructure, RayTracingAccelerationStructure.BuildSettings buildSettings)
		{
			if (accelerationStructure == null)
			{
				ThrowHelper.ThrowArgumentNullException(accelerationStructure, "accelerationStructure");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = RayTracingAccelerationStructure.BindingsMarshaller.ConvertToNative(accelerationStructure);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(accelerationStructure, "accelerationStructure");
			}
			CommandBuffer.Internal_BuildRayTracingAccelerationStructure_Injected(intPtr, intPtr2, ref buildSettings);
		}

		// Token: 0x06002F54 RID: 12116 RVA: 0x0005AB40 File Offset: 0x00058D40
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetRayTracingAccelerationStructure", HasExplicitThis = true)]
		private void Internal_SetRayTracingAccelerationStructure([NotNull] RayTracingShader rayTracingShader, int nameID, [NotNull] RayTracingAccelerationStructure accelerationStructure)
		{
			if (rayTracingShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			if (accelerationStructure == null)
			{
				ThrowHelper.ThrowArgumentNullException(accelerationStructure, "accelerationStructure");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<RayTracingShader>(rayTracingShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			IntPtr intPtr3 = RayTracingAccelerationStructure.BindingsMarshaller.ConvertToNative(accelerationStructure);
			if (intPtr3 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(accelerationStructure, "accelerationStructure");
			}
			CommandBuffer.Internal_SetRayTracingAccelerationStructure_Injected(intPtr, intPtr2, nameID, intPtr3);
		}

		// Token: 0x06002F55 RID: 12117 RVA: 0x0005ABAC File Offset: 0x00058DAC
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetComputeRayTracingAccelerationStructure", HasExplicitThis = true)]
		private void Internal_SetComputeRayTracingAccelerationStructure([NotNull] ComputeShader computeShader, int kernelIndex, int nameID, [NotNull] RayTracingAccelerationStructure accelerationStructure)
		{
			if (computeShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			if (accelerationStructure == null)
			{
				ThrowHelper.ThrowArgumentNullException(accelerationStructure, "accelerationStructure");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(computeShader);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(computeShader, "computeShader");
			}
			IntPtr intPtr3 = RayTracingAccelerationStructure.BindingsMarshaller.ConvertToNative(accelerationStructure);
			if (intPtr3 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(accelerationStructure, "accelerationStructure");
			}
			CommandBuffer.Internal_SetComputeRayTracingAccelerationStructure_Injected(intPtr, intPtr2, kernelIndex, nameID, intPtr3);
		}

		// Token: 0x06002F56 RID: 12118 RVA: 0x0005AC1C File Offset: 0x00058E1C
		[NativeMethod("AddSetRayTracingShaderPass")]
		public unsafe void SetRayTracingShaderPass([NotNull] RayTracingShader rayTracingShader, string passName)
		{
			if (rayTracingShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			try
			{
				IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<RayTracingShader>(rayTracingShader);
				if (intPtr2 == 0)
				{
					ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(passName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = passName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				CommandBuffer.SetRayTracingShaderPass_Injected(intPtr, intPtr2, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06002F57 RID: 12119 RVA: 0x0005ACA4 File Offset: 0x00058EA4
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_DispatchRays", HasExplicitThis = true, ThrowsException = true)]
		private unsafe void Internal_DispatchRays([NotNull] RayTracingShader rayTracingShader, string rayGenShaderName, uint width, uint height, uint depth, Camera camera = null)
		{
			if (rayTracingShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			try
			{
				IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<RayTracingShader>(rayTracingShader);
				if (intPtr2 == 0)
				{
					ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(rayGenShaderName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = rayGenShaderName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				CommandBuffer.Internal_DispatchRays_Injected(intPtr, intPtr2, ref managedSpanWrapper, width, height, depth, Object.MarshalledUnityObject.Marshal<Camera>(camera));
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06002F58 RID: 12120 RVA: 0x0005AD38 File Offset: 0x00058F38
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_DispatchRaysIndirect", HasExplicitThis = true, ThrowsException = true)]
		private unsafe void Internal_DispatchRaysIndirect([NotNull] RayTracingShader rayTracingShader, string rayGenShaderName, [NotNull] GraphicsBuffer argsBuffer, uint argsOffset = 0U, Camera camera = null)
		{
			if (rayTracingShader == null)
			{
				ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
			}
			if (argsBuffer == null)
			{
				ThrowHelper.ThrowArgumentNullException(argsBuffer, "argsBuffer");
			}
			try
			{
				IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<RayTracingShader>(rayTracingShader);
				if (intPtr2 == 0)
				{
					ThrowHelper.ThrowArgumentNullException(rayTracingShader, "rayTracingShader");
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(rayGenShaderName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = rayGenShaderName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				IntPtr intPtr3 = GraphicsBuffer.BindingsMarshaller.ConvertToNative(argsBuffer);
				if (intPtr3 == 0)
				{
					ThrowHelper.ThrowArgumentNullException(argsBuffer, "argsBuffer");
				}
				CommandBuffer.Internal_DispatchRaysIndirect_Injected(intPtr, intPtr2, ref managedSpanWrapper, intPtr3, argsOffset, Object.MarshalledUnityObject.Marshal<Camera>(camera));
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06002F59 RID: 12121 RVA: 0x0005ADF0 File Offset: 0x00058FF0
		[NativeMethod("AddGenerateMips")]
		private void Internal_GenerateMips(RenderTargetIdentifier rt)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.Internal_GenerateMips_Injected(intPtr, ref rt);
		}

		// Token: 0x06002F5A RID: 12122 RVA: 0x0005AE14 File Offset: 0x00059014
		[NativeMethod("AddResolveAntiAliasedSurface")]
		private void Internal_ResolveAntiAliasedSurface(RenderTexture rt, RenderTexture target)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.Internal_ResolveAntiAliasedSurface_Injected(intPtr, Object.MarshalledUnityObject.Marshal<RenderTexture>(rt), Object.MarshalledUnityObject.Marshal<RenderTexture>(target));
		}

		// Token: 0x06002F5B RID: 12123 RVA: 0x0005AE44 File Offset: 0x00059044
		[NativeMethod("AddCopyCounterValue")]
		private void CopyCounterValueCC(ComputeBuffer src, ComputeBuffer dst, uint dstOffsetBytes)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.CopyCounterValueCC_Injected(intPtr, (src == null) ? ((IntPtr)0) : ComputeBuffer.BindingsMarshaller.ConvertToNative(src), (dst == null) ? ((IntPtr)0) : ComputeBuffer.BindingsMarshaller.ConvertToNative(dst), dstOffsetBytes);
		}

		// Token: 0x06002F5C RID: 12124 RVA: 0x0005AE88 File Offset: 0x00059088
		[NativeMethod("AddCopyCounterValue")]
		private void CopyCounterValueGC(GraphicsBuffer src, ComputeBuffer dst, uint dstOffsetBytes)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.CopyCounterValueGC_Injected(intPtr, (src == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(src), (dst == null) ? ((IntPtr)0) : ComputeBuffer.BindingsMarshaller.ConvertToNative(dst), dstOffsetBytes);
		}

		// Token: 0x06002F5D RID: 12125 RVA: 0x0005AECC File Offset: 0x000590CC
		[NativeMethod("AddCopyCounterValue")]
		private void CopyCounterValueCG(ComputeBuffer src, GraphicsBuffer dst, uint dstOffsetBytes)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.CopyCounterValueCG_Injected(intPtr, (src == null) ? ((IntPtr)0) : ComputeBuffer.BindingsMarshaller.ConvertToNative(src), (dst == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(dst), dstOffsetBytes);
		}

		// Token: 0x06002F5E RID: 12126 RVA: 0x0005AF10 File Offset: 0x00059110
		[NativeMethod("AddCopyCounterValue")]
		private void CopyCounterValueGG(GraphicsBuffer src, GraphicsBuffer dst, uint dstOffsetBytes)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.CopyCounterValueGG_Injected(intPtr, (src == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(src), (dst == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(dst), dstOffsetBytes);
		}

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x06002F5F RID: 12127 RVA: 0x0005AF54 File Offset: 0x00059154
		// (set) Token: 0x06002F60 RID: 12128 RVA: 0x0005AF94 File Offset: 0x00059194
		public unsafe string name
		{
			get
			{
				string stringAndDispose;
				try
				{
					IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					ManagedSpanWrapper managedSpanWrapper;
					CommandBuffer.get_name_Injected(intPtr, out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
			set
			{
				try
				{
					IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					ManagedSpanWrapper managedSpanWrapper;
					if (!StringMarshaller.TryMarshalEmptyOrNullString(value, ref managedSpanWrapper))
					{
						ReadOnlySpan<char> readOnlySpan = value.AsSpan();
						fixed (char* ptr = readOnlySpan.GetPinnableReference())
						{
							managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
						}
					}
					CommandBuffer.set_name_Injected(intPtr, ref managedSpanWrapper);
				}
				finally
				{
					char* ptr = null;
				}
			}
		}

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x06002F61 RID: 12129 RVA: 0x0005AFF8 File Offset: 0x000591F8
		public int sizeInBytes
		{
			[NativeMethod("GetBufferSize")]
			get
			{
				IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return CommandBuffer.get_sizeInBytes_Injected(intPtr);
			}
		}

		// Token: 0x06002F62 RID: 12130 RVA: 0x0005B01C File Offset: 0x0005921C
		[NativeMethod("ClearCommands")]
		public void Clear()
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.Clear_Injected(intPtr);
		}

		// Token: 0x06002F63 RID: 12131 RVA: 0x0005B040 File Offset: 0x00059240
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_DrawMesh", HasExplicitThis = true)]
		private void Internal_DrawMesh([NotNull] Mesh mesh, Matrix4x4 matrix, Material material, int submeshIndex, int shaderPass, MaterialPropertyBlock properties)
		{
			if (mesh == null)
			{
				ThrowHelper.ThrowArgumentNullException(mesh, "mesh");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(mesh);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(mesh, "mesh");
			}
			CommandBuffer.Internal_DrawMesh_Injected(intPtr, intPtr2, ref matrix, Object.MarshalledUnityObject.Marshal<Material>(material), submeshIndex, shaderPass, (properties == null) ? ((IntPtr)0) : MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(properties));
		}

		// Token: 0x06002F64 RID: 12132 RVA: 0x0005B0A4 File Offset: 0x000592A4
		[NativeMethod("AddDrawMultipleMeshes")]
		private unsafe void Internal_DrawMultipleMeshes(Matrix4x4[] matrices, Mesh[] meshes, int[] subsetIndices, int count, Material material, int shaderPass, MaterialPropertyBlock properties)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<Matrix4x4> span = new Span<Matrix4x4>(matrices);
			fixed (Matrix4x4* ptr = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, span.Length);
				Span<int> span2 = new Span<int>(subsetIndices);
				fixed (int* pinnableReference = span2.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper2 = new ManagedSpanWrapper((void*)pinnableReference, span2.Length);
					CommandBuffer.Internal_DrawMultipleMeshes_Injected(intPtr, ref managedSpanWrapper, meshes, ref managedSpanWrapper2, count, Object.MarshalledUnityObject.Marshal<Material>(material), shaderPass, (properties == null) ? ((IntPtr)0) : MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(properties));
					ptr = null;
				}
			}
		}

		// Token: 0x06002F65 RID: 12133 RVA: 0x0005B134 File Offset: 0x00059334
		[NativeMethod("AddDrawRenderer")]
		private void Internal_DrawRenderer([NotNull] Renderer renderer, Material material, int submeshIndex, int shaderPass)
		{
			if (renderer == null)
			{
				ThrowHelper.ThrowArgumentNullException(renderer, "renderer");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Renderer>(renderer);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(renderer, "renderer");
			}
			CommandBuffer.Internal_DrawRenderer_Injected(intPtr, intPtr2, Object.MarshalledUnityObject.Marshal<Material>(material), submeshIndex, shaderPass);
		}

		// Token: 0x06002F66 RID: 12134 RVA: 0x0005B184 File Offset: 0x00059384
		[NativeMethod("AddDrawRendererList")]
		private void Internal_DrawRendererList(RendererList rendererList)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.Internal_DrawRendererList_Injected(intPtr, ref rendererList);
		}

		// Token: 0x06002F67 RID: 12135 RVA: 0x0005B1A8 File Offset: 0x000593A8
		private void Internal_DrawRenderer(Renderer renderer, Material material, int submeshIndex)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Internal_DrawRenderer(renderer, material, submeshIndex, -1);
		}

		// Token: 0x06002F68 RID: 12136 RVA: 0x0005B1BF File Offset: 0x000593BF
		private void Internal_DrawRenderer(Renderer renderer, Material material)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Internal_DrawRenderer(renderer, material, 0);
		}

		// Token: 0x06002F69 RID: 12137 RVA: 0x0005B1D8 File Offset: 0x000593D8
		[NativeMethod("AddDrawProcedural")]
		private void Internal_DrawProcedural(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, int vertexCount, int instanceCount, MaterialPropertyBlock properties)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.Internal_DrawProcedural_Injected(intPtr, ref matrix, Object.MarshalledUnityObject.Marshal<Material>(material), shaderPass, topology, vertexCount, instanceCount, (properties == null) ? ((IntPtr)0) : MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(properties));
		}

		// Token: 0x06002F6A RID: 12138 RVA: 0x0005B21C File Offset: 0x0005941C
		[NativeMethod("AddDrawProceduralIndexed")]
		private void Internal_DrawProceduralIndexed(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, int indexCount, int instanceCount, MaterialPropertyBlock properties)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.Internal_DrawProceduralIndexed_Injected(intPtr, (indexBuffer == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(indexBuffer), ref matrix, Object.MarshalledUnityObject.Marshal<Material>(material), shaderPass, topology, indexCount, instanceCount, (properties == null) ? ((IntPtr)0) : MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(properties));
		}

		// Token: 0x06002F6B RID: 12139 RVA: 0x0005B270 File Offset: 0x00059470
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_DrawProceduralIndirect", HasExplicitThis = true)]
		private void Internal_DrawProceduralIndirect(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, ComputeBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.Internal_DrawProceduralIndirect_Injected(intPtr, ref matrix, Object.MarshalledUnityObject.Marshal<Material>(material), shaderPass, topology, (bufferWithArgs == null) ? ((IntPtr)0) : ComputeBuffer.BindingsMarshaller.ConvertToNative(bufferWithArgs), argsOffset, (properties == null) ? ((IntPtr)0) : MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(properties));
		}

		// Token: 0x06002F6C RID: 12140 RVA: 0x0005B2C4 File Offset: 0x000594C4
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_DrawProceduralIndexedIndirect", HasExplicitThis = true)]
		private void Internal_DrawProceduralIndexedIndirect(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, ComputeBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.Internal_DrawProceduralIndexedIndirect_Injected(intPtr, (indexBuffer == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(indexBuffer), ref matrix, Object.MarshalledUnityObject.Marshal<Material>(material), shaderPass, topology, (bufferWithArgs == null) ? ((IntPtr)0) : ComputeBuffer.BindingsMarshaller.ConvertToNative(bufferWithArgs), argsOffset, (properties == null) ? ((IntPtr)0) : MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(properties));
		}

		// Token: 0x06002F6D RID: 12141 RVA: 0x0005B328 File Offset: 0x00059528
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_DrawProceduralIndirect", HasExplicitThis = true)]
		private void Internal_DrawProceduralIndirectGraphicsBuffer(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, GraphicsBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.Internal_DrawProceduralIndirectGraphicsBuffer_Injected(intPtr, ref matrix, Object.MarshalledUnityObject.Marshal<Material>(material), shaderPass, topology, (bufferWithArgs == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(bufferWithArgs), argsOffset, (properties == null) ? ((IntPtr)0) : MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(properties));
		}

		// Token: 0x06002F6E RID: 12142 RVA: 0x0005B37C File Offset: 0x0005957C
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_DrawProceduralIndexedIndirect", HasExplicitThis = true)]
		private void Internal_DrawProceduralIndexedIndirectGraphicsBuffer(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, GraphicsBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.Internal_DrawProceduralIndexedIndirectGraphicsBuffer_Injected(intPtr, (indexBuffer == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(indexBuffer), ref matrix, Object.MarshalledUnityObject.Marshal<Material>(material), shaderPass, topology, (bufferWithArgs == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(bufferWithArgs), argsOffset, (properties == null) ? ((IntPtr)0) : MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(properties));
		}

		// Token: 0x06002F6F RID: 12143 RVA: 0x0005B3E0 File Offset: 0x000595E0
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_DrawMeshInstanced", HasExplicitThis = true)]
		private unsafe void Internal_DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, int shaderPass, Matrix4x4[] matrices, int count, MaterialPropertyBlock properties)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.Marshal<Mesh>(mesh);
			IntPtr intPtr3 = Object.MarshalledUnityObject.Marshal<Material>(material);
			Span<Matrix4x4> span = new Span<Matrix4x4>(matrices);
			fixed (Matrix4x4* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				CommandBuffer.Internal_DrawMeshInstanced_Injected(intPtr, intPtr2, submeshIndex, intPtr3, shaderPass, ref managedSpanWrapper, count, (properties == null) ? ((IntPtr)0) : MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(properties));
			}
		}

		// Token: 0x06002F70 RID: 12144 RVA: 0x0005B44C File Offset: 0x0005964C
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_DrawMeshInstancedProcedural", HasExplicitThis = true)]
		private void Internal_DrawMeshInstancedProcedural(Mesh mesh, int submeshIndex, Material material, int shaderPass, int count, MaterialPropertyBlock properties)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.Internal_DrawMeshInstancedProcedural_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Mesh>(mesh), submeshIndex, Object.MarshalledUnityObject.Marshal<Material>(material), shaderPass, count, (properties == null) ? ((IntPtr)0) : MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(properties));
		}

		// Token: 0x06002F71 RID: 12145 RVA: 0x0005B490 File Offset: 0x00059690
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_DrawMeshInstancedIndirect", HasExplicitThis = true)]
		private void Internal_DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, int shaderPass, ComputeBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.Internal_DrawMeshInstancedIndirect_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Mesh>(mesh), submeshIndex, Object.MarshalledUnityObject.Marshal<Material>(material), shaderPass, (bufferWithArgs == null) ? ((IntPtr)0) : ComputeBuffer.BindingsMarshaller.ConvertToNative(bufferWithArgs), argsOffset, (properties == null) ? ((IntPtr)0) : MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(properties));
		}

		// Token: 0x06002F72 RID: 12146 RVA: 0x0005B4E8 File Offset: 0x000596E8
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_DrawMeshInstancedIndirect", HasExplicitThis = true)]
		private void Internal_DrawMeshInstancedIndirectGraphicsBuffer(Mesh mesh, int submeshIndex, Material material, int shaderPass, GraphicsBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.Internal_DrawMeshInstancedIndirectGraphicsBuffer_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Mesh>(mesh), submeshIndex, Object.MarshalledUnityObject.Marshal<Material>(material), shaderPass, (bufferWithArgs == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(bufferWithArgs), argsOffset, (properties == null) ? ((IntPtr)0) : MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(properties));
		}

		// Token: 0x06002F73 RID: 12147 RVA: 0x0005B540 File Offset: 0x00059740
		[FreeFunction("RenderingCommandBuffer_Bindings::Internal_DrawOcclusionMesh", HasExplicitThis = true)]
		private void Internal_DrawOcclusionMesh(RectInt normalizedCamViewport)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.Internal_DrawOcclusionMesh_Injected(intPtr, ref normalizedCamViewport);
		}

		// Token: 0x06002F74 RID: 12148 RVA: 0x0005B564 File Offset: 0x00059764
		[FreeFunction("RenderingCommandBuffer_Bindings::SetRandomWriteTarget_Texture", HasExplicitThis = true, ThrowsException = true)]
		private void SetRandomWriteTarget_Texture(int index, ref RenderTargetIdentifier rt)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetRandomWriteTarget_Texture_Injected(intPtr, index, ref rt);
		}

		// Token: 0x06002F75 RID: 12149 RVA: 0x0005B588 File Offset: 0x00059788
		[FreeFunction("RenderingCommandBuffer_Bindings::SetRandomWriteTarget_Buffer", HasExplicitThis = true, ThrowsException = true)]
		private void SetRandomWriteTarget_Buffer(int index, ComputeBuffer uav, bool preserveCounterValue)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetRandomWriteTarget_Buffer_Injected(intPtr, index, (uav == null) ? ((IntPtr)0) : ComputeBuffer.BindingsMarshaller.ConvertToNative(uav), preserveCounterValue);
		}

		// Token: 0x06002F76 RID: 12150 RVA: 0x0005B5BC File Offset: 0x000597BC
		[FreeFunction("RenderingCommandBuffer_Bindings::SetRandomWriteTarget_Buffer", HasExplicitThis = true, ThrowsException = true)]
		private void SetRandomWriteTarget_GraphicsBuffer(int index, GraphicsBuffer uav, bool preserveCounterValue)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetRandomWriteTarget_GraphicsBuffer_Injected(intPtr, index, (uav == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(uav), preserveCounterValue);
		}

		// Token: 0x06002F77 RID: 12151 RVA: 0x0005B5F0 File Offset: 0x000597F0
		[FreeFunction("RenderingCommandBuffer_Bindings::ClearRandomWriteTargets", HasExplicitThis = true, ThrowsException = true)]
		public void ClearRandomWriteTargets()
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.ClearRandomWriteTargets_Injected(intPtr);
		}

		// Token: 0x06002F78 RID: 12152 RVA: 0x0005B614 File Offset: 0x00059814
		[FreeFunction("RenderingCommandBuffer_Bindings::SetViewport", HasExplicitThis = true, ThrowsException = true)]
		public void SetViewport(Rect pixelRect)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetViewport_Injected(intPtr, ref pixelRect);
		}

		// Token: 0x06002F79 RID: 12153 RVA: 0x0005B638 File Offset: 0x00059838
		[FreeFunction("RenderingCommandBuffer_Bindings::EnableScissorRect", HasExplicitThis = true, ThrowsException = true)]
		public void EnableScissorRect(Rect scissor)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.EnableScissorRect_Injected(intPtr, ref scissor);
		}

		// Token: 0x06002F7A RID: 12154 RVA: 0x0005B65C File Offset: 0x0005985C
		[FreeFunction("RenderingCommandBuffer_Bindings::DisableScissorRect", HasExplicitThis = true, ThrowsException = true)]
		public void DisableScissorRect()
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.DisableScissorRect_Injected(intPtr);
		}

		// Token: 0x06002F7B RID: 12155 RVA: 0x0005B680 File Offset: 0x00059880
		[FreeFunction("RenderingCommandBuffer_Bindings::CopyTexture_Internal", HasExplicitThis = true)]
		private void CopyTexture_Internal(ref RenderTargetIdentifier src, int srcElement, int srcMip, int srcX, int srcY, int srcWidth, int srcHeight, ref RenderTargetIdentifier dst, int dstElement, int dstMip, int dstX, int dstY, int mode)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.CopyTexture_Internal_Injected(intPtr, ref src, srcElement, srcMip, srcX, srcY, srcWidth, srcHeight, ref dst, dstElement, dstMip, dstX, dstY, mode);
		}

		// Token: 0x06002F7C RID: 12156 RVA: 0x0005B6BC File Offset: 0x000598BC
		[FreeFunction("RenderingCommandBuffer_Bindings::Blit_Texture", HasExplicitThis = true)]
		private void Blit_Texture(Texture source, ref RenderTargetIdentifier dest, Material mat, int pass, Vector2 scale, Vector2 offset, int sourceDepthSlice, int destDepthSlice)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.Blit_Texture_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Texture>(source), ref dest, Object.MarshalledUnityObject.Marshal<Material>(mat), pass, ref scale, ref offset, sourceDepthSlice, destDepthSlice);
		}

		// Token: 0x06002F7D RID: 12157 RVA: 0x0005B6F8 File Offset: 0x000598F8
		[FreeFunction("RenderingCommandBuffer_Bindings::Blit_Identifier", HasExplicitThis = true)]
		private void Blit_Identifier(ref RenderTargetIdentifier source, ref RenderTargetIdentifier dest, Material mat, int pass, Vector2 scale, Vector2 offset, int sourceDepthSlice, int destDepthSlice)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.Blit_Identifier_Injected(intPtr, ref source, ref dest, Object.MarshalledUnityObject.Marshal<Material>(mat), pass, ref scale, ref offset, sourceDepthSlice, destDepthSlice);
		}

		// Token: 0x06002F7E RID: 12158 RVA: 0x0005B72C File Offset: 0x0005992C
		[FreeFunction("RenderingCommandBuffer_Bindings::GetTemporaryRT", HasExplicitThis = true)]
		private void GetTemporaryRT(int nameID, int width, int height, FilterMode filter, GraphicsFormat colorFormat, GraphicsFormat depthStencilFormat, int antiAliasing, bool enableRandomWrite, RenderTextureMemoryless memorylessMode, bool useDynamicScale, ShadowSamplingMode shadowSamplingMode)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.GetTemporaryRT_Injected(intPtr, nameID, width, height, filter, colorFormat, depthStencilFormat, antiAliasing, enableRandomWrite, memorylessMode, useDynamicScale, shadowSamplingMode);
		}

		// Token: 0x06002F7F RID: 12159 RVA: 0x0005B764 File Offset: 0x00059964
		public void GetTemporaryRT(int nameID, int width, int height, int depthBuffer, FilterMode filter, GraphicsFormat format, int antiAliasing, bool enableRandomWrite, RenderTextureMemoryless memorylessMode, bool useDynamicScale)
		{
			GraphicsFormat depthStencilFormatLegacy = RenderTexture.GetDepthStencilFormatLegacy(depthBuffer, format);
			this.GetTemporaryRT(nameID, width, height, filter, format, depthStencilFormatLegacy, antiAliasing, enableRandomWrite, memorylessMode, useDynamicScale, ShadowSamplingMode.None);
		}

		// Token: 0x06002F80 RID: 12160 RVA: 0x0005B794 File Offset: 0x00059994
		public void GetTemporaryRT(int nameID, int width, int height, int depthBuffer, FilterMode filter, GraphicsFormat format, int antiAliasing, bool enableRandomWrite, RenderTextureMemoryless memorylessMode)
		{
			this.GetTemporaryRT(nameID, width, height, depthBuffer, filter, format, antiAliasing, enableRandomWrite, memorylessMode, false);
		}

		// Token: 0x06002F81 RID: 12161 RVA: 0x0005B7BC File Offset: 0x000599BC
		public void GetTemporaryRT(int nameID, int width, int height, int depthBuffer, FilterMode filter, GraphicsFormat format, int antiAliasing, bool enableRandomWrite)
		{
			this.GetTemporaryRT(nameID, width, height, depthBuffer, filter, format, antiAliasing, enableRandomWrite, RenderTextureMemoryless.None);
		}

		// Token: 0x06002F82 RID: 12162 RVA: 0x0005B7E0 File Offset: 0x000599E0
		public void GetTemporaryRT(int nameID, int width, int height, int depthBuffer, FilterMode filter, GraphicsFormat format, int antiAliasing)
		{
			this.GetTemporaryRT(nameID, width, height, depthBuffer, filter, format, antiAliasing, false, RenderTextureMemoryless.None);
		}

		// Token: 0x06002F83 RID: 12163 RVA: 0x0005B802 File Offset: 0x00059A02
		public void GetTemporaryRT(int nameID, int width, int height, int depthBuffer, FilterMode filter, GraphicsFormat format)
		{
			this.GetTemporaryRT(nameID, width, height, depthBuffer, filter, format, 1);
		}

		// Token: 0x06002F84 RID: 12164 RVA: 0x0005B818 File Offset: 0x00059A18
		public void GetTemporaryRT(int nameID, int width, int height, int depthBuffer, FilterMode filter, RenderTextureFormat format, RenderTextureReadWrite readWrite, int antiAliasing, bool enableRandomWrite, RenderTextureMemoryless memorylessMode, bool useDynamicScale)
		{
			ShadowSamplingMode shadowSamplingModeForFormat = RenderTexture.GetShadowSamplingModeForFormat(format);
			GraphicsFormat graphicsFormat = GraphicsFormatUtility.GetGraphicsFormat(format, readWrite);
			GraphicsFormat depthStencilFormatLegacy = RenderTexture.GetDepthStencilFormatLegacy(depthBuffer, format, false);
			this.GetTemporaryRT(nameID, width, height, filter, graphicsFormat, depthStencilFormatLegacy, antiAliasing, enableRandomWrite, memorylessMode, useDynamicScale, shadowSamplingModeForFormat);
		}

		// Token: 0x06002F85 RID: 12165 RVA: 0x0005B85C File Offset: 0x00059A5C
		public void GetTemporaryRT(int nameID, int width, int height, int depthBuffer, FilterMode filter, RenderTextureFormat format, RenderTextureReadWrite readWrite, int antiAliasing, bool enableRandomWrite, RenderTextureMemoryless memorylessMode)
		{
			this.GetTemporaryRT(nameID, width, height, depthBuffer, filter, format, readWrite, antiAliasing, enableRandomWrite, memorylessMode, false);
		}

		// Token: 0x06002F86 RID: 12166 RVA: 0x0005B884 File Offset: 0x00059A84
		public void GetTemporaryRT(int nameID, int width, int height, int depthBuffer, FilterMode filter, RenderTextureFormat format, RenderTextureReadWrite readWrite, int antiAliasing, bool enableRandomWrite)
		{
			this.GetTemporaryRT(nameID, width, height, depthBuffer, filter, format, readWrite, antiAliasing, enableRandomWrite, RenderTextureMemoryless.None);
		}

		// Token: 0x06002F87 RID: 12167 RVA: 0x0005B8AC File Offset: 0x00059AAC
		public void GetTemporaryRT(int nameID, int width, int height, int depthBuffer, FilterMode filter, RenderTextureFormat format, RenderTextureReadWrite readWrite, int antiAliasing)
		{
			this.GetTemporaryRT(nameID, width, height, depthBuffer, filter, format, readWrite, antiAliasing, false);
		}

		// Token: 0x06002F88 RID: 12168 RVA: 0x0005B8D0 File Offset: 0x00059AD0
		public void GetTemporaryRT(int nameID, int width, int height, int depthBuffer, FilterMode filter, RenderTextureFormat format, RenderTextureReadWrite readWrite)
		{
			this.GetTemporaryRT(nameID, width, height, depthBuffer, filter, format, readWrite, 1);
		}

		// Token: 0x06002F89 RID: 12169 RVA: 0x0005B8F1 File Offset: 0x00059AF1
		public void GetTemporaryRT(int nameID, int width, int height, int depthBuffer, FilterMode filter, RenderTextureFormat format)
		{
			this.GetTemporaryRT(nameID, width, height, depthBuffer, filter, format, RenderTextureReadWrite.Default);
		}

		// Token: 0x06002F8A RID: 12170 RVA: 0x0005B905 File Offset: 0x00059B05
		public void GetTemporaryRT(int nameID, int width, int height, int depthBuffer, FilterMode filter)
		{
			this.GetTemporaryRT(nameID, width, height, depthBuffer, filter, RenderTextureFormat.Default);
		}

		// Token: 0x06002F8B RID: 12171 RVA: 0x0005B917 File Offset: 0x00059B17
		public void GetTemporaryRT(int nameID, int width, int height, int depthBuffer)
		{
			this.GetTemporaryRT(nameID, width, height, depthBuffer, FilterMode.Point);
		}

		// Token: 0x06002F8C RID: 12172 RVA: 0x0005B927 File Offset: 0x00059B27
		public void GetTemporaryRT(int nameID, int width, int height)
		{
			this.GetTemporaryRT(nameID, width, height, 0);
		}

		// Token: 0x06002F8D RID: 12173 RVA: 0x0005B938 File Offset: 0x00059B38
		[FreeFunction("RenderingCommandBuffer_Bindings::GetTemporaryRTWithDescriptor", HasExplicitThis = true)]
		private void GetTemporaryRTWithDescriptor(int nameID, RenderTextureDescriptor desc, FilterMode filter)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.GetTemporaryRTWithDescriptor_Injected(intPtr, nameID, ref desc, filter);
		}

		// Token: 0x06002F8E RID: 12174 RVA: 0x0005B95E File Offset: 0x00059B5E
		public void GetTemporaryRT(int nameID, RenderTextureDescriptor desc, FilterMode filter)
		{
			this.GetTemporaryRTWithDescriptor(nameID, desc, filter);
		}

		// Token: 0x06002F8F RID: 12175 RVA: 0x0005B96B File Offset: 0x00059B6B
		public void GetTemporaryRT(int nameID, RenderTextureDescriptor desc)
		{
			this.GetTemporaryRT(nameID, desc, FilterMode.Point);
		}

		// Token: 0x06002F90 RID: 12176 RVA: 0x0005B978 File Offset: 0x00059B78
		[FreeFunction("RenderingCommandBuffer_Bindings::GetTemporaryRTArray", HasExplicitThis = true)]
		private void GetTemporaryRTArray(int nameID, int width, int height, int slices, FilterMode filter, GraphicsFormat colorFormat, GraphicsFormat depthStencilFormat, int antiAliasing, bool enableRandomWrite, bool useDynamicScale, ShadowSamplingMode shadowSamplingMode)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.GetTemporaryRTArray_Injected(intPtr, nameID, width, height, slices, filter, colorFormat, depthStencilFormat, antiAliasing, enableRandomWrite, useDynamicScale, shadowSamplingMode);
		}

		// Token: 0x06002F91 RID: 12177 RVA: 0x0005B9B0 File Offset: 0x00059BB0
		public void GetTemporaryRTArray(int nameID, int width, int height, int slices, int depthBuffer, FilterMode filter, GraphicsFormat format, int antiAliasing, bool enableRandomWrite, bool useDynamicScale)
		{
			GraphicsFormat depthStencilFormatLegacy = RenderTexture.GetDepthStencilFormatLegacy(depthBuffer, format);
			this.GetTemporaryRTArray(nameID, width, height, slices, filter, format, depthStencilFormatLegacy, antiAliasing, enableRandomWrite, useDynamicScale, ShadowSamplingMode.None);
		}

		// Token: 0x06002F92 RID: 12178 RVA: 0x0005B9E0 File Offset: 0x00059BE0
		public void GetTemporaryRTArray(int nameID, int width, int height, int slices, int depthBuffer, FilterMode filter, GraphicsFormat format, int antiAliasing, bool enableRandomWrite)
		{
			this.GetTemporaryRTArray(nameID, width, height, slices, depthBuffer, filter, format, antiAliasing, enableRandomWrite, false);
		}

		// Token: 0x06002F93 RID: 12179 RVA: 0x0005BA08 File Offset: 0x00059C08
		public void GetTemporaryRTArray(int nameID, int width, int height, int slices, int depthBuffer, FilterMode filter, GraphicsFormat format, int antiAliasing)
		{
			this.GetTemporaryRTArray(nameID, width, height, slices, depthBuffer, filter, format, antiAliasing, false);
		}

		// Token: 0x06002F94 RID: 12180 RVA: 0x0005BA2C File Offset: 0x00059C2C
		public void GetTemporaryRTArray(int nameID, int width, int height, int slices, int depthBuffer, FilterMode filter, GraphicsFormat format)
		{
			this.GetTemporaryRTArray(nameID, width, height, slices, depthBuffer, filter, format, 1);
		}

		// Token: 0x06002F95 RID: 12181 RVA: 0x0005BA50 File Offset: 0x00059C50
		public void GetTemporaryRTArray(int nameID, int width, int height, int slices, int depthBuffer, FilterMode filter, RenderTextureFormat format, RenderTextureReadWrite readWrite, int antiAliasing, bool enableRandomWrite)
		{
			ShadowSamplingMode shadowSamplingModeForFormat = RenderTexture.GetShadowSamplingModeForFormat(format);
			GraphicsFormat graphicsFormat = GraphicsFormatUtility.GetGraphicsFormat(format, readWrite);
			GraphicsFormat depthStencilFormatLegacy = RenderTexture.GetDepthStencilFormatLegacy(depthBuffer, format, false);
			this.GetTemporaryRTArray(nameID, width, height, slices, filter, graphicsFormat, depthStencilFormatLegacy, antiAliasing, enableRandomWrite, false, shadowSamplingModeForFormat);
		}

		// Token: 0x06002F96 RID: 12182 RVA: 0x0005BA94 File Offset: 0x00059C94
		public void GetTemporaryRTArray(int nameID, int width, int height, int slices, int depthBuffer, FilterMode filter, RenderTextureFormat format, RenderTextureReadWrite readWrite, int antiAliasing)
		{
			this.GetTemporaryRTArray(nameID, width, height, slices, depthBuffer, filter, format, readWrite, antiAliasing, false);
		}

		// Token: 0x06002F97 RID: 12183 RVA: 0x0005BABC File Offset: 0x00059CBC
		public void GetTemporaryRTArray(int nameID, int width, int height, int slices, int depthBuffer, FilterMode filter, RenderTextureFormat format, RenderTextureReadWrite readWrite)
		{
			this.GetTemporaryRTArray(nameID, width, height, slices, depthBuffer, filter, format, readWrite, 1);
		}

		// Token: 0x06002F98 RID: 12184 RVA: 0x0005BAE0 File Offset: 0x00059CE0
		public void GetTemporaryRTArray(int nameID, int width, int height, int slices, int depthBuffer, FilterMode filter, RenderTextureFormat format)
		{
			this.GetTemporaryRTArray(nameID, width, height, slices, depthBuffer, filter, format, RenderTextureReadWrite.Default);
		}

		// Token: 0x06002F99 RID: 12185 RVA: 0x0005BB01 File Offset: 0x00059D01
		public void GetTemporaryRTArray(int nameID, int width, int height, int slices, int depthBuffer, FilterMode filter)
		{
			this.GetTemporaryRTArray(nameID, width, height, slices, depthBuffer, filter, RenderTextureFormat.Default);
		}

		// Token: 0x06002F9A RID: 12186 RVA: 0x0005BB15 File Offset: 0x00059D15
		public void GetTemporaryRTArray(int nameID, int width, int height, int slices, int depthBuffer)
		{
			this.GetTemporaryRTArray(nameID, width, height, slices, depthBuffer, FilterMode.Point);
		}

		// Token: 0x06002F9B RID: 12187 RVA: 0x0005BB27 File Offset: 0x00059D27
		public void GetTemporaryRTArray(int nameID, int width, int height, int slices)
		{
			this.GetTemporaryRTArray(nameID, width, height, slices, 0);
		}

		// Token: 0x06002F9C RID: 12188 RVA: 0x0005BB38 File Offset: 0x00059D38
		[FreeFunction("RenderingCommandBuffer_Bindings::ReleaseTemporaryRT", HasExplicitThis = true)]
		public void ReleaseTemporaryRT(int nameID)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.ReleaseTemporaryRT_Injected(intPtr, nameID);
		}

		// Token: 0x06002F9D RID: 12189 RVA: 0x0005BB5B File Offset: 0x00059D5B
		public void ClearRenderTarget(bool clearDepth, bool clearColor, Color backgroundColor)
		{
			this.ClearRenderTarget(clearDepth, clearColor, backgroundColor, 1f, 0U);
		}

		// Token: 0x06002F9E RID: 12190 RVA: 0x0005BB6E File Offset: 0x00059D6E
		public void ClearRenderTarget(bool clearDepth, bool clearColor, Color backgroundColor, float depth)
		{
			this.ClearRenderTarget(clearDepth, clearColor, backgroundColor, depth, 0U);
		}

		// Token: 0x06002F9F RID: 12191 RVA: 0x0005BB7E File Offset: 0x00059D7E
		public void ClearRenderTarget(bool clearDepth, bool clearColor, Color backgroundColor, float depth = 1f, uint stencil = 0U)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.ClearRenderTargetSingle_Internal((clearColor ? RTClearFlags.Color : RTClearFlags.None) | (clearDepth ? RTClearFlags.DepthStencil : RTClearFlags.None), backgroundColor, depth, stencil);
		}

		// Token: 0x06002FA0 RID: 12192 RVA: 0x0005BBA5 File Offset: 0x00059DA5
		public void ClearRenderTarget(RTClearFlags clearFlags, Color backgroundColor, float depth = 1f, uint stencil = 0U)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.ClearRenderTargetSingle_Internal(clearFlags, backgroundColor, depth, stencil);
		}

		// Token: 0x06002FA1 RID: 12193 RVA: 0x0005BBC0 File Offset: 0x00059DC0
		public void ClearRenderTarget(RTClearFlags clearFlags, Color[] backgroundColors, float depth = 1f, uint stencil = 0U)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			bool flag = backgroundColors.Length < 1;
			if (flag)
			{
				throw new ArgumentException(string.Format("The number of clear colors must be at least 1, but is {0}", backgroundColors.Length));
			}
			bool flag2 = backgroundColors.Length > SystemInfo.supportedRenderTargetCount;
			if (flag2)
			{
				throw new ArgumentException(string.Format("The number of clear colors ({0}) exceeds the maximum supported number of render targets ({1})", backgroundColors.Length, SystemInfo.supportedRenderTargetCount));
			}
			this.ClearRenderTargetMulti_Internal(clearFlags, backgroundColors, depth, stencil);
		}

		// Token: 0x06002FA2 RID: 12194 RVA: 0x0005BC38 File Offset: 0x00059E38
		[FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalFloat", HasExplicitThis = true)]
		public void SetGlobalFloat(int nameID, float value)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetGlobalFloat_Injected(intPtr, nameID, value);
		}

		// Token: 0x06002FA3 RID: 12195 RVA: 0x0005BC5C File Offset: 0x00059E5C
		[FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalInt", HasExplicitThis = true)]
		public void SetGlobalInt(int nameID, int value)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetGlobalInt_Injected(intPtr, nameID, value);
		}

		// Token: 0x06002FA4 RID: 12196 RVA: 0x0005BC80 File Offset: 0x00059E80
		[FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalInteger", HasExplicitThis = true)]
		public void SetGlobalInteger(int nameID, int value)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetGlobalInteger_Injected(intPtr, nameID, value);
		}

		// Token: 0x06002FA5 RID: 12197 RVA: 0x0005BCA4 File Offset: 0x00059EA4
		[FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalVector", HasExplicitThis = true)]
		public void SetGlobalVector(int nameID, Vector4 value)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetGlobalVector_Injected(intPtr, nameID, ref value);
		}

		// Token: 0x06002FA6 RID: 12198 RVA: 0x0005BCCC File Offset: 0x00059ECC
		[FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalColor", HasExplicitThis = true)]
		public void SetGlobalColor(int nameID, Color value)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetGlobalColor_Injected(intPtr, nameID, ref value);
		}

		// Token: 0x06002FA7 RID: 12199 RVA: 0x0005BCF4 File Offset: 0x00059EF4
		[FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalMatrix", HasExplicitThis = true)]
		public void SetGlobalMatrix(int nameID, Matrix4x4 value)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetGlobalMatrix_Injected(intPtr, nameID, ref value);
		}

		// Token: 0x06002FA8 RID: 12200 RVA: 0x0005BD1C File Offset: 0x00059F1C
		[FreeFunction("RenderingCommandBuffer_Bindings::EnableShaderKeyword", HasExplicitThis = true)]
		public unsafe void EnableShaderKeyword(string keyword)
		{
			try
			{
				IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(keyword, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = keyword.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				CommandBuffer.EnableShaderKeyword_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06002FA9 RID: 12201 RVA: 0x0005BD80 File Offset: 0x00059F80
		[FreeFunction("RenderingCommandBuffer_Bindings::EnableShaderKeyword", HasExplicitThis = true)]
		private void EnableGlobalKeyword(GlobalKeyword keyword)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.EnableGlobalKeyword_Injected(intPtr, ref keyword);
		}

		// Token: 0x06002FAA RID: 12202 RVA: 0x0005BDA4 File Offset: 0x00059FA4
		[FreeFunction("RenderingCommandBuffer_Bindings::EnableMaterialKeyword", HasExplicitThis = true)]
		private void EnableMaterialKeyword(Material material, LocalKeyword keyword)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.EnableMaterialKeyword_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Material>(material), ref keyword);
		}

		// Token: 0x06002FAB RID: 12203 RVA: 0x0005BDD0 File Offset: 0x00059FD0
		[FreeFunction("RenderingCommandBuffer_Bindings::EnableComputeKeyword", HasExplicitThis = true)]
		private void EnableComputeKeyword(ComputeShader computeShader, LocalKeyword keyword)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.EnableComputeKeyword_Injected(intPtr, Object.MarshalledUnityObject.Marshal<ComputeShader>(computeShader), ref keyword);
		}

		// Token: 0x06002FAC RID: 12204 RVA: 0x0005BDFA File Offset: 0x00059FFA
		public void EnableKeyword(in GlobalKeyword keyword)
		{
			this.EnableGlobalKeyword(keyword);
		}

		// Token: 0x06002FAD RID: 12205 RVA: 0x0005BE0A File Offset: 0x0005A00A
		public void EnableKeyword(Material material, in LocalKeyword keyword)
		{
			this.EnableMaterialKeyword(material, keyword);
		}

		// Token: 0x06002FAE RID: 12206 RVA: 0x0005BE1B File Offset: 0x0005A01B
		public void EnableKeyword(ComputeShader computeShader, in LocalKeyword keyword)
		{
			this.EnableComputeKeyword(computeShader, keyword);
		}

		// Token: 0x06002FAF RID: 12207 RVA: 0x0005BE2C File Offset: 0x0005A02C
		[FreeFunction("RenderingCommandBuffer_Bindings::DisableShaderKeyword", HasExplicitThis = true)]
		public unsafe void DisableShaderKeyword(string keyword)
		{
			try
			{
				IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(keyword, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = keyword.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				CommandBuffer.DisableShaderKeyword_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06002FB0 RID: 12208 RVA: 0x0005BE90 File Offset: 0x0005A090
		[FreeFunction("RenderingCommandBuffer_Bindings::DisableShaderKeyword", HasExplicitThis = true)]
		private void DisableGlobalKeyword(GlobalKeyword keyword)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.DisableGlobalKeyword_Injected(intPtr, ref keyword);
		}

		// Token: 0x06002FB1 RID: 12209 RVA: 0x0005BEB4 File Offset: 0x0005A0B4
		[FreeFunction("RenderingCommandBuffer_Bindings::DisableMaterialKeyword", HasExplicitThis = true)]
		private void DisableMaterialKeyword(Material material, LocalKeyword keyword)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.DisableMaterialKeyword_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Material>(material), ref keyword);
		}

		// Token: 0x06002FB2 RID: 12210 RVA: 0x0005BEE0 File Offset: 0x0005A0E0
		[FreeFunction("RenderingCommandBuffer_Bindings::DisableComputeKeyword", HasExplicitThis = true)]
		private void DisableComputeKeyword(ComputeShader computeShader, LocalKeyword keyword)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.DisableComputeKeyword_Injected(intPtr, Object.MarshalledUnityObject.Marshal<ComputeShader>(computeShader), ref keyword);
		}

		// Token: 0x06002FB3 RID: 12211 RVA: 0x0005BF0A File Offset: 0x0005A10A
		public void DisableKeyword(in GlobalKeyword keyword)
		{
			this.DisableGlobalKeyword(keyword);
		}

		// Token: 0x06002FB4 RID: 12212 RVA: 0x0005BF1A File Offset: 0x0005A11A
		public void DisableKeyword(Material material, in LocalKeyword keyword)
		{
			this.DisableMaterialKeyword(material, keyword);
		}

		// Token: 0x06002FB5 RID: 12213 RVA: 0x0005BF2B File Offset: 0x0005A12B
		public void DisableKeyword(ComputeShader computeShader, in LocalKeyword keyword)
		{
			this.DisableComputeKeyword(computeShader, keyword);
		}

		// Token: 0x06002FB6 RID: 12214 RVA: 0x0005BF3C File Offset: 0x0005A13C
		[FreeFunction("RenderingCommandBuffer_Bindings::SetShaderKeyword", HasExplicitThis = true)]
		private void SetGlobalKeyword(GlobalKeyword keyword, bool value)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetGlobalKeyword_Injected(intPtr, ref keyword, value);
		}

		// Token: 0x06002FB7 RID: 12215 RVA: 0x0005BF64 File Offset: 0x0005A164
		[FreeFunction("RenderingCommandBuffer_Bindings::SetMaterialKeyword", HasExplicitThis = true)]
		private void SetMaterialKeyword(Material material, LocalKeyword keyword, bool value)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetMaterialKeyword_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Material>(material), ref keyword, value);
		}

		// Token: 0x06002FB8 RID: 12216 RVA: 0x0005BF90 File Offset: 0x0005A190
		[FreeFunction("RenderingCommandBuffer_Bindings::SetComputeKeyword", HasExplicitThis = true)]
		private void SetComputeKeyword(ComputeShader computeShader, LocalKeyword keyword, bool value)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetComputeKeyword_Injected(intPtr, Object.MarshalledUnityObject.Marshal<ComputeShader>(computeShader), ref keyword, value);
		}

		// Token: 0x06002FB9 RID: 12217 RVA: 0x0005BFBB File Offset: 0x0005A1BB
		public void SetKeyword(in GlobalKeyword keyword, bool value)
		{
			this.SetGlobalKeyword(keyword, value);
		}

		// Token: 0x06002FBA RID: 12218 RVA: 0x0005BFCC File Offset: 0x0005A1CC
		public void SetKeyword(Material material, in LocalKeyword keyword, bool value)
		{
			this.SetMaterialKeyword(material, keyword, value);
		}

		// Token: 0x06002FBB RID: 12219 RVA: 0x0005BFDE File Offset: 0x0005A1DE
		public void SetKeyword(ComputeShader computeShader, in LocalKeyword keyword, bool value)
		{
			this.SetComputeKeyword(computeShader, keyword, value);
		}

		// Token: 0x06002FBC RID: 12220 RVA: 0x0005BFF0 File Offset: 0x0005A1F0
		[FreeFunction("RenderingCommandBuffer_Bindings::SetViewMatrix", HasExplicitThis = true, ThrowsException = true)]
		public void SetViewMatrix(Matrix4x4 view)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetViewMatrix_Injected(intPtr, ref view);
		}

		// Token: 0x06002FBD RID: 12221 RVA: 0x0005C014 File Offset: 0x0005A214
		[FreeFunction("RenderingCommandBuffer_Bindings::SetProjectionMatrix", HasExplicitThis = true, ThrowsException = true)]
		public void SetProjectionMatrix(Matrix4x4 proj)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetProjectionMatrix_Injected(intPtr, ref proj);
		}

		// Token: 0x06002FBE RID: 12222 RVA: 0x0005C038 File Offset: 0x0005A238
		[FreeFunction("RenderingCommandBuffer_Bindings::SetViewProjectionMatrices", HasExplicitThis = true, ThrowsException = true)]
		public void SetViewProjectionMatrices(Matrix4x4 view, Matrix4x4 proj)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetViewProjectionMatrices_Injected(intPtr, ref view, ref proj);
		}

		// Token: 0x06002FBF RID: 12223 RVA: 0x0005C060 File Offset: 0x0005A260
		[NativeMethod("AddSetGlobalDepthBias")]
		public void SetGlobalDepthBias(float bias, float slopeBias)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetGlobalDepthBias_Injected(intPtr, bias, slopeBias);
		}

		// Token: 0x06002FC0 RID: 12224 RVA: 0x0005C084 File Offset: 0x0005A284
		[FreeFunction("RenderingCommandBuffer_Bindings::SetExecutionFlags", HasExplicitThis = true, ThrowsException = true)]
		public void SetExecutionFlags(CommandBufferExecutionFlags flags)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetExecutionFlags_Injected(intPtr, flags);
		}

		// Token: 0x06002FC1 RID: 12225 RVA: 0x0005C0A8 File Offset: 0x0005A2A8
		[FreeFunction("RenderingCommandBuffer_Bindings::ValidateAgainstExecutionFlags", HasExplicitThis = true, ThrowsException = true)]
		private bool ValidateAgainstExecutionFlags(CommandBufferExecutionFlags requiredFlags, CommandBufferExecutionFlags invalidFlags)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return CommandBuffer.ValidateAgainstExecutionFlags_Injected(intPtr, requiredFlags, invalidFlags);
		}

		// Token: 0x06002FC2 RID: 12226 RVA: 0x0005C0CC File Offset: 0x0005A2CC
		[FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalFloatArrayListImpl", HasExplicitThis = true)]
		private void SetGlobalFloatArrayListImpl(int nameID, object values)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetGlobalFloatArrayListImpl_Injected(intPtr, nameID, values);
		}

		// Token: 0x06002FC3 RID: 12227 RVA: 0x0005C0F0 File Offset: 0x0005A2F0
		[FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalVectorArrayListImpl", HasExplicitThis = true)]
		private void SetGlobalVectorArrayListImpl(int nameID, object values)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetGlobalVectorArrayListImpl_Injected(intPtr, nameID, values);
		}

		// Token: 0x06002FC4 RID: 12228 RVA: 0x0005C114 File Offset: 0x0005A314
		[FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalMatrixArrayListImpl", HasExplicitThis = true)]
		private void SetGlobalMatrixArrayListImpl(int nameID, object values)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetGlobalMatrixArrayListImpl_Injected(intPtr, nameID, values);
		}

		// Token: 0x06002FC5 RID: 12229 RVA: 0x0005C138 File Offset: 0x0005A338
		[FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalFloatArray", HasExplicitThis = true, ThrowsException = true)]
		public unsafe void SetGlobalFloatArray(int nameID, [NotNull] float[] values)
		{
			if (values == null)
			{
				ThrowHelper.ThrowArgumentNullException(values, "values");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<float> span = new Span<float>(values);
			fixed (float* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				CommandBuffer.SetGlobalFloatArray_Injected(intPtr, nameID, ref managedSpanWrapper);
			}
		}

		// Token: 0x06002FC6 RID: 12230 RVA: 0x0005C190 File Offset: 0x0005A390
		[FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalVectorArray", HasExplicitThis = true, ThrowsException = true)]
		public unsafe void SetGlobalVectorArray(int nameID, [NotNull] Vector4[] values)
		{
			if (values == null)
			{
				ThrowHelper.ThrowArgumentNullException(values, "values");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<Vector4> span = new Span<Vector4>(values);
			fixed (Vector4* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				CommandBuffer.SetGlobalVectorArray_Injected(intPtr, nameID, ref managedSpanWrapper);
			}
		}

		// Token: 0x06002FC7 RID: 12231 RVA: 0x0005C1E8 File Offset: 0x0005A3E8
		[FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalMatrixArray", HasExplicitThis = true, ThrowsException = true)]
		public unsafe void SetGlobalMatrixArray(int nameID, [NotNull] Matrix4x4[] values)
		{
			if (values == null)
			{
				ThrowHelper.ThrowArgumentNullException(values, "values");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<Matrix4x4> span = new Span<Matrix4x4>(values);
			fixed (Matrix4x4* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				CommandBuffer.SetGlobalMatrixArray_Injected(intPtr, nameID, ref managedSpanWrapper);
			}
		}

		// Token: 0x06002FC8 RID: 12232 RVA: 0x0005C240 File Offset: 0x0005A440
		[FreeFunction("RenderingCommandBuffer_Bindings::SetLateLatchProjectionMatrices", HasExplicitThis = true, ThrowsException = true)]
		public unsafe void SetLateLatchProjectionMatrices([NotNull] Matrix4x4[] projectionMat)
		{
			if (projectionMat == null)
			{
				ThrowHelper.ThrowArgumentNullException(projectionMat, "projectionMat");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<Matrix4x4> span = new Span<Matrix4x4>(projectionMat);
			fixed (Matrix4x4* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				CommandBuffer.SetLateLatchProjectionMatrices_Injected(intPtr, ref managedSpanWrapper);
			}
		}

		// Token: 0x06002FC9 RID: 12233 RVA: 0x0005C298 File Offset: 0x0005A498
		[FreeFunction("RenderingCommandBuffer_Bindings::MarkLateLatchMatrixShaderPropertyID", HasExplicitThis = true)]
		public void MarkLateLatchMatrixShaderPropertyID(CameraLateLatchMatrixType matrixPropertyType, int shaderPropertyID)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.MarkLateLatchMatrixShaderPropertyID_Injected(intPtr, matrixPropertyType, shaderPropertyID);
		}

		// Token: 0x06002FCA RID: 12234 RVA: 0x0005C2BC File Offset: 0x0005A4BC
		[FreeFunction("RenderingCommandBuffer_Bindings::UnmarkLateLatchMatrix", HasExplicitThis = true)]
		public void UnmarkLateLatchMatrix(CameraLateLatchMatrixType matrixPropertyType)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.UnmarkLateLatchMatrix_Injected(intPtr, matrixPropertyType);
		}

		// Token: 0x06002FCB RID: 12235 RVA: 0x0005C2E0 File Offset: 0x0005A4E0
		[FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalTexture_Impl", HasExplicitThis = true)]
		private void SetGlobalTexture_Impl(int nameID, ref RenderTargetIdentifier rt, RenderTextureSubElement element)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetGlobalTexture_Impl_Injected(intPtr, nameID, ref rt, element);
		}

		// Token: 0x06002FCC RID: 12236 RVA: 0x0005C308 File Offset: 0x0005A508
		[FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalBuffer", HasExplicitThis = true)]
		private void SetGlobalBufferInternal(int nameID, ComputeBuffer value)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetGlobalBufferInternal_Injected(intPtr, nameID, (value == null) ? ((IntPtr)0) : ComputeBuffer.BindingsMarshaller.ConvertToNative(value));
		}

		// Token: 0x06002FCD RID: 12237 RVA: 0x0005C33C File Offset: 0x0005A53C
		[FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalBuffer", HasExplicitThis = true)]
		private void SetGlobalGraphicsBufferInternal(int nameID, GraphicsBuffer value)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetGlobalGraphicsBufferInternal_Injected(intPtr, nameID, (value == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(value));
		}

		// Token: 0x06002FCE RID: 12238 RVA: 0x0005C370 File Offset: 0x0005A570
		[FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalRayTracingAccelerationStructure", HasExplicitThis = true)]
		private void SetGlobalRayTracingAccelerationStructureInternal(RayTracingAccelerationStructure accelerationStructure, int nameID)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetGlobalRayTracingAccelerationStructureInternal_Injected(intPtr, (accelerationStructure == null) ? ((IntPtr)0) : RayTracingAccelerationStructure.BindingsMarshaller.ConvertToNative(accelerationStructure), nameID);
		}

		// Token: 0x06002FCF RID: 12239 RVA: 0x0005C3A4 File Offset: 0x0005A5A4
		[FreeFunction("RenderingCommandBuffer_Bindings::SetShadowSamplingMode_Impl", HasExplicitThis = true)]
		private void SetShadowSamplingMode_Impl(ref RenderTargetIdentifier shadowmap, ShadowSamplingMode mode)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetShadowSamplingMode_Impl_Injected(intPtr, ref shadowmap, mode);
		}

		// Token: 0x06002FD0 RID: 12240 RVA: 0x0005C3C8 File Offset: 0x0005A5C8
		[FreeFunction("RenderingCommandBuffer_Bindings::IssuePluginEventInternal", HasExplicitThis = true)]
		private void IssuePluginEventInternal(IntPtr callback, int eventID)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.IssuePluginEventInternal_Injected(intPtr, callback, eventID);
		}

		// Token: 0x06002FD1 RID: 12241 RVA: 0x0005C3EC File Offset: 0x0005A5EC
		[FreeFunction("RenderingCommandBuffer_Bindings::BeginSample", HasExplicitThis = true)]
		public unsafe void BeginSample(string name)
		{
			try
			{
				IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
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
				CommandBuffer.BeginSample_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06002FD2 RID: 12242 RVA: 0x0005C450 File Offset: 0x0005A650
		[FreeFunction("RenderingCommandBuffer_Bindings::EndSample", HasExplicitThis = true)]
		public unsafe void EndSample(string name)
		{
			try
			{
				IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
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
				CommandBuffer.EndSample_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06002FD3 RID: 12243 RVA: 0x0005C4B4 File Offset: 0x0005A6B4
		public void BeginSample(CustomSampler sampler)
		{
			this.BeginSample_CustomSampler(sampler);
		}

		// Token: 0x06002FD4 RID: 12244 RVA: 0x0005C4BF File Offset: 0x0005A6BF
		public void EndSample(CustomSampler sampler)
		{
			this.EndSample_CustomSampler(sampler);
		}

		// Token: 0x06002FD5 RID: 12245 RVA: 0x0005C4CC File Offset: 0x0005A6CC
		[FreeFunction("RenderingCommandBuffer_Bindings::BeginSample_CustomSampler", HasExplicitThis = true)]
		private void BeginSample_CustomSampler([NotNull] CustomSampler sampler)
		{
			if (sampler == null)
			{
				ThrowHelper.ThrowArgumentNullException(sampler, "sampler");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = CustomSampler.BindingsMarshaller.ConvertToNative(sampler);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(sampler, "sampler");
			}
			CommandBuffer.BeginSample_CustomSampler_Injected(intPtr, intPtr2);
		}

		// Token: 0x06002FD6 RID: 12246 RVA: 0x0005C514 File Offset: 0x0005A714
		[FreeFunction("RenderingCommandBuffer_Bindings::EndSample_CustomSampler", HasExplicitThis = true)]
		private void EndSample_CustomSampler([NotNull] CustomSampler sampler)
		{
			if (sampler == null)
			{
				ThrowHelper.ThrowArgumentNullException(sampler, "sampler");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = CustomSampler.BindingsMarshaller.ConvertToNative(sampler);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(sampler, "sampler");
			}
			CommandBuffer.EndSample_CustomSampler_Injected(intPtr, intPtr2);
		}

		// Token: 0x06002FD7 RID: 12247 RVA: 0x0005C55A File Offset: 0x0005A75A
		[Conditional("ENABLE_PROFILER")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void BeginSample(ProfilerMarker marker)
		{
			this.BeginSample_ProfilerMarker(marker.Handle);
		}

		// Token: 0x06002FD8 RID: 12248 RVA: 0x0005C56B File Offset: 0x0005A76B
		[Conditional("ENABLE_PROFILER")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void EndSample(ProfilerMarker marker)
		{
			this.EndSample_ProfilerMarker(marker.Handle);
		}

		// Token: 0x06002FD9 RID: 12249 RVA: 0x0005C57C File Offset: 0x0005A77C
		[FreeFunction("RenderingCommandBuffer_Bindings::BeginSample_ProfilerMarker", HasExplicitThis = true, ThrowsException = true)]
		private void BeginSample_ProfilerMarker(IntPtr markerHandle)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.BeginSample_ProfilerMarker_Injected(intPtr, markerHandle);
		}

		// Token: 0x06002FDA RID: 12250 RVA: 0x0005C5A0 File Offset: 0x0005A7A0
		[FreeFunction("RenderingCommandBuffer_Bindings::EndSample_ProfilerMarker", HasExplicitThis = true, ThrowsException = true)]
		private void EndSample_ProfilerMarker(IntPtr markerHandle)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.EndSample_ProfilerMarker_Injected(intPtr, markerHandle);
		}

		// Token: 0x06002FDB RID: 12251 RVA: 0x0005C5C4 File Offset: 0x0005A7C4
		[FreeFunction("RenderingCommandBuffer_Bindings::IssuePluginEventAndDataInternal", HasExplicitThis = true)]
		private void IssuePluginEventAndDataInternal(IntPtr callback, int eventID, IntPtr data)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.IssuePluginEventAndDataInternal_Injected(intPtr, callback, eventID, data);
		}

		// Token: 0x06002FDC RID: 12252 RVA: 0x0005C5EC File Offset: 0x0005A7EC
		[FreeFunction("RenderingCommandBuffer_Bindings::IssuePluginEventAndDataWithFlagsInternal", HasExplicitThis = true)]
		private void IssuePluginEventAndDataInternalWithFlags(IntPtr callback, int eventID, CustomMarkerCallbackFlags flags, IntPtr data)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.IssuePluginEventAndDataInternalWithFlags_Injected(intPtr, callback, eventID, flags, data);
		}

		// Token: 0x06002FDD RID: 12253 RVA: 0x0005C614 File Offset: 0x0005A814
		[FreeFunction("RenderingCommandBuffer_Bindings::IssuePluginCustomBlitInternal", HasExplicitThis = true)]
		private void IssuePluginCustomBlitInternal(IntPtr callback, uint command, ref RenderTargetIdentifier source, ref RenderTargetIdentifier dest, uint commandParam, uint commandFlags)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.IssuePluginCustomBlitInternal_Injected(intPtr, callback, command, ref source, ref dest, commandParam, commandFlags);
		}

		// Token: 0x06002FDE RID: 12254 RVA: 0x0005C640 File Offset: 0x0005A840
		[FreeFunction("RenderingCommandBuffer_Bindings::IssuePluginCustomTextureUpdateInternal", HasExplicitThis = true)]
		private void IssuePluginCustomTextureUpdateInternal(IntPtr callback, Texture targetTexture, uint userData, bool useNewUnityRenderingExtTextureUpdateParamsV2)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.IssuePluginCustomTextureUpdateInternal_Injected(intPtr, callback, Object.MarshalledUnityObject.Marshal<Texture>(targetTexture), userData, useNewUnityRenderingExtTextureUpdateParamsV2);
		}

		// Token: 0x06002FDF RID: 12255 RVA: 0x0005C66C File Offset: 0x0005A86C
		[FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalConstantBuffer", HasExplicitThis = true)]
		private void SetGlobalConstantBufferInternal(ComputeBuffer buffer, int nameID, int offset, int size)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetGlobalConstantBufferInternal_Injected(intPtr, (buffer == null) ? ((IntPtr)0) : ComputeBuffer.BindingsMarshaller.ConvertToNative(buffer), nameID, offset, size);
		}

		// Token: 0x06002FE0 RID: 12256 RVA: 0x0005C6A4 File Offset: 0x0005A8A4
		[FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalConstantBuffer", HasExplicitThis = true)]
		private void SetGlobalConstantGraphicsBufferInternal(GraphicsBuffer buffer, int nameID, int offset, int size)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetGlobalConstantGraphicsBufferInternal_Injected(intPtr, (buffer == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(buffer), nameID, offset, size);
		}

		// Token: 0x06002FE1 RID: 12257 RVA: 0x0005C6DC File Offset: 0x0005A8DC
		[FreeFunction("RenderingCommandBuffer_Bindings::IncrementUpdateCount", HasExplicitThis = true)]
		public void IncrementUpdateCount(RenderTargetIdentifier dest)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.IncrementUpdateCount_Injected(intPtr, ref dest);
		}

		// Token: 0x06002FE2 RID: 12258 RVA: 0x0005C700 File Offset: 0x0005A900
		[FreeFunction("RenderingCommandBuffer_Bindings::SetInstanceMultiplier", HasExplicitThis = true)]
		public void SetInstanceMultiplier(uint multiplier)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetInstanceMultiplier_Injected(intPtr, multiplier);
		}

		// Token: 0x06002FE3 RID: 12259 RVA: 0x0005C724 File Offset: 0x0005A924
		[FreeFunction("RenderingCommandBuffer_Bindings::SetFoveatedRenderingMode", HasExplicitThis = true)]
		public void SetFoveatedRenderingMode(FoveatedRenderingMode foveatedRenderingMode)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetFoveatedRenderingMode_Injected(intPtr, foveatedRenderingMode);
		}

		// Token: 0x06002FE4 RID: 12260 RVA: 0x0005C748 File Offset: 0x0005A948
		[FreeFunction("RenderingCommandBuffer_Bindings::SetWireframe", HasExplicitThis = true)]
		public void SetWireframe(bool enable)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetWireframe_Injected(intPtr, enable);
		}

		// Token: 0x06002FE5 RID: 12261 RVA: 0x0005C76C File Offset: 0x0005A96C
		[FreeFunction("RenderingCommandBuffer_Bindings::ConfigureFoveatedRendering", HasExplicitThis = true)]
		public void ConfigureFoveatedRendering(IntPtr platformData)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.ConfigureFoveatedRendering_Injected(intPtr, platformData);
		}

		// Token: 0x06002FE6 RID: 12262 RVA: 0x0005C790 File Offset: 0x0005A990
		private static void CheckThrowOnSetRenderTarget()
		{
			bool throwOnSetRenderTarget = CommandBuffer.ThrowOnSetRenderTarget;
			if (throwOnSetRenderTarget)
			{
				throw new Exception("Setrendertarget is not allowed in this context");
			}
		}

		// Token: 0x06002FE7 RID: 12263 RVA: 0x0005C7B2 File Offset: 0x0005A9B2
		public void SetRenderTarget(RenderTargetIdentifier rt)
		{
			CommandBuffer.CheckThrowOnSetRenderTarget();
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.SetRenderTargetSingle_Internal(rt, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store);
		}

		// Token: 0x06002FE8 RID: 12264 RVA: 0x0005C7D0 File Offset: 0x0005A9D0
		public void SetRenderTarget(RenderTargetIdentifier rt, RenderBufferLoadAction loadAction, RenderBufferStoreAction storeAction)
		{
			CommandBuffer.CheckThrowOnSetRenderTarget();
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			bool flag = loadAction == RenderBufferLoadAction.Clear;
			if (flag)
			{
				throw new ArgumentException("RenderBufferLoadAction.Clear is not supported");
			}
			this.SetRenderTargetSingle_Internal(rt, loadAction, storeAction, loadAction, storeAction);
		}

		// Token: 0x06002FE9 RID: 12265 RVA: 0x0005C80C File Offset: 0x0005AA0C
		public void SetRenderTarget(RenderTargetIdentifier rt, RenderBufferLoadAction colorLoadAction, RenderBufferStoreAction colorStoreAction, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction)
		{
			CommandBuffer.CheckThrowOnSetRenderTarget();
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			bool flag = colorLoadAction == RenderBufferLoadAction.Clear || depthLoadAction == RenderBufferLoadAction.Clear;
			if (flag)
			{
				throw new ArgumentException("RenderBufferLoadAction.Clear is not supported");
			}
			this.SetRenderTargetSingle_Internal(rt, colorLoadAction, colorStoreAction, depthLoadAction, depthStoreAction);
		}

		// Token: 0x06002FEA RID: 12266 RVA: 0x0005C854 File Offset: 0x0005AA54
		public void SetRenderTarget(RenderTargetIdentifier rt, int mipLevel)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			bool flag = mipLevel < 0;
			if (flag)
			{
				throw new ArgumentException(string.Format("Invalid value for mipLevel ({0})", mipLevel));
			}
			this.SetRenderTargetSingle_Internal(new RenderTargetIdentifier(rt, mipLevel, CubemapFace.Unknown, 0), RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store);
		}

		// Token: 0x06002FEB RID: 12267 RVA: 0x0005C8A0 File Offset: 0x0005AAA0
		public void SetRenderTarget(RenderTargetIdentifier rt, int mipLevel, CubemapFace cubemapFace)
		{
			CommandBuffer.CheckThrowOnSetRenderTarget();
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			bool flag = mipLevel < 0;
			if (flag)
			{
				throw new ArgumentException(string.Format("Invalid value for mipLevel ({0})", mipLevel));
			}
			this.SetRenderTargetSingle_Internal(new RenderTargetIdentifier(rt, mipLevel, cubemapFace, 0), RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store);
		}

		// Token: 0x06002FEC RID: 12268 RVA: 0x0005C8F0 File Offset: 0x0005AAF0
		public void SetRenderTarget(RenderTargetIdentifier rt, int mipLevel, CubemapFace cubemapFace, int depthSlice)
		{
			CommandBuffer.CheckThrowOnSetRenderTarget();
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			bool flag = depthSlice < -1;
			if (flag)
			{
				throw new ArgumentException(string.Format("Invalid value for depthSlice ({0})", depthSlice));
			}
			bool flag2 = mipLevel < 0;
			if (flag2)
			{
				throw new ArgumentException(string.Format("Invalid value for mipLevel ({0})", mipLevel));
			}
			this.SetRenderTargetSingle_Internal(new RenderTargetIdentifier(rt, mipLevel, cubemapFace, depthSlice), RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store);
		}

		// Token: 0x06002FED RID: 12269 RVA: 0x0005C960 File Offset: 0x0005AB60
		public void SetRenderTarget(RenderTargetIdentifier color, RenderTargetIdentifier depth)
		{
			CommandBuffer.CheckThrowOnSetRenderTarget();
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.SetRenderTargetColorDepth_Internal(color, depth, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, RenderTargetFlags.None);
		}

		// Token: 0x06002FEE RID: 12270 RVA: 0x0005C980 File Offset: 0x0005AB80
		public void SetRenderTarget(RenderTargetIdentifier color, RenderTargetIdentifier depth, int mipLevel)
		{
			CommandBuffer.CheckThrowOnSetRenderTarget();
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			bool flag = mipLevel < 0;
			if (flag)
			{
				throw new ArgumentException(string.Format("Invalid value for mipLevel ({0})", mipLevel));
			}
			this.SetRenderTargetColorDepth_Internal(new RenderTargetIdentifier(color, mipLevel, CubemapFace.Unknown, 0), depth, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, RenderTargetFlags.None);
		}

		// Token: 0x06002FEF RID: 12271 RVA: 0x0005C9D4 File Offset: 0x0005ABD4
		public void SetRenderTarget(RenderTargetIdentifier color, RenderTargetIdentifier depth, int mipLevel, CubemapFace cubemapFace)
		{
			CommandBuffer.CheckThrowOnSetRenderTarget();
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			bool flag = mipLevel < 0;
			if (flag)
			{
				throw new ArgumentException(string.Format("Invalid value for mipLevel ({0})", mipLevel));
			}
			this.SetRenderTargetColorDepth_Internal(new RenderTargetIdentifier(color, mipLevel, cubemapFace, 0), depth, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, RenderTargetFlags.None);
		}

		// Token: 0x06002FF0 RID: 12272 RVA: 0x0005CA28 File Offset: 0x0005AC28
		public void SetRenderTarget(RenderTargetIdentifier color, RenderTargetIdentifier depth, int mipLevel, CubemapFace cubemapFace, int depthSlice)
		{
			CommandBuffer.CheckThrowOnSetRenderTarget();
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			bool flag = depthSlice < -1;
			if (flag)
			{
				throw new ArgumentException(string.Format("Invalid value for depthSlice ({0})", depthSlice));
			}
			bool flag2 = mipLevel < 0;
			if (flag2)
			{
				throw new ArgumentException(string.Format("Invalid value for mipLevel ({0})", mipLevel));
			}
			this.SetRenderTargetColorDepth_Internal(new RenderTargetIdentifier(color, mipLevel, cubemapFace, depthSlice), depth, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, RenderTargetFlags.None);
		}

		// Token: 0x06002FF1 RID: 12273 RVA: 0x0005CA9C File Offset: 0x0005AC9C
		public void SetRenderTarget(RenderTargetIdentifier color, RenderBufferLoadAction colorLoadAction, RenderBufferStoreAction colorStoreAction, RenderTargetIdentifier depth, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction)
		{
			CommandBuffer.CheckThrowOnSetRenderTarget();
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			bool flag = colorLoadAction == RenderBufferLoadAction.Clear || depthLoadAction == RenderBufferLoadAction.Clear;
			if (flag)
			{
				throw new ArgumentException("RenderBufferLoadAction.Clear is not supported");
			}
			this.SetRenderTargetColorDepth_Internal(color, depth, colorLoadAction, colorStoreAction, depthLoadAction, depthStoreAction, RenderTargetFlags.None);
		}

		// Token: 0x06002FF2 RID: 12274 RVA: 0x0005CAE8 File Offset: 0x0005ACE8
		public void SetRenderTarget(RenderTargetIdentifier[] colors, RenderTargetIdentifier depth)
		{
			CommandBuffer.CheckThrowOnSetRenderTarget();
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			bool flag = colors.Length < 1;
			if (flag)
			{
				throw new ArgumentException(string.Format("colors.Length must be at least 1, but was {0}", colors.Length));
			}
			bool flag2 = colors.Length > SystemInfo.supportedRenderTargetCount;
			if (flag2)
			{
				throw new ArgumentException(string.Format("colors.Length is {0} and exceeds the maximum number of supported render targets ({1})", colors.Length, SystemInfo.supportedRenderTargetCount));
			}
			this.SetRenderTargetMulti_Internal(colors, depth, null, null, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, RenderTargetFlags.None);
		}

		// Token: 0x06002FF3 RID: 12275 RVA: 0x0005CB68 File Offset: 0x0005AD68
		public void SetRenderTarget(RenderTargetIdentifier[] colors, RenderTargetIdentifier depth, int mipLevel, CubemapFace cubemapFace, int depthSlice)
		{
			CommandBuffer.CheckThrowOnSetRenderTarget();
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			bool flag = colors.Length < 1;
			if (flag)
			{
				throw new ArgumentException(string.Format("colors.Length must be at least 1, but was {0}", colors.Length));
			}
			bool flag2 = colors.Length > SystemInfo.supportedRenderTargetCount;
			if (flag2)
			{
				throw new ArgumentException(string.Format("colors.Length is {0} and exceeds the maximum number of supported render targets ({1})", colors.Length, SystemInfo.supportedRenderTargetCount));
			}
			this.SetRenderTargetMultiSubtarget(colors, depth, null, null, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, mipLevel, cubemapFace, depthSlice);
		}

		// Token: 0x06002FF4 RID: 12276 RVA: 0x0005CBEC File Offset: 0x0005ADEC
		public void SetRenderTarget(RenderTargetBinding binding, int mipLevel, CubemapFace cubemapFace, int depthSlice)
		{
			CommandBuffer.CheckThrowOnSetRenderTarget();
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			bool flag = binding.colorRenderTargets.Length < 1;
			if (flag)
			{
				throw new ArgumentException(string.Format("The number of color render targets must be at least 1, but was {0}", binding.colorRenderTargets.Length));
			}
			bool flag2 = binding.colorRenderTargets.Length > SystemInfo.supportedRenderTargetCount;
			if (flag2)
			{
				throw new ArgumentException(string.Format("The number of color render targets ({0}) and exceeds the maximum supported number of render targets ({1})", binding.colorRenderTargets.Length, SystemInfo.supportedRenderTargetCount));
			}
			bool flag3 = binding.colorLoadActions.Length != binding.colorRenderTargets.Length;
			if (flag3)
			{
				throw new ArgumentException(string.Format("The number of color load actions provided ({0}) does not match the number of color render targets ({1})", binding.colorLoadActions.Length, binding.colorRenderTargets.Length));
			}
			bool flag4 = binding.colorStoreActions.Length != binding.colorRenderTargets.Length;
			if (flag4)
			{
				throw new ArgumentException(string.Format("The number of color store actions provided ({0}) does not match the number of color render targets ({1})", binding.colorLoadActions.Length, binding.colorRenderTargets.Length));
			}
			bool flag5 = binding.depthLoadAction == RenderBufferLoadAction.Clear || Array.IndexOf<RenderBufferLoadAction>(binding.colorLoadActions, RenderBufferLoadAction.Clear) > -1;
			if (flag5)
			{
				throw new ArgumentException("RenderBufferLoadAction.Clear is not supported");
			}
			bool flag6 = binding.colorRenderTargets.Length == 1;
			if (flag6)
			{
				this.SetRenderTargetColorDepthSubtarget(binding.colorRenderTargets[0], binding.depthRenderTarget, binding.colorLoadActions[0], binding.colorStoreActions[0], binding.depthLoadAction, binding.depthStoreAction, mipLevel, cubemapFace, depthSlice);
			}
			else
			{
				this.SetRenderTargetMultiSubtarget(binding.colorRenderTargets, binding.depthRenderTarget, binding.colorLoadActions, binding.colorStoreActions, binding.depthLoadAction, binding.depthStoreAction, mipLevel, cubemapFace, depthSlice);
			}
		}

		// Token: 0x06002FF5 RID: 12277 RVA: 0x0005CDB8 File Offset: 0x0005AFB8
		public void SetRenderTarget(RenderTargetBinding binding)
		{
			CommandBuffer.CheckThrowOnSetRenderTarget();
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			bool flag = binding.colorRenderTargets.Length < 1;
			if (flag)
			{
				throw new ArgumentException(string.Format("The number of color render targets must be at least 1, but was {0}", binding.colorRenderTargets.Length));
			}
			bool flag2 = binding.colorRenderTargets.Length > SystemInfo.supportedRenderTargetCount;
			if (flag2)
			{
				throw new ArgumentException(string.Format("The number of color render targets ({0}) and exceeds the maximum supported number of render targets ({1})", binding.colorRenderTargets.Length, SystemInfo.supportedRenderTargetCount));
			}
			bool flag3 = binding.colorLoadActions.Length != binding.colorRenderTargets.Length;
			if (flag3)
			{
				throw new ArgumentException(string.Format("The number of color load actions provided ({0}) does not match the number of color render targets ({1})", binding.colorLoadActions.Length, binding.colorRenderTargets.Length));
			}
			bool flag4 = binding.colorStoreActions.Length != binding.colorRenderTargets.Length;
			if (flag4)
			{
				throw new ArgumentException(string.Format("The number of color store actions provided ({0}) does not match the number of color render targets ({1})", binding.colorLoadActions.Length, binding.colorRenderTargets.Length));
			}
			bool flag5 = binding.depthLoadAction == RenderBufferLoadAction.Clear || Array.IndexOf<RenderBufferLoadAction>(binding.colorLoadActions, RenderBufferLoadAction.Clear) > -1;
			if (flag5)
			{
				throw new ArgumentException("RenderBufferLoadAction.Clear is not supported");
			}
			bool flag6 = binding.colorRenderTargets.Length == 1;
			if (flag6)
			{
				this.SetRenderTargetColorDepth_Internal(binding.colorRenderTargets[0], binding.depthRenderTarget, binding.colorLoadActions[0], binding.colorStoreActions[0], binding.depthLoadAction, binding.depthStoreAction, binding.flags);
			}
			else
			{
				this.SetRenderTargetMulti_Internal(binding.colorRenderTargets, binding.depthRenderTarget, binding.colorLoadActions, binding.colorStoreActions, binding.depthLoadAction, binding.depthStoreAction, binding.flags);
			}
		}

		// Token: 0x06002FF6 RID: 12278 RVA: 0x0005CF8C File Offset: 0x0005B18C
		private void ClearRenderTargetSingle_Internal(RTClearFlags clearFlags, Color color, float depth, uint stencil)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.ClearRenderTargetSingle_Internal_Injected(intPtr, clearFlags, ref color, depth, stencil);
		}

		// Token: 0x06002FF7 RID: 12279 RVA: 0x0005CFB4 File Offset: 0x0005B1B4
		private unsafe void ClearRenderTargetMulti_Internal(RTClearFlags clearFlags, Color[] colors, float depth, uint stencil)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<Color> span = new Span<Color>(colors);
			fixed (Color* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				CommandBuffer.ClearRenderTargetMulti_Internal_Injected(intPtr, clearFlags, ref managedSpanWrapper, depth, stencil);
			}
		}

		// Token: 0x06002FF8 RID: 12280 RVA: 0x0005D000 File Offset: 0x0005B200
		private void SetRenderTargetSingle_Internal(RenderTargetIdentifier rt, RenderBufferLoadAction colorLoadAction, RenderBufferStoreAction colorStoreAction, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetRenderTargetSingle_Internal_Injected(intPtr, ref rt, colorLoadAction, colorStoreAction, depthLoadAction, depthStoreAction);
		}

		// Token: 0x06002FF9 RID: 12281 RVA: 0x0005D02C File Offset: 0x0005B22C
		private void SetRenderTargetColorDepth_Internal(RenderTargetIdentifier color, RenderTargetIdentifier depth, RenderBufferLoadAction colorLoadAction, RenderBufferStoreAction colorStoreAction, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction, RenderTargetFlags flags)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetRenderTargetColorDepth_Internal_Injected(intPtr, ref color, ref depth, colorLoadAction, colorStoreAction, depthLoadAction, depthStoreAction, flags);
		}

		// Token: 0x06002FFA RID: 12282 RVA: 0x0005D05C File Offset: 0x0005B25C
		private unsafe void SetRenderTargetMulti_Internal(RenderTargetIdentifier[] colors, RenderTargetIdentifier depth, RenderBufferLoadAction[] colorLoadActions, RenderBufferStoreAction[] colorStoreActions, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction, RenderTargetFlags flags)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<RenderTargetIdentifier> span = new Span<RenderTargetIdentifier>(colors);
			fixed (RenderTargetIdentifier* ptr = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, span.Length);
				Span<RenderBufferLoadAction> span2 = new Span<RenderBufferLoadAction>(colorLoadActions);
				fixed (RenderBufferLoadAction* ptr2 = span2.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, span2.Length);
					Span<RenderBufferStoreAction> span3 = new Span<RenderBufferStoreAction>(colorStoreActions);
					fixed (RenderBufferStoreAction* pinnableReference = span3.GetPinnableReference())
					{
						ManagedSpanWrapper managedSpanWrapper3 = new ManagedSpanWrapper((void*)pinnableReference, span3.Length);
						CommandBuffer.SetRenderTargetMulti_Internal_Injected(intPtr, ref managedSpanWrapper, ref depth, ref managedSpanWrapper2, ref managedSpanWrapper3, depthLoadAction, depthStoreAction, flags);
						ptr = null;
						ptr2 = null;
					}
				}
			}
		}

		// Token: 0x06002FFB RID: 12283 RVA: 0x0005D100 File Offset: 0x0005B300
		private void SetRenderTargetColorDepthSubtarget(RenderTargetIdentifier color, RenderTargetIdentifier depth, RenderBufferLoadAction colorLoadAction, RenderBufferStoreAction colorStoreAction, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction, int mipLevel, CubemapFace cubemapFace, int depthSlice)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.SetRenderTargetColorDepthSubtarget_Injected(intPtr, ref color, ref depth, colorLoadAction, colorStoreAction, depthLoadAction, depthStoreAction, mipLevel, cubemapFace, depthSlice);
		}

		// Token: 0x06002FFC RID: 12284 RVA: 0x0005D134 File Offset: 0x0005B334
		private unsafe void SetRenderTargetMultiSubtarget(RenderTargetIdentifier[] colors, RenderTargetIdentifier depth, RenderBufferLoadAction[] colorLoadActions, RenderBufferStoreAction[] colorStoreActions, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction, int mipLevel, CubemapFace cubemapFace, int depthSlice)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<RenderTargetIdentifier> span = new Span<RenderTargetIdentifier>(colors);
			fixed (RenderTargetIdentifier* ptr = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, span.Length);
				Span<RenderBufferLoadAction> span2 = new Span<RenderBufferLoadAction>(colorLoadActions);
				fixed (RenderBufferLoadAction* ptr2 = span2.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, span2.Length);
					Span<RenderBufferStoreAction> span3 = new Span<RenderBufferStoreAction>(colorStoreActions);
					fixed (RenderBufferStoreAction* pinnableReference = span3.GetPinnableReference())
					{
						ManagedSpanWrapper managedSpanWrapper3 = new ManagedSpanWrapper((void*)pinnableReference, span3.Length);
						CommandBuffer.SetRenderTargetMultiSubtarget_Injected(intPtr, ref managedSpanWrapper, ref depth, ref managedSpanWrapper2, ref managedSpanWrapper3, depthLoadAction, depthStoreAction, mipLevel, cubemapFace, depthSlice);
						ptr = null;
						ptr2 = null;
					}
				}
			}
		}

		// Token: 0x06002FFD RID: 12285 RVA: 0x0005D1DC File Offset: 0x0005B3DC
		[NativeMethod("ProcessVTFeedback")]
		private void Internal_ProcessVTFeedback(RenderTargetIdentifier rt, IntPtr resolver, int slice, int x, int width, int y, int height, int mip)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.Internal_ProcessVTFeedback_Injected(intPtr, ref rt, resolver, slice, x, width, y, height, mip);
		}

		// Token: 0x06002FFE RID: 12286 RVA: 0x0005D20C File Offset: 0x0005B40C
		[SecuritySafeCritical]
		public void SetBufferData(ComputeBuffer buffer, Array data)
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			bool flag2 = !UnsafeUtility.IsArrayBlittable(data);
			if (flag2)
			{
				throw new ArgumentException(string.Format("Array passed to RenderingCommandBuffer.SetBufferData(array) must be blittable.\n{0}", UnsafeUtility.GetReasonForArrayNonBlittable(data)));
			}
			this.InternalSetComputeBufferData(buffer, data, 0, 0, data.Length, UnsafeUtility.SizeOf(data.GetType().GetElementType()));
		}

		// Token: 0x06002FFF RID: 12287 RVA: 0x0005D274 File Offset: 0x0005B474
		[SecuritySafeCritical]
		public void SetBufferData<T>(ComputeBuffer buffer, List<T> data) where T : struct
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			bool flag2 = !UnsafeUtility.IsGenericListBlittable<T>();
			if (flag2)
			{
				throw new ArgumentException(string.Format("List<{0}> passed to RenderingCommandBuffer.SetBufferData(List<>) must be blittable.\n{1}", typeof(T), UnsafeUtility.GetReasonForGenericListNonBlittable<T>()));
			}
			this.InternalSetComputeBufferData(buffer, NoAllocHelpers.ExtractArrayFromList<T>(data), 0, 0, NoAllocHelpers.SafeLength<T>(data), Marshal.SizeOf(typeof(T)));
		}

		// Token: 0x06003000 RID: 12288 RVA: 0x0005D2E6 File Offset: 0x0005B4E6
		[SecuritySafeCritical]
		public void SetBufferData<T>(ComputeBuffer buffer, NativeArray<T> data) where T : struct
		{
			this.InternalSetComputeBufferNativeData(buffer, (IntPtr)data.GetUnsafeReadOnlyPtr<T>(), 0, 0, data.Length, UnsafeUtility.SizeOf<T>());
		}

		// Token: 0x06003001 RID: 12289 RVA: 0x0005D310 File Offset: 0x0005B510
		[SecuritySafeCritical]
		public void SetBufferData(ComputeBuffer buffer, Array data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count)
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			bool flag2 = !UnsafeUtility.IsArrayBlittable(data);
			if (flag2)
			{
				throw new ArgumentException(string.Format("Array passed to RenderingCommandBuffer.SetBufferData(array) must be blittable.\n{0}", UnsafeUtility.GetReasonForArrayNonBlittable(data)));
			}
			bool flag3 = managedBufferStartIndex < 0 || graphicsBufferStartIndex < 0 || count < 0 || managedBufferStartIndex + count > data.Length;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad indices/count arguments (managedBufferStartIndex:{0} graphicsBufferStartIndex:{1} count:{2})", managedBufferStartIndex, graphicsBufferStartIndex, count));
			}
			this.InternalSetComputeBufferData(buffer, data, managedBufferStartIndex, graphicsBufferStartIndex, count, Marshal.SizeOf(data.GetType().GetElementType()));
		}

		// Token: 0x06003002 RID: 12290 RVA: 0x0005D3B8 File Offset: 0x0005B5B8
		[SecuritySafeCritical]
		public void SetBufferData<T>(ComputeBuffer buffer, List<T> data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count) where T : struct
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			bool flag2 = !UnsafeUtility.IsGenericListBlittable<T>();
			if (flag2)
			{
				throw new ArgumentException(string.Format("List<{0}> passed to RenderingCommandBuffer.SetBufferData(List<>) must be blittable.\n{1}", typeof(T), UnsafeUtility.GetReasonForGenericListNonBlittable<T>()));
			}
			bool flag3 = managedBufferStartIndex < 0 || graphicsBufferStartIndex < 0 || count < 0 || managedBufferStartIndex + count > data.Count;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad indices/count arguments (managedBufferStartIndex:{0} graphicsBufferStartIndex:{1} count:{2})", managedBufferStartIndex, graphicsBufferStartIndex, count));
			}
			this.InternalSetComputeBufferData(buffer, NoAllocHelpers.ExtractArrayFromList<T>(data), managedBufferStartIndex, graphicsBufferStartIndex, count, Marshal.SizeOf(typeof(T)));
		}

		// Token: 0x06003003 RID: 12291 RVA: 0x0005D46C File Offset: 0x0005B66C
		[SecuritySafeCritical]
		public void SetBufferData<T>(ComputeBuffer buffer, NativeArray<T> data, int nativeBufferStartIndex, int graphicsBufferStartIndex, int count) where T : struct
		{
			bool flag = nativeBufferStartIndex < 0 || graphicsBufferStartIndex < 0 || count < 0 || nativeBufferStartIndex + count > data.Length;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad indices/count arguments (nativeBufferStartIndex:{0} graphicsBufferStartIndex:{1} count:{2})", nativeBufferStartIndex, graphicsBufferStartIndex, count));
			}
			this.InternalSetComputeBufferNativeData(buffer, (IntPtr)data.GetUnsafeReadOnlyPtr<T>(), nativeBufferStartIndex, graphicsBufferStartIndex, count, UnsafeUtility.SizeOf<T>());
		}

		// Token: 0x06003004 RID: 12292 RVA: 0x0005D4E0 File Offset: 0x0005B6E0
		public void SetBufferCounterValue(ComputeBuffer buffer, uint counterValue)
		{
			this.InternalSetComputeBufferCounterValue(buffer, counterValue);
		}

		// Token: 0x06003005 RID: 12293 RVA: 0x0005D4EC File Offset: 0x0005B6EC
		[FreeFunction(Name = "RenderingCommandBuffer_Bindings::InternalSetGraphicsBufferNativeData", HasExplicitThis = true, ThrowsException = true)]
		private void InternalSetComputeBufferNativeData([NotNull] ComputeBuffer buffer, IntPtr data, int nativeBufferStartIndex, int graphicsBufferStartIndex, int count, int elemSize)
		{
			if (buffer == null)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = ComputeBuffer.BindingsMarshaller.ConvertToNative(buffer);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			CommandBuffer.InternalSetComputeBufferNativeData_Injected(intPtr, intPtr2, data, nativeBufferStartIndex, graphicsBufferStartIndex, count, elemSize);
		}

		// Token: 0x06003006 RID: 12294 RVA: 0x0005D53C File Offset: 0x0005B73C
		[FreeFunction(Name = "RenderingCommandBuffer_Bindings::InternalSetGraphicsBufferData", HasExplicitThis = true, ThrowsException = true)]
		private void InternalSetComputeBufferData([NotNull] ComputeBuffer buffer, Array data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count, int elemSize)
		{
			if (buffer == null)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = ComputeBuffer.BindingsMarshaller.ConvertToNative(buffer);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			CommandBuffer.InternalSetComputeBufferData_Injected(intPtr, intPtr2, data, managedBufferStartIndex, graphicsBufferStartIndex, count, elemSize);
		}

		// Token: 0x06003007 RID: 12295 RVA: 0x0005D58C File Offset: 0x0005B78C
		[FreeFunction(Name = "RenderingCommandBuffer_Bindings::InternalSetGraphicsBufferCounterValue", HasExplicitThis = true)]
		private void InternalSetComputeBufferCounterValue([NotNull] ComputeBuffer buffer, uint counterValue)
		{
			if (buffer == null)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = ComputeBuffer.BindingsMarshaller.ConvertToNative(buffer);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			CommandBuffer.InternalSetComputeBufferCounterValue_Injected(intPtr, intPtr2, counterValue);
		}

		// Token: 0x06003008 RID: 12296 RVA: 0x0005D5D4 File Offset: 0x0005B7D4
		[SecuritySafeCritical]
		public void SetBufferData(GraphicsBuffer buffer, Array data)
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			bool flag2 = !UnsafeUtility.IsArrayBlittable(data);
			if (flag2)
			{
				throw new ArgumentException(string.Format("Array passed to RenderingCommandBuffer.SetBufferData(array) must be blittable.\n{0}", UnsafeUtility.GetReasonForArrayNonBlittable(data)));
			}
			this.InternalSetGraphicsBufferData(buffer, data, 0, 0, data.Length, UnsafeUtility.SizeOf(data.GetType().GetElementType()));
		}

		// Token: 0x06003009 RID: 12297 RVA: 0x0005D63C File Offset: 0x0005B83C
		[SecuritySafeCritical]
		public void SetBufferData<T>(GraphicsBuffer buffer, List<T> data) where T : struct
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			bool flag2 = !UnsafeUtility.IsGenericListBlittable<T>();
			if (flag2)
			{
				throw new ArgumentException(string.Format("List<{0}> passed to RenderingCommandBuffer.SetBufferData(List<>) must be blittable.\n{1}", typeof(T), UnsafeUtility.GetReasonForGenericListNonBlittable<T>()));
			}
			this.InternalSetGraphicsBufferData(buffer, NoAllocHelpers.ExtractArrayFromList<T>(data), 0, 0, NoAllocHelpers.SafeLength<T>(data), Marshal.SizeOf(typeof(T)));
		}

		// Token: 0x0600300A RID: 12298 RVA: 0x0005D6AE File Offset: 0x0005B8AE
		[SecuritySafeCritical]
		public void SetBufferData<T>(GraphicsBuffer buffer, NativeArray<T> data) where T : struct
		{
			this.InternalSetGraphicsBufferNativeData(buffer, (IntPtr)data.GetUnsafeReadOnlyPtr<T>(), 0, 0, data.Length, UnsafeUtility.SizeOf<T>());
		}

		// Token: 0x0600300B RID: 12299 RVA: 0x0005D6D8 File Offset: 0x0005B8D8
		[SecuritySafeCritical]
		public void SetBufferData(GraphicsBuffer buffer, Array data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count)
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			bool flag2 = !UnsafeUtility.IsArrayBlittable(data);
			if (flag2)
			{
				throw new ArgumentException(string.Format("Array passed to RenderingCommandBuffer.SetBufferData(array) must be blittable.\n{0}", UnsafeUtility.GetReasonForArrayNonBlittable(data)));
			}
			bool flag3 = managedBufferStartIndex < 0 || graphicsBufferStartIndex < 0 || count < 0 || managedBufferStartIndex + count > data.Length;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad indices/count arguments (managedBufferStartIndex:{0} graphicsBufferStartIndex:{1} count:{2})", managedBufferStartIndex, graphicsBufferStartIndex, count));
			}
			this.InternalSetGraphicsBufferData(buffer, data, managedBufferStartIndex, graphicsBufferStartIndex, count, Marshal.SizeOf(data.GetType().GetElementType()));
		}

		// Token: 0x0600300C RID: 12300 RVA: 0x0005D780 File Offset: 0x0005B980
		[SecuritySafeCritical]
		public void SetBufferData<T>(GraphicsBuffer buffer, List<T> data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count) where T : struct
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			bool flag2 = !UnsafeUtility.IsGenericListBlittable<T>();
			if (flag2)
			{
				throw new ArgumentException(string.Format("List<{0}> passed to RenderingCommandBuffer.SetBufferData(List<>) must be blittable.\n{1}", typeof(T), UnsafeUtility.GetReasonForGenericListNonBlittable<T>()));
			}
			bool flag3 = managedBufferStartIndex < 0 || graphicsBufferStartIndex < 0 || count < 0 || managedBufferStartIndex + count > data.Count;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad indices/count arguments (managedBufferStartIndex:{0} graphicsBufferStartIndex:{1} count:{2})", managedBufferStartIndex, graphicsBufferStartIndex, count));
			}
			this.InternalSetGraphicsBufferData(buffer, NoAllocHelpers.ExtractArrayFromList<T>(data), managedBufferStartIndex, graphicsBufferStartIndex, count, Marshal.SizeOf(typeof(T)));
		}

		// Token: 0x0600300D RID: 12301 RVA: 0x0005D834 File Offset: 0x0005BA34
		[SecuritySafeCritical]
		public void SetBufferData<T>(GraphicsBuffer buffer, NativeArray<T> data, int nativeBufferStartIndex, int graphicsBufferStartIndex, int count) where T : struct
		{
			bool flag = nativeBufferStartIndex < 0 || graphicsBufferStartIndex < 0 || count < 0 || nativeBufferStartIndex + count > data.Length;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad indices/count arguments (nativeBufferStartIndex:{0} graphicsBufferStartIndex:{1} count:{2})", nativeBufferStartIndex, graphicsBufferStartIndex, count));
			}
			this.InternalSetGraphicsBufferNativeData(buffer, (IntPtr)data.GetUnsafeReadOnlyPtr<T>(), nativeBufferStartIndex, graphicsBufferStartIndex, count, UnsafeUtility.SizeOf<T>());
		}

		// Token: 0x0600300E RID: 12302 RVA: 0x0005D8A8 File Offset: 0x0005BAA8
		public void SetBufferCounterValue(GraphicsBuffer buffer, uint counterValue)
		{
			this.InternalSetGraphicsBufferCounterValue(buffer, counterValue);
		}

		// Token: 0x0600300F RID: 12303 RVA: 0x0005D8B4 File Offset: 0x0005BAB4
		[FreeFunction(Name = "RenderingCommandBuffer_Bindings::InternalSetGraphicsBufferNativeData", HasExplicitThis = true, ThrowsException = true)]
		private void InternalSetGraphicsBufferNativeData([NotNull] GraphicsBuffer buffer, IntPtr data, int nativeBufferStartIndex, int graphicsBufferStartIndex, int count, int elemSize)
		{
			if (buffer == null)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = GraphicsBuffer.BindingsMarshaller.ConvertToNative(buffer);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			CommandBuffer.InternalSetGraphicsBufferNativeData_Injected(intPtr, intPtr2, data, nativeBufferStartIndex, graphicsBufferStartIndex, count, elemSize);
		}

		// Token: 0x06003010 RID: 12304 RVA: 0x0005D904 File Offset: 0x0005BB04
		[FreeFunction(Name = "RenderingCommandBuffer_Bindings::InternalSetGraphicsBufferData", HasExplicitThis = true, ThrowsException = true)]
		[SecurityCritical]
		private void InternalSetGraphicsBufferData([NotNull] GraphicsBuffer buffer, Array data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count, int elemSize)
		{
			if (buffer == null)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = GraphicsBuffer.BindingsMarshaller.ConvertToNative(buffer);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			CommandBuffer.InternalSetGraphicsBufferData_Injected(intPtr, intPtr2, data, managedBufferStartIndex, graphicsBufferStartIndex, count, elemSize);
		}

		// Token: 0x06003011 RID: 12305 RVA: 0x0005D954 File Offset: 0x0005BB54
		[FreeFunction(Name = "RenderingCommandBuffer_Bindings::InternalSetGraphicsBufferCounterValue", HasExplicitThis = true)]
		private void InternalSetGraphicsBufferCounterValue([NotNull] GraphicsBuffer buffer, uint counterValue)
		{
			if (buffer == null)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = GraphicsBuffer.BindingsMarshaller.ConvertToNative(buffer);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			CommandBuffer.InternalSetGraphicsBufferCounterValue_Injected(intPtr, intPtr2, counterValue);
		}

		// Token: 0x06003012 RID: 12306 RVA: 0x0005D99C File Offset: 0x0005BB9C
		[FreeFunction(Name = "RenderingCommandBuffer_Bindings::CopyBuffer", HasExplicitThis = true, ThrowsException = true)]
		private void CopyBufferImpl([NotNull] GraphicsBuffer source, [NotNull] GraphicsBuffer dest)
		{
			if (source == null)
			{
				ThrowHelper.ThrowArgumentNullException(source, "source");
			}
			if (dest == null)
			{
				ThrowHelper.ThrowArgumentNullException(dest, "dest");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = GraphicsBuffer.BindingsMarshaller.ConvertToNative(source);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(source, "source");
			}
			IntPtr intPtr3 = GraphicsBuffer.BindingsMarshaller.ConvertToNative(dest);
			if (intPtr3 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(dest, "dest");
			}
			CommandBuffer.CopyBufferImpl_Injected(intPtr, intPtr2, intPtr3);
		}

		// Token: 0x06003013 RID: 12307 RVA: 0x0005DA08 File Offset: 0x0005BC08
		[FreeFunction("RenderingCommandBuffer_Bindings::BeginRenderPass", HasExplicitThis = true)]
		private unsafe void BeginRenderPass_Internal(int width, int height, int volumeDepth, int samples, ReadOnlySpan<AttachmentDescriptor> attachments, int depthAttachmentIndex, ReadOnlySpan<SubPassDescriptor> subPasses, ReadOnlySpan<byte> debugNameUtf8)
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ReadOnlySpan<AttachmentDescriptor> readOnlySpan = attachments;
			fixed (AttachmentDescriptor* ptr = readOnlySpan.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
				ReadOnlySpan<SubPassDescriptor> readOnlySpan2 = subPasses;
				fixed (SubPassDescriptor* ptr2 = readOnlySpan2.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, readOnlySpan2.Length);
					ReadOnlySpan<byte> readOnlySpan3 = debugNameUtf8;
					fixed (byte* pinnableReference = readOnlySpan3.GetPinnableReference())
					{
						ManagedSpanWrapper managedSpanWrapper3 = new ManagedSpanWrapper((void*)pinnableReference, readOnlySpan3.Length);
						CommandBuffer.BeginRenderPass_Internal_Injected(intPtr, width, height, volumeDepth, samples, ref managedSpanWrapper, depthAttachmentIndex, ref managedSpanWrapper2, ref managedSpanWrapper3);
						ptr = null;
						ptr2 = null;
					}
				}
			}
		}

		// Token: 0x06003014 RID: 12308 RVA: 0x0005DA9C File Offset: 0x0005BC9C
		public void BeginRenderPass(int width, int height, int samples, NativeArray<AttachmentDescriptor> attachments, int depthAttachmentIndex, NativeArray<SubPassDescriptor> subPasses)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.BeginRenderPass_Internal(width, height, 1, samples, in attachments, depthAttachmentIndex, in subPasses, default(ReadOnlySpan<byte>));
		}

		// Token: 0x06003015 RID: 12309 RVA: 0x0005DAD8 File Offset: 0x0005BCD8
		public void BeginRenderPass(int width, int height, int volumeDepth, int samples, NativeArray<AttachmentDescriptor> attachments, int depthAttachmentIndex, NativeArray<SubPassDescriptor> subPasses)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.BeginRenderPass_Internal(width, height, volumeDepth, samples, in attachments, depthAttachmentIndex, in subPasses, default(ReadOnlySpan<byte>));
		}

		// Token: 0x06003016 RID: 12310 RVA: 0x0005DB14 File Offset: 0x0005BD14
		public void BeginRenderPass(int width, int height, int samples, NativeArray<AttachmentDescriptor> attachments, int depthAttachmentIndex, NativeArray<SubPassDescriptor> subPasses, ReadOnlySpan<byte> debugNameUtf8)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.BeginRenderPass_Internal(width, height, 1, samples, in attachments, depthAttachmentIndex, in subPasses, debugNameUtf8);
		}

		// Token: 0x06003017 RID: 12311 RVA: 0x0005DB48 File Offset: 0x0005BD48
		public void BeginRenderPass(int width, int height, int volumeDepth, int samples, NativeArray<AttachmentDescriptor> attachments, int depthAttachmentIndex, NativeArray<SubPassDescriptor> subPasses, ReadOnlySpan<byte> debugNameUtf8)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.BeginRenderPass_Internal(width, height, volumeDepth, samples, in attachments, depthAttachmentIndex, in subPasses, debugNameUtf8);
		}

		// Token: 0x06003018 RID: 12312 RVA: 0x0005DB80 File Offset: 0x0005BD80
		[FreeFunction("RenderingCommandBuffer_Bindings::NextSubPass", HasExplicitThis = true)]
		private void NextSubPass_Internal()
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.NextSubPass_Internal_Injected(intPtr);
		}

		// Token: 0x06003019 RID: 12313 RVA: 0x0005DBA2 File Offset: 0x0005BDA2
		public void NextSubPass()
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.NextSubPass_Internal();
		}

		// Token: 0x0600301A RID: 12314 RVA: 0x0005DBB8 File Offset: 0x0005BDB8
		[FreeFunction("RenderingCommandBuffer_Bindings::EndRenderPass", HasExplicitThis = true)]
		private void EndRenderPass_Internal()
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.EndRenderPass_Internal_Injected(intPtr);
		}

		// Token: 0x0600301B RID: 12315 RVA: 0x0005DBDA File Offset: 0x0005BDDA
		public void EndRenderPass()
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.EndRenderPass_Internal();
		}

		// Token: 0x0600301C RID: 12316 RVA: 0x0005DBF0 File Offset: 0x0005BDF0
		[FreeFunction("RenderingCommandBuffer_Bindings::SetupCameraProperties", HasExplicitThis = true)]
		private void SetupCameraProperties_Internal([NotNull] Camera camera)
		{
			if (camera == null)
			{
				ThrowHelper.ThrowArgumentNullException(camera, "camera");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Camera>(camera);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(camera, "camera");
			}
			CommandBuffer.SetupCameraProperties_Internal_Injected(intPtr, intPtr2);
		}

		// Token: 0x0600301D RID: 12317 RVA: 0x0005DC36 File Offset: 0x0005BE36
		public void SetupCameraProperties(Camera camera)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.SetupCameraProperties_Internal(camera);
		}

		// Token: 0x0600301E RID: 12318 RVA: 0x0005DC4C File Offset: 0x0005BE4C
		[FreeFunction("RenderingCommandBuffer_Bindings::InvokeOnRenderObjectCallbacks", HasExplicitThis = true)]
		private void InvokeOnRenderObjectCallbacks_Internal()
		{
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CommandBuffer.InvokeOnRenderObjectCallbacks_Internal_Injected(intPtr);
		}

		// Token: 0x0600301F RID: 12319 RVA: 0x0005DC6E File Offset: 0x0005BE6E
		public void InvokeOnRenderObjectCallbacks()
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.InvokeOnRenderObjectCallbacks_Internal();
		}

		// Token: 0x06003020 RID: 12320 RVA: 0x0005DC81 File Offset: 0x0005BE81
		private CommandBuffer(IntPtr ptr)
		{
			this.m_Ptr = ptr;
		}

		// Token: 0x06003021 RID: 12321 RVA: 0x0005DC94 File Offset: 0x0005BE94
		~CommandBuffer()
		{
			this.Dispose(false);
		}

		// Token: 0x06003022 RID: 12322 RVA: 0x0005DCC8 File Offset: 0x0005BEC8
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06003023 RID: 12323 RVA: 0x0005DCDA File Offset: 0x0005BEDA
		private void Dispose(bool disposing)
		{
			this.ReleaseBuffer();
			this.m_Ptr = IntPtr.Zero;
		}

		// Token: 0x06003024 RID: 12324 RVA: 0x0005DCEF File Offset: 0x0005BEEF
		public CommandBuffer()
		{
			this.m_Ptr = CommandBuffer.InitBuffer();
		}

		// Token: 0x06003025 RID: 12325 RVA: 0x0005DD04 File Offset: 0x0005BF04
		public void Release()
		{
			this.Dispose();
		}

		// Token: 0x06003026 RID: 12326 RVA: 0x0005DD10 File Offset: 0x0005BF10
		public GraphicsFence CreateAsyncGraphicsFence()
		{
			return this.CreateGraphicsFence(GraphicsFenceType.AsyncQueueSynchronisation, SynchronisationStageFlags.PixelProcessing);
		}

		// Token: 0x06003027 RID: 12327 RVA: 0x0005DD2C File Offset: 0x0005BF2C
		public GraphicsFence CreateAsyncGraphicsFence(SynchronisationStage stage)
		{
			return this.CreateGraphicsFence(GraphicsFenceType.AsyncQueueSynchronisation, GraphicsFence.TranslateSynchronizationStageToFlags(stage));
		}

		// Token: 0x06003028 RID: 12328 RVA: 0x0005DD4C File Offset: 0x0005BF4C
		public GraphicsFence CreateGraphicsFence(GraphicsFenceType fenceType, SynchronisationStageFlags stage)
		{
			GraphicsFence graphicsFence = default(GraphicsFence);
			graphicsFence.m_FenceType = fenceType;
			graphicsFence.m_Ptr = this.CreateGPUFence_Internal(fenceType, stage);
			graphicsFence.InitPostAllocation();
			graphicsFence.Validate();
			return graphicsFence;
		}

		// Token: 0x06003029 RID: 12329 RVA: 0x0005DD8E File Offset: 0x0005BF8E
		public void WaitOnAsyncGraphicsFence(GraphicsFence fence)
		{
			this.WaitOnAsyncGraphicsFence(fence, SynchronisationStage.VertexProcessing);
		}

		// Token: 0x0600302A RID: 12330 RVA: 0x0005DD9A File Offset: 0x0005BF9A
		public void WaitOnAsyncGraphicsFence(GraphicsFence fence, SynchronisationStage stage)
		{
			this.WaitOnAsyncGraphicsFence(fence, GraphicsFence.TranslateSynchronizationStageToFlags(stage));
		}

		// Token: 0x0600302B RID: 12331 RVA: 0x0005DDAC File Offset: 0x0005BFAC
		public void WaitOnAsyncGraphicsFence(GraphicsFence fence, SynchronisationStageFlags stage)
		{
			bool flag = fence.m_FenceType > GraphicsFenceType.AsyncQueueSynchronisation;
			if (flag)
			{
				throw new ArgumentException("Attempting to call WaitOnAsyncGPUFence on a fence that is not of GraphicsFenceType.AsyncQueueSynchronization");
			}
			fence.Validate();
			bool flag2 = fence.IsFencePending();
			if (flag2)
			{
				this.WaitOnGPUFence_Internal(fence.m_Ptr, stage);
			}
		}

		// Token: 0x0600302C RID: 12332 RVA: 0x0005DDF3 File Offset: 0x0005BFF3
		public void SetComputeFloatParam(ComputeShader computeShader, string name, float val)
		{
			this.SetComputeFloatParam(computeShader, Shader.PropertyToID(name), val);
		}

		// Token: 0x0600302D RID: 12333 RVA: 0x0005DE05 File Offset: 0x0005C005
		public void SetComputeIntParam(ComputeShader computeShader, string name, int val)
		{
			this.SetComputeIntParam(computeShader, Shader.PropertyToID(name), val);
		}

		// Token: 0x0600302E RID: 12334 RVA: 0x0005DE17 File Offset: 0x0005C017
		public void SetComputeVectorParam(ComputeShader computeShader, string name, Vector4 val)
		{
			this.SetComputeVectorParam(computeShader, Shader.PropertyToID(name), val);
		}

		// Token: 0x0600302F RID: 12335 RVA: 0x0005DE29 File Offset: 0x0005C029
		public void SetComputeVectorArrayParam(ComputeShader computeShader, string name, Vector4[] values)
		{
			this.SetComputeVectorArrayParam(computeShader, Shader.PropertyToID(name), values);
		}

		// Token: 0x06003030 RID: 12336 RVA: 0x0005DE3B File Offset: 0x0005C03B
		public void SetComputeMatrixParam(ComputeShader computeShader, string name, Matrix4x4 val)
		{
			this.SetComputeMatrixParam(computeShader, Shader.PropertyToID(name), val);
		}

		// Token: 0x06003031 RID: 12337 RVA: 0x0005DE4D File Offset: 0x0005C04D
		public void SetComputeMatrixArrayParam(ComputeShader computeShader, string name, Matrix4x4[] values)
		{
			this.SetComputeMatrixArrayParam(computeShader, Shader.PropertyToID(name), values);
		}

		// Token: 0x06003032 RID: 12338 RVA: 0x0005DE5F File Offset: 0x0005C05F
		public void SetComputeFloatParams(ComputeShader computeShader, string name, params float[] values)
		{
			this.Internal_SetComputeFloats(computeShader, Shader.PropertyToID(name), values);
		}

		// Token: 0x06003033 RID: 12339 RVA: 0x0005DE71 File Offset: 0x0005C071
		public void SetComputeFloatParams(ComputeShader computeShader, int nameID, params float[] values)
		{
			this.Internal_SetComputeFloats(computeShader, nameID, values);
		}

		// Token: 0x06003034 RID: 12340 RVA: 0x0005DE7E File Offset: 0x0005C07E
		public void SetComputeIntParams(ComputeShader computeShader, string name, params int[] values)
		{
			this.Internal_SetComputeInts(computeShader, Shader.PropertyToID(name), values);
		}

		// Token: 0x06003035 RID: 12341 RVA: 0x0005DE90 File Offset: 0x0005C090
		public void SetComputeIntParams(ComputeShader computeShader, int nameID, params int[] values)
		{
			this.Internal_SetComputeInts(computeShader, nameID, values);
		}

		// Token: 0x06003036 RID: 12342 RVA: 0x0005DE9D File Offset: 0x0005C09D
		public void SetComputeTextureParam(ComputeShader computeShader, int kernelIndex, string name, RenderTargetIdentifier rt)
		{
			this.Internal_SetComputeTextureParam(computeShader, kernelIndex, Shader.PropertyToID(name), ref rt, 0, RenderTextureSubElement.Default);
		}

		// Token: 0x06003037 RID: 12343 RVA: 0x0005DEB3 File Offset: 0x0005C0B3
		public void SetComputeTextureParam(ComputeShader computeShader, int kernelIndex, int nameID, RenderTargetIdentifier rt)
		{
			this.Internal_SetComputeTextureParam(computeShader, kernelIndex, nameID, ref rt, 0, RenderTextureSubElement.Default);
		}

		// Token: 0x06003038 RID: 12344 RVA: 0x0005DEC4 File Offset: 0x0005C0C4
		public void SetComputeTextureParam(ComputeShader computeShader, int kernelIndex, string name, RenderTargetIdentifier rt, int mipLevel)
		{
			this.Internal_SetComputeTextureParam(computeShader, kernelIndex, Shader.PropertyToID(name), ref rt, mipLevel, RenderTextureSubElement.Default);
		}

		// Token: 0x06003039 RID: 12345 RVA: 0x0005DEDB File Offset: 0x0005C0DB
		public void SetComputeTextureParam(ComputeShader computeShader, int kernelIndex, int nameID, RenderTargetIdentifier rt, int mipLevel)
		{
			this.Internal_SetComputeTextureParam(computeShader, kernelIndex, nameID, ref rt, mipLevel, RenderTextureSubElement.Default);
		}

		// Token: 0x0600303A RID: 12346 RVA: 0x0005DEED File Offset: 0x0005C0ED
		public void SetComputeTextureParam(ComputeShader computeShader, int kernelIndex, string name, RenderTargetIdentifier rt, int mipLevel, RenderTextureSubElement element)
		{
			this.Internal_SetComputeTextureParam(computeShader, kernelIndex, Shader.PropertyToID(name), ref rt, mipLevel, element);
		}

		// Token: 0x0600303B RID: 12347 RVA: 0x0005DF05 File Offset: 0x0005C105
		public void SetComputeTextureParam(ComputeShader computeShader, int kernelIndex, int nameID, RenderTargetIdentifier rt, int mipLevel, RenderTextureSubElement element)
		{
			this.Internal_SetComputeTextureParam(computeShader, kernelIndex, nameID, ref rt, mipLevel, element);
		}

		// Token: 0x0600303C RID: 12348 RVA: 0x0005DF18 File Offset: 0x0005C118
		public void SetComputeBufferParam(ComputeShader computeShader, int kernelIndex, int nameID, ComputeBuffer buffer)
		{
			this.Internal_SetComputeBufferParam(computeShader, kernelIndex, nameID, buffer);
		}

		// Token: 0x0600303D RID: 12349 RVA: 0x0005DF27 File Offset: 0x0005C127
		public void SetComputeBufferParam(ComputeShader computeShader, int kernelIndex, string name, ComputeBuffer buffer)
		{
			this.Internal_SetComputeBufferParam(computeShader, kernelIndex, Shader.PropertyToID(name), buffer);
		}

		// Token: 0x0600303E RID: 12350 RVA: 0x0005DF3B File Offset: 0x0005C13B
		public void SetComputeBufferParam(ComputeShader computeShader, int kernelIndex, int nameID, GraphicsBufferHandle bufferHandle)
		{
			this.Internal_SetComputeGraphicsBufferHandleParam(computeShader, kernelIndex, nameID, bufferHandle);
		}

		// Token: 0x0600303F RID: 12351 RVA: 0x0005DF4A File Offset: 0x0005C14A
		public void SetComputeBufferParam(ComputeShader computeShader, int kernelIndex, string name, GraphicsBufferHandle bufferHandle)
		{
			this.Internal_SetComputeGraphicsBufferHandleParam(computeShader, kernelIndex, Shader.PropertyToID(name), bufferHandle);
		}

		// Token: 0x06003040 RID: 12352 RVA: 0x0005DF5E File Offset: 0x0005C15E
		public void SetComputeBufferParam(ComputeShader computeShader, int kernelIndex, int nameID, GraphicsBuffer buffer)
		{
			this.Internal_SetComputeGraphicsBufferParam(computeShader, kernelIndex, nameID, buffer);
		}

		// Token: 0x06003041 RID: 12353 RVA: 0x0005DF6D File Offset: 0x0005C16D
		public void SetComputeBufferParam(ComputeShader computeShader, int kernelIndex, string name, GraphicsBuffer buffer)
		{
			this.Internal_SetComputeGraphicsBufferParam(computeShader, kernelIndex, Shader.PropertyToID(name), buffer);
		}

		// Token: 0x06003042 RID: 12354 RVA: 0x0005DF81 File Offset: 0x0005C181
		public void SetComputeConstantBufferParam(ComputeShader computeShader, int nameID, ComputeBuffer buffer, int offset, int size)
		{
			this.Internal_SetComputeConstantComputeBufferParam(computeShader, nameID, buffer, offset, size);
		}

		// Token: 0x06003043 RID: 12355 RVA: 0x0005DF92 File Offset: 0x0005C192
		public void SetComputeConstantBufferParam(ComputeShader computeShader, string name, ComputeBuffer buffer, int offset, int size)
		{
			this.Internal_SetComputeConstantComputeBufferParam(computeShader, Shader.PropertyToID(name), buffer, offset, size);
		}

		// Token: 0x06003044 RID: 12356 RVA: 0x0005DFA8 File Offset: 0x0005C1A8
		public void SetComputeConstantBufferParam(ComputeShader computeShader, int nameID, GraphicsBuffer buffer, int offset, int size)
		{
			this.Internal_SetComputeConstantGraphicsBufferParam(computeShader, nameID, buffer, offset, size);
		}

		// Token: 0x06003045 RID: 12357 RVA: 0x0005DFB9 File Offset: 0x0005C1B9
		public void SetComputeConstantBufferParam(ComputeShader computeShader, string name, GraphicsBuffer buffer, int offset, int size)
		{
			this.Internal_SetComputeConstantGraphicsBufferParam(computeShader, Shader.PropertyToID(name), buffer, offset, size);
		}

		// Token: 0x06003046 RID: 12358 RVA: 0x0005DFCF File Offset: 0x0005C1CF
		public void SetComputeParamsFromMaterial(ComputeShader computeShader, int kernelIndex, Material material)
		{
			this.Internal_SetComputeParamsFromMaterial(computeShader, kernelIndex, material);
		}

		// Token: 0x06003047 RID: 12359 RVA: 0x0005DFDC File Offset: 0x0005C1DC
		public void DispatchCompute(ComputeShader computeShader, int kernelIndex, int threadGroupsX, int threadGroupsY, int threadGroupsZ)
		{
			this.Internal_DispatchCompute(computeShader, kernelIndex, threadGroupsX, threadGroupsY, threadGroupsZ);
		}

		// Token: 0x06003048 RID: 12360 RVA: 0x0005DFF0 File Offset: 0x0005C1F0
		public void DispatchCompute(ComputeShader computeShader, int kernelIndex, ComputeBuffer indirectBuffer, uint argsOffset)
		{
			bool flag = SystemInfo.graphicsDeviceType == GraphicsDeviceType.Metal && !SystemInfo.supportsIndirectArgumentsBuffer;
			if (flag)
			{
				throw new InvalidOperationException("Indirect argument buffers are not supported.");
			}
			this.Internal_DispatchComputeIndirect(computeShader, kernelIndex, indirectBuffer, argsOffset);
		}

		// Token: 0x06003049 RID: 12361 RVA: 0x0005E030 File Offset: 0x0005C230
		public void DispatchCompute(ComputeShader computeShader, int kernelIndex, GraphicsBuffer indirectBuffer, uint argsOffset)
		{
			bool flag = SystemInfo.graphicsDeviceType == GraphicsDeviceType.Metal && !SystemInfo.supportsIndirectArgumentsBuffer;
			if (flag)
			{
				throw new InvalidOperationException("Indirect argument buffers are not supported.");
			}
			this.Internal_DispatchComputeIndirectGraphicsBuffer(computeShader, kernelIndex, indirectBuffer, argsOffset);
		}

		// Token: 0x0600304A RID: 12362 RVA: 0x0005E070 File Offset: 0x0005C270
		public void BuildRayTracingAccelerationStructure(RayTracingAccelerationStructure accelerationStructure)
		{
			RayTracingAccelerationStructure.BuildSettings buildSettings = new RayTracingAccelerationStructure.BuildSettings
			{
				buildFlags = RayTracingAccelerationStructureBuildFlags.PreferFastTrace,
				relativeOrigin = Vector3.zero
			};
			this.Internal_BuildRayTracingAccelerationStructure(accelerationStructure, buildSettings);
		}

		// Token: 0x0600304B RID: 12363 RVA: 0x0005E0A8 File Offset: 0x0005C2A8
		public void BuildRayTracingAccelerationStructure(RayTracingAccelerationStructure accelerationStructure, Vector3 relativeOrigin)
		{
			RayTracingAccelerationStructure.BuildSettings buildSettings = new RayTracingAccelerationStructure.BuildSettings
			{
				buildFlags = RayTracingAccelerationStructureBuildFlags.PreferFastTrace,
				relativeOrigin = relativeOrigin
			};
			this.Internal_BuildRayTracingAccelerationStructure(accelerationStructure, buildSettings);
		}

		// Token: 0x0600304C RID: 12364 RVA: 0x0005E0DA File Offset: 0x0005C2DA
		public void BuildRayTracingAccelerationStructure(RayTracingAccelerationStructure accelerationStructure, RayTracingAccelerationStructure.BuildSettings buildSettings)
		{
			this.Internal_BuildRayTracingAccelerationStructure(accelerationStructure, buildSettings);
		}

		// Token: 0x0600304D RID: 12365 RVA: 0x0005E0E6 File Offset: 0x0005C2E6
		public void SetRayTracingAccelerationStructure(RayTracingShader rayTracingShader, string name, RayTracingAccelerationStructure rayTracingAccelerationStructure)
		{
			this.Internal_SetRayTracingAccelerationStructure(rayTracingShader, Shader.PropertyToID(name), rayTracingAccelerationStructure);
		}

		// Token: 0x0600304E RID: 12366 RVA: 0x0005E0F8 File Offset: 0x0005C2F8
		public void SetRayTracingAccelerationStructure(RayTracingShader rayTracingShader, int nameID, RayTracingAccelerationStructure rayTracingAccelerationStructure)
		{
			this.Internal_SetRayTracingAccelerationStructure(rayTracingShader, nameID, rayTracingAccelerationStructure);
		}

		// Token: 0x0600304F RID: 12367 RVA: 0x0005E105 File Offset: 0x0005C305
		public void SetRayTracingAccelerationStructure(ComputeShader computeShader, int kernelIndex, string name, RayTracingAccelerationStructure rayTracingAccelerationStructure)
		{
			this.Internal_SetComputeRayTracingAccelerationStructure(computeShader, kernelIndex, Shader.PropertyToID(name), rayTracingAccelerationStructure);
		}

		// Token: 0x06003050 RID: 12368 RVA: 0x0005E119 File Offset: 0x0005C319
		public void SetRayTracingAccelerationStructure(ComputeShader computeShader, int kernelIndex, int nameID, RayTracingAccelerationStructure rayTracingAccelerationStructure)
		{
			this.Internal_SetComputeRayTracingAccelerationStructure(computeShader, kernelIndex, nameID, rayTracingAccelerationStructure);
		}

		// Token: 0x06003051 RID: 12369 RVA: 0x0005E128 File Offset: 0x0005C328
		public void SetRayTracingBufferParam(RayTracingShader rayTracingShader, string name, ComputeBuffer buffer)
		{
			this.Internal_SetRayTracingComputeBufferParam(rayTracingShader, Shader.PropertyToID(name), buffer);
		}

		// Token: 0x06003052 RID: 12370 RVA: 0x0005E13A File Offset: 0x0005C33A
		public void SetRayTracingBufferParam(RayTracingShader rayTracingShader, int nameID, ComputeBuffer buffer)
		{
			this.Internal_SetRayTracingComputeBufferParam(rayTracingShader, nameID, buffer);
		}

		// Token: 0x06003053 RID: 12371 RVA: 0x0005E147 File Offset: 0x0005C347
		public void SetRayTracingBufferParam(RayTracingShader rayTracingShader, string name, GraphicsBuffer buffer)
		{
			this.Internal_SetRayTracingGraphicsBufferParam(rayTracingShader, Shader.PropertyToID(name), buffer);
		}

		// Token: 0x06003054 RID: 12372 RVA: 0x0005E159 File Offset: 0x0005C359
		public void SetRayTracingBufferParam(RayTracingShader rayTracingShader, int nameID, GraphicsBuffer buffer)
		{
			this.Internal_SetRayTracingGraphicsBufferParam(rayTracingShader, nameID, buffer);
		}

		// Token: 0x06003055 RID: 12373 RVA: 0x0005E166 File Offset: 0x0005C366
		public void SetRayTracingBufferParam(RayTracingShader rayTracingShader, string name, GraphicsBufferHandle bufferHandle)
		{
			this.Internal_SetRayTracingGraphicsBufferHandleParam(rayTracingShader, Shader.PropertyToID(name), bufferHandle);
		}

		// Token: 0x06003056 RID: 12374 RVA: 0x0005E178 File Offset: 0x0005C378
		public void SetRayTracingBufferParam(RayTracingShader rayTracingShader, int nameID, GraphicsBufferHandle bufferHandle)
		{
			this.Internal_SetRayTracingGraphicsBufferHandleParam(rayTracingShader, nameID, bufferHandle);
		}

		// Token: 0x06003057 RID: 12375 RVA: 0x0005E185 File Offset: 0x0005C385
		public void SetRayTracingConstantBufferParam(RayTracingShader rayTracingShader, int nameID, ComputeBuffer buffer, int offset, int size)
		{
			this.Internal_SetRayTracingConstantComputeBufferParam(rayTracingShader, nameID, buffer, offset, size);
		}

		// Token: 0x06003058 RID: 12376 RVA: 0x0005E196 File Offset: 0x0005C396
		public void SetRayTracingConstantBufferParam(RayTracingShader rayTracingShader, string name, ComputeBuffer buffer, int offset, int size)
		{
			this.Internal_SetRayTracingConstantComputeBufferParam(rayTracingShader, Shader.PropertyToID(name), buffer, offset, size);
		}

		// Token: 0x06003059 RID: 12377 RVA: 0x0005E1AC File Offset: 0x0005C3AC
		public void SetRayTracingConstantBufferParam(RayTracingShader rayTracingShader, int nameID, GraphicsBuffer buffer, int offset, int size)
		{
			this.Internal_SetRayTracingConstantGraphicsBufferParam(rayTracingShader, nameID, buffer, offset, size);
		}

		// Token: 0x0600305A RID: 12378 RVA: 0x0005E1BD File Offset: 0x0005C3BD
		public void SetRayTracingConstantBufferParam(RayTracingShader rayTracingShader, string name, GraphicsBuffer buffer, int offset, int size)
		{
			this.Internal_SetRayTracingConstantGraphicsBufferParam(rayTracingShader, Shader.PropertyToID(name), buffer, offset, size);
		}

		// Token: 0x0600305B RID: 12379 RVA: 0x0005E1D3 File Offset: 0x0005C3D3
		public void SetRayTracingTextureParam(RayTracingShader rayTracingShader, string name, RenderTargetIdentifier rt)
		{
			this.Internal_SetRayTracingTextureParam(rayTracingShader, Shader.PropertyToID(name), ref rt);
		}

		// Token: 0x0600305C RID: 12380 RVA: 0x0005E1E6 File Offset: 0x0005C3E6
		public void SetRayTracingTextureParam(RayTracingShader rayTracingShader, int nameID, RenderTargetIdentifier rt)
		{
			this.Internal_SetRayTracingTextureParam(rayTracingShader, nameID, ref rt);
		}

		// Token: 0x0600305D RID: 12381 RVA: 0x0005E1F4 File Offset: 0x0005C3F4
		public void SetRayTracingFloatParam(RayTracingShader rayTracingShader, string name, float val)
		{
			this.Internal_SetRayTracingFloatParam(rayTracingShader, Shader.PropertyToID(name), val);
		}

		// Token: 0x0600305E RID: 12382 RVA: 0x0005E206 File Offset: 0x0005C406
		public void SetRayTracingFloatParam(RayTracingShader rayTracingShader, int nameID, float val)
		{
			this.Internal_SetRayTracingFloatParam(rayTracingShader, nameID, val);
		}

		// Token: 0x0600305F RID: 12383 RVA: 0x0005E213 File Offset: 0x0005C413
		public void SetRayTracingFloatParams(RayTracingShader rayTracingShader, string name, params float[] values)
		{
			this.Internal_SetRayTracingFloats(rayTracingShader, Shader.PropertyToID(name), values);
		}

		// Token: 0x06003060 RID: 12384 RVA: 0x0005E225 File Offset: 0x0005C425
		public void SetRayTracingFloatParams(RayTracingShader rayTracingShader, int nameID, params float[] values)
		{
			this.Internal_SetRayTracingFloats(rayTracingShader, nameID, values);
		}

		// Token: 0x06003061 RID: 12385 RVA: 0x0005E232 File Offset: 0x0005C432
		public void SetRayTracingIntParam(RayTracingShader rayTracingShader, string name, int val)
		{
			this.Internal_SetRayTracingIntParam(rayTracingShader, Shader.PropertyToID(name), val);
		}

		// Token: 0x06003062 RID: 12386 RVA: 0x0005E244 File Offset: 0x0005C444
		public void SetRayTracingIntParam(RayTracingShader rayTracingShader, int nameID, int val)
		{
			this.Internal_SetRayTracingIntParam(rayTracingShader, nameID, val);
		}

		// Token: 0x06003063 RID: 12387 RVA: 0x0005E251 File Offset: 0x0005C451
		public void SetRayTracingIntParams(RayTracingShader rayTracingShader, string name, params int[] values)
		{
			this.Internal_SetRayTracingInts(rayTracingShader, Shader.PropertyToID(name), values);
		}

		// Token: 0x06003064 RID: 12388 RVA: 0x0005E263 File Offset: 0x0005C463
		public void SetRayTracingIntParams(RayTracingShader rayTracingShader, int nameID, params int[] values)
		{
			this.Internal_SetRayTracingInts(rayTracingShader, nameID, values);
		}

		// Token: 0x06003065 RID: 12389 RVA: 0x0005E270 File Offset: 0x0005C470
		public void SetRayTracingVectorParam(RayTracingShader rayTracingShader, string name, Vector4 val)
		{
			this.Internal_SetRayTracingVectorParam(rayTracingShader, Shader.PropertyToID(name), val);
		}

		// Token: 0x06003066 RID: 12390 RVA: 0x0005E282 File Offset: 0x0005C482
		public void SetRayTracingVectorParam(RayTracingShader rayTracingShader, int nameID, Vector4 val)
		{
			this.Internal_SetRayTracingVectorParam(rayTracingShader, nameID, val);
		}

		// Token: 0x06003067 RID: 12391 RVA: 0x0005E28F File Offset: 0x0005C48F
		public void SetRayTracingVectorArrayParam(RayTracingShader rayTracingShader, string name, params Vector4[] values)
		{
			this.Internal_SetRayTracingVectorArrayParam(rayTracingShader, Shader.PropertyToID(name), values);
		}

		// Token: 0x06003068 RID: 12392 RVA: 0x0005E2A1 File Offset: 0x0005C4A1
		public void SetRayTracingVectorArrayParam(RayTracingShader rayTracingShader, int nameID, params Vector4[] values)
		{
			this.Internal_SetRayTracingVectorArrayParam(rayTracingShader, nameID, values);
		}

		// Token: 0x06003069 RID: 12393 RVA: 0x0005E2AE File Offset: 0x0005C4AE
		public void SetRayTracingMatrixParam(RayTracingShader rayTracingShader, string name, Matrix4x4 val)
		{
			this.Internal_SetRayTracingMatrixParam(rayTracingShader, Shader.PropertyToID(name), val);
		}

		// Token: 0x0600306A RID: 12394 RVA: 0x0005E2C0 File Offset: 0x0005C4C0
		public void SetRayTracingMatrixParam(RayTracingShader rayTracingShader, int nameID, Matrix4x4 val)
		{
			this.Internal_SetRayTracingMatrixParam(rayTracingShader, nameID, val);
		}

		// Token: 0x0600306B RID: 12395 RVA: 0x0005E2CD File Offset: 0x0005C4CD
		public void SetRayTracingMatrixArrayParam(RayTracingShader rayTracingShader, string name, params Matrix4x4[] values)
		{
			this.Internal_SetRayTracingMatrixArrayParam(rayTracingShader, Shader.PropertyToID(name), values);
		}

		// Token: 0x0600306C RID: 12396 RVA: 0x0005E2DF File Offset: 0x0005C4DF
		public void SetRayTracingMatrixArrayParam(RayTracingShader rayTracingShader, int nameID, params Matrix4x4[] values)
		{
			this.Internal_SetRayTracingMatrixArrayParam(rayTracingShader, nameID, values);
		}

		// Token: 0x0600306D RID: 12397 RVA: 0x0005E2EC File Offset: 0x0005C4EC
		public void DispatchRays(RayTracingShader rayTracingShader, string rayGenName, uint width, uint height, uint depth, Camera camera = null)
		{
			this.Internal_DispatchRays(rayTracingShader, rayGenName, width, height, depth, camera);
		}

		// Token: 0x0600306E RID: 12398 RVA: 0x0005E2FF File Offset: 0x0005C4FF
		public void DispatchRays(RayTracingShader rayTracingShader, string rayGenName, GraphicsBuffer argsBuffer, uint argsOffset, Camera camera = null)
		{
			this.Internal_DispatchRaysIndirect(rayTracingShader, rayGenName, argsBuffer, argsOffset, camera);
		}

		// Token: 0x0600306F RID: 12399 RVA: 0x0005E310 File Offset: 0x0005C510
		public void GenerateMips(RenderTargetIdentifier rt)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Internal_GenerateMips(rt);
		}

		// Token: 0x06003070 RID: 12400 RVA: 0x0005E324 File Offset: 0x0005C524
		public void GenerateMips(RenderTexture rt)
		{
			bool flag = rt == null;
			if (flag)
			{
				throw new ArgumentNullException("rt");
			}
			this.GenerateMips(new RenderTargetIdentifier(rt));
		}

		// Token: 0x06003071 RID: 12401 RVA: 0x0005E358 File Offset: 0x0005C558
		public void ResolveAntiAliasedSurface(RenderTexture rt, RenderTexture target = null)
		{
			bool flag = rt == null;
			if (flag)
			{
				throw new ArgumentNullException("rt");
			}
			this.Internal_ResolveAntiAliasedSurface(rt, target);
		}

		// Token: 0x06003072 RID: 12402 RVA: 0x0005E388 File Offset: 0x0005C588
		public void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, [DefaultValue("0")] int submeshIndex, [DefaultValue("-1")] int shaderPass, [DefaultValue("null")] MaterialPropertyBlock properties)
		{
			bool flag = mesh == null;
			if (flag)
			{
				throw new ArgumentNullException("mesh");
			}
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			bool flag2 = submeshIndex < 0 || submeshIndex >= mesh.subMeshCount;
			if (flag2)
			{
				submeshIndex = Mathf.Clamp(submeshIndex, 0, mesh.subMeshCount - 1);
				Debug.LogWarning(string.Format("submeshIndex out of range. Clampped to {0}.", submeshIndex));
			}
			bool flag3 = material == null;
			if (flag3)
			{
				throw new ArgumentNullException("material");
			}
			this.Internal_DrawMesh(mesh, matrix, material, submeshIndex, shaderPass, properties);
		}

		// Token: 0x06003073 RID: 12403 RVA: 0x0005E41F File Offset: 0x0005C61F
		[ExcludeFromDocs]
		public void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int submeshIndex, int shaderPass)
		{
			this.DrawMesh(mesh, matrix, material, submeshIndex, shaderPass, null);
		}

		// Token: 0x06003074 RID: 12404 RVA: 0x0005E431 File Offset: 0x0005C631
		[ExcludeFromDocs]
		public void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int submeshIndex)
		{
			this.DrawMesh(mesh, matrix, material, submeshIndex, -1);
		}

		// Token: 0x06003075 RID: 12405 RVA: 0x0005E441 File Offset: 0x0005C641
		[ExcludeFromDocs]
		public void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material)
		{
			this.DrawMesh(mesh, matrix, material, 0);
		}

		// Token: 0x06003076 RID: 12406 RVA: 0x0005E450 File Offset: 0x0005C650
		[ExcludeFromDocs]
		public void DrawMultipleMeshes(Matrix4x4[] matrices, Mesh[] meshes, int[] subsetIndices, int count, Material material, int shaderPass, [DefaultValue("null")] MaterialPropertyBlock properties)
		{
			bool flag = matrices.Length != meshes.Length || matrices.Length != subsetIndices.Length;
			if (flag)
			{
				throw new InvalidOperationException("matrices, meshes, subsetIndices must be of same length and must be valid");
			}
			bool flag2 = count < 1;
			if (flag2)
			{
				throw new InvalidOperationException("count must be atleast 1");
			}
			this.Internal_DrawMultipleMeshes(matrices, meshes, subsetIndices, count, material, shaderPass, properties);
		}

		// Token: 0x06003077 RID: 12407 RVA: 0x0005E4AC File Offset: 0x0005C6AC
		public void DrawRenderer(Renderer renderer, Material material, [DefaultValue("0")] int submeshIndex, [DefaultValue("-1")] int shaderPass)
		{
			bool flag = renderer == null;
			if (flag)
			{
				throw new ArgumentNullException("renderer");
			}
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			bool flag2 = submeshIndex < 0;
			if (flag2)
			{
				submeshIndex = Mathf.Max(submeshIndex, 0);
				Debug.LogWarning(string.Format("submeshIndex out of range. Clampped to {0}.", submeshIndex));
			}
			bool flag3 = material == null;
			if (flag3)
			{
				throw new ArgumentNullException("material");
			}
			this.Internal_DrawRenderer(renderer, material, submeshIndex, shaderPass);
		}

		// Token: 0x06003078 RID: 12408 RVA: 0x0005E524 File Offset: 0x0005C724
		[ExcludeFromDocs]
		public void DrawRenderer(Renderer renderer, Material material, int submeshIndex)
		{
			this.DrawRenderer(renderer, material, submeshIndex, -1);
		}

		// Token: 0x06003079 RID: 12409 RVA: 0x0005E532 File Offset: 0x0005C732
		[ExcludeFromDocs]
		public void DrawRenderer(Renderer renderer, Material material)
		{
			this.DrawRenderer(renderer, material, 0);
		}

		// Token: 0x0600307A RID: 12410 RVA: 0x0005E53F File Offset: 0x0005C73F
		public void DrawRendererList(RendererList rendererList)
		{
			this.Internal_DrawRendererList(rendererList);
		}

		// Token: 0x0600307B RID: 12411 RVA: 0x0005E54C File Offset: 0x0005C74C
		public void DrawProcedural(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, int vertexCount, [DefaultValue("1")] int instanceCount, [DefaultValue("null")] MaterialPropertyBlock properties)
		{
			bool flag = material == null;
			if (flag)
			{
				throw new ArgumentNullException("material");
			}
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Internal_DrawProcedural(matrix, material, shaderPass, topology, vertexCount, instanceCount, properties);
		}

		// Token: 0x0600307C RID: 12412 RVA: 0x0005E58B File Offset: 0x0005C78B
		[ExcludeFromDocs]
		public void DrawProcedural(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, int vertexCount, int instanceCount)
		{
			this.DrawProcedural(matrix, material, shaderPass, topology, vertexCount, instanceCount, null);
		}

		// Token: 0x0600307D RID: 12413 RVA: 0x0005E59F File Offset: 0x0005C79F
		[ExcludeFromDocs]
		public void DrawProcedural(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, int vertexCount)
		{
			this.DrawProcedural(matrix, material, shaderPass, topology, vertexCount, 1);
		}

		// Token: 0x0600307E RID: 12414 RVA: 0x0005E5B4 File Offset: 0x0005C7B4
		public void DrawProcedural(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, int indexCount, int instanceCount, MaterialPropertyBlock properties)
		{
			bool flag = indexBuffer == null;
			if (flag)
			{
				throw new ArgumentNullException("indexBuffer");
			}
			bool flag2 = material == null;
			if (flag2)
			{
				throw new ArgumentNullException("material");
			}
			this.Internal_DrawProceduralIndexed(indexBuffer, matrix, material, shaderPass, topology, indexCount, instanceCount, properties);
		}

		// Token: 0x0600307F RID: 12415 RVA: 0x0005E600 File Offset: 0x0005C800
		public void DrawProcedural(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, int indexCount, int instanceCount)
		{
			this.DrawProcedural(indexBuffer, matrix, material, shaderPass, topology, indexCount, instanceCount, null);
		}

		// Token: 0x06003080 RID: 12416 RVA: 0x0005E621 File Offset: 0x0005C821
		public void DrawProcedural(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, int indexCount)
		{
			this.DrawProcedural(indexBuffer, matrix, material, shaderPass, topology, indexCount, 1);
		}

		// Token: 0x06003081 RID: 12417 RVA: 0x0005E638 File Offset: 0x0005C838
		public void DrawProceduralIndirect(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, ComputeBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties)
		{
			bool flag = !SystemInfo.supportsIndirectArgumentsBuffer;
			if (flag)
			{
				throw new InvalidOperationException("Indirect argument buffers are not supported.");
			}
			bool flag2 = material == null;
			if (flag2)
			{
				throw new ArgumentNullException("material");
			}
			bool flag3 = bufferWithArgs == null;
			if (flag3)
			{
				throw new ArgumentNullException("bufferWithArgs");
			}
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Internal_DrawProceduralIndirect(matrix, material, shaderPass, topology, bufferWithArgs, argsOffset, properties);
		}

		// Token: 0x06003082 RID: 12418 RVA: 0x0005E6A2 File Offset: 0x0005C8A2
		public void DrawProceduralIndirect(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, ComputeBuffer bufferWithArgs, int argsOffset)
		{
			this.DrawProceduralIndirect(matrix, material, shaderPass, topology, bufferWithArgs, argsOffset, null);
		}

		// Token: 0x06003083 RID: 12419 RVA: 0x0005E6B6 File Offset: 0x0005C8B6
		public void DrawProceduralIndirect(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, ComputeBuffer bufferWithArgs)
		{
			this.DrawProceduralIndirect(matrix, material, shaderPass, topology, bufferWithArgs, 0);
		}

		// Token: 0x06003084 RID: 12420 RVA: 0x0005E6C8 File Offset: 0x0005C8C8
		public void DrawProceduralIndirect(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, ComputeBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties)
		{
			bool flag = !SystemInfo.supportsIndirectArgumentsBuffer;
			if (flag)
			{
				throw new InvalidOperationException("Indirect argument buffers are not supported.");
			}
			bool flag2 = indexBuffer == null;
			if (flag2)
			{
				throw new ArgumentNullException("indexBuffer");
			}
			bool flag3 = material == null;
			if (flag3)
			{
				throw new ArgumentNullException("material");
			}
			bool flag4 = bufferWithArgs == null;
			if (flag4)
			{
				throw new ArgumentNullException("bufferWithArgs");
			}
			this.Internal_DrawProceduralIndexedIndirect(indexBuffer, matrix, material, shaderPass, topology, bufferWithArgs, argsOffset, properties);
		}

		// Token: 0x06003085 RID: 12421 RVA: 0x0005E740 File Offset: 0x0005C940
		public void DrawProceduralIndirect(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, ComputeBuffer bufferWithArgs, int argsOffset)
		{
			this.DrawProceduralIndirect(indexBuffer, matrix, material, shaderPass, topology, bufferWithArgs, argsOffset, null);
		}

		// Token: 0x06003086 RID: 12422 RVA: 0x0005E761 File Offset: 0x0005C961
		public void DrawProceduralIndirect(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, ComputeBuffer bufferWithArgs)
		{
			this.DrawProceduralIndirect(indexBuffer, matrix, material, shaderPass, topology, bufferWithArgs, 0);
		}

		// Token: 0x06003087 RID: 12423 RVA: 0x0005E778 File Offset: 0x0005C978
		public void DrawProceduralIndirect(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, GraphicsBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties)
		{
			bool flag = !SystemInfo.supportsIndirectArgumentsBuffer;
			if (flag)
			{
				throw new InvalidOperationException("Indirect argument buffers are not supported.");
			}
			bool flag2 = material == null;
			if (flag2)
			{
				throw new ArgumentNullException("material");
			}
			bool flag3 = bufferWithArgs == null;
			if (flag3)
			{
				throw new ArgumentNullException("bufferWithArgs");
			}
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Internal_DrawProceduralIndirectGraphicsBuffer(matrix, material, shaderPass, topology, bufferWithArgs, argsOffset, properties);
		}

		// Token: 0x06003088 RID: 12424 RVA: 0x0005E7E2 File Offset: 0x0005C9E2
		public void DrawProceduralIndirect(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, GraphicsBuffer bufferWithArgs, int argsOffset)
		{
			this.DrawProceduralIndirect(matrix, material, shaderPass, topology, bufferWithArgs, argsOffset, null);
		}

		// Token: 0x06003089 RID: 12425 RVA: 0x0005E7F6 File Offset: 0x0005C9F6
		public void DrawProceduralIndirect(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, GraphicsBuffer bufferWithArgs)
		{
			this.DrawProceduralIndirect(matrix, material, shaderPass, topology, bufferWithArgs, 0);
		}

		// Token: 0x0600308A RID: 12426 RVA: 0x0005E808 File Offset: 0x0005CA08
		public void DrawProceduralIndirect(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, GraphicsBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties)
		{
			bool flag = !SystemInfo.supportsIndirectArgumentsBuffer;
			if (flag)
			{
				throw new InvalidOperationException("Indirect argument buffers are not supported.");
			}
			bool flag2 = indexBuffer == null;
			if (flag2)
			{
				throw new ArgumentNullException("indexBuffer");
			}
			bool flag3 = material == null;
			if (flag3)
			{
				throw new ArgumentNullException("material");
			}
			bool flag4 = bufferWithArgs == null;
			if (flag4)
			{
				throw new ArgumentNullException("bufferWithArgs");
			}
			this.Internal_DrawProceduralIndexedIndirectGraphicsBuffer(indexBuffer, matrix, material, shaderPass, topology, bufferWithArgs, argsOffset, properties);
		}

		// Token: 0x0600308B RID: 12427 RVA: 0x0005E880 File Offset: 0x0005CA80
		public void DrawProceduralIndirect(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, GraphicsBuffer bufferWithArgs, int argsOffset)
		{
			this.DrawProceduralIndirect(indexBuffer, matrix, material, shaderPass, topology, bufferWithArgs, argsOffset, null);
		}

		// Token: 0x0600308C RID: 12428 RVA: 0x0005E8A1 File Offset: 0x0005CAA1
		public void DrawProceduralIndirect(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, GraphicsBuffer bufferWithArgs)
		{
			this.DrawProceduralIndirect(indexBuffer, matrix, material, shaderPass, topology, bufferWithArgs, 0);
		}

		// Token: 0x0600308D RID: 12429 RVA: 0x0005E8B8 File Offset: 0x0005CAB8
		public void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, int shaderPass, Matrix4x4[] matrices, int count, MaterialPropertyBlock properties)
		{
			bool flag = !SystemInfo.supportsInstancing;
			if (flag)
			{
				throw new InvalidOperationException("DrawMeshInstanced is not supported.");
			}
			bool flag2 = mesh == null;
			if (flag2)
			{
				throw new ArgumentNullException("mesh");
			}
			bool flag3 = submeshIndex < 0 || submeshIndex >= mesh.subMeshCount;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("submeshIndex", "submeshIndex out of range.");
			}
			bool flag4 = material == null;
			if (flag4)
			{
				throw new ArgumentNullException("material");
			}
			bool flag5 = !material.enableInstancing;
			if (flag5)
			{
				throw new InvalidOperationException("Material needs to enable instancing for use with DrawMeshInstanced.");
			}
			bool flag6 = matrices == null;
			if (flag6)
			{
				throw new ArgumentNullException("matrices");
			}
			bool flag7 = count < 0 || count > Mathf.Min(Graphics.kMaxDrawMeshInstanceCount, matrices.Length);
			if (flag7)
			{
				throw new ArgumentOutOfRangeException("count", string.Format("Count must be in the range of 0 to {0}.", Mathf.Min(Graphics.kMaxDrawMeshInstanceCount, matrices.Length)));
			}
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			bool flag8 = count > 0;
			if (flag8)
			{
				this.Internal_DrawMeshInstanced(mesh, submeshIndex, material, shaderPass, matrices, count, properties);
			}
		}

		// Token: 0x0600308E RID: 12430 RVA: 0x0005E9CE File Offset: 0x0005CBCE
		public void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, int shaderPass, Matrix4x4[] matrices, int count)
		{
			this.DrawMeshInstanced(mesh, submeshIndex, material, shaderPass, matrices, count, null);
		}

		// Token: 0x0600308F RID: 12431 RVA: 0x0005E9E2 File Offset: 0x0005CBE2
		public void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, int shaderPass, Matrix4x4[] matrices)
		{
			this.DrawMeshInstanced(mesh, submeshIndex, material, shaderPass, matrices, matrices.Length);
		}

		// Token: 0x06003090 RID: 12432 RVA: 0x0005E9F8 File Offset: 0x0005CBF8
		public void DrawMeshInstancedProcedural(Mesh mesh, int submeshIndex, Material material, int shaderPass, int count, MaterialPropertyBlock properties = null)
		{
			bool flag = !SystemInfo.supportsInstancing;
			if (flag)
			{
				throw new InvalidOperationException("DrawMeshInstancedProcedural is not supported.");
			}
			bool flag2 = mesh == null;
			if (flag2)
			{
				throw new ArgumentNullException("mesh");
			}
			bool flag3 = submeshIndex < 0 || submeshIndex >= mesh.subMeshCount;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("submeshIndex", "submeshIndex out of range.");
			}
			bool flag4 = material == null;
			if (flag4)
			{
				throw new ArgumentNullException("material");
			}
			bool flag5 = count <= 0;
			if (flag5)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			bool flag6 = count > 0;
			if (flag6)
			{
				this.Internal_DrawMeshInstancedProcedural(mesh, submeshIndex, material, shaderPass, count, properties);
			}
		}

		// Token: 0x06003091 RID: 12433 RVA: 0x0005EAB0 File Offset: 0x0005CCB0
		public void DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, int shaderPass, ComputeBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties)
		{
			bool flag = !SystemInfo.supportsInstancing;
			if (flag)
			{
				throw new InvalidOperationException("Instancing is not supported.");
			}
			bool flag2 = !SystemInfo.supportsIndirectArgumentsBuffer;
			if (flag2)
			{
				throw new InvalidOperationException("Indirect argument buffers are not supported.");
			}
			bool flag3 = mesh == null;
			if (flag3)
			{
				throw new ArgumentNullException("mesh");
			}
			bool flag4 = submeshIndex < 0 || submeshIndex >= mesh.subMeshCount;
			if (flag4)
			{
				throw new ArgumentOutOfRangeException("submeshIndex", "submeshIndex out of range.");
			}
			bool flag5 = material == null;
			if (flag5)
			{
				throw new ArgumentNullException("material");
			}
			bool flag6 = bufferWithArgs == null;
			if (flag6)
			{
				throw new ArgumentNullException("bufferWithArgs");
			}
			this.Internal_DrawMeshInstancedIndirect(mesh, submeshIndex, material, shaderPass, bufferWithArgs, argsOffset, properties);
		}

		// Token: 0x06003092 RID: 12434 RVA: 0x0005EB69 File Offset: 0x0005CD69
		public void DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, int shaderPass, ComputeBuffer bufferWithArgs, int argsOffset)
		{
			this.DrawMeshInstancedIndirect(mesh, submeshIndex, material, shaderPass, bufferWithArgs, argsOffset, null);
		}

		// Token: 0x06003093 RID: 12435 RVA: 0x0005EB7D File Offset: 0x0005CD7D
		public void DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, int shaderPass, ComputeBuffer bufferWithArgs)
		{
			this.DrawMeshInstancedIndirect(mesh, submeshIndex, material, shaderPass, bufferWithArgs, 0, null);
		}

		// Token: 0x06003094 RID: 12436 RVA: 0x0005EB90 File Offset: 0x0005CD90
		public void DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, int shaderPass, GraphicsBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties)
		{
			bool flag = !SystemInfo.supportsInstancing;
			if (flag)
			{
				throw new InvalidOperationException("Instancing is not supported.");
			}
			bool flag2 = !SystemInfo.supportsIndirectArgumentsBuffer;
			if (flag2)
			{
				throw new InvalidOperationException("Indirect argument buffers are not supported.");
			}
			bool flag3 = mesh == null;
			if (flag3)
			{
				throw new ArgumentNullException("mesh");
			}
			bool flag4 = submeshIndex < 0 || submeshIndex >= mesh.subMeshCount;
			if (flag4)
			{
				throw new ArgumentOutOfRangeException("submeshIndex", "submeshIndex out of range.");
			}
			bool flag5 = material == null;
			if (flag5)
			{
				throw new ArgumentNullException("material");
			}
			bool flag6 = bufferWithArgs == null;
			if (flag6)
			{
				throw new ArgumentNullException("bufferWithArgs");
			}
			this.Internal_DrawMeshInstancedIndirectGraphicsBuffer(mesh, submeshIndex, material, shaderPass, bufferWithArgs, argsOffset, properties);
		}

		// Token: 0x06003095 RID: 12437 RVA: 0x0005EC49 File Offset: 0x0005CE49
		public void DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, int shaderPass, GraphicsBuffer bufferWithArgs, int argsOffset)
		{
			this.DrawMeshInstancedIndirect(mesh, submeshIndex, material, shaderPass, bufferWithArgs, argsOffset, null);
		}

		// Token: 0x06003096 RID: 12438 RVA: 0x0005EC5D File Offset: 0x0005CE5D
		public void DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, int shaderPass, GraphicsBuffer bufferWithArgs)
		{
			this.DrawMeshInstancedIndirect(mesh, submeshIndex, material, shaderPass, bufferWithArgs, 0, null);
		}

		// Token: 0x06003097 RID: 12439 RVA: 0x0005EC70 File Offset: 0x0005CE70
		public void DrawOcclusionMesh(RectInt normalizedCamViewport)
		{
			this.Internal_DrawOcclusionMesh(normalizedCamViewport);
		}

		// Token: 0x06003098 RID: 12440 RVA: 0x0005EC7B File Offset: 0x0005CE7B
		public void SetRandomWriteTarget(int index, RenderTargetIdentifier rt)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.SetRandomWriteTarget_Texture(index, ref rt);
		}

		// Token: 0x06003099 RID: 12441 RVA: 0x0005EC91 File Offset: 0x0005CE91
		public void SetRandomWriteTarget(int index, ComputeBuffer buffer, bool preserveCounterValue)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.SetRandomWriteTarget_Buffer(index, buffer, preserveCounterValue);
		}

		// Token: 0x0600309A RID: 12442 RVA: 0x0005ECA7 File Offset: 0x0005CEA7
		public void SetRandomWriteTarget(int index, ComputeBuffer buffer)
		{
			this.SetRandomWriteTarget(index, buffer, false);
		}

		// Token: 0x0600309B RID: 12443 RVA: 0x0005ECB4 File Offset: 0x0005CEB4
		public void SetRandomWriteTarget(int index, GraphicsBuffer buffer, bool preserveCounterValue)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.SetRandomWriteTarget_GraphicsBuffer(index, buffer, preserveCounterValue);
		}

		// Token: 0x0600309C RID: 12444 RVA: 0x0005ECCA File Offset: 0x0005CECA
		public void SetRandomWriteTarget(int index, GraphicsBuffer buffer)
		{
			this.SetRandomWriteTarget(index, buffer, false);
		}

		// Token: 0x0600309D RID: 12445 RVA: 0x0005ECD7 File Offset: 0x0005CED7
		public void CopyCounterValue(ComputeBuffer src, ComputeBuffer dst, uint dstOffsetBytes)
		{
			this.CopyCounterValueCC(src, dst, dstOffsetBytes);
		}

		// Token: 0x0600309E RID: 12446 RVA: 0x0005ECE4 File Offset: 0x0005CEE4
		public void CopyCounterValue(GraphicsBuffer src, ComputeBuffer dst, uint dstOffsetBytes)
		{
			this.CopyCounterValueGC(src, dst, dstOffsetBytes);
		}

		// Token: 0x0600309F RID: 12447 RVA: 0x0005ECF1 File Offset: 0x0005CEF1
		public void CopyCounterValue(ComputeBuffer src, GraphicsBuffer dst, uint dstOffsetBytes)
		{
			this.CopyCounterValueCG(src, dst, dstOffsetBytes);
		}

		// Token: 0x060030A0 RID: 12448 RVA: 0x0005ECFE File Offset: 0x0005CEFE
		public void CopyCounterValue(GraphicsBuffer src, GraphicsBuffer dst, uint dstOffsetBytes)
		{
			this.CopyCounterValueGG(src, dst, dstOffsetBytes);
		}

		// Token: 0x060030A1 RID: 12449 RVA: 0x0005ED0C File Offset: 0x0005CF0C
		public void CopyTexture(RenderTargetIdentifier src, RenderTargetIdentifier dst)
		{
			this.CopyTexture_Internal(ref src, -1, -1, -1, -1, -1, -1, ref dst, -1, -1, -1, -1, 1);
		}

		// Token: 0x060030A2 RID: 12450 RVA: 0x0005ED30 File Offset: 0x0005CF30
		public void CopyTexture(RenderTargetIdentifier src, int srcElement, RenderTargetIdentifier dst, int dstElement)
		{
			this.CopyTexture_Internal(ref src, srcElement, -1, -1, -1, -1, -1, ref dst, dstElement, -1, -1, -1, 2);
		}

		// Token: 0x060030A3 RID: 12451 RVA: 0x0005ED58 File Offset: 0x0005CF58
		public void CopyTexture(RenderTargetIdentifier src, int srcElement, int srcMip, RenderTargetIdentifier dst, int dstElement, int dstMip)
		{
			this.CopyTexture_Internal(ref src, srcElement, srcMip, -1, -1, -1, -1, ref dst, dstElement, dstMip, -1, -1, 3);
		}

		// Token: 0x060030A4 RID: 12452 RVA: 0x0005ED80 File Offset: 0x0005CF80
		public void CopyTexture(RenderTargetIdentifier src, int srcElement, int srcMip, int srcX, int srcY, int srcWidth, int srcHeight, RenderTargetIdentifier dst, int dstElement, int dstMip, int dstX, int dstY)
		{
			this.CopyTexture_Internal(ref src, srcElement, srcMip, srcX, srcY, srcWidth, srcHeight, ref dst, dstElement, dstMip, dstX, dstY, 4);
		}

		// Token: 0x060030A5 RID: 12453 RVA: 0x0005EDAC File Offset: 0x0005CFAC
		public void Blit(Texture source, RenderTargetIdentifier dest)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Blit_Texture(source, ref dest, null, -1, new Vector2(1f, 1f), new Vector2(0f, 0f), Texture2DArray.allSlices, 0);
		}

		// Token: 0x060030A6 RID: 12454 RVA: 0x0005EDF4 File Offset: 0x0005CFF4
		public void Blit(Texture source, RenderTargetIdentifier dest, Vector2 scale, Vector2 offset)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Blit_Texture(source, ref dest, null, -1, scale, offset, Texture2DArray.allSlices, 0);
		}

		// Token: 0x060030A7 RID: 12455 RVA: 0x0005EE20 File Offset: 0x0005D020
		public void Blit(Texture source, RenderTargetIdentifier dest, Material mat)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Blit_Texture(source, ref dest, mat, -1, new Vector2(1f, 1f), new Vector2(0f, 0f), Texture2DArray.allSlices, 0);
		}

		// Token: 0x060030A8 RID: 12456 RVA: 0x0005EE68 File Offset: 0x0005D068
		public void Blit(Texture source, RenderTargetIdentifier dest, Material mat, int pass)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Blit_Texture(source, ref dest, mat, pass, new Vector2(1f, 1f), new Vector2(0f, 0f), Texture2DArray.allSlices, 0);
		}

		// Token: 0x060030A9 RID: 12457 RVA: 0x0005EEB0 File Offset: 0x0005D0B0
		public void Blit(RenderTargetIdentifier source, RenderTargetIdentifier dest)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Blit_Identifier(ref source, ref dest, null, -1, new Vector2(1f, 1f), new Vector2(0f, 0f), Texture2DArray.allSlices, 0);
		}

		// Token: 0x060030AA RID: 12458 RVA: 0x0005EEF8 File Offset: 0x0005D0F8
		public void Blit(RenderTargetIdentifier source, RenderTargetIdentifier dest, Vector2 scale, Vector2 offset)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Blit_Identifier(ref source, ref dest, null, -1, scale, offset, Texture2DArray.allSlices, 0);
		}

		// Token: 0x060030AB RID: 12459 RVA: 0x0005EF28 File Offset: 0x0005D128
		public void Blit(RenderTargetIdentifier source, RenderTargetIdentifier dest, Material mat)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Blit_Identifier(ref source, ref dest, mat, -1, new Vector2(1f, 1f), new Vector2(0f, 0f), Texture2DArray.allSlices, 0);
		}

		// Token: 0x060030AC RID: 12460 RVA: 0x0005EF70 File Offset: 0x0005D170
		public void Blit(RenderTargetIdentifier source, RenderTargetIdentifier dest, Material mat, int pass)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Blit_Identifier(ref source, ref dest, mat, pass, new Vector2(1f, 1f), new Vector2(0f, 0f), Texture2DArray.allSlices, 0);
		}

		// Token: 0x060030AD RID: 12461 RVA: 0x0005EFBC File Offset: 0x0005D1BC
		public void Blit(RenderTargetIdentifier source, RenderTargetIdentifier dest, int sourceDepthSlice, int destDepthSlice)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Blit_Identifier(ref source, ref dest, null, -1, new Vector2(1f, 1f), new Vector2(0f, 0f), sourceDepthSlice, destDepthSlice);
		}

		// Token: 0x060030AE RID: 12462 RVA: 0x0005F004 File Offset: 0x0005D204
		public void Blit(RenderTargetIdentifier source, RenderTargetIdentifier dest, Vector2 scale, Vector2 offset, int sourceDepthSlice, int destDepthSlice)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Blit_Identifier(ref source, ref dest, null, -1, scale, offset, sourceDepthSlice, destDepthSlice);
		}

		// Token: 0x060030AF RID: 12463 RVA: 0x0005F030 File Offset: 0x0005D230
		public void Blit(RenderTargetIdentifier source, RenderTargetIdentifier dest, Material mat, int pass, int destDepthSlice)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Blit_Identifier(ref source, ref dest, mat, pass, new Vector2(1f, 1f), new Vector2(0f, 0f), Texture2DArray.allSlices, destDepthSlice);
		}

		// Token: 0x060030B0 RID: 12464 RVA: 0x0005F07A File Offset: 0x0005D27A
		public void SetGlobalFloat(string name, float value)
		{
			this.SetGlobalFloat(Shader.PropertyToID(name), value);
		}

		// Token: 0x060030B1 RID: 12465 RVA: 0x0005F08B File Offset: 0x0005D28B
		public void SetGlobalInt(string name, int value)
		{
			this.SetGlobalInt(Shader.PropertyToID(name), value);
		}

		// Token: 0x060030B2 RID: 12466 RVA: 0x0005F09C File Offset: 0x0005D29C
		public void SetGlobalInteger(string name, int value)
		{
			this.SetGlobalInteger(Shader.PropertyToID(name), value);
		}

		// Token: 0x060030B3 RID: 12467 RVA: 0x0005F0AD File Offset: 0x0005D2AD
		public void SetGlobalVector(string name, Vector4 value)
		{
			this.SetGlobalVector(Shader.PropertyToID(name), value);
		}

		// Token: 0x060030B4 RID: 12468 RVA: 0x0005F0BE File Offset: 0x0005D2BE
		public void SetGlobalColor(string name, Color value)
		{
			this.SetGlobalColor(Shader.PropertyToID(name), value);
		}

		// Token: 0x060030B5 RID: 12469 RVA: 0x0005F0CF File Offset: 0x0005D2CF
		public void SetGlobalMatrix(string name, Matrix4x4 value)
		{
			this.SetGlobalMatrix(Shader.PropertyToID(name), value);
		}

		// Token: 0x060030B6 RID: 12470 RVA: 0x0005F0E0 File Offset: 0x0005D2E0
		public void SetGlobalFloatArray(string propertyName, List<float> values)
		{
			this.SetGlobalFloatArray(Shader.PropertyToID(propertyName), values);
		}

		// Token: 0x060030B7 RID: 12471 RVA: 0x0005F0F4 File Offset: 0x0005D2F4
		public void SetGlobalFloatArray(int nameID, List<float> values)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			bool flag2 = values.Count == 0;
			if (flag2)
			{
				throw new ArgumentException("Zero-sized array is not allowed.");
			}
			this.SetGlobalFloatArrayListImpl(nameID, values);
		}

		// Token: 0x060030B8 RID: 12472 RVA: 0x0005F136 File Offset: 0x0005D336
		public void SetGlobalFloatArray(string propertyName, float[] values)
		{
			this.SetGlobalFloatArray(Shader.PropertyToID(propertyName), values);
		}

		// Token: 0x060030B9 RID: 12473 RVA: 0x0005F147 File Offset: 0x0005D347
		public void SetGlobalVectorArray(string propertyName, List<Vector4> values)
		{
			this.SetGlobalVectorArray(Shader.PropertyToID(propertyName), values);
		}

		// Token: 0x060030BA RID: 12474 RVA: 0x0005F158 File Offset: 0x0005D358
		public void SetGlobalVectorArray(int nameID, List<Vector4> values)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			bool flag2 = values.Count == 0;
			if (flag2)
			{
				throw new ArgumentException("Zero-sized array is not allowed.");
			}
			this.SetGlobalVectorArrayListImpl(nameID, values);
		}

		// Token: 0x060030BB RID: 12475 RVA: 0x0005F19A File Offset: 0x0005D39A
		public void SetGlobalVectorArray(string propertyName, Vector4[] values)
		{
			this.SetGlobalVectorArray(Shader.PropertyToID(propertyName), values);
		}

		// Token: 0x060030BC RID: 12476 RVA: 0x0005F1AB File Offset: 0x0005D3AB
		public void SetGlobalMatrixArray(string propertyName, List<Matrix4x4> values)
		{
			this.SetGlobalMatrixArray(Shader.PropertyToID(propertyName), values);
		}

		// Token: 0x060030BD RID: 12477 RVA: 0x0005F1BC File Offset: 0x0005D3BC
		public void SetGlobalMatrixArray(int nameID, List<Matrix4x4> values)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			bool flag2 = values.Count == 0;
			if (flag2)
			{
				throw new ArgumentException("Zero-sized array is not allowed.");
			}
			this.SetGlobalMatrixArrayListImpl(nameID, values);
		}

		// Token: 0x060030BE RID: 12478 RVA: 0x0005F1FE File Offset: 0x0005D3FE
		public void SetGlobalMatrixArray(string propertyName, Matrix4x4[] values)
		{
			this.SetGlobalMatrixArray(Shader.PropertyToID(propertyName), values);
		}

		// Token: 0x060030BF RID: 12479 RVA: 0x0005F20F File Offset: 0x0005D40F
		public void SetGlobalTexture(string name, RenderTargetIdentifier value)
		{
			this.SetGlobalTexture(Shader.PropertyToID(name), value, RenderTextureSubElement.Default);
		}

		// Token: 0x060030C0 RID: 12480 RVA: 0x0005F221 File Offset: 0x0005D421
		public void SetGlobalTexture(int nameID, RenderTargetIdentifier value)
		{
			this.SetGlobalTexture_Impl(nameID, ref value, RenderTextureSubElement.Default);
		}

		// Token: 0x060030C1 RID: 12481 RVA: 0x0005F22F File Offset: 0x0005D42F
		public void SetGlobalTexture(string name, RenderTargetIdentifier value, RenderTextureSubElement element)
		{
			this.SetGlobalTexture(Shader.PropertyToID(name), value, element);
		}

		// Token: 0x060030C2 RID: 12482 RVA: 0x0005F241 File Offset: 0x0005D441
		public void SetGlobalTexture(int nameID, RenderTargetIdentifier value, RenderTextureSubElement element)
		{
			this.SetGlobalTexture_Impl(nameID, ref value, element);
		}

		// Token: 0x060030C3 RID: 12483 RVA: 0x0005F24F File Offset: 0x0005D44F
		public void SetGlobalBuffer(string name, ComputeBuffer value)
		{
			this.SetGlobalBufferInternal(Shader.PropertyToID(name), value);
		}

		// Token: 0x060030C4 RID: 12484 RVA: 0x0005F260 File Offset: 0x0005D460
		public void SetGlobalBuffer(int nameID, ComputeBuffer value)
		{
			this.SetGlobalBufferInternal(nameID, value);
		}

		// Token: 0x060030C5 RID: 12485 RVA: 0x0005F26C File Offset: 0x0005D46C
		public void SetGlobalBuffer(string name, GraphicsBuffer value)
		{
			this.SetGlobalGraphicsBufferInternal(Shader.PropertyToID(name), value);
		}

		// Token: 0x060030C6 RID: 12486 RVA: 0x0005F27D File Offset: 0x0005D47D
		public void SetGlobalBuffer(int nameID, GraphicsBuffer value)
		{
			this.SetGlobalGraphicsBufferInternal(nameID, value);
		}

		// Token: 0x060030C7 RID: 12487 RVA: 0x0005F289 File Offset: 0x0005D489
		public void SetGlobalConstantBuffer(ComputeBuffer buffer, int nameID, int offset, int size)
		{
			this.SetGlobalConstantBufferInternal(buffer, nameID, offset, size);
		}

		// Token: 0x060030C8 RID: 12488 RVA: 0x0005F298 File Offset: 0x0005D498
		public void SetGlobalConstantBuffer(ComputeBuffer buffer, string name, int offset, int size)
		{
			this.SetGlobalConstantBufferInternal(buffer, Shader.PropertyToID(name), offset, size);
		}

		// Token: 0x060030C9 RID: 12489 RVA: 0x0005F2AC File Offset: 0x0005D4AC
		public void SetGlobalConstantBuffer(GraphicsBuffer buffer, int nameID, int offset, int size)
		{
			this.SetGlobalConstantGraphicsBufferInternal(buffer, nameID, offset, size);
		}

		// Token: 0x060030CA RID: 12490 RVA: 0x0005F2BB File Offset: 0x0005D4BB
		public void SetGlobalConstantBuffer(GraphicsBuffer buffer, string name, int offset, int size)
		{
			this.SetGlobalConstantGraphicsBufferInternal(buffer, Shader.PropertyToID(name), offset, size);
		}

		// Token: 0x060030CB RID: 12491 RVA: 0x0005F2CF File Offset: 0x0005D4CF
		public void SetGlobalRayTracingAccelerationStructure(string name, RayTracingAccelerationStructure accelerationStructure)
		{
			this.SetGlobalRayTracingAccelerationStructureInternal(accelerationStructure, Shader.PropertyToID(name));
		}

		// Token: 0x060030CC RID: 12492 RVA: 0x0005F2E0 File Offset: 0x0005D4E0
		public void SetGlobalRayTracingAccelerationStructure(int nameID, RayTracingAccelerationStructure accelerationStructure)
		{
			this.SetGlobalRayTracingAccelerationStructureInternal(accelerationStructure, nameID);
		}

		// Token: 0x060030CD RID: 12493 RVA: 0x0005F2EC File Offset: 0x0005D4EC
		public void SetShadowSamplingMode(RenderTargetIdentifier shadowmap, ShadowSamplingMode mode)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.SetShadowSamplingMode_Impl(ref shadowmap, mode);
		}

		// Token: 0x060030CE RID: 12494 RVA: 0x0005F302 File Offset: 0x0005D502
		public void SetSinglePassStereo(SinglePassStereoMode mode)
		{
			this.Internal_SetSinglePassStereo(mode);
		}

		// Token: 0x060030CF RID: 12495 RVA: 0x0005F310 File Offset: 0x0005D510
		public void IssuePluginEvent(IntPtr callback, int eventID)
		{
			bool flag = callback == IntPtr.Zero;
			if (flag)
			{
				throw new ArgumentException("Null callback specified.");
			}
			this.IssuePluginEventInternal(callback, eventID);
		}

		// Token: 0x060030D0 RID: 12496 RVA: 0x0005F344 File Offset: 0x0005D544
		public void IssuePluginEventAndData(IntPtr callback, int eventID, IntPtr data)
		{
			bool flag = callback == IntPtr.Zero;
			if (flag)
			{
				throw new ArgumentException("Null callback specified.");
			}
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.IssuePluginEventAndDataInternal(callback, eventID, data);
		}

		// Token: 0x060030D1 RID: 12497 RVA: 0x0005F380 File Offset: 0x0005D580
		public void IssuePluginEventAndDataWithFlags(IntPtr callback, int eventID, CustomMarkerCallbackFlags flags, IntPtr data)
		{
			bool flag = callback == IntPtr.Zero;
			if (flag)
			{
				throw new ArgumentException("Null callback specified.");
			}
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.IssuePluginEventAndDataInternalWithFlags(callback, eventID, flags, data);
		}

		// Token: 0x060030D2 RID: 12498 RVA: 0x0005F3BD File Offset: 0x0005D5BD
		public void IssuePluginCustomBlit(IntPtr callback, uint command, RenderTargetIdentifier source, RenderTargetIdentifier dest, uint commandParam, uint commandFlags)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.IssuePluginCustomBlitInternal(callback, command, ref source, ref dest, commandParam, commandFlags);
		}

		// Token: 0x060030D3 RID: 12499 RVA: 0x0005F3DA File Offset: 0x0005D5DA
		[Obsolete("Use IssuePluginCustomTextureUpdateV2 to register TextureUpdate callbacks instead. Callbacks will be passed event IDs kUnityRenderingExtEventUpdateTextureBeginV2 or kUnityRenderingExtEventUpdateTextureEndV2, and data parameter of type UnityRenderingExtTextureUpdateParamsV2.", false)]
		public void IssuePluginCustomTextureUpdate(IntPtr callback, Texture targetTexture, uint userData)
		{
			this.IssuePluginCustomTextureUpdateInternal(callback, targetTexture, userData, false);
		}

		// Token: 0x060030D4 RID: 12500 RVA: 0x0005F3DA File Offset: 0x0005D5DA
		[Obsolete("Use IssuePluginCustomTextureUpdateV2 to register TextureUpdate callbacks instead. Callbacks will be passed event IDs kUnityRenderingExtEventUpdateTextureBeginV2 or kUnityRenderingExtEventUpdateTextureEndV2, and data parameter of type UnityRenderingExtTextureUpdateParamsV2.", false)]
		public void IssuePluginCustomTextureUpdateV1(IntPtr callback, Texture targetTexture, uint userData)
		{
			this.IssuePluginCustomTextureUpdateInternal(callback, targetTexture, userData, false);
		}

		// Token: 0x060030D5 RID: 12501 RVA: 0x0005F3E8 File Offset: 0x0005D5E8
		public void IssuePluginCustomTextureUpdateV2(IntPtr callback, Texture targetTexture, uint userData)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.IssuePluginCustomTextureUpdateInternal(callback, targetTexture, userData, true);
		}

		// Token: 0x060030D6 RID: 12502 RVA: 0x0005F400 File Offset: 0x0005D600
		public void ProcessVTFeedback(RenderTargetIdentifier rt, IntPtr resolver, int slice, int x, int width, int y, int height, int mip)
		{
			this.ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
			this.Internal_ProcessVTFeedback(rt, resolver, slice, x, width, y, height, mip);
		}

		// Token: 0x060030D7 RID: 12503 RVA: 0x0005F42B File Offset: 0x0005D62B
		public void CopyBuffer(GraphicsBuffer source, GraphicsBuffer dest)
		{
			Graphics.ValidateCopyBuffer(source, dest);
			this.CopyBufferImpl(source, dest);
		}

		// Token: 0x060030D8 RID: 12504 RVA: 0x0005F440 File Offset: 0x0005D640
		[Obsolete("CommandBuffer.CreateGPUFence has been deprecated. Use CreateGraphicsFence instead (UnityUpgradable) -> CreateAsyncGraphicsFence(*)", false)]
		public GPUFence CreateGPUFence(SynchronisationStage stage)
		{
			return default(GPUFence);
		}

		// Token: 0x060030D9 RID: 12505 RVA: 0x0005F45C File Offset: 0x0005D65C
		[Obsolete("CommandBuffer.CreateGPUFence has been deprecated. Use CreateGraphicsFence instead (UnityUpgradable) -> CreateAsyncGraphicsFence()", false)]
		public GPUFence CreateGPUFence()
		{
			return default(GPUFence);
		}

		// Token: 0x060030DA RID: 12506 RVA: 0x00002925 File Offset: 0x00000B25
		[Obsolete("CommandBuffer.WaitOnGPUFence has been deprecated. Use WaitOnGraphicsFence instead (UnityUpgradable) -> WaitOnAsyncGraphicsFence(*)", false)]
		public void WaitOnGPUFence(GPUFence fence, SynchronisationStage stage)
		{
		}

		// Token: 0x060030DB RID: 12507 RVA: 0x00002925 File Offset: 0x00000B25
		[Obsolete("CommandBuffer.WaitOnGPUFence has been deprecated. Use WaitOnGraphicsFence instead (UnityUpgradable) -> WaitOnAsyncGraphicsFence(*)", false)]
		public void WaitOnGPUFence(GPUFence fence)
		{
		}

		// Token: 0x060030DC RID: 12508 RVA: 0x0005F477 File Offset: 0x0005D677
		[Obsolete("CommandBuffer.SetComputeBufferData has been deprecated. Use SetBufferData instead (UnityUpgradable) -> SetBufferData(*)", false)]
		public void SetComputeBufferData(ComputeBuffer buffer, Array data)
		{
			this.SetBufferData(buffer, data);
		}

		// Token: 0x060030DD RID: 12509 RVA: 0x0005F483 File Offset: 0x0005D683
		[Obsolete("CommandBuffer.SetComputeBufferData has been deprecated. Use SetBufferData instead (UnityUpgradable) -> SetBufferData<T>(*)", false)]
		public void SetComputeBufferData<T>(ComputeBuffer buffer, List<T> data) where T : struct
		{
			this.SetBufferData<T>(buffer, data);
		}

		// Token: 0x060030DE RID: 12510 RVA: 0x0005F48F File Offset: 0x0005D68F
		[Obsolete("CommandBuffer.SetComputeBufferData has been deprecated. Use SetBufferData instead (UnityUpgradable) -> SetBufferData<T>(*)", false)]
		public void SetComputeBufferData<T>(ComputeBuffer buffer, NativeArray<T> data) where T : struct
		{
			this.SetBufferData<T>(buffer, data);
		}

		// Token: 0x060030DF RID: 12511 RVA: 0x0005F49B File Offset: 0x0005D69B
		[Obsolete("CommandBuffer.SetComputeBufferData has been deprecated. Use SetBufferData instead (UnityUpgradable) -> SetBufferData(*)", false)]
		public void SetComputeBufferData(ComputeBuffer buffer, Array data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count)
		{
			this.SetBufferData(buffer, data, managedBufferStartIndex, graphicsBufferStartIndex, count);
		}

		// Token: 0x060030E0 RID: 12512 RVA: 0x0005F4AC File Offset: 0x0005D6AC
		[Obsolete("CommandBuffer.SetComputeBufferData has been deprecated. Use SetBufferData instead (UnityUpgradable) -> SetBufferData<T>(*)", false)]
		public void SetComputeBufferData<T>(ComputeBuffer buffer, List<T> data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count) where T : struct
		{
			this.SetBufferData<T>(buffer, data, managedBufferStartIndex, graphicsBufferStartIndex, count);
		}

		// Token: 0x060030E1 RID: 12513 RVA: 0x0005F4BD File Offset: 0x0005D6BD
		[Obsolete("CommandBuffer.SetComputeBufferData has been deprecated. Use SetBufferData instead (UnityUpgradable) -> SetBufferData<T>(*)", false)]
		public void SetComputeBufferData<T>(ComputeBuffer buffer, NativeArray<T> data, int nativeBufferStartIndex, int graphicsBufferStartIndex, int count) where T : struct
		{
			this.SetBufferData<T>(buffer, data, nativeBufferStartIndex, graphicsBufferStartIndex, count);
		}

		// Token: 0x060030E2 RID: 12514 RVA: 0x0005F4CE File Offset: 0x0005D6CE
		[Obsolete("CommandBuffer.SetComputeBufferCounterValue has been deprecated. Use SetBufferCounterValue instead (UnityUpgradable) -> SetBufferCounterValue(*)", false)]
		public void SetComputeBufferCounterValue(ComputeBuffer buffer, uint counterValue)
		{
			this.SetBufferCounterValue(buffer, counterValue);
		}

		// Token: 0x060030E3 RID: 12515
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void WaitAllAsyncReadbackRequests_Injected(IntPtr _unity_self);

		// Token: 0x060030E4 RID: 12516
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Internal_RequestAsyncReadback_1_Injected(IntPtr _unity_self, IntPtr src, Action<AsyncGPUReadbackRequest> callback, AsyncRequestNativeArrayData* nativeArrayData);

		// Token: 0x060030E5 RID: 12517
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Internal_RequestAsyncReadback_2_Injected(IntPtr _unity_self, IntPtr src, int size, int offset, Action<AsyncGPUReadbackRequest> callback, AsyncRequestNativeArrayData* nativeArrayData);

		// Token: 0x060030E6 RID: 12518
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Internal_RequestAsyncReadback_3_Injected(IntPtr _unity_self, IntPtr src, Action<AsyncGPUReadbackRequest> callback, AsyncRequestNativeArrayData* nativeArrayData);

		// Token: 0x060030E7 RID: 12519
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Internal_RequestAsyncReadback_4_Injected(IntPtr _unity_self, IntPtr src, int mipIndex, Action<AsyncGPUReadbackRequest> callback, AsyncRequestNativeArrayData* nativeArrayData);

		// Token: 0x060030E8 RID: 12520
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Internal_RequestAsyncReadback_5_Injected(IntPtr _unity_self, IntPtr src, int mipIndex, GraphicsFormat dstFormat, Action<AsyncGPUReadbackRequest> callback, AsyncRequestNativeArrayData* nativeArrayData);

		// Token: 0x060030E9 RID: 12521
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Internal_RequestAsyncReadback_6_Injected(IntPtr _unity_self, IntPtr src, int mipIndex, int x, int width, int y, int height, int z, int depth, Action<AsyncGPUReadbackRequest> callback, AsyncRequestNativeArrayData* nativeArrayData);

		// Token: 0x060030EA RID: 12522
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Internal_RequestAsyncReadback_7_Injected(IntPtr _unity_self, IntPtr src, int mipIndex, int x, int width, int y, int height, int z, int depth, GraphicsFormat dstFormat, Action<AsyncGPUReadbackRequest> callback, AsyncRequestNativeArrayData* nativeArrayData);

		// Token: 0x060030EB RID: 12523
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Internal_RequestAsyncReadback_8_Injected(IntPtr _unity_self, IntPtr src, Action<AsyncGPUReadbackRequest> callback, AsyncRequestNativeArrayData* nativeArrayData);

		// Token: 0x060030EC RID: 12524
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Internal_RequestAsyncReadback_9_Injected(IntPtr _unity_self, IntPtr src, int size, int offset, Action<AsyncGPUReadbackRequest> callback, AsyncRequestNativeArrayData* nativeArrayData);

		// Token: 0x060030ED RID: 12525
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetInvertCulling_Injected(IntPtr _unity_self, bool invertCulling);

		// Token: 0x060030EE RID: 12526
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ConvertTexture_Internal_Injected(IntPtr _unity_self, [In] ref RenderTargetIdentifier src, int srcElement, [In] ref RenderTargetIdentifier dst, int dstElement);

		// Token: 0x060030EF RID: 12527
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetSinglePassStereo_Injected(IntPtr _unity_self, SinglePassStereoMode mode);

		// Token: 0x060030F0 RID: 12528
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr CreateGPUFence_Internal_Injected(IntPtr _unity_self, GraphicsFenceType fenceType, SynchronisationStageFlags stage);

		// Token: 0x060030F1 RID: 12529
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void WaitOnGPUFence_Internal_Injected(IntPtr _unity_self, IntPtr fencePtr, SynchronisationStageFlags stage);

		// Token: 0x060030F2 RID: 12530
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReleaseBuffer_Injected(IntPtr _unity_self);

		// Token: 0x060030F3 RID: 12531
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetComputeFloatParam_Injected(IntPtr _unity_self, IntPtr computeShader, int nameID, float val);

		// Token: 0x060030F4 RID: 12532
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetComputeIntParam_Injected(IntPtr _unity_self, IntPtr computeShader, int nameID, int val);

		// Token: 0x060030F5 RID: 12533
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetComputeVectorParam_Injected(IntPtr _unity_self, IntPtr computeShader, int nameID, [In] ref Vector4 val);

		// Token: 0x060030F6 RID: 12534
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetComputeVectorArrayParam_Injected(IntPtr _unity_self, IntPtr computeShader, int nameID, ref ManagedSpanWrapper values);

		// Token: 0x060030F7 RID: 12535
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetComputeMatrixParam_Injected(IntPtr _unity_self, IntPtr computeShader, int nameID, [In] ref Matrix4x4 val);

		// Token: 0x060030F8 RID: 12536
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetComputeMatrixArrayParam_Injected(IntPtr _unity_self, IntPtr computeShader, int nameID, ref ManagedSpanWrapper values);

		// Token: 0x060030F9 RID: 12537
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetComputeFloats_Injected(IntPtr _unity_self, IntPtr computeShader, int nameID, ref ManagedSpanWrapper values);

		// Token: 0x060030FA RID: 12538
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetComputeInts_Injected(IntPtr _unity_self, IntPtr computeShader, int nameID, ref ManagedSpanWrapper values);

		// Token: 0x060030FB RID: 12539
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetComputeTextureParam_Injected(IntPtr _unity_self, IntPtr computeShader, int kernelIndex, int nameID, ref RenderTargetIdentifier rt, int mipLevel, RenderTextureSubElement element);

		// Token: 0x060030FC RID: 12540
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetComputeBufferParam_Injected(IntPtr _unity_self, IntPtr computeShader, int kernelIndex, int nameID, IntPtr buffer);

		// Token: 0x060030FD RID: 12541
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetComputeGraphicsBufferHandleParam_Injected(IntPtr _unity_self, IntPtr computeShader, int kernelIndex, int nameID, [In] ref GraphicsBufferHandle bufferHandle);

		// Token: 0x060030FE RID: 12542
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetComputeGraphicsBufferParam_Injected(IntPtr _unity_self, IntPtr computeShader, int kernelIndex, int nameID, IntPtr buffer);

		// Token: 0x060030FF RID: 12543
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetComputeConstantComputeBufferParam_Injected(IntPtr _unity_self, IntPtr computeShader, int nameID, IntPtr buffer, int offset, int size);

		// Token: 0x06003100 RID: 12544
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetComputeConstantGraphicsBufferParam_Injected(IntPtr _unity_self, IntPtr computeShader, int nameID, IntPtr buffer, int offset, int size);

		// Token: 0x06003101 RID: 12545
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetComputeParamsFromMaterial_Injected(IntPtr _unity_self, IntPtr computeShader, int kernelIndex, IntPtr material);

		// Token: 0x06003102 RID: 12546
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DispatchCompute_Injected(IntPtr _unity_self, IntPtr computeShader, int kernelIndex, int threadGroupsX, int threadGroupsY, int threadGroupsZ);

		// Token: 0x06003103 RID: 12547
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DispatchComputeIndirect_Injected(IntPtr _unity_self, IntPtr computeShader, int kernelIndex, IntPtr indirectBuffer, uint argsOffset);

		// Token: 0x06003104 RID: 12548
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DispatchComputeIndirectGraphicsBuffer_Injected(IntPtr _unity_self, IntPtr computeShader, int kernelIndex, IntPtr indirectBuffer, uint argsOffset);

		// Token: 0x06003105 RID: 12549
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetRayTracingComputeBufferParam_Injected(IntPtr _unity_self, IntPtr rayTracingShader, int nameID, IntPtr buffer);

		// Token: 0x06003106 RID: 12550
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetRayTracingGraphicsBufferParam_Injected(IntPtr _unity_self, IntPtr rayTracingShader, int nameID, IntPtr buffer);

		// Token: 0x06003107 RID: 12551
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetRayTracingGraphicsBufferHandleParam_Injected(IntPtr _unity_self, IntPtr rayTracingShader, int nameID, [In] ref GraphicsBufferHandle bufferHandle);

		// Token: 0x06003108 RID: 12552
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetRayTracingConstantComputeBufferParam_Injected(IntPtr _unity_self, IntPtr rayTracingShader, int nameID, IntPtr buffer, int offset, int size);

		// Token: 0x06003109 RID: 12553
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetRayTracingConstantGraphicsBufferParam_Injected(IntPtr _unity_self, IntPtr rayTracingShader, int nameID, IntPtr buffer, int offset, int size);

		// Token: 0x0600310A RID: 12554
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetRayTracingTextureParam_Injected(IntPtr _unity_self, IntPtr rayTracingShader, int nameID, ref RenderTargetIdentifier rt);

		// Token: 0x0600310B RID: 12555
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetRayTracingFloatParam_Injected(IntPtr _unity_self, IntPtr rayTracingShader, int nameID, float val);

		// Token: 0x0600310C RID: 12556
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetRayTracingIntParam_Injected(IntPtr _unity_self, IntPtr rayTracingShader, int nameID, int val);

		// Token: 0x0600310D RID: 12557
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetRayTracingVectorParam_Injected(IntPtr _unity_self, IntPtr rayTracingShader, int nameID, [In] ref Vector4 val);

		// Token: 0x0600310E RID: 12558
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetRayTracingVectorArrayParam_Injected(IntPtr _unity_self, IntPtr rayTracingShader, int nameID, ref ManagedSpanWrapper values);

		// Token: 0x0600310F RID: 12559
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetRayTracingMatrixParam_Injected(IntPtr _unity_self, IntPtr rayTracingShader, int nameID, [In] ref Matrix4x4 val);

		// Token: 0x06003110 RID: 12560
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetRayTracingMatrixArrayParam_Injected(IntPtr _unity_self, IntPtr rayTracingShader, int nameID, ref ManagedSpanWrapper values);

		// Token: 0x06003111 RID: 12561
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetRayTracingFloats_Injected(IntPtr _unity_self, IntPtr rayTracingShader, int nameID, ref ManagedSpanWrapper values);

		// Token: 0x06003112 RID: 12562
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetRayTracingInts_Injected(IntPtr _unity_self, IntPtr rayTracingShader, int nameID, ref ManagedSpanWrapper values);

		// Token: 0x06003113 RID: 12563
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_BuildRayTracingAccelerationStructure_Injected(IntPtr _unity_self, IntPtr accelerationStructure, [In] ref RayTracingAccelerationStructure.BuildSettings buildSettings);

		// Token: 0x06003114 RID: 12564
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetRayTracingAccelerationStructure_Injected(IntPtr _unity_self, IntPtr rayTracingShader, int nameID, IntPtr accelerationStructure);

		// Token: 0x06003115 RID: 12565
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetComputeRayTracingAccelerationStructure_Injected(IntPtr _unity_self, IntPtr computeShader, int kernelIndex, int nameID, IntPtr accelerationStructure);

		// Token: 0x06003116 RID: 12566
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetRayTracingShaderPass_Injected(IntPtr _unity_self, IntPtr rayTracingShader, ref ManagedSpanWrapper passName);

		// Token: 0x06003117 RID: 12567
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DispatchRays_Injected(IntPtr _unity_self, IntPtr rayTracingShader, ref ManagedSpanWrapper rayGenShaderName, uint width, uint height, uint depth, IntPtr camera);

		// Token: 0x06003118 RID: 12568
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DispatchRaysIndirect_Injected(IntPtr _unity_self, IntPtr rayTracingShader, ref ManagedSpanWrapper rayGenShaderName, IntPtr argsBuffer, uint argsOffset, IntPtr camera);

		// Token: 0x06003119 RID: 12569
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_GenerateMips_Injected(IntPtr _unity_self, [In] ref RenderTargetIdentifier rt);

		// Token: 0x0600311A RID: 12570
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_ResolveAntiAliasedSurface_Injected(IntPtr _unity_self, IntPtr rt, IntPtr target);

		// Token: 0x0600311B RID: 12571
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyCounterValueCC_Injected(IntPtr _unity_self, IntPtr src, IntPtr dst, uint dstOffsetBytes);

		// Token: 0x0600311C RID: 12572
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyCounterValueGC_Injected(IntPtr _unity_self, IntPtr src, IntPtr dst, uint dstOffsetBytes);

		// Token: 0x0600311D RID: 12573
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyCounterValueCG_Injected(IntPtr _unity_self, IntPtr src, IntPtr dst, uint dstOffsetBytes);

		// Token: 0x0600311E RID: 12574
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyCounterValueGG_Injected(IntPtr _unity_self, IntPtr src, IntPtr dst, uint dstOffsetBytes);

		// Token: 0x0600311F RID: 12575
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_name_Injected(IntPtr _unity_self, out ManagedSpanWrapper ret);

		// Token: 0x06003120 RID: 12576
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_name_Injected(IntPtr _unity_self, ref ManagedSpanWrapper value);

		// Token: 0x06003121 RID: 12577
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_sizeInBytes_Injected(IntPtr _unity_self);

		// Token: 0x06003122 RID: 12578
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Clear_Injected(IntPtr _unity_self);

		// Token: 0x06003123 RID: 12579
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawMesh_Injected(IntPtr _unity_self, IntPtr mesh, [In] ref Matrix4x4 matrix, IntPtr material, int submeshIndex, int shaderPass, IntPtr properties);

		// Token: 0x06003124 RID: 12580
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawMultipleMeshes_Injected(IntPtr _unity_self, ref ManagedSpanWrapper matrices, Mesh[] meshes, ref ManagedSpanWrapper subsetIndices, int count, IntPtr material, int shaderPass, IntPtr properties);

		// Token: 0x06003125 RID: 12581
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawRenderer_Injected(IntPtr _unity_self, IntPtr renderer, IntPtr material, int submeshIndex, int shaderPass);

		// Token: 0x06003126 RID: 12582
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawRendererList_Injected(IntPtr _unity_self, [In] ref RendererList rendererList);

		// Token: 0x06003127 RID: 12583
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawProcedural_Injected(IntPtr _unity_self, [In] ref Matrix4x4 matrix, IntPtr material, int shaderPass, MeshTopology topology, int vertexCount, int instanceCount, IntPtr properties);

		// Token: 0x06003128 RID: 12584
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawProceduralIndexed_Injected(IntPtr _unity_self, IntPtr indexBuffer, [In] ref Matrix4x4 matrix, IntPtr material, int shaderPass, MeshTopology topology, int indexCount, int instanceCount, IntPtr properties);

		// Token: 0x06003129 RID: 12585
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawProceduralIndirect_Injected(IntPtr _unity_self, [In] ref Matrix4x4 matrix, IntPtr material, int shaderPass, MeshTopology topology, IntPtr bufferWithArgs, int argsOffset, IntPtr properties);

		// Token: 0x0600312A RID: 12586
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawProceduralIndexedIndirect_Injected(IntPtr _unity_self, IntPtr indexBuffer, [In] ref Matrix4x4 matrix, IntPtr material, int shaderPass, MeshTopology topology, IntPtr bufferWithArgs, int argsOffset, IntPtr properties);

		// Token: 0x0600312B RID: 12587
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawProceduralIndirectGraphicsBuffer_Injected(IntPtr _unity_self, [In] ref Matrix4x4 matrix, IntPtr material, int shaderPass, MeshTopology topology, IntPtr bufferWithArgs, int argsOffset, IntPtr properties);

		// Token: 0x0600312C RID: 12588
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawProceduralIndexedIndirectGraphicsBuffer_Injected(IntPtr _unity_self, IntPtr indexBuffer, [In] ref Matrix4x4 matrix, IntPtr material, int shaderPass, MeshTopology topology, IntPtr bufferWithArgs, int argsOffset, IntPtr properties);

		// Token: 0x0600312D RID: 12589
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawMeshInstanced_Injected(IntPtr _unity_self, IntPtr mesh, int submeshIndex, IntPtr material, int shaderPass, ref ManagedSpanWrapper matrices, int count, IntPtr properties);

		// Token: 0x0600312E RID: 12590
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawMeshInstancedProcedural_Injected(IntPtr _unity_self, IntPtr mesh, int submeshIndex, IntPtr material, int shaderPass, int count, IntPtr properties);

		// Token: 0x0600312F RID: 12591
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawMeshInstancedIndirect_Injected(IntPtr _unity_self, IntPtr mesh, int submeshIndex, IntPtr material, int shaderPass, IntPtr bufferWithArgs, int argsOffset, IntPtr properties);

		// Token: 0x06003130 RID: 12592
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawMeshInstancedIndirectGraphicsBuffer_Injected(IntPtr _unity_self, IntPtr mesh, int submeshIndex, IntPtr material, int shaderPass, IntPtr bufferWithArgs, int argsOffset, IntPtr properties);

		// Token: 0x06003131 RID: 12593
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawOcclusionMesh_Injected(IntPtr _unity_self, [In] ref RectInt normalizedCamViewport);

		// Token: 0x06003132 RID: 12594
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetRandomWriteTarget_Texture_Injected(IntPtr _unity_self, int index, ref RenderTargetIdentifier rt);

		// Token: 0x06003133 RID: 12595
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetRandomWriteTarget_Buffer_Injected(IntPtr _unity_self, int index, IntPtr uav, bool preserveCounterValue);

		// Token: 0x06003134 RID: 12596
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetRandomWriteTarget_GraphicsBuffer_Injected(IntPtr _unity_self, int index, IntPtr uav, bool preserveCounterValue);

		// Token: 0x06003135 RID: 12597
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClearRandomWriteTargets_Injected(IntPtr _unity_self);

		// Token: 0x06003136 RID: 12598
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetViewport_Injected(IntPtr _unity_self, [In] ref Rect pixelRect);

		// Token: 0x06003137 RID: 12599
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EnableScissorRect_Injected(IntPtr _unity_self, [In] ref Rect scissor);

		// Token: 0x06003138 RID: 12600
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DisableScissorRect_Injected(IntPtr _unity_self);

		// Token: 0x06003139 RID: 12601
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyTexture_Internal_Injected(IntPtr _unity_self, ref RenderTargetIdentifier src, int srcElement, int srcMip, int srcX, int srcY, int srcWidth, int srcHeight, ref RenderTargetIdentifier dst, int dstElement, int dstMip, int dstX, int dstY, int mode);

		// Token: 0x0600313A RID: 12602
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Blit_Texture_Injected(IntPtr _unity_self, IntPtr source, ref RenderTargetIdentifier dest, IntPtr mat, int pass, [In] ref Vector2 scale, [In] ref Vector2 offset, int sourceDepthSlice, int destDepthSlice);

		// Token: 0x0600313B RID: 12603
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Blit_Identifier_Injected(IntPtr _unity_self, ref RenderTargetIdentifier source, ref RenderTargetIdentifier dest, IntPtr mat, int pass, [In] ref Vector2 scale, [In] ref Vector2 offset, int sourceDepthSlice, int destDepthSlice);

		// Token: 0x0600313C RID: 12604
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetTemporaryRT_Injected(IntPtr _unity_self, int nameID, int width, int height, FilterMode filter, GraphicsFormat colorFormat, GraphicsFormat depthStencilFormat, int antiAliasing, bool enableRandomWrite, RenderTextureMemoryless memorylessMode, bool useDynamicScale, ShadowSamplingMode shadowSamplingMode);

		// Token: 0x0600313D RID: 12605
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetTemporaryRTWithDescriptor_Injected(IntPtr _unity_self, int nameID, [In] ref RenderTextureDescriptor desc, FilterMode filter);

		// Token: 0x0600313E RID: 12606
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetTemporaryRTArray_Injected(IntPtr _unity_self, int nameID, int width, int height, int slices, FilterMode filter, GraphicsFormat colorFormat, GraphicsFormat depthStencilFormat, int antiAliasing, bool enableRandomWrite, bool useDynamicScale, ShadowSamplingMode shadowSamplingMode);

		// Token: 0x0600313F RID: 12607
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReleaseTemporaryRT_Injected(IntPtr _unity_self, int nameID);

		// Token: 0x06003140 RID: 12608
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalFloat_Injected(IntPtr _unity_self, int nameID, float value);

		// Token: 0x06003141 RID: 12609
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalInt_Injected(IntPtr _unity_self, int nameID, int value);

		// Token: 0x06003142 RID: 12610
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalInteger_Injected(IntPtr _unity_self, int nameID, int value);

		// Token: 0x06003143 RID: 12611
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalVector_Injected(IntPtr _unity_self, int nameID, [In] ref Vector4 value);

		// Token: 0x06003144 RID: 12612
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalColor_Injected(IntPtr _unity_self, int nameID, [In] ref Color value);

		// Token: 0x06003145 RID: 12613
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalMatrix_Injected(IntPtr _unity_self, int nameID, [In] ref Matrix4x4 value);

		// Token: 0x06003146 RID: 12614
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EnableShaderKeyword_Injected(IntPtr _unity_self, ref ManagedSpanWrapper keyword);

		// Token: 0x06003147 RID: 12615
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EnableGlobalKeyword_Injected(IntPtr _unity_self, [In] ref GlobalKeyword keyword);

		// Token: 0x06003148 RID: 12616
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EnableMaterialKeyword_Injected(IntPtr _unity_self, IntPtr material, [In] ref LocalKeyword keyword);

		// Token: 0x06003149 RID: 12617
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EnableComputeKeyword_Injected(IntPtr _unity_self, IntPtr computeShader, [In] ref LocalKeyword keyword);

		// Token: 0x0600314A RID: 12618
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DisableShaderKeyword_Injected(IntPtr _unity_self, ref ManagedSpanWrapper keyword);

		// Token: 0x0600314B RID: 12619
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DisableGlobalKeyword_Injected(IntPtr _unity_self, [In] ref GlobalKeyword keyword);

		// Token: 0x0600314C RID: 12620
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DisableMaterialKeyword_Injected(IntPtr _unity_self, IntPtr material, [In] ref LocalKeyword keyword);

		// Token: 0x0600314D RID: 12621
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DisableComputeKeyword_Injected(IntPtr _unity_self, IntPtr computeShader, [In] ref LocalKeyword keyword);

		// Token: 0x0600314E RID: 12622
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalKeyword_Injected(IntPtr _unity_self, [In] ref GlobalKeyword keyword, bool value);

		// Token: 0x0600314F RID: 12623
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetMaterialKeyword_Injected(IntPtr _unity_self, IntPtr material, [In] ref LocalKeyword keyword, bool value);

		// Token: 0x06003150 RID: 12624
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetComputeKeyword_Injected(IntPtr _unity_self, IntPtr computeShader, [In] ref LocalKeyword keyword, bool value);

		// Token: 0x06003151 RID: 12625
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetViewMatrix_Injected(IntPtr _unity_self, [In] ref Matrix4x4 view);

		// Token: 0x06003152 RID: 12626
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetProjectionMatrix_Injected(IntPtr _unity_self, [In] ref Matrix4x4 proj);

		// Token: 0x06003153 RID: 12627
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetViewProjectionMatrices_Injected(IntPtr _unity_self, [In] ref Matrix4x4 view, [In] ref Matrix4x4 proj);

		// Token: 0x06003154 RID: 12628
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalDepthBias_Injected(IntPtr _unity_self, float bias, float slopeBias);

		// Token: 0x06003155 RID: 12629
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetExecutionFlags_Injected(IntPtr _unity_self, CommandBufferExecutionFlags flags);

		// Token: 0x06003156 RID: 12630
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ValidateAgainstExecutionFlags_Injected(IntPtr _unity_self, CommandBufferExecutionFlags requiredFlags, CommandBufferExecutionFlags invalidFlags);

		// Token: 0x06003157 RID: 12631
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalFloatArrayListImpl_Injected(IntPtr _unity_self, int nameID, object values);

		// Token: 0x06003158 RID: 12632
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalVectorArrayListImpl_Injected(IntPtr _unity_self, int nameID, object values);

		// Token: 0x06003159 RID: 12633
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalMatrixArrayListImpl_Injected(IntPtr _unity_self, int nameID, object values);

		// Token: 0x0600315A RID: 12634
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalFloatArray_Injected(IntPtr _unity_self, int nameID, ref ManagedSpanWrapper values);

		// Token: 0x0600315B RID: 12635
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalVectorArray_Injected(IntPtr _unity_self, int nameID, ref ManagedSpanWrapper values);

		// Token: 0x0600315C RID: 12636
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalMatrixArray_Injected(IntPtr _unity_self, int nameID, ref ManagedSpanWrapper values);

		// Token: 0x0600315D RID: 12637
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLateLatchProjectionMatrices_Injected(IntPtr _unity_self, ref ManagedSpanWrapper projectionMat);

		// Token: 0x0600315E RID: 12638
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MarkLateLatchMatrixShaderPropertyID_Injected(IntPtr _unity_self, CameraLateLatchMatrixType matrixPropertyType, int shaderPropertyID);

		// Token: 0x0600315F RID: 12639
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UnmarkLateLatchMatrix_Injected(IntPtr _unity_self, CameraLateLatchMatrixType matrixPropertyType);

		// Token: 0x06003160 RID: 12640
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalTexture_Impl_Injected(IntPtr _unity_self, int nameID, ref RenderTargetIdentifier rt, RenderTextureSubElement element);

		// Token: 0x06003161 RID: 12641
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalBufferInternal_Injected(IntPtr _unity_self, int nameID, IntPtr value);

		// Token: 0x06003162 RID: 12642
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalGraphicsBufferInternal_Injected(IntPtr _unity_self, int nameID, IntPtr value);

		// Token: 0x06003163 RID: 12643
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalRayTracingAccelerationStructureInternal_Injected(IntPtr _unity_self, IntPtr accelerationStructure, int nameID);

		// Token: 0x06003164 RID: 12644
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetShadowSamplingMode_Impl_Injected(IntPtr _unity_self, ref RenderTargetIdentifier shadowmap, ShadowSamplingMode mode);

		// Token: 0x06003165 RID: 12645
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void IssuePluginEventInternal_Injected(IntPtr _unity_self, IntPtr callback, int eventID);

		// Token: 0x06003166 RID: 12646
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void BeginSample_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name);

		// Token: 0x06003167 RID: 12647
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EndSample_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name);

		// Token: 0x06003168 RID: 12648
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void BeginSample_CustomSampler_Injected(IntPtr _unity_self, IntPtr sampler);

		// Token: 0x06003169 RID: 12649
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EndSample_CustomSampler_Injected(IntPtr _unity_self, IntPtr sampler);

		// Token: 0x0600316A RID: 12650
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void BeginSample_ProfilerMarker_Injected(IntPtr _unity_self, IntPtr markerHandle);

		// Token: 0x0600316B RID: 12651
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EndSample_ProfilerMarker_Injected(IntPtr _unity_self, IntPtr markerHandle);

		// Token: 0x0600316C RID: 12652
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void IssuePluginEventAndDataInternal_Injected(IntPtr _unity_self, IntPtr callback, int eventID, IntPtr data);

		// Token: 0x0600316D RID: 12653
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void IssuePluginEventAndDataInternalWithFlags_Injected(IntPtr _unity_self, IntPtr callback, int eventID, CustomMarkerCallbackFlags flags, IntPtr data);

		// Token: 0x0600316E RID: 12654
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void IssuePluginCustomBlitInternal_Injected(IntPtr _unity_self, IntPtr callback, uint command, ref RenderTargetIdentifier source, ref RenderTargetIdentifier dest, uint commandParam, uint commandFlags);

		// Token: 0x0600316F RID: 12655
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void IssuePluginCustomTextureUpdateInternal_Injected(IntPtr _unity_self, IntPtr callback, IntPtr targetTexture, uint userData, bool useNewUnityRenderingExtTextureUpdateParamsV2);

		// Token: 0x06003170 RID: 12656
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalConstantBufferInternal_Injected(IntPtr _unity_self, IntPtr buffer, int nameID, int offset, int size);

		// Token: 0x06003171 RID: 12657
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalConstantGraphicsBufferInternal_Injected(IntPtr _unity_self, IntPtr buffer, int nameID, int offset, int size);

		// Token: 0x06003172 RID: 12658
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void IncrementUpdateCount_Injected(IntPtr _unity_self, [In] ref RenderTargetIdentifier dest);

		// Token: 0x06003173 RID: 12659
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetInstanceMultiplier_Injected(IntPtr _unity_self, uint multiplier);

		// Token: 0x06003174 RID: 12660
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetFoveatedRenderingMode_Injected(IntPtr _unity_self, FoveatedRenderingMode foveatedRenderingMode);

		// Token: 0x06003175 RID: 12661
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetWireframe_Injected(IntPtr _unity_self, bool enable);

		// Token: 0x06003176 RID: 12662
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ConfigureFoveatedRendering_Injected(IntPtr _unity_self, IntPtr platformData);

		// Token: 0x06003177 RID: 12663
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClearRenderTargetSingle_Internal_Injected(IntPtr _unity_self, RTClearFlags clearFlags, [In] ref Color color, float depth, uint stencil);

		// Token: 0x06003178 RID: 12664
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClearRenderTargetMulti_Internal_Injected(IntPtr _unity_self, RTClearFlags clearFlags, ref ManagedSpanWrapper colors, float depth, uint stencil);

		// Token: 0x06003179 RID: 12665
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetRenderTargetSingle_Internal_Injected(IntPtr _unity_self, [In] ref RenderTargetIdentifier rt, RenderBufferLoadAction colorLoadAction, RenderBufferStoreAction colorStoreAction, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction);

		// Token: 0x0600317A RID: 12666
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetRenderTargetColorDepth_Internal_Injected(IntPtr _unity_self, [In] ref RenderTargetIdentifier color, [In] ref RenderTargetIdentifier depth, RenderBufferLoadAction colorLoadAction, RenderBufferStoreAction colorStoreAction, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction, RenderTargetFlags flags);

		// Token: 0x0600317B RID: 12667
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetRenderTargetMulti_Internal_Injected(IntPtr _unity_self, ref ManagedSpanWrapper colors, [In] ref RenderTargetIdentifier depth, ref ManagedSpanWrapper colorLoadActions, ref ManagedSpanWrapper colorStoreActions, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction, RenderTargetFlags flags);

		// Token: 0x0600317C RID: 12668
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetRenderTargetColorDepthSubtarget_Injected(IntPtr _unity_self, [In] ref RenderTargetIdentifier color, [In] ref RenderTargetIdentifier depth, RenderBufferLoadAction colorLoadAction, RenderBufferStoreAction colorStoreAction, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction, int mipLevel, CubemapFace cubemapFace, int depthSlice);

		// Token: 0x0600317D RID: 12669
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetRenderTargetMultiSubtarget_Injected(IntPtr _unity_self, ref ManagedSpanWrapper colors, [In] ref RenderTargetIdentifier depth, ref ManagedSpanWrapper colorLoadActions, ref ManagedSpanWrapper colorStoreActions, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction, int mipLevel, CubemapFace cubemapFace, int depthSlice);

		// Token: 0x0600317E RID: 12670
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_ProcessVTFeedback_Injected(IntPtr _unity_self, [In] ref RenderTargetIdentifier rt, IntPtr resolver, int slice, int x, int width, int y, int height, int mip);

		// Token: 0x0600317F RID: 12671
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetComputeBufferNativeData_Injected(IntPtr _unity_self, IntPtr buffer, IntPtr data, int nativeBufferStartIndex, int graphicsBufferStartIndex, int count, int elemSize);

		// Token: 0x06003180 RID: 12672
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetComputeBufferData_Injected(IntPtr _unity_self, IntPtr buffer, Array data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count, int elemSize);

		// Token: 0x06003181 RID: 12673
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetComputeBufferCounterValue_Injected(IntPtr _unity_self, IntPtr buffer, uint counterValue);

		// Token: 0x06003182 RID: 12674
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetGraphicsBufferNativeData_Injected(IntPtr _unity_self, IntPtr buffer, IntPtr data, int nativeBufferStartIndex, int graphicsBufferStartIndex, int count, int elemSize);

		// Token: 0x06003183 RID: 12675
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetGraphicsBufferData_Injected(IntPtr _unity_self, IntPtr buffer, Array data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count, int elemSize);

		// Token: 0x06003184 RID: 12676
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetGraphicsBufferCounterValue_Injected(IntPtr _unity_self, IntPtr buffer, uint counterValue);

		// Token: 0x06003185 RID: 12677
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyBufferImpl_Injected(IntPtr _unity_self, IntPtr source, IntPtr dest);

		// Token: 0x06003186 RID: 12678
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void BeginRenderPass_Internal_Injected(IntPtr _unity_self, int width, int height, int volumeDepth, int samples, ref ManagedSpanWrapper attachments, int depthAttachmentIndex, ref ManagedSpanWrapper subPasses, ref ManagedSpanWrapper debugNameUtf8);

		// Token: 0x06003187 RID: 12679
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void NextSubPass_Internal_Injected(IntPtr _unity_self);

		// Token: 0x06003188 RID: 12680
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EndRenderPass_Internal_Injected(IntPtr _unity_self);

		// Token: 0x06003189 RID: 12681
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetupCameraProperties_Internal_Injected(IntPtr _unity_self, IntPtr camera);

		// Token: 0x0600318A RID: 12682
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InvokeOnRenderObjectCallbacks_Internal_Injected(IntPtr _unity_self);

		// Token: 0x04000F4E RID: 3918
		public static bool ThrowOnSetRenderTarget;

		// Token: 0x04000F4F RID: 3919
		internal IntPtr m_Ptr;

		// Token: 0x020004ED RID: 1261
		internal static class BindingsMarshaller
		{
			// Token: 0x0600318B RID: 12683 RVA: 0x0005F4DA File Offset: 0x0005D6DA
			public static CommandBuffer ConvertToManaged(IntPtr ptr)
			{
				return new CommandBuffer(ptr);
			}

			// Token: 0x0600318C RID: 12684 RVA: 0x0005F4E2 File Offset: 0x0005D6E2
			public static IntPtr ConvertToNative(CommandBuffer commandBuffer)
			{
				return commandBuffer.m_Ptr;
			}
		}
	}
}
