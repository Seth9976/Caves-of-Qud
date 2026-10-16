using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000101 RID: 257
public class PositionResponsiveElement : MonoBehaviour
{
	// Token: 0x0600098D RID: 2445 RVA: 0x0003C984 File Offset: 0x0003AB84
	private void LateUpdate()
	{
		if (!this.keyCanvas.enabled)
		{
			return;
		}
		if (this.mainCamera == null)
		{
			this.mainCamera = Camera.main;
		}
		if (this.rectTransform == null)
		{
			this.rectTransform = base.GetComponent<RectTransform>();
		}
		this.rectTransform.GetWorldCorners(this.worldCorners);
		bool flag = true;
		for (int i = 0; i < 4; i++)
		{
			this.screenCorners[i] = this.mainCamera.WorldToScreenPoint(this.worldCorners[i]);
			if (this.screenCorners[i].x < 0f)
			{
				flag = false;
			}
			if (this.screenCorners[i].y < 0f)
			{
				flag = false;
			}
			if (this.screenCorners[i].x >= (float)Screen.width)
			{
				flag = false;
			}
			if (this.screenCorners[i].y >= (float)Screen.height)
			{
				flag = false;
			}
		}
		if (!flag)
		{
			if (this.targetImage != null && this.targetImage.enabled)
			{
				this.targetImage.enabled = false;
				return;
			}
		}
		else if (this.targetImage != null && !this.targetImage.enabled)
		{
			this.targetImage.enabled = true;
		}
	}

	// Token: 0x04000643 RID: 1603
	public Camera mainCamera;

	// Token: 0x04000644 RID: 1604
	public Canvas keyCanvas;

	// Token: 0x04000645 RID: 1605
	public Image targetImage;

	// Token: 0x04000646 RID: 1606
	private RectTransform rectTransform;

	// Token: 0x04000647 RID: 1607
	private Vector3[] worldCorners = new Vector3[4];

	// Token: 0x04000648 RID: 1608
	private Vector3[] screenCorners = new Vector3[4];
}
