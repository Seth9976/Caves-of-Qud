using System;
using UnityEngine;
using UnityEngine.UI;

namespace RedShadow.CommonDialogs
{
	// Token: 0x02000201 RID: 513
	public class MessageDialog : DialogBase
	{
		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06001276 RID: 4726 RVA: 0x0006D8FF File Offset: 0x0006BAFF
		// (set) Token: 0x06001277 RID: 4727 RVA: 0x0006D907 File Offset: 0x0006BB07
		public Buttons Result { get; private set; }

		// Token: 0x06001278 RID: 4728 RVA: 0x0006D910 File Offset: 0x0006BB10
		protected override void Awake()
		{
			base.Awake();
			this.Result = Buttons.Cancel;
			this._messageText = base.transform.Find("Window/MessagePanel/Text").GetComponent<Text>();
			this._buttonPanel = base.transform.Find("Window/ButtonPanel").gameObject;
			this._icon = base.transform.Find("Window/MessagePanel/Icon").GetComponent<Image>();
		}

		// Token: 0x06001279 RID: 4729 RVA: 0x0006D97C File Offset: 0x0006BB7C
		public override void Update()
		{
			base.Update();
			if (base.isTop())
			{
				Buttons buttons = Buttons.None;
				if ((this._buttons & Buttons.Ok) == Buttons.Ok && Input.GetKeyDown(KeyCode.Return))
				{
					buttons = Buttons.Ok;
				}
				if ((this._buttons & Buttons.Cancel) == Buttons.Cancel && Input.GetKeyDown(KeyCode.C))
				{
					buttons = Buttons.Cancel;
				}
				if ((this._buttons & Buttons.Yes) == Buttons.Yes && Input.GetKeyDown(KeyCode.Y))
				{
					buttons = Buttons.Yes;
				}
				if ((this._buttons & Buttons.No) == Buttons.No && Input.GetKeyDown(KeyCode.N))
				{
					buttons = Buttons.No;
				}
				if ((this._buttons & Buttons.Abort) == Buttons.Abort && Input.GetKeyDown(KeyCode.A))
				{
					buttons = Buttons.Abort;
				}
				if ((this._buttons & Buttons.Retry) == Buttons.Retry && Input.GetKeyDown(KeyCode.R))
				{
					buttons = Buttons.Retry;
				}
				if ((this._buttons & Buttons.Ignore) == Buttons.Ignore && Input.GetKeyDown(KeyCode.I))
				{
					buttons = Buttons.Ignore;
				}
				if (buttons != Buttons.None)
				{
					this.Result = buttons;
					this.hide();
				}
			}
		}

		// Token: 0x0600127A RID: 4730 RVA: 0x0006DA5C File Offset: 0x0006BC5C
		public void show(string text, Action<Buttons> callback, Sprite icon, Buttons buttons = Buttons.Ok)
		{
			this.Result = Buttons.Cancel;
			this._callback = callback;
			this._buttons = buttons;
			this.setText(text);
			this.setIcon(icon);
			foreach (object obj in this._buttonPanel.transform)
			{
				global::UnityEngine.Object.Destroy(((Transform)obj).gameObject);
			}
			if ((buttons & Buttons.Ok) == Buttons.Ok)
			{
				this.addButton("Ok", Buttons.Ok);
			}
			if ((buttons & Buttons.Cancel) == Buttons.Cancel)
			{
				this.addButton("Cancel", Buttons.Cancel);
			}
			if ((buttons & Buttons.Yes) == Buttons.Yes)
			{
				this.addButton("Yes", Buttons.Yes);
			}
			if ((buttons & Buttons.No) == Buttons.No)
			{
				this.addButton("No", Buttons.No);
			}
			if ((buttons & Buttons.Abort) == Buttons.Abort)
			{
				this.addButton("Abort", Buttons.Abort);
			}
			if ((buttons & Buttons.Retry) == Buttons.Retry)
			{
				this.addButton("Retry", Buttons.Retry);
			}
			if ((buttons & Buttons.Ignore) == Buttons.Ignore)
			{
				this.addButton("Ignore", Buttons.Ignore);
			}
			base.StartCoroutine(this.show_co(0.01f));
		}

		// Token: 0x0600127B RID: 4731 RVA: 0x0006DB98 File Offset: 0x0006BD98
		public MessageDialog setText(string text)
		{
			this._messageText.text = text;
			return this;
		}

		// Token: 0x0600127C RID: 4732 RVA: 0x0006DBA7 File Offset: 0x0006BDA7
		public MessageDialog setIcon(Sprite icon)
		{
			this._icon.sprite = icon;
			this._icon.gameObject.SetActive(icon != null);
			return this;
		}

		// Token: 0x0600127D RID: 4733 RVA: 0x0006DBD0 File Offset: 0x0006BDD0
		public Button addButton(string text, Buttons result)
		{
			Button button = global::UnityEngine.Object.Instantiate<Button>(this.ButtonPrefab);
			button.transform.SetParent(this._buttonPanel.transform, false);
			button.transform.Find("Text").GetComponent<Text>().text = text;
			button.onClick.AddListener(delegate
			{
				this.Result = result;
				this.hide();
			});
			return button;
		}

		// Token: 0x0600127E RID: 4734 RVA: 0x0006DC45 File Offset: 0x0006BE45
		protected override void hide()
		{
			base.hide();
			this._callback(this.Result);
		}

		// Token: 0x04000C5A RID: 3162
		public Button ButtonPrefab;

		// Token: 0x04000C5B RID: 3163
		public Sprite InfoSprite;

		// Token: 0x04000C5C RID: 3164
		public Sprite QuestionSprite;

		// Token: 0x04000C5D RID: 3165
		public Sprite ErrorSprite;

		// Token: 0x04000C5E RID: 3166
		public Sprite WarningSprite;

		// Token: 0x04000C5F RID: 3167
		private GameObject _buttonPanel;

		// Token: 0x04000C60 RID: 3168
		private Image _icon;

		// Token: 0x04000C61 RID: 3169
		private Text _messageText;

		// Token: 0x04000C62 RID: 3170
		private Action<Buttons> _callback;

		// Token: 0x04000C63 RID: 3171
		private Buttons _buttons;
	}
}
