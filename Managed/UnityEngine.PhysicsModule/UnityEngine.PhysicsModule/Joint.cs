using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200002C RID: 44
	[NativeClass("Unity::Joint")]
	[NativeHeader("Modules/Physics/Joint.h")]
	public class Joint : Component
	{
		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x0000833C File Offset: 0x0000653C
		// (set) Token: 0x0600041A RID: 1050 RVA: 0x00008364 File Offset: 0x00006564
		public Rigidbody connectedBody
		{
			[NativeName("GetConnectedRigidbody")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Joint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Rigidbody>(Joint.get_connectedBody_Injected(intPtr));
			}
			[NativeName("SetConnectedRigidbody")]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Joint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Joint.set_connectedBody_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Rigidbody>(value));
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x0000838C File Offset: 0x0000658C
		// (set) Token: 0x0600041C RID: 1052 RVA: 0x000083B4 File Offset: 0x000065B4
		public ArticulationBody connectedArticulationBody
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Joint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<ArticulationBody>(Joint.get_connectedArticulationBody_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Joint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Joint.set_connectedArticulationBody_Injected(intPtr, Object.MarshalledUnityObject.Marshal<ArticulationBody>(value));
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x000083DC File Offset: 0x000065DC
		// (set) Token: 0x0600041E RID: 1054 RVA: 0x00008404 File Offset: 0x00006604
		public Vector3 axis
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Joint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				Joint.get_axis_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Joint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Joint.set_axis_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x00008428 File Offset: 0x00006628
		// (set) Token: 0x06000420 RID: 1056 RVA: 0x00008450 File Offset: 0x00006650
		public Vector3 anchor
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Joint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				Joint.get_anchor_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Joint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Joint.set_anchor_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x00008474 File Offset: 0x00006674
		// (set) Token: 0x06000422 RID: 1058 RVA: 0x0000849C File Offset: 0x0000669C
		public Vector3 connectedAnchor
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Joint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				Joint.get_connectedAnchor_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Joint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Joint.set_connectedAnchor_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x000084C0 File Offset: 0x000066C0
		// (set) Token: 0x06000424 RID: 1060 RVA: 0x000084E4 File Offset: 0x000066E4
		public bool autoConfigureConnectedAnchor
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Joint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Joint.get_autoConfigureConnectedAnchor_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Joint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Joint.set_autoConfigureConnectedAnchor_Injected(intPtr, value);
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x00008508 File Offset: 0x00006708
		// (set) Token: 0x06000426 RID: 1062 RVA: 0x0000852C File Offset: 0x0000672C
		public float breakForce
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Joint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Joint.get_breakForce_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Joint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Joint.set_breakForce_Injected(intPtr, value);
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x00008550 File Offset: 0x00006750
		// (set) Token: 0x06000428 RID: 1064 RVA: 0x00008574 File Offset: 0x00006774
		public float breakTorque
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Joint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Joint.get_breakTorque_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Joint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Joint.set_breakTorque_Injected(intPtr, value);
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x00008598 File Offset: 0x00006798
		// (set) Token: 0x0600042A RID: 1066 RVA: 0x000085BC File Offset: 0x000067BC
		public bool enableCollision
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Joint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Joint.get_enableCollision_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Joint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Joint.set_enableCollision_Injected(intPtr, value);
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x000085E0 File Offset: 0x000067E0
		// (set) Token: 0x0600042C RID: 1068 RVA: 0x00008604 File Offset: 0x00006804
		public bool enablePreprocessing
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Joint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Joint.get_enablePreprocessing_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Joint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Joint.set_enablePreprocessing_Injected(intPtr, value);
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x00008628 File Offset: 0x00006828
		// (set) Token: 0x0600042E RID: 1070 RVA: 0x0000864C File Offset: 0x0000684C
		public float massScale
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Joint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Joint.get_massScale_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Joint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Joint.set_massScale_Injected(intPtr, value);
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x00008670 File Offset: 0x00006870
		// (set) Token: 0x06000430 RID: 1072 RVA: 0x00008694 File Offset: 0x00006894
		public float connectedMassScale
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Joint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Joint.get_connectedMassScale_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Joint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Joint.set_connectedMassScale_Injected(intPtr, value);
			}
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x000086B8 File Offset: 0x000068B8
		private void GetCurrentForces(ref Vector3 linearForce, ref Vector3 angularForce)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Joint>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Joint.GetCurrentForces_Injected(intPtr, ref linearForce, ref angularForce);
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x000086DC File Offset: 0x000068DC
		public Vector3 currentForce
		{
			get
			{
				Vector3 zero = Vector3.zero;
				Vector3 zero2 = Vector3.zero;
				this.GetCurrentForces(ref zero, ref zero2);
				return zero;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x00008708 File Offset: 0x00006908
		public Vector3 currentTorque
		{
			get
			{
				Vector3 zero = Vector3.zero;
				Vector3 zero2 = Vector3.zero;
				this.GetCurrentForces(ref zero, ref zero2);
				return zero2;
			}
		}

		// Token: 0x06000435 RID: 1077
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_connectedBody_Injected(IntPtr _unity_self);

		// Token: 0x06000436 RID: 1078
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_connectedBody_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x06000437 RID: 1079
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_connectedArticulationBody_Injected(IntPtr _unity_self);

		// Token: 0x06000438 RID: 1080
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_connectedArticulationBody_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x06000439 RID: 1081
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_axis_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x0600043A RID: 1082
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_axis_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x0600043B RID: 1083
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_anchor_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x0600043C RID: 1084
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_anchor_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x0600043D RID: 1085
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_connectedAnchor_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x0600043E RID: 1086
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_connectedAnchor_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x0600043F RID: 1087
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_autoConfigureConnectedAnchor_Injected(IntPtr _unity_self);

		// Token: 0x06000440 RID: 1088
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_autoConfigureConnectedAnchor_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000441 RID: 1089
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_breakForce_Injected(IntPtr _unity_self);

		// Token: 0x06000442 RID: 1090
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_breakForce_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000443 RID: 1091
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_breakTorque_Injected(IntPtr _unity_self);

		// Token: 0x06000444 RID: 1092
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_breakTorque_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000445 RID: 1093
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_enableCollision_Injected(IntPtr _unity_self);

		// Token: 0x06000446 RID: 1094
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_enableCollision_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000447 RID: 1095
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_enablePreprocessing_Injected(IntPtr _unity_self);

		// Token: 0x06000448 RID: 1096
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_enablePreprocessing_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000449 RID: 1097
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_massScale_Injected(IntPtr _unity_self);

		// Token: 0x0600044A RID: 1098
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_massScale_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600044B RID: 1099
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_connectedMassScale_Injected(IntPtr _unity_self);

		// Token: 0x0600044C RID: 1100
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_connectedMassScale_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600044D RID: 1101
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetCurrentForces_Injected(IntPtr _unity_self, ref Vector3 linearForce, ref Vector3 angularForce);
	}
}
