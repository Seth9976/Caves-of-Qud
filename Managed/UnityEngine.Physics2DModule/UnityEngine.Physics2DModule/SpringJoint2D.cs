using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000032 RID: 50
	[NativeHeader("Modules/Physics2D/SpringJoint2D.h")]
	public sealed class SpringJoint2D : AnchoredJoint2D
	{
		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x0000CB04 File Offset: 0x0000AD04
		// (set) Token: 0x06000559 RID: 1369 RVA: 0x0000CB28 File Offset: 0x0000AD28
		public bool autoConfigureDistance
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpringJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SpringJoint2D.get_autoConfigureDistance_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpringJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SpringJoint2D.set_autoConfigureDistance_Injected(intPtr, value);
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600055A RID: 1370 RVA: 0x0000CB4C File Offset: 0x0000AD4C
		// (set) Token: 0x0600055B RID: 1371 RVA: 0x0000CB70 File Offset: 0x0000AD70
		public float distance
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpringJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SpringJoint2D.get_distance_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpringJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SpringJoint2D.set_distance_Injected(intPtr, value);
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600055C RID: 1372 RVA: 0x0000CB94 File Offset: 0x0000AD94
		// (set) Token: 0x0600055D RID: 1373 RVA: 0x0000CBB8 File Offset: 0x0000ADB8
		public float dampingRatio
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpringJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SpringJoint2D.get_dampingRatio_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpringJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SpringJoint2D.set_dampingRatio_Injected(intPtr, value);
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600055E RID: 1374 RVA: 0x0000CBDC File Offset: 0x0000ADDC
		// (set) Token: 0x0600055F RID: 1375 RVA: 0x0000CC00 File Offset: 0x0000AE00
		public float frequency
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpringJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SpringJoint2D.get_frequency_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpringJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SpringJoint2D.set_frequency_Injected(intPtr, value);
			}
		}

		// Token: 0x06000561 RID: 1377
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_autoConfigureDistance_Injected(IntPtr _unity_self);

		// Token: 0x06000562 RID: 1378
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_autoConfigureDistance_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000563 RID: 1379
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_distance_Injected(IntPtr _unity_self);

		// Token: 0x06000564 RID: 1380
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_distance_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000565 RID: 1381
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_dampingRatio_Injected(IntPtr _unity_self);

		// Token: 0x06000566 RID: 1382
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_dampingRatio_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000567 RID: 1383
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_frequency_Injected(IntPtr _unity_self);

		// Token: 0x06000568 RID: 1384
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_frequency_Injected(IntPtr _unity_self, float value);
	}
}
