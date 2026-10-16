using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000ACE RID: 2766
	public class RuinPowerGrids : ZoneBuilderSandbox
	{
		// Token: 0x06006FCD RID: 28621 RVA: 0x002763D0 File Offset: 0x002745D0
		public bool BuildZone(Zone Z)
		{
			string text = this.DamageChance.RollCached().ToString();
			PowerGrid powerGrid = new PowerGrid();
			powerGrid.DamageChance = text;
			if (this.MissingElectricalChance.in100())
			{
				powerGrid.MissingProducers = this.MissingElectricalProducers;
				powerGrid.MissingConsumers = this.MissingElectricalConsumers;
			}
			powerGrid.PreferWalls = this.PreferWalls;
			powerGrid.AvoidWalls = this.AvoidWalls;
			powerGrid.BuildZone(Z);
			Hydraulics hydraulics = new Hydraulics();
			hydraulics.DamageChance = text;
			hydraulics.Liquid = this.HydraulicLiquid;
			hydraulics.LiquidTable = this.HydraulicLiquidTable;
			if (this.MissingHydraulicsChance.in100())
			{
				hydraulics.MissingProducers = this.MissingHydraulicsProducers;
				hydraulics.MissingConsumers = this.MissingHydraulicsConsumers;
			}
			hydraulics.PreferWalls = this.PreferWalls;
			hydraulics.AvoidWalls = this.AvoidWalls;
			hydraulics.BuildZone(Z);
			MechanicalPower mechanicalPower = new MechanicalPower();
			mechanicalPower.DamageChance = text;
			if (this.MissingMechanicalChance.in100())
			{
				mechanicalPower.MissingProducers = this.MissingMechanicalProducers;
				mechanicalPower.MissingConsumers = this.MissingMechanicalConsumers;
			}
			mechanicalPower.PreferWalls = this.PreferWalls;
			mechanicalPower.AvoidWalls = this.AvoidWalls;
			mechanicalPower.BuildZone(Z);
			Z.RebuildReachableMap();
			return true;
		}

		// Token: 0x0400335F RID: 13151
		public string DamageChance = "0-3x10";

		// Token: 0x04003360 RID: 13152
		public int MissingElectricalChance = 30;

		// Token: 0x04003361 RID: 13153
		public string MissingElectricalProducers = "1d3";

		// Token: 0x04003362 RID: 13154
		public string MissingElectricalConsumers = "1d6";

		// Token: 0x04003363 RID: 13155
		public int MissingHydraulicsChance = 5;

		// Token: 0x04003364 RID: 13156
		public string MissingHydraulicsProducers = "1d2";

		// Token: 0x04003365 RID: 13157
		public string MissingHydraulicsConsumers = "1d6";

		// Token: 0x04003366 RID: 13158
		public string HydraulicLiquid;

		// Token: 0x04003367 RID: 13159
		public string HydraulicLiquidTable = "HydraulicFluid";

		// Token: 0x04003368 RID: 13160
		public int MissingMechanicalChance = 5;

		// Token: 0x04003369 RID: 13161
		public string MissingMechanicalProducers = "1d2";

		// Token: 0x0400336A RID: 13162
		public string MissingMechanicalConsumers = "1d4";

		// Token: 0x0400336B RID: 13163
		public bool PreferWalls = true;

		// Token: 0x0400336C RID: 13164
		public bool AvoidWalls;
	}
}
