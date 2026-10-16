using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000155 RID: 341
[AddComponentMenu("Accessibility/Helper/Group Focus Notification")]
public class UAP_SelectionGroup : MonoBehaviour
{
	// Token: 0x06000D9A RID: 3482 RVA: 0x000585F2 File Offset: 0x000567F2
	public void AddElement(UAP_BaseElement element)
	{
		if (!this.m_AllElements.Contains(element))
		{
			this.m_AllElements.Add(element);
		}
	}

	// Token: 0x06000D9B RID: 3483 RVA: 0x0005860E File Offset: 0x0005680E
	public void RemoveElement(UAP_BaseElement element)
	{
		if (!this.m_AllElements.Contains(element))
		{
			this.m_AllElements.Remove(element);
		}
	}

	// Token: 0x06000D9C RID: 3484 RVA: 0x0005862C File Offset: 0x0005682C
	private void Update()
	{
		GameObject currentFocusObject = UAP_AccessibilityManager.GetCurrentFocusObject();
		if (currentFocusObject == this.m_LastFocusObject)
		{
			return;
		}
		this.m_LastFocusObject = currentFocusObject;
		bool flag = false;
		if (UAP_AccessibilityManager.IsEnabled() && currentFocusObject != null)
		{
			UAP_BaseElement component = currentFocusObject.GetComponent<UAP_BaseElement>();
			if (this.m_AllElements.Contains(component))
			{
				flag = true;
			}
		}
		if (flag != this.m_Selected)
		{
			this.m_Selected = flag;
			base.gameObject.BroadcastMessage("Accessibility_Selected", flag, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x06000D9D RID: 3485 RVA: 0x000586A8 File Offset: 0x000568A8
	private void OnDisable()
	{
		bool flag = false;
		base.gameObject.BroadcastMessage("Accessibility_Selected", flag, SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x06000D9E RID: 3486 RVA: 0x000586D0 File Offset: 0x000568D0
	private void OnDestroy()
	{
		bool flag = false;
		base.gameObject.BroadcastMessage("Accessibility_Selected", flag, SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x04000967 RID: 2407
	private List<UAP_BaseElement> m_AllElements = new List<UAP_BaseElement>();

	// Token: 0x04000968 RID: 2408
	private bool m_Selected;

	// Token: 0x04000969 RID: 2409
	private GameObject m_LastFocusObject;
}
