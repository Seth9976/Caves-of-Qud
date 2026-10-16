using System;
using XRL.Rules;
using XRL.World.Effects;
using XRL.World.Parts.Skill;

namespace XRL.World.Parts
{
	// Token: 0x02001138 RID: 4408
	[Serializable]
	public class ModFatecaller : IModification
	{
		// Token: 0x0600AEBC RID: 44732 RVA: 0x00408A45 File Offset: 0x00406C45
		public ModFatecaller()
		{
		}

		// Token: 0x0600AEBD RID: 44733 RVA: 0x00408A55 File Offset: 0x00406C55
		public ModFatecaller(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600AEBE RID: 44734 RVA: 0x00408A66 File Offset: 0x00406C66
		public override void Configure()
		{
			this.WorksOnSelf = true;
			this.NameForStatus = "ProbabilityEngine";
		}

		// Token: 0x0600AEBF RID: 44735 RVA: 0x00408A7A File Offset: 0x00406C7A
		public override bool SameAs(IPart p)
		{
			return (p as ModFatecaller).Chance == this.Chance && base.SameAs(p);
		}

		// Token: 0x0600AEC0 RID: 44736 RVA: 0x00408A98 File Offset: 0x00406C98
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetItemElementsEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600AEC1 RID: 44737 RVA: 0x00408AB8 File Offset: 0x00406CB8
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (!this.ParentObject.HasTag("Creature"))
			{
				int @for = GetSpecialEffectChanceEvent.GetFor(this.ParentObject.Equipped ?? this.ParentObject.Implantee, this.ParentObject, "Modification ModFatecaller Activation", this.Chance, null, null, true, false);
				E.Postfix.AppendRules(@for.ToString() + "% of the time, the Fates have their way.");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AEC2 RID: 44738 RVA: 0x00408B30 File Offset: 0x00406D30
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantObject(this.ParentObject))
			{
				E.Add("chance", 10);
				E.Add("might", 2);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AEC3 RID: 44739 RVA: 0x00408B60 File Offset: 0x00406D60
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600AEC4 RID: 44740 RVA: 0x00408B63 File Offset: 0x00406D63
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("WeaponHit");
			Registrar.Register("AttackerAfterDamage");
			Registrar.Register("DealingMissileDamage");
			Registrar.Register("WeaponMissileWeaponHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600AEC5 RID: 44741 RVA: 0x00408B9C File Offset: 0x00406D9C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "WeaponHit" || E.ID == "AttackerAfterDamage" || E.ID == "DealingMissileDamage" || E.ID == "WeaponMissileWeaponHit")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Attacker");
				GameObject gameObjectParameter2 = E.GetGameObjectParameter("Defender");
				GameObject gameObjectParameter3 = E.GetGameObjectParameter("Projectile");
				GameObject gameObject = gameObjectParameter;
				GameObject parentObject = this.ParentObject;
				string text = "Modification ModFatecaller Activation";
				GameObject gameObject2 = gameObjectParameter2;
				GameObject gameObject3 = gameObjectParameter3;
				if (GetSpecialEffectChanceEvent.GetFor(gameObject, parentObject, text, this.Chance, gameObject2, gameObject3, true, false).in100() && base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
				{
					gameObjectParameter2.Rainbowsplatter();
					gameObjectParameter2.PlayWorldSound("sfx_characterTrigger_fatesHaveTheirWay", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
					int num = Stat.Random(1, 41);
					if (num <= 5)
					{
						Axe_Dismember.Dismember(gameObjectParameter, gameObjectParameter2, null, null, null, null, "sfx_characterTrigger_dismember", false, false, false, false);
					}
					else if (num <= 10)
					{
						Axe_Decapitate.Decapitate(gameObjectParameter, gameObjectParameter2, null, null, null, null, false, false);
					}
					else if (num <= 15)
					{
						gameObjectParameter2.ApplyEffect(new Asleep(Stat.Random(1, 100), false, false, false, true), null);
					}
					else if (num <= 20)
					{
						gameObjectParameter2.ApplyEffect(new Confused(Stat.Random(1, 100), Stat.Random(1, 20), Stat.Random(1, 20), "Confusion"), null);
					}
					else if (num <= 25)
					{
						gameObjectParameter2.ApplyEffect(new Stun(Stat.Random(1, 100), Stat.Random(1, 20), false), null);
					}
					else if (num <= 30)
					{
						gameObjectParameter2.ApplyEffect(new Shaken(Stat.Random(1, 100), Stat.Random(1, 20)), null);
					}
					else if (num <= 35)
					{
						GameObject gameObject4 = gameObjectParameter;
						Cell cell = null;
						gameObject3 = gameObjectParameter2;
						gameObject4.Discharge(cell, Stat.Random(1, 100), 0, "1d" + Stat.Random(1, 10).ToString(), null, gameObjectParameter, this.ParentObject, gameObject3, null, null, null, null, 0, false, false, false);
					}
					else if (num <= 40)
					{
						gameObjectParameter2.ApplyEffect(new Bleeding("1d" + Stat.Random(1, 10).ToString(), Stat.Random(1, 100), gameObjectParameter, true, false, false, false), null);
					}
					else
					{
						gameObjectParameter2.CurrentCell.AddObject("Space-Time Vortex", null, null, null, null);
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x040047FB RID: 18427
		public int Chance = 50;
	}
}
