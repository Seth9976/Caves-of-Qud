using System;

namespace XRL.World.Parts
{
	// Token: 0x02001127 RID: 4391
	[Serializable]
	public class ModAirfoil : IModification
	{
		// Token: 0x0600ADBA RID: 44474 RVA: 0x00404C6E File Offset: 0x00402E6E
		public ModAirfoil()
		{
		}

		// Token: 0x0600ADBB RID: 44475 RVA: 0x00404C76 File Offset: 0x00402E76
		public ModAirfoil(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600ADBC RID: 44476 RVA: 0x00404C7F File Offset: 0x00402E7F
		public override void Configure()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600ADBD RID: 44477 RVA: 0x00404C88 File Offset: 0x00402E88
		public override bool ModificationApplicable(GameObject Object)
		{
			return Object.HasPart<ThrownWeapon>();
		}

		// Token: 0x0600ADBE RID: 44478 RVA: 0x00404C90 File Offset: 0x00402E90
		public override void ApplyModification(GameObject Object)
		{
			Object.ModIntProperty("ThrowRangeBonus", 4, true);
			base.IncreaseDifficultyAndComplexity(1, 1, null);
		}

		// Token: 0x0600ADBF RID: 44479 RVA: 0x00404CA9 File Offset: 0x00402EA9
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600ADC0 RID: 44480 RVA: 0x00404CC7 File Offset: 0x00402EC7
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddAdjective("{{Y|airfoil}}", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600ADC1 RID: 44481 RVA: 0x00404CF1 File Offset: 0x00402EF1
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Base.Compound(this.GetPhysicalDescription());
			E.Postfix.AppendRules(ModAirfoil.GetDescription(this.Tier));
			return base.HandleEvent(E);
		}

		// Token: 0x0600ADC2 RID: 44482 RVA: 0x00404D23 File Offset: 0x00402F23
		public string GetPhysicalDescription()
		{
			return "";
		}

		// Token: 0x0600ADC3 RID: 44483 RVA: 0x00404D2A File Offset: 0x00402F2A
		public static string GetDescription(int Tier)
		{
			return "Airfoil: This item can be thrown at +4 throwing range.";
		}
	}
}
