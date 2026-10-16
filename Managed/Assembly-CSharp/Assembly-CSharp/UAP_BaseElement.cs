using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

// Token: 0x02000144 RID: 324
public abstract class UAP_BaseElement : MonoBehaviour
{
	// Token: 0x06000CBB RID: 3259 RVA: 0x000547BF File Offset: 0x000529BF
	private void Reset()
	{
		this.AutoFillTextLabel();
		this.m_IsInitialized = false;
		this.Initialize();
	}

	// Token: 0x06000CBC RID: 3260 RVA: 0x000547D5 File Offset: 0x000529D5
	public void Initialize()
	{
		if (this.m_IsInitialized)
		{
			return;
		}
		this.AutoInitialize();
		if (!this.m_IsLocalizationKey)
		{
			this.m_Text = this.GetMainText();
		}
		this.m_TryToReadLabel = this.m_NameLabel != null;
		this.m_IsInitialized = true;
	}

	// Token: 0x06000CBD RID: 3261 RVA: 0x00054813 File Offset: 0x00052A13
	protected virtual void AutoInitialize()
	{
		bool isInitialized = this.m_IsInitialized;
	}

	// Token: 0x06000CBE RID: 3262 RVA: 0x0005481C File Offset: 0x00052A1C
	private void OnEnable()
	{
		if (!this.m_HasStarted)
		{
			return;
		}
		this.RegisterWithContainer();
		base.CancelInvoke("RefreshContainerNextFrame");
		base.Invoke("RefreshContainerNextFrame", 0.5f);
	}

	// Token: 0x06000CBF RID: 3263 RVA: 0x00054848 File Offset: 0x00052A48
	private void Start()
	{
		this.m_HasStarted = true;
		this.RegisterWithContainer();
		base.CancelInvoke("RefreshContainerNextFrame");
		base.Invoke("RefreshContainerNextFrame", 0.5f);
	}

	// Token: 0x06000CC0 RID: 3264 RVA: 0x00054874 File Offset: 0x00052A74
	private void GetContainer()
	{
		Transform transform = base.transform;
		while (transform != null && this.AUIContainer == null)
		{
			this.AUIContainer = transform.gameObject.GetComponent<AccessibleUIGroupRoot>();
			transform = transform.parent;
		}
	}

	// Token: 0x06000CC1 RID: 3265 RVA: 0x000548B9 File Offset: 0x00052AB9
	internal AccessibleUIGroupRoot GetUIGroupContainer()
	{
		if (this.AUIContainer == null)
		{
			this.GetContainer();
		}
		return this.AUIContainer;
	}

	// Token: 0x06000CC2 RID: 3266 RVA: 0x000548D8 File Offset: 0x00052AD8
	private void RegisterWithContainer()
	{
		this.GetContainer();
		if (this.AUIContainer == null)
		{
			this.LogErrorNoValidParent();
			return;
		}
		this.AUIContainer.CheckForRegister(this);
		UAP_SelectionGroup[] componentsInParent = base.GetComponentsInParent<UAP_SelectionGroup>();
		for (int i = 0; i < componentsInParent.Length; i++)
		{
			componentsInParent[i].AddElement(this);
		}
	}

	// Token: 0x06000CC3 RID: 3267 RVA: 0x0005492A File Offset: 0x00052B2A
	public void SetAsStartItem()
	{
		this.m_ForceStartHere = true;
		this.GetContainer();
		if (this.AUIContainer == null)
		{
			this.LogErrorNoValidParent();
			return;
		}
		this.AUIContainer.SetAsStartItem(this);
	}

	// Token: 0x06000CC4 RID: 3268 RVA: 0x0005495A File Offset: 0x00052B5A
	private void RefreshContainerNextFrame()
	{
		this.GetContainer();
		if (this.AUIContainer == null)
		{
			this.LogErrorNoValidParent();
			return;
		}
		this.AUIContainer.RefreshNextUpdate();
	}

	// Token: 0x06000CC5 RID: 3269 RVA: 0x00054984 File Offset: 0x00052B84
	private void LogErrorNoValidParent()
	{
		string text = base.gameObject.name;
		Transform transform = base.gameObject.transform.parent;
		while (transform != null)
		{
			text = transform.name + "/" + text;
			transform = transform.parent;
		}
		Debug.LogError("[Accessibility] Could not find an Accessibility UI Container in any parent object of " + base.gameObject.name + "! This UI element will be unaccessible. Full Path: " + text);
	}

