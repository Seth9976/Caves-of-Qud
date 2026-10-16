using System;
using System.Collections.Generic;

// Token: 0x020000E0 RID: 224
public class exUIPanel : exUIControl
{
	// Token: 0x0600084D RID: 2125 RVA: 0x00035EA4 File Offset: 0x000340A4
	public void OnEnter(exUIEvent _event)
	{
		exUIMng.inst.DispatchEvent(this, "onEnter", this.onEnter, _event);
	}

	// Token: 0x0600084E RID: 2126 RVA: 0x00035EBD File Offset: 0x000340BD
	public void OnExit(exUIEvent _event)
	{
		exUIMng.inst.DispatchEvent(this, "onExit", this.onExit, _event);
	}

	// Token: 0x0600084F RID: 2127 RVA: 0x00035ED6 File Offset: 0x000340D6
	public void OnStartFadeIn(exUIEvent _event)
	{
		exUIMng.inst.DispatchEvent(this, "onStartFadeIn", this.onStartFadeIn, _event);
	}

	// Token: 0x06000850 RID: 2128 RVA: 0x00035EEF File Offset: 0x000340EF
	public void OnFinishFadeIn(exUIEvent _event)
	{
		exUIMng.inst.DispatchEvent(this, "onFinishFadeIn", this.onFinishFadeIn, _event);
	}

	// Token: 0x06000851 RID: 2129 RVA: 0x00035F08 File Offset: 0x00034108
	public void OnFadeIn(exUIEvent _event)
	{
		exUIMng.inst.DispatchEvent(this, "onFadeIn", this.onFadeIn, _event);
	}

	// Token: 0x06000852 RID: 2130 RVA: 0x00035F21 File Offset: 0x00034121
	public void OnStartFadeOut(exUIEvent _event)
	{
		exUIMng.inst.DispatchEvent(this, "onStartFadeOut", this.onStartFadeOut, _event);
	}

	// Token: 0x06000853 RID: 2131 RVA: 0x00035F3A File Offset: 0x0003413A
	public void OnFinishFadeOut(exUIEvent _event)
	{
		exUIMng.inst.DispatchEvent(this, "onFinishFadeOut", this.onFinishFadeOut, _event);
	}

	// Token: 0x06000854 RID: 2132 RVA: 0x00035F53 File Offset: 0x00034153
	public void OnFadeOut(exUIEvent _event)
	{
		exUIMng.inst.DispatchEvent(this, "onFadeOut", this.onFadeOut, _event);
	}

	// Token: 0x06000855 RID: 2133 RVA: 0x00035F6C File Offset: 0x0003416C
	public override void CacheEventListeners()
	{
		base.CacheEventListeners();
		this.onEnter = this.eventListenerTable["onEnter"];
		this.onExit = this.eventListenerTable["onExit"];
		this.onStartFadeIn = this.eventListenerTable["onStartFadeIn"];
		this.onFinishFadeIn = this.eventListenerTable["onFinishFadeIn"];
		this.onFadeIn = this.eventListenerTable["onFadeIn"];
		this.onStartFadeOut = this.eventListenerTable["onStartFadeOut"];
		this.onFinishFadeOut = this.eventListenerTable["onFinishFadeOut"];
		this.onFadeOut = this.eventListenerTable["onFadeOut"];
	}

	// Token: 0x06000856 RID: 2134 RVA: 0x00036030 File Offset: 0x00034230
	public override string[] GetEventNames()
	{
		string[] array = base.GetEventNames();
		string[] array2 = new string[array.Length + exUIPanel.eventNames.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array2[i] = array[i];
		}
		for (int j = 0; j < exUIPanel.eventNames.Length; j++)
		{
			array2[j + array.Length] = exUIPanel.eventNames[j];
		}
		return array2;
	}

	// Token: 0x06000857 RID: 2135 RVA: 0x0003608A File Offset: 0x0003428A
	protected new void Awake()
	{
		base.Awake();
	}

	// Token: 0x06000858 RID: 2136 RVA: 0x00036094 File Offset: 0x00034294
	public void Enter()
	{
		this.OnEnter(new exUIEvent
		{
			bubbles = false
		});
	}

	// Token: 0x06000859 RID: 2137 RVA: 0x000360B8 File Offset: 0x000342B8
	public void Exit()
	{
		this.OnExit(new exUIEvent
		{
			bubbles = false
		});
	}

	// Token: 0x0600085A RID: 2138 RVA: 0x000360DC File Offset: 0x000342DC
	public void StartFadeIn()
	{
		this.OnStartFadeIn(new exUIEvent
		{
			bubbles = false
		});
	}

	// Token: 0x0600085B RID: 2139 RVA: 0x00036100 File Offset: 0x00034300
	public void FinishFadeIn()
	{
		this.OnFinishFadeIn(new exUIEvent
		{
			bubbles = false
		});
	}

	// Token: 0x0600085C RID: 2140 RVA: 0x00036124 File Offset: 0x00034324
	public void FadeIn(float _ratio)
	{
		this.OnFadeIn(new exUIRatioEvent
		{
			bubbles = false,
			ratio = _ratio
		});
	}

	// Token: 0x0600085D RID: 2141 RVA: 0x0003614C File Offset: 0x0003434C
	public void StartFadeOut()
	{
		this.OnStartFadeOut(new exUIEvent
		{
			bubbles = false
		});
	}

	// Token: 0x0600085E RID: 2142 RVA: 0x00036170 File Offset: 0x00034370
	public void FinishFadeOut()
	{
		this.OnFinishFadeOut(new exUIEvent
		{
			bubbles = false
		});
	}

	// Token: 0x0600085F RID: 2143 RVA: 0x00036194 File Offset: 0x00034394
	public void FadeOut(float _ratio)
	{
		this.OnFadeOut(new exUIRatioEvent
		{
			bubbles = false,
			ratio = _ratio
		});
	}

	// Token: 0x040005B4 RID: 1460
	public new static string[] eventNames = new string[] { "onEnter", "onExit", "onStartFadeIn", "onFinishFadeIn", "onFadeIn", "onStartFadeOut", "onFinishFadeOut", "onFadeOut" };

	// Token: 0x040005B5 RID: 1461
	private List<exUIEventListener> onEnter;

	// Token: 0x040005B6 RID: 1462
	private List<exUIEventListener> onExit;

	// Token: 0x040005B7 RID: 1463
	private List<exUIEventListener> onStartFadeIn;

	// Token: 0x040005B8 RID: 1464
	private List<exUIEventListener> onFinishFadeIn;

	// Token: 0x040005B9 RID: 1465
	private List<exUIEventListener> onFadeIn;

	// Token: 0x040005BA RID: 1466
	private List<exUIEventListener> onStartFadeOut;

	// Token: 0x040005BB RID: 1467
	private List<exUIEventListener> onFinishFadeOut;

	// Token: 0x040005BC RID: 1468
	private List<exUIEventListener> onFadeOut;
}
