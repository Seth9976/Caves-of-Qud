using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000036 RID: 54
	[NativeHeader("Modules/Physics2D/RelativeJoint2D.h")]
	public sealed class RelativeJoint2D : Joint2D
	{
		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x0000CFB8 File Offset: 0x0000B1B8
		// (set) Token: 0x0600059F RID: 1439 RVA: 0x0000CFDC File Offset: 0x0000B1DC
		public float maxForce
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RelativeJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return RelativeJoint2D.get_maxForce_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RelativeJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RelativeJoint2D.set_maxForce_Injected(intPtr, value);
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060005A0 RID: 1440 RVA: 0x0000D000 File Offset: 0x0000B200
		// (set) Token: 0x060005A1 RID: 1441 RVA: 0x0000D024 File Offset: 0x0000B224
		public float maxTorque
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RelativeJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return RelativeJoint2D.get_maxTorque_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RelativeJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RelativeJoint2D.set_maxTorque_Injected(intPtr, value);
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x0000D048 File Offset: 0x0000B248
		// (set) Token: 0x060005A3 RID: 1443 RVA: 0x0000D06C File Offset: 0x0000B26C
		public float correctionScale
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RelativeJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return RelativeJoint2D.get_correctionScale_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RelativeJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RelativeJoint2D.set_correctionScale_Injected(intPtr, value);
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x0000D090 File Offset: 0x0000B290
		// (set) Token: 0x060005A5 RID: 1445 RVA: 0x0000D0B4 File Offset: 0x0000B2B4
		public bool autoConfigureOffset
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RelativeJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return RelativeJoint2D.get_autoConfigureOffset_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RelativeJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RelativeJoint2D.set_autoConfigureOffset_Injected(intPtr, value);
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x0000D0D8 File Offset: 0x0000B2D8
		// (set) Token: 0x060005A7 RID: 1447 RVA: 0x0000D100 File Offset: 0x0000B300
		public Vector2 linearOffset
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RelativeJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				RelativeJoint2D.get_linearOffset_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RelativeJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RelativeJoint2D.set_linearOffset_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x0000D124 File Offset: 0x0000B324
		// (set) Token: 0x060005A9 RID: 1449 RVA: 0x0000D148 File Offset: 0x0000B348
		public float angularOffset
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RelativeJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return RelativeJoint2D.get_angularOffset_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RelativeJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RelativeJoint2D.set_angularOffset_Injected(intPtr, value);
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x0000D16C File Offset: 0x0000B36C
		public Vector2 target
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RelativeJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				RelativeJoint2D.get_target_Injected(intPtr, out vector);
				return vector;
			}
		}

		// Token: 0x060005AC RID: 1452
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_maxForce_Injected(IntPtr _unity_self);

		// Token: 0x060005AD RID: 1453
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_maxForce_Injected(IntPtr _unity_self, float value);

		// Token: 0x060005AE RID: 1454
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_maxTorque_Injected(IntPtr _unity_self);

		// Token: 0x060005AF RID: 1455
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_maxTorque_Injected(IntPtr _unity_self, float value);

		// Token: 0x060005B0 RID: 1456
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_correctionScale_Injected(IntPtr _unity_self);

		// Token: 0x060005B1 RID: 1457
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_correctionScale_Injected(IntPtr _unity_self, float value);

		// Token: 0x060005B2 RID: 1458
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_autoConfigureOffset_Injected(IntPtr _unity_self);

		// Token: 0x060005B3 RID: 1459
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_autoConfigureOffset_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060005B4 RID: 1460
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_linearOffset_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x060005B5 RID: 1461
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_linearOffset_Injected(IntPtr _unity_self, [In] ref Vector2 value);

		// Token: 0x060005B6 RID: 1462
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_angularOffset_Injected(IntPtr _unity_self);

		// Token: 0x060005B7 RID: 1463
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_angularOffset_Injected(IntPtr _unity_self, float value);

		// Token: 0x060005B8 RID: 1464
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_target_Injected(IntPtr _unity_self, out Vector2 ret);
	}
}
