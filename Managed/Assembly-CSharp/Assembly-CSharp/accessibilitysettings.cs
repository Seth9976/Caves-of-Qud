using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000130 RID: 304
public class accessibilitysettings : MonoBehaviour
{
	// Token: 0x06000B7B RID: 2939 RVA: 0x0004CAA7 File Offset: 0x0004ACA7
	public void OnEnable()
	{
		this.m_EnableAccessibility.isOn = UAP_AccessibilityManager.IsEnabled();
		this.m_SpeechRateSlider.value = (float)UAP_AccessibilityManager.GetSpeechRate();
	}

	// Token: 0x06000B7C RID: 2940 RVA: 0x0004CACA File Offset: 0x0004ACCA
	public void OnAccessibilityEnabledToggleChanged(bool newValue)
	{
		if (UAP_AccessibilityManager.IsEnabled() == newValue)
		{
			return;
		}
		if (newValue)
		{
			this.m_AccessibilityConfirmation.SetActive(true);
			return;
		}
		UAP_AccessibilityManager.EnableAccessibility(false, true);
	}

	// Token: 0x06000B7D RID: 2941 RVA: 0x0004CAEC File Offset: 0x0004ACEC
	public void OnSpeechRateSliderChanged()
	{
		UAP_AccessibilityManager.SetSpeechRate(Mathf.RoundToInt(this.m_SpeechRateSlider.value));
	}

	// Token: 0x06000B7E RID: 2942 RVA: 0x0004CB04 File Offset: 0x0004AD04
	public void OnEnableCancel()
	{
		this.m_EnableAccessibility.isOn = false;
		this.m_AccessibilityConfirmation.SetActive(false);
	}

	// Token: 0x06000B7F RID: 2943 RVA: 0x0004CB1E File Offset: 0x0004AD1E
	public void OnEnableConfirm()
	{
		UAP_AccessibilityManager.EnableAccessibility(true, true);
		this.m_AccessibilityConfirmation.SetActive(false);
	}

	// Token: 0x06000B80 RID: 2944 RVA: 0x0004CB33 File Offset: 0x0004AD33
	public void OnCloseSettings()
	{
		global::UnityEngine.Object.DestroyImmediate(base.gameObject);
	}

	// Token: 0x04000807 RID: 2055
	public Toggle m_EnableAccessibility;

	// Token: 0x04000808 RID: 2056
	public Slider m_SpeechRateSlider;

	// Token: 0x04000809 RID: 2057
	public GameObject m_AccessibilityConfirmation;
}
