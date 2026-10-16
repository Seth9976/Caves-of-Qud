using System;

namespace XRL.World.Parts
{
	// Token: 0x020010DB RID: 4315
	[Serializable]
	public class DrunkOnHit : IPart
	{
		// Token: 0x0600AB59 RID: 43865 RVA: 0x003EB392 File Offset: 0x003E9592
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("WeaponHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600AB5A RID: 43866 RVA: 0x003EB3A8 File Offset: 0x003E95A8
		public override bool FireEvent(Event E)
		{
			if (E.ID == "WeaponHit")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Attacker");
				GameObject gameObjectParameter2 = E.GetGameObjectParameter("Defender");
				GameObject parentObject = this.ParentObject;
				string text = "Part DrunkOnHit Activation";
				GameObject gameObject = gameObjectParameter2;
				GetSpecialEffectChanceEvent.GetFor(gameObjectParameter, parentObject, text, this.Chance, gameObject, null, true, false);
				if (this.Chance.in100())
				{
					for (int i = 0; i < 4; i++)
					{
						LiquidVolume.GetLiquid("wine").Drank(null, 0, gameObjectParameter2, Event.NewStringBuilder(null));
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x040046AC RID: 18092
		public int Chance;
	}
}
