using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x0200053C RID: 1340
	[Obsolete("UxmlTemplateTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
	public class UxmlTemplateTraits : UxmlTraits
	{
		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x0600291D RID: 10525 RVA: 0x000953DC File Offset: 0x000935DC
		public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
		{
			get
			{
				yield break;
			}
		}

		// Token: 0x04001273 RID: 4723
		private UxmlStringAttributeDescription m_Name = new UxmlStringAttributeDescription
		{
			name = "name",
			use = UxmlAttributeDescription.Use.Required
		};

		// Token: 0x04001274 RID: 4724
		private UxmlStringAttributeDescription m_Path = new UxmlStringAttributeDescription
		{
			name = "path"
		};

		// Token: 0x04001275 RID: 4725
		private UxmlStringAttributeDescription m_Src = new UxmlStringAttributeDescription
		{
			name = "src"
		};
	}
}
