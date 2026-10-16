using System;
using XRL.World.AI;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x020010E6 RID: 4326
	[Serializable]
	public class LifeDrainOnHit : IPart
	{
		// Token: 0x0600ABA4 RID: 43940 RVA: 0x003EE2A6 File Offset: 0x003EC4A6
		public LifeDrainOnHit()
		{
		}

		// Token: 0x0600ABA5 RID: 43941 RVA: 0x003EE2C8 File Offset: 0x003EC4C8
		public LifeDrainOnHit(string Damage, int Chance)
			: this()
		{
			this.Damage = Damage;
			this.Chance = Chance;
		}

		// Token: 0x0600ABA6 RID: 43942 RVA: 0x003EE2DE File Offset: 0x003EC4DE
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600ABA7 RID: 43943 RVA: 0x003EE2E1 File Offset: 0x003EC4E1
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("WeaponHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600ABA8 RID: 43944 RVA: 0x003EE2F8 File Offset: 0x003EC4F8
		public override bool FireEvent(Event E)
		{
			if (E.ID == "WeaponHit")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Attacker");
				GameObject gameObjectParameter2 = E.GetGameObjectParameter("Defender");
				GameObject gameObjectParameter3 = E.GetGameObjectParameter("Weapon");
				GameObject gameObjectParameter4 = E.GetGameObjectParameter("Projectile");
				if (GameObject.Validate(ref gameObjectParameter2))
				{
					GameObject gameObject = gameObjectParameter;
					GameObject gameObject2 = gameObjectParameter3;
					string text = "Part LifeDrainOnHit Activation";
					GameObject gameObject3 = gameObjectParameter2;
					GameObject gameObject4 = gameObjectParameter4;
					if (GetSpecialEffectChanceEvent.GetFor(gameObject, gameObject2, text, this.Chance, gameObject3, gameObject4, true, false).in100())
					{
						gameObjectParameter2.ApplyEffect(new LifeDrain(2, 10, this.Damage, gameObjectParameter, this.RealityDistortionBased), null);
						if (!gameObjectParameter2.IsHostileTowards(gameObjectParameter))
						{
							gameObjectParameter2.AddOpinion<OpinionAttack>(gameObjectParameter, gameObjectParameter3, 1f);
						}
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x040046E6 RID: 18150
		public string Damage = "15-20";

		// Token: 0x040046E7 RID: 18151
		public int Chance = 100;

		// Token: 0x040046E8 RID: 18152
		public bool RealityDistortionBased = true;
	}
}
