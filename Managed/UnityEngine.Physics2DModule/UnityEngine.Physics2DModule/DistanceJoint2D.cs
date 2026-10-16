using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000033 RID: 51
	[NativeHeader("Modules/Physics2D/DistanceJoint2D.h")]
	public sealed class DistanceJoint2D : AnchoredJoint2D
	{
		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000569 RID: 1385 RVA: 0x0000CC2C File Offset: 0x0000AE2C
		// (set) Token: 0x0600056A RID: 1386 RVA: 0x0000CC50 File Offset: 0x0000AE50
		public bool autoConfigureDistance
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<DistanceJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return DistanceJoint2D.get_autoConfigureDistance_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<DistanceJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				DistanceJoint2D.set_autoConfigureDistance_Injected(intPtr, value);
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x0000CC74 File Offset: 0x0000AE74
		// (set) Token: 0x0600056C RID: 1388 RVA: 0x0000CC98 File Offset: 0x0000AE98
		public float distance
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<DistanceJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return DistanceJoint2D.get_distance_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<DistanceJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				DistanceJoint2D.set_distance_Injected(intPtr, value);
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600056D RID: 1389 RVA: 0x0000CCBC File Offset: 0x0000AEBC
		// (set) Token: 0x0600056E RID: 1390 RVA: 0x0000CCE0 File Offset: 0x0000AEE0
		public bool maxDistanceOnly
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<DistanceJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return DistanceJoint2D.get_maxDistanceOnly_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<DistanceJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				DistanceJoint2D.set_maxDistanceOnly_Injected(intPtr, value);
			}
		}

		// Token: 0x06000570 RID: 1392
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_autoConfigureDistance_Injected(IntPtr _unity_self);

		// Token: 0x06000571 RID: 1393
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_autoConfigureDistance_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000572 RID: 1394
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_distance_Injected(IntPtr _unity_self);

		// Token: 0x06000573 RID: 1395
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_distance_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000574 RID: 1396
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_maxDistanceOnly_Injected(IntPtr _unity_self);

		// Token: 0x06000575 RID: 1397
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_maxDistanceOnly_Injected(IntPtr _unity_self, bool value);
	}
}
