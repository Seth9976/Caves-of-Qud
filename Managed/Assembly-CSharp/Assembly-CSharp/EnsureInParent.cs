using System;
using UnityEngine;

// Token: 0x0200002D RID: 45
public class EnsureInParent : MonoBehaviour
{
	// Token: 0x17000035 RID: 53
	// (get) Token: 0x060000A7 RID: 167 RVA: 0x0000551C File Offset: 0x0000371C
	public RectTransform parentTransform
	{
		get
		{
			if (this._parentTransform == null)
			{
				this._parentTransform = base.transform.parent.GetComponent<RectTransform>();
			}
			return this._parentTransform;
		}
	}

	// Token: 0x17000036 RID: 54
	// (get) Token: 0x060000A8 RID: 168 RVA: 0x00005548 File Offset: 0x00003748
	public RectTransform rectTransform
	{
		get
		{
			if (this._rectTransform == null)
			{
				this._rectTransform = base.GetComponent<RectTransform>();
			}
			return this._rectTransform;
		}
	}

	// Token: 0x060000A9 RID: 169 RVA: 0x0000556A File Offset: 0x0000376A
	private void Start()
	{
	}

	// Token: 0x060000AA RID: 170 RVA: 0x0000556C File Offset: 0x0000376C
	private void Update()
	{
		if (this.rectTransform.rect.width + this.rectTransform.anchoredPosition.x > this.parentTransform.rect.width)
		{
			this.rectTransform.anchoredPosition = new Vector2(this.parentTransform.rect.width - this.rectTransform.rect.width, this.rectTransform.anchoredPosition.y);
		}
		if (this.rectTransform.rect.height + -this.rectTransform.anchoredPosition.y > this.parentTransform.rect.height)
		{
			this.rectTransform.anchoredPosition = new Vector2(this.rectTransform.anchoredPosition.x, -this.parentTransform.rect.height + this.rectTransform.rect.height);
		}
	}

	// Token: 0x040000D5 RID: 213
	public RectTransform _parentTransform;

	// Token: 0x040000D6 RID: 214
	private RectTransform _rectTransform;
}
