using System;
using XRL.Core;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x0200126A RID: 4714
	[Serializable]
	public class FactoryArm : IPart
	{
		// Token: 0x0600BADE RID: 47838 RVA: 0x00443AD0 File Offset: 0x00441CD0
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<EndTurnEvent>.ID;
		}

		// Token: 0x0600BADF RID: 47839 RVA: 0x00443AE8 File Offset: 0x00441CE8
		public override bool HandleEvent(EndTurnEvent E)
		{
			this.Counter++;
			if (this.Counter >= this.Frequency)
			{
				this.Counter = 0;
				Cell cellFromDirection = base.currentCell.GetCellFromDirection(this.Direction, true);
				Cell cellFromDirection2 = base.currentCell.GetCellFromDirection(Directions.GetOppositeDirection(this.Direction), true);
				if (cellFromDirection.IsPassable(null, true) && cellFromDirection2.GetCombatTarget(this.ParentObject, false, false, false, 0, null, null, null, null, null, false, false, null) != null)
				{
					for (int i = cellFromDirection2.Objects.Count - 1; i >= 0; i--)
					{
						GameObject gameObject = cellFromDirection2.Objects[i];
						if (gameObject.Physics != null && gameObject.CanBeInvoluntarilyMoved() && gameObject.PhaseAndFlightMatches(this.ParentObject))
						{
							gameObject.DirectMoveTo(cellFromDirection, 0, true, true, false, null);
							break;
						}
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BAE0 RID: 47840 RVA: 0x00443BC4 File Offset: 0x00441DC4
		public override bool Render(RenderEvent E)
		{
			if (this.Counter == this.Frequency - 1)
			{
				int num = XRLCore.CurrentFrame % 30;
				if (num > 0 && num < 15)
				{
					E.Tile = null;
					E.RenderString = Directions.GetArrowForDirection(this.Direction);
					E.ColorString = "&C";
					return false;
				}
			}
			return base.Render(E);
		}

		// Token: 0x04004AF9 RID: 19193
		public string Direction;

		// Token: 0x04004AFA RID: 19194
		public int Frequency;

		// Token: 0x04004AFB RID: 19195
		public int Counter;
	}
}
