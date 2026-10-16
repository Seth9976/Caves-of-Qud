using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000042 RID: 66
	[RequireComponent(typeof(Rigidbody2D))]
	[NativeHeader("Modules/Physics2D/ConstantForce2D.h")]
	public sealed class ConstantForce2D : PhysicsUpdateBehaviour2D
	{
		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060006C2 RID: 1730 RVA: 0x0000E390 File Offset: 0x0000C590
		// (set) Token: 0x060006C3 RID: 1731 RVA: 0x0000E3B8 File Offset: 0x0000C5B8
		public Vector2 force
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConstantForce2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				ConstantForce2D.get_force_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConstantForce2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ConstantForce2D.set_force_Injected(intPtr, ref value);
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060006C4 RID: 1732 RVA: 0x0000E3DC File Offset: 0x0000C5DC
		// (set) Token: 0x060006C5 RID: 1733 RVA: 0x0000E404 File Offset: 0x0000C604
		public Vector2 relativeForce
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConstantForce2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				ConstantForce2D.get_relativeForce_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConstantForce2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ConstantForce2D.set_relativeForce_Injected(intPtr, ref value);
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060006C6 RID: 1734 RVA: 0x0000E428 File Offset: 0x0000C628
		// (set) Token: 0x060006C7 RID: 1735 RVA: 0x0000E44C File Offset: 0x0000C64C
		public float torque
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConstantForce2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ConstantForce2D.get_torque_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConstantForce2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ConstantForce2D.set_torque_Injected(intPtr, value);
			}
		}

		// Token: 0x060006C9 RID: 1737
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_force_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x060006CA RID: 1738
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_force_Injected(IntPtr _unity_self, [In] ref Vector2 value);

		// Token: 0x060006CB RID: 1739
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_relativeForce_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x060006CC RID: 1740
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_relativeForce_Injected(IntPtr _unity_self, [In] ref Vector2 value);

		// Token: 0x060006CD RID: 1741
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_torque_Injected(IntPtr _unity_self);

		// Token: 0x060006CE RID: 1742
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_torque_Injected(IntPtr _unity_self, float value);
	}
}
