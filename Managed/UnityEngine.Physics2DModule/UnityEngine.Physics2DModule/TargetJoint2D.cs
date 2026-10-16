using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000038 RID: 56
	[NativeHeader("Modules/Physics2D/TargetJoint2D.h")]
	public sealed class TargetJoint2D : Joint2D
	{
		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060005DC RID: 1500 RVA: 0x0000D400 File Offset: 0x0000B600
		// (set) Token: 0x060005DD RID: 1501 RVA: 0x0000D428 File Offset: 0x0000B628
		public Vector2 anchor
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TargetJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				TargetJoint2D.get_anchor_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TargetJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TargetJoint2D.set_anchor_Injected(intPtr, ref value);
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060005DE RID: 1502 RVA: 0x0000D44C File Offset: 0x0000B64C
		// (set) Token: 0x060005DF RID: 1503 RVA: 0x0000D474 File Offset: 0x0000B674
		public Vector2 target
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TargetJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				TargetJoint2D.get_target_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TargetJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TargetJoint2D.set_target_Injected(intPtr, ref value);
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x0000D498 File Offset: 0x0000B698
		// (set) Token: 0x060005E1 RID: 1505 RVA: 0x0000D4BC File Offset: 0x0000B6BC
		public bool autoConfigureTarget
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TargetJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TargetJoint2D.get_autoConfigureTarget_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TargetJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TargetJoint2D.set_autoConfigureTarget_Injected(intPtr, value);
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060005E2 RID: 1506 RVA: 0x0000D4E0 File Offset: 0x0000B6E0
		// (set) Token: 0x060005E3 RID: 1507 RVA: 0x0000D504 File Offset: 0x0000B704
		public float maxForce
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TargetJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TargetJoint2D.get_maxForce_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TargetJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TargetJoint2D.set_maxForce_Injected(intPtr, value);
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060005E4 RID: 1508 RVA: 0x0000D528 File Offset: 0x0000B728
		// (set) Token: 0x060005E5 RID: 1509 RVA: 0x0000D54C File Offset: 0x0000B74C
		public float dampingRatio
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TargetJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TargetJoint2D.get_dampingRatio_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TargetJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TargetJoint2D.set_dampingRatio_Injected(intPtr, value);
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x0000D570 File Offset: 0x0000B770
		// (set) Token: 0x060005E7 RID: 1511 RVA: 0x0000D594 File Offset: 0x0000B794
		public float frequency
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TargetJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TargetJoint2D.get_frequency_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TargetJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TargetJoint2D.set_frequency_Injected(intPtr, value);
			}
		}

		// Token: 0x060005E9 RID: 1513
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_anchor_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x060005EA RID: 1514
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_anchor_Injected(IntPtr _unity_self, [In] ref Vector2 value);

		// Token: 0x060005EB RID: 1515
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_target_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x060005EC RID: 1516
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_target_Injected(IntPtr _unity_self, [In] ref Vector2 value);

		// Token: 0x060005ED RID: 1517
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_autoConfigureTarget_Injected(IntPtr _unity_self);

		// Token: 0x060005EE RID: 1518
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_autoConfigureTarget_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060005EF RID: 1519
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_maxForce_Injected(IntPtr _unity_self);

		// Token: 0x060005F0 RID: 1520
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_maxForce_Injected(IntPtr _unity_self, float value);

		// Token: 0x060005F1 RID: 1521
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_dampingRatio_Injected(IntPtr _unity_self);

		// Token: 0x060005F2 RID: 1522
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_dampingRatio_Injected(IntPtr _unity_self, float value);

		// Token: 0x060005F3 RID: 1523
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_frequency_Injected(IntPtr _unity_self);

		// Token: 0x060005F4 RID: 1524
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_frequency_Injected(IntPtr _unity_self, float value);
	}
}
