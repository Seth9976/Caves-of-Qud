using System;
using Qud.UI;
using XRL;
using XRL.UI;
using XRL.World;
using XRL.World.Capabilities;

namespace JoppaTutorial
{
	// Token: 0x020003ED RID: 1005
	public class BattleRemains : TutorialStep
	{
		// Token: 0x060025A8 RID: 9640 RVA: 0x000C01D1 File Offset: 0x000BE3D1
		public override bool BeforePlayerEnterCell(Cell cell)
		{
			return base.ConstrainToCurrentZone(cell);
		}

		// Token: 0x060025A9 RID: 9641 RVA: 0x000C01DA File Offset: 0x000BE3DA
		public override bool AllowInventoryInteract(GameObject go)
		{
			if (this.step == 0 && this.showingTooltip < 3 && go == this.battleAxe)
			{
				Popup.Show("You should view the tooltip before equipping the axe.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				return false;
			}
			return true;
		}

		// Token: 0x060025AA RID: 9642 RVA: 0x000C0210 File Offset: 0x000BE410
		public override bool AllowSelectedPopupCommand(string title, QudMenuItem command)
		{
			if (string.IsNullOrEmpty(title))
			{
				return true;
			}
			if (this.step != 0 || !(title == "InventoryActionMenu:" + this.battleAxe.ID))
			{
				return true;
			}
			if (this.battleAxe.InInventory != The.Player)
			{
				return command.simpleText == "get" || command.simpleText == "equip (auto)" || command.command == "Cancel";
			}
			if (this.showingTooltip < 3)
			{
				if (command.command == "Cancel")
				{
					return true;
				}
			}
			else
			{
				if (command.simpleText == "get")
				{
					return true;
				}
				if (command.simpleText == "equip (auto)")
				{
					return true;
				}
				if (command.command == "Cancel")
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060025AB RID: 9643 RVA: 0x000C0302 File Offset: 0x000BE502
		public override bool StartingLineTooltip(GameObject go, GameObject compareGo)
		{
			if (go == this.battleAxe && this.showingTooltip == 0)
			{
				this.showingTooltip = 1;
			}
			return true;
		}

		// Token: 0x060025AC RID: 9644 RVA: 0x000C031D File Offset: 0x000BE51D
		public override bool AllowTooltipUpdate()
		{
			return this.showingTooltip == 0 || this.showingTooltip > 3;
		}

		// Token: 0x060025AD RID: 9645 RVA: 0x000C0334 File Offset: 0x000BE534
		public override void LateUpdate()
		{
			this.manager.ClearHighlight();
			GameObject player = The.Player;
			if (((player != null) ? player.CurrentCell : null) == null)
			{
				this.manager.Highlight(null, null, null, 64f, 64f, 0f, "big");
				return;
			}
			if (this.battleAxe == null)
			{
				this.battleAxe = The.Player.CurrentCell.ParentZone.FindObject("TutorialBattleAxe");
				if (this.battleAxe == null)
				{
					this.battleAxe = The.Player.Inventory.FindObjectByBlueprint("TutorialBattleAxe");
				}
				GameObject gameObject = this.battleAxe;
				if (gameObject != null)
				{
					gameObject.RequireID();
				}
			}
			if (this.battleAxe != null && !this.battleAxe.IsValid())
			{
				TutorialManager.ShowCellPopup(The.Player.CurrentCell.Location, "Huh, you somehow destroyed the battle axe. Let's move on.", "ne", 6, 6, null);
				TutorialManager.AdvanceStep(new GetBooks());
				return;
			}
			if (this.showingTooltip == 1)
			{
				if (ControlId.Get("DualPolatLooker:Left:DisplayName", false) != null)
				{
					this.showingTooltip = 3;
					TutorialManager.ShowIntermissionPopupAsync("The battle axe has better penetration and deals more damage than your dagger.", delegate
					{
						this.showingTooltip = 4;
					}, 0, 0f, "sw");
				}
				else if (ControlId.Get("PolatLooker:DisplayName", false) != null)
				{
					this.showingTooltip = 2;
					TutorialManager.ShowCIDPopupAsync("PolatLooker:Left:DisplayName", "Let’s take another look at weapon stats. Next to the {{c|→}} is 5. That means you’ll usually penetrate the armor of a creature with AV equal to 5.\r\n\r\nYou can penetrate armor multiple times, though, and each time you penetrate, you deal the weapon’s damage.\r\n\r\nlet’s equip it.", "ne", "[~Accept] Continue", 16, 16, 0f, delegate
					{
						this.showingTooltip = 4;
					});
				}
				else
				{
					this.showingTooltip = 0;
				}
			}
			if (base.CurrentGameView == "Stage")
			{
				this.manager.ClearHighlight();
				if (this.step == 0)
				{
					Cell currentCell = The.Player.CurrentCell;
					GameObject gameObject2 = this.battleAxe;
					if (currentCell == ((gameObject2 != null) ? gameObject2.CurrentCell : null))
					{
						if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
						{
							this.manager.HighlightCell(this.battleAxe.CurrentCell.X, this.battleAxe.CurrentCell.Y, "A battle axe. Get it.\n\nHit ~CmdUse", "ne", 3f, 3f, 0f);
						}
						else
						{
							this.manager.HighlightCell(this.battleAxe.CurrentCell.X, this.battleAxe.CurrentCell.Y, "A battle axe. Get it.\n\nHit ~CmdGet", "ne", 3f, 3f, 0f);
						}
					}
					else if (this.battleAxe.CurrentCell != null)
					{
						if (this.sawBattle || this.battleAxe.CurrentCell.IsVisible())
						{
							if (!this.sawBattle)
							{
								AutoAct.Setting = "";
							}
							this.sawBattle = true;
							this.manager.HighlightCell(this.battleAxe.CurrentCell.X, this.battleAxe.CurrentCell.Y, "Looks like there was some sort of battle to the north. Let’s grab the gear that was left behind.", "ne", 3f, 3f, 0f);
						}
						else
						{
							this.manager.HighlightCell(this.battleAxe.CurrentCell.X, this.battleAxe.CurrentCell.Y, "Keep moving forward.", "ne", 3f, 3f, 0f);
						}
					}
					if (this.battleAxe.InInventory == The.Player)
					{
						if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
						{
							this.manager.HighlightCell(The.PlayerCell.X, The.PlayerCell.Y, "Let's take a look at the Equipment screen.\n\nPress ~CmdCharacter", "se", 3f, 3f, 0f);
							return;
						}
						this.manager.HighlightCell(The.PlayerCell.X, The.PlayerCell.Y, "Let's take a look at the Equipment screen.\n\nPress ~CmdEquipment", "se", 3f, 3f, 0f);
						return;
					}
				}
			}
			else
			{
				if (GameManager.Instance.CurrentGameView == "PopupMessage")
				{
					string lastPopupID = PopupMessage.lastPopupID;
					if (lastPopupID != null && lastPopupID.StartsWith("InventoryActionMenu"))
					{
						if (this.battleAxe.InInventory == The.Player)
						{
							if (this.showingTooltip < 3)
							{
								this.manager.HighlightByCID("QudTextMenuItem:Cancel", "Hold on, we want to show you the tooltip compare feature.", "ne", 64, 64, 0f, "horiz");
								return;
							}
							this.manager.HighlightByCID("QudTextMenuItem:equip (auto)", "Equip the battle axe.", "ne", 64, TutorialManager.TWIDDLE_MENU_YPADDING, 0f, "horiz");
							return;
						}
						else
						{
							GameObject gameObject3 = this.battleAxe;
							if (((gameObject3 != null) ? gameObject3.Equipped : null) == null)
							{
								this.manager.HighlightByCID("QudTextMenuItem:get", "A battle axe. Get it.", "ne", 64, TutorialManager.TWIDDLE_MENU_YPADDING, 0f, "horiz");
								return;
							}
							return;
						}
					}
				}
				if (base.CurrentGameView == "StatusScreensScreen")
				{
					if (SingletonWindowBase<StatusScreensScreen>.instance.CurrentScreen == 2)
					{
						if (this.battleAxe.InInventory == The.Player)
						{
							if (this.showingTooltip != 0)
							{
								this.manager.HighlightByCID("InventoryLine:Item:" + this.battleAxe.ID, "Equip the battle axe since it's better than your dagger.", "ne", 64, TutorialManager.TWIDDLE_MENU_YPADDING, 0f, "horiz");
								return;
							}
							if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
							{
								this.manager.HighlightByCID("InventoryLine:Item:" + this.battleAxe.ID, "Press ~GamepadAlt + ~Take A Step to view the tooltip.", "ne", 64, TutorialManager.TWIDDLE_MENU_YPADDING, 0f, "horiz");
								return;
							}
							this.manager.HighlightByCID("InventoryLine:Item:" + this.battleAxe.ID, "Mouse over the battle axe and hover or press {{W|Alt}} to view the tooltip.", "ne", 64, TutorialManager.TWIDDLE_MENU_YPADDING, 0f, "horiz");
							return;
						}
						else if (this.battleAxe.Equipped == The.Player)
						{
							TutorialManager.AdvanceStep(new GetBooks());
							return;
						}
					}
					else
					{
						this.manager.HighlightByCID("StatusScreensScreenTab:2", "Select the Inventory && Equipment tab.\n\nYou can use ~Page Left and ~Page Right to navigate between tabs.", "se", 32, 16, 0f, "horiz");
					}
				}
			}
		}

		// Token: 0x040012DC RID: 4828
		public GameObject battleAxe;

		// Token: 0x040012DD RID: 4829
		public bool equippedPopupShown;

		// Token: 0x040012DE RID: 4830
		public bool lookPopupShown;

		// Token: 0x040012DF RID: 4831
		public int showingTooltip;

		// Token: 0x040012E0 RID: 4832
		public bool infoShown;

		// Token: 0x040012E1 RID: 4833
		public bool snapjawDead;

		// Token: 0x040012E2 RID: 4834
		public bool sawBattle;
	}
}