	// Token: 0x06000CC6 RID: 3270 RVA: 0x000549F2 File Offset: 0x00052BF2
	public virtual bool Is3DElement()
	{
		return false;
	}

	// Token: 0x06000CC7 RID: 3271 RVA: 0x000549F8 File Offset: 0x00052BF8
	public virtual bool AutoFillTextLabel()
	{
		bool flag = false;
		if (this.m_NameLabel != null)
		{
			Text component = this.m_NameLabel.GetComponent<Text>();
			if (component != null)
			{
				this.m_Text = component.text;
				flag = true;
			}
			if (!flag)
			{
				Component component2 = this.m_NameLabel.GetComponent("TMP_Text");
				if (component2 != null)
				{
					this.m_Text = this.GetTextFromTextMeshPro(component2);
					flag = true;
				}
			}
		}
		if (!flag)
		{
			this.m_Text = base.gameObject.name;
		}
		return flag;
	}

	// Token: 0x06000CC8 RID: 3272 RVA: 0x00054A7C File Offset: 0x00052C7C
	private void OnDestroy()
	{
		if (this.AUIContainer != null)
		{
			this.AUIContainer.UnRegister(this);
		}
		UAP_SelectionGroup[] componentsInParent = base.GetComponentsInParent<UAP_SelectionGroup>();
		for (int i = 0; i < componentsInParent.Length; i++)
		{
			componentsInParent[i].RemoveElement(this);
		}
	}

	// Token: 0x06000CC9 RID: 3273 RVA: 0x00054AC1 File Offset: 0x00052CC1
	public virtual bool IsInteractable()
	{
		return false;
	}

	// Token: 0x06000CCA RID: 3274 RVA: 0x00054AC4 File Offset: 0x00052CC4
	public void Interact()
	{
		this.m_OnInteractionStart.Invoke();
		this.OnInteract();
	}

	// Token: 0x06000CCB RID: 3275 RVA: 0x00054AD7 File Offset: 0x00052CD7
	protected virtual void OnInteract()
	{
	}

	// Token: 0x06000CCC RID: 3276 RVA: 0x00054AD9 File Offset: 0x00052CD9
	public void InteractEnd()
	{
		this.m_OnInteractionEnd.Invoke();
		this.OnInteractEnd();
	}

	// Token: 0x06000CCD RID: 3277 RVA: 0x00054AEC File Offset: 0x00052CEC
	protected virtual void OnInteractEnd()
	{
	}

	// Token: 0x06000CCE RID: 3278 RVA: 0x00054AEE File Offset: 0x00052CEE
	public void InteractAbort()
	{
		this.m_OnInteractionAbort.Invoke();
		this.OnInteractAbort();
	}

	// Token: 0x06000CCF RID: 3279 RVA: 0x00054B01 File Offset: 0x00052D01
	protected virtual void OnInteractAbort()
	{
		this.OnInteractEnd();
	}

	// Token: 0x06000CD0 RID: 3280 RVA: 0x00054B0C File Offset: 0x00052D0C
	protected string CombinePrefix(string text)
	{
		string text2 = (this.m_PrefixIsLocalizationKey ? UAP_AccessibilityManager.Localize(this.m_Prefix) : this.m_Prefix);
		if (text2.Length == 0)
		{
			return text;
		}
		if (text2.IndexOf("{0}") != -1)
		{
			string text3 = text2;
			text3 = text3.Replace("{0}", text);
			if (this.m_AdditionalNameLabels != null && this.m_AdditionalNameLabels.Count > 0)
			{
				for (int i = 0; i < this.m_AdditionalNameLabels.Count; i++)
				{
					if (text3.IndexOf("{" + (i + 1).ToString("0") + "}") != -1)
					{
						text3 = text3.Replace("{" + (i + 1).ToString("0") + "}", this.GetLabelText(this.m_AdditionalNameLabels[i]));
					}
				}
			}
			return text3;
		}
		if (this.m_PrefixIsPostFix)
		{
			return text + " " + text2;
		}
		return text2 + " " + text;
	}

