using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000149 RID: 329
[AddComponentMenu("Accessibility/UI/Accessible Dropdown")]
public class AccessibleDropdown : UAP_BaseElement
{
	// Token: 0x06000D19 RID: 3353 RVA: 0x00056218 File Offset: 0x00054418
	private AccessibleDropdown()
	{
		this.m_Type = AccessibleUIGroupRoot.EUIElement.EDropDown;
	}

	// Token: 0x06000D1A RID: 3354 RVA: 0x00056239 File Offset: 0x00054439
	private void Awake()
	{
	}

	// Token: 0x06000D1B RID: 3355 RVA: 0x0005623C File Offset: 0x0005443C
	private Dropdown GetDropdown()
	{
		Dropdown dropdown = null;
		if (this.m_ReferenceElement != null)
		{
			dropdown = this.m_ReferenceElement.GetComponent<Dropdown>();
		}
		if (dropdown == null)
		{
			dropdown = base.GetComponent<Dropdown>();
		}
		return dropdown;
	}

	// Token: 0x06000D1C RID: 3356 RVA: 0x00056278 File Offset: 0x00054478
	private Component GetTMPDropDown()
	{
		Component component = null;
		if (this.m_ReferenceElement != null)
		{
			component = this.m_ReferenceElement.GetComponent("TMP_Dropdown");
		}
		if (component == null)
		{
			component = base.GetComponent("TMP_Dropdown");
		}
		return component;
	}

	// Token: 0x06000D1D RID: 3357 RVA: 0x000562BC File Offset: 0x000544BC
	public override bool IsInteractable()
	{
		return this.IsElementActive();
	}

	// Token: 0x06000D1E RID: 3358 RVA: 0x000562C4 File Offset: 0x000544C4
	public override bool IsElementActive()
	{
		return base.IsElementActive() && (!(this.m_ReferenceElement != null) || this.m_ReferenceElement.gameObject.activeInHierarchy) && (UAP_AccessibilityManager.GetSpeakDisabledInteractables() || this.IsInteractable());
	}

	// Token: 0x06000D1F RID: 3359 RVA: 0x00056304 File Offset: 0x00054504
	public override string GetCurrentValueAsText()
	{
		Dropdown dropdown = this.GetDropdown();
		if (dropdown != null)
		{
			return dropdown.captionText.text;
		}
		Component tmpdropDown = this.GetTMPDropDown();
		if (tmpdropDown != null)
		{
			PropertyInfo property = tmpdropDown.GetType().GetProperty("captionText");
			if (property != null)
			{
				Component component = property.GetValue(tmpdropDown, null) as Component;
				return base.GetTextFromTextMeshPro(component);
			}
		}
		return "";
	}

	// Token: 0x06000D20 RID: 3360 RVA: 0x00056374 File Offset: 0x00054574
	public override AudioClip GetCurrentValueAsAudio()
	{
		Dropdown dropdown = this.GetDropdown();
		if (dropdown != null && this.m_ValuesAsAudio.Count > dropdown.value)
		{
			return this.m_ValuesAsAudio[dropdown.value];
		}
		return null;
	}

	// Token: 0x06000D21 RID: 3361 RVA: 0x000563B8 File Offset: 0x000545B8
	protected override void OnInteract()
	{
		Dropdown dropdown = this.GetDropdown();
		if (dropdown != null)
		{
			this.prevSelectedIndex = dropdown.value;
			dropdown.Show();
		}
		Component tmpdropDown = this.GetTMPDropDown();
		if (tmpdropDown != null)
		{
			IPointerClickHandler component = tmpdropDown.gameObject.GetComponent<IPointerClickHandler>();
			if (component != null)
			{
				PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
				component.OnPointerClick(pointerEventData);
			}
		}
	}

	// Token: 0x06000D22 RID: 3362 RVA: 0x00056418 File Offset: 0x00054618
	protected override void OnInteractEnd()
	{
		this.prevSelectedIndex = -1;
		Dropdown dropdown = this.GetDropdown();
		if (dropdown != null)
		{
			dropdown.Hide();
		}
		Component tmpdropDown = this.GetTMPDropDown();
		if (tmpdropDown != null)
		{
			ICancelHandler component = tmpdropDown.gameObject.GetComponent<ICancelHandler>();
			if (component != null)
			{
				BaseEventData baseEventData = new BaseEventData(EventSystem.current);
				component.OnCancel(baseEventData);
			}
		}
	}

