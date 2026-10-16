using System;

// Token: 0x020000D2 RID: 210
public class exUIEvent
{
	// Token: 0x1700010D RID: 269
	// (get) Token: 0x06000819 RID: 2073 RVA: 0x000344F6 File Offset: 0x000326F6
	public bool isPropagationStopped
	{
		get
		{
			return this.isPropagationStopped_;
		}
	}

	// Token: 0x0600081A RID: 2074 RVA: 0x000344FE File Offset: 0x000326FE
	public void StopPropagation()
	{
		this.isPropagationStopped_ = true;
	}

	// Token: 0x0600081B RID: 2075 RVA: 0x00034507 File Offset: 0x00032707
	public void Reset()
	{
		this.isPropagationStopped_ = false;
		this.target = null;
		this.currentTarget = null;
	}

	// Token: 0x04000588 RID: 1416
	public bool bubbles = true;

	// Token: 0x04000589 RID: 1417
	public bool cancelable = true;

	// Token: 0x0400058A RID: 1418
	public exUIControl target;

	// Token: 0x0400058B RID: 1419
	public exUIControl currentTarget;

	// Token: 0x0400058C RID: 1420
	public exUIEventPhase eventPhase = exUIEventPhase.Target;

	// Token: 0x0400058D RID: 1421
	private bool isPropagationStopped_;
}
