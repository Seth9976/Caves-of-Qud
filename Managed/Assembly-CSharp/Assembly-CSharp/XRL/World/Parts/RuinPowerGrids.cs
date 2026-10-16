using System;
using XRL.World.ZoneBuilders;

namespace XRL.World.Parts
{
	// Token: 0x0200130B RID: 4875
	[Serializable]
	public class RuinPowerGrids : IPart
	{
		// Token: 0x0600BECF RID: 48847 RVA: 0x0045DC01 File Offset: 0x0045BE01
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeZoneBuiltEvent.ID;
		}

		// Token: 0x0600BED0 RID: 48848 RVA: 0x0045DC18 File Offset: 0x0045BE18
		public override bool HandleEvent(BeforeZoneBuiltEvent E)
		{
			Zone currentZone = this.ParentObject.CurrentZone;
			if (currentZone != null)
			{
				new RuinPowerGrids
				{
					DamageChance = this.DamageChance,
					MissingElectricalChance = this.MissingElectricalChance,
					MissingElectricalProducers = this.MissingElectricalProducers,
					MissingElectricalConsumers = this.MissingElectricalConsumers,
					MissingHydraulicsChance = this.MissingHydraulicsChance,
					MissingHydraulicsProducers = this.MissingHydraulicsProducers,
					MissingHydraulicsConsumers = this.MissingHydraulicsConsumers,
					HydraulicLiquid = this.HydraulicLiquid,
					HydraulicLiquidTable = this.HydraulicLiquidTable,
					MissingMechanicalChance = this.MissingMechanicalChance,
					MissingMechanicalProducers = this.MissingMechanicalProducers,
					MissingMechanicalConsumers = this.MissingMechanicalConsumers,
					PreferWalls = this.PreferWalls,
					AvoidWalls = this.AvoidWalls
				}.BuildZone(currentZone);
			}
			this.ParentObject.Obliterate(null, false, null);
			return base.HandleEvent(E);
		}

		// Token: 0x04004CE6 RID: 19686
		public string DamageChance = "0-3x10";

		// Token: 0x04004CE7 RID: 19687
		public int MissingElectricalChance = 30;

		// Token: 0x04004CE8 RID: 19688
		public string MissingElectricalProducers = "1d3";

		// Token: 0x04004CE9 RID: 19689
		public string MissingElectricalConsumers = "1d6";

		// Token: 0x04004CEA RID: 19690
		public int MissingHydraulicsChance = 5;

		// Token: 0x04004CEB RID: 19691
		public string MissingHydraulicsProducers = "1d2";

		// Token: 0x04004CEC RID: 19692
		public string MissingHydraulicsConsumers = "1d6";

		// Token: 0x04004CED RID: 19693
		public string HydraulicLiquid;

		// Token: 0x04004CEE RID: 19694
		public string HydraulicLiquidTable = "HydraulicFluid";

		// Token: 0x04004CEF RID: 19695
		public int MissingMechanicalChance = 5;

		// Token: 0x04004CF0 RID: 19696
		public string MissingMechanicalProducers = "1d2";

		// Token: 0x04004CF1 RID: 19697
		public string MissingMechanicalConsumers = "1d4";

		// Token: 0x04004CF2 RID: 19698
		public bool PreferWalls = true;

		// Token: 0x04004CF3 RID: 19699
		public bool AvoidWalls;
	}
}
