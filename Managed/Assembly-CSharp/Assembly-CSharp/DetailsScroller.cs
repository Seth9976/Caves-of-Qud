using System;
using UnityEngine;

// Token: 0x0200007F RID: 127
public class DetailsScroller : MonoBehaviour
{
	// Token: 0x060004A9 RID: 1193 RVA: 0x0001E9C4 File Offset: 0x0001CBC4
	private void Update()
	{
		if (!this.keyCanvas.enabled)
		{
			return;
		}
		if (this.contentTransform != null)
		{
			float num = 512f;
			string text = ControlManager.ResolveAxisDirection("UI:DetailsNavigate");
			if (text != null && text.Contains("N"))
			{
				this.contentTransform.anchoredPosition += new Vector2(0f, -num * Time.deltaTime);
			}
			if (text != null && text.Contains("S"))
			{
				this.contentTransform.anchoredPosition += new Vector2(0f, num * Time.deltaTime);
			}
			if (text != null && text.Contains("E"))
			{
				this.contentTransform.anchoredPosition += new Vector2(-num * Time.deltaTime, 0f);
			}
			if (text != null && text.Contains("W"))
			{
				this.contentTransform.anchoredPosition += new Vector2(num * Time.deltaTime, 0f);
			}
		}
	}

	// Token: 0x04000395 RID: 917
	public Canvas keyCanvas;

	// Token: 0x04000396 RID: 918
	public RectTransform contentTransform;
}
