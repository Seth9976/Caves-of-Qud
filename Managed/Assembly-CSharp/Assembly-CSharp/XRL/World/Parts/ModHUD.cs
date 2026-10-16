using System;

namespace XRL.World.Parts
{
	/// This part is not used in the base game.
	// Token: 0x02001143 RID: 4419
	[Serializable]
	public class ModHUD : IModification
	{
		// Token: 0x0600AF5D RID: 44893 RVA: 0x0040B144 File Offset: 0x00409344
		public ModHUD()
		{
		}

		// Token: 0x0600AF5E RID: 44894 RVA: 0x0040B14C File Offset: 0x0040934C
		public ModHUD(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600AF5F RID: 44895 RVA: 0x0040B155 File Offset: 0x00409355
		public override void Configure()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600AF60 RID: 44896 RVA: 0x0040B160 File Offset: 0x00409360
		public override bool ModificationApplicable(GameObject Object)
		{
			return IModification.CheckWornSlot(Object, "Head", "Face", null, null, true, true, true, false);
		}

		// Token: 0x0600AF61 RID: 44897 RVA: 0x0040B184 File Offset: 0x00409384
		public override void ApplyModification(GameObject Object)
		{
			Object.RequirePart<HUD>(false);
			Object.RequirePart<EnergyCellSocket>(false);
			int num = Math.Max((this.Tier >= 5) ? (8 - this.Tier) : (this.Tier + 1), 2);
			BootSequence part = Object.GetPart<BootSequence>();
			if (part == null)
			{
				Object.AddPart<BootSequence>(new BootSequence
				{
					BootTime = num,
					ReadoutInName = true,
					ReadoutInDescription = true
				}, true, false);
			}
			else if (part.BootTime < num)
			{
				part.BootTime = num;
			}
			base.IncreaseDifficultyAndComplexity(3, 2, null);
		}

		// Token: 0x0600AF62 RID: 44898 RVA: 0x0040B20D File Offset: 0x0040940D
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600AF63 RID: 44899 RVA: 0x0040B22B File Offset: 0x0040942B
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddAdjective("HUD", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AF64 RID: 44900 RVA: 0x0040B255 File Offset: 0x00409455
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(ModHUD.GetDescription(this.Tier), base.GetEventSensitiveAddStatusSummary(E));
			return base.HandleEvent(E);
		}

		// Token: 0x0600AF65 RID: 44901 RVA: 0x0040B27C File Offset: 0x0040947C
		public static string GetDescription(int Tier)
		{
			return "HUD: When powered and started up, this item can be used with a smartgun to enable its bonuses.";
		}
	}
}
