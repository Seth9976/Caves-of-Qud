using System;
using System.Collections.Generic;
using XRL.Core;
using XRL.World.AI.GoalHandlers;

namespace XRL.World.Parts
{
	// Token: 0x02000E28 RID: 3624
	[Serializable]
	public class AISeekHealingPool : AIBehaviorPart
	{
		// Token: 0x06009357 RID: 37719 RVA: 0x0035130E File Offset: 0x0034F50E
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AITakingAction");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009358 RID: 37720 RVA: 0x00351324 File Offset: 0x0034F524
		public override bool FireEvent(Event E)
		{
			if (E.ID == "AITakingAction" && this.ParentObject.Statistics.ContainsKey("Hitpoints"))
			{
				Statistic statistic = this.ParentObject.Statistics["Hitpoints"];
				Cell currentCell = this.ParentObject.CurrentCell;
				if ((float)statistic.Penalty >= (float)statistic.BaseValue * this.Trigger && currentCell != null && !currentCell.HasHealingPool())
				{
					Zone parentZone = currentCell.ParentZone;
					if (parentZone != null)
					{
						List<Cell> cells = parentZone.GetCells((Cell C) => C.HasHealingPool());
						if (cells.Count > 0)
						{
							cells.Sort(new XRLCore.SortCellBydistanceToObject(this.ParentObject));
							this.ParentObject.Brain.Goals.Clear();
							this.ParentObject.Brain.PushGoal(new MoveTo(parentZone.ZoneID, cells[0].X, cells[0].Y, false, false, 0, false, false, false, -1, -1, 95));
							if (this.ParentObject.Brain.Goals.Count > 0)
							{
								this.ParentObject.Brain.Goals.Peek().TakeAction();
								return false;
							}
						}
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003CF5 RID: 15605
		public float Trigger = 0.35f;
	}
}
