using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AAC RID: 2732
	public class Music
	{
		// Token: 0x06006F53 RID: 28499 RVA: 0x0026CD72 File Offset: 0x0026AF72
		public bool BuildZone(Zone Z)
		{
			Z.SetMusic(this.Track);
			return true;
		}

		// Token: 0x0400331E RID: 13086
		public string Track = "";

		// Token: 0x0400331F RID: 13087
		public string Chance = "100";
	}
}
