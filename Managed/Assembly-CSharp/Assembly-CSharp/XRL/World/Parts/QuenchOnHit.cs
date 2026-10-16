using System;

namespace XRL.World.Parts
{
	// Token: 0x020010EF RID: 4335
	[Serializable]
	public class QuenchOnHit : IPart
	{
		// Token: 0x0600ABC5 RID: 43973 RVA: 0x003EEDCE File Offset: 0x003ECFCE
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("WeaponHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600ABC6 RID: 43974 RVA: 0x003EEDE4 File Offset: 0x003ECFE4
		public override bool FireEvent(Event E)
		{
			if (E.ID == "WeaponHit")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Attacker");
				GameObject gameObjectParameter2 = E.GetGameObjectParameter("Defender");
				GameObject gameObjectParameter3 = E.GetGameObjectParameter("Projectile");
				GameObject parentObject = this.ParentObject;
				string text = "Part QuenchOnHit Activation";
				GameObject gameObject = gameObjectParameter2;
				GameObject gameObject2 = gameObjectParameter3;
				if (GetSpecialEffectChanceEvent.GetFor(gameObjectParameter, parentObject, text, this.Chance, gameObject, gameObject2, true, false).in100() && !gameObjectParameter2.FireEvent(new Event("AddWater", "Amount", 60000, "Forced", 1)))
				{
					return false;
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x040046FC RID: 18172
		public int Chance;
	}
}
