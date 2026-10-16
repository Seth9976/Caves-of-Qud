using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x0200053F RID: 1343
	[Obsolete("UxmlAttributeOverridesTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
	public class UxmlAttributeOverridesTraits : UxmlTraits
	{
		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x0600292E RID: 10542 RVA: 0x00095530 File Offset: 0x00093730
		public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
		{
			get
			{
				yield break;
			}
		}

		// Token: 0x0400127B RID: 4731
		internal const string k_ElementNameAttributeName = "element-name";

		// Token: 0x0400127C RID: 4732
		private UxmlStringAttributeDescription m_ElementName = new UxmlStringAttributeDescription
		{
			name = "element-name",
			use = UxmlAttributeDescription.Use.Required
		};
	}
}
