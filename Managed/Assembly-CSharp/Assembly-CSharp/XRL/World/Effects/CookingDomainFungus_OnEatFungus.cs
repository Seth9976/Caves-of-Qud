using System;

namespace XRL.World.Effects
{
	// Token: 0x02000CB7 RID: 3255
	public class CookingDomainFungus_OnEatFungus : ProceduralCookingEffectWithTrigger
	{
		// Token: 0x06008213 RID: 33299 RVA: 0x002F7280 File Offset: 0x002F5480
		public override string GetTriggerDescription()
		{
			return "whenever @thisCreature eat@s a mushroom,";
		}

		// Token: 0x06008214 RID: 33300 RVA: 0x002F7287 File Offset: 0x002F5487
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("Eating");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008215 RID: 33301 RVA: 0x002F729C File Offset: 0x002F549C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "Eating")
			{
				GameObject gameObject = E.GetParameter("Food") as GameObject;
				if (gameObject == null)
				{
					return true;
				}
				if (gameObject.GetBlueprint(true).InheritsFrom("Mushroom") || gameObject.HasTag("Mushroom"))
				{
					base.Trigger();
				}
			}
			return base.FireEvent(E);
		}
	}
}
