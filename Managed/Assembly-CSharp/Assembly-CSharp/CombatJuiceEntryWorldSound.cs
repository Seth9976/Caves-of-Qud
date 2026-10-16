using System;
using Genkit;
using UnityEngine;

// Token: 0x02000045 RID: 69
public class CombatJuiceEntryWorldSound : CombatJuiceEntry
{
	// Token: 0x0600025B RID: 603 RVA: 0x0000C247 File Offset: 0x0000A447
	public CombatJuiceEntryWorldSound(string clip, int distance, bool occluded, float volume, float variance, float delay, Location2D cell)
	{
		this.clip = clip;
		this.distance = distance;
		this.occluded = occluded;
		this.volume = volume;
		this.variance = variance;
		this.delay = delay;
		this.cell = cell;
	}

	// Token: 0x0600025C RID: 604 RVA: 0x0000C284 File Offset: 0x0000A484
	public override bool canStart()
	{
		if (this.delay > 0f)
		{
			this.delay -= Time.deltaTime;
		}
		return this.delay <= 0f;
	}

	// Token: 0x0600025D RID: 605 RVA: 0x0000C2B8 File Offset: 0x0000A4B8
	public override void start()
	{
		SoundManager.PlayWorldSound(this.clip, this.distance, this.occluded, this.volume, this.cell, 0f, 0f, 1f);
		this.t = 0f;
		this.duration = 0f;
	}

	// Token: 0x0400013A RID: 314
	public string clip;

	// Token: 0x0400013B RID: 315
	public int distance;

	// Token: 0x0400013C RID: 316
	public bool occluded;

	// Token: 0x0400013D RID: 317
	public float volume;

	// Token: 0x0400013E RID: 318
	public float variance;

	// Token: 0x0400013F RID: 319
	public float delay;

	// Token: 0x04000140 RID: 320
	public Location2D cell;
}
