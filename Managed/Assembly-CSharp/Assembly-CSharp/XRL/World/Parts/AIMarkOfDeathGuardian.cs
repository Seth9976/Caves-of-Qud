using System;

namespace XRL.World.Parts
{
	// Token: 0x02000E22 RID: 3618
	[Serializable]
	public class AIMarkOfDeathGuardian : AIBehaviorPart
	{
		// Token: 0x0600933A RID: 37690 RVA: 0x00350ADE File Offset: 0x0034ECDE
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register(PooledEvent<GetFeelingEvent>.ID, 0, false);
		}

		// Token: 0x0600933B RID: 37691 RVA: 0x00350AF0 File Offset: 0x0034ECF0
		public override bool HandleEvent(GetFeelingEvent E)
		{
			if (!E.Faction)
			{
				return base.HandleEvent(E);
			}
			if (E.Target.HasPart(typeof(AIMarkOfDeathGuardian)))
			{
				E.Feeling = 50;
				return false;
			}
			E.Feeling = (E.Target.HasMarkOfDeath(null) ? 0 : (-100));
			return false;
		}
	}
}
