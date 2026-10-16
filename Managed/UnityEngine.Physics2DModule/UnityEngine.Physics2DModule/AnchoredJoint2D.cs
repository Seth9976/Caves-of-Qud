using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000031 RID: 49
	[NativeHeader("Modules/Physics2D/AnchoredJoint2D.h")]
	public class AnchoredJoint2D : Joint2D
	{
		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600054B RID: 1355 RVA: 0x0000CA1C File Offset: 0x0000AC1C
		// (set) Token: 0x0600054C RID: 1356 RVA: 0x0000CA44 File Offset: 0x0000AC44
		public Vector2 anchor
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnchoredJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				AnchoredJoint2D.get_anchor_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnchoredJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AnchoredJoint2D.set_anchor_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x0000CA68 File Offset: 0x0000AC68
		// (set) Token: 0x0600054E RID: 1358 RVA: 0x0000CA90 File Offset: 0x0000AC90
		public Vector2 connectedAnchor
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnchoredJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				AnchoredJoint2D.get_connectedAnchor_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnchoredJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AnchoredJoint2D.set_connectedAnchor_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x0000CAB4 File Offset: 0x0000ACB4
		// (set) Token: 0x06000550 RID: 1360 RVA: 0x0000CAD8 File Offset: 0x0000ACD8
		public bool autoConfigureConnectedAnchor
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnchoredJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AnchoredJoint2D.get_autoConfigureConnectedAnchor_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnchoredJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AnchoredJoint2D.set_autoConfigureConnectedAnchor_Injected(intPtr, value);
			}
		}

		// Token: 0x06000552 RID: 1362
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_anchor_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x06000553 RID: 1363
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_anchor_Injected(IntPtr _unity_self, [In] ref Vector2 value);

		// Token: 0x06000554 RID: 1364
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_connectedAnchor_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x06000555 RID: 1365
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_connectedAnchor_Injected(IntPtr _unity_self, [In] ref Vector2 value);

		// Token: 0x06000556 RID: 1366
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_autoConfigureConnectedAnchor_Injected(IntPtr _unity_self);

		// Token: 0x06000557 RID: 1367
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_autoConfigureConnectedAnchor_Injected(IntPtr _unity_self, bool value);
	}
}
