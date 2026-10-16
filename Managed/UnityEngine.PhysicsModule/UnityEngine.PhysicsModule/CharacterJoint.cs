using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200001C RID: 28
	[RequireComponent(typeof(Rigidbody))]
	[NativeHeader("Modules/Physics/CharacterJoint.h")]
	[NativeClass("Unity::CharacterJoint")]
	public class CharacterJoint : Joint
	{
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00004C68 File Offset: 0x00002E68
		// (set) Token: 0x060001CA RID: 458 RVA: 0x00004C90 File Offset: 0x00002E90
		public Vector3 swingAxis
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CharacterJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				CharacterJoint.get_swingAxis_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CharacterJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				CharacterJoint.set_swingAxis_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00004CB4 File Offset: 0x00002EB4
		// (set) Token: 0x060001CC RID: 460 RVA: 0x00004CDC File Offset: 0x00002EDC
		public SoftJointLimitSpring twistLimitSpring
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CharacterJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SoftJointLimitSpring softJointLimitSpring;
				CharacterJoint.get_twistLimitSpring_Injected(intPtr, out softJointLimitSpring);
				return softJointLimitSpring;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CharacterJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				CharacterJoint.set_twistLimitSpring_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001CD RID: 461 RVA: 0x00004D00 File Offset: 0x00002F00
		// (set) Token: 0x060001CE RID: 462 RVA: 0x00004D28 File Offset: 0x00002F28
		public SoftJointLimitSpring swingLimitSpring
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CharacterJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SoftJointLimitSpring softJointLimitSpring;
				CharacterJoint.get_swingLimitSpring_Injected(intPtr, out softJointLimitSpring);
				return softJointLimitSpring;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CharacterJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				CharacterJoint.set_swingLimitSpring_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001CF RID: 463 RVA: 0x00004D4C File Offset: 0x00002F4C
		// (set) Token: 0x060001D0 RID: 464 RVA: 0x00004D74 File Offset: 0x00002F74
		public SoftJointLimit lowTwistLimit
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CharacterJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SoftJointLimit softJointLimit;
				CharacterJoint.get_lowTwistLimit_Injected(intPtr, out softJointLimit);
				return softJointLimit;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CharacterJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				CharacterJoint.set_lowTwistLimit_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x00004D98 File Offset: 0x00002F98
		// (set) Token: 0x060001D2 RID: 466 RVA: 0x00004DC0 File Offset: 0x00002FC0
		public SoftJointLimit highTwistLimit
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CharacterJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SoftJointLimit softJointLimit;
				CharacterJoint.get_highTwistLimit_Injected(intPtr, out softJointLimit);
				return softJointLimit;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CharacterJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				CharacterJoint.set_highTwistLimit_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x00004DE4 File Offset: 0x00002FE4
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x00004E0C File Offset: 0x0000300C
		public SoftJointLimit swing1Limit
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CharacterJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SoftJointLimit softJointLimit;
				CharacterJoint.get_swing1Limit_Injected(intPtr, out softJointLimit);
				return softJointLimit;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CharacterJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				CharacterJoint.set_swing1Limit_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x00004E30 File Offset: 0x00003030
		// (set) Token: 0x060001D6 RID: 470 RVA: 0x00004E58 File Offset: 0x00003058
		public SoftJointLimit swing2Limit
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CharacterJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SoftJointLimit softJointLimit;
				CharacterJoint.get_swing2Limit_Injected(intPtr, out softJointLimit);
				return softJointLimit;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CharacterJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				CharacterJoint.set_swing2Limit_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x00004E7C File Offset: 0x0000307C
		// (set) Token: 0x060001D8 RID: 472 RVA: 0x00004EA0 File Offset: 0x000030A0
		public bool enableProjection
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CharacterJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return CharacterJoint.get_enableProjection_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CharacterJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				CharacterJoint.set_enableProjection_Injected(intPtr, value);
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x00004EC4 File Offset: 0x000030C4
		// (set) Token: 0x060001DA RID: 474 RVA: 0x00004EE8 File Offset: 0x000030E8
		public float projectionDistance
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CharacterJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return CharacterJoint.get_projectionDistance_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CharacterJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				CharacterJoint.set_projectionDistance_Injected(intPtr, value);
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00004F0C File Offset: 0x0000310C
		// (set) Token: 0x060001DC RID: 476 RVA: 0x00004F30 File Offset: 0x00003130
		public float projectionAngle
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CharacterJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return CharacterJoint.get_projectionAngle_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CharacterJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				CharacterJoint.set_projectionAngle_Injected(intPtr, value);
			}
		}

		// Token: 0x060001DE RID: 478
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_swingAxis_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x060001DF RID: 479
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_swingAxis_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x060001E0 RID: 480
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_twistLimitSpring_Injected(IntPtr _unity_self, out SoftJointLimitSpring ret);

		// Token: 0x060001E1 RID: 481
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_twistLimitSpring_Injected(IntPtr _unity_self, [In] ref SoftJointLimitSpring value);

		// Token: 0x060001E2 RID: 482
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_swingLimitSpring_Injected(IntPtr _unity_self, out SoftJointLimitSpring ret);

		// Token: 0x060001E3 RID: 483
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_swingLimitSpring_Injected(IntPtr _unity_self, [In] ref SoftJointLimitSpring value);

		// Token: 0x060001E4 RID: 484
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_lowTwistLimit_Injected(IntPtr _unity_self, out SoftJointLimit ret);

		// Token: 0x060001E5 RID: 485
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_lowTwistLimit_Injected(IntPtr _unity_self, [In] ref SoftJointLimit value);

		// Token: 0x060001E6 RID: 486
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_highTwistLimit_Injected(IntPtr _unity_self, out SoftJointLimit ret);

		// Token: 0x060001E7 RID: 487
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_highTwistLimit_Injected(IntPtr _unity_self, [In] ref SoftJointLimit value);

		// Token: 0x060001E8 RID: 488
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_swing1Limit_Injected(IntPtr _unity_self, out SoftJointLimit ret);

		// Token: 0x060001E9 RID: 489
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_swing1Limit_Injected(IntPtr _unity_self, [In] ref SoftJointLimit value);

		// Token: 0x060001EA RID: 490
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_swing2Limit_Injected(IntPtr _unity_self, out SoftJointLimit ret);

		// Token: 0x060001EB RID: 491
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_swing2Limit_Injected(IntPtr _unity_self, [In] ref SoftJointLimit value);

		// Token: 0x060001EC RID: 492
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_enableProjection_Injected(IntPtr _unity_self);

		// Token: 0x060001ED RID: 493
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_enableProjection_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060001EE RID: 494
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_projectionDistance_Injected(IntPtr _unity_self);

		// Token: 0x060001EF RID: 495
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_projectionDistance_Injected(IntPtr _unity_self, float value);

		// Token: 0x060001F0 RID: 496
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_projectionAngle_Injected(IntPtr _unity_self);

		// Token: 0x060001F1 RID: 497
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_projectionAngle_Injected(IntPtr _unity_self, float value);

		// Token: 0x04000064 RID: 100
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("TargetRotation not in use for Unity 5 and assumed disabled.", true)]
		public Quaternion targetRotation;

		// Token: 0x04000065 RID: 101
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("TargetAngularVelocity not in use for Unity 5 and assumed disabled.", true)]
		public Vector3 targetAngularVelocity;

		// Token: 0x04000066 RID: 102
		[Obsolete("RotationDrive not in use for Unity 5 and assumed disabled.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public JointDrive rotationDrive;
	}
}
