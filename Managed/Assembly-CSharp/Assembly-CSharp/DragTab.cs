using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000095 RID: 149
public class DragTab : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IDragHandler
{
	// Token: 0x06000517 RID: 1303 RVA: 0x00021541 File Offset: 0x0001F741
	public void OnPointerEnter(PointerEventData eventData)
	{
		this.highlightTarget.SetActive(true);
	}

	// Token: 0x06000518 RID: 1304 RVA: 0x0002154F File Offset: 0x0001F74F
	public void OnPointerExit(PointerEventData eventData)
	{
		this.highlightTarget.SetActive(false);
	}

	// Token: 0x06000519 RID: 1305 RVA: 0x00021560 File Offset: 0x0001F760
	public void OnDrag(PointerEventData eventData)
	{
		this.dragTarget.GetComponent<RectTransform>().sizeDelta -= new Vector2(0f, eventData.delta.y);
		this.dragTarget.GetComponent<RectTransform>().anchoredPosition += new Vector2(0f, eventData.delta.y / 2f);
	}

	// Token: 0x040003EB RID: 1003
	public GameObject highlightTarget;

	// Token: 0x040003EC RID: 1004
	public GameObject dragTarget;
}
