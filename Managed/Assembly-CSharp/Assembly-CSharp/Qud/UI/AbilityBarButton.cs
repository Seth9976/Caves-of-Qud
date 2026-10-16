using System;
using ConsoleLib.Console;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Qud.UI
{
	// Token: 0x02000488 RID: 1160
	[ExecuteInEditMode]
	public class AbilityBarButton : MonoBehaviour
	{
		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06002CA1 RID: 11425 RVA: 0x000F145D File Offset: 0x000EF65D
		// (set) Token: 0x06002CA2 RID: 11426 RVA: 0x000F1465 File Offset: 0x000EF665
		public bool highlighted
		{
			get
			{
				return this._highlighted;
			}
			set
			{
				if (value && !this._highlighted)
				{
					this.UpdateText();
					this.highlightOverlay.SetActive(true);
				}
				else if (!value && this._highlighted)
				{
					this.highlightOverlay.SetActive(false);
				}
				this._highlighted = value;
			}
		}

		// Token: 0x06002CA3 RID: 11427 RVA: 0x000F14A4 File Offset: 0x000EF6A4
		public void UpdateText()
		{
			this.Hotkey.text = ControlManager.getCommandInputDescription("Use Ability", true, true, ControlManager.InputDeviceType.Unknown, false);
		}

		// Token: 0x06002CA4 RID: 11428 RVA: 0x000F14C0 File Offset: 0x000EF6C0
		[ExecuteInEditMode]
		public void Update()
		{
			if (this.highlightOverlay.activeInHierarchy && !this._highlighted)
			{
				this.highlightOverlay.SetActive(false);
			}
			if (!this.highlightOverlay.activeInHierarchy && this._highlighted)
			{
				this.highlightOverlay.SetActive(true);
			}
		}

		// Token: 0x06002CA5 RID: 11429 RVA: 0x000F150F File Offset: 0x000EF70F
		public void LateUpdate()
		{
			if (ControlManager.ControllerChangedThisLateUpdate && this._highlighted)
			{
				this.UpdateText();
			}
		}

		// Token: 0x06002CA6 RID: 11430 RVA: 0x000F1526 File Offset: 0x000EF726
		public void Awake()
		{
			this.highlightOverlay.SetActive(this.highlighted);
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06002CA7 RID: 11431 RVA: 0x000F1539 File Offset: 0x000EF739
		// (set) Token: 0x06002CA8 RID: 11432 RVA: 0x000F154E File Offset: 0x000EF74E
		public bool disabled
		{
			get
			{
				return !base.gameObject.GetComponent<Button>().interactable;
			}
			set
			{
				base.gameObject.GetComponent<Button>().interactable = !value;
			}
		}

		// Token: 0x06002CA9 RID: 11433 RVA: 0x000F1564 File Offset: 0x000EF764
		public void OnClick()
		{
			if (GameManager.Instance.CurrentGameView != "Stage")
			{
				return;
			}
			if (!this.disabled)
			{
				Keyboard.PushMouseEvent("Command:" + this.command, null);
			}
		}

		// Token: 0x040018D9 RID: 6361
		public GameObject highlightOverlay;

		// Token: 0x040018DA RID: 6362
		public TextMeshProUGUI Text;

		// Token: 0x040018DB RID: 6363
		public TextMeshProUGUI Hotkey;

		// Token: 0x040018DC RID: 6364
		public UIThreeColorProperties Icon;

		// Token: 0x040018DD RID: 6365
		public string command;

		// Token: 0x040018DE RID: 6366
		public bool _highlighted;
	}
}
