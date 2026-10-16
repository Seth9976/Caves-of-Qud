using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200000B RID: 11
public class RightClickButton : Button
{
	// Token: 0x06000045 RID: 69 RVA: 0x00003308 File Offset: 0x00001508
	public override void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Left)
		{
			base.OnPointerClick(eventData);
			return;
		}
		if (eventData.button == PointerEventData.InputButton.Right)
		{
			this.m_OnRightClick.Invoke();
		}
	}

	// Token: 0x0400002B RID: 43
	[SerializeField]
	public UnityEvent m_OnRightClick = new RightClickButton.ButtonRightClickedEvent();

	// Token: 0x02001553 RID: 5459
	[Serializable]
	public class ButtonRightClickedEvent : UnityEvent
	{
	}
}
