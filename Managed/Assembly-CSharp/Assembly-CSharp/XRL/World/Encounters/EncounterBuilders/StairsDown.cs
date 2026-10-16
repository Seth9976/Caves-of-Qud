using System;
using XRL.World.ZoneBuilders;

namespace XRL.World.Encounters.EncounterBuilders
{
	// Token: 0x02000BA5 RID: 2981
	public class StairsDown
	{
		// Token: 0x060076BE RID: 30398 RVA: 0x002CB3A1 File Offset: 0x002C95A1
		public bool BuildEncounter(Zone NewZone)
		{
			return new StairsDown
			{
				x = this.x,
				y = this.y
			}.BuildZone(NewZone);
		}

		// Token: 0x040036A6 RID: 13990
		public string x = "-1";

		// Token: 0x040036A7 RID: 13991
		public string y = "-1";
	}
}
