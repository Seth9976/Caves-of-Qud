using System;

namespace XRL.World.Parts
{
	// Token: 0x0200115B RID: 4443
	[Serializable]
	public class ModPiping : IModification
	{
		// Token: 0x0600B09E RID: 45214 RVA: 0x0040F830 File Offset: 0x0040DA30
		public ModPiping()
		{
		}

		// Token: 0x0600B09F RID: 45215 RVA: 0x0040F843 File Offset: 0x0040DA43
		public ModPiping(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B0A0 RID: 45216 RVA: 0x0040F857 File Offset: 0x0040DA57
		public override bool SameAs(IPart p)
		{
			return !((p as ModPiping).Liquid != this.Liquid);
		}

		// Token: 0x0600B0A1 RID: 45217 RVA: 0x0040F874 File Offset: 0x0040DA74
		public override void ApplyModification(GameObject Object)
		{
			HydraulicPowerTransmission hydraulicPowerTransmission = Object.GetPart<HydraulicPowerTransmission>();
			if (hydraulicPowerTransmission == null)
			{
				hydraulicPowerTransmission = new HydraulicPowerTransmission();
				if (this.Tier > 1)
				{
					hydraulicPowerTransmission.ChargeRate = 1500 + this.Tier * 500;
				}
				Object.AddPart<HydraulicPowerTransmission>(hydraulicPowerTransmission, true, false);
			}
			else if (hydraulicPowerTransmission.ChargeRate < 1500 + this.Tier * 500)
			{
				hydraulicPowerTransmission.ChargeRate = 1500 + this.Tier * 500;
			}
			LiquidVolume liquidVolume = Object.LiquidVolume;
			if (liquidVolume == null)
			{
				liquidVolume = new LiquidVolume(this.Liquid, 8, 8);
				Object.AddPart<LiquidVolume>(liquidVolume, true, false);
			}
			else if (liquidVolume.Volume < liquidVolume.MaxVolume)
			{
				liquidVolume.Volume = liquidVolume.MaxVolume;
			}
			liquidVolume.Sealed = true;
			if (Object.GetIntProperty("ThermalInsulation", 0) < 900)
			{
				Object.SetIntProperty("ThermalInsulation", 900, false);
			}
			if (Object.Physics != null && Object.Physics.FlameTemperature < 1200)
			{
				Object.Physics.FlameTemperature = 1200;
			}
			base.IncreaseDifficulty(1, null);
		}

		// Token: 0x0600B0A2 RID: 45218 RVA: 0x0040F98C File Offset: 0x0040DB8C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GenericRatingEvent>.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600B0A3 RID: 45219 RVA: 0x0040F9B2 File Offset: 0x0040DBB2
		public override bool HandleEvent(GenericRatingEvent E)
		{
			if (E.Type == "WallDigNavigationWeight" && E.Object == this.ParentObject)
			{
				E.Rating += 20;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B0A4 RID: 45220 RVA: 0x0040F9EA File Offset: 0x0040DBEA
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddWithClause("piping");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B0A5 RID: 45221 RVA: 0x0040FA13 File Offset: 0x0040DC13
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(ModPiping.GetDescription(this.Tier));
			return base.HandleEvent(E);
		}

		// Token: 0x0600B0A6 RID: 45222 RVA: 0x0040FA33 File Offset: 0x0040DC33
		public static string GetDescription(int Tier)
		{
			return "Piping: Has hydraulic pipes installed.";
		}

		// Token: 0x0400481D RID: 18461
		public string Liquid = "water";
	}
}
