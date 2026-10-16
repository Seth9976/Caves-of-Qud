using System;
using QupKit;
using UnityEngine;
using XRL.Core;

namespace XRL.Sound
{
	// Token: 0x020005D4 RID: 1492
	public class MusicSource : ObjectPool<MusicSource>
	{
		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x0600401E RID: 16414 RVA: 0x0013AB72 File Offset: 0x00138D72
		// (set) Token: 0x0600401F RID: 16415 RVA: 0x0013AB7C File Offset: 0x00138D7C
		public string Channel
		{
			get
			{
				return this._Channel;
			}
			set
			{
				global::UnityEngine.Object @object = this.Object;
				this._Channel = value;
				@object.name = value;
			}
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06004020 RID: 16416 RVA: 0x0013AB9E File Offset: 0x00138D9E
		// (set) Token: 0x06004021 RID: 16417 RVA: 0x0013ABA6 File Offset: 0x00138DA6
		public bool Enabled
		{
			get
			{
				return this._Enabled;
			}
			set
			{
				if (this._Enabled != value)
				{
					this._Enabled = value;
					this.Audio.enabled = this._Enabled;
					this.Buffer.enabled = this._Enabled;
				}
			}
		}

		// Token: 0x06004022 RID: 16418 RVA: 0x0013ABDC File Offset: 0x00138DDC
		public MusicSource()
		{
			this.Object = new GameObject();
			this.Object.transform.position = new Vector3(0f, 0f, 1f);
			this.Object.transform.parent = GameObject.Find("AudioListener").transform;
			this.Audio = this.Object.AddComponent<AudioSource>();
			this.Audio.volume = 0f;
			this.Audio.priority = 0;
			this.Buffer = this.Object.AddComponent<AudioSource>();
			this.Buffer.volume = 0f;
			this.Buffer.priority = 0;
			this.Fade = this.Object.AddComponent<FadeAway>();
			this.Fade.enabled = false;
			this.Fade.Source = this;
			this.EventTime = -1.0;
		}

		// Token: 0x06004023 RID: 16419 RVA: 0x0013ACE0 File Offset: 0x00138EE0
		public void Reset()
		{
			this.Audio.clip = null;
			this.Audio.volume = 0f;
			this.Buffer.clip = null;
			this.Buffer.volume = 0f;
			this.Track = null;
			this.EntrySet = null;
			this.EventTime = -1.0;
			this.TargetVolume = 0f;
		}

		// Token: 0x06004024 RID: 16420 RVA: 0x0013AD4D File Offset: 0x00138F4D
		public void SetMusicBackground(bool State)
		{
			if (State == this.Audio.ignoreListenerPause)
			{
				return;
			}
			this.Audio.ignoreListenerPause = State;
			this.Audio.enabled = false;
			this.Audio.enabled = true;
		}

		// Token: 0x06004025 RID: 16421 RVA: 0x0013AD84 File Offset: 0x00138F84
		public void SetAudioVolume(float Volume)
		{
			if (this.Type == MusicType.General)
			{
				if (Globals.EnableMusic)
				{
					this.Audio.volume = Volume * SoundManager.MusicVolume;
					return;
				}
			}
			else if (this.Type == MusicType.Ambience && Globals.EnableAmbient)
			{
				this.Audio.volume = Volume * Globals.AmbientVolume;
			}
		}

		// Token: 0x06004026 RID: 16422 RVA: 0x0013ADD5 File Offset: 0x00138FD5
		public void StopFade()
		{
			this.SetAudioVolume(this.TargetVolume);
		}

		// Token: 0x06004027 RID: 16423 RVA: 0x0013ADE4 File Offset: 0x00138FE4
		public void Update()
		{
			float num = 0f;
			if (this.Type == MusicType.General)
			{
				if (!Globals.EnableMusic)
				{
					this.Enabled = false;
					return;
				}
				this.Enabled = true;
				num = this.TargetVolume * SoundManager.MusicVolume;
			}
			else if (this.Type == MusicType.Ambience)
			{
				if (!Globals.EnableAmbient)
				{
					this.Enabled = false;
					return;
				}
				this.Enabled = true;
				num = this.TargetVolume * Globals.AmbientVolume;
			}
			if (this.EventTime > 0.0 && (this.Loop || this.FirstPlay) && AudioSettings.dspTime + 2.0 > this.EventTime)
			{
				this.FirstPlay = false;
				AudioSource buffer = this.Buffer;
				this.Buffer = this.Audio;
				this.Audio = buffer;
				AudioEntry audioEntry = this.EntrySet.Next();
				this.Audio.clip = audioEntry.Clip;
				this.Audio.volume = this.Buffer.volume;
				this.Audio.PlayScheduled(this.EventTime);
				this.EventTime += (double)audioEntry.Clip.length;
			}
			if (Mathf.Abs(num - this.Audio.volume) < 0.025f)
			{
				this.Audio.volume = num;
				return;
			}
			if (num > this.Audio.volume)
			{
				this.Audio.volume += 0.2f * Time.deltaTime;
				if (this.Audio.volume > num)
				{
					this.Audio.volume = num;
					return;
				}
			}
			else if (num < this.Audio.volume)
			{
				this.Audio.volume -= 0.2f * Time.deltaTime;
				if (this.Audio.volume < num)
				{
					this.Audio.volume = num;
				}
			}
		}

		// Token: 0x04001F58 RID: 8024
		private string _Channel;

		// Token: 0x04001F59 RID: 8025
		private bool _Enabled;

		// Token: 0x04001F5A RID: 8026
		public GameObject Object;

		// Token: 0x04001F5B RID: 8027
		public AudioEntrySet EntrySet;

		// Token: 0x04001F5C RID: 8028
		public AudioSource Audio;

		// Token: 0x04001F5D RID: 8029
		public AudioSource Buffer;

		// Token: 0x04001F5E RID: 8030
		public FadeAway Fade;

		// Token: 0x04001F5F RID: 8031
		public MusicType Type;

		// Token: 0x04001F60 RID: 8032
		public string Track;

		// Token: 0x04001F61 RID: 8033
		public float TargetVolume;

		// Token: 0x04001F62 RID: 8034
		public double EventTime;

		// Token: 0x04001F63 RID: 8035
		public bool Loop = true;

		// Token: 0x04001F64 RID: 8036
		public bool FirstPlay = true;
	}
}
