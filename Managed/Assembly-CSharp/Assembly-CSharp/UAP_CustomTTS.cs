using System;
using UnityEngine;

// Token: 0x02000152 RID: 338
public class UAP_CustomTTS : MonoBehaviour
{
	// Token: 0x06000D86 RID: 3462 RVA: 0x000583FC File Offset: 0x000565FC
	protected virtual void Initialize()
	{
	}

	// Token: 0x06000D87 RID: 3463 RVA: 0x000583FE File Offset: 0x000565FE
	protected virtual UAP_CustomTTS.TTSInitializationState GetInitializationStatus()
	{
		return UAP_CustomTTS.TTSInitializationState.NotInitialized;
	}

	// Token: 0x06000D88 RID: 3464 RVA: 0x00058401 File Offset: 0x00056601
	protected virtual void SpeakText(string textToSay, float speakRate)
	{
	}

	// Token: 0x06000D89 RID: 3465 RVA: 0x00058403 File Offset: 0x00056603
	protected virtual void StopSpeaking()
	{
	}

	// Token: 0x06000D8A RID: 3466 RVA: 0x00058405 File Offset: 0x00056605
	protected virtual bool IsCurrentlySpeaking()
	{
		return false;
	}

	// Token: 0x06000D8B RID: 3467 RVA: 0x00058408 File Offset: 0x00056608
	public static void InitializeCustomTTS<T>()
	{
		if (UAP_CustomTTS.Instance != null)
		{
			return;
		}
		GameObject gameObject = new GameObject("Custom TTS");
		gameObject.AddComponent(typeof(T));
		UAP_CustomTTS.Instance = gameObject.GetComponent<UAP_CustomTTS>();
		if (UAP_CustomTTS.Instance == null)
		{
			Debug.LogError("[TTS] Error creating custom TTS system. " + typeof(T).ToString() + " is not derived from UAP_CustomTTS");
			return;
		}
		Debug.Log("[TTS] Initializing Custom TTS");
		UAP_CustomTTS.Instance.Initialize();
		global::UnityEngine.Object.DontDestroyOnLoad(gameObject);
	}

	// Token: 0x06000D8C RID: 3468 RVA: 0x00058495 File Offset: 0x00056695
	public static void Speak(string textToSay, float speakRate)
	{
		if (UAP_CustomTTS.Instance != null)
		{
			UAP_CustomTTS.Instance.SpeakText(textToSay, speakRate);
		}
	}

	// Token: 0x06000D8D RID: 3469 RVA: 0x000584B0 File Offset: 0x000566B0
	public static void Stop()
	{
		if (UAP_CustomTTS.Instance != null)
		{
			UAP_CustomTTS.Instance.StopSpeaking();
		}
	}

	// Token: 0x06000D8E RID: 3470 RVA: 0x000584C9 File Offset: 0x000566C9
	public static bool IsSpeaking()
	{
		return UAP_CustomTTS.Instance != null && UAP_CustomTTS.Instance.IsCurrentlySpeaking();
	}

	// Token: 0x06000D8F RID: 3471 RVA: 0x000584E4 File Offset: 0x000566E4
	public static UAP_CustomTTS.TTSInitializationState IsInitialized()
	{
		if (UAP_CustomTTS.Instance == null)
		{
			return UAP_CustomTTS.TTSInitializationState.NotInitialized;
		}
		return UAP_CustomTTS.Instance.GetInitializationStatus();
	}

	// Token: 0x06000D90 RID: 3472 RVA: 0x000584FF File Offset: 0x000566FF
	private void OnDestroy()
	{
		if (UAP_CustomTTS.Instance == this)
		{
			UAP_CustomTTS.Instance = null;
		}
	}

	// Token: 0x04000964 RID: 2404
	protected static UAP_CustomTTS Instance;

	// Token: 0x020015FE RID: 5630
	public enum TTSInitializationState
	{
		// Token: 0x04005899 RID: 22681
		NotInitialized,
		// Token: 0x0400589A RID: 22682
		InProgress,
		// Token: 0x0400589B RID: 22683
		Initialized
	}
}
