using System;
using XRL.World.Parts;

namespace XRL.World.Effects
{
	// Token: 0x02000C60 RID: 3168
	public class OnDrinkWaterProceduralCookingTrigger : ProceduralCookingEffectWithTrigger
	{
		// Token: 0x060080D9 RID: 32985 RVA: 0x002F5475 File Offset: 0x002F3675
		public override string GetTriggerDescription()
		{
			return "whenever @thisCreature drink@s freshwater, there's a 25% chance";
		}

		// Token: 0x060080DA RID: 32986 RVA: 0x002F547C File Offset: 0x002F367C
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("Drank");
			base.Register(Object, Registrar);
		}

		// Token: 0x060080DB RID: 32987 RVA: 0x002F5494 File Offset: 0x002F3694
		public override bool FireEvent(Event E)
		{
			if (E.ID == "Drank" && base.Object != null && !base.Object.OnWorldMap() && E.GetIntParameter("WasFreshWater", 0) == 1 && 25.in100())
			{
				int i = 0;
				int num = DrinkMagnifier.Magnify(base.Object, 1);
				while (i < num)
				{
					base.Trigger();
					i++;
				}
			}
			return base.FireEvent(E);
		}
	}
}
