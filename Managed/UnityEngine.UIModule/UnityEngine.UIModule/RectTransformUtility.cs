using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000005 RID: 5
	[NativeHeader("Runtime/Camera/Camera.h")]
	[NativeHeader("Modules/UI/Canvas.h")]
	[NativeHeader("Modules/UI/RectTransformUtil.h")]
	[NativeHeader("Runtime/Transform/RectTransform.h")]
	[StaticAccessor("UI", StaticAccessorType.DoubleColon)]
	public sealed class RectTransformUtility
	{
		// Token: 0x06000062 RID: 98 RVA: 0x000029A0 File Offset: 0x00000BA0
		public static Vector2 PixelAdjustPoint(Vector2 point, Transform elementTransform, Canvas canvas)
		{
			Vector2 vector;
			RectTransformUtility.PixelAdjustPoint_Injected(ref point, Object.MarshalledUnityObject.Marshal<Transform>(elementTransform), Object.MarshalledUnityObject.Marshal<Canvas>(canvas), out vector);
			return vector;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000029C4 File Offset: 0x00000BC4
		public static Rect PixelAdjustRect(RectTransform rectTransform, Canvas canvas)
		{
			Rect rect;
			RectTransformUtility.PixelAdjustRect_Injected(Object.MarshalledUnityObject.Marshal<RectTransform>(rectTransform), Object.MarshalledUnityObject.Marshal<Canvas>(canvas), out rect);
			return rect;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000029E8 File Offset: 0x00000BE8
		private static bool PointInRectangle(Vector2 screenPoint, RectTransform rect, Camera cam, Vector4 offset)
		{
			return RectTransformUtility.PointInRectangle_Injected(ref screenPoint, Object.MarshalledUnityObject.Marshal<RectTransform>(rect), Object.MarshalledUnityObject.Marshal<Camera>(cam), ref offset);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002A0A File Offset: 0x00000C0A
		private RectTransformUtility()
		{
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002A14 File Offset: 0x00000C14
		public static bool RectangleContainsScreenPoint(RectTransform rect, Vector2 screenPoint)
		{
			return RectTransformUtility.RectangleContainsScreenPoint(rect, screenPoint, null);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002A30 File Offset: 0x00000C30
		public static bool RectangleContainsScreenPoint(RectTransform rect, Vector2 screenPoint, Camera cam)
		{
			return RectTransformUtility.RectangleContainsScreenPoint(rect, screenPoint, cam, Vector4.zero);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002A50 File Offset: 0x00000C50
		public static bool RectangleContainsScreenPoint(RectTransform rect, Vector2 screenPoint, Camera cam, Vector4 offset)
		{
			return RectTransformUtility.PointInRectangle(screenPoint, rect, cam, offset);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002A6C File Offset: 0x00000C6C
		public static bool ScreenPointToWorldPointInRectangle(RectTransform rect, Vector2 screenPoint, Camera cam, out Vector3 worldPoint)
		{
			worldPoint = Vector2.zero;
			Ray ray = RectTransformUtility.ScreenPointToRay(cam, screenPoint);
			Plane plane = new Plane(rect.rotation * Vector3.back, rect.position);
			float num = 0f;
			float num2 = Vector3.Dot(Vector3.Normalize(rect.position - ray.origin), plane.normal);
			bool flag = num2 != 0f && !plane.Raycast(ray, out num);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				worldPoint = ray.GetPoint(num);
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002B14 File Offset: 0x00000D14
		public static bool ScreenPointToLocalPointInRectangle(RectTransform rect, Vector2 screenPoint, Camera cam, out Vector2 localPoint)
		{
			localPoint = Vector2.zero;
			Vector3 vector;
			bool flag = RectTransformUtility.ScreenPointToWorldPointInRectangle(rect, screenPoint, cam, out vector);
			bool flag2;
			if (flag)
			{
				localPoint = rect.InverseTransformPoint(vector);
				flag2 = true;
			}
			else
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002B58 File Offset: 0x00000D58
		public static Ray ScreenPointToRay(Camera cam, Vector2 screenPos)
		{
			bool flag = cam != null;
			Ray ray;
			if (flag)
			{
				ray = cam.ScreenPointToRay(screenPos);
			}
			else
			{
				Vector3 vector = screenPos;
				vector.z -= 100f;
				ray = new Ray(vector, Vector3.forward);
			}
			return ray;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002BA8 File Offset: 0x00000DA8
		public static Vector2 WorldToScreenPoint(Camera cam, Vector3 worldPoint)
		{
			bool flag = cam == null;
			Vector2 vector;
			if (flag)
			{
				vector = new Vector2(worldPoint.x, worldPoint.y);
			}
			else
			{
				vector = cam.WorldToScreenPoint(worldPoint);
			}
			return vector;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002BE8 File Offset: 0x00000DE8
		public static Bounds CalculateRelativeRectTransformBounds(Transform root, Transform child)
		{
			RectTransform[] componentsInChildren = child.GetComponentsInChildren<RectTransform>(false);
			bool flag = componentsInChildren.Length != 0;
			Bounds bounds2;
			if (flag)
			{
				Vector3 vector = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
				Vector3 vector2 = new Vector3(float.MinValue, float.MinValue, float.MinValue);
				Matrix4x4 worldToLocalMatrix = root.worldToLocalMatrix;
				int i = 0;
				int num = componentsInChildren.Length;
				while (i < num)
				{
					componentsInChildren[i].GetWorldCorners(RectTransformUtility.s_Corners);
					for (int j = 0; j < 4; j++)
					{
						Vector3 vector3 = worldToLocalMatrix.MultiplyPoint3x4(RectTransformUtility.s_Corners[j]);
						vector = Vector3.Min(vector3, vector);
						vector2 = Vector3.Max(vector3, vector2);
					}
					i++;
				}
				Bounds bounds = new Bounds(vector, Vector3.zero);
				bounds.Encapsulate(vector2);
				bounds2 = bounds;
			}
			else
			{
				bounds2 = new Bounds(Vector3.zero, Vector3.zero);
			}
			return bounds2;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002CDC File Offset: 0x00000EDC
		public static Bounds CalculateRelativeRectTransformBounds(Transform trans)
		{
			return RectTransformUtility.CalculateRelativeRectTransformBounds(trans, trans);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002CF8 File Offset: 0x00000EF8
		public static void FlipLayoutOnAxis(RectTransform rect, int axis, bool keepPositioning, bool recursive)
		{
			bool flag = rect == null;
			if (!flag)
			{
				if (recursive)
				{
					for (int i = 0; i < rect.childCount; i++)
					{
						RectTransform rectTransform = rect.GetChild(i) as RectTransform;
						bool flag2 = rectTransform != null;
						if (flag2)
						{
							RectTransformUtility.FlipLayoutOnAxis(rectTransform, axis, false, true);
						}
					}
				}
				Vector2 pivot = rect.pivot;
				pivot[axis] = 1f - pivot[axis];
				rect.pivot = pivot;
				if (!keepPositioning)
				{
					Vector2 anchoredPosition = rect.anchoredPosition;
					anchoredPosition[axis] = -anchoredPosition[axis];
					rect.anchoredPosition = anchoredPosition;
					Vector2 anchorMin = rect.anchorMin;
					Vector2 anchorMax = rect.anchorMax;
					float num = anchorMin[axis];
					anchorMin[axis] = 1f - anchorMax[axis];
					anchorMax[axis] = 1f - num;
					rect.anchorMin = anchorMin;
					rect.anchorMax = anchorMax;
				}
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002E08 File Offset: 0x00001008
		public static void FlipLayoutAxes(RectTransform rect, bool keepPositioning, bool recursive)
		{
			bool flag = rect == null;
			if (!flag)
			{
				if (recursive)
				{
					for (int i = 0; i < rect.childCount; i++)
					{
						RectTransform rectTransform = rect.GetChild(i) as RectTransform;
						bool flag2 = rectTransform != null;
						if (flag2)
						{
							RectTransformUtility.FlipLayoutAxes(rectTransform, false, true);
						}
					}
				}
				rect.pivot = RectTransformUtility.GetTransposed(rect.pivot);
				rect.sizeDelta = RectTransformUtility.GetTransposed(rect.sizeDelta);
				if (!keepPositioning)
				{
					rect.anchoredPosition = RectTransformUtility.GetTransposed(rect.anchoredPosition);
					rect.anchorMin = RectTransformUtility.GetTransposed(rect.anchorMin);
					rect.anchorMax = RectTransformUtility.GetTransposed(rect.anchorMax);
				}
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002ECC File Offset: 0x000010CC
		private static Vector2 GetTransposed(Vector2 input)
		{
			return new Vector2(input.y, input.x);
		}

		// Token: 0x06000073 RID: 115
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void PixelAdjustPoint_Injected([In] ref Vector2 point, IntPtr elementTransform, IntPtr canvas, out Vector2 ret);

		// Token: 0x06000074 RID: 116
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void PixelAdjustRect_Injected(IntPtr rectTransform, IntPtr canvas, out Rect ret);

		// Token: 0x06000075 RID: 117
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool PointInRectangle_Injected([In] ref Vector2 screenPoint, IntPtr rect, IntPtr cam, [In] ref Vector4 offset);

		// Token: 0x04000002 RID: 2
		private static readonly Vector3[] s_Corners = new Vector3[4];
	}
}