	// Token: 0x06000CD1 RID: 3281 RVA: 0x00054C14 File Offset: 0x00052E14
	public static string FilterText(string text)
	{
		UAP_BaseElement.RemoveSubsting(ref text, "[-]");
		UAP_BaseElement.RemoveSubsting(ref text, "<b>");
		UAP_BaseElement.RemoveSubsting(ref text, "</b>");
		UAP_BaseElement.RemoveSubsting(ref text, "<B>");
		UAP_BaseElement.RemoveSubsting(ref text, "</B>");
		UAP_BaseElement.RemoveSubsting(ref text, "<u>");
		UAP_BaseElement.RemoveSubsting(ref text, "</u>");
		UAP_BaseElement.RemoveSubsting(ref text, "<U>");
		UAP_BaseElement.RemoveSubsting(ref text, "</U>");
		UAP_BaseElement.RemoveSubsting(ref text, "<i>");
		UAP_BaseElement.RemoveSubsting(ref text, "</i>");
		UAP_BaseElement.RemoveSubsting(ref text, "<I>");
		UAP_BaseElement.RemoveSubsting(ref text, "</I>");
		int num = 0;
		num = text.IndexOf('[', num);
		while (num > -1 && text.Length > num + 7)
		{
			if (text[num + 7] == ']')
			{
				text = text.Remove(num, 8);
			}
			else
			{
				num++;
			}
			num = text.IndexOf('[', num);
		}
		return text;
	}

	// Token: 0x06000CD2 RID: 3282 RVA: 0x00054D04 File Offset: 0x00052F04
	private static void RemoveSubsting(ref string text, string substring)
	{
		for (int i = text.LastIndexOf(substring); i >= 0; i = text.LastIndexOf(substring))
		{
			text = text.Replace(substring, "");
		}
	}

	// Token: 0x06000CD3 RID: 3283 RVA: 0x00054D38 File Offset: 0x00052F38
	public string GetTextToRead()
	{
		string text = this.GetMainText();
		if (this.m_FilterText)
		{
			text = UAP_BaseElement.FilterText(text);
		}
		return text;
	}

	// Token: 0x06000CD4 RID: 3284 RVA: 0x00054D5C File Offset: 0x00052F5C
	protected virtual string GetMainText()
	{
		if (this.m_TryToReadLabel)
		{
			this.AutoFillTextLabel();
		}
		if (this.IsNameLocalizationKey())
		{
			return this.CombinePrefix(UAP_AccessibilityManager.Localize(this.m_Text));
		}
		if (this.m_TryToReadLabel)
		{
			return this.CombinePrefix(this.m_Text);
		}
		return this.m_Text;
	}

	// Token: 0x06000CD5 RID: 3285 RVA: 0x00054DAD File Offset: 0x00052FAD
	public void SetCustomText(string itemText)
	{
		this.m_TryToReadLabel = false;
		this.m_Text = itemText;
		this.m_IsLocalizationKey = false;
	}

	// Token: 0x06000CD6 RID: 3286 RVA: 0x00054DC4 File Offset: 0x00052FC4
	public virtual string GetCurrentValueAsText()
	{
		return "";
	}

	// Token: 0x06000CD7 RID: 3287 RVA: 0x00054DCB File Offset: 0x00052FCB
	public virtual AudioClip GetCurrentValueAsAudio()
	{
		return null;
	}

	// Token: 0x06000CD8 RID: 3288 RVA: 0x00054DCE File Offset: 0x00052FCE
	public virtual bool IsElementActive()
	{
		return base.enabled && base.gameObject.activeInHierarchy;
	}

	// Token: 0x06000CD9 RID: 3289 RVA: 0x00054DEC File Offset: 0x00052FEC
	public bool SelectItem(bool forceRepeatItem = false)
	{
		if (!this.IsElementActive())
		{
			if (UAP_AccessibilityManager.IsEnabled())
			{
				Debug.LogWarning(string.Concat(new string[]
				{
					"[Accessibility] Trying to select element '",
					this.GetMainText(),
					"' (",
					base.gameObject.name,
					") but the element is not active/interactable/visible."
				}));
			}
			return false;
		}
		bool hasStarted = this.m_HasStarted;
		return this.SelectItem_Internal(forceRepeatItem);
	}

