using System;
using XRL.Rules;
using XRL.World.AI.GoalHandlers;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000E30 RID: 3632
	[Serializable]
	public class AITryKeepDistance : AIBehaviorPart
	{
		// Token: 0x0600937B RID: 37755 RVA: 0x00351A85 File Offset: 0x0034FC85
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetHostileWalkRadiusEvent.ID;
		}

		// Token: 0x0600937C RID: 37756 RVA: 0x00351A9B File Offset: 0x0034FC9B
		public override bool HandleEvent(GetHostileWalkRadiusEvent E)
		{
			if (!this.NotStuck || !E.Actor.HasEffect<Stuck>())
			{
				E.MaxRadius(2);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600937D RID: 37757 RVA: 0x00351AC0 File Offset: 0x0034FCC0
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600937E RID: 37758 RVA: 0x00351AC3 File Offset: 0x0034FCC3
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AICantAttackRange");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600937F RID: 37759 RVA: 0x00351AD8 File Offset: 0x0034FCD8
		public override bool FireEvent(Event E)
		{
			if (E.ID == "AICantAttackRange")
			{
				GameObject target = this.ParentObject.Target;
				if (target != null && (float)this.ParentObject.hitpoints > (float)this.ParentObject.baseHitpoints * this.AttackThreshold && (!this.NotStuck || !target.HasEffect<Stuck>()))
				{
					this.ParentObject.Brain.PushGoal(new Flee(target, Stat.Roll(this.Duration, null), false, this.LocalOnly));
					return false;
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003CFC RID: 15612
		public float AttackThreshold = 0.5f;

		// Token: 0x04003CFD RID: 15613
		public string Duration = "1d2";

		// Token: 0x04003CFE RID: 15614
		public bool NotStuck;

		// Token: 0x04003CFF RID: 15615
		public bool LocalOnly;
	}
}
