using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200014D RID: 333
[AddComponentMenu("Accessibility/UI/Accessible Slider")]
public class AccessibleSlider : UAP_BaseElement
{
	// Token: 0x06000D39 RID: 3385 RVA: 0x00056A84 File Offset: 0x00054C84
	private AccessibleSlider()
	{
		this.m_Type = AccessibleUIGroupRoot.EUIElement.ESlider;
	}

	// Token: 0x06000D3A RID: 3386 RVA: 0x00056AB3 File Offset: 0x00054CB3
	public override bool IsElementActive()
	{
		return base.IsElementActive() && (!(this.m_ReferenceElement != null) || this.m_ReferenceElement.gameObject.activeInHierarchy) && (UAP_AccessibilityManager.GetSpeakDisabledInteractables() || this.IsInteractable());
	}

	// Token: 0x06000D3B RID: 3387 RVA: 0x00056AF4 File Offset: 0x00054CF4
	public override bool IsInteractable()
	{
		Slider slider = this.GetSlider();
		return !(slider != null) || (slider.enabled && slider.IsInteractable());
	}

	// Token: 0x06000D3C RID: 3388 RVA: 0x00056B28 File Offset: 0x00054D28
	private Slider GetSlider()
	{
		Slider slider = null;
		if (this.m_ReferenceElement != null)
		{
			slider = this.m_ReferenceElement.GetComponent<Slider>();
		}
		if (slider == null)
		{
			slider = base.GetComponent<Slider>();
		}
		return slider;
	}

	// Token: 0x06000D3D RID: 3389 RVA: 0x00056B64 File Offset: 0x00054D64
	public override string GetCurrentValueAsText()
	{
		bool flag = false;
		float num = -1f;
		Slider slider = this.GetSlider();
		if (slider != null)
		{
			flag = true;
			num = slider.value;
			if (this.m_ReadPercentages)
			{
				num = (num - slider.minValue) / (slider.maxValue - slider.minValue);
				num *= 100f;
			}
		}
		if (flag)
		{
			string text = num.ToString("0.##");
			if (this.m_WholeNumbersOnly || (slider != null && slider.wholeNumbers))
			{
				text = num.ToString("0");
			}
			if (this.m_ReadPercentages)
			{
				text += "%";
			}
			return text;
		}
		return "";
	}

	// Token: 0x06000D3E RID: 3390 RVA: 0x00056C0C File Offset: 0x00054E0C
	public override bool Increment()
	{
		Slider slider = this.GetSlider();
		if (slider != null && slider.value == slider.maxValue)
		{
			return false;
		}
		this.ModifySliderValue(this.m_Increments);
		return true;
	}

	// Token: 0x06000D3F RID: 3391 RVA: 0x00056C48 File Offset: 0x00054E48
	public override bool Decrement()
	{
		Slider slider = this.GetSlider();
		if (slider != null && slider.value == slider.minValue)
		{
			return false;
		}
		this.ModifySliderValue(-this.m_Increments);
		return true;
	}

	// Token: 0x06000D40 RID: 3392 RVA: 0x00056C84 File Offset: 0x00054E84
	private void ModifySliderValue(float change)
	{
		Slider slider = this.GetSlider();
		if (slider != null)
		{
			float num = change;
			if (this.m_IncrementInPercent)
			{
				num = (slider.maxValue - slider.minValue) * (num / 100f);
			}
			slider.value += num;
		}
	}

	// Token: 0x06000D41 RID: 3393 RVA: 0x00056CD0 File Offset: 0x00054ED0
	protected override void OnHoverHighlight(bool enable)
	{
		Slider slider = this.GetSlider();
		if (slider != null)
		{
			PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
			if (enable)
			{
				slider.OnPointerEnter(pointerEventData);
				return;
			}
			slider.OnPointerExit(pointerEventData);
		}
	}

	// Token: 0x06000D42 RID: 3394 RVA: 0x00056D0A File Offset: 0x00054F0A
	public override bool AutoFillTextLabel()
	{
		if (!base.AutoFillTextLabel())
		{
			this.m_Text = "";
		}
		return false;
	}

	// Token: 0x04000943 RID: 2371
	public bool m_ReadPercentages = true;

	// Token: 0x04000944 RID: 2372
	public float m_Increments = 5f;

	// Token: 0x04000945 RID: 2373
	public bool m_IncrementInPercent = true;

	// Token: 0x04000946 RID: 2374
	public bool m_WholeNumbersOnly = true;
}
