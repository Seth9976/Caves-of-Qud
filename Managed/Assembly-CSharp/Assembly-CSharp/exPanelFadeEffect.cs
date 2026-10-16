using System;
using UnityEngine;

// Token: 0x020000C2 RID: 194
public class exPanelFadeEffect : MonoBehaviour
{
	// Token: 0x060007C0 RID: 1984 RVA: 0x000327B3 File Offset: 0x000309B3
	private void Awake()
	{
		this.Init();
	}

	// Token: 0x060007C1 RID: 1985 RVA: 0x000327BC File Offset: 0x000309BC
	public void Init()
	{
		if (this.inited)
		{
			return;
		}
		this.panel = base.GetComponent<exUIPanel>();
		this.colorCtrl = base.GetComponent<exSpriteColorController>();
		if (this.panel)
		{
			this.panel.AddEventListener("onStartFadeIn", delegate(exUIEvent _event)
			{
				this.panel.gameObject.SetActive(true);
				if (this.colorCtrl)
				{
					this.colorCtrl.color = new Color(1f, 1f, 1f, 0f);
				}
			}, false);
			this.panel.AddEventListener("onFinishFadeOut", delegate(exUIEvent _event)
			{
				this.panel.gameObject.SetActive(false);
			}, false);
			this.panel.AddEventListener("onFadeIn", delegate(exUIEvent _event)
			{
				exUIRatioEvent exUIRatioEvent = _event as exUIRatioEvent;
				if (this.colorCtrl)
				{
					this.colorCtrl.color = new Color(1f, 1f, 1f, exUIRatioEvent.ratio);
				}
			}, false);
			this.panel.AddEventListener("onFadeOut", delegate(exUIEvent _event)
			{
				exUIRatioEvent exUIRatioEvent2 = _event as exUIRatioEvent;
				if (this.colorCtrl)
				{
					this.colorCtrl.color = new Color(1f, 1f, 1f, 1f - exUIRatioEvent2.ratio);
				}
			}, false);
		}
		this.inited = true;
	}

	// Token: 0x0400053E RID: 1342
	private exUIPanel panel;

	// Token: 0x0400053F RID: 1343
	private exSpriteColorController colorCtrl;

	// Token: 0x04000540 RID: 1344
	private bool inited;
}
