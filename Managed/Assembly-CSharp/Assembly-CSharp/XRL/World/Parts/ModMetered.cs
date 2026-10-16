using System;

namespace XRL.World.Parts
{
	// Token: 0x02001151 RID: 4433
	[Serializable]
	public class ModMetered : IModification
	{
		// Token: 0x0600B00E RID: 45070 RVA: 0x0040D930 File Offset: 0x0040BB30
		public ModMetered()
		{
		}

		// Token: 0x0600B00F RID: 45071 RVA: 0x0040D938 File Offset: 0x0040BB38
		public ModMetered(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B010 RID: 45072 RVA: 0x0040D941 File Offset: 0x0040BB41
		public override void Configure()
		{
			this.WorksOnSelf = true;
			this.NameForStatus = "ChargeMeter";
		}

		// Token: 0x0600B011 RID: 45073 RVA: 0x0040D955 File Offset: 0x0040BB55
		public override bool ModificationApplicable(GameObject Object)
		{
			return Object.HasPart<EnergyCell>();
		}

		// Token: 0x0600B012 RID: 45074 RVA: 0x0040D95D File Offset: 0x0040BB5D
		public override void ApplyModification(GameObject Object)
		{
			EnergyCell part = Object.GetPart<EnergyCell>();
			part.ChargeDisplayStyle = "percentage";
			part.AltChargeDisplayStyle = "percentage";
			base.IncreaseComplexity(1, null);
		}

		// Token: 0x0600B013 RID: 45075 RVA: 0x0040D982 File Offset: 0x0040BB82
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600B014 RID: 45076 RVA: 0x0040D9A0 File Offset: 0x0040BBA0
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddAdjective("{{c|metered}}", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B015 RID: 45077 RVA: 0x0040D9CA File Offset: 0x0040BBCA
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(ModMetered.GetDescription(this.Tier));
			return base.HandleEvent(E);
		}

		// Token: 0x0600B016 RID: 45078 RVA: 0x0040D9EA File Offset: 0x0040BBEA
		public static string GetDescription(int Tier)
		{
			return "Metered: This item has a readout displaying its charge percentage.";
		}
	}
}
