using System;
using System.Collections.Generic;
using UnityEngine;

namespace XRL.UI.Framework
{
	// Token: 0x020014F3 RID: 5363
	public class CategoryIcons : FrameworkDataElement, IFrameworkDataList
	{
		// Token: 0x0600D199 RID: 53657 RVA: 0x004DE08A File Offset: 0x004DC28A
		public IEnumerable<FrameworkDataElement> getChildren()
		{
			return this.Choices;
		}

		// Token: 0x040052D1 RID: 21201
		public string Title;

		// Token: 0x040052D2 RID: 21202
		public Color TitleColor;

		// Token: 0x040052D3 RID: 21203
		public List<ChoiceWithColorIcon> Choices;
	}
}
