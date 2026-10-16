using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200014A RID: 330
[AddComponentMenu("Accessibility/UI/Accessible Label")]
public class AccessibleLabel : UAP_BaseElement
{
	// Token: 0x06000D29 RID: 3369 RVA: 0x0005675E File Offset: 0x0005495E
	private AccessibleLabel()
	{
		this.m_Type = AccessibleUIGroupRoot.EUIElement.ELabel;
	}

	// Token: 0x06000D2A RID: 3370 RVA: 0x00056770 File Offset: 0x00054970
	public override bool IsElementActive()
	{
		if (!base.IsElementActive())
		{
			return false;
		}
		Text label = this.GetLabel();
		if (label != null)
		{
			return label.gameObject.activeInHierarchy && label.enabled;
		}
		Component textMeshLabel = this.GetTextMeshLabel();
		return !(textMeshLabel != null) || textMeshLabel.gameObject.activeInHierarchy;
	}

	// Token: 0x06000D2B RID: 3371 RVA: 0x000567D2 File Offset: 0x000549D2
	public override bool AutoFillTextLabel()
	{
		return true;
	}

	// Token: 0x06000D2C RID: 3372 RVA: 0x000567D8 File Offset: 0x000549D8
	protected Component GetTextMeshLabel()
	{
		GameObject gameObject = base.gameObject;
		if (this.m_ReferenceElement != null)
		{
			gameObject = this.m_ReferenceElement;
		}
		if (this.m_NameLabel != null)
		{
			gameObject = this.m_NameLabel;
		}
		if (gameObject == null)
		{
			gameObject = base.gameObject;
		}
		return gameObject.GetComponent("TMP_Text");
	}

	// Token: 0x06000D2D RID: 3373 RVA: 0x00056834 File Offset: 0x00054A34
	protected override string GetMainText()
	{
		if (!this.m_TryToReadLabel)
		{
			if (base.IsNameLocalizationKey())
			{
				return UAP_AccessibilityManager.Localize(this.m_Text);
			}
			return this.m_Text;
		}
		else
		{
			Text label = this.GetLabel();
			if (label != null)
			{
				if (base.IsNameLocalizationKey())
				{
					return base.CombinePrefix(UAP_AccessibilityManager.Localize(label.text));
				}
				return base.CombinePrefix(label.text);
			}
			else
			{
				string textFromTextMeshPro = base.GetTextFromTextMeshPro(this.GetTextMeshLabel());
				if (!string.IsNullOrEmpty(textFromTextMeshPro))
				{
					if (base.IsNameLocalizationKey())
					{
						return base.CombinePrefix(UAP_AccessibilityManager.Localize(textFromTextMeshPro));
					}
					return base.CombinePrefix(textFromTextMeshPro);
				}
				else
				{
					if (base.IsNameLocalizationKey())
					{
						return UAP_AccessibilityManager.Localize(this.m_Text);
					}
					return this.m_Text;
				}
			}
		}
	}

	// Token: 0x06000D2E RID: 3374 RVA: 0x000568E8 File Offset: 0x00054AE8
	private Text GetLabel()
	{
		Text text = null;
		if (this.m_ReferenceElement != null)
		{
			text = this.m_ReferenceElement.GetComponent<Text>();
		}
		if (this.m_NameLabel != null)
		{
			text = this.m_NameLabel.GetComponent<Text>();
		}
		if (text == null)
		{
			text = base.gameObject.GetComponent<Text>();
		}
		return text;
	}

	// Token: 0x06000D2F RID: 3375 RVA: 0x00056944 File Offset: 0x00054B44
	protected override void AutoInitialize()
	{
		if (this.m_TryToReadLabel)
		{
			Text label = this.GetLabel();
			if (label != null)
			{
				this.m_NameLabel = label.gameObject;
			}
			if (this.m_NameLabel == null)
			{
				Component textMeshLabel = this.GetTextMeshLabel();
				if (textMeshLabel != null)
				{
					this.m_NameLabel = textMeshLabel.gameObject;
					return;
				}
			}
		}
		else
		{
			this.m_NameLabel = null;
		}
	}
}
