using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AA5 RID: 2725
	public class JungleRuins : ZoneBuilderSandbox
	{
		// Token: 0x06006F26 RID: 28454 RVA: 0x00269D7C File Offset: 0x00267F7C
		public bool BuildZone(Zone Z)
		{
			if (this.Chance.in100())
			{
				new Ruins
				{
					RuinLevel = this.RuinLevel,
					ZonesWide = this.ZonesWide,
					ZonesHigh = this.ZonesHigh
				}.BuildZone(Z);
				ZoneTemplateManager.Templates["SecretRuins"].Execute(Z, null);
				ZoneTemplateManager.Templates["SurfaceRuinsTechInfrastructure"].Execute(Z, null);
			}
			return true;
		}

		// Token: 0x040032F0 RID: 13040
		public int RuinLevel = 100;

		// Token: 0x040032F1 RID: 13041
		public int Chance = 100;

		// Token: 0x040032F2 RID: 13042
		public string ZonesWide = "1d3";

		// Token: 0x040032F3 RID: 13043
		public string ZonesHigh = "1d2";
	}
}
