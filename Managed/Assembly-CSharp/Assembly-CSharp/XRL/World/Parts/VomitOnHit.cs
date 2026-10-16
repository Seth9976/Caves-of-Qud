using System;
using System.Text;
using XRL.Liquids;

namespace XRL.World.Parts
{
	// Token: 0x020010FD RID: 4349
	[Serializable]
	public class VomitOnHit : IPart
	{
		// Token: 0x0600AC18 RID: 44056 RVA: 0x003F145F File Offset: 0x003EF65F
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600AC19 RID: 44057 RVA: 0x003F1462 File Offset: 0x003EF662
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("WeaponHit");
			Registrar.Register("AttackerAfterDamage");
			Registrar.Register("DealingMissileDamage");
			Registrar.Register("WeaponMissileWeaponHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600AC1A RID: 44058 RVA: 0x003F1498 File Offset: 0x003EF698
		public override bool FireEvent(Event E)
		{
			if (E.ID == "WeaponHit" || E.ID == "AttackerAfterDamage" || E.ID == "DealingMissileDamage" || E.ID == "WeaponMissileWeaponHit")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Attacker");
				GameObject gameObjectParameter2 = E.GetGameObjectParameter("Defender");
				GameObject gameObjectParameter3 = E.GetGameObjectParameter("Weapon");
				GameObject gameObjectParameter4 = E.GetGameObjectParameter("Projectile");
				if (GameObject.Validate(ref gameObjectParameter2))
				{
					GameObject gameObject = gameObjectParameter;
					GameObject gameObject2 = gameObjectParameter3;
					string text = "Part VomitOnHit Activation";
					GameObject gameObject3 = gameObjectParameter2;
					GameObject gameObject4 = gameObjectParameter4;
					int @for = GetSpecialEffectChanceEvent.GetFor(gameObject, gameObject2, text, this.Chance, gameObject3, gameObject4, true, false);
					if (@for.in100())
					{
						StringBuilder stringBuilder = Event.NewStringBuilder(null);
						BaseLiquid liquid = LiquidVolume.GetLiquid("putrid");
						for (int i = this.Hurls; i > 0; i--)
						{
							liquid.Drank(null, 0, gameObjectParameter2, stringBuilder);
							if (!@for.in100())
							{
								break;
							}
						}
						if (stringBuilder.Length > 0)
						{
							IComponent<GameObject>.AddPlayerMessage(stringBuilder.ToString(), null, true);
						}
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0400473D RID: 18237
		public int Hurls = 1;

		// Token: 0x0400473E RID: 18238
		public int Chance = 100;
	}
}
