using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200014F RID: 335
[AddComponentMenu("Accessibility/UI/Accessible Toggle")]
public class AccessibleToggle : UAP_BaseElement
{
	// Token: 0x06000D58 RID: 3416 RVA: 0x000573D8 File Offset: 0x000555D8
	public AccessibleToggle()
	{
		this.m_Type = AccessibleUIGroupRoot.EUIElement.EToggle;
	}

	// Token: 0x06000D59 RID: 3417 RVA: 0x000573FD File Offset: 0x000555FD
	public override bool IsElementActive()
	{
		return base.IsElementActive() && (!(this.m_ReferenceElement != null) || this.m_ReferenceElement.gameObject.activeInHierarchy) && (UAP_AccessibilityManager.GetSpeakDisabledInteractables() || this.IsInteractable());
	}

	// Token: 0x06000D5A RID: 3418 RVA: 0x00057440 File Offset: 0x00055640
	public override bool IsInteractable()
	{
		Toggle toggle = this.GetToggle();
		return toggle != null && toggle.enabled && toggle.IsInteractable();
	}

	// Token: 0x06000D5B RID: 3419 RVA: 0x00057474 File Offset: 0x00055674
	protected override void OnInteract()
	{
		Toggle toggle = this.GetToggle();
		if (toggle != null)
		{
			PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
			toggle.OnPointerClick(pointerEventData);
		}
	}

	// Token: 0x06000D5C RID: 3420 RVA: 0x000574A4 File Offset: 0x000556A4
	protected Toggle GetToggle()
	{
		Toggle toggle = null;
		if (this.m_ReferenceElement != null)
		{
			toggle = this.m_ReferenceElement.GetComponent<Toggle>();
		}
		if (toggle == null)
		{
			toggle = base.GetComponent<Toggle>();
		}
		return toggle;
	}

	// Token: 0x06000D5D RID: 3421 RVA: 0x000574E0 File Offset: 0x000556E0
	public override string GetCurrentValueAsText()
	{
		if (this.IsChecked())
		{
			if (!this.m_UseCustomOnOff)
			{
				return UAP_AccessibilityManager.Localize_Internal("Checkbox_Checked");
			}
			if (this.m_CustomHintsAreLocalizationKeys)
			{
				return UAP_AccessibilityManager.Localize(this.m_CustomOn);
			}
			return this.m_CustomOn;
		}
		else
		{
			if (!this.m_UseCustomOnOff)
			{
				return UAP_AccessibilityManager.Localize_Internal("Checkbox_NotChecked");
			}
			if (this.m_CustomHintsAreLocalizationKeys)
			{
				return UAP_AccessibilityManager.Localize(this.m_CustomOff);
			}
			return this.m_CustomOff;
		}
	}

	// Token: 0x06000D5E RID: 3422 RVA: 0x00057550 File Offset: 0x00055750
	public bool IsChecked()
	{
		Toggle toggle = this.GetToggle();
		return toggle != null && toggle.isOn;
	}

	// Token: 0x06000D5F RID: 3423 RVA: 0x00057578 File Offset: 0x00055778
	public void SetToggleState(bool toggleState)
	{
		if (toggleState == this.IsChecked())
		{
			return;
		}
		Toggle toggle = this.GetToggle();
		if (toggle != null)
		{
			toggle.isOn = toggleState;
		}
	}

	// Token: 0x06000D60 RID: 3424 RVA: 0x000575A8 File Offset: 0x000557A8
	public override AudioClip GetCurrentValueAsAudio()
	{
		Toggle toggle = this.GetToggle();
		if (toggle == null)
		{
			return null;
		}
		if (!this.m_UseCustomOnOff)
		{
			return null;
		}
		if (toggle.isOn)
		{
			return this.m_CustomOnAudio;
		}
		return this.m_CustomOffAudio;
	}

	// Token: 0x06000D61 RID: 3425 RVA: 0x000575E8 File Offset: 0x000557E8
	public override bool AutoFillTextLabel()
	{
		if (base.AutoFillTextLabel())
		{
			return true;
		}
		bool flag = false;
		Transform transform = base.gameObject.transform.Find("Label");
		if (transform != null)
		{
			Text component = transform.gameObject.GetComponent<Text>();
			if (component != null)
			{
				this.m_Text = component.text;
				flag = true;
			}
		}
		if (!flag)
		{
			Text componentInChildren = base.gameObject.GetComponentInChildren<Text>();
			if (componentInChildren != null)
			{
				this.m_Text = componentInChildren.text;
				flag = true;
			}
		}
		if (!flag)
		{
			this.m_Text = base.gameObject.name;
		}
		return flag;
	}

	// Token: 0x06000D62 RID: 3426 RVA: 0x00057680 File Offset: 0x00055880
	protected override void OnHoverHighlight(bool enable)
	{
		Toggle toggle = this.GetToggle();
		if (toggle != null)
		{
			PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
			if (enable)
			{
				toggle.OnPointerEnter(pointerEventData);
				return;
			}
			toggle.OnPointerExit(pointerEventData);
		}
	}

	// Token: 0x06000D63 RID: 3427 RVA: 0x000576BC File Offset: 0x000558BC
	protected override void AutoInitialize()
	{
		if (this.m_TryToReadLabel)
		{
			bool flag = false;
			Transform transform = base.gameObject.transform.Find("Text");
			if (transform != null)
			{
				Text component = transform.gameObject.GetComponent<Text>();
				if (component != null)
				{
					this.m_NameLabel = component.gameObject;
					flag = true;
				}
			}
			if (!flag)
			{
				Text componentInChildren = base.gameObject.GetComponentInChildren<Text>();
				if (componentInChildren != null)
				{
					this.m_NameLabel = componentInChildren.gameObject;
					return;
				}
			}
		}
		else
		{
			this.m_NameLabel = null;
		}
	}

	// Token: 0x04000949 RID: 2377
	public bool m_UseCustomOnOff;

	// Token: 0x0400094A RID: 2378
	public string m_CustomOn = "Checked";

	// Token: 0x0400094B RID: 2379
	public string m_CustomOff = "Not Checked";

	// Token: 0x0400094C RID: 2380
	public bool m_CustomHintsAreLocalizationKeys;

	// Token: 0x0400094D RID: 2381
	public AudioClip m_CustomOnAudio;

	// Token: 0x0400094E RID: 2382
	public AudioClip m_CustomOffAudio;
}
