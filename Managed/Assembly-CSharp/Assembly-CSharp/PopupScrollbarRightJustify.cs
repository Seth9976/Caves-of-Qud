using System;
using UnityEngine;

// Token: 0x02000100 RID: 256
[ExecuteAlways]
public class PopupScrollbarRightJustify : MonoBehaviour
{
	// Token: 0x0600098A RID: 2442 RVA: 0x0003C900 File Offset: 0x0003AB00
	private void Start()
	{
	}

	// Token: 0x0600098B RID: 2443 RVA: 0x0003C904 File Offset: 0x0003AB04
	private void Update()
	{
		this.proposedOffset = this.target.rect.width - this.offset - this.parent.rect.width;
		RectTransform rectTransform = base.transform as RectTransform;
		if (rectTransform.anchoredPosition.x != this.proposedOffset)
		{
			rectTransform.anchoredPosition = new Vector2(this.proposedOffset, 0f);
		}
	}

	// Token: 0x0400063F RID: 1599
	public RectTransform parent;

	// Token: 0x04000640 RID: 1600
	public RectTransform target;

	// Token: 0x04000641 RID: 1601
	public float offset;

	// Token: 0x04000642 RID: 1602
	public float proposedOffset;
}
