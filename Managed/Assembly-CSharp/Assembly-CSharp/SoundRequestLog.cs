using System;

// Token: 0x02000162 RID: 354
public class SoundRequestLog : SoundRequest
{
	// Token: 0x06000DFB RID: 3579 RVA: 0x00059FA8 File Offset: 0x000581A8
	public void Set(float timePlayed, SoundRequest request)
	{
		this.timePlayed = timePlayed;
		this.Channel = request.Channel;
		this.Clip = request.Clip;
		this.Type = request.Type;
		this.Cell = request.Cell;
		this.Crossfade = request.Crossfade;
		this.CrossfadeDuration = request.CrossfadeDuration;
		this.Volume = request.Volume;
		this.LowPass = request.LowPass;
		this.Pitch = request.Pitch;
		this.PitchVariance = request.PitchVariance;
		this.Effect = request.Effect;
		this.Loop = request.Loop;
	}

	// Token: 0x06000DFC RID: 3580 RVA: 0x0005A04C File Offset: 0x0005824C
	public override string ToString()
	{
		return string.Format("T:{0:#.###} {1}", this.timePlayed, base.ToString());
	}

	// Token: 0x040009B3 RID: 2483
	public float timePlayed;
}
