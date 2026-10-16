using System;

namespace XRL.World.Effects
{
	// Token: 0x02000D17 RID: 3351
	public class CookingDomainTongue_OnStuck : ProceduralCookingEffectWithTrigger
	{
		// Token: 0x060083FA RID: 33786 RVA: 0x002FBD08 File Offset: 0x002F9F08
		public override string GetTriggerDescription()
		{
			return "whenever @thisCreature get@s stuck, there's a 50% chance";
		}

		// Token: 0x060083FB RID: 33787 RVA: 0x002FBD0F File Offset: 0x002F9F0F
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EffectApplied");
			base.Register(Object, Registrar);
		}

		// Token: 0x060083FC RID: 33788 RVA: 0x002FBD24 File Offset: 0x002F9F24
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EffectApplied" && E.GetParameter("Effect") is Stuck && 50.in100())
			{
				base.Trigger();
			}
			return base.FireEvent(E);
		}
	}
}
