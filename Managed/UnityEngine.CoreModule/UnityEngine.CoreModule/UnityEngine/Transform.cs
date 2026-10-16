using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020002FA RID: 762
	[NativeHeader("Configuration/UnityConfigure.h")]
	[RequiredByNativeCode]
	[NativeHeader("Runtime/Transform/ScriptBindings/TransformScriptBindings.h")]
	[NativeHeader("Runtime/Transform/Transform.h")]
	public class Transform : Component, IEnumerable
	{
		// Token: 0x06002533 RID: 9523 RVA: 0x00047F0A File Offset: 0x0004610A
		protected Transform()
		{
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06002534 RID: 9524 RVA: 0x00047F14 File Offset: 0x00046114
		// (set) Token: 0x06002535 RID: 9525 RVA: 0x00047F3C File Offset: 0x0004613C
		public Vector3 position
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				Transform.get_position_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Transform.set_position_Injected(intPtr, ref value);
			}
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06002536 RID: 9526 RVA: 0x00047F60 File Offset: 0x00046160
		// (set) Token: 0x06002537 RID: 9527 RVA: 0x00047F88 File Offset: 0x00046188
		public Vector3 localPosition
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				Transform.get_localPosition_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Transform.set_localPosition_Injected(intPtr, ref value);
			}
		}

		// Token: 0x06002538 RID: 9528 RVA: 0x00047FAC File Offset: 0x000461AC
		internal Vector3 GetLocalEulerAngles(RotationOrder order)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3 vector;
			Transform.GetLocalEulerAngles_Injected(intPtr, order, out vector);
			return vector;
		}

		// Token: 0x06002539 RID: 9529 RVA: 0x00047FD4 File Offset: 0x000461D4
		internal void SetLocalEulerAngles(Vector3 euler, RotationOrder order)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Transform.SetLocalEulerAngles_Injected(intPtr, ref euler, order);
		}

		// Token: 0x0600253A RID: 9530 RVA: 0x00047FFC File Offset: 0x000461FC
		[NativeConditional("UNITY_EDITOR")]
		internal void SetLocalEulerHint(Vector3 euler)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Transform.SetLocalEulerHint_Injected(intPtr, ref euler);
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x0600253B RID: 9531 RVA: 0x00048020 File Offset: 0x00046220
		// (set) Token: 0x0600253C RID: 9532 RVA: 0x00048040 File Offset: 0x00046240
		public Vector3 eulerAngles
		{
			get
			{
				return this.rotation.eulerAngles;
			}
			set
			{
				this.rotation = Quaternion.Euler(value);
			}
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x0600253D RID: 9533 RVA: 0x00048050 File Offset: 0x00046250
		// (set) Token: 0x0600253E RID: 9534 RVA: 0x00048070 File Offset: 0x00046270
		public Vector3 localEulerAngles
		{
			get
			{
				return this.localRotation.eulerAngles;
			}
			set
			{
				this.localRotation = Quaternion.Euler(value);
			}
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x0600253F RID: 9535 RVA: 0x00048080 File Offset: 0x00046280
		// (set) Token: 0x06002540 RID: 9536 RVA: 0x000480A2 File Offset: 0x000462A2
		public Vector3 right
		{
			get
			{
				return this.rotation * Vector3.right;
			}
			set
			{
				this.rotation = Quaternion.FromToRotation(Vector3.right, value);
			}
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06002541 RID: 9537 RVA: 0x000480B8 File Offset: 0x000462B8
		// (set) Token: 0x06002542 RID: 9538 RVA: 0x000480DA File Offset: 0x000462DA
		public Vector3 up
		{
			get
			{
				return this.rotation * Vector3.up;
			}
			set
			{
				this.rotation = Quaternion.FromToRotation(Vector3.up, value);
			}
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06002543 RID: 9539 RVA: 0x000480F0 File Offset: 0x000462F0
		// (set) Token: 0x06002544 RID: 9540 RVA: 0x00048112 File Offset: 0x00046312
		public Vector3 forward
		{
			get
			{
				return this.rotation * Vector3.forward;
			}
			set
			{
				this.rotation = Quaternion.LookRotation(value);
			}
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06002545 RID: 9541 RVA: 0x00048124 File Offset: 0x00046324
		// (set) Token: 0x06002546 RID: 9542 RVA: 0x0004814C File Offset: 0x0004634C
		public Quaternion rotation
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Quaternion quaternion;
				Transform.get_rotation_Injected(intPtr, out quaternion);
				return quaternion;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Transform.set_rotation_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06002547 RID: 9543 RVA: 0x00048170 File Offset: 0x00046370
		// (set) Token: 0x06002548 RID: 9544 RVA: 0x00048198 File Offset: 0x00046398
		public Quaternion localRotation
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Quaternion quaternion;
				Transform.get_localRotation_Injected(intPtr, out quaternion);
				return quaternion;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Transform.set_localRotation_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06002549 RID: 9545 RVA: 0x000481BC File Offset: 0x000463BC
		// (set) Token: 0x0600254A RID: 9546 RVA: 0x000481D4 File Offset: 0x000463D4
		[NativeConditional("UNITY_EDITOR")]
		internal RotationOrder rotationOrder
		{
			get
			{
				return (RotationOrder)this.GetRotationOrderInternal();
			}
			set
			{
				this.SetRotationOrderInternal(value);
			}
		}

		// Token: 0x0600254B RID: 9547 RVA: 0x000481E0 File Offset: 0x000463E0
		[NativeConditional("UNITY_EDITOR")]
		[NativeMethod("GetRotationOrder")]
		internal int GetRotationOrderInternal()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Transform.GetRotationOrderInternal_Injected(intPtr);
		}

		// Token: 0x0600254C RID: 9548 RVA: 0x00048204 File Offset: 0x00046404
		[NativeConditional("UNITY_EDITOR")]
		[NativeMethod("SetRotationOrder")]
		internal void SetRotationOrderInternal(RotationOrder rotationOrder)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Transform.SetRotationOrderInternal_Injected(intPtr, rotationOrder);
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x0600254D RID: 9549 RVA: 0x00048228 File Offset: 0x00046428
		// (set) Token: 0x0600254E RID: 9550 RVA: 0x00048250 File Offset: 0x00046450
		public Vector3 localScale
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				Transform.get_localScale_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Transform.set_localScale_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x0600254F RID: 9551 RVA: 0x00048274 File Offset: 0x00046474
		// (set) Token: 0x06002550 RID: 9552 RVA: 0x0004828C File Offset: 0x0004648C
		public Transform parent
		{
			get
			{
				return this.parentInternal;
			}
			set
			{
				bool flag = this is RectTransform;
				if (flag)
				{
					Debug.LogWarning("Parent of RectTransform is being set with parent property. Consider using the SetParent method instead, with the worldPositionStays argument set to false. This will retain local orientation and scale rather than world orientation and scale, which can prevent common UI scaling issues.", this);
				}
				this.parentInternal = value;
			}
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06002551 RID: 9553 RVA: 0x000482BC File Offset: 0x000464BC
		// (set) Token: 0x06002552 RID: 9554 RVA: 0x000482D4 File Offset: 0x000464D4
		internal Transform parentInternal
		{
			get
			{
				return this.GetParent();
			}
			set
			{
				this.SetParent(value);
			}
		}

		// Token: 0x06002553 RID: 9555 RVA: 0x000482E0 File Offset: 0x000464E0
		private Transform GetParent()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<Transform>(Transform.GetParent_Injected(intPtr));
		}

		// Token: 0x06002554 RID: 9556 RVA: 0x00048307 File Offset: 0x00046507
		public void SetParent(Transform p)
		{
			this.SetParent(p, true);
		}

		// Token: 0x06002555 RID: 9557 RVA: 0x00048314 File Offset: 0x00046514
		[FreeFunction("SetParent", HasExplicitThis = true)]
		public void SetParent(Transform parent, bool worldPositionStays)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Transform.SetParent_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Transform>(parent), worldPositionStays);
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06002556 RID: 9558 RVA: 0x00048340 File Offset: 0x00046540
		public Matrix4x4 worldToLocalMatrix
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Matrix4x4 matrix4x;
				Transform.get_worldToLocalMatrix_Injected(intPtr, out matrix4x);
				return matrix4x;
			}
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06002557 RID: 9559 RVA: 0x00048368 File Offset: 0x00046568
		public Matrix4x4 localToWorldMatrix
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Matrix4x4 matrix4x;
				Transform.get_localToWorldMatrix_Injected(intPtr, out matrix4x);
				return matrix4x;
			}
		}

		// Token: 0x06002558 RID: 9560 RVA: 0x00048390 File Offset: 0x00046590
		public void SetPositionAndRotation(Vector3 position, Quaternion rotation)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Transform.SetPositionAndRotation_Injected(intPtr, ref position, ref rotation);
		}

		// Token: 0x06002559 RID: 9561 RVA: 0x000483B8 File Offset: 0x000465B8
		public void SetLocalPositionAndRotation(Vector3 localPosition, Quaternion localRotation)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Transform.SetLocalPositionAndRotation_Injected(intPtr, ref localPosition, ref localRotation);
		}

		// Token: 0x0600255A RID: 9562 RVA: 0x000483E0 File Offset: 0x000465E0
		public void GetPositionAndRotation(out Vector3 position, out Quaternion rotation)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Transform.GetPositionAndRotation_Injected(intPtr, out position, out rotation);
		}

		// Token: 0x0600255B RID: 9563 RVA: 0x00048404 File Offset: 0x00046604
		public void GetLocalPositionAndRotation(out Vector3 localPosition, out Quaternion localRotation)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Transform.GetLocalPositionAndRotation_Injected(intPtr, out localPosition, out localRotation);
		}

		// Token: 0x0600255C RID: 9564 RVA: 0x00048428 File Offset: 0x00046628
		public void Translate(Vector3 translation, [DefaultValue("Space.Self")] Space relativeTo)
		{
			bool flag = relativeTo == Space.World;
			if (flag)
			{
				this.position += translation;
			}
			else
			{
				this.position += this.TransformDirection(translation);
			}
		}

		// Token: 0x0600255D RID: 9565 RVA: 0x0004846C File Offset: 0x0004666C
		public void Translate(Vector3 translation)
		{
			this.Translate(translation, Space.Self);
		}

		// Token: 0x0600255E RID: 9566 RVA: 0x00048478 File Offset: 0x00046678
		public void Translate(float x, float y, float z, [DefaultValue("Space.Self")] Space relativeTo)
		{
			this.Translate(new Vector3(x, y, z), relativeTo);
		}

		// Token: 0x0600255F RID: 9567 RVA: 0x0004848C File Offset: 0x0004668C
		public void Translate(float x, float y, float z)
		{
			this.Translate(new Vector3(x, y, z), Space.Self);
		}

		// Token: 0x06002560 RID: 9568 RVA: 0x000484A0 File Offset: 0x000466A0
		public void Translate(Vector3 translation, Transform relativeTo)
		{
			bool flag = relativeTo;
			if (flag)
			{
				this.position += relativeTo.TransformDirection(translation);
			}
			else
			{
				this.position += translation;
			}
		}

		// Token: 0x06002561 RID: 9569 RVA: 0x000484E6 File Offset: 0x000466E6
		public void Translate(float x, float y, float z, Transform relativeTo)
		{
			this.Translate(new Vector3(x, y, z), relativeTo);
		}

		// Token: 0x06002562 RID: 9570 RVA: 0x000484FC File Offset: 0x000466FC
		public void Rotate(Vector3 eulers, [DefaultValue("Space.Self")] Space relativeTo)
		{
			Quaternion quaternion = Quaternion.Euler(eulers.x, eulers.y, eulers.z);
			bool flag = relativeTo == Space.Self;
			if (flag)
			{
				this.localRotation *= quaternion;
			}
			else
			{
				this.rotation *= Quaternion.Inverse(this.rotation) * quaternion * this.rotation;
			}
		}

		// Token: 0x06002563 RID: 9571 RVA: 0x0004856F File Offset: 0x0004676F
		public void Rotate(Vector3 eulers)
		{
			this.Rotate(eulers, Space.Self);
		}

		// Token: 0x06002564 RID: 9572 RVA: 0x0004857B File Offset: 0x0004677B
		public void Rotate(float xAngle, float yAngle, float zAngle, [DefaultValue("Space.Self")] Space relativeTo)
		{
			this.Rotate(new Vector3(xAngle, yAngle, zAngle), relativeTo);
		}

		// Token: 0x06002565 RID: 9573 RVA: 0x0004858F File Offset: 0x0004678F
		public void Rotate(float xAngle, float yAngle, float zAngle)
		{
			this.Rotate(new Vector3(xAngle, yAngle, zAngle), Space.Self);
		}

		// Token: 0x06002566 RID: 9574 RVA: 0x000485A4 File Offset: 0x000467A4
		[NativeMethod("RotateAround")]
		internal void RotateAroundInternal(Vector3 axis, float angle)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Transform.RotateAroundInternal_Injected(intPtr, ref axis, angle);
		}

		// Token: 0x06002567 RID: 9575 RVA: 0x000485CC File Offset: 0x000467CC
		public void Rotate(Vector3 axis, float angle, [DefaultValue("Space.Self")] Space relativeTo)
		{
			bool flag = relativeTo == Space.Self;
			if (flag)
			{
				this.RotateAroundInternal(base.transform.TransformDirection(axis), angle * 0.017453292f);
			}
			else
			{
				this.RotateAroundInternal(axis, angle * 0.017453292f);
			}
		}

		// Token: 0x06002568 RID: 9576 RVA: 0x0004860D File Offset: 0x0004680D
		public void Rotate(Vector3 axis, float angle)
		{
			this.Rotate(axis, angle, Space.Self);
		}

		// Token: 0x06002569 RID: 9577 RVA: 0x0004861C File Offset: 0x0004681C
		public void RotateAround(Vector3 point, Vector3 axis, float angle)
		{
			Vector3 vector = this.position;
			Quaternion quaternion = Quaternion.AngleAxis(angle, axis);
			Vector3 vector2 = vector - point;
			vector2 = quaternion * vector2;
			vector = point + vector2;
			this.position = vector;
			this.RotateAroundInternal(axis, angle * 0.017453292f);
		}

		// Token: 0x0600256A RID: 9578 RVA: 0x00048668 File Offset: 0x00046868
		public void LookAt(Transform target, [DefaultValue("Vector3.up")] Vector3 worldUp)
		{
			bool flag = target;
			if (flag)
			{
				this.LookAt(target.position, worldUp);
			}
		}

		// Token: 0x0600256B RID: 9579 RVA: 0x00048690 File Offset: 0x00046890
		public void LookAt(Transform target)
		{
			bool flag = target;
			if (flag)
			{
				this.LookAt(target.position, Vector3.up);
			}
		}

		// Token: 0x0600256C RID: 9580 RVA: 0x000486BA File Offset: 0x000468BA
		public void LookAt(Vector3 worldPosition, [DefaultValue("Vector3.up")] Vector3 worldUp)
		{
			this.Internal_LookAt(worldPosition, worldUp);
		}

		// Token: 0x0600256D RID: 9581 RVA: 0x000486C6 File Offset: 0x000468C6
		public void LookAt(Vector3 worldPosition)
		{
			this.Internal_LookAt(worldPosition, Vector3.up);
		}

		// Token: 0x0600256E RID: 9582 RVA: 0x000486D8 File Offset: 0x000468D8
		[FreeFunction("Internal_LookAt", HasExplicitThis = true)]
		private void Internal_LookAt(Vector3 worldPosition, Vector3 worldUp)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Transform.Internal_LookAt_Injected(intPtr, ref worldPosition, ref worldUp);
		}

		// Token: 0x0600256F RID: 9583 RVA: 0x00048700 File Offset: 0x00046900
		public Vector3 TransformDirection(Vector3 direction)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3 vector;
			Transform.TransformDirection_Injected(intPtr, ref direction, out vector);
			return vector;
		}

		// Token: 0x06002570 RID: 9584 RVA: 0x00048728 File Offset: 0x00046928
		public Vector3 TransformDirection(float x, float y, float z)
		{
			return this.TransformDirection(new Vector3(x, y, z));
		}

		// Token: 0x06002571 RID: 9585 RVA: 0x00048748 File Offset: 0x00046948
		[NativeMethod(Name = "TransformDirections")]
		internal unsafe void TransformDirectionsInternal(ReadOnlySpan<Vector3> directions, Span<Vector3> transformedDirections)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ReadOnlySpan<Vector3> readOnlySpan = directions;
			fixed (Vector3* ptr = readOnlySpan.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
				Span<Vector3> span = transformedDirections;
				fixed (Vector3* pinnableReference = span.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper2 = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
					Transform.TransformDirectionsInternal_Injected(intPtr, ref managedSpanWrapper, ref managedSpanWrapper2);
					ptr = null;
				}
			}
		}

		// Token: 0x06002572 RID: 9586 RVA: 0x000487B0 File Offset: 0x000469B0
		public void TransformDirections(ReadOnlySpan<Vector3> directions, Span<Vector3> transformedDirections)
		{
			bool flag = directions.Length != transformedDirections.Length;
			if (flag)
			{
				throw new InvalidOperationException("Both spans passed to Transform.TransformDirections() must be the same length");
			}
			this.TransformDirectionsInternal(directions, transformedDirections);
		}

		// Token: 0x06002573 RID: 9587 RVA: 0x000487E9 File Offset: 0x000469E9
		public void TransformDirections(Span<Vector3> directions)
		{
			this.TransformDirectionsInternal(directions, directions);
		}

		// Token: 0x06002574 RID: 9588 RVA: 0x000487FC File Offset: 0x000469FC
		public Vector3 InverseTransformDirection(Vector3 direction)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3 vector;
			Transform.InverseTransformDirection_Injected(intPtr, ref direction, out vector);
			return vector;
		}

		// Token: 0x06002575 RID: 9589 RVA: 0x00048824 File Offset: 0x00046A24
		public Vector3 InverseTransformDirection(float x, float y, float z)
		{
			return this.InverseTransformDirection(new Vector3(x, y, z));
		}

		// Token: 0x06002576 RID: 9590 RVA: 0x00048844 File Offset: 0x00046A44
		[NativeMethod(Name = "InverseTransformDirections")]
		internal unsafe void InverseTransformDirectionsInternal(ReadOnlySpan<Vector3> directions, Span<Vector3> transformedDirections)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ReadOnlySpan<Vector3> readOnlySpan = directions;
			fixed (Vector3* ptr = readOnlySpan.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
				Span<Vector3> span = transformedDirections;
				fixed (Vector3* pinnableReference = span.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper2 = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
					Transform.InverseTransformDirectionsInternal_Injected(intPtr, ref managedSpanWrapper, ref managedSpanWrapper2);
					ptr = null;
				}
			}
		}

		// Token: 0x06002577 RID: 9591 RVA: 0x000488AC File Offset: 0x00046AAC
		public void InverseTransformDirections(ReadOnlySpan<Vector3> directions, Span<Vector3> transformedDirections)
		{
			bool flag = directions.Length != transformedDirections.Length;
			if (flag)
			{
				throw new InvalidOperationException("Both spans passed to Transform.InverseTransformDirections() must be the same length");
			}
			this.InverseTransformDirectionsInternal(directions, transformedDirections);
		}

		// Token: 0x06002578 RID: 9592 RVA: 0x000488E5 File Offset: 0x00046AE5
		public void InverseTransformDirections(Span<Vector3> directions)
		{
			this.InverseTransformDirectionsInternal(directions, directions);
		}

		// Token: 0x06002579 RID: 9593 RVA: 0x000488F8 File Offset: 0x00046AF8
		public Vector3 TransformVector(Vector3 vector)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3 vector2;
			Transform.TransformVector_Injected(intPtr, ref vector, out vector2);
			return vector2;
		}

		// Token: 0x0600257A RID: 9594 RVA: 0x00048920 File Offset: 0x00046B20
		public Vector3 TransformVector(float x, float y, float z)
		{
			return this.TransformVector(new Vector3(x, y, z));
		}

		// Token: 0x0600257B RID: 9595 RVA: 0x00048940 File Offset: 0x00046B40
		[NativeMethod(Name = "TransformVectors")]
		internal unsafe void TransformVectorsInternal(ReadOnlySpan<Vector3> vectors, Span<Vector3> transformedVectors)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ReadOnlySpan<Vector3> readOnlySpan = vectors;
			fixed (Vector3* ptr = readOnlySpan.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
				Span<Vector3> span = transformedVectors;
				fixed (Vector3* pinnableReference = span.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper2 = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
					Transform.TransformVectorsInternal_Injected(intPtr, ref managedSpanWrapper, ref managedSpanWrapper2);
					ptr = null;
				}
			}
		}

		// Token: 0x0600257C RID: 9596 RVA: 0x000489A8 File Offset: 0x00046BA8
		public void TransformVectors(ReadOnlySpan<Vector3> vectors, Span<Vector3> transformedVectors)
		{
			bool flag = vectors.Length != transformedVectors.Length;
			if (flag)
			{
				throw new InvalidOperationException("Both spans passed to Transform.TransformVectors() must be the same length");
			}
			this.TransformVectorsInternal(vectors, transformedVectors);
		}

		// Token: 0x0600257D RID: 9597 RVA: 0x000489E1 File Offset: 0x00046BE1
		public void TransformVectors(Span<Vector3> vectors)
		{
			this.TransformVectorsInternal(vectors, vectors);
		}

		// Token: 0x0600257E RID: 9598 RVA: 0x000489F4 File Offset: 0x00046BF4
		public Vector3 InverseTransformVector(Vector3 vector)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3 vector2;
			Transform.InverseTransformVector_Injected(intPtr, ref vector, out vector2);
			return vector2;
		}

		// Token: 0x0600257F RID: 9599 RVA: 0x00048A1C File Offset: 0x00046C1C
		public Vector3 InverseTransformVector(float x, float y, float z)
		{
			return this.InverseTransformVector(new Vector3(x, y, z));
		}

		// Token: 0x06002580 RID: 9600 RVA: 0x00048A3C File Offset: 0x00046C3C
		[NativeMethod(Name = "InverseTransformVectors")]
		internal unsafe void InverseTransformVectorsInternal(ReadOnlySpan<Vector3> vectors, Span<Vector3> transformedVectors)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ReadOnlySpan<Vector3> readOnlySpan = vectors;
			fixed (Vector3* ptr = readOnlySpan.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
				Span<Vector3> span = transformedVectors;
				fixed (Vector3* pinnableReference = span.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper2 = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
					Transform.InverseTransformVectorsInternal_Injected(intPtr, ref managedSpanWrapper, ref managedSpanWrapper2);
					ptr = null;
				}
			}
		}

		// Token: 0x06002581 RID: 9601 RVA: 0x00048AA4 File Offset: 0x00046CA4
		public void InverseTransformVectors(ReadOnlySpan<Vector3> vectors, Span<Vector3> transformedVectors)
		{
			bool flag = vectors.Length != transformedVectors.Length;
			if (flag)
			{
				throw new InvalidOperationException("Both spans passed to Transform.InverseTransformVectors() must be the same length");
			}
			this.InverseTransformVectorsInternal(vectors, transformedVectors);
		}

		// Token: 0x06002582 RID: 9602 RVA: 0x00048ADD File Offset: 0x00046CDD
		public void InverseTransformVectors(Span<Vector3> vectors)
		{
			this.InverseTransformVectorsInternal(vectors, vectors);
		}

		// Token: 0x06002583 RID: 9603 RVA: 0x00048AF0 File Offset: 0x00046CF0
		public Vector3 TransformPoint(Vector3 position)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3 vector;
			Transform.TransformPoint_Injected(intPtr, ref position, out vector);
			return vector;
		}

		// Token: 0x06002584 RID: 9604 RVA: 0x00048B18 File Offset: 0x00046D18
		public Vector3 TransformPoint(float x, float y, float z)
		{
			return this.TransformPoint(new Vector3(x, y, z));
		}

		// Token: 0x06002585 RID: 9605 RVA: 0x00048B38 File Offset: 0x00046D38
		[NativeMethod(Name = "TransformPoints")]
		internal unsafe void TransformPointsInternal(ReadOnlySpan<Vector3> positions, Span<Vector3> transformedPositions)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ReadOnlySpan<Vector3> readOnlySpan = positions;
			fixed (Vector3* ptr = readOnlySpan.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
				Span<Vector3> span = transformedPositions;
				fixed (Vector3* pinnableReference = span.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper2 = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
					Transform.TransformPointsInternal_Injected(intPtr, ref managedSpanWrapper, ref managedSpanWrapper2);
					ptr = null;
				}
			}
		}

		// Token: 0x06002586 RID: 9606 RVA: 0x00048BA0 File Offset: 0x00046DA0
		public void TransformPoints(ReadOnlySpan<Vector3> positions, Span<Vector3> transformedPositions)
		{
			bool flag = positions.Length != transformedPositions.Length;
			if (flag)
			{
				throw new InvalidOperationException("Both spans passed to Transform.TransformPoints() must be the same length");
			}
			this.TransformPointsInternal(positions, transformedPositions);
		}

		// Token: 0x06002587 RID: 9607 RVA: 0x00048BD9 File Offset: 0x00046DD9
		public void TransformPoints(Span<Vector3> positions)
		{
			this.TransformPointsInternal(positions, positions);
		}

		// Token: 0x06002588 RID: 9608 RVA: 0x00048BEC File Offset: 0x00046DEC
		public Vector3 InverseTransformPoint(Vector3 position)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3 vector;
			Transform.InverseTransformPoint_Injected(intPtr, ref position, out vector);
			return vector;
		}

		// Token: 0x06002589 RID: 9609 RVA: 0x00048C14 File Offset: 0x00046E14
		public Vector3 InverseTransformPoint(float x, float y, float z)
		{
			return this.InverseTransformPoint(new Vector3(x, y, z));
		}

		// Token: 0x0600258A RID: 9610 RVA: 0x00048C34 File Offset: 0x00046E34
		[NativeMethod(Name = "InverseTransformPoints")]
		internal unsafe void InverseTransformPointsInternal(ReadOnlySpan<Vector3> positions, Span<Vector3> transformedPositions)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ReadOnlySpan<Vector3> readOnlySpan = positions;
			fixed (Vector3* ptr = readOnlySpan.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
				Span<Vector3> span = transformedPositions;
				fixed (Vector3* pinnableReference = span.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper2 = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
					Transform.InverseTransformPointsInternal_Injected(intPtr, ref managedSpanWrapper, ref managedSpanWrapper2);
					ptr = null;
				}
			}
		}

		// Token: 0x0600258B RID: 9611 RVA: 0x00048C9C File Offset: 0x00046E9C
		public void InverseTransformPoints(ReadOnlySpan<Vector3> positions, Span<Vector3> transformedPositions)
		{
			bool flag = positions.Length != transformedPositions.Length;
			if (flag)
			{
				throw new InvalidOperationException("Both spans passed to Transform.InverseTransformPoints() must be the same length");
			}
			this.InverseTransformPointsInternal(positions, transformedPositions);
		}

		// Token: 0x0600258C RID: 9612 RVA: 0x00048CD5 File Offset: 0x00046ED5
		public void InverseTransformPoints(Span<Vector3> positions)
		{
			this.InverseTransformPoints(positions, positions);
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x0600258D RID: 9613 RVA: 0x00048CE8 File Offset: 0x00046EE8
		public Transform root
		{
			get
			{
				return this.GetRoot();
			}
		}

		// Token: 0x0600258E RID: 9614 RVA: 0x00048D00 File Offset: 0x00046F00
		private Transform GetRoot()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<Transform>(Transform.GetRoot_Injected(intPtr));
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x0600258F RID: 9615 RVA: 0x00048D28 File Offset: 0x00046F28
		public int childCount
		{
			[NativeMethod("GetChildrenCount")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Transform.get_childCount_Injected(intPtr);
			}
		}

		// Token: 0x06002590 RID: 9616 RVA: 0x00048D4C File Offset: 0x00046F4C
		[FreeFunction("DetachChildren", HasExplicitThis = true)]
		public void DetachChildren()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Transform.DetachChildren_Injected(intPtr);
		}

		// Token: 0x06002591 RID: 9617 RVA: 0x00048D70 File Offset: 0x00046F70
		public void SetAsFirstSibling()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Transform.SetAsFirstSibling_Injected(intPtr);
		}

		// Token: 0x06002592 RID: 9618 RVA: 0x00048D94 File Offset: 0x00046F94
		public void SetAsLastSibling()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Transform.SetAsLastSibling_Injected(intPtr);
		}

		// Token: 0x06002593 RID: 9619 RVA: 0x00048DB8 File Offset: 0x00046FB8
		public void SetSiblingIndex(int index)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Transform.SetSiblingIndex_Injected(intPtr, index);
		}

		// Token: 0x06002594 RID: 9620 RVA: 0x00048DDC File Offset: 0x00046FDC
		[NativeMethod("MoveAfterSiblingInternal")]
		internal void MoveAfterSibling(Transform transform, bool notifyEditorAndMarkDirty)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Transform.MoveAfterSibling_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Transform>(transform), notifyEditorAndMarkDirty);
		}

		// Token: 0x06002595 RID: 9621 RVA: 0x00048E08 File Offset: 0x00047008
		public int GetSiblingIndex()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Transform.GetSiblingIndex_Injected(intPtr);
		}

		// Token: 0x06002596 RID: 9622 RVA: 0x00048E2C File Offset: 0x0004702C
		[FreeFunction(HasExplicitThis = true)]
		private unsafe Transform FindRelativeTransformWithPath(string path, [DefaultValue("false")] bool isActiveOnly)
		{
			Transform transform;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(path, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = path.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				IntPtr intPtr2 = Transform.FindRelativeTransformWithPath_Injected(intPtr, ref managedSpanWrapper, isActiveOnly);
			}
			finally
			{
				IntPtr intPtr2;
				transform = Unmarshal.UnmarshalUnityObject<Transform>(intPtr2);
				char* ptr = null;
			}
			return transform;
		}

		// Token: 0x06002597 RID: 9623 RVA: 0x00048E9C File Offset: 0x0004709C
		public Transform Find(string n)
		{
			bool flag = n == null;
			if (flag)
			{
				throw new ArgumentNullException("Name cannot be null");
			}
			return this.FindRelativeTransformWithPath(n, false);
		}

		// Token: 0x06002598 RID: 9624 RVA: 0x00048ECC File Offset: 0x000470CC
		[NativeConditional("UNITY_EDITOR")]
		internal void SendTransformChangedScale()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Transform.SendTransformChangedScale_Injected(intPtr);
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06002599 RID: 9625 RVA: 0x00048EF0 File Offset: 0x000470F0
		public Vector3 lossyScale
		{
			[NativeMethod("GetWorldScaleLossy")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				Transform.get_lossyScale_Injected(intPtr, out vector);
				return vector;
			}
		}

		// Token: 0x0600259A RID: 9626 RVA: 0x00048F18 File Offset: 0x00047118
		[FreeFunction("Internal_IsChildOrSameTransform", HasExplicitThis = true)]
		public bool IsChildOf([NotNull] Transform parent)
		{
			if (parent == null)
			{
				ThrowHelper.ThrowArgumentNullException(parent, "parent");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Transform>(parent);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(parent, "parent");
			}
			return Transform.IsChildOf_Injected(intPtr, intPtr2);
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x0600259B RID: 9627 RVA: 0x00048F60 File Offset: 0x00047160
		// (set) Token: 0x0600259C RID: 9628 RVA: 0x00048F84 File Offset: 0x00047184
		[NativeProperty("HasChangedDeprecated")]
		public bool hasChanged
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Transform.get_hasChanged_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Transform.set_hasChanged_Injected(intPtr, value);
			}
		}

		// Token: 0x0600259D RID: 9629 RVA: 0x00048FA8 File Offset: 0x000471A8
		[Obsolete("FindChild has been deprecated. Use Find instead (UnityUpgradable) -> Find([mscorlib] System.String)", false)]
		public Transform FindChild(string n)
		{
			return this.Find(n);
		}

		// Token: 0x0600259E RID: 9630 RVA: 0x00048FC4 File Offset: 0x000471C4
		public IEnumerator GetEnumerator()
		{
			return new Transform.Enumerator(this);
		}

		// Token: 0x0600259F RID: 9631 RVA: 0x00048FDC File Offset: 0x000471DC
		[Obsolete("warning use Transform.Rotate instead.")]
		public void RotateAround(Vector3 axis, float angle)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Transform.RotateAround_Injected(intPtr, ref axis, angle);
		}

		// Token: 0x060025A0 RID: 9632 RVA: 0x00049004 File Offset: 0x00047204
		[Obsolete("warning use Transform.Rotate instead.")]
		public void RotateAroundLocal(Vector3 axis, float angle)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Transform.RotateAroundLocal_Injected(intPtr, ref axis, angle);
		}

		// Token: 0x060025A1 RID: 9633 RVA: 0x0004902C File Offset: 0x0004722C
		[NativeThrows]
		[FreeFunction("GetChild", HasExplicitThis = true)]
		public Transform GetChild(int index)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<Transform>(Transform.GetChild_Injected(intPtr, index));
		}

		// Token: 0x060025A2 RID: 9634 RVA: 0x00049054 File Offset: 0x00047254
		[Obsolete("warning use Transform.childCount instead (UnityUpgradable) -> Transform.childCount", false)]
		[NativeMethod("GetChildrenCount")]
		public int GetChildCount()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Transform.GetChildCount_Injected(intPtr);
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x060025A3 RID: 9635 RVA: 0x00049078 File Offset: 0x00047278
		// (set) Token: 0x060025A4 RID: 9636 RVA: 0x00049090 File Offset: 0x00047290
		public int hierarchyCapacity
		{
			get
			{
				return this.internal_getHierarchyCapacity();
			}
			set
			{
				this.internal_setHierarchyCapacity(value);
			}
		}

		// Token: 0x060025A5 RID: 9637 RVA: 0x0004909C File Offset: 0x0004729C
		[FreeFunction("GetHierarchyCapacity", HasExplicitThis = true)]
		private int internal_getHierarchyCapacity()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Transform.internal_getHierarchyCapacity_Injected(intPtr);
		}

		// Token: 0x060025A6 RID: 9638 RVA: 0x000490C0 File Offset: 0x000472C0
		[FreeFunction("SetHierarchyCapacity", HasExplicitThis = true)]
		private void internal_setHierarchyCapacity(int value)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Transform.internal_setHierarchyCapacity_Injected(intPtr, value);
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x060025A7 RID: 9639 RVA: 0x000490E4 File Offset: 0x000472E4
		public int hierarchyCount
		{
			get
			{
				return this.internal_getHierarchyCount();
			}
		}

		// Token: 0x060025A8 RID: 9640 RVA: 0x000490FC File Offset: 0x000472FC
		[FreeFunction("GetHierarchyCount", HasExplicitThis = true)]
		private int internal_getHierarchyCount()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Transform.internal_getHierarchyCount_Injected(intPtr);
		}

		// Token: 0x060025A9 RID: 9641 RVA: 0x00049120 File Offset: 0x00047320
		[FreeFunction("IsNonUniformScaleTransform", HasExplicitThis = true)]
		[NativeConditional("UNITY_EDITOR")]
		internal bool IsNonUniformScaleTransform()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Transform.IsNonUniformScaleTransform_Injected(intPtr);
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x060025AA RID: 9642 RVA: 0x00049142 File Offset: 0x00047342
		// (set) Token: 0x060025AB RID: 9643 RVA: 0x0004914A File Offset: 0x0004734A
		[NativeConditional("UNITY_EDITOR")]
		internal bool constrainProportionsScale
		{
			get
			{
				return this.IsConstrainProportionsScale();
			}
			set
			{
				this.SetConstrainProportionsScale(value);
			}
		}

		// Token: 0x060025AC RID: 9644 RVA: 0x00049154 File Offset: 0x00047354
		[NativeConditional("UNITY_EDITOR")]
		private void SetConstrainProportionsScale(bool isLinked)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Transform.SetConstrainProportionsScale_Injected(intPtr, isLinked);
		}

		// Token: 0x060025AD RID: 9645 RVA: 0x00049178 File Offset: 0x00047378
		[NativeConditional("UNITY_EDITOR")]
		private bool IsConstrainProportionsScale()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Transform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Transform.IsConstrainProportionsScale_Injected(intPtr);
		}

		// Token: 0x060025AE RID: 9646
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_position_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x060025AF RID: 9647
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_position_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x060025B0 RID: 9648
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_localPosition_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x060025B1 RID: 9649
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_localPosition_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x060025B2 RID: 9650
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalEulerAngles_Injected(IntPtr _unity_self, RotationOrder order, out Vector3 ret);

		// Token: 0x060025B3 RID: 9651
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLocalEulerAngles_Injected(IntPtr _unity_self, [In] ref Vector3 euler, RotationOrder order);

		// Token: 0x060025B4 RID: 9652
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLocalEulerHint_Injected(IntPtr _unity_self, [In] ref Vector3 euler);

		// Token: 0x060025B5 RID: 9653
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_rotation_Injected(IntPtr _unity_self, out Quaternion ret);

		// Token: 0x060025B6 RID: 9654
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_rotation_Injected(IntPtr _unity_self, [In] ref Quaternion value);

		// Token: 0x060025B7 RID: 9655
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_localRotation_Injected(IntPtr _unity_self, out Quaternion ret);

		// Token: 0x060025B8 RID: 9656
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_localRotation_Injected(IntPtr _unity_self, [In] ref Quaternion value);

		// Token: 0x060025B9 RID: 9657
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetRotationOrderInternal_Injected(IntPtr _unity_self);

		// Token: 0x060025BA RID: 9658
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetRotationOrderInternal_Injected(IntPtr _unity_self, RotationOrder rotationOrder);

		// Token: 0x060025BB RID: 9659
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_localScale_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x060025BC RID: 9660
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_localScale_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x060025BD RID: 9661
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetParent_Injected(IntPtr _unity_self);

		// Token: 0x060025BE RID: 9662
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetParent_Injected(IntPtr _unity_self, IntPtr parent, bool worldPositionStays);

		// Token: 0x060025BF RID: 9663
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_worldToLocalMatrix_Injected(IntPtr _unity_self, out Matrix4x4 ret);

		// Token: 0x060025C0 RID: 9664
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_localToWorldMatrix_Injected(IntPtr _unity_self, out Matrix4x4 ret);

		// Token: 0x060025C1 RID: 9665
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPositionAndRotation_Injected(IntPtr _unity_self, [In] ref Vector3 position, [In] ref Quaternion rotation);

		// Token: 0x060025C2 RID: 9666
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLocalPositionAndRotation_Injected(IntPtr _unity_self, [In] ref Vector3 localPosition, [In] ref Quaternion localRotation);

		// Token: 0x060025C3 RID: 9667
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPositionAndRotation_Injected(IntPtr _unity_self, out Vector3 position, out Quaternion rotation);

		// Token: 0x060025C4 RID: 9668
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalPositionAndRotation_Injected(IntPtr _unity_self, out Vector3 localPosition, out Quaternion localRotation);

		// Token: 0x060025C5 RID: 9669
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RotateAroundInternal_Injected(IntPtr _unity_self, [In] ref Vector3 axis, float angle);

		// Token: 0x060025C6 RID: 9670
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_LookAt_Injected(IntPtr _unity_self, [In] ref Vector3 worldPosition, [In] ref Vector3 worldUp);

		// Token: 0x060025C7 RID: 9671
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void TransformDirection_Injected(IntPtr _unity_self, [In] ref Vector3 direction, out Vector3 ret);

		// Token: 0x060025C8 RID: 9672
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void TransformDirectionsInternal_Injected(IntPtr _unity_self, ref ManagedSpanWrapper directions, ref ManagedSpanWrapper transformedDirections);

		// Token: 0x060025C9 RID: 9673
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InverseTransformDirection_Injected(IntPtr _unity_self, [In] ref Vector3 direction, out Vector3 ret);

		// Token: 0x060025CA RID: 9674
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InverseTransformDirectionsInternal_Injected(IntPtr _unity_self, ref ManagedSpanWrapper directions, ref ManagedSpanWrapper transformedDirections);

		// Token: 0x060025CB RID: 9675
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void TransformVector_Injected(IntPtr _unity_self, [In] ref Vector3 vector, out Vector3 ret);

		// Token: 0x060025CC RID: 9676
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void TransformVectorsInternal_Injected(IntPtr _unity_self, ref ManagedSpanWrapper vectors, ref ManagedSpanWrapper transformedVectors);

		// Token: 0x060025CD RID: 9677
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InverseTransformVector_Injected(IntPtr _unity_self, [In] ref Vector3 vector, out Vector3 ret);

		// Token: 0x060025CE RID: 9678
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InverseTransformVectorsInternal_Injected(IntPtr _unity_self, ref ManagedSpanWrapper vectors, ref ManagedSpanWrapper transformedVectors);

		// Token: 0x060025CF RID: 9679
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void TransformPoint_Injected(IntPtr _unity_self, [In] ref Vector3 position, out Vector3 ret);

		// Token: 0x060025D0 RID: 9680
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void TransformPointsInternal_Injected(IntPtr _unity_self, ref ManagedSpanWrapper positions, ref ManagedSpanWrapper transformedPositions);

		// Token: 0x060025D1 RID: 9681
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InverseTransformPoint_Injected(IntPtr _unity_self, [In] ref Vector3 position, out Vector3 ret);

		// Token: 0x060025D2 RID: 9682
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InverseTransformPointsInternal_Injected(IntPtr _unity_self, ref ManagedSpanWrapper positions, ref ManagedSpanWrapper transformedPositions);

		// Token: 0x060025D3 RID: 9683
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetRoot_Injected(IntPtr _unity_self);

		// Token: 0x060025D4 RID: 9684
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_childCount_Injected(IntPtr _unity_self);

		// Token: 0x060025D5 RID: 9685
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DetachChildren_Injected(IntPtr _unity_self);

		// Token: 0x060025D6 RID: 9686
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetAsFirstSibling_Injected(IntPtr _unity_self);

		// Token: 0x060025D7 RID: 9687
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetAsLastSibling_Injected(IntPtr _unity_self);

		// Token: 0x060025D8 RID: 9688
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetSiblingIndex_Injected(IntPtr _unity_self, int index);

		// Token: 0x060025D9 RID: 9689
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MoveAfterSibling_Injected(IntPtr _unity_self, IntPtr transform, bool notifyEditorAndMarkDirty);

		// Token: 0x060025DA RID: 9690
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetSiblingIndex_Injected(IntPtr _unity_self);

		// Token: 0x060025DB RID: 9691
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr FindRelativeTransformWithPath_Injected(IntPtr _unity_self, ref ManagedSpanWrapper path, [DefaultValue("false")] bool isActiveOnly);

		// Token: 0x060025DC RID: 9692
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SendTransformChangedScale_Injected(IntPtr _unity_self);

		// Token: 0x060025DD RID: 9693
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_lossyScale_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x060025DE RID: 9694
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsChildOf_Injected(IntPtr _unity_self, IntPtr parent);

		// Token: 0x060025DF RID: 9695
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_hasChanged_Injected(IntPtr _unity_self);

		// Token: 0x060025E0 RID: 9696
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_hasChanged_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060025E1 RID: 9697
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RotateAround_Injected(IntPtr _unity_self, [In] ref Vector3 axis, float angle);

		// Token: 0x060025E2 RID: 9698
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RotateAroundLocal_Injected(IntPtr _unity_self, [In] ref Vector3 axis, float angle);

		// Token: 0x060025E3 RID: 9699
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetChild_Injected(IntPtr _unity_self, int index);

		// Token: 0x060025E4 RID: 9700
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetChildCount_Injected(IntPtr _unity_self);

		// Token: 0x060025E5 RID: 9701
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int internal_getHierarchyCapacity_Injected(IntPtr _unity_self);

		// Token: 0x060025E6 RID: 9702
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void internal_setHierarchyCapacity_Injected(IntPtr _unity_self, int value);

		// Token: 0x060025E7 RID: 9703
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int internal_getHierarchyCount_Injected(IntPtr _unity_self);

		// Token: 0x060025E8 RID: 9704
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsNonUniformScaleTransform_Injected(IntPtr _unity_self);

		// Token: 0x060025E9 RID: 9705
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetConstrainProportionsScale_Injected(IntPtr _unity_self, bool isLinked);

		// Token: 0x060025EA RID: 9706
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsConstrainProportionsScale_Injected(IntPtr _unity_self);

		// Token: 0x020002FB RID: 763
		private class Enumerator : IEnumerator
		{
			// Token: 0x060025EB RID: 9707 RVA: 0x0004919A File Offset: 0x0004739A
			internal Enumerator(Transform outer)
			{
				this.outer = outer;
			}

			// Token: 0x17000622 RID: 1570
			// (get) Token: 0x060025EC RID: 9708 RVA: 0x000491B4 File Offset: 0x000473B4
			public object Current
			{
				get
				{
					return this.outer.GetChild(this.currentIndex);
				}
			}

			// Token: 0x060025ED RID: 9709 RVA: 0x000491D8 File Offset: 0x000473D8
			public bool MoveNext()
			{
				int childCount = this.outer.childCount;
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < childCount;
			}

			// Token: 0x060025EE RID: 9710 RVA: 0x0004920A File Offset: 0x0004740A
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000AA6 RID: 2726
			private Transform outer;

			// Token: 0x04000AA7 RID: 2727
			private int currentIndex = -1;
		}
	}
}
