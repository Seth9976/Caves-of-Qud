using System;
using UnityEngine;

// Token: 0x02000106 RID: 262
public class LeanAudioOptions
{
	// Token: 0x060009A3 RID: 2467 RVA: 0x0003D37D File Offset: 0x0003B57D
	public LeanAudioOptions setFrequency(int frequencyRate)
	{
		this.frequencyRate = frequencyRate;
		return this;
	}

	// Token: 0x060009A4 RID: 2468 RVA: 0x0003D387 File Offset: 0x0003B587
	public LeanAudioOptions setVibrato(Vector3[] vibrato)
	{
		this.vibrato = vibrato;
		return this;
	}

	// Token: 0x04000651 RID: 1617
	public Vector3[] vibrato;

	// Token: 0x04000652 RID: 1618
	public int frequencyRate = 44100;

	// Token: 0x04000653 RID: 1619
	public bool useSetData = true;

	// Token: 0x04000654 RID: 1620
	public LeanAudioStream stream;
}
