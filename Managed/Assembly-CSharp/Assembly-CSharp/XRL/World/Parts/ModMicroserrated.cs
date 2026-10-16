using System;
using XRL.Language;
using XRL.World.Parts.Skill;

namespace XRL.World.Parts
{
	/// This part is not used in the base game.
	// Token: 0x0200118D RID: 4493
	[Serializable]
	public class ModMicroserrated : IModification
	{
		// Token: 0x0600B2A5 RID: 45733 RVA: 0x004147BB File Offset: 0x004129BB
		public ModMicroserrated()
		{
		}

		// Token: 0x0600B2A6 RID: 45734 RVA: 0x004147CA File Offset: 0x004129CA
		public ModMicroserrated(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B2A7 RID: 45735 RVA: 0x004147DA File Offset: 0x004129DA
		public override bool SameAs(IPart p)
		{
			return (p as ModMicroserrated).Chance == this.Chance && base.SameAs(p);
		}

		// Token: 0x0600B2A8 RID: 45736 RVA: 0x004147F8 File Offset: 0x004129F8
		public override void Configure()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600B2A9 RID: 45737 RVA: 0x00414801 File Offset: 0x00412A01
		public override bool ModificationApplicable(GameObject Object)
		{
			return Object.HasPart<ModSerrated>();
		}

		// Token: 0x0600B2AA RID: 45738 RVA: 0x00414809 File Offset: 0x00412A09
		public override void ApplyModification(GameObject Object)
		{
			base.IncreaseDifficultyAndComplexityIfComplex(1, 1, null);
		}

		// Token: 0x0600B2AB RID: 45739 RVA: 0x00414814 File Offset: 0x00412A14
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600B2AC RID: 45740 RVA: 0x0041483A File Offset: 0x00412A3A
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddAdjective("{{Y|mi{{R|c}}roserra{{R|t}}ed}}", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B2AD RID: 45741 RVA: 0x00414864 File Offset: 0x00412A64
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(this.GetInstanceDescription());
			return base.HandleEvent(E);
		}

		// Token: 0x0600B2AE RID: 45742 RVA: 0x0041487F File Offset: 0x00412A7F
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantObject(this.ParentObject))
			{
				E.Add("might", 1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B2AF RID: 45743 RVA: 0x004148A2 File Offset: 0x00412AA2
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600B2B0 RID: 45744 RVA: 0x004148A5 File Offset: 0x00412AA5
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("WeaponHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600B2B1 RID: 45745 RVA: 0x004148BC File Offset: 0x00412ABC
		public override bool FireEvent(Event E)
		{
			if (E.ID == "WeaponHit")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Attacker");
				GameObject gameObjectParameter2 = E.GetGameObjectParameter("Defender");
				GameObject gameObject = gameObjectParameter;
				GameObject parentObject = this.ParentObject;
				string text = "Modification ModMicroserrated Dismember";
				GameObject gameObject2 = gameObjectParameter2;
				if (GetSpecialEffectChanceEvent.GetFor(gameObject, parentObject, text, this.Chance, gameObject2, null, true, false).in100() && base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
				{
					bool flag = 3.in1000();
					Axe_Dismember.Dismember(gameObjectParameter, gameObjectParameter2, null, null, this.ParentObject, null, "sfx_characterTrigger_dismember", flag, !flag, false, false);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600B2B2 RID: 45746 RVA: 0x00414970 File Offset: 0x00412B70
		public static string GetDescription(int Tier)
		{
			return "Microserrated: This weapon has a chance to dismember opponents.";
		}

		// Token: 0x0600B2B3 RID: 45747 RVA: 0x00414978 File Offset: 0x00412B78
		public string GetInstanceDescription()
		{
			int @for = GetSpecialEffectChanceEvent.GetFor(this.ParentObject.Equipped ?? this.ParentObject.Implantee, this.ParentObject, "Modification ModMicroserrated Dismember", this.Chance, null, null, true, false);
			return "Microserrated: This weapon has " + Grammar.A(@for, false) + "% chance to dismember opponents.";
		}

		// Token: 0x04004838 RID: 18488
		public int Chance = 7;
	}
}
