using System;
using Qud.UI;
using XRL;
using XRL.World;

namespace JoppaTutorial
{
	// Token: 0x020003EE RID: 1006
	public class ExamineChemcell : TutorialStep
	{
		// Token: 0x060025B1 RID: 9649 RVA: 0x000C094C File Offset: 0x000BEB4C
		public override bool AllowSelectedPopupCommand(string title, QudMenuItem command)
		{
			if (string.IsNullOrEmpty(title))
			{
				return true;
			}
			if (this.step == 100 && GameManager.Instance.CurrentGameView == "PopupMessage")
			{
				string lastPopupID = PopupMessage.lastPopupID;
				if (lastPopupID != null && lastPopupID.StartsWith("InventoryActionMenu:" + this.chemCell.ID) && !this.chemCell.Understood())
				{
					return command.simpleText == "examine" || command.command == "Cancel";
				}
			}
			return true;
		}

		// Token: 0x060025B2 RID: 9650 RVA: 0x000C09E2 File Offset: 0x000BEBE2
		public override int AdjustExaminationRoll(int result)
		{
			return 9999;
		}

		// Token: 0x060025B3 RID: 9651 RVA: 0x000C09EC File Offset: 0x000BEBEC
		public override void LateUpdate()
		{
			this.manager.ClearHighlight();
			if (this.step == 0)
			{
				this.step = 10;
				TutorialManager.ShowIntermissionPopupAsync("The bear is dead! Looks like it dropped something, too.\n\nBut first, let's use this opportunity to heal.", delegate
				{
					this.step = 20;
				}, 0, 0f, "sw");
				return;
			}
			if (this.step != 20)
			{
				if (this.step == 100)
				{
					GameObject player = The.Player;
					if (((player != null) ? player.CurrentCell : null) == null)
					{
						this.manager.Highlight(null, null, null, 64f, 64f, 0f, "big");
						return;
					}
					if (this.chemCell == null)
					{
						this.chemCell = The.Player.CurrentCell.ParentZone.FindObject("TutorialChemCell");
						if (this.chemCell == null)
						{
							this.chemCell = The.Player.Inventory.FindObjectByBlueprint("TutorialChemCell");
						}
						GameObject gameObject = this.chemCell;
						if (gameObject != null)
						{
							gameObject.RequireID();
						}
					}
					if (this.chemCell.Understood() && (GameManager.Instance.CurrentGameView == "PopupMessage" || GameManager.Instance.CurrentGameView == "Stage"))
					{
						TutorialManager.ShowCIDPopupAsync("PopupMessage", "It's a chem cell. It can be used later to power other artifacts you find.", "s", "[~Accept] Continue", 0, 0, 0f, null);
						TutorialManager.AdvanceStep(new ExploreDeeper());
					}
					if (this.chemCell != null && !this.chemCell.IsValid())
					{
						TutorialManager.AdvanceStep(new ExploreDeeper());
					}
					if (base.CurrentGameView == "Stage")
					{
						if (this.chemCell.InInventory == The.Player)
						{
							if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
							{
								this.manager.HighlightObject(The.Player, "You picked up the odd trinket automatically because it is an artifact.\n\nPress ~CmdCharacter to investigate it.", "ne", 3f, 3f);
							}
							else
							{
								this.manager.HighlightObject(The.Player, "You picked up the odd trinket automatically because it is an artifact.\n\nPress ~CmdInventory to investigate it.", "ne", 3f, 3f);
							}
						}
						else
						{
							this.manager.HighlightObject(this.chemCell, "The bear is dead! Looks like it dropped something, too.", "ne", 3f, 3f);
						}
					}
					if (base.CurrentGameView == "StatusScreensScreen")
					{
						if (SingletonWindowBase<StatusScreensScreen>.instance.CurrentScreen == 2)
						{
							if (this.chemCell.InInventory == The.Player)
							{
								if (this.step == 0)
								{
									this.step = 1;
									TutorialManager.ShowIntermissionPopupAsync("What's that???", null, 0, 0f, "sw");
								}
								else
								{
									this.manager.HighlightByCID("InventoryLine:Item:" + this.chemCell.ID, "This is an artifact beyond simple understanding. Try examining it.", "ne", 64, TutorialManager.TWIDDLE_MENU_YPADDING, 0f, "horiz");
								}
							}
						}
						else
						{
							this.manager.HighlightByCID("StatusScreensScreenTab:2", "Select the Inventory && Equipment tab.\n\nYou can use ~Page Left and ~Page Right to navigate between tabs.", "se", 32, 16, 0f, "horiz");
						}
					}
					if (GameManager.Instance.CurrentGameView == "PopupMessage")
					{
						string lastPopupID = PopupMessage.lastPopupID;
						if (lastPopupID != null && lastPopupID.StartsWith("InventoryActionMenu:" + this.chemCell.ID))
						{
							if (this.step == 0)
							{
								this.step = 1;
								TutorialManager.ShowIntermissionPopupAsync("What's that???", null, 0, 0f, "sw");
								return;
							}
							this.manager.HighlightByCID("QudTextMenuItem:examine", "This is an artifact beyond simple understanding. Try examining it.", "ne", 64, TutorialManager.TWIDDLE_MENU_YPADDING, 0f, "horiz");
						}
					}
				}
				return;
			}
			if (The.Player.hitpoints == The.Player.baseHitpoints)
			{
				this.step = 100;
				return;
			}
			this.manager.HighlightObject(The.Player, "You regain hitpoints naturally as turns pass. You can pass a few turns by waiting, or if there are no hostile creatures around, you can {{W|rest until healed}}.\n\nPress ~CmdWaitUntilHealed", "ne", 3f, 3f);
		}

		// Token: 0x040012E3 RID: 4835
		public GameObject chemCell;

		// Token: 0x040012E4 RID: 4836
		public bool snapjawDead;
	}
}
