using System;
using XRL.World.AI.GoalHandlers;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000E2C RID: 3628
	[Serializable]
	public class AIShoreLounging : AIBehaviorPart
	{
		// Token: 0x06009367 RID: 37735 RVA: 0x00351719 File Offset: 0x0034F919
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<AIBoredEvent>.ID;
		}

		// Token: 0x06009368 RID: 37736 RVA: 0x00351730 File Offset: 0x0034F930
		public override bool HandleEvent(AIBoredEvent E)
		{
			Cell currentCell = this.ParentObject.CurrentCell;
			if (currentCell.HasAquaticSupportFor(this.ParentObject))
			{
				if (this.GoToShoreChance.in100())
				{
					currentCell.GetPassableConnectedAdjacentCells(this.Range, true, null, true);
					Cell firstPassableConnectedAdjacentCell = currentCell.GetFirstPassableConnectedAdjacentCell(this.Range, true, new Predicate<Cell>(this.IsShore), this.ParentObject, true);
					if (firstPassableConnectedAdjacentCell != null)
					{
						this.ParentObject.Brain.PushGoal(new MoveTo(firstPassableConnectedAdjacentCell, false, false, 0, false, false, false, -1, -1, 95));
						if (this.ParentObject.HasEffect<Submerged>())
						{
							this.ParentObject.Brain.PushGoal(new Command("CommandSubmerge"));
						}
					}
				}
			}
			else if (this.GoBackToPoolChance.in100())
			{
				Cell cell = null;
				if (this.ParentObject.Brain.StartingCell != null)
				{
					cell = this.ParentObject.Brain.StartingCell.ResolveCell();
					if (cell != null && !cell.HasAquaticSupportFor(this.ParentObject))
					{
						cell = null;
					}
				}
				if (cell == null)
				{
					cell = currentCell.GetFirstPassableConnectedAdjacentCell(this.Range, true, new Predicate<Cell>(this.IsLurkable), this.ParentObject, true);
				}
				if (cell != null)
				{
					if (this.ParentObject.HasEffect<Submerged>())
					{
						this.ParentObject.Brain.PushGoal(new Command("CommandSubmerge"));
					}
					this.ParentObject.Brain.PushGoal(new MoveTo(cell, false, false, 0, false, false, false, -1, -1, 95));
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009369 RID: 37737 RVA: 0x003518B6 File Offset: 0x0034FAB6
		public bool IsShore(Cell C)
		{
			return !C.HasOpenLiquidVolume() && C.HasAdjacentAquaticSupportFor(this.ParentObject);
		}

		// Token: 0x0600936A RID: 37738 RVA: 0x003518CE File Offset: 0x0034FACE
		public bool IsLurkable(Cell C)
		{
			return C.HasAquaticSupportFor(this.ParentObject) && !C.HasAdjacentNonAquaticSupportFor(this.ParentObject);
		}

		// Token: 0x04003CF8 RID: 15608
		public int GoToShoreChance = 4;

		// Token: 0x04003CF9 RID: 15609
		public int GoBackToPoolChance = 10;

		// Token: 0x04003CFA RID: 15610
		public int Range = 20;
	}
}
