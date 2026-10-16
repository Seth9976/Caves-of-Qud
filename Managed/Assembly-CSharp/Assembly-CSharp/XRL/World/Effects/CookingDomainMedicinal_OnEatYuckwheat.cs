using System;

namespace XRL.World.Effects
{
	// Token: 0x02000CCB RID: 3275
	public class CookingDomainMedicinal_OnEatYuckwheat : ProceduralCookingEffectWithTrigger
	{
		// Token: 0x0600826F RID: 33391 RVA: 0x002F7AE6 File Offset: 0x002F5CE6
		public override string GetTriggerDescription()
		{
			return "whenever @thisCreature eat@s an unfermented yuckwheat stem,";
		}

		// Token: 0x06008270 RID: 33392 RVA: 0x002F7AED File Offset: 0x002F5CED
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("Eating");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008271 RID: 33393 RVA: 0x002F7B04 File Offset: 0x002F5D04
		public override bool FireEvent(Event E)
		{
			if (E.ID == "Eating")
			{
				GameObject gameObject = E.GetParameter("Food") as GameObject;
				if (gameObject == null)
				{
					return true;
				}
				if (gameObject.GetBlueprint(true).Name == "Yuckwheat Stem" || gameObject.GetBlueprint(true).InheritsFrom("Yuckwheat Stem") || gameObject.HasTag("Yuckwheat Stem"))
				{
					base.Trigger();
				}
			}
			return base.FireEvent(E);
		}
	}
}
