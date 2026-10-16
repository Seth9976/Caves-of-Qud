using System;
using UnityEngine;

// Token: 0x0200013D RID: 317
public class WebSpeechAPI_TTS : UAP_CustomTTS
{
	// Token: 0x06000BF7 RID: 3063 RVA: 0x0004F2B1 File Offset: 0x0004D4B1
	protected override void Initialize()
	{
	}

	// Token: 0x06000BF8 RID: 3064 RVA: 0x0004F2B3 File Offset: 0x0004D4B3
	protected override UAP_CustomTTS.TTSInitializationState GetInitializationStatus()
	{
		return UAP_CustomTTS.TTSInitializationState.Initialized;
	}

	// Token: 0x06000BF9 RID: 3065 RVA: 0x0004F2B6 File Offset: 0x0004D4B6
	protected override void SpeakText(string textToSay, float speakRate)
	{
	}

	// Token: 0x06000BFA RID: 3066 RVA: 0x0004F2B8 File Offset: 0x0004D4B8
	protected override void StopSpeaking()
	{
		RuntimePlatform platform = Application.platform;
	}

	// Token: 0x06000BFB RID: 3067 RVA: 0x0004F2C3 File Offset: 0x0004D4C3
	protected override bool IsCurrentlySpeaking()
	{
		RuntimePlatform platform = Application.platform;
		return false;
	}
}
