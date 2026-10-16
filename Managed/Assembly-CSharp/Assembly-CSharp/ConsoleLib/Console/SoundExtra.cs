using System;
using System.Collections.Generic;
using UnityEngine;
using XRL.Core;
using XRL.Rules;

namespace ConsoleLib.Console
{
	// Token: 0x02001532 RID: 5426
	public class SoundExtra : IConsoleCharExtra
	{
		// Token: 0x0600D369 RID: 54121 RVA: 0x004E79D4 File Offset: 0x004E5BD4
		public void SetDistance(int Distance)
		{
			this.Distance = Distance;
		}

		// Token: 0x0600D36A RID: 54122 RVA: 0x004E79DD File Offset: 0x004E5BDD
		public void SetOccluded(bool Occluded)
		{
			this.Occluded = Occluded;
		}

		// Token: 0x0600D36B RID: 54123 RVA: 0x004E79E6 File Offset: 0x004E5BE6
		public void Add(string ID, string Sound, float Volume = 1f, float PitchVariance = 1f, float CostMultiplier = 1f, int CostMaximum = 2147483647)
		{
			this.Sounds.Add(new SoundExtra.SoundInfo(ID, Sound, Volume, PitchVariance, CostMultiplier, CostMaximum));
		}

		// Token: 0x0600D36C RID: 54124 RVA: 0x004E7A04 File Offset: 0x004E5C04
		public void CopyFrom(SoundExtra extra)
		{
			if (this.Sounds.Count > 0)
			{
				this.Sounds.Clear();
			}
			if (extra != null)
			{
				if (extra.Sounds.Count > 0)
				{
					this.Sounds.AddRange(extra.Sounds);
				}
				this.Distance = extra.Distance;
				this.Occluded = extra.Occluded;
				return;
			}
			this.Distance = 0;
			this.Occluded = false;
		}

		// Token: 0x0600D36D RID: 54125 RVA: 0x004E7A73 File Offset: 0x004E5C73
		public override IConsoleCharExtra Copy()
		{
			SoundExtra soundExtra = new SoundExtra();
			soundExtra.Sounds.AddRange(this.Sounds);
			soundExtra.Distance = this.Distance;
			soundExtra.Occluded = this.Occluded;
			return soundExtra;
		}

		// Token: 0x0600D36E RID: 54126 RVA: 0x004E7AA3 File Offset: 0x004E5CA3
		public override void Clear(bool overtyping)
		{
			if (!overtyping)
			{
				this.Sounds.Clear();
			}
		}

		// Token: 0x0600D36F RID: 54127 RVA: 0x004E7AB4 File Offset: 0x004E5CB4
		public void UpdateSourceFromInfo(AudioSource source, SoundExtra.SoundInfo info, ex3DSprite2 sprite, bool start = false)
		{
			float num = 1f;
			float num2 = info.Volume;
			int num3 = Mathf.RoundToInt((float)this.Distance * info.CostMultiplier);
			if (num3 > info.CostMaximum)
			{
				num3 = info.CostMaximum;
			}
			if (this.Occluded)
			{
				if (num3 == 9999)
				{
					num2 = 0f;
				}
				else
				{
					num = 0.15f + 0.55f * ((float)(80 - num3) / 80f);
					num2 *= Mathf.Pow(0.9f, (float)num3);
				}
			}
			else if (num3 > 40)
			{
				num2 = 0f;
			}
			else
			{
				num = 1f;
				num2 *= Mathf.Pow(0.9f, (float)num3);
			}
			if (source.gameObject.transform.position != sprite.transform.position)
			{
				source.gameObject.transform.position = sprite.transform.position;
			}
			source.GetComponent<AudioLowPassFilter>().cutoffFrequency = 22000f * num * num2;
			source.volume = num2;
			if (!source.isActiveAndEnabled)
			{
				source.gameObject.SetActive(true);
			}
			if (!source.isPlaying && start)
			{
				source.Play();
			}
		}

		// Token: 0x0600D370 RID: 54128 RVA: 0x004E7BD8 File Offset: 0x004E5DD8
		public override void AfterRender(int x, int y, ConsoleChar ch, ex3DSprite2 sprite, ScreenBuffer buffer)
		{
			if (Globals.EnableSound)
			{
				for (int i = 0; i < this.Sounds.Count; i++)
				{
					string id = this.Sounds[i].ID;
					if (!SoundExtra.NextPlayingSounds.ContainsKey(id))
					{
						AudioSource audioSource;
						if (SoundExtra.PlayingSounds.TryGetValue(id, out audioSource))
						{
							SoundExtra.NextPlayingSounds.Add(id, audioSource);
							this.UpdateSourceFromInfo(audioSource, this.Sounds[i], sprite, false);
							SoundExtra.PlayingSounds.Remove(id);
						}
						else
						{
							string sound = this.Sounds[i].Sound;
							AudioEntrySet audioEntrySet;
							if (SoundManager.TryGetClipSet(sound, out audioEntrySet))
							{
								if (audioEntrySet.Initialized)
								{
									AudioSource audioSource2 = SoundExtra.next(this.Sounds[i]);
									this.UpdateSourceFromInfo(audioSource2, this.Sounds[i], sprite, true);
									SoundExtra.NextPlayingSounds.Add(id, audioSource2);
								}
								else
								{
									SoundManager.PreloadClipSet(sound);
								}
							}
						}
					}
				}
			}
			if (x == 79 && y == 24)
			{
				Dictionary<string, AudioSource> playingSounds = SoundExtra.PlayingSounds;
				SoundExtra.PlayingSounds = SoundExtra.NextPlayingSounds;
				foreach (KeyValuePair<string, AudioSource> keyValuePair in playingSounds)
				{
					SoundExtra.free(keyValuePair.Value);
				}
				playingSounds.Clear();
				SoundExtra.NextPlayingSounds = playingSounds;
			}
		}

