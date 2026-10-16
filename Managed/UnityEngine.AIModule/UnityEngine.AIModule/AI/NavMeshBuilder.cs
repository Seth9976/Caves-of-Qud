using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine.AI
{
	// Token: 0x02000008 RID: 8
	[NativeHeader("Modules/AI/Builder/NavMeshBuilder.bindings.h")]
	[StaticAccessor("NavMeshBuilderBindings", StaticAccessorType.DoubleColon)]
	public static class NavMeshBuilder
	{
		// Token: 0x06000047 RID: 71 RVA: 0x0000271C File Offset: 0x0000091C
		public static void CollectSources(Bounds includedWorldBounds, int includedLayerMask, NavMeshCollectGeometry geometry, int defaultArea, bool generateLinksByDefault, List<NavMeshBuildMarkup> markups, bool includeOnlyMarkedObjects, List<NavMeshBuildSource> results)
		{
			bool flag = markups == null;
			if (flag)
			{
				throw new ArgumentNullException("markups");
			}
			bool flag2 = results == null;
			if (flag2)
			{
				throw new ArgumentNullException("results");
			}
			includedWorldBounds.extents = Vector3.Max(includedWorldBounds.extents, 0.001f * Vector3.one);
			NavMeshBuildSource[] array = NavMeshBuilder.CollectSourcesInternal(includedLayerMask, includedWorldBounds, null, true, geometry, defaultArea, generateLinksByDefault, markups.ToArray(), includeOnlyMarkedObjects);
			results.Clear();
			results.AddRange(array);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x0000279D File Offset: 0x0000099D
		public static void CollectSources(Bounds includedWorldBounds, int includedLayerMask, NavMeshCollectGeometry geometry, int defaultArea, List<NavMeshBuildMarkup> markups, List<NavMeshBuildSource> results)
		{
			NavMeshBuilder.CollectSources(includedWorldBounds, includedLayerMask, geometry, defaultArea, false, markups, false, results);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000027B0 File Offset: 0x000009B0
		public static void CollectSources(Transform root, int includedLayerMask, NavMeshCollectGeometry geometry, int defaultArea, bool generateLinksByDefault, List<NavMeshBuildMarkup> markups, bool includeOnlyMarkedObjects, List<NavMeshBuildSource> results)
		{
			bool flag = markups == null;
			if (flag)
			{
				throw new ArgumentNullException("markups");
			}
			bool flag2 = results == null;
			if (flag2)
			{
				throw new ArgumentNullException("results");
			}
			NavMeshBuildSource[] array = NavMeshBuilder.CollectSourcesInternal(includedLayerMask, default(Bounds), root, false, geometry, defaultArea, generateLinksByDefault, markups.ToArray(), includeOnlyMarkedObjects);
			results.Clear();
			results.AddRange(array);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002816 File Offset: 0x00000A16
		public static void CollectSources(Transform root, int includedLayerMask, NavMeshCollectGeometry geometry, int defaultArea, List<NavMeshBuildMarkup> markups, List<NavMeshBuildSource> results)
		{
			NavMeshBuilder.CollectSources(root, includedLayerMask, geometry, defaultArea, false, markups, false, results);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000282C File Offset: 0x00000A2C
		private unsafe static NavMeshBuildSource[] CollectSourcesInternal(int includedLayerMask, Bounds includedWorldBounds, Transform root, bool useBounds, NavMeshCollectGeometry geometry, int defaultArea, bool generateLinksByDefault, NavMeshBuildMarkup[] markups, bool includeOnlyMarkedObjects)
		{
			NavMeshBuildSource[] array2;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.Marshal<Transform>(root);
				Span<NavMeshBuildMarkup> span = new Span<NavMeshBuildMarkup>(markups);
				fixed (NavMeshBuildMarkup* ptr = span.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, span.Length);
					BlittableArrayWrapper blittableArrayWrapper;
					NavMeshBuilder.CollectSourcesInternal_Injected(includedLayerMask, ref includedWorldBounds, intPtr, useBounds, geometry, defaultArea, generateLinksByDefault, ref managedSpanWrapper, includeOnlyMarkedObjects, out blittableArrayWrapper);
				}
			}
			finally
			{
				NavMeshBuildMarkup* ptr = null;
				BlittableArrayWrapper blittableArrayWrapper;
				NavMeshBuildSource[] array;
				blittableArrayWrapper.Unmarshal<NavMeshBuildSource>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000289C File Offset: 0x00000A9C
		public static NavMeshData BuildNavMeshData(NavMeshBuildSettings buildSettings, List<NavMeshBuildSource> sources, Bounds localBounds, Vector3 position, Quaternion rotation)
		{
			bool flag = sources == null;
			if (flag)
			{
				throw new ArgumentNullException("sources");
			}
			NavMeshData navMeshData = new NavMeshData(buildSettings.agentTypeID)
			{
				position = position,
				rotation = rotation
			};
			NavMeshBuilder.UpdateNavMeshDataListInternal(navMeshData, buildSettings, sources, localBounds);
			return navMeshData;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000028EC File Offset: 0x00000AEC
		public static bool UpdateNavMeshData(NavMeshData data, NavMeshBuildSettings buildSettings, List<NavMeshBuildSource> sources, Bounds localBounds)
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			bool flag2 = sources == null;
			if (flag2)
			{
				throw new ArgumentNullException("sources");
			}
			return NavMeshBuilder.UpdateNavMeshDataListInternal(data, buildSettings, sources, localBounds);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002930 File Offset: 0x00000B30
		private static bool UpdateNavMeshDataListInternal(NavMeshData data, NavMeshBuildSettings buildSettings, object sources, Bounds localBounds)
		{
			return NavMeshBuilder.UpdateNavMeshDataListInternal_Injected(Object.MarshalledUnityObject.Marshal<NavMeshData>(data), ref buildSettings, sources, ref localBounds);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002950 File Offset: 0x00000B50
		public static AsyncOperation UpdateNavMeshDataAsync(NavMeshData data, NavMeshBuildSettings buildSettings, List<NavMeshBuildSource> sources, Bounds localBounds)
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			bool flag2 = sources == null;
			if (flag2)
			{
				throw new ArgumentNullException("sources");
			}
			return NavMeshBuilder.UpdateNavMeshDataAsyncListInternal(data, buildSettings, sources, localBounds);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002994 File Offset: 0x00000B94
		[NativeHeader("Modules/AI/NavMeshManager.h")]
		[StaticAccessor("GetNavMeshManager().GetNavMeshBuildManager()", StaticAccessorType.Arrow)]
		[NativeMethod("Purge")]
		public static void Cancel(NavMeshData data)
		{
			NavMeshBuilder.Cancel_Injected(Object.MarshalledUnityObject.Marshal<NavMeshData>(data));
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000029AC File Offset: 0x00000BAC
		private static AsyncOperation UpdateNavMeshDataAsyncListInternal(NavMeshData data, NavMeshBuildSettings buildSettings, object sources, Bounds localBounds)
		{
			IntPtr intPtr = NavMeshBuilder.UpdateNavMeshDataAsyncListInternal_Injected(Object.MarshalledUnityObject.Marshal<NavMeshData>(data), ref buildSettings, sources, ref localBounds);
			return (intPtr == 0) ? null : AsyncOperation.BindingsMarshaller.ConvertToManaged(intPtr);
		}

		// Token: 0x06000052 RID: 82
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CollectSourcesInternal_Injected(int includedLayerMask, [In] ref Bounds includedWorldBounds, IntPtr root, bool useBounds, NavMeshCollectGeometry geometry, int defaultArea, bool generateLinksByDefault, ref ManagedSpanWrapper markups, bool includeOnlyMarkedObjects, out BlittableArrayWrapper ret);

		// Token: 0x06000053 RID: 83
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool UpdateNavMeshDataListInternal_Injected(IntPtr data, [In] ref NavMeshBuildSettings buildSettings, object sources, [In] ref Bounds localBounds);

		// Token: 0x06000054 RID: 84
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Cancel_Injected(IntPtr data);

		// Token: 0x06000055 RID: 85
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr UpdateNavMeshDataAsyncListInternal_Injected(IntPtr data, [In] ref NavMeshBuildSettings buildSettings, object sources, [In] ref Bounds localBounds);
	}
}
