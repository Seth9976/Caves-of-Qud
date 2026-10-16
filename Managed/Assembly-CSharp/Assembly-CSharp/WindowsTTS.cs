using System;
using System.Runtime.InteropServices;
using UnityEngine;

// Token: 0x02000141 RID: 321
public class WindowsTTS : MonoBehaviour
{
	// Token: 0x06000C06 RID: 3078
	[DllImport("WindowsTTS")]
	public static extern void Initialize();

	// Token: 0x06000C07 RID: 3079
	[DllImport("WindowsTTS")]
	public static extern void DestroySpeech();

	// Token: 0x06000C08 RID: 3080
	[DllImport("WindowsTTS")]
	public static extern void StopSpeech();

	// Token: 0x06000C09 RID: 3081
	[DllImport("WindowsTTS")]
	public static extern void AddToSpeechQueue(string s);

	// Token: 0x06000C0A RID: 3082
	[DllImport("WindowsTTS")]
	public static extern bool IsVoiceSpeaking();

	// Token: 0x06000C0B RID: 3083
	[DllImport("nvdaControllerClient")]
	internal static extern int nvdaController_testIfRunning();

	// Token: 0x06000C0C RID: 3084
	[DllImport("nvdaControllerClient", CharSet = CharSet.Auto)]
	internal static extern int nvdaController_speakText(string text);

	// Token: 0x06000C0D RID: 3085
	[DllImport("nvdaControllerClient")]
	internal static extern int nvdaController_cancelSpeech();

	// Token: 0x06000C0E RID: 3086 RVA: 0x0004F34E File Offset: 0x0004D54E
	private void Awake()
	{
		WindowsTTS.m_UseNVDA = WindowsTTS.nvdaController_testIfRunning() == 0;
	}

	// Token: 0x06000C0F RID: 3087 RVA: 0x0004F35D File Offset: 0x0004D55D
	public static bool IsScreenReaderDetected()
	{
		WindowsTTS.m_UseNVDA = WindowsTTS.nvdaController_testIfRunning() == 0;
		return WindowsTTS.m_UseNVDA;
	}

	// Token: 0x06000C10 RID: 3088 RVA: 0x0004F371 File Offset: 0x0004D571
	private void Start()
	{
		if (WindowsTTS.instance == null)
		{
			WindowsTTS.instance = this;
			WindowsTTS.Initialize();
			return;
		}
		Debug.LogError("[Accessibility] Trying to create another Windows TTS instance, when there already is one.");
		global::UnityEngine.Object.DestroyImmediate(base.gameObject);
	}

	// Token: 0x06000C11 RID: 3089 RVA: 0x0004F3A1 File Offset: 0x0004D5A1
	public static void Speak(string msg)
	{
		if (WindowsTTS.m_UseNVDA)
		{
			WindowsTTS.nvdaController_speakText(msg);
			WindowsTTS.m_NVDAIsSpeakingTimer += (float)msg.Length / 16f;
			return;
		}
		WindowsTTS.AddToSpeechQueue(msg);
	}

	// Token: 0x06000C12 RID: 3090 RVA: 0x0004F3D0 File Offset: 0x0004D5D0
	public static void Stop()
	{
		if (WindowsTTS.m_UseNVDA)
		{
			WindowsTTS.nvdaController_cancelSpeech();
			WindowsTTS.m_NVDAIsSpeakingTimer = 0f;
			return;
		}
		WindowsTTS.StopSpeech();
	}

	// Token: 0x06000C13 RID: 3091 RVA: 0x0004F3EF File Offset: 0x0004D5EF
	public static bool IsSpeaking()
	{
		if (!Application.isPlaying)
		{
			return false;
		}
		if (WindowsTTS.m_UseNVDA)
		{
			return WindowsTTS.m_NVDAIsSpeakingTimer > 0f;
		}
		return WindowsTTS.IsVoiceSpeaking();
	}

	// Token: 0x06000C14 RID: 3092 RVA: 0x0004F413 File Offset: 0x0004D613
	private void Update()
	{
		if (WindowsTTS.m_NVDAIsSpeakingTimer > 0f)
		{
			WindowsTTS.m_NVDAIsSpeakingTimer -= Time.unscaledDeltaTime;
		}
	}

	// Token: 0x06000C15 RID: 3093 RVA: 0x0004F431 File Offset: 0x0004D631
	private void OnDestroy()
	{
		if (WindowsTTS.instance == this)
		{
			WindowsTTS.DestroySpeech();
			WindowsTTS.instance = null;
		}
	}

	// Token: 0x04000869 RID: 2153
	public static WindowsTTS instance = null;

	// Token: 0x0400086A RID: 2154
	private static bool m_UseNVDA = false;

	// Token: 0x0400086B RID: 2155
	private static float m_NVDAIsSpeakingTimer = -1f;
}
