using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000151 RID: 337
public class UAP_BackCloseEventListener : MonoBehaviour
{
	// Token: 0x06000D82 RID: 3458 RVA: 0x000583B6 File Offset: 0x000565B6
	private void OnEnable()
	{
		UAP_AccessibilityManager.RegisterOnBackCallback(new UAP_AccessibilityManager.OnTapEvent(this.OnScrubGesturePerformed));
	}

	// Token: 0x06000D83 RID: 3459 RVA: 0x000583C9 File Offset: 0x000565C9
	private void OnDisable()
	{
		UAP_AccessibilityManager.UnregisterOnBackCallback(new UAP_AccessibilityManager.OnTapEvent(this.OnScrubGesturePerformed));
	}

	// Token: 0x06000D84 RID: 3460 RVA: 0x000583DC File Offset: 0x000565DC
	private void OnScrubGesturePerformed()
	{
		this.m_OnBackEvent.Invoke();
	}

	// Token: 0x04000963 RID: 2403
	public UnityEvent m_OnBackEvent = new UnityEvent();
}
