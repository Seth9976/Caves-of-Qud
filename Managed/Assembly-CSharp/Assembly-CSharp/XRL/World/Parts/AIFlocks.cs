using System;
using System.Collections.Generic;
using XRL.Language;
using XRL.Rules;
using XRL.World.AI.GoalHandlers;

namespace XRL.World.Parts
{
	// Token: 0x02000E1E RID: 3614
	[Serializable]
	public class AIFlocks : AIBehaviorPart
	{
		// Token: 0x06009328 RID: 37672 RVA: 0x003505BE File Offset: 0x0034E7BE
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<AIBoredEvent>.ID;
		}

		// Token: 0x06009329 RID: 37673 RVA: 0x003505D4 File Offset: 0x0034E7D4
		public override bool HandleEvent(AIBoredEvent E)
		{
			this.Flock();
			return base.HandleEvent(E);
		}

		// Token: 0x0600932A RID: 37674 RVA: 0x003505E4 File Offset: 0x0034E7E4
		public bool Flock()
		{
			Cell currentCell = this.ParentObject.CurrentCell;
			if (currentCell == null)
			{
				return false;
			}
			List<GameObject> list = Event.NewGameObjectList();
			foreach (GameObject gameObject in currentCell.ParentZone.FastCombatSquareVisibility(currentCell.X, currentCell.Y, 8, this.ParentObject, null, false, false, true, true, null))
			{
				if (gameObject != this.ParentObject && gameObject.Blueprint == this.FlocksWith && gameObject.CurrentCell != null)
				{
					if (gameObject.CurrentCell.PathDistanceTo(currentCell) <= this.MinDistance)
					{
						this.ParentObject.Brain.Think("I'm too close to " + this.FlocksWith + ".");
						this.ParentObject.Brain.PushGoal(new Step(Directions.GetRandomDirection(), false, false, false, false, null, false));
						this.ParentObject.Brain.PushGoal(new Step(Directions.GetRandomDirection(), false, false, false, false, null, false));
						return false;
					}
					list.Add(gameObject);
				}
			}
			if (list.Count <= 0)
			{
				this.ParentObject.Brain.Think("I can't find " + Grammar.A(this.FlocksWith, false) + " to flock with.");
				return false;
			}
			int num = 0;
			int num2 = 0;
			foreach (GameObject gameObject2 in list)
			{
				num += gameObject2.CurrentCell.X;
				num2 += gameObject2.CurrentCell.Y;
			}
			num /= list.Count;
			num2 /= list.Count;
			Cell cell = this.ParentObject.CurrentCell.ParentZone.GetCell(num, num2);
			if (cell.PathDistanceTo(this.ParentObject.CurrentCell) > this.MaxDistance)
			{
				this.ParentObject.Brain.PushGoal(new MoveTo(cell, false, false, 0, false, false, false, 3, -1, 95));
			}
			return true;
		}

		// Token: 0x04003CDE RID: 15582
		public string FlocksWith = "Pig";

		// Token: 0x04003CDF RID: 15583
		public int MinDistance = 1;

		// Token: 0x04003CE0 RID: 15584
		public int MaxDistance = 3;
	}
}
