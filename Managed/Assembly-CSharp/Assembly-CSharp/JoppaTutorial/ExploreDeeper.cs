using System;
using Qud.UI;
using XRL;
using XRL.World;

namespace JoppaTutorial
{
	// Token: 0x020003EF RID: 1007
	public class ExploreDeeper : TutorialStep
	{
		// Token: 0x060025B6 RID: 9654 RVA: 0x000C0DB0 File Offset: 0x000BEFB0
		public override bool BeforePlayerEnterCell(Cell cell)
		{
			string text;
			if (cell == null)
			{
				text = null;
			}
			else
			{
				Zone parentZone = cell.ParentZone;
				text = ((parentZone != null) ? parentZone.ZoneID : null);
			}
			if (!(text == "JoppaWorld.11.24.1.1.11"))
			{
				string text2;
				if (cell == null)
				{
					text2 = null;
				}
				else
				{
					Zone parentZone2 = cell.ParentZone;
					text2 = ((parentZone2 != null) ? parentZone2.ZoneID : null);
				}
				if (!(text2 == "JoppaWorld.11.24.1.0.11"))
				{
					return base.BeforePlayerEnterCell(cell);
				}
			}
			return true;
		}

		// Token: 0x060025B7 RID: 9655 RVA: 0x000C0E0E File Offset: 0x000BF00E
		public override bool AllowSelectedPopupCommand(string title, QudMenuItem command)
		{
			return true;
		}

		// Token: 0x060025B8 RID: 9656 RVA: 0x000C0E14 File Offset: 0x000BF014
		public override void LateUpdate()
		{
			this.manager.ClearHighlight();
			if (base.CurrentGameView == "Stage")
			{
				GameObject player = The.Player;
				if (((player != null) ? player.CurrentCell : null) != null)
				{
					if (The.Player.CurrentCell.Y == 0)
					{
						this.manager.HighlightCell(41, -1, "The whole game world is a series of contiguous maps. Our passage continues off screen. Let's follow it. Walk north.", "nw", 0f, 0f, 0f);
					}
					else if (The.Player.CurrentCell.Y > 8)
					{
						this.manager.HighlightCell(41, 8, "Let's move on. Keep exploring.", "nw", 0f, 0f, 0f);
					}
					else
					{
						this.manager.HighlightCell(41, -1, "Let's move on. Keep exploring.", "nw", 0f, 0f, 0f);
					}
					if (The.Player.CurrentZone.Y == 0)
					{
						TutorialManager.AdvanceStep(new BattleRemains());
					}
				}
			}
		}

		// Token: 0x040012E5 RID: 4837
		public GameObject chemCell;

		// Token: 0x040012E6 RID: 4838
		public bool infoShown;

		// Token: 0x040012E7 RID: 4839
		public bool snapjawDead;
	}
}
