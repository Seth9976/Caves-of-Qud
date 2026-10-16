using System;
using System.Collections.Generic;

namespace XRL.World.AI.GoalHandlers
{
	// Token: 0x02000B83 RID: 2947
	[Serializable]
	public class FleeLocation : IMovementGoal
	{
		// Token: 0x060075E6 RID: 30182 RVA: 0x002C3A0C File Offset: 0x002C1C0C
		public FleeLocation()
		{
		}

		// Token: 0x060075E7 RID: 30183 RVA: 0x002C3A2D File Offset: 0x002C1C2D
		public FleeLocation(Cell Target = null, int Duration = -1, bool Panicked = false, bool LocalOnly = false)
			: this()
		{
			this.Target = Target;
			this.Duration = Duration;
			this.Panicked = Panicked;
			this.LocalOnly = LocalOnly;
		}

		// Token: 0x060075E8 RID: 30184 RVA: 0x002C3A52 File Offset: 0x002C1C52
		public override bool CanFight()
		{
			return false;
		}

		// Token: 0x060075E9 RID: 30185 RVA: 0x002C3A55 File Offset: 0x002C1C55
		public override bool IsNonAggressive()
		{
			return true;
		}

		// Token: 0x060075EA RID: 30186 RVA: 0x002C3A58 File Offset: 0x002C1C58
		public override bool IsFleeing()
		{
			return true;
		}

		// Token: 0x060075EB RID: 30187 RVA: 0x002C3A5B File Offset: 0x002C1C5B
		public override void Create()
		{
			base.Think("I'm trying to flee from a location!");
		}

		// Token: 0x060075EC RID: 30188 RVA: 0x002C3A68 File Offset: 0x002C1C68
		public override bool Finished()
		{
			return this.Duration == 0;
		}

		// Token: 0x060075ED RID: 30189 RVA: 0x002C3A73 File Offset: 0x002C1C73
		public bool TryRetreatAbilities()
		{
			return AICommandList.HandleCommandList(AIGetRetreatAbilityListEvent.GetFor(base.ParentObject, null, null, this.Target, -1, 0), base.ParentObject, null, null, 0);
		}

		// Token: 0x060075EE RID: 30190 RVA: 0x002C3A98 File Offset: 0x002C1C98
		public override void TakeAction()
		{
			if (this.Duration > 0)
			{
				this.Duration--;
			}
			else if (this.Duration == 0)
			{
				return;
			}
			Cell currentCell = base.ParentObject.CurrentCell;
			if (currentCell == null)
			{
				base.Think("I don't have a location!");
				base.FailToParent();
				return;
			}
			if (!this.Panicked && this.TryRetreatAbilities())
			{
				return;
			}
			string directionFromCell = this.Target.GetDirectionFromCell(currentCell, false);
			Cell cell = null;
			int num = 268435456;
			int num2;
			if (this.Panicked)
			{
				num2 = 50;
				if (directionFromCell == ".")
				{
					cell = currentCell.GetRandomLocalAdjacentCell() ?? currentCell;
				}
				else
				{
					cell = currentCell.GetCellFromDirectionGlobal(directionFromCell, true, true);
				}
			}
			else
			{
				num2 = 10;
				List<Cell> directionAndAdjacentCells = currentCell.GetDirectionAndAdjacentCells(directionFromCell, this.LocalOnly, true);
				if (directionAndAdjacentCells.Count > 3)
				{
					directionAndAdjacentCells.ShuffleInPlace<Cell>(null);
				}
				int num3 = int.MaxValue;
				foreach (Cell cell2 in directionAndAdjacentCells)
				{
					if (cell2 != currentCell && !this.Entered.Contains(cell2) && cell2.NavigationWeight(base.ParentObject, ref num) < num2)
					{
						int totalAdjacentHostileDifficultyLevel = cell2.GetTotalAdjacentHostileDifficultyLevel(base.ParentObject);
						if (totalAdjacentHostileDifficultyLevel < num3)
						{
							cell = cell2;
							num3 = totalAdjacentHostileDifficultyLevel;
							if (num3 <= 0)
							{
								break;
							}
						}
					}
				}
			}
			if (cell != null && !this.Entered.Contains(cell) && cell.NavigationWeight(base.ParentObject, ref num) < num2)
			{
				this.ProcessDestination(cell, currentCell);
				return;
			}
			int num4 = this.Target.PathDistanceTo(currentCell);
			foreach (Cell cell3 in currentCell.GetLocalAdjacentCells(1, false).ShuffleInPlace<Cell>(null))
			{
				if (!this.Entered.Contains(cell3) && this.Target.PathDistanceTo(cell3) >= num4 && cell3.NavigationWeight(base.ParentObject, ref num) < num2)
				{
					this.ProcessDestination(cell3, currentCell);
					return;
				}
			}
			base.Think("I can't find anyplace to flee!");
			base.FailToParent();
		}

		// Token: 0x060075EF RID: 30191 RVA: 0x002C3CCC File Offset: 0x002C1ECC
		private void ProcessDestination(Cell C, Cell From)
		{
			this.PushChildGoal(new Step(From.GetDirectionFromCell(C, false), false, this.Panicked, false, false, null, false));
			this.Entered.Add(C);
			if (this.ParentBrain.StartingCell != null && this.ParentBrain.StartingCell.Equals(this.Target))
			{
				this.ParentBrain.StartingCell = new GlobalLocation(C);
			}
		}

		// Token: 0x040035FC RID: 13820
		public Cell Target;

		// Token: 0x040035FD RID: 13821
		public int Duration = -1;

		// Token: 0x040035FE RID: 13822
		public bool Panicked;

		// Token: 0x040035FF RID: 13823
		public bool LocalOnly = true;

		// Token: 0x04003600 RID: 13824
		public List<Cell> Entered = new List<Cell>();
	}
}
