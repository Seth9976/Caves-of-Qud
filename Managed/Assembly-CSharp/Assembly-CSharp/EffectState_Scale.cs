using System;
using UnityEngine;

// Token: 0x020000CC RID: 204
public class EffectState_Scale : EffectState_Base
{
	// Token: 0x06000806 RID: 2054 RVA: 0x0003386B File Offset: 0x00031A6B
	public void Begin(Vector3 _to)
	{
		this.timer = 0f;
		this.start = true;
		this.from = this.info.target.localScale;
		this.to = _to;
	}

	// Token: 0x06000807 RID: 2055 RVA: 0x0003389C File Offset: 0x00031A9C
	public override bool Tick(float _delta)
	{
		if (this.start)
		{
			this.timer += _delta;
			float num = this.func(this.timer / this.info.duration);
			Vector3 vector = exMath.Lerp(this.from, this.to, num);
			if (this.timer >= this.info.duration)
			{
				this.timer = 0f;
				this.start = false;
				vector = this.to;
			}
			this.info.target.localScale = vector;
		}
		return !this.start;
	}

	// Token: 0x04000575 RID: 1397
	public EffectInfo_Scale info;

	// Token: 0x04000576 RID: 1398
	private Vector3 from;

	// Token: 0x04000577 RID: 1399
	private Vector3 to;
}
