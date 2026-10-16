using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AAF RID: 2735
	public class GoatfolkYurts
	{
		// Token: 0x06006F59 RID: 28505 RVA: 0x0026D77C File Offset: 0x0026B97C
		public bool BuildZone(Zone Z)
		{
			return new VillageMaker().BuildZone(Z, true, "ThatchedWall", true, "1d6", "5-Cistern", "GoatfolkVillageYurt", "GoatfolkParty", null, true);
		}
	}
}
