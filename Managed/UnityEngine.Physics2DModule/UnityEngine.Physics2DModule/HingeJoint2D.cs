using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000035 RID: 53
	[NativeHeader("Modules/Physics2D/HingeJoint2D.h")]
	public sealed class HingeJoint2D : AnchoredJoint2D
	{
		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600057F RID: 1407 RVA: 0x0000CD94 File Offset: 0x0000AF94
		// (set) Token: 0x06000580 RID: 1408 RVA: 0x0000CDB8 File Offset: 0x0000AFB8
		public bool useMotor
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<HingeJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return HingeJoint2D.get_useMotor_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<HingeJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				HingeJoint2D.set_useMotor_Injected(intPtr, value);
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x0000CDDC File Offset: 0x0000AFDC
		// (set) Token: 0x06000582 RID: 1410 RVA: 0x0000CE00 File Offset: 0x0000B000
		public bool useLimits
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<HingeJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return HingeJoint2D.get_useLimits_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<HingeJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				HingeJoint2D.set_useLimits_Injected(intPtr, value);
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000583 RID: 1411 RVA: 0x0000CE24 File Offset: 0x0000B024
		// (set) Token: 0x06000584 RID: 1412 RVA: 0x0000CE48 File Offset: 0x0000B048
		public bool useConnectedAnchor
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<HingeJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return HingeJoint2D.get_useConnectedAnchor_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<HingeJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				HingeJoint2D.set_useConnectedAnchor_Injected(intPtr, value);
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000585 RID: 1413 RVA: 0x0000CE6C File Offset: 0x0000B06C
		// (set) Token: 0x06000586 RID: 1414 RVA: 0x0000CE94 File Offset: 0x0000B094
		public JointMotor2D motor
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<HingeJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				JointMotor2D jointMotor2D;
				HingeJoint2D.get_motor_Injected(intPtr, out jointMotor2D);
				return jointMotor2D;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<HingeJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				HingeJoint2D.set_motor_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000587 RID: 1415 RVA: 0x0000CEB8 File Offset: 0x0000B0B8
		// (set) Token: 0x06000588 RID: 1416 RVA: 0x0000CEE0 File Offset: 0x0000B0E0
		public JointAngleLimits2D limits
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<HingeJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				JointAngleLimits2D jointAngleLimits2D;
				HingeJoint2D.get_limits_Injected(intPtr, out jointAngleLimits2D);
				return jointAngleLimits2D;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<HingeJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				HingeJoint2D.set_limits_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x0000CF04 File Offset: 0x0000B104
		public JointLimitState2D limitState
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<HingeJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return HingeJoint2D.get_limitState_Injected(intPtr);
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600058A RID: 1418 RVA: 0x0000CF28 File Offset: 0x0000B128
		public float referenceAngle
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<HingeJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return HingeJoint2D.get_referenceAngle_Injected(intPtr);
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x0000CF4C File Offset: 0x0000B14C
		public float jointAngle
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<HingeJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return HingeJoint2D.get_jointAngle_Injected(intPtr);
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600058C RID: 1420 RVA: 0x0000CF70 File Offset: 0x0000B170
		public float jointSpeed
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<HingeJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return HingeJoint2D.get_jointSpeed_Injected(intPtr);
			}
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0000CF94 File Offset: 0x0000B194
		public float GetMotorTorque(float timeStep)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<HingeJoint2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return HingeJoint2D.GetMotorTorque_Injected(intPtr, timeStep);
		}

		// Token: 0x0600058F RID: 1423
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_useMotor_Injected(IntPtr _unity_self);

		// Token: 0x06000590 RID: 1424
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_useMotor_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000591 RID: 1425
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_useLimits_Injected(IntPtr _unity_self);

		// Token: 0x06000592 RID: 1426
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_useLimits_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000593 RID: 1427
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_useConnectedAnchor_Injected(IntPtr _unity_self);

		// Token: 0x06000594 RID: 1428
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_useConnectedAnchor_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000595 RID: 1429
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_motor_Injected(IntPtr _unity_self, out JointMotor2D ret);

		// Token: 0x06000596 RID: 1430
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_motor_Injected(IntPtr _unity_self, [In] ref JointMotor2D value);

		// Token: 0x06000597 RID: 1431
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_limits_Injected(IntPtr _unity_self, out JointAngleLimits2D ret);

		// Token: 0x06000598 RID: 1432
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_limits_Injected(IntPtr _unity_self, [In] ref JointAngleLimits2D value);

		// Token: 0x06000599 RID: 1433
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern JointLimitState2D get_limitState_Injected(IntPtr _unity_self);

		// Token: 0x0600059A RID: 1434
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_referenceAngle_Injected(IntPtr _unity_self);

		// Token: 0x0600059B RID: 1435
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_jointAngle_Injected(IntPtr _unity_self);

		// Token: 0x0600059C RID: 1436
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_jointSpeed_Injected(IntPtr _unity_self);

		// Token: 0x0600059D RID: 1437
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetMotorTorque_Injected(IntPtr _unity_self, float timeStep);
	}
}
