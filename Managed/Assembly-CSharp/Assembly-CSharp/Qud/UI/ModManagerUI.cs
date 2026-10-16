using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ConsoleLib.Console;
using Genkit;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using XRL;
using XRL.UI;

namespace Qud.UI
{
	// Token: 0x02000496 RID: 1174
	[UIView("ModManager", false, false, false, null, null, false, 0, false, NavCategory = "Popup", UICanvas = "ModManager", UICanvasHost = 1, OverlayMode = 1)]
	public class ModManagerUI : SingletonWindowBase<ModManagerUI>
	{
		// Token: 0x06002D07 RID: 11527 RVA: 0x000F2B64 File Offset: 0x000F0D64
		public async Task<bool> ShowMenuAsync()
		{
			this.menuclosed.TrySetCanceled();
			this.menuclosed = new TaskCompletionSource<bool>();
			await The.UiContext;
			if (ModManager.IsScriptingUndetermined())
			{
				await ModManagerUI.PromptScripting();
				await Task.Delay(250);
			}
			GameManager.Instance.PushGameView("ModManager", true);
			return await this.menuclosed.Task;
		}

		// Token: 0x06002D08 RID: 11528 RVA: 0x000F2BA7 File Offset: 0x000F0DA7
		public override void Hide()
		{
			OldMainMenuView instance = OldMainMenuView.instance;
			if (instance != null)
			{
				instance.DisplayAlert();
			}
			this.nextHideCallback.Invoke();
			this.nextHideCallback.RemoveAllListeners();
			base.Hide();
			base.gameObject.SetActive(false);
		}

		// Token: 0x06002D09 RID: 11529 RVA: 0x000F2BE1 File Offset: 0x000F0DE1
		public override void Show()
		{
			if (CapabilityManager.CurrentPlatformClassification() == CapabilityManager.PlatformClassification.Console)
			{
				this.SetBackButtonText("Back");
			}
			base.gameObject.SetActive(true);
			base.Show();
			this.SetupMods();
			this.ms1.Reselect(0);
		}

		// Token: 0x06002D0A RID: 11530 RVA: 0x000F2C1C File Offset: 0x000F0E1C
		public void SetBackButtonText(string text)
		{
			QudMenuItem qudMenuItem = this.ms1.bottomContextOptions[0];
			if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
			{
				qudMenuItem.text = string.Concat(new string[]
				{
					"{{W|[",
					ControlManager.getCommandInputDescription("Cancel", true, false, ControlManager.InputDeviceType.Unknown, false),
					"]}} {{y|",
					text,
					"}}"
				});
			}
			else
			{
				qudMenuItem.text = "{{W|[Esc]}} {{y|" + text + "}}";
			}
			this.ms1.bottomContextOptions[0] = qudMenuItem;
		}

		// Token: 0x06002D0B RID: 11531 RVA: 0x000F2CB0 File Offset: 0x000F0EB0
		public void SetupMods()
		{
			if (this.ms1 != null)
			{
				List<ModInfo> list = new List<ModInfo>(ModManager.Mods);
				list.Sort(new ModManagerUI.ModManagerSorter());
				this.ms1.mods = list;
				this.ms1.isCurrentWindow = new Func<bool>(this.IsCurrentWindow);
				this._initialHash = this.GetModsHash();
			}
		}

		// Token: 0x06002D0C RID: 11532 RVA: 0x000F2D10 File Offset: 0x000F0F10
		public bool IsCurrentWindow()
		{
			return GameManager.Instance.CurrentGameView == "ModManager" && base.raycaster.enabled;
		}

		// Token: 0x06002D0D RID: 11533 RVA: 0x000F2D38 File Offset: 0x000F0F38
		public int GetModsHash()
		{
			uint num = 2166136261U;
			foreach (ModInfo modInfo in ModManager.Mods)
			{
				if (modInfo.IsEnabled)
				{
					num = Hash.FNV1A32(modInfo.ID, num);
				}
			}
			return (int)num;
		}

		// Token: 0x06002D0E RID: 11534 RVA: 0x000F2DA0 File Offset: 0x000F0FA0
		public static async Task PromptScripting()
		{
			QudMenuItem qudMenuItem = await Popup.NewPopupMessageAsync("Scripting mods are currently disabled. Do you want to enable them?\n\nScripts may contain malicious code with unfettered access to your computer.\nYou can change this option again later in Options -> Mods -> Allow scripting mods.", PopupMessage.YesNoButton, null, "{{W|Unapproved Scripting}}", null, 0, null, null, null, true, true, false, default(CancellationToken), false, "", null, null, null);
			string text = ((qudMenuItem.command == "Yes") ? "Yes" : "No");
			Options.SetOption("OptionAllowCSMods", text);
			if (qudMenuItem.command == "Yes")
			{
				GameManager.Restart();
			}
		}

