using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000002 RID: 2
	[NativeHeader("Modules/Terrain/Public/TerrainData.h")]
	[NativeHeader("Modules/TerrainPhysics/TerrainCollider.h")]
	public class TerrainCollider : Collider
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002078 File Offset: 0x00000278
		public TerrainData terrainData
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<TerrainData>(TerrainCollider.get_terrainData_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TerrainCollider.set_terrainData_Injected(intPtr, Object.MarshalledUnityObject.Marshal<TerrainData>(value));
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020A0 File Offset: 0x000002A0
		private RaycastHit Raycast(Ray ray, float maxDistance, bool hitHoles, ref bool hasHit)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainCollider>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			RaycastHit raycastHit;
			TerrainCollider.Raycast_Injected(intPtr, ref ray, maxDistance, hitHoles, ref hasHit, out raycastHit);
			return raycastHit;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020CC File Offset: 0x000002CC
		internal bool Raycast(Ray ray, out RaycastHit hitInfo, float maxDistance, bool hitHoles)
		{
			bool flag = false;
			hitInfo = this.Raycast(ray, maxDistance, hitHoles, ref flag);
			return flag;
		}

		// Token: 0x06000006 RID: 6
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_terrainData_Injected(IntPtr _unity_self);

		// Token: 0x06000007 RID: 7
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_terrainData_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x06000008 RID: 8
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Raycast_Injected(IntPtr _unity_self, [In] ref Ray ray, float maxDistance, bool hitHoles, ref bool hasHit, out RaycastHit ret);
	}
}
