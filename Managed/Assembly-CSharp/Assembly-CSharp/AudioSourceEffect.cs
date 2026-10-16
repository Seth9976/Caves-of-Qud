using System;
using UnityEngine;

// Token: 0x02000160 RID: 352
public class AudioSourceEffect : MonoBehaviour
{
	// Token: 0x06000DF6 RID: 3574 RVA: 0x00059D94 File Offset: 0x00057F94
	public void Update()
	{
		if (this.source == null)
		{
			this.source = base.GetComponent<AudioSource>();
		}
		if (this.source != null)
		{
			if (this.Effect.HasFlag(SoundRequest.SoundEffectType.FullPanLeftToRight))
			{
				this.source.panStereo = Mathf.Lerp(-1f, 1f, this.source.time / this.source.clip.length);
				return;
			}
			if (this.Effect.HasFlag(SoundRequest.SoundEffectType.FullPanRightToLeft))
			{
				this.source.panStereo = Mathf.Lerp(1f, -1f, this.source.time / this.source.clip.length);
			}
		}
	}

	// Token: 0x06000DF7 RID: 3575 RVA: 0x00059E6C File Offset: 0x0005806C
	public static void PoolReset(AudioSource source)
	{
		AudioSourceEffect component = source.GetComponent<AudioSourceEffect>();
		if (component != null)
		{
			component.Effect = SoundRequest.SoundEffectType.None;
		}
		source.panStereo = 0f;
	}

	// Token: 0x040009A3 RID: 2467
	public SoundRequest.SoundEffectType Effect;

	// Token: 0x040009A4 RID: 2468
	private AudioSource source;
}
