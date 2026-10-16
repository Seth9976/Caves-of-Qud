using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000143 RID: 323
[RequireComponent(typeof(AudioSource))]
[AddComponentMenu("Accessibility/Core/UAP Audio Queue")]
public class UAP_AudioQueue : MonoBehaviour
{
	// Token: 0x06000CA5 RID: 3237 RVA: 0x000540F4 File Offset: 0x000522F4
	public void QueueAudio(string textForTTS, UAP_AudioQueue.EAudioType type, bool allowVoiceOver, UAP_AudioQueue.UAP_GenericCallback callbackOnDone = null, UAP_AudioQueue.EInterrupt interruptsAudioTypes = UAP_AudioQueue.EInterrupt.None, bool isInterruptible = true)
	{
		this.QueueAudio(new UAP_AudioQueue.SAudioEntry
		{
			m_TTS_Text = textForTTS,
			m_AllowVoiceOver = allowVoiceOver,
			m_AudioType = type,
			m_IsInterruptible = isInterruptible,
			m_CallbackOnDone = callbackOnDone
		}, interruptsAudioTypes);
	}

	// Token: 0x06000CA6 RID: 3238 RVA: 0x00054138 File Offset: 0x00052338
	public void QueueAudio(AudioClip audioFile, UAP_AudioQueue.EAudioType type, UAP_AudioQueue.UAP_GenericCallback callbackOnDone = null, UAP_AudioQueue.EInterrupt interruptsAudioTypes = UAP_AudioQueue.EInterrupt.None, bool isInterruptible = true)
	{
		this.QueueAudio(new UAP_AudioQueue.SAudioEntry
		{
			m_Audio = audioFile,
			m_AudioType = type,
			m_IsInterruptible = isInterruptible,
			m_CallbackOnDone = callbackOnDone
		}, interruptsAudioTypes);
	}

	// Token: 0x06000CA7 RID: 3239 RVA: 0x00054174 File Offset: 0x00052374
	public void QueuePause(float durationInSecs)
	{
		this.QueueAudio(new UAP_AudioQueue.SAudioEntry
		{
			m_AudioType = UAP_AudioQueue.EAudioType.Pause,
			m_PauseDuration = durationInSecs
		}, UAP_AudioQueue.EInterrupt.None);
	}

	// Token: 0x06000CA8 RID: 3240 RVA: 0x0005419D File Offset: 0x0005239D
	public void Stop()
	{
		this.StopAudio(true);
		this.m_AudioQueue.Clear();
		this.InvalidateActiveEntry();
	}

	// Token: 0x06000CA9 RID: 3241 RVA: 0x000541B8 File Offset: 0x000523B8
	public void StopAllInterruptibles()
	{
		if (this.m_ActiveEntry != null && this.m_ActiveEntry.m_IsInterruptible)
		{
			this.StopAudio(false);
			this.InvalidateActiveEntry();
		}
		UAP_AudioQueue.SAudioEntry[] array = this.m_AudioQueue.ToArray();
		this.m_AudioQueue.Clear();
		for (int i = 0; i < array.Length; i++)
		{
			if (!array[i].m_IsInterruptible)
			{
				this.m_AudioQueue.Enqueue(array[i]);
			}
		}
	}

	// Token: 0x06000CAA RID: 3242 RVA: 0x00054224 File Offset: 0x00052424
	public void InterruptAppAnnouncement()
	{
		if (this.m_ActiveEntry != null && this.m_ActiveEntry.m_AudioType == UAP_AudioQueue.EAudioType.App && this.m_ActiveEntry.m_IsInterruptible)
		{
			this.StopAudio(false);
			this.InvalidateActiveEntry();
		}
	}

