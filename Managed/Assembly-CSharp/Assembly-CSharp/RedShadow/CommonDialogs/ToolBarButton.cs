using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace RedShadow.CommonDialogs
{
	// Token: 0x020001F3 RID: 499
	public class ToolBarButton : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
	{
		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060011D1 RID: 4561 RVA: 0x0006A90F File Offset: 0x00068B0F
		// (set) Token: 0x060011D2 RID: 4562 RVA: 0x0006A917 File Offset: 0x00068B17
		public bool Checkable { get; private set; }

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060011D3 RID: 4563 RVA: 0x0006A920 File Offset: 0x00068B20
		// (set) Token: 0x060011D4 RID: 4564 RVA: 0x0006A928 File Offset: 0x00068B28
		public bool Checked { get; private set; }

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x060011D5 RID: 4565 RVA: 0x0006A931 File Offset: 0x00068B31
		// (set) Token: 0x060011D6 RID: 4566 RVA: 0x0006A939 File Offset: 0x00068B39
		public string Text { get; private set; }

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x060011D7 RID: 4567 RVA: 0x0006A942 File Offset: 0x00068B42
		// (set) Token: 0x060011D8 RID: 4568 RVA: 0x0006A94A File Offset: 0x00068B4A
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

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x060011D9 RID: 4569 RVA: 0x0006A953 File Offset: 0x00068B53
		// (set) Token: 0x060011DA RID: 4570 RVA: 0x0006A95B File Offset: 0x00068B5B
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

		// Token: 0x060011DB RID: 4571 RVA: 0x0006A964 File Offset: 0x00068B64
		public ToolBarButton setText(string text)
		{
			this.Text = text;
			return this;
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x0006A96E File Offset: 0x00068B6E
		public ToolBarButton setEnabled(bool enabled)
		{
			base.GetComponent<Button>().interactable = enabled;
			base.transform.Find("Image").GetComponent<Image>().material = (enabled ? null : this.GrayScaleMaterial);
			return this;
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x0006A9A3 File Offset: 0x00068BA3
		public ToolBarButton setCallback(UnityAction callback)
		{
			base.GetComponent<Button>().onClick.AddListener(callback);
			return this;
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x0006A9B7 File Offset: 0x00068BB7
		public ToolBarButton setIcon(Sprite icon)
		{
			base.transform.Find("Image").GetComponent<Image>().sprite = icon;
			this.setChecked(this.Checked);
			return this;
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x0006A9E2 File Offset: 0x00068BE2
		public ToolBarButton setCheckable(bool checkable)
		{
			this.Checkable = true;
			this.setChecked(this.Checked);
			return this;
		}

		// Token: 0x060011E0 RID: 4576 RVA: 0x0006A9F9 File Offset: 0x00068BF9
		public ToolBarButton setChecked(bool check)
		{
			if (!this.Checkable)
			{
				return this;
			}
			this.Checked = check;
			base.GetComponent<Button>().image.color = (this.Checked ? this.CheckedColor : this.NormalColor);
			return this;
		}

		// Token: 0x060011E1 RID: 4577 RVA: 0x0006AA33 File Offset: 0x00068C33
		public void toggle()
		{
			this.setChecked(!this.Checked);
		}

		// Token: 0x060011E2 RID: 4578 RVA: 0x0006AA45 File Offset: 0x00068C45
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

		// Token: 0x04000BDC RID: 3036
		public Color NormalColor;

		// Token: 0x04000BDD RID: 3037
		public Color CheckedColor;

		// Token: 0x04000BDE RID: 3038
		public Material GrayScaleMaterial;

		// Token: 0x04000BE2 RID: 3042
		[FormerlySerializedAs("onDoubleClick")]
		[SerializeField]
		private Button.ButtonClickedEvent _onDoubleClick = new Button.ButtonClickedEvent();

		// Token: 0x04000BE3 RID: 3043
		[FormerlySerializedAs("onRightClick")]
		[SerializeField]
		private Button.ButtonClickedEvent _onRightClick = new Button.ButtonClickedEvent();
	}
}
