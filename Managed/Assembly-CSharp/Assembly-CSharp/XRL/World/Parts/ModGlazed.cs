using System;
using XRL.World.Parts.Skill;

namespace XRL.World.Parts
{
	// Token: 0x02001142 RID: 4418
	[Serializable]
	public class ModGlazed : IModification
	{
		// Token: 0x0600AF52 RID: 44882 RVA: 0x0040AF31 File Offset: 0x00409131
		public ModGlazed()
		{
		}

		// Token: 0x0600AF53 RID: 44883 RVA: 0x0040AF39 File Offset: 0x00409139
		public ModGlazed(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600AF54 RID: 44884 RVA: 0x0040AF42 File Offset: 0x00409142
		public override void Configure()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600AF55 RID: 44885 RVA: 0x0040AF4B File Offset: 0x0040914B
		public override void TierConfigure()
		{
			this.Chance = 10 + this.Tier * 2;
		}

		// Token: 0x0600AF56 RID: 44886 RVA: 0x0040AF5E File Offset: 0x0040915E
		public override bool ModificationApplicable(GameObject Object)
		{
			return Object.HasPart<MeleeWeapon>();
		}

		// Token: 0x0600AF57 RID: 44887 RVA: 0x0040AF66 File Offset: 0x00409166
		public override bool SameAs(IPart p)
		{
			return (p as ModGlazed).Chance == this.Chance && base.SameAs(p);
		}

		// Token: 0x0600AF58 RID: 44888 RVA: 0x0040AF84 File Offset: 0x00409184
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600AF59 RID: 44889 RVA: 0x0040AF9C File Offset: 0x0040919C
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (!this.ParentObject.IsCreature)
			{
				int @for = GetSpecialEffectChanceEvent.GetFor(this.ParentObject.Equipped ?? this.ParentObject.Implantee, this.ParentObject, "Modification ModGlazed Dismember", this.Chance, null, null, true, false);
				E.Postfix.AppendRules(@for.ToString() + "% chance to dismember on hit.");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AF5A RID: 44890 RVA: 0x0040B00F File Offset: 0x0040920F
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600AF5B RID: 44891 RVA: 0x0040B012 File Offset: 0x00409212
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("WeaponHit");
			Registrar.Register("AttackerAfterDamage");
			Registrar.Register("DealingMissileDamage");
			Registrar.Register("WeaponMissileWeaponHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600AF5C RID: 44892 RVA: 0x0040B048 File Offset: 0x00409248
		public override bool FireEvent(Event E)
		{
			if (E.ID == "WeaponHit" || E.ID == "AttackerAfterDamage" || E.ID == "DealingMissileDamage" || E.ID == "WeaponMissileWeaponHit")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Attacker");
				GameObject gameObjectParameter2 = E.GetGameObjectParameter("Defender");
				GameObject gameObjectParameter3 = E.GetGameObjectParameter("Projectile");
				GameObject gameObject = gameObjectParameter;
				GameObject parentObject = this.ParentObject;
				string text = "Modification ModGlazed Dismember";
				GameObject gameObject2 = gameObjectParameter2;
				GameObject gameObject3 = gameObjectParameter3;
				if (GetSpecialEffectChanceEvent.GetFor(gameObject, parentObject, text, this.Chance, gameObject2, gameObject3, true, false).in100() && base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
				{
					bool flag = 2.in1000();
					Axe_Dismember.Dismember(gameObjectParameter, gameObjectParameter2, null, null, this.ParentObject, null, "sfx_characterTrigger_dismember", flag, !flag, false, false);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004800 RID: 18432
		public int Chance;
	}
}
