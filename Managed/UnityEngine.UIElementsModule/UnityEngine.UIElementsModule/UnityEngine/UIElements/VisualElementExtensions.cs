using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020005B3 RID: 1459
	public static class VisualElementExtensions
	{
		// Token: 0x06002B65 RID: 11109 RVA: 0x0009CB94 File Offset: 0x0009AD94
		public static void StretchToParentSize(this VisualElement elem)
		{
			bool flag = elem == null;
			if (flag)
			{
				throw new ArgumentNullException("elem");
			}
			IStyle style = elem.style;
			style.position = Position.Absolute;
			style.left = 0f;
			style.top = 0f;
			style.right = 0f;
			style.bottom = 0f;
		}

		// Token: 0x06002B66 RID: 11110 RVA: 0x0009CC10 File Offset: 0x0009AE10
		public static void StretchToParentWidth(this VisualElement elem)
		{
			bool flag = elem == null;
			if (flag)
			{
				throw new ArgumentNullException("elem");
			}
			IStyle style = elem.style;
			style.position = Position.Absolute;
			style.left = 0f;
			style.right = 0f;
		}

		// Token: 0x06002B67 RID: 11111 RVA: 0x0009CC68 File Offset: 0x0009AE68
		public static void AddManipulator(this VisualElement ele, IManipulator manipulator)
		{
			bool flag = manipulator != null;
			if (flag)
			{
				manipulator.target = ele;
			}
		}

		// Token: 0x06002B68 RID: 11112 RVA: 0x0009CC88 File Offset: 0x0009AE88
		public static void RemoveManipulator(this VisualElement ele, IManipulator manipulator)
		{
			bool flag = manipulator != null;
			if (flag)
			{
				manipulator.target = null;
			}
		}

		// Token: 0x06002B69 RID: 11113 RVA: 0x0009CCA8 File Offset: 0x0009AEA8
		public static Vector2 WorldToLocal(this VisualElement ele, Vector2 p)
		{
			bool flag = ele == null;
			if (flag)
			{
				throw new ArgumentNullException("ele");
			}
			return VisualElement.MultiplyMatrix44Point2(ele.worldTransformInverse, p);
		}

		// Token: 0x06002B6A RID: 11114 RVA: 0x0009CCDC File Offset: 0x0009AEDC
		public static Vector2 LocalToWorld(this VisualElement ele, Vector2 p)
		{
			bool flag = ele == null;
			if (flag)
			{
				throw new ArgumentNullException("ele");
			}
			return VisualElement.MultiplyMatrix44Point2(ele.worldTransformRef, p);
		}

		// Token: 0x06002B6B RID: 11115 RVA: 0x0009CD10 File Offset: 0x0009AF10
		public static Rect WorldToLocal(this VisualElement ele, Rect r)
		{
			bool flag = ele == null;
			if (flag)
			{
				throw new ArgumentNullException("ele");
			}
			return VisualElement.CalculateConservativeRect(ele.worldTransformInverse, r);
		}

		// Token: 0x06002B6C RID: 11116 RVA: 0x0009CD44 File Offset: 0x0009AF44
		public static Rect LocalToWorld(this VisualElement ele, Rect r)
		{
			bool flag = ele == null;
			if (flag)
			{
				throw new ArgumentNullException("ele");
			}
			return VisualElement.CalculateConservativeRect(ele.worldTransformRef, r);
		}

		// Token: 0x06002B6D RID: 11117 RVA: 0x0009CD78 File Offset: 0x0009AF78
		public static Vector2 ChangeCoordinatesTo(this VisualElement src, VisualElement dest, Vector2 point)
		{
			return dest.WorldToLocal(src.LocalToWorld(point));
		}

		// Token: 0x06002B6E RID: 11118 RVA: 0x0009CD98 File Offset: 0x0009AF98
		public static Rect ChangeCoordinatesTo(this VisualElement src, VisualElement dest, Rect rect)
		{
			return dest.WorldToLocal(src.LocalToWorld(rect));
		}
	}
}
