using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020002CE RID: 718
	internal class UIDocumentHierarchicalIndexComparer : IComparer<UIDocumentHierarchicalIndex>
	{
		// Token: 0x060014B3 RID: 5299 RVA: 0x00053058 File Offset: 0x00051258
		public int Compare(UIDocumentHierarchicalIndex x, UIDocumentHierarchicalIndex y)
		{
			return x.CompareTo(y);
		}
	}
}
