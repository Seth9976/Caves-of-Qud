using System;
using UnityEngine;

// Token: 0x02000153 RID: 339
[AddComponentMenu("Accessibility/Helper/GameObject Enabler")]
public class UAP_GameObjectEnabler : MonoBehaviour
{
	// Token: 0x06000D92 RID: 3474 RVA: 0x0005851C File Offset: 0x0005671C
	private void Awake()
	{
		this.SetActiveState();
	}

	// Token: 0x06000D93 RID: 3475 RVA: 0x00058524 File Offset: 0x00056724
	private void Start()
	{
		this.SetActiveState();
	}

	// Token: 0x06000D94 RID: 3476 RVA: 0x0005852C File Offset: 0x0005672C
	private void OnEnable()
	{
		UAP_AccessibilityManager.RegisterAccessibilityModeChangeCallback(new UAP_AccessibilityManager.OnAccessibilityModeChanged(this.Accessibility_StateChange));
		this.SetActiveState();
	}

	// Token: 0x06000D95 RID: 3477 RVA: 0x00058548 File Offset: 0x00056748
	private void SetActiveState()
	{
		if (base.gameObject.activeInHierarchy && base.enabled)
		{
			foreach (GameObject gameObject in this.m_ObjectsToEnable)
			{
				if (gameObject != null)
				{
					gameObject.SetActive(UAP_AccessibilityManager.IsEnabled());
				}
			}
			foreach (GameObject gameObject2 in this.m_ObjectsToDisable)
			{
				if (gameObject2 != null)
				{
					gameObject2.SetActive(!UAP_AccessibilityManager.IsEnabled());
				}
			}
		}
	}

	// Token: 0x06000D96 RID: 3478 RVA: 0x000585C7 File Offset: 0x000567C7
	private void OnDisable()
	{
		UAP_AccessibilityManager.UnregisterAccessibilityModeChangeCallback(new UAP_AccessibilityManager.OnAccessibilityModeChanged(this.Accessibility_StateChange));
	}

	// Token: 0x06000D97 RID: 3479 RVA: 0x000585DA File Offset: 0x000567DA
	public void Accessibility_StateChange(bool newState)
	{
		this.SetActiveState();
	}

	// Token: 0x04000965 RID: 2405
	public GameObject[] m_ObjectsToEnable;

	// Token: 0x04000966 RID: 2406
	public GameObject[] m_ObjectsToDisable;
}
