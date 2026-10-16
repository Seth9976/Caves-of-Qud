using System;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

// Token: 0x0200013B RID: 315
[RequireComponent(typeof(AudioSource))]
public class Google_TTS : UAP_CustomTTS
{
	// Token: 0x06000BE0 RID: 3040 RVA: 0x0004ED3F File Offset: 0x0004CF3F
	protected override void Initialize()
	{
		this.m_AudioPlayer = base.GetComponent<AudioSource>();
	}

	// Token: 0x06000BE1 RID: 3041 RVA: 0x0004ED4D File Offset: 0x0004CF4D
	protected override UAP_CustomTTS.TTSInitializationState GetInitializationStatus()
	{
		if (this.m_AudioPlayer == null)
		{
			return UAP_CustomTTS.TTSInitializationState.NotInitialized;
		}
		return UAP_CustomTTS.TTSInitializationState.Initialized;
	}

	// Token: 0x06000BE2 RID: 3042 RVA: 0x0004ED60 File Offset: 0x0004CF60
	protected override void SpeakText(string textToSay, float speakRate)
	{
		if (this.m_AudioPlayer == null)
		{
			return;
		}
		this.m_IsWaitingForSynth = true;
		string googleTTSAPIKey = UAP_AccessibilityManager.GoogleTTSAPIKey;
		float num = speakRate;
		string text = "en-US";
		string text2 = string.Concat(new string[]
		{
			"{\"input\":{\"text\":\"",
			textToSay,
			"\"},\"voice\":{\"languageCode\":\"",
			text,
			"\",\"name\":\"en-US-Wavenet-D\",\"ssmlGender\":1},\"audioConfig\":{\"audioEncoding\":1,\"speakingRate\":",
			num.ToString("0.0"),
			",\"pitch\":1.0,\"volumeGainDb\":0.0,\"sampleRateHertz\":24000.0}}"
		});
		byte[] bytes = Encoding.UTF8.GetBytes(text2);
		string text3 = "https://texttospeech.googleapis.com/v1/text:synthesize?key=" + googleTTSAPIKey;
		if (this.m_CurrentRequest != null)
		{
			this.m_CurrentRequest.Abort();
			this.m_CurrentRequest.Dispose();
		}
		this.m_CurrentRequest = new UnityWebRequest(text3, "POST");
		this.m_CurrentRequest.uploadHandler = new UploadHandlerRaw(bytes);
		this.m_CurrentRequest.SetRequestHeader("Content-Type", "application/json");
		this.m_CurrentRequest.downloadHandler = new DownloadHandlerBuffer();
		this.m_CurrentRequest.SendWebRequest();
	}

	// Token: 0x06000BE3 RID: 3043 RVA: 0x0004EE64 File Offset: 0x0004D064
	private void Update()
	{
		if (!this.m_IsWaitingForSynth)
		{
			return;
		}
		if (this.m_CurrentRequest.isDone)
		{
			this.m_IsWaitingForSynth = false;
			if (this.m_CurrentRequest.responseCode == 403L)
			{
				Debug.LogWarning("[Google TTS] Received response FORBIDDEN from Google. Please check whether your API key restrictions might be blocking this call. You might have set this to only be allowed from your website. If so, you might want to create an unrestricted Editor-only API key which you're using for development.");
			}
			if (!string.IsNullOrEmpty(this.m_CurrentRequest.error))
			{
				Debug.LogError("[Google TTS] Error Code: " + this.m_CurrentRequest.responseCode.ToString() + " - " + this.m_CurrentRequest.error);
				return;
			}
			if (this.m_CurrentRequest.downloadHandler.text.Contains("error"))
			{
				Debug.LogError("[Google TTS] Error Code: " + this.m_CurrentRequest.responseCode.ToString() + " - " + this.m_CurrentRequest.downloadHandler.text);
				return;
			}
			Google_TTS.GoogleTTSSynthesizeResponse googleTTSSynthesizeResponse = (Google_TTS.GoogleTTSSynthesizeResponse)JsonUtility.FromJson(this.m_CurrentRequest.downloadHandler.text, typeof(Google_TTS.GoogleTTSSynthesizeResponse));
			if (googleTTSSynthesizeResponse == null)
			{
				Debug.LogError("[Google TTS] Error - no audio received: " + this.m_CurrentRequest.downloadHandler.text);
				return;
			}
			WAV wav = new WAV(Convert.FromBase64String(googleTTSSynthesizeResponse.audioContent));
			AudioClip audioClip = AudioClip.Create("testSound", wav.SampleCount, wav.ChannelCount, wav.Frequency, false);
			audioClip.SetData(wav.LeftChannel, 0);
			this.m_AudioPlayer.clip = audioClip;
			this.m_AudioPlayer.Play();
			this.m_CurrentRequest.Dispose();
			this.m_CurrentRequest = null;
		}
	}

	// Token: 0x06000BE4 RID: 3044 RVA: 0x0004EFF8 File Offset: 0x0004D1F8
	protected override void StopSpeaking()
	{
		if (this.m_AudioPlayer == null)
		{
			return;
		}
		if (this.m_IsWaitingForSynth)
		{
			if (this.m_CurrentRequest != null)
			{
				this.m_CurrentRequest.Abort();
				this.m_CurrentRequest.Dispose();
			}
			this.m_CurrentRequest = null;
			this.m_IsWaitingForSynth = false;
		}
		this.m_AudioPlayer.Stop();
	}

	// Token: 0x06000BE5 RID: 3045 RVA: 0x0004F053 File Offset: 0x0004D253
	protected override bool IsCurrentlySpeaking()
	{
		return !(this.m_AudioPlayer == null) && (this.m_IsWaitingForSynth || this.m_AudioPlayer.isPlaying);
	}

	// Token: 0x0400085E RID: 2142
	private AudioSource m_AudioPlayer;

	// Token: 0x0400085F RID: 2143
	private UnityWebRequest m_CurrentRequest;

	// Token: 0x04000860 RID: 2144
	private bool m_IsWaitingForSynth;

	// Token: 0x020015EB RID: 5611
	[Serializable]
	public class GoogleTTSSynthesizeResponse
	{
		// Token: 0x04005861 RID: 22625
		public string audioContent;
	}
}
