using System;
using System.Text;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x020010EE RID: 4334
	[Serializable]
	public class ProneOnHit : IPart
	{
		// Token: 0x0600ABBF RID: 43967 RVA: 0x003EEC0C File Offset: 0x003ECE0C
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AttackerHit");
			Registrar.Register("ProjectileHit");
			Registrar.Register("WeaponHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600ABC0 RID: 43968 RVA: 0x003EEC38 File Offset: 0x003ECE38
		public override bool FireEvent(Event E)
		{
			if (E.ID == "AttackerHit" || E.ID == "WeaponHit" || E.ID == "ProjectileHit")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Attacker");
				GameObject gameObjectParameter2 = E.GetGameObjectParameter("Defender");
				GameObject gameObjectParameter3 = E.GetGameObjectParameter("Weapon");
				GameObject gameObjectParameter4 = E.GetGameObjectParameter("Projectile");
				if (GameObject.Validate(ref gameObjectParameter2))
				{
					GameObject gameObject = gameObjectParameter;
					GameObject gameObject2 = gameObjectParameter3;
					string text = "Part ProneOnHit Activation";
					GameObject gameObject3 = gameObjectParameter2;
					GameObject gameObject4 = gameObjectParameter4;
					if (GetSpecialEffectChanceEvent.GetFor(gameObject, gameObject2, text, this.Chance, gameObject3, gameObject4, true, false).in100())
					{
						GameObject gameObject5 = gameObjectParameter2;
						string saveStat = this.SaveStat;
						int saveTarget = this.SaveTarget;
						string saveVs = this.SaveVs;
						if (!gameObject5.MakeSave(saveStat, saveTarget, gameObjectParameter, this.AttackerStat, saveVs, false, false, false, false, (E.ID == "WeaponHit") ? gameObjectParameter3 : null))
						{
							gameObjectParameter2.ApplyEffect(new Prone(), null);
						}
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600ABC1 RID: 43969 RVA: 0x003EED2F File Offset: 0x003ECF2F
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600ABC2 RID: 43970 RVA: 0x003EED45 File Offset: 0x003ECF45
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(new Action<StringBuilder>(this.AppendEffect));
			return base.HandleEvent(E);
		}

		// Token: 0x0600ABC3 RID: 43971 RVA: 0x003EED66 File Offset: 0x003ECF66
		public void AppendEffect(StringBuilder SB)
		{
			if (this.Chance <= 0)
			{
				return;
			}
			if (this.ParentObject != null && this.ParentObject.IsProjectile)
			{
				SB.Append("Causes enemies to be knocked prone");
				return;
			}
			SB.Append("Melee attacks cause enemies to be knocked prone");
		}

		// Token: 0x040046F7 RID: 18167
		public int Chance = 100;

		// Token: 0x040046F8 RID: 18168
		public int SaveTarget = 25;

		// Token: 0x040046F9 RID: 18169
		public string SaveVs = "Knockdown";

		// Token: 0x040046FA RID: 18170
		public string SaveStat = "Strength";

		// Token: 0x040046FB RID: 18171
		public string AttackerStat;
	}
}
