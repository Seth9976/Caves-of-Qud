using System;
using UnityEngine;
using UnityEngine.UI;

namespace Qud.UI
{
	// Token: 0x0200048B RID: 1163
	public class ConsoleWindow : SingletonWindowBase<ConsoleWindow>
	{
		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06002CB6 RID: 11446 RVA: 0x000F1BBA File Offset: 0x000EFDBA
		public static Text textField
		{
			get
			{
				return SingletonWindowBase<ConsoleWindow>.instance._textField;
			}
		}

		// Token: 0x06002CB7 RID: 11447 RVA: 0x000F1BC6 File Offset: 0x000EFDC6
		public void ScrollToTop()
		{
			this._scrollRect.normalizedPosition = new Vector2(0f, 1f);
		}

		// Token: 0x06002CB8 RID: 11448 RVA: 0x000F1BE2 File Offset: 0x000EFDE2
		public void ScrollToBottom()
		{
			LayoutRebuilder.ForceRebuildLayoutImmediate(this._scrollRect.gameObject.GetComponent<RectTransform>());
			this._scrollRect.normalizedPosition = new Vector2(0f, 0f);
		}

		// Token: 0x06002CB9 RID: 11449 RVA: 0x000F1C13 File Offset: 0x000EFE13
		public void OnServerInitialized()
		{
		}

		// Token: 0x06002CBA RID: 11450 RVA: 0x000F1C18 File Offset: 0x000EFE18
		public void Update()
		{
			if (DebugConsole.dirty)
			{
				this.ScrollToBottom();
				DebugConsole.dirty = false;
			}
			if (this.enterAllowed && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
			{
				DebugConsole.Execute(this._inputField.text);
				this._inputField.text = "";
				this.enterAllowed = false;
				this._inputField.ActivateInputField();
				return;
			}
			this.enterAllowed = this._inputField.isFocused;
		}

		// Token: 0x06002CBB RID: 11451 RVA: 0x000F1C99 File Offset: 0x000EFE99
		public override void Show()
		{
			base.Show();
			this._inputField.ActivateInputField();
		}

		// Token: 0x040018F5 RID: 6389
		public ScrollRect _scrollRect;

		// Token: 0x040018F6 RID: 6390
		public InputField _inputField;

		// Token: 0x040018F7 RID: 6391
		public Text _textField;

		// Token: 0x040018F8 RID: 6392
		public RectTransform panel;

		// Token: 0x040018F9 RID: 6393
		private bool enterAllowed;
	}
}
