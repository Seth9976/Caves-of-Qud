using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000ABC RID: 2748
	public class OverlandRuins : ZoneBuilderSandbox
	{
		// Token: 0x06006F7B RID: 28539 RVA: 0x00271200 File Offset: 0x0026F400
		public bool BuildZone(Zone Z)
		{
			Coach.StartSection("Build OverlandRuins", false);
			if (70.in100())
			{
				new SultanDungeon().BuildRandomZone(Z, 5, false, "SultanDungeons_");
				The.ZoneManager.AddZonePostBuilderIfNotAlreadyPresent(Z.ZoneID, "ForceConnections");
				return true;
			}
			new Ruins
			{
				RuinLevel = this.RuinLevel,
				ZonesWide = this.ZonesWide,
				ZonesHigh = this.ZonesHigh
			}.BuildZone(Z);
			ZoneTemplateManager.Templates["SecretRuins"].Execute(Z, null);
			ZoneTemplateManager.Templates["SurfaceRuinsTechInfrastructure"].Execute(Z, null);
			return true;
		}

		// Token: 0x04003321 RID: 13089
		public int RuinLevel = 100;

		// Token: 0x04003322 RID: 13090
		public string ZonesWide = "1d3";

		// Token: 0x04003323 RID: 13091
		public string ZonesHigh = "1d2";
	}
}
