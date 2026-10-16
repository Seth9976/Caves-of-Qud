using System;
using XRL.World.AI.GoalHandlers;

namespace XRL.World.Parts
{
	// Token: 0x02000E31 RID: 3633
	[Serializable]
	public class AITryKeepSteadyDistance : AIBehaviorPart
	{
		// Token: 0x06009381 RID: 37761 RVA: 0x00351B88 File Offset: 0x0034FD88
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009382 RID: 37762 RVA: 0x00351B8B File Offset: 0x0034FD8B
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeginTakeAction");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009383 RID: 37763 RVA: 0x00351BA0 File Offset: 0x0034FDA0
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeginTakeAction")
			{
				GameObject target = this.ParentObject.Target;
				if (target != null)
				{
					int num = target.DistanceTo(this.ParentObject);
					if (num < this.Distance)
					{
						this.ParentObject.Brain.PushGoal(new Flee(target, 2, false, true));
					}
					else if (num > this.Distance)
					{
						this.ParentObject.Brain.StepTowards(target.CurrentCell, false);
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003D00 RID: 15616
		public int Distance = 5;
	}
}
