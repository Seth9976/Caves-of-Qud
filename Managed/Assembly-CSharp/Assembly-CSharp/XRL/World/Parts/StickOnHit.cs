using System;
using System.Text;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x020010F5 RID: 4341
	[Serializable]
	public class StickOnHit : IPart
	{
		// Token: 0x0600ABE8 RID: 44008 RVA: 0x003F037C File Offset: 0x003EE57C
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AttackerHit");
			Registrar.Register("WeaponHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600ABE9 RID: 44009 RVA: 0x003F039C File Offset: 0x003EE59C
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
					string text = "Part StickOnHit Activation";
					GameObject gameObject3 = gameObjectParameter2;
					GameObject gameObject4 = gameObjectParameter4;
					if (GetSpecialEffectChanceEvent.GetFor(gameObject, gameObject2, text, this.Chance, gameObject3, gameObject4, true, false).in100())
					{
						gameObjectParameter2.ApplyEffect(new Stuck(this.Duration.RollCached(), this.SaveTarget, this.SaveVs, null, "stuck", "in", null, "Move", 0.25f, 1f, 1f, false, false), null);
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600ABEA RID: 44010 RVA: 0x003F0469 File Offset: 0x003EE669
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600ABEB RID: 44011 RVA: 0x003F047F File Offset: 0x003EE67F
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(new Action<StringBuilder>(this.AppendEffect));
			return base.HandleEvent(E);
		}

		// Token: 0x0600ABEC RID: 44012 RVA: 0x003F04A0 File Offset: 0x003EE6A0
		public void AppendEffect(StringBuilder SB)
		{
			if (this.Chance <= 0)
			{
				return;
			}
			SB.Append("Melee attacks cause enemies to get stuck for ").Append(this.Duration).Append(" turns");
		}

		// Token: 0x0400471E RID: 18206
		public string Duration = "5";

		// Token: 0x0400471F RID: 18207
		public int Chance;

		// Token: 0x04004720 RID: 18208
		public int SaveTarget = 15;

		// Token: 0x04004721 RID: 18209
		public string SaveVs = "Stuck Restraint";
	}
}
