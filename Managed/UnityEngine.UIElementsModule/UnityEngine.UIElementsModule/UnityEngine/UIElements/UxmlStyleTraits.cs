using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000539 RID: 1337
	[Obsolete("UxmlStyleTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
	public class UxmlStyleTraits : UxmlTraits
	{
		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x0600290C RID: 10508 RVA: 0x00095290 File Offset: 0x00093490
		public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
		{
			get
			{
				yield break;
			}
		}

		// Token: 0x0400126B RID: 4715
		private UxmlStringAttributeDescription m_Name = new UxmlStringAttributeDescription
		{
			name = "name"
		};

		// Token: 0x0400126C RID: 4716
		private UxmlStringAttributeDescription m_Path = new UxmlStringAttributeDescription
		{
			name = "path"
		};

		// Token: 0x0400126D RID: 4717
		private UxmlStringAttributeDescription m_Src = new UxmlStringAttributeDescription
		{
			name = "src"
		};
	}
}
