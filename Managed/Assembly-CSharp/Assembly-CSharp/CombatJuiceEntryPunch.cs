using System;
using UnityEngine;

// Token: 0x02000046 RID: 70
public class CombatJuiceEntryPunch : CombatJuiceEntry
{
	// Token: 0x0600025E RID: 606 RVA: 0x0000C30D File Offset: 0x0000A50D
	public CombatJuiceEntryPunch(ex3DSprite2 target, Vector3 start, Vector3 end, float duration, Easing.Functions ease)
	{
		this.target = target;
		this.startPosition = start;
		this.endPosition = end;
		this.duration = duration;
		this.ease = ease;
	}

	// Token: 0x0600025F RID: 607 RVA: 0x0000C33C File Offset: 0x0000A53C
	public override void update()
	{
		float num = Mathf.Clamp(this.t / this.duration, 0f, 1f);
		this.target.transform.position = Vector3.Lerp(this.startPosition, this.endPosition, Easing.Interpolate(num, this.ease)) + new Vector3(0f, 0f, -50f);
	}

	// Token: 0x06000260 RID: 608 RVA: 0x0000C3AC File Offset: 0x0000A5AC
	public override void finish()
	{
		this.target.transform.position = this.startPosition;
		base.finish();
	}

	// Token: 0x04000141 RID: 321
	public ex3DSprite2 target;

	// Token: 0x04000142 RID: 322
	public Vector3 startPosition;

	// Token: 0x04000143 RID: 323
	public Vector3 endPosition;

	// Token: 0x04000144 RID: 324
	public Easing.Functions ease;
}
