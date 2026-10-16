using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000039 RID: 57
	[NativeHeader("Modules/Physics2D/FixedJoint2D.h")]
	public sealed class FixedJoint2D : AnchoredJoint2D
	{
		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060005F5 RID: 1525 RVA: 0x0000D5B8 File Offset: 0x0000B7B8
		// (set) Token: 0x060005F6 RID: 1526 RVA: 0x0000D5DC File Offset: 0x0000B7DC
		public float dampingRatio
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<FixedJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return FixedJoint2D.get_dampingRatio_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<FixedJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				FixedJoint2D.set_dampingRatio_Injected(intPtr, value);
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060005F7 RID: 1527 RVA: 0x0000D600 File Offset: 0x0000B800
		// (set) Token: 0x060005F8 RID: 1528 RVA: 0x0000D624 File Offset: 0x0000B824
		public float frequency
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<FixedJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return FixedJoint2D.get_frequency_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<FixedJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				FixedJoint2D.set_frequency_Injected(intPtr, value);
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060005F9 RID: 1529 RVA: 0x0000D648 File Offset: 0x0000B848
		public float referenceAngle
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<FixedJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return FixedJoint2D.get_referenceAngle_Injected(intPtr);
			}
		}

		// Token: 0x060005FB RID: 1531
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_dampingRatio_Injected(IntPtr _unity_self);

		// Token: 0x060005FC RID: 1532
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_dampingRatio_Injected(IntPtr _unity_self, float value);

		// Token: 0x060005FD RID: 1533
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_frequency_Injected(IntPtr _unity_self);

		// Token: 0x060005FE RID: 1534
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_frequency_Injected(IntPtr _unity_self, float value);

		// Token: 0x060005FF RID: 1535
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_referenceAngle_Injected(IntPtr _unity_self);
	}
}
