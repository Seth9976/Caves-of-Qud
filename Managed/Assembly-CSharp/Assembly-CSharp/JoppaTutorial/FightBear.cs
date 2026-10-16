using System;
using System.Collections.Generic;
using System.Linq;
using Genkit;
using Qud.UI;
using XRL;
using XRL.UI;
using XRL.World;

namespace JoppaTutorial
{
	// Token: 0x020003F3 RID: 1011
	public class FightBear : TutorialStep
	{
		// Token: 0x060025C9 RID: 9673 RVA: 0x000C1728 File Offset: 0x000BF928
		public override bool AllowTargetPick(GameObject go, Type ability, List<Cell> target)
		{
			if (this.step == 300)
			{
				if (!this.bear.IsValid())
				{
					return true;
				}
				if (this.bear.DistanceTo(The.Player) == 1)
				{
					return true;
				}
				if (GameManager.IsOnGameContext())
				{
					Popup.Show("Wait for the bear to take a step towards you.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					return false;
				}
			}
			if (this.step >= 800)
			{
				if (target != null)
				{
					if (target.Any<Cell>((Cell t) => t.GetObjectCount("TutorialBear") > 0))
					{
						return true;
					}
				}
				Popup.Show("Make sure the bear is in the path of your freezing ray.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
			return true;
		}

		// Token: 0x060025CA RID: 9674 RVA: 0x000C17D1 File Offset: 0x000BF9D1
		public void BearSeen(Location2D location)
		{
			this.bearLocation = location;
			TutorialManager.ShowCellPopup(location, "What's that??? Some sort of creature. Take a closer look at it.", "ne", 6, 6, delegate
			{
				this.step = 200;
			});
			this.step = 100;
		}

		// Token: 0x060025CB RID: 9675 RVA: 0x000C1801 File Offset: 0x000BFA01
		public override bool BeforePlayerEnterCell(Cell cell)
		{
			if (cell.GetObjectCount("TutorialBear") > 0 && this.step >= 700)
			{
				Popup.Show("It's quite dangerous to fight this bear in melee combat! Try backing away and using Freezing Ray.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
			return base.ConstrainToCurrentZone(cell);
		}

		// Token: 0x060025CC RID: 9676 RVA: 0x000C183A File Offset: 0x000BFA3A
		public override bool AllowSelectedPopupCommand(string title, QudMenuItem command)
		{
			string.IsNullOrEmpty(title);
			return true;
		}

		// Token: 0x060025CD RID: 9677 RVA: 0x000C1844 File Offset: 0x000BFA44
		public override bool AllowCommand(string id)
		{
			if (base.CurrentGameView == "Stage")
			{
				if (id == "Accept" || id == "Cancel" || id == "CmdUse" || id == "Command:CmdUse" || id == "CmdSystemMenu")
				{
					return true;
				}
				if (this.step == 100)
				{
					if (!this.bear.IsValid())
					{
						return true;
					}
					if (id != "CmdLook" && id != "Command:CmdLook" && id != "CmdEquipment" && id != "Command:CmdEquipment" && id != "CmdInventory" && id != "Command:CmdInventory" && id != "Command:CmdCharacter" && id != "CmdCharacter" && id != "AdventureMouseInteract" && id != "Command:CmdSystemMenu" && GameManager.IsOnGameContext())
					{
						Popup.Show("Let's take a look at the bear before we continue.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
						return false;
					}
				}
				if (this.step == 300)
				{
					if (!this.bear.IsValid())
					{
						return true;
					}
					if (this.bear.DistanceTo(The.Player) == 1)
					{
						return true;
					}
					if (id != "CmdWait" && id != "Command:CmdWait" && id != "CmdEquipment" && id != "Command:CmdEquipment" && id != "CmdInventory" && id != "Command:CmdInventory" && id != "Command:CmdCharacter" && id != "CmdCharacter" && id != "AdventureMouseContextAction" && id != "Cancel" && id != "CmdSystemMenu" && id != "Command:CmdSystemMenu" && id != "CmdNone" && GameManager.IsOnGameContext())
					{
						Popup.Show("Wait for the bear to take a step towards you.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060025CE RID: 9678 RVA: 0x000C1A78 File Offset: 0x000BFC78
		public override void GameSync()
		{
			GameObject gameObject = this.bear;
			this.bearVisible = gameObject != null && gameObject.IsVisible();
			if (this.step == 600)
			{
				The.Player.ActivatedAbilities.GetAbilityByCommand("CommandToggleRunning").Refresh(null);
			}
			if (this.step == 800)
			{
				The.Player.ActivatedAbilities.GetAbilityByCommand("CommandFreezingRay").Refresh(null);
			}
		}

		// Token: 0x060025CF RID: 9679 RVA: 0x000C1AEC File Offset: 0x000BFCEC
		public override void LateUpdate()
		{
			this.manager.ClearHighlight();
			GameObject player = The.Player;
			if (((player != null) ? player.CurrentCell : null) == null)
			{
				this.manager.Highlight(null, null, null, 64f, 64f, 0f, "big");
				return;
			}
			if (this.bear == null)
			{
				this.bear = The.Player.CurrentCell.ParentZone.FindObject("TutorialBear");
				GameObject gameObject = this.bear;
				if (gameObject != null)
				{
					gameObject.RequireID();
				}
			}
			if (this.bear.IsInvalid())
			{
				TutorialManager.AdvanceStep(new ExamineChemcell());
				return;
			}
			if (this.step == 0)
			{
				if (base.CurrentGameView == "Stage")
				{
					this.manager.HighlightCell(this.bear.CurrentCell.X, this.bear.CurrentCell.Y, "Keep moving down the passage...", "se", 3f, 3f, 0f);
				}
				if (this.bearVisible)
				{
					TutorialManager.ShowCellPopup(this.bear.CurrentCell.Location, "Oh! Another creature. Let's look at it more closely.\n\nWhen you come across something new, it's always a good idea to inspect it.", "ne", 6, 6, null);
					this.step = 100;
					return;
				}
			}
			else if (this.step >= 100 && this.step < 200)
			{
				if (GameManager.Instance.CurrentGameView == "DynamicPopupMessage")
				{
					TutorialManager.ShowIntermissionPopupAsync("It's a bear.", delegate
					{
						TutorialManager.ShowCIDPopupAsync("PopupMessage:ContextItemText", "The bear is wounded, but it's tough. This'll be a harder fight.", "ne", "[~Accept] Continue", 0, 8, 0f, delegate
						{
							this.step = 300;
						});
					}, 0, 0f, "sw");
					return;
				}
				if (GameManager.Instance.CurrentGameView == "PopupMessage")
				{
					string lastPopupID = PopupMessage.lastPopupID;
					if (lastPopupID != null && lastPopupID.StartsWith("InventoryActionMenu"))
					{
						this.manager.HighlightByCID("QudTextMenuItem:look", "Inspect the bear.", "ne", 64, TutorialManager.TWIDDLE_MENU_YPADDING, 0f, "horiz");
						return;
					}
				}
				if (GameManager.Instance._CurrentGameView == "Stage")
				{
					if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
					{
						this.manager.HighlightCell(this.bear.CurrentCell.X, this.bear.CurrentCell.Y, "Use ~LookDirection to look at the bear.", "sw", 3f, 3f, 0f);
						return;
					}
					this.manager.HighlightCell(this.bear.CurrentCell.X, this.bear.CurrentCell.Y, "Press ~CmdLook or ~AdventureMouseInteract the bear.", "sw", 3f, 3f, 0f);
					return;
				}
				else if (GameManager.Instance.CurrentGameView == "Looker")
				{
					if (Look.lookingAt != this.bear)
					{
						this.manager.HighlightCell(this.bear.CurrentCell.X, this.bear.CurrentCell.Y, "Look view lets you look around and investigate your surroundings.\n\nMove the cursor to the bear.", "ne", 12f, 12f, 0f);
						return;
					}
					if (this.step == 100)
					{
						this.step = 200;
						TutorialManager.ShowIntermissionPopupAsync("It's a bear.", delegate
						{
							TutorialManager.ShowCIDPopupAsync("PolatLooker:SubHeader", "The bear is wounded, but it's tough. This'll be a harder fight.", "ne", "[~Accept] Continue", 0, 12, 0f, delegate
							{
								this.step = 300;
							});
						}, 0, 0f, "sw");
						return;
					}
				}
			}
			else if (this.step == 300)
			{
				if (!this.bear.IsValid())
				{
					this.step = 500;
				}
				if (GameManager.Instance._CurrentGameView == "Stage")
				{
					if (this.bear.DistanceTo(The.Player) == 1)
					{
						if (base.CurrentGameView == "Stage")
						{
							this.manager.HighlightCell(this.bear.CurrentCell.X, this.bear.CurrentCell.Y, "Bump attack the bear.", "se", 3f, 3f, 0f);
							return;
						}
					}
					else if (base.CurrentGameView == "Stage")
					{
						if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
						{
							this.manager.HighlightCell(this.bear.CurrentCell.X, this.bear.CurrentCell.Y, "You can wait a turn for the bear to approach. That way, it won't be in range to attack you when it takes its turn.\n\nTap ~Take A Step without choosing a direction with the stick to pass a turn.", "se", 3f, 3f, 0f);
							return;
						}
						this.manager.HighlightCell(this.bear.CurrentCell.X, this.bear.CurrentCell.Y, "You can wait a turn for the bear to approach. That way, it won't be in range to attack you when it takes its turn.\n\nPress %CmdWait or ~AdventureMouseContextAction on your character to pass a turn.", "se", 3f, 3f, 0f);
						return;
					}
				}
			}
			else if (this.step == 400)
			{
				if (base.CurrentGameView == "Stage")
				{
					this.manager.HighlightByCID("Stage:MessageLog", "You missed, then the bear attacked and missed.\n\nAttack again.", "sw", 2, 2, 0f, "horiz");
				}
				if (The.Player.hitpoints < The.Player.baseHitpoints)
				{
					this.step = 500;
					return;
				}
			}
			else
			{
				if (this.step == 500)
				{
					TutorialManager.ShowCIDPopupAsync("$HPBar", "This time you hit, but you failed to penetrate the bear's armor.\n\nThen, the bear hit you and DID penetrate your armor. You took 4 damage.", "sw", "[~Accept] Continue", 16, 16, 0f, null);
					this.step = 600;
					return;
				}
				if (this.step == 600)
				{
					if (base.CurrentGameView == "Stage")
					{
						if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
						{
							this.manager.HighlightByCID("AbilityBar:Button:CommandToggleRunning", "Let's try using an ability. Look at your ability bar. Let's start sprinting. \n\nSelect it with ~Next Ability and ~Previous Ability then press ~Use Ability.", "nw", 4, 4, 0f, "horiz");
						}
						else
						{
							this.manager.HighlightByCID("AbilityBar:Button:CommandToggleRunning", "Let's try using an ability. Look at your ability bar. Let's start sprinting. You can click the button or hit the hotkey {{hotkey|~CmdAbility1}}.", "nw", 4, 4, 0f, "horiz");
						}
					}
					if (The.Player.HasEffect("Running"))
					{
						this.step = 700;
						return;
					}
				}
				else if (this.step == 700)
				{
					if (base.CurrentGameView == "Stage")
					{
						this.manager.HighlightCell(19, 13, "Sprinting doubles your movespeed but ends if you make an attack. Let's sprint back down the corridor to put some distance between us and the bear.", "sw", 2f, 2f, 0f);
					}
					if (The.Player.DistanceTo(this.bear) >= 2 && this.bearVisible)
					{
						this.step = 800;
						return;
					}
				}
				else if (this.step == 800)
				{
					if (!this.bearVisible)
					{
						this.step = 700;
					}
					if (base.CurrentGameView == "Stage")
					{
						if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
						{
							this.manager.HighlightByCID("AbilityBar:Button:CommandFreezingRay", "Every character has Sprint, but most of your abilities are determined by traits like your mutations. You have Freezing Ray, so you can shoot frost out of your hands.\n\nSelect it with ~Next Ability and ~Previous Ability then press ~Use Ability.", "nw", 4, 4, 0f, "horiz");
							return;
						}
						this.manager.HighlightByCID("AbilityBar:Button:CommandFreezingRay", "Every character has Sprint, but most of your abilities are determined by traits like your mutations. You have Freezing Ray, so you can shoot frost out of your hands. Select it.\n\nPress {{hotkey|~CmdAbility2}}", "nw", 4, 4, 0f, "horiz");
						return;
					}
				}
				else
				{
					this.manager.ClearHighlight();
				}
			}
		}

		// Token: 0x040012F0 RID: 4848
		public GameObject bear;

		// Token: 0x040012F1 RID: 4849
		public GameObject chemCell;

		// Token: 0x040012F2 RID: 4850
		private Location2D bearLocation;

		// Token: 0x040012F3 RID: 4851
		public bool bearDead;

		// Token: 0x040012F4 RID: 4852
		public bool bearVisible;
	}
}
