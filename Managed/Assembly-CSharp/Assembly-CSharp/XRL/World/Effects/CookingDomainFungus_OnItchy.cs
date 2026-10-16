using System;

namespace XRL.World.Effects
{
	// Token: 0x02000CB6 RID: 3254
	public class CookingDomainFungus_OnItchy : ProceduralCookingEffectWithTrigger
	{
		// Token: 0x0600820F RID: 33295 RVA: 0x002F723B File Offset: 0x002F543B
		public override string GetTriggerDescription()
		{
			return "whenever @thisCreature get@s itchy skin,";
		}

		// Token: 0x06008210 RID: 33296 RVA: 0x002F7242 File Offset: 0x002F5442
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeforeApplyFungalInfection");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008211 RID: 33297 RVA: 0x002F7257 File Offset: 0x002F5457
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeforeApplyFungalInfection")
			{
				base.Trigger();
			}
			return base.FireEvent(E);
		}
	}
}
