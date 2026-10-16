using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering
{
	// Token: 0x0200050F RID: 1295
	[NativeHeader("Runtime/Camera/BatchRendererGroup.h")]
	public struct ThreadedBatchContext
	{
		// Token: 0x060031EF RID: 12783 RVA: 0x00060B48 File Offset: 0x0005ED48
		[FreeFunction("BatchRendererGroup::AddDrawCommandBatch_Threaded", IsThreadSafe = true)]
		private static BatchID AddDrawCommandBatch(IntPtr brg, IntPtr values, int count, GraphicsBufferHandle buffer, uint bufferOffset, uint windowSize)
		{
			BatchID batchID;
			ThreadedBatchContext.AddDrawCommandBatch_Injected(brg, values, count, ref buffer, bufferOffset, windowSize, out batchID);
			return batchID;
		}

		// Token: 0x060031F0 RID: 12784 RVA: 0x00060B68 File Offset: 0x0005ED68
		[FreeFunction("BatchRendererGroup::SetDrawCommandBatchBuffer_Threaded", IsThreadSafe = true)]
		private static void SetDrawCommandBatchBuffer(IntPtr brg, BatchID batchID, GraphicsBufferHandle buffer)
		{
			ThreadedBatchContext.SetDrawCommandBatchBuffer_Injected(brg, ref batchID, ref buffer);
		}

		// Token: 0x060031F1 RID: 12785 RVA: 0x00060B80 File Offset: 0x0005ED80
		[FreeFunction("BatchRendererGroup::RemoveDrawCommandBatch_Threaded", IsThreadSafe = true)]
		private static void RemoveDrawCommandBatch(IntPtr brg, BatchID batchID)
		{
			ThreadedBatchContext.RemoveDrawCommandBatch_Injected(brg, ref batchID);
		}

		// Token: 0x060031F2 RID: 12786 RVA: 0x00060B98 File Offset: 0x0005ED98
		public BatchID AddBatch(NativeArray<MetadataValue> batchMetadata, GraphicsBufferHandle buffer)
		{
			return ThreadedBatchContext.AddDrawCommandBatch(this.batchRendererGroup, (IntPtr)batchMetadata.GetUnsafeReadOnlyPtr<MetadataValue>(), batchMetadata.Length, buffer, 0U, 0U);
		}

		// Token: 0x060031F3 RID: 12787 RVA: 0x00060BCC File Offset: 0x0005EDCC
		public BatchID AddBatch(NativeArray<MetadataValue> batchMetadata, GraphicsBufferHandle buffer, uint bufferOffset, uint windowSize)
		{
			return ThreadedBatchContext.AddDrawCommandBatch(this.batchRendererGroup, (IntPtr)batchMetadata.GetUnsafeReadOnlyPtr<MetadataValue>(), batchMetadata.Length, buffer, bufferOffset, windowSize);
		}

		// Token: 0x060031F4 RID: 12788 RVA: 0x00060BFF File Offset: 0x0005EDFF
		public void SetBatchBuffer(BatchID batchID, GraphicsBufferHandle buffer)
		{
			ThreadedBatchContext.SetDrawCommandBatchBuffer(this.batchRendererGroup, batchID, buffer);
		}

		// Token: 0x060031F5 RID: 12789 RVA: 0x00060C10 File Offset: 0x0005EE10
		public void RemoveBatch(BatchID batchID)
		{
			ThreadedBatchContext.RemoveDrawCommandBatch(this.batchRendererGroup, batchID);
		}

		// Token: 0x060031F6 RID: 12790
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddDrawCommandBatch_Injected(IntPtr brg, IntPtr values, int count, [In] ref GraphicsBufferHandle buffer, uint bufferOffset, uint windowSize, out BatchID ret);

		// Token: 0x060031F7 RID: 12791
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetDrawCommandBatchBuffer_Injected(IntPtr brg, [In] ref BatchID batchID, [In] ref GraphicsBufferHandle buffer);

		// Token: 0x060031F8 RID: 12792
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RemoveDrawCommandBatch_Injected(IntPtr brg, [In] ref BatchID batchID);

		// Token: 0x0400101F RID: 4127
		public IntPtr batchRendererGroup;
	}
}
