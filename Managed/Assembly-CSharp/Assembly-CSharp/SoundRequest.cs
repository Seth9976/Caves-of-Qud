using System;
using Genkit;

// Token: 0x02000161 RID: 353
public class SoundRequest
{
	// Token: 0x06000DF9 RID: 3577 RVA: 0x00059EA4 File Offset: 0x000580A4
	public override string ToString()
	{
		return string.Format("Clip:{0} ({1}/{2})\n  - V:{3} LP:{4} P:{5} PV:{6} CF:{7}/{8} fx:{9} pos:{10}", new object[]
		{
			this.Clip, this.Type, this.Channel, this.Volume, this.LowPass, this.Pitch, this.PitchVariance, this.Crossfade, this.CrossfadeDuration, this.Effect,
			this.Cell
		});
	}

	// Token: 0x040009A5 RID: 2469
	public Action OnPlay;

	// Token: 0x040009A6 RID: 2470
	public string Channel;

	// Token: 0x040009A7 RID: 2471
	public string Clip;

	// Token: 0x040009A8 RID: 2472
	public SoundRequest.SoundRequestType Type;

	// Token: 0x040009A9 RID: 2473
	public Point2D Cell = Point2D.invalid;

	// Token: 0x040009AA RID: 2474
	public bool Crossfade;

	// Token: 0x040009AB RID: 2475
	public float CrossfadeDuration = 12f;

	// Token: 0x040009AC RID: 2476
	public float Volume = 1f;

	// Token: 0x040009AD RID: 2477
	public float LowPass = 1f;

	// Token: 0x040009AE RID: 2478
	public float Pitch = 1f;

	// Token: 0x040009AF RID: 2479
	public float PitchVariance;

	// Token: 0x040009B0 RID: 2480
	public float Delay;

	// Token: 0x040009B1 RID: 2481
	public bool Loop = true;

	// Token: 0x040009B2 RID: 2482
	public SoundRequest.SoundEffectType Effect;

	// Token: 0x02001607 RID: 5639
	public enum SoundRequestType
	{
		// Token: 0x040058BE RID: 22718
		Sound,
		// Token: 0x040058BF RID: 22719
		Spatial,
		// Token: 0x040058C0 RID: 22720
		Music,
		// Token: 0x040058C1 RID: 22721
		Interface
	}

	// Token: 0x02001608 RID: 5640
	[Flags]
	public enum SoundEffectType
	{
		// Token: 0x040058C3 RID: 22723
		None = 0,
		// Token: 0x040058C4 RID: 22724
		Interface = 0,
		// Token: 0x040058C5 RID: 22725
		FullPanLeftToRight = 1,
		// Token: 0x040058C6 RID: 22726
		FullPanRightToLeft = 2
	}
}
