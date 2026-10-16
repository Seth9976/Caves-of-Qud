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
	// Token: 0x020002CF RID: 719
	[UsedByNativeCode]
	[NativeHeader("Runtime/Shaders/GraphicsBuffer.h")]
	[NativeClass("GraphicsBuffer")]
	[NativeHeader("Runtime/Export/Graphics/GraphicsBuffer.bindings.h")]
	public sealed class ComputeBuffer : IDisposable
	{
		// Token: 0x06002289 RID: 8841 RVA: 0x0004398C File Offset: 0x00041B8C
		~ComputeBuffer()
		{
			this.Dispose(false);
		}

		// Token: 0x0600228A RID: 8842 RVA: 0x000439C0 File Offset: 0x00041BC0
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600228B RID: 8843 RVA: 0x000439D4 File Offset: 0x00041BD4
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				ComputeBuffer.DestroyBuffer(this);
			}
			else
			{
				bool flag = this.m_Ptr != IntPtr.Zero;
				if (flag)
				{
					Debug.LogWarning("GarbageCollector disposing of ComputeBuffer. Please use ComputeBuffer.Release() or .Dispose() to manually release the buffer.");
				}
			}
			this.m_Ptr = IntPtr.Zero;
		}

		// Token: 0x0600228C RID: 8844
		[FreeFunction("GraphicsBuffer_Bindings::InitComputeBuffer")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr InitBuffer(int count, int stride, ComputeBufferType type, ComputeBufferMode usage);

		// Token: 0x0600228D RID: 8845 RVA: 0x00043A20 File Offset: 0x00041C20
		[FreeFunction("GraphicsBuffer_Bindings::DestroyComputeBuffer")]
		private static void DestroyBuffer(ComputeBuffer buf)
		{
			ComputeBuffer.DestroyBuffer_Injected((buf == null) ? ((IntPtr)0) : ComputeBuffer.BindingsMarshaller.ConvertToNative(buf));
		}

		// Token: 0x0600228E RID: 8846 RVA: 0x00043A41 File Offset: 0x00041C41
		public ComputeBuffer(int count, int stride)
			: this(count, stride, ComputeBufferType.Default, ComputeBufferMode.Immutable, 3)
		{
		}

		// Token: 0x0600228F RID: 8847 RVA: 0x00043A50 File Offset: 0x00041C50
		public ComputeBuffer(int count, int stride, ComputeBufferType type)
			: this(count, stride, type, ComputeBufferMode.Immutable, 3)
		{
		}

		// Token: 0x06002290 RID: 8848 RVA: 0x00043A5F File Offset: 0x00041C5F
		public ComputeBuffer(int count, int stride, ComputeBufferType type, ComputeBufferMode usage)
			: this(count, stride, type, usage, 3)
		{
		}

		// Token: 0x06002291 RID: 8849 RVA: 0x00043A70 File Offset: 0x00041C70
		private ComputeBuffer(int count, int stride, ComputeBufferType type, ComputeBufferMode usage, int stackDepth)
		{
			bool flag = count <= 0;
			if (flag)
			{
				throw new ArgumentException("Attempting to create a zero length compute buffer", "count");
			}
			bool flag2 = stride <= 0;
			if (flag2)
			{
				throw new ArgumentException("Attempting to create a compute buffer with a negative or null stride", "stride");
			}
			long num = (long)count * (long)stride;
			long maxGraphicsBufferSize = SystemInfo.maxGraphicsBufferSize;
			bool flag3 = num > maxGraphicsBufferSize;
			if (flag3)
			{
				throw new ArgumentException(string.Format("The total size of the compute buffer ({0} bytes) exceeds the maximum buffer size. Maximum supported buffer size: {1} bytes.", num, maxGraphicsBufferSize));
			}
			this.m_Ptr = ComputeBuffer.InitBuffer(count, stride, type, usage);
		}

		// Token: 0x06002292 RID: 8850 RVA: 0x00043B00 File Offset: 0x00041D00
		public void Release()
		{
			this.Dispose();
		}

		// Token: 0x06002293 RID: 8851 RVA: 0x00043B0C File Offset: 0x00041D0C
		[FreeFunction("GraphicsBuffer_Bindings::IsValidBuffer")]
		private static bool IsValidBuffer(ComputeBuffer buf)
		{
			return ComputeBuffer.IsValidBuffer_Injected((buf == null) ? ((IntPtr)0) : ComputeBuffer.BindingsMarshaller.ConvertToNative(buf));
		}

		// Token: 0x06002294 RID: 8852 RVA: 0x00043B30 File Offset: 0x00041D30
		public bool IsValid()
		{
			return this.m_Ptr != IntPtr.Zero && ComputeBuffer.IsValidBuffer(this);
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06002295 RID: 8853 RVA: 0x00043B60 File Offset: 0x00041D60
		public int count
		{
			get
			{
				IntPtr intPtr = ComputeBuffer.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ComputeBuffer.get_count_Injected(intPtr);
			}
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06002296 RID: 8854 RVA: 0x00043B84 File Offset: 0x00041D84
		public int stride
		{
			get
			{
				IntPtr intPtr = ComputeBuffer.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ComputeBuffer.get_stride_Injected(intPtr);
			}
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06002297 RID: 8855 RVA: 0x00043BA8 File Offset: 0x00041DA8
		private ComputeBufferMode usage
		{
			get
			{
				IntPtr intPtr = ComputeBuffer.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ComputeBuffer.get_usage_Injected(intPtr);
			}
		}

		// Token: 0x06002298 RID: 8856 RVA: 0x00043BCC File Offset: 0x00041DCC
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
				throw new ArgumentException(string.Format("Array passed to ComputeBuffer.SetData(array) must be blittable.\n{0}", UnsafeUtility.GetReasonForArrayNonBlittable(data)));
			}
			this.InternalSetData(data, 0, 0, data.Length, UnsafeUtility.SizeOf(data.GetType().GetElementType()));
		}

		// Token: 0x06002299 RID: 8857 RVA: 0x00043C34 File Offset: 0x00041E34
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
				throw new ArgumentException(string.Format("List<{0}> passed to ComputeBuffer.SetData(List<>) must be blittable.\n{1}", typeof(T), UnsafeUtility.GetReasonForGenericListNonBlittable<T>()));
			}
			this.InternalSetData(NoAllocHelpers.ExtractArrayFromList<T>(data), 0, 0, NoAllocHelpers.SafeLength<T>(data), Marshal.SizeOf(typeof(T)));
		}

		// Token: 0x0600229A RID: 8858 RVA: 0x00043CA5 File Offset: 0x00041EA5
		[SecuritySafeCritical]
		public void SetData<T>(NativeArray<T> data) where T : struct
		{
			this.InternalSetNativeData((IntPtr)data.GetUnsafeReadOnlyPtr<T>(), 0, 0, data.Length, UnsafeUtility.SizeOf<T>());
		}

		// Token: 0x0600229B RID: 8859 RVA: 0x00043CC8 File Offset: 0x00041EC8
		[SecuritySafeCritical]
		public void SetData(Array data, int managedBufferStartIndex, int computeBufferStartIndex, int count)
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			bool flag2 = !UnsafeUtility.IsArrayBlittable(data);
			if (flag2)
			{
				throw new ArgumentException(string.Format("Array passed to ComputeBuffer.SetData(array) must be blittable.\n{0}", UnsafeUtility.GetReasonForArrayNonBlittable(data)));
			}
			bool flag3 = managedBufferStartIndex < 0 || computeBufferStartIndex < 0 || count < 0 || managedBufferStartIndex + count > data.Length;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad indices/count arguments (managedBufferStartIndex:{0} computeBufferStartIndex:{1} count:{2})", managedBufferStartIndex, computeBufferStartIndex, count));
			}
			this.InternalSetData(data, managedBufferStartIndex, computeBufferStartIndex, count, Marshal.SizeOf(data.GetType().GetElementType()));
		}

		// Token: 0x0600229C RID: 8860 RVA: 0x00043D6C File Offset: 0x00041F6C
		[SecuritySafeCritical]
		public void SetData<T>(List<T> data, int managedBufferStartIndex, int computeBufferStartIndex, int count) where T : struct
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			bool flag2 = !UnsafeUtility.IsGenericListBlittable<T>();
			if (flag2)
			{
				throw new ArgumentException(string.Format("List<{0}> passed to ComputeBuffer.SetData(List<>) must be blittable.\n{1}", typeof(T), UnsafeUtility.GetReasonForGenericListNonBlittable<T>()));
			}
			bool flag3 = managedBufferStartIndex < 0 || computeBufferStartIndex < 0 || count < 0 || managedBufferStartIndex + count > data.Count;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad indices/count arguments (managedBufferStartIndex:{0} computeBufferStartIndex:{1} count:{2})", managedBufferStartIndex, computeBufferStartIndex, count));
			}
			this.InternalSetData(NoAllocHelpers.ExtractArrayFromList<T>(data), managedBufferStartIndex, computeBufferStartIndex, count, Marshal.SizeOf(typeof(T)));
		}

		// Token: 0x0600229D RID: 8861 RVA: 0x00043E1C File Offset: 0x0004201C
		[SecuritySafeCritical]
		public void SetData<T>(NativeArray<T> data, int nativeBufferStartIndex, int computeBufferStartIndex, int count) where T : struct
		{
			bool flag = nativeBufferStartIndex < 0 || computeBufferStartIndex < 0 || count < 0 || nativeBufferStartIndex + count > data.Length;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad indices/count arguments (nativeBufferStartIndex:{0} computeBufferStartIndex:{1} count:{2})", nativeBufferStartIndex, computeBufferStartIndex, count));
			}
			this.InternalSetNativeData((IntPtr)data.GetUnsafeReadOnlyPtr<T>(), nativeBufferStartIndex, computeBufferStartIndex, count, UnsafeUtility.SizeOf<T>());
		}

		// Token: 0x0600229E RID: 8862 RVA: 0x00043E8C File Offset: 0x0004208C
		[FreeFunction(Name = "GraphicsBuffer_Bindings::InternalSetNativeData", HasExplicitThis = true, ThrowsException = true)]
		private void InternalSetNativeData(IntPtr data, int nativeBufferStartIndex, int computeBufferStartIndex, int count, int elemSize)
		{
			IntPtr intPtr = ComputeBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ComputeBuffer.InternalSetNativeData_Injected(intPtr, data, nativeBufferStartIndex, computeBufferStartIndex, count, elemSize);
		}

		// Token: 0x0600229F RID: 8863 RVA: 0x00043EB8 File Offset: 0x000420B8
		[FreeFunction(Name = "GraphicsBuffer_Bindings::InternalSetData", HasExplicitThis = true, ThrowsException = true)]
		private void InternalSetData(Array data, int managedBufferStartIndex, int computeBufferStartIndex, int count, int elemSize)
		{
			IntPtr intPtr = ComputeBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ComputeBuffer.InternalSetData_Injected(intPtr, data, managedBufferStartIndex, computeBufferStartIndex, count, elemSize);
		}

		// Token: 0x060022A0 RID: 8864 RVA: 0x00043EE4 File Offset: 0x000420E4
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
				throw new ArgumentException(string.Format("Array passed to ComputeBuffer.GetData(array) must be blittable.\n{0}", UnsafeUtility.GetReasonForArrayNonBlittable(data)));
			}
			this.InternalGetData(data, 0, 0, data.Length, Marshal.SizeOf(data.GetType().GetElementType()));
		}

		// Token: 0x060022A1 RID: 8865 RVA: 0x00043F4C File Offset: 0x0004214C
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
				throw new ArgumentException(string.Format("Array passed to ComputeBuffer.GetData(array) must be blittable.\n{0}", UnsafeUtility.GetReasonForArrayNonBlittable(data)));
			}
			bool flag3 = managedBufferStartIndex < 0 || computeBufferStartIndex < 0 || count < 0 || managedBufferStartIndex + count > data.Length;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad indices/count argument (managedBufferStartIndex:{0} computeBufferStartIndex:{1} count:{2})", managedBufferStartIndex, computeBufferStartIndex, count));
			}
			this.InternalGetData(data, managedBufferStartIndex, computeBufferStartIndex, count, Marshal.SizeOf(data.GetType().GetElementType()));
		}

		// Token: 0x060022A2 RID: 8866 RVA: 0x00043FF0 File Offset: 0x000421F0
		[FreeFunction(Name = "GraphicsBuffer_Bindings::InternalGetData", HasExplicitThis = true, ThrowsException = true)]
		private void InternalGetData(Array data, int managedBufferStartIndex, int computeBufferStartIndex, int count, int elemSize)
		{
			IntPtr intPtr = ComputeBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ComputeBuffer.InternalGetData_Injected(intPtr, data, managedBufferStartIndex, computeBufferStartIndex, count, elemSize);
		}

		// Token: 0x060022A3 RID: 8867 RVA: 0x0004401C File Offset: 0x0004221C
		private unsafe void* BeginBufferWrite(int offset = 0, int size = 0)
		{
			IntPtr intPtr = ComputeBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return ComputeBuffer.BeginBufferWrite_Injected(intPtr, offset, size);
		}

		// Token: 0x060022A4 RID: 8868 RVA: 0x00044040 File Offset: 0x00042240
		public unsafe NativeArray<T> BeginWrite<T>(int computeBufferStartIndex, int count) where T : struct
		{
			bool flag = !this.IsValid();
			if (flag)
			{
				throw new InvalidOperationException("BeginWrite requires a valid ComputeBuffer");
			}
			bool flag2 = this.usage != ComputeBufferMode.SubUpdates;
			if (flag2)
			{
				throw new ArgumentException("ComputeBuffer must be created with usage mode ComputeBufferMode.SubUpdates to be able to be mapped with BeginWrite");
			}
			int num = UnsafeUtility.SizeOf<T>();
			bool flag3 = computeBufferStartIndex < 0 || count < 0 || (computeBufferStartIndex + count) * num > this.count * this.stride;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad indices/count arguments (computeBufferStartIndex:{0} count:{1} elementSize:{2}, this.count:{3}, this.stride{4})", new object[] { computeBufferStartIndex, count, num, this.count, this.stride }));
			}
			void* ptr = this.BeginBufferWrite(computeBufferStartIndex * num, count * num);
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>(ptr, count, Allocator.Invalid);
		}

		// Token: 0x060022A5 RID: 8869 RVA: 0x0004411C File Offset: 0x0004231C
		private void EndBufferWrite(int bytesWritten = 0)
		{
			IntPtr intPtr = ComputeBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ComputeBuffer.EndBufferWrite_Injected(intPtr, bytesWritten);
		}

		// Token: 0x060022A6 RID: 8870 RVA: 0x00044140 File Offset: 0x00042340
		public void EndWrite<T>(int countWritten) where T : struct
		{
			bool flag = countWritten < 0;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad indices/count arguments (countWritten:{0})", countWritten));
			}
			int num = UnsafeUtility.SizeOf<T>();
			this.EndBufferWrite(countWritten * num);
		}

		// Token: 0x1700054E RID: 1358
		// (set) Token: 0x060022A7 RID: 8871 RVA: 0x0004417C File Offset: 0x0004237C
		public string name
		{
			set
			{
				this.SetName(value);
			}
		}

		// Token: 0x060022A8 RID: 8872 RVA: 0x00044188 File Offset: 0x00042388
		[FreeFunction(Name = "GraphicsBuffer_Bindings::SetName", HasExplicitThis = true)]
		private unsafe void SetName(string name)
		{
			try
			{
				IntPtr intPtr = ComputeBuffer.BindingsMarshaller.ConvertToNative(this);
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
				ComputeBuffer.SetName_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x060022A9 RID: 8873 RVA: 0x000441EC File Offset: 0x000423EC
		public void SetCounterValue(uint counterValue)
		{
			IntPtr intPtr = ComputeBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ComputeBuffer.SetCounterValue_Injected(intPtr, counterValue);
		}

		// Token: 0x060022AA RID: 8874 RVA: 0x00044210 File Offset: 0x00042410
		public static void CopyCount(ComputeBuffer src, ComputeBuffer dst, int dstOffsetBytes)
		{
			ComputeBuffer.CopyCount_Injected((src == null) ? ((IntPtr)0) : ComputeBuffer.BindingsMarshaller.ConvertToNative(src), (dst == null) ? ((IntPtr)0) : ComputeBuffer.BindingsMarshaller.ConvertToNative(dst), dstOffsetBytes);
		}

		// Token: 0x060022AB RID: 8875 RVA: 0x00044244 File Offset: 0x00042444
		public IntPtr GetNativeBufferPtr()
		{
			IntPtr intPtr = ComputeBuffer.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return ComputeBuffer.GetNativeBufferPtr_Injected(intPtr);
		}

		// Token: 0x060022AC RID: 8876
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DestroyBuffer_Injected(IntPtr buf);

		// Token: 0x060022AD RID: 8877
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsValidBuffer_Injected(IntPtr buf);

		// Token: 0x060022AE RID: 8878
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_count_Injected(IntPtr _unity_self);

		// Token: 0x060022AF RID: 8879
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_stride_Injected(IntPtr _unity_self);

		// Token: 0x060022B0 RID: 8880
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ComputeBufferMode get_usage_Injected(IntPtr _unity_self);

		// Token: 0x060022B1 RID: 8881
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetNativeData_Injected(IntPtr _unity_self, IntPtr data, int nativeBufferStartIndex, int computeBufferStartIndex, int count, int elemSize);

		// Token: 0x060022B2 RID: 8882
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetData_Injected(IntPtr _unity_self, Array data, int managedBufferStartIndex, int computeBufferStartIndex, int count, int elemSize);

		// Token: 0x060022B3 RID: 8883
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalGetData_Injected(IntPtr _unity_self, Array data, int managedBufferStartIndex, int computeBufferStartIndex, int count, int elemSize);

		// Token: 0x060022B4 RID: 8884
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void* BeginBufferWrite_Injected(IntPtr _unity_self, int offset, int size);

		// Token: 0x060022B5 RID: 8885
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EndBufferWrite_Injected(IntPtr _unity_self, int bytesWritten);

		// Token: 0x060022B6 RID: 8886
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetName_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name);

		// Token: 0x060022B7 RID: 8887
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetCounterValue_Injected(IntPtr _unity_self, uint counterValue);

		// Token: 0x060022B8 RID: 8888
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyCount_Injected(IntPtr src, IntPtr dst, int dstOffsetBytes);

		// Token: 0x060022B9 RID: 8889
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetNativeBufferPtr_Injected(IntPtr _unity_self);

		// Token: 0x04000A16 RID: 2582
		internal IntPtr m_Ptr;

		// Token: 0x020002D0 RID: 720
		internal static class BindingsMarshaller
		{
			// Token: 0x060022BA RID: 8890 RVA: 0x00044266 File Offset: 0x00042466
			public static IntPtr ConvertToNative(ComputeBuffer computeBuffer)
			{
				return computeBuffer.m_Ptr;
			}
		}
	}
}
