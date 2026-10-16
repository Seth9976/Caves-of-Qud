using System;
using fsm;

// Token: 0x020000EC RID: 236
public class AniTransition : TimerTransition
{
	// Token: 0x060008ED RID: 2285 RVA: 0x000397EE File Offset: 0x000379EE
	public AniTransition()
	{
		this.onCheck = new Func<bool>(this.OnCheck);
	}

	// Token: 0x060008EE RID: 2286 RVA: 0x00039808 File Offset: 0x00037A08
	public AniTransition when(Func<bool> _onCheck = null)
	{
		this.onDoCheck = _onCheck;
		return this;
	}

	// Token: 0x060008EF RID: 2287 RVA: 0x00039812 File Offset: 0x00037A12
	public AniTransition after(float _normalizedTime)
	{
		this.exitTime = new float?(_normalizedTime);
		return this;
	}

	// Token: 0x060008F0 RID: 2288 RVA: 0x00039824 File Offset: 0x00037A24
	private bool OnCheck()
	{
		if (this.trigger)
		{
			this.trigger = false;
			return true;
		}
		float num = ((AniState)this.source).normalizedTime;
		if (num == 0f)
		{
			num = 1f;
		}
		if (this.exitTime != null && num < this.exitTime.Value)
		{
			return false;
		}
		if (this.onDoCheck != null)
		{
			return this.onDoCheck();
		}
		return this.exitTime != null && num >= this.exitTime.Value;
	}

	// Token: 0x04000615 RID: 1557
	public bool trigger;

	// Token: 0x04000616 RID: 1558
	private float? exitTime;

	// Token: 0x04000617 RID: 1559
	private Func<bool> onDoCheck;
}
