using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000037 RID: 55
	[NativeHeader("Modules/Physics2D/SliderJoint2D.h")]
	public sealed class SliderJoint2D : AnchoredJoint2D
	{
		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060005B9 RID: 1465 RVA: 0x0000D194 File Offset: 0x0000B394
		// (set) Token: 0x060005BA RID: 1466 RVA: 0x0000D1B8 File Offset: 0x0000B3B8
		public bool autoConfigureAngle
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SliderJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SliderJoint2D.get_autoConfigureAngle_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SliderJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SliderJoint2D.set_autoConfigureAngle_Injected(intPtr, value);
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060005BB RID: 1467 RVA: 0x0000D1DC File Offset: 0x0000B3DC
		// (set) Token: 0x060005BC RID: 1468 RVA: 0x0000D200 File Offset: 0x0000B400
		public float angle
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SliderJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SliderJoint2D.get_angle_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SliderJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SliderJoint2D.set_angle_Injected(intPtr, value);
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060005BD RID: 1469 RVA: 0x0000D224 File Offset: 0x0000B424
		// (set) Token: 0x060005BE RID: 1470 RVA: 0x0000D248 File Offset: 0x0000B448
		public bool useMotor
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SliderJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SliderJoint2D.get_useMotor_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SliderJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SliderJoint2D.set_useMotor_Injected(intPtr, value);
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060005BF RID: 1471 RVA: 0x0000D26C File Offset: 0x0000B46C
		// (set) Token: 0x060005C0 RID: 1472 RVA: 0x0000D290 File Offset: 0x0000B490
		public bool useLimits
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SliderJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SliderJoint2D.get_useLimits_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SliderJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SliderJoint2D.set_useLimits_Injected(intPtr, value);
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060005C1 RID: 1473 RVA: 0x0000D2B4 File Offset: 0x0000B4B4
		// (set) Token: 0x060005C2 RID: 1474 RVA: 0x0000D2DC File Offset: 0x0000B4DC
		public JointMotor2D motor
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SliderJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				JointMotor2D jointMotor2D;
				SliderJoint2D.get_motor_Injected(intPtr, out jointMotor2D);
				return jointMotor2D;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SliderJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SliderJoint2D.set_motor_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060005C3 RID: 1475 RVA: 0x0000D300 File Offset: 0x0000B500
		// (set) Token: 0x060005C4 RID: 1476 RVA: 0x0000D328 File Offset: 0x0000B528
		public JointTranslationLimits2D limits
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SliderJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				JointTranslationLimits2D jointTranslationLimits2D;
				SliderJoint2D.get_limits_Injected(intPtr, out jointTranslationLimits2D);
				return jointTranslationLimits2D;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SliderJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SliderJoint2D.set_limits_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060005C5 RID: 1477 RVA: 0x0000D34C File Offset: 0x0000B54C
		public JointLimitState2D limitState
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SliderJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SliderJoint2D.get_limitState_Injected(intPtr);
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060005C6 RID: 1478 RVA: 0x0000D370 File Offset: 0x0000B570
		public float referenceAngle
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SliderJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SliderJoint2D.get_referenceAngle_Injected(intPtr);
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060005C7 RID: 1479 RVA: 0x0000D394 File Offset: 0x0000B594
		public float jointTranslation
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SliderJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SliderJoint2D.get_jointTranslation_Injected(intPtr);
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060005C8 RID: 1480 RVA: 0x0000D3B8 File Offset: 0x0000B5B8
		public float jointSpeed
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SliderJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SliderJoint2D.get_jointSpeed_Injected(intPtr);
			}
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x0000D3DC File Offset: 0x0000B5DC
		public float GetMotorForce(float timeStep)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SliderJoint2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return SliderJoint2D.GetMotorForce_Injected(intPtr, timeStep);
		}

		// Token: 0x060005CB RID: 1483
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_autoConfigureAngle_Injected(IntPtr _unity_self);

		// Token: 0x060005CC RID: 1484
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_autoConfigureAngle_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060005CD RID: 1485
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_angle_Injected(IntPtr _unity_self);

		// Token: 0x060005CE RID: 1486
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_angle_Injected(IntPtr _unity_self, float value);

		// Token: 0x060005CF RID: 1487
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_useMotor_Injected(IntPtr _unity_self);

		// Token: 0x060005D0 RID: 1488
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_useMotor_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060005D1 RID: 1489
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_useLimits_Injected(IntPtr _unity_self);

		// Token: 0x060005D2 RID: 1490
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_useLimits_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060005D3 RID: 1491
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_motor_Injected(IntPtr _unity_self, out JointMotor2D ret);

		// Token: 0x060005D4 RID: 1492
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_motor_Injected(IntPtr _unity_self, [In] ref JointMotor2D value);

		// Token: 0x060005D5 RID: 1493
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_limits_Injected(IntPtr _unity_self, out JointTranslationLimits2D ret);

		// Token: 0x060005D6 RID: 1494
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_limits_Injected(IntPtr _unity_self, [In] ref JointTranslationLimits2D value);

		// Token: 0x060005D7 RID: 1495
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern JointLimitState2D get_limitState_Injected(IntPtr _unity_self);

		// Token: 0x060005D8 RID: 1496
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_referenceAngle_Injected(IntPtr _unity_self);

		// Token: 0x060005D9 RID: 1497
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_jointTranslation_Injected(IntPtr _unity_self);

		// Token: 0x060005DA RID: 1498
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_jointSpeed_Injected(IntPtr _unity_self);

		// Token: 0x060005DB RID: 1499
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetMotorForce_Injected(IntPtr _unity_self, float timeStep);
	}
}
