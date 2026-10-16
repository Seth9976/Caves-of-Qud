using System;
using UnityEngine;

// Token: 0x02000139 RID: 313
public class AndroidTTS : MonoBehaviour
{
	// Token: 0x06000BD2 RID: 3026 RVA: 0x0004ECD8 File Offset: 0x0004CED8
	public static void Initialize()
	{
	}

	// Token: 0x06000BD3 RID: 3027 RVA: 0x0004ECDA File Offset: 0x0004CEDA
	public static void Shutdown()
	{
	}

	// Token: 0x06000BD4 RID: 3028 RVA: 0x0004ECDC File Offset: 0x0004CEDC
	public static string GetTTSStatus()
	{
		return "Only supported on Android";
	}

	// Token: 0x06000BD5 RID: 3029 RVA: 0x0004ECE3 File Offset: 0x0004CEE3
	public static bool IsSpeaking()
	{
		return false;
	}

	// Token: 0x06000BD6 RID: 3030 RVA: 0x0004ECE6 File Offset: 0x0004CEE6
	public static void Speak(string text)
	{
	}

	// Token: 0x06000BD7 RID: 3031 RVA: 0x0004ECE8 File Offset: 0x0004CEE8
	public static void StopSpeaking()
	{
	}

	// Token: 0x06000BD8 RID: 3032 RVA: 0x0004ECEA File Offset: 0x0004CEEA
	public static void SetSpeechRate(int speechRate)
	{
		if (speechRate < 1)
		{
			speechRate = 1;
		}
		if (speechRate > 100)
		{
			speechRate = 100;
		}
	}
}
