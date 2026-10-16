using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020002CD RID: 717
	internal static class UIDocumentHierarchyUtil
	{
		// Token: 0x060014AF RID: 5295 RVA: 0x00052E88 File Offset: 0x00051088
		internal static int FindHierarchicalSortedIndex(SortedDictionary<UIDocumentHierarchicalIndex, UIDocument> children, UIDocument child)
		{
			int num = 0;
			foreach (UIDocument uidocument in children.Values)
			{
				bool flag = uidocument == child;
				if (flag)
				{
					return num;
				}
				bool flag2 = uidocument.rootVisualElement != null && uidocument.rootVisualElement.parent != null;
				if (flag2)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x060014B0 RID: 5296 RVA: 0x00052F18 File Offset: 0x00051118
		internal static void SetHierarchicalIndex(Transform childTransform, Transform directParentTransform, Transform mainParentTransform, out UIDocumentHierarchicalIndex hierarchicalIndex)
		{
			bool flag = mainParentTransform == null || childTransform == null;
			if (flag)
			{
				hierarchicalIndex.pathToParent = null;
			}
			else
			{
				bool flag2 = directParentTransform == mainParentTransform;
				if (flag2)
				{
					hierarchicalIndex.pathToParent = new int[] { childTransform.GetSiblingIndex() };
				}
				else
				{
					List<int> list = new List<int>();
					while (mainParentTransform != childTransform && childTransform != null)
					{
						list.Add(childTransform.GetSiblingIndex());
						childTransform = childTransform.parent;
					}
					list.Reverse();
					hierarchicalIndex.pathToParent = list.ToArray();
				}
			}
		}

		// Token: 0x060014B1 RID: 5297 RVA: 0x00052FB8 File Offset: 0x000511B8
		internal static void SetGlobalIndex(Transform objectTransform, Transform directParentTransform, out UIDocumentHierarchicalIndex globalIndex)
		{
			bool flag = objectTransform == null;
			if (flag)
			{
				globalIndex.pathToParent = null;
			}
			else
			{
				bool flag2 = directParentTransform == null;
				if (flag2)
				{
					globalIndex.pathToParent = new int[] { objectTransform.GetSiblingIndex() };
				}
				else
				{
					List<int> list = new List<int> { objectTransform.GetSiblingIndex() };
					while (directParentTransform != null)
					{
						list.Add(directParentTransform.GetSiblingIndex());
						directParentTransform = directParentTransform.parent;
					}
					list.Reverse();
					globalIndex.pathToParent = list.ToArray();
				}
			}
		}

		// Token: 0x04000B39 RID: 2873
		internal static UIDocumentHierarchicalIndexComparer indexComparer = new UIDocumentHierarchicalIndexComparer();
	}
}
