using System;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000CF8 RID: 3320
	public class CookingDomainRegenLowtier_OnDamaged : ProceduralCookingEffectWithTrigger
	{
		// Token: 0x06008369 RID: 33641 RVA: 0x002FAC94 File Offset: 0x002F8E94
		public override void Init(GameObject target)
		{
			this.Tier = ((this.Tier > 0) ? this.Tier : Stat.Random(8, 10));
			base.Init(target);
		}

		// Token: 0x0600836A RID: 33642 RVA: 0x002FACBC File Offset: 0x002F8EBC
		public override string GetTriggerDescription()
		{
			return "whenever @thisCreature take@s damage, there's a " + this.Tier.ToString() + "% chance";
		}

		// Token: 0x0600836B RID: 33643 RVA: 0x002FACD8 File Offset: 0x002F8ED8
		public override string GetTemplatedTriggerDescription()
		{
			return "whenever @thisCreature take@s damage, there's a 8-10% chance";
		}

		// Token: 0x0600836C RID: 33644 RVA: 0x002FACDF File Offset: 0x002F8EDF
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("TookDamage");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600836D RID: 33645 RVA: 0x002FACF4 File Offset: 0x002F8EF4
		public override bool FireEvent(Event E)
		{
			if (E.ID == "TookDamage" && Stat.Random(1, 100) <= this.Tier)
			{
				base.Trigger();
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003965 RID: 14693
		public int Tier;
	}
}
