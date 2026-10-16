using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200014E RID: 334
[AddComponentMenu("Accessibility/UI/Accessible Text Edit")]
public class AccessibleTextEdit : UAP_BaseElement
{
	// Token: 0x06000D43 RID: 3395 RVA: 0x00056D20 File Offset: 0x00054F20
	private AccessibleTextEdit()
	{
		this.m_Type = AccessibleUIGroupRoot.EUIElement.ETextEdit;
	}

	// Token: 0x06000D44 RID: 3396 RVA: 0x00056D45 File Offset: 0x00054F45
	public override bool IsElementActive()
	{
		return base.IsElementActive() && (!(this.m_ReferenceElement != null) || this.m_ReferenceElement.gameObject.activeInHierarchy) && (UAP_AccessibilityManager.GetSpeakDisabledInteractables() || this.IsInteractable());
	}

	// Token: 0x06000D45 RID: 3397 RVA: 0x00056D88 File Offset: 0x00054F88
	private InputField GetInputField()
	{
		InputField inputField = null;
		if (this.m_ReferenceElement != null)
		{
			inputField = this.m_ReferenceElement.GetComponent<InputField>();
		}
		if (inputField == null)
		{
			inputField = base.GetComponent<InputField>();
		}
		return inputField;
	}

	// Token: 0x06000D46 RID: 3398 RVA: 0x00056DC4 File Offset: 0x00054FC4
	private Component GetTMPInputField()
	{
		Component component = null;
		if (this.m_ReferenceElement != null)
		{
			component = this.m_ReferenceElement.GetComponent("TMP_InputField");
		}
		if (component == null)
		{
			component = base.GetComponent("TMP_InputField");
		}
		return component;
	}

	// Token: 0x06000D47 RID: 3399 RVA: 0x00056E08 File Offset: 0x00055008
	public override string GetCurrentValueAsText()
	{
		if (this.IsPassword())
		{
			return UAP_AccessibilityManager.Localize_Internal("Keyboard_PasswordHidden");
		}
		return this.GetValueFromEditBox();
	}

	// Token: 0x06000D48 RID: 3400 RVA: 0x00056E24 File Offset: 0x00055024
	private string GetValueFromEditBox()
	{
		InputField inputField = this.GetInputField();
		if (inputField != null)
		{
			return inputField.text;
		}
		Component tmpinputField = this.GetTMPInputField();
		if (tmpinputField != null)
		{
			PropertyInfo property = tmpinputField.GetType().GetProperty("text");
			if (property != null)
			{
				return property.GetValue(tmpinputField, null) as string;
			}
		}
		return "";
	}

	// Token: 0x06000D49 RID: 3401 RVA: 0x00056E88 File Offset: 0x00055088
	private bool IsPassword()
	{
		InputField inputField = this.GetInputField();
		if (inputField != null)
		{
			return inputField.contentType == InputField.ContentType.Password;
		}
		Component tmpinputField = this.GetTMPInputField();
		if (tmpinputField != null)
		{
			PropertyInfo property = tmpinputField.GetType().GetProperty("contentType");
			if (property != null)
			{
				return (int)property.GetValue(tmpinputField, null) == 7;
			}
		}
		return false;
	}

	// Token: 0x06000D4A RID: 3402 RVA: 0x00056EEC File Offset: 0x000550EC
	public override bool IsInteractable()
	{
		InputField inputField = this.GetInputField();
		if (inputField != null)
		{
			return inputField.enabled && inputField.interactable;
		}
		Behaviour behaviour = (Behaviour)this.GetTMPInputField();
		if (!(behaviour != null))
		{
			return false;
		}
		if (!behaviour.enabled)
		{
			return false;
		}
		PropertyInfo property = behaviour.GetType().GetProperty("interactable");
		return !(property != null) || (bool)property.GetValue(behaviour, null);
	}

	// Token: 0x06000D4B RID: 3403 RVA: 0x00056F68 File Offset: 0x00055168
	protected override void OnInteract()
	{
		bool flag = UAP_AccessibilityManager.ShouldUseBuiltInKeyboard();
		bool flag2 = this.IsPassword();
		bool flag3 = false;
		InputField inputField = this.GetInputField();
		if (inputField != null)
		{
			this.prevText = inputField.text;
			this.deltaText = this.prevText;
			if (!flag)
			{
				inputField.onValueChanged.AddListener(delegate
				{
					this.ValueChangeCheck();
				});
				EventSystem.current.SetSelectedGameObject(inputField.gameObject);
			}
			else
			{
				flag3 = inputField.multiLine;
			}
		}
		Behaviour behaviour = (Behaviour)this.GetTMPInputField();
		if (behaviour != null)
		{
			PropertyInfo property = behaviour.GetType().GetProperty("text");
			this.prevText = property.GetValue(behaviour, null) as string;
			this.deltaText = this.prevText;
			if (!flag)
			{
				PropertyInfo property2 = behaviour.GetType().GetProperty("onValueChanged");
				if (property2 != null)
				{
					((UnityEvent<string>)property2.GetValue(behaviour, null)).AddListener(delegate
					{
						this.ValueChangeCheck();
					});
					EventSystem.current.SetSelectedGameObject(behaviour.gameObject);
				}
			}
			else
			{
				PropertyInfo property3 = behaviour.GetType().GetProperty("multiLine");
				if (property3 != null)
				{
					flag3 = (bool)property3.GetValue(behaviour, null);
				}
			}
		}
		if (flag)
		{
			UAP_VirtualKeyboard.ShowOnscreenKeyboard(this.prevText, flag2 ? UAP_VirtualKeyboard.EKeyboardMode.Password : UAP_VirtualKeyboard.EKeyboardMode.Default, !flag2, flag3);
			UAP_VirtualKeyboard.SetOnFinishListener(new UnityAction<string, bool>(this.OnInputFinished));
		}
	}

