using System;
using Genkit;
using Qud.UI;
using XRL;
using XRL.UI;
using XRL.World;
using XRL.World.Parts;

namespace JoppaTutorial
{
	// Token: 0x020003F4 RID: 1012
	public class FightSnapjaw : TutorialStep
	{
		// Token: 0x060025D6 RID: 9686 RVA: 0x000C226C File Offset: 0x000C046C
		public void SnapjawSeen(Location2D location)
		{
			this.snapjawLocation = location;
			TutorialManager.ShowCellPopup(location, "What's that??? Some sort of creature. Take a closer look at it.", "ne", 6, 6, delegate
			{
				this.step = 200;
			});
			this.step = 100;
		}

		// Token: 0x060025D7 RID: 9687 RVA: 0x000C229C File Offset: 0x000C049C
		public override bool BeforePlayerEnterCell(Cell cell)
		{
			if (cell.X > 27)
			{
				Popup.Show("Before you move on, you should loot the corpse of the snapjaw.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				return false;
			}
			return base.ConstrainToCurrentZone(cell);
		}

		// Token: 0x060025D8 RID: 9688 RVA: 0x000C22C8 File Offset: 0x000C04C8
		public override bool AllowSelectedPopupCommand(string title, QudMenuItem command)
		{
			if (string.IsNullOrEmpty(title))
			{
				return true;
			}
			string text = "InventoryActionMenu:";
			GameObject gameObject = this.leatherArmor;
			return !(title == text + ((gameObject != null) ? gameObject.ID : null)) || (!this.lookPopupShown && (command.simpleText == "get" || command.command == "Cancel")) || (this.lookPopupShown && (command.simpleText == "equip (auto)" || command.command == "Cancel"));
		}

		// Token: 0x060025D9 RID: 9689 RVA: 0x000C2364 File Offset: 0x000C0564
		public override bool AllowCommand(string id)
		{
			if (base.CurrentGameView == "Stage")
			{
				if (id == "Accept" || id == "Cancel" || id == "CmdUse" || id == "Command:CmdUse" || id == "CmdSystemMenu" || id == "Command:CmdSystemMenu" || id == "CmdNone")
				{
					return true;
				}
				GameManager.Instance.CurrentGameView == "Looker";
				if (this.step >= 200 && this.step < 300)
				{
					GameObject gameObject = this.snapjaw;
					if (gameObject == null || !gameObject.IsValid())
					{
						return true;
					}
					if (id != "CmdLook" && id != "Command:CmdLook" && id != "CmdEquipment" && id != "Command:CmdEquipment" && id != "CmdInventory" && id != "Command:CmdInventory" && id != "Command:CmdCharacter" && id != "CmdCharacter" && id != "AdventureMouseInteract" && id != "Command:CmdSystemMenu" && id != "CmdNone" && GameManager.IsOnGameContext())
					{
						Popup.Show("Take a look at the snapjaw before we continue.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060025DA RID: 9690 RVA: 0x000C24E8 File Offset: 0x000C06E8
		public override void LateUpdate()
		{
			GameObject player = The.Player;
			if (((player != null) ? player.CurrentCell : null) == null)
			{
				this.manager.Highlight(null, null, null, 64f, 64f, 0f, "big");
				return;
			}
			if (this.snapjaw == null)
			{
				this.snapjaw = The.Player.CurrentCell.ParentZone.FindObject("TutorialSnapjaw");
				GameObject gameObject = this.snapjaw;
				if (gameObject != null)
				{
					gameObject.RequireID();
				}
			}
			if (this.step == 500)
			{
				if (this.leatherArmor == null)
				{
					Zone currentZone = The.Player.CurrentZone;
					this.leatherArmor = ((currentZone != null) ? currentZone.FindObject("TutorialLeatherArmor") : null);
				}
				if (this.leatherArmor == null)
				{
					Inventory inventory = The.Player.Inventory;
					this.leatherArmor = ((inventory != null) ? inventory.FindObjectByBlueprint("TutorialChemCell") : null);
				}
			}
			if (this.leatherArmor != null && !this.leatherArmor.IsValid())
			{
				TutorialManager.ShowCellPopup(The.Player.CurrentCell.Location, "Huh, you somehow destroyed the leather armor. Let's move on.", "ne", 6, 6, null);
				TutorialManager.AdvanceStep(new FightBear());
				return;
			}
			if (this.snapjawDead && this.step < 400)
			{
				this.step = 500;
			}
			if (this.step != 0)
			{
				if (this.step != 100)
				{
					if (this.step >= 200 && this.step < 300)
					{
						if (GameManager.Instance.CurrentGameView == "DynamicPopupMessage")
						{
							if (this.step == 200)
							{
								this.step = 300;
								TutorialManager.ShowIntermissionPopupAsync("Looking at a creature shows its description and some other important details. Every object in the world can be looked at.", delegate
								{
									TutorialManager.ShowCIDPopupAsync("PopupMessage:ContextItemText", "The snapjaw is hostile! Let's get ready for a fight.", "ne", "[~Accept] Continue", 0, 8, 0f, null);
								}, 0, 0f, "sw");
								return;
							}
						}
						else
						{
							if (GameManager.Instance.CurrentGameView == "PopupMessage")
							{
								string lastPopupID = PopupMessage.lastPopupID;
								if (lastPopupID != null && lastPopupID.StartsWith("InventoryActionMenu"))
								{
									this.manager.HighlightByCID("QudTextMenuItem:look", "Inspect the snapjaw.", "ne", 64, TutorialManager.TWIDDLE_MENU_YPADDING, 0f, "horiz");
									return;
								}
							}
							if (GameManager.Instance._CurrentGameView == "Stage")
							{
								if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
								{
									this.manager.HighlightCell(this.snapjawLocation.X, this.snapjawLocation.Y, "Use ~LookDirection to look at the snapjaw.", "sw", 3f, 3f, 0f);
									return;
								}
								this.manager.HighlightCell(this.snapjawLocation.X, this.snapjawLocation.Y, "Press ~CmdLook or ~AdventureMouseInteract the snapjaw.", "sw", 3f, 3f, 0f);
								return;
							}
							else
							{
								if (!(GameManager.Instance.CurrentGameView == "Looker"))
								{
									this.manager.ClearHighlight();
									return;
								}
								if (Look.lookingAt != this.snapjaw)
								{
									this.manager.HighlightCell(this.snapjawLocation.X, this.snapjawLocation.Y, "Look view lets you look around and investigate your surroundings.\n\nMove the cursor to the snapjaw.", "ne", 12f, 12f, 0f);
									return;
								}
								if (this.step == 200)
								{
									this.step = 300;
									TutorialManager.ShowIntermissionPopupAsync("Here we have a description of the creature, along with some details about them.", delegate
									{
										TutorialManager.ShowCIDPopupAsync("PolatLooker:SubHeader", "The snapjaw is hostile! Let's get ready for a fight.", "ne", "[~Accept] Continue", 0, 12, 0f, null);
									}, 0, 0f, "sw");
									return;
								}
							}
						}
					}
					else if (this.step == 300)
					{
						if (GameManager.Instance._CurrentGameView == "Stage")
						{
							this.step = 400;
							TutorialManager.ShowCellPopup(this.snapjaw.CurrentCell.Location, "All actions are turn-based, meaning you take a turn, then other creatures take a turn. For the most part, creatures won't act until you do.\r\n\r\nIf you ever get panicked, just slow down and consider your next move. There's no rush.", "ne", 6, 6, null);
							return;
						}
					}
					else if (this.step == 400)
					{
						if (this.leatherArmor != null || this.snapjawDead)
						{
							this.step = 450;
							this.manager.ClearHighlight();
							this.manager.After(1000, delegate
							{
								this.step = 500;
							});
							return;
						}
						if (!(GameManager.Instance._CurrentGameView == "Stage"))
						{
							this.manager.ClearHighlight();
							return;
						}
						GameObject gameObject2 = this.snapjaw;
						if (gameObject2 != null && gameObject2.DistanceTo(The.Player) == 1)
						{
							this.manager.HighlightCell(this.snapjaw.CurrentCell.X, this.snapjaw.CurrentCell.Y, "You can attack a hostile creature by moving into its square. This is called {{W|bump attacking}}.", "se", 3f, 3f, 0f);
							return;
						}
						this.manager.HighlightCell(this.snapjaw.CurrentCell.X, this.snapjaw.CurrentCell.Y, "Take a step toward the snapjaw.", "se", 3f, 3f, 0f);
						return;
					}
					else if (this.step == 500)
					{
						this.manager.ClearHighlight();
						if (base.CurrentGameView == "Stage")
						{
							if (The.Player.CurrentCell == this.leatherArmor.CurrentCell)
							{
								if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
								{
									this.manager.HighlightCell(this.leatherArmor.CurrentCell.X, this.leatherArmor.CurrentCell.Y, "You can pick up things that are in the same space as you.\n\nPress ~CmdUse", "ne", 3f, 3f, 0f);
								}
								else
								{
									this.manager.HighlightCell(this.leatherArmor.CurrentCell.X, this.leatherArmor.CurrentCell.Y, "You can pick up things that are in the same space as you.\n\nPress ~CmdGet", "ne", 3f, 3f, 0f);
								}
							}
							else if (this.leatherArmor.CurrentCell != null)
							{
								string text = " or press ~AdventureMouseInteract.";
								if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
								{
									text = ".";
								}
								this.manager.HighlightCell(this.leatherArmor.CurrentCell.X, this.leatherArmor.CurrentCell.Y, "Whew! You killed the snapjaw and earned some experience. It looks like they dropped a piece of equipment, too.\n\nPress ~CmdGetFrom and then choose a direction" + text, "ne", 3f, 3f, 0f);
							}
						}
						if (GameManager.Instance.CurrentGameView == "PopupMessage")
						{
							string lastPopupID2 = PopupMessage.lastPopupID;
							if (lastPopupID2 != null && lastPopupID2.StartsWith("InventoryActionMenu"))
							{
								if (this.leatherArmor.InInventory == The.Player)
								{
									if (!this.lookPopupShown)
									{
										TutorialManager.ShowCIDPopupAsync("PopupMessage:ContextItemText", "Let's look at the leather armor you picked up.\n\nArmor has an armor value ({{b|♦}}{{y|2}}) and a dodge value ({{K|○}}{{y|0}})\n\nBoth help you avoid damage.", "ne", "[~Accept] Continue", 16, 16, 0f, null);
										this.lookPopupShown = true;
									}
									else
									{
										this.manager.HighlightByCID("QudTextMenuItem:equip (auto)", "Let's equip it.", "ne", 64, TutorialManager.TWIDDLE_MENU_YPADDING, 0f, "horiz");
									}
								}
								else
								{
									GameObject gameObject3 = this.leatherArmor;
									if (((gameObject3 != null) ? gameObject3.Equipped : null) == null)
									{
										this.manager.HighlightByCID("QudTextMenuItem:get", "It's leather armor. This time, get it instead of equipping it.", "ne", 64, TutorialManager.TWIDDLE_MENU_YPADDING, 0f, "horiz");
									}
								}
							}
						}
						if (this.leatherArmor.InInventory == The.Player && base.CurrentGameView == "Stage")
						{
							if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
							{
								this.manager.HighlightCell(The.PlayerCell.X, The.PlayerCell.Y, "Let's take a look at the Equipment screen.\n\nPress ~CmdCharacter", "se", 3f, 3f, 0f);
							}
							else
							{
								this.manager.HighlightCell(The.PlayerCell.X, The.PlayerCell.Y, "Let's take a look at the Equipment screen.\n\nPress ~CmdEquipment", "se", 3f, 3f, 0f);
							}
						}
						if (base.CurrentGameView == "StatusScreensScreen")
						{
							if (SingletonWindowBase<StatusScreensScreen>.instance.CurrentScreen != 2)
							{
								this.manager.HighlightByCID("StatusScreensScreenTab:2", "Select the Inventory && Equipment tab.\n\nYou can use {{~Page Left}} and {{~Page Right}} to navigate between tabs.", "se", 32, 16, 0f, "horiz");
								return;
							}
							if (!this.equipmentPopupShown)
							{
								this.equipmentPopupShown = true;
								TutorialManager.ShowIntermissionPopupAsync("This is your equipment and inventory screen. Equipped items on the left, and inventory on the right.", null, 0, 0f, "sw");
								return;
							}
							if (this.leatherArmor.InInventory == The.Player)
							{
								if (!this.lookPopupShown)
								{
									TutorialManager.ShowCIDPopupAsync("InventoryLine:Item:" + this.leatherArmor.ID, "Let's look at the leather armor you picked up.\n\nArmor has an armor value ({{b|♦}}{{y|2}}) and a dodge value ({{K|○}}{{y|0}})\n\nBoth help you avoid damage.", "ne", "[~Accept] Continue", 16, 16, 0f, null);
									this.lookPopupShown = true;
									return;
								}
								this.manager.HighlightByCID("InventoryLine:Item:" + this.leatherArmor.ID, "Let's equip it.", "nw", 64, TutorialManager.TWIDDLE_MENU_YPADDING, 0f, "horiz");
								return;
							}
							else if (this.leatherArmor.Equipped == The.Player && !this.equippedPopupShown)
							{
								this.equippedPopupShown = true;
								TutorialManager.ShowCIDPopupAsync("EquipmentLine:Slot:body", "You can also equip items by dragging them from your inventory to their equipment slot.\n\nBy the way, you have several other tabs in your character sheet. Feel free to explore them later.", "ne", "[~Accept] Continue", 48, 8, 0f, delegate
								{
									TutorialManager.AdvanceStep(new FightBear());
								});
								return;
							}
						}
					}
					else
					{
						this.manager.ClearHighlight();
					}
				}
				return;
			}
			if (!(base.CurrentGameView == "Stage"))
			{
				this.manager.ClearHighlight();
				return;
			}
			if (The.Player.CurrentCell.X < 17)
			{
				this.manager.HighlightCell(17, 13, "Let's explore further into the cave.", "se", 3f, 3f, 0f);
				return;
			}
			this.manager.HighlightCell(26, 13, "Let's explore further into the cave.\n\nWalk down the passage.", "se", 3f, 3f, 0f);
		}

		// Token: 0x040012F5 RID: 4853
		public GameObject snapjaw;

		// Token: 0x040012F6 RID: 4854
		public GameObject leatherArmor;

		// Token: 0x040012F7 RID: 4855
		private Location2D snapjawLocation;

		// Token: 0x040012F8 RID: 4856
		public bool snapjawDead;

		// Token: 0x040012F9 RID: 4857
		public bool lookPopupShown;

		// Token: 0x040012FA RID: 4858
		public bool equippedPopupShown;

		// Token: 0x040012FB RID: 4859
		public float timer;

		// Token: 0x040012FC RID: 4860
		public bool equipmentPopupShown;
	}
}
