using System;
using System.Linq;
using Qud.UI;
using XRL;
using XRL.World;

namespace JoppaTutorial
{
	// Token: 0x020003F1 RID: 1009
	public class ExploreStairs : TutorialStep
	{
		// Token: 0x060025C1 RID: 9665 RVA: 0x000C12A2 File Offset: 0x000BF4A2
		public override bool BeforePlayerEnterCell(Cell cell)
		{
			return (cell != null && cell.ParentZone.Z == 10) || base.ConstrainToCurrentZone(cell);
		}

		// Token: 0x060025C2 RID: 9666 RVA: 0x000C12BF File Offset: 0x000BF4BF
		public override bool AllowSelectedPopupCommand(string title, QudMenuItem command)
		{
			return true;
		}

		// Token: 0x060025C3 RID: 9667 RVA: 0x000C12C4 File Offset: 0x000BF4C4
		public override void LateUpdate()
		{
			if (this.stairsUp == null || this.stairsCell == null)
			{
				this.stairsUp = The.Player.CurrentZone.FindObjectsWithPart("StairsUp").FirstOrDefault<GameObject>();
				GameObject gameObject = this.stairsUp;
				this.stairsCell = ((gameObject != null) ? gameObject.CurrentCell : null);
			}
			this.manager.ClearHighlight();
			if (base.CurrentGameView == "Stage")
			{
				if (The.Player.CurrentZone.Z == 10)
				{
					TutorialManager.AdvanceStep(new MakeCamp());
					return;
				}
				if (this.stairsUp != null)
				{
					if (The.PlayerCell.DistanceTo(this.stairsUp) <= 1)
					{
						if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
						{
							this.manager.HighlightByCID("Stage:TopButtonBar:UpButton", "Ascend.\n\nPress ~CmdMoveU.", "sw", 64, 64, 0f, "horiz");
							return;
						}
						this.manager.HighlightByCID("Stage:TopButtonBar:UpButton", "Ascend.\n\nYou can click this button or press ~CmdMoveU.", "sw", 64, 64, 0f, "horiz");
						return;
					}
					else
					{
						if (!this.saw)
						{
							GameObject gameObject2 = this.stairsUp;
							if (gameObject2 == null || !gameObject2.IsVisible())
							{
								this.manager.HighlightCell(48, 17, "Keep exploring.", "nw", 0f, 0f, 0f);
								return;
							}
						}
						this.saw = true;
						if (this.stairsCell != null)
						{
							this.manager.HighlightCell(this.stairsCell.X, this.stairsCell.Y, "Finally, a staircase. Walk over to it.", "nw", 0f, 0f, 0f);
							return;
						}
					}
				}
			}
		}

		// Token: 0x040012EC RID: 4844
		public GameObject stairsUp;

		// Token: 0x040012ED RID: 4845
		private Cell stairsCell;

		// Token: 0x040012EE RID: 4846
		private bool saw;
	}
}
