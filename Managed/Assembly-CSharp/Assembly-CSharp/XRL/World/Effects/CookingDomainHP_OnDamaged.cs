using System;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000CBD RID: 3261
	public class CookingDomainHP_OnDamaged : ProceduralCookingEffectWithTrigger
	{
		// Token: 0x06008231 RID: 33329 RVA: 0x002F757C File Offset: 0x002F577C
		public override void Init(GameObject target)
		{
			this.Tier = ((this.Tier > 0) ? this.Tier : Stat.Random(8, 10));
			base.Init(target);
		}

		// Token: 0x06008232 RID: 33330 RVA: 0x002F75A4 File Offset: 0x002F57A4
		public override string GetTriggerDescription()
		{
			return "whenever @thisCreature take@s damage, there's a " + this.Tier.ToString() + "% chance";
		}

		// Token: 0x06008233 RID: 33331 RVA: 0x002F75C0 File Offset: 0x002F57C0
		public override string GetTemplatedTriggerDescription()
		{
			return "whenever @thisCreature take@s damage, there's a 8-10% chance";
		}

		// Token: 0x06008234 RID: 33332 RVA: 0x002F75C7 File Offset: 0x002F57C7
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("TookDamage");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008235 RID: 33333 RVA: 0x002F75DC File Offset: 0x002F57DC
		public override bool FireEvent(Event E)
		{
			if (E.ID == "TookDamage" && this.Tier.in100())
			{
				base.Trigger();
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003931 RID: 14641
		public int Tier;
	}
}
