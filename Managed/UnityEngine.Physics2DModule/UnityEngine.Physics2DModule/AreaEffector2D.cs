using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200003C RID: 60
	[NativeHeader("Modules/Physics2D/AreaEffector2D.h")]
	public class AreaEffector2D : Effector2D
	{
		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000626 RID: 1574 RVA: 0x0000D8FC File Offset: 0x0000BAFC
		// (set) Token: 0x06000627 RID: 1575 RVA: 0x0000D920 File Offset: 0x0000BB20
		public float forceAngle
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AreaEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AreaEffector2D.get_forceAngle_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AreaEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AreaEffector2D.set_forceAngle_Injected(intPtr, value);
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000628 RID: 1576 RVA: 0x0000D944 File Offset: 0x0000BB44
		// (set) Token: 0x06000629 RID: 1577 RVA: 0x0000D968 File Offset: 0x0000BB68
		public bool useGlobalAngle
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AreaEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AreaEffector2D.get_useGlobalAngle_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AreaEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AreaEffector2D.set_useGlobalAngle_Injected(intPtr, value);
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600062A RID: 1578 RVA: 0x0000D98C File Offset: 0x0000BB8C
		// (set) Token: 0x0600062B RID: 1579 RVA: 0x0000D9B0 File Offset: 0x0000BBB0
		public float forceMagnitude
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AreaEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AreaEffector2D.get_forceMagnitude_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AreaEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AreaEffector2D.set_forceMagnitude_Injected(intPtr, value);
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x0000D9D4 File Offset: 0x0000BBD4
		// (set) Token: 0x0600062D RID: 1581 RVA: 0x0000D9F8 File Offset: 0x0000BBF8
		public float forceVariation
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AreaEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AreaEffector2D.get_forceVariation_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AreaEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AreaEffector2D.set_forceVariation_Injected(intPtr, value);
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x0000DA1C File Offset: 0x0000BC1C
		// (set) Token: 0x0600062F RID: 1583 RVA: 0x0000DA40 File Offset: 0x0000BC40
		public float drag
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AreaEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AreaEffector2D.get_drag_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AreaEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AreaEffector2D.set_drag_Injected(intPtr, value);
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000630 RID: 1584 RVA: 0x0000DA64 File Offset: 0x0000BC64
		// (set) Token: 0x06000631 RID: 1585 RVA: 0x0000DA88 File Offset: 0x0000BC88
		public float angularDrag
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AreaEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AreaEffector2D.get_angularDrag_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AreaEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AreaEffector2D.set_angularDrag_Injected(intPtr, value);
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000632 RID: 1586 RVA: 0x0000DAAC File Offset: 0x0000BCAC
		// (set) Token: 0x06000633 RID: 1587 RVA: 0x0000DAD0 File Offset: 0x0000BCD0
		public EffectorSelection2D forceTarget
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AreaEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AreaEffector2D.get_forceTarget_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AreaEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AreaEffector2D.set_forceTarget_Injected(intPtr, value);
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x0000DAF4 File Offset: 0x0000BCF4
		// (set) Token: 0x06000635 RID: 1589 RVA: 0x0000DB0C File Offset: 0x0000BD0C
		[Obsolete("AreaEffector2D.forceDirection has been deprecated. Use AreaEffector2D.forceAngle instead (UnityUpgradable) -> forceAngle", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float forceDirection
		{
			get
			{
				return this.forceAngle;
			}
			set
			{
				this.forceAngle = value;
			}
		}

		// Token: 0x06000637 RID: 1591
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_forceAngle_Injected(IntPtr _unity_self);

		// Token: 0x06000638 RID: 1592
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_forceAngle_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000639 RID: 1593
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_useGlobalAngle_Injected(IntPtr _unity_self);

		// Token: 0x0600063A RID: 1594
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_useGlobalAngle_Injected(IntPtr _unity_self, bool value);

		// Token: 0x0600063B RID: 1595
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_forceMagnitude_Injected(IntPtr _unity_self);

		// Token: 0x0600063C RID: 1596
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_forceMagnitude_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600063D RID: 1597
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_forceVariation_Injected(IntPtr _unity_self);

		// Token: 0x0600063E RID: 1598
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_forceVariation_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600063F RID: 1599
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_drag_Injected(IntPtr _unity_self);

		// Token: 0x06000640 RID: 1600
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_drag_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000641 RID: 1601
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_angularDrag_Injected(IntPtr _unity_self);

		// Token: 0x06000642 RID: 1602
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_angularDrag_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000643 RID: 1603
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern EffectorSelection2D get_forceTarget_Injected(IntPtr _unity_self);

		// Token: 0x06000644 RID: 1604
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_forceTarget_Injected(IntPtr _unity_self, EffectorSelection2D value);
	}
}
