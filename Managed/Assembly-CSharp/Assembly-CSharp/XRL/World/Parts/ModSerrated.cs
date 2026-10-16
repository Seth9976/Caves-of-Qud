using System;
using XRL.Language;
using XRL.World.Parts.Skill;

namespace XRL.World.Parts
{
	// Token: 0x02001167 RID: 4455
	[Serializable]
	public class ModSerrated : IModification
	{
		// Token: 0x0600B135 RID: 45365 RVA: 0x004111DF File Offset: 0x0040F3DF
		public ModSerrated()
		{
		}

		// Token: 0x0600B136 RID: 45366 RVA: 0x004111EE File Offset: 0x0040F3EE
		public ModSerrated(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B137 RID: 45367 RVA: 0x004111FE File Offset: 0x0040F3FE
		public override bool SameAs(IPart p)
		{
			return (p as ModSerrated).Chance == this.Chance && base.SameAs(p);
		}

		// Token: 0x0600B138 RID: 45368 RVA: 0x0041121C File Offset: 0x0040F41C
		public override void Configure()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600B139 RID: 45369 RVA: 0x00411228 File Offset: 0x0040F428
		public override bool ModificationApplicable(GameObject Object)
		{
			MeleeWeapon part = Object.GetPart<MeleeWeapon>();
			return part != null && (!(part.Skill != "LongBlades") || !(part.Skill != "Axe"));
		}

		// Token: 0x0600B13A RID: 45370 RVA: 0x00411268 File Offset: 0x0040F468
		public override void ApplyModification(GameObject Object)
		{
			base.IncreaseDifficultyAndComplexityIfComplex(1, 1, null);
		}

		// Token: 0x0600B13B RID: 45371 RVA: 0x00411273 File Offset: 0x0040F473
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600B13C RID: 45372 RVA: 0x00411299 File Offset: 0x0040F499
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName && !E.Object.HasPart<ModMicroserrated>())
			{
				E.AddAdjective("{{Y|serra{{R|t}}ed}}", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B13D RID: 45373 RVA: 0x004112D0 File Offset: 0x0040F4D0
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (!E.Object.HasPart<ModMicroserrated>())
			{
				E.Postfix.AppendRules(this.GetInstanceDescription());
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B13E RID: 45374 RVA: 0x004112F8 File Offset: 0x0040F4F8
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantObject(this.ParentObject))
			{
				E.Add("might", 1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B13F RID: 45375 RVA: 0x0041131B File Offset: 0x0040F51B
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600B140 RID: 45376 RVA: 0x0041131E File Offset: 0x0040F51E
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("WeaponHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600B141 RID: 45377 RVA: 0x00411334 File Offset: 0x0040F534
		public override bool FireEvent(Event E)
		{
			if (E.ID == "WeaponHit" && !this.ParentObject.HasPart<ModMicroserrated>())
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Attacker");
				GameObject gameObjectParameter2 = E.GetGameObjectParameter("Defender");
				GameObject gameObject = gameObjectParameter;
				GameObject parentObject = this.ParentObject;
				string text = "Modification ModSerrated Dismember";
				GameObject gameObject2 = gameObjectParameter2;
				if (GetSpecialEffectChanceEvent.GetFor(gameObject, parentObject, text, this.Chance, gameObject2, null, true, false).in100() && base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
				{
					bool flag = 1.in1000();
					Axe_Dismember.Dismember(gameObjectParameter, gameObjectParameter2, null, null, this.ParentObject, null, "sfx_characterTrigger_dismember", flag, !flag, false, false);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600B142 RID: 45378 RVA: 0x004113F8 File Offset: 0x0040F5F8
		public static string GetDescription(int Tier)
		{
			return "Serrated: This weapon has a chance to dismember opponents.";
		}

		// Token: 0x0600B143 RID: 45379 RVA: 0x00411400 File Offset: 0x0040F600
		public string GetInstanceDescription()
		{
			int @for = GetSpecialEffectChanceEvent.GetFor(this.ParentObject.Equipped ?? this.ParentObject.Implantee, this.ParentObject, "Modification ModSerrated Dismember", this.Chance, null, null, true, false);
			return "Serrated: This weapon has " + Grammar.A(@for, false) + "% chance to dismember opponents.";
		}

		// Token: 0x04004824 RID: 18468
		public int Chance = 3;
	}
}
