using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000147 RID: 327
[AddComponentMenu("Accessibility/UI/Accessible Button")]
public class AccessibleButton : UAP_BaseElement
{
	// Token: 0x06000D0E RID: 3342 RVA: 0x00055F0E File Offset: 0x0005410E
	private AccessibleButton()
	{
		this.m_Type = AccessibleUIGroupRoot.EUIElement.EButton;
	}

	// Token: 0x06000D0F RID: 3343 RVA: 0x00055F20 File Offset: 0x00054120
	protected override void OnInteract()
	{
		Button button = this.GetButton();
		if (button != null)
		{
			PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
			button.OnPointerClick(pointerEventData);
			return;
		}
	}

	// Token: 0x06000D10 RID: 3344 RVA: 0x00055F50 File Offset: 0x00054150
	public override bool IsElementActive()
	{
		return base.IsElementActive() && (!(this.m_ReferenceElement != null) || this.m_ReferenceElement.gameObject.activeInHierarchy) && (UAP_AccessibilityManager.GetSpeakDisabledInteractables() || this.IsInteractable());
	}

	// Token: 0x06000D11 RID: 3345 RVA: 0x00055F90 File Offset: 0x00054190
	private Button GetButton()
	{
		Button button = null;
		if (this.m_ReferenceElement != null && this.m_ReferenceElement.activeInHierarchy)
		{
			button = this.m_ReferenceElement.GetComponent<Button>();
		}
		if (button == null && base.gameObject.activeInHierarchy)
		{
			button = base.gameObject.GetComponent<Button>();
		}
		return button;
	}

	// Token: 0x06000D12 RID: 3346 RVA: 0x00055FEC File Offset: 0x000541EC
	public override bool IsInteractable()
	{
		Button button = this.GetButton();
		return button != null && button.enabled && button.IsInteractable();
	}

	// Token: 0x06000D13 RID: 3347 RVA: 0x00056020 File Offset: 0x00054220
	public override bool AutoFillTextLabel()
	{
		if (base.AutoFillTextLabel())
		{
			return true;
		}
		bool flag = false;
		Transform transform = base.gameObject.transform.Find("Text");
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
			Component textMeshProLabelInChildren = base.GetTextMeshProLabelInChildren();
			if (textMeshProLabelInChildren != null)
			{
				this.m_Text = base.GetTextFromTextMeshPro(textMeshProLabelInChildren);
				flag = true;
			}
		}
		if (!flag)
		{
			this.m_Text = base.gameObject.name;
		}
		return flag;
	}

	// Token: 0x06000D14 RID: 3348 RVA: 0x000560DC File Offset: 0x000542DC
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
					flag = true;
				}
			}
			if (!flag)
			{
				Component textMeshProLabelInChildren = base.GetTextMeshProLabelInChildren();
				if (textMeshProLabelInChildren != null)
				{
					this.m_NameLabel = textMeshProLabelInChildren.gameObject;
					return;
				}
			}
		}
		else
		{
			this.m_NameLabel = null;
		}
	}

	// Token: 0x06000D15 RID: 3349 RVA: 0x0005618C File Offset: 0x0005438C
	protected override void OnHoverHighlight(bool enable)
	{
		Button button = this.GetButton();
		if (button != null)
		{
			PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
			if (enable)
			{
				button.OnPointerEnter(pointerEventData);
				return;
			}
			button.OnPointerExit(pointerEventData);
		}
	}
}