		// Token: 0x06002D0F RID: 11535 RVA: 0x000F2DDB File Offset: 0x000F0FDB
		public override void Init()
		{
			base.Init();
		}

		// Token: 0x06002D10 RID: 11536 RVA: 0x000F2DE4 File Offset: 0x000F0FE4
		public void OnSelect(ModInfo modInfo)
		{
			this.SelectedModTitle.SetText("{{W|" + modInfo.DisplayTitle + "}}");
			if (string.IsNullOrEmpty(modInfo.Manifest.Author))
			{
				this.SelectedModAuthor.gameObject.SetActive(false);
				this.SelectedModAuthorSpacer.gameObject.SetActive(false);
			}
			else
			{
				this.SelectedModAuthor.gameObject.SetActive(true);
				this.SelectedModAuthorSpacer.gameObject.SetActive(true);
				this.SelectedModAuthor.SetText("{{C|by " + modInfo.Manifest.Author + "}}");
			}
			this.SelectedModDescription.SetText("{{c|" + modInfo.Manifest.Description + "}}");
			this.SelectedModImage.image.sprite = modInfo.GetSprite();
		}

		// Token: 0x06002D11 RID: 11537 RVA: 0x000F2ECC File Offset: 0x000F10CC
		public void Awake()
		{
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06002D12 RID: 11538 RVA: 0x000F2ECE File Offset: 0x000F10CE
		private Dictionary<bool, string> shouldEnableTexts
		{
			get
			{
				if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
				{
					return this._shouldEnableTextsJoystick;
				}
				return this._shouldEnableTexts;
			}
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06002D13 RID: 11539 RVA: 0x000F2EE8 File Offset: 0x000F10E8
		private Dictionary<ModState, string> spaceButtonTexts
		{
			get
			{
				if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
				{
					return new Dictionary<ModState, string>
					{
						{
							ModState.Disabled,
							"{{W|[" + ControlManager.getCommandInputDescription("Accept", true, false, ControlManager.InputDeviceType.Unknown, false) + "]}} {{y|Enable mod}}"
						},
						{
							ModState.MissingDependency,
							"{{W|[" + ControlManager.getCommandInputDescription("Accept", true, false, ControlManager.InputDeviceType.Unknown, false) + "]}} {{y|View dependencies}}"
						},
						{
							ModState.Enabled,
							"{{W|[" + ControlManager.getCommandInputDescription("Accept", true, false, ControlManager.InputDeviceType.Unknown, false) + "]}} {{y|Disable mod}}"
						},
						{
							ModState.Failed,
							"{{W|[" + ControlManager.getCommandInputDescription("Accept", true, false, ControlManager.InputDeviceType.Unknown, false) + "]}} {{y|Recompile mod}}"
						}
					};
				}
				return this._spaceButtonTexts;
			}
		}

		// Token: 0x06002D14 RID: 11540 RVA: 0x000F2F9C File Offset: 0x000F119C
		public void Update()
		{
			bool flag = this.ms1.mods.FindCount<ModInfo>((ModInfo i) => i.IsEnabled) < this.ms1.mods.Count / 2;
			int num = this.ms1.bottomContextOptions.FindIndex((QudMenuItem i) => i.command == "ToggleAll");
			if (this.ms1.bottomContextOptions[num].text != this.shouldEnableTexts[flag])
			{
				QudMenuItem qudMenuItem = this.ms1.bottomContextOptions[num];
				qudMenuItem.text = this.shouldEnableTexts[flag];
				this.ms1.bottomContextOptions[num] = qudMenuItem;
			}
			int num2 = this.ms1.bottomContextOptions.FindIndex((QudMenuItem i) => i.command == "");
			string text = "{{W|[space]}} {{y|press button}}";
			if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
			{
				text = "{{W|[" + ControlManager.getCommandInputDescription("Accept", true, false, ControlManager.InputDeviceType.Unknown, false) + "]}} {{y|press button}}";
			}
			if (this.ms1.selectedOption < this.ms1.mods.Count)
			{
				text = this.spaceButtonTexts[this.ms1.mods[this.ms1.selectedOption].State];
			}
			if (this.ms1.bottomContextOptions[num2].text != text)
			{
				QudMenuItem qudMenuItem2 = this.ms1.bottomContextOptions[num2];
				qudMenuItem2.text = text;
				this.ms1.bottomContextOptions[num2] = qudMenuItem2;
			}
			if (this.ms1.menuBottomContext.GetComponent<RectTransform>().rect.width < this.ms1.menuBottomContext.GetComponent<LayoutGroup>().preferredWidth)
			{
				this.ms1.menuBottomContext.buttons[num2].gameObject.SetActive(false);
			}
		}

		// Token: 0x06002D15 RID: 11541 RVA: 0x000F31C8 File Offset: 0x000F13C8
		public void OnCancel()
		{
			if (this.GetModsHash() != this._initialHash)
			{
				Popup.WaitNewPopupMessage("Your mod configuration has changed, do you want to apply your changes and restart the game?", PopupMessage.YesNoButton, delegate(QudMenuItem i)
				{
					if (i.command == "Yes")
					{
						ModManager.WriteModSettings();
						GameManager.Restart();
					}
					else
					{
						ModManager.ReadModSettings(true);
						this.SetupMods();
					}
					GameManager.Instance.RemoveGameView("ModManager", false);
					this.menuclosed.TrySetResult(true);
				}, null, null, null, 0, null, null, null, true, true, null, null, false);
				return;
			}
			GameManager.Instance.RemoveGameView("ModManager", false);
			this.menuclosed.TrySetResult(true);
		}

		// Token: 0x06002D16 RID: 11542 RVA: 0x000F3228 File Offset: 0x000F1428
		public void OnActivateContext(QudMenuItem data)
		{
			string text = "Activate Context: ";
			QudMenuItem qudMenuItem = data;
			Debug.Log(text + qudMenuItem.ToString());
			if (data.command == "ToggleAll")
			{
				int num = this.ms1.mods.FindCount<ModInfo>((ModInfo i) => i.IsEnabled);
				bool shouldEnable = num < this.ms1.mods.Count / 2;
				this.ms1.mods.ForEach(delegate(ModInfo i)
				{
					i.IsEnabled = shouldEnable;
				});
				return;
			}
			if (data.command == "Undo")
			{
				ModManager.ReadModSettings(true);
				this.SetupMods();
				return;
			}
			if (data.command == "Reload" && this.GetModsHash() != this._initialHash)
			{
				ModManager.WriteModSettings();
				GameManager.Restart();
			}
		}

		// Token: 0x04001921 RID: 6433
		public ModScrollerOne ms1;

		// Token: 0x04001922 RID: 6434
		public UITextSkin SelectedModTitle;

		// Token: 0x04001923 RID: 6435
		public GameObject SelectedModAuthorSpacer;

		// Token: 0x04001924 RID: 6436
		public UITextSkin SelectedModAuthor;

		// Token: 0x04001925 RID: 6437
		public UITextSkin SelectedModDescription;

		// Token: 0x04001926 RID: 6438
		public ImageTinyFrame SelectedModImage;

		// Token: 0x04001927 RID: 6439
		public UnityEvent nextHideCallback;

		// Token: 0x04001928 RID: 6440
		private TaskCompletionSource<bool> menuclosed = new TaskCompletionSource<bool>();

		// Token: 0x04001929 RID: 6441
		private int _initialHash;

		// Token: 0x0400192A RID: 6442
		private Dictionary<bool, string> _shouldEnableTexts = new Dictionary<bool, string>
		{
			{ false, "{{W|[v]}} {{y|Disable all}}" },
			{ true, "{{W|[v]}} {{y|Enable all}}" }
		};

		// Token: 0x0400192B RID: 6443
		private Dictionary<bool, string> _shouldEnableTextsJoystick = new Dictionary<bool, string>
		{
			{ false, "{{y|Disable all}}" },
			{ true, "{{y|Enable all}}" }
		};

		// Token: 0x0400192C RID: 6444
		private Dictionary<ModState, string> _spaceButtonTexts = new Dictionary<ModState, string>
		{
			{
				ModState.Disabled,
				"{{W|[space]}} {{y|Enable mod}}"
			},
			{
				ModState.MissingDependency,
				"{{W|[space]}} {{y|View dependencies}}"
			},
			{
				ModState.Enabled,
				"{{W|[space]}} {{y|Disable mod}}"
			},
			{
				ModState.Failed,
				"{{W|[space]}} {{y|Recompile mod}}"
			}
		};

		// Token: 0x020018FD RID: 6397
		public class ModManagerSorter : Comparer<ModInfo>
		{
			// Token: 0x0600E1FB RID: 57851 RVA: 0x005210D2 File Offset: 0x0051F2D2
			public override int Compare(ModInfo a, ModInfo b)
			{
				return ConsoleLib.Console.ColorUtility.CompareExceptFormattingNoCase(a.DisplayTitle, b.DisplayTitle);
			}
		}
	}
}
