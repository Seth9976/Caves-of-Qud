using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000018 RID: 24
	[RequireComponent(typeof(Transform))]
	[NativeHeader("Modules/Physics/CapsuleCollider.h")]
	public class CapsuleCollider : Collider
	{
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600017B RID: 379 RVA: 0x000046B8 File Offset: 0x000028B8
		// (set) Token: 0x0600017C RID: 380 RVA: 0x000046E0 File Offset: 0x000028E0
		public Vector3 center
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CapsuleCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				CapsuleCollider.get_center_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CapsuleCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				CapsuleCollider.set_center_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600017D RID: 381 RVA: 0x00004704 File Offset: 0x00002904
		// (set) Token: 0x0600017E RID: 382 RVA: 0x00004728 File Offset: 0x00002928
		public float radius
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CapsuleCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return CapsuleCollider.get_radius_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CapsuleCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				CapsuleCollider.set_radius_Injected(intPtr, value);
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600017F RID: 383 RVA: 0x0000474C File Offset: 0x0000294C
		// (set) Token: 0x06000180 RID: 384 RVA: 0x00004770 File Offset: 0x00002970
		public float height
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CapsuleCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return CapsuleCollider.get_height_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CapsuleCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				CapsuleCollider.set_height_Injected(intPtr, value);
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000181 RID: 385 RVA: 0x00004794 File Offset: 0x00002994
		// (set) Token: 0x06000182 RID: 386 RVA: 0x000047B8 File Offset: 0x000029B8
		public int direction
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CapsuleCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return CapsuleCollider.get_direction_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CapsuleCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				CapsuleCollider.set_direction_Injected(intPtr, value);
			}
		}

		// Token: 0x06000184 RID: 388
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_center_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x06000185 RID: 389
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_center_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x06000186 RID: 390
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_radius_Injected(IntPtr _unity_self);

		// Token: 0x06000187 RID: 391
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_radius_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000188 RID: 392
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_height_Injected(IntPtr _unity_self);

		// Token: 0x06000189 RID: 393
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_height_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600018A RID: 394
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_direction_Injected(IntPtr _unity_self);

		// Token: 0x0600018B RID: 395
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_direction_Injected(IntPtr _unity_self, int value);
	}
}
