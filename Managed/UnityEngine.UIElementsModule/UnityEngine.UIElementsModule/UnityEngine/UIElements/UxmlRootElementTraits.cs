using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000536 RID: 1334
	[Obsolete("UxmlRootElementTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
	public class UxmlRootElementTraits : UxmlTraits
	{
		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x060028FB RID: 10491 RVA: 0x000950FC File Offset: 0x000932FC
		public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
		{
			get
			{
				yield return new UxmlChildElementDescription(typeof(VisualElement));
				yield break;
			}
		}

		// Token: 0x04001264 RID: 4708
		protected UxmlStringAttributeDescription m_Name = new UxmlStringAttributeDescription
		{
			name = "name"
		};

		// Token: 0x04001265 RID: 4709
		private UxmlStringAttributeDescription m_Class = new UxmlStringAttributeDescription
		{
			name = "class"
		};
	}
}
