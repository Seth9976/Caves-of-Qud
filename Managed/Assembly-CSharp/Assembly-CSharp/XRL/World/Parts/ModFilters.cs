using System;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	// Token: 0x0200113A RID: 4410
	[Serializable]
	public class ModFilters : IModification
	{
		// Token: 0x0600AECF RID: 44751 RVA: 0x00408F37 File Offset: 0x00407137
		public ModFilters()
		{
		}

		// Token: 0x0600AED0 RID: 44752 RVA: 0x00408F3F File Offset: 0x0040713F
		public ModFilters(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600AED1 RID: 44753 RVA: 0x00408F48 File Offset: 0x00407148
		public override void Configure()
		{
			this.WorksOnEquipper = true;
		}

		// Token: 0x0600AED2 RID: 44754 RVA: 0x00408F51 File Offset: 0x00407151
		public override bool ModificationApplicable(GameObject Object)
		{
			return !Object.HasPart<GasMask>();
		}

		// Token: 0x0600AED3 RID: 44755 RVA: 0x00408F5C File Offset: 0x0040715C
		public override void ApplyModification(GameObject Object)
		{
			base.IncreaseDifficultyAndComplexity(1, 1, null);
		}

		// Token: 0x0600AED4 RID: 44756 RVA: 0x00408F67 File Offset: 0x00407167
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeApplyDamageEvent.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == PooledEvent<GetRespiratoryAgentPerformanceEvent>.ID || ID == GetShortDescriptionEvent.ID || ID == ModifyDefendingSaveEvent.ID;
		}

		// Token: 0x0600AED5 RID: 44757 RVA: 0x00408F9D File Offset: 0x0040719D
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddWithClause("{{Y|filters}}");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AED6 RID: 44758 RVA: 0x00408FC6 File Offset: 0x004071C6
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(ModFilters.GetDescription(this.Tier));
			return base.HandleEvent(E);
		}

		// Token: 0x0600AED7 RID: 44759 RVA: 0x00408FE6 File Offset: 0x004071E6
		public override bool HandleEvent(GetRespiratoryAgentPerformanceEvent E)
		{
			if (!E.WillAllowSave && E.Object == this.ParentObject.Equipped)
			{
				E.LinearAdjustment -= this.GetFilterPerformance() * 5;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AED8 RID: 44760 RVA: 0x0040901F File Offset: 0x0040721F
		public override bool HandleEvent(ModifyDefendingSaveEvent E)
		{
			if (SavingThrows.Applicable("Inhaled Gas", E.Vs))
			{
				E.Roll += this.GetFilterPerformance();
				E.IgnoreNatural1 = true;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AED9 RID: 44761 RVA: 0x00409054 File Offset: 0x00407254
		public override bool HandleEvent(BeforeApplyDamageEvent E)
		{
			if (E.Damage != null && E.Damage.HasAttribute("Gas") && E.Damage.HasAttribute("InhaleDanger") && E.Object == this.ParentObject.Equipped)
			{
				E.Damage.Amount = E.Damage.Amount * (100 - this.GetFilterPerformance() * 10) / 100;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AEDA RID: 44762 RVA: 0x004090CD File Offset: 0x004072CD
		public static int GetFilterPerformance(int Tier)
		{
			return 2 + Tier;
		}

		// Token: 0x0600AEDB RID: 44763 RVA: 0x004090D2 File Offset: 0x004072D2
		public int GetFilterPerformance()
		{
			return ModFilters.GetFilterPerformance(this.Tier);
		}

		// Token: 0x0600AEDC RID: 44764 RVA: 0x004090DF File Offset: 0x004072DF
		public static string GetDescription(int Tier)
		{
			return "Fitted with filters: This item protects against breathing in dangerous gases.";
		}
	}
}