	// Token: 0x06000CAB RID: 3243 RVA: 0x00054258 File Offset: 0x00052458
	private void QueueAudio(UAP_AudioQueue.SAudioEntry newEntry, UAP_AudioQueue.EInterrupt interrupts)
	{
		if (interrupts != UAP_AudioQueue.EInterrupt.None)
		{
			if (this.m_ActiveEntry != null && this.m_ActiveEntry.m_IsInterruptible && (this.m_ActiveEntry.m_AudioType & (UAP_AudioQueue.EAudioType)interrupts) > UAP_AudioQueue.EAudioType.None)
			{
				this.StopAudio(false);
				this.InvalidateActiveEntry();
			}
			int count = this.m_AudioQueue.Count;
			Queue<UAP_AudioQueue.SAudioEntry> queue = new Queue<UAP_AudioQueue.SAudioEntry>();
			for (int i = 0; i < count; i++)
			{
				UAP_AudioQueue.SAudioEntry saudioEntry = this.m_AudioQueue.Dequeue();
				if (!saudioEntry.m_IsInterruptible)
				{
					queue.Enqueue(saudioEntry);
				}
				else if ((saudioEntry.m_AudioType & (UAP_AudioQueue.EAudioType)interrupts) == UAP_AudioQueue.EAudioType.None)
				{
					queue.Enqueue(saudioEntry);
				}
			}
			this.m_AudioQueue = queue;
		}
		if (newEntry.m_AudioType == UAP_AudioQueue.EAudioType.None)
		{
			return;
		}
		if (newEntry.m_AudioType != UAP_AudioQueue.EAudioType.Pause && newEntry.m_Audio == null && newEntry.m_TTS_Text.Length == 0)
		{
			return;
		}
		this.m_AudioQueue.Enqueue(newEntry);
	}

	// Token: 0x06000CAC RID: 3244 RVA: 0x0005432A File Offset: 0x0005252A
	private void InvalidateActiveEntry()
	{
		if (this.m_ActiveEntry != null && this.m_ActiveEntry.m_CallbackOnDone != null)
		{
			this.m_ActiveEntry.m_CallbackOnDone();
		}
		this.m_ActiveEntry = null;
	}

	// Token: 0x06000CAD RID: 3245 RVA: 0x00054358 File Offset: 0x00052558
	private void InitializeWindowsTTS()
	{
		if (WindowsTTS.instance == null)
		{
			GameObject gameObject = new GameObject("Windows TTS");
			gameObject.AddComponent<WindowsTTS>();
			gameObject.transform.SetParent(base.transform, false);
		}
	}

	// Token: 0x06000CAE RID: 3246 RVA: 0x00054389 File Offset: 0x00052589
	public void Initialize()
	{
		this.m_SpeechRate = PlayerPrefs.GetInt("Accessibility_Speech_Rate", 50);
		if (this.m_AudioPlayer == null)
		{
			this.m_AudioPlayer = base.GetComponent<AudioSource>();
		}
		this.InitializeCustomTTS();
		UAP_CustomTTS.IsInitialized();
	}

	// Token: 0x06000CAF RID: 3247 RVA: 0x000543C3 File Offset: 0x000525C3
	private void OnDestroy()
	{
	}

	// Token: 0x06000CB0 RID: 3248 RVA: 0x000543C5 File Offset: 0x000525C5
	private void TTS_Speak(string text, bool allowVoiceOver = true)
	{
		if (UAP_CustomTTS.IsInitialized() == UAP_CustomTTS.TTSInitializationState.NotInitialized)
		{
			if (UAP_AccessibilityManager.UseWindowsTTS())
			{
				this.InitializeWindowsTTS();
				WindowsTTS.Speak(text);
				return;
			}
		}
		else
		{
			UAP_CustomTTS.Speak(text, (float)(this.m_SpeechRate + 50) / 100f);
		}
	}

	// Token: 0x06000CB1 RID: 3249 RVA: 0x000543F8 File Offset: 0x000525F8
	private bool TTS_IsSpeaking()
	{
		if (UAP_CustomTTS.IsInitialized() == UAP_CustomTTS.TTSInitializationState.NotInitialized)
		{
			return UAP_AccessibilityManager.UseWindowsTTS() && WindowsTTS.instance != null && WindowsTTS.IsSpeaking();
		}
		return UAP_CustomTTS.IsSpeaking();
	}

