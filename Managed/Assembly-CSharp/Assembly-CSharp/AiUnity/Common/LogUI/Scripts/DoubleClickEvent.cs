using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace AiUnity.Common.LogUI.Scripts
{
	// Token: 0x020002C6 RID: 710
	public class DoubleClickEvent : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
	{
		// Token: 0x0600189B RID: 6299 RVA: 0x0007F812 File Offset: 0x0007DA12
		public void OnPointerClick(PointerEventData eventData)
		{
			int clickCount = eventData.clickCount;
		}

		// Token: 0x04000E97 RID: 3735
		public UnityEvent doubleClickEvent;
	}
}
