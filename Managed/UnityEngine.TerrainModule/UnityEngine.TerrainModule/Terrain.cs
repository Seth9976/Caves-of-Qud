using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000004 RID: 4
	[UsedByNativeCode]
	[NativeHeader("Modules/Terrain/Public/Terrain.h")]
	[StaticAccessor("GetITerrainManager()", StaticAccessorType.Arrow)]
	[NativeHeader("TerrainScriptingClasses.h")]
	[NativeHeader("Runtime/Interfaces/ITerrainManager.h")]
	public sealed class Terrain : Behaviour
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002078 File Offset: 0x00000278
		public TerrainData terrainData
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<TerrainData>(Terrain.get_terrainData_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Terrain.set_terrainData_Injected(intPtr, Object.MarshalledUnityObject.Marshal<TerrainData>(value));
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020A0 File Offset: 0x000002A0
		// (set) Token: 0x06000004 RID: 4 RVA: 0x000020C4 File Offset: 0x000002C4
		public float treeDistance
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Terrain.get_treeDistance_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Terrain.set_treeDistance_Injected(intPtr, value);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020E8 File Offset: 0x000002E8
		// (set) Token: 0x06000006 RID: 6 RVA: 0x0000210C File Offset: 0x0000030C
		public float treeBillboardDistance
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Terrain.get_treeBillboardDistance_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Terrain.set_treeBillboardDistance_Injected(intPtr, value);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002130 File Offset: 0x00000330
		// (set) Token: 0x06000008 RID: 8 RVA: 0x00002154 File Offset: 0x00000354
		public float treeCrossFadeLength
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Terrain.get_treeCrossFadeLength_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Terrain.set_treeCrossFadeLength_Injected(intPtr, value);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002178 File Offset: 0x00000378
		// (set) Token: 0x0600000A RID: 10 RVA: 0x0000219C File Offset: 0x0000039C
		public int treeMaximumFullLODCount
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Terrain.get_treeMaximumFullLODCount_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Terrain.set_treeMaximumFullLODCount_Injected(intPtr, value);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000021C0 File Offset: 0x000003C0
		// (set) Token: 0x0600000C RID: 12 RVA: 0x000021E4 File Offset: 0x000003E4
		public float detailObjectDistance
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Terrain.get_detailObjectDistance_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Terrain.set_detailObjectDistance_Injected(intPtr, value);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002208 File Offset: 0x00000408
		// (set) Token: 0x0600000E RID: 14 RVA: 0x0000222C File Offset: 0x0000042C
		public float detailObjectDensity
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Terrain.get_detailObjectDensity_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Terrain.set_detailObjectDensity_Injected(intPtr, value);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002250 File Offset: 0x00000450
		// (set) Token: 0x06000010 RID: 16 RVA: 0x00002274 File Offset: 0x00000474
		public float heightmapPixelError
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Terrain.get_heightmapPixelError_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Terrain.set_heightmapPixelError_Injected(intPtr, value);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002298 File Offset: 0x00000498
		// (set) Token: 0x06000012 RID: 18 RVA: 0x000022BC File Offset: 0x000004BC
		public int heightmapMaximumLOD
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Terrain.get_heightmapMaximumLOD_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Terrain.set_heightmapMaximumLOD_Injected(intPtr, value);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000022E0 File Offset: 0x000004E0
		// (set) Token: 0x06000014 RID: 20 RVA: 0x00002304 File Offset: 0x00000504
		public int heightmapMinimumLODSimplification
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Terrain.get_heightmapMinimumLODSimplification_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Terrain.set_heightmapMinimumLODSimplification_Injected(intPtr, value);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002328 File Offset: 0x00000528
		// (set) Token: 0x06000016 RID: 22 RVA: 0x0000234C File Offset: 0x0000054C
		public float basemapDistance
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Terrain.get_basemapDistance_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Terrain.set_basemapDistance_Injected(intPtr, value);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002370 File Offset: 0x00000570
		// (set) Token: 0x06000018 RID: 24 RVA: 0x00002394 File Offset: 0x00000594
		[NativeProperty("StaticLightmapIndexInt")]
		public int lightmapIndex
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Terrain.get_lightmapIndex_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Terrain.set_lightmapIndex_Injected(intPtr, value);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000023B8 File Offset: 0x000005B8
		// (set) Token: 0x0600001A RID: 26 RVA: 0x000023DC File Offset: 0x000005DC
		[NativeProperty("DynamicLightmapIndexInt")]
		public int realtimeLightmapIndex
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Terrain.get_realtimeLightmapIndex_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Terrain.set_realtimeLightmapIndex_Injected(intPtr, value);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002400 File Offset: 0x00000600
		// (set) Token: 0x0600001C RID: 28 RVA: 0x00002428 File Offset: 0x00000628
		[NativeProperty("StaticLightmapST")]
		public Vector4 lightmapScaleOffset
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector4 vector;
				Terrain.get_lightmapScaleOffset_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Terrain.set_lightmapScaleOffset_Injected(intPtr, ref value);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001D RID: 29 RVA: 0x0000244C File Offset: 0x0000064C
		// (set) Token: 0x0600001E RID: 30 RVA: 0x00002474 File Offset: 0x00000674
		[NativeProperty("DynamicLightmapST")]
		public Vector4 realtimeLightmapScaleOffset
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector4 vector;
				Terrain.get_realtimeLightmapScaleOffset_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Terrain.set_realtimeLightmapScaleOffset_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002498 File Offset: 0x00000698
		// (set) Token: 0x06000020 RID: 32 RVA: 0x000024BC File Offset: 0x000006BC
		[NativeProperty("FreeUnusedRenderingResourcesObsolete")]
		[Obsolete("Terrain.freeUnusedRenderingResources is obsolete; use keepUnusedRenderingResources instead.")]
		public bool freeUnusedRenderingResources
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Terrain.get_freeUnusedRenderingResources_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Terrain.set_freeUnusedRenderingResources_Injected(intPtr, value);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000024E0 File Offset: 0x000006E0
		// (set) Token: 0x06000022 RID: 34 RVA: 0x00002504 File Offset: 0x00000704
		[NativeProperty("KeepUnusedRenderingResources")]
		public bool keepUnusedRenderingResources
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Terrain.get_keepUnusedRenderingResources_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Terrain.set_keepUnusedRenderingResources_Injected(intPtr, value);
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002528 File Offset: 0x00000728
		public bool GetKeepUnusedCameraRenderingResources(int cameraInstanceID)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Terrain.GetKeepUnusedCameraRenderingResources_Injected(intPtr, cameraInstanceID);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000254C File Offset: 0x0000074C
		public void SetKeepUnusedCameraRenderingResources(int cameraInstanceID, bool keepUnused)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Terrain.SetKeepUnusedCameraRenderingResources_Injected(intPtr, cameraInstanceID, keepUnused);
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002570 File Offset: 0x00000770
		// (set) Token: 0x06000026 RID: 38 RVA: 0x00002594 File Offset: 0x00000794
		public ShadowCastingMode shadowCastingMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Terrain.get_shadowCastingMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Terrain.set_shadowCastingMode_Injected(intPtr, value);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000025B8 File Offset: 0x000007B8
		// (set) Token: 0x06000028 RID: 40 RVA: 0x000025DC File Offset: 0x000007DC
		public ReflectionProbeUsage reflectionProbeUsage
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Terrain.get_reflectionProbeUsage_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Terrain.set_reflectionProbeUsage_Injected(intPtr, value);
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002600 File Offset: 0x00000800
		public void GetClosestReflectionProbes(List<ReflectionProbeBlendInfo> result)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Terrain.GetClosestReflectionProbes_Injected(intPtr, result);
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002624 File Offset: 0x00000824
		// (set) Token: 0x0600002B RID: 43 RVA: 0x0000264C File Offset: 0x0000084C
		public Material materialTemplate
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Material>(Terrain.get_materialTemplate_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Terrain.set_materialTemplate_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Material>(value));
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002674 File Offset: 0x00000874
		// (set) Token: 0x0600002D RID: 45 RVA: 0x00002698 File Offset: 0x00000898
		public bool drawHeightmap
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Terrain.get_drawHeightmap_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Terrain.set_drawHeightmap_Injected(intPtr, value);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000026BC File Offset: 0x000008BC
		// (set) Token: 0x0600002F RID: 47 RVA: 0x000026E0 File Offset: 0x000008E0
		public bool allowAutoConnect
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Terrain.get_allowAutoConnect_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Terrain.set_allowAutoConnect_Injected(intPtr, value);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002704 File Offset: 0x00000904
		// (set) Token: 0x06000031 RID: 49 RVA: 0x00002728 File Offset: 0x00000928
		public int groupingID
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Terrain.get_groupingID_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Terrain.set_groupingID_Injected(intPtr, value);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000032 RID: 50 RVA: 0x0000274C File Offset: 0x0000094C
		// (set) Token: 0x06000033 RID: 51 RVA: 0x00002770 File Offset: 0x00000970
		public bool drawInstanced
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Terrain.get_drawInstanced_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Terrain.set_drawInstanced_Injected(intPtr, value);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002794 File Offset: 0x00000994
		// (set) Token: 0x06000035 RID: 53 RVA: 0x000027B8 File Offset: 0x000009B8
		public bool enableHeightmapRayTracing
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Terrain.get_enableHeightmapRayTracing_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Terrain.set_enableHeightmapRayTracing_Injected(intPtr, value);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000036 RID: 54 RVA: 0x000027DC File Offset: 0x000009DC
		// (set) Token: 0x06000037 RID: 55 RVA: 0x00002800 File Offset: 0x00000A00
		public bool enableHeightmapLODFrustumCulling
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Terrain.get_enableHeightmapLODFrustumCulling_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Terrain.set_enableHeightmapLODFrustumCulling_Injected(intPtr, value);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002824 File Offset: 0x00000A24
		public RenderTexture normalmapTexture
		{
			[NativeMethod("TryGetNormalMapTexture")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<RenderTexture>(Terrain.get_normalmapTexture_Injected(intPtr));
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000039 RID: 57 RVA: 0x0000284C File Offset: 0x00000A4C
		// (set) Token: 0x0600003A RID: 58 RVA: 0x00002870 File Offset: 0x00000A70
		public bool drawTreesAndFoliage
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Terrain.get_drawTreesAndFoliage_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Terrain.set_drawTreesAndFoliage_Injected(intPtr, value);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002894 File Offset: 0x00000A94
		// (set) Token: 0x0600003C RID: 60 RVA: 0x000028BC File Offset: 0x00000ABC
		public Vector3 patchBoundsMultiplier
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				Terrain.get_patchBoundsMultiplier_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Terrain.set_patchBoundsMultiplier_Injected(intPtr, ref value);
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000028E0 File Offset: 0x00000AE0
		public float SampleHeight(Vector3 worldPosition)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Terrain.SampleHeight_Injected(intPtr, ref worldPosition);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002904 File Offset: 0x00000B04
		public void AddTreeInstance(TreeInstance instance)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Terrain.AddTreeInstance_Injected(intPtr, ref instance);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002928 File Offset: 0x00000B28
		public void SetNeighbors(Terrain left, Terrain top, Terrain right, Terrain bottom)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Terrain.SetNeighbors_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Terrain>(left), Object.MarshalledUnityObject.Marshal<Terrain>(top), Object.MarshalledUnityObject.Marshal<Terrain>(right), Object.MarshalledUnityObject.Marshal<Terrain>(bottom));
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002964 File Offset: 0x00000B64
		// (set) Token: 0x06000041 RID: 65 RVA: 0x00002988 File Offset: 0x00000B88
		public float treeLODBiasMultiplier
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Terrain.get_treeLODBiasMultiplier_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Terrain.set_treeLODBiasMultiplier_Injected(intPtr, value);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000042 RID: 66 RVA: 0x000029AC File Offset: 0x00000BAC
		// (set) Token: 0x06000043 RID: 67 RVA: 0x000029D0 File Offset: 0x00000BD0
		public bool collectDetailPatches
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Terrain.get_collectDetailPatches_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Terrain.set_collectDetailPatches_Injected(intPtr, value);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000044 RID: 68 RVA: 0x000029F4 File Offset: 0x00000BF4
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00002A18 File Offset: 0x00000C18
		public bool ignoreQualitySettings
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Terrain.get_ignoreQualitySettings_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Terrain.set_ignoreQualitySettings_Injected(intPtr, value);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002A3C File Offset: 0x00000C3C
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00002A60 File Offset: 0x00000C60
		public TerrainRenderFlags editorRenderFlags
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Terrain.get_editorRenderFlags_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Terrain.set_editorRenderFlags_Injected(intPtr, value);
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002A84 File Offset: 0x00000C84
		public Vector3 GetPosition()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3 vector;
			Terrain.GetPosition_Injected(intPtr, out vector);
			return vector;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002AAC File Offset: 0x00000CAC
		public void Flush()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Terrain.Flush_Injected(intPtr);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002AD0 File Offset: 0x00000CD0
		internal void RemoveTrees(Vector2 position, float radius, int prototypeIndex)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Terrain.RemoveTrees_Injected(intPtr, ref position, radius, prototypeIndex);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002AF8 File Offset: 0x00000CF8
		[NativeMethod("CopySplatMaterialCustomProps")]
		public void SetSplatMaterialPropertyBlock(MaterialPropertyBlock properties)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Terrain.SetSplatMaterialPropertyBlock_Injected(intPtr, (properties == null) ? ((IntPtr)0) : MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(properties));
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002B2C File Offset: 0x00000D2C
		public void GetSplatMaterialPropertyBlock(MaterialPropertyBlock dest)
		{
			bool flag = dest == null;
			if (flag)
			{
				throw new ArgumentNullException("dest");
			}
			this.Internal_GetSplatMaterialPropertyBlock(dest);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002B58 File Offset: 0x00000D58
		[NativeMethod("GetSplatMaterialCustomProps")]
		private void Internal_GetSplatMaterialPropertyBlock(MaterialPropertyBlock dest)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Terrain.Internal_GetSplatMaterialPropertyBlock_Injected(intPtr, (dest == null) ? ((IntPtr)0) : MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(dest));
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002B8C File Offset: 0x00000D8C
		// (set) Token: 0x0600004F RID: 79 RVA: 0x00002BB0 File Offset: 0x00000DB0
		public TreeMotionVectorModeOverride treeMotionVectorModeOverride
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Terrain.get_treeMotionVectorModeOverride_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Terrain.set_treeMotionVectorModeOverride_Injected(intPtr, value);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002BD4 File Offset: 0x00000DD4
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00002BF8 File Offset: 0x00000DF8
		public bool preserveTreePrototypeLayers
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Terrain.get_preserveTreePrototypeLayers_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Terrain.set_preserveTreePrototypeLayers_Injected(intPtr, value);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000052 RID: 82
		[StaticAccessor("Terrain", StaticAccessorType.DoubleColon)]
		public static extern GraphicsFormat heightmapFormat
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002C1C File Offset: 0x00000E1C
		public static TextureFormat heightmapTextureFormat
		{
			get
			{
				return GraphicsFormatUtility.GetTextureFormat(Terrain.heightmapFormat);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002C38 File Offset: 0x00000E38
		public static RenderTextureFormat heightmapRenderTextureFormat
		{
			get
			{
				return GraphicsFormatUtility.GetRenderTextureFormat(Terrain.heightmapFormat);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000055 RID: 85
		[StaticAccessor("Terrain", StaticAccessorType.DoubleColon)]
		public static extern GraphicsFormat normalmapFormat
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002C54 File Offset: 0x00000E54
		public static TextureFormat normalmapTextureFormat
		{
			get
			{
				return GraphicsFormatUtility.GetTextureFormat(Terrain.normalmapFormat);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002C70 File Offset: 0x00000E70
		public static RenderTextureFormat normalmapRenderTextureFormat
		{
			get
			{
				return GraphicsFormatUtility.GetRenderTextureFormat(Terrain.normalmapFormat);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000058 RID: 88
		[StaticAccessor("Terrain", StaticAccessorType.DoubleColon)]
		public static extern GraphicsFormat holesFormat
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002C8C File Offset: 0x00000E8C
		public static RenderTextureFormat holesRenderTextureFormat
		{
			get
			{
				return GraphicsFormatUtility.GetRenderTextureFormat(Terrain.holesFormat);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600005A RID: 90
		[StaticAccessor("Terrain", StaticAccessorType.DoubleColon)]
		public static extern GraphicsFormat compressedHolesFormat
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002CA8 File Offset: 0x00000EA8
		public static TextureFormat compressedHolesTextureFormat
		{
			get
			{
				return GraphicsFormatUtility.GetTextureFormat(Terrain.compressedHolesFormat);
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002CC4 File Offset: 0x00000EC4
		public static Terrain activeTerrain
		{
			get
			{
				return Unmarshal.UnmarshalUnityObject<Terrain>(Terrain.get_activeTerrain_Injected());
			}
		}

		// Token: 0x0600005D RID: 93
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetConnectivityDirty();

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600005E RID: 94
		[NativeProperty("ActiveTerrainsScriptingArray")]
		public static extern Terrain[] activeTerrains
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[return: Unmarshalled]
			get;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002CDB File Offset: 0x00000EDB
		public static void GetActiveTerrains(List<Terrain> terrainList)
		{
			Terrain.Internal_FillActiveTerrainList(terrainList);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002CE8 File Offset: 0x00000EE8
		private static void Internal_FillActiveTerrainList([NotNull] object terrainList)
		{
			if (terrainList == null)
			{
				ThrowHelper.ThrowArgumentNullException(terrainList, "terrainList");
			}
			Terrain.Internal_FillActiveTerrainList_Injected(terrainList);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002D0C File Offset: 0x00000F0C
		[UsedByNativeCode]
		public static GameObject CreateTerrainGameObject(TerrainData assignTerrain)
		{
			return Unmarshal.UnmarshalUnityObject<GameObject>(Terrain.CreateTerrainGameObject_Injected(Object.MarshalledUnityObject.Marshal<TerrainData>(assignTerrain)));
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002D2C File Offset: 0x00000F2C
		public Terrain leftNeighbor
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Terrain>(Terrain.get_leftNeighbor_Injected(intPtr));
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002D54 File Offset: 0x00000F54
		public Terrain rightNeighbor
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Terrain>(Terrain.get_rightNeighbor_Injected(intPtr));
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002D7C File Offset: 0x00000F7C
		public Terrain topNeighbor
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Terrain>(Terrain.get_topNeighbor_Injected(intPtr));
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002DA4 File Offset: 0x00000FA4
		public Terrain bottomNeighbor
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Terrain>(Terrain.get_bottomNeighbor_Injected(intPtr));
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002DCC File Offset: 0x00000FCC
		// (set) Token: 0x06000067 RID: 103 RVA: 0x00002DF0 File Offset: 0x00000FF0
		public uint renderingLayerMask
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Terrain.get_renderingLayerMask_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Terrain>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Terrain.set_renderingLayerMask_Injected(intPtr, value);
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00002E14 File Offset: 0x00001014
		// (set) Token: 0x06000069 RID: 105 RVA: 0x00002E2C File Offset: 0x0000102C
		[Obsolete("splatmapDistance is deprecated, please use basemapDistance instead. (UnityUpgradable) -> basemapDistance", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float splatmapDistance
		{
			get
			{
				return this.basemapDistance;
			}
			set
			{
				this.basemapDistance = value;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00002E38 File Offset: 0x00001038
		// (set) Token: 0x0600006B RID: 107 RVA: 0x00002E53 File Offset: 0x00001053
		[Obsolete("castShadows is deprecated, please use shadowCastingMode instead.")]
		public bool castShadows
		{
			get
			{
				return this.shadowCastingMode > ShadowCastingMode.Off;
			}
			set
			{
				this.shadowCastingMode = (value ? ShadowCastingMode.TwoSided : ShadowCastingMode.Off);
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00002E64 File Offset: 0x00001064
		// (set) Token: 0x0600006D RID: 109 RVA: 0x00002E77 File Offset: 0x00001077
		[Obsolete("Property materialType is not used any more. Set materialTemplate directly.", false)]
		public Terrain.MaterialType materialType
		{
			get
			{
				return Terrain.MaterialType.Custom;
			}
			set
			{
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00002E7C File Offset: 0x0000107C
		// (set) Token: 0x0600006F RID: 111 RVA: 0x00002E77 File Offset: 0x00001077
		[Obsolete("Property legacySpecular is not used any more. Set materialTemplate directly.", false)]
		public Color legacySpecular
		{
			get
			{
				return Color.gray;
			}
			set
			{
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00002E94 File Offset: 0x00001094
		// (set) Token: 0x06000071 RID: 113 RVA: 0x00002E77 File Offset: 0x00001077
		[Obsolete("Property legacyShininess is not used any more. Set materialTemplate directly.", false)]
		public float legacyShininess
		{
			get
			{
				return 0.078125f;
			}
			set
			{
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002EAB File Offset: 0x000010AB
		[Obsolete("Use TerrainData.SyncHeightmap to notify all Terrain instances using the TerrainData.", false)]
		public void ApplyDelayedHeightmapModification()
		{
			TerrainData terrainData = this.terrainData;
			if (terrainData != null)
			{
				terrainData.SyncHeightmap();
			}
		}

		// Token: 0x06000074 RID: 116
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_terrainData_Injected(IntPtr _unity_self);

		// Token: 0x06000075 RID: 117
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_terrainData_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x06000076 RID: 118
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_treeDistance_Injected(IntPtr _unity_self);

		// Token: 0x06000077 RID: 119
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_treeDistance_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000078 RID: 120
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_treeBillboardDistance_Injected(IntPtr _unity_self);

		// Token: 0x06000079 RID: 121
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_treeBillboardDistance_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600007A RID: 122
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_treeCrossFadeLength_Injected(IntPtr _unity_self);

		// Token: 0x0600007B RID: 123
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_treeCrossFadeLength_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600007C RID: 124
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_treeMaximumFullLODCount_Injected(IntPtr _unity_self);

		// Token: 0x0600007D RID: 125
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_treeMaximumFullLODCount_Injected(IntPtr _unity_self, int value);

		// Token: 0x0600007E RID: 126
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_detailObjectDistance_Injected(IntPtr _unity_self);

		// Token: 0x0600007F RID: 127
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_detailObjectDistance_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000080 RID: 128
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_detailObjectDensity_Injected(IntPtr _unity_self);

		// Token: 0x06000081 RID: 129
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_detailObjectDensity_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000082 RID: 130
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_heightmapPixelError_Injected(IntPtr _unity_self);

		// Token: 0x06000083 RID: 131
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_heightmapPixelError_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000084 RID: 132
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_heightmapMaximumLOD_Injected(IntPtr _unity_self);

		// Token: 0x06000085 RID: 133
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_heightmapMaximumLOD_Injected(IntPtr _unity_self, int value);

		// Token: 0x06000086 RID: 134
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_heightmapMinimumLODSimplification_Injected(IntPtr _unity_self);

		// Token: 0x06000087 RID: 135
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_heightmapMinimumLODSimplification_Injected(IntPtr _unity_self, int value);

		// Token: 0x06000088 RID: 136
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_basemapDistance_Injected(IntPtr _unity_self);

		// Token: 0x06000089 RID: 137
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_basemapDistance_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600008A RID: 138
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_lightmapIndex_Injected(IntPtr _unity_self);

		// Token: 0x0600008B RID: 139
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_lightmapIndex_Injected(IntPtr _unity_self, int value);

		// Token: 0x0600008C RID: 140
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_realtimeLightmapIndex_Injected(IntPtr _unity_self);

		// Token: 0x0600008D RID: 141
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_realtimeLightmapIndex_Injected(IntPtr _unity_self, int value);

		// Token: 0x0600008E RID: 142
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_lightmapScaleOffset_Injected(IntPtr _unity_self, out Vector4 ret);

		// Token: 0x0600008F RID: 143
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_lightmapScaleOffset_Injected(IntPtr _unity_self, [In] ref Vector4 value);

		// Token: 0x06000090 RID: 144
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_realtimeLightmapScaleOffset_Injected(IntPtr _unity_self, out Vector4 ret);

		// Token: 0x06000091 RID: 145
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_realtimeLightmapScaleOffset_Injected(IntPtr _unity_self, [In] ref Vector4 value);

		// Token: 0x06000092 RID: 146
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_freeUnusedRenderingResources_Injected(IntPtr _unity_self);

		// Token: 0x06000093 RID: 147
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_freeUnusedRenderingResources_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000094 RID: 148
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_keepUnusedRenderingResources_Injected(IntPtr _unity_self);

		// Token: 0x06000095 RID: 149
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_keepUnusedRenderingResources_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000096 RID: 150
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetKeepUnusedCameraRenderingResources_Injected(IntPtr _unity_self, int cameraInstanceID);

		// Token: 0x06000097 RID: 151
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetKeepUnusedCameraRenderingResources_Injected(IntPtr _unity_self, int cameraInstanceID, bool keepUnused);

		// Token: 0x06000098 RID: 152
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ShadowCastingMode get_shadowCastingMode_Injected(IntPtr _unity_self);

		// Token: 0x06000099 RID: 153
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_shadowCastingMode_Injected(IntPtr _unity_self, ShadowCastingMode value);

		// Token: 0x0600009A RID: 154
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ReflectionProbeUsage get_reflectionProbeUsage_Injected(IntPtr _unity_self);

		// Token: 0x0600009B RID: 155
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_reflectionProbeUsage_Injected(IntPtr _unity_self, ReflectionProbeUsage value);

		// Token: 0x0600009C RID: 156
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetClosestReflectionProbes_Injected(IntPtr _unity_self, List<ReflectionProbeBlendInfo> result);

		// Token: 0x0600009D RID: 157
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_materialTemplate_Injected(IntPtr _unity_self);

		// Token: 0x0600009E RID: 158
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_materialTemplate_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x0600009F RID: 159
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_drawHeightmap_Injected(IntPtr _unity_self);

		// Token: 0x060000A0 RID: 160
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_drawHeightmap_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060000A1 RID: 161
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_allowAutoConnect_Injected(IntPtr _unity_self);

		// Token: 0x060000A2 RID: 162
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_allowAutoConnect_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060000A3 RID: 163
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_groupingID_Injected(IntPtr _unity_self);

		// Token: 0x060000A4 RID: 164
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_groupingID_Injected(IntPtr _unity_self, int value);

		// Token: 0x060000A5 RID: 165
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_drawInstanced_Injected(IntPtr _unity_self);

		// Token: 0x060000A6 RID: 166
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_drawInstanced_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060000A7 RID: 167
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_enableHeightmapRayTracing_Injected(IntPtr _unity_self);

		// Token: 0x060000A8 RID: 168
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_enableHeightmapRayTracing_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060000A9 RID: 169
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_enableHeightmapLODFrustumCulling_Injected(IntPtr _unity_self);

		// Token: 0x060000AA RID: 170
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_enableHeightmapLODFrustumCulling_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060000AB RID: 171
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_normalmapTexture_Injected(IntPtr _unity_self);

		// Token: 0x060000AC RID: 172
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_drawTreesAndFoliage_Injected(IntPtr _unity_self);

		// Token: 0x060000AD RID: 173
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_drawTreesAndFoliage_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060000AE RID: 174
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_patchBoundsMultiplier_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x060000AF RID: 175
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_patchBoundsMultiplier_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x060000B0 RID: 176
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float SampleHeight_Injected(IntPtr _unity_self, [In] ref Vector3 worldPosition);

		// Token: 0x060000B1 RID: 177
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddTreeInstance_Injected(IntPtr _unity_self, [In] ref TreeInstance instance);

		// Token: 0x060000B2 RID: 178
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetNeighbors_Injected(IntPtr _unity_self, IntPtr left, IntPtr top, IntPtr right, IntPtr bottom);

		// Token: 0x060000B3 RID: 179
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_treeLODBiasMultiplier_Injected(IntPtr _unity_self);

		// Token: 0x060000B4 RID: 180
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_treeLODBiasMultiplier_Injected(IntPtr _unity_self, float value);

		// Token: 0x060000B5 RID: 181
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_collectDetailPatches_Injected(IntPtr _unity_self);

		// Token: 0x060000B6 RID: 182
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_collectDetailPatches_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060000B7 RID: 183
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_ignoreQualitySettings_Injected(IntPtr _unity_self);

		// Token: 0x060000B8 RID: 184
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_ignoreQualitySettings_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060000B9 RID: 185
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern TerrainRenderFlags get_editorRenderFlags_Injected(IntPtr _unity_self);

		// Token: 0x060000BA RID: 186
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_editorRenderFlags_Injected(IntPtr _unity_self, TerrainRenderFlags value);

		// Token: 0x060000BB RID: 187
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPosition_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x060000BC RID: 188
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Flush_Injected(IntPtr _unity_self);

		// Token: 0x060000BD RID: 189
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RemoveTrees_Injected(IntPtr _unity_self, [In] ref Vector2 position, float radius, int prototypeIndex);

		// Token: 0x060000BE RID: 190
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetSplatMaterialPropertyBlock_Injected(IntPtr _unity_self, IntPtr properties);

		// Token: 0x060000BF RID: 191
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_GetSplatMaterialPropertyBlock_Injected(IntPtr _unity_self, IntPtr dest);

		// Token: 0x060000C0 RID: 192
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern TreeMotionVectorModeOverride get_treeMotionVectorModeOverride_Injected(IntPtr _unity_self);

		// Token: 0x060000C1 RID: 193
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_treeMotionVectorModeOverride_Injected(IntPtr _unity_self, TreeMotionVectorModeOverride value);

		// Token: 0x060000C2 RID: 194
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_preserveTreePrototypeLayers_Injected(IntPtr _unity_self);

		// Token: 0x060000C3 RID: 195
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_preserveTreePrototypeLayers_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060000C4 RID: 196
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_activeTerrain_Injected();

		// Token: 0x060000C5 RID: 197
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_FillActiveTerrainList_Injected(object terrainList);

		// Token: 0x060000C6 RID: 198
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr CreateTerrainGameObject_Injected(IntPtr assignTerrain);

		// Token: 0x060000C7 RID: 199
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_leftNeighbor_Injected(IntPtr _unity_self);

		// Token: 0x060000C8 RID: 200
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_rightNeighbor_Injected(IntPtr _unity_self);

		// Token: 0x060000C9 RID: 201
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_topNeighbor_Injected(IntPtr _unity_self);

		// Token: 0x060000CA RID: 202
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_bottomNeighbor_Injected(IntPtr _unity_self);

		// Token: 0x060000CB RID: 203
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint get_renderingLayerMask_Injected(IntPtr _unity_self);

		// Token: 0x060000CC RID: 204
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_renderingLayerMask_Injected(IntPtr _unity_self, uint value);

		// Token: 0x02000005 RID: 5
		[Obsolete("Enum type MaterialType is not used any more.", false)]
		public enum MaterialType
		{
			// Token: 0x04000015 RID: 21
			BuiltInStandard,
			// Token: 0x04000016 RID: 22
			BuiltInLegacyDiffuse,
			// Token: 0x04000017 RID: 23
			BuiltInLegacySpecular,
			// Token: 0x04000018 RID: 24
			Custom
		}
	}
}
