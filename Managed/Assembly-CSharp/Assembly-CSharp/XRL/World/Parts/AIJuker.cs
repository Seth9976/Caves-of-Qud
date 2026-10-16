using System;
using XRL.Rules;
using XRL.World.AI.GoalHandlers;

namespace XRL.World.Parts
{
	// Token: 0x02000E20 RID: 3616
	[Serializable]
	public class AIJuker : AIBehaviorPart
	{
		// Token: 0x06009332 RID: 37682 RVA: 0x0035091B File Offset: 0x0034EB1B
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AITakingAction");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009333 RID: 37683 RVA: 0x00350930 File Offset: 0x0034EB30
		public override bool FireEvent(Event E)
		{
			if (E.ID == "AITakingAction" && 25.in100())
			{
				this.ParentObject.Brain.PushGoal(new Step(Directions.GetRandomDirection(), false, false, false, false, null, false));
				if (this.ParentObject.Brain.Goals.Count > 0)
				{
					this.ParentObject.Brain.Goals.Peek().TakeAction();
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003CE1 RID: 15585
		public float Trigger = 0.35f;
	}
}