	// Token: 0x06000CDA RID: 3290 RVA: 0x00054E58 File Offset: 0x00053058
	private bool SelectItem_Internal(bool forceRepeatItem)
	{
		if (this.AUIContainer == null)
		{
			this.RegisterWithContainer();
			if (this.AUIContainer == null)
			{
				Debug.LogWarning("[Accessibility] SelectItem: " + base.gameObject.name + " is not placed within an Accessibility UI container. Can't be selected. Aborting.");
				return false;
			}
		}
		return this.AUIContainer.SelectItem(this, forceRepeatItem);
	}

	// Token: 0x06000CDB RID: 3291 RVA: 0x00054EB5 File Offset: 0x000530B5
	public virtual bool Increment()
	{
		return false;
	}

	// Token: 0x06000CDC RID: 3292 RVA: 0x00054EB8 File Offset: 0x000530B8
	public virtual bool Decrement()
	{
		return false;
	}

	// Token: 0x06000CDD RID: 3293 RVA: 0x00054EBC File Offset: 0x000530BC
	public virtual void HoverHighlight(bool enable, UAP_BaseElement.EHighlightSource selectionSource)
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

	// Token: 0x06000CDE RID: 3294 RVA: 0x00054F6E File Offset: 0x0005316E
	protected virtual void OnHoverHighlight(bool enable)
	{
	}

	// Token: 0x06000CDF RID: 3295 RVA: 0x00054F70 File Offset: 0x00053170
	public GameObject GetTargetGameObject()
	{
		if (this.m_ReferenceElement != null)
		{
			return this.m_ReferenceElement.gameObject;
		}
		return base.gameObject;
	}

	// Token: 0x06000CE0 RID: 3296 RVA: 0x00054F92 File Offset: 0x00053192
	public bool IsNameLocalizationKey()
	{
		return this.m_IsLocalizationKey;
	}

	// Token: 0x06000CE1 RID: 3297 RVA: 0x00054F9A File Offset: 0x0005319A
	public string GetCustomHint()
	{
		if (this.m_HintIsLocalizationKey)
		{
			return UAP_AccessibilityManager.Localize(this.m_Hint);
		}
		return this.m_Hint;
	}

	// Token: 0x06000CE2 RID: 3298 RVA: 0x00054FB6 File Offset: 0x000531B6
	public void SetCustomHintText(string hintText, bool isLocalizationKey = false)
	{
		this.m_CustomHint = true;
		this.m_Hint = hintText;
		this.m_HintIsLocalizationKey = isLocalizationKey;
	}

	// Token: 0x06000CE3 RID: 3299 RVA: 0x00054FCD File Offset: 0x000531CD
	public void ResetHintText()
	{
		this.m_CustomHint = false;
	}

	// Token: 0x06000CE4 RID: 3300 RVA: 0x00054FD8 File Offset: 0x000531D8
	protected string GetTextFromTextMeshPro(Component textMeshProLabel)
	{
		if (textMeshProLabel == null)
		{
			return null;
		}
		string text = null;
		PropertyInfo property = textMeshProLabel.GetType().GetProperty("text");
		if (property != null)
		{
			text = property.GetValue(textMeshProLabel, null) as string;
		}
		return text;
	}

