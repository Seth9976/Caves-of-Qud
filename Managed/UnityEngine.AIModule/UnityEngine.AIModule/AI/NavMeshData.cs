using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine.AI
{
	// Token: 0x02000012 RID: 18
	[NativeHeader("Modules/AI/NavMesh/NavMesh.bindings.h")]
	public sealed class NavMeshData : Object
	{
		// Token: 0x06000156 RID: 342 RVA: 0x00003BC2 File Offset: 0x00001DC2
		public NavMeshData()
		{
			NavMeshData.Internal_Create(this, 0);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00003BD4 File Offset: 0x00001DD4
		public NavMeshData(int agentTypeID)
		{
			NavMeshData.Internal_Create(this, agentTypeID);
		}

		// Token: 0x06000158 RID: 344
		[StaticAccessor("NavMeshDataBindings", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create([Writable] NavMeshData mono, int agentTypeID);

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public Bounds sourceBounds
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshData>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Bounds bounds;
				NavMeshData.get_sourceBounds_Injected(intPtr, out bounds);
				return bounds;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00003C10 File Offset: 0x00001E10
		// (set) Token: 0x0600015B RID: 347 RVA: 0x00003C38 File Offset: 0x00001E38
		public Vector3 position
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshData>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				NavMeshData.get_position_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshData>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				NavMeshData.set_position_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00003C5C File Offset: 0x00001E5C
		// (set) Token: 0x0600015D RID: 349 RVA: 0x00003C84 File Offset: 0x00001E84
		public Quaternion rotation
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshData>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Quaternion quaternion;
				NavMeshData.get_rotation_Injected(intPtr, out quaternion);
				return quaternion;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshData>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				NavMeshData.set_rotation_Injected(intPtr, ref value);
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00003CA8 File Offset: 0x00001EA8
		internal bool hasHeightMeshData
		{
			[NativeMethod("HasHeightMeshData")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshData>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return NavMeshData.get_hasHeightMeshData_Injected(intPtr);
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00003CCC File Offset: 0x00001ECC
		internal NavMeshBuildSettings buildSettings
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshData>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				NavMeshBuildSettings navMeshBuildSettings;
				NavMeshData.get_buildSettings_Injected(intPtr, out navMeshBuildSettings);
				return navMeshBuildSettings;
			}
		}

		// Token: 0x06000160 RID: 352
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_sourceBounds_Injected(IntPtr _unity_self, out Bounds ret);

		// Token: 0x06000161 RID: 353
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_position_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x06000162 RID: 354
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_position_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x06000163 RID: 355
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_rotation_Injected(IntPtr _unity_self, out Quaternion ret);

		// Token: 0x06000164 RID: 356
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_rotation_Injected(IntPtr _unity_self, [In] ref Quaternion value);

		// Token: 0x06000165 RID: 357
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_hasHeightMeshData_Injected(IntPtr _unity_self);

		// Token: 0x06000166 RID: 358
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_buildSettings_Injected(IntPtr _unity_self, out NavMeshBuildSettings ret);
	}
}
