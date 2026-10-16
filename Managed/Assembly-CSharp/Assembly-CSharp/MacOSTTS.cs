using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200013E RID: 318
public class MacOSTTS : MonoBehaviour
{
	// Token: 0x06000BFD RID: 3069 RVA: 0x0004F2D7 File Offset: 0x0004D4D7
	private void Start()
	{
		if (MacOSTTS.instance == null)
		{
			MacOSTTS.instance = this;
			return;
		}
		Debug.LogError("[Accessibility] Trying to create another MacOS TTS instance, when there already is one.");
		global::UnityEngine.Object.DestroyImmediate(base.gameObject);
	}

	// Token: 0x06000BFE RID: 3070 RVA: 0x0004F302 File Offset: 0x0004D502
	public void Speak(string msg)
	{
		if (msg.Length == 0)
		{
			return;
		}
		this.Stop();
	}

	// Token: 0x06000BFF RID: 3071 RVA: 0x0004F313 File Offset: 0x0004D513
	private IEnumerator SpeakText(string textToSpeak)
	{
		yield break;
	}

	// Token: 0x06000C00 RID: 3072 RVA: 0x0004F31B File Offset: 0x0004D51B
	public void Stop()
	{
	}

	// Token: 0x06000C01 RID: 3073 RVA: 0x0004F31D File Offset: 0x0004D51D
	public bool IsSpeaking()
	{
		return Application.isPlaying && this.m_IsSpeaking;
	}

	// Token: 0x06000C02 RID: 3074 RVA: 0x0004F32E File Offset: 0x0004D52E
	private void OnDestroy()
	{
		this.Stop();
	}

	// Token: 0x04000866 RID: 2150
	public static MacOSTTS instance;

	// Token: 0x04000867 RID: 2151
	private bool m_IsSpeaking;
}
