using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200003D RID: 61
	[NativeHeader("Modules/Physics2D/BuoyancyEffector2D.h")]
	public class BuoyancyEffector2D : Effector2D
	{
		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000645 RID: 1605 RVA: 0x0000DB20 File Offset: 0x0000BD20
		// (set) Token: 0x06000646 RID: 1606 RVA: 0x0000DB44 File Offset: 0x0000BD44
		public float surfaceLevel
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<BuoyancyEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return BuoyancyEffector2D.get_surfaceLevel_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<BuoyancyEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BuoyancyEffector2D.set_surfaceLevel_Injected(intPtr, value);
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000647 RID: 1607 RVA: 0x0000DB68 File Offset: 0x0000BD68
		// (set) Token: 0x06000648 RID: 1608 RVA: 0x0000DB8C File Offset: 0x0000BD8C
		public float density
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<BuoyancyEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return BuoyancyEffector2D.get_density_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<BuoyancyEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BuoyancyEffector2D.set_density_Injected(intPtr, value);
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000649 RID: 1609 RVA: 0x0000DBB0 File Offset: 0x0000BDB0
		// (set) Token: 0x0600064A RID: 1610 RVA: 0x0000DBD4 File Offset: 0x0000BDD4
		public float linearDrag
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<BuoyancyEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return BuoyancyEffector2D.get_linearDrag_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<BuoyancyEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BuoyancyEffector2D.set_linearDrag_Injected(intPtr, value);
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600064B RID: 1611 RVA: 0x0000DBF8 File Offset: 0x0000BDF8
		// (set) Token: 0x0600064C RID: 1612 RVA: 0x0000DC1C File Offset: 0x0000BE1C
		public float angularDrag
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<BuoyancyEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return BuoyancyEffector2D.get_angularDrag_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<BuoyancyEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BuoyancyEffector2D.set_angularDrag_Injected(intPtr, value);
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600064D RID: 1613 RVA: 0x0000DC40 File Offset: 0x0000BE40
		// (set) Token: 0x0600064E RID: 1614 RVA: 0x0000DC64 File Offset: 0x0000BE64
		public float flowAngle
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<BuoyancyEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return BuoyancyEffector2D.get_flowAngle_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<BuoyancyEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BuoyancyEffector2D.set_flowAngle_Injected(intPtr, value);
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600064F RID: 1615 RVA: 0x0000DC88 File Offset: 0x0000BE88
		// (set) Token: 0x06000650 RID: 1616 RVA: 0x0000DCAC File Offset: 0x0000BEAC
		public float flowMagnitude
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<BuoyancyEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return BuoyancyEffector2D.get_flowMagnitude_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<BuoyancyEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BuoyancyEffector2D.set_flowMagnitude_Injected(intPtr, value);
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000651 RID: 1617 RVA: 0x0000DCD0 File Offset: 0x0000BED0
		// (set) Token: 0x06000652 RID: 1618 RVA: 0x0000DCF4 File Offset: 0x0000BEF4
		public float flowVariation
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<BuoyancyEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return BuoyancyEffector2D.get_flowVariation_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<BuoyancyEffector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BuoyancyEffector2D.set_flowVariation_Injected(intPtr, value);
			}
		}

		// Token: 0x06000654 RID: 1620
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_surfaceLevel_Injected(IntPtr _unity_self);

		// Token: 0x06000655 RID: 1621
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_surfaceLevel_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000656 RID: 1622
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_density_Injected(IntPtr _unity_self);

		// Token: 0x06000657 RID: 1623
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_density_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000658 RID: 1624
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_linearDrag_Injected(IntPtr _unity_self);

		// Token: 0x06000659 RID: 1625
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_linearDrag_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600065A RID: 1626
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_angularDrag_Injected(IntPtr _unity_self);

		// Token: 0x0600065B RID: 1627
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_angularDrag_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600065C RID: 1628
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_flowAngle_Injected(IntPtr _unity_self);

		// Token: 0x0600065D RID: 1629
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_flowAngle_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600065E RID: 1630
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_flowMagnitude_Injected(IntPtr _unity_self);

		// Token: 0x0600065F RID: 1631
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_flowMagnitude_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000660 RID: 1632
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_flowVariation_Injected(IntPtr _unity_self);

		// Token: 0x06000661 RID: 1633
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_flowVariation_Injected(IntPtr _unity_self, float value);
	}
}
