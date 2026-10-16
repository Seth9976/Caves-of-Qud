using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000034 RID: 52
	[NativeHeader("Modules/Physics2D/FrictionJoint2D.h")]
	public sealed class FrictionJoint2D : AnchoredJoint2D
	{
		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x0000CD04 File Offset: 0x0000AF04
		// (set) Token: 0x06000577 RID: 1399 RVA: 0x0000CD28 File Offset: 0x0000AF28
		public float maxForce
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<FrictionJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return FrictionJoint2D.get_maxForce_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<FrictionJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				FrictionJoint2D.set_maxForce_Injected(intPtr, value);
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x0000CD4C File Offset: 0x0000AF4C
		// (set) Token: 0x06000579 RID: 1401 RVA: 0x0000CD70 File Offset: 0x0000AF70
		public float maxTorque
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<FrictionJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return FrictionJoint2D.get_maxTorque_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<FrictionJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				FrictionJoint2D.set_maxTorque_Injected(intPtr, value);
			}
		}

		// Token: 0x0600057B RID: 1403
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_maxForce_Injected(IntPtr _unity_self);

		// Token: 0x0600057C RID: 1404
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_maxForce_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600057D RID: 1405
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_maxTorque_Injected(IntPtr _unity_self);

		// Token: 0x0600057E RID: 1406
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_maxTorque_Injected(IntPtr _unity_self, float value);
	}
}
