using System;
using QupKit;
using UnityEngine;
using XRL.Sound;

// Token: 0x02000113 RID: 275
public class FadeAway : MonoBehaviour
{
	// Token: 0x06000AD8 RID: 2776 RVA: 0x00046120 File Offset: 0x00044320
	public void StartFade(float Duration)
	{
		this.Duration = Duration;
		this.StartVolume = (this.Source.Audio.isPlaying ? this.Source.Audio.volume : 0f);
		this.StartTime = Time.time;
		base.enabled = true;
	}

	// Token: 0x06000AD9 RID: 2777 RVA: 0x00046178 File Offset: 0x00044378
	public void Update()
	{
		float num = Time.time - this.StartTime;
		if (num > this.Duration)
		{
			base.enabled = false;
			this.Source.Audio.volume = 0f;
			this.Source.Buffer.volume = 0f;
			this.Source.Enabled = false;
			ObjectPool<MusicSource>.Return(this.Source);
			return;
		}
		this.Source.Audio.volume = Mathf.Lerp(this.StartVolume, 0f, num / this.Duration);
	}

	// Token: 0x04000745 RID: 1861
	public MusicSource Source;

	// Token: 0x04000746 RID: 1862
	public float StartTime;

	// Token: 0x04000747 RID: 1863
	public float StartVolume;

	// Token: 0x04000748 RID: 1864
	public float Duration;
}
