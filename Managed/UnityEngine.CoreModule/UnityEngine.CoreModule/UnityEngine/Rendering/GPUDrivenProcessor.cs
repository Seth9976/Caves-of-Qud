using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x0200051A RID: 1306
	[RequiredByNativeCode]
	[NativeHeader("Runtime/Camera/GPUDrivenProcessor.h")]
	internal class GPUDrivenProcessor
	{
		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x06003246 RID: 12870 RVA: 0x00061204 File Offset: 0x0005F404
		// (set) Token: 0x06003247 RID: 12871 RVA: 0x0006120C File Offset: 0x0005F40C
		internal List<Mesh> scratchMeshes { get; private set; }

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x06003248 RID: 12872 RVA: 0x00061215 File Offset: 0x0005F415
		// (set) Token: 0x06003249 RID: 12873 RVA: 0x0006121D File Offset: 0x0005F41D
		internal List<Material> scratchMaterials { get; private set; }

		// Token: 0x0600324A RID: 12874 RVA: 0x00061226 File Offset: 0x0005F426
		public GPUDrivenProcessor()
		{
			this.m_Ptr = GPUDrivenProcessor.Internal_Create();
			this.scratchMeshes = new List<Mesh>();
			this.scratchMaterials = new List<Material>();
		}

		// Token: 0x0600324B RID: 12875 RVA: 0x00061254 File Offset: 0x0005F454
		~GPUDrivenProcessor()
		{
			this.Destroy();
		}

		// Token: 0x0600324C RID: 12876 RVA: 0x00061284 File Offset: 0x0005F484
		public void Dispose()
		{
			this.scratchMeshes = null;
			this.scratchMaterials = null;
			this.Destroy();
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600324D RID: 12877 RVA: 0x000612A8 File Offset: 0x0005F4A8
		private void Destroy()
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				GPUDrivenProcessor.Internal_Destroy(this.m_Ptr);
				this.m_Ptr = IntPtr.Zero;
			}
		}

		// Token: 0x0600324E RID: 12878
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_Create();

		// Token: 0x0600324F RID: 12879
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Destroy(IntPtr ptr);

		// Token: 0x06003250 RID: 12880 RVA: 0x000612E4 File Offset: 0x0005F4E4
		private unsafe void EnableGPUDrivenRenderingAndDispatchRendererData(ReadOnlySpan<int> renderersID, GPUDrivenRendererDataNativeCallback callback, List<Mesh> meshes, List<Material> materials, GPUDrivenRendererDataCallback param, bool materialUpdateOnly)
		{
			IntPtr intPtr = GPUDrivenProcessor.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ReadOnlySpan<int> readOnlySpan = renderersID;
			fixed (int* pinnableReference = readOnlySpan.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, readOnlySpan.Length);
				GPUDrivenProcessor.EnableGPUDrivenRenderingAndDispatchRendererData_Injected(intPtr, ref managedSpanWrapper, callback, meshes, materials, param, materialUpdateOnly);
			}
		}

		// Token: 0x06003251 RID: 12881 RVA: 0x0006132E File Offset: 0x0005F52E
		public void EnableGPUDrivenRenderingAndDispatchRendererData(ReadOnlySpan<int> renderersID, GPUDrivenRendererDataCallback callback, bool materialUpdateOnly = false)
		{
			this.scratchMeshes.Clear();
			this.scratchMaterials.Clear();
			this.EnableGPUDrivenRenderingAndDispatchRendererData(renderersID, GPUDrivenProcessor.s_NativeRendererCallback, this.scratchMeshes, this.scratchMaterials, callback, materialUpdateOnly);
		}

		// Token: 0x06003252 RID: 12882 RVA: 0x00061364 File Offset: 0x0005F564
		public unsafe void DisableGPUDrivenRendering(ReadOnlySpan<int> renderersID)
		{
			IntPtr intPtr = GPUDrivenProcessor.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ReadOnlySpan<int> readOnlySpan = renderersID;
			fixed (int* pinnableReference = readOnlySpan.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, readOnlySpan.Length);
				GPUDrivenProcessor.DisableGPUDrivenRendering_Injected(intPtr, ref managedSpanWrapper);
			}
		}

		// Token: 0x06003253 RID: 12883 RVA: 0x000613A8 File Offset: 0x0005F5A8
		private unsafe void DispatchLODGroupData(ReadOnlySpan<int> lodGroupID, GPUDrivenLODGroupDataNativeCallback callback, GPUDrivenLODGroupDataCallback param)
		{
			IntPtr intPtr = GPUDrivenProcessor.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ReadOnlySpan<int> readOnlySpan = lodGroupID;
			fixed (int* pinnableReference = readOnlySpan.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, readOnlySpan.Length);
				GPUDrivenProcessor.DispatchLODGroupData_Injected(intPtr, ref managedSpanWrapper, callback, param);
			}
		}

		// Token: 0x06003254 RID: 12884 RVA: 0x000613EC File Offset: 0x0005F5EC
		public void DispatchLODGroupData(ReadOnlySpan<int> lodGroupID, GPUDrivenLODGroupDataCallback callback)
		{
			this.DispatchLODGroupData(lodGroupID, GPUDrivenProcessor.s_NativeLODGroupCallback, callback);
		}

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x06003256 RID: 12886 RVA: 0x00061424 File Offset: 0x0005F624
		// (set) Token: 0x06003255 RID: 12885 RVA: 0x00061400 File Offset: 0x0005F600
		public bool enablePartialRendering
		{
			get
			{
				IntPtr intPtr = GPUDrivenProcessor.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return GPUDrivenProcessor.get_enablePartialRendering_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = GPUDrivenProcessor.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				GPUDrivenProcessor.set_enablePartialRendering_Injected(intPtr, value);
			}
		}

		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x06003258 RID: 12888 RVA: 0x0006146C File Offset: 0x0005F66C
		// (set) Token: 0x06003257 RID: 12887 RVA: 0x00061448 File Offset: 0x0005F648
		public bool enableMaterialFilters
		{
			get
			{
				IntPtr intPtr = GPUDrivenProcessor.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return GPUDrivenProcessor.get_enableMaterialFilters_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = GPUDrivenProcessor.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				GPUDrivenProcessor.set_enableMaterialFilters_Injected(intPtr, value);
			}
		}

		// Token: 0x06003259 RID: 12889 RVA: 0x00061490 File Offset: 0x0005F690
		public void AddMaterialFilters([NotNull] GPUDrivenMaterialFilterEntry[] filters)
		{
			if (filters == null)
			{
				ThrowHelper.ThrowArgumentNullException(filters, "filters");
			}
			IntPtr intPtr = GPUDrivenProcessor.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			GPUDrivenProcessor.AddMaterialFilters_Injected(intPtr, filters);
		}

		// Token: 0x0600325A RID: 12890 RVA: 0x000614C4 File Offset: 0x0005F6C4
		public void ClearMaterialFilters()
		{
			IntPtr intPtr = GPUDrivenProcessor.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			GPUDrivenProcessor.ClearMaterialFilters_Injected(intPtr);
		}

		// Token: 0x0600325B RID: 12891 RVA: 0x000614E8 File Offset: 0x0005F6E8
		public int GetMaterialFilterFlags(Material material)
		{
			IntPtr intPtr = GPUDrivenProcessor.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return GPUDrivenProcessor.GetMaterialFilterFlags_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Material>(material));
		}

		// Token: 0x0600325C RID: 12892 RVA: 0x00061510 File Offset: 0x0005F710
		[FreeFunction("GPUDrivenProcessor::ClassifyMaterials", IsThreadSafe = true)]
		private unsafe static int ClassifyMaterialsImpl(ReadOnlySpan<int> materialIDs, Span<int> unsupportedMaterialIDs, Span<int> supportedMaterialIDs, Span<GPUDrivenPackedMaterialData> supportedPackedMaterialDatas)
		{
			ReadOnlySpan<int> readOnlySpan = materialIDs;
			fixed (int* ptr = readOnlySpan.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
				Span<int> span = unsupportedMaterialIDs;
				fixed (int* ptr2 = span.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, span.Length);
					Span<int> span2 = supportedMaterialIDs;
					fixed (int* ptr3 = span2.GetPinnableReference())
					{
						ManagedSpanWrapper managedSpanWrapper3 = new ManagedSpanWrapper((void*)ptr3, span2.Length);
						Span<GPUDrivenPackedMaterialData> span3 = supportedPackedMaterialDatas;
						int num;
						fixed (GPUDrivenPackedMaterialData* pinnableReference = span3.GetPinnableReference())
						{
							ManagedSpanWrapper managedSpanWrapper4 = new ManagedSpanWrapper((void*)pinnableReference, span3.Length);
							num = GPUDrivenProcessor.ClassifyMaterialsImpl_Injected(ref managedSpanWrapper, ref managedSpanWrapper2, ref managedSpanWrapper3, ref managedSpanWrapper4);
							ptr = null;
							ptr2 = null;
							ptr3 = null;
						}
						return num;
					}
				}
			}
		}

		// Token: 0x0600325D RID: 12893 RVA: 0x000615B0 File Offset: 0x0005F7B0
		public static int ClassifyMaterials(NativeArray<int> materialIDs, NativeArray<int> unsupportedMaterialIDs, NativeArray<int> supportedMaterialIDs, NativeArray<GPUDrivenPackedMaterialData> supportedPackedMaterialDatas)
		{
			return GPUDrivenProcessor.ClassifyMaterialsImpl(in materialIDs, in unsupportedMaterialIDs, in supportedMaterialIDs, in supportedPackedMaterialDatas);
		}

		// Token: 0x0600325F RID: 12895
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EnableGPUDrivenRenderingAndDispatchRendererData_Injected(IntPtr _unity_self, ref ManagedSpanWrapper renderersID, GPUDrivenRendererDataNativeCallback callback, List<Mesh> meshes, List<Material> materials, GPUDrivenRendererDataCallback param, bool materialUpdateOnly);

		// Token: 0x06003260 RID: 12896
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DisableGPUDrivenRendering_Injected(IntPtr _unity_self, ref ManagedSpanWrapper renderersID);

		// Token: 0x06003261 RID: 12897
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DispatchLODGroupData_Injected(IntPtr _unity_self, ref ManagedSpanWrapper lodGroupID, GPUDrivenLODGroupDataNativeCallback callback, GPUDrivenLODGroupDataCallback param);

		// Token: 0x06003262 RID: 12898
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_enablePartialRendering_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06003263 RID: 12899
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_enablePartialRendering_Injected(IntPtr _unity_self);

		// Token: 0x06003264 RID: 12900
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_enableMaterialFilters_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06003265 RID: 12901
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_enableMaterialFilters_Injected(IntPtr _unity_self);

		// Token: 0x06003266 RID: 12902
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddMaterialFilters_Injected(IntPtr _unity_self, GPUDrivenMaterialFilterEntry[] filters);

		// Token: 0x06003267 RID: 12903
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClearMaterialFilters_Injected(IntPtr _unity_self);

		// Token: 0x06003268 RID: 12904
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMaterialFilterFlags_Injected(IntPtr _unity_self, IntPtr material);

		// Token: 0x06003269 RID: 12905
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int ClassifyMaterialsImpl_Injected(ref ManagedSpanWrapper materialIDs, ref ManagedSpanWrapper unsupportedMaterialIDs, ref ManagedSpanWrapper supportedMaterialIDs, ref ManagedSpanWrapper supportedPackedMaterialDatas);

		// Token: 0x04001026 RID: 4134
		internal IntPtr m_Ptr;

		// Token: 0x04001029 RID: 4137
		private static GPUDrivenRendererDataNativeCallback s_NativeRendererCallback = delegate(in GPUDrivenRendererGroupDataNative nativeData, List<Mesh> meshes, List<Material> materials, GPUDrivenRendererDataCallback callback)
		{
			NativeArray<int> nativeArray = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>((void*)nativeData.rendererGroupID, nativeData.rendererGroupCount, Allocator.Invalid);
			NativeArray<Bounds> nativeArray2 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Bounds>((void*)nativeData.localBounds, (nativeData.localBounds == null) ? 0 : nativeData.rendererGroupCount, Allocator.Invalid);
			NativeArray<Vector4> nativeArray3 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Vector4>((void*)nativeData.lightmapScaleOffset, nativeData.rendererGroupCount, Allocator.Invalid);
			NativeArray<int> nativeArray4 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>((void*)nativeData.gameObjectLayer, nativeData.rendererGroupCount, Allocator.Invalid);
			NativeArray<uint> nativeArray5 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<uint>((void*)nativeData.renderingLayerMask, nativeData.rendererGroupCount, Allocator.Invalid);
			NativeArray<int> nativeArray6 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>((void*)nativeData.lodGroupID, (nativeData.lodGroupID == null) ? 0 : nativeData.rendererGroupCount, Allocator.Invalid);
			NativeArray<int> nativeArray7 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>((void*)nativeData.motionVecGenMode, nativeData.rendererGroupCount, Allocator.Invalid);
			NativeArray<GPUDrivenPackedRendererData> nativeArray8 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<GPUDrivenPackedRendererData>((void*)nativeData.packedRendererData, nativeData.rendererGroupCount, Allocator.Invalid);
			NativeArray<int> nativeArray9 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>((void*)nativeData.rendererPriority, nativeData.rendererGroupCount, Allocator.Invalid);
			NativeArray<int> nativeArray10 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>((void*)nativeData.meshIndex, nativeData.rendererGroupCount, Allocator.Invalid);
			NativeArray<short> nativeArray11 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<short>((void*)nativeData.subMeshStartIndex, nativeData.rendererGroupCount, Allocator.Invalid);
			NativeArray<int> nativeArray12 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>((void*)nativeData.materialsOffset, nativeData.rendererGroupCount, Allocator.Invalid);
			NativeArray<short> nativeArray13 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<short>((void*)nativeData.materialsCount, nativeData.rendererGroupCount, Allocator.Invalid);
			NativeArray<int> nativeArray14 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>(null, 0, Allocator.Invalid);
			NativeArray<int> nativeArray15 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>(null, 0, Allocator.Invalid);
			NativeArray<GPUDrivenRendererEditorData> nativeArray16 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<GPUDrivenRendererEditorData>((void*)nativeData.editorData, nativeData.rendererGroupCount, Allocator.Invalid);
			NativeArray<int> nativeArray17 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>((void*)nativeData.invalidRendererGroupID, nativeData.invalidRendererGroupIDCount, Allocator.Invalid);
			NativeArray<Matrix4x4> nativeArray18 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Matrix4x4>((void*)nativeData.localToWorldMatrix, (nativeData.localToWorldMatrix == null) ? 0 : nativeData.rendererGroupCount, Allocator.Invalid);
			NativeArray<Matrix4x4> nativeArray19 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Matrix4x4>((void*)nativeData.prevLocalToWorldMatrix, (nativeData.prevLocalToWorldMatrix == null) ? 0 : nativeData.rendererGroupCount, Allocator.Invalid);
			NativeArray<int> nativeArray20 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>(null, 0, Allocator.Invalid);
			NativeArray<int> nativeArray21 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>((void*)nativeData.meshID, nativeData.meshCount, Allocator.Invalid);
			NativeArray<short> nativeArray22 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<short>((void*)nativeData.subMeshCount, nativeData.meshCount, Allocator.Invalid);
			NativeArray<int> nativeArray23 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>((void*)nativeData.subMeshDescOffset, nativeData.meshCount, Allocator.Invalid);
			NativeArray<SubMeshDescriptor> nativeArray24 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<SubMeshDescriptor>((void*)nativeData.subMeshDesc, nativeData.subMeshDescCount, Allocator.Invalid);
			NativeArray<int> nativeArray25 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>((void*)nativeData.materialIndex, nativeData.materialIndexCount, Allocator.Invalid);
			NativeArray<int> nativeArray26 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>((void*)nativeData.materialID, nativeData.materialCount, Allocator.Invalid);
			NativeArray<GPUDrivenPackedMaterialData> nativeArray27 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<GPUDrivenPackedMaterialData>((void*)nativeData.packedMaterialData, (nativeData.packedMaterialData == null) ? 0 : nativeData.materialCount, Allocator.Invalid);
			NativeArray<int> nativeArray28 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>((void*)nativeData.materialFilterFlags, (nativeData.packedMaterialData == null) ? 0 : nativeData.materialCount, Allocator.Invalid);
			GPUDrivenRendererGroupData gpudrivenRendererGroupData = new GPUDrivenRendererGroupData
			{
				rendererGroupID = nativeArray,
				localBounds = nativeArray2,
				lightmapScaleOffset = nativeArray3,
				gameObjectLayer = nativeArray4,
				renderingLayerMask = nativeArray5,
				lodGroupID = nativeArray6,
				lightmapIndex = nativeArray7,
				packedRendererData = nativeArray8,
				rendererPriority = nativeArray9,
				meshIndex = nativeArray10,
				subMeshStartIndex = nativeArray11,
				materialsOffset = nativeArray12,
				materialsCount = nativeArray13,
				instancesOffset = nativeArray14,
				instancesCount = nativeArray15,
				editorData = nativeArray16,
				invalidRendererGroupID = nativeArray17,
				localToWorldMatrix = nativeArray18,
				prevLocalToWorldMatrix = nativeArray19,
				rendererGroupIndex = nativeArray20,
				meshID = nativeArray21,
				subMeshCount = nativeArray22,
				subMeshDescOffset = nativeArray23,
				subMeshDesc = nativeArray24,
				materialIndex = nativeArray25,
				materialID = nativeArray26,
				packedMaterialData = nativeArray27,
				materialFilterFlags = nativeArray28
			};
			callback(in gpudrivenRendererGroupData, meshes, materials);
		};

		// Token: 0x0400102A RID: 4138
		private static GPUDrivenLODGroupDataNativeCallback s_NativeLODGroupCallback = delegate(in GPUDrivenLODGroupDataNative nativeData, GPUDrivenLODGroupDataCallback callback)
		{
			NativeArray<int> nativeArray29 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>((void*)nativeData.lodGroupID, nativeData.lodGroupCount, Allocator.Invalid);
			NativeArray<int> nativeArray30 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>((void*)nativeData.lodOffset, nativeData.lodGroupCount, Allocator.Invalid);
			NativeArray<int> nativeArray31 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>((void*)nativeData.lodCount, nativeData.lodGroupCount, Allocator.Invalid);
			NativeArray<LODFadeMode> nativeArray32 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<LODFadeMode>((void*)nativeData.fadeMode, nativeData.lodGroupCount, Allocator.Invalid);
			NativeArray<Vector3> nativeArray33 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Vector3>((void*)nativeData.worldSpaceReferencePoint, nativeData.lodGroupCount, Allocator.Invalid);
			NativeArray<float> nativeArray34 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<float>((void*)nativeData.worldSpaceSize, nativeData.lodGroupCount, Allocator.Invalid);
			NativeArray<short> nativeArray35 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<short>((void*)nativeData.renderersCount, nativeData.lodGroupCount, Allocator.Invalid);
			NativeArray<bool> nativeArray36 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<bool>((void*)nativeData.lastLODIsBillboard, nativeData.lodGroupCount, Allocator.Invalid);
			NativeArray<byte> nativeArray37 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<byte>((void*)nativeData.forceLODMask, nativeData.lodGroupCount, Allocator.Invalid);
			NativeArray<int> nativeArray38 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>((void*)nativeData.invalidLODGroupID, nativeData.invalidLODGroupCount, Allocator.Invalid);
			NativeArray<short> nativeArray39 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<short>((void*)nativeData.lodRenderersCount, nativeData.lodDataCount, Allocator.Invalid);
			NativeArray<float> nativeArray40 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<float>((void*)nativeData.lodScreenRelativeTransitionHeight, nativeData.lodDataCount, Allocator.Invalid);
			NativeArray<float> nativeArray41 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<float>((void*)nativeData.lodFadeTransitionWidth, nativeData.lodDataCount, Allocator.Invalid);
			GPUDrivenLODGroupData gpudrivenLODGroupData = new GPUDrivenLODGroupData
			{
				lodGroupID = nativeArray29,
				lodOffset = nativeArray30,
				lodCount = nativeArray31,
				fadeMode = nativeArray32,
				worldSpaceReferencePoint = nativeArray33,
				worldSpaceSize = nativeArray34,
				renderersCount = nativeArray35,
				lastLODIsBillboard = nativeArray36,
				forceLODMask = nativeArray37,
				invalidLODGroupID = nativeArray38,
				lodRenderersCount = nativeArray39,
				lodScreenRelativeTransitionHeight = nativeArray40,
				lodFadeTransitionWidth = nativeArray41
			};
			callback(in gpudrivenLODGroupData);
		};

		// Token: 0x0200051B RID: 1307
		internal static class BindingsMarshaller
		{
			// Token: 0x0600326A RID: 12906 RVA: 0x0006160F File Offset: 0x0005F80F
			public static IntPtr ConvertToNative(GPUDrivenProcessor obj)
			{
				return obj.m_Ptr;
			}
		}
	}
}
