using System;
using Qud.UI;
using XRL;
using XRL.UI;
using XRL.World;

namespace JoppaTutorial
{
	// Token: 0x020003F2 RID: 1010
	public class ExploreWorldMap : TutorialStep
	{
		// Token: 0x060025C5 RID: 9669 RVA: 0x000C1468 File Offset: 0x000BF668
		public override bool BeforePlayerEnterCell(Cell cell)
		{
			if (cell.ParentZone.ZoneID == "JoppaWorld")
			{
				if (cell.X == 11 && cell.Y >= 22 && cell.Y <= 24)
				{
					return true;
				}
			}
			else if (cell.ParentZone.ZoneID == "JoppaWorld.11.22.1.1.10")
			{
				return true;
			}
			Popup.Show("You'll be able to explore the world freely after the tutorial.\n\nFor now, let's visit Joppa.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			return false;
		}

		// Token: 0x060025C6 RID: 9670 RVA: 0x000C14DB File Offset: 0x000BF6DB
		public override bool AllowSelectedPopupCommand(string title, QudMenuItem command)
		{
			return true;
		}

		// Token: 0x060025C7 RID: 9671 RVA: 0x000C14E0 File Offset: 0x000BF6E0
		public override void LateUpdate()
		{
			this.manager.ClearHighlight();
			if (this.joppa == null)
			{
				Zone currentZone = The.Player.CurrentZone;
				this.joppa = ((currentZone != null) ? currentZone.FindObject("TerrainJoppa") : null);
			}
			if (base.CurrentGameView == "Looker" && this.step == 0)
			{
				this.step = 100;
			}
			if (base.CurrentGameView == "Stage")
			{
				if (The.Player.CurrentZone.Z == 10)
				{
					TutorialManager.AdvanceStep(new ExploreJoppa());
					return;
				}
				if (this.step == 0)
				{
					if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
					{
						this.manager.HighlightCell(The.Player.CurrentCell.X, The.Player.CurrentCell.Y, "You can look around here just like on a local map.\n\nUse ~LookDirection", "sw", 3f, 3f, 0f);
					}
					else
					{
						this.manager.HighlightCell(The.Player.CurrentCell.X, The.Player.CurrentCell.Y, "You can look around here just like on a local map.\n\nPress ~CmdLook", "sw", 3f, 3f, 0f);
					}
				}
				if (this.step == 100)
				{
					if (The.Player.CurrentCell == this.joppa.CurrentCell)
					{
						if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
						{
							this.manager.HighlightByCID("Stage:TopButtonBar:DownButton", "Descend.\n\nPress ~CmdMoveD.", "sw", 64, 64, 0f, "horiz");
							return;
						}
						this.manager.HighlightByCID("Stage:TopButtonBar:DownButton", "Descend.\n\nYou can click this button or press ~CmdMoveD.", "sw", 64, 64, 0f, "horiz");
						return;
					}
					else
					{
						if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
						{
							this.manager.HighlightCell(this.joppa.CurrentCell.X, this.joppa.CurrentCell.Y, "There’s a village to the north called Joppa. Let’s go there.\n\nHold ~IndicateDirection towards the north and press ~Take A Step.", "ne", 3f, 3f, 0f);
							return;
						}
						this.manager.HighlightCell(this.joppa.CurrentCell.X, this.joppa.CurrentCell.Y, "There’s a village to the north called Joppa. Let’s go there.\n\nPress ~CmdMoveN or ~AdventureMouseContextAction.", "ne", 3f, 3f, 0f);
					}
				}
			}
		}

		// Token: 0x040012EF RID: 4847
		public GameObject joppa;
	}
}
