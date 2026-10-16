using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using Genkit;
using QupKit;
using UnityEngine;
using XRL;
using XRL.Collections;
using XRL.Core;
using XRL.Messages;
using XRL.Rules;
using XRL.Sound;
using XRL.UI;
using XRL.Wish;
using XRL.World;

// Token: 0x02000163 RID: 355
[HasModSensitiveStaticCache]
[HasWishCommand]
public static class SoundManager
{
	// Token: 0x06000DFE RID: 3582 RVA: 0x0005A071 File Offset: 0x00058271
	[WishCommand("soundlog", null)]
	public static bool ShowSoundLog()
	{
		return SoundManager.ShowSoundLog("20");
	}

	// Token: 0x06000DFF RID: 3583 RVA: 0x0005A080 File Offset: 0x00058280
	[WishCommand("soundlog", null)]
	public static bool ShowSoundLog(string rest)
	{
		int num;
		if (rest == "all")
		{
			num = SoundManager.RequestLogs.Length;
		}
		if (!int.TryParse(rest, out num))
		{
			num = 20;
		}
		int num2 = SoundManager.RequestLogNextIndex - 1 + SoundManager.RequestLogs.Length;
		StringBuilder stringBuilder = global::XRL.World.Event.NewStringBuilder(null);
		stringBuilder.Append(string.Format("Log of last {0} sounds:\n\n", num));
		for (int i = 0; i < num; i++)
		{
			SoundRequestLog soundRequestLog = SoundManager.RequestLogs[(num2 - i) % SoundManager.RequestLogs.Length];
			if (soundRequestLog.timePlayed > 0f)
			{
				stringBuilder.Append(soundRequestLog.ToString()).Append('\n');
			}
		}
		Popup.Show(stringBuilder.ToString(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
		return true;
	}

	// Token: 0x06000E00 RID: 3584 RVA: 0x0005A136 File Offset: 0x00058336
	[ModSensitiveCacheInit]
	public static void ClearCache()
	{
		SoundManager.ClipFrames = new Dictionary<string, int[]>();
	}

	// Token: 0x06000E01 RID: 3585 RVA: 0x0005A142 File Offset: 0x00058342
	public static void StopMusic(string Channel = "music", bool Crossfade = true, float CrossfadeDuration = 12f)
	{
		SoundManager.PlayMusic(null, Channel, Crossfade, CrossfadeDuration, 1f, null, true);
	}

	// Token: 0x06000E02 RID: 3586 RVA: 0x0005A154 File Offset: 0x00058354
	public static void PlayMusic(string Track, string Channel = "music", bool Crossfade = true, float CrossfadeDuration = 12f, float VolumeAttenuation = 1f, Action OnPlay = null, bool Loop = true)
	{
		if (SoundManager.RequestPool.Count == 0)
		{
			Debug.LogWarning("sound request pool was empty for request " + Track);
			if (OnPlay != null)
			{
				OnPlay();
			}
			return;
		}
		SoundRequest soundRequest = SoundManager.RequestPool.Dequeue();
		soundRequest.Channel = Channel;
		soundRequest.Clip = Track;
		soundRequest.Crossfade = Crossfade;
		soundRequest.CrossfadeDuration = CrossfadeDuration;
		soundRequest.Volume = VolumeAttenuation;
		soundRequest.Type = SoundRequest.SoundRequestType.Music;
		soundRequest.OnPlay = OnPlay;
		soundRequest.Loop = Loop;
		if (!BeforePlayMusicEvent.Check(soundRequest))
		{
			SoundManager.RequestPool.Enqueue(soundRequest);
			if (OnPlay != null)
			{
				OnPlay();
			}
			return;
		}
		Queue<SoundRequest> soundRequests = SoundManager.SoundRequests;
		lock (soundRequests)
		{
			SoundManager.SoundRequests.Enqueue(soundRequest);
		}
	}

	// Token: 0x06000E03 RID: 3587 RVA: 0x0005A228 File Offset: 0x00058428
	public static void PlayWorldSound(string Clip, int Distance, bool Occluded, float VolumeIntensity, Location2D Cell, float PitchVariance = 0f, float Delay = 0f, float Pitch = 1f)
	{
		SoundManager.PlayWorldSound(Clip, Distance, Occluded, VolumeIntensity, Cell.Point, PitchVariance, Delay, Pitch);
	}

	// Token: 0x06000E04 RID: 3588 RVA: 0x0005A240 File Offset: 0x00058440
	public static void PlayWorldSound(string Clip, int Distance, bool Occluded, float VolumeIntensity, Point2D Cell, float PitchVariance = 0f, float Delay = 0f, float Pitch = 1f)
	{
		int count = SoundManager.RequestPool.Count;
		if (count == 0)
		{
			Debug.LogWarning("sound request pool was empty for request " + Clip);
			return;
		}
		VolumeIntensity *= SoundManager.GetSoundFrameVolume(Clip);
		Queue<SoundRequest> queue;
		if (Occluded)
		{
			if (Distance == 9999)
			{
				return;
			}
			if ((float)Distance > 40f * VolumeIntensity)
			{
				return;
			}
			SoundRequest soundRequest = SoundManager.RequestPool.Dequeue();
			if (soundRequest == null)
			{
				Debug.LogWarning("null from request pool even though count was " + count.ToString() + " for " + Clip);
				return;
			}
			soundRequest.Cell = Cell;
			soundRequest.Clip = Clip;
			soundRequest.Type = SoundRequest.SoundRequestType.Spatial;
			soundRequest.LowPass = 0.15f + 0.55f * ((float)(80 - Distance) / 80f);
			soundRequest.Volume = VolumeIntensity * ((float)((40 - Distance) * (40 - Distance)) / 1600f);
			soundRequest.PitchVariance = PitchVariance;
			soundRequest.Delay = Delay;
			soundRequest.Pitch = Pitch;
			queue = SoundManager.SoundRequests;
			lock (queue)
			{
				SoundManager.SoundRequests.Enqueue(soundRequest);
				return;
			}
		}
		if (Distance > 40)
		{
			return;
		}
		SoundRequest soundRequest2 = SoundManager.RequestPool.Dequeue();
		if (soundRequest2 == null)
		{
			Debug.LogWarning("null from request pool even though count was " + count.ToString() + " for " + Clip);
			return;
		}
		soundRequest2.Cell = Cell;
		soundRequest2.Clip = Clip;
		soundRequest2.Type = SoundRequest.SoundRequestType.Spatial;
		soundRequest2.LowPass = 1f;
		soundRequest2.Volume = VolumeIntensity * ((float)((40 - Distance) * (40 - Distance)) / 1600f);
		soundRequest2.PitchVariance = PitchVariance;
		soundRequest2.Delay = Delay;
		soundRequest2.Pitch = Pitch;
		queue = SoundManager.SoundRequests;
		lock (queue)
		{
			SoundManager.SoundRequests.Enqueue(soundRequest2);
		}
	}

	// Token: 0x06000E05 RID: 3589 RVA: 0x0005A41C File Offset: 0x0005861C
	public static void PlayUISound(string Clip, float Volume = 1f, bool Combat = false, bool Interface = false, SoundRequest.SoundEffectType Effect = SoundRequest.SoundEffectType.None)
	{
		if (Interface)
		{
			if (Effect != SoundRequest.SoundEffectType.None)
			{
				MetricsManager.LogWarning("You can't use a spatial effect type on an interface sound.");
			}
			else
			{
				Effect = SoundRequest.SoundEffectType.None;
			}
		}
		if (!Clip.IsNullOrEmpty() && Options.Sound && (!Combat || Options.UseCombatSounds) && (!Interface || Options.UseInterfaceSounds))
		{
			if (Interface)
			{
				Volume *= Globals.InterfaceVolume;
			}
			if (Combat)
			{
				Volume *= Globals.CombatVolume;
			}
			SoundManager.PlaySound(Clip, 0f, Volume, 1f, Effect, 0f);
		}
	}

	// Token: 0x06000E06 RID: 3590 RVA: 0x0005A494 File Offset: 0x00058694
	public static void PlaySound(string Clip, float PitchVariance = 0f, float Volume = 1f, float Pitch = 1f, SoundRequest.SoundEffectType Effect = SoundRequest.SoundEffectType.None, float Delay = 0f)
	{
		if (Effect != SoundRequest.SoundEffectType.None)
		{
			Volume *= SoundManager.GetSoundFrameVolume(Clip);
			if (Volume <= 0f)
			{
				return;
			}
			Effect = SoundRequest.SoundEffectType.None;
		}
		if (SoundManager.RequestPool.Count == 0)
		{
			Debug.LogWarning("sound request pool was empty for request " + Clip);
			return;
		}
		SoundRequest soundRequest = SoundManager.RequestPool.Dequeue();
		soundRequest.Clip = Clip;
		soundRequest.Type = SoundRequest.SoundRequestType.Sound;
		soundRequest.Pitch = Pitch;
		soundRequest.PitchVariance = PitchVariance;
		soundRequest.Volume = Volume;
		soundRequest.Effect = Effect;
		soundRequest.Delay = Delay;
		Queue<SoundRequest> soundRequests = SoundManager.SoundRequests;
		lock (soundRequests)
		{
			SoundManager.SoundRequests.Enqueue(soundRequest);
		}
	}

	// Token: 0x06000E07 RID: 3591 RVA: 0x0005A550 File Offset: 0x00058750
	public static List<SoundRequest> GetRequests()
	{
		if (SoundManager.Requests.Count > 0)
		{
			SoundManager.Requests.Clear();
		}
		if (SoundManager.SoundRequests.Count > 0)
		{
			Queue<SoundRequest> soundRequests = SoundManager.SoundRequests;
			lock (soundRequests)
			{
				while (SoundManager.SoundRequests.Count > 0)
				{
					SoundRequest soundRequest = SoundManager.SoundRequests.Dequeue();
					if (soundRequest.Delay > 0f)
					{
						soundRequest.Delay -= Time.deltaTime;
					}
					if (soundRequest.Delay <= 0f)
					{
						SoundManager.Requests.Add(soundRequest);
						SoundManager.RequestLogs[SoundManager.RequestLogNextIndex].Set(Time.fixedUnscaledTime, soundRequest);
						SoundManager.RequestLogNextIndex = (SoundManager.RequestLogNextIndex + 1) % SoundManager.RequestLogs.Length;
					}
					else
					{
						SoundManager.DelayedRequests.Enqueue(soundRequest);
					}
				}
				if (SoundManager.DelayedRequests.Count > 0)
				{
					Queue<SoundRequest> soundRequests2 = SoundManager.SoundRequests;
					SoundManager.SoundRequests = SoundManager.DelayedRequests;
					SoundManager.DelayedRequests = soundRequests2;
				}
			}
		}
		return SoundManager.Requests;
	}

	// Token: 0x06000E08 RID: 3592 RVA: 0x0005A664 File Offset: 0x00058864
	public static void UnloadClipSet(string ClipID)
	{
		AudioEntrySet audioEntrySet;
		if (SoundManager.TryGetClipSet(ClipID, out audioEntrySet))
		{
			audioEntrySet.Unload();
		}
	}

	// Token: 0x17000155 RID: 341
	// (get) Token: 0x06000E09 RID: 3593 RVA: 0x0005A684 File Offset: 0x00058884
	public static string MusicTrack
	{
		get
		{
			MusicSource musicSource;
			if (!SoundManager.MusicSources.TryGetValue("music", out musicSource))
			{
				return null;
			}
			return musicSource.Track;
		}
	}

	// Token: 0x06000E0A RID: 3594 RVA: 0x0005A6AC File Offset: 0x000588AC
	public static MusicSource RequireMusicSource(string Channel, bool Reset = false)
	{
		MusicSource musicSource;
		if (SoundManager.MusicSources.TryGetValue(Channel, out musicSource))
		{
			if (!Reset)
			{
				return musicSource;
			}
		}
		else
		{
			musicSource = (SoundManager.MusicSources[Channel] = ObjectPool<MusicSource>.Checkout());
		}
		musicSource.Channel = Channel;
		musicSource.Type = (Channel.StartsWith("ambient_bed") ? MusicType.Ambience : MusicType.General);
		musicSource.Enabled = true;
		musicSource.TargetVolume = 0f;
		musicSource.Track = null;
		musicSource.SetMusicBackground(Options.MusicBackground);
		return musicSource;
	}

	// Token: 0x06000E0B RID: 3595 RVA: 0x0005A724 File Offset: 0x00058924
	public static void Init()
	{
		SoundManager.RequireMusicSource("music", false);
		SoundManager.RequireMusicSource("ambient_bed", false);
		SoundManager.RequireMusicSource("ambient_bed_2", false);
		SoundManager.RequireMusicSource("ambient_bed_3", false);
		if (SoundManager.UIAudioSources == null)
		{
			SoundManager.UIAudioSources = new Queue<AudioSource>();
			for (int i = 0; i < SoundManager.UI_AUDIO_SOURCES; i++)
			{
				global::UnityEngine.GameObject gameObject = new global::UnityEngine.GameObject();
				gameObject.AddComponent<AudioSource>();
				AudioSource component = gameObject.GetComponent<AudioSource>();
				gameObject.transform.position = new Vector3(0f, 0f, 1f);
				gameObject.transform.parent = global::UnityEngine.GameObject.Find("AudioListener").transform;
				gameObject.name = "UISound";
				component.priority = 16;
				SoundManager.UIAudioSources.Enqueue(component);
			}
		}
		for (int j = 0; j < 256; j++)
		{
			SoundManager.RequestPool.Enqueue(new SoundRequest());
		}
	}

	// Token: 0x06000E0C RID: 3596 RVA: 0x0005A80D File Offset: 0x00058A0D
	private static void setClipName(string channel, string value)
	{
		SoundManager.ClipNames[channel] = value;
	}

	// Token: 0x06000E0D RID: 3597 RVA: 0x0005A81C File Offset: 0x00058A1C
	private static string getClipName(string channel)
	{
		string text;
		if (SoundManager.ClipNames.TryGetValue(channel, out text))
		{
			return text;
		}
		return null;
	}

	// Token: 0x06000E0E RID: 3598 RVA: 0x0005A83C File Offset: 0x00058A3C
	public static async UniTask SetChannelTrack(string Track, string Channel, bool Crossfade, float CrossfadeDuration = 12f, float VolumeAttenuation = 1f, int Priority = 0, Action OnPlay = null, bool Loop = true)
	{
		MusicSource source = SoundManager.RequireMusicSource(Channel, false);
		AudioSource audioSource = source.Audio;
		if (source.Track == Track && audioSource.isPlaying)
		{
			source.TargetVolume = VolumeAttenuation;
		}
		else
		{
			if (Crossfade && audioSource.isPlaying && audioSource.volume > 0f)
			{
				SoundManager.MusicSources.Remove(Channel);
				MusicSource musicSource = source;
				musicSource.Fade.StartFade(CrossfadeDuration);
				musicSource.Channel = "Crossfade";
				source = SoundManager.RequireMusicSource(Channel, true);
				audioSource = source.Audio;
			}
			source.Track = Track;
			if (Track == null)
			{
				audioSource.Stop();
				source.Reset();
			}
			else
			{
				if (source.Enabled)
				{
					audioSource.Stop();
					audioSource.volume = 0f;
					audioSource.loop = false;
					AudioEntrySet set;
					if (!SoundManager.TryGetClipSet(Track, out set) || set.Count == 0)
					{
						if (SoundManager.WriteSoundsToLog)
						{
							MessageQueue.AddPlayerMessage(Channel + ": " + Track + " (Wasn't found)", null, true);
						}
						MetricsManager.LogWarning("SoundManager::Missing music track: " + Track);
						source.Reset();
						return;
					}
					if (!set.Initialized)
					{
						await set.Load();
					}
					if (!set.Shuffle)
					{
						set.Index = 0;
					}
					source.EntrySet = set;
					source.EventTime = AudioSettings.dspTime;
					set = null;
				}
				source.TargetVolume = VolumeAttenuation;
				source.SetAudioVolume(Crossfade ? 0f : VolumeAttenuation);
				source.Loop = Loop;
				source.FirstPlay = true;
				if (OnPlay != null)
				{
					OnPlay();
				}
				if (SoundManager.WriteSoundsToLog)
				{
					MessageQueue.AddPlayerMessage(Channel + ": " + Track, null, true);
				}
			}
		}
	}

	// Token: 0x06000E0F RID: 3599 RVA: 0x0005A8B4 File Offset: 0x00058AB4
	public static async UniTask _PlaySound(string Name, float Volume, float Pitch = 1f, SoundRequest.SoundEffectType Effect = SoundRequest.SoundEffectType.None)
	{
		AudioEntrySet clipSet = SoundManager.GetClipSet(Name, true);
		AudioEntry audioEntry = ((clipSet != null) ? clipSet.Next() : null);
		if (audioEntry == null)
		{
			if (SoundManager.WriteSoundsToLog)
			{
				MessageQueue.AddPlayerMessage(Name + " (missing)", null, true);
				MetricsManager.LogWarning("Missing sound: " + Name);
			}
		}
		else
		{
			AudioClip audioClip = await audioEntry.GetClip();
			if (!audioClip)
			{
				if (SoundManager.WriteSoundsToLog)
				{
					MessageQueue.AddPlayerMessage(Name + " (invalid)", null, true);
					MetricsManager.LogWarning("Invalid sound: " + Name);
				}
			}
			else
			{
				if (SoundManager.WriteSoundsToLog)
				{
					MessageQueue.AddPlayerMessage(Name, null, true);
				}
				if (Effect == SoundRequest.SoundEffectType.None)
				{
					AudioSource audioSource = SoundManager.UIAudioSources.Dequeue();
					SoundManager.UIAudioSources.Enqueue(audioSource);
					audioSource.pitch = Pitch;
					audioSource.PlayOneShot(audioClip, SoundManager.SoundVolume * Volume);
				}
				else
				{
					global::UnityEngine.GameObject audioSourceFromPool = SoundManager.getAudioSourceFromPool();
					AudioSource component = audioSourceFromPool.GetComponent<AudioSource>();
					AudioLowPassFilter component2 = audioSourceFromPool.GetComponent<AudioLowPassFilter>();
					component.clip = audioClip;
					component.volume = Volume;
					component.pitch = 1f;
					component.transform.position = SoundManager.UIAudioSources.Peek().transform.position;
					component2.cutoffFrequency = 22000f;
					AudioSourceEffect audioSourceEffect = audioSourceFromPool.GetComponent<AudioSourceEffect>();
					if (audioSourceEffect == null)
					{
						audioSourceEffect = audioSourceFromPool.AddComponent<AudioSourceEffect>();
					}
					audioSourceEffect.Effect = Effect;
					SoundManager.PlayingAudioSources.Enqueue(audioSourceFromPool);
					component.Play();
				}
			}
		}
	}

	// Token: 0x06000E10 RID: 3600 RVA: 0x0005A910 File Offset: 0x00058B10
	public static global::UnityEngine.GameObject getAudioSourceFromPool()
	{
		global::UnityEngine.GameObject gameObject;
		if (SoundManager.AudioSourcePool.Count > 0)
		{
			gameObject = SoundManager.AudioSourcePool.Dequeue();
		}
		else
		{
			gameObject = new global::UnityEngine.GameObject();
			gameObject.transform.position = new Vector3(0f, 0f, 1f);
			gameObject.name = "PooledWorldSound";
			AudioSource audioSource = gameObject.AddComponent<AudioSource>();
			gameObject.AddComponent<AudioLowPassFilter>();
			audioSource.dopplerLevel = 0f;
			audioSource.minDistance = 0f;
			audioSource.maxDistance = 10000f;
			audioSource.SetCustomCurve(AudioSourceCurveType.CustomRolloff, SoundManager.distanceCurve);
			audioSource.spatialBlend = 0.5f;
			audioSource.priority = 128;
			global::UnityEngine.Object.DontDestroyOnLoad(gameObject);
		}
		AudioSourceEffect.PoolReset(gameObject.GetComponent<AudioSource>());
		return gameObject;
	}

	// Token: 0x06000E11 RID: 3601 RVA: 0x0005A9D0 File Offset: 0x00058BD0
	public static async UniTask _PlayWorldSound(string Name, float Volume, float LowPass, float Pitch, float PitchVariance, Point2D Cell)
	{
		AudioEntrySet clipSet = SoundManager.GetClipSet(Name, true);
		AudioEntry audioEntry = ((clipSet != null) ? clipSet.Next() : null);
		if (audioEntry == null)
		{
			if (SoundManager.WriteSoundsToLog)
			{
				MessageQueue.AddPlayerMessage(Name + " (missing)", null, true);
				MetricsManager.LogWarning("Missing sound: " + Name);
			}
		}
		else
		{
			AudioClip audioClip = await audioEntry.GetClip();
			if (!audioClip)
			{
				if (SoundManager.WriteSoundsToLog)
				{
					MessageQueue.AddPlayerMessage(Name + " (invalid)", null, true);
					MetricsManager.LogWarning("Invalid sound: " + Name);
				}
			}
			else
			{
				if (SoundManager.WriteSoundsToLog)
				{
					MessageQueue.AddPlayerMessage(Name, null, true);
				}
				global::UnityEngine.GameObject audioSourceFromPool = SoundManager.getAudioSourceFromPool();
				AudioSource component = audioSourceFromPool.GetComponent<AudioSource>();
				AudioLowPassFilter component2 = audioSourceFromPool.GetComponent<AudioLowPassFilter>();
				component.clip = audioClip;
				component.volume = Volume;
				component.pitch = Pitch - ((float)Stat.Rnd5.NextDouble() * 2f - 1f) * PitchVariance;
				component.transform.position = GameManager.Instance.GetCellCenter(Cell.x, Cell.y, -10);
				component2.cutoffFrequency = 22000f * LowPass * Volume;
				SoundManager.PlayingAudioSources.Enqueue(audioSourceFromPool);
				component.Play();
			}
		}
	}

	// Token: 0x06000E12 RID: 3602 RVA: 0x0005AA40 File Offset: 0x00058C40
	public static AudioSource CreateAudioSource(float volume, float lowPass, float pitchVariance)
	{
		global::UnityEngine.GameObject gameObject;
		if (SoundManager.AudioSourcePool.Count > 0)
		{
			gameObject = SoundManager.AudioSourcePool.Dequeue();
		}
		else
		{
			gameObject = new global::UnityEngine.GameObject();
			gameObject.transform.position = new Vector3(0f, 0f, 1f);
			gameObject.name = "PooledWorldSound";
			gameObject.AddComponent<AudioSource>().priority = 128;
			gameObject.AddComponent<AudioLowPassFilter>();
			global::UnityEngine.Object.DontDestroyOnLoad(gameObject);
		}
		AudioSource component = gameObject.GetComponent<AudioSource>();
		AudioLowPassFilter component2 = gameObject.GetComponent<AudioLowPassFilter>();
		AudioSourceEffect.PoolReset(gameObject.GetComponent<AudioSource>());
		component.volume = volume;
		component.pitch = 1f - ((float)Stat.Rnd5.NextDouble() * 2f - 1f) * pitchVariance;
		component2.cutoffFrequency = 22000f * lowPass * volume;
		return component;
	}

	// Token: 0x06000E13 RID: 3603 RVA: 0x0005AB0C File Offset: 0x00058D0C
	public static void Update()
	{
		SoundManager.soundsPlayedThisFrame.Clear();
		if (!Globals.EnableSound && SoundManager.PlayingAudioSources.Count > 0)
		{
			while (SoundManager.PlayingAudioSources.Count > 0)
			{
				global::UnityEngine.GameObject gameObject = SoundManager.PlayingAudioSources.Dequeue();
				gameObject.GetComponent<AudioSource>().Stop();
				SoundManager.AudioSourcePool.Enqueue(gameObject);
			}
		}
		AudioListener.volume = SoundManager.MasterVolume;
		SoundManager.MusicSources.Update();
		while (SoundManager.PlayingAudioSources.Count > 0 && !SoundManager.PlayingAudioSources.Peek().GetComponent<AudioSource>().isPlaying)
		{
			SoundManager.AudioSourcePool.Enqueue(SoundManager.PlayingAudioSources.Dequeue());
		}
		SoundManager.GetRequests();
		int i = 0;
		while (i < SoundManager.Requests.Count)
		{
			SoundRequest soundRequest = SoundManager.Requests[i];
			if (soundRequest.Type != SoundRequest.SoundRequestType.Spatial && soundRequest.Type != SoundRequest.SoundRequestType.Sound)
			{
				goto IL_00F6;
			}
			if (soundRequest.Clip != null && !SoundManager.soundsPlayedThisFrame.Contains(soundRequest.Clip))
			{
				SoundManager.soundsPlayedThisFrame.Add(soundRequest.Clip);
				goto IL_00F6;
			}
			IL_019E:
			i++;
			continue;
			IL_00F6:
			if (soundRequest.Type == SoundRequest.SoundRequestType.Spatial)
			{
				if (Options.Sound)
				{
					SoundManager._PlayWorldSound(soundRequest.Clip, soundRequest.Volume, soundRequest.LowPass, soundRequest.Pitch, soundRequest.PitchVariance, soundRequest.Cell);
					goto IL_019E;
				}
				goto IL_019E;
			}
			else if (soundRequest.Type == SoundRequest.SoundRequestType.Sound)
			{
				if (Options.Sound)
				{
					SoundManager._PlaySound(soundRequest.Clip, soundRequest.Volume, soundRequest.Pitch, soundRequest.Effect);
					goto IL_019E;
				}
				goto IL_019E;
			}
			else
			{
				if (soundRequest.Type == SoundRequest.SoundRequestType.Music)
				{
					SoundManager.SetChannelTrack(soundRequest.Clip, soundRequest.Channel, soundRequest.Crossfade, soundRequest.CrossfadeDuration, soundRequest.Volume, 0, soundRequest.OnPlay, soundRequest.Loop);
					goto IL_019E;
				}
				goto IL_019E;
			}
		}
		for (int j = 0; j < SoundManager.Requests.Count; j++)
		{
			if (SoundManager.Requests[j] == null)
			{
				Debug.LogError("Requests had a null entry!");
			}
			else
			{
				SoundManager.Requests[j].OnPlay = null;
				SoundManager.Requests[j].Loop = true;
				SoundManager.RequestPool.Enqueue(SoundManager.Requests[j]);
			}
		}
		SoundManager.Requests.Clear();
	}

	// Token: 0x06000E14 RID: 3604 RVA: 0x0005AD3C File Offset: 0x00058F3C
	[ModSensitiveCacheInit]
	public static void Initialize()
	{
		SoundManager.ClipFrames.Clear();
		List<string> list = new List<string>();
		int num = 0;
		foreach (ModInfo modInfo in ModManager.ActiveMods)
		{
			foreach (ModFile modFile in modInfo.Files)
			{
				if (modFile.Type == ModFileType.Audio)
				{
					list.Add(modFile.OriginalName);
					num++;
				}
			}
		}
		SoundDatabaseScriptable soundDatabaseScriptable = Resources.Load<SoundDatabaseScriptable>("SoundDatabase");
		if (soundDatabaseScriptable)
		{
			list.AddRange(soundDatabaseScriptable.Paths);
		}
		else
		{
			MetricsManager.LogError("Sound database not found, no sounds will be played");
		}
		MetricsManager.LogInfo(string.Format("Audio files: {0} base, {1} mod", list.Count - num, num));
		SoundManager.SetMapLock.EnterWriteLock();
		try
		{
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				string text = list[i];
				bool flag = i < num;
				ReadOnlySpan<char> readOnlySpan;
				int num2;
				if (SoundManager.TryGetAudioVariant(text, out readOnlySpan, out num2))
				{
					AudioEntrySet audioEntrySet;
					if (!SoundManager.SetMap.TryGetValue(readOnlySpan, out audioEntrySet))
					{
						string text2 = text;
						if (num2 != -1)
						{
							int num3 = text2.LastIndexOf('-');
							if (num3 != -1)
							{
								text2 = text2.Substring(0, num3);
							}
						}
						audioEntrySet = (flag ? new ModAudioEntrySet() : new AudioEntrySet());
						audioEntrySet.Initialize(text2);
						SoundManager.SetMap.Add(readOnlySpan, audioEntrySet);
					}
					if (audioEntrySet.IndexOfVariant(num2) == -1)
					{
						AudioEntry audioEntry = (flag ? new ModAudioEntry() : new AudioEntry());
						audioEntry.Path = text;
						audioEntry.Variant = num2;
						audioEntrySet.Add(audioEntry);
					}
				}
				i++;
			}
		}
		finally
		{
			SoundManager.SetMapLock.ExitWriteLock();
		}
		if (Options.GetOption("OptionDisableSoundPreload", "") != "Yes")
		{
			Debug.Log("Preloading sounds...");
			SoundManager.SetMapLock.EnterReadLock();
			try
			{
				foreach (KeyValuePair<string, AudioEntrySet> keyValuePair in SoundManager.SetMap)
				{
					string text3;
					AudioEntrySet audioEntrySet2;
					keyValuePair.Deconstruct(out text3, out audioEntrySet2);
					audioEntrySet2.Load().Forget();
				}
			}
			finally
			{
				SoundManager.SetMapLock.ExitReadLock();
			}
		}
	}

	// Token: 0x06000E15 RID: 3605 RVA: 0x0005AFD0 File Offset: 0x000591D0
	public static bool TryGetClipSet(string Name, out AudioEntrySet Set)
	{
		SoundManager.SetMapLock.EnterReadLock();
		try
		{
			if (!SoundManager.SetMap.TryGetValue(Name, out Set))
			{
				ReadOnlySpan<char> soundName = SoundManager.GetSoundName(Name);
				if (!SoundManager.SetMap.TryGetValue(soundName, out Set))
				{
					return false;
				}
			}
		}
		finally
		{
			SoundManager.SetMapLock.ExitReadLock();
		}
		return true;
	}

	// Token: 0x06000E16 RID: 3606 RVA: 0x0005B030 File Offset: 0x00059230
	public unsafe static ReadOnlySpan<char> GetSoundName(string Path)
	{
		if (Path.IsNullOrEmpty())
		{
			return ReadOnlySpan<char>.Empty;
		}
		ReadOnlySpan<char> readOnlySpan = Path.AsSpan();
		int num = readOnlySpan.LastIndexOf('.');
		if (num != -1)
		{
			readOnlySpan = readOnlySpan.Slice(0, num);
		}
		num = readOnlySpan.LastIndexOf('/');
		if (num != -1)
		{
			readOnlySpan = readOnlySpan.Slice(num + 1);
		}
		num = readOnlySpan.LastIndexOf('\\');
		if (num != -1)
		{
			readOnlySpan = readOnlySpan.Slice(num + 1);
		}
		int length = readOnlySpan.Length;
		char[] array = SoundManager.KeyBuffer.GetArray(length);
		for (int i = 0; i < length; i++)
		{
			array[i] = char.ToLowerInvariant((char)(*readOnlySpan[i]));
		}
		return array.AsSpan<char>(0, length);
	}

	// Token: 0x06000E17 RID: 3607 RVA: 0x0005B0E0 File Offset: 0x000592E0
	public unsafe static bool TryGetAudioVariant(string Path, out ReadOnlySpan<char> Key, out int Variant)
	{
		Variant = -1;
		if (Path.IsNullOrEmpty())
		{
			Key = ReadOnlySpan<char>.Empty;
			return false;
		}
		Key = SoundManager.GetSoundName(Path);
		int i = Key.Length - 1;
		int num = 0;
		while (i >= 0)
		{
			char c = (char)(*Key[i]);
			if (char.IsDigit(c))
			{
				num++;
				i--;
			}
			else
			{
				int num2;
				if (num > 0 && !char.IsLetter(c) && int.TryParse(Key.Slice(i + 1, num), out num2))
				{
					Key = Key.Slice(0, i);
					Variant = num2;
					break;
				}
				break;
			}
		}
		return true;
	}

	// Token: 0x06000E18 RID: 3608 RVA: 0x0005B170 File Offset: 0x00059370
	public static AudioType GetAudioTypeFromFile(string path)
	{
		if (path.EndsWith(".wav", StringComparison.OrdinalIgnoreCase))
		{
			return AudioType.WAV;
		}
		if (path.EndsWith(".aiff", StringComparison.OrdinalIgnoreCase))
		{
			return AudioType.AIFF;
		}
		if (path.EndsWith(".ogg", StringComparison.OrdinalIgnoreCase))
		{
			return AudioType.OGGVORBIS;
		}
		if (path.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase))
		{
			return AudioType.MPEG;
		}
		return AudioType.UNKNOWN;
	}

