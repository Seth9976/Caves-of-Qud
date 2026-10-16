using System;
using UnityEngine;
using XRL.Core;

// Token: 0x020000FF RID: 255
public class OptionsDrivenAudioSource : MonoBehaviour
{
	// Token: 0x06000987 RID: 2439 RVA: 0x0003C7F0 File Offset: 0x0003A9F0
	private void Awake()
	{
		if (this.source == null)
		{
			this.source = base.GetComponent<AudioSource>();
		}
		if (this.source != null)
		{
			this.volume = this.source.volume;
		}
		if (this.source.enabled && !Globals.EnableSound)
		{
			this.source.enabled = false;
			return;
		}
		if (!this.source.enabled && Globals.EnableSound)
		{
			this.source.enabled = true;
		}
	}

	// Token: 0x06000988 RID: 2440 RVA: 0x0003C878 File Offset: 0x0003AA78
	private void Update()
	{
		if (this.source.enabled && !Globals.EnableSound)
		{
			this.source.enabled = false;
		}
		if (!Globals.EnableSound)
		{
			return;
		}
		if (!this.source.enabled && Globals.EnableSound)
		{
			this.source.enabled = true;
		}
		if (this.source != null)
		{
			this.source.volume = SoundManager.MasterVolume * SoundManager.SoundVolume * this.volume;
		}
	}

	// Token: 0x0400063D RID: 1597
	public AudioSource source;

	// Token: 0x0400063E RID: 1598
	private float volume;
}