	// Token: 0x06000CB2 RID: 3250 RVA: 0x00054424 File Offset: 0x00052624
	private void StopAudio(bool includingAndroid = false)
	{
		if (this.m_AudioPlayer.isPlaying)
		{
			this.m_AudioPlayer.Stop();
			this.m_AudioPlayer.clip = null;
		}
		if (UAP_CustomTTS.IsInitialized() == UAP_CustomTTS.TTSInitializationState.NotInitialized)
		{
			if (UAP_AccessibilityManager.UseWindowsTTS() && WindowsTTS.instance != null)
			{
				WindowsTTS.Stop();
				return;
			}
		}
		else
		{
			UAP_CustomTTS.Stop();
		}
	}

	// Token: 0x06000CB3 RID: 3251 RVA: 0x0005447C File Offset: 0x0005267C
	private void Update()
	{
		this.m_CurrentQueueLength = this.m_AudioQueue.Count;
		this.m_CurrentPauseDuration = this.m_PauseTimer;
		if (this.m_ActiveEntry != null)
		{
			if (this.m_ActiveEntry.m_AudioType == UAP_AudioQueue.EAudioType.Pause)
			{
				this.m_CurrentElement = "Pause";
				this.m_IsSpeaking = false;
				if (!this.IsPlaying())
				{
					this.m_PauseTimer -= Time.unscaledDeltaTime;
					if (this.m_PauseTimer <= 0f)
					{
						this.InvalidateActiveEntry();
					}
				}
			}
			else
			{
				this.m_CurrentElement = "Voice";
				bool flag = this.IsPlaying();
				this.m_IsSpeaking = flag;
				if (!flag)
				{
					this.InvalidateActiveEntry();
				}
			}
		}
		else
		{
			this.m_CurrentElement = "none";
			this.m_IsSpeaking = false;
		}
		if (this.m_TTS_SpeakingTimer > 0f)
		{
			this.m_TTS_SpeakingTimer -= Time.unscaledDeltaTime;
		}
		if (this.m_ActiveEntry == null && this.m_AudioQueue.Count > 0)
		{
			bool flag2 = true;
			if (UAP_CustomTTS.IsInitialized() == UAP_CustomTTS.TTSInitializationState.InProgress && this.m_AudioQueue.Peek().m_Audio == null)
			{
				flag2 = false;
			}
			if (flag2)
			{
				this.m_ActiveEntry = this.m_AudioQueue.Dequeue();
				if (this.m_ActiveEntry.m_AudioType == UAP_AudioQueue.EAudioType.Pause)
				{
					this.m_PauseTimer = this.m_ActiveEntry.m_PauseDuration;
					return;
				}
				if (this.m_ActiveEntry.m_Audio != null)
				{
					this.m_AudioPlayer.clip = this.m_ActiveEntry.m_Audio;
					this.m_AudioPlayer.Play();
					return;
				}
				if (this.m_ActiveEntry.m_TTS_Text.Length > 0)
				{
					this.TTS_Speak(this.m_ActiveEntry.m_TTS_Text, this.m_ActiveEntry.m_AllowVoiceOver);
				}
			}
		}
	}

	// Token: 0x06000CB4 RID: 3252 RVA: 0x00054630 File Offset: 0x00052830
	public bool IsPlaying()
	{
		if (this.m_ActiveEntry == null)
		{
			return false;
		}
		bool flag = false;
		if (this.m_ActiveEntry.m_Audio != null)
		{
			flag = this.m_AudioPlayer.isPlaying;
		}
		else if (this.m_ActiveEntry.m_TTS_Text.Length > 0)
		{
			flag = this.TTS_IsSpeaking();
		}
		return flag;
	}

	// Token: 0x06000CB5 RID: 3253 RVA: 0x00054688 File Offset: 0x00052888
	public bool IsPlayingExceptAppOrSkippable()
	{
		if (this.m_ActiveEntry == null)
		{
			return false;
		}
		bool flag = false;
		if (this.m_ActiveEntry.m_Audio != null)
		{
			flag = this.m_AudioPlayer.isPlaying;
		}
		else if (this.m_ActiveEntry.m_TTS_Text.Length > 0)
		{
			flag = this.TTS_IsSpeaking();
		}
		return (!flag || (this.m_ActiveEntry.m_AudioType != UAP_AudioQueue.EAudioType.Skippable && this.m_ActiveEntry.m_AudioType != UAP_AudioQueue.EAudioType.App)) && flag;
	}

