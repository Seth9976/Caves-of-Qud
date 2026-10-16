using System;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x020006CD RID: 1741
	internal abstract class HierarchyTraversal
	{
		// Token: 0x06003226 RID: 12838 RVA: 0x000BD5C7 File Offset: 0x000BB7C7
		public virtual void Traverse(VisualElement element)
		{
			this.TraverseRecursive(element, 0);
		}

		// Token: 0x06003227 RID: 12839
		public abstract void TraverseRecursive(VisualElement element, int depth);

		// Token: 0x06003228 RID: 12840 RVA: 0x000BD5D4 File Offset: 0x000BB7D4
		protected void Recurse(VisualElement element, int depth)
		{
			int i = 0;
			while (i < element.hierarchy.childCount)
			{
				VisualElement visualElement = element.hierarchy[i];
				this.TraverseRecursive(visualElement, depth + 1);
				bool flag = visualElement.hierarchy.parent != element;
				if (!flag)
				{
					i++;
				}
			}
		}
	}
}
