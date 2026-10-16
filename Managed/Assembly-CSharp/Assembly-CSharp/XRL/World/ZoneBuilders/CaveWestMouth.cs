using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A60 RID: 2656
	public class CaveWestMouth : IConnectionBuilder
	{
		// Token: 0x06006E36 RID: 28214 RVA: 0x0025A2BC File Offset: 0x002584BC
		public bool BuildZone(Zone Z)
		{
			this.Range = 3;
			return base.ConnectionMouth(Z, "Cave", "West", "Mouth");
		}
	}
}
