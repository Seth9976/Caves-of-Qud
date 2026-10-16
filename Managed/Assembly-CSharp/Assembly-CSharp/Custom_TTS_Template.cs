using System;

// Token: 0x0200013A RID: 314
public class Custom_TTS_Template : UAP_CustomTTS
{
	// Token: 0x06000BDA RID: 3034 RVA: 0x0004ED04 File Offset: 0x0004CF04
	protected override void Initialize()
	{
		throw new Exception("The method or operation is not implemented.");
	}

	// Token: 0x06000BDB RID: 3035 RVA: 0x0004ED10 File Offset: 0x0004CF10
	protected override UAP_CustomTTS.TTSInitializationState GetInitializationStatus()
	{
		return UAP_CustomTTS.TTSInitializationState.Initialized;
	}

	// Token: 0x06000BDC RID: 3036 RVA: 0x0004ED13 File Offset: 0x0004CF13
	protected override void SpeakText(string textToSay, float speakRate)
	{
		throw new Exception("The method or operation is not implemented.");
	}

	// Token: 0x06000BDD RID: 3037 RVA: 0x0004ED1F File Offset: 0x0004CF1F
	protected override void StopSpeaking()
	{
		throw new Exception("The method or operation is not implemented.");
	}

	// Token: 0x06000BDE RID: 3038 RVA: 0x0004ED2B File Offset: 0x0004CF2B
	protected override bool IsCurrentlySpeaking()
	{
		throw new Exception("The method or operation is not implemented.");
	}
}
