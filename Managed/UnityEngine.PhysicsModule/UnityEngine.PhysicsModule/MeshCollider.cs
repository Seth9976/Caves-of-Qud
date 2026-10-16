using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200002E RID: 46
	[RequireComponent(typeof(Transform))]
	[NativeHeader("Runtime/Graphics/Mesh/Mesh.h")]
	[NativeHeader("Modules/Physics/MeshCollider.h")]
	public class MeshCollider : Collider
	{
		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x00008734 File Offset: 0x00006934
		// (set) Token: 0x0600044F RID: 1103 RVA: 0x0000875C File Offset: 0x0000695C
		public Mesh sharedMesh
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MeshCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Mesh>(MeshCollider.get_sharedMesh_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MeshCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				MeshCollider.set_sharedMesh_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Mesh>(value));
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x00008784 File Offset: 0x00006984
		// (set) Token: 0x06000451 RID: 1105 RVA: 0x000087A8 File Offset: 0x000069A8
		public bool convex
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MeshCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return MeshCollider.get_convex_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MeshCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				MeshCollider.set_convex_Injected(intPtr, value);
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x000087CC File Offset: 0x000069CC
		// (set) Token: 0x06000453 RID: 1107 RVA: 0x000087F0 File Offset: 0x000069F0
		public MeshColliderCookingOptions cookingOptions
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MeshCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return MeshCollider.get_cookingOptions_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MeshCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				MeshCollider.set_cookingOptions_Injected(intPtr, value);
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x00008814 File Offset: 0x00006A14
		// (set) Token: 0x06000455 RID: 1109 RVA: 0x0000274B File Offset: 0x0000094B
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Configuring smooth sphere collisions is no longer needed.", true)]
		public bool smoothSphereCollisions
		{
			get
			{
				return true;
			}
			set
			{
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x00008828 File Offset: 0x00006A28
		// (set) Token: 0x06000457 RID: 1111 RVA: 0x0000274B File Offset: 0x0000094B
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("MeshCollider.skinWidth is no longer used.")]
		public float skinWidth
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000458 RID: 1112 RVA: 0x00008840 File Offset: 0x00006A40
		// (set) Token: 0x06000459 RID: 1113 RVA: 0x0000274B File Offset: 0x0000094B
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("MeshCollider.inflateMesh is no longer supported. The new cooking algorithm doesn't need inflation to be used.")]
		public bool inflateMesh
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x0600045B RID: 1115
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_sharedMesh_Injected(IntPtr _unity_self);

		// Token: 0x0600045C RID: 1116
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_sharedMesh_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x0600045D RID: 1117
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_convex_Injected(IntPtr _unity_self);

		// Token: 0x0600045E RID: 1118
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_convex_Injected(IntPtr _unity_self, bool value);

		// Token: 0x0600045F RID: 1119
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern MeshColliderCookingOptions get_cookingOptions_Injected(IntPtr _unity_self);

		// Token: 0x06000460 RID: 1120
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_cookingOptions_Injected(IntPtr _unity_self, MeshColliderCookingOptions value);
	}
}