	// Token: 0x06000D23 RID: 3363 RVA: 0x00056474 File Offset: 0x00054674
	protected override void OnInteractAbort()
	{
		if (this.prevSelectedIndex != -1)
		{
			Dropdown dropdown = this.GetDropdown();
			if (dropdown != null)
			{
				dropdown.value = this.prevSelectedIndex;
			}
			Component tmpdropDown = this.GetTMPDropDown();
			if (tmpdropDown != null)
			{
				PropertyInfo property = tmpdropDown.GetType().GetProperty("value");
				if (property != null)
				{
					property.SetValue(tmpdropDown, this.prevSelectedIndex, null);
				}
			}
		}
		this.OnInteractEnd();
	}

	// Token: 0x06000D24 RID: 3364 RVA: 0x000564EC File Offset: 0x000546EC
	public override bool Increment()
	{
		Dropdown dropdown = this.GetDropdown();
		if (!(dropdown != null))
		{
			Component tmpdropDown = this.GetTMPDropDown();
			if (tmpdropDown != null)
			{
				PropertyInfo property = tmpdropDown.GetType().GetProperty("value");
				if (property != null)
				{
					int num = (int)property.GetValue(tmpdropDown, null);
					int itemCount = this.GetItemCount();
					if (num == itemCount - 1)
					{
						return false;
					}
					property.SetValue(tmpdropDown, num + 1, null);
					return true;
				}
			}
			return false;
		}
		if (dropdown.value == dropdown.options.Count - 1)
		{
			return false;
		}
		Dropdown dropdown2 = dropdown;
		int num2 = dropdown2.value + 1;
		dropdown2.value = num2;
		return true;
	}

	// Token: 0x06000D25 RID: 3365 RVA: 0x00056594 File Offset: 0x00054794
	public override bool Decrement()
	{
		Dropdown dropdown = this.GetDropdown();
		if (!(dropdown != null))
		{
			Component tmpdropDown = this.GetTMPDropDown();
			if (tmpdropDown != null)
			{
				PropertyInfo property = tmpdropDown.GetType().GetProperty("options");
				PropertyInfo property2 = tmpdropDown.GetType().GetProperty("value");
				if (property != null && property2 != null)
				{
					int num = (int)property2.GetValue(tmpdropDown, null);
					if (num == 0)
					{
						return false;
					}
					property2.SetValue(tmpdropDown, num - 1, null);
					return true;
				}
			}
			return false;
		}
		if (dropdown.value == 0)
		{
			return false;
		}
		Dropdown dropdown2 = dropdown;
		int num2 = dropdown2.value - 1;
		dropdown2.value = num2;
		return true;
	}

	// Token: 0x06000D26 RID: 3366 RVA: 0x00056638 File Offset: 0x00054838
	public int GetItemCount()
	{
		Dropdown dropdown = this.GetDropdown();
		if (dropdown != null)
		{
			return dropdown.options.Count;
		}
		Component tmpdropDown = this.GetTMPDropDown();
		if (tmpdropDown != null)
		{
			PropertyInfo property = tmpdropDown.GetType().GetProperty("options");
			if (property != null)
			{
				object value = property.GetValue(tmpdropDown, null);
				PropertyInfo property2 = value.GetType().GetProperty("Count");
				if (property2 != null)
				{
					return (int)property2.GetValue(value, null);
				}
			}
		}
		return 0;
	}

	// Token: 0x06000D27 RID: 3367 RVA: 0x000566C0 File Offset: 0x000548C0
	public int GetSelectedItemIndex()
	{
		Dropdown dropdown = this.GetDropdown();
		if (dropdown != null)
		{
			return dropdown.value + 1;
		}
		Component tmpdropDown = this.GetTMPDropDown();
		if (tmpdropDown != null)
		{
			PropertyInfo property = tmpdropDown.GetType().GetProperty("value");
			if (property != null)
			{
				return (int)property.GetValue(tmpdropDown, null) + 1;
			}
		}
		return 0;
	}

	// Token: 0x06000D28 RID: 3368 RVA: 0x00056724 File Offset: 0x00054924
	protected override void OnHoverHighlight(bool enable)
	{
		Dropdown dropdown = this.GetDropdown();
		if (dropdown != null)
		{
			PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
			if (enable)
			{
				dropdown.OnPointerEnter(pointerEventData);
				return;
			}
			dropdown.OnPointerExit(pointerEventData);
		}
	}

	// Token: 0x04000940 RID: 2368
	[Header("Other")]
	public List<AudioClip> m_ValuesAsAudio = new List<AudioClip>();

	// Token: 0x04000941 RID: 2369
	private int prevSelectedIndex = -1;
}
