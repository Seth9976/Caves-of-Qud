using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Qud.UI
{
	// Token: 0x0200049A RID: 1178
	public class MovableSceneFrameCornerTab : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IDragHandler
	{
		// Token: 0x06002D2C RID: 11564 RVA: 0x000F4207 File Offset: 0x000F2407
		public void OnPointerEnter(PointerEventData eventData)
		{
		}

		// Token: 0x06002D2D RID: 11565 RVA: 0x000F4209 File Offset: 0x000F2409
		public void OnPointerExit(PointerEventData eventData)
		{
		}

		// Token: 0x06002D2E RID: 11566 RVA: 0x000F420C File Offset: 0x000F240C
		public void OnDrag(PointerEventData eventData)
		{
			if (this.dragTarget.GetComponent<RectTransform>().anchorMin.x == 1f)
			{
				this.dragTarget.GetComponent<RectTransform>().sizeDelta += new Vector2(eventData.delta.x, -eventData.delta.y) / UIManager.scale;
				this.dragTarget.GetComponent<RectTransform>().anchoredPosition += new Vector2(eventData.delta.x, eventData.delta.y) / UIManager.scale;
				return;
			}
			if (this.dragTarget.GetComponent<RectTransform>().anchorMin.x == 0f)
			{
				this.dragTarget.GetComponent<RectTransform>().sizeDelta += new Vector2(eventData.delta.x, -eventData.delta.y) / UIManager.scale;
				this.dragTarget.GetComponent<RectTransform>().anchoredPosition += new Vector2(0f, eventData.delta.y) / UIManager.scale;
			}
		}

		// Token: 0x04001948 RID: 6472
		public GameObject highlightTarget;

		// Token: 0x04001949 RID: 6473
		public GameObject dragTarget;

		// Token: 0x0400194A RID: 6474
		public string prefsKey = "MessageLog";
	}
}
