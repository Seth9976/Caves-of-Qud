using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000053 RID: 83
	[RequireComponent(typeof(Transform))]
	[NativeHeader("Modules/Physics/SphereCollider.h")]
	public class SphereCollider : Collider
	{
		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000668 RID: 1640 RVA: 0x0000BB90 File Offset: 0x00009D90
		// (set) Token: 0x06000669 RID: 1641 RVA: 0x0000BBB8 File Offset: 0x00009DB8
		public Vector3 center
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SphereCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				SphereCollider.get_center_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SphereCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SphereCollider.set_center_Injected(intPtr, ref value);
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x0600066A RID: 1642 RVA: 0x0000BBDC File Offset: 0x00009DDC
		// (set) Token: 0x0600066B RID: 1643 RVA: 0x0000BC00 File Offset: 0x00009E00
		public float radius
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SphereCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SphereCollider.get_radius_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SphereCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SphereCollider.set_radius_Injected(intPtr, value);
			}
		}

		// Token: 0x0600066D RID: 1645
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_center_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x0600066E RID: 1646
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_center_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x0600066F RID: 1647
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_radius_Injected(IntPtr _unity_self);

		// Token: 0x06000670 RID: 1648
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_radius_Injected(IntPtr _unity_self, float value);
	}
}
