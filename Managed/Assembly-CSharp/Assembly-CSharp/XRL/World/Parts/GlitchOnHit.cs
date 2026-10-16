using System;
using XRL.Liquids;

namespace XRL.World.Parts
{
	// Token: 0x02001124 RID: 4388
	[Serializable]
	public class GlitchOnHit : IPart
	{
		// Token: 0x0600AD9C RID: 44444 RVA: 0x00404590 File Offset: 0x00402790
		public override bool SameAs(IPart p)
		{
			return (p as GlitchOnHit).ChancePerThousand == this.ChancePerThousand && base.SameAs(p);
		}

		// Token: 0x0600AD9D RID: 44445 RVA: 0x004045AE File Offset: 0x004027AE
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600AD9E RID: 44446 RVA: 0x004045B1 File Offset: 0x004027B1
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("WeaponHit");
			Registrar.Register("AttackerAfterDamage");
			Registrar.Register("DealingMissileDamage");
			Registrar.Register("WeaponMissileWeaponHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600AD9F RID: 44447 RVA: 0x004045E8 File Offset: 0x004027E8
		public override bool FireEvent(Event E)
		{
			if (E.ID == "WeaponHit" || E.ID == "AttackerAfterDamage" || E.ID == "DealingMissileDamage" || E.ID == "WeaponMissileWeaponHit")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Attacker");
				GameObject gameObjectParameter2 = E.GetGameObjectParameter("Defender");
				Cell cell = ((gameObjectParameter2 != null) ? gameObjectParameter2.GetCurrentCell() : null);
				if (gameObjectParameter != null && cell != null)
				{
					GameObject gameObject = gameObjectParameter;
					GameObject parentObject = this.ParentObject;
					string text = "Part GlitchOnHit Activation";
					GameObject gameObject2 = gameObjectParameter2;
					if (GetSpecialEffectChanceEvent.GetFor(gameObject, parentObject, text, this.ChancePerThousand, gameObject2, null, false, true).in1000())
					{
						LiquidWarmStatic.GlitchObject(gameObjectParameter2);
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x040047DA RID: 18394
		public int ChancePerThousand = 1;
	}
}
