using System;
using ConsoleLib.Console;
using QupKit;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.QupKit.Components
{
	// Token: 0x020003E4 RID: 996
	public class HotkeyCommand : MonoBehaviour
	{
		// Token: 0x06002553 RID: 9555 RVA: 0x000BE0E5 File Offset: 0x000BC2E5
		public void Awake()
		{
			if (this.parentControl == null)
			{
				this.parentControl = base.GetComponent<Button>();
			}
		}

		// Token: 0x06002554 RID: 9556 RVA: 0x000BE104 File Offset: 0x000BC304
		public void Update()
		{
			if ((this.parentControl == null || this.parentControl.interactable) && this.Hotkey != global::UnityEngine.KeyCode.None && Input.GetKeyDown(this.Hotkey))
			{
				if (this.HotkeyCapitalized != (GameManager.capslock || Input.GetKey(global::UnityEngine.KeyCode.LeftShift) || Input.GetKey(global::UnityEngine.KeyCode.RightShift)))
				{
					return;
				}
				Keyboard.ClearInput(false, false);
				LegacyViewManager.Instance.OnCommand(this.CommandID);
			}
		}

		// Token: 0x040012A2 RID: 4770
		public string CommandID = "";

		// Token: 0x040012A3 RID: 4771
		public global::UnityEngine.KeyCode Hotkey;

		// Token: 0x040012A4 RID: 4772
		public bool HotkeyCapitalized;

		// Token: 0x040012A5 RID: 4773
		private Selectable parentControl;
	}
}
