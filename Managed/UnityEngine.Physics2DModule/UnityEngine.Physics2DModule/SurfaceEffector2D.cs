using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000040 RID: 64
	[NativeHeader("Modules/Physics2D/SurfaceEffector2D.h")]
	public class SurfaceEffector2D : Effector2D
	{
		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060006A8 RID: 1704 RVA: 0x0000E1E0 File Offset: 0x0000C3E0
		// (set) Token: 0x060006A9 RID: 1705 RVA: 0x0000E204 File Offset: 0x0000C404
		public float speed
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SurfaceEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SurfaceEffector2D.get_speed_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SurfaceEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SurfaceEffector2D.set_speed_Injected(intPtr, value);
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060006AA RID: 1706 RVA: 0x0000E228 File Offset: 0x0000C428
		// (set) Token: 0x060006AB RID: 1707 RVA: 0x0000E24C File Offset: 0x0000C44C
		public float speedVariation
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SurfaceEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SurfaceEffector2D.get_speedVariation_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SurfaceEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SurfaceEffector2D.set_speedVariation_Injected(intPtr, value);
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060006AC RID: 1708 RVA: 0x0000E270 File Offset: 0x0000C470
		// (set) Token: 0x060006AD RID: 1709 RVA: 0x0000E294 File Offset: 0x0000C494
		public float forceScale
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SurfaceEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SurfaceEffector2D.get_forceScale_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SurfaceEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SurfaceEffector2D.set_forceScale_Injected(intPtr, value);
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060006AE RID: 1710 RVA: 0x0000E2B8 File Offset: 0x0000C4B8
		// (set) Token: 0x060006AF RID: 1711 RVA: 0x0000E2DC File Offset: 0x0000C4DC
		public bool useContactForce
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SurfaceEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SurfaceEffector2D.get_useContactForce_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SurfaceEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SurfaceEffector2D.set_useContactForce_Injected(intPtr, value);
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060006B0 RID: 1712 RVA: 0x0000E300 File Offset: 0x0000C500
		// (set) Token: 0x060006B1 RID: 1713 RVA: 0x0000E324 File Offset: 0x0000C524
		public bool useFriction
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SurfaceEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SurfaceEffector2D.get_useFriction_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SurfaceEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SurfaceEffector2D.set_useFriction_Injected(intPtr, value);
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060006B2 RID: 1714 RVA: 0x0000E348 File Offset: 0x0000C548
		// (set) Token: 0x060006B3 RID: 1715 RVA: 0x0000E36C File Offset: 0x0000C56C
		public bool useBounce
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SurfaceEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SurfaceEffector2D.get_useBounce_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SurfaceEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SurfaceEffector2D.set_useBounce_Injected(intPtr, value);
			}
		}

		// Token: 0x060006B5 RID: 1717
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_speed_Injected(IntPtr _unity_self);

		// Token: 0x060006B6 RID: 1718
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_speed_Injected(IntPtr _unity_self, float value);

		// Token: 0x060006B7 RID: 1719
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_speedVariation_Injected(IntPtr _unity_self);

		// Token: 0x060006B8 RID: 1720
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_speedVariation_Injected(IntPtr _unity_self, float value);

		// Token: 0x060006B9 RID: 1721
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_forceScale_Injected(IntPtr _unity_self);

		// Token: 0x060006BA RID: 1722
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_forceScale_Injected(IntPtr _unity_self, float value);

		// Token: 0x060006BB RID: 1723
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_useContactForce_Injected(IntPtr _unity_self);

		// Token: 0x060006BC RID: 1724
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_useContactForce_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060006BD RID: 1725
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_useFriction_Injected(IntPtr _unity_self);

		// Token: 0x060006BE RID: 1726
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_useFriction_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060006BF RID: 1727
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_useBounce_Injected(IntPtr _unity_self);

		// Token: 0x060006C0 RID: 1728
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_useBounce_Injected(IntPtr _unity_self, bool value);
	}
}
