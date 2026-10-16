using System;
using XRL.Liquids;

namespace XRL.World.Parts
{
	// Token: 0x020010CF RID: 4303
	[Serializable]
	public class BoomOnHit : IPart
	{
		// Token: 0x0600AB24 RID: 43812 RVA: 0x003EA2F4 File Offset: 0x003E84F4
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("WeaponHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600AB25 RID: 43813 RVA: 0x003EA30C File Offset: 0x003E850C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "WeaponHit")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Defender");
				GameObject gameObjectParameter2 = E.GetGameObjectParameter("Attacker");
				if (GetSpecialEffectChanceEvent.GetFor(gameObjectParameter2, this.ParentObject, "Part BoomOnHit Activation", this.Chance, null, null, true, false).in100())
				{
					LiquidNeutronFlux.Explode(gameObjectParameter, gameObjectParameter2, 0);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0400468D RID: 18061
		public int Chance;
	}
}
