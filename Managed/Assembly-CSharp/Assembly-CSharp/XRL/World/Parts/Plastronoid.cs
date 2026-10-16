using System;
using System.Collections.Generic;
using XRL.Rules;
using XRL.World.AI.GoalHandlers;

namespace XRL.World.Parts
{
	// Token: 0x02000E87 RID: 3719
	[Serializable]
	public class Plastronoid : IPart
	{
		// Token: 0x060095DF RID: 38367 RVA: 0x00363E64 File Offset: 0x00362064
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AIBored");
			Registrar.Register("TakingAction");
			base.Register(Object, Registrar);
		}

		// Token: 0x060095E0 RID: 38368 RVA: 0x00363E84 File Offset: 0x00362084
		public override bool FireEvent(Event E)
		{
			if (E.ID == "AIBored")
			{
				this.PlastronoidFlock(100);
			}
			else if (E.ID == "TakingAction")
			{
				this.PlastronoidFlock(50);
			}
			return base.FireEvent(E);
		}

		// Token: 0x060095E1 RID: 38369 RVA: 0x00363EC4 File Offset: 0x003620C4
		public void PlastronoidFlock(int Chance = 100)
		{
			if (this.ParentObject.IsPlayer())
			{
				return;
			}
			if (this.ParentObject.IsNowhere())
			{
				return;
			}
			if (this.ParentObject.OnWorldMap())
			{
				return;
			}
			if (!Chance.in100())
			{
				return;
			}
			Cell currentCell = this.ParentObject.CurrentCell;
			if (currentCell == null)
			{
				return;
			}
			Zone parentZone = currentCell.ParentZone;
			if (parentZone == null)
			{
				return;
			}
			Brain brain = this.ParentObject.Brain;
			if (brain == null)
			{
				return;
			}
			if (currentCell.X % 2 == 1)
			{
				Cell cellFromDirection = currentCell.GetCellFromDirection("W", true);
				if (cellFromDirection != null && cellFromDirection.IsEmptyOfSolidFor(this.ParentObject, true))
				{
					brain.PushGoal(new Step("W", false, false, false, false, null, false));
					return;
				}
				Cell cellFromDirection2 = currentCell.GetCellFromDirection("E", true);
				if (cellFromDirection2 != null && cellFromDirection2.IsEmptyOfSolidFor(this.ParentObject, true))
				{
					brain.PushGoal(new Step("E", false, false, false, false, null, false));
					return;
				}
				brain.PushGoal(new Step(Directions.GetRandomDirection(), false, false, false, false, null, false));
			}
			else if (currentCell.Y % 2 == 1)
			{
				Cell cellFromDirection3 = currentCell.GetCellFromDirection("N", true);
				if (cellFromDirection3 != null && cellFromDirection3.IsEmptyOfSolidFor(this.ParentObject, true))
				{
					brain.PushGoal(new Step("N", false, false, false, false, null, false));
					return;
				}
				Cell cellFromDirection4 = currentCell.GetCellFromDirection("S", true);
				if (cellFromDirection4 != null && cellFromDirection4.IsEmptyOfSolidFor(this.ParentObject, true))
				{
					brain.PushGoal(new Step("S", false, false, false, false, null, false));
					return;
				}
				brain.PushGoal(new Step(Directions.GetRandomDirection(), false, false, false, false, null, false));
			}
			List<GameObject> list = Event.NewGameObjectList();
			foreach (GameObject gameObject in parentZone.FastCombatSquareVisibility(currentCell.X, currentCell.Y, 8, this.ParentObject, null, false, true, true, true, null))
			{
				if (gameObject != this.ParentObject && gameObject.Blueprint == this.FlocksWith && gameObject.CurrentCell != null)
				{
					if (gameObject.CurrentCell.PathDistanceTo(currentCell) <= this.MinDistance)
					{
						brain.Think("I'm too close to " + this.FlocksWith + ".");
						brain.PushGoal(new Step(Directions.GetRandomDirection(), false, false, false, false, null, false));
						brain.PushGoal(new Step(Directions.GetRandomDirection(), false, false, false, false, null, false));
						return;
					}
					list.Add(gameObject);
				}
			}
			if (list.Count > 0)
			{
				int num = 0;
				int num2 = 0;
				foreach (GameObject gameObject2 in list)
				{
					num += gameObject2.CurrentCell.X;
					num2 += gameObject2.CurrentCell.Y;
				}
				num /= list.Count;
				num2 /= list.Count;
				Cell cell = parentZone.GetCell(num, num2);
				if (brain.Target != null && brain.Target.CurrentCell != null)
				{
					cell = brain.Target.CurrentCell;
				}
				if (cell.PathDistanceTo(currentCell) > this.MaxDistance)
				{
					brain.PushGoal(new MoveTo(cell, false, false, 0, false, false, false, 3, -1, 95));
					return;
				}
			}
			else
			{
				brain.Think("I can't find a creature to flock with.");
			}
		}

		// Token: 0x04003DFA RID: 15866
		public string FlocksWith = "Plastronoid";

		// Token: 0x04003DFB RID: 15867
		public int MinDistance = 1;

		// Token: 0x04003DFC RID: 15868
		public int MaxDistance = 1;
	}
}
