using System;
using System.Text;

namespace XRL.World.Parts
{
	// Token: 0x020010D7 RID: 4311
	[Serializable]
	public class DetonateOnHit : IPart
	{
		// Token: 0x0600AB45 RID: 43845 RVA: 0x003EADCC File Offset: 0x003E8FCC
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AttackerHit");
			Registrar.Register("WeaponHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600AB46 RID: 43846 RVA: 0x003EADEC File Offset: 0x003E8FEC
		public override bool FireEvent(Event E)
		{
			if (E.ID.EndsWith("Hit"))
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Attacker");
				GameObject gameObjectParameter2 = E.GetGameObjectParameter("Defender");
				GameObject gameObjectParameter3 = E.GetGameObjectParameter("Weapon");
				GameObject gameObjectParameter4 = E.GetGameObjectParameter("Projectile");
				if (GameObject.Validate(ref gameObjectParameter2))
				{
					GameObject gameObject = gameObjectParameter;
					GameObject gameObject2 = gameObjectParameter3;
					string text = "Part DetonateOnHit Activation";
					GameObject gameObject3 = gameObjectParameter2;
					GameObject gameObject4 = gameObjectParameter4;
					if (GetSpecialEffectChanceEvent.GetFor(gameObject, gameObject2, text, this.Chance, gameObject3, gameObject4, true, false).in100())
					{
						gameObjectParameter2.Explode(this.Force, gameObjectParameter, this.Damage, 1f, false, true, false, 0, null);
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600AB47 RID: 43847 RVA: 0x003EAE8E File Offset: 0x003E908E
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600AB48 RID: 43848 RVA: 0x003EAEA4 File Offset: 0x003E90A4
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(new Action<StringBuilder>(this.AppendEffect));
			return base.HandleEvent(E);
		}

		// Token: 0x0600AB49 RID: 43849 RVA: 0x003EAEC5 File Offset: 0x003E90C5
		public void AppendEffect(StringBuilder SB)
		{
			if (this.Chance <= 0)
			{
				return;
			}
			if (this.Chance < 100)
			{
				SB.Append(this.Chance).Append("% chance ");
			}
			SB.AppendCase("melee attacks cause a small explosion");
		}

		// Token: 0x040046A4 RID: 18084
		public string Damage = "2d6";

		// Token: 0x040046A5 RID: 18085
		public int Force = 10000;

		// Token: 0x040046A6 RID: 18086
		public int Chance;
	}
}
