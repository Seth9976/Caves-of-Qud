using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000ADB RID: 2779
	public class MinorRazedGoatfolkVillage
	{
		// Token: 0x06006FED RID: 28653 RVA: 0x00278404 File Offset: 0x00276604
		public bool BuildZone(Zone Z)
		{
			return new VillageMaker().BuildZone(Z, true, "ThatchedWall", true, "1", null, "GoatfolkVillageYurtMinorRazed", "GoatfolkVillageGlobalsMinorRazed", null, true);
		}
	}
}
