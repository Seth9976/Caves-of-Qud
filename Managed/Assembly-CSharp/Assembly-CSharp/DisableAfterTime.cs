using System;
using UnityEngine;

// Token: 0x02000165 RID: 357
public class DisableAfterTime : MonoBehaviour, CombatJuice.ICombatJuiceAnimator
{
	// Token: 0x06000E1D RID: 3613 RVA: 0x0005B3FA File Offset: 0x000595FA
	public DisableAfterTime()
	{
	}

	// Token: 0x06000E1E RID: 3614 RVA: 0x0005B41B File Offset: 0x0005961B
	public DisableAfterTime(float _Duration)
	{
		this.Duration = _Duration * 1000f;
	}

	// Token: 0x06000E1F RID: 3615 RVA: 0x0005B449 File Offset: 0x00059649
	private void Awake()
	{
		this.Countdown = this.Duration;
	}

	// Token: 0x06000E20 RID: 3616 RVA: 0x0005B457 File Offset: 0x00059657
	private void Start()
	{
	}

	// Token: 0x06000E21 RID: 3617 RVA: 0x0005B45C File Offset: 0x0005965C
	private void Update()
	{
		this.Countdown -= Time.deltaTime;
		if (this.Countdown <= 0f)
		{
			base.gameObject.SetActive(false);
			if (this.after != null)
			{
				Action action = this.after;
				this.after = null;
				action();
			}
		}
	}

	// Token: 0x06000E22 RID: 3618 RVA: 0x0005B4AE File Offset: 0x000596AE
	public void Play(bool loop = false, Action after = null, string name = null, string objectId = null)
	{
		this.Countdown = this.Duration;
		if (this.Singleton)
		{
			CombatJuice.pool(name, base.gameObject, false);
			this.after = null;
			return;
		}
		this.after = after;
	}

	// Token: 0x06000E23 RID: 3619 RVA: 0x0005B4E0 File Offset: 0x000596E0
	public void Stop()
	{
		this.Countdown = 0f;
	}

	// Token: 0x040009D3 RID: 2515
	public bool Singleton = true;

	// Token: 0x040009D4 RID: 2516
	public bool PoolAfter = true;

	// Token: 0x040009D5 RID: 2517
	public float Duration = 2f;

	// Token: 0x040009D6 RID: 2518
	[NonSerialized]
	public float Countdown;

	// Token: 0x040009D7 RID: 2519
	[NonSerialized]
	private Action after;
}
