using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RedShadow.CommonDialogs
{
	// Token: 0x02000203 RID: 515
	public class NotificationMessage : DialogBase
	{
		// Token: 0x06001280 RID: 4736 RVA: 0x0006DC66 File Offset: 0x0006BE66
		protected override void Awake()
		{
			base.Awake();
			this._messageText = base.transform.Find("Window/MessagePanel/Text").GetComponent<Text>();
			this._icon = base.transform.Find("Window/MessagePanel/Icon").GetComponent<Image>();
		}

		// Token: 0x06001281 RID: 4737 RVA: 0x0006DCA4 File Offset: 0x0006BEA4
		public override void Update()
		{
			base.Update();
			RectTransform component = this._mainWindow.GetComponent<RectTransform>();
			if (component.position != this._endPos)
			{
				float num = (Time.time - this._moveStartTime) / 0.2f;
				if (num > 1f)
				{
					component.position = this._endPos;
				}
				else
				{
					component.position = (this._endPos - this._startPos) * num + this._startPos;
				}
			}
			if (base.IsVisible && this._startTime > 0f && Time.time > this._startTime + this.Duration)
			{
				this.hide();
			}
			while (NotificationMessage._activeMessages.Count > NotificationMessage.MaxVisible)
			{
				NotificationMessage._activeMessages[0].hide();
			}
		}

		// Token: 0x06001282 RID: 4738 RVA: 0x0006DD79 File Offset: 0x0006BF79
		public void show(string text, Sprite icon)
		{
			this.setIcon(icon);
			this.setText(text);
			base.StartCoroutine(this.show_co(0.01f));
		}

		// Token: 0x06001283 RID: 4739 RVA: 0x0006DD9D File Offset: 0x0006BF9D
		public NotificationMessage setText(string text)
		{
			this._messageText.text = text;
			if (base.IsVisible)
			{
				Canvas.ForceUpdateCanvases();
				this.adjustPositions();
			}
			return this;
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x0006DDBF File Offset: 0x0006BFBF
		public NotificationMessage setIcon(Sprite icon)
		{
			this._icon.sprite = icon;
			this._icon.gameObject.SetActive(icon != null);
			if (base.IsVisible)
			{
				Canvas.ForceUpdateCanvases();
				this.adjustPositions();
			}
			return this;
		}

		// Token: 0x06001285 RID: 4741 RVA: 0x0006DDF8 File Offset: 0x0006BFF8
		public static void clear()
		{
			while (NotificationMessage._activeMessages.Count > 0)
			{
				NotificationMessage._activeMessages[0].hide();
			}
		}

		// Token: 0x06001286 RID: 4742 RVA: 0x0006DE1C File Offset: 0x0006C01C
		protected override void show()
		{
			base.show();
			DialogBase.VisibleDialogs.Remove(this);
			this._mainWindow.transform.eulerAngles = new Vector3(0f, 0f, 0f);
			this._startTime = Time.time;
			NotificationMessage._activeMessages.Add(this);
			Canvas.ForceUpdateCanvases();
			this.adjustPositions();
		}

		// Token: 0x06001287 RID: 4743 RVA: 0x0006DE80 File Offset: 0x0006C080
		protected override void hide()
		{
			base.hide();
			NotificationMessage._activeMessages.Remove(this);
			this.adjustPositions();
		}

		// Token: 0x06001288 RID: 4744 RVA: 0x0006DE9C File Offset: 0x0006C09C
		private void adjustPositions()
		{
			RectTransform component = base.GetComponent<RectTransform>();
			Vector3[] array = new Vector3[4];
			component.GetWorldCorners(array);
			float num = 0f;
			for (int i = NotificationMessage._activeMessages.Count - 1; i >= 0; i--)
			{
				NotificationMessage notificationMessage = NotificationMessage._activeMessages[i];
				RectTransform component2 = notificationMessage._mainWindow.GetComponent<RectTransform>();
				Vector3[] array2 = new Vector3[4];
				component2.GetWorldCorners(array2);
				float num2 = array2[2].x - array2[0].x;
				float num3 = array2[2].y - array2[0].y;
				float num4;
				if (this.Area == NotificationArea.LowerLeft || this.Area == NotificationArea.UpperLeft)
				{
					num4 = array[0].x + 5f + num2 / 2f;
				}
				else
				{
					num4 = array[2].x - 5f - num2 / 2f;
				}
				float num5;
				if (this.Area == NotificationArea.LowerLeft || this.Area == NotificationArea.LowerRight)
				{
					num5 = array[0].y + 5f + num3 / 2f + num;
				}
				else
				{
					num5 = array[2].y - 5f - num3 / 2f - num;
				}
				notificationMessage._moveStartTime = Time.time;
				notificationMessage._startPos = component2.position;
				notificationMessage._endPos = new Vector2(num4, num5);
				num += num3 + 5f;
			}
		}

		// Token: 0x04000C69 RID: 3177
		public float Duration = 10f;

		// Token: 0x04000C6A RID: 3178
		public NotificationArea Area = NotificationArea.LowerRight;

		// Token: 0x04000C6B RID: 3179
		public static int MaxVisible = 5;

		// Token: 0x04000C6C RID: 3180
		private Image _icon;

		// Token: 0x04000C6D RID: 3181
		private Text _messageText;

		// Token: 0x04000C6E RID: 3182
		private static readonly List<NotificationMessage> _activeMessages = new List<NotificationMessage>();

		// Token: 0x04000C6F RID: 3183
		private float _startTime;

		// Token: 0x04000C70 RID: 3184
		private float _moveStartTime;

		// Token: 0x04000C71 RID: 3185
		private Vector3 _startPos;

		// Token: 0x04000C72 RID: 3186
		private Vector3 _endPos;
	}
}
