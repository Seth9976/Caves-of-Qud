using System;

// Token: 0x020000CB RID: 203
public class EffectState_Base
{
	// Token: 0x06000804 RID: 2052 RVA: 0x00033860 File Offset: 0x00031A60
	public virtual bool Tick(float _delta)
	{
		return true;
	}

	// Token: 0x04000572 RID: 1394
	public Func<float, float> func;

	// Token: 0x04000573 RID: 1395
	protected float timer;

	// Token: 0x04000574 RID: 1396
	protected bool start;
}
