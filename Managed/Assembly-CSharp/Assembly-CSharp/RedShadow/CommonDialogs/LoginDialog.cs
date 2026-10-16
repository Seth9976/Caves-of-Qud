using System;
using UnityEngine.UI;

namespace RedShadow.CommonDialogs
{
	// Token: 0x020001FC RID: 508
	public class LoginDialog : DialogBase
	{
		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06001257 RID: 4695 RVA: 0x0006D328 File Offset: 0x0006B528
		// (set) Token: 0x06001258 RID: 4696 RVA: 0x0006D330 File Offset: 0x0006B530
		public Buttons Result { get; private set; }

		// Token: 0x06001259 RID: 4697 RVA: 0x0006D33C File Offset: 0x0006B53C
		protected override void Awake()
		{
			base.Awake();
			this.Result = Buttons.Cancel;
			this._messageText = base.transform.Find("Window/MessagePanel/Text").GetComponent<Text>();
			this._userNameInputField = base.transform.Find("Window/InputPanel/UserName").GetComponent<InputField>();
			this._passwordInputField = base.transform.Find("Window/InputPanel/Password").GetComponent<InputField>();
		}

		// Token: 0x0600125A RID: 4698 RVA: 0x0006D3A7 File Offset: 0x0006B5A7
		public void getLogin(string text, string defaultUserName, string defaultPassword, Action<string, string> callback)
		{
			this.Result = Buttons.Cancel;
			this._callback = callback;
			this.setText(text);
			this.setUserName(defaultUserName);
			this.setPassword(defaultPassword);
			base.StartCoroutine(this.show_co(0.01f));
		}

		// Token: 0x0600125B RID: 4699 RVA: 0x0006D3DF File Offset: 0x0006B5DF
		public void setText(string text)
		{
			this._messageText.text = text;
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x0006D3ED File Offset: 0x0006B5ED
		public void setUserName(string value)
		{
			this._userNameInputField.text = value;
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x0006D3FB File Offset: 0x0006B5FB
		public void setPassword(string value)
		{
			this._passwordInputField.text = value;
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x0006D409 File Offset: 0x0006B609
		public string getUserName()
		{
			return this._userNameInputField.text;
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x0006D416 File Offset: 0x0006B616
		public string getPassword()
		{
			return this._passwordInputField.text;
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x0006D423 File Offset: 0x0006B623
		public void onOk()
		{
			this.Result = Buttons.Ok;
			this.hide();
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x0006D432 File Offset: 0x0006B632
		protected override void hide()
		{
			base.hide();
			this._callback((this.Result != Buttons.Ok) ? null : this.getUserName(), (this.Result != Buttons.Ok) ? null : this.getPassword());
		}

		// Token: 0x04000C45 RID: 3141
		private InputField _userNameInputField;

		// Token: 0x04000C46 RID: 3142
		private InputField _passwordInputField;

		// Token: 0x04000C47 RID: 3143
		private Text _messageText;

		// Token: 0x04000C48 RID: 3144
		private Action<string, string> _callback;
	}
}
