using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200012E RID: 302
public class TotalWeightPanelBehavior : MonoBehaviour
{
	// Token: 0x06000B6D RID: 2925 RVA: 0x0004C928 File Offset: 0x0004AB28
	public void SetWeight(int current, int max)
	{
		float num = Math.Min((float)current / (float)max, 1f);
		this.progress.sizeDelta = new Vector2(this.progressFrame.sizeDelta.x * num, this.progressFrame.sizeDelta.y);
		this.text.text = current.ToString() + " / " + max.ToString() + " lbs.";
	}

	// Token: 0x06000B6E RID: 2926 RVA: 0x0004C99F File Offset: 0x0004AB9F
	private void Start()
	{
	}

	// Token: 0x06000B6F RID: 2927 RVA: 0x0004C9A1 File Offset: 0x0004ABA1
	private void Update()
	{
	}

	// Token: 0x04000800 RID: 2048
	public Text text;

	// Token: 0x04000801 RID: 2049
	public RectTransform progress;

	// Token: 0x04000802 RID: 2050
	public RectTransform progressFrame;
}
