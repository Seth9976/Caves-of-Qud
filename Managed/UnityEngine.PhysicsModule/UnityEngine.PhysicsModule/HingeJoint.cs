using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200002B RID: 43
	[NativeClass("Unity::HingeJoint")]
	[RequireComponent(typeof(Rigidbody))]
	[NativeHeader("Modules/Physics/HingeJoint.h")]
	public class HingeJoint : Joint
	{
		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x000080A8 File Offset: 0x000062A8
		// (set) Token: 0x060003F5 RID: 1013 RVA: 0x000080D0 File Offset: 0x000062D0
		public JointMotor motor
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<HingeJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				JointMotor jointMotor;
				HingeJoint.get_motor_Injected(intPtr, out jointMotor);
				return jointMotor;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<HingeJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				HingeJoint.set_motor_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x000080F4 File Offset: 0x000062F4
		// (set) Token: 0x060003F7 RID: 1015 RVA: 0x0000811C File Offset: 0x0000631C
		public JointLimits limits
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<HingeJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				JointLimits jointLimits;
				HingeJoint.get_limits_Injected(intPtr, out jointLimits);
				return jointLimits;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<HingeJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				HingeJoint.set_limits_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x00008140 File Offset: 0x00006340
		// (set) Token: 0x060003F9 RID: 1017 RVA: 0x00008168 File Offset: 0x00006368
		public JointSpring spring
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<HingeJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				JointSpring jointSpring;
				HingeJoint.get_spring_Injected(intPtr, out jointSpring);
				return jointSpring;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<HingeJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				HingeJoint.set_spring_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x0000818C File Offset: 0x0000638C
		// (set) Token: 0x060003FB RID: 1019 RVA: 0x000081B0 File Offset: 0x000063B0
		public bool useMotor
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<HingeJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return HingeJoint.get_useMotor_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<HingeJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				HingeJoint.set_useMotor_Injected(intPtr, value);
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x000081D4 File Offset: 0x000063D4
		// (set) Token: 0x060003FD RID: 1021 RVA: 0x000081F8 File Offset: 0x000063F8
		public bool useLimits
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<HingeJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return HingeJoint.get_useLimits_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<HingeJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				HingeJoint.set_useLimits_Injected(intPtr, value);
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x0000821C File Offset: 0x0000641C
		// (set) Token: 0x060003FF RID: 1023 RVA: 0x00008240 File Offset: 0x00006440
		public bool extendedLimits
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<HingeJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return HingeJoint.get_extendedLimits_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<HingeJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				HingeJoint.set_extendedLimits_Injected(intPtr, value);
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x00008264 File Offset: 0x00006464
		// (set) Token: 0x06000401 RID: 1025 RVA: 0x00008288 File Offset: 0x00006488
		public bool useSpring
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<HingeJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return HingeJoint.get_useSpring_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<HingeJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				HingeJoint.set_useSpring_Injected(intPtr, value);
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x000082AC File Offset: 0x000064AC
		public float velocity
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<HingeJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return HingeJoint.get_velocity_Injected(intPtr);
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000403 RID: 1027 RVA: 0x000082D0 File Offset: 0x000064D0
		public float angle
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<HingeJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return HingeJoint.get_angle_Injected(intPtr);
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x000082F4 File Offset: 0x000064F4
		// (set) Token: 0x06000405 RID: 1029 RVA: 0x00008318 File Offset: 0x00006518
		public bool useAcceleration
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<HingeJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return HingeJoint.get_useAcceleration_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<HingeJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				HingeJoint.set_useAcceleration_Injected(intPtr, value);
			}
		}

		// Token: 0x06000407 RID: 1031
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_motor_Injected(IntPtr _unity_self, out JointMotor ret);

		// Token: 0x06000408 RID: 1032
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_motor_Injected(IntPtr _unity_self, [In] ref JointMotor value);

		// Token: 0x06000409 RID: 1033
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_limits_Injected(IntPtr _unity_self, out JointLimits ret);

		// Token: 0x0600040A RID: 1034
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_limits_Injected(IntPtr _unity_self, [In] ref JointLimits value);

		// Token: 0x0600040B RID: 1035
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_spring_Injected(IntPtr _unity_self, out JointSpring ret);

		// Token: 0x0600040C RID: 1036
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_spring_Injected(IntPtr _unity_self, [In] ref JointSpring value);

		// Token: 0x0600040D RID: 1037
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_useMotor_Injected(IntPtr _unity_self);

		// Token: 0x0600040E RID: 1038
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_useMotor_Injected(IntPtr _unity_self, bool value);

		// Token: 0x0600040F RID: 1039
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_useLimits_Injected(IntPtr _unity_self);

		// Token: 0x06000410 RID: 1040
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_useLimits_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000411 RID: 1041
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_extendedLimits_Injected(IntPtr _unity_self);

		// Token: 0x06000412 RID: 1042
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_extendedLimits_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000413 RID: 1043
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_useSpring_Injected(IntPtr _unity_self);

		// Token: 0x06000414 RID: 1044
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_useSpring_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000415 RID: 1045
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_velocity_Injected(IntPtr _unity_self);

		// Token: 0x06000416 RID: 1046
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_angle_Injected(IntPtr _unity_self);

		// Token: 0x06000417 RID: 1047
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_useAcceleration_Injected(IntPtr _unity_self);

		// Token: 0x06000418 RID: 1048
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_useAcceleration_Injected(IntPtr _unity_self, bool value);
	}
}
