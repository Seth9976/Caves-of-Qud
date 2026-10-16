using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TMPro
{
	// Token: 0x02000022 RID: 34
	[AddComponentMenu("UI/Dropdown - TextMeshPro", 35)]
	[RequireComponent(typeof(RectTransform))]
	public class TMP_Dropdown : Selectable, IPointerClickHandler, IEventSystemHandler, ISubmitHandler, ICancelHandler
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00003C91 File Offset: 0x00001E91
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x00003C99 File Offset: 0x00001E99
		public RectTransform template
		{
			get
			{
				return this.m_Template;
			}
			set
			{
				this.m_Template = value;
				this.RefreshShownValue();
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00003CA8 File Offset: 0x00001EA8
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x00003CB0 File Offset: 0x00001EB0
		public TMP_Text captionText
		{
			get
			{
				return this.m_CaptionText;
			}
			set
			{
				this.m_CaptionText = value;
				this.RefreshShownValue();
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00003CBF File Offset: 0x00001EBF
		// (set) Token: 0x060000BB RID: 187 RVA: 0x00003CC7 File Offset: 0x00001EC7
		public Image captionImage
		{
			get
			{
				return this.m_CaptionImage;
			}
			set
			{
				this.m_CaptionImage = value;
				this.RefreshShownValue();
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00003CD6 File Offset: 0x00001ED6
		// (set) Token: 0x060000BD RID: 189 RVA: 0x00003CDE File Offset: 0x00001EDE
		public Graphic placeholder
		{
			get
			{
				return this.m_Placeholder;
			}
			set
			{
				this.m_Placeholder = value;
				this.RefreshShownValue();
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00003CED File Offset: 0x00001EED
		// (set) Token: 0x060000BF RID: 191 RVA: 0x00003CF5 File Offset: 0x00001EF5
		public TMP_Text itemText
		{
			get
			{
				return this.m_ItemText;
			}
			set
			{
				this.m_ItemText = value;
				this.RefreshShownValue();
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00003D04 File Offset: 0x00001F04
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x00003D0C File Offset: 0x00001F0C
		public Image itemImage
		{
			get
			{
				return this.m_ItemImage;
			}
			set
			{
				this.m_ItemImage = value;
				this.RefreshShownValue();
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00003D1B File Offset: 0x00001F1B
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x00003D28 File Offset: 0x00001F28
		public List<TMP_Dropdown.OptionData> options
		{
			get
			{
				return this.m_Options.options;
			}
			set
			{
				this.m_Options.options = value;
				this.RefreshShownValue();
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00003D3C File Offset: 0x00001F3C
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x00003D44 File Offset: 0x00001F44
		public TMP_Dropdown.DropdownEvent onValueChanged
		{
			get
			{
				return this.m_OnValueChanged;
			}
			set
			{
				this.m_OnValueChanged = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00003D4D File Offset: 0x00001F4D
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x00003D55 File Offset: 0x00001F55
		public float alphaFadeSpeed
		{
			get
			{
				return this.m_AlphaFadeSpeed;
			}
			set
			{
				this.m_AlphaFadeSpeed = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00003D5E File Offset: 0x00001F5E
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x00003D66 File Offset: 0x00001F66
		public int value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.SetValue(value, true);
			}
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00003D70 File Offset: 0x00001F70
		public void SetValueWithoutNotify(int input)
		{
			this.SetValue(input, false);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00003D7C File Offset: 0x00001F7C
		private void SetValue(int value, bool sendCallback = true)
		{
			if (Application.isPlaying && (value == this.m_Value || this.options.Count == 0))
			{
				return;
			}
			if (this.m_MultiSelect)
			{
				this.m_Value = value;
			}
			else
			{
				this.m_Value = Mathf.Clamp(value, this.m_Placeholder ? (-1) : 0, this.options.Count - 1);
			}
			this.RefreshShownValue();
			if (sendCallback)
			{
				UISystemProfilerApi.AddMarker("Dropdown.value", this);
				this.m_OnValueChanged.Invoke(this.m_Value);
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00003E07 File Offset: 0x00002007
		public bool IsExpanded
		{
			get
			{
				return this.m_Dropdown != null;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00003E15 File Offset: 0x00002015
		// (set) Token: 0x060000CE RID: 206 RVA: 0x00003E1D File Offset: 0x0000201D
		public bool MultiSelect
		{
			get
			{
				return this.m_MultiSelect;
			}
			set
			{
				this.m_MultiSelect = value;
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00003E26 File Offset: 0x00002026
		protected TMP_Dropdown()
		{
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00003E5C File Offset: 0x0000205C
		protected override void Awake()
		{
			if (this.m_CaptionImage)
			{
				this.m_CaptionImage.enabled = this.m_CaptionImage.sprite != null && this.m_CaptionImage.color.a > 0f;
			}
			if (this.m_Template)
			{
				this.m_Template.gameObject.SetActive(false);
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00003ECC File Offset: 0x000020CC
		protected override void Start()
		{
			this.m_AlphaTweenRunner = new TweenRunner<FloatTween>();
			this.m_AlphaTweenRunner.Init(this);
			base.Start();
			this.RefreshShownValue();
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00003EF1 File Offset: 0x000020F1
		protected override void OnDisable()
		{
			this.ImmediateDestroyDropdownList();
			if (this.m_Blocker != null)
			{
				this.DestroyBlocker(this.m_Blocker);
			}
			this.m_Blocker = null;
			base.OnDisable();
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00003F20 File Offset: 0x00002120
		public void RefreshShownValue()
		{
			TMP_Dropdown.OptionData optionData = TMP_Dropdown.s_NoOptionData;
			if (this.options.Count > 0)
			{
				if (this.m_MultiSelect)
				{
					int num = TMP_Dropdown.FirstActiveFlagIndex(this.m_Value);
					if (this.m_Value == 0 || num >= this.options.Count)
					{
						optionData = TMP_Dropdown.k_NothingOption;
					}
					else if (TMP_Dropdown.IsEverythingValue(this.options.Count, this.m_Value))
					{
						optionData = TMP_Dropdown.k_EverythingOption;
					}
					else if (Mathf.IsPowerOfTwo(this.m_Value) && this.m_Value > 0)
					{
						optionData = this.options[num];
					}
					else
					{
						optionData = TMP_Dropdown.k_MixedOption;
					}
				}
				else if (this.m_Value >= 0)
				{
					optionData = this.options[Mathf.Clamp(this.m_Value, 0, this.options.Count - 1)];
				}
			}
			if (this.m_CaptionText)
			{
				if (optionData != null && optionData.text != null)
				{
					this.m_CaptionText.text = optionData.text;
				}
				else
				{
					this.m_CaptionText.text = "";
				}
			}
			if (this.m_CaptionImage)
			{
				this.m_CaptionImage.sprite = optionData.image;
				this.m_CaptionImage.color = optionData.color;
				this.m_CaptionImage.enabled = this.m_CaptionImage.sprite != null && this.m_CaptionImage.color.a > 0f;
			}
			if (this.m_Placeholder)
			{
				this.m_Placeholder.enabled = this.options.Count == 0 || this.m_Value == -1;
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x000040C4 File Offset: 0x000022C4
		public void AddOptions(List<TMP_Dropdown.OptionData> options)
		{
			this.options.AddRange(options);
			this.RefreshShownValue();
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000040D8 File Offset: 0x000022D8
		public void AddOptions(List<string> options)
		{
			for (int i = 0; i < options.Count; i++)
			{
				this.options.Add(new TMP_Dropdown.OptionData(options[i]));
			}
			this.RefreshShownValue();
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004114 File Offset: 0x00002314
		public void AddOptions(List<Sprite> options)
		{
			for (int i = 0; i < options.Count; i++)
			{
				this.options.Add(new TMP_Dropdown.OptionData(options[i]));
			}
			this.RefreshShownValue();
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x0000414F File Offset: 0x0000234F
		public void ClearOptions()
		{
			this.options.Clear();
			this.m_Value = (this.m_Placeholder ? (-1) : 0);
			this.RefreshShownValue();
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x0000417C File Offset: 0x0000237C
		private void SetupTemplate()
		{
			this.validTemplate = false;
			if (!this.m_Template)
			{
				Debug.LogError("The dropdown template is not assigned. The template needs to be assigned and must have a child GameObject with a Toggle component serving as the item.", this);
				return;
			}
			GameObject gameObject = this.m_Template.gameObject;
			gameObject.SetActive(true);
			Toggle componentInChildren = this.m_Template.GetComponentInChildren<Toggle>();
			this.validTemplate = true;
			if (!componentInChildren || componentInChildren.transform == this.template)
			{
				this.validTemplate = false;
				Debug.LogError("The dropdown template is not valid. The template must have a child GameObject with a Toggle component serving as the item.", this.template);
			}
			else if (!(componentInChildren.transform.parent is RectTransform))
			{
				this.validTemplate = false;
				Debug.LogError("The dropdown template is not valid. The child GameObject with a Toggle component (the item) must have a RectTransform on its parent.", this.template);
			}
			else if (this.itemText != null && !this.itemText.transform.IsChildOf(componentInChildren.transform))
			{
				this.validTemplate = false;
				Debug.LogError("The dropdown template is not valid. The Item Text must be on the item GameObject or children of it.", this.template);
			}
			else if (this.itemImage != null && !this.itemImage.transform.IsChildOf(componentInChildren.transform))
			{
				this.validTemplate = false;
				Debug.LogError("The dropdown template is not valid. The Item Image must be on the item GameObject or children of it.", this.template);
			}
			if (!this.validTemplate)
			{
				gameObject.SetActive(false);
				return;
			}
			TMP_Dropdown.DropdownItem dropdownItem = componentInChildren.gameObject.AddComponent<TMP_Dropdown.DropdownItem>();
			dropdownItem.text = this.m_ItemText;
			dropdownItem.image = this.m_ItemImage;
			dropdownItem.toggle = componentInChildren;
			dropdownItem.rectTransform = (RectTransform)componentInChildren.transform;
			Canvas canvas = null;
			Transform transform = this.m_Template.parent;
			while (transform != null)
			{
				canvas = transform.GetComponent<Canvas>();
				if (canvas != null)
				{
					break;
				}
				transform = transform.parent;
			}
			Canvas orAddComponent = TMP_Dropdown.GetOrAddComponent<Canvas>(gameObject);
			orAddComponent.overrideSorting = true;
			orAddComponent.sortingOrder = 30000;
			if (canvas != null)
			{
				Component[] components = canvas.GetComponents<BaseRaycaster>();
				Component[] array = components;
				for (int i = 0; i < array.Length; i++)
				{
					Type type = array[i].GetType();
					if (gameObject.GetComponent(type) == null)
					{
						gameObject.AddComponent(type);
					}
				}
			}
			else
			{
				TMP_Dropdown.GetOrAddComponent<GraphicRaycaster>(gameObject);
			}
			TMP_Dropdown.GetOrAddComponent<CanvasGroup>(gameObject);
			gameObject.SetActive(false);
			this.validTemplate = true;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000043AC File Offset: 0x000025AC
		private static T GetOrAddComponent<T>(GameObject go) where T : Component
		{
			T t = go.GetComponent<T>();
			if (!t)
			{
				t = go.AddComponent<T>();
			}
			return t;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000043D5 File Offset: 0x000025D5
		public virtual void OnPointerClick(PointerEventData eventData)
		{
			this.Show();
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000043DD File Offset: 0x000025DD
		public virtual void OnSubmit(BaseEventData eventData)
		{
			this.Show();
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000043E5 File Offset: 0x000025E5
		public virtual void OnCancel(BaseEventData eventData)
		{
			this.Hide();
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000043F0 File Offset: 0x000025F0
		public void Show()
		{
			if (this.m_Coroutine != null)
			{
				base.StopCoroutine(this.m_Coroutine);
				this.ImmediateDestroyDropdownList();
			}
			if (!this.IsActive() || !this.IsInteractable() || this.m_Dropdown != null)
			{
				return;
			}
			List<Canvas> list = TMP_ListPool<Canvas>.Get();
			base.gameObject.GetComponentsInParent<Canvas>(false, list);
			if (list.Count == 0)
			{
				return;
			}
			Canvas canvas = list[list.Count - 1];
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].isRootCanvas)
				{
					canvas = list[i];
					break;
				}
			}
			TMP_ListPool<Canvas>.Release(list);
			if (!this.validTemplate)
			{
				this.SetupTemplate();
				if (!this.validTemplate)
				{
					return;
				}
			}
			this.m_Template.gameObject.SetActive(true);
			this.m_Template.GetComponent<Canvas>().sortingLayerID = canvas.sortingLayerID;
			this.m_Dropdown = this.CreateDropdownList(this.m_Template.gameObject);
			this.m_Dropdown.name = "Dropdown List";
			this.m_Dropdown.SetActive(true);
			RectTransform rectTransform = this.m_Dropdown.transform as RectTransform;
			rectTransform.SetParent(this.m_Template.transform.parent, false);
			TMP_Dropdown.DropdownItem componentInChildren = this.m_Dropdown.GetComponentInChildren<TMP_Dropdown.DropdownItem>();
			RectTransform rectTransform2 = componentInChildren.rectTransform.parent.gameObject.transform as RectTransform;
			componentInChildren.rectTransform.gameObject.SetActive(true);
			Rect rect = rectTransform2.rect;
			Rect rect2 = componentInChildren.rectTransform.rect;
			Vector2 vector = rect2.min - rect.min + componentInChildren.rectTransform.localPosition;
			Vector2 vector2 = rect2.max - rect.max + componentInChildren.rectTransform.localPosition;
			Vector2 size = rect2.size;
			this.m_Items.Clear();
			Toggle toggle = null;
			if (this.m_MultiSelect && this.options.Count > 0)
			{
				TMP_Dropdown.DropdownItem dropdownItem = this.AddItem(TMP_Dropdown.k_NothingOption, this.value == 0, componentInChildren, this.m_Items);
				if (dropdownItem.image != null)
				{
					dropdownItem.image.gameObject.SetActive(false);
				}
				Toggle nothingToggle = dropdownItem.toggle;
				nothingToggle.isOn = this.value == 0;
				nothingToggle.onValueChanged.AddListener(delegate(bool x)
				{
					this.OnSelectItem(nothingToggle);
				});
				toggle = nothingToggle;
				bool flag = TMP_Dropdown.IsEverythingValue(this.options.Count, this.value);
				dropdownItem = this.AddItem(TMP_Dropdown.k_EverythingOption, flag, componentInChildren, this.m_Items);
				if (dropdownItem.image != null)
				{
					dropdownItem.image.gameObject.SetActive(false);
				}
				Toggle everythingToggle = dropdownItem.toggle;
				everythingToggle.isOn = flag;
				everythingToggle.onValueChanged.AddListener(delegate(bool x)
				{
					this.OnSelectItem(everythingToggle);
				});
				if (toggle != null)
				{
					Navigation navigation = toggle.navigation;
					Navigation navigation2 = dropdownItem.toggle.navigation;
					navigation.mode = Navigation.Mode.Explicit;
					navigation2.mode = Navigation.Mode.Explicit;
					navigation.selectOnDown = dropdownItem.toggle;
					navigation.selectOnRight = dropdownItem.toggle;
					navigation2.selectOnLeft = toggle;
					navigation2.selectOnUp = toggle;
					toggle.navigation = navigation;
					dropdownItem.toggle.navigation = navigation2;
				}
			}
			for (int j = 0; j < this.options.Count; j++)
			{
				TMP_Dropdown.OptionData optionData = this.options[j];
				TMP_Dropdown.DropdownItem item = this.AddItem(optionData, this.value == j, componentInChildren, this.m_Items);
				if (!(item == null))
				{
					if (this.m_MultiSelect)
					{
						item.toggle.isOn = (this.value & (1 << j)) != 0;
					}
					else
					{
						item.toggle.isOn = this.value == j;
					}
					item.toggle.onValueChanged.AddListener(delegate(bool x)
					{
						this.OnSelectItem(item.toggle);
					});
					if (item.toggle.isOn)
					{
						item.toggle.Select();
					}
					if (toggle != null)
					{
						Navigation navigation3 = toggle.navigation;
						Navigation navigation4 = item.toggle.navigation;
						navigation3.mode = Navigation.Mode.Explicit;
						navigation4.mode = Navigation.Mode.Explicit;
						navigation3.selectOnDown = item.toggle;
						navigation3.selectOnRight = item.toggle;
						navigation4.selectOnLeft = toggle;
						navigation4.selectOnUp = toggle;
						toggle.navigation = navigation3;
						item.toggle.navigation = navigation4;
					}
					toggle = item.toggle;
				}
			}
			Vector2 sizeDelta = rectTransform2.sizeDelta;
			sizeDelta.y = size.y * (float)this.m_Items.Count + vector.y - vector2.y;
			rectTransform2.sizeDelta = sizeDelta;
			float num = rectTransform.rect.height - rectTransform2.rect.height;
			if (num > 0f)
			{
				rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, rectTransform.sizeDelta.y - num);
			}
			Vector3[] array = new Vector3[4];
			rectTransform.GetWorldCorners(array);
			RectTransform rectTransform3 = canvas.transform as RectTransform;
			Rect rect3 = rectTransform3.rect;
			for (int k = 0; k < 2; k++)
			{
				bool flag2 = false;
				for (int l = 0; l < 4; l++)
				{
					Vector3 vector3 = rectTransform3.InverseTransformPoint(array[l]);
					if ((vector3[k] < rect3.min[k] && !Mathf.Approximately(vector3[k], rect3.min[k])) || (vector3[k] > rect3.max[k] && !Mathf.Approximately(vector3[k], rect3.max[k])))
					{
						flag2 = true;
						break;
					}
				}
				if (flag2)
				{
					RectTransformUtility.FlipLayoutOnAxis(rectTransform, k, false, false);
				}
			}
			for (int m = 0; m < this.m_Items.Count; m++)
			{
				RectTransform rectTransform4 = this.m_Items[m].rectTransform;
				rectTransform4.anchorMin = new Vector2(rectTransform4.anchorMin.x, 0f);
				rectTransform4.anchorMax = new Vector2(rectTransform4.anchorMax.x, 0f);
				rectTransform4.anchoredPosition = new Vector2(rectTransform4.anchoredPosition.x, vector.y + size.y * (float)(this.m_Items.Count - 1 - m) + size.y * rectTransform4.pivot.y);
				rectTransform4.sizeDelta = new Vector2(rectTransform4.sizeDelta.x, size.y);
			}
			this.AlphaFadeList(this.m_AlphaFadeSpeed, 0f, 1f);
			this.m_Template.gameObject.SetActive(false);
			componentInChildren.gameObject.SetActive(false);
			this.m_Blocker = this.CreateBlocker(canvas);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00004BD4 File Offset: 0x00002DD4
		private static bool IsEverythingValue(int count, int value)
		{
			bool flag = true;
			for (int i = 0; i < count; i++)
			{
				if ((value & (1 << i)) == 0)
				{
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004BFC File Offset: 0x00002DFC
		private static int EverythingValue(int count)
		{
			int num = 0;
			for (int i = 0; i < count; i++)
			{
				num |= 1 << i;
			}
			return num;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00004C24 File Offset: 0x00002E24
		protected virtual GameObject CreateBlocker(Canvas rootCanvas)
		{
			GameObject gameObject = new GameObject("Blocker");
			gameObject.layer = rootCanvas.gameObject.layer;
			RectTransform rectTransform = gameObject.AddComponent<RectTransform>();
			rectTransform.SetParent(rootCanvas.transform, false);
			rectTransform.anchorMin = Vector3.zero;
			rectTransform.anchorMax = Vector3.one;
			rectTransform.sizeDelta = Vector2.zero;
			Canvas canvas = gameObject.AddComponent<Canvas>();
			canvas.overrideSorting = true;
			Canvas component = this.m_Dropdown.GetComponent<Canvas>();
			canvas.sortingLayerID = component.sortingLayerID;
			canvas.sortingOrder = component.sortingOrder - 1;
			Canvas canvas2 = null;
			Transform transform = this.m_Template.parent;
			while (transform != null)
			{
				canvas2 = transform.GetComponent<Canvas>();
				if (canvas2 != null)
				{
					break;
				}
				transform = transform.parent;
			}
			if (canvas2 != null)
			{
				Component[] components = canvas2.GetComponents<BaseRaycaster>();
				Component[] array = components;
				for (int i = 0; i < array.Length; i++)
				{
					Type type = array[i].GetType();
					if (gameObject.GetComponent(type) == null)
					{
						gameObject.AddComponent(type);
					}
				}
			}
			else
			{
				TMP_Dropdown.GetOrAddComponent<GraphicRaycaster>(gameObject);
			}
			gameObject.AddComponent<Image>().color = Color.clear;
			gameObject.AddComponent<Button>().onClick.AddListener(new UnityAction(this.Hide));
			gameObject.AddComponent<CanvasGroup>().ignoreParentGroups = true;
			return gameObject;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00004D7B File Offset: 0x00002F7B
		protected virtual void DestroyBlocker(GameObject blocker)
		{
			global::UnityEngine.Object.Destroy(blocker);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00004D83 File Offset: 0x00002F83
		protected virtual GameObject CreateDropdownList(GameObject template)
		{
			return global::UnityEngine.Object.Instantiate<GameObject>(template);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00004D8B File Offset: 0x00002F8B
		protected virtual void DestroyDropdownList(GameObject dropdownList)
		{
			global::UnityEngine.Object.Destroy(dropdownList);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004D93 File Offset: 0x00002F93
		protected virtual TMP_Dropdown.DropdownItem CreateItem(TMP_Dropdown.DropdownItem itemTemplate)
		{
			return global::UnityEngine.Object.Instantiate<TMP_Dropdown.DropdownItem>(itemTemplate);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00004D9B File Offset: 0x00002F9B
		protected virtual void DestroyItem(TMP_Dropdown.DropdownItem item)
		{
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00004DA0 File Offset: 0x00002FA0
		private TMP_Dropdown.DropdownItem AddItem(TMP_Dropdown.OptionData data, bool selected, TMP_Dropdown.DropdownItem itemTemplate, List<TMP_Dropdown.DropdownItem> items)
		{
			TMP_Dropdown.DropdownItem dropdownItem = this.CreateItem(itemTemplate);
			dropdownItem.rectTransform.SetParent(itemTemplate.rectTransform.parent, false);
			dropdownItem.gameObject.SetActive(true);
			dropdownItem.gameObject.name = "Item " + items.Count.ToString() + ((data.text != null) ? (": " + data.text) : "");
			if (dropdownItem.toggle != null)
			{
				dropdownItem.toggle.isOn = false;
			}
			if (dropdownItem.text)
			{
				dropdownItem.text.text = data.text;
			}
			if (dropdownItem.image)
			{
				dropdownItem.image.sprite = data.image;
				dropdownItem.image.color = data.color;
				dropdownItem.image.enabled = dropdownItem.image.sprite != null && data.color.a > 0f;
			}
			items.Add(dropdownItem);
			return dropdownItem;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00004EC0 File Offset: 0x000030C0
		private void AlphaFadeList(float duration, float alpha)
		{
			CanvasGroup component = this.m_Dropdown.GetComponent<CanvasGroup>();
			this.AlphaFadeList(duration, component.alpha, alpha);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00004EE8 File Offset: 0x000030E8
		private void AlphaFadeList(float duration, float start, float end)
		{
			if (end.Equals(start))
			{
				return;
			}
			FloatTween floatTween = new FloatTween
			{
				duration = duration,
				startValue = start,
				targetValue = end
			};
			floatTween.AddOnChangedCallback(new UnityAction<float>(this.SetAlpha));
			floatTween.ignoreTimeScale = true;
			this.m_AlphaTweenRunner.StartTween(floatTween);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004F49 File Offset: 0x00003149
		private void SetAlpha(float alpha)
		{
			if (!this.m_Dropdown)
			{
				return;
			}
			this.m_Dropdown.GetComponent<CanvasGroup>().alpha = alpha;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004F6C File Offset: 0x0000316C
		public void Hide()
		{
			if (this.m_Coroutine == null)
			{
				if (this.m_Dropdown != null)
				{
					this.AlphaFadeList(this.m_AlphaFadeSpeed, 0f);
					if (this.IsActive())
					{
						this.m_Coroutine = base.StartCoroutine(this.DelayedDestroyDropdownList(this.m_AlphaFadeSpeed));
					}
				}
				if (this.m_Blocker != null)
				{
					this.DestroyBlocker(this.m_Blocker);
				}
				this.m_Blocker = null;
				this.Select();
			}
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004FE7 File Offset: 0x000031E7
		private IEnumerator DelayedDestroyDropdownList(float delay)
		{
			yield return new WaitForSecondsRealtime(delay);
			this.ImmediateDestroyDropdownList();
			yield break;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00005000 File Offset: 0x00003200
		private void ImmediateDestroyDropdownList()
		{
			for (int i = 0; i < this.m_Items.Count; i++)
			{
				if (this.m_Items[i] != null)
				{
					this.DestroyItem(this.m_Items[i]);
				}
			}
			this.m_Items.Clear();
			if (this.m_Dropdown != null)
			{
				this.DestroyDropdownList(this.m_Dropdown);
			}
			if (this.m_AlphaTweenRunner != null)
			{
				this.m_AlphaTweenRunner.StopTween();
			}
			this.m_Dropdown = null;
			this.m_Coroutine = null;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00005090 File Offset: 0x00003290
		private void OnSelectItem(Toggle toggle)
		{
			int num = -1;
			Transform transform = toggle.transform;
			Transform parent = transform.parent;
			for (int i = 1; i < parent.childCount; i++)
			{
				if (parent.GetChild(i) == transform)
				{
					num = i - 1;
					break;
				}
			}
			if (num < 0)
			{
				return;
			}
			if (this.m_MultiSelect)
			{
				if (num != 0)
				{
					if (num != 1)
					{
						int num2 = 1 << num - 2;
						bool flag = (this.value & num2) != 0;
						toggle.SetIsOnWithoutNotify(!flag);
						if (flag)
						{
							this.value &= ~num2;
						}
						else
						{
							this.value |= num2;
						}
					}
					else
					{
						this.value = TMP_Dropdown.EverythingValue(this.options.Count);
						for (int j = 3; j < parent.childCount; j++)
						{
							Toggle componentInChildren = parent.GetChild(j).GetComponentInChildren<Toggle>();
							if (componentInChildren)
							{
								componentInChildren.SetIsOnWithoutNotify(j > 2);
							}
						}
					}
				}
				else
				{
					this.value = 0;
					for (int k = 3; k < parent.childCount; k++)
					{
						Toggle componentInChildren2 = parent.GetChild(k).GetComponentInChildren<Toggle>();
						if (componentInChildren2)
						{
							componentInChildren2.SetIsOnWithoutNotify(false);
						}
					}
					toggle.isOn = true;
				}
			}
			else
			{
				if (!toggle.isOn)
				{
					toggle.SetIsOnWithoutNotify(true);
				}
				this.value = num;
			}
			this.Hide();
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000051F0 File Offset: 0x000033F0
		private static int FirstActiveFlagIndex(int value)
		{
			if (value == 0)
			{
				return 0;
			}
			for (int i = 0; i < 32; i++)
			{
				if ((value & (1 << i)) != 0)
				{
					return i;
				}
			}
			return 0;
		}

		// Token: 0x0400008A RID: 138
		private static readonly TMP_Dropdown.OptionData k_NothingOption = new TMP_Dropdown.OptionData
		{
			text = "Nothing"
		};

		// Token: 0x0400008B RID: 139
		private static readonly TMP_Dropdown.OptionData k_EverythingOption = new TMP_Dropdown.OptionData
		{
			text = "Everything"
		};

		// Token: 0x0400008C RID: 140
		private static readonly TMP_Dropdown.OptionData k_MixedOption = new TMP_Dropdown.OptionData
		{
			text = "Mixed..."
		};

		// Token: 0x0400008D RID: 141
		[SerializeField]
		private RectTransform m_Template;

		// Token: 0x0400008E RID: 142
		[SerializeField]
		private TMP_Text m_CaptionText;

		// Token: 0x0400008F RID: 143
		[SerializeField]
		private Image m_CaptionImage;

		// Token: 0x04000090 RID: 144
		[SerializeField]
		private Graphic m_Placeholder;

		// Token: 0x04000091 RID: 145
		[Space]
		[SerializeField]
		private TMP_Text m_ItemText;

		// Token: 0x04000092 RID: 146
		[SerializeField]
		private Image m_ItemImage;

		// Token: 0x04000093 RID: 147
		[Space]
		[SerializeField]
		private int m_Value;

		// Token: 0x04000094 RID: 148
		[SerializeField]
		private bool m_MultiSelect;

		// Token: 0x04000095 RID: 149
		[Space]
		[SerializeField]
		private TMP_Dropdown.OptionDataList m_Options = new TMP_Dropdown.OptionDataList();

		// Token: 0x04000096 RID: 150
		[Space]
		[SerializeField]
		private TMP_Dropdown.DropdownEvent m_OnValueChanged = new TMP_Dropdown.DropdownEvent();

		// Token: 0x04000097 RID: 151
		[SerializeField]
		private float m_AlphaFadeSpeed = 0.15f;

		// Token: 0x04000098 RID: 152
		private GameObject m_Dropdown;

		// Token: 0x04000099 RID: 153
		private GameObject m_Blocker;

		// Token: 0x0400009A RID: 154
		private List<TMP_Dropdown.DropdownItem> m_Items = new List<TMP_Dropdown.DropdownItem>();

		// Token: 0x0400009B RID: 155
		private TweenRunner<FloatTween> m_AlphaTweenRunner;

		// Token: 0x0400009C RID: 156
		private bool validTemplate;

		// Token: 0x0400009D RID: 157
		private Coroutine m_Coroutine;

		// Token: 0x0400009E RID: 158
		private static TMP_Dropdown.OptionData s_NoOptionData = new TMP_Dropdown.OptionData();

		// Token: 0x0200008E RID: 142
		protected internal class DropdownItem : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, ICancelHandler
		{
			// Token: 0x17000190 RID: 400
			// (get) Token: 0x06000690 RID: 1680 RVA: 0x00043872 File Offset: 0x00041A72
			// (set) Token: 0x06000691 RID: 1681 RVA: 0x0004387A File Offset: 0x00041A7A
			public TMP_Text text
			{
				get
				{
					return this.m_Text;
				}
				set
				{
					this.m_Text = value;
				}
			}

			// Token: 0x17000191 RID: 401
			// (get) Token: 0x06000692 RID: 1682 RVA: 0x00043883 File Offset: 0x00041A83
			// (set) Token: 0x06000693 RID: 1683 RVA: 0x0004388B File Offset: 0x00041A8B
			public Image image
			{
				get
				{
					return this.m_Image;
				}
				set
				{
					this.m_Image = value;
				}
			}

			// Token: 0x17000192 RID: 402
			// (get) Token: 0x06000694 RID: 1684 RVA: 0x00043894 File Offset: 0x00041A94
			// (set) Token: 0x06000695 RID: 1685 RVA: 0x0004389C File Offset: 0x00041A9C
			public RectTransform rectTransform
			{
				get
				{
					return this.m_RectTransform;
				}
				set
				{
					this.m_RectTransform = value;
				}
			}

			// Token: 0x17000193 RID: 403
			// (get) Token: 0x06000696 RID: 1686 RVA: 0x000438A5 File Offset: 0x00041AA5
			// (set) Token: 0x06000697 RID: 1687 RVA: 0x000438AD File Offset: 0x00041AAD
			public Toggle toggle
			{
				get
				{
					return this.m_Toggle;
				}
				set
				{
					this.m_Toggle = value;
				}
			}

			// Token: 0x06000698 RID: 1688 RVA: 0x000438B6 File Offset: 0x00041AB6
			public virtual void OnPointerEnter(PointerEventData eventData)
			{
				EventSystem.current.SetSelectedGameObject(base.gameObject);
			}

			// Token: 0x06000699 RID: 1689 RVA: 0x000438C8 File Offset: 0x00041AC8
			public virtual void OnCancel(BaseEventData eventData)
			{
				TMP_Dropdown componentInParent = base.GetComponentInParent<TMP_Dropdown>();
				if (componentInParent)
				{
					componentInParent.Hide();
				}
			}

			// Token: 0x0400060F RID: 1551
			[SerializeField]
			private TMP_Text m_Text;

			// Token: 0x04000610 RID: 1552
			[SerializeField]
			private Image m_Image;

			// Token: 0x04000611 RID: 1553
			[SerializeField]
			private RectTransform m_RectTransform;

			// Token: 0x04000612 RID: 1554
			[SerializeField]
			private Toggle m_Toggle;
		}

		// Token: 0x0200008F RID: 143
		[Serializable]
		public class OptionData
		{
			// Token: 0x17000194 RID: 404
			// (get) Token: 0x0600069B RID: 1691 RVA: 0x000438F2 File Offset: 0x00041AF2
			// (set) Token: 0x0600069C RID: 1692 RVA: 0x000438FA File Offset: 0x00041AFA
			public string text
			{
				get
				{
					return this.m_Text;
				}
				set
				{
					this.m_Text = value;
				}
			}

			// Token: 0x17000195 RID: 405
			// (get) Token: 0x0600069D RID: 1693 RVA: 0x00043903 File Offset: 0x00041B03
			// (set) Token: 0x0600069E RID: 1694 RVA: 0x0004390B File Offset: 0x00041B0B
			public Sprite image
			{
				get
				{
					return this.m_Image;
				}
				set
				{
					this.m_Image = value;
				}
			}

			// Token: 0x17000196 RID: 406
			// (get) Token: 0x0600069F RID: 1695 RVA: 0x00043914 File Offset: 0x00041B14
			// (set) Token: 0x060006A0 RID: 1696 RVA: 0x0004391C File Offset: 0x00041B1C
			public Color color
			{
				get
				{
					return this.m_Color;
				}
				set
				{
					this.m_Color = value;
				}
			}

			// Token: 0x060006A1 RID: 1697 RVA: 0x00043925 File Offset: 0x00041B25
			public OptionData()
			{
			}

			// Token: 0x060006A2 RID: 1698 RVA: 0x00043938 File Offset: 0x00041B38
			public OptionData(string text)
			{
				this.text = text;
			}

			// Token: 0x060006A3 RID: 1699 RVA: 0x00043952 File Offset: 0x00041B52
			public OptionData(Sprite image)
			{
				this.image = image;
			}

			// Token: 0x060006A4 RID: 1700 RVA: 0x0004396C File Offset: 0x00041B6C
			public OptionData(string text, Sprite image, Color color)
			{
				this.text = text;
				this.image = image;
				this.color = color;
			}

			// Token: 0x04000613 RID: 1555
			[SerializeField]
			private string m_Text;

			// Token: 0x04000614 RID: 1556
			[SerializeField]
			private Sprite m_Image;

			// Token: 0x04000615 RID: 1557
			[SerializeField]
			private Color m_Color = Color.white;
		}

		// Token: 0x02000090 RID: 144
		[Serializable]
		public class OptionDataList
		{
			// Token: 0x17000197 RID: 407
			// (get) Token: 0x060006A5 RID: 1701 RVA: 0x00043994 File Offset: 0x00041B94
			// (set) Token: 0x060006A6 RID: 1702 RVA: 0x0004399C File Offset: 0x00041B9C
			public List<TMP_Dropdown.OptionData> options
			{
				get
				{
					return this.m_Options;
				}
				set
				{
					this.m_Options = value;
				}
			}

			// Token: 0x060006A7 RID: 1703 RVA: 0x000439A5 File Offset: 0x00041BA5
			public OptionDataList()
			{
				this.options = new List<TMP_Dropdown.OptionData>();
			}

			// Token: 0x04000616 RID: 1558
			[SerializeField]
			private List<TMP_Dropdown.OptionData> m_Options;
		}

		// Token: 0x02000091 RID: 145
		[Serializable]
		public class DropdownEvent : UnityEvent<int>
		{
		}
	}
}
