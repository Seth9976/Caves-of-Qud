using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200003A RID: 58
	[NativeHeader("Modules/Physics2D/WheelJoint2D.h")]
	public sealed class WheelJoint2D : AnchoredJoint2D
	{
		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000600 RID: 1536 RVA: 0x0000D66C File Offset: 0x0000B86C
		// (set) Token: 0x06000601 RID: 1537 RVA: 0x0000D694 File Offset: 0x0000B894
		public JointSuspension2D suspension
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WheelJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				JointSuspension2D jointSuspension2D;
				WheelJoint2D.get_suspension_Injected(intPtr, out jointSuspension2D);
				return jointSuspension2D;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WheelJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				WheelJoint2D.set_suspension_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000602 RID: 1538 RVA: 0x0000D6B8 File Offset: 0x0000B8B8
		// (set) Token: 0x06000603 RID: 1539 RVA: 0x0000D6DC File Offset: 0x0000B8DC
		public bool useMotor
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WheelJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return WheelJoint2D.get_useMotor_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WheelJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				WheelJoint2D.set_useMotor_Injected(intPtr, value);
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000604 RID: 1540 RVA: 0x0000D700 File Offset: 0x0000B900
		// (set) Token: 0x06000605 RID: 1541 RVA: 0x0000D728 File Offset: 0x0000B928
		public JointMotor2D motor
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WheelJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				JointMotor2D jointMotor2D;
				WheelJoint2D.get_motor_Injected(intPtr, out jointMotor2D);
				return jointMotor2D;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WheelJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				WheelJoint2D.set_motor_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000606 RID: 1542 RVA: 0x0000D74C File Offset: 0x0000B94C
		public float jointTranslation
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WheelJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return WheelJoint2D.get_jointTranslation_Injected(intPtr);
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000607 RID: 1543 RVA: 0x0000D770 File Offset: 0x0000B970
		public float jointLinearSpeed
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WheelJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return WheelJoint2D.get_jointLinearSpeed_Injected(intPtr);
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000608 RID: 1544 RVA: 0x0000D794 File Offset: 0x0000B994
		public float jointSpeed
		{
			[NativeMethod("GetJointAngularSpeed")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WheelJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return WheelJoint2D.get_jointSpeed_Injected(intPtr);
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000609 RID: 1545 RVA: 0x0000D7B8 File Offset: 0x0000B9B8
		public float jointAngle
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WheelJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return WheelJoint2D.get_jointAngle_Injected(intPtr);
			}
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x0000D7DC File Offset: 0x0000B9DC
		public float GetMotorTorque(float timeStep)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WheelJoint2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return WheelJoint2D.GetMotorTorque_Injected(intPtr, timeStep);
		}

		// Token: 0x0600060C RID: 1548
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_suspension_Injected(IntPtr _unity_self, out JointSuspension2D ret);

		// Token: 0x0600060D RID: 1549
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_suspension_Injected(IntPtr _unity_self, [In] ref JointSuspension2D value);

		// Token: 0x0600060E RID: 1550
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_useMotor_Injected(IntPtr _unity_self);

		// Token: 0x0600060F RID: 1551
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_useMotor_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000610 RID: 1552
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_motor_Injected(IntPtr _unity_self, out JointMotor2D ret);

		// Token: 0x06000611 RID: 1553
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_motor_Injected(IntPtr _unity_self, [In] ref JointMotor2D value);

		// Token: 0x06000612 RID: 1554
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_jointTranslation_Injected(IntPtr _unity_self);

		// Token: 0x06000613 RID: 1555
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_jointLinearSpeed_Injected(IntPtr _unity_self);

		// Token: 0x06000614 RID: 1556
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_jointSpeed_Injected(IntPtr _unity_self);

		// Token: 0x06000615 RID: 1557
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_jointAngle_Injected(IntPtr _unity_self);

		// Token: 0x06000616 RID: 1558
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetMotorTorque_Injected(IntPtr _unity_self, float timeStep);
	}
}