	// Token: 0x06000E19 RID: 3609 RVA: 0x0005B1C4 File Offset: 0x000593C4
	public static void PreloadClipSet(string Name)
	{
		SoundManager.<>c__DisplayClass52_0 CS$<>8__locals1 = new SoundManager.<>c__DisplayClass52_0();
		if (SoundManager.TryGetClipSet(Name, out CS$<>8__locals1.set) && !CS$<>8__locals1.set.Initialized)
		{
			UniTask.RunOnThreadPool(delegate
			{
				SoundManager.<>c__DisplayClass52_0.<<PreloadClipSet>b__0>d <<PreloadClipSet>b__0>d;
				<<PreloadClipSet>b__0>d.<>t__builder = AsyncUniTaskMethodBuilder.Create();
				<<PreloadClipSet>b__0>d.<>4__this = CS$<>8__locals1;
				<<PreloadClipSet>b__0>d.<>1__state = -1;
				<<PreloadClipSet>b__0>d.<>t__builder.Start<SoundManager.<>c__DisplayClass52_0.<<PreloadClipSet>b__0>d>(ref <<PreloadClipSet>b__0>d);
				return <<PreloadClipSet>b__0>d.<>t__builder.Task;
			}, true, default(CancellationToken));
		}
	}

	// Token: 0x06000E1A RID: 3610 RVA: 0x0005B210 File Offset: 0x00059410
	public static AudioEntrySet GetClipSet(string Name, bool Preload = false)
	{
		AudioEntrySet audioEntrySet;
		if (!SoundManager.TryGetClipSet(Name, out audioEntrySet))
		{
			return null;
		}
		if (Preload && !audioEntrySet.Initialized)
		{
			audioEntrySet.Load();
		}
		return audioEntrySet;
	}

