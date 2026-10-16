using System;
using XRL.Language;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02001173 RID: 4467
	[Serializable]
	public class ModTimereaver : IModification
	{
		// Token: 0x0600B1B8 RID: 45496 RVA: 0x00412648 File Offset: 0x00410848
		public ModTimereaver()
		{
		}

		// Token: 0x0600B1B9 RID: 45497 RVA: 0x0041265F File Offset: 0x0041085F
		public ModTimereaver(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B1BA RID: 45498 RVA: 0x00412677 File Offset: 0x00410877
		public ModTimereaver(int Tier, int Chance)
			: this(Tier)
		{
			this.Chance = Chance;
		}

		// Token: 0x0600B1BB RID: 45499 RVA: 0x00412687 File Offset: 0x00410887
		public override void Configure()
		{
			this.WorksOnSelf = true;
			this.IsEMPSensitive = true;
			this.IsBootSensitive = true;
			base.IsTechScannable = true;
			this.NameForStatus = "TimelikeManifoldResonator";
		}

		// Token: 0x0600B1BC RID: 45500 RVA: 0x004126B0 File Offset: 0x004108B0
		public override bool ModificationApplicable(GameObject Object)
		{
			MeleeWeapon meleeWeapon;
			return Object.TryGetPart<MeleeWeapon>(out meleeWeapon) && !meleeWeapon.IsImprovisedWeapon();
		}

		// Token: 0x0600B1BD RID: 45501 RVA: 0x004126D2 File Offset: 0x004108D2
		public override void ApplyModification()
		{
			base.IncreaseDifficultyAndComplexity(1, 1, null);
		}

		// Token: 0x0600B1BE RID: 45502 RVA: 0x004126DD File Offset: 0x004108DD
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetItemElementsEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600B1BF RID: 45503 RVA: 0x004126FC File Offset: 0x004108FC
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			int @for = GetSpecialEffectChanceEvent.GetFor(this.ParentObject.Equipped ?? this.ParentObject.Implantee, this.ParentObject, "Modification ModTimereaver TimeStop", this.Chance, null, null, true, false);
			E.Postfix.AppendRules(string.Concat(new string[]
			{
				"Timereaver: This weapon has ",
				Grammar.A(@for, false),
				"% chance to stop time for two turns when ",
				this.ParentObject.it,
				this.ParentObject.GetVerb("hit", true, true, false, true, false),
				"."
			}));
			return base.HandleEvent(E);
		}

		// Token: 0x0600B1C0 RID: 45504 RVA: 0x004127A4 File Offset: 0x004109A4
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantObject(this.ParentObject))
			{
				E.Add("time", 10);
				E.Add("might", 2);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B1C1 RID: 45505 RVA: 0x004127D4 File Offset: 0x004109D4
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600B1C2 RID: 45506 RVA: 0x004127D7 File Offset: 0x004109D7
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("WeaponHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600B1C3 RID: 45507 RVA: 0x004127EC File Offset: 0x004109EC
		public override bool FireEvent(Event E)
		{
			if (E.ID == "WeaponHit")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Attacker");
				GameObject gameObjectParameter2 = E.GetGameObjectParameter("Defender");
				GameObject gameObject = gameObjectParameter;
				GameObject parentObject = this.ParentObject;
				string text = "Modification ModTimereaver TimeStop";
				GameObject gameObject2 = gameObjectParameter2;
				if (GetSpecialEffectChanceEvent.GetFor(gameObject, parentObject, text, this.Chance, gameObject2, null, true, false).in100() && base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null) && gameObjectParameter.FireEvent(Event.New("InitiateRealityDistortionLocal", "Object", gameObjectParameter, "Device", this.ParentObject, "RealityStabilizationPenetration", this.RealityStabilizationPenetration), E))
				{
					gameObjectParameter.DilationSplat();
					gameObjectParameter.ApplyEffect(new TimeCubed(2200), null);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004826 RID: 18470
		public int Chance = 3;

		// Token: 0x04004827 RID: 18471
		public int RealityStabilizationPenetration = 50;
	}
}
