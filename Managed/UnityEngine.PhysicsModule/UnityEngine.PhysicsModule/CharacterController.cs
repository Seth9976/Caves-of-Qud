using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200001B RID: 27
	[NativeHeader("Modules/Physics/CharacterController.h")]
	public class CharacterController : Collider
	{
		// Token: 0x06000198 RID: 408 RVA: 0x00004900 File Offset: 0x00002B00
		public bool SimpleMove(Vector3 speed)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CharacterController>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return CharacterController.SimpleMove_Injected(intPtr, ref speed);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00004924 File Offset: 0x00002B24
		public CollisionFlags Move(Vector3 motion)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CharacterController>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return CharacterController.Move_Injected(intPtr, ref motion);
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00004948 File Offset: 0x00002B48
		public Vector3 velocity
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CharacterController>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				CharacterController.get_velocity_Injected(intPtr, out vector);
				return vector;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00004970 File Offset: 0x00002B70
		public bool isGrounded
		{
			[NativeName("IsGrounded")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CharacterController>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return CharacterController.get_isGrounded_Injected(intPtr);
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00004994 File Offset: 0x00002B94
		public CollisionFlags collisionFlags
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CharacterController>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return CharacterController.get_collisionFlags_Injected(intPtr);
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600019D RID: 413 RVA: 0x000049B8 File Offset: 0x00002BB8
		// (set) Token: 0x0600019E RID: 414 RVA: 0x000049DC File Offset: 0x00002BDC
		public float radius
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CharacterController>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return CharacterController.get_radius_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CharacterController>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				CharacterController.set_radius_Injected(intPtr, value);
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00004A00 File Offset: 0x00002C00
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x00004A24 File Offset: 0x00002C24
		public float height
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CharacterController>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return CharacterController.get_height_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CharacterController>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				CharacterController.set_height_Injected(intPtr, value);
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00004A48 File Offset: 0x00002C48
		// (set) Token: 0x060001A2 RID: 418 RVA: 0x00004A70 File Offset: 0x00002C70
		public Vector3 center
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CharacterController>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				CharacterController.get_center_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CharacterController>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				CharacterController.set_center_Injected(intPtr, ref value);
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00004A94 File Offset: 0x00002C94
		// (set) Token: 0x060001A4 RID: 420 RVA: 0x00004AB8 File Offset: 0x00002CB8
		public float slopeLimit
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CharacterController>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return CharacterController.get_slopeLimit_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CharacterController>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				CharacterController.set_slopeLimit_Injected(intPtr, value);
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00004ADC File Offset: 0x00002CDC
		// (set) Token: 0x060001A6 RID: 422 RVA: 0x00004B00 File Offset: 0x00002D00
		public float stepOffset
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CharacterController>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return CharacterController.get_stepOffset_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CharacterController>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				CharacterController.set_stepOffset_Injected(intPtr, value);
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00004B24 File Offset: 0x00002D24
		// (set) Token: 0x060001A8 RID: 424 RVA: 0x00004B48 File Offset: 0x00002D48
		public float skinWidth
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CharacterController>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return CharacterController.get_skinWidth_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CharacterController>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				CharacterController.set_skinWidth_Injected(intPtr, value);
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00004B6C File Offset: 0x00002D6C
		// (set) Token: 0x060001AA RID: 426 RVA: 0x00004B90 File Offset: 0x00002D90
		public float minMoveDistance
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CharacterController>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return CharacterController.get_minMoveDistance_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CharacterController>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				CharacterController.set_minMoveDistance_Injected(intPtr, value);
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00004BB4 File Offset: 0x00002DB4
		// (set) Token: 0x060001AC RID: 428 RVA: 0x00004BD8 File Offset: 0x00002DD8
		public bool detectCollisions
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CharacterController>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return CharacterController.get_detectCollisions_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CharacterController>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				CharacterController.set_detectCollisions_Injected(intPtr, value);
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001AD RID: 429 RVA: 0x00004BFC File Offset: 0x00002DFC
		// (set) Token: 0x060001AE RID: 430 RVA: 0x00004C20 File Offset: 0x00002E20
		public bool enableOverlapRecovery
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CharacterController>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return CharacterController.get_enableOverlapRecovery_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CharacterController>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				CharacterController.set_enableOverlapRecovery_Injected(intPtr, value);
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001AF RID: 431 RVA: 0x00004C44 File Offset: 0x00002E44
		internal bool isSupported
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CharacterController>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return CharacterController.get_isSupported_Injected(intPtr);
			}
		}

		// Token: 0x060001B1 RID: 433
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SimpleMove_Injected(IntPtr _unity_self, [In] ref Vector3 speed);

		// Token: 0x060001B2 RID: 434
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern CollisionFlags Move_Injected(IntPtr _unity_self, [In] ref Vector3 motion);

		// Token: 0x060001B3 RID: 435
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_velocity_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x060001B4 RID: 436
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isGrounded_Injected(IntPtr _unity_self);

		// Token: 0x060001B5 RID: 437
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern CollisionFlags get_collisionFlags_Injected(IntPtr _unity_self);

		// Token: 0x060001B6 RID: 438
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_radius_Injected(IntPtr _unity_self);

		// Token: 0x060001B7 RID: 439
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_radius_Injected(IntPtr _unity_self, float value);

		// Token: 0x060001B8 RID: 440
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_height_Injected(IntPtr _unity_self);

		// Token: 0x060001B9 RID: 441
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_height_Injected(IntPtr _unity_self, float value);

		// Token: 0x060001BA RID: 442
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_center_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x060001BB RID: 443
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_center_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x060001BC RID: 444
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_slopeLimit_Injected(IntPtr _unity_self);

		// Token: 0x060001BD RID: 445
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_slopeLimit_Injected(IntPtr _unity_self, float value);

		// Token: 0x060001BE RID: 446
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_stepOffset_Injected(IntPtr _unity_self);

		// Token: 0x060001BF RID: 447
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_stepOffset_Injected(IntPtr _unity_self, float value);

		// Token: 0x060001C0 RID: 448
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_skinWidth_Injected(IntPtr _unity_self);

		// Token: 0x060001C1 RID: 449
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_skinWidth_Injected(IntPtr _unity_self, float value);

		// Token: 0x060001C2 RID: 450
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_minMoveDistance_Injected(IntPtr _unity_self);

		// Token: 0x060001C3 RID: 451
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_minMoveDistance_Injected(IntPtr _unity_self, float value);

		// Token: 0x060001C4 RID: 452
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_detectCollisions_Injected(IntPtr _unity_self);

		// Token: 0x060001C5 RID: 453
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_detectCollisions_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060001C6 RID: 454
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_enableOverlapRecovery_Injected(IntPtr _unity_self);

		// Token: 0x060001C7 RID: 455
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_enableOverlapRecovery_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060001C8 RID: 456
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isSupported_Injected(IntPtr _unity_self);
	}
}
