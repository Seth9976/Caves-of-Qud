using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000022 RID: 34
	[RequireComponent(typeof(Rigidbody))]
	[NativeHeader("Modules/Physics/ConstantForce.h")]
	public class ConstantForce : Behaviour
	{
		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x00005CE0 File Offset: 0x00003EE0
		// (set) Token: 0x060002AA RID: 682 RVA: 0x00005D08 File Offset: 0x00003F08
		public Vector3 force
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConstantForce>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				ConstantForce.get_force_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConstantForce>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ConstantForce.set_force_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060002AB RID: 683 RVA: 0x00005D2C File Offset: 0x00003F2C
		// (set) Token: 0x060002AC RID: 684 RVA: 0x00005D54 File Offset: 0x00003F54
		public Vector3 torque
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConstantForce>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				ConstantForce.get_torque_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConstantForce>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ConstantForce.set_torque_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060002AD RID: 685 RVA: 0x00005D78 File Offset: 0x00003F78
		// (set) Token: 0x060002AE RID: 686 RVA: 0x00005DA0 File Offset: 0x00003FA0
		public Vector3 relativeForce
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConstantForce>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				ConstantForce.get_relativeForce_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConstantForce>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ConstantForce.set_relativeForce_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060002AF RID: 687 RVA: 0x00005DC4 File Offset: 0x00003FC4
		// (set) Token: 0x060002B0 RID: 688 RVA: 0x00005DEC File Offset: 0x00003FEC
		public Vector3 relativeTorque
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConstantForce>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				ConstantForce.get_relativeTorque_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConstantForce>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ConstantForce.set_relativeTorque_Injected(intPtr, ref value);
			}
		}

		// Token: 0x060002B2 RID: 690
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_force_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x060002B3 RID: 691
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_force_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x060002B4 RID: 692
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_torque_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x060002B5 RID: 693
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_torque_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x060002B6 RID: 694
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_relativeForce_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x060002B7 RID: 695
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_relativeForce_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x060002B8 RID: 696
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_relativeTorque_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x060002B9 RID: 697
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_relativeTorque_Injected(IntPtr _unity_self, [In] ref Vector3 value);
	}
}
