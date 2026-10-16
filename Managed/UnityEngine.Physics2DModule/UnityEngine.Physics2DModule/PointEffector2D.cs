using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200003E RID: 62
	[NativeHeader("Modules/Physics2D/PointEffector2D.h")]
	public class PointEffector2D : Effector2D
	{
		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000662 RID: 1634 RVA: 0x0000DD18 File Offset: 0x0000BF18
		// (set) Token: 0x06000663 RID: 1635 RVA: 0x0000DD3C File Offset: 0x0000BF3C
		public float forceMagnitude
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PointEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return PointEffector2D.get_forceMagnitude_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PointEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				PointEffector2D.set_forceMagnitude_Injected(intPtr, value);
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000664 RID: 1636 RVA: 0x0000DD60 File Offset: 0x0000BF60
		// (set) Token: 0x06000665 RID: 1637 RVA: 0x0000DD84 File Offset: 0x0000BF84
		public float forceVariation
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PointEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return PointEffector2D.get_forceVariation_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PointEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				PointEffector2D.set_forceVariation_Injected(intPtr, value);
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000666 RID: 1638 RVA: 0x0000DDA8 File Offset: 0x0000BFA8
		// (set) Token: 0x06000667 RID: 1639 RVA: 0x0000DDCC File Offset: 0x0000BFCC
		public float distanceScale
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PointEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return PointEffector2D.get_distanceScale_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PointEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				PointEffector2D.set_distanceScale_Injected(intPtr, value);
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000668 RID: 1640 RVA: 0x0000DDF0 File Offset: 0x0000BFF0
		// (set) Token: 0x06000669 RID: 1641 RVA: 0x0000DE14 File Offset: 0x0000C014
		public float drag
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PointEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return PointEffector2D.get_drag_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PointEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				PointEffector2D.set_drag_Injected(intPtr, value);
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x0600066A RID: 1642 RVA: 0x0000DE38 File Offset: 0x0000C038
		// (set) Token: 0x0600066B RID: 1643 RVA: 0x0000DE5C File Offset: 0x0000C05C
		public float angularDrag
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PointEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return PointEffector2D.get_angularDrag_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PointEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				PointEffector2D.set_angularDrag_Injected(intPtr, value);
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x0600066C RID: 1644 RVA: 0x0000DE80 File Offset: 0x0000C080
		// (set) Token: 0x0600066D RID: 1645 RVA: 0x0000DEA4 File Offset: 0x0000C0A4
		public EffectorSelection2D forceSource
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PointEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return PointEffector2D.get_forceSource_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PointEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				PointEffector2D.set_forceSource_Injected(intPtr, value);
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x0600066E RID: 1646 RVA: 0x0000DEC8 File Offset: 0x0000C0C8
		// (set) Token: 0x0600066F RID: 1647 RVA: 0x0000DEEC File Offset: 0x0000C0EC
		public EffectorSelection2D forceTarget
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PointEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return PointEffector2D.get_forceTarget_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PointEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				PointEffector2D.set_forceTarget_Injected(intPtr, value);
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000670 RID: 1648 RVA: 0x0000DF10 File Offset: 0x0000C110
		// (set) Token: 0x06000671 RID: 1649 RVA: 0x0000DF34 File Offset: 0x0000C134
		public EffectorForceMode2D forceMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PointEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return PointEffector2D.get_forceMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PointEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				PointEffector2D.set_forceMode_Injected(intPtr, value);
			}
		}

		// Token: 0x06000673 RID: 1651
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_forceMagnitude_Injected(IntPtr _unity_self);

		// Token: 0x06000674 RID: 1652
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_forceMagnitude_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000675 RID: 1653
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_forceVariation_Injected(IntPtr _unity_self);

		// Token: 0x06000676 RID: 1654
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_forceVariation_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000677 RID: 1655
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_distanceScale_Injected(IntPtr _unity_self);

		// Token: 0x06000678 RID: 1656
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_distanceScale_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000679 RID: 1657
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_drag_Injected(IntPtr _unity_self);

		// Token: 0x0600067A RID: 1658
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_drag_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600067B RID: 1659
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_angularDrag_Injected(IntPtr _unity_self);

		// Token: 0x0600067C RID: 1660
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_angularDrag_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600067D RID: 1661
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern EffectorSelection2D get_forceSource_Injected(IntPtr _unity_self);

		// Token: 0x0600067E RID: 1662
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_forceSource_Injected(IntPtr _unity_self, EffectorSelection2D value);

		// Token: 0x0600067F RID: 1663
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern EffectorSelection2D get_forceTarget_Injected(IntPtr _unity_self);

		// Token: 0x06000680 RID: 1664
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_forceTarget_Injected(IntPtr _unity_self, EffectorSelection2D value);

		// Token: 0x06000681 RID: 1665
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern EffectorForceMode2D get_forceMode_Injected(IntPtr _unity_self);

		// Token: 0x06000682 RID: 1666
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_forceMode_Injected(IntPtr _unity_self, EffectorForceMode2D value);
	}
}
