using System;
using Qud.UI;
using XRL;
using XRL.World;

namespace JoppaTutorial
{
	// Token: 0x020003F5 RID: 1013
	public class GetBooks : TutorialStep
	{
		// Token: 0x060025DE RID: 9694 RVA: 0x000C2EDC File Offset: 0x000C10DC
		public override bool BeforePlayerEnterCell(Cell cell)
		{
			return base.ConstrainToCurrentZone(cell);
		}

		// Token: 0x060025DF RID: 9695 RVA: 0x000C2EE5 File Offset: 0x000C10E5
		public override bool AllowSelectedPopupCommand(string title, QudMenuItem command)
		{
			return true;
		}

		// Token: 0x060025E0 RID: 9696 RVA: 0x000C2EE8 File Offset: 0x000C10E8
		public override bool AllowCommand(string id)
		{
			return this.step != 0 || !(base.CurrentGameView == "Stage") || The.Player.DistanceTo(The.PlayerCell.ParentZone.GetCell(38, 16)) > 1 || (id != null && id.Contains("CmdGetFrom")) || (id != null && id.Contains("Cancel")) || (id != null && id.Contains("AdventureMouseInteractAll"));
		}

		// Token: 0x060025E1 RID: 9697 RVA: 0x000C2F64 File Offset: 0x000C1164
		public override void GameSync()
		{
			if (this.book1 == null)
			{
				this.book1 = The.Player.CurrentZone.FindObject("TutorialAphorisms");
				GameObject gameObject = this.book1;
				if (gameObject != null)
				{
					gameObject.RequireID();
				}
				this.book2 = The.Player.CurrentZone.FindObject("TutorialMarkovBook");
				GameObject gameObject2 = this.book2;
				if (gameObject2 == null)
				{
					return;
				}
				gameObject2.RequireID();
			}
		}

		// Token: 0x060025E2 RID: 9698 RVA: 0x000C2FD0 File Offset: 0x000C11D0
		public override void LateUpdate()
		{
			this.manager.ClearHighlight();
			if (The.Player.CurrentZone.GetCell(38, 16).GetObjectCount((GameObject o) => o.Blueprint == "TutorialMarkovBook") == 0)
			{
				TutorialManager.AdvanceStep(new ExploreStairs());
				return;
			}
			if (this.step == 0)
			{
				if (base.CurrentGameView == "Stage")
				{
					if (The.Player.DistanceTo(The.PlayerCell.ParentZone.GetCell(38, 16)) > 1)
					{
						this.manager.HighlightCell(38, 16, "There's something else over here.", "nw", 3f, 3f, 0f);
					}
					else
					{
						string text = " or press ~AdventureMouseInteractAll.";
						if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
						{
							text = ".";
						}
						this.manager.HighlightCell(38, 16, "Sometimes squares have multiple items in them.\n\nTo see everything in a space, press ~CmdGetFrom and then choose a direction" + text, "nw", 3f, 3f, 0f);
					}
				}
				else if (base.CurrentGameView == "PickGameObject" && ControlId.Get("PickGameObject:Item:" + this.book1.ID, false) != null)
				{
					this.step = 1;
					TutorialManager.ShowCIDPopupAsync("PickGameObject:Item:" + this.book1.ID, "Whoever this was, they had some books. All books can be read in game.\n\nBooks with {{Y|white}} titles are generated and usually contain nonsense, but the nonsense can occasionally be useful.\n\nBooks with {{W|gold}} titles are more valuable: they are handwritten by in-world beings and contain interesting takes on the world.", "nw", "[~Accept] Continue", 16, 36, -18f, null);
				}
				if (this.book1 != null && (!this.book1.IsValid() || !this.book2.IsValid() || this.book1.InInventory != null || this.book2.InInventory != null))
				{
					this.step = 1;
				}
			}
			if (this.step == 1 || this.step == 2)
			{
				if (base.CurrentGameView == "Stage")
				{
					if (this.step == 1)
					{
						string text2 = " or press ~AdventureMouseInteractAll.";
						if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
						{
							text2 = ".";
						}
						this.manager.HighlightCell(38, 16, "Sometimes squares have multiple items in them.\n\nTo see everything in a space, press ~CmdGetFrom and then choose a direction" + text2, "nw", 3f, 3f, 0f);
						return;
					}
					TutorialManager.AdvanceStep(new ExploreStairs());
					return;
				}
				else if (base.CurrentGameView == "PickGameObject")
				{
					if (ControlId.Get("PickGameObject:Item:" + this.book1.ID, false) != null)
					{
						this.manager.HighlightByCID("PickGameObject:Item:" + this.book1.ID, "Get the books.", "ne", 64, TutorialManager.TWIDDLE_MENU_YPADDING, 0f, "horiz");
						return;
					}
					if (ControlId.Get("PickGameObject:Item:" + this.book2.ID, false) != null)
					{
						this.manager.HighlightByCID("PickGameObject:Item:" + this.book2.ID, "Get the books.", "ne", 64, TutorialManager.TWIDDLE_MENU_YPADDING, 0f, "horiz");
						return;
					}
					this.step = 2;
					this.manager.HighlightByCID("TAKE_ALL", "This water is valuable too. Take it all.", "ne", 64, 36, -18f, "horiz");
				}
			}
		}

		// Token: 0x040012FD RID: 4861
		private GameObject book1;

		// Token: 0x040012FE RID: 4862
		private GameObject book2;
	}
}
