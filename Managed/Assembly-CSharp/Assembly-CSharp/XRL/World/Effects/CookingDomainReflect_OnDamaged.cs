using System;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000CEF RID: 3311
	public class CookingDomainReflect_OnDamaged : ProceduralCookingEffectWithTrigger
	{
		// Token: 0x06008344 RID: 33604 RVA: 0x002FA6C5 File Offset: 0x002F88C5
		public override void Init(GameObject target)
		{
			this.Tier = Stat.Random(8, 10);
			base.Init(target);
		}

		// Token: 0x06008345 RID: 33605 RVA: 0x002FA6DC File Offset: 0x002F88DC
		public override string GetTriggerDescription()
		{
			return "whenever @thisCreature take@s damage, there's a " + this.Tier.ToString() + "% chance";
		}

		// Token: 0x06008346 RID: 33606 RVA: 0x002FA6F8 File Offset: 0x002F88F8
		public override string GetTemplatedTriggerDescription()
		{
			return "whenever @thisCreature take@s damage, there's a 8-10% chance";
		}

		// Token: 0x06008347 RID: 33607 RVA: 0x002FA6FF File Offset: 0x002F88FF
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("TookDamage");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008348 RID: 33608 RVA: 0x002FA714 File Offset: 0x002F8914
		public override bool FireEvent(Event E)
		{
			if (E.ID == "TookDamage" && Stat.Random(1, 100) <= this.Tier)
			{
				base.Trigger();
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003963 RID: 14691
		public int Tier;
	}
}
