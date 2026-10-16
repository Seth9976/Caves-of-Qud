using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AD9 RID: 2777
	public class RazedGoatfolkVillage
	{
		// Token: 0x06006FE9 RID: 28649 RVA: 0x00278278 File Offset: 0x00276478
		public bool BuildZone(Zone Z)
		{
			return new VillageMaker().BuildZone(Z, true, "ThatchedWall", true, "6", "5-BloodCistern", "GoatfolkVillageYurtRazed", "GoatfolkVillageGlobalsRazed", null, true);
		}
	}
}
