using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02001159 RID: 4441
	[Serializable]
	public class ModPhaseConjugate : IModification
	{
		// Token: 0x0600B081 RID: 45185 RVA: 0x0040F3F8 File Offset: 0x0040D5F8
		public ModPhaseConjugate()
		{
		}

		// Token: 0x0600B082 RID: 45186 RVA: 0x0040F400 File Offset: 0x0040D600
		public ModPhaseConjugate(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B083 RID: 45187 RVA: 0x0040F409 File Offset: 0x0040D609
		public override void Configure()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600B084 RID: 45188 RVA: 0x0040F412 File Offset: 0x0040D612
		public override bool ModificationApplicable(GameObject Object)
		{
			return Object.HasTagOrProperty("Grenade") && !Object.HasPart<PhaseGrenade>() && !Object.HasPart<ModPhaseHarmonic>();
		}

		// Token: 0x0600B085 RID: 45189 RVA: 0x0040F438 File Offset: 0x0040D638
		public override void ApplyModification(GameObject Object)
		{
			base.IncreaseDifficultyAndComplexity(1, 2, null);
		}

		// Token: 0x0600B086 RID: 45190 RVA: 0x0040F443 File Offset: 0x0040D643
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<BeforeDetonateEvent>.ID || ID == PooledEvent<GenericQueryEvent>.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600B087 RID: 45191 RVA: 0x0040F474 File Offset: 0x0040D674
		public override bool HandleEvent(BeforeDetonateEvent E)
		{
			if (base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				Phased effect = this.ParentObject.GetEffect<Phased>();
				if (effect == null)
				{
					this.ParentObject.ForceApplyEffect(new Phased(1), null);
				}
				else
				{
					this.ParentObject.RemoveEffect(effect, true);
					if (this.ParentObject.GetIntProperty("ProjectilePhaseAdded", 0) > 0)
					{
						this.ParentObject.ModIntProperty("ProjectilePhaseAdded", -1, true);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B088 RID: 45192 RVA: 0x0040F509 File Offset: 0x0040D709
		public override bool HandleEvent(GenericQueryEvent E)
		{
			if (E.Query == "PhaseHarmonicEligible")
			{
				E.Result = false;
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B089 RID: 45193 RVA: 0x0040F52D File Offset: 0x0040D72D
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddAdjective("{{K|phase-conjugate}}", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B08A RID: 45194 RVA: 0x0040F557 File Offset: 0x0040D757
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Base.Compound(this.GetPhysicalDescription());
			E.Postfix.AppendRules(ModPhaseConjugate.GetDescription(this.Tier), base.GetEventSensitiveAddStatusSummary(E));
			return base.HandleEvent(E);
		}

		// Token: 0x0600B08B RID: 45195 RVA: 0x0040F590 File Offset: 0x0040D790
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600B08C RID: 45196 RVA: 0x0040F593 File Offset: 0x0040D793
		public string GetPhysicalDescription()
		{
			return "";
		}

		// Token: 0x0600B08D RID: 45197 RVA: 0x0040F59A File Offset: 0x0040D79A
		public static string GetDescription(int Tier)
		{
			return "Phase-conjugate: This explosive shifts phase immediately before detonating.";
		}
	}
}
