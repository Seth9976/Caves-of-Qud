using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000030 RID: 48
	[RequireComponent(typeof(Transform), typeof(Rigidbody2D))]
	[NativeHeader("Modules/Physics2D/Joint2D.h")]
	public class Joint2D : Behaviour
	{
		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600052A RID: 1322 RVA: 0x0000C7C8 File Offset: 0x0000A9C8
		public Rigidbody2D attachedRigidbody
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Joint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Rigidbody2D>(Joint2D.get_attachedRigidbody_Injected(intPtr));
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x0000C7F0 File Offset: 0x0000A9F0
		// (set) Token: 0x0600052C RID: 1324 RVA: 0x0000C818 File Offset: 0x0000AA18
		public Rigidbody2D connectedBody
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Joint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Rigidbody2D>(Joint2D.get_connectedBody_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Joint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Joint2D.set_connectedBody_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Rigidbody2D>(value));
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x0000C840 File Offset: 0x0000AA40
		// (set) Token: 0x0600052E RID: 1326 RVA: 0x0000C864 File Offset: 0x0000AA64
		public bool enableCollision
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Joint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Joint2D.get_enableCollision_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Joint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Joint2D.set_enableCollision_Injected(intPtr, value);
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x0000C888 File Offset: 0x0000AA88
		// (set) Token: 0x06000530 RID: 1328 RVA: 0x0000C8AC File Offset: 0x0000AAAC
		public float breakForce
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Joint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Joint2D.get_breakForce_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Joint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Joint2D.set_breakForce_Injected(intPtr, value);
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x0000C8D0 File Offset: 0x0000AAD0
		// (set) Token: 0x06000532 RID: 1330 RVA: 0x0000C8F4 File Offset: 0x0000AAF4
		public float breakTorque
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Joint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Joint2D.get_breakTorque_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Joint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Joint2D.set_breakTorque_Injected(intPtr, value);
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x0000C918 File Offset: 0x0000AB18
		// (set) Token: 0x06000534 RID: 1332 RVA: 0x0000C93C File Offset: 0x0000AB3C
		public JointBreakAction2D breakAction
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Joint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Joint2D.get_breakAction_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Joint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Joint2D.set_breakAction_Injected(intPtr, value);
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x0000C960 File Offset: 0x0000AB60
		public Vector2 reactionForce
		{
			[NativeMethod("GetReactionForceFixedTime")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Joint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				Joint2D.get_reactionForce_Injected(intPtr, out vector);
				return vector;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x0000C988 File Offset: 0x0000AB88
		public float reactionTorque
		{
			[NativeMethod("GetReactionTorqueFixedTime")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Joint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Joint2D.get_reactionTorque_Injected(intPtr);
			}
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x0000C9AC File Offset: 0x0000ABAC
		public Vector2 GetReactionForce(float timeStep)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Joint2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector2 vector;
			Joint2D.GetReactionForce_Injected(intPtr, timeStep, out vector);
			return vector;
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0000C9D4 File Offset: 0x0000ABD4
		public float GetReactionTorque(float timeStep)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Joint2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Joint2D.GetReactionTorque_Injected(intPtr, timeStep);
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x0000C9F8 File Offset: 0x0000ABF8
		// (set) Token: 0x0600053A RID: 1338 RVA: 0x0000CA10 File Offset: 0x0000AC10
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Joint2D.collideConnected has been deprecated. Use Joint2D.enableCollision instead (UnityUpgradable) -> enableCollision", true)]
		public bool collideConnected
		{
			get
			{
				return this.enableCollision;
			}
			set
			{
				this.enableCollision = value;
			}
		}

		// Token: 0x0600053C RID: 1340
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_attachedRigidbody_Injected(IntPtr _unity_self);

		// Token: 0x0600053D RID: 1341
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_connectedBody_Injected(IntPtr _unity_self);

		// Token: 0x0600053E RID: 1342
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_connectedBody_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x0600053F RID: 1343
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_enableCollision_Injected(IntPtr _unity_self);

		// Token: 0x06000540 RID: 1344
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_enableCollision_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000541 RID: 1345
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_breakForce_Injected(IntPtr _unity_self);

		// Token: 0x06000542 RID: 1346
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_breakForce_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000543 RID: 1347
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_breakTorque_Injected(IntPtr _unity_self);

		// Token: 0x06000544 RID: 1348
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_breakTorque_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000545 RID: 1349
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern JointBreakAction2D get_breakAction_Injected(IntPtr _unity_self);

		// Token: 0x06000546 RID: 1350
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_breakAction_Injected(IntPtr _unity_self, JointBreakAction2D value);

		// Token: 0x06000547 RID: 1351
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_reactionForce_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x06000548 RID: 1352
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_reactionTorque_Injected(IntPtr _unity_self);

		// Token: 0x06000549 RID: 1353
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetReactionForce_Injected(IntPtr _unity_self, float timeStep, out Vector2 ret);

		// Token: 0x0600054A RID: 1354
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetReactionTorque_Injected(IntPtr _unity_self, float timeStep);
	}
}
