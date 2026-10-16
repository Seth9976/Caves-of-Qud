using System;

namespace XRL.World.Effects
{
	// Token: 0x02000CDB RID: 3291
	public class CookingDomainPlant_OnDamageByPlant : ProceduralCookingEffectWithTrigger
	{
		// Token: 0x060082BE RID: 33470 RVA: 0x002F8319 File Offset: 0x002F6519
		public override string GetTriggerDescription()
		{
			return "whenever @thisCreature take@s damage from a plant,";
		}

		// Token: 0x060082BF RID: 33471 RVA: 0x002F8320 File Offset: 0x002F6520
		public override string GetTemplatedTriggerDescription()
		{
			return "whenever @thisCreature take@s damage from a plant,";
		}

		// Token: 0x060082C0 RID: 33472 RVA: 0x002F8327 File Offset: 0x002F6527
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("TookDamage");
			base.Register(Object, Registrar);
		}

		// Token: 0x060082C1 RID: 33473 RVA: 0x002F833C File Offset: 0x002F653C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "TookDamage")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Attacker");
				Damage parameter = E.GetParameter<Damage>("Damage");
				if ((gameObjectParameter != null && (gameObjectParameter.GetBlueprint(true).DescendsFrom("Plant") || gameObjectParameter.GetBlueprint(true).DescendsFrom("MutatedPlant") || gameObjectParameter.HasTagOrProperty("Plant"))) || (parameter != null && parameter.HasAttribute("Plant")))
				{
					base.Trigger();
				}
			}
			return base.FireEvent(E);
		}
	}
}
