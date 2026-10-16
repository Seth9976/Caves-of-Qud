using System;
using UnityEngine;

// Token: 0x02000082 RID: 130
public class VerticalDetailsScroller : MonoBehaviour
{
	// Token: 0x060004B7 RID: 1207 RVA: 0x0001FB9C File Offset: 0x0001DD9C
	private void Update()
	{
		if (this.keyCanvas != null && !this.keyCanvas.enabled)
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
		}
	}

	// Token: 0x040003AB RID: 939
	public Canvas keyCanvas;

	// Token: 0x040003AC RID: 940
	public RectTransform contentTransform;
}
