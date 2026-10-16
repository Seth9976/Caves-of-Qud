using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200019C RID: 412
	[NativeHeader("Runtime/Graphics/Mesh/MeshFilter.h")]
	[RequireComponent(typeof(Transform))]
	public sealed class MeshFilter : Component
	{
		// Token: 0x0600149F RID: 5279 RVA: 0x00002925 File Offset: 0x00000B25
		[RequiredByNativeCode]
		private void DontStripMeshFilter()
		{
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x060014A0 RID: 5280 RVA: 0x00023248 File Offset: 0x00021448
		// (set) Token: 0x060014A1 RID: 5281 RVA: 0x00023270 File Offset: 0x00021470
		public Mesh sharedMesh
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MeshFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Mesh>(MeshFilter.get_sharedMesh_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MeshFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				MeshFilter.set_sharedMesh_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Mesh>(value));
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x060014A2 RID: 5282 RVA: 0x00023298 File Offset: 0x00021498
		// (set) Token: 0x060014A3 RID: 5283 RVA: 0x000232C0 File Offset: 0x000214C0
		public Mesh mesh
		{
			[NativeName("GetInstantiatedMeshFromScript")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MeshFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Mesh>(MeshFilter.get_mesh_Injected(intPtr));
			}
			[NativeName("SetInstantiatedMesh")]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MeshFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				MeshFilter.set_mesh_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Mesh>(value));
			}
		}

		// Token: 0x060014A5 RID: 5285
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_sharedMesh_Injected(IntPtr _unity_self);

		// Token: 0x060014A6 RID: 5286
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_sharedMesh_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x060014A7 RID: 5287
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_mesh_Injected(IntPtr _unity_self);

		// Token: 0x060014A8 RID: 5288
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_mesh_Injected(IntPtr _unity_self, IntPtr value);
	}
}
