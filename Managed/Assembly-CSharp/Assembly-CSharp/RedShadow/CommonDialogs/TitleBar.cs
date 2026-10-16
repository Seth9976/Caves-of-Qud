using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace RedShadow.CommonDialogs
{
	// Token: 0x02000206 RID: 518
	public class TitleBar : MonoBehaviour
	{
		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06001298 RID: 4760 RVA: 0x0006E224 File Offset: 0x0006C424
		// (set) Token: 0x06001299 RID: 4761 RVA: 0x0006E22C File Offset: 0x0006C42C
		public Button.ButtonClickedEvent onClose
		{
			get
			{
				return this._onClose;
			}
			set
			{
				this._onClose = value;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x0600129A RID: 4762 RVA: 0x0006E235 File Offset: 0x0006C435
		// (set) Token: 0x0600129B RID: 4763 RVA: 0x0006E23D File Offset: 0x0006C43D
		public bool Maximized { get; private set; }

		// Token: 0x0600129C RID: 4764 RVA: 0x0006E248 File Offset: 0x0006C448
		protected void Awake()
		{
			this._titleText = base.transform.Find("Panel/Text").GetComponent<Text>();
			this._closeButton = base.transform.Find("Panel/CloseButton").GetComponent<Button>();
			this._restoreButton = base.transform.Find("Panel/RestoreButton").GetComponent<Button>();
			this._maximizeButton = base.transform.Find("Panel/MaximizeButton").GetComponent<Button>();
			this._dragPanel = base.transform.Find("Panel/Text").GetComponent<DragPanel>();
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x0006E2DC File Offset: 0x0006C4DC
		public TitleBar showSizeButtons(bool show)
		{
			base.transform.Find("Panel/MinimizeButton").gameObject.SetActive(show);
			this._restoreButton.gameObject.SetActive(show && this.Maximized);
			this._maximizeButton.gameObject.SetActive(show && !this.Maximized);
			return this;
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x0006E340 File Offset: 0x0006C540
		public TitleBar setTitleText(string text)
		{
			this._titleText.text = text;
			return this;
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x0006E34F File Offset: 0x0006C54F
		public TitleBar showCloseButton(bool show)
		{
			this._closeButton.gameObject.SetActive(show);
			return this;
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x0006E363 File Offset: 0x0006C563
		public TitleBar setAllowDrag(bool draggable)
		{
			this._dragPanel.gameObject.SetActive(draggable);
			return this;
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x0006E378 File Offset: 0x0006C578
		public void onMinimize()
		{
			this._restoreRect = new Rect(this._dragPanel.TargetPanel.localPosition, this._dragPanel.TargetPanel.sizeDelta);
			this._dragPanel.TargetPanel.localPosition = Vector2.zero;
			this._dragPanel.TargetPanel.sizeDelta = this.MinSize;
			this._dragPanel.clampToWindow();
			this._restoreButton.gameObject.SetActive(true);
			this._maximizeButton.gameObject.SetActive(false);
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x0006E414 File Offset: 0x0006C614
		public void onRestore()
		{
			this._dragPanel.TargetPanel.localPosition = this._restoreRect.position;
			this._dragPanel.TargetPanel.sizeDelta = this._restoreRect.size;
			this._dragPanel.clampToWindow();
			this._restoreButton.gameObject.SetActive(false);
			this._maximizeButton.gameObject.SetActive(true);
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x0006E48C File Offset: 0x0006C68C
		public void onMaximize()
		{
			this._restoreRect = new Rect(this._dragPanel.TargetPanel.localPosition, this._dragPanel.TargetPanel.sizeDelta);
			RectTransform component = this._dragPanel.TargetPanel.parent.GetComponent<RectTransform>();
			this._dragPanel.TargetPanel.localPosition = component.localPosition;
			this._dragPanel.TargetPanel.sizeDelta = component.rect.size;
			this._dragPanel.clampToWindow();
			this._restoreButton.gameObject.SetActive(true);
			this._maximizeButton.gameObject.SetActive(false);
			this.Maximized = true;
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x0006E547 File Offset: 0x0006C747
		public void onCloseButton()
		{
			this._onClose.Invoke();
		}

		// Token: 0x04000C7B RID: 3195
		public Vector2 MinSize;

		// Token: 0x04000C7D RID: 3197
		private Text _titleText;

		// Token: 0x04000C7E RID: 3198
		private Button _closeButton;

		// Token: 0x04000C7F RID: 3199
		private Button _maximizeButton;

		// Token: 0x04000C80 RID: 3200
		private Button _restoreButton;

		// Token: 0x04000C81 RID: 3201
		private Rect _restoreRect;

		// Token: 0x04000C82 RID: 3202
		private DragPanel _dragPanel;

		// Token: 0x04000C83 RID: 3203
		[FormerlySerializedAs("onClose")]
		[SerializeField]
		private Button.ButtonClickedEvent _onClose = new Button.ButtonClickedEvent();
	}
}
