using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020005CA RID: 1482
	internal static class VisualElementUtils
	{
		// Token: 0x06002BC6 RID: 11206 RVA: 0x0009E150 File Offset: 0x0009C350
		public static string GetUniqueName(string nameBase)
		{
			string text = nameBase;
			int num = 2;
			while (VisualElementUtils.s_usedNames.Contains(text))
			{
				text = nameBase + num.ToString();
				num++;
			}
			VisualElementUtils.s_usedNames.Add(text);
			return text;
		}

		// Token: 0x06002BC7 RID: 11207 RVA: 0x0009E198 File Offset: 0x0009C398
		internal static int GetFoldoutDepth(this VisualElement element)
		{
			int num = 0;
			bool flag = element.parent != null;
			if (flag)
			{
				for (VisualElement visualElement = element.parent; visualElement != null; visualElement = visualElement.parent)
				{
					bool flag2 = VisualElementUtils.s_FoldoutType.IsAssignableFrom(visualElement.GetType());
					if (flag2)
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x06002BC8 RID: 11208 RVA: 0x0009E1F4 File Offset: 0x0009C3F4
		internal static void AssignInspectorStyleIfNecessary(this VisualElement element, string classNameToEnable)
		{
			VisualElement firstAncestorWhere = element.GetFirstAncestorWhere((VisualElement i) => i.ClassListContains(VisualElementUtils.s_InspectorElementUssClassName));
			element.EnableInClassList(classNameToEnable, firstAncestorWhere != null);
		}

		// Token: 0x040013EA RID: 5098
		private static readonly HashSet<string> s_usedNames = new HashSet<string>();

		// Token: 0x040013EB RID: 5099
		private static readonly Type s_FoldoutType = typeof(Foldout);

		// Token: 0x040013EC RID: 5100
		private static readonly string s_InspectorElementUssClassName = "unity-inspector-element";
	}
}
