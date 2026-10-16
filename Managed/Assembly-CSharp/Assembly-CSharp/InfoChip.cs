using System;
using UnityEngine;
using XRL.UI;

// Token: 0x02000078 RID: 120
[ExecuteAlways]
public class InfoChip : MonoBehaviour
{
	// Token: 0x0600047F RID: 1151 RVA: 0x0001DCF7 File Offset: 0x0001BEF7
	private void Start()
	{
	}

	// Token: 0x06000480 RID: 1152 RVA: 0x0001DCFC File Offset: 0x0001BEFC
	private void Update()
	{
		bool flag = false;
		bool flag2 = false;
		if (this._lastTitleColor != this.titleColor && this.titleText != null)
		{
			this.titleText.color = (this._lastTitleColor = this.titleColor);
			flag = true;
		}
		if (this._lastTitle != this.title && this.titleText != null)
		{
			this._lastTitle = this.title;
			this.titleText.text = this.title + ":";
			flag = true;
		}
		if (this._lastValueColor != this.valueColor && this.valueText != null)
		{
			this.valueText.color = (this._lastValueColor = this.valueColor);
			flag2 = true;
		}
		if (this._lastValue != this.value && this.valueText != null)
		{
			this.valueText.text = (this._lastValue = this.value);
			flag2 = true;
		}
		if (flag)
		{
			this.titleText.Apply();
		}
		if (flag2)
		{
			this.valueText.Apply();
		}
	}

	// Token: 0x04000367 RID: 871
	public string title;

	// Token: 0x04000368 RID: 872
	public Color titleColor;

	// Token: 0x04000369 RID: 873
	public string value;

	// Token: 0x0400036A RID: 874
	public Color valueColor;

	// Token: 0x0400036B RID: 875
	public UITextSkin titleText;

	// Token: 0x0400036C RID: 876
	public UITextSkin valueText;

	// Token: 0x0400036D RID: 877
	private string _lastTitle;

	// Token: 0x0400036E RID: 878
	private string _lastValue = "not it";

	// Token: 0x0400036F RID: 879
	private Color _lastTitleColor;

	// Token: 0x04000370 RID: 880
	private Color _lastValueColor;
}
