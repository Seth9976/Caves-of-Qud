using System;
using XRL.World.Parts;

namespace XRL.World.Effects
{
	// Token: 0x02000CCC RID: 3276
	public class CookingDomainMedicinal_OnDrinkHoney : ProceduralCookingEffectWithTrigger
	{
		// Token: 0x06008273 RID: 33395 RVA: 0x002F7B86 File Offset: 0x002F5D86
		public override string GetTriggerDescription()
		{
			return "whenever @thisCreature drink@s honey,";
		}

		// Token: 0x06008274 RID: 33396 RVA: 0x002F7B8D File Offset: 0x002F5D8D
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("DrinkingFrom");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008275 RID: 33397 RVA: 0x002F7BA4 File Offset: 0x002F5DA4
		public override bool FireEvent(Event E)
		{
			if (E.ID == "DrinkingFrom")
			{
				LiquidVolume liquidVolume = (E.GetParameter("Container") as GameObject).LiquidVolume;
				if (liquidVolume.GetPrimaryLiquid() != null && liquidVolume.IsPureLiquid("honey", false))
				{
					int i = 0;
					int num = DrinkMagnifier.Magnify(base.Object, 1);
					while (i < num)
					{
						base.Trigger();
						i++;
					}
				}
			}
			return base.FireEvent(E);
		}
	}
}