	// Token: 0x06000D4C RID: 3404 RVA: 0x000570DC File Offset: 0x000552DC
	private void OnInputFinished(string editedText, bool wasConfirmed)
	{
		if (wasConfirmed)
		{
			InputField inputField = this.GetInputField();
			if (inputField != null)
			{
				inputField.text = editedText;
				inputField.onEndEdit.Invoke(editedText);
			}
			Component tmpinputField = this.GetTMPInputField();
			if (tmpinputField != null)
			{
				PropertyInfo property = tmpinputField.GetType().GetProperty("text");
				if (property != null)
				{
					property.SetValue(tmpinputField, editedText, null);
					PropertyInfo property2 = tmpinputField.GetType().GetProperty("onEndEdit");
					if (property2 != null)
					{
						((UnityEvent<string>)property2.GetValue(tmpinputField, null)).Invoke(editedText);
					}
				}
			}
		}
		UAP_AccessibilityManager.FinishCurrentInteraction();
	}

	// Token: 0x06000D4D RID: 3405 RVA: 0x00057178 File Offset: 0x00055378
	public void ValueChangeCheck()
	{
		string text = "";
		InputField inputField = this.GetInputField();
		if (inputField != null)
		{
			text = inputField.text;
		}
		Component tmpinputField = this.GetTMPInputField();
		if (tmpinputField != null)
		{
			PropertyInfo property = tmpinputField.GetType().GetProperty("text");
			if (property != null)
			{
				text = property.GetValue(tmpinputField, null) as string;
			}
		}
		string text2 = text;
		if (text.StartsWith(this.deltaText))
		{
			text = text.Substring(this.deltaText.Length);
		}
		if (text.Length > 0)
		{
			UAP_AccessibilityManager.Say(text, true, true, UAP_AudioQueue.EInterrupt.Elements);
		}
		this.deltaText = text2;
	}

	// Token: 0x06000D4E RID: 3406 RVA: 0x00057220 File Offset: 0x00055420
	protected override void OnInteractAbort()
	{
		InputField inputField = this.GetInputField();
		if (inputField != null)
		{
			inputField.onValueChanged.RemoveListener(delegate
			{
				this.ValueChangeCheck();
			});
			inputField.text = this.prevText;
		}
		Component tmpinputField = this.GetTMPInputField();
		if (tmpinputField != null)
		{
			PropertyInfo property = tmpinputField.GetType().GetProperty("onValueChanged");
			if (property != null)
			{
				((UnityEvent<string>)property.GetValue(tmpinputField, null)).AddListener(delegate
				{
					this.ValueChangeCheck();
				});
				PropertyInfo property2 = tmpinputField.GetType().GetProperty("text");
				if (property2 != null)
				{
					property2.SetValue(tmpinputField, this.prevText, null);
				}
			}
		}
		this.prevText = "";
	}

	// Token: 0x06000D4F RID: 3407 RVA: 0x000572DC File Offset: 0x000554DC
	protected override void OnInteractEnd()
	{
		InputField inputField = this.GetInputField();
		if (inputField != null)
		{
			inputField.onValueChanged.RemoveListener(delegate
			{
				this.ValueChangeCheck();
			});
		}
		Component tmpinputField = this.GetTMPInputField();
		if (tmpinputField != null)
		{
			PropertyInfo property = tmpinputField.GetType().GetProperty("onValueChanged");
			if (property != null)
			{
				((UnityEvent<string>)property.GetValue(tmpinputField, null)).RemoveListener(delegate
				{
					this.ValueChangeCheck();
				});
			}
		}
	}

	// Token: 0x06000D50 RID: 3408 RVA: 0x00057358 File Offset: 0x00055558
	protected override void OnHoverHighlight(bool enable)
	{
		InputField inputField = this.GetInputField();
		if (inputField != null)
		{
			PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
			if (enable)
			{
				inputField.OnPointerEnter(pointerEventData);
				return;
			}
			inputField.OnPointerExit(pointerEventData);
		}
	}

	// Token: 0x06000D51 RID: 3409 RVA: 0x00057392 File Offset: 0x00055592
	public override bool AutoFillTextLabel()
	{
		if (!base.AutoFillTextLabel())
		{
			this.m_Text = "";
		}
		return false;
	}

	// Token: 0x04000947 RID: 2375
	private string prevText = "";

	// Token: 0x04000948 RID: 2376
	private string deltaText = "";
}
