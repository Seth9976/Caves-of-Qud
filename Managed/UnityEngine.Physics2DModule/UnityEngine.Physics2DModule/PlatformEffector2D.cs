using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200003F RID: 63
	[NativeHeader("Modules/Physics2D/PlatformEffector2D.h")]
	public class PlatformEffector2D : Effector2D
	{
		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000683 RID: 1667 RVA: 0x0000DF58 File Offset: 0x0000C158
		// (set) Token: 0x06000684 RID: 1668 RVA: 0x0000DF7C File Offset: 0x0000C17C
		public bool useOneWay
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlatformEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return PlatformEffector2D.get_useOneWay_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlatformEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				PlatformEffector2D.set_useOneWay_Injected(intPtr, value);
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000685 RID: 1669 RVA: 0x0000DFA0 File Offset: 0x0000C1A0
		// (set) Token: 0x06000686 RID: 1670 RVA: 0x0000DFC4 File Offset: 0x0000C1C4
		public bool useOneWayGrouping
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlatformEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return PlatformEffector2D.get_useOneWayGrouping_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlatformEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				PlatformEffector2D.set_useOneWayGrouping_Injected(intPtr, value);
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x0000DFE8 File Offset: 0x0000C1E8
		// (set) Token: 0x06000688 RID: 1672 RVA: 0x0000E00C File Offset: 0x0000C20C
		public bool useSideFriction
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlatformEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return PlatformEffector2D.get_useSideFriction_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlatformEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				PlatformEffector2D.set_useSideFriction_Injected(intPtr, value);
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000689 RID: 1673 RVA: 0x0000E030 File Offset: 0x0000C230
		// (set) Token: 0x0600068A RID: 1674 RVA: 0x0000E054 File Offset: 0x0000C254
		public bool useSideBounce
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlatformEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return PlatformEffector2D.get_useSideBounce_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlatformEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				PlatformEffector2D.set_useSideBounce_Injected(intPtr, value);
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x0000E078 File Offset: 0x0000C278
		// (set) Token: 0x0600068C RID: 1676 RVA: 0x0000E09C File Offset: 0x0000C29C
		public float surfaceArc
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlatformEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return PlatformEffector2D.get_surfaceArc_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlatformEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				PlatformEffector2D.set_surfaceArc_Injected(intPtr, value);
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600068D RID: 1677 RVA: 0x0000E0C0 File Offset: 0x0000C2C0
		// (set) Token: 0x0600068E RID: 1678 RVA: 0x0000E0E4 File Offset: 0x0000C2E4
		public float sideArc
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlatformEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return PlatformEffector2D.get_sideArc_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlatformEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				PlatformEffector2D.set_sideArc_Injected(intPtr, value);
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x0600068F RID: 1679 RVA: 0x0000E108 File Offset: 0x0000C308
		// (set) Token: 0x06000690 RID: 1680 RVA: 0x0000E12C File Offset: 0x0000C32C
		public float rotationalOffset
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlatformEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return PlatformEffector2D.get_rotationalOffset_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlatformEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				PlatformEffector2D.set_rotationalOffset_Injected(intPtr, value);
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000691 RID: 1681 RVA: 0x0000E150 File Offset: 0x0000C350
		// (set) Token: 0x06000692 RID: 1682 RVA: 0x0000E168 File Offset: 0x0000C368
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("PlatformEffector2D.oneWay has been deprecated. Use PlatformEffector2D.useOneWay instead (UnityUpgradable) -> useOneWay", true)]
		public bool oneWay
		{
			get
			{
				return this.useOneWay;
			}
			set
			{
				this.useOneWay = value;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000693 RID: 1683 RVA: 0x0000E174 File Offset: 0x0000C374
		// (set) Token: 0x06000694 RID: 1684 RVA: 0x0000E18C File Offset: 0x0000C38C
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("PlatformEffector2D.sideFriction has been deprecated. Use PlatformEffector2D.useSideFriction instead (UnityUpgradable) -> useSideFriction", true)]
		public bool sideFriction
		{
			get
			{
				return this.useSideFriction;
			}
			set
			{
				this.useSideFriction = value;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000695 RID: 1685 RVA: 0x0000E198 File Offset: 0x0000C398
		// (set) Token: 0x06000696 RID: 1686 RVA: 0x0000E1B0 File Offset: 0x0000C3B0
		[Obsolete("PlatformEffector2D.sideBounce has been deprecated. Use PlatformEffector2D.useSideBounce instead (UnityUpgradable) -> useSideBounce", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool sideBounce
		{
			get
			{
				return this.useSideBounce;
			}
			set
			{
				this.useSideBounce = value;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x0000E1BC File Offset: 0x0000C3BC
		// (set) Token: 0x06000698 RID: 1688 RVA: 0x0000E1D4 File Offset: 0x0000C3D4
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("PlatformEffector2D.sideAngleVariance has been deprecated. Use PlatformEffector2D.sideArc instead (UnityUpgradable) -> sideArc", true)]
		public float sideAngleVariance
		{
			get
			{
				return this.sideArc;
			}
			set
			{
				this.sideArc = value;
			}
		}

		// Token: 0x0600069A RID: 1690
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_useOneWay_Injected(IntPtr _unity_self);

		// Token: 0x0600069B RID: 1691
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_useOneWay_Injected(IntPtr _unity_self, bool value);

		// Token: 0x0600069C RID: 1692
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_useOneWayGrouping_Injected(IntPtr _unity_self);

		// Token: 0x0600069D RID: 1693
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_useOneWayGrouping_Injected(IntPtr _unity_self, bool value);

		// Token: 0x0600069E RID: 1694
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_useSideFriction_Injected(IntPtr _unity_self);

		// Token: 0x0600069F RID: 1695
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_useSideFriction_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060006A0 RID: 1696
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_useSideBounce_Injected(IntPtr _unity_self);

		// Token: 0x060006A1 RID: 1697
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_useSideBounce_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060006A2 RID: 1698
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_surfaceArc_Injected(IntPtr _unity_self);

		// Token: 0x060006A3 RID: 1699
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_surfaceArc_Injected(IntPtr _unity_self, float value);

		// Token: 0x060006A4 RID: 1700
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_sideArc_Injected(IntPtr _unity_self);

		// Token: 0x060006A5 RID: 1701
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_sideArc_Injected(IntPtr _unity_self, float value);

		// Token: 0x060006A6 RID: 1702
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_rotationalOffset_Injected(IntPtr _unity_self);

		// Token: 0x060006A7 RID: 1703
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_rotationalOffset_Injected(IntPtr _unity_self, float value);
	}
}
