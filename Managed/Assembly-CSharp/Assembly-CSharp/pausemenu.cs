using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000136 RID: 310
public class pausemenu : MonoBehaviour
{
	// Token: 0x06000BC4 RID: 3012 RVA: 0x0004EB76 File Offset: 0x0004CD76
	private void OnEnable()
	{
		this.EnableMusic(PlayerPrefs.GetInt("Music_Enabled", 1) == 1);
		UAP_AccessibilityManager.RegisterOnPauseToggledCallback(new UAP_AccessibilityManager.OnPauseToggleCallbackFunc(this.OnUserPause));
	}

	// Token: 0x06000BC5 RID: 3013 RVA: 0x0004EB9D File Offset: 0x0004CD9D
	private void OnDisable()
	{
		UAP_AccessibilityManager.UnregisterOnPauseToggledCallback(new UAP_AccessibilityManager.OnPauseToggleCallbackFunc(this.OnUserPause));
	}

	// Token: 0x06000BC6 RID: 3014 RVA: 0x0004EBB0 File Offset: 0x0004CDB0
	public void OnUserPause()
	{
		this.OnResumeButtonPressed();
	}

	// Token: 0x06000BC7 RID: 3015 RVA: 0x0004EBB8 File Offset: 0x0004CDB8
	public void OnResumeButtonPressed()
	{
		gameplay.Instance.ResumeGame();
		global::UnityEngine.Object.DestroyImmediate(base.gameObject);
	}

	// Token: 0x06000BC8 RID: 3016 RVA: 0x0004EBCF File Offset: 0x0004CDCF
	public void OnAbortGameButtonPressed()
	{
		global::UnityEngine.Object.DestroyImmediate(base.gameObject);
		gameplay.Instance.AbortGame();
	}

	// Token: 0x06000BC9 RID: 3017 RVA: 0x0004EBE6 File Offset: 0x0004CDE6
	public void OnSoundToggle()
	{
		this.EnableMusic(PlayerPrefs.GetInt("Music_Enabled", 1) != 1);
	}

	// Token: 0x06000BCA RID: 3018 RVA: 0x0004EC00 File Offset: 0x0004CE00
	private void EnableMusic(bool enable)
	{
		this.m_SoundToggleButton_Access.m_Text = (enable ? "Turn Music Off" : "Turn Music On");
		this.m_SoundToggle.sprite = (enable ? this.m_SoundOn : this.m_SoundOff);
		PlayerPrefs.SetInt("Music_Enabled", enable ? 1 : 0);
		PlayerPrefs.Save();
	}

	// Token: 0x04000856 RID: 2134
	public Image m_SoundToggle;

	// Token: 0x04000857 RID: 2135
	public Sprite m_SoundOn;

	// Token: 0x04000858 RID: 2136
	public Sprite m_SoundOff;

	// Token: 0x04000859 RID: 2137
	public UAP_BaseElement m_SoundToggleButton_Access;
}
