using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering
{
	// Token: 0x02000536 RID: 1334
	[NativeHeader("Runtime/Export/RenderPipeline/ScriptableRenderPipeline.bindings.h")]
	[NativeHeader("Runtime/Graphics/ScriptableRenderLoop/ScriptableCulling.h")]
	[NativeHeader("Runtime/Scripting/ScriptingCommonStructDefinitions.h")]
	public struct CullingResults : IEquatable<CullingResults>
	{
		// Token: 0x06003302 RID: 13058
		[FreeFunction("ScriptableRenderPipeline_Bindings::GetLightIndexCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetLightIndexCount(IntPtr cullingResultsPtr);

		// Token: 0x06003303 RID: 13059
		[FreeFunction("ScriptableRenderPipeline_Bindings::GetReflectionProbeIndexCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetReflectionProbeIndexCount(IntPtr cullingResultsPtr);

		// Token: 0x06003304 RID: 13060 RVA: 0x00063BAC File Offset: 0x00061DAC
		[FreeFunction("FillLightAndReflectionProbeIndices")]
		private static void FillLightAndReflectionProbeIndices(IntPtr cullingResultsPtr, ComputeBuffer computeBuffer)
		{
			CullingResults.FillLightAndReflectionProbeIndices_Injected(cullingResultsPtr, (computeBuffer == null) ? ((IntPtr)0) : ComputeBuffer.BindingsMarshaller.ConvertToNative(computeBuffer));
		}

		// Token: 0x06003305 RID: 13061 RVA: 0x00063BD0 File Offset: 0x00061DD0
		[FreeFunction("FillLightAndReflectionProbeIndices")]
		private static void FillLightAndReflectionProbeIndicesGraphicsBuffer(IntPtr cullingResultsPtr, GraphicsBuffer buffer)
		{
			CullingResults.FillLightAndReflectionProbeIndicesGraphicsBuffer_Injected(cullingResultsPtr, (buffer == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(buffer));
		}

		// Token: 0x06003306 RID: 13062
		[FreeFunction("GetLightIndexMapSize")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetLightIndexMapSize(IntPtr cullingResultsPtr);

		// Token: 0x06003307 RID: 13063
		[FreeFunction("GetReflectionProbeIndexMapSize")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetReflectionProbeIndexMapSize(IntPtr cullingResultsPtr);

		// Token: 0x06003308 RID: 13064
		[FreeFunction("FillLightIndexMapScriptable")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FillLightIndexMap(IntPtr cullingResultsPtr, IntPtr indexMapPtr, int indexMapSize);

		// Token: 0x06003309 RID: 13065
		[FreeFunction("FillReflectionProbeIndexMapScriptable")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FillReflectionProbeIndexMap(IntPtr cullingResultsPtr, IntPtr indexMapPtr, int indexMapSize);

		// Token: 0x0600330A RID: 13066
		[FreeFunction("SetLightIndexMapScriptable")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLightIndexMap(IntPtr cullingResultsPtr, IntPtr indexMapPtr, int indexMapSize);

		// Token: 0x0600330B RID: 13067
		[FreeFunction("SetReflectionProbeIndexMapScriptable")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetReflectionProbeIndexMap(IntPtr cullingResultsPtr, IntPtr indexMapPtr, int indexMapSize);

		// Token: 0x0600330C RID: 13068
		[FreeFunction("ScriptableRenderPipeline_Bindings::GetShadowCasterBounds")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetShadowCasterBounds(IntPtr cullingResultsPtr, int lightIndex, out Bounds bounds);

		// Token: 0x0600330D RID: 13069
		[FreeFunction("ScriptableRenderPipeline_Bindings::ComputeSpotShadowMatricesAndCullingPrimitives")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ComputeSpotShadowMatricesAndCullingPrimitives(IntPtr cullingResultsPtr, int activeLightIndex, out Matrix4x4 viewMatrix, out Matrix4x4 projMatrix, out ShadowSplitData shadowSplitData);

		// Token: 0x0600330E RID: 13070
		[FreeFunction("ScriptableRenderPipeline_Bindings::ComputePointShadowMatricesAndCullingPrimitives")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ComputePointShadowMatricesAndCullingPrimitives(IntPtr cullingResultsPtr, int activeLightIndex, CubemapFace cubemapFace, float fovBias, out Matrix4x4 viewMatrix, out Matrix4x4 projMatrix, out ShadowSplitData shadowSplitData);

		// Token: 0x0600330F RID: 13071 RVA: 0x00063BF4 File Offset: 0x00061DF4
		[FreeFunction("ScriptableRenderPipeline_Bindings::ComputeDirectionalShadowMatricesAndCullingPrimitives")]
		private static bool ComputeDirectionalShadowMatricesAndCullingPrimitives(IntPtr cullingResultsPtr, int activeLightIndex, int splitIndex, int splitCount, Vector3 splitRatio, int shadowResolution, float shadowNearPlaneOffset, out Matrix4x4 viewMatrix, out Matrix4x4 projMatrix, out ShadowSplitData shadowSplitData)
		{
			return CullingResults.ComputeDirectionalShadowMatricesAndCullingPrimitives_Injected(cullingResultsPtr, activeLightIndex, splitIndex, splitCount, ref splitRatio, shadowResolution, shadowNearPlaneOffset, out viewMatrix, out projMatrix, out shadowSplitData);
		}

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x06003310 RID: 13072 RVA: 0x00063C16 File Offset: 0x00061E16
		public unsafe NativeArray<VisibleLight> visibleLights
		{
			get
			{
				return this.GetNativeArray<VisibleLight>((void*)this.m_AllocationInfo->visibleLightsPtr, this.m_AllocationInfo->visibleLightCount);
			}
		}

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x06003311 RID: 13073 RVA: 0x00063C34 File Offset: 0x00061E34
		public unsafe NativeArray<VisibleLight> visibleOffscreenVertexLights
		{
			get
			{
				return this.GetNativeArray<VisibleLight>((void*)this.m_AllocationInfo->visibleOffscreenVertexLightsPtr, this.m_AllocationInfo->visibleOffscreenVertexLightCount);
			}
		}

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x06003312 RID: 13074 RVA: 0x00063C52 File Offset: 0x00061E52
		public unsafe NativeArray<VisibleReflectionProbe> visibleReflectionProbes
		{
			get
			{
				return this.GetNativeArray<VisibleReflectionProbe>((void*)this.m_AllocationInfo->visibleReflectionProbesPtr, this.m_AllocationInfo->visibleReflectionProbeCount);
			}
		}

		// Token: 0x06003313 RID: 13075 RVA: 0x00063C70 File Offset: 0x00061E70
		private unsafe NativeArray<T> GetNativeArray<T>(void* dataPointer, int length) where T : struct
		{
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>(dataPointer, length, Allocator.Invalid);
		}

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x06003314 RID: 13076 RVA: 0x00063C8C File Offset: 0x00061E8C
		public int lightIndexCount
		{
			get
			{
				return CullingResults.GetLightIndexCount(this.ptr);
			}
		}

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x06003315 RID: 13077 RVA: 0x00063CAC File Offset: 0x00061EAC
		public int reflectionProbeIndexCount
		{
			get
			{
				return CullingResults.GetReflectionProbeIndexCount(this.ptr);
			}
		}

		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x06003316 RID: 13078 RVA: 0x00063CCC File Offset: 0x00061ECC
		public int lightAndReflectionProbeIndexCount
		{
			get
			{
				return CullingResults.GetLightIndexCount(this.ptr) + CullingResults.GetReflectionProbeIndexCount(this.ptr);
			}
		}

		// Token: 0x06003317 RID: 13079 RVA: 0x00063CF5 File Offset: 0x00061EF5
		public void FillLightAndReflectionProbeIndices(ComputeBuffer computeBuffer)
		{
			CullingResults.FillLightAndReflectionProbeIndices(this.ptr, computeBuffer);
		}

		// Token: 0x06003318 RID: 13080 RVA: 0x00063D05 File Offset: 0x00061F05
		public void FillLightAndReflectionProbeIndices(GraphicsBuffer buffer)
		{
			CullingResults.FillLightAndReflectionProbeIndicesGraphicsBuffer(this.ptr, buffer);
		}

		// Token: 0x06003319 RID: 13081 RVA: 0x00063D18 File Offset: 0x00061F18
		public NativeArray<int> GetLightIndexMap(Allocator allocator)
		{
			int lightIndexMapSize = CullingResults.GetLightIndexMapSize(this.ptr);
			NativeArray<int> nativeArray = new NativeArray<int>(lightIndexMapSize, allocator, NativeArrayOptions.UninitializedMemory);
			CullingResults.FillLightIndexMap(this.ptr, (IntPtr)nativeArray.GetUnsafePtr<int>(), lightIndexMapSize);
			return nativeArray;
		}

		// Token: 0x0600331A RID: 13082 RVA: 0x00063D59 File Offset: 0x00061F59
		public void SetLightIndexMap(NativeArray<int> lightIndexMap)
		{
			CullingResults.SetLightIndexMap(this.ptr, (IntPtr)lightIndexMap.GetUnsafeReadOnlyPtr<int>(), lightIndexMap.Length);
		}

		// Token: 0x0600331B RID: 13083 RVA: 0x00063D7C File Offset: 0x00061F7C
		public NativeArray<int> GetReflectionProbeIndexMap(Allocator allocator)
		{
			int reflectionProbeIndexMapSize = CullingResults.GetReflectionProbeIndexMapSize(this.ptr);
			NativeArray<int> nativeArray = new NativeArray<int>(reflectionProbeIndexMapSize, allocator, NativeArrayOptions.UninitializedMemory);
			CullingResults.FillReflectionProbeIndexMap(this.ptr, (IntPtr)nativeArray.GetUnsafePtr<int>(), reflectionProbeIndexMapSize);
			return nativeArray;
		}

		// Token: 0x0600331C RID: 13084 RVA: 0x00063DBD File Offset: 0x00061FBD
		public void SetReflectionProbeIndexMap(NativeArray<int> lightIndexMap)
		{
			CullingResults.SetReflectionProbeIndexMap(this.ptr, (IntPtr)lightIndexMap.GetUnsafeReadOnlyPtr<int>(), lightIndexMap.Length);
		}

		// Token: 0x0600331D RID: 13085 RVA: 0x00063DE0 File Offset: 0x00061FE0
		public bool GetShadowCasterBounds(int lightIndex, out Bounds outBounds)
		{
			return CullingResults.GetShadowCasterBounds(this.ptr, lightIndex, out outBounds);
		}

		// Token: 0x0600331E RID: 13086 RVA: 0x00063E00 File Offset: 0x00062000
		public bool ComputeSpotShadowMatricesAndCullingPrimitives(int activeLightIndex, out Matrix4x4 viewMatrix, out Matrix4x4 projMatrix, out ShadowSplitData shadowSplitData)
		{
			return CullingResults.ComputeSpotShadowMatricesAndCullingPrimitives(this.ptr, activeLightIndex, out viewMatrix, out projMatrix, out shadowSplitData);
		}

		// Token: 0x0600331F RID: 13087 RVA: 0x00063E24 File Offset: 0x00062024
		public bool ComputePointShadowMatricesAndCullingPrimitives(int activeLightIndex, CubemapFace cubemapFace, float fovBias, out Matrix4x4 viewMatrix, out Matrix4x4 projMatrix, out ShadowSplitData shadowSplitData)
		{
			return CullingResults.ComputePointShadowMatricesAndCullingPrimitives(this.ptr, activeLightIndex, cubemapFace, fovBias, out viewMatrix, out projMatrix, out shadowSplitData);
		}

		// Token: 0x06003320 RID: 13088 RVA: 0x00063E4C File Offset: 0x0006204C
		public bool ComputeDirectionalShadowMatricesAndCullingPrimitives(int activeLightIndex, int splitIndex, int splitCount, Vector3 splitRatio, int shadowResolution, float shadowNearPlaneOffset, out Matrix4x4 viewMatrix, out Matrix4x4 projMatrix, out ShadowSplitData shadowSplitData)
		{
			return CullingResults.ComputeDirectionalShadowMatricesAndCullingPrimitives(this.ptr, activeLightIndex, splitIndex, splitCount, splitRatio, shadowResolution, shadowNearPlaneOffset, out viewMatrix, out projMatrix, out shadowSplitData);
		}

		// Token: 0x06003321 RID: 13089 RVA: 0x00002925 File Offset: 0x00000B25
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		internal void Validate()
		{
		}

		// Token: 0x06003322 RID: 13090 RVA: 0x00063E78 File Offset: 0x00062078
		public bool Equals(CullingResults other)
		{
			return this.ptr.Equals(other.ptr) && this.m_AllocationInfo == other.m_AllocationInfo;
		}

		// Token: 0x06003323 RID: 13091 RVA: 0x00063EB4 File Offset: 0x000620B4
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is CullingResults && this.Equals((CullingResults)obj);
		}

		// Token: 0x06003324 RID: 13092 RVA: 0x00063EEC File Offset: 0x000620EC
		public override int GetHashCode()
		{
			int hashCode = this.ptr.GetHashCode();
			return (hashCode * 397) ^ this.m_AllocationInfo;
		}

		// Token: 0x06003325 RID: 13093 RVA: 0x00063F20 File Offset: 0x00062120
		public static bool operator ==(CullingResults left, CullingResults right)
		{
			return left.Equals(right);
		}

		// Token: 0x06003326 RID: 13094 RVA: 0x00063F3C File Offset: 0x0006213C
		public static bool operator !=(CullingResults left, CullingResults right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06003327 RID: 13095
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FillLightAndReflectionProbeIndices_Injected(IntPtr cullingResultsPtr, IntPtr computeBuffer);

		// Token: 0x06003328 RID: 13096
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FillLightAndReflectionProbeIndicesGraphicsBuffer_Injected(IntPtr cullingResultsPtr, IntPtr buffer);

		// Token: 0x06003329 RID: 13097
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ComputeDirectionalShadowMatricesAndCullingPrimitives_Injected(IntPtr cullingResultsPtr, int activeLightIndex, int splitIndex, int splitCount, [In] ref Vector3 splitRatio, int shadowResolution, float shadowNearPlaneOffset, out Matrix4x4 viewMatrix, out Matrix4x4 projMatrix, out ShadowSplitData shadowSplitData);

		// Token: 0x04001111 RID: 4369
		[VisibleToOtherModules(new string[] { "UnityEngine.VFXModule" })]
		internal IntPtr ptr;

		// Token: 0x04001112 RID: 4370
		private unsafe CullingAllocationInfo* m_AllocationInfo;
	}
}