	// Token: 0x06000E1B RID: 3611 RVA: 0x0005B23C File Offset: 0x0005943C
	public static float GetSoundFrameVolume(string Clip)
	{
		if (Clip == null)
		{
			return 0f;
		}
		float num = 1f;
		int currentFrameAtFPS = XRLCore.GetCurrentFrameAtFPS(50, 1f);
		int[] array;
		if (SoundManager.ClipFrames.TryGetValue(Clip, out array))
		{
			if (array[0] >= currentFrameAtFPS - 1)
			{
				array[0] = currentFrameAtFPS;
				int[] array2 = array;
				int num2 = 1;
				int num3 = array2[num2] + 1;
				array2[num2] = num3;
				int num4 = num3;
				if (num4 >= 5)
				{
					return 0f;
				}
				num /= (float)(num4 * num4);
			}
			else
			{
				array[0] = currentFrameAtFPS;
				array[1] = 1;
			}
		}
		else
		{
			SoundManager.ClipFrames[Clip] = new int[] { currentFrameAtFPS, 1 };
		}
		return num;
	}

	// Token: 0x040009B4 RID: 2484
	private static Dictionary<string, int[]> ClipFrames = new Dictionary<string, int[]>();

	// Token: 0x040009B5 RID: 2485
	public static bool WriteSoundsToLog;

