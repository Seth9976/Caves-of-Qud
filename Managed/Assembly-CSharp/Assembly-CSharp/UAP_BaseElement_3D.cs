using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000145 RID: 325
public abstract class UAP_BaseElement_3D : UAP_BaseElement
{
	// Token: 0x06000CE8 RID: 3304 RVA: 0x00055183 File Offset: 0x00053383
	public override bool AutoFillTextLabel()
	{
		if (string.IsNullOrEmpty(this.m_Text))
		{
			this.m_Text = base.gameObject.name;
		}
		return true;
	}

	// Token: 0x06000CE9 RID: 3305 RVA: 0x000551A4 File Offset: 0x000533A4
	public override bool Is3DElement()
	{
		return true;
	}

	// Token: 0x06000CEA RID: 3306 RVA: 0x000551A7 File Offset: 0x000533A7
	public float GetPixelHeight()
	{
		return (float)Screen.height / 8f;
	}

	// Token: 0x06000CEB RID: 3307 RVA: 0x000551B5 File Offset: 0x000533B5
	public float GetPixelWidth()
	{
		return (float)Screen.height / 8f;
	}

	// Token: 0x06000CEC RID: 3308 RVA: 0x000551C4 File Offset: 0x000533C4
	public override void HoverHighlight(bool enable, UAP_BaseElement.EHighlightSource selectionSource)
	{
		EventTrigger eventTrigger = null;
		if (this.m_ReferenceElement != null && this.m_ReferenceElement.activeInHierarchy)
		{
			eventTrigger = this.m_ReferenceElement.GetComponent<EventTrigger>();
		}
		if (eventTrigger == null && base.gameObject.activeInHierarchy)
		{
			eventTrigger = base.gameObject.GetComponent<EventTrigger>();
		}
		if (eventTrigger != null)
		{
			if (enable)
			{
				eventTrigger.OnSelect(new BaseEventData(EventSystem.current)
				{
					selectedObject = eventTrigger.gameObject
				});
			}
			else
			{
				eventTrigger.OnDeselect(new BaseEventData(EventSystem.current)
				{
					selectedObject = eventTrigger.gameObject
				});
			}
		}
		this.OnHoverHighlight(enable);
		this.m_CallbackOnHighlight.Invoke(enable, selectionSource);
	}

	// Token: 0x06000CED RID: 3309 RVA: 0x00055278 File Offset: 0x00053478
	protected override string GetLabelText(GameObject go)
	{
		if (go == null)
		{
			return "";
		}
		Text component = go.GetComponent<Text>();
		if (component != null)
		{
			return component.text;
		}
		return "";
	}

	// Token: 0x0400091D RID: 2333
	public Camera m_CameraRenderingThisObject;
}
