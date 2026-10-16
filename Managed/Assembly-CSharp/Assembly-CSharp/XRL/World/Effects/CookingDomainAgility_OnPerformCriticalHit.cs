using System;

namespace XRL.World.Effects
{
	// Token: 0x02000C50 RID: 3152
	public class CookingDomainAgility_OnPerformCriticalHit : ProceduralCookingEffectWithTrigger
	{
		// Token: 0x06008098 RID: 32920 RVA: 0x002F4D44 File Offset: 0x002F2F44
		public override string GetTriggerDescription()
		{
			return "whenever @thisCreature perform@s a critical hit,";
		}

		// Token: 0x06008099 RID: 32921 RVA: 0x002F4D4B File Offset: 0x002F2F4B
		public override string GetTemplatedTriggerDescription()
		{
			return "whenever @thisCreature perform@s a critical hit,";
		}

		// Token: 0x0600809A RID: 32922 RVA: 0x002F4D52 File Offset: 0x002F2F52
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AttackerCriticalHit");
			Registrar.Register("MissileAttackerCriticalHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600809B RID: 32923 RVA: 0x002F4D72 File Offset: 0x002F2F72
		public override bool FireEvent(Event E)
		{
			if (E.ID == "AttackerCriticalHit" || E.ID == "MissileAttackerCriticalHit")
			{
				base.Trigger();
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003900 RID: 14592
		public int Tier;
	}
}