	// Token: 0x06000CB6 RID: 3254 RVA: 0x00054700 File Offset: 0x00052900
	public bool IsCompletelyEmpty()
	{
		return this.m_AudioQueue.Count <= 0 && !this.IsPlaying() && this.m_ActiveEntry == null;
	}

	// Token: 0x06000CB7 RID: 3255 RVA: 0x00054727 File Offset: 0x00052927
	public int GetSpeechRate()
	{
		return this.m_SpeechRate;
	}

	// Token: 0x06000CB8 RID: 3256 RVA: 0x00054730 File Offset: 0x00052930
	public int SetSpeechRate(int speechRate)
	{
		this.m_SpeechRate = speechRate;
		if (this.m_SpeechRate < 1)
		{
			this.m_SpeechRate = 1;
		}
		if (this.m_SpeechRate > 100)
		{
			this.m_SpeechRate = 100;
		}
		PlayerPrefs.SetInt("Accessibility_Speech_Rate", this.m_SpeechRate);
		PlayerPrefs.Save();
		return this.m_SpeechRate;
	}

	// Token: 0x06000CB9 RID: 3257 RVA: 0x00054781 File Offset: 0x00052981
	private void InitializeCustomTTS()
	{
	}

	// Token: 0x040008F0 RID: 2288
	public int m_CurrentQueueLength;

	// Token: 0x040008F1 RID: 2289
	public float m_CurrentPauseDuration;

	// Token: 0x040008F2 RID: 2290
	public string m_CurrentElement = "none";

	// Token: 0x040008F3 RID: 2291
	public bool m_IsSpeaking;

	// Token: 0x040008F4 RID: 2292
	private int m_SpeechRate = 65;

	// Token: 0x040008F5 RID: 2293
	private AudioSource m_AudioPlayer;

	// Token: 0x040008F6 RID: 2294
	private Queue<UAP_AudioQueue.SAudioEntry> m_AudioQueue = new Queue<UAP_AudioQueue.SAudioEntry>();

	// Token: 0x040008F7 RID: 2295
	private UAP_AudioQueue.SAudioEntry m_ActiveEntry;

	// Token: 0x040008F8 RID: 2296
	private float m_PauseTimer = -1f;

	// Token: 0x040008F9 RID: 2297
	private float m_TTS_SpeakingTimer = -1f;

	// Token: 0x020015F2 RID: 5618
	public enum EAudioType
	{
		// Token: 0x0400586A RID: 22634
		None,
		// Token: 0x0400586B RID: 22635
		Pause,
		// Token: 0x0400586C RID: 22636
		Element_Text,
		// Token: 0x0400586D RID: 22637
		Element_Type = 4,
		// Token: 0x0400586E RID: 22638
		Element_Hint = 8,
		// Token: 0x0400586F RID: 22639
		App = 16,
		// Token: 0x04005870 RID: 22640
		Container_Name = 32,
		// Token: 0x04005871 RID: 22641
		Skippable = 64
	}

	// Token: 0x020015F3 RID: 5619
	public enum EInterrupt
	{
		// Token: 0x04005873 RID: 22643
		None,
		// Token: 0x04005874 RID: 22644
		Elements = 79,
		// Token: 0x04005875 RID: 22645
		All = 127
	}

	// Token: 0x020015F4 RID: 5620
	// (Invoke) Token: 0x0600D67D RID: 54909
	public delegate void UAP_GenericCallback();

	// Token: 0x020015F5 RID: 5621
	private class SAudioEntry
	{
		// Token: 0x04005876 RID: 22646
		public string m_TTS_Text = "";

		// Token: 0x04005877 RID: 22647
		public bool m_AllowVoiceOver = true;

		// Token: 0x04005878 RID: 22648
		public AudioClip m_Audio;

		// Token: 0x04005879 RID: 22649
		public UAP_AudioQueue.EAudioType m_AudioType;

		// Token: 0x0400587A RID: 22650
		public bool m_IsInterruptible = true;

		// Token: 0x0400587B RID: 22651
		public float m_PauseDuration;

		// Token: 0x0400587C RID: 22652
		public UAP_AudioQueue.UAP_GenericCallback m_CallbackOnDone;
	}
}
