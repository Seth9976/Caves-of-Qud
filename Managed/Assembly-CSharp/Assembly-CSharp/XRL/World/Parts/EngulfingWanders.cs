using System;
using XRL.World.AI.GoalHandlers;

namespace XRL.World.Parts
{
	// Token: 0x02000EB0 RID: 3760
	[Serializable]
	public class EngulfingWanders : IPart
	{
		// Token: 0x060096E4 RID: 38628 RVA: 0x0036BCD9 File Offset: 0x00369ED9
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x060096E5 RID: 38629 RVA: 0x0036BCDC File Offset: 0x00369EDC
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeginTakeAction");
			base.Register(Object, Registrar);
		}

		// Token: 0x060096E6 RID: 38630 RVA: 0x0036BCF4 File Offset: 0x00369EF4
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeginTakeAction")
			{
				Engulfing part = this.ParentObject.GetPart<Engulfing>();
				if (part != null)
				{
					if (part.Engulfed != null)
					{
						if (!this.ParentObject.Brain.HasGoal("FleeLocation"))
						{
							this.ParentObject.Brain.Goals.Clear();
							this.ParentObject.Brain.PushGoal(new FleeLocation(this.ParentObject.CurrentCell, "2d4".RollCached(), false, false));
						}
					}
					else if (this.ParentObject.Brain.HasGoal("FleeLocation"))
					{
						this.ParentObject.Brain.Goals.Clear();
					}
				}
			}
			return base.FireEvent(E);
		}
	}
}
