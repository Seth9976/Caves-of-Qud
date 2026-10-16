using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020004EF RID: 1263
	internal class TextJobSystem
	{
		// Token: 0x06002691 RID: 9873 RVA: 0x0008D548 File Offset: 0x0008B748
		internal void GenerateText(MeshGenerationContext mgc, TextElement textElement)
		{
			bool flag = TextUtilities.IsAdvancedTextEnabledForElement(textElement);
			if (flag)
			{
				if (this.m_ATGTextJobSystem == null)
				{
					this.m_ATGTextJobSystem = new ATGTextJobSystem();
				}
				this.m_ATGTextJobSystem.GenerateText(mgc, textElement);
			}
			else
			{
				if (this.m_UITKTextJobSystem == null)
				{
					this.m_UITKTextJobSystem = new UITKTextJobSystem();
				}
				this.m_UITKTextJobSystem.GenerateText(mgc, textElement);
			}
		}

		// Token: 0x0400117C RID: 4476
		internal UITKTextJobSystem m_UITKTextJobSystem;

		// Token: 0x0400117D RID: 4477
		private ATGTextJobSystem m_ATGTextJobSystem;
	}
}