	// Token: 0x06000CE5 RID: 3301 RVA: 0x0005501C File Offset: 0x0005321C
	protected virtual string GetLabelText(GameObject go)
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
		string textFromTextMeshPro = this.GetTextFromTextMeshPro(go.GetComponent("TMP_Text"));
		if (!string.IsNullOrEmpty(textFromTextMeshPro))
		{
			return textFromTextMeshPro;
		}
		return "";
	}

	// Token: 0x06000CE6 RID: 3302 RVA: 0x00055070 File Offset: 0x00053270
	protected Component GetTextMeshProLabelInChildren()
	{
		foreach (object obj in base.transform)
		{
			Component component = ((Transform)obj).gameObject.GetComponent("TMP_Text");
			if (component != null)
			{
				return component;
			}
		}
		return null;
	}

	// Token: 0x040008FA RID: 2298
	public UnityEvent m_OnInteractionStart = new UnityEvent();

	// Token: 0x040008FB RID: 2299
	public UnityEvent m_OnInteractionEnd = new UnityEvent();

	// Token: 0x040008FC RID: 2300
	public UnityEvent m_OnInteractionAbort = new UnityEvent();

	// Token: 0x040008FD RID: 2301
	public bool m_ForceStartHere;

	// Token: 0x040008FE RID: 2302
	public int m_ManualPositionOrder = -1;

	// Token: 0x040008FF RID: 2303
	public GameObject m_ManualPositionParent;

	// Token: 0x04000900 RID: 2304
	public bool m_UseTargetForOutline;

	// Token: 0x04000901 RID: 2305
	public int m_PositionOrder;

	// Token: 0x04000902 RID: 2306
	public int m_SecondaryOrder;

	// Token: 0x04000903 RID: 2307
	public Vector2 m_Pos = new Vector2(0f, 0f);

	// Token: 0x04000904 RID: 2308
	[Header("Element Name")]
	public AudioClip m_TextAsAudio;

	// Token: 0x04000905 RID: 2309
	public string m_Prefix = "";

	// Token: 0x04000906 RID: 2310
	public bool m_PrefixIsLocalizationKey;

	// Token: 0x04000907 RID: 2311
	public bool m_PrefixIsPostFix;

	// Token: 0x04000908 RID: 2312
	public bool m_FilterText = true;

	// Token: 0x04000909 RID: 2313
	public string m_Text = "";

	// Token: 0x0400090A RID: 2314
	public GameObject m_NameLabel;

	// Token: 0x0400090B RID: 2315
	public List<GameObject> m_AdditionalNameLabels;

	// Token: 0x0400090C RID: 2316
	public GameObject[] m_TestList;

	// Token: 0x0400090D RID: 2317
	[FormerlySerializedAs("m_IsNGUILocalizationKey")]
	public bool m_IsLocalizationKey;

	// Token: 0x0400090E RID: 2318
	public bool m_TryToReadLabel = true;

	// Token: 0x0400090F RID: 2319
	public GameObject m_ReferenceElement;

	// Token: 0x04000910 RID: 2320
	public bool m_AllowVoiceOver = true;

	// Token: 0x04000911 RID: 2321
	public bool m_ReadType = true;

	// Token: 0x04000912 RID: 2322
	[HideInInspector]
	public bool m_WasJustAdded = true;

	// Token: 0x04000913 RID: 2323
	[HideInInspector]
	public AccessibleUIGroupRoot.EUIElement m_Type;

	// Token: 0x04000914 RID: 2324
	private AccessibleUIGroupRoot AUIContainer;

	// Token: 0x04000915 RID: 2325
	public bool m_CustomHint;

	// Token: 0x04000916 RID: 2326
	public AudioClip m_HintAsAudio;

	// Token: 0x04000917 RID: 2327
	public string m_Hint = "";

	// Token: 0x04000918 RID: 2328
	public bool m_HintIsLocalizationKey;

	// Token: 0x04000919 RID: 2329
	[HideInInspector]
	public bool m_IsInsideScrollView;

	// Token: 0x0400091A RID: 2330
	private bool m_HasStarted;

	// Token: 0x0400091B RID: 2331
	[HideInInspector]
	public bool m_IsInitialized;

	// Token: 0x0400091C RID: 2332
	public UAP_BaseElement.UAPHighlightCallback m_CallbackOnHighlight = new UAP_BaseElement.UAPHighlightCallback();

	// Token: 0x020015F6 RID: 5622
	[Serializable]
	public class UAPBoolCallback : UnityEvent<bool>
	{
	}

	// Token: 0x020015F7 RID: 5623
	public enum EHighlightSource
	{
		// Token: 0x0400587E RID: 22654
		Internal,
		// Token: 0x0400587F RID: 22655
		UserInput,
		// Token: 0x04005880 RID: 22656
		TouchExplore
	}

	// Token: 0x020015F8 RID: 5624
	[Serializable]
	public class UAPHighlightCallback : UnityEvent<bool, UAP_BaseElement.EHighlightSource>
	{
	}
}
