using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AB0 RID: 2736
	public class GoatfolkQlippothYurts
	{
		// Token: 0x06006F5B RID: 28507 RVA: 0x0026D7BC File Offset: 0x0026B9BC
		public bool BuildZone(Zone Z)
		{
			return new VillageMaker().BuildZone(Z, true, "HolographicThatchedWall", true, "1-3", "3-QlippothBloodCistern", "GoatfolkQlippothVillageYurt", "GoatfolkQlippothParty", null, true);
		}
	}
}
