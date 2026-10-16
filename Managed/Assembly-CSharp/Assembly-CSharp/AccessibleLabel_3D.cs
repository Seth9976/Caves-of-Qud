using System;
using UnityEngine;

// Token: 0x0200014B RID: 331
[AddComponentMenu("Accessibility/UI/Accessible Label 3D")]
public class AccessibleLabel_3D : UAP_BaseElement_3D
{
	// Token: 0x06000D30 RID: 3376 RVA: 0x000569A7 File Offset: 0x00054BA7
	private AccessibleLabel_3D()
	{
		this.m_Type = AccessibleUIGroupRoot.EUIElement.ELabel;
	}

	// Token: 0x06000D31 RID: 3377 RVA: 0x000569B6 File Offset: 0x00054BB6
	protected override string GetMainText()
	{
		if (base.IsNameLocalizationKey())
		{
			return base.CombinePrefix(UAP_AccessibilityManager.Localize(this.m_Text));
		}
		return this.m_Text;
	}
}
