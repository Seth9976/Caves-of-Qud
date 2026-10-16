using System;
using System.Collections.Generic;
using UnityEngine.Pool;

namespace UnityEngine.UI
{
	// Token: 0x02000028 RID: 40
	public static class LayoutUtility
	{
		// Token: 0x060002CD RID: 717 RVA: 0x0000F2C9 File Offset: 0x0000D4C9
		public static float GetMinSize(RectTransform rect, int axis)
		{
			if (axis != 0)
			{
				return LayoutUtility.GetMinHeight(rect);
			}
			return LayoutUtility.GetMinWidth(rect);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000F2DB File Offset: 0x0000D4DB
		public static float GetPreferredSize(RectTransform rect, int axis)
		{
			if (axis != 0)
			{
				return LayoutUtility.GetPreferredHeight(rect);
			}
			return LayoutUtility.GetPreferredWidth(rect);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000F2ED File Offset: 0x0000D4ED
		public static float GetFlexibleSize(RectTransform rect, int axis)
		{
			if (axis != 0)
			{
				return LayoutUtility.GetFlexibleHeight(rect);
			}
			return LayoutUtility.GetFlexibleWidth(rect);
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000F2FF File Offset: 0x0000D4FF
		public static float GetMinWidth(RectTransform rect)
		{
			return LayoutUtility.GetLayoutProperty(rect, (ILayoutElement e) => e.minWidth, 0f);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000F32C File Offset: 0x0000D52C
		public static float GetPreferredWidth(RectTransform rect)
		{
			return Mathf.Max(LayoutUtility.GetLayoutProperty(rect, (ILayoutElement e) => e.minWidth, 0f), LayoutUtility.GetLayoutProperty(rect, (ILayoutElement e) => e.preferredWidth, 0f));
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000F392 File Offset: 0x0000D592
		public static float GetFlexibleWidth(RectTransform rect)
		{
			return LayoutUtility.GetLayoutProperty(rect, (ILayoutElement e) => e.flexibleWidth, 0f);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000F3BE File Offset: 0x0000D5BE
		public static float GetMinHeight(RectTransform rect)
		{
			return LayoutUtility.GetLayoutProperty(rect, (ILayoutElement e) => e.minHeight, 0f);
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000F3EC File Offset: 0x0000D5EC
		public static float GetPreferredHeight(RectTransform rect)
		{
			return Mathf.Max(LayoutUtility.GetLayoutProperty(rect, (ILayoutElement e) => e.minHeight, 0f), LayoutUtility.GetLayoutProperty(rect, (ILayoutElement e) => e.preferredHeight, 0f));
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000F452 File Offset: 0x0000D652
		public static float GetFlexibleHeight(RectTransform rect)
		{
			return LayoutUtility.GetLayoutProperty(rect, (ILayoutElement e) => e.flexibleHeight, 0f);
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000F480 File Offset: 0x0000D680
		public static float GetLayoutProperty(RectTransform rect, Func<ILayoutElement, float> property, float defaultValue)
		{
			ILayoutElement layoutElement;
			return LayoutUtility.GetLayoutProperty(rect, property, defaultValue, out layoutElement);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000F498 File Offset: 0x0000D698
		public static float GetLayoutProperty(RectTransform rect, Func<ILayoutElement, float> property, float defaultValue, out ILayoutElement source)
		{
			source = null;
			if (rect == null)
			{
				return 0f;
			}
			float num = defaultValue;
			int num2 = int.MinValue;
			List<Component> list = CollectionPool<List<Component>, Component>.Get();
			rect.GetComponents(typeof(ILayoutElement), list);
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				ILayoutElement layoutElement = list[i] as ILayoutElement;
				if (!(layoutElement is Behaviour) || ((Behaviour)layoutElement).isActiveAndEnabled)
				{
					int layoutPriority = layoutElement.layoutPriority;
					if (layoutPriority >= num2)
					{
						float num3 = property(layoutElement);
						if (num3 >= 0f)
						{
							if (layoutPriority > num2)
							{
								num = num3;
								num2 = layoutPriority;
								source = layoutElement;
							}
							else if (num3 > num)
							{
								num = num3;
								source = layoutElement;
							}
						}
					}
				}
			}
			CollectionPool<List<Component>, Component>.Release(list);
			return num;
		}
	}
}
