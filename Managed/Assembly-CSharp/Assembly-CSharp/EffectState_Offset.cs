using System;
using UnityEngine;

// Token: 0x020000CD RID: 205
public class EffectState_Offset : EffectState_Base
{
	// Token: 0x06000809 RID: 2057 RVA: 0x0003393E File Offset: 0x00031B3E
	public void Begin(Vector2 _to)
	{
		this.timer = 0f;
		this.start = true;
		this.from = this.info.target.offset;
		this.to = _to;
	}

	// Token: 0x0600080A RID: 2058 RVA: 0x00033970 File Offset: 0x00031B70
	public override bool Tick(float _delta)
	{
		if (this.start)
		{
			this.timer += _delta;
			float num = this.func(this.timer / this.info.duration);
			Vector2 vector = exMath.Lerp(this.from, this.to, num);
			if (this.timer >= this.info.duration)
			{
				this.timer = 0f;
				this.start = false;
				vector = this.to;
			}
			this.info.target.offset = vector;
		}
		return !this.start;
	}

	// Token: 0x04000578 RID: 1400
	public EffectInfo_Offset info;

	// Token: 0x04000579 RID: 1401
	private Vector2 from;

	// Token: 0x0400057A RID: 1402
	private Vector2 to;
}
