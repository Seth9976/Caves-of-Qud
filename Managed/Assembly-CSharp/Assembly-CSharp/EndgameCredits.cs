using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using XRL.UI;

// Token: 0x02000180 RID: 384
[ExecuteAlways]
public class EndgameCredits : MonoBehaviour
{
	// Token: 0x06000ED2 RID: 3794 RVA: 0x0005EA50 File Offset: 0x0005CC50
	private void Start()
	{
	}

	// Token: 0x06000ED3 RID: 3795 RVA: 0x0005EA54 File Offset: 0x0005CC54
	public void SetEndmark(string id, bool ultra)
	{
		this.tombstoneImage.sprite = this.endmarks.Where<Sprite>((Sprite e) => e.name == id).FirstOrDefault<Sprite>();
		this.ultraMark.enabled = ultra;
	}

	// Token: 0x06000ED4 RID: 3796 RVA: 0x0005EAA4 File Offset: 0x0005CCA4
	public void PrepSizes()
	{
		this.topTombPadding.preferredHeight = (base.transform as RectTransform).rect.height / 2f - 100f;
		this.topTombPadding.minHeight = this.topTombPadding.preferredHeight;
		this.bottomTombPadding.preferredHeight = (base.transform as RectTransform).rect.height + 100f;
		this.bottomTombPadding.minHeight = this.bottomTombPadding.minHeight;
		(this.tombText.transform as RectTransform).sizeDelta = new Vector2((base.transform as RectTransform).rect.width, (this.tombText.transform as RectTransform).sizeDelta.y);
		if (!this.running)
		{
			(base.transform as RectTransform).anchoredPosition = new Vector2(0f, 0f);
		}
	}

	// Token: 0x06000ED5 RID: 3797 RVA: 0x0005EBA8 File Offset: 0x0005CDA8
	public void Awake()
	{
		this.testRun = false;
		this.running = false;
	}

	// Token: 0x06000ED6 RID: 3798 RVA: 0x0005EBB8 File Offset: 0x0005CDB8
	private void LateUpdate()
	{
		this.scrollSize = (this.content.transform as RectTransform).rect.height;
		if (this.testRun)
		{
			this.PrepSizes();
			this.testRun = false;
			if (!this.running)
			{
				this.running = true;
				this.runtime = -this.FADEIN_TIME;
				this.content.anchoredPosition = new Vector2(0f, 0f);
				return;
			}
			this.running = false;
			this.runtime = 0f;
			this.content.anchoredPosition = new Vector2(0f, 0f);
		}
	}

	// Token: 0x06000ED7 RID: 3799 RVA: 0x0005EC60 File Offset: 0x0005CE60
	public void Update()
	{
		if (this.running)
		{
			this.Run();
		}
	}

	// Token: 0x06000ED8 RID: 3800 RVA: 0x0005EC70 File Offset: 0x0005CE70
	public void Run()
	{
		this.runtime += Time.deltaTime;
		if (this.runtime < 0f)
		{
			this.contentGroup.alpha = (this.runtime + this.FADEIN_TIME) / (this.FADEIN_TIME / 2f);
		}
		else
		{
			this.contentGroup.alpha = 1f;
			this.content.anchoredPosition = new Vector2(0f, Mathf.Lerp(0f, this.scrollSize, this.runtime / this.scrollTime));
		}
		if (this.runtime > this.scrollTime - this.FADEIN_TIME)
		{
			this.contentGroup.alpha = 1f - (this.runtime - (this.scrollTime - this.FADEIN_TIME)) / this.FADEIN_TIME;
		}
		if (this.runtime > this.scrollTime)
		{
			this.running = false;
			if (this.complete != null)
			{
				Action action = this.complete;
				this.complete = null;
				action();
			}
		}
	}

	// Token: 0x04000A2F RID: 2607
	public LayoutElement topTombPadding;

	// Token: 0x04000A30 RID: 2608
	public LayoutElement bottomTombPadding;

	// Token: 0x04000A31 RID: 2609
	public RectTransform tombText;

	// Token: 0x04000A32 RID: 2610
	public bool testRun;

	// Token: 0x04000A33 RID: 2611
	public bool running;

	// Token: 0x04000A34 RID: 2612
	public float runtime;

	// Token: 0x04000A35 RID: 2613
	public float scrollTime;

	// Token: 0x04000A36 RID: 2614
	public float scrollSize;

	// Token: 0x04000A37 RID: 2615
	public RectTransform content;

	// Token: 0x04000A38 RID: 2616
	public Action complete;

	// Token: 0x04000A39 RID: 2617
	public Image tombstoneImage;

	// Token: 0x04000A3A RID: 2618
	public UITextSkin tombstoneText;

	// Token: 0x04000A3B RID: 2619
	public Sprite[] endmarks;

	// Token: 0x04000A3C RID: 2620
	public Image ultraMark;

	// Token: 0x04000A3D RID: 2621
	public UITextSkin creditsTextSkin;

	// Token: 0x04000A3E RID: 2622
	public float FADEIN_TIME = 10f;

	// Token: 0x04000A3F RID: 2623
	public CanvasGroup contentGroup;
}
