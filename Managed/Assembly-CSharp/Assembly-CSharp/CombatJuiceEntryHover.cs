using System;
using UnityEngine;

// Token: 0x02000047 RID: 71
public class CombatJuiceEntryHover : CombatJuiceEntry
{
	// Token: 0x06000261 RID: 609 RVA: 0x0000C3CC File Offset: 0x0000A5CC
	public CombatJuiceEntryHover(ex3DSprite2 Target, Vector3 Start, float Height, float Duration, float Rise)
	{
		this.Target = Target;
		this.Start = Start;
		this.Rise = Rise;
		this.End = new Vector3(0f, Height) + this.Start;
		this.Sine = this.End - new Vector3(0f, Height / 4f);
		this.duration = Duration + Rise * 2f;
	}

	// Token: 0x06000262 RID: 610 RVA: 0x0000C444 File Offset: 0x0000A644
	public override void update()
	{
		if (this.t <= this.Rise)
		{
			float num = Mathf.Clamp(this.t / this.Rise, 0f, 1f);
			this.Target.transform.position = Vector3.Lerp(this.Start, this.End, Easing.SineEaseInOut(num)) + new Vector3(0f, 0f, -50f);
			return;
		}
		if (this.t <= this.duration - this.Rise)
		{
			float num2 = Mathf.Sin(this.t - this.Rise);
			this.Target.transform.position = Vector3.Lerp(this.End, this.Sine, Easing.SineEaseInOut(num2)) + new Vector3(0f, 0f, -50f);
			return;
		}
		float num3 = Mathf.Clamp((this.duration - this.t) / this.Rise, 0f, 1f);
		this.Target.transform.position = Vector3.Lerp(this.Start, this.Sine, Easing.SineEaseInOut(num3)) + new Vector3(0f, 0f, -50f);
	}

	// Token: 0x06000263 RID: 611 RVA: 0x0000C58A File Offset: 0x0000A78A
	public override void finish()
	{
		this.Target.transform.position = this.Start;
		base.finish();
	}

	// Token: 0x04000145 RID: 325
	public ex3DSprite2 Target;

	// Token: 0x04000146 RID: 326
	public Vector3 Start;

	// Token: 0x04000147 RID: 327
	public Vector3 End;

	// Token: 0x04000148 RID: 328
	public Vector3 Sine;

	// Token: 0x04000149 RID: 329
	public float Rise;
}
