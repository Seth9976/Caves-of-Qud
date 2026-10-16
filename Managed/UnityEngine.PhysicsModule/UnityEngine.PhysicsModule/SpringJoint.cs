using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000054 RID: 84
	[NativeClass("Unity::SpringJoint")]
	[RequireComponent(typeof(Rigidbody))]
	[NativeHeader("Modules/Physics/SpringJoint.h")]
	public class SpringJoint : Joint
	{
		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000671 RID: 1649 RVA: 0x0000BC24 File Offset: 0x00009E24
		// (set) Token: 0x06000672 RID: 1650 RVA: 0x0000BC48 File Offset: 0x00009E48
		public float spring
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpringJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SpringJoint.get_spring_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpringJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SpringJoint.set_spring_Injected(intPtr, value);
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000673 RID: 1651 RVA: 0x0000BC6C File Offset: 0x00009E6C
		// (set) Token: 0x06000674 RID: 1652 RVA: 0x0000BC90 File Offset: 0x00009E90
		public float damper
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpringJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SpringJoint.get_damper_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpringJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SpringJoint.set_damper_Injected(intPtr, value);
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000675 RID: 1653 RVA: 0x0000BCB4 File Offset: 0x00009EB4
		// (set) Token: 0x06000676 RID: 1654 RVA: 0x0000BCD8 File Offset: 0x00009ED8
		public float minDistance
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpringJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SpringJoint.get_minDistance_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpringJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SpringJoint.set_minDistance_Injected(intPtr, value);
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000677 RID: 1655 RVA: 0x0000BCFC File Offset: 0x00009EFC
		// (set) Token: 0x06000678 RID: 1656 RVA: 0x0000BD20 File Offset: 0x00009F20
		public float maxDistance
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpringJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SpringJoint.get_maxDistance_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpringJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SpringJoint.set_maxDistance_Injected(intPtr, value);
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000679 RID: 1657 RVA: 0x0000BD44 File Offset: 0x00009F44
		// (set) Token: 0x0600067A RID: 1658 RVA: 0x0000BD68 File Offset: 0x00009F68
		public float tolerance
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpringJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SpringJoint.get_tolerance_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SpringJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SpringJoint.set_tolerance_Injected(intPtr, value);
			}
		}

		// Token: 0x0600067C RID: 1660
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_spring_Injected(IntPtr _unity_self);

		// Token: 0x0600067D RID: 1661
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_spring_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600067E RID: 1662
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_damper_Injected(IntPtr _unity_self);

		// Token: 0x0600067F RID: 1663
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_damper_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000680 RID: 1664
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_minDistance_Injected(IntPtr _unity_self);

		// Token: 0x06000681 RID: 1665
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_minDistance_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000682 RID: 1666
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_maxDistance_Injected(IntPtr _unity_self);

		// Token: 0x06000683 RID: 1667
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_maxDistance_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000684 RID: 1668
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_tolerance_Injected(IntPtr _unity_self);

		// Token: 0x06000685 RID: 1669
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_tolerance_Injected(IntPtr _unity_self, float value);
	}
}
