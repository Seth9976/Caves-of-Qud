using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.AI
{
	// Token: 0x02000017 RID: 23
	[NativeHeader("Modules/AI/NavMeshManager.h")]
	[MovedFrom("UnityEngine")]
	[StaticAccessor("NavMeshBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Modules/AI/NavMesh/NavMesh.bindings.h")]
	public static class NavMesh
	{
		// Token: 0x06000189 RID: 393 RVA: 0x00003F5A File Offset: 0x0000215A
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void ClearPreUpdateListeners()
		{
			NavMesh.onPreUpdate = null;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00003F64 File Offset: 0x00002164
		[RequiredByNativeCode]
		private static void Internal_CallOnNavMeshPreUpdate()
		{
			bool flag = NavMesh.onPreUpdate != null;
			if (flag)
			{
				NavMesh.onPreUpdate();
			}
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00003F8C File Offset: 0x0000218C
		public static bool Raycast(Vector3 sourcePosition, Vector3 targetPosition, out NavMeshHit hit, int areaMask)
		{
			return NavMesh.Raycast_Injected(ref sourcePosition, ref targetPosition, out hit, areaMask);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00003FA4 File Offset: 0x000021A4
		public static bool CalculatePath(Vector3 sourcePosition, Vector3 targetPosition, int areaMask, NavMeshPath path)
		{
			path.ClearCorners();
			return NavMesh.CalculatePathInternal(sourcePosition, targetPosition, areaMask, path);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00003FC8 File Offset: 0x000021C8
		private static bool CalculatePathInternal(Vector3 sourcePosition, Vector3 targetPosition, int areaMask, NavMeshPath path)
		{
			return NavMesh.CalculatePathInternal_Injected(ref sourcePosition, ref targetPosition, areaMask, (path == null) ? ((IntPtr)0) : NavMeshPath.BindingsMarshaller.ConvertToNative(path));
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00003FF0 File Offset: 0x000021F0
		public static bool FindClosestEdge(Vector3 sourcePosition, out NavMeshHit hit, int areaMask)
		{
			return NavMesh.FindClosestEdge_Injected(ref sourcePosition, out hit, areaMask);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00004008 File Offset: 0x00002208
		public static bool SamplePosition(Vector3 sourcePosition, out NavMeshHit hit, float maxDistance, int areaMask)
		{
			return NavMesh.SamplePosition_Injected(ref sourcePosition, out hit, maxDistance, areaMask);
		}

		// Token: 0x06000190 RID: 400
		[Obsolete("Use SetAreaCost instead.")]
		[StaticAccessor("GetNavMeshProjectSettings()")]
		[NativeName("SetAreaCost")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetLayerCost(int layer, float cost);

		// Token: 0x06000191 RID: 401
		[Obsolete("Use GetAreaCost instead.")]
		[StaticAccessor("GetNavMeshProjectSettings()")]
		[NativeName("GetAreaCost")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GetLayerCost(int layer);

		// Token: 0x06000192 RID: 402 RVA: 0x00004020 File Offset: 0x00002220
		[Obsolete("Use GetAreaFromName instead.")]
		[NativeName("GetAreaFromName")]
		[StaticAccessor("GetNavMeshProjectSettings()")]
		public unsafe static int GetNavMeshLayerFromName(string layerName)
		{
			int navMeshLayerFromName_Injected;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(layerName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = layerName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				navMeshLayerFromName_Injected = NavMesh.GetNavMeshLayerFromName_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return navMeshLayerFromName_Injected;
		}

		// Token: 0x06000193 RID: 403
		[StaticAccessor("GetNavMeshProjectSettings()")]
		[NativeName("SetAreaCost")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetAreaCost(int areaIndex, float cost);

		// Token: 0x06000194 RID: 404
		[StaticAccessor("GetNavMeshProjectSettings()")]
		[NativeName("GetAreaCost")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GetAreaCost(int areaIndex);

		// Token: 0x06000195 RID: 405 RVA: 0x00004078 File Offset: 0x00002278
		[StaticAccessor("GetNavMeshProjectSettings()")]
		[NativeName("GetAreaFromName")]
		public unsafe static int GetAreaFromName(string areaName)
		{
			int areaFromName_Injected;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(areaName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = areaName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				areaFromName_Injected = NavMesh.GetAreaFromName_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return areaFromName_Injected;
		}

		// Token: 0x06000196 RID: 406
		[StaticAccessor("GetNavMeshProjectSettings()")]
		[NativeName("GetAreaNames")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string[] GetAreaNames();

		// Token: 0x06000197 RID: 407 RVA: 0x000040D0 File Offset: 0x000022D0
		public static NavMeshTriangulation CalculateTriangulation()
		{
			NavMeshTriangulation navMeshTriangulation;
			NavMesh.CalculateTriangulation_Injected(out navMeshTriangulation);
			return navMeshTriangulation;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x000040E8 File Offset: 0x000022E8
		[Obsolete("use NavMesh.CalculateTriangulation() instead.")]
		public static void Triangulate(out Vector3[] vertices, out int[] indices)
		{
			NavMeshTriangulation navMeshTriangulation = NavMesh.CalculateTriangulation();
			vertices = navMeshTriangulation.vertices;
			indices = navMeshTriangulation.indices;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000410C File Offset: 0x0000230C
		[Obsolete("AddOffMeshLinks has no effect and is deprecated.")]
		public static void AddOffMeshLinks()
		{
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000410C File Offset: 0x0000230C
		[Obsolete("RestoreNavMesh has no effect and is deprecated.")]
		public static void RestoreNavMesh()
		{
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600019B RID: 411
		// (set) Token: 0x0600019C RID: 412
		[StaticAccessor("GetNavMeshManager()")]
		public static extern float avoidancePredictionTime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600019D RID: 413
		// (set) Token: 0x0600019E RID: 414
		[StaticAccessor("GetNavMeshManager()")]
		public static extern int pathfindingIterationsPerFrame
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00004110 File Offset: 0x00002310
		public static NavMeshDataInstance AddNavMeshData(NavMeshData navMeshData)
		{
			bool flag = navMeshData == null;
			if (flag)
			{
				throw new ArgumentNullException("navMeshData");
			}
			return new NavMeshDataInstance
			{
				id = NavMesh.AddNavMeshDataInternal(navMeshData)
			};
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00004150 File Offset: 0x00002350
		public static NavMeshDataInstance AddNavMeshData(NavMeshData navMeshData, Vector3 position, Quaternion rotation)
		{
			bool flag = navMeshData == null;
			if (flag)
			{
				throw new ArgumentNullException("navMeshData");
			}
			return new NavMeshDataInstance
			{
				id = NavMesh.AddNavMeshDataTransformedInternal(navMeshData, position, rotation)
			};
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00004191 File Offset: 0x00002391
		public static void RemoveNavMeshData(NavMeshDataInstance handle)
		{
			NavMesh.RemoveNavMeshDataInternal(handle.id);
		}

		// Token: 0x060001A2 RID: 418
		[StaticAccessor("GetNavMeshManager()")]
		[NativeName("IsValidSurfaceID")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsValidNavMeshDataHandle(int handle);

		// Token: 0x060001A3 RID: 419
		[StaticAccessor("GetNavMeshManager()")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsValidLinkHandle(int handle);

		// Token: 0x060001A4 RID: 420 RVA: 0x000041A4 File Offset: 0x000023A4
		internal static Object InternalGetOwner(int dataID)
		{
			return Unmarshal.UnmarshalUnityObject<Object>(NavMesh.InternalGetOwner_Injected(dataID));
		}

		// Token: 0x060001A5 RID: 421
		[StaticAccessor("GetNavMeshManager()")]
		[NativeName("SetSurfaceUserID")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool InternalSetOwner(int dataID, int ownerID);

		// Token: 0x060001A6 RID: 422 RVA: 0x000041BC File Offset: 0x000023BC
		internal static Object InternalGetLinkOwner(int linkID)
		{
			return Unmarshal.UnmarshalUnityObject<Object>(NavMesh.InternalGetLinkOwner_Injected(linkID));
		}

		// Token: 0x060001A7 RID: 423
		[StaticAccessor("GetNavMeshManager()")]
		[NativeName("SetLinkUserID")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool InternalSetLinkOwner(int linkID, int ownerID);

		// Token: 0x060001A8 RID: 424 RVA: 0x000041D4 File Offset: 0x000023D4
		[NativeName("LoadData")]
		[StaticAccessor("GetNavMeshManager()")]
		internal static int AddNavMeshDataInternal(NavMeshData navMeshData)
		{
			return NavMesh.AddNavMeshDataInternal_Injected(Object.MarshalledUnityObject.Marshal<NavMeshData>(navMeshData));
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x000041EC File Offset: 0x000023EC
		[StaticAccessor("GetNavMeshManager()")]
		[NativeName("LoadData")]
		internal static int AddNavMeshDataTransformedInternal(NavMeshData navMeshData, Vector3 position, Quaternion rotation)
		{
			return NavMesh.AddNavMeshDataTransformedInternal_Injected(Object.MarshalledUnityObject.Marshal<NavMeshData>(navMeshData), ref position, ref rotation);
		}

		// Token: 0x060001AA RID: 426
		[StaticAccessor("GetNavMeshManager()")]
		[NativeName("UnloadData")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void RemoveNavMeshDataInternal(int handle);

		// Token: 0x060001AB RID: 427 RVA: 0x00004208 File Offset: 0x00002408
		public static NavMeshLinkInstance AddLink(NavMeshLinkData link)
		{
			return new NavMeshLinkInstance
			{
				id = NavMesh.AddLinkInternal(link, Vector3.zero, Quaternion.identity)
			};
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000423C File Offset: 0x0000243C
		public static NavMeshLinkInstance AddLink(NavMeshLinkData link, Vector3 position, Quaternion rotation)
		{
			return new NavMeshLinkInstance
			{
				id = NavMesh.AddLinkInternal(link, position, rotation)
			};
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00004267 File Offset: 0x00002467
		public static void RemoveLink(NavMeshLinkInstance handle)
		{
			NavMesh.RemoveLinkInternal(handle.id);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00004278 File Offset: 0x00002478
		public static bool IsLinkActive(NavMeshLinkInstance handle)
		{
			return NavMesh.IsOffMeshConnectionActive(handle.id);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00004296 File Offset: 0x00002496
		public static void SetLinkActive(NavMeshLinkInstance handle, bool value)
		{
			NavMesh.SetOffMeshConnectionActive(handle.id, value);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x000042A8 File Offset: 0x000024A8
		public static bool IsLinkOccupied(NavMeshLinkInstance handle)
		{
			return NavMesh.IsOffMeshConnectionOccupied(handle.id);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x000042C8 File Offset: 0x000024C8
		public static bool IsLinkValid(NavMeshLinkInstance handle)
		{
			return NavMesh.IsValidLinkHandle(handle.id);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x000042E8 File Offset: 0x000024E8
		public static Object GetLinkOwner(NavMeshLinkInstance handle)
		{
			return NavMesh.InternalGetLinkOwner(handle.id);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00004308 File Offset: 0x00002508
		public static void SetLinkOwner(NavMeshLinkInstance handle, Object owner)
		{
			int num = ((owner != null) ? owner.GetInstanceID() : 0);
			bool flag = !NavMesh.InternalSetLinkOwner(handle.id, num);
			if (flag)
			{
				Debug.LogError("Cannot set 'owner' on an invalid NavMeshLinkInstance");
			}
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00004348 File Offset: 0x00002548
		[StaticAccessor("GetNavMeshManager()")]
		[NativeName("AddLink")]
		internal static int AddLinkInternal(NavMeshLinkData link, Vector3 position, Quaternion rotation)
		{
			return NavMesh.AddLinkInternal_Injected(ref link, ref position, ref rotation);
		}

		// Token: 0x060001B5 RID: 437
		[StaticAccessor("GetNavMeshManager()")]
		[NativeName("RemoveLink")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void RemoveLinkInternal(int handle);

		// Token: 0x060001B6 RID: 438
		[StaticAccessor("GetNavMeshManager()")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsOffMeshConnectionOccupied(int handle);

		// Token: 0x060001B7 RID: 439
		[StaticAccessor("GetNavMeshManager()")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsOffMeshConnectionActive(int linkHandle);

		// Token: 0x060001B8 RID: 440
		[StaticAccessor("GetNavMeshManager()")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetOffMeshConnectionActive(int linkHandle, bool activated);

		// Token: 0x060001B9 RID: 441 RVA: 0x00004360 File Offset: 0x00002560
		public static bool SamplePosition(Vector3 sourcePosition, out NavMeshHit hit, float maxDistance, NavMeshQueryFilter filter)
		{
			return NavMesh.SamplePositionFilter(sourcePosition, out hit, maxDistance, filter.agentTypeID, filter.areaMask);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00004388 File Offset: 0x00002588
		private static bool SamplePositionFilter(Vector3 sourcePosition, out NavMeshHit hit, float maxDistance, int type, int mask)
		{
			return NavMesh.SamplePositionFilter_Injected(ref sourcePosition, out hit, maxDistance, type, mask);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x000043A4 File Offset: 0x000025A4
		public static bool FindClosestEdge(Vector3 sourcePosition, out NavMeshHit hit, NavMeshQueryFilter filter)
		{
			return NavMesh.FindClosestEdgeFilter(sourcePosition, out hit, filter.agentTypeID, filter.areaMask);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x000043CC File Offset: 0x000025CC
		private static bool FindClosestEdgeFilter(Vector3 sourcePosition, out NavMeshHit hit, int type, int mask)
		{
			return NavMesh.FindClosestEdgeFilter_Injected(ref sourcePosition, out hit, type, mask);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x000043E4 File Offset: 0x000025E4
		public static bool Raycast(Vector3 sourcePosition, Vector3 targetPosition, out NavMeshHit hit, NavMeshQueryFilter filter)
		{
			return NavMesh.RaycastFilter(sourcePosition, targetPosition, out hit, filter.agentTypeID, filter.areaMask);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000440C File Offset: 0x0000260C
		private static bool RaycastFilter(Vector3 sourcePosition, Vector3 targetPosition, out NavMeshHit hit, int type, int mask)
		{
			return NavMesh.RaycastFilter_Injected(ref sourcePosition, ref targetPosition, out hit, type, mask);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00004428 File Offset: 0x00002628
		public static bool CalculatePath(Vector3 sourcePosition, Vector3 targetPosition, NavMeshQueryFilter filter, NavMeshPath path)
		{
			path.ClearCorners();
			return NavMesh.CalculatePathFilterInternal(sourcePosition, targetPosition, path, filter.agentTypeID, filter.areaMask, filter.costs);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00004460 File Offset: 0x00002660
		private unsafe static bool CalculatePathFilterInternal(Vector3 sourcePosition, Vector3 targetPosition, NavMeshPath path, int type, int mask, float[] costs)
		{
			IntPtr intPtr = ((path == null) ? ((IntPtr)0) : NavMeshPath.BindingsMarshaller.ConvertToNative(path));
			Span<float> span = new Span<float>(costs);
			bool flag;
			fixed (float* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				flag = NavMesh.CalculatePathFilterInternal_Injected(ref sourcePosition, ref targetPosition, intPtr, type, mask, ref managedSpanWrapper);
			}
			return flag;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x000044B0 File Offset: 0x000026B0
		[StaticAccessor("GetNavMeshProjectSettings()")]
		public static NavMeshBuildSettings CreateSettings()
		{
			NavMeshBuildSettings navMeshBuildSettings;
			NavMesh.CreateSettings_Injected(out navMeshBuildSettings);
			return navMeshBuildSettings;
		}

		// Token: 0x060001C2 RID: 450
		[StaticAccessor("GetNavMeshProjectSettings()")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void RemoveSettings(int agentTypeID);

		// Token: 0x060001C3 RID: 451 RVA: 0x000044C8 File Offset: 0x000026C8
		public static NavMeshBuildSettings GetSettingsByID(int agentTypeID)
		{
			NavMeshBuildSettings navMeshBuildSettings;
			NavMesh.GetSettingsByID_Injected(agentTypeID, out navMeshBuildSettings);
			return navMeshBuildSettings;
		}

		// Token: 0x060001C4 RID: 452
		[StaticAccessor("GetNavMeshProjectSettings()")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetSettingsCount();

		// Token: 0x060001C5 RID: 453 RVA: 0x000044E0 File Offset: 0x000026E0
		public static NavMeshBuildSettings GetSettingsByIndex(int index)
		{
			NavMeshBuildSettings navMeshBuildSettings;
			NavMesh.GetSettingsByIndex_Injected(index, out navMeshBuildSettings);
			return navMeshBuildSettings;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x000044F8 File Offset: 0x000026F8
		public static string GetSettingsNameFromID(int agentTypeID)
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				NavMesh.GetSettingsNameFromID_Injected(agentTypeID, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x060001C7 RID: 455
		[StaticAccessor("GetNavMeshManager()")]
		[NativeName("CleanupAfterCarving")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void RemoveAllNavMeshData();

		// Token: 0x060001C8 RID: 456
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Raycast_Injected([In] ref Vector3 sourcePosition, [In] ref Vector3 targetPosition, out NavMeshHit hit, int areaMask);

		// Token: 0x060001C9 RID: 457
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CalculatePathInternal_Injected([In] ref Vector3 sourcePosition, [In] ref Vector3 targetPosition, int areaMask, IntPtr path);

		// Token: 0x060001CA RID: 458
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool FindClosestEdge_Injected([In] ref Vector3 sourcePosition, out NavMeshHit hit, int areaMask);

		// Token: 0x060001CB RID: 459
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SamplePosition_Injected([In] ref Vector3 sourcePosition, out NavMeshHit hit, float maxDistance, int areaMask);

		// Token: 0x060001CC RID: 460
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetNavMeshLayerFromName_Injected(ref ManagedSpanWrapper layerName);

		// Token: 0x060001CD RID: 461
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetAreaFromName_Injected(ref ManagedSpanWrapper areaName);

		// Token: 0x060001CE RID: 462
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CalculateTriangulation_Injected(out NavMeshTriangulation ret);

		// Token: 0x060001CF RID: 463
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr InternalGetOwner_Injected(int dataID);

		// Token: 0x060001D0 RID: 464
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr InternalGetLinkOwner_Injected(int linkID);

		// Token: 0x060001D1 RID: 465
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int AddNavMeshDataInternal_Injected(IntPtr navMeshData);

		// Token: 0x060001D2 RID: 466
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int AddNavMeshDataTransformedInternal_Injected(IntPtr navMeshData, [In] ref Vector3 position, [In] ref Quaternion rotation);

		// Token: 0x060001D3 RID: 467
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int AddLinkInternal_Injected([In] ref NavMeshLinkData link, [In] ref Vector3 position, [In] ref Quaternion rotation);

		// Token: 0x060001D4 RID: 468
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SamplePositionFilter_Injected([In] ref Vector3 sourcePosition, out NavMeshHit hit, float maxDistance, int type, int mask);

		// Token: 0x060001D5 RID: 469
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool FindClosestEdgeFilter_Injected([In] ref Vector3 sourcePosition, out NavMeshHit hit, int type, int mask);

		// Token: 0x060001D6 RID: 470
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool RaycastFilter_Injected([In] ref Vector3 sourcePosition, [In] ref Vector3 targetPosition, out NavMeshHit hit, int type, int mask);

		// Token: 0x060001D7 RID: 471
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CalculatePathFilterInternal_Injected([In] ref Vector3 sourcePosition, [In] ref Vector3 targetPosition, IntPtr path, int type, int mask, ref ManagedSpanWrapper costs);

		// Token: 0x060001D8 RID: 472
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CreateSettings_Injected(out NavMeshBuildSettings ret);

		// Token: 0x060001D9 RID: 473
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSettingsByID_Injected(int agentTypeID, out NavMeshBuildSettings ret);

		// Token: 0x060001DA RID: 474
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSettingsByIndex_Injected(int index, out NavMeshBuildSettings ret);

		// Token: 0x060001DB RID: 475
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSettingsNameFromID_Injected(int agentTypeID, out ManagedSpanWrapper ret);

		// Token: 0x0400003D RID: 61
		public const int AllAreas = -1;

		// Token: 0x0400003E RID: 62
		public static NavMesh.OnNavMeshPreUpdate onPreUpdate;

		// Token: 0x02000018 RID: 24
		// (Invoke) Token: 0x060001DD RID: 477
		public delegate void OnNavMeshPreUpdate();
	}
}