		// Token: 0x0600D371 RID: 54129 RVA: 0x004E7D44 File Offset: 0x004E5F44
		private static void free(AudioSource source)
		{
			if (source != null)
			{
				source.Stop();
				source.gameObject.SetActive(false);
				SoundExtra.pool.Enqueue(source);
			}
		}

		// Token: 0x0600D372 RID: 54130 RVA: 0x004E7D6C File Offset: 0x004E5F6C
		private static AudioSource next(SoundExtra.SoundInfo info)
		{
			if (info.Sound == null)
			{
				return null;
			}
			if (SoundExtra.pool.Count == 0)
			{
				AudioSource audioSource = SoundManager.CreateAudioSource(info.Volume, 1f, info.PitchVariance);
				audioSource.loop = true;
				if (SoundExtra.imposterRoot == null)
				{
					SoundExtra.imposterRoot = new GameObject("TileSoundRoot");
				}
				audioSource.transform.parent = SoundExtra.imposterRoot.transform;
				SoundExtra.pool.Enqueue(audioSource);
			}
			AudioSource audioSource2 = SoundExtra.pool.Dequeue();
			audioSource2.gameObject.name = info.ID;
			audioSource2.clip = SoundManager.GetClipSet(info.Sound, false).Next().Clip;
			audioSource2.pitch = 1f - ((float)Stat.Rnd5.NextDouble() * 2f - 1f) * info.PitchVariance;
			audioSource2.rolloffMode = AudioRolloffMode.Custom;
			audioSource2.dopplerLevel = 0f;
			audioSource2.minDistance = 0f;
			audioSource2.maxDistance = 10000f;
			audioSource2.SetCustomCurve(AudioSourceCurveType.CustomRolloff, SoundExtra.distanceCurve);
			audioSource2.spatialBlend = 0.5f;
			audioSource2.gameObject.SetActive(true);
			return audioSource2;
		}

		// Token: 0x040053E7 RID: 21479
		private static Dictionary<string, AudioSource> PlayingSounds = new Dictionary<string, AudioSource>();

		// Token: 0x040053E8 RID: 21480
		private static Dictionary<string, AudioSource> NextPlayingSounds = new Dictionary<string, AudioSource>();

		// Token: 0x040053E9 RID: 21481
		public List<SoundExtra.SoundInfo> Sounds = new List<SoundExtra.SoundInfo>();

		// Token: 0x040053EA RID: 21482
		public int Distance;

		// Token: 0x040053EB RID: 21483
		public bool Occluded;

		// Token: 0x040053EC RID: 21484
		public static AnimationCurve distanceCurve = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 1f),
			new Keyframe(1f, 1f)
		});

		// Token: 0x040053ED RID: 21485
		private static GameObject imposterRoot;

		// Token: 0x040053EE RID: 21486
		private static Queue<AudioSource> pool = new Queue<AudioSource>();

		// Token: 0x02001E3A RID: 7738
		public struct SoundInfo
		{
			// Token: 0x0600F6E5 RID: 63205 RVA: 0x0055AB67 File Offset: 0x00558D67
			public SoundInfo(string ID, string Sound, float Volume = 1f, float PitchVariance = 0f, float CostMultiplier = 1f, int CostMaximum = 2147483647)
			{
				this.ID = ID;
				this.Sound = Sound;
				this.Volume = Volume;
				this.PitchVariance = PitchVariance;
				this.CostMultiplier = CostMultiplier;
				this.CostMaximum = CostMaximum;
			}

			// Token: 0x0600F6E6 RID: 63206 RVA: 0x0055AB96 File Offset: 0x00558D96
			public bool SameAs(SoundExtra.SoundInfo info)
			{
				return this.ID == info.ID;
			}

			// Token: 0x04007618 RID: 30232
			public string ID;

			// Token: 0x04007619 RID: 30233
			public string Sound;

			// Token: 0x0400761A RID: 30234
			public float Volume;

			// Token: 0x0400761B RID: 30235
			public float PitchVariance;

			// Token: 0x0400761C RID: 30236
			public float CostMultiplier;

			// Token: 0x0400761D RID: 30237
			public int CostMaximum;
		}
	}
}
