using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020002F5 RID: 757
	[NativeHeader("Runtime/Transform/RectTransform.h")]
	[NativeClass("UI::RectTransform")]
	public sealed class RectTransform : Transform
	{
		// Token: 0x1400001B RID: 27
		// (add) Token: 0x060024FB RID: 9467 RVA: 0x00047704 File Offset: 0x00045904
		// (remove) Token: 0x060024FC RID: 9468 RVA: 0x00047738 File Offset: 0x00045938
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event RectTransform.ReapplyDrivenProperties reapplyDrivenProperties;

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x060024FD RID: 9469 RVA: 0x0004776C File Offset: 0x0004596C
		public Rect rect
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RectTransform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rect rect;
				RectTransform.get_rect_Injected(intPtr, out rect);
				return rect;
			}
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x060024FE RID: 9470 RVA: 0x00047794 File Offset: 0x00045994
		// (set) Token: 0x060024FF RID: 9471 RVA: 0x000477BC File Offset: 0x000459BC
		public Vector2 anchorMin
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RectTransform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				RectTransform.get_anchorMin_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RectTransform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RectTransform.set_anchorMin_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06002500 RID: 9472 RVA: 0x000477E0 File Offset: 0x000459E0
		// (set) Token: 0x06002501 RID: 9473 RVA: 0x00047808 File Offset: 0x00045A08
		public Vector2 anchorMax
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RectTransform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				RectTransform.get_anchorMax_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RectTransform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RectTransform.set_anchorMax_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06002502 RID: 9474 RVA: 0x0004782C File Offset: 0x00045A2C
		// (set) Token: 0x06002503 RID: 9475 RVA: 0x00047854 File Offset: 0x00045A54
		public Vector2 anchoredPosition
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RectTransform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				RectTransform.get_anchoredPosition_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RectTransform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RectTransform.set_anchoredPosition_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06002504 RID: 9476 RVA: 0x00047878 File Offset: 0x00045A78
		// (set) Token: 0x06002505 RID: 9477 RVA: 0x000478A0 File Offset: 0x00045AA0
		public Vector2 sizeDelta
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RectTransform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				RectTransform.get_sizeDelta_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RectTransform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RectTransform.set_sizeDelta_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06002506 RID: 9478 RVA: 0x000478C4 File Offset: 0x00045AC4
		// (set) Token: 0x06002507 RID: 9479 RVA: 0x000478EC File Offset: 0x00045AEC
		public Vector2 pivot
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RectTransform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				RectTransform.get_pivot_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RectTransform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RectTransform.set_pivot_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06002508 RID: 9480 RVA: 0x00047910 File Offset: 0x00045B10
		// (set) Token: 0x06002509 RID: 9481 RVA: 0x00047948 File Offset: 0x00045B48
		public Vector3 anchoredPosition3D
		{
			get
			{
				Vector2 anchoredPosition = this.anchoredPosition;
				return new Vector3(anchoredPosition.x, anchoredPosition.y, base.localPosition.z);
			}
			set
			{
				this.anchoredPosition = new Vector2(value.x, value.y);
				Vector3 localPosition = base.localPosition;
				localPosition.z = value.z;
				base.localPosition = localPosition;
			}
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x0600250A RID: 9482 RVA: 0x0004798C File Offset: 0x00045B8C
		// (set) Token: 0x0600250B RID: 9483 RVA: 0x000479BC File Offset: 0x00045BBC
		public Vector2 offsetMin
		{
			get
			{
				return this.anchoredPosition - Vector2.Scale(this.sizeDelta, this.pivot);
			}
			set
			{
				Vector2 vector = value - (this.anchoredPosition - Vector2.Scale(this.sizeDelta, this.pivot));
				this.sizeDelta -= vector;
				this.anchoredPosition += Vector2.Scale(vector, Vector2.one - this.pivot);
			}
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x0600250C RID: 9484 RVA: 0x00047A28 File Offset: 0x00045C28
		// (set) Token: 0x0600250D RID: 9485 RVA: 0x00047A60 File Offset: 0x00045C60
		public Vector2 offsetMax
		{
			get
			{
				return this.anchoredPosition + Vector2.Scale(this.sizeDelta, Vector2.one - this.pivot);
			}
			set
			{
				Vector2 vector = value - (this.anchoredPosition + Vector2.Scale(this.sizeDelta, Vector2.one - this.pivot));
				this.sizeDelta += vector;
				this.anchoredPosition += Vector2.Scale(vector, this.pivot);
			}
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x0600250E RID: 9486 RVA: 0x00047ACC File Offset: 0x00045CCC
		// (set) Token: 0x0600250F RID: 9487 RVA: 0x00047AF4 File Offset: 0x00045CF4
		public Object drivenByObject
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RectTransform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Object>(RectTransform.get_drivenByObject_Injected(intPtr));
			}
			internal set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RectTransform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RectTransform.set_drivenByObject_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Object>(value));
			}
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06002510 RID: 9488 RVA: 0x00047B1C File Offset: 0x00045D1C
		// (set) Token: 0x06002511 RID: 9489 RVA: 0x00047B40 File Offset: 0x00045D40
		internal DrivenTransformProperties drivenProperties
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RectTransform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return RectTransform.get_drivenProperties_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RectTransform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RectTransform.set_drivenProperties_Injected(intPtr, value);
			}
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06002512 RID: 9490 RVA: 0x00047B64 File Offset: 0x00045D64
		// (set) Token: 0x06002513 RID: 9491 RVA: 0x00047B88 File Offset: 0x00045D88
		public bool sendChildDimensionsChange
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RectTransform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return RectTransform.get_sendChildDimensionsChange_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RectTransform>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RectTransform.set_sendChildDimensionsChange_Injected(intPtr, value);
			}
		}

		// Token: 0x06002514 RID: 9492 RVA: 0x00047BAC File Offset: 0x00045DAC
		[NativeMethod("UpdateIfTransformDispatchIsDirty")]
		public void ForceUpdateRectTransforms()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RectTransform>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			RectTransform.ForceUpdateRectTransforms_Injected(intPtr);
		}

		// Token: 0x06002515 RID: 9493 RVA: 0x00047BD0 File Offset: 0x00045DD0
		public void GetLocalCorners(Vector3[] fourCornersArray)
		{
			bool flag = fourCornersArray == null || fourCornersArray.Length < 4;
			if (flag)
			{
				Debug.LogError("Calling GetLocalCorners with an array that is null or has less than 4 elements.");
			}
			else
			{
				Rect rect = this.rect;
				float x = rect.x;
				float y = rect.y;
				float xMax = rect.xMax;
				float yMax = rect.yMax;
				fourCornersArray[0] = new Vector3(x, y, 0f);
				fourCornersArray[1] = new Vector3(x, yMax, 0f);
				fourCornersArray[2] = new Vector3(xMax, yMax, 0f);
				fourCornersArray[3] = new Vector3(xMax, y, 0f);
			}
		}

		// Token: 0x06002516 RID: 9494 RVA: 0x00047C74 File Offset: 0x00045E74
		public void GetWorldCorners(Vector3[] fourCornersArray)
		{
			bool flag = fourCornersArray == null || fourCornersArray.Length < 4;
			if (flag)
			{
				Debug.LogError("Calling GetWorldCorners with an array that is null or has less than 4 elements.");
			}
			else
			{
				this.GetLocalCorners(fourCornersArray);
				Matrix4x4 localToWorldMatrix = base.localToWorldMatrix;
				for (int i = 0; i < 4; i++)
				{
					fourCornersArray[i] = localToWorldMatrix.MultiplyPoint(fourCornersArray[i]);
				}
			}
		}

		// Token: 0x06002517 RID: 9495 RVA: 0x00047CD4 File Offset: 0x00045ED4
		public void SetInsetAndSizeFromParentEdge(RectTransform.Edge edge, float inset, float size)
		{
			int num = ((edge == RectTransform.Edge.Top || edge == RectTransform.Edge.Bottom) ? 1 : 0);
			bool flag = edge == RectTransform.Edge.Top || edge == RectTransform.Edge.Right;
			float num2 = (float)(flag ? 1 : 0);
			Vector2 vector = this.anchorMin;
			vector[num] = num2;
			this.anchorMin = vector;
			vector = this.anchorMax;
			vector[num] = num2;
			this.anchorMax = vector;
			Vector2 sizeDelta = this.sizeDelta;
			sizeDelta[num] = size;
			this.sizeDelta = sizeDelta;
			Vector2 anchoredPosition = this.anchoredPosition;
			anchoredPosition[num] = (flag ? (-inset - size * (1f - this.pivot[num])) : (inset + size * this.pivot[num]));
			this.anchoredPosition = anchoredPosition;
		}

		// Token: 0x06002518 RID: 9496 RVA: 0x00047DA0 File Offset: 0x00045FA0
		public void SetSizeWithCurrentAnchors(RectTransform.Axis axis, float size)
		{
			Vector2 sizeDelta = this.sizeDelta;
			sizeDelta[(int)axis] = size - this.GetParentSize()[(int)axis] * (this.anchorMax[(int)axis] - this.anchorMin[(int)axis]);
			this.sizeDelta = sizeDelta;
		}

		// Token: 0x06002519 RID: 9497 RVA: 0x00047DF9 File Offset: 0x00045FF9
		[RequiredByNativeCode]
		internal static void SendReapplyDrivenProperties(RectTransform driven)
		{
			RectTransform.ReapplyDrivenProperties reapplyDrivenProperties = RectTransform.reapplyDrivenProperties;
			if (reapplyDrivenProperties != null)
			{
				reapplyDrivenProperties(driven);
			}
		}

		// Token: 0x0600251A RID: 9498 RVA: 0x00047E10 File Offset: 0x00046010
		internal Rect GetRectInParentSpace()
		{
			Rect rect = this.rect;
			Vector2 vector = this.offsetMin + Vector2.Scale(this.pivot, rect.size);
			bool flag = base.parent;
			if (flag)
			{
				RectTransform component = base.parent.GetComponent<RectTransform>();
				bool flag2 = component;
				if (flag2)
				{
					vector += Vector2.Scale(this.anchorMin, component.rect.size);
				}
			}
			rect.x += vector.x;
			rect.y += vector.y;
			return rect;
		}

		// Token: 0x0600251B RID: 9499 RVA: 0x00047EC0 File Offset: 0x000460C0
		private Vector2 GetParentSize()
		{
			RectTransform rectTransform = base.parent as RectTransform;
			bool flag = !rectTransform;
			Vector2 vector;
			if (flag)
			{
				vector = Vector2.zero;
			}
			else
			{
				vector = rectTransform.rect.size;
			}
			return vector;
		}

		// Token: 0x0600251D RID: 9501
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_rect_Injected(IntPtr _unity_self, out Rect ret);

		// Token: 0x0600251E RID: 9502
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_anchorMin_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x0600251F RID: 9503
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_anchorMin_Injected(IntPtr _unity_self, [In] ref Vector2 value);

		// Token: 0x06002520 RID: 9504
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_anchorMax_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x06002521 RID: 9505
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_anchorMax_Injected(IntPtr _unity_self, [In] ref Vector2 value);

		// Token: 0x06002522 RID: 9506
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_anchoredPosition_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x06002523 RID: 9507
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_anchoredPosition_Injected(IntPtr _unity_self, [In] ref Vector2 value);

		// Token: 0x06002524 RID: 9508
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_sizeDelta_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x06002525 RID: 9509
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_sizeDelta_Injected(IntPtr _unity_self, [In] ref Vector2 value);

		// Token: 0x06002526 RID: 9510
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_pivot_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x06002527 RID: 9511
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_pivot_Injected(IntPtr _unity_self, [In] ref Vector2 value);

		// Token: 0x06002528 RID: 9512
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_drivenByObject_Injected(IntPtr _unity_self);

		// Token: 0x06002529 RID: 9513
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_drivenByObject_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x0600252A RID: 9514
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern DrivenTransformProperties get_drivenProperties_Injected(IntPtr _unity_self);

		// Token: 0x0600252B RID: 9515
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_drivenProperties_Injected(IntPtr _unity_self, DrivenTransformProperties value);

		// Token: 0x0600252C RID: 9516
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_sendChildDimensionsChange_Injected(IntPtr _unity_self);

		// Token: 0x0600252D RID: 9517
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_sendChildDimensionsChange_Injected(IntPtr _unity_self, bool value);

		// Token: 0x0600252E RID: 9518
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ForceUpdateRectTransforms_Injected(IntPtr _unity_self);

		// Token: 0x020002F6 RID: 758
		public enum Edge
		{
			// Token: 0x04000A98 RID: 2712
			Left,
			// Token: 0x04000A99 RID: 2713
			Right,
			// Token: 0x04000A9A RID: 2714
			Top,
			// Token: 0x04000A9B RID: 2715
			Bottom
		}

		// Token: 0x020002F7 RID: 759
		public enum Axis
		{
			// Token: 0x04000A9D RID: 2717
			Horizontal,
			// Token: 0x04000A9E RID: 2718
			Vertical
		}

		// Token: 0x020002F8 RID: 760
		// (Invoke) Token: 0x06002530 RID: 9520
		public delegate void ReapplyDrivenProperties(RectTransform driven);
	}
}
