using System;
using XRL.Rules;
using XRL.World.AI.GoalHandlers;

namespace XRL.World.Parts
{
	// Token: 0x02000E29 RID: 3625
	[Serializable]
	public class AISelfPreservation : AIBehaviorPart
	{
		// Token: 0x0600935A RID: 37722 RVA: 0x0035149E File Offset: 0x0034F69E
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AITakingAction");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600935B RID: 37723 RVA: 0x003514B4 File Offset: 0x0034F6B4
		public override bool FireEvent(Event E)
		{
			if (E.ID == "AITakingAction" && this.ParentObject.HasStat("Hitpoints"))
			{
				Statistic stat = this.ParentObject.GetStat("Hitpoints");
				if (stat.Penalty >= stat.BaseValue * (100 - this.Threshold) / 100 && !this.ParentObject.Brain.HasGoal("Retreat"))
				{
					this.ParentObject.Brain.Goals.Clear();
					this.ParentObject.Brain.PushGoal(new Retreat(Stat.Random(30, 50), null, 95));
					if (this.ParentObject.Brain.Goals.Count > 0)
					{
						this.ParentObject.Brain.Goals.Peek().TakeAction();
						return false;
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003CF6 RID: 15606
		public int Threshold = 35;
	}
}
