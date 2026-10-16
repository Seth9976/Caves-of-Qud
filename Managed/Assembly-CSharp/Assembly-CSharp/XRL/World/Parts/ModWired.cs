using System;

namespace XRL.World.Parts
{
	// Token: 0x02001179 RID: 4473
	[Serializable]
	public class ModWired : IModification
	{
		// Token: 0x0600B1FC RID: 45564 RVA: 0x0041309E File Offset: 0x0041129E
		public ModWired()
		{
		}

		// Token: 0x0600B1FD RID: 45565 RVA: 0x004130A6 File Offset: 0x004112A6
		public ModWired(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B1FE RID: 45566 RVA: 0x004130AF File Offset: 0x004112AF
		public override void Configure()
		{
			this.WorksOnSelf = true;
			this.NameForStatus = "Wiring";
		}

		// Token: 0x0600B1FF RID: 45567 RVA: 0x004130C3 File Offset: 0x004112C3
		public override bool ModificationApplicable(GameObject Object)
		{
			return Object.IsWall();
		}

		// Token: 0x0600B200 RID: 45568 RVA: 0x004130CC File Offset: 0x004112CC
		public override void ApplyModification(GameObject Object)
		{
			int num = this.Tier * this.Tier * 500;
			ElectricalPowerTransmission electricalPowerTransmission = Object.RequirePart<ElectricalPowerTransmission>(false);
			if (electricalPowerTransmission.ChargeRate < num)
			{
				electricalPowerTransmission.ChargeRate = num;
			}
			base.IncreaseDifficultyIfComplex(1, null);
		}

		// Token: 0x0600B201 RID: 45569 RVA: 0x0041310D File Offset: 0x0041130D
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GenericRatingEvent>.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600B202 RID: 45570 RVA: 0x00413133 File Offset: 0x00411333
		public override bool HandleEvent(GenericRatingEvent E)
		{
			if (E.Type == "WallDigNavigationWeight" && E.Object == this.ParentObject)
			{
				E.Rating += 12;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B203 RID: 45571 RVA: 0x0041316B File Offset: 0x0041136B
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddAdjective("{{c|wired}}", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B204 RID: 45572 RVA: 0x00413195 File Offset: 0x00411395
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(ModWired.GetDescription(this.Tier));
			return base.HandleEvent(E);
		}

		// Token: 0x0600B205 RID: 45573 RVA: 0x004131B5 File Offset: 0x004113B5
		public static string GetDescription(int Tier)
		{
			return "Wired: Has electrical wiring.";
		}
	}
}
