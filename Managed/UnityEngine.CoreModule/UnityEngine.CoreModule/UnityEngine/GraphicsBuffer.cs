using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200018D RID: 397
	[NativeHeader("Runtime/Export/Graphics/GraphicsBuffer.bindings.h")]
	[NativeHeader("Runtime/Shaders/GraphicsBuffer.h")]
	[UsedByNativeCode]
	public sealed class GraphicsBuffer : IDisposable
	{
		// Token: 0x0600134A RID: 4938 RVA: 0x000213E8 File Offset: 0x0001F5E8
		~GraphicsBuffer()
		{
			this.Dispose(false);
		}

		// Token: 0x0600134B RID: 4939 RVA: 0x0002141C File Offset: 0x0001F61C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x00021430 File Offset: 0x0001F630
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				GraphicsBuffer.DestroyBuffer(this);
			}
			else
			{
				bool flag = this.m_Ptr != IntPtr.Zero;
				if (flag)
				{
					Debug.LogWarning("GarbageCollector disposing of GraphicsBuffer. Please use GraphicsBuffer.Release() or .Dispose() to manually release the buffer.");
				}
			}
			this.m_Ptr = IntPtr.Zero;
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x0002147C File Offset: 0x0001F67C
		private static bool RequiresCompute(GraphicsBuffer.Target target)
		{
			GraphicsBuffer.Target target2 = GraphicsBuffer.Target.Structured | GraphicsBuffer.Target.Raw | GraphicsBuffer.Target.Append | GraphicsBuffer.Target.Counter | GraphicsBuffer.Target.IndirectArguments;
			return (target & target2) > (GraphicsBuffer.Target)0;
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x0002149C File Offset: 0x0001F69C
		private static bool IsVertexIndexOrCopyOnly(GraphicsBuffer.Target target)
		{
			GraphicsBuffer.Target target2 = GraphicsBuffer.Target.Vertex | GraphicsBuffer.Target.Index | GraphicsBuffer.Target.CopySource | GraphicsBuffer.Target.CopyDestination;
			return (target & target2) == target;
		}

		// Token: 0x0600134F RID: 4943
		[FreeFunction("GraphicsBuffer_Bindings::InitBuffer")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr InitBuffer(GraphicsBuffer.Target target, GraphicsBuffer.UsageFlags usageFlags, int count, int stride);

		// Token: 0x06001350 RID: 4944 RVA: 0x000214B8 File Offset: 0x0001F6B8
		[FreeFunction("GraphicsBuffer_Bindings::DestroyBuffer")]
		private static void DestroyBuffer(GraphicsBuffer buf)
		{
			GraphicsBuffer.DestroyBuffer_Injected((buf == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(buf));
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x000214D9 File Offset: 0x0001F6D9
		private GraphicsBuffer(IntPtr ptr)
		{
			this.m_Ptr = ptr;
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x000214EC File Offset: 0x0001F6EC
		public GraphicsBuffer(GraphicsBuffer.Target target, int count, int stride)
		{
			GraphicsBuffer.UsageFlags usageFlags = (((target & (GraphicsBuffer.Target.Vertex | GraphicsBuffer.Target.Index)) == target) ? GraphicsBuffer.UsageFlags.LockBufferForWrite : GraphicsBuffer.UsageFlags.None);
			this.InternalInitialization(target, usageFlags, count, stride);
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x0002151B File Offset: 0x0001F71B
		public GraphicsBuffer(GraphicsBuffer.Target target, GraphicsBuffer.UsageFlags usageFlags, int count, int stride)
		{
			this.InternalInitialization(target, usageFlags, count, stride);
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x00021534 File Offset: 0x0001F734
		private void InternalInitialization(GraphicsBuffer.Target target, GraphicsBuffer.UsageFlags usageFlags, int count, int stride)
		{
			bool flag = GraphicsBuffer.RequiresCompute(target) && !SystemInfo.supportsComputeShaders;
			if (flag)
			{
				throw new ArgumentException("Attempting to create a graphics buffer that requires compute shader support, but compute shaders are not supported on this platform. Target: " + target.ToString());
			}
			bool flag2 = count <= 0;
			if (flag2)
			{
				throw new ArgumentException("Attempting to create a zero length graphics buffer", "count");
			}
			bool flag3 = stride <= 0;
			if (flag3)
			{
				throw new ArgumentException("Attempting to create a graphics buffer with a negative or null stride", "stride");
			}
			bool flag4 = (target & GraphicsBuffer.Target.Index) != (GraphicsBuffer.Target)0 && stride != 2 && stride != 4;
			if (flag4)
			{
				throw new ArgumentException("Attempting to create an index buffer with an invalid stride: " + stride.ToString(), "stride");
			}
			bool flag5 = !GraphicsBuffer.IsVertexIndexOrCopyOnly(target) && stride % 4 != 0;
			if (flag5)
			{
				throw new ArgumentException("Stride must be a multiple of 4 unless the buffer is only used as a vertex buffer and/or index buffer ", "stride");
			}
			long num = (long)count * (long)stride;
			long maxGraphicsBufferSize = SystemInfo.maxGraphicsBufferSize;
			bool flag6 = num > maxGraphicsBufferSize;
			if (flag6)
			{
				throw new ArgumentException(string.Format("The total size of the graphics buffer ({0} bytes) exceeds the maximum buffer size. Maximum supported buffer size: {1} bytes.", num, maxGraphicsBufferSize));
			}
			bool flag7 = (usageFlags & GraphicsBuffer.UsageFlags.LockBufferForWrite) != GraphicsBuffer.UsageFlags.None && (target & GraphicsBuffer.Target.CopyDestination) > (GraphicsBuffer.Target)0;
			if (flag7)
			{
				throw new ArgumentException("Attempting to create a LockBufferForWrite capable buffer that can be copied into. LockBufferForWrite buffers are read-only on the GPU.");
			}
			this.m_Ptr = GraphicsBuffer.InitBuffer(target, usageFlags, count, stride);
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x00021677 File Offset: 0x0001F877
		public void Release()
		{
			this.Dispose();
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x00021684 File Offset: 0x0001F884
		[FreeFunction("GraphicsBuffer_Bindings::IsValidBuffer")]
		private static bool IsValidBuffer(GraphicsBuffer buf)
		{
			return GraphicsBuffer.IsValidBuffer_Injected((buf == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(buf));
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x000216A8 File Offset: 0x0001F8A8
		public bool IsValid()
		{
			return this.m_Ptr != IntPtr.Zero && GraphicsBuffer.IsValidBuffer(this);
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06001358 RID: 4952 RVA: 0x000216D8 File Offset: 0x0001F8D8
		public int count
		{
			get
			{
				IntPtr intPtr = GraphicsBuffer.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return GraphicsBuffer.get_count_Injected(intPtr);
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06001359 RID: 4953 RVA: 0x000216FC File Offset: 0x0001F8FC
		public int stride
		{
			get
			{
				IntPtr intPtr = GraphicsBuffer.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return GraphicsBuffer.get_stride_Injected(intPtr);
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x0600135A RID: 4954 RVA: 0x00021720 File Offset: 0x0001F920
		public GraphicsBuffer.Target target
		{
			get
			{
				IntPtr intPtr = GraphicsBuffer.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return GraphicsBuffer.get_target_Injected(intPtr);
			}
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x00021744 File Offset: 0x0001F944
		[FreeFunction(Name = "GraphicsBuffer_Bindings::GetUsageFlags", HasExplicitThis = true)]
		private GraphicsBuffer.UsageFlags GetUsageFlags()
		{
			IntPtr intPtr = GraphicsBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return GraphicsBuffer.GetUsageFlags_Injected(intPtr);
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x0600135C RID: 4956 RVA: 0x00021768 File Offset: 0x0001F968
		public GraphicsBuffer.UsageFlags usageFlags
		{
			get
			{
				return this.GetUsageFlags();
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x0600135D RID: 4957 RVA: 0x00021780 File Offset: 0x0001F980
		public GraphicsBufferHandle bufferHandle
		{
			get
			{
				IntPtr intPtr = GraphicsBuffer.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				GraphicsBufferHandle graphicsBufferHandle;
				GraphicsBuffer.get_bufferHandle_Injected(intPtr, out graphicsBufferHandle);
				return graphicsBufferHandle;
			}
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x000217A8 File Offset: 0x0001F9A8
		[SecuritySafeCritical]
		public void SetData(Array data)
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			bool flag2 = !UnsafeUtility.IsArrayBlittable(data);
			if (flag2)
			{
				throw new ArgumentException(string.Format("Array passed to GraphicsBuffer.SetData(array) must be blittable.\n{0}", UnsafeUtility.GetReasonForArrayNonBlittable(data)));
			}
			this.InternalSetData(data, 0, 0, data.Length, UnsafeUtility.SizeOf(data.GetType().GetElementType()));
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x00021810 File Offset: 0x0001FA10
		[SecuritySafeCritical]
		public void SetData<T>(List<T> data) where T : struct
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			bool flag2 = !UnsafeUtility.IsGenericListBlittable<T>();
			if (flag2)
			{
				throw new ArgumentException(string.Format("List<{0}> passed to GraphicsBuffer.SetData(List<>) must be blittable.\n{1}", typeof(T), UnsafeUtility.GetReasonForGenericListNonBlittable<T>()));
			}
			this.InternalSetData(NoAllocHelpers.ExtractArrayFromList<T>(data), 0, 0, NoAllocHelpers.SafeLength<T>(data), Marshal.SizeOf(typeof(T)));
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x00021881 File Offset: 0x0001FA81
		[SecuritySafeCritical]
		public void SetData<T>(NativeArray<T> data) where T : struct
		{
			this.InternalSetNativeData((IntPtr)data.GetUnsafeReadOnlyPtr<T>(), 0, 0, data.Length, UnsafeUtility.SizeOf<T>());
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x000218A4 File Offset: 0x0001FAA4
		[SecuritySafeCritical]
		public void SetData(Array data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count)
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			bool flag2 = !UnsafeUtility.IsArrayBlittable(data);
			if (flag2)
			{
				throw new ArgumentException(string.Format("Array passed to GraphicsBuffer.SetData(array) must be blittable.\n{0}", UnsafeUtility.GetReasonForArrayNonBlittable(data)));
			}
			bool flag3 = managedBufferStartIndex < 0 || graphicsBufferStartIndex < 0 || count < 0 || managedBufferStartIndex + count > data.Length;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad indices/count arguments (managedBufferStartIndex:{0} graphicsBufferStartIndex:{1} count:{2})", managedBufferStartIndex, graphicsBufferStartIndex, count));
			}
			this.InternalSetData(data, managedBufferStartIndex, graphicsBufferStartIndex, count, Marshal.SizeOf(data.GetType().GetElementType()));
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x00021948 File Offset: 0x0001FB48
		[SecuritySafeCritical]
		public void SetData<T>(List<T> data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count) where T : struct
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			bool flag2 = !UnsafeUtility.IsGenericListBlittable<T>();
			if (flag2)
			{
				throw new ArgumentException(string.Format("List<{0}> passed to GraphicsBuffer.SetData(List<>) must be blittable.\n{1}", typeof(T), UnsafeUtility.GetReasonForGenericListNonBlittable<T>()));
			}
			bool flag3 = managedBufferStartIndex < 0 || graphicsBufferStartIndex < 0 || count < 0 || managedBufferStartIndex + count > data.Count;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad indices/count arguments (managedBufferStartIndex:{0} graphicsBufferStartIndex:{1} count:{2})", managedBufferStartIndex, graphicsBufferStartIndex, count));
			}
			this.InternalSetData(NoAllocHelpers.ExtractArrayFromList<T>(data), managedBufferStartIndex, graphicsBufferStartIndex, count, Marshal.SizeOf(typeof(T)));
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x000219F8 File Offset: 0x0001FBF8
		[SecuritySafeCritical]
		public void SetData<T>(NativeArray<T> data, int nativeBufferStartIndex, int graphicsBufferStartIndex, int count) where T : struct
		{
			bool flag = nativeBufferStartIndex < 0 || graphicsBufferStartIndex < 0 || count < 0 || nativeBufferStartIndex + count > data.Length;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad indices/count arguments (nativeBufferStartIndex:{0} graphicsBufferStartIndex:{1} count:{2})", nativeBufferStartIndex, graphicsBufferStartIndex, count));
			}
			this.InternalSetNativeData((IntPtr)data.GetUnsafeReadOnlyPtr<T>(), nativeBufferStartIndex, graphicsBufferStartIndex, count, UnsafeUtility.SizeOf<T>());
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x00021A68 File Offset: 0x0001FC68
		[FreeFunction(Name = "GraphicsBuffer_Bindings::InternalSetNativeData", HasExplicitThis = true, ThrowsException = true)]
		[SecurityCritical]
		private void InternalSetNativeData(IntPtr data, int nativeBufferStartIndex, int graphicsBufferStartIndex, int count, int elemSize)
		{
			IntPtr intPtr = GraphicsBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			GraphicsBuffer.InternalSetNativeData_Injected(intPtr, data, nativeBufferStartIndex, graphicsBufferStartIndex, count, elemSize);
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x00021A94 File Offset: 0x0001FC94
		[SecurityCritical]
		[FreeFunction(Name = "GraphicsBuffer_Bindings::InternalSetData", HasExplicitThis = true, ThrowsException = true)]
		private void InternalSetData(Array data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count, int elemSize)
		{
			IntPtr intPtr = GraphicsBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			GraphicsBuffer.InternalSetData_Injected(intPtr, data, managedBufferStartIndex, graphicsBufferStartIndex, count, elemSize);
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x00021AC0 File Offset: 0x0001FCC0
		[SecurityCritical]
		public void GetData(Array data)
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			bool flag2 = !UnsafeUtility.IsArrayBlittable(data);
			if (flag2)
			{
				throw new ArgumentException(string.Format("Array passed to GraphicsBuffer.GetData(array) must be blittable.\n{0}", UnsafeUtility.GetReasonForArrayNonBlittable(data)));
			}
			this.InternalGetData(data, 0, 0, data.Length, Marshal.SizeOf(data.GetType().GetElementType()));
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x00021B28 File Offset: 0x0001FD28
		[SecurityCritical]
		public void GetData(Array data, int managedBufferStartIndex, int computeBufferStartIndex, int count)
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			bool flag2 = !UnsafeUtility.IsArrayBlittable(data);
			if (flag2)
			{
				throw new ArgumentException(string.Format("Array passed to GraphicsBuffer.GetData(array) must be blittable.\n{0}", UnsafeUtility.GetReasonForArrayNonBlittable(data)));
			}
			bool flag3 = managedBufferStartIndex < 0 || computeBufferStartIndex < 0 || count < 0 || managedBufferStartIndex + count > data.Length;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad indices/count argument (managedBufferStartIndex:{0} computeBufferStartIndex:{1} count:{2})", managedBufferStartIndex, computeBufferStartIndex, count));
			}
			this.InternalGetData(data, managedBufferStartIndex, computeBufferStartIndex, count, Marshal.SizeOf(data.GetType().GetElementType()));
		}

		// Token: 0x06001368 RID: 4968 RVA: 0x00021BCC File Offset: 0x0001FDCC
		[FreeFunction(Name = "GraphicsBuffer_Bindings::InternalGetData", HasExplicitThis = true, ThrowsException = true)]
		[SecurityCritical]
		private void InternalGetData(Array data, int managedBufferStartIndex, int computeBufferStartIndex, int count, int elemSize)
		{
			IntPtr intPtr = GraphicsBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			GraphicsBuffer.InternalGetData_Injected(intPtr, data, managedBufferStartIndex, computeBufferStartIndex, count, elemSize);
		}

		// Token: 0x06001369 RID: 4969 RVA: 0x00021BF8 File Offset: 0x0001FDF8
		[FreeFunction(Name = "GraphicsBuffer_Bindings::InternalGetNativeBufferPtr", HasExplicitThis = true)]
		public IntPtr GetNativeBufferPtr()
		{
			IntPtr intPtr = GraphicsBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return GraphicsBuffer.GetNativeBufferPtr_Injected(intPtr);
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x00021C1C File Offset: 0x0001FE1C
		private unsafe void* BeginBufferWrite(int offset = 0, int size = 0)
		{
			IntPtr intPtr = GraphicsBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return GraphicsBuffer.BeginBufferWrite_Injected(intPtr, offset, size);
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x00021C40 File Offset: 0x0001FE40
		public unsafe NativeArray<T> LockBufferForWrite<T>(int bufferStartIndex, int count) where T : struct
		{
			bool flag = !this.IsValid();
			if (flag)
			{
				throw new InvalidOperationException("LockBufferForWrite requires a valid GraphicsBuffer");
			}
			bool flag2 = (this.usageFlags & GraphicsBuffer.UsageFlags.LockBufferForWrite) == GraphicsBuffer.UsageFlags.None;
			if (flag2)
			{
				throw new InvalidOperationException("GraphicsBuffer must be created with usage mode UsageFlage.LockBufferForWrite to use LockBufferForWrite");
			}
			int num = UnsafeUtility.SizeOf<T>();
			bool flag3 = bufferStartIndex < 0 || count < 0 || (bufferStartIndex + count) * num > this.count * this.stride;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad indices/count arguments (bufferStartIndex:{0} count:{1} elementSize:{2}, this.count:{3}, this.stride{4})", new object[] { bufferStartIndex, count, num, this.count, this.stride }));
			}
			void* ptr = this.BeginBufferWrite(bufferStartIndex * num, count * num);
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>(ptr, count, Allocator.Invalid);
		}

		// Token: 0x0600136C RID: 4972 RVA: 0x00021D1C File Offset: 0x0001FF1C
		private void EndBufferWrite(int bytesWritten = 0)
		{
			IntPtr intPtr = GraphicsBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			GraphicsBuffer.EndBufferWrite_Injected(intPtr, bytesWritten);
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x00021D40 File Offset: 0x0001FF40
		public void UnlockBufferAfterWrite<T>(int countWritten) where T : struct
		{
			bool flag = countWritten < 0;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad indices/count arguments (countWritten:{0})", countWritten));
			}
			int num = UnsafeUtility.SizeOf<T>();
			this.EndBufferWrite(countWritten * num);
		}

		// Token: 0x1700033A RID: 826
		// (set) Token: 0x0600136E RID: 4974 RVA: 0x00021D7C File Offset: 0x0001FF7C
		public string name
		{
			set
			{
				this.SetName(value);
			}
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x00021D88 File Offset: 0x0001FF88
		[FreeFunction(Name = "GraphicsBuffer_Bindings::SetName", HasExplicitThis = true)]
		private unsafe void SetName(string name)
		{
			try
			{
				IntPtr intPtr = GraphicsBuffer.BindingsMarshaller.ConvertToNative(this);
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
				GraphicsBuffer.SetName_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x00021DEC File Offset: 0x0001FFEC
		public void SetCounterValue(uint counterValue)
		{
			IntPtr intPtr = GraphicsBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			GraphicsBuffer.SetCounterValue_Injected(intPtr, counterValue);
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x00021E10 File Offset: 0x00020010
		[FreeFunction(Name = "GraphicsBuffer_Bindings::CopyCount")]
		private static void CopyCountCC(ComputeBuffer src, ComputeBuffer dst, int dstOffsetBytes)
		{
			GraphicsBuffer.CopyCountCC_Injected((src == null) ? ((IntPtr)0) : ComputeBuffer.BindingsMarshaller.ConvertToNative(src), (dst == null) ? ((IntPtr)0) : ComputeBuffer.BindingsMarshaller.ConvertToNative(dst), dstOffsetBytes);
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x00021E44 File Offset: 0x00020044
		[FreeFunction(Name = "GraphicsBuffer_Bindings::CopyCount")]
		private static void CopyCountGC(GraphicsBuffer src, ComputeBuffer dst, int dstOffsetBytes)
		{
			GraphicsBuffer.CopyCountGC_Injected((src == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(src), (dst == null) ? ((IntPtr)0) : ComputeBuffer.BindingsMarshaller.ConvertToNative(dst), dstOffsetBytes);
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x00021E78 File Offset: 0x00020078
		[FreeFunction(Name = "GraphicsBuffer_Bindings::CopyCount")]
		private static void CopyCountCG(ComputeBuffer src, GraphicsBuffer dst, int dstOffsetBytes)
		{
			GraphicsBuffer.CopyCountCG_Injected((src == null) ? ((IntPtr)0) : ComputeBuffer.BindingsMarshaller.ConvertToNative(src), (dst == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(dst), dstOffsetBytes);
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x00021EAC File Offset: 0x000200AC
		[FreeFunction(Name = "GraphicsBuffer_Bindings::CopyCount")]
		private static void CopyCountGG(GraphicsBuffer src, GraphicsBuffer dst, int dstOffsetBytes)
		{
			GraphicsBuffer.CopyCountGG_Injected((src == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(src), (dst == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(dst), dstOffsetBytes);
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x00021EDD File Offset: 0x000200DD
		public static void CopyCount(ComputeBuffer src, ComputeBuffer dst, int dstOffsetBytes)
		{
			GraphicsBuffer.CopyCountCC(src, dst, dstOffsetBytes);
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x00021EE9 File Offset: 0x000200E9
		public static void CopyCount(GraphicsBuffer src, ComputeBuffer dst, int dstOffsetBytes)
		{
			GraphicsBuffer.CopyCountGC(src, dst, dstOffsetBytes);
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x00021EF5 File Offset: 0x000200F5
		public static void CopyCount(ComputeBuffer src, GraphicsBuffer dst, int dstOffsetBytes)
		{
			GraphicsBuffer.CopyCountCG(src, dst, dstOffsetBytes);
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x00021F01 File Offset: 0x00020101
		public static void CopyCount(GraphicsBuffer src, GraphicsBuffer dst, int dstOffsetBytes)
		{
			GraphicsBuffer.CopyCountGG(src, dst, dstOffsetBytes);
		}

		// Token: 0x06001379 RID: 4985
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DestroyBuffer_Injected(IntPtr buf);

		// Token: 0x0600137A RID: 4986
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsValidBuffer_Injected(IntPtr buf);

		// Token: 0x0600137B RID: 4987
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_count_Injected(IntPtr _unity_self);

		// Token: 0x0600137C RID: 4988
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_stride_Injected(IntPtr _unity_self);

		// Token: 0x0600137D RID: 4989
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern GraphicsBuffer.Target get_target_Injected(IntPtr _unity_self);

		// Token: 0x0600137E RID: 4990
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern GraphicsBuffer.UsageFlags GetUsageFlags_Injected(IntPtr _unity_self);

		// Token: 0x0600137F RID: 4991
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_bufferHandle_Injected(IntPtr _unity_self, out GraphicsBufferHandle ret);

		// Token: 0x06001380 RID: 4992
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetNativeData_Injected(IntPtr _unity_self, IntPtr data, int nativeBufferStartIndex, int graphicsBufferStartIndex, int count, int elemSize);

		// Token: 0x06001381 RID: 4993
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetData_Injected(IntPtr _unity_self, Array data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count, int elemSize);

		// Token: 0x06001382 RID: 4994
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalGetData_Injected(IntPtr _unity_self, Array data, int managedBufferStartIndex, int computeBufferStartIndex, int count, int elemSize);

		// Token: 0x06001383 RID: 4995
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetNativeBufferPtr_Injected(IntPtr _unity_self);

		// Token: 0x06001384 RID: 4996
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void* BeginBufferWrite_Injected(IntPtr _unity_self, int offset, int size);

		// Token: 0x06001385 RID: 4997
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EndBufferWrite_Injected(IntPtr _unity_self, int bytesWritten);

		// Token: 0x06001386 RID: 4998
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetName_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name);

		// Token: 0x06001387 RID: 4999
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetCounterValue_Injected(IntPtr _unity_self, uint counterValue);

		// Token: 0x06001388 RID: 5000
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyCountCC_Injected(IntPtr src, IntPtr dst, int dstOffsetBytes);

		// Token: 0x06001389 RID: 5001
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyCountGC_Injected(IntPtr src, IntPtr dst, int dstOffsetBytes);

		// Token: 0x0600138A RID: 5002
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyCountCG_Injected(IntPtr src, IntPtr dst, int dstOffsetBytes);

		// Token: 0x0600138B RID: 5003
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyCountGG_Injected(IntPtr src, IntPtr dst, int dstOffsetBytes);

		// Token: 0x04000504 RID: 1284
		internal IntPtr m_Ptr;

		// Token: 0x0200018E RID: 398
		[Flags]
		public enum Target
		{
			// Token: 0x04000506 RID: 1286
			Vertex = 1,
			// Token: 0x04000507 RID: 1287
			Index = 2,
			// Token: 0x04000508 RID: 1288
			CopySource = 4,
			// Token: 0x04000509 RID: 1289
			CopyDestination = 8,
			// Token: 0x0400050A RID: 1290
			Structured = 16,
			// Token: 0x0400050B RID: 1291
			Raw = 32,
			// Token: 0x0400050C RID: 1292
			Append = 64,
			// Token: 0x0400050D RID: 1293
			Counter = 128,
			// Token: 0x0400050E RID: 1294
			IndirectArguments = 256,
			// Token: 0x0400050F RID: 1295
			Constant = 512
		}

		// Token: 0x0200018F RID: 399
		[Flags]
		public enum UsageFlags
		{
			// Token: 0x04000511 RID: 1297
			None = 0,
			// Token: 0x04000512 RID: 1298
			LockBufferForWrite = 1
		}

		// Token: 0x02000190 RID: 400
		public struct IndirectDrawArgs
		{
			// Token: 0x1700033B RID: 827
			// (get) Token: 0x0600138C RID: 5004 RVA: 0x00021F0D File Offset: 0x0002010D
			// (set) Token: 0x0600138D RID: 5005 RVA: 0x00021F15 File Offset: 0x00020115
			public uint vertexCountPerInstance { readonly get; set; }

			// Token: 0x1700033C RID: 828
			// (get) Token: 0x0600138E RID: 5006 RVA: 0x00021F1E File Offset: 0x0002011E
			// (set) Token: 0x0600138F RID: 5007 RVA: 0x00021F26 File Offset: 0x00020126
			public uint instanceCount { readonly get; set; }

			// Token: 0x1700033D RID: 829
			// (get) Token: 0x06001390 RID: 5008 RVA: 0x00021F2F File Offset: 0x0002012F
			// (set) Token: 0x06001391 RID: 5009 RVA: 0x00021F37 File Offset: 0x00020137
			public uint startVertex { readonly get; set; }

			// Token: 0x1700033E RID: 830
			// (get) Token: 0x06001392 RID: 5010 RVA: 0x00021F40 File Offset: 0x00020140
			// (set) Token: 0x06001393 RID: 5011 RVA: 0x00021F48 File Offset: 0x00020148
			public uint startInstance { readonly get; set; }

			// Token: 0x04000513 RID: 1299
			public const int size = 16;
		}

		// Token: 0x02000191 RID: 401
		public struct IndirectDrawIndexedArgs
		{
			// Token: 0x1700033F RID: 831
			// (get) Token: 0x06001394 RID: 5012 RVA: 0x00021F51 File Offset: 0x00020151
			// (set) Token: 0x06001395 RID: 5013 RVA: 0x00021F59 File Offset: 0x00020159
			public uint indexCountPerInstance { readonly get; set; }

			// Token: 0x17000340 RID: 832
			// (get) Token: 0x06001396 RID: 5014 RVA: 0x00021F62 File Offset: 0x00020162
			// (set) Token: 0x06001397 RID: 5015 RVA: 0x00021F6A File Offset: 0x0002016A
			public uint instanceCount { readonly get; set; }

			// Token: 0x17000341 RID: 833
			// (get) Token: 0x06001398 RID: 5016 RVA: 0x00021F73 File Offset: 0x00020173
			// (set) Token: 0x06001399 RID: 5017 RVA: 0x00021F7B File Offset: 0x0002017B
			public uint startIndex { readonly get; set; }

			// Token: 0x17000342 RID: 834
			// (get) Token: 0x0600139A RID: 5018 RVA: 0x00021F84 File Offset: 0x00020184
			// (set) Token: 0x0600139B RID: 5019 RVA: 0x00021F8C File Offset: 0x0002018C
			public uint baseVertexIndex { readonly get; set; }

			// Token: 0x17000343 RID: 835
			// (get) Token: 0x0600139C RID: 5020 RVA: 0x00021F95 File Offset: 0x00020195
			// (set) Token: 0x0600139D RID: 5021 RVA: 0x00021F9D File Offset: 0x0002019D
			public uint startInstance { readonly get; set; }

			// Token: 0x04000518 RID: 1304
			public const int size = 20;
		}

		// Token: 0x02000192 RID: 402
		internal static class BindingsMarshaller
		{
			// Token: 0x0600139E RID: 5022 RVA: 0x00021FA6 File Offset: 0x000201A6
			public static GraphicsBuffer ConvertToManaged(IntPtr ptr)
			{
				return new GraphicsBuffer(ptr);
			}

			// Token: 0x0600139F RID: 5023 RVA: 0x00021FAE File Offset: 0x000201AE
			public static IntPtr ConvertToNative(GraphicsBuffer graphicsBuffer)
			{
				return graphicsBuffer.m_Ptr;
			}
		}
	}
}
