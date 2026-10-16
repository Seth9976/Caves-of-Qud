using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000003 RID: 3
	[NativeHeader("Modules/Vehicles/WheelCollider.h")]
	[NativeHeader("PhysicsScriptingClasses.h")]
	public class WheelCollider : Collider
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002170 File Offset: 0x00000370
		// (set) Token: 0x06000012 RID: 18 RVA: 0x00002198 File Offset: 0x00000398
		public Vector3 center
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WheelCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				WheelCollider.get_center_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WheelCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				WheelCollider.set_center_Injected(intPtr, ref value);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000021BC File Offset: 0x000003BC
		// (set) Token: 0x06000014 RID: 20 RVA: 0x000021E0 File Offset: 0x000003E0
		public float radius
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WheelCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return WheelCollider.get_radius_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WheelCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				WheelCollider.set_radius_Injected(intPtr, value);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002204 File Offset: 0x00000404
		// (set) Token: 0x06000016 RID: 22 RVA: 0x00002228 File Offset: 0x00000428
		public float suspensionDistance
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WheelCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return WheelCollider.get_suspensionDistance_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WheelCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				WheelCollider.set_suspensionDistance_Injected(intPtr, value);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000017 RID: 23 RVA: 0x0000224C File Offset: 0x0000044C
		// (set) Token: 0x06000018 RID: 24 RVA: 0x00002274 File Offset: 0x00000474
		public JointSpring suspensionSpring
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WheelCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				JointSpring jointSpring;
				WheelCollider.get_suspensionSpring_Injected(intPtr, out jointSpring);
				return jointSpring;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WheelCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				WheelCollider.set_suspensionSpring_Injected(intPtr, ref value);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002298 File Offset: 0x00000498
		// (set) Token: 0x0600001A RID: 26 RVA: 0x000022BC File Offset: 0x000004BC
		public bool suspensionExpansionLimited
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WheelCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return WheelCollider.get_suspensionExpansionLimited_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WheelCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				WheelCollider.set_suspensionExpansionLimited_Injected(intPtr, value);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000022E0 File Offset: 0x000004E0
		// (set) Token: 0x0600001C RID: 28 RVA: 0x00002304 File Offset: 0x00000504
		public float forceAppPointDistance
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WheelCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return WheelCollider.get_forceAppPointDistance_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WheelCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				WheelCollider.set_forceAppPointDistance_Injected(intPtr, value);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002328 File Offset: 0x00000528
		// (set) Token: 0x0600001E RID: 30 RVA: 0x0000234C File Offset: 0x0000054C
		public float mass
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WheelCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return WheelCollider.get_mass_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WheelCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				WheelCollider.set_mass_Injected(intPtr, value);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002370 File Offset: 0x00000570
		// (set) Token: 0x06000020 RID: 32 RVA: 0x00002394 File Offset: 0x00000594
		public float wheelDampingRate
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WheelCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return WheelCollider.get_wheelDampingRate_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WheelCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				WheelCollider.set_wheelDampingRate_Injected(intPtr, value);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000023B8 File Offset: 0x000005B8
		// (set) Token: 0x06000022 RID: 34 RVA: 0x000023E0 File Offset: 0x000005E0
		public WheelFrictionCurve forwardFriction
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WheelCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				WheelFrictionCurve wheelFrictionCurve;
				WheelCollider.get_forwardFriction_Injected(intPtr, out wheelFrictionCurve);
				return wheelFrictionCurve;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WheelCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				WheelCollider.set_forwardFriction_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002404 File Offset: 0x00000604
		// (set) Token: 0x06000024 RID: 36 RVA: 0x0000242C File Offset: 0x0000062C
		public WheelFrictionCurve sidewaysFriction
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WheelCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				WheelFrictionCurve wheelFrictionCurve;
				WheelCollider.get_sidewaysFriction_Injected(intPtr, out wheelFrictionCurve);
				return wheelFrictionCurve;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WheelCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				WheelCollider.set_sidewaysFriction_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002450 File Offset: 0x00000650
		// (set) Token: 0x06000026 RID: 38 RVA: 0x00002474 File Offset: 0x00000674
		public float motorTorque
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WheelCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return WheelCollider.get_motorTorque_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WheelCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				WheelCollider.set_motorTorque_Injected(intPtr, value);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002498 File Offset: 0x00000698
		// (set) Token: 0x06000028 RID: 40 RVA: 0x000024BC File Offset: 0x000006BC
		public float brakeTorque
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WheelCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return WheelCollider.get_brakeTorque_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WheelCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				WheelCollider.set_brakeTorque_Injected(intPtr, value);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000024E0 File Offset: 0x000006E0
		// (set) Token: 0x0600002A RID: 42 RVA: 0x00002504 File Offset: 0x00000704
		public float steerAngle
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WheelCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return WheelCollider.get_steerAngle_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WheelCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				WheelCollider.set_steerAngle_Injected(intPtr, value);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002528 File Offset: 0x00000728
		public bool isGrounded
		{
			[NativeName("IsGrounded")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WheelCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return WheelCollider.get_isGrounded_Injected(intPtr);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600002C RID: 44 RVA: 0x0000254C File Offset: 0x0000074C
		public float rpm
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WheelCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return WheelCollider.get_rpm_Injected(intPtr);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002570 File Offset: 0x00000770
		// (set) Token: 0x0600002E RID: 46 RVA: 0x00002594 File Offset: 0x00000794
		public float sprungMass
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WheelCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return WheelCollider.get_sprungMass_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WheelCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				WheelCollider.set_sprungMass_Injected(intPtr, value);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000025B8 File Offset: 0x000007B8
		// (set) Token: 0x06000030 RID: 48 RVA: 0x000025DC File Offset: 0x000007DC
		public float rotationSpeed
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WheelCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return WheelCollider.get_rotationSpeed_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WheelCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				WheelCollider.set_rotationSpeed_Injected(intPtr, value);
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002600 File Offset: 0x00000800
		public void ResetSprungMasses()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WheelCollider>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			WheelCollider.ResetSprungMasses_Injected(intPtr);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002624 File Offset: 0x00000824
		public void ConfigureVehicleSubsteps(float speedThreshold, int stepsBelowThreshold, int stepsAboveThreshold)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WheelCollider>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			WheelCollider.ConfigureVehicleSubsteps_Injected(intPtr, speedThreshold, stepsBelowThreshold, stepsAboveThreshold);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000264C File Offset: 0x0000084C
		public void GetWorldPose(out Vector3 pos, out Quaternion quat)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WheelCollider>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			WheelCollider.GetWorldPose_Injected(intPtr, out pos, out quat);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002670 File Offset: 0x00000870
		public bool GetGroundHit(out WheelHit hit)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WheelCollider>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return WheelCollider.GetGroundHit_Injected(intPtr, out hit);
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002694 File Offset: 0x00000894
		internal bool isSupported
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<WheelCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return WheelCollider.get_isSupported_Injected(intPtr);
			}
		}

		// Token: 0x06000037 RID: 55
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_center_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x06000038 RID: 56
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_center_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x06000039 RID: 57
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_radius_Injected(IntPtr _unity_self);

		// Token: 0x0600003A RID: 58
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_radius_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600003B RID: 59
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_suspensionDistance_Injected(IntPtr _unity_self);

		// Token: 0x0600003C RID: 60
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_suspensionDistance_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600003D RID: 61
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_suspensionSpring_Injected(IntPtr _unity_self, out JointSpring ret);

		// Token: 0x0600003E RID: 62
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_suspensionSpring_Injected(IntPtr _unity_self, [In] ref JointSpring value);

		// Token: 0x0600003F RID: 63
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_suspensionExpansionLimited_Injected(IntPtr _unity_self);

		// Token: 0x06000040 RID: 64
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_suspensionExpansionLimited_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000041 RID: 65
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_forceAppPointDistance_Injected(IntPtr _unity_self);

		// Token: 0x06000042 RID: 66
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_forceAppPointDistance_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000043 RID: 67
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_mass_Injected(IntPtr _unity_self);

		// Token: 0x06000044 RID: 68
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_mass_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000045 RID: 69
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_wheelDampingRate_Injected(IntPtr _unity_self);

		// Token: 0x06000046 RID: 70
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_wheelDampingRate_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000047 RID: 71
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_forwardFriction_Injected(IntPtr _unity_self, out WheelFrictionCurve ret);

		// Token: 0x06000048 RID: 72
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_forwardFriction_Injected(IntPtr _unity_self, [In] ref WheelFrictionCurve value);

		// Token: 0x06000049 RID: 73
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_sidewaysFriction_Injected(IntPtr _unity_self, out WheelFrictionCurve ret);

		// Token: 0x0600004A RID: 74
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_sidewaysFriction_Injected(IntPtr _unity_self, [In] ref WheelFrictionCurve value);

		// Token: 0x0600004B RID: 75
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_motorTorque_Injected(IntPtr _unity_self);

		// Token: 0x0600004C RID: 76
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_motorTorque_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600004D RID: 77
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_brakeTorque_Injected(IntPtr _unity_self);

		// Token: 0x0600004E RID: 78
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_brakeTorque_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600004F RID: 79
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_steerAngle_Injected(IntPtr _unity_self);

		// Token: 0x06000050 RID: 80
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_steerAngle_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000051 RID: 81
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isGrounded_Injected(IntPtr _unity_self);

		// Token: 0x06000052 RID: 82
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_rpm_Injected(IntPtr _unity_self);

		// Token: 0x06000053 RID: 83
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_sprungMass_Injected(IntPtr _unity_self);

		// Token: 0x06000054 RID: 84
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_sprungMass_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000055 RID: 85
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_rotationSpeed_Injected(IntPtr _unity_self);

		// Token: 0x06000056 RID: 86
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_rotationSpeed_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000057 RID: 87
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ResetSprungMasses_Injected(IntPtr _unity_self);

		// Token: 0x06000058 RID: 88
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ConfigureVehicleSubsteps_Injected(IntPtr _unity_self, float speedThreshold, int stepsBelowThreshold, int stepsAboveThreshold);

		// Token: 0x06000059 RID: 89
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetWorldPose_Injected(IntPtr _unity_self, out Vector3 pos, out Quaternion quat);

		// Token: 0x0600005A RID: 90
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetGroundHit_Injected(IntPtr _unity_self, out WheelHit hit);

		// Token: 0x0600005B RID: 91
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isSupported_Injected(IntPtr _unity_self);
	}
}
