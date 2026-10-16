using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000511 RID: 1297
	[NativeHeader("Runtime/Camera/BatchRendererGroup.h")]
	[NativeHeader("Runtime/Math/Matrix4x4.h")]
	[RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public class BatchRendererGroup : IDisposable
	{
		// Token: 0x060031F9 RID: 12793 RVA: 0x00060C20 File Offset: 0x0005EE20
		public unsafe BatchRendererGroup(BatchRendererGroup.OnPerformCulling cullingCallback, IntPtr userContext)
		{
			this.m_PerformCulling = cullingCallback;
			this.m_GroupHandle = BatchRendererGroup.Create(this, (void*)userContext);
		}

		// Token: 0x060031FA RID: 12794 RVA: 0x00060C50 File Offset: 0x0005EE50
		public unsafe BatchRendererGroup(BatchRendererGroupCreateInfo info)
		{
			this.m_PerformCulling = info.cullingCallback;
			this.m_GroupHandle = BatchRendererGroup.Create(this, (void*)info.userContext);
			this.m_FinishedCulling = info.finishedCullingCallback;
		}

		// Token: 0x060031FB RID: 12795 RVA: 0x00060C9F File Offset: 0x0005EE9F
		public void Dispose()
		{
			BatchRendererGroup.Destroy(this.m_GroupHandle);
			this.m_GroupHandle = IntPtr.Zero;
		}

		// Token: 0x060031FC RID: 12796 RVA: 0x00060CBC File Offset: 0x0005EEBC
		public ThreadedBatchContext GetThreadedBatchContext()
		{
			return new ThreadedBatchContext
			{
				batchRendererGroup = this.m_GroupHandle
			};
		}

		// Token: 0x060031FD RID: 12797 RVA: 0x00060CE4 File Offset: 0x0005EEE4
		private BatchID AddDrawCommandBatch(IntPtr values, int count, GraphicsBufferHandle buffer, uint bufferOffset, uint windowSize)
		{
			IntPtr intPtr = BatchRendererGroup.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			BatchID batchID;
			BatchRendererGroup.AddDrawCommandBatch_Injected(intPtr, values, count, ref buffer, bufferOffset, windowSize, out batchID);
			return batchID;
		}

		// Token: 0x060031FE RID: 12798 RVA: 0x00060D14 File Offset: 0x0005EF14
		public BatchID AddBatch(NativeArray<MetadataValue> batchMetadata, GraphicsBufferHandle buffer)
		{
			return this.AddDrawCommandBatch((IntPtr)batchMetadata.GetUnsafeReadOnlyPtr<MetadataValue>(), batchMetadata.Length, buffer, 0U, 0U);
		}

		// Token: 0x060031FF RID: 12799 RVA: 0x00060D44 File Offset: 0x0005EF44
		public BatchID AddBatch(NativeArray<MetadataValue> batchMetadata, GraphicsBufferHandle buffer, uint bufferOffset, uint windowSize)
		{
			return this.AddDrawCommandBatch((IntPtr)batchMetadata.GetUnsafeReadOnlyPtr<MetadataValue>(), batchMetadata.Length, buffer, bufferOffset, windowSize);
		}

		// Token: 0x06003200 RID: 12800 RVA: 0x00060D74 File Offset: 0x0005EF74
		private void RemoveDrawCommandBatch(BatchID batchID)
		{
			IntPtr intPtr = BatchRendererGroup.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			BatchRendererGroup.RemoveDrawCommandBatch_Injected(intPtr, ref batchID);
		}

		// Token: 0x06003201 RID: 12801 RVA: 0x00060D98 File Offset: 0x0005EF98
		public void RemoveBatch(BatchID batchID)
		{
			this.RemoveDrawCommandBatch(batchID);
		}

		// Token: 0x06003202 RID: 12802 RVA: 0x00060DA4 File Offset: 0x0005EFA4
		private void SetDrawCommandBatchBuffer(BatchID batchID, GraphicsBufferHandle buffer)
		{
			IntPtr intPtr = BatchRendererGroup.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			BatchRendererGroup.SetDrawCommandBatchBuffer_Injected(intPtr, ref batchID, ref buffer);
		}

		// Token: 0x06003203 RID: 12803 RVA: 0x00060DCA File Offset: 0x0005EFCA
		public void SetBatchBuffer(BatchID batchID, GraphicsBufferHandle buffer)
		{
			this.SetDrawCommandBatchBuffer(batchID, buffer);
		}

		// Token: 0x06003204 RID: 12804 RVA: 0x00060DD8 File Offset: 0x0005EFD8
		public BatchMaterialID RegisterMaterial(Material material)
		{
			IntPtr intPtr = BatchRendererGroup.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			BatchMaterialID batchMaterialID;
			BatchRendererGroup.RegisterMaterial_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Material>(material), out batchMaterialID);
			return batchMaterialID;
		}

		// Token: 0x06003205 RID: 12805 RVA: 0x00060E04 File Offset: 0x0005F004
		internal unsafe void RegisterMaterials(ReadOnlySpan<int> materialID, Span<BatchMaterialID> batchMaterialID)
		{
			IntPtr intPtr = BatchRendererGroup.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ReadOnlySpan<int> readOnlySpan = materialID;
			fixed (int* ptr = readOnlySpan.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
				Span<BatchMaterialID> span = batchMaterialID;
				fixed (BatchMaterialID* pinnableReference = span.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper2 = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
					BatchRendererGroup.RegisterMaterials_Injected(intPtr, ref managedSpanWrapper, ref managedSpanWrapper2);
					ptr = null;
				}
			}
		}

		// Token: 0x06003206 RID: 12806 RVA: 0x00060E6C File Offset: 0x0005F06C
		public void UnregisterMaterial(BatchMaterialID material)
		{
			IntPtr intPtr = BatchRendererGroup.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			BatchRendererGroup.UnregisterMaterial_Injected(intPtr, ref material);
		}

		// Token: 0x06003207 RID: 12807 RVA: 0x00060E90 File Offset: 0x0005F090
		public Material GetRegisteredMaterial(BatchMaterialID material)
		{
			IntPtr intPtr = BatchRendererGroup.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<Material>(BatchRendererGroup.GetRegisteredMaterial_Injected(intPtr, ref material));
		}

		// Token: 0x06003208 RID: 12808 RVA: 0x00060EBC File Offset: 0x0005F0BC
		public BatchMeshID RegisterMesh(Mesh mesh)
		{
			IntPtr intPtr = BatchRendererGroup.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			BatchMeshID batchMeshID;
			BatchRendererGroup.RegisterMesh_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Mesh>(mesh), out batchMeshID);
			return batchMeshID;
		}

		// Token: 0x06003209 RID: 12809 RVA: 0x00060EE8 File Offset: 0x0005F0E8
		internal unsafe void RegisterMeshes(ReadOnlySpan<int> meshID, Span<BatchMeshID> batchMeshID)
		{
			IntPtr intPtr = BatchRendererGroup.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ReadOnlySpan<int> readOnlySpan = meshID;
			fixed (int* ptr = readOnlySpan.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
				Span<BatchMeshID> span = batchMeshID;
				fixed (BatchMeshID* pinnableReference = span.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper2 = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
					BatchRendererGroup.RegisterMeshes_Injected(intPtr, ref managedSpanWrapper, ref managedSpanWrapper2);
					ptr = null;
				}
			}
		}

		// Token: 0x0600320A RID: 12810 RVA: 0x00060F50 File Offset: 0x0005F150
		public void UnregisterMesh(BatchMeshID mesh)
		{
			IntPtr intPtr = BatchRendererGroup.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			BatchRendererGroup.UnregisterMesh_Injected(intPtr, ref mesh);
		}

		// Token: 0x0600320B RID: 12811 RVA: 0x00060F74 File Offset: 0x0005F174
		public Mesh GetRegisteredMesh(BatchMeshID mesh)
		{
			IntPtr intPtr = BatchRendererGroup.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<Mesh>(BatchRendererGroup.GetRegisteredMesh_Injected(intPtr, ref mesh));
		}

		// Token: 0x0600320C RID: 12812 RVA: 0x00060FA0 File Offset: 0x0005F1A0
		public void SetGlobalBounds(Bounds bounds)
		{
			IntPtr intPtr = BatchRendererGroup.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			BatchRendererGroup.SetGlobalBounds_Injected(intPtr, ref bounds);
		}

		// Token: 0x0600320D RID: 12813 RVA: 0x00060FC4 File Offset: 0x0005F1C4
		public void SetPickingMaterial(Material material)
		{
			IntPtr intPtr = BatchRendererGroup.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			BatchRendererGroup.SetPickingMaterial_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Material>(material));
		}

		// Token: 0x0600320E RID: 12814 RVA: 0x00060FEC File Offset: 0x0005F1EC
		public void SetErrorMaterial(Material material)
		{
			IntPtr intPtr = BatchRendererGroup.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			BatchRendererGroup.SetErrorMaterial_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Material>(material));
		}

		// Token: 0x0600320F RID: 12815 RVA: 0x00061014 File Offset: 0x0005F214
		public void SetLoadingMaterial(Material material)
		{
			IntPtr intPtr = BatchRendererGroup.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			BatchRendererGroup.SetLoadingMaterial_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Material>(material));
		}

		// Token: 0x06003210 RID: 12816 RVA: 0x0006103C File Offset: 0x0005F23C
		public unsafe void SetEnabledViewTypes(BatchCullingViewType[] viewTypes)
		{
			IntPtr intPtr = BatchRendererGroup.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<BatchCullingViewType> span = new Span<BatchCullingViewType>(viewTypes);
			fixed (BatchCullingViewType* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				BatchRendererGroup.SetEnabledViewTypes_Injected(intPtr, ref managedSpanWrapper);
			}
		}

		// Token: 0x06003211 RID: 12817
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern BatchBufferTarget GetBufferTarget();

		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x06003212 RID: 12818 RVA: 0x00061084 File Offset: 0x0005F284
		public static BatchBufferTarget BufferTarget
		{
			get
			{
				return BatchRendererGroup.GetBufferTarget();
			}
		}

		// Token: 0x06003213 RID: 12819
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetConstantBufferMaxWindowSize();

		// Token: 0x06003214 RID: 12820
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetConstantBufferOffsetAlignment();

		// Token: 0x06003215 RID: 12821
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern IntPtr Create([Unmarshalled] BatchRendererGroup group, void* userContext);

		// Token: 0x06003216 RID: 12822
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Destroy(IntPtr groupHandle);

		// Token: 0x06003217 RID: 12823 RVA: 0x0006108C File Offset: 0x0005F28C
		[RequiredByNativeCode]
		private unsafe static void InvokeOnPerformCulling(BatchRendererGroup group, ref BatchRendererCullingOutput context, ref LODParameters lodParameters, IntPtr userContext)
		{
			NativeArray<Plane> nativeArray = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Plane>((void*)context.cullingPlanes, context.cullingPlaneCount, Allocator.Invalid);
			NativeArray<CullingSplit> nativeArray2 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<CullingSplit>((void*)context.cullingSplits, context.cullingSplitCount, Allocator.Invalid);
			NativeArray<BatchCullingOutputDrawCommands> nativeArray3 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<BatchCullingOutputDrawCommands>((void*)context.drawCommands, 1, Allocator.Invalid);
			try
			{
				BatchCullingOutput batchCullingOutput = new BatchCullingOutput
				{
					drawCommands = nativeArray3,
					customCullingResult = new NativeArray<IntPtr>(1, Allocator.Temp, NativeArrayOptions.ClearMemory)
				};
				context.cullingJobsFence = group.m_PerformCulling(group, new BatchCullingContext(nativeArray, nativeArray2, lodParameters, context.localToWorldMatrix, context.viewType, context.projectionType, context.cullingFlags, context.viewID, context.cullingLayerMask, context.sceneCullingMask, context.splitExclusionMask, context.receiverPlaneOffset, context.receiverPlaneCount, context.occlusionBuffer), batchCullingOutput, userContext);
				context.customCullingResult = batchCullingOutput.customCullingResult[0];
			}
			finally
			{
				JobHandle.ScheduleBatchedJobs();
			}
		}

		// Token: 0x06003218 RID: 12824 RVA: 0x00061184 File Offset: 0x0005F384
		[RequiredByNativeCode]
		private static void InvokeOnFinishedCulling(BatchRendererGroup group, IntPtr customCullingResult)
		{
			try
			{
				bool flag = group.m_FinishedCulling != null;
				if (flag)
				{
					group.m_FinishedCulling(customCullingResult);
				}
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
			}
		}

		// Token: 0x06003219 RID: 12825 RVA: 0x000611CC File Offset: 0x0005F3CC
		[FreeFunction("BatchRendererGroup::OcclusionTestAABB", IsThreadSafe = true)]
		internal static bool OcclusionTestAABB(IntPtr occlusionBuffer, Bounds aabb)
		{
			return BatchRendererGroup.OcclusionTestAABB_Injected(occlusionBuffer, ref aabb);
		}

		// Token: 0x0600321A RID: 12826
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddDrawCommandBatch_Injected(IntPtr _unity_self, IntPtr values, int count, [In] ref GraphicsBufferHandle buffer, uint bufferOffset, uint windowSize, out BatchID ret);

		// Token: 0x0600321B RID: 12827
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RemoveDrawCommandBatch_Injected(IntPtr _unity_self, [In] ref BatchID batchID);

		// Token: 0x0600321C RID: 12828
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetDrawCommandBatchBuffer_Injected(IntPtr _unity_self, [In] ref BatchID batchID, [In] ref GraphicsBufferHandle buffer);

		// Token: 0x0600321D RID: 12829
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RegisterMaterial_Injected(IntPtr _unity_self, IntPtr material, out BatchMaterialID ret);

		// Token: 0x0600321E RID: 12830
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RegisterMaterials_Injected(IntPtr _unity_self, ref ManagedSpanWrapper materialID, ref ManagedSpanWrapper batchMaterialID);

		// Token: 0x0600321F RID: 12831
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UnregisterMaterial_Injected(IntPtr _unity_self, [In] ref BatchMaterialID material);

		// Token: 0x06003220 RID: 12832
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetRegisteredMaterial_Injected(IntPtr _unity_self, [In] ref BatchMaterialID material);

		// Token: 0x06003221 RID: 12833
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RegisterMesh_Injected(IntPtr _unity_self, IntPtr mesh, out BatchMeshID ret);

		// Token: 0x06003222 RID: 12834
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RegisterMeshes_Injected(IntPtr _unity_self, ref ManagedSpanWrapper meshID, ref ManagedSpanWrapper batchMeshID);

		// Token: 0x06003223 RID: 12835
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UnregisterMesh_Injected(IntPtr _unity_self, [In] ref BatchMeshID mesh);

		// Token: 0x06003224 RID: 12836
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetRegisteredMesh_Injected(IntPtr _unity_self, [In] ref BatchMeshID mesh);

		// Token: 0x06003225 RID: 12837
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalBounds_Injected(IntPtr _unity_self, [In] ref Bounds bounds);

		// Token: 0x06003226 RID: 12838
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPickingMaterial_Injected(IntPtr _unity_self, IntPtr material);

		// Token: 0x06003227 RID: 12839
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetErrorMaterial_Injected(IntPtr _unity_self, IntPtr material);

		// Token: 0x06003228 RID: 12840
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLoadingMaterial_Injected(IntPtr _unity_self, IntPtr material);

		// Token: 0x06003229 RID: 12841
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetEnabledViewTypes_Injected(IntPtr _unity_self, ref ManagedSpanWrapper viewTypes);

		// Token: 0x0600322A RID: 12842
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool OcclusionTestAABB_Injected(IntPtr occlusionBuffer, [In] ref Bounds aabb);

		// Token: 0x04001023 RID: 4131
		private IntPtr m_GroupHandle = IntPtr.Zero;

		// Token: 0x04001024 RID: 4132
		private BatchRendererGroup.OnPerformCulling m_PerformCulling;

		// Token: 0x04001025 RID: 4133
		private BatchRendererGroup.OnFinishedCulling m_FinishedCulling;

		// Token: 0x02000512 RID: 1298
		// (Invoke) Token: 0x0600322C RID: 12844
		public delegate JobHandle OnPerformCulling(BatchRendererGroup rendererGroup, BatchCullingContext cullingContext, BatchCullingOutput cullingOutput, IntPtr userContext);

		// Token: 0x02000513 RID: 1299
		// (Invoke) Token: 0x06003230 RID: 12848
		public delegate void OnFinishedCulling(IntPtr customCullingResult);

		// Token: 0x02000514 RID: 1300
		internal static class BindingsMarshaller
		{
			// Token: 0x06003233 RID: 12851 RVA: 0x000611E1 File Offset: 0x0005F3E1
			public static IntPtr ConvertToNative(BatchRendererGroup batchRendererGroup)
			{
				return batchRendererGroup.m_GroupHandle;
			}
		}
	}
}