	// Token: 0x040009B6 RID: 2486
	private static SoundRequestLog[] RequestLogs = (from _ in Enumerable.Range(0, 100)
		select new SoundRequestLog()).ToArray<SoundRequestLog>();

	// Token: 0x040009B7 RID: 2487
	private static int RequestLogNextIndex = 0;

	// Token: 0x040009B8 RID: 2488
	public static Queue<SoundRequest> RequestPool = new Queue<SoundRequest>();

	// Token: 0x040009B9 RID: 2489
	public static Queue<SoundRequest> SoundRequests = new Queue<SoundRequest>();

	// Token: 0x040009BA RID: 2490
	private static List<SoundRequest> Requests = new List<SoundRequest>();

	// Token: 0x040009BB RID: 2491
	public static AnimationCurve distanceCurve = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 1f),
		new Keyframe(1f, 1f)
	});

	// Token: 0x040009BC RID: 2492
	private static Queue<SoundRequest> DelayedRequests = new Queue<SoundRequest>();

	// Token: 0x040009BD RID: 2493
	public static MusicSourceCollection MusicSources = new MusicSourceCollection();

	// Token: 0x040009BE RID: 2494
	public static global::UnityEngine.GameObject UISource = null;

	// Token: 0x040009BF RID: 2495
	public static Queue<AudioSource> UIAudioSources = null;

	// Token: 0x040009C0 RID: 2496
	public static int UI_AUDIO_SOURCES = 24;

	// Token: 0x040009C1 RID: 2497
	private static Queue<global::UnityEngine.GameObject> AudioSourcePool = new Queue<global::UnityEngine.GameObject>();

	// Token: 0x040009C2 RID: 2498
	private static Queue<global::UnityEngine.GameObject> PlayingAudioSources = new Queue<global::UnityEngine.GameObject>();

	// Token: 0x040009C3 RID: 2499
	private static Dictionary<string, string> ClipNames = new Dictionary<string, string>();

	// Token: 0x040009C4 RID: 2500
	public static float MasterVolume = 1f;

	// Token: 0x040009C5 RID: 2501
	public static float SoundVolume = 1f;

	// Token: 0x040009C6 RID: 2502
	public static float MusicVolume = 1f;

	// Token: 0x040009C7 RID: 2503
	private static HashSet<string> soundsPlayedThisFrame = new HashSet<string>();

	// Token: 0x040009C8 RID: 2504
	public static StringMap<AudioEntrySet> SetMap = new StringMap<AudioEntrySet>();

	// Token: 0x040009C9 RID: 2505
	public static ReaderWriterLockSlim SetMapLock = new ReaderWriterLockSlim();

	// Token: 0x040009CA RID: 2506
	private static Rack<char> KeyBuffer = new Rack<char>(128);

	// Token: 0x02001609 RID: 5641
	public class ModSoundFile
	{
		// Token: 0x0600D69A RID: 54938 RVA: 0x004F911E File Offset: 0x004F731E
		public ModSoundFile(ModFile File, AudioType Type)
		{
			this.File = File;
			this.Type = Type;
		}

		// Token: 0x040058C7 RID: 22727
		public ModFile File;

		// Token: 0x040058C8 RID: 22728
		public AudioType Type;
	}
}
