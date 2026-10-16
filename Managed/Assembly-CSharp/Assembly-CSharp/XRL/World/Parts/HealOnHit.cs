using System;

namespace XRL.World.Parts
{
	// Token: 0x020010E3 RID: 4323
	[Serializable]
	public class HealOnHit : IPart
	{
		// Token: 0x0600AB8E RID: 43918 RVA: 0x003ECBC5 File Offset: 0x003EADC5
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("WeaponHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600AB8F RID: 43919 RVA: 0x003ECBDC File Offset: 0x003EADDC
		public override bool FireEvent(Event E)
		{
			if (E.ID == "WeaponHit")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Attacker");
				GameObject gameObjectParameter2 = E.GetGameObjectParameter("Defender");
				GameObject parentObject = this.ParentObject;
				string text = "Part HealOnHit Activation";
				GameObject gameObject = gameObjectParameter2;
				if (GetSpecialEffectChanceEvent.GetFor(gameObjectParameter, parentObject, text, this.Chance, gameObject, null, true, false).in100() && gameObjectParameter2.HasStat("Hitpoints") && gameObjectParameter2.IsOrganic)
				{
					gameObjectParameter2.Heal(this.Amount.RollCached(), true, true, false);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x040046CD RID: 18125
		public int Chance;

		// Token: 0x040046CE RID: 18126
		public string Amount = "15-25";
	}
}
