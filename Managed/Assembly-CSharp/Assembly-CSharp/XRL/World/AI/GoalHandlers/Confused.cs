using System;
using XRL.Rules;

namespace XRL.World.AI.GoalHandlers
{
	// Token: 0x02000B7D RID: 2941
	[Serializable]
	public class Confused : GoalHandler
	{
		// Token: 0x060075BE RID: 30142 RVA: 0x002C2E74 File Offset: 0x002C1074
		public Confused(int Turns, int Level)
		{
			this.TurnsLeft = Turns;
		}

		// Token: 0x060075BF RID: 30143 RVA: 0x002C2E83 File Offset: 0x002C1083
		public override bool CanFight()
		{
			return false;
		}

		// Token: 0x060075C0 RID: 30144 RVA: 0x002C2E86 File Offset: 0x002C1086
		public override bool Finished()
		{
			return this.TurnsLeft <= 0;
		}

		// Token: 0x060075C1 RID: 30145 RVA: 0x002C2E94 File Offset: 0x002C1094
		public override void Create()
		{
		}

		// Token: 0x060075C2 RID: 30146 RVA: 0x002C2E98 File Offset: 0x002C1098
		public override void TakeAction()
		{
			if (this.TurnsLeft < 0)
			{
				base.Pop();
				return;
			}
			string randomDirection = Directions.GetRandomDirection();
			Cell localCellFromDirection = base.ParentObject.CurrentCell.GetLocalCellFromDirection(randomDirection, true);
			if (localCellFromDirection != null)
			{
				if (this.ParentBrain.LimitToAquatic() && !localCellFromDirection.HasAquaticSupportFor(base.ParentObject))
				{
					return;
				}
				if (!localCellFromDirection.IsEmpty())
				{
					return;
				}
			}
			base.Think("I'll stumble " + randomDirection);
			this.PushChildGoal(new Step(randomDirection, false, false, false, false, null, false));
			this.TurnsLeft--;
		}

		// Token: 0x040035E7 RID: 13799
		private int TurnsLeft;
	}
}
