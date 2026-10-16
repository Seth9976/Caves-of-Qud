using System;
using ConsoleLib.Console;
using QupKit;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.QupKit.Components
{
	// Token: 0x020003E3 RID: 995
	public class ButtonKeyShortcut : MonoBehaviour
	{
		// Token: 0x0600254F RID: 9551 RVA: 0x000BDFFC File Offset: 0x000BC1FC
		public void Update()
		{
			if (this.parentButton != null && this.parentButton.interactable && this.Hotkey != global::UnityEngine.KeyCode.None && Input.GetKeyDown(this.Hotkey))
			{
				Keyboard.ClearInput(false, false);
				ControlManager.ResetInput(false, true);
				LegacyViewManager.Instance.OnCommand(this.CommandID);
			}
		}

		// Token: 0x06002550 RID: 9552 RVA: 0x000BE058 File Offset: 0x000BC258
		public void Awake()
		{
			if (!this.init)
			{
				this.init = true;
				this.parentButton = base.GetComponent<Button>();
				base.GetComponent<Button>().onClick.RemoveAllListeners();
				base.GetComponent<Button>().onClick.AddListener(delegate
				{
					if (this.parentButton.enabled)
					{
						LegacyViewManager.Instance.OnCommand(this.CommandID);
					}
				});
			}
		}

		// Token: 0x0400129D RID: 4765
		public string CommandID = string.Empty;

		// Token: 0x0400129E RID: 4766
		public global::UnityEngine.KeyCode Hotkey;

		// Token: 0x0400129F RID: 4767
		private Button parentButton;

		// Token: 0x040012A0 RID: 4768
		public bool AllowWithRewired = true;

		// Token: 0x040012A1 RID: 4769
		public bool init;
	}
}
