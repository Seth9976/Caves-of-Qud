using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace RedShadow.CommonDialogs
{
	// Token: 0x020001EF RID: 495
	public class MenuItem : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
	{
		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060011B0 RID: 4528 RVA: 0x0006A2B6 File Offset: 0x000684B6
		// (set) Token: 0x060011B1 RID: 4529 RVA: 0x0006A2BE File Offset: 0x000684BE
		public bool Checked { get; private set; }

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060011B2 RID: 4530 RVA: 0x0006A2C7 File Offset: 0x000684C7
		// (set) Token: 0x060011B3 RID: 4531 RVA: 0x0006A2CF File Offset: 0x000684CF
		public string Text { get; private set; }

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060011B4 RID: 4532 RVA: 0x0006A2D8 File Offset: 0x000684D8
		// (set) Token: 0x060011B5 RID: 4533 RVA: 0x0006A2E0 File Offset: 0x000684E0
		public string HotKeyText { get; private set; }

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060011B6 RID: 4534 RVA: 0x0006A2E9 File Offset: 0x000684E9
		// (set) Token: 0x060011B7 RID: 4535 RVA: 0x0006A2F1 File Offset: 0x000684F1
		public Button.ButtonClickedEvent onDoubleClick
		{
			get
			{
				return this._onDoubleClick;
			}
			set
			{
				this._onDoubleClick = value;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060011B8 RID: 4536 RVA: 0x0006A2FA File Offset: 0x000684FA
		// (set) Token: 0x060011B9 RID: 4537 RVA: 0x0006A302 File Offset: 0x00068502
		public Button.ButtonClickedEvent onRightClick
		{
			get
			{
				return this._onRightClick;
			}
			set
			{
				this._onRightClick = value;
			}
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x0006A30B File Offset: 0x0006850B
		public void onMouseEnter()
		{
			base.GetComponent<Button>().image.color = this.HoverColor;
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x0006A323 File Offset: 0x00068523
		public void onMouseExit()
		{
			base.GetComponent<Button>().image.color = this.NormalColor;
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x0006A33B File Offset: 0x0006853B
		public MenuItem setText(string text)
		{
			this.Text = text;
			base.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = text;
			return this;
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x0006A360 File Offset: 0x00068560
		public MenuItem setHotKeyText(string text)
		{
			this.HotKeyText = text;
			base.transform.Find("HotKeyText").GetComponent<Text>().text = text;
			return this;
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x0006A388 File Offset: 0x00068588
		public MenuItem setEnabled(bool enabled)
		{
			base.GetComponent<Button>().interactable = enabled;
			base.transform.Find("Text").GetComponent<Text>().color = (enabled ? Color.white : Color.gray);
			base.transform.Find("HotKeyText").GetComponent<Text>().color = (enabled ? Color.white : Color.gray);
			base.transform.Find("Image").GetComponent<Image>().material = (enabled ? null : this.GrayScaleMaterial);
			base.transform.Find("CheckBoxImage").GetComponent<Image>().material = (enabled ? null : this.GrayScaleMaterial);
			return this;
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x0006A440 File Offset: 0x00068640
		public MenuItem setCallback(UnityAction callback)
		{
			base.GetComponent<Button>().onClick.AddListener(callback);
			return this;
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x0006A454 File Offset: 0x00068654
		public MenuItem setIcon(Sprite icon)
		{
			Image component = base.transform.Find("Image").GetComponent<Image>();
			component.sprite = icon;
			component.gameObject.SetActive(icon != null);
			this.setChecked(this.Checked);
			return this;
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x0006A491 File Offset: 0x00068691
		public MenuItem setCheckable(bool checkable)
		{
			this.Checkable = true;
			this.setChecked(this.Checked);
			return this;
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x0006A4A8 File Offset: 0x000686A8
		public void showCheckbox(bool show)
		{
			base.transform.Find("CheckBoxImage").gameObject.SetActive(show);
			base.transform.Find("CheckBoxImage").GetComponent<Image>().enabled = this.Checkable;
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x0006A4E8 File Offset: 0x000686E8
		public MenuItem setChecked(bool check)
		{
			if (!this.Checkable)
			{
				return this;
			}
			this.Checked = check;
			base.transform.Find("CheckBoxImage").GetComponent<Image>().sprite = (this.Checked ? this.CheckedSprite : this.UncheckedSprite);
			if (this.Radio && check)
			{
				int childCount = base.gameObject.transform.parent.childCount;
				for (int i = 0; i < childCount; i++)
				{
					MenuItem component = base.gameObject.transform.parent.GetChild(i).gameObject.GetComponent<MenuItem>();
					if (component != null && component != this && component.Radio)
					{
						component.setChecked(false);
					}
				}
			}
			return this;
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x0006A5A6 File Offset: 0x000687A6
		public void toggle()
		{
			this.setChecked(!this.Checked);
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x0006A5B8 File Offset: 0x000687B8
		public virtual void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.button == PointerEventData.InputButton.Right)
			{
				this._onRightClick.Invoke();
			}
			if (eventData.button == PointerEventData.InputButton.Left && eventData.clickCount == 2)
			{
				this._onDoubleClick.Invoke();
			}
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x0006A5EA File Offset: 0x000687EA
		public MenuItem setMenuItemClicked(Action a)
		{
			this.onMenuItemClicked = a;
			return this;
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x0006A5F4 File Offset: 0x000687F4
		public void OnMenuItemClicked()
		{
			if (this.onMenuItemClicked != null)
			{
				this.onMenuItemClicked();
			}
		}

		// Token: 0x04000BC3 RID: 3011
		public Color NormalColor;

		// Token: 0x04000BC4 RID: 3012
		public Color HoverColor;

		// Token: 0x04000BC5 RID: 3013
		public Sprite CheckedSprite;

		// Token: 0x04000BC6 RID: 3014
		public Sprite UncheckedSprite;

		// Token: 0x04000BC7 RID: 3015
		public Material GrayScaleMaterial;

		// Token: 0x04000BC8 RID: 3016
		public bool Radio;

		// Token: 0x04000BC9 RID: 3017
		public bool Checkable;

		// Token: 0x04000BCD RID: 3021
		[FormerlySerializedAs("onDoubleClick")]
		[SerializeField]
		private Button.ButtonClickedEvent _onDoubleClick = new Button.ButtonClickedEvent();

		// Token: 0x04000BCE RID: 3022
		[FormerlySerializedAs("onRightClick")]
		[SerializeField]
		private Button.ButtonClickedEvent _onRightClick = new Button.ButtonClickedEvent();

		// Token: 0x04000BCF RID: 3023
		public Action onMenuItemClicked;
	}
}
