using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200009A RID: 154
public class ScrollRectPosition : MonoBehaviour
{
	// Token: 0x06000528 RID: 1320 RVA: 0x00021845 File Offset: 0x0001FA45
	private void Start()
	{
		this.scrollRectTransform = base.GetComponent<RectTransform>();
		this.contentPanel = base.GetComponent<ScrollRect>().content;
	}

	// Token: 0x06000529 RID: 1321 RVA: 0x00021864 File Offset: 0x0001FA64
	private void Update()
	{
		GameObject currentSelectedGameObject = EventSystem.current.currentSelectedGameObject;
		if (currentSelectedGameObject == null)
		{
			return;
		}
		float num = 0f;
		if (currentSelectedGameObject.transform.parent != this.contentPanel.transform)
		{
			if (currentSelectedGameObject.transform.parent == null)
			{
				return;
			}
			if (currentSelectedGameObject.transform.parent.parent != this.contentPanel.transform)
			{
				return;
			}
			num = -(currentSelectedGameObject.transform.parent.GetComponent<RectTransform>().anchoredPosition.y - 50f);
		}
		if (currentSelectedGameObject == this.lastSelected)
		{
			return;
		}
		this.selectedRectTransform = currentSelectedGameObject.GetComponent<RectTransform>();
		float num2 = Mathf.Abs(this.selectedRectTransform.anchoredPosition.y) + this.selectedRectTransform.rect.height / 2f + num;
		float num3 = Mathf.Abs(this.selectedRectTransform.anchoredPosition.y) - this.selectedRectTransform.rect.height / 2f + num;
		float y = this.contentPanel.anchoredPosition.y;
		float num4 = this.contentPanel.anchoredPosition.y + this.scrollRectTransform.rect.height;
		if (num2 > num4)
		{
			float num5 = num2 - this.scrollRectTransform.rect.height;
			this.contentPanel.anchoredPosition = new Vector2(this.contentPanel.anchoredPosition.x, num5);
		}
		else if (num3 < y)
		{
			this.contentPanel.anchoredPosition = new Vector2(this.contentPanel.anchoredPosition.x, num3 - this.selectedRectTransform.sizeDelta.y);
		}
		this.lastSelected = currentSelectedGameObject;
	}

	// Token: 0x040003F8 RID: 1016
	private RectTransform scrollRectTransform;

	// Token: 0x040003F9 RID: 1017
	private RectTransform contentPanel;

	// Token: 0x040003FA RID: 1018
	private RectTransform selectedRectTransform;

	// Token: 0x040003FB RID: 1019
	private GameObject lastSelected;
}
