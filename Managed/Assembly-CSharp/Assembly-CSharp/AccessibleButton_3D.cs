using System;
using UnityEngine;

// Token: 0x02000148 RID: 328
[AddComponentMenu("Accessibility/UI/Accessible Button 3D")]
public class AccessibleButton_3D : UAP_BaseElement_3D
{
	// Token: 0x06000D16 RID: 3350 RVA: 0x000561C6 File Offset: 0x000543C6
	private AccessibleButton_3D()
	{
		this.m_Type = AccessibleUIGroupRoot.EUIElement.EButton;
	}

	// Token: 0x06000D17 RID: 3351 RVA: 0x000561D5 File Offset: 0x000543D5
	public override bool IsElementActive()
	{
		return base.IsElementActive() && (!(this.m_ReferenceElement != null) || this.m_ReferenceElement.gameObject.activeInHierarchy) && (UAP_AccessibilityManager.GetSpeakDisabledInteractables() || this.IsInteractable());
	}

	// Token: 0x06000D18 RID: 3352 RVA: 0x00056215 File Offset: 0x00054415
	public override bool IsInteractable()
	{
		return true;
	}
}
