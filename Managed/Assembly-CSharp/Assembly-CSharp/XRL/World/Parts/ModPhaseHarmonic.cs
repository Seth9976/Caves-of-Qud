using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x0200115A RID: 4442
	[Serializable]
	public class ModPhaseHarmonic : IModification
	{
		// Token: 0x0600B08E RID: 45198 RVA: 0x0040F5A1 File Offset: 0x0040D7A1
		public ModPhaseHarmonic()
		{
		}

		// Token: 0x0600B08F RID: 45199 RVA: 0x0040F5A9 File Offset: 0x0040D7A9
		public ModPhaseHarmonic(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B090 RID: 45200 RVA: 0x0040F5B2 File Offset: 0x0040D7B2
		public override void Configure()
		{
			this.ChargeUse = 100;
			this.WorksOnSelf = true;
			base.IsTechScannable = true;
			this.NameForStatus = "PhaseRegulator";
		}

		// Token: 0x0600B091 RID: 45201 RVA: 0x0040F5D5 File Offset: 0x0040D7D5
		public override bool ModificationApplicable(GameObject Object)
		{
			return Object.HasTag("NoModPhaseHarmonic") || GenericQueryEvent.Check(Object, "PhaseHarmonicEligible", null, null, 0, false);
		}

		// Token: 0x0600B092 RID: 45202 RVA: 0x0040F5FA File Offset: 0x0040D7FA
		public override void ApplyModification(GameObject Object)
		{
			if (Object.HasPart<AmmoGrenade>())
			{
				this.ChargeUse = 0;
			}
			else
			{
				Object.RequirePart<EnergyCellSocket>(false);
			}
			base.IncreaseDifficultyAndComplexity(1, 2, null);
		}

		// Token: 0x0600B093 RID: 45203 RVA: 0x0040F61E File Offset: 0x0040D81E
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<BeforeDetonateEvent>.ID || ID == PooledEvent<GetActivationPhaseEvent>.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600B094 RID: 45204 RVA: 0x0040F64C File Offset: 0x0040D84C
		public override bool HandleEvent(BeforeDetonateEvent E)
		{
			if (base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				this.ParentObject.ForceApplyEffect(new Omniphase(1), null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B095 RID: 45205 RVA: 0x0040F69B File Offset: 0x0040D89B
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddAdjective("{{phase-harmonic|phase-harmonic}}", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B096 RID: 45206 RVA: 0x0040F6C5 File Offset: 0x0040D8C5
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Base.Compound(this.GetPhysicalDescription());
			E.Postfix.AppendRules(ModPhaseHarmonic.GetDescription(this.Tier), base.GetEventSensitiveAddStatusSummary(E));
			return base.HandleEvent(E);
		}

		// Token: 0x0600B097 RID: 45207 RVA: 0x0040F700 File Offset: 0x0040D900
		public override bool HandleEvent(GetActivationPhaseEvent E)
		{
			if ((E.Phase == 1 || E.Phase == 2) && base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				E.Phase = 3;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B098 RID: 45208 RVA: 0x0040F755 File Offset: 0x0040D955
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600B099 RID: 45209 RVA: 0x0040F758 File Offset: 0x0040D958
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ProjectileSetup");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600B09A RID: 45210 RVA: 0x0040F770 File Offset: 0x0040D970
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ProjectileSetup")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Projectile");
				if (GameObject.Validate(ref gameObjectParameter) && !gameObjectParameter.HasEffect<Omniphase>() && base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
				{
					gameObjectParameter.ForceApplyEffect(new Omniphase("ModPhaseHarmonic"), null);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600B09B RID: 45211 RVA: 0x0040F7ED File Offset: 0x0040D9ED
		public string GetPhysicalDescription()
		{
			return "";
		}

		// Token: 0x0600B09C RID: 45212 RVA: 0x0040F7F4 File Offset: 0x0040D9F4
		public static string GetDescription(int Tier)
		{
			return "Phase-Harmonic: This weapon can affect both in-phase and out-of-phase objects.";
		}

		// Token: 0x0600B09D RID: 45213 RVA: 0x0040F7FC File Offset: 0x0040D9FC
		public static bool IsProjectileCompatible(string BlueprintName)
		{
			if (!BlueprintName.IsNullOrEmpty())
			{
				GameObjectBlueprint blueprintIfExists = GameObjectFactory.Factory.GetBlueprintIfExists(BlueprintName);
				if (blueprintIfExists != null && blueprintIfExists.HasPart("OmniphaseProjectile"))
				{
					return false;
				}
			}
			return true;
		}
	}
}
