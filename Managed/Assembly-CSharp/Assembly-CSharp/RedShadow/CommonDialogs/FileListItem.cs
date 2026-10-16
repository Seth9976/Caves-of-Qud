using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace RedShadow.CommonDialogs
{
	// Token: 0x020001ED RID: 493
	public class FileListItem : MonoBehaviour, IPointerClickHandler, IEventSystemHandler, IPointerEnterHandler, IPointerExitHandler
	{
		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x0600119B RID: 4507 RVA: 0x0006A057 File Offset: 0x00068257
		// (set) Token: 0x0600119C RID: 4508 RVA: 0x0006A05F File Offset: 0x0006825F
		public bool Highlighted { get; private set; }

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x0600119D RID: 4509 RVA: 0x0006A068 File Offset: 0x00068268
		// (set) Token: 0x0600119E RID: 4510 RVA: 0x0006A070 File Offset: 0x00068270
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

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x0600119F RID: 4511 RVA: 0x0006A079 File Offset: 0x00068279
		// (set) Token: 0x060011A0 RID: 4512 RVA: 0x0006A081 File Offset: 0x00068281
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

		// Token: 0x060011A1 RID: 4513 RVA: 0x0006A08C File Offset: 0x0006828C
		public void setHighlighted(bool highlighted)
		{
			if (highlighted == this.Highlighted)
			{
				return;
			}
			Button component = base.gameObject.GetComponent<Button>();
			this.Highlighted = highlighted;
			if (this.Highlighted)
			{
				component.image.color = this.HighlightedColor;
			}
			else
			{
				component.image.color = this.NormalColor;
			}
			if (highlighted)
			{
				int childCount = base.transform.parent.childCount;
				for (int i = 0; i < childCount; i++)
				{
					FileListItem component2 = base.gameObject.transform.parent.GetChild(i).gameObject.GetComponent<FileListItem>();
					if (component2 != null && component2 != this)
					{
						component2.setHighlighted(false);
					}
				}
			}
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x0006A13C File Offset: 0x0006833C
		public void toggle()
		{
			this.setHighlighted(!this.Highlighted);
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x0006A14D File Offset: 0x0006834D
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

		// Token: 0x060011A4 RID: 4516 RVA: 0x0006A17F File Offset: 0x0006837F
		public void OnPointerEnter(PointerEventData eventData)
		{
			base.gameObject.GetComponent<Button>().image.color = (this.Highlighted ? this.HighlightedColor : this.HoverColor);
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x0006A1AC File Offset: 0x000683AC
		public void OnPointerExit(PointerEventData eventData)
		{
			base.gameObject.GetComponent<Button>().image.color = (this.Highlighted ? this.HighlightedColor : this.NormalColor);
		}

		// Token: 0x04000BBA RID: 3002
		public Color NormalColor;

		// Token: 0x04000BBB RID: 3003
		public Color HoverColor;

		// Token: 0x04000BBC RID: 3004
		public Color HighlightedColor;

		// Token: 0x04000BBE RID: 3006
		public string Filename;

		// Token: 0x04000BBF RID: 3007
		[FormerlySerializedAs("onDoubleClick")]
		[SerializeField]
		private Button.ButtonClickedEvent _onDoubleClick = new Button.ButtonClickedEvent();

		// Token: 0x04000BC0 RID: 3008
		[FormerlySerializedAs("onRightClick")]
		[SerializeField]
		private Button.ButtonClickedEvent _onRightClick = new Button.ButtonClickedEvent();
	}
}
