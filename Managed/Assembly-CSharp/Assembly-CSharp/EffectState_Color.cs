using System;
using UnityEngine;

// Token: 0x020000CE RID: 206
public class EffectState_Color : EffectState_Base
{
	// Token: 0x0600080C RID: 2060 RVA: 0x00033A12 File Offset: 0x00031C12
	public void Begin(Color _to)
	{
		this.timer = 0f;
		this.start = true;
		this.from = this.info.target.color;
		this.to = _to;
	}

	// Token: 0x0600080D RID: 2061 RVA: 0x00033A44 File Offset: 0x00031C44
	public override bool Tick(float _delta)
	{
		if (this.start)
		{
			this.timer += _delta;
			float num = this.func(this.timer / this.info.duration);
			Color color = exMath.Lerp(this.from, this.to, num);
			if (this.timer >= this.info.duration)
			{
				this.timer = 0f;
				this.start = false;
				color = this.to;
			}
			this.info.target.color = color;
		}
		return !this.start;
	}

	// Token: 0x0400057B RID: 1403
	public EffectInfo_Color info;

	// Token: 0x0400057C RID: 1404
	private Color from;

	// Token: 0x0400057D RID: 1405
	private Color to;
}
