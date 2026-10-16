using System;
using XRL.UI;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	// Token: 0x02001174 RID: 4468
	[Serializable]
	public class ModTransmuteOnHit : IModification
	{
		// Token: 0x0600B1C4 RID: 45508 RVA: 0x004128C0 File Offset: 0x00410AC0
		public ModTransmuteOnHit()
		{
		}

		// Token: 0x0600B1C5 RID: 45509 RVA: 0x004128E5 File Offset: 0x00410AE5
		public ModTransmuteOnHit(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B1C6 RID: 45510 RVA: 0x0041290B File Offset: 0x00410B0B
		public ModTransmuteOnHit(int ChancePerThousand, string Table)
			: this()
		{
			this.ChancePerThousand = ChancePerThousand;
			this.Table = Table;
		}

		// Token: 0x0600B1C7 RID: 45511 RVA: 0x00412921 File Offset: 0x00410B21
		public override void Configure()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600B1C8 RID: 45512 RVA: 0x0041292A File Offset: 0x00410B2A
		public override bool ModificationApplicable(GameObject Object)
		{
			return Object.HasPart<MeleeWeapon>();
		}

		// Token: 0x0600B1C9 RID: 45513 RVA: 0x00412937 File Offset: 0x00410B37
		public override void ApplyModification()
		{
			base.IncreaseDifficultyAndComplexity(1, 1, null);
		}

		// Token: 0x0600B1CA RID: 45514 RVA: 0x00412944 File Offset: 0x00410B44
		public override bool SameAs(IPart p)
		{
			ModTransmuteOnHit modTransmuteOnHit = p as ModTransmuteOnHit;
			return modTransmuteOnHit.ChancePerThousand == this.ChancePerThousand && !(modTransmuteOnHit.Table != this.Table) && base.SameAs(p);
		}

		// Token: 0x0600B1CB RID: 45515 RVA: 0x00412984 File Offset: 0x00410B84
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600B1CC RID: 45516 RVA: 0x0041299C File Offset: 0x00410B9C
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (!this.ParentObject.HasTag("Creature"))
			{
				E.Postfix.Append("\n{{rules|");
				if (this.ChancePerThousand < 10)
				{
					E.Postfix.Append("Small");
				}
				else
				{
					E.Postfix.Append(this.ChancePerThousand / 10).Append('%');
				}
				E.Postfix.Append(" chance to transmute an enemy into ").Append(this.DescriptionTerm).Append(" on hit.")
					.Append("}}");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B1CD RID: 45517 RVA: 0x00412A3C File Offset: 0x00410C3C
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600B1CE RID: 45518 RVA: 0x00412A3F File Offset: 0x00410C3F
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("WeaponHit");
			Registrar.Register("AttackerAfterDamage");
			Registrar.Register("DealingMissileDamage");
			Registrar.Register("WeaponMissileWeaponHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600B1CF RID: 45519 RVA: 0x00412A78 File Offset: 0x00410C78
		public override bool FireEvent(Event E)
		{
			if (E.ID == "WeaponHit" || E.ID == "AttackerAfterDamage" || E.ID == "DealingMissileDamage" || E.ID == "WeaponMissileWeaponHit")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Attacker");
				GameObject gameObjectParameter2 = E.GetGameObjectParameter("Defender");
				if (gameObjectParameter != null && gameObjectParameter2 != null && gameObjectParameter2.IsHostileTowards(gameObjectParameter))
				{
					GameObject gameObject = gameObjectParameter;
					GameObject parentObject = this.ParentObject;
					string text = "Modification ModTransmuteOnHit Activation";
					GameObject gameObject2 = gameObjectParameter2;
					if (GetSpecialEffectChanceEvent.GetFor(gameObject, parentObject, text, this.ChancePerThousand, gameObject2, null, false, true).in1000() && base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null) && gameObjectParameter2.GetCurrentCell() != null)
					{
						if (!Options.UseParticleVFX)
						{
							gameObjectParameter2.Splatter("&B!");
							gameObjectParameter2.Splatter("&b?");
						}
						Transmutation.TransmuteObject(gameObjectParameter2, gameObjectParameter, E.GetGameObjectParameter("Weapon"), null, PopulationManager.RollOneFrom(this.Table, null, null).Blueprint, "=subject.T= =verb:were= transmuted into =object.an=.", "Transmute", "sfx_transmute_gemstone", this.Animate, false);
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004828 RID: 18472
		public const string MSG_TRANSMUTE_OBJECT = "=subject.T= =verb:were= transmuted into =object.an=.";

		// Token: 0x04004829 RID: 18473
		public int ChancePerThousand = 1;

		// Token: 0x0400482A RID: 18474
		public string Table = "Gemstones";

		// Token: 0x0400482B RID: 18475
		public string DescriptionTerm = "a gemstone";

		// Token: 0x0400482C RID: 18476
		public bool Animate;
	}
}
