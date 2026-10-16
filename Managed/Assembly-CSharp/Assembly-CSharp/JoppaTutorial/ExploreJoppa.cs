using System;
using Qud.UI;
using XRL;
using XRL.UI;
using XRL.World;
using XRL.World.Parts;

namespace JoppaTutorial
{
	// Token: 0x020003F0 RID: 1008
	public class ExploreJoppa : TutorialStep
	{
		// Token: 0x060025BA RID: 9658 RVA: 0x000C0F16 File Offset: 0x000BF116
		public override bool BeforePlayerEnterCell(Cell cell)
		{
			return ((cell != null) ? cell.ParentZone.ZoneID : null) == "JoppaWorld.11.22.1.1.10" || base.ConstrainToCurrentZone(cell);
		}

		// Token: 0x060025BB RID: 9659 RVA: 0x000C0F3E File Offset: 0x000BF13E
		public override bool AllowSelectedPopupCommand(string title, QudMenuItem command)
		{
			return true;
		}

		// Token: 0x060025BC RID: 9660 RVA: 0x000C0F44 File Offset: 0x000BF144
		public override void LateUpdate()
		{
			this.manager.ClearHighlight();
			if (this.mehmet == null)
			{
				Zone currentZone = The.Player.CurrentZone;
				this.mehmet = ((currentZone != null) ? currentZone.FindObject("Mehmet") : null);
				Zone currentZone2 = The.Player.CurrentZone;
				this.yrame = ((currentZone2 != null) ? currentZone2.FindObject("Warden Yrame") : null);
			}
			if (this.mehmet != null && this.yrame != null)
			{
				Brain brain = this.yrame.Brain;
				if (((brain != null) ? brain.Target : null) == this.mehmet)
				{
					this.uhoh = true;
				}
			}
			if (base.CurrentGameView == "PopupMessage")
			{
				string lastPopupID = PopupMessage.lastPopupID;
				if (lastPopupID != null && lastPopupID.StartsWith("CmdMoveToPointOfInterestMenu"))
				{
					this.step = 200;
				}
			}
			if (this.step == 200 && base.CurrentGameView == "PopupMessage")
			{
				string lastPopupID2 = PopupMessage.lastPopupID;
				if (lastPopupID2 != null && lastPopupID2.StartsWith("CmdMoveToPointOfInterestMenu"))
				{
					this.manager.HighlightByCID("QudTextMenuItem:mehmet [ne]", "Choose Mehmet.", "ne", 64, 8, 0f, "horiz");
				}
			}
			if (this.uhoh && !this.mehmet.IsValid())
			{
				TutorialManager.ShowCellPopup(this.yrame.CurrentCell.Location, "Oh… it looks like Warden Yrame killed Mehmet. Why, you ask?\n\nWell, named creatures who you can perform the water ritual with, like Mehmet, generate with dynamic faction relationships. In your game, Mehmet must have been hated by the Fellowship of Wardens for some reason.", "nw", 6, 6, delegate
				{
					TutorialManager.ShowCellPopup(this.yrame.CurrentCell.Location, "And so Warden Yrame took her revenge. In Qud, sometimes even our best efforts at providing a safe starter village are foiled by the simulation…\n\nWhat a charmed run! It's quite rare for this to happen, but now you get to see this hidden section of the tutorial. Good job.", "nw", 6, 6, delegate
					{
						TutorialManager.ShowCellPopup(The.Player.CurrentCell.Location, "You have a few options now. You can roll with it and continue in this world where Mehmet's been revenge-slain. The early game will be a bit harder, but the main quest is not impacted.\n\nYou can start over and play the tutorial again, for the \"normal\" ending, which you were about to reach.\n\nOr, since you were near the end anyway, you can start a new game and begin your journey proper!", "nw", 6, 6, delegate
						{
							TutorialManager.ShowCellPopup(The.Player.CurrentCell.Location, "Whatever you choose, good luck, and feel free to return here for a refresher.\n\nLive and drink.", "nw", 6, 6, delegate
							{
								Options.SetOption("OptionTutorialComplete", "Yes");
								TutorialManager.AdvanceStep(null);
							});
						});
					});
				});
			}
			if (The.Player.DistanceTo(this.mehmet) > 1)
			{
				GameObject gameObject = this.mehmet;
				if ((gameObject == null || gameObject.IsValid()) && !this.fancyDone)
				{
					goto IL_01C2;
				}
			}
			TutorialManager.ShowCellPopup(this.mehmet.CurrentCell.Location, "Quetzal! That's all for the tutorial, friend.\n\nWe just scratched the very tip of the surface. Quests, secrets, leveling up, physics, reputation, limbs... we leave that for you to discover.", "nw", 6, 6, delegate
			{
				TutorialManager.ShowCellPopup(this.mehmet.CurrentCell.Location, "You're ready to continue the journey on your own, but don't hesitate to play the tutorial again if you need a refresher.", "nw", 6, 6, delegate
				{
					TutorialManager.ShowCellPopup(this.mehmet.CurrentCell.Location, "Live and drink.", "nw", 6, 6, delegate
					{
						Options.SetOption("OptionTutorialComplete", "Yes");
						TutorialManager.AdvanceStep(null);
					});
				});
			});
			IL_01C2:
			if (base.CurrentGameView == "Stage")
			{
				if (this.step == 0)
				{
					this.step = 100;
					TutorialManager.ShowCellPopup(The.Player.CurrentCell.Location, "Joppa is a village. Everything you learned in the cave applies here too. Villages are like caves with very high roofs.", "nw", 6, 6, null);
				}
				if (this.step == 100 || this.step == 200)
				{
					if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
					{
						this.manager.HighlightByCID("Stage:TopButtonBar:POIButton", "You can see a list of points of interest by pressing ~CmdMoveToPointOfInterest.\n\nThe list will grow as you explore more of the map.", "sw", 64, 64, 0f, "horiz");
						return;
					}
					this.manager.HighlightByCID("Stage:TopButtonBar:POIButton", "You can see a list of points of interest by clicking this button or pressing ~CmdMoveToPointOfInterest.\n\nThe list will grow as you explore more of the map.", "sw", 64, 64, 0f, "horiz");
				}
			}
		}

		// Token: 0x040012E8 RID: 4840
		public GameObject mehmet;

		// Token: 0x040012E9 RID: 4841
		public GameObject yrame;

		// Token: 0x040012EA RID: 4842
		public bool uhoh;

		// Token: 0x040012EB RID: 4843
		private bool fancyDone;
	}
}
