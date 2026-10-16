using System;
using System.Collections.Generic;

namespace XRL.World.AI.GoalHandlers
{
	// Token: 0x02000B82 RID: 2946
	[Serializable]
	public class Flee : IMovementGoal
	{
		// Token: 0x060075DC RID: 30172 RVA: 0x002C35F2 File Offset: 0x002C17F2
		public Flee()
		{
		}

		// Token: 0x060075DD RID: 30173 RVA: 0x002C3613 File Offset: 0x002C1813
		public Flee(GameObject Target = null, int Duration = -1, bool Panicked = false, bool LocalOnly = true)
			: this()
		{
			this.Target = Target;
			this.Duration = Duration;
			this.Panicked = Panicked;
			this.LocalOnly = LocalOnly;
		}

		// Token: 0x060075DE RID: 30174 RVA: 0x002C3638 File Offset: 0x002C1838
		public override bool CanFight()
		{
			return false;
		}

		// Token: 0x060075DF RID: 30175 RVA: 0x002C363B File Offset: 0x002C183B
		public override bool IsNonAggressive()
		{
			return true;
		}

		// Token: 0x060075E0 RID: 30176 RVA: 0x002C363E File Offset: 0x002C183E
		public override bool IsFleeing()
		{
			return true;
		}

		// Token: 0x060075E1 RID: 30177 RVA: 0x002C3644 File Offset: 0x002C1844
		public override void Create()
		{
			string text = "I'm trying to flee from ";
			GameObject target = this.Target;
			base.Think(text + (((target != null) ? target.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false) : null) ?? "nothing") + "!");
		}

		// Token: 0x060075E2 RID: 30178 RVA: 0x002C369F File Offset: 0x002C189F
		public override bool Finished()
		{
			return this.Duration == 0;
		}

		// Token: 0x060075E3 RID: 30179 RVA: 0x002C36AA File Offset: 0x002C18AA
		public bool TryRetreatAbilities()
		{
			return AICommandList.HandleCommandList(AIGetRetreatAbilityListEvent.GetFor(base.ParentObject, this.Target, null, null, -1, 0), base.ParentObject, this.Target, null, 0);
		}

		// Token: 0x060075E4 RID: 30180 RVA: 0x002C36D4 File Offset: 0x002C18D4
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
			if (!GameObject.Validate(ref this.Target))
			{
				base.Think("I don't have a target any more!");
				this.Target = null;
				base.FailToParent();
				return;
			}
			if (base.ParentObject.IsPlayer())
			{
				GoalHandler.AddPlayerMessage("You are fleeing from " + this.Target.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, true, null, false, null, false) + "!", 'r', true);
			}
			Cell currentCell = base.ParentObject.CurrentCell;
			if (currentCell == null)
			{
				base.Think("I don't have a location!");
				this.Target = null;
				base.FailToParent();
				return;
			}
			Cell currentCell2 = this.Target.GetCurrentCell();
			if (currentCell2 != null)
			{
				Zone parentZone = currentCell2.ParentZone;
				if (((parentZone != null) ? parentZone.ZoneID : null) != null)
				{
					if (this.LocalOnly && currentCell.ParentZone != currentCell2.ParentZone)
					{
						base.Think("My target is in another zone!");
						this.Target = null;
						base.FailToParent();
						return;
					}
					if (!this.Panicked && this.TryRetreatAbilities())
					{
						return;
					}
					string directionFromCell = currentCell2.GetDirectionFromCell(currentCell, false);
					Cell cell = null;
					int num = 268435456;
					int num2;
					if (this.Panicked)
					{
						num2 = 50;
						if (directionFromCell == ".")
						{
							cell = currentCell.GetRandomLocalAdjacentCell();
						}
						else
						{
							cell = currentCell.GetCellFromDirectionGlobal(directionFromCell, this.LocalOnly, true);
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
					int num4 = currentCell2.PathDistanceTo(currentCell);
					foreach (Cell cell3 in currentCell.GetLocalAdjacentCells(1, false).ShuffleInPlace<Cell>(null))
					{
						if (!this.Entered.Contains(cell3) && currentCell2.PathDistanceTo(cell3) >= num4 && cell3.NavigationWeight(base.ParentObject, ref num) < num2)
						{
							this.ProcessDestination(cell3, currentCell);
							return;
						}
					}
					base.Think("I can't find anyplace to flee!");
					base.FailToParent();
					return;
				}
			}
			base.Think("My target has no location!");
			this.Target = null;
			base.FailToParent();
		}

		// Token: 0x060075E5 RID: 30181 RVA: 0x002C39E0 File Offset: 0x002C1BE0
		private void ProcessDestination(Cell C, Cell From)
		{
			this.PushChildGoal(new Step(From.GetDirectionFromCell(C, false), false, this.Panicked, false, false, null, false));
			this.Entered.Add(C);
		}

		// Token: 0x040035F7 RID: 13815
		public GameObject Target;

		// Token: 0x040035F8 RID: 13816
		public int Duration = -1;

		// Token: 0x040035F9 RID: 13817
		public bool Panicked;

		// Token: 0x040035FA RID: 13818
		public bool LocalOnly = true;

		// Token: 0x040035FB RID: 13819
		private List<Cell> Entered = new List<Cell>();
	}
}
