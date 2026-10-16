using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200014C RID: 332
[AddComponentMenu("Accessibility/UI/Accessible Plugin Toggle")]
public class AccessiblePluginToggle : AccessibleToggle
{
	// Token: 0x06000D32 RID: 3378 RVA: 0x000569D8 File Offset: 0x00054BD8
	private void OnEnable()
	{
		UAP_AccessibilityManager.RegisterAccessibilityModeChangeCallback(new UAP_AccessibilityManager.OnAccessibilityModeChanged(this.AccessibilitiyPlugin_StateChanged));
		this.UpdateToggleState();
	}

	// Token: 0x06000D33 RID: 3379 RVA: 0x000569F1 File Offset: 0x00054BF1
	private void OnDisable()
	{
		UAP_AccessibilityManager.UnregisterAccessibilityModeChangeCallback(new UAP_AccessibilityManager.OnAccessibilityModeChanged(this.AccessibilitiyPlugin_StateChanged));
	}

	// Token: 0x06000D34 RID: 3380 RVA: 0x00056A04 File Offset: 0x00054C04
	private void Start()
	{
		Toggle toggle = base.GetToggle();
		if (toggle != null)
		{
			toggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnToggleStateChanged));
		}
	}

	// Token: 0x06000D35 RID: 3381 RVA: 0x00056A38 File Offset: 0x00054C38
	public void OnToggleStateChanged(bool newState)
	{
		if (!this.m_HandleActivation)
		{
			return;
		}
		if (newState == UAP_AccessibilityManager.IsEnabled())
		{
			return;
		}
		UAP_AccessibilityManager.EnableAccessibility(newState, false);
	}

	// Token: 0x06000D36 RID: 3382 RVA: 0x00056A53 File Offset: 0x00054C53
	public void AccessibilitiyPlugin_StateChanged(bool newEnabledState)
	{
		this.UpdateToggleState();
	}

	// Token: 0x06000D37 RID: 3383 RVA: 0x00056A5B File Offset: 0x00054C5B
	private void UpdateToggleState()
	{
		if (UAP_AccessibilityManager.IsEnabled() != base.IsChecked())
		{
			base.SetToggleState(UAP_AccessibilityManager.IsEnabled());
		}
	}

	// Token: 0x04000942 RID: 2370
	public bool m_HandleActivation = true;
}
